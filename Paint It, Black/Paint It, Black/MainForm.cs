using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_It__Black {
    public partial class MainForm : Form {

        //lists containing the controls:
        List<Button> buttons = new List<Button>();
        List<Label> labels = new List<Label>();

        byte[] clickRequirements = { 0, 2, 3, 4, 6 };  //useful 
        string[] shapeNames = { "nulloid", "segment", "triangle", "quadrangle", "hexagon" };    //less useful

        byte selectedShape = 0; //if this is 0 it means we're not drawing at this moment

        List<Point> clickedPoints = new List<Point>();

        List<Shape> shapes = new List<Shape>();
        private int tempVertexIndex = -1;
        private Shape tempShape;
        private bool moving;

        private class Shape {

            public List<Point> Vertices { get; private set; }
            public Color Color { get; private set; }
            public bool HasBorder { get; internal set; }

            public Shape(List<Point> pointsClicked, Color color) {
                Vertices = pointsClicked;
                Color = color;
            }

        }

        public MainForm() {
            InitializeComponent();
            //add controls to the lists, and by the way add event handlers to the buttons:
            foreach (Control c in splitContainer.Panel1.Controls) {
                if (c is Label && !c.Equals(colorButtonExplainingLabel)) labels.Add(c as Label);
                if (c is Button && !c.Equals(colorPickerButton)) {
                    buttons.Add(c as Button);
                    c.Click += button_Click;
                }
            }
            drawingPanel.Click += drawingPanel_Click;
            drawingPanel.MouseMove += drawingPanel_MouseMove;
            drawingPanel.MouseDown += drawingPanel_MouseDown;
            drawingPanel.MouseUp += drawingPanel_MouseUp;
            drawingPanel.MouseDoubleClick += drawingPanel_MouseDoubleClick;

            KeyDown += KeyDown_Handler;
        }

        private void KeyDown_Handler(object sender, KeyEventArgs e) {
            if (moving) {
                switch (e.KeyCode) {
                    case Keys.Enter:
                        tempShape.HasBorder = false;
                        moving = false;
                        break;

                    case Keys.Down:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = tempShape.Vertices[i];
                            v.Y -= 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Up:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = tempShape.Vertices[i];
                            v.Y += 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Left:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = tempShape.Vertices[i];
                            v.X -= 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Right:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = tempShape.Vertices[i];
                            v.X += 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;
                }
            }
            drawingPanel.Invalidate();

        }

        private void button_Click(object sender, EventArgs e) {
            selectedShape = byte.Parse((sender as Button).Tag as string);
            foreach (var b in buttons) b.Enabled = false;
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Visible = true;
            label.Text = "Click " + clickRequirements[selectedShape] + " more points to draw a " + shapeNames[selectedShape];
        }

        private void colorPickerButton_Click(object sender, EventArgs e) {
            colorDialog.ShowDialog();
            colorPickerButton.BackColor = colorDialog.Color;
        }

        private void drawingPanel_Click(object sender, EventArgs e) {
            MouseEventArgs args = e as MouseEventArgs;
            if (selectedShape > 0) {
                clickedPoints.Add(args.Location);
                UpdateLabel();
                if (clickedPoints.Count == clickRequirements[selectedShape]) DrawShape();
            }
            if (moving) {
                GraphicsPath gp = new GraphicsPath();
                gp.AddPolygon(tempShape.Vertices.ToArray());
                if (gp.IsVisible(args.Location)) {
                    moving = false;
                    tempShape.HasBorder = false;
                }
            }
        }

        private void UpdateLabel() {
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Text = "Click " + (clickRequirements[selectedShape] - clickedPoints.Count) + " more points to draw a " + shapeNames[selectedShape] + ".";
        }

        private void DrawShape() {
            Shape s = new Shape(new List<Point>(clickedPoints), colorDialog.Color);
            shapes.Add(s);
            drawingPanel.Refresh();
            foreach (var b in buttons) b.Enabled = true;
            foreach (var l in labels) l.Visible = false;
            clickedPoints.Clear();
            selectedShape = 0;
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e) {
            Graphics g = drawingPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var shape in shapes) {
                if (!shape.HasBorder) {
                    Pen pen = new Pen(shape.Color, 2);
                    g.DrawPolygon(pen, shape.Vertices.ToArray());
                    g.FillPolygon(pen.Brush, shape.Vertices.ToArray());
                    pen.Dispose();
                } else {
                    Pen pen = new Pen(Color.Yellow, 5);
                    g.DrawPolygon(pen, shape.Vertices.ToArray());
                    pen.Color = shape.Color;
                    g.FillPolygon(pen.Brush, shape.Vertices.ToArray());
                    pen.Dispose();
                }

            }
            g.Dispose();
        }

        private void drawingPanel_MouseDown(object sender, MouseEventArgs e) {
            if (selectedShape == 0) {
                foreach (var shape in shapes) {
                    for (int i = 0; i < shape.Vertices.Count; i++) {
                        Point vertex = shape.Vertices[i];
                        if (AreArbitrarilyClose(vertex, e.Location)) {
                            tempShape = shape;
                            tempVertexIndex = i;
                        }
                    }
                }
            }
        }

        private bool AreArbitrarilyClose(Point vertex, Point p) {
            //hilarious method, makes clicking "on" a vertex easier.
            if (vertex.Equals(p)) return true;
            p.Y++;
            if (vertex.Equals(p)) return true;
            p.X++;
            if (vertex.Equals(p)) return true;
            p.Y--;
            if (vertex.Equals(p)) return true;
            p.Y--;
            if (vertex.Equals(p)) return true;
            p.X--;
            if (vertex.Equals(p)) return true;
            p.X--;
            if (vertex.Equals(p)) return true;
            p.Y++;
            if (vertex.Equals(p)) return true;
            p.Y++;
            if (vertex.Equals(p)) return true;
            return false;
        }

        private void drawingPanel_MouseMove(object sender, MouseEventArgs e) {
            if (selectedShape == 0 && tempVertexIndex != -1) {
                tempShape.Vertices[tempVertexIndex] = new Point(e.X, e.Y);
                drawingPanel.Refresh();
            }
        }

        private void drawingPanel_MouseUp(object sender, MouseEventArgs e) {
            if (selectedShape == 0 && tempVertexIndex != -1) {
                tempVertexIndex = -1;
                shapes.Remove(tempShape);
                shapes.Add(tempShape);
                drawingPanel.Invalidate();
            }
        }

        private void drawingPanel_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (selectedShape == 0 && tempVertexIndex == -1 && !moving) {
                foreach (var shape in shapes) {
                    GraphicsPath gp = new GraphicsPath();
                    if (shape.Vertices.Count > 2) gp.AddPolygon(shape.Vertices.ToArray());
                    else gp.AddLine(shape.Vertices[0], shape.Vertices[1]);
                    if (gp.IsVisible(e.Location)) {
                        shape.HasBorder = true;
                        moving = true;
                        tempShape = shape;
                        drawingPanel.Invalidate();
                        foreach (var c in splitContainer.Panel1.Controls)
                            if (c is Button) (c as Button).Enabled = false;
                    }
                }
            }
            if (moving) {
                tempShape.HasBorder = false;
                moving = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            //we refresh everything!
            foreach (var b in buttons) b.Enabled = true;
            foreach (var l in labels) l.Visible = false;
            shapes.Clear();
            clickedPoints.Clear();
            selectedShape = 0;
            colorDialog.Color = Color.Black;
            colorPickerButton.BackColor = colorDialog.Color;
            drawingPanel.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            //show the dialog, get the correct path:
            folderBrowserDialog.ShowDialog();
            string path = folderBrowserDialog.SelectedPath;
            //make a bitmap, draw on it just as you would do on the drawingPanel:
            Bitmap bitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var shape in shapes) {
                Pen pen = new Pen(shape.Color, 2);
                g.DrawPolygon(pen, shape.Vertices.ToArray());
                if (shape.Vertices.Count > 2) g.FillPolygon(pen.Brush, shape.Vertices.ToArray());
                pen.Dispose();
            }
            g.Dispose();

            bitmap.Save(path + "/drawing.bmp");
        }
    }
}

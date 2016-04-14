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
                if (c is Button && !(c.Tag as string).Equals("5")) {
                    buttons.Add(c as Button);
                    c.Click += button_Click;
                }
            }
            canvas.Click += Canvas_Click;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseDoubleClick += Canvas_MouseDoubleClick;
            canvas.Paint += Canvas_Paint;

            KeyUp += KeyUp_Handler;

        }

        private void KeyUp_Handler(object sender, KeyEventArgs e) {
            if (moving) {
                switch (e.KeyData) {
                    case Keys.Enter:
                        StopMoving();
                        break;

                    case Keys.Up:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.Y -= 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Down:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.Y += 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Left:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.X -= 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Right:
                        for (int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.X += 5;
                            tempShape.Vertices[i] = v;
                        }
                        break;
                }
            }
            canvas.Refresh();

        }

        private void button_Click(object sender, EventArgs e) {
            selectedShape = byte.Parse((sender as Button).Tag as string);
            foreach (var b in buttons) b.Enabled = false;
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Visible = true;
            label.Text = "Click " + clickRequirements[selectedShape] + " more points to draw a " + shapeNames[selectedShape];
        }

        private void Canvas_Click(object sender, EventArgs e) {
            MouseEventArgs args = e as MouseEventArgs;
            if (selectedShape > 0) {
                clickedPoints.Add(args.Location);
                UpdateLabel();
                if (clickedPoints.Count == clickRequirements[selectedShape]) CreateShape();
            }
            if (moving) {
                GraphicsPath gp = new GraphicsPath();
                if (tempShape.Vertices.Count > 2) gp.AddPolygon(tempShape.Vertices.ToArray());
                else gp.AddLine(tempShape.Vertices[0], tempShape.Vertices[1]);
                if (gp.IsVisible(args.Location) || gp.IsOutlineVisible(args.Location, new Pen(Color.Black, 3))) {
                    StopMoving();
                }
            }
        }

        private void StopMoving() {
            moving = false;
            tempShape.HasBorder = false;
            shapes.Remove(tempShape);
            shapes.Add(tempShape);
            foreach (var c in splitContainer.Panel1.Controls) 
                if (c is Button) (c as Button).Enabled = true;
        }

        private void UpdateLabel() {
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Text = "Click " + (clickRequirements[selectedShape] - clickedPoints.Count) + " more points to draw a " + shapeNames[selectedShape] + ".";
        }

        private void CreateShape() {
            Shape s = new Shape(new List<Point>(clickedPoints), colorDialog.Color);
            shapes.Add(s);
            canvas.Invalidate();
            foreach (var b in buttons) b.Enabled = true;
            foreach (var l in labels) l.Visible = false;
            clickedPoints.Clear();
            selectedShape = 0;
        }

        private Bitmap GetBitmap() {
            Bitmap theBitmap = new Bitmap(canvas.Width, canvas.Height);
            Graphics g = Graphics.FromImage(theBitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var shape in shapes) {
                Pen pen = new Pen(shape.Color, 2);
                g.DrawPolygon(pen, shape.Vertices.ToArray());
                g.FillPolygon(pen.Brush, shape.Vertices.ToArray());
                pen.Dispose();
                if (shape.HasBorder) {
                    pen = new Pen(Color.Yellow, 2);
                    g.DrawPolygon(pen, shape.Vertices.ToArray());
                }
            }
            g.Dispose();
            return theBitmap;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            canvas.BackgroundImage = GetBitmap();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e) {
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

        private void Canvas_MouseMove(object sender, MouseEventArgs e) {
            if (selectedShape == 0 && tempVertexIndex != -1) {
                tempShape.Vertices[tempVertexIndex] = new Point(e.X, e.Y);
                canvas.Refresh();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e) {
            if (selectedShape == 0 && tempVertexIndex != -1) {
                tempVertexIndex = -1;
                shapes.Remove(tempShape);
                shapes.Add(tempShape);
                canvas.Refresh();
            }
        }

        private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (selectedShape == 0 && tempVertexIndex == -1 && !moving) {
                for (int i = shapes.Count - 1; i >= 0; i--) {
                    Shape shape = shapes[i];
                    Point p = new Point(e.X, e.Y);
                    GraphicsPath gp = new GraphicsPath();
                    if (shape.Vertices.Count > 2) gp.AddPolygon(shape.Vertices.ToArray());
                    else gp.AddLine(shape.Vertices[0], shape.Vertices[1]);
                    if (gp.IsVisible(p) || gp.IsOutlineVisible(p, new Pen(Color.Black, 3))) {
                        shape.HasBorder = true;
                        moving = true;
                        tempShape = shape;
                        foreach (var c in splitContainer.Panel1.Controls)
                            if (c is Button) (c as Button).Enabled = false;
                        break;
                    }
                }
            } else if (moving) StopMoving();
            canvas.Refresh();
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
            canvas.Refresh();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            //show the dialog, get the correct path:
            folderBrowserDialog.ShowDialog();
            string path = folderBrowserDialog.SelectedPath;
            GetBitmap().Save(path + "/drawing.bmp");
        }

        private void colorPickerButton_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK) colorPickerButton.BackColor = colorDialog.Color;
        }
    }
}

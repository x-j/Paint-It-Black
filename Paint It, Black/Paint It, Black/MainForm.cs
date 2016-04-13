using System;
using System.Collections.Generic;
using System.Drawing;
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

        private class Shape {

            public List<Point> Vertices { get; private set; }
            public Color Color { get; private set; }

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
            if (selectedShape > 0) {
                MouseEventArgs args = e as MouseEventArgs;
                clickedPoints.Add(new Point(args.X, args.Y));
                UpdateLabel();
                if (clickedPoints.Count == clickRequirements[selectedShape]) DrawShape();
            } else {
                //TODO: implement moving of the shapes here
            }
        }

        private void UpdateLabel() {
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Text = "Click " + (clickRequirements[selectedShape] - clickedPoints.Count) + " more points to draw a " + shapeNames[selectedShape]+".";
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
            foreach (var shape in shapes) {
                Graphics g = drawingPanel.CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                if (shape.Vertices.Count == 2) g.DrawLine(new Pen(shape.Color, 2), shape.Vertices[0], shape.Vertices[1]);
                else {
                    Pen pen = new Pen(shape.Color, 2);
                    g.DrawPolygon(pen, shape.Vertices.ToArray());
                    g.FillPolygon(pen.Brush, shape.Vertices.ToArray());
                    pen.Dispose();
                }
                g.Dispose();
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
    }
}

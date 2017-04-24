using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_It__Black {
    public partial class MainForm : Form {

        private const int V = 5;
        private const int VERTEX_CLICK_TOLERANCE = 5;

        //lists containing the controls:
        List<Button> buttons = new List<Button>();
        List<Label> labels = new List<Label>();

        byte[] clickRequirements = { 0, 2, 3, 4, 6, 1 };  //useful 
        string[] shapeNames = { "nulloid", "segment", "triangle", "quadrangle", "hexagon", "circle" };    //less useful

        byte selectedShape = 0; //if this is 0 it means we're not drawing at this moment

        List<Point> clickedPoints = new List<Point>();

        List<Shape> shapes = new List<Shape>();
        private int tempVertexIndex = -1;
        private Shape tempShape;
        private bool isMoving;
        private bool isUpToDate = true;
        private FilenameAsker asker = new FilenameAsker();

        private class Shape {

            public List<Point> Vertices { get; private set; }
            public Color Color { get; private set; }
            public bool HasBorder { get; internal set; }
            public int Thickness { get; internal set; }
            public bool AntiAliased { get; internal set; }
            public int Radius { get; internal set; }

            public Shape(List<Point> pointsClicked, Color color) {
                Vertices = pointsClicked;
                Color = color;
            }

        }

        public MainForm() {
            InitializeComponent();
            //add controls to the lists, and by the way add event handlers to the buttons:
            foreach(Control c in splitContainer.Panel1.Controls) {
                if(c is Label)
                    labels.Add(c as Label);
                if(c is Button && !(c.Tag as string).Equals("color picker")) {
                    buttons.Add(c as Button);
                    c.Click += button_Click;

                    // 2017 CODE: MAKE INVISIBLE ALL BUTTONS EXCEPT FOR THE SEGMENT ONE.
                    if(!((c.Tag as string).Equals("1") || (c.Tag as string).Equals("5")))
                        c.Visible = false;
                }
            }
            canvas.Click += Canvas_Click;
            canvas.Paint += Canvas_Paint;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseDoubleClick += Canvas_MouseDoubleClick;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if(isMoving) {
                switch(keyData) {
                    case Keys.Enter:
                        StopMoving();
                        break;

                    case Keys.Up:
                        for(int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.Y -= V;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Down:
                        for(int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.Y += V;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Left:
                        for(int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.X -= V;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    case Keys.Right:
                        for(int i = 0; i < tempShape.Vertices.Count; i++) {
                            Point v = new Point(tempShape.Vertices[i].X, tempShape.Vertices[i].Y);
                            v.X += V;
                            tempShape.Vertices[i] = v;
                        }
                        break;

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            isUpToDate = false;
            canvas.Refresh();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button_Click(object sender, EventArgs e) {
            selectedShape = byte.Parse((sender as Button).Tag as string);
            foreach(var b in buttons)
                b.Enabled = false;
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Visible = true;
            label.Text = "Click " + clickRequirements[selectedShape] + " more points to draw a " + shapeNames[selectedShape];
        }

        private void Canvas_Click(object sender, EventArgs e) {
            MouseEventArgs args = e as MouseEventArgs;
            if(selectedShape > 0) {
                clickedPoints.Add(args.Location);
                UpdateLabel();
                if(clickedPoints.Count == clickRequirements[selectedShape])
                    CreateShape();
            }
            // HANDLE MOVING SHAPES, THIS IS NOT REQUIRED FOR 2017 AND BY THE WAY I DIDN'T IMPLEMENT DRAWING HERE.
            if(isMoving) {
                GraphicsPath gp = new GraphicsPath();
                if(tempShape.Vertices.Count > 2)
                    gp.AddPolygon(tempShape.Vertices.ToArray());
                else
                    gp.AddLine(tempShape.Vertices[0], tempShape.Vertices[1]);
                if(gp.IsVisible(args.Location) || gp.IsOutlineVisible(args.Location, new Pen(Color.Black, 3))) {
                    StopMoving();
                }
            }
        }

        private void StopMoving() {
            isMoving = false;
            tempShape.HasBorder = false;
            foreach(var c in splitContainer.Panel1.Controls)
                if(c is Button)
                    (c as Button).Enabled = true;
            isUpToDate = false;
            canvas.Refresh();
        }

        private void UpdateLabel() {
            Label label = labels.Find(l => (l.Tag as string).Equals(selectedShape.ToString()));
            label.Text = "Click " + (clickRequirements[selectedShape] - clickedPoints.Count) + " more points to draw a " + shapeNames[selectedShape] + ".";
        }

        private void CreateShape() {
            Shape s = new Shape(new List<Point>(clickedPoints), colorDialog.Color) {
                Thickness = (int)numericUpDown.Value,
                AntiAliased = antialiasCheckBox.Checked
            };
            if(s.Vertices.Count == 1)
                s.Radius = (int)radiusPicker.Value;
            shapes.Add(s);
            isUpToDate = false;
            canvas.Refresh();
            foreach(var b in buttons)
                b.Enabled = true;
            foreach(var l in labels)
                l.Visible = false;
            clickedPoints.Clear();
            selectedShape = 0;
        }

        private Bitmap DrawBitmap() {
            //2017: this method actually handles drawing the lines. Mid-point etc. should go here.

            //some sweet code below, shame we can't use it :(

            //Bitmap theBitmap = new Bitmap(canvas.Width, canvas.Height);
            //Graphics g = Graphics.FromImage(theBitmap);
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            //foreach (var shape in shapes) {
            //    Pen pen = new Pen(shape.Color, 2);
            //    g.DrawPolygon(pen, shape.Vertices.ToArray());
            //    g.FillPolygon(pen.Brush, shape.Vertices.ToArray());
            //    pen.Dispose();
            //    if (shape.HasBorder) {
            //        pen = new Pen(Color.Yellow, 2);
            //        g.DrawPolygon(pen, shape.Vertices.ToArray());
            //    }
            //}
            //g.Dispose();

            Bitmap oldBitmap = new Bitmap(canvas.Width, canvas.Height);

            foreach(var shape in shapes) {
                //this just to make it easier for me, but all shapes should be lines (for now?!)
                if(shape.Vertices.Count == 2) {
                    // it is a line!
                    Point[] vertices = shape.Vertices.ToArray();
                    Point vA = vertices[0];
                    Point vB = vertices[1];

                    // check if the points are located inside the bitmap:
                    if(new Rectangle(new Point(0, 0), oldBitmap.Size).Contains(vA) && new Rectangle(new Point(0, 0), oldBitmap.Size).Contains(vB)) {
                        if(shape.AntiAliased) {
                            ThickAntialiasedLine(vA, vB, shape.Thickness, oldBitmap, shape.Color);
                        } else {
                            // if we're not doing antialiasing, then just use pixel copying
                            for(int i = 0; i < shape.Thickness; i++) {
                                if(i % 2 == 0) {
                                    vA.Y -= i + 1;
                                    vB.Y -= i + 1;
                                    DrawMidpointLine(vA, vB, oldBitmap, shape.Color);
                                    vA.Y += i + 1;
                                    vB.Y += i + 1;
                                } else {
                                    DrawMidpointLine(vA, vB, oldBitmap, shape.Color);
                                    vA.Y += 1;
                                    vB.Y += 1;
                                }
                            }
                        }
                    }
                }
                if(shape.Vertices.Count == 1) {
                    //it is a circle!
                    Point p = shape.Vertices[0];
                    MidpointCircle(p.X, p.Y, shape.Radius, oldBitmap, shape.Color);
                }

            }

            return oldBitmap;
        }
        void MidpointCircle(int xCenter, int yCenter, int radius, Bitmap bitmap, Color color) {
            int dE = 3;
            int dSE = 5 - 2 * radius;
            int d = 1 - radius;
            //y += radius;
            //bitmap.SetPixel(x, y, color);
            //while(y > x) {
            //    if(d < 0) { //move to E
            //        d += dE;
            //        dE += 2;
            //        dSE += 2;
            //    } else { //move to SE
            //        d += dSE;
            //        dE += 2;
            //        dSE += 4;
            //        --y;
            //    }
            //    ++x;
            //    bitmap.SetPixel(x, y, color);
            //}

            int x = 0;
            int y = radius;
            bitmap.SetPixel(x + xCenter, y + yCenter, color);
            while(y > x) {
                if(d < 0) {
                    d += 2 * x + 3;
                } else {
                    d += ((2 * x) - (2 * y) + 5);
                    --y;
                }
                ++x;
                //Circle has 8-fold symmetry
                bitmap.SetPixel(x + xCenter, y + yCenter, color);
                bitmap.SetPixel(y + xCenter, x + yCenter, color);
                bitmap.SetPixel(y + xCenter, -x + yCenter, color);
                bitmap.SetPixel(x + xCenter, -y + yCenter, color);
                bitmap.SetPixel(-x + xCenter, -y + yCenter, color);
                bitmap.SetPixel(-y + xCenter, -x + yCenter, color);
                bitmap.SetPixel(-y + xCenter, x + yCenter, color);
                bitmap.SetPixel(-x + xCenter, y + yCenter, color);
            }
        }

        float IntensifyPixel(int x, int y, float thickness, float distance, Bitmap bitmap, Color color) {
            float cov = distance;
            if(cov > 0)
                if(x > 0 && x < bitmap.Width && y > 0 && y < bitmap.Height)
                    bitmap.SetPixel(x, y, Lerp(Color.White, color, cov));
            return cov;
        }

        private Color Lerp(Color c1, Color c2, float cov) {
            int newR = (int)((1 - cov) * c1.R + cov * c2.R);
            if(newR < 0)
                newR = 0;
            int newG = (int)((1 - cov) * c1.G + cov * c2.G);
            if(newG < 0)
                newG = 0;
            int newB = (int)((1 - cov) * c1.B + cov * c2.B);
            if(newB < 0)
                newB = 0;
            return Color.FromArgb(newR, newG, newB);
        }

        void ThickAntialiasedLine(Point p1, Point p2, float thickness, Bitmap bitmap, Color color) {

            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            thickness = thickness / 2;
            //initial values in Bresenham;s algorithm
            int dx = x2 - x1, dy = y2 - y1;
            int dE = 2 * dy, dNE = 2 * (dy - dx);
            int d = 2 * dy - dx;
            int two_v_dx = 0; //numerator, v=0 for the first pixel
            float invDenom = (float)(1.0 / (2 * Math.Sqrt(dx * dx + dy * dy))); //inverted denominator
            float two_dx_invDenom = 2 * dx * invDenom; //precomputed constant
            int x = x1, y = y1;
            IntensifyPixel(x, y, thickness, 0, bitmap, color);
            for(int i = 1; i < thickness; i++)
                IntensifyPixel(x, y + i, thickness, i * two_dx_invDenom, bitmap, color);
            for(int i = 1; i < thickness; ++i)
                IntensifyPixel(x, y - i, thickness, i * two_dx_invDenom, bitmap, color);
            while(x < x2) {
                ++x;
                if(d < 0) // move to E
                {
                    two_v_dx = d + dx;
                    d += dE;
                } else // move to NE
                  {
                    two_v_dx = d - dx;
                    d += dNE;
                    ++y;
                }
                // Now set the chosen pixel and its neighbors
                IntensifyPixel(x, y, thickness, two_v_dx * invDenom, bitmap, color);
                for(int i = 0; i < thickness/2 + 1; i++)
                    IntensifyPixel(x, y + i, thickness, two_dx_invDenom + two_v_dx * invDenom - i, bitmap, color);
                for(int i = 1; i < thickness/2 + 1; i++)
                    IntensifyPixel(x, y - i, thickness, two_dx_invDenom - two_v_dx * invDenom - i, bitmap, color);

            }
        }


        void DrawMidpointLine(Point p1, Point p2, Bitmap bitmap, Color color) {


            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;

            int dx = x2 - x1;
            int dy = y2 - y1;
            int d = 2 * dy - dx; // initial value of d
            int dE = 2 * dy; // increment used when moving to E
            int dNE = 2 * (dy - dx); // increment used when movint to NE
            int x = x1, y = y1;
            bitmap.SetPixel(x, y, color);
            while(x < x2) {
                if(d < 0) // move to E
                {
                    d += dE;
                    x++;
                } else // move to NE
                  {
                    d += dNE;
                    ++x;
                    ++y;
                }
                bitmap.SetPixel(x, y, color);
            }
        }


        private void Canvas_Paint(object sender, PaintEventArgs e) {
            if(!isUpToDate) {
                canvas.BackgroundImage = DrawBitmap();
                isUpToDate = true;
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e) {
            if(selectedShape == 0) {
                foreach(var shape in shapes) {
                    for(int i = 0; i < shape.Vertices.Count; i++) {
                        Point vertex = shape.Vertices[i];
                        if(AreArbitrarilyClose(vertex, e.Location)) {
                            tempShape = shape;
                            tempVertexIndex = i;
                        }
                    }
                }
            }
        }

        private bool AreArbitrarilyClose(Point vertex, Point p) {
            //hilarious method, makes clicking "on" a vertex easier.
            int radius = VERTEX_CLICK_TOLERANCE;
            int xDistance = vertex.X - p.X;
            int yDistance = vertex.Y - p.Y;
            if(xDistance * xDistance + yDistance * yDistance <= radius)
                return true;
            else
                return false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e) {
            if(selectedShape == 0 && tempVertexIndex != -1) {
                tempShape.Vertices[tempVertexIndex] = new Point(e.X, e.Y);
                shapes.Remove(tempShape);
                shapes.Add(tempShape);
                isUpToDate = false;
                canvas.Refresh();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e) {
            if(selectedShape == 0 && tempVertexIndex != -1) {
                tempVertexIndex = -1;
                shapes.Remove(tempShape);
                shapes.Add(tempShape);
                isUpToDate = false;
                canvas.Refresh();
            }
        }

        private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e) {
            if(selectedShape == 0 && tempVertexIndex == -1 && !isMoving) {
                for(int i = shapes.Count - 1; i >= 0; i--) {
                    Shape shape = shapes[i];
                    if(shape.Vertices.Count == 1)
                        return;
                    Point p = new Point(e.X, e.Y);
                    GraphicsPath gp = new GraphicsPath();
                    if(shape.Vertices.Count > 2)
                        gp.AddPolygon(shape.Vertices.ToArray());
                    else
                        gp.AddLine(shape.Vertices[0], shape.Vertices[1]);
                    if(gp.IsVisible(p) || gp.IsOutlineVisible(p, new Pen(Color.Black, 3))) {
                        shape.HasBorder = true;
                        isMoving = true;
                        splitContainer.Panel2.Focus();
                        tempShape = shape;
                        shapes.Remove(tempShape);
                        shapes.Add(tempShape);
                        break;
                    }
                }
            } else if(isMoving)
                StopMoving();
            isUpToDate = false;
            canvas.Refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            //we refresh everything!
            foreach(var b in buttons)
                b.Enabled = true;
            foreach(var l in labels)
                l.Visible = false;
            shapes.Clear();
            clickedPoints.Clear();
            selectedShape = 0;
            colorDialog.Color = Color.Black;
            colorPickerButton.BackColor = colorDialog.Color;
            isUpToDate = false;
            canvas.Refresh();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            //show the dialog, get the correct path:
            DialogResult result = folderBrowserDialog.ShowDialog();
            if(result == DialogResult.OK) {
                string path = folderBrowserDialog.SelectedPath;
                asker.ShowDialog();
                string filename = asker.textBox.Text;
                DrawBitmap().Save(path + "\\" + filename + ".bmp");
            }
        }

        private void colorPickerButton_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog.ShowDialog();
            if(result == DialogResult.OK)
                colorPickerButton.BackColor = colorDialog.Color;
        }

    }
}

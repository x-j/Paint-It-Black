using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_It__Black {
    public partial class MainForm : Form {

        private const int V = 5;
        private const int VERTEX_CLICK_TOLERANCE = 15;

        //lists containing the controls:
        List<Button> buttons = new List<Button>();
        List<Label> labels = new List<Label>();

        string[] shapeNames = { "nulloid", "segment", "rectangle", "conves", "hexagon", "circle" };    //not so useful
        byte[] clickRequirements = { 0, 2, 2, 100, 6, 1 };  //kinda useful

        byte LEFT = 1, RIGHT = 2, BOTTOM = 4, TOP = 8;  //outcodes
        enum FillType { STANDARD, FLOOD };

        byte selectedShape = 0; //if this is 0 it means we're not drawing at this moment

        Queue<Point> clickedPoints = new Queue<Point>();

        List<Shape> shapes = new List<Shape>();
        private int tempVertexIndex = -1;
        private Shape tempShape;
        private bool isMoving;
        private bool isUpToDate = true;
        private FilenameAsker asker = new FilenameAsker();
        private Shape clippingShape;
        private bool awaitingFlood = false;
        private Shape shapeToFlood;
        private bool startSelected = false;

        private Point startFlood = new Point();

        private class Shape {

            public List<Point> Vertices { get; set; }
            public bool HasBorder { get; internal set; }
            public int Thickness { get; internal set; }
            public bool AntiAliased { get; internal set; }
            public int Radius { get; internal set; }
            public int MultiSampling { get; internal set; }
            public Color LineColor { get; internal set; }
            public Color FillColor { get; internal set; }
            public bool Filled { get; internal set; }
            public FillType FillType { get; internal set; }

            public Shape(List<Point> pointsClicked) {
                Vertices = pointsClicked;
            }

        }

        private class Bucket {
            public Point a;
            public Point b;
            public int yMax;
            public int yMin;
            public double currentX;
            public int startingX;
            public int dx;
            public int dy;
            public double invSlope;
            public int sign;

            public Bucket(Point a, Point b) {
                this.a = new Point(new Size(a));
                this.b = new Point(new Size(b));
                dx = b.X - a.X;
                dy = b.Y - a.Y;
                yMax = Math.Max(a.Y, b.Y);
                yMin = Math.Min(a.Y, b.Y);
                startingX = a.X;
                invSlope = Math.Abs(((double)dx) / dy);
                currentX = a.X + invSlope;
                sign = Math.Sign(dx) * Math.Sign(dy);
            }
        }

        public MainForm() {
            InitializeComponent();
            //add controls to the lists, and by the way add event handlers to the buttons:
            foreach(Control c in splitContainer.Panel1.Controls) {
                if(c is Label)
                    labels.Add(c as Label);
                if(c.Tag != null) {
                    if(c is Button && !(c.Tag as string).Equals("color picker")) {
                        buttons.Add(c as Button);
                        c.Click += button_Click;
                    }
                }
            }
            canvas.Click += Canvas_Click;
            canvas.Paint += Canvas_Paint;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseDoubleClick += Canvas_MouseDoubleClick;

            canvas.BackgroundImage = new Bitmap(canvas.Width, canvas.Height);
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
            if(label != null) {
                label.Visible = true;
                label.Text = "Click " + clickRequirements[selectedShape] + " more points to draw a " + shapeNames[selectedShape];
            }
        }

        private void Canvas_Click(object sender, EventArgs e) {
            MouseEventArgs args = e as MouseEventArgs;
            if(awaitingFlood && !startSelected /*and check if it's inside the polygon*/) {
                startFlood = args.Location;
                startSelected = true;
                isUpToDate = false;
                canvas.Refresh();
            }
            if(selectedShape > 0) {
                if(selectedShape == 3) {
                    Graphics graphics = Graphics.FromImage(canvas.BackgroundImage);
                    Point p = args.Location;
                    graphics.FillEllipse(new SolidBrush(Color.DarkCyan), p.X - 3, p.Y - 3, 6, 6);
                    canvas.Refresh();
                    if(clickedPoints.Count > 0 && AreArbitrarilyClose(clickedPoints.Peek(), args.Location))
                        CreateShape();
                    else
                        clickedPoints.Enqueue(args.Location);

                } else {
                    clickedPoints.Enqueue(args.Location);
                    UpdateLabel();
                    if(clickedPoints.Count == clickRequirements[selectedShape])
                        CreateShape();
                }

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
            if(label != null) {
                int remainingClicks = clickRequirements[selectedShape] - clickedPoints.Count;
                if(remainingClicks > 0)
                    label.Text = "Click " + remainingClicks + " more points to draw a " + shapeNames[selectedShape] + ".";
                else
                    label.Text = "";
            }

        }

        private void CreateShape() {
            Shape s = new Shape(new List<Point>(clickedPoints)) {
                Thickness = (int)numericUpDown.Value,
                AntiAliased = antialiasCheckBox.Checked,
                MultiSampling = (int)supersamplingUpDown.Value,
                LineColor = lineColorPickerButton.BackColor,
                FillColor = fillColorPickerButton.BackColor
            };
            // if the selected shape was a rectangle, infer the two remaining vertices (only two were clicked, and we need 4 in total)
            if(selectedShape == 2) {
                Point p1 = clickedPoints.Dequeue();
                Point p3 = clickedPoints.Dequeue();
                Point p2 = new Point(p1.X, p3.Y);
                Point p4 = new Point(p3.X, p1.Y);
                List<Point> newVs = new List<Point>();
                newVs.Add(p1);
                newVs.Add(p2);
                newVs.Add(p3);
                newVs.Add(p4);
                s.Vertices = newVs;
            }

            if(s.Vertices.Count == 1)
                s.Radius = (int)radiusPicker.Value;

            if(standardFillingCheckbox.Checked) {
                s.Filled = true;
                s.FillType = FillType.STANDARD;
            }
            if(floodFillingCheckBox.Checked) {
                s.Filled = true;
                s.FillType = FillType.FLOOD;
                shapeToFlood = s;
            }

            if(clippingCheckBox.Checked) {
                clippingCheckBox.Checked = false;
                clippingShape = s;  //remember this dude for later clipping purposes
            }

            shapes.Add(s);

            isUpToDate = false;
            canvas.Refresh();
            foreach(var b in buttons)
                b.Enabled = true;
            clickedPoints.Clear();
            selectedShape = 0;

            awaitingFlood = true;
            startSelected = false;
        }

        private Bitmap DrawBitmap() {

            //2017: this method actually handles drawing the lines. Mid-point etc. should go here.
            Bitmap oldBitmap = new Bitmap(canvas.Width, canvas.Height);

            foreach(var shape in shapes) {
                if(shape.Vertices.Count == 2) {
                    // it is a line!
                    Point a = shape.Vertices[0];
                    Point b = shape.Vertices[1];

                    // check if it clips against the clippingShape
                    if(clippingShape != null) {
                        CohenSutherland(a, b, shape, oldBitmap);
                    } else
                        DrawAccordingLine(a, b, shape, oldBitmap);
                }
                if(shape.Vertices.Count == 1) {
                    //it is a circle!
                    Point p = shape.Vertices[0];
                    MidpointCircle(p.X, p.Y, shape.Radius, oldBitmap, shape.LineColor);
                }
                if(shape.Vertices.Count >= 4) {
                    List<Bucket> buckets = new List<Bucket>();
                    for(int i = 0; i < shape.Vertices.Count; i++) {
                        Point a;
                        if(i == 0)
                            a = shape.Vertices.FindLast(p => true);
                        else
                            a = shape.Vertices[i - 1];
                        Point b = shape.Vertices[i];

                        Bucket bucket = new Bucket(a, b);
                        buckets.Add(bucket);
                        DrawAccordingLine(a, b, shape, oldBitmap);
                    }
                    if(shape.Filled) {
                        if(shape.FillType == FillType.STANDARD) {
                            // sort the buckets
                            oldBitmap = ScanlineAET(shape, buckets, oldBitmap);

                        } else if(shape.FillType == FillType.FLOOD) {
                            if(startSelected)
                                oldBitmap = FloodFill(shape, startFlood, oldBitmap);
                        }
                    }
                }
            }

            return oldBitmap;
        }

        private Bitmap ScanlineAET(Shape shape, List<Bucket> buckets, Bitmap oldBitmap) {

            Bitmap newBitmap = new Bitmap(oldBitmap);

            buckets.Sort(delegate (Bucket a, Bucket b) {
                if(a.yMin < b.yMin)
                    return -1;
                else if(a.yMin == b.yMin) {
                    if(a.a.Y + a.b.Y < b.a.Y + b.b.Y)
                        return -1;
                    else if(a.a.Y + a.b.Y > b.a.Y + b.b.Y)
                        return 1;
                    else
                        return 0;
                } else
                    return 1;
            });

            Bucket lowestBucket = buckets[0];
            List<Bucket> activeEdgeTable = new List<Bucket>();
            for(int scanline = lowestBucket.yMin; buckets.Count > 0; scanline++) {
                // add edges that might need to be added to AET
                for(int i = 0; i < buckets.Count; i++) {
                    Bucket edge = buckets[i];
                    if(edge.yMin == scanline) {
                        activeEdgeTable.Add(edge);
                        buckets.Remove(edge);
                        i--;
                    }
                }
                // remove edges that need to be removed from AET
                for(int i = 0; i < activeEdgeTable.Count; i++) {
                    Bucket edge = activeEdgeTable[i];
                    if(edge.yMax == scanline)
                        activeEdgeTable.Remove(edge);
                }
                // sort edges in AET by x
                activeEdgeTable.Sort(delegate (Bucket a, Bucket b) {
                    if(a.currentX < b.currentX)
                        return -1;
                    else if(a.currentX > b.currentX)
                        return 1;
                    else
                        return 0;
                });
                // Fill in the scan line between pairs of edges from AET
                for(int i = 1; i < activeEdgeTable.Count; i++) {
                    Bucket leftEdge = activeEdgeTable[i - 1];
                    Bucket rightEdge = activeEdgeTable[i];
                    for(double doublex = leftEdge.currentX; doublex < rightEdge.currentX; doublex++) {
                        int x = (int)doublex;
                        if(newBitmap.GetPixel(x, scanline).ToArgb() != shape.LineColor.ToArgb())
                            newBitmap.SetPixel(x, scanline, shape.FillColor);
                    }
                    // increment current X's if not vertical
                    if(leftEdge.dx != 0)
                        leftEdge.currentX += leftEdge.sign * leftEdge.invSlope;
                    if(rightEdge.dx != 0)
                        rightEdge.currentX += rightEdge.sign * rightEdge.invSlope;
                }
            }
            return newBitmap;
        }

        private Bitmap FloodFill(Shape shape, Point start, Bitmap oldBitmap) {

            Bitmap newBitmap = new Bitmap(oldBitmap);

            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(start);
            while(queue.Count > 0) {
                Point p = queue.Dequeue();
                if(newBitmap.GetPixel(p.X, p.Y).Equals(shape.LineColor)) {
                    continue;
                } else {
                    newBitmap.SetPixel(p.X, p.Y, shape.FillColor);

                    Point left = new Point(p.X - 1, p.Y);
                    Point right = new Point(p.X + 1, p.Y);
                    Point up = new Point(p.X, p.Y - 1);
                    Point down = new Point(p.X, p.Y + 1);

                    if(left.X >= 0 && newBitmap.GetPixel(left.X, left.Y).ToArgb() != shape.LineColor.ToArgb()) {
                        if(newBitmap.GetPixel(left.X, left.Y).ToArgb() != shape.FillColor.ToArgb()) {
                            newBitmap.SetPixel(left.X, left.Y, shape.FillColor);
                            queue.Enqueue(left);
                        }
                    }

                    if(right.X <= newBitmap.Width - 1 && newBitmap.GetPixel(right.X, right.Y).ToArgb() != shape.LineColor.ToArgb()) {
                        if(newBitmap.GetPixel(right.X, right.Y).ToArgb() != shape.FillColor.ToArgb()) {
                            newBitmap.SetPixel(right.X, right.Y, shape.FillColor);
                            queue.Enqueue(right);
                        }
                    }

                    if(up.Y >= 0 && newBitmap.GetPixel(up.X, up.Y).ToArgb() != shape.LineColor.ToArgb()) {
                        if(newBitmap.GetPixel(up.X, up.Y).ToArgb() != shape.FillColor.ToArgb()) {
                            newBitmap.SetPixel(up.X, up.Y, shape.FillColor);
                            queue.Enqueue(up);
                        }
                    }

                    if(down.Y < newBitmap.Height - 1 && newBitmap.GetPixel(down.X, down.Y).ToArgb() != shape.LineColor.ToArgb()) {
                        if(newBitmap.GetPixel(down.X, down.Y).ToArgb() != shape.FillColor.ToArgb()) {
                            newBitmap.SetPixel(down.X, down.Y, shape.FillColor);
                            queue.Enqueue(down);
                        }
                    }
                }
            }
            return newBitmap;
        }

        void CohenSutherland(Point a, Point b, Shape shape, Bitmap oldBitmap) {
            bool accept = false, done = false;
            // warning: this will only work on rectangles. not convexii
            Point clipStart = clippingShape.Vertices[0];
            Size clipSize = new Size(Point.Subtract(clippingShape.Vertices[2], new Size(clipStart)));
            Rectangle clip = new Rectangle(clipStart, clipSize);
            byte outcode1 = ComputeOutcode(a, clip);
            byte outcode2 = ComputeOutcode(b, clip);

            do {
                if((outcode1 | outcode2) == 0) { //trivially rejected
                    accept = true;
                    done = true;
                } else if((outcode1 & outcode2) != 0) { //trivially accepted
                    accept = false;
                    done = true;
                } else { //subdivide
                    byte outcodeOut = (outcode1 != 0) ? outcode1 : outcode2;
                    Point p = new Point();
                    if((outcodeOut & TOP) != 0)
                        p = new Point(a.X + (b.X - a.X) * (clip.Top - a.Y) / (b.Y - a.Y), clip.Top);
                    else if((outcodeOut & BOTTOM) != 0)
                        p = new Point(a.X + (b.X - a.X) * (clip.Bottom - a.Y) / (b.Y - a.Y), clip.Bottom);
                    else if((outcodeOut & RIGHT) != 0)   // point is to the right of clip rectangle
                        p = new Point(clip.Right, a.Y + (b.Y - a.Y) * (clip.Right - a.X) / (b.X - a.X));
                    else if((outcodeOut & LEFT) != 0)    // point is to the left of clip rectangle
                        p = new Point(clip.Left, a.Y + (b.Y - a.Y) * (clip.Left - a.X) / (b.X - a.X));
                    if(outcodeOut == outcode1) {
                        a = p;
                        outcode1 = ComputeOutcode(a, clip);
                    } else {
                        b = p;
                        outcode2 = ComputeOutcode(b, clip);
                    }
                }
            } while(!done);
            if(accept) {
                shape.Vertices[0] = a;
                shape.Vertices[1] = b;
                DrawAccordingLine(a, b, shape, oldBitmap);
            }
        }

        byte ComputeOutcode(Point p, Rectangle clip) {
            byte outcode = 0;
            if(p.X > clip.Right)
                outcode |= RIGHT;
            else if(p.X < clip.Left)
                outcode |= LEFT;
            if(p.Y < clip.Top)
                outcode |= TOP;
            else if(p.Y > clip.Bottom)
                outcode |= BOTTOM;
            return outcode;
        }

        private void DrawAccordingLine(Point vA, Point vB, Shape shape, Bitmap oldBitmap) {
            // check if the points are located inside the bitmap:
            if(new Rectangle(new Point(0, 0), oldBitmap.Size).Contains(vA) && new Rectangle(new Point(0, 0), oldBitmap.Size).Contains(vB)) {

                if(shape.AntiAliased) {
                    ThickAntialiasedLine(vA, vB, shape.Thickness, oldBitmap, shape.LineColor);
                } else {
                    // if we're not doing antialiasing, then just use pixel copying
                    if(shape.MultiSampling == 1) {
                        ThickMidpointLine(vA, vB, shape.Thickness, oldBitmap, shape.LineColor);
                    } else {
                        // draw a thicker line on the bigger bitmap:
                        int scale = shape.MultiSampling;
                        ThickMultiSampledLine(vA, vB, scale, shape.Thickness, oldBitmap, shape.LineColor);
                    }
                }
            }
        }

        private void ThickMultiSampledLine(Point vA, Point vB, int scale, int thickness, Bitmap oldBitmap, Color color) {
            Bitmap bigBitmap = new Bitmap(oldBitmap, oldBitmap.Width * scale, oldBitmap.Height * scale);
            using(Graphics gfx = Graphics.FromImage(bigBitmap))
            using(SolidBrush brush = new SolidBrush(Color.White)) {
                gfx.FillRectangle(brush, 0, 0, bigBitmap.Width, bigBitmap.Height);
            }

            int thickerThickness = thickness + scale;
            Point thickStart = Point.Add(vA, new Size(vA));
            Point thickEnd = Point.Add(vB, new Size(vB));
            ThickMidpointLine(thickStart, thickEnd, thickerThickness, bigBitmap, color);

            Color[,] intensities = new Color[bigBitmap.Width, bigBitmap.Height];

            // count occurences of pixels in the bigger bitmap.
            for(int y = 0; y <= thickEnd.Y - thickStart.Y; y++) {
                for(int x = 0; x <= thickEnd.X - thickStart.X; x++) {

                    int imageX = x + thickStart.X;
                    int imageY = y + thickStart.Y;

                    int avgR = 0;
                    int avgG = 0;
                    int avgB = 0;
                    for(int i = 0; i < scale; i++) {
                        for(int j = 0; j < scale; j++) {
                            avgR += bigBitmap.GetPixel(imageX + i, imageY + j).R;
                            avgG += bigBitmap.GetPixel(imageX + i, imageY + j).G;
                            avgB += bigBitmap.GetPixel(imageX + i, imageY + j).B;
                        }
                    }
                    int newR = avgR / (scale * scale);
                    int newG = avgG / (scale * scale);
                    int newB = avgB / (scale * scale);
                    intensities[x, y] = Color.FromArgb(newR, newG, newB);
                }
            }

            for(int y = 0; y <= vB.Y - vA.Y; y++)
                for(int x = 0; x <= vB.X - vA.X; x++)
                    oldBitmap.SetPixel(vA.X + x, vA.Y + y, intensities[x, y]);

        }

        private void ThickMidpointLine(Point a, Point b, int thickness, Bitmap oldBitmap, Color color) {
            for(int i = 0; i < thickness; i++) {
                if(i % 2 == 0) {
                    a.Y -= i + 1;
                    b.Y -= i + 1;
                    DrawMidpointLine(a, b, oldBitmap, color);
                    a.Y += i + 1;
                    b.Y += i + 1;
                } else {
                    DrawMidpointLine(a, b, oldBitmap, color);
                    a.Y += 1;
                    b.Y += 1;
                }
            }
        }

        void MidpointCircle(int xCenter, int yCenter, int radius, Bitmap bitmap, Color color) {
            int dE = 3;
            int dSE = 5 - 2 * radius;
            int d = 1 - radius;
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
                // 8-fold symmetry
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
                for(int i = 0; i < thickness / 2 + 1; i++)
                    IntensifyPixel(x, y + i, thickness, two_dx_invDenom + two_v_dx * invDenom - i, bitmap, color);
                for(int i = 0; i < thickness / 2 + 1; i++)
                    IntensifyPixel(x, y - i, thickness, two_dx_invDenom - two_v_dx * invDenom - i, bitmap, color);

            }
        }

        void DrawMidpointLine(Point a, Point b, Bitmap bitmap, Color color) {

            int Bx = b.X;
            int Ax = a.X;
            int By = b.Y;
            int Ay = a.Y;

            int y, x, dy, dx, sx, sy;
            int decision, incE, incNE;

            dx = Bx - Ax;
            dy = By - Ay;

            sx = Math.Sign(dx);
            sy = Math.Sign(dy);

            dx = Math.Abs(dx);
            dy = Math.Abs(dy);

            if(dy > dx) {
                incE = 2 * dx;
                incNE = 2 * dx - 2 * dy;
                decision = 2 * dx - dy;

                x = Ax;
                y = Ay;
                do {
                    bitmap.SetPixel(x, y, color);
                    if(decision <= 0)
                        decision += incE;
                    else {
                        decision += incNE;
                        x += sx;
                    }
                    y += sy;
                } while(y != By);
            } else {
                incE = 2 * dy;
                incNE = 2 * dy - 2 * dx;
                decision = 2 * dy - dx;

                x = Ax;
                y = Ay;
                do {
                    bitmap.SetPixel(x, y, color);
                    if(decision <= 0)
                        decision += incE;
                    else {
                        decision += incNE;
                        y += sy;
                    }
                    x += sx;
                } while(x != Bx);
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
            shapes.Clear();
            clickedPoints.Clear();
            selectedShape = 0;
            colorDialog.Color = Color.Black;
            lineColorPickerButton.BackColor = colorDialog.Color;
            clippingShape = null;
            isUpToDate = false;
            canvas.Refresh();
        }

        private void cleanClipButton_Click(object sender, EventArgs e) {
            clippingShape = null;
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

        private void lineColorPickerButton_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog.ShowDialog();
            if(result == DialogResult.OK)
                lineColorPickerButton.BackColor = colorDialog.Color;
        }
        private void fillColorPickerButton_Click(object sender, EventArgs e) {
            DialogResult result = colorDialog.ShowDialog();
            if(result == DialogResult.OK)
                fillColorPickerButton.BackColor = colorDialog.Color;
        }

        private void refreshButton_Click(object sender, EventArgs e) {
            isUpToDate = false;
            canvas.Refresh();
        }
    }
}

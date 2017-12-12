using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransformationsInSpace
{
    public partial class formMain : Form
    {
        private House _house = new House();
        private List<Label> debug = new List<Label>();

        public formMain()
        {
            InitializeComponent();
            debug.Add(label2);
            debug.Add(label3);
            debug.Add(label4);
            debug.Add(label5);
            debug.Add(label6);
            debug.Add(label7);
            debug.Add(label8);
            debug.Add(label9);
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var grafics = e.Graphics;
            var pen = new Pen(Color.Black);

            System.Drawing.Point invisible;

            System.Drawing.Point[] pr;
            var points = _house.GetBox(50, panel1.Width / 2, panel1.Height / 2, out invisible, debug);

            var index = Array.IndexOf(points, invisible);
            label1.Text = index.ToString();

            var i = 0;
            foreach (var point in points)
            {
                debug[i].Text = String.Format("X: {0} Y: {1} {2}", point.X, point.Y, debug[i].Text);
                i++;
            }

            var planes = new List<List<int>>()
            {
                new List<int>() {0, 2, 6, 4},
                new List<int>() {4, 5, 7, 6},
                new List<int>() {2, 3, 7, 6},
                new List<int>() {0, 1, 3, 2},
                new List<int>() {0, 1, 5, 4},
                new List<int>() {1, 5, 7, 3}
            };

            foreach (var plane in planes)
            {
                var draw = ProjectionTool.GetPoints(points, plane.ToArray());
                //if (!draw.Contains(invisible))
                    grafics.DrawPolygon(pen, draw);
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            IntializaMoveBar(scrlbarMovementX, panel1);
            IntializaMoveBar(scrlbarMovementY, panel1);
            IntializaMoveBar(scrlbarMovementZ, panel1);

            InitializaRotateBar(scrlbarRotateX);
            InitializaRotateBar(scrlbarRotateY);
            InitializaRotateBar(scrlbarRotateZ);
        }

        private static void IntializaMoveBar(HScrollBar bar, Panel panel1)
        {
            bar.Minimum = -panel1.Width / 2;
            bar.Maximum = panel1.Width / 2;
            bar.Value = 0;
        }

        private static void InitializaRotateBar(HScrollBar bar)
        {
            bar.Minimum = 0;
            bar.Maximum = 360;
            bar.Value = 0;
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Move(_house, scrlbarMovementX.Value, 0);

            panel1.Invalidate();
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Move(_house, scrlbarMovementY.Value, 1);

            panel1.Invalidate();
        }

        private void hScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Move(_house, scrlbarMovementZ.Value, 2);

            panel1.Invalidate();
        }

        private void scrlbarRotateX_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Rotate(_house, scrlbarRotateX.Value, 0);

            panel1.Invalidate();
        }

        private void scrlbarRotateY_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Rotate(_house, scrlbarRotateY.Value, 1);

            panel1.Invalidate();
        }

        private void scrlbarRotateZ_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Rotate(_house, scrlbarRotateZ.Value, 2);

            panel1.Invalidate();
        }

        private void scrlbarScaling_ValueChanged(object sender, EventArgs e)
        {
            ProjectionTool.Scale(_house, scrlbarScaling.Value / 10.0);

            panel1.Invalidate();
        }
    }
}

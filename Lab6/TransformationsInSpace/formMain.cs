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

        public formMain()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var grafics = e.Graphics;
            var pen = new Pen(Color.Black);

            var points = _house.GetBox(50, panel1.Width / 2, panel1.Height / 2);

            grafics.DrawPolygon(pen, ProjectionTool.GetPoints(points, 0, 2, 6, 4));
            grafics.DrawPolygon(pen, ProjectionTool.GetPoints(points, 4, 5, 7, 6));
            grafics.DrawPolygon(pen, ProjectionTool.GetPoints(points, 0, 1, 5, 4));
            grafics.DrawPolygon(pen, ProjectionTool.GetPoints(points, 0, 1, 3, 2));
            grafics.DrawPolygon(pen, ProjectionTool.GetPoints(points, 2, 3, 7, 6));
            grafics.DrawPolygon(pen, ProjectionTool.GetPoints(points, 1, 5, 7, 3));
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            IntializaMoveBar(hScrollBar1, panel1);
            IntializaMoveBar(hScrollBar2, panel1);
            IntializaMoveBar(hScrollBar3, panel1);

            InitializaRotateBar(hScrollBar4);
            InitializaRotateBar(hScrollBar5);
            InitializaRotateBar(hScrollBar6);
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
            ProjectionTool.Move(_house, hScrollBar1.Value, 0);

            panel1.Invalidate();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            ProjectionTool.Move(_house, hScrollBar2.Value, 1);

            panel1.Invalidate();
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            ProjectionTool.Move(_house, hScrollBar3.Value, 2);

            panel1.Invalidate();
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            ProjectionTool.Rotate(_house, hScrollBar4.Value, 0);
            panel1.Invalidate();
        }

        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            ProjectionTool.Rotate(_house, hScrollBar5.Value, 1);
            panel1.Invalidate();
        }

        private void hScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            ProjectionTool.Rotate(_house, hScrollBar6.Value, 2);
            panel1.Invalidate();
        }

        private void hScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            ProjectionTool.Scale(_house, hScrollBar7.Value/10.0);
            panel1.Invalidate();
        }
    }
}

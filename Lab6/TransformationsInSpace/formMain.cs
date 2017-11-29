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
    }
}

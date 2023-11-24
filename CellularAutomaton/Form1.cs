using System.Numerics;

namespace CellularAutomaton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public Rendering Rendering { get; set; }
        private void pBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Rendering == null)
            {
                Rendering = new(pBox.Width / 4, pBox.Height / 2, 0, pBox.CreateGraphics(), new Pen(Color.Aqua));
                Rendering.Draw();
            }
            else
            {
                Rendering.AddCube(e.X, e.Y, 0);
            }
                    

        }
    }
}
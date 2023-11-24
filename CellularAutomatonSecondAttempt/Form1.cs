using System.Numerics;

namespace CellularAutomatonSecondAttempt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cube = new Cube3D(new Vector3(pBox.ClientSize.Width / 2, pBox.ClientSize.Height / 2, 0));
            graphics = pBox.CreateGraphics();
            penC = new Pen(Color.Black);
            penF = new Pen(Color.Green);

            GameField = new(new Vector3(pBox.ClientSize.Width - Cube3D.LengthEdge * 2, pBox.ClientSize.Height, 0));
        }



        public Cube3D cube;
        public Graphics graphics;
        public Pen penC;
        public Pen penF;

        public GameField GameField;

        private void buttonRun_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            GameField.Draw(graphics, penC);
        }

        private void pBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GameField.Update(graphics, penC, e.X, e.Y);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            GameField = new(new Vector3(pBox.ClientSize.Width - Cube3D.LengthEdge * 2, pBox.ClientSize.Height, 0));
            graphics.Clear(Color.White);
            GameField.Draw(graphics, penC);
        }
    }
}
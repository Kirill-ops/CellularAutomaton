using System.Numerics;

namespace CellularAutomatonSecondAttempt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Graphics = pBox.CreateGraphics();
            PenContour = new Pen(Color.Black);

            GameField = new(new Vector3(pBox.ClientSize.Width - Cube3D.LengthEdge * 2, pBox.ClientSize.Height - Cube3D.LengthEdge * 12, 0));
            
        }


        public Graphics Graphics;
        public Pen PenContour;

        public GameField GameField;

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Graphics.Clear(Color.White);
            GameField.Draw(Graphics, PenContour);
        }

        private void pBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GameField.Update(Graphics, PenContour, e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                GameField.Remove(Graphics, PenContour, e.X, e.Y);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            GameField = new(new Vector3(pBox.ClientSize.Width - Cube3D.LengthEdge * 2, pBox.ClientSize.Height, 0));
            Graphics.Clear(Color.White);
            GameField.Draw(Graphics, PenContour);
        }
    }
}
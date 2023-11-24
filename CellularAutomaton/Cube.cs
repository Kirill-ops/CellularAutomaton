using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton
{
    public enum State { Empty, Road, House, BLocked }

    public class Cube
    {
        public static int LengthEdge = 50;
        public List<List<Vector3>> Polygons;
        public State ActiveState { get; set; }



        public Cube(Vector3 baseVector, int lengthEdge = 50, State state = State.Empty)
        {
            LengthEdge = lengthEdge;
            Polygons = Polygon.CalculateCubePolygonsRotates(baseVector, LengthEdge);
            ActiveState = state;
        }

        public Cube(Cube cube, int lengthEdge = 50, State state = State.Empty)
        {
            LengthEdge = lengthEdge;
            Polygons = Polygon.CalculateCubePolygonsUpY(cube, LengthEdge);
            ActiveState = state;
        }

        private Pen _penContour = new Pen(Color.Black);

        public void Draw(Graphics graphics, Pen pen)
        {
            PointF[] Polygon2DArray = new PointF[Polygons[0].Count];
            int k = 0;
            foreach(var polygon in Polygons)
            {
                for (int i = 0; i < polygon.Count; i++)
                {
                    Polygon2DArray[i].X = polygon[i].X;
                    Polygon2DArray[i].Y = polygon[i].Y;
                }
                graphics.FillPolygon(pen.Brush, Polygon2DArray);
                k++;
                graphics.DrawPolygon(_penContour, Polygon2DArray);
            }
        }

        


    }
}

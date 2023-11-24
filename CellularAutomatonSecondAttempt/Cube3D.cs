using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomatonSecondAttempt
{

    public enum State { Grass, Foundation, House }
    public class Cube3D
    {
        public List<List<Vector3>> Polygons { get; set; }
        public Vector3 BaseVector { get; set; }
        public static int LengthEdge = 40;
        public List<List<Vector3>> BufferPolygons { get; set; }

        public State ActiveState { get; set; }


        public Cube3D() 
        {
            BaseVector = new(0, 0, 0);
            ActiveState = State.Grass;
            SetPolygons();
        }

        public Cube3D(Vector3 baseVector, State state = State.Grass, int lengthEdge = 50)
        {
            BaseVector = baseVector;
            SetPolygons();
            ActiveState = state;
        }




        public void SetPolygons()
        {
            Polygons = new()
            {
                new()
                {
                    BaseVector,
                    BaseVector + new Vector3(LengthEdge, 0, 0),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, 0),
                    BaseVector + new Vector3(0, LengthEdge, 0),
                },

                new()
                {
                    BaseVector,
                    BaseVector + new Vector3(0, LengthEdge, 0),
                    BaseVector + new Vector3(0, LengthEdge, LengthEdge),
                    BaseVector + new Vector3(0, 0, LengthEdge),
                },

                new()
                {
                    BaseVector,
                    BaseVector + new Vector3(0, 0, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, 0, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, 0, 0)
                },


                new()
                {
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(LengthEdge, 0, 0),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(LengthEdge, LengthEdge, 0),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(0, LengthEdge, 0),
                },

                new()
                {
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(0, LengthEdge, 0),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(0, LengthEdge, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(0, 0, LengthEdge),
                },

                new()
                {
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(0, 0, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(LengthEdge, 0, LengthEdge),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, LengthEdge) - new Vector3(LengthEdge, 0, 0)
                }
            };
        }

        private void SetBufferPolygons()
        {
            BufferPolygons = new();
            for (int i = 0; i < Polygons.Count; i++)
            {
                BufferPolygons.Add(new());
                for (int j = 0; j < Polygons[i].Count; j++)
                    BufferPolygons[i].Add(new Vector3(Polygons[i][j].X, Polygons[i][j].Y, Polygons[i][j].Z));
            }

        }

        private bool _flag = true;
        public void DrawCube(Graphics graphics, Pen penContour, Pen penFill)
        {
            if (_flag)
            {
                RotateX(50);
                RotateY(-40);
                _flag = false;
            }
            PointF[] Polygon2DArray = new PointF[Polygons[0].Count];
            foreach (var polygon in Polygons)
            {
                for (int i = 0; i < polygon.Count; i++)
                {
                    Polygon2DArray[i].X = polygon[i].X;
                    Polygon2DArray[i].Y = polygon[i].Y;
                }
                graphics.FillPolygon(penFill.Brush, Polygon2DArray);
                graphics.DrawPolygon(penContour, Polygon2DArray);
            }
        }

        private bool _flagX = true;
        private bool _flagY = true;

        public void RotateX(double angel)
        {
            if (_flagY)
            {
                _flagY = false;
                _flagX = true;
                SetBufferPolygons();
            }
            for (int i = 0; i < Polygons.Count; i++)
            {
                for (int j = 0; j < Polygons[i].Count; j++)
                {
                    Polygons[i][j] = RotateX(BufferPolygons[i][j], angel);
                }
            }
        }



        public void RotateY(double angel)
        {
            if (_flagX)
            {
                _flagY = true;
                _flagX = false;
                SetBufferPolygons();
            }
            for (int i = 0; i < Polygons.Count; i++)
            {
                for (int j = 0; j < Polygons[i].Count; j++)
                {
                    Polygons[i][j] = RotateY(BufferPolygons[i][j], angel);
                }
            }
        }

        private Vector3 RotateX(Vector3 vector, double Angel)
        {
            double radians = Angel * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            vector.Y = (int)(vector.Y * cos + vector.Z * (-sin));
            vector.Z = (int)(vector.Y * sin + vector.Z * cos);
            return new Vector3(vector.X, vector.Y, vector.Z);
        }


        private Vector3 RotateY(Vector3 vector, double Angel)
        {
            double radians = Angel * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            vector.X = (int)(vector.X * cos + vector.Z * sin);
            vector.Z = (int)(vector.X * (-sin) + vector.Z * cos);
            return new Vector3(vector.X, vector.Y, vector.Z);
        }


    }
}

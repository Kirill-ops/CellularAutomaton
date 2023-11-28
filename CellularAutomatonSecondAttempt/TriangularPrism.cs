using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomatonSecondAttempt
{
    internal class TriangularPrism : IFigure3D
    {

        public List<List<Vector3>> Polygons { get; set; } // полигоны куба (квадрат)
        public Vector3 BaseVector { get; set; }           // базовый вектор - относительно него задаются остальные вершины куба. Вершина 0

        //        0---------------1
        //       /|              /|
        //      / |             / |
        //     /  |            /  |
        //    4---------------5   |
        //    |   |           |   |
        //    |   3-----------|---2
        //    |  /            |  /
        //    |/              | /
        //    7---------------6

        public List<List<Vector3>> BufferPolygons { get; set; }  // буфер полигонов
        public static int LengthEdge = 40; // длина ребра


        public State ActiveState { get; set; } // текущее состояние куба


        public TriangularPrism()
        {
            BaseVector = new(0, 0, 0);
            ActiveState = State.Grass;
            SetPolygonsDefault();
        }

        public TriangularPrism(Vector3 baseVector, State state = State.Grass, int lengthEdge = 50)
        {
            BaseVector = baseVector;
            SetPolygonsDefault();
            ActiveState = state;
        }

        // задать Polygons
        public void SetPolygonsDefault()
        {
            Polygons = new()
            {

                // левый
                new()
                {
                    BaseVector,
                    BaseVector + new Vector3(LengthEdge / 2, 0, LengthEdge / 2),
                    BaseVector + new Vector3(LengthEdge / 2, LengthEdge,  LengthEdge/ 2),
                    BaseVector + new Vector3(0, LengthEdge, 0),
                },

                // правый
                new()
                {
                    BaseVector + new Vector3(LengthEdge / 2, 0, LengthEdge / 2),
                    BaseVector + new Vector3(LengthEdge / 2, LengthEdge,  LengthEdge/ 2),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, 0),
                    BaseVector + new Vector3(LengthEdge, 0, 0),
                },

                // передний
                new()
                {
                    BaseVector + new Vector3(0, LengthEdge, 0),
                    BaseVector + new Vector3(LengthEdge / 2, LengthEdge,  LengthEdge/ 2),
                    BaseVector + new Vector3(LengthEdge, LengthEdge, 0),
                },

            };
            RotateX(50);
            RotateY(-40);
        }

        // задать BufferPolygons
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

        // отрисовка куба
        public void DrawFigure(Graphics graphics, Pen penContour, Pen penFill)
        {
            
            foreach (var polygon in Polygons)
            {
                PointF[] Polygon2DArray = new PointF[polygon.Count];
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

        // поворот Polygons по оси X на угол angel
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


        // поворот Polygons по оси Y на угол angel
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

        // поврот вектора vector по оси X на угол Angel
        private Vector3 RotateX(Vector3 vector, double Angel)
        {
            double radians = Angel * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            vector.Y = (int)(vector.Y * cos + vector.Z * (-sin));
            vector.Z = (int)(vector.Y * sin + vector.Z * cos);
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        // поврот вектора vector по оси Y на угол Angel
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

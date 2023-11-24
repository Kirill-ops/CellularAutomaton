using NetTopologySuite.Mathematics;
using NetTopologySuite.Triangulate.QuadEdge;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace CellularAutomaton
{
    public class Polygon
    {

        public static Vector3 RotateX(Vector3 vector, double Angel = 40)
        {
            double radians = Angel * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            vector.Y = (int)(vector.Y * cos + vector.Z * (-sin));
            vector.Z = (int)(vector.Y * sin + vector.Z * cos);
            return new Vector3(vector.X, vector.Y, vector.Z);
        }


        public static Vector3 RotateY(Vector3 vector, double Angel = -30)
        {
            double radians = Angel * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            vector.X = (int)(vector.X * cos + vector.Z * sin);
            vector.Z = (int)(vector.X * (-sin) + vector.Z * cos);
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static List<List<Vector3>> CalculateCubePolygonsUpY(Cube cube, int edgeLength)
        {
            var polygons = new List<List<Vector3>>
            {
                new(),
                new(),
                new(),
            };

            var deltaVector = RotateY(RotateX(new Vector3(0, 50, 0)));
            deltaVector.X += 18;
            deltaVector.Y -= 6;
            for (int i = 0; i < cube.Polygons.Count; i++)
                for (int j = 0; j < cube.Polygons[i].Count; j++)
                    polygons[i].Add(cube.Polygons[i][j] - deltaVector);
            return polygons;
        }

        public static List<List<Vector3>> CalculateCubePolygonsRotates(Vector3 vertex, int edgeLength)
        {
            int ugolX = 40;
            int ugolY = -30;
            var polygons = new List<List<Vector3>>
            {
                new() 
                {
                    RotateX(vertex, ugolX),
                    RotateX(vertex - new Vector3(edgeLength, 0, 0), ugolX),
                    RotateX(vertex - new Vector3(edgeLength, edgeLength, 0), ugolX),
                    RotateX(vertex - new Vector3(0, edgeLength, 0), ugolX), 
                },

                new() 
                {
                    RotateX(vertex, ugolX),
                    RotateX(vertex - new Vector3(0, edgeLength, 0), ugolX), 
                    RotateX(vertex - new Vector3(0, edgeLength, edgeLength), ugolX),
                    RotateX(vertex - new Vector3(0, 0, edgeLength), ugolX),
                },

                new()
                {
                    RotateX(vertex, ugolX), 
                    RotateX(vertex - new Vector3(0, 0, edgeLength), ugolX), 
                    RotateX(vertex - new Vector3(edgeLength, 0, edgeLength), ugolX), 
                    RotateX(vertex - new Vector3(edgeLength, 0, 0), ugolX)
                }
            };


            for (int i = 0; i < polygons.Count; i++)
                for (int j = 0; j < polygons[i].Count; j++)
                    polygons[i][j] = RotateY(polygons[i][j], ugolY);

            return polygons;
        }

    }
}

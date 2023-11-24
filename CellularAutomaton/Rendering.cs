using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton
{
    public class Rendering
    {

        public List<KeyValuePair<(float X, float Y), Cube>> GameField { get; set; }
        public Graphics Graphics { get; set; }
        public Pen Pen { get; set; }

        public Vector3 BaseVector { get; set; }

        public Rendering() 
        {
            BaseVector = new Vector3();
            GameField = new();
        }

        public Rendering(int x, int y, int z, Graphics graphics, Pen pen)
        {
            BaseVector = new Vector3(x, y, z);

            GameField = new();
            DefaultGameField();
            
            Graphics = graphics;
            Pen = pen;
        }

        private void DefaultGameField()
        {
            int index = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var cube = new Cube(new Vector3(BaseVector.X + i * Cube.LengthEdge, BaseVector.Y + j * Cube.LengthEdge, 0));
                    GameField.Add(new ((cube.Polygons[0][0].X, cube.Polygons[0][0].Y), cube));
                    index++;
                }
            }
        }

        public void Draw()
        {
            foreach (var field in GameField)
                field.Value.Draw(Graphics, Pen);
        }

        public KeyValuePair<(float X, float Y), Cube> Min(KeyValuePair<(float X, float Y), Cube> one, KeyValuePair<(float X, float Y), Cube> two, int x)
        {
            return one;
        }


        public void AddCube(int x, int y, int z)
        {

            var keyValue = GameField.FirstOrDefault(e => e.Key.Item1 - x >= 0 && e.Key.Item2 - y >= 0);
            if (GameField.Contains(keyValue))
            {
                var key = keyValue.Key;
                int index = GameField.IndexOf(keyValue);
                switch (GameField[index].Value.ActiveState)
                {
                    case State.Empty:
                        GameField[index].Value.ActiveState = State.BLocked;
                        var roadCube = new Cube(GameField[index].Value, state: State.Road);
                        GameField.Insert(index, new((roadCube.Polygons[0][0].X, roadCube.Polygons[0][0].Y), roadCube));
                        roadCube.Draw(Graphics, new Pen(Color.Silver));
                        break;
                    case State.Road:
                        var housCube = new Cube(GameField[index].Value, state: State.House);
                        GameField.Insert(index, new((housCube.Polygons[0][0].X, housCube.Polygons[0][0].Y), housCube));
                        housCube.Draw(Graphics, new Pen(Color.Brown));
                        break;
                    case State.House:
                        var housCubeUp = new Cube(GameField[index].Value, state: State.House);
                        GameField.Insert(index, new((housCubeUp.Polygons[0][0].X, housCubeUp.Polygons[0][0].Y), housCubeUp));
                        housCubeUp.Draw(Graphics, new Pen(Color.Brown));
                        break;
                }
            }
        }

    }
}

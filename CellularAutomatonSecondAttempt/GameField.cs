using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellularAutomatonSecondAttempt
{
    public class GameField
    {


        //public List<List<Rectangle>> Rectangles;
        public Dictionary<(int, int), Rectangle> Rectangles;
        public int StartFildRectanglesX;
        public int StartFildRectanglesY;

        public List<List<List<IFigure3D>>> Cubs;
        public int Count = 10;


        public GameField(Vector3 baseVector) 
        {
            StartFildRectanglesX = Cube3D.LengthEdge;
            StartFildRectanglesY = Cube3D.LengthEdge; 
            SetRectagles();
            SetCubs(baseVector);
        }


        public void SetRectagles()
        {
            Rectangles = new();
            for (int i = 0; i < Count; i++ )
            {
                //Rectangles.Add(new List<Rectangle>());
                for (int j = 0; j < Count; j++)
                {
                    Rectangles.Add((i, j), new Rectangle(StartFildRectanglesX + i * Cube3D.LengthEdge, 
                        StartFildRectanglesY + j * Cube3D.LengthEdge, Cube3D.LengthEdge, Cube3D.LengthEdge));
                }
            }
        }

        public void SetCubs(Vector3 baseVector)
        {
            Cubs = new();
            for (int i = 0; i < Count; i++)
            {
                Cubs.Add(new());
                for (int j = 0; j < Count; j++)
                {
                    Cubs[i].Add(new());
                    Cubs[i][j].Add(new Cube3D(new Vector3(baseVector.X + i * Cube3D.LengthEdge, baseVector.Y + j * Cube3D.LengthEdge, 0)));
                }
            }
        }

        public void Draw(Graphics graphics, Pen penContour)
        {
            
            for (int i = 0; i < Cubs.Count; i++)
            {
                for (int j = 0; j < Cubs[i].Count; j++)
                {
                    for (int k = 0; k < Cubs[i][j].Count; k++)
                    {
                        switch(Cubs[i][j][k].ActiveState)
                        {
                            case State.Grass:
                                Cubs[i][j][k].DrawFigure(graphics, penContour, new Pen(Color.GreenYellow));
                                break;
                            case State.Foundation:
                                Cubs[i][j][k].DrawFigure(graphics, penContour, new Pen(Color.Gray));
                                break;
                            case State.FirstFloor:
                                Cubs[i][j][k].DrawFigure(graphics, penContour, new Pen(Color.FromArgb(210, 176, 76)));
                                break;
                            case State.SecondFloor:
                                Cubs[i][j][k].DrawFigure(graphics, penContour, new Pen(Color.FromArgb(51, 51, 51)));
                                break;
                            case State.Roof:
                                Cubs[i][j][k].DrawFigure(graphics, penContour, new Pen(Color.FromArgb(200, 80, 0)));
                                break;
                            /*case State.Sky:
                                var brushSky = new SolidBrush(Color.FromArgb(128, 153, 255, 251));
                                Cubs[i][j][k].DrawCube(graphics, penContour, new Pen(Color.FromArgb(128, 153, 255, 251)));
                                break;*/
                        }
                    }
                    var cube = Cubs[i][j].LastOrDefault();
                    switch (cube.ActiveState)
                    {
                        case State.Grass:
                            graphics.FillRectangle(Brushes.GreenYellow, Rectangles[(i, j)]);
                            graphics.DrawRectangle(penContour, Rectangles[(i, j)]);
                            break;
                        case State.Foundation:
                            graphics.FillRectangle(Brushes.Gray, Rectangles[(i, j)]);
                            graphics.DrawRectangle(penContour, Rectangles[(i, j)]);
                            break;
                        case State.FirstFloor:
                            var brushFirstFloor = new SolidBrush(Color.FromArgb(210, 176, 76));
                            graphics.FillRectangle(brushFirstFloor, Rectangles[(i, j)]);
                            graphics.DrawRectangle(penContour, Rectangles[(i, j)]);
                            break;
                        case State.SecondFloor:
                            var brushSecondFloor = new SolidBrush(Color.FromArgb(51, 51, 51));
                            graphics.FillRectangle(brushSecondFloor, Rectangles[(i, j)]);
                            graphics.DrawRectangle(penContour, Rectangles[(i, j)]);
                            break;
                        case State.Roof:
                            var brushRoof = new SolidBrush(Color.FromArgb(200, 80, 0));
                            graphics.FillRectangle(brushRoof, Rectangles[(i, j)]);
                            graphics.DrawRectangle(penContour, Rectangles[(i, j)]);
                            break;
                        /*case State.Sky:
                            var brushSky = new SolidBrush(Color.FromArgb(153, 255, 251));
                            graphics.FillRectangle(brushSky, Rectangles[(i, j)]);
                            graphics.DrawRectangle(penContour, Rectangles[(i, j)]);
                            break;*/
                    }
                    
                }
            }

        }


        public void Update(Graphics graphics, Pen penContour, int x, int y)
        {
            x -= x % Cube3D.LengthEdge;
            y -= y % Cube3D.LengthEdge;
            var rectangle = new Rectangle(x, y, Cube3D.LengthEdge, Cube3D.LengthEdge);
            if (Rectangles.ContainsValue(rectangle))
            {
                var key = Rectangles.FirstOrDefault(e => e.Value == rectangle).Key;
                var cube = Cubs[key.Item1][key.Item2].LastOrDefault();
                switch(cube.ActiveState)
                {
                    case State.Grass:
                        Cubs[key.Item1][key.Item2].Add(new Cube3D(new Vector3(cube.BaseVector.X, cube.BaseVector.Y, cube.BaseVector.Z + Cube3D.LengthEdge), State.Foundation));
                        Draw(graphics, penContour);
                        break;
                    case State.Foundation:
                        Cubs[key.Item1][key.Item2].Add(new Cube3D(new Vector3(cube.BaseVector.X, cube.BaseVector.Y, cube.BaseVector.Z + Cube3D.LengthEdge), State.FirstFloor));
                        Draw(graphics, penContour);
                        break;
                    case State.FirstFloor:
                        Cubs[key.Item1][key.Item2].Add(new Cube3D(new Vector3(cube.BaseVector.X, cube.BaseVector.Y, cube.BaseVector.Z + Cube3D.LengthEdge), State.SecondFloor));
                        Draw(graphics, penContour);
                        break;
                    case State.SecondFloor:
                        Cubs[key.Item1][key.Item2].Add(new TriangularPrism(new Vector3(cube.BaseVector.X, cube.BaseVector.Y, cube.BaseVector.Z + Cube3D.LengthEdge), State.Roof));
                        Draw(graphics, penContour);
                        break;
                    /*case State.Roof:
                        Cubs[key.Item1][key.Item2].Add(new Cube3D(new Vector3(cube.BaseVector.X, cube.BaseVector.Y, cube.BaseVector.Z + Cube3D.LengthEdge), State.Sky));
                        break;
                    case State.Sky:
                        Cubs[key.Item1][key.Item2].Add(new Cube3D(new Vector3(cube.BaseVector.X, cube.BaseVector.Y, cube.BaseVector.Z + Cube3D.LengthEdge), State.Sky));
                        break;*/
                }

            }
        }

        public void Remove(Graphics graphics, Pen penContour, int x, int y)
        {
            x -= x % Cube3D.LengthEdge;
            y -= y % Cube3D.LengthEdge;
            var rectangle = new Rectangle(x, y, Cube3D.LengthEdge, Cube3D.LengthEdge);
            if (Rectangles.ContainsValue(rectangle))
            {
                var key = Rectangles.FirstOrDefault(e => e.Value == rectangle).Key;
                if (Cubs[key.Item1][key.Item2].Count != 1)
                {
                    Cubs[key.Item1][key.Item2].Remove(Cubs[key.Item1][key.Item2][Cubs[key.Item1][key.Item2].Count - 1]);
                    Draw(graphics, penContour);
                }
            }
        }

    }
}

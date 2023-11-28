using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomatonSecondAttempt
{
    public interface IFigure3D
    {
        public void DrawFigure(Graphics graphics, Pen penContour, Pen penFill);
        public State ActiveState { get; set; } // текущее состояние куба
        public Vector3 BaseVector { get; set; }
    }
}

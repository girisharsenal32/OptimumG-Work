using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;
using devDept.Eyeshot.Entities;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// Class of Lines which represent the Projection of a Parent line in the 3 Standard Planes
    /// </summary>
    public class ChildLine_ViewLines
    {
        /// <summary>
        /// Front View 2D Line
        /// </summary>
        public ChildLine_VariableLine FrontView { get; set; }
        /// <summary>
        /// Side View 2D Line
        /// </summary>
        public ChildLine_VariableLine SideView { get; set; }
        /// <summary>
        /// Top View 2D Line
        /// </summary>
        public ChildLine_VariableLine TopView { get; set; }

        /// <summary>
        /// Constructor to create the Projection of the Parent 2D Lines
        /// </summary>
        /// <param name="_startXY"></param>
        /// <param name="_endXY"></param>
        /// <param name="_startYZ"></param>
        /// <param name="_endYZ"></param>
        /// <param name="_startXZ"></param>
        /// <param name="_endXZ"></param>
        public ChildLine_ViewLines(Point2D _startXY, Point2D _endXY, Point2D _startYZ, Point2D _endYZ, Point2D _startXZ, Point2D _endXZ)
        {
            FrontView = new ChildLine_VariableLine(_startXY, _endXY, Plane.XY);

            SideView = new ChildLine_VariableLine(_startYZ, _endYZ, Plane.ZY);

            TopView = new ChildLine_VariableLine(_startXZ, _endXZ, Plane.XZ);
        }


        public void AddLineToDeltaLine(Point2D _startXY, Point2D _endXY, Point2D _startYZ, Point2D _endYZ, Point2D _startXZ, Point2D _endXZ)
        {
            FrontView.DeltaLine.Add(new Line(Plane.XY, _startXY.X, _startXY.Y, _endXY.X, _endXY.Y));

            SideView.DeltaLine.Add(new Line(Plane.ZY, _startYZ.X, _startYZ.Y, _endYZ.X, _endYZ.Y));

            TopView.DeltaLine.Add(new Line(Plane.XZ, _startXZ.X, _startXZ.Y, _endXZ.X, _endXZ.Y));


        }

        public void AssignFinalState(Point2D _startXY, Point2D _endXY, Point2D _startYZ, Point2D _endYZ, Point2D _startXZ, Point2D _endXZ, int _indexOfLine)
        {
            FrontView.AssignFinalState_2DLine(_startXY, _endXY, Plane.XY, _indexOfLine);

            SideView.AssignFinalState_2DLine(_startYZ, _endYZ, Plane.ZY, _indexOfLine);

            TopView.AssignFinalState_2DLine(_startXZ, _endXZ, Plane.XZ, _indexOfLine);
        }

    }
}

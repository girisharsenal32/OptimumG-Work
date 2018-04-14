using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// Class of Lines which have a initial and final state. The Final state represents the line after a set of Transformations have been applied to it
    /// </summary>
    public class ChildLine_VariableLine
    {
        ///// <summary>
        ///// Initial line before any transformations
        ///// </summary>
        //public Line Init { get; set; }
        ///// <summary>
        ///// Final line after transformations are applied to it 
        ///// </summary>
        //public Line Fin { get; set; }

        /// <summary>
        /// List of Lines. The <see cref="List{T}[0]"/> represent the Initial State of the Line with no transformations or changes. All the remaining indices correspond to a changed state of the line as the tranformations are applied
        /// </summary>
        public List<Line> DeltaLine = new List<Line>();

        /// <summary>
        /// Constructor to construct the initial line. The initial and final are the same when the Line is constructed
        /// </summary>
        /// <param name="_start">Start Point of the Line</param>
        /// <param name="_end">End point of the Line</param>
        public ChildLine_VariableLine(Point3D _start, Point3D _end)
        {
            //Init = new Line(_start, _end);

            //Fin = new Line(_start, _end);

            DeltaLine.Add(new Line(_start.X, _start.Y, _start.Z, _end.X, _end.Y, _end.Z));
            //DeltaLine.Add(new Line(_start.X, _start.Y, _start.Z, _end.X, _end.Y, _end.Z));
        }

        /// <summary>
        /// Overloaded Constructor to Initialize a 2D line. The initial and final are the same when the Line is constructed
        /// This constructor is used primarily to initial the lines of type <see cref="ChildLine_ViewLines"/>
        /// </summary>
        /// <param name="_start2D"></param>
        /// <param name="_end2D"></param>
        /// <param name="_sketchPlane">Front, Side or Top View Plane on which this Projection Line is to be drawn</param>
        public ChildLine_VariableLine(Point2D _start2D, Point2D _end2D, Plane _sketchPlane)
        {
            ///<summary>Plotting 3D Line which is a projection of the Parent Line in the Plane of the users choice passed as a parameter</summary>
            DeltaLine.Add(new Line(_sketchPlane,_start2D.X, _start2D.Y, _end2D.X, _end2D.Y));
        }

        public ChildLine_VariableLine()
        {
        }

        /// <summary>
        /// Method to Assign the Final line with its newly calculate Start and End Points
        /// </summary>
        /// <param name="_start">3D Start Point</param>
        /// <param name="_end">3D End Point</param>
        public void AssignFinalState_3DLine(Point3D _start, Point3D _end, int _indexOfLine)
        {
            //Fin = new Line(_start, _end);
            DeltaLine[_indexOfLine] = new Line(_start.X, _start.Y, _start.Z, _end.X, _end.Y, _end.Z);
        }

        /// <summary>
        /// Method to Assign the Final 2D Line with its newly calculated Start and End Points
        /// </summary>
        /// <param name="_start2D">2D Start Point</param>
        /// <param name="_end2D">2D End Point</param>
        public void AssignFinalState_2DLine(Point2D _start2D, Point2D _end2D, Plane _sketchPlane, int _indexOfLine)
        {
            DeltaLine[_indexOfLine] = new Line(_sketchPlane, _start2D.X, _start2D.Y, _end2D.X, _end2D.Y);
        }


    }
}

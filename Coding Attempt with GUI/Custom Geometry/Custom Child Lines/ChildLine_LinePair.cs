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
    /// Class of Lines which contain a pair of 3 lines which are drawing from the start, mid and end point of another Line, which is usually perpendicular to the Pair of Lines
    /// </summary>
    public class ChildLine_LinePair
    {
        /// <summary>
        /// Perpendicular to the parent line and starting from its Frontmost, Leftmost or Topmost point
        /// </summary>
        public Line Fore { get; set; }
        /// <summary>
        /// Perpendicular to the parent line and starting from its Mid point
        /// </summary>
        public Line Mid { get; set; }
        /// <summary>
        /// Perpendicular to the parent line and starting from its Rearmost, Righttmost or Bottommost point
        /// </summary>
        public Line Aft { get; set; }

        /// <summary>
        /// Constructor to construct the three lines. All the three are pass through the mid point of the parent line in the start
        /// </summary>
        /// <param name="_Start">Start Point of the Line</param>
        /// <param name="_End">End Point of the Line</param>
        public ChildLine_LinePair(Point3D _Start, Point3D _End)
        {
            Fore = new Line(_Start.X, _Start.Y, _Start.Z, _End.X, _End.Y, _End.Z);

            Mid = new Line(_Start.X, _Start.Y, _Start.Z, _End.X, _End.Y, _End.Z);

            Aft = new Line(_Start.X, _Start.Y, _Start.Z, _End.X, _End.Y, _End.Z);
        }
    }
}

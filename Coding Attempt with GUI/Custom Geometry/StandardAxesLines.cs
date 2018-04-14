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
    /// Pair of 3 lines which originate from a PARTICULAR Point of the user's choice and are along the three axis directions of the local or world coordinate system
    /// Could also be used as a resolution of a Line along the 3 Standar Axes
    /// </summary>
    public class StandardAxesLines
    {
        /// <summary>
        /// Axis line along the Longitudinal 
        /// </summary>
        public Line Longitudinal { get; set; }
        /// <summary>
        /// Axis line along the Lateral 
        /// </summary>
        public Line Lateral { get; set; }
        /// <summary>
        /// Axis line along the Vertical 
        /// </summary>
        public Line Vertical { get; set; }

        /// <summary>
        /// Constructor to construct each of the 
        /// </summary>
        /// <param name="_sLong">Start Point of the Longitudinal Line</param>
        /// <param name="_eLong">End Point of the Longitudinal Line</param>
        /// <param name="_sLat">Start Point of the Lateral Line</param>
        /// <param name="_eLat">End Point of the Lateral Line</param>
        /// <param name="_sVert">Start Point of the Vertical Line</param>
        /// <param name="_eVert">End Point of the Vertical Line</param>
        public StandardAxesLines(Point3D _sLong, Point3D _eLong, Point3D _sLat, Point3D _eLat, Point3D _sVert, Point3D _eVert)
        {
            Longitudinal = new Line(_sLong.X, _sLong.Y, _sLong.Z, _eLong.X, _eLong.Y, _eLong.Z);

            Lateral = new Line(_sLat.X, _sLat.Y, _sLat.Z, _eLat.X, _eLat.Y, _eLat.Z);

            Vertical = new Line(_sVert.X, _sVert.Y, _sVert.Z, _eVert.X, _eVert.Y, _eVert.Z);
        }
    }
}

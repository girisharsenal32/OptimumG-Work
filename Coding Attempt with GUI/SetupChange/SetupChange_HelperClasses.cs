using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public class SetupChange_HelperClasses
    {
    }

    /// <summary>
    /// <para>Class to house the Coordinates of the Pick-Up Points which were Optimized</para>
    /// <para>---IMPORTANT--- These are NOT the Adjusters of the <see cref="SetupChange"/> Optmization</para>
    /// </summary>
    public class SetupChange_OptimizedCoordinate
    {
        public string PointName;

        public Point3D OptimizedCoordinates;

        public Point3D NominalCoordinates;

        public Point3D UpperCoordinateLimit;

        public Point3D LowerCoordinateLimit;

        public double UpperLinkLengthLimit_Front;

        public double LowerLinkengthLimit_Front;

        public double UpperLinkLengthLimit_Rear;

        public double LowerLinkLengthLimit_Rear;

        public SetupChange_OptimizedCoordinate(Point3D _nominalCoord, Point3D _upperLimit, Point3D _lowerLimit/*, int _bitSize*/)
        {
            NominalCoordinates = _nominalCoord;

            OptimizedCoordinates = NominalCoordinates.Clone() as Point3D;

            UpperCoordinateLimit = _upperLimit;

            LowerCoordinateLimit = _lowerLimit;
        }
    }

    /// <summary>
    /// Class which house all the relevant information pertaining to the ADJUSTERS of the Optimization for <see cref="SetupChange"/> 
    /// </summary>
    public class SetupChange_AdjToolParams
    {
        public string ParamName { get; set; }

        public double Nominal { get; set; }

        public double Uppwer { get; set; }

        public double Lower { get; set; }

        public int BitSize { get; set; }

        public double OptimizedIteration { get; set; }

        public SetupChange_AdjToolParams() { }

        public SetupChange_AdjToolParams(string _paramName, double _nominal, double _upper, double _lower, int _bitSiz)
        {
            ParamName = _paramName;

            Nominal = _nominal;

            Uppwer = _upper;

            Lower = _lower;

            BitSize = _bitSiz;
        }
    }

}

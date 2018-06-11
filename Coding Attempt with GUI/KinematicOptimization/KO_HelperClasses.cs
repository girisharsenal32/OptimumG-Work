using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;


namespace Coding_Attempt_with_GUI
{
    public class KO_HelperClasses
    {

    }

    /// <summary>
    /// Enum of <see cref="SuspensionParameter"/> for which the Suspension Coordinates would be Optimized for
    /// </summary>
    public enum SuspensionParameter
    {
        FrontRollCenter,
        RearRollCenter,
        LeftPitchCenter,
        RightPitchCenter,
        CamberVariation,
        BumpSteer,
        Ackermann,
        SpringDeflection
    };

    /// <summary>
    /// Enum to determine whether the user is requesting for a Static <see cref="SuspensionParameter"/> or a variation of the <see cref="SuspensionParameter"/>
    /// </summary>
    public enum SuspensionParameterMode
    {
        Static,
        Variation
    };

    /// <summary>
    /// <para>Class which stores information regarding the <see cref="Point3D'"/> Adjusters of the <see cref="KinematicOptimization"/></para>
    /// <para>These are the Pick-Up Points of the Suspension</para>
    /// </summary>
    public class KO_AdjToolParams
    {
        public string PointName { get; set; }

        /// <summary>
        /// Coordinates given by the Optimizer Algorithm
        /// </summary>
        public Point3D OptimizedCoordinates { get; set; }

        /// <summary>
        /// Nominal Coordinates of the Adjuster
        /// </summary>
        public Point3D NominalCoordinates { get; set; }

        /// <summary>
        /// Upper Limit of Adjuster's Coordinates
        /// </summary>
        public Point3D UpperCoordinateLimit { get; set; }

        /// <summary>
        /// Lower Limit of the Adjuster's Coordinates
        /// </summary>
        public Point3D LowerCoordinateLimit { get; set; }

        /// <summary>
        /// Bit size of the Adjuster which needs to be passed to Optimizer
        /// </summary>
        public int BitSize { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_nominalCoord">Nominal Coordinates of the Adjuster</param>
        /// <param name="_upperLimit">Upper Limit of Adjuster's Coordinates</param>
        /// <param name="_lowerLimit">Lower Limit of the Adjuster's Coordinates</param>
        /// <param name="_bitSize">Bit size of the Adjuster which needs to be passed to Optimizer</param>
        public KO_AdjToolParams(Point3D _nominalCoord, Point3D _upperLimit, Point3D _lowerLimit, int _bitSize)
        {
            NominalCoordinates = _nominalCoord;

            OptimizedCoordinates = NominalCoordinates.Clone() as Point3D;

            UpperCoordinateLimit = _upperLimit;

            LowerCoordinateLimit = _lowerLimit;

            BitSize = _bitSize;
        }






    }


}


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
    /// This Class houses the Components assembled onto the Vehicle which connect 2 corners\
    /// This would include the Anti-Roll Bar
    /// </summary>
    public class KO_CentralVariables
    {
        /// <summary>
        /// Wheelbase of the Vehicle as input by the User
        /// </summary>
        public double WheelBase { get; set; }

        /// <summary>
        /// Front Track of the Vehicle as input by the user
        /// </summary>
        public double Track_Front { get; set; }

        /// <summary>
        /// Rear Track of the Vehicle as input by the user
        /// </summary>
        public double Track_Rear { get; set; }

        /// <summary>
        /// <para><see cref="Point3D"/> representing the Front Roll Center Position</para>
        /// <para>Usually this would represent only a RC Height as for the Front, the lateral and longitudinal coordinates are usually 0</para>
        /// </summary>
        public Point3D RC_Front { get; set; }

        /// <summary>
        /// <para><see cref="Point3D"/> representing the Rear Roll Center Position</para>
        /// <para>Usually this would represent a Longitudinal and Vertical coordinates as the RC is usually symmetric </para>
        /// </summary>
        public Point3D RC_Rear { get; set; }

        /// <summary>
        /// <see cref="Point3D"/> representing the Left Pitch Center
        /// </summary>
        public Point3D PC_Left { get; set; }

        /// <summary>
        /// <see cref="Point3D"/> representing the Right Pitch Center
        /// </summary>
        public Point3D PC_Right { get; set; }

        /// <summary>
        /// Ackermann given as a percentage of the Wheelbase
        /// </summary>
        public double Ackermann { get; set; }




    }
}

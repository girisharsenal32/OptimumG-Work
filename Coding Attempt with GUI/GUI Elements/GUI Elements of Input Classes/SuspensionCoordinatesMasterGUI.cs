using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding_Attempt_with_GUI
{
    public class SuspensionCoordinatesMasterGUI
    {
        #region Auto implemented propeties for Suspension Type
        public int DoubleWishboneIdentifierFront { get; set; }
        public int McPhersonIdentifierFront { get; set; }
        public int DoubleWishboneIdentifierRear { get; set; }
        public int McPhersonIdentifierRear { get; set; }
        public int PushrodIdentifierFront { get; set; }
        public int PullrodIdentifierFront { get; set; }
        public int PushrodIdentifierRear { get; set; }
        public int PullrodIdentifierRear { get; set; }
        public int UARBIdentifierFront { get; set; }
        public int TARBIdentifierFront { get; set; }
        public int UARBIdentifierRear { get; set; }
        public int TARBIdentifierRear { get; set; }
        public int NoOfCouplings { get; set; }
        #endregion

        #region Autoimplemented Coordinates of Master Suspesnion Coordinates Class

        #region Fixed Points
        /// <summary>
        /// Upper Wishbone Fore
        /// </summary>
        public double A1x { get; set; }
        public double A1y { get; set; }
        public double A1z { get; set; }
        /// <summary>
        /// upper Wishbone Aft
        /// </summary>
        public double B1x { get; set; }
        public double B1y { get; set; }
        public double B1z { get; set; }
        /// <summary>
        /// Lower Wishbone For
        /// </summary>
        public double C1x { get; set; }
        public double C1y { get; set; }
        public double C1z { get; set; }
        /// <summary>
        /// Lower Wishbone Aft
        /// </summary>
        public double D1x { get; set; }
        public double D1y { get; set; }
        public double D1z { get; set; }
        /// <summary>
        /// Damper or Coilover Shock Mount
        /// </summary>
        public double JO1x { get; set; }
        public double JO1y { get; set; }
        public double JO1z { get; set; }
        /// <summary>
        /// Bell Crank Pivot
        /// </summary>
        public double I1x { get; set; }
        public double I1y { get; set; }
        public double I1z { get; set; }
        /// <summary>
        /// Antiroll Bar Chassis Mount
        /// </summary>
        public double Q1x { get; set; }
        public double Q1y { get; set; }
        public double Q1z { get; set; }
        /// <summary>
        /// Steering Rod Chassis Mount
        /// </summary>
        public double N1x { get; set; }
        public double N1y { get; set; }
        public double N1z { get; set; }
        /// <summary>
        /// Pinion Start Coordinate
        /// </summary>
        public double Pin1x { get; set; }
        public double Pin1y { get; set; }
        public double Pin1z { get; set; }
        ///// <summary>
        ///// Pinion Axis End Coordinate
        ///// </summary>
        //public double PinA1x { get; set; }
        //public double PinA1y { get; set; }
        //public double PinA1z { get; set; }
        /// <summary>
        /// 1st universal Joint Centre Coordinate
        /// </summary>
        public double UV1x { get; set; }
        public double UV1y { get; set; }
        public double UV1z{ get; set; }
        /// <summary>
        /// 2nd Universal Joint Centre Coordinate
        /// </summary>
        public double UV2x { get; set; }
        public double UV2y { get; set; }
        public double UV2z { get; set; }
        /// <remarks>
        /// Steerning Wheel Mount Chassis
        /// </remarks>
        public double STC1x { get; set; }
        public double STC1y { get; set; }
        public double STC1z { get; set; }
        ///// <summary>
        ///// Steering Wheel Mount's Axis
        ///// </summary>
        //public double STA1x { get; set; }
        //public double STA1y { get; set; }
        //public double STA1z { get; set; }
        /// <summary>
        /// Torion Bar Bottom or Torision Bar Chassis Mounting Point
        /// </summary>
        public double R1x { get; set; }
        public double R1y { get; set; }
        public double R1z { get; set; }
        /// <summary>
        /// Ride Height Reference
        /// </summary>
        public double RideHeightRefx { get; set; }
        public double RideHeightRefy { get; set; }
        public double RideHeightRefz { get; set; }
        #endregion

        #region Moving Points 
        public double J1x { get; set; }
        public double J1y { get; set; }
        public double J1z { get; set; }
        public double H1x { get; set; }
        public double H1y { get; set; }
        public double H1z { get; set; }
        public double O1x { get; set; }
        public double O1y { get; set; }
        public double O1z { get; set; }
        public double G1x { get; set; }
        public double G1y { get; set; }
        public double G1z { get; set; }
        public double F1x { get; set; }
        public double F1y { get; set; }
        public double F1z { get; set; }
        public double E1x { get; set; }
        public double E1y { get; set; }
        public double E1z { get; set; }
        public double M1x { get; set; }
        public double M1y { get; set; }
        public double M1z { get; set; }
        public double K1x { get; set; }
        public double K1y { get; set; }
        public double K1z { get; set; }
        public double P1x { get; set; }
        public double P1y { get; set; }
        public double P1z { get; set; }
        public double W1x { get; set; }
        public double W1y { get; set; }
        public double W1z { get; set; }
        #endregion

        #endregion

        #region Auto implemented properties for Suspension Symmetry
        public bool FrontSymmetryGUI { get; set; }
        public bool RearSymmetryGUI { get; set; }
        #endregion

        #region Auto implemented properties for Suspension Input and Output Origin
        public double _InputOriginX { get; set; }
        public double _InputOriginY { get; set; }
        public double _InputOriginZ { get; set; }
        #endregion

        #region Auto implemented property for Suspension Coordinate's MotionExists 
        public bool SuspensionMotionExists { get; set; } 
        #endregion

    }
}

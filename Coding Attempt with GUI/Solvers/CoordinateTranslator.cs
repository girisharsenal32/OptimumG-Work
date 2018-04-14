using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public static class CoordinateTranslator
    {
        /// <summary>
        /// This class exists exclusively to transalte the coordinates From the stands to the ground
        /// </summary>

        #region Coordinate Declrations
        #region Local Fixed Points Front Declaration
        static double l_A1x, l_A1y, l_A1z, l_B1x, l_B1y, l_B1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, l_JO1x, l_JO1y, l_JO1z, l_I1x, l_I1y, l_I1z, l_Q1x, l_Q1y, l_Q1z, l_N1x, l_N1y, l_N1z,
                  l_Pin1x, l_Pin1y, l_Pin1z, l_UV1x, l_UV1y, l_UV1z, l_UV2x, l_UV2y, l_UV2z, l_STC1x, l_STC1y, l_STC1z, l_R1x, l_R1y, l_R1z, l_RideHeightRefx, l_RideHeightRefy, l_RideHeightRefz;
        #endregion

        #region Local Moving Points Front Declaration
        static double l_J1x, l_J1y, l_J1z, l_H1x, l_H1y, l_H1z, l_O1x, l_O1y, l_O1z, l_G1x, l_G1y, l_G1z, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, l_M1x, l_M1y, l_M1z, l_K1x, l_K1y, l_K1z, l_P1x, l_P1y, l_P1z, l_W1x, l_W1y, l_W1z;
        #endregion
        #endregion

        #region Initialization of the Variables
        public static void InitializeVariables(SuspensionCoordinatesMaster _scmTransIP,SuspensionCoordinatesMaster _scmTransOP, double _ioX, double _ioY, double _ioZ)
        {
            #region Initializing the Local Coordinate Variables for Double Wishbone
            //
            //Fixed or Inboard Points
            //

            ///<remarks>
            ///Irrespective of whether the Car is brought from the Stands to th ground or is being given a motion, I am passing the Input Coordinates of Suspension to this Initialization method. 
            ///Hence, I am going to first translate it to the local coordinate system 
            /// </remarks>
            _scmTransOP.A1x = _scmTransIP.A1x - _ioX;    _scmTransOP.A1y = _scmTransIP.A1y - _ioY;    _scmTransOP.A1z = _scmTransIP.A1z - _ioZ;
            _scmTransOP.B1x = _scmTransIP.B1x - _ioX;    _scmTransOP.B1y = _scmTransIP.B1y - _ioY;    _scmTransOP.B1z = _scmTransIP.B1z - _ioZ;
            _scmTransOP.C1x = _scmTransIP.C1x - _ioX;    _scmTransOP.C1y = _scmTransIP.C1y - _ioY;    _scmTransOP.C1z = _scmTransIP.C1z - _ioZ;
            _scmTransOP.D1x = _scmTransIP.D1x - _ioX;    _scmTransOP.D1y = _scmTransIP.D1y - _ioY;    _scmTransOP.D1z = _scmTransIP.D1z - _ioZ;
            _scmTransOP.I1x = _scmTransIP.I1x - _ioX;    _scmTransOP.I1y = _scmTransIP.I1y - _ioY;    _scmTransOP.I1z = _scmTransIP.I1z - _ioZ;
            _scmTransOP.JO1x = _scmTransIP.JO1x - _ioX;  _scmTransOP.JO1y = _scmTransIP.JO1y - _ioY;  _scmTransOP.JO1z = _scmTransIP.JO1z - _ioZ;
            _scmTransOP.N1x = _scmTransIP.N1x - _ioX;    _scmTransOP.N1y = _scmTransIP.N1y - _ioY;    _scmTransOP.N1z = _scmTransIP.N1z - _ioZ;

            _scmTransOP.Pin1x = _scmTransIP.Pin1x - _ioX; _scmTransOP.Pin1y = _scmTransIP.Pin1y - _ioY; _scmTransOP.Pin1z = _scmTransIP.Pin1z - _ioZ;
            _scmTransOP.UV1x = _scmTransIP.UV1x - _ioX; _scmTransOP.UV1y = _scmTransIP.UV1y - _ioY; _scmTransOP.UV1z = _scmTransIP.UV1z - _ioZ;
            _scmTransOP.UV2x = _scmTransIP.UV2x - _ioX; _scmTransOP.UV2y = _scmTransIP.UV2y - _ioY; _scmTransOP.UV2z = _scmTransIP.UV2z - _ioZ;
            _scmTransOP.STC1x = _scmTransIP.STC1x - _ioX; _scmTransOP.STC1y = _scmTransIP.STC1y - _ioY; _scmTransOP.STC1z = _scmTransIP.STC1z - _ioZ;

            _scmTransOP.Q1x = _scmTransIP.Q1x - _ioX;    _scmTransOP.Q1y = _scmTransIP.Q1y - _ioY;    _scmTransOP.Q1z = _scmTransIP.Q1z - _ioZ;
            _scmTransOP.R1x = _scmTransIP.R1x - _ioX;    _scmTransOP.R1y = _scmTransIP.R1y - _ioY;    _scmTransOP.R1z = _scmTransIP.R1z - _ioZ;
            _scmTransOP.RideHeightRefx = _scmTransIP.RideHeightRefx - _ioX; _scmTransOP.RideHeightRefy = _scmTransIP.RideHeightRefy - _ioY; _scmTransOP.RideHeightRefz = _scmTransIP.RideHeightRefz - _ioZ;
            
            #endregion
           
        }
        #endregion

        #region Translation Methods
        public static void TranslateCoordinates_To_AnyCS(SuspensionCoordinatesMaster _scmTrans, double trans_X, double trans_Y, double trans_Z)
        {
            #region Converting the points back to the INPUT Coordinate System
            ///<summary>
            ///Translating the Fixed Points to the required Coordinate System
            /// </summary>
            ///<summary>
            ///Chassis Pick up of Lower Wishbone
            /// </summary>
            _scmTrans.A1x +=trans_X; _scmTrans.B1x +=trans_X;
            ///<summary>
            ///Chassis Pick Up of Upper Wishbone
            /// </summary>
            _scmTrans.C1x +=trans_X; _scmTrans.D1x +=trans_X;
            ///<summary>
            ///Bell-Crank Pivot and Damper Chassis Pick Up
            /// </summary>
            _scmTrans.I1x +=trans_X; _scmTrans.JO1x +=trans_X;
            ///<summary>
            ///Steering Chassis and ARB Chassis 
            /// </summary>
            _scmTrans.N1x +=/* l_N1x*/trans_X; _scmTrans.Q1x +=trans_X;
            ///<summary>
            ///T-ARB Pick Chassis Mount (IF T-ARB exists)
            /// </summary>
            _scmTrans.R1x += trans_X;
            ///<summary>
            ///Pinion, 1st UV, 2nd UV (if exists) and Steering Column Chassis 
            /// </summary>
            _scmTrans.Pin1x +=/* l_Pin1x*/trans_X; 
            _scmTrans.UV1x +=/* l_UV1x*/trans_X; 
            _scmTrans.UV2x +=/* l_UV2x*/trans_X;
            _scmTrans.STC1x +=/* l_STC1x*/trans_X;

            ///<summary>
            ///Chassis Pick up of Lower Wishbone
            /// </summary>
            _scmTrans.A1y +=trans_Y; _scmTrans.B1y +=trans_Y;
            ///<summary>
            ///Chassis Pick Up of Upper Wishbone
            /// </summary>
            _scmTrans.C1y +=trans_Y; _scmTrans.D1y +=trans_Y;
            ///<summary>
            ///Bell-Crank Pivot and Damper Chassis Pick Up
            /// </summary>
            _scmTrans.I1y +=trans_Y; _scmTrans.JO1y +=trans_Y;
            ///<summary>
            ///Steering Chassis and ARB Chassis 
            /// </summary>
            _scmTrans.N1y += /*l_N1y*/trans_Y; _scmTrans.Q1y +=trans_Y;
            ///<summary>
            ///T-ARB Pick Chassis Mount (IF T-ARB exists)
            /// </summary>
            _scmTrans.R1y += trans_Y;
            ///<summary>
            ///Pinion, 1st UV, 2nd UV (if exists) and Steering Column Chassis 
            /// </summary>
            _scmTrans.Pin1y += /*l_Pin1y*/trans_Y; 
            _scmTrans.UV1y += /*l_UV1y*/trans_Y;
            _scmTrans.UV2y += /*l_UV2y*/trans_Y; 
            _scmTrans.STC1y += /*l_STC1y*/trans_Y;

            ///<summary>
            ///Chassis Pick up of Lower Wishbone
            /// </summary>
            _scmTrans.A1z +=trans_Z; _scmTrans.B1z +=trans_Z;
            ///<summary>
            ///Chassis Pick Up of Upper Wishbone
            /// </summary>
            _scmTrans.C1z +=trans_Z; _scmTrans.D1z +=trans_Z;
            ///<summary>
            ///Bell-Crank Pivot and Damper Chassis Pick Up
            /// </summary>
            _scmTrans.I1z +=trans_Z; _scmTrans.JO1z +=trans_Z;
            ///<summary>
            ///Steering Chassis and ARB Chassis 
            /// </summary>
            _scmTrans.N1z += /*l_N1z*/trans_Z; _scmTrans.Q1z +=trans_Z;
            ///<summary>
            ///T-ARB Pick Chassis Mount (IF T-ARB exists)
            /// </summary>
            _scmTrans.R1z += trans_Z;
            ///<summary>
            ///Pinion, 1st UV, 2nd UV (if exists) and Steering Column Chassis 
            /// </summary>
            _scmTrans.Pin1z += /*l_Pin1z*/trans_Z; 
            _scmTrans.UV1z += /*l_UV1z*/trans_Z; 
            _scmTrans.UV2z += /*l_UV2z*/trans_Z; 
            _scmTrans.STC1z += /*l_STC1z*/trans_Z; 

            


            ///<summary>
            ///Moving Points
            /// </summary>
            _scmTrans.J1x += trans_X; _scmTrans.H1x += trans_X;
            _scmTrans.O1x += trans_X; _scmTrans.G1x += trans_X;
            _scmTrans.F1x += trans_X; _scmTrans.E1x += trans_X;
            _scmTrans.M1x += trans_X; _scmTrans.K1x += trans_X;
            _scmTrans.L1x += trans_X; _scmTrans.P1x += trans_X;
            _scmTrans.W1x += trans_X;

            _scmTrans.J1y += trans_Y; _scmTrans.H1y += trans_Y;
            _scmTrans.O1y += trans_Y; _scmTrans.G1y += trans_Y;
            _scmTrans.F1y += trans_Y; _scmTrans.E1y += trans_Y;
            _scmTrans.M1y += trans_Y; _scmTrans.K1y += trans_Y;
            _scmTrans.L1y += trans_Y; _scmTrans.P1y += trans_Y;
            _scmTrans.W1y += trans_Y;

            _scmTrans.J1z += trans_Z; _scmTrans.H1z += trans_Z;
            _scmTrans.O1z += trans_Z; _scmTrans.G1z += trans_Z;
            _scmTrans.F1z += trans_Z; _scmTrans.E1z += trans_Z;
            _scmTrans.M1z += trans_Z; _scmTrans.K1z += trans_Z;
            _scmTrans.L1z += trans_Z; _scmTrans.P1z += trans_Z;
            _scmTrans.W1z += trans_Z;
            #endregion
        }

        public static void TranslateCoordinates_DropToGround(Vehicle _vehicleTranslations, OutputClass _ocTranslations)
        {
            #region Dropping the Vehicle to the Ground
            //Fixed Points
            _ocTranslations.scmOP.A1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.B1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.C1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.D1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.I1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.JO1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.N1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.Pin1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.UV1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.UV2y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.STC1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.Q1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.R1y -= _ocTranslations.FinalRideHeight_ForTrans;
            //Moving Points
            _ocTranslations.scmOP.J1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.H1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.O1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.G1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.F1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.E1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.M1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.K1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.L1y -= _ocTranslations.FinalRideHeight_ForTrans;
            _ocTranslations.scmOP.P1y -= _ocTranslations.FinalRideHeight_ForTrans; _ocTranslations.scmOP.W1y -= _ocTranslations.FinalRideHeight_ForTrans;
            //_ocTranslations.scmOP.FinalRideHeight_ForTrans -= _ocTranslations.scmOP.FinalRideHeight_ForTrans;
            #endregion
        }

 

        #endregion


    }
}

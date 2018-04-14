using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
//using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using devDept.Geometry;
using devDept.Eyeshot.Entities;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This method consists of methods for the common operations to the Double Wishbone and McPherson Solver classes
    /// </summary>
    public class SolverMasterClass
    {

        #region Declarations

        #region Loop Variables
        public int i, j, k, dummy1, dummy2;
        #endregion

        #region Matrix Declarations
        public static double[,] Matrix_TranslationBC, Matrix_RotationXBC, Matrix_RotationYBC, Matrix_RotationZBC, Matrix_InverseRotYBC, Matrix_InverseRotXBC, Matrix_InverseTBC;
        double[,] Matrix_TranslationCnT, Matrix_InverseTCnT, Matrix_RotationZCnT, Matrix_RotationYCnT, Matrix_RotationAboutAxis, Matrix_Concat1CnT, Matrix_Concat2CnT, Matrix_Concat3CnT;
        public static double[,] Matrix_J, Matrix_H, Matrix_O, Matrix_AxisBC;
        public static double[,] Matrix_pseudo_AxisBC1, Matrix_pseudo_AxisBC2, Matrix_pseudo_AxisBC3;
        #endregion

        #region Local Fixed Points Front Declaration
        public double l_A1x, l_A1y, l_A1z, l_B1x, l_B1y, l_B1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, l_JO1x, l_JO1y, l_JO1z, l_I1x, l_I1y, l_I1z, l_Q1x, l_Q1y, l_Q1z, l_N1x, l_N1y, l_N1z,
                  l_Pin1x,l_Pin1y,l_Pin1z,l_UV1x,l_UV1y,l_UV1z,l_UV2x,l_UV2y,l_UV2z,l_STC1x,l_STC1y,l_STC1z, l_R1x, l_R1y, l_R1z, l_RideHeightRefx, l_RideHeightRefy, l_RideHeightRefz;
        #endregion

        #region Local Moving Points Front Declaration
        public double l_J1x, l_J1y, l_J1z, l_H1x, l_H1y, l_H1z, l_O1x, l_O1y, l_O1z, l_G1x, l_G1y, l_G1z, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, l_M1x, l_M1y, l_M1z, l_K1x, l_K1y, l_K1z, l_P1x, l_P1y, l_P1z, l_W1x, l_W1y, l_W1z;
        public double Lx = 0, Ly = 0, Lz = 0; // Initial Coordinates of Spindle End
        public double L1x, L1y, L1z; // Final Coordinated of Spindle End
        #endregion

        #region Local variables for Camber and Toe
        public double StaticCamber, StaticToe; // Local variables for Camber and Toe declared and used so that if the same object of WheelAlignment is passed, it can be manipulated if needed
        #endregion

        #region Local Variables to required to calculate Steering Angles 
        private double SteeringRatio;
        double Angle_InputOutputShaft, Angle_InputIntermediateShaft, Angle_IntermediateOutputShaft;
        static double deltaAngle_Pinion, Angle_InternmediateShaft; 
        #endregion

        #region Local variables to calculate KPI and Caster
        public double E1F1_KPI, E1F1_Caster;
        public double WheelCentreVert_KPI, WheelCentreVert_Caster;
        #endregion

        #region Local Variables for Wheel Deflection Calculations
        public double WheelDeflection = 0, SpringDeflection = 0, SpringDeflection_Delta = 0, Preload_mm = 0, Damper_Preload = 0, Combined_PreloadForce = 0;
        public double[] SpringDeflection_Prev;
        double temp_SpringDef_ForIndexZero = 0;
        //public double[] DeltaSpringDef_Steering_FL, DeltaSpringDef_Steering_FR;
        #endregion

        #region Local Variables to calculate the Spindle Vectors for Camber and Toe or Camber and Steering Angle
        public double dToe, dToe_Steering, dCamber, dCamber_Steering, deltaSteeringWheel, KL_Toe, K2L2_Toe, K2L2_ToePrev, KL_Camber, K2L2_Camber, K2L2_CamberPrev, M1M2, M1K1;
        public double SpindleX1, SpindleY1, SpindleZ1; // These represent the position vectors of the wheel spindle. This is required because if the camber and toe are calculated inside the loop, then it results in no camber or toe change. 
        #endregion

        #region Variables for simulation setup
        public int a1, N = 200;
        public double Corrected_SpringDeflection_Loop, J2Jo_Loop;
        #endregion

        #region Variables to calculate the bell-crank rotation or spring deflection in small steps
        public double /*AngleOfRotation,*/ J1I, JoI, J1Jo, J2Jo;
        #endregion

        #region Variable to store the Vertical Wheel Forces
        public double Fy_Wheel, Fy_Spring;
        #endregion

        #region Variable to store the temporary outputclass
        public OutputClass temp_OC;
        #endregion

        #region Variable to Indicate that the motion is returning to bump after series of rebounds
        public bool IsStillinRebound = true;
        #endregion

        #region Objects of the SetupChangeDatabase Class
        /// <summary>
        /// Master Object of the SetupChangeDatabase class which holds the vectors and points representing the Wheel Assembly parameters which can be changed
        /// </summary>
        public SetupChangeDatabase SetupChange_DB_Master = new SetupChangeDatabase();
        /// <summary>
        /// Front Left Object of the SetupChangeDatabase class which holds the vectors and points representing the Wheel Assembly parameters which can be changed
        /// </summary>
        SetupChangeDatabase SetupChange_DB_FL = new SetupChangeDatabase();
        /// <summary>
        /// Front Right Object of the SetupChangeDatabase class which holds the vectors and points representing the Wheel Assembly parameters which can be changed
        /// </summary>
        SetupChangeDatabase SetupChange_DB_FR = new SetupChangeDatabase();
        /// <summary>
        /// Rear Left Object of the SetupChangeDatabase class which holds the vectors and points representing the Wheel Assembly parameters which can be changed
        /// </summary>
        SetupChangeDatabase SetupChange_DB_RL = new SetupChangeDatabase();
        /// <summary>
        /// Rear Right Object of the SetupChangeDatabase class which holds the vectors and points representing the Wheel Assembly parameters which can be changed
        /// </summary>
        SetupChangeDatabase SetupChange_DB_RR = new SetupChangeDatabase();
        #endregion

        #region Objects off the SetupChange_CornerVariablesClass
        ///// <summary>
        ///// Front Left object of the <see cref="SetupChange_CornerVariables"/> class.
        ///// </summary>
        //SetupChange_CornerVariables SetupChange_CV_FL;
        ///// <summary>
        ///// Front Right object of the <see cref="SetupChange_CornerVariables"/> class.
        ///// </summary>
        //SetupChange_CornerVariables SetupChange_CV_FR;
        ///// <summary>
        ///// Rear Left object of the <see cref="SetupChange_CornerVariables"/> class.
        ///// </summary>
        //SetupChange_CornerVariables SetupChange_CV_RL;
        ///// <summary>
        ///// Rear Rght object of the <see cref="SetupChange_CornerVariables"/> class.
        ///// </summary>
        //SetupChange_CornerVariables SetupChange_CV_RR;

        #endregion

        #region Objects of the SetupChange_ClosedLoopVariables Class
        /// <summary>
        /// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Front Left Corner
        /// </summary>
        SetupChange_ClosedLoopSolver SetupChange_CLS;
        ///// <summary>
        ///// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Front Right Corner
        ///// </summary>
        //SetupChange_ClosedLoopSolver SetupChange_CLS_FR;
        ///// <summary>
        ///// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Rear Left Corner
        ///// </summary>
        //SetupChange_ClosedLoopSolver SetupChange_CLS_RL;
        ///// <summary>
        ///// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Rear Right Corner
        ///// </summary>
        //SetupChange_ClosedLoopSolver SetupChange_CLS_RR;
        #endregion

        double XW1 = 0, YW1 = 0, ZW1 = 0, XW2 = 0, YW2 = 0, ZW2 = 0;



        #endregion

        #region Common Methods

        //public void DeclareMatrices()
        //{
        //    var M = Matrix<double>.Build;
        //    var m = M.Dense(6, 6);
        //}

        #region Initializer Methods

        #region Initializing the Local Coordinate Variables
        public void AssignLocalCoordinateVariables_FixesPoints(SuspensionCoordinatesMaster _scmAssign)
        {
            l_A1x = _scmAssign.A1x; l_A1y = _scmAssign.A1y; l_A1z = _scmAssign.A1z;
            l_B1x = _scmAssign.B1x; l_B1y = _scmAssign.B1y; l_B1z = _scmAssign.B1z;
            l_C1x = _scmAssign.C1x; l_C1y = _scmAssign.C1y; l_C1z = _scmAssign.C1z;
            l_D1x = _scmAssign.D1x; l_D1y = _scmAssign.D1y; l_D1z = _scmAssign.D1z;
            l_I1x = _scmAssign.I1x; l_I1y = _scmAssign.I1y; l_I1z = _scmAssign.I1z;
            l_JO1x = _scmAssign.JO1x; l_JO1y = _scmAssign.JO1y; l_JO1z = _scmAssign.JO1z;
            l_N1x = _scmAssign.N1x; l_N1y = _scmAssign.N1y; l_N1z = _scmAssign.N1z;

            ///<remarks>
            ///Newly added coordinates of Steering Column
            /// </remarks>
            l_Pin1x = _scmAssign.Pin1x; l_Pin1y = _scmAssign.Pin1y; l_Pin1z = _scmAssign.Pin1z;
            l_UV1x = _scmAssign.UV1x; l_UV1y = _scmAssign.UV1y; l_UV1z = _scmAssign.UV1z;
            l_UV2x = _scmAssign.UV2x; l_UV2y = _scmAssign.UV2y; l_UV2z = _scmAssign.UV2z;
            l_STC1x = _scmAssign.STC1x; l_STC1y = _scmAssign.STC1y; l_STC1z = _scmAssign.STC1z;

            l_Q1x = _scmAssign.Q1x; l_Q1y = _scmAssign.Q1y; l_Q1z = _scmAssign.Q1z;
            l_R1x = _scmAssign.R1x; l_R1y = _scmAssign.R1y; l_R1z = _scmAssign.R1z;
            l_RideHeightRefx = _scmAssign.RideHeightRefx; l_RideHeightRefy = _scmAssign.RideHeightRefy; l_RideHeightRefz = _scmAssign.RideHeightRefz;
        }
        public void AssignLocalCoordinateVariables_MovingPoints(SuspensionCoordinatesMaster _scmAssign)
        {
            l_E1x = _scmAssign.E1x; l_E1y = _scmAssign.E1y; l_E1z = _scmAssign.E1z;
            l_F1x = _scmAssign.F1x; l_F1y = _scmAssign.F1y; l_F1z = _scmAssign.F1z;
            l_G1x = _scmAssign.G1x; l_G1y = _scmAssign.G1y; l_G1z = _scmAssign.G1z;
            l_H1x = _scmAssign.H1x; l_H1y = _scmAssign.H1y; l_H1z = _scmAssign.H1z;
            l_J1x = _scmAssign.J1x; l_J1y = _scmAssign.J1y; l_J1z = _scmAssign.J1z;
            l_K1x = _scmAssign.K1x; l_K1y = _scmAssign.K1y; l_K1z = _scmAssign.K1z;
            l_M1x = _scmAssign.M1x; l_M1y = _scmAssign.M1y; l_M1z = _scmAssign.M1z;
            l_O1x = _scmAssign.O1x; l_O1y = _scmAssign.O1y; l_O1z = _scmAssign.O1z;
            l_P1x = _scmAssign.P1x; l_P1y = _scmAssign.P1y; l_P1z = _scmAssign.P1z;
            l_W1x = _scmAssign.W1x; l_W1y = _scmAssign.W1y; l_W1z = _scmAssign.W1z;
        }
        #endregion

        #region Initializing Camber and Toe
        public void InitializeWheelAlignmentVariables(WheelAlignment _waInitialize, OutputClass _ocInitialize, int _identifierInitialize, bool _steeringExists)
        {
            _ocInitialize.waOP.WheelIsSteered = _steeringExists;

            #region Initializating the Local Wheel Alignment variables
            StaticCamber = _waInitialize.StaticCamber * (Math.PI / 180);
            StaticToe = _waInitialize.StaticToe * (Math.PI / 180);
            #endregion

            #region Initializing the Camber and Toe of the Output Class
            //if (_identifierInitialize == 1 || _identifierInitialize == 3)
            //{
            //    _ocInitialize.waOP.StaticCamber = -StaticCamber;
            //    _ocInitialize.waOP.StaticToe = /*-*/StaticToe;

            //}
            //else if (_identifierInitialize == 2 || _identifierInitialize == 4)
            //{
            //    _ocInitialize.waOP.StaticCamber = /*-*/StaticCamber;
            //    _ocInitialize.waOP.StaticToe = -StaticToe;
            //}
            AssignOrientation_CamberToe(ref _ocInitialize.waOP.StaticCamber, ref _ocInitialize.waOP.StaticToe, StaticCamber, StaticToe, _identifierInitialize);
            #endregion


            ///<remarks>
            ///The below line of code is added here to calculate the magnitude of the vector joining the Wheel Centre and the Tie rod point on the Upright. This vector is used to calculate the change in Toe for a given 
            ///in the Steering Angle. It is used in the ToeRod_Steering() method.
            /// </remarks>
            M1K1 = Math.Sqrt(Math.Pow((l_M1x - l_K1x), 2) + Math.Pow((l_M1z - l_K1z), 2));
        }

        /// <summary>
        /// Assigns the Orientation of Camber and Toe based on the corner which is being considered using the Identifier variable
        /// </summary>
        /// <param name="_camberValueReceiver"></param>
        /// <param name="_toeValueReceiver"></param>
        /// <param name="_camberValueSender"></param>
        /// <param name="_toeValueSender"></param>
        /// <param name="_identifier"></param>
        private void AssignOrientation_CamberToe(ref double _camberValueReceiver, ref double _toeValueReceiver, double _camberValueSender, double _toeValueSender, int _identifier)
        {
            if (_identifier == 1 || _identifier == 3)
            {
                _camberValueReceiver = -_camberValueSender;
                _toeValueReceiver = /*-*/_toeValueSender;

            }
            else if (_identifier == 2 || _identifier == 4)
            {
                _camberValueReceiver = /*-*/_camberValueSender;
                _toeValueReceiver = -_toeValueSender;
            }
        }

        #endregion

        #region KPI and Caster Angle
        public void CalculateKPIandCaster(OutputClass _ocKPICaster, bool IsDoubleWishbone, int _identifier)
        {
            if (IsDoubleWishbone)
            {
                E1F1_Caster = Math.Sqrt(Math.Pow((l_F1y - l_E1y), 2) + Math.Pow((l_F1z - l_E1z), 2));
                WheelCentreVert_Caster = Math.Sqrt(Math.Pow((l_K1y - 0), 2))/* + Math.Pow((l_K1z - 0), 2))*/;
                _ocKPICaster.Caster = (Math.Acos((((l_F1y - l_E1y) * (l_K1y - 0)) + ((l_F1z - l_E1z) * (l_K1z - l_K1z))) / (E1F1_Caster * WheelCentreVert_Caster)));

                E1F1_KPI = Math.Sqrt(Math.Pow((l_F1x - l_E1x), 2) + Math.Pow((l_F1y - l_E1y), 2));
                WheelCentreVert_KPI = Math.Sqrt(/*Math.Pow((l_K1x - 0), 2) +*/ Math.Pow((l_K1y - 0), 2));
                _ocKPICaster.KPI = (Math.Acos((((l_F1x - l_E1x) * (l_K1x - l_K1x)) + ((l_F1y - l_E1y) * (l_K1y - 0))) / (E1F1_KPI * WheelCentreVert_KPI))); 
            }
            else if (!IsDoubleWishbone)
            {
                E1F1_Caster = Math.Sqrt(Math.Pow((l_J1y - l_E1y), 2) + Math.Pow((l_J1z - l_E1z), 2));
                WheelCentreVert_Caster = Math.Sqrt(Math.Pow((l_K1y - 0), 2))/* + Math.Pow((l_K1z - 0), 2))*/;
                _ocKPICaster.Caster = (Math.Acos((((l_J1y - l_E1y) * (l_K1y - 0)) + ((l_J1z - l_E1z) * (l_K1z - l_K1z))) / (E1F1_Caster * WheelCentreVert_Caster)));

                E1F1_KPI = Math.Sqrt(Math.Pow((l_J1x - l_E1x), 2) + Math.Pow((l_J1y - l_E1y), 2));
                WheelCentreVert_KPI = Math.Sqrt(/*Math.Pow((l_K1x - 0), 2) +*/ Math.Pow((l_K1y - 0), 2));
                _ocKPICaster.KPI = (Math.Acos((((l_J1x - l_E1x) * (l_K1x - l_K1x)) + ((l_J1y - l_E1y) * (l_K1y - 0))) / (E1F1_KPI * WheelCentreVert_KPI)));
            }

            if (_identifier == 1 || _identifier == 3)
            {
                //_ocKPICaster.Caster *= -1;
                _ocKPICaster.KPI *= /*-*/1;
            }
            else if (_identifier == 2 || _identifier == 4)
            {
                //_ocKPICaster.Caster *= /*-*/1;
                _ocKPICaster.KPI *= -1;
            }

        }
        #endregion

        #region Converting Coordinates from World Coordinate System to Car Coordinate System
        public void TranslateToLocalCS(SuspensionCoordinatesMaster _scmLCS)
        {
            #region Fixed Points Translation to Local Coordinate System
            //  Coordinates of Fixed Point A
            l_A1x -= _scmLCS.InputOriginX;
            l_A1y -= _scmLCS.InputOriginY;
            l_A1z -= _scmLCS.InputOriginZ;

            //  Coordinates of Fixed Point B
            l_B1x -= _scmLCS.InputOriginX;
            l_B1y -= _scmLCS.InputOriginY;
            l_B1z -= _scmLCS.InputOriginZ;

            //  Coordinates of Fixed Point C
            l_C1x -= _scmLCS.InputOriginX;
            l_C1y -= _scmLCS.InputOriginY;
            l_C1z -= _scmLCS.InputOriginZ;

            //  Coordinates of Fixed Point D
            l_D1x -= _scmLCS.InputOriginX;
            l_D1y -= _scmLCS.InputOriginY;
            l_D1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point I
            l_I1x -= _scmLCS.InputOriginX;
            l_I1y -= _scmLCS.InputOriginY;
            l_I1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point Jo
            l_JO1x -= _scmLCS.InputOriginX;
            l_JO1y -= _scmLCS.InputOriginY;
            l_JO1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            l_N1x -= _scmLCS.InputOriginX;
            l_N1y -= _scmLCS.InputOriginY;
            l_N1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Fixed Point Pin1
            l_Pin1x -= _scmLCS.InputOriginX;
            l_Pin1y -= _scmLCS.InputOriginY;
            l_Pin1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            l_UV1x -= _scmLCS.InputOriginX;
            l_UV1y -= _scmLCS.InputOriginY;
            l_UV1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            l_UV2x -= _scmLCS.InputOriginX;
            l_UV2y -= _scmLCS.InputOriginY;
            l_UV2z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            l_STC1x -= _scmLCS.InputOriginX;
            l_STC1y -= _scmLCS.InputOriginY;
            l_STC1z -= _scmLCS.InputOriginZ;

            //  Coordinates of Fixed Point Q
            l_Q1x -= _scmLCS.InputOriginX;
            l_Q1y -= _scmLCS.InputOriginY;
            l_Q1z -= _scmLCS.InputOriginZ;

            // Coordinates of Fixed Point R
            l_R1x -= _scmLCS.InputOriginX;
            l_R1y -= _scmLCS.InputOriginY;
            l_R1z -= _scmLCS.InputOriginZ;

            #endregion

            #region Moving Points translation to Local Coordinate System
            // Initial Coordinates of Moving Point J
            l_J1x -= _scmLCS.InputOriginX;
            l_J1y -= _scmLCS.InputOriginY;
            l_J1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point H
            l_H1x -= _scmLCS.InputOriginX;
            l_H1y -= _scmLCS.InputOriginY;
            l_H1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point G
            l_G1x -= _scmLCS.InputOriginX;
            l_G1y -= _scmLCS.InputOriginY;
            l_G1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point F
            l_F1x -= _scmLCS.InputOriginX;
            l_F1y -= _scmLCS.InputOriginY;
            l_F1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point E
            l_E1x -= _scmLCS.InputOriginX;
            l_E1y -= _scmLCS.InputOriginY;
            l_E1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point K
            l_K1x -= _scmLCS.InputOriginX; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            l_K1y -= _scmLCS.InputOriginY;//PATCH CENTRE - 1/2 TIRE WIDTH
            l_K1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point M
            l_M1x -= _scmLCS.InputOriginX;
            l_M1y -= _scmLCS.InputOriginY;
            l_M1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point O
            l_O1x -= _scmLCS.InputOriginX;
            l_O1y -= _scmLCS.InputOriginY;
            l_O1z -= _scmLCS.InputOriginZ;

            // Initial Coordinates of Moving Point P
            l_P1x -= _scmLCS.InputOriginX;
            l_P1y -= _scmLCS.InputOriginY;
            l_P1z -= _scmLCS.InputOriginZ;

            //  Coordinates of Moving Contact Patch Point W
            l_W1x -= _scmLCS.InputOriginX;
            l_W1y -= _scmLCS.InputOriginY;
            l_W1z -= _scmLCS.InputOriginZ;

            //  Ride Height Reference Points
            l_RideHeightRefx -= _scmLCS.InputOriginX;
            l_RideHeightRefy -= _scmLCS.InputOriginY;
            l_RideHeightRefz -= _scmLCS.InputOriginZ;
            #endregion 
        }
        #endregion

        #region Metthod to Translate the Coordinates to any Coordinate System
        public void TranslateToRequiredCS(SuspensionCoordinatesMaster _scmTrans, OutputClass _ocTrans, Vehicle _vTrans, bool _motionExists, double ipX, double ipY, double ipZ, double opX, double opY, double opZ)
        {
            CoordinateTranslator.InitializeVariables(_scmTrans, _ocTrans.scmOP, ipX, ipY, ipZ);
            CoordinateTranslator.TranslateCoordinates_To_AnyCS(_ocTrans.scmOP, ipX, ipY, ipZ);
            _ocTrans.FinalRideHeight_ForTrans += ipY;
            if (!_motionExists)
            {
                CoordinateTranslator.TranslateCoordinates_DropToGround(_vTrans, _ocTrans);
            }
            CoordinateTranslator.TranslateCoordinates_To_AnyCS(_ocTrans.scmOP, opX, opY, opZ);
            _ocTrans.FinalRideHeight_ForTrans += opY;
        } 
        #endregion

        #region Calculating the Spindle End using the Camber and Toe Angles which are input by the user
        /// <summary>
        /// Initialzer and Invoker method to rotate the Spindle Vector and calculate its end coordinates. Rotation is done about the Steering Axis (arbitrary axis) or about the standard axis based on user choice. 
        /// </summary>
        /// <param name="_rotaboutZ"></param>
        /// <param name="_rotaboutY"></param>
        /// <param name="_arbitraryAxis"></param>
        /// <param name="_axis1x"></param>
        /// <param name="_axis1y"></param>
        /// <param name="_axis1z"></param>
        /// <param name="_axis2x"></param>
        /// <param name="_axis2y"></param>
        /// <param name="_axis2z"></param>
        /// <param name="_Ipx"></param>
        /// <param name="_Ipy"></param>
        /// <param name="_Ipz"></param>
        /// <param name="_Opx"></param>
        /// <param name="_Opy"></param>
        /// <param name="_Opz"></param>
        public void RotateSpindleVector(double _rotaboutZ, double _rotaboutY, bool _arbitraryAxis, double _axis1x, double _axis1y, double _axis1z, double _axis2x, double _axis2y, double _axis2z, double _Ipx, double _Ipy, double _Ipz,out double _Opx, out double _Opy, out double _Opz)
        {
            ///<remarks>This method initializes the Translational and Rotational Matrices required for the Transformation</remarks>
            

            _Opx = 0; _Opy = 0; _Opz = 0;
            
            double UnitX, UnitY, UnitZ, MagU;

            Matrix_TranslationCnT = new double[4, 4];
            Matrix_RotationZCnT = new double[4, 4];
            Matrix_RotationYCnT = new double[4, 4];
            Matrix_RotationAboutAxis = new double[4, 4];
            Matrix_InverseTCnT = new double[4, 4];
            Matrix_Concat1CnT = new double[4, 4];
            Matrix_Concat2CnT = new double[4, 4];
            Matrix_Concat3CnT = new double[4, 4];

            MagU = Math.Sqrt(Math.Pow(_axis1x - _axis2x, 2) + Math.Pow(_axis1y - _axis2y, 2) + Math.Pow((_axis1z - _axis2z), 2));
            UnitX = (_axis1x - _axis2x) / MagU;
            UnitY = (_axis1y - _axis2y) / MagU;
            UnitZ = (_axis1z - _axis2z) / MagU;

            for (i = 0; i < 4; i++) // To initialize all diagonal elements to 1 and remaining to 0
            {
                for (j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        Matrix_TranslationCnT[i, j] = 1;
                        Matrix_RotationZCnT[i, j] = 1;
                        Matrix_RotationYCnT[i, j] = 1;
                        Matrix_InverseTCnT[i, j] = 1;
                    }
                    else
                    {
                        Matrix_TranslationCnT[i, j] = 0;
                        Matrix_RotationZCnT[i, j] = 0;
                        Matrix_RotationYCnT[i, j] = 0;
                        Matrix_InverseTCnT[i, j] = 0;
                    }
                }
            }

            Matrix_TranslationCnT[0, 3] = l_K1x; // To assign the required values to the required cells
            Matrix_TranslationCnT[1, 3] = l_K1y;
            Matrix_TranslationCnT[2, 3] = l_K1z;

            Matrix_InverseTCnT[0, 3] = -l_K1x;
            Matrix_InverseTCnT[1, 3] = -l_K1y;
            Matrix_InverseTCnT[2, 3] = -l_K1z;

            Matrix_RotationZCnT[0, 0] = Math.Cos(_rotaboutZ);
            Matrix_RotationZCnT[0, 1] = -Math.Sin(_rotaboutZ);
            Matrix_RotationZCnT[1, 0] = Math.Sin(_rotaboutZ);
            Matrix_RotationZCnT[1, 1] = Math.Cos(_rotaboutZ);

            if (_arbitraryAxis)
            {
                Matrix_RotationAboutAxis[0, 0] = Math.Cos(_rotaboutY) + (Math.Pow(UnitX, 2) * (1 - Math.Cos(_rotaboutY)));
                Matrix_RotationAboutAxis[0, 1] = ((UnitX * UnitY) * (1 - Math.Cos(_rotaboutY))) - (UnitZ * (Math.Sin(_rotaboutY)));
                Matrix_RotationAboutAxis[0, 2] = ((UnitX * UnitZ) * (1 - Math.Cos(_rotaboutY))) + (UnitY * (Math.Sin(_rotaboutY)));
                Matrix_RotationAboutAxis[0, 3] = 0;
                Matrix_RotationAboutAxis[1, 0] = ((UnitY * UnitX) * (1 - Math.Cos(_rotaboutY))) + (UnitZ * (Math.Sin(_rotaboutY)));
                Matrix_RotationAboutAxis[1, 1] = Math.Cos(_rotaboutY) + (Math.Pow(UnitY, 2) * (1 - Math.Cos(_rotaboutY)));
                Matrix_RotationAboutAxis[1, 2] = ((UnitY * UnitZ) * (1 - Math.Cos(_rotaboutY))) - (UnitX * (Math.Sin(_rotaboutY)));
                Matrix_RotationAboutAxis[1, 3] = 0;
                Matrix_RotationAboutAxis[2, 0] = ((UnitZ * UnitX) * (1 - Math.Cos(_rotaboutY))) - (UnitY * (Math.Sin(_rotaboutY)));
                Matrix_RotationAboutAxis[2, 1] = ((UnitZ * UnitY) * (1 - Math.Cos(_rotaboutY))) + (UnitX * (Math.Sin(_rotaboutY)));
                Matrix_RotationAboutAxis[2, 2] = Math.Cos(_rotaboutY) + (Math.Pow(UnitZ, 2) * (1 - Math.Cos(_rotaboutY)));
                Matrix_RotationAboutAxis[2, 3] = 0;
                Matrix_RotationAboutAxis[3, 0] = 0;
                Matrix_RotationAboutAxis[3, 1] = 0;
                Matrix_RotationAboutAxis[3, 2] = 0;
                Matrix_RotationAboutAxis[3, 3] = 1;

                CalculateSpindleEndCoordinates_ArbitraryAxes(_Ipx, _Ipy, _Ipz, out _Opx, out _Opy, out _Opz);

            }
            else if (!_arbitraryAxis)
            {
                Matrix_RotationYCnT[0, 0] = Math.Cos(_rotaboutY);
                Matrix_RotationYCnT[0, 2] = Math.Sin(_rotaboutY);
                Matrix_RotationYCnT[2, 0] = -Math.Sin(_rotaboutY);
                Matrix_RotationYCnT[2, 2] = Math.Cos(_rotaboutY);

                CalculateSpindleEndCoordinates_StandardAxes(_Ipx, _Ipy, _Ipz, out _Opx, out _Opy, out _Opz);
            }




        }

        /// <summary>
        /// Method to Rotate the Spindle End about the Arbitrary Axes for Camber and Toe Angles and calculate its end coordinate
        /// The arbitrary axis is axis joining the UBJ and LBJ Points
        /// </summary>
        /// <param name="_ipx"></param>
        /// <param name="_ipy"></param>
        /// <param name="_ipz"></param>
        /// <param name="_opx"></param>
        /// <param name="_opy"></param>
        /// <param name="_opz"></param>
        private void CalculateSpindleEndCoordinates_ArbitraryAxes(double _ipx, double _ipy, double _ipz, out double _opx, out double _opy, out double _opz)
        {
            ///<summary
            ///This method rotates a point about an arbitrary axis 
            ///</summary
            
            for (i = 0; i < 4; i++) //Translation Matrix x RotiationZ Matrix
            {
                for (j = 0; j < 4; j++)
                {
                    for (k = 0; k < 4; k++)
                    {
                        Matrix_Concat1CnT[i, j] = Matrix_Concat1CnT[i, j] + (Matrix_TranslationCnT[i, k] * Matrix_RotationZCnT[k, j]);
                    }
                }
            }

            for (i = 0; i < 4; i++) // Translation x RotationZ x RotationY
            {
                for (j = 0; j < 4; j++)
                {
                    for (k = 0; k < 4; k++)
                    {
                        Matrix_Concat2CnT[i, j] = Matrix_Concat2CnT[i, j] + (Matrix_Concat1CnT[i, k] * Matrix_RotationAboutAxis[k, j]);
                    }
                }
            }
            for (i = 0; i < 4; i++) // Translation x RotationZ x RotationY x Inverse Translation
            {
                for (j = 0; j < 4; j++)
                {
                    for (k = 0; k < 4; k++)
                    {
                        Matrix_Concat3CnT[i, j] = Matrix_Concat3CnT[i, j] + (Matrix_Concat2CnT[i, k] * Matrix_InverseTCnT[k, j]);
                    }
                }
            }

            _opx = ((Matrix_Concat3CnT[0, 0] * _ipx) + (Matrix_Concat3CnT[0, 1] * _ipy) + (Matrix_Concat3CnT[0, 2] * _ipz) + (Matrix_Concat3CnT[0, 3] * 1));
            _opy = ((Matrix_Concat3CnT[1, 0] * _ipx) + (Matrix_Concat3CnT[1, 1] * _ipy) + (Matrix_Concat3CnT[1, 2] * _ipz) + (Matrix_Concat3CnT[1, 3] * 1));
            _opz = ((Matrix_Concat3CnT[2, 0] * _ipx) + (Matrix_Concat3CnT[2, 1] * _ipy) + (Matrix_Concat3CnT[2, 2] * _ipz) + (Matrix_Concat3CnT[2, 3] * 1));




        }

        /// <summary>
        /// Method to Rotate the Spindle End about the Standard Axes for Camber and Toe Angles and calculate its end coordinate
        /// </summary>
        /// <param name="_ipx"></param>
        /// <param name="_ipy"></param>
        /// <param name="_ipz"></param>
        /// <param name="_opx"></param>
        /// <param name="_opy"></param>
        /// <param name="_opz"></param>
        private void CalculateSpindleEndCoordinates_StandardAxes(double _ipx, double _ipy, double _ipz, out double _opx, out double _opy, out double _opz)
        {
            ///<summary
            ///This function rotates a point about the Z and Y axis
            ///</summary
            
            for (i = 0; i < 4; i++) //Translation Matrix x RotiationZ Matrix
            {
                for (j = 0; j < 4; j++)
                {
                    for (k = 0; k < 4; k++)
                    {
                        Matrix_Concat1CnT[i, j] = Matrix_Concat1CnT[i, j] + (Matrix_TranslationCnT[i, k] * Matrix_RotationZCnT[k, j]);
                    }
                }
            }

            for (i = 0; i < 4; i++) // Translation x RotationZ x RotationY
            {
                for (j = 0; j < 4; j++)
                {
                    for (k = 0; k < 4; k++)
                    {
                        Matrix_Concat2CnT[i, j] = Matrix_Concat2CnT[i, j] + (Matrix_Concat1CnT[i, k] * Matrix_RotationYCnT[k, j]);
                    }
                }
            }
            for (i = 0; i < 4; i++) // Translation x RotationZ x RotationY x Inverse Translation
            {
                for (j = 0; j < 4; j++)
                {
                    for (k = 0; k < 4; k++)
                    {
                        Matrix_Concat3CnT[i, j] = Matrix_Concat3CnT[i, j] + (Matrix_Concat2CnT[i, k] * Matrix_InverseTCnT[k, j]);
                    }
                }
            }

            _opx = ((Matrix_Concat3CnT[0, 0] * _ipx) + (Matrix_Concat3CnT[0, 1] * _ipy) + (Matrix_Concat3CnT[0, 2] * _ipz) + (Matrix_Concat3CnT[0, 3] * 1));
            _opy = ((Matrix_Concat3CnT[1, 0] * _ipx) + (Matrix_Concat3CnT[1, 1] * _ipy) + (Matrix_Concat3CnT[1, 2] * _ipz) + (Matrix_Concat3CnT[1, 3] * 1));
            _opz = ((Matrix_Concat3CnT[2, 0] * _ipx) + (Matrix_Concat3CnT[2, 1] * _ipy) + (Matrix_Concat3CnT[2, 2] * _ipz) + (Matrix_Concat3CnT[2, 3] * 1));
        }
        #endregion

        #region Initalizing the Spindle End
        public void InitializeSpindleEndCoordinates(Tire _tireSpindleEnd)
        {
            if (l_W1x < 0) { Lx = l_K1x - (_tireSpindleEnd.TireWidth / 2); }
            else if (l_W1x > 0) { Lx = l_K1x + (_tireSpindleEnd.TireWidth / 2); }
            Ly = l_K1y;
            Lz = l_K1z;

        }
        #endregion

        #region Preliminary Calculations to find the New Camber and Toe - Used only if Motion does not exist or for the Rear
        public void CalculateInitialSpindleVector()
        {
            ///<remarks>
            ///Preliminary Calculations to find the New Camber and Toe. The calculation is done in two steps and is done outside the for loop. If not done this way the change in camber and toe will be calculationed
            ///using initial and final values which are are very close to each other. 
            ///This is because, the angle between old and new wheel spindle vector is calculated for very small increments and hence results in very small angle change. By declaring and calculating the Spindle variabe, 
            ///the inital toe and camber are used for the delta camber and toe calcs
            ///</ remarks >
            ///
            SpindleX1 = L1x  /*Lx*/ - l_K1x;
            SpindleY1 = L1y  /*Ly*/ - l_K1y;
            SpindleZ1 = L1z  /*Lz*/ - l_K1z;
            KL_Toe = Math.Sqrt(Math.Pow((SpindleX1), 2) + Math.Pow((SpindleZ1), 2));
            KL_Camber = Math.Sqrt(Math.Pow((SpindleX1), 2) + Math.Pow((SpindleY1), 2));

        }
        #endregion

        #endregion

        #region Calculating the Wheel and Spring Deflections for Static force or for Motion
        public void CalculateWheelAndSpringDeflection(SuspensionCoordinatesMaster _scmDef, Spring _springDef, Damper _damperDef, Vehicle _vehicleDef, List<double> _WheelDeflection, OutputClass _ocDef, Tire _tireDef, int Identifier, double arb_Rate_Nmm, bool MotionExists,bool _recalSteering, int Index)
        {
            _ocDef.WheelRate = (_springDef.SpringRate * Math.Pow(_scmDef.InitialMR, 2)) + (arb_Rate_Nmm * Math.Pow(_scmDef.Initial_ARB_MR, 2));
            _ocDef.WheelRate_WO_ARB = (_springDef.SpringRate * Math.Pow(_scmDef.InitialMR, 2));
            _ocDef.RideRate = ((_ocDef.WheelRate * _tireDef.TireRate) / (_ocDef.WheelRate + _tireDef.TireRate));

            Damper_Preload = (_damperDef.DamperGasPressure * Math.PI * Math.Pow(_damperDef.DamperShaftDia, 2)) / 4;

            #region Calculating the Preload load due to _springDef and _damperDef
            Combined_PreloadForce = (_springDef.PreloadForce) + Damper_Preload;
            Preload_mm = Combined_PreloadForce / _springDef.SpringRate;
            #endregion


            // Will need to put a logic here to make sure that Wheel deflection is +ve for bump and -ve for rebound. THIS -VE IS ADDED SO THAT WHEEL DEFLECTION REFLECTS BUMP IN THIS CASE AS WHEN CAR IS DROPPED ONLY BUMP WILL 
            if (!MotionExists)
            {
                Fy_Wheel = ((_ocDef.CW));
                Fy_Spring = Fy_Wheel / _scmDef.InitialMR;

                WheelDeflection = ((_vehicleDef.CW[Identifier - 1]) / _ocDef.RideRate);
                SpringDeflection = (Fy_Spring / _springDef.SpringRate)/* - Preload_mm*/;

                #region Calculating the Preload load due to _springDef and _damperDef
                _ocDef.Corrected_SpringDeflection = SpringDeflection - Preload_mm;
                _ocDef.Corrected_WheelDeflection = _ocDef.Corrected_SpringDeflection / _scmDef.InitialMR;
                #endregion

            }
            else if (MotionExists)
            {
                ///<!--Most likely the below line of code is incorrect when the solver is Recalculating for Steering becsuse, the Wheel Deflection passed during Steering is a delta value and not an absolute value as is the case when running the solver for a Motion-->
                Fy_Wheel = ((_ocDef.CW)) /*+*/- (_WheelDeflection[Index] * _ocDef.RideRate);
                Fy_Spring = Fy_Wheel / _scmDef.InitialMR;

                SpringDeflection = (Fy_Spring / _springDef.SpringRate) - Preload_mm;

                if (Index != 0 )
                {
                    //if (!_recalSteering)
                    //{
                    ///<remarks>This IF loop is used to determine wheather the Delta of the Spring deflection should be calculated using the Current and Previous of the Spring Deflection or if it just needs to be assigned. This is because, if the solver is passing
                    ///through for the 2nd time because of Steering then the <paramref name="_WheelDeflection"/>> consists of the deltas of the Wheel Deflection and not the absolute values unlike the case when the solver is solving for motion 
                    ///<seealso cref="InitializeVehicleOutputModel"/> and notice how the <value>WheelDeflection_DiagonalWT_Steering</value> is being calculated. </remarks>
                    SpringDeflection_Prev[Index] = SpringDeflection;
                    SpringDeflection_Delta = SpringDeflection - SpringDeflection_Prev[Index - 1];
                    //}
                    //else SpringDeflection_Delta = SpringDeflection;
                }
                else if (Index == 0)
                {
                    temp_SpringDef_ForIndexZero = SpringDeflection;
                    SpringDeflection_Prev[0] = SpringDeflection;
                    SpringDeflection_Delta = SpringDeflection;
                }

                _ocDef.Corrected_SpringDeflection = SpringDeflection;
                _ocDef.Corrected_WheelDeflection = Fy_Wheel / _ocDef.RideRate /*_ocDef.Corrected_SpringDeflection / _scmDef.InitialMR*/;
                //_ocDef.CW = Fy_Wheel;
                _ocDef.CW_1 = _ocDef.CW; // This code is added so that there always exists a record of what the weight actually was

            }
        }
        #endregion

 #region Calculating Spring Deflection - This method is only used when deflection due to the steering is considered
        public void CalculateSpringDeflection_AfterVehicleModel(List<OutputClass> _oc, int _identfier, double _springDeflection, double _motionRatio, int index)
        {
            _oc[index].DeltaSpringDef_Steering = _springDeflection;
            SpringDeflection_Delta = _oc[index].DeltaSpringDef_Steering;

            if (index == 0)
            {
                WheelDeflection = _springDeflection*_motionRatio;
            }
            else if (index != 0)
            {
                WheelDeflection +=  _springDeflection*_motionRatio;
            }

            //_oc[index].Corrected_WheelDeflection += WheelDeflection;
            //_oc[index].Corrected_SpringDeflection += SpringDeflection_Delta;
        }
        #endregion

        //public void CalculateSpringDeflection_ForSteering_Rear()
        //{

        //}

        public void CalculateSpringDeflection_DiagonalWT_Steering_Rear(List<OutputClass> _oc, int _identiier, int Index)
        {
            ///<remarks>
            ///Might need this class 
            /// </remarks>
        }
        #region Calculating the Spring Deflection in small steps 
        public void CalculateAngleOfRotationOrDamperLength(OutputClass _ocSteps, bool MotionExists, int _springPrevIndex, Damper _damperSteps,bool _recalculateSteering)
        {
            
            J1I = Math.Sqrt(Math.Pow((l_I1x - l_J1x), 2) + (Math.Pow((l_I1y - l_J1y), 2)) + (Math.Pow((l_I1z - l_J1z), 2)));
            JoI = Math.Sqrt(Math.Pow((l_I1x - l_JO1x), 2) + (Math.Pow((l_I1y - l_JO1y), 2)) + (Math.Pow((l_I1z - l_JO1z), 2)));
            J1Jo = Math.Sqrt(Math.Pow((l_J1x - l_JO1x), 2) + (Math.Pow((l_J1y - l_JO1y), 2)) + (Math.Pow((l_J1z - l_JO1z), 2)));

            if (!MotionExists)
            {
                Corrected_SpringDeflection_Loop = _ocSteps.Corrected_SpringDeflection / N; // To calculate the the step size of spring deflection for the given number of steps  
                J2Jo = J1Jo - _ocSteps.Corrected_SpringDeflection;
                J2Jo_Loop = J1Jo - Corrected_SpringDeflection_Loop;
            }
            else if (MotionExists)
            {
                J2Jo = J1Jo - _ocSteps.Corrected_SpringDeflection;
                J2Jo_Loop = J1Jo - SpringDeflection_Delta;


                int SpringPrevIndex = 0;
                if (_springPrevIndex != 0)
                {
                    SpringPrevIndex = _springPrevIndex - 1;
                }

                if (SpringDeflection_Prev[SpringPrevIndex] > SpringDeflection)
                {
                    IsStillinRebound = true;
                }
                else if (SpringDeflection_Prev[SpringPrevIndex] < SpringDeflection)
                {
                    IsStillinRebound = false;
                }
            }

            #region Still determining if this is necessary

            //else if (SpringDeflection_Prev[SpringPrevIndex] == SpringDeflection && _springPrevIndex != 0)
            //{
            //    J2Jo = J1Jo;
            //    J2Jo_Loop = J1Jo;
            //}
            if ((J2Jo > _damperSteps.DamperFreeLength) && (IsStillinRebound))
            {
                J1Jo = J2Jo = J2Jo_Loop = _damperSteps.DamperFreeLength;

            }
            else if ((J2Jo < (_damperSteps.DamperFreeLength - _damperSteps.DamperStroke)) && (!IsStillinRebound)) 
            {
                J1Jo = J2Jo = J2Jo_Loop = (_damperSteps.DamperFreeLength - _damperSteps.DamperStroke);
            }
            #endregion

            _ocSteps.DamperLength = J2Jo_Loop;
            //if (_recalculateSteering)
            //{
            //    _ocSteps.AngleOfRotation += Math.Acos(((Math.Pow(J1I, 2) + Math.Pow(JoI, 2) - Math.Pow(J1Jo, 2)) / (2 * J1I * JoI))) - Math.Acos(((Math.Pow(J1I, 2) + Math.Pow(JoI, 2) - Math.Pow(J2Jo_Loop, 2)) / (2 * J1I * JoI))); 
            //}
            //else
            //{
                _ocSteps.AngleOfRotation = Math.Acos(((Math.Pow(J1I, 2) + Math.Pow(JoI, 2) - Math.Pow(J1Jo, 2)) / (2 * J1I * JoI))) - Math.Acos(((Math.Pow(J1I, 2) + Math.Pow(JoI, 2) - Math.Pow(J2Jo_Loop, 2)) / (2 * J1I * JoI)));
            //}

            if (Double.IsNaN(_ocSteps.AngleOfRotation) || Double.IsInfinity(_ocSteps.AngleOfRotation))
            {
                _ocSteps.AngleOfRotation = 0;
            }
        }
        #endregion

        #region ---This method is no longer required as the Steering Angles are calculated based on the Number of Universal Joints---
        public void CalculateDCamberDToe_Steering(OutputClass _ocDCamberDToe_Steering, Vehicle _vehicleDCamberDToe_Steering, int Index)
        {
            //_ocDCamberDToe_Steering.waOP.WheelIsSteered = _vehicleDCamberDToe_Steering.vehicle_Motion.SteeringExists;
            //if (Index != 0)
            //{
            //    deltaSteeringWheel = _vehicleDCamberDToe_Steering.vehicle_Motion.Final_WheelSteerY[Index] - _vehicleDCamberDToe_Steering.vehicle_Motion.Final_WheelSteerY[Index - 1];
            //}

            //dCamber_Steering = _ocDCamberDToe_Steering.KPI * (1 - Math.Cos(deltaSteeringWheel * (Math.PI / 180))) - (_ocDCamberDToe_Steering.Caster * Math.Sin(deltaSteeringWheel * (Math.PI / 180)));
            //dToe_Steering = deltaSteeringWheel * (Math.PI / 180);
        }
        #endregion

        #region Method to Calculate New Camber and Toe using the delta of Camber and Toe - Used only for Steering 
        public void CalculateNewCamberAndToe_Steering(List<OutputClass> _ocNewCamberToe, int index, Vehicle _vehicleNewCamberToe_Steering)
        {
            _ocNewCamberToe[index].waOP.StaticCamber = dCamber_Steering + _ocNewCamberToe[index - 1].waOP.StaticCamber;

            _ocNewCamberToe[index].waOP.StaticToe = dToe_Steering + _ocNewCamberToe[index - 1].waOP.StaticToe;
        } 
        #endregion

        #region Calculating the Camber and Toe for the Rear OR for the front if there is no Steering Input
        private double AssignDirection_Toe(double _toe, int _identifier, double Check1z, double Check2Z)
        {
            
            if (Double.IsNaN(_toe) || Double.IsInfinity(_toe))
            {
                _toe = 0;
            }

            if (_identifier == 1 || _identifier == 3)
            {
                if (Check1z < Check2Z)
                {
                    _toe *= /*-*/1;
                }
                else if (Check1z > Check2Z)
                {
                    _toe *= -1;
                }
            }
            else if (_identifier == 2 || _identifier == 4)
            {
                if (Check1z < Check2Z)
                {
                    _toe *= -1;
                }
                else if (Check1z > Check2Z)
                {
                    _toe *= /*-*/1;
                }
            }
            return _toe;

        }
        private double AssignDirection_Camber(double _camber, int _identifier, double Check1Y, double Check2Y)
        {
            if (Double.IsNaN(_camber) || Double.IsInfinity(_camber))
            {
                _camber = 0;
            }

            if ((_identifier == 1 || _identifier == 3))
            {
                if ((Check1Y < Check2Y))
                {
                    _camber *= /*-*/1;
                }
                else if ((Check1Y > Check2Y))
                {
                    _camber *= -1;
                }
            }
            else if (_identifier == 2 || _identifier == 4)
            {
                if (Check1Y < Check2Y)
                {
                    _camber *= -1;
                }
                else if (Check1Y > Check2Y)
                {
                    _camber *= /*-*/1;
                }
            }

            return _camber;


        }


        public void CalculatenewCamberAndToe_Rear(List<OutputClass> _ocNewCamberToe, int dummy2, Vehicle _vehicleCamberToe, int _identifier)
        {

            K2L2_Toe = Math.Sqrt(Math.Pow((_ocNewCamberToe[dummy2].scmOP.L1x - _ocNewCamberToe[dummy2].scmOP.K1x), 2) + Math.Pow((_ocNewCamberToe[dummy2].scmOP.L1z - _ocNewCamberToe[dummy2].scmOP.K1z), 2));
            K2L2_Camber = Math.Sqrt(Math.Pow((_ocNewCamberToe[dummy2].scmOP.L1x - _ocNewCamberToe[dummy2].scmOP.K1x), 2) + Math.Pow((_ocNewCamberToe[dummy2].scmOP.L1y - _ocNewCamberToe[dummy2].scmOP.K1y), 2));

            if (_vehicleCamberToe.sc_FL.SuspensionMotionExists)
            {
                K2L2_ToePrev = Math.Sqrt(Math.Pow((_ocNewCamberToe[dummy2 - 1].scmOP.L1x - _ocNewCamberToe[dummy2 - 1].scmOP.K1x), 2) + Math.Pow((_ocNewCamberToe[dummy2 - 1].scmOP.L1z - _ocNewCamberToe[dummy2 - 1].scmOP.K1z), 2));
                K2L2_CamberPrev = Math.Sqrt(Math.Pow((_ocNewCamberToe[dummy2 - 1].scmOP.L1x - _ocNewCamberToe[dummy2 - 1].scmOP.K1x), 2) + Math.Pow((_ocNewCamberToe[dummy2 - 1].scmOP.L1y - _ocNewCamberToe[dummy2 - 1].scmOP.K1y), 2));

                dToe = (Math.Acos((((_ocNewCamberToe[dummy2 - 1].scmOP.L1x - _ocNewCamberToe[dummy2 - 1].scmOP.K1x) * (_ocNewCamberToe[dummy2].scmOP.L1x - _ocNewCamberToe[dummy2].scmOP.K1x)) + ((_ocNewCamberToe[dummy2 - 1].scmOP.L1z - _ocNewCamberToe[dummy2 - 1].scmOP.K1z) * (_ocNewCamberToe[dummy2].scmOP.L1z - _ocNewCamberToe[dummy2].scmOP.K1z))) / (K2L2_ToePrev * K2L2_Toe)));
                dCamber = (Math.Acos((((_ocNewCamberToe[dummy2 - 1].scmOP.L1x - _ocNewCamberToe[dummy2 - 1].scmOP.K1x) * (_ocNewCamberToe[dummy2].scmOP.L1x - _ocNewCamberToe[dummy2].scmOP.K1x)) + ((_ocNewCamberToe[dummy2 - 1].scmOP.L1y - _ocNewCamberToe[dummy2 - 1].scmOP.K1y) * (_ocNewCamberToe[dummy2].scmOP.L1y - _ocNewCamberToe[dummy2].scmOP.K1y))) / (K2L2_CamberPrev * K2L2_Camber)));

                dCamber = AssignDirection_Camber(dCamber, _identifier, _ocNewCamberToe[dummy2 - 1].scmOP.L1y - _vehicleCamberToe.OutputOrigin_y, _ocNewCamberToe[dummy2].scmOP.L1y);
                dToe = AssignDirection_Toe(dToe, _identifier, _ocNewCamberToe[dummy2 - 1].scmOP.L1z - _vehicleCamberToe.OutputOrigin_z, _ocNewCamberToe[dummy2].scmOP.L1z);

                _ocNewCamberToe[dummy2].waOP.StaticCamber = (dCamber + _ocNewCamberToe[dummy2 - 1].waOP.StaticCamber);
                _ocNewCamberToe[dummy2].waOP.StaticToe = (dToe + _ocNewCamberToe[dummy2 - 1].waOP.StaticToe);
            }
            else if (!_vehicleCamberToe.sc_FL.SuspensionMotionExists)
            {
                ///<remarks>There is some problem with the below code which I don't have time to debugg. The value of dCamber and dToe is very very high and not reasonable. So for now I am adopting a very stupid stopgap method 
                ///and making a Degre</remarks>
                dToe = (Math.Acos((((SpindleX1) * (_ocNewCamberToe[dummy2].scmOP.L1x - _ocNewCamberToe[dummy2].scmOP.K1x)) + ((SpindleZ1) * (_ocNewCamberToe[dummy2].scmOP.L1z - _ocNewCamberToe[dummy2].scmOP.K1z))) / (KL_Toe * K2L2_Toe)));
                dCamber = (Math.Acos((((SpindleX1) * (_ocNewCamberToe[dummy2].scmOP.L1x - _ocNewCamberToe[dummy2].scmOP.K1x)) + ((SpindleY1) * (_ocNewCamberToe[dummy2].scmOP.L1y - _ocNewCamberToe[dummy2].scmOP.K1y))) / (KL_Camber * K2L2_Camber)));


                //dToe *= (Math.PI / 180);
                //dCamber *= (Math.PI / 180);

                dCamber = AssignDirection_Camber(dCamber, _identifier, Ly - _vehicleCamberToe.OutputOrigin_y, _ocNewCamberToe[dummy2].scmOP.L1y);
                dToe = AssignDirection_Toe(dToe, _identifier, Lz - _vehicleCamberToe.OutputOrigin_z, _ocNewCamberToe[dummy2].scmOP.L1z);

                // Don't know why I have added '-' sign for dCamber and dToe. Need to figure that out while debuggin a case where the car is on the stands and brought down 
                _ocNewCamberToe[dummy2].waOP.StaticCamber = /*-*/ dCamber + _ocNewCamberToe[dummy2].waOP.StaticCamber;
                _ocNewCamberToe[dummy2].waOP.StaticToe = /*-*/ dToe + _ocNewCamberToe[dummy2].waOP.StaticToe;

             }


        }
        #endregion

        private double GetSteeringRatio(int Index, WheelAlignment _waSteeringRatio)
        {
            return 0;
        }

        #region Calculating the Angles of Steering System's Input and Output Shafts based on the Steering Wheel Turn Angle - 2 Universal Joints
        private void GetAngles_SteeringSystemShafts(SuspensionCoordinatesMaster _scmUJoint, OutputClass _ocUJoint)
        {
            ///<summary>
            ///Vector joining the 1st Universal Joint to thw Steering Wheel Chassis Mount
            /// </summary>
            Vector<double> UV1STC1 = Vector<double>.Build.Dense(new double[]
            {
                (_scmUJoint.UV1x-_scmUJoint.STC1x),(_scmUJoint.UV1y-_scmUJoint.STC1y),(_scmUJoint.UV1z-_scmUJoint.STC1z)
            });

            ///<summary>
            ///Vector joining the 2nd Unviersal Joint to the Pinion
            /// </summary>
            Vector<double> PIN1UV2 = Vector<double>.Build.Dense(new double[]
                {
                    (_scmUJoint.Pin1x-_scmUJoint.UV2x),(_scmUJoint.Pin1y-_scmUJoint.UV2y),(_scmUJoint.Pin1z-_scmUJoint.UV2z)
                });

            ///<summary>
            ///Vector Joining the 2 Unversal Joints
            /// </summary>
            Vector<double> UV2UV1 = Vector<double>.Build.Dense(new double[]
                {
                    (_scmUJoint.UV2x - _scmUJoint.UV1x),(_scmUJoint.UV2y - _scmUJoint.UV1y),(_scmUJoint.UV2z - _scmUJoint.UV1z)
                });

            ///<summary>
            ///Dot Products of the required Vectors
            /// </summary>
            double Dot_InputOuput = UV1STC1.DotProduct(PIN1UV2);
            double Dot_InputIntermediate = UV1STC1.DotProduct(UV2UV1);
            double Dot_IntermediateOutput = UV2UV1.DotProduct(PIN1UV2);


            ///<summary>
            ///Magnitudes of the required Vectors
            /// </summary>
            double Mag_UV1STC1 = UV1STC1.L2Norm();
            double Mag_PIN1UV2 = PIN1UV2.L2Norm();
            double Mag_UV2UV1 = UV2UV1.L2Norm();

            ///<summary>
            ///Angles between the Vectors of interest
            /// </summary>
            /// 
            ///<remarks>
            ///Ideally the Angle between the Input and Output Shaft should be 0. Only then can we assure that the UV Joints in series are 
            /// </remarks>
            Angle_InputOutputShaft = Math.Acos(Dot_InputOuput / (Mag_PIN1UV2 * Mag_UV1STC1));
            _ocUJoint.Angle_InputOutputShafts = Angle_InputOutputShaft;
            ///<remarks>
            ///The angle between the Input - Intermediate and the Intermediate - Output Shaft shoudle be equal (Obviously it will be equal if the Angle between the Input and Output Shaft is equal)
            /// </remarks>
            Angle_InputIntermediateShaft = Math.Acos(Dot_InputIntermediate / (Mag_UV1STC1 * Mag_UV2UV1));
            _ocUJoint.Angle_InputIntermediateShaft = Angle_InputIntermediateShaft;
            Angle_IntermediateOutputShaft = Math.Acos(Dot_IntermediateOutput / (Mag_UV2UV1 * Mag_PIN1UV2));
            _ocUJoint.Angle_IntertermediateOutputShaft = Angle_IntermediateOutputShaft;

        }
        #endregion

        #region Calculating the Angle of the Input and Output Shafts based on the Steering Wheel Turn Angle - 1 Universal Joint
        private double PinionRotationAngle(int _noOfCouplings, double _deltaSteeringWheel)
        {
            double _deltaAngle_Pinion;
            if (_noOfCouplings == 2)
            {
                ///<param name="deltaAngle_InternmediateShaft">
                ///Angle of Rotation of the Intermediate Shaft which is caused by the rotation of the Steering wheel 
                /// </param>
                Angle_InternmediateShaft = Math.Atan2(Math.Sin(_deltaSteeringWheel), (Math.Cos(Angle_InputIntermediateShaft) * Math.Cos(_deltaSteeringWheel)));
                _deltaAngle_Pinion = Math.Atan2(Math.Sin(Angle_InternmediateShaft + (Math.PI / 2)), (Math.Cos(Angle_IntermediateOutputShaft) * Math.Cos(Angle_InternmediateShaft + (Math.PI / 2))));
                _deltaAngle_Pinion -= Math.PI / 2;
                if (_deltaSteeringWheel < Math.PI / 2)
                {
                    //double deltaAngle_InternmediateShaft = Math.Atan2(Math.Sin(_deltaSteeringWheel), (Math.Cos(Angle_InputIntermediateShaft) * Math.Cos(_deltaSteeringWheel)));
                    //deltaAngle_Pinion = Math.Atan2(Math.Sin(deltaAngle_InternmediateShaft), (Math.Cos(Angle_InputIntermediateShaft) * Math.Cos(deltaAngle_InternmediateShaft)));
                }
                else if (_deltaSteeringWheel == Math.PI / 2)
                {

                }
                else if (_deltaSteeringWheel > Math.PI / 2)
                {

                }
            }

            else
            {
                //double deltaAngle_InternmediateShaft = Math.Atan2(Math.Sin(_deltaSteeringWheel), (Math.Cos(Angle_InputOutputShaft) * Math.Cos(_deltaSteeringWheel)));
                _deltaAngle_Pinion = Math.Atan2(Math.Sin(_deltaSteeringWheel), (Math.Cos(Angle_InputOutputShaft) * Math.Cos(_deltaSteeringWheel)));
            }
            return _deltaAngle_Pinion;
        }
        #endregion

        #region Calculating the Delta of the TOE Angle based on the previous and current steering Wheel Angle which is converted to the Pinion Angle
        public void ToeRod_Steering(OutputClass _ocM, int Index, Vehicle _vehicleM, WheelAlignment _waM)
        {
            ///<summary>
            ///This method calculates the delta of the TOE angle based on the previous and current steering wheel angle which is converted to the pinion angle. 
            ///This method then calculates the new coordinate of the Outboard and Inboard Toe Link points
            /// </summary>

            _ocM.waOP.WheelIsSteered = _vehicleM.vehicle_Motion.SteeringExists;

            ///<param name="deltaSteeringWheel">
            ///Delta of the Steering Angle which is provided as input by the user
            /// </param>
            deltaSteeringWheel = ((_vehicleM.vehicle_Motion.Final_WheelSteerY[Index] - _vehicleM.vehicle_Motion.Final_WheelSteerY[Index - 1]));
            _ocM.Angle_Steering = _vehicleM.vehicle_Motion.Final_WheelSteerY[Index] * (Math.PI / 180);

            ///<param name ="Angle_InputIntermediateShaft" || "Angle_InputOutputShafft">
            ///The Angle between the Input and Output shaft (for 1 coupling system) or the Input and Intermediate Shaft (for 2 coupling system
            /// </param>
            GetAngles_SteeringSystemShafts(_vehicleM.sc_FL, _ocM);

            ///<param name = "deltaAnglePinion">
            ///The pinion angle of rotation is calculated using the data form the input steering wheel angle, the Number of Couplings used in the Steering System and the angles between the shafts of the system
            /// </param>
            ///<remarks>
            /// If there is only 1 UV JOint in the system, then I cannot calculate the delta of steering angle and then use it to calculate the Pinion angle.
            /// This is because, in the 2 UV joint case, the Input and Output shaft angles are almost equal (because 2 UV Joints are used, the fluctuating output at the intermediate shaft start is the flucatuating input at the intermediate shaft's end which causes a constant output)
            /// and hence simply sending in the delta of the Steering Wheel Angle gives the delta of the Pinion Angle
            /// In the case of 1 UV Joint, the output angle is non linear. It increases first progressively till 45 deg and then increases degressively from 45 to 90 degree. Hence if I pass a delta value of the steering wheel angle, it will almost always be something withing 
            /// 45 degree and hence the true nature of the UV Joint will not be exposed. 
            /// 
            /// </remarks>
            if (_vehicleM.sc_FL.NoOfCouplings == 2)
            {
                deltaAngle_Pinion = PinionRotationAngle(_vehicleM.sc_FL.NoOfCouplings, deltaSteeringWheel * (Math.PI / 180));
                _ocM.Angle_Pinion = PinionRotationAngle(_vehicleM.sc_FL.NoOfCouplings, _ocM.Angle_Steering);
                _ocM.Angle_Intermediate = Angle_InternmediateShaft;
            }
            else
            {
                double CurentSteeringWheel = _vehicleM.vehicle_Motion.Final_WheelSteerY[Index];
                double CurrentPinionAngle = PinionRotationAngle(_vehicleM.sc_FL.NoOfCouplings, CurentSteeringWheel * (Math.PI / 180));
                _ocM.Angle_Pinion = CurrentPinionAngle;

                double PreviousSteeringWheel = _vehicleM.vehicle_Motion.Final_WheelSteerY[Index - 1];
                double PreviousPinionAngle = PinionRotationAngle(_vehicleM.sc_FL.NoOfCouplings, PreviousSteeringWheel * (Math.PI / 180));

                deltaAngle_Pinion = CurrentPinionAngle - PreviousPinionAngle;
            }
            //_ocM.scmOP.NoOfCouplings = _vehicleM.sc_FL.NoOfCouplings;

            ///<remarks>
            ///Multiplied with 360 because now I take input in the form of mm of rack travel per revolution of steering wheel 
            /// </remarks>
            M1M2 = (deltaAngle_Pinion * (180 / Math.PI)) * (_waM.SteeringRatio / 360);

            dToe_Steering = Math.Atan(M1M2 / M1K1);

            dCamber_Steering = _ocM.KPI * (1 - Math.Cos(dToe_Steering)) - (_ocM.Caster * Math.Sin(dToe_Steering));

            ///<remarks>
            ///Changing the coordinates of the Steering Link 
            /// </remarks>
            _ocM.scmOP.M1x += M1M2;
            _ocM.scmOP.N1x += M1M2;
        }
        #endregion

        #region Methods to RECALCULATE the Suspension Coordinates using the newly obtained Spindle End Coordinates calculate due to Steering. NOTE - SOME METHODS ARE NOT USED BECAUSE THEY ARE RESULTING IN ERRENEOUS RESULTS
        /// <summary>
        /// The Contact Patch coordinates are recalculated using the new Spindle End and Steering Upright Coordinates
        /// </summary>
        /// <param name="_ocW">Object of OUtput Class</param>
        public void ContactPatch_Steering(OutputClass _ocW)
        {
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W' after the steering rotation has been applied
            // Vectors used -> W'M', W'K' & W'L'
            double XW1 = 0, YW1 = 0, ZW1 = 0;
            //double XK1 = 0, YK1 = 0, ZK1 = 0;

            QuadraticEquationSolver.Solver(l_W1x, l_W1y, l_W1z, l_M1x, l_M1y, l_M1z, 0, l_K1x, l_K1y, l_K1z, L1x, L1y, L1z, _ocW.scmOP.M1x, _ocW.scmOP.M1y, _ocW.scmOP.M1z, _ocW.scmOP.K1x, _ocW.scmOP.K1y, _ocW.scmOP.K1z, _ocW.scmOP.L1x, _ocW.scmOP.L1y, _ocW.scmOP.L1z, l_E1y,
                                                                                                                                                                                                                                                 true, out XW1, out YW1, out ZW1);
            _ocW.scmOP.W1x = XW1;
            _ocW.scmOP.W1y = YW1;
            _ocW.scmOP.W1z = ZW1;
        }
        

        /// <summary>
        /// This method is used to calculate the Wheel Centre Coordinates after the Steering has been performed.
        /// IMPORTANT - This method is called only in the 2nd iteration
        /// </summary>
        /// <param name="_ocK">Object of the Output Class</param>
        /// <param name="_VehicleK">Object of the Vehicle Class</param>
        /// <param name="_index">Index</param>
        public void WheelCentre_Steering(List<OutputClass> _ocK, Vehicle _VehicleK, int _index)
        {
            _ocK[_index].scmOP.K1x = _ocK[_index - 1].scmOP.K1x;
            _ocK[_index].scmOP.K1y = _ocK[_index - 1].scmOP.K1y - (_ocK[_index].TireDeflection - _ocK[_index - 1].TireDeflection);
            _ocK[_index].scmOP.K1z = _ocK[_index - 1].scmOP.K1z;
        }


        /// <summary>
        /// This method is exclusive to the front and is used to recalculate the UBJ and the LBJ coordinates and ONLY during the 2nd pass and ONLy for the condition of Steering
        /// </summary>
        /// <param name="_ocEF">Object of the Output Class</param>
        public void UBJandLBJ_Steering(List<OutputClass> _ocEF, int i)
        {
            
            double XE1, YE1, ZE1;
            QuadraticEquationSolver.Solver(_ocEF[i - 1].scmOP.E1x, _ocEF[i - 1].scmOP.E1y, _ocEF[i - 1].scmOP.E1z, _ocEF[i - 1].scmOP.L1x, _ocEF[i - 1].scmOP.L1y, _ocEF[i - 1].scmOP.L1z, 0,
                                           _ocEF[i - 1].scmOP.K1x, _ocEF[i - 1].scmOP.K1y, _ocEF[i - 1].scmOP.K1z,
                                           _ocEF[i - 1].scmOP.W1x, _ocEF[i - 1].scmOP.W1y, _ocEF[i - 1].scmOP.W1z, _ocEF[i].scmOP.L1x, _ocEF[i].scmOP.L1y, _ocEF[i].scmOP.L1z, _ocEF[i].scmOP.K1x, _ocEF[i].scmOP.K1y, _ocEF[i].scmOP.K1z,
                                           _ocEF[i].scmOP.W1x, _ocEF[i].scmOP.W1y, _ocEF[i].scmOP.W1z, _ocEF[i].scmOP.F1y, true, out XE1, out YE1, out ZE1);

            _ocEF[i].scmOP.E1x = XE1;
            _ocEF[i].scmOP.E1y = YE1;
            _ocEF[i].scmOP.E1z = ZE1;

            double XF1, YF1, ZF1;
            QuadraticEquationSolver.Solver(_ocEF[i - 1].scmOP.F1x, _ocEF[i - 1].scmOP.F1y, _ocEF[i - 1].scmOP.F1z, _ocEF[i - 1].scmOP.L1x, _ocEF[i - 1].scmOP.L1y, _ocEF[i - 1].scmOP.L1z, 0,
                                           _ocEF[i - 1].scmOP.K1x, _ocEF[i - 1].scmOP.K1y, _ocEF[i - 1].scmOP.K1z,
                                           _ocEF[i - 1].scmOP.E1x, _ocEF[i - 1].scmOP.E1y, _ocEF[i - 1].scmOP.E1z, _ocEF[i].scmOP.L1x, _ocEF[i].scmOP.L1y, _ocEF[i].scmOP.L1z, 
                                           _ocEF[i].scmOP.K1x, _ocEF[i].scmOP.K1y, _ocEF[i].scmOP.K1z,
                                           _ocEF[i].scmOP.E1x, _ocEF[i].scmOP.E1y, _ocEF[i].scmOP.E1z, _ocEF[i].scmOP.W1y, false, out XF1, out YF1, out ZF1);


            _ocEF[i].scmOP.F1x = XF1;
            _ocEF[i].scmOP.F1y = YF1;
            _ocEF[i].scmOP.F1z = ZF1;

        }

        public void Pushrod_Steering(List<OutputClass> _ocG, int i)
        {
            double XG1, YG1, ZG1;
            QuadraticEquationSolver.Solver(_ocG[i - 1].scmOP.G1x, _ocG[i - 1].scmOP.G1y, _ocG[i - 1].scmOP.G1z, _ocG[i - 1].scmOP.F1x, _ocG[i - 1].scmOP.F1y, _ocG[i - 1].scmOP.F1z, 0,
                                           _ocG[i - 1].scmOP.E1x, _ocG[i - 1].scmOP.E1y, _ocG[i - 1].scmOP.E1z,
                                           _ocG[i - 1].scmOP.K1x, _ocG[i - 1].scmOP.K1y, _ocG[i - 1].scmOP.K1z, _ocG[i].scmOP.F1x, _ocG[i].scmOP.F1y, _ocG[i].scmOP.F1z, _ocG[i].scmOP.E1x, _ocG[i].scmOP.E1y, _ocG[i].scmOP.E1z,
                                           _ocG[i].scmOP.K1x, _ocG[i].scmOP.K1y, _ocG[i].scmOP.K1z, 0, false, out XG1, out YG1, out ZG1);

            _ocG[i].scmOP.G1x = XG1;
            _ocG[i].scmOP.G1y = YG1;
            _ocG[i].scmOP.G1z = ZG1;

        }
        #endregion

        public void SetupChange_PrimaryInvoker(SetupChange_CornerVariables _FlCV, SetupChange_CornerVariables _FrCV, SetupChange_CornerVariables _RlCV, SetupChange_CornerVariables _RrCV, Vehicle _Vehicle)
        {
            ///<summary>Assinging the <see cref="SetupChange_CornerVariables"/> class objects of each corner</summary>
            _Vehicle.oc_FL[0].sccvOP = _FlCV;
            _Vehicle.oc_FR[0].sccvOP = _FrCV;
            _Vehicle.oc_RL[0].sccvOP = _RlCV;
            _Vehicle.oc_RR[0].sccvOP = _RrCV;

            AssignOrientation_CamberToe(ref _Vehicle.oc_FL[0].sccvOP.deltaCamber, ref _Vehicle.oc_FL[0].sccvOP.deltaToe, _Vehicle.oc_FL[0].sccvOP.deltaCamber, _Vehicle.oc_FL[0].sccvOP.deltaToe, 1);
            AssignOrientation_CamberToe(ref _Vehicle.oc_FR[0].sccvOP.deltaCamber, ref _Vehicle.oc_FR[0].sccvOP.deltaToe, _Vehicle.oc_FR[0].sccvOP.deltaCamber, _Vehicle.oc_FR[0].sccvOP.deltaToe, 1);
            AssignOrientation_CamberToe(ref _Vehicle.oc_RL[0].sccvOP.deltaCamber, ref _Vehicle.oc_RL[0].sccvOP.deltaToe, _Vehicle.oc_RL[0].sccvOP.deltaCamber, _Vehicle.oc_RL[0].sccvOP.deltaToe, 1);
            AssignOrientation_CamberToe(ref _Vehicle.oc_RR[0].sccvOP.deltaCamber, ref _Vehicle.oc_RR[0].sccvOP.deltaToe, _Vehicle.oc_RR[0].sccvOP.deltaCamber, _Vehicle.oc_RR[0].sccvOP.deltaToe, 1);

            ///<summary>Bringing about the Setup Changes in each of the corners and assigning the <see cref="SetupChangeDatabase"/>objects to the corners as well</summary>
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_FL[0].sccvOP, _Vehicle.oc_FL, 1);
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_FR[0].sccvOP, _Vehicle.oc_FR, 2);
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_RL[0].sccvOP, _Vehicle.oc_RL, 3);
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_RR[0].sccvOP, _Vehicle.oc_RR, 4);

        }

        /// <summary>
        /// Public method to initialize the <see cref="SetupChangeDatabase"/> object of this class and assign the <see cref="OutputClass.scmOP"/>'s coordinate values to the local variables of this class
        /// This method will also need to be called by the <seealso cref="SetupChange_ClosedLoopSolver"/> Class while performing the Closed Loop Simulation
        /// </summary>
        /// <param name="_requestedChanges"></param>
        /// <param name="_oc"></param>
        public void SetupChange_InitializeSetupChange(SetupChange_CornerVariables _requestedChanges, OutputClass _oc)
        {
            ///<summary>Initializing the SetupChangeDatabase's Master Object</summary>
            SetupChange_DB_Master = new SetupChangeDatabase();

            ///<summary>Assigning the <see cref="SolverMasterClass"/> Class' local variables with the <see cref="OutputClass.scmOP"/> values</summary>
            AssignLocalCoordinateVariables_FixesPoints(_oc.scmOP);
            AssignLocalCoordinateVariables_MovingPoints(_oc.scmOP);
            L1x = _oc.scmOP.L1x; L1y = _oc.scmOP.L1y; L1z = _oc.scmOP.L1z;

            ///<summary>Initializing the Points and Vectors of the Wheel Assembly</summary>
            SetupChange_DB_Master.InitializePointsAndVectors(_oc.scmOP);
        }

        public void SetupChange_AssignNewSetupValues(List<OutputClass> _oc, SetupChange_CornerVariables _requestedChanges)
        {
            ///<summary>Calculating the Final Value of Camber that is requested by the User. No change in parameter's value if requested change is 0</summary>
            Angle deltaCamber = new Angle(_requestedChanges.deltaCamber, AngleUnit.Degrees);
            _oc[0].waOP.StaticCamber += deltaCamber.Radians;

            ///<summary>Calculating the Final Value of Toe that is requested by the User. No change in parameter's value if requested change is 0</summary>
            Angle deltaToeReq = new Angle(_requestedChanges.deltaToe, AngleUnit.Degrees);
            _oc[0].waOP.StaticToe += deltaToeReq.Radians;

            ///<summary>Calculating the Final Value of Caster that is requested by the User. No change in parameter's value if requested change is 0</summary>
            _oc[0].Caster += _requestedChanges.deltaCaster;
            
            ///<summary>Calculating the Final Value of KPI that is requested by the User. No change in parameter's value if requested change is 0</summary>
            _oc[0].KPI += _requestedChanges.deltaKPI;
            
            ///<summary>Calculating the Final Value of Ride Height that is requested by the User. No change in parameter's value if requested change is 0</summary>
            _oc[0].FinalRideHeight += _requestedChanges.deltaRideHeight;

        }

        /// <summary>
        /// Base Invoker method to do initialization work and then invoke the remaining SetupChange Methods
        /// </summary>
        /// <param name="_dCamber">Change of Camber requested by the User in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_dToe">Change of Toe requested by the User in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_dKPI">Change of KPI requested by the User in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_dCaster">Change of Caster requested by the User in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_dRideHeight">Change of Ridhe Height requested by the User in mm</param>
        private void SetupChange_InvokeChangeSolvers(SetupChange_CornerVariables _RequestedChanges, List<OutputClass> _Oc, int _Identifier)
        {
            ///<summary>Initializing the <see cref="SetupChangeDatabase"/> object of this class and assigning the <see cref="OutputClass.scmOP"/>'s coordinate values to the local variables of this class</summary>
            SetupChange_InitializeSetupChange(_RequestedChanges, _Oc[0]);
            
            ///<remarks>Constructing the <see cref="SetupChange_ClosedLoopSolver"/> object before calling the <see cref="SetupChange_AssignNewSetupValues(List{OutputClass}, SetupChange_CornerVariables)"/> so that the static values of Camber, Caster, Toe etc are stored in the 
            ///first posotion of the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> and other lists. This way the minute I make the first pass through the <see cref="SetupChange_CamberChange(double, OutputClass, int, int, bool)"/> or any other Setup Change Method, the 2nd position 
            ///of the lists have the delta values 
            /// </remarks>
            //SetupChange_CLS = new SetupChange_ClosedLoopSolver(this, _Oc, ref SetupChange_DB_Master.SetupChangeOPDictionary);

            ///<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            SetupChange_AssignNewSetupValues(_Oc, _RequestedChanges);

            SetupChange_CLS = new SetupChange_ClosedLoopSolver(this, _Oc, ref SetupChange_DB_Master.SetupChangeOPDictionary);

            ///<summary>The IF loops below decide which Parameter change is the starting point for the Closed Loop Solve</summary>
            if (_RequestedChanges.deltaCamber != 0)
            {
                ///<remarks>Now the Camber Change has been established as the starting point for the Closed Loop </remarks>

                ///<summary>Invoking the Camber Change method</summary>
                ///<remarks>1 is passed for the Index in the line below because during initialization "i-1" is done to get the previous term</remarks>
                SetupChange_CamberChange(_RequestedChanges.deltaCamber, _Oc[0], 1, 1, false);

                SetupChange_CLS.GetCurrentChange(SetupChange_ClosedLoopSolver.CurrentChange.Camber);
            }
            else if (_RequestedChanges.deltaToe != 0)
            {

                SetupChange_ToeChange(_RequestedChanges.deltaToe, _Oc[0], 1, 1, false);
            }
            else if (_RequestedChanges.deltaCaster!= 0)
            {
                

                SetupChange_CasterChange(_RequestedChanges.deltaCaster, _Oc[0], 1, 1, false);
            }
            else if (_RequestedChanges.deltaKPI!= 0)
            {
                

                SetupChange_KPIChange(_RequestedChanges.deltaKPI, _Oc[0], 1, 1, false);
            }
            else if (_RequestedChanges.deltaRideHeight!= 0)
            {
                
            }

            SetupChange_AssignSetupChangeDatabase(_Identifier);

        }

        /// <summary>
        /// Method to simply assign the <see cref="SetupChangeDatabase"/> Master Object to the Vehicle's Corner Object
        /// </summary>
        /// <param name="_identifier"></param>
        private void SetupChange_AssignSetupChangeDatabase(int _identifier)
        {
            if (_identifier == 1)
            {
                SetupChange_DB_FL = null;
                SetupChange_DB_FL = SetupChange_DB_Master;
            }
            else if (_identifier == 2)
            {
                SetupChange_DB_FR = null;
                SetupChange_DB_FR = SetupChange_DB_Master;
            }
            else if (_identifier == 3)
            {
                SetupChange_DB_RL = null;
                SetupChange_DB_RL = SetupChange_DB_Master;
            }
            else if (_identifier == 4)
            {
                SetupChange_DB_RR = null;
                SetupChange_DB_RR = SetupChange_DB_Master;
            }
        }

        /// <summary>
        /// Method to calculate the Camber change requested by the user as a Setup Change. This method shall also calculate all the other things which chnge with the camber 
        /// </summary>
        /// <param name="_dCamber">Change in camber requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupCamber">Object of the Output Class to store the new value of Camber</param>
        /// <param name="_indexSteeringAxis"></param>
        /// <param name="_indexWheelSpindle"></param>
        /// <param name="_undoChange">This is a boolean which determines if the method is being called to ACTUALLY Make a Setup change or if it is being called to UNDO a change caused by a previous Setup Change</param>
        internal void SetupChange_CamberChange(double _dCamber, OutputClass _ocSetupCamber, int _indexWheelSpindle, int _indexSteeringAxis, bool _undoChange)
        {
            ///<summary>Calculating the new camber </summary>
            Angle dCamber = new Angle(_dCamber, AngleUnit.Degrees);

            ///<remarks>Adding a Label here so I can createa Loop without using a For Statement. Label placed in the 2nd line so that the <see cref="dCamber"/> Angle gets the user requested OR reversal value only once. All other rotations should be by angle <see cref="ReCheckCamber()"/></remarks>
            Start:

            //_ocSetupCamber.waOP.StaticCamber += dCamber.Radians;
            SetupChange_DB_Master.SetupChangeOPDictionary["Camber"] = dCamber.Radians;

            ///<summary>Rotating the Wheel Assembyly Points and Vectors using the <see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> </summary>
            RotateWheelAssemblyComponents((SetupChange_DB_Master.SteeringAxis.PerpAlongZ.Mid), dCamber, _ocSetupCamber, _indexWheelSpindle);
            ///<remarks>
            ///The method below is no longer needed because the Wheel Spindle is Translated by an amount equal to the 
            ///contact patch displacement inside the <see cref="RotateWheelAssemblyComponents(Line, Angle, OutputClass)"/>
            ///</remarks>

            ///<summary>
            ///Checking if performing the rotation of the <see cref="SetupChangeDatabase.WheelSpindle"/>by the angle <see cref="dCamber"/> about <see cref="SetupChangeDatabase.SteeringAxis.PerpAlongZ"/> 
            ///ACTUALLY resulted in getting a Camber Angle of <see cref="dCamber"/>
            ///IF not then <see cref="SetupChange_CamberChange(double, OutputClass, int, int)"/> is performed again 
            ///</summary>
            Angle checkCamber = ReCheckCamber();
            //AssignCamberDelta(checkCamber);
            double checker;
            if (!_undoChange) { checker = dCamber.Radians - checkCamber.Radians; }
            else { checker = checkCamber.Radians; }

            AssignCamberDelta(new Angle(checker, AngleUnit.Radians));

            if (Math.Abs(checker) > 0.009)
            {
                //AssignCamberDelta(checkCamber);
                dCamber = checkCamber;
                goto Start;
            }




            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight(_indexWheelSpindle);

            ///<summary>Calculating the new coordinates of the UBJ and the LBJ using the <see cref="UBJAndLBJ_SetupChange_CasterAndKPI(OutputClass)"/></summary>
            ///<remarks>
            ///The method below is no longer needed because the UBJ and LBJ Points are taken of when the Steering Axis is Translated by an amount equal to the 
            ///contact patch displacement inside the <see cref="RotateWheelAssemblyComponents(Line, Angle, OutputClass)"/>
            ///</remarks>
            //UbJAngLBJ_SetupChange_CamberToe(_ocSetupCamber, 1);

            ///<summary>Calculating the change in toe</summary>
            ///<remarks>
            ///Unlike the <see cref="ReCheckCamber()"/> operations done  on Line 1429 here it is not necessary to do such operations here. This is because on Line 1429 what we are doing it we are checking if the Rotation operation that we performed ACTUALLY resulted in us getting the 
            ///correct camber angle (which is <see cref="dCamber"/>). If NO then the rotation is performed again until we get the right cambrr. The line of code finds the value by which the TOE has changed so that we can later on Reverse that Toe change to arrive at the samee OROROR 
            ///arrive at the required Toe.
            ///</remarks>
            Angle checkToe =  ReCheckToe();
            if (!_undoChange)
            {
                AssignToeDelta(checkToe); 
            }
            else
            {
                AssignToeDelta(new Angle(_ocSetupCamber.sccvOP.deltaToe - checkToe.Degrees, AngleUnit.Degrees));
            }

            ///<summary>Calculating the change in Caster </summary>
            GetNewCaster(_indexSteeringAxis);

            ///<summary>Calcualting the change in KPI</summary>
            GetNewKPI(_indexSteeringAxis);
        }

        /// <summary>
        /// Method to calcualte the Toe change requested by the user as a Setup Change. This method shall also re-calculate all the other things which change with the Toe
        /// </summary>
        /// <param name="_dToe">Change in Toe requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupToe">Object of the OutputClass to store the value of the Toe</param>
        internal void SetupChange_ToeChange(double _dToe, OutputClass _ocSetupToe, int _indexWheelSpindle,int _indexSteeringAxis, bool _undoChange)
        {
            ///<summary>Calculating the new Toe using the delta of Toe</summary>
            Angle dToe = new Angle(_dToe, AngleUnit.Degrees);

            ///<remarks>Adding a Label here so I can createa Loop without using a For Statement. Label placed in the 2nd line so that the <see cref="dToe"/> Angle gets the user requested OR reversal value only once. All other rotations should be by angle <see cref="ReCheckToe()"/></remarks>
            Start:

            //_ocSetupToe.waOP.StaticToe += dToe.Radians;
            SetupChange_DB_Master.SetupChangeOPDictionary["Toe"] = dToe.Radians;

            ///<summary>Rotating the Wheel Assembyly Points and Vectors using the <see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> </summary>
            RotateWheelAssemblyComponents((SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[_indexSteeringAxis]), dToe, _ocSetupToe, _indexWheelSpindle);
            ///<remarks>
            ///The method below is no longer needed because the Wheel Spindle is Translated by an amount equal to the 
            ///contact patch displacement inside the <see cref="RotateWheelAssemblyComponents(Line, Angle, OutputClass)"/>
            ///</remarks>

            ///<summary>
            ///Checking if performing the rotation of the <see cref="SetupChangeDatabase.WheelSpindle"/>by the angle <see cref="dToe"/> about <see cref="SetupChangeDatabase.SteeringAxis"/> 
            ///ACTUALLY resulted in getting a Toe Angle of <see cref="dToe"/>
            ///IF not then <see cref="SetupChange_ToeChange(double, OutputClass, int, int)"/> is performed again 
            ///</summary>
            Angle checkToe  = ReCheckToe();
            
            double checker;
            if (!_undoChange) { checker = dToe.Radians - checkToe.Radians; }
            else { checker = checkToe.Radians; }

            //if (!_undoChange)
            //{
            //    AssignToeDelta(new Angle(dToe.Degrees - checkToe.Degrees, AngleUnit.Radians));
            //}
            //else
            //{
            //    AssignToeDelta(checkToe);
            //}
            AssignToeDelta(new Angle(/*checker*/dToe.Radians - checkToe.Radians, AngleUnit.Radians));

            if (Math.Abs(checker) > 0.0009) 
            {
                //AssignToeDelta(checkToe);
                dToe = checkToe;
                goto Start;
            }
            

            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight(_indexWheelSpindle);

            ///<summary>Calculating the new coordinates of the UBJ and the LBJ using the <see cref="UBJAndLBJ_SetupChange_CasterAndKPI(OutputClass)"/></summary>
            ///<remarks>
            ///The method below is no longer needed because the UBJ and LBJ Points are taken of when the Steering Axis is Translated by an amount equal to the 
            ///contact patch displacement inside the <see cref="RotateWheelAssemblyComponents(Line, Angle, OutputClass)"/>
            ///</remarks>
            //UbJAngLBJ_SetupChange_CamberToe(_ocSetupToe, 1);



            ///<summary>Calculating the change in Camber</summary>
            ///<remarks>
            ///Unlike the <see cref="ReCheckToe()"/> operations done on Line 1492 here it is not necessary to do such operations here. This is because on Line 1492 what we are doing it we are checking if the Rotation operation that we performed ACTUALLY resulted in us getting the 
            ///correct Toe angle (which is <see cref="dToe"/>). If NO then the rotation is performed again until we get the right Toe. The line of code finds the value by which the Camber has changed so that we can later on Reverse that Camber change to arrive at the samee OROROR 
            ///arrive at the required Camber.
            ///</remarks>
            //GetNewCamber(_indexWheelSpindle);
            Angle checkCamber =  ReCheckCamber();
            if (!_undoChange)
            {
                AssignCamberDelta(checkCamber); 
            }
            else if (_undoChange)
            {
                AssignCamberDelta(new Angle(_ocSetupToe.sccvOP.deltaCamber - checkCamber.Degrees, AngleUnit.Degrees));
            }

            ///<summary>Calculating the change in Caster </summary>
            GetNewCaster(_indexSteeringAxis);

            ///<summary>Calcualting the change in KPI</summary>
            GetNewKPI(_indexSteeringAxis);

        }

        /// <summary>
        /// Method to calcualte the Caster change requested by the user as a Setup Change. This method shall also re-calculate all the other things which change with the Caster
        /// </summary>
        /// <param name="_dCaster">Change in Caster requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupCaster"></param>
        internal void SetupChange_CasterChange(double _dCaster, OutputClass _ocSetupCaster, int _indexWheelSpindle, int _indexSteeringAxis, bool _undoChange)
        {
            ///<summary>Calculating the new Caster using the delta of Caster</summary>
            Angle dCaster = new Angle(_dCaster, AngleUnit.Degrees);
            _ocSetupCaster.Caster += dCaster.Radians;
            SetupChange_DB_Master.SetupChangeOPDictionary["Caster"] = dCaster.Radians;

            ///<summary>
            ///Rotating the Steering Axis and the Wheel Assembyly Points and Vectors using the 
            ///<see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> and the 
            ///<see cref="RotateSteeringAxis(Vector3D, Angle)"/>
            ///</summary>
            RotateSteeringAxis(Custom3DGeometry.GetdevDeptVector3D(SetupChange_DB_Master.SteeringAxis.PerpAlongX.Aft), dCaster, _indexSteeringAxis);
            RotateWheelAssemblyComponents((SetupChange_DB_Master.SteeringAxis.PerpAlongX.Aft), dCaster, _ocSetupCaster, _indexWheelSpindle);
            //SetupChange_DB_Master.SteeringAxis.GetFinalStateOfVariableLines(SetupChange_DB_Master.SteeringAxis, 1);
            //SetupChange_DB_Master.InitializeSteeringAxis(1);

            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight(_indexWheelSpindle);

            ///<summary>Calculating the new coordinates of the UBJ and the LBJ using the <see cref="UBJAndLBJ_SetupChange_CasterAndKPI(OutputClass)"/></summary>
            ///<remarks>
            ///Deeper thought made me realise. Why would I need this? When I rotate the Steering axis, I infact rotate the UBJ and LBJ and hence I already know their new position.....RIGHT?
            /// </remarks>
            //UBJAndLBJ_SetupChange_CasterAndKPI(_ocSetupCaster);


            ///<summary>Calculating the change in Camber</summary>
            //GetNewCamber(_indexWheelSpindle);
            ReCheckCamber();

            ///<summary>Calculating the change in toe</summary>
            //GetNewToe(_indexWheelSpindle);
            ReCheckToe();

            ///<summary>Calcualting the change in KPI</summary>
            GetNewKPI(_indexSteeringAxis);

        }

        /// <summary>
        /// Method to calcualte the KPI change requested by the user as a Setup Change. This method shall also re-calculate all the other things which change with the KPI
        /// </summary>
        /// <param name="_dKPI">Change in KPI requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupKPI"></param>
        internal void SetupChange_KPIChange(double _dKPI, OutputClass _ocSetupKPI, int _indexWheelSpindle, int _indexSteeringAxis, bool _undoChange)
        {
            ///<summary>Calculating the new KPI using the delta of KPI</summary>
            Angle dKPI = new Angle(_dKPI, AngleUnit.Degrees);
            _ocSetupKPI.KPI += dKPI.Radians;
            SetupChange_DB_Master.SetupChangeOPDictionary["KPI"] = dKPI.Radians;

            ///<summary>
            ///Rotating the Steering Axis and the Wheel Assembyly Points and Vectors using the 
            ///<see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> and the 
            ///<see cref="RotateSteeringAxis(Vector3D, Angle)"/>
            ///</summary>
            RotateSteeringAxis(Custom3DGeometry.GetdevDeptVector3D(SetupChange_DB_Master.SteeringAxis.PerpAlongZ.Aft), dKPI, _indexSteeringAxis);
            RotateWheelAssemblyComponents((SetupChange_DB_Master.SteeringAxis.PerpAlongZ.Aft), dKPI, _ocSetupKPI, _indexWheelSpindle);
            //SetupChange_DB_Master.SteeringAxis.GetFinalStateOfVariableLines(SetupChange_DB_Master.SteeringAxis, 1);
            //SetupChange_DB_Master.InitializeSteeringAxis(1);

            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight(_indexWheelSpindle);

            ///<summary>Calculating the new coordinates of the UBJ and the LBJ using the <see cref="UBJAndLBJ_SetupChange_CasterAndKPI(OutputClass)"/></summary>
            ///<remarks>
            ///Deeper thought made me realise. Why would I need this? When I rotate the Steering axis, I infact rotate the UBJ and LBJ and hence I already know their new position.....RIGHT?
            /// </remarks>
            //UBJAndLBJ_SetupChange_CasterAndKPI(_ocSetupKPI);

            ///<summary>Calculating the change in Camber</summary>
            //GetNewCamber(_indexWheelSpindle);
            ReCheckCamber();

            ///<summary>Calculating the change in toe</summary>
            //GetNewToe(_indexWheelSpindle);
            ReCheckToe();

            ///<summary>Calculating the change in Caster </summary>
            GetNewCaster(_indexSteeringAxis);


        }


        /// <summary>
        /// Method to assign a point to the corresponding Output Class SuspensionCoordinateMaster Variables
        /// </summary>
        /// <param name="_pointToBeAssigned">Point which is calculated and is now to be assigned</param>
        /// <param name="_x">X coordinate of the SuspensionCOordinateMaster Objects variable which is receive the value</param>
        /// <param name="_y">Y coordinate of the SuspensionCOordinateMaster Objects variable which is receive the value</param>
        /// <param name="_z">Z coordinate of the SuspensionCOordinateMaster Objects variable which is receive the value</param>
        private void AssignPoint3D(Point3D _pointToBeAssigned, ref double _x, ref double _y, ref double _z)
        {
            _x = _pointToBeAssigned.X;
            _y = _pointToBeAssigned.Y;
            _z = _pointToBeAssigned.Z;
        }

        /// <summary>
        /// Rotates the Wheel Spindle End, Contact Patch and Wheel Spindle by rotation angle about a rotation axis which the user passes. The Wheel Spindle Start (aka Wheel Centre) is translated by an amount equal to the delta of 
        /// wheel deflection (aka COntact Patch Vertical Movement)
        /// </summary>
        /// <param name="_rotationLine"></param>
        /// <param name="_rotationAngle"></param>
        /// <param name="_ocRotate"></param>
        private void RotateWheelAssemblyComponents(Line _rotationLine, Angle _rotationAngle, OutputClass _ocRotate, int i)
        {
            ///<summary>Rotating the points and the spindle vector</summary>
            ///<remarks>
            ///The code below represents rotation of the wheel (i.e., points of the wheel) about a vector perpendicular to the steering axis and along the Z axis. Hence this is a camber rotation of the wheel
            ///The points being rotated are the Wheel Spindle End and the contact patch.
            ///Based on the new position of the contact patch, the wheel centre will be translated. 
            ///Based on the CP, K and L the new position of the steering axis will be found
            /// </remarks>

            ///<summary>Adding a new Line to the List of <see cref="ChildLine_VariableLine.DeltaLine"/></summary>
            SetupChange_DB_Master.ContactPatch.DeltaPoint.Add(new Point3D(SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1].X, 
                                                                          SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1].Y, 
                                                                          SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1].Z));


            SetupChange_DB_Master.WheelSpindle.AddLineAndPointToDeltaLineAndDeltaPoint(SetupChange_DB_Master.WheelSpindle, SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1);

            ///<summary>Rotating the Wheel Spindle to reflect the Camber</summary>
            SetupChange_DB_Master.WheelSpindle.Line.DeltaLine[SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1].Rotate(_rotationAngle.Radians, _rotationLine.StartPoint, _rotationLine.EndPoint);
            AssignPoint3D(SetupChange_DB_Master.WheelSpindle.Line.DeltaLine[SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1].EndPoint, ref _ocRotate.scmOP.L1x, ref _ocRotate.scmOP.L1y, ref _ocRotate.scmOP.L1z);

            ///<summary>Rotating the Contact Patch</summary>
            SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1] = SetupChange_DB_Master.ContactPatch.RotatePoint(SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1],
                                                                                                                                                                    SetupChange_DB_Master.WheelSpindle.Line.DeltaLine[SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1].StartPoint, 
                                                                                                                                                                    _rotationLine, _rotationAngle);

            AssignPoint3D(SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1], ref _ocRotate.scmOP.W1x, ref _ocRotate.scmOP.W1y, ref _ocRotate.scmOP.W1z);

            ///<summary>Translating the coordinate of the Wheel Centre (aka Spindle Start) by an amount equal to the Contact Patch's vertical displacement</summary>
            if (SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1 - 1].Y != 0)
            {
                TranslateWheelAssembly(0, /*-(SetupChange_DB_Master.ContactPatch.DeltaPoint[i].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[i -1].Y)*/0, 0, i);
            }

            SetupChange_DB_Master.WheelSpindle.UpdateComponent(SetupChange_DB_Master.WheelSpindle, SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1);
            SetupChange_DB_Master.SteeringAxis.UpdateComponent(SetupChange_DB_Master.SteeringAxis, SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            SetupChange_DB_Master.InitializeSteeringAxis(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);


            AssignPoint3D(SetupChange_DB_Master.WheelSpindle.Line.DeltaLine[SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1].StartPoint, ref _ocRotate.scmOP.K1x, ref _ocRotate.scmOP.K1y, ref _ocRotate.scmOP.K1z);
        }

        private void RotateSteeringAxis(Vector3D _rotationAxis, Angle _rotationAngle, int i)
        {

            SetupChange_DB_Master.SteeringAxis.AddLineAndPointToDeltaLineAndDeltaPoint(SetupChange_DB_Master.SteeringAxis, SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            SetupChange_DB_Master.InitializeSteeringAxis(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);

            SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1].Rotate(_rotationAngle.Radians, _rotationAxis);
            SetupChange_DB_Master.SteeringAxis.UpdateComponent(SetupChange_DB_Master.SteeringAxis, SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            SetupChange_DB_Master.InitializeSteeringAxis(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            //SetupChange_DB_Master.SteeringAxis.GetFinalStateOfVariableLines(SetupChange_DB_Master.SteeringAxis, 1);

        }

        /// <summary>
        /// EXCEPT the contact patch, all the Points and Lines of the Wheel Assembly are translated using this method.
        /// This method is called when the <see cref="SetupChangeDatabase.ContactPatch"/>'s Y Coordinate changes. 
        /// When the Y Coordinate changes it means the Wheel Assemly also changes and hence this method must be called
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        private void TranslateWheelAssembly(double x, double y, double z, int _i)
        {
            SetupChange_DB_Master.TranslateWheelAssembly(x, y, z, _i);
            //SetupChange_DB_Master.WheelSpindle.Line.DeltaLine[1].Translate(0, -(SetupChange_DB_Master.ContactPatch.DeltaPoint[1].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[0].Y), 0)
        }

        #region --- MOSTLY DELETE ---Method to calculate the UBJ and LBJ points using the Vector Geometry Analysis method
        /// <summary>
        /// Method to calculate the UBJ and LBJ points using the Vector Geometry Analysis method
        /// </summary>
        /// <param name="_ocEF"></param>
        private void UBJAndLBJ_SetupChange_CasterAndKPI(OutputClass _ocEF)
        {
            ///<summary>Calcualting the new Position of the Lower Ball Joint</summary>
            QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, L1x, L1y, L1z, 0, l_K1x, l_K1y, l_K1z, l_W1x, l_W1y, l_W1z, _ocEF.scmOP.L1x, _ocEF.scmOP.L1y, _ocEF.scmOP.L1z, _ocEF.scmOP.K1x, _ocEF.scmOP.K1y, _ocEF.scmOP.K1z,
                                           _ocEF.scmOP.W1x, _ocEF.scmOP.W1y, _ocEF.scmOP.W1z, _ocEF.scmOP.F1y, true, out double XE1, out double YE1, out double ZE1);

            _ocEF.scmOP.E1x = XE1;
            _ocEF.scmOP.E1y = YE1;
            _ocEF.scmOP.E1z = ZE1;

            ///<summary>Calculating the new positon of the Upper Ball Joint</summary>
            QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, L1x, L1y, L1z, 0, l_K1x, l_K1y, l_K1z, l_E1x, l_E1y, l_E1z, _ocEF.scmOP.L1x, _ocEF.scmOP.L1y, _ocEF.scmOP.L1z, _ocEF.scmOP.K1x, _ocEF.scmOP.K1y, _ocEF.scmOP.K1z,
                                           _ocEF.scmOP.E1x, _ocEF.scmOP.E1y, _ocEF.scmOP.E1z, _ocEF.scmOP.W1y, false, out double XF1, out double YF1, out double ZF1);


            _ocEF.scmOP.F1x = XF1;
            _ocEF.scmOP.F1y = YF1;
            _ocEF.scmOP.F1z = ZF1;

            ///<summary>Creating the Vector representing the New Steering Axis</summary>
            //SetupChange_Master.SteeringAxis_New = new Vector3D(_ocEF.scmOP.F1x - _ocEF.scmOP.E1x, _ocEF.scmOP.F1y - _ocEF.scmOP.E1y, _ocEF.scmOP.F1z - _ocEF.scmOP.E1z);
            //SetupChange_DB_Master.SteeringAxis.Line.AssignFinalState_3DLine(new Point3D(_ocEF.scmOP.E1x, _ocEF.scmOP.E1y, _ocEF.scmOP.E1z), new devDept.Geometry.Point3D(_ocEF.scmOP.F1x, _ocEF.scmOP.F1y, _ocEF.scmOP.F1z), 0);
            SetupChange_DB_Master.SteeringAxis.UpdateComponent(SetupChange_DB_Master.SteeringAxis, 1);
            SetupChange_DB_Master.InitializeSteeringAxis(1);
        }
        #endregion

        #region ---DELETE--- Method to calculate the new coordinates of the UBJ and LBJ when the Camber and Toe is changed. 
        ///// <summary>
        ///// Method to calculate the new coordinates of the UBJ and LBJ when the Camber and Toe is changed. 
        ///// This seperate method is necessary because when the Camber and Toe is change there is more or less only vertical movement of the Steering Axis. 
        ///// </summary>
        ///// <param name="_ocEF"></param>
        ///// <param name="i"></param>
        //private void UbJAngLBJ_SetupChange_CamberToe(OutputClass _ocEF, int i)
        //{
        //    ///<summary>Assinging the Lower Ball Joint Point</summary>
        //    SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[i].StartPoint.Y -= SetupChange_DB_Master.ContactPatch.DeltaPoint[1].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[0].Y;
        //    AssignPoint3D(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[i].StartPoint, ref _ocEF.scmOP.E1x, ref _ocEF.scmOP.E1y, ref _ocEF.scmOP.E1z);

        //    SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[i].EndPoint.Y -= SetupChange_DB_Master.ContactPatch.DeltaPoint[1].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[0].Y;
        //    AssignPoint3D(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[i].EndPoint, ref _ocEF.scmOP.F1x, ref _ocEF.scmOP.F1y, ref _ocEF.scmOP.F1z);

        //} 
        #endregion

        /// <summary>
        /// Calculating the Camber with the Initial Wheel Spindle Vector. Remember that this angle is still a DELTA of Camber and not the absoulte. The only thing is that this is a DELTA of Camber with the starting condition of the Wheel Spindle Vector
        /// </summary>
        /// <returns>Angle of CAMBER between the current Wheel Spindle Vector and the Initial or 1st Wheel Spindle Vector when only STaTIC CAMBER was applied</returns>
        private Angle ReCheckCamber()
        {
            ///<remarks>Remember that the Wheel spindle is along the X axis. So to find the Camber, we need the angle of the Wheel Spindle in the FRONT VIEW with the XX axis passing through the Wheel Centre</remarks>
            Angle dCamber_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine[SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine.Count - 1]),
                                                                       Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine[0]),
                                                                       Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Longitudinal));

            //SetupChange_DB_Master.SetupChangeOPDictionary["Camber"] = dCamber_New.Radians;

            //SetupChange_CLS.ListHelper(SetupChange_CLS.Summ_dCamber, SetupChange_CLS.Final_Camber, "Camber");

            return /*-*/dCamber_New;
        }

        ///// <summary>
        ///// Calculating the change Camber
        ///// </summary>
        //private Angle GetNewCamber(int _i)
        //{
        //    ///<remarks>Remember that the Wheel spindle is along the X axis. So to find the Camber, we need the angle of the Wheel Spindle in the FRONT VIEW with the XX axis passing through the Wheel Centre</remarks>
        //    Angle dCamber_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine[SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine.Count - 1]),
        //                                                               Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine[/*SetupChange_DB_Master.WheelSpindle.ViewLines.FrontView.DeltaLine.Count - 1 - 1*/ 0]),
        //                                                               Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Longitudinal));

        //    //SetupChange_DB_Master.SetupChangeOPDictionary["Camber"] = dCamber_New.Radians;

        //    //SetupChange_CLS.ListHelper(SetupChange_CLS.Summ_dCamber, SetupChange_CLS.Final_Camber, "Camber");


        //    return dCamber_New;
        //}

        private void AssignCamberDelta(Angle _dCamber_New)
        {
            SetupChange_DB_Master.SetupChangeOPDictionary["Camber"] = _dCamber_New.Radians;

            SetupChange_CLS.ListHelper(SetupChange_CLS.Summ_dCamber, SetupChange_CLS.Final_Camber, "Camber");
        }

        /// <summary>
        /// Calculating the Toe with the Initial Wheel Spindle Vector. Remember that this angle is still a DELTA of Toe and not the absoulte. The only thing is that this is a DELTA of Toe with the starting condition of the Wheel Spindle Vector
        /// </summary>
        /// <returns>Angle of TOE between the current Wheel Spindle Vector and the Initial or 1st Wheel Spindle Vector when only STaTIC TOE was applied</returns>
        public Angle ReCheckToe()
        {
            ///<remarks>Remember that the Wheel spindle is along the X axis. So to find the TOE, we need the angle of the Wheel Spindle in the TOP VIEW with the XX axis passing through the Wheel Centre</remarks>
            Angle dToe_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine[SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine.Count - 1]),
                                                                     Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine[0]),
                                                                     Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Vertical));

            //SetupChange_DB_Master.SetupChangeOPDictionary["Toe"] = dToe_New.Radians;

            //SetupChange_CLS.ListHelper(SetupChange_CLS.Summ_dToe, SetupChange_CLS.Final_Toe, "Toe");
            
            ///<remarks>
            ///Why I need negative for Toe is mentioned in my OneNote section called "Epiphanies!" 
            ///In short; All the while I was imagining myself to be sitting in the car and hence in MY front view, CW was positive. But for C#, MathNet.Spatial and EYESHOT, FRONT VIEW is same as that of SolidWorks and hence for their Front View, positive rotation is CCW
            ///Howeverm for Topview mine and C#, MathNet.Spatial and EYESHOT have the same defintions so CW rotation is negative. But to maintain my convention and have CW as positive I have to add Negative sign to TOE. 
            /// </remarks>
            return /*-*/dToe_New;
        }

        ///// <summary>
        ///// Calculating the change in Toe or DELTA Toe. I.E., the angle between the Current Wheel Spindle and the Just before Wheel Spindle
        ///// </summary>
        //private Angle GetNewToe(int _i)
        //{
        //    ///<remarks>Remember that the Wheel spindle is along the X axis. So to find the TOE, we need the angle of the Wheel Spindle in the TOP VIEW with the XX axis passing through the Wheel Centre</remarks>
        //    Angle dToe_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine[SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine.Count - 1]),
        //                                                             Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine[SetupChange_DB_Master.WheelSpindle.ViewLines.TopView.DeltaLine.Count - 1 - 1]),
        //                                                             Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Vertical));

        //    SetupChange_DB_Master.SetupChangeOPDictionary["Toe"] = dToe_New.Radians;

        //    SetupChange_CLS.ListHelper(SetupChange_CLS.Summ_dToe, SetupChange_CLS.Final_Toe, "Toe");


        //    return dToe_New;
        //}

        private void AssignToeDelta(Angle _dToe_New)
        {
            SetupChange_DB_Master.SetupChangeOPDictionary["Toe"] = _dToe_New.Radians;

            SetupChange_CLS.ListHelper(SetupChange_CLS.Summ_dToe, SetupChange_CLS.Final_Toe, "Toe");
        }

        /// <summary>
        /// Calculating the change in Caster
        /// </summary>
        private Angle GetNewCaster(int _i)
        {
            //Angle dCaster_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.SideView.DeltaLine[_i]),
            //                                                            Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.SideView.DeltaLine[_i - 1]),Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Lateral));
            //SetupChange_DB_Master.SetupChangeOPDictionary["Caster"] = dCaster_New.Radians;
            //SetupChange_DB_Master.InitializeSteeringAxis(1);

            //return dCaster_New;
            return new Angle();
        }

        /// <summary>
        /// Calcualting the change in KPI
        /// </summary>
        private Angle GetNewKPI(int _i)
        {
            //Angle dKPI_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.FrontView.DeltaLine[_i]),
            //                                                         Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.FrontView.DeltaLine[_i - 1]), Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Longitudinal));
            //SetupChange_DB_Master.SetupChangeOPDictionary["KPI"] = dKPI_New.Radians;
            //SetupChange_DB_Master.InitializeSteeringAxis(1);

            //return dKPI_New;
            return new Angle();

        }

        /// <summary>
        /// Calculating the new contact patch coordinates. This will determine the change in Ride Height
        /// </summary>
        private double GetNewRideHeight(int _i)
        {
            ///<summary>Calculating the change in Ride Height</summary>
            ///<remarks>
            /// When the New and Old values of the Contact Patch Y coordinate are subtraced if the result is positive then it means that the Contact Patch has risen above. 
            /// This implies that the chassis has gone down (Imagine It!!)
            /// Hence below, a minus sign is added to tell the software that the Ride Height is infact reducing when the Contact Patch is going up
            /// </remarks>
            double dRideHeight_New = -(SetupChange_DB_Master.ContactPatch.DeltaPoint[_i].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[_i - 1].Y);
            SetupChange_DB_Master.SetupChangeOPDictionary["RideHeight"] = dRideHeight_New;

            return dRideHeight_New;
        }





















        #endregion
    }
}
    

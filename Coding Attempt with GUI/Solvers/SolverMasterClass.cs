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

        #region ---DECLARATIONS---

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
        /// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be treated as the Master
        /// </summary>
        SetupChange_ClosedLoopSolver SetupChange_CLS_Master;
        /// <summary>
        /// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Front Left Corner
        /// </summary>
        public SetupChange_ClosedLoopSolver SetupChange_CLS_FL;
        /// <summary>
        /// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Front Right Corner
        /// </summary>
        public SetupChange_ClosedLoopSolver SetupChange_CLS_FR;
        /// <summary>
        /// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Rear Left Corner
        /// </summary>
        public SetupChange_ClosedLoopSolver SetupChange_CLS_RL;
        /// <summary>
        /// Object of the <see cref="SetupChange_ClosedLoopSolver"/> Class which will be used by the Rear Right Corner
        /// </summary>
        public SetupChange_ClosedLoopSolver SetupChange_CLS_RR;
        #endregion

        #region Object of the Vehicle Model Class. Used exclusively for the Setup Change
        private VehicleModel SetupChange_VehicleModel = new VehicleModel();
        #endregion

        /// <summary>
        /// <para><see cref="SimulationType"/> object will help me calculate with the Delta of the CornerWeight. <see cref="CalculateWheelAndSpringDeflection(SuspensionCoordinatesMaster, Spring, Damper, Vehicle, List{double}, OutputClass, Tire, int, double, bool, bool, int)"/></para>
        /// <para>I would enter this method for <see cref="SetupChange"/> operations because <see cref="SuspensionCoordinatesMaster.SuspensionMotionExists"/> would be false. Hence After the <see cref="VehicleModel"/> is solved for Diagonal Weight Transfer I would need to calculate the Delta
        /// of Spring Deflection to find the change in the Suspension Coordinates. At that point of time I use this object below to tell the <see cref="CalculateWheelAndSpringDeflection(SuspensionCoordinatesMaster, Spring, Damper, Vehicle, List{double}, OutputClass, Tire, int, double, bool, bool, int)"/>
        /// method that I should find the delta of Corner Weight rather than use the Corner Weight as it is</para>
        /// </summary>
        public static SimulationType SimType = SimulationType.Dummy;

        /// <summary>
        /// 
        /// </summary>
        Angle FinalCamberFL, FinalCamberFR, FinalCamberRL, FinalCamberRR;
        /// <summary>
        /// 
        /// </summary>
        Angle FinalToeFL, FinalToeFR, FinalToeRL, FinalToeRR;
        /// <summary>
        /// 
        /// </summary>
        Angle FinalCasterFL, FinalCasterFR, FinalCasterRL, FinalCasterRR;
        /// <summary>
        /// 
        /// </summary>
        Angle FinalKPIFL, FinalKPIFR, FinalKPIRL, FinalKPIRR;
        /// <summary>
        /// Double Variables to store the Pushrod of each corner. These are needed because the Pushrod deltas calculated during the RideHeight Change Phase are wiped out during the Individual Corner SetupChange Phase. <see cref="SetupChange_PrimaryInvoker(SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, Vehicle)"/>
        /// </summary>
        double FinalPushrod_FL, FinalPushrod_FR, FinalPushrod_RL, FinalPushrod_RR;
        /// <summary>
        /// Double Variables to store the Pushrod of each corner. These are needed because the Ride Height deltas calculated during the RideHeight Change Phase are wiped out during the Individual Corner SetupChange Phase. <see cref="SetupChange_PrimaryInvoker(SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, Vehicle)"/>
        /// </summary>
        double FinalRideHeight_FL, FinalRideHeight_FR, FinalRideHeight_RL, FinalRideHeight_RR;

        double XW1 = 0, YW1 = 0, ZW1 = 0, XW2 = 0, YW2 = 0, ZW2 = 0;



        #endregion

        #region Common Methods
        
        #region ---INITIALIZER METHODS---

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
        public void CalculateKPIandCaster(OutputClass ocKPICaster, bool IsDoubleWishbone, int identifier)
        {
            if (IsDoubleWishbone)
            {
                E1F1_Caster = Math.Sqrt(Math.Pow((l_F1y - l_E1y), 2) + Math.Pow((l_F1z - l_E1z), 2));
                WheelCentreVert_Caster = Math.Sqrt(Math.Pow((l_K1y - 0), 2))/* + Math.Pow((l_K1z - 0), 2))*/;
                ocKPICaster.Caster = (Math.Acos((((l_F1y - l_E1y) * (l_K1y - 0)) + ((l_F1z - l_E1z) * (l_K1z - l_K1z))) / (E1F1_Caster * WheelCentreVert_Caster)));

                E1F1_KPI = Math.Sqrt(Math.Pow((l_F1x - l_E1x), 2) + Math.Pow((l_F1y - l_E1y), 2));
                WheelCentreVert_KPI = Math.Sqrt(/*Math.Pow((l_K1x - 0), 2) +*/ Math.Pow((l_K1y - 0), 2));
                ocKPICaster.KPI = (Math.Acos((((l_F1x - l_E1x) * (l_K1x - l_K1x)) + ((l_F1y - l_E1y) * (l_K1y - 0))) / (E1F1_KPI * WheelCentreVert_KPI))); 
            }
            else if (!IsDoubleWishbone)
            {
                E1F1_Caster = Math.Sqrt(Math.Pow((l_J1y - l_E1y), 2) + Math.Pow((l_J1z - l_E1z), 2));
                WheelCentreVert_Caster = Math.Sqrt(Math.Pow((l_K1y - 0), 2))/* + Math.Pow((l_K1z - 0), 2))*/;
                ocKPICaster.Caster = (Math.Acos((((l_J1y - l_E1y) * (l_K1y - 0)) + ((l_J1z - l_E1z) * (l_K1z - l_K1z))) / (E1F1_Caster * WheelCentreVert_Caster)));

                E1F1_KPI = Math.Sqrt(Math.Pow((l_J1x - l_E1x), 2) + Math.Pow((l_J1y - l_E1y), 2));
                WheelCentreVert_KPI = Math.Sqrt(/*Math.Pow((l_K1x - 0), 2) +*/ Math.Pow((l_K1y - 0), 2));
                ocKPICaster.KPI = (Math.Acos((((l_J1x - l_E1x) * (l_K1x - l_K1x)) + ((l_J1y - l_E1y) * (l_K1y - 0))) / (E1F1_KPI * WheelCentreVert_KPI)));
            }

            AssignDirection_KPI(identifier, ref ocKPICaster.KPI);

            //if (_identifier == 1 || _identifier == 3)
            //{
            //    //_ocKPICaster.Caster *= -1;
            //    _ocKPICaster.KPI *= /*-*/1;
            //}
            //else if (_identifier == 2 || _identifier == 4)
            //{
            //    //_ocKPICaster.Caster *= /*-*/1;
            //    _ocKPICaster.KPI *= -1;
            //}

        }

        private void AssignDirection_KPI(int _identifier, ref double _kpiAngle)
        {
            if (_identifier == 1 || _identifier == 3)
            {
                //_ocKPICaster.Caster *= -1;
                _kpiAngle *= /*-*/1;
            }
            else if (_identifier == 2 || _identifier == 4)
            {
                //_ocKPICaster.Caster *= /*-*/1;
                _kpiAngle *= -1;
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
            _ocTrans.FinalRideHeight_1 = opY;
            CoordinateTranslator.InitializeVariables(_scmTrans, _ocTrans.scmOP, ipX, ipY, ipZ);
            CoordinateTranslator.TranslateCoordinates_To_AnyCS(_ocTrans.scmOP, ipX, ipY, ipZ);
            _ocTrans.FinalRideHeight_ForTrans += ipY;
            //_ocTrans.FinalRideHeight_1 = opY;
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

        #region ---KINEMATIC SOLVER METHODS---

        #region Calculating the Wheel and Spring Deflections for Static force or for Motion
        public void CalculateWheelAndSpringDeflection(SuspensionCoordinatesMaster _scmDef, Spring _springDef, Damper _damperDef, Vehicle _vehicleDef, List<double> _WheelDeflection, OutputClass _ocDef, Tire _tireDef, int Identifier, double arb_Rate_Nmm, bool MotionExists, bool _recalSteering, int Index)
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
                if (SimType != SimulationType.SetupChange)
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
                else
                {
                    Fy_Wheel = (_ocDef.CW - _ocDef.CW_1);
                    Fy_Spring = Fy_Wheel / _scmDef.InitialMR;

                    WheelDeflection = ((Fy_Wheel / _ocDef.RideRate));
                    SpringDeflection = (Fy_Spring / _springDef.SpringRate);

                    #region Calculating the Preload load due to _springDef and _damperDef
                    _ocDef.Corrected_SpringDeflection = SpringDeflection;
                    _ocDef.Corrected_WheelDeflection = WheelDeflection;
                    #endregion
                }
                //Fy_Spring = Fy_Wheel / _scmDef.InitialMR;

                //WheelDeflection = ((_vehicleDef.CW[Identifier - 1]) / _ocDef.RideRate);
                //SpringDeflection = (Fy_Spring / _springDef.SpringRate)/* - Preload_mm*/;

                //#region Calculating the Preload load due to _springDef and _damperDef
                //_ocDef.Corrected_SpringDeflection = SpringDeflection - Preload_mm;
                //_ocDef.Corrected_WheelDeflection = _ocDef.Corrected_SpringDeflection / _scmDef.InitialMR;
                //#endregion

            }
            else if (MotionExists)
            {
                ///<!--Most likely the below line of code is incorrect when the solver is Recalculating for Steering becsuse, the Wheel Deflection passed during Steering is a delta value and not an absolute value as is the case when running the solver for a Motion-->
                Fy_Wheel = ((_ocDef.CW)) /*+*/- (_WheelDeflection[Index] * _ocDef.RideRate);
                Fy_Spring = Fy_Wheel / _scmDef.InitialMR;

                SpringDeflection = (Fy_Spring / _springDef.SpringRate) - Preload_mm;

                if (Index != 0)
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
                WheelDeflection = _springDeflection * _motionRatio;
            }
            else if (index != 0)
            {
                WheelDeflection += _springDeflection * _motionRatio;
            }

            //_oc[index].Corrected_WheelDeflection += WheelDeflection;
            //_oc[index].Corrected_SpringDeflection += SpringDeflection_Delta;
        }
        #endregion

        public void CalculateSpringDeflection_DiagonalWT_Steering_Rear(List<OutputClass> _oc, int _identiier, int Index)
        {
            ///<remarks>
            ///Might need this class 
            /// </remarks>
        }

        #region Calculating the Spring Deflection in small steps 
        public void CalculateAngleOfRotationOrDamperLength(OutputClass _ocSteps, bool MotionExists, int _springPrevIndex, Damper _damperSteps, bool _recalculateSteering)
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

        #endregion

        #region ---SETUP CHANGE METHODS---
        /// <summary>
        /// Primary and only public entry point method for the Setup Change methods. 
        /// </summary>
        /// <param name="_FlCV">Front Left corner Setup Change values requested by the user</param>
        /// <param name="_FrCV">Front Right corner Setup Change values requested by the user</param>
        /// <param name="_RlCV">Rear Left corner Setup Change values requested by the user</param>
        /// <param name="_RrCV">Rear Right corner Setup Change values requested by the user</param>
        /// <param name="_Vehicle"Object of the <see cref="Vehicle"/> class</param>
        public void SetupChange_PrimaryInvoker(SetupChange_CornerVariables _FlCV, SetupChange_CornerVariables _FrCV, SetupChange_CornerVariables _RlCV, SetupChange_CornerVariables _RrCV, Vehicle _Vehicle)
        {
            Identifier = 0;

            ///<summary>Assinging the <see cref="SetupChange_CornerVariables"/> class objects of each corner</summary>
            _Vehicle.oc_FL[0].sccvOP = _FlCV;
            _Vehicle.oc_FR[0].sccvOP = _FrCV;
            _Vehicle.oc_RL[0].sccvOP = _RlCV;
            _Vehicle.oc_RR[0].sccvOP = _RrCV;

            ///<summary>Assigning Correct orientation of Camber and Toe </summary>
            AssignOrientation_CamberToe(ref _Vehicle.oc_FL[0].sccvOP.deltaCamber, ref _Vehicle.oc_FL[0].sccvOP.deltaToe, _Vehicle.oc_FL[0].sccvOP.deltaCamber, _Vehicle.oc_FL[0].sccvOP.deltaToe, 1);
            AssignOrientation_CamberToe(ref _Vehicle.oc_FR[0].sccvOP.deltaCamber, ref _Vehicle.oc_FR[0].sccvOP.deltaToe, _Vehicle.oc_FR[0].sccvOP.deltaCamber, _Vehicle.oc_FR[0].sccvOP.deltaToe, 2);
            AssignOrientation_CamberToe(ref _Vehicle.oc_RL[0].sccvOP.deltaCamber, ref _Vehicle.oc_RL[0].sccvOP.deltaToe, _Vehicle.oc_RL[0].sccvOP.deltaCamber, _Vehicle.oc_RL[0].sccvOP.deltaToe, 3);
            AssignOrientation_CamberToe(ref _Vehicle.oc_RR[0].sccvOP.deltaCamber, ref _Vehicle.oc_RR[0].sccvOP.deltaToe, _Vehicle.oc_RR[0].sccvOP.deltaCamber, _Vehicle.oc_RR[0].sccvOP.deltaToe, 4);

            ///<summary>Assigning correct orientation for KPI. Caster is always correct as all 4 wheel Steering Axis have the same orientation when viewed from the side view</summary>
            AssignDirection_KPI(1, ref _Vehicle.oc_FL[0].sccvOP.deltaKPI);
            AssignDirection_KPI(2, ref _Vehicle.oc_FR[0].sccvOP.deltaKPI);
            AssignDirection_KPI(3, ref _Vehicle.oc_RL[0].sccvOP.deltaKPI);
            AssignDirection_KPI(4, ref _Vehicle.oc_RR[0].sccvOP.deltaKPI);

            _Vehicle.oc_FL[0].sccvOP.deltaCaster *= -1;
            _Vehicle.oc_FR[0].sccvOP.deltaCaster *= -1;
            _Vehicle.oc_RL[0].sccvOP.deltaCaster *= -1;
            _Vehicle.oc_RR[0].sccvOP.deltaCaster *= -1;

            _Vehicle.oc_FL[0].Caster *= -1;
            _Vehicle.oc_FR[0].Caster *= -1;
            _Vehicle.oc_RL[0].Caster *= -1;
            _Vehicle.oc_RR[0].Caster *= -1;

            ///<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            SetupChange_AssignNewSetupValues(_Vehicle.oc_FL, _FlCV, ref FinalCamberFL, ref FinalToeFL, ref FinalCasterFL, ref FinalKPIFL);
            ///<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            SetupChange_AssignNewSetupValues(_Vehicle.oc_FR, _FrCV, ref FinalCamberFR, ref FinalToeFR, ref FinalCasterFR, ref FinalKPIFR);
            ///<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            SetupChange_AssignNewSetupValues(_Vehicle.oc_RL, _RlCV, ref FinalCamberRL, ref FinalToeRL, ref FinalCasterRL, ref FinalKPIRL);
            ///<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            SetupChange_AssignNewSetupValues(_Vehicle.oc_RR, _RrCV, ref FinalCamberRR, ref FinalToeRR, ref FinalCasterRR, ref FinalKPIRR);


            ///<summary>Performing a Primary Initialization Method so that, in case the Ride Height OR Pushrod is changed, it can be solved first</summary>
            if (_FlCV.RideHeightChanged || _FrCV.RideHeightChanged || _RlCV.RideHeightChanged || _RrCV.RideHeightChanged)
            {
                Identifier = 1;
                SetupChange_PrimaryInitializeMethod(_FlCV, _Vehicle.oc_FL, FinalCamberFL, FinalToeFL, FinalCasterFL, FinalKPIFL);
                FinalRideHeight_FL = SetupChange_GetRideHeightChange(_FlCV, SetupChange_DB_Master.AdjOptions.PushrodLine, SetupChange_DB_Master.AdjOptions.PushrodVector, _FlCV.rideheightAdjustmentType, _FlCV.rideheightAdjustmentTool, 1, out FinalPushrod_FL);

                Identifier = 2;
                SetupChange_PrimaryInitializeMethod(_FrCV, _Vehicle.oc_FR, FinalCamberFR, FinalToeFR, FinalCasterFR, FinalKPIFR);
                FinalRideHeight_FR = SetupChange_GetRideHeightChange(_FrCV, SetupChange_DB_Master.AdjOptions.PushrodLine, SetupChange_DB_Master.AdjOptions.PushrodVector, _FrCV.rideheightAdjustmentType, _FrCV.rideheightAdjustmentTool, 2, out FinalPushrod_FR);

                Identifier = 3;
                SetupChange_PrimaryInitializeMethod(_RlCV, _Vehicle.oc_RL, FinalCamberRL, FinalToeRL, FinalCasterRL, FinalKPIRL);
                FinalRideHeight_RL = SetupChange_GetRideHeightChange(_RlCV, SetupChange_DB_Master.AdjOptions.PushrodLine, SetupChange_DB_Master.AdjOptions.PushrodVector, _RlCV.rideheightAdjustmentType, _RlCV.rideheightAdjustmentTool, 3, out FinalPushrod_RL);

                Identifier = 4;
                SetupChange_PrimaryInitializeMethod(_RrCV, _Vehicle.oc_RR, FinalCamberRR, FinalToeRR, FinalCasterRR, FinalKPIRR);
                FinalRideHeight_RR = SetupChange_GetRideHeightChange(_RrCV, SetupChange_DB_Master.AdjOptions.PushrodLine, SetupChange_DB_Master.AdjOptions.PushrodVector, _RrCV.rideheightAdjustmentType, _RrCV.rideheightAdjustmentTool, 4, out FinalPushrod_RR);

                SetupChange_RideHeightChange_Helper_SolveForRideHeightChanges(_Vehicle, FinalRideHeight_FL, FinalRideHeight_FR, FinalRideHeight_RL, FinalRideHeight_RR);

                SetupChange_EditSetupValues(_Vehicle.oc_FL[0], FinalCamberFL, FinalToeFL, FinalCasterFL, FinalKPIFL);
                SetupChange_EditSetupValues(_Vehicle.oc_FR[0], FinalCamberFR, FinalToeFR, FinalCasterFR, FinalKPIFR);
                SetupChange_EditSetupValues(_Vehicle.oc_RL[0], FinalCamberRL, FinalToeRL, FinalCasterRL, FinalKPIRL);
                SetupChange_EditSetupValues(_Vehicle.oc_RR[0], FinalCamberRR, FinalToeRR, FinalCasterRR, FinalKPIRR);



            }

            Identifier = 1;
            ///<summary>Bringing about the Setup Changes in each of the corners and assigning the <see cref="SetupChangeDatabase"/>objects to the corners as well</summary>
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_FL[0].sccvOP, _Vehicle.oc_FL, 1, FinalCamberFL, FinalToeFL, FinalCasterFL, FinalKPIFL, FinalRideHeight_FL, FinalPushrod_FL);
            ///<summary>Assigning All the Final Values to the FRONT LEFT Object of the Closed Loop Solver</summary>
            AssignAllFinalValues(1, _Vehicle.oc_FL[0].sccvOP, FinalRideHeight_FL, FinalPushrod_FL);

            Identifier = 2;
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_FR[0].sccvOP, _Vehicle.oc_FR, 2, FinalCamberFR, FinalToeFR, FinalCasterFR, FinalKPIFR, FinalRideHeight_FR, FinalPushrod_FR);
            AssignAllFinalValues(2, _Vehicle.oc_FR[0].sccvOP, FinalRideHeight_FR, FinalPushrod_FR);

            Identifier = 3;
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_RL[0].sccvOP, _Vehicle.oc_RL, 3, FinalCamberRL, FinalToeRL, FinalCasterRL, FinalKPIRL, FinalRideHeight_RL, FinalPushrod_RL);
            AssignAllFinalValues(3, _Vehicle.oc_RL[0].sccvOP, FinalRideHeight_RL, FinalPushrod_RL);

            Identifier = 4;
            SetupChange_InvokeChangeSolvers(_Vehicle.oc_RR[0].sccvOP, _Vehicle.oc_RR, 4, FinalCamberRR, FinalToeRR, FinalCasterRR, FinalKPIRR, FinalRideHeight_RR, FinalPushrod_RR);
            AssignAllFinalValues(4, _Vehicle.oc_RR[0].sccvOP, FinalRideHeight_RR, FinalPushrod_RR);
        }

        /// <summary>
        /// Public method to initialize the <see cref="SetupChangeDatabase"/> object of this class and assign the <see cref="OutputClass.scmOP"/>'s coordinate values to the local variables of this class
        /// This method will also need to be called by the <seealso cref="SetupChange_ClosedLoopSolver"/> Class while performing the Closed Loop Simulation
        /// </summary>
        /// <param name="_requestedChanges"></param>
        /// <param name="_oc"></param>
        private void SetupChange_InitializeSetupChange(SetupChange_CornerVariables _requestedChanges, OutputClass _oc, Dictionary<string, AdjustmentTools> _adjToolDictionary)
        {
            ///<summary>Initializing the SetupChangeDatabase's Master Object</summary>
            SetupChange_DB_Master = null;
            SetupChange_DB_Master = new SetupChangeDatabase();

            ///<summary>Assigning the <see cref="SolverMasterClass"/> Class' local variables with the <see cref="OutputClass.scmOP"/> values</summary>
            AssignLocalCoordinateVariables_FixesPoints(_oc.scmOP);
            AssignLocalCoordinateVariables_MovingPoints(_oc.scmOP);
            L1x = _oc.scmOP.L1x; L1y = _oc.scmOP.L1y; L1z = _oc.scmOP.L1z;

            ///<summary>Initializing the Points and Vectors of the Wheel Assembly</summary>
            SetupChange_DB_Master.InitializePointsAndVectors(_requestedChanges, _oc.scmOP, _adjToolDictionary);
        }

        /// <summary>
        /// <para>Used when the there is a pushrod or Ride Height Change happeneing</para>
        /// <para>When this happens, the OutputClass Parameters (which are used to check the final results) are changed and so they need to be recalculated</para> 
        /// <para>This method is used to edit the Output Class Parameters to reflect the actual change that the user wants</para>
        /// </summary>
        /// <param name="_oc"></param>
        /// <param name="_finalCamber">Final Camber of the CornerWhich was Calculated before any Ride height Changes </param>
        /// <param name="_finalToe">Final Toe of the CornerWhich was Calculated before any Ride height Changes </param>
        /// <param name="_finalCaster">Final Caster of the CornerWhich was Calculated before any Ride height Changes </param>
        /// <param name="_finalKPI">Final KPI of the CornerWhich was Calculated before any Ride height Changes </param>
        private void SetupChange_EditSetupValues(OutputClass _oc, Angle _finalCamber, Angle _finalToe, Angle _finalCaster, Angle _finalKPI)
        {
            _oc.sccvOP.deltaCamber = (_finalCamber.Radians - _oc.waOP.StaticCamber) * (180 / Math.PI);

            _oc.sccvOP.deltaToe = (_finalToe.Radians - _oc.waOP.StaticToe) * (180 / Math.PI);

            _oc.sccvOP.deltaCaster = (_finalCaster.Radians - _oc.Caster) * (180 / Math.PI);

            _oc.sccvOP.deltaKPI = (_finalKPI.Radians - _oc.KPI) * (180 / Math.PI);
        }

        /// <summary>
        /// Method to Assign the <see cref="OutputClass"/> Object of each corner with the new values of Setup Paramaeters using the changes which the user has requested
        /// </summary>
        /// <param name="_oc"></param>
        /// <param name="_requestedChanges"></param>
        private void SetupChange_AssignNewSetupValues(List<OutputClass> _oc, SetupChange_CornerVariables _requestedChanges, ref Angle _finalCamber, ref Angle _finalToe, ref Angle _finalCaster, ref Angle _finalKPI)
        {
            ///<summary>Calculating the Final Value of Camber that is requested by the User. No change in parameter's value if requested change is 0</summary>
            Angle deltaCamber = new Angle(_requestedChanges.deltaCamber, AngleUnit.Degrees);
            //_oc[0].waOP.StaticCamber += deltaCamber.Radians;
            _finalCamber = new Angle(_oc[0].waOP.StaticCamber + deltaCamber.Radians, AngleUnit.Radians);

            ///<summary>Calculating the Final Value of Toe that is requested by the User. No change in parameter's value if requested change is 0</summary>
            Angle deltaToeReq = new Angle(_requestedChanges.deltaToe, AngleUnit.Degrees);
            //_oc[0].waOP.StaticToe += deltaToeReq.Radians;
            _finalToe = new Angle(_oc[0].waOP.StaticToe + deltaToeReq.Radians, AngleUnit.Radians);

            ///<summary>Calculating the Final Value of Caster that is requested by the User. No change in parameter's value if requested change is 0</summary>
            Angle deltaCaster = new Angle(_requestedChanges.deltaCaster, AngleUnit.Degrees);
            //_oc[0].Caster += deltaCaster.Radians;
            _finalCaster = new Angle(_oc[0].Caster + deltaCaster.Radians, AngleUnit.Radians);

            ///<summary>Calculating the Final Value of KPI that is requested by the User. No change in parameter's value if requested change is 0</summary>
            Angle deltaKPI = new Angle(_requestedChanges.deltaKPI, AngleUnit.Degrees);
            //_oc[0].KPI += deltaKPI.Radians;
            _finalKPI = new Angle(_oc[0].KPI + deltaKPI.Radians, AngleUnit.Radians);

            ///<summary>Calculating the Final Value of Ride Height that is requested by the User. No change in parameter's value if requested change is 0</summary>
            _oc[0].FinalRideHeight += _requestedChanges.deltaRideHeight;

        }

        /// <summary>
        /// Initializing a Random Class object
        /// </summary>
        Random AxisAndAdjusterSelecter = new Random();
        /// <summary>
        /// 
        /// </summary>
        List<int> randomIndexSelecter = new List<int>();
        
        /// <summary>
        /// Method to assign the <see cref="AdjustmentTools"/> and Axis of Rotation to the <see cref="AdjustmentOptions.MCasterAdjustmenterLine"/> and <see cref="AdjustmentOptions.MKPIAdjusterLine"/>.
        /// This method uses a Random Assigner in sync with <see cref="SetupChange_CornerVariables.LinkLengthsWhichHaveNotChanged"/> List to decide the Master Adjuster and Axis of Rotation based on which Links have been left free of any Change
        /// </summary>
        private void SetupChange_DecideAxisAndAdjuster_KPI(SetupChange_CornerVariables _requestedChange)
        {
            int axisAndAdjusterSelecter_KPI = -1;
            int tempRandom = 0;

            AdjustmentTools kpiTool = new AdjustmentTools();

            /////<summary>Using the Index Numbers of the <see cref="SetupChange_CornerVariables.LinkLengthsWhichHaveNotChanged"/> to generate and store 2 Random Numbers</summary>
            //tempRandom = AxisAndAdjusterSelecter.Next(0, _requestedChange.LinkLengthsWhichHaveNotChanged.Count - 1);
            //randomIndexSelecter.Add(tempRandom);


            ///<summary>Assinging the <see cref="AdjustmentTools"/> using their integer counterparts as descibred in the <see cref="AdjustmentTools"/> Enum</summary>
            if (_requestedChange.LinkLengthsWhichHaveNotChanged.Count != 0)
            {
                for (int i = 0; i < _requestedChange.LinkLengthsWhichHaveNotChanged.Count; i++)
                {
                    axisAndAdjusterSelecter_KPI = _requestedChange.LinkLengthsWhichHaveNotChanged[i];
                    //_requestedChange.LinkLengthsWhichHaveNotChanged.RemoveAt(0);
                    kpiTool = (AdjustmentTools)axisAndAdjusterSelecter_KPI;

                    if (kpiTool != _requestedChange.casterAdjustmentTool)
                    {
                        break;
                    }

                }

                ///<summary>Assigning the Axis and Mater Adjuster for the Caster and KPI Adjustments</summary>
                _requestedChange.kpiAdjustmentTool = kpiTool;
                _requestedChange.AdjToolsDictionary["KPIChange"] = kpiTool;
                SetupChange_DB_Master.AdjOptions.AssignMasterAdjusters(_requestedChange, _requestedChange.AdjToolsDictionary, SetupChange_DB_Master);
            }
            else
            {
                ///Add Convergence String here
            }

        }

        /// <summary>
        /// Method to assign the <see cref="AdjustmentTools"/> and Axis of Rotation to the <see cref="AdjustmentOptions.MCasterAdjustmenterLine"/> and <see cref="AdjustmentOptions.MKPIAdjusterLine"/>.
        /// This method uses a Random Assigner in sync with <see cref="SetupChange_CornerVariables.LinkLengthsWhichHaveNotChanged"/> List to decide the Master Adjuster and Axis of Rotation based on which Links have been left free of any Change
        /// </summary>
        /// <param name="_requestedChanges"></param>
        private void SetupChange_DecideAxisAndAdjuster_Caster(SetupChange_CornerVariables _requestedChanges)
        {
            int axisAndAdjusterSelecter_Caster = -1;

            AdjustmentTools casterTool = new AdjustmentTools();

            ///<summary>Assinging the <see cref="AdjustmentTools"/> using their integer counterparts as descibred in the <see cref="AdjustmentTools"/> Enum</summary>
            if (_requestedChanges.LinkLengthsWhichHaveNotChanged.Count != 0)
            {
                for (int i = 0; i < _requestedChanges.LinkLengthsWhichHaveNotChanged.Count; i++)
                {
                    axisAndAdjusterSelecter_Caster = _requestedChanges.LinkLengthsWhichHaveNotChanged[i];
                    casterTool = (AdjustmentTools)axisAndAdjusterSelecter_Caster;

                    if (casterTool != _requestedChanges.kpiAdjustmentTool)
                    {
                        break;
                    }
                }

                ///<summary>Assigning the Axis and Mater Adjuster for the Caster and KPI Adjustments</summary>
                _requestedChanges.casterAdjustmentTool = casterTool;
                _requestedChanges.AdjToolsDictionary["CasterChange"] = casterTool;
                SetupChange_DB_Master.AdjOptions.AssignMasterAdjusters(_requestedChanges, _requestedChanges.AdjToolsDictionary, SetupChange_DB_Master);
            }
            else
            {
                ///Add Convergence String here
            }



        }

        private void SetupChange_PrimaryInitializeMethod(SetupChange_CornerVariables _requestedChanges, List<OutputClass> _oc, Angle finalCamber, Angle finalToe, Angle finalCaster, Angle finalKPI)
        {
            ///<summary>Initializing the <see cref="SetupChangeDatabase"/> object of this class and assigning the <see cref="OutputClass.scmOP"/>'s coordinate values to the local variables of this class</summary>
            SetupChange_InitializeSetupChange(_requestedChanges, _oc[0], _requestedChanges.AdjToolsDictionary);

            ///<remarks>Constructing the <see cref="SetupChange_ClosedLoopSolver"/> object before calling the <see cref="SetupChange_AssignNewSetupValues(List{OutputClass}, SetupChange_CornerVariables)"/> so that the static values of Camber, Caster, Toe etc are stored in the 
            ///first posotion of the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> and other lists. This way the minute I make the first pass through the <see cref="SetupChange_CamberChange(double, OutputClass, int, int, bool)"/> or any other Setup Change Method, the 2nd position 
            ///of the lists have the delta values 
            /// </remarks>
            SetupChange_CLS_Master = null;
            SetupChange_CLS_Master = new SetupChange_ClosedLoopSolver(this, _oc, ref SetupChange_DB_Master.SetupChangeOPDictionary, finalCamber, finalToe, finalCaster, finalKPI);

            /////<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            //SetupChange_AssignNewSetupValues(_oc, _requestedChanges);
        }

        /// <summary>
        /// Base Invoker method to do initialization work and then invoke the remaining SetupChange Methods
        /// </summary>
        /// <param name="_RequestedChanges">Object of the <see cref="SetupChange_CornerVariables"/> which contains the user's requested changes. </param>
        /// <param name="_Oc"></param>
        /// <param name="_Identifier">Corner Identifier</param>
        private void SetupChange_InvokeChangeSolvers(SetupChange_CornerVariables _RequestedChanges, List<OutputClass> _Oc, int _Identifier, Angle _FinalCamber, Angle _FinalToe, Angle _FinalCaster, Angle _FinalKPI, double _FinalRideHeight, double _FinalPushrod)
        {
            SetupChange_PrimaryInitializeMethod(_RequestedChanges, _Oc, _FinalCamber, _FinalToe, _FinalCaster, _FinalKPI);

            /////<summary>Initializing the <see cref="SetupChangeDatabase"/> object of this class and assigning the <see cref="OutputClass.scmOP"/>'s coordinate values to the local variables of this class</summary>
            //SetupChange_InitializeSetupChange(_RequestedChanges, _Oc[0], _RequestedChanges.AdjToolsDictionary);

            /////<remarks>Constructing the <see cref="SetupChange_ClosedLoopSolver"/> object before calling the <see cref="SetupChange_AssignNewSetupValues(List{OutputClass}, SetupChange_CornerVariables)"/> so that the static values of Camber, Caster, Toe etc are stored in the 
            /////first posotion of the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> and other lists. This way the minute I make the first pass through the <see cref="SetupChange_CamberChange(double, OutputClass, int, int, bool)"/> or any other Setup Change Method, the 2nd position 
            /////of the lists have the delta values 
            ///// </remarks>
            //SetupChange_CLS = new SetupChange_ClosedLoopSolver(this, _Oc, ref SetupChange_DB_Master.SetupChangeOPDictionary);

            /////<summary>Finding the final values of the Setup Parameters by adding the changes in the corresponding parameters which the user has requested</summary>
            //SetupChange_AssignNewSetupValues(_Oc, _RequestedChanges);

            ///<summary>Selecting the Links for KPI and Caster Changes in case the user has not selected them from the combobox provided AND Caster/KPI const or change is requested</summary>
            if (!_RequestedChanges.OverrideRandomSelectorForKPI)
            {
                SetupChange_DecideAxisAndAdjuster_KPI(_RequestedChanges);
            }

            if (!_RequestedChanges.OverrideRandomSelectorForCaster)
            {
                SetupChange_DecideAxisAndAdjuster_Caster(_RequestedChanges);
            }

            ///<summary>The IF loops below decide which Parameter change is the starting point for the Closed Loop Solve</summary>
            if (_RequestedChanges.LinkLengthChanged)
            {
                if (_RequestedChanges.deltaTopFrontArm != 0/*true*/)
                {
                    SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaTopFrontArm, _RequestedChanges.deltaTopRearArm, SetupChange_DB_Master.LBJToToeLink.Line.DeltaLine[SetupChange_DB_Master.LBJToToeLink.Line.DeltaLine.Count - 1],
                                                                         SetupChange_DB_Master.AdjOptions.TopFrontArm, SetupChange_DB_Master.AdjOptions.TopFrontVector, SetupChange_DB_Master.AdjOptions.TopRearArm, SetupChange_DB_Master.AdjOptions.TopRearVector,
                                                                         AdjustmentTools.TopFrontArm, SetupChange_CLS_Master.Final_TopFrontArm, SetupChange_CLS_Master.Final_TopRearArm, _Oc, 0, SetupChange_DB_Master.AdjOptions.TopWishbonePlane, _RequestedChanges);
                    //SetupChange_LinkLengthChange_Helper_EditCounterWishbone(SetupChange_DB_Master.AdjOptions.TopFrontArm, SetupChange_DB_Master.AdjOptions.TopRearArm, SetupChange_DB_Master.AdjOptions.TopRearVector, SetupChange_CLS.Final_TopRearArm);
                }
                if (_RequestedChanges.deltaTopRearArm != 0/*true*/)
                {
                    //SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaTopRearArm, SetupChange_DB_Master.AdjOptions.TopRearArm, SetupChange_DB_Master.AdjOptions.TopRearVector, AdjustmentTools.TopRearArm, SetupChange_CLS.Final_TopRearArm, _Oc);

                    SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaTopRearArm, _RequestedChanges.deltaTopFrontArm, SetupChange_DB_Master.LBJToToeLink.Line.DeltaLine[SetupChange_DB_Master.LBJToToeLink.Line.DeltaLine.Count - 1],
                                                                         SetupChange_DB_Master.AdjOptions.TopRearArm, SetupChange_DB_Master.AdjOptions.TopRearVector, SetupChange_DB_Master.AdjOptions.TopFrontArm, SetupChange_DB_Master.AdjOptions.TopFrontVector,
                                                                         AdjustmentTools.TopRearArm, SetupChange_CLS_Master.Final_TopRearArm, SetupChange_CLS_Master.Final_TopFrontArm, _Oc, 0, SetupChange_DB_Master.AdjOptions.TopWishbonePlane, _RequestedChanges);

                }
                if (_RequestedChanges.deltaBottmFrontArm != 0 /*true*/)
                {
                    //SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaBottmFrontArm, SetupChange_DB_Master.AdjOptions.BottomFrontArm, SetupChange_DB_Master.AdjOptions.BottomFrontArmVector, AdjustmentTools.BottomFrontArm, SetupChange_CLS.Final_BottomFrontArm, _Oc);


                    SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaBottmFrontArm, _RequestedChanges.deltaBottomRearArm, SetupChange_DB_Master.UBJToToeLink.Line.DeltaLine[SetupChange_DB_Master.UBJToToeLink.Line.DeltaLine.Count - 1],
                                                                         SetupChange_DB_Master.AdjOptions.BottomFrontArm, SetupChange_DB_Master.AdjOptions.BottomFrontArmVector, SetupChange_DB_Master.AdjOptions.BottomRearArm, SetupChange_DB_Master.AdjOptions.BottomRearArmVector,
                                                                         AdjustmentTools.BottomFrontArm, SetupChange_CLS_Master.Final_BottomFrontArm, SetupChange_CLS_Master.Final_BottomRearArm, _Oc, 1, SetupChange_DB_Master.AdjOptions.BottomWishbonePlane, _RequestedChanges);

                }
                if (_RequestedChanges.deltaBottomRearArm != 0 /*true*/)
                {
                    //SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaBottomRearArm, SetupChange_DB_Master.AdjOptions.BottomRearArm, SetupChange_DB_Master.AdjOptions.BottomRearArmVector, AdjustmentTools.BottomRearArm, SetupChange_CLS.Final_BottomRearArm, _Oc);

                    SetupChange_LinkLengthChange_Helper_ChangeLinkLength(_RequestedChanges.deltaBottomRearArm, _RequestedChanges.deltaBottmFrontArm, SetupChange_DB_Master.UBJToToeLink.Line.DeltaLine[SetupChange_DB_Master.UBJToToeLink.Line.DeltaLine.Count - 1],
                                                                         SetupChange_DB_Master.AdjOptions.BottomRearArm, SetupChange_DB_Master.AdjOptions.BottomRearArmVector, SetupChange_DB_Master.AdjOptions.BottomFrontArm, SetupChange_DB_Master.AdjOptions.BottomFrontArmVector,
                                                                         AdjustmentTools.BottomRearArm, SetupChange_CLS_Master.Final_BottomRearArm, SetupChange_CLS_Master.Final_BottomFrontArm, _Oc, 1, SetupChange_DB_Master.AdjOptions.BottomWishbonePlane, _RequestedChanges);

                }
                if (_RequestedChanges.deltaToeLinkLength != 0)
                {
                    ///<summary>
                    ///Calculating the Angle to be rotated for a given ToeLink Increase. Achieved by extending the <see cref="AdjustmentOptions.MToeAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s ToeLinkUpright Point to the new
                    ///Point created by the <see cref="AdjustmentOptions.MToeAdjusterLine"/> and then finding the angle between the 2 Upright positions
                    ///</summary>
                    Angle AngleToBeRotated = SetupChange_ToeLinkLengthChanged(_RequestedChanges.deltaToeLinkLength);
                    ///<summary>Invoking the Actual Toe Change Method by passing the Angle calculated above for a requested change in Toe Link Length </summary>
                    SetupChange_ToeChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MToeAdjusterLine, SetupChange_DB_Master.AdjOptions.MToeAdjusterVector,
                                          SetupChange_DB_Master.AdjOptions.AxisRotation_Toe[SetupChange_DB_Master.AdjOptions.AxisRotation_Toe.Count - 1], 2);
                }
                if (_RequestedChanges.deltaPushrod != 0)
                {
                    SetupChange_RideHeightChange(_Oc[0]);

                    SetupChange_CLS_Master.Final_Pushrod.Add(_FinalPushrod);

                    SetupChange_CLS_Master.Final_RideHeight.Add(_FinalRideHeight);

                    SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.RideHeight);

                    SetupChange_CLS_Master.RHConvergence = Convergence.Successful;

                }

                ///<summary>Assigning the Adjuster for KPI and Caster using a Random function. Only the wishbones which are not changed are considered</summary>
                //if (!_RequestedChanges.OverrideRandomSelectorForKPI)
                //{
                //    SetupChange_DecideAxisAndAdjuster_KPI(_RequestedChanges);
                //}

                //if (!_RequestedChanges.OverrideRandomSelectorForCaster)
                //{
                //    SetupChange_DecideAxisAndAdjuster_Caster(_RequestedChanges);
                //}

                SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.LinkLength);
            }

            else if (_RequestedChanges.deltaRideHeight != 0)
            {
                SetupChange_RideHeightChange(_Oc[0]);

                SetupChange_CLS_Master.Final_Pushrod.Add(_FinalPushrod);

                SetupChange_CLS_Master.Final_RideHeight.Add(_FinalRideHeight);

                SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.RideHeight);

                SetupChange_CLS_Master.RHConvergence = Convergence.Successful;

            }

            else if (_RequestedChanges.deltaKPI != 0 || _RequestedChanges.deltaTopFrontArm != 0)
            {
                if (_RequestedChanges.kpiAdjustmentType == AdjustmentType.Indirect)
                {
                    /////<summary>
                    /////Calculating the Angle to be rotated for a given Adjuster(wishbone) Length Increase. Achieved by extending the <see cref="AdjustmentOptions.MKPIAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s UBJ or LBJ Point to the new
                    /////Point created by the <see cref="AdjustmentOptions.MKPIAdjusterLine"/> and then finding the angle between the 2 Upright positions
                    /////</summary>
                    //Angle AngleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(_RequestedChanges.deltaTopFrontArm);
                    /////<summary>Invoking the Actual Camber Change Method by passing the Angle calculated above for a requested change in Camber Shims </summary>
                    //SetupChange_KPIChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine, SetupChange_DB_Master.AdjOptions.MKPIAdjusterVector,
                    //                      SetupChange_DB_Master.AdjOptions.AxisRotation_KPI[SetupChange_DB_Master.AdjOptions.AxisRotation_KPI.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForKPI);
                }
                else
                {
                    SetupChange_KPIChange(_RequestedChanges.deltaKPI, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine, SetupChange_DB_Master.AdjOptions.MKPIAdjusterVector,
                      SetupChange_DB_Master.AdjOptions.AxisRotation_KPI[SetupChange_DB_Master.AdjOptions.AxisRotation_KPI.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForKPI);
                }

                SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.KPI);
            }

            else if (_RequestedChanges.deltaCaster != 0 || _RequestedChanges.deltaBottmFrontArm != 0)
            {
                if (_RequestedChanges.casterAdjustmentType == AdjustmentType.Indirect)
                {
                    /////<summary>
                    /////Calculating the Angle to be rotated for a given Adjuster(wishbone) Length Increase. Achieved by extending the <see cref="AdjustmentOptions.MCasterAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s UBJ or LBJ Point to the new
                    /////Point created by the <see cref="AdjustmentOptions.MCamberAdjusterLine"/> and then finding the angle between the 2 Upright positions
                    /////</summary>
                    //Angle AngleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(_RequestedChanges.deltaBottmFrontArm);
                    /////<summary>Invoking the Actual Camber Change Method by passing the Angle calculated above for a requested change in Camber Shims </summary>
                    //SetupChange_CasterChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine, SetupChange_DB_Master.AdjOptions.MCasterAdjusterVector, 
                    //                         SetupChange_DB_Master.AdjOptions.AxisRotation_Caster[SetupChange_DB_Master.AdjOptions.AxisRotation_Caster.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForCaster);
                }
                else
                {
                    SetupChange_CasterChange(_RequestedChanges.deltaCaster, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine, SetupChange_DB_Master.AdjOptions.MCasterAdjusterVector,
                         SetupChange_DB_Master.AdjOptions.AxisRotation_Caster[SetupChange_DB_Master.AdjOptions.AxisRotation_Caster.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForCaster);
                }

                SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.Caster);

            }

            else if (_RequestedChanges.deltaCamber != 0 || _RequestedChanges.deltaCamberShims != 0)
            {
                ///<remarks>Now the Camber Change has been established as the starting point for the Closed Loop </remarks>

                ///<summary>Invoking the Camber Change method</summary>
                if (_RequestedChanges.camberAdjustmentType == AdjustmentType.Indirect)
                {
                    ///<summary>
                    ///Calculating the Angle to be rotated for a given Shim Increase. Achieved by extending the <see cref="AdjustmentOptions.MCamberAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s UBJ or LBJ Point to the new
                    ///Point created by the <see cref="AdjustmentOptions.MCamberAdjusterLine"/> and then finding the angle between the 2 Upright positions
                    ///</summary>
                    Angle AngleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(_RequestedChanges.camberShimThickness * _RequestedChanges.deltaCamberShims, SetupChange_DB_Master.UBJ, SetupChange_DB_Master.LBJ, _RequestedChanges.camberAdjustmentTool);
                    ///<summary>Invoking the Actual Camber Change Method by passing the Angle calculated above for a requested change in Camber Shims </summary>
                    SetupChange_CamberChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine, SetupChange_DB_Master.AdjOptions.MCamberAdjusterVector,
                                             SetupChange_DB_Master.AdjOptions.AxisRotation_Camber[SetupChange_DB_Master.AdjOptions.AxisRotation_Camber.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForCamber);
                }
                else
                {
                    ///<summary>Invoking the Actual Camber Chsnge method for SetupChange Camber requested by the User </summary>
                    SetupChange_CamberChange(_RequestedChanges.deltaCamber, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine, SetupChange_DB_Master.AdjOptions.MCamberAdjusterVector,
                                             SetupChange_DB_Master.AdjOptions.AxisRotation_Camber[SetupChange_DB_Master.AdjOptions.AxisRotation_Camber.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForCamber);
                }

                SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.Camber);
            }

            else if (_RequestedChanges.deltaToe != 0 || _RequestedChanges.deltaToeLinkLength != 0)
            {
                ///<remarks>Now the Toe Change has been established as the starting point for the Closed Loop </remarks>
                if (_RequestedChanges.toeAdjustmentType == AdjustmentType.Indirect)
                {
                    /////<summary>
                    /////Calculating the Angle to be rotated for a given ToeLink Increase. Achieved by extending the <see cref="AdjustmentOptions.MToeAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s ToeLinkUpright Point to the new
                    /////Point created by the <see cref="AdjustmentOptions.MToeAdjusterLine"/> and then finding the angle between the 2 Upright positions
                    /////</summary>
                    //Angle AngleToBeRotated = SetupChange_ToeLinkLengthChanged(_RequestedChanges.deltaToeLinkLength);
                    /////<summary>Invoking the Actual Toe Change Method by passing the Angle calculated above for a requested change in Toe Link Length </summary>
                    //SetupChange_ToeChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_FL.AdjOptions.MToeAdjusterLine, SetupChange_DB_FL.AdjOptions.MToeAdjusterVector, 
                    //                      SetupChange_DB_Master.AdjOptions.AxisRotation_Toe[SetupChange_DB_Master.AdjOptions.AxisRotation_Toe.Count - 1], 2);
                }
                else
                {
                    ///<summary>Invoking the Actual Toe Chsnge method for SetupChange Toe requested by the User </summary>
                    SetupChange_ToeChange(_RequestedChanges.deltaToe, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MToeAdjusterLine, SetupChange_DB_Master.AdjOptions.MToeAdjusterVector,
                      SetupChange_DB_Master.AdjOptions.AxisRotation_Toe[SetupChange_DB_Master.AdjOptions.AxisRotation_Toe.Count - 1], 2);
                }

                SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.Toe);

            }

            //else if (_RequestedChanges.deltaCaster != 0 || _RequestedChanges.deltaBottmFrontArm != 0)
            //{
            //    if (_RequestedChanges.casterAdjustmentType == AdjustmentType.Indirect)
            //    {
            //        /////<summary>
            //        /////Calculating the Angle to be rotated for a given Adjuster(wishbone) Length Increase. Achieved by extending the <see cref="AdjustmentOptions.MCasterAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s UBJ or LBJ Point to the new
            //        /////Point created by the <see cref="AdjustmentOptions.MCamberAdjusterLine"/> and then finding the angle between the 2 Upright positions
            //        /////</summary>
            //        //Angle AngleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(_RequestedChanges.deltaBottmFrontArm);
            //        /////<summary>Invoking the Actual Camber Change Method by passing the Angle calculated above for a requested change in Camber Shims </summary>
            //        //SetupChange_CasterChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine, SetupChange_DB_Master.AdjOptions.MCasterAdjusterVector, 
            //        //                         SetupChange_DB_Master.AdjOptions.AxisRotation_Caster[SetupChange_DB_Master.AdjOptions.AxisRotation_Caster.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForCaster);
            //    }
            //    else
            //    {
            //        SetupChange_CasterChange(_RequestedChanges.deltaCaster, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine, SetupChange_DB_Master.AdjOptions.MCasterAdjusterVector,
            //             SetupChange_DB_Master.AdjOptions.AxisRotation_Caster[SetupChange_DB_Master.AdjOptions.AxisRotation_Caster.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForCaster);
            //    }

            //    SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.Caster);

            //}

            //else if (_RequestedChanges.deltaKPI != 0 || _RequestedChanges.deltaTopFrontArm != 0)
            //{
            //    if (_RequestedChanges.kpiAdjustmentType == AdjustmentType.Indirect)
            //    {
            //        /////<summary>
            //        /////Calculating the Angle to be rotated for a given Adjuster(wishbone) Length Increase. Achieved by extending the <see cref="AdjustmentOptions.MKPIAdjusterLine"/>, taking the <see cref="SetupChangeDatabase.UprightTriangle"/>'s UBJ or LBJ Point to the new
            //        /////Point created by the <see cref="AdjustmentOptions.MKPIAdjusterLine"/> and then finding the angle between the 2 Upright positions
            //        /////</summary>
            //        //Angle AngleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(_RequestedChanges.deltaTopFrontArm);
            //        /////<summary>Invoking the Actual Camber Change Method by passing the Angle calculated above for a requested change in Camber Shims </summary>
            //        //SetupChange_KPIChange(AngleToBeRotated.Degrees, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine, SetupChange_DB_Master.AdjOptions.MKPIAdjusterVector,
            //        //                      SetupChange_DB_Master.AdjOptions.AxisRotation_KPI[SetupChange_DB_Master.AdjOptions.AxisRotation_KPI.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForKPI);
            //    }
            //    else
            //    {
            //        SetupChange_KPIChange(_RequestedChanges.deltaKPI, _Oc[0], false, _RequestedChanges, SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine, SetupChange_DB_Master.AdjOptions.MKPIAdjusterVector,
            //          SetupChange_DB_Master.AdjOptions.AxisRotation_KPI[SetupChange_DB_Master.AdjOptions.AxisRotation_KPI.Count - 1], SetupChange_DB_Master.AdjOptions.UprightIndexForKPI);
            //    }

            //    SetupChange_CLS_Master.ClosedLoop_Solver(CurrentChange.KPI);
            //}



            SetupChange_AssignSetupChangeDatabase(_Identifier);

        }

        /// <summary>
        /// Method to Calculate the return the ride height based on the type of Adjustment and Adjustment tool the user has selected
        /// </summary>
        /// <param name="_reqChanges"></param>
        /// <param name="_line"></param>
        /// <param name="_vector"></param>
        /// <param name="_adjType"></param>
        /// <param name="_adjTools"></param>
        /// <returns>Change in Ride height either directly or indirectly requested by the user</returns>
        private double SetupChange_GetRideHeightChange(SetupChange_CornerVariables _reqChanges, List<Line> _line, List<Vector3D> _vector, AdjustmentType _adjType, AdjustmentTools _adjTools, int identifier, out double _finalPushrod)
        {
            double _dRideHeight = 0;

            ///<summary>Variable which will determine the sign which is to be used for the calculate the Horizontal to the Pushrod. I need this because I always want the horizontal to move inwards and towards the center of the Vehicle from the Pushrod End poing</summary>
            int sign = 1;
            if (identifier == 1 || identifier == 3)
            {
                sign = -1;
            }
            else if (identifier == 2 || identifier == 4)
            {
                sign = 1;
            }

            Line PushrodLineforAngle = (_line[_line.Count - 1]);

            ///<remarks> 
            ///For LEFT Corner - Having a Line which starts from the Pushrod Upright Point and extends towards centre Line results in a Positive Angle greater than 90. 
            ///
            /// </remarks>
            Line HorzFromPushrod = new Line(PushrodLineforAngle.EndPoint, new Point3D(PushrodLineforAngle.EndPoint.X + (sign * 100), PushrodLineforAngle.EndPoint.Y, PushrodLineforAngle.EndPoint.Z));

            Line LongFromPushrod = new Line(PushrodLineforAngle.EndPoint, new Point3D(PushrodLineforAngle.EndPoint.X, PushrodLineforAngle.EndPoint.Y, PushrodLineforAngle.EndPoint.Z + 100));

            Angle PushrodAngleWithHorz = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(PushrodLineforAngle), Custom3DGeometry.GetMathNetVector3D(HorzFromPushrod), Custom3DGeometry.GetMathNetVector3D(LongFromPushrod));

            double PushrodDelta = 0;

            ///<summary>Assigning/Calculating the Ride Height Change either as a <see cref="AdjustmentType.Direct"/> input from the user or by using the <see cref="AdjustmentTools.PushrodLength"/>'s SINE component  </summary>
            if (_adjType == AdjustmentType.Direct)
            {
                _dRideHeight = _reqChanges.deltaRideHeight;
                PushrodDelta = _dRideHeight / Math.Sin((Math.PI) - Math.Abs(PushrodAngleWithHorz.Radians));
                SetupChange_DB_Master.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(_line, _vector, PushrodDelta);

            }

            else
            {
                if (_adjTools == AdjustmentTools.PushrodLength)
                {
                    SetupChange_DB_Master.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(_line, _vector, _reqChanges.deltaPushrod);
                    PushrodDelta = _line[_line.Count - 1].Length() - _line[0].Length();
                    _dRideHeight = PushrodDelta * Math.Sin((Math.PI) - Math.Abs(PushrodAngleWithHorz.Radians));
                    SetupChange_CLS_Master.Final_Pushrod.Add(PushrodDelta);

                }
            }
            _finalPushrod = PushrodDelta;

            return _dRideHeight;
        }

        /// <summary>
        /// Helper method to change the Link Length of the Primary Link being considered and also pass both the Primiary and Counter Link data to the 
        /// <see cref="SetupChange_LinkLengthChange(double, double, double, OutputClass, Line, List{Line}, List{Vector3D}, List{Line}, List{Vector3D}, List{double}, List{double}, int, AdjustmentTools)"/> method
        /// </summary>
        /// <param name="_deltaLinkLength"></param>
        /// <param name="_deltaCounterLinklength"></param>
        /// <param name="_axisRot"></param>
        /// <param name="_linkLine"></param>
        /// <param name="_linkVector"></param>
        /// <param name="_colinkLine"></param>
        /// <param name="_colinkVector"></param>
        /// <param name="_adjTool"></param>
        /// <param name="_finalLinkLengthList"></param>
        /// <param name="_fincoDList"></param>
        /// <param name="_ocLink"></param>
        /// <param name="_upVertex"></param>
        private void SetupChange_LinkLengthChange_Helper_ChangeLinkLength(double _deltaLinkLength, double _deltaCounterLinklength, Line _axisRot, List<Line> _linkLine, List<Vector3D> _linkVector, List<Line> _colinkLine, List<Vector3D> _colinkVector, AdjustmentTools _adjTool,
                                                                          List<double> _finalLinkLengthList, List<double> _fincoDList, List<OutputClass> _ocLink, int _upVertex, Plane _plane, SetupChange_CornerVariables sccv)
        {

            ///<summary>
            ///Finding the angle to be rotated by the Upright because of the Wishbone length Change. The 3 points of the Triangle are passed as it is. 
            ///The will worked upon inside the <see cref="SolverMasterClass.SetupChange_WishboneLengthChanged(double, List{Line}, List{Vector3D}, Point3D, Point3D, Point3D, AdjustmentTools)"/> method
            ///</summary>
            Angle AngleToBeRotated = SetupChange_WishboneLengthChanged(_deltaLinkLength, _plane, _linkLine, _colinkLine, _linkVector, SetupChange_DB_Master.UBJ, SetupChange_DB_Master.LBJ, SetupChange_DB_Master.ToeLinkUpright, _adjTool, _axisRot);

            ///<summary>Link Length Changed Operations</summary>
            SetupChange_LinkLengthChange(AngleToBeRotated.Degrees, _deltaLinkLength, _deltaCounterLinklength, _ocLink[0], _axisRot, _linkLine, _linkVector, _colinkLine, _colinkVector, _finalLinkLengthList, _fincoDList, _upVertex, _plane, _adjTool, sccv);

        }

        /// <summary>
        /// Method to Edit the Counter Wishbone of a Primary Wishbone. For Example :- If Top Front Arm is changed then this  method should be called to change the Top Rear Arm. 
        /// </summary>
        /// <param name="_primaryLinkLine">Primary Arm which was edited</param>
        /// <param name="_counterLinkLine">Counter Arm which is TO BE Edited</param>
        /// <param name="_counterLinkVector">Counter Arm Vecctor which is to be edited</param>
        /// <param name="_finalCounterLinkLength">Final List of Deltas off the Counter Arm </param>
        private void SetupChange_LinkLengthChange_Helper_EditCounterWishbone(double _counterDeltaLength, List<Line> _primaryLinkLine, List<Line> _counterLinkLine, List<Vector3D> _counterLinkVector, List<double> _finalPrimaryLinkLength, List<double> _finalCounterLinkLength)
        {
            ///<summary>Adding the a Line and Vector to the Counter Wishbone Arm</summary>
            SetupChange_DB_Master.AdjOptions.AddAdjusterToMasterAdjusterList(_counterLinkLine, _counterLinkVector);

            ///<summary>Editing the Point of the Counter Line and creating a new Counter Link Vector</summary>
            _counterLinkLine[_counterLinkLine.Count - 1].EndPoint = _primaryLinkLine[_primaryLinkLine.Count - 1].EndPoint;
            _counterLinkVector[_counterLinkVector.Count - 1] = new Vector3D(_counterLinkLine[_counterLinkLine.Count - 1].StartPoint, _counterLinkLine[_counterLinkLine.Count - 1].EndPoint);

            ///<summary>Getting the Lengths of the Old and New lines</summary>
            double testStatic, testNew;
            testStatic = _counterLinkLine[0].Length();
            testNew = _counterLinkLine[_counterLinkLine.Count - 1].Length();

            ///<summary>
            /// Adding the delta of Inadvertant Link Length to the Final List which holds the Delta Values of Link Length Change
            /// IMPORTANT. In reality this inadvertant Length Change SHOULD NOT HAPPEN. Because the Link Length CANNOT CHANGE INADVERTANTLY as it is fixed. So hope to god that this is as low as possible 
            /// </summary>
            _finalCounterLinkLength.Add(testNew - testStatic);

            double checker = _counterDeltaLength - _finalCounterLinkLength[_finalCounterLinkLength.Count - 1];

            if (Math.Abs(checker) > 0.25)
            {
                SetupChange_DB_Master.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(_counterLinkLine, _counterLinkVector, /*-*/checker);
                _primaryLinkLine[_primaryLinkLine.Count - 1].EndPoint = _counterLinkLine[_counterLinkLine.Count - 1].EndPoint;

                //_finalPrimaryLinkLength.Add(_primaryLinkLine[_primaryLinkLine.Count - 1].Length() - _primaryLinkLine[0].Length());
                _finalCounterLinkLength.Add(_counterLinkLine[_counterLinkLine.Count - 1].Length() - _counterLinkLine[0].Length());

            }

        }

        private void SetupChange_LinkLengthChange_Helper_EditUprightDueToCounter(Point3D _uprightVertex, int _vIndex)
        {
            SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[_vIndex] = _uprightVertex;
        }


        /// <summary>
        /// Method to Calculate the Angle created between the Latest and Previous Position of the Upright for requested change in Camber Shims. The Angle returned will be passed into the <see cref="SetupChange_CamberChange(double, OutputClass, bool, SetupChange_CornerVariables, List{Line}, List{Vector3D}, int)"/> 
        /// </summary>
        /// <param name="deltaShimLength">Change in Camber Shims Length</param>
        /// <returns>Angle subtended by the Latest and Previous position of the Upright as the upright is <see cref="SetupChangeDatabase.UBJ or LBJ"/> point is changed</returns>
        internal Angle SetupChange_CamberShims_OR_ShimsVectorLengthChanged(double deltaShimLength, Point3D _v1, Point3D _v2, AdjustmentTools _adjTool)
        {
            Angle angleToBeRotated = new Angle(0, AngleUnit.Degrees);

            SetupChange_DB_Master.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine, SetupChange_DB_Master.AdjOptions.MCamberAdjusterVector, deltaShimLength);

            if (_adjTool == AdjustmentTools.TopCamberMount)
            {
                _v1 = SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine[SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine.Count - 1].EndPoint /*SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[0]*/;
            }
            else if (_adjTool == AdjustmentTools.BottomCamberMount)
            {
                _v2 = SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine[SetupChange_DB_Master.AdjOptions.MCamberAdjusterLine.Count - 1].EndPoint /*SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[1]*/;
            }


            angleToBeRotated = SetupChange_DB_Master.AdjOptions.GetNewUprightTrianglePosition(ref SetupChange_DB_Master.UprightTriangle, _v1, _v2, SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[2], SetupChange_DB_Master,
                                                                                              SetupChange_DB_Master.AdjOptions.AxisRotation_Camber[SetupChange_DB_Master.AdjOptions.AxisRotation_Camber.Count - 1]);

            return angleToBeRotated;
        }

        /// <summary>
        /// Method to calcualte the Angle Createed by the Upright because of the change in the Wishbone Length
        /// </summary>
        /// <param name="deltaLinkLength"></param>
        /// <param name="_linkLine"></param>
        /// <param name="_linkVector"></param>
        /// <param name="_v1"></param>
        /// <param name="_v2"></param>
        /// <param name="_v3"></param>
        /// <returns></returns>
        internal Angle SetupChange_WishboneLengthChanged(double deltaLinkLength, Plane _planeArms, List<Line> _linkLine, List<Line> _counterLine, List<Vector3D> _linkVector, Point3D _v1, Point3D _v2, Point3D _v3, AdjustmentTools _adjTool, Line _axisRotation)
        {
            Angle angleToBeRotated = new Angle(0, AngleUnit.Degrees);

            SetupChange_DB_Master.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(_linkLine, _linkVector, deltaLinkLength);
            //SetupChange_DB_Master.AdjOptions.LinkLengthChanged(deltaLinkLength, _planeArms, _linkLine,_linkVector, _counterLine, SetupChange_DB_Master, this);

            if (_adjTool == AdjustmentTools.TopFrontArm || _adjTool == AdjustmentTools.TopRearArm)
            {
                _v1 = _linkLine[_linkLine.Count - 1].EndPoint;
            }
            else if (_adjTool == AdjustmentTools.BottomFrontArm || _adjTool == AdjustmentTools.BottomRearArm)
            {
                _v2 = _linkLine[_linkLine.Count - 1].EndPoint;
            }

            angleToBeRotated = SetupChange_DB_Master.AdjOptions.GetNewUprightTrianglePosition(ref SetupChange_DB_Master.UprightTriangle, _v1, _v2, SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[2]/*_v3*/, SetupChange_DB_Master, _axisRotation);

            return angleToBeRotated;
        }

        /// <summary>
        /// Method to Calculate the Angle created between the Latest and Previous Position of the Upright for requested change in Camber Shims. The Angle returned will be passed into the <see cref="SetupChange_CamberChange(double, OutputClass, bool, SetupChange_CornerVariables, List{Line}, List{Vector3D}, int)"/> 
        /// </summary>
        /// <param name="deltaLinkLength">Change in Toe Link Length requested by the User </param>
        /// <returns>Angle subtended by the Latest and Previous position of the Upright as the upright is <see cref="SetupChangeDatabase.ToeLinkUpright"/> point is changed</returns>
        internal Angle SetupChange_ToeLinkLengthChanged(double deltaLinkLength)
        {
            Angle angleToBeRotated = new Angle(0, AngleUnit.Degrees);

            SetupChange_DB_Master.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(SetupChange_DB_Master.AdjOptions.MToeAdjusterLine, SetupChange_DB_Master.AdjOptions.MToeAdjusterVector, deltaLinkLength);
            angleToBeRotated = SetupChange_DB_Master.AdjOptions.GetNewUprightTrianglePosition(ref SetupChange_DB_Master.UprightTriangle, SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[0],
                                                                                                  SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[1],
                                                                                                  SetupChange_DB_Master.AdjOptions.MToeAdjusterLine[SetupChange_DB_Master.AdjOptions.MToeAdjusterLine.Count - 1].EndPoint, SetupChange_DB_Master,
                                                                                                  SetupChange_DB_Master.AdjOptions.AxisRotation_Toe[SetupChange_DB_Master.AdjOptions.AxisRotation_Toe.Count - 1]);

            return angleToBeRotated;
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
        /// Method to return the value of the Internal Iterator check variable. This variable can be considered as a tolerance check variable. If it is lesser than the allowed tolerance, then the Loop which is calling this will terminate.
        /// </summary>
        /// <param name="_undoRotatation">Boolean to determine if this is called by a SetupChange method going through an UnRotation phase</param>
        /// <param name="_checkAngleOfConcern">Angle which is being considered. For example, Camber will be passed if SetupChange method of Camber is calling this</param>
        /// <param name="_oc"></param>
        /// <returns>Returns double value of the Internal Iteration checker</returns>
        private double GetInternalIterationChecker_DirectChange(bool _undoRotatation, Angle _checkAngleOfConcern, double _deltaRequiredDegrees)
        {

            ///<summary>Need an IF loop below to determine if the the method is called as the primary setup change or to Undo an INADVERTENT Setup Change caused because of a primary Setup Change</summary>
            ///
            ///<remarks>
            ///If it is a first pass is happeneing remember that the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> already contains the exact value of Final Camber (summed with the delta of Camber Requested) hence passing the value of 
            ///(dCamber - checkCamber) will result in the addition of a new angle to the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> List which is equal to the Actuall and Current Value of Camber
            ///
            ///If it is an Undopass then an UNROTATION is happening and hence the <see cref="ReCheckCamber()"/> will give you the angle which remainins between the 1st and Latest <see cref="SetupChangeDatabase.WheelSpindle"/> 
            ///Hence, you again need to pass (dCamber - checkCamber) as it will give you the value by which the UNROTATION needs to happen again 
            ///</remarks>
            ///
            if (!_undoRotatation)
            {
                return _checkAngleOfConcern.Degrees - _deltaRequiredDegrees;
            }
            else
            {
                return _checkAngleOfConcern.Degrees - _deltaRequiredDegrees;
            }
        }

        /// <summary>
        /// Method to return the valu of the Internal Iterator check variable when the Setup Change is done indirectly
        /// </summary>
        /// <param name="_checkLengthChange"></param>
        /// <param name="_requiredLengthChange"></param>
        /// <returns></returns>
        private double GetInternalIterationChecker_IndirectChange(double _checkLengthChange, double _requiredLengthChange)
        {
            return _requiredLengthChange - _checkLengthChange;
        }

        /// <summary>
        /// Method to get the PRIMARY (that is, the angle which is being changed by the SetupChange method which is calling this) angle to be assigned to the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> or any other "Final" lists of the <see cref="SetupChange_ClosedLoopSolver"/>
        /// </summary>
        /// <param name="_undoRotatation"></param>
        /// <param name="reRotateForCamber"></param>
        /// <param name="_rotatedAngle"></param>
        /// <param name="checkCamber"></param>
        /// <param name="_historyOfDeltaOfConcernedAngle"></param>
        /// <returns>ACTUAL Camber Angle to be added to the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> or any other "Final" lists of the <see cref="SetupChange_ClosedLoopSolver"/></returns>
        private Angle GetPrimaryAngleToBeAssigned(bool _undoRotatation, bool reRotateForCamber, Angle _rotatedAngle, Angle checkCamber, List<Angle> _historyOfDeltaOfConcernedAngle)
        {
            if (!_undoRotatation)
            {
                if (!reRotateForCamber)
                {
                    return new Angle(_historyOfDeltaOfConcernedAngle[_historyOfDeltaOfConcernedAngle.Count - 1].Degrees, AngleUnit.Degrees);
                }
                else
                {
                    return new Angle(_historyOfDeltaOfConcernedAngle[_historyOfDeltaOfConcernedAngle.Count - 1].Degrees - _historyOfDeltaOfConcernedAngle[_historyOfDeltaOfConcernedAngle.Count - 1 - 1].Degrees, AngleUnit.Degrees);
                }
            }
            else
            {
                //if (!reRotateForCamber)
                //{
                //    return new Angle(_historyOfDeltaOfConcernedAngle[_historyOfDeltaOfConcernedAngle.Count - 1].Degrees, AngleUnit.Degrees);
                //}
                //else
                //{
                return new Angle(_historyOfDeltaOfConcernedAngle[_historyOfDeltaOfConcernedAngle.Count - 1].Degrees - _historyOfDeltaOfConcernedAngle[_historyOfDeltaOfConcernedAngle.Count - 1 - 1].Degrees, AngleUnit.Degrees);
                //}

                return /*_rotatedAngle*/ /*+*//*-*/ checkCamber;
            }
        }

        /// <summary>
        /// Method to perform the Link Length Change Operations of Link Length change and Wheel Assembly Rotate which are methods in the <see cref="SolverMasterClass"/>
        /// </summary>
        /// <param name="_angleRot"></param>
        /// <param name="_dLength"> Primary Link Length Change. Change in Link Length of the Concerned Link requested by the user</param>
        /// <param name="_counterDLength"> Counter Link Change. Change in Link Length of the Counter Link which may have been requested by the user</param>
        /// <param name="_oc"> <see cref="List{T}"/> of the Output Class Objects</param>
        /// <param name="_axisRot"> Axis of Rotation of the Concerned Link Length. <see cref="SetupChangeDatabase.LBJToToeLink"/> OR <see cref="SetupChangeDatabase.UBJToToeLink"/> OR <see cref="SetupChangeDatabase.SteeringAxis"/></param>
        /// <param name="_linkLine"> <see cref="List{T}"/> off the Primary Link <see cref="Line"/> </param>
        /// <param name="_linkVector"> <see cref="List{T}"/> of the Primary Link <see cref="Vector3D"/> </param>
        /// <param name="_coLinkLine"> <see cref="List{T}"/> off the Primary Link <see cref="Line"/> </param>
        /// <param name="_coLinkVector"> <see cref="List{T}"/> of the Primary Link <see cref="Vector3D"/>< </param>
        /// <param name="_finDLength"> <see cref="List{T}"/> which holds the history of Deltas of the Primary Link Length </param>
        /// <param name="_finCoDLength"> <see cref="List{T}"/> which holds the history of Deltas of the Counter Link Length </param>
        /// <param name="_upVert"> Vertex of the <see cref="SetupChangeDatabase.UprightTriangle"/> which is connected to the Primary and Counter Links </param>
        /// <param name="_adjTool"> Enum of the <see cref="AdjustmentTools"/> which represents the Priimary Link Length </param>
        internal void SetupChange_LinkLengthChange(double _angleRot, double _dLength, double _counterDLength, OutputClass _oc, Line _axisRot, List<Line> _linkLine, List<Vector3D> _linkVector, List<Line> _coLinkLine, List<Vector3D> _coLinkVector, List<double> _finDLength,
                                                   List<double> _finCoDLength, int _upVert, Plane _planeArms, AdjustmentTools _adjTool, SetupChange_CornerVariables _sccv)
        {
            ///<summary>Angle to be rotated </summary>
            Angle angleOfRotation = new Angle(_angleRot, AngleUnit.Degrees);

            /////<summary>Label to create an internal Iterative Loop</summary>
            //Start:

            SteeringAxisISRotationAxis = false;
            KPIRotation = false;
            CasterRotation = false;

            SetupChange_CLS_Master.LinkLengthConvergence = Convergence.InProgress;

            for (int i_LinkLength = 0; i_LinkLength < _sccv.iterationsLinkLength; i_LinkLength++)
            {
                ///<summary>Rotating ALL the Components of the Wheel Assmebly</summary>
                RotateWheelAssemblyComponents(_axisRot, angleOfRotation, _oc, CurrentChange.LinkLength);
                RotateSteeringAxis(_axisRot, angleOfRotation, CurrentChange.LinkLength);
                RotateUprightTriangle(_axisRot, angleOfRotation, AdjustmentType.Indirect, CurrentChange.LinkLength);

                ///<summary>
                ///-> Finding the Change in lenth of the concerned Wishbone
                ///-> Editing the CounterWishbone and Upright
                /// </summary>
                double checkLinkActualLengthWithABS = SetupChange_DB_Master.AdjOptions.GetLinkLengthChangeFromUprightAngle(_linkLine, _linkVector, SetupChange_DB_Master, _upVert);
                SetupChange_LinkLengthChange_Helper_EditCounterWishbone(_counterDLength, _linkLine, _coLinkLine, _coLinkVector, _finDLength, _finCoDLength);
                SetupChange_LinkLengthChange_Helper_EditUprightDueToCounter(_linkLine[_linkLine.Count - 1].EndPoint, _upVert);

                ///<summary>The above Operations of Editing the Counter Wishbone and the Upright will have skewed the calculated length of the concerned wishbone so recalculating that. </summary>
                checkLinkActualLengthWithABS = SetupChange_DB_Master.AdjOptions.GetLinkLengthChangeFromUprightAngle(_linkLine, _linkVector, SetupChange_DB_Master, _upVert);
                _finDLength.Add(checkLinkActualLengthWithABS);
                ///<summary>
                ///The method
                /// <see cref="SetupChange_LinkLengthChange_Helper_EditCounterWishbone(double, List{Line}, List{Line}, List{Vector3D}, List{double}, List{double})"/>
                /// Takes care of adding the actual delta to the Final List of Delta Histories of Length Change. Hence the line below is not needed. It is kept only for illustrative purposes to explain why it is not there should I get confused in teh future.
                /// </summary>
                /// _finDLength.Add(checkLinkActualLengthWithABS);


                double checker = GetInternalIterationChecker_IndirectChange(checkLinkActualLengthWithABS, _dLength);
                if (Math.Abs(checker) > 0.5)
                {
                    //angleOfRotation = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(checkLinkActualLengthWithABS);
                    angleOfRotation = SetupChange_WishboneLengthChanged(checker, _planeArms, _linkLine, _coLinkLine, _linkVector, SetupChange_DB_Master.UBJ, SetupChange_DB_Master.LBJ, SetupChange_DB_Master.ToeLinkUpright, _adjTool, _axisRot);
                    //goto Start;
                }
                else
                {
                    break;
                }
            }

            Point3D test = SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[1];

            RecordInadvertantCamberChange(_oc);

            RecordInadvertantToeChange(_oc);

            RecordInadvertantCasterChange(_oc);

            RecordInadvertantChangeKPI(_oc);


        }

        /// <summary>
        /// <para>Exclusive Method to</para>  
        /// <para>1-> Increase the length of the pushrod and find the corresponding Ride Height Change</para>
        /// <para>2-> To Invoke the <see cref="VehicleModel"/> Class and solve the Model for Diagonal Weight Transfer due to Ride Height Change</para>
        /// </summary>
        /// <param name="_dChange"></param>
        /// <param name="_line"></param>
        /// <param name="_vector"></param>
        /// <param name="_finalChange"></param>
        private void SetupChange_RideHeightChange_Helper_SolveForRideHeightChanges(Vehicle _vRideHeight, double _defFL, double _defFR, double _defRL, double _defRR)
        {
            ///<summary>Assigning the <see cref="SuspensionCoordinatesMaster.WheelDeflection_Steering"/> Arrays with the values of the </summary>
            _vRideHeight.sc_FL.WheelDeflection_Steering[0] = _defFL;

            _vRideHeight.sc_FR.WheelDeflection_Steering[0] = _defFR;

            SetupChange_VehicleModel.AssignRearRideHeightChanges(_defRL, _defRR);

            ///<summary>Calling the Vehicle Model Initializer method which also contains the Solvers</summary>
            SetupChange_VehicleModel.InitializeVehicleOutputModel(_vRideHeight, true, false, SimulationType.SetupChange);

            SetupChange_VehicleModel.ComputeVehicleModel_SummationOfResults_For_SetupChange(_vRideHeight, SimulationType.SetupChange);

            ///<summary>Calling the Kinematics Solver to adapt the Suspension to these changes </summary>
            _vRideHeight.KinematicsInvoker(false, SimulationType.SetupChange);

        }

        /// <summary>
        /// Method to Recorrd the Inadvertent Changes caused to other parameters because of the Ride Height Chang e
        /// </summary>
        /// <param name="_oc"></param>
        internal void SetupChange_RideHeightChange(OutputClass _oc)
        {
            SetupChange_CLS_Master.RHConvergence = Convergence.InProgress;

            RecordInadvertantCamberChange(_oc);

            RecordInadvertantToeChange(_oc);

            RecordInadvertantCasterChange(_oc);

            RecordInadvertantChangeKPI(_oc);
        }

        /// <summary>
        /// Method to calculate the Camber change requested by the user as a Setup Change. This method shall also calculate all the other things which chnge with the camber 
        /// </summary>
        /// <param name="_dCamber">Change in camber requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupCamber">Object of the Output Class to store the new value of Camber</param>
        /// <param name="_undoRotatation">This is a boolean which determines if the method is being called to ACTUALLY Make a Setup change or if it is being called to UNDO a change caused by a previous Setup Change</param>
        /// <param name="_sccv"></param>
        /// <param name="_shimsLine">List of the <see cref="AdjustmentOptions.MCamberAdjusterLine"/> lines</param>
        /// <param name="_shimsVector">List of the <see cref="AdjustmentOptions.MCamberAdjusterVector"/> vectors</param>
        /// <param name="_upIndex">Index of the Upright Vertex (UBJ[0] or LBJ[0]) which is to be considered</param>
        internal void SetupChange_CamberChange(double _dCamber, OutputClass _ocSetupCamber, bool _undoRotatation, SetupChange_CornerVariables _sccv, List<Line> _shimsLine, List<Vector3D> _shimsVector, Line _axisOfRotation, int _upIndex)
        {
            ///<summary>Calculating the new camber </summary>
            Angle dCamber = new Angle(_dCamber, AngleUnit.Degrees);

            ///<summary>Boolean internal iteration variable</summary>
            bool reRotateForCamber = false;

            SteeringAxisISRotationAxis = false;
            KPIRotation = false;
            CasterRotation = false;

            SetupChange_CLS_Master.CamberConvergence = Convergence.InProgress;

            /////<remarks>Adding a Label here so I can create a Loop without using a For Statement. Label placed in the 2nd line so that the <see cref="dCamber"/> Angle gets the user requested OR reversal value only once. All other rotations should be by angle <see cref="ReCheckCamber()"/></remarks>
            //Start:

            for (int i_Camber = 0; i_Camber < _sccv.iterationsCamber; i_Camber++)
            {
                SetupChange_DB_Master.SetupChangeOPDictionary["Camber"] = dCamber.Radians;

                ///<summary>
                ///The <see cref="AdjustmentOptions.MCamberAdjusterLine"/> will never have more than one intity if the Setup Change is <see cref="AdjustmentType.Indirect"/>. So the below method 
                ///necessary so that List can be assinged new values 
                /// </summary>
                if (_sccv.camberAdjustmentType == AdjustmentType.Direct)
                {
                    SetupChange_DB_Master.AdjOptions.AddAdjusterToMasterAdjusterList(_shimsLine, _shimsVector);
                }

                ///<summary>Rotating the Wheel Assembyly Points and Vectors using the <see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> </summary>
                ///<remarks>Steering Axis Rotation will be done IF Camber change is done using any of the Wishbones. Will be attempted Later</remarks>
                RotateWheelAssemblyComponents(_axisOfRotation, dCamber, _ocSetupCamber, CurrentChange.Camber);
                RotateUprightTriangle(_axisOfRotation, dCamber, _sccv.camberAdjustmentType, CurrentChange.Camber);

                ///<summary>
                ///Checking if performing the rotation of the <see cref="SetupChangeDatabase.WheelSpindle"/>by the angle <see cref="dCamber"/> about <see cref="SetupChangeDatabase.SteeringAxis.PerpAlongZ"/> 
                ///ACTUALLY resulted in getting a Camber Angle of <see cref="dCamber"/>
                ///IF not then <see cref="SetupChange_CamberChange(double, OutputClass, int, int)"/> is performed again 
                ///</summary>
                Angle checkCamberWithAbs = ReCheckCamber();
                SetupChange_CLS_Master.Summ_dCamber.Add(checkCamberWithAbs);

                ///<summary>
                ///Assinging the Rotated Angle and the ACTUAL ANGLE OF CAMBER to the <see cref="SetupChange_ClosedLoopSolver.RotationsAbout_LBJToToeLinkAxis"/> or any other Axis List
                /// and the <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> List 
                /// </summary>
                AssignCamberDelta(dCamber, SetupChange_CLS_Master.Summ_dCamber[SetupChange_CLS_Master.Summ_dCamber.Count - 1]);

                ///<summary>Finding the change in the <see cref="AdjustmentOptions.MCamberAdjusterLine"/> due to the Rotations perfomed for the Camber</summary>
                double checkShimsLinkLengthWithAbs = SetupChange_DB_Master.AdjOptions.GetLinkLengthChangeFromUprightAngle(_shimsLine, _shimsVector, SetupChange_DB_Master, _upIndex);
                SetupChange_CLS_Master.Summ_dCamberAdjusterLength.Add(checkShimsLinkLengthWithAbs);
                SetupChange_CLS_Master.Final_CamberAdjusterLength.Add(SetupChange_CLS_Master.Summ_dCamberAdjusterLength[SetupChange_CLS_Master.Summ_dCamberAdjusterLength.Count - 1]);

                ///<summary>Repetetive loop to redo the rotation within this pass only if it is found that the required rotation (or UNROTATION) and the Actual Rotation(or UNROTATION) are too far off</summary>
                ///<remarks>The Iterative Loop will proceed based on whether the user hs requested for a direct Angle change in Camber or a Shims Change</remarks>
                if (_sccv.camberAdjustmentType == AdjustmentType.Direct)
                {
                    ///<summary>Checker Variable for the Internal Loop Iterator. <see cref="GetInternalIterationChecker(bool, Angle, OutputClass)"/> for information on this and how it is obtained </summary>
                    double checkerDirect = GetInternalIterationChecker_DirectChange(_undoRotatation, checkCamberWithAbs, _ocSetupCamber.sccvOP.deltaCamber);

                    ///<summary>Internal Iteration code</summary>
                    if (Math.Abs(checkerDirect) > 0.009)
                    {
                        dCamber = new Angle(-checkerDirect, AngleUnit.Degrees);
                        AssignAngleToBeRotatedForCamber(dCamber);
                        reRotateForCamber = true;
                        //goto Start;
                    }
                    else
                    {
                        break;
                    }
                }
                else if ((_sccv.camberAdjustmentType == AdjustmentType.Indirect))
                {
                    ///<summary>Getting the internal iterator for Indirect Setup Change </summary>
                    double checkerIndirect = GetInternalIterationChecker_IndirectChange(checkShimsLinkLengthWithAbs, _sccv.deltaCamberShims * _sccv.camberShimThickness);

                    if (Math.Abs(checkerIndirect) > 0.05)
                    {
                        Angle angleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(checkerIndirect, SetupChange_DB_Master.UBJ, SetupChange_DB_Master.LBJ, _sccv.camberAdjustmentTool);
                        dCamber = new Angle(angleToBeRotated.Degrees, AngleUnit.Degrees);
                        AssignAngleToBeRotatedForCamber(dCamber);
                        reRotateForCamber = true;
                        //goto Start;
                    }
                    else
                    {
                        break;
                    }

                }
            }

            reRotateForCamber = false;

            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight();

            ///<summary>Calculating the change in toe</summary>
            RecordInadvertantToeChange(_ocSetupCamber);

            ///<summary>Calculating the change in Caster</summary>
            RecordInadvertantCasterChange(_ocSetupCamber);

            ///<summary>Calcualting the change in KPI</summary>
            RecordInadvertantChangeKPI(_ocSetupCamber);
        }

        /// <summary>
        /// Method to calcualte the Toe change requested by the user as a Setup Change. This method shall also re-calculate all the other things which change with the Toe
        /// </summary>
        /// <param name="_dToe">Change in Toe requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupToe">Object of the OutputClass to store the value of the Toe</param>
        /// <param name="_undoChange">This is a boolean which determines if the method is being called to ACTUALLY Make a Setup change or if it is being called to UNDO a change caused by a previous Setup Change</param>
        /// <param name="_sccv"></param>
        /// <param name="_shimsLine">List of the <see cref="AdjustmentOptions.MToeAdjusterLine"/> lines</param>
        /// <param name="_shimsVector">List of the <see cref="AdjustmentOptions.MToeAdjusterVector"/> vectors</param>
        /// <param name="_upIndex">Index of the Upright Vertex (for now only ToeLinkUpright[2] is the option) which is to be considered</param>
        internal void SetupChange_ToeChange(double _dToe, OutputClass _ocSetupToe, bool _undoChange, SetupChange_CornerVariables _sccv, List<Line> _shimsLine, List<Vector3D> _shimsVector, Line _axisOfRotation, int _upIndex)
        {
            ///<summary>Calculating the new Toe using the delta of Toe</summary>
            Angle dToe = new Angle(_dToe, AngleUnit.Degrees);

            bool reRotateForToe = false;

            SteeringAxisISRotationAxis = true;
            KPIRotation = false;
            CasterRotation = false;

            SetupChange_CLS_Master.ToeConvergence = Convergence.InProgress;

            /////<remarks>Adding a Label here so I can createa Loop without using a For Statement. Label placed in the 2nd line so that the <see cref="dToe"/> Angle gets the user requested OR reversal value only once. All other rotations should be by angle <see cref="ReCheckToe()"/></remarks>
            //Start:

            for (int i_Toe = 0; i_Toe < _sccv.iterationsToe; i_Toe++)
            {
                SetupChange_DB_Master.SetupChangeOPDictionary["Toe"] = dToe.Radians;

                ///<summary>
                ///The <see cref="AdjustmentOptions.MToeAdjusterLine"/> will never have more than one intity if the Setup Change is <see cref="AdjustmentType.Indirect"/>. So the below method 
                ///necessary so that List can be assinged new values 
                /// </summary>
                if (_sccv.toeAdjustmentType == AdjustmentType.Direct)
                {
                    SetupChange_DB_Master.AdjOptions.AddAdjusterToMasterAdjusterList(_shimsLine, _shimsVector);
                }

                ///<summary>Rotating the Wheel Assembyly Points and Vectors using the <see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> </summary>
                RotateWheelAssemblyComponents(_axisOfRotation, dToe, _ocSetupToe, CurrentChange.Toe);
                RotateUprightTriangle(_axisOfRotation, dToe, _sccv.toeAdjustmentType, CurrentChange.Toe);

                ///<summary>
                ///Checking if performing the rotation of the <see cref="SetupChangeDatabase.WheelSpindle"/>by the angle <see cref="dToe"/> about <see cref="SetupChangeDatabase.SteeringAxis"/> 
                ///ACTUALLY resulted in getting a Toe Angle of <see cref="dToe"/>
                ///IF not then <see cref="SetupChange_ToeChange(double, OutputClass, int, int)"/> is performed again 
                ///</summary>
                Angle checkToe = ReCheckToe();
                SetupChange_CLS_Master.Summ_dToe.Add(checkToe);

                AssignToeDelta(dToe, SetupChange_CLS_Master.Summ_dToe[SetupChange_CLS_Master.Summ_dToe.Count - 1]);

                ///<summary>Finding the change in the <see cref="AdjustmentOptions.MToeAdjusterLine"/> due to the Rotations perfomed for the Toe</summary>
                double checkLinkLengthWithAbs = SetupChange_DB_Master.AdjOptions.GetLinkLengthChangeFromUprightAngle(_shimsLine, _shimsVector, SetupChange_DB_Master, _upIndex);
                SetupChange_CLS_Master.Summ_dToeAdjusterLength.Add(checkLinkLengthWithAbs);
                SetupChange_CLS_Master.Final_ToeAdjusterLength.Add(checkLinkLengthWithAbs);

                ///<summary>Repetetive loop to redo the rotation within this pass only if it is found that the required rotation (or UNROTATION) and the Actual Rotation(or UNROTATION) are too far off</summary>
                ///<remarks>The Iterative Loop will proceed based on whether the user hs requested for a direct Angle change in Toe or a Link Length Change</remarks>
                if (_sccv.toeAdjustmentType == AdjustmentType.Direct)
                {
                    ///<summary>Checker Variable for the Internal Loop Iterator. <see cref="GetInternalIterationChecker(bool, Angle, OutputClass)"/> for information on this and how it is obtained </summary>
                    double checkerDirect = GetInternalIterationChecker_DirectChange(_undoChange, checkToe, _ocSetupToe.sccvOP.deltaToe);

                    ///<summary>Internal Iteration code</summary>
                    if (Math.Abs(checkerDirect) > 0.0009)
                    {
                        dToe = new Angle(-checkerDirect, AngleUnit.Degrees);
                        AssignAngleToBeRotatedForToe(dToe);
                        reRotateForToe = true;
                        //goto Start;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (_sccv.toeAdjustmentType == AdjustmentType.Indirect)
                {
                    ///<summary>Getting the internal iterator for Indirect Setup Change </summary>
                    double checkerIndirect = GetInternalIterationChecker_IndirectChange(checkLinkLengthWithAbs, _sccv.deltaToeLinkLength);

                    ///<summary>Internal iteration code </summary>
                    if (Math.Abs(checkerIndirect) > 0.05)
                    {
                        Angle angleToBeRotated = SetupChange_ToeLinkLengthChanged(checkerIndirect);
                        dToe = new Angle(angleToBeRotated.Degrees, AngleUnit.Degrees);
                        AssignAngleToBeRotatedForToe(dToe);
                        reRotateForToe = true;
                        //goto Start;
                    }
                    else
                    {
                        break;
                    }

                }
            }


            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight();

            ///<summary>Calculating the change in Camber</summary>
            RecordInadvertantCamberChange(_ocSetupToe);

            ///<summary>Calculating the change in Caster</summary>
            RecordInadvertantCasterChange(_ocSetupToe);

            ///<summary>Calcualting the change in KPI</summary>
            RecordInadvertantChangeKPI(_ocSetupToe);

        }

        /// <summary>
        /// Method to calcualte the Caster change requested by the user as a Setup Change. This method shall also re-calculate all the other things which change with the Caster
        /// </summary>
        /// <param name="_dCaster">Change in Caster requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupCaster"></param>
        /// <param name="_undoChange">This is a boolean which determines if the method is being called to ACTUALLY Make a Setup change or if it is being called to UNDO a change caused by a previous Setup Change</param>
        /// <param name="_sccv"></param>
        /// <param name="_linkLine">List of the <see cref="AdjustmentOptions.MCasterAdjusterLine"/> lines</param>
        /// <param name="_linkVector">List of the <see cref="AdjustmentOptions.MCasterAdjusterVector"/> vectors</param>
        /// <param name="_uprightVertex">Vertex of the Upright Being considered. UBJ[0], LBJ[1] and ToeLinkUpright[2]</param>
        internal void SetupChange_CasterChange(double _dCaster, OutputClass _ocSetupCaster, bool _undoChange, SetupChange_CornerVariables _sccv, List<Line> _linkLine, List<Vector3D> _linkVector, Line _axisOfRotation, int _uprightVertex)
        {
            ///<summary>Calculating the new Caster using the delta of Caster</summary>
            Angle dCaster = new Angle(_dCaster, AngleUnit.Degrees);

            ///<summary>Boolean internal iteration variable</summary>
            bool reRotateForCaster = false;

            SteeringAxisISRotationAxis = false;
            KPIRotation = false;
            CasterRotation = true;

            SetupChange_CLS_Master.CasterConvergence = Convergence.InProgress;

            ///<remarks>Label which will be used to create an iterative loop without a FOR or WHILE statement</remarks>
            //Start:

            for (int i_Caster = 0; i_Caster < _sccv.iterationsCaster; i_Caster++)
            {
                SetupChange_DB_Master.SetupChangeOPDictionary["Caster"] = dCaster.Radians;

                ///<summary>
                ///The <see cref="AdjustmentOptions.MCasterAdjusterLine"/> will never have more than one entity if the Setup Change is <see cref="AdjustmentType.Indirect"/>. So the below method 
                ///necessary so that List can be assinged new values 
                /// </summary`
                if (_sccv.casterAdjustmentType == AdjustmentType.Direct)
                {
                    SetupChange_DB_Master.AdjOptions.AddAdjusterToMasterAdjusterList(_linkLine, _linkVector);
                }

                ///<summary>
                ///Rotating the Steering Axis and the Wheel Assembyly Points and Vectors using the 
                ///<see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> and the 
                ///<see cref="RotateSteeringAxis(Vector3D, Angle)"/>
                ///</summary>
                RotateSteeringAxis(_axisOfRotation, dCaster, CurrentChange.Caster);
                RotateWheelAssemblyComponents(_axisOfRotation, dCaster, _ocSetupCaster, CurrentChange.Caster);
                RotateUprightTriangle(_axisOfRotation, dCaster, _sccv.casterAdjustmentType, CurrentChange.Caster);

                ///<summary>
                ///Checking if performing the rotation of the <see cref="SetupChangeDatabase.WheelSpindle"/>by the angle <see cref="dCaster"/> about <see cref="SetupChangeDatabase.SteeringAxis.PerpAlongZ"/> 
                ///ACTUALLY resulted in getting a Caster Angle of <see cref="dCaster"/>
                ///IF not then <see cref="SetupChange_CasterChange(double, OutputClass, int, int)"/> is performed again 
                ///</summary>
                Angle checkCaster = ReCheckCaster();
                SetupChange_CLS_Master.Summ_dCaster.Add(checkCaster);

                ///<summary>
                ///Assinging the Rotated Angle and the ACTUAL ANGLE OF Caster to the <see cref="SetupChange_ClosedLoopSolver.RotationsAbout_LBJToToeLinkAxis"/> or any other Axis List
                /// and the <see cref="SetupChange_ClosedLoopSolver.Final_Caster"/> List 
                /// </summary>
                AssignCasterDelta(dCaster, SetupChange_CLS_Master.Summ_dCaster[SetupChange_CLS_Master.Summ_dCaster.Count - 1]);

                ///<summary>Finding the change in the <see cref="AdjustmentOptions.MCasterAdjusterLine"/> due to the Rotations perfomed for the Caster</summary>
                double checkAdjusterLinkLength = SetupChange_DB_Master.AdjOptions.GetLinkLengthChangeFromUprightAngle(_linkLine, _linkVector, SetupChange_DB_Master, _uprightVertex);
                SetupChange_CLS_Master.Summ_dCasterAdjusterLength.Add(checkAdjusterLinkLength);
                SetupChange_CLS_Master.Final_CasterAdjusterLength.Add(SetupChange_CLS_Master.Summ_dCasterAdjusterLength[SetupChange_CLS_Master.Summ_dCasterAdjusterLength.Count - 1]);

                ///<summary>Repetetive loop to redo the rotation within this pass only if it is found that the required rotation (or UNROTATION) and the Actual Rotation(or UNROTATION) are too far off</summary>
                ///<remarks>The Iterative Loop will proceed based on whether the user hs requested for a direct Angle change or a Wishbone change </remarks>
                if ((_sccv.casterAdjustmentType == AdjustmentType.Direct))
                {
                    ///<summary>Checker Variable for the Internal Loop Iterator. <see cref="GetInternalIterationChecker(bool, Angle, OutputClass)"/> for information on this and how it is obtained </summary>
                    double checkerDirect = GetInternalIterationChecker_DirectChange(_undoChange, checkCaster, _ocSetupCaster.sccvOP.deltaCaster);

                    if (Math.Abs(checkerDirect) > 0.009)
                    {
                        dCaster = new Angle(-checkerDirect, AngleUnit.Degrees);
                        AssignAngleToBeRotatedForCaster(dCaster);
                        reRotateForCaster = true;
                        //goto Start;
                    }
                    else
                    {
                        break;
                    }
                }
                ///<remarks>Caster OR KPI are never changed Indirectly. If there is a LINK LENGTH CHANGE. Then it is treated seperately and the Caster and KPI are (if needed) reversed using the Direct Method</remarks>
                else if (_sccv.casterAdjustmentType == AdjustmentType.Indirect)
                {
                    ///<summary>Getting the internal iterator for Indirect Setup Change </summary>
                    double checkerIndirect = GetInternalIterationChecker_IndirectChange(checkAdjusterLinkLength, _sccv.deltaBottmFrontArm);

                    if (Math.Abs(checkerIndirect) > 0.05)
                    {
                        //Angle angleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(checkerIndirect);
                        //dCaster = new Angle(angleToBeRotated.Degrees, AngleUnit.Degrees);
                        //AssignAngleToBeRotatedForCaster(dCaster);
                        //reRotateForCaster = true;
                        //goto Start;
                    }
                }
            }

            reRotateForCaster = false;

            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight();

            ///<summary>Calculating the change in Camber</summary>
            RecordInadvertantCamberChange(_ocSetupCaster);

            ///<summary>Calculating the change in toe</summary>
            RecordInadvertantToeChange(_ocSetupCaster);

            ///<summary>Calcualting the change in KPI</summary>
            RecordInadvertantChangeKPI(_ocSetupCaster);

        }

        /// <summary>
        /// Method to calcualte the KPI change requested by the user as a Setup Change. This method shall also re-calculate all the other things which change with the KPI
        /// </summary>
        /// <param name="_dKPI">Change in KPI requested in <see cref="AngleUnit.Degrees"/></param>
        /// <param name="_ocSetupKPI">Object of the Output Class to store the new value of KPI</param>
        /// <param name="_undoChange">This is a boolean which determines if the method is being called to ACTUALLY Make a Setup change or if it is being called to UNDO a change caused by a previous Setup Change</param>
        /// <param name="_sccv"></param>
        /// <param name="_linkLengthLine">List of the <see cref="AdjustmentOptions.MKPIAdjusterLine"/> lines</param>
        /// <param name="_linkLengthVector">List of the <see cref="AdjustmentOptions.MKPIAdjusterVector"/> vectors</param>
        /// <param name="_uprightVertex">Index of the Upright Vertex (UBJ[0] or LBJ[0]) which is to be considered</param>
        internal void SetupChange_KPIChange(double _dKPI, OutputClass _ocSetupKPI, bool _undoChange, SetupChange_CornerVariables _sccv, List<Line> _linkLengthLine, List<Vector3D> _linkLengthVector, Line _axisRotation, int _uprightVertex)
        {
            ///<summary>Calculating the new KPI using the delta of KPI</summary>
            Angle dKPI = new Angle(_dKPI, AngleUnit.Degrees);

            ///<summary>Boolean internal iteration variable</summary>
            bool reRotateKPI = false;

            SteeringAxisISRotationAxis = false;
            KPIRotation = true;
            CasterRotation = false;

            SetupChange_CLS_Master.KPIConvergence = Convergence.InProgress;

            ///<remarks>Adding a Label here so I can create a Loop without using a For Statement. Label placed in the 2nd line so that the <see cref="dCamber"/> Angle gets the user requested OR reversal value only once. All other rotations should be by angle <see cref="ReCheckCamber()"/></remarks>
            //Start:

            for (int i_KPI = 0; i_KPI < _sccv.iterationsKPI; i_KPI++)
            {
                SetupChange_DB_Master.SetupChangeOPDictionary["KPI"] = dKPI.Radians;

                ///<summary>
                ///The <see cref="AdjustmentOptions.MKPIAdjusterLine"/> will never have more than one entity if the Setup Change is <see cref="AdjustmentType.Indirect"/>. So the below method 
                ///necessary so that List can be assinged new values 
                /// </summary>
                if (_sccv.kpiAdjustmentType == AdjustmentType.Direct)
                {
                    SetupChange_DB_Master.AdjOptions.AddAdjusterToMasterAdjusterList(_linkLengthLine, _linkLengthVector);
                }

                ///<summary>
                ///Rotating the Steering Axis and the Wheel Assembyly Points and Vectors using the 
                ///<see cref="RotateWheelAssemblyComponents(Vector3D, Angle, OutputClass)"/> and the 
                ///<see cref="RotateSteeringAxis(Vector3D, Angle)"/>
                ///</summary>
                RotateSteeringAxis(_axisRotation, dKPI, CurrentChange.KPI);
                RotateWheelAssemblyComponents(_axisRotation, dKPI, _ocSetupKPI, CurrentChange.KPI);
                RotateUprightTriangle(_axisRotation, dKPI, _sccv.kpiAdjustmentType, CurrentChange.KPI);

                ///<summary>
                ///Checking if performing the rotation of the <see cref="SetupChangeDatabase.WheelSpindle"/>by the angle <see cref="dKPI"/> about <see cref="SetupChangeDatabase.SteeringAxis.PerpAlongZ"/> 
                ///ACTUALLY resulted in getting a KPI Angle of <see cref="dKPI"/>
                ///IF not then <see cref="SetupChange_KPIChange(double, OutputClass, int, int)"/> is performed again 
                ///</summary>
                Angle checkKPI = ReCheckKPI();
                SetupChange_CLS_Master.Summ_dKPI.Add(checkKPI);

                AssignKPIDelta(dKPI, SetupChange_CLS_Master.Summ_dKPI[SetupChange_CLS_Master.Summ_dKPI.Count - 1]);

                ///<summary>Finding the change in the <see cref="AdjustmentOptions.MKPIAdjusterVector"/> due to the Rotations perfomed for the KPI</summary>
                double checkAdjusterLengthWithAbs = SetupChange_DB_Master.AdjOptions.GetLinkLengthChangeFromUprightAngle(_linkLengthLine, _linkLengthVector, SetupChange_DB_Master, _uprightVertex);
                SetupChange_CLS_Master.Summ_dKPIAdjusterLength.Add(checkAdjusterLengthWithAbs);
                SetupChange_CLS_Master.Final_KPIAdjusterLength.Add(SetupChange_CLS_Master.Summ_dKPIAdjusterLength[SetupChange_CLS_Master.Summ_dKPIAdjusterLength.Count - 1]);

                ///<summary>Repetetive loop to redo the rotation within this pass only if it is found that the required rotation (or UNROTATION) and the Actual Rotation(or UNROTATION) are too far off</summary>
                ///<remarks>The iterative loop will proceed based on whether the user has selected a Direct Angle change or change in Wishbone length</remarks>
                if ((_sccv.kpiAdjustmentType == AdjustmentType.Direct))
                {
                    ///<summary>Checker Variable for the Internal Loop Iterator. <see cref="GetInternalIterationChecker(bool, Angle, OutputClass)"/> for information on this and how it is obtained </summary>
                    double checkerDirect = GetInternalIterationChecker_DirectChange(_undoChange, checkKPI, _ocSetupKPI.sccvOP.deltaKPI);

                    if (Math.Abs(checkerDirect) > 0.009)
                    {
                        dKPI = new Angle(-checkerDirect, AngleUnit.Degrees);
                        AssignAngleToBeRotatedForKPI(dKPI);
                        reRotateKPI = true;
                        //goto Start;
                    }
                    else
                    {
                        ///<summary>Not Setting the <see cref="Convergence"/> enum here because UNLESS MY CODE IS INCORRECT THIS INTERNAL LOOP WILL ALWAYS CCONVERGE</summary>
                        //SetupChange_CLS_Master.KPIConvergence = Convergence.Successful;
                        break;
                    }
                }

                ///<remarks>Caster OR KPI are never changed Indirectly. If there is a LINK LENGTH CHANGE. Then it is treated seperately and the Caster and KPI are (if needed) reversed using the Direct Method</remarks>
                else if (_sccv.kpiAdjustmentType == AdjustmentType.Indirect)
                {
                    ///<summary>Getting the internal iterator for Indirect Setup Change </summary>
                    double checkerIndirect = GetInternalIterationChecker_IndirectChange(checkAdjusterLengthWithAbs, _sccv.deltaBottmFrontArm);

                    if (Math.Abs(checkerIndirect) > 0.05)
                    {
                        //Angle angleToBeRotated = SetupChange_CamberShims_OR_ShimsVectorLengthChanged(checkerIndirect);
                        //dKPI = new Angle(angleToBeRotated.Degrees, AngleUnit.Degrees);
                        //AssignAngleToBeRotatedForKPI(dKPI);
                        //reRotateKPI = true;
                        //goto Start;
                    }

                }
            }

            ///<remarks>This code should ideally be inside the <see cref="SetupChange_ClosedLoopSolver"/> checks </remarks>
            //if (SetupChange_CLS_Master.KPIConvergence != Convergence.Successful)
            //{
            //    SetupChange_CLS_Master.KPIConvergence = Convergence.UnSuccessful;
            //}

            reRotateKPI = false;

            ///<summary>Calculating the change in Ride Height</summary>
            GetNewRideHeight();

            ///<summary>Calculating the change in toe</summary>
            RecordInadvertantToeChange(_ocSetupKPI);

            ///<summary>Calculating the change in Camber</summary>
            RecordInadvertantCamberChange(_ocSetupKPI);

            ///<summary>Calculating the change in Caster</summary>
            RecordInadvertantCasterChange(_ocSetupKPI);

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
        private void RotateWheelAssemblyComponents(Line _rotationLine, Angle _rotationAngle, OutputClass _ocRotate, CurrentChange _currChange)
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

            ///<remarks>
            /// ---IMPORTANT---
            /// -> Assigning the Angle of Rotation's Sign based on the <see cref="Line.Direction"/> of the Axis of Rotation.
            /// -> I noticed and confirmed from SolidWorks that in CAD the sense of Rotation is different if the Direction Vector of the Axis of Rotation is pointing backwards. I.E., the start point of the axis of rotation is greater than the end point
            /// -> So this method is needed
            /// </remarks>
            _rotationAngle = AssignRotationAngleSign(_rotationLine.StartPoint, _rotationLine.EndPoint, _rotationAngle, _currChange);

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
            SetupChange_DB_Master.InitializeAuxillaries(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);


            AssignPoint3D(SetupChange_DB_Master.WheelSpindle.Line.DeltaLine[SetupChange_DB_Master.WheelSpindle.Line.DeltaLine.Count - 1].StartPoint, ref _ocRotate.scmOP.K1x, ref _ocRotate.scmOP.K1y, ref _ocRotate.scmOP.K1z);
        }

        /// <summary>
        /// Method to Rotate the Steering Axis. Used when the KPI, CASTER or CAMBER(If wishbone is method to change Camber) is Rotated
        /// </summary>
        /// <param name="_rotationLine"></param>
        /// <param name="_rotationAngle"></param>
        /// <param name="i"></param>
        private void RotateSteeringAxis(Line _rotationLine, Angle _rotationAngle, CurrentChange _currChange)
        {

            SetupChange_DB_Master.SteeringAxis.AddLineAndPointToDeltaLineAndDeltaPoint(SetupChange_DB_Master.SteeringAxis, SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            SetupChange_DB_Master.InitializeAuxillaries(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);

            ///<remarks>
            /// ---IMPORTANT---
            /// -> Assigning the Angle of Rotation's Sign based on the <see cref="Line.Direction"/> of the Axis of Rotation.
            /// -> I noticed and confirmed from SolidWorks that in CAD the sense of Rotation is different if the Direction Vector of the Axis of Rotation is pointing backwards. I.E., the start point of the axis of rotation is greater than the end point
            /// -> So this method is needed
            /// </remarks>
            _rotationAngle = AssignRotationAngleSign(_rotationLine.StartPoint, _rotationLine.EndPoint, _rotationAngle, _currChange);

            SetupChange_DB_Master.SteeringAxis.Line.DeltaLine[SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1].Rotate(_rotationAngle.Radians, _rotationLine.StartPoint, _rotationLine.EndPoint);
            SetupChange_DB_Master.SteeringAxis.UpdateComponent(SetupChange_DB_Master.SteeringAxis, SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            SetupChange_DB_Master.InitializeAuxillaries(SetupChange_DB_Master.SteeringAxis.Line.DeltaLine.Count - 1);
            //SetupChange_DB_Master.SteeringAxis.GetFinalStateOfVariableLines(SetupChange_DB_Master.SteeringAxis, 1);

        }

        /// <summary>
        /// Method to Rotate the <see cref="SetupChangeDatabase.UprightTriangle"/> about an axis defined by the user
        /// </summary>
        /// <param name="_rotationLine"></param>
        /// <param name="_rotationAngle"></param>
        private void RotateUprightTriangle(Line _rotationLine, Angle _rotationAngle, AdjustmentType _adjType, CurrentChange _currChange)
        {
            if (_adjType == AdjustmentType.Direct)
            {
                Point3D V1 = new Point3D(SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[0].X, SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[0].Y,
                                         SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[0].Z);

                Point3D V2 = new Point3D(SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[1].X, SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[1].Y,
                                         SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[1].Z);

                Point3D V3 = new Point3D(SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[2].X, SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[2].Y,
                                         SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Vertices[2].Z);

                SetupChange_DB_Master.UprightTriangle.Add(new Triangle(V1, V2, V3));
            }

            ///<remarks>
            /// ---IMPORTANT---
            /// -> Assigning the Angle of Rotation's Sign based on the <see cref="Line.Direction"/> of the Axis of Rotation.
            /// -> I noticed and confirmed from SolidWorks that in CAD the sense of Rotation is different if the Direction Vector of the Axis of Rotation is pointing backwards. I.E., the start point of the axis of rotation is greater than the end point
            /// -> So this method is needed
            /// </remarks>
            _rotationAngle = AssignRotationAngleSign(_rotationLine.StartPoint, _rotationLine.EndPoint, _rotationAngle, _currChange);

            SetupChange_DB_Master.UprightTriangle[SetupChange_DB_Master.UprightTriangle.Count - 1].Rotate(_rotationAngle.Radians, _rotationLine.StartPoint, _rotationLine.EndPoint);
        }

        /// <summary>
        /// <para>This variable is crucial in returning the right sign of angle of rotation </para>
        /// See <see cref="RotateSteeringAxis(Line, Angle)"/> or any other Rotate Method for Explanation
        /// </summary>
        bool SteeringAxisISRotationAxis = false;
        bool KPIRotation = false;
        bool CasterRotation = false;
        int Identifier;
        private Angle AssignRotationAngleSign(Point3D _axisStart, Point3D _axisEnd, Angle _angleRotation, CurrentChange _currentChange)
        {
            ///<summary>---IMPORTANT--- FOR EXPLANATION ON WHY THIS IS DONE SEE YOUR NOTES. "EPIPHANIES" SECTION IN ORANGE HIGHLIGHT INSIDE THE ANGLE REALISATION SECTION</summary>
            if (_currentChange != CurrentChange.Toe)
            {
                if (_currentChange == CurrentChange.KPI || _currentChange == CurrentChange.Camber)
                {
                    if (_axisStart.Z > _axisEnd.Z)
                    {
                        return -_angleRotation;
                    }
                    else
                    {
                        return _angleRotation;
                    }
                }
                else if (_currentChange == CurrentChange.Caster)
                {
                    if (Identifier == 1 || Identifier == 3)
                    {
                        return _angleRotation;
                    }
                    else
                    {
                        return -_angleRotation;
                    }
                }
                else
                {
                    return _angleRotation;
                }
            }
            else
            {
                return _angleRotation;
            }
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
            SetupChange_DB_Master.InitializeAuxillaries(1);
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
        /// This method is the parent method which calls the sub method involved in recording a Camber change due to change in any other parameter
        /// </summary>
        /// <param name="_oc"></param>
        private void RecordInadvertantCamberChange(OutputClass _oc)
        {
            ///<summary>Calculating the change in Camber</summary>
            ///<remarks>
            ///Unlike the <see cref="ReCheckCamber()"/> operations done on Line 1492 here it is not necessary to do such operations here. This is because on Line 1492 what we are doing it we are checking if the Rotation operation that we performed ACTUALLY resulted in us getting the 
            ///correct Camber angle (which is <see cref="dCamber"/>). If NO then the rotation is performed again until we get the right Camber. The line of code finds the value by which the Camber has changed so that we can later on Reverse that Camber change to arrive at the samee OROROR 
            ///arrive at the required Camber.
            ///</remarks>
            Angle checkCamber = ReCheckCamber();

            ///<summary>Recording the change of Camber in the <see cref="SetupChange_ClosedLoopSolver.Summ_dCamber"/> list of Camber Changes</summary>
            SetupChange_CLS_Master.Summ_dCamber.Add(new Angle(checkCamber.Degrees /*- _oc.sccvOP.deltaCamber*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in Camber to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForCamber"/> and <see cref="SetupChange_ClosedLoopSolver.Final_Camber"/> lists</summary
            AssignCamberDelta(new Angle(0, AngleUnit.Degrees), new Angle(checkCamber.Degrees /*- _oc.sccvOP.deltaCamber*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in Camber to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForCamber"/> </summary
            AssignAngleToBeRotatedForCamber(new Angle(-(checkCamber.Degrees - _oc.sccvOP.deltaCamber), AngleUnit.Degrees));

        }
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
        /// <summary>
        /// Method to add the next angle by which the wheel spindle should be rotated to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForCamber"/>
        /// </summary>
        /// <param name="_angleToBeRotatedForCamber">Next angle to be rotated bye</param>
        private void AssignAngleToBeRotatedForCamber(Angle _angleToBeRotatedForCamber)
        {
            SetupChange_CLS_Master.AngleToBeRotatedForCamber.Add(_angleToBeRotatedForCamber);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotatedAngle"></param>
        /// <param name="actualCamberChange"></param>
        private void AssignCamberDelta(Angle rotatedAngle, Angle actualCamberChange)
        {
            if (rotatedAngle.Degrees != 0)
            {
                SetupChange_CLS_Master.RotationsAbout_LBJToToeLinkAxis.Add(rotatedAngle);
            }

            if (actualCamberChange.Degrees != 0)
            {
                SetupChange_CLS_Master.Final_Camber.Add(SetupChange_CLS_Master.Final_Camber[0] + actualCamberChange);
            }
        }


        /// <summary>
        /// This method is the parent method which calls the sub method involved in recording a Toe change due to change in any other parameter
        /// </summary>
        /// <param name="_oc"></param>
        private void RecordInadvertantToeChange(OutputClass _oc)
        {
            ///<summary>Calculating the change in toe</summary>
            ///<remarks>
            ///Unlike the <see cref="ReCheckCamber()"/> operations done  on Line 1429 here it is not necessary to do such operations here. This is because on Line 1429 what we are doing it we are checking if the Rotation operation that we performed ACTUALLY resulted in us getting the 
            ///correct camber angle (which is <see cref="dCamber"/>). If NO then the rotation is performed again until we get the right cambrr. The line of code finds the value by which the TOE has changed so that we can later on Reverse that Toe change to arrive at the samee OROROR 
            ///arrive at the required Toe.
            ///</remarks>
            Angle checkToe = ReCheckToe();

            ///<summary>Recording the change of Toe in the <see cref="SetupChange_ClosedLoopSolver.Summ_dToe"/> list of Toe Changes</summary>
            SetupChange_CLS_Master.Summ_dToe.Add(new Angle(checkToe.Degrees /*- _oc.sccvOP.deltaToe*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in Toe to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForToe"/> and <see cref="SetupChange_ClosedLoopSolver.Final_Toe"/> lists</summary
            AssignToeDelta(new Angle(0, AngleUnit.Degrees), new Angle((checkToe.Degrees /*- _oc.sccvOP.deltaToe*/), AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in Toe to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForToe"/> </summary
            AssignAngleToBeRotatedForToe(new Angle(-(checkToe.Degrees - _oc.sccvOP.deltaToe), AngleUnit.Degrees));

            ///<remarks>Link length change of toe need not be recorded here because you change the angle Inadvertenly. Not the Link length</remarks>
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
        /// <summary>
        /// Method to add the next angle by which the wheel spindle should be rotated to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForToe"/>
        /// </summary>
        /// <param name="_angleToBeRotatedForToe">Next to be rotated</param>
        private void AssignAngleToBeRotatedForToe(Angle _angleToBeRotatedForToe)
        {
            SetupChange_CLS_Master.AngleToBeRotatedForToe.Add(_angleToBeRotatedForToe);
        }
        /// <summary>
        /// Method to assign the Latest Actual Delta of Toe to the <see cref="SetupChange_ClosedLoopSolver.Final_Toe"/>
        /// </summary>
        /// <param name="rotatedAngle">Angle through which the Wheel Assembly was rotated about the Steering Axis</param>
        /// <param name="actualToeChange">Actual Angle of due to the above rotation</param>
        private void AssignToeDelta(Angle rotatedAngle, Angle actualToeChange)
        {

            if (rotatedAngle.Degrees != 0)
            {
                SetupChange_CLS_Master.RotationsAbout_LBJToToeLinkAxis.Add(rotatedAngle);
            }

            if (actualToeChange.Degrees != 0)
            {
                SetupChange_CLS_Master.Final_Toe.Add(SetupChange_CLS_Master.Final_Toe[0] + actualToeChange);
            }

        }

        /// <summary>
        /// This method is the parent method which calls the sub method involved in recording a Caster change due to change in any other parameter
        /// </summary>
        /// <param name="_oc"></param>
        private void RecordInadvertantCasterChange(OutputClass _oc)
        {
            ///<summary>Calculating the change in Caster</summary>
            ///Unlike the <see cref="ReCheckCaster()"/> operations done  on Line 1429 here it is not necessary to do such operations here. This is because on Line 1429 what we are doing it we are checking if the Rotation operation that we performed ACTUALLY resulted in us getting the 
            ///correct Caster angle (which is <see cref="dCaster"/>). If NO then the rotation is performed again until we get the right cambrr. The line of code finds the value by which the Caster has changed so that we can later on Reverse that Caster change to arrive at the samee OROROR 
            ///arrive at the required Caster.
            ///</remarks>
            Angle checkCaster = ReCheckCaster();

            ///<summary>Recording the change of caster in the <see cref="SetupChange_ClosedLoopSolver.Summ_dCaster"/> list of Caster Changes</summary>
            SetupChange_CLS_Master.Summ_dCaster.Add(new Angle(checkCaster.Degrees /*- _oc.sccvOP.deltaCaster*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in caster to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForCaster"/> and <see cref="SetupChange_ClosedLoopSolver.Final_Caster"/> lists</summary>
            AssignCasterDelta(new Angle(0, AngleUnit.Degrees), new Angle(checkCaster.Degrees /*- _oc.sccvOP.deltaCaster*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in caster to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForCaster"/> </summary>
            AssignAngleToBeRotatedForCaster(new Angle(-(checkCaster.Degrees - _oc.sccvOP.deltaCaster), AngleUnit.Degrees));
        }
        /// <summary>
        /// Calculating the change in Caster
        /// </summary>
        private Angle ReCheckCaster()
        {
            Angle dCaster_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.SideView.DeltaLine[SetupChange_DB_Master.SteeringAxis.ViewLines.SideView.DeltaLine.Count - 1]),
                                                                        Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.SideView.DeltaLine[0]),
                                                                        Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Lateral));

            //SetupChange_DB_Master.SetupChangeOPDictionary["Caster"] = dCaster_New.Radians;
            //SetupChange_DB_Master.InitializeSteeringAxis(1);

            return dCaster_New;
        }
        /// <summary>
        /// Method to add the next angle through which the Steering Axis Should be rotated to get the required caster Change
        /// </summary>
        /// <param name="_angleToBeRotatedForCaster"></param>
        private void AssignAngleToBeRotatedForCaster(Angle _angleToBeRotatedForCaster)
        {
            SetupChange_CLS_Master.AngleToBeRotatedForCaster.Add(_angleToBeRotatedForCaster);
        }
        /// <summary>
        /// Method to assign the Latest Actual Delta of Caster to the <see cref="SetupChange_ClosedLoopSolver.Final_Caster"/>
        /// </summary>
        /// <param name="rotatedAngle">Angle through which the Wheel Assembly was rotated about UBJToToe or LBJToToe axis </param>
        /// <param name="actualCasterChange">Actual Change in caster due to the above rotation</param>
        private void AssignCasterDelta(Angle rotatedAngle, Angle actualCasterChange)
        {
            if (rotatedAngle.Degrees != 0)
            {
                SetupChange_CLS_Master.RotationsAbout_LBJToToeLinkAxis.Add(rotatedAngle);
            }

            if (actualCasterChange.Degrees != 0)
            {
                SetupChange_CLS_Master.Final_Caster.Add(SetupChange_CLS_Master.Final_Caster[0] + actualCasterChange);
            }
        }


        /// <summary>
        /// Method to assign the Latest Actual Delta of KPI to the <see cref="SetupChange_ClosedLoopSolver.Final_KPI"/>
        /// </summary>
        /// <param name="_oc"></param>
        private void RecordInadvertantChangeKPI(OutputClass _oc)
        {
            ///<summary>Calculating the change in KPI</summary>
            ///Unlike the <see cref="ReCheckKPI()"/> operations done  on Line 1429 here it is not necessary to do such operations here. This is because on Line 1429 what we are doing it we are checking if the Rotation operation that we performed ACTUALLY resulted in us getting the 
            ///correct KPI angle (which is <see cref="dKPI"/>). If NO then the rotation is performed again until we get the right cambrr. The line of code finds the value by which the KPI has changed so that we can later on Reverse that KPI change to arrive at the samee OROROR 
            ///arrive at the required KPI.
            ///</remarks>
            Angle checkKPI = ReCheckKPI();

            ///<summary>Recording the change of KPI in the <see cref="SetupChange_ClosedLoopSolver.Summ_dKPI"/> list of KPI Changes</summary>
            SetupChange_CLS_Master.Summ_dKPI.Add(new Angle(checkKPI.Degrees /*- _oc.sccvOP.deltaKPI*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in KPI to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForKPI"/> and <see cref="SetupChange_ClosedLoopSolver.Final_KPI"/> lists</summary>
            AssignKPIDelta(new Angle(0, AngleUnit.Degrees), new Angle(checkKPI.Degrees /*- _oc.sccvOP.deltaKPI*/, AngleUnit.Degrees));

            ///<summary>Assigning the rotated angle and change in KPI to the <see cref="SetupChange_ClosedLoopSolver.AngleToBeRotatedForKPI"/> </summary>
            AssignAngleToBeRotatedForKPI(new Angle(-(checkKPI.Degrees - _oc.sccvOP.deltaKPI), AngleUnit.Degrees));
        }
        /// <summary>
        /// Calcualting the change in KPI
        /// </summary>
        private Angle ReCheckKPI()
        {
            Angle dKPI_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.FrontView.DeltaLine[SetupChange_DB_Master.SteeringAxis.ViewLines.FrontView.DeltaLine.Count - 1]),
                                                                     Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.SteeringAxis.ViewLines.FrontView.DeltaLine[0]),
                                                                     Custom3DGeometry.GetMathNetVector3D(SetupChange_DB_Master.WheelCentreAxis.Longitudinal));

            //SetupChange_DB_Master.SetupChangeOPDictionary["KPI"] = dKPI_New.Radians;
            //SetupChange_DB_Master.InitializeSteeringAxis(1);

            return dKPI_New;

        }
        /// <summary>
        /// Method to add the next angle through which the Steering Axis Should be rotated to get the required caster Change
        /// </summary>
        /// <param name="_angleToBeRotatedForCaster"></param>
        private void AssignAngleToBeRotatedForKPI(Angle _angleToBeRotatedForCaster)
        {
            SetupChange_CLS_Master.AngleToBeRotatedForKPI.Add(_angleToBeRotatedForCaster);
        }
        /// <summary>
        /// Method to assign the Latest Actual Delta of KPI to the <see cref="SetupChange_ClosedLoopSolver.Final_KPI"/>
        /// </summary>
        /// <param name="rotatedAngle">Angle through which the Wheel Assembly was rotated about UBJToToe or LBJToToe axisv</param>
        /// <param name="actualKPIChange">Actual Change in KPI due to the above rotation</param>
        private void AssignKPIDelta(Angle rotatedAngle, Angle actualKPIChange)
        {
            if (rotatedAngle.Degrees != 0)
            {
                SetupChange_CLS_Master.RotationsAbout_LBJToToeLinkAxis.Add(rotatedAngle);
            }
            if (actualKPIChange.Degrees != 0)
            {
                SetupChange_CLS_Master.Final_KPI.Add(SetupChange_CLS_Master.Final_KPI[0] + actualKPIChange);
            }
        }





        /// <summary>
        /// Calculating the new contact patch coordinates. This will determine the change in Ride Height
        /// </summary>
        private double GetNewRideHeight()
        {
            ///<summary>Calculating the change in Ride Height</summary>
            ///<remarks>
            /// When the New and Old values of the Contact Patch Y coordinate are subtraced if the result is positive then it means that the Contact Patch has risen above. 
            /// This implies that the chassis has gone down (Imagine It!!)
            /// Hence below, a minus sign is added to tell the software that the Ride Height is infact reducing when the Contact Patch is going up
            /// </remarks>
            double dRideHeight_New = -(SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1].Y - SetupChange_DB_Master.ContactPatch.DeltaPoint[SetupChange_DB_Master.ContactPatch.DeltaPoint.Count - 1 - 1].Y);
            SetupChange_DB_Master.SetupChangeOPDictionary["RideHeight"] = dRideHeight_New;

            SetupChange_CLS_Master.Summ_RideHeight.Add(dRideHeight_New);

            return dRideHeight_New;
        }


        /// <summary>
        /// Method to Assign All the Final Values of the correct <see cref="SetupChange_ClosedLoopSolver"/> Object which have not been assigned during Operations.
        /// </summary>
        /// <param name="Identifier">Corner Identifier</param>
        /// <param name="_sccvOut">Object of the <see cref="SetupChange_CornerVariables"/>. Need this only for Adjustment Tools</param>
        private void AssignAllFinalValues(int Identifier, SetupChange_CornerVariables _sccvOut, double _finalRideHeight, double _finalPushrod)
        {

            ///<summary>Assigning the Final Wishbone Lengths bssed on which of them was chosen for KPI Adjustmenr. Need to do this exclusively as all adjustments will be done with help of <see cref="AdjustmentOptions.MKPIAdjusterLine"/> </summary>
            if (_sccvOut.CasterChangeRequested)
            {
                if (_sccvOut.kpiAdjustmentTool == AdjustmentTools.TopFrontArm)
                {
                    SetupChange_CLS_Master.Final_TopFrontArm.Add(SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine[SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine.Count - 1].Length());
                    //SetupChange_CLS_Master.Final_TopFrontArm = SetupChange_CLS_Master.Final_KPIAdjusterLength;
                }
                else if (_sccvOut.kpiAdjustmentTool == AdjustmentTools.TopRearArm)
                {
                    SetupChange_CLS_Master.Final_TopRearArm.Add(SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine[SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine.Count - 1].Length());
                }
                else if (_sccvOut.kpiAdjustmentTool == AdjustmentTools.BottomFrontArm)
                {
                    SetupChange_CLS_Master.Final_BottomFrontArm.Add(SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine[SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine.Count - 1].Length());
                }
                else if (_sccvOut.kpiAdjustmentTool == AdjustmentTools.BottomRearArm)
                {
                    SetupChange_CLS_Master.Final_BottomRearArm.Add(SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine[SetupChange_DB_Master.AdjOptions.MKPIAdjusterLine.Count - 1].Length());
                }
            }

            if (_sccvOut.KPIChangeRequested)
            {
                ///<summary>Assigning the Final Wishbone Lengths bssed on which of them was chosen for Caster Adjustmenr. Need to do this exclusively as all adjustments will be done with help of <see cref="AdjustmentOptions.MCasterAdjustmenterLine"/> </summary>
                if (_sccvOut.casterAdjustmentTool == AdjustmentTools.TopFrontArm)
                {
                    SetupChange_CLS_Master.Final_TopFrontArm.Add(SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine[SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine.Count - 1].Length());
                }
                else if (_sccvOut.casterAdjustmentTool == AdjustmentTools.TopRearArm)
                {
                    SetupChange_CLS_Master.Final_TopRearArm.Add(SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine[SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine.Count - 1].Length());
                }
                else if (_sccvOut.casterAdjustmentTool == AdjustmentTools.BottomFrontArm)
                {
                    SetupChange_CLS_Master.Final_BottomFrontArm.Add(SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine[SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine.Count - 1].Length());
                }
                else if (_sccvOut.casterAdjustmentTool == AdjustmentTools.BottomRearArm)
                {
                    SetupChange_CLS_Master.Final_BottomRearArm.Add(SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine[SetupChange_DB_Master.AdjOptions.MCasterAdjustmenterLine.Count - 1].Length());
                }
            }

            ///<summary>Ride Height Adjustment </summary>
            ///<remarks>This step below is removed because it is now already done inside the <see cref="SetupChange_InvokeChangeSolvers(SetupChange_CornerVariables, List{OutputClass}, int, Angle, Angle, Angle, Angle, double, double)"/> method</remarks>
            //SetupChange_CLS_Master.Final_Pushrod.Add(_finalPushrod);
            //SetupChange_CLS_Master.Final_RideHeight.Add(_finalRideHeight);
            if (_sccvOut.RideHeightChanged == false && SetupChange_CLS_Master.Summ_RideHeight.Count != 0)
            {
                SetupChange_CLS_Master.Final_RideHeight.Add(SetupChange_CLS_Master.Summ_RideHeight[SetupChange_CLS_Master.Summ_RideHeight.Count - 1]);
            }

            //SetupChange_CLS_Master.Final_ToeAdjusterLength.Add(SetupChange_DB_Master.AdjOptions.MToeAdjusterLine[SetupChange_DB_Master.AdjOptions.MToeAdjusterLine.Count - 1].Length());

            ///<summary>Assigning the KPI Angle Direction. Have to use a temp variable since <see cref="Angle"/> is a property and I cannot pass a property as a <see cref="ref"/></summary>
            double tempKPI = SetupChange_CLS_Master.Final_KPI[SetupChange_CLS_Master.Final_KPI.Count - 1].Degrees;
            AssignDirection_KPI(Identifier, ref tempKPI);
            SetupChange_CLS_Master.Final_KPI[SetupChange_CLS_Master.Final_KPI.Count - 1] = new Angle(tempKPI, AngleUnit.Degrees);

            double tempKPIStart = SetupChange_CLS_Master.Final_KPI[0].Degrees;
            AssignDirection_KPI(Identifier, ref tempKPI);
            SetupChange_CLS_Master.Final_KPI[0] = new Angle(tempKPIStart, AngleUnit.Degrees);

            ///<summary></summary>
            double tempCamber = SetupChange_CLS_Master.Final_Camber[SetupChange_CLS_Master.Final_Camber.Count - 1].Degrees;
            double tempToe = SetupChange_CLS_Master.Final_Toe[SetupChange_CLS_Master.Final_Toe.Count - 1].Degrees;
            AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, Identifier);
            SetupChange_CLS_Master.Final_Camber[SetupChange_CLS_Master.Final_Camber.Count - 1] = new Angle(tempCamber, AngleUnit.Degrees);
            SetupChange_CLS_Master.Final_Toe[SetupChange_CLS_Master.Final_Toe.Count - 1] = new Angle(tempToe, AngleUnit.Degrees);

            double tempCamberStart = SetupChange_CLS_Master.Final_Camber[0].Degrees;
            double tempToeStart = SetupChange_CLS_Master.Final_Toe[0].Degrees;
            AssignOrientation_CamberToe(ref tempCamberStart, ref tempToeStart, tempCamberStart, tempToeStart, Identifier);
            SetupChange_CLS_Master.Final_Camber[0] = new Angle(tempCamberStart, AngleUnit.Degrees);
            SetupChange_CLS_Master.Final_Toe[0] = new Angle(tempToeStart, AngleUnit.Degrees);


            ///<summary></summary>
            double tempCaster = -SetupChange_CLS_Master.Final_Caster[SetupChange_CLS_Master.Final_Caster.Count - 1].Degrees;
            SetupChange_CLS_Master.Final_Caster[SetupChange_CLS_Master.Final_Caster.Count - 1] = new Angle(tempCaster, AngleUnit.Degrees);

            double tempCasterStart = -SetupChange_CLS_Master.Final_Caster[0].Degrees;
            SetupChange_CLS_Master.Final_Caster[0] = new Angle(tempCasterStart, AngleUnit.Degrees);

            if (Identifier == 1)
            {

                SetupChange_CLS_FL = null;
                SetupChange_CLS_FL = SetupChange_CLS_Master;
            }
            if (Identifier == 2)
            {

                SetupChange_CLS_FR = null;
                SetupChange_CLS_FR = SetupChange_CLS_Master;
            }
            if (Identifier == 3)
            {
                SetupChange_CLS_RL = null;
                SetupChange_CLS_RL = SetupChange_CLS_Master;
            }
            if (Identifier == 4)
            {
                SetupChange_CLS_RR = null;
                SetupChange_CLS_RR = SetupChange_CLS_Master;
            }

        } 
        #endregion

        #endregion
    }
}
    

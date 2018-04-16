using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;


namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class OutputClass : ISerializable
    {
        // This class contains all the variables which hold the outputs of the calculations

        #region Final Suspension Coordinates Declaration
        public SuspensionCoordinatesMaster scmOP = new SuspensionCoordinatesMaster();
        #endregion

        #region Setup Change Variables
        public SetupChange_CornerVariables sccvOP = new SetupChange_CornerVariables();
        #endregion

        #region Output Class Data Table
        public DataTable OC_SC_DataTable = new DataTable();
        #endregion

        #region Output Class Identifier Variable
        public int Identifier;
        #endregion

        #region Motion Ratio Declaration
        public double InitialMR, FinalMR, Initial_ARB_MR, Final_ARB_MR;
        #endregion

        #region Final Ride Height Declaration
        public double FinalRideHeight, FinalRideHeight_1, FinalRideHeight_ForTrans;
        #endregion

        #region Rates Declaration
        public double WheelRate, WheelRate_WO_ARB, RideRate, RideRate_1;
        #endregion

        #region Deflections Declaration
        public double Corrected_SpringDeflection, DamperLength, Corrected_WheelDeflection;  // These variables will hold the DELTA values of Spring and Wheel Deflection when the Recalculate button is clicked
        public double Corrected_SpringDeflection_1, Corrected_WheelDeflection_1; // These variables will hold the TOTAL values of Spring and Wheel Deflection when the Recalculate button is clicked (NOT USED ANYMORE b's Recalculate doesn't Delta of Corner Weight but the actual New Corner Weight)
        public double AngleOfRotation;
        /// <summary>
        /// Summation of the Changes calculated during the Heave Pass and Steering Pass. Basically this is the delta1 + delta2.
        /// </summary>
        public double deltaNet_SpringDeflection;
        /// <summary>
        /// Summation of the Changes calculated during the Heave Pass and the Steering Pass. Basically this is delta1 + delta2
        /// </summary>
        public double deltaNet_WheelDeflection;
        public double DeltaSpringDef_Steering/*, DeltaSpringDef_Steering_FR, DeltaSpringDef_Steering_RL, DeltaSpringDef_Steering_RR*/;
        public double TireDeflection;
        /// <summary>
        /// Summation of the Changes calculated during the Heave Pass and Steering Pass. Basically this is the delta1 + delta2.
        /// </summary>
        public double deltaNet_TireDeflection;
        public double ChassisHeave;
        /// <summary>
        /// Summation of the Changes calculated during the Heave Pass and Steering Pass. Basically this is the delta1 + delta2. 
        /// </summary>
        public double deltaNet_ChassisHeave;
        #endregion

        #region Camber and Toe Declaration
        public WheelAlignment waOP = new WheelAlignment(); // These variables will hold the DELTA values of Camber and Toe when the Recalculate button is clicked (NOT USED ANYMORE b's Recalculate doesn't Delta of Corner Weight but the actual New Corner Weight)
        public double FinalCamber_1, FinalToe_1;// These variables will hold the TOTAL values of Camber and Toe when the Recalculate button is clicked
        #endregion

        #region KPI and Caster Angle Declaration
        public double KPI, Caster;
        #endregion

        #region Corner Weights Declaration
        public double CW;// This variable will hold the total value of Corner Weight when the Recalculate button is clicked
        public double CW_1; // This variable will hold the total value of Corner Weight when the Recalculate button is clicked (NOT USED ANYMORE b's Recalculate doesn't Delta of Corner Weight but the actual New Corner Weight)
        /// <summary>
        /// Summation of the Changes calculated during the Heave Pass and Steering Pass. Basically this is the delta1 + delta2.
        /// </summary>
        public double deltaNet_CornerWeight;
        #endregion

        #region Tire Loaded Radius Declaration
        public double TireLoadedRadius;
        #endregion

        #region Wishbone and Wishbone XYZ Forces Declaration
        public double LowerFront, LowerRear, UpperFront, UpperRear, PushRod, ToeLink, DamperForce, ARBDroopLink,
                      LowerFront_x, LowerFront_y, LowerFront_z,
                      LowerRear_x, LowerRear_y, LowerRear_z,
                      UpperFront_x, UpperFront_y, UpperFront_z,
                      UpperRear_x, UpperRear_y, UpperRear_z,
                      PushRod_x, PushRod_y, PushRod_z,
                      DamperForce_x, DamperForce_y, DamperForce_z,
                      ARBDroopLink_x, ARBDroopLink_y, ARBDroopLink_z,
                      ToeLink_x, ToeLink_y, ToeLink_z,
                      UBJ_x, UBJ_y, UBJ_z,
                      LBJ_x, LBJ_y, LBJ_z,
                      RackInboard1_x, RackInboard1_y, RackInboard1_z,
                      RackInboard2_x, RackInboard2_y, RackInboard2_z,
                      ARBInboard1_x, ARBInboard1_y, ARBInboard1_z,
                      ARBInboard2_x, ARBInboard2_y, ARBInboard2_z,
                      SColumnInboard1_x, SColumnInboard1_y, SColumnInboard1_z,
                      SColumnInboard2_x, SColumnInboard2_y, SColumnInboard2_z;

        /// <summary>
        /// <see cref="List{T}"/> of Wishbone Link Forces. This will be used for regular Simulations to find the heaviest loaded link and colour the <see cref="devDept.Eyeshot"/> with a Gradient
        /// </summary>
        public List<double> WishboneForceList = new List<double>();
        /// <summary>
        /// <see cref="List{T}"/> of Wishbone Decomposition Forces. This will be used for regular Simulations to find the heaviest loaded link and colour the <see cref="devDept.Eyeshot"/> with a Gradient
        /// </summary>
        public List<double> WishboneForceDecompList_X = new List<double>();
        public List<double> WishboneForceDecompList_Y = new List<double>();
        public List<double> WishboneForceDecompList_Z = new List<double>();
        /// <summary>
        /// <see cref="List{T}"/> of Bearing Cap Forces. This will be used for regular Simulations to find the heaviest loaded link and colour the <see cref="devDept.Eyeshot"/> with a Gradient
        /// </summary>
        public List<double> BearingCapForcesList_X = new List<double>();
        public List<double> BearingCapForcesList_Y = new List<double>();
        public List<double> BearingCapForcesList_Z = new List<double>();
        /// <summary>
        /// Variable representing the Most Compressive Force
        /// </summary>
        public double MaxForce, MinForce;

        public double MaxCompressiveWishboneForce, MinCompressiveWishboneForce;

        public double MaxTensileWishboneForce, MinTensileWishboneForce;


        #endregion

        #region Non Suspended Mass Centres of Gravity Declaration
        public double New_NonSuspendedMassCoGx, New_NonSuspendedMassCoGy, New_NonSuspendedMassCoGz;
        #endregion

        #region Steering related variables Declaration
        public double SteeringTorque, SteeringEffort, Angle_InputOutputShafts, Angle_InputIntermediateShaft, Angle_IntertermediateOutputShaft, Angle_Steering, Angle_Intermediate, Angle_Pinion;
        #endregion

        #region Method to Initialize the OuputClass Data Table
        public DataTable InitializeDataTable()
        {
            OC_SC_DataTable.TableName = "Suspension Coordinates Output";

            OC_SC_DataTable.Columns.Add("Suspension Point", typeof(String));
            OC_SC_DataTable.Columns[0].ReadOnly = true;


            OC_SC_DataTable.Columns.Add("X (mm)", typeof(double));
            OC_SC_DataTable.Columns[1].ReadOnly = true;


            OC_SC_DataTable.Columns.Add("Y (mm)", typeof(double));
            OC_SC_DataTable.Columns[2].ReadOnly = true;

            OC_SC_DataTable.Columns.Add("Z (mm)", typeof(double));
            OC_SC_DataTable.Columns[3].ReadOnly = true;

            return OC_SC_DataTable;
        }
        #endregion

        /// <summary>
        /// <para> Method to Populate the 3 Wishbone Force lists with the forces of ALL Corners </para>
        /// <para> Currently this method will generate Lists of Forces which will be used in colouring the Force Arrows in Eyeshot with a Gradient </para>
        /// </summary>
        /// <param name="_ocList">Object of the Output Class whos Force Lists are to be populated</param>
        public void PopulateForceLists(OutputClass _ocList)
        {
            ///<summary>Adding the Wishbone forces to the List of Wishbone Link Forces so that they can be sorted and Max and Min Values decided</summary>
            _ocList.WishboneForceList.AddRange(new double[] { _ocList.LowerFront, _ocList.LowerRear, _ocList.UpperFront, _ocList.UpperRear, _ocList.PushRod, _ocList.ToeLink, _ocList.DamperForce, _ocList.ARBDroopLink });
            _ocList.WishboneForceList.Sort();

            ///<summary>Adding the Wishbone Decomposition forces to the List of Wishbone Decomposition Forces so that they can be sorted and Max and Min Values decided</summary>
            _ocList.WishboneForceDecompList_X.AddRange(new double[] { LowerFront_x, LowerRear_x, UpperFront_x, UpperRear_x, PushRod_x, DamperForce_x, ARBDroopLink_x, ToeLink_x, UBJ_x, LBJ_x });
            _ocList.WishboneForceDecompList_X.Sort();
            _ocList.WishboneForceDecompList_Y.AddRange(new double[] { LowerFront_y, LowerRear_y, UpperFront_y, UpperRear_y, PushRod_y, DamperForce_y, ARBDroopLink_y, ToeLink_y, UBJ_y, LBJ_y });
            _ocList.WishboneForceDecompList_Y.Sort();
            _ocList.WishboneForceDecompList_Z.AddRange(new double[] { LowerFront_z, LowerRear_z, UpperFront_z, UpperRear_z, PushRod_z, DamperForce_z, ARBDroopLink_z, ToeLink_z, UBJ_z, LBJ_z });
            _ocList.WishboneForceDecompList_Z.Sort();

            ///<summary>Adding the Bearing Decomposition forces to the List of Bearing Decomposition Forces so that they can be sorted and Max and Min Values decided</summary>
            _ocList.BearingCapForcesList_X.AddRange(new double[] { RackInboard1_x, RackInboard2_x, ARBInboard1_x, ARBInboard2_x, SColumnInboard1_x, SColumnInboard2_x});
            _ocList.BearingCapForcesList_X.Sort();
            _ocList.BearingCapForcesList_Y.AddRange(new double[] { RackInboard1_y, RackInboard2_y, ARBInboard1_y, ARBInboard2_y, SColumnInboard1_y, SColumnInboard2_y});
            _ocList.BearingCapForcesList_Y.Sort();
            _ocList.BearingCapForcesList_Z.AddRange(new double[] { RackInboard1_z, RackInboard2_z, ARBInboard1_z, ARBInboard2_z, SColumnInboard1_z, SColumnInboard2_z});
            _ocList.BearingCapForcesList_Z.Sort();

            ///<summary>Finding the Max Force (Most Compressive) </summary>
            FindMaxForce(_ocList);

            ///<summary>Finding the Min Force (Most Tensile) </summary>
            FindMinForce(_ocList);

            ///<summary>Method to segregate the Wishbone forces into Most and Least Compressive Tensile Forces. See descritption of Method for more details</summary>
            SegregateWishboneLinkForces(_ocList);


        }

        /// <summary>
        /// Method to find the Maximum Force or Most Compressive  among ALL the Forces 
        /// </summary>
        /// <param name="_oc"></param>
        private void FindMaxForce(OutputClass _oc)
        {

            if (MaxForce < _oc.WishboneForceList[0])
            {
                MaxForce = _oc.WishboneForceList[0];
            }
            if (MaxForce < _oc.WishboneForceDecompList_X[0]) 
            {
                MaxForce = _oc.WishboneForceDecompList_X[0];
            }
            if (MaxForce < _oc.WishboneForceDecompList_Y[0]) 
            {
                MaxForce = _oc.WishboneForceDecompList_Y[0];
            }
            if (MaxForce < _oc.WishboneForceDecompList_Z[0]) 
            {
                MaxForce = _oc.WishboneForceDecompList_Z[0];
            }
            if (MaxForce < _oc.BearingCapForcesList_X[0]) 
            {
                MaxForce = _oc.BearingCapForcesList_X[0];
            }
            if (MaxForce < _oc.BearingCapForcesList_Y[0]) 
            {
                MaxForce = _oc.BearingCapForcesList_Y[0];
            }
            if (MaxForce < _oc.BearingCapForcesList_Z[0]) 
            {
                MaxForce = _oc.BearingCapForcesList_Z[0];
            }
        }

        /// <summary>
        /// Method to find the Least force or the Most Tensile Force
        /// </summary>
        /// <param name="_oc"></param>
        private void FindMinForce(OutputClass _oc)
        {
            if (MinForce > _oc.WishboneForceList[_oc.WishboneForceList.Count - 1])
            {
                MinForce = _oc.WishboneForceList[_oc.WishboneForceList.Count - 1];
            }
            if (MinForce > _oc.WishboneForceDecompList_X[_oc.WishboneForceDecompList_X.Count - 1])
            {
                MinForce = _oc.WishboneForceDecompList_X[_oc.WishboneForceDecompList_X.Count - 1];
            }
            if (MinForce > _oc.WishboneForceDecompList_Y[_oc.WishboneForceDecompList_Y.Count - 1])
            {
                MinForce = _oc.WishboneForceDecompList_Y[_oc.WishboneForceDecompList_Y.Count - 1];
            }
            if (MinForce > _oc.WishboneForceDecompList_Z[_oc.WishboneForceDecompList_Z.Count - 1])
            {
                MinForce = _oc.WishboneForceDecompList_Z[_oc.WishboneForceDecompList_Z.Count - 1];
            }
            if (MinForce > _oc.BearingCapForcesList_X[_oc.BearingCapForcesList_X.Count - 1])
            {
                MinForce = _oc.BearingCapForcesList_X[_oc.BearingCapForcesList_X.Count - 1];
            }
            if (MinForce > _oc.BearingCapForcesList_Y[_oc.BearingCapForcesList_Y.Count - 1])
            {
                MinForce = _oc.BearingCapForcesList_Y[_oc.BearingCapForcesList_Y.Count - 1];
            }
            if (MinForce > _oc.BearingCapForcesList_Z[_oc.BearingCapForcesList_Z.Count - 1])
            {
                MinForce = _oc.BearingCapForcesList_Z[_oc.BearingCapForcesList_Z.Count - 1];
            }
        }

        /// <summary>
        /// <para>Method to find the Most and Least Compressive and Tensile Forces respectively</para>
        /// <para>Such a method is needed because unlike the Decomposition forces, the Wishbone axial forces are coloured differently for tensile and compressive forces (blue and red respectively)</para>
        /// </summary>
        /// <param name="_oc"></param>
        private void SegregateWishboneLinkForces(OutputClass _oc)
        {

            for (int i = 0; i < _oc.WishboneForceList.Count; i++)
            {
                if (_oc.WishboneForceList[i] > 0 && MaxCompressiveWishboneForce < _oc.WishboneForceList[i])
                {
                    MaxCompressiveWishboneForce = _oc.WishboneForceList[0];
                }
                else if (_oc.WishboneForceList[i] > 0 && MinCompressiveWishboneForce > _oc.WishboneForceList[i])
                {
                    MinCompressiveWishboneForce = _oc.WishboneForceList[i];
                }
                else if (_oc.WishboneForceList[i] < 0 && MaxTensileWishboneForce > _oc.WishboneForceList[i])
                {
                    MaxTensileWishboneForce = _oc.WishboneForceList[i];
                }
                else if (_oc.WishboneForceList[i] < 0 && MinTensileWishboneForce < _oc.WishboneForceList[i])
                {
                    MinTensileWishboneForce = _oc.WishboneForceList[i];
                }
            }
        }

        public OutputClass()
        {
            #region Final Suspension Coordinates Initialization
            scmOP.A1x = 0; scmOP.A1y = 0; scmOP.A1z = 0; scmOP.B1x = 0; scmOP.B1y = 0; scmOP.B1z = 0; scmOP.C1x = 0; scmOP.C1y = 0; scmOP.C1z = 0; scmOP.D1x = 0; scmOP.D1y = 0; scmOP.D1z = 0; scmOP.E1x = 0; scmOP.E1y = 0; scmOP.E1z = 0; scmOP.F1x = 0; scmOP.F1y = 0; scmOP.F1z = 0; scmOP.G1x = 0; scmOP.G1y = 0; scmOP.G1z = 0;
            scmOP.H1x = 0; scmOP.H1y = 0; scmOP.H1z = 0; scmOP.I1x = 0; scmOP.I1y = 0; scmOP.I1z = 0; scmOP.JO1x = 0; scmOP.JO1y = 0; scmOP.JO1z = 0; scmOP.J1x = 0; scmOP.J1y = 0; scmOP.J1z = 0; scmOP.K1x = 0; scmOP.K1y = 0; scmOP.K1z = 0; scmOP.L1x = 0; scmOP.L1y = 0; scmOP.L1z = 0; scmOP.M1x = 0; scmOP.M1y = 0; scmOP.M1z = 0;
            scmOP.N1x = 0; scmOP.N1y = 0; scmOP.N1z = 0; scmOP.O1x = 0; scmOP.O1y = 0; scmOP.O1z = 0; scmOP.P1x = 0; scmOP.P1y = 0; scmOP.P1z = 0; scmOP.Q1x = 0; scmOP.Q1y = 0; scmOP.Q1z = 0; scmOP.R1x = 0; scmOP.R1y = 0; scmOP.R1z = 0; scmOP.W1x = 0; scmOP.W1y = 0; scmOP.W1z = 0;
            #endregion

            #region Setup Change Corner Variables Initialization
            sccvOP.deltaCamber = 0; sccvOP.deltaCaster = 0; sccvOP.deltaKPI = 0; sccvOP.deltaRideHeight = 0; sccvOP.deltaCamberShims = 0; sccvOP.deltaToe = 0;
            #endregion

            #region Initializing the Output Class Data Table using the DataTable Initializer
            OC_SC_DataTable = InitializeDataTable();
            #endregion

            #region Motion Ratio Initialization
            InitialMR = 0; FinalMR = 0; Initial_ARB_MR = 0; Final_ARB_MR = 0;
            #endregion

            #region Final Ride Height Initialization
            FinalRideHeight = 0; FinalRideHeight_ForTrans = 0;
            FinalRideHeight_1 = 0;
            #endregion

            #region Rates Initialization
            WheelRate = 0; RideRate = 0; RideRate_1 = 0;
            #endregion

            #region Deflections Initialization
            Corrected_SpringDeflection = 0; DamperLength = 0; Corrected_WheelDeflection = 0;
            Corrected_SpringDeflection_1 = 0; Corrected_WheelDeflection_1 = 0;
            TireDeflection = 0;ChassisHeave = 0;
            #endregion

            #region Camber and Toe Initialization
            waOP.StaticCamber = 0; waOP.StaticToe = 0;
            FinalCamber_1 = 0; FinalToe_1 = 0;
            #endregion

            #region Tire Loaded Radius Initialization
            TireLoadedRadius = 0;
            #endregion

            #region Corner Weights Initialization
            CW = 0; CW_1 = 0;
            #endregion

            #region Wishbone and Wishbone XYZ Forces Initialization
            LowerFront = 0; LowerRear = 0; UpperFront = 0; UpperRear = 0; PushRod = 0; ToeLink = 0; DamperForce = 0; ARBDroopLink = 0;
            LowerFront_x = 0; LowerFront_y = 0; LowerFront_z = 0;
            LowerRear_x = 0; LowerRear_y = 0; LowerRear_z = 0;
            UpperFront_x = 0; UpperFront_y = 0; UpperFront_z = 0;
            UpperRear_x = 0; UpperRear_y = 0; UpperRear_z = 0;
            PushRod_x = 0; PushRod_y = 0; PushRod_z = 0;
            DamperForce_x = 0; DamperForce_y = 0; DamperForce_z = 0;
            ARBDroopLink_x = 0; ARBDroopLink_y = 0; ARBDroopLink_z = 0;
            ToeLink_x = 0; ToeLink_y = 0; ToeLink_z = 0;
            UBJ_x = 0; UBJ_y = 0; UBJ_z = 0;
            LBJ_x = 0; LBJ_y = 0; LBJ_z = 0;
            RackInboard1_x = 0;RackInboard1_y = 0;RackInboard1_z = 0;
            RackInboard2_x = 0;RackInboard2_y = 0;RackInboard2_z = 0;
            ARBInboard1_x = 0;ARBInboard1_y = 0;ARBInboard1_z = 0;
            ARBInboard2_x = 0;ARBInboard2_y = 0;ARBInboard2_z = 0;
            SColumnInboard1_x = 0;SColumnInboard1_y = 0;SColumnInboard1_z = 0;
            SColumnInboard2_x = 0;SColumnInboard2_y = 0;SColumnInboard2_z = 0;    
            #endregion

            #region  Non Suspended Mass Centres of Gravity Initialization
            New_NonSuspendedMassCoGx = 0; New_NonSuspendedMassCoGy = 0; New_NonSuspendedMassCoGz = 0;
            #endregion

            #region Steering variables Intitialization
            SteeringTorque = SteeringEffort = Angle_InputOutputShafts = Angle_IntertermediateOutputShaft = Angle_InputIntermediateShaft = Angle_Steering = Angle_Pinion = Angle_Intermediate = 0;
            #endregion

        }

        public void Clear()
        {
            #region Final Suspension Coordinates Clearing
            scmOP.A1x = 0; scmOP.A1y = 0; scmOP.A1z = 0; scmOP.B1x = 0; scmOP.B1y = 0; scmOP.B1z = 0; scmOP.C1x = 0; scmOP.C1y = 0; scmOP.C1z = 0; scmOP.D1x = 0; scmOP.D1y = 0; scmOP.D1z = 0; scmOP.E1x = 0; scmOP.E1y = 0; scmOP.E1z = 0; scmOP.F1x = 0; scmOP.F1y = 0; scmOP.F1z = 0; scmOP.G1x = 0; scmOP.G1y = 0; scmOP.G1z = 0;
            scmOP.H1x = 0; scmOP.H1y = 0; scmOP.H1z = 0; scmOP.I1x = 0; scmOP.I1y = 0; scmOP.I1z = 0; scmOP.JO1x = 0; scmOP.JO1y = 0; scmOP.JO1z = 0; scmOP.J1x = 0; scmOP.J1y = 0; scmOP.J1z = 0; scmOP.K1x = 0; scmOP.K1y = 0; scmOP.K1z = 0; scmOP.L1x = 0; scmOP.L1y = 0; scmOP.L1z = 0; scmOP.M1x = 0; scmOP.M1y = 0; scmOP.M1z = 0;
            scmOP.N1x = 0; scmOP.N1y = 0; scmOP.N1z = 0; scmOP.O1x = 0; scmOP.O1y = 0; scmOP.O1z = 0; scmOP.P1x = 0; scmOP.P1y = 0; scmOP.P1z = 0; scmOP.Q1x = 0; scmOP.Q1y = 0; scmOP.Q1z = 0; scmOP.R1x = 0; scmOP.R1y = 0; scmOP.R1z = 0; scmOP.W1x = 0; scmOP.W1y = 0; scmOP.W1z = 0;
            #endregion

            #region Setup Change Corner Variables Clearing
            sccvOP.deltaCamber = 0; sccvOP.deltaCaster = 0; sccvOP.deltaKPI = 0; sccvOP.deltaRideHeight = 0; sccvOP.deltaCamberShims = 0; sccvOP.deltaToe = 0;
            #endregion

            #region Motion Ratio Clearing
            InitialMR = 0; FinalMR = 0; Initial_ARB_MR = 0; Final_ARB_MR = 0;
            #endregion

            #region Final Ride Height Clearing
            FinalRideHeight = 0; FinalRideHeight_ForTrans = 0;
            FinalRideHeight_1 = 0;
            #endregion

            #region Rates Clearing
            WheelRate = 0; RideRate = 0; 
            #endregion

            #region Deflections Clearing
            Corrected_SpringDeflection = 0; DamperLength = 0; Corrected_WheelDeflection = 0;
            Corrected_SpringDeflection_1 = 0; Corrected_WheelDeflection_1 = 0; TireDeflection = 0;ChassisHeave = 0;
            #endregion

            #region Camber and Toe Clearing
            waOP.StaticCamber = 0; waOP.StaticToe = 0;
            FinalCamber_1 = 0; FinalToe_1 = 0;
            #endregion

            #region Tire Loaded Radius Clearing
            TireLoadedRadius = 0;
            #endregion

            #region Corner Weights Clearing
            CW = 0; 
            #endregion

            #region Wishbone and Wishbone XYZ Forces Clearing
            LowerFront = 0; LowerRear = 0; UpperFront = 0; UpperRear = 0; PushRod = 0; ToeLink = 0; DamperForce = 0; ARBDroopLink = 0;
            LowerFront_x = 0; LowerFront_y = 0; LowerFront_z = 0;
            LowerRear_x = 0; LowerRear_y = 0; LowerRear_z = 0;
            UpperFront_x = 0; UpperFront_y = 0; UpperFront_z = 0;
            UpperRear_x = 0; UpperRear_y = 0; UpperRear_z = 0;
            PushRod_x = 0; PushRod_y = 0; PushRod_z = 0;
            DamperForce_x = 0; DamperForce_y = 0; DamperForce_z = 0;
            ARBDroopLink_x = 0; ARBDroopLink_y = 0; ARBDroopLink_z = 0;
            ToeLink_x = 0; ToeLink_y = 0; ToeLink_z = 0;
            UBJ_x = 0; UBJ_y = 0; UBJ_z = 0;
            LBJ_x = 0; LBJ_y = 0; LBJ_z = 0;
            RackInboard1_x = 0; RackInboard1_y = 0; RackInboard1_z = 0;
            RackInboard2_x = 0; RackInboard2_y = 0; RackInboard2_z = 0;
            ARBInboard1_x = 0; ARBInboard1_y = 0; ARBInboard1_z = 0;
            ARBInboard2_x = 0; ARBInboard2_y = 0; ARBInboard2_z = 0;
            SColumnInboard1_x = 0; SColumnInboard1_y = 0; SColumnInboard1_z = 0;
            SColumnInboard2_x = 0; SColumnInboard2_y = 0; SColumnInboard2_z = 0;
            #endregion

            #region Non Suspended Mass Centres of Clearing
            New_NonSuspendedMassCoGx = 0; New_NonSuspendedMassCoGy = 0; New_NonSuspendedMassCoGz = 0;
            #endregion

            #region Steering variables Clearing
            SteeringEffort = SteeringTorque = Angle_InputOutputShafts = Angle_InputIntermediateShaft = Angle_IntertermediateOutputShaft = Angle_Pinion = Angle_Steering = Angle_Intermediate = 0;
            #endregion

        }
        
        public DataTable PopulateDataTable(Vehicle _vehicle)
        {

            #region Front Double Wishbone
            if (_vehicle.DoubleWishboneFront == 1 && (Identifier == 1 || Identifier == 2))
            {
                OC_SC_DataTable.Rows.Add("Lower Front Chassis", scmOP. D1z, scmOP. D1x, scmOP. D1y);
                OC_SC_DataTable.Rows.Add("Lower Rear Chassis", scmOP. C1z, scmOP. C1x, scmOP. C1y);
                OC_SC_DataTable.Rows.Add("Upper Front Chassis", scmOP. A1z, scmOP. A1x, scmOP. A1y);
                OC_SC_DataTable.Rows.Add("Upper Rear Chassis", scmOP. B1z, scmOP. B1x, scmOP. B1y);
                OC_SC_DataTable.Rows.Add("Bell-Crank Pivot", scmOP. I1z, scmOP. I1x, scmOP. I1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Chassis", scmOP. Q1z, scmOP. Q1x, scmOP. Q1y);
                OC_SC_DataTable.Rows.Add("Steering Link Chassis", scmOP. N1z, scmOP. N1x, scmOP. N1y);
                OC_SC_DataTable.Rows.Add("Damper Shock Mount", scmOP. JO1z, scmOP. JO1x, scmOP. JO1y);
                OC_SC_DataTable.Rows.Add("Damper Bell Crank", scmOP. J1z, scmOP. J1x, scmOP. J1y);

                #region Push/Pullrod Bell Crank - H
                if (_vehicle.PushRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                else if (_vehicle.PushRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                else if (_vehicle.PullRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                else if (_vehicle.PullRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                #endregion

                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Bell Crank", scmOP. O1z, scmOP. O1x, scmOP. O1y);

                #region Push/Pull Rod Upright - G
                if (_vehicle.PushRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                else if (_vehicle.PushRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                else if (_vehicle.PullRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                else if (_vehicle.PullRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                #endregion

                OC_SC_DataTable.Rows.Add("Upper Ball Joint", scmOP. F1z, scmOP. F1x, scmOP. F1y);
                OC_SC_DataTable.Rows.Add("Lower Ball Joint", scmOP. E1z, scmOP. E1x, scmOP. E1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Link", scmOP. P1z, scmOP. P1x, scmOP. P1y);
                OC_SC_DataTable.Rows.Add("Wheel Centre", scmOP. K1z, scmOP. K1x, scmOP. K1y);
                OC_SC_DataTable.Rows.Add("Wheel Spindle End", scmOP. L1z, scmOP. L1x, scmOP. L1y);
                OC_SC_DataTable.Rows.Add("Steering Link Upright", scmOP. M1z, scmOP. M1x, scmOP. M1y);
                OC_SC_DataTable.Rows.Add("Contact Patch", scmOP. W1z, scmOP. W1x, scmOP. W1y);

                #region Torsion Bar Botton - R
                if (_vehicle.TARBIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Torsion Bar Bottn", scmOP. R1z, scmOP. R1x, scmOP. R1y);
                }
                else if (_vehicle.TARBIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Torsion Bar Bottn", scmOP. R1z, scmOP. R1x, scmOP. R1y);
                }
                #endregion
            } 
            #endregion

            #region Rear Double Wishbone
            else if (_vehicle.DoubleWishboneRear == 1 && (Identifier == 3 || Identifier == 4))
            {
                OC_SC_DataTable.Rows.Add("Lower Front Chassis", scmOP. D1z, scmOP. D1x, scmOP. D1y);
                OC_SC_DataTable.Rows.Add("Lower Rear Chassis", scmOP. C1z, scmOP. C1x, scmOP. C1y);
                OC_SC_DataTable.Rows.Add("Upper Front Chassis", scmOP. A1z, scmOP. A1x, scmOP. A1y);
                OC_SC_DataTable.Rows.Add("Upper Rear Chassis", scmOP. B1z, scmOP. B1x, scmOP. B1y);
                OC_SC_DataTable.Rows.Add("Bell-Crank Pivot", scmOP. I1z, scmOP. I1x, scmOP. I1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Chassis", scmOP. Q1z, scmOP. Q1x, scmOP. Q1y);
                OC_SC_DataTable.Rows.Add("Steering Link Chassis", scmOP. N1z, scmOP. N1x, scmOP. N1y);
                OC_SC_DataTable.Rows.Add("Damper Shock Mount", scmOP. JO1z, scmOP. JO1x, scmOP. JO1y);
                OC_SC_DataTable.Rows.Add("Damper Bell Crank", scmOP. J1z, scmOP. J1x, scmOP. J1y);

                #region Push/Pullrod Bell Crank - H
                if (_vehicle.PushRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                else if (_vehicle.PushRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                else if (_vehicle.PullRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                else if (_vehicle.PullRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Bell-Crank", scmOP. H1z, scmOP. H1x, scmOP. H1y);
                }
                #endregion

                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Bell Crank", scmOP. O1z, scmOP. O1x, scmOP. O1y);

                #region Push/Pull Rod Upright - G
                if (_vehicle.PushRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                else if (_vehicle.PushRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pushrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                else if (_vehicle.PullRodIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                else if (_vehicle.PullRodIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Pullrod Upright", scmOP. G1z, scmOP. G1x, scmOP. G1y);
                }
                #endregion

                OC_SC_DataTable.Rows.Add("Upper Ball Joint", scmOP. F1z, scmOP. F1x, scmOP. F1y);
                OC_SC_DataTable.Rows.Add("Lower Ball Joint", scmOP. E1z, scmOP. E1x, scmOP. E1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Link", scmOP. P1z, scmOP. P1x, scmOP. P1y);
                OC_SC_DataTable.Rows.Add("Wheel Centre", scmOP. K1z, scmOP. K1x, scmOP. K1y);
                OC_SC_DataTable.Rows.Add("Wheel Spindle End", scmOP. L1z, scmOP. L1x, scmOP. L1y);
                OC_SC_DataTable.Rows.Add("Steering Link Upright", scmOP. M1z, scmOP. M1x, scmOP. M1y);
                OC_SC_DataTable.Rows.Add("Contact Patch", scmOP. W1z, scmOP. W1x, scmOP. W1y);

                #region Torsion Bar Botton - R
                if (_vehicle.TARBIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    OC_SC_DataTable.Rows.Add("Torsion Bar Bottn", scmOP. R1z, scmOP. R1x, scmOP. R1y);
                }
                else if (_vehicle.TARBIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    OC_SC_DataTable.Rows.Add("Torsion Bar Bottn", scmOP. R1z, scmOP. R1x, scmOP. R1y);
                }
                #endregion
            }
            #endregion

            #region Front McPherson
            else if (_vehicle.McPhersonFront == 1 && (Identifier == 1 || Identifier == 2))
            {
                OC_SC_DataTable.Rows.Add("Lower Front Chassis", scmOP. D1z, scmOP. D1x, scmOP. D1y);
                OC_SC_DataTable.Rows.Add("Lower Rear Chassis", scmOP. C1z, scmOP. C1x, scmOP. C1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Chassis", scmOP. Q1z, scmOP. Q1x, scmOP. Q1y);
                OC_SC_DataTable.Rows.Add("Steering Link Chassis", scmOP. N1z, scmOP. N1x, scmOP. N1y);
                OC_SC_DataTable.Rows.Add("Damper Chassis Mount", scmOP. JO1z, scmOP. JO1x, scmOP. JO1y);
                OC_SC_DataTable.Rows.Add("Damper Upright", scmOP. J1z, scmOP. J1x, scmOP. J1y);
                OC_SC_DataTable.Rows.Add("Lower Ball Joint", scmOP. E1z, scmOP. E1x, scmOP. E1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Link", scmOP. P1z, scmOP. P1x, scmOP. P1y);
                OC_SC_DataTable.Rows.Add("Wheel Centre", scmOP. K1z, scmOP. K1x, scmOP. K1y);
                OC_SC_DataTable.Rows.Add("Wheel Spindle End", scmOP. L1z, scmOP. L1x, scmOP. L1y);
                OC_SC_DataTable.Rows.Add("Steering Link Upright", scmOP. M1z, scmOP. M1x, scmOP. M1y);
                OC_SC_DataTable.Rows.Add("Contact Patch", scmOP. W1z, scmOP. W1x, scmOP. W1y);
            }
            #endregion

            #region Rear McPherson
            else if (_vehicle.McPhersonRear == 1 && (Identifier == 3 || Identifier == 4))
            {
                OC_SC_DataTable.Rows.Add("Lower Front Chassis", scmOP. D1z, scmOP. D1x, scmOP. D1y);
                OC_SC_DataTable.Rows.Add("Lower Rear Chassis", scmOP.C1z, scmOP. C1x, scmOP. C1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Chassis", scmOP. Q1z, scmOP. Q1x, scmOP. Q1y);
                OC_SC_DataTable.Rows.Add("Steering Link Chassis", scmOP. N1z, scmOP. N1x, scmOP. N1y);
                OC_SC_DataTable.Rows.Add("Damper Chassis Mount", scmOP. JO1z, scmOP. JO1x, scmOP. JO1y);
                OC_SC_DataTable.Rows.Add("Damper Upright", scmOP. J1z, scmOP. J1x, scmOP. J1y);
                OC_SC_DataTable.Rows.Add("Lower Ball Joint", scmOP. E1z, scmOP. E1x, scmOP. E1y);
                OC_SC_DataTable.Rows.Add("Anti-Roll Bar Link", scmOP. P1z, scmOP. P1x, scmOP. P1y);
                OC_SC_DataTable.Rows.Add("Wheel Centre", scmOP. K1z, scmOP. K1x, scmOP. K1y);
                OC_SC_DataTable.Rows.Add("Wheel Spindle End", scmOP. L1z, scmOP. L1x, scmOP. L1y);
                OC_SC_DataTable.Rows.Add("Steering Link Upright", scmOP. M1z, scmOP. M1x, scmOP. M1y);
                OC_SC_DataTable.Rows.Add("Contact Patch", scmOP. W1z, scmOP. W1x, scmOP. W1y);
            } 
            #endregion

            return OC_SC_DataTable;
        }

        /// <summary>
        /// Method exclusive for the Setup Mode AND when the user wants trigger a Setup Change. 
        /// This method is to be used to assign the previous Suspension Coordinates (<see cref="OutputClass.scmOP"/>) to the new Suspension Coordinates
        /// </summary>
        /// <param name="ocNew"></param>
        /// <param name="i"></param>
        public void AssignNewSuspensionCoordinates(List<OutputClass> ocNew, int i)
        {

        }


        #region De-Serialization of the Output Class Objects
        public OutputClass(SerializationInfo info, StreamingContext context)
        {
            #region De - Serialization of the Outuput Coordinates
            scmOP = (SuspensionCoordinatesMaster)info.GetValue("OutputSuspensionCoordinates", typeof(SuspensionCoordinatesMaster));
            #endregion

            #region De-Serialization of the Setup Change Corner Variables
            sccvOP = (SetupChange_CornerVariables)info.GetValue("sccvOP", typeof(SetupChange_CornerVariables));
            #endregion

            #region De-serialization of the Output Class Data Table
            OC_SC_DataTable = (DataTable)info.GetValue("OCDataTable", typeof(DataTable));
            #endregion

            #region De-serialization of the Output Class Identifier
            Identifier = (int)info.GetValue("Identifier", typeof(int));
            #endregion

            #region De - Serialization of the Final Moiton Ratio
            InitialMR = (double)info.GetValue("InitialMotionRatio", typeof(double));
            FinalMR = (double)info.GetValue("FinalMotionRatio", typeof(double));
            Initial_ARB_MR = (double)info.GetValue("InitialARBMotionRatio", typeof(double));
            Final_ARB_MR = (double)info.GetValue("FinalARBMotionRatio", typeof(double));
            #endregion

            #region De - Serialization of Ride Height 
            FinalRideHeight = (double)info.GetValue("Final_RideHeight", typeof(double));
            FinalRideHeight_ForTrans = (double)info.GetValue("Final_RideHeight_For_Translation", typeof(double));
            #endregion

            #region De - Serialization of Rates
            WheelRate = (double)info.GetValue("WheelRate", typeof(double));
            WheelRate_WO_ARB = (double)info.GetValue("WheelRate_WO_ARB", typeof(double));
            RideRate = (double)info.GetValue("RideRate", typeof(double));
            RideRate_1 = (double)info.GetValue("RideRate_1", typeof(double));
            #endregion

            #region De-Serialization of Deflections
            Corrected_SpringDeflection = (double)info.GetValue("Corrected_SpringDeflection", typeof(double));
            Corrected_SpringDeflection_1 = (double)info.GetValue("Corrected_SpringDeflection_1", typeof(double));
            Corrected_WheelDeflection = (double)info.GetValue("Corrected_WheelDeflection", typeof(double));
            Corrected_WheelDeflection_1 = (double)info.GetValue("Corrected_WheelDeflection_1", typeof(double));
            DamperLength = (double)info.GetValue("Damper_Length", typeof(double));
            TireDeflection = (double)info.GetValue("TireDeflection", typeof(double));
            ChassisHeave = (double)info.GetValue("ChassisHeave", typeof(double));
            #endregion

            #region De-Serialization of Alignment
            waOP.StaticCamber = (double)info.GetValue("Final_Camber", typeof(double));
            FinalCamber_1 = (double)info.GetValue("Final_Camber_1", typeof(double));
            waOP.StaticToe = (double)info.GetValue("Final_Toe", typeof(double));
            FinalToe_1 = (double)info.GetValue("Final_Toe_1", typeof(double));
            #endregion

            #region De-Serialization of the Tire Radius
            TireLoadedRadius = (double)info.GetValue("TireLoadedRadius", typeof(double));
            #endregion

            #region De-Serialization of the Corner Weights
            CW = (double)info.GetValue("CornerWeight", typeof(double));
            CW_1 = (double)info.GetValue("CornerWeight_1", typeof(double));
            #endregion

            #region De-Serialization of the Wishbone Forces
            LowerFront = (double)info.GetValue("LowerFront", typeof(double));
            LowerFront_x = (double)info.GetValue("LowerFront_x", typeof(double));
            LowerFront_y = (double)info.GetValue("LowerFront_y", typeof(double));
            LowerFront_z = (double)info.GetValue("LowerFront_z", typeof(double));

            LowerRear = (double)info.GetValue("LowerRear", typeof(double));
            LowerRear_x = (double)info.GetValue("LowerRear_x", typeof(double));
            LowerRear_y = (double)info.GetValue("LowerRear_y", typeof(double));
            LowerRear_z = (double)info.GetValue("LowerRear_z", typeof(double));

            UpperFront = (double)info.GetValue("UpperFront", typeof(double));
            UpperFront_x = (double)info.GetValue("UpperFront_x", typeof(double));
            UpperFront_y = (double)info.GetValue("UpperFront_y", typeof(double));
            UpperFront_z = (double)info.GetValue("UpperFront_z", typeof(double));

            UpperRear = (double)info.GetValue("UpperRear", typeof(double));
            UpperRear_x = (double)info.GetValue("UpperRear_x", typeof(double));
            UpperRear_y = (double)info.GetValue("UpperRear_y", typeof(double));
            UpperRear_z = (double)info.GetValue("UpperRear_z", typeof(double));

            PushRod = (double)info.GetValue("Pushrod", typeof(double));
            PushRod_x = (double)info.GetValue("Pushrod_x", typeof(double));
            PushRod_y = (double)info.GetValue("Pushrod_y", typeof(double));
            PushRod_z = (double)info.GetValue("Pushrod_z", typeof(double));

            ToeLink = (double)info.GetValue("ToeLink", typeof(double));
            ToeLink_x = (double)info.GetValue("ToeLink_x", typeof(double));
            ToeLink_y = (double)info.GetValue("ToeLink_y", typeof(double));
            ToeLink_z = (double)info.GetValue("ToeLink_z", typeof(double));

            DamperForce = (double)info.GetValue("DamperForce", typeof(double));
            DamperForce_x = (double)info.GetValue("DamperForce_x", typeof(double));
            DamperForce_y = (double)info.GetValue("DamperForce_y", typeof(double));
            DamperForce_z = (double)info.GetValue("DamperForce_z", typeof(double));


            ARBDroopLink = (double)info.GetValue("ARBDroopLink", typeof(double));
            ARBDroopLink_x = (double)info.GetValue("ARBDroopLink_x", typeof(double));
            ARBDroopLink_y = (double)info.GetValue("ARBDroopLink_y", typeof(double));
            ARBDroopLink_z = (double)info.GetValue("ARBDroopLink_z", typeof(double));

            UBJ_x = (double)info.GetValue("UBJ_x", typeof(double));
            UBJ_y = (double)info.GetValue("UBJ_y", typeof(double));
            UBJ_z = (double)info.GetValue("UBJ_z", typeof(double));

            LBJ_x = (double)info.GetValue("LBJ_x", typeof(double));
            LBJ_y = (double)info.GetValue("LBJ_y", typeof(double));
            LBJ_z = (double)info.GetValue("LBJ_z", typeof(double));

            RackInboard1_x = (double)info.GetValue("RackInboard1_x", typeof(double));
            RackInboard1_y = (double)info.GetValue("RackInboard1_y", typeof(double));
            RackInboard1_z = (double)info.GetValue("RackInboard1_z", typeof(double));
            RackInboard2_x = (double)info.GetValue("RackInboard2_x", typeof(double));
            RackInboard2_y = (double)info.GetValue("RackInboard2_y", typeof(double));
            RackInboard2_z = (double)info.GetValue("RackInboard2_z", typeof(double));

            ARBInboard1_x = (double)info.GetValue("ARBInboard1_x", typeof(double));
            ARBInboard1_y = (double)info.GetValue("ARBInboard1_y", typeof(double));
            ARBInboard1_z = (double)info.GetValue("ARBInboard1_z", typeof(double));
            ARBInboard2_x = (double)info.GetValue("ARBInboard2_x", typeof(double));
            ARBInboard2_y = (double)info.GetValue("ARBInboard2_y", typeof(double));
            ARBInboard2_z = (double)info.GetValue("ARBInboard2_z", typeof(double));

            SColumnInboard1_x = (double)info.GetValue("SColumnInboard1_x", typeof(double));
            SColumnInboard1_y = (double)info.GetValue("SColumnInboard1_y", typeof(double));
            SColumnInboard1_z = (double)info.GetValue("SColumnInboard1_z", typeof(double));
            SColumnInboard2_x = (double)info.GetValue("SColumnInboard2_x", typeof(double));
            SColumnInboard2_y = (double)info.GetValue("SColumnInboard2_y", typeof(double));
            SColumnInboard2_z = (double)info.GetValue("SColumnInboard2_z", typeof(double));

            #endregion

            #region Serialization of the New Non Suspended Mass CoG
            New_NonSuspendedMassCoGx = (double)info.GetValue("New_NSMCoG_x", typeof(double));
            New_NonSuspendedMassCoGy = (double)info.GetValue("New_NSMCoG_y", typeof(double));
            New_NonSuspendedMassCoGz = (double)info.GetValue("New_NSMCoG_z", typeof(double));
            #endregion

            #region De-serialization of the Steering Variables
            SteeringTorque = (double)info.GetValue("SteeringTorque", typeof(double));
            SteeringEffort = (double)info.GetValue("SteeringEffort", typeof(double));
            Angle_InputOutputShafts = (double)info.GetValue("Angle_InputOutputShafts", typeof(double));
            Angle_InputIntermediateShaft = (double)info.GetValue("Angle_InputIntermediateShaft", typeof(double));
            Angle_IntertermediateOutputShaft = (double)info.GetValue("Angle_IntertermediateOutputShaft", typeof(double));
            Angle_Steering = (double)info.GetValue("Angle_Steering", typeof(double));
            Angle_Pinion = (double)info.GetValue("Angle_Pinion", typeof(double));
            Angle_Intermediate = (double)info.GetValue("Angle_Intermediate", typeof(double));
            #endregion

        }
        #endregion


        #region Serialization of the Output Class Objects
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            #region Serialization of the Outuput Coordinates
            info.AddValue("OutputSuspensionCoordinates", scmOP);
            #endregion

            #region Serialization of the Output SetupChange Corner Variables
            info.AddValue("sccvOP", sccvOP);
            #endregion

            #region Serialization of the Output Class Data Table
            info.AddValue("OCDataTable", OC_SC_DataTable);
            #endregion

            #region Serialization of the Output Class Identifier
            info.AddValue("Identifier", Identifier);
            #endregion

            #region Serialization of the Final Moiton Ratio
            info.AddValue("InitialMotionRatio", InitialMR);
            info.AddValue("FinalMotionRatio", FinalMR);
            info.AddValue("InitialARBMotionRatio", Initial_ARB_MR);
            info.AddValue("FinalARBMotionRatio", Final_ARB_MR);
            #endregion

            #region Serialization of Ride Height 
            info.AddValue("Final_RideHeight", FinalRideHeight);
            info.AddValue("Final_RideHeight_For_Translation", FinalRideHeight_ForTrans);
            #endregion

            #region Serialization of Rates
            info.AddValue("WheelRate", WheelRate);
            info.AddValue("WheelRate_WO_ARB", WheelRate_WO_ARB);
            info.AddValue("RideRate", RideRate);
            info.AddValue("RideRate_1", RideRate_1);
            #endregion

            #region Serialization of Deflections
            info.AddValue("Corrected_SpringDeflection", Corrected_SpringDeflection);
            info.AddValue("Corrected_SpringDeflection_1", Corrected_SpringDeflection_1);
            info.AddValue("Corrected_WheelDeflection", Corrected_WheelDeflection);
            info.AddValue("Corrected_WheelDeflection_1", Corrected_WheelDeflection_1);
            info.AddValue("Damper_Length", DamperLength);
            info.AddValue("TireDeflection", TireDeflection);
            info.AddValue("ChassisHeave", ChassisHeave);
            #endregion

            #region Serialization of Alignment
            info.AddValue("Final_Camber", waOP.StaticCamber);
            info.AddValue("Final_Camber_1", FinalCamber_1);
            info.AddValue("Final_Toe", waOP.StaticToe);
            info.AddValue("Final_Toe_1", FinalToe_1);
            #endregion

            #region Serialization of the Tire Radius
            info.AddValue("TireLoadedRadius", TireLoadedRadius);
            #endregion

            #region Serialization of the Corner Weights
            info.AddValue("CornerWeight", CW);
            info.AddValue("CornerWeight_1", CW_1);
            #endregion

            #region Serialization of the Wishbone Forces
            info.AddValue("LowerFront", LowerFront);
            info.AddValue("LowerFront_x", LowerFront_x);
            info.AddValue("LowerFront_y", LowerFront_y);
            info.AddValue("LowerFront_z", LowerFront_z);

            info.AddValue("LowerRear", LowerRear);
            info.AddValue("LowerRear_x", LowerRear_x);
            info.AddValue("LowerRear_y", LowerRear_y);
            info.AddValue("LowerRear_z", LowerRear_z);

            info.AddValue("UpperFront", UpperFront);
            info.AddValue("UpperFront_x", UpperFront_x);
            info.AddValue("UpperFront_y", UpperFront_y);
            info.AddValue("UpperFront_z", UpperFront_z);

            info.AddValue("UpperRear", UpperFront);
            info.AddValue("UpperRear_x", UpperFront_x);
            info.AddValue("UpperRear_y", UpperFront_y);
            info.AddValue("UpperRear_z", UpperFront_z);

            info.AddValue("Pushrod", PushRod);
            info.AddValue("Pushrod_x", PushRod_x);
            info.AddValue("Pushrod_y", PushRod_y);
            info.AddValue("Pushrod_z", PushRod_z);

            info.AddValue("ToeLink", ToeLink);
            info.AddValue("ToeLink_x", ToeLink_x);
            info.AddValue("ToeLink_y", ToeLink_y);
            info.AddValue("ToeLink_z", ToeLink_z);

            info.AddValue("DamperForce", DamperForce);
            info.AddValue("DamperForce_x", DamperForce_x);
            info.AddValue("DamperForce_y", DamperForce_y);
            info.AddValue("DamperForce_z", DamperForce_z);

            info.AddValue("ARBDroopLink", ARBDroopLink);
            info.AddValue("ARBDroopLink_x", ARBDroopLink_x);
            info.AddValue("ARBDroopLink_y", ARBDroopLink_y);
            info.AddValue("ARBDroopLink_z", ARBDroopLink_z);

            info.AddValue("UBJ_x", UBJ_x);
            info.AddValue("UBJ_y", UBJ_y);
            info.AddValue("UBJ_z", UBJ_z);

            info.AddValue("LBJ_x", LBJ_x);
            info.AddValue("LBJ_y", LBJ_y);
            info.AddValue("LBJ_z", LBJ_z);

            info.AddValue("RackInboard1_x", RackInboard1_x);
            info.AddValue("RackInboard1_y" , RackInboard1_y);
            info.AddValue("RackInboard1_z", RackInboard1_z);
            info.AddValue("RackInboard2_x", RackInboard2_x);
            info.AddValue("RackInboard2_y", RackInboard2_y);
            info.AddValue("RackInboard2_z", RackInboard2_z);

            info.AddValue("ARBInboard1_x", ARBInboard1_x);
            info.AddValue("ARBInboard1_y", ARBInboard1_y);
            info.AddValue("ARBInboard1_z", ARBInboard1_z);
            info.AddValue("ARBInboard2_x", ARBInboard2_x);
            info.AddValue("ARBInboard2_y", ARBInboard2_y);
            info.AddValue("ARBInboard2_z", ARBInboard2_z);

            info.AddValue("SColumnInboard1_x", SColumnInboard1_x);
            info.AddValue("SColumnInboard1_y", SColumnInboard1_y);
            info.AddValue("SColumnInboard1_z", SColumnInboard1_z);
            info.AddValue("SColumnInboard2_x", SColumnInboard2_x);
            info.AddValue("SColumnInboard2_y", SColumnInboard2_y);
            info.AddValue("SColumnInboard2_z", SColumnInboard2_z);



            #endregion

            #region Serialization of the New Non Suspended Mass CoG 
            info.AddValue("New_NSMCoG_x", New_NonSuspendedMassCoGx);
            info.AddValue("New_NSMCoG_y", New_NonSuspendedMassCoGy);
            info.AddValue("New_NSMCoG_z", New_NonSuspendedMassCoGz);
            #endregion

            #region Serialization of the Steering Variables
            info.AddValue("SteeringTorque", SteeringTorque);
            info.AddValue("SteeringEffort", SteeringEffort);
            info.AddValue("Angle_InputOutputShafts", Angle_InputOutputShafts);
            info.AddValue("Angle_InputIntermediateShaft", Angle_InputIntermediateShaft);
            info.AddValue("Angle_IntertermediateOutputShaft", Angle_IntertermediateOutputShaft);
            info.AddValue("Angle_Steering", Angle_Steering);
            info.AddValue("Angle_Pinion", Angle_Pinion);
            info.AddValue("Angle_Intermediate", Angle_Intermediate);
            #endregion

        } 
        #endregion
    }
}

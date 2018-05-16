using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This is the vehicle class. It contains methods which call the Solver method to calculate the motion of the Suspension Coordinates. 
    /// It also houses some auxillar method to calculate ARB Rates and also some methods to perform Reset operations
    /// </summary>

    [Serializable()]
    public class Vehicle : ISerializable, ICommand
    {

        #region Declarations
        #region Vehicle Identifiers
        public string _VehicleName { get; set; }
        public static int VehicleCounter = 0;
        public static int VehicleResultsCounter = 1;
        public int Vehicle_Results_Tracker = 0;
        public int VehicleID = -1;
        public bool VehicleIsModified { get; set; }
        public bool Vehicle_MotionExists { get; set; }
        public static int CurrentVehicleID { get; set; }
        public bool SuspensionIsSolved { get; set; }
        #endregion

        #region Check Variables
        public int AssemblyChecker;
        public bool SuspensionIsAssembled = false;
        public bool TireIsAssembled = false;
        public bool SpringIsAssembled = false;
        public bool DamperIsAssembled = false;
        public bool ARBIsAssembled = false;
        public bool ChassisIsAssembled = false;
        public bool WAIsAssembled = false;
        public bool VehicleHasBeenValidated = false;
        public bool SetupChangeRequested = false;
        #endregion

        #region Unde/Redo Stack
        public Stack<ICommand> _UndocommandsVehicle = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsVehicle = new Stack<ICommand>();
        #endregion

        #region Declaration of the Global List of the Vehicle Object
        public static List<Vehicle> List_Vehicle = new List<Vehicle>();
        public static Vehicle Assembled_Vehicle = new Vehicle();
        #endregion

        #region Input Item Class Object declaration
        public VehicleGUI vehicleGUI;
        //public SuspensionCoordinatesMaster sc_FL, sc_FR, sc_RL, sc_RR;
        public SuspensionCoordinatesFront sc_FL;
        public SuspensionCoordinatesFrontRight sc_FR;
        public SuspensionCoordinatesRear sc_RL;  
        public SuspensionCoordinatesRearRight sc_RR;
        public Tire tire_FL, tire_FR, tire_RL, tire_RR;
        public Spring spring_FL, spring_FR, spring_RL, spring_RR;
        public Damper damper_FL, damper_FR, damper_RL, damper_RR;
        public AntiRollBar arb_FL, arb_FR, arb_RL, arb_RR;
        public Chassis chassis_vehicle;
        /// <remarks>
        /// public Chassis[] chassis_vehicleOP;
        /// public OutputClass[] oc_Vehicle;
        /// 
        /// The above 2 class should be used as a replacement for all the loose variables hanging around in the vehicle class such as RollAngle, PitchAngle, NewWheelbase, New_SMCoG.. etc.,
        /// </remarks>


        public WheelAlignment wa_FL, wa_FR, wa_RL, wa_RR;

        public List<OutputClass> oc_FL, oc_FR, oc_RL, oc_RR;
        public Motion vehicle_Motion;
        public LoadCase vehicleLoadCase;
        public SetupChange vehicleSetupChange;
        #endregion

        #region Solver Object Declaration
        public SolverMasterClass MasterSolver = new SolverMasterClass();
        public DoubleWishboneKinematicsSolver DWSolver = new DoubleWishboneKinematicsSolver();
        public McPhersonKinematicsSolver MPSolver = new McPhersonKinematicsSolver();
        public VehicleOutputsCalculator VehicleOutputsSolver = new VehicleOutputsCalculator();
        public VehicleModel vehicleModel = new VehicleModel();
        #endregion

        #region Local variables for calculations
        public double[] CW = new double[4];
        public int Identifier_FL, Identifier_FR, Identifier_RL, Identifier_RR;
        public int DoubleWishboneFront, DoubleWishboneRear, McPhersonFront, McPhersonRear;
        public int PushRodIdentifierFront, PushRodIdentifierRear, PullRodIdentifierFront, PullRodIdentifierRear;
        public int UARBIdentifierFront, UARBIdentifierRear, TARBIdentifierFront, TARBIdentifierRear;
        public double /*InputOrigin_x, InputOrigin_y, InputOrigin_z,*/ OutputOrigin_x, OutputOrigin_y, OutputOrigin_z;
        public double WheelBase, TrackF, TrackR, TrackAvg;
        public double[] New_WheelBase, New_TrackF, New_TrackR;
        public double SM_Vehicle, SM_Front, SM_Rear, SM_FL, SM_FR, SM_RL, SM_RR;
        public double NSM_FL, NSM_FR, NSM_RL, NSM_RR;
        public double[] New_SMCoGx, New_SMCoGz, New_SMCoGy, FinalRideHeight_ForTrans_VehicleCGy;
        public double Vehicle_CG_y;
        public double[] /*RollAngle_Front, RollAngle_Rear, RollAngle_Chassis*/ RollAngle;
        public double[] /*PitchAngle_Left, PitchAngle_Right, PitchAngle_Chassis*/ PitchAngle;
        public double QP_FL, QP_FR, P1x_FL, P1x_FR, P1z_FL, P1z_FR, P_FLFR1, P_FLFR2, ARB_Twist_Front, QP_RL, QP_RR, P1x_RL, P1x_RR, P1z_RL, P1z_RR, P_RLRR1, P_RLRR2, ARB_Twist_Rear; //  These ARB Motorion Ratios are the Chassis/ARB Twist Motion Rations
        public double[] ARB_MR_Front, ARB_MR_Rear;
        public double ARB_Rate_Nmm_FL, ARB_Rate_Nmm_FR, ARB_Rate_Nmm_RL, ARB_Rate_Nmm_RR; // These local ARB Rate variabbles will be used so that they can be set to zero when the ARB is not needed. The AntiRollBar Class' ARBRate will retain its value this way. 

        #endregion

        /// <summary>
        /// Summation of the Changes calculated during the Heave Pass and Steering Pass. Basically this is the delta1 + delta2. 
        /// delta1 = Change calculated during Heave
        /// delta2 = Change calculated during Steering
        /// </summary>
        public double[] deltaNet_RollAngle = new double[100], deltaNet_Pitch = new double[100];
        #endregion

        /// <Nomenclature> - New position of the movable point is indicated using '2' after its identifying letter
        ///                  Old position of the movable point is indicated using '1' after its identifying letter
        ///                  No suffix after the identifying letter indicates a Fixed Point OR that the variable indicates the unfound movable coordinate OR any calculation that would result in the unfound coordinate
        /// </Nomenclature>

        /// <Axis System> X -> Lateral, towards the right when looking at car the from front such that bulkhead and engine are both ahead (RWD)
        ///               Y -> Vertical Upwards
        ///               Z -> Longitudinal, towards the oberserver when he is looking at the car from front such that bulkhead and engine are both ahead (RWD)
        /// </Axis>


        #region Auxillary Methods

        /// <summary>
        /// The underlying purpose of these functions to reset certain properties of the Vehicle in order to carry out the Re-Calculations which the user wants to perform
        /// </summary>


        public void OutputClassInitializer(SuspensionCoordinatesMaster _scmOP, SuspensionCoordinatesMaster _scmIP)
        {
            _scmOP.DoubleWishboneIdentifierFront = _scmIP.DoubleWishboneIdentifierFront;
            _scmOP.McPhersonIdentifierFront = _scmIP.McPhersonIdentifierFront;
            _scmOP.PushrodIdentifierFront = _scmIP.PushrodIdentifierFront;
            _scmOP.PullrodIdentifierFront = _scmIP.PullrodIdentifierFront;
            _scmOP.UARBIdentifierFront = _scmIP.UARBIdentifierFront;
            _scmOP.TARBIdentifierFront = _scmIP.TARBIdentifierFront;

            _scmOP.DoubleWishboneIdentifierRear = _scmIP.DoubleWishboneIdentifierRear;
            _scmOP.McPhersonIdentifierRear = _scmIP.McPhersonIdentifierRear;
            _scmOP.PushrodIdentifierRear = _scmIP.PushrodIdentifierRear;
            _scmOP.PullrodIdentifierRear = _scmIP.PullrodIdentifierRear;
            _scmOP.UARBIdentifierRear = _scmIP.UARBIdentifierRear;
            _scmOP.TARBIdentifierRear = _scmIP.TARBIdentifierRear;

        }

        public void OverrideCornerWeights(double Override_CW_FL, double Override_CW_FR, double Override_CW_RL, double Override_CW_RR)
        {
            //
            // This method is used to override the corner weights with the weights that have been calculated or obtained from user in the Recalculate Methods

            CW[0] = Override_CW_FL;
            CW[1] = Override_CW_FR;
            CW[2] = Override_CW_RL;
            CW[3] = Override_CW_RR;
        }

        public void Reset_PushrodLengths()
        {
            //
            //This method is used to reset the Pushrod lengths to the values that were calculated just after the Suspension Coordinates item was created. This is so that the there is no residue error while doing the Recalculation

            sc_FL.PushRodLength = sc_FL.PushRodLength_1;
            sc_FR.PushRodLength = sc_FR.PushRodLength_1;
            sc_RL.PushRodLength = sc_RL.PushRodLength_1;
            sc_RR.PushRodLength = sc_RR.PushRodLength_1;

        }

        public void Reset_CornerWeights(double Reset_CW_FL, double Reset_CW_FR, double Reset_CW_RL, double Reset_CW_RR, int OutPutIndex)
        {
            //
            //This method is used to reset the corner weights to the values that were calculated just after the "Calculate Results" button was pressed. This is so that the there is no residue error while doing the Recalculation

            CW[0] = Reset_CW_FL;
            CW[1] = Reset_CW_FR;
            CW[2] = Reset_CW_RL;
            CW[3] = Reset_CW_RR;

            oc_FL[OutPutIndex].CW = Reset_CW_FL;
            oc_FR[OutPutIndex].CW = Reset_CW_FR;
            oc_RL[OutPutIndex].CW = Reset_CW_RL;
            oc_RR[OutPutIndex].CW = Reset_CW_RR;
        }

        public void Reset_RideHeight()
        {
            for (int i_reset = 0; i_reset < oc_FL.Count; i_reset++)
            {
                oc_FL[i_reset].FinalRideHeight = oc_FL[i_reset].FinalRideHeight_1;
                oc_FR[i_reset].FinalRideHeight = oc_FR[i_reset].FinalRideHeight_1;
                oc_RL[i_reset].FinalRideHeight = oc_RL[i_reset].FinalRideHeight_1;
                oc_RR[i_reset].FinalRideHeight = oc_RR[i_reset].FinalRideHeight_1;
            }

        }

        public void Reset_Deflections()
        {
            for (int i_Reset = 0; i_Reset < oc_FL.Count; i_Reset++)
            {
                oc_FL[i_Reset].Corrected_SpringDeflection = oc_FL[i_Reset].Corrected_SpringDeflection_1;
                oc_FR[i_Reset].Corrected_SpringDeflection = oc_FR[i_Reset].Corrected_SpringDeflection_1;
                oc_RL[i_Reset].Corrected_SpringDeflection = oc_RL[i_Reset].Corrected_SpringDeflection_1;
                oc_RR[i_Reset].Corrected_SpringDeflection = oc_RR[i_Reset].Corrected_SpringDeflection_1;

                oc_FL[i_Reset].Corrected_WheelDeflection = oc_FL[i_Reset].Corrected_WheelDeflection_1;
                oc_FR[i_Reset].Corrected_WheelDeflection = oc_FR[i_Reset].Corrected_WheelDeflection_1;
                oc_RL[i_Reset].Corrected_WheelDeflection = oc_RL[i_Reset].Corrected_WheelDeflection_1;
                oc_RR[i_Reset].Corrected_WheelDeflection = oc_RR[i_Reset].Corrected_WheelDeflection_1;
            }
        }

        public void Reset_RideRate()
        {
            for (int i_Reset = 0; i_Reset < oc_FL.Count; i_Reset++)
            {
                oc_FL[i_Reset].RideRate = oc_FL[i_Reset].RideRate_1;
                oc_FR[i_Reset].RideRate = oc_FR[i_Reset].RideRate_1;
                oc_RL[i_Reset].RideRate = oc_RL[i_Reset].RideRate_1;
                oc_RR[i_Reset].RideRate = oc_RR[i_Reset].RideRate_1;
            }
        }
        public void ChassisCornerMassCalculator()
        {
            double VehicleWeight = Math.Abs(chassis_vehicle.SuspendedMass + chassis_vehicle.NonSuspendedMassFL + chassis_vehicle.NonSuspendedMassFR + chassis_vehicle.NonSuspendedMassRL + chassis_vehicle.NonSuspendedMassRR);
            vehicleModel.InitializeVehicleOutputModel(this, false, false, SimulationType.Dummy);
            vehicleModel.ComputeVehicleModel_SummationOfResults(this);

            for (int i_chassis = 0; i_chassis < oc_FL.Count; i_chassis++)
            {

                SM_FL = oc_FL[i_chassis].CW - Math.Abs(NSM_FL * 9.81);
                SM_FR = oc_FR[i_chassis].CW - Math.Abs(NSM_FR * 9.81);
                SM_RL = oc_RL[i_chassis].CW - Math.Abs(NSM_RL * 9.81);
                SM_RR = oc_RR[i_chassis].CW - Math.Abs(NSM_RR * 9.81);

                oc_FL[i_chassis].CW_1 = oc_FL[i_chassis].CW;

                oc_FR[i_chassis].CW_1 = oc_FR[i_chassis].CW;

                oc_RL[i_chassis].CW_1 = oc_RL[i_chassis].CW;

                oc_RR[i_chassis].CW_1 = oc_RR[i_chassis].CW;
            }
        }

        #endregion


        #region Method to create a new Vehicle
        public void CreateNewVehicle(int l_create_vehicle, Vehicle create_vehicle_list)
        {
            ///<<summary>
            ///This section of the code creates a new Vehicle and addes it to the List of Vehicle objects 
            ///</summary>

            #region Adding a new Vehicle to the list of Vehicles
            Vehicle vehicle_list = create_vehicle_list;

            List_Vehicle.Insert(l_create_vehicle, vehicle_list);
            List_Vehicle[l_create_vehicle]._VehicleName = "Vehicle " + Convert.ToString(l_create_vehicle + 1);
            List_Vehicle[l_create_vehicle].VehicleID = Vehicle.VehicleCounter + 1;
            if (List_Vehicle[l_create_vehicle].sc_FL != null)
            {
                List_Vehicle[l_create_vehicle].Vehicle_MotionExists = List_Vehicle[l_create_vehicle].sc_FL.SuspensionMotionExists;
                Kinematics_Software_New.M1_Global.vehicleGUI[l_create_vehicle].Vehicle_MotionExists = Vehicle.List_Vehicle[l_create_vehicle].sc_FL.SuspensionMotionExists;
            }
            List_Vehicle[l_create_vehicle]._UndocommandsVehicle = new Stack<ICommand>();
            List_Vehicle[l_create_vehicle]._RedocommandsVehicle = new Stack<ICommand>();
            #endregion



        }
        #endregion

        /// <summary>
        /// Assign the Assembly Validator variables from the GUI Class to the Main Vehicle Class
        /// </summary>
        /// <param name="_vGUIVal">Object of the Vehicle GUI Class</param>
        public void AssignAssemblyValidators(VehicleGUI _vGUIVal)
        {
            SuspensionIsAssembled = _vGUIVal.SuspensionIsAssembled_GUI;
            TireIsAssembled = _vGUIVal.TireIsAssembled_GUI;
            SpringIsAssembled = _vGUIVal.SpringIsAssembled_GUI;
            DamperIsAssembled = _vGUIVal.DamperIsAssembled_GUI;
            ARBIsAssembled = _vGUIVal.ARBIsAssembled_GUI;
            ChassisIsAssembled = _vGUIVal.ChassisIsAssembled_GUI;
            WAIsAssembled = _vGUIVal.WAIsAssembled_GUI;
        }

        #region Vehicle Validator
        /// <summary>
        /// Method to Validate the Vehicle 
        /// </summary>
        public bool ValidateAssembly(out string _ErrorMessage)
        {
            AssemblyChecker = 0;
            _ErrorMessage = null;

            if (!SuspensionIsAssembled)
            {
                _ErrorMessage = "Suspension is not assembled";
                return false;
            }
            else AssemblyChecker++;

            if (!TireIsAssembled)
            {
                _ErrorMessage = "Tire is not assembled";
                return false;
            }
            else AssemblyChecker++;

            if (!SpringIsAssembled)
            {
                _ErrorMessage = "Spring is not assembled";
                return false;
            }
            else AssemblyChecker++;

            if (!DamperIsAssembled)
            {
                _ErrorMessage = "Damper is not assembled";
                return false;
            }
            else AssemblyChecker++;


            if (!ARBIsAssembled)
            {
                _ErrorMessage = "ARB is not assembled";
                AssemblyChecker++;
                return false;
            }
            else AssemblyChecker++;

            if (!ChassisIsAssembled)
            {
                _ErrorMessage = "Chassis is not assembled";
                return false;
            }
            else AssemblyChecker++;


            if (!WAIsAssembled)
            {
                _ErrorMessage = "Wheel Alignment is not assembled";
                return false;
            }
            else AssemblyChecker++;


            if (AssemblyChecker == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion


        #region Method to Modify Vehicle data OR redo the actions of Vehicle
        public void ModifyObjectData(int l_modify_vehicle, object modify_vehicle_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the vehicle is being modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>

            #region Ediing the Vehicle object
            Vehicle vehicle_list = (Vehicle)modify_vehicle_list;

            ICommand cmd = List_Vehicle[l_modify_vehicle];
            List_Vehicle[l_modify_vehicle]._UndocommandsVehicle.Push(cmd);


            vehicle_list._UndocommandsVehicle = List_Vehicle[l_modify_vehicle]._UndocommandsVehicle;
            vehicle_list._RedocommandsVehicle = List_Vehicle[l_modify_vehicle]._RedocommandsVehicle;
            vehicle_list._VehicleName = List_Vehicle[l_modify_vehicle]._VehicleName;
            vehicle_list.Vehicle_Results_Tracker = List_Vehicle[l_modify_vehicle].Vehicle_Results_Tracker;

            List_Vehicle[l_modify_vehicle] = vehicle_list;
            List_Vehicle[l_modify_vehicle].VehicleID = l_modify_vehicle + 1;

            ///<summary>
            ///Since the Vehicle is now allowed to be created without the creation of the Inputs items such as Suspension,Spring, Damper etc., this IF Loop is necesaary to ensure the 
            ///Suspension is not used when it is null
            ///</summary>
            if (List_Vehicle[l_modify_vehicle].sc_FL != null) 
            {
                List_Vehicle[l_modify_vehicle].Vehicle_MotionExists = List_Vehicle[l_modify_vehicle].sc_FL.SuspensionMotionExists;
                Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].Vehicle_MotionExists = Vehicle.List_Vehicle[l_modify_vehicle].sc_FL.SuspensionMotionExists;
            }
            List_Vehicle[l_modify_vehicle].VehicleIsModified = true;
            #endregion

            if (modify_vehicle_list is ICommand)
            {
                VehicleGUI.DisplayVehicleItem(List_Vehicle[l_modify_vehicle]);
            }
            if (redo_Identifier == false)
            {
                _RedocommandsVehicle.Clear();
            }

            ///<summary>
            ///Since the Vehicle is now allowed to be created without the creation of the Inputs items such as Suspension,Spring, Damper etc., this IF Loop is necesaary to ensure the 
            ///<see cref="Kinematics_Software_New.EditVehicleCAD(CAD, int, bool, bool, bool)"/> is not invoked when a Suspension Item is null
            ///</summary>
            if (List_Vehicle[l_modify_vehicle].sc_FL != null)
            {
                if (Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].VisualizationType == VehicleVisualizationType.Generic)
                {
                    Kinematics_Software_New.EditVehicleCAD(Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].CADVehicleInputs, l_modify_vehicle, true, Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].CadIsTobeImported,
                                                                                                                                                                                Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].PlotWheel);  
                }
                else if (Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].VisualizationType == VehicleVisualizationType.ImportedCAD)
                {
                    Kinematics_Software_New.EditVehicleCAD(Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].importCADForm.importCADViewport, l_modify_vehicle, true, Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].CadIsTobeImported,
                                                                                                                                                                                Kinematics_Software_New.M1_Global.vehicleGUI[l_modify_vehicle].PlotWheel);
                }
            }

            Kinematics_Software_New.ComboboxVehicleOperationsInvoker();


            Kinematics_Software_New.DeleteNavBarControlResultsGroupANDTabPages_Invoker(l_modify_vehicle);




        }
        #endregion


        #region Method to Undo the modifications done to the vehicle  
        public void Undo_ModifyObjectData(int l_unexcute_vehicle, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            try
            {
                ICommand cmd = List_Vehicle[l_unexcute_vehicle];

                List_Vehicle[l_unexcute_vehicle]._RedocommandsVehicle.Push(cmd);

                List_Vehicle[l_unexcute_vehicle] = (Vehicle)command;

                VehicleGUI.DisplayVehicleItem(List_Vehicle[l_unexcute_vehicle]);

                if (Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].VisualizationType == VehicleVisualizationType.Generic)
                {
                    Kinematics_Software_New.EditVehicleCAD(Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].CADVehicleInputs, l_unexcute_vehicle, true, Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].CadIsTobeImported, Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].PlotWheel);
                }
                else if (Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].VisualizationType == VehicleVisualizationType.ImportedCAD)
                {
                    Kinematics_Software_New.EditVehicleCAD(Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].importCADForm.importCADViewport, l_unexcute_vehicle, true, Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].CadIsTobeImported, Kinematics_Software_New.M1_Global.vehicleGUI[l_unexcute_vehicle].PlotWheel);
                }

                Kinematics_Software_New.ComboboxVehicleOperationsInvoker();

                Kinematics_Software_New.DeleteNavBarControlResultsGroupANDTabPages_Invoker(l_unexcute_vehicle);


            }
            catch (Exception) { }


        }
        #endregion


        #region Constructors

        #region Base constructor
        public Vehicle()
        {
            ///<summary>
            /// This constructor is only used to Initialize the Vehicle Object which is going to be used for Calculations
            /// </summary>

            // This constructor is created here only so that the Chassis object can be initialized without having to pass any arguments.
            // This is needed because otherwise the chassis object will not be instantiated untill a chassis item is created. 
            // If the user wants to save the file without creating a chassis object he will not be able to do so unless this constructor is used to create the chassis object 
            VehicleIsModified = false;
            SuspensionIsSolved = false;
            
        }
        #endregion 

        #region Overloaded constructor to assemble the Vehicle
        public Vehicle(VehicleGUI _vehicleGUI, int[] identifier, SuspensionCoordinatesMaster[] sc, Tire[] tire, Spring[] spring, Damper[] damper, AntiRollBar[] arb, Chassis chassis, WheelAlignment[] wa, OutputClass[] oc)
        {
            ///<summary>
            ///This is the constructor of the Vehicle Class. 
            ///It uses the Arrys of Input Item Objects (passed as parameters) to create a Vehicle Item which can be used for simulations
            ///</summary

            ///<remarks>
            ///Below there are bunch of IF loops employed which check if the Input Items are null. This is necessary for 2 reasons
            ///1 -> A Vehicle item can now b created even if the Input Items are not created. 
            ///2 -> In this constrctor method the Suspension and Chassis Input items are used for some calculations. If they were only assigned then it woudln't have been a problem even if they were null. But since they are used for some prelim calculations
            ///     they can't be null
            /// </remarks>

            // New Vehicle Creation
            #region Determining if the Vehicle has a Motion 
            Vehicle_MotionExists = _vehicleGUI.Vehicle_MotionExists;
            #endregion

            #region Assigning the Vehicle GUI and Vehicle Validators
            vehicleGUI = _vehicleGUI;
            AssignAssemblyValidators(_vehicleGUI);
            #endregion

            #region Determining the Tpe of Suspension, Actuation and Anti-Roll Bar


            if (sc[0] != null || sc[1] != null || sc[2] != null || sc[3] != null) 
            {
                #region Vehicle Origin Initialization
                OutputOrigin_x = vehicleGUI._OutputOriginX;
                OutputOrigin_y = vehicleGUI._OutputOriginY;
                OutputOrigin_z = vehicleGUI._OutputOriginZ;
                #endregion

                #region Suspension Type Initialization

                DoubleWishboneFront = sc[0].DoubleWishboneIdentifierFront;
                DoubleWishboneRear = sc[2].DoubleWishboneIdentifierRear;

                McPhersonFront = sc[0].McPhersonIdentifierFront;
                McPhersonRear = sc[2].McPhersonIdentifierRear;

                #endregion

                #region Anti-Roll Bar Type Initialization

                UARBIdentifierFront = sc[0].UARBIdentifierFront;
                UARBIdentifierRear = sc[2].UARBIdentifierFront;

                TARBIdentifierFront = sc[0].TARBIdentifierFront;
                TARBIdentifierRear = sc[2].TARBIdentifierRear;
                #endregion

                #region Actuation Type Initialization

                PushRodIdentifierFront = sc[0].PushrodIdentifierFront;
                PushRodIdentifierRear = sc[2].PushrodIdentifierRear;

                PullRodIdentifierFront = sc[0].PullrodIdentifierFront;
                PullRodIdentifierRear = sc[2].PullrodIdentifierRear;

                #endregion 
            }

            #endregion

            #region Vehicle Model Initialization
            vehicleModel = new VehicleModel(); 
            #endregion

            #region FRONT LEFT Initialization
            Identifier_FL = identifier[0];
            sc_FL = (SuspensionCoordinatesFront)sc[0];
            tire_FL = tire[0];
            spring_FL = spring[0];
            damper_FL = damper[0];
            arb_FL = arb[0];
            if (sc_FL != null)
            {
                ARB_Rate_Nmm_FL = AntiRollBarRate_Nmm(sc_FL, arb_FL);
                arb_FL.AntiRollBarRate_Nmm = ARB_Rate_Nmm_FL; 
            }

            wa_FL = wa[0];
            #endregion

            #region FRONT RIGHT Initialization
            Identifier_FR = identifier[1];
            sc_FR = (SuspensionCoordinatesFrontRight)sc[1];
            tire_FR = tire[1];
            spring_FR = spring[1];
            damper_FR = damper[1];
            arb_FR = arb[1];
            if (sc_FR != null)
            {
                ARB_Rate_Nmm_FR = AntiRollBarRate_Nmm(sc_FR, arb_FR); 
            }
            arb_FR.AntiRollBarRate_Nmm = ARB_Rate_Nmm_FR;
            wa_FR = wa[1];
            #endregion

            #region REAR LEFT Initialization
            Identifier_RL = identifier[2];
            sc_RL = (SuspensionCoordinatesRear)sc[2];
            tire_RL = tire[2];
            spring_RL = spring[2];
            damper_RL = damper[2];
            arb_RL = arb[2];
            if (sc_RL != null)
            {
                ARB_Rate_Nmm_RL = AntiRollBarRate_Nmm(sc_RL, arb_RL);
                arb_RL.AntiRollBarRate_Nmm = ARB_Rate_Nmm_RL; 
            }
            wa_RL = wa[2];
            #endregion

            #region REAR RIGHT Initialization
            Identifier_RR = identifier[3];
            sc_RR = (SuspensionCoordinatesRearRight)sc[3];
            tire_RR = tire[3];
            spring_RR = spring[3];
            damper_RR = damper[3];
            arb_RR = arb[3];
            if (sc_RR != null)
            {
                ARB_Rate_Nmm_RR = AntiRollBarRate_Nmm(sc_RR, arb_RR);
                arb_RR.AntiRollBarRate_Nmm = ARB_Rate_Nmm_RR; 
            }
            wa_RR = wa[3];
            #endregion

            #region Chassis Initialization
            chassis_vehicle = chassis;

            if (chassis != null)
            {
                SM_Vehicle = chassis_vehicle.SuspendedMass;
                NSM_FL = chassis_vehicle.NonSuspendedMassFL;
                NSM_FR = chassis_vehicle.NonSuspendedMassFR;
                NSM_RL = chassis_vehicle.NonSuspendedMassRL;
                NSM_RR = chassis_vehicle.NonSuspendedMassRR; 
            }

            #endregion

            #region Preliminary Calculations
            if (sc_FL != null || sc_FR != null || sc_RL != null || sc_RR != null)
            {
                // Preliminary Calculations
                #region Wheelbase & Trackwidth Calculations
                WheelBase = Math.Abs(sc_FL.W1z - sc_RL.W1z);
                TrackF = Math.Abs(sc_FL.W1x - sc_FR.W1x);
                TrackR = Math.Abs(sc_RL.W1x - sc_RR.W1x);
                TrackAvg = (TrackF + TrackR) / 2;
                #endregion

                #region Motion Ratio Calculations

                #region Initial Motion Ratio Calculations
                sc_FL.MotionRatio(McPhersonFront, McPhersonRear, PullRodIdentifierFront, PullRodIdentifierRear, 1);
                sc_FR.MotionRatio(McPhersonFront, McPhersonRear, PullRodIdentifierFront, PullRodIdentifierRear, 2);
                sc_RL.MotionRatio(McPhersonFront, McPhersonRear, PullRodIdentifierFront, PullRodIdentifierRear, 3);
                sc_RR.MotionRatio(McPhersonFront, McPhersonRear, PullRodIdentifierFront, PullRodIdentifierRear, 4);
                #endregion

                #endregion 
            } 
            #endregion

        }
        #endregion

        #region Output class Initializer Helper Method
        private void InitializeOutputClass(OutputClass _ocInit, int Identifier, SuspensionCoordinatesMaster _scmInit)
        {
            _ocInit.Identifier = Identifier;
            OutputClassInitializer(_ocInit.scmOP, _scmInit);
        }
        #endregion

        #region Output Class Initializer Method
        public void InitializeOutputClass(int noOfSteps)
        {
            oc_FL = new List<OutputClass>();
            oc_FR = new List<OutputClass>();
            oc_RL = new List<OutputClass>();
            oc_RR = new List<OutputClass>();

            for (int i_InitOC = 0; i_InitOC < noOfSteps; i_InitOC++)
            {
                oc_FL.Add(new OutputClass());
                oc_FR.Add(new OutputClass());
                oc_RL.Add(new OutputClass());
                oc_RR.Add(new OutputClass());

                InitializeOutputClass(oc_FL[i_InitOC], 1, sc_FL);
                InitializeOutputClass(oc_FR[i_InitOC], 2, sc_FR);
                InitializeOutputClass(oc_RL[i_InitOC], 3, sc_RL);
                InitializeOutputClass(oc_RR[i_InitOC], 4, sc_RR);

            }

            New_WheelBase = new double[noOfSteps];
            New_TrackF = new double[noOfSteps];
            New_TrackR = new double[noOfSteps];

            New_SMCoGx = new double[noOfSteps];
            New_SMCoGy = new double[noOfSteps];
            New_SMCoGz = new double[noOfSteps];

            RollAngle = new double[noOfSteps];
            PitchAngle = new double[noOfSteps];

            ARB_MR_Front = new double[noOfSteps];
            ARB_MR_Rear = new double[noOfSteps];
        }
        #endregion

        #endregion


        #region Anti-Roll Bar Rate Calculator (N/mm)
        public double AntiRollBarRate_Nmm(SuspensionCoordinatesMaster sc, AntiRollBar arb)
        {
            ///<summary>
            ///This method is used to calculate the AntiRollBar Rate in N/mm and assign it to a local variable
            ///</summary>

            double AntiRollBarRate_Nmm, P1Q;

            //FRONT LEFT ARB Lever 
            P1Q = Math.Sqrt(Math.Pow((sc.P1y - sc.Q1y), 2) + Math.Pow((sc.P1z - sc.Q1z), 2) + Math.Pow((sc.P1x - sc.Q1x), 2));

            //Converting from Nm/deg to N/mm
            AntiRollBarRate_Nmm = (180 / Math.PI) * ((Convert.ToDouble(arb.AntiRollBarRate) * 1000) / (P1Q)) * Math.Atan(1 / P1Q);

            return AntiRollBarRate_Nmm;

        }
        #endregion


        #region Kinematics Invoker

        #region Public Translator Invoker
        public void InitializeTranslation(bool TranslateToGround, bool _motionExists, double _IPx, double _IPy, double _IPz, double _OPx, double _OPy, double _OPz)
        {
            if (TranslateToGround)
            {
                for (int i_trans = 0; i_trans < oc_FL.Count; i_trans++)
                {
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, 0, -oc_FL[i_trans].scmOP.W1y, 0, oc_FL[i_trans].scmOP, oc_FL[i_trans]);
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, 0, -oc_FR[i_trans].scmOP.W1y, 0, oc_FR[i_trans].scmOP, oc_FR[i_trans]);
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, 0, -oc_RL[i_trans].scmOP.W1y, 0, oc_RL[i_trans].scmOP, oc_RL[i_trans]);
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, 0, -oc_RR[i_trans].scmOP.W1y, 0, oc_RR[i_trans].scmOP, oc_RR[i_trans]);
                }
            }

            else
            {
                for (int i_trans = 0; i_trans < oc_FL.Count; i_trans++)
                {
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, _OPx, _OPy, _OPz, oc_FL[i_trans].scmOP, oc_FL[i_trans]);
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, _OPx, _OPy, _OPz, oc_FR[i_trans].scmOP, oc_FR[i_trans]);
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, _OPx, _OPy, _OPz, oc_RL[i_trans].scmOP, oc_RL[i_trans]);
                    TranslatorMethod(_motionExists, _IPx, _IPy, _IPz, _OPx, _OPy, _OPz, oc_RR[i_trans].scmOP, oc_RR[i_trans]);
                }
            }
        } 
        #endregion

        #region Private Translator Invoker
        private void TranslatorMethod(bool _motionExists, double IPx, double IPy, double IPz, double OPx, double OPy, double OPz, SuspensionCoordinatesMaster _scm, OutputClass _oc)
        {
            MasterSolver.TranslateToRequiredCS(_scm, _oc, this, _motionExists, IPx, IPy,  IPz, OPx, OPy, OPz);
        }
        #endregion

        #region Private Kinematics Invokers
        /// <summary>
        /// Kinematics Invoker for the FRONT
        /// </summary>
        /// <param name="_motionExists"></param>
        /// <param name="_wheelOrSpringDeflections_FL"></param>
        /// <param name="_wheelOrSpringDeflections_FR"></param>
        /// <param name="_recalculateRearSteering"></param>
        private void KinematicsInvoker_Front(bool _motionExists, List<double> _wheelOrSpringDeflections_FL, List<double> _wheelOrSpringDeflections_FR, bool _recalculateRearSteering)
        {
            #region ---FRONT Kinematics---
            if ((((String.Format("{0:0}", CW[0]) == String.Format("{0:0}", CW[1]))) && (((spring_FL.SpringRate == spring_FR.SpringRate)) && (spring_FL.SpringPreload == spring_FR.SpringPreload) && (spring_FL.SpringFreeLength == spring_FR.SpringFreeLength))) &&
               ((((damper_FL.DamperGasPressure == damper_FR.DamperGasPressure)) && (damper_FL.DamperShaftDia == damper_FR.DamperShaftDia))) && ((((tire_FL.TireRate == tire_FR.TireRate)))))
            {
                ///<remarks>
                ///Changin the ARB Rate to 0 in case left and right springs, dampers and tires are same and also the weights are same
                /// </remarks>
                #region Invoking Kinematics Solver for Front Left Corner for NO ARB
                //
                //Invoking Kinematics Solver for Front Left Corner
                //
                if (McPhersonFront == 1)
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_FL_Fx, vehicleLoadCase.TotalLoad_FL_Fz, vehicleLoadCase.TotalLoad_FL_Fy, vehicleLoadCase.NSM_FL_Mx, vehicleLoadCase.NSM_FL_Mz);
                    MPSolver.KinematicsMcPherson(Identifier_FL, sc_FL, wa_FL, tire_FL, arb_FL, 0, spring_FL, damper_FL, oc_FL, this, _wheelOrSpringDeflections_FL, _motionExists, _recalculateRearSteering);
                }
                
                else
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_FL_Fx, vehicleLoadCase.TotalLoad_FL_Fz, vehicleLoadCase.TotalLoad_FL_Fy, vehicleLoadCase.NSM_FL_Mx, vehicleLoadCase.NSM_FL_Mz);
                    DWSolver.Kinematics(Identifier_FL, sc_FL, wa_FL, tire_FL, arb_FL, 0, spring_FL, damper_FL, oc_FL, this, _wheelOrSpringDeflections_FL, _motionExists, _recalculateRearSteering);
                }
                #endregion


                #region Invoking Kinematics Solver for Front Right Corner for No ARB
                //
                // Invoking Kinematics Solver for Front Right Corner
                //
                if (McPhersonFront == 1)
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_FR_Fx, vehicleLoadCase.TotalLoad_FR_Fz, vehicleLoadCase.TotalLoad_FR_Fy, vehicleLoadCase.NSM_FR_Mx, vehicleLoadCase.NSM_FR_Mz);
                    MPSolver.KinematicsMcPherson(Identifier_FR, sc_FR, wa_FR, tire_FR, arb_FR, 0, spring_FR, damper_FR, oc_FR, this, _wheelOrSpringDeflections_FR, _motionExists, _recalculateRearSteering);
                }
                else
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_FR_Fx, vehicleLoadCase.TotalLoad_FR_Fz, vehicleLoadCase.TotalLoad_FR_Fy, vehicleLoadCase.NSM_FR_Mx, vehicleLoadCase.NSM_FR_Mz);

                    DWSolver.Kinematics(Identifier_FR, sc_FR, wa_FR, tire_FR, arb_FR, 0, spring_FR, damper_FR, oc_FR, this, _wheelOrSpringDeflections_FR, _motionExists, _recalculateRearSteering);
                }
                #endregion


            }

            else
            {
                #region Invoking Kinematics Solver for Front Left Corner with ARB
                //Invoking Kinematics Solver for Front Left Corner
                if (McPhersonFront == 1)
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_FL_Fx, vehicleLoadCase.TotalLoad_FL_Fz, vehicleLoadCase.TotalLoad_FL_Fy, vehicleLoadCase.NSM_FL_Mx, vehicleLoadCase.NSM_FL_Mz);
                    MPSolver.KinematicsMcPherson(Identifier_FL, sc_FL, wa_FL, tire_FL, arb_FL, ARB_Rate_Nmm_FL, spring_FL, damper_FL, oc_FL, this, _wheelOrSpringDeflections_FL, _motionExists, _recalculateRearSteering);
                }

                else
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_FL_Fx, vehicleLoadCase.TotalLoad_FL_Fz, vehicleLoadCase.TotalLoad_FL_Fy, vehicleLoadCase.NSM_FL_Mx, vehicleLoadCase.NSM_FL_Mz);

                    DWSolver.Kinematics(Identifier_FL, sc_FL, wa_FL, tire_FL, arb_FL, ARB_Rate_Nmm_FL, spring_FL, damper_FL, oc_FL, this, _wheelOrSpringDeflections_FL, _motionExists, _recalculateRearSteering);
                }
                #endregion


                #region Invoking Kinematics Solver for Front Right Corner with ARB
                // Invoking Kinematics Solver for Front Right Corner
                if (McPhersonFront == 1)
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_FR_Fx, vehicleLoadCase.TotalLoad_FR_Fz, vehicleLoadCase.TotalLoad_FR_Fy, vehicleLoadCase.NSM_FR_Mx, vehicleLoadCase.NSM_FR_Mz);
                    MPSolver.KinematicsMcPherson(Identifier_FR, sc_FR, wa_FR, tire_FR, arb_FR, ARB_Rate_Nmm_FR, spring_FR, damper_FR, oc_FR, this, _wheelOrSpringDeflections_FR, _motionExists, _recalculateRearSteering);
                }
                else
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_FR_Fx, vehicleLoadCase.TotalLoad_FR_Fz, vehicleLoadCase.TotalLoad_FR_Fy, vehicleLoadCase.NSM_FR_Mx, vehicleLoadCase.NSM_FR_Mz);

                    DWSolver.Kinematics(Identifier_FR, sc_FR, wa_FR, tire_FR, arb_FR, ARB_Rate_Nmm_FR, spring_FR, damper_FR, oc_FR, this, _wheelOrSpringDeflections_FR, _motionExists, _recalculateRearSteering);
                }
                #endregion
            }
            #endregion
        }
        /// <summary>
        /// Kinematics Invoker for the REAR
        /// </summary>
        /// <param name="_motionExists"></param>
        /// <param name="_wheelDeflections_RL"></param>
        /// <param name="_wheelDeflections_RR"></param>
        /// <param name="_recalculateRearSteering"></param>
        private void KinematicsInvoker_Rear(bool _motionExists, List<double> _wheelDeflections_RL, List<double> _wheelDeflections_RR, bool _recalculateRearSteering)
        {
            #region ---Rear Kinematics---
            if ((((String.Format("{0:0}", CW[2]) == String.Format("{0:0}", CW[3]))) && (((spring_RL.SpringRate == spring_RR.SpringRate)) && (spring_RL.SpringPreload == spring_RR.SpringPreload) && (spring_RL.SpringFreeLength == spring_RR.SpringFreeLength))) &&
       ((((damper_RL.DamperGasPressure == damper_RR.DamperGasPressure)) && (damper_RL.DamperShaftDia == damper_RR.DamperShaftDia))) && ((((tire_RL.TireRate == tire_RR.TireRate)))))
            {
                ///<remarks>
                ///Changin the ARB Rate to 0 in case left and right springs, dampers and tires are same and also the weights are same
                /// </remarks>
                #region Invoking Kinematics Solver for Rear Left Corner for No ARB
                //
                //Invoking Kinematics Solver for Rear Left Corner
                //
                if (McPhersonRear == 1)
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_RL_Fx, vehicleLoadCase.TotalLoad_RL_Fz, vehicleLoadCase.TotalLoad_RL_Fy, vehicleLoadCase.NSM_RL_Mx, vehicleLoadCase.NSM_RL_Mz);
                    MPSolver.KinematicsMcPherson(Identifier_RL, sc_RL, wa_RL, tire_RL, arb_RL, 0, spring_RL, damper_RL, oc_RL, this, _wheelDeflections_RL, _motionExists, _recalculateRearSteering);
                }
                else
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_RL_Fx, vehicleLoadCase.TotalLoad_RL_Fz, vehicleLoadCase.TotalLoad_RL_Fy, vehicleLoadCase.NSM_RL_Mx, vehicleLoadCase.NSM_RL_Mz);

                    DWSolver.Kinematics(Identifier_RL, sc_RL, wa_RL, tire_RL, arb_RL, 0, spring_RL, damper_RL, oc_RL, this, _wheelDeflections_RL, _motionExists, _recalculateRearSteering);
                }
                #endregion


                #region Invoking Kinematics Solver for Rear Right Corner for no ARB
                //
                //Invoking Kinematics Solver for Rear Right Corner 
                //
                if (McPhersonRear == 1)
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_RR_Fx, vehicleLoadCase.TotalLoad_RR_Fz, vehicleLoadCase.TotalLoad_RR_Fy, vehicleLoadCase.NSM_RR_Mx, vehicleLoadCase.NSM_RR_Mz);
                    MPSolver.KinematicsMcPherson(Identifier_RR, sc_RR, wa_RR, tire_RR, arb_RR, 0, spring_RR, damper_RR, oc_RR, this, _wheelDeflections_RR, _motionExists, _recalculateRearSteering);
                }
                else
                {
                    ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                    ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                    ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                    ///</remarks>
                    DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_RR_Fx, vehicleLoadCase.TotalLoad_RR_Fz, vehicleLoadCase.TotalLoad_RR_Fy, vehicleLoadCase.NSM_RR_Mx, vehicleLoadCase.NSM_RR_Mz);

                    DWSolver.Kinematics(Identifier_RR, sc_RR, wa_RR, tire_RR, arb_RR, 0, spring_RR, damper_RR, oc_RR, this, _wheelDeflections_RR, _motionExists, _recalculateRearSteering);
                }
                #endregion

            }
            else
            {
                #region Invoking Kinematics Solver for Rear Left Corner with ARB
                {//Invoking Kinematics Solver for Rear Left Corner

                    if (McPhersonRear == 1)

                    {
                        ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                        ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                        ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                        ///</remarks>
                        MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_RL_Fx, vehicleLoadCase.TotalLoad_RL_Fz, vehicleLoadCase.TotalLoad_RL_Fy, vehicleLoadCase.NSM_RL_Mx, vehicleLoadCase.NSM_RL_Mz);
                        MPSolver.KinematicsMcPherson(Identifier_RL, sc_RL, wa_RL, tire_RL, arb_RL, ARB_Rate_Nmm_RL, spring_RL, damper_RL, oc_RL, this, _wheelDeflections_RL, _motionExists, _recalculateRearSteering);
                    }
                    else
                    {
                        ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                        ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                        ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                        ///</remarks>
                        DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_RL_Fx, vehicleLoadCase.TotalLoad_RL_Fz, vehicleLoadCase.TotalLoad_RL_Fy, vehicleLoadCase.NSM_RL_Mx, vehicleLoadCase.NSM_RL_Mz);

                        DWSolver.Kinematics(Identifier_RL, sc_RL, wa_RL, tire_RL, arb_RL, ARB_Rate_Nmm_RL, spring_RL, damper_RL, oc_RL, this, _wheelDeflections_RL, _motionExists, _recalculateRearSteering);
                    }

                }
                #endregion


                #region Invoking Kinematics Solver for Rear Right Corner
                {
                    //Invoking Kinematics Solver for Rear Right Corner 

                    if (McPhersonRear == 1)

                    {
                        ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                        ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>MPSolver.Kinematics</c> Class 
                        ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                        ///</remarks>
                        MPSolver.AssignLoadCaseMcPherson(vehicleLoadCase.TotalLoad_RR_Fx, vehicleLoadCase.TotalLoad_RR_Fz, vehicleLoadCase.TotalLoad_RR_Fy, vehicleLoadCase.NSM_RR_Mx, vehicleLoadCase.NSM_RR_Mz);
                        MPSolver.KinematicsMcPherson(Identifier_RR, sc_RR, wa_RR, tire_RR, arb_RR, ARB_Rate_Nmm_RR, spring_RR, damper_RR, oc_RR, this, _wheelDeflections_RR, _motionExists, _recalculateRearSteering);
                    }
                    else
                    {
                        ///<remarks>Passed in the order: Lateral Force, Longitudinal Force, Vertical Force</remarks>
                        ///<remarks>Moments in the order of Overturning and Self Aligning Moments. Their Coordinates have NOT been Changed as they are taken of inside the <c>DWSolver.Kinematics</c> Class 
                        ///<seealso cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
                        ///</remarks>
                        DWSolver.AssignLoadCaseDW(vehicleLoadCase.TotalLoad_RR_Fx, vehicleLoadCase.TotalLoad_RR_Fz, vehicleLoadCase.TotalLoad_RR_Fy, vehicleLoadCase.NSM_RR_Mx, vehicleLoadCase.NSM_RR_Mz);

                        DWSolver.Kinematics(Identifier_RR, sc_RR, wa_RR, tire_RR, arb_RR, ARB_Rate_Nmm_RR, spring_RR, damper_RR, oc_RR, this, _wheelDeflections_RR, _motionExists, _recalculateRearSteering);
                    }


                }
                #endregion
            }
            #endregion
        }



        private void KinematicsInvoker_For_MotionORSteering(bool _motionExists)
        {
            ///<remarks>
            ///This is the first pass of the solver where the Input Coordinates are used to calculate the new coordinates based on the Weight of the Vehicle (No Motion case) OR based on the supplied input of Motion
            /// </remarks>
            KinematicsInvoker_Front(_motionExists, vehicle_Motion.Final_WheelDeflectionsY, vehicle_Motion.Final_WheelDeflectionsY, false);
            KinematicsInvoker_Rear(_motionExists, vehicle_Motion.Final_WheelDeflectionsY, vehicle_Motion.Final_WheelDeflectionsY, false);

            ///<remarks>
            ///Here the vehicle model is simulated to solve for the 6 Degrees of Freedom and the Spring Deflections for Heave Motion
            /// </remarks>
            vehicleModel.InitializeVehicleOutputModel(this, false, true, SimulationType.MotionAnalysis);

            ///<remarks>
            ///Here the vehicle model is simulated to solve for the 7 Degrees of Freedom and the Spring Deflections for Steering Motion
            ///</remarks>
            vehicleModel.InitializeVehicleOutputModel(this, true, false, SimulationType.SteeringAnalysis);

            ///<remarks>
            ///Here the final result values are calculated by adding the Net Delta Values to the previous value of the result channel
            /// </remarks>
            vehicleModel.ComputeVehicleModel_SummationOfResults(this);

            if (vehicle_Motion.SteeringExists)
            {
                ///<remarks>
                ///This is the 2nd pass of the Solver where the already calculated coordinates inside the OutputClass are used to calculate the new coordinates based on the Steering Input
                /// </remarks>
                KinematicsInvoker_Front(_motionExists, sc_FL.SpringDeflection_DeltSteering, sc_FR.SpringDeflection_DeltSteering, true);
                KinematicsInvoker_Rear(_motionExists, sc_RL.SpringDeflection_DeltSteering, sc_RR.SpringDeflection_DeltSteering, true);

            }

            InitializeTranslation(false, true, this.sc_FL.InputOriginX, this.sc_FL.InputOriginY, this.sc_FL.InputOriginZ, 0, 0, 0);

            vehicleModel.ComputeVehicleModel_SteeringEffort(this.oc_FL, this.oc_FR, this);

            for (int i = 0; i < oc_FL.Count; i++)
            {
                vehicleModel.BoltedJoint_ARB_And_Rack(oc_FL, oc_FR, i, vehicleLoadCase.FL_BearingCoordinates, vehicleLoadCase.FR_BearingCoordinates, true, oc_FL[i].ToeLink_z, oc_FR[i].ToeLink_z, oc_FL[i].ToeLink_y, oc_FR[i].ToeLink_y);
                vehicleModel.AssignRackForces(oc_FL, oc_FR, i);
                
                vehicleModel.BoltedJoint_ARB_And_Rack(oc_FL, oc_FR, i, vehicleLoadCase.FL_BearingCoordinates, vehicleLoadCase.FR_BearingCoordinates, false, oc_FL[i].ARBDroopLink_z, oc_FR[i].ARBDroopLink_z, oc_FL[i].ARBDroopLink_y, oc_FR[i].ARBDroopLink_y);
                vehicleModel.AssignARBForces(oc_FL, oc_FR, i);
                
            }

            for (int i = 0; i < oc_RL.Count; i++)
            {
                ///<remarks>
                ///NOTE - The Boolean Variable called "Steering" which is passed in the <c>BoltedJoint_ARB_Rack</c> method is only relevent in the Front because it needs to differentiate between the Steering Rack and the ARB Coordinates. 
                ///But in the Rear there is only ARB and no steering. TRUE value is passed in the rear because only then will the variables stored in the start of the 3x2 array be accessed. 
                ///<seealso cref="VehicleModel.BoltedJoint_ARB_And_Rack(List{OutputClass}, List{OutputClass}, int, double[,], double[,], bool, double, double, double, double)"/>
                /// </remarks>
                vehicleModel.BoltedJoint_ARB_And_Rack(oc_RL, oc_RR, i, vehicleLoadCase.RL_BearingCoordinates, vehicleLoadCase.RR_BearingCoordinates, true, oc_RL[i].ARBDroopLink_z, oc_RR[i].ARBDroopLink_z, oc_RL[i].ARBDroopLink_y, oc_RR[i].ARBDroopLink_y);
                vehicleModel.AssignARBForces(oc_RL, oc_RR, i);
            }

            for (int i = 0; i < oc_FL.Count; i++)
            {
                vehicleModel.BoltedJoint_SteeringColumn(oc_FL, oc_FR, i, vehicleLoadCase.SteeringColumnBearing);
                vehicleModel.AssignSteeringColumnForces(oc_FL, i);
            }

        }
        private void KinematicsInvoker_For_NoMotion()
        {
            KinematicsInvoker_Front(false, null, null, false);

            KinematicsInvoker_Rear(false, null, null, false);

            for (int i = 0; i < oc_FL.Count; i++)
            {
                vehicleModel.BoltedJoint_ARB_And_Rack(oc_FL, oc_FR, i, vehicleLoadCase.FL_BearingCoordinates, vehicleLoadCase.FR_BearingCoordinates, true, oc_FL[i].ToeLink_z, oc_FR[i].ToeLink_z, oc_FL[i].ToeLink_y, oc_FR[i].ToeLink_y);
                vehicleModel.AssignRackForces(oc_FL, oc_FR, i);

                vehicleModel.BoltedJoint_ARB_And_Rack(oc_FL, oc_FR, i, vehicleLoadCase.FL_BearingCoordinates, vehicleLoadCase.FR_BearingCoordinates, false, oc_FL[i].ARBDroopLink_z, oc_FR[i].ARBDroopLink_z, oc_FL[i].ARBDroopLink_y, oc_FR[i].ARBDroopLink_y);
                vehicleModel.AssignARBForces(oc_FL, oc_FR, i);

            }

            for (int i = 0; i < oc_RL.Count; i++)
            {
                ///<remarks>
                ///NOTE - The Boolean Variable called "Steering" which is passed in the <c>BoltedJoint_ARB_Rack</c> method is only relevent in the Front because it needs to differentiate between the Steering Rack and the ARB Coordinates. 
                ///But in the Rear there is only ARB and no steering. TRUE value is passed in the rear because only then will the variables stored in the start of the 3x2 array be accessed. 
                ///<seealso cref="VehicleModel.BoltedJoint_ARB_And_Rack(List{OutputClass}, List{OutputClass}, int, double[,], double[,], bool, double, double, double, double)"/>
                /// </remarks>
                vehicleModel.BoltedJoint_ARB_And_Rack(oc_RL, oc_RR, i, vehicleLoadCase.RL_BearingCoordinates, vehicleLoadCase.RR_BearingCoordinates, true, oc_RL[i].ARBDroopLink_z, oc_RR[i].ARBDroopLink_z, oc_RL[i].ARBDroopLink_y, oc_RR[i].ARBDroopLink_y);
                vehicleModel.AssignARBForces(oc_RL, oc_RR, i);
            }

            for (int i = 0; i < oc_FL.Count; i++)
            {
                vehicleModel.BoltedJoint_SteeringColumn(oc_FL, oc_FR, i, vehicleLoadCase.SteeringColumnBearing);
                vehicleModel.AssignSteeringColumnForces(oc_FL, i);
            }
        }

        private void KinematicsInvoker_For_SetupChange()
        {
            KinematicsInvoker_Front(false, sc_FL.SpringDeflection_DeltSteering, sc_FR.SpringDeflection_DeltSteering, true);
            KinematicsInvoker_Rear(false, sc_RL.SpringDeflection_DeltSteering, sc_RR.SpringDeflection_DeltSteering, true);
        }
        #endregion

        #region Public Kinematics Invoker
        public void KinematicsInvoker(bool _MotionExists, SimulationType _simType)
        {

            if (_simType == SimulationType.MotionAnalysis || _simType == SimulationType.SteeringAnalysis) 
            {
                SolverMasterClass.SimType = SimulationType.Dummy;
                KinematicsInvoker_For_MotionORSteering(_MotionExists);
            }
            else if (/*!_MotionExists*/ _simType == SimulationType.StandToGround)
            {
                SolverMasterClass.SimType = SimulationType.Dummy;
                KinematicsInvoker_For_NoMotion();
            }

            else if (_simType == SimulationType.SetupChange)
            {
                SolverMasterClass.SimType = SimulationType.SetupChange;
                KinematicsInvoker_For_SetupChange();
            }
            else if (_simType == SimulationType.BatchRun)
            {
                if (_MotionExists)
                {
                    KinematicsInvoker_For_MotionORSteering(_MotionExists);

                }
                else
                {
                    KinematicsInvoker_For_NoMotion();
                }
            }

        }
        #endregion

        #endregion


        #region Setup Change Invoker
        /// <summary>
        /// Method to invoke the <see cref="SolverMasterClass.SetupChange_PrimaryInvoker(SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, Vehicle)"/> method
        /// </summary>
        /// <param name="_setupChange"></param>
        public void SetupChangeInvoker(SetupChange _setupChange)
        {
            ///<summary>Calling the <see cref="SolverMasterClass.SetupChange_PrimaryInvoker(SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, SetupChange_CornerVariables, Vehicle)"/> method</summary>
            MasterSolver.SetupChange_PrimaryInvoker(_setupChange.setupChange_CV_FL, _setupChange.setupChange_CV_FR, _setupChange.setupChange_CV_RL, _setupChange.setupChange_CV_RR, this);

            int iSetup = this.vehicleSetupChange.SetupChangeID - 1;

            SetupChange_GUI.List_SetupChangeGUI[iSetup].XUC_SetupChange.DisplayOutputs(MasterSolver.SetupChange_CLS_FL, _setupChange.setupChange_CV_FL, MasterSolver.SetupChange_CLS_FR, _setupChange.setupChange_CV_FR, MasterSolver.SetupChange_CLS_RL, _setupChange.setupChange_CV_RL,
                                                                                       MasterSolver.SetupChange_CLS_RR, _setupChange.setupChange_CV_RR);

        }
        #endregion


        #region Vehicle Level Outputs
        public void VehicleOutputs(int noOfSteps)
        {
            VehicleOutputsSolver.Solver_VehicleOutputs(this, oc_FL, oc_FR, oc_RL, oc_RR, noOfSteps);
        }
        #endregion


        #region De-Serialization of the Vehicle Object
        public Vehicle(SerializationInfo info, StreamingContext context)
        {
            _VehicleName = (string)info.GetValue("Vehicle_Name", typeof(string));

            VehicleID = (int)info.GetValue("Vehicle_ID", typeof(int));

            CurrentVehicleID = (int)info.GetValue("CurrentVehicle_ID", typeof(int));

            vehicleGUI = (VehicleGUI)info.GetValue("Vehicle_GUI", typeof(VehicleGUI));

            VehicleCounter = (int)info.GetValue("Vehicle_Counter", typeof(int));

            VehicleResultsCounter = (int)info.GetValue("Vehicle_Results_Counter", typeof(int));

            Vehicle_Results_Tracker = (int)info.GetValue("Vehicle_Results_Tracker", typeof(int));

            Vehicle_MotionExists = (bool)info.GetValue("VehicleMotionExists", typeof(bool));

            AssemblyChecker = (int)info.GetValue("AssemblyChecker", typeof(int));

            SuspensionIsAssembled = (bool)info.GetValue("SuspensionIsAssembled", typeof(bool));

            TireIsAssembled = (bool)info.GetValue("TireIsAssembled", typeof(bool));

            SpringIsAssembled = (bool)info.GetValue("SpringIsAssembled", typeof(bool));

            DamperIsAssembled = (bool)info.GetValue("DamperIsAssembled", typeof(bool));

            ARBIsAssembled = (bool)info.GetValue("ARBIsAssembled", typeof(bool));

            ChassisIsAssembled = (bool)info.GetValue("ChassisIsAssembled", typeof(bool));

            WAIsAssembled = (bool)info.GetValue("WAIsAssembled", typeof(bool));

            VehicleHasBeenValidated = (bool)info.GetValue("VehicleHasBeenValidated", typeof(bool));

            #region De-serialization of the Identifier 
            Identifier_FL = (int)info.GetValue("Identifier_FL", typeof(int));
            Identifier_FR = (int)info.GetValue("Identifier_FR", typeof(int));
            Identifier_RL = (int)info.GetValue("Identifier_RL", typeof(int));
            Identifier_RR = (int)info.GetValue("Identifier_RR", typeof(int));
            #endregion

            #region De-Serialization of Suspension Coordinate Object
            sc_FL = (/*SuspensionCoordinatesMaster*/SuspensionCoordinatesFront)info.GetValue("SCFL_Object", typeof(/*SuspensionCoordinatesMaster*/SuspensionCoordinatesFront));
            sc_FR = (/*SuspensionCoordinatesMaster*/SuspensionCoordinatesFrontRight)info.GetValue("SCFR_Object", typeof(/*SuspensionCoordinatesMaster*/SuspensionCoordinatesFrontRight));
            sc_RL = (/*SuspensionCoordinatesMaster*/SuspensionCoordinatesRear)info.GetValue("SCRL_Object", typeof(/*SuspensionCoordinatesMaster*/SuspensionCoordinatesRear));
            sc_RR = (/*SuspensionCoordinatesMaster*/SuspensionCoordinatesRearRight)info.GetValue("SCRR_Object", typeof(/*SuspensionCoordinatesMaster*/SuspensionCoordinatesRearRight));
            #endregion

            #region De-Serialization of the Tire Object
            tire_FL = (Tire)info.GetValue("TireFL_Object", typeof(Tire));
            tire_FR = (Tire)info.GetValue("TireFR_Object", typeof(Tire));
            tire_RL = (Tire)info.GetValue("TireRL_Object", typeof(Tire));
            tire_RR = (Tire)info.GetValue("TireRR_Object", typeof(Tire));
            #endregion

            #region De-Serialization of the Sprig Object
            spring_FL = (Spring)info.GetValue("SpringFL_Object", typeof(Spring));
            spring_FR = (Spring)info.GetValue("SpringFR_Object", typeof(Spring));
            spring_RL = (Spring)info.GetValue("SpringRL_Object", typeof(Spring));
            spring_RR = (Spring)info.GetValue("SpringRR_Object", typeof(Spring));
            #endregion

            #region De-Serialization of the Damper Object
            damper_FL = (Damper)info.GetValue("DamperFL_Object", typeof(Damper));
            damper_FR = (Damper)info.GetValue("DamperFR_Object", typeof(Damper));
            damper_RL = (Damper)info.GetValue("DamperRL_Object", typeof(Damper));
            damper_RR = (Damper)info.GetValue("DamperRR_Object", typeof(Damper));
            #endregion

            #region De-Serialization of the ARB Object
            arb_FL = (AntiRollBar)info.GetValue("ARB_FL_Object", typeof(AntiRollBar));
            arb_FR = (AntiRollBar)info.GetValue("ARB_FR_Object", typeof(AntiRollBar));
            arb_RL = (AntiRollBar)info.GetValue("ARB_RL_Object", typeof(AntiRollBar));
            arb_RR = (AntiRollBar)info.GetValue("ARB_RR_Object", typeof(AntiRollBar));
            #endregion

            chassis_vehicle = (Chassis)info.GetValue("Chassis_Object", typeof(Chassis));

            #region De-Serialization of the Wheel Alignment Object
            wa_FL = (WheelAlignment)info.GetValue("WA_FL_Object", typeof(WheelAlignment));
            wa_FR = (WheelAlignment)info.GetValue("WA_FR_Object", typeof(WheelAlignment));
            wa_RL = (WheelAlignment)info.GetValue("WA_RL_Object", typeof(WheelAlignment));
            wa_RR = (WheelAlignment)info.GetValue("WA_RR_Object", typeof(WheelAlignment));
            #endregion

            vehicle_Motion = (Motion)info.GetValue("vehicle_Motion", typeof(Motion));

            #region De-Serialization of the OutputClass Object
            oc_FL = (List<OutputClass>)info.GetValue("OC_FL_Object", typeof(List<OutputClass>));
            oc_FR = (List<OutputClass>)info.GetValue("OC_FR_Object", typeof(List<OutputClass>));
            oc_RL = (List<OutputClass>)info.GetValue("OC_RL_Object", typeof(List<OutputClass>));
            oc_RR = (List<OutputClass>)info.GetValue("OC_RR_Object", typeof(List<OutputClass>));
            #endregion

            CW = (double[])info.GetValue("CW_Array", typeof(double[]));

            #region De-Serialization of the Geometry Type
            DoubleWishboneFront = (int)info.GetValue("DoubleWishboneFront_Identifier", typeof(int));
            DoubleWishboneRear = (int)info.GetValue("DoubleWishboneRear_Identifer", typeof(int));
            McPhersonFront = (int)info.GetValue("McPhersonFront_Identifer", typeof(int));
            McPhersonRear = (int)info.GetValue("McPhersonRear_Identifier", typeof(int));
            #endregion

            #region De-Serialization of the Actuation Type
            PushRodIdentifierFront = (int)info.GetValue("PushrodFront_Identifier", typeof(int));
            PullRodIdentifierFront = (int)info.GetValue("PullrodFrontIdentifer", typeof(int));
            PushRodIdentifierRear = (int)info.GetValue("PushrodRearIdentifier", typeof(int));
            PullRodIdentifierRear = (int)info.GetValue("PullrodRearIdentifier", typeof(int));
            #endregion

            #region De-Serialization of the ARB Type
            UARBIdentifierFront = (int)info.GetValue("UARBFront_Identifier", typeof(int));
            TARBIdentifierFront = (int)info.GetValue("TARBFront_Identifier", typeof(int));
            UARBIdentifierRear = (int)info.GetValue("UARBRear_Identifier", typeof(int));
            TARBIdentifierRear = (int)info.GetValue("TARBRear_Identifier", typeof(int));
            #endregion

            #region De-Serialization of the Vehicle Origin Coordinates
            //InputOrigin_x = (double)info.GetValue("InputOrigin_x", typeof(double));
            //InputOrigin_y = (double)info.GetValue("InputOrigin_y", typeof(double));
            //InputOrigin_z = (double)info.GetValue("InputOrigin_z", typeof(double));
            OutputOrigin_x = (double)info.GetValue("OutputOrigin_x", typeof(double));
            OutputOrigin_y = (double)info.GetValue("OutputOrigin_y", typeof(double));
            OutputOrigin_z = (double)info.GetValue("OutputOrigin_z", typeof(double));
            #endregion

            #region De-Serilization of the Vehicle Measurement
            WheelBase = (double)info.GetValue("Wheelbase", typeof(double));
            TrackF = (double)info.GetValue("Track_Front", typeof(double));
            TrackR = (double)info.GetValue("Track_Rear", typeof(double));
            TrackAvg = (double)info.GetValue("Track_Avg", typeof(double));
            New_WheelBase = (double[])info.GetValue("New_Wheelbase", typeof(double[]));
            New_TrackF = (double[])info.GetValue("New_Track_Front", typeof(double[]));
            New_TrackR = (double[])info.GetValue("New_Track_Rear", typeof(double[]));
            #endregion

            #region De-Serilization of the Masses
            SM_Vehicle = (double)info.GetValue("SM_Vehicle", typeof(double));

            SM_Front = (double)info.GetValue("SM_Front", typeof(double));
            SM_Rear = (double)info.GetValue("SM_Rear", typeof(double));

            SM_FL = (double)info.GetValue("SM_FL", typeof(double));
            NSM_FL = (double)info.GetValue("NSM_FL", typeof(double));

            SM_FR = (double)info.GetValue("SM_FR", typeof(double));
            NSM_FR = (double)info.GetValue("NSM_FR", typeof(double));

            SM_RL = (double)info.GetValue("SM_RL", typeof(double));
            NSM_RL = (double)info.GetValue("NSM_RL", typeof(double));

            SM_RR = (double)info.GetValue("SM_RR", typeof(double));
            NSM_RR = (double)info.GetValue("NSM_RR", typeof(double));

            #endregion

            #region De-Serilization of the Suspended Mass CoG
            New_SMCoGx = (double[])info.GetValue("New_SM_CoG_x", typeof(double[]));
            New_SMCoGy = (double[])info.GetValue("New_SM_CoG_y", typeof(double[]));
            New_SMCoGz = (double[])info.GetValue("New_SM_CoG_z", typeof(double[]));
            Vehicle_CG_y = (double)info.GetValue("Vehicle_CoG_y", typeof(double));
            //FinalRideHeight_ForTrans_VehicleCGy = (double)info.GetValue("FinalRideHeight_For_VehicleCoGy_Translation", typeof(double));
            #endregion

            #region De-Serilization of the Roll and Pitch Angle
            //RollAngle_Front = (double)info.GetValue("RollAngle_Front", typeof(double));
            //RollAngle_Rear = (double)info.GetValue("RollAngle_Rear", typeof(double));
            //RollAngle_Chassis = (double)info.GetValue("RollAngle_Chassis", typeof(double));
            RollAngle = (double[])info.GetValue("RollAngle", typeof(double[]));
            //PitchAngle_Left = (double)info.GetValue("PitchAngle_Left", typeof(double));
            //PitchAngle_Right = (double)info.GetValue("PitchAngle_Right", typeof(double));
            //PitchAngle_Chassis = (double)info.GetValue("PitchAngle_Chassis", typeof(double));
            PitchAngle = (double[])info.GetValue("PitchAngle", typeof(double[]));
            #endregion

            #region De-Serilization of the variables for ARB calculations
            QP_FL = (double)info.GetValue("QP_FL", typeof(double));
            QP_FR = (double)info.GetValue("QP_FR", typeof(double));
            QP_RL = (double)info.GetValue("QP_RL", typeof(double));
            QP_RR = (double)info.GetValue("QP_RR", typeof(double));

            P1x_FL = (double)info.GetValue("P1x_FL", typeof(double));
            P1x_FR = (double)info.GetValue("P1x_FR", typeof(double));
            P1x_RL = (double)info.GetValue("P1x_RL", typeof(double));
            P1x_RR = (double)info.GetValue("P1x_RR", typeof(double));

            P1z_FL = (double)info.GetValue("P1z_FL", typeof(double));
            P1z_FR = (double)info.GetValue("P1z_FR", typeof(double));
            P1z_RL = (double)info.GetValue("P1z_RL", typeof(double));
            P1z_RR = (double)info.GetValue("P1z_RR", typeof(double));

            P_FLFR1 = (double)info.GetValue("P_FLFR1", typeof(double));
            P_FLFR2 = (double)info.GetValue("P_FLFR2", typeof(double));
            P_RLRR1 = (double)info.GetValue("P_RLRR1", typeof(double));
            P_RLRR2 = (double)info.GetValue("P_RLRR2", typeof(double));

            ARB_Twist_Front = (double)info.GetValue("ARB_Twist_Front", typeof(double));
            ARB_Twist_Rear = (double)info.GetValue("ARB_Twist_Rear", typeof(double));

            ARB_MR_Front = (double[])info.GetValue("ARB_MR_Front", typeof(double[]));
            ARB_MR_Rear = (double[])info.GetValue("ARB_MR_Rear", typeof(double[]));

            ARB_Rate_Nmm_FL = (double)info.GetValue("ARB_Rate_Nmm_FL", typeof(double));
            ARB_Rate_Nmm_FR = (double)info.GetValue("ARB_Rate_Nmm_FR", typeof(double));
            ARB_Rate_Nmm_RL = (double)info.GetValue("ARB_Rate_Nmm_RL", typeof(double));
            ARB_Rate_Nmm_RR = (double)info.GetValue("ARB_Rate_Nmm_RR", typeof(double));
            #endregion

            #region De-Serialization of the Vehicle's Load Case
            vehicleLoadCase = (LoadCase)info.GetValue("vehicleLoadCase", typeof(LoadCase));
            #endregion

        }
        #endregion


        #region Serialization of the Vehicle Object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Vehicle_Name", _VehicleName);

            info.AddValue("Vehicle_ID", VehicleID);

            info.AddValue("CurrentVehicle_ID", CurrentVehicleID);

            info.AddValue("Vehicle_GUI", vehicleGUI);

            info.AddValue("Vehicle_Counter", VehicleCounter);

            info.AddValue("Vehicle_Results_Counter", VehicleResultsCounter);

            info.AddValue("Vehicle_Results_Tracker", Vehicle_Results_Tracker);

            info.AddValue("VehicleMotionExists", Vehicle_MotionExists);

            info.AddValue("AssemblyChecker", AssemblyChecker);

            info.AddValue("SuspensionIsAssembled", SuspensionIsAssembled);

            info.AddValue("TireIsAssembled", TireIsAssembled);

            info.AddValue("SpringIsAssembled", SpringIsAssembled);

            info.AddValue("DamperIsAssembled", DamperIsAssembled);

            info.AddValue("ARBIsAssembled", ARBIsAssembled);

            info.AddValue("ChassisIsAssembled", ChassisIsAssembled);

            info.AddValue("WAIsAssembled", WAIsAssembled);

            info.AddValue("VehicleHasBeenValidated", VehicleHasBeenValidated);

            #region Serialization of the Identifier
            info.AddValue("Identifier_FL", Identifier_FL);
            info.AddValue("Identifier_FR", Identifier_FR);
            info.AddValue("Identifier_RL", Identifier_RL);
            info.AddValue("Identifier_RR", Identifier_RR);
            #endregion

            #region Serialization of Suspension Coordinate Object
            info.AddValue("SCFL_Object", sc_FL);
            info.AddValue("SCFR_Object", sc_FR);
            info.AddValue("SCRL_Object", sc_RL);
            info.AddValue("SCRR_Object", sc_RR);
            #endregion

            #region Serialization of Tire Object
            info.AddValue("TireFL_Object", tire_FL);
            info.AddValue("TireFR_Object", tire_FR);
            info.AddValue("TireRL_Object", tire_RL);
            info.AddValue("TireRR_Object", tire_RR);
            #endregion

            #region Serialization of the Sprig Object
            info.AddValue("SpringFL_Object", spring_FL);
            info.AddValue("SpringFR_Object", spring_FR);
            info.AddValue("SpringRL_Object", spring_RL);
            info.AddValue("SpringRR_Object", spring_RR);
            #endregion

            #region Serialization of the Damper Object
            info.AddValue("DamperFL_Object", damper_FL);
            info.AddValue("DamperFR_Object", damper_FR);
            info.AddValue("DamperRL_Object", damper_RL);
            info.AddValue("DamperRR_Object", damper_RR);
            #endregion

            #region Serialization of the ARB Object
            info.AddValue("ARB_FL_Object", arb_FL);
            info.AddValue("ARB_FR_Object", arb_FR);
            info.AddValue("ARB_RL_Object", arb_RL);
            info.AddValue("ARB_RR_Object", arb_RR);
            #endregion

            info.AddValue("Chassis_Object", chassis_vehicle);

            #region Serialization of the Wheel Alignment Object
            info.AddValue("WA_FL_Object", wa_FL);
            info.AddValue("WA_FR_Object", wa_FR);
            info.AddValue("WA_RL_Object", wa_RL);
            info.AddValue("WA_RR_Object", wa_RR);
            #endregion

            info.AddValue("vehicle_Motion", vehicle_Motion);

            #region Serialization of the OutputClass Object
            info.AddValue("OC_FL_Object", oc_FL);
            info.AddValue("OC_FR_Object", oc_FR);
            info.AddValue("OC_RL_Object", oc_RL);
            info.AddValue("OC_RR_Object", oc_RR);
            #endregion

            info.AddValue("CW_Array", CW);

            #region Serialization of the Geometry Type
            info.AddValue("DoubleWishboneFront_Identifier", DoubleWishboneFront);
            info.AddValue("DoubleWishboneRear_Identifer", DoubleWishboneRear);
            info.AddValue("McPhersonFront_Identifer", McPhersonFront);
            info.AddValue("McPhersonRear_Identifier", McPhersonRear);
            #endregion

            #region Serialization of the Actuation Type
            info.AddValue("PushrodFront_Identifier", PushRodIdentifierFront);
            info.AddValue("PullrodFrontIdentifer", PullRodIdentifierFront);
            info.AddValue("PushrodRearIdentifier", PushRodIdentifierRear);
            info.AddValue("PullrodRearIdentifier", PullRodIdentifierRear);
            #endregion

            #region Serialization of the ARB Type
            info.AddValue("UARBFront_Identifier", UARBIdentifierFront);
            info.AddValue("TARBFront_Identifier", TARBIdentifierFront);
            info.AddValue("UARBRear_Identifier", UARBIdentifierRear);
            info.AddValue("TARBRear_Identifier", TARBIdentifierRear);
            #endregion

            #region Serialization of the Vehicle Origin Coordinates
            //info.AddValue("InputOrigin_x", InputOrigin_x);
            //info.AddValue("InputOrigin_y", InputOrigin_y);
            //info.AddValue("InputOrigin_z", InputOrigin_z);
            info.AddValue("OutputOrigin_x", OutputOrigin_x);
            info.AddValue("OutputOrigin_y", OutputOrigin_y);
            info.AddValue("OutputOrigin_z", OutputOrigin_z);
            #endregion

            #region Serilization of the Vehicle Measurement
            info.AddValue("Wheelbase", WheelBase);
            info.AddValue("Track_Front", TrackF);
            info.AddValue("Track_Rear", TrackR);
            info.AddValue("Track_Avg", TrackAvg);
            info.AddValue("New_Wheelbase", New_WheelBase);
            info.AddValue("New_Track_Front", New_TrackF);
            info.AddValue("New_Track_Rear", New_TrackR);
            #endregion

            #region Serilization of the Masses
            info.AddValue("SM_Vehicle", SM_Vehicle);

            info.AddValue("SM_Front", SM_Front);
            info.AddValue("SM_Rear", SM_Rear);

            info.AddValue("SM_FL", SM_FL);
            info.AddValue("NSM_FL", NSM_FL);

            info.AddValue("SM_FR", SM_FR);
            info.AddValue("NSM_FR", NSM_FR);

            info.AddValue("SM_RL", SM_RL);
            info.AddValue("NSM_RL", NSM_RL);

            info.AddValue("SM_RR", SM_RR);
            info.AddValue("NSM_RR", NSM_RR);

            #endregion

            #region Serilization of the Suspended Mass CoG
            info.AddValue("New_SM_CoG_x", New_SMCoGx);
            info.AddValue("New_SM_CoG_y", New_SMCoGy);
            info.AddValue("New_SM_CoG_z", New_SMCoGz);
            info.AddValue("Vehicle_CoG_y", Vehicle_CG_y);
            info.AddValue("FinalRideHeight_For_VehicleCoGy_Translation", FinalRideHeight_ForTrans_VehicleCGy);
            #endregion

            #region Serilization of the Roll and Pitch Angle
            //info.AddValue("RollAngle_Front", RollAngle_Front);
            //info.AddValue("RollAngle_Rear", RollAngle_Rear);
            //info.AddValue("RollAngle_Chassis", RollAngle_Chassis);
            info.AddValue("RollAngle", RollAngle);
            //info.AddValue("PitchAngle_Left", PitchAngle_Left);
            //info.AddValue("PitchAngle_Right", PitchAngle_Right);
            //info.AddValue("PitchAngle_Chassis", PitchAngle_Chassis);
            info.AddValue("PitchAngle", PitchAngle);
            #endregion

            #region Serilization of the variables for ARB calculations
            info.AddValue("QP_FL", QP_FL);
            info.AddValue("QP_FR", QP_FR);
            info.AddValue("QP_RL", QP_RL);
            info.AddValue("QP_RR", QP_RR);

            info.AddValue("P1x_FL", P1x_FL);
            info.AddValue("P1x_FR", P1x_FR);
            info.AddValue("P1x_RL", P1x_RL);
            info.AddValue("P1x_RR", P1x_RR);

            info.AddValue("P1z_FL", P1z_FL);
            info.AddValue("P1z_FR", P1z_FR);
            info.AddValue("P1z_RL", P1z_RL);
            info.AddValue("P1z_RR", P1z_RR);

            info.AddValue("P_FLFR1", P_FLFR1);
            info.AddValue("P_FLFR2", P_FLFR2);
            info.AddValue("P_RLRR1", P_RLRR1);
            info.AddValue("P_RLRR2", P_RLRR2);

            info.AddValue("ARB_Twist_Front", ARB_Twist_Front);
            info.AddValue("ARB_Twist_Rear", ARB_Twist_Rear);

            info.AddValue("ARB_MR_Front", ARB_MR_Front);
            info.AddValue("ARB_MR_Rear", ARB_MR_Rear);

            info.AddValue("ARB_Rate_Nmm_FL", ARB_Rate_Nmm_FL);
            info.AddValue("ARB_Rate_Nmm_FR", ARB_Rate_Nmm_FR);
            info.AddValue("ARB_Rate_Nmm_RL", ARB_Rate_Nmm_RL);
            info.AddValue("ARB_Rate_Nmm_RR", ARB_Rate_Nmm_RR);
            #endregion

            #region Serialization of the Vehicle's Load Case
             info.AddValue("vehicleLoadCase", vehicleLoadCase); 
            #endregion

        }
        #endregion


    }

    /// <summary>
    /// Enum to determine which Corner of the Vehicle is calling this class
    /// </summary>
    public enum VehicleCorner
    {
        FrontLeft,
        FrontRight,
        RearLeft,
        RearRight
    }

    public enum SimulationType
    {
        MotionAnalysis = 1,
        SteeringAnalysis = 2,
        StandToGround = 3,
        SetupChange = 4,
        BatchRun = 5,
        Optimization = 6,
        Dummy = 7
    };

}
 
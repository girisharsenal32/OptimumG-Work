using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data.Access;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab.Buttons;
using DevExpress.XtraSplashScreen;
using devDept.Geometry;


namespace Coding_Attempt_with_GUI
{

    public partial class Kinematics_Software_New : RibbonForm
    {
        #region Initialization of the Form and ObjectInitializer Class's Object
        public static Kinematics_Software_New R1;
        public static ObjectInitializer M1_Global;
        public KinematicsSoftwareNewSerialization K1;
        public static CustomXtraTabControl TabControl_Outputs;
        public List<int> navBarResultVehicleIndex = new List<int>();
        public bool MotionExists;
        public int OutputIndex = 0;
        public bool IsBeingOpened = false;
        public bool CurrentSuspensionIsMapped = false;
        #endregion

        #region Constructor
        public Kinematics_Software_New()
        {
            InitializeComponent();
            R1 = this;
            this.DoubleBuffered = true;

            #region GUI tasks - Hiding
            accordionControlVehicleItem.Hide();
            sidePanel2.Hide();
            #endregion

            M1_Global = new ObjectInitializer();

            #region Creating the Progress Bar and Label and initializaing it
            progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 800, 1);
            #endregion

            #region Assigning an Event to the UndoRedo Class' EnableDisableUndoRedoFeature to determine whether the Undo button shuld be enabled or diasbled
            UndoRedo.EnableDisableUndoRedoFeature += new EventHandler(UndoObject_EnableDisableUndoRedoFeature);
            #endregion

            InitializeTabControl();

            TabControl_Outputs.Visible = false;

            GC.Visible = false;
            GCArrow.Visible = false;
            GCBar.Visible = false;

            navBarControlResults.ActiveGroupChanged += new NavBarGroupEventHandler(navBarControlResults_ActiveGroupChanged);
            navBarControlResults.LookAndFeel.UseDefaultLookAndFeel = false;

            defaultLookAndFeel1 = new DefaultLookAndFeel();
            SkinName = "VS2010";

            gridControl2.BringToFront();

            MotionExists = true;
            InputOriginY.Text = Convert.ToString(60);
        }



        #endregion

        private void radioButtonOnStands_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSetupMode.Checked == true)
            {
                MotionExists = false;
                InputOriginY.Text = Convert.ToString(1033);
                Motion.List_Motion.Insert(Motion.MotionCounter, new Motion());
                ribbonPageGroupSetupChange.Visible = true;
                ChangeTracker++;
                //ribbonPageGroupRecalculate.Visible = true;
            }
        }

        private void radioButtonOnGround_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSimulationMode.Checked == true)
            {
                MotionExists = true;
                InputOriginY.Text = Convert.ToString(60);
                ribbonPageGroupSetupChange.Visible = false;
                ChangeTracker++;
                //ribbonPageGroupRecalculate.Visible = false;
            }
        }

        #region Form Load Event
        private void Kinematics_Software_New_Load(object sender, EventArgs e)
        {

            DoubleWishboneFront_VehicleGUI = 1;
            DoubleWishboneRear_VehicleGUI = 1;

            CreateALLDefaultLoadCases();

            ribbonPageGroupHeatMap.Enabled = false;

            TabControl_Outputs.Visible = true;

            this.Show();
        }
        #endregion

        #region Method to determine wheterh the Undo/Redo buttons should be enabled or diabled
        public void UndoObject_EnableDisableUndoRedoFeature(object sender, EventArgs e)
        {
            if (UndoObject.IsUndoPossible())
            {
                barButtonUndo.Enabled = true;
            }
            else
            {
                barButtonUndo.Enabled = false;

            }

            if (UndoObject.IsRedoPossible())
            {
                barButtonRedo.Enabled = true;
            }
            else
            {
                barButtonRedo.Enabled = false;
            }
        }
        #endregion

        #region Miscallaneous Declarations

        public ProgressBarSerialization progressBar;

        public int CalculateResultsButtonClickCounter = 1;

        public static int InputItemID_For_Undo;

        public UndoRedo UndoObject = new UndoRedo();

        public int ChangeTracker = 0;

        #endregion

        #region Static Objects of the Input Item Classes and their corresponding navBarItemClasses

        #region Static Object of Tire (Implemented for Undo/Redo functionality)
        public static Tire T1_Global = new Tire();
        public static navBarItemTireClass navBarTire_Global = new navBarItemTireClass();
        #endregion

        #region Static Object of Spring (Implemented for Undo/Redo functionality)
        public static Spring S1_Global = new Spring();
        public static navBarItemSpringClass navBarSpring_Global = new navBarItemSpringClass();
        #endregion

        #region Static Object of Damper (Implemented for Undo/Redo functionality)
        public static Damper D1_Global = new Damper();
        public static navbarItemDamperClass navBarDamper_Global = new navbarItemDamperClass();
        #endregion

        #region Static Object of AntiRollBar (Implemented for Undo/Redo functionality)
        public static AntiRollBar A1_Global = new AntiRollBar();
        public static navBarItemARBClass navBarARB_Global = new navBarItemARBClass();
        #endregion

        #region Static Object of Chassis (Implemented for Undo/Redo functionality)
        public static Chassis C1_Global = new Chassis();
        public static navBarItemChassisClass navBarChassis_Global = new navBarItemChassisClass();
        #endregion

        #region Static Object of Wheel Alignment (Implemented for Undo/Redo functionality)
        public static WheelAlignment W1_Global = new WheelAlignment();
        public static navBarItemWAClass navBarWA_Global = new navBarItemWAClass();
        #endregion

        #region Static Object of SCFL (Implemented for Undo/Redo functionality)
        public static SuspensionCoordinatesFront SCFL1_Global = new SuspensionCoordinatesFront();
        public static navBarItemSCFLClass navBarSCFL_Global = new navBarItemSCFLClass();
        #endregion

        #region Static Object of SCFR (Implemented for Undo/Redo functionality)
        public static SuspensionCoordinatesFrontRight SCFR1_Global = new SuspensionCoordinatesFrontRight();
        public static navBarItemSCFRClass navBarSCFR_Global = new navBarItemSCFRClass();
        #endregion

        #region Static Object of SCRL (Implemented for Undo/Redo functionality)
        public static SuspensionCoordinatesRear SCRL1_Global = new SuspensionCoordinatesRear();
        public static navBarItemSCRLClass navBarSCRL_Global = new navBarItemSCRLClass();
        #endregion

        #region Static Object of SCRR (Implemented for Undo/Redo functionality)
        public static SuspensionCoordinatesRearRight SCRR1_Global = new SuspensionCoordinatesRearRight();
        public static navBarItemSCRRClass navBarSCRR_Global = new navBarItemSCRRClass();
        #endregion

        #region Static Object of Vehicle (Implemented for Undo/Redo functionality)
        public static Vehicle V1_Global = new Vehicle();
        public static navBarItemVehicleClass navBarVehicle_Global = new navBarItemVehicleClass();
        #endregion 

        #endregion

        #region Object Initializer Class
        public class ObjectInitializer
        {

            public int Assy_IdentifierFL, Assy_IdentifierFR, Assy_IdentifierRL, Assy_IdentifierRR;

            #region Declaration of the Global Array and Globl List of Suspension Coordinate Objects
            public SuspensionCoordinatesMaster[] Assy_SCM;
            #endregion

            public List<InputSheet> List_I1;

            public OutputClass[] Assy_OC;

            #region Declaration of the Vehicle GUI Object
            public List<VehicleGUI> vehicleGUI; // Declard Here so that it can be 
            #endregion

            public ObjectInitializer()
            {

                #region Initialization of the Global Identifier for Assembly
                Assy_IdentifierFL = 1;
                Assy_IdentifierFR = 2;
                Assy_IdentifierRL = 3;
                Assy_IdentifierRR = 4;
                #endregion

                #region Initialization of the Global Array ad Global Array of Suspension Coordinates Object
                Assy_SCM = new SuspensionCoordinatesMaster[4];
                #endregion

                List_I1 = new List<InputSheet>();

                #region Output Class
                #region New Output Instance -  - Front Left, Front Right, Rear Left, Rear Right
                OutputClass ocfl = new OutputClass();
                OutputClass ocfr = new OutputClass();
                OutputClass ocrl = new OutputClass();
                OutputClass ocrr = new OutputClass();
                #endregion

                #region Initialization of the Global Array of OutputClass Object
                Assy_OC = new OutputClass[4];
                Assy_OC[0] = ocfl;
                Assy_OC[1] = ocfr;
                Assy_OC[2] = ocrl;
                Assy_OC[3] = ocrr;
                #endregion
                #endregion

                #region Initialization of the VehicleGUI Object
                vehicleGUI = new List<VehicleGUI>();
                #endregion

            }
        }
        #endregion

        #region GUI Code to copy the Left coordinates to the Right and vice versa

        #region Suspension Coordinate Symmetry Identifier
        public bool FrontSymmetry = true;
        public bool RearSymmetry = true;
        #endregion


        public void CopyFrontLeftTOFrontRight()
        {
            int index = navBarGroupSuspensionFL.SelectedLinkIndex;
            int indexRowLeft = 0, indexRowRight = 0;
            #region Copying Coordinates for Double Wishbone
            if (SuspensionCoordinatesFront.Assy_List_SCFL[index].DoubleWishboneIdentifierFront == 1)
            {
                #region Copying coordinates for Double Wishbone

                #region Copying the Longitudinal Coordinates

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                }
                #endregion

                #region Copying the Lateral Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                }
                #endregion

                #region Copying the Vertical Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                }
                #endregion

                #endregion
            }
            #endregion

            #region Copying Coordinates for McPherson
            else if (SuspensionCoordinatesFront.Assy_List_SCFL[index].McPhersonIdentifierFront == 1)
            {
                #region Copying coordinates for McPherson

                #region Copying Longitudinal Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(1));
                #endregion

                #region Copying Lateral Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(2));
                #endregion

                #region Copying Vertical Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].Field<double>(3));
                #endregion

                #endregion
            }
            #endregion

            scfrGUI[index].SCFRDataTableGUI = SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable;

            ModifyFrontRightSuspension(true, index);


        }

        public void CopyRearRightTORearLeft()
        {
            int index = navBarGroupSuspensionRR.SelectedLinkIndex;

            #region Copying Coordinates for Double Wishbone
            if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].DoubleWishboneIdentifierRear == 1)
            {
                #region Copying coordinates for Double Wishbone

                #region Copying the Longitudinal Coordinates

                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[12].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[12].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[13].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[13].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[14].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[14].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[15].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[15].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[16].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[16].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[17].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[17].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[18].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[18].Field<double>(1));
                if (SuspensionCoordinatesRear.Assy_List_SCRL[index].TARBIdentifierRear == 1)
                {
                    SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[19].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[19].Field<double>(1));
                }
                #endregion

                #region Copying the Lateral Coordinates

                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[12].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[12].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[13].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[13].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[14].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[14].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[15].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[15].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[16].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[16].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[17].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[17].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[18].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[18].Field<double>(2));
                if (SuspensionCoordinatesRear.Assy_List_SCRL[index].TARBIdentifierRear == 1)
                {
                    SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[19].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[19].Field<double>(2));
                }
                #endregion

                #region Copying the Vertical Coordinates

                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[12].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[12].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[13].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[13].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[14].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[14].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[15].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[15].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[16].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[16].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[17].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[17].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[18].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[18].Field<double>(3));
                if (SuspensionCoordinatesRear.Assy_List_SCRL[index].TARBIdentifierRear == 1)
                {
                    SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[19].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[19].Field<double>(3));
                }
                #endregion

                #endregion
            }
            #endregion

            #region Copying Coordinates for McPherson
            else if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].McPhersonIdentifierRear == 1)
            {
                #region Copying coordinates for McPherson

                #region Copying Longitudinal Coordinates
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].Field<double>(1));
                #endregion

                #region Copying Lateral Coordinates
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].Field<double>(2));
                #endregion

                #region Copying Vertical Coordinates
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].Field<double>(3));
                #endregion

                #endregion
            }
            #endregion

            scrlGUI[index].SCRLDataTableGUI = SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable;

            ModifyRearLeftSuspension(true, index);


        }

        public void CopyRearLeftTOReaRight()
        {
            int index = navBarGroupSuspensionRL.SelectedLinkIndex;

            #region Copying Coordinates for Double Wishbone
            if (SuspensionCoordinatesRear.Assy_List_SCRL[index].DoubleWishboneIdentifierRear == 1)
            {
                #region Copying coordinates for Double Wishbone

                #region Copying the Longitudinal Coordinates

                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[12].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[12].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[13].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[13].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[14].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[14].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[15].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[15].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[16].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[16].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[17].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[17].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[18].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[18].Field<double>(1));
                if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].TARBIdentifierRear == 1)
                {
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[19].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[19].Field<double>(1));
                }
                #endregion

                #region Copying the Lateral Coordinates

                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[12].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[12].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[13].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[13].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[14].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[14].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[15].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[15].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[16].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[16].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[17].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[17].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[18].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[18].Field<double>(2));
                if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].TARBIdentifierRear == 1)
                {
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[19].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[19].Field<double>(2));
                }
                #endregion

                #region Copying the Vertical Coordinates

                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[12].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[12].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[13].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[13].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[14].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[14].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[15].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[15].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[16].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[16].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[17].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[17].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[18].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[18].Field<double>(3));
                if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].TARBIdentifierRear == 1)
                {
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[19].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[19].Field<double>(3));
                }
                #endregion

                #endregion
            }
            #endregion

            #region Copying Coordinates for McPherson
            else if (SuspensionCoordinatesRear.Assy_List_SCRL[index].McPhersonIdentifierRear == 1)
            {
                #region Copying coordinates for McPherson

                #region Copying Longitudinal Coordinates
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].Field<double>(1));
                #endregion

                #region Copying Lateral Coordinates
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].Field<double>(2));
                #endregion

                #region Copying Vertical Coordinates
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLDataTable.Rows[11].Field<double>(3));
                #endregion

                #endregion
            }
            #endregion

            scrrGUI[index].SCRRDataTableGUI = SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRDataTable;

            ModifyRearRightSuspension(true, index);
        }


        public void CopyFrontRightTOFrontLeft()
        {
            int index = navBarGroupSuspensionFR.SelectedLinkIndex;
            int indexRowLeft = 0, indexRowRight = 0;
            #region Copying Coordinates for Double Wishbone
            if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].DoubleWishboneIdentifierFront == 1)
            {
                #region Copying coordinates for Double Wishbone

                #region Copying the Longitudinal Coordinates

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                }
                #endregion

                #region Copying the Lateral Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                }
                #endregion

                #region Copying the Vertical Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                }
                #endregion

                #endregion
            }
            #endregion

            #region Copying Coordinates for McPherson
            else if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].McPhersonIdentifierFront == 1)
            {
                #region Copying coordinates for McPherson

                #region Copying Longitudinal Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(1));
                #endregion

                #region Copying Lateral Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(2));
                #endregion

                #region Copying Vertical Coordinates
                indexRowLeft = 0; indexRowRight = 0;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].NoOfCouplings == 2)
                {
                    indexRowLeft += 5;
                }
                else indexRowLeft += 4;
                indexRowRight++;

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                indexRowLeft++; indexRowRight++;
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[indexRowLeft].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[indexRowRight].Field<double>(3));
                #endregion

                #endregion
            }
            #endregion

            scflGUI[index].SCFLDataTableGUI = SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable;

            ModifyFrontLeftSuspension(true, index);

        }

        #endregion

        #region Suspension Types Formn Invoking
        private void barButtonSuspensionTypes_ItemClick(object sender, ItemClickEventArgs e)
        {
            SuspensionType S1 = new SuspensionType(this);
            S1.LookAndFeel.SkinName = SkinName;
            defaultLookAndFeel1.LookAndFeel.SkinName = SkinName;
            S1.Reset();
            S1.Show();
            ChangeTracker++;
        }
        #endregion

        #region GUI operations to change the values of Labels, Textboxes when McPherson, TARB or Pullrod is selected.

        #region Type of Geometry

        public int DoubleWishboneFront_VehicleGUI, DoubleWishboneRear_VehicleGUI, McPhersonFront_VehicleGUI, McPhersonRear_VehicleGUI;
        public void GeometryType(int DoubleWishboneFront, int DoubleWishboneRear, int McPhersonFront, int McPhersonRear)
        {
            DoubleWishboneFront_VehicleGUI = 0; DoubleWishboneRear_VehicleGUI = 0; McPhersonFront_VehicleGUI = 0; McPhersonRear_VehicleGUI = 0;

            DoubleWishboneFront_VehicleGUI = DoubleWishboneFront; DoubleWishboneRear_VehicleGUI = DoubleWishboneRear;
            McPhersonFront_VehicleGUI = McPhersonFront; McPhersonRear_VehicleGUI = McPhersonRear;


            #region GUI operations for Front Double Wishbone
            if (DoubleWishboneFront == 1)
            {
                barButtonActuationType.Enabled = true;
                barButtonAntiRollBarType.Enabled = true;
            }
            #endregion

            #region GUI operations for Front McPherson
            else if (McPhersonFront == 1)
            {
                if (McPhersonRear == 1)
                {
                    barButtonActuationType.Enabled = false;
                    barButtonAntiRollBarType.Enabled = false;
                }
            }
            #endregion

            #region GUI operations for Rear Double Wishbone
            if (DoubleWishboneRear == 1)
            {

                barButtonActuationType.Enabled = true;
                barButtonAntiRollBarType.Enabled = true;

            }
            #endregion

            #region GUI operations for Rear McPherson Strut
            else if (McPhersonRear == 1)
            {
                if (McPhersonFront == 1)
                {
                    barButtonActuationType.Enabled = false;
                    barButtonAntiRollBarType.Enabled = false;
                }
            }
            #endregion

        }

        #endregion


        #region Type of Actuation

        public int PushrodFront_VehicleGUI, PullrodFront_VehicleGUI, PushrodRear_VehicleGUI, PullrodRear_VehicleGUI;
        public void ActuationType(int PushrodFront, int PullrodFront, int PushrodRear, int PullrodRear)
        {
            PushrodFront_VehicleGUI = 0; PullrodFront_VehicleGUI = 0; PushrodRear_VehicleGUI = 0; PullrodRear_VehicleGUI = 0;

            PushrodFront_VehicleGUI = PushrodFront; PullrodFront_VehicleGUI = PullrodFront;
            PushrodRear_VehicleGUI = PushrodRear; PullrodRear_VehicleGUI = PullrodRear;
        }

        #endregion


        #region Type of Anti-Roll Bar

        public int UARBFront_VehicleGUI, TARBFront_VehicleGUI, UARBRear_VehicleGUI, TARBRear_VehicleGUI;
        public void AntiRollBarType(int UARBFront, int TARBFront, int UARBRear, int TARBRear)
        {
            UARBFront_VehicleGUI = 0; TARBFront_VehicleGUI = 0; UARBRear_VehicleGUI = 0; TARBRear_VehicleGUI = 0;

            UARBFront_VehicleGUI = UARBFront; TARBFront_VehicleGUI = TARBFront;
            UARBRear_VehicleGUI = UARBRear; TARBRear_VehicleGUI = TARBRear;

        }

        #endregion

        #region Number of Couplings
        public int NoOfCouplings_VehicleGUI;
        public void NoOfCouplings(int _noOfCoupling)
        {
            NoOfCouplings_VehicleGUI = 0;
            NoOfCouplings_VehicleGUI = _noOfCoupling;
        }
        #endregion

        #endregion

        #region NavBar, TabControl and Ribbon GUI Operations
        //
        // NavBar, TabControl and Ribbon GUI Operations
        //

        #region TabControl_Outputs Events and Initialization
        private void InitializeTabControl()
        {
            #region TabControl_Outputs Events
            TabControl_Outputs = new CustomXtraTabControl();
            this.Controls.Add(TabControl_Outputs);
            TabControl_Outputs.ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;
            TabControl_Outputs.CloseButtonClick += new EventHandler(TabControl_Outputs_CloseButtonClick);
            TabControl_Outputs.SelectedPageChanged += TabControl_Outputs_SelectedPageChanged;
            TabControl_Outputs.MouseUp += TabControl_Outputs_MouseUp;

            TabControl_Outputs.Visible = true;
            TabControl_Outputs.BringToFront();
            TabControl_Outputs.Dock = DockStyle.Fill;
            #endregion
        }

        #region XtraTabControl close button click event
        void TabControl_Outputs_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;

        }
        #endregion

        private void TabControl_Outputs_MouseUp(object sender, MouseEventArgs e)
        {
            #region Need more time to evaluate the SelectedPageChanged event and the MouseUp event to find which is better. 
            ///<remarks>
            ///Right now, the issue which the MouseUp event is that if the tab is closed, then still this event is fired for the tabPage which was closed. Which doesn't make sense
            /// </remarks>


            TabControl_Outputs = sender as CustomXtraTabControl;
            XtraTabHitInfo hitInfo = TabControl_Outputs.CalcHitInfo(e.Location);

            bool TabPageBeingClosed = TabControl_Outputs.CalcHitInfo(e.Location).InPageControlBox;

            if (hitInfo.HitTest == XtraTabHitTest.PageHeader && !TabPageBeingClosed)
            {

                if (!IsBeingOpened)
                {
                    try
                    {
                        for (int i_VehicleGUI = 0; i_VehicleGUI < M1_Global.vehicleGUI.Count; i_VehicleGUI++)
                        {
                            for (int i_Page = 0; i_Page < M1_Global.vehicleGUI[i_VehicleGUI].TabPages_Vehicle.Count; i_Page++)
                            {
                                if (hitInfo.Page == M1_Global.vehicleGUI[i_VehicleGUI].TabPages_Vehicle[i_Page])
                                {
                                    navBarControlResults.ActiveGroup = M1_Global.vehicleGUI[i_VehicleGUI].navBarGroup_Vehicle_Result;

                                    if (ribbon.SelectedPage != ribbonPageResults || navBarControl1.ActiveGroup != navBarGroupResults)
                                    {
                                        ribbon.SelectedPage = ribbonPageResults; navBarControl1.ActiveGroup = navBarGroupResults;
                                    }
                                    DisplayMotionView(Vehicle.List_Vehicle[i_VehicleGUI]);

                                    break;
                                }
                            }
                            if (hitInfo.Page == M1_Global.vehicleGUI[i_VehicleGUI].TabPage_VehicleInputCAD)
                            {
                                ribbon.SelectedPage = ribbonPageDesign; navBarControl1.ActiveGroup = navBarGroupDesign;
                                navBarControlDesign.ActiveGroup = navBarGroupVehicle;
                                navBarGroupVehicle.SelectedLinkIndex = i_VehicleGUI;
                                navBarGroupVehicle.SelectedLink.PerformClick();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {

                        // Just in case
                    }

                    try
                    {
                        for (int i_sus = 0; i_sus < scflGUI.Count; i_sus++)
                        {
                            if (hitInfo.Page == scflGUI[i_sus].TabPage_FrontCAD)
                            {
                                ribbon.SelectedPage = ribbonPageDesign; navBarControl1.ActiveGroup = navBarGroupDesign;
                                navBarControlDesign.ActiveGroup = navBarGroupSuspensionFL;
                                navBarGroupSuspensionFL.SelectedLinkIndex = i_sus;
                                navBarGroupSuspensionFL.SelectedLink.PerformClick();
                                navBarGroupSuspensionFR.SelectedLinkIndex = i_sus;
                            }
                            else if (hitInfo.Page == scrlGUI[i_sus].TabPage_RearCAD)
                            {
                                ribbon.SelectedPage = ribbonPageDesign; navBarControl1.ActiveGroup = navBarGroupDesign;
                                navBarControlDesign.ActiveGroup = navBarGroupSuspensionRL;
                                navBarGroupSuspensionRL.SelectedLinkIndex = i_sus;
                                navBarGroupSuspensionRL.SelectedLink.PerformClick();
                                navBarGroupSuspensionRR.SelectedLinkIndex = i_sus;
                            }
                        }
                    }
                    catch (Exception)
                    {

                        //Just in case 
                    }

                }


            }
            #endregion

        }

        private void TabControl_Outputs_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            ///<remarks>
            ///The issue with this is that if a tab is closed, the thid event is triggerd as the selected TabPage is chnged
            /// </remarks>
            #region Selected the relevant input item or result
            //if (!IsBeingOpened)
            //{
            //    try
            //    {
            //        for (int i_VehicleGUI = 0; i_VehicleGUI < M1_Global.vehicleGUI.Count; i_VehicleGUI++)
            //        {
            //            for (int i_Page = 0; i_Page < M1_Global.vehicleGUI[i_VehicleGUI].TabPages_Vehicle.Count; i_Page++)
            //            {
            //                if (e.Page == M1_Global.vehicleGUI[i_VehicleGUI].TabPages_Vehicle[i_Page])
            //                {
            //                    navBarControlResults.ActiveGroup = M1_Global.vehicleGUI[i_VehicleGUI].navBarGroup_Vehicle_Result;

            //                    if (ribbon.SelectedPage != ribbonPageResults || navBarControl1.ActiveGroup != navBarGroupResults)
            //                    {
            //                        ribbon.SelectedPage = ribbonPageResults; navBarControl1.ActiveGroup = navBarGroupResults;
            //                    }
            //                    DisplayMotionView(Vehicle.List_Vehicle[i_VehicleGUI]);

            //                    break;
            //                }
            //            }
            //            if (e.Page == M1_Global.vehicleGUI[i_VehicleGUI].TabPage_VehicleInputCAD)
            //            {
            //                ribbon.SelectedPage = ribbonPageDesign; navBarControl1.ActiveGroup = navBarGroupDesign;
            //                navBarControl2.ActiveGroup = navBarGroupVehicle;
            //                navBarGroupVehicle.SelectedLinkIndex = i_VehicleGUI;
            //                navBarGroupVehicle.SelectedLink.PerformClick();
            //                break;
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        // Just in case
            //    }

            //    try
            //    {
            //        for (int i_sus = 0; i_sus < scflGUI.Count; i_sus++)
            //        {
            //            if (e.Page == scflGUI[i_sus].TabPage_FrontCAD)
            //            {
            //                ribbon.SelectedPage = ribbonPageDesign; navBarControl1.ActiveGroup = navBarGroupDesign;
            //                navBarControl2.ActiveGroup = navBarGroupSuspensionFL;
            //                navBarGroupSuspensionFL.SelectedLinkIndex = i_sus;
            //                navBarGroupSuspensionFL.SelectedLink.PerformClick();
            //                navBarGroupSuspensionFR.SelectedLinkIndex = i_sus;
            //            }
            //            else if (e.Page == scrlGUI[i_sus].TabPage_RearCAD)
            //            {
            //                ribbon.SelectedPage = ribbonPageDesign; navBarControl1.ActiveGroup = navBarGroupDesign;
            //                navBarControl2.ActiveGroup = navBarGroupSuspensionRL;
            //                navBarGroupSuspensionRL.SelectedLinkIndex = i_sus;
            //                navBarGroupSuspensionRL.SelectedLink.PerformClick();
            //                navBarGroupSuspensionRR.SelectedLinkIndex = i_sus;
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        //Just in case 
            //    }

            //}
            #endregion

        }
        #endregion

        #region NavBar Control 1 Activities

        private void navBarControl1_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            if (!IsBeingOpened)
            {
                try
                {
                    if (navBarControl1.ActiveGroup == navBarGroupDesign)
                    {
                        ribbon.SelectedPage = ribbonPageDesign;
                        navBarControl2ActiveGroupOperations(navBarControlDesign.ActiveGroup);
                    }

                    if (navBarControl1.ActiveGroup == navBarGroupSimulationSetup)
                    {
                        #region GUI
                        ribbon.SelectedPage = ribbonPageSimulation;

                        sidePanel2.Hide();
                        #endregion
                    }

                    if (navBarControl1.ActiveGroup == navBarGroupResults)
                    {
                        ribbon.SelectedPage = ribbonPageResults;

                        #region Motion View Operations
                        try
                        {
                            for (int i_res = 0; i_res < Vehicle.List_Vehicle.Count; i_res++)
                            {
                                if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_res].navBarGroup_Vehicle_Result.Name)
                                {
                                    #region GUI
                                    groupControl13.Show();
                                    accordionControlVehicleItem.Hide();
                                    sidePanel2.Show();
                                    gridControl2.Show();
                                    #endregion

                                    int motionIndex = Vehicle.List_Vehicle[i_res].vehicle_Motion.MotionID - 1;
                                    gridControl2.MainView = MotionGUI.List_MotionGUI[motionIndex].bandedGridView_Motion;
                                    DisplayMotionView(Vehicle.List_Vehicle[i_res]);
                                }
                            }
                        }
                        catch (Exception)
                        {

                            //In case the active group is null. This happens when there are no results yet to display
                        }
                        #endregion
                    }

                }
                catch (Exception)
                {

                }
            }
        }
        private void navBarControl1_MouseDown(object sender, MouseEventArgs e)
        {
            #region MyRegion
            //navBarControl1 = sender as NavBarControl;
            //NavBarHitInfo hitinfo = navBarControl1.CalcHitInfo(e.Location);

            //if (hitinfo.HitTest == NavBarHitTest.GroupCaption)
            //{


            //    #region MyRegion
            //    //try
            //    //{
            //    //    switch (hitinfo.Group.Caption)
            //    //    {
            //    //        case "Design":
            //    //            //#region GUI
            //    //            ribbon.SelectedPage = ribbonPageDesign;
            //    //            //groupControl13.Show();
            //    //            //sidePanel2.Show();
            //    //            //#endregion

            //    //            navBarControl2ActiveGroupOperations(navBarControl2.ActiveGroup);

            //    //            break;

            //    //        case "Simulation Setup":
            //    //            #region GUI
            //    //            ribbon.SelectedPage = ribbonPageSimulation;
            //    //            
            //    //            sidePanel2.Hide(); 
            //    //            #endregion

            //    //            break;

            //    //        case "Results":
            //    //            ribbon.SelectedPage = ribbonPageResults;

            //    //            #region Motion View Operations
            //    //            try
            //    //            {
            //    //                for (int i_res = 0; i_res < Vehicle.List_Vehicle.Count; i_res++)
            //    //                {
            //    //                    if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_res].navBarGroup_Vehicle_Result.Name)
            //    //                    {
            //    //                        #region GUI
            //    //                        groupControl13.Show();
            //    //                        accordionControlVehicleItem.Hide();
            //    //                        sidePanel2.Show();
            //    //                        #endregion

            //    //                        int motionIndex = Vehicle.List_Vehicle[i_res].vehicle_Motion.MotionID - 1;
            //    //                        gridControl2.MainView = MotionGUI.List_MotionGUI[motionIndex].bandedGridView_Motion;
            //    //                        DisplayMotionView();
            //    //                    }
            //    //                }
            //    //            }
            //    //            catch (Exception)
            //    //            {

            //    //                //In case the active group is null. This happens when there are no results yet to display
            //    //            } 
            //    //            #endregion

            //    //            break;
            //    //    }
            //    //}
            //    //catch (Exception)
            //    //{

            //    //} 
            //    #endregion
            //} 
            #endregion
        }
        #endregion

        #region NavBarControlResults GUI
        void navBarControlResults_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            if (!IsBeingOpened)
            {
                Button_Recalculate_Enabler();
                for (int i_Motion = 0; i_Motion < Vehicle.List_Vehicle.Count; i_Motion++)
                {
                    if (e.Group.Name == M1_Global.vehicleGUI[i_Motion].navBarGroup_Vehicle_Result.Name)
                    {
                        ///<remarks>
                        ///The below commented out lines of code were used to hide and show the side panel and hence the motion grid view when the vehicle had Motion. But these lines of code for some reason also triggered the 
                        ///<see cref="InputComboboxes_Leave(object, EventArgs)"/> method. No idea why. Only clue is that this method is called when a result is calculated so maybe the groupControl13.Show() or sidePanel2.Show() method caused the trigger 
                        /// </remarks>
                        navBarControlResults.ActiveGroup = e.Group;

                        DisplayMotionView(Vehicle.List_Vehicle[i_Motion]);
                        break;

                    }
                }

                for (int i = 0; i < BatchRunGUI.batchRuns_GUI.Count; i++)
                {
                    if (e.Group.Name == BatchRunGUI.batchRuns_GUI[i].navBarGroupBatchRunResults.Name)
                    {
                        barButtonItemCreateWorksheet.Enabled = true;
                        break;
                    }
                    else
                    {
                        barButtonItemCreateWorksheet.Enabled = false;

                    }
                }

            }
        }
        private void DisplayMotionView(Vehicle _vehicleDataSource)
        {
            try
            {

                gridControl2.DataSource = _vehicleDataSource.vehicle_Motion.Motion_DataTable;

            }
            catch (Exception E)
            {
                string error = E.Message;
                //Need to determine if this try block here is really necessary
            }
        }

        #endregion

        #region NavBarControl2 GUI

        private void navBarControl2ActiveGroupOperations(NavBarGroup _navBarGroup)
        {
            if (_navBarGroup == navBarGroupSuspensionFL)
            {
                if (SuspensionCoordinatesFront.SCFLCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[SuspensionCoordinatesFront.SCFLCurrentID - 1]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[SuspensionCoordinatesFront.SCFLCurrentID - 1]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[SuspensionCoordinatesFront.SCFLCurrentID - 1].SCFLIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].SCFLDataTableGUI;
                    gridControl2.MainView = scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].bandedGridView_SCFLGUI;
                    scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].bandedGridView_SCFLGUI.ExpandAllGroups();
                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupSuspensionFR)
            {
                if (SuspensionCoordinatesFrontRight.SCFRCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[SuspensionCoordinatesFrontRight.SCFRCurrentID - 1]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[SuspensionCoordinatesFrontRight.SCFRCurrentID - 1]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[SuspensionCoordinatesFrontRight.SCFRCurrentID - 1].SCFRIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].SCFRDataTableGUI;
                    gridControl2.MainView = scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].bandedGridView_SCFRGUI;
                    scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].bandedGridView_SCFRGUI.ExpandAllGroups();
                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupSuspensionRL)
            {
                if (SuspensionCoordinatesRear.SCRLCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();

                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[SuspensionCoordinatesRear.SCRLCurrentID - 1]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[SuspensionCoordinatesRear.SCRLCurrentID - 1]._RedocommandsSCRL, SuspensionCoordinatesRear.SCRLCurrentID, SuspensionCoordinatesRear.Assy_List_SCRL[SuspensionCoordinatesRear.SCRLCurrentID - 1].SCRLIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].SCRLDataTableGUI;
                    gridControl2.MainView = scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].bandedGridView_SCRLGUI;
                    scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].bandedGridView_SCRLGUI.ExpandAllGroups();

                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupSuspensionRR)
            {
                if (SuspensionCoordinatesRearRight.SCRRCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[SuspensionCoordinatesRearRight.SCRRCurrentID - 1]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[SuspensionCoordinatesRearRight.SCRRCurrentID - 1]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[SuspensionCoordinatesRearRight.SCRRCurrentID - 1].SCRRIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.MainView = scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].bandedGridView_SCRRGUI;
                    gridControl2.DataSource = scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].SCRRDataTableGUI;
                    scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].bandedGridView_SCRRGUI.ExpandAllGroups();
                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupTireStiffness)
            {
                if (Tire.TireCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(Tire.Assy_List_Tire[Tire.CurrentTireID - 1]._UndocommandsTire, Tire.Assy_List_Tire[Tire.CurrentTireID - 1]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[Tire.CurrentTireID - 1].TireIsModified);
                    #endregion

                    #region Grid Control operations
                    gridControl2.DataSource = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].TireDataTableGUI;
                    gridControl2.MainView = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].bandedGridView_TireGUI;
                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupSprings)
            {
                if (Spring.SpringCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(Spring.Assy_List_Spring[Spring.CurrentSpringID - 1]._UndocommandsSpring, Spring.Assy_List_Spring[Spring.CurrentSpringID - 1]._RedocommandsSpring, Spring.CurrentSpringID, Spring.Assy_List_Spring[Spring.CurrentSpringID - 1].SpringIsModified);
                    #endregion

                    #region Grid Control operations
                    gridControl2.DataSource = springGUI[navBarGroupSprings.SelectedLinkIndex].SpringDataTableGUI;
                    gridControl2.MainView = springGUI[navBarGroupSprings.SelectedLinkIndex].bandedGridView_SpringGUI;
                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupDamper)
            {
                if (Damper.DamperCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(Damper.Assy_List_Damper[Damper.CurrentDamperID - 1]._UndocommandsDamper, Damper.Assy_List_Damper[Damper.CurrentDamperID - 1]._RedocommandsDamper, Damper.CurrentDamperID, Damper.Assy_List_Damper[Damper.CurrentDamperID - 1].DamperIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = damperGUI[navBarGroupDamper.SelectedLinkIndex].DamperDataTableGUI;
                    gridControl2.MainView = damperGUI[navBarGroupDamper.SelectedLinkIndex].bandedGridView_DamperGUI;
                    #endregion
                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupAntiRollBar)
            {
                if (AntiRollBar.AntiRollBarCounter != 0)
                {

                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(AntiRollBar.Assy_List_ARB[AntiRollBar.CurrentAntiRollBarID - 1]._UndocommandsARB, AntiRollBar.Assy_List_ARB[AntiRollBar.CurrentAntiRollBarID - 1]._RedocommandsARB, AntiRollBar.CurrentAntiRollBarID, AntiRollBar.Assy_List_ARB[AntiRollBar.CurrentAntiRollBarID - 1].AntiRollBarIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].ARBDataTableGUI;
                    gridControl2.MainView = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].bandedGridView_ARBGUI;
                    #endregion

                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupChassis)
            {
                if (Chassis.ChassisCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(Chassis.Assy_List_Chassis[Chassis.CurrentChassisID - 1]._UndocommandsChassis, Chassis.Assy_List_Chassis[Chassis.CurrentChassisID - 1]._RedocommandsChassis, Chassis.CurrentChassisID, Chassis.Assy_List_Chassis[Chassis.CurrentChassisID - 1].ChassisIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = chassisGUI[navBarGroupChassis.SelectedLinkIndex].ChassisDataTableGUI;
                    gridControl2.MainView = chassisGUI[navBarGroupChassis.SelectedLinkIndex].bandedGridViewChassis;
                    chassisGUI[navBarGroupChassis.SelectedLinkIndex].bandedGridViewChassis.ExpandAllGroups();
                    #endregion

                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupWheelAlignment)
            {
                if (WheelAlignment.WheelAlignmentCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    gridControl2.Show();
                    gridControl2.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(WheelAlignment.Assy_List_WA[WheelAlignment.CurrentWheelAlignmentID - 1]._UndocommandsWheelAlignment, WheelAlignment.Assy_List_WA[WheelAlignment.CurrentWheelAlignmentID - 1]._RedocommandsWheelAlignment, WheelAlignment.CurrentWheelAlignmentID, WheelAlignment.Assy_List_WA[WheelAlignment.CurrentWheelAlignmentID - 1].WheelAlignmentIsModified);
                    #endregion

                    #region Grid Control Operations
                    gridControl2.DataSource = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].WADataTableGUI;
                    gridControl2.MainView = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].bandedGridView_WAGUI;
                    #endregion

                }
                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }

            if (_navBarGroup == navBarGroupVehicle)
            {
                if (Vehicle.VehicleCounter != 0)
                {
                    #region GUI
                    sidePanel2.Show();
                    groupControl13.Show();
                    accordionControlVehicleItem.Hide();
                    accordionControlVehicleItem.Show();
                    accordionControlVehicleItem.BringToFront();
                    #endregion

                    #region Populating the UndoRedo Class' Undo/Redo Stacks
                    UndoObject.Identifier(Vehicle.List_Vehicle[Vehicle.CurrentVehicleID - 1]._UndocommandsVehicle, Vehicle.List_Vehicle[Vehicle.CurrentVehicleID - 1]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[Vehicle.CurrentVehicleID - 1].VehicleIsModified);
                    #endregion
                }

                else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
            }


        }

        private void navBarControl2_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {

            try
            {
                if (!IsBeingOpened)
                {
                    navBarControl2ActiveGroupOperations(navBarControlDesign.ActiveGroup);
                }
            }
            catch (Exception)
            {

                //To prevent exception in case active group is null
            }

        }

        private void navBarControl2_MouseDown(object sender, MouseEventArgs e)
        {
        }
        #endregion

        #region Ribbon GUI
        private void ribbon_MouseDown(object sender, MouseEventArgs e)
        {
            ribbon = sender as RibbonControl;
            RibbonHitInfo hitinfo = ribbon.CalcHitInfo(e.Location);

            if (hitinfo.HitTest == RibbonHitTest.PageHeader)
            {
                switch (hitinfo.Page.Name)
                {
                    case "ribbonPageDesign":
                        //#region GUI
                        //groupControl13.Show();
                        //sidePanel2.Show();
                        //accordionControlTireStiffness.Hide();
                        //accordionControlSuspensionCoordinatesFL.Hide();
                        //accordionControlSuspensionCoordinatesFR.Hide();
                        //accordionControlSuspensionCoordinatesRL.Hide();
                        //accordionControlSuspensionCoordinatesRR.Hide();
                        //accordionControlDamper.Hide();
                        //accordionControlAntiRollBar.Hide();
                        //accordionControlSprings.Hide();
                        //accordionControlChassis.Hide();
                        //accordionControlWheelAlignment.Hide();
                        accordionControlVehicleItem.Hide();
                        navBarGroupSimulationSetup.Expanded = false;
                        navBarGroupResults.Expanded = false;
                        navBarGroupDesign.Visible = true;
                        navBarGroupDesign.Expanded = true;

                        //#endregion

                        navBarControl2ActiveGroupOperations(navBarControlDesign.ActiveGroup);

                        break;

                    case "ribbonPageSimulation":
                        #region GUI

                        sidePanel2.Hide();
                        //accordionControlTireStiffness.Hide();
                        //accordionControlSuspensionCoordinatesFL.Hide();
                        //accordionControlSuspensionCoordinatesFR.Hide();
                        //accordionControlSuspensionCoordinatesRL.Hide();
                        //accordionControlSuspensionCoordinatesRR.Hide();
                        //accordionControlDamper.Hide();
                        //accordionControlAntiRollBar.Hide();
                        //accordionControlSprings.Hide();
                        //accordionControlChassis.Hide();
                        //accordionControlWheelAlignment.Hide();
                        //accordionControlVehicleItem.Hide();
                        navBarGroupDesign.Expanded = false;
                        navBarGroupResults.Expanded = false;
                        navBarGroupSimulationSetup.Visible = true;
                        navBarGroupSimulationSetup.Expanded = true;
                        #endregion

                        barButtonUndo.Enabled = false; barButtonRedo.Enabled = false;
                        break;

                    case "ribbonPageResults":
                        navBarGroupDesign.Expanded = false;
                        navBarGroupSimulationSetup.Expanded = false;
                        navBarGroupResults.Visible = true;
                        navBarGroupResults.Expanded = true;

                        barButtonUndo.Enabled = false; barButtonRedo.Enabled = false;

                        #region Motion View operations
                        try
                        {
                            for (int i_res = 0; i_res < Vehicle.List_Vehicle.Count; i_res++)
                            {
                                if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_res].navBarGroup_Vehicle_Result.Name)
                                {
                                    #region GUI
                                    groupControl13.Show();
                                    sidePanel2.Show();
                                    //accordionControlTireStiffness.Hide();
                                    //accordionControlSuspensionCoordinatesFL.Hide();
                                    //accordionControlSuspensionCoordinatesFR.Hide();
                                    //accordionControlSuspensionCoordinatesRL.Hide();
                                    //accordionControlSuspensionCoordinatesRR.Hide();
                                    //accordionControlDamper.Hide();
                                    //accordionControlAntiRollBar.Hide();
                                    //accordionControlSprings.Hide();
                                    //accordionControlChassis.Hide();
                                    //accordionControlWheelAlignment.Hide();
                                    accordionControlVehicleItem.Hide();
                                    #endregion

                                    int motionIndex = Vehicle.List_Vehicle[i_res].vehicle_Motion.MotionID - 1;
                                    gridControl2.MainView = MotionGUI.List_MotionGUI[motionIndex].bandedGridView_Motion;
                                    DisplayMotionView(Vehicle.List_Vehicle[i_res]);
                                }
                            }
                        }
                        catch (Exception E)
                        {
                            string error = E.Message;
                            //In case the active group is null. This happens when there are no results yet to display
                        }
                        #endregion

                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        #endregion

        //
        // Input Item Creation and Modification
        //
        #region Input Item Creation and Modification 

        #region Helper Modifiying Methods
        public static void Tire_ModifyInVehicle(int _index, Tire _tire)
        {
            #region Changing the Vehicle Tires
            bool VehicleIsChanged;
            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].tire_FL._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_FL = _tire;
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].tire_FR._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_FR = _tire;
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].tire_RL._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_RL = _tire;
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].tire_RR._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_RR = _tire;
                        VehicleIsChanged = true;

                    }

                    if (VehicleIsChanged)
                    {
                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }
        public static void SCFL_ModifyInVehicle(int _index, SuspensionCoordinatesFront _SCFL)
        {
            #region Changing the Vehicle Front Left Suspension Coordinate 


            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].sc_FL._SCName == SuspensionCoordinatesFront.Assy_List_SCFL[_index]._SCName)
                    {

                        Vehicle.List_Vehicle[i].sc_FL = _SCFL;
                        VehicleIsChanged = true;

                    }

                    if (VehicleIsChanged)
                    {
                        if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.Generic)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].CADVehicleInputs, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }
                        else if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.ImportedCAD)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].importCADForm.importCADViewport, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }

                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }
            }
            catch (Exception) { }
            #endregion
        }
        public static void SCFR_ModifyInVehicle(int _index, SuspensionCoordinatesFrontRight _SCFR)
        {
            #region Changing the  Vehicle Front Right Suspension Coordinate 


            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].sc_FR._SCName == SuspensionCoordinatesFrontRight.Assy_List_SCFR[_index]._SCName)
                    {
                        Vehicle.List_Vehicle[i].sc_FR = _SCFR;
                        VehicleIsChanged = true;
                    }

                    if (VehicleIsChanged)
                    {
                        if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.Generic)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].CADVehicleInputs, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }

                        else if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.ImportedCAD)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].importCADForm.importCADViewport, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }


                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }
            }
            catch (Exception) { }
            #endregion
        }

        public static void SCRL_ModifyInVehicle(int _index, SuspensionCoordinatesRear _SCRL)
        {
            #region Changing the Vehicle Rear Left Suspension Coordinate 
            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].sc_RL._SCName == SuspensionCoordinatesRear.Assy_List_SCRL[_index]._SCName)
                    {

                        Vehicle.List_Vehicle[i].sc_RL = _SCRL;
                        VehicleIsChanged = true;

                    }

                    if (VehicleIsChanged)
                    {
                        if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.Generic)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].CADVehicleInputs, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }
                        else if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.ImportedCAD)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].importCADForm.importCADViewport, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }

                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }

            }
            catch (Exception) { }
            #endregion
        }
        public static void SCRR_ModifyInVehicle(int _index, SuspensionCoordinatesRearRight _SCRR)
        {
            #region Changing the Vehicle Rear Right Suspension Coordinate 
            try
            {
                bool VehicleIsChanged = false;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].sc_RR._SCName == SuspensionCoordinatesRearRight.Assy_List_SCRR[_index]._SCName)
                    {
                        Vehicle.List_Vehicle[i].sc_RR = _SCRR;
                        VehicleIsChanged = true;

                    }

                    if (VehicleIsChanged)
                    {
                        if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.Generic)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].CADVehicleInputs, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);

                        }
                        else if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.ImportedCAD)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].importCADForm.importCADViewport, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }

                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }

            }
            catch (Exception) { }
            #endregion
        }

        public static void Chassis_ModifyInVehicle(int _index, Chassis _chassis)
        {
            #region Chnaing the Vehicle Chassis 
            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].chassis_vehicle._ChassisName == Chassis.Assy_List_Chassis[_index]._ChassisName)
                    {
                        Vehicle.List_Vehicle[i].chassis_vehicle = _chassis;
                        //Vehicle.List_Vehicle[i].ChassisSuspendedMassCalculator();
                        Vehicle.List_Vehicle[i].ChassisCornerMassCalculator();
                        VehicleIsChanged = true;
                    }

                    if (VehicleIsChanged)
                    {
                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }
            }
            catch (Exception) { }

            #endregion
        }

        public static void Sring_ModifyInVehicle(int _index, Spring _spring)
        {
            #region Changing the Vehicle Spring 
            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].spring_FL._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {

                        Vehicle.List_Vehicle[i].spring_FL = _spring;
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].spring_FR._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {

                        Vehicle.List_Vehicle[i].spring_FR = _spring;
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].spring_RL._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {


                        Vehicle.List_Vehicle[i].spring_RL = _spring;
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].spring_RR._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {

                        Vehicle.List_Vehicle[i].spring_RR = _spring;
                        VehicleIsChanged = true;
                    }

                    if (VehicleIsChanged)
                    {
                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }

            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }

        public static void Damper_ModifyInVehicle(int _index, Damper _damper)
        {
            #region Changing the Vehicle Damper 
            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].damper_FL._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {
                        Vehicle.List_Vehicle[i].damper_FL = _damper;
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].damper_FR._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {
                        Vehicle.List_Vehicle[i].damper_FR = _damper;
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].damper_RL._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {
                        Vehicle.List_Vehicle[i].damper_RL = _damper;
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].damper_RR._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {
                        Vehicle.List_Vehicle[i].damper_RR = _damper;
                        VehicleIsChanged = true;
                    }

                    if (VehicleIsChanged)
                    {
                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }

        public static void ARB_ModifyInVehicle(int _index, AntiRollBar _arb)
        {
            #region Changing the Vehicle ARB 
            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].arb_FL._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {
                        Vehicle.List_Vehicle[i].arb_FL = _arb;
                        Vehicle.List_Vehicle[i].ARB_Rate_Nmm_FL = Vehicle.List_Vehicle[i].AntiRollBarRate_Nmm(Vehicle.List_Vehicle[i].sc_FL, Vehicle.List_Vehicle[i].arb_FL);
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].arb_FR._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {
                        Vehicle.List_Vehicle[i].arb_FR = _arb;
                        Vehicle.List_Vehicle[i].ARB_Rate_Nmm_FR = Vehicle.List_Vehicle[i].AntiRollBarRate_Nmm(Vehicle.List_Vehicle[i].sc_FR, Vehicle.List_Vehicle[i].arb_FR);
                        VehicleIsChanged = true;
                    }

                    if (Vehicle.List_Vehicle[i].arb_RL._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {
                        Vehicle.List_Vehicle[i].arb_RL = _arb;
                        Vehicle.List_Vehicle[i].ARB_Rate_Nmm_RL = Vehicle.List_Vehicle[i].AntiRollBarRate_Nmm(Vehicle.List_Vehicle[i].sc_RL, Vehicle.List_Vehicle[i].arb_RL);
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].arb_RR._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {
                        Vehicle.List_Vehicle[i].arb_RR = _arb;
                        Vehicle.List_Vehicle[i].ARB_Rate_Nmm_RR = Vehicle.List_Vehicle[i].AntiRollBarRate_Nmm(Vehicle.List_Vehicle[i].sc_RR, Vehicle.List_Vehicle[i].arb_RR);
                        VehicleIsChanged = true;

                    }

                    if (VehicleIsChanged)
                    {
                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }

            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }
        public static void WA_ModifyInVehicle(int _index, WheelAlignment _wa)
        {
            #region Changing the Vehicle WheelAlignment 
            try
            {
                bool VehicleIsChanged;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    VehicleIsChanged = false;
                    if (Vehicle.List_Vehicle[i].wa_FL._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_FL = _wa;
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].wa_FR._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_FR = _wa;
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].wa_RL._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_RL = _wa;
                        VehicleIsChanged = true;

                    }

                    if (Vehicle.List_Vehicle[i].wa_RR._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_RR = _wa;
                        VehicleIsChanged = true;

                    }

                    if (VehicleIsChanged)
                    {
                        if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.Generic)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].CADVehicleInputs, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                             
                        }
                        else if (M1_Global.vehicleGUI[i].VisualizationType == VehicleVisualizationType.ImportedCAD)
                        {
                            M1_Global.vehicleGUI[i].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i].importCADForm.importCADViewport, i, true, M1_Global.vehicleGUI[i].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[i].CadIsTobeImported, M1_Global.vehicleGUI[i].PlotWheel);
                        }
                        R1.FormVariableUpdater();
                        R1.DeleteNavBarControlResultsGroupANDTabPages(i);
                    }

                }


            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }
        #endregion

        #region Data Validation for Tire, Damper & Anti-Roll Bar
        void bandedGridView_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            double checker = 0;
            if (!Double.TryParse(e.Value as string, out checker))
            {
                e.Valid = false;
                e.ErrorText = "Please enter numeric values";
            }
            else if (Convert.ToDouble(e.Value) <= 0)
            {

                e.Valid = false;
                e.ErrorText = "Please enter positive values";
            }
        }
        #endregion

        #region Data Grid Cell Change Operations (Modification of Input Items
        void bandedGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            #region Tire Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupTireStiffness)
            {
                int index = navBarGroupTireStiffness.SelectedLinkIndex;

                for (int l_tire = 0; l_tire <= Tire.Assy_List_Tire.Count; l_tire++)
                {
                    if (index == l_tire)
                    {
                        #region Editing the TireGUI object
                        tireGUI[l_tire].Update_TireGUI(this, l_tire);
                        #endregion

                        #region Editing the Tire object
                        Tire.Assy_List_Tire[l_tire].ModifyObjectData(l_tire, tireGUI[l_tire], false);
                        Tire.CurrentTireID = Tire.Assy_List_Tire[l_tire].TireID;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(Tire.Assy_List_Tire[l_tire]._UndocommandsTire, Tire.Assy_List_Tire[l_tire]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[l_tire].TireIsModified);
                        #endregion

                        ChangeTracker++;
                        break;
                    }
                }

                ComboboxTireOperations();

                // Counter is not incremented here because in this code block a new item is not being created, it is only being edited. 
            }
            #endregion

            #region Spring Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupSprings)
            {
                int index = navBarGroupSprings.SelectedLinkIndex;

                for (int l_spring = 0; l_spring <= Spring.Assy_List_Spring.Count; l_spring++)
                {
                    if (index == l_spring)
                    {
                        #region Editing the SpringGUI object
                        springGUI[l_spring].Update_SpringGUI(this, l_spring);
                        #endregion

                        #region Adding a new Spring to List of Spring Objects
                        Spring.Assy_List_Spring[l_spring].ModifyObjectData(l_spring, springGUI[l_spring], false);
                        Spring.CurrentSpringID = Spring.Assy_List_Spring[l_spring].SpringID;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(Spring.Assy_List_Spring[l_spring]._UndocommandsSpring, Spring.Assy_List_Spring[l_spring]._RedocommandsSpring, Spring.CurrentSpringID, Spring.Assy_List_Spring[l_spring].SpringIsModified);
                        #endregion

                        ChangeTracker++;
                        break;

                    }
                }
                ComboBoxSpringOperations();
                // Counter is not imcremented here becsuse in this code bloc a new item is not created, it is only being edited
            }
            #endregion

            #region Damper Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupDamper)
            {
                int index = navBarGroupDamper.SelectedLinkIndex;

                for (int l_damper = 0; l_damper <= Damper.Assy_List_Damper.Count; l_damper++)
                {
                    if (index == l_damper)
                    {
                        #region Editing the DamperGUI object
                        damperGUI[l_damper].Update_DamperGUI(this, l_damper);
                        #endregion

                        #region Adding the new damper to the list of damper objects
                        Damper.Assy_List_Damper[l_damper].ModifyObjectData(l_damper, damperGUI[l_damper], false);
                        Damper.CurrentDamperID = Damper.Assy_List_Damper[l_damper].DamperID;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(Damper.Assy_List_Damper[l_damper]._UndocommandsDamper, Damper.Assy_List_Damper[l_damper]._RedocommandsDamper, Damper.CurrentDamperID, Damper.Assy_List_Damper[l_damper].DamperIsModified);
                        #endregion

                        ChangeTracker++;
                        break;

                    }
                }
                ComboboxDamperOperations();
                // Counter is not incremented here because in this code block a new damperitem is not being created, it is only being edited.
            }
            #endregion

            #region ARB Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupAntiRollBar)
            {
                int index = navBarGroupAntiRollBar.SelectedLinkIndex;

                for (int l_arb = 0; l_arb < AntiRollBar.Assy_List_ARB.Count; l_arb++)
                {
                    if (index == l_arb)
                    {
                        #region Editing the ARBGUI object
                        arbGUI[l_arb].Update_ARBGUI(this, l_arb);
                        #endregion

                        #region Editing the AntiRollBar Object
                        AntiRollBar.Assy_List_ARB[l_arb].ModifyObjectData(l_arb, arbGUI[l_arb], false);
                        AntiRollBar.CurrentAntiRollBarID = AntiRollBar.Assy_List_ARB[l_arb].AntiRollBarID;
                        #endregion

                        #region Populating the Undo/Edo Stack of the UndoRedo Class
                        UndoObject.Identifier(AntiRollBar.Assy_List_ARB[l_arb]._UndocommandsARB, AntiRollBar.Assy_List_ARB[l_arb]._RedocommandsARB, AntiRollBar.CurrentAntiRollBarID, AntiRollBar.Assy_List_ARB[l_arb].AntiRollBarIsModified);
                        #endregion

                        ChangeTracker++;
                        break;
                    }

                }
                ComboboxARBOperations();
                // Counter is not incremented here because in this code a new ARB item is not being created, it is only being edited
            }
            #endregion

            #region Wheel Alignment Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupWheelAlignment)
            {
                int index = navBarGroupWheelAlignment.SelectedLinkIndex;

                for (int l_wa = 0; l_wa <= WheelAlignment.Assy_List_WA.Count; l_wa++)
                {
                    if (index == l_wa)
                    {

                        #region Editing the WheelAlignmntGUI object
                        waGUI[l_wa].Update_WheelAlignmentGUI(this, l_wa);
                        #endregion

                        #region Editing the WheelAlignment object
                        WheelAlignment.Assy_List_WA[l_wa].ModifyObjectData(l_wa, waGUI[l_wa], false);
                        WheelAlignment.CurrentWheelAlignmentID = WheelAlignment.Assy_List_WA[l_wa].WheelAlignmentID;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(WheelAlignment.Assy_List_WA[l_wa]._UndocommandsWheelAlignment, WheelAlignment.Assy_List_WA[l_wa]._RedocommandsWheelAlignment, WheelAlignment.CurrentWheelAlignmentID, WheelAlignment.Assy_List_WA[l_wa].WheelAlignmentIsModified);
                        #endregion

                        ChangeTracker++;
                        break;
                    }
                }
                ComboboxWheelAlignmentOperations();
            }
            #endregion

            #region Chassis Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupChassis)
            {
                int index = navBarGroupChassis.SelectedLinkIndex;

                for (int l_chassis = 0; l_chassis <= Chassis.Assy_List_Chassis.Count; l_chassis++)
                {
                    if (index == l_chassis)
                    {
                        #region Editing the ChassiGUI object
                        chassisGUI[l_chassis].Update_ChassisGUI(this, l_chassis);
                        #endregion

                        #region Editing the Chassis object
                        Chassis.Assy_List_Chassis[l_chassis].ModifyObjectData(l_chassis, chassisGUI[l_chassis], false);
                        Chassis.CurrentChassisID = Chassis.Assy_List_Chassis[l_chassis].ChassisID;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(Chassis.Assy_List_Chassis[l_chassis]._UndocommandsChassis, Chassis.Assy_List_Chassis[l_chassis]._RedocommandsChassis, Chassis.CurrentChassisID, Chassis.Assy_List_Chassis[l_chassis].ChassisIsModified);
                        #endregion

                        ChangeTracker++;
                        break;
                    }
                }
                ComboboxChassisOperations();
                // Counter is not incremented here because in this code a new Chassis item is not being created, it is only being edited
            }
            #endregion

            #region SCFL Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionFL)
            {
                ModifyFrontLeftSuspension(false, navBarGroupSuspensionFL.SelectedLinkIndex);

            }
            #endregion

            #region SCFR Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionFR)
            {
                ModifyFrontRightSuspension(false, navBarGroupSuspensionFR.SelectedLinkIndex);
            }
            #endregion

            #region SCRL Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionRL)
            {
                ModifyRearLeftSuspension(false, navBarGroupSuspensionRL.SelectedLinkIndex);
            }
            #endregion

            #region SCRR Modification
            if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionRR)
            {
                ModifyRearRightSuspension(false, navBarGroupSuspensionRR.SelectedLinkIndex);
            }
            #endregion
        }
        #endregion

        //
        //Tire Item Creation and GUI
        //
        #region Tire Item Creation and GUI

        List<TireGUI> tireGUI = new List<TireGUI>();

        public void BarButtonTireRate_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlTireStiffness.Show();
            navBarGroupTireStiffness.Expanded = true;
            navBarGroupDesign.Visible = true;
            //accordionControlTireStiffness.ExpandElement(accordionControlTire1TireStiffness);
            //accordionControlTireStiffness.ExpandElement(accordionControlTire1TireWidth);

            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_tire = 0; i_tire <= navBarItemTireClass.navBarItemTire.Count; i_tire++)
            {
                if (Tire.TireCounter == i_tire)
                {
                    #region Creating a new NavBarItem and adding it the Tire Group
                    navBarItemTireClass temp_navBarItemTire = new navBarItemTireClass();
                    navBarTire_Global.CreateNewNavBarItem(i_tire, temp_navBarItemTire, navBarControlDesign, navBarGroupTireStiffness);
                    navBarItemTireClass.navBarItemTire[i_tire].LinkClicked += new NavBarLinkEventHandler(navBarItemTire1_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_tire = 0; l_tire <= Tire.Assy_List_Tire.Count; l_tire++)
            {


                if (Tire.TireCounter == l_tire)
                {

                    #region Inserting the new TireGUI object to the list of objects
                    tireGUI.Insert(l_tire, new TireGUI());
                    #endregion

                    #region Invoking the Default_Values class' method to populate the TireGUI table
                    Default_Values.TireDefaultValues2(tireGUI[l_tire]);
                    #endregion

                    #region Updating the TireGUI object
                    tireGUI[l_tire].Update_TireGUI(this, l_tire);
                    #endregion

                    #region Adding the new Tire object to the List of Tire objects
                    T1_Global.CreateNewTire(l_tire, tireGUI[l_tire]);
                    Tire.CurrentTireID = Tire.Assy_List_Tire[l_tire].TireID;
                    #endregion

                    #region Initializing the DataGrid for the Tire
                    tireGUI[l_tire].bandedGridView_TireGUI = CustomBandedGridView.CreateNewBandedGridView(l_tire, 2, "Tire Properties");
                    gridControl2.DataSource = tireGUI[l_tire].TireDataTableGUI;
                    gridControl2.MainView = tireGUI[l_tire].bandedGridView_TireGUI;
                    tireGUI[l_tire].bandedGridView_TireGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    tireGUI[l_tire].bandedGridView_TireGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor);
                    tireGUI[l_tire].bandedGridView_TireGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    tireGUI[l_tire].bandedGridView_TireGUI.OptionsNavigation.AutoMoveRowFocus = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(Tire.Assy_List_Tire[l_tire]._UndocommandsTire, Tire.Assy_List_Tire[l_tire]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[l_tire].TireIsModified);
                    #endregion

                    ChangeTracker++;
                    break;

                }
            }

            Tire.TireCounter++; // This is a static counter and it keeps track of the number of tire items created. It is a zero based counter
            ComboboxTireOperations();
        }


        #region Delete
        //private void TireTextBox_Leave(object sender, EventArgs e)
        //{
        //    int index = navBarGroupTireStiffness.SelectedLinkIndex;

        //    #region GUI Operation to change the color of the Text Box to white in case invalid User input was entered
        //    TireRateFL.BackColor = Color.White;
        //    TireWidthFL.BackColor = Color.White;
        //    #endregion

        //    #region Creating a Object of Tire which will be added to the List of Tire Objects

        //    tireGUI.Update_TireGUI(this);
        //    Tire tire_list = new Tire(tireGUI);
        //    T1_Global = tire_list;
        //    #endregion

        //    for (int l_tire = 0; l_tire <= Tire.Assy_List_Tire.Count; l_tire++)
        //    {
        //        if (index == l_tire)
        //        {
        //            #region Editing the Tire object to the List of Tire objects
        //            T1_Global.ModifyObjectData(l_tire, tire_list, false);
        //            Tire.CurrentTireID = Tire.Assy_List_Tire[l_tire].TireID;
        //            UndoObject.Identifier(Tire.Assy_List_Tire[l_tire]._UndocommandsTire, Tire.Assy_List_Tire[l_tire]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[l_tire].TireIsModified);
        //            break;
        //            #endregion
        //        }
        //    }
        //ComboboxTireOperations();
        //// Counter is not incremented here because in this code block a new item is not being created, it is only being edited. 
        //}

        //private void TireTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int index = navBarGroupTireStiffness.SelectedLinkIndex;

        //    #region GUI Operation to change the color of the Text Box to white in case invalid User input was entered
        //    TireRateFL.BackColor = Color.White;
        //    TireWidthFL.BackColor = Color.White;
        //    #endregion

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        #region Creating a Object of Tire which will be added to the List of Tire Objects

        //        tireGUI.Update_TireGUI(this);
        //        Tire tire_list = new Tire(tireGUI);
        //        T1_Global = tire_list;
        //        #endregion

        //        for (int l_tire = 0; l_tire <= Tire.Assy_List_Tire.Count; l_tire++)
        //        {
        //            if (index == l_tire)
        //            {

        //                #region Editing the Tire object to the List of Tire objects
        //                T1_Global.ModifyObjectData(l_tire, tire_list, false);
        //                Tire.CurrentTireID = Tire.Assy_List_Tire[l_tire].TireID;
        //                UndoObject.Identifier(Tire.Assy_List_Tire[l_tire]._UndocommandsTire, Tire.Assy_List_Tire[l_tire]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[l_tire].TireIsModified);
        //                break;
        //                #endregion

        //            }
        //        }
        //        ComboboxTireOperations();
        //        // Counter is not incremented here because in this code block a new item is not being created, it is only being edited. 
        //    }

        //} 
        #endregion

        void navBarItemTire1_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupTireStiffness.SelectedLinkIndex;
            Tire.CurrentTireID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(Tire.Assy_List_Tire[index]._UndocommandsTire, Tire.Assy_List_Tire[index]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[index].TireIsModified);
            #endregion

            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlTireStiffness.Show();
            //navBarGroupTireStiffness.Expanded = true;
            //navBarGroupDesign.Visible = true;
            //accordionControlTireStiffness.ExpandElement(accordionControlTire1TireStiffness);
            //accordionControlTireStiffness.ExpandElement(accordionControlTire1TireWidth);
            #endregion

            try
            {
                for (int c_tire = 0; c_tire <= navBarItemTireClass.navBarItemTire.Count; c_tire++)
                {
                    if (index == c_tire)
                    {
                        #region Displaying to the user the input items that have been created
                        gridControl2.MainView = tireGUI[c_tire].bandedGridView_TireGUI;
                        gridControl2.DataSource = Tire.Assy_List_Tire[c_tire].TireDataTable;
                        tireGUI[c_tire].TireDataTableGUI = Tire.Assy_List_Tire[c_tire].TireDataTable;
                        #endregion
                    }

                }
            }
            catch (Exception)
            {
            }
        }
        private void ComboboxTireOperations()
        {
            int indexFL = comboBoxTireFL.SelectedIndex;
            int indexFR = comboBoxTireFR.SelectedIndex;
            int indexRL = comboBoxTireRL.SelectedIndex;
            int indexRR = comboBoxTireRR.SelectedIndex;

            #region Clearing out the Comboboxes
            comboBoxTireFL.Items.Clear();
            comboBoxTireFR.Items.Clear();
            comboBoxTireRL.Items.Clear();
            comboBoxTireRR.Items.Clear();
            #endregion

            for (int i_combobox_tire = 0; i_combobox_tire < Tire.Assy_List_Tire.Count; i_combobox_tire++)
            {
                try
                {
                    #region Poplulating the Comboboxes with the List of Tire Objects
                    comboBoxTireFL.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireFL.DisplayMember = "_TireName";


                    comboBoxTireFR.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireFR.DisplayMember = "_TireName";


                    comboBoxTireRL.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireRL.DisplayMember = "_TireName";


                    comboBoxTireRR.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireRR.DisplayMember = "_TireName";

                    //CheckTireComboboxes(i_combobox_tire);

                    #endregion
                }

                catch (Exception) { }
            }

            #region Re-assigning the combobox selected item index
            try
            {
                if (indexFL != -1)
                {
                    comboBoxTireFL.SelectedIndex = indexFL;
                }
                else comboBoxTireFL.SelectedIndex = 0;

                if (indexFR != -1)
                {
                    comboBoxTireFR.SelectedIndex = indexFR;
                }
                else comboBoxTireFR.SelectedIndex = 0;

                if (indexRL != -1)
                {
                    comboBoxTireRL.SelectedIndex = indexRL;
                }
                else comboBoxTireRL.SelectedIndex = 0;
                if (indexRR != -1)
                {
                    comboBoxTireRR.SelectedIndex = indexRR;
                }
                else comboBoxTireRR.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox 
            }
            #endregion
        }

        #endregion

        //
        //Spring Item Creation and GUI
        //
        #region Spring Item Creation and GUI

        List<SpringGUI> springGUI = new List<SpringGUI>();

        private void BarButtonSpring_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlSprings.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSprings.Expanded = true;
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringRate);
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringPreload);
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringFreeLength);

            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_spring = 0; i_spring <= navBarItemSpringClass.navBarItemSpring.Count; i_spring++)
            {
                if (Spring.SpringCounter == i_spring)
                {
                    #region Creating a new NavBarItem and adding it the Spring Group
                    navBarItemSpringClass temp_navBarItemSpring = new navBarItemSpringClass();
                    navBarSpring_Global.CreateNewNavBarItem(i_spring, temp_navBarItemSpring, navBarControlDesign, navBarGroupSprings);
                    navBarItemSpringClass.navBarItemSpring[i_spring].LinkClicked += new NavBarLinkEventHandler(navBarItemSpring_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_spring = 0; l_spring <= Spring.Assy_List_Spring.Count; l_spring++)
            {
                if (Spring.SpringCounter == l_spring)
                {
                    #region Inserting a new SpringGUI object to the list of objects
                    springGUI.Insert(l_spring, new SpringGUI());
                    #endregion

                    #region Invoking the Default_Values Class's method to popluate the SpringGUI Data Table
                    Default_Values.SpringDefaultValues2(springGUI[l_spring]);
                    #endregion

                    #region Populating the TireGUI Object
                    springGUI[l_spring].Update_SpringGUI(this, l_spring);
                    #endregion

                    #region Adding a new Spring to List of Spring Objects
                    S1_Global.CreateNewSpring(l_spring, springGUI[l_spring]);
                    Spring.CurrentSpringID = Spring.Assy_List_Spring[l_spring].SpringID;
                    #endregion

                    #region Initializing the DataGrid for the Spring
                    springGUI[l_spring].bandedGridView_SpringGUI = CustomBandedGridView.CreateNewBandedGridView(l_spring, 2, "Spring Properties");
                    gridControl2.DataSource = springGUI[l_spring].SpringDataTableGUI;
                    gridControl2.MainView = springGUI[l_spring].bandedGridView_SpringGUI;
                    springGUI[l_spring].bandedGridView_SpringGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    springGUI[l_spring].bandedGridView_SpringGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor_Spring);
                    springGUI[l_spring].bandedGridView_SpringGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack
                    UndoObject.Identifier(Spring.Assy_List_Spring[l_spring]._UndocommandsSpring, Spring.Assy_List_Spring[l_spring]._RedocommandsSpring, Spring.CurrentSpringID, Spring.Assy_List_Spring[l_spring].SpringIsModified);
                    #endregion

                    ChangeTracker++;
                    break;
                }
            }

            Spring.SpringCounter++;// This is a static counter and it keeps track of the number of Spring items created
            ComboBoxSpringOperations();

        }

        #region Data Validation for Spring Inputs
        void bandedGridView_ValidatingEditor_Spring(object sender, BaseContainerValidateEditorEventArgs e)
        {
            double checker = 0;
            if (!Double.TryParse(e.Value as string, out checker))
            {
                e.Valid = false;
                e.ErrorText = "Please enter numeric values";
            }
            else if (Convert.ToDouble(e.Value) <= 0)
            {
                ColumnView view = (ColumnView)gridControl2.FocusedView;
                GridColumn column = view.Columns["Column 1"];
                if (view.FocusedRowHandle != 1)
                {

                    e.Valid = false;
                    e.ErrorText = "Please enter positive values";
                }
            }
        }
        #endregion

        void navBarItemSpring_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSprings.SelectedLinkIndex;
            Spring.CurrentSpringID = index + 1;
            UndoObject.Identifier(Spring.Assy_List_Spring[index]._UndocommandsSpring, Spring.Assy_List_Spring[index]._RedocommandsSpring, Spring.CurrentSpringID, Spring.Assy_List_Spring[index].SpringIsModified);

            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlSprings.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupSprings.Expanded = true;
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringRate);
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringPreload);
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringFreeLength);
            #endregion

            try
            {
                for (int c_spring = 0; c_spring <= navBarItemSpringClass.navBarItemSpring.Count; c_spring++)
                {
                    if (index == c_spring)
                    {
                        #region Displaying to the user the items that have been created
                        gridControl2.MainView = springGUI[c_spring].bandedGridView_SpringGUI;
                        gridControl2.DataSource = Spring.Assy_List_Spring[c_spring].SpringDataTable;
                        springGUI[c_spring].SpringDataTableGUI = Spring.Assy_List_Spring[c_spring].SpringDataTable;
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ComboBoxSpringOperations()
        {
            int indexFL = comboBoxSpringFL.SelectedIndex;
            int indexFR = comboBoxSpringFR.SelectedIndex;
            int indexRL = comboBoxSpringRL.SelectedIndex;
            int indexRR = comboBoxSpringRR.SelectedIndex;

            #region Clearing out the Comboboxes
            comboBoxSpringFL.Items.Clear();
            comboBoxSpringFR.Items.Clear();
            comboBoxSpringRL.Items.Clear();
            comboBoxSpringRR.Items.Clear();
            #endregion

            for (int i_combobox_spring = 0; i_combobox_spring < Spring.Assy_List_Spring.Count; i_combobox_spring++)
            {
                try
                {
                    #region Populating the comboboxes with the lit of the Spring Objects
                    comboBoxSpringFL.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringFL.DisplayMember = "_SpringName";


                    comboBoxSpringFR.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringFR.DisplayMember = "_SpringName";


                    comboBoxSpringRL.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringRL.DisplayMember = "_SpringName";


                    comboBoxSpringRR.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringRR.DisplayMember = "_SpringName";

                    //CheckSpringComboboxes(i_combobox_spring);

                    #endregion
                }
                catch (Exception) { }
            }

            #region Re-assigning the combobox selected item index
            try
            {
                if (indexFL != -1)
                {
                    comboBoxSpringFL.SelectedIndex = indexFL;
                }
                else comboBoxSpringFL.SelectedIndex = 0;

                if (indexFR != -1)
                {
                    comboBoxSpringFR.SelectedIndex = indexFR;
                }
                else comboBoxSpringFR.SelectedIndex = 0;

                if (indexRL != -1)
                {
                    comboBoxSpringRL.SelectedIndex = indexRL;
                }
                else comboBoxSpringRL.SelectedIndex = 0;

                if (indexRR != -1)
                {
                    comboBoxSpringRR.SelectedIndex = indexRR;
                }
                else comboBoxSpringRR.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
            }
            #endregion

        }
        #endregion

        //
        // Damper Item Creation and Gui
        // 
        #region Damper Item Creation and GUI

        List<DamperGUI> damperGUI = new List<DamperGUI>();

        private void BarButtonDamper_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlDamper.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupDamper.Expanded = true;
            //accordionControlDamper.ExpandElement(accordionControlDamperGasPressure);
            //accordionControlDamper.ExpandElement(accordionControlDamperShaftDiameter);



            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_damper = 0; i_damper <= navbarItemDamperClass.navBarItemDamper.Count; i_damper++)
            {
                if (Damper.DamperCounter == i_damper)
                {
                    #region Creating a new navBarItem and adding it to the Damper Group
                    navbarItemDamperClass temp_navBarItemDamper = new navbarItemDamperClass();
                    navBarDamper_Global.CreateNewNavBarItem(i_damper, temp_navBarItemDamper, navBarControlDesign, navBarGroupDamper);
                    navbarItemDamperClass.navBarItemDamper[i_damper].LinkClicked += new NavBarLinkEventHandler(navBarItemDamper_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_damper = 0; l_damper <= Damper.Assy_List_Damper.Count; l_damper++)
            {
                if (Damper.DamperCounter == l_damper)
                {
                    #region Inserting new DamperGUI object into the list of objects
                    damperGUI.Insert(l_damper, new DamperGUI());
                    #endregion

                    #region Invoking the Default_Values class' method to populate the DamperGUI table
                    Default_Values.DamperDefaultValues2(damperGUI[l_damper]);
                    #endregion

                    #region Populating the DameprGUI object
                    damperGUI[l_damper].Update_DamperGUI(this, l_damper);
                    #endregion

                    #region Adding the new Damper object to the List of Damper objects
                    D1_Global.CreateNewDamper(l_damper, damperGUI[l_damper]);
                    Damper.CurrentDamperID = Damper.Assy_List_Damper[l_damper].DamperID;
                    #endregion

                    #region Initializing the Data Grid View for the Damper
                    damperGUI[l_damper].bandedGridView_DamperGUI = CustomBandedGridView.CreateNewBandedGridView(l_damper, 2, " Damper Properties");
                    gridControl2.DataSource = damperGUI[l_damper].DamperDataTableGUI;
                    gridControl2.MainView = damperGUI[l_damper].bandedGridView_DamperGUI;
                    damperGUI[l_damper].bandedGridView_DamperGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    damperGUI[l_damper].bandedGridView_DamperGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor);
                    damperGUI[l_damper].bandedGridView_DamperGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(Damper.Assy_List_Damper[l_damper]._UndocommandsDamper, Damper.Assy_List_Damper[l_damper]._RedocommandsDamper, Damper.CurrentDamperID, Damper.Assy_List_Damper[l_damper].DamperIsModified);
                    #endregion

                    ChangeTracker++;

                    break;
                }
            }

            Damper.DamperCounter++; // This is a static counter which keeps track of the number of Damper items created. It is a zero based counter
            ComboboxDamperOperations();

        }



        #region Delete
        //private void DamperTextBox_Leave(object sender, EventArgs e)
        //{

        //}

        //private void DamperTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int index = navBarGroupDamper.SelectedLinkIndex;

        //    #region GUI operatin to change the color of the textbox back to white if invalid input is entered by the user
        //    DamperGasPressureFL.BackColor = Color.White;
        //    DamperShaftDiaFL.BackColor = Color.White;
        //    #endregion

        //    if (e.KeyCode == Keys.Enter)
        //    {

        //        #region Creating an object of Damper which will be added to the list of damper objects

        //        DamperGUI damperGUI = new DamperGUI(this);
        //        Damper damper_list = new Damper(damperGUI);
        //        D1_Global = damper_list;
        //        #endregion

        //        for (int l_damper = 0; l_damper <= Damper.Assy_List_Damper.Count; l_damper++)
        //        {

        //            if (index == l_damper)
        //            {
        //                #region Adding the new damper to the list of damper objects
        //                D1_Global.ModifyObjectData(l_damper, damper_list, false);
        //                Damper.CurrentDamperID = Damper.Assy_List_Damper[l_damper].DamperID;
        //                UndoObject.Identifier(Damper.Assy_List_Damper[l_damper]._UndocommandsDamper, Damper.Assy_List_Damper[l_damper]._RedocommandsDamper, Damper.CurrentDamperID, Damper.Assy_List_Damper[l_damper].DamperIsModified);
        //                break;
        //                #endregion
        //            }
        //        }
        //        ComboboxDamperOperations();
        //        // Counter is not incremented here because in this code block a new damperitem is not being created, it is only being edited. 
        //    }

        //} 
        #endregion

        void navBarItemDamper_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupDamper.SelectedLinkIndex;
            Damper.CurrentDamperID = index + 1;
            UndoObject.Identifier(Damper.Assy_List_Damper[index]._UndocommandsDamper, Damper.Assy_List_Damper[index]._RedocommandsDamper, Damper.CurrentDamperID, Damper.Assy_List_Damper[index].DamperIsModified);

            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlDamper.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupDamper.Expanded = true;
            //accordionControlDamper.ExpandElement(accordionControlDamperGasPressure);
            //accordionControlDamper.ExpandElement(accordionControlDamperShaftDiameter);
            #endregion

            for (int c_damper = 0; c_damper <= navbarItemDamperClass.navBarItemDamper.Count; c_damper++)
            {
                if (index == c_damper)
                {
                    #region Displaying to the user the input items that have been created
                    gridControl2.MainView = damperGUI[c_damper].bandedGridView_DamperGUI;
                    gridControl2.DataSource = Damper.Assy_List_Damper[c_damper].DamperDataTable;
                    damperGUI[c_damper].DamperDataTableGUI = Damper.Assy_List_Damper[c_damper].DamperDataTable;
                    #endregion
                }
            }
        }
        private void ComboboxDamperOperations()
        {
            int indexFL = comboBoxDamperFL.SelectedIndex;
            int indexFR = comboBoxDamperFR.SelectedIndex;
            int indexRL = comboBoxDamperRL.SelectedIndex;
            int indexRR = comboBoxDamperRR.SelectedIndex;

            #region Clearing out the comboboxes
            comboBoxDamperFL.Items.Clear();
            comboBoxDamperFR.Items.Clear();
            comboBoxDamperRL.Items.Clear();
            comboBoxDamperRR.Items.Clear();
            #endregion

            for (int i_combobox_damper = 0; i_combobox_damper < Damper.Assy_List_Damper.Count; i_combobox_damper++)
            {
                try
                {
                    #region Populating the comboboxes with the list of damper objects
                    comboBoxDamperFL.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperFL.DisplayMember = "_DamperName";


                    comboBoxDamperFR.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperFR.DisplayMember = "_DamperName";


                    comboBoxDamperRL.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperRL.DisplayMember = "_DamperName";


                    comboBoxDamperRR.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperRR.DisplayMember = "_DamperName";

                    //CheckDamperComboboxes(i_combobox_damper);

                    #endregion
                }
                catch (Exception)
                {
                }
            }

            #region Re-assigning the combobox selected item index
            try
            {
                if (indexFL != -1)
                {
                    comboBoxDamperFL.SelectedIndex = indexFL;
                }
                else comboBoxDamperFL.SelectedIndex = 0;

                if (indexFR != -1)
                {
                    comboBoxDamperFR.SelectedIndex = indexFR;
                }
                else comboBoxDamperFR.SelectedIndex = 0;

                if (indexRL != -1)
                {
                    comboBoxDamperRL.SelectedIndex = indexRL;
                }
                else comboBoxDamperRL.SelectedIndex = 0;

                if (indexRR != -1)
                {
                    comboBoxDamperRR.SelectedIndex = indexRR;
                }
                else comboBoxDamperRR.SelectedIndex = 0;
            }
            catch (Exception)
            { // To safeguard against Open command if there is no item in combobox
            }
            #endregion

        }
        #endregion

        //
        // Anti-Roll Bar Item Creation and GUI
        //
        #region ARB Item Creation and GUI

        List<AntiRollBarGUI> arbGUI = new List<AntiRollBarGUI>();

        private void BarButtonARB_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlAntiRollBar.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupAntiRollBar.Expanded = true;
            //accordionControlAntiRollBar.ExpandElement(accordionControlAntiRollBarStiffness);



            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_arb = 0; i_arb <= navBarItemARBClass.navBarItemARB.Count; i_arb++)
            {
                if (AntiRollBar.AntiRollBarCounter == i_arb)
                {
                    #region Creating a new NavBarItem and adding it to the AntiRollBar Group
                    navBarItemARBClass temp_navBarItemARB = new navBarItemARBClass();
                    temp_navBarItemARB.CreateNewNavBarItem(i_arb, temp_navBarItemARB, navBarControlDesign, navBarGroupAntiRollBar);
                    navBarItemARBClass.navBarItemARB[i_arb].LinkClicked += new NavBarLinkEventHandler(navBarItemARB_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_arb = 0; l_arb <= AntiRollBar.Assy_List_ARB.Count; l_arb++)
            {
                if (AntiRollBar.AntiRollBarCounter == l_arb)
                {

                    #region Inserting a new AntiRollBarGUI object into the list of objects
                    arbGUI.Insert(l_arb, new AntiRollBarGUI());
                    #endregion

                    #region Invoking the Default_Values class' method to populate the TireGUI table
                    Default_Values.ARBDefaultValues2(arbGUI[l_arb]);
                    #endregion

                    #region Modifiying the AntiRollBarGUI object
                    arbGUI[l_arb].Update_ARBGUI(this, l_arb);
                    #endregion

                    #region Adding the new AntiRollBar object to the list AntiRollBar Objects
                    A1_Global.CreateNewARB(l_arb, arbGUI[l_arb]);
                    AntiRollBar.CurrentAntiRollBarID = AntiRollBar.Assy_List_ARB[l_arb].AntiRollBarID;
                    #endregion

                    #region Initializing the Data Grid for AntiRollBar
                    arbGUI[l_arb].bandedGridView_ARBGUI = CustomBandedGridView.CreateNewBandedGridView(l_arb, 2, "Anti-Roll Bar Properties");
                    gridControl2.DataSource = arbGUI[l_arb].ARBDataTableGUI;
                    gridControl2.MainView = arbGUI[l_arb].bandedGridView_ARBGUI;
                    arbGUI[l_arb].bandedGridView_ARBGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    arbGUI[l_arb].bandedGridView_ARBGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor);
                    arbGUI[l_arb].bandedGridView_ARBGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populate Data Table Method
                    UndoObject.Identifier(AntiRollBar.Assy_List_ARB[l_arb]._UndocommandsARB, AntiRollBar.Assy_List_ARB[l_arb]._RedocommandsARB, AntiRollBar.CurrentAntiRollBarID, AntiRollBar.Assy_List_ARB[l_arb].AntiRollBarIsModified);
                    #endregion

                    ChangeTracker++;
                    break;
                }

            }
            AntiRollBar.AntiRollBarCounter++;
            ComboboxARBOperations();

        }

        #region Delete
        //private void ARBTextBox_Leave(object sender, EventArgs e)
        //{



        //}

        //private void ARBTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int index = navBarGroupAntiRollBar.SelectedLinkIndex;

        //    #region GUI Operation to change thr color of the Text Box to white in case invalid user input is enteres
        //    AntiRollBarRateFront.BackColor = Color.White;
        //    #endregion

        //    if (e.KeyCode == Keys.Enter)
        //    {

        //        #region Creating a new object of AntiRollBar which will be added to the list of the AntiRollBar Objects

        //        AntiRollBarGUI arbGUI = new AntiRollBarGUI(this);
        //        AntiRollBar arb_list = new AntiRollBar(arbGUI);
        //        A1_Global = arb_list;
        //        #endregion

        //        for (int l_arb = 0; l_arb < AntiRollBar.Assy_List_ARB.Count; l_arb++)
        //        {
        //            if (index == l_arb)
        //            {
        //                #region Adding the new AntiRollBar object to the list AntiRollBar Objects
        //                A1_Global.ModifyObjectData(l_arb, arb_list, false);
        //                AntiRollBar.CurrentAntiRollBarID = AntiRollBar.Assy_List_ARB[l_arb].AntiRollBarID;
        //                UndoObject.Identifier(AntiRollBar.Assy_List_ARB[l_arb]._UndocommandsARB, AntiRollBar.Assy_List_ARB[l_arb]._RedocommandsARB, AntiRollBar.CurrentAntiRollBarID, AntiRollBar.Assy_List_ARB[l_arb].AntiRollBarIsModified);
        //                break;
        //                #endregion
        //            }

        //        }
        //        ComboboxARBOperations();
        //        // Counter is not incremented here because in this code a new ARB item is not being created, it is only being edited

        //    }
        //} 
        #endregion

        void navBarItemARB_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupAntiRollBar.SelectedLinkIndex;
            AntiRollBar.CurrentAntiRollBarID = index + 1;

            #region Populating the Undo/Redo Stack of the Undo Redo Class
            UndoObject.Identifier(AntiRollBar.Assy_List_ARB[index]._UndocommandsARB, AntiRollBar.Assy_List_ARB[index]._RedocommandsARB, AntiRollBar.CurrentAntiRollBarID, AntiRollBar.Assy_List_ARB[index].AntiRollBarIsModified);
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlAntiRollBar.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupAntiRollBar.Expanded = true;
            //accordionControlAntiRollBar.ExpandElement(accordionControlAntiRollBarStiffness);
            #endregion

            for (int c_arb = 0; c_arb <= navBarItemARBClass.navBarItemARB.Count; c_arb++)
            {
                if (index == c_arb)
                {
                    #region Displaying to the user the input items that have been created
                    gridControl2.MainView = arbGUI[c_arb].bandedGridView_ARBGUI;
                    gridControl2.DataSource = AntiRollBar.Assy_List_ARB[c_arb].ARBDataTable;
                    arbGUI[c_arb].ARBDataTableGUI = AntiRollBar.Assy_List_ARB[c_arb].ARBDataTable;
                    #endregion
                }
            }
        }

        private void ComboboxARBOperations()
        {
            int indexFront = comboBoxARBFront.SelectedIndex;
            int indexRear = comboBoxARBRear.SelectedIndex;
            #region Clearing out the Comboboxes
            comboBoxARBFront.Items.Clear();
            comboBoxARBRear.Items.Clear();
            #endregion

            for (int i_combobox_arb = 0; i_combobox_arb < AntiRollBar.Assy_List_ARB.Count; i_combobox_arb++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of ARB Objects
                    comboBoxARBFront.Items.Insert(i_combobox_arb, AntiRollBar.Assy_List_ARB[i_combobox_arb]);
                    comboBoxARBFront.DisplayMember = "_ARBName";


                    comboBoxARBRear.Items.Insert(i_combobox_arb, AntiRollBar.Assy_List_ARB[i_combobox_arb]);
                    comboBoxARBRear.DisplayMember = "_ARBName";

                    #endregion
                }
                catch (Exception) { }
            }


            #region Re-assigning the combobox selected item index
            try
            {
                if (indexFront != -1)
                {
                    comboBoxARBFront.SelectedIndex = indexFront;
                }
                else comboBoxARBFront.SelectedIndex = 0;

                if (indexRear != -1)
                {
                    comboBoxARBRear.SelectedIndex = indexRear;
                }
                else comboBoxARBRear.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
            }
            #endregion
        }
        #endregion

        //
        // Chassis item Creation and GUI
        //
        #region Chassis Item Creation and GUI

        public List<ChassisGUI> chassisGUI = new List<ChassisGUI>();

        private void BarButtonChassis_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlChassis.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupChassis.Expanded = true;
            //accordionControlChassis.ExpandElement(accordionControlSuspendedMassExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlSuspendedMassCGExpectedCG);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass2ExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass3ExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass4ExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationFL);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationFR);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationRL);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationRR);
            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_chassis = 0; i_chassis <= navBarItemChassisClass.navBarItemChassis.Count; i_chassis++)
            {
                if (Chassis.ChassisCounter == i_chassis)
                {
                    #region Creating a new NavBarItem and adding it to the Chassis Group
                    navBarItemChassisClass temp_navBarItemChassis = new navBarItemChassisClass();
                    navBarChassis_Global.CreateNewNavBarItem(i_chassis, temp_navBarItemChassis, navBarControlDesign, navBarGroupChassis);
                    navBarItemChassisClass.navBarItemChassis[i_chassis].LinkClicked += new NavBarLinkEventHandler(navBarItemChassis_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_chassis = 0; l_chassis <= Chassis.Assy_List_Chassis.Count; l_chassis++)
            {
                try
                {
                    if (Chassis.ChassisCounter == l_chassis)
                    {

                        #region Creating an object of chassis whcih will be added to the list of chassis objects
                        chassisGUI.Insert(l_chassis, new ChassisGUI(this));
                        #endregion

                        #region Invoking the Default_Values class' method to populate the ChassisGUI table
                        Default_Values.ChassisDefaultValues.MassAndSMCoGDefaultValues(chassisGUI[l_chassis], this);
                        Default_Values.ChassisDefaultValues.FRONTLEFTNonSuspendedMassCoGValues(chassisGUI[l_chassis], this);
                        Default_Values.ChassisDefaultValues.FRONTRIGHTNonSuspendedMassCoGValues(chassisGUI[l_chassis], this);
                        Default_Values.ChassisDefaultValues.REARLEFTNonSuspendedMassCoGValues(chassisGUI[l_chassis], this);
                        Default_Values.ChassisDefaultValues.REARRIGHTNonSuspendedMassCoGValues(chassisGUI[l_chassis], this);
                        #endregion

                        #region Updating the ChassisGUI Object
                        chassisGUI[l_chassis].Update_ChassisGUI(this, l_chassis);
                        #endregion

                        #region Adding the new Chassis object to the list Chassis Objects
                        C1_Global.CreateNewChassis(l_chassis, chassisGUI[l_chassis]);
                        Chassis.CurrentChassisID = Chassis.Assy_List_Chassis[l_chassis].ChassisID;
                        #endregion

                        #region Initializing the DataGrid for the Chassis
                        chassisGUI[l_chassis].bandedGridViewChassis = CustomBandedGridView.CreateNewBandedGridView(l_chassis, 5, "Chassis Properties");
                        gridControl2.DataSource = chassisGUI[l_chassis].ChassisDataTableGUI;
                        gridControl2.MainView = chassisGUI[l_chassis].bandedGridViewChassis;
                        chassisGUI[l_chassis].bandedGridViewChassis.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                        chassisGUI[l_chassis].bandedGridViewChassis.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridViewChassis_ValidatingEditor);
                        chassisGUI[l_chassis].bandedGridViewChassis.OptionsView.ShowColumnHeaders = true;
                        chassisGUI[l_chassis].bandedGridViewChassis = CustomBandedGridColumn.ColumnEditor_ForChassis(chassisGUI[l_chassis].bandedGridViewChassis);
                        chassisGUI[l_chassis].bandedGridViewChassis.OptionsNavigation.EnterMoveNextColumn = true;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(Chassis.Assy_List_Chassis[l_chassis]._UndocommandsChassis, Chassis.Assy_List_Chassis[l_chassis]._RedocommandsChassis, Chassis.CurrentChassisID, Chassis.Assy_List_Chassis[l_chassis].ChassisIsModified);
                        #endregion

                        ChangeTracker++;
                        break;

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please Create Suspension First");
                    navBarGroupChassis.ItemLinks.Remove(navBarItemChassisClass.navBarItemChassis[l_chassis]);
                    navBarControlDesign.Items.Remove(navBarItemChassisClass.navBarItemChassis[l_chassis]);
                    navBarItemChassisClass.navBarItemChassis.RemoveAt(l_chassis);
                    sidePanel2.Hide();
                    return;
                }

            }
            Chassis.ChassisCounter++; // This is a static counter and it keeps track of the number of Chassis items created
            ComboboxChassisOperations();
        }

        #region Data Validation for Chassis Inputs
        void bandedGridViewChassis_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            double checker = 0;
            if (!Double.TryParse(e.Value as string, out checker))
            {
                e.Valid = false;
                e.ErrorText = "Please enter numeric values";
            }
            else if (Convert.ToDouble(e.Value) <= 0)
            {
                ColumnView view = (ColumnView)gridControl2.FocusedView;
                GridColumn column = view.Columns["Mass (Kg)"];
                if (view.FocusedColumn.Caption == "Mass (Kg)")
                {
                    e.Valid = false;
                    e.ErrorText = "Please enter positive values";
                }
            }
        }
        #endregion

        #region Delete
        //private void ChassisTextBox_Leave(object sender, EventArgs e)
        //{


        //}

        //private void ChassisTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int index = navBarGroupChassis.SelectedLinkIndex;

        //    #region GUI Operation to change thr color of the Text Box to white in case invalid user input is entered
        //    SuspendedMass.BackColor = Color.White;
        //    NonSuspendedMassFL.BackColor = Color.White;
        //    NonSuspendedMassFR.BackColor = Color.White;
        //    NonSuspendedMassRL.BackColor = Color.White;
        //    NonSuspendedMassRR.BackColor = Color.White;

        //    SMCGx.BackColor = Color.White;
        //    SMCGy.BackColor = Color.White;
        //    SMCGz.BackColor = Color.White;

        //    NSMCGFLx.BackColor = Color.White;
        //    NSMCGFLy.BackColor = Color.White;
        //    NSMCGFLz.BackColor = Color.White;
        //    NSMCGFRx.BackColor = Color.White;
        //    NSMCGFRy.BackColor = Color.White;
        //    NSMCGFRz.BackColor = Color.White;
        //    NSMCGRLx.BackColor = Color.White;
        //    NSMCGRLy.BackColor = Color.White;
        //    NSMCGRLz.BackColor = Color.White;
        //    NSMCGRRx.BackColor = Color.White;
        //    NSMCGRRy.BackColor = Color.White;
        //    NSMCGRRz.BackColor = Color.White;
        //    #endregion

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        #region Creating an object of chassis whcih will be added to the list of chassis objects

        //        ChassisGUI chassisGUI = new ChassisGUI(this);
        //        Chassis chassis_list = new Chassis(chassisGUI);
        //        C1_Global = chassis_list;
        //        #endregion

        //        for (int l_chassis = 0; l_chassis <= Chassis.Assy_List_Chassis.Count; l_chassis++)
        //        {
        //            if (index == l_chassis)
        //            {
        //                #region Adding the new Chassis object to the list Chassis Objects
        //                UndoObject.Identifier(Chassis.Assy_List_Chassis[l_chassis]._UndocommandsChassis, Chassis.Assy_List_Chassis[l_chassis]._RedocommandsChassis, Chassis.CurrentChassisID, Chassis.Assy_List_Chassis[l_chassis].ChassisIsModified);
        //                break;
        //                #endregion

        //            }
        //        }
        //        // Counter is not incremented here because in this code a new Chassis item is not being created, it is only being edited
        //    }

        //} 
        #endregion


        void navBarItemChassis_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupChassis.SelectedLinkIndex;
            Chassis.CurrentChassisID = index + 1;
            UndoObject.Identifier(Chassis.Assy_List_Chassis[index]._UndocommandsChassis, Chassis.Assy_List_Chassis[index]._RedocommandsChassis, Chassis.CurrentChassisID, Chassis.Assy_List_Chassis[index].ChassisIsModified);

            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlChassis.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupChassis.Expanded = true;
            //accordionControlChassis.ExpandElement(accordionControlSuspendedMassExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlSuspendedMassCGExpectedCG);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass2ExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass3ExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass4ExpectedMass);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationFL);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationFR);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationRL);
            //accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationRR);
            #endregion

            for (int c_chassis = 0; c_chassis <= navBarItemChassisClass.navBarItemChassis.Count; c_chassis++)
            {
                if (index == c_chassis)
                {
                    #region Displaying to the user the input items that have been created
                    gridControl2.MainView = chassisGUI[c_chassis].bandedGridViewChassis;
                    gridControl2.DataSource = Chassis.Assy_List_Chassis[c_chassis].ChassisDataTable;
                    chassisGUI[c_chassis].ChassisDataTableGUI = Chassis.Assy_List_Chassis[c_chassis].ChassisDataTable;
                    chassisGUI[c_chassis].bandedGridViewChassis.ExpandAllGroups();
                    #endregion

                }
            }
        }

        private void ComboboxChassisOperations()
        {
            int index = comboBoxChassis.SelectedIndex;

            #region Clearing out the Comboboxes
            comboBoxChassis.Items.Clear();
            #endregion

            for (int i_combobox_chassis = 0; i_combobox_chassis < Chassis.Assy_List_Chassis.Count; i_combobox_chassis++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of Chassis Objects
                    comboBoxChassis.Items.Insert(i_combobox_chassis, Chassis.Assy_List_Chassis[i_combobox_chassis]);
                    comboBoxChassis.DisplayMember = "_ChassisName";

                    //CheckChassisComboboxes(i_combobox_chassis);

                    #endregion

                }
                catch (Exception)
                {
                }
            }


            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    comboBoxChassis.SelectedIndex = index;
                }
                else comboBoxChassis.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
            }
            #endregion
        }
        #endregion

        //
        // Wheel Alignment Item Creation and GUI
        //
        #region Wheel Alignment Item Creation and GUI

        public List<WheelAlignmentGUI> waGUI = new List<WheelAlignmentGUI>();

        private void BarButtonWA_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlWheelAlignment.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupWheelAlignment.Expanded = true;
            //accordionControlWheelAlignment.ExpandElement(accordionControlWACamber1);
            //accordionControlWheelAlignment.ExpandElement(accordionControWAlToe1);



            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_wa = 0; i_wa <= navBarItemWAClass.navBarItemWA.Count; i_wa++)
            {
                if (WheelAlignment.WheelAlignmentCounter == i_wa)
                {
                    #region Creating a new NavBarItem and adding it to the WheelAlignment Group
                    navBarItemWAClass temp_navBarItemWA = new navBarItemWAClass();
                    navBarWA_Global.CreateNewWAItem(i_wa, temp_navBarItemWA, navBarControlDesign, navBarGroupWheelAlignment);
                    navBarItemWAClass.navBarItemWA[i_wa].LinkClicked += new NavBarLinkEventHandler(navBarItemWA_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_wa = 0; l_wa <= WheelAlignment.Assy_List_WA.Count; l_wa++)
            {
                if (WheelAlignment.WheelAlignmentCounter == l_wa)
                {
                    #region Inserting a new WheelAlignmentGUI object to the list of objects
                    waGUI.Insert(l_wa, new WheelAlignmentGUI());
                    #endregion

                    #region Invoking the Default_Values class' method to populate the TireGUI table
                    Default_Values.WheelAlignmentDefaultValues2(waGUI[l_wa]);
                    #endregion

                    #region Populating the WheelAlignmentGUI object
                    waGUI[l_wa].Update_WheelAlignmentGUI(this, l_wa);
                    #endregion

                    #region Adding the new AntiRollBar object to the list AntiRollBar Objects
                    W1_Global.CreateNewWheelAlignment(l_wa, waGUI[l_wa]);
                    WheelAlignment.CurrentWheelAlignmentID = WheelAlignment.Assy_List_WA[l_wa].WheelAlignmentID;
                    #endregion

                    #region Initializing the DataGrid for the Wheel Alignment 
                    waGUI[l_wa].bandedGridView_WAGUI = CustomBandedGridView.CreateNewBandedGridView(l_wa, 2, "Wheel Alignment");
                    gridControl2.DataSource = waGUI[l_wa].WADataTableGUI;
                    gridControl2.MainView = waGUI[l_wa].bandedGridView_WAGUI;
                    waGUI[l_wa].bandedGridView_WAGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    waGUI[l_wa].bandedGridView_WAGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(WheelAlignment.Assy_List_WA[l_wa]._UndocommandsWheelAlignment, WheelAlignment.Assy_List_WA[l_wa]._RedocommandsWheelAlignment, WheelAlignment.CurrentWheelAlignmentID, WheelAlignment.Assy_List_WA[l_wa].WheelAlignmentIsModified);
                    #endregion

                    ChangeTracker++;
                    break;

                }
            }
            WheelAlignment.WheelAlignmentCounter++; // This is a static counter and it keeps track of the number of Chassis items created
            ComboboxWheelAlignmentOperations();

        }

        void navBarItemWA_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupWheelAlignment.SelectedLinkIndex;
            WheelAlignment.CurrentWheelAlignmentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(WheelAlignment.Assy_List_WA[index]._UndocommandsWheelAlignment, WheelAlignment.Assy_List_WA[index]._RedocommandsWheelAlignment, WheelAlignment.CurrentWheelAlignmentID, WheelAlignment.Assy_List_WA[index].WheelAlignmentIsModified);
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlWheelAlignment.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupWheelAlignment.Expanded = true;
            //accordionControlWheelAlignment.ExpandElement(accordionControlWACamber1);
            //accordionControlWheelAlignment.ExpandElement(accordionControWAlToe1);
            #endregion

            for (int c_wa = 0; c_wa <= navBarItemWAClass.navBarItemWA.Count; c_wa++)
            {
                if (index == c_wa)
                {
                    #region Displaying to the user the input items that have been created
                    gridControl2.MainView = waGUI[c_wa].bandedGridView_WAGUI;
                    gridControl2.DataSource = WheelAlignment.Assy_List_WA[c_wa].WADataTable;
                    waGUI[c_wa].WADataTableGUI = WheelAlignment.Assy_List_WA[c_wa].WADataTable;
                    #endregion
                }
            }
        }

        private void ComboboxWheelAlignmentOperations()
        {
            int indexFL = comboBoxWAFL.SelectedIndex;
            int indexFR = comboBoxWAFR.SelectedIndex;
            int indexRL = comboBoxWARL.SelectedIndex;
            int indexRR = comboBoxWARR.SelectedIndex;


            #region Clearing out the Comboboxes
            comboBoxWAFL.Items.Clear();
            comboBoxWAFR.Items.Clear();
            comboBoxWARL.Items.Clear();
            comboBoxWARR.Items.Clear();
            #endregion

            for (int i_combobox_wa = 0; i_combobox_wa < WheelAlignment.Assy_List_WA.Count; i_combobox_wa++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of WheelAlignment Objects

                    comboBoxWAFL.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWAFL.DisplayMember = "_WAName";


                    comboBoxWAFR.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWAFR.DisplayMember = "_WAName";


                    comboBoxWARL.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWARL.DisplayMember = "_WAName";


                    comboBoxWARR.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWARR.DisplayMember = "_WAName";

                    //CheckWAComboboxes(i_combobox_wa);

                    #endregion
                }
                catch (Exception)
                {
                }
            }

            #region Re-assigning the combobox selected item index
            try
            {
                if (indexFL != -1)
                {
                    comboBoxWAFL.SelectedIndex = indexFL;
                }
                else comboBoxWAFL.SelectedIndex = 0;

                if (indexFR != -1)
                {
                    comboBoxWAFR.SelectedIndex = indexFR;
                }
                else comboBoxWAFR.SelectedIndex = 0;

                if (indexRL != -1)
                {
                    comboBoxWARL.SelectedIndex = indexRL;
                }
                else comboBoxWARL.SelectedIndex = 0;

                if (indexRR != -1)
                {
                    comboBoxWARR.SelectedIndex = indexRR;
                }
                else comboBoxWARR.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
            }
            #endregion
        }
        #endregion

        #region Load Case Creation and GUI
        /// <summary>
        /// Method create ALL the default Load Case Items 
        /// </summary>
        private void CreateALLDefaultLoadCases()
        {
            for (int i = 0; i < popupMenuLoadCase.ItemLinks.Count; i++)
            {
                CreateLoadCase(popupMenuLoadCase.ItemLinks[i].Caption, true);
            }
        }

        /// <summary>
        /// Method to create a Load Case item
        /// </summary>
        /// <param name="loadCaseName"></param>
        private void CreateLoadCase(string loadCaseName, bool defaultItemBeingCreated)
        {
            string LoadCaseName = loadCaseName;

            for (int i_LC = 0; i_LC <= LoadCaseGUI.List_LoadCaseGUI.Count; i_LC++)
            {
                if (LoadCase.LoadCaseCounter == i_LC)
                {
                    ///<summary>
                    ///Adding an object of the LoadCaseGUI to the List of LoadCaseGUI objects
                    /// </summary>
                    LoadCaseGUI.List_LoadCaseGUI.Insert(i_LC, new LoadCaseGUI(LoadCaseName, i_LC, this));

                    ///<summary>
                    ///Calling the HandleGUI method to initialize all the GUI elements
                    /// </summary>
                    LoadCaseGUI.List_LoadCaseGUI[i_LC].HandleGUI(navBarGroupLoadCases, navBarControlSimulation, this, i_LC, defaultItemBeingCreated);

                    ///<summary>
                    ///Calling the <c>Default_Values</c> class methods to populate the GridControls of the Load Case User control
                    /// </summary>
                    Default_Values.LoadCaseDefaultValues.AssignTypeOfLoadCase(LoadCaseName);
                    Default_Values.LoadCaseDefaultValues.InitializeLoadCase(LoadCaseGUI.List_LoadCaseGUI[i_LC]);

                    ///<summary>
                    ///Initializing the LoadGUI Class' variables
                    ///</summary>
                    LoadCaseGUI.List_LoadCaseGUI[i_LC].UpdateLoadCase();

                    ///<summary>
                    ///Adding an object of the LoadCase class to the List of LoadCse Objects
                    /// </summary>
                    LoadCase.CreateLoadCase(i_LC, LoadCaseGUI.List_LoadCaseGUI[i_LC], LoadCaseName);

                    ///<summary>
                    ///Initializing the DataGrids of the Load Case User Control
                    /// </summary>
                    LoadCaseGUI.List_LoadCaseGUI[i_LC].InitializeGridControls(this);

                    if (LoadCaseName == "Cornering with Steering")
                    {
                        //barButtonItemCreateMotion_ItemClick(sender, e);
                        barButtonItemCreateMotion.PerformClick();
                    }

                    if (!defaultItemBeingCreated)
                    {
                        navBarGroupSimulationSetup.Visible = true;
                        navBarGroupSimulationSetup.Expanded = true;
                        navBarGroupLoadCases.Expanded = true;
                    }

                    ///<summary>
                    ///Updating the counters of the LoadCase and LoadCaseGUI Classes
                    /// </summary>
                    LoadCase.LoadCaseCounter++;
                    LoadCaseGUI._LoadCaseCounter++;

                    ///<summary>
                    ///Invoking the Combobox operations command to populate the comboboxes of the SImulation Panels with the Load Cases
                    /// </summary>
                    comboBoxLoadCaseOperations();

                    break;
                }

            }
        }

        /// <summary>
        /// Fires when the Any of the Load Case Buttons in the Drop Down Menu is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadCaseButtonClicked(object sender, ItemClickEventArgs e)
        {
            CreateLoadCase(e.Item.Caption, false);
        }

        private void barButtonCreateBatchRun_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateNewBatchRun();
        }

        /// <summary>
        /// Method to Create a new Batch Run and add it to the <see cref="BatchRunGUI.batchRuns_GUI"/> list and also handle the GUI
        /// </summary>
        private void CreateNewBatchRun()
        {
            int index = BatchRunGUI.Counter;

            BatchRunGUI.batchRunBeingCreated = true;

            BatchRunGUI.batchRuns_GUI.Add(new BatchRunGUI());

            navBarGroupSimulationSetup.Visible = true;
            navBarGroupSimulationSetup.Expanded = true;
            navBarGroupLoadCaseBatchRun.Expanded = true;

            BatchRunGUI.batchRuns_GUI[index].InitializeBatchRunForm(LoadCase.List_LoadCases);
            BatchRunGUI.batchRuns_GUI[index].HandleGUI(navBarControlSimulation, navBarGroupLoadCaseBatchRun);

            BatchRunGUI.batchRuns_GUI[index].batchRun.ShowDialog();
            BatchRunGUI.batchRunBeingCreated = false;

            BatchRunGUI.Counter++;

        }

        private void barButtonItemCreateWorksheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateHeatMapInformationPanel(Mode.Create);
        }

        public int CreateHeatMapWorksheet(string heatMapName, BatchRunOutputMode outputMode, HeatMapMode heatMapMode, SpecialCaseOption specialCase)
        {

            int brIndex = 0;

            for (int i = 0; i < BatchRunGUI.batchRuns_GUI.Count; i++)
            {
                if (navBarControlResults.ActiveGroup.Name == BatchRunGUI.batchRuns_GUI[i].navBarGroupBatchRunResults.Name)
                {
                    brIndex = i;
                }
            }

            HeatMapWorksheet.Worksheets.Add(new HeatMapWorksheet());

            HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter].ConstructWorksheet(heatMapName, HeatMapWorksheet.Counter + 1, brIndex, outputMode);

            HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter].HandleGUI(navBarControlResults, BatchRunGUI.batchRuns_GUI[brIndex].navBarGroupBatchRunResults, heatMapMode, specialCase, brIndex);

            HeatMapWorksheet.Counter++;

            return HeatMapWorksheet.Counter;
        }

        private void barButtonItemCreateHeatMap_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateHeatMapInformationPanel(Mode.Modify);
        }

        private void CreateHeatMapInformationPanel(Mode heatMapOperationmode)
        {
            HeatMapInformation heatMapInformation = new HeatMapInformation(heatMapOperationmode);
            heatMapInformation.GetBatchRunData(BatchRunGUI.batchRuns_GUI);
            heatMapInformation.ShowDialog();
        }


        #region Combobox Load Case Operations
        public void comboBoxLoadCaseOperations()
        {
            ///<remarks>
            ///The <c>index</c> here is used to refer to the index of the selected item of the combobox. Hence, it is initialized as -1. So, if there isn't any item inside the combobox, the for loop will not be solved even once. At this point, if the code to re-assign the existing 
            ///index in the combobox is executed, everything will fail if I assign index to 0 initially as there is no 0th element in the combobox, only a -1th element
            /// </remarks>
            int index = -1;

            for (int i_SPane = 0; i_SPane < Simulation.List_Simulation.Count; i_SPane++)
            {
                index = Simulation.List_Simulation[i_SPane].simulationPanel.comboBoxLoadCase.SelectedIndex;
                Simulation.List_Simulation[i_SPane].simulationPanel.comboBoxLoadCase.Items.Clear();

                for (int i_SLC = 0; i_SLC < LoadCase.List_LoadCases.Count; i_SLC++)
                {
                    try
                    {
                        Simulation.List_Simulation[i_SPane].simulationPanel.comboBoxLoadCase.Items.Insert(i_SLC, LoadCase.List_LoadCases[i_SLC]);
                        Simulation.List_Simulation[i_SPane].simulationPanel.comboBoxLoadCase.DisplayMember = "LoadCaseName";
                    }
                    catch (Exception)
                    {

                        ///<remarks>
                        ///To safeguard the code since the almost always the Load Case will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                        ///a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 
                        /// </remarks>
                    }
                }

                #region Reassigning the combobox Index
                try
                {
                    if (index != -1)
                    {
                        Simulation.List_Simulation[i_SPane].simulationPanel.comboBoxLoadCase.SelectedIndex = index;
                    }
                    else Simulation.List_Simulation[i_SPane].simulationPanel.comboBoxLoadCase.SelectedIndex = 0;

                }
                catch (Exception)
                {


                }
                #endregion

            }
        }
        #endregion

        #endregion

        //
        // Suspension Coordinates Creation and GUI
        //
        #region Suspension Coordinates Creation

        //
        // Front Left Suspension Coordinate Item Creation and GUI
        //
        #region Front Left Suspension Coordinate Item Creation and GUI

        public List<SuspensionCoordinatesFrontGUI> scflGUI = new List<SuspensionCoordinatesFrontGUI>();

        private void BarButtonSCFL_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesFL.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionFL.Expanded = true;
            //accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlFixedPointsFL);
            //accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlMovingPointsFL);

            //Default_Values.FRONTLEFTSuspensionDefaultValues.LowerFrontChassis(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.LowerRearChassis(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.UpperFrontChassis(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.UpperRearChassis(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.BellCrankPivot(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.ARBChassis(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.TorsionBarBottom(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.SteeringLinkChassis(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.DamperShockMount(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.DamperBellCrank(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.PushRullBellCrank(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.ARBBellCrank(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.PushPullUpright(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.UBJ(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.LBJ(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.ARBLowerLink(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.WheelCentre(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.SteeringLinkpUpright(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.ContactPatch(this);
            //Default_Values.FRONTLEFTSuspensionDefaultValues.RideHeightRef(this);


            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scfl = 0; i_scfl <= navBarItemSCFLClass.navBarItemSCFL.Count; i_scfl++)
            {
                if (SuspensionCoordinatesFront.SCFLCounter == i_scfl)
                {
                    #region Creating a new NavBarItem and adding it to the Suspension Coordinates Front Left Group
                    navBarItemSCFLClass temp_navBarItemSCFL = new navBarItemSCFLClass();
                    navBarSCFL_Global.CreateNewNavbarItem(i_scfl, temp_navBarItemSCFL, navBarControlDesign, navBarGroupSuspensionFL);
                    navBarItemSCFLClass.navBarItemSCFL[i_scfl].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFL_LinkClicked);
                    break;
                    #endregion
                }
            }


            for (int l_scfl = 0; l_scfl <= SuspensionCoordinatesFront.Assy_List_SCFL.Count; l_scfl++)
            {


                if (SuspensionCoordinatesFront.SCFLCounter == l_scfl)
                {
                    #region Creating a GUI object of SCFL whcih will be added to the list of SCFL GUI objects
                    scflGUI.Insert(l_scfl, new SuspensionCoordinatesFrontGUI());
                    scflGUI[l_scfl].FrontSuspensionTypeGUI(this);
                    #endregion

                    #region Invoking the Default_Values class' method to populate the SCFLGUI table
                    if (DoubleWishboneFront_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.FRONTLEFTSuspensionDefaultValues.DoubleWishBone(this, scflGUI[l_scfl]);
                    }
                    else if (McPhersonFront_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.FRONTLEFTSuspensionDefaultValues.McPherson(scflGUI[l_scfl], this);

                    }
                    /*else*/
                    if (CurrentSuspensionIsMapped)
                    {
                        Default_Values.FRONTLEFTSuspensionDefaultValues.CreateMappedSuspension(M1_Global.vehicleGUI[navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.CoordinatesFL, scflGUI[l_scfl]);
                    }
                    #endregion

                    #region Populating the SCFLGUI Object
                    scflGUI[l_scfl].EditFrontLeftCoordinatesGUI(this, scflGUI[l_scfl], l_scfl);
                    #endregion

                    #region Adding the new SCFL object to the list SCFL Objects
                    SCFL1_Global.CreateNewSCFL(l_scfl, scflGUI[l_scfl]);
                    SuspensionCoordinatesFront.SCFLCurrentID = SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFL_ID;
                    #endregion

                    #region Initializing the DataGrid of the for SCFLGUI
                    scflGUI[l_scfl].bandedGridView_SCFLGUI = CustomBandedGridView.CreateNewBandedGridView(l_scfl, 4, "Front Left Suspension Coordinates");
                    gridControl2.DataSource = scflGUI[l_scfl].SCFLDataTableGUI;
                    gridControl2.MainView = scflGUI[l_scfl].bandedGridView_SCFLGUI;
                    scflGUI[l_scfl].bandedGridView_SCFLGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scflGUI[l_scfl].bandedGridView_SCFLGUI, this);
                    scflGUI[l_scfl].bandedGridView_SCFLGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scflGUI[l_scfl].bandedGridView_SCFLGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFLIsModified);
                    #endregion

                    ChangeTracker++;
                    break;
                }
            }

            SuspensionCoordinatesFront.SCFLCounter++;// This is a static counter and it keeps track of the number of Chassis items created
            ComboboxSCFLOperations();

        }

        public void ModifyFrontLeftSuspension(bool Copied_Identifier, int index)
        {
            // Copied Identifier determines whether Front Left Coordinates have been copied or manually edited by the user. Based on its value, the CopyFrontLeftTOFrontRight function is called.
            // This prevents an infinite loop

            //int index = navBarGroupSuspensionFL.SelectedLinkIndex;

            for (int l_scfl = 0; l_scfl <= SuspensionCoordinatesFront.Assy_List_SCFL.Count; l_scfl++)
            {
                if (index == l_scfl)
                {
                    try
                    {
                        #region Editing the SCFLGUI Object
                        scflGUI[l_scfl].EditFrontLeftCoordinatesGUI(this, scflGUI[l_scfl], l_scfl);
                        #endregion

                        #region Editing the Object
                        SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].EditFrontLeftSuspension(l_scfl, scflGUI[l_scfl]);
                        SuspensionCoordinatesFront.SCFLCurrentID = SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFL_ID;
                        #endregion

                        #region Copying the Front Left Coordinates to the Front Right if symmetry is chosen
                        if (SuspensionCoordinatesFront.Assy_List_SCFL[index].FrontSymmetry == true && Copied_Identifier == false)
                        {
                            CopyFrontLeftTOFrontRight();

                        }
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFLIsModified);
                        #endregion

                        SCFL_ModifyInVehicle(l_scfl, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]);

                        R1 = this;
                        EditFrontCAD(l_scfl);

                        ChangeTracker++;
                        break;
                    }
                    catch (Exception)
                    {
                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                }
            }

            ComboboxSCFLOperations();

        }

        void navBarItemSCFL_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            int index = navBarGroupSuspensionFL.SelectedLinkIndex;
            navBarGroupSuspensionFR.SelectedLinkIndex = navBarGroupSuspensionFL.SelectedLinkIndex;
            SuspensionCoordinatesFront.SCFLCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[index]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[index]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLIsModified);
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesFL.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupSuspensionFL.Expanded = true;
            //accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlFixedPointsFL);
            //accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlMovingPointsFL);
            #endregion

            for (int c_scfl = 0; c_scfl <= navBarItemSCFLClass.navBarItemSCFL.Count; c_scfl++)
            {
                if (index == c_scfl)
                {
                    #region Displaying to the user the Coordinates
                    try
                    {
                        gridControl2.DataSource = SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].SCFLDataTable;
                        gridControl2.MainView = scflGUI[c_scfl].bandedGridView_SCFLGUI;
                        scflGUI[c_scfl].SCFLDataTableGUI = SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].SCFLDataTable;
                        scflGUI[c_scfl].bandedGridView_SCFLGUI.ExpandAllGroups();
                        scflGUI[c_scfl].TabPage_FrontCAD.PageVisible = true;
                        int SelectedPage = TabControl_Outputs.TabPages.IndexOf(scflGUI[c_scfl].TabPage_FrontCAD);
                        TabControl_Outputs.SelectedTabPageIndex = SelectedPage;

                    }
                    catch (Exception)
                    {
                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                    #endregion
                    break;
                }
            }

        }

        private void ComboboxSCFLOperations()
        {
            int index = comboBoxSCFL.SelectedIndex;
            comboBoxSCFL.Items.Clear();

            for (int i_combobox_scfl = 0; i_combobox_scfl < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_combobox_scfl++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of SCFL Objects
                    comboBoxSCFL.Items.Insert(i_combobox_scfl, SuspensionCoordinatesFront.Assy_List_SCFL[i_combobox_scfl]);
                    comboBoxSCFL.DisplayMember = "_SCName";
                    #endregion
                }
                catch (Exception)
                {
                }
            }


            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    comboBoxSCFL.SelectedIndex = index;
                }
                else comboBoxSCFL.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
                try
                {
                    comboBoxSCFL.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    // This additional block is to deal with the scenarion that the Suspension has been imported with lesser number of items than what existed and hence, to resotre the combobox to 0 index value
                }
            }
            #endregion
        }
        #endregion

        //
        // Front Right Suspension Coordinate Item Creation and GUI
        //
        #region Front Right Suspension Coordinate Item Creation and GUI

        public List<SuspensionCoordinatesFrontRightGUI> scfrGUI = new List<SuspensionCoordinatesFrontRightGUI>();

        private void BarButtonSCFR_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesFR.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionFR.Expanded = true;
            //accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlFixedPointsFR);
            //accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlMovingPointsFR);

            //Default_Values.FRONTRIGHTSuspensionDefaultValues.LowerFrontChassis(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.LowerRearChassis(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.UpperFrontChassis(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.UpperRearChassis(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.BellCrankPivot(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.ARBChassis(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.TorsionBarBottom(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.SteeringLinkChassis(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.DamperShockMount(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.DamperBellCrank(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.PushRullBellCrank(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.ARBBellCrank(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.PushPullUpright(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.UBJ(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.LBJ(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.ARBLowerLink(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.WheelCentre(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.SteeringLinkpUpright(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.ContactPatch(this);
            //Default_Values.FRONTRIGHTSuspensionDefaultValues.RideHeightRef(this);




            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scfr = 0; i_scfr <= navBarItemSCFRClass.navBarItemSCFR.Count; i_scfr++)
            {
                if (SuspensionCoordinatesFrontRight.SCFRCounter == i_scfr)
                {
                    #region Creating a new NavBarItem and adding it to the Front Right Suspnesion Coordinates Group
                    navBarItemSCFRClass temp_navBarItemSCFR = new navBarItemSCFRClass();
                    navBarSCFR_Global.CreateNewNarBarItem(i_scfr, temp_navBarItemSCFR, navBarControlDesign, navBarGroupSuspensionFR);
                    navBarItemSCFRClass.navBarItemSCFR[i_scfr].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFR_LinkClicked);
                    break;
                    #endregion
                }
            }


            for (int l_scfr = 0; l_scfr <= SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; l_scfr++)
            {


                if (SuspensionCoordinatesFrontRight.SCFRCounter == l_scfr)
                {
                    #region Creating a GUI object of SCFR whcih will be added to the list of SCFR GUI objects
                    scfrGUI.Insert(l_scfr, new SuspensionCoordinatesFrontRightGUI());
                    scfrGUI[l_scfr].FrontSuspensionTypeGUI(this);
                    #endregion

                    #region Invoking the Default_Values class' method to populate the SCFRGUI table
                    if (DoubleWishboneFront_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.FRONTRIGHTSuspensionDefaultValues.DoubleWishBone(this, scfrGUI[l_scfr]);
                    }
                    else if (McPhersonFront_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.FRONTRIGHTSuspensionDefaultValues.McPherson(scfrGUI[l_scfr], this);
                    }
                    /*else*/
                    if (CurrentSuspensionIsMapped)
                    {
                        Default_Values.FRONTRIGHTSuspensionDefaultValues.CreateMappedSuspension(M1_Global.vehicleGUI[navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.CoordinatesFR, scfrGUI[l_scfr]);
                    }
                    #endregion

                    #region Populating the SCFR Object
                    scfrGUI[l_scfr].EditFrontRightCoordinatesGUI(this, scfrGUI[l_scfr]);
                    #endregion

                    #region Adding the new SCFR object to the list SCFR Objects
                    SCFR1_Global.CreateNewSCFR(l_scfr, scfrGUI[l_scfr]);
                    SuspensionCoordinatesFrontRight.SCFRCurrentID = SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFR_ID;
                    #endregion

                    #region Initializing the DataGrid of the for SCFRGUI
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI = CustomBandedGridView.CreateNewBandedGridView(l_scfr, 4, "Front Right Suspension Coordinates");
                    gridControl2.DataSource = scfrGUI[l_scfr].SCFRDataTableGUI;
                    gridControl2.MainView = scfrGUI[l_scfr].bandedGridView_SCFRGUI;
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scfrGUI[l_scfr].bandedGridView_SCFRGUI, this);
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFRIsModified);
                    #endregion

                    ChangeTracker++;
                    break;
                }
            }

            SuspensionCoordinatesFrontRight.SCFRCounter++; // This is a static counter and it keeps track of the number of Chassis items created
            ComboBoxSCFROperations();
        }

        public void ModifyFrontRightSuspension(bool Copied_Identifier, int index)
        {
            // Copied Identifier determines whether Front Right Coordinates have been copied or manually edited by the user. Based on its value, the CopyFrontRightTOFrontLeft function is called.
            // This prevents an infinite loop

            //int index = navBarGroupSuspensionFR.SelectedLinkIndex;

            for (int l_scfr = 0; l_scfr <= SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; l_scfr++)
            {
                if (index == l_scfr)
                {
                    try
                    {
                        #region Editing the SCFLGUI Object
                        scfrGUI[l_scfr].EditFrontRightCoordinatesGUI(this, scfrGUI[l_scfr]);
                        #endregion

                        #region Editing the SCFRGUI Object
                        SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].EditFrontRightSuspension(l_scfr, scfrGUI[l_scfr]);
                        SuspensionCoordinatesFrontRight.SCFRCurrentID = SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFR_ID;
                        #endregion

                        #region Copying the Front Right Coordinates to the Front Left if symmetry is chosen
                        if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].FrontSymmetry == true && Copied_Identifier == false)
                        {
                            CopyFrontRightTOFrontLeft();
                        }
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFRIsModified);
                        #endregion

                        SCFR_ModifyInVehicle(l_scfr, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]);

                        R1 = this;
                        EditFrontCAD(l_scfr);

                        ChangeTracker++;
                        break;
                    }
                    catch (Exception)
                    {// Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception}
                    }
                }
            }

            ComboBoxSCFROperations();
        }

        void navBarItemSCFR_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            int index = navBarGroupSuspensionFR.SelectedLinkIndex;
            navBarGroupSuspensionFL.SelectedLinkIndex = navBarGroupSuspensionFR.SelectedLinkIndex;
            SuspensionCoordinatesFrontRight.SCFRCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[index]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[index]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRIsModified);
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesFR.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupSuspensionFR.Expanded = true;
            //accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlFixedPointsFR);
            //accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlMovingPointsFR);
            #endregion

            for (int c_scfr = 0; c_scfr <= navBarItemSCFRClass.navBarItemSCFR.Count; c_scfr++)
            {
                if (index == c_scfr)
                {

                    #region Displaying to the user the Coordinates
                    try
                    {
                        gridControl2.DataSource = SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].SCFRDataTable;
                        gridControl2.MainView = scfrGUI[c_scfr].bandedGridView_SCFRGUI;
                        scfrGUI[c_scfr].SCFRDataTableGUI = SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].SCFRDataTable;
                        scfrGUI[c_scfr].bandedGridView_SCFRGUI.ExpandAllGroups();
                        scflGUI[c_scfr].TabPage_FrontCAD.PageVisible = true;
                        int SelectedPage = TabControl_Outputs.TabPages.IndexOf(scflGUI[c_scfr].TabPage_FrontCAD);
                        TabControl_Outputs.SelectedTabPageIndex = SelectedPage;

                    }
                    catch (Exception)
                    {

                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                    #endregion
                }
            }
        }

        private void ComboBoxSCFROperations()
        {
            int index = comboBoxSCFR.SelectedIndex;
            comboBoxSCFR.Items.Clear();

            for (int i_combobox_scfr = 0; i_combobox_scfr < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_combobox_scfr++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of SuspensionCoordinateFrontRight Objects
                    comboBoxSCFR.Items.Insert(i_combobox_scfr, SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_combobox_scfr]);
                    comboBoxSCFR.DisplayMember = "_SCName";
                    #endregion
                }
                catch (Exception)
                {
                }
            }


            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    comboBoxSCFR.SelectedIndex = index;
                }
                else comboBoxSCFR.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
                try
                {
                    comboBoxSCFR.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    // This additional block is to deal with the scenarion that the Suspension has been imported with lesser number of items than what existed and hence, to resotre the combobox to 0 index value
                }
            }
            #endregion

        }

        #endregion

        #region Front Suspension CAD Operations
        #region Method to exclusively edit the Front Suspension CAD
        public static void EditFrontCAD(int Index)
        {
            R1 = R1.FormVariableUpdater();
            R1.scflGUI[Index].CreateFrontCAD(R1.scflGUI[Index].CADFront, R1.scflGUI[Index], SuspensionCoordinatesFront.Assy_List_SCFL[Index], SuspensionCoordinatesFrontRight.Assy_List_SCFR[Index]);
        }

        #endregion

        #region Method to invoke the Front Suspension CAD Creator
        public void CreateFrontInputCAD(int Index, bool IsRecreated) => scflGUI[Index].FrontCADPreProcessor(scflGUI[Index], Index, IsRecreated);
        #endregion 
        #endregion

        //
        //Rear Left Suspension Coordinate Item Creation and GUI
        //
        #region Rear Left Suspension Coordinate Item Creation and GUI

        public List<SuspensionCoordinatesRearGUI> scrlGUI = new List<SuspensionCoordinatesRearGUI>();

        private void BarButtonSCRL_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesRL.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionRL.Expanded = true;
            //accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlFixedPointsRL);
            //accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlMovingPointsRL);

            //Default_Values.REARLEFTSuspensionDefaultValues.LowerFrontChassis(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.LowerRearChassis(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.UpperFrontChassis(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.UpperRearChassis(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.BellCrankPivot(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.ARBChassis(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.TorsionBarBottom(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.SteeringLinkChassis(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.DamperShockMount(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.DamperBellCrank(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.PushRullBellCrank(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.ARBBellCrank(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.PushPullUpright(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.UBJ(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.LBJ(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.ARBLowerLink(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.WheelCentre(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.SteeringLinkpUpright(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.ContactPatch(this);
            //Default_Values.REARLEFTSuspensionDefaultValues.RideHeightRef(this);



            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scrl = 0; i_scrl <= navBarItemSCRLClass.navBarItemSCRL.Count; i_scrl++)
            {
                if (SuspensionCoordinatesRear.SCRLCounter == i_scrl)
                {
                    #region Creating a new NavBarItem and adding it to the AntiRollBar Group
                    navBarItemSCRLClass temp_navBarItemSCRL = new navBarItemSCRLClass();
                    navBarSCRL_Global.CreateNewNavBarItem(i_scrl, temp_navBarItemSCRL, navBarControlDesign, navBarGroupSuspensionRL);
                    navBarItemSCRLClass.navBarItemSCRL[i_scrl].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRL_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_scrl = 0; l_scrl <= SuspensionCoordinatesRear.Assy_List_SCRL.Count; l_scrl++)
            {
                if (SuspensionCoordinatesRear.SCRLCounter == l_scrl)
                {
                    #region Creating a GUI object of SCRL whcih will be added to the list of SCRL GUI objects
                    scrlGUI.Insert(l_scrl, new SuspensionCoordinatesRearGUI());
                    scrlGUI[l_scrl].RearSuspensionTypeGUI(this);
                    #endregion

                    #region Invoking the Default_Values class' method to populate the SCRLGUI table
                    if (DoubleWishboneRear_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.REARLEFTSuspensionDefaultValues.DoubleWishBone(this, scrlGUI[l_scrl]);
                    }
                    else if (McPhersonRear_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.REARLEFTSuspensionDefaultValues.McPherson(scrlGUI[l_scrl], this);
                    }
                    /*else*/
                    if (CurrentSuspensionIsMapped)
                    {
                        Default_Values.REARLEFTSuspensionDefaultValues.CreateMappedSuspension(M1_Global.vehicleGUI[navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.CoordinatesRL, scrlGUI[l_scrl]);
                    }
                    #endregion

                    #region Populating the SCRLGUI Object
                    scrlGUI[l_scrl].EditRearLeftCoordinatesGUI(this, scrlGUI[l_scrl]);
                    #endregion

                    #region Adding the new SCRL object to the list SCRL Objects
                    SCRL1_Global.CreateNewSCRL(l_scrl, scrlGUI[l_scrl]);
                    SuspensionCoordinatesRear.SCRLCurrentID = SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl].SCRL_ID;
                    #endregion

                    #region Initializng the Data Grid View for SCRL object
                    scrlGUI[l_scrl].bandedGridView_SCRLGUI = CustomBandedGridView.CreateNewBandedGridView(l_scrl, 4, "Rear Left Suspension Coordinates");
                    gridControl2.DataSource = scrlGUI[l_scrl].SCRLDataTableGUI;
                    gridControl2.MainView = scrlGUI[l_scrl].bandedGridView_SCRLGUI;
                    scrlGUI[l_scrl].bandedGridView_SCRLGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scrlGUI[l_scrl].bandedGridView_SCRLGUI, this);
                    scrlGUI[l_scrl].bandedGridView_SCRLGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scrlGUI[l_scrl].bandedGridView_SCRLGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]._RedocommandsSCRL, SuspensionCoordinatesRear.SCRLCurrentID, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl].SCRLIsModified);
                    #endregion

                    ChangeTracker++;
                    break;
                }
            }

            SuspensionCoordinatesRear.SCRLCounter++; // This is a static counter and it keeps track of the number of SuspensionCoordinatesRear items created
            ComboboxSCRLOperations();

        }

        public void ModifyRearLeftSuspension(bool CopiedIdentifier, int index)
        {
            // Copied Identifier determines whether Rear Left Coordinates have been copied or manually edited by the user. Based on its value, the CopyRearLeftTORearRight function is called.
            // This prevents an infinite loop

            //int index = navBarGroupSuspensionRL.SelectedLinkIndex;

            for (int l_scrl = 0; l_scrl <= SuspensionCoordinatesRear.Assy_List_SCRL.Count; l_scrl++)
            {
                if (index == l_scrl)
                {
                    try
                    {
                        #region Editing the SCRLGUI Object
                        scrlGUI[l_scrl].EditRearLeftCoordinatesGUI(this, scrlGUI[l_scrl]);
                        #endregion

                        #region Editing the Object
                        SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl].EditRearLeftSuspension(l_scrl, scrlGUI[l_scrl]);
                        SuspensionCoordinatesRear.SCRLCurrentID = SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl].SCRL_ID;
                        #endregion

                        #region Copying the Rear Left Coordinates to the Rear Right if symmetry is chosen
                        if (SuspensionCoordinatesRear.Assy_List_SCRL[index].RearSymmetry == true && CopiedIdentifier == false)
                        {
                            CopyRearLeftTOReaRight();
                        }
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]._RedocommandsSCRL, SuspensionCoordinatesRear.SCRLCurrentID, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl].SCRLIsModified);
                        #endregion

                        SCRL_ModifyInVehicle(l_scrl, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]);

                        R1 = this;
                        EditRearCAD(l_scrl);

                        ChangeTracker++;
                        break;
                    }
                    catch (Exception)
                    {

                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                }
            }
            ComboboxSCRLOperations();
        }

        void navBarItemSCRL_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSuspensionRL.SelectedLinkIndex;
            navBarGroupSuspensionRR.SelectedLinkIndex = navBarGroupSuspensionRL.SelectedLinkIndex;
            SuspensionCoordinatesRear.SCRLCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[index]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[index]._RedocommandsSCRL, SuspensionCoordinatesRear.SCRLCurrentID, SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLIsModified);
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesRL.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupSuspensionRL.Expanded = true;
            //accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlFixedPointsRL);
            //accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlMovingPointsRL);
            #endregion

            for (int c_scrl = 0; c_scrl <= navBarItemSCRLClass.navBarItemSCRL.Count; c_scrl++)
            {
                if (index == c_scrl)
                {
                    #region Displaying to the user the Coordinates
                    try
                    {
                        gridControl2.DataSource = SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].SCRLDataTable;
                        gridControl2.MainView = scrlGUI[c_scrl].bandedGridView_SCRLGUI;
                        scrlGUI[c_scrl].SCRLDataTableGUI = SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].SCRLDataTable;
                        scrlGUI[c_scrl].bandedGridView_SCRLGUI.ExpandAllGroups();
                        scrlGUI[c_scrl].TabPage_RearCAD.PageVisible = true;
                        int SelectedPage = TabControl_Outputs.TabPages.IndexOf(scrlGUI[c_scrl].TabPage_RearCAD);
                        TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                    }
                    catch (Exception)
                    {
                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception

                    }
                    #endregion

                }
            }

        }

        private void ComboboxSCRLOperations()
        {
            int index = comboBoxSCRL.SelectedIndex;
            comboBoxSCRL.Items.Clear();

            for (int i_combobox_scrl = 0; i_combobox_scrl < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_combobox_scrl++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of ARB Objects
                    comboBoxSCRL.Items.Insert(i_combobox_scrl, SuspensionCoordinatesRear.Assy_List_SCRL[i_combobox_scrl]);
                    comboBoxSCRL.DisplayMember = "_SCName";
                    #endregion
                }
                catch (Exception)
                {
                }
            }


            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    comboBoxSCRL.SelectedIndex = index;
                }
                else comboBoxSCRL.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
                try
                {
                    comboBoxSCRL.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    // This additional block is to deal with the scenarion that the Suspension has been imported with lesser number of items than what existed and hence, to resotre the combobox to 0 index value
                }
            }
            #endregion
        }
        #endregion

        //
        //Rear Right Suspension Coordinate Item Creation and GUI
        //
        #region Rear Right Suspension Coordinate Item Creation and GUI

        public List<SuspensionCoordinatesRearRightGUI> scrrGUI = new List<SuspensionCoordinatesRearRightGUI>();

        private void BarButtonSCRR_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            gridControl2.BringToFront();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesRR.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionRR.Expanded = true;
            //accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlFixedPointsRR);
            //accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlMovingPointsRR);

            //Default_Values.REARRIGHTSuspensionDefaultValues.LowerFrontChassis(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.LowerRearChassis(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.UpperFrontChassis(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.UpperRearChassis(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.BellCrankPivot(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.ARBChassis(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.TorsionBarBottom(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.SteeringLinkChassis(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.DamperShockMount(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.DamperBellCrank(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.PushRullBellCrank(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.ARBBellCrank(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.PushPullUpright(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.UBJ(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.LBJ(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.ARBLowerLink(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.WheelCentre(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.SteeringLinkpUpright(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.ContactPatch(this);
            //Default_Values.REARRIGHTSuspensionDefaultValues.RideHeightRef(this);


            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scrr = 0; i_scrr <= navBarItemSCRRClass.navBarItemSCRR.Count; i_scrr++)
            {
                if (SuspensionCoordinatesRearRight.SCRRCounter == i_scrr)
                {
                    #region Creating a new NavBarItem and adding it to the SuspensionCoordinatesRearRight Group
                    navBarItemSCRRClass temp_navBarItemSCRR = new navBarItemSCRRClass();
                    navBarSCRR_Global.CreateNewNarBarItem(i_scrr, temp_navBarItemSCRR, navBarControlDesign, navBarGroupSuspensionRR);
                    navBarItemSCRRClass.navBarItemSCRR[i_scrr].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRR_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_scrr = 0; l_scrr <= SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; l_scrr++)
            {


                if (SuspensionCoordinatesRearRight.SCRRCounter == l_scrr)
                {
                    #region Creating an object of SuspensionCoordinatesRearRight whcih will be added to the list of SuspensionCoordinatesRearRight objects
                    scrrGUI.Insert(l_scrr, new SuspensionCoordinatesRearRightGUI());
                    scrrGUI[l_scrr].RearSuspensionTypeGUI(this);
                    #endregion

                    #region Invoking the Default_Values class' method to populate the SCRRGUI table
                    if (DoubleWishboneRear_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.REARRIGHTSuspensionDefaultValues.DoubleWishBone(this, scrrGUI[l_scrr]);
                    }
                    else if (McPhersonRear_VehicleGUI == 1 /*&& !CurrentSuspensionIsMapped*/)
                    {
                        Default_Values.REARRIGHTSuspensionDefaultValues.McPherson(scrrGUI[l_scrr], this);
                    }
                    /*else*/
                    if (CurrentSuspensionIsMapped)
                    {
                        Default_Values.REARRIGHTSuspensionDefaultValues.CreateMappedSuspension(M1_Global.vehicleGUI[navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.CoordinatesRR, scrrGUI[l_scrr]);
                    }
                    #endregion

                    #region Populating the SCRRGUI Object
                    scrrGUI[l_scrr].EditRearSuspensionGUI(this, scrrGUI[l_scrr]);
                    #endregion

                    #region Adding the new SuspensionCoordinatesRearRight object to the list SuspensionCoordinatesRearRight Objects
                    SCRR1_Global.CreateNewSCRR(l_scrr, scrrGUI[l_scrr]);
                    SuspensionCoordinatesRearRight.SCRRCurrentID = SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRR_ID;
                    #endregion

                    #region Initializing the DataGridView for SCRRGUI
                    scrrGUI[l_scrr].bandedGridView_SCRRGUI = CustomBandedGridView.CreateNewBandedGridView(l_scrr, 4, "Rear Right Suspension Coordinates");
                    gridControl2.DataSource = scrrGUI[l_scrr].SCRRDataTableGUI;
                    gridControl2.MainView = scrrGUI[l_scrr].bandedGridView_SCRRGUI;
                    scrrGUI[l_scrr].bandedGridView_SCRRGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scrrGUI[l_scrr].bandedGridView_SCRRGUI, this);
                    scrrGUI[l_scrr].bandedGridView_SCRRGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scrrGUI[l_scrr].bandedGridView_SCRRGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRRIsModified);
                    #endregion

                    ChangeTracker++;

                    break;
                }
            }

            SuspensionCoordinatesRearRight.SCRRCounter++; // This is a static counter and it keeps track of the number of SuspensionCoordinatesRearRight items created
            ComboboxSCRROperations();

        }

        public void ModifyRearRightSuspension(bool CopiedIdentifier, int index)
        {
            // Copied Identifier determines whether Rear Right Coordinates have been copied or manually edited by the user. Based on its value, the CopyRearRightTORearLeft function is called.
            // This prevents an infinite loop

            //int index = navBarGroupSuspensionRR.SelectedLinkIndex;

            for (int l_scrr = 0; l_scrr <= SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; l_scrr++)
            {
                if (index == l_scrr)
                {
                    try
                    {
                        #region Editing the SCRRGUI Object
                        scrrGUI[l_scrr].EditRearSuspensionGUI(this, scrrGUI[l_scrr]);
                        #endregion

                        #region Editing the object
                        SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].EditRearSuspension(l_scrr, scrrGUI[l_scrr]);
                        SuspensionCoordinatesRearRight.SCRRCurrentID = SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRR_ID;
                        #endregion

                        #region Copying the Rear Right Coordinates to the Rear Left if symmetry is chosen
                        if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].RearSymmetry == true && CopiedIdentifier == false)
                        {
                            CopyRearRightTORearLeft();
                        }
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRRIsModified);
                        #endregion

                        SCRR_ModifyInVehicle(l_scrr, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]);

                        R1 = this;
                        EditRearCAD(l_scrr);

                        ChangeTracker++;
                        break;
                    }
                    catch (Exception)
                    {

                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                }
            }
            ComboboxSCRROperations();
        }

        void navBarItemSCRR_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSuspensionRR.SelectedLinkIndex;
            navBarGroupSuspensionRL.SelectedLinkIndex = navBarGroupSuspensionRR.SelectedLinkIndex;
            SuspensionCoordinatesRearRight.SCRRCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[index]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[index]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRIsModified);
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Show();
            //accordionControlTireStiffness.Hide();
            //accordionControlSuspensionCoordinatesFL.Hide();
            //accordionControlSuspensionCoordinatesFR.Hide();
            //accordionControlSuspensionCoordinatesRL.Hide();
            //accordionControlSuspensionCoordinatesRR.Hide();
            //accordionControlDamper.Hide();
            //accordionControlAntiRollBar.Hide();
            //accordionControlSprings.Hide();
            //accordionControlChassis.Hide();
            //tabPaneResults.Hide();
            //accordionControlWheelAlignment.Hide();
            //accordionControlVehicleItem.Hide();
            //accordionControlSuspensionCoordinatesRR.Show();
            //navBarGroupDesign.Visible = true;
            //navBarGroupSuspensionRR.Expanded = true;
            //accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlFixedPointsRR);
            //accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlMovingPointsRR);
            #endregion

            for (int c_scrr = 0; c_scrr <= navBarItemSCRRClass.navBarItemSCRR.Count; c_scrr++)
            {
                if (index == c_scrr)
                {

                    #region Displaying to the user the Coordinates
                    try
                    {
                        gridControl2.DataSource = SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].SCRRDataTable;
                        gridControl2.MainView = scrrGUI[c_scrr].bandedGridView_SCRRGUI;
                        scrrGUI[c_scrr].SCRRDataTableGUI = SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].SCRRDataTable;
                        scrrGUI[c_scrr].bandedGridView_SCRRGUI.ExpandAllGroups();
                        scrlGUI[c_scrr].TabPage_RearCAD.PageVisible = true;
                        int SelectedPage = TabControl_Outputs.TabPages.IndexOf(scrlGUI[c_scrr].TabPage_RearCAD);
                        TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                    }
                    catch (Exception)
                    {

                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                    #endregion

                }
            }
        }

        private void ComboboxSCRROperations()
        {
            int index = comboBoxSCRR.SelectedIndex;
            comboBoxSCRR.Items.Clear();

            for (int i_combobox_scrr = 0; i_combobox_scrr < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_combobox_scrr++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of SuspensionCoordinatesRearRight Objects
                    comboBoxSCRR.Items.Insert(i_combobox_scrr, SuspensionCoordinatesRearRight.Assy_List_SCRR[i_combobox_scrr]);
                    comboBoxSCRR.DisplayMember = "_SCName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }


            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    comboBoxSCRR.SelectedIndex = index;
                }
                else comboBoxSCRR.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
                try
                {
                    comboBoxSCRR.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    // This additional block is to deal with the scenarion that the Suspension has been imported with lesser number of items than what existed and hence, to resotre the combobox to 0 index value
                }
            }
            #endregion
        }
        #endregion

        #region Rear Suspension CAD Operations
        #region Method to exclusively edit the Rear Suspension CAD
        public static void EditRearCAD(int Index)
        {
            R1 = R1.FormVariableUpdater();
            R1.scrlGUI[Index].CreateRearCAD(R1.scrlGUI[Index].CADRear, R1.scrlGUI[Index], SuspensionCoordinatesRear.Assy_List_SCRL[Index], SuspensionCoordinatesRearRight.Assy_List_SCRR[Index]);
        }
        #endregion

        #region Method to invoke the Rear Suspension CAD Creator.
        public void CreateRearInputCAD(int Index, bool IsRecreated) => scrlGUI[Index].RearCADPreProcessor(scrlGUI[Index], Index, IsRecreated);
        #endregion
        #endregion

        #endregion

        //
        // Vehicle Item Creation and GUI
        //
        #region Vehicle Item Creation and GUI

        #region Assembly Validators

        /// <summary>
        /// Checks if the Vehicle's Suspension is assembled
        /// </summary>
        /// <returns>Booloean</returns>
        public bool CheckSuspensionComboboxes(int indexVehicle)
        {
            if ((comboBoxSCFL.SelectedItem != null) && (comboBoxSCFR.SelectedItem != null) && (comboBoxSCRL.SelectedItem != null) && (comboBoxSCRR.SelectedItem != null))
            {
                M1_Global.vehicleGUI[indexVehicle].SuspensionIsAssembled_GUI = true;
                return true;
            }
            else
            {
                M1_Global.vehicleGUI[indexVehicle].SuspensionIsAssembled_GUI = false;
                return false;
            }
        }

        /// <summary>
        /// Checks if the Vehicle's Tire is assembled
        /// </summary>
        /// <returns>Booloean</returns>
        public bool CheckTireComboboxes(int indexVehicle)
        {
            if ((comboBoxTireFL.SelectedItem != null) && (comboBoxTireFR.SelectedItem != null) && (comboBoxTireRL.SelectedItem != null) && (comboBoxTireRR.SelectedItem != null))
            {
                M1_Global.vehicleGUI[indexVehicle].TireIsAssembled_GUI = true;
                return true;
            }
            else
            {
                M1_Global.vehicleGUI[indexVehicle].TireIsAssembled_GUI = false;
                return false;
            }
        }

        /// <summary>
        /// Checks if the Vehicle's Spring is assembled
        /// </summary>
        /// <returns>Booloean</returns>
        public bool CheckSpringComboboxes(int indexVehicle)
        {
            if ((comboBoxSpringFL.SelectedItem != null) && (comboBoxSpringFR.SelectedItem != null) && (comboBoxSpringRL.SelectedItem != null) && (comboBoxSpringRR.SelectedItem != null))
            {
                M1_Global.vehicleGUI[indexVehicle].SpringIsAssembled_GUI = true;
                return true;
            }
            else
            {
                M1_Global.vehicleGUI[indexVehicle].SpringIsAssembled_GUI = false;
                return false;
            }
        }

        /// <summary>
        /// Checks if the Vehicle's Damper is assembled
        /// </summary>
        /// <returns>Booloean</returns>
        public bool CheckDamperComboboxes(int indexVehicle)
        {
            if ((comboBoxDamperFL.SelectedItem != null) && (comboBoxDamperFR.SelectedItem != null) && (comboBoxDamperRL.SelectedItem != null) && (comboBoxDamperRR.SelectedItem != null))
            {
                M1_Global.vehicleGUI[indexVehicle].DamperIsAssembled_GUI = true;
                return true;
            }
            else
            {
                M1_Global.vehicleGUI[indexVehicle].DamperIsAssembled_GUI = false;
                return false;
            }
        }

        /// <summary>
        /// Checks if the Vehicle's Chassis is assembled
        /// </summary>
        /// <returns>Booloean</returns>
        public bool CheckChassisComboboxes(int indexVehicle)
        {
            if (comboBoxChassis.SelectedItem != null)
            {
                M1_Global.vehicleGUI[indexVehicle].ChassisIsAssembled_GUI = true;
                return true;
            }
            else
            {
                M1_Global.vehicleGUI[indexVehicle].ChassisIsAssembled_GUI = false;
                return false;
            }
        }

        /// <summary>
        /// Checks if the Vehicle's Wheel Alignment is assembled
        /// </summary>
        /// <returns>Booloean</returns>
        public bool CheckWAComboboxes(int indexVehicle)
        {
            if ((comboBoxWAFL.SelectedItem != null) && (comboBoxWAFR.SelectedItem != null) && (comboBoxWARL.SelectedItem != null) && (comboBoxWARR.SelectedItem != null))
            {
                M1_Global.vehicleGUI[indexVehicle].WAIsAssembled_GUI = true;
                return true;
            }
            else
            {
                M1_Global.vehicleGUI[indexVehicle].WAIsAssembled_GUI = true;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Creates a new navBarItem for the Vehicle
        /// </summary>
        /// <param name="_i_vehicle"></param>
        /// 
        private void CreateVehicleNavBarItems(int _i_vehicle)
        {
            #region Creating a new NavBarItem and adding it to the Vehicle Group
            navBarItemVehicleClass temp_navBarItemVehicle = new navBarItemVehicleClass();
            navBarVehicle_Global.CreateNewNavBarItem(_i_vehicle, temp_navBarItemVehicle, navBarControlDesign, navBarGroupVehicle);
            navBarItemVehicleClass.navBarItemVehicle[_i_vehicle].LinkClicked += new NavBarLinkEventHandler(navBarItemVehicle_LinkClicked);
            progressBar.PerformStep();
            progressBar.Update();

            #endregion
        }

        private void barButtonVehicleItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateNewVehicleItem(VehicleVisualizationType.Generic);
        }

        private void barButtonImportVehicleModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateNewVehicleItem(VehicleVisualizationType.ImportedCAD);
            VehicleGUI.InitializeImportCADForm(this);
        }

        public void CreateNewVehicleItem(/*bool _calledByImportVehicleButton*/VehicleVisualizationType vVisualizationType)
        {
            #region GUI
            gridControl2.Hide();
            accordionControlVehicleItem.Hide();
            navBarGroupDesign.Visible = true;
            accordionControlVehicleItem.Show();
            accordionControlVehicleItem.BringToFront();
            navBarGroupVehicle.Expanded = true;

            defaultLookAndFeel1.LookAndFeel.SkinName = SkinName;

            progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 10, 1);
            progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
            progressBar.Show();
            #endregion

            navBarControlDesign.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_vehicle = 0; i_vehicle <= navBarItemVehicleClass.navBarItemVehicle.Count; i_vehicle++)
            {
                if (Vehicle.VehicleCounter == i_vehicle)
                {
                    #region Creating a new NavBarItem and adding it to the Vehicle Group
                    CreateVehicleNavBarItems(i_vehicle);
                    break;
                    #endregion

                }
            }

            for (int l_vehicle = 0; l_vehicle <= Vehicle.List_Vehicle.Count; l_vehicle++)
            {
                if (Vehicle.VehicleCounter == l_vehicle)
                {
                    M1_Global.vehicleGUI.Insert(l_vehicle, new VehicleGUI(this, vVisualizationType));
                    try
                    {
                        #region Assembling the Vehicle and creating a Vehicle Item
                        InputSheet I1 = new InputSheet(this);

                        //
                        // Passing the local Input Sheet Object to the Global List of Input Sheet
                        //
                        M1_Global.List_I1.Insert(l_vehicle, I1);
                        InputSheet.InputSheetCounter++;

                        //
                        // Checking if all the Input Parameters have been assembled
                        //
                        #region Checking if all Input Parameters have been assembled

                        Vehicle vehicleList = ValidateVehicleAssembly(l_vehicle);
                        V1_Global = vehicleList;
                        V1_Global.CreateNewVehicle(l_vehicle, vehicleList);
                        Vehicle.CurrentVehicleID = Vehicle.List_Vehicle[l_vehicle].VehicleID;
                        UndoObject.Identifier(Vehicle.List_Vehicle[l_vehicle]._UndocommandsVehicle, Vehicle.List_Vehicle[l_vehicle]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[l_vehicle].VehicleIsModified);

                        M1_Global.vehicleGUI[l_vehicle]._VehicleGUIName = Vehicle.List_Vehicle[l_vehicle]._VehicleName;
                        M1_Global.vehicleGUI[l_vehicle]._VehicleID = Vehicle.List_Vehicle[l_vehicle].VehicleID;

                        progressBar.PerformStep();
                        progressBar.Update();

                        Vehicle.VehicleCounter++; // This is a static counter which keeps track of the number of Vehicle Items created

                        M1_Global.vehicleGUI[l_vehicle].VehicleCADPreProcessor(M1_Global.vehicleGUI[l_vehicle], l_vehicle, false, M1_Global.vehicleGUI[l_vehicle].CadIsTobeImported, Vehicle.List_Vehicle[l_vehicle].SuspensionIsAssembled);


                        ChangeTracker++;
                        break;
                        #endregion

                        #endregion
                    }
                    catch (Exception E)
                    {
                        string s = E.Message;
                        MessageBox.Show("Unexpected Error during Vehicle Assembly");
                        navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                        navBarControlDesign.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                        navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);
                        accordionControlVehicleItem.Hide();
                        sidePanel2.Hide();
                        progressBar.Hide();
                        return;
                    }

                }
            }

            ComboboxSimulationVehicleOperations();
            ComboboxBatchRunVehicleOperations();
            progressBar.Hide();

            sidePanel2.Show();
            groupControl13.Show();
            defaultLookAndFeel1.LookAndFeel.SkinName = SkinName;
        }

        /// <summary>
        /// Event which is fired when the Validate Vehicle Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonValidateVehic_Click(object sender, EventArgs e)
        {
            ///<summary>Initalizing the Progress Bar</summary>
            progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 10, 1);
            progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
            progressBar.Show();

            ///<summary>Obtaining the index of the Vehicle</summary>
            int index = navBarGroupVehicle.SelectedLinkIndex;
            ///<summary>Assembling and validating the Vehicle. <seealso cref="ValidateVehicleAssembly(int)"/></summary>
            Vehicle.List_Vehicle[index] = ValidateVehicleAssembly(index);
            if (Vehicle.List_Vehicle[index].ValidateAssembly(out string Error))
            {
                //M1_Global.vehicleGUI[index].VehicleCADPreProcessor(M1_Global.vehicleGUI[index], index, false, M1_Global.vehicleGUI[index].CadIsTobeImported, Vehicle.List_Vehicle[index].SuspensionIsAssembled);
                //M1_Global.vehicleGUI[index].EditORCreateVehicleCAD(M1_Global.vehicleGUI[index].CADVehicleInputs,index,true,)
                UpdateVehicle(index, Vehicle.List_Vehicle[index]);
                //simpleButtonValidate_Disabler(index);

            }
            else
            {
                ///<summary>
                ///Vehicle updated inside the Else loop also. 
                ///Here IF Else LOOP employed only to show user the message
                /// </summary>
                UpdateVehicle(index, Vehicle.List_Vehicle[index]);
                MessageBox.Show(Error);
                progressBar.Hide();
                return;
            }
            progressBar.Hide();
        }

        private void UpdateVehicle(int l_vehicle, Vehicle vUpdate)
        {
            try
            {
                #region Assembling the Vehicle and creating a Vehicle Item

                InputSheet I1 = new InputSheet(this);

                //
                // Passing the local Input Sheet Object to the Global List of Input Sheet
                //

                M1_Global.List_I1[l_vehicle] = I1;

                //
                // Passing the parameters to VEHICLE ASSEMBLY method where a new Vehicle Object will be Initialized. This object will then be returned using an out parameter of type Vehicle 
                //
                //Vehicle vehicle_list = ValidateVehicleAssembly(l_vehicle);
                V1_Global = vUpdate;
                vUpdate._VehicleName = M1_Global.vehicleGUI[l_vehicle]._VehicleGUIName;
                vUpdate.VehicleID = M1_Global.vehicleGUI[l_vehicle]._VehicleID;
                V1_Global.ModifyObjectData(l_vehicle, vUpdate, false);
                Vehicle.CurrentVehicleID = Vehicle.List_Vehicle[l_vehicle].VehicleID;

                UndoObject.Identifier(Vehicle.List_Vehicle[l_vehicle]._UndocommandsVehicle, Vehicle.List_Vehicle[l_vehicle]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[l_vehicle].VehicleIsModified);
                M1_Global.vehicleGUI[l_vehicle]._VehicleGUIName = Vehicle.List_Vehicle[l_vehicle]._VehicleName;
                DeleteNavBarControlResultsGroupANDTabPages(l_vehicle);

                if (Vehicle.List_Vehicle[l_vehicle].SuspensionIsAssembled)
                {
                    if (M1_Global.vehicleGUI[l_vehicle].VisualizationType == VehicleVisualizationType.Generic)
                    {
                        EditVehicleCAD(M1_Global.vehicleGUI[l_vehicle].CADVehicleInputs, l_vehicle, true, M1_Global.vehicleGUI[l_vehicle].CadIsTobeImported, M1_Global.vehicleGUI[l_vehicle].PlotWheel);

                    }
                    else if (M1_Global.vehicleGUI[l_vehicle].VisualizationType == VehicleVisualizationType.ImportedCAD)
                    {
                        EditVehicleCAD(M1_Global.vehicleGUI[l_vehicle].importCADForm.importCADViewport, l_vehicle, true, M1_Global.vehicleGUI[l_vehicle].CadIsTobeImported, M1_Global.vehicleGUI[l_vehicle].PlotWheel);
                    }

                    //M1_Global.vehicleGUI[l_vehicle].CADVehicleInputs.viewportLayout1.ZoomFit();
                }

                ChangeTracker++;

                simpleButtonValidate_Disabler(l_vehicle);

                ComboboxSimulationVehicleOperations();
                ComboboxBatchRunVehicleOperations();
                #endregion

            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected Error during Vehicle Modification");
            }
        }

        private void simpleButtonValidate_Disabler(int indexVehicle)
        {
            if (Vehicle.List_Vehicle[indexVehicle].VehicleHasBeenValidated)
            {
                simpleButtonValidateVehic.Enabled = false;
            }
            else if (!Vehicle.List_Vehicle[indexVehicle].VehicleHasBeenValidated)
            {
                simpleButtonValidateVehic.Enabled = true;
            }
        }

        /// <summary>
        /// Method to Assemble the Input Items, Assemble th Vehicle and Validate it
        /// </summary>
        /// <param name="indexVehicle"></param>
        private Vehicle ValidateVehicleAssembly(int indexVehicle)
        {
            ///<summary>Assembling the Input Items</summary>
            AssembleInputItems(indexVehicle);
            ///<summary>Assembling the Input items ONTO the Vehicle</summary>
            AssembleVehicle(M1_Global.Assy_SCM, Tire.Assy_Tire, Spring.Assy_Spring, Damper.Assy_Damper, AntiRollBar.Assy_ARB, Chassis.Assy_Chassis, WheelAlignment.Assy_WA, M1_Global.Assy_OC, out Vehicle _vehicleList);
            ///<summary>Validaring the Vehicle</summary>
            if (_vehicleList.ValidateAssembly(out string Error))
            {
                _vehicleList.VehicleHasBeenValidated = true;
                M1_Global.vehicleGUI[indexVehicle].VehicleHasBeenValidated_GUI = true;
            }

            return _vehicleList;
        }
        /// <summary>
        /// Method to assign the Input Items
        /// </summary>
        /// <param name="l_vehicle"></param>
        private void AssembleInputItems(int l_vehicle)
        {
            ///<summary>Initalizing the Progress Bar</summary>
            //progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 10, 1);
            //progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
            progressBar.Show();

            #region Suspension Assembly
            if (CheckSuspensionComboboxes(l_vehicle))
            {
                #region Assembling the Suspension Coordinates which User has selected

                M1_Global.Assy_SCM[0] = (SuspensionCoordinatesFront)comboBoxSCFL.SelectedItem;

                M1_Global.Assy_SCM[1] = (SuspensionCoordinatesFrontRight)comboBoxSCFR.SelectedItem;

                #region Checking if the Front Left and Right have the same Geometry Type
                if (((M1_Global.Assy_SCM[0].McPhersonIdentifierFront != M1_Global.Assy_SCM[1].McPhersonIdentifierFront) || (M1_Global.Assy_SCM[1].McPhersonIdentifierFront != M1_Global.Assy_SCM[0].McPhersonIdentifierFront)) &&
                   ((M1_Global.Assy_SCM[0].PullrodIdentifierFront != M1_Global.Assy_SCM[1].PullrodIdentifierFront) || (M1_Global.Assy_SCM[1].PullrodIdentifierFront != M1_Global.Assy_SCM[0].PullrodIdentifierFront)) &&
                   ((M1_Global.Assy_SCM[0].TARBIdentifierFront != M1_Global.Assy_SCM[1].TARBIdentifierFront) || (M1_Global.Assy_SCM[1].TARBIdentifierFront != M1_Global.Assy_SCM[0].TARBIdentifierFront)))
                {
                    MessageBox.Show("Please Select the same Suspension Type for the Front Left and Front Right Corner");

                    return;
                }
                #endregion

                M1_Global.Assy_SCM[2] = (SuspensionCoordinatesRear)comboBoxSCRL.SelectedItem;

                M1_Global.Assy_SCM[3] = (SuspensionCoordinatesRearRight)comboBoxSCRR.SelectedItem;

                #region Checking if the Rear Left and Right have the same Geometry Type
                if (((M1_Global.Assy_SCM[2].McPhersonIdentifierRear != M1_Global.Assy_SCM[3].McPhersonIdentifierRear) || (M1_Global.Assy_SCM[3].McPhersonIdentifierRear != M1_Global.Assy_SCM[2].McPhersonIdentifierRear)) &&
                   ((M1_Global.Assy_SCM[2].PullrodIdentifierRear != M1_Global.Assy_SCM[3].PullrodIdentifierRear) || (M1_Global.Assy_SCM[3].PullrodIdentifierRear != M1_Global.Assy_SCM[2].PullrodIdentifierRear)) &&
                   ((M1_Global.Assy_SCM[2].TARBIdentifierRear != M1_Global.Assy_SCM[3].TARBIdentifierRear) || (M1_Global.Assy_SCM[3].TARBIdentifierRear != M1_Global.Assy_SCM[2].TARBIdentifierRear)))
                {
                    MessageBox.Show("Please Select the same Suspension Type for the Rear Left and Front Right Corner");

                    return;
                }
                #endregion

                if (M1_Global.vehicleGUI[l_vehicle].CadIsTobeImported)
                {
                    M1_Global.vehicleGUI[l_vehicle].importCADForm.SuspensionIsCreated_Form = true;
                }

                #endregion
                progressBar.PerformStep();
                progressBar.Update();
                //Assy_Checker++;
                M1_Global.vehicleGUI[l_vehicle].SuspensionIsAssembled_GUI = true;
            }
            else
            {
                M1_Global.vehicleGUI[l_vehicle].SuspensionIsAssembled_GUI = false;
                progressBar.PerformStep();
                progressBar.Update();
            }
            #endregion

            #region Tire Assembly
            if (CheckTireComboboxes(l_vehicle))
            {
                #region Assembling the Tires which User has selected
                Tire.Assy_Tire[0] = (Tire)comboBoxTireFL.SelectedItem;


                Tire.Assy_Tire[1] = (Tire)comboBoxTireFR.SelectedItem;


                Tire.Assy_Tire[2] = (Tire)comboBoxTireRL.SelectedItem;


                Tire.Assy_Tire[3] = (Tire)comboBoxTireRR.SelectedItem;


                #endregion
                //Assy_Checker++;
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].TireIsAssembled_GUI = true;
            }
            else
            {
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].TireIsAssembled_GUI = false;
            }
            #endregion

            #region Spring Assembly
            if (CheckSpringComboboxes(l_vehicle))
            {
                #region Assembling the Springs which the User has Selected
                Spring.Assy_Spring[0] = (Spring)comboBoxSpringFL.SelectedItem;
                Spring.Assy_Spring[1] = (Spring)comboBoxSpringFR.SelectedItem;
                Spring.Assy_Spring[2] = (Spring)comboBoxSpringRL.SelectedItem;
                Spring.Assy_Spring[3] = (Spring)comboBoxSpringRR.SelectedItem;
                #endregion
                //Assy_Checker++;
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].SpringIsAssembled_GUI = true;
            }
            else
            {
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].SpringIsAssembled_GUI = false;
            }
            #endregion

            #region Damper Assembly
            if (CheckDamperComboboxes(l_vehicle))
            {
                #region Assembling the Dampers which the user has selected
                Damper.Assy_Damper[0] = (Damper)comboBoxDamperFL.SelectedItem;
                Damper.Assy_Damper[1] = (Damper)comboBoxDamperFR.SelectedItem;
                Damper.Assy_Damper[2] = (Damper)comboBoxDamperRL.SelectedItem;
                Damper.Assy_Damper[3] = (Damper)comboBoxDamperRR.SelectedItem;
                #endregion
                //Assy_Checker++;
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].DamperIsAssembled_GUI = true;
            }
            else
            {
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].DamperIsAssembled_GUI = false;
            }
            #endregion

            #region Anti-Roll Bar Assembly
            if (comboBoxARBFront.SelectedItem != null)
            {
                #region Assembling the Anti-Roll Bars which the user has selected
                AntiRollBar.Assy_ARB[0] = (AntiRollBar)comboBoxARBFront.SelectedItem;
                AntiRollBar.Assy_ARB[1] = (AntiRollBar)comboBoxARBFront.SelectedItem;

                #endregion

                //Assy_Checker += 0.5;
            }
            else if (comboBoxARBFront.SelectedItem == null)
            {
                //DialogResult result = MessageBox.Show("Run Simulation without Front Anti-Roll Bar?");
                //if (result == DialogResult.OK)
                //{
                arbGUI.Insert(l_vehicle, new AntiRollBarGUI());
                Default_Values.ARBDefaultValues2(arbGUI[l_vehicle]);
                arbGUI[l_vehicle].Update_ARBGUI(this, l_vehicle);

                AntiRollBar.Assy_ARB[0] = new AntiRollBar(arbGUI[l_vehicle]);
                AntiRollBar.Assy_ARB[0].AntiRollBarRate = 0;

                AntiRollBar.Assy_ARB[1] = new AntiRollBar(arbGUI[l_vehicle]);
                AntiRollBar.Assy_ARB[1].AntiRollBarRate = 0;
                //Assy_Checker += 0.5;
                //}
            }
            if (comboBoxARBRear.SelectedItem != null)
            {
                #region Assembling the Rear Anti-Roll Bar whcih the user has selected
                AntiRollBar.Assy_ARB[2] = (AntiRollBar)comboBoxARBRear.SelectedItem;
                AntiRollBar.Assy_ARB[3] = (AntiRollBar)comboBoxARBRear.SelectedItem;

                #endregion
                //Assy_Checker += 0.5;

            }
            else if (comboBoxARBRear.SelectedItem == null)
            {
                //DialogResult result = MessageBox.Show("Run Simulation without Rear Anti-Roll Bar?");
                //if (result == DialogResult.OK)
                //{
                arbGUI.Insert(l_vehicle, new AntiRollBarGUI());
                Default_Values.ARBDefaultValues2(arbGUI[l_vehicle]);
                arbGUI[l_vehicle].Update_ARBGUI(this, l_vehicle);

                AntiRollBar.Assy_ARB[2] = new AntiRollBar(arbGUI[l_vehicle]);
                AntiRollBar.Assy_ARB[2].AntiRollBarRate = 0;

                AntiRollBar.Assy_ARB[3] = new AntiRollBar(arbGUI[l_vehicle]);
                AntiRollBar.Assy_ARB[3].AntiRollBarRate = 0;
                //Assy_Checker += 0.5;
                //}
            }
            progressBar.PerformStep();
            progressBar.Update();
            M1_Global.vehicleGUI[l_vehicle].ARBIsAssembled_GUI = true;
            #endregion

            #region Chassis Assembly
            if (CheckChassisComboboxes(l_vehicle))
            {
                #region Assembling the Chassis which the user has selected
                Chassis.Assy_Chassis = (Chassis)comboBoxChassis.SelectedItem;
                #endregion
                //Assy_Checker++;
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].ChassisIsAssembled_GUI = true;
            }
            else
            {
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].ChassisIsAssembled_GUI = false;
            }
            #endregion

            #region WheelAlignment Assembly
            if (CheckWAComboboxes(l_vehicle))
            {
                #region Assembling the Wheel Alignment which the user has selected
                WheelAlignment.Assy_WA[0] = (WheelAlignment)comboBoxWAFL.SelectedItem;
                WheelAlignment.Assy_WA[1] = (WheelAlignment)comboBoxWAFR.SelectedItem;
                WheelAlignment.Assy_WA[2] = (WheelAlignment)comboBoxWARL.SelectedItem;
                WheelAlignment.Assy_WA[3] = (WheelAlignment)comboBoxWARR.SelectedItem;
                #endregion
                //Assy_Checker++;
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].WAIsAssembled_GUI = true;
            }
            else
            {
                progressBar.PerformStep();
                progressBar.Update();
                M1_Global.vehicleGUI[l_vehicle].WAIsAssembled_GUI = false;
            }
            #endregion

            #region Output Class
            #region New Output Instance -  - Front Left, Front Right, Rear Left, Rear Right
            OutputClass ocfl = new OutputClass();
            OutputClass ocfr = new OutputClass();
            OutputClass ocrl = new OutputClass();
            OutputClass ocrr = new OutputClass();
            #endregion

            #region Initialization of the Global Array of OutputClass Object
            M1_Global.Assy_OC = new OutputClass[4];
            M1_Global.Assy_OC[0] = ocfl;
            M1_Global.Assy_OC[1] = ocfr;
            M1_Global.Assy_OC[2] = ocrl;
            M1_Global.Assy_OC[3] = ocrr;
            #endregion

            progressBar.PerformStep();
            progressBar.Update();
            #endregion

        }

        private void InputComboboxes_Leave(object sender, EventArgs e)
        {
            int index = navBarGroupVehicle.SelectedLinkIndex;
            defaultLookAndFeel1.LookAndFeel.SkinName = SkinName;

            Vehicle.List_Vehicle[index].VehicleHasBeenValidated = false;
            simpleButtonValidate_Disabler(index);
        }

        #region Method to exclusively edit the Vehicle CAD
        public static void EditVehicleCAD(CAD cadControl, int Index, bool IsInputControl, bool ImportCAD, bool PlotWheel) => M1_Global.vehicleGUI[Index].EditORCreateVehicleCAD(cadControl, Index, IsInputControl, M1_Global.vehicleGUI[Index].Vehicle_MotionExists, 0, true, ImportCAD, PlotWheel);
        #endregion

        public static void DeleteNavBarControlResultsGroupANDTabPages_Invoker(int _positionOfDelete)
        {
            //
            // This method is created so that the Vehicle Class can call the DeleteNavBarControlResultsGroupANDTabPages method in a static way
            //
            R1 = R1.FormVariableUpdater();
            R1.DeleteNavBarControlResultsGroupANDTabPages(_positionOfDelete);
        }

        private void DeleteNavBarControlResultsGroupANDTabPages(int PositionOfDelete)
        {
            try
            {
                if (Vehicle.List_Vehicle[PositionOfDelete].Vehicle_Results_Tracker == 1)
                {
                    int IndexofDelete;

                    TabControl_Outputs = CustomXtraTabPage.ClearTabPages(TabControl_Outputs, M1_Global.vehicleGUI[PositionOfDelete].TabPages_Vehicle);

                    foreach (NavBarGroup item in navBarControlResults.Groups)
                    {
                        if (item.Name == M1_Global.vehicleGUI[PositionOfDelete].navBarGroup_Vehicle_Result.Name)
                        {
                            navBarControlResults.Groups[item.Name].ItemLinks.Clear();
                            navBarControlResults.Groups.Remove(item);

                            for (int i_navControlClear = 0; i_navControlClear < M1_Global.vehicleGUI[PositionOfDelete].navBarItem_Vehicle_Results.Count; i_navControlClear++)
                            {
                                IndexofDelete = navBarControlResults.Items.IndexOf(M1_Global.vehicleGUI[PositionOfDelete].navBarItem_Vehicle_Results[i_navControlClear]);
                                navBarControlResults.Items.RemoveAt(IndexofDelete);
                            }

                            M1_Global.vehicleGUI[PositionOfDelete].navBarItem_Vehicle_Results.Clear();

                        }
                    }
                }

            }
            catch (Exception) { }
        }

        public static void comboBoxVehicle_Leave_Invoker()
        {
            R1 = R1.FormVariableUpdater();
            object sender = new object();
            EventArgs e = new EventArgs();

            R1.comboBoxVehicle_Leave(sender, e);
        }

        private void comboBoxVehicle_Leave(object sender, EventArgs e)
        {
            try
            {
                int index = navBarGroupSimulation.SelectedLinkIndex;

                Vehicle.Assembled_Vehicle = (Vehicle)Simulation.List_Simulation[index].simulationPanel.comboBoxVehicle.SelectedItem;

                if (Vehicle.Assembled_Vehicle.McPhersonFront == 1)
                {
                    ribbonPageGroupRecalculate.Enabled = false;
                    return;
                }
                else if (Vehicle.Assembled_Vehicle.McPhersonRear == 1)
                {
                    ribbonPageGroupRecalculate.Enabled = false;
                    return;
                }

                if (Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker == 1)
                {
                    ribbonPageGroupRecalculate.Enabled = true;
                }
                else if (Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker == 0)
                {
                    ribbonPageGroupRecalculate.Enabled = false;
                }


                ChangeTracker++;

            }
            catch (Exception)
            {


            }
        }

        void navBarItemVehicle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupVehicle.SelectedLinkIndex;
            Vehicle.CurrentVehicleID = index + 1;
            UndoObject.Identifier(Vehicle.List_Vehicle[index]._UndocommandsVehicle, Vehicle.List_Vehicle[index]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[index].VehicleIsModified);

            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            gridControl2.Hide();
            accordionControlVehicleItem.Hide();
            navBarGroupDesign.Visible = true;
            accordionControlVehicleItem.Show();
            navBarGroupVehicle.Expanded = true;
            #endregion

            for (int c_vehicle = 0; c_vehicle <= navBarItemVehicleClass.navBarItemVehicle.Count; c_vehicle++)
            {
                if (index == c_vehicle)
                {
                    try
                    {
                        #region Populating the Comboboes with the Values which each vehicle item contains
                        if (Vehicle.List_Vehicle[c_vehicle].sc_FL != null && Vehicle.List_Vehicle[c_vehicle].sc_FR != null && Vehicle.List_Vehicle[c_vehicle].sc_RL != null && Vehicle.List_Vehicle[c_vehicle].sc_RR != null)
                        {
                            comboBoxSCFL.Text = Vehicle.List_Vehicle[c_vehicle].sc_FL._SCName;
                            comboBoxSCFR.Text = Vehicle.List_Vehicle[c_vehicle].sc_FR._SCName;
                            comboBoxSCRL.Text = Vehicle.List_Vehicle[c_vehicle].sc_RL._SCName;
                            comboBoxSCRR.Text = Vehicle.List_Vehicle[c_vehicle].sc_RR._SCName;
                        }

                        if (Vehicle.List_Vehicle[c_vehicle].tire_FL != null && Vehicle.List_Vehicle[c_vehicle].tire_FR != null && Vehicle.List_Vehicle[c_vehicle].tire_RL != null && Vehicle.List_Vehicle[c_vehicle].tire_RR != null)
                        {
                            comboBoxTireFL.Text = Vehicle.List_Vehicle[c_vehicle].tire_FL._TireName;
                            comboBoxTireFR.Text = Vehicle.List_Vehicle[c_vehicle].tire_FR._TireName;
                            comboBoxTireRL.Text = Vehicle.List_Vehicle[c_vehicle].tire_RL._TireName;
                            comboBoxTireRR.Text = Vehicle.List_Vehicle[c_vehicle].tire_RR._TireName;
                        }

                        if (Vehicle.List_Vehicle[c_vehicle].spring_FL != null && Vehicle.List_Vehicle[c_vehicle].spring_FR != null && Vehicle.List_Vehicle[c_vehicle].spring_RL != null && Vehicle.List_Vehicle[c_vehicle].spring_RR != null)
                        {
                            comboBoxSpringFL.Text = Vehicle.List_Vehicle[c_vehicle].spring_FL._SpringName;
                            comboBoxSpringFR.Text = Vehicle.List_Vehicle[c_vehicle].spring_FR._SpringName;
                            comboBoxSpringRL.Text = Vehicle.List_Vehicle[c_vehicle].spring_RL._SpringName;
                            comboBoxSpringRR.Text = Vehicle.List_Vehicle[c_vehicle].spring_RR._SpringName;
                        }

                        if (Vehicle.List_Vehicle[c_vehicle].damper_FL != null && Vehicle.List_Vehicle[c_vehicle].damper_FR != null && Vehicle.List_Vehicle[c_vehicle].damper_RL != null && Vehicle.List_Vehicle[c_vehicle].damper_RR != null)
                        {
                            comboBoxDamperFL.Text = Vehicle.List_Vehicle[c_vehicle].damper_FL._DamperName;
                            comboBoxDamperFR.Text = Vehicle.List_Vehicle[c_vehicle].damper_FR._DamperName;
                            comboBoxDamperRL.Text = Vehicle.List_Vehicle[c_vehicle].damper_RL._DamperName;
                            comboBoxDamperRR.Text = Vehicle.List_Vehicle[c_vehicle].damper_RR._DamperName;
                        }

                        comboBoxARBFront.Text = Vehicle.List_Vehicle[c_vehicle].arb_FL._ARBName;
                        comboBoxARBRear.Text = Vehicle.List_Vehicle[c_vehicle].arb_RL._ARBName;

                        if (Vehicle.List_Vehicle[c_vehicle].chassis_vehicle != null)
                        {
                            comboBoxChassis.Text = Vehicle.List_Vehicle[c_vehicle].chassis_vehicle._ChassisName;

                        }
                        if (Vehicle.List_Vehicle[c_vehicle].wa_FL != null && Vehicle.List_Vehicle[c_vehicle].wa_FR != null && Vehicle.List_Vehicle[c_vehicle].wa_RL != null && Vehicle.List_Vehicle[c_vehicle].wa_RR != null)
                        {
                            comboBoxWAFL.Text = Vehicle.List_Vehicle[c_vehicle].wa_FL._WAName;
                            comboBoxWAFR.Text = Vehicle.List_Vehicle[c_vehicle].wa_FR._WAName;
                            comboBoxWARL.Text = Vehicle.List_Vehicle[c_vehicle].wa_RL._WAName;
                            comboBoxWARR.Text = Vehicle.List_Vehicle[c_vehicle].wa_RR._WAName;
                        }
                        #endregion

                        M1_Global.vehicleGUI[c_vehicle].TabPage_VehicleInputCAD.PageVisible = true;
                        int SelectedPage = TabControl_Outputs.TabPages.IndexOf(M1_Global.vehicleGUI[c_vehicle].TabPage_VehicleInputCAD);
                        TabControl_Outputs.SelectedTabPageIndex = SelectedPage;

                        if (M1_Global.vehicleGUI[index].CadIsTobeImported)
                        {
                            //VehicleGUI.InitializeImportCADForm(this); 
                            M1_Global.vehicleGUI[index].importCADForm.Show();
                        }

                        if (M1_Global.vehicleGUI[index].VehicleHasBeenValidated_GUI)
                        {
                            simpleButtonValidateVehic.Enabled = false;
                        }
                        else
                        {
                            simpleButtonValidateVehic.Enabled = true;
                        }

                    }
                    catch (Exception)
                    {

                        //Added for safety
                    }


                }
            }

        }

        public static void ComboboxVehicleOperationsInvoker()
        {
            //
            // This method is created so that the Vehicle Class can call the ComboboxVehicleOperations method in a static way
            //
            R1 = R1.FormVariableUpdater();
            R1.ComboboxSimulationVehicleOperations();
            R1.ComboboxBatchRunVehicleOperations();
        }

        private void ComboboxSimulationVehicleOperations()
        {
            int index = -1;
            try
            {
                for (int i_SimulationList = 0; i_SimulationList < Simulation.List_Simulation.Count; i_SimulationList++)
                {
                    index = Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxVehicle.SelectedIndex;
                    Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxVehicle.Items.Clear();

                    for (int i_combobox_vehicle = 0; i_combobox_vehicle < Vehicle.List_Vehicle.Count; i_combobox_vehicle++)
                    {

                        try
                        {
                            #region Populating the Vehicle comboboxes
                            Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxVehicle.Items.Insert(i_combobox_vehicle, Vehicle.List_Vehicle[i_combobox_vehicle]);
                            Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxVehicle.DisplayMember = "_VehicleName";
                            #endregion

                        }
                        catch (Exception)
                        {
                            // To safeguard the code since the almost always the vehicle will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                            // a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 
                        }
                    }

                    #region Re-assigning the combobox selected item index
                    try
                    {
                        if (index != -1)
                        {
                            Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxVehicle.SelectedIndex = index;
                        }
                        else Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxVehicle.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {
                        // To safeguard against Open command if there is no item in combobox
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                // To safeguard the code since the almost always the vehicle will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                // a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 
            }
            ChangeTracker++;
        }

        /// <summary>
        /// Method to initialize the <see cref="BatchRunForm"/> form's <see cref="BatchRunForm.comboBoxVehicleBatchRun"/> combobox with the Vehicle items which have been created and restore the selected Vehicle Item
        /// </summary>
        public void ComboboxBatchRunVehicleOperations()
        {
            int index = -1;

            for (int i_BR = 0; i_BR < BatchRunGUI.batchRuns_GUI.Count; i_BR++)
            {
                index = BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxVehicleBatchRun.SelectedIndex;
                BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxVehicleBatchRun.Items.Clear();

                for (int i_CB_Vehicle = 0; i_CB_Vehicle < Vehicle.List_Vehicle.Count; i_CB_Vehicle++)
                {
                    BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxVehicleBatchRun.Items.Add(Vehicle.List_Vehicle[i_CB_Vehicle]);
                    BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxVehicleBatchRun.DisplayMember = "_VehicleName";

                }


                if (Vehicle.List_Vehicle.Count != 0)
                {
                    if (index != -1)
                    {
                        BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxVehicleBatchRun.SelectedIndex = index;
                    }
                    else
                    {
                        BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxVehicleBatchRun.SelectedIndex = 0;
                    }
                }
            }
            ChangeTracker++;
        }

        /// <summary>
        /// This method obtains the index of the Comboboxes that the user 
        /// </summary>
        /// <param name="_importCAD">Object of the ImportCADForm</param>
        public void AssignSuspensionComboboxIndex(/*ImportCADForm*/ XUC_ImportCAD _importCAD)
        {
            //comboBoxSCFL.SelectedIndex = _importCAD.comboBoxSuspensionFL.SelectedIndex;
            //comboBoxSCFR.SelectedIndex = _importCAD.comboBoxSuspensionFR.SelectedIndex;
            //comboBoxSCRL.SelectedIndex = _importCAD.comboBoxSuspensionRL.SelectedIndex;
            //comboBoxSCRR.SelectedIndex = _importCAD.comboBoxSuspensionRR.SelectedIndex;
        }

        /// <summary>
        /// Method to assign the igesEntities
        /// </summary>
        /// <param name="_importCAD"></param>
        public void AssignIgesEntities(/*ImportCADForm*/XUC_ImportCAD _importCAD, int _indexVehicle)
        {
            int index =  /*Vehicle.List_Vehicle.Count - 1*/ _indexVehicle;

            //M1_Global.vehicleGUI[index].CadIsTobeImported = true;

            M1_Global.vehicleGUI[index].CADVehicleInputs.igesEntities = _importCAD.importCADViewport.igesEntities;

            M1_Global.vehicleGUI[index].CADVehicleInputs.importedFile = _importCAD.importedFile_Form;

            M1_Global.vehicleGUI[index].CADVehicleInputs.openFileDialog1 = _importCAD.importCADViewport.openFileDialog1;
        }

        #endregion



        #region Assembling the Vehicle by calling the constructor of the Vehicle Class
        public void AssembleVehicle(SuspensionCoordinatesMaster[] _sc, Tire[] _tire, Spring[] _spring, Damper[] _damper, AntiRollBar[] _arb, Chassis _chassis, WheelAlignment[] _wa, OutputClass[] _oc, out Vehicle _vehicle)
        {
            //
            // Naming convention for this method:-
            // _tire is the array of objects of Tire which the user has selected
            // _Tire is the array of objects of Tire which THIS function passes to the Vehicle Class
            //

            int index = navBarGroupVehicle.SelectedLinkIndex;
            string error = null;

            #region Identifier Variables for Each Corner of the Car

            #region FRONT LEFT
            // Simple identifier variable to indicate that this is thr Front Left OR the 1st Corner
            int Identifier_FL = 1;
            #endregion

            #region FRONT RIGHT
            // Simple identifier variable to indicate that this is thr Front Left OR the 1st Corner
            int Identifier_FR = 2;
            #endregion

            #region REAR LEFT
            // Simple identifier variable to indicate that this is thr Rear Left OR the 3rd Corner
            int Identifier_RL = 3;
            #endregion

            #region REAR RIGHT
            // Simple identifier variable to indicate that this is thr Rear Right OR the 4th Corner
            int Identifier_RR = 4;
            #endregion

            #endregion

            #region Creating Array of Objects (The Array consists of 4 cells. Each Cell consists of the instances of the Input Parameters created for each corner.)

            #region Array of Identifier
            int[] Identifier;
            Identifier = new int[4];
            Identifier[0] = Identifier_FL;
            Identifier[1] = Identifier_FR;
            Identifier[2] = Identifier_RL;
            Identifier[3] = Identifier_RR;
            #endregion

            #region Array of Suspension Coordinates Object
            SuspensionCoordinatesMaster[] _SC;
            _SC = new SuspensionCoordinatesMaster[4];
            _SC[0] = _sc[0];
            _SC[1] = _sc[1];
            _SC[2] = _sc[2];
            _SC[3] = _sc[3];
            #endregion

            #region Array of Tire Object
            Tire[] _Tire;
            _Tire = new Tire[4];
            _Tire[0] = _tire[0];
            _Tire[1] = _tire[1];
            _Tire[2] = _tire[2];
            _Tire[3] = _tire[3];
            #endregion

            #region Array of Spring Objects
            Spring[] _Spring;
            _Spring = new Spring[4];
            _Spring[0] = _spring[0];
            _Spring[1] = _spring[1];
            _Spring[2] = _spring[2];
            _Spring[3] = _spring[3];

            #endregion

            #region Array of Damper Objects
            Damper[] _Damper;
            _Damper = new Damper[4];
            _Damper[0] = _damper[0];
            _Damper[1] = _damper[1];
            _Damper[2] = _damper[2];
            _Damper[3] = _damper[3];

            #endregion

            #region Array of Anti-Roll Bar Objects
            AntiRollBar[] _ARB;
            _ARB = new AntiRollBar[4];
            _ARB[0] = _arb[0];
            _ARB[1] = _arb[1];
            _ARB[2] = _arb[2];
            _ARB[3] = _arb[3];
            #endregion

            #region New Chassis Instance
            Chassis chassis1 = _chassis;

            #endregion

            #region Array of Wheel Alignment Objects
            WheelAlignment[] _WA;
            _WA = new WheelAlignment[4];
            _WA[0] = _wa[0];
            _WA[1] = _wa[1];
            _WA[2] = _wa[2];
            _WA[3] = _wa[3];

            #endregion

            #region Array of Output Objects
            OutputClass[] _OC;
            _OC = new OutputClass[4];
            _OC[0] = _oc[0];
            _OC[1] = _oc[1];
            _OC[2] = _oc[2];
            _OC[3] = _oc[3];

            #endregion

            #endregion

            _vehicle = new Vehicle();
            _vehicle.AssignAssemblyValidators(M1_Global.vehicleGUI[index]);
            for (int i_vehicle = 0; i_vehicle <= Vehicle.List_Vehicle.Count; i_vehicle++)
            {
                if (index == i_vehicle)
                {
                    #region Vehicle object
                    Vehicle vehicle1 = new Vehicle(M1_Global.vehicleGUI[index], Identifier, _SC, _Tire, _Spring, _Damper, _ARB, chassis1, _WA, _OC);
                    _vehicle = vehicle1;
                    #endregion  
                }
            }
        }
        #endregion

        #region Method to Update the Form Variable
        private Kinematics_Software_New FormVariableUpdater()
        {
            R1 = this;
            return R1;

        }
        #endregion

        #endregion

        //
        // Input Sheet Population
        //
        #region Handling the Input Sheet

        #region Input Sheet Population Method
        public void PopulateInputSheet(Vehicle _vehicleInputSheetPopulation)
        {
            try
            {
                int local_VehicleID = 0;
                local_VehicleID = _vehicleInputSheetPopulation.VehicleID;
                //if (_vehicleInputSheetPopulation.VehicleID == 0)
                //{
                //    Vehicle.Assembled_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;
                //    local_VehicleID = _vehicleInputSheetPopulation.VehicleID;
                //}
                //else
                //    local_VehicleID = _vehicleInputSheetPopulation.VehicleID;

                int index = OutputIndex;

                #region Population of the Input Sheet
                #region Populating the Input Sheet - Front Left Coordinates
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.A1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.A1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.A1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.B1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.B1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.B1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.C1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.C1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.C1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.D1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.D1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.D1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.N1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.N1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.N1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.Q1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.Q1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.Q1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.I1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.I1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.I1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.JO1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.JO1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.J1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.J1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.H1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.H1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.O1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.O1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.G1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.G1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.F1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.F1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.E1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.E1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.M1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.M1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.K1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.K1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.P1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.P1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1xFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.W1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1yFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.W1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1zFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.W1z)));
                // Link Lengths
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerFrontFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.LowerFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerRearFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.LowerRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperFrontFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.UpperFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperRearFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.UpperRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.PushRodFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.PushRodLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeLinkFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.ToeLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBBladeFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.ARBBladeLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBDroopLinkFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.ARBDroopLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperLengthFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.DamperLength)));
                #endregion
                #region Populating the Input Sheet - Front Left Coordinates
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.A1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.A1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.A1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.B1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.B1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.B1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.C1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.C1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.C1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.D1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.D1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.D1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.N1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.N1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.N1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.Q1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.Q1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.Q1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.I1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.I1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.I1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.JO1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.JO1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.J1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.J1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.H1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.H1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.O1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.O1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.G1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.G1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.F1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.F1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.E1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.E1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.M1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.M1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.K1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.K1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.P1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.P1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1xFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.W1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1yFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.W1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1zFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.W1z)));
                // Link Lengths
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerFrontFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.LowerFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerRearFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.LowerRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperFrontFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.UpperFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperRearFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.UpperRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.PushRodFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.PushRodLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeLinkFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.ToeLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBBladeFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.ARBBladeLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBDroopLinkFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.ARBDroopLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperLengthFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.DamperLength)));
                #endregion
                #region Populating the Input Sheet - REAR Left Coordinates
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.A1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.A1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.A1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.B1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.B1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.B1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.C1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.C1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.C1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.D1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.D1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.D1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.N1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.N1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.N1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.Q1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.Q1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.Q1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.I1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.I1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.I1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.JO1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.JO1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.J1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.J1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.H1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.H1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.O1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.O1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.G1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.G1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.F1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.F1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.E1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.E1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.M1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.M1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.K1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.K1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.P1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.P1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1xRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.W1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1yRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.W1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1zRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.W1z)));
                // Link Lengths
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerFrontRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.LowerFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerRearRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.LowerRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperFrontRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.UpperFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperRearRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.UpperRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.PushRodRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.PushRodLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeLinkRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.ToeLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBBladeRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.ARBBladeLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBDroopLinkRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.ARBDroopLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperLengthRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.DamperLength)));
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Coordinates
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.A1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.A1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.A1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.B1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.B1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.B1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.C1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.C1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.C1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.C1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.D1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.D1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.D1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.D1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.N1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.N1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.N1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.N1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.Q1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.Q1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.Q1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.Q1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.I1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.I1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.I1z)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.JO1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.JO1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.JO1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.J1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.J1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.J1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.H1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.H1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.O1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.O1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.G1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.G1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.F1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.F1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.E1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.E1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.E1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.M1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.M1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.M1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.K1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.K1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.K1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.P1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.P1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.P1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1xRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.W1x)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1yRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.W1y)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.W1zRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.W1z)));
                // Link Lengths
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerFrontRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.LowerFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.LowerRearRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.LowerRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperFrontRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.UpperFrontLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.UpperRearRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.UpperRearLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.PushRodRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.PushRodLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeLinkRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.ToeLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBBladeRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.ARBBladeLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ARBDroopLinkRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.ARBDroopLinkLength)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperLengthRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.DamperLength)));
                #endregion
                #region GUI operations to change the Input Sheet Textboxes and labels
                if (_vehicleInputSheetPopulation.McPhersonFront == 1)
                {
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zFR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zFR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zFR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zFR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zFR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zFR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yFL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zFL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yFR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zFR.Text = "NaN";

                }

                if (_vehicleInputSheetPopulation.McPhersonRear == 1)
                {
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.A1zRR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.B1zRR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.I1zRR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.H1zRR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.G1zRR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.F1zRR.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yRL.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zRL.Text = "NaN";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1xRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1yRR.Text = "NaN"; M1_Global.vehicleGUI[local_VehicleID - 1].IS.O1zRR.Text = "NaN";
                }

                if (_vehicleInputSheetPopulation.PushRodIdentifierFront == 1 || _vehicleInputSheetPopulation.PushRodIdentifierFront == 0)
                {
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label183.Text = "Pushrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label205.Text = "Pushrod Upright";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label317.Text = "Pushrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label244.Text = "Pushrod Upright";
                }
                if (_vehicleInputSheetPopulation.PullRodIdentifierFront == 1)
                {
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label183.Text = "Pullrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label205.Text = "Pullrod Upright";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label317.Text = "Pullrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label244.Text = "Pullrod Upright";
                }

                if (_vehicleInputSheetPopulation.PushRodIdentifierRear == 1 || _vehicleInputSheetPopulation.PushRodIdentifierRear == 0)
                {
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label277.Text = "Pushrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label272.Text = "Pushrod Upright";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label396.Text = "Pushrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label349.Text = "Pushrod Upright";
                }
                if (_vehicleInputSheetPopulation.PullRodIdentifierRear == 1)
                {
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label277.Text = "Pullrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label272.Text = "Pullrod Upright";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label396.Text = "Pullrod Bell Crank";
                    M1_Global.vehicleGUI[local_VehicleID - 1].IS.label349.Text = "Pullrod Upright";
                }
                #endregion


                #region Populating Input Sheet - FRONT LEFT Tire
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireRateFL.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_FL.TireRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireWidthFL.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_FL.TireWidth);
                #endregion
                #region Populating Input Sheet - FRONT RIGHT Tire
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireRateFR.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_FR.TireRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireWidthFR.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_FR.TireWidth);
                #endregion
                #region Populating Input Sheet - REAR LEFT Tire
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireRateRL.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_RL.TireRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireWidthRL.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_RL.TireWidth);
                #endregion
                #region Populating Input Sheet - REAR RIGHT Tire
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireRateRR.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_RR.TireRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.TireWidthRR.Text = Convert.ToString(_vehicleInputSheetPopulation.tire_RR.TireWidth);
                #endregion


                #region Populating the Input Sheet - FRONT LEFT Spring
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringRateFL.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_FL.SpringRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringPreloadFL.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_FL.SpringPreload);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringFreeLengthFL.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_FL.SpringFreeLength);
                #endregion
                #region Populating the Input Sheet - FRONT RIGHT Spring
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringRateFR.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_FR.SpringRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringPreloadFR.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_FR.SpringPreload);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringFreeLengthFR.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_FR.SpringFreeLength);
                #endregion
                #region Populating the Input Sheet - REAR LEFT Spring
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringRateRL.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_RL.SpringRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringPreloadRL.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_RL.SpringPreload);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringFreeLengthRL.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_RL.SpringFreeLength);
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Spring
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringRateRR.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_RR.SpringRate);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringPreloadRR.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_RR.SpringPreload);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SpringFreeLengthRR.Text = Convert.ToString(_vehicleInputSheetPopulation.spring_RR.SpringFreeLength);
                #endregion


                #region Populating the Input Sheet - FRONT LEFT Damper
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperPressureFL.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_FL.DamperGasPressure);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperShaftDiaFL.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_FL.DamperShaftDia);
                #endregion
                #region Populating the Input Sheet - FRONT RIGHT Damper
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperPressureFR.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_FR.DamperGasPressure);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperShaftDiaFR.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_FR.DamperShaftDia);
                #endregion
                #region Populating the Input Sheet - REAR LEFT Damper
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperPressureRL.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_RL.DamperGasPressure);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperShaftDiaRL.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_RL.DamperShaftDia);
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Damper
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperPressureRR.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_RR.DamperGasPressure);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.DamperShaftDiaRR.Text = Convert.ToString(_vehicleInputSheetPopulation.damper_RR.DamperShaftDia);
                #endregion


                #region Populating the Input Sheet - Rear Anti-Roll Bar
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.RearAntiRollBar.Text = Convert.ToString(_vehicleInputSheetPopulation.arb_RL.AntiRollBarRate);
                #endregion
                #region Populating the Input Sheet - Front Anti-Roll Bar
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.FrontAntiRollBar.Text = Convert.ToString(_vehicleInputSheetPopulation.arb_FL.AntiRollBarRate);
                #endregion


                #region Populating the Input Sheet Chassis Items
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SuspendedMass.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.SuspendedMass);

                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SuspendedMassCoGx.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.SuspendedMassCoGx);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SuspendedMassCoGy.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.SuspendedMassCoGy);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.SuspendedMassCoGz.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.SuspendedMassCoGz);

                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassFL.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFL);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGFLx.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFLCoGx);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGFLy.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFLCoGy);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGFLz.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFLCoGz);

                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassFR.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFR);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGFRx.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFRCoGx);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGFRy.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFRCoGy);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGFRz.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassFRCoGz);

                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassRL.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRL);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGRLx.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRLCoGx);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGRLy.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRLCoGy);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGRLz.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRLCoGz);

                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassRR.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRR);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGRRx.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRRCoGx);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGRRy.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRRCoGy);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.NonSuspendedMassCoGRRz.Text = Convert.ToString(_vehicleInputSheetPopulation.chassis_vehicle.NonSuspendedMassRRCoGz);
                #endregion


                #region Populating the Input Sheet - FRONT LEFT Wheel Alignment
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CamberFL.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_FL.StaticCamber);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeFL.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_FL.StaticToe);
                #endregion
                #region Populating the Input Sheet - FRONT RIGHT Wheel Alignment
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CamberFR.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_FR.StaticCamber);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeFR.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_FR.StaticToe);
                #endregion
                #region Populating the Input Sheet - REAR LEFT Wheel Alignment
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CamberRL.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_RL.StaticCamber);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeRL.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_RL.StaticToe);
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Wheel Alignment
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CamberRR.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_RR.StaticCamber);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.ToeRR.Text = Convert.ToString(_vehicleInputSheetPopulation.wa_RR.StaticToe);
                #endregion


                #region Populating the Input Sheet -MOTION RATIO of All Corners
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.MotionRatioFL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FL.InitialMR)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.MotionRatioFR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_FR.InitialMR)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.MotionRatioRL.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RL.InitialMR)));
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.MotionRatioRR.Text = String.Format("{0:0.000}", ((_vehicleInputSheetPopulation.sc_RR.InitialMR)));
                #endregion


                #region Populating the Input Sheet - Corner Weight of All Corners
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CornerWeightFL.Text = String.Format("{0:00.000}", /*-*/_vehicleInputSheetPopulation.oc_FL[index].CW);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CornerWeightFR.Text = String.Format("{0:00.000}", /*-*/_vehicleInputSheetPopulation.oc_FR[index].CW);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CornerWeightRL.Text = String.Format("{0:00.000}", /*-*/_vehicleInputSheetPopulation.oc_RL[index].CW);
                M1_Global.vehicleGUI[local_VehicleID - 1].IS.CornerWeightRR.Text = String.Format("{0:00.000}", /*-*/_vehicleInputSheetPopulation.oc_RR[index].CW);
                #endregion
                #endregion

            }
            catch (Exception) { }
        }
        #endregion

        #endregion

        #region Selct Vehicle Event
        private void barButtonSelectVehicle_ItemClick(object sender, ItemClickEventArgs e)
        {
            accordionControlVehicleItem.Hide();
            navBarGroupSimulationSetup.Visible = true;
            navBarGroupSimulationSetup.Expanded = true;
            //navBarGroupSimulationSelectVehicle.Expanded = true;
        }
        #endregion

        #region Create Setup Change Event
        private void barButtonSetupChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            navBarGroupSimulationSetup.Visible = true;
            navBarGroupSimulationSetup.Expanded = true;
            navBarGroupSetupChange.Expanded = true;

            int index = SetupChange_GUI._SetupChangeCounter;

            SetupChange_GUI.List_SetupChangeGUI.Insert(index, new SetupChange_GUI("Setup Change", index + 1));
            SetupChange.List_SetupChange.Insert(index, new SetupChange("Setup Change", index + 1));

            //SetupChange.List_SetupChange[index].InitDeltas(SetupChange_GUI.List_SetupChangeGUI[index]);

            SetupChange_GUI.List_SetupChangeGUI[index].HandleGUI(navBarGroupSetupChange, navBarControlSimulation, index);

            ///Combobox Operations
            ComboboxSetupChangeOperations();

            SetupChange.SetupChangeCounter++;
            SetupChange_GUI._SetupChangeCounter++;

            ChangeTracker++;

        }

        public static void ComboboxSetupChangeOperations_Invoker()
        {
            R1 = Kinematics_Software_New.AssignFormVariable();
            R1.ComboboxSetupChangeOperations();
        }

        public void ComboboxSetupChangeOperations()
        {
            int index = -1;

            try
            {
                for (int i_SimList = 0; i_SimList < Simulation.List_Simulation.Count; i_SimList++)
                {
                    index = Simulation.List_Simulation[i_SimList].simulationPanel.comboBoxSetupChange.SelectedIndex;
                    Simulation.List_Simulation[i_SimList].simulationPanel.comboBoxSetupChange.Items.Clear();

                    for (int i_cbSetupC = 0; i_cbSetupC < SetupChange.List_SetupChange.Count; i_cbSetupC++)
                    {
                        try
                        {
                            #region Populating the Setup Change comboboxes
                            Simulation.List_Simulation[i_SimList].simulationPanel.comboBoxSetupChange.Items.Insert(i_cbSetupC, SetupChange.List_SetupChange[i_cbSetupC]);
                            Simulation.List_Simulation[i_SimList].simulationPanel.comboBoxSetupChange.DisplayMember = "SetupChangeName";
                            #endregion
                        }
                        catch (Exception)
                        {
                            // To safeguard the code since the almost always the vehicle will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                            // a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 
                        }
                    }

                    #region Re-assigning the combobox selected item index
                    try
                    {
                        if (index != -1)
                        {
                            Simulation.List_Simulation[i_SimList].simulationPanel.comboBoxSetupChange.SelectedIndex = index;
                        }
                        else Simulation.List_Simulation[i_SimList].simulationPanel.comboBoxSetupChange.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {
                        // To safeguard against Open command if there is no item in combobox
                    }
                    #endregion


                }
            }
            catch (Exception)
            {
                // To safeguard the code since the almost always the vehicle will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                // a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 
            }
        }

        #endregion

        #region Create Motion Event

        private void barButtonItemCreateMotion_ItemClick(object sender, ItemClickEventArgs e)
        {
            navBarGroupSimulationSetup.Visible = true;
            navBarGroupSimulationSetup.Expanded = true;
            navBarGroupMotion.Expanded = true;

            int index = MotionGUI._MotionGUICounter;

            MotionGUI.List_MotionGUI.Insert(index, new MotionGUI("Motion", index + 1, this));
            Motion.List_Motion.Insert(index, new Motion("Motion", index + 1));

            MotionGUI.List_MotionGUI[index].HandleGUI(navBarGroupMotion, navBarControlSimulation, this, index);
            Motion.List_Motion[index].GetWheelDeflectionAndSteer(MotionGUI.List_MotionGUI[index], true, false, false);

            comboBoxSimulationMotionOperations();
            ComboboxBatchRunVehicleOperations();

            Motion.MotionCounter++;
            MotionGUI._MotionGUICounter++;

            ChangeTracker++;
        }

        public static void comboBoxMotionOperations_Invoker()
        {
            R1.FormVariableUpdater();
            R1.comboBoxSimulationMotionOperations();
            R1.ComboboxBatchRunVehicleOperations();
        }

        #region Motion Combobox Operations
        private void comboBoxSimulationMotionOperations()
        {
            int index = -1;

            try
            {
                for (int i_SimulationList = 0; i_SimulationList < Simulation.List_Simulation.Count; i_SimulationList++)
                {
                    index = Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxMotion.SelectedIndex;
                    Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxMotion.Items.Clear();

                    for (int i_comboBox_Motion = 0; i_comboBox_Motion < Motion.List_Motion.Count; i_comboBox_Motion++)
                    {
                        try
                        {
                            #region Populating the Motion comboboxes
                            Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxMotion.Items.Insert(i_comboBox_Motion, Motion.List_Motion[i_comboBox_Motion]);
                            Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxMotion.DisplayMember = "MotionName";
                            #endregion
                        }
                        catch (Exception)
                        {
                            // To safeguard the code since the almost always the vehicle will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                            // a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 

                        }
                    }

                    #region Re-assigning the combobox selected item index
                    try
                    {
                        if (index != -1)
                        {
                            Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxMotion.SelectedIndex = index;
                        }
                        else Simulation.List_Simulation[i_SimulationList].simulationPanel.comboBoxMotion.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {
                        // To safeguard against Open command if there is no item in combobox
                    }
                    #endregion

                }

                ChangeTracker++;
            }
            catch (Exception)
            {

                // To safeguard the code since the almost always the vehicle will be created before the "Create Simulation" button is pressed. Hence, this try block will prevent
                // a run time exception as an object of type SImulation and hence an object of the SImulationPanel Usercontrol is created only when the "Create SImulation" button is pressed. 
            }

        }

        public void comboBoxBatchRunMotionOperations()
        {
            int index = -1;

            for (int i_BR = 0; i_BR < BatchRunGUI.batchRuns_GUI.Count; i_BR++)
            {
                index = BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxMotionBatchRun.SelectedIndex;
                BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxMotionBatchRun.Items.Clear();

                for (int i_CB_Motion = 0; i_CB_Motion < Motion.List_Motion.Count; i_CB_Motion++)
                {
                    BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxMotionBatchRun.Items.Insert(i_CB_Motion, Motion.List_Motion[i_CB_Motion]);
                    BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxMotionBatchRun.DisplayMember = "MotionName";

                }


                if (Motion.List_Motion.Count != 0)
                {
                    if (index != -1)
                    {
                        BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxMotionBatchRun.SelectedIndex = index;
                    }
                    else
                    {
                        BatchRunGUI.batchRuns_GUI[i_BR].batchRun.comboBoxMotionBatchRun.SelectedIndex = 0;
                    }
                }
            }
            ChangeTracker++;
        }

        #endregion

        #endregion

        #region Create Simulation Event

        private void barButtonItemCreateSimulation_ItemClick(object sender, ItemClickEventArgs e)
        {

            navBarGroupSimulationSetup.Visible = true;
            navBarGroupSimulationSetup.Expanded = true;
            navBarGroupSimulation.Expanded = true;

            int index = Simulation.SimulationCounter;
            Simulation.List_Simulation.Insert(index, new Simulation("Simulation", index + 1, this));
            Simulation.List_Simulation[index].HandleGUI(navBarGroupSimulation, navBarControlSimulation, this, index);

            ComboboxSimulationVehicleOperations();
            comboBoxSimulationMotionOperations();
            comboBoxLoadCaseOperations();
            ComboboxSetupChangeOperations();

            Simulation.SimulationCounter++;
            ChangeTracker++;

        }

        #endregion

        #region Handing the Outputs

        #region Determining the Index of the Output to be displayed 
        public void FindOutPutIndex(int _outputIndex)
        {
            OutputIndex = _outputIndex;
        }
        #endregion

        #region Reset of Outputs
        private void ResetOutputs()
        {
            // Add code if necessary 
        }
        #endregion

        #region Populating the Output Class Data Table
        public void PopulateOutputDataTable(Vehicle _vehicleOutputDataTable)
        {
            int index = OutputIndex;
            try
            {
                OutputClassGUI.Rounder_Outputs(_vehicleOutputDataTable.oc_FL[index]);
                OutputClassGUI.Rounder_Outputs(_vehicleOutputDataTable.oc_FR[index]);
                OutputClassGUI.Rounder_Outputs(_vehicleOutputDataTable.oc_RL[index]);
                OutputClassGUI.Rounder_Outputs(_vehicleOutputDataTable.oc_RR[index]);

                _vehicleOutputDataTable.oc_FL[index].OC_SC_DataTable = new DataTable();
                _vehicleOutputDataTable.oc_FR[index].OC_SC_DataTable = new DataTable();
                _vehicleOutputDataTable.oc_RL[index].OC_SC_DataTable = new DataTable();
                _vehicleOutputDataTable.oc_RR[index].OC_SC_DataTable = new DataTable();

                _vehicleOutputDataTable.oc_FL[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_FL[index].InitializeDataTable();
                _vehicleOutputDataTable.oc_FR[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_FR[index].InitializeDataTable();
                _vehicleOutputDataTable.oc_RL[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_RL[index].InitializeDataTable();
                _vehicleOutputDataTable.oc_RR[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_RR[index].InitializeDataTable();

                _vehicleOutputDataTable.oc_FL[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_FL[index].PopulateDataTable(_vehicleOutputDataTable);
                _vehicleOutputDataTable.oc_FR[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_FR[index].PopulateDataTable(_vehicleOutputDataTable);
                _vehicleOutputDataTable.oc_RL[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_RL[index].PopulateDataTable(_vehicleOutputDataTable);
                _vehicleOutputDataTable.oc_RR[index].OC_SC_DataTable = _vehicleOutputDataTable.oc_RR[index].PopulateDataTable(_vehicleOutputDataTable);
            }
            catch (Exception)
            {
                // Incase this function is called by the Open command and there have been no calculations done
            }

        }
        #endregion

        #region Creating NavBarGroups for the Vehicle Outputs
        public void Results_NavBarOerations(Vehicle _vehicleNavBarOperations)
        {
            int index = _vehicleNavBarOperations.VehicleID - 1;

            if (!SameGroupRepeated(_vehicleNavBarOperations))
            {
                ///<summary>Creating a <see cref="NavBarGroup"/> object </summary>
                M1_Global.vehicleGUI[index].navBarGroup_Vehicle_Result = M1_Global.vehicleGUI[index].navBarGroup_Vehicle_Result.CreateNewNavBarGroup_For_VehicleResults(M1_Global.vehicleGUI[index].navBarGroup_Vehicle_Result, navBarControlResults, index + 1, _vehicleNavBarOperations._VehicleName);

                ///<summary>Adding the created <see cref="NavBarGroup"/> object to the <see cref="navBarControlResults"/></summary>
                navBarControlResults.Groups.Add(M1_Global.vehicleGUI[index].navBarGroup_Vehicle_Result);

                ///<summary>Adding a dummy <see cref="NavBarItem"/> object to the <see cref="List{T}"/> of <see cref="VehicleGUI.navBarItem_Vehicle_Results"/></summary>
                M1_Global.vehicleGUI[index].navBarItem_Vehicle_Results.Insert(0, new CusNavBarItem());

                ///<summary>Using the dummy <see cref="NavBarItem"/> to create all the <see cref="VehicleGUI.navBarItem_Vehicle_Results"/>s </summary>
                M1_Global.vehicleGUI[index].navBarItem_Vehicle_Results[0].CreateNavBarItem(M1_Global.vehicleGUI[index].navBarItem_Vehicle_Results, M1_Global.vehicleGUI[index].navBarGroup_Vehicle_Result, navBarControlResults, index + 1, this);

                ///<summary>Removing the dummy <see cref="NavBarItem"/> from the <see cref="VehicleGUI.navBarItem_Vehicle_Results"/> </summary>
                M1_Global.vehicleGUI[index].navBarItem_Vehicle_Results.RemoveAt(M1_Global.vehicleGUI[index].navBarItem_Vehicle_Results.Count - 1);

                ///<summary>Activating the newly added group</summary>
                int groupIndex = navBarControlResults.Groups.IndexOf(M1_Global.vehicleGUI[index].navBarGroup_Vehicle_Result);
                navBarControlResults.ActiveGroup = navBarControlResults.Groups[groupIndex];

            }
            else { }
        }
        #endregion

        #region Method to Catch repetitions
        public bool SameGroupRepeated(Vehicle _vehicleGroupRepeatCheck)
        {

            try
            {
                foreach (NavBarGroup group in navBarControlResults.Groups)
                {
                    if (group.Name == M1_Global.vehicleGUI[_vehicleGroupRepeatCheck.VehicleID - 1].navBarGroup_Vehicle_Result.Name)
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception)
            {
                return false;
                // If catch block is entered then it means that there was nothing inside the navBarControlResults
            }
        }
        #endregion

        #region Display of Outputs
        public void DisplayOutputs(Vehicle _vehicle_OutputDisplay/*int vehicleIndex*/)
        {
            try
            {
                #region Display of Outputs

                int _OPIndex = OutputIndex;

                #region Display of Outputs of FRONT LEFT
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.MotionRatioFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].InitialMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.InitialARBMRFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalMotionRatioFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].FinalMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalARBMRFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].Final_ARB_MR);
                //To Display the New Camber and Toe
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalCamberFL.Text = String.Format("{0:0.000}", -(_vehicle_OutputDisplay.oc_FL[_OPIndex].waOP.StaticCamber * (180 / Math.PI)));
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalToeFL.Text = String.Format("{0:0.000}", (_vehicle_OutputDisplay.oc_FL[_OPIndex].waOP.StaticToe * (180 / Math.PI)));
                //Calculating The Final Ride Height 
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.RideHeightFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].FinalRideHeight);
                //Calculating the New Corner Weights 
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CWFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].CW);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireLoadedRadiusFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedSpringDeflectionFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].Corrected_SpringDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedWheelDeflectionFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].Corrected_WheelDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireDeflectionFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].TireDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.NewDamperLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].New_NonSuspendedMassCoGx);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].New_NonSuspendedMassCoGy);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].New_NonSuspendedMassCoGz);



                #region Displaying the Wishbone Forces
                //Calculating the Wishbone Forces
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink);

                //Chassic Pick Up Points in XYZ direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LowerRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UpperRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].PushRod_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.SpringPreloadOutputFL.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].DamperForce_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkForceFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBDroopLink_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ToeLink_z);

                //Upper and Lower Ball Joint Forces in XYZ Direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightFLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightFLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightFLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].UBJ_z);

                ///<remarks>
                ///Steering Rack and ARB Attachment point forces in XYZ direction
                /// </remarks>
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard1x_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].RackInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard1y_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].RackInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard1z_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].RackInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard2x_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].RackInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard2y_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].RackInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard2z_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].RackInboard2_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1x_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1y_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1z_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2x_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2y_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2z_FL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].ARBInboard2_z);




                #endregion

                #region Displaying the Link Lengths
                // Link Lengths
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerFrontLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.LowerFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerRearLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.LowerRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.UpperFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.UpperRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.PushRodLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ToeLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.ToeLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.ARBDroopLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.DamperLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.DamperLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBLeverLinkLengthFL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FL.ARBBladeLength);
                #endregion

                #endregion

                #region Display of Outputs of FRONT RIGHT
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.MotionRatioFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].InitialMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.InitialARBMRFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalMotionRatioFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].FinalMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalARBMRFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].Final_ARB_MR);
                //To Display the New Camber and Toe
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalCamberFR.Text = String.Format("{0:0.000}", /*-*/(_vehicle_OutputDisplay.oc_FR[_OPIndex].waOP.StaticCamber * (180 / Math.PI)));
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalToeFR.Text = String.Format("{0:0.000}", -(_vehicle_OutputDisplay.oc_FR[_OPIndex].waOP.StaticToe * (180 / Math.PI)));
                //Calculating The Final Ride Height 
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.RideHeightFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].FinalRideHeight);
                //Calculating the New Corner Weights
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CWFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].CW);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireLoadedRadiusFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedSpringDeflectionFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].Corrected_SpringDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedWheelDeflectionFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].Corrected_WheelDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireDeflectionFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].TireDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.NewDamperLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].New_NonSuspendedMassCoGx);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].New_NonSuspendedMassCoGy);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].New_NonSuspendedMassCoGz);

                #region Displaying the Wishbone Forces
                //Calculating the Wishbone Forces
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink);

                //Chassic Pick Up Points in XYZ direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LowerRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UpperRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].PushRod_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.SpringPreloadOutputFR.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].DamperForce_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkForceFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBDroopLink_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ToeLink_z);

                //Upper and Lower Ball Joint Forces in XYZ Direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightFRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightFRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightFRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].UBJ_z);

                ///<remarks>
                ///Steering Rack and ARB Attachment point forces in XYZ direction
                /// </remarks>
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard1x_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].RackInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard1y_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].RackInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard1z_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].RackInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard2x_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].RackInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard2y_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].RackInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.RackInboard2z_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].RackInboard2_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1x_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1y_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1z_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2x_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2y_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2z_FR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FR[_OPIndex].ARBInboard2_z);


                #endregion

                #region Displaying the Link Lengths
                // Link Lengths
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerFrontLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.LowerFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerRearLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.LowerRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.UpperFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.UpperRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.PushRodLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ToeLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.ToeLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.ARBDroopLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.DamperLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.DamperLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBLeverLinkLengthFR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_FR.ARBBladeLength);
                #endregion

                #endregion

                #region Display of Outputs of REAR LEFT
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.MotionRatioRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].InitialMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.InitialARBMRRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalMotionRatioRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].FinalMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalARBMRRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].Final_ARB_MR);
                //To Display the New Camber and Toe
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalCamberRL.Text = String.Format("{0:0.000}", -(_vehicle_OutputDisplay.oc_RL[_OPIndex].waOP.StaticCamber * (180 / Math.PI)));
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalToeRL.Text = String.Format("{0:0.000}", (_vehicle_OutputDisplay.oc_RL[_OPIndex].waOP.StaticToe * (180 / Math.PI)));
                //Calculating The Final Ride Height 
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.RideHeightRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].FinalRideHeight);
                //Calculating the New Corner Weights
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CWRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].CW);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireLoadedRadiusRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedSpringDeflectionRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].Corrected_SpringDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedWheelDeflectionRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].Corrected_WheelDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireDeflectionRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].TireDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.NewDamperLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].New_NonSuspendedMassCoGx);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].New_NonSuspendedMassCoGy);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].New_NonSuspendedMassCoGz);

                #region Displaying the Wishbone Forces
                //Calculating the Wishbone Forces
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink);

                //Chassic Pick Up Points in XYZ direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LowerRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UpperRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].PushRod_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.SpringPreloadOutputRL.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].DamperForce_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkForceRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBDroopLink_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ToeLink_z);

                //Upper and Lower Ball Joint Forces in XYZ Direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightRLx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightRLy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightRLz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].UBJ_z);

                ///<remarks>
                ///ARB Bearing Attachment Forces in XYZ 
                /// </remarks>
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1x_RL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1y_RL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1z_RL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2x_RL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2y_RL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2z_RL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RL[_OPIndex].ARBInboard2_z);

                #endregion

                #region Displaying the Link Lengths
                // Link Lengths
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerFrontLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.LowerFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerRearLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.LowerRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.UpperFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.UpperRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.PushRodLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ToeLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.ToeLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.ARBDroopLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.DamperLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.DamperLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBLeverLinkLengthRL.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RL.ARBBladeLength);
                #endregion



                #endregion

                #region Display of Outputs of REAR RIGHT
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.MotionRatioRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].InitialMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.InitialARBMRRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalMotionRatioRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].FinalMR);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalARBMRRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].Final_ARB_MR);
                //To Display the New Camber and Toe
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalCamberRR.Text = String.Format("{0:0.000}", /*-*/(_vehicle_OutputDisplay.oc_RR[_OPIndex].waOP.StaticCamber * (180 / Math.PI)));
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.FinalToeRR.Text = String.Format("{0:0.000}", -(_vehicle_OutputDisplay.oc_RR[_OPIndex].waOP.StaticToe * (180 / Math.PI)));
                //Calculating The Final Ride Height 
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.RideHeightRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].FinalRideHeight);
                //Calculating the New Corner Weights
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CWRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].CW);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireLoadedRadiusRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedSpringDeflectionRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].Corrected_SpringDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.CorrectedWheelDeflectionRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].Corrected_WheelDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.TireDeflectionRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].TireDeflection);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].CW_Def_WA.NewDamperLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].New_NonSuspendedMassCoGx);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].New_NonSuspendedMassCoGy);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewNSMCGRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].New_NonSuspendedMassCoGz);

                #region Displaying the Wishbone Forces
                //Calculating the Wishbone Forces
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperFront);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperRear);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink);

                //Chassic Pick Up Points in XYZ direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LowerRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperFront_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperFront_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperFront_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperRear_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperRear_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UpperRear_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.PushRodUprightRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].PushRod_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.SpringPreloadOutputRR.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DamperForceBellCrankRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].DamperForce_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkForceRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkBellCrankRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.DroopLinkLeverRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBDroopLink_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkChassisRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ToeLinkUprightRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ToeLink_z);

                //Upper and Lower Ball Joint Forces in XYZ Direction
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerFrontUprightRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.LowerRearUprightRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].LBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperFrontUprightRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UBJ_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightRRx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UBJ_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightRRy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UBJ_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.UpperRearUprightRRz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].UBJ_z);

                ///<remarks>
                ///ARB Bearing Attachment Forces in XYZ 
                /// </remarks>
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1x_RR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1y_RR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard1z_RR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2x_RR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2y_RR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.ARBInboard2z_RR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_RR[_OPIndex].ARBInboard2_z);

                #endregion

                #region Displaying the Link Lengths
                // Link Lengths
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerFrontLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.LowerFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.LowerRearLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.LowerRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.UpperFrontLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.UpperRearLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.PushRodLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ToeLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.ToeLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.ARBDroopLinkLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.DamperLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.DamperLength);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBLeverLinkLengthRR.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.sc_RR.ARBBladeLength);
                #endregion


                #endregion

                #region Vehicle Level Outputs
                ///<remarks>
                ///The SteeringTorque is the only Output Channel which is not an independent array like the rest of the Vehicle Outputs. It is a double variable inside the OutputClass List and hence
                ///oc_FL[Index] is used to reference it
                /// </remarks>
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringTorque.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SteeringTorque);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringColumnInboard1x.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SColumnInboard1_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringColumnInboard1y.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SColumnInboard1_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringColumnInboard1z.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SColumnInboard1_z);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringColumnInboard2x.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SColumnInboard2_x);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringColumnInboard2y.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SColumnInboard2_y);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.SteeringColumnInboard2z.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.oc_FL[_OPIndex].SColumnInboard2_z);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewWheelBase.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.New_WheelBase[_OPIndex]);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewTrackFront.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.New_TrackF[_OPIndex]);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewTrackRear.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.New_TrackR[_OPIndex]);

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewSuspendedMassCGx.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.New_SMCoGx[_OPIndex]);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewSuspendedMassCGz.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.New_SMCoGz[_OPIndex]);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.NewSuspendedMassCGy.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.New_SMCoGy[_OPIndex]);

                ///<remarks>
                ///Chassis Heave should have been inside the Vehicle Class and not the OutputClass. Realized too late
                /// </remarks>
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.ChassisHeave.Text = String.Format("{0:0.000}", (_vehicle_OutputDisplay.oc_FL[_OPIndex].ChassisHeave));
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.RollAngleChassis.Text = String.Format("{0:0.000}", (_vehicle_OutputDisplay.RollAngle[_OPIndex]));
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.PitchAngleChassis.Text = String.Format("{0:0.000}", (_vehicle_OutputDisplay.PitchAngle[_OPIndex]));

                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.ARBMotionRatioFront.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.ARB_MR_Front[_OPIndex]);
                M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].VO.ARBMotionRatioRear.Text = String.Format("{0:0.000}", _vehicle_OutputDisplay.ARB_MR_Rear[_OPIndex]);
                #endregion

                #region Output GUI

                TabControl_Outputs.Visible = true;

                int Index = _vehicle_OutputDisplay.VehicleID - 1;

                #endregion

                #region GUI Operations on based on the Suspension Type of the Selected Vehicle

                #region Geometry Type
                if (_vehicle_OutputDisplay.DoubleWishboneFront == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension Type is Double Wishbone with Pushrod

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthFL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthFR.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthFL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthFR.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthFL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthFR.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthFL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthFR.Show();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1110.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1104.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1112.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1105.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1114.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1106.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1113.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label2.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1127.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1125.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1123.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1124.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1133.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1132.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1131.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label3.Show();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFL.Caption = "Pushrod FL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFR.Caption = "Pushrod FR";


                    #endregion
                }
                else if (_vehicle_OutputDisplay.McPhersonFront == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension type is McPherson Strut
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthFL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthFR.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthFL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthFR.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthFL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthFR.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthFL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthFR.Hide();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1110.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1104.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1112.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1105.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1114.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1106.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1113.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label2.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1127.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1125.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1123.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1124.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1133.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1132.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1131.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label3.Hide();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFL.Caption = "Strut FL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFR.Caption = "Strut FR";

                    #endregion
                }


                if (_vehicle_OutputDisplay.DoubleWishboneRear == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension Type is Double Wishbone with Pushrod

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthRL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthRR.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthRL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthRR.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthRL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthRR.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthRL.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthRR.Show();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1149.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1152.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1157.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1154.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1163.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1161.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1158.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label8.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1142.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1140.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1137.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1139.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1148.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1147.Show();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1146.Show(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label5.Show();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRL.Caption = "Pushrod RL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRR.Caption = "Pushrod RR";

                    #endregion
                }
                else if (_vehicle_OutputDisplay.McPhersonRear == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension type is McPherson Strut
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthRL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperFrontLinkLengthRR.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthRL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.UpperRearLinkLengthRR.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthRL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.PushRodLinkLengthRR.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthRL.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.ARBDroopLinkLengthRR.Hide();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1149.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1152.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1157.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1154.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1163.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1161.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1158.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label8.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1142.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1140.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1137.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1139.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1148.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1147.Hide();
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label1146.Hide(); M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].LL.label5.Hide();

                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRL.Caption = "Stut RL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRR.Caption = "Strut RR";

                    #endregion
                }
                #endregion

                #region Actuation Type
                if (_vehicle_OutputDisplay.PushRodIdentifierFront == 1)
                {
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFL.Caption = "Pushrod FL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFR.Caption = "Pushrod FR";
                }
                else if (_vehicle_OutputDisplay.PullRodIdentifierFront == 1)
                {
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFL.Caption = "Pullrod FL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodFR.Caption = "Pullrod FR";
                }

                if (_vehicle_OutputDisplay.PushRodIdentifierRear == 1)
                {
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRL.Caption = "Pushrod RL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRR.Caption = "Pushrod RR";
                }
                else if (_vehicle_OutputDisplay.PullRodIdentifierRear == 1)
                {
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRL.Caption = "Pullrod RL";
                    M1_Global.vehicleGUI[_vehicle_OutputDisplay.VehicleID - 1].WF.navigationPagePushRodRR.Caption = "Pullrod RR";
                }
                #endregion

                #endregion

                #endregion
            }
            catch (Exception E)
            {
                string source = E.Source;
                string message = E.Message;
                //Try block added here so that in case Vehicle.Assembled_Vehicle is null (because calcs have not been done) the display function doesn't fail 
            }
        }
        #endregion

        #region Motion View - Delete after checking if this can be useful 
        public void InitializeGridControl_MotionView(int Index)
        {


        }

        private void BandedGridView_Motion_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

        }
        #endregion

        #endregion

        #region Calculate Results (To be shifted to another solver class
        /// <summary>
        /// Index of the Vehicle In the List of Vehicles which corresponds to the <see cref="Simulation.Simulation_Vehicle"/>
        /// </summary>
        int VIndex = 0;
        /// <summary>
        /// Index of the Simulation which is user has selected from the <see cref="navBarGroupSimulation"/>
        /// </summary>
        int SimulationIndex = 0;
        /// <summary>
        /// Index of the motion in the List of Motions which corresponds to the <see cref="Simulation.Simulation_Motion"/>
        /// </summary>
        int MotionIndex = 0;
        /// <summary>
        /// No of Steps
        /// </summary>
        int NoOfSteps = 1;
        /// <summary>
        /// Error Messages  which arises when the Vehicle Validation is done
        /// </summary>
        string ErrorMessageVehicleCheck = "No Error";

        /// <summary>
        /// Method to perform Preliminary Vehicle Chekcs
        /// </summary>
        private bool PreliminaryVehicleChecks()
        {
            if (Vehicle.VehicleCounter != 0 && Simulation.SimulationCounter != 0)
            {
                SimulationIndex = navBarGroupSimulation.SelectedLinkIndex;
                ///<summary>
                ///Passing the selected Simulation's Vehicle to temporary Vehicle object to solve. 
                /// </summary>
                Vehicle.Assembled_Vehicle = Simulation.List_Simulation[SimulationIndex].Simulation_Vehicle;
                VIndex = Vehicle.Assembled_Vehicle.VehicleID - 1;

                ///<summary>Validating the Vehicle</summary>
                if (!Vehicle.Assembled_Vehicle.ValidateAssembly(out ErrorMessageVehicleCheck))
                {
                    MessageBox.Show(ErrorMessageVehicleCheck);
                    return false;
                }

                ///<summary>
                ///Passing the selected Simulation's Load Case to temporary Vehicle object to solve. 
                /// </summary>
                Vehicle.Assembled_Vehicle.vehicleLoadCase = new LoadCase();
                Vehicle.Assembled_Vehicle.vehicleLoadCase = Simulation.List_Simulation[SimulationIndex].Simulation_LoadCase;
                ///<summary>Passing the selected Simulation's Setup Change object to the temporary Vehicle object to solve</summary>                
                Simulation.List_Simulation[SimulationIndex].simulationPanel.AssignSimulationObjects();
                Vehicle.Assembled_Vehicle.vehicleSetupChange = Simulation.List_Simulation[SimulationIndex].Simulation_SetupChange;

                Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker = 0; Vehicle.Assembled_Vehicle.SuspensionIsSolved = false;

                #region IF Loops to safeguard the calculations
                ///<summary>
                ///Passing the selected Simulation's Motion to temporary Vehicle object to solve. 
                /// </summary>
                if (Simulation.List_Simulation[SimulationIndex].Simulation_Vehicle.sc_FL.SuspensionMotionExists && Simulation.List_Simulation[SimulationIndex].Simulation_Motion != null)
                {
                    Vehicle.Assembled_Vehicle.vehicle_Motion = Simulation.List_Simulation[SimulationIndex].Simulation_Motion;
                }
                else if (Simulation.List_Simulation[SimulationIndex].Simulation_Vehicle.sc_FL.SuspensionMotionExists && Simulation.List_Simulation[SimulationIndex].Simulation_Motion == null)
                {
                    //MessageBox.Show("Motion not created");
                    ErrorMessageVehicleCheck = "Motion not created";
                    return false;
                }
                if (Vehicle.Assembled_Vehicle.vehicle_Motion == null)
                {
                    Vehicle.Assembled_Vehicle.InitializeOutputClass(NoOfSteps);

                }
                else if (Vehicle.Assembled_Vehicle.vehicle_Motion != null)
                {
                    if (Vehicle.Assembled_Vehicle.vehicle_Motion.Final_WheelDeflectionsX.Count != 0)
                    {
                        NoOfSteps = Vehicle.Assembled_Vehicle.vehicle_Motion.Final_WheelDeflectionsX.Count;
                        Vehicle.Assembled_Vehicle.InitializeOutputClass(NoOfSteps);
                    }
                    else
                    {
                        ErrorMessageVehicleCheck = "Motion Not Defined";
                        //MessageBox.Show(ErrorMessageVehicleCheck);
                        return false;
                    }

                }
                #endregion

                ///<summary>
                ///Calculating the Corner Weights of the Chassis using the Vehicle Model 
                /// </summary>
                Vehicle.Assembled_Vehicle.ChassisCornerMassCalculator();
                ///<summary>
                ///Calculating the Load Case Loads. Since Load Case is initialized in the as <c>public LoadCase vehicleLoadCase = new LoadCase()</c> it will never be null
                ///</summary>
                Vehicle.Assembled_Vehicle.vehicleLoadCase.ComputeWheelLoads(Vehicle.Assembled_Vehicle);

                progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 800, 1);
                progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
                M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI = progressBar;
                M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI.Show();

                Vehicle.Assembled_Vehicle.vehicleGUI = M1_Global.vehicleGUI[VIndex];

                #region Reseting the corner weights, ride height and deflections inside the Assmebled Vehicle to the values that were calculated after the initial calculationd
                for (int i_reset = 0; i_reset < Vehicle.Assembled_Vehicle.oc_FL.Count; i_reset++)
                {
                    Vehicle.Assembled_Vehicle.Reset_CornerWeights(Vehicle.Assembled_Vehicle.oc_FL[i_reset].CW_1, Vehicle.Assembled_Vehicle.oc_FR[i_reset].CW_1, Vehicle.Assembled_Vehicle.oc_RL[i_reset].CW_1, Vehicle.Assembled_Vehicle.oc_RR[i_reset].CW_1, i_reset);
                }
                Vehicle.Assembled_Vehicle.Reset_PushrodLengths();
                Vehicle.Assembled_Vehicle.Reset_RideHeight();
                Vehicle.Assembled_Vehicle.Reset_Deflections();
                #endregion

                #region Clearing the Output Class to eliminate any chances of residue
                M1_Global.Assy_OC[0].Clear();
                M1_Global.Assy_OC[1].Clear();
                M1_Global.Assy_OC[2].Clear();
                M1_Global.Assy_OC[3].Clear();
                #endregion

                return true;
            }

            else
            {
                progressBar.Hide();
                ErrorMessageVehicleCheck = "User has not selected Vehicle item for Simulation";
                MessageBox.Show(ErrorMessageVehicleCheck);
                return false;
            }
        }

        /// <summary>
        /// Method to invoke the <see cref="Vehicle.KinematicsInvoker(bool)"/> method of the <see cref="Vehicle"/> class and solve the Kinematics of the Vehicle 
        /// </summary>
        private bool SolveVehicleKinematics()
        {
            try
            {
                SimulationType simType = new SimulationType();

                if (Vehicle.Assembled_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    simType = SimulationType.MotionAnalysis;
                }
                else if (!Vehicle.Assembled_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    simType = SimulationType.StandToGround;
                }

                ///<summary>Invoking the Solver methods of the Vehicle class</summary>
                Vehicle.Assembled_Vehicle.KinematicsInvoker(Simulation.List_Simulation[SimulationIndex].Simulation_Vehicle.sc_FL.SuspensionMotionExists, simType);
                Vehicle.Assembled_Vehicle.VehicleOutputs(NoOfSteps);
                Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker = 1;

                ///<summary>Assinging the <see cref="Vehicle.Assembled_Vehicle"/> to the <see cref="Vehicle.List_Vehicle"/> at the correct index</summary>
                Vehicle temp_Vehicle = new Vehicle();
                temp_Vehicle = Vehicle.Assembled_Vehicle;
                Vehicle.List_Vehicle[VIndex] = temp_Vehicle;

                return true;
            }
            catch (Exception e)
            {
                ErrorMessageVehicleCheck = e.Message;
                return false;
            }

        }
         
        /// <summary>
        /// Method to carry out the GUI operations required to display the Vehicle's Outputs
        /// </summary>
        private void OutputGUIOperations()
        {
            ///<summary></summary>
            FindOutPutIndex(0);

            ///<summary></summary>
            Button_Recalculate_Enabler();

            #region ---NOT REALLY NEEDED NOW--- Coloring the Corner Weight and Pushrod Length Textboxes White. 
            //M1_Global.vehicleGUI[VIndex].LL.PushRodLinkLengthFL.BackColor = Color.White;
            //M1_Global.vehicleGUI[VIndex].CW_Def_WA.CWFL.BackColor = Color.White;

            //M1_Global.vehicleGUI[VIndex].LL.PushRodLinkLengthFR.BackColor = Color.White;
            //M1_Global.vehicleGUI[VIndex].CW_Def_WA.CWFR.BackColor = Color.White;

            //M1_Global.vehicleGUI[VIndex].LL.PushRodLinkLengthRL.BackColor = Color.White;
            //M1_Global.vehicleGUI[VIndex].CW_Def_WA.CWRL.BackColor = Color.White;


            //M1_Global.vehicleGUI[VIndex].LL.PushRodLinkLengthRR.BackColor = Color.White;
            //M1_Global.vehicleGUI[VIndex].CW_Def_WA.CWRR.BackColor = Color.White;
            #endregion

            ///<summary></summary>
            TabControl_Outputs = CustomXtraTabPage.ClearTabPages(TabControl_Outputs, M1_Global.vehicleGUI[VIndex].TabPages_Vehicle);

            ///<summary>Constructing the Output <see cref="CAD"/> usercontrol here to prevent overcrowding the memory by initializing the controls in the declaration itself</summary>
            M1_Global.vehicleGUI[VIndex].CADVehicleOutputs = new CAD();

            ///<summary></summary>
            M1_Global.vehicleGUI[VIndex].TabPages_Vehicle = M1_Global.vehicleGUI[VIndex].CreateTabPages_For_Vehicle_Outputs(M1_Global.vehicleGUI[VIndex].TabPages_Vehicle, this, Vehicle.List_Vehicle[VIndex].VehicleID);

            ///<summary></summary>
            PopulateOutputDataTable(Vehicle.Assembled_Vehicle);

            ///<summary></summary>
            TabControl_Outputs = CustomXtraTabPage.AddTabPages(TabControl_Outputs, M1_Global.vehicleGUI[Vehicle.Assembled_Vehicle.VehicleID - 1].TabPages_Vehicle);

            ///<summary></summary>
            DisplayOutputs(Vehicle.Assembled_Vehicle);

            ///<summary></summary>
            M1_Global.vehicleGUI[VIndex].PopulateSuspensionGridControl(this, M1_Global.vehicleGUI[VIndex], Vehicle.Assembled_Vehicle, OutputIndex);

            ///<summary></summary>
            Results_NavBarOerations(Vehicle.Assembled_Vehicle);

            ///<summary></summary>
            PopulateInputSheet(Vehicle.Assembled_Vehicle);


            ///<summary></summary>
            M1_Global.vehicleGUI[VIndex].OutputIGESPlotted = false;
            ///<summary></summary>
            M1_Global.vehicleGUI[VIndex].TranslateChassisToGround = false;
            ///<summary></summary>
            M1_Global.vehicleGUI[VIndex].ImportedCADTranslationHistory = new List<double>(new double[] { 0, 0 });

            ///<summary>Initializing the <see cref="VehicleGUI.LoadCaseLegend"/></summary>
            M1_Global.vehicleGUI[VIndex].LoadCaseLegend = new LegendEditor();

            ///<summary>Cloning everything (<see cref="devDept.Eyeshot.Block"/> <see cref="devDept.Eyeshot.Layer"/> Imported Files) from the Input CAD</summary>
            if (M1_Global.vehicleGUI[VIndex].CadIsTobeImported)
            {
                M1_Global.vehicleGUI[VIndex].CADVehicleOutputs.CloneOutputViewPort(M1_Global.vehicleGUI[VIndex].CADVehicleOutputs.viewportLayout1, M1_Global.vehicleGUI[VIndex].importCADForm.importCADViewport.viewportLayout1);
            }
            else
            {
                M1_Global.vehicleGUI[VIndex].CADVehicleOutputs.CloneOutputViewPort(M1_Global.vehicleGUI[VIndex].CADVehicleOutputs.viewportLayout1, M1_Global.vehicleGUI[VIndex].CADVehicleInputs.viewportLayout1);
            }

            M1_Global.vehicleGUI[VIndex].LoadCaseLegend.MaxValue = M1_Global.vehicleGUI[VIndex].LoadCaseLegend.MinValue = 0;
            M1_Global.vehicleGUI[VIndex].EditORCreateVehicleCAD(M1_Global.vehicleGUI[VIndex].CADVehicleOutputs, VIndex, false, M1_Global.vehicleGUI[VIndex].Vehicle_MotionExists, 0, false, M1_Global.vehicleGUI[VIndex].CadIsTobeImported, M1_Global.vehicleGUI[VIndex].PlotWheel);

            progressBar.Hide();

            radioButtonTranstoGround.Checked = false;
            radioButtonTransToCS.Checked = false;

            ChangeTracker++;

            try
            {
                ribbon.SelectedPage = ribbonPageResults;
                navBarControl1.ActiveGroup = navBarGroupResults;
                navBarControlResults.ActiveGroup = navBarControlResults.Groups[M1_Global.vehicleGUI[VIndex].navBarGroup_Vehicle_Result.Name];
            }
            catch (Exception)
            {

                //Unexpectedly failing sometimes

            }

            if (Vehicle.Assembled_Vehicle.vehicle_Motion != null)
            {
                ///<remarks>
                ///This line of code exists, so that the Vehicle's Motion Object can be identified in the List of MotionGUI (Motion and MotionGUI have synchronus Lists). Then this MotionGUI object can be used to populate the GridControl of the Motion View. 
                /// </remarks>
                MotionIndex = Vehicle.Assembled_Vehicle.vehicle_Motion.MotionID - 1;
                MotionGUI.List_MotionGUI[MotionIndex].InitializeGridControl_MotionView(VIndex, this);

                sidePanel2.Show();
                groupControl13.Show();
                gridControl2.Show();
            }
            else
            {
                sidePanel2.Hide();
            }
        }

        private void barButtonCalculateResults_ItemClicked(object sender, ItemClickEventArgs e)
        {

            this.ActiveControl = sidePanel1;

            try
            {
                ///<summary>
                ///<value> index </value This is the index of the Vehicle in the List of Vehicle Items. 
                ///<value> SimulationIndex</value> This is the Index of the SImulation in the list of Simulation Items
                ///<value> MotionIndex </value> This is the ID (or Index) of the Motion which is INSDE the VEHICLE (NOT the index of the navBarGroupMotion's selected link index)
                ///</summary>


                #region This is the stage where the calculations are actually performed by calling the Kinematics Invoker and Vehicle Output Functions
                bool Proceed;
                ///<summary>Performing the preliminary Vehicle check operations</summary>
                Proceed = PreliminaryVehicleChecks();

                ///<summary>Solving the Vehicle's Kinematics</summary>
                if (Proceed)
                {
                    Proceed = SolveVehicleKinematics();
                }
                else
                {
                    //MessageBox.Show(ErrorMessageVehicleCheck);
                    return;
                }

                if (Proceed)
                {
                    OutputGUIOperations();
                }
                else
                {
                    //MessageBox.Show(ErrorMessageVehicleCheck);
                    return;
                }

                #endregion

            }

            catch (Exception E)
            {
                DialogResult result;
                result = MessageBox.Show(E.Message);
                Exception eee = E.InnerException;


                if (result == DialogResult.OK)
                {
                    try
                    {
                        progressBar.Hide();
                    }
                    catch (Exception) { }
                }
            }

            CalculateResultsButtonClickCounter++;
        }


        #endregion

        #region Setup Change Events
        private void barButtonRunSetupChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            ///<summary>Boolean to check if the the code has suceeded and the software can proceed or if the code has to be stopped with an error message displayed to the user </summary>
            bool Proceed;

            ///<summary>Performing the preliminary Vehicle check operations</summary>
            Proceed = PreliminaryVehicleChecks();

            ///<summary>Solving the Vehicle's Kinematics</summary>
            if (Proceed)
            {

                Proceed = SolveVehicleKinematics();

                if (Vehicle.Assembled_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    MessageBox.Show("Incorrect Vehicle Configuration : Please select a Assemble a Suspension which is created in the Setup Mode");
                    progressBar.Hide();
                    return;
                }

            }
            else
            {
                //MessageBox.Show(ErrorMessageVehicleCheck);
                return;
            }

            ///<summary>Invoking the <see cref="Vehicle.SetupChangeInvoker(SetupChange)"/> method</summary>
            if (Proceed)
            {
                Vehicle.Assembled_Vehicle.SetupChangeInvoker(Vehicle.Assembled_Vehicle.vehicleSetupChange);

                try
                {
                    ribbon.SelectedPage = ribbonPageSimulation;
                    navBarControl1.ActiveGroup = navBarGroupSimulationSetup;
                    navBarControlSimulation.ActiveGroup = navBarGroupSetupChange;
                }
                catch (Exception)
                {

                    //Unexpectedly failing sometimes

                }
            }
            else
            {
                //MessageBox.Show(ErrorMessageVehicleCheck);
                return;
            }

            ///<summary>Incrementing the Change Tracker to inform the software that a change has happened and hence upon closing the Save File Dialog will be displayed</summary>
            ChangeTracker++;

            progressBar.Hide();
        }
        #endregion

        #region Translator invoker methods
        private void OutputOriginZ_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int Index = 0;
                FocusedRowChangedEventArgs E = new FocusedRowChangedEventArgs(0, 0);
                for (int i_Vehicle = 0; i_Vehicle < Vehicle.List_Vehicle.Count; i_Vehicle++)
                {
                    if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_Vehicle].navBarGroup_Vehicle_Result.Name)
                    {
                        Index = i_Vehicle;
                        break;
                    }
                }
                Vehicle.List_Vehicle[Index].OutputOrigin_x = Convert.ToDouble(R1.OutputOriginX.Text);
                Vehicle.List_Vehicle[Index].OutputOrigin_y = Convert.ToDouble(R1.OutputOriginY.Text);
                Vehicle.List_Vehicle[Index].OutputOrigin_z = Convert.ToDouble(R1.OutputOriginZ.Text);

                Vehicle.List_Vehicle[Index].InitializeTranslation(false, Vehicle.List_Vehicle[Index].Vehicle_MotionExists, 0, 0, 0, Vehicle.List_Vehicle[Index].OutputOrigin_x, Vehicle.List_Vehicle[Index].OutputOrigin_y, Vehicle.List_Vehicle[Index].OutputOrigin_z);

                Vehicle.List_Vehicle[Index].vehicle_Motion.Motion_MotionGUI.BandedGridView_Motion_FocusedRowChanged(sender, E);

            }
            catch (Exception)
            {

                // Incase vehicle is not yet created while this code is being executed

            }
        }

        private void radioButtonTranstoGround_CheckedChanged(object sender, EventArgs e)
        {
            label531.Hide();
            label532.Hide();
            label533.Hide();
            OutputOriginX.Hide();
            OutputOriginY.Hide();
            OutputOriginZ.Hide();
            FocusedRowChangedEventArgs E = new FocusedRowChangedEventArgs(0, 0);
            int Index = 0;

            try
            {
                if (radioButtonTranstoGround.Checked == true)
                {
                    for (int i_Vehicle = 0; i_Vehicle < Vehicle.List_Vehicle.Count; i_Vehicle++)
                    {
                        if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_Vehicle].navBarGroup_Vehicle_Result.Name)
                        {
                            Index = i_Vehicle;
                            break;
                        }
                    }


                    Vehicle.List_Vehicle[Index].InitializeTranslation(true, Vehicle.List_Vehicle[Index].Vehicle_MotionExists, 0, 0, 0, 0, 0, 0);
                    Vehicle.List_Vehicle[Index].vehicle_Motion.Motion_MotionGUI.BandedGridView_Motion_FocusedRowChanged(sender, E);

                    M1_Global.vehicleGUI[Index].TranslateChassisToGround = true;

                }
                //else
                //{
                //    M1_Global.vehicleGUI[Index].TranslateChassisToGround = false;
                //}
            }
            catch (Exception)
            {

                // In case the radio button is pressed without a Vehicle being created
            }

        }

        private void radioButtonTransToCS_CheckedChanged(object sender, EventArgs e)
        {
            label531.Show();
            label532.Show();
            label533.Show();
            OutputOriginX.Show();
            OutputOriginY.Show();
            OutputOriginZ.Show();
        }
        #endregion

        #region Input Origin Changed Event
        private void InputOriginY_Leave(object sender, EventArgs e)
        {
            int SusIndex = navBarGroupSuspensionFL.SelectedLinkIndex;

            try
            {
                SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex].InputOriginX = Convert.ToDouble(InputOriginX.Text);
                SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex].InputOriginY = Convert.ToDouble(InputOriginY.Text);
                SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex].InputOriginZ = Convert.ToDouble(InputOriginZ.Text);

                if (Vehicle.List_Vehicle.Count != 0)
                {
                    for (int i_sus_Vehicl = 0; i_sus_Vehicl < Vehicle.List_Vehicle.Count; i_sus_Vehicl++)
                    {
                        if (Vehicle.List_Vehicle[i_sus_Vehicl].sc_FL._SCName == SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex]._SCName)
                        {
                            Vehicle.List_Vehicle[i_sus_Vehicl].sc_FL.InputOriginX = SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex].InputOriginX;
                            Vehicle.List_Vehicle[i_sus_Vehicl].sc_FL.InputOriginY = SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex].InputOriginY;
                            Vehicle.List_Vehicle[i_sus_Vehicl].sc_FL.InputOriginZ = SuspensionCoordinatesFront.Assy_List_SCFL[SusIndex].InputOriginZ;
                        }
                    }

                }
            }
            catch (Exception)
            {


            }

        }
        #endregion

        #region Display Results Event 
        private void barButtonDisplayResults_ItemClicked(object sender, ItemClickEventArgs e)
        {
            //OTKi.Hide();
        }
        #endregion

        #region Method to determine whether to Enable the Recalculate Button or not 
        public void Button_Recalculate_Enabler()
        {
            try
            {
                //int index = 0;
                //foreach (VehicleGUI item in M1_Global.vehicleGUI)
                //{
                //    if (navBarControlResults.ActiveGroup.Name==item.navBarGroup_Vehicle_Result.Name)
                //    {
                //        index = item.navBarGroup_Vehicle_Result.navBarID - 1;
                //    }
                //}

                //for (int i_DisableRecalculateInsideInputShee = 0; i_DisableRecalculateInsideInputShee < M1_Global.vehicleGUI.Count; i_DisableRecalculateInsideInputShee++)
                //{
                //    M1_Global.vehicleGUI[i_DisableRecalculateInsideInputShee].IS.RecalculateCornerWeightForPushRodLength.Enabled = false;
                //    M1_Global.vehicleGUI[i_DisableRecalculateInsideInputShee].IS.RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;
                //}

                //if (Vehicle.List_Vehicle[index].McPhersonFront == 1)
                //{
                //    ribbonPageGroupRecalculate.Visible = false;
                //    return;
                //}
                //else if (Vehicle.List_Vehicle[index].McPhersonRear == 1)
                //{
                //    ribbonPageGroupRecalculate.Visible = false;
                //    return;
                //}

                //if (Vehicle.List_Vehicle[index].Vehicle_Results_Tracker == 1 && !Vehicle.List_Vehicle[index].sc_FL.SuspensionMotionExists)
                //{
                //    ribbonPageGroupRecalculate.Visible = true;
                //}
                //else if (Vehicle.List_Vehicle[index].Vehicle_Results_Tracker == 0 || Vehicle.List_Vehicle[index].sc_FL.SuspensionMotionExists)
                //{
                //    ribbonPageGroupRecalculate.Visible = false;
                //}
            }
            catch (Exception) { }
        }
        #endregion

        #region Recalculate Methods

        #region Handling the Recalculate Corner Weights for New Pushrod Length Events

        #region Button Click Event
        private void ReCalculate_GUI_Click(object sender, EventArgs e)
        {
            try
            {
                Recalculate_GUI_Click_Operations();
            }
            catch (Exception)
            {
                MessageBox.Show("Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }
        }
        #endregion

        #region GUI Operations
        public void Recalculate_GUI_Click_Operations()
        {
            try
            {

                int local_VehicleID = 0;
                popupControlContainerRecalculateResults.HidePopup();

                for (int i_Recalculate = 0; i_Recalculate < M1_Global.vehicleGUI.Count; i_Recalculate++)
                {
                    if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.Name)
                    {
                        local_VehicleID = M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.navBarID - 1;
                        break;
                    }
                }

                M1_Global.vehicleGUI[local_VehicleID].IS.Kinematics_Software_New_ObjectInitializer(this);

                #region GUI operations to Show the Input Sheet with Link Length Page opne, pushrod textbox enabled and green, corner weight textbox disabled and white
                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane1.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageLinkLengthsFL;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.BackColor = Color.LimeGreen;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.BackColor = Color.White;
                M1_Global.vehicleGUI[local_VehicleID].IS.RecalculateCornerWeightForPushRodLength.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;

                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane2.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageLinkLengthsFR;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.BackColor = Color.LimeGreen;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.BackColor = Color.White;

                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane3.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageLinkLengthsRL;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.BackColor = Color.LimeGreen;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.BackColor = Color.White;

                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane4.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageLinkLengthsRR;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.BackColor = Color.LimeGreen;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.BackColor = Color.White;
                #endregion

                Vehicle.List_Vehicle[local_VehicleID].Reset_PushrodLengths();

                PopulateInputSheet(Vehicle.List_Vehicle[local_VehicleID]);

                M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[0].PageVisible = true;
                TabControl_Outputs.SelectedTabPage = M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[0];
            }
            catch (Exception) { }
        }
        #endregion

        #region Calculations
        public void ReCalculate_Click()
        {
            try
            {
                if (Vehicle.List_Vehicle.Count != 0)
                {
                    int local_VehicleID = 0;
                    for (int i_Recalculate = 0; i_Recalculate < M1_Global.vehicleGUI.Count; i_Recalculate++)
                    {
                        if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.Name)
                        {
                            local_VehicleID = M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.navBarID - 1;
                            break;
                        }
                    }

                    #region Reseting the corner weights, ride height and deflections inside the Assmebled Vehicle to the values that were calculated after the initial calculationd
                    Vehicle.List_Vehicle[local_VehicleID].Reset_CornerWeights(Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW_1, 0);
                    Vehicle.List_Vehicle[local_VehicleID].Reset_RideHeight();
                    Vehicle.List_Vehicle[local_VehicleID].Reset_Deflections();
                    Vehicle.List_Vehicle[local_VehicleID].Reset_RideRate();
                    #endregion


                    M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[3].PageVisible = true;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.Visible = true;
                    TabControl_Outputs.SelectedTabPage = M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[3];
                    TabControl_Outputs.SelectedTabPage.PageVisible = true;


                    #region Declarations
                    double New_PushRodFL, New_PushRodFR, New_PushRodRL, New_PushRodRR;
                    double New_RideheightFL, New_RideheightFR, New_RideheightRL, New_RideheightRR;
                    double alphaFL, alphaFR, alphaRL, alphaRR;
                    double G1H1_Perp_FL, G1H1_Perp_FR, G1H1_Perp_RL, G1H1_Perp_RR, G1H1_FL, G1H1_FR, G1H1_RL, G1H1_RR;
                    double New_WheelDeflectionFL, New_WheelDeflectionFL_1, New_WheelDeflectionFR, New_WheelDeflectionFR_1, New_WheelDeflectionRL, New_WheelDeflectionRL_1, New_WheelDeflectionRR, New_WheelDeflectionRR_1;
                    double New_CW_FL, New_CW_FR, New_CW_RL, New_CW_RR;
                    double Delta_CW_FL, Delta_CW_FR, Delta_CW_RL, Delta_CW_RR;
                    #endregion

                    #region FRONT LEFT
                    #region FRONT LEFT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.Update();
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.Refresh();

                        New_PushRodFL = Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.Text = "";
                        return;

                    }

                    G1H1_Perp_FL = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_FL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FL.H1x);
                    G1H1_FL = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FL.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FL.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_FL.H1y), 2));
                    alphaFL = Math.Asin(G1H1_Perp_FL / G1H1_FL);
                    New_RideheightFL = Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight + ((New_PushRodFL - Vehicle.List_Vehicle[local_VehicleID].sc_FL.PushRodLength) * Math.Sin(alphaFL));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionFL = Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].Corrected_WheelDeflection - (New_RideheightFL - Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight);

                    if (String.Format("{0:0.0}", New_PushRodFL) == String.Format("{0:0.0}", Vehicle.List_Vehicle[local_VehicleID].sc_FL.PushRodLength))
                    {
                        New_CW_FL = Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW;
                    }

                    else
                    {
                        New_CW_FL = /*-*/((((New_WheelDeflectionFL + (Vehicle.List_Vehicle[local_VehicleID].spring_FL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FL.InitialMR)) * Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].RideRate)))/* / 9.81*/;
                    }

                    Delta_CW_FL = (Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW - (New_CW_FL)); // If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW += /*-*/(Delta_CW_FL);
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW += (Delta_CW_FL);
                    New_WheelDeflectionRR_1 = /*-*/(((/*9.81 **/ Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].RideRate)) /*+*/- (Vehicle.List_Vehicle[local_VehicleID].spring_RR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RR.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight = -New_WheelDeflectionRR_1 + Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight;




                    #endregion
                    #endregion

                    #region FRONT RIGHT
                    #region FRONT RIGHT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        New_PushRodFR = Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.Text = "";
                        return;
                    }

                    G1H1_Perp_FR = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_FR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FR.H1x);
                    G1H1_FR = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FR.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FR.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_FR.H1y), 2));
                    alphaFR = Math.Asin(G1H1_Perp_FR / G1H1_FR);
                    New_RideheightFR = Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight + ((New_PushRodFR - Vehicle.List_Vehicle[local_VehicleID].sc_FR.PushRodLength) * Math.Sin(alphaFR));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionFR = Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].Corrected_WheelDeflection - (New_RideheightFR - Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight);

                    if (String.Format("{0:0.0}", New_PushRodFR) == String.Format("{0:0.0}", Vehicle.List_Vehicle[local_VehicleID].sc_FR.PushRodLength))
                    {
                        New_CW_FR = Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW;
                    }
                    else
                    {
                        New_CW_FR = /*-*/((((New_WheelDeflectionFR + (Vehicle.List_Vehicle[local_VehicleID].spring_FR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FR.InitialMR)) * Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].RideRate)))/* / 9.81*/;
                    }

                    Delta_CW_FR = (Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW - (New_CW_FR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW += /*-*/Delta_CW_FR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW += Delta_CW_FR;
                    New_WheelDeflectionRL_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].RideRate)) /*+*/ - (Vehicle.List_Vehicle[local_VehicleID].spring_RL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RL.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight = -New_WheelDeflectionRL_1 + Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight;





                    #endregion
                    #endregion

                    #region REAL LEFT
                    #region REAR LEFT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        New_PushRodRL = Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.Text);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.Text = "";
                        return;

                    }
                    G1H1_Perp_RL = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_RL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RL.H1x);
                    G1H1_RL = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RL.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RL.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_RL.H1y), 2));
                    alphaRL = Math.Asin(G1H1_Perp_RL / G1H1_RL);
                    New_RideheightRL = Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight + ((New_PushRodRL - Vehicle.List_Vehicle[local_VehicleID].sc_RL.PushRodLength) * Math.Sin(alphaRL));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionRL = New_WheelDeflectionRL_1 - (New_RideheightRL - Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight);
                    if (String.Format("{0:0.000}", New_PushRodRL) == String.Format("{0:0.000}", Vehicle.List_Vehicle[local_VehicleID].sc_RL.PushRodLength))
                    {
                        New_CW_RL = Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW;
                    }
                    else
                    {


                        New_CW_RL = /*-*/((((New_WheelDeflectionRL + (Vehicle.List_Vehicle[local_VehicleID].spring_RL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RL.InitialMR)) * Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].RideRate)))/* / 9.81*/;
                    }

                    Delta_CW_RL = (Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW - (New_CW_RL));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW += /*-*/Delta_CW_RL;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW += Delta_CW_RL;

                    New_WheelDeflectionFR_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].RideRate)) /*+*/ - (Vehicle.List_Vehicle[local_VehicleID].spring_FR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FR.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight = -New_WheelDeflectionFR_1 + Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight;



                    #endregion
                    #endregion

                    #region REAR RIGHT
                    #region REAR RIGHT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        New_PushRodRR = Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.Text);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.Text = "";
                        return;
                    }
                    G1H1_Perp_RR = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_RR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RR.H1x);
                    G1H1_RR = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RR.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RR.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_RR.H1y), 2));
                    alphaRR = Math.Asin(G1H1_Perp_RR / G1H1_RR);
                    New_RideheightRR = Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight + ((New_PushRodRR - Vehicle.List_Vehicle[local_VehicleID].sc_RR.PushRodLength) * Math.Sin(alphaRR));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionRR = New_WheelDeflectionRR_1 - (New_RideheightRR - Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight);
                    if (String.Format("{0:0.0}", New_PushRodRR) == String.Format("{0:0.0}", Vehicle.List_Vehicle[local_VehicleID].sc_RR.PushRodLength))
                    {


                        New_CW_RR = Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW;
                    }
                    else
                    {
                        New_CW_RR = /*-*/((((New_WheelDeflectionRR + (Vehicle.List_Vehicle[local_VehicleID].spring_RR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RR.InitialMR)) * Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].RideRate)))/* / 9.81*/;
                    }


                    Delta_CW_RR = (Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW - (New_CW_RR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW += /*-*/Delta_CW_RR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW += Delta_CW_RR;
                    New_WheelDeflectionFL_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].RideRate)) /*+*/ - (Vehicle.List_Vehicle[local_VehicleID].spring_FL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FL.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight = -New_WheelDeflectionFL_1 + Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight;



                    #endregion
                    #endregion

                    //
                    // Not Assigning the New Values of Deflections and Ride Height because they will be recalculated upon invoking the Kinematics Invoker Function


                    //
                    // Assigning the New values of Push Rod Lengths
                    Vehicle.List_Vehicle[local_VehicleID].sc_FL.PushRodLength = New_PushRodFL;
                    Vehicle.List_Vehicle[local_VehicleID].sc_FR.PushRodLength = New_PushRodFR;
                    Vehicle.List_Vehicle[local_VehicleID].sc_RL.PushRodLength = New_PushRodRL;
                    Vehicle.List_Vehicle[local_VehicleID].sc_RR.PushRodLength = New_PushRodRR;



                    //
                    //Invoking the Kinematics and Vehicle Output Functions again 
                    #region Overriding the Corner Weights to the new values calculated
                    Vehicle.List_Vehicle[local_VehicleID].OverrideCornerWeights(Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW, Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW, Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW, Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW);
                    #endregion

                    progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 800, 1);
                    progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
                    M1_Global.vehicleGUI[local_VehicleID].ProgressBarVehicleGUI = progressBar;

                    M1_Global.vehicleGUI[local_VehicleID].ProgressBarVehicleGUI.Show();

                    Vehicle.List_Vehicle[local_VehicleID].vehicleGUI = M1_Global.vehicleGUI[local_VehicleID];
                    Vehicle.List_Vehicle[local_VehicleID].KinematicsInvoker(MotionExists, SimulationType.StandToGround);


                    Vehicle.List_Vehicle[local_VehicleID].VehicleOutputs(1);

                    #region Coloring the Corner Weight and Pushrod Length Textboxes
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWFL.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthFL.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWFR.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthFR.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWRL.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthRL.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWRR.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthRR.BackColor = Color.White;
                    #endregion


                    PopulateOutputDataTable(Vehicle.List_Vehicle[local_VehicleID]);

                    M1_Global.vehicleGUI[local_VehicleID].ProgressBarVehicleGUI.Hide();

                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight = New_RideheightFL;
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight = New_RideheightFR;
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight = New_RideheightRL;
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight = New_RideheightRR;


                    DisplayOutputs(Vehicle.List_Vehicle[local_VehicleID]);

                    //M1_Global.vehicleGUI[local_VehicleID].EditORCreateVehicleCAD(M1_Global.vehicleGUI[local_VehicleID].CADVehicleOutputs, local_VehicleID, false, M1_Global.vehicleGUI[local_VehicleID].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[local_VehicleID].CadIsTobeImported);

                    Vehicle.List_Vehicle[local_VehicleID].Reset_PushrodLengths();

                    ChangeTracker++;

                }

                else
                    MessageBox.Show(" Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }

            catch (Exception)
            {
                MessageBox.Show(" Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }
        }
        #endregion

        #endregion

        #region Handling the Recalculate Pushrod Lengths for Desired Corner Weight Events

        #region Button Click Event
        private void ReCalculateForDesiredCornerWeight_GUI_Click(object sender, EventArgs e)
        {
            try
            {

                ReCalculateForDesiredCornerWeight_GUI_Click_Operations();

            }
            catch (Exception)
            {

                MessageBox.Show(" Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }

        }
        #endregion

        #region GUI Operations
        public void ReCalculateForDesiredCornerWeight_GUI_Click_Operations()
        {
            try
            {
                int local_VehicleID = 0;

                for (int i_Recalculate = 0; i_Recalculate < M1_Global.vehicleGUI.Count; i_Recalculate++)
                {
                    if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.Name)
                    {
                        local_VehicleID = M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.navBarID - 1;
                        break;
                    }
                }

                M1_Global.vehicleGUI[local_VehicleID].IS.Kinematics_Software_New_ObjectInitializer(this);
                PopulateInputSheet(Vehicle.List_Vehicle[local_VehicleID]);

                popupControlContainerRecalculateResults.HidePopup();

                #region GUI operations to Show the Input Sheet with Corner Weights Page opne, pushrod textbox disabled and white, corner weight textbox enabled and green
                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane1.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageCornerWeightFL;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFL.BackColor = Color.White;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.BackColor = Color.LimeGreen;
                M1_Global.vehicleGUI[local_VehicleID].IS.RecalculatePushrodLengthForDesiredCornerWeight.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.RecalculateCornerWeightForPushRodLength.Enabled = false;

                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane2.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageCornerWeightFR;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodFR.BackColor = Color.White;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.BackColor = Color.LimeGreen;

                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane3.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageCornerWeightRL;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRL.BackColor = Color.White;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.BackColor = Color.LimeGreen;

                M1_Global.vehicleGUI[local_VehicleID].IS.navigationPane4.SelectedPage = M1_Global.vehicleGUI[local_VehicleID].IS.navigationPageCornerWeightRR;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.Enabled = false;
                M1_Global.vehicleGUI[local_VehicleID].IS.PushRodRR.BackColor = Color.White;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.Enabled = true;
                M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.BackColor = Color.LimeGreen;
                #endregion

                M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[0].PageVisible = true;
                TabControl_Outputs.SelectedTabPage = M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[0];

                Vehicle.List_Vehicle[local_VehicleID].Reset_CornerWeights(Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW_1, 0);

                PopulateInputSheet(Vehicle.List_Vehicle[local_VehicleID]);
            }
            catch (Exception) { }
        }
        #endregion

        #region Calculations
        public void ReCalculateForDesiredCornerWeight_Click()
        {
            try
            {
                //ReCalculateForDesiredCornerWeight_GUI_Click_Operations();

                if (Vehicle.List_Vehicle.Count != 0)
                {
                    int local_VehicleID = 0;
                    for (int i_Recalculate = 0; i_Recalculate < M1_Global.vehicleGUI.Count; i_Recalculate++)
                    {
                        if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.Name)
                        {
                            local_VehicleID = M1_Global.vehicleGUI[i_Recalculate].navBarGroup_Vehicle_Result.navBarID - 1;
                            break;
                        }
                    }

                    #region Reseting the corner weights, ride height and deflections inside the Assmebled Vehicle to the values that were calculated after the initial calculationd
                    Vehicle.List_Vehicle[local_VehicleID].Reset_PushrodLengths();
                    Vehicle.List_Vehicle[local_VehicleID].Reset_RideHeight();
                    Vehicle.List_Vehicle[local_VehicleID].Reset_Deflections();
                    Vehicle.List_Vehicle[local_VehicleID].Reset_RideRate();
                    #endregion


                    //M1_Global.List_I1[local_VehicleID ].Hide();
                    M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[5].PageVisible = true;
                    M1_Global.vehicleGUI[local_VehicleID].LL.Visible = true;
                    TabControl_Outputs.SelectedTabPage = M1_Global.vehicleGUI[local_VehicleID].TabPages_Vehicle[5];
                    TabControl_Outputs.SelectedTabPage.PageVisible = true;

                    #region Declarations
                    double New_CW_FL, New_CW_FR, New_CW_RL, New_CW_RR;
                    double New_WheelDeflectionFL, New_WheelDeflectionFR, New_WheelDeflectionRL, New_WheelDeflectionRL_1, New_WheelDeflectionRR, New_WheelDeflectionRR_1;
                    double New_RideheightFL, New_RideheightFR, New_WheelDeflectionFR_1, New_RideheightRL, New_RideheightRR;
                    double New_PushRodFL, New_WheelDeflectionFL_1, New_PushRodFR, New_PushRodRL, New_PushRodRR;
                    double G1H1_Perp_FL, G1H1_Perp_FR, G1H1_Perp_RL, G1H1_Perp_RR, G1H1_FL, G1H1_FR, G1H1_RL, G1H1_RR;
                    double alphaFL, alphaFR, alphaRL, alphaRR;
                    double Delta_CW_FL, Delta_CW_FR, Delta_CW_RL, Delta_CW_RR;
                    #endregion

                    #region FRONT LEFT
                    #region FRONT LEFT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_FL = /*-*/Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFL.Text = "";
                        return;

                    }

                    if (String.Format("{0:0.00}", New_CW_FL) == String.Format("{0:0.00}", Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW_1))
                    {
                        New_CW_FL = Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW;
                    }

                    Delta_CW_FL = (Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW - (New_CW_FL)); // If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionFL = /*-*/(((/*9.81 * */New_CW_FL) / (Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].RideRate)) /*+*/- (Vehicle.List_Vehicle[local_VehicleID].spring_FL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FL.InitialMR));
                    New_RideheightFL = -New_WheelDeflectionFL + Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight;
                    #endregion

                    #region FRONT LEFT Calculation of New Push Rod Length
                    G1H1_Perp_FL = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_FL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FL.H1x);
                    G1H1_FL = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FL.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FL.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_FL.H1y), 2));
                    alphaFL = Math.Asin(G1H1_Perp_FL / G1H1_FL);
                    New_PushRodFL = ((New_RideheightFL - Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight) / Math.Sin(alphaFL)) + Vehicle.List_Vehicle[local_VehicleID].sc_FL.PushRodLength;
                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW += /*-*/(Delta_CW_FL);
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW += (Delta_CW_FL);
                    New_WheelDeflectionRR_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].RideRate)) + (Vehicle.List_Vehicle[local_VehicleID].spring_RR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RR.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight = -New_WheelDeflectionRR_1 + Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight;





                    #endregion
                    #endregion

                    #region FRONT RIGHT
                    #region FRONT RIGHT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_FR = /*-*/Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.Text);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightFR.Text = "";
                        return;
                    }
                    if (String.Format("{0:0.000}", New_CW_FR) == String.Format("{0:0.000}", Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW_1))
                    {
                        New_CW_FR = Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW;
                    }
                    Delta_CW_FR = (Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW - (New_CW_FR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionFR = /*-*/(((/*9.81 * */New_CW_FR) / (Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].RideRate)) /*+*/ - (Vehicle.List_Vehicle[local_VehicleID].spring_FR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FR.InitialMR));
                    New_RideheightFR = -New_WheelDeflectionFR + Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight;
                    #endregion

                    #region FRONT RIGHT Calculation of New Push Rod Length
                    G1H1_Perp_FR = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_FR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FR.H1x);
                    G1H1_FR = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_FR.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_FR.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_FR.H1y), 2));
                    alphaFR = Math.Asin(G1H1_Perp_FR / G1H1_FR);
                    New_PushRodFR = ((New_RideheightFR - Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight) / Math.Sin(alphaFR)) + Vehicle.List_Vehicle[local_VehicleID].sc_FR.PushRodLength;
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW += /*-*/Delta_CW_FR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW += Delta_CW_FR;
                    New_WheelDeflectionRL_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].RideRate)) + (Vehicle.List_Vehicle[local_VehicleID].spring_RL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RL.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight = -New_WheelDeflectionRL_1 + Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight;


                    #endregion
                    #endregion

                    #region REAR LEFT
                    #region REAR LEFT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_RL = /*-*/Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRL.Text = "";
                        return;
                    }
                    if (String.Format("{0:0.000}", New_CW_RL) == String.Format("{0:0.000}", Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW_1))
                    {
                        New_CW_RL = Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW;
                    }
                    Delta_CW_RL = (Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW - (New_CW_RL));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionRL = /*-*/(((/*9.81 * */New_CW_RL) / (Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].RideRate)) /*+*/ - (Vehicle.List_Vehicle[local_VehicleID].spring_RL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RL.InitialMR));
                    New_RideheightRL = -New_WheelDeflectionRL + Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight;
                    #endregion

                    #region REAR LEFT Calculation of New Push Rod Length
                    G1H1_Perp_RL = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_RL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RL.H1x);
                    G1H1_RL = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RL.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RL.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RL.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_RL.H1y), 2));
                    alphaRL = Math.Asin(G1H1_Perp_RL / G1H1_RL);
                    New_PushRodRL = ((New_RideheightRL - Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].FinalRideHeight) / Math.Sin(alphaRL)) + Vehicle.List_Vehicle[local_VehicleID].sc_RL.PushRodLength;
                    Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW += /*-*/Delta_CW_RL;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW += Delta_CW_RL;
                    New_WheelDeflectionFR_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].RideRate)) + (Vehicle.List_Vehicle[local_VehicleID].spring_FR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FR.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight = -New_WheelDeflectionFR_1 + Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].FinalRideHeight;

                    #endregion
                    #endregion

                    #region REAR RIGHT
                    #region REAR RIGHT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_RR = /*-*/Convert.ToDouble(M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.vehicleGUI[local_VehicleID].IS.Show();
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.BackColor = Color.IndianRed;
                        M1_Global.vehicleGUI[local_VehicleID].IS.CornerWeightRR.Text = "";
                        return;
                    }

                    if (String.Format("{0:0.00}", New_CW_RR) == String.Format("{0:0.00}", Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW_1))
                    {
                        New_CW_RR = Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW;
                    }

                    Delta_CW_RR = (Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW - (New_CW_RR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionRR = /*-*/(((/*9.81 * */New_CW_RR) / (Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].RideRate)) /*+*/ - (Vehicle.List_Vehicle[local_VehicleID].spring_RR.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_RR.InitialMR));
                    New_RideheightRR = -New_WheelDeflectionRR + New_WheelDeflectionRR_1 + Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight;
                    #endregion

                    #region REAR RIGHT Calculation of New Push Rod Length
                    G1H1_Perp_RR = Math.Abs(Vehicle.List_Vehicle[local_VehicleID].sc_RR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RR.H1x);
                    G1H1_RR = Math.Sqrt(Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RR.G1x - Vehicle.List_Vehicle[local_VehicleID].sc_RR.H1x), 2) + Math.Pow((Vehicle.List_Vehicle[local_VehicleID].sc_RR.G1y - Vehicle.List_Vehicle[local_VehicleID].sc_RR.H1y), 2));
                    alphaRR = Math.Asin(G1H1_Perp_RR / G1H1_RR);
                    New_PushRodRR = ((New_RideheightRR - Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].FinalRideHeight) / Math.Sin(alphaRR)) + Vehicle.List_Vehicle[local_VehicleID].sc_RR.PushRodLength;
                    Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW += /*-*/Delta_CW_RR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW += Delta_CW_RR;
                    New_WheelDeflectionFL_1 = /*-*/(((/*9.81 * */Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW) / (Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].RideRate)) + (Vehicle.List_Vehicle[local_VehicleID].spring_FL.SpringPreload / Vehicle.List_Vehicle[local_VehicleID].sc_FL.InitialMR));
                    Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight = -New_WheelDeflectionFL_1 + Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].Corrected_WheelDeflection + Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].FinalRideHeight;


                    #endregion
                    #endregion


                    //
                    // Assigning new values of Pushrod Lengths
                    Vehicle.List_Vehicle[local_VehicleID].sc_FL.PushRodLength = New_PushRodFL;
                    Vehicle.List_Vehicle[local_VehicleID].sc_FR.PushRodLength = New_PushRodFR;
                    Vehicle.List_Vehicle[local_VehicleID].sc_RL.PushRodLength = New_PushRodRL;
                    Vehicle.List_Vehicle[local_VehicleID].sc_RR.PushRodLength = New_PushRodRR;

                    //
                    //Invoking the Kinematics and Vehicle Output Functions again 
                    #region Overriding the Corner Weights to the new values calculated
                    Vehicle.List_Vehicle[local_VehicleID].OverrideCornerWeights(Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW, Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW, Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW, Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW);
                    #endregion

                    progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 800, 1);
                    progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
                    M1_Global.vehicleGUI[local_VehicleID].ProgressBarVehicleGUI = progressBar;

                    M1_Global.vehicleGUI[local_VehicleID].ProgressBarVehicleGUI.Show();

                    Vehicle.List_Vehicle[local_VehicleID].vehicleGUI = M1_Global.vehicleGUI[local_VehicleID];

                    Vehicle.List_Vehicle[local_VehicleID].KinematicsInvoker(Vehicle.List_Vehicle[local_VehicleID].sc_FL.SuspensionMotionExists, SimulationType.StandToGround);
                    Vehicle.List_Vehicle[local_VehicleID].VehicleOutputs(1);

                    #region Coloring the Corner Weight and Pushrod Length Textboxes
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWFL.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthFL.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWFR.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthFR.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWRL.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthRL.BackColor = Color.LimeGreen;
                    M1_Global.vehicleGUI[local_VehicleID].CW_Def_WA.CWRR.BackColor = Color.White;
                    M1_Global.vehicleGUI[local_VehicleID].LL.PushRodLinkLengthRR.BackColor = Color.LimeGreen;
                    #endregion

                    PopulateOutputDataTable(Vehicle.List_Vehicle[local_VehicleID]);

                    progressBar.Hide();

                    DisplayOutputs(Vehicle.List_Vehicle[local_VehicleID]);

                    ///<remarks> Reactivate this eventually. No time right now</remarks>
                    //M1_Global.vehicleGUI[local_VehicleID].EditORCreateVehicleCAD(M1_Global.vehicleGUI[local_VehicleID].CADVehicleOutputs, local_VehicleID, false, M1_Global.vehicleGUI[local_VehicleID].Vehicle_MotionExists, 0, true, M1_Global.vehicleGUI[local_VehicleID].CadIsTobeImported);
                    Vehicle.List_Vehicle[local_VehicleID].Reset_CornerWeights(Vehicle.List_Vehicle[local_VehicleID].oc_FL[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_FR[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_RL[0].CW_1, Vehicle.List_Vehicle[local_VehicleID].oc_RR[0].CW_1, 0);

                    ChangeTracker++;



                }

                else
                    MessageBox.Show(" Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");




            }
            catch (Exception)
            {
                MessageBox.Show(" Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }
        }
        #endregion

        #endregion

        #endregion

        #region Graphics Coordinates User Control Operations
        public static void GraphicsCoordinatesHide()
        {
            R1 = R1.FormVariableUpdater();

            R1.GCArrow.SendToBack();
            R1.GC.SendToBack();
            R1.GCBar.SendToBack();
        }

        public static void GraphicsControlArrowOperations(double _arrowForce)
        {
            R1 = R1.FormVariableUpdater();



            R1.GCArrow.ArrowForce.Text = String.Format("{0:0.000}", (_arrowForce));

            R1.GCArrow.Show();
            R1.GC.SendToBack();
            R1.GCBar.SendToBack();
            R1.GCArrow.BringToFront();

        }

        public static void GraphicsCoordinatesJointsOperations(Point3D _temp_Point)
        {
            R1 = R1.FormVariableUpdater();

            R1.GC.Xcoordinate.Text = Convert.ToString(_temp_Point.Z);
            R1.GC.Ycoordinate.Text = Convert.ToString(_temp_Point.X);
            R1.GC.Zcoordinate.Text = Convert.ToString(_temp_Point.Y);

            //R1.GCBar.Hide();
            R1.GC.Show();
            R1.GCBar.SendToBack();
            R1.GCArrow.SendToBack();
            R1.GC.BringToFront();

        }

        public static void GraphicsCoordinatesBarOperations(Point3D _temp_Bar_Start, Point3D _temp_Bar_End, Vector3D _temp_Bar_Length, double _tempBarForce)
        {
            R1 = R1.FormVariableUpdater();

            R1.GCBar.X1coordinate.Text = Convert.ToString(_temp_Bar_Start.Z);
            R1.GCBar.Y1coordinate.Text = Convert.ToString(_temp_Bar_Start.X);
            R1.GCBar.Z1coordinate.Text = Convert.ToString(_temp_Bar_Start.Y);

            R1.GCBar.X2coordinate.Text = Convert.ToString(_temp_Bar_End.Z);
            R1.GCBar.Y2coordinate.Text = Convert.ToString(_temp_Bar_End.X);
            R1.GCBar.Z2coordinate.Text = Convert.ToString(_temp_Bar_End.Y);

            R1.GCBar.BarLength.Text = String.Format("{0:0.000}", _temp_Bar_Length.Length);

            double BarForce = (_tempBarForce);

            if (BarForce == 0)
            {
                R1.GCBar.BarForce.Hide();
                R1.GCBar.labelNeawton.Hide();
                R1.GCBar.labelForce.Hide();
            }
            else
            {
                R1.GCBar.BarForce.Show();
                R1.GCBar.labelForce.Show();
                R1.GCBar.labelNeawton.Show();
                R1.GCBar.BarForce.Text = String.Format("{0:0.000}", (_tempBarForce));
            }

            R1.GCBar.Show();
            R1.GC.SendToBack();
            R1.GCArrow.SendToBack();
            R1.GCBar.BringToFront();
        }

        #endregion

        #region OPEN / Save
        #region Declarations for OPEN/SAVE Commands
        Stream stream1;
        Stream stream2;
        Stream stream_Suspension;
        Stream stream_Form;
        Stream stream_Results;
        Stream stream_Motion;
        Stream stream_LoadCase;
        BinaryFormatter bformatter = new BinaryFormatter();
        BinaryFormatter bformatter_Form = new BinaryFormatter();
        BinaryFormatter bformatter_MotionAndLoadCase = new BinaryFormatter();
        OpenFileDialog openFileDialog1;
        string FileNameSave;
        string FileNameOpen;
        #endregion

        #region Get File Name Method
        private string GetFilename(string _fileName)
        {
            string _fileNameWOextensionSave;
            string _fileDirectorySave;

            _fileNameWOextensionSave = Path.GetFileNameWithoutExtension(_fileName);
            _fileDirectorySave = Path.GetDirectoryName(_fileName);

            return _fileDirectorySave + "\\" + _fileNameWOextensionSave;

        }
        #endregion

        #region Method to Get the file name of the previously opened file
        private string GetOldOpenedFileName(OpenFileDialog _openFileDialog)
        {
            try
            {
                return _openFileDialog.FileName;
            }
            catch (Exception)
            {

                return "No File Selected";
            }
        }
        #endregion

        #region SAVE Command

        #region Method to return an instance of the SaveFileDialog
        private SaveFileDialog Initialize_SaveFileDialog(SaveFileDialog _saveFileDialog, string _fileExtension)
        {
            //_saveFileDialog = new SaveFileDialog();
            _saveFileDialog.Filter = _fileExtension;
            _saveFileDialog.FilterIndex = 2;
            _saveFileDialog.RestoreDirectory = true;
            _saveFileDialog.OverwritePrompt = true;

            return _saveFileDialog;
        }
        #endregion

        #region Method to save the Load Case
        private void SaveLoadCase(BinaryFormatter _bfLoadCase, Stream _streamLoadCase, string _fileNameSave)
        {
            _streamLoadCase = File.Open(_fileNameSave + ".KSLoadCase", FileMode.Create);

            _bfLoadCase.Serialize(_streamLoadCase, LoadCase.List_LoadCases);
            _bfLoadCase.Serialize(_streamLoadCase, LoadCaseGUI.List_LoadCaseGUI);

            _streamLoadCase.Close();
        }
        #endregion

        #region Method to save the Motion
        private void SaveMotion(BinaryFormatter _bfMotion, Stream _streamMotion, string _filenameSave)
        {
            _streamMotion = File.Open(_filenameSave + ".KSMotion", FileMode.Create);

            _bfMotion.Serialize(_streamMotion, Motion.List_Motion);
            _bfMotion.Serialize(_streamMotion, MotionGUI.List_MotionGUI);

            _streamMotion.Close();
        }
        #endregion

        #region Method to save the Suspension
        private void SaveSuspension(BinaryFormatter _bfSuspension, Stream _streamSuspension, string _fileNameSave)
        {
            _streamSuspension = File.Open(_fileNameSave + ".KSus", FileMode.Create);

            _bfSuspension.Serialize(_streamSuspension, SuspensionCoordinatesFront.Assy_List_SCFL);
            _bfSuspension.Serialize(_streamSuspension, scflGUI);
            _bfSuspension.Serialize(_streamSuspension, navBarItemSCFLClass.navBarItemSCFL);

            _bfSuspension.Serialize(_streamSuspension, SuspensionCoordinatesFrontRight.Assy_List_SCFR);
            _bfSuspension.Serialize(_streamSuspension, scfrGUI);
            _bfSuspension.Serialize(_streamSuspension, navBarItemSCFRClass.navBarItemSCFR);

            _bfSuspension.Serialize(_streamSuspension, SuspensionCoordinatesRear.Assy_List_SCRL);
            _bfSuspension.Serialize(_streamSuspension, scrlGUI);
            _bfSuspension.Serialize(_streamSuspension, navBarItemSCRLClass.navBarItemSCRL);

            _bfSuspension.Serialize(_streamSuspension, SuspensionCoordinatesRearRight.Assy_List_SCRR);
            _bfSuspension.Serialize(_streamSuspension, scrrGUI);
            _bfSuspension.Serialize(_streamSuspension, navBarItemSCRRClass.navBarItemSCRR);


            _streamSuspension.Close();
        }
        #endregion

        #region Method to Export the Suspension alone
        private void barButtonItemExportSuspension_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialogSuspension = new SaveFileDialog();
            saveFileDialogSuspension = Initialize_SaveFileDialog(saveFileDialogSuspension, "KSus files (*.KSus)|*.KSus");

            if (saveFileDialogSuspension.ShowDialog() == DialogResult.OK)
            {
                bformatter = new BinaryFormatter();
                FileNameSave = GetFilename(saveFileDialogSuspension.FileName);
                SaveSuspension(bformatter, stream_Suspension, FileNameSave);
            }
        }
        #endregion

        #region Save Button Click Event
        private void barButtonSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl2.MainView.CloseEditor();
            gridControl2.MainView.UpdateCurrentRow();

            if (ChangeTracker != 0)
            {
                try
                {
                    #region Creating an instance of the SaveFileDialog
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1 = Initialize_SaveFileDialog(saveFileDialog1, "KS files (*.KS)|*.KS");

                    #endregion


                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        #region Creating new streams for the Objects and the GUI
                        stream1 = File.Open(saveFileDialog1.FileName, FileMode.Create);

                        FileNameSave = GetFilename(saveFileDialog1.FileName);

                        stream_Form = File.Open(FileNameSave + ".KSGUI", FileMode.Create);
                        stream_Results = File.Open(FileNameSave + ".KSResults", FileMode.Create);
                        #endregion

                        #region ORDER OF THESE STATEMENTS IS CRUCIAL - Serializing the Objects

                        bformatter.Serialize(stream1, Tire.Assy_List_Tire);
                        bformatter.Serialize(stream1, Tire.Assy_Tire);
                        bformatter.Serialize(stream1, tireGUI);
                        bformatter.Serialize(stream1, navBarItemTireClass.navBarItemTire);

                        bformatter.Serialize(stream1, Spring.Assy_List_Spring);
                        bformatter.Serialize(stream1, Spring.Assy_Spring);
                        bformatter.Serialize(stream1, springGUI);
                        bformatter.Serialize(stream1, navBarItemSpringClass.navBarItemSpring);

                        bformatter.Serialize(stream1, Damper.Assy_List_Damper);
                        bformatter.Serialize(stream1, Damper.Assy_Damper);
                        bformatter.Serialize(stream1, damperGUI);
                        bformatter.Serialize(stream1, navbarItemDamperClass.navBarItemDamper);

                        bformatter.Serialize(stream1, AntiRollBar.Assy_List_ARB);
                        bformatter.Serialize(stream1, AntiRollBar.Assy_ARB);
                        bformatter.Serialize(stream1, arbGUI);
                        bformatter.Serialize(stream1, navBarItemARBClass.navBarItemARB);

                        bformatter.Serialize(stream1, Chassis.Assy_List_Chassis);
                        bformatter.Serialize(stream1, Chassis.Assy_Chassis);
                        bformatter.Serialize(stream1, chassisGUI);
                        bformatter.Serialize(stream1, navBarItemChassisClass.navBarItemChassis);

                        bformatter.Serialize(stream1, WheelAlignment.Assy_List_WA);
                        bformatter.Serialize(stream1, WheelAlignment.Assy_WA);
                        bformatter.Serialize(stream1, waGUI);
                        bformatter.Serialize(stream1, navBarItemWAClass.navBarItemWA);

                        bformatter.Serialize(stream1, M1_Global.Assy_SCM);

                        bformatter.Serialize(stream1, M1_Global.vehicleGUI);

                        bformatter.Serialize(stream1, Vehicle.List_Vehicle);
                        bformatter.Serialize(stream1, navBarItemVehicleClass.navBarItemVehicle);

                        bformatter.Serialize(stream1, M1_Global.Assy_OC);

                        bformatter.Serialize(stream1, Vehicle.Assembled_Vehicle);

                        bformatter.Serialize(stream1, M1_Global.List_I1);

                        bformatter.Serialize(stream1, Simulation.List_Simulation);

                        navBarControlDesign.SaveToStream(stream1);

                        R1 = this;
                        K1 = new KinematicsSoftwareNewSerialization(R1);

                        SaveMotion(bformatter_MotionAndLoadCase, stream_Motion, FileNameSave);

                        SaveLoadCase(bformatter_MotionAndLoadCase, stream_LoadCase, FileNameSave);

                        bformatter_Form.Serialize(stream_Form, K1);

                        navBarControl1.SaveToStream(stream_Form);

                        navBarControlResults.SaveToStream(stream_Results);

                        SaveSuspension(bformatter, stream1, FileNameSave);

                        #endregion

                        this.Text = "Kinematic Software - " + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                    }

                    stream1.Close();
                    stream_Form.Close();
                    stream_Results.Close();
                    ChangeTracker = 0;
                }
                catch (Exception E)
                {
                    string soucrce = E.Source;
                    string message = E.Message;
                    return;
                }
            }
        }
        #endregion

        #endregion

        #region OPEN Command

        #region Method to Initalize the MotionView Data Grid
        private void InitalizeMotionGrid_For_Open()
        {
            ///<remarks>
            ///To prevent exception in case Vehicle and Motion is created but vehicle_Motion is null because calculations have not been performed. 
            ///vehicle_Motion is assigned a value only when calculations are done 
            ///IF/Else loop solves the above mentioned problem so the try/catch block can be eliminated
            /// </remarks>
            try
            {
                for (int i_recreateFridView_Motion = 0; i_recreateFridView_Motion < Vehicle.List_Vehicle.Count; i_recreateFridView_Motion++)
                {
                    if (Vehicle.List_Vehicle[i_recreateFridView_Motion].vehicle_Motion is null)
                    {

                    }
                    else Vehicle.List_Vehicle[i_recreateFridView_Motion].vehicle_Motion.Motion_MotionGUI.InitializeGridControl_MotionView(i_recreateFridView_Motion, this);
                }
            }
            catch (Exception)
            {


            }
        }
        #endregion

        #region Method to Open the Load Case
        private void OpenLoadCase(BinaryFormatter _bfLoadCase, Stream _streamLoadCase, string FileNameOpen)
        {
            LoadCase.List_LoadCases = null;
            LoadCase.LoadCaseCounter = 0;
            LoadCaseGUI.List_LoadCaseGUI = null;
            LoadCaseGUI._LoadCaseCounter = 0;

            _streamLoadCase = File.Open(FileNameOpen + ".KSLoadCase", FileMode.Open);

            LoadCase.List_LoadCases = (List<LoadCase>)_bfLoadCase.Deserialize(_streamLoadCase);
            LoadCaseGUI.List_LoadCaseGUI = (List<LoadCaseGUI>)_bfLoadCase.Deserialize(_streamLoadCase);

            for (int i_openLOC = 0; i_openLOC < LoadCaseGUI.List_LoadCaseGUI.Count; i_openLOC++)
            {
                LoadCaseGUI.List_LoadCaseGUI[i_openLOC].HandleGUI(navBarGroupLoadCases, navBarControlSimulation, this, i_openLOC, false);
                LoadCaseGUI.List_LoadCaseGUI[i_openLOC].InitializeGridControls(this);
            }

            _streamLoadCase.Close();



        }
        #endregion

        #region Exclusive code for Motion openeing
        private void OpenMotion(BinaryFormatter _bfMotion, Stream _streamMotion, string _fileNameOpen)
        {
            Motion.List_Motion = null;
            Motion.MotionCounter = 0;

            MotionGUI.List_MotionGUI = null;
            MotionGUI._MotionGUICounter = 0;

            _streamMotion = File.Open(_fileNameOpen + ".KSMotion", FileMode.Open);

            Motion.List_Motion = (List<Motion>)_bfMotion.Deserialize(_streamMotion);
            MotionGUI.List_MotionGUI = (List<MotionGUI>)_bfMotion.Deserialize(_streamMotion);

            for (int i_motion = 0; i_motion < MotionGUI.List_MotionGUI.Count; i_motion++)
            {
                MotionGUI.List_MotionGUI[i_motion].HandleGUI(navBarGroupMotion, navBarControlSimulation, this, i_motion);
                RecreateMotion(i_motion);
            }

            _streamMotion.Close();
        }
        #endregion

        #region Exclusive code for Suspension Opening and Importing 
        private void barButtonItemImportSuspension_ItemClick(object sender, ItemClickEventArgs e)
        {
            ///<remarks>
            ///This is the button click event to import the Suspension
            /// </remarks>
            gridControl2.MainView.CloseEditor();
            gridControl2.MainView.UpdateCurrentRow();
            if (ChangeTracker != 0)
            {
                DialogResult result = MessageBox.Show("Save file before proceeding?", "Save Prompt", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    barButtonSave.PerformClick();
                }
                else if (result == DialogResult.No) { }
            }
            bformatter = new BinaryFormatter();

            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "KSus files (*.KSus)|*.KSus";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;



            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileNameOpen = GetFilename(openFileDialog1.FileName);
                OpenSuspension(bformatter, stream_Suspension, FileNameOpen, true);

            }


        }
        private void ClearNavBarControl_For_Open_StandaloneSuspension()
        {
            ///<remarks>
            ///This method clears out the navBarControl off the Suspension Items and also the NavBarGroups of the Suspension Item Links
            /// </remarks>
            for (int i_open_scfl = 0; i_open_scfl < navBarItemSCFLClass.navBarItemSCFL.Count; i_open_scfl++)
            {
                //navBarControl2.Items.Remove(navBarItemSCFLClass.navBarItemSCFL[i_open_scfl]);
                navBarControlDesign.Items.Remove(navBarControlDesign.Items[navBarItemSCFLClass.navBarItemSCFL[i_open_scfl].Name]);
                navBarGroupSuspensionFL.ItemLinks.Remove(navBarControlDesign.Items[navBarItemSCFLClass.navBarItemSCFL[i_open_scfl].Name]);
                //navBarGroupSuspensionFL.ItemLinks.Remove(navBarItemSCFLClass.navBarItemSCFL[i_open_scfl]);

                TabControl_Outputs.TabPages.Remove(scflGUI[i_open_scfl].TabPage_FrontCAD);

            }
            for (int i_open_scfr = 0; i_open_scfr < navBarItemSCFRClass.navBarItemSCFR.Count; i_open_scfr++)
            {
                //navBarControl2.Items.Remove(navBarItemSCFRClass.navBarItemSCFR[i_open_scfr]);
                //navBarGroupSuspensionFR.ItemLinks.Remove(navBarItemSCFRClass.navBarItemSCFR[i_open_scfr]);
                navBarControlDesign.Items.Remove(navBarControlDesign.Items[navBarItemSCFRClass.navBarItemSCFR[i_open_scfr].Name]);
                navBarGroupSuspensionFR.ItemLinks.Remove(navBarControlDesign.Items[navBarItemSCFRClass.navBarItemSCFR[i_open_scfr].Name]);

            }
            for (int i_open_scrl = 0; i_open_scrl < navBarItemSCRLClass.navBarItemSCRL.Count; i_open_scrl++)
            {
                //navBarControl2.Items.Remove(navBarItemSCRLClass.navBarItemSCRL[i_open_scrl]);
                //navBarGroupSuspensionRL.ItemLinks.Remove(navBarItemSCRLClass.navBarItemSCRL[i_open_scrl]);
                navBarControlDesign.Items.Remove(navBarControlDesign.Items[navBarItemSCRLClass.navBarItemSCRL[i_open_scrl].Name]);
                navBarGroupSuspensionRL.ItemLinks.Remove(navBarControlDesign.Items[navBarItemSCRLClass.navBarItemSCRL[i_open_scrl].Name]);


                TabControl_Outputs.TabPages.Remove(scrlGUI[i_open_scrl].TabPage_RearCAD);

            }
            for (int i_open_scrr = 0; i_open_scrr < navBarItemSCRRClass.navBarItemSCRR.Count; i_open_scrr++)
            {
                //navBarControl2.Items.Remove(navBarItemSCRRClass.navBarItemSCRR[i_open_scrr]);
                //navBarGroupSuspensionRR.ItemLinks.Remove(navBarItemSCRRClass.navBarItemSCRR[i_open_scrr]);

                navBarControlDesign.Items.Remove(navBarControlDesign.Items[navBarItemSCRRClass.navBarItemSCRR[i_open_scrr].Name]);
                navBarGroupSuspensionRR.ItemLinks.Remove(navBarControlDesign.Items[navBarItemSCRRClass.navBarItemSCRR[i_open_scrr].Name]);

            }

        }
        private void AddSuspNavBarToNavControl_For_Open()
        {
            ///<remarks>
            ///This method adds the Suspension Items to the NavBarControl and NavBarGroups
            /// </remarks>
            for (int i_open_scfl = 0; i_open_scfl < navBarItemSCFLClass.navBarItemSCFL.Count; i_open_scfl++)
            {
                navBarControlDesign.Items.Add(navBarItemSCFLClass.navBarItemSCFL[i_open_scfl]);
                navBarGroupSuspensionFL.ItemLinks.Add(navBarItemSCFLClass.navBarItemSCFL[i_open_scfl]);
            }
            for (int i_open_scfr = 0; i_open_scfr < navBarItemSCFRClass.navBarItemSCFR.Count; i_open_scfr++)
            {
                navBarControlDesign.Items.Add(navBarItemSCFRClass.navBarItemSCFR[i_open_scfr]);
                navBarGroupSuspensionFR.ItemLinks.Add(navBarItemSCFRClass.navBarItemSCFR[i_open_scfr]);
            }
            for (int i_open_scrl = 0; i_open_scrl < navBarItemSCRLClass.navBarItemSCRL.Count; i_open_scrl++)
            {
                navBarControlDesign.Items.Add(navBarItemSCRLClass.navBarItemSCRL[i_open_scrl]);
                navBarGroupSuspensionRL.ItemLinks.Add(navBarItemSCRLClass.navBarItemSCRL[i_open_scrl]);
            }
            for (int i_open_scrr = 0; i_open_scrr < navBarItemSCRRClass.navBarItemSCRR.Count; i_open_scrr++)
            {
                navBarControlDesign.Items.Add(navBarItemSCRRClass.navBarItemSCRR[i_open_scrr]);
                navBarGroupSuspensionRR.ItemLinks.Add(navBarItemSCRRClass.navBarItemSCRR[i_open_scrr]);
            }

        }
        private void OpenSuspension(BinaryFormatter _bfSuspension, Stream _streamSuspension, string _fileNameSave, bool IsOpenedStandalone)
        {
            ///<remarks>
            ///This method opens the Suspension, it can be used for Importing the suspension exclusively or for openeing along with a project
            /// </remarks>
            try
            {
                //IsBeingOpened = true;
                if (IsOpenedStandalone)
                {
                    ClearNavBarControl_For_Open_StandaloneSuspension();
                }

                #region Setting the Suspension Items to null
                SuspensionCoordinatesFront.Assy_List_SCFL = null;
                SuspensionCoordinatesFront.SCFLCurrentID = 0;
                SuspensionCoordinatesFront.SCFLCounter = 0;
                scflGUI.Clear();
                scflGUI = null;
                navBarItemSCFLClass.navBarItemSCFL = null;

                SuspensionCoordinatesFrontRight.Assy_List_SCFR = null;
                SuspensionCoordinatesFrontRight.SCFRCurrentID = 0;
                SuspensionCoordinatesFrontRight.SCFRCounter = 0;
                scfrGUI.Clear();
                scfrGUI = null;
                navBarItemSCFRClass.navBarItemSCFR = null;

                SuspensionCoordinatesRear.Assy_List_SCRL = null;
                SuspensionCoordinatesRear.SCRLCurrentID = 0;
                SuspensionCoordinatesRear.SCRLCounter = 0;
                scrlGUI.Clear();
                scrlGUI = null;
                navBarItemSCRLClass.navBarItemSCRL = null;

                SuspensionCoordinatesRearRight.Assy_List_SCRR = null;
                SuspensionCoordinatesRearRight.SCRRCurrentID = 0;
                SuspensionCoordinatesRearRight.SCRRCounter = 0;
                scrrGUI.Clear();
                scrrGUI = null;
                navBarItemSCRRClass.navBarItemSCRR = null;
                #endregion

                _streamSuspension = File.Open(_fileNameSave + ".KSus", FileMode.Open);

                #region Loading the Suspension from the Stream
                SuspensionCoordinatesFront.Assy_List_SCFL = (List<SuspensionCoordinatesFront>)_bfSuspension.Deserialize(_streamSuspension);
                scflGUI = (List<SuspensionCoordinatesFrontGUI>)_bfSuspension.Deserialize(_streamSuspension);
                navBarItemSCFLClass.navBarItemSCFL = (List<navBarItemSCFLClass>)_bfSuspension.Deserialize(_streamSuspension);
                progressBar.PerformStep();
                progressBar.Update();

                SuspensionCoordinatesFrontRight.Assy_List_SCFR = (List<SuspensionCoordinatesFrontRight>)_bfSuspension.Deserialize(_streamSuspension);
                scfrGUI = (List<SuspensionCoordinatesFrontRightGUI>)_bfSuspension.Deserialize(_streamSuspension);
                navBarItemSCFRClass.navBarItemSCFR = (List<navBarItemSCFRClass>)_bfSuspension.Deserialize(_streamSuspension);
                progressBar.PerformStep();
                progressBar.Update();

                SuspensionCoordinatesRear.Assy_List_SCRL = (List<SuspensionCoordinatesRear>)_bfSuspension.Deserialize(_streamSuspension);
                scrlGUI = (List<SuspensionCoordinatesRearGUI>)_bfSuspension.Deserialize(_streamSuspension);
                navBarItemSCRLClass.navBarItemSCRL = (List<navBarItemSCRLClass>)_bfSuspension.Deserialize(_streamSuspension);
                progressBar.PerformStep();
                progressBar.Update();

                SuspensionCoordinatesRearRight.Assy_List_SCRR = (List<SuspensionCoordinatesRearRight>)_bfSuspension.Deserialize(_streamSuspension);
                scrrGUI = (List<SuspensionCoordinatesRearRightGUI>)_bfSuspension.Deserialize(_streamSuspension);
                navBarItemSCRRClass.navBarItemSCRR = (List<navBarItemSCRRClass>)_bfSuspension.Deserialize(_streamSuspension);
                progressBar.PerformStep();
                progressBar.Update();
                #endregion

                if (IsOpenedStandalone)
                {
                    AddSuspNavBarToNavControl_For_Open();
                }

                #region Resotring the Links of the Suspension navBaritems
                for (int i_open_scfl = 0; i_open_scfl < navBarItemSCFLClass.navBarItemSCFL.Count; i_open_scfl++)
                {
                    navBarControlDesign.Items[navBarItemSCFLClass.navBarItemSCFL[i_open_scfl].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFL_LinkClicked);
                }
                progressBar.PerformStep();
                progressBar.Update();

                for (int i_open_scfr = 0; i_open_scfr < navBarItemSCFRClass.navBarItemSCFR.Count; i_open_scfr++)
                {
                    navBarControlDesign.Items[navBarItemSCFRClass.navBarItemSCFR[i_open_scfr].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFR_LinkClicked);
                }
                progressBar.PerformStep();
                progressBar.Update();

                for (int i_open_scrl = 0; i_open_scrl < navBarItemSCRLClass.navBarItemSCRL.Count; i_open_scrl++)
                {
                    navBarControlDesign.Items[navBarItemSCRLClass.navBarItemSCRL[i_open_scrl].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRL_LinkClicked);
                }
                progressBar.PerformStep();
                progressBar.Update();

                for (int i_open_scrr = 0; i_open_scrr < navBarItemSCRRClass.navBarItemSCRR.Count; i_open_scrr++)
                {
                    navBarControlDesign.Items[navBarItemSCRRClass.navBarItemSCRR[i_open_scrr].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRR_LinkClicked);
                }
                progressBar.PerformStep();
                progressBar.Update();
                #endregion

                #region SCFL Grid View recreation
                for (int i_recreateGridView_SCFL = 0; i_recreateGridView_SCFL < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_recreateGridView_SCFL++)
                {
                    scflGUI[i_recreateGridView_SCFL].bandedGridView_SCFLGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_SCFL, 4, "Front Left Suspension Coordinates");
                    gridControl2.DataSource = scflGUI[i_recreateGridView_SCFL].SCFLDataTableGUI;
                    gridControl2.MainView = scflGUI[i_recreateGridView_SCFL].bandedGridView_SCFLGUI;
                    scflGUI[i_recreateGridView_SCFL].bandedGridView_SCFLGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scflGUI[i_recreateGridView_SCFL].bandedGridView_SCFLGUI, this);
                    scflGUI[i_recreateGridView_SCFL].bandedGridView_SCFLGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scflGUI[i_recreateGridView_SCFL].bandedGridView_SCFLGUI.OptionsNavigation.EnterMoveNextColumn = true;
                }
                #endregion
                progressBar.PerformStep();
                progressBar.Update();

                #region SCFR Grid View recreation
                for (int i_recreateGridView_SCFR = 0; i_recreateGridView_SCFR < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_recreateGridView_SCFR++)
                {
                    scfrGUI[i_recreateGridView_SCFR].bandedGridView_SCFRGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_SCFR, 4, "Front Right Suspension Coordinates");
                    gridControl2.DataSource = scfrGUI[i_recreateGridView_SCFR].SCFRDataTableGUI;
                    gridControl2.MainView = scfrGUI[i_recreateGridView_SCFR].bandedGridView_SCFRGUI;
                    scfrGUI[i_recreateGridView_SCFR].bandedGridView_SCFRGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scfrGUI[i_recreateGridView_SCFR].bandedGridView_SCFRGUI, this);
                    scfrGUI[i_recreateGridView_SCFR].bandedGridView_SCFRGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scfrGUI[i_recreateGridView_SCFR].bandedGridView_SCFRGUI.OptionsNavigation.EnterMoveNextColumn = true;
                }
                #endregion
                progressBar.PerformStep();
                progressBar.Update();

                #region SCFR Grid View recreation
                for (int i_recreateGridView_SCRL = 0; i_recreateGridView_SCRL < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_recreateGridView_SCRL++)
                {
                    scrlGUI[i_recreateGridView_SCRL].bandedGridView_SCRLGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_SCRL, 4, "Rear Left Suspension Coordinates");
                    gridControl2.DataSource = scrlGUI[i_recreateGridView_SCRL].SCRLDataTableGUI;
                    gridControl2.MainView = scrlGUI[i_recreateGridView_SCRL].bandedGridView_SCRLGUI;
                    scrlGUI[i_recreateGridView_SCRL].bandedGridView_SCRLGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scrlGUI[i_recreateGridView_SCRL].bandedGridView_SCRLGUI, this);
                    scrlGUI[i_recreateGridView_SCRL].bandedGridView_SCRLGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scrlGUI[i_recreateGridView_SCRL].bandedGridView_SCRLGUI.OptionsNavigation.EnterMoveNextColumn = true;
                }
                #endregion
                progressBar.PerformStep();
                progressBar.Update();

                #region SCRR Grid View recreation
                for (int i_recreateGridView_SCRR = 0; i_recreateGridView_SCRR < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_recreateGridView_SCRR++)
                {
                    scrrGUI[i_recreateGridView_SCRR].bandedGridView_SCRRGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_SCRR, 4, "Rear Right Suspension Coordinates");
                    gridControl2.DataSource = scrrGUI[i_recreateGridView_SCRR].SCRRDataTableGUI;
                    gridControl2.MainView = scrrGUI[i_recreateGridView_SCRR].bandedGridView_SCRRGUI;
                    scrrGUI[i_recreateGridView_SCRR].bandedGridView_SCRRGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scrrGUI[i_recreateGridView_SCRR].bandedGridView_SCRRGUI, this);
                    scrrGUI[i_recreateGridView_SCRR].bandedGridView_SCRRGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scrrGUI[i_recreateGridView_SCRR].bandedGridView_SCRRGUI.OptionsNavigation.EnterMoveNextColumn = true;
                }
                #endregion
                progressBar.PerformStep();
                progressBar.Update();

                #region Clearing out the Undo/Redo Stacks of Suspension
                for (int i_OPEN_UndoRedo_SCFL = 0; i_OPEN_UndoRedo_SCFL < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_OPEN_UndoRedo_SCFL++)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[i_OPEN_UndoRedo_SCFL]._UndocommandsSCFL = new Stack<ICommand>();
                    SuspensionCoordinatesFront.Assy_List_SCFL[i_OPEN_UndoRedo_SCFL]._RedocommandsSCFL = new Stack<ICommand>();
                }

                for (int i_OPEN_UndoRedo_SCFR = 0; i_OPEN_UndoRedo_SCFR < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_OPEN_UndoRedo_SCFR++)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_OPEN_UndoRedo_SCFR]._UndocommandsSCFR = new Stack<ICommand>();
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_OPEN_UndoRedo_SCFR]._RedocommandsSCFR = new Stack<ICommand>();
                }

                for (int i_OPEN_UndoRedo_SCRL = 0; i_OPEN_UndoRedo_SCRL < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_OPEN_UndoRedo_SCRL++)
                {
                    SuspensionCoordinatesRear.Assy_List_SCRL[i_OPEN_UndoRedo_SCRL]._UndocommandsSCRL = new Stack<ICommand>();
                    SuspensionCoordinatesRear.Assy_List_SCRL[i_OPEN_UndoRedo_SCRL]._RedocommandsSCRL = new Stack<ICommand>();
                }

                for (int i_OPEN_UndoRedo_SCRR = 0; i_OPEN_UndoRedo_SCRR < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_OPEN_UndoRedo_SCRR++)
                {
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_OPEN_UndoRedo_SCRR]._UndocommandsSCRR = new Stack<ICommand>();
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_OPEN_UndoRedo_SCRR]._RedocommandsSCRR = new Stack<ICommand>();
                }
                #endregion

                RecreateCAD_Suspension();

                #region Populating the Suspension Comboboxes
                ComboboxSCFLOperations();
                ComboBoxSCFROperations();
                ComboboxSCRLOperations();
                ComboboxSCRROperations();
                #endregion

                if (Vehicle.List_Vehicle.Count != 0 && IsOpenedStandalone)
                {
                    Object sender = new Object();
                    EventArgs e = new EventArgs();
                    InputComboboxes_Leave(sender, e);
                }

                _streamSuspension.Close();
                IsBeingOpened = false;
            }
            catch (Exception E)
            {
                string d = E.Message;
                IsBeingOpened = false;

            }
        }
        #endregion

        private void barButtonOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gridControl2.MainView.CloseEditor();
                gridControl2.MainView.UpdateCurrentRow();

                //TabControl_Outputs.Visible = false;

                if (ChangeTracker != 0)
                {
                    DialogResult result = MessageBox.Show("Save file before proceeding?", "Save Prompt", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        barButtonSave.PerformClick();
                    }
                    else if (result == DialogResult.No) { }
                }

                #region Creating an instance of the OpenFileDialog
                string OldFileName = GetOldOpenedFileName(openFileDialog1);

                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "KS files (*.KS)|*.KS";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                #endregion

                bformatter = new BinaryFormatter();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string NewFileName = openFileDialog1.FileName;
                    if (OldFileName == NewFileName)
                    {
                        OpenFile(stream2, openFileDialog1, bformatter);
                    }
                    else OpenFile(stream1, openFileDialog1, bformatter);

                    splashScreenManager2.CloseWaitForm();
                }

            }
            catch (Exception E)
            {
                string soucrce = E.Source;
                string message = E.Message;
                IsBeingOpened = false;
                splashScreenManager2.CloseWaitForm();
            }

        }

        private void OpenFile(Stream _stream, OpenFileDialog _openFileDialog, BinaryFormatter _bformatter)
        {

            IsBeingOpened = true;
            splashScreenManager2.ShowWaitForm();

            #region Progress Bar GUI

            progressBar = ProgressBarSerialization.CreateProgressBar(progressBar, 48, 1);
            progressBar.Show();
            progressBar.BringToFront();
            progressBar.AddProgressBarToRibbonStatusBar(this, progressBar);
            #endregion

            progressBar.PerformStep();
            progressBar.Update();

            #region Setting all the Objects to Null
            Tire.Assy_List_Tire = null;
            Tire.CurrentTireID = 0;
            Tire.TireCounter = 0;
            Tire.Assy_Tire = null;
            tireGUI = null;
            navBarItemTireClass.navBarItemTire = null;

            Spring.Assy_List_Spring = null;
            Spring.CurrentSpringID = 0;
            Spring.SpringCounter = 0;
            Spring.Assy_Spring = null;
            springGUI = null;
            navBarItemSpringClass.navBarItemSpring = null;

            Damper.Assy_List_Damper = null;
            Damper.CurrentDamperID = 0;
            Damper.DamperCounter = 0;
            Damper.Assy_Damper = null;
            damperGUI = null;
            navbarItemDamperClass.navBarItemDamper = null;

            AntiRollBar.Assy_List_ARB = null;
            AntiRollBar.CurrentAntiRollBarID = 0;
            AntiRollBar.AntiRollBarCounter = 0;
            AntiRollBar.Assy_ARB = null;
            arbGUI = null;
            navBarItemARBClass.navBarItemARB = null;

            Chassis.Assy_List_Chassis = null;
            Chassis.CurrentChassisID = 0;
            Chassis.ChassisCounter = 0;
            Chassis.Assy_Chassis = null;
            chassisGUI = null;
            navBarItemChassisClass.navBarItemChassis = null;

            WheelAlignment.Assy_List_WA = null;
            WheelAlignment.CurrentWheelAlignmentID = 0;
            WheelAlignment.WheelAlignmentCounter = 0;
            WheelAlignment.Assy_WA = null;
            waGUI = null;
            navBarItemWAClass.navBarItemWA = null;

            Motion.List_Motion.Clear();
            Motion.List_Motion = null;
            Motion.MotionCounter = 0;
            MotionGUI.List_MotionGUI = null;
            MotionGUI._MotionGUICounter = 0;

            M1_Global.Assy_SCM = null;

            M1_Global.vehicleGUI = null;

            Vehicle.List_Vehicle = null;
            Vehicle.CurrentVehicleID = 0;
            Vehicle.VehicleCounter = 0;
            navBarItemVehicleClass.navBarItemVehicle = null;
            Vehicle.Assembled_Vehicle = null;

            M1_Global.List_I1 = null;

            Simulation.List_Simulation.Clear();
            Simulation.List_Simulation = null;
            Simulation.SimulationCounter = 0;

            M1_Global.Assy_OC = null;

            progressBar.PerformStep();
            progressBar.Update();

            navBarControlDesign.Items.Clear();

            for (int i_nv2 = 0; i_nv2 < navBarControlDesign.Groups.Count; i_nv2++)
            {
                navBarControlDesign.Groups[i_nv2].ItemLinks.Clear();
            }

            navBarControlSimulation.Items.Clear();
            for (int i_nvS = 0; i_nvS < navBarControlSimulation.Groups.Count; i_nvS++)
            {
                navBarControlSimulation.Groups[i_nvS].ItemLinks.Clear();
            }

            TabControl_Outputs.TabPages.Clear();
            #endregion

            #region Opening the Object and GUI Streams
            _stream = File.Open(_openFileDialog.FileName, FileMode.Open);

            FileNameOpen = GetFilename(_openFileDialog.FileName);

            stream_Form = File.Open(FileNameOpen + ".KSGUI", FileMode.Open);
            stream_Results = File.Open(FileNameOpen + ".KSResults", FileMode.Open);

            progressBar.PerformStep();
            progressBar.Update();
            #endregion

            #region ORDER OF THESE STATMENTS IS CRUCIAL - Deserializing the Objects

            Tire.Assy_List_Tire = (List<Tire>)_bformatter.Deserialize(_stream);
            Tire.Assy_Tire = (Tire[])_bformatter.Deserialize(_stream);
            tireGUI = (List<TireGUI>)_bformatter.Deserialize(_stream);
            navBarItemTireClass.navBarItemTire = (List<navBarItemTireClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();


            Spring.Assy_List_Spring = (List<Spring>)_bformatter.Deserialize(_stream);
            Spring.Assy_Spring = (Spring[])_bformatter.Deserialize(_stream);
            springGUI = (List<SpringGUI>)_bformatter.Deserialize(_stream);
            navBarItemSpringClass.navBarItemSpring = (List<navBarItemSpringClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            Damper.Assy_List_Damper = (List<Damper>)_bformatter.Deserialize(_stream);
            Damper.Assy_Damper = (Damper[])_bformatter.Deserialize(_stream);
            damperGUI = ((List<DamperGUI>)_bformatter.Deserialize(_stream));
            navbarItemDamperClass.navBarItemDamper = (List<navbarItemDamperClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            AntiRollBar.Assy_List_ARB = (List<AntiRollBar>)_bformatter.Deserialize(_stream);
            AntiRollBar.Assy_ARB = (AntiRollBar[])_bformatter.Deserialize(_stream);
            arbGUI = ((List<AntiRollBarGUI>)_bformatter.Deserialize(_stream));
            navBarItemARBClass.navBarItemARB = (List<navBarItemARBClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            Chassis.Assy_List_Chassis = (List<Chassis>)_bformatter.Deserialize(_stream);
            Chassis.Assy_Chassis = (Chassis)_bformatter.Deserialize(_stream);
            chassisGUI = ((List<ChassisGUI>)_bformatter.Deserialize(_stream));
            navBarItemChassisClass.navBarItemChassis = (List<navBarItemChassisClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            WheelAlignment.Assy_List_WA = (List<WheelAlignment>)_bformatter.Deserialize(_stream);
            WheelAlignment.Assy_WA = (WheelAlignment[])_bformatter.Deserialize(_stream);
            waGUI = ((List<WheelAlignmentGUI>)_bformatter.Deserialize(_stream));
            navBarItemWAClass.navBarItemWA = (List<navBarItemWAClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            M1_Global.Assy_SCM = (SuspensionCoordinatesMaster[])_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            M1_Global.vehicleGUI = (List<VehicleGUI>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            Vehicle.List_Vehicle = (List<Vehicle>)_bformatter.Deserialize(_stream);
            navBarItemVehicleClass.navBarItemVehicle = (List<navBarItemVehicleClass>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();
            for (int i_vehicleGUI = 0; i_vehicleGUI < Vehicle.List_Vehicle.Count; i_vehicleGUI++)
            {
                M1_Global.vehicleGUI[i_vehicleGUI].Kinematics_Software_New_ObjectInitializer(this);
                M1_Global.vehicleGUI[i_vehicleGUI].ProgressBarVehicleGUI = progressBar;
            }

            M1_Global.Assy_OC = (OutputClass[])_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            Vehicle.Assembled_Vehicle = (Vehicle)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            M1_Global.List_I1 = (List<InputSheet>)_bformatter.Deserialize(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            Simulation.List_Simulation = (List<Simulation>)_bformatter.Deserialize(_stream);

            navBarControlDesign.RestoreFromStream(_stream);
            progressBar.PerformStep();
            progressBar.Update();

            OpenMotion(bformatter_MotionAndLoadCase, stream_Motion, FileNameOpen);

            OpenLoadCase(bformatter_MotionAndLoadCase, stream_LoadCase, FileNameOpen);

            R1 = this;
            K1 = new KinematicsSoftwareNewSerialization(R1);
            K1 = (KinematicsSoftwareNewSerialization)bformatter_Form.Deserialize(stream_Form);
            K1.RestoreForm(this);
            progressBar.PerformStep();
            progressBar.Update();

            navBarControl1.RestoreFromStream(stream_Form);
            progressBar.PerformStep();
            progressBar.Update();

            navBarControlResults.RestoreFromStream(stream_Results);
            progressBar.PerformStep();
            progressBar.Update();

            OpenSuspension(_bformatter, _stream, FileNameOpen, false);

            _stream.Close();
            stream_Form.Close();
            stream_Results.Close();

            #endregion

            this.Text = "Kinematics Software - " + Path.GetFileNameWithoutExtension(_openFileDialog.FileName);

            #region Re-creating the GridView elements

            #region Tire Grid View recreation
            for (int i_recreateGridView_Tire = 0; i_recreateGridView_Tire < Tire.Assy_List_Tire.Count; i_recreateGridView_Tire++)
            {
                tireGUI[i_recreateGridView_Tire].bandedGridView_TireGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_Tire, 2, "Tire Properties");
                gridControl2.DataSource = tireGUI[i_recreateGridView_Tire].TireDataTableGUI;
                gridControl2.MainView = tireGUI[i_recreateGridView_Tire].bandedGridView_TireGUI;
                tireGUI[i_recreateGridView_Tire].bandedGridView_TireGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                tireGUI[i_recreateGridView_Tire].bandedGridView_TireGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor);
                tireGUI[i_recreateGridView_Tire].bandedGridView_TireGUI.OptionsNavigation.EnterMoveNextColumn = true;
                tireGUI[i_recreateGridView_Tire].bandedGridView_TireGUI.OptionsNavigation.AutoMoveRowFocus = true;
            }
            #endregion
            progressBar.PerformStep();
            progressBar.Update();

            #region Spring Grid View recreation
            for (int i_recreateGridView_Spring = 0; i_recreateGridView_Spring < Spring.Assy_List_Spring.Count; i_recreateGridView_Spring++)
            {
                springGUI[i_recreateGridView_Spring].bandedGridView_SpringGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_Spring, 2, "Spring Properties");
                gridControl2.DataSource = springGUI[i_recreateGridView_Spring].SpringDataTableGUI;
                gridControl2.MainView = springGUI[i_recreateGridView_Spring].bandedGridView_SpringGUI;
                springGUI[i_recreateGridView_Spring].bandedGridView_SpringGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                springGUI[i_recreateGridView_Spring].bandedGridView_SpringGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor_Spring);
                springGUI[i_recreateGridView_Spring].bandedGridView_SpringGUI.OptionsNavigation.EnterMoveNextColumn = true;
            }
            #endregion
            progressBar.PerformStep();
            progressBar.Update();

            #region Damper Grid View recreation
            for (int i_recreateGridView_Damper = 0; i_recreateGridView_Damper < Damper.Assy_List_Damper.Count; i_recreateGridView_Damper++)
            {
                damperGUI[i_recreateGridView_Damper].bandedGridView_DamperGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_Damper, 2, " Damper Properties");
                gridControl2.DataSource = damperGUI[i_recreateGridView_Damper].DamperDataTableGUI;
                gridControl2.MainView = damperGUI[i_recreateGridView_Damper].bandedGridView_DamperGUI;
                damperGUI[i_recreateGridView_Damper].bandedGridView_DamperGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                damperGUI[i_recreateGridView_Damper].bandedGridView_DamperGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor);
                damperGUI[i_recreateGridView_Damper].bandedGridView_DamperGUI.OptionsNavigation.EnterMoveNextColumn = true;
            }
            #endregion
            progressBar.PerformStep();
            progressBar.Update();

            #region Anti Roll Bar Grid View Recreation
            for (int i_recreateGridView_ARB = 0; i_recreateGridView_ARB < AntiRollBar.Assy_List_ARB.Count; i_recreateGridView_ARB++)
            {
                arbGUI[i_recreateGridView_ARB].bandedGridView_ARBGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_ARB, 2, "Anti-Roll Bar Properties");
                gridControl2.DataSource = arbGUI[i_recreateGridView_ARB].ARBDataTableGUI;
                gridControl2.MainView = arbGUI[i_recreateGridView_ARB].bandedGridView_ARBGUI;
                arbGUI[i_recreateGridView_ARB].bandedGridView_ARBGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                arbGUI[i_recreateGridView_ARB].bandedGridView_ARBGUI.ValidatingEditor += new BaseContainerValidateEditorEventHandler(bandedGridView_ValidatingEditor);
                arbGUI[i_recreateGridView_ARB].bandedGridView_ARBGUI.OptionsNavigation.EnterMoveNextColumn = true;
            }
            #endregion
            progressBar.PerformStep();
            progressBar.Update();

            #region Wheel Alignment Grid View Recreation
            for (int i_recreateGridView_WA = 0; i_recreateGridView_WA < WheelAlignment.Assy_List_WA.Count; i_recreateGridView_WA++)
            {
                waGUI[i_recreateGridView_WA].bandedGridView_WAGUI = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_WA, 2, "Wheel Alignment");
                gridControl2.DataSource = waGUI[i_recreateGridView_WA].WADataTableGUI;
                gridControl2.MainView = waGUI[i_recreateGridView_WA].bandedGridView_WAGUI;
                waGUI[i_recreateGridView_WA].bandedGridView_WAGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                waGUI[i_recreateGridView_WA].bandedGridView_WAGUI.OptionsNavigation.EnterMoveNextColumn = true;
            }
            #endregion
            progressBar.PerformStep();
            progressBar.Update();

            #region Chassis Grid View recreation
            for (int i_recreateGridView_chassis = 0; i_recreateGridView_chassis < Chassis.Assy_List_Chassis.Count; i_recreateGridView_chassis++)
            {
                chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis = CustomBandedGridView.CreateNewBandedGridView(i_recreateGridView_chassis, 5, "Chassis Properties");
                gridControl2.DataSource = chassisGUI[i_recreateGridView_chassis].ChassisDataTableGUI;
                gridControl2.MainView = chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis;
                chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis.OptionsView.ShowColumnHeaders = true;
                chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis = CustomBandedGridColumn.ColumnEditor_ForChassis(chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis);
                chassisGUI[i_recreateGridView_chassis].bandedGridViewChassis.OptionsNavigation.EnterMoveNextColumn = true;
            }
            #endregion
            progressBar.PerformStep();
            progressBar.Update();

            InitalizeMotionGrid_For_Open();

            #endregion

            #region Restoring the Links of the navBarItems
            for (int i_open_tire = 0; i_open_tire < tireGUI.Count; i_open_tire++)
            {
                navBarControlDesign.Items[navBarItemTireClass.navBarItemTire[i_open_tire].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemTire1_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_spring = 0; i_open_spring < navBarItemSpringClass.navBarItemSpring.Count; i_open_spring++)
            {
                navBarControlDesign.Items[navBarItemSpringClass.navBarItemSpring[i_open_spring].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSpring_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_damper = 0; i_open_damper < navbarItemDamperClass.navBarItemDamper.Count; i_open_damper++)
            {
                navBarControlDesign.Items[navbarItemDamperClass.navBarItemDamper[i_open_damper].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemDamper_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_arb = 0; i_open_arb < navBarItemARBClass.navBarItemARB.Count; i_open_arb++)
            {
                navBarControlDesign.Items[navBarItemARBClass.navBarItemARB[i_open_arb].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemARB_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_chassis = 0; i_open_chassis < navBarItemChassisClass.navBarItemChassis.Count; i_open_chassis++)
            {
                navBarControlDesign.Items[navBarItemChassisClass.navBarItemChassis[i_open_chassis].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemChassis_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_wa = 0; i_open_wa < navBarItemWAClass.navBarItemWA.Count; i_open_wa++)
            {
                navBarControlDesign.Items[navBarItemWAClass.navBarItemWA[i_open_wa].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemWA_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_vehicle = 0; i_open_vehicle < M1_Global.vehicleGUI.Count; i_open_vehicle++)
            {
                navBarControlDesign.Items[navBarItemVehicleClass.navBarItemVehicle[i_open_vehicle].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemVehicle_LinkClicked);
            }
            progressBar.PerformStep();
            progressBar.Update();

            for (int i_open_simulation = 0; i_open_simulation < Simulation.List_Simulation.Count; i_open_simulation++)
            {
                Simulation.List_Simulation[i_open_simulation].HandleGUI(navBarGroupSimulation, navBarControlSimulation, this, i_open_simulation);
                Simulation.List_Simulation[i_open_simulation].simulationPanel.Hide();
                Simulation.List_Simulation[i_open_simulation].simulationPanel.SendToBack();
            }

            for (int i_open_inputsheet = 0; i_open_inputsheet < M1_Global.List_I1.Count; i_open_inputsheet++)
            {
                M1_Global.List_I1[i_open_inputsheet] = new InputSheet(this);
            }
            progressBar.PerformStep();
            progressBar.Update();

            #endregion

            #region Clearing out the Undo/Redo Stacks of Input Items 
            for (int i_OPEN_UndoRedo_Tire = 0; i_OPEN_UndoRedo_Tire < Tire.Assy_List_Tire.Count; i_OPEN_UndoRedo_Tire++)
            {
                Tire.Assy_List_Tire[i_OPEN_UndoRedo_Tire]._UndocommandsTire = new Stack<ICommand>();
                Tire.Assy_List_Tire[i_OPEN_UndoRedo_Tire]._RedocommandsTire = new Stack<ICommand>();
            }

            for (int i_OPEN_UndoRedo_Spring = 0; i_OPEN_UndoRedo_Spring < Spring.Assy_List_Spring.Count; i_OPEN_UndoRedo_Spring++)
            {
                Spring.Assy_List_Spring[i_OPEN_UndoRedo_Spring]._UndocommandsSpring = new Stack<ICommand>();
                Spring.Assy_List_Spring[i_OPEN_UndoRedo_Spring]._RedocommandsSpring = new Stack<ICommand>();
            }

            for (int i_OPEN_UndoRedo_Damper = 0; i_OPEN_UndoRedo_Damper < Damper.Assy_List_Damper.Count; i_OPEN_UndoRedo_Damper++)
            {
                Damper.Assy_List_Damper[i_OPEN_UndoRedo_Damper]._UndocommandsDamper = new Stack<ICommand>();
                Damper.Assy_List_Damper[i_OPEN_UndoRedo_Damper]._RedocommandsDamper = new Stack<ICommand>();
            }

            for (int i_OPEN_UndoRedo_AntiRollBar = 0; i_OPEN_UndoRedo_AntiRollBar < AntiRollBar.Assy_List_ARB.Count; i_OPEN_UndoRedo_AntiRollBar++)
            {
                AntiRollBar.Assy_List_ARB[i_OPEN_UndoRedo_AntiRollBar]._UndocommandsARB = new Stack<ICommand>();
                AntiRollBar.Assy_List_ARB[i_OPEN_UndoRedo_AntiRollBar]._RedocommandsARB = new Stack<ICommand>();
            }


            for (int i_OPEN_UndoRedo_Chassis = 0; i_OPEN_UndoRedo_Chassis < Chassis.Assy_List_Chassis.Count; i_OPEN_UndoRedo_Chassis++)
            {
                Chassis.Assy_List_Chassis[i_OPEN_UndoRedo_Chassis]._UndocommandsChassis = new Stack<ICommand>();
                Chassis.Assy_List_Chassis[i_OPEN_UndoRedo_Chassis]._RedocommandsChassis = new Stack<ICommand>();
            }

            for (int i_OPEN_UndoRedo_WA = 0; i_OPEN_UndoRedo_WA < WheelAlignment.Assy_List_WA.Count; i_OPEN_UndoRedo_WA++)
            {
                WheelAlignment.Assy_List_WA[i_OPEN_UndoRedo_WA]._UndocommandsWheelAlignment = new Stack<ICommand>();
                WheelAlignment.Assy_List_WA[i_OPEN_UndoRedo_WA]._RedocommandsWheelAlignment = new Stack<ICommand>();
            }

            //for (int i_OPEN_UndoRedo_SCFL = 0; i_OPEN_UndoRedo_SCFL < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_OPEN_UndoRedo_SCFL++)
            //{
            //    SuspensionCoordinatesFront.Assy_List_SCFL[i_OPEN_UndoRedo_SCFL]._UndocommandsSCFL = new Stack<ICommand>();
            //    SuspensionCoordinatesFront.Assy_List_SCFL[i_OPEN_UndoRedo_SCFL]._RedocommandsSCFL = new Stack<ICommand>();
            //}

            //for (int i_OPEN_UndoRedo_SCFR = 0; i_OPEN_UndoRedo_SCFR < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_OPEN_UndoRedo_SCFR++)
            //{
            //    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_OPEN_UndoRedo_SCFR]._UndocommandsSCFR = new Stack<ICommand>();
            //    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_OPEN_UndoRedo_SCFR]._RedocommandsSCFR = new Stack<ICommand>();
            //}

            //for (int i_OPEN_UndoRedo_SCRL = 0; i_OPEN_UndoRedo_SCRL < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_OPEN_UndoRedo_SCRL++)
            //{
            //    SuspensionCoordinatesRear.Assy_List_SCRL[i_OPEN_UndoRedo_SCRL]._UndocommandsSCRL = new Stack<ICommand>();
            //    SuspensionCoordinatesRear.Assy_List_SCRL[i_OPEN_UndoRedo_SCRL]._RedocommandsSCRL = new Stack<ICommand>();
            //}

            //for (int i_OPEN_UndoRedo_SCRR = 0; i_OPEN_UndoRedo_SCRR < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_OPEN_UndoRedo_SCRR++)
            //{
            //    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_OPEN_UndoRedo_SCRR]._UndocommandsSCRR = new Stack<ICommand>();
            //    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_OPEN_UndoRedo_SCRR]._RedocommandsSCRR = new Stack<ICommand>();
            //}
            #endregion

            progressBar.PerformStep();
            progressBar.Update();

            #region Re-populating the Comboboxes
            ComboboxTireOperations();
            ComboBoxSpringOperations();
            ComboboxDamperOperations();
            ComboboxARBOperations();
            ComboboxChassisOperations();
            ComboboxWheelAlignmentOperations();
            comboBoxSimulationMotionOperations();
            ComboboxBatchRunVehicleOperations();
            ComboboxBatchRunVehicleOperations();
            ComboboxSimulationVehicleOperations();
            comboBoxLoadCaseOperations();
            K1.RestoreComboboxSelectedIndex(this);
            #endregion

            RecreateCAD_Vehicle();
            RecreateResults();

            progressBar.PerformStep();
            progressBar.Update();

            RestoreGrid_navBarSelectedGroup();
            progressBar.PerformStep();
            progressBar.Update();

            #region Reseting the UndoRedo Object
            UndoObject.ResetUndoRedo();

            UndoObject_EnableDisableUndoRedoFeature(null, null);
            #endregion

            #region The below lines of code or functional right now because of a glitch which is causing recalculate to fail if a project is loaded
            //
            //The below lines of code or functional right now because of a glitch which is causing recalculate to fail if a project is loaded
            //

            //
            //#region Enabling/Disabling the Recalculate based on McPherson or Results_Tracker
            //if (Vehicle.Assembled_Vehicle.McPhersonFront == 1)
            //{
            //    ribbonPageGroupRecalculate.Enabled = false;
            //    return;
            //}
            //else if (Vehicle.Assembled_Vehicle.McPhersonRear == 1)
            //{
            //    ribbonPageGroupRecalculate.Enabled = false;
            //    return;
            //}

            //if (Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker == 1)
            //{
            //    ribbonPageGroupRecalculate.Enabled = true;
            //}
            //else if (Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker == 0)
            //{
            //    ribbonPageGroupRecalculate.Enabled = false;
            //} 
            //#endregion 
            #endregion

            progressBar.Hide();

            ChangeTracker = 0;

            IsBeingOpened = false;

            #region TabControlOperations
            K1.RestoreTabControl(this);
            TabControl_Outputs.Visible = true;
            #endregion

        }

        #region Displaying in the gridview the Data Table of the Input item which was selected while the save operation was done
        private void Restore_For_Design()
        {
            if (navBarControlDesign.ActiveGroup == navBarGroupTireStiffness)
            {
                gridControl2.MainView = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].bandedGridView_TireGUI;
                gridControl2.DataSource = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].TireDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSprings)
            {
                gridControl2.MainView = springGUI[navBarGroupSprings.SelectedLinkIndex].bandedGridView_SpringGUI;
                gridControl2.DataSource = springGUI[navBarGroupSprings.SelectedLinkIndex].SpringDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupDamper)
            {
                gridControl2.MainView = damperGUI[navBarGroupDamper.SelectedLinkIndex].bandedGridView_DamperGUI;
                gridControl2.DataSource = damperGUI[navBarGroupDamper.SelectedLinkIndex].DamperDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupAntiRollBar)
            {
                gridControl2.MainView = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].bandedGridView_ARBGUI;
                gridControl2.DataSource = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].ARBDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupWheelAlignment)
            {
                gridControl2.MainView = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].bandedGridView_WAGUI;
                gridControl2.DataSource = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].WADataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupChassis)
            {
                gridControl2.MainView = chassisGUI[navBarGroupChassis.SelectedLinkIndex].bandedGridViewChassis;
                gridControl2.DataSource = chassisGUI[navBarGroupChassis.SelectedLinkIndex].ChassisDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionFL)
            {
                gridControl2.MainView = scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].bandedGridView_SCFLGUI;
                gridControl2.DataSource = scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].SCFLDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionFR)
            {
                gridControl2.MainView = scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].bandedGridView_SCFRGUI;
                gridControl2.DataSource = scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].SCFRDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionRL)
            {
                gridControl2.MainView = scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].bandedGridView_SCRLGUI;
                gridControl2.DataSource = scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].SCRLDataTableGUI;
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionRR)
            {
                gridControl2.MainView = scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].bandedGridView_SCRRGUI;
                gridControl2.DataSource = scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].SCRRDataTableGUI;
            }
        }

        private void Restore_For_Results()
        {
            for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
            {
                if (navBarControlResults.ActiveGroup.Name == M1_Global.vehicleGUI[i].navBarGroup_Vehicle_Result.Name)
                {
                    int indexOfMotion = Vehicle.List_Vehicle[i].vehicle_Motion.MotionID - 1;
                    gridControl2.MainView = MotionGUI.List_MotionGUI[indexOfMotion].bandedGridView_Motion;
                    gridControl2.DataSource = Vehicle.List_Vehicle[i].vehicle_Motion.Motion_DataTable;
                }
            }
        }

        private void RestoreGrid_navBarSelectedGroup()
        {
            try
            {
                #region Displaying in the gridview the Data Table of the Input item which was selected while the save operation was done

                if (navBarControl1.ActiveGroup == navBarGroupDesign)
                {
                    Restore_For_Design();
                }
                else if (navBarControl1.ActiveGroup == navBarGroupResults)
                {
                    Restore_For_Results();
                }

                #endregion
            }
            catch (Exception)
            {
                sidePanel2.Hide();
            }
        }

        #endregion

        #region Recreating Motion
        private void RecreateMotion(int i)
        {
            ///<remarks>
            ///The IF loop is necessary because if a motion is created but no points are created on the chart and the project is saved (Unless a point is created on the chart, the <c>ChartPoints_WheelDef_X</c> will not be initialized and if saved in this state then it will null 
            ///and this operation will fail. 
            ///<seealso cref="Motion.GetWheelDeflectionAndSteer(MotionGUI, bool, bool, bool)"/>
            /// </remarks>
            if (MotionGUI.List_MotionGUI[i].ChartPoints_WheelDef_X != null)
            {
                for (int i_Def = 0; i_Def < MotionGUI.List_MotionGUI[i].ChartPoints_WheelDef_X.Length; i_Def++)
                {
                    MotionGUI.List_MotionGUI[i].motionGUI_MotionChart.AddPointToChart(MotionGUI.List_MotionGUI[i].motionGUI_MotionChart.chartControl1, MotionGUI.List_MotionGUI[i].ChartPoints_WheelDef_X[i_Def], MotionGUI.List_MotionGUI[i].ChartPoints_WheelDef_Y[i_Def], 0);
                }
            }

            if (MotionGUI.List_MotionGUI[i].ChartPoints_Steering_X != null)
            {
                for (int i_steer = 0; i_steer < MotionGUI.List_MotionGUI[i].ChartPoints_Steering_X.Length; i_steer++)
                {
                    MotionGUI.List_MotionGUI[i].motionGUI_MotionChart.AddPointToChart(MotionGUI.List_MotionGUI[i].motionGUI_MotionChart.chartControl2, MotionGUI.List_MotionGUI[i].ChartPoints_Steering_X[i_steer], MotionGUI.List_MotionGUI[i].ChartPoints_Steering_Y[i_steer], 0);
                }
            }

        }
        #endregion

        #region Recreating CAD
        private void RecreateCAD_Suspension()
        {
            for (int i_RecreateFrontSusCAD = 0; i_RecreateFrontSusCAD < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_RecreateFrontSusCAD++)
            {
                scflGUI[i_RecreateFrontSusCAD].FrontCADPreProcessor(scflGUI[i_RecreateFrontSusCAD], i_RecreateFrontSusCAD, true);
            }

            for (int i_RecreateRearSusCAD = 0; i_RecreateRearSusCAD < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_RecreateRearSusCAD++)
            {
                scrlGUI[i_RecreateRearSusCAD].RearCADPreProcessor(scrlGUI[i_RecreateRearSusCAD], i_RecreateRearSusCAD, true);
            }
        }

        private void RecreateCAD_Vehicle()
        {
            for (int i_RecreateCAD = 0; i_RecreateCAD < M1_Global.vehicleGUI.Count; i_RecreateCAD++)
            {
                ///<summary>
                ///Re-plotting all the Suspension Items
                /// </summary>
                M1_Global.vehicleGUI[i_RecreateCAD].VehicleCADPreProcessor(M1_Global.vehicleGUI[i_RecreateCAD], i_RecreateCAD, true, M1_Global.vehicleGUI[i_RecreateCAD].CadIsTobeImported, Vehicle.List_Vehicle[i_RecreateCAD].SuspensionIsAssembled);
                if (M1_Global.vehicleGUI[i_RecreateCAD].CadIsTobeImported)
                {
                    ///<summary>
                    ///Re-importing the CAD items which were imported (if they were imported)
                    /// </summary>
                    M1_Global.vehicleGUI[i_RecreateCAD].CADVehicleInputs.CloneImportedCAD(ref M1_Global.vehicleGUI[i_RecreateCAD].FileHasBeenImported, ref M1_Global.vehicleGUI[i_RecreateCAD].CadIsTobeImported, false,
                                                                                        M1_Global.vehicleGUI[i_RecreateCAD].importCADForm.importCADViewport.igesEntities);
                    M1_Global.vehicleGUI[i_RecreateCAD].CADVehicleInputs.openFileDialog1 = new OpenFileDialog();
                    M1_Global.vehicleGUI[i_RecreateCAD].CADVehicleInputs.openFileDialog1.FileName = M1_Global.vehicleGUI[i_RecreateCAD].IGESFIleName;
                }
            }
        }
        #endregion

        #region Recreating the Results
        public void RecreateResults()
        {
            for (int i_RecreareResults = 0; i_RecreareResults < Vehicle.List_Vehicle.Count; i_RecreareResults++)
            {


                if (Vehicle.List_Vehicle[i_RecreareResults].Vehicle_Results_Tracker == 1)
                {
                    FindOutPutIndex(0);
                    PopulateOutputDataTable(Vehicle.List_Vehicle[i_RecreareResults]);

                    M1_Global.vehicleGUI[i_RecreareResults].AddSuspensionGridtoScrollableControl();
                    M1_Global.vehicleGUI[i_RecreareResults].AddUserControlToTabPage(M1_Global.vehicleGUI[i_RecreareResults].TabPages_Vehicle);

                    TabControl_Outputs = CustomXtraTabPage.ClearTabPages(TabControl_Outputs, M1_Global.vehicleGUI[i_RecreareResults].TabPages_Vehicle);
                    TabControl_Outputs = CustomXtraTabPage.AddTabPages(TabControl_Outputs, M1_Global.vehicleGUI[Vehicle.List_Vehicle[i_RecreareResults].VehicleID - 1].TabPages_Vehicle);

                    DisplayOutputs(Vehicle.List_Vehicle[i_RecreareResults]);

                    M1_Global.vehicleGUI[i_RecreareResults].PopulateSuspensionGridControl(this, M1_Global.vehicleGUI[i_RecreareResults], Vehicle.List_Vehicle[i_RecreareResults], OutputIndex);

                    PopulateInputSheet(Vehicle.List_Vehicle[i_RecreareResults]);

                    if (M1_Global.vehicleGUI[i_RecreareResults].CadIsTobeImported)
                    {
                        M1_Global.vehicleGUI[i_RecreareResults].CADVehicleOutputs.CloneImportedCAD(ref M1_Global.vehicleGUI[i_RecreareResults].FileHasBeenImported, ref M1_Global.vehicleGUI[i_RecreareResults].CadIsTobeImported, true,
                                                                                            M1_Global.vehicleGUI[i_RecreareResults].importCADForm.importCADViewport.igesEntities);
                        M1_Global.vehicleGUI[i_RecreareResults].CADVehicleOutputs.openFileDialog1 = new OpenFileDialog();
                        M1_Global.vehicleGUI[i_RecreareResults].CADVehicleOutputs.openFileDialog1.FileName = M1_Global.vehicleGUI[i_RecreareResults].IGESFIleName;
                    }

                    M1_Global.vehicleGUI[i_RecreareResults].EditORCreateVehicleCAD(M1_Global.vehicleGUI[i_RecreareResults].CADVehicleOutputs, i_RecreareResults, false, M1_Global.vehicleGUI[i_RecreareResults].Vehicle_MotionExists, 0, false,
                                                                                                                                    M1_Global.vehicleGUI[i_RecreareResults].CadIsTobeImported, M1_Global.vehicleGUI[i_RecreareResults].PlotWheel);

                    M1_Global.vehicleGUI[i_RecreareResults].IS.Kinematics_Software_New_ObjectInitializer(this);
                }

            }

            foreach (NavBarGroup item in navBarControlResults.Groups)
            {
                navBarControlResults.Groups[item.Name].ItemLinks.Clear();
            }

            for (int i_nVC = 0; i_nVC < navBarControlResults.Groups.Count; i_nVC++)
            {
                for (int i_V = 0; i_V < M1_Global.vehicleGUI.Count; i_V++)
                {
                    if (navBarControlResults.Groups[i_nVC].Name == M1_Global.vehicleGUI[i_V].navBarGroup_Vehicle_Result.Name)
                    {
                        ///<remarks>
                        ///Storing the index of the Nav Bar Result Items in a temporary list so that they can be restored later
                        /// </remarks>
                        navBarResultVehicleIndex.Insert(i_nVC, i_V);
                        break;
                    }
                }
            }
            navBarControlResults.Groups.Clear();
            for (int i_reAdd = 0; i_reAdd < navBarResultVehicleIndex.Count; i_reAdd++)
            {
                for (int i_V = 0; i_V < M1_Global.vehicleGUI.Count; i_V++)
                {
                    ///<remarks>
                    ///Using the temporary list of indices to recreate the navBarControlResults in the same order as it was
                    /// </remarks>
                    if (navBarResultVehicleIndex[i_reAdd] == i_V)
                    {
                        Results_NavBarOerations(Vehicle.List_Vehicle[i_V]);
                    }
                }
            }
            Button_Recalculate_Enabler();
        }
        #endregion

        #endregion
        #endregion

        #region New Project event

        #region Method to reset everything
        private void ResetAllObjectAndControls()
        {
            #region Reseting all the lists, Arrays and Counters
            Tire.Assy_List_Tire = new List<Tire>();
            Tire.Assy_Tire = new Tire[4];
            tireGUI = new List<TireGUI>();
            Tire.TireCounter = 0;
            Tire.CurrentTireID = 0;
            navBarItemTireClass.navBarItemTire = new List<navBarItemTireClass>();
            for (int i_NEW_UndoRedo_Tire = 0; i_NEW_UndoRedo_Tire < Tire.Assy_List_Tire.Count; i_NEW_UndoRedo_Tire++)
            {
                Tire.Assy_List_Tire[i_NEW_UndoRedo_Tire]._UndocommandsTire = new Stack<ICommand>();
                Tire.Assy_List_Tire[i_NEW_UndoRedo_Tire]._RedocommandsTire = new Stack<ICommand>();
            }

            Spring.Assy_List_Spring = new List<Spring>();
            Spring.Assy_Spring = new Spring[4];
            springGUI = new List<SpringGUI>();
            Spring.SpringCounter = 0;
            Spring.CurrentSpringID = 0;
            navBarItemSpringClass.navBarItemSpring = new List<navBarItemSpringClass>();
            for (int i_NEW_UndoRedo_Spring = 0; i_NEW_UndoRedo_Spring < Spring.Assy_List_Spring.Count; i_NEW_UndoRedo_Spring++)
            {
                Spring.Assy_List_Spring[i_NEW_UndoRedo_Spring]._UndocommandsSpring = new Stack<ICommand>();
                Spring.Assy_List_Spring[i_NEW_UndoRedo_Spring]._RedocommandsSpring = new Stack<ICommand>();
            }


            Damper.Assy_List_Damper = new List<Damper>();
            Damper.Assy_Damper = new Damper[4];
            damperGUI = new List<DamperGUI>();
            Damper.DamperCounter = 0;
            Damper.CurrentDamperID = 0;
            navbarItemDamperClass.navBarItemDamper = new List<navbarItemDamperClass>();
            for (int i_NEW_UndoRedo_Damper = 0; i_NEW_UndoRedo_Damper < Damper.Assy_List_Damper.Count; i_NEW_UndoRedo_Damper++)
            {
                Damper.Assy_List_Damper[i_NEW_UndoRedo_Damper]._UndocommandsDamper = new Stack<ICommand>();
                Damper.Assy_List_Damper[i_NEW_UndoRedo_Damper]._RedocommandsDamper = new Stack<ICommand>();
            }



            AntiRollBar.Assy_List_ARB = new List<AntiRollBar>();
            AntiRollBar.Assy_ARB = new AntiRollBar[4];
            arbGUI = new List<AntiRollBarGUI>();
            AntiRollBar.AntiRollBarCounter = 0;
            AntiRollBar.CurrentAntiRollBarID = 0;
            navBarItemARBClass.navBarItemARB = new List<navBarItemARBClass>();
            for (int i_NEW_UndoRedo_AntiRollBar = 0; i_NEW_UndoRedo_AntiRollBar < AntiRollBar.Assy_List_ARB.Count; i_NEW_UndoRedo_AntiRollBar++)
            {
                AntiRollBar.Assy_List_ARB[i_NEW_UndoRedo_AntiRollBar]._UndocommandsARB = new Stack<ICommand>();
                AntiRollBar.Assy_List_ARB[i_NEW_UndoRedo_AntiRollBar]._RedocommandsARB = new Stack<ICommand>();
            }



            Chassis.Assy_List_Chassis = new List<Chassis>();
            Chassis.Assy_Chassis = new Chassis();
            chassisGUI = new List<ChassisGUI>();
            Chassis.ChassisCounter = 0;
            Chassis.CurrentChassisID = 0;
            navBarItemChassisClass.navBarItemChassis = new List<navBarItemChassisClass>();
            for (int i_NEW_UndoRedo_Chassis = 0; i_NEW_UndoRedo_Chassis < Chassis.Assy_List_Chassis.Count; i_NEW_UndoRedo_Chassis++)
            {
                Chassis.Assy_List_Chassis[i_NEW_UndoRedo_Chassis]._UndocommandsChassis = new Stack<ICommand>();
                Chassis.Assy_List_Chassis[i_NEW_UndoRedo_Chassis]._RedocommandsChassis = new Stack<ICommand>();
            }



            WheelAlignment.Assy_List_WA = new List<WheelAlignment>();
            WheelAlignment.Assy_WA = new WheelAlignment[4];
            waGUI = new List<WheelAlignmentGUI>();
            WheelAlignment.WheelAlignmentCounter = 0;
            WheelAlignment.CurrentWheelAlignmentID = 0;
            navBarItemWAClass.navBarItemWA = new List<navBarItemWAClass>();
            for (int i_NEW_UndoRedo_WA = 0; i_NEW_UndoRedo_WA < WheelAlignment.Assy_List_WA.Count; i_NEW_UndoRedo_WA++)
            {
                WheelAlignment.Assy_List_WA[i_NEW_UndoRedo_WA]._UndocommandsWheelAlignment = new Stack<ICommand>();
                WheelAlignment.Assy_List_WA[i_NEW_UndoRedo_WA]._RedocommandsWheelAlignment = new Stack<ICommand>();
            }



            SuspensionCoordinatesFront.Assy_List_SCFL = new List<SuspensionCoordinatesFront>();
            SuspensionCoordinatesFront.SCFLCounter = 0;
            SuspensionCoordinatesFront.SCFLCurrentID = 0;

            scflGUI = new List<SuspensionCoordinatesFrontGUI>();
            navBarItemSCFLClass.navBarItemSCFL = new List<navBarItemSCFLClass>();
            for (int i_NEW_UndoRedo_SCFL = 0; i_NEW_UndoRedo_SCFL < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_NEW_UndoRedo_SCFL++)
            {

                SuspensionCoordinatesFront.Assy_List_SCFL[i_NEW_UndoRedo_SCFL]._UndocommandsSCFL = new Stack<ICommand>();
                SuspensionCoordinatesFront.Assy_List_SCFL[i_NEW_UndoRedo_SCFL]._RedocommandsSCFL = new Stack<ICommand>();
            }



            SuspensionCoordinatesFrontRight.Assy_List_SCFR = new List<SuspensionCoordinatesFrontRight>();
            SuspensionCoordinatesFrontRight.SCFRCounter = 0;
            SuspensionCoordinatesFrontRight.SCFRCurrentID = 0;
            scfrGUI = new List<SuspensionCoordinatesFrontRightGUI>();
            navBarItemSCFRClass.navBarItemSCFR = new List<navBarItemSCFRClass>();
            for (int i_NEW_UndoRedo_SCFR = 0; i_NEW_UndoRedo_SCFR < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_NEW_UndoRedo_SCFR++)
            {
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_NEW_UndoRedo_SCFR]._UndocommandsSCFR = new Stack<ICommand>();
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_NEW_UndoRedo_SCFR]._RedocommandsSCFR = new Stack<ICommand>();
            }



            SuspensionCoordinatesRear.Assy_List_SCRL = new List<SuspensionCoordinatesRear>();
            SuspensionCoordinatesRear.SCRLCounter = 0;
            SuspensionCoordinatesRear.SCRLCurrentID = 0;
            scrlGUI = new List<SuspensionCoordinatesRearGUI>();
            navBarItemSCRLClass.navBarItemSCRL = new List<navBarItemSCRLClass>();
            for (int i_NEW_UndoRedo_SCRL = 0; i_NEW_UndoRedo_SCRL < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_NEW_UndoRedo_SCRL++)
            {
                SuspensionCoordinatesRear.Assy_List_SCRL[i_NEW_UndoRedo_SCRL]._UndocommandsSCRL = new Stack<ICommand>();
                SuspensionCoordinatesRear.Assy_List_SCRL[i_NEW_UndoRedo_SCRL]._RedocommandsSCRL = new Stack<ICommand>();
            }



            SuspensionCoordinatesRearRight.Assy_List_SCRR = new List<SuspensionCoordinatesRearRight>();
            SuspensionCoordinatesRearRight.SCRRCounter = 0;
            SuspensionCoordinatesRearRight.SCRRCurrentID = 0;
            scrrGUI = new List<SuspensionCoordinatesRearRightGUI>();
            navBarItemSCRRClass.navBarItemSCRR = new List<navBarItemSCRRClass>();
            for (int i_NEW_UndoRedo_SCRR = 0; i_NEW_UndoRedo_SCRR < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_NEW_UndoRedo_SCRR++)
            {
                SuspensionCoordinatesRearRight.Assy_List_SCRR[i_NEW_UndoRedo_SCRR]._UndocommandsSCRR = new Stack<ICommand>();
                SuspensionCoordinatesRearRight.Assy_List_SCRR[i_NEW_UndoRedo_SCRR]._RedocommandsSCRR = new Stack<ICommand>();
            }

            for (int i = 0; i < Motion.List_Motion.Count; i++)
            {
                Motion.List_Motion[i].Motion_MotionGUI = new MotionGUI();
            }
            Motion.List_Motion.Clear();
            Motion.List_Motion = new List<Motion>();
            Motion.MotionCounter = 0;
            MotionGUI.List_MotionGUI = new List<MotionGUI>();
            MotionGUI._MotionGUICounter = 0;

            for (int i = 0; i < Simulation.List_Simulation.Count; i++)
            {
                Simulation.List_Simulation[i].Simulation_Motion = new Motion();
                Simulation.List_Simulation[i].Simulation_Vehicle = new Vehicle();
            }
            Simulation.List_Simulation.Clear();
            Simulation.List_Simulation = new List<Simulation>();
            Simulation.SimulationCounter = 0;

            M1_Global.Assy_SCM = new SuspensionCoordinatesMaster[4];

            M1_Global.vehicleGUI = new List<VehicleGUI>();
            progressBar = new ProgressBarSerialization();
            Vehicle.List_Vehicle = new List<Vehicle>();
            navBarItemVehicleClass.navBarItemVehicle = new List<navBarItemVehicleClass>();
            Vehicle.VehicleCounter = 0;
            Vehicle.CurrentVehicleID = 0;
            Vehicle.Assembled_Vehicle = new Vehicle();

            M1_Global.List_I1 = new List<InputSheet>();
            InputSheet.InputSheetCounter = 0; ;

            M1_Global.Assy_OC = new OutputClass[4];

            UndoObject.ResetUndoRedo();

            UndoObject_EnableDisableUndoRedoFeature(null, null);
            #endregion

            #region Reseting the navBarControl
            navBarControlDesign.Items.Clear();
            navBarControlSimulation.Items.Clear();
            navBarControlResults.Items.Clear();
            navBarControlResults.Groups.Clear();
            #endregion

            #region Reseting the comboBoxes
            comboBoxSCFL.Items.Clear();
            comboBoxSCFR.Items.Clear();
            comboBoxSCRL.Items.Clear();
            comboBoxSCRR.Items.Clear();

            comboBoxTireFL.Items.Clear();
            comboBoxTireFR.Items.Clear();
            comboBoxTireRL.Items.Clear();
            comboBoxTireRR.Items.Clear();

            comboBoxSpringFL.Items.Clear();
            comboBoxSpringFR.Items.Clear();
            comboBoxSpringRL.Items.Clear();
            comboBoxSpringRR.Items.Clear();

            comboBoxDamperFL.Items.Clear();
            comboBoxDamperFR.Items.Clear();
            comboBoxDamperRL.Items.Clear();
            comboBoxDamperRR.Items.Clear();

            comboBoxARBFront.Items.Clear();
            comboBoxARBRear.Items.Clear();

            comboBoxChassis.Items.Clear();

            comboBoxWAFL.Items.Clear();
            comboBoxWAFR.Items.Clear();
            comboBoxWARL.Items.Clear();
            comboBoxWARR.Items.Clear();

            for (int i_Simulation = 0; i_Simulation < Simulation.List_Simulation.Count; i_Simulation++)
            {
                Simulation.List_Simulation[i_Simulation].simulationPanel.comboBoxVehicle.Items.Clear();
                Simulation.List_Simulation[i_Simulation].simulationPanel.comboBoxMotion.Items.Clear();
            }
            #endregion

            sidePanel2.Hide();

            gridControl2.BindingContext = new BindingContext();
            gridControl2.DataSource = null;
            gridControl2.ForceInitialize();
            gridControl2.MainView = null;
            gridControl2.BringToFront();
            groupControl13.Controls.Add(gridControl2);
            gridControl2.Dock = DockStyle.Fill;

            navBarControl1.ActiveGroup = navBarGroupDesign;

            ribbon.SelectedPage = ribbonPageDesign;
            ribbon.Select();

            TabControl_Outputs.TabPages.Clear();
        }
        #endregion

        #region Button Click event. This is fired indirectly through the backstage view button
        private void barButtonNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl2.MainView.CloseEditor();
            gridControl2.MainView.UpdateCurrentRow();

            if (ChangeTracker != 0)
            {
                DialogResult result = MessageBox.Show("Save changes to file?", "Save Prompt", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    barButtonSave_ItemClick(sender, e);

                    //ResetAllObjectAndControls();
                    Application.Restart();
                }

                else if (result == DialogResult.No)
                {
                    //ResetAllObjectAndControls();

                    ChangeTracker = 0;

                    Application.Restart();
                }

                else if (result == DialogResult.Cancel) { }
            }
            else if (ChangeTracker == 0)
            {
                Application.Restart();
                //ResetAllObjectAndControls();
            }



            this.Text = "Kinematics Software ";
        }
        #endregion

        #endregion

        #region Form closing event
        private void Kinematics_Software_New_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChangeTracker != 0)
            {
                DialogResult result = MessageBox.Show("Save changes to file before closing?", "Save Prompt", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    barButtonSave.PerformClick();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Undo / Redo
        private void barButtonRedo_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl2.MainView.CloseEditor();
            gridControl2.MainView.UpdateCurrentRow();
            UndoObject.Redo(1);
        }

        private void barButtonUndo_ItemClick(object sender, ItemClickEventArgs e)
        {


            gridControl2.MainView.CloseEditor();
            gridControl2.MainView.UpdateCurrentRow();
            UndoObject.Undo(1);


        }




        #endregion

        #region Backstage View Operations
        private void backstageViewButtonItemClose_ItemClick(object sender, BackstageViewItemEventArgs e)
        {
            backstageViewControl1.Hide();
        }
        private void backstageViewButtonItemOpen_ItemClick(object sender, BackstageViewItemEventArgs e)
        {
            barButtonOpen.PerformClick();
            backstageViewControl1.Hide();

        }
        private void backstageViewButtonItemSave_ItemClick(object sender, BackstageViewItemEventArgs e)
        {
            barButtonSave.PerformClick();
            backstageViewControl1.Hide();
        }
        private void simpleButtonNew_Click(object sender, EventArgs e)
        {

            barButtonNew.PerformClick();
            backstageViewControl1.Close();

        }
        #endregion

        #region Name Change of Input Items and Vehicle

        #region Name Changing Methods
        private void Tire_NameChange(int _index)
        {
            #region Changing the Tire Name
            string NewName = toolStripTextBoxRenameInputItem.Text;

            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].tire_FL._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_FL._TireName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].tire_FR._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_RR._TireName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].tire_RL._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_RL._TireName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].tire_RR._TireName == Tire.Assy_List_Tire[_index]._TireName)
                    {

                        Vehicle.List_Vehicle[i].tire_RR._TireName = NewName;

                    }
                }
                Tire.Assy_List_Tire[_index]._TireName = NewName;
                ComboboxTireOperations();
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }
        private void SCFL_NameChange(int _index)
        {
            #region Changing the Front Left Suspension Coordinate Name
            string NewName = toolStripTextBoxRenameInputItem.Text;

            try
            {
                int Index;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].sc_FL._SCName == SuspensionCoordinatesFront.Assy_List_SCFL[_index]._SCName)
                    {
                        Vehicle.List_Vehicle[i].sc_FL._SCName = NewName;

                    }
                }


                Index = TabControl_Outputs.TabPages.IndexOf(scflGUI[_index].TabPage_FrontCAD);
                TabControl_Outputs.TabPages[Index].Text = NewName;

                SuspensionCoordinatesFront.Assy_List_SCFL[_index]._SCName = NewName;
                ComboboxSCFLOperations();
            }
            catch (Exception) { }
            #endregion
        }
        private void SCFR_NameChange(int _index)
        {
            #region Changing the Front Right Suspension Coordinate Name
            string NewName = toolStripTextBoxRenameInputItem.Text;

            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].sc_FR._SCName == SuspensionCoordinatesFrontRight.Assy_List_SCFR[_index]._SCName)
                    {
                        Vehicle.List_Vehicle[i].sc_FR._SCName = NewName;
                    }
                }

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[_index]._SCName = NewName;
                ComboBoxSCFROperations();
            }
            catch (Exception) { }
            #endregion
        }

        private void SCRL_NameChange(int _index)
        {
            #region Changing the Rear Left Suspension Coordinate Name
            string NewName = toolStripTextBoxRenameInputItem.Text;

            try
            {
                int Index;
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].sc_RL._SCName == SuspensionCoordinatesRear.Assy_List_SCRL[_index]._SCName)
                    {
                        Vehicle.List_Vehicle[i].sc_RL._SCName = NewName;
                    }
                }

                Index = TabControl_Outputs.TabPages.IndexOf(scrlGUI[_index].TabPage_RearCAD);
                TabControl_Outputs.TabPages[Index].Text = NewName;

                SuspensionCoordinatesRear.Assy_List_SCRL[_index]._SCName = NewName;
                ComboboxSCRLOperations();
            }
            catch (Exception) { }
            #endregion
        }
        private void SCRR_NameChange(int _index)
        {
            #region Changing the Rear Right Suspension Coordinate Name
            string NewName = toolStripTextBoxRenameInputItem.Text;

            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].sc_RR._SCName == SuspensionCoordinatesRearRight.Assy_List_SCRR[_index]._SCName)
                    {

                        Vehicle.List_Vehicle[i].sc_RR._SCName = NewName;

                    }
                }
                SuspensionCoordinatesRearRight.Assy_List_SCRR[_index]._SCName = NewName;
                ComboboxSCRROperations();
            }
            catch (Exception) { }
            #endregion
        }

        private void Chassis_NameChanged(int _index)
        {
            string NewName = toolStripTextBoxRenameInputItem.Text;

            #region Changing the Chassis Name
            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].chassis_vehicle._ChassisName == Chassis.Assy_List_Chassis[_index]._ChassisName)
                    {

                        Vehicle.List_Vehicle[i].chassis_vehicle._ChassisName = NewName;

                    }
                }
                Chassis.Assy_List_Chassis[_index]._ChassisName = NewName;
                ComboboxChassisOperations();
            }
            catch (Exception) { }
            #endregion
        }

        private void Sring_NameChanged(int _index)
        {
            String NewName = toolStripTextBoxRenameInputItem.Text;

            #region Changing the Spring Name
            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].spring_FL._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {
                        Vehicle.List_Vehicle[i].spring_FL._SpringName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].spring_FR._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {

                        Vehicle.List_Vehicle[i].spring_FR._SpringName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].spring_RL._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {


                        Vehicle.List_Vehicle[i].spring_RL._SpringName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].spring_RR._SpringName == Spring.Assy_List_Spring[_index]._SpringName)
                    {

                        Vehicle.List_Vehicle[i].spring_RR._SpringName = NewName;

                    }
                }
                Spring.Assy_List_Spring[_index]._SpringName = NewName;
                ComboBoxSpringOperations();
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }

        private void Damper_NameChanged(int _index)
        {
            String NewName = toolStripTextBoxRenameInputItem.Text;

            #region Changing the Damper Name
            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].damper_FL._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {

                        Vehicle.List_Vehicle[i].damper_FL._DamperName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].damper_FR._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {

                        Vehicle.List_Vehicle[i].damper_FR._DamperName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].damper_RL._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {

                        Vehicle.List_Vehicle[i].damper_RL._DamperName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].damper_RR._DamperName == Damper.Assy_List_Damper[_index]._DamperName)
                    {

                        Vehicle.List_Vehicle[i].damper_RR._DamperName = NewName;

                    }
                }
                Damper.Assy_List_Damper[_index]._DamperName = NewName;
                ComboboxDamperOperations();
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }

        private void ARB_NameChanged(int _index)
        {
            String NewName = toolStripTextBoxRenameInputItem.Text;

            #region Changing the ARB Name
            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].arb_FL._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {

                        Vehicle.List_Vehicle[i].arb_FL._ARBName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].arb_FR._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {

                        Vehicle.List_Vehicle[i].arb_FR._ARBName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].arb_RL._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {

                        Vehicle.List_Vehicle[i].arb_RL._ARBName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].arb_RR._ARBName == AntiRollBar.Assy_List_ARB[_index]._ARBName)
                    {

                        Vehicle.List_Vehicle[i].arb_RR._ARBName = NewName;

                    }
                }
                AntiRollBar.Assy_List_ARB[_index]._ARBName = NewName;
                ComboboxARBOperations();
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }
        private void WA_NameChanged(int _index)
        {
            String NewName = toolStripTextBoxRenameInputItem.Text;

            #region Changing the WheelAlignment Name
            try
            {
                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    if (Vehicle.List_Vehicle[i].wa_FL._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_FL._WAName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].wa_FR._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_FR._WAName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].wa_RL._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_RL._WAName = NewName;

                    }

                    if (Vehicle.List_Vehicle[i].wa_RR._WAName == WheelAlignment.Assy_List_WA[_index]._WAName)
                    {

                        Vehicle.List_Vehicle[i].wa_RR._WAName = NewName;

                    }
                }
                WheelAlignment.Assy_List_WA[_index]._WAName = NewName;
                ComboboxWheelAlignmentOperations();
            }
            catch (Exception)
            {

                // Vehicle not assembled if this Exception is encountered
            }
            #endregion
        }

        private void barButtonItemOutputOrigin_DownChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void popupControlContainerOutputOrigin_VisibleChanged(object sender, EventArgs e)
        {
            radioButtonTransToCS.Checked = false;

            radioButtonTranstoGround.Checked = false;

        }

        #endregion

        #region Textbox Content Change Event for Name Change
        private void toolStripTextBoxRenameInputItem_Leave(object sender, EventArgs e)
        {
            if (navBarControlDesign.ActiveGroup == navBarGroupTireStiffness)
            {
                int index = navBarGroupTireStiffness.SelectedLinkIndex;
                navBarItemTireClass.navBarItemTire[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemTireClass.navBarItemTire[index].Name].Caption = navBarItemTireClass.navBarItemTire[index].Caption;
                Tire_NameChange(index);

            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionFL)
            {
                int index = navBarGroupSuspensionFL.SelectedLinkIndex;
                navBarItemSCFLClass.navBarItemSCFL[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCFLClass.navBarItemSCFL[index].Name].Caption = navBarItemSCFLClass.navBarItemSCFL[index].Caption;
                SCFL_NameChange(index);

                navBarItemSCFRClass.navBarItemSCFR[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCFRClass.navBarItemSCFR[index].Name].Caption = navBarItemSCFRClass.navBarItemSCFR[index].Caption;
                SCFR_NameChange(index);
            }

            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionFR)
            {
                int index = navBarGroupSuspensionFR.SelectedLinkIndex;
                navBarItemSCFRClass.navBarItemSCFR[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCFRClass.navBarItemSCFR[index].Name].Caption = navBarItemSCFRClass.navBarItemSCFR[index].Caption;
                SCFR_NameChange(index);

                navBarItemSCFLClass.navBarItemSCFL[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCFLClass.navBarItemSCFL[index].Name].Caption = navBarItemSCFLClass.navBarItemSCFL[index].Caption;
                SCFL_NameChange(index);
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionRL)
            {
                int index = navBarGroupSuspensionRL.SelectedLinkIndex;
                navBarItemSCRLClass.navBarItemSCRL[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCRLClass.navBarItemSCRL[index].Name].Caption = navBarItemSCRLClass.navBarItemSCRL[index].Caption;
                SCRL_NameChange(index);

                navBarItemSCRRClass.navBarItemSCRR[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCRRClass.navBarItemSCRR[index].Name].Caption = navBarItemSCRRClass.navBarItemSCRR[index].Caption;
                SCRR_NameChange(index);
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSuspensionRR)
            {
                int index = navBarGroupSuspensionRR.SelectedLinkIndex;
                navBarItemSCRRClass.navBarItemSCRR[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCRRClass.navBarItemSCRR[index].Name].Caption = navBarItemSCRRClass.navBarItemSCRR[index].Caption;
                SCRR_NameChange(index);

                navBarItemSCRLClass.navBarItemSCRL[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSCRLClass.navBarItemSCRL[index].Name].Caption = navBarItemSCRLClass.navBarItemSCRL[index].Caption;
                SCRL_NameChange(index);
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupSprings)
            {
                int index = navBarGroupSprings.SelectedLinkIndex;
                navBarItemSpringClass.navBarItemSpring[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemSpringClass.navBarItemSpring[index].Name].Caption = navBarItemSpringClass.navBarItemSpring[index].Caption;
                Sring_NameChanged(index);
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupDamper)
            {
                int index = navBarGroupDamper.SelectedLinkIndex;
                navbarItemDamperClass.navBarItemDamper[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navbarItemDamperClass.navBarItemDamper[index].Name].Caption = navbarItemDamperClass.navBarItemDamper[index].Caption;
                Damper_NameChanged(index);
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupAntiRollBar)
            {
                int index = navBarGroupAntiRollBar.SelectedLinkIndex;
                navBarItemARBClass.navBarItemARB[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemARBClass.navBarItemARB[index].Name].Caption = navBarItemARBClass.navBarItemARB[index].Caption;
                ARB_NameChanged(index);
            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupChassis)
            {
                int index = navBarGroupChassis.SelectedLinkIndex;
                navBarItemChassisClass.navBarItemChassis[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemChassisClass.navBarItemChassis[index].Name].Caption = navBarItemChassisClass.navBarItemChassis[index].Caption;
                Chassis_NameChanged(index);

            }
            else if (navBarControlDesign.ActiveGroup == navBarGroupWheelAlignment)
            {
                int index = navBarGroupWheelAlignment.SelectedLinkIndex;
                navBarItemWAClass.navBarItemWA[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemWAClass.navBarItemWA[index].Name].Caption = navBarItemWAClass.navBarItemWA[index].Caption;
                WA_NameChanged(index);
            }



            else if (navBarControlDesign.ActiveGroup == navBarGroupVehicle)
            {
                int index = navBarGroupVehicle.SelectedLinkIndex, IndexOfTabPage;
                navBarItemVehicleClass.navBarItemVehicle[index].Caption = toolStripTextBoxRenameInputItem.Text;
                navBarControlDesign.Items[navBarItemVehicleClass.navBarItemVehicle[index].Name].Caption = navBarItemVehicleClass.navBarItemVehicle[index].Caption;
                Vehicle.List_Vehicle[index]._VehicleName = toolStripTextBoxRenameInputItem.Text;

                IndexOfTabPage = TabControl_Outputs.TabPages.IndexOf(M1_Global.vehicleGUI[index].TabPage_VehicleInputCAD);
                TabControl_Outputs.TabPages[IndexOfTabPage].Text = toolStripTextBoxRenameInputItem.Text;

                ComboboxSimulationVehicleOperations();
                ComboboxBatchRunVehicleOperations();

                NavBarGroupResults_NameChange(index);
                NavBarItemResults_NameChanged(index);
                TabPagesVehicle_NameChange(index);

            }

            ChangeTracker++;

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void navBarControlSimulation_Click(object sender, EventArgs e)
        {

        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }



        private void NavBarGroupResults_NameChange(int _index)
        {
            try
            {
                foreach (NavBarGroup Group in navBarControlResults.Groups)
                {
                    if (M1_Global.vehicleGUI[_index].navBarGroup_Vehicle_Result.Name == Group.Name)
                    {
                        navBarControlResults.Groups[Group.Name].Caption = toolStripTextBoxRenameInputItem.Text + " - Results";
                        M1_Global.vehicleGUI[_index].navBarGroup_Vehicle_Result.Caption = navBarControlResults.Groups[Group.Name].Caption;
                    }
                }
            }
            catch (Exception)
            {
                // To safeguard in case calculations have not been done for the Vehicle
            }
        }


        private void NavBarItemResults_NameChanged(int _index)
        {
            try
            {
                int IndexOfItem;
                for (int i_navBarItemNameChange = 0; i_navBarItemNameChange < M1_Global.vehicleGUI[_index].navBarItem_Vehicle_Results.Count; i_navBarItemNameChange++)
                {
                    IndexOfItem = navBarControlResults.Items.IndexOf(M1_Global.vehicleGUI[_index].navBarItem_Vehicle_Results[i_navBarItemNameChange]);
                    navBarControlResults.Items[IndexOfItem].Caption = M1_Global.vehicleGUI[_index].navBarItem_Vehicle_Results[i_navBarItemNameChange].Name + toolStripTextBoxRenameInputItem.Text;
                }
            }
            catch (Exception)
            {
                // To safeguard in case calculations have not been done for the Vehicle
            }
        }

        private void TabPagesVehicle_NameChange(int _index)
        {
            try
            {
                int IndexOfPage;

                for (int i_TabPageNameChange = 0; i_TabPageNameChange < M1_Global.vehicleGUI[_index].TabPages_Vehicle.Count; i_TabPageNameChange++)
                {
                    IndexOfPage = TabControl_Outputs.TabPages.IndexOf(M1_Global.vehicleGUI[_index].TabPages_Vehicle[i_TabPageNameChange]);
                    TabControl_Outputs.TabPages[IndexOfPage].Text = M1_Global.vehicleGUI[_index].TabPages_Vehicle[i_TabPageNameChange].Name + toolStripTextBoxRenameInputItem.Text;
                }

            }
            catch (Exception)
            {
                // To safeguard in case calculations have not been done for the Vehicle
            }
        }


        #endregion 
        #endregion

        #region Look and Feel Operations

        string SkinName;
        private void barButtonItemRegular_ItemClick(object sender, ItemClickEventArgs e)
        {
            //defaultLookAndFeel1.EnableBonusSkins = true;
            defaultLookAndFeel1.LookAndFeel.SkinName = "VS2010";
            navBarControl1.LookAndFeel.SkinName = "VS2010";
            navBarControlDesign.LookAndFeel.SkinName = "VS2010";
            TabControl_Outputs.LookAndFeel.SkinName = "VS2010";
            navBarControlResults.LookAndFeel.SkinName = "VS2010";
            navBarControlSimulation.LookAndFeel.SkinName = "VS2010";
            gridControl2.LookAndFeel.SkinName = "VS2010";
            groupControl13.LookAndFeel.SkinName = "VS2010";
            accordionControlVehicleItem.LookAndFeel.SkinName = "VS2010";
            SkinName = "VS2010";
        }

        private void barButtonItemWhite_ItemClick(object sender, ItemClickEventArgs e)
        {
            //defaultLookAndFeel1.EnableBonusSkins = true;
            defaultLookAndFeel1.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            navBarControl1.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            navBarControlDesign.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            TabControl_Outputs.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            navBarControlResults.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            navBarControlSimulation.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            gridControl2.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            groupControl13.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            accordionControlVehicleItem.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            SkinName = "Visual Studio 2013 Light";
        }

        private void barButtonItemDark_ItemClick(object sender, ItemClickEventArgs e)
        {
            //defaultLookAndFeel1.EnableBonusSkins = true;
            defaultLookAndFeel1.LookAndFeel.SkinName = "DevExpress Dark Style";
            navBarControl1.LookAndFeel.SkinName = "DevExpress Dark Style";
            navBarControlDesign.LookAndFeel.SkinName = "DevExpress Dark Style";
            TabControl_Outputs.LookAndFeel.SkinName = "DevExpress Dark Style";
            navBarControlResults.LookAndFeel.SkinName = "DevExpress Dark Style";
            navBarControlSimulation.LookAndFeel.SkinName = "DevExpress Dark Style";
            gridControl2.LookAndFeel.SkinName = "DevExpress Dark Style";
            groupControl13.LookAndFeel.SkinName = "DevExpress Dark Style";
            accordionControlVehicleItem.LookAndFeel.SkinName = "DevExpress Dark Style";
            SkinName = "DevExpress Dark Style";
        }
        #endregion

        #region About Software
        private void simpleButtonAbout_Click(object sender, EventArgs e)
        {
            //About A = new About();
            //A.LookAndFeel.SkinName = SkinName;
            //A.Size = new Size(570, 700);
            //A.Show();
        }
        #endregion

        public static Kinematics_Software_New AssignFormVariable()
        {
            return R1;
        }

        /// <summary>
        /// Don't delete. Try to make this work 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonUserManual_ItemClick(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    System.Diagnostics.Process.Start(@".\User Manual Weekly Deliverable.pdf");
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("File not found");
            //}
        }


        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Temp_BobillierMethod tempBob = new Temp_BobillierMethod();
            //if (SuspensionCoordinatesFront.Assy_List_SCFL != null)
            //{
            //    if (SuspensionCoordinatesFront.Assy_List_SCFL.Count != 0 && Vehicle.List_Vehicle.Count != 0) 
            //    {
            //        tempBob.AssignLocalSuspensionObject(Vehicle.List_Vehicle[0], SuspensionCoordinatesFront.Assy_List_SCFL[0], 1);
            //        tempBob.ConstructBobilierLine();
            //        tempBob.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("HRHRRRHHHKHKKKHH");
            //    }
            //}
        }


    }
}
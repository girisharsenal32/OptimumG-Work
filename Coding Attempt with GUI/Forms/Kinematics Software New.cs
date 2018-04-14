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


namespace Coding_Attempt_with_GUI
{
    
    public partial class Kinematics_Software_New : DevExpress.XtraBars.Ribbon.RibbonForm
    {



        #region Initialization of the Form and ObjectInitializer Class's Object
        public static Kinematics_Software_New R1;
        public static ObjectInitializer M1_Global;
        public KinematicsSoftwareNewSerialization K1;
        #endregion

        #region Constructor
        public Kinematics_Software_New()
        {
            InitializeComponent();
            R1 = this;
            this.DoubleBuffered = true;

            #region GUI tasks - Hiding
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            tabPaneResults.Hide();
            sidePanel2.Hide();
            #endregion

            M1_Global = new ObjectInitializer();

            #region Creating a Form to display the Progress Bar
            ProgressBarForm = new Form();
            ProgressBarForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ProgressBarForm.Size = new System.Drawing.Size(300, 15);
            ProgressBarForm.ShowInTaskbar = false;
            ProgressBarForm.StartPosition = FormStartPosition.CenterScreen;
            ProgressBarForm.TopMost = true;
            #endregion

            #region Creating the Progress Bar and Label and initializaing it
            progressBar = new ProgressBarSerialization();
            progressBar.Name = "Progress Bar";
            //ProgressBarForm.Controls.Add(progressBar);
            //progressBar.Dock = DockStyle.Fill;
            //progressBar.SendToBack();
            progressBar.Properties.Maximum = 800;
            progressBar.Properties.Step = 1;
            progressBar.Hide();
            ribbonStatusBar.Controls.Add(progressBar);
            progressBar.Dock = DockStyle.Right;

            #endregion

            #region Assigning an Event to the UndoRedo Class' EnableDisableUndoRedoFeature to determine whether the Undo button shuld be enabled or diasbled
            UndoObject.EnableDisableUndoRedoFeature += new EventHandler(UndoObject_EnableDisableUndoRedoFeature);
            #endregion


        } 
        #endregion

        #region Method to determine wheterh the Undo/Redo buttons should be enabled or diabled
        void UndoObject_EnableDisableUndoRedoFeature(object sender, EventArgs e)
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

        Form ProgressBarForm;

        public int CalculateResultsButtonClickCounter = 1;

        public static int InputItemID_For_Undo;

        public UndoRedo UndoObject = new UndoRedo();

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

            #region Declaration of the Global List of the Vehicle GUI Object
            public VehicleGUI vehicleGUI; // Declard Here so that it can be 
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

                #region Initialization of the Global List of the Vehicle item
                vehicleGUI = new VehicleGUI();
                #endregion

            }
        } 
        #endregion

        #region GUI Code to copy the Left coordinates to the Right and vice versa

        #region Suspension Coordinate Symmetry Identifier
        public bool FrontSymmetry = true;
        public bool RearSymmetry = true;
        #endregion

        #region Delete thid Event
        public void CopyFrontLeftTORight_Click(object sender, EventArgs e)
        {


            #region Delete
            #region Changing the Sign of the x-coordinates
            //double A1x = -Convert.ToDouble(A1xFL.Text);
            //double B1x = -Convert.ToDouble(B1xFL.Text);
            //double C1x = -Convert.ToDouble(C1xFL.Text);
            //double D1x = -Convert.ToDouble(D1xFL.Text);
            //double E1x = -Convert.ToDouble(E1xFL.Text);
            //double F1x = -Convert.ToDouble(F1xFL.Text);
            //double G1x = -Convert.ToDouble(G1xFL.Text);
            //double H1x = -Convert.ToDouble(H1xFL.Text);
            //double I1x = -Convert.ToDouble(I1xFL.Text);
            //double J1x = -Convert.ToDouble(J1xFL.Text);
            //double JO1x = -Convert.ToDouble(JO1xFL.Text);
            //double K1x = -Convert.ToDouble(K1xFL.Text);
            //double M1x = -Convert.ToDouble(M1xFL.Text);
            //double N1x = -Convert.ToDouble(N1xFL.Text);
            //double O1x = -Convert.ToDouble(O1xFL.Text);
            //double P1x = -Convert.ToDouble(P1xFL.Text);
            //double Q1x = -Convert.ToDouble(Q1xFL.Text);
            //double R1x = -Convert.ToDouble(R1xFL.Text);
            //double W1x = -Convert.ToDouble(W1xFL.Text);
            //double RideHeightRefx = -Convert.ToDouble(RideHeightRefFLx.Text);
            #endregion

            #region Copying the Front Left coordinates to Front Right

            //A1xFR.Text = Convert.ToString(A1x);
            //A1yFR.Text = A1yFL.Text;
            //A1zFR.Text = A1zFL.Text;

            //B1xFR.Text = Convert.ToString(B1x);
            //B1yFR.Text = B1yFL.Text;
            //B1zFR.Text = B1zFL.Text;

            //C1xFR.Text = Convert.ToString(C1x);
            //C1yFR.Text = C1yFL.Text;
            //C1zFR.Text = C1zFL.Text;

            //D1xFR.Text = Convert.ToString(D1x);
            //D1yFR.Text = D1yFL.Text;
            //D1zFR.Text = D1zFL.Text;

            //E1xFR.Text = Convert.ToString(E1x);
            //E1yFR.Text = E1yFL.Text;
            //E1zFR.Text = E1zFL.Text;

            //F1xFR.Text = Convert.ToString(F1x);
            //F1yFR.Text = F1yFL.Text;
            //F1zFR.Text = F1zFL.Text;

            //G1xFR.Text = Convert.ToString(G1x);
            //G1yFR.Text = G1yFL.Text;
            //G1zFR.Text = G1zFL.Text;

            //H1xFR.Text = Convert.ToString(H1x);
            //H1yFR.Text = H1yFL.Text;
            //H1zFR.Text = H1zFL.Text;

            //I1xFR.Text = Convert.ToString(I1x);
            //I1yFR.Text = I1yFL.Text;
            //I1zFR.Text = I1zFL.Text;

            //J1xFR.Text = Convert.ToString(J1x);
            //J1yFR.Text = J1yFL.Text;
            //J1zFR.Text = J1zFL.Text;

            //JO1xFR.Text = Convert.ToString(JO1x);
            //JO1yFR.Text = JO1yFL.Text;
            //JO1zFR.Text = JO1zFL.Text;

            //K1xFR.Text = Convert.ToString(K1x);
            //K1yFR.Text = K1yFL.Text;
            //K1zFR.Text = K1zFL.Text;

            //M1xFR.Text = Convert.ToString(M1x);
            //M1yFR.Text = M1yFL.Text;
            //M1zFR.Text = M1zFL.Text;

            //N1xFR.Text = Convert.ToString(N1x);
            //N1yFR.Text = N1yFL.Text;
            //N1zFR.Text = N1zFL.Text;

            //O1xFR.Text = Convert.ToString(Q1x);
            //O1yFR.Text = O1yFL.Text;
            //O1zFR.Text = O1zFL.Text;

            //P1xFR.Text = Convert.ToString(P1x);
            //P1yFR.Text = P1yFL.Text;
            //P1zFR.Text = P1zFL.Text;

            //Q1xFR.Text = Convert.ToString(Q1x);
            //Q1yFR.Text = Q1yFL.Text;
            //Q1zFR.Text = Q1zFL.Text;

            //R1xFR.Text = Convert.ToString(R1x);
            //R1yFR.Text = R1yFL.Text;
            //R1zFR.Text = R1zFL.Text;

            //W1xFR.Text = Convert.ToString(W1x);
            //W1yFR.Text = W1yFL.Text;
            //W1zFR.Text = W1zFL.Text;

            //RideHeightRefFRx.Text = Convert.ToString(RideHeightRefx);
            //RideHeightRefFRy.Text = RideHeightRefFLy.Text;
            //RideHeightRefFRz.Text = RideHeightRefFLz.Text;

            #endregion
            #endregion


        } 
        #endregion

        public void CopyFrontLeftTOFrontRight()
        {
            int index = navBarGroupSuspensionFL.SelectedLinkIndex;

            #region Copying Coordinates for Double Wishbone
            if (SuspensionCoordinatesFront.Assy_List_SCFL[index].DoubleWishboneIdentifierFront == 1)
            {
                #region Copying coordinates for Double Wishbone

                #region Copying the Longitudinal Coordinates

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[12].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[12].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[13].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[13].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[14].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[14].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[15].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[15].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[16].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[16].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[17].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[17].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[18].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[18].Field<double>(1));
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[19].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[19].Field<double>(1));
                }
                #endregion

                #region Copying the Lateral Coordinates

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[12].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[12].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[13].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[13].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[14].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[14].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[15].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[15].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[16].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[16].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[17].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[17].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[18].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[18].Field<double>(2));
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[19].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[19].Field<double>(2));
                }
                #endregion

                #region Copying the Vertical Coordinates

                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[12].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[12].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[13].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[13].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[14].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[14].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[15].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[15].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[16].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[16].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[17].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[17].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[18].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[18].Field<double>(3));
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[19].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[19].Field<double>(3));
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
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].Field<double>(1));
                #endregion

                #region Copying Lateral Coordinates
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].Field<double>(2));
                #endregion

                #region Copying Vertical Coordinates
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].Field<double>(3));
                #endregion

                #endregion
            }
            #endregion

            scfrGUI[index].SCFRDataTableGUI = SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable;

            ModifyFrontRightSuspension(true);
 
        }

        #region Delete this Event
        private void CopyRearRightTOLeft_Click(object sender, EventArgs e)
        {
            int index = navBarGroupSuspensionRR.SelectedLinkIndex;
            DialogResult result = MessageBox.Show("Please ensure you have made all changes to the coordintes." + Environment.NewLine + "Copy Coordinates?", "Copy Rear Right Coordinates to Left", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                navBarGroupSuspensionRL.ItemLinks[index].PerformClick();

                #region Changing the Sign of the x-coordinates
                double A1x = -Convert.ToDouble(A1xRR.Text);
                double B1x = -Convert.ToDouble(B1xRR.Text);
                double C1x = -Convert.ToDouble(C1xRR.Text);
                double D1x = -Convert.ToDouble(D1xRR.Text);
                double E1x = -Convert.ToDouble(E1xRR.Text);
                double F1x = -Convert.ToDouble(F1xRR.Text);
                double G1x = -Convert.ToDouble(G1xRR.Text);
                double H1x = -Convert.ToDouble(H1xRR.Text);
                double I1x = -Convert.ToDouble(I1xRR.Text);
                double J1x = -Convert.ToDouble(J1xRR.Text);
                double JO1x = -Convert.ToDouble(JO1xRR.Text);
                double K1x = -Convert.ToDouble(K1xRR.Text);
                double M1x = -Convert.ToDouble(M1xRR.Text);
                double N1x = -Convert.ToDouble(N1xRR.Text);
                double O1x = -Convert.ToDouble(O1xRR.Text);
                double P1x = -Convert.ToDouble(P1xRR.Text);
                double Q1x = -Convert.ToDouble(Q1xRR.Text);
                double R1x = -Convert.ToDouble(R1xRR.Text);
                double W1x = -Convert.ToDouble(W1xRR.Text);
                double RideHeightRefx = -Convert.ToDouble(RideHeightRefRRx.Text);
                #endregion

                #region Copying the Rear Right coordinates to Rear Left

                A1xRL.Text = Convert.ToString(A1x);
                A1yRL.Text = A1yRR.Text;
                A1zRL.Text = A1zRR.Text;

                B1xRL.Text = Convert.ToString(B1x);
                B1yRL.Text = B1yRR.Text;
                B1zRL.Text = B1zRR.Text;

                C1xRL.Text = Convert.ToString(C1x);
                C1yRL.Text = C1yRR.Text;
                C1zRL.Text = C1zRR.Text;

                D1xRL.Text = Convert.ToString(D1x);
                D1yRL.Text = D1yRR.Text;
                D1zRL.Text = D1zRR.Text;

                E1xRL.Text = Convert.ToString(E1x);
                E1yRL.Text = E1yRR.Text;
                E1zRL.Text = E1zRR.Text;

                F1xRL.Text = Convert.ToString(F1x);
                F1yRL.Text = F1yRR.Text;
                F1zRL.Text = F1zRR.Text;

                G1xRL.Text = Convert.ToString(G1x);
                G1yRL.Text = G1yRR.Text;
                G1zRL.Text = G1zRR.Text;

                H1xRL.Text = Convert.ToString(H1x);
                H1yRL.Text = H1yRR.Text;
                H1zRL.Text = H1zRR.Text;

                I1xRL.Text = Convert.ToString(I1x);
                I1yRL.Text = I1yRR.Text;
                I1zRL.Text = I1zRR.Text;

                J1xRL.Text = Convert.ToString(J1x);
                J1yRL.Text = J1yRR.Text;
                J1zRL.Text = J1zRR.Text;

                JO1xRL.Text = Convert.ToString(JO1x);
                JO1yRL.Text = JO1yRR.Text;
                JO1zRL.Text = JO1zRR.Text;

                K1xRL.Text = Convert.ToString(K1x);
                K1yRL.Text = K1yRR.Text;
                K1zRL.Text = K1zRR.Text;

                M1xRL.Text = Convert.ToString(M1x);
                M1yRL.Text = M1yRR.Text;
                M1zRL.Text = M1zRR.Text;

                N1xRL.Text = Convert.ToString(N1x);
                N1yRL.Text = N1yRR.Text;
                N1zRL.Text = N1zRR.Text;

                O1xRL.Text = Convert.ToString(O1x);
                O1yRL.Text = O1yRR.Text;
                O1zRL.Text = O1zRR.Text;

                P1xRL.Text = Convert.ToString(P1x);
                P1yRL.Text = P1yRR.Text;
                P1zRL.Text = P1zRR.Text;

                Q1xRL.Text = Convert.ToString(Q1x);
                Q1yRL.Text = Q1yRR.Text;
                Q1zRL.Text = Q1zRR.Text;

                R1xRL.Text = Convert.ToString(R1x);
                R1yRL.Text = R1yRR.Text;
                R1zRL.Text = R1zRR.Text;

                W1xRL.Text = Convert.ToString(W1x);
                W1yRL.Text = W1yRR.Text;
                W1zRL.Text = W1zRR.Text;

                RideHeightRefRLx.Text = Convert.ToString(RideHeightRefx);
                RideHeightRefRLy.Text = RideHeightRefRRy.Text;
                RideHeightRefRLz.Text = RideHeightRefRRz.Text;

                #endregion

                SCRLTextBox_Leave(sender, e);

            }

            else if (result == DialogResult.Cancel)
            {

            }
        } 
        #endregion

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

            ModifyRearLeftSuspension(true);
 
        }

        #region Delete this Event
        private void CopyRearLeftTORight_Click(object sender, EventArgs e)
        {
            int index = navBarGroupSuspensionRL.SelectedLinkIndex;
            DialogResult result = MessageBox.Show("Please ensure you have made all changes to the coordintes." + Environment.NewLine + "Copy Coordinates?", "Copy Rear Left Coordinates to Right", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                navBarGroupSuspensionRR.ItemLinks[index].PerformClick();

                #region Changing the Sign of the x-coordinates
                double A1x = -Convert.ToDouble(A1xRL.Text);
                double B1x = -Convert.ToDouble(B1xRL.Text);
                double C1x = -Convert.ToDouble(C1xRL.Text);
                double D1x = -Convert.ToDouble(D1xRL.Text);
                double E1x = -Convert.ToDouble(E1xRL.Text);
                double F1x = -Convert.ToDouble(F1xRL.Text);
                double G1x = -Convert.ToDouble(G1xRL.Text);
                double H1x = -Convert.ToDouble(H1xRL.Text);
                double I1x = -Convert.ToDouble(I1xRL.Text);
                double J1x = -Convert.ToDouble(J1xRL.Text);
                double JO1x = -Convert.ToDouble(JO1xRL.Text);
                double K1x = -Convert.ToDouble(K1xRL.Text);
                double M1x = -Convert.ToDouble(M1xRL.Text);
                double N1x = -Convert.ToDouble(N1xRL.Text);
                double O1x = -Convert.ToDouble(O1xRL.Text);
                double P1x = -Convert.ToDouble(P1xRL.Text);
                double Q1x = -Convert.ToDouble(Q1xRL.Text);
                double R1x = -Convert.ToDouble(R1xRL.Text);
                double W1x = -Convert.ToDouble(W1xRL.Text);
                double RideHeightRefx = -Convert.ToDouble(RideHeightRefRLx.Text);
                #endregion

                #region Copying the Rear Left coordinates to Rear Right

                A1xRR.Text = Convert.ToString(A1x);
                A1yRR.Text = A1yRL.Text;
                A1zRR.Text = A1zRL.Text;

                B1xRR.Text = Convert.ToString(B1x);
                B1yRR.Text = B1yRL.Text;
                B1zRR.Text = B1zRL.Text;

                C1xRR.Text = Convert.ToString(C1x);
                C1yRR.Text = C1yRL.Text;
                C1zRR.Text = C1zRL.Text;

                D1xRR.Text = Convert.ToString(D1x);
                D1yRR.Text = D1yRL.Text;
                D1zRR.Text = D1zRL.Text;

                E1xRR.Text = Convert.ToString(E1x);
                E1yRR.Text = E1yRL.Text;
                E1zRR.Text = E1zRL.Text;

                F1xRR.Text = Convert.ToString(F1x);
                F1yRR.Text = F1yRL.Text;
                F1zRR.Text = F1zRL.Text;

                G1xRR.Text = Convert.ToString(G1x);
                G1yRR.Text = G1yRL.Text;
                G1zRR.Text = G1zRL.Text;

                H1xRR.Text = Convert.ToString(H1x);
                H1yRR.Text = H1yRL.Text;
                H1zRR.Text = H1zRL.Text;

                I1xRR.Text = Convert.ToString(I1x);
                I1yRR.Text = I1yRL.Text;
                I1zRR.Text = I1zRL.Text;

                J1xRR.Text = Convert.ToString(J1x);
                J1yRR.Text = J1yRL.Text;
                J1zRR.Text = J1zRL.Text;

                JO1xRR.Text = Convert.ToString(JO1x);
                JO1yRR.Text = JO1yRL.Text;
                JO1zRR.Text = JO1zRL.Text;

                K1xRR.Text = Convert.ToString(K1x);
                K1yRR.Text = K1yRL.Text;
                K1zRR.Text = K1zRL.Text;

                M1xRR.Text = Convert.ToString(M1x);
                M1yRR.Text = M1yRL.Text;
                M1zRR.Text = M1zRL.Text;

                N1xRR.Text = Convert.ToString(N1x);
                N1yRR.Text = N1yRL.Text;
                N1zRR.Text = N1zRL.Text;

                O1xRR.Text = Convert.ToString(Q1x);
                O1yRR.Text = O1yRL.Text;
                O1zRR.Text = O1zRL.Text;

                P1xRR.Text = Convert.ToString(P1x);
                P1yRR.Text = P1yRL.Text;
                P1zRR.Text = P1zRL.Text;

                Q1xRR.Text = Convert.ToString(Q1x);
                Q1yRR.Text = Q1yRL.Text;
                Q1zRR.Text = Q1zRL.Text;

                R1xRR.Text = Convert.ToString(R1x);
                R1yRR.Text = R1yRL.Text;
                R1zRR.Text = R1zRL.Text;

                W1xRR.Text = Convert.ToString(W1x);
                W1yRR.Text = W1yRL.Text;
                W1zRR.Text = W1zRL.Text;

                RideHeightRefRRx.Text = Convert.ToString(RideHeightRefx);
                RideHeightRefRRy.Text = RideHeightRefRLy.Text;
                RideHeightRefRRz.Text = RideHeightRefRLz.Text;

                #endregion


                SCRRTextBox_Leave(sender, e);
            }

            else if (result == DialogResult.Cancel)
            {

            }
        } 
        #endregion

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

            ModifyRearRightSuspension(true);
        }

        #region Delete this Event
        private void CopyFrontRightTOLeft_Click_1(object sender, EventArgs e)
        {
            int index = navBarGroupSuspensionFR.SelectedLinkIndex;
            DialogResult result = MessageBox.Show("Please ensure you have made all changes to the coordintes." + Environment.NewLine + "Copy Coordinates?", "Copy Front Right Coordinates to Left", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                navBarGroupSuspensionFL.ItemLinks[index].PerformClick();

                #region Changing the Sign of the x-coordinates
                double A1x = -Convert.ToDouble(A1xFR.Text);
                double B1x = -Convert.ToDouble(B1xFR.Text);
                double C1x = -Convert.ToDouble(C1xFR.Text);
                double D1x = -Convert.ToDouble(D1xFR.Text);
                double E1x = -Convert.ToDouble(E1xFR.Text);
                double F1x = -Convert.ToDouble(F1xFR.Text);
                double G1x = -Convert.ToDouble(G1xFR.Text);
                double H1x = -Convert.ToDouble(H1xFR.Text);
                double I1x = -Convert.ToDouble(I1xFR.Text);
                double J1x = -Convert.ToDouble(J1xFR.Text);
                double JO1x = -Convert.ToDouble(JO1xFR.Text);
                double K1x = -Convert.ToDouble(K1xFR.Text);
                double M1x = -Convert.ToDouble(M1xFR.Text);
                double N1x = -Convert.ToDouble(N1xFR.Text);
                double O1x = -Convert.ToDouble(O1xFR.Text);
                double P1x = -Convert.ToDouble(P1xFR.Text);
                double Q1x = -Convert.ToDouble(Q1xFR.Text);
                double R1x = -Convert.ToDouble(R1xFR.Text);
                double W1x = -Convert.ToDouble(W1xFR.Text);
                double RideHeightRefx = -Convert.ToDouble(RideHeightRefFRx.Text);
                #endregion

                #region Copying the Front Right coordinates to Front Left

                A1xFL.Text = Convert.ToString(A1x);
                A1yFL.Text = A1yFR.Text;
                A1zFL.Text = A1zFR.Text;

                B1xFL.Text = Convert.ToString(B1x);
                B1yFL.Text = B1yFR.Text;
                B1zFL.Text = B1zFR.Text;

                C1xFL.Text = Convert.ToString(C1x);
                C1yFL.Text = C1yFR.Text;
                C1zFL.Text = C1zFR.Text;

                D1xFL.Text = Convert.ToString(D1x);
                D1yFL.Text = D1yFR.Text;
                D1zFL.Text = D1zFR.Text;

                E1xFL.Text = Convert.ToString(E1x);
                E1yFL.Text = E1yFR.Text;
                E1zFL.Text = E1zFR.Text;

                F1xFL.Text = Convert.ToString(F1x);
                F1yFL.Text = F1yFR.Text;
                F1zFL.Text = F1zFR.Text;

                G1xFL.Text = Convert.ToString(G1x);
                G1yFL.Text = G1yFR.Text;
                G1zFL.Text = G1zFR.Text;

                H1xFL.Text = Convert.ToString(H1x);
                H1yFL.Text = H1yFR.Text;
                H1zFL.Text = H1zFR.Text;

                I1xFL.Text = Convert.ToString(I1x);
                I1yFL.Text = I1yFR.Text;
                I1zFL.Text = I1zFR.Text;

                J1xFL.Text = Convert.ToString(J1x);
                J1yFL.Text = J1yFR.Text;
                J1zFL.Text = J1zFR.Text;

                JO1xFL.Text = Convert.ToString(JO1x);
                JO1yFL.Text = JO1yFR.Text;
                JO1zFL.Text = JO1zFR.Text;

                K1xFL.Text = Convert.ToString(K1x);
                K1yFL.Text = K1yFR.Text;
                K1zFL.Text = K1zFR.Text;

                M1xFL.Text = Convert.ToString(M1x);
                M1yFL.Text = M1yFR.Text;
                M1zFL.Text = M1zFR.Text;

                N1xFL.Text = Convert.ToString(N1x);
                N1yFL.Text = N1yFR.Text;
                N1zFL.Text = N1zFR.Text;

                O1xFL.Text = Convert.ToString(O1x);
                O1yFL.Text = O1yFR.Text;
                O1zFL.Text = O1zFR.Text;

                P1xFL.Text = Convert.ToString(P1x);
                P1yFL.Text = P1yFR.Text;
                P1zFL.Text = P1zFR.Text;

                Q1xFL.Text = Convert.ToString(Q1x);
                Q1yFL.Text = Q1yFR.Text;
                Q1zFL.Text = Q1zFR.Text;

                R1xFL.Text = Convert.ToString(R1x);
                R1yFL.Text = R1yFR.Text;
                R1zFL.Text = R1zFR.Text;

                W1xFL.Text = Convert.ToString(W1x);
                W1yFL.Text = W1yFR.Text;
                W1zFL.Text = W1zFR.Text;

                RideHeightRefFLx.Text = Convert.ToString(RideHeightRefx);
                RideHeightRefFLy.Text = RideHeightRefFRy.Text;
                RideHeightRefFLz.Text = RideHeightRefFRz.Text;

                #endregion


                //SCFLTextBox_Leave(sender, e);
            }

            else if (result == DialogResult.Cancel)
            {

            }
        } 
        #endregion

        public void CopyFrontRightTOFrontLeft()
        {
            int index = navBarGroupSuspensionFR.SelectedLinkIndex;

            #region Copying Coordinates for Double Wishbone
            if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].DoubleWishboneIdentifierFront == 1)
            {
                #region Copying coordinates for Double Wishbone

                #region Copying the Longitudinal Coordinates

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[12].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[12].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[13].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[13].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[14].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[14].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[15].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[15].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[16].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[16].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[17].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[17].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[18].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[18].Field<double>(1));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[19].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[19].Field<double>(1));
                }
                #endregion

                #region Copying the Lateral Coordinates

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[12].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[12].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[13].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[13].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[14].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[14].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[15].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[15].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[16].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[16].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[17].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[17].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[18].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[18].Field<double>(2));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[19].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[19].Field<double>(2));
                }
                #endregion

                #region Copying the Vertical Coordinates

                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[12].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[12].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[13].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[13].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[14].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[14].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[15].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[15].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[16].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[16].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[17].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[17].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[18].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[18].Field<double>(3));
                if (SuspensionCoordinatesFront.Assy_List_SCFL[index].TARBIdentifierFront == 1)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[19].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[19].Field<double>(3));
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
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].Field<double>(1));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].SetField<double>("X (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].Field<double>(1));
                #endregion

                #region Copying Lateral Coordinates
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].Field<double>(2));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].SetField<double>("Y (mm)", -SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].Field<double>(2));
                #endregion

                #region Copying Vertical Coordinates
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[0].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[0].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[1].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[1].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[2].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[2].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[3].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[3].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[4].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[4].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[5].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[5].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[6].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[6].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[7].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[7].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[8].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[8].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[9].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[9].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[10].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[10].Field<double>(3));
                SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable.Rows[11].SetField<double>("Z (mm)", SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRDataTable.Rows[11].Field<double>(3));
                #endregion

                #endregion
            }
            #endregion

            scflGUI[index].SCFLDataTableGUI = SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLDataTable;

            ModifyFrontLeftSuspension(true);

        }

        #endregion

        #region Suspension Types Formn Invoking
        private void barButtonSuspensionTypes_ItemClick(object sender, ItemClickEventArgs e)
        {
            SuspensionType S1 = new SuspensionType(this);
            S1.Reset();
            S1.Show();
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

                accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsFRTorsionBarBottom.Visible = false;

                #region Bringing out the changes in UI and values if the Suspension Type is Double Wishbone with Pushrod
                A1xFL.Show(); A1yFL.Show(); A1zFL.Show(); A1xFR.Show(); A1yFR.Show(); A1zFR.Show(); A2xFL.Show(); A2yFL.Show(); A2zFL.Show(); A2xFR.Show(); A2yFR.Show(); A2zFR.Show();
                B1xFL.Show(); B1yFL.Show(); B1zFL.Show(); B1xFR.Show(); B1yFR.Show(); B1zFR.Show(); B2xFL.Show(); B2yFL.Show(); B2zFL.Show(); B2xFR.Show(); B2yFR.Show(); B2zFR.Show();
                I1xFL.Show(); I1yFL.Show(); I1zFL.Show(); I1xFR.Show(); I1yFR.Show(); I1zFR.Show(); I2xFL.Show(); I2yFL.Show(); I2zFL.Show(); I2xFR.Show(); I2yFR.Show(); I2zFR.Show();
                H1xFL.Show(); H1yFL.Show(); H1zFL.Show(); H1xFR.Show(); H1yFR.Show(); H1zFR.Show(); H2xFL.Show(); H2yFL.Show(); H2zFL.Show(); H2xFR.Show(); H2yFR.Show(); H2zFR.Show();
                G1xFL.Show(); G1yFL.Show(); G1zFL.Show(); G1xFR.Show(); G1yFR.Show(); G1zFR.Show(); G2xFL.Show(); G2yFL.Show(); G2zFL.Show(); G2xFR.Show(); G2yFR.Show(); G2zFR.Show();
                F1xFL.Show(); F1yFL.Show(); F1zFL.Show(); F1xFR.Show(); F1yFR.Show(); F1zFR.Show(); F2xFL.Show(); F2yFL.Show(); F2zFL.Show(); F2xFR.Show(); F2yFR.Show(); F2zFR.Show();
                O1xFL.Show(); O1yFL.Show(); O1zFL.Show(); O1xFR.Show(); O1yFR.Show(); O1zFR.Show(); O2xFL.Show(); O2yFL.Show(); O2zFL.Show(); O2xFR.Show(); O2yFR.Show(); O2zFR.Show();


                label562.Show(); label563.Show(); label564.Show(); label577.Show(); label578.Show(); label579.Show(); label565.Show(); label566.Show(); label577.Show(); label725.Show(); label726.Show(); label727.Show();
                label740.Show(); label741.Show(); label742.Show(); label701.Show(); label702.Show(); label703.Show(); label704.Show(); label705.Show(); label715.Show();
                label685.Show(); label686.Show(); label687.Show(); label682.Show(); label683.Show(); label684.Show(); label692.Show(); label693.Show(); label694.Show(); label872.Show(); label873.Show(); label874.Show(); label869.Show();
                label870.Show(); label871.Show(); label887.Show(); label888.Show(); label889.Show(); label884.Show(); label885.Show(); label886.Show();

                accordionControlFixedPointsFLUpperFrontChassis.Visible = true; accordionControlFixedPointsFLUpperRearChassis.Visible = true; accordionControlFixedPointsFLBellCrankPivot.Visible = true;
                accordionControlFixedPointsFRUpperFrontChassis.Visible = true; accordionControlFixedPointsFRUpperRearChassis.Visible = true; accordionControlFixedPointsFRBellCrankPivot.Visible = true;
                accordionControlMovingPointsFLPushRodBellCrank.Visible = true; accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFLPushRodUpright.Visible = true; accordionControlMovingPointsFLUpperBallJoint.Visible = true;
                accordionControlMovingPointsFRPushRodBellCrank.Visible = true; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFRPushRodUpright.Visible = true; accordionControlMovingPointsFRUpperBallJoint.Visible = true;

                label841.Text = "Damper Shock Mount";
                label7.Text = "Damper Shock Mount";
                label860.Text = "Damper Bell Crank";
                label39.Text = "Damper Bell Crank";
                accordionControlMovingPointsFLDamperBellCrank.Text = "Damper Bell Crank";
                accordionControlMovingPointsFRDamperBellCrank.Text = "Damper Bell Crank";
                label24.Show(); label23.Show(); label26.Show(); label14.Show(); label17.Show(); label15.Show(); label41.Show(); label40.Show(); label36.Show(); label31.Show(); label858.Show(); label859.Show(); label862.Show(); label867.Show();
                navigationPagePushRodFL.Caption = "Pushrod FL";
                navigationPagePushRodFR.Caption = "Pushrod FR";


                D1xFL.Text = "221.21";    D1yFL.Text = "1065.17";       D1zFL.Text = "105";   C1xFL.Text = "239.68"; C1yFL.Text = "1068.38"; C1zFL.Text = "-220.68";
                Q1xFL.Text = "284";       Q1yFL.Text = "1033";          Q1zFL.Text = "60.8";  JO1xFL.Text = "36"; JO1yFL.Text = "1572";      JO1zFL.Text = "-6.73";
                J1xFL.Text = "235.1";     J1yFL.Text = "1592.74";       J1zFL.Text = "-6.73"; E1xFL.Text = "566.57"; E1yFL.Text = "1076.04"; E1zFL.Text = "3.94";
                N1xFL.Text = "232.12";    N1yFL.Text = "1097.4";        N1zFL.Text = "60.8";
                M1xFL.Text = "586.92";    M1yFL.Text = "1126.03";       M1zFL.Text = "71.31"; K1xFL.Text = "547.99"; K1yFL.Text = "1182.49"; K1zFL.Text = "0.71";
                P1xFL.Text = "278.97";    P1yFL.Text = "1021.58";       P1zFL.Text = "-5.57"; W1xFL.Text = "621.35"; W1yFL.Text = "950.85";  W1zFL.Text = "-1.13";
                NSMCGFLx.Text = "560.26"; NSMCGFLy.Text = "1262.87";    NSMCGFLz.Text = "0";

                D1xFR.Text = "-221.21";    D1yFR.Text = "1065.17";    D1zFR.Text = "105";   C1xFR.Text = "-239.68"; C1yFR.Text = "1068.38"; C1zFR.Text = "-220.68";
                Q1xFR.Text = "-284";       Q1yFR.Text = "1033";       Q1zFR.Text = "60.8";  JO1xFR.Text = "-36";    JO1yFR.Text = "1572";   JO1zFR.Text = "-6.73";
                J1xFR.Text = "-235.1";     J1yFR.Text = "1592.74";    J1zFR.Text = "-6.73"; E1xFR.Text = "-566.57"; E1yFR.Text = "1076.04"; E1zFR.Text = "3.94";
                N1xFR.Text = "-232.12";    N1yFR.Text = "1097.4";     N1zFR.Text = "60.8";
                M1xFR.Text = "-586.92";    M1yFR.Text = "1126.03";    M1zFR.Text = "71.31"; K1xFR.Text = "-547.99"; K1yFR.Text = "1182.49"; K1zFR.Text = "0.71";
                P1xFR.Text = "-278.97";    P1yFR.Text = "1021.58";    P1zFR.Text = "-5.57"; W1xFR.Text = "-621.35"; W1yFR.Text = "950.85";  W1zFR.Text = "-1.13";
                NSMCGFRx.Text = "-560.26"; NSMCGFRy.Text = "1262.87"; NSMCGFRz.Text = "0";
                #endregion
            }
            #endregion

            #region GUI operations for Front McPherson
            else if (McPhersonFront == 1)
            {

                accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsFRTorsionBarBottom.Visible = false;

                accordionControlMovingPointsFLPushRodUpright.Visible = false;
                accordionControlMovingPointsFRPushRodUpright.Visible = false;

                if (McPhersonRear == 1)
                {
                    barButtonActuationType.Enabled = false;
                    barButtonAntiRollBarType.Enabled = false;
                }

                #region Bringing out the changes in UI and values if the Suspension type is McPherson Strut
                A1xFL.Hide(); A1yFL.Hide(); A1zFL.Hide(); A1xFR.Hide(); A1yFR.Hide(); A1zFR.Hide(); A2xFL.Hide(); A2yFL.Hide(); A2zFL.Hide(); A2xFR.Hide(); A2yFR.Hide(); A2zFR.Hide(); // All irrelevant textboxes removed
                B1xFL.Hide(); B1yFL.Hide(); B1zFL.Hide(); B1xFR.Hide(); B1yFR.Hide(); B1zFR.Hide(); B2xFL.Hide(); B2yFL.Hide(); B2zFL.Hide(); B2xFR.Hide(); B2yFR.Hide(); B2zFR.Hide();
                I1xFL.Hide(); I1yFL.Hide(); I1zFL.Hide(); I1xFR.Hide(); I1yFR.Hide(); I1zFR.Hide(); I2xFL.Hide(); I2yFL.Hide(); I2zFL.Hide(); I2xFR.Hide(); I2yFR.Hide(); I2zFR.Hide();
                H1xFL.Hide(); H1yFL.Hide(); H1zFL.Hide(); H1xFR.Hide(); H1yFR.Hide(); H1zFR.Hide(); H2xFL.Hide(); H2yFL.Hide(); H2zFL.Hide(); H2xFR.Hide(); H2yFR.Hide(); H2zFR.Hide();
                G1xFL.Hide(); G1yFL.Hide(); G1zFL.Hide(); G1xFR.Hide(); G1yFR.Hide(); G1zFR.Hide(); G2xFL.Hide(); G2yFL.Hide(); G2zFL.Hide(); G2xFR.Hide(); G2yFR.Hide(); G2zFR.Hide();
                F1xFL.Hide(); F1yFL.Hide(); F1zFL.Hide(); F1xFR.Hide(); F1yFR.Hide(); F1zFR.Hide(); F2xFL.Hide(); F2yFL.Hide(); F2zFL.Hide(); F2xFR.Hide(); F2yFR.Hide(); F2zFR.Hide();
                O1xFL.Hide(); O1yFL.Hide(); O1zFL.Hide(); O1xFR.Hide(); O1yFR.Hide(); O1zFR.Hide(); O2xFL.Hide(); O2yFL.Hide(); O2zFL.Hide(); O2xFR.Hide(); O2yFR.Hide(); O2zFR.Hide();


                label562.Hide(); label563.Hide(); label564.Hide(); label577.Hide(); label578.Hide(); label579.Hide(); label565.Hide(); label566.Hide(); label577.Hide(); label725.Hide(); label726.Hide(); label727.Hide();
                label740.Hide(); label741.Hide(); label742.Hide(); label701.Hide(); label702.Hide(); label703.Hide(); label704.Hide(); label705.Hide(); label715.Hide();
                label685.Hide(); label686.Hide(); label687.Hide(); label682.Hide(); label683.Hide(); label684.Hide(); label692.Hide(); label693.Hide(); label694.Hide(); label872.Hide(); label873.Hide(); label874.Hide(); label869.Hide();
                label870.Hide(); label871.Hide(); label887.Hide(); label888.Hide(); label889.Hide(); label884.Hide(); label885.Hide(); label886.Hide();


                accordionControlFixedPointsFLUpperFrontChassis.Visible = false; accordionControlFixedPointsFLUpperRearChassis.Visible = false; accordionControlFixedPointsFLBellCrankPivot.Visible = false; // Removing irrelevant accordion control elements
                accordionControlFixedPointsFRUpperFrontChassis.Visible = false; accordionControlFixedPointsFRUpperRearChassis.Visible = false; accordionControlFixedPointsFRBellCrankPivot.Visible = false;
                accordionControlMovingPointsFLPushRodBellCrank.Visible = false; accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFLPushRodBellCrank.Visible = false; accordionControlMovingPointsFLUpperBallJoint.Visible = false;
                accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRUpperBallJoint.Visible = false;

                label841.Text = "Damper Chassis Mount";
                label7.Text = "Damper Chassis Mount";
                label860.Text = "Upper Ball Joint";
                label39.Text = "Upper Ball Joint";
                accordionControlMovingPointsFLDamperBellCrank.Text = "Upper Ball Joint";
                accordionControlMovingPointsFRDamperBellCrank.Text = "Upper Ball Joint";
                label24.Hide(); label23.Hide(); label26.Hide(); label14.Hide(); label17.Hide(); label15.Hide(); label41.Hide(); label40.Hide(); label36.Hide(); label31.Hide(); label858.Hide(); label859.Hide(); label862.Hide(); label867.Hide();

                navigationPagePushRodFL.Caption = "Strut FL";
                navigationPagePushRodFR.Caption = "Strut FR";
                D1xFL.Text = "146.87"; D1yFL.Text = "1054.3"; D1zFL.Text = "69.72"; C1xFL.Text = "159.15"; C1yFL.Text = "1056.5"; C1zFL.Text = "-146.53";
                Q1xFL.Text = "350"; Q1yFL.Text = "1033"; Q1zFL.Text = "40.37"; JO1xFL.Text = "312.06"; JO1yFL.Text = "1417.92"; JO1zFL.Text = "-1.46";
                J1xFL.Text = "363.04"; J1yFL.Text = "1178.035"; J1zFL.Text = "-1.46"; E1xFL.Text = "376.21"; E1yFL.Text = "1061.57"; E1zFL.Text = "2.61";
                M1xFL.Text = "389.73"; M1yFL.Text = "1094.77"; M1zFL.Text = "47.35"; K1xFL.Text = "363.89"; K1yFL.Text = "1180.26"; K1zFL.Text = "0.47";
                N1xFL.Text = "154.13"; N1yFL.Text = "1075.7"; N1zFL.Text = "40.37";
                P1xFL.Text = "380.76"; P1yFL.Text = "1025.42"; P1zFL.Text = "-3.69"; W1xFL.Text = "412.6"; W1yFL.Text = "950"; W1zFL.Text = "-0.75";
                NSMCGFLx.Text = "375.8"; NSMCGFLy.Text = "1195.2"; NSMCGFLz.Text = "0.48";



                D1xFR.Text = "-146.87"; D1yFR.Text = "1054.3"; D1zFR.Text = "69.72"; C1xFR.Text = "-159.15"; C1yFR.Text = "1056.5"; C1zFR.Text = "-146.53";
                Q1xFR.Text = "-350"; Q1yFR.Text = "1033"; Q1zFR.Text = "40.37"; JO1xFR.Text = "-312.06"; JO1yFR.Text = "1417.92"; JO1zFR.Text = "-1.46";
                J1xFR.Text = "-363.04"; J1yFR.Text = "1178.035"; J1zFR.Text = "-1.46"; E1xFR.Text = "-376.21"; E1yFR.Text = "1061.57"; E1zFR.Text = "2.61";
                M1xFR.Text = "-389.73"; M1yFR.Text = "1094.77"; M1zFR.Text = "47.35"; K1xFR.Text = "-363.89"; K1yFR.Text = "1180.26"; K1zFR.Text = "0.47";
                N1xFR.Text = "-154.13"; N1yFR.Text = "1075.7"; N1zFR.Text = "40.37";
                P1xFR.Text = "-380.76"; P1yFR.Text = "1025.42"; P1zFR.Text = "-3.69"; W1xFR.Text = "-412.6"; W1yFR.Text = "950"; W1zFR.Text = "-0.75";
                NSMCGFRx.Text = "-375.8"; NSMCGFRy.Text = "1195.2"; NSMCGFRz.Text = "0.48";
                #endregion
            }
            #endregion

            #region GUI operations for Rear Double Wishbone
            if (DoubleWishboneRear == 1)
            {

                barButtonActuationType.Enabled = true;
                barButtonAntiRollBarType.Enabled = true;

                accordionControlFixedPointsRLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsRRTorsionBarBottom.Visible = false;

                #region Bringing out the changes in UI and values if the Suspension Type is Double Wishbone with Pushrod
                A1xRL.Show(); A1yRL.Show(); A1zRL.Show(); A1xRR.Show(); A1yRR.Show(); A1zRR.Show(); A2xRL.Show(); A2yRL.Show(); A2zRL.Show(); A2xRR.Show(); A2yRR.Show(); A2zRR.Show();
                B1xRL.Show(); B1yRL.Show(); B1zRL.Show(); B1xRR.Show(); B1yRR.Show(); B1zRR.Show(); B2xRL.Show(); B2yRL.Show(); B2zRL.Show(); B2xRR.Show(); B2yRR.Show(); B2zRR.Show();
                I1xRL.Show(); I1yRL.Show(); I1zRL.Show(); I1xRR.Show(); I1yRR.Show(); I1zRR.Show(); I2xRL.Show(); I2yRL.Show(); I2zRL.Show(); I2xRR.Show(); I2yRR.Show(); I2zRR.Show();
                H1xRL.Show(); H1yRL.Show(); H1zRL.Show(); H1xRR.Show(); H1yRR.Show(); H1zRR.Show(); H2xRL.Show(); H2yRL.Show(); H2zRL.Show(); H2xRR.Show(); H2yRR.Show(); H2zRR.Show();
                G1xRL.Show(); G1yRL.Show(); G1zRL.Show(); G1xRR.Show(); G1yRR.Show(); G1zRR.Show(); G2xRL.Show(); G2yRL.Show(); G2zRL.Show(); G2xRR.Show(); G2yRR.Show(); G2zRR.Show();
                F1xRL.Show(); F1yRL.Show(); F1zRL.Show(); F1xRR.Show(); F1yRR.Show(); F1zRR.Show(); F2xRL.Show(); F2yRL.Show(); F2zRL.Show(); F2xRR.Show(); F2yRR.Show(); F2zRR.Show();
                O1xRL.Show(); O1yRL.Show(); O1zRL.Show(); O1xRR.Show(); O1yRR.Show(); O1zRR.Show(); O2xRL.Show(); O2yRL.Show(); O2zRL.Show(); O2xRR.Show(); O2yRR.Show(); O2zRR.Show();


                label598.Show(); label599.Show(); label600.Show(); label586.Show(); label587.Show(); label588.Show(); label601.Show(); label602.Show(); label603.Show(); label786.Show(); label788.Show(); label793.Show();
                label803.Show(); label804.Show(); label806.Show(); label744.Show(); label745.Show(); label743.Show(); label769.Show(); label771.Show(); label773.Show();
                label613.Show(); label614.Show(); label615.Show(); label610.Show(); label611.Show(); label612.Show(); label641.Show(); label642.Show(); label643.Show(); label833.Show(); label834.Show(); label835.Show();
                label846.Show(); label847.Show(); label848.Show(); label815.Show(); label816.Show(); label817.Show(); label818.Show(); label820.Show(); label822.Show();


                accordionControlFixedPointsRLUpperFrontChassis.Visible = true; accordionControlFixedPointsRLUpperRearChassis.Visible = true; accordionControlFixedPointsRLBellCrankPivot.Visible = true;
                accordionControlFixedPointsRRUpperFrontChassis.Visible = true; accordionControlFixedPointsRRUpperRearChassis.Visible = true; accordionControlFixedPointsRRBellCrankPivot.Visible = true;
                accordionControlMovingPointsRLPushRodBellCrank.Visible = true; accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRLPushRodBellCrank.Visible = true; accordionControlMovingPointsRLUpperBallJoint.Visible = true;
                accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRUpperBallJoint.Visible = true;

                label25.Text = "Damper Shock Mount";
                label7.Text = "Damper Shock Mount";
                label51.Text = "Damper Bell Crank";
                label63.Text = "Damper Bell Crank";
                accordionControlMovingPointsRLDamperBellCrank.Text = "Damper Bell Crank";
                accordionControlMovingPointsRRDamperBellCrank.Text = "Damper Bell Crank";
                label831.Show(); label821.Show(); label856.Show(); label53.Show(); label52.Show(); label48.Show(); label43.Show(); label65.Show(); label64.Show(); label60.Show(); label55.Show();
                navigationPagePushRodRL.Caption = "Pushrod RL";
                navigationPagePushRodRR.Caption = "Pushrod RR";


                D1xRL.Text = "225.38"; D1yRL.Text = "1087"; D1zRL.Text = "-1050"; C1xRL.Text = "190.8"; C1yRL.Text = "1081.65"; C1zRL.Text = "-1460";
                Q1xRL.Text = "265"; Q1yRL.Text = "1068"; Q1zRL.Text = "-1548.13"; JO1xRL.Text = "18"; JO1yRL.Text = "1367"; JO1zRL.Text = "-1505";
                J1xRL.Text = "216.65"; J1yRL.Text = "1389.98"; J1zRL.Text = "-1505"; E1xRL.Text = "474.3"; E1yRL.Text = "1076.93"; E1zRL.Text = "-1526.35";
                M1xRL.Text = "487.55"; M1yRL.Text = "1075.9"; M1zRL.Text = "-1603.84"; K1xRL.Text = "498.75"; K1yRL.Text = "1180.5"; K1zRL.Text = "-1551";
                N1xRL.Text = "185.8"; N1yRL.Text = "1081.65"; N1zRL.Text = "-1500";
                P1xRL.Text = "260.21"; P1yRL.Text = "1055.57"; P1zRL.Text = "-1507.11"; W1xRL.Text = "573.14"; W1yRL.Text = "949.2"; W1zRL.Text = "-1548.21";
                NSMCGRLx.Text = "520.26"; NSMCGRLy.Text = "1262.87"; NSMCGRLz.Text = "-1550";


                D1xRR.Text = "-225.38"; D1yRR.Text = "1087"; D1zRR.Text = "-1050"; C1xRR.Text = "-190.8"; C1yRR.Text = "1081.65"; C1zRR.Text = "-1460";
                Q1xRR.Text = "-265"; Q1yRR.Text = "1068"; Q1zRR.Text = "-1548.13"; JO1xRR.Text = "-18"; JO1yRR.Text = "1367"; JO1zRR.Text = "-1505";
                J1xRR.Text = "-216.65"; J1yRR.Text = "1389.98"; J1zRR.Text = "-1505"; E1xRR.Text = "-474.3"; E1yRR.Text = "1076.93"; E1zRR.Text = "-1526.35";
                M1xRR.Text = "-487.55"; M1yRR.Text = "1075.9"; M1zRR.Text = "-1603.84"; K1xRR.Text = "-498.75"; K1yRR.Text = "1180.5"; K1zRR.Text = "-1551";
                N1xRR.Text = "-185.8"; N1yRR.Text = "1081.65"; N1zRR.Text = "-1500";
                P1xRR.Text = "-260.21"; P1yRR.Text = "1055.57"; P1zRR.Text = "-1507.11"; W1xRR.Text = "-573.14"; W1yRR.Text = "949.2"; W1zRR.Text = "-1548.21";
                NSMCGRRx.Text = "-520.26"; NSMCGRRy.Text = "1262.87"; NSMCGRRz.Text = "-1550";
                #endregion
            }
            #endregion

            #region GUI operations for Rear McPherson Strut
            else if (McPhersonRear == 1)
            {

                accordionControlFixedPointsRLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsRRTorsionBarBottom.Visible = false;

                accordionControlMovingPointsRRPushRodUpright.Visible = false;
                accordionControlMovingPointsRLPushRodUpright.Visible = false;


                if (McPhersonFront == 1)
                {
                    barButtonActuationType.Enabled = false;
                    barButtonAntiRollBarType.Enabled = false;
                }

                #region Bringing out the changes in UI and values if the Suspension type is McPherson Strut
                A1xRL.Hide(); A1yRL.Hide(); A1zRL.Hide(); A1xRR.Hide(); A1yRR.Hide(); A1zRR.Hide(); A2xRL.Hide(); A2yRL.Hide(); A2zRL.Hide(); A2xRR.Hide(); A2yRR.Hide(); A2zRR.Hide();
                B1xRL.Hide(); B1yRL.Hide(); B1zRL.Hide(); B1xRR.Hide(); B1yRR.Hide(); B1zRR.Hide(); B2xRL.Hide(); B2yRL.Hide(); B2zRL.Hide(); B2xRR.Hide(); B2yRR.Hide(); B2zRR.Hide();
                I1xRL.Hide(); I1yRL.Hide(); I1zRL.Hide(); I1xRR.Hide(); I1yRR.Hide(); I1zRR.Hide(); I2xRL.Hide(); I2yRL.Hide(); I2zRL.Hide(); I2xRR.Hide(); I2yRR.Hide(); I2zRR.Hide();
                H1xRL.Hide(); H1yRL.Hide(); H1zRL.Hide(); H1xRR.Hide(); H1yRR.Hide(); H1zRR.Hide(); H2xRL.Hide(); H2yRL.Hide(); H2zRL.Hide(); H2xRR.Hide(); H2yRR.Hide(); H2zRR.Hide();
                G1xRL.Hide(); G1yRL.Hide(); G1zRL.Hide(); G1xRR.Hide(); G1yRR.Hide(); G1zRR.Hide(); G2xRL.Hide(); G2yRL.Hide(); G2zRL.Hide(); G2xRR.Hide(); G2yRR.Hide(); G2zRR.Hide();
                F1xRL.Hide(); F1yRL.Hide(); F1zRL.Hide(); F1xRR.Hide(); F1yRR.Hide(); F1zRR.Hide(); F2xRL.Hide(); F2yRL.Hide(); F2zRL.Hide(); F2xRR.Hide(); F2yRR.Hide(); F2zRR.Hide();
                O1xRL.Hide(); O1yRL.Hide(); O1zRL.Hide(); O1xRR.Hide(); O1yRR.Hide(); O1zRR.Hide(); O2xRL.Hide(); O2yRL.Hide(); O2zRL.Hide(); O2xRR.Hide(); O2yRR.Hide(); O2zRR.Hide();


                label598.Hide(); label599.Hide(); label600.Hide(); label586.Hide(); label587.Hide(); label588.Hide(); label601.Hide(); label602.Hide(); label603.Hide(); label786.Hide(); label788.Hide(); label793.Hide();
                label803.Hide(); label804.Hide(); label806.Hide(); label744.Hide(); label745.Hide(); label743.Hide(); label769.Hide(); label771.Hide(); label773.Hide();
                label613.Hide(); label614.Hide(); label615.Hide(); label610.Hide(); label611.Hide(); label612.Hide(); label641.Hide(); label642.Hide(); label643.Hide(); label833.Hide(); label834.Hide(); label835.Hide();
                label846.Hide(); label847.Hide(); label848.Hide(); label815.Hide(); label816.Hide(); label817.Hide(); label818.Hide(); label820.Hide(); label822.Hide();


                accordionControlFixedPointsRLUpperFrontChassis.Visible = false; accordionControlFixedPointsRLUpperRearChassis.Visible = false; accordionControlFixedPointsRLBellCrankPivot.Visible = false;
                accordionControlFixedPointsRRUpperFrontChassis.Visible = false; accordionControlFixedPointsRRUpperRearChassis.Visible = false; accordionControlFixedPointsRRBellCrankPivot.Visible = false;
                accordionControlMovingPointsRLPushRodBellCrank.Visible = false; accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRLPushRodBellCrank.Visible = false; accordionControlMovingPointsRLUpperBallJoint.Visible = false;
                accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRUpperBallJoint.Visible = false;

                label25.Text = "Damper Chassis Mount";
                label7.Text = "Damper Chassis Mount";
                label51.Text = "Upper Ball Joint";
                label63.Text = "Upper Ball Joint";
                accordionControlMovingPointsRLDamperBellCrank.Text = "Upper Ball Joint";
                accordionControlMovingPointsRRDamperBellCrank.Text = "Upper Ball Joint";
                label831.Hide(); label821.Hide(); label856.Hide(); label53.Hide(); label52.Hide(); label48.Hide(); label43.Hide(); label65.Hide(); label64.Hide(); label60.Hide(); label55.Hide();
                navigationPagePushRodRL.Caption = "Stut RL";
                navigationPagePushRodRR.Caption = "Strut RR";

                D1xRL.Text = "149.65"; D1yRL.Text = "1068.9"; D1zRL.Text = "-697.2"; C1xRL.Text = "126.7"; C1yRL.Text = "1065.3"; C1zRL.Text = "-969.47";
                Q1xRL.Text = "315.06"; Q1yRL.Text = "1056.2"; Q1zRL.Text = "-1027.99"; JO1xRL.Text = "246.58"; JO1yRL.Text = "1389.37"; JO1zRL.Text = "-1035.72";
                J1xRL.Text = "307.22"; J1yRL.Text = "1177.89"; J1zRL.Text = "-1035.72"; E1xRL.Text = "314.95"; E1yRL.Text = "1062.17"; E1zRL.Text = "-1035.72";
                N1xRL.Text = "123.37"; N1yRL.Text = "1065.3"; N1zRL.Text = "-996.03";
                M1xRL.Text = "323.74"; M1yRL.Text = "1061.53"; M1zRL.Text = "-1064.98"; K1xRL.Text = "331.18"; K1yRL.Text = "1180.26"; K1zRL.Text = "-1029.89";
                P1xRL.Text = "337"; P1yRL.Text = "1047.98"; P1zRL.Text = "-1000.75"; W1xRL.Text = "573.14"; W1yRL.Text = "950"; W1zRL.Text = "-1028.59";
                NSMCGRLx.Text = "345.2"; NSMCGRLy.Text = "1192.26"; NSMCGRLz.Text = "-1029.89";

                D1xRR.Text = "-149.65"; D1yRR.Text = "1068.9"; D1zRR.Text = "-697.2"; C1xRR.Text = "-126.7"; C1yRR.Text = "1065.3"; C1zRR.Text = "-969.47";
                Q1xRR.Text = "-315.06"; Q1yRR.Text = "1056.2"; Q1zRR.Text = "-1027.99"; JO1xRR.Text = "-246.58"; JO1yRR.Text = "1389.37"; JO1zRR.Text = "-1035.72";
                J1xRR.Text = "-307.22"; J1yRR.Text = "1177.89"; J1zRR.Text = "-1035.72"; E1xRR.Text = "-314.95"; E1yRR.Text = "1062.17"; E1zRR.Text = "-1035.72";
                N1xRR.Text = "-123.37"; N1yRR.Text = "1065.3"; N1zRR.Text = "-996.03";
                M1xRR.Text = "-323.74"; M1yRR.Text = "1061.53"; M1zRR.Text = "-1064.98"; K1xRR.Text = "-331.18"; K1yRR.Text = "1180.26"; K1zRR.Text = "-1029.89";
                P1xRR.Text = "-337"; P1yRR.Text = "1047.98"; P1zRR.Text = "-1000.75"; W1xRR.Text = "-573.14"; W1yRR.Text = "950"; W1zRR.Text = "-1028.59";
                NSMCGRRx.Text = "-345.2"; NSMCGRRy.Text = "1192.26"; NSMCGRLz.Text = "-1029.89";
                #endregion
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


            #region GUI operations for Front Pushrod
            if (PushrodFront == 1)
            {
                label41.Text = "Push Rod";
                label858.Text = "Push Rod";
                label862.Text = "Push Rod Bell-Crank";
                label36.Text = "Push Rod Bell-Crank";
                label1114.Text = "Pushrod";
                label123.Text = "Pushrod";
                navigationPagePushRodFL.Caption = "Pushrod FL";
                navigationPagePushRodFR.Caption = "Pushrod FR";

                #region Changing the coordinates for Pushrod
                I1yFL.Text = "1530";
                JO1yFL.Text = "1572";
                J1yFL.Text = "1592.74";
                H1xFL.Text = "298.55";
                H1yFL.Text = "1557.22";
                O1yFL.Text = "1504.96";
                G1yFL.Text = "1265";

                I1yFR.Text = "1530";
                JO1yFR.Text = "1572";
                J1yFR.Text = "1592.74";
                H1xFR.Text = "-298.55";
                H1yFR.Text = "1557.22";
                O1yFR.Text = "1504.96";
                G1yFR.Text = "1265";
                #endregion
            }
            #endregion

            #region GUI operations for Front Pullrod
            else if (PullrodFront == 1)
            {
                label41.Text = "Pull Rod";
                label858.Text = "Pull Rod";
                label862.Text = "Pull Rod Bell-Crank";
                label36.Text = "Pull Rod Bell-Crank";
                label1114.Text = "Pullrod";
                label123.Text = "Pullrod";
                navigationPagePushRodFL.Caption = "Pullrod FL";
                navigationPagePushRodFR.Caption = "Pullrod FR";

                #region Coordinates for Front Pullrod
                I1yFL.Text = "1063";
                JO1yFL.Text = "1105";
                J1yFL.Text = "1125.4";
                H1xFL.Text = "310";
                H1yFL.Text = "1073";
                O1yFL.Text = "1037.96";
                G1yFL.Text = "1233";

                I1yFR.Text = "1063";
                JO1yFR.Text = "1105";
                J1yFR.Text = "1125.4";
                H1xFR.Text = "-310";
                H1yFR.Text = "1073";
                O1yFR.Text = "1037.96";
                G1yFR.Text = "1233";
                #endregion
            }
            #endregion

            #region GUI operations for Rear Pushrod
            if (PushrodRear == 1)
            {
                label53.Text = "Push Rod";
                label65.Text = "Push Rod";
                label48.Text = "Push Rod Bell-Crank";
                label60.Text = "Push Rod Bell-Crank";
                label1157.Text = "Pushrod";
                label1137.Text = "Pushrod";
                navigationPagePushRodRL.Caption = "Pushrod RL";
                navigationPagePushRodRR.Caption = "Pushrod RR";

                #region Chanding coordinates for Rear Pushrod
                I1yRL.Text = "1328.14";
                JO1yRL.Text = "1367";
                J1yRL.Text = "1389.98";
                H1yRL.Text = "1349.01";
                O1yRL.Text = "1302.71";
                G1yRL.Text = "1094.39";

                I1yRR.Text = "1328.14";
                JO1yRR.Text = "1367";
                J1yRR.Text = "1389.98";
                H1yRR.Text = "1349.01";
                O1yRR.Text = "1302.71";
                G1yRR.Text = "1094.39";
                #endregion
            }
            #endregion

            #region GUI operations for Rear Pullrod
            else if (PullrodRear == 1)
            {
                label53.Text = "Pull Rod";
                label65.Text = "Pull Rod";
                label48.Text = "Pull Rod Bell-Crank";
                label60.Text = "Pull Rod Bell-Crank";
                label1157.Text = "Pullrod";
                label1137.Text = "Pullrod";
                navigationPagePushRodRL.Caption = "Pullrod RL";
                navigationPagePushRodRR.Caption = "Pullrod RR";

                #region Changin coordinates for Rear Pullrod
                I1yRL.Text = "1063";
                JO1yRL.Text = "1105";
                J1yRL.Text = "1125.4";
                H1yRL.Text = "1073";
                O1yRL.Text = "1037.96";
                G1yRL.Text = "1233";

                I1yRR.Text = "1063";
                JO1yRR.Text = "1105";
                J1yRR.Text = "1125.4";
                H1yRR.Text = "1073";
                O1yRR.Text = "1037.96";
                G1yRR.Text = "1233";
                #endregion
            }
            #endregion
        }

        #endregion


        #region Type of Anti-Roll Bar

        public int UARBFront_VehicleGUI, TARBFront_VehicleGUI, UARBRear_VehicleGUI, TARBRear_VehicleGUI;
        public void AntiRollBarType(int UARBFront, int TARBFront, int UARBRear, int TARBRear)
        {
            UARBFront_VehicleGUI = 0; TARBFront_VehicleGUI = 0; UARBRear_VehicleGUI = 0; TARBRear_VehicleGUI = 0;

            UARBFront_VehicleGUI = UARBFront; TARBFront_VehicleGUI = TARBFront;
            UARBRear_VehicleGUI = UARBRear; TARBRear_VehicleGUI = TARBRear;

            #region GUI operations for Front U-ARB
            if (UARBFront == 1)
            {
                #region Hiding the FRONT Torsion Bar Bottom Point
                accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsFRTorsionBarBottom.Visible = false;
                #endregion

                if (PullrodFront_VehicleGUI == 0 && PushrodFront_VehicleGUI == 1)
                {
                    #region Changing the FRONT Textboxes for PUSHROD and  to reflect a Bell Crank whos Axis is Parallel to Z axis and Perpendicular to Y and X axes (Default Bell CranK)
                    Q1xFL.Text = "284";
                    Q1yFL.Text = "1033";
                    Q1zFL.Text = "60.80";
                    JO1xFL.Text = "36";
                    JO1yFL.Text = "1572";
                    JO1zFL.Text = "-6.73";
                    P1xFL.Text = "278.97";
                    P1yFL.Text = "1021.58";
                    P1zFL.Text = "-5.57";
                    J1xFL.Text = "235.10";
                    J1yFL.Text = "1592.74";
                    J1zFL.Text = "-6.73";
                    O1xFL.Text = "277.03";
                    O1yFL.Text = "1504.96";
                    O1zFL.Text = "-6.73";
                    H1xFL.Text = "298.55";
                    H1yFL.Text = "1557.22";
                    H1zFL.Text = "-6.73";

                    Q1xFR.Text = "-284";
                    Q1yFR.Text = "1033";
                    Q1zFR.Text = "60.80";
                    JO1xFR.Text = "-36";
                    JO1yFR.Text = "1572";
                    JO1zFR.Text = "-6.73";
                    P1xFR.Text = "-278.97";
                    P1yFR.Text = "1021.58";
                    P1zFR.Text = "-5.57";
                    J1xFR.Text = "-235.10";
                    J1yFR.Text = "1592.74";
                    J1zFR.Text = "-6.73";
                    O1xFR.Text = "-277.03";
                    O1yFR.Text = "1504.96";
                    O1zFR.Text = "-6.73";
                    H1xFR.Text = "-298.55";
                    H1yFR.Text = "1557.22";
                    H1zFR.Text = "-6.73";

                    #endregion
                }

                else if (PullrodFront_VehicleGUI == 1 && PushrodFront_VehicleGUI == 0)
                {
                    #region Changing the FRONT Textboxes for PULLROD and  to reflect a Bell Crank whos Axis is Parallel to Z axis and Perpendicular to Y and X axes (Default Bell CranK)
                    Q1xFL.Text = "284";
                    Q1yFL.Text = "1033";
                    Q1zFL.Text = "60.80";
                    JO1xFL.Text = "36";
                    JO1yFL.Text = "1105";
                    JO1zFL.Text = "-6.73";
                    P1xFL.Text = "278.97";
                    P1yFL.Text = "1021.58";
                    P1zFL.Text = "-5.57";
                    J1xFL.Text = "235.10";
                    J1yFL.Text = "1125.4";
                    J1zFL.Text = "-6.73";
                    O1xFL.Text = "277.03";
                    O1yFL.Text = "1037.96";
                    O1zFL.Text = "-6.73";
                    H1xFL.Text = "298.55";
                    H1yFL.Text = "1073";
                    H1zFL.Text = "-6.73";

                    Q1xFR.Text = "-284";
                    Q1yFR.Text = "1033";
                    Q1zFR.Text = "60.80";
                    JO1xFR.Text = "-36";
                    JO1yFR.Text = "1105";
                    JO1zFR.Text = "-6.73";
                    P1xFR.Text = "-278.97";
                    P1yFR.Text = "1021.58";
                    P1zFR.Text = "-5.57";
                    J1xFR.Text = "-235.10";
                    J1yFR.Text = "1125.4";
                    J1zFR.Text = "-6.73";
                    O1xFR.Text = "-277.03";
                    O1yFR.Text = "1037.96";
                    O1zFR.Text = "-6.73";
                    H1xFR.Text = "-298.55";
                    H1yFR.Text = "1073";
                    H1zFR.Text = "-6.73";

                    #endregion
                }
            }
            #endregion

            #region GUI operations for Front T-ARB
            else if (TARBFront == 1)
            {
                #region Displaying the FRONT Torsion Bar Bottom Point
                accordionControlFixedPointsFLTorsionBarBottom.Visible = true;
                accordionControlFixedPointsFRTorsionBarBottom.Visible = true;
                #endregion

                if (PullrodFront_VehicleGUI == 0 && PushrodFront_VehicleGUI == 1)
                {
                    #region Changing the values of the FRONT Textboxes for PUSHROD and to reflect a Bell Crank whose Axis is at an angle with respect to XYZ axes
                    Q1xFL.Text = "0";
                    Q1yFL.Text = "1492.022";
                    Q1zFL.Text = "98.061";
                    JO1xFL.Text = "63.115";
                    JO1yFL.Text = "1566.974";
                    JO1zFL.Text = "96.676";
                    P1xFL.Text = "136.833";
                    P1yFL.Text = "1492.022";
                    P1zFL.Text = "98.061";
                    J1xFL.Text = "217.557";
                    J1yFL.Text = "1583.889";
                    J1zFL.Text = "-29.548";
                    O1xFL.Text = "224.435";
                    O1yFL.Text = "1568.017";
                    O1zFL.Text = "-22.828";
                    H1xFL.Text = "289.433";
                    H1yFL.Text = "1536.817";
                    H1zFL.Text = "-47.502";

                    Q1xFR.Text = "0";
                    Q1yFR.Text = "1492.022";
                    Q1zFR.Text = "98.061";
                    JO1xFR.Text = "-63.115";
                    JO1yFR.Text = "1566.974";
                    JO1zFR.Text = "96.676";
                    P1xFR.Text = "-136.833";
                    P1yFR.Text = "1492.022";
                    P1zFR.Text = "98.061";
                    J1xFR.Text = "-217.557";
                    J1yFR.Text = "1583.889";
                    J1zFR.Text = "-29.548";
                    O1xFR.Text = "-224.435";
                    O1yFR.Text = "1568.017";
                    O1zFR.Text = "-22.828";
                    H1xFR.Text = "-289.433";
                    H1yFR.Text = "1536.817";
                    H1zFR.Text = "-47.502";



                    #endregion
                }
                else if (PullrodFront_VehicleGUI == 1 && PushrodFront_VehicleGUI == 0)
                {
                    #region Changing the values of the FRONT Textboxes for PULLROD and to reflect a Bell Crank whose Axis is at an angle with respect to XYZ axes

                    Q1xFL.Text = "0";
                    Q1yFL.Text = "1492.022";
                    Q1zFL.Text = "98.061";
                    JO1xFL.Text = "63.115";
                    JO1yFL.Text = "1099.74";
                    JO1zFL.Text = "96.676";
                    P1xFL.Text = "136.833";
                    P1yFL.Text = "1492.022";
                    P1zFL.Text = "98.061";
                    J1xFL.Text = "217.557";
                    J1yFL.Text = "1116.889";
                    J1zFL.Text = "-29.548";
                    O1xFL.Text = "224.435";
                    O1yFL.Text = "1101.017";
                    O1zFL.Text = "-22.828";
                    H1xFL.Text = "289.433";
                    H1yFL.Text = "1069.817";
                    H1zFL.Text = "-47.502";

                    Q1xFR.Text = "0";
                    Q1yFR.Text = "1492.022";
                    Q1zFR.Text = "98.061";
                    JO1xFR.Text = "-63.115";
                    JO1yFR.Text = "1099.74";
                    JO1zFR.Text = "96.676";
                    P1xFR.Text = "-136.833";
                    P1yFR.Text = "1492.022";
                    P1zFR.Text = "98.061";
                    J1xFR.Text = "-217.557";
                    J1yFR.Text = "1116.889";
                    J1zFR.Text = "-29.548";
                    O1xFR.Text = "-224.435";
                    O1yFR.Text = "1101.017";
                    O1zFR.Text = "-22.828";
                    H1xFR.Text = "-289.433";
                    H1yFR.Text = "1069.817";
                    H1zFR.Text = "-47.502";

                    #endregion

                }
            }
            #endregion

            #region GUI operations for Rear U-ARB
            if (UARBRear == 1)
            {
                #region Hiding the REAR Torsion Bar Bottom Point
                accordionControlFixedPointsRLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsRRTorsionBarBottom.Visible = false;
                #endregion

                if (PullrodRear_VehicleGUI == 0 && PushrodRear_VehicleGUI == 1)
                {
                    #region Changing the REAR Textboxes for PUSHROD and to reflect a Bell Crank whos Axis is Parallel to Z axis and Perpendicular to Y and X axes (Default Bell CranK)
                    Q1xRL.Text = "265";
                    Q1yRL.Text = "1068";
                    Q1zRL.Text = "-1548.13";
                    JO1xRL.Text = "18";
                    JO1yRL.Text = "1367";
                    JO1zRL.Text = "-1505";
                    P1xRL.Text = "260.21";
                    P1yRL.Text = "1055.57";
                    P1zRL.Text = "-1507.11";
                    J1xRL.Text = "216.65";
                    J1yRL.Text = "1389.98";
                    J1zRL.Text = "-1505";
                    O1xRL.Text = "255.99";
                    O1yRL.Text = "1302.71";
                    O1zRL.Text = "-1505";
                    H1xRL.Text = "267.74";
                    H1yRL.Text = "1349.01";
                    H1zRL.Text = "-1505";

                    Q1xRR.Text = "-265";
                    Q1yRR.Text = "1068";
                    Q1zRR.Text = "-1548.13";
                    JO1xRR.Text = "-18";
                    JO1yRR.Text = "1367";
                    JO1zRR.Text = "-1505";
                    P1xRR.Text = "-260.21";
                    P1yRR.Text = "1055.57";
                    P1zRR.Text = "-1507.11";
                    J1xRR.Text = "-216.65";
                    J1yRR.Text = "1389.98";
                    J1zRR.Text = "-1505";
                    O1xRR.Text = "-255.99";
                    O1yRR.Text = "1302.71";
                    O1zRR.Text = "-1505";
                    H1xRR.Text = "-267.74";
                    H1yRR.Text = "1349.01";
                    H1zRR.Text = "-1505";

                    #endregion
                }
                else if (PullrodRear_VehicleGUI == 1 && PushrodRear_VehicleGUI == 0)
                {
                    #region Changing the REAR Textboxes for PULLROD and to reflect a Bell Crank whos Axis is Parallel to Z axis and Perpendicular to Y and X axes (Default Bell CranK)
                    Q1xRL.Text = "265";
                    Q1yRL.Text = "1068";
                    Q1zRL.Text = "-1548.13";
                    JO1xRL.Text = "18";
                    JO1yRL.Text = "1105";
                    JO1zRL.Text = "-1505";
                    P1xRL.Text = "260.21";
                    P1yRL.Text = "1055.57";
                    P1zRL.Text = "-1507.11";
                    J1xRL.Text = "216.65";
                    J1yRL.Text = "1125.4";
                    J1zRL.Text = "-1505";
                    O1xRL.Text = "255.99";
                    O1yRL.Text = "1037.96";
                    O1zRL.Text = "-1505";
                    H1xRL.Text = "267.74";
                    H1yRL.Text = "1073";
                    H1zRL.Text = "-1505";

                    Q1xRR.Text = "-265";
                    Q1yRR.Text = "1068";
                    Q1zRR.Text = "-1548.13";
                    JO1xRR.Text = "-18";
                    JO1yRR.Text = "1105";
                    JO1zRR.Text = "-1505";
                    P1xRR.Text = "-260.21";
                    P1yRR.Text = "1055.57";
                    P1zRR.Text = "-1507.11";
                    J1xRR.Text = "-216.65";
                    J1yRR.Text = "1125.4";
                    J1zRR.Text = "-1505";
                    O1xRR.Text = "-255.99";
                    O1yRR.Text = "1037.96";
                    O1zRR.Text = "-1505";
                    H1xRR.Text = "-267.74";
                    H1yRR.Text = "1073";
                    H1zRR.Text = "-1505";

                    #endregion
                }
            }
            #endregion

            #region GUI operations for Rear T-ARB
            else if (TARBRear == 1)
            {
                #region Displaying the REAR Torsion Bar Bottom Point
                accordionControlFixedPointsRLTorsionBarBottom.Visible = true;
                accordionControlFixedPointsRRTorsionBarBottom.Visible = true;
                #endregion

                if (PullrodRear_VehicleGUI == 0 && PushrodRear_VehicleGUI == 1)
                {
                    #region Changing the values of the REAR Textboxes for PUSHROD and to reflect a Bell Crank whose Axis is at an angle with respect to XYZ axes
                    Q1xRL.Text = "0";
                    Q1yRL.Text = "1294.733";
                    Q1zRL.Text = "-1440.473";
                    JO1xRL.Text = "24.093";
                    JO1yRL.Text = "1355.491";
                    JO1zRL.Text = "-1449.481";
                    P1xRL.Text = "135.166";
                    P1yRL.Text = "1294.733";
                    P1zRL.Text = "-1440.473";
                    J1xRL.Text = "201.496";
                    J1yRL.Text = "1375.809";
                    J1zRL.Text = "-1539.509";
                    O1xRL.Text = "207.876";
                    O1yRL.Text = "1360.199";
                    O1zRL.Text = "-1528.209";
                    H1xRL.Text = "259.957";
                    H1yRL.Text = "1331.938";
                    H1zRL.Text = "-1524.188";

                    Q1xRR.Text = "0";
                    Q1yRR.Text = "1294.733";
                    Q1zRR.Text = "-1440.473";
                    JO1xRR.Text = "-24.093";
                    JO1yRR.Text = "1355.491";
                    JO1zRR.Text = "-1449.481";
                    P1xRR.Text = "-135.166";
                    P1yRR.Text = "1294.733";
                    P1zRR.Text = "-1440.473";
                    J1xRR.Text = "-201.496";
                    J1yRR.Text = "1375.809";
                    J1zRR.Text = "-1539.509";
                    O1xRR.Text = "-207.876";
                    O1yRR.Text = "1360.199";
                    O1zRR.Text = "-1528.209";
                    H1xRR.Text = "-259.957";
                    H1yRR.Text = "1331.938";
                    H1zRR.Text = "-1524.188";
                    #endregion
                }
                else if (PullrodRear_VehicleGUI == 1 && PushrodRear_VehicleGUI == 0)
                {
                    #region Changing the values of the REAR Textboxes for PULLROD and to reflect a Bell Crank whose Axis is at an angle with respect to XYZ axes
                    Q1xRL.Text = "0";
                    Q1yRL.Text = "1294.733";
                    Q1zRL.Text = "-1440.473";
                    JO1xRL.Text = "24.093";
                    JO1yRL.Text = "1099.74";
                    JO1zRL.Text = "-1449.481";
                    P1xRL.Text = "135.166";
                    P1yRL.Text = "1294.733";
                    P1zRL.Text = "-1440.473";
                    J1xRL.Text = "201.496";
                    J1yRL.Text = "1116.889";
                    J1zRL.Text = "-1539.509";
                    O1xRL.Text = "207.876";
                    O1yRL.Text = "1101.017";
                    O1zRL.Text = "-1528.209";
                    H1xRL.Text = "259.957";
                    H1yRL.Text = "1069.817";
                    H1zRL.Text = "-1524.188";

                    Q1xRR.Text = "0";
                    Q1yRR.Text = "1294.733";
                    Q1zRR.Text = "-1440.473";
                    JO1xRR.Text = "-24.093";
                    JO1yRR.Text = "1099.74";
                    JO1zRR.Text = "-1449.481";
                    P1xRR.Text = "-135.166";
                    P1yRR.Text = "1294.733";
                    P1zRR.Text = "-1440.473";
                    J1xRR.Text = "-201.496";
                    J1yRR.Text = "1116.889";
                    J1zRR.Text = "-1539.509";
                    O1xRR.Text = "-207.876";
                    O1yRR.Text = "1101.017";
                    O1zRR.Text = "-1528.209";
                    H1xRR.Text = "-259.957";
                    H1yRR.Text = "1069.817";
                    H1zRR.Text = "-1524.188";
                    #endregion
                }
            }
            #endregion

        }

        #endregion

        #endregion

        #region NavBar and Ribbon GUI Operations
        //
        // navBar Items GUI
        //
        #region NavBarControl2 GUI
        private void navBarControl2_MouseDown(object sender, MouseEventArgs e)
        {
            navBarControl2 = sender as NavBarControl;
            NavBarHitInfo hitinfo = navBarControl2.CalcHitInfo(e.Location);
            if (hitinfo.HitTest == NavBarHitTest.GroupCaption)
            {
                switch (hitinfo.Group.Caption)
                {
                    case "Suspension Front Left":
                        if (SuspensionCoordinatesFront.SCFLCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();
                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlSuspensionCoordinatesFL.Show();
                            accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlFixedPointsFL);
                            accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlMovingPointsFL);
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
                        break;

                    case "Suspension Front Right":
                        if (SuspensionCoordinatesFrontRight.SCFRCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();
                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlSuspensionCoordinatesFR.Show();
                            accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlFixedPointsFR);
                            accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlMovingPointsFR);
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
                        break;

                    case "Suspension Rear Left":
                        if (SuspensionCoordinatesRear.SCRLCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();
                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlSuspensionCoordinatesRL.Show();
                            accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlFixedPointsRL);
                            accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlMovingPointsRL);

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
                        break;

                    case "Suspension Rear Right":
                        if (SuspensionCoordinatesRearRight.SCRRCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();
                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlSuspensionCoordinatesRR.Show();
                            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlFixedPointsRR);
                            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlMovingPointsRR);
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
                        break;

                    case "Tire":
                        if (Tire.TireCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
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
                            //accordionControlTireStiffness.Show();
                            #endregion

                            #region Populating the UndoRedo Class' Undo/Redo Stacks
                            UndoObject.Identifier(Tire.Assy_List_Tire[Tire.CurrentTireID - 1]._UndocommandsTire, Tire.Assy_List_Tire[Tire.CurrentTireID - 1]._RedocommandsTire, Tire.CurrentTireID, Tire.Assy_List_Tire[Tire.CurrentTireID - 1].TireIsModified); 
                            #endregion

                            #region Grid Control operations
                            gridControl2.DataSource = Tire.Assy_List_Tire[navBarGroupTireStiffness.SelectedLinkIndex].TireDataTable;
                            gridControl2.MainView = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].bandedGridView_TireGUI;
                            #endregion
                        }
                        else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }

                        break;

                    case "Springs":
                        if (Spring.SpringCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();

                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlSprings.Show();
                            #endregion

                            #region Populating the UndoRedo Class' Undo/Redo Stacks
                            UndoObject.Identifier(Spring.Assy_List_Spring[Spring.CurrentSpringID - 1]._UndocommandsSpring, Spring.Assy_List_Spring[Spring.CurrentSpringID - 1]._RedocommandsSpring, Spring.CurrentSpringID, Spring.Assy_List_Spring[Spring.CurrentSpringID - 1].SpringIsModified); 
                            #endregion

                            #region Grid Control operations
                            gridControl2.DataSource = Spring.Assy_List_Spring[navBarGroupSprings.SelectedLinkIndex].SpringDataTable;
                            gridControl2.MainView = springGUI[navBarGroupSprings.SelectedLinkIndex].bandedGridView_SpringGUI;
                            #endregion
                        }
                        else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }

                        break;

                    case "Damper":
                        if (Damper.DamperCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();

                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlDamper.Show();
                            #endregion

                            #region Populating the UndoRedo Class' Undo/Redo Stacks
                            UndoObject.Identifier(Damper.Assy_List_Damper[Damper.CurrentDamperID - 1]._UndocommandsDamper, Damper.Assy_List_Damper[Damper.CurrentDamperID - 1]._RedocommandsDamper, Damper.CurrentDamperID, Damper.Assy_List_Damper[Damper.CurrentDamperID - 1].DamperIsModified); 
                            #endregion

                            #region Grid Control Operations
                            gridControl2.DataSource = Damper.Assy_List_Damper[navBarGroupDamper.SelectedLinkIndex].DamperDataTable;
                            gridControl2.MainView = damperGUI[navBarGroupDamper.SelectedLinkIndex].bandedGridView_DamperGUI;
                            #endregion
                        }
                        else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }

                        break;

                    case "Anti-Roll Bar":
                        if (AntiRollBar.AntiRollBarCounter != 0)
                        {

                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();

                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlAntiRollBar.Show();
                            #endregion

                            #region Populating the UndoRedo Class' Undo/Redo Stacks
                            UndoObject.Identifier(AntiRollBar.Assy_List_ARB[AntiRollBar.CurrentAntiRollBarID - 1]._UndocommandsARB, AntiRollBar.Assy_List_ARB[AntiRollBar.CurrentAntiRollBarID - 1]._RedocommandsARB, AntiRollBar.CurrentAntiRollBarID, AntiRollBar.Assy_List_ARB[AntiRollBar.CurrentAntiRollBarID - 1].AntiRollBarIsModified); 
                            #endregion

                            #region Grid Control Operations
                            gridControl2.DataSource = AntiRollBar.Assy_List_ARB[navBarGroupAntiRollBar.SelectedLinkIndex].ARBDataTable;
                            gridControl2.MainView = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].bandedGridView_ARBGUI;
                            #endregion

                        }
                        else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }

                        break;

                    case "Chassis":
                        if (Chassis.ChassisCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();

                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlChassis.Show();
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

                        break;

                    case "Wheel Alignment":
                        if (WheelAlignment.WheelAlignmentCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();
                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlWheelAlignment.Show();
                            #endregion

                            #region Populating the UndoRedo Class' Undo/Redo Stacks
                            UndoObject.Identifier(WheelAlignment.Assy_List_WA[WheelAlignment.CurrentWheelAlignmentID - 1]._UndocommandsWheelAlignment, WheelAlignment.Assy_List_WA[WheelAlignment.CurrentWheelAlignmentID - 1]._RedocommandsWheelAlignment, WheelAlignment.CurrentWheelAlignmentID, WheelAlignment.Assy_List_WA[WheelAlignment.CurrentWheelAlignmentID - 1].WheelAlignmentIsModified); 
                            #endregion

                            #region Grid Control Operations
                            gridControl2.DataSource = WheelAlignment.Assy_List_WA[navBarGroupWheelAlignment.SelectedLinkIndex].WADataTable;
                            gridControl2.MainView = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].bandedGridView_WAGUI;
                            #endregion

                        }
                        else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
                        break;

                    case "Vehicle":
                        if (Vehicle.VehicleCounter != 0)
                        {
                            #region GUI
                            sidePanel2.Show();
                            groupControl13.Show();
                            accordionControlTireStiffness.Hide();
                            accordionControlSuspensionCoordinatesFL.Hide();
                            accordionControlSuspensionCoordinatesFR.Hide();
                            accordionControlSuspensionCoordinatesRL.Hide();
                            accordionControlSuspensionCoordinatesRR.Hide();
                            accordionControlDamper.Hide();
                            accordionControlAntiRollBar.Hide();
                            accordionControlSprings.Hide();
                            accordionControlChassis.Hide();
                            accordionControlWheelAlignment.Hide();
                            accordionControlVehicleItem.Hide();
                            accordionControlVehicleItem.Show();
                            accordionControlVehicleItem.BringToFront();
                            #endregion

                            #region Populating the UndoRedo Class' Undo/Redo Stacks
                            UndoObject.Identifier(Vehicle.List_Vehicle[Vehicle.CurrentVehicleID - 1]._UndocommandsVehicle, Vehicle.List_Vehicle[Vehicle.CurrentVehicleID - 1]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[Vehicle.CurrentVehicleID - 1].VehicleIsModified); 
                            #endregion
                        }

                        else { barButtonUndo.Enabled = false; barButtonRedo.Enabled = false; }
                        break;

                    default: break;
                }
            }
        } 
        #endregion

        //
        //Ribbon Items GUI
        //
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
                        #region GUI
                        sidePanel2.Hide();
                        accordionControlTireStiffness.Hide();
                        accordionControlSuspensionCoordinatesFL.Hide();
                        accordionControlSuspensionCoordinatesFR.Hide();
                        accordionControlSuspensionCoordinatesRL.Hide();
                        accordionControlSuspensionCoordinatesRR.Hide();
                        accordionControlDamper.Hide();
                        accordionControlAntiRollBar.Hide();
                        accordionControlSprings.Hide();
                        accordionControlChassis.Hide();
                        accordionControlWheelAlignment.Hide();
                        accordionControlVehicleItem.Hide();
                        navBarGroupSimulation.Expanded = false;
                        navBarGroupDesign.Visible = true;
                        navBarGroupDesign.Expanded = true;

                        #endregion
                        
                        break;

                    case "ribbonPageSimulation":
                        #region GUI
                        sidePanel2.Hide();
                        accordionControlTireStiffness.Hide();
                        accordionControlSuspensionCoordinatesFL.Hide();
                        accordionControlSuspensionCoordinatesFR.Hide();
                        accordionControlSuspensionCoordinatesRL.Hide();
                        accordionControlSuspensionCoordinatesRR.Hide();
                        accordionControlDamper.Hide();
                        accordionControlAntiRollBar.Hide();
                        accordionControlSprings.Hide();
                        accordionControlChassis.Hide();
                        accordionControlWheelAlignment.Hide();
                        accordionControlVehicleItem.Hide();
                        navBarGroupDesign.Expanded = false;
                        navBarGroupSimulation.Visible = true;
                        navBarGroupSimulation.Expanded = true;
                        #endregion
                        barButtonUndo.Enabled = false; barButtonRedo.Enabled = false;
                        break;
                    case "ribbonPageResults":
                        #region GUI
                        sidePanel2.Hide();
                        accordionControlTireStiffness.Hide();
                        accordionControlSuspensionCoordinatesFL.Hide();
                        accordionControlSuspensionCoordinatesFR.Hide();
                        accordionControlSuspensionCoordinatesRL.Hide();
                        accordionControlSuspensionCoordinatesRR.Hide();
                        accordionControlDamper.Hide();
                        accordionControlAntiRollBar.Hide();
                        accordionControlSprings.Hide();
                        accordionControlChassis.Hide();
                        accordionControlWheelAlignment.Hide();
                        accordionControlVehicleItem.Hide();
                        #endregion
                        barButtonUndo.Enabled = false; barButtonRedo.Enabled = false;
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
            if (navBarControl2.ActiveGroup == navBarGroupTireStiffness)
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
                        break;
                    }
                }
                ComboboxTireOperations();
                // Counter is not incremented here because in this code block a new item is not being created, it is only being edited. 
            }
            #endregion

            #region Spring Modification
            if (navBarControl2.ActiveGroup == navBarGroupSprings)
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

                        break;

                    }
                }
                ComboBoxSpringOperations();
                // Counter is not imcremented here becsuse in this code bloc a new item is not created, it is only being edited
            }
            #endregion

            #region Damper Modification
            if (navBarControl2.ActiveGroup == navBarGroupDamper)
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
                        break;

                    }
                }
                ComboboxDamperOperations();
                // Counter is not incremented here because in this code block a new damperitem is not being created, it is only being edited.
            }
            #endregion

            #region ARB Modification
            if (navBarControl2.ActiveGroup == navBarGroupAntiRollBar)
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
                        break;
                    }

                }
                ComboboxARBOperations();
                // Counter is not incremented here because in this code a new ARB item is not being created, it is only being edited
            }
            #endregion

            #region Wheel Alignment Modification
            if (navBarControl2.ActiveGroup == navBarGroupWheelAlignment)
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

                        break;
                    }
                }
                ComboboxWheelAlignmentOperations();
            }
            #endregion

            #region Chassis Modification
            if (navBarControl2.ActiveGroup == navBarGroupChassis)
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

                        break;
                    }
                }
                ComboboxChassisOperations();
                // Counter is not incremented here because in this code a new Chassis item is not being created, it is only being edited
            }
            #endregion

            #region SCFL Modification
            if (navBarControl2.ActiveGroup == navBarGroupSuspensionFL)
            {
                ModifyFrontLeftSuspension(false);
            }
            #endregion

            #region SCFR Modification
            if (navBarControl2.ActiveGroup == navBarGroupSuspensionFR)
            {
                ModifyFrontRightSuspension(false);
            }
            #endregion

            #region SCRL Modification
            if (navBarControl2.ActiveGroup == navBarGroupSuspensionRL)
            {
                ModifyRearLeftSuspension(false);
            }
            #endregion

            #region SCRR Modification
            if (navBarControl2.ActiveGroup == navBarGroupSuspensionRR)
            {
                ModifyRearRightSuspension(false);
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
            navBarGroupTireStiffness.Expanded = true;
            navBarGroupDesign.Visible = true;
            //accordionControlTireStiffness.ExpandElement(accordionControlTire1TireStiffness);
            //accordionControlTireStiffness.ExpandElement(accordionControlTire1TireWidth);

            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_tire = 0; i_tire <= navBarItemTireClass.navBarItemTire.Count; i_tire++)
            {
                if (Tire.TireCounter == i_tire)
                {
                    #region Creating a new NavBarItem and adding it the Tire Group
                    navBarItemTireClass temp_navBarItemTire = new navBarItemTireClass();
                    navBarTire_Global.CreateNewNavBarItem(i_tire, temp_navBarItemTire, navBarControl2, navBarGroupTireStiffness);
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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlTireStiffness.Show();
            navBarGroupTireStiffness.Expanded = true;
            navBarGroupDesign.Visible = true;
            accordionControlTireStiffness.ExpandElement(accordionControlTire1TireStiffness);
            accordionControlTireStiffness.ExpandElement(accordionControlTire1TireWidth);
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
                    comboBoxTireFL.SelectedIndex = 0;
                    comboBoxTireFL.DisplayMember = "_TireName";


                    comboBoxTireFR.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireFR.SelectedIndex = 0;
                    comboBoxTireFR.DisplayMember = "_TireName";


                    comboBoxTireRL.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireRL.SelectedIndex = 0;
                    comboBoxTireRL.DisplayMember = "_TireName";


                    comboBoxTireRR.Items.Insert(i_combobox_tire, Tire.Assy_List_Tire[i_combobox_tire]);
                    comboBoxTireRR.SelectedIndex = 0;
                    comboBoxTireRR.DisplayMember = "_TireName";

                    #endregion
                }
                catch (Exception) { }
            }
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
            navBarGroupDesign.Visible = true;
            navBarGroupSprings.Expanded = true;
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringRate);
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringPreload);
            //accordionControlSprings.ExpandElement(accordionControlSpringSpringFreeLength);

            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_spring = 0; i_spring <= navBarItemSpringClass.navBarItemSpring.Count; i_spring++)
            {
                if (Spring.SpringCounter == i_spring)
                {
                    #region Creating a new NavBarItem and adding it the Spring Group
                    navBarItemSpringClass temp_navBarItemSpring = new navBarItemSpringClass();
                    navBarSpring_Global.CreateNewNavBarItem(i_spring, temp_navBarItemSpring, navBarControl2, navBarGroupSprings);
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

                    break;
                }
            }

            Spring.SpringCounter++;// This is a static counter and it keeps track of the number of Spring items created
            ComboBoxSpringOperations();

        }

        //private void SpringTextBox_Leave(object sender, EventArgs e)
        //{

        //}


        //private void SpringTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int index = navBarGroupSprings.SelectedLinkIndex;

        //    #region GUI operations to change the color of the textbox to white in case invalid input was entered by the user
        //    SpringRateFL.BackColor = Color.White;
        //    SpringPreloadFL.BackColor = Color.White;
        //    SpringFreeLengthFL.BackColor = Color.White;
        //    #endregion

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //    #region Creating an Object of Spring which will be added to the List of Spring 
        //    SpringGUI springGUI = new SpringGUI(this);
        //    Spring spring_list = new Spring(springGUI);
        //    S1_Global = spring_list;
        //    #endregion

        //    for (int l_spring = 0; l_spring <= Spring.Assy_List_Spring.Count; l_spring++)
        //    {
        //        if (index == l_spring)
        //        {
        //            #region Adding a new Spring to List of Spring Objects
        //            S1_Global.ModifyObjectData(l_spring, spring_list, false);
        //            Spring.CurrentSpringID = Spring.Assy_List_Spring[l_spring].SpringID;
        //            UndoObject.Identifier(Spring.Assy_List_Spring[l_spring]._UndocommandsSpring, Spring.Assy_List_Spring[l_spring]._RedocommandsSpring, Spring.CurrentSpringID, Spring.Assy_List_Spring[l_spring].SpringIsModified);
        //            break;
        //            #endregion
        //        }
        //    }

        //        ComboBoxSpringOperations();
        //        // Counter is not imcremented here becsuse in this code bloc a new item is not created, it is only being edited
        //    }

        //}

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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSprings.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSprings.Expanded = true;
            accordionControlSprings.ExpandElement(accordionControlSpringSpringRate);
            accordionControlSprings.ExpandElement(accordionControlSpringSpringPreload);
            accordionControlSprings.ExpandElement(accordionControlSpringSpringFreeLength);
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
                    comboBoxSpringFL.SelectedIndex = 0;
                    comboBoxSpringFL.DisplayMember = "_SpringName";


                    comboBoxSpringFR.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringFR.SelectedIndex = 0;
                    comboBoxSpringFR.DisplayMember = "_SpringName";


                    comboBoxSpringRL.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringRL.SelectedIndex = 0;
                    comboBoxSpringRL.DisplayMember = "_SpringName";


                    comboBoxSpringRR.Items.Insert(i_combobox_spring, Spring.Assy_List_Spring[i_combobox_spring]);
                    comboBoxSpringRR.SelectedIndex = 0;
                    comboBoxSpringRR.DisplayMember = "_SpringName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }

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
            navBarGroupDesign.Visible = true;
            navBarGroupDamper.Expanded = true;
            //accordionControlDamper.ExpandElement(accordionControlDamperGasPressure);
            //accordionControlDamper.ExpandElement(accordionControlDamperShaftDiameter);

            

            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_damper = 0; i_damper <= navbarItemDamperClass.navBarItemDamper.Count; i_damper++)
            {
                if (Damper.DamperCounter == i_damper)
                {
                    #region Creating a new navBarItem and adding it to the Damper Group
                    navbarItemDamperClass temp_navBarItemDamper = new navbarItemDamperClass();
                    navBarDamper_Global.CreateNewNavBarItem(i_damper, temp_navBarItemDamper, navBarControl2, navBarGroupDamper);
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
                    break;
                    #endregion
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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlDamper.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupDamper.Expanded = true;
            accordionControlDamper.ExpandElement(accordionControlDamperGasPressure);
            accordionControlDamper.ExpandElement(accordionControlDamperShaftDiameter);
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
                    comboBoxDamperFL.SelectedIndex = 0;
                    comboBoxDamperFL.DisplayMember = "_DamperName";


                    comboBoxDamperFR.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperFR.SelectedIndex = 0;
                    comboBoxDamperFR.DisplayMember = "_DamperName";


                    comboBoxDamperRL.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperRL.SelectedIndex = 0;
                    comboBoxDamperRL.DisplayMember = "_DamperName";


                    comboBoxDamperRR.Items.Insert(i_combobox_damper, Damper.Assy_List_Damper[i_combobox_damper]);
                    comboBoxDamperRR.SelectedIndex = 0;
                    comboBoxDamperRR.DisplayMember = "_DamperName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }

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
            navBarGroupDesign.Visible = true;
            navBarGroupAntiRollBar.Expanded = true;
            //accordionControlAntiRollBar.ExpandElement(accordionControlAntiRollBarStiffness);

            

            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_arb = 0; i_arb <= navBarItemARBClass.navBarItemARB.Count; i_arb++)
            {
                if (AntiRollBar.AntiRollBarCounter == i_arb)
                {
                    #region Creating a new NavBarItem and adding it to the AntiRollBar Group
                    navBarItemARBClass temp_navBarItemARB = new navBarItemARBClass();
                    temp_navBarItemARB.CreateNewNavBarItem(i_arb, temp_navBarItemARB, navBarControl2, navBarGroupAntiRollBar);
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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlAntiRollBar.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupAntiRollBar.Expanded = true;
            accordionControlAntiRollBar.ExpandElement(accordionControlAntiRollBarStiffness);
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
                    comboBoxARBFront.SelectedIndex = 0;


                    comboBoxARBRear.Items.Insert(i_combobox_arb, AntiRollBar.Assy_List_ARB[i_combobox_arb]);
                    comboBoxARBRear.DisplayMember = "_ARBName";
                    comboBoxARBRear.SelectedIndex = 0;

                    #endregion
                }
                catch (Exception)
                {


                }
            }

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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_chassis = 0; i_chassis <= navBarItemChassisClass.navBarItemChassis.Count; i_chassis++)
            {
                if (Chassis.ChassisCounter == i_chassis)
                {
                    #region Creating a new NavBarItem and adding it to the Chassis Group
                    navBarItemChassisClass temp_navBarItemChassis = new navBarItemChassisClass();
                    navBarChassis_Global.CreateNewNavBarItem(i_chassis, temp_navBarItemChassis, navBarControl2, navBarGroupChassis);
                    navBarItemChassisClass.navBarItemChassis[i_chassis].LinkClicked += new NavBarLinkEventHandler(navBarItemChassis_LinkClicked);
                    break;
                    #endregion
                }
            }

            for (int l_chassis = 0; l_chassis <= Chassis.Assy_List_Chassis.Count; l_chassis++)
            {
                if (Chassis.ChassisCounter == l_chassis)
                {
                    #region Creating an object of chassis whcih will be added to the list of chassis objects
                    chassisGUI.Insert(l_chassis,new ChassisGUI(this));
                    #endregion

                    #region Invoking the Default_Values class' method to populate the ChassisGUI table
                    Default_Values.ChassisDefaultValues.MassAndSMCoGDefaultValues(chassisGUI[l_chassis]);
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

                    break;

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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlChassis.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupChassis.Expanded = true;
            accordionControlChassis.ExpandElement(accordionControlSuspendedMassExpectedMass);
            accordionControlChassis.ExpandElement(accordionControlSuspendedMassCGExpectedCG);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass2ExpectedMass);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass3ExpectedMass);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMass4ExpectedMass);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationFL);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationFR);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationRL);
            accordionControlChassis.ExpandElement(accordionControlNonSuspendedMassCGExpectedLocationRR);
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
            #region Clearing out the Comboboxes
            comboBoxChassis.Items.Clear();
            #endregion

            for (int i_combobox_chassis = 0; i_combobox_chassis < Chassis.Assy_List_Chassis.Count; i_combobox_chassis++)
            {
                try
                #region Populating the Comboboxes with the list of Chassis Objects
                {
                    comboBoxChassis.Items.Insert(i_combobox_chassis, Chassis.Assy_List_Chassis[i_combobox_chassis]);
                    comboBoxChassis.SelectedIndex = 0;
                    comboBoxChassis.DisplayMember = "_ChassisName";

                #endregion
                }
                catch (Exception)
                {
                }
            }

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
            navBarGroupDesign.Visible = true;
            navBarGroupWheelAlignment.Expanded = true;
            //accordionControlWheelAlignment.ExpandElement(accordionControlWACamber1);
            //accordionControlWheelAlignment.ExpandElement(accordionControWAlToe1);

            

            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_wa = 0; i_wa <= navBarItemWAClass.navBarItemWA.Count; i_wa++)
            {
                if (WheelAlignment.WheelAlignmentCounter == i_wa)
                {
                    #region Creating a new NavBarItem and adding it to the WheelAlignment Group
                    navBarItemWAClass temp_navBarItemWA = new navBarItemWAClass();
                    navBarWA_Global.CreateNewWAItem(i_wa, temp_navBarItemWA, navBarControl2, navBarGroupWheelAlignment);
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

                    break;

                }
            }
            WheelAlignment.WheelAlignmentCounter++; // This is a static counter and it keeps track of the number of Chassis items created
            ComboboxWheelAlignmentOperations();

        }


        #region Delete
        //private void WATextBox_Leave(object sender, EventArgs e)
        //{



        //}

        //private void WATextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int index = navBarGroupWheelAlignment.SelectedLinkIndex;

        //    #region GUI Operation to change thr color of the Text Box to white in case invalid user input is entered
        //    StaticCamberFL.BackColor = Color.White;
        //    StaticToeFL.BackColor = Color.White;
        //    #endregion

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        #region Creating an object of WheelAlignment whcih will be added to the list of chassis objects

        //        WheelAlignmentGUI waGUI = new WheelAlignmentGUI(this);
        //        WheelAlignment wa_list = new WheelAlignment(waGUI);
        //        W1_Global = wa_list;
        //        #endregion

        //        for (int l_wa = 0; l_wa <= WheelAlignment.Assy_List_WA.Count; l_wa++)
        //        {
        //            if (index == l_wa)
        //            {
        //                #region Adding the new AntiRollBar object to the list AntiRollBar Objects
        //                W1_Global.ModifyObjectData(l_wa, wa_list, false);
        //                WheelAlignment.CurrentWheelAlignmentID = WheelAlignment.Assy_List_WA[l_wa].WheelAlignmentID;
        //                UndoObject.Identifier(WheelAlignment.Assy_List_WA[l_wa]._UndocommandsWheelAlignment, WheelAlignment.Assy_List_WA[l_wa]._RedocommandsWheelAlignment, WheelAlignment.CurrentWheelAlignmentID, WheelAlignment.Assy_List_WA[l_wa].WheelAlignmentIsModified);
        //                break;
        //                #endregion
        //            }
        //        }
        //        ComboboxWheelAlignmentOperations();
        //    }
        //} 
        #endregion


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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlWheelAlignment.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupWheelAlignment.Expanded = true;
            accordionControlWheelAlignment.ExpandElement(accordionControlWACamber1);
            accordionControlWheelAlignment.ExpandElement(accordionControWAlToe1);
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
                    comboBoxWAFL.SelectedIndex = 0;
                    comboBoxWAFL.DisplayMember = "_WAName";


                    comboBoxWAFR.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWAFR.SelectedIndex = 0;
                    comboBoxWAFR.DisplayMember = "_WAName";


                    comboBoxWARL.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWARL.SelectedIndex = 0;
                    comboBoxWARL.DisplayMember = "_WAName";


                    comboBoxWARR.Items.Insert(i_combobox_wa, WheelAlignment.Assy_List_WA[i_combobox_wa]);
                    comboBoxWARR.SelectedIndex = 0;
                    comboBoxWARR.DisplayMember = "_WAName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }

        }
        #endregion

        //
        // Front Left Suspension Coordinate Item Creation and GUI
        //
        #region Front Left Suspension Coordinate Item Creation and GUI

        List<SuspensionCoordinatesFrontGUI> scflGUI = new List<SuspensionCoordinatesFrontGUI>();

        private void BarButtonSCFL_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scfl = 0; i_scfl <= navBarItemSCFLClass.navBarItemSCFL.Count; i_scfl++)
            {
                if (SuspensionCoordinatesFront.SCFLCounter == i_scfl)
                {
                    #region Creating a new NavBarItem and adding it to the Suspension Coordinates Front Left Group
                    navBarItemSCFLClass temp_navBarItemSCFL = new navBarItemSCFLClass();
                    navBarSCFL_Global.CreateNewNavbarItem(i_scfl, temp_navBarItemSCFL, navBarControl2, navBarGroupSuspensionFL);
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
                    if (DoubleWishboneFront_VehicleGUI==1)
                    {
                        Default_Values.FRONTLEFTSuspensionDefaultValues.DoubleWishBone(this, scflGUI[l_scfl]); 
                    }
                    else if (McPhersonFront_VehicleGUI==1)
                    {
                        Default_Values.FRONTLEFTSuspensionDefaultValues.McPherson(scflGUI[l_scfl]);

                    }
                    #endregion

                    #region Populating the SCFLGUI Object
                    scflGUI[l_scfl].EditFrontLeftCoordinatesGUI(this, scflGUI[l_scfl]);
                    #endregion

                    #region Adding the new SCFL object to the list SCFL Objects
                    SCFL1_Global.CreateNewSCFL(l_scfl, scflGUI[l_scfl]);
                    SuspensionCoordinatesFront.SCFLCurrentID = SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFL_ID;
                    #endregion

                    #region Initializing the DataGrid of the for SCFLGUI 
                    scflGUI[l_scfl].bandedGridView_SCFLGUI = CustomBandedGridView.CreateNewBandedGridView(l_scfl, 4, "Front Left Suspension Coordinates");
                    gridControl2.DataSource = scflGUI[l_scfl].SCFLDataTableGUI;
                    gridControl2.MainView = scflGUI[l_scfl].bandedGridView_SCFLGUI;
                    scflGUI[l_scfl].bandedGridView_SCFLGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scflGUI[l_scfl].bandedGridView_SCFLGUI,this);
                    scflGUI[l_scfl].bandedGridView_SCFLGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scflGUI[l_scfl].bandedGridView_SCFLGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFLIsModified); 
                    #endregion

                    break;
                }
            }

            SuspensionCoordinatesFront.SCFLCounter++;// This is a static counter and it keeps track of the number of Chassis items created
            ComboboxSCFLOperations();

        }

        public void ModifyFrontLeftSuspension(bool Copied_Identifier)
        {
            // Copied Identifier determines whether Front Left Coordinates have been copied or manually edited by the user. Based on its value, the CopyFrontLeftTOFrontRight function is called.
            // This prevents an infinite loop

            int index = navBarGroupSuspensionFL.SelectedLinkIndex;

            for (int l_scfl = 0; l_scfl <= SuspensionCoordinatesFront.Assy_List_SCFL.Count; l_scfl++)
            {
                if (index == l_scfl)
                {
                    try
                    {
                        #region Editing the SCFLGUI Object
                        scflGUI[l_scfl].EditFrontLeftCoordinatesGUI(this, scflGUI[l_scfl]);
                        #endregion

                        #region Editing the Object
                        SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].EditFrontLeftSuspension(l_scfl, scflGUI[l_scfl]);
                        SuspensionCoordinatesFront.SCFLCurrentID = SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFL_ID;
                        #endregion

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFLIsModified);
                        #endregion

                        #region Copying the Front Left Coordinates to the Front Right if symmetry is chosen
                        if (SuspensionCoordinatesFront.Assy_List_SCFL[index].FrontSymmetry == true && Copied_Identifier==false)
                        {
                            CopyFrontLeftTOFrontRight();
                        } 
                        #endregion

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

        #region Delete
        public void SCFLTextBox_Leave(object sender, EventArgs e)
        {
            //int index = navBarGroupSuspensionFL.SelectedLinkIndex;



            //for (int l_scfl = 0; l_scfl <= SuspensionCoordinatesFront.Assy_List_SCFL.Count; l_scfl++)
            //{
            //    if (index == l_scfl)
            //    {
            //        try
            //        {
            //            scflGUI[l_scfl].EditFrontLeftCoordinatesGUI(this);

            //            #region Editing the Object
            //            SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].EditFrontLeftSuspension(l_scfl, scflGUI[l_scfl]);
            //            SuspensionCoordinatesFront.SCFLCurrentID = SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFL_ID;
            //            UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFLIsModified);
            //            break;
            //            #endregion
            //        }
            //        catch (Exception)
            //        {
            //            // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
            //        }
            //    }
            //}
            //ComboboxSCFLOperations();

        }

        private void SCFLTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //int index = navBarGroupSuspensionFL.SelectedLinkIndex;

            //#region GUI Operation to change thr color of the Text Box to white in case invalid user input is entered
            //A1xFL.BackColor = Color.White;
            //A1yFL.BackColor = Color.White;
            //A1zFL.BackColor = Color.White;

            //B1xFL.BackColor = Color.White;
            //B1yFL.BackColor = Color.White;
            //B1zFL.BackColor = Color.White;

            //C1xFL.BackColor = Color.White;
            //C1yFL.BackColor = Color.White;
            //C1zFL.BackColor = Color.White;

            //D1xFL.BackColor = Color.White;
            //D1yFL.BackColor = Color.White;
            //D1zFL.BackColor = Color.White;

            //E1xFL.BackColor = Color.White;
            //E1yFL.BackColor = Color.White;
            //E1zFL.BackColor = Color.White;

            //F1xFL.BackColor = Color.White;
            //F1yFL.BackColor = Color.White;
            //F1zFL.BackColor = Color.White;

            //G1xFL.BackColor = Color.White;
            //G1yFL.BackColor = Color.White;
            //G1zFL.BackColor = Color.White;

            //H1xFL.BackColor = Color.White;
            //H1yFL.BackColor = Color.White;
            //H1zFL.BackColor = Color.White;

            //I1xFL.BackColor = Color.White;
            //I1yFL.BackColor = Color.White;
            //I1zFL.BackColor = Color.White;

            //J1xFL.BackColor = Color.White;
            //J1yFL.BackColor = Color.White;
            //J1zFL.BackColor = Color.White;

            //JO1xFL.BackColor = Color.White;
            //JO1yFL.BackColor = Color.White;
            //JO1zFL.BackColor = Color.White;

            //K1xFL.BackColor = Color.White;
            //K1yFL.BackColor = Color.White;
            //K1zFL.BackColor = Color.White;

            //M1xFL.BackColor = Color.White;
            //M1yFL.BackColor = Color.White;
            //M1zFL.BackColor = Color.White;

            //N1xFL.BackColor = Color.White;
            //N1yFL.BackColor = Color.White;
            //N1zFL.BackColor = Color.White;

            //O1xFL.BackColor = Color.White;
            //O1yFL.BackColor = Color.White;
            //O1zFL.BackColor = Color.White;

            //P1xFL.BackColor = Color.White;
            //P1yFL.BackColor = Color.White;
            //P1zFL.BackColor = Color.White;

            //Q1xFL.BackColor = Color.White;
            //Q1yFL.BackColor = Color.White;
            //Q1zFL.BackColor = Color.White;

            //R1xFL.BackColor = Color.White;
            //R1yFL.BackColor = Color.White;
            //R1zFL.BackColor = Color.White;

            //W1xFL.BackColor = Color.White;
            //W1yFL.BackColor = Color.White;
            //W1zFL.BackColor = Color.White;

            //RideHeightRefFLx.BackColor = Color.White;
            //RideHeightRefFLy.BackColor = Color.White;
            //RideHeightRefFLz.BackColor = Color.White;
            //#endregion

            //if (e.KeyCode == Keys.Enter)
            //{
            //    for (int l_scfl = 0; l_scfl <= SuspensionCoordinatesFront.Assy_List_SCFL.Count; l_scfl++)
            //    {
            //        if (index == l_scfl)
            //        {
            //            try
            //            {
            //                scflGUI[l_scfl].EditFrontLeftCoordinatesGUI(this);

            //                #region Editing the Object
            //                SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].EditFrontLeftSuspension(l_scfl, scflGUI[l_scfl]);
            //                SuspensionCoordinatesFront.SCFLCurrentID = SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFL_ID;
            //                UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[l_scfl].SCFLIsModified);
            //                break;
            //                #endregion
            //            }
            //            catch (Exception)
            //            {

            //                // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
            //            }
            //        }
            //    }
            //    ComboBoxSCFROperations();
            //}

        } 
        #endregion

        #region This code is specifically added to handle the selection of the Front Left navbarItem when its corresponding Front Right navbarItem is selected
        public void navBarItemSCFL_LinkClickedGUIEvents()
        {
            int index = navBarGroupSuspensionFL.SelectedLinkIndex;

            for (int c_scfl = 0; c_scfl <= navBarItemSCFLClass.navBarItemSCFL.Count; c_scfl++)
            {
                if (index == c_scfl)
                {
                    try
                    {
                        #region GUI Operations depending on the Type of Suspension
                        if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].DoubleWishboneIdentifierFront == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has Double Wishbone
                            accordionControlFixedPointsFLTorsionBarBottom.Visible = false;

                            A1xFL.Show(); A1yFL.Show(); A1zFL.Show();
                            B1xFL.Show(); B1yFL.Show(); B1zFL.Show();
                            I1xFL.Show(); I1yFL.Show(); I1zFL.Show();
                            H1xFL.Show(); H1yFL.Show(); H1zFL.Show();
                            G1xFL.Show(); G1yFL.Show(); G1zFL.Show();
                            F1xFL.Show(); F1yFL.Show(); F1zFL.Show();
                            O1xFL.Show(); O1yFL.Show(); O1zFL.Show();

                            accordionControlFixedPointsFLUpperFrontChassis.Visible = true; accordionControlFixedPointsFLUpperRearChassis.Visible = true; accordionControlFixedPointsFLBellCrankPivot.Visible = true;
                            accordionControlMovingPointsFLPushRodBellCrank.Visible = true; accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFLPushRodUpright.Visible = true; accordionControlMovingPointsFLUpperBallJoint.Visible = true;
                            accordionControlMovingPointsFLDamperBellCrank.Text = "Damper Bell Crank";
                            #endregion
                        }
                        else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].McPhersonIdentifierFront == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has Mcpherson
                            accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                            accordionControlMovingPointsFLPushRodUpright.Visible = false;
                            A1xFL.Hide(); A1yFL.Hide(); A1zFL.Hide();
                            B1xFL.Hide(); B1yFL.Hide(); B1zFL.Hide();
                            I1xFL.Hide(); I1yFL.Hide(); I1zFL.Hide();
                            H1xFL.Hide(); H1yFL.Hide(); H1zFL.Hide();
                            G1xFL.Hide(); G1yFL.Hide(); G1zFL.Hide();
                            F1xFL.Hide(); F1yFL.Hide(); F1zFL.Hide();
                            O1xFL.Hide(); O1yFL.Hide(); O1zFL.Hide();
                            accordionControlFixedPointsFLUpperFrontChassis.Visible = false; accordionControlFixedPointsFLUpperRearChassis.Visible = false; accordionControlFixedPointsFLBellCrankPivot.Visible = false; // Removing irrelevant accordion control elements
                            accordionControlMovingPointsFLPushRodBellCrank.Visible = false; accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFLPushRodBellCrank.Visible = false; accordionControlMovingPointsFLUpperBallJoint.Visible = false;
                            accordionControlMovingPointsFLDamperBellCrank.Text = "Upper Ball Joint";
                            #endregion
                        }


                        if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].UARBIdentifierFront == 1)
                        {
                            accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                        }
                        else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].TARBIdentifierFront == 1)
                        {
                            accordionControlFixedPointsFLTorsionBarBottom.Visible = true;
                        }
                        #endregion

                        #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        A1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].A1x);
                        A1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].A1y);
                        A1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].A1z);

                        B1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].B1x);
                        B1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].B1y);
                        B1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].B1z);

                        C1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].C1x);
                        C1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].C1y);
                        C1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].C1z);

                        D1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].D1x);
                        D1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].D1y);
                        D1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].D1z);

                        E1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].E1x);
                        E1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].E1y);
                        E1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].E1z);

                        F1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].F1x);
                        F1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].F1y);
                        F1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].F1z);

                        G1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].G1x);
                        G1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].G1y);
                        G1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].G1z);

                        H1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].H1x);
                        H1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].H1y);
                        H1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].H1z);

                        I1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].I1x);
                        I1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].I1y);
                        I1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].I1z);

                        J1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].J1x);
                        J1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].J1y);
                        J1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].J1z);

                        JO1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].JO1x);
                        JO1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].JO1y);
                        JO1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].JO1z);

                        K1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].K1x);
                        K1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].K1y);
                        K1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].K1z);

                        M1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].M1x);
                        M1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].M1y);
                        M1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].M1z);

                        N1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].N1x);
                        N1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].N1y);
                        N1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].N1z);

                        O1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].O1x);
                        O1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].O1y);
                        O1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].O1z);

                        P1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].P1x);
                        P1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].P1y);
                        P1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].P1z);

                        Q1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].Q1x);
                        Q1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].Q1y);
                        Q1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].Q1z);

                        R1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].R1x);
                        R1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].R1y);
                        R1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].R1z);

                        W1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].W1x);
                        W1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].W1y);
                        W1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].W1z);

                        RideHeightRefFLx.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].RideHeightRefx);
                        RideHeightRefFLy.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].RideHeightRefy);
                        RideHeightRefFLz.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].RideHeightRefz);
                        #endregion


                    }
                    catch (Exception)
                    {
                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                    break;
                }
            }
        }
        #endregion

        void navBarItemSCFL_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            int index = navBarGroupSuspensionFL.SelectedLinkIndex;
            navBarGroupSuspensionFR.SelectedLinkIndex = navBarGroupSuspensionFL.SelectedLinkIndex;
            navBarItemSCFR_LinkClickedGUIEvent();
            SuspensionCoordinatesFront.SCFLCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[index]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[index]._RedocommandsSCFL, SuspensionCoordinatesFront.SCFLCurrentID, SuspensionCoordinatesFront.Assy_List_SCFL[index].SCFLIsModified); 
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSuspensionCoordinatesFL.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionFL.Expanded = true;
            accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlFixedPointsFL);
            accordionControlSuspensionCoordinatesFL.ExpandElement(accordionControlMovingPointsFL);
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

                        #region Delete
                        //#region GUI Operations depending on the Type of Suspension
                        //if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].DoubleWishboneIdentifierFront == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has Double Wishbone
                        //    accordionControlFixedPointsFLTorsionBarBottom.Visible = false;

                        //    A1xFL.Show(); A1yFL.Show(); A1zFL.Show();
                        //    B1xFL.Show(); B1yFL.Show(); B1zFL.Show();
                        //    I1xFL.Show(); I1yFL.Show(); I1zFL.Show();
                        //    H1xFL.Show(); H1yFL.Show(); H1zFL.Show();
                        //    G1xFL.Show(); G1yFL.Show(); G1zFL.Show();
                        //    F1xFL.Show(); F1yFL.Show(); F1zFL.Show();
                        //    O1xFL.Show(); O1yFL.Show(); O1zFL.Show();

                        //    accordionControlFixedPointsFLUpperFrontChassis.Visible = true; accordionControlFixedPointsFLUpperRearChassis.Visible = true; accordionControlFixedPointsFLBellCrankPivot.Visible = true;
                        //    accordionControlMovingPointsFLPushRodBellCrank.Visible = true; accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFLPushRodUpright.Visible = true; accordionControlMovingPointsFLUpperBallJoint.Visible = true;
                        //    accordionControlMovingPointsFLDamperBellCrank.Text = "Damper Bell Crank";
                        //    #endregion
                        //}
                        //else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].McPhersonIdentifierFront == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has Mcpherson
                        //    accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                        //    accordionControlMovingPointsFLPushRodUpright.Visible = false;
                        //    A1xFL.Hide(); A1yFL.Hide(); A1zFL.Hide();
                        //    B1xFL.Hide(); B1yFL.Hide(); B1zFL.Hide();
                        //    I1xFL.Hide(); I1yFL.Hide(); I1zFL.Hide();
                        //    H1xFL.Hide(); H1yFL.Hide(); H1zFL.Hide();
                        //    G1xFL.Hide(); G1yFL.Hide(); G1zFL.Hide();
                        //    F1xFL.Hide(); F1yFL.Hide(); F1zFL.Hide();
                        //    O1xFL.Hide(); O1yFL.Hide(); O1zFL.Hide();
                        //    accordionControlFixedPointsFLUpperFrontChassis.Visible = false; accordionControlFixedPointsFLUpperRearChassis.Visible = false; accordionControlFixedPointsFLBellCrankPivot.Visible = false; // Removing irrelevant accordion control elements
                        //    accordionControlMovingPointsFLPushRodBellCrank.Visible = false; accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFLPushRodBellCrank.Visible = false; accordionControlMovingPointsFLUpperBallJoint.Visible = false;
                        //    accordionControlMovingPointsFLDamperBellCrank.Text = "Upper Ball Joint";
                        //    #endregion
                        //}


                        //if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].UARBIdentifierFront == 1)
                        //{
                        //    accordionControlFixedPointsFLTorsionBarBottom.Visible = false;
                        //}
                        //else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].TARBIdentifierFront == 1)
                        //{
                        //    accordionControlFixedPointsFLTorsionBarBottom.Visible = true;
                        //}
                        //#endregion

                        //#region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        //A1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].A1x);
                        //A1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].A1y);
                        //A1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].A1z);

                        //B1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].B1x);
                        //B1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].B1y);
                        //B1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].B1z);

                        //C1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].C1x);
                        //C1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].C1y);
                        //C1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].C1z);

                        //D1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].D1x);
                        //D1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].D1y);
                        //D1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].D1z);

                        //E1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].E1x);
                        //E1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].E1y);
                        //E1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].E1z);

                        //F1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].F1x);
                        //F1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].F1y);
                        //F1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].F1z);

                        //G1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].G1x);
                        //G1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].G1y);
                        //G1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].G1z);

                        //H1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].H1x);
                        //H1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].H1y);
                        //H1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].H1z);

                        //I1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].I1x);
                        //I1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].I1y);
                        //I1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].I1z);

                        //J1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].J1x);
                        //J1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].J1y);
                        //J1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].J1z);

                        //JO1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].JO1x);
                        //JO1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].JO1y);
                        //JO1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].JO1z);

                        //K1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].K1x);
                        //K1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].K1y);
                        //K1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].K1z);

                        //M1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].M1x);
                        //M1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].M1y);
                        //M1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].M1z);

                        //N1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].N1x);
                        //N1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].N1y);
                        //N1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].N1z);

                        //O1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].O1x);
                        //O1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].O1y);
                        //O1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].O1z);

                        //P1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].P1x);
                        //P1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].P1y);
                        //P1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].P1z);

                        //Q1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].Q1x);
                        //Q1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].Q1y);
                        //Q1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].Q1z);

                        //R1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].R1x);
                        //R1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].R1y);
                        //R1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].R1z);

                        //W1xFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].W1x);
                        //W1yFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].W1y);
                        //W1zFL.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].W1z);

                        //RideHeightRefFLx.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].RideHeightRefx);
                        //RideHeightRefFLy.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].RideHeightRefy);
                        //RideHeightRefFLz.Text = Convert.ToString(SuspensionCoordinatesFront.Assy_List_SCFL[c_scfl].RideHeightRefz);
                        //#endregion 
                        #endregion


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
            comboBoxSCFL.Items.Clear();

            for (int i_combobox_scfl = 0; i_combobox_scfl < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_combobox_scfl++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of SCFL Objects
                    comboBoxSCFL.Items.Insert(i_combobox_scfl, SuspensionCoordinatesFront.Assy_List_SCFL[i_combobox_scfl]);
                    comboBoxSCFL.SelectedIndex = 0;
                    comboBoxSCFL.DisplayMember = "_SCName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }

        }
        #endregion

        //
        // Front Right Suspension Coordinate Item Creation and GUI
        //
        #region Front Right Suspension Coordinate Item Creation and GUI

        List<SuspensionCoordinatesFrontRightGUI> scfrGUI = new List<SuspensionCoordinatesFrontRightGUI>();

        private void BarButtonSCFR_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scfr = 0; i_scfr <= navBarItemSCFRClass.navBarItemSCFR.Count; i_scfr++)
            {
                if (SuspensionCoordinatesFrontRight.SCFRCounter == i_scfr)
                {
                    #region Creating a new NavBarItem and adding it to the Front Right Suspnesion Coordinates Group
                    navBarItemSCFRClass temp_navBarItemSCFR = new navBarItemSCFRClass();
                    navBarSCFR_Global.CreateNewNarBarItem(i_scfr, temp_navBarItemSCFR, navBarControl2, navBarGroupSuspensionFR);
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
                    if (DoubleWishboneFront_VehicleGUI==1)
                    {
                        Default_Values.FRONTRIGHTSuspensionDefaultValues.DoubleWishBone(this, scfrGUI[l_scfr]);
                    }
                    else if (McPhersonFront_VehicleGUI==1)
                    {
                        Default_Values.FRONTRIGHTSuspensionDefaultValues.McPherson(scfrGUI[l_scfr]);
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
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI = CustomBandedGridColumn.ColumnEditor_ForSuspension(scfrGUI[l_scfr].bandedGridView_SCFRGUI,this);
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI.CellValueChanged += new CellValueChangedEventHandler(bandedGridView_CellValueChanged);
                    scfrGUI[l_scfr].bandedGridView_SCFRGUI.OptionsNavigation.EnterMoveNextColumn = true;
                    #endregion

                    #region Populating the Undo/Redo Stack of the UndoRedo Class
                    UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFRIsModified); 
                    #endregion

                    break;
                }
            }

            SuspensionCoordinatesFrontRight.SCFRCounter++; // This is a static counter and it keeps track of the number of Chassis items created
            ComboBoxSCFROperations();
        }

        public void ModifyFrontRightSuspension(bool Copied_Identifier)
        {
            // Copied Identifier determines whether Front Right Coordinates have been copied or manually edited by the user. Based on its value, the CopyFrontRightTOFrontLeft function is called.
            // This prevents an infinite loop

            int index = navBarGroupSuspensionFR.SelectedLinkIndex;

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

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFRIsModified);
                        #endregion

                        #region Copying the Front Right Coordinates to the Front Left if symmetry is chosen
                        if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].FrontSymmetry == true && Copied_Identifier == false)
                        {
                            CopyFrontRightTOFrontLeft();
                        }
                        #endregion

                        break;
                    }
                    catch (Exception)
                    {// Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception}
                    }
                }
                ComboBoxSCFROperations();
            }
        }

        #region Delete
        public void SCFRTextBox_Leave(object sender, EventArgs e)
        {

        }

        private void SCFRTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //int index = navBarGroupSuspensionFR.SelectedLinkIndex;

            //#region GUI Operation to change thr color of the Text Box to white in case invalid user input is entered
            //A1xFR.BackColor = Color.White;
            //A1yFR.BackColor = Color.White;
            //A1zFR.BackColor = Color.White;

            //B1xFR.BackColor = Color.White;
            //B1yFR.BackColor = Color.White;
            //B1zFR.BackColor = Color.White;

            //C1xFR.BackColor = Color.White;
            //C1yFR.BackColor = Color.White;
            //C1zFR.BackColor = Color.White;

            //D1xFR.BackColor = Color.White;
            //D1yFR.BackColor = Color.White;
            //D1zFR.BackColor = Color.White;

            //E1xFR.BackColor = Color.White;
            //E1yFR.BackColor = Color.White;
            //E1zFR.BackColor = Color.White;

            //F1xFR.BackColor = Color.White;
            //F1yFR.BackColor = Color.White;
            //F1zFR.BackColor = Color.White;

            //G1xFR.BackColor = Color.White;
            //G1yFR.BackColor = Color.White;
            //G1zFR.BackColor = Color.White;

            //H1xFR.BackColor = Color.White;
            //H1yFR.BackColor = Color.White;
            //H1zFR.BackColor = Color.White;

            //I1xFR.BackColor = Color.White;
            //I1yFR.BackColor = Color.White;
            //I1zFR.BackColor = Color.White;

            //J1xFR.BackColor = Color.White;
            //J1yFR.BackColor = Color.White;
            //J1zFR.BackColor = Color.White;

            //JO1xFR.BackColor = Color.White;
            //JO1yFR.BackColor = Color.White;
            //JO1zFR.BackColor = Color.White;

            //K1xFR.BackColor = Color.White;
            //K1yFR.BackColor = Color.White;
            //K1zFR.BackColor = Color.White;

            //M1xFR.BackColor = Color.White;
            //M1yFR.BackColor = Color.White;
            //M1zFR.BackColor = Color.White;

            //N1xFR.BackColor = Color.White;
            //N1yFR.BackColor = Color.White;
            //N1zFR.BackColor = Color.White;

            //O1xFR.BackColor = Color.White;
            //O1yFR.BackColor = Color.White;
            //O1zFR.BackColor = Color.White;

            //P1xFR.BackColor = Color.White;
            //P1yFR.BackColor = Color.White;
            //P1zFR.BackColor = Color.White;

            //Q1xFR.BackColor = Color.White;
            //Q1yFR.BackColor = Color.White;
            //Q1zFR.BackColor = Color.White;

            //R1xFR.BackColor = Color.White;
            //R1yFR.BackColor = Color.White;
            //R1zFR.BackColor = Color.White;

            //W1xFR.BackColor = Color.White;
            //W1yFR.BackColor = Color.White;
            //W1zFR.BackColor = Color.White;

            //RideHeightRefFRx.BackColor = Color.White;
            //RideHeightRefFRy.BackColor = Color.White;
            //RideHeightRefFRz.BackColor = Color.White;
            //#endregion

            //if (e.KeyCode == Keys.Enter)
            //{

            //    for (int l_scfr = 0; l_scfr <= SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; l_scfr++)
            //    {
            //        if (index == l_scfr)
            //        {
            //            try
            //            {
            //                scfrGUI[l_scfr].EditFrontRightCoordinatesGUI(this);

            //                #region Editing the Object
            //                SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].EditFrontRightSuspension(l_scfr, scfrGUI[l_scfr]);
            //                SuspensionCoordinatesFrontRight.SCFRCurrentID = SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFR_ID;
            //                UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_scfr].SCFRIsModified);
            //                break;
            //                #endregion
            //            }
            //            catch (Exception)
            //            {

            //                // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
            //            }
            //        }
            //    }
            //    ComboBoxSCFROperations();
            //}

        } 
        #endregion

        #region This code is specifically added to handle the selection of the Front Right navbarItem when its corresponding Front Left navbarItem is selected
        public void navBarItemSCFR_LinkClickedGUIEvent()
        {
            int index = navBarGroupSuspensionFR.SelectedLinkIndex;

            for (int c_scfr = 0; c_scfr <= navBarItemSCFRClass.navBarItemSCFR.Count; c_scfr++)
            {
                if (index == c_scfr)
                {

                    try
                    {
                        #region GUI Operations base on the type of Suspension
                        if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].DoubleWishboneIdentifierFront == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has Double Wishbone
                            accordionControlFixedPointsFRTorsionBarBottom.Visible = false;

                            A1xFR.Show(); A1yFR.Show(); A1zFR.Show();
                            B1xFR.Show(); B1yFR.Show(); B1zFR.Show();
                            I1xFR.Show(); I1yFR.Show(); I1zFR.Show();
                            H1xFR.Show(); H1yFR.Show(); H1zFR.Show();
                            G1xFR.Show(); G1yFR.Show(); G1zFR.Show();
                            F1xFR.Show(); F1yFR.Show(); F1zFR.Show();
                            O1xFR.Show(); O1yFR.Show(); O1zFR.Show();

                            accordionControlFixedPointsFRUpperFrontChassis.Visible = true; accordionControlFixedPointsFRUpperRearChassis.Visible = true; accordionControlFixedPointsFRBellCrankPivot.Visible = true;
                            accordionControlMovingPointsFRPushRodBellCrank.Visible = true; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFRPushRodUpright.Visible = true; accordionControlMovingPointsFRUpperBallJoint.Visible = true;
                            accordionControlMovingPointsFRDamperBellCrank.Text = "Damper Bell Crank";
                            #endregion

                        }
                        else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].McPhersonIdentifierFront == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has McPherson
                            accordionControlFixedPointsFRTorsionBarBottom.Visible = false;
                            accordionControlMovingPointsFRPushRodUpright.Visible = false;

                            A1xFR.Hide(); A1yFR.Hide(); A1zFR.Hide(); A2xFL.Hide();
                            B1xFR.Hide(); B1yFR.Hide(); B1zFR.Hide(); B2xFL.Hide();
                            I1xFR.Hide(); I1yFR.Hide(); I1zFR.Hide(); I2xFL.Hide();
                            H1xFR.Hide(); H1yFR.Hide(); H1zFR.Hide(); H2xFL.Hide();
                            G1xFR.Hide(); G1yFR.Hide(); G1zFR.Hide(); G2xFL.Hide();
                            F1xFR.Hide(); F1yFR.Hide(); F1zFR.Hide(); F2xFL.Hide();
                            O1xFR.Hide(); O1yFR.Hide(); O1zFR.Hide(); O2xFL.Hide();

                            accordionControlFixedPointsFRUpperFrontChassis.Visible = false; accordionControlFixedPointsFRUpperRearChassis.Visible = false; accordionControlFixedPointsFRBellCrankPivot.Visible = false;
                            accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRUpperBallJoint.Visible = false;
                            accordionControlMovingPointsFRDamperBellCrank.Text = "Upper Ball Joint";
                            #endregion
                        }

                        if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].UARBIdentifierFront == 1)
                        {
                            accordionControlFixedPointsFRTorsionBarBottom.Visible = false;
                        }
                        else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].TARBIdentifierFront == 1)
                        {
                            accordionControlFixedPointsFRTorsionBarBottom.Visible = true;
                        }
                        #endregion

                        #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        A1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].A1x);
                        A1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].A1y);
                        A1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].A1z);

                        B1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].B1x);
                        B1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].B1y);
                        B1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].B1z);

                        C1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].C1x);
                        C1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].C1y);
                        C1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].C1z);

                        D1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].D1x);
                        D1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].D1y);
                        D1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].D1z);

                        E1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].E1x);
                        E1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].E1y);
                        E1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].E1z);

                        F1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].F1x);
                        F1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].F1y);
                        F1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].F1z);

                        G1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].G1x);
                        G1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].G1y);
                        G1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].G1z);

                        H1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].H1x);
                        H1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].H1y);
                        H1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].H1z);

                        I1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].I1x);
                        I1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].I1y);
                        I1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].I1z);

                        J1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].J1x);
                        J1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].J1y);
                        J1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].J1z);

                        JO1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].JO1x);
                        JO1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].JO1y);
                        JO1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].JO1z);

                        K1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].K1x);
                        K1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].K1y);
                        K1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].K1z);

                        M1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].M1x);
                        M1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].M1y);
                        M1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].M1z);

                        N1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].N1x);
                        N1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].N1y);
                        N1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].N1z);

                        O1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].O1x);
                        O1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].O1y);
                        O1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].O1z);

                        P1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].P1x);
                        P1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].P1y);
                        P1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].P1z);

                        Q1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].Q1x);
                        Q1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].Q1y);
                        Q1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].Q1z);

                        R1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].R1x);
                        R1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].R1y);
                        R1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].R1z);

                        W1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].W1x);
                        W1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].W1y);
                        W1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].W1z);

                        RideHeightRefFRx.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].RideHeightRefx);
                        RideHeightRefFRy.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].RideHeightRefy);
                        RideHeightRefFRz.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].RideHeightRefz);
                        #endregion

                    }
                    catch (Exception)
                    {

                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }
                }
            }

        }
        #endregion

        void navBarItemSCFR_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            int index = navBarGroupSuspensionFR.SelectedLinkIndex;
            navBarGroupSuspensionFL.SelectedLinkIndex = navBarGroupSuspensionFR.SelectedLinkIndex;
            navBarItemSCFL_LinkClickedGUIEvents();
            SuspensionCoordinatesFrontRight.SCFRCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[index]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[index]._RedocommandsSCFR, SuspensionCoordinatesFrontRight.SCFRCurrentID, SuspensionCoordinatesFrontRight.Assy_List_SCFR[index].SCFRIsModified); 
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSuspensionCoordinatesFR.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionFR.Expanded = true;
            accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlFixedPointsFR);
            accordionControlSuspensionCoordinatesFR.ExpandElement(accordionControlMovingPointsFR);
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

                        #region Delete
                        //#region GUI Operations base on the type of Suspension
                        //if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].DoubleWishboneIdentifierFront == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has Double Wishbone
                        //    accordionControlFixedPointsFRTorsionBarBottom.Visible = false;

                        //    A1xFR.Show(); A1yFR.Show(); A1zFR.Show();
                        //    B1xFR.Show(); B1yFR.Show(); B1zFR.Show();
                        //    I1xFR.Show(); I1yFR.Show(); I1zFR.Show();
                        //    H1xFR.Show(); H1yFR.Show(); H1zFR.Show();
                        //    G1xFR.Show(); G1yFR.Show(); G1zFR.Show();
                        //    F1xFR.Show(); F1yFR.Show(); F1zFR.Show();
                        //    O1xFR.Show(); O1yFR.Show(); O1zFR.Show();

                        //    accordionControlFixedPointsFRUpperFrontChassis.Visible = true; accordionControlFixedPointsFRUpperRearChassis.Visible = true; accordionControlFixedPointsFRBellCrankPivot.Visible = true;
                        //    accordionControlMovingPointsFRPushRodBellCrank.Visible = true; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFRPushRodUpright.Visible = true; accordionControlMovingPointsFRUpperBallJoint.Visible = true;
                        //    accordionControlMovingPointsFRDamperBellCrank.Text = "Damper Bell Crank";
                        //    #endregion

                        //}
                        //else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].McPhersonIdentifierFront == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has McPherson
                        //    accordionControlFixedPointsFRTorsionBarBottom.Visible = false;
                        //    accordionControlMovingPointsFRPushRodUpright.Visible = false;

                        //    A1xFR.Hide(); A1yFR.Hide(); A1zFR.Hide(); A2xFL.Hide();
                        //    B1xFR.Hide(); B1yFR.Hide(); B1zFR.Hide(); B2xFL.Hide();
                        //    I1xFR.Hide(); I1yFR.Hide(); I1zFR.Hide(); I2xFL.Hide();
                        //    H1xFR.Hide(); H1yFR.Hide(); H1zFR.Hide(); H2xFL.Hide();
                        //    G1xFR.Hide(); G1yFR.Hide(); G1zFR.Hide(); G2xFL.Hide();
                        //    F1xFR.Hide(); F1yFR.Hide(); F1zFR.Hide(); F2xFL.Hide();
                        //    O1xFR.Hide(); O1yFR.Hide(); O1zFR.Hide(); O2xFL.Hide();

                        //    accordionControlFixedPointsFRUpperFrontChassis.Visible = false; accordionControlFixedPointsFRUpperRearChassis.Visible = false; accordionControlFixedPointsFRBellCrankPivot.Visible = false;
                        //    accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRUpperBallJoint.Visible = false;
                        //    accordionControlMovingPointsFRDamperBellCrank.Text = "Upper Ball Joint";
                        //    #endregion
                        //}

                        //if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].UARBIdentifierFront == 1)
                        //{
                        //    accordionControlFixedPointsFRTorsionBarBottom.Visible = false;
                        //}
                        //else if (SuspensionCoordinatesFront.Assy_List_SCFL[c_scfr].TARBIdentifierFront == 1)
                        //{
                        //    accordionControlFixedPointsFRTorsionBarBottom.Visible = true;
                        //}
                        //#endregion

                        //#region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        //A1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].A1x);
                        //A1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].A1y);
                        //A1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].A1z);

                        //B1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].B1x);
                        //B1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].B1y);
                        //B1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].B1z);

                        //C1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].C1x);
                        //C1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].C1y);
                        //C1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].C1z);

                        //D1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].D1x);
                        //D1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].D1y);
                        //D1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].D1z);

                        //E1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].E1x);
                        //E1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].E1y);
                        //E1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].E1z);

                        //F1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].F1x);
                        //F1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].F1y);
                        //F1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].F1z);

                        //G1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].G1x);
                        //G1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].G1y);
                        //G1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].G1z);

                        //H1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].H1x);
                        //H1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].H1y);
                        //H1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].H1z);

                        //I1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].I1x);
                        //I1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].I1y);
                        //I1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].I1z);

                        //J1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].J1x);
                        //J1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].J1y);
                        //J1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].J1z);

                        //JO1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].JO1x);
                        //JO1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].JO1y);
                        //JO1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].JO1z);

                        //K1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].K1x);
                        //K1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].K1y);
                        //K1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].K1z);

                        //M1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].M1x);
                        //M1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].M1y);
                        //M1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].M1z);

                        //N1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].N1x);
                        //N1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].N1y);
                        //N1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].N1z);

                        //O1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].O1x);
                        //O1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].O1y);
                        //O1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].O1z);

                        //P1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].P1x);
                        //P1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].P1y);
                        //P1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].P1z);

                        //Q1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].Q1x);
                        //Q1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].Q1y);
                        //Q1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].Q1z);

                        //R1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].R1x);
                        //R1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].R1y);
                        //R1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].R1z);

                        //W1xFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].W1x);
                        //W1yFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].W1y);
                        //W1zFR.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].W1z);

                        //RideHeightRefFRx.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].RideHeightRefx);
                        //RideHeightRefFRy.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].RideHeightRefy);
                        //RideHeightRefFRz.Text = Convert.ToString(SuspensionCoordinatesFrontRight.Assy_List_SCFR[c_scfr].RideHeightRefz);
                        //#endregion 
                        #endregion

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
            comboBoxSCFR.Items.Clear();

            for (int i_combobox_scfr = 0; i_combobox_scfr < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_combobox_scfr++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of SuspensionCoordinateFrontRight Objects
                    comboBoxSCFR.Items.Insert(i_combobox_scfr, SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_combobox_scfr]);
                    comboBoxSCFR.SelectedIndex = 0;
                    comboBoxSCFR.DisplayMember = "_SCName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }

        }

        #endregion

        //
        //Rear Left Suspension Coordinate Item Creation and GUI
        //
        #region Rear Left Suspension Coordinate Item Creation and GUI

        List<SuspensionCoordinatesRearGUI> scrlGUI = new List<SuspensionCoordinatesRearGUI>();

        private void BarButtonSCRL_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scrl = 0; i_scrl <= navBarItemSCRLClass.navBarItemSCRL.Count; i_scrl++)
            {
                if (SuspensionCoordinatesRear.SCRLCounter == i_scrl)
                {
                    #region Creating a new NavBarItem and adding it to the AntiRollBar Group
                    navBarItemSCRLClass temp_navBarItemSCRL = new navBarItemSCRLClass();
                    navBarSCRL_Global.CreateNewNavBarItem(i_scrl, temp_navBarItemSCRL, navBarControl2, navBarGroupSuspensionRL);
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
                    if (DoubleWishboneRear_VehicleGUI == 1)
                    {
                        Default_Values.REARLEFTSuspensionDefaultValues.DoubleWishBone(this, scrlGUI[l_scrl]);
                    }
                    else if (McPhersonRear_VehicleGUI == 1)
                    {
                        Default_Values.REARLEFTSuspensionDefaultValues.McPherson(scrlGUI[l_scrl]);
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

                    break;
                }
            }

            SuspensionCoordinatesRear.SCRLCounter++; // This is a static counter and it keeps track of the number of SuspensionCoordinatesRear items created
            ComboboxSCRLOperations();

        }

        public void ModifyRearLeftSuspension(bool CopiedIdentifier)
        {
            // Copied Identifier determines whether Rear Left Coordinates have been copied or manually edited by the user. Based on its value, the CopyRearLeftTORearRight function is called.
            // This prevents an infinite loop

            int index = navBarGroupSuspensionRL.SelectedLinkIndex;

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

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl]._RedocommandsSCRL, SuspensionCoordinatesRear.SCRLCurrentID, SuspensionCoordinatesRear.Assy_List_SCRL[l_scrl].SCRLIsModified);
                        #endregion

                        #region Copying the Rear Left Coordinates to the Rear Right if symmetry is chosen
                        if (SuspensionCoordinatesRear.Assy_List_SCRL[index].RearSymmetry==true && CopiedIdentifier==false)
                        {
                            CopyRearLeftTOReaRight();
                        }
                        #endregion

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

        #region Delete
        public void SCRLTextBox_Leave(object sender, EventArgs e)
        {


        }

        private void SCRLTextBox_KeyDown(object sender, KeyEventArgs e)
        {


        } 
        #endregion

        #region This code is specifically added to handle the selection of the Rear Left navbarItem when its corresponding Rear Right navbarItem is selected
        public void navBarItemSCRL_LinkClickedGUIEvent()
        {
            int index = navBarGroupSuspensionRL.SelectedLinkIndex;

            for (int c_scrl = 0; c_scrl <= navBarItemSCRLClass.navBarItemSCRL.Count; c_scrl++)
            {
                if (index == c_scrl)
                {
                    try
                    {
                        #region GUI Operations based on Suspension Type
                        if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].DoubleWishboneIdentifierRear == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has Double Wishbone

                            accordionControlFixedPointsRLTorsionBarBottom.Visible = false;

                            A1xRL.Show(); A1yRL.Show(); A1zRL.Show();
                            B1xRL.Show(); B1yRL.Show(); B1zRL.Show();
                            I1xRL.Show(); I1yRL.Show(); I1zRL.Show();
                            H1xRL.Show(); H1yRL.Show(); H1zRL.Show();
                            G1xRL.Show(); G1yRL.Show(); G1zRL.Show();
                            F1xRL.Show(); F1yRL.Show(); F1zRL.Show();
                            O1xRL.Show(); O1yRL.Show(); O1zRL.Show();

                            accordionControlFixedPointsRLUpperFrontChassis.Visible = true; accordionControlFixedPointsRLUpperRearChassis.Visible = true; accordionControlFixedPointsRLBellCrankPivot.Visible = true;
                            accordionControlMovingPointsRLPushRodBellCrank.Visible = true; accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRLPushRodBellCrank.Visible = true; accordionControlMovingPointsRLUpperBallJoint.Visible = true;

                            accordionControlMovingPointsRLDamperBellCrank.Text = "Damper Bell Crank";

                            #endregion
                        }
                        else if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].McPhersonIdentifierRear == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has Mcpherson

                            accordionControlFixedPointsRLTorsionBarBottom.Visible = false;
                            accordionControlFixedPointsRRTorsionBarBottom.Visible = false;

                            A1xRL.Hide(); A1yRL.Hide(); A1zRL.Hide();
                            B1xRL.Hide(); B1yRL.Hide(); B1zRL.Hide();
                            I1xRL.Hide(); I1yRL.Hide(); I1zRL.Hide();
                            H1xRL.Hide(); H1yRL.Hide(); H1zRL.Hide();
                            G1xRL.Hide(); G1yRL.Hide(); G1zRL.Hide();
                            F1xRL.Hide(); F1yRL.Hide(); F1zRL.Hide();
                            O1xRL.Hide(); O1yRL.Hide(); O1zRL.Hide();

                            accordionControlFixedPointsRLUpperFrontChassis.Visible = false; accordionControlFixedPointsRLUpperRearChassis.Visible = false; accordionControlFixedPointsRLBellCrankPivot.Visible = false;
                            accordionControlMovingPointsRLPushRodBellCrank.Visible = false; accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRLPushRodBellCrank.Visible = false; accordionControlMovingPointsRLUpperBallJoint.Visible = false;

                            accordionControlMovingPointsRLDamperBellCrank.Text = "Upper Ball Joint";

                            #endregion
                        }


                        if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].UARBIdentifierRear == 1)
                        {
                            accordionControlFixedPointsRLTorsionBarBottom.Visible = false;

                        }
                        else if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].TARBIdentifierRear == 1)
                        {
                            accordionControlFixedPointsRLTorsionBarBottom.Visible = true;

                        }
                        #endregion


                        #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        A1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].A1x);
                        A1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].A1y);
                        A1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].A1z);

                        B1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].B1x);
                        B1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].B1y);
                        B1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].B1z);

                        C1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].C1x);
                        C1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].C1y);
                        C1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].C1z);

                        D1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].D1x);
                        D1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].D1y);
                        D1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].D1z);

                        E1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].E1x);
                        E1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].E1y);
                        E1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].E1z);

                        F1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].F1x);
                        F1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].F1y);
                        F1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].F1z);

                        G1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].G1x);
                        G1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].G1y);
                        G1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].G1z);

                        H1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].H1x);
                        H1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].H1y);
                        H1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].H1z);

                        I1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].I1x);
                        I1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].I1y);
                        I1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].I1z);

                        J1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].J1x);
                        J1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].J1y);
                        J1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].J1z);

                        JO1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].JO1x);
                        JO1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].JO1y);
                        JO1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].JO1z);

                        K1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].K1x);
                        K1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].K1y);
                        K1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].K1z);

                        M1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].M1x);
                        M1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].M1y);
                        M1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].M1z);

                        N1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].N1x);
                        N1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].N1y);
                        N1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].N1z);

                        O1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].O1x);
                        O1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].O1y);
                        O1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].O1z);

                        P1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].P1x);
                        P1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].P1y);
                        P1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].P1z);

                        Q1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].Q1x);
                        Q1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].Q1y);
                        Q1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].Q1z);

                        R1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].R1x);
                        R1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].R1y);
                        R1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].R1z);

                        W1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].W1x);
                        W1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].W1y);
                        W1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].W1z);

                        RideHeightRefRLx.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].RideHeightRefx);
                        RideHeightRefRLy.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].RideHeightRefy);
                        RideHeightRefRLz.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].RideHeightRefz);
                        #endregion
                    }
                    catch (Exception)
                    {
                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception

                    }

                }
            }
        }
        #endregion

        void navBarItemSCRL_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSuspensionRL.SelectedLinkIndex;
            navBarGroupSuspensionRR.SelectedLinkIndex = navBarGroupSuspensionRL.SelectedLinkIndex;
            navBarItemSCRR_LinkClickedGUIEvent();
            SuspensionCoordinatesRear.SCRLCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[index]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[index]._RedocommandsSCRL, SuspensionCoordinatesRear.SCRLCurrentID, SuspensionCoordinatesRear.Assy_List_SCRL[index].SCRLIsModified); 
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSuspensionCoordinatesRL.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionRL.Expanded = true;
            accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlFixedPointsRL);
            accordionControlSuspensionCoordinatesRL.ExpandElement(accordionControlMovingPointsRL);
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

                        #region Delete
                        //#region GUI Operations based on Suspension Type
                        //if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].DoubleWishboneIdentifierRear == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has Double Wishbone

                        //    accordionControlFixedPointsRLTorsionBarBottom.Visible = false;

                        //    A1xRL.Show(); A1yRL.Show(); A1zRL.Show();
                        //    B1xRL.Show(); B1yRL.Show(); B1zRL.Show();
                        //    I1xRL.Show(); I1yRL.Show(); I1zRL.Show();
                        //    H1xRL.Show(); H1yRL.Show(); H1zRL.Show();
                        //    G1xRL.Show(); G1yRL.Show(); G1zRL.Show();
                        //    F1xRL.Show(); F1yRL.Show(); F1zRL.Show();
                        //    O1xRL.Show(); O1yRL.Show(); O1zRL.Show();

                        //    accordionControlFixedPointsRLUpperFrontChassis.Visible = true; accordionControlFixedPointsRLUpperRearChassis.Visible = true; accordionControlFixedPointsRLBellCrankPivot.Visible = true;
                        //    accordionControlMovingPointsRLPushRodBellCrank.Visible = true; accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRLPushRodBellCrank.Visible = true; accordionControlMovingPointsRLUpperBallJoint.Visible = true;

                        //    accordionControlMovingPointsRLDamperBellCrank.Text = "Damper Bell Crank";

                        //    #endregion
                        //}
                        //else if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].McPhersonIdentifierRear == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has Mcpherson

                        //    accordionControlFixedPointsRLTorsionBarBottom.Visible = false;
                        //    accordionControlFixedPointsRRTorsionBarBottom.Visible = false;

                        //    A1xRL.Hide(); A1yRL.Hide(); A1zRL.Hide();
                        //    B1xRL.Hide(); B1yRL.Hide(); B1zRL.Hide();
                        //    I1xRL.Hide(); I1yRL.Hide(); I1zRL.Hide();
                        //    H1xRL.Hide(); H1yRL.Hide(); H1zRL.Hide();
                        //    G1xRL.Hide(); G1yRL.Hide(); G1zRL.Hide();
                        //    F1xRL.Hide(); F1yRL.Hide(); F1zRL.Hide();
                        //    O1xRL.Hide(); O1yRL.Hide(); O1zRL.Hide();

                        //    accordionControlFixedPointsRLUpperFrontChassis.Visible = false; accordionControlFixedPointsRLUpperRearChassis.Visible = false; accordionControlFixedPointsRLBellCrankPivot.Visible = false;
                        //    accordionControlMovingPointsRLPushRodBellCrank.Visible = false; accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRLPushRodBellCrank.Visible = false; accordionControlMovingPointsRLUpperBallJoint.Visible = false;

                        //    accordionControlMovingPointsRLDamperBellCrank.Text = "Upper Ball Joint";

                        //    #endregion
                        //}


                        //if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].UARBIdentifierRear == 1)
                        //{
                        //    accordionControlFixedPointsRLTorsionBarBottom.Visible = false;

                        //}
                        //else if (SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].TARBIdentifierRear == 1)
                        //{
                        //    accordionControlFixedPointsRLTorsionBarBottom.Visible = true;

                        //}
                        //#endregion


                        //#region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        //A1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].A1x);
                        //A1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].A1y);
                        //A1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].A1z);

                        //B1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].B1x);
                        //B1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].B1y);
                        //B1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].B1z);

                        //C1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].C1x);
                        //C1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].C1y);
                        //C1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].C1z);

                        //D1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].D1x);
                        //D1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].D1y);
                        //D1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].D1z);

                        //E1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].E1x);
                        //E1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].E1y);
                        //E1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].E1z);

                        //F1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].F1x);
                        //F1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].F1y);
                        //F1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].F1z);

                        //G1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].G1x);
                        //G1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].G1y);
                        //G1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].G1z);

                        //H1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].H1x);
                        //H1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].H1y);
                        //H1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].H1z);

                        //I1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].I1x);
                        //I1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].I1y);
                        //I1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].I1z);

                        //J1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].J1x);
                        //J1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].J1y);
                        //J1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].J1z);

                        //JO1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].JO1x);
                        //JO1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].JO1y);
                        //JO1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].JO1z);

                        //K1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].K1x);
                        //K1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].K1y);
                        //K1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].K1z);

                        //M1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].M1x);
                        //M1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].M1y);
                        //M1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].M1z);

                        //N1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].N1x);
                        //N1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].N1y);
                        //N1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].N1z);

                        //O1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].O1x);
                        //O1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].O1y);
                        //O1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].O1z);

                        //P1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].P1x);
                        //P1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].P1y);
                        //P1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].P1z);

                        //Q1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].Q1x);
                        //Q1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].Q1y);
                        //Q1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].Q1z);

                        //R1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].R1x);
                        //R1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].R1y);
                        //R1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].R1z);

                        //W1xRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].W1x);
                        //W1yRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].W1y);
                        //W1zRL.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].W1z);

                        //RideHeightRefRLx.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].RideHeightRefx);
                        //RideHeightRefRLy.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].RideHeightRefy);
                        //RideHeightRefRLz.Text = Convert.ToString(SuspensionCoordinatesRear.Assy_List_SCRL[c_scrl].RideHeightRefz);
                        //#endregion 
                        #endregion
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
            comboBoxSCRL.Items.Clear();

            for (int i_combobox_scrl = 0; i_combobox_scrl < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_combobox_scrl++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of ARB Objects
                    comboBoxSCRL.Items.Insert(i_combobox_scrl, SuspensionCoordinatesRear.Assy_List_SCRL[i_combobox_scrl]);
                    comboBoxSCRL.SelectedIndex = 0;
                    comboBoxSCRL.DisplayMember = "_SCName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        //
        //Rear Right Suspension Coordinate Item Creation and GUI
        //
        #region Rear Right Suspension Coordinate Item Creation and GUI

        List<SuspensionCoordinatesRearRightGUI> scrrGUI = new List<SuspensionCoordinatesRearRightGUI>();

        private void BarButtonSCRR_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_scrr = 0; i_scrr <= navBarItemSCRRClass.navBarItemSCRR.Count; i_scrr++)
            {
                if (SuspensionCoordinatesRearRight.SCRRCounter == i_scrr)
                {
                    #region Creating a new NavBarItem and adding it to the SuspensionCoordinatesRearRight Group
                    navBarItemSCRRClass temp_navBarItemSCRR = new navBarItemSCRRClass();
                    navBarSCRR_Global.CreateNewNarBarItem(i_scrr, temp_navBarItemSCRR, navBarControl2, navBarGroupSuspensionRR);
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
                    if (DoubleWishboneRear_VehicleGUI == 1)
                    {
                        Default_Values.REARRIGHTSuspensionDefaultValues.DoubleWishBone(this, scrrGUI[l_scrr]);
                    }
                    else if (McPhersonRear_VehicleGUI == 1)
                    {
                        Default_Values.REARRIGHTSuspensionDefaultValues.McPherson(scrrGUI[l_scrr]);
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

                    break;
                }
            }

            SuspensionCoordinatesRearRight.SCRRCounter++; // This is a static counter and it keeps track of the number of SuspensionCoordinatesRearRight items created
            ComboboxSCRROperations();

        }

        public void ModifyRearRightSuspension(bool CopiedIdentifier)
        {
            // Copied Identifier determines whether Rear Right Coordinates have been copied or manually edited by the user. Based on its value, the CopyRearRightTORearLeft function is called.
            // This prevents an infinite loop

            int index = navBarGroupSuspensionRR.SelectedLinkIndex;

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

                        #region Populating the Undo/Redo Stack of the UndoRedo Class
                        UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRRIsModified);
                        #endregion

                        #region Copying the Rear Right Coordinates to the Rear Left if symmetry is chosen
                        if (SuspensionCoordinatesRearRight.Assy_List_SCRR[index].RearSymmetry == true && CopiedIdentifier == false)
                        {
                            CopyRearRightTORearLeft();
                        }
                        #endregion

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

        #region Delete
        public void SCRRTextBox_Leave(object sender, EventArgs e)
        {


        }

        private void SCRRTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //int index = navBarGroupSuspensionRR.SelectedLinkIndex;


            //#region GUI Operation to change thr color of the Text Box to white in case invalid user input is entered
            //A1xRR.BackColor = Color.White;
            //A1yRR.BackColor = Color.White;
            //A1zRR.BackColor = Color.White;

            //B1xRR.BackColor = Color.White;
            //B1yRR.BackColor = Color.White;
            //B1zRR.BackColor = Color.White;

            //C1xRR.BackColor = Color.White;
            //C1yRR.BackColor = Color.White;
            //C1zRR.BackColor = Color.White;

            //D1xRR.BackColor = Color.White;
            //D1yRR.BackColor = Color.White;
            //D1zRR.BackColor = Color.White;

            //E1xRR.BackColor = Color.White;
            //E1yRR.BackColor = Color.White;
            //E1zRR.BackColor = Color.White;

            //F1xRR.BackColor = Color.White;
            //F1yRR.BackColor = Color.White;
            //F1zRR.BackColor = Color.White;

            //G1xRR.BackColor = Color.White;
            //G1yRR.BackColor = Color.White;
            //G1zRR.BackColor = Color.White;

            //H1xRR.BackColor = Color.White;
            //H1yRR.BackColor = Color.White;
            //H1zRR.BackColor = Color.White;

            //I1xRR.BackColor = Color.White;
            //I1yRR.BackColor = Color.White;
            //I1zRR.BackColor = Color.White;

            //J1xRR.BackColor = Color.White;
            //J1yRR.BackColor = Color.White;
            //J1zRR.BackColor = Color.White;

            //JO1xRR.BackColor = Color.White;
            //JO1yRR.BackColor = Color.White;
            //JO1zRR.BackColor = Color.White;

            //K1xRR.BackColor = Color.White;
            //K1yRR.BackColor = Color.White;
            //K1zRR.BackColor = Color.White;

            //M1xRR.BackColor = Color.White;
            //M1yRR.BackColor = Color.White;
            //M1zRR.BackColor = Color.White;

            //N1xRR.BackColor = Color.White;
            //N1yRR.BackColor = Color.White;
            //N1zRR.BackColor = Color.White;

            //O1xRR.BackColor = Color.White;
            //O1yRR.BackColor = Color.White;
            //O1zRR.BackColor = Color.White;

            //P1xRR.BackColor = Color.White;
            //P1yRR.BackColor = Color.White;
            //P1zRR.BackColor = Color.White;

            //Q1xRR.BackColor = Color.White;
            //Q1yRR.BackColor = Color.White;
            //Q1zRR.BackColor = Color.White;

            //R1xRR.BackColor = Color.White;
            //R1yRR.BackColor = Color.White;
            //R1zRR.BackColor = Color.White;

            //W1xRR.BackColor = Color.White;
            //W1yRR.BackColor = Color.White;
            //W1zRR.BackColor = Color.White;

            //RideHeightRefRRx.BackColor = Color.White;
            //RideHeightRefRRy.BackColor = Color.White;
            //RideHeightRefRRz.BackColor = Color.White;
            //#endregion

            //if (e.KeyCode == Keys.Enter)
            //{
            //    for (int l_scrr = 0; l_scrr <= SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; l_scrr++)
            //    {
            //        if (index == l_scrr)
            //        {
            //            try
            //            {
            //                scrrGUI[l_scrr].EditRearSuspensionGUI(this);

            //                #region Editing the object
            //                SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].EditRearSuspension(l_scrr, scrrGUI[l_scrr]);
            //                SuspensionCoordinatesRearRight.SCRRCurrentID = SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRR_ID;
            //                UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_scrr].SCRRIsModified);
            //                break;
            //                #endregion

            //            }
            //            catch (Exception)
            //            {

            //                // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
            //            }
            //        }
            //    }
            //    ComboboxSCRROperations();
            //}

        } 
        #endregion

        #region This code is specifically added to handle the selection of the Rear Right navbarItem when its corresponding Rear Left navbarItem is selected
        public void navBarItemSCRR_LinkClickedGUIEvent()
        {
            int index = navBarGroupSuspensionRR.SelectedLinkIndex;

            for (int c_scrr = 0; c_scrr <= navBarItemSCRRClass.navBarItemSCRR.Count; c_scrr++)
            {
                if (index == c_scrr)
                {

                    try
                    {
                        #region GUI Operations base on the type of Suspension
                        if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].DoubleWishboneIdentifierRear == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has Double Wishbone

                            accordionControlFixedPointsRRTorsionBarBottom.Visible = false;

                            A1xRR.Show(); A1yRR.Show(); A1zRR.Show();
                            B1xRR.Show(); B1yRR.Show(); B1zRR.Show();
                            I1xRR.Show(); I1yRR.Show(); I1zRR.Show();
                            H1xRR.Show(); H1yRR.Show(); H1zRR.Show();
                            G1xRR.Show(); G1yRR.Show(); G1zRR.Show();
                            F1xRR.Show(); F1yRR.Show(); F1zRR.Show();
                            O1xRR.Show(); O1yRR.Show(); O1zRR.Show();

                            accordionControlFixedPointsRRUpperFrontChassis.Visible = true; accordionControlFixedPointsRRUpperRearChassis.Visible = true; accordionControlFixedPointsRRBellCrankPivot.Visible = true;
                            accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRUpperBallJoint.Visible = true;

                            accordionControlMovingPointsRRDamperBellCrank.Text = "Damper Bell Crank";

                            #endregion

                        }
                        else if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].McPhersonIdentifierRear == 1)
                        {
                            #region GUI operations for when the Suspension Coordinate has McPherson

                            accordionControlFixedPointsRRTorsionBarBottom.Visible = false;
                            accordionControlMovingPointsRRPushRodUpright.Visible = false;

                            A1xRR.Hide(); A1yRR.Hide(); A1zRR.Hide();
                            B1xRR.Hide(); B1yRR.Hide(); B1zRR.Hide();
                            I1xRR.Hide(); I1yRR.Hide(); I1zRR.Hide();
                            H1xRR.Hide(); H1yRR.Hide(); H1zRR.Hide();
                            G1xRR.Hide(); G1yRR.Hide(); G1zRR.Hide();
                            F1xRR.Hide(); F1yRR.Hide(); F1zRR.Hide();
                            O1xRR.Hide(); O1yRR.Hide(); O1zRR.Hide();

                            accordionControlFixedPointsRRUpperFrontChassis.Visible = false; accordionControlFixedPointsRRUpperRearChassis.Visible = false; accordionControlFixedPointsRRBellCrankPivot.Visible = false;
                            accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRUpperBallJoint.Visible = false;

                            accordionControlMovingPointsRRDamperBellCrank.Text = "Upper Ball Joint";

                            #endregion
                        }

                        if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].UARBIdentifierRear == 1)
                        {
                            accordionControlFixedPointsRRTorsionBarBottom.Visible = false;
                        }
                        else if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].TARBIdentifierRear == 1)
                        {
                            accordionControlFixedPointsRRTorsionBarBottom.Visible = true;
                        }
                        #endregion


                        #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        A1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].A1x);
                        A1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].A1y);
                        A1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].A1z);

                        B1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].B1x);
                        B1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].B1y);
                        B1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].B1z);

                        C1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].C1x);
                        C1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].C1y);
                        C1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].C1z);

                        D1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].D1x);
                        D1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].D1y);
                        D1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].D1z);

                        E1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].E1x);
                        E1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].E1y);
                        E1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].E1z);

                        F1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].F1x);
                        F1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].F1y);
                        F1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].F1z);

                        G1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].G1x);
                        G1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].G1y);
                        G1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].G1z);

                        H1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].H1x);
                        H1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].H1y);
                        H1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].H1z);

                        I1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].I1x);
                        I1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].I1y);
                        I1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].I1z);

                        J1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].J1x);
                        J1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].J1y);
                        J1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].J1z);

                        JO1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].JO1x);
                        JO1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].JO1y);
                        JO1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].JO1z);

                        K1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].K1x);
                        K1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].K1y);
                        K1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].K1z);

                        M1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].M1x);
                        M1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].M1y);
                        M1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].M1z);

                        N1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].N1x);
                        N1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].N1y);
                        N1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].N1z);

                        O1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].O1x);
                        O1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].O1y);
                        O1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].O1z);

                        P1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].P1x);
                        P1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].P1y);
                        P1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].P1z);

                        Q1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].Q1x);
                        Q1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].Q1y);
                        Q1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].Q1z);

                        R1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].R1x);
                        R1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].R1y);
                        R1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].R1z);

                        W1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].W1x);
                        W1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].W1y);
                        W1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].W1z);

                        RideHeightRefRRx.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].RideHeightRefx);
                        RideHeightRefRRy.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].RideHeightRefy);
                        RideHeightRefRRz.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].RideHeightRefz);
                        #endregion
                    }
                    catch (Exception)
                    {

                        // Added here so that the COPY COORDINATES button when pressed without creation of the Front Left Coordinate item doesn't create an exception
                    }

                }
            }
        }
        #endregion

        void navBarItemSCRR_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSuspensionRR.SelectedLinkIndex;
            navBarGroupSuspensionRL.SelectedLinkIndex = navBarGroupSuspensionRR.SelectedLinkIndex;
            navBarItemSCRL_LinkClickedGUIEvent();
            SuspensionCoordinatesRearRight.SCRRCurrentID = index + 1;

            #region Populating the Undo/Redo Stack of the UndoRedo Class
            UndoObject.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[index]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[index]._RedocommandsSCRR, SuspensionCoordinatesRearRight.SCRRCurrentID, SuspensionCoordinatesRearRight.Assy_List_SCRR[index].SCRRIsModified); 
            #endregion


            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            tabPaneResults.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSuspensionCoordinatesRR.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionRR.Expanded = true;
            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlFixedPointsRR);
            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlMovingPointsRR);
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

                        #region Delete
                        //#region GUI Operations base on the type of Suspension
                        //if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].DoubleWishboneIdentifierRear == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has Double Wishbone

                        //    accordionControlFixedPointsRRTorsionBarBottom.Visible = false;

                        //    A1xRR.Show(); A1yRR.Show(); A1zRR.Show();
                        //    B1xRR.Show(); B1yRR.Show(); B1zRR.Show();
                        //    I1xRR.Show(); I1yRR.Show(); I1zRR.Show();
                        //    H1xRR.Show(); H1yRR.Show(); H1zRR.Show();
                        //    G1xRR.Show(); G1yRR.Show(); G1zRR.Show();
                        //    F1xRR.Show(); F1yRR.Show(); F1zRR.Show();
                        //    O1xRR.Show(); O1yRR.Show(); O1zRR.Show();

                        //    accordionControlFixedPointsRRUpperFrontChassis.Visible = true; accordionControlFixedPointsRRUpperRearChassis.Visible = true; accordionControlFixedPointsRRBellCrankPivot.Visible = true;
                        //    accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRUpperBallJoint.Visible = true;

                        //    accordionControlMovingPointsRRDamperBellCrank.Text = "Damper Bell Crank";

                        //    #endregion

                        //}
                        //else if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].McPhersonIdentifierRear == 1)
                        //{
                        //    #region GUI operations for when the Suspension Coordinate has McPherson

                        //    accordionControlFixedPointsRRTorsionBarBottom.Visible = false;
                        //    accordionControlMovingPointsRRPushRodUpright.Visible = false;

                        //    A1xRR.Hide(); A1yRR.Hide(); A1zRR.Hide();
                        //    B1xRR.Hide(); B1yRR.Hide(); B1zRR.Hide();
                        //    I1xRR.Hide(); I1yRR.Hide(); I1zRR.Hide();
                        //    H1xRR.Hide(); H1yRR.Hide(); H1zRR.Hide();
                        //    G1xRR.Hide(); G1yRR.Hide(); G1zRR.Hide();
                        //    F1xRR.Hide(); F1yRR.Hide(); F1zRR.Hide();
                        //    O1xRR.Hide(); O1yRR.Hide(); O1zRR.Hide();

                        //    accordionControlFixedPointsRRUpperFrontChassis.Visible = false; accordionControlFixedPointsRRUpperRearChassis.Visible = false; accordionControlFixedPointsRRBellCrankPivot.Visible = false;
                        //    accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRUpperBallJoint.Visible = false;

                        //    accordionControlMovingPointsRRDamperBellCrank.Text = "Upper Ball Joint";

                        //    #endregion
                        //}

                        //if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].UARBIdentifierRear == 1)
                        //{
                        //    accordionControlFixedPointsRRTorsionBarBottom.Visible = false;
                        //}
                        //else if (SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].TARBIdentifierRear == 1)
                        //{
                        //    accordionControlFixedPointsRRTorsionBarBottom.Visible = true;
                        //}
                        //#endregion


                        //#region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                        //A1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].A1x);
                        //A1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].A1y);
                        //A1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].A1z);

                        //B1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].B1x);
                        //B1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].B1y);
                        //B1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].B1z);

                        //C1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].C1x);
                        //C1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].C1y);
                        //C1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].C1z);

                        //D1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].D1x);
                        //D1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].D1y);
                        //D1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].D1z);

                        //E1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].E1x);
                        //E1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].E1y);
                        //E1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].E1z);

                        //F1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].F1x);
                        //F1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].F1y);
                        //F1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].F1z);

                        //G1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].G1x);
                        //G1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].G1y);
                        //G1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].G1z);

                        //H1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].H1x);
                        //H1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].H1y);
                        //H1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].H1z);

                        //I1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].I1x);
                        //I1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].I1y);
                        //I1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].I1z);

                        //J1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].J1x);
                        //J1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].J1y);
                        //J1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].J1z);

                        //JO1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].JO1x);
                        //JO1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].JO1y);
                        //JO1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].JO1z);

                        //K1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].K1x);
                        //K1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].K1y);
                        //K1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].K1z);

                        //M1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].M1x);
                        //M1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].M1y);
                        //M1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].M1z);

                        //N1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].N1x);
                        //N1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].N1y);
                        //N1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].N1z);

                        //O1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].O1x);
                        //O1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].O1y);
                        //O1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].O1z);

                        //P1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].P1x);
                        //P1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].P1y);
                        //P1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].P1z);

                        //Q1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].Q1x);
                        //Q1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].Q1y);
                        //Q1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].Q1z);

                        //R1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].R1x);
                        //R1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].R1y);
                        //R1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].R1z);

                        //W1xRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].W1x);
                        //W1yRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].W1y);
                        //W1zRR.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].W1z);

                        //RideHeightRefRRx.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].RideHeightRefx);
                        //RideHeightRefRRy.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].RideHeightRefy);
                        //RideHeightRefRRz.Text = Convert.ToString(SuspensionCoordinatesRearRight.Assy_List_SCRR[c_scrr].RideHeightRefz);
                        //#endregion 
                        #endregion
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
            comboBoxSCRR.Items.Clear();

            for (int i_combobox_scrr = 0; i_combobox_scrr < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_combobox_scrr++)
            {
                try
                {
                    #region Populating the Comboboxes with the list of SuspensionCoordinatesRearRight Objects
                    comboBoxSCRR.Items.Insert(i_combobox_scrr, SuspensionCoordinatesRearRight.Assy_List_SCRR[i_combobox_scrr]);
                    comboBoxSCRR.SelectedIndex = 0;
                    comboBoxSCRR.DisplayMember = "_SCName";

                    #endregion
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        //
        // Vehicle Item Creation and GUI
        //
        #region Vehicle Item Creation and GUI

        private void barButtonVehicleItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            groupControl13.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            navBarGroupDesign.Visible = true;
            accordionControlVehicleItem.Show();
            accordionControlVehicleItem.BringToFront();
            navBarGroupVehicle.Expanded = true;
            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            for (int i_vehicle = 0; i_vehicle <= navBarItemVehicleClass.navBarItemVehicle.Count; i_vehicle++)
            {
                if (Vehicle.VehicleCounter == i_vehicle)
                {
                    #region Creating a new NavBarItem and adding it to the Vehicle Group
                    navBarItemVehicleClass temp_navBarItemVehicle = new navBarItemVehicleClass();
                    navBarVehicle_Global.CreateNewNavBarItem(i_vehicle, temp_navBarItemVehicle, navBarControl2, navBarGroupVehicle);
                    navBarItemVehicleClass.navBarItemVehicle[i_vehicle].LinkClicked += new NavBarLinkEventHandler(navBarItemVehicle_LinkClicked);
                    break;
                    #endregion
                }
            }
            for (int l_vehicle = 0; l_vehicle <= Vehicle.List_Vehicle.Count; l_vehicle++)
            {
                if (Vehicle.VehicleCounter == l_vehicle)
                {
                    try
                    {
                        #region Assembling the Vehicle and creating a Vehicle Item
                        double Assy_Checker = 0; //  This variable is used to check if all the components of the Vehicle have been assembled. 
                        InputSheet I1 = new InputSheet(this);

                        //
                        //Suspension Assembly
                        //
                        #region Suspension Assembly
                        if ((comboBoxSCFL.SelectedItem != null) && (comboBoxSCFR.SelectedItem != null) && (comboBoxSCRL.SelectedItem != null) && (comboBoxSCRR.SelectedItem != null))
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

                            #endregion

                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Suspension Pick Up Points have not been assembled");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();

                            return;

                        }
                        #endregion

                        //
                        //Tire Assembly
                        //
                        #region Tire Assembly
                        if ((comboBoxTireFL.SelectedItem != null) && (comboBoxTireFR.SelectedItem != null) && (comboBoxTireRL.SelectedItem != null) && (comboBoxTireRR.SelectedItem != null))
                        {
                            #region Assembling the Tires which User has selected
                            Tire.Assy_Tire[0] = (Tire)comboBoxTireFL.SelectedItem;


                            Tire.Assy_Tire[1] = (Tire)comboBoxTireFR.SelectedItem;


                            Tire.Assy_Tire[2] = (Tire)comboBoxTireRL.SelectedItem;


                            Tire.Assy_Tire[3] = (Tire)comboBoxTireRR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Tires have not been assembled");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();
                            return;
                        }
                        #endregion

                        //
                        //Spring Assembly
                        //
                        #region Spring Assembly
                        if ((comboBoxSpringFL.SelectedItem != null) && (comboBoxSpringFR.SelectedItem != null) && (comboBoxSpringRL.SelectedItem != null) && (comboBoxSpringRR.SelectedItem != null))
                        {
                            #region Assembling the Springs which the User has Selected
                            Spring.Assy_Spring[0] = (Spring)comboBoxSpringFL.SelectedItem;


                            Spring.Assy_Spring[1] = (Spring)comboBoxSpringFR.SelectedItem;


                            Spring.Assy_Spring[2] = (Spring)comboBoxSpringRL.SelectedItem;


                            Spring.Assy_Spring[3] = (Spring)comboBoxSpringRR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Springs have not been assembled");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();
                            return;
                        }
                        #endregion

                        //
                        //Damper Assembly
                        //
                        #region Damper Assembly
                        if ((comboBoxDamperFL.SelectedItem != null) && (comboBoxDamperFR.SelectedItem != null) && (comboBoxDamperRL.SelectedItem != null) && (comboBoxDamperRR.SelectedItem != null))
                        {
                            #region Assembling the Dampers which the user has selected
                            Damper.Assy_Damper[0] = (Damper)comboBoxDamperFL.SelectedItem;


                            Damper.Assy_Damper[1] = (Damper)comboBoxDamperFR.SelectedItem;


                            Damper.Assy_Damper[2] = (Damper)comboBoxDamperRL.SelectedItem;


                            Damper.Assy_Damper[3] = (Damper)comboBoxDamperRR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Dampers have not been assembled");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();
                            return;
                        }
                        #endregion

                        //
                        //Anti-Roll Bar Assembly
                        //
                        #region Anti-Roll Bar Assembly
                        if (comboBoxARBFront.SelectedItem != null)
                        {
                            #region Assembling the Anti-Roll Bars which the user has selected
                            AntiRollBar.Assy_ARB[0] = (AntiRollBar)comboBoxARBFront.SelectedItem;
                            AntiRollBar.Assy_ARB[1] = (AntiRollBar)comboBoxARBFront.SelectedItem;

                            #endregion

                            Assy_Checker += 0.5;
                        }
                        else if (comboBoxARBFront.SelectedItem == null)
                        {
                            DialogResult result = MessageBox.Show("Run Simulation without Front Anti-Roll Bar?");
                            if (result == DialogResult.OK)
                            {
                                arbGUI.Insert(l_vehicle, new AntiRollBarGUI());
                                Default_Values.ARBDefaultValues2(arbGUI[l_vehicle]);
                                arbGUI[l_vehicle].Update_ARBGUI(this, l_vehicle);

                                AntiRollBar.Assy_ARB[0] = new AntiRollBar(arbGUI[l_vehicle]);
                                AntiRollBar.Assy_ARB[0].AntiRollBarRate = 0;

                                AntiRollBar.Assy_ARB[1] = new AntiRollBar(arbGUI[l_vehicle]);
                                AntiRollBar.Assy_ARB[1].AntiRollBarRate = 0;
                                Assy_Checker += 0.5;
                            }
                        }
                        if (comboBoxARBRear.SelectedItem != null)
                        {
                            #region Assembling the Rear Anti-Roll Bar whcih the user has selected
                            AntiRollBar.Assy_ARB[2] = (AntiRollBar)comboBoxARBRear.SelectedItem;
                            AntiRollBar.Assy_ARB[3] = (AntiRollBar)comboBoxARBRear.SelectedItem;

                            #endregion
                            Assy_Checker += 0.5;

                        }
                        else if (comboBoxARBRear.SelectedItem == null)
                        {
                            DialogResult result = MessageBox.Show("Run Simulation without Rear Anti-Roll Bar?");
                            if (result == DialogResult.OK)
                            {
                                arbGUI.Insert(l_vehicle, new AntiRollBarGUI());
                                Default_Values.ARBDefaultValues2(arbGUI[l_vehicle]);
                                arbGUI[l_vehicle].Update_ARBGUI(this, l_vehicle);

                                AntiRollBar.Assy_ARB[2] = new AntiRollBar(arbGUI[l_vehicle]);
                                AntiRollBar.Assy_ARB[2].AntiRollBarRate = 0;

                                AntiRollBar.Assy_ARB[3] = new AntiRollBar(arbGUI[l_vehicle]);
                                AntiRollBar.Assy_ARB[3].AntiRollBarRate = 0;
                                Assy_Checker += 0.5;
                            }
                        }

                        #endregion

                        //
                        //Chassis Assembly
                        //
                        #region Chassis Assembly
                        if (comboBoxChassis.SelectedItem != null)
                        {
                            #region Assembling the Chassis which the user has selected
                            Chassis.Assy_Chassis = (Chassis)comboBoxChassis.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Chassis has not been assembled");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();
                            return;
                        }
                        #endregion

                        //
                        //WheelAlignment Assembly
                        //
                        #region WheelAlignment Assembly
                        if ((comboBoxWAFL.SelectedItem != null) && (comboBoxWAFR.SelectedItem != null) && (comboBoxWARL.SelectedItem != null) && (comboBoxWARR.SelectedItem != null))
                        {
                            #region Assembling the Wheel Alignment which the user has selected
                            WheelAlignment.Assy_WA[0] = (WheelAlignment)comboBoxWAFL.SelectedItem;


                            WheelAlignment.Assy_WA[1] = (WheelAlignment)comboBoxWAFR.SelectedItem;


                            WheelAlignment.Assy_WA[2] = (WheelAlignment)comboBoxWARL.SelectedItem;


                            WheelAlignment.Assy_WA[3] = (WheelAlignment)comboBoxWARR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Wheel Alignment has not been set");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();
                            return;
                        }
                        #endregion

                        //
                        // Output Class Assembly
                        //
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
                        #endregion


                        //
                        // Passing the local Input Sheet Object to the Global List of Input Sheet
                        //
                        M1_Global.List_I1.Insert(l_vehicle, I1);
                        InputSheet.InputSheetCounter++;

                        //
                        // Checking if all the Input Parameters have been assembled
                        //
                        #region Checking if all Input Parameters have been assembled
                        if (Assy_Checker == 7)
                        {
                            //
                            // Passing the parameters to VEHICLE ASSEMBLY method where a new Vehicle Object will be Initialized. This object will then be returned using an out parameter of type Vehicle 
                            //
                            Vehicle vehicle_list;
                            VehicleAssembly(M1_Global.Assy_SCM, Tire.Assy_Tire, Spring.Assy_Spring, Damper.Assy_Damper, AntiRollBar.Assy_ARB, Chassis.Assy_Chassis, WheelAlignment.Assy_WA, M1_Global.Assy_OC, out vehicle_list);
                            V1_Global = vehicle_list;
                            V1_Global.CreateNewVehicle(l_vehicle, vehicle_list);
                            Vehicle.CurrentVehicleID = Vehicle.List_Vehicle[l_vehicle].VehicleID;
                            UndoObject.Identifier(Vehicle.List_Vehicle[l_vehicle]._UndocommandsVehicle, Vehicle.List_Vehicle[l_vehicle]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[l_vehicle].VehicleIsModified);
                            MessageBox.Show(Vehicle.List_Vehicle[l_vehicle]._VehicleName + " has been created. Calculations can now be done using the Calculate Resuls button in the Results Tab on top.");

                            break;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle Not Assembled Properly. Please re-check your Vehicle Item");

                            navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                            navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                            sidePanel2.Hide();
                            return;
                        }
                        #endregion
                        #endregion
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unexpected Error during Vehicle Assembly");

                        navBarGroupVehicle.ItemLinks.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                        navBarControl2.Items.Remove(navBarItemVehicleClass.navBarItemVehicle[l_vehicle]);
                        navBarItemVehicleClass.navBarItemVehicle.RemoveAt(l_vehicle);

                        sidePanel2.Hide();
                    }

                }
            }
            Vehicle.VehicleCounter++; // This is a static counter which keeps track of the number of Vehicle Items created
            ComboboxVehicleOperations();
            PopulateInputSheet();

        }



        private void InputComboboxes_Leave(object sender, EventArgs e)
        {

            int index = navBarGroupVehicle.SelectedLinkIndex;

            for (int l_vehicle = 0; l_vehicle <= Vehicle.List_Vehicle.Count; l_vehicle++)
            {
                if (index == l_vehicle)
                {
                    try
                    {
                        #region Assembling the Vehicle and creating a Vehicle Item

                        double Assy_Checker = 0; //  This variable is used to check if all the components of the Vehicle have been assembled. 
                        InputSheet I1 = new InputSheet(this);

                        //
                        //Suspension Assembly
                        //
                        #region Suspension Assembly
                        if ((comboBoxSCFL.SelectedItem != null) && (comboBoxSCFR.SelectedItem != null) && (comboBoxSCRL.SelectedItem != null) && (comboBoxSCRR.SelectedItem != null))
                        {
                            #region Assembling the Suspension Coordinates which User has selected

                            M1_Global.Assy_SCM[0] = (SuspensionCoordinatesFront)comboBoxSCFL.SelectedItem;

                            M1_Global.Assy_SCM[1] = (SuspensionCoordinatesFrontRight)comboBoxSCFR.SelectedItem;

                            #region Checking if the Front Left and Right have the same Geometry Type
                            if (((M1_Global.Assy_SCM[0].McPhersonIdentifierFront != M1_Global.Assy_SCM[1].McPhersonIdentifierFront) || (M1_Global.Assy_SCM[1].McPhersonIdentifierFront != M1_Global.Assy_SCM[0].McPhersonIdentifierFront)) ||
                               ((M1_Global.Assy_SCM[0].PullrodIdentifierFront != M1_Global.Assy_SCM[1].PullrodIdentifierFront) || (M1_Global.Assy_SCM[1].PullrodIdentifierFront != M1_Global.Assy_SCM[0].PullrodIdentifierFront)) ||
                               ((M1_Global.Assy_SCM[0].TARBIdentifierFront != M1_Global.Assy_SCM[1].TARBIdentifierFront) || (M1_Global.Assy_SCM[1].TARBIdentifierFront != M1_Global.Assy_SCM[0].TARBIdentifierFront)))
                            {
                                MessageBox.Show("Please Select the same Suspension Type for the Front Left and Front Right Corner");

                                return;
                            }
                            #endregion

                            M1_Global.Assy_SCM[2] = (SuspensionCoordinatesRear)comboBoxSCRL.SelectedItem;

                            M1_Global.Assy_SCM[3] = (SuspensionCoordinatesRearRight)comboBoxSCRR.SelectedItem;

                            #region Checking if the Rear Left and Right have the same Geometry Type
                            if (((M1_Global.Assy_SCM[2].McPhersonIdentifierRear != M1_Global.Assy_SCM[3].McPhersonIdentifierRear) || (M1_Global.Assy_SCM[3].McPhersonIdentifierRear != M1_Global.Assy_SCM[2].McPhersonIdentifierRear)) ||
                               ((M1_Global.Assy_SCM[2].PullrodIdentifierRear != M1_Global.Assy_SCM[3].PullrodIdentifierRear) || (M1_Global.Assy_SCM[3].PullrodIdentifierRear != M1_Global.Assy_SCM[2].PullrodIdentifierRear)) ||
                               ((M1_Global.Assy_SCM[2].TARBIdentifierRear != M1_Global.Assy_SCM[3].TARBIdentifierRear) || (M1_Global.Assy_SCM[3].TARBIdentifierRear != M1_Global.Assy_SCM[2].TARBIdentifierRear)))
                            {
                                MessageBox.Show("Please Select the same Suspension Type for the Rear Left and Rear Right Corner");

                                return;
                            }
                            #endregion

                            #endregion

                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Suspension Pick Up Points have not been assembled");

                            return;

                        }
                        #endregion

                        //
                        //Tire Assembly
                        //
                        #region Tire Assembly
                        if ((comboBoxTireFL.SelectedItem != null) && (comboBoxTireFR.SelectedItem != null) && (comboBoxTireRL.SelectedItem != null) && (comboBoxTireRR.SelectedItem != null))
                        {
                            #region Assembling the Tires which User has selected
                            Tire.Assy_Tire[0] = (Tire)comboBoxTireFL.SelectedItem;


                            Tire.Assy_Tire[1] = (Tire)comboBoxTireFR.SelectedItem;


                            Tire.Assy_Tire[2] = (Tire)comboBoxTireRL.SelectedItem;


                            Tire.Assy_Tire[3] = (Tire)comboBoxTireRR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Tires have not been assembled"); ;
                            return;
                        }
                        #endregion

                        //
                        //Spring Assembly
                        //
                        #region Spring Assembly
                        if ((comboBoxSpringFL.SelectedItem != null) && (comboBoxSpringFR.SelectedItem != null) && (comboBoxSpringRL.SelectedItem != null) && (comboBoxSpringRR.SelectedItem != null))
                        {
                            #region Assembling the Springs which the User has Selected
                            Spring.Assy_Spring[0] = (Spring)comboBoxSpringFL.SelectedItem;


                            Spring.Assy_Spring[1] = (Spring)comboBoxSpringFR.SelectedItem;


                            Spring.Assy_Spring[2] = (Spring)comboBoxSpringRL.SelectedItem;


                            Spring.Assy_Spring[3] = (Spring)comboBoxSpringRR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Springs have not been assembled");
                            return;
                        }
                        #endregion

                        //
                        //Damper Assembly
                        //
                        #region Damper Assembly
                        if ((comboBoxDamperFL.SelectedItem != null) && (comboBoxDamperFR.SelectedItem != null) && (comboBoxDamperRL.SelectedItem != null) && (comboBoxDamperRR.SelectedItem != null))
                        {
                            #region Assembling the Dampers which the user has selected
                            Damper.Assy_Damper[0] = (Damper)comboBoxDamperFL.SelectedItem;


                            Damper.Assy_Damper[1] = (Damper)comboBoxDamperFR.SelectedItem;


                            Damper.Assy_Damper[2] = (Damper)comboBoxDamperRL.SelectedItem;


                            Damper.Assy_Damper[3] = (Damper)comboBoxDamperRR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Dampers have not been assembled");
                            return;
                        }
                        #endregion

                        //
                        //Anti-Roll Bar Assembly
                        //
                        #region Anti-Roll Bar Assembly
                        if (comboBoxARBFront.SelectedItem != null)
                        {
                            #region Assembling the Anti-Roll Bars which the user has selected
                            AntiRollBar.Assy_ARB[0] = (AntiRollBar)comboBoxARBFront.SelectedItem;
                            AntiRollBar.Assy_ARB[1] = (AntiRollBar)comboBoxARBFront.SelectedItem;

                            #endregion

                            Assy_Checker += 0.5;
                        }
                        else if (comboBoxARBFront.SelectedItem == null)
                        {
                            arbGUI.Insert(l_vehicle, new AntiRollBarGUI());
                            Default_Values.ARBDefaultValues2(arbGUI[l_vehicle]);
                            arbGUI[l_vehicle].Update_ARBGUI(this, l_vehicle);

                            AntiRollBar.Assy_ARB[0] = new AntiRollBar(arbGUI[l_vehicle]);
                            AntiRollBar.Assy_ARB[0].AntiRollBarRate = 0;

                            AntiRollBar.Assy_ARB[1] = new AntiRollBar(arbGUI[l_vehicle]);
                            AntiRollBar.Assy_ARB[1].AntiRollBarRate = 0;
                            Assy_Checker += 0.5;

                        }
                        if (comboBoxARBRear.SelectedItem != null)
                        {
                            #region Assembling the Rear Anti-Roll Bar whcih the user has selected
                            AntiRollBar.Assy_ARB[2] = (AntiRollBar)comboBoxARBRear.SelectedItem;
                            AntiRollBar.Assy_ARB[3] = (AntiRollBar)comboBoxARBRear.SelectedItem;

                            #endregion
                            Assy_Checker += 0.5;

                        }
                        else if (comboBoxARBRear.SelectedItem == null)
                        {


                            arbGUI.Insert(l_vehicle, new AntiRollBarGUI());
                            Default_Values.ARBDefaultValues2(arbGUI[l_vehicle]);
                            arbGUI[l_vehicle].Update_ARBGUI(this, l_vehicle);

                            AntiRollBar.Assy_ARB[2] = new AntiRollBar(arbGUI[l_vehicle]);
                            AntiRollBar.Assy_ARB[2].AntiRollBarRate = 0;

                            AntiRollBar.Assy_ARB[3] = new AntiRollBar(arbGUI[l_vehicle]);
                            AntiRollBar.Assy_ARB[3].AntiRollBarRate = 0;
                            Assy_Checker += 0.5;

                        }

                        #endregion

                        //
                        //Chassis Assembly
                        //
                        #region Chassis Assembly
                        if (comboBoxChassis.SelectedItem != null)
                        {
                            #region Assembling the Chassis which the user has selected
                            Chassis.Assy_Chassis = (Chassis)comboBoxChassis.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Chassis has not been assembled");
                            return;
                        }
                        #endregion

                        //
                        //WheelAlignment Assembly
                        //
                        #region WheelAlignment Assembly
                        if ((comboBoxWAFL.SelectedItem != null) && (comboBoxWAFR.SelectedItem != null) && (comboBoxWARL.SelectedItem != null) && (comboBoxWARR.SelectedItem != null))
                        {
                            #region Assembling the Wheel Alignment which the user has selected
                            WheelAlignment.Assy_WA[0] = (WheelAlignment)comboBoxWAFL.SelectedItem;


                            WheelAlignment.Assy_WA[1] = (WheelAlignment)comboBoxWAFR.SelectedItem;


                            WheelAlignment.Assy_WA[2] = (WheelAlignment)comboBoxWARL.SelectedItem;


                            WheelAlignment.Assy_WA[3] = (WheelAlignment)comboBoxWARR.SelectedItem;


                            #endregion
                            Assy_Checker++;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle's Wheel Alignment has not been set");
                            return;
                        }
                        #endregion

                        //
                        // Passing the local Input Sheet Object to the Global List of Input Sheet
                        //

                        M1_Global.List_I1[l_vehicle] = I1;

                        //
                        // Checking if all the Input Parameters have been assembled
                        //
                        #region Checking if all Input Parameters have been assembled
                        if (Assy_Checker == 7)
                        {
                            //
                            // Passing the parameters to VEHICLE ASSEMBLY method where a new Vehicle Object will be Initialized. This object will then be returned using an out parameter of type Vehicle 
                            //
                            Vehicle vehicle_list;
                            VehicleAssembly(M1_Global.Assy_SCM, Tire.Assy_Tire, Spring.Assy_Spring, Damper.Assy_Damper, AntiRollBar.Assy_ARB, Chassis.Assy_Chassis, WheelAlignment.Assy_WA, M1_Global.Assy_OC, out vehicle_list);
                            V1_Global = vehicle_list;
                            V1_Global.ModifyObjectData(l_vehicle, vehicle_list, false);
                            Vehicle.CurrentVehicleID = Vehicle.List_Vehicle[l_vehicle].VehicleID;
                            UndoObject.Identifier(Vehicle.List_Vehicle[l_vehicle]._UndocommandsVehicle, Vehicle.List_Vehicle[l_vehicle]._RedocommandsVehicle, Vehicle.CurrentVehicleID, Vehicle.List_Vehicle[l_vehicle].VehicleIsModified);
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Vehicle Not Assembled Properly. Please re-check your Vehicle Item");
                            return;
                        }
                        #endregion

                        #endregion
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unexpected Error during Vehicle Modification");
                    }

                }
            }
            ComboboxVehicleOperations();

        }

        private void comboBoxVehicle_Leave(object sender, EventArgs e)
        {
            try
            {
                Vehicle.Assembled_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;

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
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            accordionControlWheelAlignment.Hide();
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
                        comboBoxSCFL.Text = Vehicle.List_Vehicle[c_vehicle].sc_FL._SCName;
                        comboBoxSCFR.Text = Vehicle.List_Vehicle[c_vehicle].sc_FR._SCName;
                        comboBoxSCRL.Text = Vehicle.List_Vehicle[c_vehicle].sc_RL._SCName;
                        comboBoxSCRR.Text = Vehicle.List_Vehicle[c_vehicle].sc_RR._SCName;

                        comboBoxTireFL.Text = Vehicle.List_Vehicle[c_vehicle].tire_FL._TireName;
                        comboBoxTireFR.Text = Vehicle.List_Vehicle[c_vehicle].tire_FL._TireName;
                        comboBoxTireRL.Text = Vehicle.List_Vehicle[c_vehicle].tire_RL._TireName;
                        comboBoxTireRR.Text = Vehicle.List_Vehicle[c_vehicle].tire_RR._TireName;

                        comboBoxSpringFL.Text = Vehicle.List_Vehicle[c_vehicle].spring_FL._SpringName;
                        comboBoxSpringFR.Text = Vehicle.List_Vehicle[c_vehicle].spring_FR._SpringName;
                        comboBoxSpringRL.Text = Vehicle.List_Vehicle[c_vehicle].spring_RL._SpringName;
                        comboBoxSpringRR.Text = Vehicle.List_Vehicle[c_vehicle].spring_RR._SpringName;

                        comboBoxDamperFL.Text = Vehicle.List_Vehicle[c_vehicle].damper_FL._DamperName;
                        comboBoxDamperFR.Text = Vehicle.List_Vehicle[c_vehicle].damper_FR._DamperName;
                        comboBoxDamperRL.Text = Vehicle.List_Vehicle[c_vehicle].damper_RL._DamperName;
                        comboBoxDamperRR.Text = Vehicle.List_Vehicle[c_vehicle].damper_RR._DamperName;

                        comboBoxARBFront.Text = Vehicle.List_Vehicle[c_vehicle].arb_FL._ARBName;
                        comboBoxARBRear.Text = Vehicle.List_Vehicle[c_vehicle].arb_RL._ARBName;

                        comboBoxChassis.Text = Vehicle.List_Vehicle[c_vehicle].chassis_vehicle._ChassisName;

                        comboBoxWAFL.Text = Vehicle.List_Vehicle[c_vehicle].wa_FL._WAName;
                        comboBoxWAFR.Text = Vehicle.List_Vehicle[c_vehicle].wa_FR._WAName;
                        comboBoxWARL.Text = Vehicle.List_Vehicle[c_vehicle].wa_RL._WAName;
                        comboBoxWARR.Text = Vehicle.List_Vehicle[c_vehicle].wa_RR._WAName;
                        #endregion
                    }
                    catch (Exception)
                    {

                        //Added for safety
                    }


                }
            }

        }

        private void ComboboxVehicleOperations()
        {
            comboBoxVehicle.Items.Clear();

            for (int i_combobox_vehicle = 0; i_combobox_vehicle < Vehicle.List_Vehicle.Count; i_combobox_vehicle++)
            {

                try
                {
                    #region Populating the Vehicle comboboxes
                    comboBoxVehicle.Items.Insert(i_combobox_vehicle, Vehicle.List_Vehicle[i_combobox_vehicle]);
                    comboBoxVehicle.SelectedIndex = 0;
                    comboBoxVehicle.DisplayMember = "_VehicleName";
                    #endregion

                }
                catch (Exception)
                {
                }
            }
        }


        #endregion
        
        #endregion

        //
        // Input Sheet Population
        //
        #region Handling the Input Sheet

        #region Create Input Sheet
        private void CreateInputSheet_Click(object sender, EventArgs e)
        {
            try
            {
                int local_VehicleID = 0;

                if (Vehicle.Assembled_Vehicle.VehicleID == 0)
                {
                    Vehicle.Assembled_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;
                    local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;
                }
                else
                    local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                #region Disabling and Colouring the PushRod and Corner Weight Textboxes so that user doesnt change anything before Initiial Calculation of results
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].RecalculateCornerWeightForPushRodLength.Enabled = false;

                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;
                #endregion

                PopulateInputSheet();

                M1_Global.List_I1[local_VehicleID - 1].Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Input Sheet hass not been populated. Please check if Vehicle has been created properly");
            }
        } 
        #endregion

        #region Input Sheet Population Method
        private void PopulateInputSheet()
        {
            try
            {
                int local_VehicleID = 0;

                if (Vehicle.Assembled_Vehicle.VehicleID == 0)
                {
                    Vehicle.Assembled_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;
                    local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;
                }
                else
                    local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                #region Population of the Input Sheet
                #region Populating the Input Sheet - Front Left Coordinates
                M1_Global.List_I1[local_VehicleID - 1].A1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.A1x)));
                M1_Global.List_I1[local_VehicleID - 1].A1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.A1y)));
                M1_Global.List_I1[local_VehicleID - 1].A1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.A1z)));
                M1_Global.List_I1[local_VehicleID - 1].B1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.B1x)));
                M1_Global.List_I1[local_VehicleID - 1].B1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.B1y)));
                M1_Global.List_I1[local_VehicleID - 1].B1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.B1z)));
                M1_Global.List_I1[local_VehicleID - 1].C1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.C1x)));
                M1_Global.List_I1[local_VehicleID - 1].C1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.C1y)));
                M1_Global.List_I1[local_VehicleID - 1].C1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.C1z)));
                M1_Global.List_I1[local_VehicleID - 1].D1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.D1x)));
                M1_Global.List_I1[local_VehicleID - 1].D1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.D1y)));
                M1_Global.List_I1[local_VehicleID - 1].D1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.D1z)));
                M1_Global.List_I1[local_VehicleID - 1].N1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.N1x)));
                M1_Global.List_I1[local_VehicleID - 1].N1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.N1y)));
                M1_Global.List_I1[local_VehicleID - 1].N1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.N1z)));
                M1_Global.List_I1[local_VehicleID - 1].Q1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.Q1x)));
                M1_Global.List_I1[local_VehicleID - 1].Q1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.Q1y)));
                M1_Global.List_I1[local_VehicleID - 1].Q1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.Q1z)));
                M1_Global.List_I1[local_VehicleID - 1].I1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.I1x)));
                M1_Global.List_I1[local_VehicleID - 1].I1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.I1y)));
                M1_Global.List_I1[local_VehicleID - 1].I1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.I1z)));
                M1_Global.List_I1[local_VehicleID - 1].JO1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.JO1x)));
                M1_Global.List_I1[local_VehicleID - 1].JO1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.JO1y)));
                M1_Global.List_I1[local_VehicleID - 1].JO1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.List_I1[local_VehicleID - 1].J1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.J1x)));
                M1_Global.List_I1[local_VehicleID - 1].J1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.J1y)));
                M1_Global.List_I1[local_VehicleID - 1].J1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.List_I1[local_VehicleID - 1].H1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.H1x)));
                M1_Global.List_I1[local_VehicleID - 1].H1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.H1y)));
                M1_Global.List_I1[local_VehicleID - 1].H1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.List_I1[local_VehicleID - 1].O1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.O1x)));
                M1_Global.List_I1[local_VehicleID - 1].O1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.O1y)));
                M1_Global.List_I1[local_VehicleID - 1].O1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.List_I1[local_VehicleID - 1].G1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.G1x)));
                M1_Global.List_I1[local_VehicleID - 1].G1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.G1y)));
                M1_Global.List_I1[local_VehicleID - 1].G1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.List_I1[local_VehicleID - 1].F1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.F1x)));
                M1_Global.List_I1[local_VehicleID - 1].F1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.F1y)));
                M1_Global.List_I1[local_VehicleID - 1].F1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.List_I1[local_VehicleID - 1].E1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.E1x)));
                M1_Global.List_I1[local_VehicleID - 1].E1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.E1y)));
                M1_Global.List_I1[local_VehicleID - 1].E1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.List_I1[local_VehicleID - 1].M1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.M1x)));
                M1_Global.List_I1[local_VehicleID - 1].M1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.M1y)));
                M1_Global.List_I1[local_VehicleID - 1].M1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.List_I1[local_VehicleID - 1].K1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.K1x)));
                M1_Global.List_I1[local_VehicleID - 1].K1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.K1y)));
                M1_Global.List_I1[local_VehicleID - 1].K1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.List_I1[local_VehicleID - 1].P1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.P1x)));
                M1_Global.List_I1[local_VehicleID - 1].P1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.P1y)));
                M1_Global.List_I1[local_VehicleID - 1].P1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.List_I1[local_VehicleID - 1].W1xFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.W1x)));
                M1_Global.List_I1[local_VehicleID - 1].W1yFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.W1y)));
                M1_Global.List_I1[local_VehicleID - 1].W1zFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.W1z)));
                // Link Lengths
                M1_Global.List_I1[local_VehicleID - 1].LowerFrontFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.LowerFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].LowerRearFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.LowerRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperFrontFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.UpperFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperRearFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.UpperRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.PushRodLength)));
                M1_Global.List_I1[local_VehicleID - 1].ToeLinkFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.ToeLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBBladeFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.ARBBladeLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBDroopLinkFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.ARBDroopLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].DamperLengthFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.DamperLength)));
                #endregion
                #region Populating the Input Sheet - Front Left Coordinates
                M1_Global.List_I1[local_VehicleID - 1].A1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.A1x)));
                M1_Global.List_I1[local_VehicleID - 1].A1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.A1y)));
                M1_Global.List_I1[local_VehicleID - 1].A1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.A1z)));
                M1_Global.List_I1[local_VehicleID - 1].B1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.B1x)));
                M1_Global.List_I1[local_VehicleID - 1].B1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.B1y)));
                M1_Global.List_I1[local_VehicleID - 1].B1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.B1z)));
                M1_Global.List_I1[local_VehicleID - 1].C1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.C1x)));
                M1_Global.List_I1[local_VehicleID - 1].C1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.C1y)));
                M1_Global.List_I1[local_VehicleID - 1].C1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.C1z)));
                M1_Global.List_I1[local_VehicleID - 1].D1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.D1x)));
                M1_Global.List_I1[local_VehicleID - 1].D1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.D1y)));
                M1_Global.List_I1[local_VehicleID - 1].D1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.D1z)));
                M1_Global.List_I1[local_VehicleID - 1].N1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.N1x)));
                M1_Global.List_I1[local_VehicleID - 1].N1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.N1y)));
                M1_Global.List_I1[local_VehicleID - 1].N1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.N1z)));
                M1_Global.List_I1[local_VehicleID - 1].Q1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.Q1x)));
                M1_Global.List_I1[local_VehicleID - 1].Q1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.Q1y)));
                M1_Global.List_I1[local_VehicleID - 1].Q1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.Q1z)));
                M1_Global.List_I1[local_VehicleID - 1].I1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.I1x)));
                M1_Global.List_I1[local_VehicleID - 1].I1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.I1y)));
                M1_Global.List_I1[local_VehicleID - 1].I1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.I1z)));
                M1_Global.List_I1[local_VehicleID - 1].JO1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.JO1x)));
                M1_Global.List_I1[local_VehicleID - 1].JO1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.JO1y)));
                M1_Global.List_I1[local_VehicleID - 1].JO1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.List_I1[local_VehicleID - 1].J1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.J1x)));
                M1_Global.List_I1[local_VehicleID - 1].J1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.J1y)));
                M1_Global.List_I1[local_VehicleID - 1].J1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.List_I1[local_VehicleID - 1].H1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.H1x)));
                M1_Global.List_I1[local_VehicleID - 1].H1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.H1y)));
                M1_Global.List_I1[local_VehicleID - 1].H1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.List_I1[local_VehicleID - 1].O1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.O1x)));
                M1_Global.List_I1[local_VehicleID - 1].O1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.O1y)));
                M1_Global.List_I1[local_VehicleID - 1].O1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.List_I1[local_VehicleID - 1].G1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.G1x)));
                M1_Global.List_I1[local_VehicleID - 1].G1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.G1y)));
                M1_Global.List_I1[local_VehicleID - 1].G1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.List_I1[local_VehicleID - 1].F1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.F1x)));
                M1_Global.List_I1[local_VehicleID - 1].F1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.F1y)));
                M1_Global.List_I1[local_VehicleID - 1].F1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.List_I1[local_VehicleID - 1].E1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.E1x)));
                M1_Global.List_I1[local_VehicleID - 1].E1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.E1y)));
                M1_Global.List_I1[local_VehicleID - 1].E1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.List_I1[local_VehicleID - 1].M1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.M1x)));
                M1_Global.List_I1[local_VehicleID - 1].M1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.M1y)));
                M1_Global.List_I1[local_VehicleID - 1].M1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.List_I1[local_VehicleID - 1].K1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.K1x)));
                M1_Global.List_I1[local_VehicleID - 1].K1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.K1y)));
                M1_Global.List_I1[local_VehicleID - 1].K1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.List_I1[local_VehicleID - 1].P1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.P1x)));
                M1_Global.List_I1[local_VehicleID - 1].P1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.P1y)));
                M1_Global.List_I1[local_VehicleID - 1].P1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.List_I1[local_VehicleID - 1].W1xFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.W1x)));
                M1_Global.List_I1[local_VehicleID - 1].W1yFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.W1y)));
                M1_Global.List_I1[local_VehicleID - 1].W1zFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.W1z)));
                // Link Lengths
                M1_Global.List_I1[local_VehicleID - 1].LowerFrontFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.LowerFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].LowerRearFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.LowerRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperFrontFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.UpperFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperRearFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.UpperRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.PushRodLength)));
                M1_Global.List_I1[local_VehicleID - 1].ToeLinkFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.ToeLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBBladeFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.ARBBladeLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBDroopLinkFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.ARBDroopLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].DamperLengthFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.DamperLength)));
                #endregion
                #region Populating the Input Sheet - REAR Left Coordinates
                M1_Global.List_I1[local_VehicleID - 1].A1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.A1x)));
                M1_Global.List_I1[local_VehicleID - 1].A1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.A1y)));
                M1_Global.List_I1[local_VehicleID - 1].A1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.A1z)));
                M1_Global.List_I1[local_VehicleID - 1].B1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.B1x)));
                M1_Global.List_I1[local_VehicleID - 1].B1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.B1y)));
                M1_Global.List_I1[local_VehicleID - 1].B1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.B1z)));
                M1_Global.List_I1[local_VehicleID - 1].C1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.C1x)));
                M1_Global.List_I1[local_VehicleID - 1].C1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.C1y)));
                M1_Global.List_I1[local_VehicleID - 1].C1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.C1z)));
                M1_Global.List_I1[local_VehicleID - 1].D1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.D1x)));
                M1_Global.List_I1[local_VehicleID - 1].D1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.D1y)));
                M1_Global.List_I1[local_VehicleID - 1].D1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.D1z)));
                M1_Global.List_I1[local_VehicleID - 1].N1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.N1x)));
                M1_Global.List_I1[local_VehicleID - 1].N1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.N1y)));
                M1_Global.List_I1[local_VehicleID - 1].N1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.N1z)));
                M1_Global.List_I1[local_VehicleID - 1].Q1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.Q1x)));
                M1_Global.List_I1[local_VehicleID - 1].Q1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.Q1y)));
                M1_Global.List_I1[local_VehicleID - 1].Q1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.Q1z)));
                M1_Global.List_I1[local_VehicleID - 1].I1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.I1x)));
                M1_Global.List_I1[local_VehicleID - 1].I1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.I1y)));
                M1_Global.List_I1[local_VehicleID - 1].I1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.I1z)));
                M1_Global.List_I1[local_VehicleID - 1].JO1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.JO1x)));
                M1_Global.List_I1[local_VehicleID - 1].JO1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.JO1y)));
                M1_Global.List_I1[local_VehicleID - 1].JO1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.List_I1[local_VehicleID - 1].J1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.J1x)));
                M1_Global.List_I1[local_VehicleID - 1].J1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.J1y)));
                M1_Global.List_I1[local_VehicleID - 1].J1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.List_I1[local_VehicleID - 1].H1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.H1x)));
                M1_Global.List_I1[local_VehicleID - 1].H1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.H1y)));
                M1_Global.List_I1[local_VehicleID - 1].H1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.List_I1[local_VehicleID - 1].O1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.O1x)));
                M1_Global.List_I1[local_VehicleID - 1].O1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.O1y)));
                M1_Global.List_I1[local_VehicleID - 1].O1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.List_I1[local_VehicleID - 1].G1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.G1x)));
                M1_Global.List_I1[local_VehicleID - 1].G1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.G1y)));
                M1_Global.List_I1[local_VehicleID - 1].G1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.List_I1[local_VehicleID - 1].F1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.F1x)));
                M1_Global.List_I1[local_VehicleID - 1].F1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.F1y)));
                M1_Global.List_I1[local_VehicleID - 1].F1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.List_I1[local_VehicleID - 1].E1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.E1x)));
                M1_Global.List_I1[local_VehicleID - 1].E1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.E1y)));
                M1_Global.List_I1[local_VehicleID - 1].E1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.List_I1[local_VehicleID - 1].M1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.M1x)));
                M1_Global.List_I1[local_VehicleID - 1].M1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.M1y)));
                M1_Global.List_I1[local_VehicleID - 1].M1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.List_I1[local_VehicleID - 1].K1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.K1x)));
                M1_Global.List_I1[local_VehicleID - 1].K1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.K1y)));
                M1_Global.List_I1[local_VehicleID - 1].K1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.List_I1[local_VehicleID - 1].P1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.P1x)));
                M1_Global.List_I1[local_VehicleID - 1].P1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.P1y)));
                M1_Global.List_I1[local_VehicleID - 1].P1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.List_I1[local_VehicleID - 1].W1xRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.W1x)));
                M1_Global.List_I1[local_VehicleID - 1].W1yRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.W1y)));
                M1_Global.List_I1[local_VehicleID - 1].W1zRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.W1z)));
                // Link Lengths
                M1_Global.List_I1[local_VehicleID - 1].LowerFrontRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.LowerFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].LowerRearRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.LowerRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperFrontRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.UpperFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperRearRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.UpperRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.PushRodLength)));
                M1_Global.List_I1[local_VehicleID - 1].ToeLinkRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.ToeLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBBladeRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.ARBBladeLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBDroopLinkRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.ARBDroopLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].DamperLengthRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.DamperLength)));
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Coordinates
                M1_Global.List_I1[local_VehicleID - 1].A1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.A1x)));
                M1_Global.List_I1[local_VehicleID - 1].A1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.A1y)));
                M1_Global.List_I1[local_VehicleID - 1].A1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.A1z)));
                M1_Global.List_I1[local_VehicleID - 1].B1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.B1x)));
                M1_Global.List_I1[local_VehicleID - 1].B1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.B1y)));
                M1_Global.List_I1[local_VehicleID - 1].B1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.B1z)));
                M1_Global.List_I1[local_VehicleID - 1].C1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.C1x)));
                M1_Global.List_I1[local_VehicleID - 1].C1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.C1y)));
                M1_Global.List_I1[local_VehicleID - 1].C1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.C1z)));
                M1_Global.List_I1[local_VehicleID - 1].D1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.D1x)));
                M1_Global.List_I1[local_VehicleID - 1].D1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.D1y)));
                M1_Global.List_I1[local_VehicleID - 1].D1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.D1z)));
                M1_Global.List_I1[local_VehicleID - 1].N1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.N1x)));
                M1_Global.List_I1[local_VehicleID - 1].N1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.N1y)));
                M1_Global.List_I1[local_VehicleID - 1].N1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.N1z)));
                M1_Global.List_I1[local_VehicleID - 1].Q1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.Q1x)));
                M1_Global.List_I1[local_VehicleID - 1].Q1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.Q1y)));
                M1_Global.List_I1[local_VehicleID - 1].Q1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.Q1z)));
                M1_Global.List_I1[local_VehicleID - 1].I1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.I1x)));
                M1_Global.List_I1[local_VehicleID - 1].I1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.I1y)));
                M1_Global.List_I1[local_VehicleID - 1].I1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.I1z)));
                M1_Global.List_I1[local_VehicleID - 1].JO1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.JO1x)));
                M1_Global.List_I1[local_VehicleID - 1].JO1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.JO1y)));
                M1_Global.List_I1[local_VehicleID - 1].JO1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                M1_Global.List_I1[local_VehicleID - 1].J1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.J1x)));
                M1_Global.List_I1[local_VehicleID - 1].J1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.J1y)));
                M1_Global.List_I1[local_VehicleID - 1].J1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                M1_Global.List_I1[local_VehicleID - 1].H1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.H1x)));
                M1_Global.List_I1[local_VehicleID - 1].H1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.H1y)));
                M1_Global.List_I1[local_VehicleID - 1].H1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                M1_Global.List_I1[local_VehicleID - 1].O1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.O1x)));
                M1_Global.List_I1[local_VehicleID - 1].O1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.O1y)));
                M1_Global.List_I1[local_VehicleID - 1].O1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                M1_Global.List_I1[local_VehicleID - 1].G1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.G1x)));
                M1_Global.List_I1[local_VehicleID - 1].G1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.G1y)));
                M1_Global.List_I1[local_VehicleID - 1].G1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                M1_Global.List_I1[local_VehicleID - 1].F1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.F1x)));
                M1_Global.List_I1[local_VehicleID - 1].F1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.F1y)));
                M1_Global.List_I1[local_VehicleID - 1].F1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                M1_Global.List_I1[local_VehicleID - 1].E1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.E1x)));
                M1_Global.List_I1[local_VehicleID - 1].E1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.E1y)));
                M1_Global.List_I1[local_VehicleID - 1].E1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M1_Global.List_I1[local_VehicleID - 1].M1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.M1x)));
                M1_Global.List_I1[local_VehicleID - 1].M1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.M1y)));
                M1_Global.List_I1[local_VehicleID - 1].M1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                M1_Global.List_I1[local_VehicleID - 1].K1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.K1x)));
                M1_Global.List_I1[local_VehicleID - 1].K1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.K1y)));
                M1_Global.List_I1[local_VehicleID - 1].K1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                M1_Global.List_I1[local_VehicleID - 1].P1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.P1x)));
                M1_Global.List_I1[local_VehicleID - 1].P1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.P1y)));
                M1_Global.List_I1[local_VehicleID - 1].P1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                M1_Global.List_I1[local_VehicleID - 1].W1xRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.W1x)));
                M1_Global.List_I1[local_VehicleID - 1].W1yRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.W1y)));
                M1_Global.List_I1[local_VehicleID - 1].W1zRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.W1z)));
                // Link Lengths
                M1_Global.List_I1[local_VehicleID - 1].LowerFrontRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.LowerFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].LowerRearRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.LowerRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperFrontRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.UpperFrontLength)));
                M1_Global.List_I1[local_VehicleID - 1].UpperRearRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.UpperRearLength)));
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.PushRodLength)));
                M1_Global.List_I1[local_VehicleID - 1].ToeLinkRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.ToeLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBBladeRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.ARBBladeLength)));
                M1_Global.List_I1[local_VehicleID - 1].ARBDroopLinkRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.ARBDroopLinkLength)));
                M1_Global.List_I1[local_VehicleID - 1].DamperLengthRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.DamperLength)));
                #endregion
                #region GUI operations to change the Input Sheet Textboxes and labels
                if (Vehicle.Assembled_Vehicle.McPhersonFront == 1)
                {
                    M1_Global.List_I1[local_VehicleID - 1].A1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].A1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1zFR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].B1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].B1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1zFR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].I1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].I1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1zFR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].H1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].H1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1zFR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].G1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].G1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1zFR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].F1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].F1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1zFR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].O1xFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1yFL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1zFL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].O1xFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1yFR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1zFR.Text = "NaN";

                }

                if (Vehicle.Assembled_Vehicle.McPhersonRear == 1)
                {
                    M1_Global.List_I1[local_VehicleID - 1].A1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].A1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].A1zRR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].B1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].B1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].B1zRR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].I1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].I1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].I1zRR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].H1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].H1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].H1zRR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].G1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].G1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].G1zRR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].F1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].F1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].F1zRR.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].O1xRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1yRL.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1zRL.Text = "NaN";
                    M1_Global.List_I1[local_VehicleID - 1].O1xRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1yRR.Text = "NaN"; M1_Global.List_I1[local_VehicleID - 1].O1zRR.Text = "NaN";
                }

                if (Vehicle.Assembled_Vehicle.PushRodIdentifierFront == 1 || Vehicle.Assembled_Vehicle.PushRodIdentifierFront == 0)
                {
                    M1_Global.List_I1[local_VehicleID - 1].label183.Text = "Pushrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label205.Text = "Pushrod Upright";
                    M1_Global.List_I1[local_VehicleID - 1].label317.Text = "Pushrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label244.Text = "Pushrod Upright";
                }
                if (Vehicle.Assembled_Vehicle.PullRodIdentifierFront == 1)
                {
                    M1_Global.List_I1[local_VehicleID - 1].label183.Text = "Pullrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label205.Text = "Pullrod Upright";
                    M1_Global.List_I1[local_VehicleID - 1].label317.Text = "Pullrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label244.Text = "Pullrod Upright";
                }

                if (Vehicle.Assembled_Vehicle.PushRodIdentifierRear == 1 || Vehicle.Assembled_Vehicle.PushRodIdentifierRear == 0)
                {
                    M1_Global.List_I1[local_VehicleID - 1].label277.Text = "Pushrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label272.Text = "Pushrod Upright";
                    M1_Global.List_I1[local_VehicleID - 1].label396.Text = "Pushrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label349.Text = "Pushrod Upright";
                }
                if (Vehicle.Assembled_Vehicle.PullRodIdentifierRear == 1)
                {
                    M1_Global.List_I1[local_VehicleID - 1].label277.Text = "Pullrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label272.Text = "Pullrod Upright";
                    M1_Global.List_I1[local_VehicleID - 1].label396.Text = "Pullrod Bell Crank";
                    M1_Global.List_I1[local_VehicleID - 1].label349.Text = "Pullrod Upright";
                }
                #endregion


                #region Populating Input Sheet - FRONT LEFT Tire
                M1_Global.List_I1[local_VehicleID - 1].TireRateFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_FL.TireRate);
                M1_Global.List_I1[local_VehicleID - 1].TireWidthFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_FL.TireWidth);
                #endregion
                #region Populating Input Sheet - FRONT RIGHT Tire
                M1_Global.List_I1[local_VehicleID - 1].TireRateFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_FR.TireRate);
                M1_Global.List_I1[local_VehicleID - 1].TireWidthFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_FR.TireWidth);
                #endregion
                #region Populating Input Sheet - REAR LEFT Tire
                M1_Global.List_I1[local_VehicleID - 1].TireRateRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_RL.TireRate);
                M1_Global.List_I1[local_VehicleID - 1].TireWidthRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_RL.TireWidth);
                #endregion
                #region Populating Input Sheet - REAR RIGHT Tire
                M1_Global.List_I1[local_VehicleID - 1].TireRateRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_RR.TireRate);
                M1_Global.List_I1[local_VehicleID - 1].TireWidthRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.tire_RR.TireWidth);
                #endregion


                #region Populating the Input Sheet - FRONT LEFT Spring
                M1_Global.List_I1[local_VehicleID - 1].SpringRateFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_FL.SpringRate);
                M1_Global.List_I1[local_VehicleID - 1].SpringPreloadFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_FL.SpringPreload);
                M1_Global.List_I1[local_VehicleID - 1].SpringFreeLengthFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_FL.SpringFreeLength);
                #endregion
                #region Populating the Input Sheet - FRONT RIGHT Spring
                M1_Global.List_I1[local_VehicleID - 1].SpringRateFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_FR.SpringRate);
                M1_Global.List_I1[local_VehicleID - 1].SpringPreloadFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_FR.SpringPreload);
                M1_Global.List_I1[local_VehicleID - 1].SpringFreeLengthFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_FR.SpringFreeLength);
                #endregion
                #region Populating the Input Sheet - REAR LEFT Spring
                M1_Global.List_I1[local_VehicleID - 1].SpringRateRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_RL.SpringRate);
                M1_Global.List_I1[local_VehicleID - 1].SpringPreloadRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_RL.SpringPreload);
                M1_Global.List_I1[local_VehicleID - 1].SpringFreeLengthRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_RL.SpringFreeLength);
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Spring
                M1_Global.List_I1[local_VehicleID - 1].SpringRateRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_RR.SpringRate);
                M1_Global.List_I1[local_VehicleID - 1].SpringPreloadRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_RR.SpringPreload);
                M1_Global.List_I1[local_VehicleID - 1].SpringFreeLengthRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.spring_RR.SpringFreeLength);
                #endregion


                #region Populating the Input Sheet - FRONT LEFT Damper
                M1_Global.List_I1[local_VehicleID - 1].DamperPressureFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_FL.DamperGasPressure);
                M1_Global.List_I1[local_VehicleID - 1].DamperShaftDiaFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_FL.DamperShaftDia);
                #endregion
                #region Populating the Input Sheet - FRONT RIGHT Damper
                M1_Global.List_I1[local_VehicleID - 1].DamperPressureFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_FR.DamperGasPressure);
                M1_Global.List_I1[local_VehicleID - 1].DamperShaftDiaFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_FR.DamperShaftDia);
                #endregion
                #region Populating the Input Sheet - REAR LEFT Damper
                M1_Global.List_I1[local_VehicleID - 1].DamperPressureRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_RL.DamperGasPressure);
                M1_Global.List_I1[local_VehicleID - 1].DamperShaftDiaRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_RL.DamperShaftDia);
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Damper
                M1_Global.List_I1[local_VehicleID - 1].DamperPressureRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_RR.DamperGasPressure);
                M1_Global.List_I1[local_VehicleID - 1].DamperShaftDiaRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.damper_RR.DamperShaftDia);
                #endregion


                #region Populating the Input Sheet - Rear Anti-Roll Bar
                M1_Global.List_I1[local_VehicleID - 1].RearAntiRollBar.Text = Convert.ToString(Vehicle.Assembled_Vehicle.arb_RL.AntiRollBarRate);
                #endregion
                #region Populating the Input Sheet - Front Anti-Roll Bar
                M1_Global.List_I1[local_VehicleID - 1].FrontAntiRollBar.Text = Convert.ToString(Vehicle.Assembled_Vehicle.arb_FL.AntiRollBarRate);
                #endregion


                #region Populating the Input Sheet Chassis Items
                M1_Global.List_I1[local_VehicleID - 1].SuspendedMass.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.SuspendedMass);

                M1_Global.List_I1[local_VehicleID - 1].SuspendedMassCoGx.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.SuspendedMassCoGx);
                M1_Global.List_I1[local_VehicleID - 1].SuspendedMassCoGy.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.SuspendedMassCoGy);
                M1_Global.List_I1[local_VehicleID - 1].SuspendedMassCoGz.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.SuspendedMassCoGz);

                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFL);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGFLx.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFLCoGx);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGFLy.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFLCoGy);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGFLz.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFLCoGz);

                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFR);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGFRx.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFRCoGx);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGFRy.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFRCoGy);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGFRz.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassFRCoGz);

                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRL);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGRLx.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRLCoGx);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGRLy.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRLCoGy);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGRLz.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRLCoGz);

                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRR);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGRRx.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRRCoGx);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGRRy.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRRCoGy);
                M1_Global.List_I1[local_VehicleID - 1].NonSuspendedMassCoGRRz.Text = Convert.ToString(Vehicle.Assembled_Vehicle.chassis_vehicle.NonSuspendedMassRRCoGz);
                #endregion


                #region Populating the Input Sheet - FRONT LEFT Wheel Alignment
                M1_Global.List_I1[local_VehicleID - 1].CamberFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_FL.StaticCamber);
                M1_Global.List_I1[local_VehicleID - 1].ToeFL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_FL.StaticToe);
                #endregion
                #region Populating the Input Sheet - FRONT RIGHT Wheel Alignment
                M1_Global.List_I1[local_VehicleID - 1].CamberFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_FR.StaticCamber);
                M1_Global.List_I1[local_VehicleID - 1].ToeFR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_FR.StaticToe);
                #endregion
                #region Populating the Input Sheet - REAR LEFT Wheel Alignment
                M1_Global.List_I1[local_VehicleID - 1].CamberRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_RL.StaticCamber);
                M1_Global.List_I1[local_VehicleID - 1].ToeRL.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_RL.StaticToe);
                #endregion
                #region Populating the Input Sheet - REAR RIGHT Wheel Alignment
                M1_Global.List_I1[local_VehicleID - 1].CamberRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_RR.StaticCamber);
                M1_Global.List_I1[local_VehicleID - 1].ToeRR.Text = Convert.ToString(Vehicle.Assembled_Vehicle.wa_RR.StaticToe);
                #endregion


                #region Populating the Input Sheet -MOTION RATIO of All Corners
                M1_Global.List_I1[local_VehicleID - 1].MotionRatioFL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FL.InitialMR)));
                M1_Global.List_I1[local_VehicleID - 1].MotionRatioFR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_FR.InitialMR)));
                M1_Global.List_I1[local_VehicleID - 1].MotionRatioRL.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RL.InitialMR)));
                M1_Global.List_I1[local_VehicleID - 1].MotionRatioRR.Text = String.Format("{0:0.000}", ((Vehicle.Assembled_Vehicle.sc_RR.InitialMR)));
                #endregion


                #region Populating the Input Sheet - Corner Weight of All Corners
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.Text = String.Format("{0:00.000}", -Vehicle.Assembled_Vehicle.oc_FL.CW);
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.Text = String.Format("{0:00.000}", -Vehicle.Assembled_Vehicle.oc_FR.CW);
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.Text = String.Format("{0:00.000}", -Vehicle.Assembled_Vehicle.oc_RL.CW);
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.Text = String.Format("{0:00.000}", -Vehicle.Assembled_Vehicle.oc_RR.CW);
                #endregion
                #endregion

            }
            catch (Exception) { }
        }
        #endregion

        #region Hide Input Sheet
        private void HideInputSheet_Click(object sender, EventArgs e)
        {
            try
            {
                int local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                M1_Global.List_I1[local_VehicleID - 1].Hide();
            }
            catch (Exception) { }
        } 
        #endregion

        #endregion
        
        private void barButtonSelectVehicle_ItemClick(object sender, ItemClickEventArgs e)
        {
            //sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            accordionControlWheelAlignment.Hide();
            accordionControlVehicleItem.Hide();
            navBarGroupSimulation.Visible = true;
            navBarGroupSimulation.Expanded = true;
            navBarGroupSimulationSelectVehicle.Expanded = true;
        }


        #region Handing the Outputs

        #region Reset of Outputs
        private void ResetOutputs()
        {
            #region Reset of Outputs of FRONT LEFT
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xFL.ResetText(); ;
            A2yFL.ResetText(); ;
            A2zFL.ResetText(); ;
            B2xFL.ResetText(); ;
            B2yFL.ResetText(); ;
            B2zFL.ResetText(); ;
            C2xFL.ResetText(); ;
            C2yFL.ResetText(); ;
            C2zFL.ResetText(); ;
            D2xFL.ResetText(); ;
            D2yFL.ResetText(); ;
            D2zFL.ResetText(); ;
            N2xFL.ResetText(); ;
            N2yFL.ResetText(); ;
            N2zFL.ResetText(); ;
            Q2xFL.ResetText(); ;
            Q2yFL.ResetText(); ;
            Q2zFL.ResetText(); ;
            I2xFL.ResetText(); ;
            I2yFL.ResetText(); ;
            I2zFL.ResetText(); ;
            JO2xFL.ResetText(); ;
            JO2yFL.ResetText(); ;
            JO2zFL.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xFL.ResetText();
            J2yFL.ResetText(); ;
            J2zFL.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xFL.ResetText(); ;
            H2yFL.ResetText();
            H2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xFL.ResetText();
            O2yFL.ResetText();
            O2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xFL.ResetText();
            G2yFL.ResetText();
            G2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xFL.ResetText();
            F2yFL.ResetText();
            F2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xFL.ResetText();
            E2yFL.ResetText();
            E2zFL.ResetText();
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioFL.ResetText();
            InitialARBMRFL.ResetText();
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioFL.ResetText();
            FinalARBMRFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xFL.ResetText();
            M2yFL.ResetText();
            M2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xFL.ResetText();
            K2yFL.ResetText();
            K2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xFL.ResetText();
            L2yFL.ResetText();
            L2zFL.ResetText();
            //To Display the New Camber and Toe
            FinalCamberFL.ResetText();
            FinalToeFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xFL.ResetText();
            P2yFL.ResetText();
            P2zFL.ResetText();
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xFL.ResetText();
            W2yFL.ResetText();
            W2zFL.ResetText();
            //Calculating The Final Ride Height 
            RideHeightFL.ResetText();
            //Calculating the New Corner Weights 
            CWFL.ResetText();
            TireLoadedRadiusFL.ResetText();
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionFL.ResetText();
            CorrectedWheelDeflectionFL.ResetText();
            NewDamperLengthFL.ResetText();
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGFLx.ResetText();
            NewNSMCGFLy.ResetText();
            NewNSMCGFLz.ResetText();
            //Calculating the Wishbone Forces
            LowerFrontFL.ResetText();
            LowerRearFL.ResetText();
            UpperFrontFL.ResetText();
            UpperRearFL.ResetText();
            PushRodFL.ResetText();
            ToeLinkFL.ResetText();
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisFLx.ResetText();
            LowerFrontChassisFLy.ResetText();
            LowerFrontChassisFLz.ResetText();
            LowerRearChassisFLx.ResetText();
            LowerRearChassisFLy.ResetText();
            LowerRearChassisFLz.ResetText();

            UpperFrontChassisFLx.ResetText();
            UpperFrontChassisFLy.ResetText();
            UpperFrontChassisFLz.ResetText();
            UpperRearChassisFLx.ResetText();
            UpperRearChassisFLy.ResetText();
            UpperRearChassisFLz.ResetText();

            PushRodChassisFLx.ResetText();
            PushRodChassisFLy.ResetText();
            PushRodChassisFLz.ResetText();
            PushRodUprightFLx.ResetText();
            PushRodUprightFLy.ResetText();
            PushRodUprightFLz.ResetText();

            DamperForceFL.ResetText();
            SpringPreloadOutputFL.ResetText();
            DamperForceChassisFLx.ResetText();
            DamperForceChassisFLy.ResetText();
            DamperForceChassisFLz.ResetText();
            DamperForceBellCrankFLx.ResetText();
            DamperForceBellCrankFLy.ResetText();
            DamperForceBellCrankFLz.ResetText();

            DroopLinkForceFL.ResetText();
            DroopLinkBellCrankFLx.ResetText();
            DroopLinkBellCrankFLy.ResetText();
            DroopLinkBellCrankFLz.ResetText();
            DroopLinkLeverFLx.ResetText();
            DroopLinkLeverFLy.ResetText();
            DroopLinkLeverFLz.ResetText();

            ToeLinkChassisFLx.ResetText();
            ToeLinkChassisFLy.ResetText();
            ToeLinkChassisFLz.ResetText();
            ToeLinkUprightFLx.ResetText();
            ToeLinkUprightFLy.ResetText();
            ToeLinkUprightFLz.ResetText();
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightFLx.ResetText();
            LowerFrontUprightFLy.ResetText();
            LowerFrontUprightFLz.ResetText();
            LowerRearUprightFLx.ResetText();
            LowerRearUprightFLy.ResetText();
            LowerRearUprightFLz.ResetText();
            UpperFrontUprightFLx.ResetText();
            UpperFrontUprightFLy.ResetText();
            UpperFrontUprightFLz.ResetText();
            UpperRearUprightFLx.ResetText();
            UpperRearUprightFLy.ResetText();
            UpperRearUprightFLz.ResetText();

            // Link Lengths
            LowerFrontLinkLengthFL.ResetText();
            LowerRearLinkLengthFL.ResetText();
            UpperFrontLinkLengthFL.ResetText();
            UpperRearLinkLengthFL.ResetText();
            PushRodLinkLengthFL.ResetText();
            ToeLinkLengthFL.ResetText();
            ARBDroopLinkLengthFL.ResetText();
            DamperLinkLengthFL.ResetText();
            ARBLeverLinkLengthFL.ResetText();

            #endregion

            #region Reset of Outputs of FRONT Right
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xFR.ResetText();
            A2yFR.ResetText();
            A2zFR.ResetText();
            B2xFR.ResetText();
            B2yFR.ResetText();
            B2zFR.ResetText();
            C2xFR.ResetText();
            C2yFR.ResetText();
            C2zFR.ResetText();
            D2xFR.ResetText();
            D2yFR.ResetText();
            D2zFR.ResetText();
            N2xFR.ResetText();
            N2yFR.ResetText();
            N2zFR.ResetText();
            Q2xFR.ResetText();
            Q2yFR.ResetText();
            Q2zFR.ResetText();
            I2xFR.ResetText();
            I2yFR.ResetText();
            I2zFR.ResetText();
            JO2xFR.ResetText();
            JO2yFR.ResetText();
            JO2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xFR.ResetText();
            J2yFR.ResetText();
            J2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xFR.ResetText();
            H2yFR.ResetText();
            H2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xFR.ResetText();
            O2yFR.ResetText();
            O2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xFR.ResetText();
            G2yFR.ResetText();
            G2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xFR.ResetText();
            F2yFR.ResetText();
            F2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xFR.ResetText();
            E2yFR.ResetText();
            E2zFR.ResetText();
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioFR.ResetText();
            InitialARBMRFR.ResetText();
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioFR.ResetText();
            FinalARBMRFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xFR.ResetText();
            M2yFR.ResetText();
            M2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xFR.ResetText();
            K2yFR.ResetText();
            K2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xFR.ResetText();
            L2yFR.ResetText();
            L2zFR.ResetText();
            //To Display the New Camber and Toe
            FinalCamberFR.ResetText();
            FinalToeFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xFR.ResetText();
            P2yFR.ResetText();
            P2zFR.ResetText();
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xFR.ResetText();
            W2yFR.ResetText();
            W2zFR.ResetText();
            //Calculating The Final Ride Height 
            RideHeightFR.ResetText();
            //Calculating the New Corner Weights 
            CWFR.ResetText();
            TireLoadedRadiusFR.ResetText();
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionFR.ResetText();
            CorrectedWheelDeflectionFR.ResetText();
            NewDamperLengthFR.ResetText();
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGFRx.ResetText();
            NewNSMCGFRy.ResetText();
            NewNSMCGFRz.ResetText();
            //Calculating the Wishbone Forces
            LowerFrontFR.ResetText();
            LowerRearFR.ResetText();
            UpperFrontFR.ResetText();
            UpperRearFR.ResetText();
            PushRodFR.ResetText();
            ToeLinkFR.ResetText();
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisFRx.ResetText();
            LowerFrontChassisFRy.ResetText();
            LowerFrontChassisFRz.ResetText();
            LowerRearChassisFRx.ResetText();
            LowerRearChassisFRy.ResetText();
            LowerRearChassisFRz.ResetText();

            UpperFrontChassisFRx.ResetText();
            UpperFrontChassisFRy.ResetText();
            UpperFrontChassisFRz.ResetText();
            UpperRearChassisFRx.ResetText();
            UpperRearChassisFRy.ResetText();
            UpperRearChassisFRz.ResetText();

            PushRodChassisFRx.ResetText();
            PushRodChassisFRy.ResetText();
            PushRodChassisFRz.ResetText();
            PushRodUprightFRx.ResetText();
            PushRodUprightFRy.ResetText();
            PushRodUprightFRz.ResetText();

            DamperForceFR.ResetText();
            SpringPreloadOutputFR.ResetText();
            DamperForceChassisFRx.ResetText();
            DamperForceChassisFRy.ResetText();
            DamperForceChassisFRz.ResetText();
            DamperForceBellCrankFRx.ResetText();
            DamperForceBellCrankFRy.ResetText();
            DamperForceBellCrankFRz.ResetText();

            DroopLinkForceFR.ResetText();
            DroopLinkBellCrankFRx.ResetText();
            DroopLinkBellCrankFRy.ResetText();
            DroopLinkBellCrankFRz.ResetText();
            DroopLinkLeverFRx.ResetText();
            DroopLinkLeverFRy.ResetText();
            DroopLinkLeverFRz.ResetText();

            ToeLinkChassisFRx.ResetText();
            ToeLinkChassisFRy.ResetText();
            ToeLinkChassisFRz.ResetText();
            ToeLinkUprightFRx.ResetText();
            ToeLinkUprightFRy.ResetText();
            ToeLinkUprightFRz.ResetText();
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightFRx.ResetText();
            LowerFrontUprightFRy.ResetText();
            LowerFrontUprightFRz.ResetText();
            LowerRearUprightFRx.ResetText();
            LowerRearUprightFRy.ResetText();
            LowerRearUprightFRz.ResetText();
            UpperFrontUprightFRx.ResetText();
            UpperFrontUprightFRy.ResetText();
            UpperFrontUprightFRz.ResetText();
            UpperRearUprightFRx.ResetText();
            UpperRearUprightFRy.ResetText();
            UpperRearUprightFRz.ResetText();

            // Link Lengths
            LowerFrontLinkLengthFR.ResetText();
            LowerRearLinkLengthFR.ResetText();
            UpperFrontLinkLengthFR.ResetText();
            UpperRearLinkLengthFR.ResetText();
            PushRodLinkLengthFR.ResetText();
            ToeLinkLengthFR.ResetText();
            ARBDroopLinkLengthFR.ResetText();
            DamperLinkLengthFR.ResetText();
            ARBLeverLinkLengthFR.ResetText();

            #endregion

            #region Reset of Outputs of Rear LEFT
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xRL.ResetText(); ;
            A2yRL.ResetText(); ;
            A2zRL.ResetText(); ;
            B2xRL.ResetText(); ;
            B2yRL.ResetText(); ;
            B2zRL.ResetText(); ;
            C2xRL.ResetText(); ;
            C2yRL.ResetText(); ;
            C2zRL.ResetText(); ;
            D2xRL.ResetText(); ;
            D2yRL.ResetText(); ;
            D2zRL.ResetText(); ;
            N2xRL.ResetText(); ;
            N2yRL.ResetText(); ;
            N2zRL.ResetText(); ;
            Q2xRL.ResetText(); ;
            Q2yRL.ResetText(); ;
            Q2zRL.ResetText(); ;
            I2xRL.ResetText(); ;
            I2yRL.ResetText(); ;
            I2zRL.ResetText(); ;
            JO2xRL.ResetText(); ;
            JO2yRL.ResetText(); ;
            JO2zRL.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xRL.ResetText(); ;
            J2yRL.ResetText(); ;
            J2zRL.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xRL.ResetText(); ;
            H2yRL.ResetText();
            H2zRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xRL.ResetText();
            O2yRL.ResetText();
            O2zRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xRL.ResetText();
            G2yRL.ResetText();
            G2zRL.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xRL.ResetText();
            F2yRL.ResetText();
            F2zRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xRL.ResetText();
            E2yRL.ResetText();
            E2zRL.ResetText();
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioRL.ResetText();
            InitialARBMRRL.ResetText();
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioRL.ResetText();
            FinalARBMRRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xRL.ResetText();
            M2yRL.ResetText();
            M2zRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xRL.ResetText();
            K2yRL.ResetText();
            K2zRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xRL.ResetText();
            L2yRL.ResetText();
            L2zRL.ResetText();
            //To Display the New Camber and Toe
            FinalCamberRL.ResetText();
            FinalToeRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xRL.ResetText();
            P2yRL.ResetText();
            P2zRL.ResetText();
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xRL.ResetText();
            W2yRL.ResetText();
            W2zRL.ResetText();
            //Calculating The Final Ride Height 
            RideHeightRL.ResetText();
            //Calculating the New Corner Weights 
            CWRL.ResetText();
            TireLoadedRadiusRL.ResetText();
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionRL.ResetText();
            CorrectedWheelDeflectionRL.ResetText();
            NewDamperLengthRL.ResetText();
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGRLx.ResetText();
            NewNSMCGRLy.ResetText();
            NewNSMCGRLz.ResetText();
            //Calculating the Wishbone Forces
            LowerFrontRL.ResetText();
            LowerRearRL.ResetText();
            UpperFrontRL.ResetText();
            UpperRearRL.ResetText();
            PushRodRL.ResetText();
            ToeLinkRL.ResetText();
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisRLx.ResetText();
            LowerFrontChassisRLy.ResetText();
            LowerFrontChassisRLz.ResetText();
            LowerRearChassisRLx.ResetText();
            LowerRearChassisRLy.ResetText();
            LowerRearChassisRLz.ResetText();

            UpperFrontChassisRLx.ResetText();
            UpperFrontChassisRLy.ResetText();
            UpperFrontChassisRLz.ResetText();
            UpperRearChassisRLx.ResetText();
            UpperRearChassisRLy.ResetText();
            UpperRearChassisRLz.ResetText();

            PushRodChassisRLx.ResetText();
            PushRodChassisRLy.ResetText();
            PushRodChassisRLz.ResetText();
            PushRodUprightRLx.ResetText();
            PushRodUprightRLy.ResetText();
            PushRodUprightRLz.ResetText();

            DamperForceRL.ResetText();
            SpringPreloadOutputRL.ResetText();
            DamperForceChassisRLx.ResetText();
            DamperForceChassisRLy.ResetText();
            DamperForceChassisRLz.ResetText();
            DamperForceBellCrankRLx.ResetText();
            DamperForceBellCrankRLy.ResetText();
            DamperForceBellCrankRLz.ResetText();

            DroopLinkForceRL.ResetText();
            DroopLinkBellCrankRLx.ResetText();
            DroopLinkBellCrankRLy.ResetText();
            DroopLinkBellCrankRLz.ResetText();
            DroopLinkLeverRLx.ResetText();
            DroopLinkLeverRLy.ResetText();
            DroopLinkLeverRLz.ResetText();

            ToeLinkChassisRLx.ResetText();
            ToeLinkChassisRLy.ResetText();
            ToeLinkChassisRLz.ResetText();
            ToeLinkUprightRLx.ResetText();
            ToeLinkUprightRLy.ResetText();
            ToeLinkUprightRLz.ResetText();
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightRLx.ResetText();
            LowerFrontUprightRLy.ResetText();
            LowerFrontUprightRLz.ResetText();
            LowerRearUprightRLx.ResetText();
            LowerRearUprightRLy.ResetText();
            LowerRearUprightRLz.ResetText();
            UpperFrontUprightRLx.ResetText();
            UpperFrontUprightRLy.ResetText();
            UpperFrontUprightRLz.ResetText();
            UpperRearUprightRLx.ResetText();
            UpperRearUprightRLy.ResetText();
            UpperRearUprightRLz.ResetText();

            // Link Lengths
            LowerFrontLinkLengthRL.ResetText();
            LowerRearLinkLengthRL.ResetText();
            UpperFrontLinkLengthRL.ResetText();
            UpperRearLinkLengthRL.ResetText();
            PushRodLinkLengthRL.ResetText();
            ToeLinkLengthRL.ResetText();
            ARBDroopLinkLengthRL.ResetText();
            DamperLinkLengthRL.ResetText();
            ARBLeverLinkLengthRL.ResetText();

            #endregion

            #region Reset of Outputs of Rear Right
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xRR.ResetText(); ;
            A2yRR.ResetText(); ;
            A2zRR.ResetText(); ;
            B2xRR.ResetText(); ;
            B2yRR.ResetText(); ;
            B2zRR.ResetText(); ;
            C2xRR.ResetText(); ;
            C2yRR.ResetText(); ;
            C2zRR.ResetText(); ;
            D2xRR.ResetText(); ;
            D2yRR.ResetText(); ;
            D2zRR.ResetText(); ;
            N2xRR.ResetText(); ;
            N2yRR.ResetText(); ;
            N2zRR.ResetText(); ;
            Q2xRR.ResetText(); ;
            Q2yRR.ResetText(); ;
            Q2zRR.ResetText(); ;
            I2xRR.ResetText(); ;
            I2yRR.ResetText(); ;
            I2zRR.ResetText(); ;
            JO2xRR.ResetText(); ;
            JO2yRR.ResetText(); ;
            JO2zRR.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xRR.ResetText(); ;
            J2yRR.ResetText(); ;
            J2zRR.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xRR.ResetText(); ;
            H2yRR.ResetText();
            H2zRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xRR.ResetText();
            O2yRR.ResetText();
            O2zRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xRR.ResetText();
            G2yRR.ResetText();
            G2zRR.ResetText(); ;
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xRR.ResetText();
            F2yRR.ResetText();
            F2zRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xRR.ResetText();
            E2yRR.ResetText();
            E2zRR.ResetText();
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioRR.ResetText();
            InitialARBMRRR.ResetText();
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioRR.ResetText();
            FinalARBMRRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xRR.ResetText();
            M2yRR.ResetText();
            M2zRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xRR.ResetText();
            K2yRR.ResetText();
            K2zRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xRR.ResetText();
            L2yRR.ResetText();
            L2zRR.ResetText();
            //To Display the New Camber and Toe
            FinalCamberRR.ResetText();
            FinalToeRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xRR.ResetText();
            P2yRR.ResetText();
            P2zRR.ResetText();
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xRR.ResetText();
            W2yRR.ResetText();
            W2zRR.ResetText();
            //Calculating The Final Ride Height 
            RideHeightRR.ResetText();
            //Calculating the New Corner Weights 
            CWRR.ResetText();
            TireLoadedRadiusRR.ResetText();
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionRR.ResetText();
            CorrectedWheelDeflectionRR.ResetText();
            NewDamperLengthRR.ResetText();
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGRRx.ResetText();
            NewNSMCGRRy.ResetText();
            NewNSMCGRRz.ResetText();
            //Calculating the Wishbone Forces
            LowerFrontRR.ResetText();
            LowerRearRR.ResetText();
            UpperFrontRR.ResetText();
            UpperRearRR.ResetText();
            PushRodRR.ResetText();
            ToeLinkRR.ResetText();
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisRRx.ResetText();
            LowerFrontChassisRRy.ResetText();
            LowerFrontChassisRRz.ResetText();
            LowerRearChassisRRx.ResetText();
            LowerRearChassisRRy.ResetText();
            LowerRearChassisRRz.ResetText();

            UpperFrontChassisRRx.ResetText();
            UpperFrontChassisRRy.ResetText();
            UpperFrontChassisRRz.ResetText();
            UpperRearChassisRRx.ResetText();
            UpperRearChassisRRy.ResetText();
            UpperRearChassisRRz.ResetText();

            PushRodChassisRRx.ResetText();
            PushRodChassisRRy.ResetText();
            PushRodChassisRRz.ResetText();
            PushRodUprightRRx.ResetText();
            PushRodUprightRRy.ResetText();
            PushRodUprightRRz.ResetText();

            DamperForceRR.ResetText();
            SpringPreloadOutputRR.ResetText();
            DamperForceChassisRRx.ResetText();
            DamperForceChassisRRy.ResetText();
            DamperForceChassisRRz.ResetText();
            DamperForceBellCrankRRx.ResetText();
            DamperForceBellCrankRRy.ResetText();
            DamperForceBellCrankRRz.ResetText();

            DroopLinkForceRR.ResetText();
            DroopLinkBellCrankRRx.ResetText();
            DroopLinkBellCrankRRy.ResetText();
            DroopLinkBellCrankRRz.ResetText();
            DroopLinkLeverRRx.ResetText();
            DroopLinkLeverRRy.ResetText();
            DroopLinkLeverRRz.ResetText();

            ToeLinkChassisRRx.ResetText();
            ToeLinkChassisRRy.ResetText();
            ToeLinkChassisRRz.ResetText();
            ToeLinkUprightRRx.ResetText();
            ToeLinkUprightRRy.ResetText();
            ToeLinkUprightRRz.ResetText();
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightRRx.ResetText();
            LowerFrontUprightRRy.ResetText();
            LowerFrontUprightRRz.ResetText();
            LowerRearUprightRRx.ResetText();
            LowerRearUprightRRy.ResetText();
            LowerRearUprightRRz.ResetText();
            UpperFrontUprightRRx.ResetText();
            UpperFrontUprightRRy.ResetText();
            UpperFrontUprightRRz.ResetText();
            UpperRearUprightRRx.ResetText();
            UpperRearUprightRRy.ResetText();
            UpperRearUprightRRz.ResetText();

            // Link Lengths
            LowerFrontLinkLengthRR.ResetText();
            LowerRearLinkLengthRR.ResetText();
            UpperFrontLinkLengthRR.ResetText();
            UpperRearLinkLengthRR.ResetText();
            PushRodLinkLengthRR.ResetText();
            ToeLinkLengthRR.ResetText();
            ARBDroopLinkLengthRR.ResetText();
            DamperLinkLengthRR.ResetText();
            ARBLeverLinkLengthRR.ResetText();

            #endregion

            #region Reset of Vehicle Level Outputs
            NewWheelBase.ResetText();
            NewTrackFront.ResetText();
            NewTrackRear.ResetText();
            NewSuspendedMassCGx.ResetText();
            NewSuspendedMassCGz.ResetText();
            NewSuspendedMassCGy.ResetText();
            RollAngleChassis.ResetText();
            PitchAngleChassis.ResetText();
            ARBMotionRatioFront.ResetText();
            ARBMotionRatioRear.ResetText();
            #endregion

        } 
        #endregion

        #region Display of Outputs
        private void DisplayOutputs()
        {
            try
            {
                #region Display of Outputs

                #region Display of Outputs of FRONT LEFT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.A2x))); A2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.A2x);
                A2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.A2y))); A2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.A2y);
                A2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.A2z))); A2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.A2z);
                B2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.B2x))); B2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.B2x);
                B2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.B2y))); B2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.B2y);
                B2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.B2z))); B2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.B2z);
                C2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.C2x))); C2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.C2x);
                C2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.C2y))); C2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.C2y);
                C2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.C2z))); C2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.C2z);
                D2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.D2x))); D2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.D2x);
                D2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.D2y))); D2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.D2y);
                D2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.D2z))); D2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.D2z);
                N2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.N2x))); N2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.N2x);
                N2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.N2y))); N2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.N2y);
                N2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.N2z))); N2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.N2z);
                Q2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.Q2x))); Q2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Q2x);
                Q2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.Q2y))); Q2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Q2y);
                Q2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.Q2z))); Q2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Q2z);
                I2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.I2x))); I2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.I2x);
                I2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.I2y))); I2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.I2y);
                I2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.I2z))); I2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.I2z);
                JO2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.JO2x))); JO2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.JO2x);
                JO2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.JO2y))); JO2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.JO2y);
                JO2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.JO2z))); JO2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.J2x))); J2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.J2x);
                J2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.J2y))); J2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.J2y);
                J2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.J2z))); J2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.H2x))); H2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.H2x);
                H2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.H2y))); H2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.H2y);
                H2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.H2z))); H2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.O2x))); O2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.O2x);
                O2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.O2y))); O2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.O2y);
                O2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.O2z))); O2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.G2x))); G2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.G2x);
                G2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.G2y))); G2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.G2y);
                G2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.G2z))); G2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.F2x))); F2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.F2x);
                F2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.F2y))); F2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.F2y);
                F2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.F2z))); F2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.E2x))); E2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.E2x);
                E2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.E2y))); E2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.E2y);
                E2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.E2z))); E2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.InitialMR))); MotionRatioFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.InitialMR);
                InitialARBMRFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.FinalMR))); FinalMotionRatioFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.FinalMR);
                FinalARBMRFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.M2x))); M2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.M2x);
                M2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.M2y))); M2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.M2y);
                M2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.M2z))); M2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.K2x))); K2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.K2x);
                K2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.K2y))); K2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.K2y);
                K2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.K2z))); K2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.L2x))); L2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.L2x);
                L2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.L2y))); L2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.L2y);
                L2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.L2z))); L2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.L2z);
                //To Display the New Camber and Toe
                FinalCamberFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.FinalCamber))); FinalCamberFL.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_FL.FinalCamber * (180 / Math.PI)));
                FinalToeFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.FinalToe))); FinalToeFL.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_FL.FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.P2x))); P2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.P2x);
                P2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.P2y))); P2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.P2y);
                P2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.P2z))); P2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.W2x))); W2xFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.W2x);
                W2yFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.W2y))); W2yFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.W2y);
                W2zFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.W2z))); W2zFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.W2z);
                //Calculating The Final Ride Height 
                RideHeightFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight))); RideHeightFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight);
                //Calculating the New Corner Weights 
                CWFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.CW))); CWFL.Text = String.Format("{0:0.000}", -Vehicle.Assembled_Vehicle.oc_FL.CW);
                TireLoadedRadiusFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.TireLoadedRadius))); TireLoadedRadiusFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.Corrected_SpringDeflection))); CorrectedSpringDeflectionFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Corrected_SpringDeflection);
                CorrectedWheelDeflectionFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.Corrected_WheelDeflection))); CorrectedWheelDeflectionFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.Corrected_WheelDeflection);
                NewDamperLengthFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.DamperLength))); NewDamperLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.New_NonSuspendedMassCoGx))); NewNSMCGFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.New_NonSuspendedMassCoGx);
                NewNSMCGFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.New_NonSuspendedMassCoGy))); NewNSMCGFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.New_NonSuspendedMassCoGy);
                NewNSMCGFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.New_NonSuspendedMassCoGz))); NewNSMCGFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerFront))); LowerFrontFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerFront);
                LowerRearFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerRear))); LowerRearFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerRear);
                UpperFrontFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperFront))); UpperFrontFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperFront);
                UpperRearFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperRear))); UpperRearFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperRear);
                PushRodFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod))); PushRodFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod);
                ToeLinkFL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink))); ToeLinkFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerFront_x))); LowerFrontChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerFront_x);
                LowerFrontChassisFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerFront_y))); LowerFrontChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerFront_y);
                LowerFrontChassisFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerFront_z))); LowerFrontChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerFront_z);
                LowerRearChassisFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerRear_x))); LowerRearChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerRear_x);
                LowerRearChassisFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerRear_y))); LowerRearChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerRear_y);
                LowerRearChassisFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LowerRear_z))); LowerRearChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LowerRear_z);

                UpperFrontChassisFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperFront_x))); UpperFrontChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperFront_x);
                UpperFrontChassisFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperFront_y))); UpperFrontChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperFront_y);
                UpperFrontChassisFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperFront_z))); UpperFrontChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperFront_z);
                UpperRearChassisFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperRear_x))); UpperRearChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperRear_x);
                UpperRearChassisFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperRear_y))); UpperRearChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperRear_y);
                UpperRearChassisFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UpperRear_z))); UpperRearChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UpperRear_z);

                PushRodChassisFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod_x))); PushRodChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod_x);
                PushRodChassisFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod_y))); PushRodChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod_y);
                PushRodChassisFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod_z))); PushRodChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod_z);
                PushRodUprightFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod_x))); PushRodUprightFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod_x);
                PushRodUprightFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod_y))); PushRodUprightFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod_y);
                PushRodUprightFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.PushRod_z))); PushRodUprightFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.PushRod_z);

                DamperForceFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce);
                SpringPreloadOutputFL.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                DamperForceChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce_x);
                DamperForceChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce_y);
                DamperForceChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce_z);
                DamperForceBellCrankFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce_x);
                DamperForceBellCrankFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce_y);
                DamperForceBellCrankFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.DamperForce_z);

                DroopLinkForceFL.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink);
                DroopLinkBellCrankFLx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink_x);
                DroopLinkBellCrankFLy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink_y);
                DroopLinkBellCrankFLz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink_z);
                DroopLinkLeverFLx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink_x);
                DroopLinkLeverFLy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink_y);
                DroopLinkLeverFLz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FL.ARBDroopLink_z);

                ToeLinkChassisFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink_x))); ToeLinkChassisFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink_x);
                ToeLinkChassisFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink_y))); ToeLinkChassisFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink_y);
                ToeLinkChassisFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink_z))); ToeLinkChassisFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink_z);
                ToeLinkUprightFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink_x))); ToeLinkUprightFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink_x);
                ToeLinkUprightFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink_y))); ToeLinkUprightFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink_y);
                ToeLinkUprightFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.ToeLink_z))); ToeLinkUprightFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LBJ_x))); LowerFrontUprightFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LBJ_x);
                LowerFrontUprightFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LBJ_y))); LowerFrontUprightFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LBJ_y);
                LowerFrontUprightFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LBJ_z))); LowerFrontUprightFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LBJ_z);
                LowerRearUprightFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LBJ_x))); LowerRearUprightFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LBJ_x);
                LowerRearUprightFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LBJ_y))); LowerRearUprightFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LBJ_y);
                LowerRearUprightFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.LBJ_z))); LowerRearUprightFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.LBJ_z);
                UpperFrontUprightFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UBJ_x))); UpperFrontUprightFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UBJ_x);
                UpperFrontUprightFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UBJ_y))); UpperFrontUprightFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UBJ_y);
                UpperFrontUprightFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UBJ_z))); UpperFrontUprightFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UBJ_z);
                UpperRearUprightFLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UBJ_x))); UpperRearUprightFLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UBJ_x);
                UpperRearUprightFLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UBJ_y))); UpperRearUprightFLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UBJ_y);
                UpperRearUprightFLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FL.UBJ_z))); UpperRearUprightFLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FL.UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.LowerFrontLength);
                LowerRearLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.LowerRearLength);
                UpperFrontLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.UpperFrontLength);
                UpperRearLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.UpperRearLength);
                PushRodLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.PushRodLength);
                ToeLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.ToeLinkLength);
                ARBDroopLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.ARBDroopLinkLength);
                DamperLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.DamperLength);
                ARBLeverLinkLengthFL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FL.ARBBladeLength);

                #endregion

                #region Display of Outputs of FRONT RIGHT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.A2x))); A2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.A2x);
                A2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.A2y))); A2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.A2y);
                A2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.A2z))); A2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.A2z);
                B2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.B2x))); B2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.B2x);
                B2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.B2y))); B2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.B2y);
                B2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.B2z))); B2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.B2z);
                C2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.C2x))); C2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.C2x);
                C2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.C2y))); C2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.C2y);
                C2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.C2z))); C2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.C2z);
                D2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.D2x))); D2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.D2x);
                D2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.D2y))); D2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.D2y);
                D2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.D2z))); D2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.D2z);
                N2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.N2x))); N2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.N2x);
                N2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.N2y))); N2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.N2y);
                N2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.N2z))); N2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.N2z);
                Q2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.Q2x))); Q2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Q2x);
                Q2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.Q2y))); Q2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Q2y);
                Q2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.Q2z))); Q2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Q2z);
                I2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.I2x))); I2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.I2x);
                I2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.I2y))); I2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.I2y);
                I2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.I2z))); I2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.I2z);
                JO2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.JO2x))); JO2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.JO2x);
                JO2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.JO2y))); JO2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.JO2y);
                JO2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.JO2z))); JO2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.J2x))); J2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.J2x);
                J2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.J2y))); J2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.J2y);
                J2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.J2z))); J2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.H2x))); H2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.H2x);
                H2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.H2y))); H2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.H2y);
                H2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.H2z))); H2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.O2x))); O2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.O2x);
                O2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.O2y))); O2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.O2y);
                O2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.O2z))); O2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.G2x))); G2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.G2x);
                G2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.G2y))); G2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.G2y);
                G2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.G2z))); G2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.F2x))); F2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.F2x);
                F2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.F2y))); F2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.F2y);
                F2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.F2z))); F2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.E2x))); E2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.E2x);
                E2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.E2y))); E2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.E2y);
                E2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.E2z))); E2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.InitialMR))); MotionRatioFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.InitialMR);
                InitialARBMRFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.FinalMR))); FinalMotionRatioFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.FinalMR);
                FinalARBMRFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.M2x))); M2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.M2x);
                M2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.M2y))); M2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.M2y);
                M2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.M2z))); M2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.K2x))); K2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.K2x);
                K2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.K2y))); K2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.K2y);
                K2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.K2z))); K2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.L2x))); L2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.L2x);
                L2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.L2y))); L2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.L2y);
                L2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.L2z))); L2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.L2z);
                //To Display the New Camber and Toe
                FinalCamberFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.FinalCamber))); FinalCamberFR.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_FR.FinalCamber * (180 / Math.PI)));
                FinalToeFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.FinalToe))); FinalToeFR.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_FR.FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.P2x))); P2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.P2x);
                P2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.P2y))); P2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.P2y);
                P2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.P2z))); P2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.W2x))); W2xFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.W2x);
                W2yFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.W2y))); W2yFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.W2y);
                W2zFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.W2z))); W2zFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.W2z);
                //Calculating The Final Ride Height 
                RideHeightFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight))); RideHeightFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight);
                //Calculating the New Corner Weights
                CWFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.CW))); CWFR.Text = String.Format("{0:0.000}", -Vehicle.Assembled_Vehicle.oc_FR.CW);
                TireLoadedRadiusFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.TireLoadedRadius))); TireLoadedRadiusFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.Corrected_SpringDeflection))); CorrectedSpringDeflectionFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Corrected_SpringDeflection);
                CorrectedWheelDeflectionFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.Corrected_WheelDeflection))); CorrectedWheelDeflectionFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.Corrected_WheelDeflection);
                NewDamperLengthFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.DamperLength))); NewDamperLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.New_NonSuspendedMassCoGx))); NewNSMCGFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.New_NonSuspendedMassCoGx);
                NewNSMCGFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.New_NonSuspendedMassCoGy))); NewNSMCGFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.New_NonSuspendedMassCoGy);
                NewNSMCGFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.New_NonSuspendedMassCoGz))); NewNSMCGFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerFront))); LowerFrontFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerFront);
                LowerRearFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerRear))); LowerRearFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerRear);
                UpperFrontFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperFront))); UpperFrontFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperFront);
                UpperRearFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperRear))); UpperRearFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperRear);
                PushRodFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod))); PushRodFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod);
                ToeLinkFR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink))); ToeLinkFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerFront_x))); LowerFrontChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerFront_x);
                LowerFrontChassisFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerFront_y))); LowerFrontChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerFront_y);
                LowerFrontChassisFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerFront_z))); LowerFrontChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerFront_z);
                LowerRearChassisFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerRear_x))); LowerRearChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerRear_x);
                LowerRearChassisFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerRear_y))); LowerRearChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerRear_y);
                LowerRearChassisFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LowerRear_z))); LowerRearChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LowerRear_z);
                UpperFrontChassisFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperFront_x))); UpperFrontChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperFront_x);
                UpperFrontChassisFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperFront_y))); UpperFrontChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperFront_y);
                UpperFrontChassisFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperFront_z))); UpperFrontChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperFront_z);
                UpperRearChassisFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperRear_x))); UpperRearChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperRear_x);
                UpperRearChassisFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperRear_y))); UpperRearChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperRear_y);
                UpperRearChassisFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UpperRear_z))); UpperRearChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UpperRear_z);
                PushRodChassisFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod_x))); PushRodChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod_x);
                PushRodChassisFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod_y))); PushRodChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod_y);
                PushRodChassisFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod_z))); PushRodChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod_z);
                PushRodUprightFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod_x))); PushRodUprightFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod_x);
                PushRodUprightFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod_y))); PushRodUprightFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod_y);
                PushRodUprightFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.PushRod_z))); PushRodUprightFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.PushRod_z);
                DamperForceFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce);
                SpringPreloadOutputFR.Text = String.Format("{0:0.000}", Spring.Assy_Spring[1].SpringPreload * Spring.Assy_Spring[1].PreloadForce);
                DamperForceChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce_x);
                DamperForceChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce_y);
                DamperForceChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce_z);
                DamperForceBellCrankFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce_x);
                DamperForceBellCrankFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce_y);
                DamperForceBellCrankFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.DamperForce_z);

                DroopLinkForceFR.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink);
                DroopLinkBellCrankFRx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink_x);
                DroopLinkBellCrankFRy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink_y);
                DroopLinkBellCrankFRz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink_z);
                DroopLinkLeverFRx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink_x);
                DroopLinkLeverFRy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink_y);
                DroopLinkLeverFRz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_FR.ARBDroopLink_z);

                ToeLinkChassisFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink_x))); ToeLinkChassisFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink_x);
                ToeLinkChassisFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink_y))); ToeLinkChassisFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink_y);
                ToeLinkChassisFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink_z))); ToeLinkChassisFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink_z);
                ToeLinkUprightFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink_x))); ToeLinkUprightFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink_x);
                ToeLinkUprightFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink_y))); ToeLinkUprightFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink_y);
                ToeLinkUprightFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.ToeLink_z))); ToeLinkUprightFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LBJ_x))); LowerFrontUprightFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LBJ_x);
                LowerFrontUprightFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LBJ_y))); LowerFrontUprightFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LBJ_y);
                LowerFrontUprightFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LBJ_z))); LowerFrontUprightFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LBJ_z);
                LowerRearUprightFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LBJ_x))); LowerRearUprightFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LBJ_x);
                LowerRearUprightFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LBJ_y))); LowerRearUprightFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LBJ_y);
                LowerRearUprightFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.LBJ_z))); LowerRearUprightFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.LBJ_z);
                UpperFrontUprightFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UBJ_x))); UpperFrontUprightFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UBJ_x);
                UpperFrontUprightFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UBJ_y))); UpperFrontUprightFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UBJ_y);
                UpperFrontUprightFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UBJ_z))); UpperFrontUprightFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UBJ_z);
                UpperRearUprightFRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UBJ_x))); UpperRearUprightFRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UBJ_x);
                UpperRearUprightFRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UBJ_y))); UpperRearUprightFRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UBJ_y);
                UpperRearUprightFRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_FR.UBJ_z))); UpperRearUprightFRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.LowerFrontLength);
                LowerRearLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.LowerRearLength);
                UpperFrontLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.UpperFrontLength);
                UpperRearLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.UpperRearLength);
                PushRodLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.PushRodLength);
                ToeLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.ToeLinkLength);
                ARBDroopLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.ARBDroopLinkLength);
                DamperLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.DamperLength);
                ARBLeverLinkLengthFR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_FR.ARBBladeLength);

                #endregion

                #region Display of Outputs of REAR LEFT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.A2x))); A2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.A2x);
                A2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.A2y))); A2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.A2y);
                A2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.A2z))); A2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.A2z);
                B2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.B2x))); B2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.B2x);
                B2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.B2y))); B2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.B2y);
                B2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.B2z))); B2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.B2z);
                C2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.C2x))); C2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.C2x);
                C2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.C2y))); C2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.C2y);
                C2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.C2z))); C2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.C2z);
                D2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.D2x))); D2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.D2x);
                D2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.D2y))); D2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.D2y);
                D2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.D2z))); D2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.D2z);
                N2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.N2x))); N2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.N2x);
                N2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.N2y))); N2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.N2y);
                N2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.N2z))); N2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.N2z);
                Q2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.Q2x))); Q2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Q2x);
                Q2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.Q2y))); Q2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Q2y);
                Q2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.Q2z))); Q2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Q2z);
                I2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.I2x))); I2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.I2x);
                I2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.I2y))); I2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.I2y);
                I2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.I2z))); I2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.I2z);
                JO2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.JO2x))); JO2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.JO2x);
                JO2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.JO2y))); JO2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.JO2y);
                JO2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.JO2z))); JO2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.J2x))); J2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.J2x);
                J2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.J2y))); J2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.J2y);
                J2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.J2z))); J2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.H2x))); H2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.H2x);
                H2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.H2y))); H2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.H2y);
                H2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.H2z))); H2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.O2x))); O2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.O2x);
                O2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.O2y))); O2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.O2y);
                O2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.O2z))); O2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.G2x))); G2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.G2x);
                G2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.G2y))); G2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.G2y);
                G2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.G2z))); G2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.F2x))); F2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.F2x);
                F2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.F2y))); F2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.F2y);
                F2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.F2z))); F2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.E2x))); E2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.E2x);
                E2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.E2y))); E2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.E2y);
                E2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.E2z))); E2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.InitialMR))); MotionRatioRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.InitialMR);
                InitialARBMRRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.FinalMR))); FinalMotionRatioRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.FinalMR);
                FinalARBMRRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.M2x))); M2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.M2x);
                M2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.M2y))); M2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.M2y);
                M2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.M2z))); M2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.K2x))); K2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.K2x);
                K2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.K2y))); K2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.K2y);
                K2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.K2z))); K2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.L2x))); L2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.L2x);
                L2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.L2y))); L2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.L2y);
                L2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.L2z))); L2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.L2z);
                //To Display the New Camber and Toe
                FinalCamberRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.FinalCamber))); FinalCamberRL.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_RL.FinalCamber * (180 / Math.PI)));
                FinalToeRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.FinalToe))); FinalToeRL.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_RL.FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.P2x))); P2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.P2x);
                P2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.P2y))); P2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.P2y);
                P2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.P2z))); P2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.W2x))); W2xRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.W2x);
                W2yRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.W2y))); W2yRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.W2y);
                W2zRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.W2z))); W2zRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.W2z);
                //Calculating The Final Ride Height 
                RideHeightRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight))); RideHeightRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight);
                //Calculating the New Corner Weights
                CWRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.CW))); CWRL.Text = String.Format("{0:0.000}", -Vehicle.Assembled_Vehicle.oc_RL.CW);
                TireLoadedRadiusRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.TireLoadedRadius))); TireLoadedRadiusRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.Corrected_SpringDeflection))); CorrectedSpringDeflectionRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Corrected_SpringDeflection);
                CorrectedWheelDeflectionRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.Corrected_WheelDeflection))); CorrectedWheelDeflectionRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.Corrected_WheelDeflection);
                NewDamperLengthRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.DamperLength))); NewDamperLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.New_NonSuspendedMassCoGx))); NewNSMCGRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.New_NonSuspendedMassCoGx);
                NewNSMCGRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.New_NonSuspendedMassCoGy))); NewNSMCGRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.New_NonSuspendedMassCoGy);
                NewNSMCGRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.New_NonSuspendedMassCoGz))); NewNSMCGRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerFront))); LowerFrontRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerFront);
                LowerRearRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerRear))); LowerRearRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerRear);
                UpperFrontRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperFront))); UpperFrontRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperFront);
                UpperRearRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperRear))); UpperRearRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperRear);
                PushRodRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod))); PushRodRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod);
                ToeLinkRL.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink))); ToeLinkRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerFront_x))); LowerFrontChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerFront_x);
                LowerFrontChassisRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerFront_y))); LowerFrontChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerFront_y);
                LowerFrontChassisRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerFront_z))); LowerFrontChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerFront_z);
                LowerRearChassisRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerRear_x))); LowerRearChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerRear_x);
                LowerRearChassisRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerRear_y))); LowerRearChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerRear_y);
                LowerRearChassisRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LowerRear_z))); LowerRearChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LowerRear_z);
                UpperFrontChassisRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperFront_x))); UpperFrontChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperFront_x);
                UpperFrontChassisRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperFront_y))); UpperFrontChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperFront_y);
                UpperFrontChassisRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperFront_z))); UpperFrontChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperFront_z);
                UpperRearChassisRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperRear_x))); UpperRearChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperRear_x);
                UpperRearChassisRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperRear_y))); UpperRearChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperRear_y);
                UpperRearChassisRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UpperRear_z))); UpperRearChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UpperRear_z);
                PushRodChassisRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod_x))); PushRodChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod_x);
                PushRodChassisRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod_y))); PushRodChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod_y);
                PushRodChassisRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod_z))); PushRodChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod_z);
                PushRodUprightRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod_x))); PushRodUprightRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod_x);
                PushRodUprightRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod_y))); PushRodUprightRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod_y);
                PushRodUprightRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.PushRod_z))); PushRodUprightRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.PushRod_z);
                DamperForceRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce);
                SpringPreloadOutputRL.Text = String.Format("{0:0.000}", Spring.Assy_Spring[2].SpringPreload * Spring.Assy_Spring[2].PreloadForce);
                DamperForceChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce_x);
                DamperForceChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce_y);
                DamperForceChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce_z);
                DamperForceBellCrankRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce_x);
                DamperForceBellCrankRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce_y);
                DamperForceBellCrankRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.DamperForce_z);

                DroopLinkForceRL.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink);
                DroopLinkBellCrankRLx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink_x);
                DroopLinkBellCrankRLy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink_y);
                DroopLinkBellCrankRLz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink_z);
                DroopLinkLeverRLx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink_x);
                DroopLinkLeverRLy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink_y);
                DroopLinkLeverRLz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RL.ARBDroopLink_z);

                ToeLinkChassisRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink_x))); ToeLinkChassisRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink_x);
                ToeLinkChassisRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink_y))); ToeLinkChassisRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink_y);
                ToeLinkChassisRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink_z))); ToeLinkChassisRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink_z);
                ToeLinkUprightRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink_x))); ToeLinkUprightRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink_x);
                ToeLinkUprightRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink_y))); ToeLinkUprightRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink_y);
                ToeLinkUprightRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.ToeLink_z))); ToeLinkUprightRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LBJ_x))); LowerFrontUprightRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LBJ_x);
                LowerFrontUprightRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LBJ_y))); LowerFrontUprightRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LBJ_y);
                LowerFrontUprightRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LBJ_z))); LowerFrontUprightRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LBJ_z);
                LowerRearUprightRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LBJ_x))); LowerRearUprightRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LBJ_x);
                LowerRearUprightRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LBJ_y))); LowerRearUprightRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LBJ_y);
                LowerRearUprightRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.LBJ_z))); LowerRearUprightRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.LBJ_z);
                UpperFrontUprightRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UBJ_x))); UpperFrontUprightRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UBJ_x);
                UpperFrontUprightRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UBJ_y))); UpperFrontUprightRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UBJ_y);
                UpperFrontUprightRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UBJ_z))); UpperFrontUprightRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UBJ_z);
                UpperRearUprightRLx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UBJ_x))); UpperRearUprightRLx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UBJ_x);
                UpperRearUprightRLy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UBJ_y))); UpperRearUprightRLy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UBJ_y);
                UpperRearUprightRLz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RL.UBJ_z))); UpperRearUprightRLz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.LowerFrontLength);
                LowerRearLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.LowerRearLength);
                UpperFrontLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.UpperFrontLength);
                UpperRearLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.UpperRearLength);
                PushRodLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.PushRodLength);
                ToeLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.ToeLinkLength);
                ARBDroopLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.ARBDroopLinkLength);
                DamperLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.DamperLength);
                ARBLeverLinkLengthRL.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.ARBBladeLength);



                #endregion

                #region Display of Outputs of REAR RIGHT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.A2x))); A2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.A2x);
                A2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.A2y))); A2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.A2y);
                A2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.A2z))); A2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.A2z);
                B2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.B2x))); B2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.B2x);
                B2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.B2y))); B2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.B2y);
                B2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.B2z))); B2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.B2z);
                C2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.C2x))); C2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.C2x);
                C2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.C2y))); C2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.C2y);
                C2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.C2z))); C2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.C2z);
                D2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.D2x))); D2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.D2x);
                D2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.D2y))); D2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.D2y);
                D2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.D2z))); D2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.D2z);
                N2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.N2x))); N2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.N2x);
                N2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.N2y))); N2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.N2y);
                N2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.N2z))); N2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.N2z);
                Q2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.Q2x))); Q2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Q2x);
                Q2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.Q2y))); Q2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Q2y);
                Q2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.Q2z))); Q2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Q2z);
                I2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.I2x))); I2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.I2x);
                I2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.I2y))); I2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.I2y);
                I2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.I2z))); I2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.I2z);
                JO2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.JO2x))); JO2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.JO2x);
                JO2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.JO2y))); JO2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.JO2y);
                JO2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.JO2z))); JO2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.J2x))); J2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.J2x);
                J2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.J2y))); J2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.J2y);
                J2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.J2z))); J2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.H2x))); H2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.H2x);
                H2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.H2y))); H2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.H2y);
                H2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.H2z))); H2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.O2x))); O2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.O2x);
                O2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.O2y))); O2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.O2y);
                O2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.O2z))); O2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.G2x))); G2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.G2x);
                G2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.G2y))); G2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.G2y);
                G2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.G2z))); G2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.F2x))); F2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.F2x);
                F2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.F2y))); F2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.F2y);
                F2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.F2z))); F2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.E2x))); E2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.E2x);
                E2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.E2y))); E2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.E2y);
                E2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.E2z))); E2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.InitialMR))); MotionRatioRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.InitialMR);
                InitialARBMRRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.FinalMR))); FinalMotionRatioRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.FinalMR);
                FinalARBMRRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.M2x))); M2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.M2x);
                M2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.M2y))); M2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.M2y);
                M2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.M2z))); M2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.K2x))); K2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.K2x);
                K2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.K2y))); K2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.K2y);
                K2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.K2z))); K2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.L2x))); L2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.L2x);
                L2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.L2y))); L2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.L2y);
                L2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.L2z))); L2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.L2z);
                //To Display the New Camber and Toe
                FinalCamberRR.Text = Convert.ToString(((-Vehicle.Assembled_Vehicle.oc_RR.FinalCamber))); FinalCamberRR.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_RR.FinalCamber * (180 / Math.PI)));
                FinalToeRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.FinalToe))); FinalToeRR.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.oc_RR.FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.P2x))); P2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.P2x);
                P2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.P2y))); P2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.P2y);
                P2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.P2z))); P2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.W2x))); W2xRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.W2x);
                W2yRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.W2y))); W2yRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.W2y);
                W2zRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.W2z))); W2zRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.W2z);
                //Calculating The Final Ride Height 
                RideHeightRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight))); RideHeightRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight);
                //Calculating the New Corner Weights
                CWRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.CW))); CWRR.Text = String.Format("{0:0.000}", -Vehicle.Assembled_Vehicle.oc_RR.CW);
                TireLoadedRadiusRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.TireLoadedRadius))); TireLoadedRadiusRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.Corrected_SpringDeflection))); CorrectedSpringDeflectionRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Corrected_SpringDeflection);
                CorrectedWheelDeflectionRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.Corrected_WheelDeflection))); CorrectedWheelDeflectionRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.Corrected_WheelDeflection);
                NewDamperLengthRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.DamperLength))); NewDamperLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.New_NonSuspendedMassCoGx))); NewNSMCGRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.New_NonSuspendedMassCoGx);
                NewNSMCGRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.New_NonSuspendedMassCoGy))); NewNSMCGRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.New_NonSuspendedMassCoGy);
                NewNSMCGRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.New_NonSuspendedMassCoGz))); NewNSMCGRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerFront))); LowerFrontRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerFront);
                LowerRearRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerRear))); LowerRearRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerRear);
                UpperFrontRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperFront))); UpperFrontRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperFront);
                UpperRearRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperRear))); UpperRearRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperRear);
                PushRodRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod))); PushRodRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod);
                ToeLinkRR.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink))); ToeLinkRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerFront_x))); LowerFrontChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerFront_x);
                LowerFrontChassisRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerFront_y))); LowerFrontChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerFront_y);
                LowerFrontChassisRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerFront_z))); LowerFrontChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerFront_z);
                LowerRearChassisRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerRear_x))); LowerRearChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerRear_x);
                LowerRearChassisRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerRear_y))); LowerRearChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerRear_y);
                LowerRearChassisRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LowerRear_z))); LowerRearChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LowerRear_z);
                UpperFrontChassisRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperFront_x))); UpperFrontChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperFront_x);
                UpperFrontChassisRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperFront_y))); UpperFrontChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperFront_y);
                UpperFrontChassisRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperFront_z))); UpperFrontChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperFront_z);
                UpperRearChassisRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperRear_x))); UpperRearChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperRear_x);
                UpperRearChassisRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperRear_y))); UpperRearChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperRear_y);
                UpperRearChassisRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UpperRear_z))); UpperRearChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UpperRear_z);
                PushRodChassisRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod_x))); PushRodChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod_x);
                PushRodChassisRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod_y))); PushRodChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod_y);
                PushRodChassisRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod_z))); PushRodChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod_z);
                PushRodUprightRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod_x))); PushRodUprightRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod_x);
                PushRodUprightRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod_y))); PushRodUprightRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod_y);
                PushRodUprightRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.PushRod_z))); PushRodUprightRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.PushRod_z);
                DamperForceRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce);
                SpringPreloadOutputRR.Text = String.Format("{0:0.000}", Spring.Assy_Spring[3].SpringPreload * Spring.Assy_Spring[3].PreloadForce);
                DamperForceChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce_x);
                DamperForceChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce_y);
                DamperForceChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce_z);
                DamperForceBellCrankRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce_x);
                DamperForceBellCrankRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce_y);
                DamperForceBellCrankRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.DamperForce_z);

                DroopLinkForceRR.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink);
                DroopLinkBellCrankRRx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink_x);
                DroopLinkBellCrankRRy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink_y);
                DroopLinkBellCrankRRz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink_z);
                DroopLinkLeverRRx.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink_x);
                DroopLinkLeverRRy.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink_y);
                DroopLinkLeverRRz.Text = String.Format("{0:000}", Vehicle.Assembled_Vehicle.oc_RR.ARBDroopLink_z);

                ToeLinkChassisRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink_x))); ToeLinkChassisRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink_x);
                ToeLinkChassisRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink_y))); ToeLinkChassisRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink_y);
                ToeLinkChassisRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink_z))); ToeLinkChassisRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink_z);
                ToeLinkUprightRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink_x))); ToeLinkUprightRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink_x);
                ToeLinkUprightRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink_y))); ToeLinkUprightRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink_y);
                ToeLinkUprightRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.ToeLink_z))); ToeLinkUprightRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LBJ_x))); LowerFrontUprightRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LBJ_x);
                LowerFrontUprightRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LBJ_y))); LowerFrontUprightRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LBJ_y);
                LowerFrontUprightRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LBJ_z))); LowerFrontUprightRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LBJ_z);
                LowerRearUprightRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LBJ_x))); LowerRearUprightRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LBJ_x);
                LowerRearUprightRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LBJ_y))); LowerRearUprightRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LBJ_y);
                LowerRearUprightRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.LBJ_z))); LowerRearUprightRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.LBJ_z);
                UpperFrontUprightRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UBJ_x))); UpperFrontUprightRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UBJ_x);
                UpperFrontUprightRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UBJ_y))); UpperFrontUprightRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UBJ_y);
                UpperFrontUprightRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UBJ_z))); UpperFrontUprightRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UBJ_z);
                UpperRearUprightRRx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UBJ_x))); UpperRearUprightRRx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UBJ_x);
                UpperRearUprightRRy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UBJ_y))); UpperRearUprightRRy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UBJ_y);
                UpperRearUprightRRz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.oc_RR.UBJ_z))); UpperRearUprightRRz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RR.UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.LowerFrontLength);
                LowerRearLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.LowerRearLength);
                UpperFrontLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.UpperFrontLength);
                UpperRearLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.UpperRearLength);
                PushRodLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.PushRodLength);
                ToeLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.ToeLinkLength);
                ARBDroopLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.ARBDroopLinkLength);
                DamperLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.DamperLength);
                ARBLeverLinkLengthRR.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RR.ARBBladeLength);


                #endregion

                #region Vehicle Level Outputs

                NewWheelBase.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.New_WheelBase))); NewWheelBase.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.New_WheelBase);
                NewTrackFront.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.New_TrackF))); NewTrackFront.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.New_TrackF);
                NewTrackRear.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.New_TrackR))); NewTrackRear.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.New_TrackR);

                NewSuspendedMassCGx.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.New_SMCoGx))); NewSuspendedMassCGx.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.New_SMCoGx);
                NewSuspendedMassCGz.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.New_SMCoGz))); NewSuspendedMassCGz.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.New_SMCoGz);
                NewSuspendedMassCGy.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.New_SMCoGy))); NewSuspendedMassCGy.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.New_SMCoGy);

                RollAngleChassis.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.RollAngle_Chassis))); RollAngleChassis.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.RollAngle_Chassis * (180 / Math.PI)));
                PitchAngleChassis.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.PitchAngle_Chassis))); PitchAngleChassis.Text = String.Format("{0:0.000}", (Vehicle.Assembled_Vehicle.PitchAngle_Chassis * (180 / Math.PI)));

                ARBMotionRatioFront.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.ARB_MR_Front))); ARBMotionRatioFront.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.ARB_MR_Front);
                ARBMotionRatioRear.Text = Convert.ToString(((Vehicle.Assembled_Vehicle.ARB_MR_Rear))); ARBMotionRatioRear.Text = String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.ARB_MR_Rear);
                #endregion

                #region Output GUI

                OutputClassGUI.Rounder_Outputs(Vehicle.Assembled_Vehicle.oc_FL);
                OutputClassGUI.Rounder_Outputs(Vehicle.Assembled_Vehicle.oc_FR);
                OutputClassGUI.Rounder_Outputs(Vehicle.Assembled_Vehicle.oc_RL);
                OutputClassGUI.Rounder_Outputs(Vehicle.Assembled_Vehicle.oc_RR);

                Vehicle.Assembled_Vehicle.oc_FL.OC_SC_DataTable = Vehicle.Assembled_Vehicle.oc_FL.PopulateDataTable(Vehicle.Assembled_Vehicle);
                Vehicle.Assembled_Vehicle.oc_FR.OC_SC_DataTable = Vehicle.Assembled_Vehicle.oc_FR.PopulateDataTable(Vehicle.Assembled_Vehicle);
                Vehicle.Assembled_Vehicle.oc_RL.OC_SC_DataTable = Vehicle.Assembled_Vehicle.oc_RL.PopulateDataTable(Vehicle.Assembled_Vehicle);
                Vehicle.Assembled_Vehicle.oc_RR.OC_SC_DataTable = Vehicle.Assembled_Vehicle.oc_RR.PopulateDataTable(Vehicle.Assembled_Vehicle);


                GridControl GridControlOutputs_FL = new GridControl();
                GridControl GridControlOutputs_FR = new GridControl();
                GridControl GridControlOutputs_RL = new GridControl();
                GridControl GridControlOutputs_RR = new GridControl();

                XtraScrollableControl xtraScrollableControl_OutputCoordinates = new XtraScrollableControl();

                //DevExpress.XtraTabbedMdi.XtraMdiTabPage SuspensionCoordinates = new DevExpress.XtraTabbedMdi.XtraMdiTabPage(xtraTabControl1, GridControlOutputs_FL);
                
                
                xtraTabControl1.Dock = DockStyle.Fill;
                xtraTabControl1.TabPages[0].Controls.Add(tableLayoutPanel3);
                xtraTabControl1.TabPages[0].Text = "Wishbone Forces";

                xtraTabControl1.TabPages[1].Controls.Add(tableLayoutPanel4);
                xtraTabControl1.TabPages[1].Text = "Corner Weights, Deflections & Wheel Alignment";



                xtraTabControl1.TabPages.Add("Vehicle Outputs");
                xtraTabControl1.TabPages[3].Controls.Add(xtraScrollableControl2);

                xtraTabControl1.TabPages.Add("Link Lengths");
                xtraTabControl1.TabPages[4].Controls.Add(tableLayoutPanel5);

                tabPaneResults.Hide();
                xtraTabControl1.TabPages.Add("Suspension Coordinates");
                xtraTabControl1.TabPages[2].Controls.Add(xtraScrollableControl_OutputCoordinates);
                xtraScrollableControl_OutputCoordinates.Visible = true;
                xtraScrollableControl_OutputCoordinates.SendToBack();
                xtraScrollableControl_OutputCoordinates.Dock = DockStyle.Fill;

                xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_FL);
                GridControlOutputs_FL.Dock = DockStyle.Left;

                DevExpress.XtraTabbedMdi.XtraMdiTabPage SuspensionCoordinates_TabPage = new DevExpress.XtraTabbedMdi.XtraMdiTabPage(xtraTabControl1, this);
                xtraTabbedMdiManager1.Pages.Add(SuspensionCoordinates_TabPage);

                //GridControlOutputs_FLForm.MdiParent = SuspensionCoordinates_TabPage;
                //SuspensionCoordinates_TabPage.MdiChild = GridControlOutputs_FL;
                GridControlOutputs_FL.Parent = this;
                


                xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_FR);
                GridControlOutputs_FR.Dock = DockStyle.Left;


                xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_RL);
                GridControlOutputs_RL.Dock = DockStyle.Left;


                xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_RR);
                GridControlOutputs_RR.Dock = DockStyle.Left;


                Vehicle.Assembled_Vehicle.oc_FL.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Front Left Suspension Coordinates");
                Vehicle.Assembled_Vehicle.oc_FR.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Front Right Suspension Coordinates");
                Vehicle.Assembled_Vehicle.oc_RL.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Rear Left Suspension Coordinates");
                Vehicle.Assembled_Vehicle.oc_RR.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Rear Right Suspension Coordinates");

                GridControlOutputs_FL.MainView = Vehicle.Assembled_Vehicle.oc_FL.bandedGridView_Outputs;
                GridControlOutputs_FR.MainView = Vehicle.Assembled_Vehicle.oc_FR.bandedGridView_Outputs;
                GridControlOutputs_RL.MainView = Vehicle.Assembled_Vehicle.oc_RL.bandedGridView_Outputs;
                GridControlOutputs_RR.MainView = Vehicle.Assembled_Vehicle.oc_RR.bandedGridView_Outputs;


                //GridControlOutputs.MainView = AdvBandedGridView_Outputs;
                GridControlOutputs_FL.DataSource = Vehicle.Assembled_Vehicle.oc_FL.OC_SC_DataTable;
                GridControlOutputs_FR.DataSource = Vehicle.Assembled_Vehicle.oc_FR.OC_SC_DataTable;
                GridControlOutputs_RL.DataSource = Vehicle.Assembled_Vehicle.oc_RL.OC_SC_DataTable;
                GridControlOutputs_RR.DataSource = Vehicle.Assembled_Vehicle.oc_RR.OC_SC_DataTable;


                Vehicle.Assembled_Vehicle.oc_FL.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(Vehicle.Assembled_Vehicle.oc_FL.bandedGridView_Outputs, this);
                Vehicle.Assembled_Vehicle.oc_FR.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(Vehicle.Assembled_Vehicle.oc_FR.bandedGridView_Outputs, this);
                Vehicle.Assembled_Vehicle.oc_RL.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(Vehicle.Assembled_Vehicle.oc_RL.bandedGridView_Outputs, this);
                Vehicle.Assembled_Vehicle.oc_RR.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(Vehicle.Assembled_Vehicle.oc_RR.bandedGridView_Outputs, this);

                GridControlOutputs_RR.SendToBack();
                GridControlOutputs_RL.SendToBack();
                GridControlOutputs_FR.SendToBack();
                GridControlOutputs_FL.SendToBack();

                #endregion


                #region GUI Operations on based on the Suspension Type of the Selected Vehicle

                #region Geometry Type
                if (Vehicle.Assembled_Vehicle.DoubleWishboneFront == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension Type is Double Wishbone with Pushrod
                    A2xFL.Show(); A2yFL.Show(); A2zFL.Show(); A2xFR.Show(); A2yFR.Show(); A2zFR.Show();
                    B2xFL.Show(); B2yFL.Show(); B2zFL.Show(); B2xFR.Show(); B2yFR.Show(); B2zFR.Show();
                    I2xFL.Show(); I2yFL.Show(); I2zFL.Show(); I2xFR.Show(); I2yFR.Show(); I2zFR.Show();
                    H2xFL.Show(); H2yFL.Show(); H2zFL.Show(); H2xFR.Show(); H2yFR.Show(); H2zFR.Show();
                    G2xFL.Show(); G2yFL.Show(); G2zFL.Show(); G2xFR.Show(); G2yFR.Show(); G2zFR.Show();
                    F2xFL.Show(); F2yFL.Show(); F2zFL.Show(); F2xFR.Show(); F2yFR.Show(); F2zFR.Show();
                    O2xFL.Show(); O2yFL.Show(); O2zFL.Show(); O2xFR.Show(); O2yFR.Show(); O2zFR.Show();

                    UpperFrontLinkLengthFL.Show(); UpperFrontLinkLengthFR.Show(); UpperRearLinkLengthFL.Show(); UpperRearLinkLengthFR.Show();
                    PushRodLinkLengthFL.Show(); PushRodLinkLengthFR.Show(); ARBDroopLinkLengthFL.Show(); ARBDroopLinkLengthFR.Show();

                    label1110.Show(); label1104.Show(); label1112.Show(); label1105.Show(); label1114.Show(); label1106.Show(); label1113.Show(); label2.Show();
                    label1127.Show(); label1125.Show(); label1123.Show(); label1124.Show(); label1133.Show(); label1132.Show(); label1131.Show(); label3.Show();

                    label562.Show(); label563.Show(); label564.Show(); label577.Show(); label578.Show(); label579.Show(); label565.Show(); label566.Show(); label577.Show(); label725.Show(); label726.Show(); label727.Show();
                    label740.Show(); label741.Show(); label742.Show(); label701.Show(); label702.Show(); label703.Show(); label704.Show(); label705.Show(); label715.Show();
                    label685.Show(); label686.Show(); label687.Show(); label682.Show(); label683.Show(); label684.Show(); label692.Show(); label693.Show(); label694.Show(); label872.Show(); label873.Show(); label874.Show(); label869.Show();
                    label870.Show(); label871.Show(); label887.Show(); label888.Show(); label889.Show(); label884.Show(); label885.Show(); label886.Show();


                    label841.Text = "Damper Shock Mount";
                    label7.Text = "Damper Shock Mount";
                    label860.Text = "Damper Bell Crank";
                    label39.Text = "Damper Bell Crank";
                    label24.Show(); label23.Show(); label26.Show(); label14.Show(); label17.Show(); label15.Show(); label41.Show(); label40.Show(); label36.Show(); label31.Show(); label858.Show(); label859.Show(); label862.Show(); label867.Show();

                    navigationPagePushRodFL.Caption = "Pushrod FL";
                    navigationPagePushRodFR.Caption = "Pushrod FR";


                    #endregion
                }
                else if (Vehicle.Assembled_Vehicle.McPhersonFront == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension type is McPherson Strut
                    A2xFL.Hide(); A2yFL.Hide(); A2zFL.Hide(); A2xFR.Hide(); A2yFR.Hide(); A2zFR.Hide(); // All irrelevant textboxes removed
                    B2xFL.Hide(); B2yFL.Hide(); B2zFL.Hide(); B2xFR.Hide(); B2yFR.Hide(); B2zFR.Hide();
                    I2xFL.Hide(); I2yFL.Hide(); I2zFL.Hide(); I2xFR.Hide(); I2yFR.Hide(); I2zFR.Hide();
                    H2xFL.Hide(); H2yFL.Hide(); H2zFL.Hide(); H2xFR.Hide(); H2yFR.Hide(); H2zFR.Hide();
                    G2xFL.Hide(); G2yFL.Hide(); G2zFL.Hide(); G2xFR.Hide(); G2yFR.Hide(); G2zFR.Hide();
                    F2xFL.Hide(); F2yFL.Hide(); F2zFL.Hide(); F2xFR.Hide(); F2yFR.Hide(); F2zFR.Hide();
                    O2xFL.Hide(); O2yFL.Hide(); O2zFL.Hide(); O2xFR.Hide(); O2yFR.Hide(); O2zFR.Hide();

                    label562.Hide(); label563.Hide(); label564.Hide(); label577.Hide(); label578.Hide(); label579.Hide(); label565.Hide(); label566.Hide(); label577.Hide(); label725.Hide(); label726.Hide(); label727.Hide();
                    label740.Hide(); label741.Hide(); label742.Hide(); label701.Hide(); label702.Hide(); label703.Hide(); label704.Hide(); label705.Hide(); label715.Hide();
                    label685.Hide(); label686.Hide(); label687.Hide(); label682.Hide(); label683.Hide(); label684.Hide(); label692.Hide(); label693.Hide(); label694.Hide(); label872.Hide(); label873.Hide(); label874.Hide(); label869.Hide();
                    label870.Hide(); label871.Hide(); label887.Hide(); label888.Hide(); label889.Hide(); label884.Hide(); label885.Hide(); label886.Hide();

                    UpperFrontLinkLengthFL.Hide(); UpperFrontLinkLengthFR.Hide(); UpperRearLinkLengthFL.Hide(); UpperRearLinkLengthFR.Hide();
                    PushRodLinkLengthFL.Hide(); PushRodLinkLengthFR.Hide(); ARBDroopLinkLengthFL.Hide(); ARBDroopLinkLengthFR.Hide();

                    label1110.Hide(); label1104.Hide(); label1112.Hide(); label1105.Hide(); label1114.Hide(); label1106.Hide(); label1113.Hide(); label2.Hide();
                    label1127.Hide(); label1125.Hide(); label1123.Hide(); label1124.Hide(); label1133.Hide(); label1132.Hide(); label1131.Hide(); label3.Hide();


                    label841.Text = "Damper Chassis Mount";
                    label7.Text = "Damper Chassis Mount";
                    label860.Text = "Upper Ball Joint";
                    label39.Text = "Upper Ball Joint";
                    label24.Hide(); label23.Hide(); label26.Hide(); label14.Hide(); label17.Hide(); label15.Hide(); label41.Hide(); label40.Hide(); label36.Hide(); label31.Hide(); label858.Hide(); label859.Hide(); label862.Hide(); label867.Hide();

                    navigationPagePushRodFL.Caption = "Strut FL";
                    navigationPagePushRodFR.Caption = "Strut FR";

                    #endregion
                }


                if (Vehicle.Assembled_Vehicle.DoubleWishboneRear == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension Type is Double Wishbone with Pushrod
                    A2xRL.Show(); A2yRL.Show(); A2zRL.Show(); A2xRR.Show(); A2yRR.Show(); A2zRR.Show();
                    B2xRL.Show(); B2yRL.Show(); B2zRL.Show(); B2xRR.Show(); B2yRR.Show(); B2zRR.Show();
                    I2xRL.Show(); I2yRL.Show(); I2zRL.Show(); I2xRR.Show(); I2yRR.Show(); I2zRR.Show();
                    H2xRL.Show(); H2yRL.Show(); H2zRL.Show(); H2xRR.Show(); H2yRR.Show(); H2zRR.Show();
                    G2xRL.Show(); G2yRL.Show(); G2zRL.Show(); G2xRR.Show(); G2yRR.Show(); G2zRR.Show();
                    F2xRL.Show(); F2yRL.Show(); F2zRL.Show(); F2xRR.Show(); F2yRR.Show(); F2zRR.Show();
                    O2xRL.Show(); O2yRL.Show(); O2zRL.Show(); O2xRR.Show(); O2yRR.Show(); O2zRR.Show();

                    label598.Show(); label599.Show(); label600.Show(); label586.Show(); label587.Show(); label588.Show(); label601.Show(); label602.Show(); label603.Show(); label786.Show(); label788.Show(); label793.Show();
                    label803.Show(); label804.Show(); label806.Show(); label744.Show(); label745.Show(); label743.Show(); label769.Show(); label771.Show(); label773.Show();
                    label613.Show(); label614.Show(); label615.Show(); label610.Show(); label611.Show(); label612.Show(); label641.Show(); label642.Show(); label643.Show(); label833.Show(); label834.Show(); label835.Show();
                    label846.Show(); label847.Show(); label848.Show(); label815.Show(); label816.Show(); label817.Show(); label818.Show(); label820.Show(); label822.Show();
                    label55.Show(); label541.Show(); label538.Show();

                    UpperFrontLinkLengthRL.Show(); UpperFrontLinkLengthRR.Show(); UpperRearLinkLengthRL.Show(); UpperRearLinkLengthRR.Show();
                    PushRodLinkLengthRL.Show(); PushRodLinkLengthRR.Show(); ARBDroopLinkLengthRL.Show(); ARBDroopLinkLengthRR.Show();

                    label1149.Show(); label1152.Show(); label1157.Show(); label1154.Show(); label1163.Show(); label1161.Show(); label1158.Show(); label8.Show();
                    label1142.Show(); label1140.Show(); label1137.Show(); label1139.Show(); label1148.Show(); label1147.Show(); label1146.Show(); label5.Show();




                    label25.Text = "Damper Shock Mount";
                    label7.Text = "Damper Shock Mount";
                    label51.Text = "Damper Bell Crank";
                    label63.Text = "Damper Bell Crank";
                    label831.Show(); label821.Show(); label856.Show(); label53.Show(); label52.Show(); label48.Show(); label43.Show(); label65.Show(); label64.Show(); label60.Show(); label55.Show();

                    navigationPagePushRodRL.Caption = "Pushrod RL";
                    navigationPagePushRodRR.Caption = "Pushrod RR";

                    #endregion
                }
                else if (Vehicle.Assembled_Vehicle.McPhersonRear == 1)
                {
                    #region Bringing out the changes in UI and values if the Suspension type is McPherson Strut
                    A2xRL.Hide(); A2yRL.Hide(); A2zRL.Hide(); A2xRR.Hide(); A2yRR.Hide(); A2zRR.Hide();
                    B2xRL.Hide(); B2yRL.Hide(); B2zRL.Hide(); B2xRR.Hide(); B2yRR.Hide(); B2zRR.Hide();
                    I2xRL.Hide(); I2yRL.Hide(); I2zRL.Hide(); I2xRR.Hide(); I2yRR.Hide(); I2zRR.Hide();
                    H2xRL.Hide(); H2yRL.Hide(); H2zRL.Hide(); H2xRR.Hide(); H2yRR.Hide(); H2zRR.Hide();
                    G2xRL.Hide(); G2yRL.Hide(); G2zRL.Hide(); G2xRR.Hide(); G2yRR.Hide(); G2zRR.Hide();
                    F2xRL.Hide(); F2yRL.Hide(); F2zRL.Hide(); F2xRR.Hide(); F2yRR.Hide(); F2zRR.Hide();
                    O2xRL.Hide(); O2yRL.Hide(); O2zRL.Hide(); O2xRR.Hide(); O2yRR.Hide(); O2zRR.Hide();

                    label598.Hide(); label599.Hide(); label600.Hide(); label586.Hide(); label587.Hide(); label588.Hide(); label601.Hide(); label602.Hide(); label603.Hide(); label786.Hide(); label788.Hide(); label793.Hide();
                    label803.Hide(); label804.Hide(); label806.Hide(); label744.Hide(); label745.Hide(); label743.Hide(); label769.Hide(); label771.Hide(); label773.Hide();
                    label613.Hide(); label614.Hide(); label615.Hide(); label610.Hide(); label611.Hide(); label612.Hide(); label641.Hide(); label642.Hide(); label643.Hide(); label833.Hide(); label834.Hide(); label835.Hide();
                    label846.Hide(); label847.Hide(); label848.Hide(); label815.Hide(); label816.Hide(); label817.Hide(); label818.Hide(); label820.Hide(); label822.Hide();
                    label55.Hide(); label541.Hide(); label538.Hide();

                    UpperFrontLinkLengthRL.Hide(); UpperFrontLinkLengthRR.Hide(); UpperRearLinkLengthRL.Hide(); UpperRearLinkLengthRR.Hide();
                    PushRodLinkLengthRL.Hide(); PushRodLinkLengthRR.Hide(); ARBDroopLinkLengthRL.Hide(); ARBDroopLinkLengthRR.Hide();

                    label1149.Hide(); label1152.Hide(); label1157.Hide(); label1154.Hide(); label1163.Hide(); label1161.Hide(); label1158.Hide(); label8.Hide();
                    label1142.Hide(); label1140.Hide(); label1137.Hide(); label1139.Hide(); label1148.Hide(); label1147.Hide(); label1146.Hide(); label5.Hide();


                    label25.Text = "Damper Chassis Mount";
                    label7.Text = "Damper Chassis Mount";
                    label51.Text = "Upper Ball Joint";
                    label63.Text = "Upper Ball Joint";
                    label831.Hide(); label821.Hide(); label856.Hide(); label53.Hide(); label52.Hide(); label48.Hide(); label43.Hide(); label65.Hide(); label64.Hide(); label60.Hide(); label55.Hide();

                    navigationPagePushRodRL.Caption = "Stut RL";
                    navigationPagePushRodRR.Caption = "Strut RR";

                    #endregion
                }
                #endregion

                #region Actuation Type
                if (Vehicle.Assembled_Vehicle.PushRodIdentifierFront == 1)
                {
                    label41.Text = "Push Rod";
                    label858.Text = "Push Rod";
                    label862.Text = "Push Rod Bell-Crank";
                    label36.Text = "Push Rod Bell-Crank";
                    label1114.Text = "Pushrod";
                    label1123.Text = "Pushrod";
                    navigationPagePushRodFL.Caption = "Pushrod FL";
                    navigationPagePushRodFR.Caption = "Pushrod FR";
                }
                else if (Vehicle.Assembled_Vehicle.PullRodIdentifierFront == 1)
                {
                    label41.Text = "Pull Rod";
                    label858.Text = "Pull Rod";
                    label862.Text = "Pull Rod Bell-Crank";
                    label36.Text = "Pull Rod Bell-Crank";
                    label1114.Text = "Pullrod";
                    label1123.Text = "Pullrod";
                    navigationPagePushRodFL.Caption = "Pullrod FL";
                    navigationPagePushRodFR.Caption = "Pullrod FR";
                }

                if (Vehicle.Assembled_Vehicle.PushRodIdentifierRear == 1)
                {
                    label53.Text = "Push Rod";
                    label65.Text = "Push Rod";
                    label48.Text = "Push Rod Bell-Crank";
                    label60.Text = "Push Rod Bell-Crank";
                    label1157.Text = "Pushrod";
                    label1137.Text = "Pushrod";
                    navigationPagePushRodRL.Caption = "Pushrod RL";
                    navigationPagePushRodRR.Caption = "Pushrod RR";
                }
                else if (Vehicle.Assembled_Vehicle.PullRodIdentifierRear == 1)
                {
                    label53.Text = "Pull Rod";
                    label65.Text = "Pull Rod";
                    label48.Text = "Pull Rod Bell-Crank";
                    label60.Text = "Pull Rod Bell-Crank";
                    label1157.Text = "Pullrod";
                    label1137.Text = "Pullrod";
                    navigationPagePushRodRL.Caption = "Pullrod RL";
                    navigationPagePushRodRR.Caption = "Pullrod RR";
                }
                #endregion

                #endregion

                #endregion
            }
            catch (Exception)
            {

                //Try block added here so that in case Vehicle.Assembled_Vehicle is null (because calcs have not been done) the display function doesn't fail 
            }
        }
        #endregion

        #endregion

        #region Calculate Results (To be shifted to another solver class
        private void barButtonCalculateResults_ItemClicked(object sender, ItemClickEventArgs e)
        {

            this.ActiveControl = panel1;

            #region Coloring the Corner Weight and Pushrod Length Textboxes White
            PushRodLinkLengthFL.BackColor = Color.White;
            CWFL.BackColor = Color.White;

            PushRodLinkLengthFR.BackColor = Color.White;
            CWFR.BackColor = Color.White;

            PushRodLinkLengthRL.BackColor = Color.White;
            CWRL.BackColor = Color.White;


            PushRodLinkLengthRR.BackColor = Color.White;
            CWRR.BackColor = Color.White;
            #endregion


            try
            {
                 Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker = 0;

                progressBar.EditValue = 0;
                progressBar.Show();
                progressBar.BringToFront();

                #region This is the stage where the calculations are actually performed by calling the Kinematics Invoker and Vehicle Output Functions


                if (comboBoxVehicle.SelectedItem != null)
                {

                    Vehicle.Assembled_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;

                    #region Reseting the corner weights, ride height and deflections inside the Assmebled Vehicle to the values that were calculated after the initial calculationd
                    Vehicle.Assembled_Vehicle.Reset_CornerWeights(Vehicle.Assembled_Vehicle.oc_FL.CW_1, Vehicle.Assembled_Vehicle.oc_FR.CW_1, Vehicle.Assembled_Vehicle.oc_RL.CW_1, Vehicle.Assembled_Vehicle.oc_RR.CW_1);
                    Vehicle.Assembled_Vehicle.Reset_PushrodLengths();
                    Vehicle.Assembled_Vehicle.Reset_RideHeight();
                    Vehicle.Assembled_Vehicle.Reset_Deflections();
                    #endregion

                    #region Clearing the Output Class to eliminate any chances of residue
                    M1_Global.Assy_OC[0].Clear();
                    M1_Global.Assy_OC[1].Clear();
                    M1_Global.Assy_OC[2].Clear();
                    M1_Global.Assy_OC[3].Clear();

                    Vehicle.Assembled_Vehicle.oc_FL.Clear();
                    Vehicle.Assembled_Vehicle.oc_FR.Clear();
                    Vehicle.Assembled_Vehicle.oc_RL.Clear();
                    Vehicle.Assembled_Vehicle.oc_RR.Clear();
                    #endregion

                }

                else
                {
                    progressBar.Hide();
                    MessageBox.Show("User has not selected Vehicle item for Simulation");
                    return;
                }


                Vehicle.Assembled_Vehicle.KinematicsInvoker();
                Vehicle.Assembled_Vehicle.VehicleOutputs();
                Vehicle.Assembled_Vehicle.Vehicle_Results_Tracker = 1;

                DisplayOutputs();

                PopulateInputSheet();

                progressBar.Hide();

                //tabPaneResults.Show();

                ribbonPageGroupRecalculate.Enabled = true;

                #endregion

            }
            catch (Exception)
            {
                DialogResult result;
                result = MessageBox.Show("Vehicle Not Assembled Properly. Please re-check your Vehicle Item");

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

        #region Assembling the Vehicle by calling the constructor of the Vehicle Class
        public void VehicleAssembly(SuspensionCoordinatesMaster[] _sc, Tire[] _tire, Spring[] _spring, Damper[] _damper, AntiRollBar[] _arb, Chassis _chassis, WheelAlignment[] _wa, OutputClass[] _oc, out Vehicle _vehicle)
        {
            //
            // Naming convention for this method:-
            // _tire is the array of objects of Tire which the user has selected
            // _Tire is the array of objects of Tire which THIS function passes to the Vehicle Class
            //

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

            #region Vehicle object

            M1_Global.vehicleGUI = new VehicleGUI(this);
            M1_Global.vehicleGUI._VehicleGUIName = "Vehicle GUI";
            Vehicle vehicle1 = new Vehicle(M1_Global.vehicleGUI, Identifier, _SC, _Tire, _Spring, _Damper, _ARB, chassis1, _WA, _OC);
            _vehicle = vehicle1;

            #endregion
        } 
        #endregion

        private void barButtonDisplayResults_ItemClicked(object sender, ItemClickEventArgs e)
        {
            accordionControlTireStiffness.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSuspensionCoordinatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRL.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
            accordionControlDamper.Hide();
            accordionControlAntiRollBar.Hide();
            accordionControlSprings.Hide();
            accordionControlChassis.Hide();
            accordionControlWheelAlignment.Hide();
            sidePanel2.Hide();
            tabPaneResults.Show();
        }

        #region Handling the Recalculate Corner Weights for New Pushrod Length Events
        private void ReCalculate_GUI_Click(object sender, EventArgs e)
        {
            try
            {
                int local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                popupControlContainerRecalculateResults.HidePopup();

                #region GUI operations to Show the Input Sheet with Link Length Page opne, pushrod textbox enabled and green, corner weight textbox disabled and white
                M1_Global.List_I1[local_VehicleID - 1].navigationPane1.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageLinkLengthsFL;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.BackColor = Color.LimeGreen;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].RecalculateCornerWeightForPushRodLength.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;

                M1_Global.List_I1[local_VehicleID - 1].navigationPane2.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageLinkLengthsFR;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.BackColor = Color.LimeGreen;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.BackColor = Color.White;

                M1_Global.List_I1[local_VehicleID - 1].navigationPane3.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageLinkLengthsRL;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.BackColor = Color.LimeGreen;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.BackColor = Color.White;

                M1_Global.List_I1[local_VehicleID - 1].navigationPane4.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageLinkLengthsRR;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.BackColor = Color.LimeGreen;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.BackColor = Color.White;
                #endregion

                Vehicle.Assembled_Vehicle.Reset_PushrodLengths();

                PopulateInputSheet();

                M1_Global.List_I1[local_VehicleID - 1].Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }
        }



        public void ReCalculate_Click()
        {
            try
            {
                if (Vehicle.Assembled_Vehicle.VehicleID != 0)
                {
                    int local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                    #region Reseting the corner weights, ride height and deflections inside the Assmebled Vehicle to the values that were calculated after the initial calculationd
                    Vehicle.Assembled_Vehicle.Reset_CornerWeights(Vehicle.Assembled_Vehicle.oc_FL.CW_1, Vehicle.Assembled_Vehicle.oc_FR.CW_1, Vehicle.Assembled_Vehicle.oc_RL.CW_1, Vehicle.Assembled_Vehicle.oc_RR.CW_1);
                    Vehicle.Assembled_Vehicle.Reset_RideHeight();
                    Vehicle.Assembled_Vehicle.Reset_Deflections();
                    #endregion


                    M1_Global.List_I1[local_VehicleID - 1].Hide();
                    this.Show();
                    this.tabPaneResults.SelectedPage = this.tabNavigationPageCornerWeightDeflectionsWheelAlignment;

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
                        New_PushRodFL = Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].PushRodFL.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].PushRodFL.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].PushRodFL.Text = "";
                        return;

                    }

                    G1H1_Perp_FL = Math.Abs(Vehicle.Assembled_Vehicle.sc_FL.G1x - Vehicle.Assembled_Vehicle.sc_FL.H1x);
                    G1H1_FL = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_FL.G1x - Vehicle.Assembled_Vehicle.sc_FL.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_FL.G1y - Vehicle.Assembled_Vehicle.sc_FL.H1y), 2));
                    alphaFL = Math.Asin(G1H1_Perp_FL / G1H1_FL);
                    New_RideheightFL = Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight + ((New_PushRodFL - Vehicle.Assembled_Vehicle.sc_FL.PushRodLength) * Math.Sin(alphaFL));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionFL = Vehicle.Assembled_Vehicle.oc_FL.Corrected_WheelDeflection - (New_RideheightFL - Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight);

                    if (String.Format("{0:0.0}", New_PushRodFL) == String.Format("{0:0.0}", Vehicle.Assembled_Vehicle.sc_FL.PushRodLength))
                    {
                        New_CW_FL = Vehicle.Assembled_Vehicle.oc_FL.CW;
                    }

                    else
                    {
                        New_CW_FL = -((((New_WheelDeflectionFL + (Vehicle.Assembled_Vehicle.spring_FL.SpringPreload / Vehicle.Assembled_Vehicle.sc_FL.InitialMR)) * Vehicle.Assembled_Vehicle.oc_FL.RideRate))) / 9.81;
                    }

                    Delta_CW_FL = (Vehicle.Assembled_Vehicle.oc_FL.CW - (New_CW_FL)); // If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.Assembled_Vehicle.oc_FL.CW += -(Delta_CW_FL);
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_RR.CW += (Delta_CW_FL);
                    New_WheelDeflectionRR_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_RR.CW) / (Vehicle.Assembled_Vehicle.oc_RR.RideRate)) + (Vehicle.Assembled_Vehicle.spring_RR.SpringPreload / Vehicle.Assembled_Vehicle.sc_RR.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight = -New_WheelDeflectionRR_1 + Vehicle.Assembled_Vehicle.oc_RR.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight;




                    #endregion
                    #endregion

                    #region FRONT RIGHT
                    #region FRONT RIGHT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        New_PushRodFR = Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].PushRodFR.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].PushRodFR.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].PushRodFR.Text = "";
                        return;
                    }

                    G1H1_Perp_FR = Math.Abs(Vehicle.Assembled_Vehicle.sc_FR.G1x - Vehicle.Assembled_Vehicle.sc_FR.H1x);
                    G1H1_FR = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_FR.G1x - Vehicle.Assembled_Vehicle.sc_FR.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_FR.G1y - Vehicle.Assembled_Vehicle.sc_FR.H1y), 2));
                    alphaFR = Math.Asin(G1H1_Perp_FR / G1H1_FR);
                    New_RideheightFR = Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight + ((New_PushRodFR - Vehicle.Assembled_Vehicle.sc_FR.PushRodLength) * Math.Sin(alphaFR));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionFR = Vehicle.Assembled_Vehicle.oc_FR.Corrected_WheelDeflection - (New_RideheightFR - Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight);

                    if (String.Format("{0:0.0}", New_PushRodFR) == String.Format("{0:0.0}", Vehicle.Assembled_Vehicle.sc_FR.PushRodLength))
                    {
                        New_CW_FR = Vehicle.Assembled_Vehicle.oc_FR.CW;
                    }
                    else
                    {
                        New_CW_FR = -((((New_WheelDeflectionFR + (Vehicle.Assembled_Vehicle.spring_FR.SpringPreload / Vehicle.Assembled_Vehicle.sc_FR.InitialMR)) * Vehicle.Assembled_Vehicle.oc_FR.RideRate))) / 9.81;
                    }

                    Delta_CW_FR = (Vehicle.Assembled_Vehicle.oc_FR.CW - (New_CW_FR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.Assembled_Vehicle.oc_FR.CW += -Delta_CW_FR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_RL.CW += Delta_CW_FR;
                    New_WheelDeflectionRL_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_RL.CW) / (Vehicle.Assembled_Vehicle.oc_RL.RideRate)) + (Vehicle.Assembled_Vehicle.spring_RL.SpringPreload / Vehicle.Assembled_Vehicle.sc_RL.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight = -New_WheelDeflectionRL_1 + Vehicle.Assembled_Vehicle.oc_RL.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight;





                    #endregion
                    #endregion

                    #region REAL LEFT
                    #region REAR LEFT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        New_PushRodRL = Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].PushRodRL.Text);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].PushRodRL.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].PushRodRL.Text = "";
                        return;

                    }
                    G1H1_Perp_RL = Math.Abs(Vehicle.Assembled_Vehicle.sc_RL.G1x - Vehicle.Assembled_Vehicle.sc_RL.H1x);
                    G1H1_RL = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_RL.G1x - Vehicle.Assembled_Vehicle.sc_RL.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_RL.G1y - Vehicle.Assembled_Vehicle.sc_RL.H1y), 2));
                    alphaRL = Math.Asin(G1H1_Perp_RL / G1H1_RL);
                    New_RideheightRL = Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight + ((New_PushRodRL - Vehicle.Assembled_Vehicle.sc_RL.PushRodLength) * Math.Sin(alphaRL));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionRL = New_WheelDeflectionRL_1 - (New_RideheightRL - Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight);
                    if (String.Format("{0:0.000}", New_PushRodRL) == String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.sc_RL.PushRodLength))
                    {
                        New_CW_RL = Vehicle.Assembled_Vehicle.oc_RL.CW;
                    }
                    else
                    {


                        New_CW_RL = -((((New_WheelDeflectionRL + (Vehicle.Assembled_Vehicle.spring_RL.SpringPreload / Vehicle.Assembled_Vehicle.sc_RL.InitialMR)) * Vehicle.Assembled_Vehicle.oc_RL.RideRate))) / 9.81;
                    }

                    Delta_CW_RL = (Vehicle.Assembled_Vehicle.oc_RL.CW - (New_CW_RL));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.Assembled_Vehicle.oc_RL.CW += -Delta_CW_RL;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_FR.CW += Delta_CW_RL;

                    New_WheelDeflectionFR_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_FR.CW) / (Vehicle.Assembled_Vehicle.oc_FR.RideRate)) + (Vehicle.Assembled_Vehicle.spring_FR.SpringPreload / Vehicle.Assembled_Vehicle.sc_FR.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight = -New_WheelDeflectionFR_1 + Vehicle.Assembled_Vehicle.oc_FR.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight;



                    #endregion
                    #endregion

                    #region REAR RIGHT
                    #region REAR RIGHT Calculation of New Ride Height after Increasing Push Rod Length
                    try
                    {
                        New_PushRodRR = Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].PushRodRR.Text);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Pushrod length entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].PushRodRR.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].PushRodRR.Text = "";
                        return;
                    }
                    G1H1_Perp_RR = Math.Abs(Vehicle.Assembled_Vehicle.sc_RR.G1x - Vehicle.Assembled_Vehicle.sc_RR.H1x);
                    G1H1_RR = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_RR.G1x - Vehicle.Assembled_Vehicle.sc_RR.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_RR.G1y - Vehicle.Assembled_Vehicle.sc_RR.H1y), 2));
                    alphaRR = Math.Asin(G1H1_Perp_RR / G1H1_RR);
                    New_RideheightRR = Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight + ((New_PushRodRR - Vehicle.Assembled_Vehicle.sc_RR.PushRodLength) * Math.Sin(alphaRR));
                    #endregion

                    #region Calculation of New Corner Weight
                    New_WheelDeflectionRR = New_WheelDeflectionRR_1 - (New_RideheightRR - Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight);
                    if (String.Format("{0:0.0}", New_PushRodRR) == String.Format("{0:0.0}", Vehicle.Assembled_Vehicle.sc_RR.PushRodLength))
                    {


                        New_CW_RR = Vehicle.Assembled_Vehicle.oc_RR.CW;
                    }
                    else
                    {
                        New_CW_RR = -((((New_WheelDeflectionRR + (Vehicle.Assembled_Vehicle.spring_RR.SpringPreload / Vehicle.Assembled_Vehicle.sc_RR.InitialMR)) * Vehicle.Assembled_Vehicle.oc_RR.RideRate))) / 9.81;
                    }


                    Delta_CW_RR = (Vehicle.Assembled_Vehicle.oc_RR.CW - (New_CW_RR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    Vehicle.Assembled_Vehicle.oc_RR.CW += -Delta_CW_RR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_FL.CW += Delta_CW_RR;
                    New_WheelDeflectionFL_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_FL.CW) / (Vehicle.Assembled_Vehicle.oc_FL.RideRate)) + (Vehicle.Assembled_Vehicle.spring_FL.SpringPreload / Vehicle.Assembled_Vehicle.sc_FL.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight = -New_WheelDeflectionFL_1 + Vehicle.Assembled_Vehicle.oc_FL.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight;



                    #endregion
                    #endregion

                    //
                    // Not Assigning the New Values of Deflections and Ride Height because they will be recalculated upon invoking the Kinematics Invoker Function


                    //
                    // Assigning the New values of Push Rod Lengths
                    Vehicle.Assembled_Vehicle.sc_FL.PushRodLength = New_PushRodFL;
                    Vehicle.Assembled_Vehicle.sc_FR.PushRodLength = New_PushRodFR;
                    Vehicle.Assembled_Vehicle.sc_RL.PushRodLength = New_PushRodRL;
                    Vehicle.Assembled_Vehicle.sc_RR.PushRodLength = New_PushRodRR;

                    //
                    //Invoking the Kinematics and Vehicle Output Functions again 
                    #region Overriding the Corner Weights to the new values calculated
                    Vehicle.Assembled_Vehicle.OverrideCornerWeights(Vehicle.Assembled_Vehicle.oc_FL.CW, Vehicle.Assembled_Vehicle.oc_FR.CW, Vehicle.Assembled_Vehicle.oc_RL.CW, Vehicle.Assembled_Vehicle.oc_RR.CW);
                    #endregion

                    Vehicle.Assembled_Vehicle.KinematicsInvoker();
                    Vehicle.Assembled_Vehicle.VehicleOutputs();


                    #region Coloring the Corner Weight and Pushrod Length Textboxes
                    CWFL.BackColor = Color.LimeGreen;
                    PushRodLinkLengthFL.BackColor = Color.White;
                    CWFR.BackColor = Color.LimeGreen;
                    PushRodLinkLengthFR.BackColor = Color.White;
                    CWRL.BackColor = Color.LimeGreen;
                    PushRodLinkLengthRL.BackColor = Color.White;
                    CWRR.BackColor = Color.LimeGreen;
                    PushRodLinkLengthRR.BackColor = Color.White;
                    #endregion

                    DisplayOutputs();
                    tabPaneResults.Show();
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

        #region Handling the Recalculate Pushrod Lengths for Desired Corner Weight Events
        private void ReCalculateForDesiredCornerWeight_GUI_Click(object sender, EventArgs e)
        {
            try
            {

                int local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                PopulateInputSheet();

                popupControlContainerRecalculateResults.HidePopup();

                #region GUI operations to Show the Input Sheet with Corner Weights Page opne, pushrod textbox disabled and white, corner weight textbox enabled and green
                M1_Global.List_I1[local_VehicleID - 1].navigationPane1.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageCornerWeightFL;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.BackColor = Color.LimeGreen;
                M1_Global.List_I1[local_VehicleID - 1].RecalculatePushrodLengthForDesiredCornerWeight.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].RecalculateCornerWeightForPushRodLength.Enabled = false;

                M1_Global.List_I1[local_VehicleID - 1].navigationPane2.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageCornerWeightFR;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodFR.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.BackColor = Color.LimeGreen;

                M1_Global.List_I1[local_VehicleID - 1].navigationPane3.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageCornerWeightRL;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRL.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.BackColor = Color.LimeGreen;

                M1_Global.List_I1[local_VehicleID - 1].navigationPane4.SelectedPage = M1_Global.List_I1[local_VehicleID - 1].navigationPageCornerWeightRR;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.Enabled = false;
                M1_Global.List_I1[local_VehicleID - 1].PushRodRR.BackColor = Color.White;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.Enabled = true;
                M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.BackColor = Color.LimeGreen;
                #endregion

                M1_Global.List_I1[local_VehicleID - 1].Show();

                Vehicle.Assembled_Vehicle.Reset_CornerWeights(Vehicle.Assembled_Vehicle.oc_FL.CW_1, Vehicle.Assembled_Vehicle.oc_FR.CW_1, Vehicle.Assembled_Vehicle.oc_RL.CW_1, Vehicle.Assembled_Vehicle.oc_RR.CW_1);

                PopulateInputSheet();

            }
            catch (Exception)
            {

                MessageBox.Show(" Initial Calculations have not been done. Please click the CALCULATE RESULTS button to perform the initial set of Calculations");
            }

        }


        public void ReCalculateForDesiredCornerWeight_Click()
        {
            try
            {
                if (Vehicle.Assembled_Vehicle.VehicleID != 0)
                {
                    int local_VehicleID = Vehicle.Assembled_Vehicle.VehicleID;

                    #region Reseting the corner weights, ride height and deflections inside the Assmebled Vehicle to the values that were calculated after the initial calculationd
                    Vehicle.Assembled_Vehicle.Reset_PushrodLengths();
                    Vehicle.Assembled_Vehicle.Reset_RideHeight();
                    Vehicle.Assembled_Vehicle.Reset_Deflections();
                    Vehicle.Assembled_Vehicle.Reset_RideRate();
                    #endregion


                    M1_Global.List_I1[local_VehicleID - 1].Hide();
                    this.Hide();
                    this.Show();
                    this.tabPaneResults.SelectedPage = this.tabNavigationPageLinkLengths;

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
                        New_CW_FL = -Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightFL.Text = "";
                        return;

                    }

                    if (String.Format("{0:0.00}", New_CW_FL) == String.Format("{0:0.00}", Vehicle.Assembled_Vehicle.oc_FL.CW_1))
                    {
                        New_CW_FL = Vehicle.Assembled_Vehicle.oc_FL.CW;
                    }

                    Delta_CW_FL = (Vehicle.Assembled_Vehicle.oc_FL.CW - (New_CW_FL)); // If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionFL = -(((9.81 * New_CW_FL) / (Vehicle.Assembled_Vehicle.oc_FL.RideRate)) + (Vehicle.Assembled_Vehicle.spring_FL.SpringPreload / Vehicle.Assembled_Vehicle.sc_FL.InitialMR));
                    New_RideheightFL = -New_WheelDeflectionFL + Vehicle.Assembled_Vehicle.oc_FL.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight;
                    #endregion

                    #region FRONT LEFT Calculation of New Push Rod Length
                    G1H1_Perp_FL = Math.Abs(Vehicle.Assembled_Vehicle.sc_FL.G1x - Vehicle.Assembled_Vehicle.sc_FL.H1x);
                    G1H1_FL = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_FL.G1x - Vehicle.Assembled_Vehicle.sc_FL.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_FL.G1y - Vehicle.Assembled_Vehicle.sc_FL.H1y), 2));
                    alphaFL = Math.Asin(G1H1_Perp_FL / G1H1_FL);
                    New_PushRodFL = ((New_RideheightFL - Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight) / Math.Sin(alphaFL)) + Vehicle.Assembled_Vehicle.sc_FL.PushRodLength;
                    Vehicle.Assembled_Vehicle.oc_FL.CW += -(Delta_CW_FL);
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_RR.CW += (Delta_CW_FL);
                    New_WheelDeflectionRR_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_RR.CW) / (Vehicle.Assembled_Vehicle.oc_RR.RideRate)) + (Vehicle.Assembled_Vehicle.spring_RR.SpringPreload / Vehicle.Assembled_Vehicle.sc_RR.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight = -New_WheelDeflectionRR_1 + Vehicle.Assembled_Vehicle.oc_RR.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight;





                    #endregion
                    #endregion

                    #region FRONT RIGHT
                    #region FRONT RIGHT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_FR = -Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.Text);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightFR.Text = "";
                        return;
                    }
                    if (String.Format("{0:0.000}", New_CW_FR) == String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_FR.CW_1))
                    {
                        New_CW_FR = Vehicle.Assembled_Vehicle.oc_FR.CW;
                    }
                    Delta_CW_FR = (Vehicle.Assembled_Vehicle.oc_FR.CW - (New_CW_FR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionFR = -(((9.81 * New_CW_FR) / (Vehicle.Assembled_Vehicle.oc_FR.RideRate)) + (Vehicle.Assembled_Vehicle.spring_FR.SpringPreload / Vehicle.Assembled_Vehicle.sc_FR.InitialMR));
                    New_RideheightFR = -New_WheelDeflectionFR + Vehicle.Assembled_Vehicle.oc_FR.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight;
                    #endregion

                    #region FRONT RIGHT Calculation of New Push Rod Length
                    G1H1_Perp_FR = Math.Abs(Vehicle.Assembled_Vehicle.sc_FR.G1x - Vehicle.Assembled_Vehicle.sc_FR.H1x);
                    G1H1_FR = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_FR.G1x - Vehicle.Assembled_Vehicle.sc_FR.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_FR.G1y - Vehicle.Assembled_Vehicle.sc_FR.H1y), 2));
                    alphaFR = Math.Asin(G1H1_Perp_FR / G1H1_FR);
                    New_PushRodFR = ((New_RideheightFR - Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight) / Math.Sin(alphaFR)) + Vehicle.Assembled_Vehicle.sc_FR.PushRodLength;
                    Vehicle.Assembled_Vehicle.oc_FR.CW += -Delta_CW_FR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_RL.CW += Delta_CW_FR;
                    New_WheelDeflectionRL_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_RL.CW) / (Vehicle.Assembled_Vehicle.oc_RL.RideRate)) + (Vehicle.Assembled_Vehicle.spring_RL.SpringPreload / Vehicle.Assembled_Vehicle.sc_RL.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight = -New_WheelDeflectionRL_1 + Vehicle.Assembled_Vehicle.oc_RL.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight;


                    #endregion
                    #endregion

                    #region REAR LEFT
                    #region REAR LEFT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_RL = -Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightRL.Text = "";
                        return;
                    }
                    if (String.Format("{0:0.000}", New_CW_RL) == String.Format("{0:0.000}", Vehicle.Assembled_Vehicle.oc_RL.CW_1))
                    {
                        New_CW_RL = Vehicle.Assembled_Vehicle.oc_RL.CW;
                    }
                    Delta_CW_RL = (Vehicle.Assembled_Vehicle.oc_RL.CW - (New_CW_RL));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionRL = -(((9.81 * New_CW_RL) / (Vehicle.Assembled_Vehicle.oc_RL.RideRate)) + (Vehicle.Assembled_Vehicle.spring_RL.SpringPreload / Vehicle.Assembled_Vehicle.sc_RL.InitialMR));
                    New_RideheightRL = -New_WheelDeflectionRL + Vehicle.Assembled_Vehicle.oc_RL.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight;
                    #endregion

                    #region REAR LEFT Calculation of New Push Rod Length
                    G1H1_Perp_RL = Math.Abs(Vehicle.Assembled_Vehicle.sc_RL.G1x - Vehicle.Assembled_Vehicle.sc_RL.H1x);
                    G1H1_RL = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_RL.G1x - Vehicle.Assembled_Vehicle.sc_RL.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_RL.G1y - Vehicle.Assembled_Vehicle.sc_RL.H1y), 2));
                    alphaRL = Math.Asin(G1H1_Perp_RL / G1H1_RL);
                    New_PushRodRL = ((New_RideheightRL - Vehicle.Assembled_Vehicle.oc_RL.FinalRideHeight) / Math.Sin(alphaRL)) + Vehicle.Assembled_Vehicle.sc_RL.PushRodLength;
                    Vehicle.Assembled_Vehicle.oc_RL.CW += -Delta_CW_RL;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_FR.CW += Delta_CW_RL;
                    New_WheelDeflectionFR_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_FR.CW) / (Vehicle.Assembled_Vehicle.oc_FR.RideRate)) + (Vehicle.Assembled_Vehicle.spring_FR.SpringPreload / Vehicle.Assembled_Vehicle.sc_FR.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight = -New_WheelDeflectionFR_1 + Vehicle.Assembled_Vehicle.oc_FR.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_FR.FinalRideHeight;

                    #endregion
                    #endregion

                    #region REAR RIGHT
                    #region REAR RIGHT Calculation of New Ride Height for desired Corner Weight
                    try
                    {
                        New_CW_RR = -Convert.ToDouble(M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.Text);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Invalid Corner Weight entered");
                        M1_Global.List_I1[local_VehicleID - 1].Show();
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.BackColor = Color.IndianRed;
                        M1_Global.List_I1[local_VehicleID - 1].CornerWeightRR.Text = "";
                        return;
                    }

                    if (String.Format("{0:0.00}", New_CW_RR) == String.Format("{0:0.00}", Vehicle.Assembled_Vehicle.oc_RR.CW_1))
                    {
                        New_CW_RR = Vehicle.Assembled_Vehicle.oc_RR.CW;
                    }

                    Delta_CW_RR = (Vehicle.Assembled_Vehicle.oc_RR.CW - (New_CW_RR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                    New_WheelDeflectionRR = -(((9.81 * New_CW_RR) / (Vehicle.Assembled_Vehicle.oc_RR.RideRate)) + (Vehicle.Assembled_Vehicle.spring_RR.SpringPreload / Vehicle.Assembled_Vehicle.sc_RR.InitialMR));
                    New_RideheightRR = -New_WheelDeflectionRR + New_WheelDeflectionRR_1 + Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight;
                    #endregion

                    #region REAR RIGHT Calculation of New Push Rod Length
                    G1H1_Perp_RR = Math.Abs(Vehicle.Assembled_Vehicle.sc_RR.G1x - Vehicle.Assembled_Vehicle.sc_RR.H1x);
                    G1H1_RR = Math.Sqrt(Math.Pow((Vehicle.Assembled_Vehicle.sc_RR.G1x - Vehicle.Assembled_Vehicle.sc_RR.H1x), 2) + Math.Pow((Vehicle.Assembled_Vehicle.sc_RR.G1y - Vehicle.Assembled_Vehicle.sc_RR.H1y), 2));
                    alphaRR = Math.Asin(G1H1_Perp_RR / G1H1_RR);
                    New_PushRodRR = ((New_RideheightRR - Vehicle.Assembled_Vehicle.oc_RR.FinalRideHeight) / Math.Sin(alphaRR)) + Vehicle.Assembled_Vehicle.sc_RR.PushRodLength;
                    Vehicle.Assembled_Vehicle.oc_RR.CW += -Delta_CW_RR;
                    // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                    Vehicle.Assembled_Vehicle.oc_FL.CW += Delta_CW_RR;
                    New_WheelDeflectionFL_1 = -(((9.81 * Vehicle.Assembled_Vehicle.oc_FL.CW) / (Vehicle.Assembled_Vehicle.oc_FL.RideRate)) + (Vehicle.Assembled_Vehicle.spring_FL.SpringPreload / Vehicle.Assembled_Vehicle.sc_FL.InitialMR));
                    Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight = -New_WheelDeflectionFL_1 + Vehicle.Assembled_Vehicle.oc_FL.Corrected_WheelDeflection + Vehicle.Assembled_Vehicle.oc_FL.FinalRideHeight;


                    #endregion
                    #endregion


                    //
                    // Assigning new values of Pushrod Lengths
                    Vehicle.Assembled_Vehicle.sc_FL.PushRodLength = New_PushRodFL;
                    Vehicle.Assembled_Vehicle.sc_FR.PushRodLength = New_PushRodFR;
                    Vehicle.Assembled_Vehicle.sc_RL.PushRodLength = New_PushRodRL;
                    Vehicle.Assembled_Vehicle.sc_RR.PushRodLength = New_PushRodRR;

                    //
                    //Invoking the Kinematics and Vehicle Output Functions again 
                    #region Overriding the Corner Weights to the new values calculated
                    Vehicle.Assembled_Vehicle.OverrideCornerWeights(Vehicle.Assembled_Vehicle.oc_FL.CW, Vehicle.Assembled_Vehicle.oc_FR.CW, Vehicle.Assembled_Vehicle.oc_RL.CW, Vehicle.Assembled_Vehicle.oc_RR.CW);
                    #endregion

                    Vehicle.Assembled_Vehicle.KinematicsInvoker();
                    Vehicle.Assembled_Vehicle.VehicleOutputs();

                    #region Coloring the Corner Weight and Pushrod Length Textboxes
                    CWFL.BackColor = Color.White;
                    PushRodLinkLengthFL.BackColor = Color.LimeGreen;
                    CWFR.BackColor = Color.White;
                    PushRodLinkLengthFR.BackColor = Color.LimeGreen;
                    CWRL.BackColor = Color.White;
                    PushRodLinkLengthRL.BackColor = Color.LimeGreen;
                    CWRR.BackColor = Color.White;
                    PushRodLinkLengthRR.BackColor = Color.LimeGreen;
                    #endregion

                    DisplayOutputs();

                    tabPaneResults.Show();


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

        private void CloseTabPaneResults_ItemClicked(object sender, EventArgs e)
        {
            tabPaneResults.Hide();
        }

        private void OpenAbout_ItemClicked(object sender, ItemClickEventArgs e)
        {
            About A = new About();
            A.Size = new System.Drawing.Size(570, 700);
            A.Show();
        }

        #region Declarations for OPEN/SAVE Commands
        Stream stream;
        Stream stream_Form;
        BinaryFormatter bformatter = new BinaryFormatter();
        BinaryFormatter bformatter_Form = new BinaryFormatter(); 
        #endregion

        #region SAVE Command
        private void barButtonSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                #region Creating an instance of the SaveFileDialog
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "KS files (*.KS)|*.KS";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.OverwritePrompt = true;

                string FileNameWOextensionSave;
                string FileDirectorySave;
                string FileNameSave;

                #endregion


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    #region Creating new streams for the Objects and the GUI
                    stream = File.Open(saveFileDialog1.FileName, FileMode.Create);
                    FileNameWOextensionSave = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                    FileDirectorySave = Path.GetDirectoryName(saveFileDialog1.FileName);
                    FileNameSave = FileDirectorySave + "\\" + FileNameWOextensionSave;
                    stream_Form = File.Open(FileNameSave + ".KSGUI", FileMode.Create);
                    #endregion

                    #region ORDER OF THESE STATEMENTS IS CRUCIAL - Serializing the Objects


                    bformatter.Serialize(stream, Tire.Assy_List_Tire);
                    bformatter.Serialize(stream, Tire.Assy_Tire);
                    bformatter.Serialize(stream, tireGUI);
                    bformatter.Serialize(stream, navBarItemTireClass.navBarItemTire);

                    bformatter.Serialize(stream, Spring.Assy_List_Spring);
                    bformatter.Serialize(stream, Spring.Assy_Spring);
                    bformatter.Serialize(stream, springGUI);
                    bformatter.Serialize(stream, navBarItemSpringClass.navBarItemSpring);

                    bformatter.Serialize(stream, Damper.Assy_List_Damper);
                    bformatter.Serialize(stream, Damper.Assy_Damper);
                    bformatter.Serialize(stream, damperGUI);
                    bformatter.Serialize(stream, navbarItemDamperClass.navBarItemDamper);

                    bformatter.Serialize(stream, AntiRollBar.Assy_List_ARB);
                    bformatter.Serialize(stream, AntiRollBar.Assy_ARB);
                    bformatter.Serialize(stream, arbGUI);
                    bformatter.Serialize(stream, navBarItemARBClass.navBarItemARB);

                    bformatter.Serialize(stream, Chassis.Assy_List_Chassis);
                    bformatter.Serialize(stream, Chassis.Assy_Chassis);
                    bformatter.Serialize(stream, chassisGUI);
                    bformatter.Serialize(stream, navBarItemChassisClass.navBarItemChassis);

                    bformatter.Serialize(stream, WheelAlignment.Assy_List_WA);
                    bformatter.Serialize(stream, WheelAlignment.Assy_WA);
                    bformatter.Serialize(stream, waGUI);
                    bformatter.Serialize(stream, navBarItemWAClass.navBarItemWA);

                    bformatter.Serialize(stream, SuspensionCoordinatesFront.Assy_List_SCFL);
                    bformatter.Serialize(stream, scflGUI);
                    bformatter.Serialize(stream, navBarItemSCFLClass.navBarItemSCFL);

                    bformatter.Serialize(stream, SuspensionCoordinatesFrontRight.Assy_List_SCFR);
                    bformatter.Serialize(stream, scfrGUI);
                    bformatter.Serialize(stream, navBarItemSCFRClass.navBarItemSCFR);

                    bformatter.Serialize(stream, SuspensionCoordinatesRear.Assy_List_SCRL);
                    bformatter.Serialize(stream, scrlGUI);
                    bformatter.Serialize(stream, navBarItemSCRLClass.navBarItemSCRL);

                    bformatter.Serialize(stream, SuspensionCoordinatesRearRight.Assy_List_SCRR);
                    bformatter.Serialize(stream, scrrGUI);
                    bformatter.Serialize(stream, navBarItemSCRRClass.navBarItemSCRR);

                    bformatter.Serialize(stream, M1_Global.Assy_SCM);

                    bformatter.Serialize(stream, M1_Global.vehicleGUI);
                    bformatter.Serialize(stream, progressBar);

                    bformatter.Serialize(stream, Vehicle.List_Vehicle);
                    bformatter.Serialize(stream, navBarItemVehicleClass.navBarItemVehicle);

                    bformatter.Serialize(stream, M1_Global.Assy_OC);

                    bformatter.Serialize(stream, Vehicle.Assembled_Vehicle);

                    bformatter.Serialize(stream, M1_Global.List_I1);


                    navBarControl2.SaveToStream(stream);

                    R1 = this;
                    K1 = new KinematicsSoftwareNewSerialization(R1);

                    bformatter_Form.Serialize(stream_Form, K1);
                 
                    navBarControl1.SaveToStream(stream_Form);

                    

                    #endregion

                    this.Text = "Kinematic Software - " + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                }

                stream.Close();
                stream_Form.Close();

            }
            catch (Exception)
            {
                return;
            }
        } 
        #endregion

        #region OPEN Command
        private void barButtonOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("Save file before proceeding?", "Save Prompt", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    barButtonSave.PerformClick();
                }
                else if (result == DialogResult.No) { }

                #region Creating an instance of the OpenFileDialog
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "KS files (*.KS)|*.KS";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                string FileNameWOextensionOpen;
                string FileDirectoryOpen;
                string FileNameOpen;
                #endregion

                bformatter = new BinaryFormatter();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    #region Setting all the Objects to Null
                    Tire.Assy_List_Tire = null;
                    Tire.Assy_Tire = null;
                    tireGUI = null;
                    navBarItemTireClass.navBarItemTire = null;

                    Spring.Assy_List_Spring = null;
                    Spring.Assy_Spring = null;
                    springGUI = null;
                    navBarItemSpringClass.navBarItemSpring = null;

                    Damper.Assy_List_Damper = null;
                    Damper.Assy_Damper = null;
                    damperGUI = null;
                    navbarItemDamperClass.navBarItemDamper = null;

                    AntiRollBar.Assy_List_ARB = null;
                    AntiRollBar.Assy_ARB = null;
                    arbGUI = null;
                    navBarItemARBClass.navBarItemARB = null;

                    Chassis.Assy_List_Chassis = null;
                    Chassis.Assy_Chassis = null;
                    chassisGUI = null;
                    navBarItemChassisClass.navBarItemChassis = null;

                    WheelAlignment.Assy_List_WA = null;
                    WheelAlignment.Assy_WA = null;
                    waGUI = null;
                    navBarItemWAClass.navBarItemWA = null;

                    SuspensionCoordinatesFront.Assy_List_SCFL = null;
                    scflGUI = null;
                    navBarItemSCFLClass.navBarItemSCFL = null;

                    SuspensionCoordinatesFrontRight.Assy_List_SCFR = null;
                    scfrGUI = null;
                    navBarItemSCFRClass.navBarItemSCFR = null;

                    SuspensionCoordinatesRear.Assy_List_SCRL = null;
                    scrlGUI = null;
                    navBarItemSCRLClass.navBarItemSCRL = null;

                    SuspensionCoordinatesRearRight.Assy_List_SCRR = null;
                    scrrGUI = null;
                    navBarItemSCRRClass.navBarItemSCRR = null;

                    M1_Global.Assy_SCM = null;

                    M1_Global.vehicleGUI = null;
                    progressBar = null;

                    Vehicle.List_Vehicle = null;
                    navBarItemVehicleClass.navBarItemVehicle = null;
                    Vehicle.Assembled_Vehicle = null;

                    M1_Global.List_I1 = null;

                    M1_Global.Assy_OC = null;
                    #endregion

                    #region Opening the Object and GUI Streams
                    stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                    FileNameWOextensionOpen = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    FileDirectoryOpen = Path.GetDirectoryName(openFileDialog1.FileName);
                    FileNameOpen = FileDirectoryOpen + "\\" + FileNameWOextensionOpen;
                    stream_Form = File.Open(FileNameOpen + ".KSGUI", FileMode.Open);
                    #endregion

                    #region ORDER OF THESE STATMENTS IS CRUCIAL - Deserializing the Objects


                    Tire.Assy_List_Tire = (List<Tire>)bformatter.Deserialize(stream);
                    Tire.Assy_Tire = (Tire[])bformatter.Deserialize(stream);
                    tireGUI = (List<TireGUI>)bformatter.Deserialize(stream);
                    navBarItemTireClass.navBarItemTire = (List<navBarItemTireClass>)bformatter.Deserialize(stream);

                    Spring.Assy_List_Spring = (List<Spring>)bformatter.Deserialize(stream);
                    Spring.Assy_Spring = (Spring[])bformatter.Deserialize(stream);
                    springGUI = (List<SpringGUI>)bformatter.Deserialize(stream);
                    navBarItemSpringClass.navBarItemSpring = (List<navBarItemSpringClass>)bformatter.Deserialize(stream);

                    Damper.Assy_List_Damper = (List<Damper>)bformatter.Deserialize(stream);
                    Damper.Assy_Damper = (Damper[])bformatter.Deserialize(stream);
                    damperGUI = ((List<DamperGUI>)bformatter.Deserialize(stream));
                    navbarItemDamperClass.navBarItemDamper = (List<navbarItemDamperClass>)bformatter.Deserialize(stream);

                    AntiRollBar.Assy_List_ARB = (List<AntiRollBar>)bformatter.Deserialize(stream);
                    AntiRollBar.Assy_ARB = (AntiRollBar[])bformatter.Deserialize(stream);
                    arbGUI = ((List<AntiRollBarGUI>)bformatter.Deserialize(stream));
                    navBarItemARBClass.navBarItemARB = (List<navBarItemARBClass>)bformatter.Deserialize(stream);

                    Chassis.Assy_List_Chassis = (List<Chassis>)bformatter.Deserialize(stream);
                    Chassis.Assy_Chassis = (Chassis)bformatter.Deserialize(stream);
                    chassisGUI = ((List<ChassisGUI>)bformatter.Deserialize(stream));
                    navBarItemChassisClass.navBarItemChassis = (List<navBarItemChassisClass>)bformatter.Deserialize(stream);

                    WheelAlignment.Assy_List_WA = (List<WheelAlignment>)bformatter.Deserialize(stream);
                    WheelAlignment.Assy_WA = (WheelAlignment[])bformatter.Deserialize(stream);
                    waGUI = ((List<WheelAlignmentGUI>)bformatter.Deserialize(stream));
                    navBarItemWAClass.navBarItemWA = (List<navBarItemWAClass>)bformatter.Deserialize(stream);

                    SuspensionCoordinatesFront.Assy_List_SCFL = (List<SuspensionCoordinatesFront>)bformatter.Deserialize(stream);
                    scflGUI = (List<SuspensionCoordinatesFrontGUI>)bformatter.Deserialize(stream);
                    navBarItemSCFLClass.navBarItemSCFL = (List<navBarItemSCFLClass>)bformatter.Deserialize(stream);

                    SuspensionCoordinatesFrontRight.Assy_List_SCFR = (List<SuspensionCoordinatesFrontRight>)bformatter.Deserialize(stream);
                    scfrGUI = (List<SuspensionCoordinatesFrontRightGUI>)bformatter.Deserialize(stream);
                    navBarItemSCFRClass.navBarItemSCFR = (List<navBarItemSCFRClass>)bformatter.Deserialize(stream);

                    SuspensionCoordinatesRear.Assy_List_SCRL = (List<SuspensionCoordinatesRear>)bformatter.Deserialize(stream);
                    scrlGUI = (List<SuspensionCoordinatesRearGUI>)bformatter.Deserialize(stream);
                    navBarItemSCRLClass.navBarItemSCRL = (List<navBarItemSCRLClass>)bformatter.Deserialize(stream);

                    SuspensionCoordinatesRearRight.Assy_List_SCRR = (List<SuspensionCoordinatesRearRight>)bformatter.Deserialize(stream);
                    scrrGUI = (List<SuspensionCoordinatesRearRightGUI>)bformatter.Deserialize(stream);
                    navBarItemSCRRClass.navBarItemSCRR = (List<navBarItemSCRRClass>)bformatter.Deserialize(stream);

                    M1_Global.Assy_SCM = (SuspensionCoordinatesMaster[])bformatter.Deserialize(stream);

                    M1_Global.vehicleGUI = (VehicleGUI)bformatter.Deserialize(stream);
                    progressBar = (ProgressBarSerialization)bformatter.Deserialize(stream);

                    Vehicle.List_Vehicle = (List<Vehicle>)bformatter.Deserialize(stream);
                    navBarItemVehicleClass.navBarItemVehicle = (List<navBarItemVehicleClass>)bformatter.Deserialize(stream);

                    M1_Global.Assy_OC = (OutputClass[])bformatter.Deserialize(stream);

                    Vehicle.Assembled_Vehicle = (Vehicle)bformatter.Deserialize(stream);

                    M1_Global.List_I1 = (List<InputSheet>)bformatter.Deserialize(stream);

                    navBarControl2.RestoreFromStream(stream);

                    R1 = this;
                    K1 = new KinematicsSoftwareNewSerialization(R1);
                    K1 = (KinematicsSoftwareNewSerialization)bformatter_Form.Deserialize(stream_Form);
                    K1.RestoreForm(this);

                    navBarControl1.RestoreFromStream(stream_Form);


                    stream.Close();
                    stream_Form.Close();
                    #endregion

                    this.Text = "Kinematics Software - " + Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                }
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

                #region Displaying in the gridview the Data Table of the Input item which was selected while the save operation was done
                if (navBarControl2.ActiveGroup == navBarGroupTireStiffness)
                {
                    gridControl2.MainView = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].bandedGridView_TireGUI;
                    gridControl2.DataSource = tireGUI[navBarGroupTireStiffness.SelectedLinkIndex].TireDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupSprings)
                {
                    gridControl2.MainView = springGUI[navBarGroupSprings.SelectedLinkIndex].bandedGridView_SpringGUI;
                    gridControl2.DataSource = springGUI[navBarGroupSprings.SelectedLinkIndex].SpringDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupDamper)
                {
                    gridControl2.MainView = damperGUI[navBarGroupDamper.SelectedLinkIndex].bandedGridView_DamperGUI;
                    gridControl2.DataSource = damperGUI[navBarGroupDamper.SelectedLinkIndex].DamperDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupAntiRollBar)
                {
                    gridControl2.MainView = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].bandedGridView_ARBGUI;
                    gridControl2.DataSource = arbGUI[navBarGroupAntiRollBar.SelectedLinkIndex].ARBDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupWheelAlignment)
                {
                    gridControl2.MainView = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].bandedGridView_WAGUI;
                    gridControl2.DataSource = waGUI[navBarGroupWheelAlignment.SelectedLinkIndex].WADataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupChassis)
                {
                    gridControl2.MainView = chassisGUI[navBarGroupChassis.SelectedLinkIndex].bandedGridViewChassis;
                    gridControl2.DataSource = chassisGUI[navBarGroupChassis.SelectedLinkIndex].ChassisDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupSuspensionFL)
                {
                    gridControl2.MainView = scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].bandedGridView_SCFLGUI;
                    gridControl2.DataSource = scflGUI[navBarGroupSuspensionFL.SelectedLinkIndex].SCFLDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupSuspensionFR)
                {
                    gridControl2.MainView = scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].bandedGridView_SCFRGUI;
                    gridControl2.DataSource = scfrGUI[navBarGroupSuspensionFR.SelectedLinkIndex].SCFRDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupSuspensionRL)
                {
                    gridControl2.MainView = scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].bandedGridView_SCRLGUI;
                    gridControl2.DataSource = scrlGUI[navBarGroupSuspensionRL.SelectedLinkIndex].SCRLDataTableGUI;
                }
                else if (navBarControl2.ActiveGroup == navBarGroupSuspensionRR)
                {
                    gridControl2.MainView = scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].bandedGridView_SCRRGUI;
                    gridControl2.DataSource = scrrGUI[navBarGroupSuspensionRR.SelectedLinkIndex].SCRRDataTableGUI;
                } 
                #endregion

                #endregion

                #region Restoring the Links of the navBarItems
                for (int i_open_tire = 0; i_open_tire < navBarItemTireClass.navBarItemTire.Count; i_open_tire++)
                {
                    navBarControl2.Items[navBarItemTireClass.navBarItemTire[i_open_tire].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemTire1_LinkClicked);


                }

                for (int i_open_spring = 0; i_open_spring < navBarItemSpringClass.navBarItemSpring.Count; i_open_spring++)
                {
                    navBarControl2.Items[navBarItemSpringClass.navBarItemSpring[i_open_spring].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSpring_LinkClicked);
                }

                for (int i_open_damper = 0; i_open_damper < navbarItemDamperClass.navBarItemDamper.Count; i_open_damper++)
                {
                    navBarControl2.Items[navbarItemDamperClass.navBarItemDamper[i_open_damper].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemDamper_LinkClicked);
                }

                for (int i_open_arb = 0; i_open_arb < navBarItemARBClass.navBarItemARB.Count; i_open_arb++)
                {
                    navBarControl2.Items[navBarItemARBClass.navBarItemARB[i_open_arb].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemARB_LinkClicked);
                }

                for (int i_open_chassis = 0; i_open_chassis < navBarItemChassisClass.navBarItemChassis.Count; i_open_chassis++)
                {
                    navBarControl2.Items[navBarItemChassisClass.navBarItemChassis[i_open_chassis].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemChassis_LinkClicked);
                }

                for (int i_open_wa = 0; i_open_wa < navBarItemWAClass.navBarItemWA.Count; i_open_wa++)
                {
                    navBarControl2.Items[navBarItemWAClass.navBarItemWA[i_open_wa].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemWA_LinkClicked);
                }

                for (int i_open_scfl = 0; i_open_scfl < navBarItemSCFLClass.navBarItemSCFL.Count; i_open_scfl++)
                {
                    navBarControl2.Items[navBarItemSCFLClass.navBarItemSCFL[i_open_scfl].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFL_LinkClicked);
                }

                for (int i_open_scfr = 0; i_open_scfr < navBarItemSCFRClass.navBarItemSCFR.Count; i_open_scfr++)
                {
                    navBarControl2.Items[navBarItemSCFRClass.navBarItemSCFR[i_open_scfr].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFR_LinkClicked);
                }

                for (int i_open_scrl = 0; i_open_scrl < navBarItemSCRLClass.navBarItemSCRL.Count; i_open_scrl++)
                {
                    navBarControl2.Items[navBarItemSCRLClass.navBarItemSCRL[i_open_scrl].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRL_LinkClicked);
                }

                for (int i_open_scrr = 0; i_open_scrr < navBarItemSCRRClass.navBarItemSCRR.Count; i_open_scrr++)
                {
                    navBarControl2.Items[navBarItemSCRRClass.navBarItemSCRR[i_open_scrr].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRR_LinkClicked);
                }

                for (int i_open_vehicle = 0; i_open_vehicle < navBarItemVehicleClass.navBarItemVehicle.Count; i_open_vehicle++)
                {
                    navBarControl2.Items[navBarItemVehicleClass.navBarItemVehicle[i_open_vehicle].Name].LinkClicked += new NavBarLinkEventHandler(navBarItemVehicle_LinkClicked);
                }

                for (int i_open_inputsheet = 0; i_open_inputsheet < M1_Global.List_I1.Count; i_open_inputsheet++)
                {
                    M1_Global.List_I1[i_open_inputsheet] = new InputSheet(this);
                }

                #endregion

                #region Clearing out the Undo/Redo Stacks
                for (int i_open_UndoRedo_Tire = 0; i_open_UndoRedo_Tire < Tire.Assy_List_Tire.Count; i_open_UndoRedo_Tire++)
                {
                    Tire.Assy_List_Tire[i_open_UndoRedo_Tire]._UndocommandsTire.Clear();
                    Tire.Assy_List_Tire[i_open_UndoRedo_Tire]._RedocommandsTire.Clear();
                }

                for (int i_open_UndoRedo_Spring = 0; i_open_UndoRedo_Spring < Spring.Assy_List_Spring.Count; i_open_UndoRedo_Spring++)
                {
                    Spring.Assy_List_Spring[i_open_UndoRedo_Spring]._UndocommandsSpring.Clear();
                    Spring.Assy_List_Spring[i_open_UndoRedo_Spring]._RedocommandsSpring.Clear();
                }


                #endregion

                #region Re-populating the Comboboxes
                ComboboxTireOperations();
                ComboBoxSpringOperations();
                ComboboxDamperOperations();
                ComboboxARBOperations();
                ComboboxChassisOperations();
                ComboboxWheelAlignmentOperations();
                ComboboxSCFLOperations();
                ComboBoxSCFROperations();
                ComboboxSCRLOperations();
                ComboboxSCRROperations();
                ComboboxVehicleOperations();

                #endregion

                K1.RestoreComboboxSelectedIndex(this);

                #region Re-initializing the Progres Bar
                progressBar = new ProgressBarSerialization();
                progressBar.Name = "Progress Bar";
                progressBar.Properties.Maximum = 800;
                progressBar.Properties.Step = 1;
                progressBar.Hide();
                ribbonStatusBar.Controls.Add(progressBar);
                progressBar.Dock = DockStyle.Right;
                M1_Global.vehicleGUI.progressBar = progressBar;


                #endregion

                DisplayOutputs();

                PopulateInputSheet();

                UndoObject.ResetUndoRedo();

                UndoObject_EnableDisableUndoRedoFeature(null, null);

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

                ribbonPageGroupRecalculate.Enabled = false;
                //
                // This a temporary solution to prevent the user from clicking the recalculate if the project is loaded

                for (int i = 0; i < Vehicle.List_Vehicle.Count; i++)
                {
                    Vehicle.List_Vehicle[i].Vehicle_Results_Tracker = 0;
                }

            }
            catch (Exception) { }

        }
        #endregion

        #region New Project event
        private void barButtonNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes to file?", "Save Prompt", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                barButtonSave_ItemClick(sender, e);

                #region Reseting all the lists, Arrays and Counters
                Tire.Assy_List_Tire = new List<Tire>();
                Tire.Assy_Tire = new Tire[4];
                tireGUI = new List<TireGUI>();
                Tire.TireCounter = 0;
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
                navBarItemWAClass.navBarItemWA = new List<navBarItemWAClass>();
                for (int i_NEW_UndoRedo_WA = 0; i_NEW_UndoRedo_WA < WheelAlignment.Assy_List_WA.Count; i_NEW_UndoRedo_WA++)
                {
                    WheelAlignment.Assy_List_WA[i_NEW_UndoRedo_WA]._UndocommandsWheelAlignment = new Stack<ICommand>();
                    WheelAlignment.Assy_List_WA[i_NEW_UndoRedo_WA]._RedocommandsWheelAlignment = new Stack<ICommand>();
                }



                SuspensionCoordinatesFront.Assy_List_SCFL = new List<SuspensionCoordinatesFront>();
                SuspensionCoordinatesFront.SCFLCounter = 0;
                scflGUI = new List<SuspensionCoordinatesFrontGUI>();
                navBarItemSCFLClass.navBarItemSCFL = new List<navBarItemSCFLClass>();
                for (int i_NEW_UndoRedo_SCFL = 0; i_NEW_UndoRedo_SCFL < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_NEW_UndoRedo_SCFL++)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[i_NEW_UndoRedo_SCFL]._UndocommandsSCFL = new Stack<ICommand>();
                    SuspensionCoordinatesFront.Assy_List_SCFL[i_NEW_UndoRedo_SCFL]._RedocommandsSCFL = new Stack<ICommand>();
                }



                SuspensionCoordinatesFrontRight.Assy_List_SCFR = new List<SuspensionCoordinatesFrontRight>();
                SuspensionCoordinatesFrontRight.SCFRCounter = 0;
                scfrGUI = new List<SuspensionCoordinatesFrontRightGUI>();
                navBarItemSCFRClass.navBarItemSCFR = new List<navBarItemSCFRClass>();
                for (int i_NEW_UndoRedo_SCFR = 0; i_NEW_UndoRedo_SCFR < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_NEW_UndoRedo_SCFR++)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_NEW_UndoRedo_SCFR]._UndocommandsSCFR = new Stack<ICommand>();
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_NEW_UndoRedo_SCFR]._RedocommandsSCFR = new Stack<ICommand>();
                }



                SuspensionCoordinatesRear.Assy_List_SCRL = new List<SuspensionCoordinatesRear>();
                SuspensionCoordinatesRear.SCRLCounter = 0;
                scrlGUI = new List<SuspensionCoordinatesRearGUI>();
                navBarItemSCRLClass.navBarItemSCRL = new List<navBarItemSCRLClass>();
                for (int i_NEW_UndoRedo_SCRL = 0; i_NEW_UndoRedo_SCRL < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_NEW_UndoRedo_SCRL++)
                {
                    SuspensionCoordinatesRear.Assy_List_SCRL[i_NEW_UndoRedo_SCRL]._UndocommandsSCRL = new Stack<ICommand>();
                    SuspensionCoordinatesRear.Assy_List_SCRL[i_NEW_UndoRedo_SCRL]._RedocommandsSCRL = new Stack<ICommand>();
                }



                SuspensionCoordinatesRearRight.Assy_List_SCRR = new List<SuspensionCoordinatesRearRight>();
                SuspensionCoordinatesRearRight.SCRRCounter = 0;
                scrrGUI = new List<SuspensionCoordinatesRearRightGUI>();
                navBarItemSCRRClass.navBarItemSCRR = new List<navBarItemSCRRClass>();
                for (int i_NEW_UndoRedo_SCRR = 0; i_NEW_UndoRedo_SCRR < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_NEW_UndoRedo_SCRR++)
                {
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_NEW_UndoRedo_SCRR]._UndocommandsSCRR = new Stack<ICommand>();
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_NEW_UndoRedo_SCRR]._RedocommandsSCRR = new Stack<ICommand>();
                }



                M1_Global.Assy_SCM = new SuspensionCoordinatesMaster[4];

                M1_Global.vehicleGUI = new VehicleGUI();
                progressBar = new ProgressBarSerialization();

                Vehicle.List_Vehicle = new List<Vehicle>();
                navBarItemVehicleClass.navBarItemVehicle = new List<navBarItemVehicleClass>();
                Vehicle.VehicleCounter = 0;
                Vehicle.Assembled_Vehicle = new Vehicle();




                M1_Global.List_I1 = new List<InputSheet>();
                InputSheet.InputSheetCounter = 0; ;

                M1_Global.Assy_OC = new OutputClass[4];

                UndoObject.ResetUndoRedo();

                UndoObject_EnableDisableUndoRedoFeature(null, null);
                #endregion

                #region Reseting the navBarControl
                navBarControl2.Items.Clear();
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

                comboBoxVehicle.Items.Clear();
                #endregion

                sidePanel2.Hide();

                navBarControl1.ActiveGroup = navBarGroupDesign;

                ribbon.SelectedPage = ribbonPageDesign;

                tabPaneResults.Hide();

                ResetOutputs();
            }

            else if (result == DialogResult.No)
            {
                #region Reseting all the lists, Arrays and Counters
                Tire.Assy_List_Tire = new List<Tire>();
                Tire.Assy_Tire = new Tire[4];
                tireGUI = new List<TireGUI>();
                Tire.TireCounter = 0;
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
                navBarItemWAClass.navBarItemWA = new List<navBarItemWAClass>();
                for (int i_NEW_UndoRedo_WA = 0; i_NEW_UndoRedo_WA < WheelAlignment.Assy_List_WA.Count; i_NEW_UndoRedo_WA++)
                {
                    WheelAlignment.Assy_List_WA[i_NEW_UndoRedo_WA]._UndocommandsWheelAlignment = new Stack<ICommand>();
                    WheelAlignment.Assy_List_WA[i_NEW_UndoRedo_WA]._RedocommandsWheelAlignment = new Stack<ICommand>();
                }



                SuspensionCoordinatesFront.Assy_List_SCFL = new List<SuspensionCoordinatesFront>();
                SuspensionCoordinatesFront.SCFLCounter = 0;
                scflGUI = new List<SuspensionCoordinatesFrontGUI>();
                navBarItemSCFLClass.navBarItemSCFL = new List<navBarItemSCFLClass>();
                for (int i_NEW_UndoRedo_SCFL = 0; i_NEW_UndoRedo_SCFL < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i_NEW_UndoRedo_SCFL++)
                {
                    SuspensionCoordinatesFront.Assy_List_SCFL[i_NEW_UndoRedo_SCFL]._UndocommandsSCFL = new Stack<ICommand>();
                    SuspensionCoordinatesFront.Assy_List_SCFL[i_NEW_UndoRedo_SCFL]._RedocommandsSCFL = new Stack<ICommand>();
                }



                SuspensionCoordinatesFrontRight.Assy_List_SCFR = new List<SuspensionCoordinatesFrontRight>();
                SuspensionCoordinatesFrontRight.SCFRCounter = 0;
                scfrGUI = new List<SuspensionCoordinatesFrontRightGUI>();
                navBarItemSCFRClass.navBarItemSCFR = new List<navBarItemSCFRClass>();
                for (int i_NEW_UndoRedo_SCFR = 0; i_NEW_UndoRedo_SCFR < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i_NEW_UndoRedo_SCFR++)
                {
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_NEW_UndoRedo_SCFR]._UndocommandsSCFR = new Stack<ICommand>();
                    SuspensionCoordinatesFrontRight.Assy_List_SCFR[i_NEW_UndoRedo_SCFR]._RedocommandsSCFR = new Stack<ICommand>();
                }



                SuspensionCoordinatesRear.Assy_List_SCRL = new List<SuspensionCoordinatesRear>();
                SuspensionCoordinatesRear.SCRLCounter = 0;
                scrlGUI = new List<SuspensionCoordinatesRearGUI>();
                navBarItemSCRLClass.navBarItemSCRL = new List<navBarItemSCRLClass>();
                for (int i_NEW_UndoRedo_SCRL = 0; i_NEW_UndoRedo_SCRL < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i_NEW_UndoRedo_SCRL++)
                {
                    SuspensionCoordinatesRear.Assy_List_SCRL[i_NEW_UndoRedo_SCRL]._UndocommandsSCRL = new Stack<ICommand>();
                    SuspensionCoordinatesRear.Assy_List_SCRL[i_NEW_UndoRedo_SCRL]._RedocommandsSCRL = new Stack<ICommand>();
                }



                SuspensionCoordinatesRearRight.Assy_List_SCRR = new List<SuspensionCoordinatesRearRight>();
                SuspensionCoordinatesRearRight.SCRRCounter = 0;
                scrrGUI = new List<SuspensionCoordinatesRearRightGUI>();
                navBarItemSCRRClass.navBarItemSCRR = new List<navBarItemSCRRClass>();
                for (int i_NEW_UndoRedo_SCRR = 0; i_NEW_UndoRedo_SCRR < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i_NEW_UndoRedo_SCRR++)
                {
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_NEW_UndoRedo_SCRR]._UndocommandsSCRR = new Stack<ICommand>();
                    SuspensionCoordinatesRearRight.Assy_List_SCRR[i_NEW_UndoRedo_SCRR]._RedocommandsSCRR = new Stack<ICommand>();
                }



                M1_Global.Assy_SCM = new SuspensionCoordinatesMaster[4];

                M1_Global.vehicleGUI = new VehicleGUI();
                progressBar = new ProgressBarSerialization();

                Vehicle.List_Vehicle = new List<Vehicle>();
                navBarItemVehicleClass.navBarItemVehicle = new List<navBarItemVehicleClass>();
                Vehicle.VehicleCounter = 0;
                Vehicle.Assembled_Vehicle = new Vehicle();




                M1_Global.List_I1 = new List<InputSheet>();
                InputSheet.InputSheetCounter = 0; ;

                M1_Global.Assy_OC = new OutputClass[4];

                UndoObject.ResetUndoRedo();

                UndoObject_EnableDisableUndoRedoFeature(null, null);
                #endregion

                #region Reseting the navBarControl
                navBarControl2.Items.Clear();
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

                comboBoxVehicle.Items.Clear();
                #endregion

                sidePanel2.Hide();

                navBarControl1.ActiveGroup = navBarGroupDesign;

                ribbon.SelectedPage = ribbonPageDesign;

                tabPaneResults.Hide();

                ResetOutputs();
            }

            else if (result == DialogResult.Cancel)
            {
                
            }

            this.Text = "Kinematics Software ";


        } 
        #endregion

        #region Form closing event
        private void Kinematics_Software_New_FormClosing(object sender, FormClosingEventArgs e)
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

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            xtraTabControl1.TabPages[xtraTabControl1.SelectedTabPageIndex].Dispose();
        }

        //private void barButtonUserManual_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        System.Diagnostics.Process.Start(@".\User Manual Weekly Deliverable.pdf");
        //    }
        //    catch (Exception)
        //    {

        //        MessageBox.Show("File not found");
        //    }
        //}

    }
}
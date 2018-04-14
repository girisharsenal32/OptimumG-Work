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
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;



namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class KinematicsSoftwareNewSerialization : ISerializable
    {
        #region Autoimplemented Properties of Input Items
        #region Autoimplemented Properties of FRONT LEFT Suspension
        public string A1xFL { get; set; }
        public string A1yFL { get; set; }
        public string A1zFL { get; set; }

        public string B1xFL { get; set; }
        public string B1yFL { get; set; }
        public string B1zFL { get; set; }

        public string C1xFL { get; set; }
        public string C1yFL { get; set; }
        public string C1zFL { get; set; }

        public string D1xFL { get; set; }
        public string D1yFL { get; set; }
        public string D1zFL { get; set; }

        public string E1xFL { get; set; }
        public string E1yFL { get; set; }
        public string E1zFL { get; set; }

        public string F1xFL { get; set; }
        public string F1yFL { get; set; }
        public string F1zFL { get; set; }

        public string G1xFL { get; set; }
        public string G1yFL { get; set; }
        public string G1zFL { get; set; }

        public string H1xFL { get; set; }
        public string H1yFL { get; set; }
        public string H1zFL { get; set; }

        public string I1xFL { get; set; }
        public string I1yFL { get; set; }
        public string I1zFL { get; set; }

        public string J1xFL { get; set; }
        public string J1yFL { get; set; }
        public string J1zFL { get; set; }

        public string JO1xFL { get; set; }
        public string JO1yFL { get; set; }
        public string JO1zFL { get; set; }

        public string K1xFL { get; set; }
        public string K1yFL { get; set; }
        public string K1zFL { get; set; }

        public string M1xFL { get; set; }
        public string M1yFL { get; set; }
        public string M1zFL { get; set; }

        public string N1xFL { get; set; }
        public string N1yFL { get; set; }
        public string N1zFL { get; set; }

        public string O1xFL { get; set; }
        public string O1yFL { get; set; }
        public string O1zFL { get; set; }

        public string P1xFL { get; set; }
        public string P1yFL { get; set; }
        public string P1zFL { get; set; }

        public string Q1xFL { get; set; }
        public string Q1yFL { get; set; }
        public string Q1zFL { get; set; }

        public string R1xFL { get; set; }
        public string R1yFL { get; set; }
        public string R1zFL { get; set; }

        public string W1xFL { get; set; }
        public string W1yFL { get; set; }
        public string W1zFL { get; set; }

        public string RideHeightRefFLx { get; set; }
        public string RideHeightRefFLy { get; set; }
        public string RideHeightRefFLz { get; set; }
        #endregion

        #region Autoimplemented Properties of FRONT RIGHT Suspension
        public string A1xFR { get; set; }
        public string A1yFR { get; set; }
        public string A1zFR { get; set; }

        public string B1xFR { get; set; }
        public string B1yFR { get; set; }
        public string B1zFR { get; set; }

        public string C1xFR { get; set; }
        public string C1yFR { get; set; }
        public string C1zFR { get; set; }

        public string D1xFR { get; set; }
        public string D1yFR { get; set; }
        public string D1zFR { get; set; }

        public string E1xFR { get; set; }
        public string E1yFR { get; set; }
        public string E1zFR { get; set; }

        public string F1xFR { get; set; }
        public string F1yFR { get; set; }
        public string F1zFR { get; set; }

        public string G1xFR { get; set; }
        public string G1yFR { get; set; }
        public string G1zFR { get; set; }

        public string H1xFR { get; set; }
        public string H1yFR { get; set; }
        public string H1zFR { get; set; }

        public string I1xFR { get; set; }
        public string I1yFR { get; set; }
        public string I1zFR { get; set; }

        public string J1xFR { get; set; }
        public string J1yFR { get; set; }
        public string J1zFR { get; set; }

        public string JO1xFR { get; set; }
        public string JO1yFR { get; set; }
        public string JO1zFR { get; set; }

        public string K1xFR { get; set; }
        public string K1yFR { get; set; }
        public string K1zFR { get; set; }

        public string M1xFR { get; set; }
        public string M1yFR { get; set; }
        public string M1zFR { get; set; }

        public string N1xFR { get; set; }
        public string N1yFR { get; set; }
        public string N1zFR { get; set; }

        public string O1xFR { get; set; }
        public string O1yFR { get; set; }
        public string O1zFR { get; set; }

        public string P1xFR { get; set; }
        public string P1yFR { get; set; }
        public string P1zFR { get; set; }

        public string Q1xFR { get; set; }
        public string Q1yFR { get; set; }
        public string Q1zFR { get; set; }

        public string R1xFR { get; set; }
        public string R1yFR { get; set; }
        public string R1zFR { get; set; }

        public string W1xFR { get; set; }
        public string W1yFR { get; set; }
        public string W1zFR { get; set; }

        public string RideHeightRefFRx { get; set; }
        public string RideHeightRefFRy { get; set; }
        public string RideHeightRefFRz { get; set; }
        #endregion

        #region Autoimplemented Properties of REAR LEFT Suspension
        public string A1xRL { get; set; }
        public string A1yRL { get; set; }
        public string A1zRL { get; set; }

        public string B1xRL { get; set; }
        public string B1yRL { get; set; }
        public string B1zRL { get; set; }

        public string C1xRL { get; set; }
        public string C1yRL { get; set; }
        public string C1zRL { get; set; }

        public string D1xRL { get; set; }
        public string D1yRL { get; set; }
        public string D1zRL { get; set; }

        public string E1xRL { get; set; }
        public string E1yRL { get; set; }
        public string E1zRL { get; set; }

        public string F1xRL { get; set; }
        public string F1yRL { get; set; }
        public string F1zRL { get; set; }

        public string G1xRL { get; set; }
        public string G1yRL { get; set; }
        public string G1zRL { get; set; }

        public string H1xRL { get; set; }
        public string H1yRL { get; set; }
        public string H1zRL { get; set; }

        public string I1xRL { get; set; }
        public string I1yRL { get; set; }
        public string I1zRL { get; set; }

        public string J1xRL { get; set; }
        public string J1yRL { get; set; }
        public string J1zRL { get; set; }

        public string JO1xRL { get; set; }
        public string JO1yRL { get; set; }
        public string JO1zRL { get; set; }

        public string K1xRL { get; set; }
        public string K1yRL { get; set; }
        public string K1zRL { get; set; }

        public string M1xRL { get; set; }
        public string M1yRL { get; set; }
        public string M1zRL { get; set; }

        public string N1xRL { get; set; }
        public string N1yRL { get; set; }
        public string N1zRL { get; set; }

        public string O1xRL { get; set; }
        public string O1yRL { get; set; }
        public string O1zRL { get; set; }

        public string P1xRL { get; set; }
        public string P1yRL { get; set; }
        public string P1zRL { get; set; }

        public string Q1xRL { get; set; }
        public string Q1yRL { get; set; }
        public string Q1zRL { get; set; }

        public string R1xRL { get; set; }
        public string R1yRL { get; set; }
        public string R1zRL { get; set; }

        public string W1xRL { get; set; }
        public string W1yRL { get; set; }
        public string W1zRL { get; set; }

        public string RideHeightRefRLx { get; set; }
        public string RideHeightRefRLy { get; set; }
        public string RideHeightRefRLz { get; set; }
        #endregion

        #region Autoimplemented Properties of REAR RIGHT Suspension
        public string A1xRR { get; set; }
        public string A1yRR { get; set; }
        public string A1zRR { get; set; }

        public string B1xRR { get; set; }
        public string B1yRR { get; set; }
        public string B1zRR { get; set; }

        public string C1xRR { get; set; }
        public string C1yRR { get; set; }
        public string C1zRR { get; set; }

        public string D1xRR { get; set; }
        public string D1yRR { get; set; }
        public string D1zRR { get; set; }

        public string E1xRR { get; set; }
        public string E1yRR { get; set; }
        public string E1zRR { get; set; }

        public string F1xRR { get; set; }
        public string F1yRR { get; set; }
        public string F1zRR { get; set; }

        public string G1xRR { get; set; }
        public string G1yRR { get; set; }
        public string G1zRR { get; set; }

        public string H1xRR { get; set; }
        public string H1yRR { get; set; }
        public string H1zRR { get; set; }

        public string I1xRR { get; set; }
        public string I1yRR { get; set; }
        public string I1zRR { get; set; }

        public string J1xRR { get; set; }
        public string J1yRR { get; set; }
        public string J1zRR { get; set; }

        public string JO1xRR { get; set; }
        public string JO1yRR { get; set; }
        public string JO1zRR { get; set; }

        public string K1xRR { get; set; }
        public string K1yRR { get; set; }
        public string K1zRR { get; set; }

        public string M1xRR { get; set; }
        public string M1yRR { get; set; }
        public string M1zRR { get; set; }

        public string N1xRR { get; set; }
        public string N1yRR { get; set; }
        public string N1zRR { get; set; }

        public string O1xRR { get; set; }
        public string O1yRR { get; set; }
        public string O1zRR { get; set; }

        public string P1xRR { get; set; }
        public string P1yRR { get; set; }
        public string P1zRR { get; set; }

        public string Q1xRR { get; set; }
        public string Q1yRR { get; set; }
        public string Q1zRR { get; set; }

        public string R1xRR { get; set; }
        public string R1yRR { get; set; }
        public string R1zRR { get; set; }

        public string W1xRR { get; set; }
        public string W1yRR { get; set; }
        public string W1zRR { get; set; }

        public string RideHeightRefRRx { get; set; }
        public string RideHeightRefRRy { get; set; }
        public string RideHeightRefRRz { get; set; }
        #endregion

        #region Autoimplemented Properties of TIRE
        public string TireRate { get; set; }
        public string TireWidth { get; set; }
        #endregion

        #region Autoimplemented Properties of SPRING
        public string SpringRate { get; set; }
        public string SpringPreload { get; set; }
        public string SpringFreeLength { get; set; }
        #endregion

        #region Autoimplemented Properties of ARB
        public string AntiRollBarRate { get; set; }
        #endregion

        #region Autoimplemented Properties of Damper
        public string DamperGasPressure { get; set; }
        public string DamperShaftDia { get; set; }
        #endregion

        #region Autoimplemented Properties of CHASSIS
        public string SuspendedMass { get; set; }

        public string NonSuspendedMassFL { get; set; }
        public string NonSuspendedMassFR { get; set; }
        public string NonSuspendedMassRL { get; set; }
        public string NonSuspendedMassRR { get; set; }

        public string SMCGx { get; set; }
        public string SMCGy { get; set; }
        public string SMCGz { get; set; }

        public string NSMCGFLx { get; set; }
        public string NSMCGFLy { get; set; }
        public string NSMCGFLz { get; set; }

        public string NSMCGFRx { get; set; }
        public string NSMCGFRy { get; set; }
        public string NSMCGFRz { get; set; }

        public string NSMCGRLx { get; set; }
        public string NSMCGRLy { get; set; }
        public string NSMCGRLz { get; set; }

        public string NSMCGRRx { get; set; }
        public string NSMCGRRy { get; set; }
        public string NSMCGRRz { get; set; }

        #endregion

        #region Autoimplemented Properties of WHEEL ALIGNMENT
        public string StaticCamber { get; set; }
        public string StaticToe { get; set; }
        #endregion 

        #region Autoimplemented Propertes of Vehicle Item
        public int comboBoxSCFL_SelectedItemIndex { get; set; }
        public int comboBoxSCFR_SelectedItemIndex { get; set; }
        public int comboBoxSCRL_SelectedItemIndex { get; set; }
        public int comboBoxSCRR_SelectedItemIndex { get; set; }

        public int comboBoxTireFL_SelectedItem { get; set; }
        public int comboBoxTireFR_SelectedItem { get; set; }
        public int comboBoxTireRL_SelectedItem { get; set; }
        public int comboBoxTireRR_SelectedItem { get; set; }

        public int comboBoxSpringFL_SelectedItem { get; set; }
        public int comboBoxSpringFR_SelectedItem { get; set; }
        public int comboBoxSpringRL_SelectedItem { get; set; }
        public int comboBoxSpringRR_SelectedItem { get; set; }
        
        public int comboBoxDamperFL_SelectedItem { get; set; }
        public int comboBoxDamperFR_SelectedItem { get; set; }
        public int comboBoxDamperRL_SelectedItem { get; set; }
        public int comboBoxDamperRR_SelectedItem { get; set; }

        public int comboBoxARBFront_SelectedItem { get; set; }
        public int comboBoxARBRear_SelectedItem { get; set; }

        public int comboBoxChassis_SelectedItem { get; set; }

        public int comboBoxWAFL_SelectedItem { get; set; }
        public int comboBoxWAFR_SelectedItem { get; set; }
        public int comboBoxWARL_SelectedItem { get; set; }
        public int comboBoxWARR_SelectedItem { get; set; }

        public List<int> comboBoxVehicle_SelectedItem = new List<int>();
        public List<int> combobocMotion_SelectedItem = new List<int>();


        #endregion
        #endregion

        #region Input Item Controls
        #region Declaration of the FRONT LEFT Controls

        #region FRONT LEFT Navigation Page
        SerNavigationPage navigationPagePushRodFL = new SerNavigationPage();
        #endregion

        #region FRONT LEFT Accordion Control and its Elements

        public SerAccordionControl accordionControlSuspensionCoordinatesFL = new SerAccordionControl();

        public SerAccordionControlElement accordionControlFixedPointFL = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFL = new SerAccordionControlElement();

        public SerAccordionControlElement accordionControlFixedPointsFLUpperFrontChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsFLUpperRearChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsFLBellCrankPivot = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFLPushRodBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFLAntiRollBarBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFLUpperBallJoint = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsFLTorsionBarBottom = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFLPushRodUpright = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFLDamperBellCrank = new SerAccordionControlElement();

        #endregion

        #endregion

        #region Declaration of the FRONT RIGHT Controls

        #region FRONT RIGHT Navigation Page
        SerNavigationPage navigationPagePushRodFR = new SerNavigationPage();
        #endregion

        #region FRONT RIGHT Accordion Control and its Elements

        public SerAccordionControl accordionControlSuspensionCoordinatesFR = new SerAccordionControl();

        public SerAccordionControlElement accordionControlFixedPointFR = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFR = new SerAccordionControlElement();

        public SerAccordionControlElement accordionControlFixedPointsFRUpperFrontChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsFRUpperRearChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsFRBellCrankPivot = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFRPushRodBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFRAntiRollBarBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFRUpperBallJoint = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsFRTorsionBarBottom = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFRPushRodUpright = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsFRDamperBellCrank = new SerAccordionControlElement();

        #endregion

        #endregion

        #region Declaration of the REAR LEFT Controls

        #region REAR LEFT Navigation Page
        SerNavigationPage navigationPagePushRodRL = new SerNavigationPage();
        #endregion

        #region REAR LEFT Accordion Control and its Elements

        public SerAccordionControl accordionControlSuspensionCoordinatesRL = new SerAccordionControl();

        public SerAccordionControlElement accordionControlFixedPointRL = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRL = new SerAccordionControlElement();

        public SerAccordionControlElement accordionControlFixedPointsRLUpperFrontChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsRLUpperRearChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsRLBellCrankPivot = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRLPushRodBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRLAntiRollBarBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRLUpperBallJoint = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsRLTorsionBarBottom = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRLPushRodUpright = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRLDamperBellCrank = new SerAccordionControlElement();

        #endregion

        #endregion

        #region Declaration of the REAR RIGHT Controls

        #region REAR RIGHT Navigation Page
        SerNavigationPage navigationPagePushRodRR = new SerNavigationPage();
        #endregion

        #region REAR RIGHT Accordion Control and its Elements

        public SerAccordionControl accordionControlSuspensionCoordinatesRR = new SerAccordionControl();

        public SerAccordionControlElement accordionControlFixedPointRR = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRR = new SerAccordionControlElement();

        public SerAccordionControlElement accordionControlFixedPointsRRUpperFrontChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsRRUpperRearChassis = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsRRBellCrankPivot = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRRPushRodBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRRAntiRollBarBellCrank = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRRUpperBallJoint = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlFixedPointsRRTorsionBarBottom = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRRPushRodUpright = new SerAccordionControlElement();
        public SerAccordionControlElement accordionControlMovingPointsRRDamperBellCrank = new SerAccordionControlElement();

        #endregion

        #endregion

        #region Declaration of the TIRE Controls
        SerAccordionControl accordionControlTireStiffness = new SerAccordionControl();
        #endregion

        #region Declaration of the SPRING Controls
        SerAccordionControl accordionControlSprings = new SerAccordionControl();
        #endregion

        #region Declaration of the ARB Controls
        SerAccordionControl accordionControlAntiRollBar = new SerAccordionControl();
        #endregion

        #region Declaration of the DAMPER Controls
        SerAccordionControl accordionControlDamper = new SerAccordionControl();
        #endregion

        #region Declaration of the CHASSIS Controls
        SerAccordionControl accordionControlChassis = new SerAccordionControl();
        #endregion

        #region Declaration of the WHEEL ALIGNMENT Controls
        SerAccordionControl accordionControlWheelAlignment = new SerAccordionControl();
        #endregion

        #region Declaration of the VEHICLE Controls
        SerAccordionControl accordionControlVehicleItem = new SerAccordionControl();
        #endregion 
        #endregion

        #region Declaration of GroupControl ELement
        public SerGroupControl groupControl13 = new SerGroupControl();
        #endregion

        #region Declaration of SidePanel Element
        public SerSidePanel sidePanel2 = new SerSidePanel();
        #endregion

        #region Declaration of the Tab Page Control
        SerTabPane tabPaneResults = new SerTabPane();
        CustomXtraTabControl xtraTabControl = new CustomXtraTabControl();
        int SelectedPage;
        #endregion

        #region Declration of the Ribbon Page index
        public int selectedRibbonPage;
        #endregion

        public KinematicsSoftwareNewSerialization(Kinematics_Software_New R1)
        {
            #region Initializing the Autoimplemented Input Items with the lates values of the Textboxes which the user has added
            #region Assigning the Serializable form's FRONT LEFT SUSPENSION COORDINATE TextBoxes with the lates values which the user had added
            A1xFL = R1.A1xFL.Text;
            A1yFL = R1.A1yFL.Text;
            A1zFL = R1.A1zFL.Text;

            B1xFL = R1.B1xFL.Text;
            B1yFL = R1.B1yFL.Text;
            B1zFL = R1.B1zFL.Text;

            C1xFL = R1.C1xFL.Text;
            C1yFL = R1.C1yFL.Text;
            C1zFL = R1.C1zFL.Text;

            D1xFL = R1.D1xFL.Text;
            D1yFL = R1.D1yFL.Text;
            D1zFL = R1.D1zFL.Text;

            E1xFL = R1.E1xFL.Text;
            E1yFL = R1.E1yFL.Text;
            E1zFL = R1.E1zFL.Text;

            F1xFL = R1.F1xFL.Text;
            F1yFL = R1.F1yFL.Text;
            F1zFL = R1.F1zFL.Text;

            G1xFL = R1.G1xFL.Text;
            G1yFL = R1.G1yFL.Text;
            G1zFL = R1.G1zFL.Text;

            H1xFL = R1.H1xFL.Text;
            H1yFL = R1.H1yFL.Text;
            H1zFL = R1.H1zFL.Text;

            I1xFL = R1.I1xFL.Text;
            I1yFL = R1.I1yFL.Text;
            I1zFL = R1.I1zFL.Text;

            J1xFL = R1.J1xFL.Text;
            J1yFL = R1.J1yFL.Text;
            J1zFL = R1.J1zFL.Text;

            JO1xFL = R1.JO1xFL.Text;
            JO1yFL = R1.JO1yFL.Text;
            JO1zFL = R1.JO1zFL.Text;

            K1xFL = R1.K1xFL.Text;
            K1yFL = R1.K1yFL.Text;
            K1zFL = R1.K1zFL.Text;

            M1xFL = R1.M1xFL.Text;
            M1yFL = R1.M1yFL.Text;
            M1zFL = R1.M1zFL.Text;

            N1xFL = R1.N1xFL.Text;
            N1yFL = R1.N1yFL.Text;
            N1zFL = R1.N1zFL.Text;

            O1xFL = R1.O1xFL.Text;
            O1yFL = R1.O1yFL.Text;
            O1zFL = R1.O1zFL.Text;

            P1xFL = R1.P1xFL.Text;
            P1yFL = R1.P1yFL.Text;
            P1zFL = R1.P1zFL.Text;

            Q1xFL = R1.Q1xFL.Text;
            Q1yFL = R1.Q1yFL.Text;
            Q1zFL = R1.Q1zFL.Text;

            R1xFL = R1.R1xFL.Text;
            R1yFL = R1.R1yFL.Text;
            R1zFL = R1.R1zFL.Text;

            W1xFL = R1.W1xFL.Text;
            W1yFL = R1.W1yFL.Text;
            W1zFL = R1.W1zFL.Text;

            RideHeightRefFLx = R1.RideHeightRefFLx.Text;
            RideHeightRefFLy = R1.RideHeightRefFLy.Text;
            RideHeightRefFLz = R1.RideHeightRefFLz.Text;
            #endregion

            #region Assigning the Serializable form's FRONT RIGHT SUSPENSION COORDINATE TextBoxes with the lates values which the user had added
            A1xFR = R1.A1xFR.Text;
            A1yFR = R1.A1yFR.Text;
            A1zFR = R1.A1zFR.Text;

            B1xFR = R1.B1xFR.Text;
            B1yFR = R1.B1yFR.Text;
            B1zFR = R1.B1zFR.Text;

            C1xFR = R1.C1xFR.Text;
            C1yFR = R1.C1yFR.Text;
            C1zFR = R1.C1zFR.Text;

            D1xFR = R1.D1xFR.Text;
            D1yFR = R1.D1yFR.Text;
            D1zFR = R1.D1zFR.Text;

            E1xFR = R1.E1xFR.Text;
            E1yFR = R1.E1yFR.Text;
            E1zFR = R1.E1zFR.Text;

            F1xFR = R1.F1xFR.Text;
            F1yFR = R1.F1yFR.Text;
            F1zFR = R1.F1zFR.Text;

            G1xFR = R1.G1xFR.Text;
            G1yFR = R1.G1yFR.Text;
            G1zFR = R1.G1zFR.Text;

            H1xFR = R1.H1xFR.Text;
            H1yFR = R1.H1yFR.Text;
            H1zFR = R1.H1zFR.Text;

            I1xFR = R1.I1xFR.Text;
            I1yFR = R1.I1yFR.Text;
            I1zFR = R1.I1zFR.Text;

            J1xFR = R1.J1xFR.Text;
            J1yFR = R1.J1yFR.Text;
            J1zFR = R1.J1zFR.Text;

            JO1xFR = R1.JO1xFR.Text;
            JO1yFR = R1.JO1yFR.Text;
            JO1zFR = R1.JO1zFR.Text;

            K1xFR = R1.K1xFR.Text;
            K1yFR = R1.K1yFR.Text;
            K1zFR = R1.K1zFR.Text;

            M1xFR = R1.M1xFR.Text;
            M1yFR = R1.M1yFR.Text;
            M1zFR = R1.M1zFR.Text;

            N1xFR = R1.N1xFR.Text;
            N1yFR = R1.N1yFR.Text;
            N1zFR = R1.N1zFR.Text;

            O1xFR = R1.O1xFR.Text;
            O1yFR = R1.O1yFR.Text;
            O1zFR = R1.O1zFR.Text;

            P1xFR = R1.P1xFR.Text;
            P1yFR = R1.P1yFR.Text;
            P1zFR = R1.P1zFR.Text;

            Q1xFR = R1.Q1xFR.Text;
            Q1yFR = R1.Q1yFR.Text;
            Q1zFR = R1.Q1zFR.Text;

            R1xFR = R1.R1xFR.Text;
            R1yFR = R1.R1yFR.Text;
            R1zFR = R1.R1zFR.Text;

            W1xFR = R1.W1xFR.Text;
            W1yFR = R1.W1yFR.Text;
            W1zFR = R1.W1zFR.Text;

            RideHeightRefFRx = R1.RideHeightRefFRx.Text;
            RideHeightRefFRy = R1.RideHeightRefFRy.Text;
            RideHeightRefFRz = R1.RideHeightRefFRz.Text;
            #endregion

            #region Assigning the Serializable form's REAR LEFT SUSPENSION COORDINATE TextBoxes with the lates values which the user had added
            A1xRL = R1.A1xRL.Text;
            A1yRL = R1.A1yRL.Text;
            A1zRL = R1.A1zRL.Text;

            B1xRL = R1.B1xRL.Text;
            B1yRL = R1.B1yRL.Text;
            B1zRL = R1.B1zRL.Text;

            C1xRL = R1.C1xRL.Text;
            C1yRL = R1.C1yRL.Text;
            C1zRL = R1.C1zRL.Text;

            D1xRL = R1.D1xRL.Text;
            D1yRL = R1.D1yRL.Text;
            D1zRL = R1.D1zRL.Text;

            E1xRL = R1.E1xRL.Text;
            E1yRL = R1.E1yRL.Text;
            E1zRL = R1.E1zRL.Text;

            F1xRL = R1.F1xRL.Text;
            F1yRL = R1.F1yRL.Text;
            F1zRL = R1.F1zRL.Text;

            G1xRL = R1.G1xRL.Text;
            G1yRL = R1.G1yRL.Text;
            G1zRL = R1.G1zRL.Text;

            H1xRL = R1.H1xRL.Text;
            H1yRL = R1.H1yRL.Text;
            H1zRL = R1.H1zRL.Text;

            I1xRL = R1.I1xRL.Text;
            I1yRL = R1.I1yRL.Text;
            I1zRL = R1.I1zRL.Text;

            J1xRL = R1.J1xRL.Text;
            J1yRL = R1.J1yRL.Text;
            J1zRL = R1.J1zRL.Text;

            JO1xRL = R1.JO1xRL.Text;
            JO1yRL = R1.JO1yRL.Text;
            JO1zRL = R1.JO1zRL.Text;

            K1xRL = R1.K1xRL.Text;
            K1yRL = R1.K1yRL.Text;
            K1zRL = R1.K1zRL.Text;

            M1xRL = R1.M1xRL.Text;
            M1yRL = R1.M1yRL.Text;
            M1zRL = R1.M1zRL.Text;

            N1xRL = R1.N1xRL.Text;
            N1yRL = R1.N1yRL.Text;
            N1zRL = R1.N1zRL.Text;

            O1xRL = R1.O1xRL.Text;
            O1yRL = R1.O1yRL.Text;
            O1zRL = R1.O1zRL.Text;

            P1xRL = R1.P1xRL.Text;
            P1yRL = R1.P1yRL.Text;
            P1zRL = R1.P1zRL.Text;

            Q1xRL = R1.Q1xRL.Text;
            Q1yRL = R1.Q1yRL.Text;
            Q1zRL = R1.Q1zRL.Text;

            R1xRL = R1.R1xRL.Text;
            R1yRL = R1.R1yRL.Text;
            R1zRL = R1.R1zRL.Text;

            W1xRL = R1.W1xRL.Text;
            W1yRL = R1.W1yRL.Text;
            W1zRL = R1.W1zRL.Text;

            RideHeightRefRLx = R1.RideHeightRefRLx.Text;
            RideHeightRefRLy = R1.RideHeightRefRLy.Text;
            RideHeightRefRLz = R1.RideHeightRefRLz.Text;
            #endregion

            #region Assigning the Serializable form's REAR RIGHT SUSPENSION COORDINATE TextBoxes with the lates values which the user had added
            A1xRR = R1.A1xRR.Text;
            A1yRR = R1.A1yRR.Text;
            A1zRR = R1.A1zRR.Text;

            B1xRR = R1.B1xRR.Text;
            B1yRR = R1.B1yRR.Text;
            B1zRR = R1.B1zRR.Text;

            C1xRR = R1.C1xRR.Text;
            C1yRR = R1.C1yRR.Text;
            C1zRR = R1.C1zRR.Text;

            D1xRR = R1.D1xRR.Text;
            D1yRR = R1.D1yRR.Text;
            D1zRR = R1.D1zRR.Text;

            E1xRR = R1.E1xRR.Text;
            E1yRR = R1.E1yRR.Text;
            E1zRR = R1.E1zRR.Text;

            F1xRR = R1.F1xRR.Text;
            F1yRR = R1.F1yRR.Text;
            F1zRR = R1.F1zRR.Text;

            G1xRR = R1.G1xRR.Text;
            G1yRR = R1.G1yRR.Text;
            G1zRR = R1.G1zRR.Text;

            H1xRR = R1.H1xRR.Text;
            H1yRR = R1.H1yRR.Text;
            H1zRR = R1.H1zRR.Text;

            I1xRR = R1.I1xRR.Text;
            I1yRR = R1.I1yRR.Text;
            I1zRR = R1.I1zRR.Text;

            J1xRR = R1.J1xRR.Text;
            J1yRR = R1.J1yRR.Text;
            J1zRR = R1.J1zRR.Text;

            JO1xRR = R1.JO1xRR.Text;
            JO1yRR = R1.JO1yRR.Text;
            JO1zRR = R1.JO1zRR.Text;

            K1xRR = R1.K1xRR.Text;
            K1yRR = R1.K1yRR.Text;
            K1zRR = R1.K1zRR.Text;

            M1xRR = R1.M1xRR.Text;
            M1yRR = R1.M1yRR.Text;
            M1zRR = R1.M1zRR.Text;

            N1xRR = R1.N1xRR.Text;
            N1yRR = R1.N1yRR.Text;
            N1zRR = R1.N1zRR.Text;

            O1xRR = R1.O1xRR.Text;
            O1yRR = R1.O1yRR.Text;
            O1zRR = R1.O1zRR.Text;

            P1xRR = R1.P1xRR.Text;
            P1yRR = R1.P1yRR.Text;
            P1zRR = R1.P1zRR.Text;

            Q1xRR = R1.Q1xRR.Text;
            Q1yRR = R1.Q1yRR.Text;
            Q1zRR = R1.Q1zRR.Text;

            R1xRR = R1.R1xRR.Text;
            R1yRR = R1.R1yRR.Text;
            R1zRR = R1.R1zRR.Text;

            W1xRR = R1.W1xRR.Text;
            W1yRR = R1.W1yRR.Text;
            W1zRR = R1.W1zRR.Text;

            RideHeightRefRRx = R1.RideHeightRefRRx.Text;
            RideHeightRefRRy = R1.RideHeightRefRRy.Text;
            RideHeightRefRRz = R1.RideHeightRefRRz.Text;
            #endregion

            #region Assigning the Serializable form's TIRE TextBoxes with the lates values which the user had added
            TireRate = R1.TireRateFL.Text;
            TireWidth = R1.TireWidthFL.Text;
            #endregion

            #region Assigning the Serializable form's SPRING TextBoxes with the lates values which the user had added
            SpringRate = R1.SpringRateFL.Text;
            SpringPreload = R1.SpringPreloadFL.Text;
            SpringFreeLength = R1.SpringFreeLengthFL.Text;
            #endregion

            #region Assigning the Serializable form's ARB TextBoxes with the lates values which the user had added
            AntiRollBarRate = R1.AntiRollBarRateFront.Text;
            #endregion

            #region Assigning the Serializable form's DAMPER TextBoxes with the lates values which the user had added
            DamperGasPressure = R1.DamperGasPressureFL.Text;
            DamperShaftDia = R1.DamperShaftDiaFL.Text;
            #endregion

            #region Assigning the Serializable form's CHASSIS TextBoxes with the lates values which the user had added
            SuspendedMass = R1.SuspendedMass.Text;

            NonSuspendedMassFL = R1.NonSuspendedMassFL.Text;
            NonSuspendedMassFR = R1.NonSuspendedMassFR.Text;
            NonSuspendedMassRL = R1.NonSuspendedMassRL.Text;
            NonSuspendedMassRR = R1.NonSuspendedMassRR.Text;

            SMCGx = R1.SMCGx.Text;
            SMCGy = R1.SMCGy.Text;
            SMCGz = R1.SMCGz.Text;

            NSMCGFLx = R1.NSMCGFLx.Text;
            NSMCGFLy = R1.NSMCGFLy.Text;
            NSMCGFLz = R1.NSMCGFLz.Text;

            NSMCGFRx = R1.NSMCGFRx.Text;
            NSMCGFRy = R1.NSMCGFRy.Text;
            NSMCGFRz = R1.NSMCGFRz.Text;

            NSMCGRLx = R1.NSMCGRLx.Text;
            NSMCGRLy = R1.NSMCGRLy.Text;
            NSMCGRLz = R1.NSMCGRLz.Text;

            NSMCGRRx = R1.NSMCGRRx.Text;
            NSMCGRRy = R1.NSMCGRRy.Text;
            NSMCGRRz = R1.NSMCGRRz.Text;
            #endregion

            #region Assigning the Serializable form's WHEEL ALIGNMENT TextBoxes with the lates values which the user had added
            StaticCamber = R1.StaticCamberFL.Text;
            StaticToe = R1.StaticToeFL.Text;
            #endregion 

            #region Assigning the Serializable form's VEHICLE ComboBoxes with the lates values which the user had added
            comboBoxSCFL_SelectedItemIndex = R1.comboBoxSCFL.SelectedIndex;
            comboBoxSCFR_SelectedItemIndex = R1.comboBoxSCFR.SelectedIndex;
            comboBoxSCRL_SelectedItemIndex = R1.comboBoxSCRL.SelectedIndex;
            comboBoxSCRR_SelectedItemIndex = R1.comboBoxSCRR.SelectedIndex;

            comboBoxTireFL_SelectedItem = R1.comboBoxTireFL.SelectedIndex;
            comboBoxTireFR_SelectedItem = R1.comboBoxTireFR.SelectedIndex;
            comboBoxTireRL_SelectedItem = R1.comboBoxTireRL.SelectedIndex;
            comboBoxTireRR_SelectedItem = R1.comboBoxTireRR.SelectedIndex;

            comboBoxSpringFL_SelectedItem = R1.comboBoxSpringFL.SelectedIndex;
            comboBoxSpringFR_SelectedItem = R1.comboBoxSpringFR.SelectedIndex;
            comboBoxSpringRL_SelectedItem = R1.comboBoxSpringRL.SelectedIndex;
            comboBoxSpringRR_SelectedItem = R1.comboBoxSpringRR.SelectedIndex;

            comboBoxDamperFL_SelectedItem = R1.comboBoxDamperFL.SelectedIndex;
            comboBoxDamperFR_SelectedItem = R1.comboBoxDamperFR.SelectedIndex;
            comboBoxDamperRL_SelectedItem = R1.comboBoxDamperRL.SelectedIndex;
            comboBoxDamperRR_SelectedItem = R1.comboBoxDamperRR.SelectedIndex;

            comboBoxARBFront_SelectedItem = R1.comboBoxARBFront.SelectedIndex;
            comboBoxARBRear_SelectedItem = R1.comboBoxARBRear.SelectedIndex;

            comboBoxChassis_SelectedItem = R1.comboBoxChassis.SelectedIndex;

            comboBoxWAFL_SelectedItem = R1.comboBoxWAFL.SelectedIndex;
            comboBoxWAFR_SelectedItem = R1.comboBoxWAFR.SelectedIndex;
            comboBoxWARL_SelectedItem = R1.comboBoxWARL.SelectedIndex;
            comboBoxWARR_SelectedItem = R1.comboBoxWARR.SelectedIndex;

            for (int i_StoreIndex = 0; i_StoreIndex < Simulation.List_Simulation.Count; i_StoreIndex++)
            {
                comboBoxVehicle_SelectedItem.Insert(i_StoreIndex, Simulation.List_Simulation[i_StoreIndex].simulationPanel.comboBoxVehicle.SelectedIndex);
                combobocMotion_SelectedItem.Insert(i_StoreIndex, Simulation.List_Simulation[i_StoreIndex].simulationPanel.comboBoxMotion.SelectedIndex);
            }
            
            #endregion

            #endregion

            #region Initializing the Input Item Controls with those in the Form
            #region Obtaining the FRONT LEFT Control Properties

            #region FRONT LEFT Accordion Control
            accordionControlSuspensionCoordinatesFL.Visible = R1.accordionControlSuspensionCoordinatesFL.Visible;

            accordionControlFixedPointFL.Expanded = R1.accordionControlFixedPointsFL.Expanded;
            accordionControlMovingPointsFL.Expanded = R1.accordionControlMovingPointsFL.Expanded;

            accordionControlFixedPointsFLUpperFrontChassis.Visible = R1.accordionControlFixedPointsFLUpperFrontChassis.Visible;
            accordionControlFixedPointsFLUpperRearChassis.Visible = R1.accordionControlFixedPointsFLUpperRearChassis.Visible;
            accordionControlFixedPointsFLBellCrankPivot.Visible = R1.accordionControlFixedPointsFLBellCrankPivot.Visible;
            accordionControlMovingPointsFLPushRodBellCrank.Visible = R1.accordionControlMovingPointsFLPushRodBellCrank.Visible;
            accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = R1.accordionControlMovingPointsFLAntiRollBarBellCrank.Visible;
            accordionControlMovingPointsFLUpperBallJoint.Visible = R1.accordionControlMovingPointsFLUpperBallJoint.Visible;
            accordionControlFixedPointsFLTorsionBarBottom.Visible = R1.accordionControlFixedPointsFLTorsionBarBottom.Visible;
            accordionControlMovingPointsFLPushRodUpright.Visible = R1.accordionControlMovingPointsFLPushRodUpright.Visible;

            accordionControlMovingPointsFLDamperBellCrank.Text = R1.accordionControlMovingPointsFLDamperBellCrank.Text;
            #endregion

            #endregion

            #region Obtaining the FRONT RIGHT Control Properties


            #region FRONT RIGHT Accordion Control
            accordionControlSuspensionCoordinatesFR.Visible = R1.accordionControlSuspensionCoordinatesFR.Visible;

            accordionControlFixedPointFR.Expanded = R1.accordionControlFixedPointsFR.Expanded;
            accordionControlMovingPointsFR.Expanded = R1.accordionControlMovingPointsFR.Expanded;

            accordionControlFixedPointsFRUpperFrontChassis.Visible = R1.accordionControlFixedPointsFRUpperFrontChassis.Visible;
            accordionControlFixedPointsFRUpperRearChassis.Visible = R1.accordionControlFixedPointsFRUpperRearChassis.Visible;
            accordionControlFixedPointsFRBellCrankPivot.Visible = R1.accordionControlFixedPointsFRBellCrankPivot.Visible;
            accordionControlMovingPointsFRPushRodBellCrank.Visible = R1.accordionControlMovingPointsFRPushRodBellCrank.Visible;
            accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = R1.accordionControlMovingPointsFRAntiRollBarBellCrank.Visible;
            accordionControlMovingPointsFRUpperBallJoint.Visible = R1.accordionControlMovingPointsFRUpperBallJoint.Visible;
            accordionControlFixedPointsFRTorsionBarBottom.Visible = R1.accordionControlFixedPointsFRTorsionBarBottom.Visible;
            accordionControlMovingPointsFRPushRodUpright.Visible = R1.accordionControlMovingPointsFRPushRodUpright.Visible;

            accordionControlMovingPointsFRDamperBellCrank.Text = R1.accordionControlMovingPointsFRDamperBellCrank.Text;
            #endregion

            #endregion

            #region Obtaining the REAR LEFT Control Properties

            #region REAR LEFT Accordion Control
            accordionControlSuspensionCoordinatesRL.Visible = R1.accordionControlSuspensionCoordinatesRL.Visible;

            accordionControlFixedPointRL.Expanded = R1.accordionControlFixedPointsRL.Expanded;
            accordionControlMovingPointsRL.Expanded = R1.accordionControlMovingPointsRL.Expanded;

            accordionControlFixedPointsRLUpperFrontChassis.Visible = R1.accordionControlFixedPointsRLUpperFrontChassis.Visible;
            accordionControlFixedPointsRLUpperRearChassis.Visible = R1.accordionControlFixedPointsRLUpperRearChassis.Visible;
            accordionControlFixedPointsRLBellCrankPivot.Visible = R1.accordionControlFixedPointsRLBellCrankPivot.Visible;
            accordionControlMovingPointsRLPushRodBellCrank.Visible = R1.accordionControlMovingPointsRLPushRodBellCrank.Visible;
            accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = R1.accordionControlMovingPointsRLAntiRollBarBellCrank.Visible;
            accordionControlMovingPointsRLUpperBallJoint.Visible = R1.accordionControlMovingPointsRLUpperBallJoint.Visible;
            accordionControlFixedPointsRLTorsionBarBottom.Visible = R1.accordionControlFixedPointsRLTorsionBarBottom.Visible;
            accordionControlMovingPointsRLPushRodUpright.Visible = R1.accordionControlMovingPointsRLPushRodUpright.Visible;

            accordionControlMovingPointsRLDamperBellCrank.Text = R1.accordionControlMovingPointsRLDamperBellCrank.Text;
            #endregion

            #endregion

            #region Obtaining the REAR RIGHT Control Properties


            #region REAR RIGHT Accordion Control
            accordionControlSuspensionCoordinatesRR.Visible = R1.accordionControlSuspensionCoordinatesRR.Visible;

            accordionControlFixedPointRR.Expanded = R1.accordionControlFixedPointsRR.Expanded;
            accordionControlMovingPointsRR.Expanded = R1.accordionControlMovingPointsRR.Expanded;

            accordionControlFixedPointsRRUpperFrontChassis.Visible = R1.accordionControlFixedPointsRRUpperFrontChassis.Visible;
            accordionControlFixedPointsRRUpperRearChassis.Visible = R1.accordionControlFixedPointsRRUpperRearChassis.Visible;
            accordionControlFixedPointsRRBellCrankPivot.Visible = R1.accordionControlFixedPointsRRBellCrankPivot.Visible;
            accordionControlMovingPointsRRPushRodBellCrank.Visible = R1.accordionControlMovingPointsRRPushRodBellCrank.Visible;
            accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = R1.accordionControlMovingPointsRRAntiRollBarBellCrank.Visible;
            accordionControlMovingPointsRRUpperBallJoint.Visible = R1.accordionControlMovingPointsRRUpperBallJoint.Visible;
            accordionControlFixedPointsRRTorsionBarBottom.Visible = R1.accordionControlFixedPointsRRTorsionBarBottom.Visible;
            accordionControlMovingPointsRRPushRodUpright.Visible = R1.accordionControlMovingPointsRRPushRodUpright.Visible;

            accordionControlMovingPointsRRDamperBellCrank.Text = R1.accordionControlMovingPointsRRDamperBellCrank.Text;
            #endregion

            #endregion

            #region Obtaining the TIRE Control Properties
            accordionControlTireStiffness.Visible = R1.accordionControlTireStiffness.Visible;
            #endregion

            #region Obtaining the SPRING Control Properties
            accordionControlSprings.Visible = R1.accordionControlSprings.Visible;
            #endregion

            #region Obtaining the ARB Control Properties
            accordionControlAntiRollBar.Visible = R1.accordionControlAntiRollBar.Visible;
            #endregion

            #region Obtaining the DAMPER Control Properties
            accordionControlDamper.Visible = R1.accordionControlDamper.Visible;
            #endregion

            #region Obtaining the CHASSIS Control Properties
            accordionControlChassis.Visible = R1.accordionControlChassis.Visible;
            #endregion

            #region Obtaining the WHEEL ALIGNMENT Control Properties
            accordionControlWheelAlignment.Visible = R1.accordionControlWheelAlignment.Visible;
            #endregion

            #region Obtaining the VEHICLE Control Properties
            accordionControlVehicleItem.Visible = R1.accordionControlVehicleItem.Visible;
            #endregion 
            #endregion

            #region Obtaining the Group Control's Visibility
            groupControl13.Visible = R1.groupControl13.Visible;
            #endregion

            #region Obtaining the Side Panel's Visibility
            sidePanel2.Visible = R1.sidePanel2.Visible;
            #endregion

            #region Obtaining the Tab Pane Control Properties
            xtraTabControl.Visible = Kinematics_Software_New.TabControl_Outputs.Visible;
            SelectedPage = Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex;
            #endregion

            #region Obtaining the Ribbon's selected page
            selectedRibbonPage = R1.Ribbon.SelectedPage.PageIndex;
            #endregion

        }

        public void  RestoreForm(Kinematics_Software_New r1)
        {
            #region Assigning the Parent form's Input Item Textboxes with the values with which the user had performed the save action
            #region Assigning the Parent form's FRONT LEFT SUSPENSION COORDINATE Textboxes with the values with which the user had performed the save action
            r1.A1xFL.Text = this.A1xFL;
            r1.A1yFL.Text = this.A1yFL;
            r1.A1zFL.Text = this.A1zFL;

            r1.B1xFL.Text = this.B1xFL;
            r1.B1yFL.Text = this.B1yFL;
            r1.B1zFL.Text = this.B1zFL;

            r1.C1xFL.Text = this.C1xFL;
            r1.C1yFL.Text = this.C1yFL;
            r1.C1zFL.Text = this.C1zFL;

            r1.D1xFL.Text = this.D1xFL;
            r1.D1yFL.Text = this.D1yFL;
            r1.D1zFL.Text = this.D1zFL;

            r1.E1xFL.Text = this.E1xFL;
            r1.E1yFL.Text = this.E1yFL;
            r1.E1zFL.Text = this.E1zFL;

            r1.F1xFL.Text = this.F1xFL;
            r1.F1yFL.Text = this.F1yFL;
            r1.F1zFL.Text = this.F1zFL;

            r1.G1xFL.Text = this.G1xFL;
            r1.G1yFL.Text = this.G1yFL;
            r1.G1zFL.Text = this.G1zFL;

            r1.H1xFL.Text = this.H1xFL;
            r1.H1yFL.Text = this.H1yFL;
            r1.H1zFL.Text = this.H1zFL;

            r1.I1xFL.Text = this.I1xFL;
            r1.I1yFL.Text = this.I1yFL;
            r1.I1zFL.Text = this.I1zFL;

            r1.J1xFL.Text = this.J1xFL;
            r1.J1yFL.Text = this.J1yFL;
            r1.J1zFL.Text = this.J1zFL;

            r1.JO1xFL.Text = this.JO1xFL;
            r1.JO1yFL.Text = this.JO1yFL;
            r1.JO1zFL.Text = this.JO1zFL;

            r1.K1xFL.Text = this.K1xFL;
            r1.K1yFL.Text = this.K1yFL;
            r1.K1zFL.Text = this.K1zFL;

            r1.M1xFL.Text = this.M1xFL;
            r1.M1yFL.Text = this.M1yFL;
            r1.M1zFL.Text = this.M1zFL;

            r1.N1xFL.Text = this.N1xFL;
            r1.N1yFL.Text = this.N1yFL;
            r1.N1zFL.Text = this.N1zFL;

            r1.O1xFL.Text = this.O1xFL;
            r1.O1yFL.Text = this.O1yFL;
            r1.O1zFL.Text = this.O1zFL;

            r1.P1xFL.Text = this.P1xFL;
            r1.P1yFL.Text = this.P1yFL;
            r1.P1zFL.Text = this.P1zFL;

            r1.Q1xFL.Text = this.Q1xFL;
            r1.Q1yFL.Text = this.Q1yFL;
            r1.Q1zFL.Text = this.Q1zFL;

            r1.R1xFL.Text = this.R1xFL;
            r1.R1yFL.Text = this.R1yFL;
            r1.R1zFL.Text = this.R1zFL;

            r1.W1xFL.Text = this.W1xFL;
            r1.W1yFL.Text = this.W1yFL;
            r1.W1zFL.Text = this.W1zFL;

            r1.RideHeightRefFLx.Text = this.RideHeightRefFLx;
            r1.RideHeightRefFLy.Text = this.RideHeightRefFLy;
            r1.RideHeightRefFLz.Text = this.RideHeightRefFLz;
            #endregion

            #region Assigning the Parent form's FRONT RIGHT SUSPENSION COORDINATE Textboxes with the values with which the user had performed the save action
            r1.A1xFR.Text = this.A1xFR;
            r1.A1yFR.Text = this.A1yFR;
            r1.A1zFR.Text = this.A1zFR;

            r1.B1xFR.Text = this.B1xFR;
            r1.B1yFR.Text = this.B1yFR;
            r1.B1zFR.Text = this.B1zFR;

            r1.C1xFR.Text = this.C1xFR;
            r1.C1yFR.Text = this.C1yFR;
            r1.C1zFR.Text = this.C1zFR;

            r1.D1xFR.Text = this.D1xFR;
            r1.D1yFR.Text = this.D1yFR;
            r1.D1zFR.Text = this.D1zFR;

            r1.E1xFR.Text = this.E1xFR;
            r1.E1yFR.Text = this.E1yFR;
            r1.E1zFR.Text = this.E1zFR;

            r1.F1xFR.Text = this.F1xFR;
            r1.F1yFR.Text = this.F1yFR;
            r1.F1zFR.Text = this.F1zFR;

            r1.G1xFR.Text = this.G1xFR;
            r1.G1yFR.Text = this.G1yFR;
            r1.G1zFR.Text = this.G1zFR;

            r1.H1xFR.Text = this.H1xFR;
            r1.H1yFR.Text = this.H1yFR;
            r1.H1zFR.Text = this.H1zFR;

            r1.I1xFR.Text = this.I1xFR;
            r1.I1yFR.Text = this.I1yFR;
            r1.I1zFR.Text = this.I1zFR;

            r1.J1xFR.Text = this.J1xFR;
            r1.J1yFR.Text = this.J1yFR;
            r1.J1zFR.Text = this.J1zFR;

            r1.JO1xFR.Text = this.JO1xFR;
            r1.JO1yFR.Text = this.JO1yFR;
            r1.JO1zFR.Text = this.JO1zFR;

            r1.K1xFR.Text = this.K1xFR;
            r1.K1yFR.Text = this.K1yFR;
            r1.K1zFR.Text = this.K1zFR;

            r1.M1xFR.Text = this.M1xFR;
            r1.M1yFR.Text = this.M1yFR;
            r1.M1zFR.Text = this.M1zFR;

            r1.N1xFR.Text = this.N1xFR;
            r1.N1yFR.Text = this.N1yFR;
            r1.N1zFR.Text = this.N1zFR;

            r1.O1xFR.Text = this.O1xFR;
            r1.O1yFR.Text = this.O1yFR;
            r1.O1zFR.Text = this.O1zFR;

            r1.P1xFR.Text = this.P1xFR;
            r1.P1yFR.Text = this.P1yFR;
            r1.P1zFR.Text = this.P1zFR;

            r1.Q1xFR.Text = this.Q1xFR;
            r1.Q1yFR.Text = this.Q1yFR;
            r1.Q1zFR.Text = this.Q1zFR;

            r1.R1xFR.Text = this.R1xFR;
            r1.R1yFR.Text = this.R1yFR;
            r1.R1zFR.Text = this.R1zFR;

            r1.W1xFR.Text = this.W1xFR;
            r1.W1yFR.Text = this.W1yFR;
            r1.W1zFR.Text = this.W1zFR;

            r1.RideHeightRefFRx.Text = this.RideHeightRefFRx;
            r1.RideHeightRefFRy.Text = this.RideHeightRefFRy;
            r1.RideHeightRefFRz.Text = this.RideHeightRefFRz;
            #endregion

            #region Assigning the Parent form's REAR LEFT SUSPENSION COORDINATE Textboxes with the values with which the user had performed the save action
            r1.A1xRL.Text = this.A1xRL;
            r1.A1yRL.Text = this.A1yRL;
            r1.A1zRL.Text = this.A1zRL;

            r1.B1xRL.Text = this.B1xRL;
            r1.B1yRL.Text = this.B1yRL;
            r1.B1zRL.Text = this.B1zRL;

            r1.C1xRL.Text = this.C1xRL;
            r1.C1yRL.Text = this.C1yRL;
            r1.C1zRL.Text = this.C1zRL;

            r1.D1xRL.Text = this.D1xRL;
            r1.D1yRL.Text = this.D1yRL;
            r1.D1zRL.Text = this.D1zRL;

            r1.E1xRL.Text = this.E1xRL;
            r1.E1yRL.Text = this.E1yRL;
            r1.E1zRL.Text = this.E1zRL;

            r1.F1xRL.Text = this.F1xRL;
            r1.F1yRL.Text = this.F1yRL;
            r1.F1zRL.Text = this.F1zRL;

            r1.G1xRL.Text = this.G1xRL;
            r1.G1yRL.Text = this.G1yRL;
            r1.G1zRL.Text = this.G1zRL;

            r1.H1xRL.Text = this.H1xRL;
            r1.H1yRL.Text = this.H1yRL;
            r1.H1zRL.Text = this.H1zRL;

            r1.I1xRL.Text = this.I1xRL;
            r1.I1yRL.Text = this.I1yRL;
            r1.I1zRL.Text = this.I1zRL;

            r1.J1xRL.Text = this.J1xRL;
            r1.J1yRL.Text = this.J1yRL;
            r1.J1zRL.Text = this.J1zRL;

            r1.JO1xRL.Text = this.JO1xRL;
            r1.JO1yRL.Text = this.JO1yRL;
            r1.JO1zRL.Text = this.JO1zRL;

            r1.K1xRL.Text = this.K1xRL;
            r1.K1yRL.Text = this.K1yRL;
            r1.K1zRL.Text = this.K1zRL;

            r1.M1xRL.Text = this.M1xRL;
            r1.M1yRL.Text = this.M1yRL;
            r1.M1zRL.Text = this.M1zRL;

            r1.N1xRL.Text = this.N1xRL;
            r1.N1yRL.Text = this.N1yRL;
            r1.N1zRL.Text = this.N1zRL;

            r1.O1xRL.Text = this.O1xRL;
            r1.O1yRL.Text = this.O1yRL;
            r1.O1zRL.Text = this.O1zRL;

            r1.P1xRL.Text = this.P1xRL;
            r1.P1yRL.Text = this.P1yRL;
            r1.P1zRL.Text = this.P1zRL;

            r1.Q1xRL.Text = this.Q1xRL;
            r1.Q1yRL.Text = this.Q1yRL;
            r1.Q1zRL.Text = this.Q1zRL;

            r1.R1xRL.Text = this.R1xRL;
            r1.R1yRL.Text = this.R1yRL;
            r1.R1zRL.Text = this.R1zRL;

            r1.W1xRL.Text = this.W1xRL;
            r1.W1yRL.Text = this.W1yRL;
            r1.W1zRL.Text = this.W1zRL;

            r1.RideHeightRefRLx.Text = this.RideHeightRefRLx;
            r1.RideHeightRefRLy.Text = this.RideHeightRefRLy;
            r1.RideHeightRefRLz.Text = this.RideHeightRefRLz;
            #endregion

            #region Assigning the Parent form's REAR RIGHT SUSPENSION COORDINATE Textboxes with the values with which the user had performed the save action
            r1.A1xRR.Text = this.A1xRR;
            r1.A1yRR.Text = this.A1yRR;
            r1.A1zRR.Text = this.A1zRR;

            r1.B1xRR.Text = this.B1xRR;
            r1.B1yRR.Text = this.B1yRR;
            r1.B1zRR.Text = this.B1zRR;

            r1.C1xRR.Text = this.C1xRR;
            r1.C1yRR.Text = this.C1yRR;
            r1.C1zRR.Text = this.C1zRR;

            r1.D1xRR.Text = this.D1xRR;
            r1.D1yRR.Text = this.D1yRR;
            r1.D1zRR.Text = this.D1zRR;

            r1.E1xRR.Text = this.E1xRR;
            r1.E1yRR.Text = this.E1yRR;
            r1.E1zRR.Text = this.E1zRR;

            r1.F1xRR.Text = this.F1xRR;
            r1.F1yRR.Text = this.F1yRR;
            r1.F1zRR.Text = this.F1zRR;

            r1.G1xRR.Text = this.G1xRR;
            r1.G1yRR.Text = this.G1yRR;
            r1.G1zRR.Text = this.G1zRR;

            r1.H1xRR.Text = this.H1xRR;
            r1.H1yRR.Text = this.H1yRR;
            r1.H1zRR.Text = this.H1zRR;

            r1.I1xRR.Text = this.I1xRR;
            r1.I1yRR.Text = this.I1yRR;
            r1.I1zRR.Text = this.I1zRR;

            r1.J1xRR.Text = this.J1xRR;
            r1.J1yRR.Text = this.J1yRR;
            r1.J1zRR.Text = this.J1zRR;

            r1.JO1xRR.Text = this.JO1xRR;
            r1.JO1yRR.Text = this.JO1yRR;
            r1.JO1zRR.Text = this.JO1zRR;

            r1.K1xRR.Text = this.K1xRR;
            r1.K1yRR.Text = this.K1yRR;
            r1.K1zRR.Text = this.K1zRR;

            r1.M1xRR.Text = this.M1xRR;
            r1.M1yRR.Text = this.M1yRR;
            r1.M1zRR.Text = this.M1zRR;

            r1.N1xRR.Text = this.N1xRR;
            r1.N1yRR.Text = this.N1yRR;
            r1.N1zRR.Text = this.N1zRR;

            r1.O1xRR.Text = this.O1xRR;
            r1.O1yRR.Text = this.O1yRR;
            r1.O1zRR.Text = this.O1zRR;

            r1.P1xRR.Text = this.P1xRR;
            r1.P1yRR.Text = this.P1yRR;
            r1.P1zRR.Text = this.P1zRR;

            r1.Q1xRR.Text = this.Q1xRR;
            r1.Q1yRR.Text = this.Q1yRR;
            r1.Q1zRR.Text = this.Q1zRR;

            r1.R1xRR.Text = this.R1xRR;
            r1.R1yRR.Text = this.R1yRR;
            r1.R1zRR.Text = this.R1zRR;

            r1.W1xRR.Text = this.W1xRR;
            r1.W1yRR.Text = this.W1yRR;
            r1.W1zRR.Text = this.W1zRR;

            r1.RideHeightRefRRx.Text = this.RideHeightRefRRx;
            r1.RideHeightRefRRy.Text = this.RideHeightRefRRy;
            r1.RideHeightRefRRz.Text = this.RideHeightRefRRz;
            #endregion

            #region Assigning the Parent form's TIRE Textboxes with the values with which the user had performed the save action
            r1.TireRateFL.Text = TireRate;
            r1.TireWidthFL.Text = TireWidth;
            #endregion

            #region Assigning the Parent form's SPRING Textboxes with the values with which the user had performed the save action
            r1.SpringRateFL.Text = SpringRate;
            r1.SpringPreloadFL.Text = SpringPreload;
            r1.SpringFreeLengthFL.Text = SpringFreeLength;
            #endregion

            #region Assigning the Parent form's ARB Textboxes with the values with which the user had performed the save action
            r1.AntiRollBarRateFront.Text = AntiRollBarRate;
            #endregion

            #region Assigning the Parent form's DAMPER Textboxes with the values with which the user had performed the save action
            r1.DamperGasPressureFL.Text = DamperGasPressure;
            r1.DamperShaftDiaFL.Text = DamperShaftDia;
            #endregion

            #region Assigning the Parent form's CHASSIS Textboxes with the values with which the user had performed the save action
            r1.SuspendedMass.Text = SuspendedMass;

            r1.NonSuspendedMassFL.Text = NonSuspendedMassFL;
            r1.NonSuspendedMassFR.Text = NonSuspendedMassFR;
            r1.NonSuspendedMassRL.Text = NonSuspendedMassRL;
            r1.NonSuspendedMassRR.Text = NonSuspendedMassRR;

            r1.SMCGx.Text = SMCGx;
            r1.SMCGy.Text = SMCGy;
            r1.SMCGz.Text = SMCGz;

            r1.NSMCGFLx.Text = NSMCGFLx;
            r1.NSMCGFLy.Text = NSMCGFLy;
            r1.NSMCGFLz.Text = NSMCGFLz;

            r1.NSMCGFRx.Text = NSMCGFRx;
            r1.NSMCGFRy.Text = NSMCGFRy;
            r1.NSMCGFRz.Text = NSMCGFRz;

            r1.NSMCGRLx.Text = NSMCGRLx;
            r1.NSMCGRLy.Text = NSMCGRLy;
            r1.NSMCGRLz.Text = NSMCGRLz;

            r1.NSMCGRRx.Text = NSMCGRRx;
            r1.NSMCGRRy.Text = NSMCGRRy;
            r1.NSMCGRRz.Text = NSMCGRRz;
            #endregion

            #region Assigning the Parent form's WHEEL ALIGNMENT Textboxes with the values with which the user had performed the save action
            r1.StaticCamberFL.Text = StaticCamber;
            r1.StaticToeFL.Text = StaticToe;
            #endregion 

            //
            // Vehilce item Assignment shifted to a new method so that it can be called after the comboboxes have been populated
            //

            #endregion

            #region Restoring the Input Item Control Properties

            #region Restoring the FRONT LEFT Control Properties
            #region FRONT LEFT Accordion Control
            r1.accordionControlSuspensionCoordinatesFL.Visible = accordionControlSuspensionCoordinatesFL.Visible;

            r1.accordionControlFixedPointsFL.Expanded = accordionControlFixedPointFL.Expanded;
            r1.accordionControlMovingPointsFL.Expanded = accordionControlMovingPointsFL.Expanded;

            r1.accordionControlFixedPointsFLUpperFrontChassis.Visible = accordionControlFixedPointsFLUpperFrontChassis.Visible;
            r1.accordionControlFixedPointsFLUpperRearChassis.Visible = accordionControlFixedPointsFLUpperRearChassis.Visible;
            r1.accordionControlFixedPointsFLBellCrankPivot.Visible = accordionControlFixedPointsFLBellCrankPivot.Visible;
            r1.accordionControlMovingPointsFLPushRodBellCrank.Visible = accordionControlMovingPointsFLPushRodBellCrank.Visible;
            r1.accordionControlMovingPointsFLAntiRollBarBellCrank.Visible = accordionControlMovingPointsFLAntiRollBarBellCrank.Visible;
            r1.accordionControlMovingPointsFLUpperBallJoint.Visible = accordionControlMovingPointsFLUpperBallJoint.Visible;
            r1.accordionControlFixedPointsFLTorsionBarBottom.Visible = accordionControlFixedPointsFLTorsionBarBottom.Visible;
            r1.accordionControlMovingPointsFLPushRodUpright.Visible = accordionControlMovingPointsFLPushRodUpright.Visible;

            r1.accordionControlMovingPointsFLDamperBellCrank.Text = accordionControlMovingPointsFLDamperBellCrank.Text;
            #endregion
            #endregion

            #region Restoring the FRONT RIGHT Control Properties
            
            #region FRONT RIGHT Accordion Control
            r1.accordionControlSuspensionCoordinatesFR.Visible = accordionControlSuspensionCoordinatesFR.Visible;

            r1.accordionControlFixedPointsFR.Expanded = accordionControlFixedPointFR.Expanded;
            r1.accordionControlMovingPointsFR.Expanded = accordionControlMovingPointsFR.Expanded;

            r1.accordionControlFixedPointsFRUpperFrontChassis.Visible = accordionControlFixedPointsFRUpperFrontChassis.Visible;
            r1.accordionControlFixedPointsFRUpperRearChassis.Visible = accordionControlFixedPointsFRUpperRearChassis.Visible;
            r1.accordionControlFixedPointsFRBellCrankPivot.Visible = accordionControlFixedPointsFRBellCrankPivot.Visible;
            r1.accordionControlMovingPointsFRPushRodBellCrank.Visible = accordionControlMovingPointsFRPushRodBellCrank.Visible;
            r1.accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = accordionControlMovingPointsFRAntiRollBarBellCrank.Visible;
            r1.accordionControlMovingPointsFRUpperBallJoint.Visible = accordionControlMovingPointsFRUpperBallJoint.Visible;
            r1.accordionControlFixedPointsFRTorsionBarBottom.Visible = accordionControlFixedPointsFRTorsionBarBottom.Visible;
            r1.accordionControlMovingPointsFRPushRodUpright.Visible = accordionControlMovingPointsFRPushRodUpright.Visible;

            r1.accordionControlMovingPointsFRDamperBellCrank.Text = accordionControlMovingPointsFRDamperBellCrank.Text;
            #endregion

            #endregion

            #region Restoring the REAR LEFT Control Properties

            #region REAR LEFT Accordion Control
            r1.accordionControlSuspensionCoordinatesRL.Visible = accordionControlSuspensionCoordinatesRL.Visible;

            r1.accordionControlFixedPointsRL.Expanded = accordionControlFixedPointRL.Expanded;
            r1.accordionControlMovingPointsRL.Expanded = accordionControlMovingPointsRL.Expanded;

            r1.accordionControlFixedPointsRLUpperFrontChassis.Visible = accordionControlFixedPointsRLUpperFrontChassis.Visible;
            r1.accordionControlFixedPointsRLUpperRearChassis.Visible = accordionControlFixedPointsRLUpperRearChassis.Visible;
            r1.accordionControlFixedPointsRLBellCrankPivot.Visible = accordionControlFixedPointsRLBellCrankPivot.Visible;
            r1.accordionControlMovingPointsRLPushRodBellCrank.Visible = accordionControlMovingPointsRLPushRodBellCrank.Visible;
            r1.accordionControlMovingPointsRLAntiRollBarBellCrank.Visible = accordionControlMovingPointsRLAntiRollBarBellCrank.Visible;
            r1.accordionControlMovingPointsRLUpperBallJoint.Visible = accordionControlMovingPointsRLUpperBallJoint.Visible;
            r1.accordionControlFixedPointsRLTorsionBarBottom.Visible = accordionControlFixedPointsRLTorsionBarBottom.Visible;
            r1.accordionControlMovingPointsRLPushRodUpright.Visible = accordionControlMovingPointsRLPushRodUpright.Visible;

            r1.accordionControlMovingPointsRLDamperBellCrank.Text = accordionControlMovingPointsRLDamperBellCrank.Text;
            #endregion

            #endregion

            #region Restoring the REAR RIGHT Control Properties

            #region REAR RIGHT Accordion Control
            r1.accordionControlSuspensionCoordinatesRR.Visible = accordionControlSuspensionCoordinatesRR.Visible;

            r1.accordionControlFixedPointsRR.Expanded = accordionControlFixedPointRR.Expanded;
            r1.accordionControlMovingPointsRR.Expanded = accordionControlMovingPointsRR.Expanded;

            r1.accordionControlFixedPointsRRUpperFrontChassis.Visible = accordionControlFixedPointsRRUpperFrontChassis.Visible;
            r1.accordionControlFixedPointsRRUpperRearChassis.Visible = accordionControlFixedPointsRRUpperRearChassis.Visible;
            r1.accordionControlFixedPointsRRBellCrankPivot.Visible = accordionControlFixedPointsRRBellCrankPivot.Visible;
            r1.accordionControlMovingPointsRRPushRodBellCrank.Visible = accordionControlMovingPointsRRPushRodBellCrank.Visible;
            r1.accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = accordionControlMovingPointsRRAntiRollBarBellCrank.Visible;
            r1.accordionControlMovingPointsRRUpperBallJoint.Visible = accordionControlMovingPointsRRUpperBallJoint.Visible;
            r1.accordionControlFixedPointsRRTorsionBarBottom.Visible = accordionControlFixedPointsRRTorsionBarBottom.Visible;
            r1.accordionControlMovingPointsRRPushRodUpright.Visible = accordionControlMovingPointsRRPushRodUpright.Visible;

            r1.accordionControlMovingPointsRRDamperBellCrank.Text = accordionControlMovingPointsRRDamperBellCrank.Text;
            #endregion

            #endregion

            #region Restoring the TIRE Control Properties
            r1.accordionControlTireStiffness.Visible = accordionControlTireStiffness.Visible;
            #endregion

            #region Restoring the SPRING Control Properties
            r1.accordionControlSprings.Visible = accordionControlSprings.Visible;
            #endregion

            #region Restoring the ARB Control Properties
            r1.accordionControlAntiRollBar.Visible = accordionControlAntiRollBar.Visible;
            #endregion

            #region Restoring the DAMPER Control Properties
            r1.accordionControlDamper.Visible = accordionControlDamper.Visible;
            #endregion

            #region Restoring the CHASSIS Control Properties
            r1.accordionControlChassis.Visible = accordionControlChassis.Visible;
            #endregion

            #region Restoring the WHEEL ALIGNMENT Control Properties
            r1.accordionControlWheelAlignment.Visible = accordionControlWheelAlignment.Visible;
            #endregion

            #region Restoring the VEHICLE Control Properties
            r1.accordionControlVehicleItem.Visible = accordionControlVehicleItem.Visible;
            #endregion 
            #endregion

            #region Restoring the Group Control Element's visibility
            r1.groupControl13.Visible = this.groupControl13.Visible;
            #endregion

            #region Restoring the Side Panel Element's visibility
            r1.sidePanel2.Visible = this.sidePanel2.Visible;
            #endregion

        }

        public void RestoreTabControl(Kinematics_Software_New r1)
        {
            #region Restoring the Tab Pane Control Properties
            Kinematics_Software_New.TabControl_Outputs.Visible = xtraTabControl.Visible;
            Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;

            #endregion

            #region Restoring the Ribbon's SelectedPage
            r1.Ribbon.SelectedPage = r1.Ribbon.Pages[selectedRibbonPage];
            #endregion

        }

        public void RestoreComboboxSelectedIndex(Kinematics_Software_New r1)
        {
            #region Assigning the Parent form's VEHICLE comboBoxes with the values with which the user had performed the save action
            r1.comboBoxSCFL.SelectedIndex = comboBoxSCFL_SelectedItemIndex;
            r1.comboBoxSCFR.SelectedIndex = comboBoxSCFR_SelectedItemIndex;
            r1.comboBoxSCRL.SelectedIndex = comboBoxSCRL_SelectedItemIndex;
            r1.comboBoxSCRR.SelectedIndex = comboBoxSCRR_SelectedItemIndex;

            r1.comboBoxTireFL.SelectedIndex = comboBoxTireFL_SelectedItem;
            r1.comboBoxTireFR.SelectedIndex = comboBoxTireFR_SelectedItem;
            r1.comboBoxTireRL.SelectedIndex = comboBoxTireRL_SelectedItem;
            r1.comboBoxTireRR.SelectedIndex = comboBoxTireRR_SelectedItem;

            r1.comboBoxSpringFL.SelectedIndex = comboBoxSpringFL_SelectedItem;
            r1.comboBoxSpringFR.SelectedIndex = comboBoxSpringFR_SelectedItem;
            r1.comboBoxSpringRL.SelectedIndex = comboBoxSpringRL_SelectedItem;
            r1.comboBoxSpringRR.SelectedIndex = comboBoxSpringRR_SelectedItem;

            r1.comboBoxDamperFL.SelectedIndex = comboBoxDamperFL_SelectedItem;
            r1.comboBoxDamperFR.SelectedIndex = comboBoxDamperFR_SelectedItem;
            r1.comboBoxDamperRL.SelectedIndex = comboBoxDamperRL_SelectedItem;
            r1.comboBoxDamperRR.SelectedIndex = comboBoxDamperRR_SelectedItem;

            r1.comboBoxARBFront.SelectedIndex = comboBoxARBFront_SelectedItem;
            r1.comboBoxARBRear.SelectedIndex = comboBoxARBRear_SelectedItem;

            r1.comboBoxChassis.SelectedIndex = comboBoxChassis_SelectedItem;

            r1.comboBoxWAFL.SelectedIndex = comboBoxWAFL_SelectedItem;
            r1.comboBoxWAFR.SelectedIndex = comboBoxWAFR_SelectedItem;
            r1.comboBoxWARL.SelectedIndex = comboBoxWARL_SelectedItem;
            r1.comboBoxWARR.SelectedIndex = comboBoxWARR_SelectedItem;

            for (int i_RestoreIndex  = 0; i_RestoreIndex < Simulation.List_Simulation.Count; i_RestoreIndex++)
            {
                Simulation.List_Simulation[i_RestoreIndex].simulationPanel.comboBoxVehicle.SelectedIndex = comboBoxVehicle_SelectedItem[i_RestoreIndex];
                Simulation.List_Simulation[i_RestoreIndex].simulationPanel.comboBoxMotion.SelectedIndex = combobocMotion_SelectedItem[i_RestoreIndex];
            }
            #endregion
        }

        public KinematicsSoftwareNewSerialization(SerializationInfo info, StreamingContext context)
        {
            #region De-serialization of the Input Item TextBoxes
            #region De-serialization of the FRONT LEFT SUSPENSION COORDINATES TextBoxes
            A1xFL = (string)info.GetValue("A1x_FL", typeof(string));
            A1yFL = (string)info.GetValue("A1y_FL", typeof(string));
            A1zFL = (string)info.GetValue("A1z_FL", typeof(string));

            B1xFL = (string)info.GetValue("B1x_FL", typeof(string));
            B1yFL = (string)info.GetValue("B1y_FL", typeof(string));
            B1zFL = (string)info.GetValue("B1z_FL", typeof(string));

            C1xFL = (string)info.GetValue("C1x_FL", typeof(string));
            C1yFL = (string)info.GetValue("C1y_FL", typeof(string));
            C1zFL = (string)info.GetValue("C1z_FL", typeof(string));

            D1xFL = (string)info.GetValue("D1x_FL", typeof(string));
            D1yFL = (string)info.GetValue("D1y_FL", typeof(string));
            D1zFL = (string)info.GetValue("D1z_FL", typeof(string));

            E1xFL = (string)info.GetValue("E1x_FL", typeof(string));
            E1yFL = (string)info.GetValue("E1y_FL", typeof(string));
            E1zFL = (string)info.GetValue("E1z_FL", typeof(string));

            F1xFL = (string)info.GetValue("F1x_FL", typeof(string));
            F1yFL = (string)info.GetValue("F1y_FL", typeof(string));
            F1zFL = (string)info.GetValue("F1z_FL", typeof(string));

            G1xFL = (string)info.GetValue("G1x_FL", typeof(string));
            G1yFL = (string)info.GetValue("G1y_FL", typeof(string));
            G1zFL = (string)info.GetValue("G1z_FL", typeof(string));

            H1xFL = (string)info.GetValue("H1x_FL", typeof(string));
            H1yFL = (string)info.GetValue("H1y_FL", typeof(string));
            H1zFL = (string)info.GetValue("H1z_FL", typeof(string));

            I1xFL = (string)info.GetValue("I1x_FL", typeof(string));
            I1yFL = (string)info.GetValue("I1y_FL", typeof(string));
            I1zFL = (string)info.GetValue("I1z_FL", typeof(string));

            J1xFL = (string)info.GetValue("J1x_FL", typeof(string));
            J1yFL = (string)info.GetValue("J1y_FL", typeof(string));
            J1zFL = (string)info.GetValue("J1z_FL", typeof(string));

            JO1xFL = (string)info.GetValue("JO1x_FL", typeof(string));
            JO1yFL = (string)info.GetValue("JO1y_FL", typeof(string));
            JO1zFL = (string)info.GetValue("JO1z_FL", typeof(string));

            K1xFL = (string)info.GetValue("K1x_FL", typeof(string));
            K1yFL = (string)info.GetValue("K1y_FL", typeof(string));
            K1zFL = (string)info.GetValue("K1z_FL", typeof(string));

            M1xFL = (string)info.GetValue("M1x_FL", typeof(string));
            M1yFL = (string)info.GetValue("M1y_FL", typeof(string));
            M1zFL = (string)info.GetValue("M1z_FL", typeof(string));

            N1xFL = (string)info.GetValue("N1x_FL", typeof(string));
            N1yFL = (string)info.GetValue("N1y_FL", typeof(string));
            N1zFL = (string)info.GetValue("N1z_FL", typeof(string));

            O1xFL = (string)info.GetValue("O1x_FL", typeof(string));
            O1yFL = (string)info.GetValue("O1y_FL", typeof(string));
            O1zFL = (string)info.GetValue("O1z_FL", typeof(string));

            P1xFL = (string)info.GetValue("P1x_FL", typeof(string));
            P1yFL = (string)info.GetValue("P1y_FL", typeof(string));
            P1zFL = (string)info.GetValue("P1z_FL", typeof(string));

            Q1xFL = (string)info.GetValue("Q1x_FL", typeof(string));
            Q1yFL = (string)info.GetValue("Q1y_FL", typeof(string));
            Q1zFL = (string)info.GetValue("Q1z_FL", typeof(string));

            R1xFL = (string)info.GetValue("R1x_FL", typeof(string));
            R1yFL = (string)info.GetValue("R1y_FL", typeof(string));
            R1zFL = (string)info.GetValue("R1z_FL", typeof(string));

            W1xFL = (string)info.GetValue("W1x_FL", typeof(string));
            W1yFL = (string)info.GetValue("W1y_FL", typeof(string));
            W1zFL = (string)info.GetValue("W1z_FL", typeof(string));

            RideHeightRefFLx = (string)info.GetValue("RideHeightRefx_FL", typeof(string));
            RideHeightRefFLy = (string)info.GetValue("RideHeightRefy_FL", typeof(string));
            RideHeightRefFLz = (string)info.GetValue("RideHeightRefz_FL", typeof(string));
            #endregion

            #region De-serialization of the FRONT RIGHT SUSPENSION COORDINATES TextBoxes
            A1xFR = (string)info.GetValue("A1x_FR", typeof(string));
            A1yFR = (string)info.GetValue("A1y_FR", typeof(string));
            A1zFR = (string)info.GetValue("A1z_FR", typeof(string));

            B1xFR = (string)info.GetValue("B1x_FR", typeof(string));
            B1yFR = (string)info.GetValue("B1y_FR", typeof(string));
            B1zFR = (string)info.GetValue("B1z_FR", typeof(string));

            C1xFR = (string)info.GetValue("C1x_FR", typeof(string));
            C1yFR = (string)info.GetValue("C1y_FR", typeof(string));
            C1zFR = (string)info.GetValue("C1z_FR", typeof(string));

            D1xFR = (string)info.GetValue("D1x_FR", typeof(string));
            D1yFR = (string)info.GetValue("D1y_FR", typeof(string));
            D1zFR = (string)info.GetValue("D1z_FR", typeof(string));

            E1xFR = (string)info.GetValue("E1x_FR", typeof(string));
            E1yFR = (string)info.GetValue("E1y_FR", typeof(string));
            E1zFR = (string)info.GetValue("E1z_FR", typeof(string));

            F1xFR = (string)info.GetValue("F1x_FR", typeof(string));
            F1yFR = (string)info.GetValue("F1y_FR", typeof(string));
            F1zFR = (string)info.GetValue("F1z_FR", typeof(string));

            G1xFR = (string)info.GetValue("G1x_FR", typeof(string));
            G1yFR = (string)info.GetValue("G1y_FR", typeof(string));
            G1zFR = (string)info.GetValue("G1z_FR", typeof(string));

            H1xFR = (string)info.GetValue("H1x_FR", typeof(string));
            H1yFR = (string)info.GetValue("H1y_FR", typeof(string));
            H1zFR = (string)info.GetValue("H1z_FR", typeof(string));

            I1xFR = (string)info.GetValue("I1x_FR", typeof(string));
            I1yFR = (string)info.GetValue("I1y_FR", typeof(string));
            I1zFR = (string)info.GetValue("I1z_FR", typeof(string));

            J1xFR = (string)info.GetValue("J1x_FR", typeof(string));
            J1yFR = (string)info.GetValue("J1y_FR", typeof(string));
            J1zFR = (string)info.GetValue("J1z_FR", typeof(string));

            JO1xFR = (string)info.GetValue("JO1x_FR", typeof(string));
            JO1yFR = (string)info.GetValue("JO1y_FR", typeof(string));
            JO1zFR = (string)info.GetValue("JO1z_FR", typeof(string));

            K1xFR = (string)info.GetValue("K1x_FR", typeof(string));
            K1yFR = (string)info.GetValue("K1y_FR", typeof(string));
            K1zFR = (string)info.GetValue("K1z_FR", typeof(string));

            M1xFR = (string)info.GetValue("M1x_FR", typeof(string));
            M1yFR = (string)info.GetValue("M1y_FR", typeof(string));
            M1zFR = (string)info.GetValue("M1z_FR", typeof(string));

            N1xFR = (string)info.GetValue("N1x_FR", typeof(string));
            N1yFR = (string)info.GetValue("N1y_FR", typeof(string));
            N1zFR = (string)info.GetValue("N1z_FR", typeof(string));

            O1xFR = (string)info.GetValue("O1x_FR", typeof(string));
            O1yFR = (string)info.GetValue("O1y_FR", typeof(string));
            O1zFR = (string)info.GetValue("O1z_FR", typeof(string));

            P1xFR = (string)info.GetValue("P1x_FR", typeof(string));
            P1yFR = (string)info.GetValue("P1y_FR", typeof(string));
            P1zFR = (string)info.GetValue("P1z_FR", typeof(string));

            Q1xFR = (string)info.GetValue("Q1x_FR", typeof(string));
            Q1yFR = (string)info.GetValue("Q1y_FR", typeof(string));
            Q1zFR = (string)info.GetValue("Q1z_FR", typeof(string));

            R1xFR = (string)info.GetValue("R1x_FR", typeof(string));
            R1yFR = (string)info.GetValue("R1y_FR", typeof(string));
            R1zFR = (string)info.GetValue("R1z_FR", typeof(string));

            W1xFR = (string)info.GetValue("W1x_FR", typeof(string));
            W1yFR = (string)info.GetValue("W1y_FR", typeof(string));
            W1zFR = (string)info.GetValue("W1z_FR", typeof(string));

            RideHeightRefFRx = (string)info.GetValue("RideHeightRefx_FR", typeof(string));
            RideHeightRefFRy = (string)info.GetValue("RideHeightRefy_FR", typeof(string));
            RideHeightRefFRz = (string)info.GetValue("RideHeightRefz_FR", typeof(string));
            #endregion

            #region De-serialization of the REAR LEFT SUSPENSION COORDINATES TextBoxes
            A1xRL = (string)info.GetValue("A1x_RL", typeof(string));
            A1yRL = (string)info.GetValue("A1y_RL", typeof(string));
            A1zRL = (string)info.GetValue("A1z_RL", typeof(string));

            B1xRL = (string)info.GetValue("B1x_RL", typeof(string));
            B1yRL = (string)info.GetValue("B1y_RL", typeof(string));
            B1zRL = (string)info.GetValue("B1z_RL", typeof(string));

            C1xRL = (string)info.GetValue("C1x_RL", typeof(string));
            C1yRL = (string)info.GetValue("C1y_RL", typeof(string));
            C1zRL = (string)info.GetValue("C1z_RL", typeof(string));

            D1xRL = (string)info.GetValue("D1x_RL", typeof(string));
            D1yRL = (string)info.GetValue("D1y_RL", typeof(string));
            D1zRL = (string)info.GetValue("D1z_RL", typeof(string));

            E1xRL = (string)info.GetValue("E1x_RL", typeof(string));
            E1yRL = (string)info.GetValue("E1y_RL", typeof(string));
            E1zRL = (string)info.GetValue("E1z_RL", typeof(string));

            F1xRL = (string)info.GetValue("F1x_RL", typeof(string));
            F1yRL = (string)info.GetValue("F1y_RL", typeof(string));
            F1zRL = (string)info.GetValue("F1z_RL", typeof(string));

            G1xRL = (string)info.GetValue("G1x_RL", typeof(string));
            G1yRL = (string)info.GetValue("G1y_RL", typeof(string));
            G1zRL = (string)info.GetValue("G1z_RL", typeof(string));

            H1xRL = (string)info.GetValue("H1x_RL", typeof(string));
            H1yRL = (string)info.GetValue("H1y_RL", typeof(string));
            H1zRL = (string)info.GetValue("H1z_RL", typeof(string));

            I1xRL = (string)info.GetValue("I1x_RL", typeof(string));
            I1yRL = (string)info.GetValue("I1y_RL", typeof(string));
            I1zRL = (string)info.GetValue("I1z_RL", typeof(string));

            J1xRL = (string)info.GetValue("J1x_RL", typeof(string));
            J1yRL = (string)info.GetValue("J1y_RL", typeof(string));
            J1zRL = (string)info.GetValue("J1z_RL", typeof(string));

            JO1xRL = (string)info.GetValue("JO1x_RL", typeof(string));
            JO1yRL = (string)info.GetValue("JO1y_RL", typeof(string));
            JO1zRL = (string)info.GetValue("JO1z_RL", typeof(string));

            K1xRL = (string)info.GetValue("K1x_RL", typeof(string));
            K1yRL = (string)info.GetValue("K1y_RL", typeof(string));
            K1zRL = (string)info.GetValue("K1z_RL", typeof(string));

            M1xRL = (string)info.GetValue("M1x_RL", typeof(string));
            M1yRL = (string)info.GetValue("M1y_RL", typeof(string));
            M1zRL = (string)info.GetValue("M1z_RL", typeof(string));

            N1xRL = (string)info.GetValue("N1x_RL", typeof(string));
            N1yRL = (string)info.GetValue("N1y_RL", typeof(string));
            N1zRL = (string)info.GetValue("N1z_RL", typeof(string));

            O1xRL = (string)info.GetValue("O1x_RL", typeof(string));
            O1yRL = (string)info.GetValue("O1y_RL", typeof(string));
            O1zRL = (string)info.GetValue("O1z_RL", typeof(string));

            P1xRL = (string)info.GetValue("P1x_RL", typeof(string));
            P1yRL = (string)info.GetValue("P1y_RL", typeof(string));
            P1zRL = (string)info.GetValue("P1z_RL", typeof(string));

            Q1xRL = (string)info.GetValue("Q1x_RL", typeof(string));
            Q1yRL = (string)info.GetValue("Q1y_RL", typeof(string));
            Q1zRL = (string)info.GetValue("Q1z_RL", typeof(string));

            R1xRL = (string)info.GetValue("R1x_RL", typeof(string));
            R1yRL = (string)info.GetValue("R1y_RL", typeof(string));
            R1zRL = (string)info.GetValue("R1z_RL", typeof(string));

            W1xRL = (string)info.GetValue("W1x_RL", typeof(string));
            W1yRL = (string)info.GetValue("W1y_RL", typeof(string));
            W1zRL = (string)info.GetValue("W1z_RL", typeof(string));

            RideHeightRefRLx = (string)info.GetValue("RideHeightRefx_RL", typeof(string));
            RideHeightRefRLy = (string)info.GetValue("RideHeightRefy_RL", typeof(string));
            RideHeightRefRLz = (string)info.GetValue("RideHeightRefz_RL", typeof(string));
            #endregion

            #region De-serialization of the REAR RIGHT SUSPENSION COORDINATES TextBoxes
            A1xRR = (string)info.GetValue("A1x_RR", typeof(string));
            A1yRR = (string)info.GetValue("A1y_RR", typeof(string));
            A1zRR = (string)info.GetValue("A1z_RR", typeof(string));

            B1xRR = (string)info.GetValue("B1x_RR", typeof(string));
            B1yRR = (string)info.GetValue("B1y_RR", typeof(string));
            B1zRR = (string)info.GetValue("B1z_RR", typeof(string));

            C1xRR = (string)info.GetValue("C1x_RR", typeof(string));
            C1yRR = (string)info.GetValue("C1y_RR", typeof(string));
            C1zRR = (string)info.GetValue("C1z_RR", typeof(string));

            D1xRR = (string)info.GetValue("D1x_RR", typeof(string));
            D1yRR = (string)info.GetValue("D1y_RR", typeof(string));
            D1zRR = (string)info.GetValue("D1z_RR", typeof(string));

            E1xRR = (string)info.GetValue("E1x_RR", typeof(string));
            E1yRR = (string)info.GetValue("E1y_RR", typeof(string));
            E1zRR = (string)info.GetValue("E1z_RR", typeof(string));

            F1xRR = (string)info.GetValue("F1x_RR", typeof(string));
            F1yRR = (string)info.GetValue("F1y_RR", typeof(string));
            F1zRR = (string)info.GetValue("F1z_RR", typeof(string));

            G1xRR = (string)info.GetValue("G1x_RR", typeof(string));
            G1yRR = (string)info.GetValue("G1y_RR", typeof(string));
            G1zRR = (string)info.GetValue("G1z_RR", typeof(string));

            H1xRR = (string)info.GetValue("H1x_RR", typeof(string));
            H1yRR = (string)info.GetValue("H1y_RR", typeof(string));
            H1zRR = (string)info.GetValue("H1z_RR", typeof(string));

            I1xRR = (string)info.GetValue("I1x_RR", typeof(string));
            I1yRR = (string)info.GetValue("I1y_RR", typeof(string));
            I1zRR = (string)info.GetValue("I1z_RR", typeof(string));

            J1xRR = (string)info.GetValue("J1x_RR", typeof(string));
            J1yRR = (string)info.GetValue("J1y_RR", typeof(string));
            J1zRR = (string)info.GetValue("J1z_RR", typeof(string));

            JO1xRR = (string)info.GetValue("JO1x_RR", typeof(string));
            JO1yRR = (string)info.GetValue("JO1y_RR", typeof(string));
            JO1zRR = (string)info.GetValue("JO1z_RR", typeof(string));

            K1xRR = (string)info.GetValue("K1x_RR", typeof(string));
            K1yRR = (string)info.GetValue("K1y_RR", typeof(string));
            K1zRR = (string)info.GetValue("K1z_RR", typeof(string));

            M1xRR = (string)info.GetValue("M1x_RR", typeof(string));
            M1yRR = (string)info.GetValue("M1y_RR", typeof(string));
            M1zRR = (string)info.GetValue("M1z_RR", typeof(string));

            N1xRR = (string)info.GetValue("N1x_RR", typeof(string));
            N1yRR = (string)info.GetValue("N1y_RR", typeof(string));
            N1zRR = (string)info.GetValue("N1z_RR", typeof(string));

            O1xRR = (string)info.GetValue("O1x_RR", typeof(string));
            O1yRR = (string)info.GetValue("O1y_RR", typeof(string));
            O1zRR = (string)info.GetValue("O1z_RR", typeof(string));

            P1xRR = (string)info.GetValue("P1x_RR", typeof(string));
            P1yRR = (string)info.GetValue("P1y_RR", typeof(string));
            P1zRR = (string)info.GetValue("P1z_RR", typeof(string));

            Q1xRR = (string)info.GetValue("Q1x_RR", typeof(string));
            Q1yRR = (string)info.GetValue("Q1y_RR", typeof(string));
            Q1zRR = (string)info.GetValue("Q1z_RR", typeof(string));

            R1xRR = (string)info.GetValue("R1x_RR", typeof(string));
            R1yRR = (string)info.GetValue("R1y_RR", typeof(string));
            R1zRR = (string)info.GetValue("R1z_RR", typeof(string));

            W1xRR = (string)info.GetValue("W1x_RR", typeof(string));
            W1yRR = (string)info.GetValue("W1y_RR", typeof(string));
            W1zRR = (string)info.GetValue("W1z_RR", typeof(string));

            RideHeightRefRRx = (string)info.GetValue("RideHeightRefx_RR", typeof(string));
            RideHeightRefRRy = (string)info.GetValue("RideHeightRefy_RR", typeof(string));
            RideHeightRefRRz = (string)info.GetValue("RideHeightRefz_RR", typeof(string));
            #endregion

            #region De-serialization of the TIRE TextBoxes
            TireRate = (string)info.GetValue("TireRate", typeof(string));
            TireWidth = (string)info.GetValue("TireWidth", typeof(string));
            #endregion

            #region De-serialization of the SPRING TextBoxes
            SpringRate = (string)info.GetValue("SpringRate", typeof(string));
            SpringPreload = (string)info.GetValue("SpringPreload", typeof(string));
            SpringFreeLength = (string)info.GetValue("SpringFreeLength", typeof(string));
            #endregion

            #region De-serialization of the ARB TextBoxes
            AntiRollBarRate = (string)info.GetValue("AntiRollBarRate", typeof(string));
            #endregion

            #region De-serialization of the DAMPER TextBoxes
            DamperGasPressure = (string)info.GetValue("DamperGasPressure", typeof(string));
            DamperShaftDia = (string)info.GetValue("DamperShaftDia", typeof(string));
            #endregion

            #region De-serialization of the CHASSIS TextBoxes
            SuspendedMass = (string)info.GetValue("Suspended_Mass", typeof(string));

            NonSuspendedMassFL = (string)info.GetValue("NonSuspended_Mass_FL", typeof(string));
            NonSuspendedMassFR = (string)info.GetValue("NonSuspended_Mass_FR", typeof(string));
            NonSuspendedMassRL = (string)info.GetValue("NonSuspended_Mass_RL", typeof(string));
            NonSuspendedMassRR = (string)info.GetValue("NonSuspended_Mass_RR", typeof(string));

            SMCGx = (string)info.GetValue("SuspendedMass_COG_x", typeof(string));
            SMCGy = (string)info.GetValue("SuspendedMass_COG_y", typeof(string));
            SMCGz = (string)info.GetValue("SuspendedMass_COG_z", typeof(string));

            NSMCGFLx = (string)info.GetValue("NonSuspendedMass_COG_FL_x", typeof(string));
            NSMCGFLy = (string)info.GetValue("NonSuspendedMass_COG_FL_y", typeof(string));
            NSMCGFLz = (string)info.GetValue("NonSuspendedMass_COG_FL_z", typeof(string));

            NSMCGFRx = (string)info.GetValue("NonSuspendedMass_COG_FR_x", typeof(string));
            NSMCGFRy = (string)info.GetValue("NonSuspendedMass_COG_FR_y", typeof(string));
            NSMCGFRz = (string)info.GetValue("NonSuspendedMass_COG_FR_z", typeof(string));

            NSMCGRLx = (string)info.GetValue("NonSuspendedMass_COG_RL_x", typeof(string));
            NSMCGRLy = (string)info.GetValue("NonSuspendedMass_COG_RL_y", typeof(string));
            NSMCGRLz = (string)info.GetValue("NonSuspendedMass_COG_RL_z", typeof(string));

            NSMCGRRx = (string)info.GetValue("NonSuspendedMass_COG_RR_x", typeof(string));
            NSMCGRRy = (string)info.GetValue("NonSuspendedMass_COG_RR_y", typeof(string));
            NSMCGRRz = (string)info.GetValue("NonSuspendedMass_COG_RR_z", typeof(string));
            #endregion

            #region De-serialization of the WHEEL ALIGNMENT TextBoxes
            StaticCamber = (string)info.GetValue("StaticCamber", typeof(string));
            StaticToe = (string)info.GetValue("StaticToe", typeof(string));
            #endregion

            #region De-serialization of the VEHICLE comboBoxes
            comboBoxSCFL_SelectedItemIndex = (int)info.GetValue("comboBoxSCFL_SelectedItem", typeof(int));
            comboBoxSCFR_SelectedItemIndex = (int)info.GetValue("comboBoxSCFR_SelectedItem", typeof(int));
            comboBoxSCRL_SelectedItemIndex = (int)info.GetValue("comboBoxSCRL_SelectedItem", typeof(int));
            comboBoxSCRR_SelectedItemIndex = (int)info.GetValue("comboBoxSCRR_SelectedItem", typeof(int));

            comboBoxTireFL_SelectedItem = (int)info.GetValue("comboBoxTireFL_SelectedItem", typeof(int));
            comboBoxTireFR_SelectedItem = (int)info.GetValue("comboBoxTireFR_SelectedItem", typeof(int));
            comboBoxTireRL_SelectedItem = (int)info.GetValue("comboBoxTireRL_SelectedItem", typeof(int));
            comboBoxTireRR_SelectedItem = (int)info.GetValue("comboBoxTireRR_SelectedItem", typeof(int));

            comboBoxSpringFL_SelectedItem = (int)info.GetValue("comboBoxSpringFL_SelectedItem", typeof(int));
            comboBoxSpringFR_SelectedItem = (int)info.GetValue("comboBoxSpringFR_SelectedItem", typeof(int));
            comboBoxSpringRL_SelectedItem = (int)(int)info.GetValue("comboBoxSpringRL_SelectedItem", typeof(int));
            comboBoxSpringRR_SelectedItem = (int)info.GetValue("comboBoxSpringRR_SelectedItem", typeof(int));

            comboBoxDamperFL_SelectedItem = (int)info.GetValue("comboboxDamperFL_SelectedItem", typeof(int));
            comboBoxDamperFR_SelectedItem = (int)info.GetValue("comboBoxDamperFR_SelectedItem", typeof(int));
            comboBoxDamperRL_SelectedItem = (int)info.GetValue("comboBoxDamperRL_SelectedItem", typeof(int));
            comboBoxDamperRR_SelectedItem = (int)info.GetValue("comboBoxDamperRR_SelectedItem", typeof(int));

            comboBoxARBFront_SelectedItem = (int)info.GetValue("comboBoxARBFront_SelectedItem", typeof(int));
            comboBoxARBRear_SelectedItem = (int)info.GetValue("comboBoxARBRear_SelectedItem", typeof(int));

            comboBoxChassis_SelectedItem = (int)info.GetValue("comboBoxChassis_SelectedItem", typeof(int));

            comboBoxWAFL_SelectedItem = (int)info.GetValue("comboBoxWAFL_SelectedItem", typeof(int));
            comboBoxWAFR_SelectedItem = (int)info.GetValue("comboBoxWAFR_SelectedItem", typeof(int));
            comboBoxWARL_SelectedItem = (int)info.GetValue("comboBoxWARL_SelectedItem", typeof(int));
            comboBoxWARR_SelectedItem = (int)info.GetValue("comboBoxWARR_SelectedItem", typeof(int));

            comboBoxVehicle_SelectedItem = (List<int>)info.GetValue("comboBoxVehicle_SelectedItem", typeof(List<int>));
            combobocMotion_SelectedItem = (List<int>)info.GetValue("comboBoxMotion_SelectedItem", typeof(List<int>));
            #endregion

            #endregion

            #region De-serialization of the Input Item Control

            #region De-serialization of the FRONT LEFT Control

            #region FRONT LEFT Navigation Page
            navigationPagePushRodFL = (SerNavigationPage)info.GetValue("NavigationPageFL_Caption", typeof(SerNavigationPage));
            #endregion

            #region FRONT LEFT Accordion Control
            accordionControlSuspensionCoordinatesFL = (SerAccordionControl)info.GetValue("AccordionControl_SuspensionCoordinatesFL_Visibility", typeof(SerAccordionControl));

            accordionControlFixedPointFL = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointFL_Expanded", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFL = (SerAccordionControlElement)info.GetValue("AccordionControlElement_MovingPointFL_Expanded", typeof(SerAccordionControlElement));

            accordionControlFixedPointsFLUpperFrontChassis = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointFLUpperFrontChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsFLUpperRearChassis = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsFLUpperRearChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsFLBellCrankPivot = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsFLBellCrankPivot_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFLPushRodBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFLPushRodBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFLAntiRollBarBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFLAntiRollBarBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFLUpperBallJoint = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFLUpperBallJoint_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsFLTorsionBarBottom = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsFLTorsionBarBottom_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFLPushRodUpright = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFLPushRodUpright_Visibility", typeof(SerAccordionControlElement));

            accordionControlMovingPointsFLDamperBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFLDamperBellCrank_Text", typeof(SerAccordionControlElement));
            #endregion

            #endregion

            #region De-serialization of the FRONT RIGHT Control

            #region FRONT RIGHT Navigation Page
            navigationPagePushRodFR = (SerNavigationPage)info.GetValue("NavigationPageFR_Caption", typeof(SerNavigationPage));
            #endregion

            #region FRONT RIGHT Accordion Control
            accordionControlSuspensionCoordinatesFR = (SerAccordionControl)info.GetValue("AccordionControl_SuspensionCoordinatesFR_Visibility", typeof(SerAccordionControl));

            accordionControlFixedPointFR = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointFR_Expanded", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFR = (SerAccordionControlElement)info.GetValue("AccordionControlElement_MovingPointFR_Expanded", typeof(SerAccordionControlElement));

            accordionControlFixedPointsFRUpperFrontChassis = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointFRUpperFrontChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsFRUpperRearChassis = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsFRUpperRearChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsFRBellCrankPivot = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsFRBellCrankPivot_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFRPushRodBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFRPushRodBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFRAntiRollBarBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFRAntiRollBarBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFRUpperBallJoint = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFRUpperBallJoint_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsFRTorsionBarBottom = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsFRTorsionBarBottom_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsFRPushRodUpright = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFRPushRodUpright_Visibility", typeof(SerAccordionControlElement));

            accordionControlMovingPointsFRDamperBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsFRDamperBellCrank_Text", typeof(SerAccordionControlElement));
            #endregion

            #endregion

            #region De-serialization of the REAR LEFT Control

            #region REAR LEFT Navigation Page
            navigationPagePushRodRL = (SerNavigationPage)info.GetValue("NavigationPageRL_Caption", typeof(SerNavigationPage));
            #endregion

            #region REAR LEFT Accordion Control
            accordionControlSuspensionCoordinatesRL = (SerAccordionControl)info.GetValue("AccordionControl_SuspensionCoordinatesRL_Visibility", typeof(SerAccordionControl));

            accordionControlFixedPointRL = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointRL_Expanded", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRL = (SerAccordionControlElement)info.GetValue("AccordionControlElement_MovingPointRL_Expanded", typeof(SerAccordionControlElement));

            accordionControlFixedPointsRLUpperFrontChassis = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointRLUpperFrontChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsRLUpperRearChassis = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsRLUpperRearChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsRLBellCrankPivot = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsRLBellCrankPivot_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRLPushRodBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRLPushRodBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRLAntiRollBarBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRLAntiRollBarBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRLUpperBallJoint = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRLUpperBallJoint_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsRLTorsionBarBottom = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsRLTorsionBarBottom_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRLPushRodUpright = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRLPushRodUpright_Visibility", typeof(SerAccordionControlElement));

            accordionControlMovingPointsRLDamperBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRLDamperBellCrank_Text", typeof(SerAccordionControlElement));
            #endregion

            #endregion

            #region De-serialization of the REAR RIGHT Control

            #region REAR RIGHT Navigation Page
            navigationPagePushRodRR = (SerNavigationPage)info.GetValue("NavigationPageRR_Caption", typeof(SerNavigationPage));
            #endregion

            #region REAR RIGHT Accordion Control
            accordionControlSuspensionCoordinatesRR = (SerAccordionControl)info.GetValue("AccordionControl_SuspensionCoordinatesRR_Visibility", typeof(SerAccordionControl));

            accordionControlFixedPointRR = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointRR_Expanded", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRR = (SerAccordionControlElement)info.GetValue("AccordionControlElement_MovingPointRR_Expanded", typeof(SerAccordionControlElement));

            accordionControlFixedPointsRRUpperFrontChassis = (SerAccordionControlElement)info.GetValue("AccordionControlElement_FixedPointRRUpperFrontChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsRRUpperRearChassis = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsRRUpperRearChassis_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsRRBellCrankPivot = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsRRBellCrankPivot_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRRPushRodBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRRPushRodBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRRAntiRollBarBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRRAntiRollBarBellCrank_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRRUpperBallJoint = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRRUpperBallJoint_Visibility", typeof(SerAccordionControlElement));
            accordionControlFixedPointsRRTorsionBarBottom = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_FixedPointsRRTorsionBarBottom_Visibility", typeof(SerAccordionControlElement));
            accordionControlMovingPointsRRPushRodUpright = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRRPushRodUpright_Visibility", typeof(SerAccordionControlElement));

            accordionControlMovingPointsRRDamperBellCrank = (SerAccordionControlElement)info.GetValue("AccordiionControlElement_MovingPointsRRDamperBellCrank_Text", typeof(SerAccordionControlElement));
            #endregion

            #endregion

            #region De-serialization of the TIRE Control
            accordionControlTireStiffness = (SerAccordionControl)info.GetValue("AccordionControl_TireStiffness_Visibility", typeof(SerAccordionControl));
            #endregion

            #region De-serialization of the SPRING Control
            accordionControlSprings = (SerAccordionControl)info.GetValue("AccordionControl_Springs_Visibility", typeof(SerAccordionControl));
            #endregion

            #region De-serialization of the ARB Control
            accordionControlAntiRollBar = (SerAccordionControl)info.GetValue("AccordionControl_AntiRrollBar_Visibility", typeof(SerAccordionControl));
            #endregion

            #region De-serialization of the Damper Control
            accordionControlDamper = (SerAccordionControl)info.GetValue("AccordionControl_Damper_Visibility", typeof(SerAccordionControl));
            #endregion

            #region De-serialization of the CHASSIS Control
            accordionControlChassis = (SerAccordionControl)info.GetValue("AccordionControl_Chassis_Visibility", typeof(SerAccordionControl));
            #endregion

            #region De-serialization of the WHEEL ALIGNMENT Control
            accordionControlWheelAlignment = (SerAccordionControl)info.GetValue("AccordionControl_WheelAlignment_Visibility", typeof(SerAccordionControl));
            #endregion

            #region De-serialization of the VEHICLE Control
            accordionControlVehicleItem = (SerAccordionControl)info.GetValue("AccordionControl_Vehicle_Visibility", typeof(SerAccordionControl));
            #endregion
            #endregion

            #region De-serialization of the Group Control
            groupControl13 = (SerGroupControl)info.GetValue("groupControl_Visibility", typeof(SerGroupControl));
            #endregion

            #region De-serialization of the Side Panel
            sidePanel2 = (SerSidePanel)info.GetValue("sidePanel2_Visibility", typeof(SerSidePanel));
            #endregion

            #region De-serialization of the Tab Pane Control
            xtraTabControl = (CustomXtraTabControl)info.GetValue("XtraTabControl", typeof(CustomXtraTabControl));
            SelectedPage = (int)info.GetValue("SelectedTabPage", typeof(int));

            //tabPaneResults = (SerTabPane)info.GetValue("TabPaneResults", typeof(SerTabPane));
            //SelectedPage = (int)info.GetValue("SelectedPage", typeof(int));
            #endregion

            #region De-serialization of the Ribbon Selected Page Index
            selectedRibbonPage = (int)info.GetValue("selectedRibbonPage", typeof(int));
            #endregion

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

            #region Serialization of the Input Item TextBoxes

            #region Serialization of the FRONT LEFT SUSPENSION COORDINATE TextBoxes
            info.AddValue("A1x_FL", A1xFL);
            info.AddValue("A1y_FL", A1yFL);
            info.AddValue("A1z_FL", A1zFL);

            info.AddValue("B1x_FL", B1xFL);
            info.AddValue("B1y_FL", B1yFL);
            info.AddValue("B1z_FL", B1zFL);

            info.AddValue("C1x_FL", C1xFL);
            info.AddValue("C1y_FL", C1yFL);
            info.AddValue("C1z_FL", C1zFL);

            info.AddValue("D1x_FL", D1xFL);
            info.AddValue("D1y_FL", D1yFL);
            info.AddValue("D1z_FL", D1zFL);

            info.AddValue("E1x_FL", E1xFL);
            info.AddValue("E1y_FL", E1yFL);
            info.AddValue("E1z_FL", E1zFL);

            info.AddValue("F1x_FL", F1xFL);
            info.AddValue("F1y_FL", F1yFL);
            info.AddValue("F1z_FL", F1zFL);

            info.AddValue("G1x_FL", G1xFL);
            info.AddValue("G1y_FL", G1yFL);
            info.AddValue("G1z_FL", G1zFL);

            info.AddValue("H1x_FL", H1xFL);
            info.AddValue("H1y_FL", H1yFL);
            info.AddValue("H1z_FL", H1zFL);

            info.AddValue("I1x_FL", I1xFL);
            info.AddValue("I1y_FL", I1yFL);
            info.AddValue("I1z_FL", I1zFL);

            info.AddValue("J1x_FL", J1xFL);
            info.AddValue("J1y_FL", J1yFL);
            info.AddValue("J1z_FL", J1zFL);

            info.AddValue("JO1x_FL", JO1xFL);
            info.AddValue("JO1y_FL", JO1yFL);
            info.AddValue("JO1z_FL", JO1zFL);

            info.AddValue("K1x_FL", K1xFL);
            info.AddValue("K1y_FL", K1yFL);
            info.AddValue("K1z_FL", K1zFL);

            info.AddValue("M1x_FL", M1xFL);
            info.AddValue("M1y_FL", M1yFL);
            info.AddValue("M1z_FL", M1zFL);

            info.AddValue("N1x_FL", N1xFL);
            info.AddValue("N1y_FL", N1yFL);
            info.AddValue("N1z_FL", N1zFL);

            info.AddValue("O1x_FL", O1xFL);
            info.AddValue("O1y_FL", O1yFL);
            info.AddValue("O1z_FL", O1zFL);

            info.AddValue("P1x_FL", P1xFL);
            info.AddValue("P1y_FL", P1yFL);
            info.AddValue("P1z_FL", P1zFL);

            info.AddValue("Q1x_FL", Q1xFL);
            info.AddValue("Q1y_FL", Q1yFL);
            info.AddValue("Q1z_FL", Q1zFL);

            info.AddValue("R1x_FL", R1xFL);
            info.AddValue("R1y_FL", R1yFL);
            info.AddValue("R1z_FL", R1zFL);

            info.AddValue("W1x_FL", W1xFL);
            info.AddValue("W1y_FL", W1yFL);
            info.AddValue("W1z_FL", W1zFL);

            info.AddValue("RideHeightRefx_FL", RideHeightRefFLx);
            info.AddValue("RideHeightRefy_FL", RideHeightRefFLy);
            info.AddValue("RideHeightRefz_FL", RideHeightRefFLz);

            #endregion

            #region Serialization of the FRONT RIGHT SUSPENSION COORDINATE TextBoxes
            info.AddValue("A1x_FR", A1xFR);
            info.AddValue("A1y_FR", A1yFR);
            info.AddValue("A1z_FR", A1zFR);

            info.AddValue("B1x_FR", B1xFR);
            info.AddValue("B1y_FR", B1yFR);
            info.AddValue("B1z_FR", B1zFR);

            info.AddValue("C1x_FR", C1xFR);
            info.AddValue("C1y_FR", C1yFR);
            info.AddValue("C1z_FR", C1zFR);

            info.AddValue("D1x_FR", D1xFR);
            info.AddValue("D1y_FR", D1yFR);
            info.AddValue("D1z_FR", D1zFR);

            info.AddValue("E1x_FR", E1xFR);
            info.AddValue("E1y_FR", E1yFR);
            info.AddValue("E1z_FR", E1zFR);

            info.AddValue("F1x_FR", F1xFR);
            info.AddValue("F1y_FR", F1yFR);
            info.AddValue("F1z_FR", F1zFR);

            info.AddValue("G1x_FR", G1xFR);
            info.AddValue("G1y_FR", G1yFR);
            info.AddValue("G1z_FR", G1zFR);

            info.AddValue("H1x_FR", H1xFR);
            info.AddValue("H1y_FR", H1yFR);
            info.AddValue("H1z_FR", H1zFR);

            info.AddValue("I1x_FR", I1xFR);
            info.AddValue("I1y_FR", I1yFR);
            info.AddValue("I1z_FR", I1zFR);

            info.AddValue("J1x_FR", J1xFR);
            info.AddValue("J1y_FR", J1yFR);
            info.AddValue("J1z_FR", J1zFR);

            info.AddValue("JO1x_FR", JO1xFR);
            info.AddValue("JO1y_FR", JO1yFR);
            info.AddValue("JO1z_FR", JO1zFR);

            info.AddValue("K1x_FR", K1xFR);
            info.AddValue("K1y_FR", K1yFR);
            info.AddValue("K1z_FR", K1zFR);

            info.AddValue("M1x_FR", M1xFR);
            info.AddValue("M1y_FR", M1yFR);
            info.AddValue("M1z_FR", M1zFR);

            info.AddValue("N1x_FR", N1xFR);
            info.AddValue("N1y_FR", N1yFR);
            info.AddValue("N1z_FR", N1zFR);

            info.AddValue("O1x_FR", O1xFR);
            info.AddValue("O1y_FR", O1yFR);
            info.AddValue("O1z_FR", O1zFR);

            info.AddValue("P1x_FR", P1xFR);
            info.AddValue("P1y_FR", P1yFR);
            info.AddValue("P1z_FR", P1zFR);

            info.AddValue("Q1x_FR", Q1xFR);
            info.AddValue("Q1y_FR", Q1yFR);
            info.AddValue("Q1z_FR", Q1zFR);

            info.AddValue("R1x_FR", R1xFR);
            info.AddValue("R1y_FR", R1yFR);
            info.AddValue("R1z_FR", R1zFR);

            info.AddValue("W1x_FR", W1xFR);
            info.AddValue("W1y_FR", W1yFR);
            info.AddValue("W1z_FR", W1zFR);

            info.AddValue("RideHeightRefx_FR", RideHeightRefFRx);
            info.AddValue("RideHeightRefy_FR", RideHeightRefFRy);
            info.AddValue("RideHeightRefz_FR", RideHeightRefFRz);

            #endregion

            #region Serialization of the REAR LEFT SUSPENSION COORDINATE TextBoxes
            info.AddValue("A1x_RL", A1xRL);
            info.AddValue("A1y_RL", A1yRL);
            info.AddValue("A1z_RL", A1zRL);

            info.AddValue("B1x_RL", B1xRL);
            info.AddValue("B1y_RL", B1yRL);
            info.AddValue("B1z_RL", B1zRL);

            info.AddValue("C1x_RL", C1xRL);
            info.AddValue("C1y_RL", C1yRL);
            info.AddValue("C1z_RL", C1zRL);

            info.AddValue("D1x_RL", D1xRL);
            info.AddValue("D1y_RL", D1yRL);
            info.AddValue("D1z_RL", D1zRL);

            info.AddValue("E1x_RL", E1xRL);
            info.AddValue("E1y_RL", E1yRL);
            info.AddValue("E1z_RL", E1zRL);

            info.AddValue("F1x_RL", F1xRL);
            info.AddValue("F1y_RL", F1yRL);
            info.AddValue("F1z_RL", F1zRL);

            info.AddValue("G1x_RL", G1xRL);
            info.AddValue("G1y_RL", G1yRL);
            info.AddValue("G1z_RL", G1zRL);

            info.AddValue("H1x_RL", H1xRL);
            info.AddValue("H1y_RL", H1yRL);
            info.AddValue("H1z_RL", H1zRL);

            info.AddValue("I1x_RL", I1xRL);
            info.AddValue("I1y_RL", I1yRL);
            info.AddValue("I1z_RL", I1zRL);

            info.AddValue("J1x_RL", J1xRL);
            info.AddValue("J1y_RL", J1yRL);
            info.AddValue("J1z_RL", J1zRL);

            info.AddValue("JO1x_RL", JO1xRL);
            info.AddValue("JO1y_RL", JO1yRL);
            info.AddValue("JO1z_RL", JO1zRL);

            info.AddValue("K1x_RL", K1xRL);
            info.AddValue("K1y_RL", K1yRL);
            info.AddValue("K1z_RL", K1zRL);

            info.AddValue("M1x_RL", M1xRL);
            info.AddValue("M1y_RL", M1yRL);
            info.AddValue("M1z_RL", M1zRL);

            info.AddValue("N1x_RL", N1xRL);
            info.AddValue("N1y_RL", N1yRL);
            info.AddValue("N1z_RL", N1zRL);

            info.AddValue("O1x_RL", O1xRL);
            info.AddValue("O1y_RL", O1yRL);
            info.AddValue("O1z_RL", O1zRL);

            info.AddValue("P1x_RL", P1xRL);
            info.AddValue("P1y_RL", P1yRL);
            info.AddValue("P1z_RL", P1zRL);

            info.AddValue("Q1x_RL", Q1xRL);
            info.AddValue("Q1y_RL", Q1yRL);
            info.AddValue("Q1z_RL", Q1zRL);

            info.AddValue("R1x_RL", R1xRL);
            info.AddValue("R1y_RL", R1yRL);
            info.AddValue("R1z_RL", R1zRL);

            info.AddValue("W1x_RL", W1xRL);
            info.AddValue("W1y_RL", W1yRL);
            info.AddValue("W1z_RL", W1zRL);

            info.AddValue("RideHeightRefx_RL", RideHeightRefRLx);
            info.AddValue("RideHeightRefy_RL", RideHeightRefRLy);
            info.AddValue("RideHeightRefz_RL", RideHeightRefRLz);

            #endregion

            #region Serialization of the REAR RIGHT SUSPENSION COORDINATE TextBoxes
            info.AddValue("A1x_RR", A1xRR);
            info.AddValue("A1y_RR", A1yRR);
            info.AddValue("A1z_RR", A1zRR);

            info.AddValue("B1x_RR", B1xRR);
            info.AddValue("B1y_RR", B1yRR);
            info.AddValue("B1z_RR", B1zRR);

            info.AddValue("C1x_RR", C1xRR);
            info.AddValue("C1y_RR", C1yRR);
            info.AddValue("C1z_RR", C1zRR);

            info.AddValue("D1x_RR", D1xRR);
            info.AddValue("D1y_RR", D1yRR);
            info.AddValue("D1z_RR", D1zRR);

            info.AddValue("E1x_RR", E1xRR);
            info.AddValue("E1y_RR", E1yRR);
            info.AddValue("E1z_RR", E1zRR);

            info.AddValue("F1x_RR", F1xRR);
            info.AddValue("F1y_RR", F1yRR);
            info.AddValue("F1z_RR", F1zRR);

            info.AddValue("G1x_RR", G1xRR);
            info.AddValue("G1y_RR", G1yRR);
            info.AddValue("G1z_RR", G1zRR);

            info.AddValue("H1x_RR", H1xRR);
            info.AddValue("H1y_RR", H1yRR);
            info.AddValue("H1z_RR", H1zRR);

            info.AddValue("I1x_RR", I1xRR);
            info.AddValue("I1y_RR", I1yRR);
            info.AddValue("I1z_RR", I1zRR);

            info.AddValue("J1x_RR", J1xRR);
            info.AddValue("J1y_RR", J1yRR);
            info.AddValue("J1z_RR", J1zRR);

            info.AddValue("JO1x_RR", JO1xRR);
            info.AddValue("JO1y_RR", JO1yRR);
            info.AddValue("JO1z_RR", JO1zRR);

            info.AddValue("K1x_RR", K1xRR);
            info.AddValue("K1y_RR", K1yRR);
            info.AddValue("K1z_RR", K1zRR);

            info.AddValue("M1x_RR", M1xRR);
            info.AddValue("M1y_RR", M1yRR);
            info.AddValue("M1z_RR", M1zRR);

            info.AddValue("N1x_RR", N1xRR);
            info.AddValue("N1y_RR", N1yRR);
            info.AddValue("N1z_RR", N1zRR);

            info.AddValue("O1x_RR", O1xRR);
            info.AddValue("O1y_RR", O1yRR);
            info.AddValue("O1z_RR", O1zRR);

            info.AddValue("P1x_RR", P1xRR);
            info.AddValue("P1y_RR", P1yRR);
            info.AddValue("P1z_RR", P1zRR);

            info.AddValue("Q1x_RR", Q1xRR);
            info.AddValue("Q1y_RR", Q1yRR);
            info.AddValue("Q1z_RR", Q1zRR);

            info.AddValue("R1x_RR", R1xRR);
            info.AddValue("R1y_RR", R1yRR);
            info.AddValue("R1z_RR", R1zRR);

            info.AddValue("W1x_RR", W1xRR);
            info.AddValue("W1y_RR", W1yRR);
            info.AddValue("W1z_RR", W1zRR);

            info.AddValue("RideHeightRefx_RR", RideHeightRefRRx);
            info.AddValue("RideHeightRefy_RR", RideHeightRefRRy);
            info.AddValue("RideHeightRefz_RR", RideHeightRefRRz);

            #endregion

            #region Serialization of the Tire TextBoxes
            info.AddValue("TireRate", TireRate);
            info.AddValue("TireWidth", TireWidth);
            #endregion

            #region Serialization of the SPRING TextBoxes
            info.AddValue("SpringRate", SpringRate);
            info.AddValue("SpringPreload", SpringPreload);
            info.AddValue("SpringFreeLength", SpringFreeLength);
            #endregion

            #region Serialization of the ARB TextBoxes
            info.AddValue("AntiRollBarRate", AntiRollBarRate);
            #endregion

            #region Serialization of the DAMPER TextBoxes
            info.AddValue("DamperGasPressure", DamperGasPressure);
            info.AddValue("DamperShaftDia", DamperShaftDia);
            #endregion

            #region Serialization of the CHASSIS TextBoxes
            info.AddValue("Suspended_Mass", SuspendedMass);

            info.AddValue("NonSuspended_Mass_FL", NonSuspendedMassFL);
            info.AddValue("NonSuspended_Mass_FR", NonSuspendedMassFR);
            info.AddValue("NonSuspended_Mass_RL", NonSuspendedMassRL);
            info.AddValue("NonSuspended_Mass_RR", NonSuspendedMassRR);

            info.AddValue("SuspendedMass_COG_x", SMCGx);
            info.AddValue("SuspendedMass_COG_y", SMCGy);
            info.AddValue("SuspendedMass_COG_z", SMCGz);

            info.AddValue("NonSuspendedMass_COG_FL_x", NSMCGFLx);
            info.AddValue("NonSuspendedMass_COG_FL_y", NSMCGFLy);
            info.AddValue("NonSuspendedMass_COG_FL_z", NSMCGFLz);

            info.AddValue("NonSuspendedMass_COG_FR_x", NSMCGFRx);
            info.AddValue("NonSuspendedMass_COG_FR_y", NSMCGFRy);
            info.AddValue("NonSuspendedMass_COG_FR_z", NSMCGFRz);

            info.AddValue("NonSuspendedMass_COG_RL_x", NSMCGRLx);
            info.AddValue("NonSuspendedMass_COG_RL_y", NSMCGRLy);
            info.AddValue("NonSuspendedMass_COG_RL_z", NSMCGRLz);

            info.AddValue("NonSuspendedMass_COG_RR_x", NSMCGRRx);
            info.AddValue("NonSuspendedMass_COG_RR_y", NSMCGRRy);
            info.AddValue("NonSuspendedMass_COG_RR_z", NSMCGRRz);
            #endregion

            #region Serialization of the WHEEL ALIGNMENT TextBoxes
            info.AddValue("StaticCamber", StaticCamber);
            info.AddValue("StaticToe", StaticToe);
            #endregion 

            #region Serialization of the VEHICLE comboBoxes
            info.AddValue("comboBoxSCFL_SelectedItem", comboBoxSCFL_SelectedItemIndex);
            info.AddValue("comboBoxSCFR_SelectedItem", comboBoxSCFR_SelectedItemIndex);
            info.AddValue("comboBoxSCRL_SelectedItem", comboBoxSCRL_SelectedItemIndex);
            info.AddValue("comboBoxSCRR_SelectedItem", comboBoxSCRR_SelectedItemIndex);

            info.AddValue("comboBoxTireFL_SelectedItem", comboBoxTireFL_SelectedItem);
            info.AddValue("comboBoxTireFR_SelectedItem", comboBoxTireFR_SelectedItem);
            info.AddValue("comboBoxTireRL_SelectedItem", comboBoxTireRL_SelectedItem);
            info.AddValue("comboBoxTireRR_SelectedItem", comboBoxTireRR_SelectedItem);

            info.AddValue("comboBoxSpringFL_SelectedItem", comboBoxSpringFL_SelectedItem);
            info.AddValue("comboBoxSpringFR_SelectedItem", comboBoxSpringFR_SelectedItem);
            info.AddValue("comboBoxSpringRL_SelectedItem", comboBoxSpringRL_SelectedItem);
            info.AddValue("comboBoxSpringRR_SelectedItem", comboBoxSpringRR_SelectedItem);

            info.AddValue("comboboxDamperFL_SelectedItem", comboBoxDamperFL_SelectedItem);
            info.AddValue("comboBoxDamperFR_SelectedItem", comboBoxDamperFR_SelectedItem);
            info.AddValue("comboBoxDamperRL_SelectedItem", comboBoxDamperRL_SelectedItem);
            info.AddValue("comboBoxDamperRR_SelectedItem", comboBoxDamperRR_SelectedItem);

            info.AddValue("comboBoxARBFront_SelectedItem", comboBoxARBFront_SelectedItem);
            info.AddValue("comboBoxARBRear_SelectedItem", comboBoxARBRear_SelectedItem);

            info.AddValue("comboBoxChassis_SelectedItem", comboBoxChassis_SelectedItem);

            info.AddValue("comboBoxWAFL_SelectedItem", comboBoxWAFL_SelectedItem);
            info.AddValue("comboBoxWAFR_SelectedItem", comboBoxWAFR_SelectedItem);
            info.AddValue("comboBoxWARL_SelectedItem", comboBoxWARL_SelectedItem);
            info.AddValue("comboBoxWARR_SelectedItem", comboBoxWARR_SelectedItem);

            info.AddValue("comboBoxVehicle_SelectedItem", comboBoxVehicle_SelectedItem);
            info.AddValue("comboBoxMotion_SelectedItem", combobocMotion_SelectedItem);
            #endregion

            #endregion

            #region Serialization of the Input Item Control

            #region Serialization of the FRONT LEFT Control

            #region FRONT LEFT Navigation Page
            info.AddValue("NavigationPageFL_Caption", navigationPagePushRodFL);
            #endregion

            #region FRONT LEFT Accordion Control
            info.AddValue("AccordionControl_SuspensionCoordinatesFL_Visibility", accordionControlSuspensionCoordinatesFL);

            info.AddValue("AccordionControlElement_FixedPointFL_Expanded", accordionControlFixedPointFL);
            info.AddValue("AccordionControlElement_MovingPointFL_Expanded", accordionControlMovingPointsFL);

            info.AddValue("AccordionControlElement_FixedPointFLUpperFrontChassis_Visibility", accordionControlFixedPointsFLUpperFrontChassis);
            info.AddValue("AccordiionControlElement_FixedPointsFLUpperRearChassis_Visibility", accordionControlFixedPointsFLUpperRearChassis);
            info.AddValue("AccordiionControlElement_FixedPointsFLBellCrankPivot_Visibility", accordionControlFixedPointsFLBellCrankPivot);
            info.AddValue("AccordiionControlElement_MovingPointsFLPushRodBellCrank_Visibility", accordionControlMovingPointsFLPushRodBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsFLAntiRollBarBellCrank_Visibility", accordionControlMovingPointsFLAntiRollBarBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsFLUpperBallJoint_Visibility", accordionControlMovingPointsFLUpperBallJoint);
            info.AddValue("AccordiionControlElement_FixedPointsFLTorsionBarBottom_Visibility", accordionControlFixedPointsFLTorsionBarBottom);
            info.AddValue("AccordiionControlElement_MovingPointsFLPushRodUpright_Visibility", accordionControlMovingPointsFLPushRodUpright);

            info.AddValue("AccordiionControlElement_MovingPointsFLDamperBellCrank_Text", accordionControlMovingPointsFLDamperBellCrank);
            #endregion

            #endregion

            #region Serialization of the FRONT RIGHT Control

            #region FRONT RIGHT Navigation Page
            info.AddValue("NavigationPageFR_Caption", navigationPagePushRodFR);
            #endregion

            #region FRONT RIGHT Accordion Control
            info.AddValue("AccordionControl_SuspensionCoordinatesFR_Visibility", accordionControlSuspensionCoordinatesFR);

            info.AddValue("AccordionControlElement_FixedPointFR_Expanded", accordionControlFixedPointFR);
            info.AddValue("AccordionControlElement_MovingPointFR_Expanded", accordionControlMovingPointsFR);

            info.AddValue("AccordionControlElement_FixedPointFRUpperFrontChassis_Visibility", accordionControlFixedPointsFRUpperFrontChassis);
            info.AddValue("AccordiionControlElement_FixedPointsFRUpperRearChassis_Visibility", accordionControlFixedPointsFRUpperRearChassis);
            info.AddValue("AccordiionControlElement_FixedPointsFRBellCrankPivot_Visibility", accordionControlFixedPointsFRBellCrankPivot);
            info.AddValue("AccordiionControlElement_MovingPointsFRPushRodBellCrank_Visibility", accordionControlMovingPointsFRPushRodBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsFRAntiRollBarBellCrank_Visibility", accordionControlMovingPointsFRAntiRollBarBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsFRUpperBallJoint_Visibility", accordionControlMovingPointsFRUpperBallJoint);
            info.AddValue("AccordiionControlElement_FixedPointsFRTorsionBarBottom_Visibility", accordionControlFixedPointsFRTorsionBarBottom);
            info.AddValue("AccordiionControlElement_MovingPointsFRPushRodUpright_Visibility", accordionControlMovingPointsFRPushRodUpright);

            info.AddValue("AccordiionControlElement_MovingPointsFRDamperBellCrank_Text", accordionControlMovingPointsFRDamperBellCrank);
            #endregion

            #endregion

            #region Serialization of the REAR LEFT Control

            #region REAR LEFT Navigation Page
            info.AddValue("NavigationPageRL_Caption", navigationPagePushRodRL);
            #endregion

            #region REAR LEFT Accordion Control
            info.AddValue("AccordionControl_SuspensionCoordinatesRL_Visibility", accordionControlSuspensionCoordinatesRL);

            info.AddValue("AccordionControlElement_FixedPointRL_Expanded", accordionControlFixedPointRL);
            info.AddValue("AccordionControlElement_MovingPointRL_Expanded", accordionControlMovingPointsRL);

            info.AddValue("AccordionControlElement_FixedPointRLUpperFrontChassis_Visibility", accordionControlFixedPointsRLUpperFrontChassis);
            info.AddValue("AccordiionControlElement_FixedPointsRLUpperRearChassis_Visibility", accordionControlFixedPointsRLUpperRearChassis);
            info.AddValue("AccordiionControlElement_FixedPointsRLBellCrankPivot_Visibility", accordionControlFixedPointsRLBellCrankPivot);
            info.AddValue("AccordiionControlElement_MovingPointsRLPushRodBellCrank_Visibility", accordionControlMovingPointsRLPushRodBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsRLAntiRollBarBellCrank_Visibility", accordionControlMovingPointsRLAntiRollBarBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsRLUpperBallJoint_Visibility", accordionControlMovingPointsRLUpperBallJoint);
            info.AddValue("AccordiionControlElement_FixedPointsRLTorsionBarBottom_Visibility", accordionControlFixedPointsRLTorsionBarBottom);
            info.AddValue("AccordiionControlElement_MovingPointsRLPushRodUpright_Visibility", accordionControlMovingPointsRLPushRodUpright);

            info.AddValue("AccordiionControlElement_MovingPointsRLDamperBellCrank_Text", accordionControlMovingPointsRLDamperBellCrank);
            #endregion

            #endregion

            #region Serialization of the REAR RIGHT Control

            #region REAR RIGHT Navigation Page
            info.AddValue("NavigationPageRR_Caption", navigationPagePushRodRR);
            #endregion

            #region REAR RIGHT Accordion Control
            info.AddValue("AccordionControl_SuspensionCoordinatesRR_Visibility", accordionControlSuspensionCoordinatesRR);

            info.AddValue("AccordionControlElement_FixedPointRR_Expanded", accordionControlFixedPointRR);
            info.AddValue("AccordionControlElement_MovingPointRR_Expanded", accordionControlMovingPointsRR);

            info.AddValue("AccordionControlElement_FixedPointRRUpperFrontChassis_Visibility", accordionControlFixedPointsRRUpperFrontChassis);
            info.AddValue("AccordiionControlElement_FixedPointsRRUpperRearChassis_Visibility", accordionControlFixedPointsRRUpperRearChassis);
            info.AddValue("AccordiionControlElement_FixedPointsRRBellCrankPivot_Visibility", accordionControlFixedPointsRRBellCrankPivot);
            info.AddValue("AccordiionControlElement_MovingPointsRRPushRodBellCrank_Visibility", accordionControlMovingPointsRRPushRodBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsRRAntiRollBarBellCrank_Visibility", accordionControlMovingPointsRRAntiRollBarBellCrank);
            info.AddValue("AccordiionControlElement_MovingPointsRRUpperBallJoint_Visibility", accordionControlMovingPointsRRUpperBallJoint);
            info.AddValue("AccordiionControlElement_FixedPointsRRTorsionBarBottom_Visibility", accordionControlFixedPointsRRTorsionBarBottom);
            info.AddValue("AccordiionControlElement_MovingPointsRRPushRodUpright_Visibility", accordionControlMovingPointsRRPushRodUpright);

            info.AddValue("AccordiionControlElement_MovingPointsRRDamperBellCrank_Text", accordionControlMovingPointsRRDamperBellCrank);
            #endregion

            #endregion

            #region Serialization of the TIRE Control
            info.AddValue("AccordionControl_TireStiffness_Visibility", accordionControlTireStiffness);
            #endregion

            #region Serialization of the SPRING Control
            info.AddValue("AccordionControl_Springs_Visibility", accordionControlSprings);
            #endregion

            #region Serialization of the ARB Control
            info.AddValue("AccordionControl_AntiRrollBar_Visibility", accordionControlAntiRollBar);
            #endregion

            #region Serialization of the DAMPER Control
            info.AddValue("AccordionControl_Damper_Visibility", accordionControlDamper);
            #endregion

            #region Serialization of the CHASSIS Control
            info.AddValue("AccordionControl_Chassis_Visibility", accordionControlChassis);
            #endregion

            #region Serialization of the WHEEL ALIGNMENT Control
            info.AddValue("AccordionControl_WheelAlignment_Visibility", accordionControlWheelAlignment);
            #endregion

            #region Serialization of the VEHICLE Control
            info.AddValue("AccordionControl_Vehicle_Visibility", accordionControlVehicleItem);
            #endregion 

            #endregion

            #region Serialization of the Group Control
            info.AddValue("groupControl_Visibility", groupControl13);
            #endregion

            #region Serialization of the Side Panel
            info.AddValue("sidePanel2_Visibility", sidePanel2);
            #endregion

            #region Serialization of the Tab Pane Control
            info.AddValue("XtraTabControl", xtraTabControl);
            info.AddValue("SelectedTabPage", SelectedPage);

            //info.AddValue("TabPaneResults", tabPaneResults);
            //info.AddValue("SelectedPage", SelectedPage);
            #endregion

            #region Serialization of the Ribbon Selected Page Index
            info.AddValue("selectedRibbonPage", selectedRibbonPage);
            #endregion

        }





    }
}

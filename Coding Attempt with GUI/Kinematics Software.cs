using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class KinematicsSoftwareInterface : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        

        public KinematicsSoftwareInterface()
        {

            InitializeComponent();
            
            this.DoubleBuffered = true;
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
            
            
        }

        
        public int CalculateResultsButtonClickCounter = 1;
        
        public class ObjectInitializer
        {
            
            public int Assy_IdentifierFL, Assy_IdentifierFR, Assy_IdentifierRL, Assy_IdentifierRR;

            #region Declaration of the Global Array and Globl List of Tire Object
            public int tireitemcounterBB, tireitemcounterNB;
            public Tire[] Assy_Tire;
            public List<Tire> Assy_List_Tire;
            #endregion

            #region Declaration of the Global Array and Globl List of Suspension Coordinate Objects
            public int susFLitemcounterBB, susFLitemcounterNB, susFRitemcounterBB, susFRitemcounterNB, susRLitemcounterBB, susRLitemcounterNB, susRRitemcounterBB, susRRitemcounterNB;
            public SuspensionCoordinatesMaster[] Assy_SCM;
            public List<SuspensionCoordinatesFront> Assy_List_SCFL;
            public List<SuspensionCoordinatesFrontRight> Assy_List_SCFR;
            public List<SuspensionCoordinatesRear> Assy_List_SCRL;
            public List<SuspensionCoordinatesRearRight> Assy_List_SCRR;
            #endregion

            #region Declaration of the Global Array and Globl List of Spring Object
            public int springitemcounterBB, springitemcounterNB; 
            public Spring[] Assy_Spring;
            public List<Spring> Assy_List_Spring;
            #endregion

            #region Declaration of the Global Array and Globl List of Damper Object
            public int damperitemcounterBB, damperitemcounterNB;
            public Damper[] Assy_Damper;
            public List<Damper> Assy_List_Damper;
            #endregion

            #region Declaration of the Global Array and Globl List of ARB Object
            public int arbitemcounterBB, arbitemcounterNB;
            public AntiRollBar[] Assy_ARB;
            public List<AntiRollBar> Assy_List_ARB;
            #endregion

            #region Declaration of the Global Array and Global List of the Chassis Object
            public int chassisitemcounterBB, chassisitemcounterNB;
            public Chassis Assy_Chassis;
            public List<Chassis> Assy_List_Chassis;
            #endregion

            #region Declaration of the Global Array and Global List of the Wheel Alignment Object
            public int waitemcounterBB, waitemcounterNB;
            public WheelAlignment[] Assy_WA;
            public List<WheelAlignment> Assy_List_WA;
            #endregion

            public List<InputSheet> List_I1;

            public OutputClass[] Assy_OC;
            
            public Vehicle Assembled_Vehicle;
            

            public  ObjectInitializer(KinematicsSoftwareInterface _r1)
            {

                #region Initialization of the Global Identifier for Assembly
                Assy_IdentifierFL = 1;
                Assy_IdentifierFR = 2;
                Assy_IdentifierRL = 3;
                Assy_IdentifierRR = 4;
                #endregion

                #region Initialization of the Global Array and Globl List of Tire Object
                tireitemcounterBB = 0; tireitemcounterNB = 0;
                Assy_Tire = new Tire[4];
                Assy_List_Tire = new List<Tire>();
                #endregion

                #region Initialization of the Global Array ad Global Array of Suspension Coordinates Object
                Assy_SCM = new SuspensionCoordinatesMaster[4];

                susFLitemcounterBB = 0; susFLitemcounterNB = 0;
                Assy_List_SCFL = new List<SuspensionCoordinatesFront>();

                susFRitemcounterBB = 0; susFRitemcounterNB = 0;
                Assy_List_SCFR = new List<SuspensionCoordinatesFrontRight>();

                susRLitemcounterBB = 0; susRLitemcounterNB = 0;
                Assy_List_SCRL = new List<SuspensionCoordinatesRear>();

                susRRitemcounterBB = 0; susRRitemcounterNB = 0;
                Assy_List_SCRR = new List<SuspensionCoordinatesRearRight>();
                
                #endregion

                #region Initialization of the Global Array and Globl List of Spring Object
                springitemcounterBB = 0; springitemcounterNB = 0;
                Assy_Spring = new Spring[4];
                Assy_List_Spring = new List<Spring>();
                #endregion

                #region Initialization of the Global Array and Globl List of Damper Objecto
                damperitemcounterBB = 0; damperitemcounterNB = 0;
                Assy_Damper = new Damper[4];
                Assy_List_Damper = new List<Damper>();
                #endregion

                #region Initialization of the Global Array and Globl List of Anti-Roll Bar Object
                arbitemcounterBB = 0; arbitemcounterNB = 0;
                Assy_ARB = new AntiRollBar[4];
                Assy_List_ARB = new List<AntiRollBar>();
                #endregion

                #region Initialization of the Global Chassis Object and Global List of Chassis Object
                chassisitemcounterBB = 0; chassisitemcounterNB = 0;
                Assy_List_Chassis = new List<Chassis>();
                #endregion

                #region Initialization of the Global Array and Global List of Wheel Alignment Object
                waitemcounterBB = 0; waitemcounterNB = 0;
                Assy_WA = new WheelAlignment[4];
                Assy_List_WA = new List<WheelAlignment>();
                #endregion

                List_I1 = new List<InputSheet>();
                

                #region Output Class
                #region New Output Instance -  - Front Left, Front Right, Rear Left, Rear Right
                OutputClass ocfl = new OutputClass();
                OutputClass ocfr = new OutputClass();
                OutputClass ocrl = new OutputClass();
                OutputClass ocrr = new OutputClass();
                #endregion
                #region Initialization of the Global Array of Anti-Roll Bar Object
                Assy_OC = new OutputClass[4];
                Assy_OC[0] = ocfl;
                Assy_OC[1] = ocfr;
                Assy_OC[2] = ocrl;
                Assy_OC[3] = ocrr;
                #endregion
                #endregion

            }
        }


        public static Kinematics_Software_New R1_New = new Kinematics_Software_New();
        public static ObjectInitializer M1_Global = new ObjectInitializer(R1_New);


        private void McPhersonStrutIdentifierFront_CheckedChanged_1(object sender, EventArgs e)
        {
            if (McPhersonStrutIdentifierFront.Checked == true)
            {
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


                accordionControlFixedPointFLUpperFrontChassis.Visible = false; accordionControlFixedPointFLUpperRearChassis.Visible = false; accordionControlFixedPointFLBellCrankPivot.Visible = false; // Removing irrelevant accordion control elements
                accordionControlFixedPointFRUpperFrontChassis.Visible = false; accordionControlFixedPointFRUpperRearChassis.Visible = false; accordionControlFixedPointFRBellCrankPivot.Visible = false;
                accordionControlMovingPointFLPushRodBellCrank.Visible = false; accordionControlMovingPointFLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointFLPushRodBellCrank.Visible = false; accordionControlMovingPointFLUpperBallJoint.Visible = false;
                accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsFRPushRodBellCrank.Visible = false; accordionControlMovingPointsFRUpperBallJoint.Visible = false;

                label841.Text = "Damper Chassis Mount";
                label7.Text = "Damper Chassis Mount";
                label860.Text = "Upper Ball Joint";
                label39.Text = "Upper Ball Joint";
                label24.Hide(); label23.Hide(); label26.Hide(); label14.Hide(); label17.Hide(); label15.Hide(); label41.Hide(); label40.Hide(); label36.Hide(); label31.Hide(); label858.Hide(); label859.Hide(); label862.Hide(); label867.Hide();

                navigationPagePushRodFL.Caption = "Strut FL";
                navigationPagePushRodFR.Caption = "Strut FR";
                D1xFL.Text = "-146.87"; D1yFL.Text = "1054.3"; D1zFL.Text = "69.72"; C1xFL.Text = "-159.15"; C1yFL.Text = "1056.5"; C1zFL.Text = "-146.53";
                Q1xFL.Text = "-350"; Q1yFL.Text = "1033"; Q1zFL.Text = "40.37"; JO1xFL.Text = "-312.06"; JO1yFL.Text = "1417.92"; JO1zFL.Text = "-1.46";
                J1xFL.Text = "-363.04"; J1yFL.Text = "1178.035"; J1zFL.Text = "-1.46"; E1xFL.Text = "-376.21"; E1yFL.Text = "1061.57"; E1zFL.Text = "2.61";
                M1xFL.Text = "-389.73"; M1yFL.Text = "1094.77"; M1zFL.Text = "47.35"; K1xFL.Text = "-363.89"; K1yFL.Text = "1180.26"; K1zFL.Text = "0.47";
                N1xFL.Text = "-154.13"; N1yFL.Text = "1075.7"; N1zFL.Text = "40.37";
                P1xFL.Text = "-380.76"; P1yFL.Text = "1025.42"; P1zFL.Text = "-3.69"; W1xFL.Text = "-412.6"; W1yFL.Text = "950"; W1zFL.Text = "-0.75";
                NSMCGFLx.Text = "-375.8"; NSMCGFLy.Text = "1195.2"; NSMCGFLz.Text = "0.48";



                D1xFR.Text = "146.87"; D1yFR.Text = "1054.3"; D1zFR.Text = "69.72"; C1xFR.Text = "159.15"; C1yFR.Text = "1056.5"; C1zFR.Text = "-146.53";
                Q1xFR.Text = "350"; Q1yFR.Text = "1033"; Q1zFR.Text = "40.37"; JO1xFR.Text = "312.06"; JO1yFR.Text = "1417.92"; JO1zFR.Text = "-1.46";
                J1xFR.Text = "363.04"; J1yFR.Text = "1178.035"; J1zFR.Text = "-1.46"; E1xFR.Text = "376.21"; E1yFR.Text = "1061.57"; E1zFR.Text = "2.61";
                M1xFR.Text = "389.73"; M1yFR.Text = "1094.77"; M1zFR.Text = "47.35"; K1xFR.Text = "363.89"; K1yFR.Text = "1180.26"; K1zFR.Text = "0.47";
                N1xFR.Text = "154.13"; N1yFR.Text = "1075.7"; N1zFR.Text = "40.37";
                P1xFR.Text = "380.76"; P1yFR.Text = "1025.42"; P1zFR.Text = "-3.69"; W1xFR.Text = "412.6"; W1yFR.Text = "950"; W1zFR.Text = "-0.75";
                NSMCGFRx.Text = "375.8"; NSMCGFRy.Text = "1195.2"; NSMCGFRz.Text = "0.48";
                #endregion

            }
            else
            {
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



                accordionControlFixedPointFLUpperFrontChassis.Visible = true; accordionControlFixedPointFLUpperRearChassis.Visible = true; accordionControlFixedPointFLBellCrankPivot.Visible = true;
                accordionControlFixedPointFRUpperFrontChassis.Visible = true; accordionControlFixedPointFRUpperRearChassis.Visible = true; accordionControlFixedPointFRBellCrankPivot.Visible = true;
                accordionControlMovingPointFLPushRodBellCrank.Visible = true; accordionControlMovingPointFLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointFLPushRodUpright.Visible = true; accordionControlMovingPointFLUpperBallJoint.Visible = true;
                accordionControlMovingPointsFRPushRodBellCrank.Visible = true; accordionControlMovingPointsFRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsFRPushRodUpright.Visible = true; accordionControlMovingPointsFRUpperBallJoint.Visible = true;

                label841.Text = "Damper Shock Mount";
                label7.Text = "Damper Shock Mount";
                label860.Text = "Damper Bell Crank";
                label39.Text = "Damper Bell Crank";
                label24.Show(); label23.Show(); label26.Show(); label14.Show(); label17.Show(); label15.Show(); label41.Show(); label40.Show(); label36.Show(); label31.Show(); label858.Show(); label859.Show(); label862.Show(); label867.Show();
                navigationPagePushRodFL.Caption = "Pushrod FL";
                navigationPagePushRodFR.Caption = "Pushrod FR";


                D1xFL.Text = "-221.21"; D1yFL.Text = "1065.17"; D1zFL.Text = "105"; C1xFL.Text = "-239.68"; C1yFL.Text = "1068.38"; C1zFL.Text = "-220.68";
                Q1xFL.Text = "-284"; Q1yFL.Text = "1033"; Q1zFL.Text = "60.8"; JO1xFL.Text = "-36"; JO1yFL.Text = "1572"; JO1zFL.Text = "-6.73";
                J1xFL.Text = "-235.1"; J1yFL.Text = "1592.74"; J1zFL.Text = "-6.73"; E1xFL.Text = "-566.57"; E1yFL.Text = "1076.04"; E1zFL.Text = "3.94";
                N1xFL.Text = "-232.12"; N1yFL.Text = "1097.4"; N1zFL.Text = "60.8";
                M1xFL.Text = "-586.92"; M1yFL.Text = "1126.03"; M1zFL.Text = "71.31"; K1xFL.Text = "-547.99"; K1yFL.Text = "1182.49"; K1zFL.Text = "0.71";
                P1xFL.Text = "-278.97"; P1yFL.Text = "1021.58"; P1zFL.Text = "-5.57"; W1xFL.Text = "-621.35"; W1yFL.Text = "950.85"; W1zFL.Text = "-1.13";
                NSMCGFLx.Text = "-560.26"; NSMCGFLy.Text = "1262.87"; NSMCGFLz.Text = "0";

                D1xFR.Text = "221.21"; D1yFR.Text = "1065.17"; D1zFR.Text = "105"; C1xFR.Text = "239.68"; C1yFR.Text = "1068.38"; C1zFR.Text = "-220.68";
                Q1xFR.Text = "284"; Q1yFR.Text = "1033"; Q1zFR.Text = "60.8"; JO1xFR.Text = "36"; JO1yFR.Text = "1572"; JO1zFR.Text = "-6.73";
                J1xFR.Text = "235.1"; J1yFR.Text = "1592.74"; J1zFR.Text = "-6.73"; E1xFR.Text = "566.57"; E1yFR.Text = "1076.04"; E1zFR.Text = "3.94";
                N1xFR.Text = "232.12"; N1yFR.Text = "1097.4"; N1zFR.Text = "60.8";
                M1xFR.Text = "-586.92"; M1yFR.Text = "1126.03"; M1zFR.Text = "71.31"; K1xFR.Text = "547.99"; K1yFR.Text = "1182.49"; K1zFR.Text = "0.71";
                P1xFR.Text = "278.97"; P1yFR.Text = "1021.58"; P1zFR.Text = "-5.57"; W1xFR.Text = "621.35"; W1yFR.Text = "950.85"; W1zFR.Text = "-1.13";
                NSMCGFRx.Text = "560.26"; NSMCGFRy.Text = "1262.87"; NSMCGFRz.Text = "0";
                #endregion
            }

        }

        private void McPhersonStrutIdentifierRear_CheckedChanged(object sender, EventArgs e)
        {
            if (McPhersonStrutIdentifierRear.Checked)
            {
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
                accordionControlMovingPointRLPushRodBellCrank.Visible = false; accordionControlMovingPointRLAntiRollBarBellCrank.Visible = false; accordionControlMovingPointRLPushRodBellCrank.Visible = false; accordionControlMovingPointRLUprightBallJoint.Visible = false;
                accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = false; accordionControlMovingPointsRRPushRodBellCrank.Visible = false; accordionControlMovingPointsRRUpperBallJoint.Visible = false;

                label25.Text = "Damper Chassis Mount";
                label16.Text = "Damper Chassis Mount";
                label51.Text = "Upper Ball Joint";
                label63.Text = "Upper Ball Joint";
                label831.Hide(); label821.Hide(); label856.Hide(); label5.Hide(); label6.Hide(); label8.Hide(); label53.Hide(); label52.Hide(); label48.Hide(); label43.Hide(); label65.Hide(); label64.Hide(); label60.Hide(); label55.Hide();
                navigationPagePushRodRL.Caption = "Stut RL";
                navigationPagePushRodRR.Caption = "Strut RR";

                D1xRL.Text = "-149.65"; D1yRL.Text = "1068.9"; D1zRL.Text = "-697.2"; C1xRL.Text = "-126.7"; C1yRL.Text = "1065.3"; C1zRL.Text = "-969.47";
                Q1xRL.Text = "-315.06"; Q1yRL.Text = "1056.2"; Q1zRL.Text = "-1027.99"; JO1xRL.Text = "-246.58"; JO1yRL.Text = "1389.37"; JO1zRL.Text = "-1035.72";
                J1xRL.Text = "-307.22"; J1yRL.Text = "1177.89"; J1zRL.Text = "-1035.72"; E1xRL.Text = "-314.95"; E1yRL.Text = "1062.17"; E1zRL.Text = "-1035.72";
                N1xRL.Text = "-123.37"; N1yRL.Text = "1065.3"; N1zRL.Text = "-996.03";
                M1xRL.Text = "-323.74"; M1yRL.Text = "1061.53"; M1zRL.Text = "-1064.98"; K1xRL.Text = "-331.18"; K1yRL.Text = "1180.26"; K1zRL.Text = "-1029.89";
                P1xRL.Text = "-337"; P1yRL.Text = "1047.98"; P1zRL.Text = "-1000.75"; W1xRL.Text = "-573.14"; W1yRL.Text = "950"; W1zRL.Text = "-1028.59";
                NSMCGRLx.Text = "-345.2"; NSMCGRLy.Text = "1192.26"; NSMCGRLz.Text = "-1029.89";


                D1xRR.Text = "149.65"; D1yRR.Text = "1068.9"; D1zRR.Text = "-697.2"; C1xRR.Text = "126.7"; C1yRR.Text = "1065.3"; C1zRR.Text = "-969.47";
                Q1xRR.Text = "315.06"; Q1yRR.Text = "1056.2"; Q1zRR.Text = "-1027.99"; JO1xRR.Text = "246.58"; JO1yRR.Text = "1389.37"; JO1zRR.Text = "-1035.72";
                J1xRR.Text = "307.22"; J1yRR.Text = "1177.89"; J1zRR.Text = "-1035.72"; E1xRR.Text = "314.95"; E1yRR.Text = "1062.17"; E1zRR.Text = "-1035.72";
                N1xRR.Text = "123.37"; N1yRR.Text = "1065.3"; N1zRR.Text = "-996.03";
                M1xRR.Text = "323.74"; M1yRR.Text = "1061.53"; M1zRR.Text = "-1064.98"; K1xRR.Text = "331.18"; K1yRR.Text = "1180.26"; K1zRR.Text = "-1029.89";
                P1xRR.Text = "337"; P1yRR.Text = "1047.98"; P1zRR.Text = "-1000.75"; W1xRR.Text = "573.14"; W1yRR.Text = "950"; W1zRR.Text = "-1028.59";
                NSMCGRRx.Text = "345.2"; NSMCGRRy.Text = "1192.26"; NSMCGRLz.Text = "-1029.89";

            }
            else
            {
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
                accordionControlMovingPointRLPushRodBellCrank.Visible = true; accordionControlMovingPointRLAntiRollBarBellCrank.Visible = true; accordionControlMovingPointRLPushRodBellCrank.Visible = true; accordionControlMovingPointRLUprightBallJoint.Visible = true;
                accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRAntiRollBarBellCrank.Visible = true; accordionControlMovingPointsRRPushRodBellCrank.Visible = true; accordionControlMovingPointsRRUpperBallJoint.Visible = true;

                label25.Text = "Damper Shock Mount";
                label16.Text = "Damper Shock Mount";
                label51.Text = "Damper Bell Crank";
                label63.Text = "Damper Bell Crank";
                label831.Show(); label821.Show(); label856.Show(); label5.Show(); label6.Show(); label8.Show(); label53.Show(); label52.Show(); label48.Show(); label43.Show(); label65.Show(); label64.Show(); label60.Show(); label55.Show();
                navigationPagePushRodRL.Caption = "Pushrod RL";
                navigationPagePushRodRR.Caption = "Pushrod RR";


                D1xRL.Text = "-225.38"; D1yRL.Text = "1087"; D1zRL.Text = "-1050"; C1xRL.Text = "-190.8"; C1yRL.Text = "1081.65"; C1zRL.Text = "-1460";
                Q1xRL.Text = "-265"; Q1yRL.Text = "1068"; Q1zRL.Text = "-1548.13"; JO1xRL.Text = "-18"; JO1yRL.Text = "1367"; JO1zRL.Text = "-1505";
                J1xRL.Text = "-216.65"; J1yRL.Text = "1389.98"; J1zRL.Text = "-1505"; E1xRL.Text = "-474.3"; E1yRL.Text = "1076.93"; E1zRL.Text = "-1526.35";
                M1xRL.Text = "-487.55"; M1yRL.Text = "1075.9"; M1zRL.Text = "-1603.84"; K1xRL.Text = "-498.75"; K1yRL.Text = "1180.5"; K1zRL.Text = "-1551";
                N1xRL.Text = "-185.8"; N1yRL.Text = "1081.65"; N1zRL.Text = "-1500";
                P1xRL.Text = "-260.21"; P1yRL.Text = "1055.57"; P1zRL.Text = "-1507.11"; W1xRL.Text = "-573.14"; W1yRL.Text = "949.2"; W1zRL.Text = "-1548.21";
                NSMCGRLx.Text = "-520.26"; NSMCGRLy.Text = "1262.87"; NSMCGRLz.Text = "-1550";


                D1xRR.Text = "225.38"; D1yRR.Text = "1087"; D1zRR.Text = "-1050"; C1xRR.Text = "190.8"; C1yRR.Text = "1081.65"; C1zRR.Text = "-1460";
                Q1xRR.Text = "265"; Q1yRR.Text = "1068"; Q1zRR.Text = "-1548.13"; JO1xRR.Text = "18"; JO1yRR.Text = "1367"; JO1zRR.Text = "-1505";
                J1xRR.Text = "216.65"; J1yRR.Text = "1389.98"; J1zRR.Text = "-1505"; E1xRR.Text = "474.3"; E1yRR.Text = "1076.93"; E1zRR.Text = "-1526.35";
                M1xRR.Text = "487.55"; M1yRR.Text = "1075.9"; M1zRR.Text = "-1603.84"; K1xRR.Text = "498.75"; K1yRR.Text = "1180.5"; K1zRR.Text = "-1551";
                N1xRR.Text = "185.8"; N1yRR.Text = "1081.65"; N1zRR.Text = "-1500";
                P1xRR.Text = "260.21"; P1yRR.Text = "1055.57"; P1zRR.Text = "-1507.11"; W1xRR.Text = "573.14"; W1yRR.Text = "949.2"; W1zRR.Text = "-1548.21";
                NSMCGRRx.Text = "520.26"; NSMCGRRy.Text = "1262.87"; NSMCGRRz.Text = "-1550";

            }

        }

        
        //
        // navBar Items GUI
        //
        private void navBarControl2_MouseDown(object sender, MouseEventArgs e)
        {
            navBarControl2 = sender as NavBarControl;
            NavBarHitInfo hitinfo = navBarControl2.CalcHitInfo(e.Location);
            if (hitinfo.HitTest == NavBarHitTest.GroupCaption)
            {
                switch (hitinfo.Group.Caption)
                {
                    case "Suspension Front Left":
                        #region GUI
                        sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
                    accordionControlSuspensionCoordinatesFR.Hide();
                    accordionControlSuspensionCoordinatesRL.Hide();
                    accordionControlSuspensionCoordinatesRR.Hide();
                    accordionControlDamper.Hide();
                    accordionControlAntiRollBar.Hide();
                    accordionControlSprings.Hide();
                    accordionControlChassis.Hide();
                    
                    accordionControlWheelAlignment.Hide();
                    accordionControlVehicleItem.Hide();
                    accordionControlSuspensionCoorindatesFL.Show();
                        #endregion
                        break;

                    case "Suspension Front Right":
                        #region GUI
                        sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        #endregion
                        break;

                    case "Suspension Rear Left":
                        #region GUI
                        sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        #endregion
                        break;

                    case "Suspension Rear Right":
                        #region GUI
                        sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        #endregion
                        break;

                    case "Tire": 
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
                    accordionControlSuspensionCoordinatesFR.Hide();
                    accordionControlSuspensionCoordinatesRL.Hide();
                    accordionControlSuspensionCoordinatesRR.Hide();
                    accordionControlDamper.Hide();
                    accordionControlAntiRollBar.Hide();
                    accordionControlSprings.Hide();
                    accordionControlChassis.Hide();
                    
                    accordionControlWheelAlignment.Hide();
                    accordionControlVehicleItem.Hide();
                    accordionControlTireStiffness.Show();
                    #endregion
                        break;

                    case "Springs":
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        break;

                    case "Damper":
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        break;

                    case "Anti-Roll Bar":
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        break;

                    case "Chassis":
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                     accordionControlSuspensionCoorindatesFL.Hide();
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
                        break;

                    case "Wheel Alignment":
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                        break;

                    case "Vehicle":
                        #region GUI
                    sidePanel2.Show();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
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
                    #endregion


                    break;

                    default: break;
                }
            }
        }

        //
        //Ribbon Items GUI
        //
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
                    accordionControlSuspensionCoorindatesFL.Hide();
                    accordionControlSuspensionCoordinatesFR.Hide();
                    accordionControlSuspensionCoordinatesRL.Hide();
                    accordionControlSuspensionCoordinatesRR.Hide();
                    accordionControlDamper.Hide();
                    accordionControlAntiRollBar.Hide();
                    accordionControlSprings.Hide();
                    accordionControlChassis.Hide();
                    
                    accordionControlWheelAlignment.Hide();
                    accordionControlVehicleItem.Hide();
                    navBarGroupSimulation.Visible = false;

                    navBarGroupDesign.Visible = true;
                        #endregion

                        break;

                    case "ribbonPageSimulation":
                        #region GUI
                        sidePanel2.Hide();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
                    accordionControlSuspensionCoordinatesFR.Hide();
                    accordionControlSuspensionCoordinatesRL.Hide();
                    accordionControlSuspensionCoordinatesRR.Hide();
                    accordionControlDamper.Hide();
                    accordionControlAntiRollBar.Hide();
                    accordionControlSprings.Hide();
                    accordionControlChassis.Hide();                    
                    accordionControlWheelAlignment.Hide();
                    accordionControlVehicleItem.Hide();
                    navBarGroupDesign.Visible = false;
                    navBarGroupSimulation.Visible = true;
                        #endregion
                        
                        break;
                    case "ribbonPageResults":
                        #region GUI
                        sidePanel2.Hide();
                    accordionControlTireStiffness.Hide();
                    accordionControlSuspensionCoorindatesFL.Hide();
                    accordionControlSuspensionCoordinatesFR.Hide();
                    accordionControlSuspensionCoordinatesRL.Hide();
                    accordionControlSuspensionCoordinatesRR.Hide();
                    accordionControlDamper.Hide();
                    accordionControlAntiRollBar.Hide();
                    accordionControlSprings.Hide();
                    accordionControlChassis.Hide();
                    accordionControlWheelAlignment.Hide();
                    accordionControlVehicleItem.Hide();
                    navBarGroupSimulation.Visible = false;
                        #endregion 
                                          
                        break;

                    default:
                        break;
                }
            }
        }



        //
        //Tire Item Creation and GUI
        //
        #region Tire Item Creation and GUI
        List<NavBarItem> navBarItemTire = new List<NavBarItem>();
        int tireitementerpress = 0; int tireitembuttonpress = 0;
        public void BarButtonTireRate_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;

            if (M1_Global.tireitemcounterBB == tireitementerpress)
            {
                for (int i_tire = 0; i_tire <= navBarItemTire.Count; i_tire++)
                {
                    if (M1_Global.tireitemcounterBB == i_tire)
                    {
                        navBarItemTire.Insert(i_tire, navBarControl2.Items.Add());
                        navBarItemTire[i_tire].Caption = "Tire " + Convert.ToString(M1_Global.tireitemcounterBB + 1);
                        navBarGroupTireStiffness.ItemLinks.Add(navBarItemTire[i_tire]);
                        navBarItemTire[i_tire].LinkClicked += new NavBarLinkEventHandler(navBarItemTire1_LinkClicked);
                        navBarItemTire[i_tire].Enabled = false;
                        M1_Global.tireitemcounterBB++;
                        break;
                    }
                }
            }
        }
        void navBarItemTire1_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupTireStiffness.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_tire = 0; c_tire <= navBarItemTire.Count; c_tire++)
            {
                if (index == c_tire)
                {
                    TireRateFL.Text = Convert.ToString(M1_Global.Assy_List_Tire[c_tire].TireRate);
                    TireWidthFL.Text = Convert.ToString(M1_Global.Assy_List_Tire[c_tire].TireWidth);
                }

            }
        }   
        public void CreateNewTire_Click(object sender, EventArgs e)
        {
            tireitembuttonpress++;
            tireitementerpress++;
            if (M1_Global.tireitemcounterBB == tireitembuttonpress)
            {
            for (int l_tire = 0; l_tire <= M1_Global.Assy_List_Tire.Count; l_tire++)
            {
                R1_New = this;
                Tire tire_list = new Tire(R1_New);

                    if (M1_Global.tireitemcounterNB == l_tire)
                    {
                        M1_Global.Assy_List_Tire.Insert(l_tire, tire_list);
                        M1_Global.Assy_List_Tire[l_tire]._TireName = "Tire " + Convert.ToString(l_tire + 1);
                        MessageBox.Show("Tire Item " + M1_Global.Assy_List_Tire[l_tire]._TireName + " has been created");

                        comboBoxTireFL.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                        comboBoxTireFL.DisplayMember = "_TireName";

                        comboBoxTireFR.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                        comboBoxTireFR.DisplayMember = "_TireName";

                        comboBoxTireRL.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                        comboBoxTireRL.DisplayMember = "_TireName";

                        comboBoxTireRR.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                        comboBoxTireRR.DisplayMember = "_TireName";

                        navBarItemTire[l_tire].Enabled = true;
                        M1_Global.tireitemcounterNB++;

                        break;
                    }
                }
            tireitembuttonpress++;
            tireitementerpress++;
            }
            tireitembuttonpress--;
            tireitementerpress--;
        }

        
        private void TireTextbox_Click(object sender, KeyEventArgs e)
      {
            if (e.KeyCode == Keys.Enter)
            {
                tireitementerpress++;
                tireitembuttonpress++;
                if (M1_Global.tireitemcounterBB == tireitementerpress)
                {
                    if ((string.IsNullOrEmpty(TireWidthFL.Text)) || (string.IsNullOrEmpty(TireRateFL.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }

                    else if ((!string.IsNullOrEmpty(TireWidthFL.Text)) || (!string.IsNullOrEmpty(TireRateFL.Text)))
                    {
                        for (int l_tire = 0; l_tire <= M1_Global.Assy_List_Tire.Count; l_tire++)
                        {
                            R1_New = this;
                            Tire tire_list = new Tire(R1_New);

                            if (M1_Global.tireitemcounterNB == l_tire)
                            {
                                M1_Global.Assy_List_Tire.Insert(l_tire, tire_list);
                                M1_Global.Assy_List_Tire[l_tire]._TireName = "Tire " + Convert.ToString(l_tire + 1);
                                MessageBox.Show("Tire Item " + M1_Global.Assy_List_Tire[l_tire]._TireName + " has been created");

                                comboBoxTireFL.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                                comboBoxTireFL.DisplayMember = "_TireName";

                                comboBoxTireFR.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                                comboBoxTireFR.DisplayMember = "_TireName";

                                comboBoxTireRL.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                                comboBoxTireRL.DisplayMember = "_TireName";

                                comboBoxTireRR.Items.Insert(l_tire, M1_Global.Assy_List_Tire[l_tire]);
                                comboBoxTireRR.DisplayMember = "_TireName";

                                navBarItemTire[l_tire].Enabled = true;
                                M1_Global.tireitemcounterNB++;

                                break;
                            }
                        }
                    }
                    tireitementerpress++;
                    tireitembuttonpress++;
                }
                tireitementerpress--;
                tireitembuttonpress--;
            }
        }
        #endregion

        //
        //Spring Item Creation and GUI
        //
        #region Spring Item Creation and GUI
        List<NavBarItem> navBarItemSpring = new List<NavBarItem>();
        int springitempressed = 0; int springitembuttonpressed = 0;
        private void BarButtonSpring_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
            
            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.springitemcounterBB == springitempressed)
            {
                for (int i_spring = 0; i_spring <= navBarItemSpring.Count; i_spring++)
                {
                    if (M1_Global.springitemcounterBB == i_spring)
                    {
                        navBarItemSpring.Insert(i_spring, navBarControl2.Items.Add());
                        navBarItemSpring[i_spring].Caption = "Spring " + Convert.ToString(M1_Global.springitemcounterBB + 1);
                        navBarGroupSprings.ItemLinks.Add(navBarItemSpring[i_spring]);
                        navBarItemSpring[i_spring].LinkClicked += new NavBarLinkEventHandler(navBarItemSpring_LinkClicked);
                        navBarItemSpring[i_spring].Enabled = false;
                        M1_Global.springitemcounterBB++;
                        break;
                    }
                }
            }
        }
        void navBarItemSpring_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSprings.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_spring = 0; c_spring <= navBarItemSpring.Count; c_spring++)
            {
                if (index == c_spring)
                {
                    SpringRateFL.Text = Convert.ToString(M1_Global.Assy_List_Spring[c_spring].SpringRate);
                    SpringPreloadFL.Text = Convert.ToString(M1_Global.Assy_List_Spring[c_spring].SpringPreload);
                    SpringFreeLengthFL.Text = Convert.ToString(M1_Global.Assy_List_Spring[c_spring].SpringFreeLength);
                }
            }
        }
        private void CreateNewSpring_Click(object sender, EventArgs e)
        {
            springitembuttonpressed++;
            springitempressed++;
            if (M1_Global.springitemcounterBB == springitembuttonpressed)
            {
                for (int l_spring = 0; l_spring <= M1_Global.Assy_List_Spring.Count; l_spring++)
                {
                    R1_New = this;
                    Spring spring_list = new Spring(R1_New);
                    if (M1_Global.springitemcounterNB == l_spring)
                    {
                        M1_Global.Assy_List_Spring.Insert(l_spring, spring_list);
                        M1_Global.Assy_List_Spring[l_spring]._SpringName = "Spring " + Convert.ToString(l_spring + 1);
                        MessageBox.Show("Spring Item " + M1_Global.Assy_List_Spring[l_spring]._SpringName + " has been created");

                        comboBoxSpringFL.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                        comboBoxSpringFL.DisplayMember = "_SpringName";

                        comboBoxSpringFR.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                        comboBoxSpringFR.DisplayMember = "_SpringName";

                        comboBoxSpringRL.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                        comboBoxSpringRL.DisplayMember = "_SpringName";

                        comboBoxSpringRR.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                        comboBoxSpringRR.DisplayMember = "_SpringName";

                        navBarItemSpring[l_spring].Enabled = true;
                        M1_Global.springitemcounterNB++;
                        break;
                    }
                }
                springitembuttonpressed++;
                springitempressed++;
            }
            springitembuttonpressed--;
            springitempressed--;
        }
        
        private void SpringTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                springitempressed++;
                springitembuttonpressed++;
                if (M1_Global.springitemcounterBB == springitempressed)
                {
                    if ((string.IsNullOrEmpty(SpringRateFL.Text)) || (string.IsNullOrEmpty(SpringPreloadFL.Text)) || (string.IsNullOrEmpty(SpringFreeLengthFL.Text)))
                    {
                            MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!string.IsNullOrEmpty(SpringRateFL.Text)) || (!string.IsNullOrEmpty(SpringPreloadFL.Text)) || (!string.IsNullOrEmpty(SpringFreeLengthFL.Text)))
                    {
                        for (int l_spring = 0; l_spring <= M1_Global.Assy_List_Spring.Count; l_spring++)
                        {
                            R1_New = this;
                            Spring spring_list = new Spring(R1_New);
                            if (M1_Global.springitemcounterNB == l_spring)
                            {
                                M1_Global.Assy_List_Spring.Insert(l_spring, spring_list);
                                M1_Global.Assy_List_Spring[l_spring]._SpringName = "Spring " + Convert.ToString(l_spring + 1);
                                MessageBox.Show("Spring Item " + M1_Global.Assy_List_Spring[l_spring]._SpringName + " has been created");

                                comboBoxSpringFL.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                                comboBoxSpringFL.DisplayMember = "_SpringName";

                                comboBoxSpringFR.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                                comboBoxSpringFR.DisplayMember = "_SpringName";

                                comboBoxSpringRL.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                                comboBoxSpringRL.DisplayMember = "_SpringName";

                                comboBoxSpringRR.Items.Insert(l_spring, M1_Global.Assy_List_Spring[l_spring]);
                                comboBoxSpringRR.DisplayMember = "_SpringName";

                                navBarItemSpring[l_spring].Enabled = true;
                                M1_Global.springitemcounterNB++;
                                break;
                            }
                        }
                    }
                    springitempressed++;
                    springitembuttonpressed++;
                }
                springitempressed--;
                springitembuttonpressed--;
            }
        }
        #endregion

        //
        // Damper Item Creation and Gui
        //
        #region Damper Item Creation and Gui
        List<NavBarItem> navBarItemDamper = new List<NavBarItem>();
        int damperitempressed = 0; int damperitembuttonpressed = 0;
        private void BarButtonDamper_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.damperitemcounterBB == damperitempressed)
            {
                for (int i_damper = 0; i_damper <= navBarItemDamper.Count; i_damper++)
                {
                    if (M1_Global.damperitemcounterBB == i_damper)
                    {
                        navBarItemDamper.Insert(i_damper, navBarControl2.Items.Add());
                        navBarItemDamper[i_damper].Caption = "Damper " + Convert.ToString(M1_Global.damperitemcounterBB + 1);
                        navBarGroupDamper.ItemLinks.Add(navBarItemDamper[i_damper]);
                        navBarItemDamper[i_damper].LinkClicked += new NavBarLinkEventHandler(navBarItemDamper_LinkClicked);
                        navBarItemDamper[i_damper].Enabled = false;
                        M1_Global.damperitemcounterBB++;
                        break;
                    }
                }
            }
        }
        void navBarItemDamper_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupDamper.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_damper = 0; c_damper <= navBarItemDamper.Count; c_damper++)
            {
                if (index == c_damper)
                {
                    DamperGasPressureFL.Text = Convert.ToString(M1_Global.Assy_List_Damper[c_damper].DamperGasPressure);
                    DamperShaftDiaFL.Text = Convert.ToString(M1_Global.Assy_List_Damper[c_damper].DamperShaftDia);
                }
            }
        }
        private void CreateNewDamper_Click(object sender, EventArgs e)
        {
            damperitembuttonpressed++;
            damperitempressed++;
            if (M1_Global.damperitemcounterBB == damperitembuttonpressed)
            {
                for (int l_damper = 0; l_damper <= M1_Global.Assy_List_Damper.Count; l_damper++)
                {
                    R1_New = this;
                    Damper damper_list = new Damper(R1_New); ;
                    if (M1_Global.damperitemcounterNB == l_damper)
                    {
                        M1_Global.Assy_List_Damper.Insert(l_damper, damper_list);
                        M1_Global.Assy_List_Damper[l_damper]._DamperName = "Damper " + Convert.ToString(l_damper + 1);
                        MessageBox.Show("Damper Item " + M1_Global.Assy_List_Damper[l_damper]._DamperName + " has been created");

                        comboBoxDamperFL.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                        comboBoxDamperFL.DisplayMember = "_DamperName";

                        comboBoxDamperFR.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                        comboBoxDamperFR.DisplayMember = "_DamperName";

                        comboBoxDamperRL.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                        comboBoxDamperRL.DisplayMember = "_DamperName";

                        comboBoxDamperRR.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                        comboBoxDamperRR.DisplayMember = "_DamperName";

                        navBarItemDamper[l_damper].Enabled = true;
                        M1_Global.damperitemcounterNB++;
                        break;
                    }
                }
                damperitembuttonpressed++;
                damperitempressed++;
            }
            damperitembuttonpressed--;
            damperitempressed--;
        }
        private void DamperTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                damperitempressed++;
                damperitembuttonpressed++;
                if (M1_Global.damperitemcounterBB == damperitempressed)
                {
                    if ((string.IsNullOrEmpty(DamperGasPressureFL.Text)) || (string.IsNullOrEmpty(DamperShaftDiaFL.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!string.IsNullOrEmpty(DamperGasPressureFL.Text)) || (!string.IsNullOrEmpty(DamperShaftDiaFL.Text)))
                    {
                        for (int l_damper = 0; l_damper <= M1_Global.Assy_List_Damper.Count; l_damper++)
                        {
                            R1_New = this;
                            Damper damper_list = new Damper(R1_New); ;
                            if (M1_Global.damperitemcounterNB == l_damper)
                            {
                                M1_Global.Assy_List_Damper.Insert(l_damper, damper_list);
                                M1_Global.Assy_List_Damper[l_damper]._DamperName = "Damper " + Convert.ToString(l_damper + 1);
                                MessageBox.Show("Damper Item " + M1_Global.Assy_List_Damper[l_damper]._DamperName + " has been created");

                                comboBoxDamperFL.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                                comboBoxDamperFL.DisplayMember = "_DamperName";

                                comboBoxDamperFR.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                                comboBoxDamperFR.DisplayMember = "_DamperName";

                                comboBoxDamperRL.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                                comboBoxDamperRL.DisplayMember = "_DamperName";

                                comboBoxDamperRR.Items.Insert(l_damper, M1_Global.Assy_List_Damper[l_damper]);
                                comboBoxDamperRR.DisplayMember = "_DamperName";

                                navBarItemDamper[l_damper].Enabled = true;
                                M1_Global.damperitemcounterNB++;
                                break;
                            }
                        }
                    }
                    damperitempressed++;
                    damperitembuttonpressed++;
                }
                damperitempressed--;
                damperitembuttonpressed--;
            }
        }
        #endregion

        //
        // Anti-Roll Bar Item Creation and GUI
        //

        #region ARB Item Creation and GUI
        List<NavBarItem> navBarItemARB = new List<NavBarItem>();
        int arbitempressed = 0; int arbitembuttonpressed = 0;
        private void BarButtonARB_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.arbitemcounterBB == arbitempressed)
            {
                for (int i_arb = 0; i_arb <= navBarItemChassis.Count; i_arb++)
                {
                    if (M1_Global.arbitemcounterBB == i_arb)
                    {
                        navBarItemChassis.Insert(i_arb, navBarControl2.Items.Add());
                        navBarItemChassis[i_arb].Caption = "Anti-Roll Bar " + Convert.ToString(M1_Global.arbitemcounterBB + 1);
                        navBarGroupAntiRollBar.ItemLinks.Add(navBarItemChassis[i_arb]);
                        navBarItemChassis[i_arb].LinkClicked += new NavBarLinkEventHandler(navBarItemARB_LinkClicked);
                        navBarItemChassis[i_arb].Enabled = false;
                        M1_Global.arbitemcounterBB++;
                        break;
                    }
                }
            }
        }
        void navBarItemARB_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupAntiRollBar.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_arb = 0; c_arb <= navBarItemARB.Count; c_arb++)
            {
                if (index == c_arb)
                {
                    AntiRollBarRateFront.Text = Convert.ToString(M1_Global.Assy_List_ARB[c_arb].AntiRollBarRate);
                }
            }
        }
        
        private void CreateNewARB_Click(object sender, EventArgs e)
        {
            arbitembuttonpressed++;
            arbitempressed++;
            if (M1_Global.arbitemcounterBB == arbitembuttonpressed)
            {
                for (int l_arb = 0; l_arb <= M1_Global.Assy_List_ARB.Count; l_arb++)
                {
                    R1_New = this;
                    AntiRollBar arb_list = new AntiRollBar(R1_New);
                    if ((M1_Global.arbitemcounterNB == l_arb))
                    {
                        M1_Global.Assy_List_ARB.Insert(l_arb, arb_list);
                        M1_Global.Assy_List_ARB[l_arb]._ARBName = "ARB " + Convert.ToString(l_arb + 1);
                        MessageBox.Show("Anti-Roll Bar Item " + M1_Global.Assy_List_ARB[l_arb]._ARBName + " has been created");

                        comboBoxARBFront.Items.Insert(l_arb, M1_Global.Assy_List_ARB[l_arb]);
                        comboBoxARBFront.DisplayMember = "_ARBName";

                        comboBoxARBRear.Items.Insert(l_arb, M1_Global.Assy_List_ARB[l_arb]);
                        comboBoxARBRear.DisplayMember = "_ARBName";

                        navBarItemChassis[l_arb].Enabled = true;
                        M1_Global.arbitemcounterNB++;
                        break;
                    }
                }
                arbitembuttonpressed++;
                arbitempressed++;
            }
            arbitembuttonpressed--;
            arbitempressed--;
        }
        private void ARBTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                arbitempressed++;
                arbitembuttonpressed++;
                if (M1_Global.arbitemcounterBB == arbitempressed)
                {

                    if (string.IsNullOrEmpty(AntiRollBarRateFront.Text))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if (!string.IsNullOrEmpty(AntiRollBarRateFront.Text))
                    {
                        for (int l_arb = 0; l_arb <= M1_Global.Assy_List_ARB.Count; l_arb++)
                        {
                            R1_New = this;
                            AntiRollBar arb_list = new AntiRollBar(R1_New);
                            if ((M1_Global.arbitemcounterNB == l_arb))
                            {
                                M1_Global.Assy_List_ARB.Insert(l_arb, arb_list);
                                M1_Global.Assy_List_ARB[l_arb]._ARBName = "ARB " + Convert.ToString(l_arb + 1);
                                MessageBox.Show("Anti-Roll Bar Item " + M1_Global.Assy_List_ARB[l_arb]._ARBName + " has been created");

                                comboBoxARBFront.Items.Insert(l_arb, M1_Global.Assy_List_ARB[l_arb]);
                                comboBoxARBFront.DisplayMember = "_ARBName";

                                comboBoxARBRear.Items.Insert(l_arb, M1_Global.Assy_List_ARB[l_arb]);
                                comboBoxARBRear.DisplayMember = "_ARBName";

                                navBarItemChassis[l_arb].Enabled = true;
                                M1_Global.arbitemcounterNB++;
                                break;
                            }
                        }
                    }
                    arbitempressed++;
                    arbitembuttonpressed++;
                }
                arbitempressed--;
                arbitembuttonpressed--;
            }
        }
        #endregion

        //
        // Chassis item Creation and GUI
        //
        #region Chassis Item Creation and GUI
        List<NavBarItem> navBarItemChassis = new List<NavBarItem>();
        int chassisitempressed = 0; int chassisitembuttonpressed = 0;
        private void BarButtonChassis_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.chassisitemcounterBB == chassisitempressed)
            {
                for (int i_chassis = 0; i_chassis <= navBarItemChassis.Count; i_chassis++)
                {
                    if (M1_Global.chassisitemcounterBB == i_chassis)
                    {
                        navBarItemChassis.Insert(i_chassis, navBarControl2.Items.Add());
                        navBarItemChassis[i_chassis].Caption = "Chassis " + Convert.ToString(M1_Global.chassisitemcounterBB + 1);
                        navBarGroupChassis.ItemLinks.Add(navBarItemChassis[i_chassis]);
                        navBarItemChassis[i_chassis].LinkClicked += new NavBarLinkEventHandler(navBarItemChassis_LinkClicked);
                        navBarItemChassis[i_chassis].Enabled = false;
                        M1_Global.chassisitemcounterBB++;
                        break;
                    }
                }
            }
        }

        void navBarItemChassis_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupChassis.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_chassis = 0; c_chassis <= navBarItemChassis.Count; c_chassis++)
            {
                if (index == c_chassis)
                {
                    SuspendedMass.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].SuspendedMass);
                    SMCGx.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].SuspendedMassCoGx);
                    SMCGy.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].SuspendedMassCoGy);
                    SMCGz.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].SuspendedMassCoGz);

                    NonSuspendedMassFL.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFL);
                    NSMCGFLx.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFLCoGx);
                    NSMCGFLy.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFLCoGy);
                    NSMCGFLz.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFLCoGz);

                    NonSuspendedMassFR.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFR);
                    NSMCGFRx.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFRCoGx);
                    NSMCGFRy.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFRCoGy);
                    NSMCGFRz.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassFRCoGz);

                    NonSuspendedMassRL.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRL);
                    NSMCGRLx.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRLCoGx);
                    NSMCGRLy.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRLCoGy);
                    NSMCGRLz.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRLCoGz);

                    NonSuspendedMassRR.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRR);
                    NSMCGRRx.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRRCoGx);
                    NSMCGRRy.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRRCoGy);
                    NSMCGRRz.Text = Convert.ToString(M1_Global.Assy_List_Chassis[c_chassis].NonSuspendedMassRRCoGz);

                }
            }
        }
        private void CreateNewChassis_Click(object sender, EventArgs e)
        {
            chassisitembuttonpressed++;
            chassisitempressed++;
            if (M1_Global.chassisitemcounterBB == chassisitembuttonpressed)
            {
                for (int l_chassis = 0; l_chassis <= M1_Global.Assy_List_Chassis.Count; l_chassis++)
                {
                    R1_New = this;
                    Chassis chassis_list = new Chassis(R1_New);
                    if (M1_Global.chassisitemcounterNB == l_chassis)
                    {
                        M1_Global.Assy_List_Chassis.Insert(l_chassis, chassis_list);
                        M1_Global.Assy_List_Chassis[l_chassis]._ChassisName = "Chassis " + Convert.ToString(l_chassis + 1);
                        MessageBox.Show("Chassis Item " + M1_Global.Assy_List_Chassis[l_chassis]._ChassisName + " has been created");

                        comboBoxChassis.Items.Insert(l_chassis, M1_Global.Assy_List_Chassis[l_chassis]);
                        comboBoxChassis.DisplayMember = "_ChassisName";

                        navBarItemChassis[l_chassis].Enabled = true;
                        M1_Global.chassisitemcounterNB++;
                        break;
                    }
                }
                chassisitembuttonpressed++;
                chassisitempressed++;
            }
            chassisitembuttonpressed--;
            chassisitempressed--;
        }
        private void ChassisTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chassisitempressed++;
                chassisitembuttonpressed++;
                if (M1_Global.chassisitemcounterBB == chassisitempressed)
                {
                    if ((string.IsNullOrEmpty(SuspendedMass.Text))      || (string.IsNullOrEmpty(SMCGx.Text))    || (string.IsNullOrEmpty(SMCGy.Text))    || (string.IsNullOrEmpty(SMCGz.Text))    ||
                        (string.IsNullOrEmpty(NonSuspendedMassFL.Text)) || (string.IsNullOrEmpty(NSMCGFLx.Text)) || (string.IsNullOrEmpty(NSMCGFLy.Text)) || (string.IsNullOrEmpty(NSMCGFLz.Text)) ||
                        (string.IsNullOrEmpty(NonSuspendedMassFR.Text)) || (string.IsNullOrEmpty(NSMCGFRx.Text)) || (string.IsNullOrEmpty(NSMCGFRy.Text)) || (string.IsNullOrEmpty(NSMCGFRz.Text)) ||
                        (string.IsNullOrEmpty(NonSuspendedMassRL.Text)) || (string.IsNullOrEmpty(NSMCGRLx.Text)) || (string.IsNullOrEmpty(NSMCGRLy.Text)) || (string.IsNullOrEmpty(NSMCGRLz.Text)) ||
                        (string.IsNullOrEmpty(NonSuspendedMassRR.Text)) || (string.IsNullOrEmpty(NSMCGRRx.Text)) || (string.IsNullOrEmpty(NSMCGRRy.Text)) || (string.IsNullOrEmpty(NSMCGRRz.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!string.IsNullOrEmpty(SuspendedMass.Text))     || (!string.IsNullOrEmpty(SMCGx.Text))    || (!string.IsNullOrEmpty(SMCGy.Text))    || (!string.IsNullOrEmpty(SMCGz.Text))    ||
                            (!string.IsNullOrEmpty(NonSuspendedMassFL.Text)) || (!string.IsNullOrEmpty(NSMCGFLx.Text)) || (!string.IsNullOrEmpty(NSMCGFLy.Text)) || (!string.IsNullOrEmpty(NSMCGFLz.Text)) ||
                            (!string.IsNullOrEmpty(NonSuspendedMassFR.Text)) || (!string.IsNullOrEmpty(NSMCGFRx.Text)) || (!string.IsNullOrEmpty(NSMCGFRy.Text)) || (!string.IsNullOrEmpty(NSMCGFRz.Text)) ||
                            (!string.IsNullOrEmpty(NonSuspendedMassRL.Text)) || (!string.IsNullOrEmpty(NSMCGRLx.Text)) || (!string.IsNullOrEmpty(NSMCGRLy.Text)) || (!string.IsNullOrEmpty(NSMCGRLz.Text)) ||
                            (!string.IsNullOrEmpty(NonSuspendedMassRR.Text)) || (!string.IsNullOrEmpty(NSMCGRRx.Text)) || (!string.IsNullOrEmpty(NSMCGRRy.Text)) || (!string.IsNullOrEmpty(NSMCGRRz.Text)))
                    {
                        for (int l_chassis = 0; l_chassis <= M1_Global.Assy_List_Chassis.Count; l_chassis++)
                        {
                            R1_New = this;
                            Chassis chassis_list = new Chassis(R1_New);
                            if (M1_Global.chassisitemcounterNB == l_chassis)
                            {
                                M1_Global.Assy_List_Chassis.Insert(l_chassis, chassis_list);
                                M1_Global.Assy_List_Chassis[l_chassis]._ChassisName = "Chassis " + Convert.ToString(l_chassis + 1);
                                MessageBox.Show("Chassis Item " + M1_Global.Assy_List_Chassis[l_chassis]._ChassisName + " has been created");

                                comboBoxChassis.Items.Insert(l_chassis, M1_Global.Assy_List_Chassis[l_chassis]);
                                comboBoxChassis.DisplayMember = "_ChassisName";

                                navBarItemChassis[l_chassis].Enabled = true;
                                M1_Global.chassisitemcounterNB++;
                                break;
                            }
                        }
                    }
                    chassisitempressed++;
                    chassisitembuttonpressed++;
                }
                chassisitempressed--;
                chassisitembuttonpressed--;
            }
        }
        #endregion

        //
        // Wheel Alignment Item Creation and GUI
        //
        #region Wheel Alignment Item Creation and GUI
        List<NavBarItem> navBarItemWA = new List<NavBarItem>();
        int waitempressed = 0; int waitembuttonpressed = 0;
        private void BarButtonWA_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.waitemcounterBB == waitempressed)
            {
                for (int i_wa = 0; i_wa <= navBarItemWA.Count; i_wa++)
                {
                    if (M1_Global.waitemcounterBB == i_wa)
                    {
                        navBarItemWA.Insert(i_wa, navBarControl2.Items.Add());
                        navBarItemWA[i_wa].Caption = "WA " + Convert.ToString(M1_Global.waitemcounterBB + 1);
                        navBarGroupWheelAlignment.ItemLinks.Add(navBarItemWA[i_wa]);
                        navBarItemWA[i_wa].LinkClicked += new NavBarLinkEventHandler(navBarItemWA_LinkClicked);
                        navBarItemWA[i_wa].Enabled = false;
                        M1_Global.waitemcounterBB++;
                        break;
                    }
                }
            }
        }
        void navBarItemWA_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupWheelAlignment.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_wa = 0; c_wa <= navBarItemWA.Count; c_wa++)
            {
                if (index == c_wa)
                {
                    StaticCamberFL.Text = Convert.ToString(M1_Global.Assy_List_WA[c_wa].StaticCamber);
                    StaticToeFL.Text = Convert.ToString(M1_Global.Assy_List_WA[c_wa].StaticToe);
                }
            }
        }
        private void CreateNewWA_Click(object sender, EventArgs e)
        {
            waitembuttonpressed++;
            waitempressed++;
            if (M1_Global.waitemcounterBB == waitembuttonpressed)
            {
                for (int l_wa = 0; l_wa <= M1_Global.Assy_List_WA.Count; l_wa++)
                {
                    R1_New = this;
                    WheelAlignment wa_list = new WheelAlignment(R1_New);
                    if (M1_Global.waitemcounterNB == l_wa)
                    {
                        M1_Global.Assy_List_WA.Insert(l_wa, wa_list);
                        M1_Global.Assy_List_WA[l_wa]._WAName = "WA " + Convert.ToString(l_wa + 1);
                        MessageBox.Show("Wheel Alignment Item " + M1_Global.Assy_List_WA[l_wa]._WAName + " has been created");

                        comboBoxWAFL.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                        comboBoxWAFL.DisplayMember = "_WAName";

                        comboBoxWAFR.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                        comboBoxWAFR.DisplayMember = "_WAName";

                        comboBoxWARL.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                        comboBoxWARL.DisplayMember = "_WAName";

                        comboBoxWARR.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                        comboBoxWARR.DisplayMember = "_WAName";

                        navBarItemWA[l_wa].Enabled = true;
                        M1_Global.waitemcounterNB++;
                        break;
                    }
                }
                waitembuttonpressed++;
                waitempressed++;
            }
            waitembuttonpressed--;
            waitempressed--;
        }
        private void WATextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                waitempressed++;
                waitembuttonpressed++;
                if (M1_Global.waitemcounterBB == waitempressed)
                {
                    if ((string.IsNullOrEmpty(StaticCamberFL.Text)) || (string.IsNullOrEmpty(StaticToeFL.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!string.IsNullOrEmpty(StaticCamberFL.Text)) || (!string.IsNullOrEmpty(StaticToeFL.Text)))
                    {
                        for (int l_wa = 0; l_wa <= M1_Global.Assy_List_WA.Count; l_wa++)
                        {
                            R1_New = this;
                            WheelAlignment wa_list = new WheelAlignment(R1_New);
                            if (M1_Global.waitemcounterNB == l_wa)
                            {
                                M1_Global.Assy_List_WA.Insert(l_wa, wa_list);
                                M1_Global.Assy_List_WA[l_wa]._WAName = "WA " + Convert.ToString(l_wa + 1);
                                MessageBox.Show("Wheel Alignment Item " + M1_Global.Assy_List_WA[l_wa]._WAName + " has been created");

                                comboBoxWAFL.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                                comboBoxWAFL.DisplayMember = "_WAName";

                                comboBoxWAFR.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                                comboBoxWAFR.DisplayMember = "_WAName";

                                comboBoxWARL.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                                comboBoxWARL.DisplayMember = "_WAName";

                                comboBoxWARR.Items.Insert(l_wa, M1_Global.Assy_List_WA[l_wa]);
                                comboBoxWARR.DisplayMember = "_WAName";

                                navBarItemWA[l_wa].Enabled = true;
                                M1_Global.waitemcounterNB++;
                                break;
                            }
                        }
                    }
                    waitempressed++;
                    waitembuttonpressed++;
                }
                waitempressed--;
                waitembuttonpressed--;
            }
        }
        #endregion

        //
        //Front Left Suspension Coordinate Item Creation and GUI
        //
        #region Front Left Suspension Coordinate Item Creation and GUI
        List<NavBarItem> navBarItemSCFL = new List<NavBarItem>();
        int susFLitempressed = 0; int susFLitembuttonpressed = 0;
        private void BarButtonSCFL_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
            accordionControlSuspensionCoorindatesFL.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionFL.Expanded = true;
            accordionControlSuspensionCoorindatesFL.ExpandElement(accordionControlFixedPointsFL);
            accordionControlSuspensionCoorindatesFL.ExpandElement(accordionControlMovingPointFL);
            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.susFLitemcounterBB == susFLitempressed)
            {
                for (int i_scfl = 0; i_scfl <= navBarItemSCFL.Count; i_scfl++)
                {
                    if (M1_Global.susFLitemcounterBB == i_scfl)
                    {
                        navBarItemSCFL.Insert(i_scfl, navBarControl2.Items.Add());
                        navBarItemSCFL[i_scfl].Caption = "Front Left Coordinates " + Convert.ToString(M1_Global.susFLitemcounterBB + 1);
                        navBarGroupSuspensionFL.ItemLinks.Add(navBarItemSCFL[i_scfl]);
                        navBarItemSCFL[i_scfl].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFL_LinkClicked);
                        navBarItemSCFL[i_scfl].Enabled = false;
                        M1_Global.susFLitemcounterBB++;
                        break;
                    }
                }
            }
        }

        void navBarItemSCFL_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            int index = navBarGroupSuspensionFL.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
            accordionControlSuspensionCoorindatesFL.Show();
            navBarGroupDesign.Visible = true;
            navBarGroupSuspensionFL.Expanded = true;
            accordionControlSuspensionCoorindatesFL.ExpandElement(accordionControlFixedPointsFL);
            accordionControlSuspensionCoorindatesFL.ExpandElement(accordionControlMovingPointFL);
            #endregion

            for (int c_scfl = 0; c_scfl <= navBarItemSCFL.Count; c_scfl++)
            {
                if (index == c_scfl)
                {
                    #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                    A1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].A1x);
                    A1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].A1y);
                    A1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].A1z);

                    B1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].B1x);
                    B1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].B1y);
                    B1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].B1z);

                    C1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].C1x);
                    C1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].C1y);
                    C1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].C1z);

                    D1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].D1x);
                    D1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].D1y);
                    D1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].D1z);

                    E1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].E1x);
                    E1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].E1y);
                    E1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].E1z);

                    F1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].F1x);
                    F1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].F1y);
                    F1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].F1z);

                    G1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].G1x);
                    G1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].G1y);
                    G1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].G1z);

                    H1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].H1x);
                    H1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].H1y);
                    H1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].H1z);

                    I1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].I1x);
                    I1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].I1y);
                    I1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].I1z);

                    J1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].J1x);
                    J1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].J1y);
                    J1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].J1z);

                    JO1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].JO1x);
                    JO1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].JO1y);
                    JO1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].JO1z);

                    K1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].K1x);
                    K1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].K1y);
                    K1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].K1z);

                    M1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].M1x);
                    M1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].M1y);
                    M1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].M1z);

                    N1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].N1x);
                    N1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].N1y);
                    N1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].N1z);

                    O1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].O1x);
                    O1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].O1y);
                    O1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].O1z);

                    P1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].P1x);
                    P1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].P1y);
                    P1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].P1z);

                    Q1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].Q1x);
                    Q1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].Q1y);
                    Q1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].Q1z);

                    R1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].R1x);
                    R1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].R1y);
                    R1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].R1z);

                    W1xFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].W1x);
                    W1yFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].W1y);
                    W1zFL.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].W1z);

                    RideHeightRefFLx.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].RideHeightRefx);
                    RideHeightRefFLy.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].RideHeightRefy);
                    RideHeightRefFLz.Text = Convert.ToString(M1_Global.Assy_List_SCFL[c_scfl].RideHeightRefz);
                    #endregion

                }
            }
            
        }
        private void CreateNewSCFL_Click(object sender, EventArgs e)
        {
            susFLitembuttonpressed++;
            susFLitempressed++;
            if (M1_Global.susFLitemcounterBB == susFLitembuttonpressed)
            {
                for (int l_scfl = 0; l_scfl <= M1_Global.Assy_List_SCFL.Count; l_scfl++)
                {
                    R1_New = this;
                    SuspensionCoordinatesFront scfl_list = new SuspensionCoordinatesFront(R1_New);
                    if (M1_Global.susFLitemcounterNB == l_scfl)
                    {
                        M1_Global.Assy_List_SCFL.Insert(l_scfl, scfl_list);
                        M1_Global.Assy_List_SCFL[l_scfl]._SCFLName = "Front Left Coordinates " + Convert.ToString(l_scfl + 1);
                        MessageBox.Show("Front Left Coordinates Item " + M1_Global.Assy_List_SCFL[l_scfl]._SCFLName + " has been created");

                        comboBoxSCFL.Items.Insert(l_scfl, M1_Global.Assy_List_SCFL[l_scfl]);
                        comboBoxSCFL.DisplayMember = "_SCFLName";

                        navBarItemSCFL[l_scfl].Enabled = true;
                        M1_Global.susFLitemcounterNB++;
                        break;
                    }
                }
                susFLitembuttonpressed++;
                susFLitempressed++;
            }
            susFLitembuttonpressed--;
            susFLitempressed--;
        }
        private void SCFLTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                susFLitempressed++;
                susFLitembuttonpressed++;
                if (M1_Global.susFLitemcounterBB == susFLitempressed)
                {
                    if ((string.IsNullOrEmpty(A1xFL.Text))||(string.IsNullOrEmpty(A1yFL.Text))||(string.IsNullOrEmpty(A1zFL.Text))||
                        (string.IsNullOrEmpty(B1xFL.Text))||(string.IsNullOrEmpty(B1yFL.Text))||(string.IsNullOrEmpty(B1zFL.Text))||
                        (string.IsNullOrEmpty(C1xFL.Text))||(string.IsNullOrEmpty(C1yFL.Text))||(string.IsNullOrEmpty(C1zFL.Text))||
                        (string.IsNullOrEmpty(D1xFL.Text))||(string.IsNullOrEmpty(D1yFL.Text))||(string.IsNullOrEmpty(D1zFL.Text))||
                        (string.IsNullOrEmpty(E1xFL.Text))||(string.IsNullOrEmpty(E1yFL.Text))||(string.IsNullOrEmpty(E1zFL.Text))||
                        (string.IsNullOrEmpty(F1xFL.Text))||(string.IsNullOrEmpty(F1yFL.Text))||(string.IsNullOrEmpty(F1zFL.Text))||
                        (string.IsNullOrEmpty(G1xFL.Text))||(string.IsNullOrEmpty(G1yFL.Text))||(string.IsNullOrEmpty(G1zFL.Text))||
                        (string.IsNullOrEmpty(H1xFL.Text))||(string.IsNullOrEmpty(H1yFL.Text))||(string.IsNullOrEmpty(H1zFL.Text))||
                        (string.IsNullOrEmpty(I1xFL.Text))||(string.IsNullOrEmpty(I1yFL.Text))||(string.IsNullOrEmpty(I1zFL.Text))||
                        (string.IsNullOrEmpty(J1xFL.Text))||(string.IsNullOrEmpty(J1yFL.Text))||(string.IsNullOrEmpty(J1zFL.Text))||
                        (string.IsNullOrEmpty(JO1xFL.Text))||(string.IsNullOrEmpty(JO1yFL.Text))||(string.IsNullOrEmpty(JO1zFL.Text))||
                        (string.IsNullOrEmpty(K1xFL.Text))||(string.IsNullOrEmpty(K1yFL.Text))||(string.IsNullOrEmpty(K1zFL.Text))||
                        (string.IsNullOrEmpty(M1xFL.Text))||(string.IsNullOrEmpty(M1yFL.Text))||(string.IsNullOrEmpty(M1zFL.Text))||
                        (string.IsNullOrEmpty(N1xFL.Text))||(string.IsNullOrEmpty(N1yFL.Text))||(string.IsNullOrEmpty(N1zFL.Text))||
                        (string.IsNullOrEmpty(O1xFL.Text))||(string.IsNullOrEmpty(O1yFL.Text))||(string.IsNullOrEmpty(O1zFL.Text))||
                        (string.IsNullOrEmpty(P1xFL.Text))||(string.IsNullOrEmpty(P1yFL.Text))||(string.IsNullOrEmpty(P1zFL.Text))||
                        (string.IsNullOrEmpty(Q1xFL.Text))||(string.IsNullOrEmpty(Q1yFL.Text))||(string.IsNullOrEmpty(Q1zFL.Text))||
                        (string.IsNullOrEmpty(R1xFL.Text))||(string.IsNullOrEmpty(R1yFL.Text))||(string.IsNullOrEmpty(R1zFL.Text))||
                        (string.IsNullOrEmpty(W1xFL.Text))||(string.IsNullOrEmpty(W1yFL.Text))||(string.IsNullOrEmpty(W1zFL.Text))||
                        (string.IsNullOrEmpty(RideHeightRefFLx.Text)) || (string.IsNullOrEmpty(RideHeightRefFLy.Text)) || (string.IsNullOrEmpty(RideHeightRefFLz.Text)))

                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if((!!string.IsNullOrEmpty(A1xFL.Text))|| (!string.IsNullOrEmpty(A1yFL.Text)) || (!string.IsNullOrEmpty(A1zFL.Text)) ||
                            (!string.IsNullOrEmpty(B1xFL.Text)) || (!string.IsNullOrEmpty(B1yFL.Text)) || (!string.IsNullOrEmpty(B1zFL.Text)) ||
                            (!string.IsNullOrEmpty(C1xFL.Text)) || (!string.IsNullOrEmpty(C1yFL.Text)) || (!string.IsNullOrEmpty(C1zFL.Text)) ||
                            (!string.IsNullOrEmpty(D1xFL.Text)) || (!string.IsNullOrEmpty(D1yFL.Text)) || (!string.IsNullOrEmpty(D1zFL.Text)) ||
                            (!string.IsNullOrEmpty(E1xFL.Text)) || (!string.IsNullOrEmpty(E1yFL.Text)) || (!string.IsNullOrEmpty(E1zFL.Text)) ||
                            (!string.IsNullOrEmpty(F1xFL.Text)) || (!string.IsNullOrEmpty(F1yFL.Text)) || (!string.IsNullOrEmpty(F1zFL.Text)) ||
                            (!string.IsNullOrEmpty(G1xFL.Text)) || (!string.IsNullOrEmpty(G1yFL.Text)) || (!string.IsNullOrEmpty(G1zFL.Text)) ||
                            (!string.IsNullOrEmpty(H1xFL.Text)) || (!string.IsNullOrEmpty(H1yFL.Text)) || (!string.IsNullOrEmpty(H1zFL.Text)) ||
                            (!string.IsNullOrEmpty(I1xFL.Text)) || (!string.IsNullOrEmpty(I1yFL.Text)) || (!string.IsNullOrEmpty(I1zFL.Text)) ||
                            (!string.IsNullOrEmpty(J1xFL.Text)) || (!string.IsNullOrEmpty(J1yFL.Text)) || (!string.IsNullOrEmpty(J1zFL.Text)) ||
                            (!string.IsNullOrEmpty(JO1xFL.Text))|| (!string.IsNullOrEmpty(JO1yFL.Text))|| (!string.IsNullOrEmpty(JO1zFL.Text))||
                            (!string.IsNullOrEmpty(K1xFL.Text)) || (!string.IsNullOrEmpty(K1yFL.Text)) || (!string.IsNullOrEmpty(K1zFL.Text)) ||
                            (!string.IsNullOrEmpty(M1xFL.Text)) || (!string.IsNullOrEmpty(M1yFL.Text)) || (!string.IsNullOrEmpty(M1zFL.Text)) ||
                            (!string.IsNullOrEmpty(N1xFL.Text)) || (!string.IsNullOrEmpty(N1yFL.Text)) || (!string.IsNullOrEmpty(N1zFL.Text)) ||
                            (!string.IsNullOrEmpty(O1xFL.Text)) || (!string.IsNullOrEmpty(O1yFL.Text)) || (!string.IsNullOrEmpty(O1zFL.Text)) ||
                            (!string.IsNullOrEmpty(P1xFL.Text)) || (!string.IsNullOrEmpty(P1yFL.Text)) || (!string.IsNullOrEmpty(P1zFL.Text)) ||
                            (!string.IsNullOrEmpty(Q1xFL.Text)) || (!string.IsNullOrEmpty(Q1yFL.Text)) || (!string.IsNullOrEmpty(Q1zFL.Text)) ||
                            (!string.IsNullOrEmpty(R1xFL.Text)) || (!string.IsNullOrEmpty(R1yFL.Text)) || (!string.IsNullOrEmpty(R1zFL.Text)) ||
                            (!string.IsNullOrEmpty(W1xFL.Text)) || (!string.IsNullOrEmpty(W1yFL.Text)) || (!string.IsNullOrEmpty(W1zFL.Text)) ||
                            (!string.IsNullOrEmpty(RideHeightRefFLx.Text)) || (!string.IsNullOrEmpty(RideHeightRefFLy.Text)) || (!string.IsNullOrEmpty(RideHeightRefFLz.Text)))
                    {
                        for (int l_scfl = 0; l_scfl <= M1_Global.Assy_List_SCFL.Count; l_scfl++)
                        {
                            R1 = this;
                            SuspensionCoordinatesFront scfl_list = new SuspensionCoordinatesFront(R1);
                            if (M1_Global.susFLitemcounterNB == l_scfl)
                            {
                                M1_Global.Assy_List_SCFL.Insert(l_scfl, scfl_list);
                                M1_Global.Assy_List_SCFL[l_scfl]._SCFLName = "Front Left Coordinates " + Convert.ToString(l_scfl + 1);
                                MessageBox.Show("Front Left Coordinates Item " + M1_Global.Assy_List_SCFL[l_scfl]._SCFLName + " has been created");

                                comboBoxSCFL.Items.Insert(l_scfl, M1_Global.Assy_List_SCFL[l_scfl]);
                                comboBoxSCFL.DisplayMember = "_SCFLName";

                                navBarItemSCFL[l_scfl].Enabled = true;
                                M1_Global.susFLitemcounterNB++;
                                break;
                            }
                        }
                    }
                    susFLitempressed++;
                    susFLitembuttonpressed++;
                }
                susFLitempressed--;
                susFLitembuttonpressed--;
            }
        }
        #endregion

        //
        //Front Right Suspension Coordinate Item Creation and GUI
        //
        #region Front Right Suspension Coordinate Item Creation and GUI
        List<NavBarItem> navBarItemSCFR = new List<NavBarItem>();
        int susFRitempressed = 0; int susFRitembuttonpressed = 0;
        private void BarButtonSCFR_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.susFRitemcounterBB == susFRitempressed)
            {
                for (int i_scfr = 0; i_scfr <= navBarItemSCFR.Count; i_scfr++)
                {
                    if (M1_Global.susFRitemcounterBB == i_scfr)
                    {
                        navBarItemSCFR.Insert(i_scfr, navBarControl2.Items.Add());
                        navBarItemSCFR[i_scfr].Caption = "Front Right Coordinates " + Convert.ToString(M1_Global.susFRitemcounterBB + 1);
                        navBarGroupSuspensionFR.ItemLinks.Add(navBarItemSCFR[i_scfr]);
                        navBarItemSCFR[i_scfr].LinkClicked += new NavBarLinkEventHandler(navBarItemSCFR_LinkClicked);
                        navBarItemSCFR[i_scfr].Enabled = false;
                        M1_Global.susFRitemcounterBB++;
                        break;
                    }
                }
            }
        }

        void navBarItemSCFR_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

            int index = navBarGroupSuspensionFR.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_scfr = 0; c_scfr <= navBarItemSCFR.Count; c_scfr++)
            {
                if (index == c_scfr)
                {
                    #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                    A1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].A1x);
                    A1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].A1y);
                    A1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].A1z);

                    B1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].B1x);
                    B1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].B1y);
                    B1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].B1z);

                    C1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].C1x);
                    C1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].C1y);
                    C1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].C1z);

                    D1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].D1x);
                    D1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].D1y);
                    D1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].D1z);

                    E1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].E1x);
                    E1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].E1y);
                    E1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].E1z);

                    F1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].F1x);
                    F1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].F1y);
                    F1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].F1z);

                    G1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].G1x);
                    G1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].G1y);
                    G1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].G1z);

                    H1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].H1x);
                    H1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].H1y);
                    H1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].H1z);

                    I1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].I1x);
                    I1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].I1y);
                    I1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].I1z);

                    J1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].J1x);
                    J1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].J1y);
                    J1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].J1z);

                    JO1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].JO1x);
                    JO1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].JO1y);
                    JO1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].JO1z);

                    K1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].K1x);
                    K1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].K1y);
                    K1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].K1z);

                    M1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].M1x);
                    M1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].M1y);
                    M1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].M1z);

                    N1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].N1x);
                    N1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].N1y);
                    N1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].N1z);

                    O1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].O1x);
                    O1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].O1y);
                    O1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].O1z);

                    P1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].P1x);
                    P1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].P1y);
                    P1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].P1z);

                    Q1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].Q1x);
                    Q1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].Q1y);
                    Q1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].Q1z);

                    R1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].R1x);
                    R1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].R1y);
                    R1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].R1z);

                    W1xFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].W1x);
                    W1yFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].W1y);
                    W1zFR.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].W1z);

                    RideHeightFRxRef.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].RideHeightRefx);
                    RideHeightRefFRy.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].RideHeightRefy);
                    RideHeightRefFRz.Text = Convert.ToString(M1_Global.Assy_List_SCFR[c_scfr].RideHeightRefz);
                    #endregion

                }
            }
        }
        private void CreateNewSCFR_Click(object sender, EventArgs e)
        {
            susFRitembuttonpressed++;
            susFRitempressed++;
            if (M1_Global.susFRitemcounterBB == susFRitembuttonpressed)
            {
                for (int l_scfr = 0; l_scfr <= M1_Global.Assy_List_SCFR.Count; l_scfr++)
                {
                    R1 = this;
                    SuspensionCoordinatesFrontRight scfr_list = new SuspensionCoordinatesFrontRight(R1);
                    if (M1_Global.susFRitemcounterNB == l_scfr)
                    {
                        M1_Global.Assy_List_SCFR.Insert(l_scfr, scfr_list);
                        M1_Global.Assy_List_SCFR[l_scfr]._SCFRName = "Front Right Coordinates " + Convert.ToString(l_scfr + 1);
                        MessageBox.Show("Front Right Coordinates Item " + M1_Global.Assy_List_SCFR[l_scfr]._SCFRName + " has been created");

                        comboBoxSCFR.Items.Insert(l_scfr, M1_Global.Assy_List_SCFR[l_scfr]);
                        comboBoxSCFR.DisplayMember = "_SCFRName";

                        navBarItemSCFR[l_scfr].Enabled = true;
                        M1_Global.susFRitemcounterNB++;
                        break;
                    }
                }
                susFRitembuttonpressed++;
                susFRitempressed++;
            }
            susFRitembuttonpressed--;
            susFRitempressed--;
        }
            
        private void SCFRTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                susFRitempressed++;
                susFRitembuttonpressed++;
                if (M1_Global.susFRitemcounterBB == susFRitempressed)
                {
                    if ((string.IsNullOrEmpty(A1xFR.Text)) || (string.IsNullOrEmpty(A1yFR.Text)) || (string.IsNullOrEmpty(A1zFR.Text)) ||
                        (string.IsNullOrEmpty(B1xFR.Text)) || (string.IsNullOrEmpty(B1yFR.Text)) || (string.IsNullOrEmpty(B1zFR.Text)) ||
                        (string.IsNullOrEmpty(C1xFR.Text)) || (string.IsNullOrEmpty(C1yFR.Text)) || (string.IsNullOrEmpty(C1zFR.Text)) ||
                        (string.IsNullOrEmpty(D1xFR.Text)) || (string.IsNullOrEmpty(D1yFR.Text)) || (string.IsNullOrEmpty(D1zFR.Text)) ||
                        (string.IsNullOrEmpty(E1xFR.Text)) || (string.IsNullOrEmpty(E1yFR.Text)) || (string.IsNullOrEmpty(E1zFR.Text)) ||
                        (string.IsNullOrEmpty(F1xFR.Text)) || (string.IsNullOrEmpty(F1yFR.Text)) || (string.IsNullOrEmpty(F1zFR.Text)) ||
                        (string.IsNullOrEmpty(G1xFR.Text)) || (string.IsNullOrEmpty(G1yFR.Text)) || (string.IsNullOrEmpty(G1zFR.Text)) ||
                        (string.IsNullOrEmpty(H1xFR.Text)) || (string.IsNullOrEmpty(H1yFR.Text)) || (string.IsNullOrEmpty(H1zFR.Text)) ||
                        (string.IsNullOrEmpty(I1xFR.Text)) || (string.IsNullOrEmpty(I1yFR.Text)) || (string.IsNullOrEmpty(I1zFR.Text)) ||
                        (string.IsNullOrEmpty(J1xFR.Text)) || (string.IsNullOrEmpty(J1yFR.Text)) || (string.IsNullOrEmpty(J1zFR.Text)) ||
                        (string.IsNullOrEmpty(JO1xFR.Text)) || (string.IsNullOrEmpty(JO1yFR.Text)) || (string.IsNullOrEmpty(JO1zFR.Text)) ||
                        (string.IsNullOrEmpty(K1xFR.Text)) || (string.IsNullOrEmpty(K1yFR.Text)) || (string.IsNullOrEmpty(K1zFR.Text)) ||
                        (string.IsNullOrEmpty(M1xFR.Text)) || (string.IsNullOrEmpty(M1yFR.Text)) || (string.IsNullOrEmpty(M1zFR.Text)) ||
                        (string.IsNullOrEmpty(N1xFR.Text)) || (string.IsNullOrEmpty(N1yFR.Text)) || (string.IsNullOrEmpty(N1zFR.Text)) ||
                        (string.IsNullOrEmpty(O1xFR.Text)) || (string.IsNullOrEmpty(O1yFR.Text)) || (string.IsNullOrEmpty(O1zFR.Text)) ||
                        (string.IsNullOrEmpty(P1xFR.Text)) || (string.IsNullOrEmpty(P1yFR.Text)) || (string.IsNullOrEmpty(P1zFR.Text)) ||
                        (string.IsNullOrEmpty(Q1xFR.Text)) || (string.IsNullOrEmpty(Q1yFR.Text)) || (string.IsNullOrEmpty(Q1zFR.Text)) ||
                        (string.IsNullOrEmpty(R1xFR.Text)) || (string.IsNullOrEmpty(R1yFR.Text)) || (string.IsNullOrEmpty(R1zFR.Text)) ||
                        (string.IsNullOrEmpty(W1xFR.Text)) || (string.IsNullOrEmpty(W1yFR.Text)) || (string.IsNullOrEmpty(W1zFR.Text)) ||
                        (string.IsNullOrEmpty(RideHeightFRxRef.Text)) || (string.IsNullOrEmpty(RideHeightRefFRy.Text)) || (string.IsNullOrEmpty(RideHeightRefFRz.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!!string.IsNullOrEmpty(A1xFR.Text)) || (!string.IsNullOrEmpty(A1yFR.Text)) || (!string.IsNullOrEmpty(A1zFR.Text)) ||
                            (!string.IsNullOrEmpty(B1xFR.Text)) || (!string.IsNullOrEmpty(B1yFR.Text)) || (!string.IsNullOrEmpty(B1zFR.Text)) ||
                            (!string.IsNullOrEmpty(C1xFR.Text)) || (!string.IsNullOrEmpty(C1yFR.Text)) || (!string.IsNullOrEmpty(C1zFR.Text)) ||
                            (!string.IsNullOrEmpty(D1xFR.Text)) || (!string.IsNullOrEmpty(D1yFR.Text)) || (!string.IsNullOrEmpty(D1zFR.Text)) ||
                            (!string.IsNullOrEmpty(E1xFR.Text)) || (!string.IsNullOrEmpty(E1yFR.Text)) || (!string.IsNullOrEmpty(E1zFR.Text)) ||
                            (!string.IsNullOrEmpty(F1xFR.Text)) || (!string.IsNullOrEmpty(F1yFR.Text)) || (!string.IsNullOrEmpty(F1zFR.Text)) ||
                            (!string.IsNullOrEmpty(G1xFR.Text)) || (!string.IsNullOrEmpty(G1yFR.Text)) || (!string.IsNullOrEmpty(G1zFR.Text)) ||
                            (!string.IsNullOrEmpty(H1xFR.Text)) || (!string.IsNullOrEmpty(H1yFR.Text)) || (!string.IsNullOrEmpty(H1zFR.Text)) ||
                            (!string.IsNullOrEmpty(I1xFR.Text)) || (!string.IsNullOrEmpty(I1yFR.Text)) || (!string.IsNullOrEmpty(I1zFR.Text)) ||
                            (!string.IsNullOrEmpty(J1xFR.Text)) || (!string.IsNullOrEmpty(J1yFR.Text)) || (!string.IsNullOrEmpty(J1zFR.Text)) ||
                            (!string.IsNullOrEmpty(JO1xFR.Text)) || (!string.IsNullOrEmpty(JO1yFR.Text)) || (!string.IsNullOrEmpty(JO1zFR.Text)) ||
                            (!string.IsNullOrEmpty(K1xFR.Text)) || (!string.IsNullOrEmpty(K1yFR.Text)) || (!string.IsNullOrEmpty(K1zFR.Text)) ||
                            (!string.IsNullOrEmpty(M1xFR.Text)) || (!string.IsNullOrEmpty(M1yFR.Text)) || (!string.IsNullOrEmpty(M1zFR.Text)) ||
                            (!string.IsNullOrEmpty(N1xFR.Text)) || (!string.IsNullOrEmpty(N1yFR.Text)) || (!string.IsNullOrEmpty(N1zFR.Text)) ||
                            (!string.IsNullOrEmpty(O1xFR.Text)) || (!string.IsNullOrEmpty(O1yFR.Text)) || (!string.IsNullOrEmpty(O1zFR.Text)) ||
                            (!string.IsNullOrEmpty(P1xFR.Text)) || (!string.IsNullOrEmpty(P1yFR.Text)) || (!string.IsNullOrEmpty(P1zFR.Text)) ||
                            (!string.IsNullOrEmpty(Q1xFR.Text)) || (!string.IsNullOrEmpty(Q1yFR.Text)) || (!string.IsNullOrEmpty(Q1zFR.Text)) ||
                            (!string.IsNullOrEmpty(R1xFR.Text)) || (!string.IsNullOrEmpty(R1yFR.Text)) || (!string.IsNullOrEmpty(R1zFR.Text)) ||
                            (!string.IsNullOrEmpty(W1xFR.Text)) || (!string.IsNullOrEmpty(W1yFR.Text)) || (!string.IsNullOrEmpty(W1zFR.Text)) ||
                            (!string.IsNullOrEmpty(RideHeightFRxRef.Text)) || (!string.IsNullOrEmpty(RideHeightRefFRy.Text)) || (!string.IsNullOrEmpty(RideHeightRefFRz.Text)))
                    {
                        for (int l_scfr = 0; l_scfr <= M1_Global.Assy_List_SCFR.Count; l_scfr++)
                        {
                            R1 = this;
                            SuspensionCoordinatesFrontRight scfr_list = new SuspensionCoordinatesFrontRight(R1);
                            if (M1_Global.susFRitemcounterNB == l_scfr)
                            {
                                M1_Global.Assy_List_SCFR.Insert(l_scfr, scfr_list);
                                M1_Global.Assy_List_SCFR[l_scfr]._SCFRName = "Front Right Coordinates " + Convert.ToString(l_scfr + 1);
                                MessageBox.Show("Front Right Coordinates Item " + M1_Global.Assy_List_SCFR[l_scfr]._SCFRName + " has been created");

                                comboBoxSCFR.Items.Insert(l_scfr, M1_Global.Assy_List_SCFR[l_scfr]);
                                comboBoxSCFR.DisplayMember = "_SCFRName";

                                navBarItemSCFR[l_scfr].Enabled = true;
                                M1_Global.susFRitemcounterNB++;
                                break;
                            }
                        }
                    }
                    susFRitempressed++;
                    susFRitembuttonpressed++;
                }
                susFRitempressed--;
                susFRitembuttonpressed--;
            }
        }
        #endregion

        //
        //Rear Left Suspension Coordinate Item Creation and GUI
        //
        #region Rear Left Suspension Coordinate Item Creation and GUI
        List<NavBarItem> navBarItemSCRL = new List<NavBarItem>();
        int susRLitempressed = 0; int susRLitembuttonpressed = 0;
        private void BarButtonSCRL_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.susRLitemcounterBB == susRLitempressed)
            {
                for (int i_scrl = 0; i_scrl <= navBarItemSCRL.Count; i_scrl++)
                {
                    if (M1_Global.susRLitemcounterBB == i_scrl)
                    {
                        navBarItemSCRL.Insert(i_scrl, navBarControl2.Items.Add());
                        navBarItemSCRL[i_scrl].Caption = "Rear Left Coordinates " + Convert.ToString(M1_Global.susRLitemcounterBB + 1);
                        navBarGroupSuspensionRL.ItemLinks.Add(navBarItemSCRL[i_scrl]);
                        navBarItemSCRL[i_scrl].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRL_LinkClicked);
                        navBarItemSCRL[i_scrl].Enabled = false;
                        M1_Global.susRLitemcounterBB++;
                        break;
                    }
                }
            }
        }

        void navBarItemSCRL_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSuspensionRL.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

            for (int c_scrl = 0; c_scrl <= navBarItemSCRL.Count; c_scrl++)
            {
                if (index == c_scrl)
                {
                    #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                    A1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].A1x);
                    A1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].A1y);
                    A1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].A1z);

                    B1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].B1x);
                    B1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].B1y);
                    B1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].B1z);

                    C1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].C1x);
                    C1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].C1y);
                    C1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].C1z);

                    D1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].D1x);
                    D1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].D1y);
                    D1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].D1z);

                    E1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].E1x);
                    E1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].E1y);
                    E1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].E1z);

                    F1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].F1x);
                    F1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].F1y);
                    F1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].F1z);

                    G1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].G1x);
                    G1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].G1y);
                    G1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].G1z);

                    H1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].H1x);
                    H1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].H1y);
                    H1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].H1z);

                    I1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].I1x);
                    I1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].I1y);
                    I1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].I1z);

                    J1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].J1x);
                    J1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].J1y);
                    J1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].J1z);

                    JO1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].JO1x);
                    JO1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].JO1y);
                    JO1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].JO1z);

                    K1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].K1x);
                    K1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].K1y);
                    K1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].K1z);

                    M1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].M1x);
                    M1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].M1y);
                    M1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].M1z);

                    N1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].N1x);
                    N1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].N1y);
                    N1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].N1z);

                    O1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].O1x);
                    O1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].O1y);
                    O1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].O1z);

                    P1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].P1x);
                    P1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].P1y);
                    P1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].P1z);

                    Q1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].Q1x);
                    Q1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].Q1y);
                    Q1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].Q1z);

                    R1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].R1x);
                    R1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].R1y);
                    R1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].R1z);

                    W1xRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].W1x);
                    W1yRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].W1y);
                    W1zRL.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].W1z);

                    RideHeightRefRLx.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].RideHeightRefx);
                    RideHeightRefRLy.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].RideHeightRefy);
                    RideHeightRefRLz.Text = Convert.ToString(M1_Global.Assy_List_SCRL[c_scrl].RideHeightRefz);
                    #endregion

                }
            }

        }
        private void CreateNewSCRL_Click(object sender, EventArgs e)
        {
            susRLitembuttonpressed++;
            susRLitempressed++;
            if (M1_Global.susRLitemcounterBB == susRLitembuttonpressed)
            {
                for (int l_scrl = 0; l_scrl <= M1_Global.Assy_List_SCRL.Count; l_scrl++)
                {
                    R1 = this;
                    SuspensionCoordinatesRear scrl_list = new SuspensionCoordinatesRear(R1);
                    if (M1_Global.susRLitemcounterNB == l_scrl)
                    {
                        M1_Global.Assy_List_SCRL.Insert(l_scrl, scrl_list);
                        M1_Global.Assy_List_SCRL[l_scrl]._SCRLName = "Rear Left Coordinates " + Convert.ToString(l_scrl + 1);
                        MessageBox.Show("Rear Left Coordinates Item " + M1_Global.Assy_List_SCRL[l_scrl]._SCRLName + " has been created");

                        comboBoxSCRL.Items.Insert(l_scrl, M1_Global.Assy_List_SCRL[l_scrl]);
                        comboBoxSCRL.DisplayMember = "_SCRLName";

                        navBarItemSCRL[l_scrl].Enabled = true;
                        M1_Global.susRLitemcounterNB++;
                        break;
                    }
                }
                susRLitembuttonpressed++;
                susRLitempressed++;
            }
            susRLitembuttonpressed--;
            susRLitempressed--;
        }
        private void SCRLTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                susRLitempressed++;
                susRLitembuttonpressed++;
                if (M1_Global.susRLitemcounterBB == susRLitempressed)
                {
                    if ((string.IsNullOrEmpty(A1xRL.Text)) || (string.IsNullOrEmpty(A1yRL.Text)) || (string.IsNullOrEmpty(A1zRL.Text)) ||
                        (string.IsNullOrEmpty(B1xRL.Text)) || (string.IsNullOrEmpty(B1yRL.Text)) || (string.IsNullOrEmpty(B1zRL.Text)) ||
                        (string.IsNullOrEmpty(C1xRL.Text)) || (string.IsNullOrEmpty(C1yRL.Text)) || (string.IsNullOrEmpty(C1zRL.Text)) ||
                        (string.IsNullOrEmpty(D1xRL.Text)) || (string.IsNullOrEmpty(D1yRL.Text)) || (string.IsNullOrEmpty(D1zRL.Text)) ||
                        (string.IsNullOrEmpty(E1xRL.Text)) || (string.IsNullOrEmpty(E1yRL.Text)) || (string.IsNullOrEmpty(E1zRL.Text)) ||
                        (string.IsNullOrEmpty(F1xRL.Text)) || (string.IsNullOrEmpty(F1yRL.Text)) || (string.IsNullOrEmpty(F1zRL.Text)) ||
                        (string.IsNullOrEmpty(G1xRL.Text)) || (string.IsNullOrEmpty(G1yRL.Text)) || (string.IsNullOrEmpty(G1zRL.Text)) ||
                        (string.IsNullOrEmpty(H1xRL.Text)) || (string.IsNullOrEmpty(H1yRL.Text)) || (string.IsNullOrEmpty(H1zRL.Text)) ||
                        (string.IsNullOrEmpty(I1xRL.Text)) || (string.IsNullOrEmpty(I1yRL.Text)) || (string.IsNullOrEmpty(I1zRL.Text)) ||
                        (string.IsNullOrEmpty(J1xRL.Text)) || (string.IsNullOrEmpty(J1yRL.Text)) || (string.IsNullOrEmpty(J1zRL.Text)) ||
                        (string.IsNullOrEmpty(JO1xRL.Text)) || (string.IsNullOrEmpty(JO1yRL.Text)) || (string.IsNullOrEmpty(JO1zRL.Text)) ||
                        (string.IsNullOrEmpty(K1xRL.Text)) || (string.IsNullOrEmpty(K1yRL.Text)) || (string.IsNullOrEmpty(K1zRL.Text)) ||
                        (string.IsNullOrEmpty(M1xRL.Text)) || (string.IsNullOrEmpty(M1yRL.Text)) || (string.IsNullOrEmpty(M1zRL.Text)) ||
                        (string.IsNullOrEmpty(N1xRL.Text)) || (string.IsNullOrEmpty(N1yRL.Text)) || (string.IsNullOrEmpty(N1zRL.Text)) ||
                        (string.IsNullOrEmpty(O1xRL.Text)) || (string.IsNullOrEmpty(O1yRL.Text)) || (string.IsNullOrEmpty(O1zRL.Text)) ||
                        (string.IsNullOrEmpty(P1xRL.Text)) || (string.IsNullOrEmpty(P1yRL.Text)) || (string.IsNullOrEmpty(P1zRL.Text)) ||
                        (string.IsNullOrEmpty(Q1xRL.Text)) || (string.IsNullOrEmpty(Q1yRL.Text)) || (string.IsNullOrEmpty(Q1zRL.Text)) ||
                        (string.IsNullOrEmpty(R1xRL.Text)) || (string.IsNullOrEmpty(R1yRL.Text)) || (string.IsNullOrEmpty(R1zRL.Text)) ||
                        (string.IsNullOrEmpty(W1xRL.Text)) || (string.IsNullOrEmpty(W1yRL.Text)) || (string.IsNullOrEmpty(W1zRL.Text)) ||
                        (string.IsNullOrEmpty(RideHeightRefRLx.Text)) || (string.IsNullOrEmpty(RideHeightRefRLy.Text)) || (string.IsNullOrEmpty(RideHeightRefRLz.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!!string.IsNullOrEmpty(A1xRL.Text)) || (!string.IsNullOrEmpty(A1yRL.Text)) || (!string.IsNullOrEmpty(A1zRL.Text)) ||
                            (!string.IsNullOrEmpty(B1xRL.Text)) || (!string.IsNullOrEmpty(B1yRL.Text)) || (!string.IsNullOrEmpty(B1zRL.Text)) ||
                            (!string.IsNullOrEmpty(C1xRL.Text)) || (!string.IsNullOrEmpty(C1yRL.Text)) || (!string.IsNullOrEmpty(C1zRL.Text)) ||
                            (!string.IsNullOrEmpty(D1xRL.Text)) || (!string.IsNullOrEmpty(D1yRL.Text)) || (!string.IsNullOrEmpty(D1zRL.Text)) ||
                            (!string.IsNullOrEmpty(E1xRL.Text)) || (!string.IsNullOrEmpty(E1yRL.Text)) || (!string.IsNullOrEmpty(E1zRL.Text)) ||
                            (!string.IsNullOrEmpty(F1xRL.Text)) || (!string.IsNullOrEmpty(F1yRL.Text)) || (!string.IsNullOrEmpty(F1zRL.Text)) ||
                            (!string.IsNullOrEmpty(G1xRL.Text)) || (!string.IsNullOrEmpty(G1yRL.Text)) || (!string.IsNullOrEmpty(G1zRL.Text)) ||
                            (!string.IsNullOrEmpty(H1xRL.Text)) || (!string.IsNullOrEmpty(H1yRL.Text)) || (!string.IsNullOrEmpty(H1zRL.Text)) ||
                            (!string.IsNullOrEmpty(I1xRL.Text)) || (!string.IsNullOrEmpty(I1yRL.Text)) || (!string.IsNullOrEmpty(I1zRL.Text)) ||
                            (!string.IsNullOrEmpty(J1xRL.Text)) || (!string.IsNullOrEmpty(J1yRL.Text)) || (!string.IsNullOrEmpty(J1zRL.Text)) ||
                            (!string.IsNullOrEmpty(JO1xRL.Text)) || (!string.IsNullOrEmpty(JO1yRL.Text)) || (!string.IsNullOrEmpty(JO1zRL.Text)) ||
                            (!string.IsNullOrEmpty(K1xRL.Text)) || (!string.IsNullOrEmpty(K1yRL.Text)) || (!string.IsNullOrEmpty(K1zRL.Text)) ||
                            (!string.IsNullOrEmpty(M1xRL.Text)) || (!string.IsNullOrEmpty(M1yRL.Text)) || (!string.IsNullOrEmpty(M1zRL.Text)) ||
                            (!string.IsNullOrEmpty(N1xRL.Text)) || (!string.IsNullOrEmpty(N1yRL.Text)) || (!string.IsNullOrEmpty(N1zRL.Text)) ||
                            (!string.IsNullOrEmpty(O1xRL.Text)) || (!string.IsNullOrEmpty(O1yRL.Text)) || (!string.IsNullOrEmpty(O1zRL.Text)) ||
                            (!string.IsNullOrEmpty(P1xRL.Text)) || (!string.IsNullOrEmpty(P1yRL.Text)) || (!string.IsNullOrEmpty(P1zRL.Text)) ||
                            (!string.IsNullOrEmpty(Q1xRL.Text)) || (!string.IsNullOrEmpty(Q1yRL.Text)) || (!string.IsNullOrEmpty(Q1zRL.Text)) ||
                            (!string.IsNullOrEmpty(R1xRL.Text)) || (!string.IsNullOrEmpty(R1yRL.Text)) || (!string.IsNullOrEmpty(R1zRL.Text)) ||
                            (!string.IsNullOrEmpty(W1xRL.Text)) || (!string.IsNullOrEmpty(W1yRL.Text)) || (!string.IsNullOrEmpty(W1zRL.Text)) ||
                            (!string.IsNullOrEmpty(RideHeightRefRLx.Text)) || (!string.IsNullOrEmpty(RideHeightRefRLy.Text)) || (!string.IsNullOrEmpty(RideHeightRefRLz.Text)))
                    {
                        for (int l_scrl = 0; l_scrl <= M1_Global.Assy_List_SCRL.Count; l_scrl++)
                        {
                            R1 = this;
                            SuspensionCoordinatesRear scrl_list = new SuspensionCoordinatesRear(R1);
                            if (M1_Global.susRLitemcounterNB == l_scrl)
                            {
                                M1_Global.Assy_List_SCRL.Insert(l_scrl, scrl_list);
                                M1_Global.Assy_List_SCRL[l_scrl]._SCRLName = "Rear Left Coordinates " + Convert.ToString(l_scrl + 1);
                                MessageBox.Show("Rear Left Coordinates Item " + M1_Global.Assy_List_SCRL[l_scrl]._SCRLName + " has been created");

                                comboBoxSCRL.Items.Insert(l_scrl, M1_Global.Assy_List_SCRL[l_scrl]);
                                comboBoxSCRL.DisplayMember = "_SCRLName";

                                navBarItemSCRL[l_scrl].Enabled = true;
                                M1_Global.susRLitemcounterNB++;
                                break;
                            }
                        }
                    }
                    susRLitempressed++;
                    susRLitembuttonpressed++;
                }
                susRLitempressed--;
                susRLitembuttonpressed--;
            }
        }
        #endregion

        //
        //Rear Right Suspension Coordinate Item Creation and GUI
        //
        #region Rear Right Suspension Coordinate Item Creation and GUI
        List<NavBarItem> navBarItemSCRR = new List<NavBarItem>();
        int susRRitempressed = 0; int susRRitembuttonpressed = 0;
        private void BarButtonSCRR_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlFixedPointRR);
            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlMovingPointsRR);
            #endregion

            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if (M1_Global.susRRitemcounterBB == susRRitempressed)
            {
                for (int i_scrr = 0; i_scrr <= navBarItemSCRR.Count; i_scrr++)
                {
                    if (M1_Global.susRRitemcounterBB == i_scrr)
                    {
                        navBarItemSCRR.Insert(i_scrr, navBarControl2.Items.Add());
                        navBarItemSCRR[i_scrr].Caption = "Rear Right Coordinates " + Convert.ToString(M1_Global.susRRitemcounterBB + 1);
                        navBarGroupSuspensionRR.ItemLinks.Add(navBarItemSCRR[i_scrr]);
                        navBarItemSCRR[i_scrr].LinkClicked += new NavBarLinkEventHandler(navBarItemSCRR_LinkClicked);
                        navBarItemSCRR[i_scrr].Enabled = false;
                        M1_Global.susRRitemcounterBB++;
                        break;
                    }
                }
            }
        }

        void navBarItemSCRR_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = navBarGroupSuspensionRR.SelectedLinkIndex;

            #region GUI
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
            accordionControlSuspensionCoordinatesFR.Hide();
            accordionControlSuspensionCoordinatesRR.Hide();
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
            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlFixedPointRR);
            accordionControlSuspensionCoordinatesRR.ExpandElement(accordionControlMovingPointsRR);
            #endregion

            for (int c_scrr = 0; c_scrr <= navBarItemSCRR.Count; c_scrr++)
            {
                if (index == c_scrr)
                {
                    #region Displaying All the Suspension Coordinates of the Selected Suspension Coordinate Item
                    A1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].A1x);
                    A1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].A1y);
                    A1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].A1z);

                    B1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].B1x);
                    B1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].B1y);
                    B1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].B1z);

                    C1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].C1x);
                    C1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].C1y);
                    C1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].C1z);

                    D1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].D1x);
                    D1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].D1y);
                    D1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].D1z);

                    E1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].E1x);
                    E1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].E1y);
                    E1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].E1z);

                    F1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].F1x);
                    F1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].F1y);
                    F1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].F1z);

                    G1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].G1x);
                    G1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].G1y);
                    G1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].G1z);

                    H1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].H1x);
                    H1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].H1y);
                    H1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].H1z);

                    I1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].I1x);
                    I1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].I1y);
                    I1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].I1z);

                    J1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].J1x);
                    J1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].J1y);
                    J1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].J1z);

                    JO1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].JO1x);
                    JO1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].JO1y);
                    JO1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].JO1z);

                    K1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].K1x);
                    K1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].K1y);
                    K1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].K1z);

                    M1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].M1x);
                    M1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].M1y);
                    M1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].M1z);

                    N1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].N1x);
                    N1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].N1y);
                    N1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].N1z);

                    O1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].O1x);
                    O1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].O1y);
                    O1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].O1z);

                    P1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].P1x);
                    P1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].P1y);
                    P1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].P1z);

                    Q1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].Q1x);
                    Q1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].Q1y);
                    Q1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].Q1z);

                    R1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].R1x);
                    R1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].R1y);
                    R1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].R1z);

                    W1xRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].W1x);
                    W1yRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].W1y);
                    W1zRR.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].W1z);

                    RideHeightRefRRx.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].RideHeightRefx);
                    RideHeightRefRRy.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].RideHeightRefy);
                    RideHeightRefRRz.Text = Convert.ToString(M1_Global.Assy_List_SCRR[c_scrr].RideHeightRefz);
                    #endregion

                }
            }
        }
        private void CreateNewSCRR_Click(object sender, EventArgs e)
        {
            susRRitembuttonpressed++;
            susRRitempressed++;
            if (M1_Global.susRRitemcounterBB == susRRitembuttonpressed)
            {
                for (int l_scrr = 0; l_scrr <= M1_Global.Assy_List_SCRR.Count; l_scrr++)
                {
                    R1 = this;
                    SuspensionCoordinatesRearRight scrr_list = new SuspensionCoordinatesRearRight(R1);
                    if (M1_Global.susRRitemcounterNB == l_scrr)
                    {
                        M1_Global.Assy_List_SCRR.Insert(l_scrr, scrr_list);
                        M1_Global.Assy_List_SCRR[l_scrr]._SCRRName = "Rear Right Coordinates " + Convert.ToString(l_scrr + 1);
                        MessageBox.Show("Rear Right Coordinates Item " + M1_Global.Assy_List_SCRR[l_scrr]._SCRRName + " has been created");

                        comboBoxSCRR.Items.Insert(l_scrr, M1_Global.Assy_List_SCRR[l_scrr]);
                        comboBoxSCRR.DisplayMember = "_SCRRName";

                        navBarItemSCRR[l_scrr].Enabled = true;
                        M1_Global.susRRitemcounterNB++;
                        break;
                    }
                }
                susRRitembuttonpressed++;
                susRRitempressed++;
            }
            susRRitembuttonpressed--;
            susRRitempressed--;
        }
        private void SCRRTextBox_Clicked(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                susRRitempressed++;
                susRRitembuttonpressed++;
                if (M1_Global.susRRitemcounterBB == susRRitempressed)
                {
                    if ((string.IsNullOrEmpty(A1xRR.Text)) || (string.IsNullOrEmpty(A1yRR.Text)) || (string.IsNullOrEmpty(A1zRR.Text)) ||
                        (string.IsNullOrEmpty(B1xRR.Text)) || (string.IsNullOrEmpty(B1yRR.Text)) || (string.IsNullOrEmpty(B1zRR.Text)) ||
                        (string.IsNullOrEmpty(C1xRR.Text)) || (string.IsNullOrEmpty(C1yRR.Text)) || (string.IsNullOrEmpty(C1zRR.Text)) ||
                        (string.IsNullOrEmpty(D1xRR.Text)) || (string.IsNullOrEmpty(D1yRR.Text)) || (string.IsNullOrEmpty(D1zRR.Text)) ||
                        (string.IsNullOrEmpty(E1xRR.Text)) || (string.IsNullOrEmpty(E1yRR.Text)) || (string.IsNullOrEmpty(E1zRR.Text)) ||
                        (string.IsNullOrEmpty(F1xRR.Text)) || (string.IsNullOrEmpty(F1yRR.Text)) || (string.IsNullOrEmpty(F1zRR.Text)) ||
                        (string.IsNullOrEmpty(G1xRR.Text)) || (string.IsNullOrEmpty(G1yRR.Text)) || (string.IsNullOrEmpty(G1zRR.Text)) ||
                        (string.IsNullOrEmpty(H1xRR.Text)) || (string.IsNullOrEmpty(H1yRR.Text)) || (string.IsNullOrEmpty(H1zRR.Text)) ||
                        (string.IsNullOrEmpty(I1xRR.Text)) || (string.IsNullOrEmpty(I1yRR.Text)) || (string.IsNullOrEmpty(I1zRR.Text)) ||
                        (string.IsNullOrEmpty(J1xRR.Text)) || (string.IsNullOrEmpty(J1yRR.Text)) || (string.IsNullOrEmpty(J1zRR.Text)) ||
                        (string.IsNullOrEmpty(JO1xRR.Text)) || (string.IsNullOrEmpty(JO1yRR.Text)) || (string.IsNullOrEmpty(JO1zRR.Text)) ||
                        (string.IsNullOrEmpty(K1xRR.Text)) || (string.IsNullOrEmpty(K1yRR.Text)) || (string.IsNullOrEmpty(K1zRR.Text)) ||
                        (string.IsNullOrEmpty(M1xRR.Text)) || (string.IsNullOrEmpty(M1yRR.Text)) || (string.IsNullOrEmpty(M1zRR.Text)) ||
                        (string.IsNullOrEmpty(N1xRR.Text)) || (string.IsNullOrEmpty(N1yRR.Text)) || (string.IsNullOrEmpty(N1zRR.Text)) ||
                        (string.IsNullOrEmpty(O1xRR.Text)) || (string.IsNullOrEmpty(O1yRR.Text)) || (string.IsNullOrEmpty(O1zRR.Text)) ||
                        (string.IsNullOrEmpty(P1xRR.Text)) || (string.IsNullOrEmpty(P1yRR.Text)) || (string.IsNullOrEmpty(P1zRR.Text)) ||
                        (string.IsNullOrEmpty(Q1xRR.Text)) || (string.IsNullOrEmpty(Q1yRR.Text)) || (string.IsNullOrEmpty(Q1zRR.Text)) ||
                        (string.IsNullOrEmpty(R1xRR.Text)) || (string.IsNullOrEmpty(R1yRR.Text)) || (string.IsNullOrEmpty(R1zRR.Text)) ||
                        (string.IsNullOrEmpty(W1xRR.Text)) || (string.IsNullOrEmpty(W1yRR.Text)) || (string.IsNullOrEmpty(W1zRR.Text)) ||
                        (string.IsNullOrEmpty(RideHeightRefRRx.Text)) || (string.IsNullOrEmpty(RideHeightRefRRy.Text)) || (string.IsNullOrEmpty(RideHeightRefRRz.Text)))
                    {
                        MessageBox.Show("Input cannot be empty! Please enter a value");
                    }
                    else if ((!!string.IsNullOrEmpty(A1xRR.Text)) || (!string.IsNullOrEmpty(A1yRR.Text)) || (!string.IsNullOrEmpty(A1zRR.Text)) ||
                            (!string.IsNullOrEmpty(B1xRR.Text)) || (!string.IsNullOrEmpty(B1yRR.Text)) || (!string.IsNullOrEmpty(B1zRR.Text)) ||
                            (!string.IsNullOrEmpty(C1xRR.Text)) || (!string.IsNullOrEmpty(C1yRR.Text)) || (!string.IsNullOrEmpty(C1zRR.Text)) ||
                            (!string.IsNullOrEmpty(D1xRR.Text)) || (!string.IsNullOrEmpty(D1yRR.Text)) || (!string.IsNullOrEmpty(D1zRR.Text)) ||
                            (!string.IsNullOrEmpty(E1xRR.Text)) || (!string.IsNullOrEmpty(E1yRR.Text)) || (!string.IsNullOrEmpty(E1zRR.Text)) ||
                            (!string.IsNullOrEmpty(F1xRR.Text)) || (!string.IsNullOrEmpty(F1yRR.Text)) || (!string.IsNullOrEmpty(F1zRR.Text)) ||
                            (!string.IsNullOrEmpty(G1xRR.Text)) || (!string.IsNullOrEmpty(G1yRR.Text)) || (!string.IsNullOrEmpty(G1zRR.Text)) ||
                            (!string.IsNullOrEmpty(H1xRR.Text)) || (!string.IsNullOrEmpty(H1yRR.Text)) || (!string.IsNullOrEmpty(H1zRR.Text)) ||
                            (!string.IsNullOrEmpty(I1xRR.Text)) || (!string.IsNullOrEmpty(I1yRR.Text)) || (!string.IsNullOrEmpty(I1zRR.Text)) ||
                            (!string.IsNullOrEmpty(J1xRR.Text)) || (!string.IsNullOrEmpty(J1yRR.Text)) || (!string.IsNullOrEmpty(J1zRR.Text)) ||
                            (!string.IsNullOrEmpty(JO1xRR.Text)) || (!string.IsNullOrEmpty(JO1yRR.Text)) || (!string.IsNullOrEmpty(JO1zRR.Text)) ||
                            (!string.IsNullOrEmpty(K1xRR.Text)) || (!string.IsNullOrEmpty(K1yRR.Text)) || (!string.IsNullOrEmpty(K1zRR.Text)) ||
                            (!string.IsNullOrEmpty(M1xRR.Text)) || (!string.IsNullOrEmpty(M1yRR.Text)) || (!string.IsNullOrEmpty(M1zRR.Text)) ||
                            (!string.IsNullOrEmpty(N1xRR.Text)) || (!string.IsNullOrEmpty(N1yRR.Text)) || (!string.IsNullOrEmpty(N1zRR.Text)) ||
                            (!string.IsNullOrEmpty(O1xRR.Text)) || (!string.IsNullOrEmpty(O1yRR.Text)) || (!string.IsNullOrEmpty(O1zRR.Text)) ||
                            (!string.IsNullOrEmpty(P1xRR.Text)) || (!string.IsNullOrEmpty(P1yRR.Text)) || (!string.IsNullOrEmpty(P1zRR.Text)) ||
                            (!string.IsNullOrEmpty(Q1xRR.Text)) || (!string.IsNullOrEmpty(Q1yRR.Text)) || (!string.IsNullOrEmpty(Q1zRR.Text)) ||
                            (!string.IsNullOrEmpty(R1xRR.Text)) || (!string.IsNullOrEmpty(R1yRR.Text)) || (!string.IsNullOrEmpty(R1zRR.Text)) ||
                            (!string.IsNullOrEmpty(W1xRR.Text)) || (!string.IsNullOrEmpty(W1yRR.Text)) || (!string.IsNullOrEmpty(W1zRR.Text)) ||
                            (!string.IsNullOrEmpty(RideHeightRefRRx.Text)) || (!string.IsNullOrEmpty(RideHeightRefRRy.Text)) || (!string.IsNullOrEmpty(RideHeightRefRRz.Text)))
                    {
                        for (int l_scrr = 0; l_scrr <= M1_Global.Assy_List_SCRR.Count; l_scrr++)
                        {
                            R1 = this;
                            SuspensionCoordinatesRearRight scrr_list = new SuspensionCoordinatesRearRight(R1);
                            if (M1_Global.susRRitemcounterNB == l_scrr)
                            {
                                M1_Global.Assy_List_SCRR.Insert(l_scrr, scrr_list);
                                M1_Global.Assy_List_SCRR[l_scrr]._SCRRName = "Rear Right Coordinates " + Convert.ToString(l_scrr + 1);
                                MessageBox.Show("Rear Right Coordinates Item " + M1_Global.Assy_List_SCRR[l_scrr]._SCRRName + " has been created");

                                comboBoxSCRR.Items.Insert(l_scrr, M1_Global.Assy_List_SCRR[l_scrr]);
                                comboBoxSCRR.DisplayMember = "_SCRRName";

                                navBarItemSCRR[l_scrr].Enabled = true;
                                M1_Global.susRRitemcounterNB++;
                                break;
                            }
                        }
                    }
                    susRRitempressed++;
                    susRRitembuttonpressed++;
                }
                susRRitempressed--;
                susRRitembuttonpressed--;
            }
        }
        #endregion


        //
        // Vehicle Item GUI 
        //
        List<NavBarItem> navBarItemVehicle = new List<NavBarItem>();
        int vehiclecounter=0;
        private void barButtonVehicleItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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


            navBarControl2.LinkSelectionMode = LinkSelectionModeType.OneInGroup;
            if(vehiclecounter==0)
            {
                for (int i_vehicle = 0; i_vehicle <= navBarItemVehicle.Count; i_vehicle++)
                {
                    if (vehiclecounter == i_vehicle)
                    {
                        navBarItemVehicle.Insert(i_vehicle, navBarControl2.Items.Add());
                        navBarItemVehicle[i_vehicle].Caption = "Vehicle ";
                        navBarGroupVehicle.ItemLinks.Add(navBarItemVehicle[i_vehicle]);
                        navBarItemVehicle[i_vehicle].LinkClicked += new NavBarLinkEventHandler(navBarItemVehicle_LinkClicked);
                        navBarItemVehicle[i_vehicle].Enabled = true;
                        vehiclecounter++;
                        break;
                    }
                }
            }
            


        }

        void navBarItemVehicle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            sidePanel2.Show();
            accordionControlTireStiffness.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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
        }





        private void AssembleVehicle_Click(object sender, EventArgs e)
        {
            int Assy_Checker = 0;
            InputSheet I1 = new InputSheet(R1);
            //
            //Suspension Assembly
            //
            #region Suspension Assembly
            if ((comboBoxSCFL.SelectedItem!=null)&&(comboBoxSCFR.SelectedItem!=null)&&(comboBoxSCRL.SelectedItem!=null)&&(comboBoxSCRR.SelectedItem!=null))
            {
                #region Assembling the Suspension Coordinates which User has selected
                M1_Global.Assy_SCM[0] = (SuspensionCoordinatesFront)comboBoxSCFL.SelectedItem;
                #region Populating the Input Sheet - Front Left Coordinates
                I1.A1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].A1x)));
                I1.A1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].A1y)));
                I1.A1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].A1z)));
                I1.B1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].B1x)));
                I1.B1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].B1y)));
                I1.B1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].B1z)));
                I1.C1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].C1x)));
                I1.C1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].C1y)));
                I1.C1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].C1z)));
                I1.D1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].D1x)));
                I1.D1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].D1y)));
                I1.D1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].D1z)));
                I1.N1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].N1x)));
                I1.N1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].N1y)));
                I1.N1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].N1z)));
                I1.Q1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].Q1x)));
                I1.Q1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].Q1y)));
                I1.Q1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].Q1z)));
                I1.I1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].I1x)));
                I1.I1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].I1y)));
                I1.I1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].I1z)));
                I1.JO1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].JO1x)));
                I1.JO1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].JO1y)));
                I1.JO1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                I1.J1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].J1x)));
                I1.J1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].J1y)));
                I1.J1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                I1.H1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].H1x)));
                I1.H1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].H1y)));
                I1.H1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                I1.O1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].O1x)));
                I1.O1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].O1y)));
                I1.O1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                I1.G1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].G1x)));
                I1.G1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].G1y)));
                I1.G1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                I1.F1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].F1x)));
                I1.F1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].F1y)));
                I1.F1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                I1.E1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].E1x)));
                I1.E1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].E1y)));
                I1.E1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                I1.M1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].M1x)));
                I1.M1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].M1y)));
                I1.M1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                I1.K1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].K1x)));
                I1.K1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].K1y)));
                I1.K1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                I1.P1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].P1x)));
                I1.P1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].P1y)));
                I1.P1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                I1.W1xFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].W1x)));
                I1.W1yFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].W1y)));
                I1.W1zFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].W1z)));
                // Link Lengths
                I1.LowerFrontFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].LowerFrontLength)));
                I1.LowerRearFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].LowerRearLength)));
                I1.UpperFrontFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].UpperFrontLength)));
                I1.UpperRearFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].UpperRearLength)));
                I1.PushRodFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].PushRodLength)));
                I1.ToeLinkFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].ToeLinkLength)));
                I1.ARBBladeFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].ARBBladeLength)));
                I1.ARBDroopLinkFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].ARBDroopLinkLength)));
                I1.DamperLengthFL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[0].DamperLength)));
                #endregion

                M1_Global.Assy_SCM[1] = (SuspensionCoordinatesFrontRight)comboBoxSCFR.SelectedItem;
                #region Populating the Input Sheet - Front Left Coordinates
                I1.A1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].A1x)));
                I1.A1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].A1y)));
                I1.A1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].A1z)));
                I1.B1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].B1x)));
                I1.B1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].B1y)));
                I1.B1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].B1z)));
                I1.C1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].C1x)));
                I1.C1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].C1y)));
                I1.C1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].C1z)));
                I1.D1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].D1x)));
                I1.D1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].D1y)));
                I1.D1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].D1z)));
                I1.N1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].N1x)));
                I1.N1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].N1y)));
                I1.N1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].N1z)));
                I1.Q1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].Q1x)));
                I1.Q1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].Q1y)));
                I1.Q1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].Q1z)));
                I1.I1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].I1x)));
                I1.I1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].I1y)));
                I1.I1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].I1z)));
                I1.JO1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].JO1x)));
                I1.JO1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].JO1y)));
                I1.JO1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                I1.J1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].J1x)));
                I1.J1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].J1y)));
                I1.J1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                I1.H1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].H1x)));
                I1.H1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].H1y)));
                I1.H1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                I1.O1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].O1x)));
                I1.O1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].O1y)));
                I1.O1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                I1.G1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].G1x)));
                I1.G1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].G1y)));
                I1.G1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                I1.F1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].F1x)));
                I1.F1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].F1y)));
                I1.F1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                I1.E1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].E1x)));
                I1.E1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].E1y)));
                I1.E1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                I1.M1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].M1x)));
                I1.M1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].M1y)));
                I1.M1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                I1.K1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].K1x)));
                I1.K1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].K1y)));
                I1.K1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                I1.P1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].P1x)));
                I1.P1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].P1y)));
                I1.P1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                I1.W1xFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].W1x)));
                I1.W1yFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].W1y)));
                I1.W1zFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].W1z)));
                // Link Lengths
                I1.LowerFrontFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].LowerFrontLength)));
                I1.LowerRearFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].LowerRearLength)));
                I1.UpperFrontFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].UpperFrontLength)));
                I1.UpperRearFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].UpperRearLength)));
                I1.PushRodFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].PushRodLength)));
                I1.ToeLinkFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].ToeLinkLength)));
                I1.ARBBladeFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].ARBBladeLength)));
                I1.ARBDroopLinkFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].ARBDroopLinkLength)));
                I1.DamperLengthFR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[1].DamperLength)));
                #endregion

                M1_Global.Assy_SCM[2] = (SuspensionCoordinatesRear)comboBoxSCRL.SelectedItem;
                #region Populating the Input Sheet - REAR Left Coordinates
                I1.A1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].A1x)));
                I1.A1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].A1y)));
                I1.A1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].A1z)));
                I1.B1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].B1x)));
                I1.B1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].B1y)));
                I1.B1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].B1z)));
                I1.C1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].C1x)));
                I1.C1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].C1y)));
                I1.C1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].C1z)));
                I1.D1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].D1x)));
                I1.D1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].D1y)));
                I1.D1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].D1z)));
                I1.N1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].N1x)));
                I1.N1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].N1y)));
                I1.N1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].N1z)));
                I1.Q1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].Q1x)));
                I1.Q1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].Q1y)));
                I1.Q1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].Q1z)));
                I1.I1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].I1x)));
                I1.I1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].I1y)));
                I1.I1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].I1z)));
                I1.JO1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].JO1x)));
                I1.JO1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].JO1y)));
                I1.JO1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                I1.J1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].J1x)));
                I1.J1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].J1y)));
                I1.J1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                I1.H1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].H1x)));
                I1.H1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].H1y)));
                I1.H1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                I1.O1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].O1x)));
                I1.O1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].O1y)));
                I1.O1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                I1.G1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].G1x)));
                I1.G1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].G1y)));
                I1.G1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                I1.F1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].F1x)));
                I1.F1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].F1y)));
                I1.F1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                I1.E1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].E1x)));
                I1.E1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].E1y)));
                I1.E1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                I1.M1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].M1x)));
                I1.M1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].M1y)));
                I1.M1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                I1.K1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].K1x)));
                I1.K1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].K1y)));
                I1.K1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                I1.P1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].P1x)));
                I1.P1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].P1y)));
                I1.P1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                I1.W1xRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].W1x)));
                I1.W1yRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].W1y)));
                I1.W1zRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].W1z)));
                // Link Lengths
                I1.LowerFrontRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].LowerFrontLength)));
                I1.LowerRearRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].LowerRearLength)));
                I1.UpperFrontRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].UpperFrontLength)));
                I1.UpperRearRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].UpperRearLength)));
                I1.PushRodRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].PushRodLength)));
                I1.ToeLinkRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].ToeLinkLength)));
                I1.ARBBladeRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].ARBBladeLength)));
                I1.ARBDroopLinkRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].ARBDroopLinkLength)));
                I1.DamperLengthRL.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[2].DamperLength)));
                #endregion
                
                M1_Global.Assy_SCM[3] = (SuspensionCoordinatesRearRight)comboBoxSCRR.SelectedItem;
                #region Populating the Input Sheet - REAR RIGHT Coordinates
                I1.A1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].A1x)));
                I1.A1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].A1y)));
                I1.A1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].A1z)));
                I1.B1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].B1x)));
                I1.B1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].B1y)));
                I1.B1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].B1z)));
                I1.C1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].C1x)));
                I1.C1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].C1y)));
                I1.C1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].C1z)));
                I1.D1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].D1x)));
                I1.D1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].D1y)));
                I1.D1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].D1z)));
                I1.N1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].N1x)));
                I1.N1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].N1y)));
                I1.N1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].N1z)));
                I1.Q1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].Q1x)));
                I1.Q1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].Q1y)));
                I1.Q1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].Q1z)));
                I1.I1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].I1x)));
                I1.I1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].I1y)));
                I1.I1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].I1z)));
                I1.JO1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].JO1x)));
                I1.JO1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].JO1y)));
                I1.JO1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].JO1z)));
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                I1.J1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].J1x)));
                I1.J1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].J1y)));
                I1.J1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].J1z)));
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                I1.H1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].H1x)));
                I1.H1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].H1y)));
                I1.H1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].H1z)));
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                I1.O1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].O1x)));
                I1.O1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].O1y)));
                I1.O1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].O1z)));
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                I1.G1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].G1x)));
                I1.G1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].G1y)));
                I1.G1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].G1z)));
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                I1.F1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].F1x)));
                I1.F1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].F1y)));
                I1.F1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].F1z)));
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                I1.E1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].E1x)));
                I1.E1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].E1y)));
                I1.E1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].E1z)));
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                I1.M1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].M1x)));
                I1.M1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].M1y)));
                I1.M1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].M1z)));
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                I1.K1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].K1x)));
                I1.K1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].K1y)));
                I1.K1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].K1z)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                I1.P1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].P1x)));
                I1.P1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].P1y)));
                I1.P1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].P1z)));
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                I1.W1xRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].W1x)));
                I1.W1yRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].W1y)));
                I1.W1zRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].W1z)));
                // Link Lengths
                I1.LowerFrontRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].LowerFrontLength)));
                I1.LowerRearRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].LowerRearLength)));
                I1.UpperFrontRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].UpperFrontLength)));
                I1.UpperRearRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].UpperRearLength)));
                I1.PushRodRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].PushRodLength)));
                I1.ToeLinkRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].ToeLinkLength)));
                I1.ARBBladeRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].ARBBladeLength)));
                I1.ARBDroopLinkRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].ARBDroopLinkLength)));
                I1.DamperLengthRR.Text = String.Format("{0:0.000}", ((M1_Global.Assy_SCM[3].DamperLength)));
                #endregion
                
                #endregion
                
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Suspension Pick Up Points have not been assembled");
            }
            #endregion

            //
            //Tire Assembly
            //
            #region Tire Assembly
            if ((comboBoxTireFL.SelectedItem != null) && (comboBoxTireFR.SelectedItem != null) && (comboBoxTireRL.SelectedItem != null) && (comboBoxTireRR.SelectedItem != null))
            {
                #region Assembling the Tires which User has selected
                M1_Global.Assy_Tire[0] = (Tire)comboBoxTireFL.SelectedItem;
                #region Populating Input Sheet - FRONT LEFT Tire
                I1.TireRateFL.Text = Convert.ToString(M1_Global.Assy_Tire[0].TireRate);
                I1.TireWidthFL.Text = Convert.ToString(M1_Global.Assy_Tire[0].TireWidth);
                #endregion

                M1_Global.Assy_Tire[1] = (Tire)comboBoxTireFR.SelectedItem;
                #region Populating Input Sheet - FRONT RIGHT Tire
                I1.TireRateFR.Text = Convert.ToString(M1_Global.Assy_Tire[1].TireRate);
                I1.TireWidthFR.Text = Convert.ToString(M1_Global.Assy_Tire[1].TireWidth);
                #endregion
                
                M1_Global.Assy_Tire[2] = (Tire)comboBoxTireRL.SelectedItem;
                #region Populating Input Sheet - REAR LEFT Tire
                I1.TireRateRL.Text = Convert.ToString(M1_Global.Assy_Tire[2].TireRate);
                I1.TireWidthRL.Text = Convert.ToString(M1_Global.Assy_Tire[2].TireWidth);
                #endregion
                
                M1_Global.Assy_Tire[3] = (Tire)comboBoxTireRR.SelectedItem;
                #region Populating Input Sheet - REAR RIGHT Tire
                I1.TireRateRR.Text = Convert.ToString(M1_Global.Assy_Tire[3].TireRate);
                I1.TireWidthRR.Text = Convert.ToString(M1_Global.Assy_Tire[3].TireWidth);
                #endregion

                #endregion
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Tires have not been assembled");
            }
            #endregion

            //
            //Spring Assembly
            //
            #region Spring Assembly
            if ((comboBoxSpringFL.SelectedItem != null) && (comboBoxSpringFR.SelectedItem != null) && (comboBoxSpringRL.SelectedItem != null) && (comboBoxSpringRR.SelectedItem != null))
            {
                #region Assembling the Springs which the User has Selected
                M1_Global.Assy_Spring[0] = (Spring)comboBoxSpringFL.SelectedItem;
                #region Populating the Input Sheet - FRONT LEFT Spring
                I1.SpringRateFL.Text = Convert.ToString(M1_Global.Assy_Spring[0].SpringRate);
                I1.SpringPreloadFL.Text = Convert.ToString(M1_Global.Assy_Spring[0].SpringPreload);
                I1.SpringFreeLengthFL.Text = Convert.ToString(M1_Global.Assy_Spring[0].SpringFreeLength);
                #endregion

                M1_Global.Assy_Spring[1] = (Spring)comboBoxSpringFR.SelectedItem;
                #region Populating the Input Sheet - FRONT RIGHT Spring
                I1.SpringRateFR.Text = Convert.ToString(M1_Global.Assy_Spring[1].SpringRate);
                I1.SpringPreloadFR.Text = Convert.ToString(M1_Global.Assy_Spring[1].SpringPreload);
                I1.SpringFreeLengthFR.Text = Convert.ToString(M1_Global.Assy_Spring[1].SpringFreeLength);
                #endregion

                M1_Global.Assy_Spring[2] = (Spring)comboBoxSpringRL.SelectedItem;
                #region Populating the Input Sheet - REAR LEFT Spring
                I1.SpringRateRL.Text = Convert.ToString(M1_Global.Assy_Spring[2].SpringRate);
                I1.SpringPreloadRL.Text = Convert.ToString(M1_Global.Assy_Spring[2].SpringPreload);
                I1.SpringFreeLengthRL.Text = Convert.ToString(M1_Global.Assy_Spring[2].SpringFreeLength);
                #endregion
                
                M1_Global.Assy_Spring[3] = (Spring)comboBoxSpringRR.SelectedItem;
                #region Populating the Input Sheet - FRONT LEFT Spring
                I1.SpringRateRR.Text = Convert.ToString(M1_Global.Assy_Spring[3].SpringRate);
                I1.SpringPreloadRR.Text = Convert.ToString(M1_Global.Assy_Spring[3].SpringPreload);
                I1.SpringFreeLengthRR.Text = Convert.ToString(M1_Global.Assy_Spring[3].SpringFreeLength);
                #endregion
                
                #endregion
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Springs have not been assembled");
            }
            #endregion

            //
            //Damper Assembly
            //
            #region Damper Assembly
            if ((comboBoxDamperFL.SelectedItem != null) && (comboBoxDamperFR.SelectedItem != null) && (comboBoxDamperRL.SelectedItem != null) && (comboBoxDamperRR.SelectedItem != null))
            {
                #region Assembling the Dampers which the user has selected
                M1_Global.Assy_Damper[0] = (Damper)comboBoxDamperFL.SelectedItem;
                #region Populating the Input Sheet - FRONT LEFT Damper
                I1.DamperPressureFL.Text = Convert.ToString(M1_Global.Assy_Damper[0].DamperGasPressure);
                I1.DamperShaftDiaFL.Text = Convert.ToString(M1_Global.Assy_Damper[0].DamperShaftDia);
                #endregion

                M1_Global.Assy_Damper[1] = (Damper)comboBoxDamperFR.SelectedItem;
                #region Populating the Input Sheet - FRONT RIGHT Damper
                I1.DamperPressureFR.Text = Convert.ToString(M1_Global.Assy_Damper[1].DamperGasPressure);
                I1.DamperShaftDiaFR.Text = Convert.ToString(M1_Global.Assy_Damper[1].DamperShaftDia);
                #endregion
                
                M1_Global.Assy_Damper[2] = (Damper)comboBoxDamperRL.SelectedItem;
                #region Populating the Input Sheet - REAR LEFT Damper
                I1.DamperPressureRL.Text = Convert.ToString(M1_Global.Assy_Damper[2].DamperGasPressure);
                I1.DamperShaftDiaRL.Text = Convert.ToString(M1_Global.Assy_Damper[2].DamperShaftDia);
                #endregion
                
                M1_Global.Assy_Damper[3] = (Damper)comboBoxDamperRR.SelectedItem;
                #region Populating the Input Sheet - REAR RIGHT Damper
                I1.DamperPressureRR.Text = Convert.ToString(M1_Global.Assy_Damper[3].DamperGasPressure);
                I1.DamperShaftDiaRR.Text = Convert.ToString(M1_Global.Assy_Damper[3].DamperShaftDia);
                #endregion
                
                #endregion
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Dampers have not been assembled");
            }
            #endregion

            //
            //Anti-Roll Bar Assembly
            //
            #region Anti-Roll Bar Assembly
            if ((comboBoxARBFront.SelectedItem != null) && (comboBoxARBFront.SelectedItem != null) && (comboBoxARBRear.SelectedItem != null) && (comboBoxARBRear.SelectedItem != null))
            {
                #region Assembling the Anti-Roll Bars which the user has selected
                M1_Global.Assy_ARB[0] = (AntiRollBar)comboBoxARBFront.SelectedItem;
                M1_Global.Assy_ARB[1] = (AntiRollBar)comboBoxARBFront.SelectedItem;
                #region Populating the Input Sheet - Front Anti-Roll Bar
                I1.FrontAntiRollBar.Text = Convert.ToString(M1_Global.Assy_ARB[1].AntiRollBarRate);
                #endregion
                
                M1_Global.Assy_ARB[2] = (AntiRollBar)comboBoxARBRear.SelectedItem;
                M1_Global.Assy_ARB[3] = (AntiRollBar)comboBoxARBRear.SelectedItem;
                #region Populating the Input Sheet - Front Anti-Roll Bar
                I1.RearAntiRollBar.Text = Convert.ToString(M1_Global.Assy_ARB[3].AntiRollBarRate);
                #endregion
                
                #endregion
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Anti-Roll Bars have not been assembled");
            }
            #endregion

            //
            //Chassis Assembly
            //
            #region Chassis Assembly
            if (comboBoxChassis.SelectedItem!=null)
            {
                #region Assembling the Chassis which the user has selected
                M1_Global.Assy_Chassis = (Chassis)comboBoxChassis.SelectedItem;
                #region Populating the Input Sheet Chassis Items
                I1.SuspendedMass.Text = Convert.ToString(M1_Global.Assy_Chassis.SuspendedMass);
                I1.SuspendedMassCoGx.Text = Convert.ToString(M1_Global.Assy_Chassis.SuspendedMassCoGx);
                I1.SuspendedMassCoGy.Text = Convert.ToString(M1_Global.Assy_Chassis.SuspendedMassCoGy);
                I1.SuspendedMassCoGz.Text = Convert.ToString(M1_Global.Assy_Chassis.SuspendedMassCoGz);
                I1.NonSuspendedMassFL.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFL);
                I1.NonSuspendedMassCoGFLx.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFLCoGx);
                I1.NonSuspendedMassCoGFLy.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFLCoGy);
                I1.NonSuspendedMassCoGFLz.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFLCoGz);
                I1.NonSuspendedMassFR.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFR);
                I1.NonSuspendedMassCoGFRx.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFRCoGx);
                I1.NonSuspendedMassCoGFRy.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFRCoGy);
                I1.NonSuspendedMassCoGFRz.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassFRCoGz);
                I1.NonSuspendedMassRL.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRL);
                I1.NonSuspendedMassCoGRLx.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRLCoGx);
                I1.NonSuspendedMassCoGRLy.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRLCoGy);
                I1.NonSuspendedMassCoGRLz.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRLCoGz);
                I1.NonSuspendedMassRR.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRR);
                I1.NonSuspendedMassCoGRRx.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRRCoGx);
                I1.NonSuspendedMassCoGRRy.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRRCoGy);
                I1.NonSuspendedMassCoGRRz.Text = Convert.ToString(M1_Global.Assy_Chassis.NonSuspendedMassRRCoGz);
                #endregion

                #endregion
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Chassis has not been assembled");
            }
            #endregion

            //
            //WheelAlignment Assembly
            //
            #region WheelAlignment Assembly
            if ((comboBoxWAFL.SelectedItem != null) && (comboBoxWAFR.SelectedItem != null) && (comboBoxWARL.SelectedItem != null) && (comboBoxWARR.SelectedItem != null))
            {
                #region Assembling the Wheel Alignment which the user has selected
                M1_Global.Assy_WA[0] = (WheelAlignment)comboBoxWAFL.SelectedItem;
                #region Populating the Input Sheet - FRONT LEFT Wheel Alignment
                I1.CamberFL.Text = Convert.ToString(M1_Global.Assy_WA[0].StaticCamber);
                I1.ToeFL.Text = Convert.ToString(M1_Global.Assy_WA[0].StaticToe);
                #endregion
                
                M1_Global.Assy_WA[1] = (WheelAlignment)comboBoxWAFR.SelectedItem;
                #region Populating the Input Sheet - FRONT RIGHT Wheel Alignment
                I1.CamberFR.Text = Convert.ToString(M1_Global.Assy_WA[1].StaticCamber);
                I1.ToeFR.Text = Convert.ToString(M1_Global.Assy_WA[1].StaticToe);
                #endregion
                
                M1_Global.Assy_WA[2] = (WheelAlignment)comboBoxWARL.SelectedItem;
                #region Populating the Input Sheet - REAR LEFT Wheel Alignment
                I1.CamberRL.Text = Convert.ToString(M1_Global.Assy_WA[2].StaticCamber);
                I1.ToeRL.Text = Convert.ToString(M1_Global.Assy_WA[2].StaticToe);
                #endregion
                
                M1_Global.Assy_WA[3] = (WheelAlignment)comboBoxWARR.SelectedItem;
                #region Populating the Input Sheet - REAR RIGHT Wheel Alignment
                I1.CamberRR.Text = Convert.ToString(M1_Global.Assy_WA[3].StaticCamber);
                I1.ToeRR.Text = Convert.ToString(M1_Global.Assy_WA[3].StaticToe);
                #endregion
                
                #endregion
                Assy_Checker++;
            }
            else
            {
                MessageBox.Show("Vehicle's Wheel Alignment has not been set");
            }
                #endregion

            //
            // Passing the local Input Sheet Object to the Global List of Input Sheet
            //
            M1_Global.List_I1.Insert(0, I1);

            if (Assy_Checker==7)
            {
                MessageBox.Show("New Vehicle has been assembled. Calculations can now be done using the Calculate Resuls button in the Results Tab on top.");
            }
            else
            {
                MessageBox.Show("Vehicle Not Assembled Properly. Please re-check your Vehicle Item");
            }
        }



        public ProgressBarControl progressBar;
        public Label progressBarLabel;
        private void barButtonCalculateResults_ItemClicked(object sender, ItemClickEventArgs e)
        {

            CalculateResultsButtonClickCounter = 0;
            timer1.Start();
            try
            {
                #region Creating a Form to display the Progress Bar
                Form ProgressBarForm = new Form();
                ProgressBarForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                ProgressBarForm.Size = new System.Drawing.Size(300, 15);
                ProgressBarForm.ShowInTaskbar = false;
                ProgressBarForm.StartPosition = FormStartPosition.CenterScreen;
                ProgressBarForm.TopMost = true;
                #endregion

                #region Creating the Progress Bar and Label and initializaing it
                progressBar = new ProgressBarControl();
                ProgressBarForm.Controls.Add(progressBar);
                progressBar.Dock = DockStyle.Fill;
                progressBar.SendToBack();
                progressBar.Properties.Maximum = 800;
                progressBar.Properties.Step = 1;


                ProgressBarForm.Show();
                #endregion

                VehicleAssembly(M1_Global.Assy_SCM, M1_Global.Assy_Tire, M1_Global.Assy_Spring, M1_Global.Assy_Damper, M1_Global.Assy_ARB, M1_Global.Assy_Chassis, M1_Global.Assy_WA, M1_Global.Assy_OC, out M1_Global.Assembled_Vehicle);
                ProgressBarForm.Close();
                tabPaneResults.Show();
            }
            catch (Exception)
            {
                
                MessageBox.Show("Vehicle Not Assembled Properly. Please re-check your Vehicle Item");
            }
            
            CalculateResultsButtonClickCounter++;
        }

        public void VehicleAssembly(SuspensionCoordinatesMaster[] _sc,Tire[] _tire,Spring[] _spring, Damper[] _damper, AntiRollBar[] _arb, Chassis _chassis, WheelAlignment[] _wa, OutputClass[] _oc, out Vehicle _vehicle)
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
                Vehicle vehicle1 = new Vehicle(Identifier, _SC, _Tire, _Spring, _Damper, _ARB, chassis1, _WA, _OC);
                _vehicle = vehicle1;
                _vehicle.KinematicsInvoker(R1);
                _vehicle.VehicleOutputs(R1);

            #region Populating the Input Sheet -MOTION RATIO of All Corners
            M1_Global.List_I1[0].MotionRatioFL.Text = Convert.ToString(_OC[0].InitialMR);
            M1_Global.List_I1[0].MotionRatioFR.Text = Convert.ToString(_OC[1].InitialMR);
            M1_Global.List_I1[0].MotionRatioRL.Text = Convert.ToString(_OC[2].InitialMR);
            M1_Global.List_I1[0].MotionRatioRR.Text = Convert.ToString(_OC[3].InitialMR);
            #endregion
            #region Populating the Input Sheet - Corner Weight of All Corners
            M1_Global.List_I1[0].CornerWeightFL.Text = String.Format("{0:00.000}", _OC[0].CW);
            M1_Global.List_I1[0].CornerWeightFR.Text = String.Format("{0:00.000}", _OC[1].CW);
            M1_Global.List_I1[0].CornerWeightRL.Text = String.Format("{0:00.000}", _OC[2].CW);
            M1_Global.List_I1[0].CornerWeightRR.Text = String.Format("{0:00.000}", _OC[3].CW);
            #endregion

            #endregion

            

            #region Display of Outputs
            #region Display of Outputs of FRONT LEFT
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xFL.Text = Convert.ToString(((_oc[0].A2x))); A2xFL.Text = String.Format("{0:0.000}", _oc[0].A2x);
            A2yFL.Text = Convert.ToString(((_oc[0].A2y))); A2yFL.Text = String.Format("{0:0.000}", _oc[0].A2y);
            A2zFL.Text = Convert.ToString(((_oc[0].A2z))); A2zFL.Text = String.Format("{0:0.000}", _oc[0].A2z);
            B2xFL.Text = Convert.ToString(((_oc[0].B2x))); B2xFL.Text = String.Format("{0:0.000}", _oc[0].B2x);
            B2yFL.Text = Convert.ToString(((_oc[0].B2y))); B2yFL.Text = String.Format("{0:0.000}", _oc[0].B2y);
            B2zFL.Text = Convert.ToString(((_oc[0].B2z))); B2zFL.Text = String.Format("{0:0.000}", _oc[0].B2z);
            C2xFL.Text = Convert.ToString(((_oc[0].C2x))); C2xFL.Text = String.Format("{0:0.000}", _oc[0].C2x);
            C2yFL.Text = Convert.ToString(((_oc[0].C2y))); C2yFL.Text = String.Format("{0:0.000}", _oc[0].C2y);
            C2zFL.Text = Convert.ToString(((_oc[0].C2z))); C2zFL.Text = String.Format("{0:0.000}", _oc[0].C2z);
            D2xFL.Text = Convert.ToString(((_oc[0].D2x))); D2xFL.Text = String.Format("{0:0.000}", _oc[0].D2x);
            D2yFL.Text = Convert.ToString(((_oc[0].D2y))); D2yFL.Text = String.Format("{0:0.000}", _oc[0].D2y);
            D2zFL.Text = Convert.ToString(((_oc[0].D2z))); D2zFL.Text = String.Format("{0:0.000}", _oc[0].D2z);
            N2xFL.Text = Convert.ToString(((_oc[0].N2x))); N2xFL.Text = String.Format("{0:0.000}", _oc[0].N2x);
            N2yFL.Text = Convert.ToString(((_oc[0].N2y))); N2yFL.Text = String.Format("{0:0.000}", _oc[0].N2y);
            N2zFL.Text = Convert.ToString(((_oc[0].N2z))); N2zFL.Text = String.Format("{0:0.000}", _oc[0].N2z);
            Q2xFL.Text = Convert.ToString(((_oc[0].Q2x))); Q2xFL.Text = String.Format("{0:0.000}", _oc[0].Q2x);
            Q2yFL.Text = Convert.ToString(((_oc[0].Q2y))); Q2yFL.Text = String.Format("{0:0.000}", _oc[0].Q2y);
            Q2zFL.Text = Convert.ToString(((_oc[0].Q2z))); Q2zFL.Text = String.Format("{0:0.000}", _oc[0].Q2z);
            I2xFL.Text = Convert.ToString(((_oc[0].I2x))); I2xFL.Text = String.Format("{0:0.000}", _oc[0].I2x);
            I2yFL.Text = Convert.ToString(((_oc[0].I2y))); I2yFL.Text = String.Format("{0:0.000}", _oc[0].I2y);
            I2zFL.Text = Convert.ToString(((_oc[0].I2z))); I2zFL.Text = String.Format("{0:0.000}", _oc[0].I2z);
            JO2xFL.Text = Convert.ToString(((_oc[0].JO2x))); JO2xFL.Text = String.Format("{0:0.000}", _oc[0].JO2x);
            JO2yFL.Text = Convert.ToString(((_oc[0].JO2y))); JO2yFL.Text = String.Format("{0:0.000}", _oc[0].JO2y);
            JO2zFL.Text = Convert.ToString(((_oc[0].JO2z))); JO2zFL.Text = String.Format("{0:0.000}", _oc[0].JO2z);
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xFL.Text = Convert.ToString(((_oc[0].J2x))); J2xFL.Text = String.Format("{0:0.000}", _oc[0].J2x);
            J2yFL.Text = Convert.ToString(((_oc[0].J2y))); J2yFL.Text = String.Format("{0:0.000}", _oc[0].J2y);
            J2zFL.Text = Convert.ToString(((_oc[0].J2z))); J2zFL.Text = String.Format("{0:0.000}", _oc[0].J2z);
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xFL.Text = Convert.ToString(((_oc[0].H2x))); H2xFL.Text = String.Format("{0:0.000}", _oc[0].H2x);
            H2yFL.Text = Convert.ToString(((_oc[0].H2y))); H2yFL.Text = String.Format("{0:0.000}", _oc[0].H2y);
            H2zFL.Text = Convert.ToString(((_oc[0].H2z))); H2zFL.Text = String.Format("{0:0.000}", _oc[0].H2z);
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xFL.Text = Convert.ToString(((_oc[0].O2x))); O2xFL.Text = String.Format("{0:0.000}", _oc[0].O2x);
            O2yFL.Text = Convert.ToString(((_oc[0].O2y))); O2yFL.Text = String.Format("{0:0.000}", _oc[0].O2y);
            O2zFL.Text = Convert.ToString(((_oc[0].O2z))); O2zFL.Text = String.Format("{0:0.000}", _oc[0].O2z);
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xFL.Text = Convert.ToString(((_oc[0].G2x))); G2xFL.Text = String.Format("{0:0.000}", _oc[0].G2x);
            G2yFL.Text = Convert.ToString(((_oc[0].G2y))); G2yFL.Text = String.Format("{0:0.000}", _oc[0].G2y);
            G2zFL.Text = Convert.ToString(((_oc[0].G2z))); G2zFL.Text = String.Format("{0:0.000}", _oc[0].G2z);
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xFL.Text = Convert.ToString(((_oc[0].F2x))); F2xFL.Text = String.Format("{0:0.000}", _oc[0].F2x);
            F2yFL.Text = Convert.ToString(((_oc[0].F2y))); F2yFL.Text = String.Format("{0:0.000}", _oc[0].F2y);
            F2zFL.Text = Convert.ToString(((_oc[0].F2z))); F2zFL.Text = String.Format("{0:0.000}", _oc[0].F2z);
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xFL.Text = Convert.ToString(((_oc[0].E2x))); E2xFL.Text = String.Format("{0:0.000}", _oc[0].E2x);
            E2yFL.Text = Convert.ToString(((_oc[0].E2y))); E2yFL.Text = String.Format("{0:0.000}", _oc[0].E2y);
            E2zFL.Text = Convert.ToString(((_oc[0].E2z))); E2zFL.Text = String.Format("{0:0.000}", _oc[0].E2z);
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioFL.Text = Convert.ToString(((_oc[0].InitialMR))); MotionRatioFL.Text = String.Format("{0:0.000}", _oc[0].InitialMR);
            InitialARBMRFL.Text = String.Format("{0:0.000}", _oc[0].Initial_ARB_MR);
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioFL.Text = Convert.ToString(((_oc[0].FinalMR))); FinalMotionRatioFL.Text = String.Format("{0:0.000}", _oc[0].FinalMR);
            FinalARBMRFL.Text = String.Format("{0:0.000}", _oc[0].Final_ARB_MR);
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xFL.Text = Convert.ToString(((_oc[0].M2x))); M2xFL.Text = String.Format("{0:0.000}", _oc[0].M2x);
            M2yFL.Text = Convert.ToString(((_oc[0].M2y))); M2yFL.Text = String.Format("{0:0.000}", _oc[0].M2y);
            M2zFL.Text = Convert.ToString(((_oc[0].M2z))); M2zFL.Text = String.Format("{0:0.000}", _oc[0].M2z);
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xFL.Text = Convert.ToString(((_oc[0].K2x))); K2xFL.Text = String.Format("{0:0.000}", _oc[0].K2x);
            K2yFL.Text = Convert.ToString(((_oc[0].K2y))); K2yFL.Text = String.Format("{0:0.000}", _oc[0].K2y);
            K2zFL.Text = Convert.ToString(((_oc[0].K2z))); K2zFL.Text = String.Format("{0:0.000}", _oc[0].K2z);
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xFL.Text = Convert.ToString(((_oc[0].L2x))); L2xFL.Text = String.Format("{0:0.000}", _oc[0].L2x);
            L2yFL.Text = Convert.ToString(((_oc[0].L2y))); L2yFL.Text = String.Format("{0:0.000}", _oc[0].L2y);
            L2zFL.Text = Convert.ToString(((_oc[0].L2z))); L2zFL.Text = String.Format("{0:0.000}", _oc[0].L2z);
            //To Display the New Camber and Toe
            FinalCamberFL.Text = Convert.ToString(((_oc[0].FinalCamber))); FinalCamberFL.Text = String.Format("{0:0.000}", (_oc[0].FinalCamber * (180 / Math.PI)));
            FinalToeFL.Text = Convert.ToString(((_oc[0].FinalToe))); FinalToeFL.Text = String.Format("{0:0.000}", (_oc[0].FinalToe * (180 / Math.PI)));
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xFL.Text = Convert.ToString(((_oc[0].P2x))); P2xFL.Text = String.Format("{0:0.000}", _oc[0].P2x);
            P2yFL.Text = Convert.ToString(((_oc[0].P2y))); P2yFL.Text = String.Format("{0:0.000}", _oc[0].P2y);
            P2zFL.Text = Convert.ToString(((_oc[0].P2z))); P2zFL.Text = String.Format("{0:0.000}", _oc[0].P2z);
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xFL.Text = Convert.ToString(((_oc[0].W2x))); W2xFL.Text = String.Format("{0:0.000}", _oc[0].W2x);
            W2yFL.Text = Convert.ToString(((_oc[0].W2y))); W2yFL.Text = String.Format("{0:0.000}", _oc[0].W2y);
            W2zFL.Text = Convert.ToString(((_oc[0].W2z))); W2zFL.Text = String.Format("{0:0.000}", _oc[0].W2z);
            //Calculating The Final Ride Height 
            RideHeightFL.Text = Convert.ToString(((_oc[0].FinalRideHeight))); RideHeightFL.Text = String.Format("{0:0.000}", _oc[0].FinalRideHeight);
            //Calculating the New Corner Weights 
            CWFL.Text = Convert.ToString(((_oc[0].CW))); CWFL.Text = String.Format("{0:0.000}", _oc[0].CW);
            TireLoadedRadiusFL.Text = Convert.ToString(((_oc[0].TireLoadedRadius))); TireLoadedRadiusFL.Text = String.Format("{0:0.000}", _oc[0].TireLoadedRadius);
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionFL.Text = Convert.ToString(((_oc[0].Corrected_SpringDeflection))); CorrectedSpringDeflectionFL.Text = String.Format("{0:0.000}", _oc[0].Corrected_SpringDeflection);
            CorrectedWheelDeflectionFL.Text = Convert.ToString(((_oc[0].Corrected_WheelDeflection))); CorrectedWheelDeflectionFL.Text = String.Format("{0:0.000}", _oc[0].Corrected_WheelDeflection);
            NewDamperLengthFL.Text = Convert.ToString(((_oc[0].DamperLength))); NewDamperLengthFL.Text = String.Format("{0:0.000}", _oc[0].DamperLength);
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGFLx.Text = Convert.ToString(((_oc[0].New_NonSuspendedMassCoGx))); NewNSMCGFLx.Text = String.Format("{0:0.000}", _oc[0].New_NonSuspendedMassCoGx);
            NewNSMCGFLy.Text = Convert.ToString(((_oc[0].New_NonSuspendedMassCoGy))); NewNSMCGFLy.Text = String.Format("{0:0.000}", _oc[0].New_NonSuspendedMassCoGy);
            NewNSMCGFLz.Text = Convert.ToString(((_oc[0].New_NonSuspendedMassCoGz))); NewNSMCGFLz.Text = String.Format("{0:0.000}", _oc[0].New_NonSuspendedMassCoGz);
            //Calculating the Wishbone Forces
            LowerFrontFL.Text = Convert.ToString(((_oc[0].LowerFront))); LowerFrontFL.Text = String.Format("{0:0.000}", _oc[0].LowerFront);
            LowerRearFL.Text = Convert.ToString(((_oc[0].LowerRear))); LowerRearFL.Text = String.Format("{0:0.000}", _oc[0].LowerRear);
            UpperFrontFL.Text = Convert.ToString(((_oc[0].UpperFront))); UpperFrontFL.Text = String.Format("{0:0.000}", _oc[0].UpperFront);
            UpperRearFL.Text = Convert.ToString(((_oc[0].UpperRear))); UpperRearFL.Text = String.Format("{0:0.000}", _oc[0].UpperRear);
            PushRodFL.Text = Convert.ToString(((_oc[0].PushRod))); PushRodFL.Text = String.Format("{0:0.000}", _oc[0].PushRod);
            ToeLinkFL.Text = Convert.ToString(((_oc[0].ToeLink))); ToeLinkFL.Text = String.Format("{0:0.000}", _oc[0].ToeLink);
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisFLx.Text = Convert.ToString(((_oc[0].LowerFront_x))); LowerFrontChassisFLx.Text = String.Format("{0:0.000}", _oc[0].LowerFront_x);
            LowerFrontChassisFLy.Text = Convert.ToString(((_oc[0].LowerFront_y))); LowerFrontChassisFLy.Text = String.Format("{0:0.000}", _oc[0].LowerFront_y);
            LowerFrontChassisFLz.Text = Convert.ToString(((_oc[0].LowerFront_z))); LowerFrontChassisFLz.Text = String.Format("{0:0.000}", _oc[0].LowerFront_z);
            LowerRearChassisFLx.Text = Convert.ToString(((_oc[0].LowerRear_x))); LowerRearChassisFLx.Text = String.Format("{0:0.000}", _oc[0].LowerRear_x);
            LowerRearChassisFLy.Text = Convert.ToString(((_oc[0].LowerRear_y))); LowerRearChassisFLy.Text = String.Format("{0:0.000}", _oc[0].LowerRear_y);
            LowerRearChassisFLz.Text = Convert.ToString(((_oc[0].LowerRear_z))); LowerRearChassisFLz.Text = String.Format("{0:0.000}", _oc[0].LowerRear_z);
            
            UpperFrontChassisFLx.Text = Convert.ToString(((_oc[0].UpperFront_x))); UpperFrontChassisFLx.Text = String.Format("{0:0.000}", _oc[0].UpperFront_x);
            UpperFrontChassisFLy.Text = Convert.ToString(((_oc[0].UpperFront_y))); UpperFrontChassisFLy.Text = String.Format("{0:0.000}", _oc[0].UpperFront_y);
            UpperFrontChassisFLz.Text = Convert.ToString(((_oc[0].UpperFront_z))); UpperFrontChassisFLz.Text = String.Format("{0:0.000}", _oc[0].UpperFront_z);
            UpperRearChassisFLx.Text = Convert.ToString(((_oc[0].UpperRear_x))); UpperRearChassisFLx.Text = String.Format("{0:0.000}", _oc[0].UpperRear_x);
            UpperRearChassisFLy.Text = Convert.ToString(((_oc[0].UpperRear_y))); UpperRearChassisFLy.Text = String.Format("{0:0.000}", _oc[0].UpperRear_y);
            UpperRearChassisFLz.Text = Convert.ToString(((_oc[0].UpperRear_z))); UpperRearChassisFLz.Text = String.Format("{0:0.000}", _oc[0].UpperRear_z);
            
            PushRodChassisFLx.Text = Convert.ToString(((_oc[0].PushRod_x))); PushRodChassisFLx.Text = String.Format("{0:0.000}", _oc[0].PushRod_x);
            PushRodChassisFLy.Text = Convert.ToString(((_oc[0].PushRod_y))); PushRodChassisFLy.Text = String.Format("{0:0.000}", _oc[0].PushRod_y);
            PushRodChassisFLz.Text = Convert.ToString(((_oc[0].PushRod_z))); PushRodChassisFLz.Text = String.Format("{0:0.000}", _oc[0].PushRod_z);
            PushRodUprightFLx.Text = Convert.ToString(((_oc[0].PushRod_x))); PushRodUprightFLx.Text = String.Format("{0:0.000}", _oc[0].PushRod_x);
            PushRodUprightFLy.Text = Convert.ToString(((_oc[0].PushRod_y))); PushRodUprightFLy.Text = String.Format("{0:0.000}", _oc[0].PushRod_y);
            PushRodUprightFLz.Text = Convert.ToString(((_oc[0].PushRod_z))); PushRodUprightFLz.Text = String.Format("{0:0.000}", _oc[0].PushRod_z);
            
            DamperForceFL.Text = String.Format("{0:0.000}", _oc[0].DamperForce);
            SpringPreloadOutputFL.Text = String.Format("{0:0.000}", _spring[0].SpringPreload * _spring[0].PreloadForce);
            DamperForceChassisFLx.Text = String.Format("{0:0.000}", _oc[0].DamperForce_x);
            DamperForceChassisFLy.Text = String.Format("{0:0.000}", _oc[0].DamperForce_y);
            DamperForceChassisFLz.Text = String.Format("{0:0.000}", _oc[0].DamperForce_z);
            DamperForceBellCrankFLx.Text = String.Format("{0:0.000}", _oc[0].DamperForce_x);
            DamperForceBellCrankFLy.Text = String.Format("{0:0.000}", _oc[0].DamperForce_y);
            DamperForceBellCrankFLz.Text = String.Format("{0:0.000}", _oc[0].DamperForce_z);

            DroopLinkForceFL.Text = String.Format("{0:000}", _oc[0].ARBDroopLink);
            DroopLinkBellCrankFLx.Text = String.Format("{0:000}", _oc[0].ARBDroopLink_x);
            DroopLinkBellCrankFLy.Text = String.Format("{0:000}", _oc[0].ARBDroopLink_y);
            DroopLinkBellCrankFLz.Text = String.Format("{0:000}", _oc[0].ARBDroopLink_z);
            DroopLinkLeverFLx.Text = String.Format("{0:000}", _oc[0].ARBDroopLink_x);
            DroopLinkLeverFLy.Text = String.Format("{0:000}", _oc[0].ARBDroopLink_y);
            DroopLinkLeverFLz.Text = String.Format("{0:000}", _oc[0].ARBDroopLink_z);

            ToeLinkChassisFLx.Text = Convert.ToString(((_oc[0].ToeLink_x))); ToeLinkChassisFLx.Text = String.Format("{0:0.000}", _oc[0].ToeLink_x);
            ToeLinkChassisFLy.Text = Convert.ToString(((_oc[0].ToeLink_y))); ToeLinkChassisFLy.Text = String.Format("{0:0.000}", _oc[0].ToeLink_y);
            ToeLinkChassisFLz.Text = Convert.ToString(((_oc[0].ToeLink_z))); ToeLinkChassisFLz.Text = String.Format("{0:0.000}", _oc[0].ToeLink_z);
            ToeLinkUprightFLx.Text = Convert.ToString(((_oc[0].ToeLink_x))); ToeLinkUprightFLx.Text = String.Format("{0:0.000}", _oc[0].ToeLink_x);
            ToeLinkUprightFLy.Text = Convert.ToString(((_oc[0].ToeLink_y))); ToeLinkUprightFLy.Text = String.Format("{0:0.000}", _oc[0].ToeLink_y);
            ToeLinkUprightFLz.Text = Convert.ToString(((_oc[0].ToeLink_z))); ToeLinkUprightFLz.Text = String.Format("{0:0.000}", _oc[0].ToeLink_z);
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightFLx.Text = Convert.ToString(((_oc[0].LBJ_x))); LowerFrontUprightFLx.Text = String.Format("{0:0.000}", _oc[0].LBJ_x);
            LowerFrontUprightFLy.Text = Convert.ToString(((_oc[0].LBJ_y))); LowerFrontUprightFLy.Text = String.Format("{0:0.000}", _oc[0].LBJ_y);
            LowerFrontUprightFLz.Text = Convert.ToString(((_oc[0].LBJ_z))); LowerFrontUprightFLz.Text = String.Format("{0:0.000}", _oc[0].LBJ_z);
            LowerRearUprightFLx.Text = Convert.ToString(((_oc[0].LBJ_x))); LowerRearUprightFLx.Text = String.Format("{0:0.000}", _oc[0].LBJ_x);
            LowerRearUprightFLy.Text = Convert.ToString(((_oc[0].LBJ_y))); LowerRearUprightFLy.Text = String.Format("{0:0.000}", _oc[0].LBJ_y);
            LowerRearUprightFLz.Text = Convert.ToString(((_oc[0].LBJ_z))); LowerRearUprightFLz.Text = String.Format("{0:0.000}", _oc[0].LBJ_z);
            UpperFrontUprightFLx.Text = Convert.ToString(((_oc[0].UBJ_x))); UpperFrontUprightFLx.Text = String.Format("{0:0.000}", _oc[0].UBJ_x);
            UpperFrontUprightFLy.Text = Convert.ToString(((_oc[0].UBJ_y))); UpperFrontUprightFLy.Text = String.Format("{0:0.000}", _oc[0].UBJ_y);
            UpperFrontUprightFLz.Text = Convert.ToString(((_oc[0].UBJ_z))); UpperFrontUprightFLz.Text = String.Format("{0:0.000}", _oc[0].UBJ_z);
            UpperRearUprightFLx.Text = Convert.ToString(((_oc[0].UBJ_x))); UpperRearUprightFLx.Text = String.Format("{0:0.000}", _oc[0].UBJ_x);
            UpperRearUprightFLy.Text = Convert.ToString(((_oc[0].UBJ_y))); UpperRearUprightFLy.Text = String.Format("{0:0.000}", _oc[0].UBJ_y);
            UpperRearUprightFLz.Text = Convert.ToString(((_oc[0].UBJ_z))); UpperRearUprightFLz.Text = String.Format("{0:0.000}", _oc[0].UBJ_z);

            // Link Lengths
            LowerFrontLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].LowerFrontLength);
            LowerRearLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].LowerRearLength);
            UpperFrontLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].UpperFrontLength);
            UpperRearLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].UpperRearLength);
            PushRodLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].PushRodLength);
            ToeLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].ToeLinkLength);
            ARBDroopLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].ARBDroopLinkLength);
            DamperLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].DamperLength);
            ARBLeverLinkLengthFL.Text = String.Format("{0:0.000}", _sc[0].ARBBladeLength);

            #endregion

            #region Display of Outputs of FRONT RIGHT
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xFR.Text = Convert.ToString(((_oc[1].A2x))); A2xFR.Text = String.Format("{0:0.000}", _oc[1].A2x);
            A2yFR.Text = Convert.ToString(((_oc[1].A2y))); A2yFR.Text = String.Format("{0:0.000}", _oc[1].A2y);
            A2zFR.Text = Convert.ToString(((_oc[1].A2z))); A2zFR.Text = String.Format("{0:0.000}", _oc[1].A2z);
            B2xFR.Text = Convert.ToString(((_oc[1].B2x))); B2xFR.Text = String.Format("{0:0.000}", _oc[1].B2x);
            B2yFR.Text = Convert.ToString(((_oc[1].B2y))); B2yFR.Text = String.Format("{0:0.000}", _oc[1].B2y);
            B2zFR.Text = Convert.ToString(((_oc[1].B2z))); B2zFR.Text = String.Format("{0:0.000}", _oc[1].B2z);
            C2xFR.Text = Convert.ToString(((_oc[1].C2x))); C2xFR.Text = String.Format("{0:0.000}", _oc[1].C2x);
            C2yFR.Text = Convert.ToString(((_oc[1].C2y))); C2yFR.Text = String.Format("{0:0.000}", _oc[1].C2y);
            C2zFR.Text = Convert.ToString(((_oc[1].C2z))); C2zFR.Text = String.Format("{0:0.000}", _oc[1].C2z);
            D2xFR.Text = Convert.ToString(((_oc[1].D2x))); D2xFR.Text = String.Format("{0:0.000}", _oc[1].D2x);
            D2yFR.Text = Convert.ToString(((_oc[1].D2y))); D2yFR.Text = String.Format("{0:0.000}", _oc[1].D2y);
            D2zFR.Text = Convert.ToString(((_oc[1].D2z))); D2zFR.Text = String.Format("{0:0.000}", _oc[1].D2z);
            N2xFR.Text = Convert.ToString(((_oc[1].N2x))); N2xFR.Text = String.Format("{0:0.000}", _oc[1].N2x);
            N2yFR.Text = Convert.ToString(((_oc[1].N2y))); N2yFR.Text = String.Format("{0:0.000}", _oc[1].N2y);
            N2zFR.Text = Convert.ToString(((_oc[1].N2z))); N2zFR.Text = String.Format("{0:0.000}", _oc[1].N2z);
            Q2xFR.Text = Convert.ToString(((_oc[1].Q2x))); Q2xFR.Text = String.Format("{0:0.000}", _oc[1].Q2x);
            Q2yFR.Text = Convert.ToString(((_oc[1].Q2y))); Q2yFR.Text = String.Format("{0:0.000}", _oc[1].Q2y);
            Q2zFR.Text = Convert.ToString(((_oc[1].Q2z))); Q2zFR.Text = String.Format("{0:0.000}", _oc[1].Q2z);
            I2xFR.Text = Convert.ToString(((_oc[1].I2x))); I2xFR.Text = String.Format("{0:0.000}", _oc[1].I2x);
            I2yFR.Text = Convert.ToString(((_oc[1].I2y))); I2yFR.Text = String.Format("{0:0.000}", _oc[1].I2y);
            I2zFR.Text = Convert.ToString(((_oc[1].I2z))); I2zFR.Text = String.Format("{0:0.000}", _oc[1].I2z);
            JO2xFR.Text = Convert.ToString(((_oc[1].JO2x))); JO2xFR.Text = String.Format("{0:0.000}", _oc[1].JO2x);
            JO2yFR.Text = Convert.ToString(((_oc[1].JO2y))); JO2yFR.Text = String.Format("{0:0.000}", _oc[1].JO2y);
            JO2zFR.Text = Convert.ToString(((_oc[1].JO2z))); JO2zFR.Text = String.Format("{0:0.000}", _oc[1].JO2z);
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xFR.Text = Convert.ToString(((_oc[1].J2x))); J2xFR.Text = String.Format("{0:0.000}", _oc[1].J2x);
            J2yFR.Text = Convert.ToString(((_oc[1].J2y))); J2yFR.Text = String.Format("{0:0.000}", _oc[1].J2y);
            J2zFR.Text = Convert.ToString(((_oc[1].J2z))); J2zFR.Text = String.Format("{0:0.000}", _oc[1].J2z);
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xFR.Text = Convert.ToString(((_oc[1].H2x))); H2xFR.Text = String.Format("{0:0.000}", _oc[1].H2x);
            H2yFR.Text = Convert.ToString(((_oc[1].H2y))); H2yFR.Text = String.Format("{0:0.000}", _oc[1].H2y);
            H2zFR.Text = Convert.ToString(((_oc[1].H2z))); H2zFR.Text = String.Format("{0:0.000}", _oc[1].H2z);
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xFR.Text = Convert.ToString(((_oc[1].O2x))); O2xFR.Text = String.Format("{0:0.000}", _oc[1].O2x);
            O2yFR.Text = Convert.ToString(((_oc[1].O2y))); O2yFR.Text = String.Format("{0:0.000}", _oc[1].O2y);
            O2zFR.Text = Convert.ToString(((_oc[1].O2z))); O2zFR.Text = String.Format("{0:0.000}", _oc[1].O2z);
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xFR.Text = Convert.ToString(((_oc[1].G2x))); G2xFR.Text = String.Format("{0:0.000}", _oc[1].G2x);
            G2yFR.Text = Convert.ToString(((_oc[1].G2y))); G2yFR.Text = String.Format("{0:0.000}", _oc[1].G2y);
            G2zFR.Text = Convert.ToString(((_oc[1].G2z))); G2zFR.Text = String.Format("{0:0.000}", _oc[1].G2z);
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xFR.Text = Convert.ToString(((_oc[1].F2x))); F2xFR.Text = String.Format("{0:0.000}", _oc[1].F2x);
            F2yFR.Text = Convert.ToString(((_oc[1].F2y))); F2yFR.Text = String.Format("{0:0.000}", _oc[1].F2y);
            F2zFR.Text = Convert.ToString(((_oc[1].F2z))); F2zFR.Text = String.Format("{0:0.000}", _oc[1].F2z);
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xFR.Text = Convert.ToString(((_oc[1].E2x))); E2xFR.Text = String.Format("{0:0.000}", _oc[1].E2x);
            E2yFR.Text = Convert.ToString(((_oc[1].E2y))); E2yFR.Text = String.Format("{0:0.000}", _oc[1].E2y);
            E2zFR.Text = Convert.ToString(((_oc[1].E2z))); E2zFR.Text = String.Format("{0:0.000}", _oc[1].E2z);
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioFR.Text = Convert.ToString(((_oc[1].InitialMR))); MotionRatioFR.Text = String.Format("{0:0.000}", _oc[1].InitialMR);
            InitialARBMRFR.Text = String.Format("{0:0.000}", _oc[1].Initial_ARB_MR);
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioFR.Text = Convert.ToString(((_oc[1].FinalMR))); FinalMotionRatioFR.Text = String.Format("{0:0.000}", _oc[1].FinalMR);
            FinalARBMRFR.Text = String.Format("{0:0.000}", _oc[1].Final_ARB_MR);
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xFR.Text = Convert.ToString(((_oc[1].M2x))); M2xFR.Text = String.Format("{0:0.000}", _oc[1].M2x);
            M2yFR.Text = Convert.ToString(((_oc[1].M2y))); M2yFR.Text = String.Format("{0:0.000}", _oc[1].M2y);
            M2zFR.Text = Convert.ToString(((_oc[1].M2z))); M2zFR.Text = String.Format("{0:0.000}", _oc[1].M2z);
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xFR.Text = Convert.ToString(((_oc[1].K2x))); K2xFR.Text = String.Format("{0:0.000}", _oc[1].K2x);
            K2yFR.Text = Convert.ToString(((_oc[1].K2y))); K2yFR.Text = String.Format("{0:0.000}", _oc[1].K2y);
            K2zFR.Text = Convert.ToString(((_oc[1].K2z))); K2zFR.Text = String.Format("{0:0.000}", _oc[1].K2z);
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xFR.Text = Convert.ToString(((_oc[1].L2x))); L2xFR.Text = String.Format("{0:0.000}", _oc[1].L2x);
            L2yFR.Text = Convert.ToString(((_oc[1].L2y))); L2yFR.Text = String.Format("{0:0.000}", _oc[1].L2y);
            L2zFR.Text = Convert.ToString(((_oc[1].L2z))); L2zFR.Text = String.Format("{0:0.000}", _oc[1].L2z);
            //To Display the New Camber and Toe
            FinalCamberFR.Text = Convert.ToString(((_oc[1].FinalCamber))); FinalCamberFR.Text = String.Format("{0:0.000}", (_oc[1].FinalCamber * (180 / Math.PI)));
            FinalToeFR.Text = Convert.ToString(((_oc[1].FinalToe))); FinalToeFR.Text = String.Format("{0:0.000}", (_oc[1].FinalToe * (180 / Math.PI)));
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xFR.Text = Convert.ToString(((_oc[1].P2x))); P2xFR.Text = String.Format("{0:0.000}", _oc[1].P2x);
            P2yFR.Text = Convert.ToString(((_oc[1].P2y))); P2yFR.Text = String.Format("{0:0.000}", _oc[1].P2y);
            P2zFR.Text = Convert.ToString(((_oc[1].P2z))); P2zFR.Text = String.Format("{0:0.000}", _oc[1].P2z);
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xFR.Text = Convert.ToString(((_oc[1].W2x))); W2xFR.Text = String.Format("{0:0.000}", _oc[1].W2x);
            W2yFR.Text = Convert.ToString(((_oc[1].W2y))); W2yFR.Text = String.Format("{0:0.000}", _oc[1].W2y);
            W2zFR.Text = Convert.ToString(((_oc[1].W2z))); W2zFR.Text = String.Format("{0:0.000}", _oc[1].W2z);
            //Calculating The Final Ride Height 
            RideHeightFR.Text = Convert.ToString(((_oc[1].FinalRideHeight))); RideHeightFR.Text = String.Format("{0:0.000}", _oc[1].FinalRideHeight);
            //Calculating the New Corner Weights
            CWFR.Text = Convert.ToString(((_oc[1].CW))); CWFR.Text = String.Format("{0:0.000}", _oc[1].CW);
            TireLoadedRadiusFR.Text = Convert.ToString(((_oc[1].TireLoadedRadius))); TireLoadedRadiusFR.Text = String.Format("{0:0.000}", _oc[1].TireLoadedRadius);
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionFR.Text = Convert.ToString(((_oc[1].Corrected_SpringDeflection))); CorrectedSpringDeflectionFR.Text = String.Format("{0:0.000}", _oc[1].Corrected_SpringDeflection);
            CorrectedWheelDeflectionFR.Text = Convert.ToString(((_oc[1].Corrected_WheelDeflection))); CorrectedWheelDeflectionFR.Text = String.Format("{0:0.000}", _oc[1].Corrected_WheelDeflection);
            NewDamperLengthFR.Text = Convert.ToString(((_oc[1].DamperLength))); NewDamperLengthFR.Text = String.Format("{0:0.000}", _oc[1].DamperLength);
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGFRx.Text = Convert.ToString(((_oc[1].New_NonSuspendedMassCoGx))); NewNSMCGFRx.Text = String.Format("{0:0.000}", _oc[1].New_NonSuspendedMassCoGx);
            NewNSMCGFRy.Text = Convert.ToString(((_oc[1].New_NonSuspendedMassCoGy))); NewNSMCGFRy.Text = String.Format("{0:0.000}", _oc[1].New_NonSuspendedMassCoGy);
            NewNSMCGFRz.Text = Convert.ToString(((_oc[1].New_NonSuspendedMassCoGz))); NewNSMCGFRz.Text = String.Format("{0:0.000}", _oc[1].New_NonSuspendedMassCoGz);
            //Calculating the Wishbone Forces
            LowerFrontFR.Text = Convert.ToString(((_oc[1].LowerFront))); LowerFrontFR.Text = String.Format("{0:0.000}", _oc[1].LowerFront);
            LowerRearFR.Text = Convert.ToString(((_oc[1].LowerRear))); LowerRearFR.Text = String.Format("{0:0.000}", _oc[1].LowerRear);
            UpperFrontFR.Text = Convert.ToString(((_oc[1].UpperFront))); UpperFrontFR.Text = String.Format("{0:0.000}", _oc[1].UpperFront);
            UpperRearFR.Text = Convert.ToString(((_oc[1].UpperRear))); UpperRearFR.Text = String.Format("{0:0.000}", _oc[1].UpperRear);
            PushRodFR.Text = Convert.ToString(((_oc[1].PushRod))); PushRodFR.Text = String.Format("{0:0.000}", _oc[1].PushRod);
            ToeLinkFR.Text = Convert.ToString(((_oc[1].ToeLink))); ToeLinkFR.Text = String.Format("{0:0.000}", _oc[1].ToeLink);
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisFRx.Text = Convert.ToString(((_oc[1].LowerFront_x))); LowerFrontChassisFRx.Text = String.Format("{0:0.000}", _oc[1].LowerFront_x);
            LowerFrontChassisFRy.Text = Convert.ToString(((_oc[1].LowerFront_y))); LowerFrontChassisFRy.Text = String.Format("{0:0.000}", _oc[1].LowerFront_y);
            LowerFrontChassisFRz.Text = Convert.ToString(((_oc[1].LowerFront_z))); LowerFrontChassisFRz.Text = String.Format("{0:0.000}", _oc[1].LowerFront_z);
            LowerRearChassisFRx.Text = Convert.ToString(((_oc[1].LowerRear_x))); LowerRearChassisFRx.Text = String.Format("{0:0.000}", _oc[1].LowerRear_x);
            LowerRearChassisFRy.Text = Convert.ToString(((_oc[1].LowerRear_y))); LowerRearChassisFRy.Text = String.Format("{0:0.000}", _oc[1].LowerRear_y);
            LowerRearChassisFRz.Text = Convert.ToString(((_oc[1].LowerRear_z))); LowerRearChassisFRz.Text = String.Format("{0:0.000}", _oc[1].LowerRear_z);
            UpperFrontChassisFRx.Text = Convert.ToString(((_oc[1].UpperFront_x))); UpperFrontChassisFRx.Text = String.Format("{0:0.000}", _oc[1].UpperFront_x);
            UpperFrontChassisFRy.Text = Convert.ToString(((_oc[1].UpperFront_y))); UpperFrontChassisFRy.Text = String.Format("{0:0.000}", _oc[1].UpperFront_y);
            UpperFrontChassisFRz.Text = Convert.ToString(((_oc[1].UpperFront_z))); UpperFrontChassisFRz.Text = String.Format("{0:0.000}", _oc[1].UpperFront_z);
            UpperRearChassisFRx.Text = Convert.ToString(((_oc[1].UpperRear_x))); UpperRearChassisFRx.Text = String.Format("{0:0.000}", _oc[1].UpperRear_x);
            UpperRearChassisFRy.Text = Convert.ToString(((_oc[1].UpperRear_y))); UpperRearChassisFRy.Text = String.Format("{0:0.000}", _oc[1].UpperRear_y);
            UpperRearChassisFRz.Text = Convert.ToString(((_oc[1].UpperRear_z))); UpperRearChassisFRz.Text = String.Format("{0:0.000}", _oc[1].UpperRear_z);
            PushRodChassisFRx.Text = Convert.ToString(((_oc[1].PushRod_x))); PushRodChassisFRx.Text = String.Format("{0:0.000}", _oc[1].PushRod_x);
            PushRodChassisFRy.Text = Convert.ToString(((_oc[1].PushRod_y))); PushRodChassisFRy.Text = String.Format("{0:0.000}", _oc[1].PushRod_y);
            PushRodChassisFRz.Text = Convert.ToString(((_oc[1].PushRod_z))); PushRodChassisFRz.Text = String.Format("{0:0.000}", _oc[1].PushRod_z);
            PushRodUprightFRx.Text = Convert.ToString(((_oc[1].PushRod_x))); PushRodUprightFRx.Text = String.Format("{0:0.000}", _oc[1].PushRod_x);
            PushRodUprightFRy.Text = Convert.ToString(((_oc[1].PushRod_y))); PushRodUprightFRy.Text = String.Format("{0:0.000}", _oc[1].PushRod_y);
            PushRodUprightFRz.Text = Convert.ToString(((_oc[1].PushRod_z))); PushRodUprightFRz.Text = String.Format("{0:0.000}", _oc[1].PushRod_z);
            DamperForceFR.Text = String.Format("{0:0.000}", _oc[1].DamperForce);
            SpringPreloadOutputFR.Text = String.Format("{0:0.000}", _spring[1].SpringPreload * _spring[1].PreloadForce);
            DamperForceChassisFRx.Text = String.Format("{0:0.000}", _oc[1].DamperForce_x);
            DamperForceChassisFRy.Text = String.Format("{0:0.000}", _oc[1].DamperForce_y);
            DamperForceChassisFRz.Text = String.Format("{0:0.000}", _oc[1].DamperForce_z);
            DamperForceBellCrankFRx.Text = String.Format("{0:0.000}", _oc[1].DamperForce_x);
            DamperForceBellCrankFRy.Text = String.Format("{0:0.000}", _oc[1].DamperForce_y);
            DamperForceBellCrankFRz.Text = String.Format("{0:0.000}", _oc[1].DamperForce_z);

            DroopLinkForceFR.Text = String.Format("{0:000}", _oc[1].ARBDroopLink);
            DroopLinkBellCrankFRx.Text = String.Format("{0:000}", _oc[1].ARBDroopLink_x);
            DroopLinkBellCrankFRy.Text = String.Format("{0:000}", _oc[1].ARBDroopLink_y);
            DroopLinkBellCrankFRz.Text = String.Format("{0:000}", _oc[1].ARBDroopLink_z);
            DroopLinkLeverFRx.Text = String.Format("{0:000}", _oc[1].ARBDroopLink_x);
            DroopLinkLeverFRy.Text = String.Format("{0:000}", _oc[1].ARBDroopLink_y);
            DroopLinkLeverFRz.Text = String.Format("{0:000}", _oc[1].ARBDroopLink_z);

            ToeLinkChassisFRx.Text = Convert.ToString(((_oc[1].ToeLink_x))); ToeLinkChassisFRx.Text = String.Format("{0:0.000}", _oc[1].ToeLink_x);
            ToeLinkChassisFRy.Text = Convert.ToString(((_oc[1].ToeLink_y))); ToeLinkChassisFRy.Text = String.Format("{0:0.000}", _oc[1].ToeLink_y);
            ToeLinkChassisFRz.Text = Convert.ToString(((_oc[1].ToeLink_z))); ToeLinkChassisFRz.Text = String.Format("{0:0.000}", _oc[1].ToeLink_z);
            ToeLinkUprightFRx.Text = Convert.ToString(((_oc[1].ToeLink_x))); ToeLinkUprightFRx.Text = String.Format("{0:0.000}", _oc[1].ToeLink_x);
            ToeLinkUprightFRy.Text = Convert.ToString(((_oc[1].ToeLink_y))); ToeLinkUprightFRy.Text = String.Format("{0:0.000}", _oc[1].ToeLink_y);
            ToeLinkUprightFRz.Text = Convert.ToString(((_oc[1].ToeLink_z))); ToeLinkUprightFRz.Text = String.Format("{0:0.000}", _oc[1].ToeLink_z);
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightFRx.Text = Convert.ToString(((_oc[1].LBJ_x))); LowerFrontUprightFRx.Text = String.Format("{0:0.000}", _oc[1].LBJ_x);
            LowerFrontUprightFRy.Text = Convert.ToString(((_oc[1].LBJ_y))); LowerFrontUprightFRy.Text = String.Format("{0:0.000}", _oc[1].LBJ_y);
            LowerFrontUprightFRz.Text = Convert.ToString(((_oc[1].LBJ_z))); LowerFrontUprightFRz.Text = String.Format("{0:0.000}", _oc[1].LBJ_z);
            LowerRearUprightFRx.Text = Convert.ToString(((_oc[1].LBJ_x))); LowerRearUprightFRx.Text = String.Format("{0:0.000}", _oc[1].LBJ_x);
            LowerRearUprightFRy.Text = Convert.ToString(((_oc[1].LBJ_y))); LowerRearUprightFRy.Text = String.Format("{0:0.000}", _oc[1].LBJ_y);
            LowerRearUprightFRz.Text = Convert.ToString(((_oc[1].LBJ_z))); LowerRearUprightFRz.Text = String.Format("{0:0.000}", _oc[1].LBJ_z);
            UpperFrontUprightFRx.Text = Convert.ToString(((_oc[1].UBJ_x))); UpperFrontUprightFRx.Text = String.Format("{0:0.000}", _oc[1].UBJ_x);
            UpperFrontUprightFRy.Text = Convert.ToString(((_oc[1].UBJ_y))); UpperFrontUprightFRy.Text = String.Format("{0:0.000}", _oc[1].UBJ_y);
            UpperFrontUprightFRz.Text = Convert.ToString(((_oc[1].UBJ_z))); UpperFrontUprightFRz.Text = String.Format("{0:0.000}", _oc[1].UBJ_z);
            UpperRearUprightFRx.Text = Convert.ToString(((_oc[1].UBJ_x))); UpperRearUprightFRx.Text = String.Format("{0:0.000}", _oc[1].UBJ_x);
            UpperRearUprightFRy.Text = Convert.ToString(((_oc[1].UBJ_y))); UpperRearUprightFRy.Text = String.Format("{0:0.000}", _oc[1].UBJ_y);
            UpperRearUprightFRz.Text = Convert.ToString(((_oc[1].UBJ_z))); UpperRearUprightFRz.Text = String.Format("{0:0.000}", _oc[1].UBJ_z);

            // Link Lengths
            LowerFrontLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].LowerFrontLength);
            LowerRearLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].LowerRearLength);
            UpperFrontLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].UpperFrontLength);
            UpperRearLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].UpperRearLength);
            PushRodLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].PushRodLength);
            ToeLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].ToeLinkLength);
            ARBDroopLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].ARBDroopLinkLength);
            DamperLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].DamperLength);
            ARBLeverLinkLengthFR.Text = String.Format("{0:0.000}", _sc[1].ARBBladeLength);

            #endregion

            #region Display of Outputs of REAR LEFT
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xRL.Text = Convert.ToString(((_oc[2].A2x))); A2xRL.Text = String.Format("{0:0.000}", _oc[2].A2x);
            A2yRL.Text = Convert.ToString(((_oc[2].A2y))); A2yRL.Text = String.Format("{0:0.000}", _oc[2].A2y);
            A2zRL.Text = Convert.ToString(((_oc[2].A2z))); A2zRL.Text = String.Format("{0:0.000}", _oc[2].A2z);
            B2xRL.Text = Convert.ToString(((_oc[2].B2x))); B2xRL.Text = String.Format("{0:0.000}", _oc[2].B2x);
            B2yRL.Text = Convert.ToString(((_oc[2].B2y))); B2yRL.Text = String.Format("{0:0.000}", _oc[2].B2y);
            B2zRL.Text = Convert.ToString(((_oc[2].B2z))); B2zRL.Text = String.Format("{0:0.000}", _oc[2].B2z);
            C2xRL.Text = Convert.ToString(((_oc[2].C2x))); C2xRL.Text = String.Format("{0:0.000}", _oc[2].C2x);
            C2yRL.Text = Convert.ToString(((_oc[2].C2y))); C2yRL.Text = String.Format("{0:0.000}", _oc[2].C2y);
            C2zRL.Text = Convert.ToString(((_oc[2].C2z))); C2zRL.Text = String.Format("{0:0.000}", _oc[2].C2z);
            D2xRL.Text = Convert.ToString(((_oc[2].D2x))); D2xRL.Text = String.Format("{0:0.000}", _oc[2].D2x);
            D2yRL.Text = Convert.ToString(((_oc[2].D2y))); D2yRL.Text = String.Format("{0:0.000}", _oc[2].D2y);
            D2zRL.Text = Convert.ToString(((_oc[2].D2z))); D2zRL.Text = String.Format("{0:0.000}", _oc[2].D2z);
            N2xRL.Text = Convert.ToString(((_oc[2].N2x))); N2xRL.Text = String.Format("{0:0.000}", _oc[2].N2x);
            N2yRL.Text = Convert.ToString(((_oc[2].N2y))); N2yRL.Text = String.Format("{0:0.000}", _oc[2].N2y);
            N2zRL.Text = Convert.ToString(((_oc[2].N2z))); N2zRL.Text = String.Format("{0:0.000}", _oc[2].N2z);
            Q2xRL.Text = Convert.ToString(((_oc[2].Q2x))); Q2xRL.Text = String.Format("{0:0.000}", _oc[2].Q2x);
            Q2yRL.Text = Convert.ToString(((_oc[2].Q2y))); Q2yRL.Text = String.Format("{0:0.000}", _oc[2].Q2y);
            Q2zRL.Text = Convert.ToString(((_oc[2].Q2z))); Q2zRL.Text = String.Format("{0:0.000}", _oc[2].Q2z);
            I2xRL.Text = Convert.ToString(((_oc[2].I2x))); I2xRL.Text = String.Format("{0:0.000}", _oc[2].I2x);
            I2yRL.Text = Convert.ToString(((_oc[2].I2y))); I2yRL.Text = String.Format("{0:0.000}", _oc[2].I2y);
            I2zRL.Text = Convert.ToString(((_oc[2].I2z))); I2zRL.Text = String.Format("{0:0.000}", _oc[2].I2z);
            JO2xRL.Text = Convert.ToString(((_oc[2].JO2x))); JO2xRL.Text = String.Format("{0:0.000}", _oc[2].JO2x);
            JO2yRL.Text = Convert.ToString(((_oc[2].JO2y))); JO2yRL.Text = String.Format("{0:0.000}", _oc[2].JO2y);
            JO2zRL.Text = Convert.ToString(((_oc[2].JO2z))); JO2zRL.Text = String.Format("{0:0.000}", _oc[2].JO2z);
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xRL.Text = Convert.ToString(((_oc[2].J2x))); J2xRL.Text = String.Format("{0:0.000}", _oc[2].J2x);
            J2yRL.Text = Convert.ToString(((_oc[2].J2y))); J2yRL.Text = String.Format("{0:0.000}", _oc[2].J2y);
            J2zRL.Text = Convert.ToString(((_oc[2].J2z))); J2zRL.Text = String.Format("{0:0.000}", _oc[2].J2z);
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xRL.Text = Convert.ToString(((_oc[2].H2x))); H2xRL.Text = String.Format("{0:0.000}", _oc[2].H2x);
            H2yRL.Text = Convert.ToString(((_oc[2].H2y))); H2yRL.Text = String.Format("{0:0.000}", _oc[2].H2y);
            H2zRL.Text = Convert.ToString(((_oc[2].H2z))); H2zRL.Text = String.Format("{0:0.000}", _oc[2].H2z);
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xRL.Text = Convert.ToString(((_oc[2].O2x))); O2xRL.Text = String.Format("{0:0.000}", _oc[2].O2x);
            O2yRL.Text = Convert.ToString(((_oc[2].O2y))); O2yRL.Text = String.Format("{0:0.000}", _oc[2].O2y);
            O2zRL.Text = Convert.ToString(((_oc[2].O2z))); O2zRL.Text = String.Format("{0:0.000}", _oc[2].O2z);
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xRL.Text = Convert.ToString(((_oc[2].G2x))); G2xRL.Text = String.Format("{0:0.000}", _oc[2].G2x);
            G2yRL.Text = Convert.ToString(((_oc[2].G2y))); G2yRL.Text = String.Format("{0:0.000}", _oc[2].G2y);
            G2zRL.Text = Convert.ToString(((_oc[2].G2z))); G2zRL.Text = String.Format("{0:0.000}", _oc[2].G2z);
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xRL.Text = Convert.ToString(((_oc[2].F2x))); F2xRL.Text = String.Format("{0:0.000}", _oc[2].F2x);
            F2yRL.Text = Convert.ToString(((_oc[2].F2y))); F2yRL.Text = String.Format("{0:0.000}", _oc[2].F2y);
            F2zRL.Text = Convert.ToString(((_oc[2].F2z))); F2zRL.Text = String.Format("{0:0.000}", _oc[2].F2z);
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xRL.Text = Convert.ToString(((_oc[2].E2x))); E2xRL.Text = String.Format("{0:0.000}", _oc[2].E2x);
            E2yRL.Text = Convert.ToString(((_oc[2].E2y))); E2yRL.Text = String.Format("{0:0.000}", _oc[2].E2y);
            E2zRL.Text = Convert.ToString(((_oc[2].E2z))); E2zRL.Text = String.Format("{0:0.000}", _oc[2].E2z);
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioRL.Text = Convert.ToString(((_oc[2].InitialMR))); MotionRatioRL.Text = String.Format("{0:0.000}", _oc[2].InitialMR);
            InitialARBMRRL.Text = String.Format("{0:0.000}", _oc[2].Initial_ARB_MR);
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioRL.Text = Convert.ToString(((_oc[2].FinalMR))); FinalMotionRatioRL.Text = String.Format("{0:0.000}", _oc[2].FinalMR);
            FinalARBMRRL.Text = String.Format("{0:0.000}", _oc[2].Final_ARB_MR);
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xRL.Text = Convert.ToString(((_oc[2].M2x))); M2xRL.Text = String.Format("{0:0.000}", _oc[2].M2x);
            M2yRL.Text = Convert.ToString(((_oc[2].M2y))); M2yRL.Text = String.Format("{0:0.000}", _oc[2].M2y);
            M2zRL.Text = Convert.ToString(((_oc[2].M2z))); M2zRL.Text = String.Format("{0:0.000}", _oc[2].M2z);
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xRL.Text = Convert.ToString(((_oc[2].K2x))); K2xRL.Text = String.Format("{0:0.000}", _oc[2].K2x);
            K2yRL.Text = Convert.ToString(((_oc[2].K2y))); K2yRL.Text = String.Format("{0:0.000}", _oc[2].K2y);
            K2zRL.Text = Convert.ToString(((_oc[2].K2z))); K2zRL.Text = String.Format("{0:0.000}", _oc[2].K2z);
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xRL.Text = Convert.ToString(((_oc[2].L2x))); L2xRL.Text = String.Format("{0:0.000}", _oc[2].L2x);
            L2yRL.Text = Convert.ToString(((_oc[2].L2y))); L2yRL.Text = String.Format("{0:0.000}", _oc[2].L2y);
            L2zRL.Text = Convert.ToString(((_oc[2].L2z))); L2zRL.Text = String.Format("{0:0.000}", _oc[2].L2z);
            //To Display the New Camber and Toe
            FinalCamberRL.Text = Convert.ToString(((_oc[2].FinalCamber))); FinalCamberRL.Text = String.Format("{0:0.000}", (_oc[2].FinalCamber * (180 / Math.PI)));
            FinalToeRL.Text = Convert.ToString(((_oc[2].FinalToe))); FinalToeRL.Text = String.Format("{0:0.000}", (_oc[2].FinalToe * (180 / Math.PI)));
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xRL.Text = Convert.ToString(((_oc[2].P2x))); P2xRL.Text = String.Format("{0:0.000}", _oc[2].P2x);
            P2yRL.Text = Convert.ToString(((_oc[2].P2y))); P2yRL.Text = String.Format("{0:0.000}", _oc[2].P2y);
            P2zRL.Text = Convert.ToString(((_oc[2].P2z))); P2zRL.Text = String.Format("{0:0.000}", _oc[2].P2z);
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xRL.Text = Convert.ToString(((_oc[2].W2x))); W2xRL.Text = String.Format("{0:0.000}", _oc[2].W2x);
            W2yRL.Text = Convert.ToString(((_oc[2].W2y))); W2yRL.Text = String.Format("{0:0.000}", _oc[2].W2y);
            W2zRL.Text = Convert.ToString(((_oc[2].W2z))); W2zRL.Text = String.Format("{0:0.000}", _oc[2].W2z);
            //Calculating The Final Ride Height 
            RideHeightRL.Text = Convert.ToString(((_oc[2].FinalRideHeight))); RideHeightRL.Text = String.Format("{0:0.000}", _oc[2].FinalRideHeight);
            //Calculating the New Corner Weights
            CWRL.Text = Convert.ToString(((_oc[2].CW))); CWRL.Text = String.Format("{0:0.000}", _oc[2].CW);
            TireLoadedRadiusRL.Text = Convert.ToString(((_oc[2].TireLoadedRadius))); TireLoadedRadiusRL.Text = String.Format("{0:0.000}", _oc[2].TireLoadedRadius);
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionRL.Text = Convert.ToString(((_oc[2].Corrected_SpringDeflection))); CorrectedSpringDeflectionRL.Text = String.Format("{0:0.000}", _oc[2].Corrected_SpringDeflection);
            CorrectedWheelDeflectionRL.Text = Convert.ToString(((_oc[2].Corrected_WheelDeflection))); CorrectedWheelDeflectionRL.Text = String.Format("{0:0.000}", _oc[2].Corrected_WheelDeflection);
            NewDamperLengthRL.Text = Convert.ToString(((_oc[2].DamperLength))); NewDamperLengthRL.Text = String.Format("{0:0.000}", _oc[2].DamperLength);
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGRLx.Text = Convert.ToString(((_oc[2].New_NonSuspendedMassCoGx))); NewNSMCGRLx.Text = String.Format("{0:0.000}", _oc[2].New_NonSuspendedMassCoGx);
            NewNSMCGRLy.Text = Convert.ToString(((_oc[2].New_NonSuspendedMassCoGy))); NewNSMCGRLy.Text = String.Format("{0:0.000}", _oc[2].New_NonSuspendedMassCoGy);
            NewNSMCGRLz.Text = Convert.ToString(((_oc[2].New_NonSuspendedMassCoGz))); NewNSMCGRLz.Text = String.Format("{0:0.000}", _oc[2].New_NonSuspendedMassCoGz);
            //Calculating the Wishbone Forces
            LowerFrontRL.Text = Convert.ToString(((_oc[2].LowerFront))); LowerFrontRL.Text = String.Format("{0:0.000}", _oc[2].LowerFront);
            LowerRearRL.Text = Convert.ToString(((_oc[2].LowerRear))); LowerRearRL.Text = String.Format("{0:0.000}", _oc[2].LowerRear);
            UpperFrontRL.Text = Convert.ToString(((_oc[2].UpperFront))); UpperFrontRL.Text = String.Format("{0:0.000}", _oc[2].UpperFront);
            UpperRearRL.Text = Convert.ToString(((_oc[2].UpperRear))); UpperRearRL.Text = String.Format("{0:0.000}", _oc[2].UpperRear);
            PushRodRL.Text = Convert.ToString(((_oc[2].PushRod))); PushRodRL.Text = String.Format("{0:0.000}", _oc[2].PushRod);
            ToeLinkRL.Text = Convert.ToString(((_oc[2].ToeLink))); ToeLinkRL.Text = String.Format("{0:0.000}", _oc[2].ToeLink);
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisRLx.Text = Convert.ToString(((_oc[2].LowerFront_x))); LowerFrontChassisRLx.Text = String.Format("{0:0.000}", _oc[2].LowerFront_x);
            LowerFrontChassisRLy.Text = Convert.ToString(((_oc[2].LowerFront_y))); LowerFrontChassisRLy.Text = String.Format("{0:0.000}", _oc[2].LowerFront_y);
            LowerFrontChassisRLz.Text = Convert.ToString(((_oc[2].LowerFront_z))); LowerFrontChassisRLz.Text = String.Format("{0:0.000}", _oc[2].LowerFront_z);
            LowerRearChassisRLx.Text = Convert.ToString(((_oc[2].LowerRear_x))); LowerRearChassisRLx.Text = String.Format("{0:0.000}", _oc[2].LowerRear_x);
            LowerRearChassisRLy.Text = Convert.ToString(((_oc[2].LowerRear_y))); LowerRearChassisRLy.Text = String.Format("{0:0.000}", _oc[2].LowerRear_y);
            LowerRearChassisRLz.Text = Convert.ToString(((_oc[2].LowerRear_z))); LowerRearChassisRLz.Text = String.Format("{0:0.000}", _oc[2].LowerRear_z);
            UpperFrontChassisRLx.Text = Convert.ToString(((_oc[2].UpperFront_x))); UpperFrontChassisRLx.Text = String.Format("{0:0.000}", _oc[2].UpperFront_x);
            UpperFrontChassisRLy.Text = Convert.ToString(((_oc[2].UpperFront_y))); UpperFrontChassisRLy.Text = String.Format("{0:0.000}", _oc[2].UpperFront_y);
            UpperFrontChassisRLz.Text = Convert.ToString(((_oc[2].UpperFront_z))); UpperFrontChassisRLz.Text = String.Format("{0:0.000}", _oc[2].UpperFront_z);
            UpperRearChassisRLx.Text = Convert.ToString(((_oc[2].UpperRear_x))); UpperRearChassisRLx.Text = String.Format("{0:0.000}", _oc[2].UpperRear_x);
            UpperRearChassisRLy.Text = Convert.ToString(((_oc[2].UpperRear_y))); UpperRearChassisRLy.Text = String.Format("{0:0.000}", _oc[2].UpperRear_y);
            UpperRearChassisRLz.Text = Convert.ToString(((_oc[2].UpperRear_z))); UpperRearChassisRLz.Text = String.Format("{0:0.000}", _oc[2].UpperRear_z);
            PushRodChassisRLx.Text = Convert.ToString(((_oc[2].PushRod_x))); PushRodChassisRLx.Text = String.Format("{0:0.000}", _oc[2].PushRod_x);
            PushRodChassisRLy.Text = Convert.ToString(((_oc[2].PushRod_y))); PushRodChassisRLy.Text = String.Format("{0:0.000}", _oc[2].PushRod_y);
            PushRodChassisRLz.Text = Convert.ToString(((_oc[2].PushRod_z))); PushRodChassisRLz.Text = String.Format("{0:0.000}", _oc[2].PushRod_z);
            PushRodUprightRLx.Text = Convert.ToString(((_oc[2].PushRod_x))); PushRodUprightRLx.Text = String.Format("{0:0.000}", _oc[2].PushRod_x);
            PushRodUprightRLy.Text = Convert.ToString(((_oc[2].PushRod_y))); PushRodUprightRLy.Text = String.Format("{0:0.000}", _oc[2].PushRod_y);
            PushRodUprightRLz.Text = Convert.ToString(((_oc[2].PushRod_z))); PushRodUprightRLz.Text = String.Format("{0:0.000}", _oc[2].PushRod_z);
            DamperForceRL.Text = String.Format("{0:0.000}", _oc[2].DamperForce);
            SpringPreloadOutputRL.Text = String.Format("{0:0.000}", _spring[2].SpringPreload * _spring[2].PreloadForce);
            DamperForceChassisRLx.Text = String.Format("{0:0.000}", _oc[2].DamperForce_x);
            DamperForceChassisRLy.Text = String.Format("{0:0.000}", _oc[2].DamperForce_y);
            DamperForceChassisRLz.Text = String.Format("{0:0.000}", _oc[2].DamperForce_z);
            DamperForceBellCrankRLx.Text = String.Format("{0:0.000}", _oc[2].DamperForce_x);
            DamperForceBellCrankRLy.Text = String.Format("{0:0.000}", _oc[2].DamperForce_y);
            DamperForceBellCrankRLz.Text = String.Format("{0:0.000}", _oc[2].DamperForce_z);

            DroopLinkForceRL.Text = String.Format("{0:000}", _oc[2].ARBDroopLink);
            DroopLinkBellCrankRLx.Text = String.Format("{0:000}", _oc[2].ARBDroopLink_x);
            DroopLinkBellCrankRLy.Text = String.Format("{0:000}", _oc[2].ARBDroopLink_y);
            DroopLinkBellCrankRLz.Text = String.Format("{0:000}", _oc[2].ARBDroopLink_z);
            DroopLinkLeverRLx.Text = String.Format("{0:000}", _oc[2].ARBDroopLink_x);
            DroopLinkLeverRLy.Text = String.Format("{0:000}", _oc[2].ARBDroopLink_y);
            DroopLinkLeverRLz.Text = String.Format("{0:000}", _oc[2].ARBDroopLink_z);

            ToeLinkChassisRLx.Text = Convert.ToString(((_oc[2].ToeLink_x))); ToeLinkChassisRLx.Text = String.Format("{0:0.000}", _oc[2].ToeLink_x);
            ToeLinkChassisRLy.Text = Convert.ToString(((_oc[2].ToeLink_y))); ToeLinkChassisRLy.Text = String.Format("{0:0.000}", _oc[2].ToeLink_y);
            ToeLinkChassisRLz.Text = Convert.ToString(((_oc[2].ToeLink_z))); ToeLinkChassisRLz.Text = String.Format("{0:0.000}", _oc[2].ToeLink_z);
            ToeLinkUprightRLx.Text = Convert.ToString(((_oc[2].ToeLink_x))); ToeLinkUprightRLx.Text = String.Format("{0:0.000}", _oc[2].ToeLink_x);
            ToeLinkUprightRLy.Text = Convert.ToString(((_oc[2].ToeLink_y))); ToeLinkUprightRLy.Text = String.Format("{0:0.000}", _oc[2].ToeLink_y);
            ToeLinkUprightRLz.Text = Convert.ToString(((_oc[2].ToeLink_z))); ToeLinkUprightRLz.Text = String.Format("{0:0.000}", _oc[2].ToeLink_z);
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightRLx.Text = Convert.ToString(((_oc[2].LBJ_x))); LowerFrontUprightRLx.Text = String.Format("{0:0.000}", _oc[2].LBJ_x);
            LowerFrontUprightRLy.Text = Convert.ToString(((_oc[2].LBJ_y))); LowerFrontUprightRLy.Text = String.Format("{0:0.000}", _oc[2].LBJ_y);
            LowerFrontUprightRLz.Text = Convert.ToString(((_oc[2].LBJ_z))); LowerFrontUprightRLz.Text = String.Format("{0:0.000}", _oc[2].LBJ_z);
            LowerRearUprightRLx.Text = Convert.ToString(((_oc[2].LBJ_x))); LowerRearUprightRLx.Text = String.Format("{0:0.000}", _oc[2].LBJ_x);
            LowerRearUprightRLy.Text = Convert.ToString(((_oc[2].LBJ_y))); LowerRearUprightRLy.Text = String.Format("{0:0.000}", _oc[2].LBJ_y);
            LowerRearUprightRLz.Text = Convert.ToString(((_oc[2].LBJ_z))); LowerRearUprightRLz.Text = String.Format("{0:0.000}", _oc[2].LBJ_z);
            UpperFrontUprightRLx.Text = Convert.ToString(((_oc[2].UBJ_x))); UpperFrontUprightRLx.Text = String.Format("{0:0.000}", _oc[2].UBJ_x);
            UpperFrontUprightRLy.Text = Convert.ToString(((_oc[2].UBJ_y))); UpperFrontUprightRLy.Text = String.Format("{0:0.000}", _oc[2].UBJ_y);
            UpperFrontUprightRLz.Text = Convert.ToString(((_oc[2].UBJ_z))); UpperFrontUprightRLz.Text = String.Format("{0:0.000}", _oc[2].UBJ_z);
            UpperRearUprightRLx.Text = Convert.ToString(((_oc[2].UBJ_x))); UpperRearUprightRLx.Text = String.Format("{0:0.000}", _oc[2].UBJ_x);
            UpperRearUprightRLy.Text = Convert.ToString(((_oc[2].UBJ_y))); UpperRearUprightRLy.Text = String.Format("{0:0.000}", _oc[2].UBJ_y);
            UpperRearUprightRLz.Text = Convert.ToString(((_oc[2].UBJ_z))); UpperRearUprightRLz.Text = String.Format("{0:0.000}", _oc[2].UBJ_z);

            // Link Lengths
            LowerFrontLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].LowerFrontLength);
            LowerRearLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].LowerRearLength);
            UpperFrontLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].UpperFrontLength);
            UpperRearLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].UpperRearLength);
            PushRodLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].PushRodLength);
            ToeLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].ToeLinkLength);
            ARBDroopLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].ARBDroopLinkLength);
            DamperLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].DamperLength);
            ARBLeverLinkLengthRL.Text = String.Format("{0:0.000}", _sc[2].ARBBladeLength);



            #endregion

            #region Display of Outputs of REAR RIGHT
            //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
            A2xRR.Text = Convert.ToString(((_oc[3].A2x))); A2xRR.Text = String.Format("{0:0.000}", _oc[3].A2x);
            A2yRR.Text = Convert.ToString(((_oc[3].A2y))); A2yRR.Text = String.Format("{0:0.000}", _oc[3].A2y);
            A2zRR.Text = Convert.ToString(((_oc[3].A2z))); A2zRR.Text = String.Format("{0:0.000}", _oc[3].A2z);
            B2xRR.Text = Convert.ToString(((_oc[3].B2x))); B2xRR.Text = String.Format("{0:0.000}", _oc[3].B2x);
            B2yRR.Text = Convert.ToString(((_oc[3].B2y))); B2yRR.Text = String.Format("{0:0.000}", _oc[3].B2y);
            B2zRR.Text = Convert.ToString(((_oc[3].B2z))); B2zRR.Text = String.Format("{0:0.000}", _oc[3].B2z);
            C2xRR.Text = Convert.ToString(((_oc[3].C2x))); C2xRR.Text = String.Format("{0:0.000}", _oc[3].C2x);
            C2yRR.Text = Convert.ToString(((_oc[3].C2y))); C2yRR.Text = String.Format("{0:0.000}", _oc[3].C2y);
            C2zRR.Text = Convert.ToString(((_oc[3].C2z))); C2zRR.Text = String.Format("{0:0.000}", _oc[3].C2z);
            D2xRR.Text = Convert.ToString(((_oc[3].D2x))); D2xRR.Text = String.Format("{0:0.000}", _oc[3].D2x);
            D2yRR.Text = Convert.ToString(((_oc[3].D2y))); D2yRR.Text = String.Format("{0:0.000}", _oc[3].D2y);
            D2zRR.Text = Convert.ToString(((_oc[3].D2z))); D2zRR.Text = String.Format("{0:0.000}", _oc[3].D2z);
            N2xRR.Text = Convert.ToString(((_oc[3].N2x))); N2xRR.Text = String.Format("{0:0.000}", _oc[3].N2x);
            N2yRR.Text = Convert.ToString(((_oc[3].N2y))); N2yRR.Text = String.Format("{0:0.000}", _oc[3].N2y);
            N2zRR.Text = Convert.ToString(((_oc[3].N2z))); N2zRR.Text = String.Format("{0:0.000}", _oc[3].N2z);
            Q2xRR.Text = Convert.ToString(((_oc[3].Q2x))); Q2xRR.Text = String.Format("{0:0.000}", _oc[3].Q2x);
            Q2yRR.Text = Convert.ToString(((_oc[3].Q2y))); Q2yRR.Text = String.Format("{0:0.000}", _oc[3].Q2y);
            Q2zRR.Text = Convert.ToString(((_oc[3].Q2z))); Q2zRR.Text = String.Format("{0:0.000}", _oc[3].Q2z);
            I2xRR.Text = Convert.ToString(((_oc[3].I2x))); I2xRR.Text = String.Format("{0:0.000}", _oc[3].I2x);
            I2yRR.Text = Convert.ToString(((_oc[3].I2y))); I2yRR.Text = String.Format("{0:0.000}", _oc[3].I2y);
            I2zRR.Text = Convert.ToString(((_oc[3].I2z))); I2zRR.Text = String.Format("{0:0.000}", _oc[3].I2z);
            JO2xRR.Text = Convert.ToString(((_oc[3].JO2x))); JO2xRR.Text = String.Format("{0:0.000}", _oc[3].JO2x);
            JO2yRR.Text = Convert.ToString(((_oc[3].JO2y))); JO2yRR.Text = String.Format("{0:0.000}", _oc[3].JO2y);
            JO2zRR.Text = Convert.ToString(((_oc[3].JO2z))); JO2zRR.Text = String.Format("{0:0.000}", _oc[3].JO2z);
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            J2xRR.Text = Convert.ToString(((_oc[3].J2x))); J2xRR.Text = String.Format("{0:0.000}", _oc[3].J2x);
            J2yRR.Text = Convert.ToString(((_oc[3].J2y))); J2yRR.Text = String.Format("{0:0.000}", _oc[3].J2y);
            J2zRR.Text = Convert.ToString(((_oc[3].J2z))); J2zRR.Text = String.Format("{0:0.000}", _oc[3].J2z);
            // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
            H2xRR.Text = Convert.ToString(((_oc[3].H2x))); H2xRR.Text = String.Format("{0:0.000}", _oc[3].H2x);
            H2yRR.Text = Convert.ToString(((_oc[3].H2y))); H2yRR.Text = String.Format("{0:0.000}", _oc[3].H2y);
            H2zRR.Text = Convert.ToString(((_oc[3].H2z))); H2zRR.Text = String.Format("{0:0.000}", _oc[3].H2z);
            // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
            O2xRR.Text = Convert.ToString(((_oc[3].O2x))); O2xRR.Text = String.Format("{0:0.000}", _oc[3].O2x);
            O2yRR.Text = Convert.ToString(((_oc[3].O2y))); O2yRR.Text = String.Format("{0:0.000}", _oc[3].O2y);
            O2zRR.Text = Convert.ToString(((_oc[3].O2z))); O2zRR.Text = String.Format("{0:0.000}", _oc[3].O2z);
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            G2xRR.Text = Convert.ToString(((_oc[3].G2x))); G2xRR.Text = String.Format("{0:0.000}", _oc[3].G2x);
            G2yRR.Text = Convert.ToString(((_oc[3].G2y))); G2yRR.Text = String.Format("{0:0.000}", _oc[3].G2y);
            G2zRR.Text = Convert.ToString(((_oc[3].G2z))); G2zRR.Text = String.Format("{0:0.000}", _oc[3].G2z);
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
            F2xRR.Text = Convert.ToString(((_oc[3].F2x))); F2xRR.Text = String.Format("{0:0.000}", _oc[3].F2x);
            F2yRR.Text = Convert.ToString(((_oc[3].F2y))); F2yRR.Text = String.Format("{0:0.000}", _oc[3].F2y);
            F2zRR.Text = Convert.ToString(((_oc[3].F2z))); F2zRR.Text = String.Format("{0:0.000}", _oc[3].F2z);
            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            E2xRR.Text = Convert.ToString(((_oc[3].E2x))); E2xRR.Text = String.Format("{0:0.000}", _oc[3].E2x);
            E2yRR.Text = Convert.ToString(((_oc[3].E2y))); E2yRR.Text = String.Format("{0:0.000}", _oc[3].E2y);
            E2zRR.Text = Convert.ToString(((_oc[3].E2z))); E2zRR.Text = String.Format("{0:0.000}", _oc[3].E2z);
            // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
            MotionRatioRR.Text = Convert.ToString(((_oc[3].InitialMR))); MotionRatioRR.Text = String.Format("{0:0.000}", _oc[3].InitialMR);
            InitialARBMRRR.Text = String.Format("{0:0.000}", _oc[3].Initial_ARB_MR);
            // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
            FinalMotionRatioRR.Text = Convert.ToString(((_oc[3].FinalMR))); FinalMotionRatioRR.Text = String.Format("{0:0.000}", _oc[3].FinalMR);
            FinalARBMRRR.Text = String.Format("{0:0.000}", _oc[3].Final_ARB_MR);
            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            M2xRR.Text = Convert.ToString(((_oc[3].M2x))); M2xRR.Text = String.Format("{0:0.000}", _oc[3].M2x);
            M2yRR.Text = Convert.ToString(((_oc[3].M2y))); M2yRR.Text = String.Format("{0:0.000}", _oc[3].M2y);
            M2zRR.Text = Convert.ToString(((_oc[3].M2z))); M2zRR.Text = String.Format("{0:0.000}", _oc[3].M2z);
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            K2xRR.Text = Convert.ToString(((_oc[3].K2x))); K2xRR.Text = String.Format("{0:0.000}", _oc[3].K2x);
            K2yRR.Text = Convert.ToString(((_oc[3].K2y))); K2yRR.Text = String.Format("{0:0.000}", _oc[3].K2y);
            K2zRR.Text = Convert.ToString(((_oc[3].K2z))); K2zRR.Text = String.Format("{0:0.000}", _oc[3].K2z);
            // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
            L2xRR.Text = Convert.ToString(((_oc[3].L2x))); L2xRR.Text = String.Format("{0:0.000}", _oc[3].L2x);
            L2yRR.Text = Convert.ToString(((_oc[3].L2y))); L2yRR.Text = String.Format("{0:0.000}", _oc[3].L2y);
            L2zRR.Text = Convert.ToString(((_oc[3].L2z))); L2zRR.Text = String.Format("{0:0.000}", _oc[3].L2z);
            //To Display the New Camber and Toe
            FinalCamberRR.Text = Convert.ToString(((-_oc[3].FinalCamber))); FinalCamberRR.Text = String.Format("{0:0.000}", (_oc[3].FinalCamber * (180 / Math.PI)));
            FinalToeRR.Text = Convert.ToString(((_oc[3].FinalToe))); FinalToeRR.Text = String.Format("{0:0.000}", (_oc[3].FinalToe * (180 / Math.PI)));
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            P2xRR.Text = Convert.ToString(((_oc[3].P2x))); P2xRR.Text = String.Format("{0:0.000}", _oc[3].P2x);
            P2yRR.Text = Convert.ToString(((_oc[3].P2y))); P2yRR.Text = String.Format("{0:0.000}", _oc[3].P2y);
            P2zRR.Text = Convert.ToString(((_oc[3].P2z))); P2zRR.Text = String.Format("{0:0.000}", _oc[3].P2z);
            // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
            W2xRR.Text = Convert.ToString(((_oc[3].W2x))); W2xRR.Text = String.Format("{0:0.000}", _oc[3].W2x);
            W2yRR.Text = Convert.ToString(((_oc[3].W2y))); W2yRR.Text = String.Format("{0:0.000}", _oc[3].W2y);
            W2zRR.Text = Convert.ToString(((_oc[3].W2z))); W2zRR.Text = String.Format("{0:0.000}", _oc[3].W2z);
            //Calculating The Final Ride Height 
            RideHeightRR.Text = Convert.ToString(((_oc[3].FinalRideHeight))); RideHeightRR.Text = String.Format("{0:0.000}", _oc[3].FinalRideHeight);
            //Calculating the New Corner Weights
            CWRR.Text = Convert.ToString(((_oc[3].CW))); CWRR.Text = String.Format("{0:0.000}", _oc[3].CW);
            TireLoadedRadiusRR.Text = Convert.ToString(((_oc[3].TireLoadedRadius))); TireLoadedRadiusRR.Text = String.Format("{0:0.000}", _oc[3].TireLoadedRadius);
            //Calculating the New Spring, Damper and Wheel Deflections
            CorrectedSpringDeflectionRR.Text = Convert.ToString(((_oc[3].Corrected_SpringDeflection))); CorrectedSpringDeflectionRR.Text = String.Format("{0:0.000}", _oc[3].Corrected_SpringDeflection);
            CorrectedWheelDeflectionRR.Text = Convert.ToString(((_oc[3].Corrected_WheelDeflection))); CorrectedWheelDeflectionRR.Text = String.Format("{0:0.000}", _oc[3].Corrected_WheelDeflection);
            NewDamperLengthRR.Text = Convert.ToString(((_oc[3].DamperLength))); NewDamperLengthRR.Text = String.Format("{0:0.000}", _oc[3].DamperLength);
            //Calculating the new CG coordinates of the Non Suspended Mass
            NewNSMCGRRx.Text = Convert.ToString(((_oc[3].New_NonSuspendedMassCoGx))); NewNSMCGRRx.Text = String.Format("{0:0.000}", _oc[3].New_NonSuspendedMassCoGx);
            NewNSMCGRRy.Text = Convert.ToString(((_oc[3].New_NonSuspendedMassCoGy))); NewNSMCGRRy.Text = String.Format("{0:0.000}", _oc[3].New_NonSuspendedMassCoGy);
            NewNSMCGRRz.Text = Convert.ToString(((_oc[3].New_NonSuspendedMassCoGz))); NewNSMCGRRz.Text = String.Format("{0:0.000}", _oc[3].New_NonSuspendedMassCoGz);
            //Calculating the Wishbone Forces
            LowerFrontRR.Text = Convert.ToString(((_oc[3].LowerFront))); LowerFrontRR.Text = String.Format("{0:0.000}", _oc[3].LowerFront);
            LowerRearRR.Text = Convert.ToString(((_oc[3].LowerRear))); LowerRearRR.Text = String.Format("{0:0.000}", _oc[3].LowerRear);
            UpperFrontRR.Text = Convert.ToString(((_oc[3].UpperFront))); UpperFrontRR.Text = String.Format("{0:0.000}", _oc[3].UpperFront);
            UpperRearRR.Text = Convert.ToString(((_oc[3].UpperRear))); UpperRearRR.Text = String.Format("{0:0.000}", _oc[3].UpperRear);
            PushRodRR.Text = Convert.ToString(((_oc[3].PushRod))); PushRodRR.Text = String.Format("{0:0.000}", _oc[3].PushRod);
            ToeLinkRR.Text = Convert.ToString(((_oc[3].ToeLink))); ToeLinkRR.Text = String.Format("{0:0.000}", _oc[3].ToeLink);
            //Chassic Pick Up Points in XYZ direction
            LowerFrontChassisRRx.Text = Convert.ToString(((_oc[3].LowerFront_x))); LowerFrontChassisRRx.Text = String.Format("{0:0.000}", _oc[3].LowerFront_x);
            LowerFrontChassisRRy.Text = Convert.ToString(((_oc[3].LowerFront_y))); LowerFrontChassisRRy.Text = String.Format("{0:0.000}", _oc[3].LowerFront_y);
            LowerFrontChassisRRz.Text = Convert.ToString(((_oc[3].LowerFront_z))); LowerFrontChassisRRz.Text = String.Format("{0:0.000}", _oc[3].LowerFront_z);
            LowerRearChassisRRx.Text = Convert.ToString(((_oc[3].LowerRear_x))); LowerRearChassisRRx.Text = String.Format("{0:0.000}", _oc[3].LowerRear_x);
            LowerRearChassisRRy.Text = Convert.ToString(((_oc[3].LowerRear_y))); LowerRearChassisRRy.Text = String.Format("{0:0.000}", _oc[3].LowerRear_y);
            LowerRearChassisRRz.Text = Convert.ToString(((_oc[3].LowerRear_z))); LowerRearChassisRRz.Text = String.Format("{0:0.000}", _oc[3].LowerRear_z);
            UpperFrontChassisRRx.Text = Convert.ToString(((_oc[3].UpperFront_x))); UpperFrontChassisRRx.Text = String.Format("{0:0.000}", _oc[3].UpperFront_x);
            UpperFrontChassisRRy.Text = Convert.ToString(((_oc[3].UpperFront_y))); UpperFrontChassisRRy.Text = String.Format("{0:0.000}", _oc[3].UpperFront_y);
            UpperFrontChassisRRz.Text = Convert.ToString(((_oc[3].UpperFront_z))); UpperFrontChassisRRz.Text = String.Format("{0:0.000}", _oc[3].UpperFront_z);
            UpperRearChassisRRx.Text = Convert.ToString(((_oc[3].UpperRear_x))); UpperRearChassisRRx.Text = String.Format("{0:0.000}", _oc[3].UpperRear_x);
            UpperRearChassisRRy.Text = Convert.ToString(((_oc[3].UpperRear_y))); UpperRearChassisRRy.Text = String.Format("{0:0.000}", _oc[3].UpperRear_y);
            UpperRearChassisRRz.Text = Convert.ToString(((_oc[3].UpperRear_z))); UpperRearChassisRRz.Text = String.Format("{0:0.000}", _oc[3].UpperRear_z);
            PushRodChassisRRx.Text = Convert.ToString(((_oc[3].PushRod_x))); PushRodChassisRRx.Text = String.Format("{0:0.000}", _oc[3].PushRod_x);
            PushRodChassisRRy.Text = Convert.ToString(((_oc[3].PushRod_y))); PushRodChassisRRy.Text = String.Format("{0:0.000}", _oc[3].PushRod_y);
            PushRodChassisRRz.Text = Convert.ToString(((_oc[3].PushRod_z))); PushRodChassisRRz.Text = String.Format("{0:0.000}", _oc[3].PushRod_z);
            PushRodUprightRRx.Text = Convert.ToString(((_oc[3].PushRod_x))); PushRodUprightRRx.Text = String.Format("{0:0.000}", _oc[3].PushRod_x);
            PushRodUprightRRy.Text = Convert.ToString(((_oc[3].PushRod_y))); PushRodUprightRRy.Text = String.Format("{0:0.000}", _oc[3].PushRod_y);
            PushRodUprightRRz.Text = Convert.ToString(((_oc[3].PushRod_z))); PushRodUprightRRz.Text = String.Format("{0:0.000}", _oc[3].PushRod_z);
            DamperForceRR.Text = String.Format("{0:0.000}", _oc[3].DamperForce);
            SpringPreloadOutputRR.Text = String.Format("{0:0.000}", _spring[3].SpringPreload * _spring[3].PreloadForce);
            DamperForceChassisRRx.Text = String.Format("{0:0.000}", _oc[3].DamperForce_x);
            DamperForceChassisRRy.Text = String.Format("{0:0.000}", _oc[3].DamperForce_y);
            DamperForceChassisRRz.Text = String.Format("{0:0.000}", _oc[3].DamperForce_z);
            DamperForceBellCrankRRx.Text = String.Format("{0:0.000}", _oc[3].DamperForce_x);
            DamperForceBellCrankRRy.Text = String.Format("{0:0.000}", _oc[3].DamperForce_y);
            DamperForceBellCrankRRz.Text = String.Format("{0:0.000}", _oc[3].DamperForce_z);

            DroopLinkForceRR.Text = String.Format("{0:000}", _oc[3].ARBDroopLink);
            DroopLinkBellCrankRRx.Text = String.Format("{0:000}", _oc[3].ARBDroopLink_x);
            DroopLinkBellCrankRRy.Text = String.Format("{0:000}", _oc[3].ARBDroopLink_y);
            DroopLinkBellCrankRRz.Text = String.Format("{0:000}", _oc[3].ARBDroopLink_z);
            DroopLinkLeverRRx.Text = String.Format("{0:000}", _oc[3].ARBDroopLink_x);
            DroopLinkLeverRRy.Text = String.Format("{0:000}", _oc[3].ARBDroopLink_y);
            DroopLinkLeverRRz.Text = String.Format("{0:000}", _oc[3].ARBDroopLink_z);

            ToeLinkChassisRRx.Text = Convert.ToString(((_oc[3].ToeLink_x))); ToeLinkChassisRRx.Text = String.Format("{0:0.000}", _oc[3].ToeLink_x);
            ToeLinkChassisRRy.Text = Convert.ToString(((_oc[3].ToeLink_y))); ToeLinkChassisRRy.Text = String.Format("{0:0.000}", _oc[3].ToeLink_y);
            ToeLinkChassisRRz.Text = Convert.ToString(((_oc[3].ToeLink_z))); ToeLinkChassisRRz.Text = String.Format("{0:0.000}", _oc[3].ToeLink_z);
            ToeLinkUprightRRx.Text = Convert.ToString(((_oc[3].ToeLink_x))); ToeLinkUprightRRx.Text = String.Format("{0:0.000}", _oc[3].ToeLink_x);
            ToeLinkUprightRRy.Text = Convert.ToString(((_oc[3].ToeLink_y))); ToeLinkUprightRRy.Text = String.Format("{0:0.000}", _oc[3].ToeLink_y);
            ToeLinkUprightRRz.Text = Convert.ToString(((_oc[3].ToeLink_z))); ToeLinkUprightRRz.Text = String.Format("{0:0.000}", _oc[3].ToeLink_z);
            //Upper and Lower Ball Joint Forces in XYZ Direction
            LowerFrontUprightRRx.Text = Convert.ToString(((_oc[3].LBJ_x))); LowerFrontUprightRRx.Text = String.Format("{0:0.000}", _oc[3].LBJ_x);
            LowerFrontUprightRRy.Text = Convert.ToString(((_oc[3].LBJ_y))); LowerFrontUprightRRy.Text = String.Format("{0:0.000}", _oc[3].LBJ_y);
            LowerFrontUprightRRz.Text = Convert.ToString(((_oc[3].LBJ_z))); LowerFrontUprightRRz.Text = String.Format("{0:0.000}", _oc[3].LBJ_z);
            LowerRearUprightRRx.Text = Convert.ToString(((_oc[3].LBJ_x))); LowerRearUprightRRx.Text = String.Format("{0:0.000}", _oc[3].LBJ_x);
            LowerRearUprightRRy.Text = Convert.ToString(((_oc[3].LBJ_y))); LowerRearUprightRRy.Text = String.Format("{0:0.000}", _oc[3].LBJ_y);
            LowerRearUprightRRz.Text = Convert.ToString(((_oc[3].LBJ_z))); LowerRearUprightRRz.Text = String.Format("{0:0.000}", _oc[3].LBJ_z);
            UpperFrontUprightRRx.Text = Convert.ToString(((_oc[3].UBJ_x))); UpperFrontUprightRRx.Text = String.Format("{0:0.000}", _oc[3].UBJ_x);
            UpperFrontUprightRRy.Text = Convert.ToString(((_oc[3].UBJ_y))); UpperFrontUprightRRy.Text = String.Format("{0:0.000}", _oc[3].UBJ_y);
            UpperFrontUprightRRz.Text = Convert.ToString(((_oc[3].UBJ_z))); UpperFrontUprightRRz.Text = String.Format("{0:0.000}", _oc[3].UBJ_z);
            UpperRearUprightRRx.Text = Convert.ToString(((_oc[3].UBJ_x))); UpperRearUprightRRx.Text = String.Format("{0:0.000}", _oc[3].UBJ_x);
            UpperRearUprightRRy.Text = Convert.ToString(((_oc[3].UBJ_y))); UpperRearUprightRRy.Text = String.Format("{0:0.000}", _oc[3].UBJ_y);
            UpperRearUprightRRz.Text = Convert.ToString(((_oc[3].UBJ_z))); UpperRearUprightRRz.Text = String.Format("{0:0.000}", _oc[3].UBJ_z);

            // Link Lengths
            LowerFrontLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].LowerFrontLength);
            LowerRearLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].LowerRearLength);
            UpperFrontLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].UpperFrontLength);
            UpperRearLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].UpperRearLength);
            PushRodLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].PushRodLength);
            ToeLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].ToeLinkLength);
            ARBDroopLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].ARBDroopLinkLength);
            DamperLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].DamperLength);
            ARBLeverLinkLengthRR.Text = String.Format("{0:0.000}", _sc[3].ARBBladeLength);


            #endregion

            NewWheelBase.Text = Convert.ToString(((vehicle1.New_WheelBase))); NewWheelBase.Text = String.Format("{0:0.000}", vehicle1.New_WheelBase);
            NewTrackFront.Text = Convert.ToString(((vehicle1.New_TrackF))); NewTrackFront.Text = String.Format("{0:0.000}", vehicle1.New_TrackF);
            NewTrackRear.Text = Convert.ToString(((vehicle1.New_TrackR))); NewTrackRear.Text = String.Format("{0:0.000}", vehicle1.New_TrackR);

            NewSuspendedMassCGx.Text = Convert.ToString(((vehicle1.New_SMCoGx))); NewSuspendedMassCGx.Text = String.Format("{0:0.000}", vehicle1.New_SMCoGx);
            NewSuspendedMassCGz.Text = Convert.ToString(((vehicle1.New_SMCoGz))); NewSuspendedMassCGz.Text = String.Format("{0:0.000}", vehicle1.New_SMCoGz);
            NewSuspendedMassCGy.Text = Convert.ToString(((vehicle1.New_SMCoGy))); NewSuspendedMassCGy.Text = String.Format("{0:0.000}", vehicle1.New_SMCoGy);

            RollAngleChassis.Text = Convert.ToString(((vehicle1.RollAngle_Chassis))); RollAngleChassis.Text = String.Format("{0:0.000}", (vehicle1.RollAngle_Chassis * (180 / Math.PI)));
            PitchAngleChassis.Text = Convert.ToString(((vehicle1.PitchAngle_Chassis))); PitchAngleChassis.Text = String.Format("{0:0.000}", (vehicle1.PitchAngle_Chassis * (180 / Math.PI)));

            ARBMotionRatioFront.Text = Convert.ToString(((vehicle1.ARB_MR_Front))); ARBMotionRatioFront.Text = String.Format("{0:0.000}", vehicle1.ARB_MR_Front);
            ARBMotionRatioRear.Text = Convert.ToString(((vehicle1.ARB_MR_Rear))); ARBMotionRatioRear.Text = String.Format("{0:0.000}", vehicle1.ARB_MR_Rear);

            #endregion
        }




        private void barButtonDisplayResults_ItemClicked(object sender, ItemClickEventArgs e)
        {
            accordionControlTireStiffness.Hide();
            accordionControlVehicleItem.Hide();
            accordionControlSuspensionCoorindatesFL.Hide();
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

        private void ReCalculate_GUI_Click(object sender, EventArgs e)
        {
         
            popupControlContainerRecalculateResults.HidePopup();

            #region GUI operations to Show the Input Sheet with Link Length Page opne, pushrod textbox enabled and green, corner weight textbox disabled and white
            M1_Global.List_I1[0].navigationPane1.SelectedPage = M1_Global.List_I1[0].navigationPageLinkLengthsFL;
            M1_Global.List_I1[0].PushRodFL.Enabled = true;
            M1_Global.List_I1[0].PushRodFL.BackColor = Color.LimeGreen;
            M1_Global.List_I1[0].CornerWeightFL.Enabled = false;
            M1_Global.List_I1[0].CornerWeightFL.BackColor = Color.White;
            M1_Global.List_I1[0].RecalculateCornerWeightForPushRodLength.Enabled = true;
            M1_Global.List_I1[0].RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;

            M1_Global.List_I1[0].navigationPane2.SelectedPage = M1_Global.List_I1[0].navigationPageLinkLengthsFR;
            M1_Global.List_I1[0].PushRodFR.Enabled = true;
            M1_Global.List_I1[0].PushRodFR.BackColor = Color.LimeGreen;
            M1_Global.List_I1[0].CornerWeightFR.Enabled = false;
            M1_Global.List_I1[0].CornerWeightFR.BackColor = Color.White;

            M1_Global.List_I1[0].navigationPane3.SelectedPage = M1_Global.List_I1[0].navigationPageLinkLengthsRL;
            M1_Global.List_I1[0].PushRodRL.Enabled = true;
            M1_Global.List_I1[0].PushRodRL.BackColor = Color.LimeGreen;
            M1_Global.List_I1[0].CornerWeightRL.Enabled = false;
            M1_Global.List_I1[0].CornerWeightRL.BackColor = Color.White;

            M1_Global.List_I1[0].navigationPane4.SelectedPage = M1_Global.List_I1[0].navigationPageLinkLengthsRR;
            M1_Global.List_I1[0].PushRodRR.Enabled = true;
            M1_Global.List_I1[0].PushRodRR.BackColor = Color.LimeGreen;
            M1_Global.List_I1[0].CornerWeightRR.Enabled = false;
            M1_Global.List_I1[0].CornerWeightRR.BackColor = Color.White;
            #endregion

            M1_Global.List_I1[0].Show();


        }




        public void ReCalculate_Click()
        {
            try
            {
                M1_Global.List_I1[0].Hide();
                R1_New.Show();
                R1_New.tabPaneResults.SelectedPage = R1_New.tabNavigationPageCornerWeightDeflectionsWheelAlignment;
                double New_PushRodFL, New_PushRodFR, New_PushRodRL, New_PushRodRR;
                double New_RideheightFL, New_RideheightFR, New_RideheightRL, New_RideheightRR;
                double alphaFL, alphaFR, alphaRL, alphaRR;
                double G1H1_Perp_FL, G1H1_Perp_FR, G1H1_Perp_RL, G1H1_Perp_RR, G1H1_FL, G1H1_FR, G1H1_RL, G1H1_RR;
                double New_WheelDeflectionFL, New_WheelDeflectionFR, New_WheelDeflectionRL, New_WheelDeflectionRR;
                double New_CW_FL, New_CW_FR, New_CW_RL, New_CW_RR;
                double Delta_CW_FL, Delta_CW_FR, Delta_CW_RL, Delta_CW_RR;

                #region FRONT LEFT
                #region FRONT LEFT Calculation of New Ride Height after Increasing Push Rod Length
                New_PushRodFL = Convert.ToDouble(M1_Global.List_I1[0].PushRodFL.Text);
                
                G1H1_Perp_FL = Math.Abs(M1_Global.Assy_SCM[0].G1x - M1_Global.Assy_SCM[0].H1x);
                G1H1_FL = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[0].G1x - M1_Global.Assy_SCM[0].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[0].G1y - M1_Global.Assy_SCM[0].H1y), 2));
                alphaFL = Math.Asin(G1H1_Perp_FL / G1H1_FL);
                New_RideheightFL = M1_Global.Assy_OC[0].FinalRideHeight + ((New_PushRodFL - M1_Global.Assy_SCM[0].PushRodLength) * Math.Sin(alphaFL));
                #endregion

                #region Calculation of New Corner Weight
                New_WheelDeflectionFL = M1_Global.Assy_OC[0].Corrected_WheelDeflection - (New_RideheightFL - M1_Global.Assy_OC[0].FinalRideHeight);

                if (String.Format("{0:0.000}",New_PushRodFL) == String.Format("{0:0.000}",M1_Global.Assy_SCM[0].PushRodLength))
                {
                    New_CW_FL = M1_Global.Assy_OC[0].CW;
                }

                else
                {
                    New_CW_FL = -(((New_WheelDeflectionFL * M1_Global.Assy_OC[0].RideRate)) + ((M1_Global.Assy_Spring[0].SpringPreload * M1_Global.Assy_Spring[0].SpringRate) * M1_Global.Assy_OC[0].FinalMR)) / 9.81;
                }

                Delta_CW_FL = (M1_Global.Assy_OC[0].CW - (New_CW_FL)); // If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                M1_Global.Assy_OC[0].CW += -(Delta_CW_FL);
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[3].CW += (Delta_CW_FL);
                
                #endregion
                #endregion

                #region FRONT RIGHT
                #region FRONT RIGHT Calculation of New Ride Height after Increasing Push Rod Length
                New_PushRodFR = Convert.ToDouble(M1_Global.List_I1[0].PushRodFR.Text);
                
                G1H1_Perp_FR = Math.Abs(M1_Global.Assy_SCM[1].G1x - M1_Global.Assy_SCM[1].H1x);
                G1H1_FR = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[1].G1x - M1_Global.Assy_SCM[1].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[1].G1y - M1_Global.Assy_SCM[1].H1y), 2));
                alphaFR = Math.Asin(G1H1_Perp_FR / G1H1_FR);
                New_RideheightFR = M1_Global.Assy_OC[1].FinalRideHeight + ((New_PushRodFR - M1_Global.Assy_SCM[1].PushRodLength) * Math.Sin(alphaFR));
                #endregion

                #region Calculation of New Corner Weight
                New_WheelDeflectionFR = M1_Global.Assy_OC[1].Corrected_WheelDeflection - (New_RideheightFR - M1_Global.Assy_OC[1].FinalRideHeight);

                if (String.Format("{0:0.000}", New_PushRodFR) == String.Format("{0:0.000}", M1_Global.Assy_SCM[1].PushRodLength))
                {
                    New_CW_FR = M1_Global.Assy_OC[1].CW;
                }
                else
                {
                    New_CW_FR = -(((New_WheelDeflectionFR * M1_Global.Assy_OC[1].RideRate)) + ((M1_Global.Assy_Spring[1].SpringPreload * M1_Global.Assy_Spring[1].SpringRate) * M1_Global.Assy_OC[1].FinalMR)) / 9.81;
                }
                
                Delta_CW_FR = (M1_Global.Assy_OC[1].CW - (New_CW_FR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                M1_Global.Assy_OC[1].CW += -Delta_CW_FR;
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[2].CW += Delta_CW_FR;

                #endregion
                #endregion

                #region REAL LEFT
                #region REAR LEFT Calculation of New Ride Height after Increasing Push Rod Length
                New_PushRodRL = Convert.ToDouble(M1_Global.List_I1[0].PushRodRL.Text);
                
                G1H1_Perp_RL = Math.Abs(M1_Global.Assy_SCM[2].G1x - M1_Global.Assy_SCM[2].H1x);
                G1H1_RL = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[2].G1x - M1_Global.Assy_SCM[2].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[2].G1y - M1_Global.Assy_SCM[2].H1y), 2));
                alphaRL = Math.Asin(G1H1_Perp_RL / G1H1_RL);
                New_RideheightRL = M1_Global.Assy_OC[2].FinalRideHeight + ((New_PushRodRL - M1_Global.Assy_SCM[2].PushRodLength) * Math.Sin(alphaRL));
                #endregion

                #region Calculation of New Corner Weight
                New_WheelDeflectionRL = M1_Global.Assy_OC[2].Corrected_WheelDeflection - (New_RideheightRL - M1_Global.Assy_OC[2].FinalRideHeight);
                if (String.Format("{0:0.000}", New_PushRodRL) == String.Format("{0:0.000}", M1_Global.Assy_SCM[2].PushRodLength))
                {
                    New_CW_RL = M1_Global.Assy_OC[2].CW;
                }
                else
                {
                    New_CW_RL = -(((New_WheelDeflectionRL * M1_Global.Assy_OC[2].RideRate)) + ((M1_Global.Assy_Spring[2].SpringPreload * M1_Global.Assy_Spring[2].SpringRate) * M1_Global.Assy_OC[2].FinalMR)) / 9.81;

                }

                Delta_CW_RL = (M1_Global.Assy_OC[2].CW - (New_CW_RL));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                M1_Global.Assy_OC[2].CW += -Delta_CW_RL;
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[1].CW += Delta_CW_RL;

                #endregion
                #endregion

                #region REAR RIGHT
                #region REAR RIGHT Calculation of New Ride Height after Increasing Push Rod Length
                New_PushRodRR = Convert.ToDouble(M1_Global.List_I1[0].PushRodRR.Text);
                
                G1H1_Perp_RR = Math.Abs(M1_Global.Assy_SCM[3].G1x - M1_Global.Assy_SCM[3].H1x);
                G1H1_RR = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[3].G1x - M1_Global.Assy_SCM[3].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[3].G1y - M1_Global.Assy_SCM[3].H1y), 2));
                alphaRR = Math.Asin(G1H1_Perp_RR / G1H1_RR);
                New_RideheightRR = M1_Global.Assy_OC[3].FinalRideHeight + ((New_PushRodRR - M1_Global.Assy_SCM[3].PushRodLength) * Math.Sin(alphaRR));
                #endregion

                #region Calculation of New Corner Weight
                New_WheelDeflectionRR = M1_Global.Assy_OC[3].Corrected_WheelDeflection - (New_RideheightRR - M1_Global.Assy_OC[3].FinalRideHeight);
                if (String.Format("{0:0.000}", New_PushRodRR) == String.Format("{0:0.000}", M1_Global.Assy_SCM[3].PushRodLength))
                {
                    New_CW_RR = M1_Global.Assy_OC[3].CW;
                }
                else
                {
                    New_CW_RR = -(((New_WheelDeflectionRR * M1_Global.Assy_OC[3].RideRate)) + ((M1_Global.Assy_Spring[3].SpringPreload * M1_Global.Assy_Spring[3].SpringRate) * M1_Global.Assy_OC[3].FinalMR)) / 9.81;
                }
                
                
                Delta_CW_RR = (M1_Global.Assy_OC[3].CW - (New_CW_RR));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                M1_Global.Assy_OC[3].CW += -Delta_CW_RR;
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[0].CW += Delta_CW_RR;

                #endregion
                #endregion

                //
                // Assigning the New values of Push Rod Lengths
                M1_Global.Assy_SCM[0].PushRodLength = New_PushRodFL;
                M1_Global.Assy_SCM[1].PushRodLength = New_PushRodFR;
                M1_Global.Assy_SCM[2].PushRodLength = New_PushRodRL;
                M1_Global.Assy_SCM[3].PushRodLength = New_PushRodRR;

                //
                //Invoking the Kinematics and Vehicle Output Functions again 
                M1_Global.Assembled_Vehicle.KinematicsInvoker(R1);
                M1_Global.Assembled_Vehicle.VehicleOutputs(R1);



                #region Display of Outputs



                #region Display of Outputs of FRONT LEFT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].A2x))); A2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].A2x);
                A2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].A2y))); A2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].A2y);
                A2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].A2z))); A2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].A2z);
                B2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].B2x))); B2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].B2x);
                B2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].B2y))); B2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].B2y);
                B2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].B2z))); B2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].B2z);
                C2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].C2x))); C2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].C2x);
                C2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].C2y))); C2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].C2y);
                C2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].C2z))); C2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].C2z);
                D2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].D2x))); D2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].D2x);
                D2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].D2y))); D2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].D2y);
                D2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].D2z))); D2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].D2z);
                N2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].N2x))); N2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].N2x);
                N2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].N2y))); N2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].N2y);
                N2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].N2z))); N2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].N2z);
                Q2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Q2x))); Q2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Q2x);
                Q2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Q2y))); Q2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Q2y);
                Q2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Q2z))); Q2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Q2z);
                I2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].I2x))); I2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].I2x);
                I2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].I2y))); I2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].I2y);
                I2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].I2z))); I2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].I2z);
                JO2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].JO2x))); JO2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].JO2x);
                JO2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].JO2y))); JO2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].JO2y);
                JO2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].JO2z))); JO2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].J2x))); J2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].J2x);
                J2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].J2y))); J2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].J2y);
                J2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].J2z))); J2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].H2x))); H2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].H2x);
                H2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].H2y))); H2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].H2y);
                H2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].H2z))); H2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].O2x))); O2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].O2x);
                O2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].O2y))); O2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].O2y);
                O2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].O2z))); O2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].G2x))); G2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].G2x);
                G2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].G2y))); G2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].G2y);
                G2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].G2z))); G2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].F2x))); F2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].F2x);
                F2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].F2y))); F2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].F2y);
                F2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].F2z))); F2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].E2x))); E2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].E2x);
                E2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].E2y))); E2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].E2y);
                E2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].E2z))); E2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].InitialMR))); MotionRatioFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].InitialMR);
                InitialARBMRFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalMR))); FinalMotionRatioFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].FinalMR);
                FinalARBMRFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].M2x))); M2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].M2x);
                M2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].M2y))); M2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].M2y);
                M2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].M2z))); M2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].K2x))); K2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].K2x);
                K2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].K2y))); K2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].K2y);
                K2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].K2z))); K2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].L2x))); L2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].L2x);
                L2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].L2y))); L2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].L2y);
                L2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].L2z))); L2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].L2z);
                //To Display the New Camber and Toe
                FinalCamberFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalCamber))); FinalCamberFL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[0].FinalCamber * (180 / Math.PI)));
                FinalToeFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalToe))); FinalToeFL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[0].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].P2x))); P2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].P2x);
                P2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].P2y))); P2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].P2y);
                P2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].P2z))); P2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].W2x))); W2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].W2x);
                W2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].W2y))); W2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].W2y);
                W2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].W2z))); W2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].W2z);
                //Calculating The Final Ride Height 
                RideHeightFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalRideHeight))); RideHeightFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].FinalRideHeight);
                //Calculating the New Corner Weights 
                CWFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].CW))); CWFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].CW);
                TireLoadedRadiusFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].TireLoadedRadius))); TireLoadedRadiusFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Corrected_SpringDeflection))); CorrectedSpringDeflectionFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Corrected_SpringDeflection);
                CorrectedWheelDeflectionFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Corrected_WheelDeflection))); CorrectedWheelDeflectionFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Corrected_WheelDeflection);
                NewDamperLengthFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].DamperLength))); NewDamperLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].New_NonSuspendedMassCoGx))); NewNSMCGFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].New_NonSuspendedMassCoGx);
                NewNSMCGFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].New_NonSuspendedMassCoGy))); NewNSMCGFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].New_NonSuspendedMassCoGy);
                NewNSMCGFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].New_NonSuspendedMassCoGz))); NewNSMCGFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront))); LowerFrontFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront);
                LowerRearFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear))); LowerRearFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear);
                UpperFrontFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront))); UpperFrontFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront);
                UpperRearFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear))); UpperRearFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear);
                PushRodFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod))); PushRodFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod);
                ToeLinkFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink))); ToeLinkFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront_x))); LowerFrontChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront_x);
                LowerFrontChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront_y))); LowerFrontChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront_y);
                LowerFrontChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront_z))); LowerFrontChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront_z);
                LowerRearChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear_x))); LowerRearChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear_x);
                LowerRearChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear_y))); LowerRearChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear_y);
                LowerRearChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear_z))); LowerRearChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear_z);
                UpperFrontChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront_x))); UpperFrontChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront_x);
                UpperFrontChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront_y))); UpperFrontChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront_y);
                UpperFrontChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront_z))); UpperFrontChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront_z);
                UpperRearChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear_x))); UpperRearChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear_x);
                UpperRearChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear_y))); UpperRearChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear_y);
                UpperRearChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear_z))); UpperRearChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear_z);
                PushRodChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_x))); PushRodChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_x);
                PushRodChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_y))); PushRodChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_y);
                PushRodChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_z))); PushRodChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_z);
                PushRodUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_x))); PushRodUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_x);
                PushRodUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_y))); PushRodUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_y);
                PushRodUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_z))); PushRodUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_z);
                ToeLinkChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_x))); ToeLinkChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_x);
                ToeLinkChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_y))); ToeLinkChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_y);
                ToeLinkChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_z))); ToeLinkChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_z);
                ToeLinkUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_x))); ToeLinkUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_x);
                ToeLinkUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_y))); ToeLinkUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_y);
                ToeLinkUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_z))); ToeLinkUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_x))); LowerFrontUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_x);
                LowerFrontUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_y))); LowerFrontUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_y);
                LowerFrontUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_z))); LowerFrontUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_z);
                LowerRearUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_x))); LowerRearUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_x);
                LowerRearUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_y))); LowerRearUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_y);
                LowerRearUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_z))); LowerRearUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_z);
                UpperFrontUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_x))); UpperFrontUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_x);
                UpperFrontUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_y))); UpperFrontUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_y);
                UpperFrontUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_z))); UpperFrontUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_z);
                UpperRearUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_x))); UpperRearUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_x);
                UpperRearUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_y))); UpperRearUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_y);
                UpperRearUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_z))); UpperRearUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].LowerFrontLength);
                LowerRearLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].LowerRearLength);
                UpperFrontLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].UpperFrontLength);
                UpperRearLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].UpperRearLength);
                PushRodLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].PushRodLength);
                CWFL.BackColor = Color.LimeGreen;
                PushRodLinkLengthFL.BackColor = Color.White;
                ToeLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].ToeLinkLength);
                ARBDroopLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].ARBDroopLinkLength);
                DamperLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].DamperLength);
                ARBLeverLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].ARBBladeLength);


                #endregion

                #region Display of Outputs of FRONT RIGHT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].A2x))); A2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].A2x);
                A2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].A2y))); A2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].A2y);
                A2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].A2z))); A2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].A2z);
                B2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].B2x))); B2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].B2x);
                B2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].B2y))); B2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].B2y);
                B2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].B2z))); B2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].B2z);
                C2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].C2x))); C2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].C2x);
                C2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].C2y))); C2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].C2y);
                C2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].C2z))); C2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].C2z);
                D2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].D2x))); D2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].D2x);
                D2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].D2y))); D2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].D2y);
                D2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].D2z))); D2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].D2z);
                N2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].N2x))); N2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].N2x);
                N2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].N2y))); N2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].N2y);
                N2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].N2z))); N2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].N2z);
                Q2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Q2x))); Q2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Q2x);
                Q2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Q2y))); Q2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Q2y);
                Q2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Q2z))); Q2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Q2z);
                I2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].I2x))); I2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].I2x);
                I2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].I2y))); I2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].I2y);
                I2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].I2z))); I2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].I2z);
                JO2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].JO2x))); JO2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].JO2x);
                JO2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].JO2y))); JO2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].JO2y);
                JO2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].JO2z))); JO2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].J2x))); J2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].J2x);
                J2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].J2y))); J2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].J2y);
                J2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].J2z))); J2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].H2x))); H2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].H2x);
                H2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].H2y))); H2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].H2y);
                H2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].H2z))); H2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].O2x))); O2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].O2x);
                O2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].O2y))); O2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].O2y);
                O2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].O2z))); O2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].G2x))); G2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].G2x);
                G2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].G2y))); G2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].G2y);
                G2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].G2z))); G2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].F2x))); F2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].F2x);
                F2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].F2y))); F2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].F2y);
                F2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].F2z))); F2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].E2x))); E2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].E2x);
                E2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].E2y))); E2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].E2y);
                E2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].E2z))); E2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].InitialMR))); MotionRatioFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].InitialMR);
                InitialARBMRFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalMR))); FinalMotionRatioFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].FinalMR);
                FinalARBMRFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].M2x))); M2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].M2x);
                M2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].M2y))); M2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].M2y);
                M2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].M2z))); M2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].K2x))); K2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].K2x);
                K2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].K2y))); K2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].K2y);
                K2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].K2z))); K2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].L2x))); L2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].L2x);
                L2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].L2y))); L2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].L2y);
                L2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].L2z))); L2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].L2z);
                //To Display the New Camber and Toe
                FinalCamberFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalCamber))); FinalCamberFR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[1].FinalCamber * (180 / Math.PI)));
                FinalToeFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalToe))); FinalToeFR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[1].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].P2x))); P2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].P2x);
                P2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].P2y))); P2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].P2y);
                P2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].P2z))); P2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].W2x))); W2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].W2x);
                W2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].W2y))); W2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].W2y);
                W2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].W2z))); W2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].W2z);
                //Calculating The Final Ride Height 
                RideHeightFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalRideHeight))); RideHeightFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].FinalRideHeight);
                //Calculating the New Corner Weights
                CWFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].CW))); CWFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].CW);
                TireLoadedRadiusFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].TireLoadedRadius))); TireLoadedRadiusFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Corrected_SpringDeflection))); CorrectedSpringDeflectionFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Corrected_SpringDeflection);
                CorrectedWheelDeflectionFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Corrected_WheelDeflection))); CorrectedWheelDeflectionFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Corrected_WheelDeflection);
                NewDamperLengthFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].DamperLength))); NewDamperLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].New_NonSuspendedMassCoGx))); NewNSMCGFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].New_NonSuspendedMassCoGx);
                NewNSMCGFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].New_NonSuspendedMassCoGy))); NewNSMCGFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].New_NonSuspendedMassCoGy);
                NewNSMCGFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].New_NonSuspendedMassCoGz))); NewNSMCGFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront))); LowerFrontFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront);
                LowerRearFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear))); LowerRearFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear);
                UpperFrontFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront))); UpperFrontFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront);
                UpperRearFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear))); UpperRearFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear);
                PushRodFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod))); PushRodFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod);
                ToeLinkFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink))); ToeLinkFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront_x))); LowerFrontChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront_x);
                LowerFrontChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront_y))); LowerFrontChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront_y);
                LowerFrontChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront_z))); LowerFrontChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront_z);
                LowerRearChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear_x))); LowerRearChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear_x);
                LowerRearChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear_y))); LowerRearChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear_y);
                LowerRearChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear_z))); LowerRearChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear_z);
                UpperFrontChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront_x))); UpperFrontChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront_x);
                UpperFrontChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront_y))); UpperFrontChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront_y);
                UpperFrontChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront_z))); UpperFrontChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront_z);
                UpperRearChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear_x))); UpperRearChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear_x);
                UpperRearChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear_y))); UpperRearChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear_y);
                UpperRearChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear_z))); UpperRearChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear_z);
                PushRodChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_x))); PushRodChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_x);
                PushRodChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_y))); PushRodChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_y);
                PushRodChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_z))); PushRodChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_z);
                PushRodUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_x))); PushRodUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_x);
                PushRodUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_y))); PushRodUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_y);
                PushRodUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_z))); PushRodUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_z);
                ToeLinkChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_x))); ToeLinkChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_x);
                ToeLinkChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_y))); ToeLinkChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_y);
                ToeLinkChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_z))); ToeLinkChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_z);
                ToeLinkUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_x))); ToeLinkUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_x);
                ToeLinkUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_y))); ToeLinkUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_y);
                ToeLinkUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_z))); ToeLinkUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_x))); LowerFrontUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_x);
                LowerFrontUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_y))); LowerFrontUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_y);
                LowerFrontUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_z))); LowerFrontUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_z);
                LowerRearUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_x))); LowerRearUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_x);
                LowerRearUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_y))); LowerRearUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_y);
                LowerRearUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_z))); LowerRearUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_z);
                UpperFrontUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_x))); UpperFrontUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_x);
                UpperFrontUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_y))); UpperFrontUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_y);
                UpperFrontUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_z))); UpperFrontUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_z);
                UpperRearUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_x))); UpperRearUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_x);
                UpperRearUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_y))); UpperRearUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_y);
                UpperRearUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_z))); UpperRearUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_z);

                LowerFrontLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].LowerFrontLength);
                LowerRearLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].LowerRearLength);
                UpperFrontLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].UpperFrontLength);
                UpperRearLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].UpperRearLength);
                PushRodLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].PushRodLength);
                CWFR.BackColor = Color.LimeGreen;
                PushRodLinkLengthFR.BackColor = Color.White;
                ToeLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].ToeLinkLength);
                ARBDroopLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].ARBDroopLinkLength);
                DamperLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].DamperLength);
                ARBLeverLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].ARBBladeLength);


                #endregion

                #region Display of Outputs of REAR LEFT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].A2x))); A2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].A2x);
                A2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].A2y))); A2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].A2y);
                A2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].A2z))); A2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].A2z);
                B2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].B2x))); B2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].B2x);
                B2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].B2y))); B2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].B2y);
                B2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].B2z))); B2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].B2z);
                C2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].C2x))); C2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].C2x);
                C2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].C2y))); C2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].C2y);
                C2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].C2z))); C2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].C2z);
                D2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].D2x))); D2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].D2x);
                D2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].D2y))); D2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].D2y);
                D2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].D2z))); D2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].D2z);
                N2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].N2x))); N2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].N2x);
                N2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].N2y))); N2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].N2y);
                N2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].N2z))); N2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].N2z);
                Q2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Q2x))); Q2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Q2x);
                Q2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Q2y))); Q2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Q2y);
                Q2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Q2z))); Q2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Q2z);
                I2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].I2x))); I2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].I2x);
                I2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].I2y))); I2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].I2y);
                I2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].I2z))); I2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].I2z);
                JO2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].JO2x))); JO2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].JO2x);
                JO2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].JO2y))); JO2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].JO2y);
                JO2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].JO2z))); JO2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].J2x))); J2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].J2x);
                J2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].J2y))); J2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].J2y);
                J2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].J2z))); J2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].H2x))); H2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].H2x);
                H2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].H2y))); H2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].H2y);
                H2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].H2z))); H2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].O2x))); O2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].O2x);
                O2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].O2y))); O2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].O2y);
                O2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].O2z))); O2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].G2x))); G2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].G2x);
                G2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].G2y))); G2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].G2y);
                G2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].G2z))); G2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].F2x))); F2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].F2x);
                F2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].F2y))); F2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].F2y);
                F2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].F2z))); F2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].E2x))); E2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].E2x);
                E2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].E2y))); E2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].E2y);
                E2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].E2z))); E2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].InitialMR))); MotionRatioRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].InitialMR);
                InitialARBMRRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalMR))); FinalMotionRatioRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].FinalMR);
                FinalARBMRFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].M2x))); M2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].M2x);
                M2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].M2y))); M2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].M2y);
                M2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].M2z))); M2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].K2x))); K2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].K2x);
                K2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].K2y))); K2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].K2y);
                K2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].K2z))); K2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].L2x))); L2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].L2x);
                L2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].L2y))); L2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].L2y);
                L2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].L2z))); L2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].L2z);
                //To Display the New Camber and Toe
                FinalCamberRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalCamber))); FinalCamberRL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[2].FinalCamber * (180 / Math.PI)));
                FinalToeRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalToe))); FinalToeRL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[2].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].P2x))); P2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].P2x);
                P2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].P2y))); P2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].P2y);
                P2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].P2z))); P2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].W2x))); W2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].W2x);
                W2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].W2y))); W2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].W2y);
                W2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].W2z))); W2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].W2z);
                //Calculating The Final Ride Height 
                RideHeightRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalRideHeight))); RideHeightRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].FinalRideHeight);
                //Calculating the New Corner Weights
                CWRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].CW))); CWRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].CW);
                TireLoadedRadiusRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].TireLoadedRadius))); TireLoadedRadiusRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Corrected_SpringDeflection))); CorrectedSpringDeflectionRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Corrected_SpringDeflection);
                CorrectedWheelDeflectionRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Corrected_WheelDeflection))); CorrectedWheelDeflectionRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Corrected_WheelDeflection);
                NewDamperLengthRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].DamperLength))); NewDamperLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].New_NonSuspendedMassCoGx))); NewNSMCGRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].New_NonSuspendedMassCoGx);
                NewNSMCGRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].New_NonSuspendedMassCoGy))); NewNSMCGRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].New_NonSuspendedMassCoGy);
                NewNSMCGRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].New_NonSuspendedMassCoGz))); NewNSMCGRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront))); LowerFrontRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront);
                LowerRearRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear))); LowerRearRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear);
                UpperFrontRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront))); UpperFrontRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront);
                UpperRearRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear))); UpperRearRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear);
                PushRodRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod))); PushRodRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod);
                ToeLinkRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink))); ToeLinkRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront_x))); LowerFrontChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront_x);
                LowerFrontChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront_y))); LowerFrontChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront_y);
                LowerFrontChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront_z))); LowerFrontChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront_z);
                LowerRearChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear_x))); LowerRearChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear_x);
                LowerRearChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear_y))); LowerRearChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear_y);
                LowerRearChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear_z))); LowerRearChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear_z);
                UpperFrontChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront_x))); UpperFrontChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront_x);
                UpperFrontChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront_y))); UpperFrontChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront_y);
                UpperFrontChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront_z))); UpperFrontChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront_z);
                UpperRearChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear_x))); UpperRearChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear_x);
                UpperRearChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear_y))); UpperRearChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear_y);
                UpperRearChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear_z))); UpperRearChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear_z);
                PushRodChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_x))); PushRodChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_x);
                PushRodChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_y))); PushRodChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_y);
                PushRodChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_z))); PushRodChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_z);
                PushRodUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_x))); PushRodUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_x);
                PushRodUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_y))); PushRodUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_y);
                PushRodUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_z))); PushRodUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_z);
                ToeLinkChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_x))); ToeLinkChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_x);
                ToeLinkChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_y))); ToeLinkChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_y);
                ToeLinkChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_z))); ToeLinkChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_z);
                ToeLinkUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_x))); ToeLinkUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_x);
                ToeLinkUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_y))); ToeLinkUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_y);
                ToeLinkUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_z))); ToeLinkUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_x))); LowerFrontUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_x);
                LowerFrontUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_y))); LowerFrontUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_y);
                LowerFrontUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_z))); LowerFrontUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_z);
                LowerRearUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_x))); LowerRearUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_x);
                LowerRearUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_y))); LowerRearUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_y);
                LowerRearUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_z))); LowerRearUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_z);
                UpperFrontUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_x))); UpperFrontUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_x);
                UpperFrontUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_y))); UpperFrontUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_y);
                UpperFrontUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_z))); UpperFrontUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_z);
                UpperRearUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_x))); UpperRearUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_x);
                UpperRearUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_y))); UpperRearUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_y);
                UpperRearUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_z))); UpperRearUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].LowerFrontLength);
                LowerRearLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].LowerRearLength);
                UpperFrontLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].UpperFrontLength);
                UpperRearLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].UpperRearLength);
                PushRodLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].PushRodLength);
                CWRL.BackColor = Color.LimeGreen;
                PushRodLinkLengthRL.BackColor = Color.White;
                ToeLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].ToeLinkLength);
                ARBDroopLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].ARBDroopLinkLength);
                DamperLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].DamperLength);
                ARBLeverLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].ARBBladeLength);

                #endregion

                #region Display of Outputs of REAR RIGHT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].A2x))); A2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].A2x);
                A2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].A2y))); A2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].A2y);
                A2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].A2z))); A2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].A2z);
                B2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].B2x))); B2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].B2x);
                B2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].B2y))); B2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].B2y);
                B2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].B2z))); B2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].B2z);
                C2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].C2x))); C2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].C2x);
                C2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].C2y))); C2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].C2y);
                C2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].C2z))); C2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].C2z);
                D2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].D2x))); D2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].D2x);
                D2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].D2y))); D2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].D2y);
                D2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].D2z))); D2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].D2z);
                N2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].N2x))); N2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].N2x);
                N2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].N2y))); N2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].N2y);
                N2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].N2z))); N2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].N2z);
                Q2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Q2x))); Q2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Q2x);
                Q2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Q2y))); Q2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Q2y);
                Q2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Q2z))); Q2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Q2z);
                I2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].I2x))); I2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].I2x);
                I2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].I2y))); I2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].I2y);
                I2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].I2z))); I2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].I2z);
                JO2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].JO2x))); JO2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].JO2x);
                JO2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].JO2y))); JO2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].JO2y);
                JO2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].JO2z))); JO2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].J2x))); J2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].J2x);
                J2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].J2y))); J2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].J2y);
                J2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].J2z))); J2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].H2x))); H2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].H2x);
                H2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].H2y))); H2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].H2y);
                H2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].H2z))); H2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].O2x))); O2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].O2x);
                O2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].O2y))); O2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].O2y);
                O2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].O2z))); O2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].G2x))); G2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].G2x);
                G2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].G2y))); G2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].G2y);
                G2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].G2z))); G2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].F2x))); F2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].F2x);
                F2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].F2y))); F2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].F2y);
                F2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].F2z))); F2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].E2x))); E2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].E2x);
                E2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].E2y))); E2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].E2y);
                E2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].E2z))); E2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].InitialMR))); MotionRatioRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].InitialMR);
                InitialARBMRRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalMR))); FinalMotionRatioRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].FinalMR);
                FinalARBMRRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].M2x))); M2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].M2x);
                M2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].M2y))); M2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].M2y);
                M2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].M2z))); M2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].K2x))); K2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].K2x);
                K2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].K2y))); K2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].K2y);
                K2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].K2z))); K2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].L2x))); L2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].L2x);
                L2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].L2y))); L2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].L2y);
                L2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].L2z))); L2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].L2z);
                //To Display the New Camber and Toe
                FinalCamberRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalCamber))); FinalCamberRR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[3].FinalCamber * (180 / Math.PI)));
                FinalToeRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalToe))); FinalToeRR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[3].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].P2x))); P2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].P2x);
                P2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].P2y))); P2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].P2y);
                P2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].P2z))); P2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].W2x))); W2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].W2x);
                W2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].W2y))); W2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].W2y);
                W2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].W2z))); W2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].W2z);
                //Calculating The Final Ride Height 
                RideHeightRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalRideHeight))); RideHeightRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].FinalRideHeight);
                //Calculating the New Corner Weights
                CWRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].CW))); CWRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].CW);
                TireLoadedRadiusRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].TireLoadedRadius))); TireLoadedRadiusRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Corrected_SpringDeflection))); CorrectedSpringDeflectionRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Corrected_SpringDeflection);
                CorrectedWheelDeflectionRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Corrected_WheelDeflection))); CorrectedWheelDeflectionRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Corrected_WheelDeflection);
                NewDamperLengthRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].DamperLength))); NewDamperLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].New_NonSuspendedMassCoGx))); NewNSMCGRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].New_NonSuspendedMassCoGx);
                NewNSMCGRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].New_NonSuspendedMassCoGy))); NewNSMCGRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].New_NonSuspendedMassCoGy);
                NewNSMCGRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].New_NonSuspendedMassCoGz))); NewNSMCGRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront))); LowerFrontRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront);
                LowerRearRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear))); LowerRearRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear);
                UpperFrontRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront))); UpperFrontRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront);
                UpperRearRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear))); UpperRearRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear);
                PushRodRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod))); PushRodRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod);
                ToeLinkRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink))); ToeLinkRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront_x))); LowerFrontChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront_x);
                LowerFrontChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront_y))); LowerFrontChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront_y);
                LowerFrontChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront_z))); LowerFrontChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront_z);
                LowerRearChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear_x))); LowerRearChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear_x);
                LowerRearChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear_y))); LowerRearChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear_y);
                LowerRearChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear_z))); LowerRearChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear_z);
                UpperFrontChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront_x))); UpperFrontChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront_x);
                UpperFrontChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront_y))); UpperFrontChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront_y);
                UpperFrontChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront_z))); UpperFrontChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront_z);
                UpperRearChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear_x))); UpperRearChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear_x);
                UpperRearChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear_y))); UpperRearChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear_y);
                UpperRearChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear_z))); UpperRearChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear_z);
                PushRodChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_x))); PushRodChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_x);
                PushRodChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_y))); PushRodChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_y);
                PushRodChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_z))); PushRodChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_z);
                PushRodUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_x))); PushRodUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_x);
                PushRodUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_y))); PushRodUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_y);
                PushRodUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_z))); PushRodUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_z);
                ToeLinkChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_x))); ToeLinkChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_x);
                ToeLinkChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_y))); ToeLinkChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_y);
                ToeLinkChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_z))); ToeLinkChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_z);
                ToeLinkUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_x))); ToeLinkUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_x);
                ToeLinkUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_y))); ToeLinkUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_y);
                ToeLinkUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_z))); ToeLinkUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_x))); LowerFrontUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_x);
                LowerFrontUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_y))); LowerFrontUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_y);
                LowerFrontUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_z))); LowerFrontUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_z);
                LowerRearUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_x))); LowerRearUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_x);
                LowerRearUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_y))); LowerRearUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_y);
                LowerRearUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_z))); LowerRearUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_z);
                UpperFrontUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_x))); UpperFrontUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_x);
                UpperFrontUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_y))); UpperFrontUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_y);
                UpperFrontUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_z))); UpperFrontUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_z);
                UpperRearUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_x))); UpperRearUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_x);
                UpperRearUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_y))); UpperRearUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_y);
                UpperRearUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_z))); UpperRearUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].LowerFrontLength);
                LowerRearLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].LowerRearLength);
                UpperFrontLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].UpperFrontLength);
                UpperRearLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].UpperRearLength);
                PushRodLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].PushRodLength);
                CWRR.BackColor = Color.LimeGreen;
                PushRodLinkLengthRR.BackColor = Color.White;
                ToeLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].ToeLinkLength);
                ARBDroopLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].ARBDroopLinkLength);
                DamperLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].DamperLength);
                ARBLeverLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].ARBBladeLength);


                #endregion

                NewWheelBase.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_WheelBase))); NewWheelBase.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_WheelBase);
                NewTrackFront.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_TrackF))); NewTrackFront.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_TrackF);
                NewTrackRear.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_TrackR))); NewTrackRear.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_TrackR);

                NewSuspendedMassCGx.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_SMCoGx))); NewSuspendedMassCGx.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_SMCoGx);
                NewSuspendedMassCGz.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_SMCoGz))); NewSuspendedMassCGz.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_SMCoGz);
                NewSuspendedMassCGy.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_SMCoGy))); NewSuspendedMassCGy.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_SMCoGy);

                RollAngleChassis.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.RollAngle_Chassis))); RollAngleChassis.Text = String.Format("{0:0.000}", (M1_Global.Assembled_Vehicle.RollAngle_Chassis * (180 / Math.PI)));
                PitchAngleChassis.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.PitchAngle_Chassis))); PitchAngleChassis.Text = String.Format("{0:0.000}", (M1_Global.Assembled_Vehicle.PitchAngle_Chassis * (180 / Math.PI)));

                ARBMotionRatioFront.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.ARB_MR_Front))); ARBMotionRatioFront.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.ARB_MR_Front);
                ARBMotionRatioRear.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.ARB_MR_Rear))); ARBMotionRatioRear.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.ARB_MR_Rear);

                #endregion
            }

            catch (Exception)
            {

                MessageBox.Show(" Initial Calculations have not been done");
            }


        }


        private void ReCalculateForDesiredCornerWeight_GUI_Click(object sender, EventArgs e)
        {
            popupControlContainerRecalculateResults.HidePopup();

            #region GUI operations to Show the Input Sheet with Corner Weights Page opne, pushrod textbox disabled and white, corner weight textbox enabled and green
            M1_Global.List_I1[0].navigationPane1.SelectedPage = M1_Global.List_I1[0].navigationPageCornerWeightFL;
            M1_Global.List_I1[0].PushRodFL.Enabled = false;
            M1_Global.List_I1[0].PushRodFL.BackColor = Color.White;
            M1_Global.List_I1[0].CornerWeightFL.Enabled = true;
            M1_Global.List_I1[0].CornerWeightFL.BackColor = Color.LimeGreen;
            M1_Global.List_I1[0].RecalculatePushrodLengthForDesiredCornerWeight.Enabled = true;
            M1_Global.List_I1[0].RecalculateCornerWeightForPushRodLength.Enabled = false;

            M1_Global.List_I1[0].navigationPane2.SelectedPage = M1_Global.List_I1[0].navigationPageCornerWeightFR;
            M1_Global.List_I1[0].PushRodFR.Enabled = false;
            M1_Global.List_I1[0].PushRodFR.BackColor = Color.White;
            M1_Global.List_I1[0].CornerWeightFR.Enabled = true;
            M1_Global.List_I1[0].CornerWeightFR.BackColor = Color.LimeGreen;

            M1_Global.List_I1[0].navigationPane3.SelectedPage = M1_Global.List_I1[0].navigationPageCornerWeightRL;
            M1_Global.List_I1[0].PushRodRL.Enabled = false;
            M1_Global.List_I1[0].PushRodRL.BackColor = Color.White;
            M1_Global.List_I1[0].CornerWeightRL.Enabled = true;
            M1_Global.List_I1[0].CornerWeightRL.BackColor = Color.LimeGreen;

            M1_Global.List_I1[0].navigationPane4.SelectedPage = M1_Global.List_I1[0].navigationPageCornerWeightRR;
            M1_Global.List_I1[0].PushRodRR.Enabled = false;
            M1_Global.List_I1[0].PushRodRR.BackColor = Color.White;
            M1_Global.List_I1[0].CornerWeightRR.Enabled = true;
            M1_Global.List_I1[0].CornerWeightRR.BackColor = Color.LimeGreen;
            #endregion

            M1_Global.List_I1[0].Show();

        }


        public void ReCalculateForDesiredCornerWeight_Click()
        {
            try
            {
                M1_Global.List_I1[0].Hide();
                this.Hide();
                this.Show();
                R1_New.tabPaneResults.SelectedPage = R1.tabNavigationPageLinkLengths;

                double New_CW_FL, New_CW_FR, New_CW_RL, New_CW_RR;
                double New_WheelDeflectionFL, New_WheelDeflectionFR, New_WheelDeflectionRL, New_WheelDeflectionRR;
                double New_RideheightFL, New_RideheightFR, New_RideheightRL, New_RideheightRR;
                double New_PushRodFL, New_PushRodFR, New_PushRodRL, New_PushRodRR;
                double G1H1_Perp_FL, G1H1_Perp_FR, G1H1_Perp_RL, G1H1_Perp_RR, G1H1_FL, G1H1_FR, G1H1_RL, G1H1_RR;
                double alphaFL, alphaFR, alphaRL, alphaRR;
                double Delta_CW_FL, Delta_CW_FR, Delta_CW_RL, Delta_CW_RR;


                #region FRONT LEFT
                #region FRONT LEFT Calculation of New Ride Height for desired Corner Weight
                New_CW_FL = Convert.ToDouble(M1_Global.List_I1[0].CornerWeightFL.Text);

                if (String.Format("{0:0.000}",New_CW_FL)==String.Format("{0:0.000}",M1_Global.Assy_OC[0].CW))
                {
                    New_CW_FL = M1_Global.Assy_OC[0].CW;
                }

                Delta_CW_FL = (M1_Global.Assy_OC[0].CW - (New_CW_FL)); // If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                New_WheelDeflectionFL = -((9.81 * New_CW_FL) / (M1_Global.Assy_OC[0].RideRate));
                New_RideheightFL = -New_WheelDeflectionFL + M1_Global.Assy_OC[0].Corrected_WheelDeflection + M1_Global.Assy_OC[0].FinalRideHeight;
                #endregion

                #region FRONT LEFT Calculation of New Push Rod Length
                G1H1_Perp_FL = Math.Abs(M1_Global.Assy_SCM[0].G1x - M1_Global.Assy_SCM[0].H1x);
                G1H1_FL = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[0].G1x - M1_Global.Assy_SCM[0].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[0].G1y - M1_Global.Assy_SCM[0].H1y), 2));
                alphaFL = Math.Asin(G1H1_Perp_FL / G1H1_FL);
                New_PushRodFL = ((New_RideheightFL - M1_Global.Assy_OC[0].FinalRideHeight) / Math.Sin(alphaFL)) + M1_Global.Assy_SCM[0].PushRodLength;
                M1_Global.Assy_OC[0].CW += -(Delta_CW_FL);
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[3].CW += (Delta_CW_FL);

                #endregion
                #endregion

                #region FRONT RIGHT
                #region FRONT RIGHT Calculation of New Ride Height for desired Corner Weight
                New_CW_FR = Convert.ToDouble(M1_Global.List_I1[0].CornerWeightFR.Text);

                if (String.Format("{0:0.000}",New_CW_FR) == String.Format("{0:0.000}",M1_Global.Assy_OC[1].CW))
                {
                    New_CW_FR = M1_Global.Assy_OC[1].CW;
                }
                Delta_CW_FR = (M1_Global.Assy_OC[1].CW - (New_CW_FR ));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                New_WheelDeflectionFR = -((9.81 * New_CW_FR) / (M1_Global.Assy_OC[1].RideRate));
                New_RideheightFR = -New_WheelDeflectionFR + M1_Global.Assy_OC[1].Corrected_WheelDeflection + M1_Global.Assy_OC[1].FinalRideHeight;
                #endregion

                #region FRONT RIGHT Calculation of New Push Rod Length
                G1H1_Perp_FR = Math.Abs(M1_Global.Assy_SCM[1].G1x - M1_Global.Assy_SCM[1].H1x);
                G1H1_FR = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[1].G1x - M1_Global.Assy_SCM[1].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[1].G1y - M1_Global.Assy_SCM[1].H1y), 2));
                alphaFR = Math.Asin(G1H1_Perp_FR / G1H1_FR);
                New_PushRodFR = ((New_RideheightFR - M1_Global.Assy_OC[1].FinalRideHeight) / Math.Sin(alphaFR)) + M1_Global.Assy_SCM[1].PushRodLength;
                M1_Global.Assy_OC[1].CW += -Delta_CW_FR;
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[2].CW += Delta_CW_FR;

                #endregion
                #endregion

                #region REAR LEFT
                #region REAR LEFT Calculation of New Ride Height for desired Corner Weight
                New_CW_RL = Convert.ToDouble(M1_Global.List_I1[0].CornerWeightRL.Text);
                if (String.Format("{0:0.000}",New_CW_RL)==String.Format("{0:0.000}",M1_Global.Assy_OC[2].CW))
                {
                    New_CW_RL = M1_Global.Assy_OC[2].CW;
                }
                Delta_CW_RL = (M1_Global.Assy_OC[2].CW - (New_CW_RL));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                New_WheelDeflectionRL = -((9.81 * New_CW_RL) / (M1_Global.Assy_OC[2].RideRate));
                New_RideheightRL = -New_WheelDeflectionRL + M1_Global.Assy_OC[2].Corrected_WheelDeflection + M1_Global.Assy_OC[2].FinalRideHeight;
                #endregion

                #region REAR LEFT Calculation of New Push Rod Length
                G1H1_Perp_RL = Math.Abs(M1_Global.Assy_SCM[2].G1x - M1_Global.Assy_SCM[2].H1x);
                G1H1_RL = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[2].G1x - M1_Global.Assy_SCM[2].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[2].G1y - M1_Global.Assy_SCM[2].H1y), 2));
                alphaRL = Math.Asin(G1H1_Perp_RL / G1H1_RL);
                New_PushRodRL = ((New_RideheightRL - M1_Global.Assy_OC[2].FinalRideHeight) / Math.Sin(alphaRL)) + M1_Global.Assy_SCM[2].PushRodLength;
                M1_Global.Assy_OC[2].CW += -Delta_CW_RL;
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[1].CW += Delta_CW_RL;

                #endregion
                #endregion

                #region REAR RIGHT
                #region REAR RIGHT Calculation of New Ride Height for desired Corner Weight
                New_CW_RR = Convert.ToDouble(M1_Global.List_I1[0].CornerWeightRR.Text);

                if (String.Format("{0:0.000}",New_CW_RR)==String.Format("{0:0.000}",M1_Global.Assy_OC[3].CW))
                {
                    New_CW_RR = M1_Global.Assy_OC[3].CW;
                }
                
                Delta_CW_RR = (M1_Global.Assy_OC[3].CW - (New_CW_RR ));// If this value is negative, implies that the new Weight is lower than the old weight and hence droop condition
                New_WheelDeflectionRR = -((9.81 * New_CW_RR) / (M1_Global.Assy_OC[3].RideRate));
                New_RideheightRR = -New_WheelDeflectionRR + M1_Global.Assy_OC[3].Corrected_WheelDeflection + M1_Global.Assy_OC[3].FinalRideHeight;
                #endregion

                #region REAR RIGHT Calculation of New Push Rod Length
                G1H1_Perp_RR = Math.Abs(M1_Global.Assy_SCM[3].G1x - M1_Global.Assy_SCM[3].H1x);
                G1H1_RR = Math.Sqrt(Math.Pow((M1_Global.Assy_SCM[3].G1x - M1_Global.Assy_SCM[3].H1x), 2) + Math.Pow((M1_Global.Assy_SCM[3].G1y - M1_Global.Assy_SCM[3].H1y), 2));
                alphaRR = Math.Asin(G1H1_Perp_RR / G1H1_RR);
                New_PushRodRR = ((New_RideheightRR - M1_Global.Assy_OC[3].FinalRideHeight) / Math.Sin(alphaRR)) + M1_Global.Assy_SCM[3].PushRodLength;
                M1_Global.Assy_OC[3].CW += -Delta_CW_RR;
                // Adding the Lost/Gained Weights to the diagonally oppposite corners to maintain equilibrium
                M1_Global.Assy_OC[0].CW += Delta_CW_RR;

                #endregion
                #endregion


                //
                // Assigning new values of Pushrod Lengths
                M1_Global.Assy_SCM[0].PushRodLength = New_PushRodFL;
                M1_Global.Assy_SCM[1].PushRodLength = New_PushRodFR;
                M1_Global.Assy_SCM[2].PushRodLength = New_PushRodRL;
                M1_Global.Assy_SCM[3].PushRodLength = New_PushRodRR;

                //
                //Invoking the Kinematics and Vehicle Output Functions again 
                M1_Global.Assembled_Vehicle.KinematicsInvoker(R1);
                M1_Global.Assembled_Vehicle.VehicleOutputs(R1);



                #region Display of Outputs
                #region Display of Outputs of FRONT LEFT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].A2x))); A2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].A2x);
                A2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].A2y))); A2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].A2y);
                A2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].A2z))); A2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].A2z);
                B2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].B2x))); B2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].B2x);
                B2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].B2y))); B2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].B2y);
                B2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].B2z))); B2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].B2z);
                C2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].C2x))); C2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].C2x);
                C2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].C2y))); C2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].C2y);
                C2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].C2z))); C2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].C2z);
                D2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].D2x))); D2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].D2x);
                D2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].D2y))); D2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].D2y);
                D2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].D2z))); D2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].D2z);
                N2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].N2x))); N2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].N2x);
                N2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].N2y))); N2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].N2y);
                N2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].N2z))); N2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].N2z);
                Q2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Q2x))); Q2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Q2x);
                Q2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Q2y))); Q2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Q2y);
                Q2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Q2z))); Q2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Q2z);
                I2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].I2x))); I2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].I2x);
                I2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].I2y))); I2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].I2y);
                I2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].I2z))); I2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].I2z);
                JO2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].JO2x))); JO2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].JO2x);
                JO2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].JO2y))); JO2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].JO2y);
                JO2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].JO2z))); JO2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].J2x))); J2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].J2x);
                J2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].J2y))); J2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].J2y);
                J2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].J2z))); J2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].H2x))); H2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].H2x);
                H2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].H2y))); H2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].H2y);
                H2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].H2z))); H2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].O2x))); O2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].O2x);
                O2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].O2y))); O2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].O2y);
                O2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].O2z))); O2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].G2x))); G2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].G2x);
                G2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].G2y))); G2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].G2y);
                G2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].G2z))); G2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].F2x))); F2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].F2x);
                F2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].F2y))); F2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].F2y);
                F2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].F2z))); F2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].E2x))); E2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].E2x);
                E2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].E2y))); E2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].E2y);
                E2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].E2z))); E2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].InitialMR))); MotionRatioFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].InitialMR);
                InitialARBMRFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalMR))); FinalMotionRatioFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].FinalMR);
                FinalARBMRFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].M2x))); M2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].M2x);
                M2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].M2y))); M2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].M2y);
                M2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].M2z))); M2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].K2x))); K2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].K2x);
                K2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].K2y))); K2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].K2y);
                K2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].K2z))); K2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].L2x))); L2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].L2x);
                L2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].L2y))); L2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].L2y);
                L2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].L2z))); L2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].L2z);
                //To Display the New Camber and Toe
                FinalCamberFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalCamber))); FinalCamberFL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[0].FinalCamber * (180 / Math.PI)));
                FinalToeFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalToe))); FinalToeFL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[0].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].P2x))); P2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].P2x);
                P2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].P2y))); P2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].P2y);
                P2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].P2z))); P2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].W2x))); W2xFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].W2x);
                W2yFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].W2y))); W2yFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].W2y);
                W2zFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].W2z))); W2zFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].W2z);
                //Calculating The Final Ride Height 
                RideHeightFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].FinalRideHeight))); RideHeightFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].FinalRideHeight);
                //Calculating the New Corner Weights 
                CWFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].CW))); CWFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].CW);
                TireLoadedRadiusFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].TireLoadedRadius))); TireLoadedRadiusFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Corrected_SpringDeflection))); CorrectedSpringDeflectionFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Corrected_SpringDeflection);
                CorrectedWheelDeflectionFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].Corrected_WheelDeflection))); CorrectedWheelDeflectionFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].Corrected_WheelDeflection);
                NewDamperLengthFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].DamperLength))); NewDamperLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].New_NonSuspendedMassCoGx))); NewNSMCGFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].New_NonSuspendedMassCoGx);
                NewNSMCGFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].New_NonSuspendedMassCoGy))); NewNSMCGFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].New_NonSuspendedMassCoGy);
                NewNSMCGFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].New_NonSuspendedMassCoGz))); NewNSMCGFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront))); LowerFrontFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront);
                LowerRearFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear))); LowerRearFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear);
                UpperFrontFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront))); UpperFrontFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront);
                UpperRearFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear))); UpperRearFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear);
                PushRodFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod))); PushRodFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod);
                ToeLinkFL.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink))); ToeLinkFL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront_x))); LowerFrontChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront_x);
                LowerFrontChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront_y))); LowerFrontChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront_y);
                LowerFrontChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerFront_z))); LowerFrontChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerFront_z);
                LowerRearChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear_x))); LowerRearChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear_x);
                LowerRearChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear_y))); LowerRearChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear_y);
                LowerRearChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LowerRear_z))); LowerRearChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LowerRear_z);
                UpperFrontChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront_x))); UpperFrontChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront_x);
                UpperFrontChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront_y))); UpperFrontChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront_y);
                UpperFrontChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperFront_z))); UpperFrontChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperFront_z);
                UpperRearChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear_x))); UpperRearChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear_x);
                UpperRearChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear_y))); UpperRearChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear_y);
                UpperRearChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UpperRear_z))); UpperRearChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UpperRear_z);
                PushRodChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_x))); PushRodChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_x);
                PushRodChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_y))); PushRodChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_y);
                PushRodChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_z))); PushRodChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_z);
                PushRodUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_x))); PushRodUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_x);
                PushRodUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_y))); PushRodUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_y);
                PushRodUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].PushRod_z))); PushRodUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].PushRod_z);
                ToeLinkChassisFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_x))); ToeLinkChassisFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_x);
                ToeLinkChassisFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_y))); ToeLinkChassisFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_y);
                ToeLinkChassisFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_z))); ToeLinkChassisFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_z);
                ToeLinkUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_x))); ToeLinkUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_x);
                ToeLinkUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_y))); ToeLinkUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_y);
                ToeLinkUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].ToeLink_z))); ToeLinkUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_x))); LowerFrontUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_x);
                LowerFrontUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_y))); LowerFrontUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_y);
                LowerFrontUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_z))); LowerFrontUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_z);
                LowerRearUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_x))); LowerRearUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_x);
                LowerRearUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_y))); LowerRearUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_y);
                LowerRearUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].LBJ_z))); LowerRearUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].LBJ_z);
                UpperFrontUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_x))); UpperFrontUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_x);
                UpperFrontUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_y))); UpperFrontUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_y);
                UpperFrontUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_z))); UpperFrontUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_z);
                UpperRearUprightFLx.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_x))); UpperRearUprightFLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_x);
                UpperRearUprightFLy.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_y))); UpperRearUprightFLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_y);
                UpperRearUprightFLz.Text = Convert.ToString(((M1_Global.Assy_OC[0].UBJ_z))); UpperRearUprightFLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[0].UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].LowerFrontLength);
                LowerRearLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].LowerRearLength);
                UpperFrontLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].UpperFrontLength);
                UpperRearLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].UpperRearLength);
                PushRodLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].PushRodLength);
                PushRodLinkLengthFL.BackColor = Color.LimeGreen;
                CWFL.BackColor = Color.White;
                ToeLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].ToeLinkLength);
                ARBDroopLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].ARBDroopLinkLength);
                DamperLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].DamperLength);
                ARBLeverLinkLengthFL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[0].ARBBladeLength);


                #endregion

                #region Display of Outputs of FRONT RIGHT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].A2x))); A2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].A2x);
                A2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].A2y))); A2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].A2y);
                A2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].A2z))); A2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].A2z);
                B2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].B2x))); B2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].B2x);
                B2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].B2y))); B2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].B2y);
                B2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].B2z))); B2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].B2z);
                C2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].C2x))); C2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].C2x);
                C2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].C2y))); C2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].C2y);
                C2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].C2z))); C2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].C2z);
                D2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].D2x))); D2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].D2x);
                D2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].D2y))); D2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].D2y);
                D2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].D2z))); D2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].D2z);
                N2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].N2x))); N2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].N2x);
                N2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].N2y))); N2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].N2y);
                N2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].N2z))); N2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].N2z);
                Q2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Q2x))); Q2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Q2x);
                Q2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Q2y))); Q2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Q2y);
                Q2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Q2z))); Q2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Q2z);
                I2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].I2x))); I2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].I2x);
                I2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].I2y))); I2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].I2y);
                I2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].I2z))); I2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].I2z);
                JO2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].JO2x))); JO2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].JO2x);
                JO2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].JO2y))); JO2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].JO2y);
                JO2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].JO2z))); JO2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].J2x))); J2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].J2x);
                J2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].J2y))); J2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].J2y);
                J2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].J2z))); J2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].H2x))); H2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].H2x);
                H2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].H2y))); H2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].H2y);
                H2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].H2z))); H2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].O2x))); O2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].O2x);
                O2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].O2y))); O2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].O2y);
                O2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].O2z))); O2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].G2x))); G2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].G2x);
                G2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].G2y))); G2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].G2y);
                G2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].G2z))); G2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].F2x))); F2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].F2x);
                F2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].F2y))); F2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].F2y);
                F2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].F2z))); F2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].E2x))); E2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].E2x);
                E2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].E2y))); E2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].E2y);
                E2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].E2z))); E2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].InitialMR))); MotionRatioFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].InitialMR);
                InitialARBMRFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalMR))); FinalMotionRatioFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].FinalMR);
                FinalARBMRFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].M2x))); M2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].M2x);
                M2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].M2y))); M2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].M2y);
                M2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].M2z))); M2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].K2x))); K2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].K2x);
                K2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].K2y))); K2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].K2y);
                K2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].K2z))); K2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].L2x))); L2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].L2x);
                L2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].L2y))); L2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].L2y);
                L2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].L2z))); L2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].L2z);
                //To Display the New Camber and Toe
                FinalCamberFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalCamber))); FinalCamberFR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[1].FinalCamber * (180 / Math.PI)));
                FinalToeFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalToe))); FinalToeFR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[1].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].P2x))); P2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].P2x);
                P2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].P2y))); P2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].P2y);
                P2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].P2z))); P2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].W2x))); W2xFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].W2x);
                W2yFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].W2y))); W2yFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].W2y);
                W2zFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].W2z))); W2zFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].W2z);
                //Calculating The Final Ride Height 
                RideHeightFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].FinalRideHeight))); RideHeightFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].FinalRideHeight);
                //Calculating the New Corner Weights
                CWFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].CW))); CWFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].CW);
                TireLoadedRadiusFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].TireLoadedRadius))); TireLoadedRadiusFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Corrected_SpringDeflection))); CorrectedSpringDeflectionFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Corrected_SpringDeflection);
                CorrectedWheelDeflectionFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].Corrected_WheelDeflection))); CorrectedWheelDeflectionFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].Corrected_WheelDeflection);
                NewDamperLengthFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].DamperLength))); NewDamperLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].New_NonSuspendedMassCoGx))); NewNSMCGFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].New_NonSuspendedMassCoGx);
                NewNSMCGFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].New_NonSuspendedMassCoGy))); NewNSMCGFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].New_NonSuspendedMassCoGy);
                NewNSMCGFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].New_NonSuspendedMassCoGz))); NewNSMCGFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront))); LowerFrontFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront);
                LowerRearFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear))); LowerRearFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear);
                UpperFrontFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront))); UpperFrontFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront);
                UpperRearFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear))); UpperRearFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear);
                PushRodFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod))); PushRodFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod);
                ToeLinkFR.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink))); ToeLinkFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront_x))); LowerFrontChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront_x);
                LowerFrontChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront_y))); LowerFrontChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront_y);
                LowerFrontChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerFront_z))); LowerFrontChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerFront_z);
                LowerRearChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear_x))); LowerRearChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear_x);
                LowerRearChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear_y))); LowerRearChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear_y);
                LowerRearChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LowerRear_z))); LowerRearChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LowerRear_z);
                UpperFrontChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront_x))); UpperFrontChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront_x);
                UpperFrontChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront_y))); UpperFrontChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront_y);
                UpperFrontChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperFront_z))); UpperFrontChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperFront_z);
                UpperRearChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear_x))); UpperRearChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear_x);
                UpperRearChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear_y))); UpperRearChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear_y);
                UpperRearChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UpperRear_z))); UpperRearChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UpperRear_z);
                PushRodChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_x))); PushRodChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_x);
                PushRodChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_y))); PushRodChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_y);
                PushRodChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_z))); PushRodChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_z);
                PushRodUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_x))); PushRodUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_x);
                PushRodUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_y))); PushRodUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_y);
                PushRodUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].PushRod_z))); PushRodUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].PushRod_z);
                ToeLinkChassisFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_x))); ToeLinkChassisFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_x);
                ToeLinkChassisFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_y))); ToeLinkChassisFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_y);
                ToeLinkChassisFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_z))); ToeLinkChassisFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_z);
                ToeLinkUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_x))); ToeLinkUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_x);
                ToeLinkUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_y))); ToeLinkUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_y);
                ToeLinkUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].ToeLink_z))); ToeLinkUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_x))); LowerFrontUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_x);
                LowerFrontUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_y))); LowerFrontUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_y);
                LowerFrontUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_z))); LowerFrontUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_z);
                LowerRearUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_x))); LowerRearUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_x);
                LowerRearUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_y))); LowerRearUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_y);
                LowerRearUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].LBJ_z))); LowerRearUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].LBJ_z);
                UpperFrontUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_x))); UpperFrontUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_x);
                UpperFrontUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_y))); UpperFrontUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_y);
                UpperFrontUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_z))); UpperFrontUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_z);
                UpperRearUprightFRx.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_x))); UpperRearUprightFRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_x);
                UpperRearUprightFRy.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_y))); UpperRearUprightFRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_y);
                UpperRearUprightFRz.Text = Convert.ToString(((M1_Global.Assy_OC[1].UBJ_z))); UpperRearUprightFRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[1].UBJ_z);

                LowerFrontLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].LowerFrontLength);
                LowerRearLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].LowerRearLength);
                UpperFrontLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].UpperFrontLength);
                UpperRearLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].UpperRearLength);
                PushRodLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].PushRodLength);
                PushRodLinkLengthFR.BackColor = Color.LimeGreen;
                CWFR.BackColor = Color.White;
                ToeLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].ToeLinkLength);
                ARBDroopLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].ARBDroopLinkLength);
                DamperLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].DamperLength);
                ARBLeverLinkLengthFR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[1].ARBBladeLength);


                #endregion

                #region Display of Outputs of REAR LEFT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].A2x))); A2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].A2x);
                A2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].A2y))); A2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].A2y);
                A2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].A2z))); A2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].A2z);
                B2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].B2x))); B2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].B2x);
                B2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].B2y))); B2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].B2y);
                B2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].B2z))); B2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].B2z);
                C2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].C2x))); C2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].C2x);
                C2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].C2y))); C2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].C2y);
                C2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].C2z))); C2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].C2z);
                D2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].D2x))); D2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].D2x);
                D2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].D2y))); D2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].D2y);
                D2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].D2z))); D2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].D2z);
                N2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].N2x))); N2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].N2x);
                N2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].N2y))); N2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].N2y);
                N2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].N2z))); N2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].N2z);
                Q2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Q2x))); Q2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Q2x);
                Q2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Q2y))); Q2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Q2y);
                Q2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Q2z))); Q2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Q2z);
                I2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].I2x))); I2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].I2x);
                I2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].I2y))); I2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].I2y);
                I2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].I2z))); I2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].I2z);
                JO2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].JO2x))); JO2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].JO2x);
                JO2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].JO2y))); JO2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].JO2y);
                JO2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].JO2z))); JO2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].J2x))); J2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].J2x);
                J2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].J2y))); J2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].J2y);
                J2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].J2z))); J2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].H2x))); H2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].H2x);
                H2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].H2y))); H2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].H2y);
                H2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].H2z))); H2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].O2x))); O2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].O2x);
                O2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].O2y))); O2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].O2y);
                O2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].O2z))); O2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].G2x))); G2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].G2x);
                G2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].G2y))); G2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].G2y);
                G2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].G2z))); G2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].F2x))); F2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].F2x);
                F2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].F2y))); F2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].F2y);
                F2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].F2z))); F2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].E2x))); E2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].E2x);
                E2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].E2y))); E2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].E2y);
                E2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].E2z))); E2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].InitialMR))); MotionRatioRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].InitialMR);
                InitialARBMRRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalMR))); FinalMotionRatioRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].FinalMR);
                FinalARBMRFR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].M2x))); M2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].M2x);
                M2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].M2y))); M2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].M2y);
                M2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].M2z))); M2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].K2x))); K2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].K2x);
                K2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].K2y))); K2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].K2y);
                K2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].K2z))); K2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].L2x))); L2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].L2x);
                L2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].L2y))); L2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].L2y);
                L2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].L2z))); L2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].L2z);
                //To Display the New Camber and Toe
                FinalCamberRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalCamber))); FinalCamberRL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[2].FinalCamber * (180 / Math.PI)));
                FinalToeRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalToe))); FinalToeRL.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[2].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].P2x))); P2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].P2x);
                P2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].P2y))); P2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].P2y);
                P2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].P2z))); P2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].W2x))); W2xRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].W2x);
                W2yRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].W2y))); W2yRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].W2y);
                W2zRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].W2z))); W2zRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].W2z);
                //Calculating The Final Ride Height 
                RideHeightRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].FinalRideHeight))); RideHeightRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].FinalRideHeight);
                //Calculating the New Corner Weights
                CWRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].CW))); CWRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].CW);
                TireLoadedRadiusRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].TireLoadedRadius))); TireLoadedRadiusRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Corrected_SpringDeflection))); CorrectedSpringDeflectionRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Corrected_SpringDeflection);
                CorrectedWheelDeflectionRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].Corrected_WheelDeflection))); CorrectedWheelDeflectionRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].Corrected_WheelDeflection);
                NewDamperLengthRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].DamperLength))); NewDamperLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].New_NonSuspendedMassCoGx))); NewNSMCGRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].New_NonSuspendedMassCoGx);
                NewNSMCGRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].New_NonSuspendedMassCoGy))); NewNSMCGRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].New_NonSuspendedMassCoGy);
                NewNSMCGRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].New_NonSuspendedMassCoGz))); NewNSMCGRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront))); LowerFrontRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront);
                LowerRearRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear))); LowerRearRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear);
                UpperFrontRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront))); UpperFrontRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront);
                UpperRearRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear))); UpperRearRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear);
                PushRodRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod))); PushRodRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod);
                ToeLinkRL.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink))); ToeLinkRL.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront_x))); LowerFrontChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront_x);
                LowerFrontChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront_y))); LowerFrontChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront_y);
                LowerFrontChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerFront_z))); LowerFrontChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerFront_z);
                LowerRearChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear_x))); LowerRearChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear_x);
                LowerRearChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear_y))); LowerRearChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear_y);
                LowerRearChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LowerRear_z))); LowerRearChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LowerRear_z);
                UpperFrontChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront_x))); UpperFrontChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront_x);
                UpperFrontChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront_y))); UpperFrontChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront_y);
                UpperFrontChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperFront_z))); UpperFrontChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperFront_z);
                UpperRearChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear_x))); UpperRearChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear_x);
                UpperRearChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear_y))); UpperRearChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear_y);
                UpperRearChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UpperRear_z))); UpperRearChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UpperRear_z);
                PushRodChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_x))); PushRodChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_x);
                PushRodChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_y))); PushRodChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_y);
                PushRodChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_z))); PushRodChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_z);
                PushRodUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_x))); PushRodUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_x);
                PushRodUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_y))); PushRodUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_y);
                PushRodUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].PushRod_z))); PushRodUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].PushRod_z);
                ToeLinkChassisRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_x))); ToeLinkChassisRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_x);
                ToeLinkChassisRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_y))); ToeLinkChassisRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_y);
                ToeLinkChassisRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_z))); ToeLinkChassisRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_z);
                ToeLinkUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_x))); ToeLinkUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_x);
                ToeLinkUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_y))); ToeLinkUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_y);
                ToeLinkUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].ToeLink_z))); ToeLinkUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_x))); LowerFrontUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_x);
                LowerFrontUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_y))); LowerFrontUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_y);
                LowerFrontUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_z))); LowerFrontUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_z);
                LowerRearUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_x))); LowerRearUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_x);
                LowerRearUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_y))); LowerRearUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_y);
                LowerRearUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].LBJ_z))); LowerRearUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].LBJ_z);
                UpperFrontUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_x))); UpperFrontUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_x);
                UpperFrontUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_y))); UpperFrontUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_y);
                UpperFrontUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_z))); UpperFrontUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_z);
                UpperRearUprightRLx.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_x))); UpperRearUprightRLx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_x);
                UpperRearUprightRLy.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_y))); UpperRearUprightRLy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_y);
                UpperRearUprightRLz.Text = Convert.ToString(((M1_Global.Assy_OC[2].UBJ_z))); UpperRearUprightRLz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[2].UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].LowerFrontLength);
                LowerRearLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].LowerRearLength);
                UpperFrontLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].UpperFrontLength);
                UpperRearLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].UpperRearLength);
                PushRodLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].PushRodLength);
                PushRodLinkLengthRL.BackColor = Color.LimeGreen;
                CWRL.BackColor = Color.White;
                ToeLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].ToeLinkLength);
                ARBDroopLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].ARBDroopLinkLength);
                DamperLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].DamperLength);
                ARBLeverLinkLengthRL.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[2].ARBBladeLength);

                #endregion

                #region Display of Outputs of REAR RIGHT
                //Displaying the New positions of the Fixed Points (Incase we are translating them to WCS    
                A2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].A2x))); A2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].A2x);
                A2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].A2y))); A2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].A2y);
                A2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].A2z))); A2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].A2z);
                B2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].B2x))); B2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].B2x);
                B2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].B2y))); B2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].B2y);
                B2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].B2z))); B2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].B2z);
                C2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].C2x))); C2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].C2x);
                C2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].C2y))); C2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].C2y);
                C2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].C2z))); C2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].C2z);
                D2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].D2x))); D2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].D2x);
                D2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].D2y))); D2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].D2y);
                D2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].D2z))); D2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].D2z);
                N2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].N2x))); N2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].N2x);
                N2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].N2y))); N2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].N2y);
                N2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].N2z))); N2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].N2z);
                Q2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Q2x))); Q2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Q2x);
                Q2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Q2y))); Q2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Q2y);
                Q2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Q2z))); Q2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Q2z);
                I2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].I2x))); I2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].I2x);
                I2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].I2y))); I2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].I2y);
                I2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].I2z))); I2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].I2z);
                JO2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].JO2x))); JO2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].JO2x);
                JO2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].JO2y))); JO2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].JO2y);
                JO2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].JO2z))); JO2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].JO2z);
                // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
                J2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].J2x))); J2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].J2x);
                J2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].J2y))); J2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].J2y);
                J2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].J2z))); J2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].J2z);
                // TO CALCULATE THE NEW POSITION OF H i.e., TO CALCULATE H'
                H2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].H2x))); H2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].H2x);
                H2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].H2y))); H2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].H2y);
                H2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].H2z))); H2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].H2z);
                // TO CALCULATE THE NEW POSITION OF O i.e., TO CALCULATE O'
                O2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].O2x))); O2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].O2x);
                O2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].O2y))); O2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].O2y);
                O2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].O2z))); O2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].O2z);
                // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
                G2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].G2x))); G2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].G2x);
                G2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].G2y))); G2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].G2y);
                G2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].G2z))); G2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].G2z);
                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' 
                F2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].F2x))); F2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].F2x);
                F2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].F2y))); F2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].F2y);
                F2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].F2z))); F2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].F2z);
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
                E2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].E2x))); E2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].E2x);
                E2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].E2y))); E2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].E2y);
                E2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].E2z))); E2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].E2z);
                // TO CALCULATE THE Initial Spring and Anti-Roll Bar Motion Ratio
                MotionRatioRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].InitialMR))); MotionRatioRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].InitialMR);
                InitialARBMRRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Initial_ARB_MR);
                // TO CALCULATE THE Final Spring and Anti-Roll Bar Motion Ratio
                FinalMotionRatioRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalMR))); FinalMotionRatioRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].FinalMR);
                FinalARBMRRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Final_ARB_MR);
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                M2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].M2x))); M2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].M2x);
                M2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].M2y))); M2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].M2y);
                M2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].M2z))); M2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].M2z);
                // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
                K2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].K2x))); K2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].K2x);
                K2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].K2y))); K2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].K2y);
                K2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].K2z))); K2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].K2z);
                // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                L2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].L2x))); L2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].L2x);
                L2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].L2y))); L2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].L2y);
                L2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].L2z))); L2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].L2z);
                //To Display the New Camber and Toe
                FinalCamberRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalCamber))); FinalCamberRR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[3].FinalCamber * (180 / Math.PI)));
                FinalToeRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalToe))); FinalToeRR.Text = String.Format("{0:0.000}", (M1_Global.Assy_OC[3].FinalToe * (180 / Math.PI)));
                // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
                P2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].P2x))); P2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].P2x);
                P2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].P2y))); P2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].P2y);
                P2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].P2z))); P2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].P2z);
                // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                W2xRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].W2x))); W2xRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].W2x);
                W2yRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].W2y))); W2yRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].W2y);
                W2zRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].W2z))); W2zRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].W2z);
                //Calculating The Final Ride Height 
                RideHeightRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].FinalRideHeight))); RideHeightRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].FinalRideHeight);
                //Calculating the New Corner Weights
                CWRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].CW))); CWRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].CW);
                TireLoadedRadiusRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].TireLoadedRadius))); TireLoadedRadiusRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].TireLoadedRadius);
                //Calculating the New Spring, Damper and Wheel Deflections
                CorrectedSpringDeflectionRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Corrected_SpringDeflection))); CorrectedSpringDeflectionRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Corrected_SpringDeflection);
                CorrectedWheelDeflectionRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].Corrected_WheelDeflection))); CorrectedWheelDeflectionRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].Corrected_WheelDeflection);
                NewDamperLengthRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].DamperLength))); NewDamperLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].DamperLength);
                //Calculating the new CG coordinates of the Non Suspended Mass
                NewNSMCGRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].New_NonSuspendedMassCoGx))); NewNSMCGRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].New_NonSuspendedMassCoGx);
                NewNSMCGRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].New_NonSuspendedMassCoGy))); NewNSMCGRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].New_NonSuspendedMassCoGy);
                NewNSMCGRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].New_NonSuspendedMassCoGz))); NewNSMCGRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].New_NonSuspendedMassCoGz);
                //Calculating the Wishbone Forces
                LowerFrontRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront))); LowerFrontRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront);
                LowerRearRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear))); LowerRearRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear);
                UpperFrontRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront))); UpperFrontRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront);
                UpperRearRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear))); UpperRearRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear);
                PushRodRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod))); PushRodRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod);
                ToeLinkRR.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink))); ToeLinkRR.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink);
                //Chassic Pick Up Points in XYZ direction
                LowerFrontChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront_x))); LowerFrontChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront_x);
                LowerFrontChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront_y))); LowerFrontChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront_y);
                LowerFrontChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerFront_z))); LowerFrontChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerFront_z);
                LowerRearChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear_x))); LowerRearChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear_x);
                LowerRearChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear_y))); LowerRearChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear_y);
                LowerRearChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LowerRear_z))); LowerRearChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LowerRear_z);
                UpperFrontChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront_x))); UpperFrontChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront_x);
                UpperFrontChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront_y))); UpperFrontChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront_y);
                UpperFrontChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperFront_z))); UpperFrontChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperFront_z);
                UpperRearChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear_x))); UpperRearChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear_x);
                UpperRearChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear_y))); UpperRearChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear_y);
                UpperRearChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UpperRear_z))); UpperRearChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UpperRear_z);
                PushRodChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_x))); PushRodChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_x);
                PushRodChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_y))); PushRodChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_y);
                PushRodChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_z))); PushRodChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_z);
                PushRodUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_x))); PushRodUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_x);
                PushRodUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_y))); PushRodUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_y);
                PushRodUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].PushRod_z))); PushRodUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].PushRod_z);
                ToeLinkChassisRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_x))); ToeLinkChassisRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_x);
                ToeLinkChassisRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_y))); ToeLinkChassisRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_y);
                ToeLinkChassisRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_z))); ToeLinkChassisRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_z);
                ToeLinkUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_x))); ToeLinkUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_x);
                ToeLinkUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_y))); ToeLinkUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_y);
                ToeLinkUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].ToeLink_z))); ToeLinkUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].ToeLink_z);
                //Upper and Lower Ball Joint Forces in XYZ Direction
                LowerFrontUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_x))); LowerFrontUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_x);
                LowerFrontUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_y))); LowerFrontUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_y);
                LowerFrontUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_z))); LowerFrontUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_z);
                LowerRearUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_x))); LowerRearUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_x);
                LowerRearUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_y))); LowerRearUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_y);
                LowerRearUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].LBJ_z))); LowerRearUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].LBJ_z);
                UpperFrontUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_x))); UpperFrontUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_x);
                UpperFrontUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_y))); UpperFrontUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_y);
                UpperFrontUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_z))); UpperFrontUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_z);
                UpperRearUprightRRx.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_x))); UpperRearUprightRRx.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_x);
                UpperRearUprightRRy.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_y))); UpperRearUprightRRy.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_y);
                UpperRearUprightRRz.Text = Convert.ToString(((M1_Global.Assy_OC[3].UBJ_z))); UpperRearUprightRRz.Text = String.Format("{0:0.000}", M1_Global.Assy_OC[3].UBJ_z);

                // Link Lengths
                LowerFrontLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].LowerFrontLength);
                LowerRearLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].LowerRearLength);
                UpperFrontLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].UpperFrontLength);
                UpperRearLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].UpperRearLength);
                PushRodLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].PushRodLength);
                PushRodLinkLengthRR.BackColor = Color.LimeGreen;
                CWRR.BackColor = Color.White;
                ToeLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].ToeLinkLength);
                ARBDroopLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].ARBDroopLinkLength);
                DamperLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].DamperLength);
                ARBLeverLinkLengthRR.Text = String.Format("{0:0.000}", M1_Global.Assy_SCM[3].ARBBladeLength);


                #endregion

                NewWheelBase.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_WheelBase))); NewWheelBase.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_WheelBase);
                NewTrackFront.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_TrackF))); NewTrackFront.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_TrackF);
                NewTrackRear.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_TrackR))); NewTrackRear.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_TrackR);

                NewSuspendedMassCGx.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_SMCoGx))); NewSuspendedMassCGx.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_SMCoGx);
                NewSuspendedMassCGz.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_SMCoGz))); NewSuspendedMassCGz.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_SMCoGz);
                NewSuspendedMassCGy.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.New_SMCoGy))); NewSuspendedMassCGy.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.New_SMCoGy);

                RollAngleChassis.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.RollAngle_Chassis))); RollAngleChassis.Text = String.Format("{0:0.000}", (M1_Global.Assembled_Vehicle.RollAngle_Chassis * (180 / Math.PI)));
                PitchAngleChassis.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.PitchAngle_Chassis))); PitchAngleChassis.Text = String.Format("{0:0.000}", (M1_Global.Assembled_Vehicle.PitchAngle_Chassis * (180 / Math.PI)));

                ARBMotionRatioFront.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.ARB_MR_Front))); ARBMotionRatioFront.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.ARB_MR_Front);
                ARBMotionRatioRear.Text = Convert.ToString(((M1_Global.Assembled_Vehicle.ARB_MR_Rear))); ARBMotionRatioRear.Text = String.Format("{0:0.000}", M1_Global.Assembled_Vehicle.ARB_MR_Rear);

                #endregion



            }
            catch (Exception)
            {

                MessageBox.Show(" Initial Calculations have not been done");
            }


        }

        private void tabPaneResults_Click(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void PullRodIdentifierFront_CheckedChanged(object sender, EventArgs e)
        {
            if (PullRodIdentifierFront.Checked)
            {
 
                label41.Text = "Pull Rod";
                label858.Text = "Pull Rod";
                navigationPagePushRodFL.Caption = "Pullrod FL";
                navigationPagePushRodFR.Caption = "Pullrod FR";
            }
            else
            {

                label41.Text = "Push Rod";
                label858.Text = "Push Rod";
                navigationPagePushRodFL.Caption = "Pushrod FL";
                navigationPagePushRodFR.Caption = "Pushrod FR";
            }



        }

        private void PullRodIdentifierRear_CheckedChanged(object sender, EventArgs e)
        {

            if (PullRodIdentifierRear.Checked)
            {

                label53.Text = "Pull Rod";
                label65.Text = "Pull Rod";
                navigationPagePushRodRL.Caption = "Pullrod RL";
                navigationPagePushRodRR.Caption = "Pullrod RR";
            }
            else
            {

                label53.Text = "Push Rod";
                label65.Text = "Push Rod";
                navigationPagePushRodFL.Caption = "Pushrod RL";
                navigationPagePushRodFR.Caption = "Pushrod R  R";
            }

        }

        private void FrontTARBIdentifier_CheckedChanged(object sender, EventArgs e)
        {

            if (FrontTARBIdentifier.Checked)
            {
                #region Displaying the FRONT Torsion Bar Bottom Point
                accordionControlFixedPointFLTorsionBarBottom.Visible = true;
                accordionControlFixedPointFRTorsionBarBottom.Visible = true;
                #endregion

                #region Changing the values of the FRONT Textboxes to reflect a Bell Crank whose Axis is at an angle with respect to XYZ axes
                Q1xFL.Text = "0";
                Q1yFL.Text = "1492.022";
                Q1zFL.Text = "98.061";
                JO1xFL.Text = "-63.115";
                JO1yFL.Text = "1566.974";
                JO1zFL.Text = "96.676";
                P1xFL.Text = "-136.833";
                P1yFL.Text = "1492.022";
                P1zFL.Text = "98.061";
                J1xFL.Text = "-217.557";
                J1yFL.Text = "1583.889";
                J1zFL.Text = "-29.548";
                O1xFL.Text = "-224.435";
                O1yFL.Text = "1568.017";
                O1zFL.Text = "-22.828";
                H1xFL.Text = "-289.433";
                H1yFL.Text = "1536.817";
                H1zFL.Text = "-47.502";

                Q1xFR.Text = "0";
                Q1yFR.Text = "1492.022";
                Q1zFR.Text = "98.061";
                JO1xFR.Text = "63.115";
                JO1yFR.Text = "1566.974";
                JO1zFR.Text = "96.676";
                P1xFR.Text = "136.833";
                P1yFR.Text = "1492.022";
                P1zFR.Text = "98.061";
                J1xFR.Text = "217.557";
                J1yFR.Text = "1583.889";
                J1zFR.Text = "-29.548";
                O1xFR.Text = "224.435";
                O1yFR.Text = "1568.017";
                O1zFR.Text = "-22.828";
                H1xFR.Text = "289.433";
                H1yFR.Text = "1536.817";
                H1zFR.Text = "-47.502";



                #endregion

            }
            else
            {
                #region Hiding the FRONT Torsion Bar Bottom Point
                accordionControlFixedPointFLTorsionBarBottom.Visible = false;
                accordionControlFixedPointFRTorsionBarBottom.Visible = false;
                #endregion

                #region Changing the FRONT Textboxes to reflect a Bell Crank whos Axis is Parallel to Z axis and Perpendicular to Y and X axes (Default Bell CranK)
                Q1xFL.Text = "-284";
                Q1yFL.Text = "1033";
                Q1zFL.Text = "60.80";
                JO1xFL.Text = "-36";
                JO1yFL.Text = "1572";
                JO1zFL.Text = "-6.73";
                P1xFL.Text = "-278.97";
                P1yFL.Text = "1021.58";
                P1zFL.Text = "-5.57";
                J1xFL.Text = "-235.10";
                J1yFL.Text = "1592.74";
                J1zFL.Text = "-6.73";
                O1xFL.Text = "-277.03";
                O1yFL.Text = "1504.96";
                O1zFL.Text = "-6.73";
                H1xFL.Text = "-298.55";
                H1yFL.Text = "1557.22";
                H1zFL.Text = "-6.73";

                Q1xFR.Text = "284";
                Q1yFR.Text = "1033";
                Q1zFR.Text = "60.80";
                JO1xFR.Text = "36";
                JO1yFR.Text = "1572";
                JO1zFR.Text = "-6.73";
                P1xFR.Text = "278.97";
                P1yFR.Text = "1021.58";
                P1zFR.Text = "-5.57";
                J1xFR.Text = "235.10";
                J1yFR.Text = "1592.74";
                J1zFR.Text = "-6.73";
                O1xFR.Text = "277.03";
                O1yFR.Text = "1504.96";
                O1zFR.Text = "-6.73";
                H1xFR.Text = "298.55";
                H1yFR.Text = "1557.22";
                H1zFR.Text = "-6.73";

                #endregion

            }
        }

        private void ReatTARBIdentifier_CheckedChanged(object sender, EventArgs e)
        {
            if (RearTARBIdentifier.Checked)
            {
                #region Displaying the REAR Torsion Bar Bottom Point
                accordionControlFixedPointsRLTorsionBarBottom.Visible= true;
                accordionControlFixedPointsRRTorsionBarBottom.Visible = true;
                #endregion

                #region Changing the values of the REAR Textboxes to reflect a Bell Crank whose Axis is at an angle with respect to XYZ axes
                Q1xRL.Text = "0";
                Q1yRL.Text = "1294.733";
                Q1zRL.Text = "-1440.473";
                JO1xRL.Text = "-24.093";
                JO1yRL.Text = "1355.491";
                JO1zRL.Text = "-1449.481";
                P1xRL.Text = "-135.166";
                P1yRL.Text = "1294.733";
                P1zRL.Text = "-1440.473";
                J1xRL.Text = "-201.496";
                J1yRL.Text = "1375.809";
                J1zRL.Text = "-1539.509";
                O1xRL.Text = "-207.876";
                O1yRL.Text = "1360.199";
                O1zRL.Text = "-1528.209";
                H1xRL.Text = "-259.957";
                H1yRL.Text = "1331.938";
                H1zRL.Text = "-1524.188";

                Q1xRR.Text = "0";
                Q1yRR.Text = "1294.733";
                Q1zRR.Text = "-1440.473";
                JO1xRR.Text = "24.093";
                JO1yRR.Text = "1355.491";
                JO1zRR.Text = "-1449.481";
                P1xRR.Text = "135.166";
                P1yRR.Text = "1294.733";
                P1zRR.Text = "-1440.473";
                J1xRR.Text = "201.496";
                J1yRR.Text = "1375.809";
                J1zRR.Text = "-1539.509";
                O1xRR.Text = "207.876";
                O1yRR.Text = "1360.199";
                O1zRR.Text = "-1528.209";
                H1xRR.Text = "259.957";
                H1yRR.Text = "1331.938";
                H1zRR.Text = "-1524.188";
                #endregion
            }
            else
            {
                #region Hiding the REAR Torsion Bar Bottom Point
                accordionControlFixedPointsRLTorsionBarBottom.Visible = false;
                accordionControlFixedPointsRRTorsionBarBottom.Visible = false;
                #endregion

                #region Changing the REAR Textboxes to reflect a Bell Crank whos Axis is Parallel to Z axis and Perpendicular to Y and X axes (Default Bell CranK)
                Q1xRL.Text = "-265";
                Q1yRL.Text = "1068";
                Q1zRL.Text = "-1548.13";
                JO1xRL.Text = "-18";
                JO1yRL.Text = "1367";
                JO1zRL.Text = "-1505";
                P1xRL.Text = "-260.21";
                P1yRL.Text = "1055.57";
                P1zRL.Text = "-1507.11";
                J1xRL.Text = "-216.65";
                J1yRL.Text = "1389.98";
                J1zRL.Text = "-1505";
                O1xRL.Text = "-255.99";
                O1yRL.Text = "1302.71";
                O1zRL.Text = "-1505";
                H1xRL.Text = "-267.74";
                H1yRL.Text = "1349.01";
                H1zRL.Text = "-1505";

                Q1xRR.Text = "265";
                Q1yRR.Text = "1068";
                Q1zRR.Text = "-1548.13";
                JO1xRR.Text = "18";
                JO1yRR.Text = "1367";
                JO1zRR.Text = "-1505";
                P1xRR.Text = "260.21";
                P1yRR.Text = "1055.57";
                P1zRR.Text = "-1507.11";
                J1xRR.Text = "216.65";
                J1yRR.Text = "1389.98";
                J1zRR.Text = "-1505";
                O1xRR.Text = "255.99";
                O1yRR.Text = "1302.71";
                O1zRR.Text = "-1505";
                H1xRR.Text = "267.74";
                H1yRR.Text = "1349.01";
                H1zRR.Text = "-1505";

                #endregion


            }
      
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Stop();

        }

        private void CreateInputSheet_Click(object sender, EventArgs e)
        {
            try
            {
                M1_Global.List_I1[0].Show();

                #region Disabling and Colouring the PushRod and Corner Weight Textboxes so that user doesnt change anything before Initiial Calculation of results
                M1_Global.List_I1[0].PushRodFL.Enabled = false;
                M1_Global.List_I1[0].PushRodFL.BackColor = Color.White;
                M1_Global.List_I1[0].PushRodFR.Enabled = false;
                M1_Global.List_I1[0].PushRodFR.BackColor = Color.White;
                M1_Global.List_I1[0].PushRodRL.Enabled = false;
                M1_Global.List_I1[0].PushRodRL.BackColor = Color.White;
                M1_Global.List_I1[0].PushRodRR.Enabled = false;
                M1_Global.List_I1[0].PushRodRR.BackColor = Color.White;
                M1_Global.List_I1[0].RecalculateCornerWeightForPushRodLength.Enabled = false;

                M1_Global.List_I1[0].CornerWeightFL.Enabled = false;
                M1_Global.List_I1[0].CornerWeightFL.BackColor = Color.White;
                M1_Global.List_I1[0].CornerWeightFR.Enabled = false;
                M1_Global.List_I1[0].CornerWeightFR.BackColor = Color.White;
                M1_Global.List_I1[0].CornerWeightRL.Enabled = false;
                M1_Global.List_I1[0].CornerWeightRL.BackColor = Color.White;
                M1_Global.List_I1[0].CornerWeightRR.Enabled = false;
                M1_Global.List_I1[0].CornerWeightRR.BackColor = Color.White;
                M1_Global.List_I1[0].RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;
                #endregion


            }
            catch (Exception)
            {

                MessageBox.Show("Input Sheet hass not been populated. Please check if Vehicle has been created properly");
            }

            
        }

        private void HideInputSheet_Click(object sender, EventArgs e)
        {
            M1_Global.List_I1[0].Hide();

        }

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


        private void DamperGasPressureFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            foreach (Control c in this.Controls)
            {
                AccordionControl a = c as AccordionControl;
                if (a!=null)
                {
                    MessageBox.Show("Radhe Krishna");
                }

                
            }











            if (!Char.IsDigit(ch) && ch != 8 && ch != 13 && ch != 45 && ch != 46 && ch != 43 && ch != 9)
            {
                e.Handled = true;

            }


         



        }

        private void accordionControlDamperSet3_Click(object sender, EventArgs e)
        {

        }

        private void barButtonSave_ItemClicked(object sender, ItemClickEventArgs e)
        {





        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"D:\";
            saveFileDialog1.Title = "Save Input Values";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;


            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DamperGasPressureFL.Text = saveFileDialog1.FileName;
            }
        }


        
























        







        



     


       



































































    }


}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    public partial class SuspensionType : DevExpress.XtraEditors.XtraForm
    {
        public Kinematics_Software_New R1;

        public bool OnlyTemplate_ImportCAD { get; set; }

        public bool OnlyTemplate_DesignMode { get; set; }

        XUC_CoordinateMap coordinateMap;


        #region --Properties--
        /// <summary>
        /// Front Double WIshbone Identifier
        /// </summary>
        public int DoubleWishboneFront { get; set; } = 1;

        /// <summary>
        /// Front McPherson Identifier
        /// </summary>
        public int McPhersonFront { get; set; }

        /// <summary>
        /// Rear Double Wishbone Identifier
        /// </summary>
        public int DoubleWishboneRear { get; set; } = 1;

        /// <summary>
        /// Rear McPherson Identifier
        /// </summary>
        public int McPhersonRear { get; set; }

        /// <summary>
        /// Front UARB Idetnfier
        /// </summary>
        public int UARBFront { get; set; } = 1;

        /// <summary>
        /// Front TARB Idetnfier
        /// </summary>
        public int TARBFront { get; set; }

        /// <summary>
        /// Rear UARB Idetnfier
        /// </summary>
        public int UARBRear { get; set; } = 1;

        /// <summary>
        /// Rear TARB Idetnfier
        /// </summary>
        public int TARBRear { get; set; }

        /// <summary>
        /// Front Pushrod Identifier
        /// </summary>
        public int PushrodFront { get; set; } = 1;

        /// <summary>
        /// Front Pullhrod Identifier
        /// </summary>
        public int PullrodFront { get; set; }

        /// <summary>
        /// Rear Pushrod Identifier
        /// </summary>
        public int PushrodRear { get; set; } = 1;

        /// <summary>
        /// Rear Pullrod Identifier
        /// </summary>
        public int PullrodRear { get; set; }

        /// <summary>
        /// Rack and Pinion type steering identifier
        /// </summary>
        public int RackAndPinion { get; set; } = 1;

        /// <summary>
        /// Number of Couplings
        /// </summary>
        public int NoOfCouplings { get; set; } = 2;

        /// <summary>
        /// Front Symmetry identifier
        /// </summary>
        public bool FrontSymmetry_Boolean { get; set; } = true;

        /// <summary>
        /// Rear Symmetry identifier
        /// </summary>
        public bool RearSymmetry_Boolean { get; set; } = true;
        #endregion

        #region Constructor
        public SuspensionType(Kinematics_Software_New r1)
        {
            InitializeComponent();
            InitializeVariables();
            R1 = r1;


        }

        public SuspensionType(bool _importCAD, XUC_CoordinateMap _coordinateMap)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();
            InitializeComponent();
            InitializeVariables();
            OnlyTemplate_ImportCAD = _importCAD;
            coordinateMap = _coordinateMap;

        }

        public SuspensionType(bool _onlyTemplate_DesignMode)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();
            InitializeComponent();
            InitializeVariables();
            OnlyTemplate_DesignMode = _onlyTemplate_DesignMode;
        }

        private void InitializeVariables()
        {
            Width = 250;
            Height = 625;

            DoubleWishboneFront = 0;
            McPhersonFront = 0;
            DoubleWishboneRear = 0;
            McPhersonRear = 0;

            PushrodFront = 0;
            PullrodFront = 0;
            PushrodRear = 0;
            PullrodRear = 0;

            UARBFront = 0;
            TARBFront = 0;
            UARBRear = 0;
            TARBRear = 0;

            RackAndPinion = 0;
            NoOfCouplings = 0;

        }
        #endregion

        #region Form Load Event
        private void SuspensionType_Load(object sender, EventArgs e)
        {
            radioButtonDoubleWishboneFront_CheckedChanged(sender, e);
            radioButtonDoubleWishboneRear_CheckedChanged(sender, e);

            radioButtonPushrodFront_CheckedChanged(sender, e);
            radioButtonPushrodRear_CheckedChanged(sender, e);

            radioButtonUbarFront_CheckedChanged(sender, e);
            radioButtonUbarRear_CheckedChanged(sender, e);

            FrontSymmetry_CheckedChanged(sender, e);
            RearSymmetry_CheckedChanged(sender, e);

            radioButton2UV_CheckedChanged(sender, e);

        }
        #endregion

        #region Code for GUI and to pass Geometry Identifier



        private void radioButtonDoubleWishboneFront_CheckedChanged(object sender, EventArgs e)
        {

            accordionControlActuationTypeFront.Visible = true;
            accordionControlAntiRollBarTypeFront.Visible = true;


            if (radioButtonDoubleWishboneFront.Checked == true)
            {
                DoubleWishboneFront = 1;
            }
            else if (radioButtonDoubleWishboneFront.Checked == false)
            {
                DoubleWishboneFront = 0;
            }

        }


        private void radioButtonMcPhersonFront_CheckedChanged(object sender, EventArgs e)
        {
            accordionControlActuationTypeFront.Visible = false;
            accordionControlAntiRollBarTypeFront.Visible = false;

            radioButtonPullrodFront.Checked = false;
            radioButtonPushrodFront.Checked = false;
            radioButtonTARBFront.Checked = false;
            radioButtonUbarFront.Checked = false;


            if (radioButtonMcPhersonFront.Checked == true)
            {
                McPhersonFront = 1;
            }
            else if (radioButtonMcPhersonFront.Checked == false)
            {
                McPhersonFront = 0;
            }
        }


        private void radioButtonDoubleWishboneRear_CheckedChanged(object sender, EventArgs e)
        {
            accordionControlActuationTypeRear.Visible = true;
            accordionControlAntiRollBarTypeRear.Visible = true;
            
            if (radioButtonDoubleWishboneRear.Checked == true)
            {
                DoubleWishboneRear = 1;
            }
            else if (radioButtonDoubleWishboneRear.Checked == false)
            {
                DoubleWishboneRear = 0;
            }
        }


        private void radioButtonMcPhersonRear_CheckedChanged(object sender, EventArgs e)
        {
            accordionControlActuationTypeRear.Visible = false;
            accordionControlAntiRollBarTypeRear.Visible = false;

            radioButtonPullrodRear.Checked = false;
            radioButtonTARBRear.Checked = false;
            radioButtonPushrodRear.Checked = false;
            radioButtonUbarRear.Checked = false;

            if (radioButtonMcPhersonRear.Checked == true)
            {
                McPhersonRear = 1;
            }
            else if (radioButtonMcPhersonRear.Checked == false)
            {
                McPhersonRear = 0;
            }

        }

        #endregion

        #region Code for GUI and to pass Steering Identifier


        private void radioButton1UV_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1UV.Checked == true)
            {
                NoOfCouplings = 1;
            }
            else if (radioButton1UV.Checked ==false)
            {
                NoOfCouplings = 2;
            }
        }
        private void radioButton2UV_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2UV.Checked == true)
            {
                NoOfCouplings = 2;
            }
            else if (radioButton2UV.Checked == false)
            {
                NoOfCouplings = 1;
            }
        }

        #endregion

        #region Code for GUI and to pass Actuation Identifier

        private void radioButtonPushrodFront_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonUbarFront.Enabled = true;
            radioButtonTARBFront.Enabled = true;

            if (radioButtonPushrodFront.Checked==true)
            {
                PushrodFront = 1;
            }
            else if (radioButtonPushrodFront.Checked==false)
            {
                PushrodFront = 0;
            }
        }

        private void radioButtonPullrodFront_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonUbarFront.Enabled = true;
            radioButtonTARBFront.Enabled = true;

            if (radioButtonPullrodFront.Checked==true)
            {
                PullrodFront = 1;
            }
            else if (radioButtonPullrodFront.Checked ==false )
            {
                PullrodFront = 0;
            }
        }

        private void radioButtonPushrodRear_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonUbarRear.Enabled = true;
            radioButtonTARBRear.Enabled = true;

            if (radioButtonPushrodRear.Checked == true)
            {
                PushrodRear = 1;
            }
            else if (radioButtonPushrodRear.Checked == false)
            {
                PushrodRear = 0;
            }
        }

        private void radioButtonPullrodRear_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonUbarRear.Enabled = true;
            radioButtonTARBRear.Enabled = true;

            if (radioButtonPullrodRear.Checked == true)
            {
                PullrodRear = 1;
            }
            else if (radioButtonPullrodRear.Checked == false)
            {
                PullrodRear = 0;
            }
        }

        #endregion

        #region Code for GUI and to pass Anti-Roll Bar Identifier

        
        private void radioButtonUbarFront_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonUbarFront.Checked==true)
            {
                UARBFront = 1;
            }
            else if (radioButtonUbarFront.Checked==false)
            {
                UARBFront = 0;
            }
        }

        private void radioButtonTARBFront_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTARBFront.Checked == true)
            {
                TARBFront = 1;
            }
            else if (radioButtonTARBFront.Checked == false)
            {
                TARBFront = 0;
            }
        }

        private void radioButtonUbarRear_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonUbarRear.Checked==true)
            {
                UARBRear = 1;
            }
            else if (radioButtonUbarRear.Checked==false)
            {
                UARBRear = 0; 
            }
        }

        private void radioButtonTARBRear_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTARBRear.Checked==true)
            {
                TARBRear = 1;
            }
            else if (radioButtonTARBRear.Checked==false)
            {
                TARBRear = 0;
            }
        }

        #endregion


        #region OK Butto Click Event
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (McPhersonFront == 1)
            {
                PullrodFront = 2; TARBFront = 2;
            }

            if (McPhersonRear == 1)
            {
                PullrodRear = 2; TARBRear = 2;
            }

            if ((DoubleWishboneFront == 0 && McPhersonFront == 0) || (DoubleWishboneRear == 0 && McPhersonRear == 0))
            {
                MessageBox.Show("Please Select Geometry Type");
                return;
            }
            else if ((PushrodFront == 0 && PullrodFront == 0) || (PushrodRear == 0 && PullrodRear == 0))
            {

                MessageBox.Show("Please Select Actuation Type");
                return;
            }
            else if ((UARBFront == 0 && TARBFront == 0) || (UARBRear == 0 && TARBRear == 0))
            {
                MessageBox.Show("Please Select Anti-Roll Bar Type");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Proceed with Selection? ", "Suspension Type", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    FrontSymmetry_CheckedChanged(sender, e);
                    RearSymmetry_CheckedChanged(sender, e);

                    if (OnlyTemplate_DesignMode)
                    {
                        this.Hide();
                        return;
                    }
                    else if (!OnlyTemplate_ImportCAD)
                    {
                        CreatedSuspension_EntireSuspension(R1); 
                    }
                    else
                    {
                        CreateSuspension_OnlyTemplate();
                    }

                    
                    this.Hide();
                    Reset();
                }
                else if (result == DialogResult.No)
                {

                }
            }


        } 
        #endregion

        /// <summary>
        /// Method to define the template of the Suspension AND also create that Suspension
        /// </summary>
        /// <param name="r1"></param>
        private void CreatedSuspension_EntireSuspension(Kinematics_Software_New r1)
        {
            object sender = new object();
            EventArgs e = new EventArgs();

            #region Invoking the Geometry, Actuation and ARB Type definer methods
            R1.GeometryType(DoubleWishboneFront, DoubleWishboneRear, McPhersonFront, McPhersonRear);

            R1.ActuationType(PushrodFront, PullrodFront, PushrodRear, PullrodRear);

            R1.AntiRollBarType(UARBFront, TARBFront, UARBRear, TARBRear);

            R1.NoOfCouplings(NoOfCouplings);
            #endregion

            R1.CurrentSuspensionIsMapped = false;

            #region Creating new Suspension Coordinate items for each corner
            R1.barButtonSCFLItem.PerformClick();
            R1.barButtonItemSCFRItem.PerformClick();
            R1.barButtonItemSCRLItem.PerformClick();
            R1.barButtonItemSCRRItem.PerformClick();
            #endregion

            #region Invoking the CAD Creator
            R1.CreateFrontInputCAD(SuspensionCoordinatesFront.SCFLCounter - 1, false);
            R1.CreateRearInputCAD(SuspensionCoordinatesRear.SCRLCounter - 1, false);
            #endregion

        }
        /// <summary>
        /// Used to create only a Suspension TEMPLATE
        /// </summary>
        public void CreateSuspension_OnlyTemplate()
        {
            object sender = new object();
            EventArgs e = new EventArgs();

            #region Invoking the Geometry, Actuation and ARB Type definer methods
            ///<summary>Assigning the Goemetry Type variables</summary>
            coordinateMap.GeometryType(DoubleWishboneFront, DoubleWishboneRear, McPhersonFront, McPhersonRear);

            ///<summary>Assigning the Actuation Type variables</summary>
            coordinateMap.ActuationType(PushrodFront, PullrodFront, PushrodRear, PullrodRear);

            ///<summary>Assigning the ARB Type variables</summary>
            coordinateMap.AntiRollBarType(UARBFront, TARBFront, UARBRear, TARBRear);

            ///<summary>Assigning the No of Couplings</summary>
            coordinateMap.NoOfCouplingsCount(NoOfCouplings);

            ///<summary>Initialzing the ListBoxes</summary>
            coordinateMap.InitializeListBoxes();
            #endregion
        }

        #region Reset Method
        public void Reset()
        {
            DoubleWishboneFront = 0;
            McPhersonFront = 0;
            DoubleWishboneRear = 0;
            McPhersonRear = 0;

            PushrodFront = 0;
            PullrodFront = 0;
            PushrodRear = 0;
            PullrodRear = 0;

            UARBFront = 0;
            TARBFront = 0;
            UARBRear = 0;
            TARBRear = 0;

        }
        #endregion

        #region Cancel Button Click Event
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {

            Reset();
            this.Hide();
        }
        #endregion

        #region Symmetry Checkbox Events
        
        private void FrontSymmetry_CheckedChanged(object sender, EventArgs e)
        {
            if (FrontSymmetry.Checked == true)
            {
                R1.FrontSymmetry = true;
                FrontSymmetry_Boolean = true;

            }
            else if (FrontSymmetry.Checked == false)
            {
                R1.FrontSymmetry = false;
                FrontSymmetry_Boolean = false;
            }
        }
        
        private void RearSymmetry_CheckedChanged(object sender, EventArgs e)
        {
            if (RearSymmetry.Checked == true)
            {
                R1.RearSymmetry = true;
                RearSymmetry_Boolean = true; 
            }
            else if (RearSymmetry.Checked == false)
            {
                R1.RearSymmetry = false;
                RearSymmetry_Boolean = false;
            }
        }

        #endregion


    }
}
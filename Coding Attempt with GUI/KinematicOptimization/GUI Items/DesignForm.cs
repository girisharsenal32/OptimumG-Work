using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MathNet.Spatial.Units;
using devDept.Geometry;
using devDept.Eyeshot.Entities;

namespace Coding_Attempt_with_GUI
{
    public partial class DesignForm : XtraForm
    {
        /// <summary>
        /// Object of the <see cref="KO_CentralVariables"/> to holds the Input Paramters Central to the Vehicle
        /// </summary>
        KO_CentralVariables KO_Central;

        /// <summary>
        /// Object of hte <see cref="KO_CornverVariables"/> Class to hold the SUspensio parameters of the Front Left Corner
        /// </summary>
        KO_CornverVariables KO_CV_FL;

        /// <summary>
        /// Object of hte <see cref="KO_CornverVariables"/> Class to hold the SUspensio parameters of the Front Right Corner
        /// </summary>
        KO_CornverVariables KO_CV_FR;

        /// <summary>
        /// Object of hte <see cref="KO_CornverVariables"/> Class to hold the SUspensio parameters of the Rear Left Corner
        /// </summary>
        KO_CornverVariables KO_CV_RL;

        /// <summary>
        /// Object of hte <see cref="KO_CornverVariables"/> Class to hold the SUspensio parameters of the Rear Right Corner
        /// </summary>
        KO_CornverVariables KO_CV_RR;


        /// <summary>
        /// Consturctor
        /// </summary>
        public DesignForm()
        {
            InitializeComponent();

            Deactivate_AllTextboxes();

            Deactivate_AllTabPages();

            cad1.CreateControl();

            cad1.viewportLayout1.ZoomFit();

            wishboneInboardFL.Get_ParentObjectData(KO_CV_FL, this);

            wishboneInboardFR.Get_ParentObjectData(KO_CV_FR, this);

            wishboneInboardRL.Get_ParentObjectData(KO_CV_RL, this);

            wishboneInboardRR.Get_ParentObjectData(KO_CV_RR, this);

        }

        /// <summary>
        /// Method to Initilize the <see cref="KO_CentralVariables"/> and <see cref="KO_CornverVariables"/>
        /// </summary>
        /// <param name="_koCentral">Object of the <see cref="KO_CentralVariables"/></param>
        /// <param name="_koCVFL">Front Left object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCVFR">Front Right object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCVRL">Rear Left object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCVRR">Rear Right object of the <see cref="KO_CornverVariables"/></param>
        public void Set_KO_Variables(KO_CentralVariables _koCentral, KO_CornverVariables _koCVFL, KO_CornverVariables _koCVFR, KO_CornverVariables _koCVRL, KO_CornverVariables _koCVRR)
        {
            KO_Central = _koCentral;

            KO_CV_FL = _koCVFL;

            KO_CV_FR = _koCVFR;

            KO_CV_RL = _koCVRL;

            KO_CV_RR = _koCVRR;

            bumpSteerCurveFL.GetParentObjectData(KO_CV_FL);

            bumpSteerCurveFL.GetParentObjectData(KO_CV_FR);
        }



        #region --Suspension Input Extraction Methods--


        #region --Validation Methods--


        /// <summary>
        /// <see cref="String"/> which holds the error message in case of a Negative value is entered in a <see cref="TextBox"/> where it is not accepted
        /// </summary>
        string NegativeError = "Please Enter Positive Values";

        /// <summary>
        /// <see cref="String"/> which holds the error message in case of a Non-Numeric is entered in a <see cref="TextBox"/> accepting only <see cref="Int32"/> or <see cref="Double"/>
        /// </summary>
        string NumericError = "Please Enter Numeric Values";

        /// <summary>
        /// Method to validate the <see cref="double"/> values from the <see cref="TextBox"/>
        /// </summary>
        /// <param name="_textBoxValue">The textbox value to be validatewd</param>
        /// <returns></returns>
        private bool DoubleValidation(string _textBoxValue)
        {
            if (!Double.TryParse(_textBoxValue, out double _result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method to validate <see cref="Int32"/> values from <see cref="TextBox"/>
        /// </summary>
        /// <param name="_texBoxValue">The textbox value to be validated</param>
        /// <returns></returns>
        private bool IntegerValidation(string _texBoxValue)
        {
            if (!Int32.TryParse(_texBoxValue, out int _result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method to evaLuate if a <see cref="double"/> Value is Positve or Negative 
        /// This method is called if a particula <see cref="double"/> value can't be negagive 
        /// </summary>
        /// <param name="_textBoxValue"></param>
        /// <returns></returns>
        private bool Validatepositve_Double(string _textBoxValue)
        {
            if (Convert.ToDouble(_textBoxValue) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region ---Tab Page - Vehicle Parameters - GUI Interaction---
        
        
        #region Wheelbase
        //---Wheelbase Textbox Events

        private void tbWheelbase_Leave(object sender, EventArgs e)
        {
            SetWheelbase();
        }

        private void tbWheelbase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetWheelbase();
            }
        }

        private void SetWheelbase()
        {
            if (DoubleValidation(tbWheelbase.Text))
            {
                if (Validatepositve_Double(tbWheelbase.Text))
                {
                    KO_Central.WheelBase = Convert.ToDouble(tbWheelbase.Text);

                    Set_KO_RollCenter();

                    Plot_Wb();
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region Track Front
        //--Front Track Textbox Events

        private void tbTrackFront_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_TrackFront();
            }
        }

        private void tbTrackFront_Leave(object sender, EventArgs e)
        {
            Set_TrackFront();
        }


        private void Set_TrackFront()
        {
            if (DoubleValidation(tbTrackFront.Text))
            {
                if (Validatepositve_Double(tbTrackFront.Text))
                {
                    KO_Central.Track_Front = Convert.ToDouble(tbTrackFront.Text);

                    Set_KO_PitchCenter();

                    Plot_Tracks();

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region Track Rear
        //---Rear Track Textbox Events--

        private void tbTrackRear_Leave(object sender, EventArgs e)
        {
            Set_TrackRear();
        }

        private void tbTrackRear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_TrackRear();
            }
        }

        private void Set_TrackRear()
        {
            if (DoubleValidation(tbTrackRear.Text))
            {
                if (Validatepositve_Double(tbTrackRear.Text))
                {
                    KO_Central.Track_Rear = Convert.ToDouble(tbTrackRear.Text);

                    Set_KO_PitchCenter();

                    Plot_Tracks();
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region Roll Center Front
        //--Roll Center Front - Height and Lateral Offset

        private void tbRC_Front_Height_Leave(object sender, EventArgs e)
        {
            Set_RC_Front_LatOff();
        }

        private void tbRC_Front_Hight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_RC_Front_LatOff();
            }
        }

        private void Set_RC_Front_LatOff()
        {
            if (DoubleValidation(tbRC_Front_Height.Text))
            {

                KO_Central.RC_Front.Y = Convert.ToDouble(tbRC_Front_Height.Text);

                Set_KO_RollCenter();
                
                Plot_RCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        private void tbRC_Front_LatOff_Leave(object sender, EventArgs e)
        {
            Set_RC_Front_LateralOff();
        }

        private void tbRC_Front_LatOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_RC_Front_LateralOff();
            }
        }

        private void Set_RC_Front_LateralOff()
        {
            if (DoubleValidation(tbRC_Front_LatOff.Text))
            {

                KO_Central.RC_Front.X = Convert.ToDouble(tbRC_Front_LatOff.Text);

                Set_KO_RollCenter();

                Plot_RCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region VSAL FV Front Left
        //--VSAL Front View of Front Left Wheel

        private void tbFV_VSAL_FL_Leave(object sender, EventArgs e)
        {
            Set_FV_VSAL_FL();
        }

        private void tbFV_VSAL_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_FV_VSAL_FL();
            }
        }

        private void Set_FV_VSAL_FL()
        {
            if (DoubleValidation(tbFV_VSAL_FL.Text))
            {

                KO_CV_FL.VSAL_FV = Convert.ToDouble(tbFV_VSAL_FL.Text);

                Plot_VSAL_FV(KO_CV_FL.VCornerParams.FV_IC_Line, KO_CV_FL.VSAL_FV, KO_CV_FL.ContactPatch, KO_Central.RC_Front);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region VSAL FV Front Right
        //--VSAL Front Viw of Front Right


        private void tbFV_VSAL_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_FV_VSAL_FR();
            }
        }

        private void tbFV_VSAL_FR_Leave(object sender, EventArgs e)
        {
            Set_FV_VSAL_FR();
        }

        private void Set_FV_VSAL_FR()
        {
            if (DoubleValidation(tbFV_VSAL_FR.Text))
            {

                KO_CV_FR.VSAL_FV = Convert.ToDouble(tbFV_VSAL_FR.Text);

                Plot_VSAL_FV(KO_CV_FR.VCornerParams.FV_IC_Line, KO_CV_FR.VSAL_FV, KO_CV_FR.ContactPatch, KO_Central.RC_Front);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        } 
        #endregion


        #region Roll Center Rear
        //---Roll Center Rear and Lateral Offset 



        private void tbRC_Rear_Height_Leave(object sender, EventArgs e)
        {
            Set_RC_Rear_Height();
        }

        private void tbRC_Rear_Height_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_RC_Rear_Height();
            }
        }

        private void Set_RC_Rear_Height()
        {
            if (DoubleValidation(tbRC_Rear_Height.Text))
            {
                KO_Central.RC_Rear.Y = Convert.ToDouble(tbRC_Rear_Height.Text);

                Set_KO_RollCenter();
                
                Plot_RCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        private void tbRC_Rear_LatOff_Leave(object sender, EventArgs e)
        {
            Set_RC_Rear_LatOff();
        }

        private void tbRC_Rear_LatOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_RC_Rear_LatOff();
            }
        }

        private void Set_RC_Rear_LatOff()
        {
            if (DoubleValidation(tbRC_Rear_LatOff.Text))
            {
                KO_Central.RC_Rear.X = Convert.ToDouble(tbRC_Rear_LatOff.Text);

                Set_KO_RollCenter();

                Plot_RCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

       
        #region VSAL FV Rear Left
        //---VSAL Front View of Rear Left


        private void tbFV_VSAL_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_FL_VSAL_RL();
            }
        }

        private void tbFV_VSAL_RL_Leave(object sender, EventArgs e)
        {
            Set_FL_VSAL_RL();
        }

        private void Set_FL_VSAL_RL()
        {
            if (DoubleValidation(tbFV_VSAL_RL.Text))
            {

                KO_CV_RL.VSAL_FV = Convert.ToDouble(tbFV_VSAL_RL.Text);

                Plot_VSAL_FV(KO_CV_RL.VCornerParams.FV_IC_Line, KO_CV_RL.VSAL_FV, KO_CV_RL.ContactPatch, KO_Central.RC_Rear);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region VSAL FV Rear Right
        //---VSAL Front View of Rear Right


        private void tbFV_VSAL_RR_Leave(object sender, EventArgs e)
        {
            Set_FV_VSAL_RR();
        }

        private void tbFV_VSAL_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_FV_VSAL_RR();
            }
        }

        private void Set_FV_VSAL_RR()
        {
            if (DoubleValidation(tbFV_VSAL_RR.Text))
            {

                KO_CV_RR.VSAL_FV = Convert.ToDouble(tbFV_VSAL_RR.Text);

                Plot_VSAL_FV(KO_CV_RR.VCornerParams.FV_IC_Line, KO_CV_RR.VSAL_FV, KO_CV_RR.ContactPatch, KO_Central.RC_Rear);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        } 
        #endregion


        #region Pitch Center Left
        //--- Pitch Center Left - Height and Longitudinal Offset


        private void tbPC_Left_Height_Leave(object sender, EventArgs e)
        {
            Set_PC_Left_Height();
        }

        private void tbPC_Left_Height_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_PC_Left_Height();
            }
        }

        private void Set_PC_Left_Height()
        {
            if (DoubleValidation(tbPC_Left_Height.Text))
            {

                KO_Central.PC_Left.Y = Convert.ToDouble(tbPC_Left_Height.Text);

                Set_KO_PitchCenter();

                Plot_PCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        private void tbPC_Left_LongOff_Leave(object sender, EventArgs e)
        {
            Set_PC_Left_LongitudinalOff();
        }

        private void tbPC_Left_LongOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_PC_Left_LongitudinalOff();
            }
        }

        private void Set_PC_Left_LongitudinalOff()
        {
            if (DoubleValidation(tbPC_Left_LongOff.Text))
            {

                KO_Central.PC_Left.Z = Convert.ToDouble(tbPC_Left_LongOff.Text);

                Set_KO_PitchCenter();

                Plot_PCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region VSAL SV Front Left
        //---VSAL Side View of Front Left


        private void tbSV_VSAL_FL_Leave(object sender, EventArgs e)
        {
            Set_SV_VSAL_FL();
        }

        private void tbSV_VSAL_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_SV_VSAL_FL();
            }
        }

        private void Set_SV_VSAL_FL()
        {
            if (DoubleValidation(tbSV_VSAL_FL.Text))
            {

                KO_CV_FL.VSAL_SV = Convert.ToDouble(tbSV_VSAL_FL.Text);

                Plot_VSAL_SV(KO_CV_FL.VCornerParams.SV_IC_Line, KO_CV_FL.VSAL_SV, KO_CV_FL.ContactPatch, KO_Central.PC_Left);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        } 
        #endregion


        #region VSAL SV Front Right
        //---VSAL Side View of Front Right


        private void tbSV_VSAL_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_SV_VSAL_FR();
            }
        }

        private void tbSV_VSAL_FR_Leave(object sender, EventArgs e)
        {
            Set_SV_VSAL_FR();
        }

        private void Set_SV_VSAL_FR()
        {
            if (DoubleValidation(tbSV_VSAL_FR.Text))
            {

                KO_CV_FR.VSAL_SV = Convert.ToDouble(tbSV_VSAL_FR.Text);

                Plot_VSAL_SV(KO_CV_FR.VCornerParams.SV_IC_Line, KO_CV_FR.VSAL_SV, KO_CV_FR.ContactPatch, KO_Central.PC_Right);

            }
            else
            {
                MessageBox.Show(NumericError);
            }

        } 
        #endregion


        #region Pitch Center Right
        //---Pitch Center Right - Height and Longitudinal Offset---



        private void tbPC_Right_Height_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_PC_Right_Height();
            }
        }

        private void tbPC_Right_Height_Leave(object sender, EventArgs e)
        {
            Set_PC_Right_Height();
        }

        private void Set_PC_Right_Height()
        {
            if (DoubleValidation(tbPC_Right_Height.Text))
            {

                KO_Central.PC_Right.Y = Convert.ToDouble(tbPC_Right_Height.Text);

                Set_KO_PitchCenter();

                Plot_PCs();
            }
            else
            {
                MessageBox.Show(NumericError);
            }

        }


        private void tbPC_Right_Height_LongOff_Leave(object sender, EventArgs e)
        {
            Set_PC_Right_LongitudinalOff();
        }

        private void tbPC_Right_Height_LongOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_PC_Right_LongitudinalOff();
            }
        }

        private void Set_PC_Right_LongitudinalOff()
        {
            if (DoubleValidation(tbPC_Right_Height_LongOff.Text))
            {

                KO_Central.PC_Right.Z = Convert.ToDouble(tbPC_Right_Height_LongOff.Text);

                Set_KO_PitchCenter();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region VSAL VS Rear Left
        //---VSAL Side View of Rear Left---



        private void tbSV_VSAL_RL_Leave(object sender, EventArgs e)
        {
            Set_SV_VSAL_RL();
        }

        private void tbSV_VSAL_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_SV_VSAL_RL();
            }
        }

        private void Set_SV_VSAL_RL()
        {
            if (DoubleValidation(tbSV_VSAL_RL.Text))
            {
                if (Validatepositve_Double(tbSV_VSAL_RL.Text))
                {
                    KO_CV_RL.VSAL_SV = Convert.ToDouble(tbSV_VSAL_RL.Text);

                    Plot_VSAL_SV(KO_CV_RL.VCornerParams.SV_IC_Line, KO_CV_RL.VSAL_SV, KO_CV_RL.ContactPatch, KO_Central.PC_Left);
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        } 
        #endregion


        #region VSAL SV Rear Right

        //---VSAL Side View of Rear Right


        private void tbSV_VSAL_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_SV_VSAL_RR();
            }
        }

        private void tbSV_VSAL_RR_Leave(object sender, EventArgs e)
        {
            Set_SV_VSAL_RR();
        }

        private void Set_SV_VSAL_RR()
        {
            if (DoubleValidation(tbSV_VSAL_RR.Text))
            {

                KO_CV_RR.VSAL_SV = Convert.ToDouble(tbSV_VSAL_RR.Text);

                Plot_VSAL_SV(KO_CV_RR.VCornerParams.SV_IC_Line, KO_CV_RR.VSAL_SV, KO_CV_RR.ContactPatch, KO_Central.PC_Right);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        } 
        #endregion


        #region Ackermann
        //---Ackermann---


        private void tbAckermann_Leave(object sender, EventArgs e)
        {
            Set_Ackermann();
        }

        private void tbAckermann_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Ackermann();
            }
        }

        private void Set_Ackermann()
        {
            if (DoubleValidation(tbAckermann.Text))
            {
                if (Validatepositve_Double(tbAckermann.Text))
                {
                    KO_Central.Ackermann = Convert.ToDouble(tbAckermann.Text);
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region Pitman Trail Left

        private void tbPitmanLeft_Leave(object sender, EventArgs e)
        {
            Set_PitmanTrail_Left();
        }

        private void tbPitmanLeft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_PitmanTrail_Left();
            }
        }

        private void Set_PitmanTrail_Left()
        {
            if (DoubleValidation(tbPitmanLeft.Text))
            {

                KO_CV_FL.PitmanTrail = Convert.ToDouble(tbPitmanLeft.Text);

                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    KO_CV_FR.PitmanTrail = Convert.ToDouble(tbPitmanLeft.Text);
                }

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion


        #region Pitman Trail Right

        private void tbPitmanRightLeave(object sender, EventArgs e)
        {
            Set_PitmanTrail_Right();
        }

        private void tbPitmanRight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_PitmanTrail_Right();
            }
        }

        private void Set_PitmanTrail_Right()
        {
            if (DoubleValidation(tbPitmanRight.Text))
            {
                KO_CV_FR.PitmanTrail = Convert.ToDouble(tbPitmanRight.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion


        //---END : Tab Page Vehicle Parameters---
        #endregion

        #region ---Tab Page - Corner Parameters - GUI Interaction---

        //---Corner Parameters Tab Page GUI---

        #region --Front Left--
        //--FRONT LEFT

        #region KPI

        //---KPI---


        private void tbKPI_FL_Leave(object sender, EventArgs e)
        {
            Set_KPI_FL();
        }

        private void tbKPI_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_KPI_FL();
            }
        }

        private void Set_KPI_FL()
        {
            if (DoubleValidation(tbKPI_FL.Text))
            {

                KO_CV_FL.KPI = new Angle(Convert.ToDouble(tbKPI_FL.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch);

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    KO_CV_FR.KPI = new Angle(Convert.ToDouble(tbKPI_FL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);
                }

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region Scrub Radius

        //---Scrub Radius

        private void tbScrub_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Scrub_FL();
            }
        }

        private void tbScrub_FL_Leave(object sender, EventArgs e)
        {
            Set_Scrub_FL();
        }

        private void Set_Scrub_FL()
        {
            if (DoubleValidation(tbScrub_FL.Text))
            {
                if (Validatepositve_Double(tbScrub_FL.Text))
                {
                    KO_CV_FL.ScrubRadius = Convert.ToDouble(tbScrub_FL.Text);

                    Plot_SteeringAxis(KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch);

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.ScrubRadius = Convert.ToDouble(tbScrub_FL.Text);
                        Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);
                    }

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region Caster

        //---Caster---

        private void tbCaster_FL_Leave(object sender, EventArgs e)
        {
            Set_Caster_FL();
        }

        private void tbCaster_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Caster_FL();
            }
        }

        private void Set_Caster_FL()
        {
            if (DoubleValidation(tbCaster_FL.Text))
            {
                KO_CV_FL.Caster = new Angle(Convert.ToDouble(tbCaster_FL.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch);

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    KO_CV_FR.Caster = new Angle(Convert.ToDouble(tbCaster_FL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);
                }

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion
        
        #region Mechanical Trail

        //---Mechanical Trail

        private void tbMechtrail_FL_Leave(object sender, EventArgs e)
        {
            Set_MechTrail_FL();
        }

        private void tbMechtrail_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_MechTrail_FL();
            }
        }

        private void Set_MechTrail_FL()
        {
            if (DoubleValidation(tbMechtrail_FL.Text))
            {
                if (Validatepositve_Double(tbMechtrail_FL.Text))
                {
                    KO_CV_FL.MechTrail = Convert.ToDouble(tbMechtrail_FL.Text);

                    Plot_SteeringAxis(KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch);

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.MechTrail = Convert.ToDouble(tbMechtrail_FL.Text);
                        Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);
                    }

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region UBJ and LBJ - Parametric

        //---LBJ and UBJ---

        private void tbUBJ_FL_Leave(object sender, EventArgs e)
        {
            Set_UBJParametric_FL();
        }

        private void tbUBJ_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_UBJParametric_FL();
            }
        }

        private void Set_UBJParametric_FL()
        {
            if (DoubleValidation(tbUBJ_FL.Text))
            {
                if (Validatepositve_Double(tbUBJ_FL.Text))
                {
                    KO_CV_FL.VCornerParams.UBJ = KO_CV_FL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FL.Text), KO_CV_FL.ContactPatch, KO_CV_FL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FL.VCornerParams.UBJ);

                    Plot_WishbonePlane(KO_CV_FL.VCornerParams.TopWishbonePlane, KO_CV_FL.VCornerParams.UBJ, KO_CV_FL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FL.VCornerParams.SV_IC_Line.EndPoint);

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();

                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.VCornerParams.UBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FL.Text), KO_CV_FR.ContactPatch, KO_CV_FR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_FR.VCornerParams.UBJ);

                        KO_CV_FR.VCornerParams.Initialize_Dictionary();

                    }
                    else
                    {
                        ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                        Plot_WishbonePlane(KO_CV_FR.VCornerParams.TopWishbonePlane, KO_CV_FR.VCornerParams.UBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint);
                    }

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



        private void tbLBJ_FL_Leave(object sender, EventArgs e)
        {
            Set_LBJParametric_FL();
        }

        private void tbLBJ_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_LBJParametric_FL();
            }
        }

        private void Set_LBJParametric_FL()
        {
            if (DoubleValidation(tbLBJ_FL.Text))
            {
                if (Validatepositve_Double(tbLBJ_FL.Text))
                {
                    KO_CV_FL.VCornerParams.LBJ = KO_CV_FL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_FL.Text), KO_CV_FL.ContactPatch, KO_CV_FL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FL.VCornerParams.LBJ);

                    Plot_WishbonePlane(KO_CV_FL.VCornerParams.BottomWishbonePlane, KO_CV_FL.VCornerParams.LBJ, KO_CV_FL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FL.VCornerParams.SV_IC_Line.EndPoint);

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.VCornerParams.LBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FL.Text), KO_CV_FR.ContactPatch, KO_CV_FR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_FR.VCornerParams.LBJ);

                        KO_CV_FR.VCornerParams.Initialize_Dictionary();

                    }
                    else
                    {
                        ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                        Plot_WishbonePlane(KO_CV_FR.VCornerParams.BottomWishbonePlane, KO_CV_FR.VCornerParams.LBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint);
                    }
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #region Wheel Center - Paramettric

        private void tbWC_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_WheelCenter_FL();
            }
        }

        private void tbWC_FL_Leave(object sender, EventArgs e)
        {
            Set_WheelCenter_FL();
        }

        private void Set_WheelCenter_FL()
        {
            if (DoubleValidation(tbWC_FL.Text))
            {
                if (Validatepositve_Double(tbWC_FL.Text))
                {
                    KO_CV_FL.VCornerParams.WheelCenter = KO_CV_FL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_FL.Text), KO_CV_FL.ContactPatch, KO_CV_FL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FL.VCornerParams.WheelCenter);

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.VCornerParams.WheelCenter = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_FL.Text), KO_CV_FR.ContactPatch, KO_CV_FR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_FR.VCornerParams.WheelCenter);

                        KO_CV_FR.VCornerParams.Initialize_Dictionary();

                    }

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #endregion

        #region --Front Right--
        //--FRONT RIGHT--



        #region KPI
        private void tbKPI_FR_Leave(object sender, EventArgs e)
        {
            Set_KPI_FR();
        }

        private void tbKPI_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_KPI_FR();
            }
        }

        private void Set_KPI_FR()
        {
            if (DoubleValidation(tbKPI_FR.Text))
            {

                KO_CV_FR.KPI = new Angle(Convert.ToDouble(tbKPI_FR.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region Scrub Radius
        private void tbScrub_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Scrub_FR();
            }
        }

        private void tbScrub_FR_Leave(object sender, EventArgs e)
        {
            Set_Scrub_FR();
        }

        private void Set_Scrub_FR()
        {
            if (DoubleValidation(tbScrub_FR.Text))
            {
                if (Validatepositve_Double(tbScrub_FR.Text))
                {
                    KO_CV_FR.ScrubRadius = Convert.ToDouble(tbScrub_FR.Text);

                    Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #region Caster
        private void tbCaster_FR_Leave(object sender, EventArgs e)
        {
            Set_Caster_FR();
        }

        private void tbCaster_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Caster_FR();
            }
        }

        private void Set_Caster_FR()
        {
            if (DoubleValidation(tbCaster_FR.Text))
            {
                KO_CV_FR.Caster = new Angle(Convert.ToDouble(tbCaster_FR.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #region Mechanical Trail
        private void tbMechtrail_FR_Leave(object sender, EventArgs e)
        {
            Set_MechTrail_FR();
        }

        private void tbMechtrail_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_MechTrail_FR();
            }
        }

        private void Set_MechTrail_FR()
        {
            if (DoubleValidation(tbMechtrail_FR.Text))
            {
                if (Validatepositve_Double(tbMechtrail_FR.Text))
                {
                    KO_CV_FR.MechTrail = Convert.ToDouble(tbMechtrail_FR.Text);

                    Plot_SteeringAxis(KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch);

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #region UBJ and LBJ from parametric

        private void tbUBJ_FR_Leave(object sender, EventArgs e)
        {
            Set_UBJParametric_FR();
        }


        private void tbUBJ_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_UBJParametric_FR();
            }
        }

        private void Set_UBJParametric_FR()
        {
            if (DoubleValidation(tbUBJ_FR.Text))
            {
                if (Validatepositve_Double(tbUBJ_FR.Text))
                {
                    KO_CV_FR.VCornerParams.UBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FR.Text), KO_CV_FR.ContactPatch, KO_CV_FR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.UBJ);

                    Plot_WishbonePlane(KO_CV_FR.VCornerParams.TopWishbonePlane, KO_CV_FR.VCornerParams.UBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint);


                    KO_CV_FR.VCornerParams.Initialize_Dictionary();

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



        private void tbLBJ_FR_Leave(object sender, EventArgs e)
        {
            Set_LBJParametric_FR();
        }

        private void tbLBJ_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_LBJParametric_FR();
            }
        }

        private void Set_LBJParametric_FR()
        {
            if (DoubleValidation(tbLBJ_FR.Text))
            {
                if (Validatepositve_Double(tbLBJ_FR.Text))
                {
                    KO_CV_FR.VCornerParams.LBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_FR.Text), KO_CV_FR.ContactPatch, KO_CV_FR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.LBJ);

                    Plot_WishbonePlane(KO_CV_FR.VCornerParams.BottomWishbonePlane, KO_CV_FR.VCornerParams.LBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint);

                    KO_CV_FR.VCornerParams.Initialize_Dictionary();


                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #region Wheel Center - Parametric

        private void tbWC_FR_Leave(object sender, EventArgs e)
        {
            Set_WheelCenter_FR();
        }

        private void tbWC_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_WheelCenter_FR();
            }
        }

        private void Set_WheelCenter_FR()
        {
            if (DoubleValidation(tbWC_FR.Text))
            {
                if (Validatepositve_Double(tbWC_FR.Text))
                {

                    KO_CV_FR.VCornerParams.WheelCenter = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_FR.Text), KO_CV_FR.ContactPatch, KO_CV_FR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.WheelCenter);

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        #endregion

        #endregion

        #region --Rear Left--
        //--REAR LEFT--

        #region KPI
        private void tbKPI_RL_Leave(object sender, EventArgs e)
        {
            Set_KPI_RL();
        }

        private void tbKPI_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_KPI_RL();
            }
        }

        private void Set_KPI_RL()
        {
            if (DoubleValidation(tbKPI_RL.Text))
            {

                KO_CV_RL.KPI = new Angle(Convert.ToDouble(tbKPI_RL.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch);

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    KO_CV_RR.KPI = new Angle(Convert.ToDouble(tbKPI_RL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);
                }

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion
        
        #region Scrub Radius
        private void tbScrub_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Scrub_RL();
            }
        }

        private void tbScrub_RL_Leave(object sender, EventArgs e)
        {
            Set_Scrub_RL();
        }

        private void Set_Scrub_RL()
        {
            if (DoubleValidation(tbScrub_RL.Text))
            {
                if (Validatepositve_Double(tbScrub_RL.Text))
                {
                    KO_CV_RL.ScrubRadius = Convert.ToDouble(tbScrub_RL.Text);

                    Plot_SteeringAxis(KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch);

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.ScrubRadius = Convert.ToDouble(tbScrub_RL.Text);
                        Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);
                    }


                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion
        
        #region Caster
        private void tbCaster_RL_Leave(object sender, EventArgs e)
        {
            Set_Caster_RL();
        }

        private void tbCaster_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Caster_RL();
            }
        }

        private void Set_Caster_RL()
        {
            if (DoubleValidation(tbCaster_RL.Text))
            {
                KO_CV_RL.Caster = new Angle(Convert.ToDouble(tbCaster_RL.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch);

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    KO_CV_RR.Caster = new Angle(Convert.ToDouble(tbCaster_RL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);
                }

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion
        
        #region Mechanical Trail
        private void tbMechtrail_RL_Leave(object sender, EventArgs e)
        {
            Set_MechTrail_RL();
        }

        private void tbMechtrail_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_MechTrail_RL();
            }
        }

        private void Set_MechTrail_RL()
        {
            if (DoubleValidation(tbMechtrail_RL.Text))
            {
                if (Validatepositve_Double(tbMechtrail_RL.Text))
                {
                    KO_CV_RL.MechTrail = Convert.ToDouble(tbMechtrail_RL.Text);

                    Plot_SteeringAxis(KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch);

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.MechTrail = Convert.ToDouble(tbMechtrail_RL.Text);
                        Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);
                    }


                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region UBJ and LBJ Parametric 

        private void tbUBJ_RL_Leave(object sender, EventArgs e)
        {
            Set_UBJParametric_RL();
        }

        private void tbUBJ_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_UBJParametric_RL();
            }
        }

        private void Set_UBJParametric_RL()
        {
            if (DoubleValidation(tbUBJ_RL.Text))
            {
                if (Validatepositve_Double(tbUBJ_RL.Text))
                {
                    KO_CV_RL.VCornerParams.UBJ = KO_CV_RL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_RL.Text), KO_CV_RL.ContactPatch, KO_CV_RL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RL.VCornerParams.UBJ);

                    Plot_WishbonePlane(KO_CV_RL.VCornerParams.TopWishbonePlane, KO_CV_RL.VCornerParams.UBJ, KO_CV_RL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RL.VCornerParams.SV_IC_Line.EndPoint);

                    KO_CV_RL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.RearSymmetry_Boolean)
                    {
                        KO_CV_RR.VCornerParams.UBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_RL.Text), KO_CV_RR.ContactPatch, KO_CV_RR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_RR.VCornerParams.UBJ);

                        KO_CV_RR.VCornerParams.Initialize_Dictionary();


                    }
                    else
                    {
                        ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                        Plot_WishbonePlane(KO_CV_RR.VCornerParams.TopWishbonePlane, KO_CV_RR.VCornerParams.UBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint);
                    }
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



        private void tbLBJ_RL_Leave(object sender, EventArgs e)
        {
            Set_LBJParametric_RL();
        }

        private void tbLBJ_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_LBJParametric_RL();
            }
        }

        private void Set_LBJParametric_RL()
        {
            if (DoubleValidation(tbLBJ_RL.Text))
            {
                if (Validatepositve_Double(tbLBJ_RL.Text))
                {
                    KO_CV_RL.VCornerParams.LBJ = KO_CV_RL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_RL.Text), KO_CV_RL.ContactPatch, KO_CV_RL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RL.VCornerParams.LBJ);

                    Plot_WishbonePlane(KO_CV_RL.VCornerParams.BottomWishbonePlane, KO_CV_RL.VCornerParams.LBJ, KO_CV_RL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RL.VCornerParams.SV_IC_Line.EndPoint);


                    KO_CV_RL .VCornerParams.Initialize_Dictionary();

                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.VCornerParams.LBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_RL.Text), KO_CV_RR.ContactPatch, KO_CV_RR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_RR.VCornerParams.LBJ);

                        KO_CV_RR.VCornerParams.Initialize_Dictionary();

                    }
                    else
                    {
                        ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                        Plot_WishbonePlane(KO_CV_RR.VCornerParams.BottomWishbonePlane, KO_CV_RR.VCornerParams.LBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint);
                    }
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        #region Wheel Center - Parametric

        private void tbWC_RL_Leave(object sender, EventArgs e)
        {
            Set_WheelCenter_RL();
        }

        private void tbWC_RL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_WheelCenter_RL();
            }
        }

        private void Set_WheelCenter_RL()
        {
            if (DoubleValidation(tbWC_RL.Text))
            {
                if (Validatepositve_Double(tbWC_RL.Text))
                {
                    KO_CV_RL.VCornerParams.WheelCenter = KO_CV_RL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_RL.Text), KO_CV_RL.ContactPatch, KO_CV_RL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RL.VCornerParams.WheelCenter);

                    KO_CV_RL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.VCornerParams.WheelCenter = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_RL.Text), KO_CV_RR.ContactPatch, KO_CV_RR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_RR.VCornerParams.WheelCenter);

                        KO_CV_RR.VCornerParams.Initialize_Dictionary();

                    }
                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        #endregion

        #endregion

        #region --Rear Right--
        //--REAR RIGHT--

        #region KPI
        private void tbKPI_RR_Leave(object sender, EventArgs e)
        {
            Set_KPI_RR();
        }

        private void tbKPI_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_KPI_RR();
            }
        }

        private void Set_KPI_RR()
        {
            if (DoubleValidation(tbKPI_RR.Text))
            {

                KO_CV_RR.KPI = new Angle(Convert.ToDouble(tbKPI_RR.Text), AngleUnit.Degrees);
                Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion
        
        #region Scrub Radius
        private void tbScrub_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Scrub_RR();
            }
        }

        private void tbScrub_RR_Leave(object sender, EventArgs e)
        {
            Set_Scrub_RR();
        }

        private void Set_Scrub_RR()
        {
            if (DoubleValidation(tbScrub_RR.Text))
            {
                if (Validatepositve_Double(tbScrub_RR.Text))
                {
                    KO_CV_RR.ScrubRadius = Convert.ToDouble(tbScrub_RR.Text);
                    Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region Caster
        private void tbCaster_RR_Leave(object sender, EventArgs e)
        {
            Set_Caster_RR();
        }

        private void tbCaster_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Caster_RR();
            }
        }

        private void Set_Caster_RR()
        {
            if (DoubleValidation(tbCaster_RR.Text))
            {
                KO_CV_RR.Caster = new Angle(Convert.ToDouble(tbCaster_RR.Text), AngleUnit.Degrees);
                Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region Mechanical Trail
        private void tbMechtrail_RR_Leave(object sender, EventArgs e)
        {
            Set_MechTrail_RR();
        }

        private void tbMechtrail_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_MechTrail_RR();
            }
        }

        private void Set_MechTrail_RR()
        {
            if (DoubleValidation(tbMechtrail_RR.Text))
            {
                if (Validatepositve_Double(tbMechtrail_RR.Text))
                {
                    KO_CV_RR.MechTrail = Convert.ToDouble(tbMechtrail_RR.Text);
                    Plot_SteeringAxis(KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch);

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region UBj and LBJ Parametric


        private void tbUBJ_RR_Leave(object sender, EventArgs e)
        {
            Set_UBJParametric_RR();
        }

        private void tbUBJ_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_UBJParametric_RR();
            }
        }

        private void Set_UBJParametric_RR()
        {
            if (DoubleValidation(tbUBJ_RR.Text))
            {
                if (Validatepositve_Double(tbUBJ_RR.Text))
                {
                    KO_CV_RR.VCornerParams.UBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_RR.Text), KO_CV_RR.ContactPatch, KO_CV_RR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RR.VCornerParams.UBJ);

                    Plot_WishbonePlane(KO_CV_RR.VCornerParams.TopWishbonePlane, KO_CV_RR.VCornerParams.UBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint);
                    KO_CV_RR.VCornerParams.Initialize_Dictionary();


                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



        private void tbLBJ_RR_Leave(object sender, EventArgs e)
        {
            Set_LBJParametric_RR();
        }

        private void tbLBJ_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_LBJParametric_RR();
            }
        }

        private void Set_LBJParametric_RR()
        {
            if (DoubleValidation(tbLBJ_RR.Text))
            {
                if (Validatepositve_Double(tbLBJ_RR.Text))
                {
                    KO_CV_RR.VCornerParams.LBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_RR.Text), KO_CV_RR.ContactPatch, KO_CV_RR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RR.VCornerParams.LBJ);

                    Plot_WishbonePlane(KO_CV_RR.VCornerParams.BottomWishbonePlane, KO_CV_RR.VCornerParams.LBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint);
                    KO_CV_RR.VCornerParams.Initialize_Dictionary();


                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        #endregion

        #region Wheel Center - Parametric

        private void tbWC_RR_Leave(object sender, EventArgs e)
        {
            Set_WheelCenter_RR();
        }

        private void tbWC_RR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_WheelCenter_RR();
            }
        }

        private void Set_WheelCenter_RR()
        {
            if (DoubleValidation(tbWC_RR.Text))
            {
                if (Validatepositve_Double(tbWC_RR.Text))
                {
                    KO_CV_RR.VCornerParams.WheelCenter = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_RR.Text), KO_CV_RR.ContactPatch, KO_CV_RR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RR.VCornerParams.WheelCenter);

                    KO_CV_RR.VCornerParams.Initialize_Dictionary();

                }
                else
                {
                    MessageBox.Show(NegativeError);
                }
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        //--Rear Right EEN
        #endregion

        //---END : TAB PAGE - Conrer params---
        #endregion

        //---END : Suspension Input Extraction Methods---
        #endregion


        #region --CAD UserControl Plotter Methods--

        #region -Vehicle Parameters-
        /// <summary>
        /// Plotting the Wheelbase
        /// </summary>
        private void Plot_Wb()
        {
            cad1.Init_Wheelbase(KO_Central.WheelBase);
            //cad1.Plot_AllSuspensionParams();
        }

        /// <summary>
        /// Plotting the Track Widths
        /// </summary>
        private void Plot_Tracks()
        {
            cad1.Init_Track(KO_Central.Track_Front, KO_Central.Track_Rear);
            //cad1.Plot_AllSuspensionParams();

        }

        /// <summary>
        /// Method to Plot the Roll Centers
        /// </summary>
        private void Plot_RCs()
        {
            cad1.Init_RollCenters(KO_Central.RC_Front, KO_Central.RC_Rear);
            //cad1.Plot_AllSuspensionParams();

        }

        /// <summary>
        /// Method to Plot the Pitch Centers
        /// </summary>
        private void Plot_PCs()
        {
            cad1.Init_PitchCenters(KO_Central.PC_Left, KO_Central.PC_Right);
            //cad1.Plot_AllSuspensionParams();

        }

        /// <summary>
        /// Method to Plot the VSAL of the FV
        /// </summary>
        /// <param name="_VSAL_FV_Length">VSAL Length in Front View</param>
        /// <param name="_ConctactPatch"></param>
        /// <param name="_RC"></param>
        private void Plot_VSAL_FV(Line _FV_IC_Line, double _VSAL_FV_Length, Point3D _ConctactPatch, Point3D _RC)
        {
            cad1.Init_VSAL_FV(_FV_IC_Line, _VSAL_FV_Length, _ConctactPatch, _RC);
        }

        /// <summary>
        /// Method to plot the VSAL of the SV
        /// </summary>
        /// <param name="_VSAL_SV_Length">VSAL Length in Side View</param>
        /// <param name="_ConctactPatch"></param>
        /// <param name="_PC"></param>
        private void Plot_VSAL_SV(Line _SV_IC_Line, double _VSAL_SV_Length, Point3D _ConctactPatch, Point3D _PC)
        {
            cad1.Init_VSAL_SV(_SV_IC_Line, _VSAL_SV_Length, _ConctactPatch, _PC);
        }
        #endregion

        #region -Corner Params-

        /// <summary>
        /// Method to Initialize and Plot the Steering Axis 
        /// </summary>
        /// <param name="_SteeringAxis">Steering Axis</param>
        /// <param name="_KPI">KPI Angle</param>
        /// <param name="_Caster">Caster Angle</param>
        /// <param name="_ScrubRadius">Scrub Radius</param>
        /// <param name="_MechTrail">Mechanical Trail</param>
        /// <param name="_ContactPatch">Contact Patch</param>
        private void Plot_SteeringAxis(Line _SteeringAxis, Angle _KPI, Angle _Caster, double _ScrubRadius, double _MechTrail, Point3D _ContactPatch)
        {
            cad1.Plot_SteeringAxis(_SteeringAxis, _KPI, _Caster, _ScrubRadius, _MechTrail, _ContactPatch);
        }

        /// <summary>
        /// Method to Plot th Outboard point
        /// </summary>
        /// <param name="_outboardPoint"></param>
        private void Plot_OutboardPoint(Point3D _outboardPoint)
        {
            cad1.Plot_OutboardPoint(_outboardPoint);
        }

        /// <summary>
        /// Method to plot the Plane of the pair of wishbones being considered
        /// </summary>
        /// <param name="_WishbonePlane">Plane of the pair of wishbones being considered</param>
        /// <param name="_Point1">First point making up the plane</param>
        /// <param name="_Point2">Second point making up the plane</param>
        /// <param name="_Point3">Third point making up the plane</param>
        private void Plot_WishbonePlane(Plane _WishbonePlane, Point3D _Point1, Point3D _Point2, Point3D _Point3)
        {
            cad1.Plot_WishbonePlane(_WishbonePlane, _Point1, _Point2, _Point3);
        }

        /// <summary>
        /// Method to Plot the Inboard Point and create a Bar using it's corresponding Outboard Point
        /// </summary>
        /// <param name="_InboardPoint">Inboard Point</param>
        /// <param name="_OutboardPoint">Corresponding Outboard Point</param>
        public void Plot_InboardPoints(Point3D _InboardPoint, Point3D _OutboardPoint)
        {
            cad1.Plot_InboardWishbonePoint(_InboardPoint, _OutboardPoint);
        }

        
        #endregion

        #endregion


        #region ---Initialize Methods---

        /// <summary>
        /// Common Method to initialize the <see cref="KO_CornverVariables.ContactPatch"/> points
        /// </summary>
        private void Set_ContactPatch()
        {
            ///<summary>Using the Track Width to Set the Front Contact Patch</summary>
            KO_CV_FL.ContactPatch.X = KO_Central.Track_Front / 2;

            KO_CV_FR.ContactPatch.X = -KO_Central.Track_Front / 2;

            ///<summary>Using the Rear Track to Set tthe Rear COntact Patch</summary>
            KO_CV_RL.ContactPatch.X = KO_Central.Track_Rear / 2;

            KO_CV_RR.ContactPatch.X = -KO_Central.Track_Rear / 2;

            ///<summary>Uisng the Wheelbase to Set the RearC Contact Patch</summary>
            KO_CV_RL.ContactPatch.Z = KO_Central.WheelBase;

            KO_CV_RR.ContactPatch.Z = KO_Central.WheelBase;

        }

        /// <summary>
        /// Method to set the Front and Rear Roll Center
        /// </summary>
        private void Set_KO_RollCenter()
        {
            KO_Central.Initialize_RC_Front(KO_Central.RC_Front.Y, KO_Central.RC_Front.X);

            KO_Central.Initialize_RC_Rear(KO_Central.RC_Rear.Y, KO_Central.RC_Rear.X, KO_Central.WheelBase);
        }

        /// <summary>
        /// Method to Set the Left and Right Pitch Center (based on Symmetry)
        /// </summary>
        private void Set_KO_PitchCenter()
        {
            if (Sus_Type.FrontSymmetry_Boolean && Sus_Type.RearSymmetry_Boolean)
            {
                KO_Central.Initialize_PC_Symmetric(KO_Central.PC_Left.Y, KO_Central.PC_Left.Z, KO_Central.Track_Front, KO_Central.Track_Rear);
            }
            else
            {
                KO_Central.Initialize_PC_Left(KO_Central.PC_Left.Y, KO_Central.PC_Left.Z, KO_Central.Track_Front, KO_Central.Track_Rear);

                KO_Central.Initialize_PC_Right(KO_Central.PC_Right.Y, KO_Central.PC_Right.Z, KO_Central.Track_Front, KO_Central.Track_Rear);
            }
        }

        
        /// <summary>
        /// Method to compute the Outboard Toe Link Point once the ackermann and pitmann trails have been set 
        /// </summary>
        private void Set_OutboardToeLink()
        {
            KO_Central.Compute_OutboardToeLink(KO_CV_FL, KO_CV_FR, KO_CV_RL, KO_CV_RR);
        }

        #endregion


        

        private void simpleButtonSuspensiontemplate_Click(object sender, EventArgs e)
        {
            CreateSuspensionTemplate();

            Activate_AllTextboxes();

            Activate_AllTabPages();

            if (Sus_Type.FrontSymmetry_Boolean && Sus_Type.RearSymmetry_Boolean)
            {
                SymmtryOperations();
            }
            else
            {
                AssymmetryOperations();
            }

        }

        SuspensionType Sus_Type;

        private void CreateSuspensionTemplate()
        {
            Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();

            Sus_Type = new SuspensionType(R1);

            Sus_Type.Show();

            if (Sus_Type.FrontSymmetry_Boolean)
            {
                wishboneInboardFL.Get_ParentObjectData(KO_CV_FL, KO_CV_FR, this);
            }
            if (Sus_Type.RearSymmetry_Boolean)
            {
                wishboneInboardRL.Get_ParentObjectData(KO_CV_RL, KO_CV_RR, this);
            }



        }


        #region ---GUI Operations---
        /// <summary>
        /// Method to Activate all the <see cref="TextBox"/>s of <see cref="xtraTabPageVehicleParams"/>
        /// </summary>
        private void Activate_AllTextboxes()
        {
            ///<summary>Activating WheelBase</summary>
            tbWheelbase.Enabled = true;

            ///<summary>Activating WheelBase</summary>
            tbTrackFront.Enabled = true;
            tbTrackRear.Enabled = true;

            ///<summary>Activating Roll Center Front and FV VSALS</summary>
            tbRC_Front_Height.Enabled = true;
            tbRC_Front_LatOff.Enabled = true;
            tbFV_VSAL_FL.Enabled = true;
            tbFV_VSAL_FR.Enabled = true;

            ///<summary>Activating Roll Center Rear and FV VSALS</summary>
            tbRC_Rear_Height.Enabled = true;
            tbRC_Rear_LatOff.Enabled = true;
            tbFV_VSAL_RL.Enabled = true;
            tbFV_VSAL_RR.Enabled = true;

            ///<summary>Activating Pitch Center Left and SV VSALS</summary
            tbPC_Left_Height.Enabled = true;
            tbPC_Left_LongOff.Enabled = true;
            tbSV_VSAL_FL.Enabled = true;
            tbSV_VSAL_FR.Enabled = true;


            ///<summary>Activating Pitch Center Right and SV VSALS</summary
            tbPC_Right_Height.Enabled = true;
            tbPC_Right_Height_LongOff.Enabled = true;
            tbSV_VSAL_RL.Enabled = true;
            tbSV_VSAL_RR.Enabled = true;


            ///<summary>Activating Ackermann</summary>
            tbAckermann.Enabled = true;

            ///<summary>Activating Pitman Trail</summary>
            tbPitmanLeft.Enabled = true;
            tbPitmanRight.Enabled = true;

        }

        /// <summary>
        /// Method to De-Activate all the <see cref="TextBox"/>s of <see cref="xtraTabPageVehicleParams"/>
        /// </summary>
        private void Deactivate_AllTextboxes()
        {
            ///<summary>De-activating WheelBase</summary>
            tbWheelbase.Enabled = false;

            ///<summary>De-activating WheelBase</summary>
            tbTrackFront.Enabled = false;
            tbTrackRear.Enabled = false;

            ///<summary>De-activating Roll Center Front and FV VSALS</summary>
            tbRC_Front_Height.Enabled = false;
            tbRC_Front_LatOff.Enabled = false;
            tbFV_VSAL_FL.Enabled = false;
            tbFV_VSAL_FR.Enabled = false;

            ///<summary>De-activating Roll Center Rear and FV VSALS</summary>
            tbRC_Rear_Height.Enabled = false;
            tbRC_Rear_LatOff.Enabled = false;
            tbFV_VSAL_RL.Enabled = false;
            tbFV_VSAL_RR.Enabled = false;

            ///<summary>De-activating Pitch Center Left and SV VSALS</summary
            tbPC_Left_Height.Enabled = false;
            tbPC_Left_LongOff.Enabled = false;
            tbSV_VSAL_FL.Enabled = false;
            tbSV_VSAL_FR.Enabled = false;


            ///<summary>De-activating Pitch Center Right and SV VSALS</summary
            tbPC_Right_Height.Enabled = false;
            tbPC_Right_Height_LongOff.Enabled = false;
            tbSV_VSAL_RL.Enabled = false;
            tbSV_VSAL_RR.Enabled = false;


            ///<summary>De-activating Ackermann</summary>
            tbAckermann.Enabled = false;

            ///<summary>De-activating Pitman Trail</summary>
            tbPitmanLeft.Enabled = false;
            tbPitmanRight.Enabled = false;

        }

        /// <summary>
        /// Method to Activate all the TabPages
        /// </summary>
        private void Activate_AllTabPages()
        {
            xtraTabPageCornerParams.PageEnabled = true;

            xtraTabPageInboardPoints.PageEnabled = true;

            xtraTabPageBumpSteer.PageEnabled = true;
        }

        /// <summary>
        /// Method to De-Activate all the TabPages
        /// </summary>
        private void Deactivate_AllTabPages()
        {
            xtraTabPageCornerParams.PageEnabled = false;

            xtraTabPageInboardPoints.PageEnabled = false;

            xtraTabPageBumpSteer.PageEnabled = false;
        }

        /// <summary>
        /// Method to perform the GUI Operations of Hide incase of Symmetric Suspension
        /// </summary>
        /// <param name="_frontSymmetri"></param>
        /// <param name="_rearSymmetric"></param>
        private void SymmtryOperations()
        {
            layoutControlItemFV_VSAL_FR.HideToCustomization();

            layoutControlItemFV_VSAL_RR.HideToCustomization();

            layoutControlItemPC_Right_Height.HideToCustomization();

            layoutControlItemPC_Right_LongOff.HideToCustomization();

            layoutControlItemSV_VSAL_FR.HideToCustomization();

            layoutControlItemSV_VSAL_RR.HideToCustomization();

            layoutControlItemPitman_Right.HideToCustomization();

            layoutControl_CornerParams_FR.Hide();

            layoutControl_CornerParams_RR.Hide();

            wishboneInboardFR.Hide();

            wishboneInboardRR.Hide();

            bumpSteerCurveFR.Hide();

        }

        /// <summary>
        /// Method to perform the GUI operations of Show in case of Assymetric Suspension
        /// </summary>
        private void AssymmetryOperations()
        {
            layoutControlItemFV_VSAL_FR.ShowInCustomizationForm = true;

            layoutControlItemFV_VSAL_RR.ShowInCustomizationForm = true;

            layoutControlItemPC_Right_Height.ShowInCustomizationForm = true;

            layoutControlItemPC_Right_LongOff.ShowInCustomizationForm = true;

            layoutControlItemSV_VSAL_FR.ShowInCustomizationForm = true;

            layoutControlItemSV_VSAL_RR.ShowInCustomizationForm = true;

            layoutControlItemPitman_Right.ShowInCustomizationForm = true;

            layoutControl_CornerParams_FR.Show();

            layoutControl_CornerParams_RR.Show();

            wishboneInboardFR.Show();

            wishboneInboardRR.Show();

            bumpSteerCurveFR.Show();
        }


        
        #endregion

        private void simpleButtonUpdateSuspension_Click(object sender, EventArgs e)
        {

        }






    }
}
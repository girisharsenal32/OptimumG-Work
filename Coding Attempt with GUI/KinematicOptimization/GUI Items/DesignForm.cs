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


namespace Coding_Attempt_with_GUI
{
    public partial class DesignForm : DevExpress.XtraEditors.XtraForm
    {
        KO_CentralVariables KO_Central;

        KO_CornverVariables KO_CV_FL;

        KO_CornverVariables KO_CV_FR;

        KO_CornverVariables KO_CV_RL;

        KO_CornverVariables KO_CV_RR;

        public DesignForm()
        {
            InitializeComponent();
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



        string NegativeError = "Please Enter Positive Values";

        string NumericError = "Please Enter Numeric Values";

        #region --Validation Methods--
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

        #region ---Tab Page - Vehicle Parameters---
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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }




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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }




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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



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
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        //--- Pitch Center Left


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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        
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

            }
            else
            {
                MessageBox.Show(NumericError);
            }

        }

        

        //---Pitch Center Right---



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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



        //---Ackermann


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

        #region ---Tab Page - Corner Parameters---


        //---Corner Parameters Tab Page GUI---

        #region --Front Left--
        //--FRONT LEFT

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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






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
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



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
                    KO_CV_FL.CasterTrail = Convert.ToDouble(tbMechtrail_FL.Text);
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

        #region --Front Right--
        //--FRONT RIGHT--



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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






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
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



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
                    KO_CV_FR.CasterTrail = Convert.ToDouble(tbMechtrail_FR.Text);
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

        #region --Rear Left--
        //--REAR LEFT--

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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






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
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



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
                    KO_CV_RL.CasterTrail = Convert.ToDouble(tbMechtrail_RL.Text);
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

        #region --Rear Right--
        //--REAR RIGHT--

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

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






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
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }



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
                    KO_CV_RR.CasterTrail = Convert.ToDouble(tbMechtrail_RR.Text);
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






    }
}
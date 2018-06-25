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
        /// Object of the< <see cref="KO_SimulationParams"/> which stores all the information regarding the Simulation Parameters such as Number of iterations etc
        /// </summary>
        KO_SimulationParams KO_SimParams;

        /// <summary>
        /// Object of the <see cref="CAD"/> user control which is going to be docked on teh <see cref="groupControlCAD"/>
        /// </summary>
        CAD cad1;

        #region ---Initialization Method---
        /// <summary>
        /// Consturctor
        /// </summary>
        public DesignForm()
        {
            InitializeComponent();

            Deactivate_AllTextboxes();

            Deactivate_AllTabPages();

            cad1 = new CAD(true);
            cad1.Dock = DockStyle.Fill;

            groupControlCAD.Controls.Add(cad1);

            cad1.viewportLayout1.ZoomFit();



        }

        /// <summary>
        /// Method to Initilize the <see cref="KO_CentralVariables"/> and <see cref="KO_CornverVariables"/>
        /// </summary>
        /// <param name="_koCentral">Object of the <see cref="KO_CentralVariables"/></param>
        /// <param name="_koCVFL">Front Left object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCVFR">Front Right object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCVRL">Rear Left object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCVRR">Rear Right object of the <see cref="KO_CornverVariables"/></param>
        public void Set_KO_Variables(ref KO_CentralVariables _koCentral, ref KO_CornverVariables _koCVFL, ref KO_CornverVariables _koCVFR, ref KO_CornverVariables _koCVRL, ref KO_CornverVariables _koCVRR)
        {
            KO_Central = _koCentral;

            KO_CV_FL = _koCVFL;

            KO_CV_FR = _koCVFR;

            KO_CV_RL = _koCVRL;

            KO_CV_RR = _koCVRR;

            bumpSteerCurveFL.GetParentObjectData(KO_CV_FL);

            bumpSteerCurveFL.GetParentObjectData(KO_CV_FR);

            wishboneInboardFL.Get_ParentObjectData(ref KO_CV_FL, this, VehicleCorner.FrontLeft, DevelopmentStages.ActuationPoints);

            wishboneInboardFR.Get_ParentObjectData(ref KO_CV_FR, this, VehicleCorner.FrontRight, DevelopmentStages.ActuationPoints);

            wishboneInboardRL.Get_ParentObjectData(ref KO_CV_RL, this, VehicleCorner.RearLeft, DevelopmentStages.ActuationPoints);

            wishboneInboardRR.Get_ParentObjectData(ref KO_CV_RR, this, VehicleCorner.RearRight, DevelopmentStages.ActuationPoints);

            actuationPoints_FL.Get_ParentObject_Data(ref KO_Central, ref KO_CV_FL, this, VehicleCorner.FrontLeft);

            actuationPoints_FR.Get_ParentObject_Data(ref KO_Central, ref KO_CV_FR, this, VehicleCorner.FrontRight);

            actuationPoints_RL.Get_ParentObject_Data(ref KO_Central, ref KO_CV_RL, this, VehicleCorner.RearLeft);

            actuationPoints_RR.Get_ParentObject_Data(ref KO_Central, ref KO_CV_RR, this, VehicleCorner.RearRight);


        }

        /// <summary>
        /// Method to initialize the <see cref="KO_SimulationParams"/> object of this class
        /// </summary>
        /// <param name="_koSimParams"></param>
        public void Set_KO_SimulationParams(ref KO_SimulationParams _koSimParams)
        {
            KO_SimParams = _koSimParams;
        } 
        #endregion


        #region ---Suspension Input Extraction AND Computation Methods---

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


        #region ---Tab Page - Vehicle Parameters ---

        #region Simulation Parameters

        //---No Of Iterations---
        private void tbNoOfIterations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_NoOfIterations();
            }
        }
        private void Set_NoOfIterations()
        {
            if (IntegerValidation(tbNoOfIterations.Text))
            {
                KO_SimParams.NumberOfIterations_KinematicSolver = Convert.ToInt32(tbNoOfIterations.Text);

                KO_SimParams.StepSize_Heave = KO_SimParams.Compute_StepSize_Heave(KO_SimParams.Maximum_Heave_Deflection, KO_SimParams.Minimum_Heave_Deflection, KO_SimParams.NumberOfIterations_KinematicSolver);

                Set_StepSize_Charts_Heave(KO_SimParams.StepSize_Heave);

                KO_SimParams.StepSize_Steering = KO_SimParams.Compute_StepSize_Steering(KO_SimParams.Maximum_Steering_Angle, KO_SimParams.Minimum_Steering_Angle, KO_SimParams.NumberOfIterations_KinematicSolver);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        //---Max Heave---
        private void tbMax_Heave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Max_Heave();
            }
        }
        private void Set_Max_Heave()
        {
            if (DoubleValidation(tbMax_Heave.Text))
            {
                KO_SimParams.Maximum_Heave_Deflection = Convert.ToDouble(tbMax_Heave.Text);

                KO_SimParams.StepSize_Heave = KO_SimParams.Compute_StepSize_Heave(KO_SimParams.Maximum_Heave_Deflection, KO_SimParams.Minimum_Heave_Deflection, KO_SimParams.NumberOfIterations_KinematicSolver);

                Set_StepSize_Charts_Heave(KO_SimParams.StepSize_Heave);
            }
            else
            {
                MessageBox.Show(NumericError);
            }

        }


        //---Min Heave---
        private void tbMin_Heave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Min_Heave();
            }
        }
        private void Set_Min_Heave()
        {
            if (DoubleValidation(tbMin_Heave.Text))
            {
                KO_SimParams.Minimum_Heave_Deflection = Convert.ToDouble(tbMin_Heave.Text);

                KO_SimParams.StepSize_Heave = KO_SimParams.Compute_StepSize_Heave(KO_SimParams.Maximum_Heave_Deflection, KO_SimParams.Minimum_Heave_Deflection, KO_SimParams.NumberOfIterations_KinematicSolver);

                Set_StepSize_Charts_Heave(KO_SimParams.StepSize_Heave);

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #region ---NOT USED---

        //---NOT USED - Step Size Heave---
        private void tbStepSize_Heave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_StepSize_Heave();
            }
        }
        private void Set_StepSize_Heave()
        {
            if (IntegerValidation(tbStepSize_Heave.Text))
            {
                KO_SimParams.StepSize_Heave = Convert.ToInt32(tbStepSize_Heave.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion

        //---Max Steering---
        private void tbMax_Steering_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Max_Steering();
            }
        }
        private void Set_Max_Steering()
        {
            if (DoubleValidation(tbMax_Steering.Text))
            {
                KO_SimParams.Maximum_Steering_Angle = Convert.ToDouble(tbMax_Steering.Text);

                KO_SimParams.StepSize_Steering = KO_SimParams.Compute_StepSize_Steering(KO_SimParams.Maximum_Steering_Angle, KO_SimParams.Minimum_Steering_Angle, KO_SimParams.NumberOfIterations_KinematicSolver);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        //---Min Steering---
        private void tbMin_Steering_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Min_Steeering();
            }
        }
        private void Set_Min_Steeering()
        {
            if (DoubleValidation(tbMin_Steering.Text))
            {
                KO_SimParams.Minimum_Steering_Angle = Convert.ToDouble(tbMin_Steering.Text);

                KO_SimParams.StepSize_Steering = KO_SimParams.Compute_StepSize_Steering(KO_SimParams.Maximum_Steering_Angle, KO_SimParams.Minimum_Steering_Angle, KO_SimParams.NumberOfIterations_KinematicSolver);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        #region ---NOT USED---
        //---NOT USED - Step Size Steering---
        private void tbStepSize_Steering_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_StepSize_Steering();
            }
        }
        private void Set_StepSize_Steering()
        {
            if (IntegerValidation(tbStepSize_Steering.Text))
            {
                KO_SimParams.StepSize_Steering = Convert.ToInt32(tbStepSize_Steering.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #endregion


        #region Wheelbase
        //---Wheelbase Textbox Events

        private void tbWheelbase_Leave(object sender, EventArgs e)
        {
            Set_Wheelbase();
        }

        private void tbWheelbase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Wheelbase();
            }
        }

        private void Set_Wheelbase()
        {
            if (DoubleValidation(tbWheelbase.Text))
            {
                if (Validatepositve_Double(tbWheelbase.Text))
                {
                    KO_Central.WheelBase = -Convert.ToDouble(tbWheelbase.Text);

                    Set_ContactPatch();

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

                    Set_ContactPatch();

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

                    Set_ContactPatch();

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
            Set_RC_Front_Height();
        }

        private void tbRC_Front_Hight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_RC_Front_Height();
            }
        }

        private void Set_RC_Front_Height()
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
            Set_RC_Front_LatOff();
        }

        private void tbRC_Front_LatOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_RC_Front_LatOff();
            }
        }

        private void Set_RC_Front_LatOff()
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
                ///<remarks>
                ///The positive sign is added for a reason. The user is going to specify the Length of the VSAL. This length is usually drawn towards the Right for FrontLeft Corner and towards the Left for FrontRight Corner
                ///But the user is just going to enter a positive value since its a length. 
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Left for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_FV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_FV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_FL.VSAL_FV = +Convert.ToDouble(tbFV_VSAL_FL.Text);

                Plot_VSAL_FV(out KO_CV_FL.VCornerParams.FV_IC_Line, KO_CV_FL.VSAL_FV, KO_CV_FL.ContactPatch, KO_Central.RC_Front, "KO_CV_FL.VCornerParams.FV_IC_Line");

                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    ///<remarks>
                    ///The negative sign is added for a reason. The user is going to specify the Length of the VSAL. This length is usually drawn towards the Right for FrontLeft Corner and towards the Left for FrontRight Corner
                    ///But the user is just going to enter a positive value since its a length. Hence we need to condition the value by adding a minus sign
                    ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Left for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                    ///---IMPORTANT--- <see cref="CAD.Init_VSAL_FV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_FV"/> is used. and you will understand 
                    /// </remarks>
                    KO_CV_FR.VSAL_FV = -Convert.ToDouble(tbFV_VSAL_FL.Text);

                    Plot_VSAL_FV(out KO_CV_FR.VCornerParams.FV_IC_Line, KO_CV_FR.VSAL_FV, KO_CV_FR.ContactPatch, KO_Central.RC_Front, "KO_CV_FR.VCornerParams.FV_IC_Line");
                }

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
                ///<remarks>
                ///The negative sign is added for a reason. The user is going to specify the Length of the VSAL. This length is usually drawn towards the Right for FrontLeft Corner and towards the Left for FrontRight Corner
                ///But the user is just going to enter a positive value since its a length. Hence we need to condition the value by adding a minus sign
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Left for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_FV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_FV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_FR.VSAL_FV = -Convert.ToDouble(tbFV_VSAL_FR.Text);

                Plot_VSAL_FV(out KO_CV_FR.VCornerParams.FV_IC_Line, KO_CV_FR.VSAL_FV, KO_CV_FR.ContactPatch, KO_Central.RC_Front, "KO_CV_FR.VCornerParams.FV_IC_Line");

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
                Set_FV_VSAL_RL();
            }
        }

        private void tbFV_VSAL_RL_Leave(object sender, EventArgs e)
        {
            Set_FV_VSAL_RL();
        }

        private void Set_FV_VSAL_RL()
        {
            if (DoubleValidation(tbFV_VSAL_RL.Text))
            {
                ///<remarks>
                ///The positive sign is added for a reason. The user is going to specify the Length of the VSAL. This length is usually drawn towards the Right for FrontLeft Corner and towards the Left for FrontRight Corner
                ///But the user is just going to enter a positive value since its a length
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Left for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value. 
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_FV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_FV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_RL.VSAL_FV = +Convert.ToDouble(tbFV_VSAL_RL.Text);

                Plot_VSAL_FV(out KO_CV_RL.VCornerParams.FV_IC_Line, KO_CV_RL.VSAL_FV, KO_CV_RL.ContactPatch, KO_Central.RC_Rear, "KO_CV_RL.VCornerParams.FV_IC_Line");

                if (Sus_Type.RearSymmetry_Boolean)
                {
                    ///<remarks>
                    ///The negative sign is added for a reason. The user is going to specify the Length of the VSAL. This length is usually drawn towards the Right for FrontLeft Corner and towards the Left for FrontRight Corner
                    ///But the user is just going to enter a positive value since its a length. Hence we need to condition the value by adding a minus sign
                    ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Left for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                    ///---IMPORTANT--- <see cref="CAD.Init_VSAL_FV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_FV"/> is used. and you will understand 
                    /// </remarks>
                    KO_CV_RR.VSAL_FV = -Convert.ToDouble(tbFV_VSAL_RL.Text);

                    Plot_VSAL_FV(out KO_CV_RR.VCornerParams.FV_IC_Line, KO_CV_RR.VSAL_FV, KO_CV_RR.ContactPatch, KO_Central.RC_Rear, "KO_CV_RR.VCornerParams.FV_IC_Line");
                }

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
                ///<remarks>
                ///The negative sign is added for a reason. The user is going to specify the Length of the VSAL. This length is usually drawn towards the Right for FrontLeft Corner and towards the Left for FrontRight Corner
                ///But the user is just going to enter a positive value since its a length. Hence we need to condition the value by adding a minus sign
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Left for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_FV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_FV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_RR.VSAL_FV = -Convert.ToDouble(tbFV_VSAL_RR.Text);

                Plot_VSAL_FV(out KO_CV_RR.VCornerParams.FV_IC_Line, KO_CV_RR.VSAL_FV, KO_CV_RR.ContactPatch, KO_Central.RC_Rear, "KO_CV_RR.VCornerParams.FV_IC_Line");
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

                KO_Central.PC_Left.Z = -Convert.ToDouble(tbPC_Left_LongOff.Text);

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
                ///<remarks>
                ///The positive sign is added for a reason. The user is going to specifiy the Length of the SV VSAL. This VSAL according to the normal conventino goes rewards for the Front and comes frontward for the Rear. 
                ///Hence, the positive is added to distinguish from the Rear (where as you will see later in the code, negative is added) so that VSAL is conditioned and can be use appropriately
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Front for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_SV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_SV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_FL.VSAL_SV = +Convert.ToDouble(tbSV_VSAL_FL.Text);

                Plot_VSAL_SV(out KO_CV_FL.VCornerParams.SV_IC_Line, KO_CV_FL.VSAL_SV, KO_CV_FL.ContactPatch, KO_Central.PC_Left, "KO_CV_FL.VCornerParams.SV_IC_Line");

                if (Sus_Type.FrontSymmetry_Boolean && Sus_Type.RearSymmetry_Boolean)
                {
                    ///<remarks>
                    ///The positive sign is added for a reason. The user is going to specifiy the Length of the SV VSAL. This VSAL according to the normal conventino goes rewards for the Front and comes frontward for the Rear. 
                    ///Hence, the positive is added to distinguish from the Rear (where as you will see later in the code, negative is added) so that VSAL is conditioned and can be use appropriately
                    ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Front for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                    ///---IMPORTANT--- <see cref="CAD.Init_VSAL_SV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_SV"/> is used. and you will understand 
                    /// </remarks>
                    KO_CV_FR.VSAL_SV = +Convert.ToDouble(tbSV_VSAL_FL.Text);

                    Plot_VSAL_SV(out KO_CV_FR.VCornerParams.SV_IC_Line, KO_CV_FR.VSAL_SV, KO_CV_FR.ContactPatch, KO_Central.PC_Right, "KO_CV_FR.VCornerParams.SV_IC_Line");
                }

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
                    ///<remarks>
                    ///The negative sign is added for a reason. The user is going to specifiy the Length of the SV VSAL. This VSAL according to the normal conventino goes rewards for the Front and comes frontward for the Rear. 
                    ///Hence, the positive is added to distinguish from the Rear (where as you will see later in the code, negative is added) so that VSAL is conditioned and can be use appropriately
                    ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Front for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                    ///---IMPORTANT--- <see cref="CAD.Init_VSAL_SV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_SV"/> is used. and you will understand 
                    /// </remarks>
                    KO_CV_RL.VSAL_SV = -Convert.ToDouble(tbSV_VSAL_RL.Text);

                    Plot_VSAL_SV(out KO_CV_RL.VCornerParams.SV_IC_Line, KO_CV_RL.VSAL_SV, KO_CV_RL.ContactPatch, KO_Central.PC_Left, "KO_CV_RL.VCornerParams.SV_IC_Line");

                    if (Sus_Type.FrontSymmetry_Boolean && Sus_Type.RearSymmetry_Boolean)
                    {
                        ///<remarks>
                        ///The negative sign is added for a reason. The user is going to specifiy the Length of the SV VSAL. This VSAL according to the normal conventino goes rewards for the Front and comes frontward for the Rear. 
                        ///Hence, the positive is added to distinguish from the Rear (where as you will see later in the code, negative is added) so that VSAL is conditioned and can be use appropriately
                        ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Front for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                        ///---IMPORTANT--- <see cref="CAD.Init_VSAL_SV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_SV"/> is used. and you will understand 
                        /// </remarks>
                        KO_CV_RR.VSAL_SV = -Convert.ToDouble(tbSV_VSAL_RL.Text);

                        Plot_VSAL_SV(out KO_CV_RR.VCornerParams.SV_IC_Line, KO_CV_RR.VSAL_SV, KO_CV_RR.ContactPatch, KO_Central.PC_Right, "KO_CV_RR.VCornerParams.SV_IC_Line");
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

                KO_Central.PC_Right.Z = -Convert.ToDouble(tbPC_Right_Height_LongOff.Text);

                Set_KO_PitchCenter();

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
            if (e.KeyCode == Keys.End)
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
                ///<remarks>
                ///The positive sign is added for a reason. The user is going to specifiy the Length of the SV VSAL. This VSAL according to the normal conventino goes rewards for the Front and comes frontward for the Rear. 
                ///Hence, the positive is added to distinguish from the Rear (where as you will see later in the code, negative is added) so that VSAL is conditioned and can be use appropriately
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Front for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_SV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_SV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_FR.VSAL_SV = +Convert.ToDouble(tbSV_VSAL_FR.Text);

                Plot_VSAL_SV(out KO_CV_FR.VCornerParams.SV_IC_Line, KO_CV_FR.VSAL_SV, KO_CV_FR.ContactPatch, KO_Central.PC_Right, "KO_CV_FR.VCornerParams.SV_IC_Line");

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
                ///<remarks>
                ///The negative sign is added for a reason. The user is going to specifiy the Length of the SV VSAL. This VSAL according to the normal conventino goes rewards for the Front and comes frontward for the Rear. 
                ///Hence, the positive is added to distinguish from the Rear (where as you will see later in the code, negative is added) so that VSAL is conditioned and can be use appropriately
                ///---IMPORTANT--- Now if the user wants an unconventional VSAL (towards the Front for FrontLeft and vice versa) then he/she will have to add a minus sign while entering the value
                ///---IMPORTANT--- <see cref="CAD.Init_VSAL_SV(Line, double, Point3D, Point3D, string)"/> where the <see cref="KO_CornverVariables.VSAL_SV"/> is used. and you will understand 
                /// </remarks>
                KO_CV_RR.VSAL_SV = -Convert.ToDouble(tbSV_VSAL_RR.Text);

                Plot_VSAL_SV(out KO_CV_RR.VCornerParams.SV_IC_Line, KO_CV_RR.VSAL_SV, KO_CV_RR.ContactPatch, KO_Central.PC_Right, "KO_CV_RR.VCornerParams.SV_IC_Line");

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

        //---END : Tab Page Vehicle Parameters---
        #endregion


        #region ---Tab Page - Corner Parameters---

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

                Plot_SteeringAxis(out KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch, "KO_CV_FL.VCornerParams.SteeringAxis");

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    ///<remarks>
                    ///Negative is added for a reason. For the user the KPI Angle translates to an inclination of the Steering Axis which is tilted to the car. 
                    ///For the Left side this is a clockwise rotation which is positive. But for the RIGHT side this is a CCW rotation which is neggative. Hence the user input needs to be conditioned
                    /// </remarks>
                    KO_CV_FR.KPI = new Angle(-Convert.ToDouble(tbKPI_FL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");
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
                    ///<remarks>
                    ///Negative is added for a reason. For the user the Scrub Radius translates to the distance of the Steering Axis from the Contact Patch point which is also the half track 
                    ///For the Left side if the distance is inward then for the right also it must be inward. If the negative sign isn't applied then the value will be treaded as positve and the for the Left, the Steering Axis
                    ///will be translated more outward. 
                    /// Hence the user input needs to be conditioned
                    /// </remarks>
                    KO_CV_FL.ScrubRadius = -Convert.ToDouble(tbScrub_FL.Text);

                    Plot_SteeringAxis(out KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch, "KO_CV_FL.VCornerParams.SteeringAxis");

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        ///<summary>In order to maintain the convention of positive Scrub is translating the Steering Axis Inward, psoitive sign is applied</summary>
                        KO_CV_FR.ScrubRadius = +Convert.ToDouble(tbScrub_FL.Text);
                        Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");
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
                ///<remarks>
                ///Negative Sign is added for a Reason. For the user, positive Caster translates to the Steering Axis's top end (or UBJ) pointing MORE rearward than the bottom end (LBJ). 
                ///This is for the solver a CW rotation which is negative. 
                ///Hence the user input value must be conditioned
                /// </remarks>
                KO_CV_FL.Caster = new Angle(-Convert.ToDouble(tbCaster_FL.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(out KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch, "KO_CV_FL.VCornerParams.SteeringAxis");

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    ///<remarks>
                    ///Negative Sign is added for a Reason. For the user, positive Caster translates to the Steering Axis's top end (or UBJ) pointing MORE rearward than the bottom end (LBJ). 
                    ///This is for the solver a CW rotation which is negative. 
                    ///Hence the user input value must be conditioned
                    KO_CV_FR.Caster = new Angle(-Convert.ToDouble(tbCaster_FL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");
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

                    Plot_SteeringAxis(out KO_CV_FL.VCornerParams.SteeringAxis, KO_CV_FL.KPI, KO_CV_FL.Caster, KO_CV_FL.ScrubRadius, KO_CV_FL.MechTrail, KO_CV_FL.ContactPatch, "KO_CV_FL.VCornerParams.SteeringAxis");

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.MechTrail = Convert.ToDouble(tbMechtrail_FL.Text);
                        Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");
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
                    KO_CV_FL.VCornerParams.UBJ = KO_CV_FL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FL.Text), KO_CV_FL.VCornerParams.SteeringAxis.StartPoint, KO_CV_FL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FL.VCornerParams.UBJ, "KO_CV_FL.VCornerParams.UBJ");

                    Plot_WishbonePlane(out KO_CV_FL.VCornerParams.TopWishbonePlane, KO_CV_FL.VCornerParams.UBJ, KO_CV_FL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FL.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FL.VCornerParams.TopWishbonePlane", true);

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();

                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.VCornerParams.UBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FL.Text), KO_CV_FR.VCornerParams.SteeringAxis.StartPoint, KO_CV_FR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_FR.VCornerParams.UBJ, "KO_CV_FR.VCornerParams.UBJ");

                        Plot_WishbonePlane(out KO_CV_FR.VCornerParams.TopWishbonePlane, KO_CV_FR.VCornerParams.UBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FR.VCornerParams.TopWishbonePlane", false);

                        KO_CV_FR.VCornerParams.Initialize_Dictionary();

                    }
                    //else
                    //{
                    //    ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                    //    Plot_WishbonePlane(out KO_CV_FR.VCornerParams.TopWishbonePlane, KO_CV_FR.VCornerParams.UBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FR.VCornerParams.TopWishbonePlane");
                    //}

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
                    KO_CV_FL.VCornerParams.LBJ = KO_CV_FL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_FL.Text), KO_CV_FL.VCornerParams.SteeringAxis.StartPoint, KO_CV_FL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FL.VCornerParams.LBJ, "KO_CV_FL.VCornerParams.LBJ");

                    Plot_WishbonePlane(out KO_CV_FL.VCornerParams.BottomWishbonePlane, KO_CV_FL.VCornerParams.LBJ, KO_CV_FL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FL.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FL.VCornerParams.BottomWishbonePlane", true);

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.VCornerParams.LBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_FL.Text), KO_CV_FR.VCornerParams.SteeringAxis.StartPoint, KO_CV_FR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_FR.VCornerParams.LBJ, "KO_CV_FR.VCornerParams.LBJ");

                        Plot_WishbonePlane(out KO_CV_FR.VCornerParams.BottomWishbonePlane, KO_CV_FR.VCornerParams.LBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FR.VCornerParams.BottomWishbonePlane", false);


                        KO_CV_FR.VCornerParams.Initialize_Dictionary();

                    }
                    //else
                    //{
                    //    ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                    //    Plot_WishbonePlane(out KO_CV_FR.VCornerParams.BottomWishbonePlane, KO_CV_FR.VCornerParams.LBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FR.VCornerParams.BottomWishbonePlane");
                    //}
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
                    KO_CV_FL.VCornerParams.WheelCenter = KO_CV_FL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_FL.Text), KO_CV_FL.VCornerParams.SteeringAxis.StartPoint, KO_CV_FL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FL.VCornerParams.WheelCenter, "KO_CV_FL.VCornerParams.WheelCenter");

                    KO_CV_FL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_FR.VCornerParams.WheelCenter = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_FL.Text), KO_CV_FR.VCornerParams.SteeringAxis.StartPoint, KO_CV_FR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_FR.VCornerParams.WheelCenter, "KO_CV_FR.VCornerParams.WheelCenter");

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
                ///<remarks>
                ///Negative is added for a reason. For the user the KPI Angle translates to an inclination of the Steering Axis which is tilted to the car. 
                ///For the Left side this is a clockwise rotation which is positive. But for the RIGHT side this is a CCW rotation which is neggative. Hence the user input needs to be conditioned
                /// </remarks>
                KO_CV_FR.KPI = new Angle(-Convert.ToDouble(tbKPI_FR.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");

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

                    Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");

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
                ///<remarks>
                ///Negative Sign is added for a Reason. For the user, positive Caster translates to the Steering Axis's top end (or UBJ) pointing MORE rearward than the bottom end (LBJ). 
                ///This is for the solver a CW rotation which is negative. 
                ///Hence the user input value must be conditioned
                KO_CV_FR.Caster = new Angle(-Convert.ToDouble(tbCaster_FR.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");

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

                    Plot_SteeringAxis(out KO_CV_FR.VCornerParams.SteeringAxis, KO_CV_FR.KPI, KO_CV_FR.Caster, KO_CV_FR.ScrubRadius, KO_CV_FR.MechTrail, KO_CV_FR.ContactPatch, "KO_CV_FR.VCornerParams.SteeringAxis");

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
                    KO_CV_FR.VCornerParams.UBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_FR.Text), KO_CV_FR.VCornerParams.SteeringAxis.StartPoint, KO_CV_FR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.UBJ, "KO_CV_FR.VCornerParams.UBJ");

                    Plot_WishbonePlane(out KO_CV_FR.VCornerParams.TopWishbonePlane, KO_CV_FR.VCornerParams.UBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FR.VCornerParams.TopWishbonePlane", true);

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
                    KO_CV_FR.VCornerParams.LBJ = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_FR.Text), KO_CV_FR.VCornerParams.SteeringAxis.StartPoint, KO_CV_FR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.LBJ, "KO_CV_FR.VCornerParams.LBJ");

                    Plot_WishbonePlane(out KO_CV_FR.VCornerParams.BottomWishbonePlane, KO_CV_FR.VCornerParams.LBJ, KO_CV_FR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_FR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_FR.VCornerParams.BottomWishbonePlane", true);

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

                    KO_CV_FR.VCornerParams.WheelCenter = KO_CV_FR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_FR.Text), KO_CV_FR.VCornerParams.SteeringAxis.StartPoint, KO_CV_FR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.WheelCenter, "KO_CV_FR.VCornerParams.WheelCenter");

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

                Plot_SteeringAxis(out KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch, "KO_CV_RL.VCornerParams.SteeringAxis");

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    ///<remarks>
                    ///Negative is added for a reason. For the user the KPI Angle translates to an inclination of the Steering Axis which is tilted to the car. 
                    ///For the Left side this is a clockwise rotation which is positive. But for the RIGHT side this is a CCW rotation which is neggative. Hence the user input needs to be conditioned
                    /// </remarks>
                    KO_CV_RR.KPI = new Angle(-Convert.ToDouble(tbKPI_RL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");
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
                    ///<remarks>
                    ///Negative is added for a reason. For the user the Scrub Radius translates to the distance of the Steering Axis from the Contact Patch point which is also the half track 
                    ///For the Left side if the distance is inward then for the right also it must be inward. If the negative sign isn't applied then the value will be treaded as positve and the for the Left, the Steering Axis
                    ///will be translated more outward. 
                    /// Hence the user input needs to be conditioned
                    /// </remarks>
                    KO_CV_RL.ScrubRadius = -Convert.ToDouble(tbScrub_RL.Text);

                    Plot_SteeringAxis(out KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch, "KO_CV_RL.VCornerParams.SteeringAxis");

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.ScrubRadius = +Convert.ToDouble(tbScrub_RL.Text);
                        Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");
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
                ///<remarks>
                ///Negative Sign is added for a Reason. For the user, positive Caster translates to the Steering Axis's top end (or UBJ) pointing MORE rearward than the bottom end (LBJ). 
                ///This is for the solver a CW rotation which is negative. 
                ///Hence the user input value must be conditioned
                KO_CV_RL.Caster = new Angle(-Convert.ToDouble(tbCaster_RL.Text), AngleUnit.Degrees);

                Plot_SteeringAxis(out KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch, "KO_CV_RL.VCornerParams.SteeringAxis");

                ///<summary>Handling condition of Symmetry</summary>
                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    ///<remarks>
                    ///Negative Sign is added for a Reason. For the user, positive Caster translates to the Steering Axis's top end (or UBJ) pointing MORE rearward than the bottom end (LBJ). 
                    ///This is for the solver a CW rotation which is negative. 
                    ///Hence the user input value must be conditioned
                    KO_CV_RR.Caster = new Angle(-Convert.ToDouble(tbCaster_RL.Text), AngleUnit.Degrees);
                    Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");
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

                    Plot_SteeringAxis(out KO_CV_RL.VCornerParams.SteeringAxis, KO_CV_RL.KPI, KO_CV_RL.Caster, KO_CV_RL.ScrubRadius, KO_CV_RL.MechTrail, KO_CV_RL.ContactPatch, "KO_CV_RL.VCornerParams.SteeringAxis");

                    ///<summary>Handling condition of Symmetry</summary>
                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.MechTrail = Convert.ToDouble(tbMechtrail_RL.Text);
                        Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");
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
                    KO_CV_RL.VCornerParams.UBJ = KO_CV_RL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_RL.Text), KO_CV_RL.VCornerParams.SteeringAxis.StartPoint, KO_CV_RL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RL.VCornerParams.UBJ, "KO_CV_RL.VCornerParams.UBJ");

                    Plot_WishbonePlane(out KO_CV_RL.VCornerParams.TopWishbonePlane, KO_CV_RL.VCornerParams.UBJ, KO_CV_RL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RL.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RL.VCornerParams.TopWishbonePlane", true);

                    KO_CV_RL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.RearSymmetry_Boolean)
                    {
                        KO_CV_RR.VCornerParams.UBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_RL.Text), KO_CV_RR.VCornerParams.SteeringAxis.StartPoint, KO_CV_RR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_RR.VCornerParams.UBJ, "KO_CV_RR.VCornerParams.UBJ");

                        Plot_WishbonePlane(out KO_CV_RR.VCornerParams.TopWishbonePlane, KO_CV_RR.VCornerParams.UBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RR.VCornerParams.TopWishbonePlane", false);

                        KO_CV_RR.VCornerParams.Initialize_Dictionary();


                    }
                    //else
                    //{
                    //    ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                    //    Plot_WishbonePlane(out KO_CV_RR.VCornerParams.TopWishbonePlane, KO_CV_RR.VCornerParams.UBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RR.VCornerParams.TopWishbonePlane");
                    //}
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
                    KO_CV_RL.VCornerParams.LBJ = KO_CV_RL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_RL.Text), KO_CV_RL.VCornerParams.SteeringAxis.StartPoint, KO_CV_RL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RL.VCornerParams.LBJ, "KO_CV_RL.VCornerParams.LBJ");

                    Plot_WishbonePlane(out KO_CV_RL.VCornerParams.BottomWishbonePlane, KO_CV_RL.VCornerParams.LBJ, KO_CV_RL.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RL.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RL.VCornerParams.BottomWishbonePlane", true);


                    KO_CV_RL.VCornerParams.Initialize_Dictionary();

                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.VCornerParams.LBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_RL.Text), KO_CV_RR.VCornerParams.SteeringAxis.StartPoint, KO_CV_RR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_RR.VCornerParams.LBJ, "KO_CV_RR.VCornerParams.LBJ");

                        Plot_WishbonePlane(out KO_CV_RR.VCornerParams.BottomWishbonePlane, KO_CV_RR.VCornerParams.LBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RR.VCornerParams.BottomWishbonePlane", false);

                        KO_CV_RR.VCornerParams.Initialize_Dictionary();

                    }
                    //else
                    //{
                    //    ///<remarks>Added in Else Block because I want the user to see the Planes on the Right only for Assymetric Suspension</remarks>
                    //    Plot_WishbonePlane(out KO_CV_RR.VCornerParams.BottomWishbonePlane, KO_CV_RR.VCornerParams.LBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RR.VCornerParams.BottomWishbonePlane");
                    //}
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
                    KO_CV_RL.VCornerParams.WheelCenter = KO_CV_RL.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_RL.Text), KO_CV_RL.VCornerParams.SteeringAxis.StartPoint, KO_CV_RL.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RL.VCornerParams.WheelCenter, "KO_CV_RL.VCornerParams.WheelCenter");

                    KO_CV_RL.VCornerParams.Initialize_Dictionary();


                    if (Sus_Type.FrontSymmetry_Boolean)
                    {
                        KO_CV_RR.VCornerParams.WheelCenter = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_RL.Text), KO_CV_RR.VCornerParams.SteeringAxis.StartPoint, KO_CV_RR.VCornerParams.SteeringAxis);

                        Plot_OutboardPoint(KO_CV_RR.VCornerParams.WheelCenter, "KO_CV_RR.VCornerParams.WheelCenter");

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
                ///<remarks>
                ///Negative is added for a reason. For the user the KPI Angle translates to an inclination of the Steering Axis which is tilted to the car. 
                ///For the Left side this is a clockwise rotation which is positive. But for the RIGHT side this is a CCW rotation which is neggative. Hence the user input needs to be conditioned
                /// </remarks>
                KO_CV_RR.KPI = new Angle(-Convert.ToDouble(tbKPI_RR.Text), AngleUnit.Degrees);
                Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");

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
                    Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");

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
                ///<remarks>
                ///Negative Sign is added for a Reason. For the user, positive Caster translates to the Steering Axis's top end (or UBJ) pointing MORE rearward than the bottom end (LBJ). 
                ///This is for the solver a CW rotation which is negative. 
                ///Hence the user input value must be conditioned
                KO_CV_RR.Caster = new Angle(-Convert.ToDouble(tbCaster_RR.Text), AngleUnit.Degrees);
                Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");

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
                    Plot_SteeringAxis(out KO_CV_RR.VCornerParams.SteeringAxis, KO_CV_RR.KPI, KO_CV_RR.Caster, KO_CV_RR.ScrubRadius, KO_CV_RR.MechTrail, KO_CV_RR.ContactPatch, "KO_CV_RR.VCornerParams.SteeringAxis");

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
                    KO_CV_RR.VCornerParams.UBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbUBJ_RR.Text), KO_CV_RR.VCornerParams.SteeringAxis.StartPoint, KO_CV_RR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RR.VCornerParams.UBJ, "KO_CV_RR.VCornerParams.UBJ");

                    Plot_WishbonePlane(out KO_CV_RR.VCornerParams.TopWishbonePlane, KO_CV_RR.VCornerParams.UBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RR.VCornerParams.TopWishbonePlane", true);

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
                    KO_CV_RR.VCornerParams.LBJ = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbLBJ_RR.Text), KO_CV_RR.VCornerParams.SteeringAxis.StartPoint, KO_CV_RR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RR.VCornerParams.LBJ, "KO_CV_RR.VCornerParams.LBJ");

                    Plot_WishbonePlane(out KO_CV_RR.VCornerParams.BottomWishbonePlane, KO_CV_RR.VCornerParams.LBJ, KO_CV_RR.VCornerParams.FV_IC_Line.EndPoint, KO_CV_RR.VCornerParams.SV_IC_Line.EndPoint, "KO_CV_RR.VCornerParams.BottomWishbonePlane", true);
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
                    KO_CV_RR.VCornerParams.WheelCenter = KO_CV_RR.Compute_PointOnLine_FromScalarParametric(Convert.ToDouble(tbWC_RR.Text), KO_CV_RR.VCornerParams.SteeringAxis.StartPoint, KO_CV_RR.VCornerParams.SteeringAxis);

                    Plot_OutboardPoint(KO_CV_RR.VCornerParams.WheelCenter, "KO_CV_RR.VCornerParams.WheelCenter");

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


        #region ---Tab Page - Toe Link Points---

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

                Set_OutboardToeLink();

                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    KO_CV_FR.PitmanTrail = Convert.ToDouble(tbPitmanLeft.Text);

                    Set_OutboardToeLink();

                    Plot_OutboardPoint(KO_CV_FR.VCornerParams.ToeLinkOutboard, "KO_CV_FR.VCornerParams.ToeLinkOutboard");

                    KO_CV_FR.VCornerParams.Initialize_Dictionary();

                }

                Plot_OutboardPoint(KO_CV_FL.VCornerParams.ToeLinkOutboard, "KO_CV_FL.VCornerParams.ToeLinkOutboard");

                KO_CV_FL.VCornerParams.Initialize_Dictionary();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion


        #region Toe Link Length Left
        private void tbToeLinkLength_FL_Leave(object sender, EventArgs e)
        {
            Set_ToeLinkLength_FL();
        }

        private void tbToeLinkLength_FL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_ToeLinkLength_FL();
            }
        }

        private void Set_ToeLinkLength_FL()
        {
            if (DoubleValidation(tbToeLinkLength_FL.Text))
            {

                KO_CV_FL.ToeLinkLength = Convert.ToDouble(tbToeLinkLength_FL.Text);

                KO_CV_FL.Compute_InboardToeLink(out KO_CV_FL.VCornerParams.ToeLinkInboard, KO_CV_FL.VCornerParams.ToeLinkOutboard, KO_CV_FL.ToeLinkLength);

                KO_CV_FL.VCornerParams.Initialize_Dictionary();

                ///<remarks>
                ///Toe Link is not drawn here because this is just an initial guess. The Optimizer will generate the final value of the Toe Link and then it will be drawn
                ///</remarks>

                if (Sus_Type.FrontSymmetry_Boolean)
                {
                    ///<remarks>
                    ///Negative is added for a reason. When the user inputs the Toe Link Length it will always be positive. But for us we need the Toe Link Inboard point to always be on the Chassis side. 
                    ///Hence negative is added for the Right side Link Length. <see cref="KO_CornverVariables.Compute_InboardToeLink(out Point3D, Point3D, double)"/> . 
                    /// </remarks>
                    KO_CV_FR.ToeLinkLength = -Convert.ToDouble(tbToeLinkLength_FL.Text);

                    KO_CV_FR.Compute_InboardToeLink(out KO_CV_FR.VCornerParams.ToeLinkInboard, KO_CV_FR.VCornerParams.ToeLinkOutboard, KO_CV_FR.ToeLinkLength);

                    KO_CV_FR.VCornerParams.Initialize_Dictionary();

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

                Set_OutboardToeLink();

                Plot_OutboardPoint(KO_CV_FR.VCornerParams.ToeLinkOutboard, "KO_CV_FR.VCornerParams.ToeLinkOutboard");

                KO_CV_FR.VCornerParams.Initialize_Dictionary();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        #endregion


        #region Toe Link Length Right
        private void tbToeLinkLength_FR_Leave(object sender, EventArgs e)
        {
            Set_ToeLinkLength_FR();
        }

        private void tbToeLinkLength_FR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_ToeLinkLength_FR();
            }
        }

        private void Set_ToeLinkLength_FR()
        {
            if (DoubleValidation(tbToeLinkLength_FR.Text))
            {

                ///<remarks>
                ///Negative is added for a reason. When the user inputs the Toe Link Length it will always be positive. But for us we need the Toe Link Inboard point to always be on the Chassis side. 
                ///Hence negative is added for the Right side Link Length. <see cref="KO_CornverVariables.Compute_InboardToeLink(out Point3D, Point3D, double)"/> . 
                /// </remarks>
                KO_CV_FR.ToeLinkLength = -Convert.ToDouble(tbToeLinkLength_FR.Text);

                KO_CV_FR.Compute_InboardToeLink(out KO_CV_FR.VCornerParams.ToeLinkInboard, KO_CV_FR.VCornerParams.ToeLinkOutboard, KO_CV_FR.ToeLinkLength);

                ///<remarks>
                ///Toe Link is not drawn here because this is just an initial guess. The Optimizer will generate the final value of the Toe Link and then it will be drawn
                ///</remarks>

                KO_CV_FR.VCornerParams.Initialize_Dictionary();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion


        #region Toe Link Inboard FL
        private void simpleButtonPlotToelinkInboard_FL_Click(object sender, EventArgs e)
        {
            Compute_InboardToeLinkPoint();
        }

        private void Compute_InboardToeLinkPoint()
        {
            KO_CV_FL.BumpSteerCurve = bumpSteerCurveFL.BumpSteerParms;

            KO_CV_FL.Optimize_InboardToeLink(ref KO_CV_FL, ref KO_Central, ref KO_SimParams, VehicleCorner.FrontLeft, this);

            Plot_InboardPoints(KO_CV_FL.VCornerParams.ToeLinkInboard, KO_CV_FL.VCornerParams.ToeLinkOutboard, "KO_CV_FL.VCornerParams.ToeLinkInboard", "KO_CV_FL.VCornerParams.ToeLink");

            tbConvergence_BS_FL.Text = Convert.ToString(KO_CV_FL.BumpSteerConvergence.Percentage);

            KO_CV_FL.VCornerParams.Initialize_Dictionary();

            if (Sus_Type.FrontSymmetry_Boolean)
            {
                KO_CV_FR.VCornerParams.ToeLinkInboard = new Point3D(-KO_CV_FL.VCornerParams.ToeLinkInboard.X, KO_CV_FL.VCornerParams.ToeLinkInboard.Y, KO_CV_FL.VCornerParams.ToeLinkInboard.Z);

                KO_CV_FR.VCornerParams.Initialize_Dictionary();

                Plot_InboardPoints(KO_CV_FR.VCornerParams.ToeLinkInboard, KO_CV_FR.VCornerParams.ToeLinkOutboard, "KO_CV_FR.VCornerParams.ToeLinkInboard", "KO_CV_FR.VCornerParams.ToeLink");
            }
        }
        #endregion


        #region Toe Link Inboard FR
        private void simpleButtonPlotToelinkInboard_FR_Click(object sender, EventArgs e)
        {
            Compute_InboardToeLink_FR();
        }

        private void Compute_InboardToeLink_FR()
        {
            KO_CV_FR.BumpSteerCurve = bumpSteerCurveFR.BumpSteerParms;

            KO_CV_FR.Optimize_InboardToeLink(ref KO_CV_FR, ref KO_Central, ref KO_SimParams, VehicleCorner.FrontRight, this);

            Plot_InboardPoints(KO_CV_FR.VCornerParams.ToeLinkInboard, KO_CV_FR.VCornerParams.ToeLinkOutboard, "KO_CV_FR.VCornerParams.ToeLinkInboard", "KO_CV_FR.VCornerParams.ToeLink");

            tbConvergence_BS_FR.Text = Convert.ToString(KO_CV_FR.BumpSteerConvergence.Percentage);

        }
        #endregion

        //---END : Tab Page - Toe Link Points---
        #endregion

        //---END : Suspension Input Extraction Methods---
        #endregion


        #region ---CAD UserControl Plotter Methods---

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
        private void Plot_VSAL_FV(out Line _FV_IC_Line, double _VSAL_FV_Length, Point3D _ConctactPatch, Point3D _RC, string _ICLineName)
        {
            cad1.Init_VSAL_FV(out _FV_IC_Line, _VSAL_FV_Length, _ConctactPatch, _RC, _ICLineName);
        }

        /// <summary>
        /// Method to plot the VSAL of the SV
        /// </summary>
        /// <param name="_VSAL_SV_Length">VSAL Length in Side View</param>
        /// <param name="_ConctactPatch"></param>
        /// <param name="_PC"></param>
        private void Plot_VSAL_SV(out Line _SV_IC_Line, double _VSAL_SV_Length, Point3D _ConctactPatch, Point3D _PC, string _IClineName)
        {
            cad1.Init_VSAL_SV(out _SV_IC_Line, _VSAL_SV_Length, _ConctactPatch, _PC, _IClineName);
        }

        //---END : Vehicle Parameters 
        #endregion

        #region -Corner Params & Toe Link Outboard Point-

        /// <summary>
        /// Method to Initialize and Plot the Steering Axis 
        /// </summary>
        /// <param name="_SteeringAxis">Steering Axis</param>
        /// <param name="_KPI">KPI Angle</param>
        /// <param name="_Caster">Caster Angle</param>
        /// <param name="_ScrubRadius">Scrub Radius</param>
        /// <param name="_MechTrail">Mechanical Trail</param>
        /// <param name="_ContactPatch">Contact Patch</param>
        private void Plot_SteeringAxis(out Line _SteeringAxis, Angle _KPI, Angle _Caster, double _ScrubRadius, double _MechTrail, Point3D _ContactPatch, string _SteeringAxisName)
        {
            cad1.Plot_SteeringAxis(out _SteeringAxis, _KPI, _Caster, _ScrubRadius, _MechTrail, _ContactPatch, _SteeringAxisName);
        }

        /// <summary>
        /// Method to Plot th Outboard point
        /// </summary>
        /// <param name="_outboardPoint"></param>
        public void Plot_OutboardPoint(Point3D _outboardPoint, string _outBoardPointName)
        {
            cad1.Plot_OutboardPoint(_outboardPoint, _outBoardPointName);
        }

        /// <summary>
        /// Method to plot the Plane of the pair of wishbones being considered
        /// </summary>
        /// <param name="_WishbonePlane">Plane of the pair of wishbones being considered</param>
        /// <param name="_Point1">First point making up the plane</param>
        /// <param name="_Point2">Second point making up the plane</param>
        /// <param name="_Point3">Third point making up the plane</param>
        private void Plot_WishbonePlane(out Plane _WishbonePlane, Point3D _Point1, Point3D _Point2, Point3D _Point3, string _WishbonePlaneName, bool _PlotPlane)
        {
            cad1.Plot_Plane(out _WishbonePlane, _Point1, _Point2, _Point3, _WishbonePlaneName, _PlotPlane);
        }

        //---END : Corner Params---
        #endregion

        #region -Inboard Points and Toe Link Inboard Point-
        /// <summary>
        /// Method to Plot the Inboard Point and create a Bar using it's corresponding Outboard Point
        /// </summary>
        /// <param name="_InboardPoint">Inboard Point</param>
        /// <param name="_OutboardPoint">Corresponding Outboard Point</param>
        /// <param name="_InboardPointName">Name of the Inboard Point being Plotted</param>
        /// <param name="_WishboneArmName">Name of the Wishbone Arm being plotted</param>
        public void Plot_InboardPoints(Point3D _InboardPoint, Point3D _OutboardPoint, string _InboardPointName, string _WishboneArmName)
        {
            cad1.Plot_InboardWishbonePoint(_InboardPoint, _OutboardPoint, _InboardPointName, _WishboneArmName);
        }
        #endregion

        #region -Actuation Points-

        /// <summary>
        /// Method to Plot a <see cref="Vector3D"/> Axis in the form of a Line
        /// </summary>
        /// <param name="_AxisToPlot"><see cref="Vector3D"/> representing the Axis</param>
        /// <param name="_StartPoint">Start <see cref="Point3D"/> of the Axis</param>
        /// <param name="_AxisColour">Colour of the Axis</param>
        /// <param name="_AxisName">Name of the Axis</param>
        public void Plot_Axis(Vector3D _AxisToPlot, Point3D _StartPoint, Color _AxisColour, string _AxisName)
        {
            cad1.Plot_Axis(_AxisToPlot, _StartPoint, _AxisColour, _AxisName);
        }

        #endregion

        //---END : CAD UserControl Plotter Methods---
        #endregion


        #region ---Initialize Methods---

        /// <summary>
        /// Common Method to initialize the <see cref="KO_CornverVariables.ContactPatch"/> points
        /// </summary>
        private void Set_ContactPatch()
        {
            //---Front Contact Patch---

            ///<summary>Using the Track Width to Set the Front Contact Patch</summary>
            KO_CV_FL.ContactPatch.X = KO_Central.Track_Front / 2;

            KO_CV_FR.ContactPatch.X = -KO_Central.Track_Front / 2;

            //---Rear Contact Patch---

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
        public void Set_OutboardToeLink()
        {
            KO_Central.Compute_OutboardToeLink(ref KO_CV_FL, ref KO_CV_FR, ref KO_CV_RL, ref KO_CV_RR);
        }

        /// <summary>
        /// Method to Set the StepSize of all the charts. <see cref="XUC_KO_GenericChart.StepSize"/> and <see cref="BumpSteerCurve.StepSize"/>
        /// </summary>
        /// <param name="_stepSizeHeave">Step Size of the Heave Chart</param>
        public void Set_StepSize_Charts_Heave(double _stepSizeHeave)
        {
            bumpSteerCurveFL.StepSize = _stepSizeHeave;

            bumpSteerCurveFL.BumpSteerParms.StepSize = bumpSteerCurveFL.StepSize;
        }


        public void Set_StepSize_Charts_Steeering(double _steSizeSteering)
        {

        }


        #endregion


        #region ---Suspension Creations - Includes Template Creation, Suspnsion Creation and Vehicle Suspension Initialization---

        int SuspensionTemplateButtonClickCount = 0;
        private void simpleButtonSuspensiontemplate_Click(object sender, EventArgs e)

        {
            if (SuspensionTemplateButtonClickCount == 0)
            {
                CreateSuspensionTemplate();
            }

            SuspensionTemplateButtonClickCount = 1;

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

            Sus_Type = new SuspensionType(true);

            Sus_Type.ShowDialog();

            CreateSuspensions();

            AssignSuspensionToVehicle();

            if (Sus_Type.FrontSymmetry_Boolean)
            {
                wishboneInboardFL.Get_ParentObjectData(ref KO_CV_FL, ref KO_CV_FR, this, VehicleCorner.FrontLeft, VehicleCorner.FrontRight, DevelopmentStages.WishboneInboardPoints);

                actuationPoints_FL.Get_ParentObject_Data(ref KO_Central, ref KO_CV_FL, ref KO_CV_FR, VehicleCorner.FrontLeft, VehicleCorner.FrontRight, this);
            }
            if (Sus_Type.RearSymmetry_Boolean)
            {
                wishboneInboardRL.Get_ParentObjectData(ref KO_CV_RL, ref KO_CV_RR, this, VehicleCorner.RearLeft, VehicleCorner.RearRight, DevelopmentStages.WishboneInboardPoints);

                actuationPoints_RL.Get_ParentObject_Data(ref KO_Central, ref KO_CV_RL, ref KO_CV_RR, VehicleCorner.RearLeft, VehicleCorner.RearRight, this);
            }

        }

        private void CreateSuspensions()
        {
            Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();

            R1.scflGUI.Add(new SuspensionCoordinatesFrontGUI());
            R1.scflGUI[R1.scflGUI.Count - 1].FrontSuspensionTypeGUI(Sus_Type);
            SuspensionCoordinatesFront.Assy_List_SCFL.Add(new SuspensionCoordinatesFront(R1.scflGUI[R1.scflGUI.Count - 1]));
            SuspensionCoordinatesFront.Assy_List_SCFL[SuspensionCoordinatesFront.Assy_List_SCFL.Count - 1].FrontSuspensionType(R1.scflGUI[R1.scflGUI.Count - 1]);

            R1.scfrGUI.Add(new SuspensionCoordinatesFrontRightGUI());
            R1.scfrGUI[R1.scfrGUI.Count - 1].FrontSuspensionTypeGUI(Sus_Type);
            SuspensionCoordinatesFrontRight.Assy_List_SCFR.Add(new SuspensionCoordinatesFrontRight(R1.scfrGUI[R1.scfrGUI.Count - 1]));
            SuspensionCoordinatesFrontRight.Assy_List_SCFR[SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count - 1].FrontSuspensionType(R1.scfrGUI[R1.scfrGUI.Count - 1]);

            R1.scrlGUI.Add(new SuspensionCoordinatesRearGUI());
            R1.scrlGUI[R1.scrlGUI.Count - 1].RearSuspensionTypeGUI(Sus_Type);
            SuspensionCoordinatesRear.Assy_List_SCRL.Add(new SuspensionCoordinatesRear(R1.scrlGUI[R1.scrlGUI.Count - 1]));
            SuspensionCoordinatesRear.Assy_List_SCRL[SuspensionCoordinatesRear.Assy_List_SCRL.Count - 1].RearSuspensionType(R1.scrlGUI[R1.scrlGUI.Count - 1]);

            R1.scrrGUI.Add(new SuspensionCoordinatesRearRightGUI());
            R1.scrrGUI[R1.scrrGUI.Count - 1].RearSuspensionTypeGUI(Sus_Type);
            SuspensionCoordinatesRearRight.Assy_List_SCRR.Add(new SuspensionCoordinatesRearRight(R1.scrrGUI[R1.scrrGUI.Count - 1]));
            SuspensionCoordinatesRearRight.Assy_List_SCRR[SuspensionCoordinatesRearRight.Assy_List_SCRR.Count - 1].RearSuspensionTyppe(R1.scrrGUI[R1.scrrGUI.Count - 1]);


        }

        private void AssignSuspensionToVehicle()
        {
            KO_Central.Vehicle.sc_FL = SuspensionCoordinatesFront.Assy_List_SCFL[SuspensionCoordinatesFront.Assy_List_SCFL.Count - 1];

            KO_Central.Vehicle.sc_FR = SuspensionCoordinatesFrontRight.Assy_List_SCFR[SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count - 1];

            KO_Central.Vehicle.sc_RL = SuspensionCoordinatesRear.Assy_List_SCRL[SuspensionCoordinatesRear.Assy_List_SCRL.Count - 1];

            KO_Central.Vehicle.sc_RR = SuspensionCoordinatesRearRight.Assy_List_SCRR[SuspensionCoordinatesRearRight.Assy_List_SCRR.Count - 1];
        }

        private void ModifySuspension(List<SuspensionCoordinatesMaster> _scm, List<SuspensionCoordinatesMasterGUI> _scm_GUI)
        {

        }

        #endregion


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

            xtraTabPageActuationPoints.PageEnabled = true;
        }

        /// <summary>
        /// Method to De-Activate all the TabPages
        /// </summary>
        private void Deactivate_AllTabPages()
        {
            xtraTabPageCornerParams.PageEnabled = false;

            xtraTabPageInboardPoints.PageEnabled = false;

            xtraTabPageBumpSteer.PageEnabled = false;

            xtraTabPageActuationPoints.PageEnabled = false;
        }

        /// <summary>
        /// Method to perform the GUI Operations of Hide incase of Symmetric Suspension
        /// </summary>
        /// <param name="_frontSymmetri"></param>
        /// <param name="_rearSymmetric"></param>
        private void SymmtryOperations()
        {
            layoutControlItemRC_LatOff_Front.HideToCustomization();

            layoutControlItemRC_LatOff_Rear.HideToCustomization();

            layoutControlItemFV_VSAL_FR.HideToCustomization();

            layoutControlItemFV_VSAL_RR.HideToCustomization();

            layoutControlItemPC_Right_Height.HideToCustomization();

            layoutControlItemPC_Right_LongOff.HideToCustomization();

            layoutControlItemSV_VSAL_FR.HideToCustomization();

            layoutControlItemSV_VSAL_RR.HideToCustomization();

            layoutControlItemPitman_Right.HideToCustomization();

            layoutControl_CornerParams_FR.Hide();

            layoutControl_CornerParams_RR.Hide();

            layoutControlItemwishboneInboardFR.HideToCustomization();
            layoutControlItemwishboneInboardFR.Height = 336;

            layoutControlItemwishboneInboardRR.HideToCustomization();

            simpleLabelItemwishboneInboardFR.HideToCustomization();

            simpleLabelItemwishboneInboardRR.HideToCustomization();

            layoutControlItembumpSteerCurveFR.HideToCustomization();

            layoutControlItemToeLinkLength.HideToCustomization();

            simpleLabelItem_BS_FR.HideToCustomization();

            layoutControlItemConvergence_BS_FR.HideToCustomization();

            layoutControlItemPlotToeLinkInboard_FR.HideToCustomization();

            layoutControlItemActuationPointsFR.HideToCustomization();

            layoutControlItemActuationPointsRR.HideToCustomization();

            simpleLabelItemActuationPoint_FR.HideToCustomization();

            simpleLabelItemActuationPoint_RR.HideToCustomization();
        }

        /// <summary>
        /// Method to perform the GUI operations of Show in case of Assymetric Suspension
        /// </summary>
        private void AssymmetryOperations()
        {
            layoutControlItemRC_LatOff_Front.ShowInCustomizationForm = true;

            layoutControlItemRC_LatOff_Rear.ShowInCustomizationForm = true;

            layoutControlItemFV_VSAL_FR.ShowInCustomizationForm = true;

            layoutControlItemFV_VSAL_RR.ShowInCustomizationForm = true;

            layoutControlItemPC_Right_Height.ShowInCustomizationForm = true;

            layoutControlItemPC_Right_LongOff.ShowInCustomizationForm = true;

            layoutControlItemSV_VSAL_FR.ShowInCustomizationForm = true;

            layoutControlItemSV_VSAL_RR.ShowInCustomizationForm = true;

            layoutControlItemPitman_Right.ShowInCustomizationForm = true;

            layoutControl_CornerParams_FR.Show();

            layoutControl_CornerParams_RR.Show();

            layoutControlItemwishboneInboardFR.ShowInCustomizationForm = true;
            layoutControlItemwishboneInboardFR.Height = 336;

            layoutControlItemwishboneInboardRR.ShowInCustomizationForm = true;

            simpleLabelItemwishboneInboardFR.ShowInCustomizationForm = true;

            simpleLabelItemwishboneInboardRR.ShowInCustomizationForm = true;

            layoutControlItembumpSteerCurveFR.ShowInCustomizationForm = true;

            layoutControlItemToeLinkLength.ShowInCustomizationForm = true;

            simpleLabelItem_BS_FR.ShowInCustomizationForm = true;

            layoutControlItemConvergence_BS_FR.ShowInCustomizationForm = true;

            layoutControlItemPlotToeLinkInboard_FR.ShowInCustomizationForm = true;

            layoutControlItemActuationPointsFR.ShowInCustomizationForm = true;

            layoutControlItemActuationPointsRR.ShowInCustomizationForm = true;

            simpleLabelItemActuationPoint_FR.ShowInCustomizationForm = true;

            simpleLabelItemActuationPoint_RR.ShowInCustomizationForm = true;
        }
        
        #endregion


        /// <summary>
        /// Event raised when the <see cref="simpleButtonUpdateSuspension_VehicleParams"/> is clicked. 
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateSuspension_VehicleParams_Click(object sender, EventArgs e)
        {
            AutoInit_VehicleParams();
        }
        /// <summary>
        /// Event raised when the <see cref="simpleButtonUpdateSuspension_CornerParams_FL"/> is clicked. 
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateSuspension_CornerParams_FL_Click(object sender, EventArgs e)
        {
            AutoInit_CornerParams_FL();
        }
        /// <summary>
        /// Event raised when the <see cref="simpleButtonUpdateSuspension_CornerParams_FR"/> is clicked. 
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateSuspension_CornerParams_FR_Click(object sender, EventArgs e)
        {
            AutoInit_CornerParams_FR();
        }
        /// <summary>
        /// Event raised when the <see cref="simpleButtonUpdateSuspension_CornerParams_RL"/> is clicked. 
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateSuspension_CornerParams_RL_Click(object sender, EventArgs e)
        {
            AutoInit_CornerParams_RL();
        }
        /// <summary>
        /// Event raised when the <see cref="simpleButtonUpdateSuspension_CornerParams_RR"/> is clicked. 
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateSuspension_CornerParams_RR_Click(object sender, EventArgs e)
        {
            AutoInit_CornerParams_RR();
        }


        /// <summary>
        /// Method which initiales the First Stage of the Suspension Geometry
        /// </summary>
        private void AutoInit_VehicleParams()
        {
            Set_Wheelbase();

            Set_TrackFront();

            Set_TrackRear();

            Set_RC_Front_Height();

            Set_RC_Front_LatOff();

            Set_FV_VSAL_FL();

            Set_FV_VSAL_FR();

            Set_RC_Rear_Height();

            Set_RC_Rear_LatOff();

            Set_FV_VSAL_RL();

            Set_FV_VSAL_RR();

            Set_PC_Left_Height();

            Set_PC_Left_LongitudinalOff();

            Set_SV_VSAL_FL();

            Set_SV_VSAL_RL();

            Set_PC_Right_Height();

            Set_PC_Right_LongitudinalOff();

            Set_SV_VSAL_FR();

            Set_SV_VSAL_RR();

            Set_Ackermann();
        }

        /// <summary>
        /// Method which initiales the Second Stage of the Suspension Geometry for the Front Left
        /// </summary>
        private void AutoInit_CornerParams_FL()
        {
            Set_KPI_FL();

            Set_Scrub_FL();

            Set_Caster_FL();

            Set_MechTrail_FL();

            Set_UBJParametric_FL();

            Set_LBJParametric_FL();

            Set_WheelCenter_FL();
        }

        /// <summary>
        /// Method which initiales the Second Stage of the Suspension Geometry for the Front Right
        /// </summary>
        private void AutoInit_CornerParams_FR()
        {
            Set_KPI_FR();

            Set_Scrub_FR();

            Set_Caster_FR();

            Set_MechTrail_FR();

            Set_UBJParametric_FR();

            Set_LBJParametric_FR();

            Set_WheelCenter_FR();
        }

        /// <summary>
        /// Method which initiales the Second Stage of the Suspension Geometry for the Rear Left
        /// </summary>
        private void AutoInit_CornerParams_RL()
        {
            Set_KPI_RL();

            Set_Scrub_RL();

            Set_Caster_RL();

            Set_MechTrail_RL();

            Set_UBJParametric_RL();

            Set_LBJParametric_RL();

            Set_WheelCenter_RL();
        }

        /// <summary>
        /// Method which initiales the Second Stage of the Suspension Geometry for the Rear RIght
        /// </summary>
        private void AutoInit_CornerParams_RR()
        {
            Set_KPI_RR();

            Set_Scrub_RR();

            Set_Caster_RR();

            Set_MechTrail_RR();

            Set_UBJParametric_RR();

            Set_LBJParametric_RR();

            Set_WheelCenter_RR();

        }



        private void wishboneInboardFR_Load(object sender, EventArgs e)
        {

        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    public partial class KO_ActuationPoints : XtraUserControl
    {

        KO_CentralVariables KO_Central;

        KO_CornverVariables KO_CV_Main;

        KO_CornverVariables KO_CV_Counter;

        DesignForm ParentObject;

        VehicleCorner VCorner_Main;

        VehicleCorner VCorner_Counter;

        bool Symmetry = false;

        #region --- Initialization Methods ---
        public KO_ActuationPoints()
        {
            InitializeComponent();

            Hide_DamperInboard_InputTB();
        }



        /// <summary>
        /// Method to Obtain the object of the <see cref="DesignForm"/> which is the Parent of this Form and Assign Parent Object of <see cref="actuationPointCompute'"/>
        /// </summary>
        /// <param name="_KOCentral"></param>
        /// <param name="_KO_CV"></param>
        /// <param name="_designForm"></param>
        public void Get_ParentObject_Data(ref KO_CentralVariables _KOCentral, ref KO_CornverVariables _KO_CV, DesignForm _designForm, VehicleCorner _vCorner)
        {
            KO_Central = _KOCentral;

            KO_CV_Main = _KO_CV;

            ParentObject = _designForm;

            VCorner_Main = _vCorner;

            Symmetry = false;

            actuationPointCompute.Get_ParentObjectData(ref KO_CV_Main, ParentObject, VCorner_Main, DevelopmentStages.ActuationPoints);

        }

        /// <summary>
        /// ---Used for Symmetry---
        /// Method to Obtain the object of the <see cref="DesignForm"/> which is the Parent of this Form and Assign Parent Object of <see cref="actuationPointCompute'"/>
        /// </summary>
        /// <param name="_KOCentral"></param>
        /// <param name="_KO_CV_Main"></param>
        /// <param name="_KO_CV_Counter"></param>
        /// <param name="_VCornerMain"></param>
        /// <param name="_VCornerCounter"></param>
        /// <param name="_designForm"></param>
        public void Get_ParentObject_Data(ref KO_CentralVariables _KOCentral, ref KO_CornverVariables _KO_CV_Main, ref KO_CornverVariables _KO_CV_Counter, VehicleCorner _VCornerMain,VehicleCorner _VCornerCounter, DesignForm _designForm)
        {
            KO_Central = _KOCentral;

            KO_CV_Main = _KO_CV_Main;
            KO_CV_Counter = _KO_CV_Counter;

            ParentObject = _designForm;

            VCorner_Main = _VCornerMain;
            VCorner_Counter = _VCornerCounter;

            Symmetry = true;

            actuationPointCompute.Get_ParentObjectData(ref _KO_CV_Main, ref _KO_CV_Counter, ParentObject, VCorner_Main, _VCornerCounter, DevelopmentStages.ActuationPoints);
        }

        #endregion
        
        #region --- Input Parameters Extraction and Computation methods---

        #region -- Validation Methods --


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

        #region -- Motion Ratio Definition --
        private void radioGroupMotionRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupMotionRatio.SelectedIndex == 0)
            {
                KO_CV_Main.MotionRatio_Format = MotionRatioFormat.WheelByShock;

                if (Symmetry)
                {
                    KO_CV_Counter.MotionRatio_Format = MotionRatioFormat.WheelByShock;
                }
            }
            else if (radioGroupMotionRatio.SelectedIndex == 1)
            {
                KO_CV_Main.MotionRatio_Format = MotionRatioFormat.ShockByWheel;

                if (Symmetry)
                {
                    KO_CV_Counter.MotionRatio_Format = MotionRatioFormat.ShockByWheel;
                }
            }
        }
        #endregion

        #region -- Motion Ratio --
        //---Motion Ratio---
        private void tbMR_Leave(object sender, EventArgs e)
        {
            Set_MotionRatio();
        }

        private void tbMR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_MotionRatio();
            }
        }

        private void Set_MotionRatio()
        {
            if (DoubleValidation(tbMR.Text))
            {
                if (Validatepositve_Double(tbMR.Text))
                {
                    KO_CV_Main.MotionRatio_Spring = Convert.ToDouble(tbMR.Text);

                    if (Symmetry)
                    {
                        KO_CV_Counter.MotionRatio_Spring = KO_CV_Main.MotionRatio_Spring;
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

        #region -- Rocker --

        #region - Rocker Axis -
        //---Rocker Axis i, j and k---

        //--Axis i--
        private void bRocker_Axis_I_Leave(object sender, EventArgs e)
        {
            Set_Rocker_Axis_I();
        }

        private void bRocker_Axis_I_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Rocker_Axis_I();
            }
        }

        private void Set_Rocker_Axis_I()
        {
            if (DoubleValidation(bRocker_Axis_I.Text))
            {
                KO_CV_Main.VCornerParams.Rocker_Axis_Vector.Z = Convert.ToDouble(bRocker_Axis_I.Text);

                KO_CV_Main.Compute_Plane(out KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center);

                if (Symmetry)
                {
                    KO_CV_Counter.VCornerParams.Rocker_Axis_Vector.Z = KO_CV_Main.VCornerParams.Rocker_Axis_Vector.Z;

                    KO_CV_Counter.Compute_Plane(out KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center);
                }

                Plot_Rocker_AxisAndCenter();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        //--Axis j--
        private void bRocker_Axis_J_Leave(object sender, EventArgs e)
        {
            Set_Rocker_Axis_J();
        }

        private void bRocker_Axis_J_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Rocker_Axis_J();
            }
        }

        private void Set_Rocker_Axis_J()
        {
            if (DoubleValidation(bRocker_Axis_J.Text))
            {
                KO_CV_Main.VCornerParams.Rocker_Axis_Vector.X = Convert.ToDouble(bRocker_Axis_J.Text);

                KO_CV_Main.Compute_Plane(out KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center);

                if (Symmetry)
                {
                    KO_CV_Counter.VCornerParams.Rocker_Axis_Vector.X = -KO_CV_Main.VCornerParams.Rocker_Axis_Vector.X;

                    KO_CV_Counter.Compute_Plane(out KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center);
                }

                Plot_Rocker_AxisAndCenter();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        //--Axis k--
        private void bRocker_Axis_K_Leave(object sender, EventArgs e)
        {
            Set_Rocker_Axis_K();
        }

        private void bRocker_Axis_K_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Rocker_Axis_K();
            }
        }

        private void Set_Rocker_Axis_K()
        {
            if (DoubleValidation(bRocker_Axis_K.Text))
                {
                KO_CV_Main.VCornerParams.Rocker_Axis_Vector.Y = Convert.ToDouble(bRocker_Axis_K.Text);

                KO_CV_Main.Compute_Plane(out KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center);

                if (Symmetry)
                {
                    KO_CV_Counter.VCornerParams.Rocker_Axis_Vector.Y = KO_CV_Main.VCornerParams.Rocker_Axis_Vector.Y;

                    KO_CV_Counter.Compute_Plane(out KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center);
                }

                Plot_Rocker_AxisAndCenter();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region - Rocker Center Point -
        //---Rocker Center X, Y and Z---

        //--Center X--

        private void tbRocker_Center_X_Leave(object sender, EventArgs e)
        {
            Set_Rocker_Center_X();
        }

        private void tbRocker_Center_X_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Rocker_Center_X();
            }
        }

        private void Set_Rocker_Center_X()
        {
            if (DoubleValidation(tbRocker_Center_X.Text))
            {
                KO_CV_Main.VCornerParams.Rocker_Center.Z = Convert.ToDouble(tbRocker_Center_X.Text);

                KO_CV_Main.Compute_Plane(out KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center);

                KO_CV_Main.VCornerParams.Initialize_Dictionary();

                if (Symmetry)
                {
                    KO_CV_Counter.VCornerParams.Rocker_Center.Z = KO_CV_Main.VCornerParams.Rocker_Center.Z;

                    KO_CV_Counter.Compute_Plane(out KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center);

                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();
                }

                Plot_Rocker_AxisAndCenter();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        //--Center Y--

        private void tbRocker_Center_Y_Leave(object sender, EventArgs e)
        {
            Set_Rocker_Center_Y();
        }

        private void tbRocker_Center_Y_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Rocker_Center_Y();
            }
        }

        private void Set_Rocker_Center_Y()
        {
            if (DoubleValidation(tbRocker_Center_Y.Text))
            {
                KO_CV_Main.VCornerParams.Rocker_Center.X = Convert.ToDouble(tbRocker_Center_Y.Text);

                KO_CV_Main.Compute_Plane(out KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center);

                KO_CV_Main.VCornerParams.Initialize_Dictionary();


                if (Symmetry)
                {
                    KO_CV_Counter.VCornerParams.Rocker_Center.X = -KO_CV_Main.VCornerParams.Rocker_Center.X;

                    KO_CV_Counter.Compute_Plane(out KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center);

                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();

                }

                Plot_Rocker_AxisAndCenter();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

        //--Center Z--

        private void tbRocker_Center_Z_Leave(object sender, EventArgs e)
        {
            Set_Rocker_Center_Z();
        }

        private void tbRocker_Center_Z_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Rocker_Center_Z();
            }
        }

        private void Set_Rocker_Center_Z()
        {
            if (DoubleValidation(tbRocker_Center_Z.Text))
            {
                KO_CV_Main.VCornerParams.Rocker_Center.Y = Convert.ToDouble(tbRocker_Center_Z.Text);

                KO_CV_Main.Compute_Plane(out KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center);

                KO_CV_Main.VCornerParams.Initialize_Dictionary();


                if (Symmetry)
                {
                    KO_CV_Counter.VCornerParams.Rocker_Center.Y = KO_CV_Main.VCornerParams.Rocker_Center.Y;

                    KO_CV_Counter.Compute_Plane(out KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center);

                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();

                }

                Plot_Rocker_AxisAndCenter();

            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        //-- END: Rocker --
        #endregion

        #region -- Damper --

        #region - Damper Inboard Format -
        //--- Damper Inboard Format ---
        private void radioGroupDamperInboardFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupDamperInboardFormat.SelectedIndex == 0)
            {
                if (!actuationPointCompute.listBoxControlSuspensionCoordinate.Items.Contains(CoordinateOptions.DamperShockMount.ToString()))
                {
                    actuationPointCompute.listBoxControlSuspensionCoordinate.Items.Add(CoordinateOptions.DamperShockMount.ToString());
                }

                Hide_DamperInboard_InputTB();
            }
            else if (radioGroupDamperInboardFormat.SelectedIndex == 1)
            {
                if (actuationPointCompute.listBoxControlSuspensionCoordinate.Items.Contains(CoordinateOptions.DamperShockMount.ToString()))
                {
                    actuationPointCompute.listBoxControlSuspensionCoordinate.Items.Remove(CoordinateOptions.DamperShockMount.ToString());
                }

                Show_DamperInboard_InputTB();
            }
        }
        #endregion

        #region - Damper Inboard Point - 
        //---Damper Inboard--

        //--Damper Inboard X--
        private void tbDamperInboard_X_Leave(object sender, EventArgs e)
        {
            Set_DamperInboard_X();
        }

        private void tbDamperInboard_X_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Set_DamperInboard_X()
        {
            if (DoubleValidation(tbDamperInboard_X.Text))
            {
                KO_CV_Main.VCornerParams.DamperBellCrank.Z = Convert.ToDouble(tbDamperInboard_X.Text);

                Plot_Damper_Inboard();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        //--Damper Inboard Y--
        private void tbDamperInboard_Y_Leave(object sender, EventArgs e)
        {
            Set_DamperInboard_Y();
        }

        private void tbDamperInboard_Y_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_DamperInboard_Y();
            }
        }

        private void Set_DamperInboard_Y()
        {
            if (DoubleValidation(tbDamperInboard_Y.Text))
            {
                KO_CV_Main.VCornerParams.DamperBellCrank.X = Convert.ToDouble(tbDamperInboard_Y.Text);

                Plot_Damper_Inboard();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        //--Damper Inboard Z--
        private void tbDamperInboard_Z_Leave(object sender, EventArgs e)
        {
            Set_DamperInboard_Z();
        }

        private void tbDamperInboard_Z_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Set_DamperInboard_Z()
        {
            if (DoubleValidation(tbDamperInboard_Z.Text))
            {
                KO_CV_Main.VCornerParams.DamperBellCrank.Y = Convert.ToDouble(tbDamperInboard_Z.Text);

                Plot_Damper_Inboard();
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        #region - Damper Static Length - 
        //---Damper Static Length

        private void tbDamperStaticLength_Leave(object sender, EventArgs e)
        {
            Set_DamperStaticLength();
        }

        private void tbDamperStaticLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_DamperStaticLength();
            }
        }

        private void Set_DamperStaticLength()
        {
            if (DoubleValidation(tbDamperStaticLength.Text))
            {
                KO_CV_Main.Damper_Length = Convert.ToDouble(tbDamperStaticLength.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }
        #endregion

        //-- END : Damper--
        #endregion

        //--- END : Input Parameters Extraction and Computation methods
        #endregion

        #region --- CAD Plottoer Methods ---

        /// <summary>
        /// Method to invoke the <see cref="Plot_Rocker_Axis"/> and <see cref="Plot_Rocker_Center"/> methods together
        /// </summary>
        private void Plot_Rocker_AxisAndCenter()
        {
            Plot_Rocker_Center();

            Plot_Rocker_Axis();
        }

        /// <summary>
        /// Method to Plot the Rocker Axis Line
        /// </summary>
        private void Plot_Rocker_Axis()
        {
            ParentObject.Plot_Axis(KO_CV_Main.VCornerParams.Rocker_Axis_Vector, KO_CV_Main.VCornerParams.Rocker_Center, Color.White, "Rocker_Axis_Vector" + VCorner_Main.ToString());

            if (Symmetry)
            {
                ParentObject.Plot_Axis(KO_CV_Counter.VCornerParams.Rocker_Axis_Vector, KO_CV_Counter.VCornerParams.Rocker_Center, Color.White, "Rocker_Axis_Vector" + VCorner_Counter.ToString());
            }
        }

        /// <summary>
        /// Method to Plot the Rocker Center Point
        /// </summary>
        private void Plot_Rocker_Center()
        {
            ParentObject.Plot_OutboardPoint(KO_CV_Main.VCornerParams.Rocker_Center, "KO_CV.VCornerParams.Rocker_Center" + VCorner_Main.ToString());

            if (Symmetry)
            {
                ParentObject.Plot_OutboardPoint(KO_CV_Counter.VCornerParams.Rocker_Center, "Rocker_Center" + VCorner_Counter.ToString());
            }
        }

        /// <summary>
        /// Method to Plot the Damper Inboard point
        /// </summary>
        private void Plot_Damper_Inboard()
        {
            ParentObject.Plot_OutboardPoint(KO_CV_Main.VCornerParams.DamperShockMount, "KO_CV.VCornerParams.DamperShockMount" + VCorner_Main.ToString());

            if (Symmetry)
            {
                ParentObject.Plot_OutboardPoint(KO_CV_Counter.VCornerParams.DamperShockMount, "DamperShockMount" + VCorner_Counter.ToString());
            }
        }

        #endregion

        #region ---GUI Operations---

        /// <summary>
        /// Method to HIDE the Textboxes which accept the Inboard Damper Point. 
        /// This is to be done in case the user selects <see cref="CoordinateAcceptanceFormat.Impose"/> for the <see cref="CoordinateOptions.DamperBellCrank"/>
        /// </summary>
        private void Hide_DamperInboard_InputTB()
        {
            simpleLabelItemInboardDamper.HideToCustomization();

            layoutControlItemInboardDamper_X.HideToCustomization();

            layoutControlItemInboardDamper_Y.HideToCustomization();

            layoutControlItemInboardDamper_Z.HideToCustomization();
        }

        /// <summary>
        /// Method to SHOW the Textboxes which accept the Inboard Damper Point. 
        /// This is to be done in case the user selects <see cref="CoordinateAcceptanceFormat.Compute"/> for the <see cref="CoordinateOptions.DamperBellCrank"/>
        /// </summary>
        private void Show_DamperInboard_InputTB()
        {
            simpleLabelItemInboardDamper.ShowInCustomizationForm = true;

            layoutControlItemInboardDamper_X.ShowInCustomizationForm = true;

            layoutControlItemInboardDamper_Y.ShowInCustomizationForm = true;

            layoutControlItemInboardDamper_Z.ShowInCustomizationForm = true;
        }


        #endregion
    }
}
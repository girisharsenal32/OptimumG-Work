using System;
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

        KO_CornverVariables KO_CV;

        DesignForm ParentObject;

        VehicleCorner VCorner;

        public KO_ActuationPoints()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Method to Obtain the object of the <see cref="DesignForm"/> which is the Parent of this Form
        /// </summary>
        /// <param name="_KOCentral"></param>
        /// <param name="_KO_CV"></param>
        /// <param name="_designForm"></param>
        public void Get_ParentObject_Data(ref KO_CentralVariables _KOCentral, ref KO_CornverVariables _KO_CV, DesignForm _designForm, VehicleCorner _vCorner)
        {
            KO_Central = _KOCentral;

            KO_CV = _KO_CV;

            ParentObject = _designForm;

            VCorner = _vCorner;

            actuationPoints.Get_ParentObjectData(KO_CV, ParentObject, VCorner, DevelopmentStages.ActuationPoints);
        }


        private void radioGroupMotionRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupMotionRatio.SelectedIndex == 0)
            {

            }
            else if (radioGroupMotionRatio.SelectedIndex == 1)
            {

            }
        }

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

        #region --- Input Parameters Extraction and Computation methods---


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
                    KO_CV.MotionRatio_Spring = Convert.ToDouble(tbMR.Text);
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
                KO_CV.Rocker_Axis.X = Convert.ToDouble(bRocker_Axis_I.Text);
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
                KO_CV.Rocker_Axis.Y = Convert.ToDouble(bRocker_Axis_J.Text);
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
                KO_CV.Rocker_Axis.Z = Convert.ToDouble(bRocker_Axis_K.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

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
                KO_CV.Rocker_Center.X = Convert.ToDouble(tbRocker_Center_X.Text);
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
                KO_CV.Rocker_Center.Y = Convert.ToDouble(tbRocker_Center_Y.Text);
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
                KO_CV.Rocker_Center.Z = Convert.ToDouble(tbRocker_Center_Z.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }

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
                KO_CV.Damper_Length = Convert.ToDouble(tbDamperStaticLength.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }




        private void radioGroupDamperInboardFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 
        #endregion





    }
}

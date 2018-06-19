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
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public partial class KO_DesignForm_AArmInboard : XtraUserControl
    {
        public KO_DesignForm_AArmInboard()
        {
            InitializeComponent();
        }

        private void Init_ListBox()
        {
            listBoxControlSuspensionCoordinate.Items.AddRange(new string[] {CoordinateOptions.UpperFront.ToString(), CoordinateOptions.UpperRear.ToString(),
                                                                            CoordinateOptions.LowerFront.ToString(),CoordinateOptions.LowerRear.ToString()});
        }

        public Point3D InboardPoint;

        public InboardInputFormat InboardFormat { get; set; }

        //public InboardInputFormat InboardFormat { get; set; }

        public KO_CornverVariables KO_CV { get; set; }

        ///// <summary>
        ///// Method to obtain the Inboard Pick-Up Point which is being Set by this UserCOntrol 
        ///// </summary>
        ///// <param name="_pointInboard">Inbord Point which is being Set using this UserControl</param>
        ///// <param name="_inboardFormat">The Format of Plotting the Inboard Pick Up Point</param>
        //public void Get_PointData(Point3D _pointInboard,InboardInputFormat _inboardFormat)
        //{
        //    InboardPoint = _pointInboard;

        //    InboardFormat = _inboardFormat;

        //}


        public void Get_CornerVariablesObject(KO_CornverVariables _koCV)
        {
            KO_CV = _koCV;
        }



        /// <summary>
        /// Event Fired when the <see cref="radioGroup1"/> Index is chnaged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///<summary>Assigning the Format of plotting the Inboard Point which the user selects</summary>
            if (radioGroup1.SelectedIndex == 0)
            {
                KO_CV.VCornerParams.InputFormat= InboardInputFormat.IIO;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                KO_CV.VCornerParams.InputFormat = InboardInputFormat.IOI;
            }
            else
            {
                KO_CV.VCornerParams.InputFormat = InboardInputFormat.OII;
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


        #region Setting the Inboard Pick Up Point
        //--- X Coordinate
        private void tbX_Leave(object sender, EventArgs e)
        {
            Set_X();
        }

        private void tbX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_X();
            }
        }

        private void Set_X()
        {
            if (DoubleValidation(tbX.Text))
            {
                InboardPoint.X = Convert.ToDouble(tbX.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        //--- Y Coordinate

        private void tbY_Leave(object sender, EventArgs e)
        {
            Set_Y();
        }

        private void tbY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Y();
            }
        }

        private void Set_Y()
        {
            if (DoubleValidation(tbY.Text))
            {
                InboardPoint.Y = Convert.ToDouble(tbY.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }


        //---Z Coordinate---

        private void tbZ_Leave(object sender, EventArgs e)
        {
            Set_Z();
        }

        private void tbZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Set_Z();
            }
        }

        private void Set_Z()
        {
            if (DoubleValidation(tbZ.Text))
            {
                InboardPoint.Z = Convert.ToDouble(tbZ.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






        #endregion



        private void listBoxControlSuspensionCoordinate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.UpperFront.ToString())
            {
                InboardPoint = KO_CV.VCornerParams.UpperFront;
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.UpperRear.ToString())
            {
                InboardPoint = KO_CV.VCornerParams.UpperRear;
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerFront.ToString())
            {
                InboardPoint = KO_CV.VCornerParams.LowerFront;
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerRear.ToString())
            {
                InboardPoint = KO_CV.VCornerParams.LowerRear;
            }

        }
    }
}

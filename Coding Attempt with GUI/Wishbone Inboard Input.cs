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

        public Point3D InboardPoint_Left;

        public string InboardPointName;

        public string WishboneArmName;

        public Point3D InboardPoint_Right;

        public InboardInputFormat InboardFormat { get; set; }

        public KO_CornverVariables KO_CV_Left { get; set; }

        public KO_CornverVariables KO_CV_Right;

        public DesignForm Design_Form;

        CoordinateOptions CurrentCoordinate;


        public void Get_ParentObjectData(KO_CornverVariables _koCV, DesignForm _designForm)
        {
            KO_CV_Left = _koCV;

            Design_Form = _designForm;
        }

        /// <summary>
        /// Overloaded Constructor to deal with Symmetry
        /// </summary>
        /// <param name="_koCVLeft"><see cref="KO_CornverVariables"/> object of the Left</param>
        /// <param name="_koCVRight"><see cref="KO_CornverVariables"/> object of the Right</param>
        /// <param name="_designForm">Object of the <see cref="DesignForm"/></param>
        public void Get_ParentObjectData(KO_CornverVariables _koCVLeft, KO_CornverVariables _koCVRight, DesignForm _designForm)
        {
            KO_CV_Left = _koCVLeft;

            KO_CV_Right = _koCVRight;

            Design_Form = _designForm;
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
                KO_CV_Left.VCornerParams.InputFormat= InboardInputFormat.IIO;

                ///<remarks>Corresponding GUI activiites to allow inputs to only X and Y coordinates</remarks>
                tbX.Enabled = true;

                tbY.Enabled = true;

                tbZ.Enabled = false;

            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                KO_CV_Left.VCornerParams.InputFormat = InboardInputFormat.IOI;

                ///<remarks>Corresponding GUI activiites to allow inputs to only X and Z coordinates</remarks>
                tbX.Enabled = true;

                tbY.Enabled = false;

                tbZ.Enabled = true;
            }
            else
            {
                KO_CV_Left.VCornerParams.InputFormat = InboardInputFormat.OII;

                ///<remarks>Corresponding GUI activiites to allow inputs to only Y and Z coordinates</remarks>
                tbX.Enabled = false;

                tbY.Enabled = true;

                tbZ.Enabled = true;
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
                InboardPoint_Left.X = Convert.ToDouble(tbX.Text);
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
                InboardPoint_Left.Y = Convert.ToDouble(tbY.Text);
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
                InboardPoint_Left.Z = Convert.ToDouble(tbZ.Text);
            }
            else
            {
                MessageBox.Show(NumericError);
            }
        }






        #endregion


        /// <summary>
        /// Event fired when the selected item in the <see cref="listBoxControlSuspensionCoordinate"/> is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxControlSuspensionCoordinate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.UpperFront.ToString())
            {
                InboardPoint_Left = KO_CV_Left.VCornerParams.UpperFront;

                InboardPointName = CoordinateOptions.UpperFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.UpperRear.ToString())
            {
                InboardPoint_Left = KO_CV_Left.VCornerParams.UpperRear;

                InboardPointName = CoordinateOptions.UpperFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerFront.ToString())
            {
                InboardPoint_Left = KO_CV_Left.VCornerParams.LowerFront;

                InboardPointName = CoordinateOptions.LowerFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerRear.ToString())
            {
                InboardPoint_Left = KO_CV_Left.VCornerParams.LowerRear;

                InboardPointName = CoordinateOptions.LowerRear.ToString();
            }

            CurrentCoordinate = (CoordinateOptions)listBoxControlSuspensionCoordinate.SelectedItem;

            Init_PointToTextbox(InboardPoint_Left);

            ///<remarks>The variable below will be used ONLY in case of Symmetry</remarks>
            InboardPoint_Right = new Point3D(-InboardPoint_Left.X, InboardPoint_Left.Y, InboardPoint_Left.Z);

        }


        /// <summary>
        /// Method to display the Points' Coordinates in the textboxes
        /// </summary>
        /// <param name="_inboardPoint"></param>
        private void Init_PointToTextbox(Point3D _inboardPoint)
        {
            if (_inboardPoint != null)
            {
                tbX.Text = _inboardPoint.X.ToString();

                tbY.Text = _inboardPoint.Y.ToString();

                tbZ.Text = _inboardPoint.Z.ToString(); 
            }

        }

        /// <summary>
        /// Event fired when the <see cref="simpleButtonPlotPoint"/> is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPlotPoint_Click(object sender, EventArgs e)
        {
            ComputePoint();
        }

        /// <summary>
        /// Method to Compute the Inboard Point based on the User's choice of <see cref="InboardInputFormat"/> and 2 Coordinate Inputs
        /// </summary>
        private void ComputePoint()
        {
            if (CurrentCoordinate == CoordinateOptions.UpperFront || CurrentCoordinate == CoordinateOptions.UpperRear)
            {
                KO_CV_Left.Compute_PointOnPlane(KO_CV_Left.VCornerParams.TopWishbonePlane, KO_CV_Left.VCornerParams.InputFormat, InboardPoint_Left);
                PlotPoint(InboardPoint_Left, KO_CV_Left.VCornerParams.UBJ, InboardPointName, InboardPointName);
                KO_CV_Left.VCornerParams.Initialize_Dictionary();

                if (KO_CV_Right != null)
                {
                    KO_CV_Right.Compute_PointOnPlane(KO_CV_Right.VCornerParams.TopWishbonePlane, KO_CV_Right.VCornerParams.InputFormat, InboardPoint_Right);
                    PlotPoint(InboardPoint_Right, KO_CV_Right.VCornerParams.UBJ, InboardPointName, InboardPointName);
                    KO_CV_Right.VCornerParams.Initialize_Dictionary();
                }


            }
            else if (CurrentCoordinate == CoordinateOptions.LowerFront || CurrentCoordinate == CoordinateOptions.LowerRear)
            {
                KO_CV_Left.Compute_PointOnPlane(KO_CV_Left.VCornerParams.BottomWishbonePlane, KO_CV_Left.VCornerParams.InputFormat, InboardPoint_Left);
                PlotPoint(InboardPoint_Left, KO_CV_Left.VCornerParams.LBJ, InboardPointName, InboardPointName);
                KO_CV_Left.VCornerParams.Initialize_Dictionary();

                if (KO_CV_Right != null)
                {
                    KO_CV_Right.Compute_PointOnPlane(KO_CV_Right.VCornerParams.BottomWishbonePlane, KO_CV_Right.VCornerParams.InputFormat, InboardPoint_Right);
                    PlotPoint(InboardPoint_Right, KO_CV_Right.VCornerParams.LBJ, InboardPointName, InboardPointName);
                    KO_CV_Right.VCornerParams.Initialize_Dictionary();
                }

            }
        }

        /// <summary>
        /// Method to Plot the Point once it has been initialized
        /// </summary>
        /// <param name="_inBoard">Inboard Pick Up Point which has just  been initializd</param>
        /// <param name="_outBoard">Corresponding Outboard Point</param>
        private void PlotPoint(Point3D _inBoard, Point3D _outBoard, string _inboardPointName, string _armname)
        {
            Design_Form.Plot_InboardPoints(_inBoard, _outBoard, _inboardPointName, _armname);
        }
    }
}

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

            Init_ListBox();

            InboardPoint_Main = new Point3D();

            InboardPoint_Counter = new Point3D();
        }

        private void Init_ListBox()
        {
            listBoxControlSuspensionCoordinate.Items.AddRange(new string[] {CoordinateOptions.UpperFront.ToString(), CoordinateOptions.UpperRear.ToString(),
                                                                            CoordinateOptions.LowerFront.ToString(),CoordinateOptions.LowerRear.ToString()});


            //object _sender,

            //listBoxControlSuspensionCoordinate_SelectedIndexChanged()

        }

        public Point3D InboardPoint_Main;

        public Point3D InboardPoint_Counter;

        public string InboardPointName;

        public string WishboneArmName;

        public InboardInputFormat InboardFormat { get; set; }

        public KO_CornverVariables KO_CV_Main { get; set; }

        public KO_CornverVariables KO_CV_Counter;

        VehicleCorner VCorner_Main;

        VehicleCorner VCorner_Counter;


        public DesignForm Design_Form;

        CoordinateOptions CurrentCoordinate;

        /// <summary>
        /// ---Used for Assymetric Suspension---
        /// Method to obtain the <see cref="KO_CornverVariables"/> object corresponding to the corner which is calling the functions of this class and the <see cref="DesignForm"/> object which is the parent of this UserControl
        /// </summary>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_designForm">Object of hte <see cref="DesignForm"/></param>
        public void Get_ParentObjectData(KO_CornverVariables _koCV, DesignForm _designForm, VehicleCorner _vCorner)
        {
            VCorner_Main = _vCorner;

            KO_CV_Main = _koCV;
            KO_CV_Main.VCornerParams.Initialize_Points();

            Design_Form = _designForm;

            Set_SelectedIndices();
        }

        /// <summary>
        /// ---Used for Symmetry---
        /// Overloaded method which performs the exact same function as the method above except that it accepts a Counter object of the <see cref="KO_CornverVariables"/> class
        /// Hence, if the Front Left <see cref="KO_CornverVariables"/> is passed then the counter <see cref="KO_CornverVariables"/> object of the Front Right must also be passed
        /// </summary>
        /// <param name="_koCVLeft"><see cref="KO_CornverVariables"/> object of the Left</param>
        /// <param name="_koCVRight"><see cref="KO_CornverVariables"/> object of the Right</param>
        /// <param name="_designForm">Object of the <see cref="DesignForm"/></param>
        public void Get_ParentObjectData(KO_CornverVariables _koCVLeft, KO_CornverVariables _koCVRight, DesignForm _designForm, VehicleCorner _vCorner, VehicleCorner _vCornerCounter)
        {
            VCorner_Main = _vCorner;

            VCorner_Counter = _vCornerCounter;

            KO_CV_Main = _koCVLeft;
            KO_CV_Main.VCornerParams.Initialize_Points();


            KO_CV_Counter = _koCVRight;
            KO_CV_Counter.VCornerParams.Initialize_Points();


            Design_Form = _designForm;

            Set_SelectedIndices();
        }

        private void Set_SelectedIndices()
        {
            listBoxControlSuspensionCoordinate.SelectedIndex = 0;

            radioGroup1.SelectedIndex = -1;
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
                KO_CV_Main.VCornerParams.InputFormat = InboardInputFormat.IIO;

                ///<remarks>
                ///Added here and inside each of the for loops because this Event is called during the construction of the <see cref="KO_DesignForm_AArmInboard"/> UserControl and at this time <see cref="KO_CV_Counter"/> is null
                /// </remarks>
                KO_CV_Counter.VCornerParams.InputFormat = KO_CV_Main.VCornerParams.InputFormat;

                ///<remarks>Corresponding GUI activiites to allow inputs to only X and Y coordinates</remarks>
                tbX.Enabled = true;

                tbY.Enabled = true;

                tbZ.Enabled = false;

            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                KO_CV_Main.VCornerParams.InputFormat = InboardInputFormat.IOI;

                ///<remarks>
                ///Added here and inside each of the for loops because this Event is called during the construction of the <see cref="KO_DesignForm_AArmInboard"/> UserControl and at this time <see cref="KO_CV_Counter"/> is null
                /// </remarks>
                KO_CV_Counter.VCornerParams.InputFormat = KO_CV_Main.VCornerParams.InputFormat;

                ///<remarks>Corresponding GUI activiites to allow inputs to only X and Z coordinates</remarks>
                tbX.Enabled = true;

                tbY.Enabled = false;

                tbZ.Enabled = true;
            }
            else if (radioGroup1.SelectedIndex == 2) 
            {
                KO_CV_Main.VCornerParams.InputFormat = InboardInputFormat.OII;

                ///<remarks>
                ///Added here and inside each of the for loops because this Event is called during the construction of the <see cref="KO_DesignForm_AArmInboard"/> UserControl and at this time <see cref="KO_CV_Counter"/> is null
                /// </remarks>
                KO_CV_Counter.VCornerParams.InputFormat = KO_CV_Main.VCornerParams.InputFormat;

                ///<remarks>Corresponding GUI activiites to allow inputs to only Y and Z coordinates</remarks>
                tbX.Enabled = false;

                tbY.Enabled = true;

                tbZ.Enabled = true;
            }
            else
            {
                tbX.Enabled = false;

                tbY.Enabled = false;

                tbZ.Enabled = false;
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
                InboardPoint_Main.X = Convert.ToDouble(tbX.Text);

                InboardPoint_Counter.X = -InboardPoint_Main.X;
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
                InboardPoint_Main.Y = Convert.ToDouble(tbY.Text);

                InboardPoint_Counter.Y = InboardPoint_Main.Y;

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
                InboardPoint_Main.Z = Convert.ToDouble(tbZ.Text);

                InboardPoint_Counter.Z = InboardPoint_Main.Z;

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
                InboardPoint_Main = KO_CV_Main.VCornerParams.UpperFront;

                InboardPointName = CoordinateOptions.UpperFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.UpperRear.ToString())
            {
                InboardPoint_Main = KO_CV_Main.VCornerParams.UpperRear;

                InboardPointName = CoordinateOptions.UpperRear.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerFront.ToString())
            {
                InboardPoint_Main = KO_CV_Main.VCornerParams.LowerFront;

                InboardPointName = CoordinateOptions.LowerFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerRear.ToString())
            {
                InboardPoint_Main = KO_CV_Main.VCornerParams.LowerRear;

                InboardPointName = CoordinateOptions.LowerRear.ToString();
            }

            Set_CurrentCoordinate();

            Init_PointToTextbox(InboardPoint_Main);

            ///<remarks>The variable below will be used ONLY in case of Symmetry</remarks>
            InboardPoint_Counter = new Point3D(-InboardPoint_Main.X, InboardPoint_Main.Y, InboardPoint_Main.Z);

        }

        /// <summary>
        /// Method to determine whch of the <see cref="CoordinateOptions"/> is the current coordinate from the <see cref="listBoxControlSuspensionCoordinate"/> and 
        /// set it to the <see cref="CurrentCoordinate"/> object 
        /// </summary>
        private void Set_CurrentCoordinate()
        {
            Array coordOptions = Enum.GetValues(typeof(CoordinateOptions));

            for (int i = 0; i < coordOptions.Length; i++)
            {
                if (coordOptions.GetValue(i).ToString() == (string)listBoxControlSuspensionCoordinate.SelectedItem)
                {
                    CurrentCoordinate = (CoordinateOptions)i;
                }
            }
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
                KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.TopWishbonePlane, KO_CV_Main.VCornerParams.InputFormat, InboardPoint_Main);
                PlotPoint(InboardPoint_Main, KO_CV_Main.VCornerParams.UBJ, InboardPointName + VCorner_Main.ToString(), InboardPointName + VCorner_Main.ToString());
                KO_CV_Main.VCornerParams.Initialize_Dictionary();

                if (KO_CV_Counter != null)
                {
                    KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.TopWishbonePlane, KO_CV_Counter.VCornerParams.InputFormat, InboardPoint_Counter);
                    PlotPoint(InboardPoint_Counter, KO_CV_Counter.VCornerParams.UBJ, InboardPointName + VCorner_Counter.ToString(), InboardPointName + VCorner_Counter.ToString());
                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();
                }


            }
            else if (CurrentCoordinate == CoordinateOptions.LowerFront || CurrentCoordinate == CoordinateOptions.LowerRear)
            {
                KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.BottomWishbonePlane, KO_CV_Main.VCornerParams.InputFormat, InboardPoint_Main);
                PlotPoint(InboardPoint_Main, KO_CV_Main.VCornerParams.LBJ, InboardPointName + VCorner_Main.ToString(), InboardPointName + VCorner_Main.ToString());
                KO_CV_Main.VCornerParams.Initialize_Dictionary();

                if (KO_CV_Counter != null)
                {
                    KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.BottomWishbonePlane, KO_CV_Counter.VCornerParams.InputFormat, InboardPoint_Counter);
                    PlotPoint(InboardPoint_Counter, KO_CV_Counter.VCornerParams.LBJ, InboardPointName + VCorner_Counter.ToString(), InboardPointName + VCorner_Counter.ToString());
                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();
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

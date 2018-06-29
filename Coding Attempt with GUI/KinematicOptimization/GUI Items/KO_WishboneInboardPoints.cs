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
    public partial class KO_WishbonePointsCalculator : XtraUserControl
    {

        public DevelopmentStages CurrentDevStage { get; set; } = DevelopmentStages.ActuationPoints;

        

        public Point3D Point_Main = new Point3D();

        public Point3D Point_Counter = new Point3D();

        //public Point3D OutboardPoint_Main = new Point3D();

        //public Point3D OutboarrdPoint_Counter = new Point3D();

        public string PointName;

        //public string OutboardPointName;

        public string WishboneArmName;

        public CoordinateInputFormat InputFormat { get; set; }

        public KO_CornverVariables KO_CV_Main;

        public KO_CornverVariables KO_CV_Counter;

        VehicleCorner VCorner_Main;

        VehicleCorner VCorner_Counter;

        public DesignForm Design_Form;

        CoordinateOptions CurrentCoordinate;

        bool SymmetricSuspension = false;

        #region ---Initialization Methods---
        public KO_WishbonePointsCalculator()
        {
            InitializeComponent();

            if (CurrentDevStage == DevelopmentStages.ActuationPoints)
            {
                Init_ListBox_WishboneInboardPoints();
            }

        }

        /// <summary>
        /// ---Used for Assymetric Suspension---
        /// Method to obtain the <see cref="KO_CornverVariables"/> object corresponding to the corner which is calling the functions of this class and the <see cref="DesignForm"/> object which is the parent of this UserControl
        /// </summary>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_designForm">Object of hte <see cref="DesignForm"/></param>
        public void Get_ParentObjectData(ref KO_CornverVariables _koCV, DesignForm _designForm, VehicleCorner _vCorner, DevelopmentStages _devStage)
        {
            VCorner_Main = _vCorner;

            KO_CV_Main = _koCV;
            KO_CV_Main.VCornerParams.Initialize_Points();

            Design_Form = _designForm;

            Set_SelectedIndices();

            CurrentDevStage = _devStage;

            UpdateListBox();

            SymmetricSuspension = false;

        }

        /// <summary>
        /// Method to Update the <see cref="listBoxControlSuspensionCoordinate"/> based on the <see cref="CurrentDevStage"/>
        /// </summary>
        private void UpdateListBox()
        {
            

            if (CurrentDevStage == DevelopmentStages.ActuationPoints)
            {
                Init_Listbox_Actuationpoints();
            }
            else if (CurrentDevStage == DevelopmentStages.WishboneInboardPoints)
            {
                Init_ListBox_WishboneInboardPoints();
            }
        }

        /// <summary>
        /// ---Used for Symmetry---
        /// Overloaded method which performs the exact same function as the method above except that it accepts a Counter object of the <see cref="KO_CornverVariables"/> class
        /// Hence, if the Front Left <see cref="KO_CornverVariables"/> is passed then the counter <see cref="KO_CornverVariables"/> object of the Front Right must also be passed
        /// </summary>
        /// <param name="_koCVLeft"><see cref="KO_CornverVariables"/> object of the Left</param>
        /// <param name="_koCVRight"><see cref="KO_CornverVariables"/> object of the Right</param>
        /// <param name="_designForm">Object of the <see cref="DesignForm"/></param>
        public void Get_ParentObjectData(ref KO_CornverVariables _koCVLeft, ref KO_CornverVariables _koCVRight, DesignForm _designForm, VehicleCorner _vCorner, VehicleCorner _vCornerCounter, DevelopmentStages _devStage)
        {
            VCorner_Main = _vCorner;
            VCorner_Counter = _vCornerCounter;

            KO_CV_Main = _koCVLeft;
            KO_CV_Main.VCornerParams.Initialize_Points();


            KO_CV_Counter = _koCVRight;
            KO_CV_Counter.VCornerParams.Initialize_Points();


            Design_Form = _designForm;

            Set_SelectedIndices();

            CurrentDevStage = _devStage;

            UpdateListBox();

            SymmetricSuspension = true;
        }

        /// <summary>
        /// Method to clear the selection from the <see cref="listBoxControlSuspensionCoordinate"/> and <see cref="radioGroup1"/>
        /// </summary>
        private void Set_SelectedIndices()
        {
            listBoxControlSuspensionCoordinate.SelectedIndex = 0;

            radioGroup1.SelectedIndex = -1;
        }

        /// <summary>
        /// Method to Initialize the <see cref="listBoxControlSuspensionCoordinate"/> with the Wishbone Inboard Points. 
        /// Used when the <see cref="CurrentDevStage"/> is <see cref="DevelopmentStages.WishboneInboardPoints"/>
        /// </summary>
        private void Init_ListBox_WishboneInboardPoints()
        {
            listBoxControlSuspensionCoordinate.Items.Clear();

            listBoxControlSuspensionCoordinate.Items.AddRange(new string[] {CoordinateOptions.UpperFront.ToString(), CoordinateOptions.UpperRear.ToString(),
                                                                            CoordinateOptions.LowerFront.ToString(),CoordinateOptions.LowerRear.ToString()});
        }

        /// <summary>
        /// Method to Initialize the <see cref="listBoxControlSuspensionCoordinate"/> with the Actuation Points. 
        /// Used when the <see cref="CurrentDevStage"/> is <see cref="DevelopmentStages.ActuationPoints"/>
        /// </summary>
        public void Init_Listbox_Actuationpoints()
        {
            listBoxControlSuspensionCoordinate.Items.Clear();

            listBoxControlSuspensionCoordinate.Items.AddRange(new string[] { CoordinateOptions.PushrodOutboard.ToString(), CoordinateOptions.DamperShockMount.ToString(), CoordinateOptions.ARBLeverEndPoint.ToString() });
        }
        #endregion



        /// <summary>
        /// Event Fired when the <see cref="radioGroup1"/> Index is chnaged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///<summary>Assigning the Format of plotting the Inboard Point which the user selects</summary>
            

            ///<remarks>
            ///---IMPORTANT---
            ///
            /// Remember that I am converting from User's Vehicle Coordinate System to my COordinate System
            /// Hence, If user selects XIn YIn Zout the he means he would like to Input the Long and Lateral Coordinates
            /// For me the above translates to Input of Lat and Verticial which is ZIn and XIn which is <see cref="CoordinateInputFormat.IOI"/>
            /// </remarks>
            if (radioGroup1.SelectedIndex == 0)
            {
                KO_CV_Main.VCornerParams.InputFormat = CoordinateInputFormat.IOI;

                ///<remarks>
                ///Added here and inside each of the for loops because this Event is called during the construction of the <see cref="KO_WishbonePointsCalculator"/> UserControl and at this time <see cref="KO_CV_Counter"/> is null
                /// </remarks>
                KO_CV_Counter.VCornerParams.InputFormat = KO_CV_Main.VCornerParams.InputFormat;

                ///<remarks>Corresponding GUI activiites to allow inputs to only X and Y coordinates</remarks>
                tbX.Enabled = true;

                tbY.Enabled = true;

                tbZ.Enabled = false;
                tbZ.Clear();
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                KO_CV_Main.VCornerParams.InputFormat = CoordinateInputFormat.OII;

                ///<remarks>
                ///Added here and inside each of the for loops because this Event is called during the construction of the <see cref="KO_WishbonePointsCalculator"/> UserControl and at this time <see cref="KO_CV_Counter"/> is null
                /// </remarks>
                KO_CV_Counter.VCornerParams.InputFormat = KO_CV_Main.VCornerParams.InputFormat;

                ///<remarks>Corresponding GUI activiites to allow inputs to only X and Z coordinates</remarks>
                tbX.Enabled = true;

                tbY.Enabled = false;
                tbY.Clear();

                tbZ.Enabled = true;
            }
            else if (radioGroup1.SelectedIndex == 2) 
            {
                KO_CV_Main.VCornerParams.InputFormat = CoordinateInputFormat.IIO;

                ///<remarks>
                ///Added here and inside each of the for loops because this Event is called during the construction of the <see cref="KO_WishbonePointsCalculator"/> UserControl and at this time <see cref="KO_CV_Counter"/> is null
                /// </remarks>
                KO_CV_Counter.VCornerParams.InputFormat = KO_CV_Main.VCornerParams.InputFormat;

                ///<remarks>Corresponding GUI activiites to allow inputs to only Y and Z coordinates</remarks>
                tbX.Enabled = false;
                tbX.Clear();

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


        #region Setting the Pick Up Point
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
                Point_Main.Z = Convert.ToDouble(tbX.Text);

                Point_Counter.Z = Point_Main.Z;
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
                Point_Main.X = Convert.ToDouble(tbY.Text);

                Point_Counter.X = -Point_Main.X;

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
                Point_Main.Y = Convert.ToDouble(tbZ.Text);

                Point_Counter.Y = Point_Main.Y;

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
                Point_Main = KO_CV_Main.VCornerParams.UpperFront;

                if (SymmetricSuspension)
                {
                    ///<remarks>The variables below will be used ONLY in case of Symmetry</remarks>
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.UpperFront; 
                }

                PointName = CoordinateOptions.UpperFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.UpperRear.ToString())
            {
                Point_Main = KO_CV_Main.VCornerParams.UpperRear;

                if (SymmetricSuspension)
                {
                    ///<remarks>The variables below will be used ONLY in case of Symmetry</remarks>
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.UpperRear;
                }

                PointName = CoordinateOptions.UpperRear.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerFront.ToString())
            {
                Point_Main = KO_CV_Main.VCornerParams.LowerFront;

                if (SymmetricSuspension)
                {
                    ///<remarks>The variables below will be used ONLY in case of Symmetry</remarks>
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.LowerFront;
                }

                PointName = CoordinateOptions.LowerFront.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.LowerRear.ToString())
            {
                Point_Main = KO_CV_Main.VCornerParams.LowerRear;

                if (SymmetricSuspension)
                {
                    ///<remarks>The variables below will be used ONLY in case of Symmetry</remarks>
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.LowerRear;
                }

                PointName = CoordinateOptions.LowerRear.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.DamperShockMount.ToString())
            {
                Point_Main = KO_CV_Main.VCornerParams.DamperShockMount;

                if (SymmetricSuspension)
                {
                    ///<remarks>The variables below will be used ONLY in case of Symmetry</remarks>
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.DamperShockMount;
                }

                PointName = CoordinateOptions.DamperShockMount.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.PushrodOutboard.ToString())
            {
                Point_Main = KO_CV_Main.VCornerParams.PushrodOutboard;

                if (SymmetricSuspension)
                {
                    ///<remarks>The variables below will be used ONLY in case of Symmetry</remarks>
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.PushrodOutboard;
                }

                PointName = CoordinateOptions.PushrodOutboard.ToString();
            }
            else if ((string)listBoxControlSuspensionCoordinate.SelectedItem == CoordinateOptions.ARBLeverEndPoint.ToString())
            {
                Point_Main = KO_CV_Main.VCornerParams.ARB_DroopLink_LeverPoint;

                if (SymmetricSuspension)
                {
                    Point_Counter = new Point3D(-Point_Main.X, Point_Main.Y, Point_Main.Z);

                    Point_Counter = KO_CV_Counter.VCornerParams.ARB_DroopLink_LeverPoint; 
                }

                PointName = CoordinateOptions.ARBLeverEndPoint.ToString();
            }

            Set_CurrentCoordinate();

            Init_PointToTextbox(Point_Main);
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
        /// <param name="_point"></param>
        private void Init_PointToTextbox(Point3D _point)
        {
            if (_point != null)
            {
                tbX.Text = _point.Z.ToString();

                tbY.Text = _point.X.ToString();

                tbZ.Text = _point.Y.ToString(); 
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
        /// Method to Compute the Inboard Point based on the User's choice of <see cref="CoordinateInputFormat"/> and 2 Coordinate Inputs
        /// </summary>
        private void ComputePoint()
        {
            if (CurrentCoordinate == CoordinateOptions.UpperFront || CurrentCoordinate == CoordinateOptions.UpperRear)
            {
                Point_Main = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.TopWishbonePlane, KO_CV_Main.VCornerParams.InputFormat, Point_Main);
                Plot_Point(Point_Main, KO_CV_Main.VCornerParams.UBJ, PointName + VCorner_Main.ToString(), PointName + VCorner_Main.ToString());
                KO_CV_Main.VCornerParams.Initialize_Dictionary();

                if (SymmetricSuspension)
                {
                    Point_Counter = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.TopWishbonePlane, KO_CV_Counter.VCornerParams.InputFormat, Point_Counter);
                    Plot_Point(Point_Counter, KO_CV_Counter.VCornerParams.UBJ, PointName + VCorner_Counter.ToString(), PointName + VCorner_Counter.ToString());

                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();

                }


            }
            else if (CurrentCoordinate == CoordinateOptions.LowerFront || CurrentCoordinate == CoordinateOptions.LowerRear)
            {
                Point_Main = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.BottomWishbonePlane, KO_CV_Main.VCornerParams.InputFormat, Point_Main);
                Plot_Point(Point_Main, KO_CV_Main.VCornerParams.LBJ, PointName + VCorner_Main.ToString(), PointName + VCorner_Main.ToString());
                KO_CV_Main.VCornerParams.Initialize_Dictionary();

                if (SymmetricSuspension)
                {
                    Point_Counter = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.BottomWishbonePlane, KO_CV_Counter.VCornerParams.InputFormat, Point_Counter);
                    Plot_Point(Point_Counter, KO_CV_Counter.VCornerParams.LBJ, PointName + VCorner_Counter.ToString(), PointName + VCorner_Counter.ToString());

                    KO_CV_Counter.VCornerParams.Initialize_Dictionary();

                }
            }

            else if (CurrentCoordinate == CoordinateOptions.DamperShockMount)
            {
                Compute_DamperShockMount();
            }
            else if (CurrentCoordinate == CoordinateOptions.PushrodOutboard)
            {
                Compute_PushrodOutboard();
            }

            else if (CurrentCoordinate == CoordinateOptions.ARBLeverEndPoint)
            {
                Compute_ARBLeverEnd();
            }


            KO_CV_Main.VCornerParams.Initialize_Dictionary();

            if (KO_CV_Counter != null) 
            {
                KO_CV_Counter.VCornerParams.Initialize_Dictionary();
            }
        }


        /// <summary>
        /// Method to compute the <see cref="CoordinateOptions.DamperShockMount"/> using the input coordinates and the <see cref="CoordinateInputFormat"/>
        /// </summary>
        public void Compute_DamperShockMount()
        {
            Point_Main = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.InputFormat, Point_Main);

            ///<summary>Getting an Approx start point for the <see cref="CoordinateOptions.DamperBellCrank"/></summary>
            KO_CV_Main.VCornerParams.DamperBellCrank = KO_CV_Main.Compute_ApproxCorrespondingPoint_FromLinkLength(-KO_CV_Main.Damper_Length, KO_CV_Main.VCornerParams.DamperShockMount, VCorner_Main);
            ///<summary>Getting the proper guess of the <see cref="CoordinateOptions.DamperBellCrank"/> using the plane equations</summary>
            KO_CV_Main.VCornerParams.DamperBellCrank = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, KO_CV_Main.VCornerParams.DamperBellCrank);

            Plot_Point(Point_Main, PointName + VCorner_Main.ToString());

            KO_CV_Main.VCornerParams.Initialize_Dictionary();

            if (SymmetricSuspension)
            {
                Point_Counter = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.InputFormat, Point_Counter);

                ///<summary>Getting an Approx start point for the <see cref="CoordinateOptions.DamperBellCrank"/></summary>
                KO_CV_Counter.VCornerParams.DamperBellCrank = KO_CV_Counter.Compute_ApproxCorrespondingPoint_FromLinkLength(KO_CV_Counter.Damper_Length, KO_CV_Counter.VCornerParams.DamperShockMount, VCorner_Counter);
                ///<summary>Getting the proper guess of the <see cref="CoordinateOptions.DamperBellCrank"/> using the plane equations</summary>
                KO_CV_Counter.VCornerParams.DamperBellCrank = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, KO_CV_Counter.VCornerParams.DamperBellCrank);

                Plot_Point(Point_Counter, PointName + VCorner_Counter.ToString());

                KO_CV_Counter.VCornerParams.Initialize_Dictionary();

            }
        }

        /// <summary>
        /// Method to compute the <see cref="CoordinateOptions.PushrodOutboard"/> using the input coordinates and the <see cref="CoordinateInputFormat"/>
        /// </summary>
        public void Compute_PushrodOutboard()
        {
            Point_Main = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.InputFormat, Point_Main);

            ///<summary>Getting an Approx Start Point for the <see cref="CoordinateOptions.PushrodInboard"/></summary>
            KO_CV_Main.VCornerParams.PushrodInboard = KO_CV_Main.Compute_ApproxCorrespondingPoint_FromLinkLength(350, KO_CV_Main.VCornerParams.PushrodOutboard, VCorner_Main);
            ///<summary>Getting the proper guess of the <see cref="CoordinateOptions.DamperBellCrank"/> using the plane equations</summary>
            KO_CV_Main.VCornerParams.PushrodInboard = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, KO_CV_Main.VCornerParams.PushrodInboard);

            Plot_Point(Point_Main, PointName + VCorner_Main.ToString());

            KO_CV_Main.VCornerParams.Initialize_Dictionary();

            if (SymmetricSuspension)
            {
                Point_Counter = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.InputFormat, Point_Counter);

                ///<summary>Getting an Approx Start Point for the <see cref="CoordinateOptions.PushrodInboard"/></summary>
                KO_CV_Counter.VCornerParams.PushrodInboard = KO_CV_Counter.Compute_ApproxCorrespondingPoint_FromLinkLength(380, KO_CV_Counter.VCornerParams.PushrodOutboard, VCorner_Counter);
                ///<summary>Getting the proper guess of the <see cref="CoordinateOptions.DamperBellCrank"/> using the plane equations</summary>
                KO_CV_Counter.VCornerParams.PushrodInboard = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, KO_CV_Counter.VCornerParams.PushrodInboard);

                Plot_Point(Point_Counter, PointName + VCorner_Counter.ToString());

                KO_CV_Counter.VCornerParams.Initialize_Dictionary();
            }
        }

        /// <summary>
        /// Method to compute the <see cref="CoordinateOptions.ARBLeverEndPoint"/> using the input coordinates and the <see cref="CoordinateInputFormat"/>
        /// </summary>
        public void Compute_ARBLeverEnd()
        {
            Point_Main = KO_CV_Main.Compute_PointOnPlane(KO_CV_Main.VCornerParams.RockerPlane, KO_CV_Main.VCornerParams.InputFormat, Point_Main);

            Plot_Point(Point_Main, PointName + VCorner_Main.ToString());

            Design_Form.Plot_InboardPoints(Point_Main, KO_CV_Main.VCornerParams.ARB_EndPoint_Chassis, PointName + VCorner_Main.ToString(), "ARB Lever" + VCorner_Main.ToString());

            KO_CV_Main.VCornerParams.Initialize_Dictionary();

            if (SymmetricSuspension)
            {
                Point_Counter = KO_CV_Counter.Compute_PointOnPlane(KO_CV_Counter.VCornerParams.RockerPlane, KO_CV_Counter.VCornerParams.InputFormat, Point_Counter);

                Plot_Point(Point_Counter, PointName + VCorner_Counter.ToString());

                Design_Form.Plot_InboardPoints(Point_Counter, KO_CV_Counter.VCornerParams.ARB_EndPoint_Chassis, PointName + VCorner_Counter.ToString(), "ARB Lever" + VCorner_Counter.ToString());

                KO_CV_Counter.VCornerParams.Initialize_Dictionary();
            }
        }

        /// <summary>
        /// ---Used when Inboard and it's corresponding Outboard points are already known---
        /// Method to Plot the Point once it has been initialized
        /// </summary>
        /// <param name="_inBoard">Inboard Pick Up Point which has just  been initializd</param>
        /// <param name="_outBoard">Corresponding Outboard Point</param>
        private void Plot_Point(Point3D _inBoard, Point3D _outBoard, string _pointName, string _armname)
        {
            Design_Form.Plot_InboardPoints(_inBoard, _outBoard, _pointName, _armname);
        }

        /// <summary>
        /// ---Used when only the Inboard Point is known---
        /// Method to Plot a Point 
        /// </summary>
        /// <param name="_point">Point to be plotted</param>
        /// <param name="_pointName">Name of the Point</param>
        private void Plot_Point(Point3D _point, string _pointName)
        {
            Design_Form.Plot_OutboardPoint(_point, _pointName);
        }

        
    }
}

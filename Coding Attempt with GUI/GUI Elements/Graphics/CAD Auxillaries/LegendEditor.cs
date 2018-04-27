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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace Coding_Attempt_with_GUI
{
    public partial class LegendEditor : XtraForm
    {
        /// <summary>
        /// <see cref="DataTable"/> which will be used as source for the <see cref=gridControl1"/>
        /// </summary>
        public DataTable LegendDataTable { get; set; }
        /// <summary>
        /// Parent <see cref="CAD"/> object
        /// </summary>
        public CAD ParentCAD { get; set; }
        /// <summary>
        /// User defined Maximum Value of the Legend 
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// User defined Minimum Value of the Legend 
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// User defined Colour Upper Colour Gradient of the Legend 
        /// </summary>
        public Color UsersGradient1 { get; set; } = Color.Maroon;
        /// <summary>
        /// User defined Colour Lower Colour Gradient of the Legend. Default <see cref="Color.White"/> which is auto-selected 
        /// </summary>
        public Color UsersGradient2 { get; set; } = Color.White;
        /// <summary>
        /// User defined Colour for Bars without a Force Value. Default <see cref="Color.White"/> which is auto-selected
        /// </summary>
        public Color UserNoForceWishboneColour { get; set; } = Color.White;
        /// <summary>
        /// Number of Steps in the Legend
        /// </summary>
        public int NoOfSteps { get; set; }
        /// <summary>
        /// Step Size of the Steps in the Legned
        /// </summary>
        public double StepSize { get; set; }
        /// <summary>
        /// User defined <see cref="GradientStyle"/> of the Legend
        /// </summary>
        public GradientStyle UsersGradientStyle { get; set; } = GradientStyle.StandardFEM;
        /// <summary>
        /// User defined <see cref="ForceArrowStyle"/>. 
        /// </summary>
        public ForceArrowStyle UserForceArrowStyle { get; set; } = ForceArrowStyle.Both;
        /// <summary>
        /// Length of the Arrow CYLINDER if the user opts for <see cref="ForceArrowStyle.ColourScaling"/>
        /// </summary>
        public double ArrowNoForceCylLength { get; set; } = 50;
        /// <summary>
        /// Length of the Arrow CONE. This will be constant and no scaling will be done on this. 
        /// </summary>
        public double ArrowConstantConeLength { get; set; }
        /// <summary>
        /// Radius of the Arrow CYLINDER. This will be constant and no scaling will be done on this. 
        /// </summary>
        public double ArrowConstantCylRadius { get; set; }
        /// <summary>
        /// Radius of the Arrow CONE. This will be constant and no scaling will be done on this. 
        /// </summary>
        public double ArrowConeRadius { get; set; }
        /// <summary>
        /// Color of the Arrow if the user opts for <see cref="ForceArrowStyle.LengthScaling"/>
        /// </summary>
        public Color ArrowNoForceColor { get; set; }
        /// <summary>
        /// <see cref="Boolean"/> which determine whether the Eyeshot Control will colour the Wishbones according to their Force
        /// </summary> 
        public bool DisplayWishboneForces { get; set; } = true;
        /// <summary>
        /// <see cref="String"/> of Force Categories to be displayed. If there is a Category Name inside this list then it means the user has it from the <see cref="checkedListBoxControlDisplayOptions"/>
        /// </summary>
        public List<string> ForcesToBeDisplayed { get; set; }
        /// <summary>
        /// Base or Primary <see cref="OutputClass"/> which is used to populate the Legend in ONLY 2 conditions. First time (Initialization) and Reset or (Re-Initialization)
        /// </summary>
        public OutputClass BaseOC { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LegendEditor()
        {
            InitializeComponent();

            radioGroupGradientStyle.SelectedIndex = -1;

            radioGroupLegendParams.SelectedIndex = -1;

            radioGroupForceArrowOptions.SelectedIndex = -1;

            ForcesToBeDisplayed = new List<string>(new string[] { "Wishbone Force", "Wishbone Decomposition Forces", "Bearing Cap Forces" });

        }

        /// <summary>
        /// Public Inititalizer method which Performs all the Init activites required for a fully functioning <see cref="LegendEditor"/> Form
        /// </summary>
        /// <param name="_MasterOC">Base <see cref="OutputClass"/></param>
        /// <param name="_ParentCAD"></param>
        public void InitializeLegendEditor(OutputClass _MasterOC, CAD _ParentCAD)
        {
            ///<summary>Performing the Post Processing Activies of the Legend based on whether the Max and Min Value are 0. If they are 0 then it means the User is calling the Legend Editor for the very first time and hence the <see cref="BaseOC"/> should be passed</summary>
            if (MaxValue == 0 && MinValue == 0) 
            {
                BaseOC = _MasterOC;
                _ParentCAD.PostProcessing(this, BaseOC, UsersGradient1, UsersGradient2, UsersGradientStyle, NoOfSteps, StepSize);

            }

            else
            {
                OutputClass tempOC = new OutputClass();
                tempOC.MaxForce = MaxValue;
                tempOC.MinForce = MinValue;
                _ParentCAD.PostProcessing(this, tempOC, UsersGradient1, UsersGradient2, UsersGradientStyle, NoOfSteps, StepSize);
            }

            ///<summary>Method to assign the Parent <see cref="CAD"/> object</summary>
            GetParentCADControl(_ParentCAD);

            ///<summary>Method to get the <see cref="LegendDataTable"/> property of this class</summary>
            GetDataLegendDataSource(_ParentCAD.LegendDataTable);

            ///<summary>Method to condition the <see cref="gridControl1"/> of this Form</summary>
            GridControlConditioning(_ParentCAD.LegendDataTable);

            ///<summary>Method to initialize the <see cref="CellValueChangedEventArgs"/> </summary>
            InitializeGridControlEvents();

            ///<remarks>
            ///It is best if the below lines of code are inside THIS Class and not the <see cref="VehicleGUI"/> class's <see cref="VehicleGUI.OutputDrawer(CAD, int, int, bool, bool)"/> method because this way, when the 
            ///user wants to see the Forces of a different Motion Percentage, the options chosen by the user through the <see cref="LegendEditor"/> will be retained. 
            /// </remarks>
            ///<summary>Painting the Bars according to Force Range in between which they lie. <see cref="LegendEditor.UserNoForceWishboneColour"/> value passed as <see cref="Color.White"/></summary>
            ParentCAD.PaintBarForce(UserNoForceWishboneColour, DisplayWishboneForces);

            ///<summary>Painting the Arrows according to Force Range in between which they lie</summary>
            ///<remarks>Since by default we have <see cref="ForceArrowStyle.Both"/> I can pass any random values for Length and Colour below</remarks>
            ParentCAD.ConditionArrowForce(UserForceArrowStyle, ArrowNoForceCylLength, ArrowNoForceColor, ForcesToBeDisplayed);

        }

        /// <summary>
        /// Method to get the Parent <see cref="CAD"/> control of this Form
        /// </summary>
        /// <param name="_parentCAD">Parent <see cref="CAD"/> Control </param>
        private void GetParentCADControl(CAD _parentCAD)
        {
            ParentCAD = _parentCAD;

        }

        /// <summary>
        /// Method to get the <see cref="LegendDataTable"/> property of this Form and the <see cref="MaxValue"/> and <see cref="MinValue"/> properties
        /// </summary>
        /// <param name="_legendDataTable"></param>
        private  void GetDataLegendDataSource(DataTable _legendDataTable)
        {
            LegendDataTable = _legendDataTable;

            MaxValue = LegendDataTable.Rows[0].Field<double>("Force Start");

            MinValue = LegendDataTable.Rows[LegendDataTable.Rows.Count - 1].Field<double>("Force End");

        }

        /// <summary>
        /// Method to initialize the <see cref="bandedGridView1"/>, set the <see cref="GridControl.DataSource"/> and perform some conditioning for the <see cref="GridControl.MainView"/> (which is the <see cref="bandedGridView1"/>)
        /// </summary>
        /// <param name="_legendDataTable"><see cref="DataTable"/> of the Legend</param> 
        private void GridControlConditioning(DataTable _legendDataTable)
        {
            ///<summary> Initializing the <see cref="bandedGridView1"/> </summary>
            bandedGridView1 = CustomBandedGridView.CreateNewBandedGridView(0, 3, "Legend Data");

            ///<summary>Setting the DataSource and MainView of the <see cref="gridControl1"/></summary>
            GridControlConditioning_SetDataSource();

            ///<summary>Conditioning the Columns of the BandedGridView </summary>
            bandedGridView1 = CustomBandedGridColumn.ColumnEditor_ForLegend(bandedGridView1);

            ///<summary>Assigning the Colour Column of the Legend with the <see cref="repositoryItemColorPickEdit1"/> so that it displayes the colours from the <see cref="LegendDataTable"/></summary>
            bandedGridView1.Columns[2].ColumnEdit = repositoryItemColorPickEdit1;
            
        }

        /// <summary>
        /// Method to initialize the <see cref="CellValueChangedEventArgs"/> of the <see cref="bandedGridView1"/>
        /// </summary>
        private void InitializeGridControlEvents()
        {
            bandedGridView1.CellValueChanged += BandedGridView1_CellValueChanged;

            bandedGridView1.ValidatingEditor += BandedGridView1_ValidatingEditor;

        }

        /// <summary>
        /// Medhot to assign the <see cref="GridControl.DataSource"/> and the <see cref="GridControl.MainView"/>
        /// </summary>
        private void GridControlConditioning_SetDataSource()
        {
            gridControl1.DataSource = LegendDataTable;

            gridControl1.MainView = bandedGridView1;
        }


        /// <summary>
        /// Event which is fired when the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxControlDisplayOptions_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (checkedListBoxControlDisplayOptions.Items[0].CheckState == CheckState.Checked)
            {
                DisplayWishboneForces = true;
            }
            else 
            {
                DisplayWishboneForces = false;
            }
            
            
            //DisplayWishboneDecomp = (bool)checkedListBoxControlDisplayOptions.Items[1].Value;
            //DisplayBearingCapForces = (bool)checkedListBoxControlDisplayOptions.Items[2].Value;
            //DisplaySuspendedMassForces = (bool)checkedListBoxControlDisplayOptions.Items[3].Value;
            //DisplayNonSuspendedMassForces = (bool)checkedListBoxControlDisplayOptions.Items[4].Value;

            ForcesToBeDisplayed.Clear();

            for (int i = 0; i < checkedListBoxControlDisplayOptions.Items.Count; i++)
            {
                if (checkedListBoxControlDisplayOptions.Items[i].CheckState == CheckState.Checked)
                {
                    ForcesToBeDisplayed.Add(checkedListBoxControlDisplayOptions.Items[i].Description);
                }
            }

        }

        /// <summary>
        /// Event which is fired when the index of the <see cref="radioGroupGradientStyle"/> is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupGradientStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupGradientStyle.SelectedIndex == 0)
            {
                groupControlCustomColours.Visible = false;
                UsersGradientStyle = GradientStyle.StandardFEM;
            }
            else if (radioGroupGradientStyle.SelectedIndex == 1)
            {
                groupControlCustomColours.Visible = true;
                UsersGradientStyle = GradientStyle.Monochromatic;
            }

            ParentCAD.GradientType = UsersGradientStyle;

            ReDoEntireLegend(); 
        }

        /// <summary>
        /// Event which is fired when a cell of the <see cref="gridControl1"/> is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BandedGridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            int RowIndex = bandedGridView1.FocusedRowHandle;

            double StartForce = LegendDataTable.Rows[RowIndex].Field<double>("Force Start");

            double EndForce = LegendDataTable.Rows[RowIndex].Field<double>("Force End");

            Color ForceRangeColour = LegendDataTable.Rows[RowIndex].Field<Color>("Colour");

            ///<summary>Passed by Reference because it's an object. So the below method not needed</summary>
            //ParentCAD.EditRowLegendTable(StartForce, EndForce, ForceRangeColour, RowIndex);
            
        }

        /// <summary>
        /// Event which is fired when the cell of the <see cref="gridControl1"/> is changed. This event serves to only validate the typed values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BandedGridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            int ColumnIndex = bandedGridView1.FocusedColumn.AbsoluteIndex;

            if (ColumnIndex == 0 || ColumnIndex == 1)
            {
                if (!Double.TryParse(e.Value as string,out double checker))
                {
                    e.Valid = false;
                    e.ErrorText = "Please Enter Numeric Values";
                }
            }
            else if (ColumnIndex == 2)
            {
                if ((Color)e.Value == null)
                {
                    e.Valid = false;
                    e.ErrorText = "Please Select Color";
                }
            }
            else
            {
                e.Valid = true;
            }
        }

        private void simpleButtonAddRow_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void AddRow()
        {
            ParentCAD.AddRowToLegendTable(0, 0, MaxValue, MinValue, UsersGradient1, UsersGradient2);

            //ParentCAD.LegendDataTable.AsEnumerable().OrderByDescending(legend => legend.Field<double>("Force Start"));

            ParentCAD.LegendDataTable.DefaultView.Sort = "Force Start";

            ParentCAD.LegendDataTable = ParentCAD.LegendDataTable.DefaultView.ToTable();

            ParentCAD.LegendDataTable = ParentCAD.LegendDataTable.AsEnumerable().Reverse().CopyToDataTable();

            ReConditionLegend();

            GridControlConditioning_SetDataSource();
        }

        private void simpleButtonDeleteRow_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void DeleteRow()
        {

        }

        /// <summary>
        /// Event which is fired when the <see cref="simpleButtonReset"/> is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonReset_Click(object sender, EventArgs e)
        {
            ResetLegend();
        }
        /// <summary>
        /// Method to reset the Legend to its very initial state
        /// </summary>
        private void ResetLegend()
        {
            ///<summary>Resetting the <see cref="NoOfSteps"/> and the <see cref="StepSize"/> to because thats how the default legend is created</summary>
            NoOfSteps = 0;
            StepSize = 0;
            textBoxNoOfSteps.Clear();
            textBoxStepSize.Clear();
            ///<summary>Calling the <see cref="CAD.PostProcessing(LegendEditor, OutputClass, Color, Color, GradientStyle, int, double)"/> method. This method is what initializes the Legend and hence it is use to reset it</summary>
            ParentCAD.PostProcessing(this, BaseOC, UsersGradient1, UsersGradient2, UsersGradientStyle, NoOfSteps, StepSize);
            GridControlConditioning_SetDataSource();
        }

        /// <summary>
        /// Event which is fired when the <see cref="textBoxStepSize"/> control is left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxStepSize_Leave(object sender, EventArgs e)
        {
            CustomStepSize();
        }

        /// <summary>
        /// Event fired when the <see cref="Keys.Enter"/> is pressed when the control is inside the <see cref="textBoxStepSize"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxStepSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CustomStepSize();
                ///<summary>Random code to remove the focus from the <see cref="TextBox"/> to give the user a feel that the value has been changed</summary>
                ActiveControl = labelControl7; 
            }

        }

        /// <summary>
        /// Method to assign custom or user defined step size to the legend and redraw the legend based on this input. 
        /// </summary>
        private void CustomStepSize()
        {
            if (Double.TryParse(textBoxStepSize.Text, out double checker))
            {
                if (checker > 0)
                {
                    StepSize = Convert.ToDouble(textBoxStepSize.Text);

                    ///<summary>If Step Size is set then the NoOfSteps can't be an Input so it is set to 0</summary>
                    NoOfSteps = 0;

                    ///<summary>Redrawing the entire <see cref="LegendDataTable"/> and then <see cref="bandedGridView1"/> and then the <see cref="gridControl1"/></summary>
                    ReDoEntireLegend();

                    //EditEntireLegend();
                }
                else if (checker == 0)
                {
                    MessageBox.Show("Step Size Cannot Be Zero");
                }
                else
                {
                    MessageBox.Show("Step Size Cannot be Negative");
                }
            }
            else
            {
                MessageBox.Show("Please enter Number Values");
            }
        }

        /// <summary>
        /// Event which is fired when the <see cref="textBoxNoOfSteps"/> control is left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNoOfSteps_Leave(object sender, EventArgs e)
        {
            CustomNoOfSteps();
        }

        private void textBoxNoOfSteps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CustomNoOfSteps();
                ///<summary>Random code to remove the focus from the <see cref="TextBox"/> to give the user a feel that the value has been changed</summary>
                ActiveControl = labelControl7;
            }

        }

        /// <summary>
        /// Method to assign a custom or user degined number of steps of the legend and redraw the legend based on this input
        /// </summary>
        private void CustomNoOfSteps()
        {
            if (Int32.TryParse(textBoxNoOfSteps.Text, out int checker))
            {
                if (checker > 0)
                {
                    NoOfSteps = Convert.ToInt32(textBoxNoOfSteps.Text);

                    ///<summary>If Number of steps is set then the Step Size can't be an input so setting it to 0</summary>
                    StepSize = 0;

                    ///<summary>Redrawing the entire <see cref="LegendDataTable"/> and then <see cref="bandedGridView1"/> and then the <see cref="gridControl1"/></summary>
                    ReDoEntireLegend();
                }
                else if (checker == 0)
                {
                    MessageBox.Show("No Of Steps Cannot Be 0");
                }
                else
                {
                    MessageBox.Show("No Of Steps Cannot Be Negative");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Reasonable Whole Numbers");
            }
        }

        private void textBoxArrowConstLength_Leave(object sender, EventArgs e)
        {
            ConstantArrowLength();
        }

        private void textBoxArrowConstLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConstantArrowLength();
                ///<summary>Random code to remove the focus from the <see cref="TextBox"/> to give the user a feel that the value has been changed</summary>
                ActiveControl = labelControl7;
            }
        }

        /// <summary>
        /// Method to assign the <see cref="ArrowNoForceCylLength"/>
        /// </summary>
        private void ConstantArrowLength()
        {
            bool proceed = ParamValidator(textBoxArrowConstLength.Text, TypeToBeValidated.Double, true, out string Error);

            if (proceed)
            {
                ArrowNoForceCylLength = Convert.ToDouble(textBoxArrowConstLength.Text);
            }
            else
            {
                MessageBox.Show(Error);
            }

        }

        /// <summary>
        /// Method to Validate the content of a <see cref="TextBox.Text"/> based on whether user expects a <see cref="Double"/> or a <see cref="Int32"/> and also based on whether the user can accept Negative Values
        /// </summary>
        /// <param name="_paramFromTextBox"><see cref="TextBox.Text"/> value from the concerned <see cref="TextBox"/></param>
        /// <param name="_type">Enum to devide the <see cref="Type"/> of end result after validation</param>
        /// <param name="_noNegativeValues"><see cref="Boolean"/> to decide whther the end result can be negative</param>
        /// <param name="_errorMessage">Error Message if Parsing fails</param>
        /// <returns></returns>
        private bool ParamValidator(string _paramFromTextBox, TypeToBeValidated _type, bool _noNegativeValues, out string _errorMessage)
        {
            _errorMessage = null;
            if (_type == TypeToBeValidated.Double)
            {
                if (Double.TryParse(_paramFromTextBox,out double checker))
                {
                    if (_noNegativeValues)
                    {
                        if (checker > 0)
                        {
                            return true;
                        }
                        else if (checker == 0)
                        {
                            _errorMessage = "Zero Not Allowed";
                            return false;
                        }
                        else
                        {
                            _errorMessage = "Negative Values Not Allowed";
                            return false;
                        } 
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    _errorMessage = "Please Enter Numeric Values";
                    return false;
                }
            }

            else if (_type == TypeToBeValidated.Integer)
            {

            }

            return false;
        }

        /// <summary>
        /// Event fired when the color of the <see cref="colorPickEditForcelessArrowColor"/> is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickEditForcelessArrowColor_ColorChanged(object sender, EventArgs e)
        {
            UserNoForceWishboneColour = colorPickEditForcelessArrowColor.Color;
        }

        /// <summary>
        /// Event which is fired when the Colour of the <see cref="colorPickEdit1"/> is changed. This event causes the Legend Colours to be redrawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickEdit1_ColorChanged(object sender, EventArgs e)
         {
            ///<summary>Assinging the <see cref="UsersGradient1"/> property of this form</summary>
            UsersGradient1 = colorPickEdit1.Color;
            ParentCAD.GradientColor1 = UsersGradient1;
            ///<summary>Re-Painting the Colour Column of the <see cref="CAD.LegendDataTable"/></summary>
            ParentCAD.PaintLegendTableColorColumn(UsersGradient1, UsersGradient2);
            ///<summary>Reconditionring the Legend</summary>
            ReConditionLegend();
        }

        /// <summary>
        /// Event which is fired when the Colour of the <see cref="colorPickEdit2"/> is changed. This event causes the Legend Colours to be redrawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickEdit2_ColorChanged(object sender, EventArgs e)
        {
            ///<summary>Assinging the <see cref="UsersGradient1"/> property of this form</summary>
            UsersGradient2 = colorPickEdit2.Color;
            ParentCAD.GradientColor2 = UsersGradient2;
            ///<summary>Re-Painting the Colour Column of the <see cref="CAD.LegendDataTable"/></summary>
            ParentCAD.PaintLegendTableColorColumn(UsersGradient1, UsersGradient2);
            ///<summary>Reconditionring the Legend</summary>
            ReConditionLegend();
        }

        /// <summary>
        /// Event fires when the <see cref="colorPickEditArrowConstColor"/>'s selected color is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickEditArrowConstColor_ColorChanged(object sender, EventArgs e)
        {
            ///<summary>Assigning the <see cref="ArrowNoForceColor"/> property</summary>
            ArrowNoForceColor = colorPickEditArrowConstColor.Color;
        }



        /// <summary>
        /// Event fired when the <see cref="radioGroupLegendParams"/>'s index is changed. This event determines whether the Legend is going to be Standard FEM or Monochrome
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupLegendParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///<summary>GUI Operations based on whether the user wants to edit the <see cref="StepSize"/> or the <see cref="NoOfSteps"/></summary>
            if (radioGroupLegendParams.SelectedIndex == 0)
            {
                labelControlStepSize.Enabled = true;
                textBoxStepSize.Enabled = true;

                labelControlNoOfSteps.Enabled = false;
                textBoxNoOfSteps.Enabled = false;
                textBoxNoOfSteps.Clear();
            }
            else if (radioGroupLegendParams.SelectedIndex == 1)
            {
                labelControlStepSize.Enabled = false;
                textBoxStepSize.Enabled = false;
                textBoxStepSize.Clear();

                labelControlNoOfSteps.Enabled = true;
                textBoxNoOfSteps.Enabled = true;
            }
        }

        /// <summary>
        /// Medhot to Recreate the Entire Legend
        /// </summary>
        private void ReDoEntireLegend()
        {

            OutputClass tempOC = new OutputClass();
            GetDataLegendDataSource(ParentCAD.LegendDataTable);
            tempOC.MaxForce = MaxValue;
            tempOC.MinForce = MinValue;
            ParentCAD.PostProcessing(this, tempOC, UsersGradient1, UsersGradient2, UsersGradientStyle, NoOfSteps, StepSize);
            GridControlConditioning_SetDataSource();
        }

        /// <summary>
        /// Method to only assign the color table of the <see cref="CAD.viewportLayout1"/>'s <see cref="Legend"/>
        /// </summary>
        private void ReConditionLegend()
        {
            OutputClass tempOC = new OutputClass();
            GetDataLegendDataSource(ParentCAD.LegendDataTable);
            tempOC.MaxForce = MaxValue;
            tempOC.MinForce = MinValue;
            ParentCAD.AssignLegendColourTable();
            GridControlConditioning_SetDataSource();
        }

        /// <summary>
        /// Event fired when the user selects the <see cref="ForceArrowStyle"/> from the <see cref="radioGroupForceArrowOptions"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupForceArrowOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///<summary>Assigning the <see cref="ForceArrowStyle"/></summary>
            if (radioGroupForceArrowOptions.SelectedIndex != -1)
            {
                UserForceArrowStyle = (ForceArrowStyle)radioGroupForceArrowOptions.SelectedIndex; 
            }
            else
            {
                UserForceArrowStyle = ForceArrowStyle.Both;
            }

            ///<summary>GUI Operations based on the which <see cref="RadioButton"/> the user selects from the <see cref="radioGroupForceArrowOptions"/></summary>
            if (UserForceArrowStyle == ForceArrowStyle.Both)
            {
                labelControlArrowConstLength.Enabled = false;
                textBoxArrowConstLength.Clear();
                textBoxArrowConstLength.Enabled = false;

                labelControlArrowConstColor.Enabled = false;
                colorPickEditArrowConstColor.Color = Color.White;
                colorPickEditArrowConstColor.Enabled = false;
            }
            else if (UserForceArrowStyle == ForceArrowStyle.ColourScaling)
            {
                labelControlArrowConstLength.Enabled = true;
                textBoxArrowConstLength.Enabled = true;

                labelControlArrowConstColor.Enabled = false;
                colorPickEditArrowConstColor.Color = Color.White;
                colorPickEditArrowConstColor.Enabled = false;

            }
            else if (UserForceArrowStyle == ForceArrowStyle.LengthScaling)
            {
                labelControlArrowConstLength.Enabled = false;
                textBoxArrowConstLength.Clear();
                textBoxArrowConstLength.Enabled = false;

                labelControlArrowConstColor.Enabled = true;
                colorPickEditArrowConstColor.Enabled = true;
            }

        }

        /// <summary>
        /// Event fired when the <see cref="simpleButtonOK"/> is clicked. This method repaints ALL the Arrows and Bars of the <see cref="CAD.viewportLayout1"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ReConditionLegend();

            ParentCAD.PaintBarForce(UserNoForceWishboneColour, DisplayWishboneForces);

            ParentCAD.ConditionArrowForce(UserForceArrowStyle, ArrowNoForceCylLength, ArrowNoForceColor, ForcesToBeDisplayed);

            this.Hide();

        }

        private void simpleButtonCanel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


    }

    enum TypeToBeValidated
    {
        Double,
        Integer,
        String
    }

}
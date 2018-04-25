﻿using System;
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
        public double MaxValue { get; set; } = 0;
        /// <summary>
        /// User defined Minimum Value of the Legend 
        /// </summary>
        public double MinValue { get; set; } = 0;
        /// <summary>
        /// User defined Colour Upper Colour Gradient of the Legend 
        /// </summary>
        public Color UsersGradient1 { get; set; } = Color.Maroon;
        /// <summary>
        /// User defined Colour Lower Colour Gradient of the Legend 
        /// </summary>
        public Color UsersGradient2 { get; set; } = Color.Ivory;
        /// <summary>
        /// Number of Steps in the Legend
        /// </summary>
        public int NoOfSteps { get; set; } = 0;
        /// <summary>
        /// Step Size of the Steps in the Legned
        /// </summary>
        public double StepSize { get; set; } = 0;
        /// <summary>
        /// User defined <see cref="GradientStyle"/> 0f the Legend
        /// </summary>
        public GradientStyle UsersGradientStyle { get; set; } = GradientStyle.StandardFEM;
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

            radioGroup1.SelectedIndex = -1;

            radioGroup2.SelectedIndex = -1;
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
        /// Event which is fired when the index of the <see cref="radioGroup1"/> is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                groupControlCustomColours.Visible = false;
                UsersGradientStyle = GradientStyle.StandardFEM;
            }
            else if (radioGroup1.SelectedIndex == 1)
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

                    //EditEntireLegend();
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
        /// Event fired when the <see cref="simpleButtonOK"/> is clicked. This method repaints ALL the Arrows and Bars of the <see cref="CAD.viewportLayout1"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ReConditionLegend();

            ParentCAD.PaintBarForce();

            ParentCAD.PaintArrowForce();

            this.Hide();

        }

        private void simpleButtonCanel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Event fired when the <see cref="radioGroup2"/>'s index is changed. This event determines whether the Legend is going to be Standard FEM or Monochrome
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup2.SelectedIndex == 0)
            {
                labelControl1.Enabled = true;
                textBoxStepSize.Enabled = true;

                labelControl2.Enabled = false;
                textBoxNoOfSteps.Enabled = false;
                textBoxNoOfSteps.Clear();
            }
            else if (radioGroup2.SelectedIndex == 1)
            {
                labelControl1.Enabled = false;
                textBoxStepSize.Enabled = false;
                textBoxStepSize.Clear();

                labelControl2.Enabled = true;
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
            //ParentCAD.PostProcessing(this, tempOC, UsersGradient1, UsersGradient2, UsersGradientStyle, NoOfSteps, StepSize);
            //GetDataLegendDataSource(ParentCAD.LegendDataTable);
            //ParentCAD.GetLegendParams(tempOC, NoOfSteps, StepSize);
            //ParentCAD.PaintLegendTableColorColumn(UsersGradient1,UsersGradient2);
            ParentCAD.AssignLegendColourTable();
            GridControlConditioning_SetDataSource();
        }

    }
}
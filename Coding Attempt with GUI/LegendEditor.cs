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

        public DataTable LegendDataTable { get; set; }

        public CAD ParentCAD { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public Color UsersGradient1 { get; set; }

        public Color UsersGradient2 { get; set; }

        public int NoOfSteps { get; set; }

        public double StepSize { get; set; }

        public GradientStyle UsersGradientStyle { get; set; }

        public LegendEditor()
        {
            InitializeComponent();

            radioGroup1.SelectedIndex = -1;

            radioGroup2.SelectedIndex = -1;
        }

        public void InitializeLegendEditor(DataTable _LegendDataTable, CAD _ParentCAD)
        {


            GetParentCADControl(_ParentCAD);

            GetDataLegendDataSource(_LegendDataTable);

            GridControlConditioning();

            InitializeGridControlEvents();

        }

        private void InitializeGridControlEvents()
        {
            bandedGridView1.CellValueChanged += BandedGridView1_CellValueChanged;

            bandedGridView1.ValidatingEditor += BandedGridView1_ValidatingEditor;

        }

        private void GetParentCADControl(CAD _parentCAD)
        {
            ParentCAD = _parentCAD;

        }

        private  void GetDataLegendDataSource(DataTable _legendDataTable)
        {
            LegendDataTable = _legendDataTable;

            MaxValue = LegendDataTable.Rows[0].Field<double>("Force Start");

            MinValue = LegendDataTable.Rows[LegendDataTable.Rows.Count - 1].Field<double>("Force End");

        }

        private void GridControlConditioning()
        {
            bandedGridView1 = CustomBandedGridView.CreateNewBandedGridView(0, 3, "Legend Data");

            GridControlConditioning_SetDataSource();

            bandedGridView1 = CustomBandedGridColumn.ColumnEditor_ForLegend(bandedGridView1);

            bandedGridView1.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            bandedGridView1.Columns[2].ColumnEdit = repositoryItemColorPickEdit1;

            bandedGridView1.Columns[2].BestFit();
        }

        private void GridControlConditioning_SetDataSource()
        {
            gridControl1.DataSource = LegendDataTable;

            gridControl1.MainView = bandedGridView1;
        }

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

            EditEntireLegend(); 
        }

        void BandedGridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            int RowIndex = bandedGridView1.FocusedRowHandle;

            //string ForceRange = LegendDataTable.Rows[RowIndex].Field<string>("Force Range");

            double StartForce = LegendDataTable.Rows[RowIndex].Field<double>("Force Start");

            double EndForce = LegendDataTable.Rows[RowIndex].Field<double>("Force End");

            Color ForceRangeColour = LegendDataTable.Rows[RowIndex].Field<Color>("Colour");

            ///<summary>Passed by Reference because it's an object. So the below method not needed</summary>
            //ParentCAD.EditRowLegendTable(StartForce, EndForce, ForceRangeColour, RowIndex);

        }

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

            GridControlConditioning_SetDataSource();
        }

        private void simpleButtonDeleteRow_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void DeleteRow()
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxStepSize_Leave(object sender, EventArgs e)
        {
            if (Double.TryParse(textBoxStepSize.Text,out double checker))
            {
                if (checker > 0)
                {
                    StepSize = Convert.ToDouble(textBoxStepSize.Text);

                    NoOfSteps = 0;

                    EditEntireLegend();
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

        private void textBoxNoOfSteps_Leave(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBoxNoOfSteps.Text,out int checker))
            {
                if (checker > 0)
                {
                    NoOfSteps = Convert.ToInt32(textBoxNoOfSteps.Text);

                    StepSize = 0;

                    EditEntireLegend();
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

        private void colorPickEdit1_ColorChanged(object sender, EventArgs e)
        {
            UsersGradient1 = colorPickEdit1.Color;
            ParentCAD.GradientColor1 = UsersGradient1;
            EditEntireLegend();
        }

        private void colorPickEdit2_ColorChanged(object sender, EventArgs e)
        {
            UsersGradient2 = colorPickEdit2.Color;
            ParentCAD.GradientColor2 = UsersGradient2;
            EditEntireLegend();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ParentCAD.PaintBarForce();

            ParentCAD.PaintArrowForce();

            this.Hide();

        }

        private void simpleButtonCanel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EditEntireLegend()
        {
            OutputClass tempOC = new OutputClass();
            tempOC.MaxForce = MaxValue;
            tempOC.MinForce = MinValue;
            ParentCAD.PostProcessing(tempOC, UsersGradient1, UsersGradient2, NoOfSteps, StepSize);
            GetDataLegendDataSource(ParentCAD.LegendDataTable);
            GridControlConditioning_SetDataSource();
        }

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

        private void simpleButtonMoveUp_Click(object sender, EventArgs e)
        {

        }
    }
}
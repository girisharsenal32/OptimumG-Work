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

namespace Coding_Attempt_with_GUI
{
    public partial class LegendEditor : DevExpress.XtraEditors.XtraForm
    {

        public DataTable LegendDataTable { get; set; }

        public CAD ParentCAD { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public LegendEditor()
        {
            InitializeComponent();
        }

        public void InitializeLegendEditor(DataTable _LegendDataTable, CAD _ParentCAD)
        {
            GetParentCADControl(_ParentCAD);

            InitializeGridControlEvents();

            GetDataLegendDataSource(_LegendDataTable);

            GridControlConditioning();
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
            bandedGridView1 = CustomBandedGridView.CreateNewBandedGridView(0, 4, "Legend Data");

            gridControl1.DataSource = LegendDataTable;

            gridControl1.MainView = bandedGridView1;

            bandedGridView1 = CustomBandedGridColumn.ColumnEditor_ForLegend(bandedGridView1);

            bandedGridView1.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            bandedGridView1.Columns[3].ColumnEdit = repositoryItemColorPickEdit1;

            bandedGridView1.Columns[3].BestFit();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                groupControlCustomColours.Visible = false;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                groupControlCustomColours.Visible = true;
            }
        }

        private void BandedGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int RowIndex = bandedGridView1.FocusedRowHandle;

            string ForceRange = LegendDataTable.Rows[RowIndex].Field<string>("Force Range");

            double StartForce = LegendDataTable.Rows[RowIndex].Field<double>("Force Start");

            double EndForce = LegendDataTable.Rows[RowIndex].Field<double>("Force End");

            Color ForceRangeColour = LegendDataTable.Rows[RowIndex].Field<Color>("Force Colour");
        }

        private void BandedGridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        private void simpleButtonAddRow_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void AddRow()
        {
            ParentCAD.AddRowToLegendTable(0, 0, MaxValue, MinValue);

            gridControl1.DataSource = LegendDataTable;

            gridControl1.MainView = bandedGridView1;
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
    }
}
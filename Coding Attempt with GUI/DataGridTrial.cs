using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace Coding_Attempt_with_GUI
{
    public partial class DataGridTrial : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public DataGridTrial()
        {
            InitializeComponent();

        }
        public DataTable dt = new DataTable();
        public DataTable dt2 = new DataTable();

        private void DataGridTrial_Load(object sender, EventArgs e)
        {

            dt.Columns.Add("Front Left SuspensionoCoordinate",typeof(string));
            dt.Columns.Add("x",typeof(double));
            dt.Columns.Add("y", typeof(double));
            dt.Columns.Add("z", typeof(double));

            dt2.Columns.Add("Front Left SuspensionoCoordinate", typeof(string));
            dt2.Columns.Add("x", typeof(double));
            dt2.Columns.Add("y", typeof(double));
            dt2.Columns.Add("z", typeof(double));

            Default_Values.FRONTLEFTSuspensionDefaultValues.LowerFrontChassis_DataGridTrial(this);
            Default_Values.FRONTLEFTSuspensionDefaultValues.LowerFrontChassis_DataGridTrial2(this);
            gridControl1.DataSource = dt;

            dt.ColumnChanged += new DataColumnChangeEventHandler(dt_ColumnChanged);
            dt2.ColumnChanged += new DataColumnChangeEventHandler(dt2_ColumnChanged);
            
        }

        void dt2_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            dt2.Rows[0].BeginEdit();
            double A = dt2.Rows[0].Field<double>(1);
            MessageBox.Show(Convert.ToString(A));
        }

        void dt_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            dt.Rows[0].BeginEdit();
            double A = dt.Rows[0].Field<double>(1);
            MessageBox.Show(Convert.ToString(A));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt;
        }
    }
}
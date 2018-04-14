using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Data;


namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This it the GUI class of the Damper
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is used to initialize the Damper Class Object.
    /// </summary>
    [Serializable()]
    public class DamperGUI:ISerializable
    {
        #region Autoimplemented Properties of Damper
        public double _DamperGasPressure { get; set; }
        public double _DamperShaftDia { get; set; }
        public double _DamperFreeLength { get; set; }
        public double _DamperStroke { get; set; }
        #endregion

        public CustomBandedGridView bandedGridView_DamperGUI = new CustomBandedGridView();

        #region DamperGUI Data Table
        public DataTable DamperDataTableGUI { get; set; }
        #endregion

        static Kinematics_Software_New r1;

        public DamperGUI()
        {

            #region Default values to safeguard the Parmeters, in case the user adds invalid input
            _DamperGasPressure = 0.5;
            _DamperShaftDia = 8;
            _DamperFreeLength = 260;
            _DamperStroke = 100;
            #endregion

            #region Initialzing the Damper Data Table
            DamperDataTableGUI = new DataTable();

            DamperDataTableGUI.TableName = "Damper Properties";

            DamperDataTableGUI.Columns.Add("Column 1", typeof(string));
            DamperDataTableGUI.Columns[0].ReadOnly = true;

            DamperDataTableGUI.Columns.Add("Column 2", typeof(double)); 
            #endregion

        }

        #region Modify/Update Damper Method
        public void Update_DamperGUI(Kinematics_Software_New _r1, int l_tire)
        {
            r1 = _r1;

            #region Initialization of the Damper GUI Class using User Interface Object
            _DamperGasPressure = DamperDataTableGUI.Rows[0].Field<double>(1);
            _DamperShaftDia = DamperDataTableGUI.Rows[1].Field<double>(1);
            _DamperFreeLength = DamperDataTableGUI.Rows[2].Field<double>(1);
            _DamperStroke = DamperDataTableGUI.Rows[3].Field<double>(1);
            #endregion
        } 
        #endregion

        #region Function to display the selected damper items property
        public static void DisplayDamperItem(Damper damper)
        {
            r1.gridControl2.DataSource = damper.DamperDataTable;
        } 
        #endregion

        #region De-serialization of the DamperGUI Object
        public DamperGUI(SerializationInfo info, StreamingContext context)
        {
            _DamperGasPressure = (double)info.GetValue("DamperGasPressure", typeof(double));
            _DamperShaftDia = (double)info.GetValue("DamperShaftDia", typeof(double));
            _DamperFreeLength = (double)info.GetValue("DamperFreeLength", typeof(double));
            _DamperStroke = (double)info.GetValue("DamperStroke", typeof(double));
            DamperDataTableGUI = (DataTable)info.GetValue("DamperDataTableGUI", typeof(DataTable));
        } 
        #endregion

        #region Serialization of the DamperGUI object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DamperGasPressure", _DamperGasPressure);
            info.AddValue("DamperShaftDia", _DamperShaftDia);
            info.AddValue("DamperFreeLength", _DamperFreeLength);
            info.AddValue("DamperStroke", _DamperStroke);
            info.AddValue("DamperDataTableGUI", DamperDataTableGUI);
        } 
        #endregion
    }
}

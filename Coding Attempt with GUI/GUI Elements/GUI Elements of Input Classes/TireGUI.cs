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
    /// This it the GUI class of the Tire
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is used to initialize the Tire Class Object.
    /// </summary>

    [Serializable()]
    public class TireGUI:ISerializable
    {
        #region Autoimplemented Properties of Tire
        public string _TireName { get; set; }
        public double _TireRate { get; set; }
        public double _TireWidth { get; set; }
        #endregion

        #region Tire Banded Grid View
        public CustomBandedGridView bandedGridView_TireGUI = new CustomBandedGridView(); 
        #endregion

        #region TireGUI Data Table
        public DataTable TireDataTableGUI { get; set; } 
        #endregion

        static Kinematics_Software_New r1;

        #region Base Constructor
        public TireGUI()
        {
            #region Default values to safeguard the Parmeters, in case the user adds invalid input
            _TireRate = 117.43;
            _TireWidth = 157.48; 
            #endregion

            #region Initialzing the Tire Data Table
            TireDataTableGUI = new DataTable();

            TireDataTableGUI.TableName = "Tire Properties";

            TireDataTableGUI.Columns.Add("Column 1", typeof(string));
            TireDataTableGUI.Columns[0].ReadOnly = true;

            TireDataTableGUI.Columns.Add("Column 2",typeof(double));
            #endregion
        } 
        #endregion

        #region Modify/Update Tire Method
        public void Update_TireGUI(Kinematics_Software_New _r1, int l_tire)
        {
            r1 = _r1;

            #region Initialization of the Tire GUI Class using User Interface Object
            _TireRate = TireDataTableGUI.Rows[0].Field<double>(1);
            _TireWidth = TireDataTableGUI.Rows[1].Field<double>(1);
            #endregion
        } 
        #endregion

        #region Function to display the Selected Tire items property (Not implemented in all classes yet)
        public static void DisplayTireItem(Tire tire)
        {
            r1.gridControl2.DataSource = tire.TireDataTable;
        } 
        #endregion

        #region De-serialization of the TireGUI object
        public TireGUI(SerializationInfo info, StreamingContext context)
        {
            _TireRate = (double)info.GetValue("TireRate", typeof(double));
            _TireWidth = (double)info.GetValue("TireWidth", typeof(double));
            TireDataTableGUI = (DataTable)info.GetValue("TireDataTable", typeof(DataTable));
        } 
        #endregion

        #region Serialization of the TireGUI object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TireRate", _TireRate);
            info.AddValue("TireWidth", _TireWidth);
            info.AddValue("TireDataTable", TireDataTableGUI);

        } 
        #endregion
    }
}

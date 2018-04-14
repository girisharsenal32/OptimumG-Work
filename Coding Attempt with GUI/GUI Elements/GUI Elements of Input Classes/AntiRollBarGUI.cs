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
    /// This it the GUI class of the Anti-Roll Bar
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is used to initialize the Anti-Roll Bar Class Object.
    /// </summary>
    [Serializable()]
    public class AntiRollBarGUI:ISerializable
    {
        #region Autoimplemented Properties of Anti-Roll Bar
        public double _AntiRollBarRate { get; set; }
        #endregion

        #region ARB Banded Grid View 
        public CustomBandedGridView bandedGridView_ARBGUI = new CustomBandedGridView(); 
        #endregion

        #region ABRGUI Data Table
        public DataTable ARBDataTableGUI { get; set; }
        #endregion

        static Kinematics_Software_New r1;

        #region Constructor
        public AntiRollBarGUI()
        {
            #region Default values to safeguard the Parmeters, in case the user adds invalid input
            _AntiRollBarRate = 7;
            #endregion

            #region Initialzing the ARB Data Table
            ARBDataTableGUI = new DataTable();

            ARBDataTableGUI.TableName = "Anti-Roll Bar Properties";

            ARBDataTableGUI.Columns.Add("Column 1", typeof(string));
            ARBDataTableGUI.Columns[0].ReadOnly = true;

            ARBDataTableGUI.Columns.Add("Column 2", typeof(double));
            #endregion
        } 
        #endregion

        #region Modify/Update ARB Method
        public void Update_ARBGUI(Kinematics_Software_New _r1,int l_arb)
        {
            r1 = _r1;

            #region Anti-Roll Bar Parameters Initialization
            _AntiRollBarRate = ARBDataTableGUI.Rows[0].Field<double>(1);
            #endregion
        } 
        #endregion

        #region Function to display the properties of the selected ARB
        public static void DisplayARBItem(AntiRollBar arb)
        {
            r1.gridControl2.DataSource = arb.ARBDataTable;
        } 
        #endregion

        #region De-serialization of the ARBGUI object
        public AntiRollBarGUI(SerializationInfo info, StreamingContext context)
        {
            _AntiRollBarRate = (double)info.GetValue("AntiRollBarRate", typeof(double));
            ARBDataTableGUI = (DataTable)info.GetValue("AntiRollBarDataTable", typeof(DataTable));
        } 
        #endregion

        #region Serialization of the ARBGUI object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AntiRollBarRate", _AntiRollBarRate);
            info.AddValue("AntiRollBarDataTable", ARBDataTableGUI);
        } 
        #endregion
    }
}

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
    /// This it the GUI class of the Spring
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is used to initialize the Spring Class Object.
    /// </summary>
    [Serializable()]
    public class SpringGUI:ISerializable
    {
        #region Autoimplemented Properties of Spring
        public double _SpringRate { get; set; }
        public double _SpringPreload { get; set; }
        public double _SpringFreeLength { get; set; }
        #endregion

        #region SpringGUI Data Table
        public DataTable SpringDataTableGUI { get; set; } 
        #endregion

        public CustomBandedGridView bandedGridView_SpringGUI = new CustomBandedGridView();

        static Kinematics_Software_New r1;

        #region Constructor
        public SpringGUI()
        {

            #region Default values to safeguard the Parmeters, in case the user adds invalid input
            _SpringRate = 43.75;
            _SpringPreload = 3.5;
            _SpringFreeLength = 122;
            #endregion

            #region Initializing the Spring Data Table'

            SpringDataTableGUI = new DataTable();

            SpringDataTableGUI.TableName = "Spring Properties";

            SpringDataTableGUI.Columns.Add("Column 1", typeof(string));
            SpringDataTableGUI.Columns[0].ReadOnly = true;

            SpringDataTableGUI.Columns.Add("Column 2", typeof(double));

            #endregion

        } 
        #endregion

        #region Modify/Update Tire Method
        public void Update_SpringGUI(Kinematics_Software_New _r1, int l_spring)
        {
            r1 = _r1;

            #region Initialization of the Spring GUI Class using User Interface Object
            _SpringRate = SpringDataTableGUI.Rows[0].Field<double>(1);
            _SpringPreload = SpringDataTableGUI.Rows[1].Field<double>(1);
            _SpringFreeLength = SpringDataTableGUI.Rows[2].Field<double>(1);
            #endregion

        } 
        #endregion

        #region Function to display the Selected Spring items property 
        public static void DisplaySpringItem(Spring spring)
        {
            r1.gridControl2.DataSource = spring.SpringDataTable;
        } 
        #endregion

        #region De-serialization of the SpringGUI object
        public SpringGUI(SerializationInfo info, StreamingContext context)
        {
            _SpringRate = (double)info.GetValue("SpringRate", typeof(double));
            _SpringPreload = (double)info.GetValue("SpringPreload", typeof(double));
            _SpringFreeLength = (double)info.GetValue("SpringFreeLength", typeof(double));
            SpringDataTableGUI = (DataTable)info.GetValue("SpringDataTableGUI", typeof(DataTable));
        } 
        #endregion

        #region Serialization of the SpringGUI Object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SpringRate", _SpringRate);
            info.AddValue("SpringPreload", _SpringPreload);
            info.AddValue("SpringFreeLength", _SpringFreeLength);
            info.AddValue("SpringDataTableGUI", SpringDataTableGUI);
        } 
        #endregion

    }
}
 

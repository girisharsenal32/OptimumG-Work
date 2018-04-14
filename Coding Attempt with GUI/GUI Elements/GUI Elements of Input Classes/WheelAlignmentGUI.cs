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
    /// This it the GUI class of the Wheel Alignment
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is used to initialize the Wheel Alignment Class Object.
    /// </summary>
    [Serializable()]
    public class WheelAlignmentGUI : ISerializable
    {

        #region Autoimplemented Properties of Wheel Alignment
        public double _StaticCamber { get; set; }
        public double _StaticToe { get; set; }
        public double _SteeringRatio { get; set; }
        #endregion

        #region Wheel ALignment Banded Grid View
        public CustomBandedGridView bandedGridView_WAGUI = new CustomBandedGridView();
        #endregion

        #region WheelAlignmentGUI Data Table
        public DataTable WADataTableGUI { get; set; }
        #endregion

        static Kinematics_Software_New r1;

        public WheelAlignmentGUI()
        {

            #region Default values to safeguard the Parmeters, in case the user adds invalid input
            _StaticCamber = -0.8;
            _StaticToe = -0.5;
            _SteeringRatio = 15;
            #endregion

            #region Initializing the Wheel Alignment Data Table
            WADataTableGUI = new DataTable();

            WADataTableGUI.TableName = "Wheel Alignment";

            WADataTableGUI.Columns.Add("Column 1", typeof(string));
            WADataTableGUI.Columns[0].ReadOnly = true;

            WADataTableGUI.Columns.Add("Column 2", typeof(double));

            #endregion

        }

        public void Update_WheelAlignmentGUI(Kinematics_Software_New _r1, int l_tire)
        {
            r1 = _r1;

            #region Initialization of the Wheel Alignment GUI Class using User Interface Object
            _StaticCamber = (WADataTableGUI.Rows[0].Field<double>(1));
            _StaticToe = (WADataTableGUI.Rows[1].Field<double>(1));
            _SteeringRatio = (WADataTableGUI.Rows[2].Field<double>(1));
            #endregion
        }

        #region Function to display the properties of the selected Wheel Alignment
        public static void DisplayWheelAlignmentItem(WheelAlignment wheelAlignment)
        {
            r1.gridControl2.DataSource = wheelAlignment.WADataTable;
        }
        #endregion

        public WheelAlignmentGUI(SerializationInfo info, StreamingContext context)
        {
            _StaticCamber = (double)info.GetValue("StaticCamber", typeof(double));
            _StaticToe = (double)info.GetValue("StaticToe", typeof(double));
            _SteeringRatio = (double)info.GetValue("SteeringRatio", typeof(double));
            WADataTableGUI = (DataTable)info.GetValue("WADataTable", typeof(DataTable));
 
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StaticCamber", _StaticCamber);
            info.AddValue("StaticToe", _StaticToe);
            info.AddValue("SteeringRatio", _SteeringRatio);
            info.AddValue("WADataTable", WADataTableGUI);
        }
    }
}

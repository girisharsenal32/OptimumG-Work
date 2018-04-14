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
    /// This it the GUI class of the Chassis
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is used to initialize the Chassis Class.
    /// </summary>
    [Serializable()]
    public class ChassisGUI:ISerializable
    {
        #region Autoimplemented Properties of Chassis
        public double SuspendedMass { get; set; }

        public double _NonSuspendedMassFL { get; set; }
        public double _NonSuspendedMassFR { get; set; }
        public double _NonSuspendedMassRL { get; set; }
        public double _NonSuspendedMassRR { get; set; }

        public double _SuspendedMassCoGx { get; set; }
        public double _SuspendedMassCoGy { get; set; }
        public double _SuspendedMassCoGz { get; set; }

        public double _NonSuspendedMassFLCoGx { get; set; }
        public double _NonSuspendedMassFLCoGy { get; set; }
        public double _NonSuspendedMassFLCoGz { get; set; }

        public double _NonSuspendedMassFRCoGx { get; set; }
        public double _NonSuspendedMassFRCoGy { get; set; }
        public double _NonSuspendedMassFRCoGz { get; set; }

        public double _NonSuspendedMassRLCoGx { get; set; }
        public double _NonSuspendedMassRLCoGy { get; set; }
        public double _NonSuspendedMassRLCoGz { get; set; }

        public double _NonSuspendedMassRRCoGx { get; set; }
        public double _NonSuspendedMassRRCoGy { get; set; }
        public double _NonSuspendedMassRRCoGz { get; set; }
        #endregion

        #region Chassis Banded Grid View
        public CustomBandedGridView bandedGridViewChassis = new CustomBandedGridView(); 
        #endregion

        #region Chassis Data Table
        public DataTable ChassisDataTableGUI { get; set; } 
        #endregion

        static Kinematics_Software_New r1;

        #region Base Constructor
        public ChassisGUI(Kinematics_Software_New _r1)
        {
            r1 = _r1;

            #region Initializing the Chassis Data Table
            ChassisDataTableGUI = new DataTable();

            ChassisDataTableGUI.TableName = "Chassis Properties";

            ChassisDataTableGUI.Columns.Add("Components", typeof(string));
            ChassisDataTableGUI.Columns[0].ReadOnly = true;

            ChassisDataTableGUI.Columns.Add("Mass (Kg)", typeof(double));

            ChassisDataTableGUI.Columns.Add("X (mm)", typeof(double));

            ChassisDataTableGUI.Columns.Add("Y (mm)", typeof(double));

            ChassisDataTableGUI.Columns.Add("Z (mm)", typeof(double));
            #endregion
        }
        #endregion

        #region Modify/Update ChassisGUI Method
        public void Update_ChassisGUI(Kinematics_Software_New _r1, int l_chassis)
        {
            r1 = _r1;

            #region Initialization of the Chassis GUI Class using User Interface Object

            #region Suspended Mass Initialization
            SuspendedMass = ChassisDataTableGUI.Rows[0].Field<double>(1);
            #endregion

            #region Non Suspended Masses Initialization

            _NonSuspendedMassFL = ChassisDataTableGUI.Rows[1].Field<double>(1);
            _NonSuspendedMassFR = ChassisDataTableGUI.Rows[2].Field<double>(1);
            _NonSuspendedMassRL = ChassisDataTableGUI.Rows[3].Field<double>(1);
            _NonSuspendedMassRR = ChassisDataTableGUI.Rows[4].Field<double>(1);

            #endregion

            #region Suspended Mass Centre of Gravity Coordinates Initialization

            _SuspendedMassCoGx = ChassisDataTableGUI.Rows[0].Field<double>(2);
            _SuspendedMassCoGy = ChassisDataTableGUI.Rows[0].Field<double>(3);
            _SuspendedMassCoGz = ChassisDataTableGUI.Rows[0].Field<double>(4);

            #endregion

            #region Non Suspended Mass Centres of Gravity Coordinates Initialization

            _NonSuspendedMassFLCoGx = ChassisDataTableGUI.Rows[1].Field<double>(2);
            _NonSuspendedMassFLCoGy = ChassisDataTableGUI.Rows[1].Field<double>(3);
            _NonSuspendedMassFLCoGz = ChassisDataTableGUI.Rows[1].Field<double>(4);

            _NonSuspendedMassFRCoGx = ChassisDataTableGUI.Rows[2].Field<double>(2);
            _NonSuspendedMassFRCoGy = ChassisDataTableGUI.Rows[2].Field<double>(3);
            _NonSuspendedMassFRCoGz = ChassisDataTableGUI.Rows[2].Field<double>(4);

            _NonSuspendedMassRLCoGx = ChassisDataTableGUI.Rows[3].Field<double>(2);
            _NonSuspendedMassRLCoGy = ChassisDataTableGUI.Rows[3].Field<double>(3);
            _NonSuspendedMassRLCoGz = ChassisDataTableGUI.Rows[3].Field<double>(4);

            _NonSuspendedMassRRCoGx = ChassisDataTableGUI.Rows[4].Field<double>(2);
            _NonSuspendedMassRRCoGy = ChassisDataTableGUI.Rows[4].Field<double>(3);
            _NonSuspendedMassRRCoGz = ChassisDataTableGUI.Rows[4].Field<double>(4);

            #endregion

            #endregion

        }
        #endregion

        #region Function to display the properties of the selected chassis
        public static void DisplayChassisItem(Chassis chassis)
        {
            r1.gridControl2.DataSource = chassis.ChassisDataTable;
        } 
        #endregion

        #region De-serialization of the ChassisGUI object
        public ChassisGUI(SerializationInfo info, StreamingContext context)
        {
            SuspendedMass = (double)info.GetValue("Suspended_Mass", typeof(double));

            _NonSuspendedMassFL = (double)info.GetValue("NonSuspended_Mass_FL", typeof(double));
            _NonSuspendedMassFR = (double)info.GetValue("NonSuspended_Mass_FR", typeof(double));
            _NonSuspendedMassRL = (double)info.GetValue("NonSuspended_Mass_RL", typeof(double));
            _NonSuspendedMassRR = (double)info.GetValue("NonSuspended_Mass_RR", typeof(double));

            _SuspendedMassCoGx = (double)info.GetValue("SuspendedMass_COG_x", typeof(double));
            _SuspendedMassCoGy = (double)info.GetValue("SuspendedMass_COG_y", typeof(double));
            _SuspendedMassCoGz = (double)info.GetValue("SuspendedMass_COG_z", typeof(double));

            _NonSuspendedMassFLCoGx = (double)info.GetValue("NonSuspendedMass_COG_FL_x", typeof(double));
            _NonSuspendedMassFLCoGy = (double)info.GetValue("NonSuspendedMass_COG_FL_y", typeof(double));
            _NonSuspendedMassFLCoGz = (double)info.GetValue("NonSuspendedMass_COG_FL_z", typeof(double));

            _NonSuspendedMassFRCoGx = (double)info.GetValue("NonSuspendedMass_COG_FR_x", typeof(double));
            _NonSuspendedMassFRCoGy = (double)info.GetValue("NonSuspendedMass_COG_FR_y", typeof(double));
            _NonSuspendedMassFRCoGz = (double)info.GetValue("NonSuspendedMass_COG_FR_z", typeof(double));

            _NonSuspendedMassRLCoGx = (double)info.GetValue("NonSuspendedMass_COG_RL_x", typeof(double));
            _NonSuspendedMassRLCoGy = (double)info.GetValue("NonSuspendedMass_COG_RL_y", typeof(double));
            _NonSuspendedMassRLCoGz = (double)info.GetValue("NonSuspendedMass_COG_RL_z", typeof(double));

            _NonSuspendedMassRRCoGx = (double)info.GetValue("NonSuspendedMass_COG_RR_x", typeof(double));
            _NonSuspendedMassRRCoGy = (double)info.GetValue("NonSuspendedMass_COG_RR_y", typeof(double));
            _NonSuspendedMassRRCoGz = (double)info.GetValue("NonSuspendedMass_COG_RR_z", typeof(double));

            ChassisDataTableGUI = (DataTable)info.GetValue("ChassisDataTable", typeof(DataTable));

        } 
        #endregion

        #region Serialization of the Chassis GUI Object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Suspended_Mass", SuspendedMass);

            info.AddValue("NonSuspended_Mass_FL", _NonSuspendedMassFL);
            info.AddValue("NonSuspended_Mass_FR", _NonSuspendedMassFR);
            info.AddValue("NonSuspended_Mass_RL", _NonSuspendedMassRL);
            info.AddValue("NonSuspended_Mass_RR", _NonSuspendedMassRR);

            info.AddValue("SuspendedMass_COG_x", _SuspendedMassCoGx);
            info.AddValue("SuspendedMass_COG_y", _SuspendedMassCoGy);
            info.AddValue("SuspendedMass_COG_z", _SuspendedMassCoGz);

            info.AddValue("NonSuspendedMass_COG_FL_x", _NonSuspendedMassFLCoGx);
            info.AddValue("NonSuspendedMass_COG_FL_y", _NonSuspendedMassFLCoGy);
            info.AddValue("NonSuspendedMass_COG_FL_z", _NonSuspendedMassFLCoGz);

            info.AddValue("NonSuspendedMass_COG_FR_x", _NonSuspendedMassFRCoGx);
            info.AddValue("NonSuspendedMass_COG_FR_y", _NonSuspendedMassFRCoGy);
            info.AddValue("NonSuspendedMass_COG_FR_z", _NonSuspendedMassFRCoGz);

            info.AddValue("NonSuspendedMass_COG_RL_x", _NonSuspendedMassRLCoGx);
            info.AddValue("NonSuspendedMass_COG_RL_y", _NonSuspendedMassRLCoGy);
            info.AddValue("NonSuspendedMass_COG_RL_z", _NonSuspendedMassRLCoGz);

            info.AddValue("NonSuspendedMass_COG_RR_x", _NonSuspendedMassRRCoGx);
            info.AddValue("NonSuspendedMass_COG_RR_y", _NonSuspendedMassRRCoGy);
            info.AddValue("NonSuspendedMass_COG_RR_z", _NonSuspendedMassRRCoGz);

            info.AddValue("ChassisDataTable", ChassisDataTableGUI);
        } 
        #endregion
    }
}

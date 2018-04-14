using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This is Class representing the Chassis
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class Chassis : ISerializable,ICommand
    {
        #region Chassis Parameters Declaration
        public string _ChassisName { get; set; }
        public static int ChassisCounter = 0;
        public int ChassisID { get; set; }
        public bool ChassisIsModified { get; set; }
        public static int CurrentChassisID { get; set; }

        #region Suspned Mass Declaration
        public double SuspendedMass;
        #endregion

        #region Non Suspneded Mass Declaration
        public double NonSuspendedMassFL, NonSuspendedMassFR, NonSuspendedMassRL, NonSuspendedMassRR;
        #endregion

        #region Suspended Mass Centre of Gravity Coordinates Declaration
        public double SuspendedMassCoGx, SuspendedMassCoGy, SuspendedMassCoGz;
        #endregion

        #region Non Suspended Mass Centres of Gravity Coordinates Declaration
        public double NonSuspendedMassFLCoGx, NonSuspendedMassFLCoGy, NonSuspendedMassFLCoGz, NonSuspendedMassFRCoGx, NonSuspendedMassFRCoGy, NonSuspendedMassFRCoGz,
                      NonSuspendedMassRLCoGx, NonSuspendedMassRLCoGy, NonSuspendedMassRLCoGz, NonSuspendedMassRRCoGx, NonSuspendedMassRRCoGy, NonSuspendedMassRRCoGz;
        #endregion

        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsChassis = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsChassis = new Stack<ICommand>();
        #endregion

        #region Chassis Data Table
        public DataTable ChassisDataTable { get; set; }
        #endregion

        #region Declaration of the Global Array and Global List of the Chassis Object
        public static Chassis Assy_Chassis = new Chassis();
        public static List<Chassis> Assy_List_Chassis = new List<Chassis>();
        #endregion

        #region Constructors

        #region Base constructor
        public Chassis()
        {
            ///<summary>
            /// This constructor is created here only so that the Chassis object can be initialized without having to pass any arguments.
            /// This is needed because otherwise the chassis object will not be instantiated untill a chassis item is created. 
            /// If the user wants to save the file without creating a chassis object he will not be able to do so unless this constructor is used to create the chassis object 
            /// </summary

            ChassisIsModified = false;
        }
        #endregion

        #region Overloaded constructor
        public Chassis(ChassisGUI _chassisGUI)
        {
            ChassisDataTable = _chassisGUI.ChassisDataTableGUI;

            #region Chassis Parameter Intitialization
            #region Suspended Mass Initialization
            SuspendedMass = _chassisGUI.SuspendedMass;
            #endregion

            #region Non Suspended Masses Initialization
            NonSuspendedMassFL = _chassisGUI._NonSuspendedMassFL;
            NonSuspendedMassFR = _chassisGUI._NonSuspendedMassFR;
            NonSuspendedMassRL = _chassisGUI._NonSuspendedMassRL;
            NonSuspendedMassRR = _chassisGUI._NonSuspendedMassRR;
            #endregion

            #region Suspended Mass Centre of Gravity Coordinates Initialization
            SuspendedMassCoGx = _chassisGUI._SuspendedMassCoGy;
            SuspendedMassCoGy = _chassisGUI._SuspendedMassCoGz;
            SuspendedMassCoGz = _chassisGUI._SuspendedMassCoGx;
            #endregion

            #region Non Suspended Mass Centres of Gravity Coordinates Initialization
            NonSuspendedMassFLCoGx = _chassisGUI._NonSuspendedMassFLCoGy;
            NonSuspendedMassFLCoGy = _chassisGUI._NonSuspendedMassFLCoGz;
            NonSuspendedMassFLCoGz = _chassisGUI._NonSuspendedMassFLCoGx;

            NonSuspendedMassFRCoGx = _chassisGUI._NonSuspendedMassFRCoGy;
            NonSuspendedMassFRCoGy = _chassisGUI._NonSuspendedMassFRCoGz;
            NonSuspendedMassFRCoGz = _chassisGUI._NonSuspendedMassFRCoGx;

            NonSuspendedMassRLCoGx = _chassisGUI._NonSuspendedMassRLCoGy;
            NonSuspendedMassRLCoGy = _chassisGUI._NonSuspendedMassRLCoGz;
            NonSuspendedMassRLCoGz = _chassisGUI._NonSuspendedMassRLCoGx;

            NonSuspendedMassRRCoGx = _chassisGUI._NonSuspendedMassRRCoGy;
            NonSuspendedMassRRCoGy = _chassisGUI._NonSuspendedMassRRCoGz;
            NonSuspendedMassRRCoGz = _chassisGUI._NonSuspendedMassRRCoGx;
            #endregion

            #endregion

        }
        #endregion

        #endregion

        #region Create new Chassis method
        public void CreateNewChassis(int l_create_chassis, ChassisGUI create_chassisGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new tire and addes it to the List of tire objects 
            ///</summary>

            #region Adding the new chassis to the list of Chassis Objects
            ChassisGUI chassisGUI = create_chassisGUI_list;
            Assy_List_Chassis.Insert(l_create_chassis, new Chassis(chassisGUI));
            Assy_List_Chassis[l_create_chassis]._ChassisName = "Chassis " + Convert.ToString(l_create_chassis + 1);
            Assy_List_Chassis[l_create_chassis].ChassisID = l_create_chassis + 1;
            Assy_List_Chassis[l_create_chassis]._UndocommandsChassis = new Stack<ICommand>();
            Assy_List_Chassis[l_create_chassis]._RedocommandsChassis = new Stack<ICommand>();
            #endregion

        } 
        #endregion

        #region Modify Chassis AND Redo Method
        public void ModifyObjectData(int l_modify_chassis, object modify_chassis_list, bool redo_Identifier)
        {

            ///<summary>
            ///In this section of the code, the tire is bring modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>

            #region Editing the Chassis object to the List of Chassis Objects

            Chassis chassis_list;
            if (redo_Identifier==false)
            {
                chassis_list = new Chassis((ChassisGUI)modify_chassis_list);
            }
            else
            {
                chassis_list = (Chassis)modify_chassis_list;
            }

            ICommand cmd = Assy_List_Chassis[l_modify_chassis];
            Assy_List_Chassis[l_modify_chassis]._UndocommandsChassis.Push(cmd);

            chassis_list._UndocommandsChassis = Assy_List_Chassis[l_modify_chassis]._UndocommandsChassis;
            chassis_list._RedocommandsChassis = Assy_List_Chassis[l_modify_chassis]._RedocommandsChassis;
            chassis_list._ChassisName = Assy_List_Chassis[l_modify_chassis]._ChassisName;

            Assy_List_Chassis[l_modify_chassis] = chassis_list;
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable = chassis_list.ChassisDataTable;
            Assy_List_Chassis[l_modify_chassis].ChassisID = l_modify_chassis + 1;
            Assy_List_Chassis[l_modify_chassis].ChassisIsModified = true;

            PopulateDataTable(l_modify_chassis);

            if (redo_Identifier == false)
            {
                _RedocommandsChassis.Clear();
            }

            ChassisGUI.DisplayChassisItem(Assy_List_Chassis[l_modify_chassis]);

            Kinematics_Software_New.Chassis_ModifyInVehicle(l_modify_chassis, Assy_List_Chassis[l_modify_chassis]);

            #endregion
        }
        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexcute, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            try
            {
                #region Undoing the modification
                ICommand cmd = Assy_List_Chassis[l_unexcute];
                Assy_List_Chassis[l_unexcute]._RedocommandsChassis.Push(cmd);

                Assy_List_Chassis[l_unexcute] = (Chassis)command;

                PopulateDataTable(l_unexcute);

                ChassisGUI.DisplayChassisItem(Assy_List_Chassis[l_unexcute]);

                Kinematics_Software_New.Chassis_ModifyInVehicle(l_unexcute, Assy_List_Chassis[l_unexcute]);

                #endregion
            }
            catch (Exception) { }
        } 
        #endregion

        #region Populate Chassis Data Table Method
        private void PopulateDataTable(int l_modify_chassis)
        {
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[0].SetField<double>("Mass (Kg)", Assy_List_Chassis[l_modify_chassis].SuspendedMass);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_Chassis[l_modify_chassis].SuspendedMassCoGz);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_Chassis[l_modify_chassis].SuspendedMassCoGx);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_Chassis[l_modify_chassis].SuspendedMassCoGy);

            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[1].SetField<double>("Mass (Kg)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFL);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFLCoGz);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFLCoGx);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFLCoGy);

            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[2].SetField<double>("Mass (Kg)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFR);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFRCoGz);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFRCoGx);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassFRCoGy);

            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[3].SetField<double>("Mass (Kg)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRL);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRLCoGz);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRLCoGx);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRLCoGy);

            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[4].SetField<double>("Mass (Kg)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRR);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRRCoGz);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRRCoGx);
            Assy_List_Chassis[l_modify_chassis].ChassisDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_Chassis[l_modify_chassis].NonSuspendedMassRRCoGy);

        }  
        #endregion

        #region De-serialization of the Chassis Object's Data
        public Chassis(SerializationInfo info, StreamingContext context)
        {
            _ChassisName = (string)info.GetValue("Chassis_Name", typeof(string));

            SuspendedMass = (double)info.GetValue("Suspended_Mass", typeof(double));

            NonSuspendedMassFL = (double)info.GetValue("NonSuspended_Mass_FL", typeof(double));
            NonSuspendedMassFR = (double)info.GetValue("NonSuspended_Mass_FR", typeof(double));
            NonSuspendedMassRL = (double)info.GetValue("NonSuspended_Mass_RL", typeof(double));
            NonSuspendedMassRR = (double)info.GetValue("NonSuspended_Mass_RR", typeof(double));

            SuspendedMassCoGx = (double)info.GetValue("SuspendedMass_COG_x", typeof(double));
            SuspendedMassCoGy = (double)info.GetValue("SuspendedMass_COG_y", typeof(double));
            SuspendedMassCoGz = (double)info.GetValue("SuspendedMass_COG_z", typeof(double));

            NonSuspendedMassFLCoGx = (double)info.GetValue("NonSuspendedMass_COG_FL_x", typeof(double));
            NonSuspendedMassFLCoGy = (double)info.GetValue("NonSuspendedMass_COG_FL_y", typeof(double));
            NonSuspendedMassFLCoGz = (double)info.GetValue("NonSuspendedMass_COG_FL_z", typeof(double));

            NonSuspendedMassFRCoGx = (double)info.GetValue("NonSuspendedMass_COG_FR_x", typeof(double));
            NonSuspendedMassFRCoGy = (double)info.GetValue("NonSuspendedMass_COG_FR_y", typeof(double));
            NonSuspendedMassFRCoGz = (double)info.GetValue("NonSuspendedMass_COG_FR_z", typeof(double));

            NonSuspendedMassRLCoGx = (double)info.GetValue("NonSuspendedMass_COG_RL_x", typeof(double));
            NonSuspendedMassRLCoGy = (double)info.GetValue("NonSuspendedMass_COG_RL_y", typeof(double));
            NonSuspendedMassRLCoGz = (double)info.GetValue("NonSuspendedMass_COG_RL_z", typeof(double));

            NonSuspendedMassRRCoGx = (double)info.GetValue("NonSuspendedMass_COG_RR_x", typeof(double));
            NonSuspendedMassRRCoGy = (double)info.GetValue("NonSuspendedMass_COG_RR_y", typeof(double));
            NonSuspendedMassRRCoGz = (double)info.GetValue("NonSuspendedMass_COG_RR_z", typeof(double));

            ChassisDataTable = (DataTable)info.GetValue("ChassisDataTable", typeof(DataTable));

            ChassisID = (int)info.GetValue("Chassis_ID", typeof(int));
            CurrentChassisID = (int)info.GetValue("CurrentChassis_ID", typeof(int));
            ChassisCounter = (int)info.GetValue("Chassis_Counter", typeof(int));
        }


        #endregion

        #region Serialization of the Chassis Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Chassis_Name", _ChassisName);

            info.AddValue("Suspended_Mass", SuspendedMass);

            info.AddValue("NonSuspended_Mass_FL", NonSuspendedMassFL);
            info.AddValue("NonSuspended_Mass_FR", NonSuspendedMassFR);
            info.AddValue("NonSuspended_Mass_RL", NonSuspendedMassRL);
            info.AddValue("NonSuspended_Mass_RR", NonSuspendedMassRR);

            info.AddValue("SuspendedMass_COG_x", SuspendedMassCoGx);
            info.AddValue("SuspendedMass_COG_y", SuspendedMassCoGy);
            info.AddValue("SuspendedMass_COG_z", SuspendedMassCoGz);

            info.AddValue("NonSuspendedMass_COG_FL_x", NonSuspendedMassFLCoGx);
            info.AddValue("NonSuspendedMass_COG_FL_y", NonSuspendedMassFLCoGy);
            info.AddValue("NonSuspendedMass_COG_FL_z", NonSuspendedMassFLCoGz);

            info.AddValue("NonSuspendedMass_COG_FR_x", NonSuspendedMassFRCoGx);
            info.AddValue("NonSuspendedMass_COG_FR_y", NonSuspendedMassFRCoGy);
            info.AddValue("NonSuspendedMass_COG_FR_z", NonSuspendedMassFRCoGz);

            info.AddValue("NonSuspendedMass_COG_RL_x", NonSuspendedMassRLCoGx);
            info.AddValue("NonSuspendedMass_COG_RL_y", NonSuspendedMassRLCoGy);
            info.AddValue("NonSuspendedMass_COG_RL_z", NonSuspendedMassRLCoGz);

            info.AddValue("NonSuspendedMass_COG_RR_x", NonSuspendedMassRRCoGx);
            info.AddValue("NonSuspendedMass_COG_RR_y", NonSuspendedMassRRCoGy);
            info.AddValue("NonSuspendedMass_COG_RR_z", NonSuspendedMassRRCoGz);

            info.AddValue("ChassisDataTable", ChassisDataTable);

            info.AddValue("Chassis_ID", ChassisID);
            info.AddValue("CurrentChassis_ID", CurrentChassisID);
            info.AddValue("Chassis_Counter", ChassisCounter);
        } 
        #endregion
    }
}

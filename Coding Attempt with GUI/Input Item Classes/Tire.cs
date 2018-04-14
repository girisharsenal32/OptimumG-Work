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
    /// This is Class representing the Tire
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class Tire : ISerializable, ICommand
    {

        #region Tire Parameters Declaration
        public string _TireName { get; set; }
        public double TireRate { get; set; }
        public double TireWidth { get; set; }
        public int TireID { get; set; }
        public static int CurrentTireID = 0;
        public bool TireIsModified { get; set; }
        public static int TireCounter = 0;
        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsTire = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsTire = new Stack<ICommand>();
        #endregion

        #region Tire Data Table
        public DataTable TireDataTable { get; set; }
        #endregion

        #region Declaration and Initialization of the Global Array and Globl List of Tire Object
        public static Tire[] Assy_Tire = new Tire[4];
        public static List<Tire> Assy_List_Tire = new List<Tire>();
        #endregion 

        #region Constructors

        #region Base constructor
        public Tire()
        {
            TireIsModified = false;
        } 
        #endregion

        #region Overloaded constructor
        public Tire(TireGUI _tireGUI)
        {

            TireDataTable = _tireGUI.TireDataTableGUI;

            #region Tire Parameters Initialization
            TireRate = _tireGUI._TireRate;
            TireWidth = _tireGUI._TireWidth;
            #endregion

        }  
        #endregion

        #endregion

        #region Create New Tire Method
        public void CreateNewTire(int l_execute_tire, TireGUI execute_tireGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new tire and addes it to the List of tire objects 
            ///</summary>

            #region Adding the new Tire object to the List of Tire objects
            TireGUI tireGUI = execute_tireGUI_list;
            Assy_List_Tire.Insert(l_execute_tire, new Tire(tireGUI));
            Assy_List_Tire[l_execute_tire]._TireName = "Tire " + Convert.ToString(l_execute_tire + 1);
            Assy_List_Tire[l_execute_tire].TireID = l_execute_tire + 1;
            Assy_List_Tire[l_execute_tire]._UndocommandsTire = new Stack<ICommand>();
            Assy_List_Tire[l_execute_tire]._RedocommandsTire = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Modify Tire AND Redo Method
        public void ModifyObjectData(int l_execute_tire, Object execute_tire_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the tire is bring modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>

            #region Editing the Tire object to the List of Tire objects

            Tire tire_list;
            if (redo_Identifier == false)
            {
                tire_list = new Tire((TireGUI)execute_tire_list);
            }
            else
            {
                tire_list = (Tire)execute_tire_list;
            }

            ICommand cmd = Assy_List_Tire[l_execute_tire];
            Assy_List_Tire[l_execute_tire]._UndocommandsTire.Push(cmd);

            tire_list._UndocommandsTire = Assy_List_Tire[l_execute_tire]._UndocommandsTire;
            tire_list._RedocommandsTire = Assy_List_Tire[l_execute_tire]._RedocommandsTire;
            tire_list._TireName = Assy_List_Tire[l_execute_tire]._TireName;

            Assy_List_Tire[l_execute_tire] = tire_list;

            Assy_List_Tire[l_execute_tire].TireDataTable = tire_list.TireDataTable;
            //Assy_List_Tire[l_execute_tire]._TireName = "Tire " + Convert.ToString(l_execute_tire + 1);
            Assy_List_Tire[l_execute_tire].TireID = l_execute_tire + 1;
            Assy_List_Tire[l_execute_tire].TireIsModified = true;

            PopulateDataTable(l_execute_tire);

            #endregion

            if (redo_Identifier == false)
            {
                _RedocommandsTire.Clear();
            }

            TireGUI.DisplayTireItem(Assy_List_Tire[l_execute_tire]);

            Kinematics_Software_New.Tire_ModifyInVehicle(l_execute_tire, Assy_List_Tire[l_execute_tire]);

        } 
        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexecute_tire, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            try
            {
                #region Undo the tire modification
                ICommand cmd = Assy_List_Tire[l_unexecute_tire];
                Assy_List_Tire[l_unexecute_tire]._RedocommandsTire.Push(cmd);

                Assy_List_Tire[l_unexecute_tire] = (Tire)command;

                PopulateDataTable(l_unexecute_tire);

                TireGUI.DisplayTireItem(Assy_List_Tire[l_unexecute_tire]);

                Kinematics_Software_New.Tire_ModifyInVehicle(l_unexecute_tire, Assy_List_Tire[l_unexecute_tire]);


                #endregion
            }
            catch (Exception) { }

        } 
        #endregion

        #region Populate Data Table Method
        public void PopulateDataTable(int l_execute_tire)
        {
            Assy_List_Tire[l_execute_tire].TireDataTable.Rows[0].SetField<double>("Column 2", Assy_List_Tire[l_execute_tire].TireRate);
            Assy_List_Tire[l_execute_tire].TireDataTable.Rows[1].SetField<double>("Column 2", Assy_List_Tire[l_execute_tire].TireWidth);
        } 
        #endregion

        #region De-serialization of the Tire Object's Data
        public Tire(SerializationInfo info, StreamingContext context)
        {

            _TireName = (string)info.GetValue("Tire_Name", typeof(string));
            TireCounter = (int)info.GetValue("Tire_Counter", typeof(int));
            TireRate = (double)info.GetValue("Tire_Rate", typeof(double));
            TireID = (int)info.GetValue("Tire_ID", typeof(int));
            CurrentTireID = (int)info.GetValue("CurrentTire_ID", typeof(int));
            TireWidth = (double)info.GetValue("Tire_Width", typeof(double));
            TireDataTable = (DataTable)info.GetValue("TireDataTable", typeof(DataTable));
        }


        #endregion

        #region Serialization of the Tire Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Tire_Name", _TireName);
            info.AddValue("Tire_Rate", TireRate);
            info.AddValue("Tire_Width", TireWidth);
            info.AddValue("Tire_ID", TireID);
            info.AddValue("CurrentTire_ID", CurrentTireID);
            info.AddValue("Tire_Counter", TireCounter);
            info.AddValue("TireDataTable", TireDataTable);
        }
        #endregion


    }
}

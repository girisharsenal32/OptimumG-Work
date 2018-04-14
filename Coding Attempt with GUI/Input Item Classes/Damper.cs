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
    /// This is Class representing the Damper
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class Damper : ISerializable,ICommand
    {
        #region Damper Parameters Declaration
        public string _DamperName { get; set; }
        public double DamperGasPressure;
        public double DamperShaftDia;
        public double DamperFreeLength;
        public double DamperStroke;
        public int DamperID { get; set; }
        public static int CurrentDamperID { get; set; }
        public bool DamperIsModified { get; set; }
        public static int DamperCounter = 0;
        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsDamper = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsDamper = new Stack<ICommand>();
        #endregion

        #region Damper Data Table
        public DataTable DamperDataTable { get; set; }
        #endregion

        #region Declaration of the Global Array and Globl List of Damper Object
        public static Damper[] Assy_Damper = new Damper[4];
        public static List<Damper> Assy_List_Damper = new List<Damper>();
        #endregion

        #region Constructors

        #region Base Constructor
        public Damper()
        {
            DamperIsModified = false;
        }
        #endregion

        #region Overloaded Constructor
        public Damper(DamperGUI _damperGUI)
        {
            DamperDataTable = _damperGUI.DamperDataTableGUI;

            #region Damper Parameters Initialization
            DamperGasPressure = _damperGUI._DamperGasPressure;
            DamperShaftDia = _damperGUI._DamperShaftDia;
            DamperFreeLength = _damperGUI._DamperFreeLength;
            DamperStroke = _damperGUI._DamperStroke;
            #endregion
        }
        #endregion 

        #endregion

        #region Create Damper Method
        public void CreateNewDamper(int l_create_damper, DamperGUI create_damperGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new damper and addes it to the List of damper objects 
            ///</summary>

            #region Adding a new damper to the list of damper objects
            DamperGUI damperGUI = create_damperGUI_list;
            Assy_List_Damper.Insert(l_create_damper, new Damper(damperGUI));
            Assy_List_Damper[l_create_damper]._DamperName = "Damper " + Convert.ToString(l_create_damper + 1);
            Assy_List_Damper[l_create_damper].DamperID = l_create_damper + 1;
            Assy_List_Damper[l_create_damper]._UndocommandsDamper = new Stack<ICommand>();
            Assy_List_Damper[l_create_damper]._RedocommandsDamper = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Modify Damper AND Redo Method
        public void ModifyObjectData(int l_modify_damper, object modify_damper_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the tire is bring modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>


            #region Editing the Damper Object in the list of Damper Objects
            Damper damper_list;
            if (redo_Identifier == false)
            {
                damper_list = new Damper((DamperGUI)modify_damper_list);
            }
            else 
            {
                damper_list = (Damper)modify_damper_list; 
            }

            ICommand cmd = Assy_List_Damper[l_modify_damper];
            Assy_List_Damper[l_modify_damper]._UndocommandsDamper.Push(cmd);

            damper_list._UndocommandsDamper = Assy_List_Damper[l_modify_damper]._UndocommandsDamper;
            damper_list._RedocommandsDamper = Assy_List_Damper[l_modify_damper]._RedocommandsDamper;
            damper_list._DamperName = Assy_List_Damper[l_modify_damper]._DamperName;

            Assy_List_Damper[l_modify_damper] = damper_list;
            Assy_List_Damper[l_modify_damper].DamperDataTable = damper_list.DamperDataTable;
            Assy_List_Damper[l_modify_damper].DamperID = l_modify_damper + 1;
            Assy_List_Damper[l_modify_damper].DamperIsModified = true;

            PopulateDataTable(l_modify_damper);

            #endregion

            if (redo_Identifier == false)
            {
                _RedocommandsDamper.Clear();
            }

            DamperGUI.DisplayDamperItem(Assy_List_Damper[l_modify_damper]);

            Kinematics_Software_New.Damper_ModifyInVehicle(l_modify_damper, Assy_List_Damper[l_modify_damper]);


        }

        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexecute_damper, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            try
            {
                #region Undo the damper modification
                ICommand cmd = Assy_List_Damper[l_unexecute_damper];
                Assy_List_Damper[l_unexecute_damper]._RedocommandsDamper.Push(cmd);

                Assy_List_Damper[l_unexecute_damper] = (Damper)command;

                PopulateDataTable(l_unexecute_damper);

                DamperGUI.DisplayDamperItem(Assy_List_Damper[l_unexecute_damper]);

                Kinematics_Software_New.Damper_ModifyInVehicle(l_unexecute_damper, Assy_List_Damper[l_unexecute_damper]);

                #endregion

            }
            catch (Exception) { }
        } 
        #endregion

        #region Populate Data Table Method
        public void PopulateDataTable(int l_modify_damper)
        {
            Assy_List_Damper[l_modify_damper].DamperDataTable.Rows[0].SetField<double>("Column 2", Assy_List_Damper[l_modify_damper].DamperGasPressure);
            Assy_List_Damper[l_modify_damper].DamperDataTable.Rows[1].SetField<double>("Column 2", Assy_List_Damper[l_modify_damper].DamperShaftDia);
        }  
        #endregion


        #region De-serialization of the Damper Object's Data
        public Damper(SerializationInfo info, StreamingContext context)
        {

            _DamperName = (string)info.GetValue("Damper_Name", typeof(string));
            DamperGasPressure = (double)info.GetValue("Damper_GasPressure", typeof(double));
            DamperShaftDia = (double)info.GetValue("Damper_ShaftDia", typeof(double));
            DamperFreeLength = (double)info.GetValue("Damper_FreeLength", typeof(double));
            DamperStroke = (double)info.GetValue("Damper_Stroke", typeof(double));

            DamperID = (int)info.GetValue("Damper_ID", typeof(int));
            CurrentDamperID = (int)info.GetValue("CurrentDamper_ID", typeof(int));
            DamperCounter = (int)info.GetValue("Damper_Counter", typeof(int));
            DamperDataTable = (DataTable)info.GetValue("DamperDataTable", typeof(DataTable));
        } 
	    #endregion

        #region Serialization of the Damper Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Damper_Name", _DamperName);
            info.AddValue("Damper_GasPressure", DamperGasPressure);
            info.AddValue("Damper_ShaftDia", DamperShaftDia);
            info.AddValue("Damper_FreeLength", DamperFreeLength);
            info.AddValue("Damper_Stroke", DamperStroke);

            info.AddValue("Damper_ID", DamperID);
            info.AddValue("CurrentDamper_ID", CurrentDamperID);
            info.AddValue("Damper_Counter", DamperCounter);
            info.AddValue("DamperDataTable", DamperDataTable);
        } 
        #endregion


    }
}

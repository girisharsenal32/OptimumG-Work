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
    /// This is Class representing the Anti-Roll Bar
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class AntiRollBar : ISerializable, ICommand
    {
        #region Anti Roll Bate Stiffness Declaration
        public string _ARBName { get; set; }
        public double AntiRollBarRate;
        public double AntiRollBarRate_Nmm;
        public int AntiRollBarID { get; set; }
        public static int CurrentAntiRollBarID { get; set; }
        public bool AntiRollBarIsModified { get; set; }
        public static int AntiRollBarCounter = 0;
        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsARB = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsARB = new Stack<ICommand>();
        #endregion

        #region ARB Data Table
        public DataTable ARBDataTable { get; set; }
        #endregion

        #region Declaration of the Global Array and Globl List of ARB Object
        public static AntiRollBar[] Assy_ARB = new AntiRollBar[4];
        public static List<AntiRollBar> Assy_List_ARB = new List<AntiRollBar>();
        #endregion

        #region Constructors

        #region Base Constructor
        public AntiRollBar()
        {
            AntiRollBarIsModified = false;
        }
        #endregion

        #region Overloaded Constructor
        public AntiRollBar(AntiRollBarGUI _arbGUI)
        {
            ARBDataTable = _arbGUI.ARBDataTableGUI;

            #region Anti Roll Bar Stiffness Initialization
            AntiRollBarRate = _arbGUI._AntiRollBarRate;
            #endregion
        }
        #endregion

        #endregion

        #region Create new ARB Method
        public void CreateNewARB(int l_create_arb, AntiRollBarGUI create_arb_list)
        {
            ///<<summary>
            ///This section of the code creates a new tire and addes it to the List of tire objects 
            ///</summary>

            #region Adding a new ARB object to the List of ARB Objects
            AntiRollBarGUI arbGUI = create_arb_list;
            Assy_List_ARB.Insert(l_create_arb, new AntiRollBar(arbGUI));
            Assy_List_ARB[l_create_arb]._ARBName = "ARB " + Convert.ToString(l_create_arb + 1);
            Assy_List_ARB[l_create_arb].AntiRollBarID = l_create_arb + 1;
            Assy_List_ARB[l_create_arb]._UndocommandsARB = new Stack<ICommand>();
            Assy_List_ARB[l_create_arb]._RedocommandsARB = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Modify ARB AND Redo Method
        public void ModifyObjectData(int l_modify_arb, object modify_arb_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the tire is bring modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>

            #region Editing the ARB object in the list of ARB objects
            AntiRollBar arb_list;

            if (redo_Identifier == false)
            {
                arb_list = new AntiRollBar((AntiRollBarGUI)modify_arb_list);
            }
            else
            {
                arb_list = (AntiRollBar)modify_arb_list; 
            }

            ICommand cmd = Assy_List_ARB[l_modify_arb];
            Assy_List_ARB[l_modify_arb]._UndocommandsARB.Push(cmd);

            arb_list._UndocommandsARB = Assy_List_ARB[l_modify_arb]._UndocommandsARB;
            arb_list._RedocommandsARB = Assy_List_ARB[l_modify_arb]._RedocommandsARB;
            arb_list._ARBName = Assy_List_ARB[l_modify_arb]._ARBName;

            Assy_List_ARB[l_modify_arb] = arb_list;
            Assy_List_ARB[l_modify_arb].ARBDataTable = arb_list.ARBDataTable;
            Assy_List_ARB[l_modify_arb].AntiRollBarID = l_modify_arb + 1;
            Assy_List_ARB[l_modify_arb].AntiRollBarIsModified = true;

            PopulateDataTable(l_modify_arb);

            #endregion

            if (redo_Identifier == false)
            {
                _RedocommandsARB.Clear();
            }


            AntiRollBarGUI.DisplayARBItem(Assy_List_ARB[l_modify_arb]);

            Kinematics_Software_New.ARB_ModifyInVehicle(l_modify_arb, Assy_List_ARB[l_modify_arb]);

        }

        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexecute_arb, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            #region Undoing the modification
            try
            {
                ICommand cmd = Assy_List_ARB[l_unexecute_arb];
                Assy_List_ARB[l_unexecute_arb]._RedocommandsARB.Push(cmd);

                Assy_List_ARB[l_unexecute_arb] = (AntiRollBar)command;

                PopulateDataTable(l_unexecute_arb);

                AntiRollBarGUI.DisplayARBItem(Assy_List_ARB[l_unexecute_arb]);

                Kinematics_Software_New.ARB_ModifyInVehicle(l_unexecute_arb, Assy_List_ARB[l_unexecute_arb]);

            }
            catch (Exception) { }
            #endregion
        } 
        #endregion

        #region Populate Data Table Method
        public void PopulateDataTable(int l_modify_arb)
        {
            Assy_List_ARB[l_modify_arb].ARBDataTable.Rows[0].SetField<double>("Column 2", Assy_List_ARB[l_modify_arb].AntiRollBarRate);
        }  
        #endregion

        #region De-serialization of the AntiRollBar Object's Data
        public AntiRollBar(SerializationInfo info, StreamingContext context)
        {
            _ARBName = (string)info.GetValue("ARB_Name", typeof(string));
            AntiRollBarRate = (double)info.GetValue("ARB_Rate", typeof(double));
            AntiRollBarRate_Nmm = (double)info.GetValue("ARB_Rate_Nmm", typeof(double));
            AntiRollBarID = (int)info.GetValue("ARB_ID", typeof(int));
            CurrentAntiRollBarID = (int)info.GetValue("CurrentARB_ID", typeof(int));
            AntiRollBarCounter = (int)info.GetValue("ARB_Counter", typeof(int));
            ARBDataTable = (DataTable)info.GetValue("ARBDataTable", typeof(DataTable));
        }
        #endregion

        #region Serialization of the AntiRollBar Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ARB_Name", _ARBName);
            info.AddValue("ARB_Rate", AntiRollBarRate);
            info.AddValue("ARB_Rate_Nmm", AntiRollBarRate_Nmm);
            info.AddValue("ARB_ID", AntiRollBarID);
            info.AddValue("CurrentARB_ID", CurrentAntiRollBarID);
            info.AddValue("ARB_Counter", AntiRollBarCounter);
            info.AddValue("ARBDataTable", ARBDataTable);
        }
        #endregion


    }
}

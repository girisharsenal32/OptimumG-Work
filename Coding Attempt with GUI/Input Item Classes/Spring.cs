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
    /// This is Class representing the Spring
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class Spring : ISerializable,ICommand
    {

        #region Spring Parameters Declaration
        public string _SpringName { get; set; }
        public double SpringRate;
        public double SpringPreload;
        public double PreloadForce;
        public double SpringFreeLength;
        public int SpringID { get; set; }
        public static int CurrentSpringID = 0;
        public bool SpringIsModified { get; set; }
        public static int SpringCounter = 0;
        #endregion

        #region Undo/Redo Stacks
        public Stack<ICommand> _UndocommandsSpring = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsSpring = new Stack<ICommand>();
        #endregion

        #region Spring Data Table
        public DataTable SpringDataTable { get; set; }
        #endregion

        #region Declaration of the Global Array and Globl List of Spring Object
        public static Spring[] Assy_Spring = new Spring[4];
        public static List<Spring> Assy_List_Spring = new List<Spring>();
        #endregion

        #region Constructors

        #region Base Constructor
        public Spring()
        {
            SpringIsModified = false;
        } 
        #endregion

        #region Overloaded Constructor
        public Spring(SpringGUI _springGUI)
        {
            SpringDataTable = _springGUI.SpringDataTableGUI;

            #region Spring Parameters Initialization
            SpringRate = _springGUI._SpringRate;
            SpringPreload = _springGUI._SpringPreload;
            SpringFreeLength = _springGUI._SpringFreeLength;
            PreloadForce = SpringRate * SpringPreload;
            #endregion
        }  
        #endregion

        #endregion

        #region Create New Spring Method
        public void CreateNewSpring(int l_execute_spring, SpringGUI execute_springGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new Spring and addes it to the List of Spring objects 
            ///</summary>

            #region Adding a new Spring to List of Spring Objects
            SpringGUI springGUI = execute_springGUI_list;
            Assy_List_Spring.Insert(l_execute_spring, new Spring(springGUI));
            Assy_List_Spring[l_execute_spring]._SpringName = "Spring " + Convert.ToString(l_execute_spring + 1);
            Assy_List_Spring[l_execute_spring].SpringID = l_execute_spring + 1;
            Assy_List_Spring[l_execute_spring]._UndocommandsSpring = new Stack<ICommand>();
            Assy_List_Spring[l_execute_spring]._RedocommandsSpring = new Stack<ICommand>();
            #endregion

        } 
        #endregion

        #region Modify Spring Method
        public void ModifyObjectData(int l_execute_spring, Object execute_spring_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the Spring is bring modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>

            #region Adding a new Spring to List of Spring Objects
            Spring spring_list;

            if (redo_Identifier==false)
            {
                spring_list = new Spring((SpringGUI)execute_spring_list);
            }
            else
            {
                spring_list = (Spring)execute_spring_list;
            }

            ICommand cmd = Assy_List_Spring[l_execute_spring];
            Assy_List_Spring[l_execute_spring]._UndocommandsSpring.Push(cmd);

            spring_list._UndocommandsSpring = Assy_List_Spring[l_execute_spring]._UndocommandsSpring;
            spring_list._RedocommandsSpring = Assy_List_Spring[l_execute_spring]._RedocommandsSpring;
            spring_list._SpringName = Assy_List_Spring[l_execute_spring]._SpringName;
            
            Assy_List_Spring[l_execute_spring] = spring_list;
            Assy_List_Spring[l_execute_spring].SpringDataTable = spring_list.SpringDataTable;
            Assy_List_Spring[l_execute_spring].SpringID = l_execute_spring + 1;
            Assy_List_Spring[l_execute_spring].SpringIsModified = true;


            PopulateDataTable(l_execute_spring);

            if (redo_Identifier == false)
            {
                _RedocommandsSpring.Clear();
            }

            SpringGUI.DisplaySpringItem(Assy_List_Spring[l_execute_spring]);

            Kinematics_Software_New.Sring_ModifyInVehicle(l_execute_spring, Assy_List_Spring[l_execute_spring]);

            #endregion
        }

        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexecute_spring, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            #region Undo the Spring modification
            try
            {
                ICommand cmd = Assy_List_Spring[l_unexecute_spring];
                Assy_List_Spring[l_unexecute_spring]._RedocommandsSpring.Push(cmd);

                Assy_List_Spring[l_unexecute_spring] = (Spring)command;

                PopulateDataTable(l_unexecute_spring);

                SpringGUI.DisplaySpringItem(Assy_List_Spring[l_unexecute_spring]);

                Kinematics_Software_New.Sring_ModifyInVehicle(l_unexecute_spring, Assy_List_Spring[l_unexecute_spring]);

            }
            catch (Exception) { }
            #endregion
        } 
        #endregion

        #region Popluate Data Table Method
        private void PopulateDataTable(int l_execute_spring)
        {
            Assy_List_Spring[l_execute_spring].SpringDataTable.Rows[0].SetField<double>("Column 2", Assy_List_Spring[l_execute_spring].SpringRate);
            Assy_List_Spring[l_execute_spring].SpringDataTable.Rows[1].SetField<double>("Column 2", Assy_List_Spring[l_execute_spring].SpringPreload);
            Assy_List_Spring[l_execute_spring].SpringDataTable.Rows[2].SetField<double>("Column 2", Assy_List_Spring[l_execute_spring].SpringFreeLength);
        }  
        #endregion

        #region De-serialization of the Spring Object's Data
        public Spring(SerializationInfo info, StreamingContext context)
        {
            _SpringName = (string)info.GetValue("Spring_Name", typeof(string));
            SpringRate = (double)info.GetValue("Spring_Rate", typeof(double));
            SpringPreload = (double)info.GetValue("Spring_Preload", typeof(double));
            SpringFreeLength = (double)info.GetValue("Spring_FreeLength", typeof(double));
            PreloadForce = (double)info.GetValue("Preload_Force", typeof(double));
            SpringID = (int)info.GetValue("Spring_ID", typeof(int));
            CurrentSpringID = (int)info.GetValue("CurrentSpring_ID", typeof(int));
            SpringCounter = (int)info.GetValue("Spring_Counter", typeof(int));
            SpringDataTable = (DataTable)info.GetValue("SpringDataTable", typeof(DataTable));
        }

        #endregion

        #region Serialization of the Spring Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {   
            info.AddValue("Spring_Name", _SpringName);
            info.AddValue("Spring_Rate", SpringRate);
            info.AddValue("Spring_Preload", SpringPreload);
            info.AddValue("Spring_FreeLength", SpringFreeLength);
            info.AddValue("Preload_Force", PreloadForce);
            info.AddValue("Spring_ID", SpringID);
            info.AddValue("CurrentSpring_ID", CurrentSpringID);
            info.AddValue("Spring_Counter", SpringCounter);
            info.AddValue("SpringDataTable", SpringDataTable);
        }
        #endregion

    }
}

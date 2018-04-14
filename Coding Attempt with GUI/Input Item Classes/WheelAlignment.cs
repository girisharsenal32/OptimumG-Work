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
    /// This is Class representing the Wheel Alignment
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class WheelAlignment : ISerializable,ICommand
    {
        #region Static Camber and Toe Declaration
        public string _WAName { get; set; }
        public double StaticCamber, StaticToe, SteeringRatio;
        public int WheelAlignmentID { get; set; }
        public static int CurrentWheelAlignmentID { get; set; }
        public bool WheelAlignmentIsModified { get; set; }
        public bool WheelIsSteered { get; set; }
        public static int WheelAlignmentCounter = 0;    
        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsWheelAlignment = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsWheelAlignment = new Stack<ICommand>();
        #endregion

        #region WheelAlignment Data Table
        public DataTable WADataTable{ get; set; }
        #endregion

        #region Declaration of the Global Array and Global List of the Wheel Alignment Object
        public static WheelAlignment[] Assy_WA = new WheelAlignment[4];
        public static List<WheelAlignment> Assy_List_WA = new List<WheelAlignment>();
        #endregion

        #region Constructor

        #region Base Constructor
        public WheelAlignment()
        {
            WheelAlignmentIsModified = false;
        }
        #endregion

        #region Overloaded Constructor
        public WheelAlignment(WheelAlignmentGUI _waGUI)
        {

            //Sign Convention is same as that mentioned in the Optimum Kinematics File 

            WADataTable = _waGUI.WADataTableGUI;

            #region Static Camber and Toe Initialization
            StaticCamber = _waGUI._StaticCamber /** (Math.PI / 180)*/;
            StaticToe = _waGUI._StaticToe /** (Math.PI / 180)*/;
            SteeringRatio = _waGUI._SteeringRatio;
            #endregion

        }
        #endregion
        
        #endregion

        #region Create New Wheel Alignment method
        public void CreateNewWheelAlignment(int l_create_wa, WheelAlignmentGUI create_waGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new tire and addes it to the List of tire objects 
            ///</summary>

            #region Adding the new Wheel Alignment object to the list of Wheel Alignment objects
            WheelAlignmentGUI waGUI = create_waGUI_list;
            Assy_List_WA.Insert(l_create_wa, new WheelAlignment(waGUI));
            Assy_List_WA[l_create_wa]._WAName = "WA " + Convert.ToString(l_create_wa + 1);
            Assy_List_WA[l_create_wa].WheelAlignmentID = l_create_wa + 1;
            Assy_List_WA[l_create_wa]._UndocommandsWheelAlignment = new Stack<ICommand>();
            Assy_List_WA[l_create_wa]._RedocommandsWheelAlignment = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Modify Wheel Alignment Method
        public void ModifyObjectData(int l_modify_wa, object modify_wa_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the tire is bring modified and it is placed under the method called Execute because it is an Undoable operation 
            ///</summary>

            #region Editing the Wheel Alignment Object
            WheelAlignment wa_list;

            if (redo_Identifier == false)
            {
                wa_list = new WheelAlignment((WheelAlignmentGUI)modify_wa_list);
            }
            else
            {
                wa_list = (WheelAlignment)modify_wa_list;
            }

            ICommand cmd = Assy_List_WA[l_modify_wa];
            Assy_List_WA[l_modify_wa]._UndocommandsWheelAlignment.Push(cmd);

            wa_list._UndocommandsWheelAlignment = Assy_List_WA[l_modify_wa]._UndocommandsWheelAlignment;
            wa_list._RedocommandsWheelAlignment = Assy_List_WA[l_modify_wa]._RedocommandsWheelAlignment;
            wa_list._WAName = Assy_List_WA[l_modify_wa]._WAName;

            Assy_List_WA[l_modify_wa] = wa_list;
            Assy_List_WA[l_modify_wa].WADataTable = wa_list.WADataTable;
            Assy_List_WA[l_modify_wa].WheelAlignmentID = l_modify_wa + 1;
            Assy_List_WA[l_modify_wa].WheelAlignmentIsModified = true;

            PopulateDataTable(l_modify_wa);

            if (redo_Identifier == false)
            {
                _RedocommandsWheelAlignment.Clear();
            }

            WheelAlignmentGUI.DisplayWheelAlignmentItem(Assy_List_WA[l_modify_wa]);

            Kinematics_Software_New.WA_ModifyInVehicle(l_modify_wa, Assy_List_WA[l_modify_wa]);

            #endregion
        }
        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexcute_wa, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            try
            {
                #region Undo the wheel alignment modification
                ICommand cmd = Assy_List_WA[l_unexcute_wa];
                Assy_List_WA[l_unexcute_wa]._RedocommandsWheelAlignment.Push(cmd);

                Assy_List_WA[l_unexcute_wa] = (WheelAlignment)command;

                PopulateDataTable(l_unexcute_wa);

                WheelAlignmentGUI.DisplayWheelAlignmentItem(Assy_List_WA[l_unexcute_wa]);

                Kinematics_Software_New.WA_ModifyInVehicle(l_unexcute_wa, Assy_List_WA[l_unexcute_wa]);

                #endregion

            }
            catch (Exception) { }
        } 
        #endregion

        #region Populate Data Table Method
        public void PopulateDataTable(int l_modify_wa)
        {
            Assy_List_WA[l_modify_wa].WADataTable.Rows[0].SetField<double>("Column 2", Assy_List_WA[l_modify_wa].StaticCamber);
            Assy_List_WA[l_modify_wa].WADataTable.Rows[1].SetField<double>("Column 2", Assy_List_WA[l_modify_wa].StaticToe);
        } 
        #endregion

        #region De-serialization of the WheelAlignment Object's Data
        public WheelAlignment(SerializationInfo info, StreamingContext context)
        {
            _WAName = (string)info.GetValue("WA_Name", typeof(string));
            StaticCamber = (double)info.GetValue("Static_Camber", typeof(double));
            StaticToe = (double)info.GetValue("Static_Toe", typeof(double));
            SteeringRatio = (double)info.GetValue("Steering_Ratio", typeof(double));
            WheelAlignmentID = (int)info.GetValue("WA_ID", typeof(int));
            CurrentWheelAlignmentID = (int)info.GetValue("CurrentWA_ID", typeof(int));
            WheelAlignmentCounter = (int)info.GetValue("WA_Counter", typeof(int));
            WADataTable = (DataTable)info.GetValue("WADataTable", typeof(DataTable));
        } 
        #endregion

        #region Serialization of the WheelAlignment Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("WA_Name", _WAName);
            info.AddValue("Static_Camber", StaticCamber);
            info.AddValue("Static_Toe", StaticToe);
            info.AddValue("Steering_Ratio", SteeringRatio);
            info.AddValue("WA_ID", WheelAlignmentID);
            info.AddValue("CurrentWA_ID", CurrentWheelAlignmentID);
            info.AddValue("WA_Counter", WheelAlignmentCounter);
            info.AddValue("WADataTable", WADataTable);
        } 
        #endregion


    }
}

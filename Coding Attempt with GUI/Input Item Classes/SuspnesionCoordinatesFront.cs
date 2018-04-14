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
    /// This is Class representing the Front Left Suspension Coordinate
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesFront : SuspensionCoordinatesMaster, ISerializable,ICommand
    {
        #region Front Left Suspension Parameters
        public int SCFL_ID { get; set; }
        public static int SCFLCurrentID { get; set; }
        public bool SCFLIsModified { get; set; }

        public static int SCFLCounter = 0;

        public static bool IsUndoRedoCalledByRight = false;
        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsSCFL = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsSCFL = new Stack<ICommand>();
        #endregion

        #region SCFL Data Table
        public DataTable SCFLDataTable { get; set; }
        #endregion

        #region Declaration and Initialization of the Global Array and Globl List of Suspension Coordinate Objects
        public static List<SuspensionCoordinatesFront> Assy_List_SCFL = new List<SuspensionCoordinatesFront>();
        #endregion

        #region Constructor

        #region Base Constructor
        public SuspensionCoordinatesFront()
        {
            SCFLIsModified = false;
        } 
        #endregion

        #region Overloaded Constructor
        public SuspensionCoordinatesFront(SuspensionCoordinatesFrontGUI _scflGUI)
        {
            SCFLDataTable = _scflGUI.SCFLDataTableGUI;

            #region Front Left Cooridinates Initialization

            #region Input Origin Initalization
            InputOriginX = _scflGUI._InputOriginX;
            InputOriginY = _scflGUI._InputOriginY;
            InputOriginZ = _scflGUI._InputOriginZ;
            #endregion

            #region Fixed Points FRONT LEFT Initialization - Double Wishbone & McPherson
            //  Coordinates of Fixed Point A
            A1x = _scflGUI.A1y;
            A1y = _scflGUI.A1z;
            A1z = _scflGUI.A1x;

            //  Coordinates of Fixed Point B
            B1x = _scflGUI.B1y;
            B1y = _scflGUI.B1z;
            B1z = _scflGUI.B1x;

            //  Coordinates of Fixed Point C
            C1x = _scflGUI.C1y;
            C1y = _scflGUI.C1z;
            C1z = _scflGUI.C1x;

            //  Coordinates of Fixed Point D
            D1x = _scflGUI.D1y;
            D1y = _scflGUI.D1z;
            D1z = _scflGUI.D1x;

            // Initial Coordinates of Moving Point I
            I1x = _scflGUI.I1y;
            I1y = _scflGUI.I1z;
            I1z = _scflGUI.I1x;

            // Initial Coordinates of Moving Point Jo
            JO1x = _scflGUI.JO1y;
            JO1y = _scflGUI.JO1z;
            JO1z = _scflGUI.JO1x;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            N1x = _scflGUI.N1y;
            N1y = _scflGUI.N1z;
            N1z = _scflGUI.N1x;

            // Initial Coordinates of Fixed point Pin1
            Pin1x = _scflGUI.Pin1y;
            Pin1y = _scflGUI.Pin1z;
            Pin1z = _scflGUI.Pin1x;

            // Initial Coordinates of Fixed point UV1
            UV1x = _scflGUI.UV1y;
            UV1y = _scflGUI.UV1z;
            UV1z = _scflGUI.UV1x;

            // Initial Coordinates of Fixed point UV2
            UV2x = _scflGUI.UV2y;
            UV2y = _scflGUI.UV2z;
            UV2z = _scflGUI.UV2x;

            // Initial Coordinates of Fixed point STC1
            STC1x = _scflGUI.STC1y;
            STC1y = _scflGUI.STC1z;
            STC1z = _scflGUI.STC1x;

            //  Coordinates of Fixed Point Q
            Q1x = _scflGUI.Q1y;
            Q1y = _scflGUI.Q1z;
            Q1z = _scflGUI.Q1x;

            // Coordinates of Fixed Point R
            R1x = _scflGUI.R1y;
            R1y = _scflGUI.R1z;
            R1z = _scflGUI.R1x;

            #endregion

            #region Moving Points FRONT LEFT Initialization - Double Wishbone & McPherson
            // Initial Coordinates of Moving Point J
            J1x = _scflGUI.J1y;
            J1y = _scflGUI.J1z;
            J1z = _scflGUI.J1x;

            // Initial Coordinates of Moving Point H
            H1x = _scflGUI.H1y;
            H1y = _scflGUI.H1z;
            H1z = _scflGUI.H1x;

            // Initial Coordinates of Moving Point G
            G1x = _scflGUI.G1y;
            G1y = _scflGUI.G1z;
            G1z = _scflGUI.G1x;

            // Initial Coordinates of Moving Point F
            F1x = _scflGUI.F1y;
            F1y = _scflGUI.F1z;
            F1z = _scflGUI.F1x;

            // Initial Coordinates of Moving Point E
            E1x = _scflGUI.E1y;
            E1y = _scflGUI.E1z;
            E1z = _scflGUI.E1x;

            // Initial Coordinates of Moving Point K
            K1x = _scflGUI.K1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            K1y = _scflGUI.K1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            K1z = _scflGUI.K1x;

            // Initial Coordinates of Moving Point L
            L1x = _scflGUI.W1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            L1y = _scflGUI.W1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            L1z = _scflGUI.W1x;

            // Initial Coordinates of Moving Point M
            M1x = _scflGUI.M1y;
            M1y = _scflGUI.M1z;
            M1z = _scflGUI.M1x;

            // Initial Coordinates of Moving Point O
            O1x = _scflGUI.O1y;
            O1y = _scflGUI.O1z;
            O1z = _scflGUI.O1x;

            // Initial Coordinates of Moving Point P
            P1x = _scflGUI.P1y;
            P1y = _scflGUI.P1z;
            P1z = _scflGUI.P1x;

            //  Coordinates of Moving Contact Patch Point W
            W1x = _scflGUI.W1y;
            W1y = _scflGUI.W1z;
            W1z = _scflGUI.W1x;

            //  Ride Height Reference Points
            RideHeightRefx = _scflGUI.RideHeightRefy;
            RideHeightRefy = _scflGUI.RideHeightRefz;
            RideHeightRefz = _scflGUI.RideHeightRefx;
            #endregion

            #region Link Lengths Calculations
            //Link Lengths
            LowerFrontLength = (Math.Sqrt(Math.Pow(D1x - E1x, 2) + Math.Pow(D1y - E1y, 2) + Math.Pow(D1z - E1z, 2)));
            LowerRearLength = (Math.Sqrt(Math.Pow(C1x - E1x, 2) + Math.Pow(C1y - E1y, 2) + Math.Pow(C1z - E1z, 2)));
            UpperFrontLength = (Math.Sqrt(Math.Pow(A1x - F1x, 2) + Math.Pow(A1y - F1y, 2) + Math.Pow(A1z - F1z, 2)));
            UpperRearLength = (Math.Sqrt(Math.Pow(B1x - F1x, 2) + Math.Pow(B1y - F1y, 2) + Math.Pow(B1z - F1z, 2)));
            PushRodLength = (Math.Sqrt(Math.Pow(H1x - G1x, 2) + Math.Pow(H1y - G1y, 2) + Math.Pow(H1z - G1z, 2)));
            PushRodLength_1 = (Math.Sqrt(Math.Pow(H1x - G1x, 2) + Math.Pow(H1y - G1y, 2) + Math.Pow(H1z - G1z, 2)));
            ToeLinkLength = (Math.Sqrt(Math.Pow(N1x - M1x, 2) + Math.Pow(N1y - M1y, 2) + Math.Pow(N1z - M1z, 2)));
            DamperLength = (Math.Sqrt(Math.Pow(J1x - JO1x, 2) + Math.Pow(J1y - JO1y, 2) + Math.Pow(J1z - JO1z, 2)));
            ARBDroopLinkLength = (Math.Sqrt(Math.Pow(O1x - P1x, 2) + Math.Pow(O1y - P1y, 2) + Math.Pow(O1z - P1z, 2)));
            ARBBladeLength = (Math.Sqrt(Math.Pow(P1x - Q1x, 2) + Math.Pow(P1y - Q1y, 2) + Math.Pow(P1z - Q1z, 2)));
            #endregion

            ///< remarks > This list is going to be populated from the index number 1 as the delta of Wheel Deflection is 0 for the 0th Index </ remarks >
            //WheelDeflection_Steering.Insert(0, 0);
            #endregion
        }  
        #endregion

        #endregion

        #region Create New SCFL Method
        public void CreateNewSCFL(int i_create_scfl, SuspensionCoordinatesFrontGUI create_scflGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new SCFL and addes it to the List of SCFL objects 
            ///</summary>

            #region Adding new SCFL object to the list of SCFL Objects
            SuspensionCoordinatesFrontGUI scflGUI = create_scflGUI_list;
            Assy_List_SCFL.Insert(i_create_scfl, new SuspensionCoordinatesFront(scflGUI));
            Assy_List_SCFL[i_create_scfl].FrontSuspensionType(scflGUI);
            Assy_List_SCFL[i_create_scfl]._SCName = "Front Left Coordinates " + Convert.ToString(i_create_scfl + 1);
            Assy_List_SCFL[i_create_scfl].SCFL_ID = i_create_scfl + 1;
            Assy_List_SCFL[i_create_scfl]._UndocommandsSCFL = new Stack<ICommand>();
            Assy_List_SCFL[i_create_scfl]._RedocommandsSCFL = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Redo Method
        public void ModifyObjectData(int l_modify_SCFL, object modify_scfl_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the Suspension is bring modified and it is placed under the method called ModifyObjectData because it is an Undoable operation 
            ///</summary>

            #region Redo the Modification
            SuspensionCoordinatesFront _scfl_forRedo = (SuspensionCoordinatesFront)modify_scfl_list;

            ICommand cmd = Assy_List_SCFL[l_modify_SCFL];
            Assy_List_SCFL[l_modify_SCFL]._UndocommandsSCFL.Push(cmd);

            Assy_List_SCFL[l_modify_SCFL] = _scfl_forRedo;

            PopulateDataTable(l_modify_SCFL);

            Assy_List_SCFL[l_modify_SCFL].SCFLIsModified = true;

            SuspensionCoordinatesFrontGUI.DisplaySCFLItem(Assy_List_SCFL[l_modify_SCFL]);

            #region Calling Redo method for Opposite Suspension if symmetric
            if (Assy_List_SCFL[l_modify_SCFL].FrontSymmetry == true && IsUndoRedoCalledByRight == false)
            {
                SuspensionCoordinatesFrontRight.IsUndoRedoCalledByLeft_IdentifierMethod(true);// This method sets the IsUndoRedoCalledByLeft variable to true and prevents an infinte loop

                UndoRedo undoRedo = new UndoRedo();
                undoRedo.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_modify_SCFL]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_modify_SCFL]._RedocommandsSCFR,
                                    l_modify_SCFL + 1, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_modify_SCFL].SCFRIsModified);
                undoRedo.Redo(1);
                SuspensionCoordinatesFrontRight.IsUndoRedoCalledByLeft_IdentifierMethod(false);//This method sets the value of IsUndoRedoCalledByLeft to false so that the Right Suspension coordinate can also be Undone

            } 
            #endregion

            Kinematics_Software_New.EditFrontCAD(l_modify_SCFL);

            Kinematics_Software_New.SCFL_ModifyInVehicle(l_modify_SCFL, Assy_List_SCFL[l_modify_SCFL]);


            #endregion

        } 
        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexcute_scfl, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            #region Undo the Modification
            try
            {
                SuspensionCoordinatesFront _scfl_forUndo = (SuspensionCoordinatesFront)command;

                ICommand cmd = Assy_List_SCFL[l_unexcute_scfl];
                Assy_List_SCFL[l_unexcute_scfl]._RedocommandsSCFL.Push(cmd);

                Assy_List_SCFL[l_unexcute_scfl] = _scfl_forUndo;

                PopulateDataTable(l_unexcute_scfl);

                SuspensionCoordinatesFrontGUI.DisplaySCFLItem(Assy_List_SCFL[l_unexcute_scfl]);

                #region Calling Undo method for Opposite Suspension if symmetric
                if (Assy_List_SCFL[l_unexcute_scfl].FrontSymmetry == true && IsUndoRedoCalledByRight == false)
                {
                    SuspensionCoordinatesFrontRight.IsUndoRedoCalledByLeft_IdentifierMethod(true);// This method sets the IsUndoRedoCalledByLeft variable to true and prevents an infinte loop

                    UndoRedo undoRedo = new UndoRedo();
                    undoRedo.Identifier(SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_unexcute_scfl]._UndocommandsSCFR, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_unexcute_scfl]._RedocommandsSCFR,
                                        l_unexcute_scfl + 1, SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_unexcute_scfl].SCFRIsModified);
                    undoRedo.Undo(1);
                    SuspensionCoordinatesFrontRight.IsUndoRedoCalledByLeft_IdentifierMethod(false);//This method sets the value of IsUndoRedoCalledByLeft to false so that the Right Suspension coordinate can also be Undone
                } 
                #endregion

                Kinematics_Software_New.EditFrontCAD(l_unexcute_scfl);

                Kinematics_Software_New.SCFL_ModifyInVehicle(l_unexcute_scfl, Assy_List_SCFL[l_unexcute_scfl]);


            }
            catch (Exception) { }
            #endregion
        }
        #endregion

        public static void IsUndoRedoCalledByRight_IdentifierMethod(bool value) => IsUndoRedoCalledByRight = value;

        #region Method to assign the identifiers which will establish the type of suspension
        public void FrontSuspensionType(SuspensionCoordinatesFrontGUI _scflGUI)
        {
            #region Determining the Suspension Type using the GUI Object
            SuspensionMotionExists = _scflGUI.SuspensionMotionExists;

            FrontSymmetry = _scflGUI.FrontSymmetryGUI;

            DoubleWishboneIdentifierFront = _scflGUI.DoubleWishboneIdentifierFront;
            McPhersonIdentifierFront = _scflGUI.McPhersonIdentifierFront;

            PushrodIdentifierFront = _scflGUI.PushrodIdentifierFront;
            PullrodIdentifierFront = _scflGUI.PullrodIdentifierFront;

            UARBIdentifierFront = _scflGUI.UARBIdentifierFront;
            TARBIdentifierFront = _scflGUI.TARBIdentifierFront;

            NoOfCouplings = _scflGUI.NoOfCouplings;

            #endregion
        } 
        #endregion

        #region Method to Edit the Front Left Suspension
        public void EditFrontLeftSuspension(int l_edit_scfl, SuspensionCoordinatesFrontGUI _scflGUI)
        {
            ICommand cmd = Assy_List_SCFL[l_edit_scfl];
            Assy_List_SCFL[l_edit_scfl]._UndocommandsSCFL.Push(cmd);

            #region Front Left Cooridinates Editing
            SuspensionCoordinatesFront scfl_list = new SuspensionCoordinatesFront(_scflGUI);
            scfl_list._UndocommandsSCFL = Assy_List_SCFL[l_edit_scfl]._UndocommandsSCFL;
            scfl_list._RedocommandsSCFL = Assy_List_SCFL[l_edit_scfl]._RedocommandsSCFL;
            scfl_list._SCName = Assy_List_SCFL[l_edit_scfl]._SCName;

            Assy_List_SCFL[l_edit_scfl] = scfl_list;
            Assy_List_SCFL[l_edit_scfl].SCFLDataTable = scfl_list.SCFLDataTable;
            Assy_List_SCFL[l_edit_scfl].SCFL_ID = l_edit_scfl + 1;
            Assy_List_SCFL[l_edit_scfl].FrontSuspensionType(_scflGUI);
            Assy_List_SCFL[l_edit_scfl].SCFLIsModified = true;

            PopulateDataTable(l_edit_scfl);

            #endregion

            _RedocommandsSCFL.Clear();
            
        }
        #endregion

        #region Populate Data Table Method
        private void PopulateDataTable(int l_edit_scfl)
        {
            int indexRow = 0;
            #region Populating Table for DOUBLE WISHBONE
            if (Assy_List_SCFL[l_edit_scfl].DoubleWishboneIdentifierFront == 1)
            {
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].D1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].D1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].D1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].C1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].C1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].C1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].A1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].A1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].A1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].B1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].B1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].B1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].I1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].I1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].I1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].Q1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].Q1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].Q1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].N1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].N1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].N1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].Pin1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].Pin1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].Pin1y);
                indexRow++;

                if (this.NoOfCouplings == 2)
                {
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].UV2z);
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].UV2x);
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].UV2y);
                    indexRow++;

                }

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].UV1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].UV1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].UV1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].STC1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].STC1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].STC1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].JO1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].JO1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].JO1y);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].RideHeightRefz);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].RideHeightRefx);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].RideHeightRefy);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].J1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].J1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].J1y);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].H1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].H1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].H1y);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].O1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].O1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].O1y);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].G1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].G1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].G1y);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].F1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].F1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].F1y);
                indexRow++;


                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].E1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].E1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].E1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].P1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].P1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].P1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].K1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].K1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].K1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].M1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].M1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].M1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].W1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].W1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].W1y);
                indexRow++;

                if (Assy_List_SCFL[l_edit_scfl].TARBIdentifierFront == 1)
                {
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].R1z);
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].R1x);
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].R1y);
                    indexRow++;

                }

            }
            #endregion

            #region Populating Table for McPHERSON
            if (Assy_List_SCFL[l_edit_scfl].McPhersonIdentifierFront == 1)
            {
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].D1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].D1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].D1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].C1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].C1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].C1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].Q1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].Q1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].Q1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].N1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].N1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].N1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].Pin1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].Pin1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].Pin1y);
                indexRow++;

                if (this.NoOfCouplings == 2)
                {
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].UV2z);
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].UV2x);
                    Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].UV2y);
                    indexRow++;

                }

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].UV1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].UV1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].UV1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].STC1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].STC1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].STC1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].JO1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].JO1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].JO1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].RideHeightRefz);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].RideHeightRefx);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].RideHeightRefy);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].J1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].J1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].J1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].E1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].E1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].E1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].P1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].P1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].P1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].K1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].K1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].K1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].M1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].M1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].M1y);
                indexRow++;

                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("X (mm)", Assy_List_SCFL[l_edit_scfl].W1z);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Y (mm)", Assy_List_SCFL[l_edit_scfl].W1x);
                Assy_List_SCFL[l_edit_scfl].SCFLDataTable.Rows[indexRow].SetField<double>("Z (mm)", Assy_List_SCFL[l_edit_scfl].W1y);
                indexRow++;


            }
            #endregion
        }
        
        #endregion

        #region De-serialization of the SCFL Object's Datag
        public SuspensionCoordinatesFront(SerializationInfo info, StreamingContext context)
        {
            _SCName = (string)info.GetValue("SCFL_Name", typeof(string));

            #region De-serialization of the Coordinates
            A1x = (double)info.GetValue("A1x_FL", typeof(double));
            A1y = (double)info.GetValue("A1y_FL", typeof(double));
            A1z = (double)info.GetValue("A1z_FL", typeof(double));

            B1x = (double)info.GetValue("B1x_FL", typeof(double));
            B1y = (double)info.GetValue("B1y_FL", typeof(double));
            B1z = (double)info.GetValue("B1z_FL", typeof(double));

            C1x = (double)info.GetValue("C1x_FL", typeof(double));
            C1y = (double)info.GetValue("C1y_FL", typeof(double));
            C1z = (double)info.GetValue("C1z_FL", typeof(double));

            D1x = (double)info.GetValue("D1x_FL", typeof(double));
            D1y = (double)info.GetValue("D1y_FL", typeof(double));
            D1z = (double)info.GetValue("D1z_FL", typeof(double));

            E1x = (double)info.GetValue("E1x_FL", typeof(double));
            E1y = (double)info.GetValue("E1y_FL", typeof(double));
            E1z = (double)info.GetValue("E1z_FL", typeof(double));

            F1x = (double)info.GetValue("F1x_FL", typeof(double));
            F1y = (double)info.GetValue("F1y_FL", typeof(double));
            F1z = (double)info.GetValue("F1z_FL", typeof(double));

            G1x = (double)info.GetValue("G1x_FL", typeof(double));
            G1y = (double)info.GetValue("G1y_FL", typeof(double));
            G1z = (double)info.GetValue("G1z_FL", typeof(double));

            H1x = (double)info.GetValue("H1x_FL", typeof(double));
            H1y = (double)info.GetValue("H1y_FL", typeof(double));
            H1z = (double)info.GetValue("H1z_FL", typeof(double));

            I1x = (double)info.GetValue("I1x_FL", typeof(double));
            I1y = (double)info.GetValue("I1y_FL", typeof(double));
            I1z = (double)info.GetValue("I1z_FL", typeof(double));

            J1x = (double)info.GetValue("J1x_FL", typeof(double));
            J1y = (double)info.GetValue("J1y_FL", typeof(double));
            J1z = (double)info.GetValue("J1z_FL", typeof(double));

            JO1x = (double)info.GetValue("JO1x_FL", typeof(double));
            JO1y = (double)info.GetValue("JO1y_FL", typeof(double));
            JO1z = (double)info.GetValue("JO1z_FL", typeof(double));

            K1x = (double)info.GetValue("K1x_FL", typeof(double));
            K1y = (double)info.GetValue("K1y_FL", typeof(double));
            K1z = (double)info.GetValue("K1z_FL", typeof(double));

            L1x = (double)info.GetValue("L1x_FL", typeof(double));
            L1y = (double)info.GetValue("L1y_FL", typeof(double));
            L1z = (double)info.GetValue("L1z_FL", typeof(double));

            M1x = (double)info.GetValue("M1x_FL", typeof(double));
            M1y = (double)info.GetValue("M1y_FL", typeof(double));
            M1z = (double)info.GetValue("M1z_FL", typeof(double));

            N1x = (double)info.GetValue("N1x_FL", typeof(double));
            N1y = (double)info.GetValue("N1y_FL", typeof(double));
            N1z = (double)info.GetValue("N1z_FL", typeof(double));

            Pin1x = (double)info.GetValue("Pin1x_FL", typeof(double));
            Pin1y = (double)info.GetValue("Pin1y_FL", typeof(double));
            Pin1z = (double)info.GetValue("Pin1z_FL", typeof(double));

            UV2x = (double)info.GetValue("UV2x_FL", typeof(double));
            UV2y = (double)info.GetValue("UV2y_FL", typeof(double));
            UV2z = (double)info.GetValue("UV2z_FL", typeof(double));

            UV1x = (double)info.GetValue("UV1x_FL", typeof(double));
            UV1y = (double)info.GetValue("UV1y_FL", typeof(double));
            UV1z = (double)info.GetValue("UV1z_FL", typeof(double));

            STC1x = (double)info.GetValue("STC1x_FL", typeof(double));
            STC1y = (double)info.GetValue("STC1y_FL", typeof(double));
            STC1z = (double)info.GetValue("STC1z_FL", typeof(double));

            O1x = (double)info.GetValue("O1x_FL", typeof(double));
            O1y = (double)info.GetValue("O1y_FL", typeof(double));
            O1z = (double)info.GetValue("O1z_FL", typeof(double));

            P1x = (double)info.GetValue("P1x_FL", typeof(double));
            P1y = (double)info.GetValue("P1y_FL", typeof(double));
            P1z = (double)info.GetValue("P1z_FL", typeof(double));

            Q1x = (double)info.GetValue("Q1x_FL", typeof(double));
            Q1y = (double)info.GetValue("Q1y_FL", typeof(double));
            Q1z = (double)info.GetValue("Q1z_FL", typeof(double));

            R1x = (double)info.GetValue("R1x_FL", typeof(double));
            R1y = (double)info.GetValue("R1y_FL", typeof(double));
            R1z = (double)info.GetValue("R1z_FL", typeof(double));

            W1x = (double)info.GetValue("W1x_FL", typeof(double));
            W1y = (double)info.GetValue("W1y_FL", typeof(double));
            W1z = (double)info.GetValue("W1z_FL", typeof(double));

            RideHeightRefx = (double)info.GetValue("RideHeightRefx_FL", typeof(double));
            RideHeightRefy = (double)info.GetValue("RideHeightRefy_FL", typeof(double));
            RideHeightRefz = (double)info.GetValue("RideHeightRefz_FL", typeof(double)); 
            #endregion

            #region De-serialization of the Suspension Type Varibales
            FrontSymmetry = (bool)info.GetValue("Front_Symmetry", typeof(bool));
            DoubleWishboneIdentifierFront = (int)info.GetValue("DoubleWishbone_Identifier_Front", typeof(int));
            McPhersonIdentifierFront = (int)info.GetValue("McPherson_Identifier_Front", typeof(int));
            PushrodIdentifierFront = (int)info.GetValue("Pushrod_Identifier_Front", typeof(int));
            PullrodIdentifierFront = (int)info.GetValue("Pullrod_Identifier_Front", typeof(int));
            UARBIdentifierFront = (int)info.GetValue("UARB_Identifier_Front", typeof(int));
            TARBIdentifierFront = (int)info.GetValue("TARB_Identifier_Front", typeof(int));
            NoOfCouplings = (int)info.GetValue("NoOfCouplings", typeof(int));
            #endregion

            SuspensionMotionExists = (bool)info.GetValue("SuspensionMotionExists", typeof(bool));

            #region De-serialization of the Input Origin
            InputOriginX = (double)info.GetValue("InputOriginX", typeof(double));
            InputOriginY = (double)info.GetValue("InputOriginY", typeof(double));
            InputOriginZ = (double)info.GetValue("InputOriginZ", typeof(double)); 
            #endregion

            #region Deserialization of the Link Lengths
            LowerFrontLength = (double)info.GetValue("LowerFront_Length_FL", typeof(double));
            LowerRearLength = (double)info.GetValue("LowerRear_Length_FL", typeof(double));
            UpperFrontLength = (double)info.GetValue("UpperFront_Length_FL", typeof(double));
            UpperRearLength = (double)info.GetValue("UpperRear_Length_FL", typeof(double));
            PushRodLength = (double)info.GetValue("Pushrod_Length_FL", typeof(double));
            PushRodLength_1 = (double)info.GetValue("Pushrod_Length_1_FL", typeof(double));
            ToeLinkLength = (double)info.GetValue("ToeLink_Length_FL", typeof(double));
            DamperLength = (double)info.GetValue("Damper_Length_FL", typeof(double));
            ARBDroopLinkLength = (double)info.GetValue("ARBDroopLink_Length_FL", typeof(double));
            ARBBladeLength = (double)info.GetValue("ARBBlade_Length_FL", typeof(double));
            #endregion

            WheelDeflection_Steering = (double[])info.GetValue("WheelDeflection_Steering", typeof(double[]));
            SpringDeflection_DeltSteering = (List<double>)info.GetValue("SpringDeflection_DeltSteering", typeof(List<double>));

            SCFL_ID = (int)info.GetValue("SCFL_ID", typeof(int));
            SCFLCurrentID = (int)info.GetValue("CurrentSCFL_ID", typeof(int));
            SCFLCounter = (int)info.GetValue("SCFL_Counter", typeof(int));

            SCFLDataTable = (DataTable)info.GetValue("SCFLDataTable", typeof(DataTable));

        }
        #endregion

        #region Serialization of the SCFL Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SCFL_Name", _SCName);

            #region Serialization of Coordinates
            info.AddValue("A1x_FL", A1x);
            info.AddValue("A1y_FL", A1y);
            info.AddValue("A1z_FL", A1z);

            info.AddValue("B1x_FL", B1x);
            info.AddValue("B1y_FL", B1y);
            info.AddValue("B1z_FL", B1z);

            info.AddValue("C1x_FL", C1x);
            info.AddValue("C1y_FL", C1y);
            info.AddValue("C1z_FL", C1z);

            info.AddValue("D1x_FL", D1x);
            info.AddValue("D1y_FL", D1y);
            info.AddValue("D1z_FL", D1z);

            info.AddValue("E1x_FL", E1x);
            info.AddValue("E1y_FL", E1y);
            info.AddValue("E1z_FL", E1z);

            info.AddValue("F1x_FL", F1x);
            info.AddValue("F1y_FL", F1y);
            info.AddValue("F1z_FL", F1z);

            info.AddValue("G1x_FL", G1x);
            info.AddValue("G1y_FL", G1y);
            info.AddValue("G1z_FL", G1z);

            info.AddValue("H1x_FL", H1x);
            info.AddValue("H1y_FL", H1y);
            info.AddValue("H1z_FL", H1z);

            info.AddValue("I1x_FL", I1x);
            info.AddValue("I1y_FL", I1y);
            info.AddValue("I1z_FL", I1z);

            info.AddValue("J1x_FL", J1x);
            info.AddValue("J1y_FL", J1y);
            info.AddValue("J1z_FL", J1z);

            info.AddValue("JO1x_FL", JO1x);
            info.AddValue("JO1y_FL", JO1y);
            info.AddValue("JO1z_FL", JO1z);

            info.AddValue("K1x_FL", K1x);
            info.AddValue("K1y_FL", K1y);
            info.AddValue("K1z_FL", K1z);

            info.AddValue("L1x_FL", L1x);
            info.AddValue("L1y_FL", L1y);
            info.AddValue("L1z_FL", L1z);

            info.AddValue("M1x_FL", M1x);
            info.AddValue("M1y_FL", M1y);
            info.AddValue("M1z_FL", M1z);

            info.AddValue("N1x_FL", N1x);
            info.AddValue("N1y_FL", N1y);
            info.AddValue("N1z_FL", N1z);

            info.AddValue("Pin1x_FL", Pin1x);
            info.AddValue("Pin1y_FL", Pin1y);
            info.AddValue("Pin1z_FL", Pin1z);

            info.AddValue("UV2x_FL", UV2x);
            info.AddValue("UV2y_FL", UV2y);
            info.AddValue("UV2z_FL", UV2z);

            info.AddValue("UV1x_FL", UV1x);
            info.AddValue("UV1y_FL", UV1y);
            info.AddValue("UV1z_FL", UV1z);

            info.AddValue("STC1x_FL", STC1x);
            info.AddValue("STC1y_FL", STC1y);
            info.AddValue("STC1z_FL", STC1z);

            info.AddValue("O1x_FL", O1x);
            info.AddValue("O1y_FL", O1y);
            info.AddValue("O1z_FL", O1z);

            info.AddValue("P1x_FL", P1x);
            info.AddValue("P1y_FL", P1y);
            info.AddValue("P1z_FL", P1z);

            info.AddValue("Q1x_FL", Q1x);
            info.AddValue("Q1y_FL", Q1y);
            info.AddValue("Q1z_FL", Q1z);

            info.AddValue("R1x_FL", R1x);
            info.AddValue("R1y_FL", R1y);
            info.AddValue("R1z_FL", R1z);

            info.AddValue("W1x_FL", W1x);
            info.AddValue("W1y_FL", W1y);
            info.AddValue("W1z_FL", W1z);

            info.AddValue("RideHeightRefx_FL", RideHeightRefx);
            info.AddValue("RideHeightRefy_FL", RideHeightRefy);
            info.AddValue("RideHeightRefz_FL", RideHeightRefz);
            #endregion

            #region Serialization of the Suspension Type Variables
            info.AddValue("Front_Symmetry", FrontSymmetry);
            info.AddValue("DoubleWishbone_Identifier_Front", DoubleWishboneIdentifierFront);
            info.AddValue("McPherson_Identifier_Front", McPhersonIdentifierFront);
            info.AddValue("Pushrod_Identifier_Front", PushrodIdentifierFront);
            info.AddValue("Pullrod_Identifier_Front", PullrodIdentifierFront);
            info.AddValue("UARB_Identifier_Front", UARBIdentifierFront);
            info.AddValue("TARB_Identifier_Front", TARBIdentifierFront);
            info.AddValue("NoOfCouplings", NoOfCouplings);
            #endregion

            info.AddValue("SuspensionMotionExists", SuspensionMotionExists);

            #region Serialization of the Input Origin
            info.AddValue("InputOriginX", InputOriginX);
            info.AddValue("InputOriginY", InputOriginY);
            info.AddValue("InputOriginZ", InputOriginZ);
            #endregion

            #region Serialization of the Link Lengths
            info.AddValue("LowerFront_Length_FL", LowerFrontLength);
            info.AddValue("LowerRear_Length_FL", LowerRearLength);
            info.AddValue("UpperFront_Length_FL", UpperFrontLength);
            info.AddValue("UpperRear_Length_FL", UpperRearLength);
            info.AddValue("Pushrod_Length_FL", PushRodLength);
            info.AddValue("Pushrod_Length_1_FL", PushRodLength_1);
            info.AddValue("ToeLink_Length_FL", ToeLinkLength);
            info.AddValue("Damper_Length_FL", DamperLength);
            info.AddValue("ARBDroopLink_Length_FL", ARBDroopLinkLength);
            info.AddValue("ARBBlade_Length_FL", ARBBladeLength);
            #endregion

            info.AddValue("WheelDeflection_Steering", WheelDeflection_Steering);
            info.AddValue("SpringDeflection_DeltSteering", SpringDeflection_DeltSteering);

            info.AddValue("SCFL_ID", SCFL_ID);
            info.AddValue("CurrentSCFL_ID", SCFLCurrentID);
            info.AddValue("SCFL_Counter", SCFLCounter);

            info.AddValue("SCFLDataTable", SCFLDataTable);
        } 
        #endregion


    }
}

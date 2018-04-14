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
    /// This is Class representing the Front Right Suspension Coordinate
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesFrontRight : SuspensionCoordinatesMaster, ISerializable,ICommand
    {
        #region Front Right Suspension Parameters
        public int SCFR_ID { get; set; }
        public static int SCFRCurrentID { get; set; }
        public bool SCFRIsModified { get; set; }

        public static int SCFRCounter = 0;

        public static bool IsUndoRedoCalledByLeft = false;
        #endregion

        #region Undo/Redo Stack
        public Stack<ICommand> _UndocommandsSCFR = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsSCFR = new Stack<ICommand>();
        #endregion

        #region SCFR Data Table
        public DataTable SCFRDataTable { get; set; } 
        #endregion

        #region Declaration and Initialization of the Global Array and Global List of SCFR objects
        public static List<SuspensionCoordinatesFrontRight> Assy_List_SCFR = new List<SuspensionCoordinatesFrontRight>();
        #endregion

        #region Constructor

        #region Base Constructor
        public SuspensionCoordinatesFrontRight()
        {
            SCFRIsModified = false;
        }
        #endregion

        #region Overloaded Constructor
        public SuspensionCoordinatesFrontRight(SuspensionCoordinatesFrontRightGUI _scfrGUI)
        {
            SCFRDataTable = _scfrGUI.SCFRDataTableGUI;

            #region Front Right Cooridinates Initialization
            #region Fixed Points FRONT Right Initialization - Double Wishbone & McPherson
            //  Coordinates of Fixed Point A
            A1x = _scfrGUI.A1y;
            A1y = _scfrGUI.A1z;
            A1z = _scfrGUI.A1x;

            //  Coordinates of Fixed Point B
            B1x = _scfrGUI.B1y;
            B1y = _scfrGUI.B1z;
            B1z = _scfrGUI.B1x;

            //  Coordinates of Fixed Point C
            C1x = _scfrGUI.C1y;
            C1y = _scfrGUI.C1z;
            C1z = _scfrGUI.C1x;

            //  Coordinates of Fixed Point D
            D1x = _scfrGUI.D1y;
            D1y = _scfrGUI.D1z;
            D1z = _scfrGUI.D1x;

            // Initial Coordinates of Moving Point I
            I1x = _scfrGUI.I1y;
            I1y = _scfrGUI.I1z;
            I1z = _scfrGUI.I1x;

            // Initial Coordinates of Moving Point Jo
            JO1x = _scfrGUI.JO1y;
            JO1y = _scfrGUI.JO1z;
            JO1z = _scfrGUI.JO1x;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            N1x = _scfrGUI.N1y;
            N1y = _scfrGUI.N1z;
            N1z = _scfrGUI.N1x;

            //  Coordinates of Fixed Point Q
            Q1x = _scfrGUI.Q1y;
            Q1y = _scfrGUI.Q1z;
            Q1z = _scfrGUI.Q1x;

            // Coordinates of Fixed Point R
            R1x = _scfrGUI.R1y;
            R1y = _scfrGUI.R1z;
            R1z = _scfrGUI.R1x;

            #endregion

            #region Moving Points FRONT Right Initialization - Double Wishbone & McPherson
            // Initial Coordinates of Moving Point J
            J1x = _scfrGUI.J1y;
            J1y = _scfrGUI.J1z;
            J1z = _scfrGUI.J1x;

            // Initial Coordinates of Moving Point H
            H1x = _scfrGUI.H1y;
            H1y = _scfrGUI.H1z;
            H1z = _scfrGUI.H1x;

            // Initial Coordinates of Moving Point G
            G1x = _scfrGUI.G1y;
            G1y = _scfrGUI.G1z;
            G1z = _scfrGUI.G1x;

            // Initial Coordinates of Moving Point F
            F1x = _scfrGUI.F1y;
            F1y = _scfrGUI.F1z;
            F1z = _scfrGUI.F1x;

            // Initial Coordinates of Moving Point E
            E1x = _scfrGUI.E1y;
            E1y = _scfrGUI.E1z;
            E1z = _scfrGUI.E1x;

            // Initial Coordinates of Moving Point K
            K1x = _scfrGUI.K1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            K1y = _scfrGUI.K1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            K1z = _scfrGUI.K1x;

            // Initial Coordinates of Moving Point L
            L1x = _scfrGUI.W1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            L1y = _scfrGUI.W1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            L1z = _scfrGUI.W1x;

            // Initial Coordinates of Moving Point M
            M1x = _scfrGUI.M1y;
            M1y = _scfrGUI.M1z;
            M1z = _scfrGUI.M1x;

            // Initial Coordinates of Moving Point O
            O1x = _scfrGUI.O1y;
            O1y = _scfrGUI.O1z;
            O1z = _scfrGUI.O1x;

            // Initial Coordinates of Moving Point P
            P1x = _scfrGUI.P1y;
            P1y = _scfrGUI.P1z;
            P1z = _scfrGUI.P1x;

            //  Coordinates of Moving Contact Patch Point W
            W1x = _scfrGUI.W1y;
            W1y = _scfrGUI.W1z;
            W1z = _scfrGUI.W1x;

            //  Ride Height Reference Points
            RideHeightRefx = _scfrGUI.RideHeightRefy;
            RideHeightRefy = _scfrGUI.RideHeightRefz;
            RideHeightRefz = _scfrGUI.RideHeightRefx;
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

            /////<remarks>This list is going to be populated from the index number 1 as the delta of Wheel Deflection is 0 for the 0th Index</remarks>
            //WheelDeflection_Steering.Insert(0, 0);
            #endregion
        }
        #endregion

        #endregion

        #region Create new SCFR Method
        public void CreateNewSCFR(int i_create_scfr, SuspensionCoordinatesFrontRightGUI create_scfrGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new SCFR and addes it to the List of SCFR objects 
            ///</summary>

            #region Adding new SCFR object to the list of SCFR objects
            SuspensionCoordinatesFrontRightGUI scfrGUI = create_scfrGUI_list;
            Assy_List_SCFR.Insert(i_create_scfr, new SuspensionCoordinatesFrontRight(scfrGUI));
            Assy_List_SCFR[i_create_scfr].FrontSuspensionType(scfrGUI);
            Assy_List_SCFR[i_create_scfr]._SCName = "Front Right Coordinates " + Convert.ToString(i_create_scfr + 1);
            Assy_List_SCFR[i_create_scfr].SCFR_ID = i_create_scfr + 1;
            Assy_List_SCFR[i_create_scfr]._UndocommandsSCFR = new Stack<ICommand>();
            Assy_List_SCFR[i_create_scfr]._RedocommandsSCFR = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Redo Method
        public void ModifyObjectData(int l_modify_scfr, object modify_scfr_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the Suspension is bring modified and it is placed under the method called ModifyObjectData because it is an Undoable operation 
            ///</summary>

            #region Redoing the Modification
            SuspensionCoordinatesFrontRight _scfr_forRedo = (SuspensionCoordinatesFrontRight)modify_scfr_list;

            ICommand cmd = Assy_List_SCFR[l_modify_scfr];
            Assy_List_SCFR[l_modify_scfr]._UndocommandsSCFR.Push(cmd);

            Assy_List_SCFR[l_modify_scfr] = _scfr_forRedo;
            Assy_List_SCFR[l_modify_scfr].SCFRIsModified = true;

            PopulateDataTable(l_modify_scfr);

            SuspensionCoordinatesFrontRightGUI.DisplaySCFRItem(Assy_List_SCFR[l_modify_scfr]);

            #region Calling Redo method for Opposite Suspension if symmetric
            if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_modify_scfr].FrontSymmetry == true && IsUndoRedoCalledByLeft == false)
            {
                SuspensionCoordinatesFront.IsUndoRedoCalledByRight_IdentifierMethod(true);
                UndoRedo undoRedo = new UndoRedo();
                undoRedo.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_modify_scfr]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_modify_scfr]._RedocommandsSCFL,
                                    l_modify_scfr + 1, SuspensionCoordinatesFront.Assy_List_SCFL[l_modify_scfr].SCFLIsModified);
                undoRedo.Redo(1);
                SuspensionCoordinatesFront.IsUndoRedoCalledByRight_IdentifierMethod(false);// This method sets the IsUndoRedoCalledByRight variable to false and allows the left suspenson coordinate to be undone
            } 
            #endregion

            Kinematics_Software_New.EditFrontCAD(l_modify_scfr);

            Kinematics_Software_New.SCFR_ModifyInVehicle(l_modify_scfr, Assy_List_SCFR[l_modify_scfr]);


            #endregion
        }
        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexcute_scfr, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>
            #region Undoing the modification
            try
            {
                SuspensionCoordinatesFrontRight _scfr_forUndo = (SuspensionCoordinatesFrontRight)command;

                ICommand cmd = Assy_List_SCFR[l_unexcute_scfr];
                Assy_List_SCFR[l_unexcute_scfr]._RedocommandsSCFR.Push(cmd);

                Assy_List_SCFR[l_unexcute_scfr] = _scfr_forUndo;

                PopulateDataTable(l_unexcute_scfr);

                SuspensionCoordinatesFrontRightGUI.DisplaySCFRItem(Assy_List_SCFR[l_unexcute_scfr]);

                #region Calling Undo method for Opposite Suspension if symmetric
                if (SuspensionCoordinatesFrontRight.Assy_List_SCFR[l_unexcute_scfr].FrontSymmetry == true && IsUndoRedoCalledByLeft == false)
                {

                    SuspensionCoordinatesFront.IsUndoRedoCalledByRight_IdentifierMethod(true);// This method sets the IsUndoRedoCalledByRight variable to true and prevents an infinte loop

                    UndoRedo undoRedo = new UndoRedo();
                    undoRedo.Identifier(SuspensionCoordinatesFront.Assy_List_SCFL[l_unexcute_scfr]._UndocommandsSCFL, SuspensionCoordinatesFront.Assy_List_SCFL[l_unexcute_scfr]._RedocommandsSCFL,
                                        l_unexcute_scfr + 1, SuspensionCoordinatesFront.Assy_List_SCFL[l_unexcute_scfr].SCFLIsModified);
                    undoRedo.Undo(1);
                    SuspensionCoordinatesFront.IsUndoRedoCalledByRight_IdentifierMethod(false);// This method sets the IsUndoRedoCalledByRight variable to false and allows the left suspenson coordinate to be undone
                } 
                #endregion

                Kinematics_Software_New.EditFrontCAD(l_unexcute_scfr);

                Kinematics_Software_New.SCFR_ModifyInVehicle(l_unexcute_scfr, Assy_List_SCFR[l_unexcute_scfr]);


            }
            catch (Exception) { }
            #endregion
        }

        #endregion

        public static void IsUndoRedoCalledByLeft_IdentifierMethod(bool value) => IsUndoRedoCalledByLeft = value;

        #region Populate Data Table Method
        private void PopulateDataTable(int l_modify_scfr)
        {
            #region Populating Table for DOUBLE WISHBONE
            if (Assy_List_SCFR[l_modify_scfr].DoubleWishboneIdentifierFront == 1)
            {
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].D1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].D1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].D1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].C1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].C1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].C1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].A1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].A1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].A1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].B1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].B1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].B1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].I1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].I1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].I1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[5].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].Q1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[5].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].Q1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[5].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].Q1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[6].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].N1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[6].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].N1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[6].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].N1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[7].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].JO1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[7].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].JO1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[7].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].JO1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[8].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].RideHeightRefz);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[8].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].RideHeightRefx);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[8].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].RideHeightRefy);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[9].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].J1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[9].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].J1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[9].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].J1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[10].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].H1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[10].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].H1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[10].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].H1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[11].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].O1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[11].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].O1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[11].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].O1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[12].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].G1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[12].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].G1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[12].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].G1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[13].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].F1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[13].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].F1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[13].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].F1y);


                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[14].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].E1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[14].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].E1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[14].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].E1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[15].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].P1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[15].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].P1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[15].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].P1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[16].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].K1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[16].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].K1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[16].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].K1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[17].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].M1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[17].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].M1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[17].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].M1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[18].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].W1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[18].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].W1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[18].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].W1y);

                if (Assy_List_SCFR[l_modify_scfr].TARBIdentifierFront == 1)
                {
                    Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[19].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].R1z);
                    Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[19].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].R1x);
                    Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[19].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].R1y);
                }

            }
            #endregion

            #region Populating Table for McPHERSON
            if (Assy_List_SCFR[l_modify_scfr].McPhersonIdentifierFront == 1)
            {
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].D1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].D1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].D1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].C1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].C1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].C1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].Q1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].Q1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].Q1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].N1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].N1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].N1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].JO1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].JO1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].JO1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[5].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].RideHeightRefz);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[5].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].RideHeightRefx);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[5].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].RideHeightRefy);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[6].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].J1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[6].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].J1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[6].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].J1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[7].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].E1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[7].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].E1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[7].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].E1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[8].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].P1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[8].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].P1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[8].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].P1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[9].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].K1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[9].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].K1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[9].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].K1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[10].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].M1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[10].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].M1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[10].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].M1y);

                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[11].SetField<double>("X (mm)", Assy_List_SCFR[l_modify_scfr].W1z);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[11].SetField<double>("Y (mm)", Assy_List_SCFR[l_modify_scfr].W1x);
                Assy_List_SCFR[l_modify_scfr].SCFRDataTable.Rows[11].SetField<double>("Z (mm)", Assy_List_SCFR[l_modify_scfr].W1y);


            }
            #endregion
        }  
        #endregion

        #region Method to assign the identifiers which will establish the type of suspension
        public void FrontSuspensionType(SuspensionCoordinatesFrontRightGUI _scfrGUI)
        {
            #region Determining the Suspension Type using the GUI Object
            FrontSymmetry = _scfrGUI.FrontSymmetryGUI;

            DoubleWishboneIdentifierFront = _scfrGUI.DoubleWishboneIdentifierFront;
            McPhersonIdentifierFront = _scfrGUI.McPhersonIdentifierFront;

            PushrodIdentifierFront = _scfrGUI.PushrodIdentifierFront;
            PullrodIdentifierFront = _scfrGUI.PullrodIdentifierFront;

            UARBIdentifierFront = _scfrGUI.UARBIdentifierFront;
            TARBIdentifierFront = _scfrGUI.TARBIdentifierFront;
            #endregion
        } 
        #endregion

        #region Method to Edit the Front Right Suspension
        public void EditFrontRightSuspension(int l_edit_scfr, SuspensionCoordinatesFrontRightGUI _scfrGUI)
        {
            ICommand cmd = Assy_List_SCFR[l_edit_scfr];
            Assy_List_SCFR[l_edit_scfr]._UndocommandsSCFR.Push(cmd);

            #region Front Right Coordinates editing 
            SuspensionCoordinatesFrontRight scfr_list = new SuspensionCoordinatesFrontRight(_scfrGUI);
            scfr_list._UndocommandsSCFR = Assy_List_SCFR[l_edit_scfr]._UndocommandsSCFR;
            scfr_list._RedocommandsSCFR = Assy_List_SCFR[l_edit_scfr]._RedocommandsSCFR;
            scfr_list._SCName = Assy_List_SCFR[l_edit_scfr]._SCName;

            Assy_List_SCFR[l_edit_scfr] = scfr_list;
            Assy_List_SCFR[l_edit_scfr].SCFRDataTable = scfr_list.SCFRDataTable;
            Assy_List_SCFR[l_edit_scfr].SCFR_ID = l_edit_scfr + 1;
            Assy_List_SCFR[l_edit_scfr].FrontSuspensionType(_scfrGUI);
            Assy_List_SCFR[l_edit_scfr].SCFRIsModified = true;

            PopulateDataTable(l_edit_scfr);

            #endregion

            _RedocommandsSCFR.Clear();


        } 
        #endregion

        #region De-serialization of the SCFR Object's Data
        public SuspensionCoordinatesFrontRight(SerializationInfo info, StreamingContext context)
        {
            _SCName = (string)info.GetValue("SCFR_Name", typeof(string));

            #region De-serialization of the Coordinates
            A1x = (double)info.GetValue("A1x_FR", typeof(double));
            A1y = (double)info.GetValue("A1y_FR", typeof(double));
            A1z = (double)info.GetValue("A1z_FR", typeof(double));

            B1x = (double)info.GetValue("B1x_FR", typeof(double));
            B1y = (double)info.GetValue("B1y_FR", typeof(double));
            B1z = (double)info.GetValue("B1z_FR", typeof(double));

            C1x = (double)info.GetValue("C1x_FR", typeof(double));
            C1y = (double)info.GetValue("C1y_FR", typeof(double));
            C1z = (double)info.GetValue("C1z_FR", typeof(double));

            D1x = (double)info.GetValue("D1x_FR", typeof(double));
            D1y = (double)info.GetValue("D1y_FR", typeof(double));
            D1z = (double)info.GetValue("D1z_FR", typeof(double));

            E1x = (double)info.GetValue("E1x_FR", typeof(double));
            E1y = (double)info.GetValue("E1y_FR", typeof(double));
            E1z = (double)info.GetValue("E1z_FR", typeof(double));

            F1x = (double)info.GetValue("F1x_FR", typeof(double));
            F1y = (double)info.GetValue("F1y_FR", typeof(double));
            F1z = (double)info.GetValue("F1z_FR", typeof(double));

            G1x = (double)info.GetValue("G1x_FR", typeof(double));
            G1y = (double)info.GetValue("G1y_FR", typeof(double));
            G1z = (double)info.GetValue("G1z_FR", typeof(double));

            H1x = (double)info.GetValue("H1x_FR", typeof(double));
            H1y = (double)info.GetValue("H1y_FR", typeof(double));
            H1z = (double)info.GetValue("H1z_FR", typeof(double));

            I1x = (double)info.GetValue("I1x_FR", typeof(double));
            I1y = (double)info.GetValue("I1y_FR", typeof(double));
            I1z = (double)info.GetValue("I1z_FR", typeof(double));

            J1x = (double)info.GetValue("J1x_FR", typeof(double));
            J1y = (double)info.GetValue("J1y_FR", typeof(double));
            J1z = (double)info.GetValue("J1z_FR", typeof(double));

            JO1x = (double)info.GetValue("JO1x_FR", typeof(double));
            JO1y = (double)info.GetValue("JO1y_FR", typeof(double));
            JO1z = (double)info.GetValue("JO1z_FR", typeof(double));

            K1x = (double)info.GetValue("K1x_FR", typeof(double));
            K1y = (double)info.GetValue("K1y_FR", typeof(double));
            K1z = (double)info.GetValue("K1z_FR", typeof(double));

            L1x = (double)info.GetValue("L1x_FR", typeof(double));
            L1y = (double)info.GetValue("L1y_FR", typeof(double));
            L1z = (double)info.GetValue("L1z_FR", typeof(double));

            M1x = (double)info.GetValue("M1x_FR", typeof(double));
            M1y = (double)info.GetValue("M1y_FR", typeof(double));
            M1z = (double)info.GetValue("M1z_FR", typeof(double));

            N1x = (double)info.GetValue("N1x_FR", typeof(double));
            N1y = (double)info.GetValue("N1y_FR", typeof(double));
            N1z = (double)info.GetValue("N1z_FR", typeof(double));

            O1x = (double)info.GetValue("O1x_FR", typeof(double));
            O1y = (double)info.GetValue("O1y_FR", typeof(double));
            O1z = (double)info.GetValue("O1z_FR", typeof(double));

            P1x = (double)info.GetValue("P1x_FR", typeof(double));
            P1y = (double)info.GetValue("P1y_FR", typeof(double));
            P1z = (double)info.GetValue("P1z_FR", typeof(double));

            Q1x = (double)info.GetValue("Q1x_FR", typeof(double));
            Q1y = (double)info.GetValue("Q1y_FR", typeof(double));
            Q1z = (double)info.GetValue("Q1z_FR", typeof(double));

            R1x = (double)info.GetValue("R1x_FR", typeof(double));
            R1y = (double)info.GetValue("R1y_FR", typeof(double));
            R1z = (double)info.GetValue("R1z_FR", typeof(double));

            W1x = (double)info.GetValue("W1x_FR", typeof(double));
            W1y = (double)info.GetValue("W1y_FR", typeof(double));
            W1z = (double)info.GetValue("W1z_FR", typeof(double));

            RideHeightRefx = (double)info.GetValue("RideHeightRefx_FR", typeof(double));
            RideHeightRefy = (double)info.GetValue("RideHeightRefy_FR", typeof(double));
            RideHeightRefz = (double)info.GetValue("RideHeightRefz_FR", typeof(double));
            #endregion

            #region De-serialization of the Suspension Type Varibales
            FrontSymmetry = (bool)info.GetValue("Front_Symmetry", typeof(bool));
            DoubleWishboneIdentifierFront = (int)info.GetValue("DoubleWishbone_Identifier_Front", typeof(int));
            McPhersonIdentifierFront = (int)info.GetValue("McPherson_Identifier_Front", typeof(int));
            PushrodIdentifierFront = (int)info.GetValue("Pushrod_Identifier_Front", typeof(int));
            PullrodIdentifierFront = (int)info.GetValue("Pullrod_Identifier_Front", typeof(int));
            UARBIdentifierFront = (int)info.GetValue("UARB_Identifier_Front", typeof(int));
            TARBIdentifierFront = (int)info.GetValue("TARB_Identifier_Front", typeof(int));
            #endregion

            #region Deserialization of the Link Lengths
            LowerFrontLength = (double)info.GetValue("LowerFront_Length_FR", typeof(double));
            LowerRearLength = (double)info.GetValue("LowerRear_Length_FR", typeof(double));
            UpperFrontLength = (double)info.GetValue("UpperFront_Length_FR", typeof(double));
            UpperRearLength = (double)info.GetValue("UpperRear_Length_FR", typeof(double));
            PushRodLength = (double)info.GetValue("Pushrod_Length_FR", typeof(double));
            PushRodLength_1 = (double)info.GetValue("Pushrod_Length_1_FR", typeof(double));
            ToeLinkLength = (double)info.GetValue("ToeLink_Length_FR", typeof(double));
            DamperLength = (double)info.GetValue("Damper_Length_FR", typeof(double));
            ARBDroopLinkLength = (double)info.GetValue("ARBDroopLink_Length_FR", typeof(double));
            ARBBladeLength = (double)info.GetValue("ARBBlade_Length_FR", typeof(double));
            #endregion

            WheelDeflection_Steering = (double[])info.GetValue("WheelDeflection_Steering", typeof(double[]));
            SpringDeflection_DeltSteering = (List<double>)info.GetValue("SpringDeflection_DeltSteering", typeof(List<double>));

            SCFR_ID = (int)info.GetValue("SCFR_ID", typeof(int));
            SCFRCurrentID = (int)info.GetValue("CurrentSCFR_ID", typeof(int));
            SCFRCounter = (int)info.GetValue("SCFR_Counter", typeof(int));

            SCFRDataTable = (DataTable)info.GetValue("SCFRDataTable", typeof(DataTable));

        } 
        #endregion

        #region Serialization of the SCFR Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SCFR_Name", _SCName);

            #region Serialization of Coordinates
            info.AddValue("A1x_FR", A1x);
            info.AddValue("A1y_FR", A1y);
            info.AddValue("A1z_FR", A1z);

            info.AddValue("B1x_FR", B1x);
            info.AddValue("B1y_FR", B1y);
            info.AddValue("B1z_FR", B1z);

            info.AddValue("C1x_FR", C1x);
            info.AddValue("C1y_FR", C1y);
            info.AddValue("C1z_FR", C1z);

            info.AddValue("D1x_FR", D1x);
            info.AddValue("D1y_FR", D1y);
            info.AddValue("D1z_FR", D1z);

            info.AddValue("E1x_FR", E1x);
            info.AddValue("E1y_FR", E1y);
            info.AddValue("E1z_FR", E1z);

            info.AddValue("F1x_FR", F1x);
            info.AddValue("F1y_FR", F1y);
            info.AddValue("F1z_FR", F1z);

            info.AddValue("G1x_FR", G1x);
            info.AddValue("G1y_FR", G1y);
            info.AddValue("G1z_FR", G1z);

            info.AddValue("H1x_FR", H1x);
            info.AddValue("H1y_FR", H1y);
            info.AddValue("H1z_FR", H1z);

            info.AddValue("I1x_FR", I1x);
            info.AddValue("I1y_FR", I1y);
            info.AddValue("I1z_FR", I1z);

            info.AddValue("J1x_FR", J1x);
            info.AddValue("J1y_FR", J1y);
            info.AddValue("J1z_FR", J1z);

            info.AddValue("JO1x_FR", JO1x);
            info.AddValue("JO1y_FR", JO1y);
            info.AddValue("JO1z_FR", JO1z);

            info.AddValue("K1x_FR", K1x);
            info.AddValue("K1y_FR", K1y);
            info.AddValue("K1z_FR", K1z);

            info.AddValue("L1x_FR", L1x);
            info.AddValue("L1y_FR", L1y);
            info.AddValue("L1z_FR", L1z);

            info.AddValue("M1x_FR", M1x);
            info.AddValue("M1y_FR", M1y);
            info.AddValue("M1z_FR", M1z);

            info.AddValue("N1x_FR", N1x);
            info.AddValue("N1y_FR", N1y);
            info.AddValue("N1z_FR", N1z);

            info.AddValue("O1x_FR", O1x);
            info.AddValue("O1y_FR", O1y);
            info.AddValue("O1z_FR", O1z);

            info.AddValue("P1x_FR", P1x);
            info.AddValue("P1y_FR", P1y);
            info.AddValue("P1z_FR", P1z);

            info.AddValue("Q1x_FR", Q1x);
            info.AddValue("Q1y_FR", Q1y);
            info.AddValue("Q1z_FR", Q1z);

            info.AddValue("R1x_FR", R1x);
            info.AddValue("R1y_FR", R1y);
            info.AddValue("R1z_FR", R1z);

            info.AddValue("W1x_FR", W1x);
            info.AddValue("W1y_FR", W1y);
            info.AddValue("W1z_FR", W1z);

            info.AddValue("RideHeightRefx_FR", RideHeightRefx);
            info.AddValue("RideHeightRefy_FR", RideHeightRefy);
            info.AddValue("RideHeightRefz_FR", RideHeightRefz);
            #endregion

            #region Serialization of the Suspension Type Variables
            info.AddValue("Front_Symmetry", FrontSymmetry);
            info.AddValue("DoubleWishbone_Identifier_Front", DoubleWishboneIdentifierFront);
            info.AddValue("McPherson_Identifier_Front", McPhersonIdentifierFront);
            info.AddValue("Pushrod_Identifier_Front", PushrodIdentifierFront);
            info.AddValue("Pullrod_Identifier_Front", PullrodIdentifierFront);
            info.AddValue("UARB_Identifier_Front", UARBIdentifierFront);
            info.AddValue("TARB_Identifier_Front", TARBIdentifierFront);
            #endregion

            #region Serialization of the Link Lengths
            info.AddValue("LowerFront_Length_FR", LowerFrontLength);
            info.AddValue("LowerRear_Length_FR", LowerRearLength);
            info.AddValue("UpperFront_Length_FR", UpperFrontLength);
            info.AddValue("UpperRear_Length_FR", UpperRearLength);
            info.AddValue("Pushrod_Length_FR", PushRodLength);
            info.AddValue("Pushrod_Length_1_FR", PushRodLength_1);
            info.AddValue("ToeLink_Length_FR", ToeLinkLength);
            info.AddValue("Damper_Length_FR", DamperLength);
            info.AddValue("ARBDroopLink_Length_FR", ARBDroopLinkLength);
            info.AddValue("ARBBlade_Length_FR", ARBBladeLength);
            #endregion

            info.AddValue("WheelDeflection_Steering", WheelDeflection_Steering);
            info.AddValue("SpringDeflection_DeltSteering", SpringDeflection_DeltSteering);

            info.AddValue("SCFR_ID", SCFR_ID);
            info.AddValue("CurrentSCFR_ID", SCFRCurrentID);
            info.AddValue("SCFR_Counter", SCFRCounter);

            info.AddValue("SCFRDataTable", SCFRDataTable);
        } 
        #endregion


    }
}

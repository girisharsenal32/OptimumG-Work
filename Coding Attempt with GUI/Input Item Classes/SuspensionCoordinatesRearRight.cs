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
    /// This is Class representing the Rear Right Suspension Coordinate
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesRearRight : SuspensionCoordinatesMaster, ISerializable,ICommand
    {
        #region Rear Right Suspension Parameters
        public int SCRR_ID { get; set; }
        public static int SCRRCurrentID { get; set; }
        public bool SCRRIsModified { get; set; }

        public static int SCRRCounter = 0;

        public static bool IsUndoRedoCalledByLeft = false;

        #endregion

        #region Undo/Redo Stack 
        public Stack<ICommand> _UndocommandsSCRR = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsSCRR = new Stack<ICommand>();
        #endregion

        #region SCRR Data Table
        public DataTable SCRRDataTable { get; set; }
        #endregion

        #region Declaration and Initialization of the Global List of Suspension Coordinate Objects
        public static List<SuspensionCoordinatesRearRight> Assy_List_SCRR = new List<SuspensionCoordinatesRearRight>();
        #endregion

        #region Constructor

        #region Base Constructor
        public SuspensionCoordinatesRearRight()
        {
            SCRRIsModified = false;
        }
        #endregion

        #region Overloaded Constructor
        public SuspensionCoordinatesRearRight(SuspensionCoordinatesRearRightGUI _scrrGUI)
        {
            SCRRDataTable = _scrrGUI.SCRRDataTableGUI;

            #region Rear Right Cooridinates Initialization

            #region Fixed Points REAR RIGHT Initialization - Double Wishbone & McPherson
            //  Coordinates of Fixed Point A
            A1x = _scrrGUI.A1y;
            A1y = _scrrGUI.A1z;
            A1z = _scrrGUI.A1x;

            //  Coordinates of Fixed Point B
            B1x = _scrrGUI.B1y;
            B1y = _scrrGUI.B1z;
            B1z = _scrrGUI.B1x;

            //  Coordinates of Fixed Point C
            C1x = _scrrGUI.C1y;
            C1y = _scrrGUI.C1z;
            C1z = _scrrGUI.C1x;

            //  Coordinates of Fixed Point D
            D1x = _scrrGUI.D1y;
            D1y = _scrrGUI.D1z;
            D1z = _scrrGUI.D1x;

            // Initial Coordinates of Moving Point I
            I1x = _scrrGUI.I1y;
            I1y = _scrrGUI.I1z;
            I1z = _scrrGUI.I1x;

            // Initial Coordinates of Moving Point Jo
            JO1x = _scrrGUI.JO1y;
            JO1y = _scrrGUI.JO1z;
            JO1z = _scrrGUI.JO1x;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            N1x = _scrrGUI.N1y;
            N1y = _scrrGUI.N1z;
            N1z = _scrrGUI.N1x;

            //  Coordinates of Fixed Point Q
            Q1x = _scrrGUI.Q1y;
            Q1y = _scrrGUI.Q1z;
            Q1z = _scrrGUI.Q1x;

            // Coordinates of Fixed Point R
            R1x = _scrrGUI.R1y;
            R1y = _scrrGUI.R1z;
            R1z = _scrrGUI.R1x;

            #endregion

            #region Moving Points REAR RIGHT Initialization - Double Wishbone & McPherson
            // Initial Coordinates of Moving Point J
            J1x = _scrrGUI.J1y;
            J1y = _scrrGUI.J1z;
            J1z = _scrrGUI.J1x;

            // Initial Coordinates of Moving Point H
            H1x = _scrrGUI.H1y;
            H1y = _scrrGUI.H1z;
            H1z = _scrrGUI.H1x;

            // Initial Coordinates of Moving Point G
            G1x = _scrrGUI.G1y;
            G1y = _scrrGUI.G1z;
            G1z = _scrrGUI.G1x;

            // Initial Coordinates of Moving Point F
            F1x = _scrrGUI.F1y;
            F1y = _scrrGUI.F1z;
            F1z = _scrrGUI.F1x;

            // Initial Coordinates of Moving Point E
            E1x = _scrrGUI.E1y;
            E1y = _scrrGUI.E1z;
            E1z = _scrrGUI.E1x;

            // Initial Coordinates of Moving Point K
            K1x = _scrrGUI.K1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            K1y = _scrrGUI.K1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            K1z = _scrrGUI.K1x;

            // Initial Coordinates of Moving Point L
            L1x = _scrrGUI.W1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            L1y = _scrrGUI.W1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            L1z = _scrrGUI.W1x;

            // Initial Coordinates of Moving Point M
            M1x = _scrrGUI.M1y;
            M1y = _scrrGUI.M1z;
            M1z = _scrrGUI.M1x;

            // Initial Coordinates of Moving Point O
            O1x = _scrrGUI.O1y;
            O1y = _scrrGUI.O1z;
            O1z = _scrrGUI.O1x;

            // Initial Coordinates of Moving Point P
            P1x = _scrrGUI.P1y;
            P1y = _scrrGUI.P1z;
            P1z = _scrrGUI.P1x;

            //  Coordinates of Moving Contact Patch Point W
            W1x = _scrrGUI.W1y;
            W1y = _scrrGUI.W1z;
            W1z = _scrrGUI.W1x;

            //  Ride Height Reference Points
            RideHeightRefx = _scrrGUI.RideHeightRefy;
            RideHeightRefy = _scrrGUI.RideHeightRefz;
            RideHeightRefz = _scrrGUI.RideHeightRefx;
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

            #endregion
        }  
        #endregion

        #endregion

        #region Create new SCRR Method
        public void CreateNewSCRR(int i_create_scrr, SuspensionCoordinatesRearRightGUI create_scrrGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new SCRR and addes it to the List of SCRR objects 
            ///</summary>

            #region Adding a new SCRR obbject to the list of SCRR objects
            SuspensionCoordinatesRearRightGUI scrrGUI = create_scrrGUI_list;
            Assy_List_SCRR.Insert(i_create_scrr, new SuspensionCoordinatesRearRight(scrrGUI));
            Assy_List_SCRR[i_create_scrr].RearSuspensionTyppe(scrrGUI);
            Assy_List_SCRR[i_create_scrr]._SCName = "Rear Right Coordinates " + Convert.ToString(i_create_scrr + 1);
            Assy_List_SCRR[i_create_scrr].SCRR_ID = i_create_scrr + 1;
            Assy_List_SCRR[i_create_scrr]._UndocommandsSCRR = new Stack<ICommand>();
            Assy_List_SCRR[i_create_scrr]._RedocommandsSCRR = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Redo Method
        public void ModifyObjectData(int l_modify_scrr, object modify_scrr_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the Suspension is bring modified and it is placed under the method called ModifyObjectData because it is an Undoable operation 
            ///</summary>

            #region Redoing the modification
            SuspensionCoordinatesRearRight _scrr_forRedo = (SuspensionCoordinatesRearRight)modify_scrr_list;

            ICommand cmd = Assy_List_SCRR[l_modify_scrr];
            Assy_List_SCRR[l_modify_scrr]._UndocommandsSCRR.Push(cmd);

            Assy_List_SCRR[l_modify_scrr] = _scrr_forRedo;

            PopulateDataTable(l_modify_scrr);

            Assy_List_SCRR[l_modify_scrr].SCRRIsModified = true;

            SuspensionCoordinatesRearRightGUI.DisplaySCRRItem(Assy_List_SCRR[l_modify_scrr]);

            #region Calling Redo method for Opposite Suspension if symmetric
            if (Assy_List_SCRR[l_modify_scrr].RearSymmetry == true && IsUndoRedoCalledByLeft == false)
            {
                SuspensionCoordinatesRear.IsUndoRedoCalledByRight_IdentifierMethod(true);// This method sets the IsUndoRedoCalledByRight variable to true and prevents an infinte loop
                UndoRedo undoRedo = new UndoRedo();
                undoRedo.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[l_modify_scrr]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[l_modify_scrr]._RedocommandsSCRL,
                                     l_modify_scrr + 1, SuspensionCoordinatesRear.Assy_List_SCRL[l_modify_scrr].SCRLIsModified);
                undoRedo.Redo(1);
                SuspensionCoordinatesRear.IsUndoRedoCalledByRight_IdentifierMethod(false);// This method sets the IsUndoRedoCalledByRight variable to false and allows the left suspenson coordinate to be undone
            }
            #endregion

            Kinematics_Software_New.EditRearCAD(l_modify_scrr);

            Kinematics_Software_New.SCRR_ModifyInVehicle(l_modify_scrr, Assy_List_SCRR[l_modify_scrr]);


            #endregion
        }
        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexcute_scrr, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>


            #region Undoing the modification
            try
            {
                SuspensionCoordinatesRearRight _scrr_forUndo = (SuspensionCoordinatesRearRight)command;

                ICommand cmd = Assy_List_SCRR[l_unexcute_scrr];
                Assy_List_SCRR[l_unexcute_scrr]._RedocommandsSCRR.Push(cmd);

                Assy_List_SCRR[l_unexcute_scrr] = _scrr_forUndo;

                PopulateDataTable(l_unexcute_scrr);

                SuspensionCoordinatesRearRightGUI.DisplaySCRRItem(Assy_List_SCRR[l_unexcute_scrr]);

                #region Calling Undo method for Opposite Suspension if symmetric
                if (Assy_List_SCRR[l_unexcute_scrr].RearSymmetry == true && IsUndoRedoCalledByLeft == false)
                {
                    SuspensionCoordinatesRear.IsUndoRedoCalledByRight_IdentifierMethod(true);// This method sets the IsUndoRedoCalledByRight variable to true and prevents an infinte loop
                    UndoRedo undoRedo = new UndoRedo();
                    undoRedo.Identifier(SuspensionCoordinatesRear.Assy_List_SCRL[l_unexcute_scrr]._UndocommandsSCRL, SuspensionCoordinatesRear.Assy_List_SCRL[l_unexcute_scrr]._RedocommandsSCRL,
                                         l_unexcute_scrr + 1, SuspensionCoordinatesRear.Assy_List_SCRL[l_unexcute_scrr].SCRLIsModified);
                    undoRedo.Undo(1);
                    SuspensionCoordinatesRear.IsUndoRedoCalledByRight_IdentifierMethod(false);// This method sets the IsUndoRedoCalledByRight variable to false and allows the left suspenson coordinate to be undone
                } 
                #endregion

                Kinematics_Software_New.EditRearCAD(l_unexcute_scrr);

                Kinematics_Software_New.SCRR_ModifyInVehicle(l_unexcute_scrr, Assy_List_SCRR[l_unexcute_scrr]);


            }
            catch (Exception) { }
            #endregion

        }
        #endregion

        public static void IsUndoRedoCalledByLeft_IdentifierMethod(bool value) => IsUndoRedoCalledByLeft = value;

        #region Populate Data Table Method
        private void PopulateDataTable(int l_modify_scrr)
        {
            #region Populating Table for DOUBLE WISHBONE
            if (Assy_List_SCRR[l_modify_scrr].DoubleWishboneIdentifierRear == 1)
            {
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].D1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].D1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].D1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].C1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].C1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].C1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].A1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].A1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].A1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].B1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].B1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].B1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].I1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].I1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].I1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[5].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].Q1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[5].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].Q1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[5].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].Q1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[6].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].N1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[6].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].N1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[6].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].N1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[7].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].JO1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[7].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].JO1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[7].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].JO1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[8].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].RideHeightRefz);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[8].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].RideHeightRefx);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[8].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].RideHeightRefy);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[9].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].J1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[9].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].J1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[9].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].J1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[10].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].H1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[10].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].H1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[10].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].H1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[11].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].O1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[11].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].O1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[11].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].O1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[12].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].G1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[12].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].G1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[12].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].G1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[13].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].F1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[13].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].F1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[13].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].F1y);


                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[14].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].E1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[14].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].E1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[14].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].E1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[15].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].P1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[15].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].P1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[15].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].P1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[16].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].K1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[16].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].K1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[16].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].K1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[17].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].M1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[17].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].M1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[17].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].M1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[18].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].W1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[18].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].W1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[18].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].W1y);

                if (Assy_List_SCRR[l_modify_scrr].TARBIdentifierRear == 1)
                {
                    Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[19].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].R1z);
                    Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[19].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].R1x);
                    Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[19].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].R1y);
                }

            }
            #endregion

            #region Populating Table for McPHERSON
            if (Assy_List_SCRR[l_modify_scrr].McPhersonIdentifierRear == 1)
            {
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].D1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].D1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].D1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].C1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].C1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].C1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].Q1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].Q1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].Q1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].N1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].N1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].N1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].JO1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].JO1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].JO1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[5].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].RideHeightRefz);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[5].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].RideHeightRefx);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[5].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].RideHeightRefy);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[6].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].J1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[6].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].J1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[6].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].J1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[7].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].E1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[7].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].E1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[7].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].E1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[8].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].P1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[8].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].P1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[8].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].P1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[9].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].K1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[9].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].K1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[9].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].K1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[10].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].M1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[10].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].M1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[10].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].M1y);

                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[11].SetField<double>("X (mm)", Assy_List_SCRR[l_modify_scrr].W1z);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[11].SetField<double>("Y (mm)", Assy_List_SCRR[l_modify_scrr].W1x);
                Assy_List_SCRR[l_modify_scrr].SCRRDataTable.Rows[11].SetField<double>("Z (mm)", Assy_List_SCRR[l_modify_scrr].W1y);


            }
            #endregion
        }  
        #endregion

        #region Method to assign the identifiers which will establish the type of suspension
        public void RearSuspensionTyppe(SuspensionCoordinatesRearRightGUI _scrrGUI)
        {
            #region Determining the Suspension Type using the GUI Object
            RearSymmetry = _scrrGUI.RearSymmetryGUI;

            DoubleWishboneIdentifierRear = _scrrGUI.DoubleWishboneIdentifierRear;
            McPhersonIdentifierRear = _scrrGUI.McPhersonIdentifierRear;

            PushrodIdentifierRear = _scrrGUI.PushrodIdentifierRear;
            PullrodIdentifierRear = _scrrGUI.PullrodIdentifierRear;

            UARBIdentifierRear = _scrrGUI.UARBIdentifierRear;
            TARBIdentifierRear = _scrrGUI.TARBIdentifierRear;
            #endregion
        } 
        #endregion

        #region Method to Edit the Rear Right Suspension
        public void EditRearSuspension(int l_edit_scrr,SuspensionCoordinatesRearRightGUI _scrrGUI)
        {
            ICommand cmd = Assy_List_SCRR[l_edit_scrr];
            Assy_List_SCRR[l_edit_scrr]._UndocommandsSCRR.Push(cmd);

            #region Rear Right Cooridinates Editing
            SuspensionCoordinatesRearRight scrr_list = new SuspensionCoordinatesRearRight(_scrrGUI);
            scrr_list._UndocommandsSCRR = Assy_List_SCRR[l_edit_scrr]._UndocommandsSCRR;
            scrr_list._RedocommandsSCRR = Assy_List_SCRR[l_edit_scrr]._RedocommandsSCRR;
            scrr_list._SCName = Assy_List_SCRR[l_edit_scrr]._SCName;

            Assy_List_SCRR[l_edit_scrr] = scrr_list;
            Assy_List_SCRR[l_edit_scrr].SCRRDataTable = scrr_list.SCRRDataTable;
            Assy_List_SCRR[l_edit_scrr].SCRR_ID = l_edit_scrr + 1;
            Assy_List_SCRR[l_edit_scrr].RearSuspensionTyppe(_scrrGUI);
            Assy_List_SCRR[l_edit_scrr].SCRRIsModified = true;
            
            PopulateDataTable(l_edit_scrr);

            #endregion

            _RedocommandsSCRR.Clear();

        } 
        #endregion

        #region De-serialization of the SCRR Object's Datag
        public SuspensionCoordinatesRearRight(SerializationInfo info, StreamingContext context)
        {
            _SCName = (string)info.GetValue("SCRR_Name", typeof(string));

            #region De-serialization of the Coordinates
            A1x = (double)info.GetValue("A1x_RR", typeof(double));
            A1y = (double)info.GetValue("A1y_RR", typeof(double));
            A1z = (double)info.GetValue("A1z_RR", typeof(double));

            B1x = (double)info.GetValue("B1x_RR", typeof(double));
            B1y = (double)info.GetValue("B1y_RR", typeof(double));
            B1z = (double)info.GetValue("B1z_RR", typeof(double));

            C1x = (double)info.GetValue("C1x_RR", typeof(double));
            C1y = (double)info.GetValue("C1y_RR", typeof(double));
            C1z = (double)info.GetValue("C1z_RR", typeof(double));

            D1x = (double)info.GetValue("D1x_RR", typeof(double));
            D1y = (double)info.GetValue("D1y_RR", typeof(double));
            D1z = (double)info.GetValue("D1z_RR", typeof(double));

            E1x = (double)info.GetValue("E1x_RR", typeof(double));
            E1y = (double)info.GetValue("E1y_RR", typeof(double));
            E1z = (double)info.GetValue("E1z_RR", typeof(double));

            F1x = (double)info.GetValue("F1x_RR", typeof(double));
            F1y = (double)info.GetValue("F1y_RR", typeof(double));
            F1z = (double)info.GetValue("F1z_RR", typeof(double));

            G1x = (double)info.GetValue("G1x_RR", typeof(double));
            G1y = (double)info.GetValue("G1y_RR", typeof(double));
            G1z = (double)info.GetValue("G1z_RR", typeof(double));

            H1x = (double)info.GetValue("H1x_RR", typeof(double));
            H1y = (double)info.GetValue("H1y_RR", typeof(double));
            H1z = (double)info.GetValue("H1z_RR", typeof(double));

            I1x = (double)info.GetValue("I1x_RR", typeof(double));
            I1y = (double)info.GetValue("I1y_RR", typeof(double));
            I1z = (double)info.GetValue("I1z_RR", typeof(double));

            J1x = (double)info.GetValue("J1x_RR", typeof(double));
            J1y = (double)info.GetValue("J1y_RR", typeof(double));
            J1z = (double)info.GetValue("J1z_RR", typeof(double));

            JO1x = (double)info.GetValue("JO1x_RR", typeof(double));
            JO1y = (double)info.GetValue("JO1y_RR", typeof(double));
            JO1z = (double)info.GetValue("JO1z_RR", typeof(double));

            K1x = (double)info.GetValue("K1x_RR", typeof(double));
            K1y = (double)info.GetValue("K1y_RR", typeof(double));
            K1z = (double)info.GetValue("K1z_RR", typeof(double));

            L1x = (double)info.GetValue("L1x_RR", typeof(double));
            L1y = (double)info.GetValue("L1y_RR", typeof(double));
            L1z = (double)info.GetValue("L1z_RR", typeof(double));

            M1x = (double)info.GetValue("M1x_RR", typeof(double));
            M1y = (double)info.GetValue("M1y_RR", typeof(double));
            M1z = (double)info.GetValue("M1z_RR", typeof(double));

            N1x = (double)info.GetValue("N1x_RR", typeof(double));
            N1y = (double)info.GetValue("N1y_RR", typeof(double));
            N1z = (double)info.GetValue("N1z_RR", typeof(double));

            O1x = (double)info.GetValue("O1x_RR", typeof(double));
            O1y = (double)info.GetValue("O1y_RR", typeof(double));
            O1z = (double)info.GetValue("O1z_RR", typeof(double));

            P1x = (double)info.GetValue("P1x_RR", typeof(double));
            P1y = (double)info.GetValue("P1y_RR", typeof(double));
            P1z = (double)info.GetValue("P1z_RR", typeof(double));

            Q1x = (double)info.GetValue("Q1x_RR", typeof(double));
            Q1y = (double)info.GetValue("Q1y_RR", typeof(double));
            Q1z = (double)info.GetValue("Q1z_RR", typeof(double));

            R1x = (double)info.GetValue("R1x_RR", typeof(double));
            R1y = (double)info.GetValue("R1y_RR", typeof(double));
            R1z = (double)info.GetValue("R1z_RR", typeof(double));

            W1x = (double)info.GetValue("W1x_RR", typeof(double));
            W1y = (double)info.GetValue("W1y_RR", typeof(double));
            W1z = (double)info.GetValue("W1z_RR", typeof(double));

            RideHeightRefx = (double)info.GetValue("RideHeightRefx_RR", typeof(double));
            RideHeightRefy = (double)info.GetValue("RideHeightRefy_RR", typeof(double));
            RideHeightRefz = (double)info.GetValue("RideHeightRefz_RR", typeof(double)); 
            #endregion

            #region De-serialization of the Suspension Type Varibales
            RearSymmetry = (bool)info.GetValue("RearSymmetry", typeof(bool));
            DoubleWishboneIdentifierRear = (int)info.GetValue("DoubleWishbone_Identifier_Rear", typeof(int));
            McPhersonIdentifierRear = (int)info.GetValue("McPherson_Identifier_Rear", typeof(int));
            PushrodIdentifierRear = (int)info.GetValue("Pushrod_Identifier_Rear", typeof(int));
            PullrodIdentifierRear = (int)info.GetValue("Pullrod_Identifier_Rear", typeof(int));
            UARBIdentifierRear = (int)info.GetValue("UARB_Identifier_Rear", typeof(int));
            TARBIdentifierRear = (int)info.GetValue("TARB_Identifier_Rear", typeof(int)); 
            #endregion

            #region Deserialization of the Link Lengths
            LowerFrontLength = (double)info.GetValue("LowerFront_Length_RR", typeof(double));
            LowerRearLength = (double)info.GetValue("LowerRear_Length_RR", typeof(double));
            UpperFrontLength = (double)info.GetValue("UpperFront_Length_RR", typeof(double));
            UpperRearLength = (double)info.GetValue("UpperRear_Length_RR", typeof(double));
            PushRodLength = (double)info.GetValue("Pushrod_Length_RR", typeof(double));
            PushRodLength_1 = (double)info.GetValue("Pushrod_Length_1_RR", typeof(double));
            ToeLinkLength = (double)info.GetValue("ToeLink_Length_RR", typeof(double));
            DamperLength = (double)info.GetValue("Damper_Length_RR", typeof(double));
            ARBDroopLinkLength = (double)info.GetValue("ARBDroopLink_Length_RR", typeof(double));
            ARBBladeLength = (double)info.GetValue("ARBBlade_Length_RR", typeof(double));
            #endregion

            //WheelDeflection_DiagonalWT_Steering = (List<double>)info.GetValue("WheelDeflection_DiagonalWT_Steering", typeof(List<double>));

            SCRR_ID = (int)info.GetValue("SCRR_ID", typeof(int));
            SCRRCurrentID = (int)info.GetValue("CurrentSRR_ID", typeof(int));
            SCRRCounter = (int)info.GetValue("SCRR_Counter", typeof(int));

            SCRRDataTable = (DataTable)info.GetValue("SCRRDataTable", typeof(DataTable));

        } 
        #endregion

        #region Serialization of the SCRR Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SCRR_Name", _SCName);

            #region Serialization of Coordinates
            info.AddValue("A1x_RR", A1x);
            info.AddValue("A1y_RR", A1y);
            info.AddValue("A1z_RR", A1z);

            info.AddValue("B1x_RR", B1x);
            info.AddValue("B1y_RR", B1y);
            info.AddValue("B1z_RR", B1z);

            info.AddValue("C1x_RR", C1x);
            info.AddValue("C1y_RR", C1y);
            info.AddValue("C1z_RR", C1z);

            info.AddValue("D1x_RR", D1x);
            info.AddValue("D1y_RR", D1y);
            info.AddValue("D1z_RR", D1z);

            info.AddValue("E1x_RR", E1x);
            info.AddValue("E1y_RR", E1y);
            info.AddValue("E1z_RR", E1z);

            info.AddValue("F1x_RR", F1x);
            info.AddValue("F1y_RR", F1y);
            info.AddValue("F1z_RR", F1z);

            info.AddValue("G1x_RR", G1x);
            info.AddValue("G1y_RR", G1y);
            info.AddValue("G1z_RR", G1z);

            info.AddValue("H1x_RR", H1x);
            info.AddValue("H1y_RR", H1y);
            info.AddValue("H1z_RR", H1z);

            info.AddValue("I1x_RR", I1x);
            info.AddValue("I1y_RR", I1y);
            info.AddValue("I1z_RR", I1z);

            info.AddValue("J1x_RR", J1x);
            info.AddValue("J1y_RR", J1y);
            info.AddValue("J1z_RR", J1z);

            info.AddValue("JO1x_RR", JO1x);
            info.AddValue("JO1y_RR", JO1y);
            info.AddValue("JO1z_RR", JO1z);

            info.AddValue("K1x_RR", K1x);
            info.AddValue("K1y_RR", K1y);
            info.AddValue("K1z_RR", K1z);

            info.AddValue("L1x_RR", L1x);
            info.AddValue("L1y_RR", L1y);
            info.AddValue("L1z_RR", L1z);

            info.AddValue("M1x_RR", M1x);
            info.AddValue("M1y_RR", M1y);
            info.AddValue("M1z_RR", M1z);

            info.AddValue("N1x_RR", N1x);
            info.AddValue("N1y_RR", N1y);
            info.AddValue("N1z_RR", N1z);

            info.AddValue("O1x_RR", O1x);
            info.AddValue("O1y_RR", O1y);
            info.AddValue("O1z_RR", O1z);

            info.AddValue("P1x_RR", P1x);
            info.AddValue("P1y_RR", P1y);
            info.AddValue("P1z_RR", P1z);

            info.AddValue("Q1x_RR", Q1x);
            info.AddValue("Q1y_RR", Q1y);
            info.AddValue("Q1z_RR", Q1z);

            info.AddValue("R1x_RR", R1x);
            info.AddValue("R1y_RR", R1y);
            info.AddValue("R1z_RR", R1z);

            info.AddValue("W1x_RR", W1x);
            info.AddValue("W1y_RR", W1y);
            info.AddValue("W1z_RR", W1z);

            info.AddValue("RideHeightRefx_RR", RideHeightRefx);
            info.AddValue("RideHeightRefy_RR", RideHeightRefy);
            info.AddValue("RideHeightRefz_RR", RideHeightRefz);
            #endregion

            #region Serialization of the Suspension Type Variables
            info.AddValue("RearSymmetry", RearSymmetry);
            info.AddValue("DoubleWishbone_Identifier_Rear", DoubleWishboneIdentifierRear);
            info.AddValue("McPherson_Identifier_Rear", McPhersonIdentifierRear);
            info.AddValue("Pushrod_Identifier_Rear", PushrodIdentifierRear);
            info.AddValue("Pullrod_Identifier_Rear", PullrodIdentifierRear);
            info.AddValue("UARB_Identifier_Rear", UARBIdentifierRear);
            info.AddValue("TARB_Identifier_Rear", TARBIdentifierRear);
            #endregion

            #region Serialization of the Link Lengths
            info.AddValue("LowerFront_Length_RR", LowerFrontLength);
            info.AddValue("LowerRear_Length_RR", LowerRearLength);
            info.AddValue("UpperFront_Length_RR", UpperFrontLength);
            info.AddValue("UpperRear_Length_RR", UpperRearLength);
            info.AddValue("Pushrod_Length_RR", PushRodLength);
            info.AddValue("Pushrod_Length_1_RR", PushRodLength_1);
            info.AddValue("ToeLink_Length_RR", ToeLinkLength);
            info.AddValue("Damper_Length_RR", DamperLength);
            info.AddValue("ARBDroopLink_Length_RR", ARBDroopLinkLength);
            info.AddValue("ARBBlade_Length_RR", ARBBladeLength);
            #endregion

            //info.AddValue("WheelDeflection_DiagonalWT_Steering", WheelDeflection_DiagonalWT_Steering);

            info.AddValue("SCRR_ID", SCRR_ID);
            info.AddValue("CurrentSRR_ID", SCRRCurrentID);
            info.AddValue("SCRR_Counter", SCRRCounter);

            info.AddValue("SCRRDataTable", SCRRDataTable);
        }
        #endregion


    }
}

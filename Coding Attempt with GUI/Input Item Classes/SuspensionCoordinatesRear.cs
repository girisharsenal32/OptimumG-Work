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
    /// This is Class representing the Rear Left Suspension Coordinate
    /// The object of this class is going to be used as an input to create the Vehicle and run the simulations
    /// It is initialized using its corresponding GUI class.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesRear : SuspensionCoordinatesMaster, ISerializable,ICommand
    {

        #region Rear Left Suspension Parameters
        public int SCRL_ID { get; set; }
        public static int SCRLCurrentID { get; set; }
        public bool SCRLIsModified { get; set; }

        public static int SCRLCounter = 0;

        public static bool IsUndoRedoCalledByRight = false;

        #endregion

        #region Undo/Redo Stack 
        public Stack<ICommand> _UndocommandsSCRL = new Stack<ICommand>();
        public Stack<ICommand> _RedocommandsSCRL = new Stack<ICommand>();
        #endregion

        #region SCRL Data Table
        public DataTable SCRLDataTable { get; set; }
        #endregion

        #region Declaration and Initialization of the Global Array and Globl List of Suspension Coordinate Objects
        public static List<SuspensionCoordinatesRear> Assy_List_SCRL = new List<SuspensionCoordinatesRear>();
        #endregion

        #region Constructor

        #region Base Constructor
        public SuspensionCoordinatesRear()
        {
            SCRLIsModified = false;
        }
        #endregion

        #region Overloaded Constructor
        public SuspensionCoordinatesRear(SuspensionCoordinatesRearGUI _scrlGUI)
        {
            SCRLDataTable = _scrlGUI.SCRLDataTableGUI;

            #region Rear Coordinates, Initialization

            #region Fixed Points REAR LEFT Initialization - Double Wishbone & McPherson
            //  Coordinates of Fixed Point A
            A1x = _scrlGUI.A1y;
            A1y = _scrlGUI.A1z;
            A1z = _scrlGUI.A1x;

            //  Coordinates of Fixed Point B
            B1x = _scrlGUI.B1y;
            B1y = _scrlGUI.B1z;
            B1z = _scrlGUI.B1x;

            //  Coordinates of Fixed Point C
            C1x = _scrlGUI.C1y;
            C1y = _scrlGUI.C1z;
            C1z = _scrlGUI.C1x;

            //  Coordinates of Fixed Point D
            D1x = _scrlGUI.D1y;
            D1y = _scrlGUI.D1z;
            D1z = _scrlGUI.D1x;

            // Initial Coordinates of Moving Point I
            I1x = _scrlGUI.I1y;
            I1y = _scrlGUI.I1z;
            I1z = _scrlGUI.I1x;

            // Initial Coordinates of Moving Point Jo
            JO1x = _scrlGUI.JO1y;
            JO1y = _scrlGUI.JO1z;
            JO1z = _scrlGUI.JO1x;

            // Initial Coordinates of Fixed (For now when there is no steering) Point N
            N1x = _scrlGUI.N1y;
            N1y = _scrlGUI.N1z;
            N1z = _scrlGUI.N1x;

            //  Coordinates of Fixed Point Q
            Q1x = _scrlGUI.Q1y;
            Q1y = _scrlGUI.Q1z;
            Q1z = _scrlGUI.Q1x;

            // Coordinates of Fixed Point R
            R1x = _scrlGUI.R1y;
            R1y = _scrlGUI.R1z;
            R1z = _scrlGUI.R1x;

            #endregion

            #region Moving Points REAR LEFT Initialization - Double Wishbone & McPherson
            // Initial Coordinates of Moving Point J
            J1x = _scrlGUI.J1y;
            J1y = _scrlGUI.J1z;
            J1z = _scrlGUI.J1x;

            // Initial Coordinates of Moving Point H
            H1x = _scrlGUI.H1y;
            H1y = _scrlGUI.H1z;
            H1z = _scrlGUI.H1x;

            // Initial Coordinates of Moving Point G
            G1x = _scrlGUI.G1y;
            G1y = _scrlGUI.G1z;
            G1z = _scrlGUI.G1x;

            // Initial Coordinates of Moving Point F
            F1x = _scrlGUI.F1y;
            F1y = _scrlGUI.F1z;
            F1z = _scrlGUI.F1x;

            // Initial Coordinates of Moving Point E
            E1x = _scrlGUI.E1y;
            E1y = _scrlGUI.E1z;
            E1z = _scrlGUI.E1x;

            // Initial Coordinates of Moving Point K
            K1x = _scrlGUI.K1y; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            K1y = _scrlGUI.K1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            K1z = _scrlGUI.K1x;

            // Initial Coordinates of Moving Point L
            L1x = _scrlGUI.K1y + 157.48; //IN THE HELPFILE CLEAFLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            L1y = _scrlGUI.K1z;//PATCH CENTRE - 1/2 TIRE WIDTH
            L1z = _scrlGUI.K1x;

            // Initial Coordinates of Moving Point M
            M1x = _scrlGUI.M1y;
            M1y = _scrlGUI.M1z;
            M1z = _scrlGUI.M1x;

            // Initial Coordinates of Moving Point O
            O1x = _scrlGUI.O1y;
            O1y = _scrlGUI.O1z;
            O1z = _scrlGUI.O1x;

            // Initial Coordinates of Moving Point P
            P1x = _scrlGUI.P1y;
            P1y = _scrlGUI.P1z;
            P1z = _scrlGUI.P1x;

            //  Coordinates of Moving Contact Patch Point W
            W1x = _scrlGUI.W1y;
            W1y = _scrlGUI.W1z;
            W1z = _scrlGUI.W1x;

            //  Ride Height Reference Points
            RideHeightRefx = _scrlGUI.RideHeightRefy;
            RideHeightRefy = _scrlGUI.RideHeightRefz;
            RideHeightRefz = _scrlGUI.RideHeightRefx;
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

        #region Create new method
        public void CreateNewSCRL(int i_create_scrl, SuspensionCoordinatesRearGUI create_scrlGUI_list)
        {
            ///<<summary>
            ///This section of the code creates a new SCRL and addes it to the List of SCRL objects 
            ///</summary>


            #region Adding a new SCRL object to the list of SCRL objects
            SuspensionCoordinatesRearGUI scrlGUI = create_scrlGUI_list;
            Assy_List_SCRL.Insert(i_create_scrl, new SuspensionCoordinatesRear(scrlGUI));
            Assy_List_SCRL[i_create_scrl].RearSuspensionType(scrlGUI);
            Assy_List_SCRL[i_create_scrl]._SCName = "Rear Left Coordinates " + Convert.ToString(i_create_scrl + 1);
            Assy_List_SCRL[i_create_scrl].SCRL_ID = i_create_scrl + 1;
            Assy_List_SCRL[i_create_scrl]._UndocommandsSCRL = new Stack<ICommand>();
            Assy_List_SCRL[i_create_scrl]._RedocommandsSCRL = new Stack<ICommand>();
            #endregion
        } 
        #endregion

        #region Redo Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="l_modify_scrl"></param>
        /// <param name="modify_scrl_list"></param>
        /// <param name="redo_Identifier"></param>
        public void ModifyObjectData(int l_modify_scrl, object modify_scrl_list, bool redo_Identifier)
        {
            ///<summary>
            ///In this section of the code, the Suspension is bring modified and it is placed under the method called ModifyObjectData because it is an Undoable operation 
            ///</summary>

            #region Redoing the modification
            SuspensionCoordinatesRear _scrl_forRedo = (SuspensionCoordinatesRear)modify_scrl_list;

            ICommand cmd = Assy_List_SCRL[l_modify_scrl];
            Assy_List_SCRL[l_modify_scrl]._UndocommandsSCRL.Push(cmd);

            Assy_List_SCRL[l_modify_scrl] = _scrl_forRedo;

            PopulateDataTable(l_modify_scrl);

            Assy_List_SCRL[l_modify_scrl].SCRLIsModified = true;

            SuspensionCoordinatesRearGUI.DisplaySCRLItem(Assy_List_SCRL[l_modify_scrl]);

            #region Calling Redo method for Opposite Suspension if symmetric
            if (Assy_List_SCRL[l_modify_scrl].RearSymmetry == true && IsUndoRedoCalledByRight == false)
            {
                SuspensionCoordinatesRearRight.IsUndoRedoCalledByLeft_IdentifierMethod(true);
                UndoRedo undoRedo = new UndoRedo();
                undoRedo.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[l_modify_scrl]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_modify_scrl]._RedocommandsSCRR,
                                     l_modify_scrl + 1, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_modify_scrl].SCRRIsModified);
                undoRedo.Redo(1);
                SuspensionCoordinatesRearRight.IsUndoRedoCalledByLeft_IdentifierMethod(false);

            } 
            #endregion

            Kinematics_Software_New.EditRearCAD(l_modify_scrl);

            Kinematics_Software_New.SCRL_ModifyInVehicle(l_modify_scrl, Assy_List_SCRL[l_modify_scrl]);


            #endregion

        }

        #endregion

        #region Undo Method
        public void Undo_ModifyObjectData(int l_unexcute_scrl, ICommand command)
        {
            ///<summary>
            /// This code is to undo the modification action which the user has performed
            /// </summary>

            #region Undoing the modification
            try
            {
                SuspensionCoordinatesRear _scrl_forUndo = (SuspensionCoordinatesRear)command;

                ICommand cmd = Assy_List_SCRL[l_unexcute_scrl];
                Assy_List_SCRL[l_unexcute_scrl]._RedocommandsSCRL.Push(cmd);

                Assy_List_SCRL[l_unexcute_scrl] = _scrl_forUndo;

                PopulateDataTable(l_unexcute_scrl);

                SuspensionCoordinatesRearGUI.DisplaySCRLItem(Assy_List_SCRL[l_unexcute_scrl]);

                #region Calling Undo method for Opposite Suspension if symmetric
                if (Assy_List_SCRL[l_unexcute_scrl].RearSymmetry == true && IsUndoRedoCalledByRight == false)
                {
                    SuspensionCoordinatesRearRight.IsUndoRedoCalledByLeft_IdentifierMethod(true);// This method sets the IsUndoRedoCalledByLeft variable to true and prevents an infinte loop

                    UndoRedo undoRedo = new UndoRedo();
                    undoRedo.Identifier(SuspensionCoordinatesRearRight.Assy_List_SCRR[l_unexcute_scrl]._UndocommandsSCRR, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_unexcute_scrl]._RedocommandsSCRR,
                                         l_unexcute_scrl + 1, SuspensionCoordinatesRearRight.Assy_List_SCRR[l_unexcute_scrl].SCRRIsModified);
                    undoRedo.Undo(1);
                    SuspensionCoordinatesRearRight.IsUndoRedoCalledByLeft_IdentifierMethod(false);//This method sets the value of IsUndoRedoCalledByLeft to false so that the Right Suspension coordinate can also be Undone

                } 
                #endregion

                Kinematics_Software_New.EditRearCAD(l_unexcute_scrl);

                Kinematics_Software_New.SCRL_ModifyInVehicle(l_unexcute_scrl, Assy_List_SCRL[l_unexcute_scrl]);


            }
            catch (Exception) { } 
            #endregion
        }
        #endregion

        public static void IsUndoRedoCalledByRight_IdentifierMethod(bool value) => IsUndoRedoCalledByRight = value;

        #region Populate Data Table Method
        private void PopulateDataTable(int l_modify_scrl)
        {
            #region Populating Table for DOUBLE WISHBONE
            if (Assy_List_SCRL[l_modify_scrl].DoubleWishboneIdentifierRear == 1)
            {
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].D1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].D1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].D1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].C1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].C1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].C1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].A1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].A1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].A1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].B1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].B1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].B1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].I1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].I1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].I1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[5].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].Q1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[5].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].Q1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[5].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].Q1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[6].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].N1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[6].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].N1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[6].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].N1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[7].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].JO1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[7].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].JO1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[7].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].JO1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[8].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].RideHeightRefz);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[8].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].RideHeightRefx);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[8].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].RideHeightRefy);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[9].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].J1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[9].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].J1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[9].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].J1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[10].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].H1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[10].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].H1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[10].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].H1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[11].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].O1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[11].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].O1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[11].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].O1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[12].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].G1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[12].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].G1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[12].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].G1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[13].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].F1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[13].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].F1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[13].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].F1y);


                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[14].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].E1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[14].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].E1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[14].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].E1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[15].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].P1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[15].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].P1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[15].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].P1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[16].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].K1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[16].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].K1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[16].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].K1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[17].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].M1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[17].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].M1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[17].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].M1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[18].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].W1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[18].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].W1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[18].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].W1y);

                if (Assy_List_SCRL[l_modify_scrl].TARBIdentifierRear == 1)
                {
                    Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[19].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].R1z);
                    Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[19].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].R1x);
                    Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[19].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].R1y);
                }

            }
            #endregion

            #region Populating Table for McPHERSON
            if (Assy_List_SCRL[l_modify_scrl].McPhersonIdentifierRear == 1)
            {
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[0].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].D1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[0].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].D1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[0].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].D1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[1].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].C1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[1].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].C1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[1].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].C1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[2].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].Q1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[2].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].Q1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[2].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].Q1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[3].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].N1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[3].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].N1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[3].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].N1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[4].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].JO1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[4].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].JO1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[4].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].JO1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[5].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].RideHeightRefz);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[5].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].RideHeightRefx);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[5].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].RideHeightRefy);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[6].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].J1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[6].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].J1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[6].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].J1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[7].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].E1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[7].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].E1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[7].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].E1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[8].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].P1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[8].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].P1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[8].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].P1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[9].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].K1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[9].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].K1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[9].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].K1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[10].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].M1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[10].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].M1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[10].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].M1y);

                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[11].SetField<double>("X (mm)", Assy_List_SCRL[l_modify_scrl].W1z);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[11].SetField<double>("Y (mm)", Assy_List_SCRL[l_modify_scrl].W1x);
                Assy_List_SCRL[l_modify_scrl].SCRLDataTable.Rows[11].SetField<double>("Z (mm)", Assy_List_SCRL[l_modify_scrl].W1y);


            }
            #endregion
        }  
        #endregion

        #region Method to assign the identifiers which will establish the type of suspension
        public void RearSuspensionType(SuspensionCoordinatesRearGUI _scrlGUI)
        {
            #region Determining the Suspension Type using the GUI Object
            RearSymmetry = _scrlGUI.RearSymmetryGUI;

            DoubleWishboneIdentifierRear = _scrlGUI.DoubleWishboneIdentifierRear;
            McPhersonIdentifierRear = _scrlGUI.McPhersonIdentifierRear;

            PushrodIdentifierRear = _scrlGUI.PushrodIdentifierRear;
            PullrodIdentifierRear = _scrlGUI.PullrodIdentifierRear;

            UARBIdentifierRear = _scrlGUI.UARBIdentifierRear;
            TARBIdentifierRear = _scrlGUI.TARBIdentifierRear;
            #endregion
        } 
        #endregion

        #region Method to Edit the Rear Left Suspension
        public void EditRearLeftSuspension(int l_edit_scrl ,SuspensionCoordinatesRearGUI _scrlGUI)
        {
            ICommand cmd = Assy_List_SCRL[l_edit_scrl];
            Assy_List_SCRL[l_edit_scrl]._UndocommandsSCRL.Push(cmd);

            #region Rear Left coordinates editing 
            SuspensionCoordinatesRear scrl_list = new SuspensionCoordinatesRear(_scrlGUI);
            scrl_list._UndocommandsSCRL = Assy_List_SCRL[l_edit_scrl]._UndocommandsSCRL;
            scrl_list._RedocommandsSCRL = Assy_List_SCRL[l_edit_scrl]._RedocommandsSCRL;
            scrl_list._SCName = Assy_List_SCRL[l_edit_scrl]._SCName;

            Assy_List_SCRL[l_edit_scrl] = scrl_list;
            Assy_List_SCRL[l_edit_scrl].SCRLDataTable = scrl_list.SCRLDataTable;
            Assy_List_SCRL[l_edit_scrl].SCRL_ID = l_edit_scrl + 1;
            Assy_List_SCRL[l_edit_scrl].RearSuspensionType(_scrlGUI);
            Assy_List_SCRL[l_edit_scrl].SCRLIsModified = true;

            PopulateDataTable(l_edit_scrl);

            #endregion

            _RedocommandsSCRL.Clear();
        } 
        #endregion

        #region De-serialization of the SCRL Object's Data
        public SuspensionCoordinatesRear(SerializationInfo info, StreamingContext context)
        {
            _SCName = (string)info.GetValue("SCRL_Name", typeof(string));

            #region De-serialization of the Coordinates
            A1x = (double)info.GetValue("A1x_RL", typeof(double));
            A1y = (double)info.GetValue("A1y_RL", typeof(double));
            A1z = (double)info.GetValue("A1z_RL", typeof(double));

            B1x = (double)info.GetValue("B1x_RL", typeof(double));
            B1y = (double)info.GetValue("B1y_RL", typeof(double));
            B1z = (double)info.GetValue("B1z_RL", typeof(double));

            C1x = (double)info.GetValue("C1x_RL", typeof(double));
            C1y = (double)info.GetValue("C1y_RL", typeof(double));
            C1z = (double)info.GetValue("C1z_RL", typeof(double));

            D1x = (double)info.GetValue("D1x_RL", typeof(double));
            D1y = (double)info.GetValue("D1y_RL", typeof(double));
            D1z = (double)info.GetValue("D1z_RL", typeof(double));

            E1x = (double)info.GetValue("E1x_RL", typeof(double));
            E1y = (double)info.GetValue("E1y_RL", typeof(double));
            E1z = (double)info.GetValue("E1z_RL", typeof(double));

            F1x = (double)info.GetValue("F1x_RL", typeof(double));
            F1y = (double)info.GetValue("F1y_RL", typeof(double));
            F1z = (double)info.GetValue("F1z_RL", typeof(double));

            G1x = (double)info.GetValue("G1x_RL", typeof(double));
            G1y = (double)info.GetValue("G1y_RL", typeof(double));
            G1z = (double)info.GetValue("G1z_RL", typeof(double));

            H1x = (double)info.GetValue("H1x_RL", typeof(double));
            H1y = (double)info.GetValue("H1y_RL", typeof(double));
            H1z = (double)info.GetValue("H1z_RL", typeof(double));

            I1x = (double)info.GetValue("I1x_RL", typeof(double));
            I1y = (double)info.GetValue("I1y_RL", typeof(double));
            I1z = (double)info.GetValue("I1z_RL", typeof(double));

            J1x = (double)info.GetValue("J1x_RL", typeof(double));
            J1y = (double)info.GetValue("J1y_RL", typeof(double));
            J1z = (double)info.GetValue("J1z_RL", typeof(double));

            JO1x = (double)info.GetValue("JO1x_RL", typeof(double));
            JO1y = (double)info.GetValue("JO1y_RL", typeof(double));
            JO1z = (double)info.GetValue("JO1z_RL", typeof(double));

            K1x = (double)info.GetValue("K1x_RL", typeof(double));
            K1y = (double)info.GetValue("K1y_RL", typeof(double));
            K1z = (double)info.GetValue("K1z_RL", typeof(double));

            L1x = (double)info.GetValue("L1x_RL", typeof(double));
            L1y = (double)info.GetValue("L1y_RL", typeof(double));
            L1z = (double)info.GetValue("L1z_RL", typeof(double));

            M1x = (double)info.GetValue("M1x_RL", typeof(double));
            M1y = (double)info.GetValue("M1y_RL", typeof(double));
            M1z = (double)info.GetValue("M1z_RL", typeof(double));

            N1x = (double)info.GetValue("N1x_RL", typeof(double));
            N1y = (double)info.GetValue("N1y_RL", typeof(double));
            N1z = (double)info.GetValue("N1z_RL", typeof(double));

            O1x = (double)info.GetValue("O1x_RL", typeof(double));
            O1y = (double)info.GetValue("O1y_RL", typeof(double));
            O1z = (double)info.GetValue("O1z_RL", typeof(double));

            P1x = (double)info.GetValue("P1x_RL", typeof(double));
            P1y = (double)info.GetValue("P1y_RL", typeof(double));
            P1z = (double)info.GetValue("P1z_RL", typeof(double));

            Q1x = (double)info.GetValue("Q1x_RL", typeof(double));
            Q1y = (double)info.GetValue("Q1y_RL", typeof(double));
            Q1z = (double)info.GetValue("Q1z_RL", typeof(double));

            R1x = (double)info.GetValue("R1x_RL", typeof(double));
            R1y = (double)info.GetValue("R1y_RL", typeof(double));
            R1z = (double)info.GetValue("R1z_RL", typeof(double));

            W1x = (double)info.GetValue("W1x_RL", typeof(double));
            W1y = (double)info.GetValue("W1y_RL", typeof(double));
            W1z = (double)info.GetValue("W1z_RL", typeof(double));

            RideHeightRefx = (double)info.GetValue("RideHeightRefx_RL", typeof(double));
            RideHeightRefy = (double)info.GetValue("RideHeightRefy_RL", typeof(double));
            RideHeightRefz = (double)info.GetValue("RideHeightRefz_RL", typeof(double)); 
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
            LowerFrontLength = (double)info.GetValue("LowerFront_Length_RL", typeof(double));
            LowerRearLength = (double)info.GetValue("LowerRear_Length_RL", typeof(double));
            UpperFrontLength = (double)info.GetValue("UpperFront_Length_RL", typeof(double));
            UpperRearLength = (double)info.GetValue("UpperRear_Length_RL", typeof(double));
            PushRodLength = (double)info.GetValue("Pushrod_Length_RL", typeof(double));
            PushRodLength_1 = (double)info.GetValue("Pushrod_Length_1_RL", typeof(double));
            ToeLinkLength = (double)info.GetValue("ToeLink_Length_RL", typeof(double));
            DamperLength = (double)info.GetValue("Damper_Length_RL", typeof(double));
            ARBDroopLinkLength = (double)info.GetValue("ARBDroopLink_Length_RL", typeof(double));
            ARBBladeLength = (double)info.GetValue("ARBBlade_Length_RL", typeof(double));
            #endregion

            //WheelDeflection_DiagonalWT_Steering = (List<double>) info.GetValue("WheelDeflection_DiagonalWT_Steering", typeof(List<double>));

            SCRL_ID = (int)info.GetValue("SCRL_ID", typeof(int));
            SCRLCurrentID = (int)info.GetValue("CurrentSCRL_ID", typeof(int));
            SCRLCounter = (int)info.GetValue("SCRL_Counter", typeof(int));

            SCRLDataTable = (DataTable)info.GetValue("SCRLDataTable", typeof(DataTable));

        } 
        #endregion

        #region Serialization of the SCRL Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SCRL_Name", _SCName);

            #region Serialization of Coordinates
            info.AddValue("A1x_RL", A1x);
            info.AddValue("A1y_RL", A1y);
            info.AddValue("A1z_RL", A1z);

            info.AddValue("B1x_RL", B1x);
            info.AddValue("B1y_RL", B1y);
            info.AddValue("B1z_RL", B1z);

            info.AddValue("C1x_RL", C1x);
            info.AddValue("C1y_RL", C1y);
            info.AddValue("C1z_RL", C1z);

            info.AddValue("D1x_RL", D1x);
            info.AddValue("D1y_RL", D1y);
            info.AddValue("D1z_RL", D1z);

            info.AddValue("E1x_RL", E1x);
            info.AddValue("E1y_RL", E1y);
            info.AddValue("E1z_RL", E1z);

            info.AddValue("F1x_RL", F1x);
            info.AddValue("F1y_RL", F1y);
            info.AddValue("F1z_RL", F1z);

            info.AddValue("G1x_RL", G1x);
            info.AddValue("G1y_RL", G1y);
            info.AddValue("G1z_RL", G1z);

            info.AddValue("H1x_RL", H1x);
            info.AddValue("H1y_RL", H1y);
            info.AddValue("H1z_RL", H1z);

            info.AddValue("I1x_RL", I1x);
            info.AddValue("I1y_RL", I1y);
            info.AddValue("I1z_RL", I1z);

            info.AddValue("J1x_RL", J1x);
            info.AddValue("J1y_RL", J1y);
            info.AddValue("J1z_RL", J1z);

            info.AddValue("JO1x_RL", JO1x);
            info.AddValue("JO1y_RL", JO1y);
            info.AddValue("JO1z_RL", JO1z);

            info.AddValue("K1x_RL", K1x);
            info.AddValue("K1y_RL", K1y);
            info.AddValue("K1z_RL", K1z);

            info.AddValue("L1x_RL", L1x);
            info.AddValue("L1y_RL", L1y);
            info.AddValue("L1z_RL", L1z);

            info.AddValue("M1x_RL", M1x);
            info.AddValue("M1y_RL", M1y);
            info.AddValue("M1z_RL", M1z);

            info.AddValue("N1x_RL", N1x);
            info.AddValue("N1y_RL", N1y);
            info.AddValue("N1z_RL", N1z);

            info.AddValue("O1x_RL", O1x);
            info.AddValue("O1y_RL", O1y);
            info.AddValue("O1z_RL", O1z);

            info.AddValue("P1x_RL", P1x);
            info.AddValue("P1y_RL", P1y);
            info.AddValue("P1z_RL", P1z);

            info.AddValue("Q1x_RL", Q1x);
            info.AddValue("Q1y_RL", Q1y);
            info.AddValue("Q1z_RL", Q1z);

            info.AddValue("R1x_RL", R1x);
            info.AddValue("R1y_RL", R1y);
            info.AddValue("R1z_RL", R1z);

            info.AddValue("W1x_RL", W1x);
            info.AddValue("W1y_RL", W1y);
            info.AddValue("W1z_RL", W1z);

            info.AddValue("RideHeightRefx_RL", RideHeightRefx);
            info.AddValue("RideHeightRefy_RL", RideHeightRefy);
            info.AddValue("RideHeightRefz_RL", RideHeightRefz);
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
            info.AddValue("LowerFront_Length_RL", LowerFrontLength);
            info.AddValue("LowerRear_Length_RL", LowerRearLength);
            info.AddValue("UpperFront_Length_RL", UpperFrontLength);
            info.AddValue("UpperRear_Length_RL", UpperRearLength);
            info.AddValue("Pushrod_Length_RL", PushRodLength);
            info.AddValue("Pushrod_Length_1_RL", PushRodLength_1);
            info.AddValue("ToeLink_Length_RL", ToeLinkLength);
            info.AddValue("Damper_Length_RL", DamperLength);
            info.AddValue("ARBDroopLink_Length_RL", ARBDroopLinkLength);
            info.AddValue("ARBBlade_Length_RL", ARBBladeLength);
            #endregion

            //info.AddValue("WheelDeflection_DiagonalWT_Steering", WheelDeflection_DiagonalWT_Steering);

            info.AddValue("SCRL_ID", SCRL_ID);
            info.AddValue("CurrentSCRL_ID", SCRLCurrentID);
            info.AddValue("SCRL_Counter", SCRLCounter);

            info.AddValue("SCRLDataTable", SCRLDataTable);
        }
        #endregion


    }
}

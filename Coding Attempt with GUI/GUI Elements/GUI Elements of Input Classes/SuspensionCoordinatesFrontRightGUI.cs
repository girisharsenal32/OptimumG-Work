using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Data;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This it the GUI class of the Front Right SUspension
    /// It receives its inputs from the Form which is displayed to the user.  
    /// It is made serializable so that the latest information regarding the Coordinates and Suspension Type can be saved and loaded
    /// It is used to initialize the Front Right Suspension Class Object.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesFrontRightGUI : SuspensionCoordinatesMasterGUI,ISerializable
    {
        #region SCFR Banded Grid View 
        public CustomBandedGridView bandedGridView_SCFRGUI = new CustomBandedGridView();
        #endregion

        #region SCFR Data Table
        public DataTable SCFRDataTableGUI { get; set; } 
        #endregion

        static Kinematics_Software_New r1;

        #region Constructor
        public SuspensionCoordinatesFrontRightGUI()
        {
            #region Initializing the SCFR Data Table
            SCFRDataTableGUI = new DataTable();

            SCFRDataTableGUI.TableName = "Front Right Suspension Coordinates";

            SCFRDataTableGUI.Columns.Add("Suspension Point", typeof(String));
            SCFRDataTableGUI.Columns[0].ReadOnly = true;

            SCFRDataTableGUI.Columns.Add("X (mm)", typeof(double));

            SCFRDataTableGUI.Columns.Add("Y (mm)", typeof(double));

            SCFRDataTableGUI.Columns.Add("Z (mm)", typeof(double));

            #endregion
        } 
        #endregion

        #region Method to obtain the identifiers from the form which will establish the type of suspension
        public void FrontSuspensionTypeGUI(Kinematics_Software_New r1)
        {
            #region Determining the Suspension Type for the GUI Class using the User Interface Object
            FrontSymmetryGUI = r1.FrontSymmetry;

            DoubleWishboneIdentifierFront = r1.DoubleWishboneFront_VehicleGUI;
            McPhersonIdentifierFront = r1.McPhersonFront_VehicleGUI;

            PushrodIdentifierFront = r1.PushrodFront_VehicleGUI;
            PullrodIdentifierFront = r1.PullrodFront_VehicleGUI;

            UARBIdentifierFront = r1.UARBFront_VehicleGUI;
            TARBIdentifierFront = r1.TARBFront_VehicleGUI;
            #endregion
        }
        public void FrontSuspensionTypeGUI(SuspensionType _susType)
        {
            FrontSymmetryGUI = _susType.FrontSymmetry_Boolean;

            DoubleWishboneIdentifierFront = _susType.DoubleWishboneFront;
            McPhersonIdentifierFront = _susType.McPhersonFront;

            PushrodIdentifierFront = _susType.PushrodFront;
            PullrodIdentifierFront = _susType.PullrodFront;

            UARBIdentifierFront = _susType.UARBFront;
            TARBIdentifierFront = _susType.TARBFront;

            NoOfCouplings = _susType.NoOfCouplings;
        }
        #endregion

        #region Method to edit the Front Right Suspension GUI
        public void EditFrontRightCoordinatesGUI(Kinematics_Software_New _r1, SuspensionCoordinatesFrontRightGUI _scfrGUI)
        {
            r1 = _r1;

            #region Editing the Front Left Suspension Coordinates GUI Class using its own Data Table which is modified through the User Interface's GridControl

            #region Editing the Coordinates if the Suspension Type is Double Wishbone
            if (_scfrGUI.DoubleWishboneIdentifierFront == 1)
            {
                #region DOUBLE WISHBONE

                #region Fixed Points DOUBLE WISHBONE
                //  Coordinates of Fixed Point D

                D1x = SCFRDataTableGUI.Rows[0].Field<double>(1);
                D1y = SCFRDataTableGUI.Rows[0].Field<double>(2);
                D1z = SCFRDataTableGUI.Rows[0].Field<double>(3);


                //  Coordinates of Fixed Point C

                C1x = SCFRDataTableGUI.Rows[1].Field<double>(1);
                C1y = SCFRDataTableGUI.Rows[1].Field<double>(2);
                C1z = SCFRDataTableGUI.Rows[1].Field<double>(3);


                //  Coordinates of Fixed Point A

                A1x = SCFRDataTableGUI.Rows[2].Field<double>(1);
                A1y = SCFRDataTableGUI.Rows[2].Field<double>(2);
                A1z = SCFRDataTableGUI.Rows[2].Field<double>(3);


                //  Coordinates of Fixed Point B

                B1x = SCFRDataTableGUI.Rows[3].Field<double>(1);
                B1y = SCFRDataTableGUI.Rows[3].Field<double>(2);
                B1z = SCFRDataTableGUI.Rows[3].Field<double>(3);


                // Initial Coordinates of Moving Point I

                I1x = SCFRDataTableGUI.Rows[4].Field<double>(1);
                I1y = SCFRDataTableGUI.Rows[4].Field<double>(2);
                I1z = SCFRDataTableGUI.Rows[4].Field<double>(3);


                // Initial Coordinates of Moving Point Q

                Q1x = SCFRDataTableGUI.Rows[5].Field<double>(1);
                Q1y = SCFRDataTableGUI.Rows[5].Field<double>(2);
                Q1z = SCFRDataTableGUI.Rows[5].Field<double>(3);


                //  Coordinates of Fixed Point N

                N1x = SCFRDataTableGUI.Rows[6].Field<double>(1);
                N1y = SCFRDataTableGUI.Rows[6].Field<double>(2);
                N1z = SCFRDataTableGUI.Rows[6].Field<double>(3);


                // Coordinates of Fixed Point JO

                JO1x = SCFRDataTableGUI.Rows[7].Field<double>(1);
                JO1y = SCFRDataTableGUI.Rows[7].Field<double>(2);
                JO1z = SCFRDataTableGUI.Rows[7].Field<double>(3);

                //  Ride Height Reference Points

                RideHeightRefx = SCFRDataTableGUI.Rows[8].Field<double>(1);
                RideHeightRefy = SCFRDataTableGUI.Rows[8].Field<double>(2);
                RideHeightRefz = SCFRDataTableGUI.Rows[8].Field<double>(3);

                if (_scfrGUI.TARBIdentifierFront == 1)
                {
                    // Initial Coordinates of Fixed Point R  (Only active when the it is T ARB)

                    R1x = SCFRDataTableGUI.Rows[19].Field<double>(1);
                    R1y = SCFRDataTableGUI.Rows[19].Field<double>(2);
                    R1z = SCFRDataTableGUI.Rows[19].Field<double>(3);
                }

                #endregion

                #region Moving Points DOUBLE WISHBONE
                // Initial Coordinates of Moving Point J

                J1x = SCFRDataTableGUI.Rows[9].Field<double>(1);
                J1y = SCFRDataTableGUI.Rows[9].Field<double>(2);
                J1z = SCFRDataTableGUI.Rows[9].Field<double>(3);


                // Initial Coordinates of Moving Point H

                H1x = SCFRDataTableGUI.Rows[10].Field<double>(1);
                H1y = SCFRDataTableGUI.Rows[10].Field<double>(2);
                H1z = SCFRDataTableGUI.Rows[10].Field<double>(3);


                // Initial Coordinates of Moving Point O

                O1x = SCFRDataTableGUI.Rows[11].Field<double>(1);
                O1y = SCFRDataTableGUI.Rows[11].Field<double>(2);
                O1z = SCFRDataTableGUI.Rows[11].Field<double>(3);


                // Initial Coordinates of Moving Point G

                G1x = SCFRDataTableGUI.Rows[12].Field<double>(1);
                G1y = SCFRDataTableGUI.Rows[12].Field<double>(2);
                G1z = SCFRDataTableGUI.Rows[12].Field<double>(3);


                // Initial Coordinates of Moving Point F

                F1x = SCFRDataTableGUI.Rows[13].Field<double>(1);
                F1y = SCFRDataTableGUI.Rows[13].Field<double>(2);
                F1z = SCFRDataTableGUI.Rows[13].Field<double>(3);


                // Initial Coordinates of Moving Point E

                E1x = SCFRDataTableGUI.Rows[14].Field<double>(1);
                E1y = SCFRDataTableGUI.Rows[14].Field<double>(2);
                E1z = SCFRDataTableGUI.Rows[14].Field<double>(3);


                // Initial Coordinates of Moving Point P

                P1x = SCFRDataTableGUI.Rows[15].Field<double>(1);
                P1y = SCFRDataTableGUI.Rows[15].Field<double>(2);
                P1z = SCFRDataTableGUI.Rows[15].Field<double>(3);


                // Initial Coordinates of Moving Point K

                K1x = SCFRDataTableGUI.Rows[16].Field<double>(1);
                K1y = SCFRDataTableGUI.Rows[16].Field<double>(2);
                K1z = SCFRDataTableGUI.Rows[16].Field<double>(3);


                // Initial Coordinates of Moving Point M

                M1x = SCFRDataTableGUI.Rows[17].Field<double>(1);
                M1y = SCFRDataTableGUI.Rows[17].Field<double>(2);
                M1z = SCFRDataTableGUI.Rows[17].Field<double>(3);


                //  Coordinates of Moving Contact Patch Point W

                W1x = SCFRDataTableGUI.Rows[18].Field<double>(1);
                W1y = SCFRDataTableGUI.Rows[18].Field<double>(2);
                W1z = SCFRDataTableGUI.Rows[18].Field<double>(3);

                #endregion

                #endregion
            }
            #endregion

            #region Editing the Coordinates if the Suspension Type is McPherson
            if (_scfrGUI.McPhersonIdentifierFront == 1)
            {
                #region MCPHERSON

                #region Fixed Points MCPHERSON
                //  Coordinates of Fixed Point D

                D1x = SCFRDataTableGUI.Rows[0].Field<double>(1);
                D1y = SCFRDataTableGUI.Rows[0].Field<double>(2);
                D1z = SCFRDataTableGUI.Rows[0].Field<double>(3);


                //  Coordinates of Fixed Point C

                C1x = SCFRDataTableGUI.Rows[1].Field<double>(1);
                C1y = SCFRDataTableGUI.Rows[1].Field<double>(2);
                C1z = SCFRDataTableGUI.Rows[1].Field<double>(3);


                // Initial Coordinates of Moving Point Q

                Q1x = SCFRDataTableGUI.Rows[2].Field<double>(1);
                Q1y = SCFRDataTableGUI.Rows[2].Field<double>(2);
                Q1z = SCFRDataTableGUI.Rows[2].Field<double>(3);


                //  Coordinates of Fixed Point N

                N1x = SCFRDataTableGUI.Rows[3].Field<double>(1);
                N1y = SCFRDataTableGUI.Rows[3].Field<double>(2);
                N1z = SCFRDataTableGUI.Rows[3].Field<double>(3);


                // Coordinates of Fixed Point JO

                JO1x = SCFRDataTableGUI.Rows[4].Field<double>(1);
                JO1y = SCFRDataTableGUI.Rows[4].Field<double>(2);
                JO1z = SCFRDataTableGUI.Rows[4].Field<double>(3);

                // Ride Height Reference Coordinates

                RideHeightRefx = SCFRDataTableGUI.Rows[5].Field<double>(1);
                RideHeightRefy = SCFRDataTableGUI.Rows[5].Field<double>(2);
                RideHeightRefz = SCFRDataTableGUI.Rows[5].Field<double>(3);

                // Coordinates of Fixed Point JO

                J1x = SCFRDataTableGUI.Rows[6].Field<double>(1);
                J1y = SCFRDataTableGUI.Rows[6].Field<double>(2);
                J1z = SCFRDataTableGUI.Rows[6].Field<double>(3);

                #endregion

                #region Moving Points MCPHERSON
                // Initial Coordinates of Moving Point E

                E1x = SCFRDataTableGUI.Rows[7].Field<double>(1);
                E1y = SCFRDataTableGUI.Rows[7].Field<double>(2);
                E1z = SCFRDataTableGUI.Rows[7].Field<double>(3);

                // Initial Coordinates of Moving Point P

                P1x = SCFRDataTableGUI.Rows[8].Field<double>(1);
                P1y = SCFRDataTableGUI.Rows[8].Field<double>(2);
                P1z = SCFRDataTableGUI.Rows[8].Field<double>(3);


                // Initial Coordinates of Moving Point K

                K1x = SCFRDataTableGUI.Rows[9].Field<double>(1);
                K1y = SCFRDataTableGUI.Rows[9].Field<double>(2);
                K1z = SCFRDataTableGUI.Rows[9].Field<double>(3);


                // Initial Coordinates of Moving Point M

                M1x = SCFRDataTableGUI.Rows[10].Field<double>(1);
                M1y = SCFRDataTableGUI.Rows[10].Field<double>(2);
                M1z = SCFRDataTableGUI.Rows[10].Field<double>(3);


                //  Coordinates of Moving Contact Patch Point W

                W1x = SCFRDataTableGUI.Rows[11].Field<double>(1);
                W1y = SCFRDataTableGUI.Rows[11].Field<double>(2);
                W1z = SCFRDataTableGUI.Rows[11].Field<double>(3);
                #endregion

                #endregion
            }
            #endregion

            #endregion
        }
        #endregion

        #region Function to display the coordinates of the selected SCFR item
        public static void DisplaySCFRItem(SuspensionCoordinatesFrontRight _scfr)
        {
            r1.gridControl2.DataSource = _scfr.SCFRDataTable;

            #region Delete
            //r1.A1xFR.Text = Convert.ToString(_scfr.A1x);
            //r1.A1yFR.Text = Convert.ToString(_scfr.A1y);
            //r1.A1zFR.Text = Convert.ToString(_scfr.A1z);

            //r1.B1xFR.Text = Convert.ToString(_scfr.B1x);
            //r1.B1yFR.Text = Convert.ToString(_scfr.B1y);
            //r1.B1zFR.Text = Convert.ToString(_scfr.B1z);

            //r1.C1xFR.Text = Convert.ToString(_scfr.C1x);
            //r1.C1yFR.Text = Convert.ToString(_scfr.C1y);
            //r1.C1zFR.Text = Convert.ToString(_scfr.C1z);

            //r1.D1xFR.Text = Convert.ToString(_scfr.D1x);
            //r1.D1yFR.Text = Convert.ToString(_scfr.D1y);
            //r1.D1zFR.Text = Convert.ToString(_scfr.D1z);

            //r1.E1xFR.Text = Convert.ToString(_scfr.E1x);
            //r1.E1yFR.Text = Convert.ToString(_scfr.E1y);
            //r1.E1zFR.Text = Convert.ToString(_scfr.E1z);

            //r1.F1xFR.Text = Convert.ToString(_scfr.F1x);
            //r1.F1yFR.Text = Convert.ToString(_scfr.F1y);
            //r1.F1zFR.Text = Convert.ToString(_scfr.F1z);

            //r1.G1xFR.Text = Convert.ToString(_scfr.G1x);
            //r1.G1yFR.Text = Convert.ToString(_scfr.G1y);
            //r1.G1zFR.Text = Convert.ToString(_scfr.G1z);

            //r1.H1xFR.Text = Convert.ToString(_scfr.H1x);
            //r1.H1yFR.Text = Convert.ToString(_scfr.H1y);
            //r1.H1zFR.Text = Convert.ToString(_scfr.H1z);

            //r1.I1xFR.Text = Convert.ToString(_scfr.I1x);
            //r1.I1yFR.Text = Convert.ToString(_scfr.I1y);
            //r1.I1zFR.Text = Convert.ToString(_scfr.I1z);

            //r1.J1xFR.Text = Convert.ToString(_scfr.J1x);
            //r1.J1yFR.Text = Convert.ToString(_scfr.J1y);
            //r1.J1zFR.Text = Convert.ToString(_scfr.J1z);

            //r1.JO1xFR.Text = Convert.ToString(_scfr.JO1x);
            //r1.JO1yFR.Text = Convert.ToString(_scfr.JO1y);
            //r1.JO1zFR.Text = Convert.ToString(_scfr.JO1z);

            //r1.K1xFR.Text = Convert.ToString(_scfr.K1x);
            //r1.K1yFR.Text = Convert.ToString(_scfr.K1y);
            //r1.K1zFR.Text = Convert.ToString(_scfr.K1z);

            //r1.M1xFR.Text = Convert.ToString(_scfr.M1x);
            //r1.M1yFR.Text = Convert.ToString(_scfr.M1y);
            //r1.M1zFR.Text = Convert.ToString(_scfr.M1z);

            //r1.N1xFR.Text = Convert.ToString(_scfr.N1x);
            //r1.N1yFR.Text = Convert.ToString(_scfr.N1y);
            //r1.N1zFR.Text = Convert.ToString(_scfr.N1z);

            //r1.O1xFR.Text = Convert.ToString(_scfr.O1x);
            //r1.O1yFR.Text = Convert.ToString(_scfr.O1y);
            //r1.O1zFR.Text = Convert.ToString(_scfr.O1z);

            //r1.P1xFR.Text = Convert.ToString(_scfr.P1x);
            //r1.P1yFR.Text = Convert.ToString(_scfr.P1y);
            //r1.P1zFR.Text = Convert.ToString(_scfr.P1z);

            //r1.Q1xFR.Text = Convert.ToString(_scfr.Q1x);
            //r1.Q1yFR.Text = Convert.ToString(_scfr.Q1y);
            //r1.Q1zFR.Text = Convert.ToString(_scfr.Q1z);

            //r1.R1xFR.Text = Convert.ToString(_scfr.R1x);
            //r1.R1yFR.Text = Convert.ToString(_scfr.R1y);
            //r1.R1zFR.Text = Convert.ToString(_scfr.R1z);

            //r1.W1xFR.Text = Convert.ToString(_scfr.W1x);
            //r1.W1yFR.Text = Convert.ToString(_scfr.W1y);
            //r1.W1zFR.Text = Convert.ToString(_scfr.W1z);

            //r1.RideHeightRefFRx.Text = Convert.ToString(_scfr.RideHeightRefx);
            //r1.RideHeightRefFRy.Text = Convert.ToString(_scfr.RideHeightRefy);
            //r1.RideHeightRefFRz.Text = Convert.ToString(_scfr.RideHeightRefz); 
            #endregion
        }
        #endregion

        #region De-serialization of the SCFR Object's data
        public SuspensionCoordinatesFrontRightGUI(SerializationInfo info, StreamingContext context)
        {
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
            FrontSymmetryGUI = (bool)info.GetValue("FrontSymmetry", typeof(bool));
            DoubleWishboneIdentifierFront = (int)info.GetValue("DoubleWishbone_Identifier_Front", typeof(int));
            McPhersonIdentifierFront = (int)info.GetValue("McPherson_Identifier_Front", typeof(int));
            PushrodIdentifierFront = (int)info.GetValue("Pushrod_Identifier_Front", typeof(int));
            PullrodIdentifierFront = (int)info.GetValue("Pullrod_Identifier_Front", typeof(int));
            UARBIdentifierFront = (int)info.GetValue("UARB_Identifier_Front", typeof(int));
            TARBIdentifierFront = (int)info.GetValue("TARB_Identifier_Front", typeof(int));
            #endregion

            #region De-serialization of the Data Table
            SCFRDataTableGUI = (DataTable)info.GetValue("SCFRDataTable", typeof(DataTable));
            #endregion
        } 
        #endregion

        #region Serialization of the SCFR Object's data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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
            info.AddValue("FrontSymmetry", FrontSymmetryGUI);
            info.AddValue("DoubleWishbone_Identifier_Front", DoubleWishboneIdentifierFront);
            info.AddValue("McPherson_Identifier_Front", McPhersonIdentifierFront);
            info.AddValue("Pushrod_Identifier_Front", PushrodIdentifierFront);
            info.AddValue("Pullrod_Identifier_Front", PullrodIdentifierFront);
            info.AddValue("UARB_Identifier_Front", UARBIdentifierFront);
            info.AddValue("TARB_Identifier_Front", TARBIdentifierFront);
            #endregion

            #region Serialization of the Data Table 
            info.AddValue("SCFRDataTable", SCFRDataTableGUI);
            #endregion
        } 
        #endregion
    }
}

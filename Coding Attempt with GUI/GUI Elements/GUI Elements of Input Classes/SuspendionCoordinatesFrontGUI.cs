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
    /// This it the GUI class of the Front Left SUspension
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is made serializable so that the latest information regarding the Coordinates and Suspension Type can be saved and loaded
    /// It is used to initialize the Front Left Suspension Class Object.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesFrontGUI : SuspensionCoordinatesMasterGUI, ISerializable
    {
        #region SCFL Banded Grid View
        public CustomBandedGridView bandedGridView_SCFLGUI = new CustomBandedGridView(); 
        #endregion

        #region SCFL Data Table
        public DataTable SCFLDataTableGUI { get; set; }
        #endregion

        public CAD CADFront;

        public CustomXtraTabPage TabPage_FrontCAD;

        static Kinematics_Software_New r1;

        #region Constructor
        public SuspensionCoordinatesFrontGUI()
        {
            #region Initializing the SCFL Data Table
            SCFLDataTableGUI = new DataTable();

            SCFLDataTableGUI.TableName = "Front Left Suspension Coordinates";

            SCFLDataTableGUI.Columns.Add("Suspension Point", typeof(String));
            SCFLDataTableGUI.Columns[0].ReadOnly = true;

            SCFLDataTableGUI.Columns.Add("X (mm)", typeof(double));

            SCFLDataTableGUI.Columns.Add("Y (mm)", typeof(double));

            SCFLDataTableGUI.Columns.Add("Z (mm)", typeof(double));
            #endregion
        } 
        #endregion

        #region Method to obtain the identifiers from the form which will establish the type of suspension
        public void FrontSuspensionTypeGUI(Kinematics_Software_New r1)
        {
            #region Determining the Suspension Type for the GUI Class using the User Interface Object
            SuspensionMotionExists = r1.MotionExists;

            #region Initialization of Coordinates of Vehicle Origin with respect to User Coordinates System for Input and Output
            ///<remarks>
            ///This code is added here instead of the EditFrontLeftCoordinatesGUI method so that the InputOrigin data of each suspension objbect is unchanged once created. If this code is added below, then each time the suspension is modified, the InputOrigin Data will 
            ///also be modified and this is not safe. 
            /// </remarks>
            try
            {
                _InputOriginX = Convert.ToDouble(r1.InputOriginX.Text);
                _InputOriginY = Convert.ToDouble(r1.InputOriginY.Text);
                _InputOriginZ = Convert.ToDouble(r1.InputOriginZ.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input Origin Entered");
                r1.InputOriginX.BackColor = Color.IndianRed;
                r1.InputOriginY.BackColor = Color.IndianRed;
                r1.InputOriginZ.BackColor = Color.IndianRed;

                r1.InputOriginX.Text = "0";
                if (!r1.MotionExists)
                {
                    r1.InputOriginY.Text = "1033";
                }
                else r1.InputOriginY.Text = "0";
                r1.InputOriginZ.Text = "0";
            }

            #endregion

            FrontSymmetryGUI = r1.FrontSymmetry;

            DoubleWishboneIdentifierFront = r1.DoubleWishboneFront_VehicleGUI;
            McPhersonIdentifierFront = r1.McPhersonFront_VehicleGUI;

            PushrodIdentifierFront = r1.PushrodFront_VehicleGUI;
            PullrodIdentifierFront = r1.PullrodFront_VehicleGUI;

            UARBIdentifierFront = r1.UARBFront_VehicleGUI;
            TARBIdentifierFront = r1.TARBFront_VehicleGUI;

            NoOfCouplings = r1.NoOfCouplings_VehicleGUI;

            #endregion
        }
        #endregion

        #region Method to edit the Front Left Suspension GUI
        public void EditFrontLeftCoordinatesGUI(Kinematics_Software_New _r1, SuspensionCoordinatesFrontGUI _scflGUI, int susIndex)
        {
            r1 = _r1;
            int indexRow = 0;
            //int susIndex = r1.navBarGroupSuspensionFL.SelectedLinkIndex;
            int noOfCouplings;
            if (SuspensionCoordinatesFront.Assy_List_SCFL.Count == r1.scflGUI.Count)
            {
                noOfCouplings = SuspensionCoordinatesFront.Assy_List_SCFL[susIndex].NoOfCouplings;
            }
            else noOfCouplings = r1.NoOfCouplings_VehicleGUI;


            #region Editing the Front Left Suspension Coordinates GUI Class using its own Data Table which is modified through the User Interface's GridControl

            #region Editing the Coordinates if the Suspension Type is Double Wishbone
            if (_scflGUI.DoubleWishboneIdentifierFront == 1)
            {
                #region DOUBLE WISHBONE

                #region Fixed Points DOUBLE WISHBONE
                //  Coordinates of Fixed Point D
                D1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                D1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                D1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Fixed Point C

                C1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                C1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                C1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Fixed Point A

                A1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                A1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                A1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Fixed Point B

                B1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                B1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                B1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point I

                I1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                I1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                I1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point Q

                Q1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                Q1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                Q1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Fixed Point N
                
                N1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                N1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                N1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Coordinates of Fixed Point Pin1x
                Pin1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                Pin1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                Pin1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                if (noOfCouplings == 2)
                {
                    //Coordinates of Fixed Point UV2
                    UV2x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                    UV2y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                    UV2z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                    indexRow++; 
                }

                //Coordinates of Fixed Point UV1
                UV1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                UV1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                UV1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //Coordinates of Fixed Point STC1
                STC1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                STC1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                STC1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Coordinates of Fixed Point JO

                JO1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                JO1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                JO1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Ride Height Reference Points

                RideHeightRefx = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                RideHeightRefy = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                RideHeightRefz = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //if (_scflGUI.TARBIdentifierFront == 1)
                //{
                //    // Initial Coordinates of Fixed Point R  (Only active when the it is T ARB)

                //    R1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                //    R1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                //    R1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                //    indexRow++;
                //}

                #endregion

                #region Moving Points DOUBLE WISHBONE
                // Initial Coordinates of Moving Point J

                J1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                J1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                J1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point H

                H1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                H1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                H1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point O

                O1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                O1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                O1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point G

                G1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                G1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                G1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point F

                F1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                F1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                F1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point E

                E1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                E1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                E1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point P

                P1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                P1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                P1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point K

                K1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                K1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                K1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point M

                M1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                M1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                M1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Moving Contact Patch Point W

                W1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                W1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                W1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                if (_scflGUI.TARBIdentifierFront == 1)
                {
                    // Initial Coordinates of Fixed Point R  (Only active when the it is T ARB)

                    R1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                    R1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                    R1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                    indexRow++;
                }

                #endregion

                #endregion
            } 
            #endregion

            #region Editing the Coordinates if the Suspension Type is McPherson
            if (_scflGUI.McPhersonIdentifierFront == 1)
            {
                #region MCPHERSON

                #region Fixed Points MCPHERSON
                //  Coordinates of Fixed Point D

                D1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                D1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                D1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Fixed Point C

                C1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                C1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                C1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point Q

                Q1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                Q1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                Q1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Fixed Point N

                N1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                N1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                N1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Coordinates of Fixed Point Pin1x
                Pin1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                Pin1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                Pin1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                if (noOfCouplings == 2)
                {
                    //Coordinates of Fixed Point UV2
                    UV2x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                    UV2y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                    UV2z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                    indexRow++; 
                }

                //Coordinates of Fixed Point UV1
                UV1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                UV1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                UV1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //Coordinates of Fixed Point STC1
                STC1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                STC1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                STC1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Coordinates of Fixed Point JO

                JO1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                JO1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                JO1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Ride Height Reference Coordinates

                RideHeightRefx = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                RideHeightRefy = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                RideHeightRefz = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Coordinates of Fixed Point JO

                J1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                J1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                J1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                #endregion

                #region Moving Points MCPHERSON
                // Initial Coordinates of Moving Point E

                E1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                E1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                E1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point P

                P1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                P1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                P1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point K

                K1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                K1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                K1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                // Initial Coordinates of Moving Point M

                M1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                M1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                M1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;

                //  Coordinates of Moving Contact Patch Point W

                W1x = SCFLDataTableGUI.Rows[indexRow].Field<double>(1);
                W1y = SCFLDataTableGUI.Rows[indexRow].Field<double>(2);
                W1z = SCFLDataTableGUI.Rows[indexRow].Field<double>(3);
                indexRow++;
                #endregion

                #endregion
            } 
            #endregion

            #endregion

        }
        #endregion

        #region Function to display the coordinates of the selected SCFL item
        public static void DisplaySCFLItem(SuspensionCoordinatesFront _scfl)
        {
            r1.gridControl2.DataSource = _scfl.SCFLDataTable;

            #region Delete
            //r1.A1xFL.Text = Convert.ToString(_scfl.A1x);
            //r1.A1yFL.Text = Convert.ToString(_scfl.A1y);
            //r1.A1zFL.Text = Convert.ToString(_scfl.A1z);

            //r1.B1xFL.Text = Convert.ToString(_scfl.B1x);
            //r1.B1yFL.Text = Convert.ToString(_scfl.B1y);
            //r1.B1zFL.Text = Convert.ToString(_scfl.B1z);

            //r1.C1xFL.Text = Convert.ToString(_scfl.C1x);
            //r1.C1yFL.Text = Convert.ToString(_scfl.C1y);
            //r1.C1zFL.Text = Convert.ToString(_scfl.C1z);

            //r1.D1xFL.Text = Convert.ToString(_scfl.D1x);
            //r1.D1yFL.Text = Convert.ToString(_scfl.D1y);
            //r1.D1zFL.Text = Convert.ToString(_scfl.D1z);

            //r1.E1xFL.Text = Convert.ToString(_scfl.E1x);
            //r1.E1yFL.Text = Convert.ToString(_scfl.E1y);
            //r1.E1zFL.Text = Convert.ToString(_scfl.E1z);

            //r1.F1xFL.Text = Convert.ToString(_scfl.F1x);
            //r1.F1yFL.Text = Convert.ToString(_scfl.F1y);
            //r1.F1zFL.Text = Convert.ToString(_scfl.F1z);

            //r1.G1xFL.Text = Convert.ToString(_scfl.G1x);
            //r1.G1yFL.Text = Convert.ToString(_scfl.G1y);
            //r1.G1zFL.Text = Convert.ToString(_scfl.G1z);

            //r1.H1xFL.Text = Convert.ToString(_scfl.H1x);
            //r1.H1yFL.Text = Convert.ToString(_scfl.H1y);
            //r1.H1zFL.Text = Convert.ToString(_scfl.H1z);

            //r1.I1xFL.Text = Convert.ToString(_scfl.I1x);
            //r1.I1yFL.Text = Convert.ToString(_scfl.I1y);
            //r1.I1zFL.Text = Convert.ToString(_scfl.I1z);

            //r1.J1xFL.Text = Convert.ToString(_scfl.J1x);
            //r1.J1yFL.Text = Convert.ToString(_scfl.J1y);
            //r1.J1zFL.Text = Convert.ToString(_scfl.J1z);

            //r1.JO1xFL.Text = Convert.ToString(_scfl.JO1x);
            //r1.JO1yFL.Text = Convert.ToString(_scfl.JO1y);
            //r1.JO1zFL.Text = Convert.ToString(_scfl.JO1z);

            //r1.K1xFL.Text = Convert.ToString(_scfl.K1x);
            //r1.K1yFL.Text = Convert.ToString(_scfl.K1y);
            //r1.K1zFL.Text = Convert.ToString(_scfl.K1z);

            //r1.M1xFL.Text = Convert.ToString(_scfl.M1x);
            //r1.M1yFL.Text = Convert.ToString(_scfl.M1y);
            //r1.M1zFL.Text = Convert.ToString(_scfl.M1z);

            //r1.N1xFL.Text = Convert.ToString(_scfl.N1x);
            //r1.N1yFL.Text = Convert.ToString(_scfl.N1y);
            //r1.N1zFL.Text = Convert.ToString(_scfl.N1z);

            //r1.O1xFL.Text = Convert.ToString(_scfl.O1x);
            //r1.O1yFL.Text = Convert.ToString(_scfl.O1y);
            //r1.O1zFL.Text = Convert.ToString(_scfl.O1z);

            //r1.P1xFL.Text = Convert.ToString(_scfl.P1x);
            //r1.P1yFL.Text = Convert.ToString(_scfl.P1y);
            //r1.P1zFL.Text = Convert.ToString(_scfl.P1z);

            //r1.Q1xFL.Text = Convert.ToString(_scfl.Q1x);
            //r1.Q1yFL.Text = Convert.ToString(_scfl.Q1y);
            //r1.Q1zFL.Text = Convert.ToString(_scfl.Q1z);

            //r1.R1xFL.Text = Convert.ToString(_scfl.R1x);
            //r1.R1yFL.Text = Convert.ToString(_scfl.R1y);
            //r1.R1zFL.Text = Convert.ToString(_scfl.R1z);

            //r1.W1xFL.Text = Convert.ToString(_scfl.W1x);
            //r1.W1yFL.Text = Convert.ToString(_scfl.W1y);
            //r1.W1zFL.Text = Convert.ToString(_scfl.W1z);

            //r1.RideHeightRefFLx.Text = Convert.ToString(_scfl.RideHeightRefx);
            //r1.RideHeightRefFLy.Text = Convert.ToString(_scfl.RideHeightRefy);
            //r1.RideHeightRefFLz.Text = Convert.ToString(_scfl.RideHeightRefz); 
            #endregion

        }
        #endregion

        #region Method to Create the CAD of the Front Suspension

        #region Method to Initialize the Viewport and the Usercontrol
        public void FrontCADPreProcessor(SuspensionCoordinatesFrontGUI _scflGUI, int Index, bool IsRecreated)
        {
            ///<summary>
            ///This method initializes the CAD user control and viewport. 
            ///This method is called only during the creation of a Suspension Item 
            ///</summary>
            try
            {
                if (!IsRecreated)
                {
                    _scflGUI.TabPage_FrontCAD = CustomXtraTabPage.CreateNewTabPage_ForInputs("Front Suspension " , SuspensionCoordinatesFront.Assy_List_SCFL[Index].SCFL_ID); 
                }

                _scflGUI.CADFront = new CAD();
                _scflGUI.TabPage_FrontCAD.Controls.Add(_scflGUI.CADFront);
                Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, _scflGUI.TabPage_FrontCAD);
                _scflGUI.CADFront.Dock = DockStyle.Fill;
                CreateFrontCAD(_scflGUI.CADFront,_scflGUI, SuspensionCoordinatesFront.Assy_List_SCFL[Index], SuspensionCoordinatesFrontRight.Assy_List_SCFR[Index] );
                _scflGUI.CADFront.SetupViewPort();
                _scflGUI.CADFront.Visible = true;
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPage = _scflGUI.TabPage_FrontCAD;
            }
            catch (Exception)
            {

                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Suspension or SuspensionGUI item being present, then the software won't crash
            }
        }
        #endregion

        #region Method to create (or edit) the Suspension CAD
        public void CreateFrontCAD(CAD _susCADFront,SuspensionCoordinatesFrontGUI _scflGUI, /*int index,*/ SuspensionCoordinatesFront _scFL, SuspensionCoordinatesFrontRight _scFR)
        {            
            ///<summary>
            ///This method is called during editing of a Suspension Item 
            ///</summary>
            try
            {
                //_scflGUI.CADFront.InitializeEntities();
                _susCADFront.ClearViewPort(false, false, null);
                _susCADFront.InitializeLayers();
                _susCADFront.SuspensionPlotterInvoker(_scFL, 1, null, true, true, null, 0, 0, 0);
                _susCADFront.SuspensionPlotterInvoker(_scFR, 2, null, true, true, null, 0, 0, 0);
                _susCADFront.ARBConnector(_susCADFront.CoordinatesFL.InboardPickUp, _susCADFront.CoordinatesFR.InboardPickUp);
                _susCADFront.SteeringCSystemPlotter(_scFL, _scFR, _susCADFront.CoordinatesFL.InboardPickUp, _susCADFront.CoordinatesFR.InboardPickUp);
                _susCADFront.RefreshViewPort();
            }
            catch (Exception E )
            {
                string error = E.Message;
                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Suspension or SuspensionGUI item being present, then the software won't crash
            }
        } 
        #endregion

        #endregion

        #region Deserialization of the SCFL GUI object's data
        public SuspensionCoordinatesFrontGUI(SerializationInfo info, StreamingContext context)
        {
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
            FrontSymmetryGUI = (bool)info.GetValue("FrontSymmetry", typeof(bool));
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
            _InputOriginX = (double)info.GetValue("_InputOriginX", typeof(double));
            _InputOriginY = (double)info.GetValue("_InputOriginY", typeof(double));
            _InputOriginZ = (double)info.GetValue("_InputOriginZ", typeof(double)); 
            #endregion

            #region De-serialization of the SCFLGUI DataTable
            SCFLDataTableGUI = (DataTable)info.GetValue("SCFLDataTable", typeof(DataTable));
            #endregion

            #region De-serialization of the TabPage
            TabPage_FrontCAD = (CustomXtraTabPage)info.GetValue("FrontCADTabPage", typeof(CustomXtraTabPage)); 
            #endregion

        } 
        #endregion

        #region Serialization of the SCFL GUI object's data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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
            info.AddValue("FrontSymmetry", FrontSymmetryGUI);
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
            info.AddValue("_InputOriginX", _InputOriginX);
            info.AddValue("_InputOriginY", _InputOriginY);
            info.AddValue("_InputOriginZ", _InputOriginZ); 
            #endregion

            #region Serialization of the SCFLGUI DataTable
            info.AddValue("SCFLDataTable", SCFLDataTableGUI);

            #endregion

            #region Serialization of TabPage
            info.AddValue("FrontCADTabPage", TabPage_FrontCAD); 
            #endregion

        } 
        #endregion

        
    }
}

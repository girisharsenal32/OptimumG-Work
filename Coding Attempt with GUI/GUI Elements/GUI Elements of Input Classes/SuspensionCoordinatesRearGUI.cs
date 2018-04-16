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
    /// This it the GUI class of the Rear Left SUspension
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is made serializable so that the latest information regarding the Coordinates and Suspension Type can be saved and loaded
    /// It is used to initialize the Rear Left Suspension Class Object.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesRearGUI : SuspensionCoordinatesMasterGUI,ISerializable
    {
        #region SCRL Banded Grid View
        public CustomBandedGridView bandedGridView_SCRLGUI = new CustomBandedGridView();
        #endregion

        #region SCRL Data Table 
        public DataTable SCRLDataTableGUI { get; set; }
        #endregion

        public CAD CADRear;

        public CustomXtraTabPage TabPage_RearCAD;

        static Kinematics_Software_New r1;

        #region Constructor
        public SuspensionCoordinatesRearGUI()
        {
            #region Initializing the SCRL Data Table
            SCRLDataTableGUI = new DataTable();

            SCRLDataTableGUI.TableName = "Rear Left Suspension Coordinates";

            SCRLDataTableGUI.Columns.Add("Suspension Point", typeof(String));
            SCRLDataTableGUI.Columns[0].ReadOnly = true;

            SCRLDataTableGUI.Columns.Add("X (mm)", typeof(double));

            SCRLDataTableGUI.Columns.Add("Y (mm)", typeof(double));

            SCRLDataTableGUI.Columns.Add("Z (mm)", typeof(double));
            #endregion

            #region Initialization of the Rear Left Suspension Coordinates GUI Class using User Interface Object

            //#region Fixed Points REAR LEFT Initialization - Double Wishbone & McPherson
            ////  Coordinates of Fixed Point A
            //try
            //{
            //    A1x = (Convert.ToDouble(r1.A1xRL.Text));
            //    A1y = (Convert.ToDouble(r1.A1yRL.Text));
            //    A1z = (Convert.ToDouble(r1.A1zRL.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Upper Front Chassis Coordinates Entered");
            //    r1.A1xRL.BackColor = Color.IndianRed;
            //    r1.A1yRL.BackColor = Color.IndianRed;
            //    r1.A1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.UpperFrontChassis(r1);

            //}

            ////  Coordinates of Fixed Point B
            //try
            //{
            //    B1x = (Convert.ToDouble(r1.B1xRL.Text));
            //    B1y = (Convert.ToDouble(r1.B1yRL.Text));
            //    B1z = (Convert.ToDouble(r1.B1zRL.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Upper Rear Chassis Coordinates Entered");
            //    r1.B1xRL.BackColor = Color.IndianRed;
            //    r1.B1yRL.BackColor = Color.IndianRed;
            //    r1.B1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.UpperRearChassis(r1);
            //}

            ////  Coordinates of Fixed Point C
            //try
            //{
            //    C1x = (Convert.ToDouble(r1.C1xRL.Text));
            //    C1y = (Convert.ToDouble(r1.C1yRL.Text));
            //    C1z = (Convert.ToDouble(r1.C1zRL.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Lower Rear Chassis Coordinates Entered");
            //    r1.C1xRL.BackColor = Color.IndianRed;
            //    r1.C1yRL.BackColor = Color.IndianRed;
            //    r1.C1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.LowerRearChassis(r1);
            //}

            ////  Coordinates of Fixed Point D
            //try
            //{
            //    D1x = (Convert.ToDouble(r1.D1xRL.Text));
            //    D1y = (Convert.ToDouble(r1.D1yRL.Text));
            //    D1z = (Convert.ToDouble(r1.D1zRL.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Lower Front Chassis Coordinates Entered");
            //    r1.D1xRL.BackColor = Color.IndianRed;
            //    r1.D1yRL.BackColor = Color.IndianRed;
            //    r1.D1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.LowerFrontChassis(r1);
            //}

            //// Initial Coordinates of Moving Point I
            //try
            //{
            //    I1x = (Convert.ToDouble(r1.I1xRL.Text));
            //    I1y = (Convert.ToDouble(r1.I1yRL.Text));
            //    I1z = (Convert.ToDouble(r1.I1zRL.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Bell Crank Pivot Coordinates Entered");
            //    r1.I1xRL.BackColor = Color.IndianRed;
            //    r1.I1yRL.BackColor = Color.IndianRed;
            //    r1.I1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.BellCrankPivot(r1);
            //}

            //// Initial Coordinates of Moving Point Jo
            //try
            //{
            //    JO1x = (Convert.ToDouble(r1.JO1xRL.Text));
            //    JO1y = (Convert.ToDouble(r1.JO1yRL.Text));
            //    JO1z = (Convert.ToDouble(r1.JO1zRL.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Upper Damper Shock Mount Coordinates Entered");
            //    r1.JO1xRL.BackColor = Color.IndianRed;
            //    r1.JO1yRL.BackColor = Color.IndianRed;
            //    r1.JO1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.DamperShockMount(r1);
            //}

            //// Initial Coordinates of Fixed (For now when there is no steering) Point N
            //try
            //{
            //    N1x = (Convert.ToDouble(r1.N1xRL.Text));
            //    N1y = (Convert.ToDouble(r1.N1yRL.Text));
            //    N1z = (Convert.ToDouble(r1.N1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Steering Link Chassis Coordinates Entered");
            //    r1.N1xRL.BackColor = Color.IndianRed;
            //    r1.N1yRL.BackColor = Color.IndianRed;
            //    r1.N1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.SteeringLinkChassis(r1);
            //}

            ////  Coordinates of Fixed Point Q
            //try
            //{
            //    Q1x = (Convert.ToDouble(r1.Q1xRL.Text));
            //    Q1y = (Convert.ToDouble(r1.Q1yRL.Text));
            //    Q1z = (Convert.ToDouble(r1.Q1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid ARB Chassis Coordinates Entered");
            //    r1.Q1xRL.BackColor = Color.IndianRed;
            //    r1.Q1yRL.BackColor = Color.IndianRed;
            //    r1.Q1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.ARBChassis(r1);
            //}

            //// Coordinates of Fixed Point R
            //try
            //{
            //    R1x = (Convert.ToDouble(r1.R1xRL.Text));
            //    R1y = (Convert.ToDouble(r1.R1yRL.Text));
            //    R1z = (Convert.ToDouble(r1.R1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Torsion Bar Bottom Coordinates Entered");
            //    r1.R1xRL.BackColor = Color.IndianRed;
            //    r1.R1yRL.BackColor = Color.IndianRed;
            //    r1.R1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.TorsionBarBottom(r1);
            //}

            //#endregion

            //#region Moving Points REAR LEFT Initialization - Double Wishbone & McPherson
            //// Initial Coordinates of Moving Point J
            //try
            //{
            //    J1x = (Convert.ToDouble(r1.J1xRL.Text));
            //    J1y = (Convert.ToDouble(r1.J1yRL.Text));
            //    J1z = (Convert.ToDouble(r1.J1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Damper Bell Crank Coordinates Entered");
            //    r1.J1xRL.BackColor = Color.IndianRed;
            //    r1.J1yRL.BackColor = Color.IndianRed;
            //    r1.J1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.DamperBellCrank(r1);
            //}

            //// Initial Coordinates of Moving Point H
            //try
            //{
            //    H1x = (Convert.ToDouble(r1.H1xRL.Text));
            //    H1y = (Convert.ToDouble(r1.H1yRL.Text));
            //    H1z = (Convert.ToDouble(r1.H1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Push/Pull Bell Crank Coordinates Entered");
            //    r1.H1xRL.BackColor = Color.IndianRed;
            //    r1.H1yRL.BackColor = Color.IndianRed;
            //    r1.H1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.PushRullBellCrank(r1);
            //}

            //// Initial Coordinates of Moving Point G
            //try
            //{
            //    G1x = (Convert.ToDouble(r1.G1xRL.Text));
            //    G1y = (Convert.ToDouble(r1.G1yRL.Text));
            //    G1z = (Convert.ToDouble(r1.G1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Push Pull Upright Coordinates Entered");
            //    r1.G1xRL.BackColor = Color.IndianRed;
            //    r1.G1yRL.BackColor = Color.IndianRed;
            //    r1.G1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.PushPullUpright(r1);
            //}

            //// Initial Coordinates of Moving Point F
            //try
            //{
            //    F1x = (Convert.ToDouble(r1.F1xRL.Text));
            //    F1y = (Convert.ToDouble(r1.F1yRL.Text));
            //    F1z = (Convert.ToDouble(r1.F1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Upper Ball Joint Coordinates Entered");
            //    r1.F1xRL.BackColor = Color.IndianRed;
            //    r1.F1yRL.BackColor = Color.IndianRed;
            //    r1.F1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.UBJ(r1);
            //}

            //// Initial Coordinates of Moving Point E
            //try
            //{
            //    E1x = (Convert.ToDouble(r1.E1xRL.Text));
            //    E1y = (Convert.ToDouble(r1.E1yRL.Text));
            //    E1z = (Convert.ToDouble(r1.E1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Lower Ball Joint Coordinates Entered");
            //    r1.E1xRL.BackColor = Color.IndianRed;
            //    r1.E1yRL.BackColor = Color.IndianRed;
            //    r1.E1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.LBJ(r1);
            //}

            //// Initial Coordinates of Moving Point K
            //try
            //{
            //    K1x = (Convert.ToDouble(r1.K1xRL.Text)); //IN THE HELPFILE CLEARLY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            //    K1y = (Convert.ToDouble(r1.K1yRL.Text));//PATCH CENTRE - 1/2 TIRE WIDTH
            //    K1z = (Convert.ToDouble(r1.K1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Wheel Centre Coordinates Entered");
            //    r1.K1xRL.BackColor = Color.IndianRed;
            //    r1.K1yRL.BackColor = Color.IndianRed;
            //    r1.K1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.WheelCentre(r1);
            //}

            //// Initial Coordinates of Moving Point M
            //try
            //{
            //    M1x = (Convert.ToDouble(r1.M1xRL.Text));
            //    M1y = (Convert.ToDouble(r1.M1yRL.Text));
            //    M1z = (Convert.ToDouble(r1.M1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Steering Linkp Upright Coordinates Entered");
            //    r1.M1xRL.BackColor = Color.IndianRed;
            //    r1.M1yRL.BackColor = Color.IndianRed;
            //    r1.M1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.SteeringLinkpUpright(r1);
            //}

            //// Initial Coordinates of Moving Point O
            //try
            //{
            //    O1x = (Convert.ToDouble(r1.O1xRL.Text));
            //    O1y = (Convert.ToDouble(r1.O1yRL.Text));
            //    O1z = (Convert.ToDouble(r1.O1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid ARB Bell Crank Coordinates Entered");
            //    r1.O1xRL.BackColor = Color.IndianRed;
            //    r1.O1yRL.BackColor = Color.IndianRed;
            //    r1.O1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.ARBBellCrank(r1);
            //}

            //// Initial Coordinates of Moving Point P
            //try
            //{
            //    P1x = (Convert.ToDouble(r1.P1xRL.Text));
            //    P1y = (Convert.ToDouble(r1.P1yRL.Text));
            //    P1z = (Convert.ToDouble(r1.P1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid ARB Lower Link Coordinates Entered");
            //    r1.P1xRL.BackColor = Color.IndianRed;
            //    r1.P1yRL.BackColor = Color.IndianRed;
            //    r1.P1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.ARBLowerLink(r1);
            //}

            ////  Coordinates of Moving Contact Patch Point W
            //try
            //{
            //    W1x = (Convert.ToDouble(r1.W1xRL.Text));
            //    W1y = (Convert.ToDouble(r1.W1yRL.Text));
            //    W1z = (Convert.ToDouble(r1.W1zRL.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Contact Patch Coordinates Entered");
            //    r1.W1xRL.BackColor = Color.IndianRed;
            //    r1.W1yRL.BackColor = Color.IndianRed;
            //    r1.W1zRL.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.ContactPatch(r1);
            //}

            ////  Ride Height Reference Points
            //try
            //{
            //    RideHeightRefx = Convert.ToDouble(r1.RideHeightRefRLx.Text);
            //    RideHeightRefy = Convert.ToDouble(r1.RideHeightRefRLy.Text);
            //    RideHeightRefz = Convert.ToDouble(r1.RideHeightRefRLz.Text);
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Ride Height Reference Coordinates Entered");
            //    r1.RideHeightRefRLx.BackColor = Color.IndianRed;
            //    r1.RideHeightRefRLy.BackColor = Color.IndianRed;
            //    r1.RideHeightRefRLz.BackColor = Color.IndianRed;
            //    Default_Values.REARLEFTSuspensionDefaultValues.RideHeightRef(r1);

            //}
            //#endregion
            #endregion
        } 
        #endregion

        #region Method to obtain the identifiers from the form which will establish the type of suspension
        public void RearSuspensionTypeGUI(Kinematics_Software_New _r1)
        {
            r1 = _r1;

            #region Determining the Suspension Type for the GUI Class using the User Interface Object
            RearSymmetryGUI = r1.RearSymmetry;

            DoubleWishboneIdentifierRear = r1.DoubleWishboneRear_VehicleGUI;
            McPhersonIdentifierRear = r1.McPhersonRear_VehicleGUI;

            PushrodIdentifierRear = r1.PushrodRear_VehicleGUI;
            PullrodIdentifierRear = r1.PullrodRear_VehicleGUI;

            UARBIdentifierRear = r1.UARBRear_VehicleGUI;
            TARBIdentifierRear = r1.TARBRear_VehicleGUI;
            #endregion

        } 
        #endregion

        #region Method to edit the Rear Left Suspension GUI
        public void EditRearLeftCoordinatesGUI(Kinematics_Software_New _r1,SuspensionCoordinatesRearGUI _scrlGUI)
        {
            r1 = _r1;

            #region Editing the Rear Left Suspension Coordinates GUI Class using its own Data Table which is modified through the User Interface's GridControl

            #region Editing the Coordinates if the Suspension Type is Double Wishbone
            if (_scrlGUI.DoubleWishboneIdentifierRear == 1)
            {
                #region DOUBLE WISHBONE

                #region Fixed Points DOUBLE WISHBONE
                //  Coordinates of Fixed Point D

                D1x = SCRLDataTableGUI.Rows[0].Field<double>(1);
                D1y = SCRLDataTableGUI.Rows[0].Field<double>(2);
                D1z = SCRLDataTableGUI.Rows[0].Field<double>(3);


                //  Coordinates of Fixed Point C

                C1x = SCRLDataTableGUI.Rows[1].Field<double>(1);
                C1y = SCRLDataTableGUI.Rows[1].Field<double>(2);
                C1z = SCRLDataTableGUI.Rows[1].Field<double>(3);


                //  Coordinates of Fixed Point A

                A1x = SCRLDataTableGUI.Rows[2].Field<double>(1);
                A1y = SCRLDataTableGUI.Rows[2].Field<double>(2);
                A1z = SCRLDataTableGUI.Rows[2].Field<double>(3);


                //  Coordinates of Fixed Point B

                B1x = SCRLDataTableGUI.Rows[3].Field<double>(1);
                B1y = SCRLDataTableGUI.Rows[3].Field<double>(2);
                B1z = SCRLDataTableGUI.Rows[3].Field<double>(3);


                // Initial Coordinates of Moving Point I

                I1x = SCRLDataTableGUI.Rows[4].Field<double>(1);
                I1y = SCRLDataTableGUI.Rows[4].Field<double>(2);
                I1z = SCRLDataTableGUI.Rows[4].Field<double>(3);


                // Initial Coordinates of Moving Point Q

                Q1x = SCRLDataTableGUI.Rows[5].Field<double>(1);
                Q1y = SCRLDataTableGUI.Rows[5].Field<double>(2);
                Q1z = SCRLDataTableGUI.Rows[5].Field<double>(3);


                //  Coordinates of Fixed Point N

                N1x = SCRLDataTableGUI.Rows[6].Field<double>(1);
                N1y = SCRLDataTableGUI.Rows[6].Field<double>(2);
                N1z = SCRLDataTableGUI.Rows[6].Field<double>(3);


                // Coordinates of Fixed Point JO

                JO1x = SCRLDataTableGUI.Rows[7].Field<double>(1);
                JO1y = SCRLDataTableGUI.Rows[7].Field<double>(2);
                JO1z = SCRLDataTableGUI.Rows[7].Field<double>(3);

                //  Ride Height Reference Points

                RideHeightRefx = SCRLDataTableGUI.Rows[8].Field<double>(1);
                RideHeightRefy = SCRLDataTableGUI.Rows[8].Field<double>(2);
                RideHeightRefz = SCRLDataTableGUI.Rows[8].Field<double>(3);

                if (_scrlGUI.TARBIdentifierRear == 1)
                {
                    // Initial Coordinates of Fixed Point R  (Only active when the it is T ARB)

                    R1x = SCRLDataTableGUI.Rows[19].Field<double>(1);
                    R1y = SCRLDataTableGUI.Rows[19].Field<double>(2);
                    R1z = SCRLDataTableGUI.Rows[19].Field<double>(3);
                }

                #endregion

                #region Moving Points DOUBLE WISHBONE
                // Initial Coordinates of Moving Point J

                J1x = SCRLDataTableGUI.Rows[9].Field<double>(1);
                J1y = SCRLDataTableGUI.Rows[9].Field<double>(2);
                J1z = SCRLDataTableGUI.Rows[9].Field<double>(3);


                // Initial Coordinates of Moving Point H

                H1x = SCRLDataTableGUI.Rows[10].Field<double>(1);
                H1y = SCRLDataTableGUI.Rows[10].Field<double>(2);
                H1z = SCRLDataTableGUI.Rows[10].Field<double>(3);


                // Initial Coordinates of Moving Point O

                O1x = SCRLDataTableGUI.Rows[11].Field<double>(1);
                O1y = SCRLDataTableGUI.Rows[11].Field<double>(2);
                O1z = SCRLDataTableGUI.Rows[11].Field<double>(3);


                // Initial Coordinates of Moving Point G

                G1x = SCRLDataTableGUI.Rows[12].Field<double>(1);
                G1y = SCRLDataTableGUI.Rows[12].Field<double>(2);
                G1z = SCRLDataTableGUI.Rows[12].Field<double>(3);


                // Initial Coordinates of Moving Point F

                F1x = SCRLDataTableGUI.Rows[13].Field<double>(1);
                F1y = SCRLDataTableGUI.Rows[13].Field<double>(2);
                F1z = SCRLDataTableGUI.Rows[13].Field<double>(3);


                // Initial Coordinates of Moving Point E

                E1x = SCRLDataTableGUI.Rows[14].Field<double>(1);
                E1y = SCRLDataTableGUI.Rows[14].Field<double>(2);
                E1z = SCRLDataTableGUI.Rows[14].Field<double>(3);


                // Initial Coordinates of Moving Point P

                P1x = SCRLDataTableGUI.Rows[15].Field<double>(1);
                P1y = SCRLDataTableGUI.Rows[15].Field<double>(2);
                P1z = SCRLDataTableGUI.Rows[15].Field<double>(3);


                // Initial Coordinates of Moving Point K

                K1x = SCRLDataTableGUI.Rows[16].Field<double>(1);
                K1y = SCRLDataTableGUI.Rows[16].Field<double>(2);
                K1z = SCRLDataTableGUI.Rows[16].Field<double>(3);


                // Initial Coordinates of Moving Point M

                M1x = SCRLDataTableGUI.Rows[17].Field<double>(1);
                M1y = SCRLDataTableGUI.Rows[17].Field<double>(2);
                M1z = SCRLDataTableGUI.Rows[17].Field<double>(3);


                //  Coordinates of Moving Contact Patch Point W

                W1x = SCRLDataTableGUI.Rows[18].Field<double>(1);
                W1y = SCRLDataTableGUI.Rows[18].Field<double>(2);
                W1z = SCRLDataTableGUI.Rows[18].Field<double>(3);

                #endregion

                #endregion
            }
            #endregion

            #region Editing the Coordinates if the Suspension Type is McPherson
            if (_scrlGUI.McPhersonIdentifierRear == 1)
            {
                #region MCPHERSON

                #region Fixed Points MCPHERSON
                //  Coordinates of Fixed Point D

                D1x = SCRLDataTableGUI.Rows[0].Field<double>(1);
                D1y = SCRLDataTableGUI.Rows[0].Field<double>(2);
                D1z = SCRLDataTableGUI.Rows[0].Field<double>(3);


                //  Coordinates of Fixed Point C

                C1x = SCRLDataTableGUI.Rows[1].Field<double>(1);
                C1y = SCRLDataTableGUI.Rows[1].Field<double>(2);
                C1z = SCRLDataTableGUI.Rows[1].Field<double>(3);


                // Initial Coordinates of Moving Point Q

                Q1x = SCRLDataTableGUI.Rows[2].Field<double>(1);
                Q1y = SCRLDataTableGUI.Rows[2].Field<double>(2);
                Q1z = SCRLDataTableGUI.Rows[2].Field<double>(3);


                //  Coordinates of Fixed Point N

                N1x = SCRLDataTableGUI.Rows[3].Field<double>(1);
                N1y = SCRLDataTableGUI.Rows[3].Field<double>(2);
                N1z = SCRLDataTableGUI.Rows[3].Field<double>(3);


                // Coordinates of Fixed Point JO

                JO1x = SCRLDataTableGUI.Rows[4].Field<double>(1);
                JO1y = SCRLDataTableGUI.Rows[4].Field<double>(2);
                JO1z = SCRLDataTableGUI.Rows[4].Field<double>(3);

                // Ride Height Reference Coordinates

                RideHeightRefx = SCRLDataTableGUI.Rows[5].Field<double>(1);
                RideHeightRefy = SCRLDataTableGUI.Rows[5].Field<double>(2);
                RideHeightRefz = SCRLDataTableGUI.Rows[5].Field<double>(3);

                // Coordinates of Fixed Point JO

                J1x = SCRLDataTableGUI.Rows[6].Field<double>(1);
                J1y = SCRLDataTableGUI.Rows[6].Field<double>(2);
                J1z = SCRLDataTableGUI.Rows[6].Field<double>(3);

                #endregion

                #region Moving Points MCPHERSON
                // Initial Coordinates of Moving Point E

                E1x = SCRLDataTableGUI.Rows[7].Field<double>(1);
                E1y = SCRLDataTableGUI.Rows[7].Field<double>(2);
                E1z = SCRLDataTableGUI.Rows[7].Field<double>(3);

                // Initial Coordinates of Moving Point P

                P1x = SCRLDataTableGUI.Rows[8].Field<double>(1);
                P1y = SCRLDataTableGUI.Rows[8].Field<double>(2);
                P1z = SCRLDataTableGUI.Rows[8].Field<double>(3);


                // Initial Coordinates of Moving Point K

                K1x = SCRLDataTableGUI.Rows[9].Field<double>(1);
                K1y = SCRLDataTableGUI.Rows[9].Field<double>(2);
                K1z = SCRLDataTableGUI.Rows[9].Field<double>(3);


                // Initial Coordinates of Moving Point M

                M1x = SCRLDataTableGUI.Rows[10].Field<double>(1);
                M1y = SCRLDataTableGUI.Rows[10].Field<double>(2);
                M1z = SCRLDataTableGUI.Rows[10].Field<double>(3);


                //  Coordinates of Moving Contact Patch Point W

                W1x = SCRLDataTableGUI.Rows[11].Field<double>(1);
                W1y = SCRLDataTableGUI.Rows[11].Field<double>(2);
                W1z = SCRLDataTableGUI.Rows[11].Field<double>(3);
                #endregion

                #endregion
            }
            #endregion

            #endregion
        } 
        #endregion

        #region Function to display the coordinates of the selected SCRL Item
        public static void DisplaySCRLItem(SuspensionCoordinatesRear _scrl)
        {
            r1.gridControl2.DataSource = _scrl.SCRLDataTable;

            #region Delete
            //r1.A1xRL.Text = Convert.ToString(_scrl.A1x);
            //r1.A1yRL.Text = Convert.ToString(_scrl.A1y);
            //r1.A1zRL.Text = Convert.ToString(_scrl.A1z);

            //r1.B1xRL.Text = Convert.ToString(_scrl.B1x);
            //r1.B1yRL.Text = Convert.ToString(_scrl.B1y);
            //r1.B1zRL.Text = Convert.ToString(_scrl.B1z);

            //r1.C1xRL.Text = Convert.ToString(_scrl.C1x);
            //r1.C1yRL.Text = Convert.ToString(_scrl.C1y);
            //r1.C1zRL.Text = Convert.ToString(_scrl.C1z);

            //r1.D1xRL.Text = Convert.ToString(_scrl.D1x);
            //r1.D1yRL.Text = Convert.ToString(_scrl.D1y);
            //r1.D1zRL.Text = Convert.ToString(_scrl.D1z);

            //r1.E1xRL.Text = Convert.ToString(_scrl.E1x);
            //r1.E1yRL.Text = Convert.ToString(_scrl.E1y);
            //r1.E1zRL.Text = Convert.ToString(_scrl.E1z);

            //r1.F1xRL.Text = Convert.ToString(_scrl.F1x);
            //r1.F1yRL.Text = Convert.ToString(_scrl.F1y);
            //r1.F1zRL.Text = Convert.ToString(_scrl.F1z);

            //r1.G1xRL.Text = Convert.ToString(_scrl.G1x);
            //r1.G1yRL.Text = Convert.ToString(_scrl.G1y);
            //r1.G1zRL.Text = Convert.ToString(_scrl.G1z);

            //r1.H1xRL.Text = Convert.ToString(_scrl.H1x);
            //r1.H1yRL.Text = Convert.ToString(_scrl.H1y);
            //r1.H1zRL.Text = Convert.ToString(_scrl.H1z);

            //r1.I1xRL.Text = Convert.ToString(_scrl.I1x);
            //r1.I1yRL.Text = Convert.ToString(_scrl.I1y);
            //r1.I1zRL.Text = Convert.ToString(_scrl.I1z);

            //r1.J1xRL.Text = Convert.ToString(_scrl.J1x);
            //r1.J1yRL.Text = Convert.ToString(_scrl.J1y);
            //r1.J1zRL.Text = Convert.ToString(_scrl.J1z);

            //r1.JO1xRL.Text = Convert.ToString(_scrl.JO1x);
            //r1.JO1yRL.Text = Convert.ToString(_scrl.JO1y);
            //r1.JO1zRL.Text = Convert.ToString(_scrl.JO1z);

            //r1.K1xRL.Text = Convert.ToString(_scrl.K1x);
            //r1.K1yRL.Text = Convert.ToString(_scrl.K1y);
            //r1.K1zRL.Text = Convert.ToString(_scrl.K1z);

            //r1.M1xRL.Text = Convert.ToString(_scrl.M1x);
            //r1.M1yRL.Text = Convert.ToString(_scrl.M1y);
            //r1.M1zRL.Text = Convert.ToString(_scrl.M1z);

            //r1.N1xRL.Text = Convert.ToString(_scrl.N1x);
            //r1.N1yRL.Text = Convert.ToString(_scrl.N1y);
            //r1.N1zRL.Text = Convert.ToString(_scrl.N1z);

            //r1.O1xRL.Text = Convert.ToString(_scrl.O1x);
            //r1.O1yRL.Text = Convert.ToString(_scrl.O1y);
            //r1.O1zRL.Text = Convert.ToString(_scrl.O1z);

            //r1.P1xRL.Text = Convert.ToString(_scrl.P1x);
            //r1.P1yRL.Text = Convert.ToString(_scrl.P1y);
            //r1.P1zRL.Text = Convert.ToString(_scrl.P1z);

            //r1.Q1xRL.Text = Convert.ToString(_scrl.Q1x);
            //r1.Q1yRL.Text = Convert.ToString(_scrl.Q1y);
            //r1.Q1zRL.Text = Convert.ToString(_scrl.Q1z);

            //r1.R1xRL.Text = Convert.ToString(_scrl.R1x);
            //r1.R1yRL.Text = Convert.ToString(_scrl.R1y);
            //r1.R1zRL.Text = Convert.ToString(_scrl.R1z);

            //r1.W1xRL.Text = Convert.ToString(_scrl.W1x);
            //r1.W1yRL.Text = Convert.ToString(_scrl.W1y);
            //r1.W1zRL.Text = Convert.ToString(_scrl.W1z);

            //r1.RideHeightRefRLx.Text = Convert.ToString(_scrl.RideHeightRefx);
            //r1.RideHeightRefRLy.Text = Convert.ToString(_scrl.RideHeightRefy);
            //r1.RideHeightRefRLz.Text = Convert.ToString(_scrl.RideHeightRefz); 
            #endregion
        }
        #endregion

        #region Method to create the Rear Suspension CAD

        #region Method to Initialize the Viewport and the Usercontrol
        public void RearCADPreProcessor(SuspensionCoordinatesRearGUI _scrlGUI,int Index, bool IsRecreated)
        {
            try
            {
                if (!IsRecreated)
                {
                    _scrlGUI.TabPage_RearCAD = CustomXtraTabPage.CreateNewTabPage_ForInputs("Rear Suspension " , SuspensionCoordinatesRear.Assy_List_SCRL[Index].SCRL_ID); 
                }
                _scrlGUI.CADRear = new CAD();
                _scrlGUI.TabPage_RearCAD.Controls.Add(_scrlGUI.CADRear);
                Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, _scrlGUI.TabPage_RearCAD);
                _scrlGUI.CADRear.Dock = DockStyle.Fill;
                CreateRearCAD(_scrlGUI.CADRear,_scrlGUI, SuspensionCoordinatesRear.Assy_List_SCRL[Index], SuspensionCoordinatesRearRight.Assy_List_SCRR[Index]);
                _scrlGUI.CADRear.SetupViewPort();
                _scrlGUI.CADRear.Visible = true;
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPage = _scrlGUI.TabPage_RearCAD;
            }
            catch (Exception E)
            {
                string error = E.Message;
                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Suspension or SuspensionGUI item being present, then the software won't crash
            }
        }
        #endregion

        #region Method to create (or edit) the Suspension CAD
        public void CreateRearCAD(CAD _susCADRear,SuspensionCoordinatesRearGUI _scrlGUI,SuspensionCoordinatesRear _sCRL,SuspensionCoordinatesRearRight _sCRR)
        {
            try
            {
                //_scrlGUI.CADRear.InitializeEntities();
                _susCADRear.ClearViewPort(false, false, null);
                _susCADRear.InitializeLayers();
                _susCADRear.SuspensionPlotterInvoker(_sCRL, 3, null, true,true, null);
                _susCADRear.SuspensionPlotterInvoker(_sCRR, 4, null, true, true, null);
                _susCADRear.ARBConnector(_susCADRear.CoordinatesRL.InboardPickUp, _susCADRear.CoordinatesRR.InboardPickUp);
                _susCADRear.RefreshViewPort();
            }
            catch (Exception )
            {

                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Suspension or SuspensionGUI item being present, then the software won't crash
            }
        } 
        #endregion

        #endregion

        #region De-serialization of the SCRL Object's data
        public SuspensionCoordinatesRearGUI(SerializationInfo info, StreamingContext context)
        {
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
            RearSymmetryGUI = (bool)info.GetValue("RearSymmetry", typeof(bool));
            DoubleWishboneIdentifierRear = (int)info.GetValue("DoubleWishbone_Identifier_Rear", typeof(int));
            McPhersonIdentifierRear = (int)info.GetValue("McPherson_Identifier_Rear", typeof(int));
            PushrodIdentifierRear = (int)info.GetValue("Pushrod_Identifier_Rear", typeof(int));
            PullrodIdentifierRear = (int)info.GetValue("Pullrod_Identifier_Rear", typeof(int));
            UARBIdentifierRear = (int)info.GetValue("UARB_Identifier_Rear", typeof(int));
            TARBIdentifierRear = (int)info.GetValue("TARB_Identifier_Rear", typeof(int));
            #endregion

            #region De-erialization of the SCRL Data Table
            SCRLDataTableGUI = (DataTable)info.GetValue("SCRLDataTable", typeof(DataTable));
            #endregion

            #region De-serialization of the TabPage
            TabPage_RearCAD = (CustomXtraTabPage)info.GetValue("RearCADTabPage", typeof(CustomXtraTabPage)); 
            #endregion


        } 
        #endregion

        #region Serialization of the SCRL Object's data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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
            info.AddValue("RearSymmetry", RearSymmetryGUI);
            info.AddValue("DoubleWishbone_Identifier_Rear", DoubleWishboneIdentifierRear);
            info.AddValue("McPherson_Identifier_Rear", McPhersonIdentifierRear);
            info.AddValue("Pushrod_Identifier_Rear", PushrodIdentifierRear);
            info.AddValue("Pullrod_Identifier_Rear", PullrodIdentifierRear);
            info.AddValue("UARB_Identifier_Rear", UARBIdentifierRear);
            info.AddValue("TARB_Identifier_Rear", TARBIdentifierRear);
            #endregion

            #region Serialization of the SCRL Data Table
            info.AddValue("SCRLDataTable", SCRLDataTableGUI);
            #endregion

            #region Serialization of TabPage
            info.AddValue("RearCADTabPage", TabPage_RearCAD); 
            #endregion

        } 
        #endregion

        
    }
}

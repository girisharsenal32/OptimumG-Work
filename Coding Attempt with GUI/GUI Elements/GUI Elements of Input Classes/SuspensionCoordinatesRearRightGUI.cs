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
    /// This it the GUI class of the Rear Right SUspension
    /// It receives its inputs from the Form which is displayed to the user. 
    /// It is made serializable so that the latest information regarding the Coordinates and Suspension Type can be saved and loaded
    /// It is used to initialize the Rear Right Suspension Class Object.
    /// </summary>

    [Serializable()]
    public class SuspensionCoordinatesRearRightGUI : SuspensionCoordinatesMasterGUI,ISerializable
    {
        #region SCRR Banded Grid View
        public CustomBandedGridView bandedGridView_SCRRGUI = new CustomBandedGridView();
        #endregion

        #region SCRR Data Table
        public DataTable SCRRDataTableGUI { get; set; }
        #endregion

        static Kinematics_Software_New r1;

        #region Constructor
        public SuspensionCoordinatesRearRightGUI()
        {
            #region Initializing the SCRR Data Table
            SCRRDataTableGUI = new DataTable();

            SCRRDataTableGUI.TableName = "Rear Right Suspension Coordinates";

            SCRRDataTableGUI.Columns.Add("Suspension Point", typeof(String));
            SCRRDataTableGUI.Columns[0].ReadOnly = true;

            SCRRDataTableGUI.Columns.Add("X (mm)", typeof(double));

            SCRRDataTableGUI.Columns.Add("Y (mm)", typeof(double));

            SCRRDataTableGUI.Columns.Add("Z (mm)", typeof(double));
            #endregion

            #region Initialization of the Rear Right Suspension Coordinates GUI Class using User Interface Object

            //#region Fixed Points FRONT LEFT Initialization - Double Wishbone & McPherson
            ////  Coordinates of Fixed Point A
            //try
            //{
            //    A1x = (Convert.ToDouble(r1.A1xRR.Text));
            //    A1y = (Convert.ToDouble(r1.A1yRR.Text));
            //    A1z = (Convert.ToDouble(r1.A1zRR.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Upper Front Chassis Coordinates Entered");
            //    r1.A1xRR.BackColor = Color.IndianRed;
            //    r1.A1yRR.BackColor = Color.IndianRed;
            //    r1.A1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.UpperFrontChassis(r1);

            //}

            ////  Coordinates of Fixed Point B
            //try
            //{
            //    B1x = (Convert.ToDouble(r1.B1xRR.Text));
            //    B1y = (Convert.ToDouble(r1.B1yRR.Text));
            //    B1z = (Convert.ToDouble(r1.B1zRR.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Upper Rear Chassis Coordinates Entered");
            //    r1.B1xRR.BackColor = Color.IndianRed;
            //    r1.B1yRR.BackColor = Color.IndianRed;
            //    r1.B1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.UpperRearChassis(r1);
            //}

            ////  Coordinates of Fixed Point C
            //try
            //{
            //    C1x = (Convert.ToDouble(r1.C1xRR.Text));
            //    C1y = (Convert.ToDouble(r1.C1yRR.Text));
            //    C1z = (Convert.ToDouble(r1.C1zRR.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Lower Rear Chassis Coordinates Entered");
            //    r1.C1xRR.BackColor = Color.IndianRed;
            //    r1.C1yRR.BackColor = Color.IndianRed;
            //    r1.C1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.LowerRearChassis(r1);
            //}

            ////  Coordinates of Fixed Point D
            //try
            //{
            //    D1x = (Convert.ToDouble(r1.D1xRR.Text));
            //    D1y = (Convert.ToDouble(r1.D1yRR.Text));
            //    D1z = (Convert.ToDouble(r1.D1zRR.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Lower Front Chassis Coordinates Entered");
            //    r1.D1xRR.BackColor = Color.IndianRed;
            //    r1.D1yRR.BackColor = Color.IndianRed;
            //    r1.D1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.LowerFrontChassis(r1);
            //}

            //// Initial Coordinates of Moving Point I
            //try
            //{
            //    I1x = (Convert.ToDouble(r1.I1xRR.Text));
            //    I1y = (Convert.ToDouble(r1.I1yRR.Text));
            //    I1z = (Convert.ToDouble(r1.I1zRR.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Bell Crank Pivot Coordinates Entered");
            //    r1.I1xRR.BackColor = Color.IndianRed;
            //    r1.I1yRR.BackColor = Color.IndianRed;
            //    r1.I1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.BellCrankPivot(r1);
            //}

            //// Initial Coordinates of Moving Point Jo
            //try
            //{
            //    JO1x = (Convert.ToDouble(r1.JO1xRR.Text));
            //    JO1y = (Convert.ToDouble(r1.JO1yRR.Text));
            //    JO1z = (Convert.ToDouble(r1.JO1zRR.Text));
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Invalid Upper Damper Shock Mount Coordinates Entered");
            //    r1.JO1xRR.BackColor = Color.IndianRed;
            //    r1.JO1yRR.BackColor = Color.IndianRed;
            //    r1.JO1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.DamperShockMount(r1);
            //}

            //// Initial Coordinates of Fixed (For now when there is no steering) Point N
            //try
            //{
            //    N1x = (Convert.ToDouble(r1.N1xRR.Text));
            //    N1y = (Convert.ToDouble(r1.N1yRR.Text));
            //    N1z = (Convert.ToDouble(r1.N1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Steering Link Chassis Coordinates Entered");
            //    r1.N1xRR.BackColor = Color.IndianRed;
            //    r1.N1yRR.BackColor = Color.IndianRed;
            //    r1.N1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.SteeringLinkChassis(r1);
            //}

            ////  Coordinates of Fixed Point Q
            //try
            //{
            //    Q1x = (Convert.ToDouble(r1.Q1xRR.Text));
            //    Q1y = (Convert.ToDouble(r1.Q1yRR.Text));
            //    Q1z = (Convert.ToDouble(r1.Q1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid ARB Chassis Coordinates Entered");
            //    r1.Q1xRR.BackColor = Color.IndianRed;
            //    r1.Q1yRR.BackColor = Color.IndianRed;
            //    r1.Q1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.ARBChassis(r1);
            //}

            //// Coordinates of Fixed Point R
            //try
            //{
            //    R1x = (Convert.ToDouble(r1.R1xRR.Text));
            //    R1y = (Convert.ToDouble(r1.R1yRR.Text));
            //    R1z = (Convert.ToDouble(r1.R1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Torsion Bar Bottom Coordinates Entered");
            //    r1.R1xRR.BackColor = Color.IndianRed;
            //    r1.R1yRR.BackColor = Color.IndianRed;
            //    r1.R1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.TorsionBarBottom(r1);
            //}

            //#endregion

            //#region Moving Points FRONT LEFT Initialization - Double Wishbone & McPherson
            //// Initial Coordinates of Moving Point J
            //try
            //{
            //    J1x = (Convert.ToDouble(r1.J1xRR.Text));
            //    J1y = (Convert.ToDouble(r1.J1yRR.Text));
            //    J1z = (Convert.ToDouble(r1.J1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Damper Bell Crank Coordinates Entered");
            //    r1.J1xRR.BackColor = Color.IndianRed;
            //    r1.J1yRR.BackColor = Color.IndianRed;
            //    r1.J1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.DamperBellCrank(r1);
            //}

            //// Initial Coordinates of Moving Point H
            //try
            //{
            //    H1x = (Convert.ToDouble(r1.H1xRR.Text));
            //    H1y = (Convert.ToDouble(r1.H1yRR.Text));
            //    H1z = (Convert.ToDouble(r1.H1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Push/Pull Bell Crank Coordinates Entered");
            //    r1.H1xRR.BackColor = Color.IndianRed;
            //    r1.H1yRR.BackColor = Color.IndianRed;
            //    r1.H1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.PushRullBellCrank(r1);
            //}

            //// Initial Coordinates of Moving Point G
            //try
            //{
            //    G1x = (Convert.ToDouble(r1.G1xRR.Text));
            //    G1y = (Convert.ToDouble(r1.G1yRR.Text));
            //    G1z = (Convert.ToDouble(r1.G1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Push Pull Upright Coordinates Entered");
            //    r1.G1xRR.BackColor = Color.IndianRed;
            //    r1.G1yRR.BackColor = Color.IndianRed;
            //    r1.G1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.PushPullUpright(r1);
            //}

            //// Initial Coordinates of Moving Point F
            //try
            //{
            //    F1x = (Convert.ToDouble(r1.F1xRR.Text));
            //    F1y = (Convert.ToDouble(r1.F1yRR.Text));
            //    F1z = (Convert.ToDouble(r1.F1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Upper Ball Joint Coordinates Entered");
            //    r1.F1xRR.BackColor = Color.IndianRed;
            //    r1.F1yRR.BackColor = Color.IndianRed;
            //    r1.F1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.UBJ(r1);
            //}

            //// Initial Coordinates of Moving Point E
            //try
            //{
            //    E1x = (Convert.ToDouble(r1.E1xRR.Text));
            //    E1y = (Convert.ToDouble(r1.E1yRR.Text));
            //    E1z = (Convert.ToDouble(r1.E1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Lower Ball Joint Coordinates Entered");
            //    r1.E1xRR.BackColor = Color.IndianRed;
            //    r1.E1yRR.BackColor = Color.IndianRed;
            //    r1.E1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.LBJ(r1);
            //}

            //// Initial Coordinates of Moving Point K
            //try
            //{
            //    K1x = (Convert.ToDouble(r1.K1xRR.Text)); //IN THE HELPFILE CLEARRY MENTION THAT THE X COORDINATE IS TO BE INPUT AS CONTACT 
            //    K1y = (Convert.ToDouble(r1.K1yRR.Text));//PATCH CENTRE - 1/2 TIRE WIDTH
            //    K1z = (Convert.ToDouble(r1.K1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Wheel Centre Coordinates Entered");
            //    r1.K1xRR.BackColor = Color.IndianRed;
            //    r1.K1yRR.BackColor = Color.IndianRed;
            //    r1.K1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.WheelCentre(r1);
            //}

            //// Initial Coordinates of Moving Point M
            //try
            //{
            //    M1x = (Convert.ToDouble(r1.M1xRR.Text));
            //    M1y = (Convert.ToDouble(r1.M1yRR.Text));
            //    M1z = (Convert.ToDouble(r1.M1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Steering Linkp Upright Coordinates Entered");
            //    r1.M1xRR.BackColor = Color.IndianRed;
            //    r1.M1yRR.BackColor = Color.IndianRed;
            //    r1.M1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.SteeringLinkpUpright(r1);
            //}

            //// Initial Coordinates of Moving Point O
            //try
            //{
            //    O1x = (Convert.ToDouble(r1.O1xRR.Text));
            //    O1y = (Convert.ToDouble(r1.O1yRR.Text));
            //    O1z = (Convert.ToDouble(r1.O1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid ARB Bell Crank Coordinates Entered");
            //    r1.O1xRR.BackColor = Color.IndianRed;
            //    r1.O1yRR.BackColor = Color.IndianRed;
            //    r1.O1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.ARBBellCrank(r1);
            //}

            //// Initial Coordinates of Moving Point P
            //try
            //{
            //    P1x = (Convert.ToDouble(r1.P1xRR.Text));
            //    P1y = (Convert.ToDouble(r1.P1yRR.Text));
            //    P1z = (Convert.ToDouble(r1.P1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid ARB Lower Link Coordinates Entered");
            //    r1.P1xRR.BackColor = Color.IndianRed;
            //    r1.P1yRR.BackColor = Color.IndianRed;
            //    r1.P1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.ARBLowerLink(r1);
            //}

            ////  Coordinates of Moving Contact Patch Point W
            //try
            //{
            //    W1x = (Convert.ToDouble(r1.W1xRR.Text));
            //    W1y = (Convert.ToDouble(r1.W1yRR.Text));
            //    W1z = (Convert.ToDouble(r1.W1zRR.Text));
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Contact Patch Coordinates Entered");
            //    r1.W1xRR.BackColor = Color.IndianRed;
            //    r1.W1yRR.BackColor = Color.IndianRed;
            //    r1.W1zRR.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.ContactPatch(r1);
            //}

            ////  Ride Height Reference Points
            //try
            //{
            //    RideHeightRefx = Convert.ToDouble(r1.RideHeightRefRRx.Text);
            //    RideHeightRefy = Convert.ToDouble(r1.RideHeightRefRRy.Text);
            //    RideHeightRefz = Convert.ToDouble(r1.RideHeightRefRRz.Text);
            //}
            //catch (Exception)
            //{


            //    MessageBox.Show("Invalid Ride Height Reference Coordinates Entered");
            //    r1.RideHeightRefRRx.BackColor = Color.IndianRed;
            //    r1.RideHeightRefRRy.BackColor = Color.IndianRed;
            //    r1.RideHeightRefRRz.BackColor = Color.IndianRed;
            //    Default_Values.REARRIGHTSuspensionDefaultValues.RideHeightRef(r1);

            //}
            //#endregion

            #endregion
        }
        #endregion

        #region Method to obtain the identifiers from the form which will establish the type of suspension
        public void RearSuspensionTypeGUI(Kinematics_Software_New r1)
        {
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
        public void RearSuspensionTypeGUI(SuspensionType _susType)
        {
            RearSymmetryGUI = _susType.RearSymmetry_Boolean;

            DoubleWishboneIdentifierRear = _susType.DoubleWishboneRear;
            McPhersonIdentifierRear = _susType.McPhersonRear;

            PushrodIdentifierRear = _susType.PushrodRear;
            PullrodIdentifierRear = _susType.PullrodRear;

            UARBIdentifierRear = _susType.UARBRear;
            TARBIdentifierRear = _susType.TARBRear;
        }
        #endregion

        #region Method to edit the Rear Right Suspension GUI
        public void EditRearSuspensionGUI(Kinematics_Software_New _r1,SuspensionCoordinatesRearRightGUI _scrrGUI)
        {
            r1 = _r1;

            #region Editing the Rear Right Suspension Coordinates GUI Class using User Interface Object

            #region Editing the Coordinates if the Suspension Type is Double Wishbone
            if (_scrrGUI.DoubleWishboneIdentifierRear == 1)
            {
                #region DOUBLE WISHBONE

                #region Fixed Points DOUBLE WISHBONE
                //  Coordinates of Fixed Point D

                D1x = SCRRDataTableGUI.Rows[0].Field<double>(1);
                D1y = SCRRDataTableGUI.Rows[0].Field<double>(2);
                D1z = SCRRDataTableGUI.Rows[0].Field<double>(3);


                //  Coordinates of Fixed Point C

                C1x = SCRRDataTableGUI.Rows[1].Field<double>(1);
                C1y = SCRRDataTableGUI.Rows[1].Field<double>(2);
                C1z = SCRRDataTableGUI.Rows[1].Field<double>(3);


                //  Coordinates of Fixed Point A

                A1x = SCRRDataTableGUI.Rows[2].Field<double>(1);
                A1y = SCRRDataTableGUI.Rows[2].Field<double>(2);
                A1z = SCRRDataTableGUI.Rows[2].Field<double>(3);


                //  Coordinates of Fixed Point B

                B1x = SCRRDataTableGUI.Rows[3].Field<double>(1);
                B1y = SCRRDataTableGUI.Rows[3].Field<double>(2);
                B1z = SCRRDataTableGUI.Rows[3].Field<double>(3);


                // Initial Coordinates of Moving Point I

                I1x = SCRRDataTableGUI.Rows[4].Field<double>(1);
                I1y = SCRRDataTableGUI.Rows[4].Field<double>(2);
                I1z = SCRRDataTableGUI.Rows[4].Field<double>(3);


                // Initial Coordinates of Moving Point Q

                Q1x = SCRRDataTableGUI.Rows[5].Field<double>(1);
                Q1y = SCRRDataTableGUI.Rows[5].Field<double>(2);
                Q1z = SCRRDataTableGUI.Rows[5].Field<double>(3);


                //  Coordinates of Fixed Point N

                N1x = SCRRDataTableGUI.Rows[6].Field<double>(1);
                N1y = SCRRDataTableGUI.Rows[6].Field<double>(2);
                N1z = SCRRDataTableGUI.Rows[6].Field<double>(3);


                // Coordinates of Fixed Point JO

                JO1x = SCRRDataTableGUI.Rows[7].Field<double>(1);
                JO1y = SCRRDataTableGUI.Rows[7].Field<double>(2);
                JO1z = SCRRDataTableGUI.Rows[7].Field<double>(3);

                //  Ride Height Reference Points

                RideHeightRefx = SCRRDataTableGUI.Rows[8].Field<double>(1);
                RideHeightRefy = SCRRDataTableGUI.Rows[8].Field<double>(2);
                RideHeightRefz = SCRRDataTableGUI.Rows[8].Field<double>(3);

                if (_scrrGUI.TARBIdentifierRear == 1)
                {
                    // Initial Coordinates of Fixed Point R  (Only active when the it is T ARB)

                    R1x = SCRRDataTableGUI.Rows[19].Field<double>(1);
                    R1y = SCRRDataTableGUI.Rows[19].Field<double>(2);
                    R1z = SCRRDataTableGUI.Rows[19].Field<double>(3);
                }

                #endregion

                #region Moving Points DOUBLE WISHBONE
                // Initial Coordinates of Moving Point J

                J1x = SCRRDataTableGUI.Rows[9].Field<double>(1);
                J1y = SCRRDataTableGUI.Rows[9].Field<double>(2);
                J1z = SCRRDataTableGUI.Rows[9].Field<double>(3);


                // Initial Coordinates of Moving Point H

                H1x = SCRRDataTableGUI.Rows[10].Field<double>(1);
                H1y = SCRRDataTableGUI.Rows[10].Field<double>(2);
                H1z = SCRRDataTableGUI.Rows[10].Field<double>(3);


                // Initial Coordinates of Moving Point O

                O1x = SCRRDataTableGUI.Rows[11].Field<double>(1);
                O1y = SCRRDataTableGUI.Rows[11].Field<double>(2);
                O1z = SCRRDataTableGUI.Rows[11].Field<double>(3);


                // Initial Coordinates of Moving Point G

                G1x = SCRRDataTableGUI.Rows[12].Field<double>(1);
                G1y = SCRRDataTableGUI.Rows[12].Field<double>(2);
                G1z = SCRRDataTableGUI.Rows[12].Field<double>(3);


                // Initial Coordinates of Moving Point F

                F1x = SCRRDataTableGUI.Rows[13].Field<double>(1);
                F1y = SCRRDataTableGUI.Rows[13].Field<double>(2);
                F1z = SCRRDataTableGUI.Rows[13].Field<double>(3);


                // Initial Coordinates of Moving Point E

                E1x = SCRRDataTableGUI.Rows[14].Field<double>(1);
                E1y = SCRRDataTableGUI.Rows[14].Field<double>(2);
                E1z = SCRRDataTableGUI.Rows[14].Field<double>(3);


                // Initial Coordinates of Moving Point P

                P1x = SCRRDataTableGUI.Rows[15].Field<double>(1);
                P1y = SCRRDataTableGUI.Rows[15].Field<double>(2);
                P1z = SCRRDataTableGUI.Rows[15].Field<double>(3);


                // Initial Coordinates of Moving Point K

                K1x = SCRRDataTableGUI.Rows[16].Field<double>(1);
                K1y = SCRRDataTableGUI.Rows[16].Field<double>(2);
                K1z = SCRRDataTableGUI.Rows[16].Field<double>(3);


                // Initial Coordinates of Moving Point M

                M1x = SCRRDataTableGUI.Rows[17].Field<double>(1);
                M1y = SCRRDataTableGUI.Rows[17].Field<double>(2);
                M1z = SCRRDataTableGUI.Rows[17].Field<double>(3);


                //  Coordinates of Moving Contact Patch Point W

                W1x = SCRRDataTableGUI.Rows[18].Field<double>(1);
                W1y = SCRRDataTableGUI.Rows[18].Field<double>(2);
                W1z = SCRRDataTableGUI.Rows[18].Field<double>(3);

                #endregion

                #endregion
            }
            #endregion

            #region Editing the Coordinates if the Suspension Type is McPherson
            if (_scrrGUI.McPhersonIdentifierRear == 1)
            {
                #region MCPHERSON

                #region Fixed Points MCPHERSON
                //  Coordinates of Fixed Point D

                D1x = SCRRDataTableGUI.Rows[0].Field<double>(1);
                D1y = SCRRDataTableGUI.Rows[0].Field<double>(2);
                D1z = SCRRDataTableGUI.Rows[0].Field<double>(3);


                //  Coordinates of Fixed Point C

                C1x = SCRRDataTableGUI.Rows[1].Field<double>(1);
                C1y = SCRRDataTableGUI.Rows[1].Field<double>(2);
                C1z = SCRRDataTableGUI.Rows[1].Field<double>(3);


                // Initial Coordinates of Moving Point Q

                Q1x = SCRRDataTableGUI.Rows[2].Field<double>(1);
                Q1y = SCRRDataTableGUI.Rows[2].Field<double>(2);
                Q1z = SCRRDataTableGUI.Rows[2].Field<double>(3);


                //  Coordinates of Fixed Point N

                N1x = SCRRDataTableGUI.Rows[3].Field<double>(1);
                N1y = SCRRDataTableGUI.Rows[3].Field<double>(2);
                N1z = SCRRDataTableGUI.Rows[3].Field<double>(3);


                // Coordinates of Fixed Point JO

                JO1x = SCRRDataTableGUI.Rows[4].Field<double>(1);
                JO1y = SCRRDataTableGUI.Rows[4].Field<double>(2);
                JO1z = SCRRDataTableGUI.Rows[4].Field<double>(3);

                // Ride Height Reference Coordinates

                RideHeightRefx = SCRRDataTableGUI.Rows[5].Field<double>(1);
                RideHeightRefy = SCRRDataTableGUI.Rows[5].Field<double>(2);
                RideHeightRefz = SCRRDataTableGUI.Rows[5].Field<double>(3);

                // Coordinates of Fixed Point JO

                J1x = SCRRDataTableGUI.Rows[6].Field<double>(1);
                J1y = SCRRDataTableGUI.Rows[6].Field<double>(2);
                J1z = SCRRDataTableGUI.Rows[6].Field<double>(3);

                #endregion

                #region Moving Points MCPHERSON
                // Initial Coordinates of Moving Point E

                E1x = SCRRDataTableGUI.Rows[7].Field<double>(1);
                E1y = SCRRDataTableGUI.Rows[7].Field<double>(2);
                E1z = SCRRDataTableGUI.Rows[7].Field<double>(3);

                // Initial Coordinates of Moving Point P

                P1x = SCRRDataTableGUI.Rows[8].Field<double>(1);
                P1y = SCRRDataTableGUI.Rows[8].Field<double>(2);
                P1z = SCRRDataTableGUI.Rows[8].Field<double>(3);


                // Initial Coordinates of Moving Point K

                K1x = SCRRDataTableGUI.Rows[9].Field<double>(1);
                K1y = SCRRDataTableGUI.Rows[9].Field<double>(2);
                K1z = SCRRDataTableGUI.Rows[9].Field<double>(3);


                // Initial Coordinates of Moving Point M

                M1x = SCRRDataTableGUI.Rows[10].Field<double>(1);
                M1y = SCRRDataTableGUI.Rows[10].Field<double>(2);
                M1z = SCRRDataTableGUI.Rows[10].Field<double>(3);


                //  Coordinates of Moving Contact Patch Point W

                W1x = SCRRDataTableGUI.Rows[11].Field<double>(1);
                W1y = SCRRDataTableGUI.Rows[11].Field<double>(2);
                W1z = SCRRDataTableGUI.Rows[11].Field<double>(3);
                #endregion

                #endregion
            }
            #endregion

            #endregion
        } 
        #endregion

        #region Functin to display the coordinates of the selected SCRR item
        public static void DisplaySCRRItem(SuspensionCoordinatesRearRight _scrr)
        {
            r1.gridControl2.DataSource = _scrr.SCRRDataTable;

            #region Delete
            //r1.A1xRR.Text = Convert.ToString(_scrr.A1x);
            //r1.A1yRR.Text = Convert.ToString(_scrr.A1y);
            //r1.A1zRR.Text = Convert.ToString(_scrr.A1z);

            //r1.B1xRR.Text = Convert.ToString(_scrr.B1x);
            //r1.B1yRR.Text = Convert.ToString(_scrr.B1y);
            //r1.B1zRR.Text = Convert.ToString(_scrr.B1z);

            //r1.C1xRR.Text = Convert.ToString(_scrr.C1x);
            //r1.C1yRR.Text = Convert.ToString(_scrr.C1y);
            //r1.C1zRR.Text = Convert.ToString(_scrr.C1z);

            //r1.D1xRR.Text = Convert.ToString(_scrr.D1x);
            //r1.D1yRR.Text = Convert.ToString(_scrr.D1y);
            //r1.D1zRR.Text = Convert.ToString(_scrr.D1z);

            //r1.E1xRR.Text = Convert.ToString(_scrr.E1x);
            //r1.E1yRR.Text = Convert.ToString(_scrr.E1y);
            //r1.E1zRR.Text = Convert.ToString(_scrr.E1z);

            //r1.F1xRR.Text = Convert.ToString(_scrr.F1x);
            //r1.F1yRR.Text = Convert.ToString(_scrr.F1y);
            //r1.F1zRR.Text = Convert.ToString(_scrr.F1z);

            //r1.G1xRR.Text = Convert.ToString(_scrr.G1x);
            //r1.G1yRR.Text = Convert.ToString(_scrr.G1y);
            //r1.G1zRR.Text = Convert.ToString(_scrr.G1z);

            //r1.H1xRR.Text = Convert.ToString(_scrr.H1x);
            //r1.H1yRR.Text = Convert.ToString(_scrr.H1y);
            //r1.H1zRR.Text = Convert.ToString(_scrr.H1z);

            //r1.I1xRR.Text = Convert.ToString(_scrr.I1x);
            //r1.I1yRR.Text = Convert.ToString(_scrr.I1y);
            //r1.I1zRR.Text = Convert.ToString(_scrr.I1z);

            //r1.J1xRR.Text = Convert.ToString(_scrr.J1x);
            //r1.J1yRR.Text = Convert.ToString(_scrr.J1y);
            //r1.J1zRR.Text = Convert.ToString(_scrr.J1z);

            //r1.JO1xRR.Text = Convert.ToString(_scrr.JO1x);
            //r1.JO1yRR.Text = Convert.ToString(_scrr.JO1y);
            //r1.JO1zRR.Text = Convert.ToString(_scrr.JO1z);

            //r1.K1xRR.Text = Convert.ToString(_scrr.K1x);
            //r1.K1yRR.Text = Convert.ToString(_scrr.K1y);
            //r1.K1zRR.Text = Convert.ToString(_scrr.K1z);

            //r1.M1xRR.Text = Convert.ToString(_scrr.M1x);
            //r1.M1yRR.Text = Convert.ToString(_scrr.M1y);
            //r1.M1zRR.Text = Convert.ToString(_scrr.M1z);

            //r1.N1xRR.Text = Convert.ToString(_scrr.N1x);
            //r1.N1yRR.Text = Convert.ToString(_scrr.N1y);
            //r1.N1zRR.Text = Convert.ToString(_scrr.N1z);

            //r1.O1xRR.Text = Convert.ToString(_scrr.O1x);
            //r1.O1yRR.Text = Convert.ToString(_scrr.O1y);
            //r1.O1zRR.Text = Convert.ToString(_scrr.O1z);

            //r1.P1xRR.Text = Convert.ToString(_scrr.P1x);
            //r1.P1yRR.Text = Convert.ToString(_scrr.P1y);
            //r1.P1zRR.Text = Convert.ToString(_scrr.P1z);

            //r1.Q1xRR.Text = Convert.ToString(_scrr.Q1x);
            //r1.Q1yRR.Text = Convert.ToString(_scrr.Q1y);
            //r1.Q1zRR.Text = Convert.ToString(_scrr.Q1z);

            //r1.R1xRR.Text = Convert.ToString(_scrr.R1x);
            //r1.R1yRR.Text = Convert.ToString(_scrr.R1y);
            //r1.R1zRR.Text = Convert.ToString(_scrr.R1z);

            //r1.W1xRR.Text = Convert.ToString(_scrr.W1x);
            //r1.W1yRR.Text = Convert.ToString(_scrr.W1y);
            //r1.W1zRR.Text = Convert.ToString(_scrr.W1z);

            //r1.RideHeightRefRRx.Text = Convert.ToString(_scrr.RideHeightRefx);
            //r1.RideHeightRefRRy.Text = Convert.ToString(_scrr.RideHeightRefy);
            //r1.RideHeightRefRRz.Text = Convert.ToString(_scrr.RideHeightRefz); 
            #endregion
        }
        #endregion

        #region De-serialization of the SCRR Object's data
        public SuspensionCoordinatesRearRightGUI(SerializationInfo info, StreamingContext context)
        {
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
            RearSymmetryGUI = (bool)info.GetValue("RearSymmetry", typeof(bool));
            DoubleWishboneIdentifierRear = (int)info.GetValue("DoubleWishbone_Identifier_Rear", typeof(int));
            McPhersonIdentifierRear = (int)info.GetValue("McPherson_Identifier_Rear", typeof(int));
            PushrodIdentifierRear = (int)info.GetValue("Pushrod_Identifier_Rear", typeof(int));
            PullrodIdentifierRear = (int)info.GetValue("Pullrod_Identifier_Rear", typeof(int));
            UARBIdentifierRear = (int)info.GetValue("UARB_Identifier_Rear", typeof(int));
            TARBIdentifierRear = (int)info.GetValue("TARB_Identifier_Rear", typeof(int));
            #endregion

            #region De-serialization of the SCRR Data Table
            SCRRDataTableGUI = (DataTable)info.GetValue("SCRRDataTable", typeof(DataTable));
            #endregion
        } 
        #endregion

        #region Serialization of the SCRR Objec's data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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
            info.AddValue("RearSymmetry", RearSymmetryGUI);
            info.AddValue("DoubleWishbone_Identifier_Rear", DoubleWishboneIdentifierRear);
            info.AddValue("McPherson_Identifier_Rear", McPhersonIdentifierRear);
            info.AddValue("Pushrod_Identifier_Rear", PushrodIdentifierRear);
            info.AddValue("Pullrod_Identifier_Rear", PullrodIdentifierRear);
            info.AddValue("UARB_Identifier_Rear", UARBIdentifierRear);
            info.AddValue("TARB_Identifier_Rear", TARBIdentifierRear);
            #endregion

            #region Serialization of the SCRR Data Table
            info.AddValue("SCRRDataTable", SCRRDataTableGUI);
            #endregion
        } 
        #endregion
    }
}

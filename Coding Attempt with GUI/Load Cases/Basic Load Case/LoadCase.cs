using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class LoadCase : ISerializable
    {
        #region Declarations
        #region General Variables 
        /// <summary>
        /// <para>Never change the way this Name is created. <see cref="BatchRunGUI.navBarItem_BatchRun_Results"/> and <see cref="BatchRunGUI.TabPages_BatchRUn"/> both are dictionaries which use the name of <see cref="LoadCaseName"/> as the keys </para>
        /// Load Case Name
        /// </summary>
        public string LoadCaseName { get; set; }
        /// <summary>
        /// Load Case ID
        /// </summary>
        public int LoadCaseID { get; set; }
        /// <summary>
        /// Counter to keep track of the Number of Load Cases
        /// </summary>
        public static int LoadCaseCounter { get; set; }
        /// <summary>
        /// Boolean variable to determine wheather it is a custom load Case or one from the Template
        /// </summary>
        public bool CustomLoadCase { get; set; }
        /// <summary>
        /// List of Load Case to store the objects of the LoadCase Class.
        /// </summary>
        public static List<LoadCase> List_LoadCases = new List<LoadCase>();
        /// <summary>
        /// List of Load Cases which the user wants to use for his/her Batch Run 
        /// </summary>

        #endregion

        #region Variables to hold the Load Case Values
        /// <summary>
        /// Represents Accelerations at the Suspended Mass CoG. Will be translated to each tire. Public because they will plotted in Eyeshot Control 
        /// </summary>
        public double SM_Ay, SM_Ax, SM_Az;

        /// <summary>
        /// Represents the Lateral Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Lateral Acceleration at the CoG
        /// Public because they will plotted in Eyeshot Control 
        /// </summary>
        public double NSM_FL_Ay, NSM_FR_Ay, NSM_RL_Ay, NSM_RR_Ay;

        /// <summary>
        /// Represents the Longitudinal Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Longitudinal Acceleration at the CoG
        /// Public because they will plotted in Eyeshot Control 
        /// </summary>
        public double NSM_FL_Ax, NSM_FR_Ax, NSM_RL_Ax, NSM_RR_Ax;

        /// <summary>
        /// Represents the Vertical Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Vertical Acceleration at the CoG
        /// Public because they will plotted in Eyeshot Control 
        /// </summary>
        public double NSM_FL_Az, NSM_FR_Az, NSM_RL_Az, NSM_RR_Az;

        /// <summary>
        /// Represents the Lateral Grip Distribution. This parameter will be used to determine how much of the Lateral Force at the CG is reacted at the tire. 
        /// </summary>
        private double NSM_FL_LatGripDistribution, NSM_FR_LatGripDistribution, NSM_RL_LatGripDistribution, NSM_RR_LatGripDistribution;

        /// <summary>
        /// Represents the Longitudinal Grip Distribution. This parameter will be used to determine how much of the Lateral Force at the CG is reacted at the tire
        /// </summary>
        private double NSM_FL_LongGripDistribution, NSM_FR_LongGripDistribution, NSM_RL_LongGripDistribution, NSM_RR_LongGripDistribution;

        ///<remarks>
        ///Below are the Public Variables which will be availabble to the other Classes
        /// </remarks>

        /// <summary>
        /// Represents the Overturning Moment on the Tire on each corner
        /// </summary>
        public double NSM_FL_Mx, NSM_FR_Mx, NSM_RL_Mx, NSM_RR_Mx;

        /// <summary>
        /// Represents the Self Aligning Torque on the Tire on each corner
        /// </summary>
        public double NSM_FL_Mz, NSM_FR_Mz, NSM_RL_Mz, NSM_RR_Mz;

        /// <summary>
        /// Represents the total lateral load on each tire. This is computed by summing the Lateral Force obtained from the CG and the individual Loads at the Unsprung Masses. 
        /// </summary>
        public double TotalLoad_FL_Fy, TotalLoad_FR_Fy, TotalLoad_RL_Fy, TotalLoad_RR_Fy;
        /// <summary>
        /// Represents the total Longitudinal load on each tire. This is computed by summing the Longitudinal Force obtained from the CG and the individual Loads at the Unsprung Masses. 
        /// </summary>
        public double TotalLoad_FL_Fx, TotalLoad_FR_Fx, TotalLoad_RL_Fx, TotalLoad_RR_Fx;
        /// <summary>
        /// Represents the total Vertical load on each tire. This is computed by summing the Vertical Force obtained from the CG and the individual Loads at the Unsprung Masses. 
        /// </summary>
        public double TotalLoad_FL_Fz, TotalLoad_FR_Fz, TotalLoad_RL_Fz, TotalLoad_RR_Fz;

        /// <summary>
        /// Represents an array which holds X,Y,Z coordinates of the AntiRoll Bar and Steering Rack Attachments of the Front. This is a 3x4 matrix because there are 3 coordinates and 4 points; 2 points for securing the 2 bearings bearing.
        /// Index 0 refers to Steering Rack Inboard Point 1 - Upper in Top View
        /// Index 1 refers to Steering Rack Inboard Point 2 - Lower in Top View
        /// Index 2 referes to ARB Bearing Inboard Point 1 - Upper in Top View+
        /// Index 3 referes to ARB Bearing Inboard Point 2 - Lower in Top View
        /// </summary>
        public double[,] FL_BearingCoordinates = new double[3, 4], FR_BearingCoordinates = new double[3, 4];

        /// <summary>
        /// Represents an array which holds the X,Y,Z coordinates of the Antiroll Bar Attachments of the rear. This is 3x2 matrix because there are 3 coordinates and 2 points; 2 points for securing the single bearing 
        /// Index 0 referes to ARB Bearing Inboard Point 1 - Upper in Top View
        /// Index 1 referes to ARB Bearing Inboard Point 2 - Lower In Top View
        /// </summary>
        public double[,] RL_BearingCoordinates = new double[3, 2], RR_BearingCoordinates = new double[3, 2];

        /// <summary>
        /// Represents an array which holds the X,Y,Z coordinates of the Steering Column Bearing. This a 3x2 matrix because there are 3 coordinates and 2 points; 2 points for securingg the single bearing 
        /// Index 0 refers to Steering Column Bearing Inboard Point 1 - Upper in Top View
        /// Index 0 refers to Steering Column Bearing Inboard Point 2 - Lower in Top View
        /// </summary>
        public double[,] SteeringColumnBearing = new double[3, 2];

        #endregion

        #region Batch Run Result Variables
        /// <summary>
        /// Results of the Batch Run for the Front Left Corner
        /// </summary>
        public BatchRunResults runResults_FL = new BatchRunResults();
        /// <summary>
        /// Results of the Batch Run for the Front Right Corner
        /// </summary>
        public BatchRunResults runResults_FR = new BatchRunResults();
        /// <summary>
        /// Results of the Batch Run for the Rear Left Corner
        /// </summary>
        public BatchRunResults runResults_RL = new BatchRunResults();
        /// <summary>
        /// Results of the Batch Run for the Rear Right Corner
        /// </summary>
        public BatchRunResults runResults_RR = new BatchRunResults(); 
        #endregion

        #region Data Tables 
        /// <summary>
        /// Data Table of the Front Left Non Suspended Mass
        /// </summary>
        public DataTable NSM_FL_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Front Right Non Suspended Mass
        /// </summary>
        public DataTable NSM_FR_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Rear Left Non Suspended Mass
        /// </summary>
        public DataTable NSM_RL_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Rear Right Non Suspended Mass
        /// </summary>
        public DataTable NSM_RR_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Suspended Mass
        /// </summary>
        public DataTable SuspendedMass_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Front Left Bearing Attachment Coordinates
        /// </summary>
        public DataTable FL_Bearing_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Front Right Bearing Attachment Coordinates
        /// </summary>
        public DataTable FR_Bearing_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Rear Left Bearing Attachment Coordinates
        /// </summary>
        public DataTable RL_Bearing_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Rear Right Bearing Attachment Coordinates
        /// </summary>
        public DataTable RR_Bearing_DataTable = new DataTable();
        /// <summary>
        /// Data Table of the Steering Column Bearing Attachment Coordinates
        /// </summary>
        public DataTable SteeringColumnBearing_DataTable = new DataTable();
        #endregion 
        #endregion

        #region Constructors
        /// <summary>
        /// Base Constructor. 
        /// This constructor is created here only so that the Load Case object can be initialized without having to pass any arguments.
        /// This is needed because otherwise the LoadCase object will not be instantiated untill a Load Case item is created. 
        /// If the user wants to save the file without creating a Load Case object he will not be able to do so unless this constructor is used to create the LoadCase object 
        ///</summary>
        public LoadCase()
        {

        }
        /// <summary>
        /// Overloaded Constructor used to initialise the Data Tables
        /// </summary>
        /// <param name="_loadCaseGUI">Object of the LoadCaseGUI Class</param>
        public LoadCase(LoadCaseGUI _loadCaseGUI)
        {
            #region Data Table Initialization

            NSM_FL_DataTable = _loadCaseGUI.NSM_FL_DataTableGUI;
            NSM_FR_DataTable = _loadCaseGUI.NSM_FR_DataTableGUI;
            NSM_RL_DataTable = _loadCaseGUI.NSM_RL_DataTableGUI;
            NSM_RR_DataTable = _loadCaseGUI.NSM_RR_DataTableGUI;
            SuspendedMass_DataTable = _loadCaseGUI.SuspendedMass_DataTableGUI;
            FL_Bearing_DataTable = _loadCaseGUI.FL_Bearing_DataTable_GUI;
            FR_Bearing_DataTable = _loadCaseGUI.FR_Bearing_DataTable_GUI;
            RL_Bearing_DataTable = _loadCaseGUI.RL_Bearing_DataTable_GUI;
            RR_Bearing_DataTable = _loadCaseGUI.RR_Bearing_DataTable_GUI;

            #endregion

            #region Parameter Initialization

            UpdateLoadCase(_loadCaseGUI);

            #endregion

        } 
        #endregion

        #region Create New load Case method
        /// <summary>
        /// This is a static method inserts an object to the List of LoadCases. 
        /// </summary>
        /// <param name="index">Index of the Load Case. Basically this is the number of Load Case Items created minus 1</param>
        /// <param name="_loadCaseGUI">Object of the corresponding <c>LoadCaseGUI</c> Class</param>
        /// <param name="_loadCaseName">Name of the Load Case. To be decided based on whether custom or prediefined Load Case is used. </param>
        public static void CreateLoadCase(int index, LoadCaseGUI _loadCaseGUI, string _loadCaseName)
        {
            List_LoadCases.Insert(index, new LoadCase(_loadCaseGUI));

            if (List_LoadCases[index].CustomLoadCase) { List_LoadCases[index].LoadCaseName = "Custom Load Case - #" + Convert.ToString(index + 1); }
            else List_LoadCases[index].LoadCaseName = _loadCaseName + " - #" + Convert.ToString(index + 1);

            List_LoadCases[index].LoadCaseID = index + 1;
        }
        #endregion

        #region Update Load Case Method
        public void UpdateLoadCase(LoadCaseGUI _loadCaseGUI)
        {
            #region Parameter Initialization
            NSM_FL_Ay = _loadCaseGUI.NSM_FL_Az_GUI;
            NSM_FR_Ay = _loadCaseGUI.NSM_FR_Az_GUI;
            NSM_RL_Ay = _loadCaseGUI.NSM_RL_Az_GUI;
            NSM_RR_Ay = _loadCaseGUI.NSM_RR_Az_GUI;

            NSM_FL_Ax = _loadCaseGUI.NSM_FL_Ay_GUI;
            NSM_FR_Ax = _loadCaseGUI.NSM_FR_Ay_GUI;
            NSM_RL_Ax = _loadCaseGUI.NSM_RL_Ay_GUI;
            NSM_RR_Ax = _loadCaseGUI.NSM_RR_Ay_GUI;

            NSM_FL_Az = _loadCaseGUI.NSM_FL_Ax_GUI;
            NSM_FR_Az = _loadCaseGUI.NSM_FR_Ax_GUI;
            NSM_RL_Az = _loadCaseGUI.NSM_RL_Ax_GUI;
            NSM_RR_Az = _loadCaseGUI.NSM_RR_Ax_GUI;

            NSM_FL_LatGripDistribution = _loadCaseGUI.NSM_FL_LatGripDistribution_GUI;
            NSM_FR_LatGripDistribution = _loadCaseGUI.NSM_FR_LatGripDistribution_GUI;
            NSM_RL_LatGripDistribution = _loadCaseGUI.NSM_RL_LatGripDistribution_GUI;
            NSM_RR_LatGripDistribution = _loadCaseGUI.NSM_RR_LatGripDistribution_GUI;


            NSM_FL_LongGripDistribution = _loadCaseGUI.NSM_FL_LongGripDistribution_GUI;
            NSM_FR_LongGripDistribution = _loadCaseGUI.NSM_FR_LongGripDistribution_GUI;
            NSM_RL_LongGripDistribution = _loadCaseGUI.NSM_RL_LongGripDistribution_GUI;
            NSM_RR_LongGripDistribution = _loadCaseGUI.NSM_RR_LongGripDistribution_GUI;

            ///<remarks>
            /// Not changing the order over here because The coordinate system of the moments have been taken care of in the <c>DoubleWishboneKinematicSolver</c> CLass
            /// </remarks>
            NSM_FL_Mx = _loadCaseGUI.NSM_FL_Mx_GUI;
            NSM_FR_Mx = _loadCaseGUI.NSM_FR_Mx_GUI;
            NSM_RL_Mx = _loadCaseGUI.NSM_RL_Mx_GUI;
            NSM_RR_Mx = _loadCaseGUI.NSM_RR_Mx_GUI;

            NSM_FL_Mz = _loadCaseGUI.NSM_FL_Mz_GUI;
            NSM_FR_Mz = _loadCaseGUI.NSM_FR_Mz_GUI;
            NSM_RL_Mz = _loadCaseGUI.NSM_RL_Mz_GUI;
            NSM_RR_Mz = _loadCaseGUI.NSM_RR_Mz_GUI;

            SM_Ax = _loadCaseGUI.SM_Ay_GUI;
            SM_Ay = _loadCaseGUI.SM_Az_GUI;
            SM_Az = _loadCaseGUI.SM_Ax_GUI;

            #region Front Left Bearing Attachment points
            FL_BearingCoordinates[0, 0] = _loadCaseGUI.FL_BearingCoordinates_GUI[1, 0];
            FL_BearingCoordinates[1, 0] = _loadCaseGUI.FL_BearingCoordinates_GUI[2, 0];
            FL_BearingCoordinates[2, 0] = _loadCaseGUI.FL_BearingCoordinates_GUI[0, 0];

            FL_BearingCoordinates[0, 1] = _loadCaseGUI.FL_BearingCoordinates_GUI[1, 1];
            FL_BearingCoordinates[1, 1] = _loadCaseGUI.FL_BearingCoordinates_GUI[2, 1];
            FL_BearingCoordinates[2, 1] = _loadCaseGUI.FL_BearingCoordinates_GUI[0, 1];

            FL_BearingCoordinates[0, 2] = _loadCaseGUI.FL_BearingCoordinates_GUI[1, 2];
            FL_BearingCoordinates[1, 2] = _loadCaseGUI.FL_BearingCoordinates_GUI[2, 2];
            FL_BearingCoordinates[2, 2] = _loadCaseGUI.FL_BearingCoordinates_GUI[0, 2];

            FL_BearingCoordinates[0, 3] = _loadCaseGUI.FL_BearingCoordinates_GUI[1, 3];
            FL_BearingCoordinates[1, 3] = _loadCaseGUI.FL_BearingCoordinates_GUI[2, 3];
            FL_BearingCoordinates[2, 3] = _loadCaseGUI.FL_BearingCoordinates_GUI[0, 3];
            #endregion

            #region Front Right Bearing Attachment points
            FR_BearingCoordinates[0, 0] = _loadCaseGUI.FR_BearingCoordinates_GUI[1, 0];
            FR_BearingCoordinates[1, 0] = _loadCaseGUI.FR_BearingCoordinates_GUI[2, 0];
            FR_BearingCoordinates[2, 0] = _loadCaseGUI.FR_BearingCoordinates_GUI[0, 0];

            FR_BearingCoordinates[0, 1] = _loadCaseGUI.FR_BearingCoordinates_GUI[1, 1];
            FR_BearingCoordinates[1, 1] = _loadCaseGUI.FR_BearingCoordinates_GUI[2, 1];
            FR_BearingCoordinates[2, 1] = _loadCaseGUI.FR_BearingCoordinates_GUI[0, 1];

            FR_BearingCoordinates[0, 2] = _loadCaseGUI.FR_BearingCoordinates_GUI[1, 2];
            FR_BearingCoordinates[1, 2] = _loadCaseGUI.FR_BearingCoordinates_GUI[2, 2];
            FR_BearingCoordinates[2, 2] = _loadCaseGUI.FR_BearingCoordinates_GUI[0, 2];

            FR_BearingCoordinates[0, 3] = _loadCaseGUI.FR_BearingCoordinates_GUI[1, 3];
            FR_BearingCoordinates[1, 3] = _loadCaseGUI.FR_BearingCoordinates_GUI[2, 3];
            FR_BearingCoordinates[2, 3] = _loadCaseGUI.FR_BearingCoordinates_GUI[0, 3];
            #endregion

            #region Rear Left Bearing Attachment points
            RL_BearingCoordinates[0, 0] = _loadCaseGUI.RL_BearingCoordinates_GUI[1, 0];
            RL_BearingCoordinates[1, 0] = _loadCaseGUI.RL_BearingCoordinates_GUI[2, 0];
            RL_BearingCoordinates[2, 0] = _loadCaseGUI.RL_BearingCoordinates_GUI[0, 0];

            RL_BearingCoordinates[0, 1] = _loadCaseGUI.RL_BearingCoordinates_GUI[1, 1];
            RL_BearingCoordinates[1, 1] = _loadCaseGUI.RL_BearingCoordinates_GUI[2, 1];
            RL_BearingCoordinates[2, 1] = _loadCaseGUI.RL_BearingCoordinates_GUI[0, 1];
            #endregion

            #region Rear Right Bearing Attachment points
            RR_BearingCoordinates[0, 0] = _loadCaseGUI.RR_BearingCoordinates_GUI[1, 0];
            RR_BearingCoordinates[1, 0] = _loadCaseGUI.RR_BearingCoordinates_GUI[2, 0];
            RR_BearingCoordinates[2, 0] = _loadCaseGUI.RR_BearingCoordinates_GUI[0, 0];

            RR_BearingCoordinates[0, 1] = _loadCaseGUI.RR_BearingCoordinates_GUI[1, 1];
            RR_BearingCoordinates[1, 1] = _loadCaseGUI.RR_BearingCoordinates_GUI[2, 1];
            RR_BearingCoordinates[2, 1] = _loadCaseGUI.RR_BearingCoordinates_GUI[0, 1];
            #endregion

            #region Steering Column Bearing Attachment Points
            SteeringColumnBearing[0, 0] = _loadCaseGUI.SteeringColumnBearing_GUI[1, 0];
            SteeringColumnBearing[1, 0] = _loadCaseGUI.SteeringColumnBearing_GUI[2, 0];
            SteeringColumnBearing[2, 0] = _loadCaseGUI.SteeringColumnBearing_GUI[0, 0];

            SteeringColumnBearing[0, 1] = _loadCaseGUI.SteeringColumnBearing_GUI[1, 1];
            SteeringColumnBearing[1, 1] = _loadCaseGUI.SteeringColumnBearing_GUI[2, 1];
            SteeringColumnBearing[2, 1] = _loadCaseGUI.SteeringColumnBearing_GUI[0, 1];
            #endregion

            #endregion
        }
        #endregion

        #region Commpute Wheel Loads Method
        /// <summary>
        /// Computes the Total Wheel Loads due the Accelerations at the Suspended Mass, Accelerations at the Non Suspended Mass and Moments at the Non Suspended Mass
        /// </summary>
        /// <param name="_vLoadCase"></param>
        public void ComputeWheelLoads(Vehicle _vLoadCase)
        {
            ///<summary>
            ///Initializing the Masses
            /// </summary>
            double SuspendedMass = (_vLoadCase.SM_FL + _vLoadCase.SM_FR + _vLoadCase.SM_RL + _vLoadCase.SM_RR);
            double NSM_FL = _vLoadCase.NSM_FL * 9.18, NSM_FR = _vLoadCase.NSM_FR * 9.81, NSM_RL = _vLoadCase.NSM_RL * 9.81, NSM_RR = _vLoadCase.NSM_RR * 9.81;
            double SuspendedWeightDistributionFront = (_vLoadCase.SM_FL + _vLoadCase.SM_FR) / SuspendedMass;

            ///<summary>
            ///Calculating the Total Lateral Load on each tire
            /// </summary>

            ///<remarks>
            ///This is the Vertical Load and NOT the Lateral Load. The solver uses a Solidworks style Coordinate System
            ///---IMPORTANT--- For Suspended Mass, Negative sign is needed because negative vertical acceleration implies downard acceleration and hence increase in SM weight. Imagine IT 
            ///---IMPORTANT--- For Non Suspended Mass, Negative sign is NOT NEEDED because negative vertical acceleration implies downard acceleration and hence decrease in NSM in weight. Imagine IT 
            /// </remarks>
            TotalLoad_FL_Fy = (-SM_Ay * SuspendedMass * SuspendedWeightDistributionFront / 2) + (/*-*/NSM_FL_Ay * NSM_FL);
            TotalLoad_FR_Fy = (-SM_Ay * SuspendedMass * SuspendedWeightDistributionFront / 2) + (/*-*/NSM_FR_Ay * NSM_FR);
            TotalLoad_RL_Fy = (-SM_Ay * SuspendedMass * (1 - SuspendedWeightDistributionFront) / 2) + (/*-*/NSM_RL_Ay * NSM_RL);
            TotalLoad_RR_Fy = (-SM_Ay * SuspendedMass * (1 - SuspendedWeightDistributionFront) / 2) + (/*-*/NSM_RR_Ay * NSM_RR);
            ///<summary>
            ///This is the Lateral Load and NOT the Longitudinal Load. The solver uses a Solidworks style Coordinate System
            /// </summary>
            TotalLoad_FL_Fx = (SM_Ax * SuspendedMass * NSM_FL_LatGripDistribution) / 100 + (NSM_FL_Ax * NSM_FL);
            TotalLoad_FR_Fx = (SM_Ax * SuspendedMass * NSM_FR_LatGripDistribution) / 100 + (NSM_FR_Ax * NSM_FR);
            TotalLoad_RL_Fx = (SM_Ax * SuspendedMass * NSM_RL_LatGripDistribution) / 100 + (NSM_RL_Ax * NSM_RL);
            TotalLoad_RR_Fx = (SM_Ax * SuspendedMass * NSM_RR_LatGripDistribution) / 100 + (NSM_RR_Ax * NSM_RR);
            ///<summary>
            ///This is the Longitudinal Load and NOT the Vertical Load. The solver uses a Solidworks style Coordinate System
            /// </summary>
            TotalLoad_FL_Fz = (SM_Az * SuspendedMass * NSM_FL_LongGripDistribution) / 100 + (NSM_FL_Az * NSM_FL);
            TotalLoad_FR_Fz = (SM_Az * SuspendedMass * NSM_FR_LongGripDistribution) / 100 + (NSM_FR_Az * NSM_FR);
            TotalLoad_RL_Fz = (SM_Az * SuspendedMass * NSM_RL_LongGripDistribution) / 100 + (NSM_RL_Az * NSM_RL);
            TotalLoad_RR_Fz = (SM_Az * SuspendedMass * NSM_RR_LongGripDistribution) / 100 + (NSM_RR_Az * NSM_RR);
        }
        #endregion

        #region Deserialization
        public LoadCase(SerializationInfo info, StreamingContext context)
        {
            #region General Variables
            LoadCaseName = (string)info.GetValue("LoadCaseName", typeof(string));
            LoadCaseID = (int)info.GetValue("LoadCaseID", typeof(int));
            LoadCaseCounter = (int)info.GetValue("LoadCaseCounter", typeof(int));
            CustomLoadCase = (bool)info.GetValue("CustomLoadCase", typeof(bool));
            #endregion

            #region Load Case Variables
            SM_Ax = (double)info.GetValue("SM_Ax", typeof(double));
            SM_Ay = (double)info.GetValue("SM_Ay", typeof(double));
            SM_Az = (double)info.GetValue("SM_Az", typeof(double));

            NSM_FL_Ax = (double)info.GetValue("NSM_FL_Ax", typeof(double));
            NSM_FL_Ay = (double)info.GetValue("NSM_FL_Ay", typeof(double));
            NSM_FL_Az = (double)info.GetValue("NSM_FL_Az", typeof(double));

            NSM_FR_Ax = (double)info.GetValue("NSM_FR_Ax", typeof(double));
            NSM_FR_Ay = (double)info.GetValue("NSM_FR_Ay", typeof(double));
            NSM_FR_Az = (double)info.GetValue("NSM_FR_Az", typeof(double));

            NSM_RL_Ax = (double)info.GetValue("NSM_RL_Ax", typeof(double));
            NSM_RL_Ay = (double)info.GetValue("NSM_RL_Ay", typeof(double));
            NSM_RL_Az = (double)info.GetValue("NSM_RL_Az", typeof(double));

            NSM_RR_Ax = (double)info.GetValue("NSM_RR_Ax", typeof(double));
            NSM_RR_Ay = (double)info.GetValue("NSM_RR_Ay", typeof(double));
            NSM_RR_Az = (double)info.GetValue("NSM_RR_Az", typeof(double));

            NSM_FL_LatGripDistribution = (double)info.GetValue("NSM_FL_LatGripDistribution", typeof(double));
            NSM_FL_LongGripDistribution = (double)info.GetValue("NSM_FL_LongGripDistribution", typeof(double));

            NSM_FR_LatGripDistribution = (double)info.GetValue("NSM_FR_LatGripDistribution", typeof(double));
            NSM_FR_LongGripDistribution = (double)info.GetValue("NSM_FR_LongGripDistribution", typeof(double));

            NSM_RL_LatGripDistribution = (double)info.GetValue("NSM_RL_LatGripDistribution", typeof(double));
            NSM_RL_LongGripDistribution = (double)info.GetValue("NSM_RL_LongGripDistribution", typeof(double));

            NSM_RR_LatGripDistribution = (double)info.GetValue("NSM_RR_LatGripDistribution", typeof(double));
            NSM_RR_LongGripDistribution = (double)info.GetValue("NSM_RR_LongGripDistribution", typeof(double));

            NSM_FL_Mx = (double)info.GetValue("NSM_FL_Mx", typeof(double));
            NSM_FL_Mz = (double)info.GetValue("NSM_FL_Mz", typeof(double));

            NSM_FR_Mx = (double)info.GetValue("NSM_FR_Mx", typeof(double));
            NSM_FR_Mz = (double)info.GetValue("NSM_FR_Mz", typeof(double));

            NSM_RL_Mx = (double)info.GetValue("NSM_RL_Mx", typeof(double));
            NSM_RL_Mz = (double)info.GetValue("NSM_RL_Mz", typeof(double));

            NSM_RR_Mx = (double)info.GetValue("NSM_RR_Mx", typeof(double));
            NSM_RR_Mz = (double)info.GetValue("NSM_RR_Mz", typeof(double));

            TotalLoad_FL_Fx = (double)info.GetValue("TotalLoad_FL_Fx", typeof(double));
            TotalLoad_FL_Fy = (double)info.GetValue("TotalLoad_FL_Fy", typeof(double));
            TotalLoad_FL_Fz = (double)info.GetValue("TotalLoad_FL_Fz", typeof(double));

            TotalLoad_FR_Fx = (double)info.GetValue("TotalLoad_FR_Fx", typeof(double));
            TotalLoad_FR_Fy = (double)info.GetValue("TotalLoad_FR_Fy", typeof(double));
            TotalLoad_FR_Fz = (double)info.GetValue("TotalLoad_FR_Fz", typeof(double));

            TotalLoad_RL_Fx = (double)info.GetValue("TotalLoad_RL_Fx", typeof(double));
            TotalLoad_RL_Fy = (double)info.GetValue("TotalLoad_RL_Fy", typeof(double));
            TotalLoad_RL_Fz = (double)info.GetValue("TotalLoad_RL_Fz", typeof(double));

            TotalLoad_RR_Fx = (double)info.GetValue("TotalLoad_RR_Fx", typeof(double));
            TotalLoad_RR_Fy = (double)info.GetValue("TotalLoad_RR_Fy", typeof(double));
            TotalLoad_RR_Fz = (double)info.GetValue("TotalLoad_RR_Fz", typeof(double));

            FL_BearingCoordinates = (double[,])info.GetValue("FL_BearingCoordinates", typeof(double[,]));
            FR_BearingCoordinates = (double[,])info.GetValue("FR_BearingCoordinates", typeof(double[,]));
            RL_BearingCoordinates = (double[,])info.GetValue("RL_BearingCoordinates", typeof(double[,]));
            RR_BearingCoordinates = (double[,])info.GetValue("RR_BearingCoordinates", typeof(double[,]));
            SteeringColumnBearing = (double[,])info.GetValue("SteeringColumnBearing", typeof(double[,]));
            #endregion

            #region Datatables
            NSM_FL_DataTable = (DataTable)info.GetValue("NSM_FL_DataTable", typeof(DataTable));
            NSM_FR_DataTable = (DataTable)info.GetValue("NSM_FR_DataTable", typeof(DataTable));
            NSM_RL_DataTable = (DataTable)info.GetValue("NSM_RL_DataTable", typeof(DataTable));
            NSM_RR_DataTable = (DataTable)info.GetValue("NSM_RR_DataTable", typeof(DataTable));
            SuspendedMass_DataTable = (DataTable)info.GetValue("SuspendedMass_DataTable", typeof(DataTable));
            FL_Bearing_DataTable = (DataTable)info.GetValue("FL_Bearing_DataTable", typeof(DataTable));
            FR_Bearing_DataTable = (DataTable)info.GetValue("FR_Bearing_DataTable", typeof(DataTable));
            RL_Bearing_DataTable = (DataTable)info.GetValue("RL_Bearing_DataTable", typeof(DataTable));
            RR_Bearing_DataTable = (DataTable)info.GetValue("RR_Bearing_DataTable", typeof(DataTable));
            SteeringColumnBearing_DataTable = (DataTable)info.GetValue("SteeringColumnBearing_DataTable", typeof(DataTable));
            #endregion
        } 
        #endregion

        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            #region General Variables
            info.AddValue("LoadCaseName", LoadCaseName);
            info.AddValue("LoadCaseID", LoadCaseID);
            info.AddValue("LoadCaseCounter", LoadCaseCounter);
            info.AddValue("CustomLoadCase", CustomLoadCase);
            #endregion

            #region Load Case Variables
            info.AddValue("SM_Ax", SM_Ax);
            info.AddValue("SM_Ay", SM_Ay);
            info.AddValue("SM_Az", SM_Az);

            info.AddValue("NSM_FL_Ax", NSM_FL_Ax);
            info.AddValue("NSM_FL_Ay", NSM_FL_Ay);
            info.AddValue("NSM_FL_Az", NSM_FL_Az);

            info.AddValue("NSM_FR_Ax", NSM_FR_Ax);
            info.AddValue("NSM_FR_Ay", NSM_FR_Ay);
            info.AddValue("NSM_FR_Az", NSM_FR_Az);

            info.AddValue("NSM_RL_Ax", NSM_RL_Ax);
            info.AddValue("NSM_RL_Ay", NSM_RL_Ay);
            info.AddValue("NSM_RL_Az", NSM_RL_Az);

            info.AddValue("NSM_RR_Ax", NSM_RR_Ax);
            info.AddValue("NSM_RR_Ay", NSM_RR_Ay);
            info.AddValue("NSM_RR_Az", NSM_RR_Az);

            info.AddValue("NSM_FL_LatGripDistribution", NSM_FL_LatGripDistribution);
            info.AddValue("NSM_FL_LongGripDistribution", NSM_FL_LongGripDistribution);

            info.AddValue("NSM_FR_LatGripDistribution", NSM_FR_LatGripDistribution);
            info.AddValue("NSM_FR_LongGripDistribution", NSM_FR_LongGripDistribution);

            info.AddValue("NSM_RL_LatGripDistribution", NSM_RL_LatGripDistribution);
            info.AddValue("NSM_RL_LongGripDistribution", NSM_RL_LongGripDistribution);

            info.AddValue("NSM_RR_LatGripDistribution", NSM_RR_LatGripDistribution);
            info.AddValue("NSM_RR_LongGripDistribution", NSM_RR_LongGripDistribution);

            info.AddValue("NSM_FL_Mx", NSM_FL_Mx);
            info.AddValue("NSM_FL_Mz", NSM_FL_Mz);

            info.AddValue("NSM_FR_Mx", NSM_FR_Mx);
            info.AddValue("NSM_FR_Mz", NSM_FR_Mz);

            info.AddValue("NSM_RL_Mx", NSM_RL_Mx);
            info.AddValue("NSM_RL_Mz", NSM_RL_Mz);

            info.AddValue("NSM_RR_Mx", NSM_RR_Mx);
            info.AddValue("NSM_RR_Mz", NSM_RR_Mz);

            info.AddValue("TotalLoad_FL_Fx", TotalLoad_FL_Fx);
            info.AddValue("TotalLoad_FL_Fy", TotalLoad_FL_Fy);
            info.AddValue("TotalLoad_FL_Fz", TotalLoad_FL_Fz);

            info.AddValue("TotalLoad_FR_Fx", TotalLoad_FR_Fx);
            info.AddValue("TotalLoad_FR_Fy", TotalLoad_FR_Fy);
            info.AddValue("TotalLoad_FR_Fz", TotalLoad_FR_Fz);

            info.AddValue("TotalLoad_RL_Fx", TotalLoad_RL_Fx);
            info.AddValue("TotalLoad_RL_Fy", TotalLoad_RL_Fy);
            info.AddValue("TotalLoad_RL_Fz", TotalLoad_RL_Fz);

            info.AddValue("TotalLoad_RR_Fx", TotalLoad_RR_Fx);
            info.AddValue("TotalLoad_RR_Fy", TotalLoad_RR_Fy);
            info.AddValue("TotalLoad_RR_Fz", TotalLoad_RR_Fz);

            info.AddValue("FL_BearingCoordinates", FL_BearingCoordinates);
            info.AddValue("FR_BearingCoordinates", FR_BearingCoordinates);
            info.AddValue("RL_BearingCoordinates", RL_BearingCoordinates);
            info.AddValue("RR_BearingCoordinates", RR_BearingCoordinates);
            info.AddValue("SteeringColumnBearing", SteeringColumnBearing);
            #endregion

            #region Datatables
            info.AddValue("NSM_FL_DataTable", NSM_FL_DataTable);
            info.AddValue("NSM_FR_DataTable", NSM_FR_DataTable);
            info.AddValue("NSM_RL_DataTable", NSM_RL_DataTable);
            info.AddValue("NSM_RR_DataTable", NSM_RR_DataTable);
            info.AddValue("SuspendedMass_DataTable", SuspendedMass_DataTable);
            info.AddValue("FL_Bearing_DataTable", FL_Bearing_DataTable);
            info.AddValue("FR_Bearing_DataTable", FR_Bearing_DataTable);
            info.AddValue("RL_Bearing_DataTable", RL_Bearing_DataTable);
            info.AddValue("RR_Bearing_DataTable", RR_Bearing_DataTable);
            info.AddValue("SteeringColumnBearing_DataTable", SteeringColumnBearing_DataTable);
            #endregion
        } 
        #endregion
    }
}

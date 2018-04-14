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
using DevExpress.XtraNavBar;


namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class LoadCaseGUI : ISerializable
    {
        #region Declarations
        #region General Variables
        /// <summary>
        /// List of LoadCaseGUI Objects. 
        /// </summary>
        public static List<LoadCaseGUI> List_LoadCaseGUI = new List<LoadCaseGUI>();

        /// <summary>
        /// Name of the Load Case
        /// </summary>
        public string _LoadCaseName { get; set; }

        /// <summary>
        /// ID of the Load Case
        /// </summary>
        public int _LoadCaseID { get; set; }

        /// <summary>
        /// Counter which keeps record of the Number of Load Cases created
        /// </summary>
        public static int _LoadCaseCounter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CustomLoadCase { get; set; }

        /// <summary>
        /// Obbject of the Main Form
        /// </summary>
        Kinematics_Software_New r1;
        #endregion

        #region Variables to hold the GUI's Load Case Values
        /// <summary>
        /// Represents Accelerations at the Suspended Mass CoG. Will be translated to each tire
        /// </summary>
        public double SM_Ay_GUI, SM_Ax_GUI, SM_Az_GUI;

        /// <summary>
        /// Represents the Lateral Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Lateral Acceleration at the CoG
        /// </summary>
        public double NSM_FL_Ay_GUI, NSM_FR_Ay_GUI, NSM_RL_Ay_GUI, NSM_RR_Ay_GUI;

        /// <summary>
        /// Represents the Longitudinal Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Longitudinal Acceleration at the CoG
        /// </summary>
        public double NSM_FL_Ax_GUI, NSM_FR_Ax_GUI, NSM_RL_Ax_GUI, NSM_RR_Ax_GUI;

        /// <summary>
        /// Represents the Vertical Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Vertical Acceleration at the CoG
        /// </summary>
        public double NSM_FL_Az_GUI, NSM_FR_Az_GUI, NSM_RL_Az_GUI, NSM_RR_Az_GUI;

        /// <summary>
        /// Represents the Lateral Grip Distribution. This parameter will be used to determine how much of the Lateral Force at the CG is reacted at the tire. 
        /// </summary>
        public double NSM_FL_LatGripDistribution_GUI, NSM_FR_LatGripDistribution_GUI, NSM_RL_LatGripDistribution_GUI, NSM_RR_LatGripDistribution_GUI;

        /// <summary>
        /// Represents the Longitudinal Grip Distribution. This parameter will be used to determine how much of the Lateral Force at the CG is reacted at the tire
        /// </summary>
        public double NSM_FL_LongGripDistribution_GUI, NSM_FR_LongGripDistribution_GUI, NSM_RL_LongGripDistribution_GUI, NSM_RR_LongGripDistribution_GUI;

        /// <summary>
        /// Represents the Overturning Moment on the Tire on each corner
        /// </summary>
        public double NSM_FL_Mx_GUI, NSM_FR_Mx_GUI, NSM_RL_Mx_GUI, NSM_RR_Mx_GUI;

        /// <summary>
        /// Represents the Self Aligning Torque on the Tire on each corner
        /// </summary>
        public double NSM_FL_Mz_GUI, NSM_FR_Mz_GUI, NSM_RL_Mz_GUI, NSM_RR_Mz_GUI;

        /// <summary>
        /// Represents an array which holds X,Y,Z coordinates of the AntiRoll Bar and Steering Rack Attachments of the Front. This is a 3x4 matrix because there are 3 coordinates and 4 points; 2 points for securing the 2 bearings bearing.
        /// Index 0 refers to Steering Rack Inboard Point 1
        /// Index 1 refers to Steering Rack Inboard Point 2
        /// Index 2 referes to ARB Bearing Inboard Point 1
        /// Index 3 referes to ARB Bearing Inboard Point 2
        /// </summary>
        public double[,] FL_BearingCoordinates_GUI = new double[3, 4], FR_BearingCoordinates_GUI = new double[3, 4];

        /// <summary>
        /// Represents an array which holds the X,Y,Z coordinates of the Antiroll Bar Attachments of the rear. This is 3x2 matrix because there are 3 coordinates and 2 points; 2 points for securing the single bearing 
        /// Index 0 referes to ARB Bearing Inboard Point 1
        /// Index 1 referes to ARB Bearing Inboard Point 2
        /// </summary>
        public double[,] RL_BearingCoordinates_GUI = new double[3, 2], RR_BearingCoordinates_GUI = new double[3, 2];

        /// <summary>
        /// Represents an array which holds the X,Y,Z coordinates of the Steering Column Bearing. This a 3x2 matrix because there are 3 coordinates and 2 points; 2 points for securingg the single bearing 
        /// Index 0 refers to Steering Column Bearing Inboard Point 1
        /// Index 0 refers to Steering Column Bearing Inboard Point 2
        /// </summary>
        public double[,] SteeringColumnBearing_GUI = new double[3,2];

        #endregion

        #region NavBarItem
        /// <summary>
        /// Navigation Bar Item which will appear in the Load Case Group
        /// </summary>
        public CusNavBarItem navBarItemLoadCase;
        #endregion

        #region Grid Controls
        /// <summary>
        /// Grid Control of the Front Left Non Suspeneded Mass
        /// </summary>
        public GridControl Grid_NSM_FL = new GridControl();
        /// <summary>
        /// Grid Control of the Front Right Non Suspeneded Mass
        /// </summary>
        public GridControl Grid_NSM_FR = new GridControl();
        /// <summary>
        /// Grid Control of the Rear Left Non Suspeneded Mass
        /// </summary>
        public GridControl Grid_NSM_RL = new GridControl();
        /// <summary>
        /// Grid Control of the Rear Right Non Suspeneded Mass
        /// </summary>
        public GridControl Grid_NSM_RR = new GridControl();
        /// <summary>
        /// Grid Control of the Suspeneded Mass
        /// </summary>
        public GridControl Grid_Suspended = new GridControl();
        /// <summary>
        /// Grid Control of the Front Left Bearing Attachment Coordinates
        /// </summary>
        public GridControl Grid_BearingAttachment_FL = new GridControl();
        /// <summary>
        /// Grid Control of the Front Right Bearing Attachment Coordinates
        /// </summary>
        public GridControl Grid_BearingAttachment_FR = new GridControl();
        /// <summary>
        /// Grid Control of the Rear Left Bearing Attachment Coordinates
        /// </summary>
        public GridControl Grid_BearingAttachment_RL = new GridControl();
        /// <summary>
        /// Grid Control of the Rear Right Bearing Attachment Coordinates
        /// </summary>
        public GridControl Grid_BearingAttachment_RR = new GridControl();
        /// <summary>
        /// Grid Control of the Steering Column Bearing Attachment Coordinates
        /// </summary>
        public GridControl Grid_BearingAttachment_SteeringColumn = new GridControl();
        #endregion

        #region Data Tables
        /// <summary>
        /// Data Table of the Front Left Non Suspended Mass
        /// </summary>
        public DataTable NSM_FL_DataTableGUI = new DataTable();
        /// <summary>
        /// Data Table of the Front Right Non Suspended Mass
        /// </summary>
        public DataTable NSM_FR_DataTableGUI = new DataTable();
        /// <summary>
        /// Data Table of the Rear Left Non Suspended Mass
        /// </summary>
        public DataTable NSM_RL_DataTableGUI = new DataTable();
        /// <summary>
        /// Data Table of the Rear Right Non Suspended Mass
        /// </summary>
        public DataTable NSM_RR_DataTableGUI = new DataTable();
        /// <summary>
        /// Data Table of the Suspended Mass
        /// </summary>
        public DataTable SuspendedMass_DataTableGUI = new DataTable();
        /// <summary>
        /// Data Table of the Front Left Bearing Attachment Coordinates
        /// </summary>
        public DataTable FL_Bearing_DataTable_GUI = new DataTable();
        /// <summary>
        /// Data Table of the Front Right Bearing Attachment Coordinates
        /// </summary>
        public DataTable FR_Bearing_DataTable_GUI = new DataTable();
        /// <summary>
        /// Data Table of the Rear Left Bearing Attachment Coordinates
        /// </summary>
        public DataTable RL_Bearing_DataTable_GUI = new DataTable();
        /// <summary>
        /// Data Table of the Rear Right Bearing Attachment Coordinates
        /// </summary>
        public DataTable RR_Bearing_DataTable_GUI = new DataTable();
        /// <summary>
        /// Data Table of the Steering Column Bearing Attachment Coordinates
        /// </summary>
        public DataTable SteeringColumnBearing_DataTable_GUI = new DataTable();
        #endregion

        #region User Control
        /// <summary>
        /// Object of the Load Case User Control 
        /// </summary>
        public XUC_LoadCase LC = new XUC_LoadCase();
        /// <summary>
        /// <para>---Only for <see cref="BatchRunGUI"/>---</para>
        /// Object of the <see cref="XtraUserControl_WishboneForces"/> User Control
        /// </summary>
        public XtraUserControl_WishboneForces batchRun_WF = new XtraUserControl_WishboneForces();
        #endregion

        #region Tab Page
        /// <summary>
        /// Tab Page which will contain the Load Case User Control and will be added to the CusXtraTabControl
        /// </summary>
        public CustomXtraTabPage TabPage_LC = new CustomXtraTabPage();
        #endregion

        #region Banded Grid Views
        /// <summary>
        /// Banded Grid View for the Front Left Non Suspended Mass's Data Grid
        /// </summary>
        public CustomBandedGridView bandedGrid_NSM_FL = new CustomBandedGridView();
        /// <summary>
        /// Banded Grid View for the Front Right Non Suspended Mass's Data Grid
        /// </summary>
        public CustomBandedGridView bandedGrid_NSM_FR = new CustomBandedGridView();
        /// <summary>
        /// Banded Grid View for the Rear Left Non Suspended Mass's Data Grid
        /// </summary>
        public CustomBandedGridView bandedGrid_NSM_RL = new CustomBandedGridView();
        /// <summary>
        /// Banded Grid View for the Rear Right Non Suspended Mass's Data Grid
        /// </summary>
        public CustomBandedGridView bandedGrid_NSM_RR = new CustomBandedGridView();
        /// <summary>
        /// Banded Grid View for the Suspended Mass's Data Grid
        /// </summary>
        public CustomBandedGridView bandedGrid_SuspendedMass = new CustomBandedGridView();
        /// <summary>
        /// banded Grid View for the Front Left Bearing Attachment Coordinates
        /// </summary>
        public CustomBandedGridView bandedGird_BearingAttachment_FL = new CustomBandedGridView();
        /// <summary>
        /// banded Grid View for the Front Right Bearing Attachment Coordinates
        /// </summary>
        public CustomBandedGridView bandedGird_BearingAttachment_FR = new CustomBandedGridView();
        /// <summary>
        /// banded Grid View for the Rear Left Bearing Attachment Coordinates
        /// </summary>
        public CustomBandedGridView bandedGird_BearingAttachment_RL = new CustomBandedGridView();
        /// <summary>
        /// banded Grid View for the Rear Right Bearing Attachment Coordinates
        /// </summary>
        public CustomBandedGridView bandedGird_BearingAttachment_RR = new CustomBandedGridView();
        /// <summary>
        /// banded Grid View for the Steering Column Bearing Attachment Coordinates
        /// </summary>
        public CustomBandedGridView bandedGird_SteeringColumn = new CustomBandedGridView();
        #endregion 
        #endregion

        #region Constructors

        public LoadCaseGUI()
        {

        }

        /// <summary>
        /// Contructor of the LoadCaseGUI Class
        /// </summary>
        /// <param name="_loadCaseName">Name of the Load Case. Default is "Load Case + ID" which will be used for Custom Load Cases. </param>
        /// <param name="_loadCaseID">ID of the Load Case being created</param>
        /// <param name="_r1"></param>
        public LoadCaseGUI(string _loadCaseName, int _loadCaseID, Kinematics_Software_New _r1)
        {
            r1 = _r1;

            #region Initializing the Data Tables
            ///<summary>
            ///Front Left Non Suspended Mass Data Table Initialization
            /// </summary>
            NSM_FL_DataTableGUI = new DataTable();

            NSM_FL_DataTableGUI.TableName = "Front Left Non Suspended Mass Parameters";

            NSM_FL_DataTableGUI.Columns.Add("Input Parameters", typeof(string));
            NSM_FL_DataTableGUI.Columns[0].ReadOnly = true;

            NSM_FL_DataTableGUI.Columns.Add("Values", typeof(double));

            ///<summary>
            ///Front Right Non Suspended Mass Data Table Initialization
            /// </summary>
            NSM_FR_DataTableGUI = new DataTable();

            NSM_FR_DataTableGUI.TableName = "Front Right Non Suspended Mass Parameters";

            NSM_FR_DataTableGUI.Columns.Add("Input Parameters", typeof(string));
            NSM_FR_DataTableGUI.Columns[0].ReadOnly = true;

            NSM_FR_DataTableGUI.Columns.Add("Values", typeof(double));

            ///<summary>
            ///Rear Left Non Suspended Mass Data Table Initialization
            /// </summary>
            NSM_RL_DataTableGUI = new DataTable();

            NSM_RL_DataTableGUI.TableName = "Rear Left Non Suspended Mass Parameters";

            NSM_RL_DataTableGUI.Columns.Add("Input Parameters", typeof(string));
            NSM_RL_DataTableGUI.Columns[0].ReadOnly = true;

            NSM_RL_DataTableGUI.Columns.Add("Values", typeof(double));

            ///<summary>
            ///Rear Right Non Suspended Mass Data Table Initialization
            /// </summary>
            NSM_RR_DataTableGUI = new DataTable();

            NSM_RR_DataTableGUI.TableName = "Rear Right Non Suspended Mass Parameters";

            NSM_RR_DataTableGUI.Columns.Add("Input Parameters", typeof(string));
            NSM_RR_DataTableGUI.Columns[0].ReadOnly = true;

            NSM_RR_DataTableGUI.Columns.Add("Values", typeof(double));

            ///<summary>
            ///Suspended Mass Data Table Initialization
            /// </summary>
            /// 
            SuspendedMass_DataTableGUI = new DataTable();

            SuspendedMass_DataTableGUI.TableName = "Suspended Mass Parameters";

            SuspendedMass_DataTableGUI.Columns.Add("Input Parameters", typeof(string));
            SuspendedMass_DataTableGUI.Columns[0].ReadOnly = true;

            SuspendedMass_DataTableGUI.Columns.Add("Values", typeof(double));

            ///<summary>
            ///Front Left Bearing Coordinates Data Table Initialization
            /// </summary>
            FL_Bearing_DataTable_GUI = new DataTable();

            FL_Bearing_DataTable_GUI.TableName = "Front Left Bearing Attachment Points";

            FL_Bearing_DataTable_GUI.Columns.Add("Attachment Point", typeof(string));
            FL_Bearing_DataTable_GUI.Columns[0].ReadOnly = true;

            FL_Bearing_DataTable_GUI.Columns.Add("X (mm)", typeof(double));
            FL_Bearing_DataTable_GUI.Columns.Add("Y (mm)", typeof(double));
            FL_Bearing_DataTable_GUI.Columns.Add("Z (mm)", typeof(double));

            ///<summary>
            ///Front Right Bearing Coordinates Data Table Initialization
            /// </summary>
            FR_Bearing_DataTable_GUI = new DataTable();

            FR_Bearing_DataTable_GUI.TableName = "Front Right Bearing Attachment Points";

            FR_Bearing_DataTable_GUI.Columns.Add("Attachment Point", typeof(string));
            FR_Bearing_DataTable_GUI.Columns[0].ReadOnly = true;

            FR_Bearing_DataTable_GUI.Columns.Add("X (mm)", typeof(double));
            FR_Bearing_DataTable_GUI.Columns.Add("Y (mm)", typeof(double));
            FR_Bearing_DataTable_GUI.Columns.Add("Z (mm)", typeof(double));

            ///<summary>
            ///Rear Left Bearing Coordinates Data Table Initialization
            /// </summary>
            RL_Bearing_DataTable_GUI = new DataTable();

            RL_Bearing_DataTable_GUI.TableName = "Rear Left Bearing Attachment Points";

            RL_Bearing_DataTable_GUI.Columns.Add("Attachment Point", typeof(string));
            RL_Bearing_DataTable_GUI.Columns[0].ReadOnly = true;

            RL_Bearing_DataTable_GUI.Columns.Add("X (mm)", typeof(double));
            RL_Bearing_DataTable_GUI.Columns.Add("Y (mm)", typeof(double));
            RL_Bearing_DataTable_GUI.Columns.Add("Z (mm)", typeof(double));

            ///<summary>
            ///Rear Right Bearing Coordinates Data Table Initialization
            /// </summary>
            RR_Bearing_DataTable_GUI = new DataTable();

            RR_Bearing_DataTable_GUI.TableName = "Rear Right Bearing Attachment Points";

            RR_Bearing_DataTable_GUI.Columns.Add("Attachment Point", typeof(string));
            RR_Bearing_DataTable_GUI.Columns[0].ReadOnly = true;

            RR_Bearing_DataTable_GUI.Columns.Add("X (mm)", typeof(double));
            RR_Bearing_DataTable_GUI.Columns.Add("Y (mm)", typeof(double));
            RR_Bearing_DataTable_GUI.Columns.Add("Z (mm)", typeof(double));

            ///<summary>
            ///Steering Column Bearing Coordinate Data Table Initialization
            /// </summary>
            SteeringColumnBearing_DataTable_GUI = new DataTable();

            SteeringColumnBearing_DataTable_GUI.TableName = "Steering Column Bearing Attachment Points";

            SteeringColumnBearing_DataTable_GUI.Columns.Add("Attachment Point", typeof(string));
            SteeringColumnBearing_DataTable_GUI.Columns[0].ReadOnly = true;

            SteeringColumnBearing_DataTable_GUI.Columns.Add("X (mm)", typeof(double));
            SteeringColumnBearing_DataTable_GUI.Columns.Add("Y (mm)", typeof(double));
            SteeringColumnBearing_DataTable_GUI.Columns.Add("Z (mm)", typeof(double));
            
            #endregion

            _LoadCaseName = _loadCaseName;
            _LoadCaseID = _loadCaseID;
            navBarItemLoadCase = new CusNavBarItem(_LoadCaseName, _LoadCaseID + 1, this);
            TabPage_LC = CustomXtraTabPage.CreateNewTabPage_ForInputs(_LoadCaseName, _LoadCaseID + 1);
        }
        #endregion

        #region Methods to Handle the GUI Operations and Events
        /// <summary>
        /// Method to Handle the GUI Operations of Adding User Control to the Tab Page, navBarItems to the NavBarGroups and TabPages to the TabControl
        /// </summary>
        /// <param name="_navBarGroup">Object of the NavBarGroup inside the Simulation NavBarControl which refers to the Load Case</param>
        /// <param name="_navBarControl">NavBarControl Simulation</param>
        /// <param name="_r1">Object of the Main Class Form</param>
        /// <param name="Index">Index referes to the List Index of the LoadCase List. This is <c>_LoadCaseID<c> - 1.</param>
        public void HandleGUI(NavBarGroup _navBarGroup, NavBarControl _navBarControl, Kinematics_Software_New _r1, int Index, bool _defaultItemBeingCreated)
        {
            r1 = _r1;
            ///<remarks>
            ///Adding the Grid Controls to the Group Controls of the Load Case User Control
            /// </remarks>
            LC.groupControlNSMFL.Controls.Add(Grid_NSM_FL);
            Grid_NSM_FL.Dock = DockStyle.Fill;
            LC.groupControlNSMFR.Controls.Add(Grid_NSM_FR);
            Grid_NSM_FR.Dock = DockStyle.Fill;
            LC.groupControlNSMRL.Controls.Add(Grid_NSM_RL);
            Grid_NSM_RL.Dock = DockStyle.Fill;
            LC.groupControlNSMRR.Controls.Add(Grid_NSM_RR);
            Grid_NSM_RR.Dock = DockStyle.Fill;
            LC.groupControlSuspendedMass.Controls.Add(Grid_Suspended);
            Grid_Suspended.Dock = DockStyle.Fill;
            LC.groupControlFLBearing.Controls.Add(Grid_BearingAttachment_FL);
            Grid_BearingAttachment_FL.Dock = DockStyle.Fill;
            LC.groupControlFRBearing.Controls.Add(Grid_BearingAttachment_FR);
            Grid_BearingAttachment_FR.Dock = DockStyle.Fill;
            LC.groupControlRLBearing.Controls.Add(Grid_BearingAttachment_RL);
            Grid_BearingAttachment_RL.Dock = DockStyle.Fill;
            LC.groupControlRRBearing.Controls.Add(Grid_BearingAttachment_RR);
            Grid_BearingAttachment_RR.Dock = DockStyle.Fill;
            LC.groupControlSteeringColumnBearing.Controls.Add(Grid_BearingAttachment_SteeringColumn);
            Grid_BearingAttachment_SteeringColumn.Dock = DockStyle.Fill;

            ///<remarks>
            ///Adding the User Control to the TabPage
            ///Nxt, Adding the TabPage to the TabControl of the Main Form
            ///Last, Selecting the newly added page
            /// </remarks>
            TabPage_LC = CustomXtraTabPage.AddUserControlToTabPage(LC, TabPage_LC, DockStyle.Fill);
            Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, List_LoadCaseGUI[Index].TabPage_LC);
            if (!_defaultItemBeingCreated)
            {
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPage = List_LoadCaseGUI[Index].TabPage_LC; 
            }
            if (_defaultItemBeingCreated)
            {
                for (int i = 0; i < Kinematics_Software_New.TabControl_Outputs.TabPages.Count; i++)
                {
                    Kinematics_Software_New.TabControl_Outputs.TabPages[i].PageVisible = false;
                }
            }

            ///<remarks>
            ///Adding the NavBarItem to the LoadCase Group of the Simulation NavBarControl
            ///Assigning Link Clicked Events
            /// </remarks>
            List_LoadCaseGUI[Index].navBarItemLoadCase.CreateNavBarItem(List_LoadCaseGUI[Index].navBarItemLoadCase, _navBarGroup, _navBarControl);
            List_LoadCaseGUI[Index].navBarItemLoadCase = List_LoadCaseGUI[Index].LinkClickedEventCreator(List_LoadCaseGUI[Index].navBarItemLoadCase, _r1);

        }

        #region Methods to assign the Link Clicked Evenets
        /// <summary>
        /// Method to Create the LinkClickedEvent of the Load Cases' NavBarItem
        /// </summary>
        /// <param name="_navBarItemLoadCase"></param>
        /// <param name="_r1"></param>
        /// <returns></returns>
        public CusNavBarItem LinkClickedEventCreator(CusNavBarItem _navBarItemLoadCase, Kinematics_Software_New _r1)
        {
            _navBarItemLoadCase.LinkClicked += _navBaritemLoadCase_LinkClicked;
            r1 = _r1;

            return _navBarItemLoadCase;
        }

        /// <summary>
        /// This method is fired when a NavBarItem of the LoadCasesGUI Class is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _navBaritemLoadCase_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = 0, SelectedPage = 0;

            index = r1.navBarGroupLoadCases.SelectedLinkIndex;
            List_LoadCaseGUI[index].TabPage_LC.PageVisible = true;
            SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(List_LoadCaseGUI[index].TabPage_LC);
            Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
            Kinematics_Software_New.TabControl_Outputs.TabPages[SelectedPage].PageVisible = true;

        } 
        #endregion

        #endregion

        #region Method to Initalize OR Update the Values (if user changes values in the DataGrid)
        /// <summary>
        /// This method is called when the user creates a Load Case for the first time or updates the values using the DataGrid. 
        /// </summary>
        public void UpdateLoadCase()
        {
            ///<summary>
            ///Front Left Non Suspended Mass Initialization/Updation
            /// </summary>
            NSM_FL_Ay_GUI = NSM_FL_DataTableGUI.Rows[0].Field<double>(1);
            NSM_FL_LatGripDistribution_GUI = NSM_FL_DataTableGUI.Rows[1].Field<double>(1);
            NSM_FL_Ax_GUI = NSM_FL_DataTableGUI.Rows[2].Field<double>(1);
            NSM_FL_LongGripDistribution_GUI = NSM_FL_DataTableGUI.Rows[3].Field<double>(1);
            NSM_FL_Az_GUI = NSM_FL_DataTableGUI.Rows[4].Field<double>(1);
            NSM_FL_Mx_GUI = NSM_FL_DataTableGUI.Rows[5].Field<double>(1);
            NSM_FL_Mz_GUI = NSM_FL_DataTableGUI.Rows[6].Field<double>(1);

            ///<summary>
            ///Front Right Non Suspended Mass Initialization/Updation
            /// </summary>
            NSM_FR_Ay_GUI = NSM_FR_DataTableGUI.Rows[0].Field<double>(1);
            NSM_FR_LatGripDistribution_GUI = NSM_FR_DataTableGUI.Rows[1].Field<double>(1);
            NSM_FR_Ax_GUI = NSM_FR_DataTableGUI.Rows[2].Field<double>(1);
            NSM_FR_LongGripDistribution_GUI = NSM_FR_DataTableGUI.Rows[3].Field<double>(1);
            NSM_FR_Az_GUI = NSM_FR_DataTableGUI.Rows[4].Field<double>(1);
            NSM_FR_Mx_GUI = NSM_FR_DataTableGUI.Rows[5].Field<double>(1);
            NSM_FR_Mz_GUI = NSM_FR_DataTableGUI.Rows[6].Field<double>(1);

            ///<summary>
            ///Rear Left Non Suspended Mass Initialization/Updation
            /// </summary>
            NSM_RL_Ay_GUI = NSM_RL_DataTableGUI.Rows[0].Field<double>(1);
            NSM_RL_LatGripDistribution_GUI = NSM_RL_DataTableGUI.Rows[1].Field<double>(1);
            NSM_RL_Ax_GUI = NSM_RL_DataTableGUI.Rows[2].Field<double>(1);
            NSM_RL_LongGripDistribution_GUI = NSM_RL_DataTableGUI.Rows[3].Field<double>(1);
            NSM_RL_Az_GUI = NSM_RL_DataTableGUI.Rows[4].Field<double>(1);
            NSM_RL_Mx_GUI = NSM_RL_DataTableGUI.Rows[5].Field<double>(1);
            NSM_RL_Mz_GUI = NSM_RL_DataTableGUI.Rows[6].Field<double>(1);

            ///<summary>
            ///Rear Right Non Suspended Mass Initialization/Updation
            /// </summary>
            NSM_RR_Ay_GUI = NSM_RR_DataTableGUI.Rows[0].Field<double>(1);
            NSM_RR_LatGripDistribution_GUI = NSM_RR_DataTableGUI.Rows[1].Field<double>(1);
            NSM_RR_Ax_GUI = NSM_RR_DataTableGUI.Rows[2].Field<double>(1);
            NSM_RR_LongGripDistribution_GUI = NSM_RR_DataTableGUI.Rows[3].Field<double>(1);
            NSM_RR_Az_GUI = NSM_RR_DataTableGUI.Rows[4].Field<double>(1);
            NSM_RR_Mx_GUI = NSM_RR_DataTableGUI.Rows[5].Field<double>(1);
            NSM_RR_Mz_GUI = NSM_RR_DataTableGUI.Rows[6].Field<double>(1);

            ///<summary>
            ///Suspended Mass Initialization/Updation
            /// </summary>
            SM_Ay_GUI = SuspendedMass_DataTableGUI.Rows[0].Field<double>(1);
            SM_Ax_GUI = SuspendedMass_DataTableGUI.Rows[1].Field<double>(1);
            SM_Az_GUI = SuspendedMass_DataTableGUI.Rows[2].Field<double>(1);

            #region Front Left Bearing Attachment Points
            ///<summary>
            ///Front Left Bearing Attachment Points
            /// </summary>

            ///<remarks>
            ///Steering Inboard Point 1
            /// </remarks>
            FL_BearingCoordinates_GUI[0, 0] = FL_Bearing_DataTable_GUI.Rows[0].Field<double>(1);
            FL_BearingCoordinates_GUI[1, 0] = FL_Bearing_DataTable_GUI.Rows[0].Field<double>(2);
            FL_BearingCoordinates_GUI[2, 0] = FL_Bearing_DataTable_GUI.Rows[0].Field<double>(3);
            ///<remarks>
            ///Steering Inboard Point 2
            /// </remarks>
            FL_BearingCoordinates_GUI[0, 1] = FL_Bearing_DataTable_GUI.Rows[1].Field<double>(1);
            FL_BearingCoordinates_GUI[1, 1] = FL_Bearing_DataTable_GUI.Rows[1].Field<double>(2);
            FL_BearingCoordinates_GUI[2, 1] = FL_Bearing_DataTable_GUI.Rows[1].Field<double>(3);
            ///<remarks>
            ///ARB Inboard Point 1
            /// </remarks>
            FL_BearingCoordinates_GUI[0, 2] = FL_Bearing_DataTable_GUI.Rows[2].Field<double>(1);
            FL_BearingCoordinates_GUI[1, 2] = FL_Bearing_DataTable_GUI.Rows[2].Field<double>(2);
            FL_BearingCoordinates_GUI[2, 2] = FL_Bearing_DataTable_GUI.Rows[2].Field<double>(3);
            ///<remarks>
            ///ARB Inboard Point 2
            /// </remarks>
            FL_BearingCoordinates_GUI[0, 3] = FL_Bearing_DataTable_GUI.Rows[3].Field<double>(1);
            FL_BearingCoordinates_GUI[1, 3] = FL_Bearing_DataTable_GUI.Rows[3].Field<double>(2);
            FL_BearingCoordinates_GUI[2, 3] = FL_Bearing_DataTable_GUI.Rows[3].Field<double>(3);
            #endregion

            #region Front Right Bearing Attachment Points
            ///<summary>
            ///Front Right Bearing Attachment Points
            /// </summary>

            ///<remarks>
            ///Steering Inboard Point 1
            /// </remarks>
            FR_BearingCoordinates_GUI[0, 0] = FR_Bearing_DataTable_GUI.Rows[0].Field<double>(1);
            FR_BearingCoordinates_GUI[1, 0] = FR_Bearing_DataTable_GUI.Rows[0].Field<double>(2);
            FR_BearingCoordinates_GUI[2, 0] = FR_Bearing_DataTable_GUI.Rows[0].Field<double>(3);
            ///<remarks>
            ///Steering Inboard Point 2
            /// </remarks>
            FR_BearingCoordinates_GUI[0, 1] = FR_Bearing_DataTable_GUI.Rows[1].Field<double>(1);
            FR_BearingCoordinates_GUI[1, 1] = FR_Bearing_DataTable_GUI.Rows[1].Field<double>(2);
            FR_BearingCoordinates_GUI[2, 1] = FR_Bearing_DataTable_GUI.Rows[1].Field<double>(3);
            ///<remarks>
            ///ARB Inboard Point 1
            /// </remarks>
            FR_BearingCoordinates_GUI[0, 2] = FR_Bearing_DataTable_GUI.Rows[2].Field<double>(1);
            FR_BearingCoordinates_GUI[1, 2] = FR_Bearing_DataTable_GUI.Rows[2].Field<double>(2);
            FR_BearingCoordinates_GUI[2, 2] = FR_Bearing_DataTable_GUI.Rows[2].Field<double>(3);
            ///<remarks>
            ///ARB Inboard Point 2
            /// </remarks>
            FR_BearingCoordinates_GUI[0, 3] = FR_Bearing_DataTable_GUI.Rows[3].Field<double>(1);
            FR_BearingCoordinates_GUI[1, 3] = FR_Bearing_DataTable_GUI.Rows[3].Field<double>(2);
            FR_BearingCoordinates_GUI[2, 3] = FR_Bearing_DataTable_GUI.Rows[3].Field<double>(3);
            #endregion

            #region Rear Left Bearing Attachment Points
            ///<summary>
            ///Rear Left Bearing Attachment Points
            /// </summary>

            ///<remarks>
            ///ARB Inboard Point 1
            /// </remarks>
            RL_BearingCoordinates_GUI[0, 0] = RL_Bearing_DataTable_GUI.Rows[0].Field<double>(1);
            RL_BearingCoordinates_GUI[1, 0] = RL_Bearing_DataTable_GUI.Rows[0].Field<double>(2);
            RL_BearingCoordinates_GUI[2, 0] = RL_Bearing_DataTable_GUI.Rows[0].Field<double>(3);
            ///<remarks>
            ///ARB Inboard Point 2
            /// </remarks>
            RL_BearingCoordinates_GUI[0, 1] = RL_Bearing_DataTable_GUI.Rows[1].Field<double>(1);
            RL_BearingCoordinates_GUI[1, 1] = RL_Bearing_DataTable_GUI.Rows[1].Field<double>(2);
            RL_BearingCoordinates_GUI[2, 1] = RL_Bearing_DataTable_GUI.Rows[1].Field<double>(3);
            #endregion

            #region Rear Right Bearing Attachment Points
            ///<summary>
            ///Rear Right Bearing Attachment Points
            /// </summary>

            ///<remarks>
            ///ARB Inboard Point 1
            /// </remarks>
            RR_BearingCoordinates_GUI[0, 0] = RR_Bearing_DataTable_GUI.Rows[0].Field<double>(1);
            RR_BearingCoordinates_GUI[1, 0] = RR_Bearing_DataTable_GUI.Rows[0].Field<double>(2);
            RR_BearingCoordinates_GUI[2, 0] = RR_Bearing_DataTable_GUI.Rows[0].Field<double>(3);
            ///<remarks>
            ///ARB Inboard Point 2
            /// </remarks>
            RR_BearingCoordinates_GUI[0, 1] = RR_Bearing_DataTable_GUI.Rows[1].Field<double>(1);
            RR_BearingCoordinates_GUI[1, 1] = RR_Bearing_DataTable_GUI.Rows[1].Field<double>(2);
            RR_BearingCoordinates_GUI[2, 1] = RR_Bearing_DataTable_GUI.Rows[1].Field<double>(3);
            #endregion

            #region Steering Column Bearing Attachment Points
            ///<remarks>
            ///Inboard Point 1
            /// </remarks>
            SteeringColumnBearing_GUI[0, 0] = SteeringColumnBearing_DataTable_GUI.Rows[0].Field<double>(1);
            SteeringColumnBearing_GUI[1, 0] = SteeringColumnBearing_DataTable_GUI.Rows[0].Field<double>(2);
            SteeringColumnBearing_GUI[2, 0] = SteeringColumnBearing_DataTable_GUI.Rows[0].Field<double>(3);
            ///<remarks>
            ///Inboard Point 2
            /// </remarks>
            SteeringColumnBearing_GUI[0, 1] = SteeringColumnBearing_DataTable_GUI.Rows[1].Field<double>(1);
            SteeringColumnBearing_GUI[1, 1] = SteeringColumnBearing_DataTable_GUI.Rows[1].Field<double>(2);
            SteeringColumnBearing_GUI[2, 1] = SteeringColumnBearing_DataTable_GUI.Rows[1].Field<double>(3);
            #endregion

        }
        #endregion

        #region Grid Control Operations and Cell Value Change Events
        /// <summary>
        /// Method to Initalize the Grid Control, assign a Data Source and Main View to it, assign Value Change and Value Editor Methods
        /// </summary>

        #region Grid Control Initializer

        private void InitializeGridControls(int _noOfColumns, string _bandName, string _columnName1, string _columnName2, GridControl _gridControl, DataTable _dataSource, CustomBandedGridView _bandedGridView, bool Coordinate, Kinematics_Software_New _r)
        {
            float fontSize = 11;
            _bandedGridView = CustomBandedGridView.CreateNewBandedGridView(0, _noOfColumns, _bandName);
            _gridControl.BindingContext = new BindingContext();
            _gridControl.DataSource = null;
            _gridControl.ForceInitialize();
            _gridControl.DataSource = _dataSource;
            _gridControl.MainView = _bandedGridView;
            _bandedGridView.CellValueChanged += BandedGrid_LoadCase_CellValueChanged;
            _bandedGridView.ValidatingEditor += BandedGrid_LoadCase_ValidatingEditor;

            FontFamily LoadCaseFont = new FontFamily("Tahoma");
            _bandedGridView.Appearance.Row.Font = new Font(LoadCaseFont, fontSize);
            if (!Coordinate)
            {
                _bandedGridView = CustomBandedGridColumn.ColumnEditor_ForLoadCases(_bandedGridView, _columnName1, _columnName2); 
            }
            else
            {
                _bandedGridView = CustomBandedGridColumn.ColumnEditor_ForSuspension(_bandedGridView, _r);
                
            }
        }
        public void InitializeGridControls(Kinematics_Software_New _r1)
        {
            InitializeGridControls(2, "Front Left Non Suspended Mass Loads", "Front Left Non Suspended Mass Parameters", "Values", Grid_NSM_FL, NSM_FL_DataTableGUI, bandedGrid_NSM_FL, false, _r1);

            InitializeGridControls(2, "Front Right Non Suspended Mass Loads", "Front Right Non Suspended Mass Parameters", "Values", Grid_NSM_FR, NSM_FR_DataTableGUI, bandedGrid_NSM_FR, false, _r1);

            InitializeGridControls(2, "Rear Left Non Suspended Mass Loads", "Rear Left Non Suspended Mass Parameters", "Values", Grid_NSM_RL, NSM_RL_DataTableGUI, bandedGrid_NSM_RL, false, _r1);

            InitializeGridControls(2, "Rear Right Non Suspended Mass Loads", "Rear Right Non Suspended Mass Parameters", "Values", Grid_NSM_RR, NSM_RR_DataTableGUI, bandedGrid_NSM_RR, false, _r1);

            InitializeGridControls(2, "Suspended Mass Loads", "Suspended Mass Parameters", "Values", Grid_Suspended, SuspendedMass_DataTableGUI, bandedGrid_SuspendedMass, false, _r1);

            InitializeGridControls(4, "Front Left Bearing Attachment Points", null, null, Grid_BearingAttachment_FL, FL_Bearing_DataTable_GUI, bandedGird_BearingAttachment_FL, true, _r1);

            InitializeGridControls(4, "Front Right Bearing Attachment Points", null, null, Grid_BearingAttachment_FR, FR_Bearing_DataTable_GUI, bandedGird_BearingAttachment_FR, true, _r1);

            InitializeGridControls(4, "Rear Left Bearing Attachment Points", null, null, Grid_BearingAttachment_RL, RL_Bearing_DataTable_GUI, bandedGird_BearingAttachment_RL, true, _r1);

            InitializeGridControls(4, "Rear Right Bearing Attachment Points", null, null, Grid_BearingAttachment_RR, RR_Bearing_DataTable_GUI, bandedGird_BearingAttachment_RR, true, _r1);

            InitializeGridControls(4, "Steering Column Bearing Attachment Point", null, null, Grid_BearingAttachment_SteeringColumn, SteeringColumnBearing_DataTable_GUI, bandedGird_SteeringColumn, true, _r1);

        } 
        #endregion

        #region Cell Value Validator
        /// <summary>
        /// This method ensures that incorrect input is not passed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BandedGrid_LoadCase_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            double checker = 0;
            if (!Double.TryParse(e.Value as string, out checker))
            {
                e.Valid = false;
                e.ErrorText = "Please enter numeric values";
            }

            else
            {
                if (Grid_NSM_FL.IsFocused)
                {
                    if (bandedGrid_NSM_FL.FocusedRowHandle == 1 || bandedGrid_NSM_FL.FocusedRowHandle == 3)
                    {
                        if (Convert.ToDouble(e.Value) < 0)
                        {
                            e.Valid = false;
                            e.ErrorText = "Please Enter Positive Values";
                        }
                    }
                }
                else if (Grid_NSM_FR.IsFocused)
                {
                    if (bandedGrid_NSM_FR.FocusedRowHandle == 1 || bandedGrid_NSM_FR.FocusedRowHandle == 3)
                    {
                        if (Convert.ToDouble(e.Value) < 0)
                        {
                            e.Valid = false;
                            e.ErrorText = "Please Enter Positive Values";
                        }
                    }
                }
                else if (Grid_NSM_RL.IsFocused)
                {
                    if (bandedGrid_NSM_RL.FocusedRowHandle == 1 || bandedGrid_NSM_RL.FocusedRowHandle == 3)
                    {
                        if (Convert.ToDouble(e.Value) < 0)
                        {
                            e.Valid = false;
                            e.ErrorText = "Please Enter Positive Values";
                        }
                    }
                }
                else if (Grid_NSM_RR.IsFocused)
                {
                    if (bandedGrid_NSM_RR.FocusedRowHandle == 1 || bandedGrid_NSM_RR.FocusedRowHandle == 3)
                    {
                        if (Convert.ToDouble(e.Value) < 0)
                        {
                            e.Valid = false;
                            e.ErrorText = "Please Enter Positive Values";
                        }
                    }
                } 
            }

        }
        #endregion

        #region Cell Value Editor
        /// <summary>
        /// This method Updates all the properties of the LoadCaseGUI Class through the Update method and also calls the Update Method of the LoadCase Class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BandedGrid_LoadCase_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            UpdateLoadCase();

            int IndexOfLoadCase = this._LoadCaseID;
            LoadCase.List_LoadCases[IndexOfLoadCase].UpdateLoadCase(this);

        }
        #endregion

        #endregion

        #region Deserialization
        public LoadCaseGUI(SerializationInfo info, StreamingContext context)
        {
            #region General Variables
            _LoadCaseName = (string)info.GetValue("_LoadCaseName", typeof(string));
            _LoadCaseID = (int)info.GetValue("_LoadCaseID", typeof(int));
            _LoadCaseCounter = (int)info.GetValue("_LoadCaseCounter", typeof(int));
            CustomLoadCase = (bool)info.GetValue("CustomLoadCase", typeof(bool));
            #endregion

            #region Load Case Variables
            SM_Ax_GUI = (double)info.GetValue("SM_Ax_GUI", typeof(double));
            SM_Ay_GUI = (double)info.GetValue("SM_Ay_GUI", typeof(double));
            SM_Az_GUI = (double)info.GetValue("SM_Az_GUI", typeof(double));

            NSM_FL_Ax_GUI = (double)info.GetValue("NSM_FL_Ax_GUI", typeof(double));
            NSM_FL_Ay_GUI = (double)info.GetValue("NSM_FL_Ay_GUI", typeof(double));
            NSM_FL_Az_GUI = (double)info.GetValue("NSM_FL_Az_GUI", typeof(double));

            NSM_FR_Ax_GUI = (double)info.GetValue("NSM_FR_Ax_GUI", typeof(double));
            NSM_FR_Ay_GUI = (double)info.GetValue("NSM_FR_Ay_GUI", typeof(double));
            NSM_FR_Az_GUI = (double)info.GetValue("NSM_FR_Az_GUI", typeof(double));

            NSM_RL_Ax_GUI = (double)info.GetValue("NSM_RL_Ax_GUI", typeof(double));
            NSM_RL_Ay_GUI = (double)info.GetValue("NSM_RL_Ay_GUI", typeof(double));
            NSM_RL_Az_GUI = (double)info.GetValue("NSM_RL_Az_GUI", typeof(double));

            NSM_RR_Ax_GUI = (double)info.GetValue("NSM_RR_Ax_GUI", typeof(double));
            NSM_RR_Ay_GUI = (double)info.GetValue("NSM_RR_Ay_GUI", typeof(double));
            NSM_RR_Az_GUI = (double)info.GetValue("NSM_RR_Az_GUI", typeof(double));

            NSM_FL_LatGripDistribution_GUI = (double)info.GetValue("NSM_FL_LatGripDistribution_GUI", typeof(double));
            NSM_FL_LongGripDistribution_GUI = (double)info.GetValue("NSM_FL_LongGripDistribution_GUI", typeof(double));

            NSM_FR_LatGripDistribution_GUI = (double)info.GetValue("NSM_FR_LatGripDistribution_GUI", typeof(double));
            NSM_FR_LongGripDistribution_GUI = (double)info.GetValue("NSM_FR_LongGripDistribution_GUI", typeof(double));

            NSM_RL_LatGripDistribution_GUI = (double)info.GetValue("NSM_RL_LatGripDistribution_GUI", typeof(double));
            NSM_RL_LongGripDistribution_GUI = (double)info.GetValue("NSM_RL_LongGripDistribution_GUI", typeof(double));

            NSM_RR_LatGripDistribution_GUI = (double)info.GetValue("NSM_RR_LatGripDistribution_GUI", typeof(double));
            NSM_RR_LongGripDistribution_GUI = (double)info.GetValue("NSM_RR_LongGripDistribution_GUI", typeof(double));

            NSM_FL_Mx_GUI = (double)info.GetValue("NSM_FL_Mx_GUI", typeof(double));
            NSM_FL_Mz_GUI = (double)info.GetValue("NSM_FL_Mz_GUI", typeof(double));

            NSM_FR_Mx_GUI = (double)info.GetValue("NSM_FR_Mx_GUI", typeof(double));
            NSM_FR_Mz_GUI = (double)info.GetValue("NSM_FR_Mz_GUI", typeof(double));

            NSM_RL_Mx_GUI = (double)info.GetValue("NSM_RL_Mx_GUI", typeof(double));
            NSM_RL_Mz_GUI = (double)info.GetValue("NSM_RL_Mz_GUI", typeof(double));

            NSM_RR_Mx_GUI = (double)info.GetValue("NSM_RR_Mx_GUI", typeof(double));
            NSM_RR_Mz_GUI = (double)info.GetValue("NSM_RR_Mz_GUI", typeof(double));

            FL_BearingCoordinates_GUI = (double[,])info.GetValue("FL_BearingCoordinates_GUI", typeof(double[,]));
            FR_BearingCoordinates_GUI = (double[,])info.GetValue("FR_BearingCoordinates_GUI", typeof(double[,]));
            RL_BearingCoordinates_GUI = (double[,])info.GetValue("RL_BearingCoordinates_GUI", typeof(double[,]));
            RR_BearingCoordinates_GUI = (double[,])info.GetValue("RR_BearingCoordinates_GUI", typeof(double[,]));
            SteeringColumnBearing_GUI = (double[,])info.GetValue("SteeringColumnBearing_GUI", typeof(double[,]));

            #endregion

            #region Navidation Bar item
            navBarItemLoadCase = (CusNavBarItem)info.GetValue("navBarItemLoadCase", typeof(CusNavBarItem));
            #endregion

            #region Data Tables
            NSM_FL_DataTableGUI = (DataTable)info.GetValue("NSM_FL_DataTableGUI", typeof(DataTable));
            NSM_FR_DataTableGUI = (DataTable)info.GetValue("NSM_FR_DataTableGUI", typeof(DataTable));
            NSM_RL_DataTableGUI = (DataTable)info.GetValue("NSM_RL_DataTableGUI", typeof(DataTable));
            NSM_RR_DataTableGUI = (DataTable)info.GetValue("NSM_RR_DataTableGUI", typeof(DataTable));
            SuspendedMass_DataTableGUI = (DataTable)info.GetValue("SuspendedMass_DataTableGUI", typeof(DataTable));
            FL_Bearing_DataTable_GUI = (DataTable)info.GetValue("FL_Bearing_DataTable_GUI", typeof(DataTable));
            FR_Bearing_DataTable_GUI = (DataTable)info.GetValue("FR_Bearing_DataTable_GUI", typeof(DataTable));
            RL_Bearing_DataTable_GUI = (DataTable)info.GetValue("RL_Bearing_DataTable_GUI", typeof(DataTable));
            RR_Bearing_DataTable_GUI = (DataTable)info.GetValue("RR_Bearing_DataTable_GUI", typeof(DataTable));
            SteeringColumnBearing_DataTable_GUI = (DataTable)info.GetValue("SteeringColumnBearing_DataTable_GUI", typeof(DataTable));
            #endregion

            #region Tab Page
            TabPage_LC = (CustomXtraTabPage)info.GetValue("TabPage_LC", typeof(CustomXtraTabPage));
            #endregion
        }
        #endregion

        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            #region General Variables
            info.AddValue("_LoadCaseName", _LoadCaseName);
            info.AddValue("_LoadCaseID", _LoadCaseID);
            info.AddValue("_LoadCaseCounter", _LoadCaseCounter);
            info.AddValue("CustomLoadCase", CustomLoadCase);
            #endregion

            #region Load Case Variables
            info.AddValue("SM_Ax_GUI", SM_Ax_GUI);
            info.AddValue("SM_Ay_GUI", SM_Ay_GUI);
            info.AddValue("SM_Az_GUI", SM_Az_GUI);

            info.AddValue("NSM_FL_Ax_GUI", NSM_FL_Ax_GUI);
            info.AddValue("NSM_FL_Ay_GUI", NSM_FL_Ay_GUI);
            info.AddValue("NSM_FL_Az_GUI", NSM_FL_Az_GUI);

            info.AddValue("NSM_FR_Ax_GUI", NSM_FR_Ax_GUI);
            info.AddValue("NSM_FR_Ay_GUI", NSM_FR_Ay_GUI);
            info.AddValue("NSM_FR_Az_GUI", NSM_FR_Az_GUI);

            info.AddValue("NSM_RL_Ax_GUI", NSM_RL_Ax_GUI);
            info.AddValue("NSM_RL_Ay_GUI", NSM_RL_Ay_GUI);
            info.AddValue("NSM_RL_Az_GUI", NSM_RL_Az_GUI);

            info.AddValue("NSM_RR_Ax_GUI", NSM_RR_Ax_GUI);
            info.AddValue("NSM_RR_Ay_GUI", NSM_RR_Ay_GUI);
            info.AddValue("NSM_RR_Az_GUI", NSM_RR_Az_GUI);

            info.AddValue("NSM_FL_LatGripDistribution_GUI", NSM_FL_LatGripDistribution_GUI);
            info.AddValue("NSM_FL_LongGripDistribution_GUI", NSM_FL_LongGripDistribution_GUI);

            info.AddValue("NSM_FR_LatGripDistribution_GUI", NSM_FR_LatGripDistribution_GUI);
            info.AddValue("NSM_FR_LongGripDistribution_GUI", NSM_FR_LongGripDistribution_GUI);

            info.AddValue("NSM_RL_LatGripDistribution_GUI", NSM_RL_LatGripDistribution_GUI);
            info.AddValue("NSM_RL_LongGripDistribution_GUI", NSM_RL_LongGripDistribution_GUI);

            info.AddValue("NSM_RR_LatGripDistribution_GUI", NSM_RR_LatGripDistribution_GUI);
            info.AddValue("NSM_RR_LongGripDistribution_GUI", NSM_RR_LongGripDistribution_GUI);

            info.AddValue("NSM_FL_Mx_GUI", NSM_FL_Mx_GUI);
            info.AddValue("NSM_FL_Mz_GUI", NSM_FL_Mz_GUI);

            info.AddValue("NSM_FR_Mx_GUI", NSM_FR_Mx_GUI);
            info.AddValue("NSM_FR_Mz_GUI", NSM_FR_Mz_GUI);

            info.AddValue("NSM_RL_Mx_GUI", NSM_RL_Mx_GUI);
            info.AddValue("NSM_RL_Mz_GUI", NSM_RL_Mz_GUI);

            info.AddValue("NSM_RR_Mx_GUI", NSM_RR_Mx_GUI);
            info.AddValue("NSM_RR_Mz_GUI", NSM_RR_Mz_GUI);

            info.AddValue("FL_BearingCoordinates_GUI", FL_BearingCoordinates_GUI);
            info.AddValue("FR_BearingCoordinates_GUI", FR_BearingCoordinates_GUI);
            info.AddValue("RL_BearingCoordinates_GUI", RL_BearingCoordinates_GUI);
            info.AddValue("RR_BearingCoordinates_GUI", RR_BearingCoordinates_GUI);
            info.AddValue("SteeringColumnBearing_GUI", SteeringColumnBearing_GUI);

            #endregion

            #region Navigation Bar Item
            info.AddValue("navBarItemLoadCase", navBarItemLoadCase);
            #endregion

            #region Data Tables
            info.AddValue("NSM_FL_DataTableGUI", NSM_FL_DataTableGUI);
            info.AddValue("NSM_FR_DataTableGUI", NSM_FR_DataTableGUI);
            info.AddValue("NSM_RL_DataTableGUI", NSM_RL_DataTableGUI);
            info.AddValue("NSM_RR_DataTableGUI", NSM_RR_DataTableGUI);
            info.AddValue("SuspendedMass_DataTableGUI", SuspendedMass_DataTableGUI);
            info.AddValue("FL_Bearing_DataTable_GUI", FL_Bearing_DataTable_GUI);
            info.AddValue("FR_Bearing_DataTable_GUI", FR_Bearing_DataTable_GUI);
            info.AddValue("RL_Bearing_DataTable_GUI", RL_Bearing_DataTable_GUI);
            info.AddValue("RR_Bearing_DataTable_GUI", RR_Bearing_DataTable_GUI);
            info.AddValue("SteeringColumnBearing_DataTable_GUI", SteeringColumnBearing_DataTable_GUI);
            #endregion

            #region Tab Page
            info.AddValue("TabPage_LC", TabPage_LC);
            #endregion



        } 
        #endregion
    }
}

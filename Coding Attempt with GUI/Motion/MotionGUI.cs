using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using DevExpress.XtraNavBar;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class MotionGUI : ISerializable
    {
        #region Declrations
        public string _MotionGUIName { get; set; }
        public static int _MotionGUICounter = 0;
        public static List<MotionGUI> List_MotionGUI = new List<MotionGUI>();
        public CustomXtraTabPage TabPage_MotionGUI = new CustomXtraTabPage();
        public CustomBandedGridView bandedGridView_Motion = new CustomBandedGridView();

        public MotionChart motionGUI_MotionChart;

        public double[] ChartPoints_WheelDef_Y, ChartPoints_Steering_Y;
        public double[] ChartPoints_WheelDef_X, ChartPoints_Steering_X;

        public CusNavBarItem navBaritemMotionGUI;
        public Kinematics_Software_New r1;
        public bool DeflectionExists, SteeringExists;
        #endregion

        #region Constructor
        public MotionGUI(string _motionName, int _motionID, Kinematics_Software_New _r1)
        {
            r1 = _r1;
            motionGUI_MotionChart = new MotionChart(this);
            _MotionGUIName = _motionName + _motionID;
            navBaritemMotionGUI = new CusNavBarItem(_motionName, _motionID, this);
            TabPage_MotionGUI = CustomXtraTabPage.CreateNewTabPage_ForInputs(_motionName, _motionID);
            //TabPage_MotionGUI = CustomXtraTabPage.AddUserControlToTabPage(motionGUI_MotionChart, TabPage_MotionGUI, DockStyle.Fill);
        }
        #endregion

        #region GUI Operations
        public void HandleGUI(NavBarGroup _navBarGroup, NavBarControl _navBarControl, Kinematics_Software_New _r1, int Index)
        {
            TabPage_MotionGUI = CustomXtraTabPage.AddUserControlToTabPage(motionGUI_MotionChart, TabPage_MotionGUI, DockStyle.Fill);
            Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, List_MotionGUI[Index].TabPage_MotionGUI);
            Kinematics_Software_New.TabControl_Outputs.SelectedTabPage = List_MotionGUI[Index].TabPage_MotionGUI;

            List_MotionGUI[Index].navBaritemMotionGUI.CreateNavBarItem(List_MotionGUI[Index].navBaritemMotionGUI, _navBarGroup, _navBarControl);

            List_MotionGUI[Index].navBaritemMotionGUI = List_MotionGUI[Index].LinkClickedEventCreater(List_MotionGUI[Index].navBaritemMotionGUI, _r1);

        }
        #endregion

        #region NavBarItem Event Operations

        #region NavBarItem LinkClicked Event Creater
        public CusNavBarItem LinkClickedEventCreater(CusNavBarItem _navBaritemMotion, Kinematics_Software_New _r1)
        {
            _navBaritemMotion.LinkClicked += _navBaritemMotion_LinkClicked;
            r1 = _r1;

            return _navBaritemMotion;
        }
        #endregion

        #region NavBarItem Link Clicked Event
        private void _navBaritemMotion_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int index = 0, SelectedPage = 0;

            index = r1.navBarGroupMotion.SelectedLinkIndex;
            List_MotionGUI[index].TabPage_MotionGUI.PageVisible = true;
            SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(List_MotionGUI[index].TabPage_MotionGUI);
            Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
            Kinematics_Software_New.TabControl_Outputs.TabPages[SelectedPage].PageVisible = true;
        }
        #endregion

        #endregion

        #region GridView Operations
        public void InitializeGridControl_MotionView(int Index, Kinematics_Software_New _r1)
        {
            try
            {
                r1 = _r1;
                int guiIndex = Vehicle.List_Vehicle[Index].vehicle_Motion.MotionID - 1;
                MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion = CustomBandedGridView.CreateNewBandedGridView(guiIndex, 3, "Motion View");
                _r1.gridControl2.DataSource = Vehicle.List_Vehicle[Index].vehicle_Motion.Motion_DataTable;
                _r1.gridControl2.MainView = MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion;
                MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion.FocusedRowChanged += BandedGridView_Motion_FocusedRowChanged;
                MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion.OptionsNavigation.EnterMoveNextColumn = true;
                MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion.OptionsNavigation.AutoMoveRowFocus = true;
                MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion.OptionsNavigation.AutoFocusNewRow = true;
                MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion = CustomBandedGridColumn.ColumnEditor_ForMotion(MotionGUI.List_MotionGUI[guiIndex].bandedGridView_Motion, _r1);
            }
            catch (Exception)
            {

                // If I open a project which has no motion but has a vehicle then this function will be called on account of the fact that the there exists a Vehicle. At that point, this method will fail because the guiIndex will be -1. SO to allow the remaining
                // open function to continue, this TryCatch block exists
            }
        }

        public void BandedGridView_Motion_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (!r1.IsBeingOpened)
                {
                    int outputIndex = 0, indexOfVehicle = 0;
                    if (e.FocusedRowHandle >= 0)
                    {
                        outputIndex = e.FocusedRowHandle;
                    }
                    
                    r1.FindOutPutIndex(outputIndex);

                    foreach (NavBarGroup item in r1.navBarControlResults.Groups)
                    {
                        for (int i_Group = 0; i_Group < Kinematics_Software_New.M1_Global.vehicleGUI.Count; i_Group++)
                        {
                            if (r1.navBarControlResults.ActiveGroup.Name == Kinematics_Software_New.M1_Global.vehicleGUI[i_Group].navBarGroup_Vehicle_Result.Name)
                            {
                                indexOfVehicle = /*r1.navBarControlResults.Groups.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[i_Group].navBarGroup_Vehicle_Result)*/ i_Group;
                                goto A;
                            }
                        }
                    }
                    A:
                    r1.PopulateOutputDataTable(Vehicle.List_Vehicle[indexOfVehicle]);

                    r1.DisplayOutputs(Vehicle.List_Vehicle[indexOfVehicle]);

                    Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlDataSource(Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlOutputs_SCFL, Vehicle.List_Vehicle[indexOfVehicle].oc_FL[outputIndex].OC_SC_DataTable, Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].ocGUI_FL.bandedGridView_Outputs, r1);
                    Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlDataSource(Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlOutputs_SCFR, Vehicle.List_Vehicle[indexOfVehicle].oc_FR[outputIndex].OC_SC_DataTable, Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].ocGUI_FR.bandedGridView_Outputs, r1);
                    Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlDataSource(Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlOutputs_SCRL, Vehicle.List_Vehicle[indexOfVehicle].oc_RL[outputIndex].OC_SC_DataTable, Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].ocGUI_RL.bandedGridView_Outputs, r1);
                    Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlDataSource(Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].GridControlOutputs_SCRR, Vehicle.List_Vehicle[indexOfVehicle].oc_RR[outputIndex].OC_SC_DataTable, Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].ocGUI_RR.bandedGridView_Outputs, r1);

                    r1.PopulateInputSheet(Vehicle.List_Vehicle[indexOfVehicle]);

                    Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].EditORCreateVehicleCAD(Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].CADVehicleOutputs, indexOfVehicle, false, true, outputIndex, true,
                        Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].CadIsTobeImported, Kinematics_Software_New.M1_Global.vehicleGUI[indexOfVehicle].PlotWheel); 
                }


            }
            catch (Exception)
            {

                // Encountered a System.OutOfIndex Exception. 
            }

        }
        #endregion

        #region Method to Handle the Creation or Editing of the Chart of the Motion
        public void MotionCreateOrEdit(bool IsControlInitialized, bool _deflectionExists, bool _steeringExists)
        {
            int index = r1.navBarGroupMotion.SelectedLinkIndex;
            DeflectionExists = _deflectionExists;
            SteeringExists = _steeringExists;

            Motion.List_Motion[index].GetWheelDeflectionAndSteer(List_MotionGUI[index], IsControlInitialized, DeflectionExists, SteeringExists);
            ChartPoints_WheelDef_X = Motion.List_Motion[index].ChartPoints_WheelDef_X;
            ChartPoints_WheelDef_Y = Motion.List_Motion[index].ChartPoints_WheelDef_Y;
            ChartPoints_Steering_X = Motion.List_Motion[index].ChartPoints_Steering_X;
            ChartPoints_Steering_Y = Motion.List_Motion[index].ChartPoints_Steering_Y;


            Kinematics_Software_New.comboBoxMotionOperations_Invoker();
        }
        #endregion

        #region De-Serialization of the MotionGUI Class
        public MotionGUI(SerializationInfo info, StreamingContext context)
        {
            _MotionGUIName = (string)info.GetValue("_MotionGUIName", typeof(string));
            _MotionGUICounter = (int)info.GetValue("_MotionGUICounter", typeof(int));

            ChartPoints_WheelDef_X = (double[])info.GetValue("ChartPoints_WheelDef_X", typeof(double[]));
            ChartPoints_WheelDef_Y = (double[])info.GetValue("ChartPoints_WheelDef_Y", typeof(double[]));
            ChartPoints_Steering_X = (double[])info.GetValue("ChartPoints_Steering_X", typeof(double[]));
            ChartPoints_Steering_Y = (double[])info.GetValue("ChartPoints_Steering_Y", typeof(double[]));

            List_MotionGUI = (List<MotionGUI>)info.GetValue("List_MotionGUI", typeof(List<MotionGUI>));

            TabPage_MotionGUI = (CustomXtraTabPage)info.GetValue("TabPage_MotionGUI", typeof(CustomXtraTabPage));
            navBaritemMotionGUI = (CusNavBarItem)info.GetValue("navBarItemMotionGUI", typeof(CusNavBarItem));

            DeflectionExists = (bool)info.GetValue("DeflectionExists", typeof(bool));
            SteeringExists = (bool)info.GetValue("SteeringExists", typeof(bool));

            motionGUI_MotionChart = new MotionChart(this);
        } 
        #endregion

        public MotionGUI()
        {
        }

        #region Serialization of the MotionGUI Class
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_MotionGUIName", _MotionGUIName);
            info.AddValue("_MotionGUICounter", _MotionGUICounter);

            info.AddValue("ChartPoints_WheelDef_X", ChartPoints_WheelDef_X);
            info.AddValue("ChartPoints_WheelDef_Y", ChartPoints_WheelDef_Y);
            info.AddValue("ChartPoints_Steering_X", ChartPoints_Steering_X);
            info.AddValue("ChartPoints_Steering_Y", ChartPoints_Steering_Y);

            info.AddValue("List_MotionGUI", List_MotionGUI);

            info.AddValue("TabPage_MotionGUI", TabPage_MotionGUI);
            info.AddValue("navBarItemMotionGUI", navBaritemMotionGUI);

            info.AddValue("DeflectionExists", DeflectionExists);
            info.AddValue("SteeringExists", SteeringExists);
        } 
        #endregion
    }
}

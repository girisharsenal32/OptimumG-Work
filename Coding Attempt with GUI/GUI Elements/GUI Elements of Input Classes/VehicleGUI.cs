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
using MathNet.Spatial.Euclidean;
using devDept.Eyeshot.Entities;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class VehicleGUI : ISerializable
    {
        static Kinematics_Software_New r1;

        #region Check Variables
        public int AssemblyChecker_GUI { get; set; }
        public bool SuspensionIsAssembled_GUI = false;
        public bool TireIsAssembled_GUI = false;
        public bool SpringIsAssembled_GUI = false;
        public bool DamperIsAssembled_GUI = false;
        public bool ARBIsAssembled_GUI = false;
        public bool ChassisIsAssembled_GUI = false;
        public bool WAIsAssembled_GUI = false;
        public bool VehicleHasBeenValidated_GUI = false;
        #endregion

        #region Coordinates of Vehicle Origin with respect to User Coordinates System for Input and Output
        public string _VehicleGUIName { get; set; }
        public int _VehicleID { get; set; }

        public double _OutputOriginX { get; set; }
        public double _OutputOriginY { get; set; }
        public double _OutputOriginZ { get; set; }
        #endregion

        public bool Vehicle_MotionExists { get; set; }
        public int IndexOfOutput { get; set; }

        #region List of NavBaritem for the Results
        public List<CusNavBarItem> navBarItem_Vehicle_Results = new List<CusNavBarItem>();
        #endregion

        #region navBarGroupVehicleResult object
        public CusNavBarGroup navBarGroup_Vehicle_Result = new CusNavBarGroup();
        #endregion

        #region Vehicle Tab Pages for Output Display
        public List<CustomXtraTabPage> TabPages_Vehicle = new List<CustomXtraTabPage>();
        public CustomXtraTabPage TabPage_VehicleInputCAD;
        #endregion

        #region Vehicle Grid Controls
        public GridControl GridControlOutputs_SCFL = new GridControl();
        public GridControl GridControlOutputs_SCFR = new GridControl();
        public GridControl GridControlOutputs_SCRL = new GridControl();
        public GridControl GridControlOutputs_SCRR = new GridControl();
        #endregion

        #region Vehicle User Controls
        public XtraUserControl_WishboneForces WF = new XtraUserControl_WishboneForces();
        public XtraUserControl_CW_Def_WA CW_Def_WA = new XtraUserControl_CW_Def_WA();
        public XtraUserControl_LinkLengths LL = new XtraUserControl_LinkLengths();
        public XtraUserControl_VehicleOutputs VO = new XtraUserControl_VehicleOutputs();
        public XtraUserControl_InputSheet IS = new XtraUserControl_InputSheet(r1);
        public CAD CADVehicleInputs/* = new CAD()*/;
        public CAD CADVehicleOutputs/* = new CAD()*/;
        public LegendEditor LoadCaseLegend = new LegendEditor();
        #endregion

        #region ImportCAD Form
        public XUC_ImportCAD importCADForm/* = new XUC_ImportCAD()*/;
        public bool ImportCADFormInvoked = false;
        public bool PlotWheel = true;
        #endregion

        #region Vehicle Scrollable Controls
        public XtraScrollableControl xtraScrollableControl_OutputCoordinates = new XtraScrollableControl();
        #endregion

        #region OutputClass GUI 
        public OutputClassGUI ocGUI_FL, ocGUI_FR, ocGUI_RL, ocGUI_RR;
        #endregion

        public bool CadIsTobeImported = false;
        public VehicleVisualizationType VisualizationType { get; set; }
        public bool FileHasBeenImported = false;
        public bool OutputIGESPlotted = false;
        //public ReadFileAsync ImportedFile;
        public string IGESFIleName;
        public bool TranslateChassisToGround = false;

        /// <summary>
        /// <para>List of translations. <see cref="List{T}.Count-1"/> index is the translation which is to be done and <see cref="List{T}.Count-2"/> is the last translation which was done.</para>
        /// <para>these both are subtracted to the delta which is then the amount by which the imported CAD iis translated by</para>
        /// </summary>
        public List<double> ImportedCADTranslationHistory = new List<double>(new double[] { 0, 0 });

        public ProgressBarSerialization ProgressBarVehicleGUI;

        public VehicleGUI() { }// This constructor is created here only so that the Vehicle object can be initialized without having to pass any arguments.
                               // This is needed because otherwise the Vehicle object will not be instantiated untill a Vehicle item is created. 
                               // If the user wants to save the file without creating a Vehicle object he will not be able to do so unless this constructor is used to create the Vehicle object 


        public VehicleGUI(Kinematics_Software_New _r1, VehicleVisualizationType _vVisualizationType)
        {

            r1 = _r1;
            IS.Kinematics_Software_New_ObjectInitializer(r1);
            ProgressBarVehicleGUI = r1.progressBar;
            //Vehicle_MotionExists = Vehicle.List_Vehicle[Vehicle.VehicleCounter].sc_FL.SuspensionMotionExists;
            IndexOfOutput = 0;

            ///<summary>Constructing the <see cref="CAD"/> usercontrol here to prevent overcrowding the memory by initializing the controls in the declaration itself</summary>
            if (_vVisualizationType == VehicleVisualizationType.Generic)
            {
                CADVehicleInputs = new CAD();
                CadIsTobeImported = false;
                VisualizationType = VehicleVisualizationType.Generic;
            }
            else if (_vVisualizationType == VehicleVisualizationType.ImportedCAD)
            {
                importCADForm = new XUC_ImportCAD();
                CadIsTobeImported = true;
                VisualizationType = VehicleVisualizationType.ImportedCAD;
            }

            ocGUI_FL = new OutputClassGUI();
            ocGUI_FR = new OutputClassGUI();
            ocGUI_RL = new OutputClassGUI();
            ocGUI_RR = new OutputClassGUI();

        }

        public void Kinematics_Software_New_ObjectInitializer(Kinematics_Software_New _r1)
        {
            //
            // Seperate function added here because there is a need to initialize the Main form's variable inside the Input Sheet
            IS.Kinematics_Software_New_ObjectInitializer(r1);
            r1 = _r1;
        }

        #region Function to display the selected items of the Vehicle
        public static void DisplayVehicleItem(Vehicle _vehicle)
        {
            ///<summary>
            ///The IF Loop is necessary to ensure that the input items are not accessed when they are null. 
            ///This is necessary because now the Vehicle can be created without first creating all the Input Items.
            /// </summary>

            if (_vehicle.tire_FL != null && _vehicle.tire_FR != null && _vehicle.tire_RL != null && _vehicle.tire_RR != null )
            {
                r1.comboBoxTireFL.Text = _vehicle.tire_FL._TireName;
                r1.comboBoxTireFR.Text = _vehicle.tire_FR._TireName;
                r1.comboBoxTireRL.Text = _vehicle.tire_RL._TireName;
                r1.comboBoxTireRR.Text = _vehicle.tire_RR._TireName; 
            }

            if (_vehicle.spring_FL != null && _vehicle.spring_FR != null && _vehicle.spring_RL != null && _vehicle.spring_RR != null)
            {
                r1.comboBoxSpringFL.Text = _vehicle.spring_FL._SpringName;
                r1.comboBoxSpringFR.Text = _vehicle.spring_FR._SpringName;
                r1.comboBoxSpringRL.Text = _vehicle.spring_RL._SpringName;
                r1.comboBoxSpringRR.Text = _vehicle.spring_RR._SpringName; 
            }

            if (_vehicle.damper_FL != null && _vehicle.damper_FR != null && _vehicle.damper_RL != null && _vehicle.damper_RR != null)
            {
                r1.comboBoxDamperFL.Text = _vehicle.damper_FL._DamperName;
                r1.comboBoxDamperFR.Text = _vehicle.damper_FR._DamperName;
                r1.comboBoxDamperRL.Text = _vehicle.damper_RL._DamperName;
                r1.comboBoxDamperRR.Text = _vehicle.damper_RR._DamperName; 
            }

            r1.comboBoxARBFront.Text = _vehicle.arb_FL._ARBName;
            r1.comboBoxARBRear.Text = _vehicle.arb_FR._ARBName;

            if (_vehicle.chassis_vehicle != null)
            {
                r1.comboBoxChassis.Text = _vehicle.chassis_vehicle._ChassisName; 
            }

            if (_vehicle.wa_FL != null && _vehicle.wa_FR != null && _vehicle.wa_RL != null && _vehicle.wa_RR != null)
            {
                r1.comboBoxWAFL.Text = _vehicle.wa_FL._WAName;
                r1.comboBoxWAFR.Text = _vehicle.wa_FR._WAName;
                r1.comboBoxWARL.Text = _vehicle.wa_RL._WAName;
                r1.comboBoxWARR.Text = _vehicle.wa_RR._WAName; 
            }

            if (_vehicle.sc_FL != null && _vehicle.sc_FR != null && _vehicle.sc_RL != null && _vehicle.sc_RR != null)
            {
                r1.comboBoxSCFL.Text = _vehicle.sc_FL._SCName;
                r1.comboBoxSCFR.Text = _vehicle.sc_FR._SCName;
                r1.comboBoxSCRL.Text = _vehicle.sc_RL._SCName;
                r1.comboBoxSCRR.Text = _vehicle.sc_RR._SCName; 
            }
        }
        #endregion

        #region Method to Create Tabpages 
        public List<CustomXtraTabPage> CreateTabPages_For_Vehicle_Outputs(List<CustomXtraTabPage> _xtraTabPageList, Kinematics_Software_New _R1, int _VehicleID)
        {
            _xtraTabPageList = new List<CustomXtraTabPage>();

            CustomXtraTabPage TabPage_InputSheet;
            TabPage_InputSheet = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("Input Sheet- ", _VehicleID);

            CustomXtraTabPage TabPage_SuspensionCoordinatesOutput;
            TabPage_SuspensionCoordinatesOutput = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("Suspension Coordinates- ", _VehicleID);

            CustomXtraTabPage TabPage_WishboneForces;
            TabPage_WishboneForces = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("Wishbone Forces- ", _VehicleID);

            CustomXtraTabPage TabPage_CornerWeights_Deflections_WA;
            TabPage_CornerWeights_Deflections_WA = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("Corner Weights, Deflections and Wheel Alignment- ", _VehicleID);

            CustomXtraTabPage TabPage_VehicleOutputs;
            TabPage_VehicleOutputs = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("Vehicle Outputs- ", _VehicleID);

            CustomXtraTabPage TabPage_LinkLengths;
            TabPage_LinkLengths = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("Link Lengths- ", _VehicleID);

            CustomXtraTabPage TabPage_OutputCAD;
            TabPage_OutputCAD = CustomXtraTabPage.CreateNewTabPage_ForVehicleOutputs("CAD- ", _VehicleID);

            AddSuspensionGridtoScrollableControl();

            _xtraTabPageList.Insert(0, TabPage_InputSheet);
            _xtraTabPageList[0].PageVisible = false;

            _xtraTabPageList.Insert(1, TabPage_SuspensionCoordinatesOutput);
            _xtraTabPageList[1].PageVisible = false;

            _xtraTabPageList.Insert(2, TabPage_WishboneForces);
            _xtraTabPageList[2].PageVisible = false;

            _xtraTabPageList.Insert(3, TabPage_CornerWeights_Deflections_WA);
            _xtraTabPageList[3].PageVisible = false;

            _xtraTabPageList.Insert(4, TabPage_VehicleOutputs);
            _xtraTabPageList[4].PageVisible = false;

            _xtraTabPageList.Insert(5, TabPage_LinkLengths);
            _xtraTabPageList[5].PageVisible = false;

            _xtraTabPageList.Insert(6, TabPage_OutputCAD);
            _xtraTabPageList[6].PageVisible = false;

            AddUserControlToTabPage(_xtraTabPageList);

            return _xtraTabPageList;

        }
        #endregion

        #region Method to add the Suspension Grid Control to the Scrollable Control 
        public void AddSuspensionGridtoScrollableControl()
        {
            xtraScrollableControl_OutputCoordinates.SendToBack();
            xtraScrollableControl_OutputCoordinates.Dock = DockStyle.Fill;

            xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_SCFL);
            GridControlOutputs_SCFL.Dock = DockStyle.Left;
            GridControlOutputs_SCFL.Width = 300;

            xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_SCFR);
            GridControlOutputs_SCFR.Dock = DockStyle.Left;
            GridControlOutputs_SCFR.Width = 300;

            xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_SCRL);
            GridControlOutputs_SCRL.Dock = DockStyle.Left;
            GridControlOutputs_SCRL.Width = 300;

            xtraScrollableControl_OutputCoordinates.Controls.Add(GridControlOutputs_SCRR);
            GridControlOutputs_SCRR.Dock = DockStyle.Left;
            GridControlOutputs_SCRR.Width = 300;

            GridControlOutputs_SCRR.SendToBack();
            GridControlOutputs_SCRL.SendToBack();
            GridControlOutputs_SCFR.SendToBack();
            GridControlOutputs_SCFL.SendToBack();
        }
        #endregion

        #region Method to add the remaining User Controls to the TabPages
        public void AddUserControlToTabPage(List<CustomXtraTabPage> _xtraTabPageList)
        {
            IS.Dock = DockStyle.Fill;

            WF.Dock = DockStyle.Fill;

            CW_Def_WA.Dock = DockStyle.Fill;

            VO.Dock = DockStyle.Fill;

            LL.Dock = DockStyle.Fill;

            CADVehicleOutputs.Dock = DockStyle.Fill;

            _xtraTabPageList[0].Controls.Add(IS);
            _xtraTabPageList[1].Controls.Add(xtraScrollableControl_OutputCoordinates);
            _xtraTabPageList[2].Controls.Add(WF);
            _xtraTabPageList[3].Controls.Add(CW_Def_WA);
            _xtraTabPageList[4].Controls.Add(VO);
            _xtraTabPageList[5].Controls.Add(LL);
            _xtraTabPageList[6].Controls.Add(CADVehicleOutputs);

        }
        #endregion

        #region Grid Control Operations

        public void GridControlDataSource(GridControl _gridControl, DataTable _dataTable,CustomBandedGridView _bandedGridview,Kinematics_Software_New _R1)
        {
            _gridControl.DataSource = _dataTable;
            _bandedGridview = CustomBandedGridColumn.ColumnEditor_ForSuspension(_bandedGridview, _R1);
        }

        #region Method to Populate the GridControl
        public void PopulateSuspensionGridControl(Kinematics_Software_New _R1, VehicleGUI _vehicleGUI, Vehicle _vehicleGridControlPopulation,int OutputIndex)
        {

            _vehicleGUI.ocGUI_FL.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Front Left Suspension Coordinates");
            _vehicleGUI.GridControlOutputs_SCFL.BindingContext = new BindingContext();
            _vehicleGUI.GridControlOutputs_SCFL.DataSource = null;
            _vehicleGUI.GridControlOutputs_SCFL.ForceInitialize();
            _vehicleGUI.GridControlOutputs_SCFL.MainView = _vehicleGUI.ocGUI_FL.bandedGridView_Outputs;
            GridControlDataSource(_vehicleGUI.GridControlOutputs_SCFL, _vehicleGridControlPopulation.oc_FL[OutputIndex].OC_SC_DataTable, _vehicleGUI.ocGUI_FL.bandedGridView_Outputs,_R1);
            
            _vehicleGUI.ocGUI_FR.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Front Right Suspension Coordinates");
            _vehicleGUI.GridControlOutputs_SCFR.BindingContext = new BindingContext();
            _vehicleGUI.GridControlOutputs_SCFR.DataSource = null;
            _vehicleGUI.GridControlOutputs_SCFR.ForceInitialize();
            _vehicleGUI.GridControlOutputs_SCFR.MainView = _vehicleGUI.ocGUI_FR.bandedGridView_Outputs;
            GridControlDataSource(_vehicleGUI.GridControlOutputs_SCFR, _vehicleGridControlPopulation.oc_FR[OutputIndex].OC_SC_DataTable, _vehicleGUI.ocGUI_FR.bandedGridView_Outputs, _R1);
            //_vehicleGUI.ocGUI_FR.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(_vehicleGUI.ocGUI_FR.bandedGridView_Outputs, _R1);

            _vehicleGUI.ocGUI_RL.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Rear Left Suspension Coordinates");
            _vehicleGUI.GridControlOutputs_SCRL.BindingContext = new BindingContext();
            _vehicleGUI.GridControlOutputs_SCRL.DataSource = null;
            _vehicleGUI.GridControlOutputs_SCRL.ForceInitialize();
            _vehicleGUI.GridControlOutputs_SCRL.MainView = _vehicleGUI.ocGUI_RL.bandedGridView_Outputs;
            GridControlDataSource(_vehicleGUI.GridControlOutputs_SCRL, _vehicleGridControlPopulation.oc_RL[OutputIndex].OC_SC_DataTable, _vehicleGUI.ocGUI_RL.bandedGridView_Outputs, _R1);
            //_vehicleGUI.ocGUI_RL.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(_vehicleGUI.ocGUI_RL.bandedGridView_Outputs, _R1);

            _vehicleGUI.ocGUI_RR.bandedGridView_Outputs = CustomBandedGridView.CreateNewBandedGridView(1, 4, "Rear Right Suspension Coordinates");
            _vehicleGUI.GridControlOutputs_SCRR.BindingContext = new BindingContext();
            _vehicleGUI.GridControlOutputs_SCRR.DataSource = null;
            _vehicleGUI.GridControlOutputs_SCRR.ForceInitialize();
            _vehicleGUI.GridControlOutputs_SCRR.MainView = _vehicleGUI.ocGUI_RR.bandedGridView_Outputs;
            GridControlDataSource(_vehicleGUI.GridControlOutputs_SCRR, _vehicleGridControlPopulation.oc_RR[OutputIndex].OC_SC_DataTable, _vehicleGUI.ocGUI_RR.bandedGridView_Outputs, _R1);
            //_vehicleGUI.ocGUI_RR.bandedGridView_Outputs = CustomBandedGridColumn.ColumnEditor_ForSuspension(_vehicleGUI.ocGUI_RR.bandedGridView_Outputs, _R1);

        }
        #endregion

        #region Method to reset the GridCOntorl
        public void ResetGridControl(VehicleGUI _vehicleGUI)
        {
            _vehicleGUI.GridControlOutputs_SCFL = new GridControl();
            _vehicleGUI.GridControlOutputs_SCFR = new GridControl();
            _vehicleGUI.GridControlOutputs_SCRL = new GridControl();
            _vehicleGUI.GridControlOutputs_SCRR = new GridControl();
        }
        #endregion

        #endregion

        #region Method to Create the CAD of the Vehicle

        #region Preprocessing operations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_vehicleGUI"></param>
        /// <param name="Index"></param>
        /// <param name="IsRecreated"></param>
        /// <param name="_ImportCAD"></param>
        public void VehicleCADPreProcessor(VehicleGUI _vehicleGUI, int Index, bool IsRecreated, bool _ImportCAD, bool _SuspensionIsCreated)
        {
            try
            {
                if (!IsRecreated) _vehicleGUI.TabPage_VehicleInputCAD = CustomXtraTabPage.CreateNewTabPage_ForInputs("Vehicle ", Vehicle.List_Vehicle[Index].VehicleID);

                //_vehicleGUI.CADVehicleInputs = new CAD();
                _vehicleGUI.CadIsTobeImported = _ImportCAD;
                if (_ImportCAD == true)
                {
                    _vehicleGUI.TabPage_VehicleInputCAD.Controls.Add(_vehicleGUI.importCADForm);
                    _vehicleGUI.importCADForm.Dock = DockStyle.Fill;
                }
                else
                {
                    _vehicleGUI.TabPage_VehicleInputCAD.Controls.Add(_vehicleGUI.CADVehicleInputs);
                    _vehicleGUI.CADVehicleInputs.Dock = DockStyle.Fill;
                }
                Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, _vehicleGUI.TabPage_VehicleInputCAD);
                
                if (_SuspensionIsCreated)
                {
                    EditORCreateVehicleCAD(_vehicleGUI.CADVehicleInputs, Vehicle.List_Vehicle[Index].VehicleID - 1, true, _vehicleGUI.Vehicle_MotionExists, 0, false, CadIsTobeImported, PlotWheel); 
                }
                _vehicleGUI.CADVehicleInputs.SetupViewPort();
                _vehicleGUI.CADVehicleInputs.Visible = true;
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPage = _vehicleGUI.TabPage_VehicleInputCAD;

            }
            catch (Exception E)
            {
                string error = E.Message;
                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Vehicle or VehicleGUI item being present, then the software won't crash
            }

        }
        #endregion

        #region Method to Create or Edit the CAD
        public void EditORCreateVehicleCAD(CAD vehicleCADDrawer, int Index, bool IsInput, bool _MotionExists, int _IndexOfOutput, bool IsCreated, bool _importCAD, bool _PlotWheel)
        {
            try
            {
                IndexOfOutput = _IndexOfOutput;


                if (IsInput)
                {

                    if (_importCAD == true)
                    {
                        importCADForm.importCADViewport.ClearViewPort(CadIsTobeImported, FileHasBeenImported, Kinematics_Software_New.M1_Global.vehicleGUI[Index].importCADForm.importCADViewport.igesEntities);
                        importCADForm.importCADViewport.InitializeLayers();
                        InputDrawer(importCADForm.importCADViewport, Index, _MotionExists, _importCAD, _PlotWheel);

                    }
                    else
                    {
                        vehicleCADDrawer.ClearViewPort(CadIsTobeImported, FileHasBeenImported, null);
                        vehicleCADDrawer.InitializeLayers();
                        InputDrawer(vehicleCADDrawer, Index, _MotionExists, _importCAD, _PlotWheel);

                    }
                }
                else if (!IsInput)
                {
                    //vehicleCADDrawer.InitializeEntities();
                    //vehicleCADDrawer.CloneOutputViewPort(vehicleCADDrawer.viewportLayout1,);
                    vehicleCADDrawer.ClearViewPort(CadIsTobeImported, FileHasBeenImported, null);
                    OutputDrawer(vehicleCADDrawer, Index, _IndexOfOutput, _importCAD, _PlotWheel);
                    if (!IsCreated)
                    {
                        vehicleCADDrawer.SetupViewPort();
                    }
                }

                vehicleCADDrawer.RefreshViewPort();
            }
            catch (Exception E)
            {
                string error = E.Message;
                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Vehicle or VehicleGUI item being present, then the software won't crasha

            }
        }
        #endregion

        #region Inpput Coordinates Plotter
        /// <summary>
        /// Gets the Contact Patch Forces calculated using the Load Case. IF the load case is not null
        /// </summary>
        /// <param name="_loadCase"></param>
        /// <param name="_forceX"></param>
        /// <param name="_forceY"></param>
        /// <param name="_forceZ"></param>
        /// <param name="_identifier"></param>
        private void GetCPForce(LoadCase _loadCase, out double _forceX, out double _forceY, out double _forceZ, int _identifier)
        {
            _forceX = _forceY = _forceZ = 0;

            if (_loadCase != null)
            {
                if (_identifier == 1)
                {
                    _forceX = _loadCase.TotalLoad_FL_Fx;
                    _forceY = _loadCase.TotalLoad_FL_Fy;
                    _forceZ = _loadCase.TotalLoad_FL_Fz;
                }
                else if (_identifier == 2)
                {
                    _forceX = _loadCase.TotalLoad_FR_Fx;
                    _forceY = _loadCase.TotalLoad_FR_Fy;
                    _forceZ = _loadCase.TotalLoad_FR_Fz;
                }
                else if (_identifier == 3)
                {
                    _forceX = _loadCase.TotalLoad_RL_Fx;
                    _forceY = _loadCase.TotalLoad_RL_Fy;
                    _forceZ = _loadCase.TotalLoad_RL_Fz;
                }
                else if (_identifier == 4)
                {
                    _forceX = _loadCase.TotalLoad_RR_Fx;
                    _forceY = _loadCase.TotalLoad_RR_Fy;
                    _forceZ = _loadCase.TotalLoad_RR_Fz;
                } 
            }

        }

        private void InputDrawer(CAD vehicleCADDrawer_Input, int Index_Input, bool _motionExists, bool _importCAD,bool _plotWheel)
        {
            try
            {
                double CPForceX = 0, CPForceY = 0, CPForceZ = 0;

                ///<remarks>
                ///The If Loop is needed becsuse now the Vehicle can be created without the creation of the Input items.
                ///It may happen that the Suspension is not created but the <see cref="ImportCADForm.simpleButtonBrowse_Click(object, EventArgs)"/> is fired. 
                ///In this case, the if method (<see cref="Kinematics_Software_New.EditVehicleCAD(CAD, int, bool, bool, bool)"/>) is called then it will fail because there is no Suspension Item created and hence <see cref="VehicleGUI.InputDrawer(CAD, int, bool, bool, bool)"/>
                ///will fail because there is no Suspension. 
                ///But, the <see cref="CAD.ImportCAD(ref bool, ref bool)"/> method must be executed becase when the see cref="ImportCADForm.simpleButtonBrowse_Click(object, EventArgs)"/> is fired an Import must have heppened (Check is in place to ensure Import has happened)
                /// </remarks>
                if (Vehicle.List_Vehicle[Index_Input].sc_FL != null)
                {
                    vehicleCADDrawer_Input.GetCoG(Vehicle.List_Vehicle[Index_Input].chassis_vehicle);

                    GetCPForce(Vehicle.List_Vehicle[Index_Input].vehicleLoadCase, out CPForceX, out CPForceY, out CPForceZ, 1);
                    vehicleCADDrawer_Input.SuspensionPlotterInvoker(Vehicle.List_Vehicle[Index_Input].sc_FL, 1, Vehicle.List_Vehicle[Index_Input].wa_FL, true, _plotWheel, null, CPForceX, CPForceY, CPForceZ);

                    GetCPForce(Vehicle.List_Vehicle[Index_Input].vehicleLoadCase, out CPForceX, out CPForceY, out CPForceZ, 2);
                    vehicleCADDrawer_Input.SuspensionPlotterInvoker(Vehicle.List_Vehicle[Index_Input].sc_FR, 2, Vehicle.List_Vehicle[Index_Input].wa_FR, true, _plotWheel, null, CPForceX, CPForceY, CPForceZ);

                    GetCPForce(Vehicle.List_Vehicle[Index_Input].vehicleLoadCase, out CPForceX, out CPForceY, out CPForceZ, 3);
                    vehicleCADDrawer_Input.SuspensionPlotterInvoker(Vehicle.List_Vehicle[Index_Input].sc_RL, 3, Vehicle.List_Vehicle[Index_Input].wa_RL, true, _plotWheel, null, CPForceX, CPForceY, CPForceZ);

                    GetCPForce(Vehicle.List_Vehicle[Index_Input].vehicleLoadCase, out CPForceX, out CPForceY, out CPForceZ, 4);
                    vehicleCADDrawer_Input.SuspensionPlotterInvoker(Vehicle.List_Vehicle[Index_Input].sc_RR, 4, Vehicle.List_Vehicle[Index_Input].wa_RR, true, _plotWheel, null, CPForceX, CPForceY, CPForceZ);


                    vehicleCADDrawer_Input.ARBConnector(vehicleCADDrawer_Input.CoordinatesFL.InboardPickUp,  vehicleCADDrawer_Input.CoordinatesFR.InboardPickUp);


                    vehicleCADDrawer_Input.SteeringCSystemPlotter(Vehicle.List_Vehicle[Index_Input].sc_FL, Vehicle.List_Vehicle[Index_Input].sc_FR, vehicleCADDrawer_Input.CoordinatesFL.InboardPickUp, vehicleCADDrawer_Input.CoordinatesFR.InboardPickUp);


                    vehicleCADDrawer_Input.ARBConnector(vehicleCADDrawer_Input.CoordinatesRL.InboardPickUp, vehicleCADDrawer_Input.CoordinatesRR.InboardPickUp);

                    vehicleCADDrawer_Input.SetupViewPort();

                }

                if (_importCAD && !FileHasBeenImported) 
                {
                    ///<summary>
                    ///Calling the <see cref="CAD.ImportCAD(ref bool)"/> function. This function requires a string of the file name to be passed. But this string is used only in 2 conditions. 
                    ///1. If the Output Vehicle is calling the import function 
                    ///2. If the Input and Output Vehicles are calling the function during an Open operation
                    ///These are the only 2 conditions when the controlling IF loop will allow access to section of the code which requires the string to be passed. Hence passing null here as the below lines of code will not trigger the IF loop under norm
                    ///conditions and THIS IF LOOP will not be triggerd when Open operations is going on 
                    /// </summary>
                    //vehicleCADDrawer_Input.ImportedCADPlotter(ref FileHasBeenImported, ref CadIsTobeImported, false, vehicleCADDrawer_Input.openFileDialog1.FileName/*, CADVehicleInputs.importedFile*/);
                    //IGESFIleName = vehicleCADDrawer_Input.openFileDialog1.FileName;
                }

                ///<summary>
                ///Since the Vehicle is allowed to be created without the creation of the Input Items it may happen that (since Suspension doesn't exist yet) the <see cref="VehicleGUI.Vehicle_MotionExists"/> is not yet assigned.
                /// So the <see cref="Vehicle.sc_FL"/> is evalutated to check if it is null. Only if it is not null can the decision regarding whether or not Motion exists can be taken  
                /// </summary>
                if (!_motionExists && Vehicle.List_Vehicle[Index_Input].sc_FL != null)
                {
                    vehicleCADDrawer_Input.DrawStands(Vehicle.List_Vehicle[Index_Input].sc_FL, Vehicle.List_Vehicle[Index_Input].sc_FR, Vehicle.List_Vehicle[Index_Input].sc_RL, Vehicle.List_Vehicle[Index_Input].sc_RR); 
                }
            }
            catch (Exception E)
            {
                string error = E.Message;
                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Vehicle or VehicleGUI item being present, then the software won't crasha
            }
        }
        #endregion

        #region Output Coordinates Plotter

        #region Method to get the Bearing Cap Attachment Point Forces
        private void GetAttachmentPointForces(OutputClass _ocLeft, OutputClass _ocRight, out Vector3D _Force_P_Left, out Vector3D _Force_Q_Left, out Vector3D _Force_P_Right, out Vector3D _Force_Q_Right, bool _SRack, bool _SColumn)
        {
            _Force_P_Left = _Force_P_Right = _Force_Q_Left = _Force_Q_Right = new Vector3D();
            if (_SRack)
            {
                _Force_P_Left = new Vector3D(_ocLeft.RackInboard1_x, _ocLeft.RackInboard1_y, _ocLeft.RackInboard1_z);
                _Force_Q_Left = new Vector3D(_ocLeft.RackInboard2_x, _ocLeft.RackInboard2_y, _ocLeft.RackInboard2_z);
                _Force_P_Right = new Vector3D(_ocRight.RackInboard1_x, _ocRight.RackInboard1_y, _ocRight.RackInboard1_z);
                _Force_Q_Right = new Vector3D(_ocRight.RackInboard2_x, _ocRight.RackInboard2_y, _ocRight.RackInboard2_z);
            }
            else if (!_SRack)
            {
                _Force_P_Left = new Vector3D(_ocLeft.ARBInboard1_x, _ocLeft.ARBInboard1_y, _ocLeft.ARBInboard1_z);
                _Force_Q_Left = new Vector3D(_ocLeft.ARBInboard2_x, _ocLeft.ARBInboard2_y, _ocLeft.ARBInboard2_z);
                _Force_P_Right = new Vector3D(_ocRight.ARBInboard1_x, _ocRight.ARBInboard1_y, _ocRight.ARBInboard1_z);
                _Force_Q_Right = new Vector3D(_ocRight.ARBInboard2_x, _ocRight.ARBInboard2_y, _ocRight.ARBInboard2_z);
            }

            if (_SColumn)
            {
                _Force_P_Left = new Vector3D(_ocLeft.SColumnInboard1_x, _ocLeft.SColumnInboard1_y, _ocLeft.SColumnInboard1_z);
                _Force_P_Right = new Vector3D(_ocLeft.SColumnInboard2_x, _ocLeft.SColumnInboard2_y, _ocLeft.SColumnInboard2_z);
            }

        }
        #endregion

        #region Method to plot the Complete Output Suspension depnding on the motin Percentage Index passed and ALSO to plot all the Legend and ALSO to Paint the Force Arrows
        private void OutputDrawer(CAD vehicleCADDrawer_Output, int VehicleIndex, int OutputIndex, bool _importCAD, bool _plotWheel)
        {
            try
            {
                //vehicleCADDrawer_Output = new CAD();

                vehicleCADDrawer_Output.GetCoG(Vehicle.List_Vehicle[VehicleIndex].chassis_vehicle);

                ///<remarks> Plotting the Front Left Outputs. CP Forces which are calculated are passed so that the arrows can be plotted for them <seealso cref="CAD.PlotArrows(double, double, double, double, double, double, bool)"/></remarks>
                vehicleCADDrawer_Output.SuspensionPlotterInvoker(Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex].scmOP, 1, Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex].waOP, false, _plotWheel, Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex],
                    Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_FL_Fx, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_FL_Fy + Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex].CW, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_FL_Fz);

                ///<remarks> Plotting the Front Right Outputs. CP Forces which are calculated are passed so that the arrows can be plotted for them <seealso cref="CAD.PlotArrows(double, double, double, double, double, double, bool)"/></remarks>
                vehicleCADDrawer_Output.SuspensionPlotterInvoker(Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex].scmOP, 2, Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex].waOP, false, _plotWheel, Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex],
                   Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_FR_Fx, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_FR_Fy + Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex].CW, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_FR_Fz);

                ///<remarks> Plotting the Rear Left Outputs. CP Forces which are calculated are passed so that the arrows can be plotted for them <seealso cref="CAD.PlotArrows(double, double, double, double, double, double, bool)"/></remarks>
                vehicleCADDrawer_Output.SuspensionPlotterInvoker(Vehicle.List_Vehicle[VehicleIndex].oc_RL[OutputIndex].scmOP, 3, Vehicle.List_Vehicle[VehicleIndex].oc_RL[OutputIndex].waOP, false, _plotWheel, Vehicle.List_Vehicle[VehicleIndex].oc_RL[OutputIndex],
                    Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_RL_Fx, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_RL_Fy + Vehicle.List_Vehicle[VehicleIndex].oc_RL[OutputIndex].CW, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_RL_Fz);

                ///<remarks> Plotting the Rear Right Outputs. CP Forces which are calculated are passed so that the arrows can be plotted for them <seealso cref="CAD.PlotArrows(double, double, double, double, double, double, bool)"/></remarks>
                vehicleCADDrawer_Output.SuspensionPlotterInvoker(Vehicle.List_Vehicle[VehicleIndex].oc_RR[OutputIndex].scmOP, 4, Vehicle.List_Vehicle[VehicleIndex].oc_RR[OutputIndex].waOP, false, _plotWheel, Vehicle.List_Vehicle[VehicleIndex].oc_RR[OutputIndex],
                    Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_RR_Fx, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_RR_Fy + Vehicle.List_Vehicle[VehicleIndex].oc_RR[OutputIndex].CW, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.TotalLoad_RR_Fz);


                vehicleCADDrawer_Output.ARBConnector(vehicleCADDrawer_Output.CoordinatesFL.InboardPickUp, vehicleCADDrawer_Output.CoordinatesFR.InboardPickUp);


                vehicleCADDrawer_Output.SteeringCSystemPlotter(Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex].scmOP, Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex].scmOP, vehicleCADDrawer_Output.CoordinatesFL.InboardPickUp, vehicleCADDrawer_Output.CoordinatesFR.InboardPickUp);


                vehicleCADDrawer_Output.ARBConnector(vehicleCADDrawer_Output.CoordinatesRL.InboardPickUp, vehicleCADDrawer_Output.CoordinatesRR.InboardPickUp);

                Vector3D ForcePLeft, ForceQLeft, ForcePRight, ForceQRight = new Vector3D();
                ///<summary>Obtaining the Forces in the FL, FR, RL, RR Steering Rack Points of the FRONT</summary>
                GetAttachmentPointForces(Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex], out ForcePLeft, out ForceQLeft, out ForcePRight, out ForceQRight, true, false);
                CADVehicleOutputs.PlotLoadCase(Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.FL_BearingCoordinates, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.FR_BearingCoordinates, false, true, false, ForcePLeft, ForceQLeft, ForcePRight, ForceQRight);

                ///<summary>Obtaining the Forces in the FL, FR, RL, RR ARB Points of the FRONT</summary>
                GetAttachmentPointForces(Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex], out ForcePLeft, out ForceQLeft, out ForcePRight, out ForceQRight, false, false);
                CADVehicleOutputs.PlotLoadCase(Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.FL_BearingCoordinates, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.FR_BearingCoordinates, false, false, false, ForcePLeft, ForceQLeft, ForcePRight, ForceQRight);

                ///<summary>Obtaining the Forces in the FL, FR, RL, RR Steering Rack Points of the REAR</summary>
                GetAttachmentPointForces(Vehicle.List_Vehicle[VehicleIndex].oc_RL[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_RR[OutputIndex], out ForcePLeft, out ForceQLeft, out ForcePRight, out ForceQRight, false, false);
                CADVehicleOutputs.PlotLoadCase(Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.RL_BearingCoordinates, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.RR_BearingCoordinates, false, true, false, ForcePLeft, ForceQLeft, ForcePRight, ForceQRight);

                ///<summary>Using manipulation to obtain the forces on the Left and Right Steering Column Attachment Points</summary>
                GetAttachmentPointForces(Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex], out ForcePLeft, out ForceQLeft, out ForcePRight, out ForceQRight, false, true);
                CADVehicleOutputs.PlotLoadCase(Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.SteeringColumnBearing, Vehicle.List_Vehicle[VehicleIndex].vehicleLoadCase.SteeringColumnBearing, false, true, true, ForcePLeft, new Vector3D(), ForcePRight, new Vector3D());

                ///<remarks>Order of Painting is Important</remarks>
                ///<summary>Creating a temporary Output Class Variable which will hold the Max and Min Values collated from ALL THE 4 CORNERS</summary>
                OutputClass MasterOC = new OutputClass();
                MasterOC = MasterOC.PopulateForceLists(Vehicle.List_Vehicle[VehicleIndex].oc_FL[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_FR[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_RL[OutputIndex], Vehicle.List_Vehicle[VehicleIndex].oc_RR[OutputIndex]);

                LoadCaseLegend.InitializeLegendEditor(MasterOC, CADVehicleOutputs);

                CADVehicleOutputs.PaintBarForce();

                CADVehicleOutputs.PaintArrowForce();

                ///<summary>This method exists to ensure that the Imported FIles are not recloned everytime the user selects a different Motion Percentage from the Motion View Grid</summary>
                if (_importCAD && !OutputIGESPlotted)
                {
                    try
                    {
                        vehicleCADDrawer_Output.CloneImportedCAD(ref FileHasBeenImported, ref CadIsTobeImported, true, Kinematics_Software_New.M1_Global.vehicleGUI[VehicleIndex].importCADForm.importCADViewport.igesEntities);
                        IGESFIleName = vehicleCADDrawer_Output.openFileDialog1.FileName;
                        OutputIGESPlotted = true;
                    }
                    catch (Exception E)
                    {
                        string error = E.Message;
                    }
                }

                ///<summary>This Loop exists to allow the imported files to be translated around the Suspension  </summary>
                if (_importCAD && OutputIGESPlotted && TranslateChassisToGround)
                {
                    r1 = Kinematics_Software_New.AssignFormVariable();

                    int opIndex = 0;

                    int motionInd = Vehicle.List_Vehicle[_VehicleID - 1].vehicle_Motion.MotionID - 1;

                    opIndex = MotionGUI.List_MotionGUI[motionInd].bandedGridView_Motion.FocusedRowHandle;

                    ImportedCADTranslationHistory.Add(-Vehicle.List_Vehicle[_VehicleID - 1].oc_FL[opIndex].FinalRideHeight_1);

                    for (int iTrans = 0; iTrans < importCADForm.importCADViewport.igesEntities.Count(); iTrans++)
                    {
                        vehicleCADDrawer_Output.viewportLayout1.Entities[iTrans].Translate(0, -(ImportedCADTranslationHistory[ImportedCADTranslationHistory.Count - 1] - ImportedCADTranslationHistory[ImportedCADTranslationHistory.Count - 2]), 0);
                        vehicleCADDrawer_Output.viewportLayout1.Refresh();
                    }
                }

                vehicleCADDrawer_Output.viewportLayout1.Update();
                vehicleCADDrawer_Output.viewportLayout1.Refresh();
                //vehicleCADDrawer_Output.SetupViewPort();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                // Keeping this code in try and catch block will help during Open operation. If the method is called without a Vehicle or VehicleGUI item being present, then the software won't crasha
            }
        } 
        #endregion

        #endregion

        #endregion

        #region Method to initialize the ImportCADForm
        public static void InitializeImportCADForm(Kinematics_Software_New _r1)
        {
            int index = Vehicle.List_Vehicle.Count - 1;
            bool SuspensionIsCreated = false;
            bool WAIsCreated = false;

            
            //Kinematics_Software_New.M1_Global.vehicleGUI[index].importCADForm = new /*ImportCADForm()*/ XUC_ImportCAD();
            //Kinematics_Software_New.M1_Global.vehicleGUI[index].CadIsTobeImported = true;
            Kinematics_Software_New.M1_Global.vehicleGUI[index].importCADForm.AssignVehicleGUIObject(Kinematics_Software_New.M1_Global.vehicleGUI[index]);
            ///<summary>Below lines of code were used when the importCADForm was a form and not a User control.</summary>
            //Kinematics_Software_New.M1_Global.vehicleGUI[index].importCADForm.Show();
            //Kinematics_Software_New.M1_Global.vehicleGUI[index].importCADForm.BringToFront();

            if (SuspensionCoordinatesFront.Assy_List_SCFL.Count != 0)
            {
                SuspensionIsCreated = true;
            }
            if (WheelAlignment.Assy_List_WA.Count != 0)
            {
                WAIsCreated = true;
            }

            Kinematics_Software_New.M1_Global.vehicleGUI[index].importCADForm.AssignFormVariables(SuspensionIsCreated, WAIsCreated, index);
            Kinematics_Software_New.M1_Global.vehicleGUI[index].importCADForm.InitializeForm();

        }


        #endregion

        #region De-serialization of the Vehicle GUI object's Data
        public VehicleGUI(SerializationInfo info, StreamingContext context)
        {
            _VehicleGUIName = (string)info.GetValue("VehicleGUI_Name", typeof(string));
            _VehicleID = (int)info.GetValue("_VehicleID", typeof(int));
            navBarItem_Vehicle_Results = (List<CusNavBarItem>)info.GetValue("navBarItemResults", typeof(List<CusNavBarItem>));
            navBarGroup_Vehicle_Result = (CusNavBarGroup)info.GetValue("navBarGroupResults", typeof(CusNavBarGroup));
            TabPages_Vehicle = (List<CustomXtraTabPage>)info.GetValue("TabPages",typeof(List<CustomXtraTabPage>));
            TabPage_VehicleInputCAD = (CustomXtraTabPage)info.GetValue("TabPage_VehicleInputCAD", typeof(CustomXtraTabPage));

            AssemblyChecker_GUI = (int)info.GetValue("AssemblyChecker_GUI", typeof(int));
            SuspensionIsAssembled_GUI = (bool)info.GetValue("SuspensionIsAssembled_GUI", typeof(bool));
            TireIsAssembled_GUI = (bool)info.GetValue("TireIsAssembled_GUI", typeof(bool));
            SpringIsAssembled_GUI = (bool)info.GetValue("SpringIsAssembled_GUI", typeof(bool));
            DamperIsAssembled_GUI = (bool)info.GetValue("DamperIsAssembled_GUI", typeof(bool));
            ARBIsAssembled_GUI = (bool)info.GetValue("ARBIsAssembled_GUI", typeof(bool));
            ChassisIsAssembled_GUI = (bool)info.GetValue("ChassisIsAssembled_GUI", typeof(bool));
            WAIsAssembled_GUI = (bool)info.GetValue("WAIsAssembled_GUI", typeof(bool));
            VehicleHasBeenValidated_GUI = (bool)info.GetValue("VehicleHasBeenValidated_GUI", typeof(bool));

            _OutputOriginX = (double)info.GetValue("OutputOriginX", typeof(double));
            _OutputOriginY = (double)info.GetValue("OutputOriginY", typeof(double));
            _OutputOriginZ = (double)info.GetValue("OutputOriginZ", typeof(double));

            Vehicle_MotionExists = (bool)info.GetValue("Vehicle_MotionExists", typeof(bool));

            IndexOfOutput = (int)info.GetValue("IndexOfOutput", typeof(int));

            ocGUI_FL = (OutputClassGUI)info.GetValue("ocGUI_FL", typeof(OutputClassGUI));
            ocGUI_FR = (OutputClassGUI)info.GetValue("ocGUI_FR", typeof(OutputClassGUI));
            ocGUI_RL = (OutputClassGUI)info.GetValue("ocGUI_RL", typeof(OutputClassGUI));
            ocGUI_RR = (OutputClassGUI)info.GetValue("ocGUI_RR", typeof(OutputClassGUI));

            CadIsTobeImported = (bool)info.GetValue("CadIsTobeImported", typeof(bool));
            FileHasBeenImported = (bool)info.GetValue("FileHasBeenImported", typeof(bool));
            OutputIGESPlotted = (bool)info.GetValue("OutputIGESPlotted", typeof(bool));
            IGESFIleName = (string)info.GetValue("IGESFIleName", typeof(string));

            //CADVehicleInputs = (CAD)info.GetValue("CADVehicleInputs", typeof(CAD));
            //CADVehicleOutputs = (CAD)info.GetValue("CADVehicleOutputs", typeof(CAD));
        } 
        #endregion

        #region Serialization of the Vehicle GUI Object's data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("VehicleGUI_Name", _VehicleGUIName);
            info.AddValue("_VehicleID", _VehicleID);
            info.AddValue("navBarItemResults", navBarItem_Vehicle_Results);
            info.AddValue("navBarGroupResults", navBarGroup_Vehicle_Result);
            info.AddValue("TabPages", TabPages_Vehicle);
            info.AddValue("TabPage_VehicleInputCAD", TabPage_VehicleInputCAD);

            info.AddValue("AssemblyChecker_GUI", AssemblyChecker_GUI);
            info.AddValue("SuspensionIsAssembled_GUI", SuspensionIsAssembled_GUI);
            info.AddValue("TireIsAssembled_GUI", TireIsAssembled_GUI);
            info.AddValue("SpringIsAssembled_GUI", SpringIsAssembled_GUI);
            info.AddValue("DamperIsAssembled_GUI", DamperIsAssembled_GUI);
            info.AddValue("ARBIsAssembled_GUI", ARBIsAssembled_GUI);
            info.AddValue("ChassisIsAssembled_GUI", ChassisIsAssembled_GUI);
            info.AddValue("WAIsAssembled_GUI", WAIsAssembled_GUI);
            info.AddValue("VehicleHasBeenValidated_GUI", VehicleHasBeenValidated_GUI);

            info.AddValue("OutputOriginX", _OutputOriginX);
            info.AddValue("OutputOriginY", _OutputOriginY);
            info.AddValue("OutputOriginZ", _OutputOriginZ);

            info.AddValue("Vehicle_MotionExists", Vehicle_MotionExists);

            info.AddValue("IndexOfOutput", IndexOfOutput);

            info.AddValue("ocGUI_FL", ocGUI_FL);
            info.AddValue("ocGUI_FR", ocGUI_FR);
            info.AddValue("ocGUI_RL", ocGUI_RL);
            info.AddValue("ocGUI_RR", ocGUI_RR);

            info.AddValue("CadIsTobeImported", CadIsTobeImported);
            info.AddValue("FileHasBeenImported", FileHasBeenImported);
            info.AddValue("OutputIGESPlotted", OutputIGESPlotted);
            info.AddValue("IGESFIleName", IGESFIleName);

            //info.AddValue("CADVehicleInputs", CADVehicleInputs);
            //info.AddValue("CADVehicleOutputs", CADVehicleOutputs);
        } 
        #endregion
    }

    public enum VehicleVisualizationType
    {
        Generic,
        ImportedCAD
    }

}

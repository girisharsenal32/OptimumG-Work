using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    public class BatchRunGUI
    {
        #region Batch Run GUI Properties
        /// <summary>
        /// Name of the Batch Run
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ID of the Batch Run
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Static counter to keep track of the total number of Batch Runs Created
        /// </summary>
        public static int Counter { get; set; } = 0; 
        #endregion


        /// <summary>
        /// Object of the Main Form
        /// </summary>
        Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();


        /// <summary>
        /// List of Batch Runs created
        /// </summary>
        public static List<BatchRunGUI> batchRuns_GUI = new List<BatchRunGUI>(); 


        /// <summary>
        /// Vehicle which will be used for Batch Run 
        /// </summary>
        public Vehicle batchRunVehicle;


        /// <summary>
        /// Motion which will be used for the Batch Run
        /// </summary>
        public Motion batchRunMotion;


        /// <summary>
        /// Object of the <see cref="BatchRunForm"/> class which displays the <see cref="BatchRunForm"/>
        /// </summary>
        public BatchRunForm batchRun;


        #region Input GUI Items
        /// <summary>
        /// <see cref="CusNavBarItem"/> of the Batch Run for the Inputs
        /// </summary>
        public CusNavBarItem navBarItemBatchRun;
        ///// <summary>
        ///// <see cref="CusNavBarGroup"/> of the Batch Run for the Results
        ///// </summary>
        //public CusNavBarGroup navBarGroupBatchRunResults;

        /// <summary>
        /// Boolean to determine if the <see cref="Kinematics_Software_New.barButtonCreateBatchRun_ItemClick(object, DevExpress.XtraBars.ItemClickEventArgs)"/> is pressed. 
        /// If yes, then the <see cref="BatchRunForm.comboBoxVehicleBatchRun_SelectedIndexChanged(object, EventArgs)"/> will not be triggered. Because if it is triggered while new item is being created it would lead to exception
        /// </summary>
        public static bool batchRunBeingCreated = false;
        #endregion

        #region Output GUI Items
        public Dictionary<string, CusNavBarItem> navBarItem_BatchRun_Results = new Dictionary<string, CusNavBarItem>();

        public CusNavBarGroup navBarGroupBatchRunResults = new CusNavBarGroup();

        public Dictionary<string, CustomXtraTabPage> TabPages_BatchRUn = new Dictionary<string, CustomXtraTabPage>();

        public XtraScrollableControl xtraScrollableControl_BatchRun = new XtraScrollableControl();

        public CustomBandedGridView bandedGridViewBatchRun = new CustomBandedGridView();

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public BatchRunGUI()
        {
            batchRun = new BatchRunForm();

            ID = Counter + 1;

            Name = "Batch Run " + ID;

            navBarItemBatchRun = new CusNavBarItem(Name, ID, this);

        }

        /// <summary>
        /// Method to handle the GUI operations of the <see cref="BatchRunGUI"/>
        /// </summary>
        /// <param name="_navBarControl"></param>
        /// <param name="_navBarGroup"></param>
        public void HandleGUI(NavBarControl _navBarControl, NavBarGroup _navBarGroup)
        {
            ///<summary>Creating the Input NavBar Item and its Link Clicked Event</summary>
            navBarItemBatchRun.CreateNavBarItem(navBarItemBatchRun, _navBarGroup, _navBarControl);
            navBarItemBatchRun.LinkClicked += NavBarItemBatchRun_LinkClicked;

        }

        /// <summary>
        /// <see cref="NavBarItem"/> link clicked event of the Batch Run GUI Item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarItemBatchRun_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            InitializeBatchRunForm(LoadCase.List_LoadCases);
            batchRun.RestoreCheckState();
            batchRun.ShowDialog();
        }

        /// <summary>
        /// Method to initialize the <see cref="BatchRunForm"/>
        /// </summary>
        /// <param name="_loadCasesForBatchRun"></param>
        public void InitializeBatchRunForm(List<LoadCase> _loadCasesForBatchRun)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            batchRun.toolStripProgressBar1.ProgressBar.Value = 0;
            batchRun.toolStripProgressBar1.ProgressBar.Update();

            batchRun.PopulateBatchRunLoadCases(_loadCasesForBatchRun);

            R1.ComboboxBatchRunVehicleOperations();

            R1.comboBoxBatchRunMotionOperations();
        }

        /// <summary>
        /// Method to create the Tab Pages for each Load Case inside the <paramref name="_batchRunLoadCases"/>
        /// </summary>
        /// <param name="_tabPagesBatchRun"></param>
        /// <param name="_batchRunLoadCases"></param>
        public void CreateTabPages(Dictionary<string, CustomXtraTabPage> _tabPagesBatchRun, List<LoadCase> _batchRunLoadCases)
        {
            for (int i = 0; i < _batchRunLoadCases.Count; i++)
            {
                if (!_tabPagesBatchRun.ContainsKey(_batchRunLoadCases[i].LoadCaseName))
                {
                    _tabPagesBatchRun.Add(_batchRunLoadCases[i].LoadCaseName, CustomXtraTabPage.CreateNewTabPage_General(_batchRunLoadCases[i].LoadCaseName));
                    _tabPagesBatchRun[_batchRunLoadCases[i].LoadCaseName].PageVisible = false;
                    _tabPagesBatchRun[_batchRunLoadCases[i].LoadCaseName] = AddUserControlToTabPage(_tabPagesBatchRun[_batchRunLoadCases[i].LoadCaseName], LoadCaseGUI.List_LoadCaseGUI[i].batchRun_WF, UserControlRequired.WishboneForces); 
                }
            }
        }

        /// <summary>
        /// Method to create the Tab Pages for each <see cref="HeatMapWorksheet"/> inside the <see cref="HeatMapWorksheet.Worksheets"/>
        /// </summary>
        /// <param name="_tabPagesBatchRun"></param>
        /// <param name="_heatMapWorksheet"></param>
        public void CreateTabPages(Dictionary<string, CustomXtraTabPage> _tabPagesBatchRun, HeatMapWorksheet _heatMapWorksheet)
        {
            if (!_tabPagesBatchRun.ContainsKey(_heatMapWorksheet.Name))
            {
                _tabPagesBatchRun.Add(_heatMapWorksheet.Name, CustomXtraTabPage.CreateNewTabPage_General(_heatMapWorksheet.Name));
                _tabPagesBatchRun[_heatMapWorksheet.Name].PageVisible = false;
                _tabPagesBatchRun[_heatMapWorksheet.Name] = AddUserControlToTabPage(_tabPagesBatchRun[_heatMapWorksheet.Name], _heatMapWorksheet, UserControlRequired.HeatMap);
            }
        }

        /// <summary>
        /// Method to add the <see cref="XtraUserControl_WishboneForces"/> to the TabPages of the BatchRun
        /// </summary>
        /// <param name="_tabPagesBatchRun"></param>
        private CustomXtraTabPage AddUserControlToTabPage(CustomXtraTabPage _tabPagesBatchRun, object _userControl, UserControlRequired _userControlRequired)
        {
            if (_userControlRequired == UserControlRequired.WishboneForces)
            {
                XtraUserControl_WishboneForces tempUC = (XtraUserControl_WishboneForces)_userControl;
                tempUC.Dock = System.Windows.Forms.DockStyle.Fill;
                _tabPagesBatchRun.Controls.Add(tempUC);
                 
            }
            else if (_userControlRequired == UserControlRequired.HeatMap)
            {
                HeatMapWorksheet tempHMCWS = (HeatMapWorksheet)_userControl;
                tempHMCWS.Dock = System.Windows.Forms.DockStyle.Fill;
                _tabPagesBatchRun.Controls.Add(tempHMCWS);
            }

            return _tabPagesBatchRun;

        }

        /// <summary>
        /// Method to perform the NavBar Operations for the Batch Run
        /// </summary>
        /// <param name="_batchRunGUI"></param>
        public void BatchRun_NavBarGroupOperations(BatchRunGUI _batchRunGUI, List<LoadCase> _batchRunLoadCases)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            int index = _batchRunGUI.ID - 1;

            if (!NavBarGroupRepeatedCheck(_batchRunGUI))
            {
                ///<summary>Creating a <see cref="NavBarGroup"/> object </summary>
                _batchRunGUI.navBarGroupBatchRunResults = _batchRunGUI.navBarGroupBatchRunResults.CreateNewNavBarGroup_For_VehicleResults(_batchRunGUI.navBarGroupBatchRunResults, R1.navBarControlResults, _batchRunGUI.ID, _batchRunGUI.Name);

                ///<summary>Adding the created <see cref="NavBarGroup"/> object to the <see cref="navBarControlResults"/></summary>
                R1.navBarControlResults.Groups.Add(_batchRunGUI.navBarGroupBatchRunResults);

                ///<summary>Activating the newly added group</summary>
                int groupIndex = R1.navBarControlResults.Groups.IndexOf(_batchRunGUI.navBarGroupBatchRunResults);
                R1.navBarControlResults.ActiveGroup = R1.navBarControlResults.Groups[groupIndex];
            }

            CreateNavBarItem(_batchRunLoadCases, _batchRunGUI.navBarItem_BatchRun_Results, _batchRunGUI.navBarGroupBatchRunResults, R1.navBarControlResults);

        }

        /// <summary>
        /// Method to ensure that the same <see cref="NavBarGroup"/> is not added again to the <see cref="Kinematics_Software_New.navBarControlResults"/>
        /// </summary>
        /// <param name="_batchRUnGUICheck"></param>
        /// <returns></returns>
        private bool NavBarGroupRepeatedCheck(BatchRunGUI _batchRUnGUICheck)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            if (R1.navBarControlResults.Groups.Count != 0)
            {
                foreach (NavBarGroup group in R1.navBarControlResults.Groups)
                {
                    if (group.Name == _batchRUnGUICheck.navBarGroupBatchRunResults.Name)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to create <see cref="NavBarItem"/>s specifically for <see cref="BatchRunGUI"/> results and add them to the <see cref="Kinematics_Software_New.navBarControlResults"/> and <see cref="BatchRunGUI.navBarGroupBatchRunResults"/>
        /// </summary>
        /// <param name="_batchRunLoadCases"></param>
        /// <param name="_navBarItemsResults"></param>
        /// <param name="_navBarGroupResults"></param>
        /// <param name="_navBarControlResults"></param>
        public void CreateNavBarItem(List<LoadCase> _batchRunLoadCases, Dictionary<string, CusNavBarItem> _navBarItemsResults, CusNavBarGroup _navBarGroupResults, NavBarControl _navBarControlResults)
        {
            for (int i = 0; i < _batchRunLoadCases.Count; i++)
            {
                if (!_navBarItemsResults.ContainsKey(_batchRunLoadCases[i].LoadCaseName))
                {
                    ///<summary>Creating and adding a <see cref="CusNavBarItem"/> to <see cref="navBarItem_BatchRun_Results"/></summary>
                    _navBarItemsResults.Add(_batchRunLoadCases[i].LoadCaseName, new CusNavBarItem(_batchRunLoadCases[i].LoadCaseName, _batchRunLoadCases[i].LoadCaseID, _batchRunLoadCases[i]));

                    ///<summary>Adding the Items to the <see cref="Kinematics_Software_New.navBarControlResults"/></summary>
                    _navBarControlResults.Items.Add(_navBarItemsResults[_batchRunLoadCases[i].LoadCaseName]);
                    _navBarControlResults.Items[_navBarItemsResults[_batchRunLoadCases[i].LoadCaseName].Name].Visible = true;

                    ///<summary>Adding the Items to the <see cref="navBarGroupBatchRunResults"/></summary>
                    _navBarGroupResults.GroupStyle = NavBarGroupStyle.Default;
                    _navBarGroupResults.ItemLinks.Add(_navBarItemsResults[_batchRunLoadCases[i].LoadCaseName]);

                    ///<summary>Creating a Link Clicked Event for the </summary>
                    _navBarItemsResults[_batchRunLoadCases[i].LoadCaseName].LinkClicked += BatchRunGUI_LinkClicked; 
                }
            }
        }

        /// <summary>
        /// Method to create <see cref="NavBarItem"/>s specifically for <see cref="HeatMapWorksheet"/> items and add them to the <see cref="Kinematics_Software_New.navBarControlResults"/> and <see cref="BatchRunGUI.navBarGroupBatchRunResults"/>
        /// </summary>
        /// <param name="_heatMapWorksheet"></param>
        /// <param name="_navBarItemsResults"></param>
        /// <param name="_navBarGroupResults"></param>
        /// <param name="_navBarControlResults"></param>
        public void CreateNavBarItem(HeatMapWorksheet _heatMapWorksheet, Dictionary<string, CusNavBarItem> _navBarItemsResults, CusNavBarGroup _navBarGroupResults, NavBarControl _navBarControlResults)
        {

            if (!_navBarItemsResults.ContainsKey(_heatMapWorksheet.Name))
            {
                ///<summary>Creating and adding a <see cref="CusNavBarItem"/> to <see cref="navBarItem_BatchRun_Results"/></summary>
                _navBarItemsResults.Add(_heatMapWorksheet.Name, new CusNavBarItem(_heatMapWorksheet.Name, _heatMapWorksheet.ID, _heatMapWorksheet));

                ///<summary>Adding the Items to the <see cref="Kinematics_Software_New.navBarControlResults"/></summary>
                _navBarControlResults.Items.Add(_navBarItemsResults[_heatMapWorksheet.Name]);
                _navBarControlResults.Items[_navBarItemsResults[_heatMapWorksheet.Name].Name].Visible = true;

                ///<summary>Adding the Items to the <see cref="navBarGroupBatchRunResults"/></summary>
                _navBarGroupResults.GroupStyle = NavBarGroupStyle.Default;
                _navBarGroupResults.ItemLinks.Add(_navBarItemsResults[_heatMapWorksheet.Name]);

                ///<summary>Creating a Link Clicked Event for the </summary>
                _navBarItemsResults[_heatMapWorksheet.Name].LinkClicked += BatchRunGUI_LinkClicked;
            }

        }

        /// <summary>
        /// Link Clicked event for the <see cref="navBarItem_BatchRun_Results"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatchRunGUI_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            string Name = e.Link.Group.Name;
            int SelectedPageIndex = 0;
            int Index = 0;

            ///<summary>Finding out which of <see cref="BatchRunGUI"/> object from the <seealso cref="batchRuns_GUI"/> <see cref="List{T}"/> to use </summary>
            for (int i = 0; i < batchRuns_GUI.Count; i++)
            {
                if (Name == batchRuns_GUI[i].navBarGroupBatchRunResults.Name)
                {
                    Index = batchRuns_GUI[i].navBarGroupBatchRunResults.navBarID - 1;
                    break;
                }
            }

            ///<summary>Iterating through each item of the <see cref="navBarItem_BatchRun_Results"/> to find which one of it was clicked</summary>
            foreach (string itemName in batchRuns_GUI[Index].navBarItem_BatchRun_Results.Keys)
            {
                if (e.Link.ItemName == itemName)
                {
                    ///<summary>Using the Key of the selected <see cref="navBarItem_BatchRun_Results"/> and passing it to the <see cref="TabPages_BatchRUn"/> dictionary to find the Tab Page which is to be made visible and selected </summary>
                    SelectedPageIndex = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(batchRuns_GUI[Index].TabPages_BatchRUn[itemName]);

                    Kinematics_Software_New.TabControl_Outputs.TabPages[SelectedPageIndex].PageVisible = true;

                    Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPageIndex;
                }
            }

            for (int i = 0; i < HeatMapWorksheet.Worksheets.Count; i++)
            {
                if (Kinematics_Software_New.TabControl_Outputs.SelectedTabPage.Contains(HeatMapWorksheet.Worksheets[i]))
                {
                    R1.barButtonItemModifyHeatMap.Enabled = true;
                }
                else
                {
                    R1.barButtonItemModifyHeatMap.Enabled = false;
                }
            }

        }
    }

    public enum BatchRunOutputMode
    {
        SingleOutputChannel,
        MultipleOutputChannel
    };

    public enum UserControlRequired
    {
        WishboneForces,
        HeatMap
    };

}

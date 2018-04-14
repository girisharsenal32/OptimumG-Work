using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraNavBar;

namespace Coding_Attempt_with_GUI
{
    public class SetupChange_GUI
    {
        #region Declarations

        #region GUI Declrations
        /// <summary>
        /// Name of the Setu Change Item
        /// </summary>
        public string _SetupChangeName { get; set; }
        /// <summary>
        /// ID of the Setup Change Itemm
        /// </summary>
        public int _SetupChangeID { get; set; }
        /// <summary>
        /// Static variable to count the Number of SetupChanges created
        /// </summary>
        public static int _SetupChangeCounter { get; set; }
        /// <summary>
        /// Static List to hold the Setup change Items created
        /// </summary>
        public static List<SetupChange_GUI> List_SetupChangeGUI = new List<SetupChange_GUI>();
        
        /// <summary>
        /// TabPage of the Setup Change item
        /// </summary>
        public CustomXtraTabPage TabPage_SetupChangeGUI = new CustomXtraTabPage();
        /// <summary>
        /// Instance of the Setup Change User Control
        /// </summary>
        public XUC_SetupChange XUC_SetupChange = new XUC_SetupChange();
        /// <summary>
        /// NavBarItem representing this object of the SetupChange class
        /// </summary>
        public CusNavBarItem navBarItemSetupChange;
        #endregion

        #region Variable Declrations

        public SetupChange_CornerVariables setupChangeFL_GUI;

        public SetupChange_CornerVariables setupChangeFR_GUI;

        public SetupChange_CornerVariables setupChangeRL_GUI;

        public SetupChange_CornerVariables setupChangeRR_GUI;
        #endregion

        #endregion



        Kinematics_Software_New R1;

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="_setupChangeName">Name of the Setup Change</param>
        /// <param name="_setupChangeID">ID of the Setup Change </param>
        public SetupChange_GUI(string _setupChangeName, int _setupChangeID)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            ///<summary>Assinging the Name</summary>
            _SetupChangeName = _setupChangeName + _setupChangeID;

            ///<summary>Assinging the ID</summary>
            _SetupChangeID = _setupChangeID;

            ///<summary>Initializing the <see cref="SetupChange_CornerVariables"/>objects</summary>
            setupChangeFL_GUI = new SetupChange_CornerVariables();
            setupChangeFR_GUI = new SetupChange_CornerVariables();
            setupChangeRL_GUI = new SetupChange_CornerVariables();
            setupChangeRR_GUI = new SetupChange_CornerVariables();

            ///<summary>Initializing the <see cref="NavBarItem"/> Object</summary>
            navBarItemSetupChange = new CusNavBarItem(_setupChangeName, _setupChangeID, this);

            ///<summary>Initialzing the <see cref="CustomXtraTabPage"/> object</summary>
            TabPage_SetupChangeGUI = CustomXtraTabPage.CreateNewTabPage_ForInputs(_setupChangeName, _setupChangeID);

        }

        /// <summary>
        /// Method to Initialize all the GUI parameters of the Setup Change
        /// </summary>
        /// <param name="_navBarGroup"><see cref="NavBarGroup"/> of the Setup Change</param>
        /// <param name="_navBarControl"><see cref="NavBarControl"/> of the Setup Change</param>
        /// <param name="_indexSetupChange">ID of the Setup Change which is calling this function</param>
        public void HandleGUI(NavBarGroup _navBarGroup, NavBarControl _navBarControl, int _indexSetupChange)
        {
            TabPage_SetupChangeGUI = CustomXtraTabPage.AddUserControlToTabPage(XUC_SetupChange, TabPage_SetupChangeGUI, System.Windows.Forms.DockStyle.Fill);
            Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, TabPage_SetupChangeGUI);
            Kinematics_Software_New.TabControl_Outputs.SelectedTabPage = List_SetupChangeGUI[_indexSetupChange].TabPage_SetupChangeGUI;

            List_SetupChangeGUI[_indexSetupChange].navBarItemSetupChange.CreateNavBarItem(List_SetupChangeGUI[_indexSetupChange].navBarItemSetupChange, _navBarGroup, _navBarControl);

            List_SetupChangeGUI[_indexSetupChange].navBarItemSetupChange = List_SetupChangeGUI[_indexSetupChange].LinkClickedEventCreator(List_SetupChangeGUI[_indexSetupChange].navBarItemSetupChange);

            List_SetupChangeGUI[_indexSetupChange].XUC_SetupChange.SetupChangeFL.GetGrandParentObjectData(this, setupChangeFL_GUI, 1, "Front Left", _indexSetupChange);
            List_SetupChangeGUI[_indexSetupChange].XUC_SetupChange.SetupChangeFR.GetGrandParentObjectData(this, setupChangeFR_GUI, 2, "Front Right", _indexSetupChange);
            List_SetupChangeGUI[_indexSetupChange].XUC_SetupChange.SetupChangeRL.GetGrandParentObjectData(this, setupChangeRL_GUI, 3, "Rear Left", _indexSetupChange);
            List_SetupChangeGUI[_indexSetupChange].XUC_SetupChange.SetupChangeRR.GetGrandParentObjectData(this, setupChangeRR_GUI, 4, "Rear Right", _indexSetupChange);

            List_SetupChangeGUI[_indexSetupChange].EditSetupChangeDeltas(setupChangeFL_GUI, 1);
            List_SetupChangeGUI[_indexSetupChange].EditSetupChangeDeltas(setupChangeFR_GUI, 2);
            List_SetupChangeGUI[_indexSetupChange].EditSetupChangeDeltas(setupChangeRL_GUI, 3);
            List_SetupChangeGUI[_indexSetupChange].EditSetupChangeDeltas(setupChangeRR_GUI, 4);
        }

        /// <summary>
        /// Method to initialize the Link Clicked event of the <see cref="NavBarItem"/>
        /// </summary>
        /// <param name="_navBaritemSetupChange"></param>
        /// <returns></returns>
        public CusNavBarItem LinkClickedEventCreator(CusNavBarItem _navBaritemSetupChange)
        {
            _navBaritemSetupChange.LinkClicked += _navBaritemSetupChange_LinkClicked;

            return _navBaritemSetupChange;
        }

        /// <summary>
        /// Link Clicked event of the <see cref="NavBarItem"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _navBaritemSetupChange_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            int index = R1.navBarGroupSetupChange.SelectedLinkIndex;

            Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(List_SetupChangeGUI[index].TabPage_SetupChangeGUI);
            Kinematics_Software_New.TabControl_Outputs.TabPages[Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex].PageVisible = true;

        }

        /// <summary>
        /// Method to edit the actual Setup Change class' object
        /// </summary>
        /// <param name="setupChangeMaster"></param>
        /// <param name="identifier"></param>
        public void EditSetupChangeDeltas(SetupChange_CornerVariables setupChangeMaster, int identifier)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            int indexSetup = R1.navBarGroupSetupChange.SelectedLinkIndex;

            SetupChange.List_SetupChange[indexSetup].InitOrEditDeltas(this, setupChangeMaster, identifier);

        }

        public void PlotOutputs()
        {

        }

    }
}

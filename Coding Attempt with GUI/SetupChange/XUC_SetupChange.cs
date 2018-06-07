using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraBars.Docking;


namespace Coding_Attempt_with_GUI
{
    public partial class XUC_SetupChange : XtraUserControl
    {
        static XUC_SetupChange _r1;

        public OutputClass OC_FL;

        public OutputClass OC_FR;

        public OutputClass OC_RL;

        public OutputClass OC_RR;



        public XUC_SetupChange()
        {
            InitializeComponent();
            // Handling the QueryControl event that will populate all automatically generated Documents
            this.tabbedView1.QueryControl += tabbedView1_QueryControl;
            _r1 = this;
            dockPanelResults.Visibility = DockVisibility.Hidden;


            dockPanelFL.Width = dockPanelFR.Width = this.Width / 2;
            dockPanelRL.Width = dockPanelRR.Width = this.Width / 2;

        }

        public void AssignCurrentUserControlState()
        {
            _r1 = this;
        }

        public static XUC_SetupChange AssignFormVariable()
        {
            _r1.AssignCurrentUserControlState();
            return _r1;
        }

        // Assigning a required content for each auto generated Document
        void tabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {


        }

        private void XUC_SetupChange_Load(object sender, EventArgs e)
        {
            _r1 = this;
            SetupChangeFL.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.OrangeRed;
            SetupChangeFR.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.LightGreen;
            SetupChangeRL.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.RoyalBlue;
            SetupChangeRR.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.Yellow;

            SetupChangeFL.checkedListBoxControlChanges.Appearance.BorderColor = Color.OrangeRed;
            SetupChangeFR.checkedListBoxControlChanges.Appearance.BorderColor = Color.LightGreen;
            SetupChangeRL.checkedListBoxControlChanges.Appearance.BorderColor = Color.RoyalBlue;
            SetupChangeRR.checkedListBoxControlChanges.Appearance.BorderColor = Color.Yellow;

            SetupChangeFL.checkedListBoxControlConstraints.Appearance.BorderColor = Color.OrangeRed;
            SetupChangeFR.checkedListBoxControlConstraints.Appearance.BorderColor = Color.LightGreen;
            SetupChangeRL.checkedListBoxControlConstraints.Appearance.BorderColor = Color.RoyalBlue;
            SetupChangeRR.checkedListBoxControlConstraints.Appearance.BorderColor = Color.Yellow;

            SetupChangeFL_Results.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.OrangeRed;
            SetupChangeFR_Results.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.LightGreen;
            SetupChangeRL_Results.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.RoyalBlue;
            SetupChangeRR_Results.vGridControl1.Appearance.RowHeaderPanel.BackColor = Color.Yellow;

            SetupChangeFL_Results.groupControl1.Text = "Front Left Results";
            SetupChangeFR_Results.groupControl1.Text = "Front Right Results";
            SetupChangeRL_Results.groupControl1.Text = "Rear Left Results";
            SetupChangeRR_Results.groupControl1.Text = "Rear Right Results";

        }

        internal void SetOutputClass(OutputClass _ocFL, OutputClass _ocFR, OutputClass _ocRL, OutputClass _ocRR)
        {
            OC_FL = _ocFL;

            OC_FR = _ocFR;

            OC_RL = _ocRL;

            OC_RR = _ocRR;
        }


        internal void DisplayOutputs(SetupChange_Outputs setup_OP_FL, SetupChange_CornerVariables cvFL, SetupChange_Outputs setup_OP_FR, SetupChange_CornerVariables cvFR,
                                     SetupChange_Outputs setup_OP_RL, SetupChange_CornerVariables cvRL, SetupChange_Outputs setup_OP_RR, SetupChange_CornerVariables cvRR)
        {
            bool Converged = true;


            ///<summary>Setting the Front Left Outputs</summary>
            SetupChangeFL_Results.DisplayIndividualOutputs(OC_FL, setup_OP_FL, cvFL, SetupChangeFL_Results, SetupChangeFL_Results.vGridControl1, ref Converged);
            ///<summary>Plotting the User's and Calculated Bump Steer Curve. If they are not requested, their count will be zero and it will be taken care of </summary>
            SetupChangeFL_Results.PlotBumpSteerGraph(setup_OP_FL, cvFL, SetupChangeFL_Results);

            ///<summary>Setting the Front Right Outputs</summary>
            SetupChangeFR_Results.DisplayIndividualOutputs(OC_FR, setup_OP_FR, cvFR, SetupChangeFR_Results, SetupChangeFR_Results.vGridControl1, ref Converged);
            ///<summary>Plotting the User's and Calculated Bump Steer Curve. If they are not requested, their count will be zero and it will be taken care of </summary>
            SetupChangeFR_Results.PlotBumpSteerGraph(setup_OP_FR, cvFR, SetupChangeFR_Results);

            ///<summary>Setting the Rear Left Outputs</summary>
            SetupChangeRL_Results.DisplayIndividualOutputs(OC_RL, setup_OP_RL, cvRL, SetupChangeRL_Results, SetupChangeRL_Results.vGridControl1, ref Converged);
            ///<summary>Plotting the User's and Calculated Bump Steer Curve. If they are not requested, their count will be zero and it will be taken care of </summary>
            SetupChangeFR_Results.PlotBumpSteerGraph(setup_OP_RL, cvRL, SetupChangeRL_Results);

            ///<summary>Setting the Rear Right Outputs</summary>
            SetupChangeRR_Results.DisplayIndividualOutputs(OC_RR, setup_OP_RR, cvRR, SetupChangeRR_Results, SetupChangeRR_Results.vGridControl1, ref Converged);
            ///<summary>Plotting the User's and Calculated Bump Steer Curve. If they are not requested, their count will be zero and it will be taken care of </summary>
            SetupChangeRR_Results.PlotBumpSteerGraph(setup_OP_RR, cvRR, SetupChangeRR_Results);

            ///<summary>Displaying the Results Panel in a Floating Style</summary>
            FloatOutputDockPanel();

        }

        internal void FloatOutputDockPanel()
        {
            dockPanelResults.Visibility = DockVisibility.Visible;
            dockPanelResults.DockedAsTabbedDocument = false;
            dockPanelResults.Dock = DockingStyle.Float;
        }

        private void barButtonDisplayResults_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelResults.Visibility = DockVisibility.Visible;
            dockPanelResults.DockedAsTabbedDocument = false;
            dockPanelResults.Dock = DockingStyle.Float;
            //dockPanelResults.MakeFloat(new Point();

        }

        private void barButtonHideResults_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelResults.Visibility = DockVisibility.Hidden;
        }
    }
}

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
using DevExpress.XtraNavBar;

namespace Coding_Attempt_with_GUI
{
    public partial class HeatMapWorksheet : XtraUserControl
    {

        public string Name { get; set; }

        public int ID { get; set; }

        public static int Counter { get; set; }

        public int DockPanels { get; set; }

        public BatchRunOutputMode OutputMode { get; set; }

        public static List<HeatMapWorksheet> Worksheets = new List<HeatMapWorksheet>();

        public HeatMapWorksheet()
        {
            InitializeComponent();
        }

        public void ConstructWorksheet(string _worksheetName, int _worksheetID, int parentBatchRunGUIIndex, BatchRunOutputMode _outputMode)
        {
            ID = _worksheetID;

            Name = _worksheetName + " " + ID;

            OutputMode = _outputMode; 

        }

        public void HandleGUI(NavBarControl _navBarControl, CusNavBarGroup _navBarGroup, HeatMapMode _heatMapMode, SpecialCaseOption _specialCase, int _brIndex)
        {
            BatchRunGUI.batchRuns_GUI[_brIndex].CreateTabPages(BatchRunGUI.batchRuns_GUI[_brIndex].TabPages_BatchRUn, HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter]);

            Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, BatchRunGUI.batchRuns_GUI[_brIndex].TabPages_BatchRUn);

            BatchRunGUI.batchRuns_GUI[_brIndex].CreateNavBarItem(this, BatchRunGUI.batchRuns_GUI[_brIndex].navBarItem_BatchRun_Results, _navBarGroup, _navBarControl);

            this.heatMapFL.ConstructHeatMapControl(Name, ID, OutputMode, _heatMapMode, _specialCase, 1, "FrontLeft");
            this.heatMapFR.ConstructHeatMapControl(Name, ID, OutputMode, _heatMapMode, _specialCase, 2, "FrontRight");
            this.heatMapRL.ConstructHeatMapControl(Name, ID, OutputMode, _heatMapMode, _specialCase, 3, "RearLeft");
            this.heatMapRR.ConstructHeatMapControl(Name, ID, OutputMode, _heatMapMode, _specialCase, 4, "RearRight");

        }

        public void GeatHeatMapInformation(string opChannelAllCorners, BatchRunGUI batchRunForHeatMap, List<LoadCase> loadCasesForHeatMap)
        { 

            HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapFL.DrawHeatMapSource(opChannelAllCorners, batchRunForHeatMap, loadCasesForHeatMap);
            if (opChannelAllCorners == "Steering Column Cap Left X" || opChannelAllCorners == "Steering Column Cap Left Y" || opChannelAllCorners == "Steering Column Cap Left Z" || opChannelAllCorners == "Steering Column Cap Right X" || opChannelAllCorners == "Steering Column Cap Right Y" ||
                opChannelAllCorners == "Steering Column Cap Right Z")
            {
                HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapFR.Hide();
                dockPanelFR.Hide();
            }
            else
            {
                HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapFR.DrawHeatMapSource(opChannelAllCorners, batchRunForHeatMap, loadCasesForHeatMap);
            }

            if (opChannelAllCorners == "Steering Column Cap Left X" || opChannelAllCorners == "Steering Column Cap Left Y" || opChannelAllCorners == "Steering Column Cap Left Z" || opChannelAllCorners == "Steering Column Cap Right X" || opChannelAllCorners == "Steering Column Cap Right Y" ||
                opChannelAllCorners == "Steering Column Cap Right Z"|| 
                opChannelAllCorners == "Steering Rack Cap Left X" || opChannelAllCorners == "Steering Rack Cap Left Y" || opChannelAllCorners == "Steering Rack Cap Left Z" || opChannelAllCorners == "Steering Rack Cap Right X" || opChannelAllCorners == "Steering Rack Cap Right Y" ||
                opChannelAllCorners == "Steering Rack Cap Right Z")
            {
                HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapRL.Hide();
                dockPanelRL.Hide();
                HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapRR.Hide();
                dockPanelRR.Hide();

            }
            else
            {
                HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapRL.DrawHeatMapSource(opChannelAllCorners, batchRunForHeatMap, loadCasesForHeatMap);
                HeatMapWorksheet.Worksheets[HeatMapWorksheet.Counter - 1].heatMapRR.DrawHeatMapSource(opChannelAllCorners, batchRunForHeatMap, loadCasesForHeatMap);
            }

        }

        private void panelContainer1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace Coding_Attempt_with_GUI
{
    public partial class HeatMapInformation : XtraForm
    {
        public string HeatMapName { get; set; }

        public string OutputChannel { get; set; }

        public Mode Operationmode { get; set; } = Mode.Create;

        public int WorkSheetID { get; set; }

        public SpecialCaseOption SpecialCase = SpecialCaseOption.None;

        BatchRunGUI BatchRunGUI_ForHeatMap;

        List<LoadCase> LoadCaseList_ForHeatMap;

        BatchRunResults Results_For_HeatMap;

        BatchRunOutputMode OutputMode;        

        Kinematics_Software_New R1;

        /// <summary>
        /// Simple Constructor
        /// </summary>
        public HeatMapInformation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overloaded Constructor which assigns the <see cref="Mode"/> of operaton of the Information Panel
        /// </summary>
        /// <param name="operationMode"></param>
        public HeatMapInformation(Mode operationMode)
        {
            InitializeComponent();

            Operationmode = operationMode;
        }

        /// <summary>
        /// Method to obtain the information regardin the <see cref="BatchRunGUI"/> and by extension the <see cref="List{T}"/> of <see cref="LoadCase"/> inside the <see cref="BatchRunForm.BatchRunloadCases"/>
        /// </summary>
        /// <param name="_batchRunGUI"></param>
        public void GetBatchRunData(List<BatchRunGUI> _batchRunGUI)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            int BatchRunIndex = 0;

            for (int i = 0; i < _batchRunGUI.Count; i++)
            {
                if (_batchRunGUI[i].navBarGroupBatchRunResults.Name == R1.navBarControlResults.ActiveGroup.Name)
                {
                    BatchRunIndex = i;
                    break;
                }
            }

            BatchRunGUI_ForHeatMap = BatchRunGUI.batchRuns_GUI[BatchRunIndex];

            LoadCaseList_ForHeatMap = BatchRunGUI_ForHeatMap.batchRun.BatchRunloadCases;

            GetOutputChannelData();

            textEditBatchRun.Text = BatchRunGUI_ForHeatMap.Name;

            if (BatchRunGUI_ForHeatMap.batchRunVehicle.sc_FL.SuspensionMotionExists)
            {
                OutputMode = BatchRunOutputMode.MultipleOutputChannel;
                toolStripStatusLabelOPChannelModeValue.Text = "Multiple Channels";
            }
            else
            {
                OutputMode = BatchRunOutputMode.SingleOutputChannel;
                toolStripStatusLabelOPChannelModeValue.Text = "Single Channel";
            }

        }

        /// <summary>
        /// Method to assign all the <see cref="BatchRunResults.OutputChannels"/> strings to the <see cref="comboBoxOutputChannel"/>. 
        /// </summary>
        private void GetOutputChannelData()
        {
            foreach (string item in LoadCaseList_ForHeatMap[0].runResults_FL.OutputChannels.Keys)
            {
                comboBoxOutputChannel.Items.Add(item);
            }

            comboBoxSpecialCase.Items.Add(SpecialCaseOption.None);
            comboBoxSpecialCase.Items.Add(SpecialCaseOption.HighestLoad);

            if (comboBoxOutputChannel.Items.Count != 0)
            {
                comboBoxOutputChannel.SelectedIndex = 0;
            }
        }


        /// <summary>
        /// Event which is fired when the <see cref="simpleButtonOK"/> is clicked. This method transfers information to <see cref="Kinematics_Software_New.CreateHeatMapWorksheet(string, BatchRunOutputMode, HeatMapMode, SpecialCaseOption)"/>
        /// which then creates a new <see cref="HeatMapWorksheet"/> and also 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.ProgressBar.Step = 10;
            toolStripProgressBar1.ProgressBar.Increment(10);
            toolStripProgressBar1.ProgressBar.Update();

            R1 = Kinematics_Software_New.AssignFormVariable();
            toolStripProgressBar1.ProgressBar.Increment(10);
            toolStripProgressBar1.ProgressBar.Update();

            HeatMapName = textEditHeatMapName.Text;
            toolStripProgressBar1.ProgressBar.Increment(10);
            toolStripProgressBar1.ProgressBar.Update();

            if (Operationmode == Mode.Create)
            {
                if (SpecialCase != SpecialCaseOption.None) 
                {
                    WorkSheetID = R1.CreateHeatMapWorksheet(textEditHeatMapName.Text, OutputMode, HeatMapMode.SpecialCase, SpecialCase); 
                }
                else if (SpecialCase == SpecialCaseOption.None)
                {
                    WorkSheetID = R1.CreateHeatMapWorksheet(textEditHeatMapName.Text, OutputMode, HeatMapMode.RegularHeatMap, SpecialCaseOption.None);
                }
            }
            toolStripProgressBar1.ProgressBar.Increment(10);
            toolStripProgressBar1.ProgressBar.Update();

            HeatMapWorksheet.Worksheets[WorkSheetID - 1].GeatHeatMapInformation(OutputChannel, BatchRunGUI_ForHeatMap, LoadCaseList_ForHeatMap);
            toolStripProgressBar1.ProgressBar.Increment(10);
            toolStripProgressBar1.ProgressBar.Update();

            toolStripProgressBar1.ProgressBar.Value = 100;
            toolStripProgressBar1.ProgressBar.Update();
            this.Hide();

        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxOutputChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputChannel = (string)comboBoxOutputChannel.SelectedItem;
        }

        private void textEditHeatMapName_Leave(object sender, EventArgs e)
        {
            HeatMapName = textEditHeatMapName.Text;
        }

        private void comboBoxSpecialCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSpecialCase.SelectedItem != null)
            {
                SpecialCase = (SpecialCaseOption)comboBoxSpecialCase.SelectedItem;

                if (SpecialCase == SpecialCaseOption.None)
                {
                    groupControlOPChannel.Show();
                }
                else
                {
                    groupControlOPChannel.Hide();
                }


            }
            else
            {
                SpecialCase = SpecialCaseOption.None;
                groupControlOPChannel.Show();
            }
        }
    }

    public enum Mode
    {
        Create,
        Modify
    };
}
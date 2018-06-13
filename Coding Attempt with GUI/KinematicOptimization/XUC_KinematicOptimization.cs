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

namespace Coding_Attempt_with_GUI
{
    public partial class XUC_KinematicOptimization : XtraUserControl
    {
        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> Class representing the Front Left Corner 
        /// This variable will store ALL the information which each of the GUI Controls of this Class (such as <see cref="xuC_KO_Camber1"/>) have to offer
        /// </summary>
        KO_CornverVariables ko_CV_FL;

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> Class representing the Front Right Corner 
        /// This variable will store ALL the information which each of the GUI Controls of this Class (such as <see cref="xuC_KO_Camber1"/>) have to offer
        /// </summary>
        KO_CornverVariables ko_CV_FR;

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> Class representing the Rear Left Corner 
        /// This variable will store ALL the information which each of the GUI Controls of this Class (such as <see cref="xuC_KO_Camber1"/>) have to offer
        /// </summary>
        KO_CornverVariables ko_CV_RL;

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> Class representing the Rear Right Corner 
        /// This variable will store ALL the information which each of the GUI Controls of this Class (such as <see cref="xuC_KO_Camber1"/>) have to offer
        /// </summary>
        KO_CornverVariables ko_CV_RR;

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/> which contains crucial information regarding the Parameters of the Simulation which affect the Charts in this UserControl
        /// </summary>
        KO_SimulationParams ko_SimParams;

        public XUC_KinematicOptimization()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <para>---1st--- This methid is to be called first</para>
        /// <para>Method to initialize the crucial <see cref="KO_CornverVariables"/> object of each of the corners</para>
        /// </summary>
        /// <param name="_koFL">Front Left <see cref="KO_CornverVariables"/> object</param>
        /// <param name="_koFR">Front Right <see cref="KO_CornverVariables"/> object</param>
        /// <param name="_koRL">Rear Left <see cref="KO_CornverVariables"/> object</param>
        /// <param name="_koRR">Rear Right <see cref="KO_CornverVariables"/> object</param>
        public void GetCornerVariablesData(KO_CornverVariables _koFL, KO_CornverVariables _koFR, KO_CornverVariables _koRL, KO_CornverVariables _koRR)
        {
            ko_CV_FL = _koFL;

            ko_CV_FR = _koFR;

            ko_CV_RL = _koRL;

            ko_CV_RR = _koRR;
        }

        /// <summary>
        /// <para>---2nd--- This method is to be called 2nd</para>
        /// <para>Method to initialize the Simulation Parameters which contains crucial information regarding the Parameters of the Simulation which affect the Charts in this UserControl</para>
        /// </summary>
        /// <param name="_koSimParams">Object of the <see cref="KO_SimulationParams"/> which contains crucial information regarding the Parameters of the Simulation which affect the Charts in this UserControl</param>
        public void GetSimulationParameters(KO_SimulationParams _koSimParams)
        {
            ko_SimParams = _koSimParams;
        }

        private void dockPanelAckermann_Click(object sender, EventArgs e)
        {

        }
    }
}

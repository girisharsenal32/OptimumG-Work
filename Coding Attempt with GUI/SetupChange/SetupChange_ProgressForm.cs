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

namespace Coding_Attempt_with_GUI
{
    public partial class SetupChange_ProgressForm : DevExpress.XtraEditors.XtraForm
    {
        public SetupChange_ProgressForm()
        {
            InitializeComponent();
        }


        internal void InitializepProgressBar(int _step, int _maxValue)
        {
            progressBar1.Maximum = _maxValue;

            progressBar1.Step = _step;
        }


        /// <summary>
        /// Method to Update the Values of the Convergance on the Form and to update the Progress Bar
        /// </summary>
        /// <param name="_casterConv">Convergence of Caster</param>
        /// <param name="_kpiConv">Convergence of KPI</param>
        /// <param name="_camberConv">Convergence of Camber</param>
        /// <param name="_toeConv">Convergence of Toe</param>
        /// <param name="_bsConv">Convergence of BS</param>
        /// <param name="_total">Total Convergence</param>
        internal void UpdateValues(Convergence _casterConv, Convergence _kpiConv, Convergence _camberConv, Convergence _toeConv, Convergence _bsConv, Convergence _total)
        {
            textBoxCasterConv.Text = _casterConv.ConvergenceStatus;

            textBoxKPIConv.Text = _kpiConv.ConvergenceStatus;

            textBoxCamberConv.Text = _camberConv.ConvergenceStatus;

            textBoxToeConv.Text = _toeConv.ConvergenceStatus;

            textBoxBSConv.Text = _bsConv.ConvergenceStatus;

            textBoxRMSConv.Text = _total.ConvergenceStatus;

            progressBar1.PerformStep();

            progressBar1.Update();
        }

        internal void SetProgressBarValue(int _value)
        {
            progressBar1.Value = _value;
        }



    }
}
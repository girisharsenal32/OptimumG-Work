using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Units;
using devDept.Geometry;



namespace Coding_Attempt_with_GUI
{
    public class KO_Solver : SolverMasterClass
    {


        /// <summary>
        /// Object of the <see cref="KO_Optimizer_BumpSteer"/> which runs a Genetic Algorithm to solve compute the Bump Steer
        /// </summary>
        public KO_Optimizer_BumpSteer BS_Optimization;

        public KO_Optimizer_ActuationPoints Actuation_Opt;

        int NoOptimizerGenerattions;

        /// <summary>
        /// Object of the <see cref=" KO_CornverVariables"/>
        /// </summary>
        public KO_CornverVariables KO_CV;

        /// <summary>
        /// Object of the <see cref="KO_CentralVariables"/> which holds Variables central to the Vehicle
        /// </summary>
        public KO_CentralVariables KO_Central;

        /// <summary>
        /// Object of the <see cref="VehicleCorner"/> to identify the Vehicle Corner
        /// </summary>
        public VehicleCorner VCorner { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/> CLass which contains crucial information regarding the Number of Iterations etc
        /// </summary>
        public KO_SimulationParams SimParams;

        /// <summary>
        /// Object of the <see cref="DesignForm"/> Class which is contains the progressbar to be updates
        /// </summary>
        DesignForm Design_Form;

        OptimizaionParameter optParam;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/> Class</param>
        /// <param name="_vCorner">Corner of the Vehicle which is being computed</param>
        /// <param name="_vehicle">The <see cref="Vehicle"/> item itself</param>
        public KO_Solver(ref KO_CornverVariables _koCV, ref KO_CentralVariables _koCentral, ref KO_SimulationParams _simpParams, VehicleCorner _vCorner, ref DesignForm _designForm, OptimizaionParameter _optParam)
        {
            KO_CV = _koCV;

            VCorner = _vCorner;

            KO_Central = _koCentral;

            SimParams = _simpParams;

            Design_Form = _designForm;

            optParam = _optParam;

            if (optParam == OptimizaionParameter.BumpSteer)
            {
                Initialize_Optimizer_BS(); 
            }
            else if (optParam == OptimizaionParameter.SpringMotionRatio)
            {
                Initialize_Optimizer_ActuationPoints_RockerPoints();
            }
            else if (optParam == OptimizaionParameter.ARBMotionRatio)
            {

            }
        }

        /// <summary>
        /// Method to Initialize the <see cref="KO_Optimizer_BumpSteer"/>
        /// </summary>
        private void Initialize_Optimizer_BS()
        {
            NoOptimizerGenerattions = 100;

            InitializeProgressBarr();

            BS_Optimization = new KO_Optimizer_BumpSteer(0.85, 0.05, 5, NoOptimizerGenerattions, VCorner);

            BS_Optimization.Initialize_CornverVariables(ref KO_Central.Vehicle, ref SimParams, ref KO_CV, this);

            BS_Optimization.ConstructGeneticAlgorithm(150);
        }

        private void Initialize_Optimizer_ActuationPoints_RockerPoints()
        {
            NoOptimizerGenerattions = 100;

            InitializeProgressBarr();

            Actuation_Opt = new KO_Optimizer_ActuationPoints(0.85, 0.05, 5, NoOptimizerGenerattions, VCorner);

            Actuation_Opt.Initialize_CornverVariables(ref KO_Central.Vehicle, ref SimParams, ref KO_CV, this);

            Actuation_Opt.ConstructGeneticAlgorithm(150);
        }

        /// <summary>
        /// Method to Initialize the <see cref="DesignForm.toolStripProgressBar1"/>
        /// </summary>
        private void InitializeProgressBarr()
        {
            Design_Form.toolStripProgressBar1.ProgressBar.Value = 0;

            Design_Form.toolStripProgressBar1.ProgressBar.Update(); 

            Design_Form.toolStripProgressBar1.ProgressBar.Maximum = NoOptimizerGenerattions;

            Design_Form.toolStripProgressBar1.Step = 1;

            Design_Form.toolStripProgressBar1.ProgressBar.Show();
        }

        /// <summary>
        /// Method to update the Progress Bar 
        /// </summary>
        public void UpdateProgressBar()
        {
            Design_Form.toolStripProgressBar1.ProgressBar.PerformStep();

            Design_Form.toolStripProgressBar1.ProgressBar.Update();
        }

        /// <summary>
        /// Method to Set the Progress Bar to Max Value and then Hide it
        /// </summary>
        /// <param name="_value"></param>
        public void MaxProgressBar(int _value)
        {
            Design_Form.toolStripProgressBar1.ProgressBar.Value = _value;

            Design_Form.toolStripProgressBar1.ProgressBar.Update();

            Design_Form.toolStripProgressBar1.ProgressBar.Hide();
        }



    }
}

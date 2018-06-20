using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Units;
using devDept.Geometry;



namespace Coding_Attempt_with_GUI
{
    public class KO_BumpSteer_Solver : SolverMasterClass
    {


        /// <summary>
        /// Object of the <see cref="KO_Optimizer_BumpSteer"/> which runs a Genetic Algorithm to solve compute the Bump Steer
        /// </summary>
        public KO_Optimizer_BumpSteer BS_Optimization;

        /// <summary>
        /// Object of the <see cref=" KO_CornverVariables"/>
        /// </summary>
        public KO_CornverVariables KO_CV { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_CentralVariables"/> which holds Variables central to the Vehicle
        /// </summary>
        public KO_CentralVariables KO_Central { get; set; }

        /// <summary>
        /// Object of the <see cref="VehicleCorner"/> to identify the Vehicle Corner
        /// </summary>
        public VehicleCorner VCorner { get; set; }


        public KO_SimulationParams SimParams { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/> Class</param>
        /// <param name="_vCorner">Corner of the Vehicle which is being computed</param>
        /// <param name="_vehicle">The <see cref="Vehicle"/> item itself</param>
        public KO_BumpSteer_Solver(KO_CornverVariables _koCV, KO_CentralVariables _koCentral, KO_SimulationParams _simpParams, VehicleCorner _vCorner, SuspensionType _susType)
        {
            DWSolver = new DoubleWishboneKinematicsSolver();

            KO_CV = _koCV;

            VCorner = _vCorner;

            KO_Central = _koCentral;

            SimParams = _simpParams;

            Solve_rBumpSteer();
        }

        private void Initialize_Optimizer()
        {
            BS_Optimization = new KO_Optimizer_BumpSteer(0.85, 0.05, 5, 100, VCorner);

            BS_Optimization.Initialize_CornverVariables(KO_Central.Vehicle, SimParams, KO_CV);

            BS_Optimization.ConstructGeneticAlgorithm(150);
        }
        
        private void Solve_rBumpSteer()
        {
            
        }




    }
}

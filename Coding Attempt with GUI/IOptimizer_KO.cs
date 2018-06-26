using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;


namespace Coding_Attempt_with_GUI
{
    public interface IOptimizer_KO
    {

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed</param>
        /// <param name="_koSim">Object of the <see cref="KO_SimulationParams"/></param>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koSolver">Object of the <see cref="KO_Solver"/></param>
        void Initialize_CornverVariables(ref Vehicle _vehicle, ref KO_SimulationParams _koSim, ref KO_CornverVariables _koCV, KO_Solver _koSolver);

        /// <summary>
        /// ---3rd--- To be called 3rd
        /// Method to Constrcut the <see cref="GAF.GeneticAlgorithm"/>Class's object and assing it with the relevant operators, erroc functions etc
        /// </summary>
        /// <param name="_popSize">Size of the Population</param>
        void ConstructGeneticAlgorithm(int _popSize);

        /// <summary>
        /// The PRIMARY Error function which the <see cref="IOptimizer.GA"/> calls
        /// </summary>
        /// <param name="chromosome">Chromosome which is prodced by the Algorithm</param>
        /// <returns>Fitness of the <paramref name="chromosome"/></returns>
        double EvaluateFitnessCurve(Chromosome chromosome);

        /// <summary>
        /// The PRIMARY Terminate function called by the <see cref="IOptimizer.GA"/>
        /// </summary>
        /// <param name="_population">Will be taken care of by <see cref="GAF"/></param>
        /// <param name="_currGeneration">Will be taken care of by <see cref="GAF"/></param>
        /// <param name="currEvaluation">Will be taken care of by <see cref="GAF"/></param>
        /// <returns><see cref="bool"/> to determine whether the Algorithm should continue</returns>
        bool TerminateAlgorithm(Population _population, int _currGeneration, long currEvaluation);

        /// <summary>
        /// Method to Compute Error in the Bump Steer 
        /// </summary>
        /// <returns>Returns calculated error</returns>
        double Compute_MainError();
    }
}

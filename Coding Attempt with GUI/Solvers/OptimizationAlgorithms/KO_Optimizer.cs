using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;

namespace Coding_Attempt_with_GUI
{
    public class KO_Optimizer : Master_Optimizer
    {
        /// <summary>
        /// Local Object of the <see cref="KO_CornverVariables"/> containing ALL relevant information of the Front Left Corner
        /// </summary>
        KO_CornverVariables KO_CV_FL;

        /// <summary>
        /// Local Object of the <see cref="KO_CornverVariables"/> containing ALL relevant information of the Front Right Corner
        /// </summary>
        KO_CornverVariables KO_CV_FR;

        /// <summary>
        /// Local Object of the <see cref="KO_CornverVariables"/> containing ALL relevant information of the Rear Left Corner
        /// </summary>
        KO_CornverVariables KO_CV_RL;

        /// <summary>
        /// Local Object of the <see cref="KO_CornverVariables"/> containing ALL relevant information of the Rear Right Corner
        /// </summary>
        KO_CornverVariables KO_CV_RR;

        /// <summary>
        /// Local Object of the <see cref="KO_SimulationParams"/> 
        /// </summary>
        public KO_SimulationParams KO_SimParams;

        /// <summary>
        /// Object of the <see cref="KO_Outputs"/> Class which will store all the Outputs of the Front Left Corner
        /// </summary>
        public KO_Outputs KO_OP_FL { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_Outputs"/> Class which will store all the Outputs of the Front Right Corner
        /// </summary>
        public KO_Outputs KO_OP_FR { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_Outputs"/> Class which will store all the Outputs of the Rear left Corner
        /// </summary>
        public KO_Outputs KO_OP_RL { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_Outputs"/> Class which will store all the Outputs of the Rear Right Corner
        /// </summary>
        public KO_Outputs KO_OP_RR { get; set; }


        /// <summary>
        /// Number of Iterations that the Kinemaic Solver is going to Run for 
        /// </summary>
        public int NumberOfIterations;

        /// <summary>
        /// <para>---1st--- This mehod is to be called First</para>
        /// <para>The Constructor of the <see cref="SetupChange_Optimizer"/> Class</para>
        /// </summary>
        /// <param name="_crossover">Percentage of the Population which is going to be subject to Crossover</param>
        /// <param name="_mutation">Percentage of the Population which is going to be subject to Random Mutation</param>
        /// <param name="_elites">Percentage of the population which is going to be treated as Elite without an modifications to the Genes</param>
        /// <param name="_popSize">Size of the Population</param>
        /// <param name="_chromoseLength"> <para>Chromosome Length of the <see cref="Population"/></para>
        /// <para>Decided based on 2 things: The number of Genes and the bit size required for each Gene</para>
        /// <para>https://gaframework.org/wiki/index.php/How_to_Encode_Parameters for more information</para> </param>
        public KO_Optimizer(double _crossover, double _mutation, int _elites, int _noOfGenerations)
        {
            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;

            No_Generations = _noOfGenerations;

            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Opt_AdjToolValues = new Dictionary<string, double>();
        }

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed </param>
        /// <param name="_koCVFL"></param>
        /// <param name="_koCVFR"></param>
        /// <param name="_koCVRL"></param>
        /// <param name="_koCVRR"></param>
        public void Initialize_CornverVariables(Vehicle _vehicle, KO_SimulationParams _koSim, KO_CornverVariables _koCVFL, KO_CornverVariables _koCVFR, KO_CornverVariables _koCVRL, KO_CornverVariables _koCVRR)
        {
            Vehicle = _vehicle;

            Chassis = _vehicle.chassis_vehicle;

            KO_SimParams = _koSim;

            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object</summary>
            KO_CV_FL = _koCVFL;
            ///<summary>Initializing the <see cref="VehicleCornerParams"/> object of the <see cref="KO_CornverVariables"/> Class</summary>
            KO_CV_FL.VCornerParams = KO_CV_FL.Initialize_VehicleCornerParams(Vehicle, VehicleCorner.FrontLeft, KO_SimParams.NumberOfIterations_KinematicSolver);
            ///<see cref="Initializing the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_CornverVariables.KO_MasterAdjs"/> "/>
            KO_CV_FL.Initialize_AdjusterCoordinates(KO_CV_FL.VCornerParams);


            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object</summary>
            KO_CV_FR = _koCVFR;
            ///<summary>Initializing the <see cref="VehicleCornerParams"/> object of the <see cref="KO_CornverVariables"/> Class</summary>
            KO_CV_FR.VCornerParams = KO_CV_FR.Initialize_VehicleCornerParams(Vehicle, VehicleCorner.FrontRight, KO_SimParams.NumberOfIterations_KinematicSolver);
            ///<see cref="Initializing the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_CornverVariables.KO_MasterAdjs"/> "/>
            KO_CV_FR.Initialize_AdjusterCoordinates(KO_CV_FR.VCornerParams);


            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object</summary>
            KO_CV_RL = _koCVRL;
            ///<summary>Initializing the <see cref="VehicleCornerParams"/> object of the <see cref="KO_CornverVariables"/> Class</summary>
            KO_CV_RL.VCornerParams = KO_CV_RL.Initialize_VehicleCornerParams(Vehicle, VehicleCorner.RearLeft, KO_SimParams.NumberOfIterations_KinematicSolver);
            ///<see cref="Initializing the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_CornverVariables.KO_MasterAdjs"/> "/>
            KO_CV_RL.Initialize_AdjusterCoordinates(KO_CV_RL.VCornerParams);

            
            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object</summary>
            KO_CV_RR = _koCVRR;
            ///<summary>Initializing the <see cref="VehicleCornerParams"/> object of the <see cref="KO_CornverVariables"/> Class</summary>
            KO_CV_RR.VCornerParams = KO_CV_RR.Initialize_VehicleCornerParams(Vehicle, VehicleCorner.RearRight, KO_SimParams.NumberOfIterations_KinematicSolver);
            ///<see cref="Initializing the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_CornverVariables.KO_MasterAdjs"/> "/>
            KO_CV_RR.Initialize_AdjusterCoordinates(KO_CV_RR.VCornerParams);

        }


        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;
using devDept.Geometry;
using devDept.Eyeshot.Entities;
using MathNet.Spatial.Units;


namespace Coding_Attempt_with_GUI
{
    public class KO_Master_Optimizer : IOptimizer
    {
        #region ---Interface Implementation---
        public double CrossOverProbability { get; set; }

        public Crossover Crossovers { get; set; }

        public double MutationProbability { get; set; }

        public BinaryMutate Mutations { get; set; }

        public int ElitePercentage { get; set; }

        public Elite Elites { get; set; }

        public int PopulationSize { get; set; }

        public Population Population { get; set; }

        public int ChromosomeLength { get; set; }

        public GeneticAlgorithm GA { get; set; }

        public FitnessFunction EvaluateFitnessOfGeneticAlforithm { get; set; }

        public TerminateFunction Terminate { get; set; }

        public Dictionary<string, double> Opt_AdjToolValues { get; set; }

        public int No_Generations { get; set; }

        public double MaximumErrorOfGeneration { get; set; }

        public int SolutionCounter { get; set; }

        public DoubleWishboneKinematicsSolver DWSolver { get; set; }

        public McPhersonKinematicsSolver McPSolver { get; set; }

        public Vehicle Vehicle { get; set; }

        public Chassis Chassis { get; set; } 
        #endregion
    }

    public class KO_Optimizer_BumpSteer : KO_Master_Optimizer, IOptimizer_KO
    {
        #region ---Declarations---
        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> Class containing all the relevant information of the Corner which is being considered here
        /// </summary>
        KO_CornverVariables KO_CV;

        /// <summary>
        /// Object of the <see cref="KO_Solver"/> Class which is the Parent SOlver which calls on the Optimization Methods
        /// </summary>
        KO_Solver ParentSolver;

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/> which contains relevant information pertaining to the Simulation params such as number of iterations etc
        /// </summary>
        public KO_SimulationParams KO_SimParams;

        /// <summary>
        /// Vehicle Corner
        /// </summary>
        VehicleCorner VCorner;

        /// <summary>
        /// <see cref="List{Angle}"/> representing the Toe Angle variation for each deflection of the Wheel. 
        /// This is Bump Steer Graph which is computed for a given iteration of the <see cref="IOptimizer.GA"/> 
        /// </summary>
        List<Angle> Calc_BumpSteerGraph;

        /// <summary>
        /// Inboard Toe Link Point which is being optimized
        /// </summary>
        Point3D InboardToeLink;

        int ChromosomeLength;

        int BitSize;

        #endregion



        #region ---Initialization Methods---

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
        public KO_Optimizer_BumpSteer(double _crossover, double _mutation, int _elites, int _noOfGenerations, VehicleCorner _vCorner)
        {
            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;

            No_Generations = _noOfGenerations;

            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Opt_AdjToolValues = new Dictionary<string, double>();

            VCorner = _vCorner;

            ChromosomeLength = 30;

            BitSize = 10;
        }

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed</param>
        /// <param name="_koSim">Object of the <see cref="KO_SimulationParams"/></param>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koSolver">Object of the <see cref="KO_Solver"/></param>
        public void Initialize_CornverVariables(ref Vehicle _vehicle, ref KO_SimulationParams _koSim, ref KO_CornverVariables _koCV, KO_Solver _koSolver)
        {
            Vehicle = _vehicle;

            //Chassis = _vehicle.chassis_vehicle;

            KO_SimParams = _koSim;

            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object</summary>
            KO_CV = _koCV;

            ///<summary>Initializing the <see cref="VehicleCornerParams"/> object of the <see cref="KO_CornverVariables"/> Class</summary>
            KO_CV.Initialize_VehicleCornerParams(ref KO_CV, Vehicle, VehicleCorner.FrontLeft, KO_SimParams.NumberOfIterations_KinematicSolver);

            ///<see cref="Initializing the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_CornverVariables.KO_MasterAdjs"/> "/>
            KO_CV.Initialize_AdjusterCoordinates(KO_CV.VCornerParams);

            ParentSolver = _koSolver;

        }

        /// <summary>
        /// ---3rd--- To be called 3rd
        /// Method to Constrcut the <see cref="GAF.GeneticAlgorithm"/>Class's object and assing it with the relevant operators, erroc functions etc
        /// </summary>
        /// <param name="_popSize">Size of the Population</param>
        public void ConstructGeneticAlgorithm(int _popSize)
        {
            PopulationSize = _popSize;

            Population = new Population(PopulationSize, ChromosomeLength, false, true);

            EvaluateFitnessOfGeneticAlforithm = EvaluateFitnessCurve;

            Terminate = TerminateAlgorithm;

            GA = new GeneticAlgorithm(Population, EvaluateFitnessOfGeneticAlforithm);

            GA.OnGenerationComplete += GA_OnGenerationComplete;

            GA.OnRunComplete += GA_OnRunComplete;


            GA.Operators.Add(Elites);
            GA.Operators.Add(Crossovers);
            GA.Operators.Add(Mutations);

            GA.Run(Terminate);
        }

        #endregion



        #region ---Genetic Algorithm Methods---

        /// <summary>
        /// The PRIMARY Error function which the <see cref="IOptimizer.GA"/> calls
        /// </summary>
        /// <param name="chromosome">Chromosome which is prodced by the Algorithm</param>
        /// <returns>Fitness of the <paramref name="chromosome"/></returns>
        public double EvaluateFitnessCurve(Chromosome chromosome)
        {
            double fitness = -1;

            if (chromosome != null)
            {
                ///<summary>Extracting the Coordinates from the <see cref="IOptimizer.GA"/> generated Chromosome</summary>
                ExtractFromChromosome(chromosome, out double x, out double y, out double z);
                InboardToeLink = new Point3D(x, y, z);
                KO_CV.VCornerParams.InboardAssembly[CoordinateOptions.ToeLinkInboard.ToString()] = InboardToeLink;

                fitness = 1 - Compute_MainError();

                KO_CV.BumpSteerConvergence = new Convergence(System.Math.Round(fitness, 2));

            }

            return fitness;
        }

        /// <summary>
        /// Eventt fired when a particular Generation of the <see cref="IOptimizer.GA"/> has been solved 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GA_OnGenerationComplete(object sender, GaEventArgs e)
        {
            double Fitness = e.Population.MaximumFitness;

            Point3D temp = InboardToeLink.Clone() as Point3D;

            int Generations = e.Generation;

            ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
            var chromosome = e.Population.GetTop(1)[0];

            ExtractFromChromosome(chromosome, out double x, out double y, out double z);
            InboardToeLink = new Point3D(x, y, z);

            EvaluateFitnessCurve(chromosome);

            ParentSolver.UpdateProgressBar();
        }

        /// <summary>
        /// Evnent fired when <see cref="IOptimizer.GA"/> has solved the Genetic Algorithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GA_OnRunComplete(object sender, GaEventArgs e)
        {
            double Fitness = e.Population.MaximumFitness;

            Point3D temp = InboardToeLink.Clone() as Point3D;

            int Generations = e.Generation;

            ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
            var chromosome = e.Population.GetTop(1)[0];

            ExtractFromChromosome(chromosome, out double x, out double y, out double z);
            InboardToeLink = new Point3D(x, y, z);

            EvaluateFitnessCurve(chromosome);

            KO_CV.VCornerParams.ToeLinkInboard = InboardToeLink;

            KO_CV.VCornerParams.InboardAssembly[CoordinateOptions.ToeLinkInboard.ToString()] = InboardToeLink;

            ParentSolver.MaxProgressBar(No_Generations);

        }

        /// <summary>
        /// The PRIMARY Terminate function called by the <see cref="IOptimizer.GA"/>
        /// </summary>
        /// <param name="_population">Will be taken care of by <see cref="GAF"/></param>
        /// <param name="_currGeneration">Will be taken care of by <see cref="GAF"/></param>
        /// <param name="currEvaluation">Will be taken care of by <see cref="GAF"/></param>
        /// <returns><see cref="bool"/> to determine whether the Algorithm should continue</returns>
        public bool TerminateAlgorithm(Population _population, int _currGeneration, long currEvaluation)
        {
            if (_currGeneration > No_Generations)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion



        #region ---Helper Methods

        /// <summary>
        /// Method to Extract the Inboard Toe Link Point from the Chromosome
        /// </summary>
        /// <param name="chromosome">The Chromosome generated by the Optimzier</param>
        /// <param name="_x">X Coordinate</param>
        /// <param name="_y">Y Coordinate</param>
        /// <param name="_z">Z Coordinate</param>
        private void ExtractFromChromosome(Chromosome chromosome, out double _x, out double _y, out double _z)
        {
            ///<summary>
            ///Assigning the Nominal,Upper and Lower Values of the X/Y/Z Coordinates. This will be later on obtained from the User
            /// </summary>
            double nominalX = KO_CV.VCornerParams.ToeLinkInboard.X;
            ///<remarks>
            ///Upper and Lower limits of the X Coordinate.
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaX = 100;
            double lowerDeltaX = -100;

            double nominalY = KO_CV.VCornerParams.ToeLinkInboard.Y;
            ///<remarks>
            ///Upper and Lower limits of the X Coordinate.
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaY = 100;
            double lowerDeltaY = -100;

            double nominalZ = KO_CV.VCornerParams.ToeLinkInboard.Z;
            ///<remarks>
            ///Upper and Lower limits of the X Coordinate.
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaZ = 100;
            double lowerDeltaZ = -100;

            /// Range constant for x
            var rcx = GAF.Math.GetRangeConstant(upperDeltaX - lowerDeltaX, BitSize);

            /// Range constant for y
            var rcy = GAF.Math.GetRangeConstant(upperDeltaY - lowerDeltaY, BitSize);

            /// Range constant for z
            var rcz = GAF.Math.GetRangeConstant(upperDeltaZ - lowerDeltaZ, BitSize);

            /// Retrieving the 10 bit values as from the <param name="chromosome"
            var x1 = Convert.ToInt32(chromosome.ToBinaryString(0, BitSize), 2);
            var y1 = Convert.ToInt32(chromosome.ToBinaryString(BitSize, BitSize), 2);
            var z1 = Convert.ToInt32(chromosome.ToBinaryString(BitSize * 2, BitSize), 2);

            /// Multiplying by the appropriate range constant and adjusting for any in the range (Lower Delta is the Offset) to get the real values
            ///<remarks>
            ///https://gaframework.org/wiki/index.php/How_to_Encode_Parameters
            ///Visit link above for more information on the code below
            /// </remarks>
            _x = System.Math.Round((x1 * rcx) + (nominalX + lowerDeltaX), 3);
            _y = System.Math.Round((y1 * rcy) + (nominalY + lowerDeltaY), 3);
            _z = System.Math.Round((z1 * rcz) + (nominalZ + lowerDeltaZ), 3);
        }

        /// <summary>
        /// Method to Compute Error in the Bump Steer 
        /// </summary>
        /// <returns>Returns calculated error</returns>
        public double Compute_MainError()
        {
            DWSolver = new DoubleWishboneKinematicsSolver();

            BumpSteerSolver_FirstPass(KO_CV.BumpSteerCurve.WheelDeflections, Vehicle, DWSolver);

            ExtractToeAngles(KO_CV.VCornerParams.OC_BumpSteer);

            double bsError = EvaluateDeviation_BSChart(Calc_BumpSteerGraph, KO_CV.BumpSteerCurve.ToeAngles);

            if (bsError > 1)
            {
                return 0.99;
            }
            else if (bsError < 0)
            {
                return 0.99;
            }
            else
            {
                return bsError;
            }
        }

        Dictionary<string, Point3D> TempOutboardAssembly;
        List<double> WheelDeflection_Deltas;

        /// <summary>
        /// Method which calls the Solver methods for Kinematic pass to solve for Bump Steer
        /// </summary>
        /// <param name="_wheelDeflections"><see cref="List{Double}"/> of Wheel Deflections over which the Kinematics Computation is done </param>
        /// <param name="_OC"><see cref="List{OutputClass}"/></param>
        /// <param name="_Vehicle"><see cref="Vehicle"/> itemn</param>
        public void BumpSteerSolver_FirstPass(List<double> _wheelDeflections, Vehicle _Vehicle, DoubleWishboneKinematicsSolver _DwSolver)
        {
            ///<summary>
            ///Creating a Temporary Dictionary of Outboard Points. 
            ///This is necessary because this is just a simulation to evaluate the Bump Steer. The actual Outboard Points don't need to change. If i use the <see cref="VehicleCornerParams.OutboardAssembly"/>
            ///then it will be changed as the points changed and this is not desired
            /// </summary>
            TempOutboardAssembly = new Dictionary<string, Point3D>();
            foreach (string coordinate in KO_CV.VCornerParams.OutboardAssembly.Keys)
            {
                TempOutboardAssembly.Add(coordinate, KO_CV.VCornerParams.OutboardAssembly[coordinate].Clone() as Point3D);
            }

            ///<summary>Creating a List which holds the DELTAS of wheel deflection as it is the deltas of deflection that we are concerned with and not the actual or Absolute Wheel Deflections</summary>
            WheelDeflection_Deltas = new List<double>();

            for (int i = 0; i < _wheelDeflections.Count; i++)
            {
                if (i==0)
                {
                    WheelDeflection_Deltas.Add(0);
                }
                else
                {
                    WheelDeflection_Deltas.Add(_wheelDeflections[i] - _wheelDeflections[i - 1]);
                }
            }

            ///<summary>Initializing the Local Coordinates of the <see cref="SolverMasterClass"/> computed so far</summary>
            _DwSolver.Assign_LocalCoordinateVariables_WishbonePoints(KO_CV.VCornerParams.InboardAssembly, KO_CV.VCornerParams.OutboardAssembly);

            for (int i = 0; i < WheelDeflection_Deltas.Count; i++)
            {

                SolveKinematics_BumpSteerEval(WheelDeflection_Deltas[i], _Vehicle, i, _DwSolver);
            }

        }

        /// <summary>
        /// Kinematics Solver method which performs the Kinematic Pass for the given motion
        /// </summary>
        /// <param name="_wheelDeflection">Wheel Deflections</param>
        /// <param name="_oc">Output Class LIst </param>
        /// <param name="_vehicle">Vehicle Item</param>
        /// <param name="_index">Index at which the Motion. </param>
        /// <param name="_dwSolver"><see cref="DoubleWishboneKinematicsSolver"/> Object</param>
        private void SolveKinematics_BumpSteerEval(double _wheelDeflection, Vehicle _vehicle, int _index, DoubleWishboneKinematicsSolver _dwSolver)
        {

            /////<summary>Initializing the Local Coordinates of the <see cref="SolverMasterClass"/> computed so far</summary>
            //_dwSolver.Assign_LocalCoordinateVariables_WishbonePoints(KO_CV.VCornerParams.InboardAssembly, KO_CV.VCornerParams.OutboardAssembly);

            _dwSolver.CalculateInitialSpindleVector();

            ///Running a Kinematics Pass
            _dwSolver.KO_InitialStage_BS_LBJ(_wheelDeflection, out MathNet.Spatial.Euclidean.Point3D E);

            _dwSolver.KO_InitialStage_BS_UBJ(E, out MathNet.Spatial.Euclidean.Point3D F);

            _dwSolver.KO_InitialStage_BS_ToeLinnkOutboard(_wheelDeflection, E, F, out MathNet.Spatial.Euclidean.Point3D M);

            _dwSolver.KO_InitialStage_BS_WheelCenter(E, F, M, out MathNet.Spatial.Euclidean.Point3D K);

            _dwSolver.KO_InitialStage_BS_WheelSpindleEnd(E, K, M, out MathNet.Spatial.Euclidean.Point3D L);

            _dwSolver.KO_InitialStage_BumpSteer_ContactPatch(E, K, M, out MathNet.Spatial.Euclidean.Point3D W);

            _dwSolver.CalculatenewCamberAndToe_KO_Optimization(KO_CV.VCornerParams.OC_BumpSteer, _index, _vehicle, (int)VCorner, new Point3D(L.X, L.Y, L.Z), new Point3D(K.X, K.Y, K.Z));


            ///<summary>Assigning all the computed points to a the <see cref="UnsprungAssembly"/> dictionary</summary>
            TempOutboardAssembly[CoordinateOptions.UBJ.ToString()] = new Point3D(F.X, F.Y, F.Z);

            TempOutboardAssembly[CoordinateOptions.LBJ.ToString()] = new Point3D(E.X, E.Y, E.Z);

            TempOutboardAssembly[CoordinateOptions.ToeLinkOutboard.ToString()] = new Point3D(M.X, M.Y, M.Z);

            TempOutboardAssembly[CoordinateOptions.WheelCenter.ToString()] = new Point3D(K.X, K.Y, K.Z);

            TempOutboardAssembly[CoordinateOptions.WheelSpindleEnd.ToString()] = new Point3D(L.X, L.Y, L.Z);

            TempOutboardAssembly[CoordinateOptions.ContactPatch.ToString()] = new Point3D(W.X, W.Y, W.Z);
           

            ///<summary>Initializing the Local Coordinates of the <see cref="SolverMasterClass"/> computed so far</summary>
            _dwSolver.Assign_LocalCoordinateVariables_WishbonePoints(KO_CV.VCornerParams.InboardAssembly, TempOutboardAssembly);

        }

        /// <summary>
        /// Method to Extract the Toe Angles from the <see cref="List{OutputClass}"/>
        /// </summary>
        /// <param name="_oc"></param>
        private void ExtractToeAngles(List<OutputClass> _oc)
        {
            ///<summary>Initializing a temporary List to perform some pre-processig activaties</summary>
            List<Angle> temp = new List<Angle>();

            ///<summary>Initialzing the main List to compare the Bump Steer</summary>
            Calc_BumpSteerGraph = new List<Angle>();

            ///<summary>
            ///---IMPORTANT---
            ///Notice that I have done _oc.Count-1 instead of _oc.Count
            ///This is because while generting the Bump Steer Graph I added an extra element to the end of the List
            ///This is done so that the last deflection of the <see cref="CustomBumpSteerParams.WheelDeflections"/> also produces tangible results. If the extra element is not there then the Results at the 
            ///last wheel deflection would ALL be zero because of Lack of DELTA to compute from 
            /// </summary>
            for (int i = 0; i < _oc.Count - 1; i++)
            {
                temp.Add(new Angle(_oc[i].waOP.StaticToe, AngleUnit.Radians));
            }

            ///<summary>
            ///Not doing temp.Count-1 here as the (-1) has already been applied above
            /// </summary>
            for (int i = 0; i < temp.Count; i++)
            {
                if (i >= KO_CV.BumpSteerCurve.HighestBumpindex)
                {
                    Calc_BumpSteerGraph.Add(temp[i]);
                }
            }

            //Setup_OP.Calc_BumpSteerChart = Calc_BumpSteerGraph.ToList();
        }

        /// <summary>
        /// Method which returns the deviation of the Toe Angle variation computed by the solver with the Toe Angle Variation which the user has provided
        /// That is, this method computes the deviation between the Computed Bump Steer Graph against the Requested Bump Steer Graph
        /// </summary>
        /// <param name="_calculatedToeAngles">Toe Angle Variation or Bump Steer Chart computed  by the Kinematic SOlver </param>
        /// <param name="_requestedToeAngles">Toe Angle Variation or Bump Steer requested by the user</param>
        /// <returns></returns>
        private double EvaluateDeviation_BSChart(List<Angle> _calculatedToeAngles, List<Angle> _requestedToeAngles)
        {
            List<Angle> UserBumpSteerCurve = _requestedToeAngles;

            List<Angle> ErrorCalc_Step1 = new List<Angle>();

            ///<summary>Finding the distance between each pair of Points</summary>
            for (int i = 0; i < _requestedToeAngles.Count; i++)
            {
                if (i != 0 && i != _requestedToeAngles.Count - 1)
                {
                    ///<remarks>Not deleting the Commented out sections because they are different methods to evaluate error and I wanna do a few more iterations to tes tthem </remarks>

                    //if (i != SuspensionEvalIterations - 1)
                    //{

                    //ErrorCalc_Step1.Add(new Angle((_toeAngle[i].Degrees - UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));

                    //if (UserBumpSteerCurve[i].Degrees != 0)
                    //{
                    //    ErrorCalc_Step1.Add(new Angle(((_toeAngle[i - 1].Degrees - UserBumpSteerCurve[i].Degrees) / UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));
                    //}
                    //else
                    //{
                    ErrorCalc_Step1.Add(new Angle((_calculatedToeAngles[i - 1].Degrees - UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));
                    //}
                    //}
                    //else
                    //{
                    //    ErrorCalc_Step1.Add(ErrorCalc_Step1[i - 1]);
                    //} 
                }
            }

            List<Angle> ErrorCalc_Step2 = new List<Angle>();

            ///<summary>Finiding the square of the Distance</summary>
            for (int i = 0; i < ErrorCalc_Step1.Count; i++)
            {
                ErrorCalc_Step2.Add(new Angle(ErrorCalc_Step1[i].Degrees * ErrorCalc_Step1[i].Degrees, AngleUnit.Degrees));
            }

            double ErrorCalc_Step3 = 0;

            ///<summary>Summing the squares of the distances</summary>
            for (int i = 0; i < ErrorCalc_Step2.Count; i++)
            {
                ErrorCalc_Step3 += ErrorCalc_Step2[i].Degrees;
            }

            ///<summary>Computing the Final Error by finding the Square Root of the Squares of the distances and dividing it by the No. of Iterations</summary>
            ///<remarks>Not deleting the Commented out sections because they are different methods to evaluate error and I wanna do a few more iterations to tes tthem </remarks>

            double FinalError = System.Math.Sqrt(ErrorCalc_Step3) /*/ SuspensionEvalIterations*/;

            /////<summary>
            /////You will notice that I have added only 1 condition for Bump Steer Convergence. This is because for the Special Case of BumpSteer there is no Const Bump Steer but only a "Monitor Bump Steer' 
            /////If the user chooses to monitor it then the convergence need to be computed. the Bmp Steer Graph only needs to be plotted 
            /////</summary>
            //if (Setup_CV.BumpSteerChangeRequested)
            //{
            //    ///<summary>Setting the Bump Steer Convergence</summary>
            //    Setup_OP.BumpSteer_Conv = new Convergence(1 - SetConvergenceError(FinalError));
            //}

            /////<summary>Passing the <see cref="Req_Camber"/> of the this class to the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/></summary>
            /////<remark>Don't need to do this here as the in THIS for the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/> is what is being used right from the start</remark>

            //Setup_OP.Calc_BumpSteerChart = _toeAngle;

            return FinalError;
        }
        
        #endregion

    }

    public class KO_Optimizer_ActuationPoints : KO_Master_Optimizer, IOptimizer_KO
    {

        #region ---Declarations---

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> Class containing all the relevant information of the Corner which is being considered here
        /// </summary>
        KO_CornverVariables KO_CV;

        /// <summary>
        /// Object of the <see cref="KO_Solver"/> Class which is the Parent SOlver which calls on the Optimization Methods
        /// </summary>
        KO_Solver ParentSolver;

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/> which contains relevant information pertaining to the Simulation params such as number of iterations etc
        /// </summary>
        public KO_SimulationParams KO_SimParams;

        /// <summary>
        /// Vehicle Corner
        /// </summary>
        VehicleCorner VCorner;

        /// <summary>
        /// The angle through which the Damper will be rotated to generate a new iteration of the <see cref="CoordinateOptions.DamperBellCrank"/> Point
        /// </summary>
        Angle DamperRotationAngle;

        /// <summary>
        /// The angle through which the ARB Droop Link will be rotated to generate a new iteration of the <see cref="CoordinateOptions.ARBLeverEndPoint"/> Point
        /// </summary>
        Angle DroopLinkAngle;

        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.PushrodInboard"/> which the Optimizer is trying to Optimize
        /// ---IMPORTANT--- The Optimizer will only generate X and Y Coordinates of the this point, the Z coordinate will be generated using the Equation of the Rocker Plane
        /// ---IMPORTANT--- This point is used for the Motion Analysis and hence constantly keeps changing
        /// </summary>
        Point3D PushrodRockerPoint_ForMotionAnalysis = new Point3D();
        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.PushrodInboard"/> which the Optimizer is trying to Optimize
        /// ---IMPORTANT--- The Optimizer will only generate X and Y Coordinates of the this point, the Z coordinate will be generated using the Equation of the Rocker Plane
        /// ---IMPORTANT--- This point is NOT used for the Motion Analysis and hence once it is initialied by the <see cref="KO_Master_Optimizer.GA"/> it remains constant for that Solution
        /// </summary>
        Point3D PushrodRockerPoint = new Point3D();

        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.DamperBellCrank"/> which the optimizer is trying to Optimize
        /// ---IMPORTANT--- This coordinate not GENERATED by the optimizer. An angle for the Damper is defined by the Optimizer which is then used to rotate the Damper and generate the new position of the point
        /// ---IMPORTANT--- This point is used for the Motion Analysis and hence constantly keeps changing
        /// </summary>
        Point3D DamperRockerPoint_ForMotionAnalysis = new Point3D();
        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.DamperBellCrank"/> which the optimizer is trying to Optimize
        /// ---IMPORTANT--- This coordinate not GENERATED by the optimizer. An angle for the Damper is defined by the Optimizer which is then used to rotate the Damper and generate the new position of the point
        /// ---IMPORTANT--- This point is NOT used for the Motion Analysis and hence once it is initialied by the <see cref="KO_Master_Optimizer.GA"/> it remains constant for that Solution
        /// </summary>
        Point3D DamperRockerPoint = new Point3D();


        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.ARBBellCrank"/> which the Optimizer is trying to Optimize
        /// ---IMPORTANT--- The Optimizer will only generate X and Y Coordinates of the this point, the Z coordinate will be generated using the Equation of the Rocker Plane
        /// ---IMPORTANT--- This point is used for the Motion Analysis and hence constantly keeps changing
        /// </summary>
        Point3D ARBRockerPoint_ForMotionAnalysis = new Point3D();
        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.ARBBellCrank"/> which the Optimizer is trying to Optimize
        /// ---IMPORTANT--- The Optimizer will only generate X and Y Coordinates of the this point, the Z coordinate will be generated using the Equation of the Rocker Plane
        /// ---IMPORTANT--- This point is NOT used for the Motion Analysis and hence once it is initialied by the <see cref="KO_Master_Optimizer.GA"/> it remains constant for that Solution
        /// </summary>
        Point3D ARBRockerPoint = new Point3D();

        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.ARBLeverEndPoint"/> which the Optimizer is trying to Optimize
        /// ---IMPORTANT--- The Optimizer will only generate X and Y Coordinates of the this point, the Z coordinate will be generated using the Equation of the Rocker Plane
        /// ---IMPORTANT--- This point is used for the Motion Analysis and hence constantly keeps changing
        /// </summary>
        Point3D ARBLeverEnd_ForMotionAnalysis = new Point3D();
        /// <summary>
        /// The coordinates of the <see cref="CoordinateOptions.ARBLeverEndPoint"/> which the Optimizer is trying to Optimize
        /// ---IMPORTANT--- The Optimizer will only generate X and Y Coordinates of the this point, the Z coordinate will be generated using the Equation of the Rocker Plane
        /// ---IMPORTANT--- This point is NOT used for the Motion Analysis and hence once it is initialied by the <see cref="KO_Master_Optimizer.GA"/> it remains constant for that Solution
        /// </summary>
        Point3D ARBLeverEnd = new Point3D();

        /// <summary>
        /// Temporary assembly of Outboard Coordinates
        /// </summary>
        Dictionary<string, Point3D> TempOutboardAssembly;

        /// <summary>
        /// Temporary Assembly of Inboard Coordinates
        /// </summary>
        Dictionary<string, Point3D> TempInboardAssembly;

        /// <summary>
        /// <see cref="List{Double}"/> containing the deltas of all the wheel deflections
        /// </summary>
        List<double> SpringDeflection_Deltas;

        /// <summary>
        /// <see cref="List{Double}"/> holding the Wheel Deflection Delta Values
        /// </summary>
        List<double> Calc_WheelDeflection_Deltas;

        /// <summary>
        /// <see cref="List{Double}"/> holding the ARB Lever Deflection Delta Values
        /// </summary>
        List<double> Calc_ARBLeverDeflection_Deltas;

        /// <summary>
        /// <see cref="List{Double}"/> holding the Motion Ratio Values of the Spring as they vary with the <see cref="SpringDeflection_Deltas"/> and <see cref="Calc_WheelDeflection_Deltas"/>
        /// </summary>
        List<double> MotionRatio_Spring;

        /// <summary>
        /// <see cref="List{Double}"/> holding the Motion Ratio Values of the ARBas they vary with the <see cref="SpringDeflection_Deltas"/> and <see cref="Calc_WheelDeflection_Deltas"/>
        /// </summary>
        List<double> MotionRatio_ARB;

        /// <summary>
        /// Length of the Chromosome
        /// </summary>
        int ChromosomeLength;

        /// <summary>
        /// Bitsize of each of the Variables which the Optimizer is trying to optimize
        /// </summary>
        int BitSize; 

        #endregion

        
        #region ---Initialization Methods---

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
        public KO_Optimizer_ActuationPoints(double _crossover, double _mutation, int _elites, int _noOfGenerations, VehicleCorner _vCorner)
        {
            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;

            No_Generations = _noOfGenerations;

            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Opt_AdjToolValues = new Dictionary<string, double>();

            VCorner = _vCorner;

            ChromosomeLength = 60;

            BitSize = 10;
            
        }

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed</param>
        /// <param name="_koSim">Object of the <see cref="KO_SimulationParams"/></param>
        /// <param name="_koCV">Object of the <see cref="KO_CornverVariables"/></param>
        /// <param name="_koSolver">Object of the <see cref="KO_Solver"/></param>
        public void Initialize_CornverVariables(ref Vehicle _vehicle, ref KO_SimulationParams _koSim, ref KO_CornverVariables _koCV, KO_Solver _koSolver)
        {
            Vehicle = _vehicle;

            //Chassis = _vehicle.chassis_vehicle;

            KO_SimParams = _koSim;

            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object</summary>
            KO_CV = _koCV;

            ///<summary>Initializing the <see cref="VehicleCornerParams"/> object of the <see cref="KO_CornverVariables"/> Class</summary>
            KO_CV.Initialize_VehicleCornerParams(ref KO_CV, Vehicle, VehicleCorner.FrontLeft, KO_SimParams.NumberOfIterations_KinematicSolver);

            ///<see cref="Initializing the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_CornverVariables.KO_MasterAdjs"/> "/>
            KO_CV.Initialize_AdjusterCoordinates(KO_CV.VCornerParams);

            ParentSolver = _koSolver;

            Initialize_OptimizerOutputs();

        }

        /// <summary>
        /// ---3rd--- To be called 3rd
        /// Method to Constrcut the <see cref="GAF.GeneticAlgorithm"/>Class's object and assing it with the relevant operators, erroc functions etc
        /// </summary>
        /// <param name="_popSize">Size of the Population</param>
        public void ConstructGeneticAlgorithm(int _popSize)
        {
            PopulationSize = _popSize;

            Population = new Population(PopulationSize, ChromosomeLength, false, true);

            EvaluateFitnessOfGeneticAlforithm = EvaluateFitnessCurve;

            Terminate = TerminateAlgorithm;

            GA = new GeneticAlgorithm(Population, EvaluateFitnessOfGeneticAlforithm);

            GA.OnGenerationComplete += GA_OnGenerationComplete; 

            GA.OnRunComplete += GA_OnRunComplete;


            GA.Operators.Add(Elites);
            GA.Operators.Add(Crossovers);
            GA.Operators.Add(Mutations);

            GA.Run(Terminate);
        }

        /// <summary>
        /// Method to initialize the variables which will store the Optimizer Outputs
        /// </summary>
        private void Initialize_OptimizerOutputs()
        {
            DamperRockerPoint_ForMotionAnalysis = KO_CV.VCornerParams.DamperBellCrank.Clone() as Point3D;
            DamperRockerPoint = KO_CV.VCornerParams.DamperBellCrank.Clone() as Point3D;

            PushrodRockerPoint_ForMotionAnalysis = KO_CV.VCornerParams.PushrodInboard.Clone() as Point3D;
            PushrodRockerPoint = KO_CV.VCornerParams.PushrodInboard.Clone() as Point3D;

            ARBLeverEnd_ForMotionAnalysis = KO_CV.VCornerParams.ARB_DroopLink_LeverPoint.Clone() as Point3D;
            ARBLeverEnd = KO_CV.VCornerParams.ARB_DroopLink_LeverPoint.Clone() as Point3D;

        }


        #endregion


        #region ---Genetic Algorithm Methods---

        /// <summary>
        /// The PRIMARY Error function which the <see cref="IOptimizer.GA"/> calls
        /// </summary>
        /// <param name="chromosome">Chromosome which is prodced by the Algorithm</param>
        /// <returns>Fitness of the <paramref name="chromosome"/></returns>
        public double EvaluateFitnessCurve(Chromosome chromosome)
        {
            double fitness = -1;

            if (chromosome != null)
            {
                Initialize_OptimizerOutputs();

                ///<summary>Extracting the Coordinates from the <see cref="IOptimizer.GA"/> generated Chromosome</summary>
                ExtractChromosoomes(chromosome, out DamperRotationAngle, /*out DroopLinkAngle, */out double xPushRocker, out double yPushRocker, out double xARBRocker, out double yARBRocker);
                PushrodRockerPoint_ForMotionAnalysis = new Point3D(xPushRocker, yPushRocker, 0);
                ARBRockerPoint_ForMotionAnalysis = new Point3D(xARBRocker, yARBRocker, 0);
                
                fitness = 1 - Compute_MainError();

                KO_CV.MotionRatio_Convergence = new Convergence(System.Math.Round(fitness, 2));
            }

            return fitness;
        }

        /// <summary>
        /// Evnent fired when <see cref="IOptimizer.GA"/> has solved the Genetic Algorithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GA_OnGenerationComplete(object sender, GaEventArgs e)
        {
            double Fitness = e.Population.MaximumFitness;

            int Generations = e.Generation;

            ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
            var chromosome = e.Population.GetTop(1)[0];

            Initialize_OptimizerOutputs();

            EvaluateFitnessCurve(chromosome);

            //Assign_OptimizedPoints();

            ParentSolver.MaxProgressBar(No_Generations);


        }

        /// <summary>
        /// Event fired when a particular Generation of the <see cref="IOptimizer.GA"/> has been solved 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GA_OnRunComplete(object sender, GaEventArgs e)
        {
            double Fitness = e.Population.MaximumFitness;

            int Generations = e.Generation;

            ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
            var chromosome = e.Population.GetTop(1)[0];

            Initialize_OptimizerOutputs();

            EvaluateFitnessCurve(chromosome);

            Assign_OptimizedPoints();

            ParentSolver.UpdateProgressBar();
        }

        /// <summary>
        /// The PRIMARY Terminate function called by the <see cref="IOptimizer.GA"/>
        /// </summary>
        /// <param name="_population">Will be taken care of by <see cref="GAF"/></param>
        /// <param name="_currGeneration">Will be taken care of by <see cref="GAF"/></param>
        /// <param name="currEvaluation">Will be taken care of by <see cref="GAF"/></param>
        /// <returns><see cref="bool"/> to determine whether the Algorithm should continue</returns>
        public bool TerminateAlgorithm(Population _population, int _currGeneration, long currEvaluation)
        {
            if (_currGeneration > No_Generations-50 || _population.MaximumFitness>=0.99)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region ---Helper Methods--

        /// <summary>
        /// Method to extract the optimized values from the Chromosome
        /// </summary>
        /// <param name="chromosome">The Chromosome itself</param>
        /// <param name="_damperRotAngle">The rotation angle through which the damper will be rotated (about the Rocker Axis) to generate a new iteration of the <see cref="CoordinateOptions.DamperBellCrank"/></param>
        /// <param name="_xPush">X Coordinate of the <see cref="CoordinateOptions.PushrodInboard"/></param>
        /// <param name="_yPush">Y Coordinate of the <see cref="CoordinateOptions.PushrodInboard"/></param>
        private void ExtractChromosoomes(Chromosome chromosome, out Angle _damperRotAngle/*, out Angle _droopLinkAngle*/, out double _xPush, out double _yPush, out double _xARBRocker, out double _yARBRocker)
        {
            _damperRotAngle = new Angle();

            //_droopLinkAngle = new Angle();

            _xPush = _yPush = 0;

            _xARBRocker = _yARBRocker = 0;

            double nominalAngle_Damper = 0;
            double upperAngle_Damper = /*70*//*180*/90/*360*/;
            double lowerAngle_Damper = /*-70*//*-180*/-90/*0*/;

            //double nominalAngle_DroopLink = 0;
            //double upperAngle_DroopLink = /*70*/180;
            //double lowerAngle_DroopLink = /*-70*/-180;

            ///<summary>
            ///Assigning the Nominal,Upper and Lower Values of the X/Y/Z Coordinates of the PUSHROD ROCKER . This will be later on obtained from the User
            /// </summary>
            double nominalX_Push = KO_CV.VCornerParams.Rocker_Center.X;
            ///<remarks>
            ///Upper and Lower limits of the X Coordinate of the PUSHROD ROCKER
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaX_Push = 65;
            double lowerDeltaX_Push = /*-65*/0;

            double nominalY_Push = KO_CV.VCornerParams.Rocker_Center.Y;
            ///<remarks>
            ///Upper and Lower limits of the Y Coordinate of the PUSHROD ROCKER
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaY_Push = 65;
            double lowerDeltaY_Push = -65/*0*/;


            ///<summary>
            ///Assigning the Nominal,Upper and Lower Values of the X/Y/Z Coordinates of the ARB ROCKER . This will be later on obtained from the User
            /// </summary>
            double nominalX_ARBRocker = KO_CV.VCornerParams.Rocker_Center.X;
            ///<remarks>
            ///Upper and Lower limits of the X Coordinate of the ARB ROCKER
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaX_ARBRocker = 65;
            double lowerDeltaX_ARBRocker = /*-65*/0;

            double nominalY_ARBRocker = KO_CV.VCornerParams.Rocker_Center.Y;
            ///<remarks>
            ///Upper and Lower limits of the Y Coordinate of the ARB ROCKER
            ///Arbitrary as of no
            /// </remarks>
            double upperDeltaY_ARBRocker = 65;
            double lowerDeltaY_ARBRocker = -65/*0*/;



            ///Range Constant for the Angle of Rotation of the Damper
            var rcAngle_Damper = GAF.Math.GetRangeConstant(upperAngle_Damper - lowerAngle_Damper, BitSize);

            /// Range constant for x for the Pushrod Rocker X Coordinate
            var rcx_Push = GAF.Math.GetRangeConstant(upperDeltaX_Push - lowerDeltaX_Push, BitSize);

            /// Range constant for y for the Pushrod Rocker X Coordinate
            var rcy_Push = GAF.Math.GetRangeConstant(upperDeltaY_Push - lowerDeltaY_Push, BitSize);

            /////Range Constant for the Angle of Rotation of the ARB
            //var rcAngle_DroopLink = GAF.Math.GetRangeConstant(upperAngle_DroopLink - lowerAngle_DroopLink, BitSize);

            /// Range constant for x for the ARBrod Rocker X Coordinate
            var rcx_ARBRocker = GAF.Math.GetRangeConstant(upperDeltaX_ARBRocker - lowerDeltaX_ARBRocker, BitSize);

            /// Range constant for y for the ARBrod Rocker X Coordinate
            var rcy_ARBRocker = GAF.Math.GetRangeConstant(upperDeltaY_ARBRocker - lowerDeltaY_ARBRocker, BitSize);



            /// Retrieving the 10 bit values as from the <param name="chromosome"
            var angle1_Damper = Convert.ToInt32(chromosome.ToBinaryString(0, BitSize), 2);
            var x1_Push = Convert.ToInt32(chromosome.ToBinaryString(BitSize, BitSize), 2);
            var y1_Push = Convert.ToInt32(chromosome.ToBinaryString(BitSize * 2, BitSize), 2);
            //var angle1_DroopLink = Convert.ToInt32(chromosome.ToBinaryString(BitSize * 3, BitSize), 2);
            var x1_ARBRocker = Convert.ToInt32(chromosome.ToBinaryString(BitSize * 3, BitSize), 2);
            var y1_ARBRocker = Convert.ToInt32(chromosome.ToBinaryString(BitSize * 4, BitSize), 2);


            /// Multiplying by the appropriate range constant and adjusting for any in the range (Lower Delta is the Offset) to get the real values
            ///<remarks>
            ///https://gaframework.org/wiki/index.php/How_to_Encode_Parameters
            ///Visit link above for more information on the code below
            /// </remarks>
            _damperRotAngle = new Angle(System.Math.Round(((angle1_Damper * rcAngle_Damper) + (nominalAngle_Damper + lowerAngle_Damper)), 2), AngleUnit.Degrees);
            _xPush = System.Math.Round((x1_Push * rcx_Push) + (nominalX_Push + lowerDeltaX_Push), 3);
            _yPush = System.Math.Round((y1_Push * rcy_Push) + (nominalY_Push + lowerDeltaY_Push), 3);

            //_droopLinkAngle = new Angle(System.Math.Round(((angle1_DroopLink * rcAngle_DroopLink) + (nominalAngle_DroopLink + lowerAngle_DroopLink)), 2), AngleUnit.Degrees);
            _xARBRocker = System.Math.Round((x1_ARBRocker* rcx_ARBRocker) + (nominalX_ARBRocker + lowerDeltaX_ARBRocker), 3);
            _yARBRocker = System.Math.Round((y1_ARBRocker * rcy_ARBRocker) + (nominalY_ARBRocker + lowerDeltaY_ARBRocker), 3);
        }



        /// <summary>
        /// Main Error Function which computes and returns the error
        /// </summary>
        /// <returns>Error of the Solution</returns>
        public double Compute_MainError()
        {
            double mainError = 0;

            ///<summary>Computing the Computing the <see cref="CoordinateOptions.PushrodInboard"/> Point Point</summary>
            Compute_Pushrod_RockerPoint();
            ///<summary>Computing the <see cref="CoordinateOptions.DamperBellCrank"/> Point</summary>
            Compute_Damper_RockerPoint();
            ///<summary>Computing the <see cref="CoordinateOptions.ARBBellCrank"/> Point</summary>
            Compute_ARB_RockerPoint();
            /////<summary>Computing the <see cref="CoordinateOptions.ARBLeverEndPoint"/> Point</summary>
            //Compute_ARB_DroopLink_LeverEnd();


            Compute_Error_MotionRatio();

            mainError = System.Math.Sqrt(System.Math.Pow(Error_Spring_MR, 2) + System.Math.Pow(Error_ARB_MR, 2));

            if (mainError > 1 || mainError < 0) 
            {
                return 0.99;
            }
            else
            {
                return mainError;
            }
        }


        /// <summary>
        /// <para>Method to complete the <see cref="CoordinateOptions.PushrodInboard"/> point</para>
        /// <para>The <see cref="CoordinateOptions.PushrodInboard"/> at this stage has only X and Y coordinates as given by the Optimization Algorithm
        /// This method completes the Point by using the Plane Equation of the <see cref="VehicleCornerParams.RockerPlane"/></para>
        /// </summary>
        private void Compute_Pushrod_RockerPoint()
        {
            PushrodRockerPoint_ForMotionAnalysis = KO_CV.Compute_PointOnPlane(KO_CV.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, PushrodRockerPoint_ForMotionAnalysis);

            PushrodRockerPoint = PushrodRockerPoint_ForMotionAnalysis.Clone() as Point3D;

        }

        /// <summary>
        /// Method to generate the new <see cref="CoordinateOptions.DamperBellCrank"/> point using the Rotation Angle that the optimizer generated
        /// </summary>
        private void Compute_Damper_RockerPoint()
        {
            Line damperLine = new Line(KO_CV.VCornerParams.DamperShockMount, DamperRockerPoint_ForMotionAnalysis);

            damperLine.Rotate(DamperRotationAngle.Radians, KO_CV.VCornerParams.Rocker_Axis_Vector, damperLine.StartPoint);

            DamperRockerPoint = DamperRockerPoint_ForMotionAnalysis.Clone() as Point3D;

            double damperLengthCheck = damperLine.Length();
        }

        /// <summary>
        /// Method to generate the coordinates of the <see cref="CoordinateOptions.ARBBellCrank"/> using Plane equations once the Optimizer has generated X and Y Coordinates 
        /// </summary>
        private void Compute_ARB_RockerPoint()
        {
            ARBRockerPoint_ForMotionAnalysis = KO_CV.Compute_PointOnPlane(KO_CV.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, ARBRockerPoint_ForMotionAnalysis);

            ARBRockerPoint = ARBRockerPoint_ForMotionAnalysis.Clone() as Point3D;
        }

        ///// <summary>
        ///// Method to generate the coordiantes of the <see cref="CoordinateOptions.ARBLeverEndPoint"/> using the Link Length of the Droop Link and the angle of rotation of the droop link 
        ///// </summary>
        //private void Compute_ARB_DroopLink_LeverEnd()
        //{
        //    ARBLeverEnd_ForMotionAnalysis = KO_CV.Compute_ApproxCorrespondingPoint_FromLinkLength(KO_CV.ARB_DroopLink_Length, ARBRockerPoint_ForMotionAnalysis, VCorner);
        //    ARBLeverEnd_ForMotionAnalysis = KO_CV.Compute_PointOnPlane(KO_CV.VCornerParams.RockerPlane, CoordinateInputFormat.IIO, ARBLeverEnd_ForMotionAnalysis);

        //    Line droopLinkLine = new Line(ARBRockerPoint_ForMotionAnalysis, ARBLeverEnd_ForMotionAnalysis);
        //    droopLinkLine.Rotate(DroopLinkAngle.Radians, KO_CV.VCornerParams.Rocker_Axis_Vector, droopLinkLine.StartPoint);

        //    ARBLeverEnd = ARBLeverEnd_ForMotionAnalysis.Clone() as Point3D;

        //    double droopLinkLengthCheck = droopLinkLine.Length();

        //}

        
        private void Compute_Error_MotionRatio()
        {
            DWSolver = new DoubleWishboneKinematicsSolver();

            KinematicsSolver_For_MotionRatio(KO_CV.BumpSteerCurve.WheelDeflections, Vehicle, DWSolver);

            Extract_MotionRatio();

            /*double mrError =*/ EvaluateDevlationError_Static();

            //return mrError;
        }

  

        /// <summary>
        /// Method which calls the Solver methods for Kinematic pass to solve for Bump Steer
        /// </summary>
        /// <param name="_springDeflections"><see cref="List{Double}"/> of Wheel Deflections over which the Kinematics Computation is done </param>
        /// <param name="_OC"><see cref="List{OutputClass}"/></param>
        /// <param name="_Vehicle"><see cref="Vehicle"/> itemn</param>
        public void KinematicsSolver_For_MotionRatio(List<double> _springDeflections, Vehicle _Vehicle, DoubleWishboneKinematicsSolver _DwSolver)
        {
            Calc_WheelDeflection_Deltas = new List<double>();

            Calc_ARBLeverDeflection_Deltas = new List<double>();

            ///<summary>
            ///Creating a Temporary Dictionary of Outboard Points. 
            ///This is necessary because this is just a simulation to evaluate the Bump Steer. The actual Outboard Points don't need to change. If i use the <see cref="VehicleCornerParams.OutboardAssembly"/>
            ///then it will be changed as the points changed and this is not desired
            /// </summary>
            TempOutboardAssembly = new Dictionary<string, Point3D>();
            foreach (string coordinate in KO_CV.VCornerParams.OutboardAssembly.Keys)
            {
                TempOutboardAssembly.Add(coordinate, KO_CV.VCornerParams.OutboardAssembly[coordinate].Clone() as Point3D);
            }

            ///<summary>
            ///Creating a Temporary Dictionary of Outboard Points. 
            ///This is necessary because this is just a simulation to evaluate the Bump Steer. The actual Outboard Points don't need to change. If i use the <see cref="VehicleCornerParams.OutboardAssembly"/>
            ///then it will be changed as the points changed and this is not desired
            /// </summary>
            TempInboardAssembly = new Dictionary<string, Point3D>();
            foreach (string coordinate in KO_CV.VCornerParams.InboardAssembly.Keys)
            {
                TempInboardAssembly.Add(coordinate, KO_CV.VCornerParams.InboardAssembly[coordinate].Clone() as Point3D);
            }

            Update_InboardCoordinateDictionary();

            ///<summary>Creating a List which holds the DELTAS of wheel deflection as it is the deltas of deflection that we are concerned with and not the actual or Absolute Wheel Deflections</summary>
            SpringDeflection_Deltas = new List<double>();

            for (int i = 0; i < _springDeflections.Count; i++)
            {
                if (i == 0)
                {
                    SpringDeflection_Deltas.Add(0);
                }
                else
                {
                    SpringDeflection_Deltas.Add(_springDeflections[i] - _springDeflections[i - 1]);
                }
            }

            ///<summary>Initializing the Local Coordinates of the <see cref="SolverMasterClass"/> computed so far</summary>
            _DwSolver.Assign_LocalCoordinateVariables_WishbonePoints(TempInboardAssembly, TempOutboardAssembly);

            for (int i = 0; i < SpringDeflection_Deltas.Count; i++)
            {
                SolveKinematics_MotionRatioEval(SpringDeflection_Deltas[i], _Vehicle, i, _DwSolver);
            }

        }


        /// <summary>
        /// Kinematics Solver method which performs the Kinematic Pass for the given motion
        /// </summary>
        /// <param name="_springDeflection">Wheel Deflections</param>
        /// <param name="_oc">Output Class LIst </param>
        /// <param name="_vehicle">Vehicle Item</param>
        /// <param name="_index">Index at which the Motion. </param>
        /// <param name="_dwSolver"><see cref="DoubleWishboneKinematicsSolver"/> Object</param>
        private void SolveKinematics_MotionRatioEval(double _springDeflection, Vehicle _vehicle, int _index, DoubleWishboneKinematicsSolver _dwSolver)
        {
            _dwSolver.CalculateInitialSpindleVector();

            _dwSolver.Compute_AngleofRot_Rocker(_springDeflection, out Angle rockerRotationAngle);

            Rotate_Rocker(rockerRotationAngle, KO_CV.VCornerParams.Rocker_Axis_Vector, KO_CV.VCornerParams.Rocker_Center, ref PushrodRockerPoint_ForMotionAnalysis, ref DamperRockerPoint_ForMotionAnalysis, ref ARBRockerPoint_ForMotionAnalysis);

            MathNet.Spatial.Euclidean.Point3D J = new MathNet.Spatial.Euclidean.Point3D(DamperRockerPoint_ForMotionAnalysis.X, DamperRockerPoint_ForMotionAnalysis.Y, DamperRockerPoint_ForMotionAnalysis.Z);

            MathNet.Spatial.Euclidean.Point3D H = new MathNet.Spatial.Euclidean.Point3D(PushrodRockerPoint_ForMotionAnalysis.X, PushrodRockerPoint_ForMotionAnalysis.Y, PushrodRockerPoint_ForMotionAnalysis.Z);

            MathNet.Spatial.Euclidean.Point3D O = new MathNet.Spatial.Euclidean.Point3D(ARBRockerPoint_ForMotionAnalysis.X, ARBRockerPoint_ForMotionAnalysis.Y, ARBRockerPoint_ForMotionAnalysis.Z);


            ///Running a Kinematics Pass

            ///<remarks>Should pass a sensible value instead of simply new MathNet.Spatial.Euclidean.Point3D()</remarks>
            _dwSolver.KO_InitialStage_MR_PushPullOutboard(new MathNet.Spatial.Euclidean.Point3D(PushrodRockerPoint_ForMotionAnalysis.X, PushrodRockerPoint_ForMotionAnalysis.Y, PushrodRockerPoint_ForMotionAnalysis.Z), new MathNet.Spatial.Euclidean.Point3D(), out MathNet.Spatial.Euclidean.Point3D G);

            _dwSolver.KO_InitialStage_MR_UBJ(G, out MathNet.Spatial.Euclidean.Point3D F);

            _dwSolver.KO_InitialStage_MR_LBJ(F, out MathNet.Spatial.Euclidean.Point3D E);

            _dwSolver.KO_InitialStage_MR_ARBLeverEnd(H, O, out MathNet.Spatial.Euclidean.Point3D P);

            double wheelDef = _dwSolver.KO_InitialStage_ComputeWheelDeflection(new MathNet.Spatial.Euclidean.Point3D(KO_CV.VCornerParams.ContactPatch.X, KO_CV.VCornerParams.ContactPatch.Y, KO_CV.VCornerParams.ContactPatch.Z), E);

            double arbDef = _dwSolver.KO_InitialStage_Compute_ARBLeverDeflection(P);


            ///<summary>Assigning all the computed points to a the <see cref="UnsprungAssembly"/> dictionary</summary>
            TempOutboardAssembly[CoordinateOptions.UBJ.ToString()] = new Point3D(F.X, F.Y, F.Z);

            TempOutboardAssembly[CoordinateOptions.LBJ.ToString()] = new Point3D(E.X, E.Y, E.Z);

            TempInboardAssembly[CoordinateOptions.PushrodOutboard.ToString()] = new Point3D(G.X, G.Y, G.Z);

            TempInboardAssembly[CoordinateOptions.PushrodInboard.ToString()] = new Point3D(H.X, H.Y, H.Z);

            TempInboardAssembly[CoordinateOptions.DamperBellCrank.ToString()] = new Point3D(J.X, J.Y, J.Z);

            TempInboardAssembly[CoordinateOptions.ARBBellCrank.ToString()] = new Point3D(O.X, O.Y, O.Z);

            TempInboardAssembly[CoordinateOptions.ARBLeverEndPoint.ToString()] = new Point3D(P.X, P.Y, P.Z);

            ///<summary>Computing and storing the Wheel Deflections</summary>
            Calc_WheelDeflection_Deltas.Add(wheelDef);
            ///<summary>Computing and storing the ARB Lever Deflections</summary>
            Calc_ARBLeverDeflection_Deltas.Add(arbDef);


            ///<summary>Initializing the Local Coordinates of the <see cref="SolverMasterClass"/> computed so far</summary>
            _dwSolver.Assign_LocalCoordinateVariables_WishbonePoints(TempInboardAssembly, TempOutboardAssembly);

        }

        /// <summary>
        /// ---IMPORTANT---
        /// This method is added here instead of <see cref="DoubleWishboneKinematicsSolver"/> because in <see cref="DoubleWishboneKinematicsSolver"/> there are no references to the library <see cref="devDept.Geometry"/>
        /// This library is critical to the smooth functioning of this method and hence it is ddeclraedhere
        /// ---
        /// Method to Rotate the Rocker about its own axis
        /// </summary>
        /// <param name="_rotAngle"></param>
        /// <param name="_axisRotation"></param>
        /// <param name="_centerRot"></param>
        /// <param name="_newPushPullRocker"></param>
        /// <param name="_newDamperRocker"></param>
        private void Rotate_Rocker(Angle _rotAngle, Vector3D _axisRotation, Point3D _centerRot, ref Point3D _newPushPullRocker, ref Point3D _newDamperRocker, ref Point3D _newARBRocker)
        {
            Line damperRockerLine = new Line(_centerRot, _newDamperRocker);

            Line pushpullRockerLine = new Line(_centerRot, _newPushPullRocker);

            Line arbRockerLine = new Line(_centerRot, _newARBRocker);

            damperRockerLine.Rotate(_rotAngle.Radians, _axisRotation, _centerRot);

            pushpullRockerLine.Rotate(_rotAngle.Radians, _axisRotation, _centerRot);

            arbRockerLine.Rotate(_rotAngle.Radians, _axisRotation, _centerRot);
        }

        /// <summary>
        /// Metthod to Extract the Motion Ratio Values
        /// </summary>
        private void Extract_MotionRatio()
        {
            ///<summary>Initializing a temporary List to perform some pre-processig activaties</summary>
            List<double> temp = new List<double>();

            MotionRatio_Spring = new List<double>();

            MotionRatio_ARB = new List<double>();

            ///<summary>
            ///---IMPORTANT---
            ///Notice that I have done _oc.Count-1 instead of _oc.Count
            ///This is because while generting the Bump Steer Graph I added an extra element to the end of the List
            ///This is done so that the last deflection of the <see cref="CustomBumpSteerParams.WheelDeflections"/> also produces tangible results. If the extra element is not there then the Results at the 
            ///last wheel deflection would ALL be zero because of Lack of DELTA to compute from 
            /// </summary>
            temp = Calc_WheelDeflection_Deltas.ToList();

            //Calc_SpringDeflection_Deltas.Clear();

            ///<summary>
            ///DOING temp.Count-1 here as the (-1) has NOT been applied above (Unlike the <see cref="KO_Optimizer_BumpSteer.ExtractToeAngles(List{OutputClass})"/> method where (-1) is applied to temp.Count
            /// </summary>
            //for (int i = 0; i < temp.Count - 1; i++)
            //{
            //    if (i >= KO_CV.BumpSteerCurve.HighestBumpindex)
            //    {
            //        //Calc_SpringDeflection_Deltas.Add(temp[i]);
            //        if (KO_CV.MotionRatio_Format == MotionRatioFormat.ShockByWheel)
            //        {
            //            MotionRatio.Add(Calc_SpringDeflection_Deltas[i] / WheelDeflection_Deltas[i]);
            //        }
            //        else if (KO_CV.MotionRatio_Format == MotionRatioFormat.WheelByShock)
            //        {
            //            MotionRatio.Add(WheelDeflection_Deltas[i] / Calc_SpringDeflection_Deltas[i]);
            //        }
            //    }
            //}



            for (int i = 0; i < KO_CV.BumpSteerCurve.WheelDeflections.Count; i++)
            {
                if (i != 0 && KO_CV.BumpSteerCurve.WheelDeflections[i] == 1)
                {
                    //Calc_SpringDeflection_Deltas.Add(temp[i]);
                    if (KO_CV.MotionRatio_Format == MotionRatioFormat.WheelByShock)
                    {
                        MotionRatio_Spring.Add(Calc_WheelDeflection_Deltas[i] / SpringDeflection_Deltas[i]);
                    }
                    else if (KO_CV.MotionRatio_Format == MotionRatioFormat.ShockByWheel)
                    {
                        MotionRatio_Spring.Add(SpringDeflection_Deltas[i] / Calc_WheelDeflection_Deltas[i]);
                    }

                    break;
                }
            }

            for (int i = 0; i < KO_CV.BumpSteerCurve.WheelDeflections.Count; i++)
            {
                if (i != 0 && KO_CV.BumpSteerCurve.WheelDeflections[i] == 1) 
                {
                    MotionRatio_ARB.Add(Calc_ARBLeverDeflection_Deltas[i] / Calc_WheelDeflection_Deltas[i]);
                    break;
                }
            }

        }

        double Error_Spring_MR;

        double Error_ARB_MR;

        /// <summary>
        /// Method to Compute the Motion Ratio Error
        /// </summary>
        /// <returns></returns>
        private void EvaluateDevlationError_Static()
        {
            Error_Spring_MR = 0.99;

            Error_Spring_MR = System.Math.Sqrt((MotionRatio_Spring[0] - KO_CV.MotionRatio_Spring) / KO_CV.MotionRatio_Spring);

            Error_ARB_MR = System.Math.Sqrt((MotionRatio_ARB[0] - KO_CV.MotionRatio_ARB) / KO_CV.MotionRatio_ARB);

            //if (motionRatioError_Spring > 1 || motionRatioError_Spring < 0)
            //{
            //    return 0.99;
            //}
            //else
            //{
            //    return motionRatioError_Spring;
            //}
            
        }


        /// <summary>
        /// NOT USED AS OF NOW
        /// </summary>
        /// <param name="_calcMotionRatio"></param>
        /// <param name="_requestedMotionRatios"></param>
        /// <returns></returns>
        private double EvaluateDeviationError_Dynamics(List<double> _calcMotionRatio, List<double> _requestedMotionRatios)
        {
            List<double> UserMotionRatio = _requestedMotionRatios;

            List<double> ErrorCalc_Step1 = new List<double>();

            ///<summary>Finding the distance between each pair of Points</summary>
            for (int i = 0; i < _requestedMotionRatios.Count; i++)
            {
                if (i != 0 && i != _requestedMotionRatios.Count - 1)
                {
                    ///<remarks>Not deleting the Commented out sections because they are different methods to evaluate error and I wanna do a few more iterations to tes tthem </remarks>

                    //if (i != SuspensionEvalIterations - 1)
                    //{

                    //ErrorCalc_Step1.Add(new Angle((_toeAngle[i].Degrees - UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));

                    //if (UserBumpSteerCurve[i].Degrees != 0)
                    //{
                    //    ErrorCalc_Step1.Add(new Angle(((_toeAngle[i - 1].Degrees - UserBumpSteerCurve[i].Degrees) / UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));
                    //}
                    //else
                    //{
                    ErrorCalc_Step1.Add(((_calcMotionRatio[i - 1] - UserMotionRatio[i])));
                    //}
                    //}
                    //else
                    //{
                    //    ErrorCalc_Step1.Add(ErrorCalc_Step1[i - 1]);
                    //} 
                }
            }

            List<double> ErrorCalc_Step2 = new List<double>();

            ///<summary>Finiding the square of the Distance</summary>
            for (int i = 0; i < ErrorCalc_Step1.Count; i++)
            {
                ErrorCalc_Step2.Add((ErrorCalc_Step1[i] * ErrorCalc_Step1[i]));
            }

            double ErrorCalc_Step3 = 0;

            ///<summary>Summing the squares of the distances</summary>
            for (int i = 0; i < ErrorCalc_Step2.Count; i++)
            {
                ErrorCalc_Step3 += ErrorCalc_Step2[i];
            }

            ///<summary>Computing the Final Error by finding the Square Root of the Squares of the distances and dividing it by the No. of Iterations</summary>
            ///<remarks>Not deleting the Commented out sections because they are different methods to evaluate error and I wanna do a few more iterations to tes tthem </remarks>

            double FinalError = System.Math.Sqrt(ErrorCalc_Step3) /*/ SuspensionEvalIterations*/;

            /////<summary>
            /////You will notice that I have added only 1 condition for Bump Steer Convergence. This is because for the Special Case of BumpSteer there is no Const Bump Steer but only a "Monitor Bump Steer' 
            /////If the user chooses to monitor it then the convergence need to be computed. the Bmp Steer Graph only needs to be plotted 
            /////</summary>
            //if (Setup_CV.BumpSteerChangeRequested)
            //{
            //    ///<summary>Setting the Bump Steer Convergence</summary>
            //    Setup_OP.BumpSteer_Conv = new Convergence(1 - SetConvergenceError(FinalError));
            //}

            /////<summary>Passing the <see cref="Req_Camber"/> of the this class to the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/></summary>
            /////<remark>Don't need to do this here as the in THIS for the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/> is what is being used right from the start</remark>

            //Setup_OP.Calc_BumpSteerChart = _toeAngle;

            return FinalError;
        }

        private void Update_InboardCoordinateDictionary()
        {
            TempInboardAssembly[CoordinateOptions.PushrodInboard.ToString()] = PushrodRockerPoint_ForMotionAnalysis.Clone() as Point3D;

            TempInboardAssembly[CoordinateOptions.DamperBellCrank.ToString()] = DamperRockerPoint_ForMotionAnalysis.Clone() as Point3D;

            TempInboardAssembly[CoordinateOptions.ARBBellCrank.ToString()] = ARBRockerPoint_ForMotionAnalysis.Clone() as Point3D;

            TempInboardAssembly[CoordinateOptions.ARBLeverEndPoint.ToString()] = ARBLeverEnd_ForMotionAnalysis.Clone() as Point3D;

        }

        private void Assign_OptimizedPoints()
        {
            KO_CV.VCornerParams.PushrodInboard = PushrodRockerPoint.Clone() as Point3D;

            KO_CV.VCornerParams.DamperBellCrank = DamperRockerPoint.Clone() as Point3D;

            KO_CV.VCornerParams.ARBBellCrank = ARBRockerPoint.Clone() as Point3D;

            KO_CV.VCornerParams.ARB_DroopLink_LeverPoint = ARBLeverEnd.Clone() as Point3D;

            KO_CV.VCornerParams.Initialize_Dictionary();
        }

        #endregion


    }

}


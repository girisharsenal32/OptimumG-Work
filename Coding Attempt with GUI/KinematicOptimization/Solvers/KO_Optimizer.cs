using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;
using devDept.Geometry;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    public class KO_Optimizer : Master_Optimizer
    {
        
    }

    public class KO_Optimizer_BumpSteer : Master_Optimizer
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
        /// This is Bump Steer Graph which is computed for a given iteration of the <see cref="Master_Optimizer.GA"/> 
        /// </summary>
        List<Angle> Calc_BumpSteerGraph;

        /// <summary>
        /// Inboard Toe Link Point which is being optimized
        /// </summary>
        Point3D InboardToeLink; 
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
        }

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed </param>
        /// <param name="_koCV"></param>
        /// <param name="_koCVFR"></param>
        /// <param name="_koCVRL"></param>
        /// <param name="_koCVRR"></param>
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

            Population = new Population(PopulationSize, 30, false, true);

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
        /// The PRIMARY Error function which the <see cref="Master_Optimizer.GA"/> calls
        /// </summary>
        /// <param name="chromosome"></param>
        /// <returns></returns>
        private double EvaluateFitnessCurve(Chromosome chromosome)
        {
            double fitness = -1;

            if (chromosome != null)
            {
                ///<summary>Extracting the Coordinates from the <see cref="Master_Optimizer.GA"/> generated Chromosome</summary>
                ExtractFromChromosome(chromosome, out double x, out double y, out double z);
                InboardToeLink = new Point3D(x, y, z);
                KO_CV.VCornerParams.InboardAssembly[CoordinateOptions.ToeLinkInboard.ToString()] = InboardToeLink;

                fitness = 1 - ComputeError_BumpSteer();

                KO_CV.BumpSteerConvergence = new Convergence(System.Math.Round(fitness, 2));

                if (fitness > 1)
                {
                    return 1 - 0.99;
                }
                else if (fitness < 0)
                {
                    return 1 - 0.99;
                }

            }

            return fitness;
        }

        /// <summary>
        /// Eventt fired when a particular Generation of the <see cref="Master_Optimizer.GA"/> has been solved 
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
        /// Evnent fired when <see cref="Master_Optimizer.GA"/> has solved the Genetic Algorithm
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
        /// The PRIMARY Terminate function called by the <see cref="Master_Optimizer.GA"/>
        /// </summary>
        /// <param name="_population"></param>
        /// <param name="_currGeneration"></param>
        /// <param name="currEvaluation"></param>
        /// <returns></returns>
        private bool TerminateAlgorithm(Population _population, int _currGeneration, long currEvaluation)
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
            double upperDeltaX = 100;
            double lowerDeltaX = -100;

            double nominalY = KO_CV.VCornerParams.ToeLinkInboard.Y;
            double upperDeltaY = 100;
            double lowerDeltaY = -100;

            double nominalZ = KO_CV.VCornerParams.ToeLinkInboard.Z;
            double upperDeltaZ = 100;
            double lowerDeltaZ = -100;

            // range constant for x
            var rcx = GAF.Math.GetRangeConstant(upperDeltaX - lowerDeltaX, 10);

            // range constant for y
            var rcy = GAF.Math.GetRangeConstant(upperDeltaY - lowerDeltaY, 10);

            // range constant for z
            var rcz = GAF.Math.GetRangeConstant(upperDeltaZ - lowerDeltaZ, 10);

            // when evaluating the fitness simply retrieve the 20 bit values as integers 
            // from the chromosome e.g.
            var x1 = Convert.ToInt32(chromosome.ToBinaryString(0, 10), 2);
            var y1 = Convert.ToInt32(chromosome.ToBinaryString(10, 10), 2);
            var z1 = Convert.ToInt32(chromosome.ToBinaryString(20, 10), 2);

            // multiply by the appropriate range constant and adjust for any offset 
            // in the range to get the real values
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
        private double ComputeError_BumpSteer()
        {
            DWSolver = new DoubleWishboneKinematicsSolver();

            BumpSteerSolver_FirstPass(KO_CV.BumpSteerCurve.WheelDeflections, Vehicle, DWSolver);

            ExtractToeAngles(KO_CV.VCornerParams.OC_BumpSteer);

            double bsError = EvaluateDeviation_BSChart(Calc_BumpSteerGraph, KO_CV.BumpSteerCurve.ToeAngles);

            return bsError;

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
            _dwSolver.KO_InitialStage_BumpSteer_LBJ(_wheelDeflection, out MathNet.Spatial.Euclidean.Point3D E);

            _dwSolver.KO_InitialStage_BumpSteeer_UBJ(E, out MathNet.Spatial.Euclidean.Point3D F);

            _dwSolver.KO_InitialStage_BumpSteer_ToeLinnkOutboard(_wheelDeflection, E, F, out MathNet.Spatial.Euclidean.Point3D M);

            _dwSolver.KO_InitialStage_BumpSteer_WheelCenter(E, F, M, out MathNet.Spatial.Euclidean.Point3D K);

            _dwSolver.KO_InitialStage_BumpSteer_WheelSpindleEnd(E, K, M, out MathNet.Spatial.Euclidean.Point3D L);

            _dwSolver.KO_InitialStage_BumpSteer_ContactPatch(E, K, M, out MathNet.Spatial.Euclidean.Point3D W);

            //_dwSolver.CalculatenewCamberAndToe_Rear(_oc, _index, _vehicle, (int)VCorner);

            _dwSolver.CalculatenewCamberAndToe_KO_Optimization(KO_CV.VCornerParams.OC_BumpSteer, _index, _vehicle, (int)VCorner, new Point3D(L.X, L.Y, L.Z), new Point3D(K.X, K.Y, K.Z));


            #region No need to store the variables like this during a Bump Steer Kinematics Run
            //KO_CV.VCornerParams.UBJ = new Point3D(F.X, F.Y, F.Z);

            //KO_CV.VCornerParams.WheelCenter = new Point3D(K.X, K.Y, K.Z);
            //KO_CV.VCornerParams.LBJ = new Point3D(E.X, E.Y, E.Z);

            //KO_CV.VCornerParams.ToeLinkOutboard = new Point3D(M.X, M.Y, M.Z);

            //KO_CV.VCornerParams.WcEnd = new Point3D(L.X, L.Y, L.Z);

            //KO_CV.VCornerParams.ContactPatch = new Point3D(W.X, W.Y, W.Z);




            ///<summary>Assigning all the computed points to a the <see cref="UnsprungAssembly"/> dictionary</summary>
            TempOutboardAssembly[CoordinateOptions.UBJ.ToString()] = new Point3D(F.X, F.Y, F.Z);

            TempOutboardAssembly[CoordinateOptions.LBJ.ToString()] = new Point3D(E.X, E.Y, E.Z);

            TempOutboardAssembly[CoordinateOptions.ToeLinkOutboard.ToString()] = new Point3D(M.X, M.Y, M.Z);

            TempOutboardAssembly[CoordinateOptions.WheelCenter.ToString()] = new Point3D(K.X, K.Y, K.Z);

            TempOutboardAssembly[CoordinateOptions.WheelSpindleEnd.ToString()] = new Point3D(L.X, L.Y, L.Z);

            TempOutboardAssembly[CoordinateOptions.ContactPatch.ToString()] = new Point3D(W.X, W.Y, W.Z);
            #endregion

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

    public class KO_Optimizer_ActuationPoints : Master_Optimizer
    {

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
        }

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed </param>
        /// <param name="_koCV"></param>
        /// <param name="_koCVFR"></param>
        /// <param name="_koCVRL"></param>
        /// <param name="_koCVRR"></param>
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

            Population = new Population(PopulationSize, 30, false, true);

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



        #endregion

    }

}


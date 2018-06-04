using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GAF;
using GAF.Operators;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using MathNet.Spatial.Units;
using MathNet.Numerics.LinearAlgebra;

namespace Coding_Attempt_with_GUI
{
    public class OptimizerGeneticAlgorithm
    {
        #region ---DECLARATIONS---

        #region --GENETIC ALGORITHM PROPERTIES--
        //--Genetic Algorithm Properties--

        /// <summary>
        /// Percentage of the Population which is going to be subject to Crossover
        /// </summary>
        double CrossOverProbability { get; set; }
        /// <summary>
        /// Crossover Operator which takes the <see cref="CrossOverProbability"/> as a argument and is added to the <see cref="GA"/> during its initialization
        /// </summary>
        Crossover Crossovers { get; set; }
        /// <summary>
        /// Percentage of the Population which is going to be subject to Random Mutation
        /// </summary>
        double MutationProbability { get; set; }
        /// <summary>
        /// Mutate Operator which takes the <see cref="MutationProbability"/> as an argument and is added to the <see cref="GA"/> during it initialization
        /// </summary>
        BinaryMutate Mutations { get; set; }
        /// <summary>
        /// Percentage of the population which is going to be treated as Elite without an modifications to the Genes
        /// </summary>
        int ElitePercentage { get; set; }
        /// <summary>
        /// Elite operator which takes the <see cref="ElitePercentage"/> as an argument and is added to the <see cref="GA"/> during its initialization
        /// </summary>
        Elite Elites { get; set; }
        /// <summary>
        /// Size of the Population
        /// </summary>
        int PopulationSize { get; set; }
        /// <summary>
        /// Population which will be passed to the <see cref="GA"/> during its initialization
        /// </summary>
        Population Population { get; set; }
        /// <summary>
        /// <para>Chromosome Length of the <see cref="Population"/></para>
        /// <para>Decided based on 2 things: The number of Genes and the bit size required for each Gene</para>
        /// <para>https://gaframework.org/wiki/index.php/How_to_Encode_Parameters for more information</para>
        /// </summary>
        int ChromosomeLength { get; set; }
        /// <summary>
        /// The <see cref="GeneticAlgorithm"/>
        /// </summary>
        GeneticAlgorithm GA { get; set; }
        /// <summary>
        /// <para><see cref="Delegate"/> which points to the local Fitness Function</para>
        /// <para>In this case the <see cref="EvaluateFitnessCurve(Chromosome)"/></para>
        /// </summary>
        FitnessFunction EvaluateFitnessOfGeneticAlforithm;
        /// <summary>
        /// <para><see cref="Delegate"/> which points to the Terminate Function</para>
        /// <para>In this case the <see cref="TerminateAlgorithm(Population, int, long)"/></para>
        /// </summary>
        TerminateFunction Terminate;
        /// <summary>
        /// Maximium Error in a particular Generation
        /// </summary>
        double MaximumErrorOfGeneration { get; set; }
        /// <summary>
        /// Dictionary of <see cref="OptimizedOrientation"/> into the which the Genetic Algorithm passes it's iteration vales
        /// </summary>
        //Dictionary<string, OptimizedOrientation> GAOrientation { get; set; }

        public double ga_TopFront;

        public double ga_TopRear;

        public double ga_BottomFront;

        public double ga_BottomRear;

        public double ga_ToeLink;

        public double ga_TopCamberShims;

        public double ga_BottomCamberShims;

        public Point3D ga_ToeLinkInboard;

        DataTable Ga_Values { get; set; }

        List<object> GA_Values_Params;

        ///// <summary>
        ///// Integer to determine the Bit Size of each of the <see cref="Gene"/>s inside the <see cref="Chromosome"/>
        ///// </summary>
        //int BitSize;

        Point3D BestFit_CurrGen_ToeLinkInboard;

        double BestFitness_CurrGen;

        Dictionary<string, double[,]> Fitness_Individual_Objectives;

        int SolutionCounter { get; set; }

        int No_GaOutputs { get; set; }
        #endregion

        #region --FITNESS FUNCTION EVALUATION PARAMETERS--

        //--FITNESS FUNCTION EVALUATION PARAMETERS--


        //---IMPORTANT--- These are not Parameters of the Genetic Algorithm. These are parameters which the FITNESS FUCNTION will use to determine the Fitness. 
        /// <summary>
        /// Step Size of the Wheel Defelction.
        /// </summary>
        int SuspensionEvalStepSize;
        /// <summary>
        /// Upper Limit of Wheel Deflection
        /// </summary>
        int SuspensionEvalUpperLimit;
        /// <summary>
        /// Lower Limit of the Wheel Deflection
        /// </summary>
        int SuspensionEvalLowerLimit;
        /// <summary>
        /// <para>Number of Iterations that the <see cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/> Solver 
        /// is going to run</para>
        /// <para>Calculated based on the <see cref="SuspensionEvalStepSize"/> and the Range which is calculated using the <see cref="SuspensionEvalUpperLimit"/> and <see cref="SuspensionEvalLowerLimit"/></para>
        /// </summary>
        int SuspensionEvalIterations;

        int SuspensionEvalIterations_UpperLimit;

        int SuspensionEvalIterations_LowerLimit;
        #endregion

        #region --VEHICLE & SUSPENSION PARAMETERS--
        
        //--VEHICLE PARAMETERS--
        
        /// <summary>
        /// Object of the <see cref="VehicleCorner"/> which decides the corner of the Vehicle calling this Class
        /// </summary>
        VehicleCorner VCorner;
        /// <summary>
        /// Suspension Coordiantes of the corner of the Vehicle calling this class
        /// </summary>
        SuspensionCoordinatesMaster SCM;
        /// <summary>
        /// Cloned Object of the <see cref="SCM"/>. This is used to prevent residue error as in each iteraiton a fresh simulation is run
        /// </summary>
        SuspensionCoordinatesMaster SCM_Clone;
        /// <summary>
        /// Spring of the corner of the Vehicle calling this class
        /// </summary>
        Spring Spring;
        /// <summary>
        /// Damper of the corner of the Vehicle calling this class
        /// </summary>
        Damper Damper;
        /// <summary>
        /// Anti-Roll Bar of the corner of the Vehicle calling this class
        /// </summary>
        AntiRollBar ARB;
        /// <summary>
        /// Anti-Roll Bar Rate in N/mm of the corner of the Vehicle calling this class
        /// </summary>
        double ARBRate_Nmm;
        /// <summary>
        /// Tire of the corner of the Vehicle calling this class
        /// </summary>
        Tire Tire;
        /// <summary>
        /// Chassis of the corner of the Vehicle calling this class
        /// </summary>
        Chassis Chassis;
        /// <summary>
        /// Wheel Alignmetn of the corner of the Vehicle calling this class
        /// </summary>
        WheelAlignment WA;
        /// <summary>
        /// <see cref="List{T}"/> of <see cref="OutputClass"/> objects of the corner of the Vehicle calling this class
        /// </summary>
        List<OutputClass> OC;
        /// <summary>
        /// This <see cref="OutputClass"/> List is exclusively used for the Bump Steer method as it needs to run a Motion Analysis from -25 to +25 (or whatever the user decides)
        /// </summary>
        List<OutputClass> OC_BumpSteer;
        /// <summary>
        /// Identifier Number of the corner of the Vehicle calling this class
        /// </summary>
        int Identifier;
        /// <summary>
        /// The object of the Vehicle itself which is calling this class
        /// </summary>
        Vehicle Vehicle;

        //-RELEVANT COORDINATES-
        
        /// <summary>
        /// Upper Ball Joint
        /// </summary>
        Point3D UBJ;
        /// <summary>
        /// 2nd Upper Ball Joint incase 5 Link Suspension
        /// </summary>
        Point3D UBJ_2;
        /// <summary>
        /// Pushrod
        /// </summary>
        Point3D Pushrod;
        /// <summary>
        /// Lower Ball Joint
        /// </summary>
        Point3D LBJ;
        /// <summary>
        /// 2nd Lower Ball Joint incase 5 Link Suspension
        /// </summary>
        Point3D LBJ_2;
        /// <summary>
        /// Outboard Toe Link Joint
        /// </summary>
        Point3D ToeLinkOutboard;
        /// <summary>
        /// Wheel Center Start
        /// </summary>
        Point3D WcStart;
        /// <summary>
        /// Wheel Center End
        /// </summary>
        Point3D WcEnd;
        /// <summary>
        /// Contact Patch 
        /// </summary>
        Point3D ContactPatch;

        /// <summary>
        /// Inboard Toe Link Joint
        /// </summary>
        Point3D ToeLinkInboard;
        /// <summary>
        /// 
        /// </summary>
        Point3D TopFront;
        /// <summary>
        /// 
        /// </summary>
        Point3D TopRear;
        /// <summary>
        /// 
        /// </summary>
        Point3D BottomFront;
        /// <summary>
        /// 
        /// </summary>
        Point3D BottomRear;
        /// <summary>
        /// 
        /// </summary>
        Point3D PushrodShockMount;
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, OptimizedCoordinate> UnsprungAssembly;
        /// <summary>
        /// 
        /// </summary>
        //Dictionary<string, OptimizedCoordinate> tempInboardPoints;
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, Line> tempAxisLines;
        /// <summary>
        /// Name is Critical
        /// Works in Delta
        /// </summary>
        //Dictionary<string, Opt_AdjToolParams> CasterRequested;

        //Dictionary<string, Opt_AdjToolParams> ToeRequested;

        //Dictionary<string, Opt_AdjToolParams> CamberRequested;

        //Dictionary<string, Opt_AdjToolParams> KPIRequested;
        ///// <summary>
        ///// Worka with Nominal
        ///// </summary>
        //Dictionary<string, Opt_AdjToolParams> BumpSteerRequested;

        Dictionary<string, Dictionary<string, Opt_AdjToolParams>> MasterDictionary;

        Dictionary<String, double> Opt_AdjToolValues;


        #endregion

        #region ---Setup Change Params---
        public SetupChange_CornerVariables Setup_CV { get; set; }

        public SetupChange_Outputs Setup_OP { get; set; }

        public Angle Req_Camber { get; set; }

        public Angle Calc_Camber;

        public double CamberError { get; set; }

        public Angle Req_Caster { get; set; }

        public Angle Calc_Caster;

        public double CasterError { get; set; }

        public Angle Req_KPI { get; set; }

        public Angle Calc_KPI;

        public double KpiError { get; set; }

        public Angle Req_Toe { get; set; }

        public Angle Calc_Toe;

        public double ToeError { get; set; }

        public List<Angle> Req_BumpSteerGraph { get; set; }

        public List<Angle> Calc_BumpSteerGraph;

        public double BumpSteerError { get; set; }

        #endregion





        #region --Delegates--
        private delegate double ParamsToEvaluate();

        ParamsToEvaluate Del_RMS_Error;

        ParamsToEvaluate Del_Caster_Error;

        ParamsToEvaluate Del_KPI_Error;

        ParamsToEvaluate Del_Camber_Error;

        ParamsToEvaluate Del_Toe_Error;

        ParamsToEvaluate Del_BumpSteer_Error;

        #endregion

        #endregion

        #region ---INITIALIZATION METHODS---

        //---INITIALIZATION METHODS---

        /// <summary>
        /// <para>---1st--- This mehod is to be called First</para>
        /// <para>The Constructor of the <see cref="OptimizerGeneticAlgorithm"/> Class</para>
        /// </summary>
        /// <param name="_crossover">Percentage of the Population which is going to be subject to Crossover</param>
        /// <param name="_mutation">Percentage of the Population which is going to be subject to Random Mutation</param>
        /// <param name="_elites">Percentage of the population which is going to be treated as Elite without an modifications to the Genes</param>
        /// <param name="_popSize">Size of the Population</param>
        /// <param name="_chromoseLength"> <para>Chromosome Length of the <see cref="Population"/></para>
        /// <para>Decided based on 2 things: The number of Genes and the bit size required for each Gene</para>
        /// <para>https://gaframework.org/wiki/index.php/How_to_Encode_Parameters for more information</para> </param>
        public OptimizerGeneticAlgorithm(double _crossover, double _mutation, int _elites/*, int _popSize, int _chromoseLength*/)
        {
            //BitSize = 30;

            BestFitness_CurrGen = 0;

            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;


            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Fitness_Individual_Objectives = new Dictionary<string, double[,]>();

            BestFit_CurrGen_ToeLinkInboard = new Point3D();

            //---NEEDED---
            //InitializeDataTable(_setupsRequested);

            //No_GaOutputs = _chromoseLength / BitSize;

            Opt_AdjToolValues = new Dictionary<string, double>();

        }

        private void InitializeDataTable(List<string> _setupsRequested)
        {
            //Ga_Values = new DataTable();

            //GA_Values_Params = new List<object>();


            //Ga_Values.Columns.Add("Solution Name", typeof(string));
            //GA_Values_Params.Add("SolutionName");


            ////Ga_Values.Columns.Add("Orientation", typeof(OptimizedOrientation));
            ////GA_Values_Params.Add(new OptimizedOrientation());


            //Ga_Values.Columns.Add("Orientation Fitness", typeof(double));
            //GA_Values_Params.Add(0);


            //Ga_Values.Columns.Add("Toe Link Inboard", typeof(OptimizedCoordinate));
            //GA_Values_Params.Add(new Point3D());


            //Ga_Values.Columns.Add("Bump Steer Fitness", typeof(double));
            //GA_Values_Params.Add(0);


            //for (int i = 0; i < _adjustLinkNames.Count; i++)
            //{
            //    Ga_Values.Columns.Add(_adjustLinkNames[i], typeof(double));
            //    GA_Values_Params.Add(0);
            //}

            //Ga_Values.Columns.Add("Pareto Rank", typeof(double));

            //Ga_Values.Columns.Add("Pareto Optimal", typeof(bool));
            //GA_Values_Params.Add(false);

            //Ga_Values.Columns.Add("RMS Fitness", typeof(double));

            //for (int i = 0; i < Ga_Values.Columns.Count; i++)
            //{
            //    Ga_Values.Columns[0].ReadOnly = true;
            //}


        }

        /// <summary>
        /// <para>---2nd--- This method is to be called 4th in the sequence </para>
        /// <para>Method to Initialize the Setup Change Parameters which include the Final Values of the Setup Changes and the Tools available to adjust them </para>
        /// </summary>
        /// <param name="_masterD"><see cref="Dictionary{TKey, Dictionary}"/> (Dictionary inside a Dictionary). That is a the master Dictionary which consists of the Sub Dictionaries which contains the Adjusters of each Setup Change</param>
        /// <param name="_fCamber"></param>
        /// <param name="_fCaster"></param>
        /// <param name="_fToe"></param>
        /// <param name="_fKPI"></param>
        public void InitializeSetupParams(SetupChange_CornerVariables _reqChanges, SetupChange_Outputs _setupOP, Dictionary<string, Dictionary<string, Opt_AdjToolParams>> _masterD, Angle _fCamber, Angle _fCaster, Angle _fToe, Angle _fKPI)
        {
            ///<summary>Assigning the <see cref="SetupChange_CornerVariables"/> object</summary>
            Setup_CV = _reqChanges;

            ///<summary>Passing the Master Dictionary which contains the Dictionaries (with Adjustmer Options) of all the Setup Changes requested</summary>
            MasterDictionary = _masterD;

            ///<summary>Passing the <see cref="SetupChange_Outputs"/> object</summary>
            Setup_OP = _setupOP;

            ///<summary>Passing all the Requested Values of the Setup Change. If there is not value requested then the Required and Initial value of the Param is the same</summary>
            Req_Camber = _fCamber;

            Req_Caster = _fCaster;

            Req_Toe = _fToe;

            Req_KPI = _fKPI;

            Req_BumpSteerGraph = Setup_CV.BS_Params.ToeAngles;
        }

        /// <summary>
        /// <para>---3rd--- This method is to be called 2nd in the sequence</para>
        /// <para>Method to Initialize the Vehicle of the class and initialize all the parameters of the Vehicle along with it  </para>
        /// </summary>
        /// <param name="_vCorner">Object of the <see cref="VehicleCorner"/> which decides the corner of the Vehicle calling this Class</param>
        /// <param name="_vehicle">The object of the Vehicle itself which is calling this class</param>
        public void InitializeVehicleParams(VehicleCorner _vCorner, Vehicle _vehicle)
        {
            ///<summary>
            ///Assigning default values of Step Size, Upper and Lower Limit of the Wheel Deflections to create a default Motion profile to evalluate Bump Steer. 
            ///This will be used in case the user doesn't create a Bummp Steer Chart
            /// </summary>
            SuspensionEvalStepSize = Setup_CV.BS_Params.StepSize;
            SuspensionEvalLowerLimit = -25;
            SuspensionEvalUpperLimit = 25;

            ///<summary>Calculating the Number of iterations based on the <see cref="SuspensionEvalStepSize"/> the <see cref="SuspensionEvalUpperLimit"/> and the <see cref="SuspensionEvalLowerLimit"/></summary>
            SuspensionEvalIterations = GetNoOfIterations(SuspensionEvalUpperLimit, SuspensionEvalLowerLimit, SuspensionEvalStepSize);
            SuspensionEvalIterations_UpperLimit = GetNoOfIterations(SuspensionEvalUpperLimit, 0, SuspensionEvalStepSize);
            SuspensionEvalIterations_LowerLimit = GetNoOfIterations(0, SuspensionEvalLowerLimit, SuspensionEvalStepSize);


            Vehicle = _vehicle;

            VCorner = _vCorner;

            ///<summary>Invoking the <see cref="VehicleParamsAssigner.AssignVehicleParams_PostSolver(VehicleCorner, Vehicle, int)"/> method to assign the right Vehicle's Params (based on the Vehicle Corner) into a Dictionary</summary>
            Dictionary<string, object> tempVehicleParams = VehicleParamsAssigner.AssignVehicleParams_PostKinematicsSolver(_vCorner, _vehicle, 0);

            ///<summary>Passing the <see cref="Dictionary{TKey, TValue}"/> of Vehicle Params's objects into the right Parameter</summary>
            SCM = tempVehicleParams["SuspensionCoordinateMaster"] as SuspensionCoordinatesMaster;

            SCM_Clone = new SuspensionCoordinatesMaster();
            SCM_Clone.Clone(SCM);

            Tire = tempVehicleParams["Tire"] as Tire;

            Spring = tempVehicleParams["Spring"] as Spring;

            Damper = tempVehicleParams["Damper"] as Damper;

            ARB = tempVehicleParams["AntirollBar"] as AntiRollBar;
            ARBRate_Nmm = (double)tempVehicleParams["ARB_Rate_Nmm"];

            WA = tempVehicleParams["WheelAlignment"] as WheelAlignment;

            Chassis = tempVehicleParams["Chassis"] as Chassis;

            OC = tempVehicleParams["OutputClass"] as List<OutputClass>;

            OC_BumpSteer = VehicleParamsAssigner.AssignVehicleParams_Custom_OC_BumpSteer(SCM, _vCorner, _vehicle, (SuspensionEvalIterations * 2) + 1);

            Identifier = (int)tempVehicleParams["Identifier"];

            PopulateDictionaryTrial();

            ///<summary>Assigning the Nominal Coordinate of the <see cref="ToeLinkInboard"/> in case Bump Steer change/constant is requested</summary>
            if (MasterDictionary.ContainsKey("Bump Steer"))
            {
                MasterDictionary["Bump Steer"][AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x"].Nominal = SCM.N1x;
                MasterDictionary["Bump Steer"][AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y"].Nominal = SCM.N1y;
                MasterDictionary["Bump Steer"][AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z"].Nominal = SCM.N1z;
            }

        }



        private double Dummy() { return 0; }

        /// <summary>
        /// <para>---4th---This method is to be called 4th in the Sequence</para>
        /// <para>Method to Initialize the Delegates</para>
        /// </summary>
        public void Set_ErrorsToEvaluate()
        {
            Del_KPI_Error = new ParamsToEvaluate(ComputeKPIError);

            Del_Caster_Error = new ParamsToEvaluate(ComputeCasterError);

            Del_Camber_Error = new ParamsToEvaluate(ComputeCamberError);

            Del_Toe_Error = new ParamsToEvaluate(ComputeToeError);

            Del_BumpSteer_Error = new ParamsToEvaluate(ComputeBumpSteerError);

            Del_RMS_Error = new ParamsToEvaluate(Dummy);

            if (Setup_CV.constKPI == true || Setup_CV.KPIChangeRequested)
            {
                Del_RMS_Error += Del_KPI_Error;
            }
            if (Setup_CV.constCaster == true || Setup_CV.CasterChangeRequested)
            {
                Del_RMS_Error += Del_Caster_Error;
            }
            if (Setup_CV.constCamber == true || Setup_CV.CamberChangeRequested)
            {
                Del_RMS_Error += Del_Camber_Error;
            }
            if (Setup_CV.constToe == true || Setup_CV.ToeChangeRequested)
            {
                Del_RMS_Error += Del_Toe_Error;
            }
            if (Setup_CV.constBumpSteer || Setup_CV.BumpSteerChangeRequested)
            {
                Del_RMS_Error += Del_BumpSteer_Error;
            }

        }

        /// <summary>
        /// <para>---5th--- This method is to be called 5th in the sequence</para>
        /// <para>Method to construct and run the <see cref="GA"/> (<see cref="GeneticAlgorithm"/>)</para>
        /// </summary>
        public void ConstructGeneticAlgorithm(int _popSize)
        {
            PopulationSize = _popSize;

            Population = new Population(_popSize, GetChromsomeLength(), false, true);

            //No_GaOutputs = _chromoseLength / BitSize;

            ///<summary>Assigning the <see cref="Delegate"/> of the Ftiness Function</summary>
            EvaluateFitnessOfGeneticAlforithm = EvaluateFitnessCurve;
            ///<summary>Assigning the <see cref="Delegate"/> of the Terminate Function</summary>
            Terminate = TerminateAlgorithm;

            GA = new GeneticAlgorithm(Population, EvaluateFitnessOfGeneticAlforithm);

            ///<summary>Assigning an Event for the <see cref="GeneticAlgorithm"/></summary>
            GA.OnGenerationComplete += GA_OnGenerationComplete;
            GA.OnRunComplete += GA_OnRunComplete;


            ///<summary>Adding the <see cref="Elites"/> <see cref="Crossovers"/> and the <see cref="Mutations"/> operator to the <see cref="GA"/></summary>
            GA.Operators.Add(Elites);
            GA.Operators.Add(Crossovers);
            GA.Operators.Add(Mutations);

            ///<summary>Running the Algorithm</summary>
            if (GetChromsomeLength() != 0)
            {
                GA.Run(Terminate);

            }
        }

        #endregion

        #region ---GENETIC ALGORITHM METHODS

        //---GENETIC ALGORITHM METHODS---

        /// <summary>
        /// Method which determines the Fitness of a particular <see cref="Chromosome"/> in a <see cref="Population"/>
        /// </summary>
        /// <param name="chromosome"><see cref="Chromosome"/> which is passed by the <see cref="GeneticAlgorithm.Population"/> on its own </param>
        /// <returns>Returns the Fitness Values of the <see cref="Chromosome"/>. Must be between 0 and 1</returns>
        private double EvaluateFitnessCurve(Chromosome chromosome)
        {
            double Fitness = -1;

            if (chromosome != null)
            {
                ///<summary>Invokinig the <see cref="ExtractOptimizedValues(Chromosome, out double, out double, out double)"/> which extracts the coordinates </summary>
                //ExtractOptimizedValues(chromosome, out double x, out double y, out double z);
                ExtractOptimizedValues(chromosome);
                //Update_GADataTable("Solution" + SolutionCounter);

                ///<summary>Invoking the <see cref="ComputeBumpSteerError(double, double, double)"/> method to check the error of the calcualted Bump Steer Curve with the Curve that the user wants</summary>
                double resultError = EvaluateRMSError(SolutionCounter);

                ///<summary>Calculating the Fitness based on the Error</summary>
                Fitness = 1 - (resultError);

                SolutionCounter++;
            }

            return Fitness;
        }



        /// <summary>
        /// Event which is fired when the Creation and Fitness Evaluation of a particular <see cref="Population"/> is complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GA_OnGenerationComplete(object sender, GaEventArgs e)
        {
            ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
            var chromosome = e.Population.GetTop(1)[0];


            ///<summary>
            ///Invokinig the <see cref="ExtractOptimizedValues(Chromosome, out double, out double, out double)"/> which extracts the coordinates 
            ///of the BEST FIT of this Generation
            /// </summary>
            ExtractOptimizedValues(chromosome);

            //Ga_Values.DefaultView.Sort = "RMS Fitness";

            ///<summary>Extracting the Max Fitness</summary>
            double Fitness = e.Population.MaximumFitness;

            
            int Generations = e.Generation;
            
            long Evaluations = e.Evaluations;

            Anneal(Fitness);

            EvaluateParetoOptimial();

            int MaxRowIndex = /*GetMaxIndex()*/0;

            double resultError = EvaluateRMSError(MaxRowIndex);

            //DataTable clone = Ga_Values.Copy();

            //GetParetoSolutions();

            SolutionCounter = 0;

            //Ga_Values.Rows.Clear();


        }


        private void GA_OnRunComplete(object sender, GaEventArgs e)
        {
        }

        /// <summary>
        /// Method to terminate the Algorithm
        /// </summary>
        /// <param name="_population"><see cref="Population"/> of the <see cref="GA"/></param>
        /// <param name="_currGeneration">Current Generation being evaluated</param>
        /// <param name="currEvaluation">Current Evaluation</param>
        /// <returns>Returns <see cref=Boolean"/> to determine if the Algoritm should termine or not </returns>
        private bool TerminateAlgorithm(Population _population, int _currGeneration, long currEvaluation)
        {
            if (_currGeneration > 50)
           {
                ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
                var chromosome = _population.GetTop(1)[0];

                ///<summary>
                ///Invokinig the <see cref="ExtractOptimizedValues(Chromosome, out double, out double, out double)"/> which extracts the coordinates 
                /// of the BEST FIT of this Generation
                /// </summary>
                //ExtractOptimizedValues(chromosome, out double x, out double y, out double z);
                ExtractOptimizedValues(chromosome);

                ///<summary>Extracting the Max Fitness</summary>
                double Fitness = _population.MaximumFitness;

                int MaxRowIndex = /*GetMaxIndex()*/0;

                double resultError = EvaluateRMSError(MaxRowIndex);

                return true;


            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ---TEMP--- METHOD TO POPULATE THE DICTIONARIES LOCALLY 
        //---TEMP---
        private void PopulateDictionaryTrial()
        {
            //GAOrientation = new Dictionary<string, OptimizedOrientation>();

            //Point3D upperLimit = new Point3D(10, 10, 10);

            //Point3D LowerLimit = new Point3D(-10, -10, -10);

            //MathNet.Spatial.Euclidean.EulerAngles upperEuler = new MathNet.Spatial.Euclidean.EulerAngles(new Angle(5, AngleUnit.Degrees), new Angle(5, AngleUnit.Degrees), new Angle(5, AngleUnit.Degrees));

            //MathNet.Spatial.Euclidean.EulerAngles lowerEuler = new MathNet.Spatial.Euclidean.EulerAngles(new Angle(0, AngleUnit.Degrees), new Angle(-5, AngleUnit.Degrees), new Angle(-5, AngleUnit.Degrees));

            //GAOrientation.Add("NewOrientation1", new OptimizedOrientation(upperLimit, LowerLimit, upperEuler, lowerEuler, BitSize));

            PopulateDictionaryTrial_2();

        }
        //---TEMP--
        private void PopulateDictionaryTrial_2()
        {
            Point3D Upper = new Point3D(20, 20, 20);

            Point3D Lower = new Point3D(-20, -20, -20);


            UnsprungAssembly = new Dictionary<string, OptimizedCoordinate>();

            //tempInboardPoints = new Dictionary<string, OptimizedCoordinate>();

            ToeLinkInboard = new Point3D(SCM.N1x, SCM.N1y, SCM.N1z);
            ga_ToeLinkInboard = ToeLinkInboard.Clone() as Point3D;
            //tempInboardPoints.Add("ToeLinkInboard", new OptimizedCoordinate(ToeLinkInboard, Upper, Lower, BitSize));

            TopFront = new Point3D(SCM.A1x, SCM.A1y, SCM.A1z);

            TopRear = new Point3D(SCM.B1x, SCM.B1y, SCM.B1z);

            BottomFront = new Point3D(SCM.D1x, SCM.D1y, SCM.D1z);

            BottomRear = new Point3D(SCM.C1x, SCM.C1y, SCM.C1z);


            UBJ = new Point3D(SCM.F1x, SCM.F1y, SCM.F1z);

            UnsprungAssembly.Add("UBJ", new OptimizedCoordinate(UBJ, Upper, Lower));

            UnsprungAssembly.Add("TopCamberMount", new OptimizedCoordinate(UBJ.Clone() as Point3D, Upper, Lower));

            Pushrod = new Point3D(SCM.G1x, SCM.G1y, SCM.G1z);

            UnsprungAssembly.Add("Pushrod", new OptimizedCoordinate(Pushrod, Upper, Lower));

            PushrodShockMount = new Point3D(SCM.H1x, SCM.H1y, SCM.H1z);

            LBJ = new Point3D(SCM.E1x, SCM.E1y, SCM.E1z);

            UnsprungAssembly.Add("BottomCamberMount", new OptimizedCoordinate(LBJ.Clone() as Point3D, Upper, Lower));

            UnsprungAssembly.Add("LBJ", new OptimizedCoordinate(LBJ, Upper, Lower));

            WcStart = new Point3D(SCM.K1x, SCM.K1y, SCM.K1z);

            UnsprungAssembly.Add("WcStart", new OptimizedCoordinate(WcStart, Upper, Lower));

            WcEnd = new Point3D(SCM.L1x, SCM.L1y, SCM.L1z);

            UnsprungAssembly.Add("WcEnd", new OptimizedCoordinate(WcEnd, Upper, Lower));

            ToeLinkOutboard = new Point3D(SCM.M1x, SCM.M1y, SCM.M1z);

            UnsprungAssembly.Add("ToeLinkOutboard", new OptimizedCoordinate(ToeLinkOutboard, Upper, Lower));

            ContactPatch = new Point3D(SCM.W1x, SCM.W1x, SCM.W1z);

            UnsprungAssembly.Add("ContactPatch", new OptimizedCoordinate(ContactPatch, Upper, Lower));


            tempAxisLines = new Dictionary<string, Line>();

            tempAxisLines.Add("SteeringAxis", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

            tempAxisLines.Add("SteeringAxis_Ref", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

            tempAxisLines.Add("LateralAxis_WheelCenter", new Line(WcStart.Clone() as Point3D, new Point3D(WcStart.X + 100, WcStart.Y, WcStart.Z)));

            tempAxisLines.Add("WheelSpindle", new Line(WcStart.Clone() as Point3D, WcEnd.Clone() as Point3D));

            tempAxisLines.Add("WheelSpindle_Ref", new Line(WcStart.Clone() as Point3D, WcEnd.Clone() as Point3D));

            tempAxisLines.Add("VerticalAxis_WheelCenter", new Line(WcStart.Clone() as Point3D, new Point3D(WcStart.X, WcStart.Y + 100, WcStart.Z)));

            tempAxisLines.Add("LongitudinalAxis_WheelCenter", new Line(WcStart.Clone() as Point3D, new Point3D(WcStart.X, WcStart.Y, WcStart.Z + 100)));



            /////<remarks>For the Camber/Caster/Toe/KPI we are dealing with Deltas and hence we don't need Nominal</remarks>
            //CasterRequested = new Dictionary<string, Opt_AdjToolParams>();

            //CasterRequested.Add(AdjustmentTools.TopFrontArm.ToString(), new Opt_AdjToolParams(AdjustmentTools.TopFrontArm.ToString(), 0, 10, -10, 30));

            //ToeRequested = new Dictionary<string, Opt_AdjToolParams>();

            //ToeRequested.Add(AdjustmentTools.ToeLinkLength.ToString(), new Opt_AdjToolParams(AdjustmentTools.ToeLinkLength.ToString(), 0, 10, -10, 30));

            //KPIRequested = new Dictionary<string, Opt_AdjToolParams>();

            //KPIRequested.Add(AdjustmentTools.BottomRearArm.ToString(), new Opt_AdjToolParams(AdjustmentTools.BottomRearArm.ToString(), 0, 10, -10, 30));

            //CamberRequested = new Dictionary<string, Opt_AdjToolParams>();

            //CamberRequested.Add(AdjustmentTools.TopCamberMount.ToString(), new Opt_AdjToolParams(AdjustmentTools.TopCamberMount.ToString(), 0, 10, -10, 30));



            /////<remarks>For the Bump Steer Chnge there exists a nominal Value</remarks>
            //BumpSteerRequested = new Dictionary<string, Opt_AdjToolParams>();

            //BumpSteerRequested.Add(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x", new Opt_AdjToolParams(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x", 232.12, 5, -5, 30));
            //BumpSteerRequested.Add(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y", new Opt_AdjToolParams(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y", 124.4, 5, -5, 30));
            //BumpSteerRequested.Add(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z", new Opt_AdjToolParams(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z", 60.8, 5, -5, 30));

            //MasterDictionary = new Dictionary<string, Dictionary<string, Opt_AdjToolParams>>();

            //MasterDictionary.Add("CasterRequested", CasterRequested);
            //MasterDictionary.Add("CamberRequested", CamberRequested);
            //MasterDictionary.Add("ToeRequested", ToeRequested);
            //MasterDictionary.Add("BumpSteerRequested", BumpSteerRequested);

            //OptimizedAdjToolValues = new Dictionary<string, double>();

            ////Array AdjTool =  Enum.GetValues(typeof(AdjustmentTools)); 

            ////for (int i = 0; i < AdjTool.Length; i++)
            ////{
            ////    OptimizedAdjToolValues.Add(AdjTool.GetValue(i).ToString(), 0);
            ////}

            //foreach (string item in CasterRequested.Keys)
            //{
            //    OptimizedAdjToolValues.Add(item, 0);
            //}

            ////foreach (string item in KPIRequested.Keys)
            ////{
            ////    OptimizedAdjToolValues.Add(item, 0);
            ////}
            //foreach (string item in CamberRequested.Keys)
            //{
            //    OptimizedAdjToolValues.Add(item, 0);
            //}
            //foreach (string item in ToeRequested.Keys)
            //{
            //    OptimizedAdjToolValues.Add(item, 0);
            //}
            //foreach (string item in BumpSteerRequested.Keys)
            //{
            //    OptimizedAdjToolValues.Add(item, 0);
            //}







        }
        #endregion

        #region --HELPER METHODS--


        //--HELPER METHODS--

        private int GetChromsomeLength()
        {
            int chromLength = 0;

            foreach (string changeORconstraint in MasterDictionary.Keys)
            {
                foreach (string adjTool in MasterDictionary[changeORconstraint].Keys)
                {
                    chromLength += MasterDictionary[changeORconstraint][adjTool].BitSize;
                }
            }

            return chromLength;
        }

        private int GetMaxIndex()
        {
            int maxRowIndex = 0;

            double rmsFitness = 0;

            for (int i = 0; i < Ga_Values.Rows.Count - 1; i++)
            {
                if (Ga_Values.Rows[i].Field<double>("RMS Fitness") > rmsFitness)
                {
                    rmsFitness = Ga_Values.Rows[i].Field<double>("RMS Fitness");
                    maxRowIndex = i;
                }
            }

            return maxRowIndex;
        }


        /// <summary>
        /// Method to calculate the Number of Iterations to be performed
        /// </summary>
        /// <param name="_upperLimitWheelDeflection">Upper Limit of th Wheel Defelction</param>
        /// <param name="_lowerLimitWheelDeflection">Lower Limit of the Wheel Deflection</param>
        /// <param name="_stepSize">Step Size </param>
        /// <returns></returns>
        private int GetNoOfIterations(int _upperLimitWheelDeflection, int _lowerLimitWheelDeflection, int _stepSize)
        {
            double Range = _upperLimitWheelDeflection - _lowerLimitWheelDeflection;

            return Convert.ToInt32(Range) / _stepSize;

        }

        private void Anneal(double Fitness)
        {
            if (Fitness > 0.994 && Fitness < 0.998 && Fitness < 0.999)
            {
                if (Fitness > BestFitness_CurrGen)
                {
                    BestFitness_CurrGen = Fitness;

                    ModfiyStepSize(0.5, 20, 8);
                }

            }
            else if (Fitness > 0.998 && Fitness < 0.999)
            {

                if (Fitness > BestFitness_CurrGen)
                {
                    BestFitness_CurrGen = Fitness;

                    ModfiyStepSize(0.3, 10, 4);
                }

            }
            else if (Fitness > 0.999 && Fitness < 0.9999)
            {

                if (Fitness > BestFitness_CurrGen)
                {
                    BestFitness_CurrGen = Fitness;

                    ModfiyStepSize(0.15, 5, 2);
                }

            }
            else if (Fitness > 0.9999)
            {
                if (Fitness > BestFitness_CurrGen)
                {
                    BestFitness_CurrGen = Fitness;

                    ModfiyStepSize(0.05, 2, 1);
                }
            }
        }

        private void ModfiyStepSize(double _range, double _coordinateRange,double _linkLengthRange)
        {


            //GAOrientation["NewOrientation1"].Upper_Orientation = new MathNet.Spatial.Euclidean.EulerAngles(GAOrientation["NewOrientation1"].OptimizedEulerAngles.Alpha + new Angle(_range, AngleUnit.Degrees),
            //                                                                                               GAOrientation["NewOrientation1"].OptimizedEulerAngles.Beta + new Angle(_range, AngleUnit.Degrees),
            //                                                                                               GAOrientation["NewOrientation1"].OptimizedEulerAngles.Gamma + new Angle(_range, AngleUnit.Degrees));

            //GAOrientation["NewOrientation1"].Lower_Orientation = new MathNet.Spatial.Euclidean.EulerAngles(GAOrientation["NewOrientation1"].OptimizedEulerAngles.Alpha - new Angle(_range, AngleUnit.Degrees),
            //                                                                                               GAOrientation["NewOrientation1"].OptimizedEulerAngles.Beta - new Angle(_range, AngleUnit.Degrees),
            //                                                                                               GAOrientation["NewOrientation1"].OptimizedEulerAngles.Gamma - new Angle(_range, AngleUnit.Degrees));


            //GAOrientation["NewOrientation1"].Upper_Origin = new Point3D(GAOrientation["NewOrientation1"].OptimizedOrigin.X + _range,
            //                                                            GAOrientation["NewOrientation1"].OptimizedOrigin.Y + _range,
            //                                                            GAOrientation["NewOrientation1"].OptimizedOrigin.Z + _range);

            //GAOrientation["NewOrientation1"].Lower_Origin = new Point3D(GAOrientation["NewOrientation1"].OptimizedOrigin.X - _range,
            //                                                            GAOrientation["NewOrientation1"].OptimizedOrigin.Y - _range,
            //                                                            GAOrientation["NewOrientation1"].OptimizedOrigin.Z - _range);


            ////InboardPoints["ToeLinkInboard"].UpperCoordinateLimit = new Point3D((InboardPoints["ToeLinkInboard"].NominalCoordinates.X - InboardPoints["ToeLinkInboard"].OptimizedCoordinates.X) + _coordinateRange,
            ////                                                                   (InboardPoints["ToeLinkInboard"].NominalCoordinates.Y - InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Y) + _coordinateRange,
            ////                                                                   (InboardPoints["ToeLinkInboard"].NominalCoordinates.Z - InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Z) + _coordinateRange);

            ////InboardPoints["ToeLinkInboard"].LowerCoordinateLimit = new Point3D((InboardPoints["ToeLinkInboard"].NominalCoordinates.X - InboardPoints["ToeLinkInboard"].OptimizedCoordinates.X) - _coordinateRange,
            ////                                                                   (InboardPoints["ToeLinkInboard"].NominalCoordinates.Y - InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Y) - _coordinateRange,
            ////                                                                   (InboardPoints["ToeLinkInboard"].NominalCoordinates.Z - InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Z) - _coordinateRange);

            //UpperWishboneLinkLength = WishboneLinkLength + _linkLengthRange;

            //LowerWishboneLinkLength = WishboneLinkLength - _linkLengthRange;

        }


        /// <summary>
        /// Overloaded Method to 
        /// </summary>
        /// <param name="chromosome"></param>
        /// <param name="_OptimizedOrigin"></param>
        /// <param name="_OptimizerOrientation"></param>
        private void ExtractOptimizedValues(Chromosome chromosome)
        {
            int geneNumber;

            geneNumber = 0;
            foreach (string SetupChange in MasterDictionary.Keys)
            {
                foreach (string adjTool in MasterDictionary[SetupChange].Keys)
                {
                    var range = GAF.Math.GetRangeConstant(MasterDictionary[SetupChange][adjTool].Uppwer - MasterDictionary[SetupChange][adjTool].Lower, MasterDictionary[SetupChange][adjTool].BitSize);

                    var gene = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * MasterDictionary[SetupChange][adjTool].BitSize, MasterDictionary[SetupChange][adjTool].BitSize), 2);
                    geneNumber++;

                    double param = System.Math.Round((gene * range) + (MasterDictionary[SetupChange][adjTool].Nominal + MasterDictionary[SetupChange][adjTool].Lower), 3);

                    MasterDictionary[SetupChange][adjTool].OptimizedIteration = param;

                    if (Opt_AdjToolValues.ContainsKey(adjTool))
                    {
                        Opt_AdjToolValues[adjTool] = param;
                    }
                    else
                    {
                        Opt_AdjToolValues.Add(adjTool, param);
                    }
                }
            }

            ExtractIndividualOptimizedValues(Opt_AdjToolValues);
            
        }

        private void ExtractIndividualOptimizedValues(Dictionary<string, double> _extractedValues)
        {
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.TopFrontArm.ToString()))
            {
                ga_TopFront = Opt_AdjToolValues[AdjustmentTools.TopFrontArm.ToString()];

                Setup_OP.TopFrontLength = ga_TopFront;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.TopRearArm.ToString()))
            {
                ga_TopRear = Opt_AdjToolValues[AdjustmentTools.TopRearArm.ToString()];

                Setup_OP.TopRearLength = ga_TopRear;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.BottomFrontArm.ToString()))
            {
                ga_BottomFront = Opt_AdjToolValues[AdjustmentTools.BottomFrontArm.ToString()];

                Setup_OP.BottomFrontLength = ga_BottomFront;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.BottomRearArm.ToString()))
            {
                ga_BottomRear = Opt_AdjToolValues[AdjustmentTools.BottomRearArm.ToString()];

                Setup_OP.BottomRearLength = ga_BottomRear;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.ToeLinkLength.ToString()))
            {
                ga_ToeLink= Opt_AdjToolValues[AdjustmentTools.ToeLinkLength.ToString()];

                Setup_OP.ToeLinklength = ga_ToeLink;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.TopCamberMount.ToString()))
            {
                ga_TopCamberShims = Opt_AdjToolValues[AdjustmentTools.TopCamberMount.ToString()];

                Setup_OP.TopCamberShimsLength = ga_TopCamberShims;

                Setup_OP.TopCamberShimsNo = ga_TopCamberShims / Setup_CV.camberShimThickness;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.BottomCamberMount.ToString()))
            {
                ga_BottomCamberShims = Opt_AdjToolValues[AdjustmentTools.BottomCamberMount.ToString()];

                Setup_OP.BottomCamberShimsLength = ga_BottomCamberShims;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x")) 
            {
                ga_ToeLinkInboard.X = Opt_AdjToolValues[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x"];

                Setup_OP.ToeLinkInboard.X = ga_ToeLinkInboard.X;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y"))
            {
                ga_ToeLinkInboard.Y = Opt_AdjToolValues[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y"];

                Setup_OP.ToeLinkInboard.Y = ga_ToeLinkInboard.Y;
            }
            if (Opt_AdjToolValues.ContainsKey(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z"))
            {
                ga_ToeLinkInboard.Z = Opt_AdjToolValues[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z"];

                Setup_OP.ToeLinkInboard.Z = ga_ToeLinkInboard.Z;
            }

        }

        private void Update_GADataTable(string _solutionName)
        {
            //Ga_Values.Rows.Add(_solutionName, GAOrientation["NewOrientation1"], 0, InboardPoints["ToeLinkInboard"], 0, WishboneLinkLength, 0, false, 0);
        }

        private double EvaluateRMSError(int rowIndex)
        {
            SolveKinematics();
            
            Update_SuspensionCoordinateData();


            BumpSteerError = CasterError = ToeError = CamberError = KpiError = 0;

            double rmsError = 0;

            //bumpSteerError = ComputeBumpSteerError();

            //casterError = ComputeCasterError();

            //toeError = ComputeToeError();

            //camberError = ComputeCamberError();

            //kpiError = ComputeKPIError();


            Del_RMS_Error();

            rmsError = System.Math.Sqrt((System.Math.Pow(BumpSteerError, 2) + System.Math.Pow(CasterError, 2) + System.Math.Pow(ToeError, 2) + System.Math.Pow(CamberError, 2) + System.Math.Pow(KpiError, 2)));

            //errorPile.Add(Del_RMS_Error());

            //for (int i = 0; i < errorPile.Count; i++)
            //{
            //    squareErrors += System.Math.Pow(errorPile[i], 2);
            //}

            //rmsError = System.Math.Sqrt(squareErrors);

            //Ga_Values.Rows[rowIndex].SetField<double>("Orientation Fitness", orientationError);
            //Ga_Values.Rows[rowIndex].SetField<double>("Bump Steer Fitness", bumpSteerError);
            //Ga_Values.Rows[rowIndex].SetField<double>("RMS Fitness", 1 - rmsError);

            //EvaluateWishboneConstraints();
            
            if (rmsError > 1 )
            {
                return 0.99;
            }
            else if (rmsError < 0)
            {
                return 0.99;
            }
            else return rmsError;
        }

        private void GetParetoSolutions()
        {
            //Dictionary<string,OptimizedOrientation> paretoOrientations = new Dictionary<string, OptimizedOrientation>();

            //Dictionary<string, Point3D> paretoToeLink = new Dictionary<string, Point3D>();

            //for (int i = 0; i < Ga_Values.Rows.Count; i++)
            //{
            //    if (Ga_Values.Rows[i].Field<bool>("Pareto Optimal"))
            //    {
            //        paretoOrientations.Add(Ga_Values.Rows[i].Field<double>("Orientation Fitness").ToString() + i, Ga_Values.Rows[i].Field<OptimizedOrientation>("Orientation"));
            //        paretoToeLink.Add(Ga_Values.Rows[i].Field<double>("Bump Steer Fitness").ToString() + i, Ga_Values.Rows[i].Field<OptimizedCoordinate>("Toe Link Inboard").OptimizedCoordinates);
            //    }
            //}
        }

        #region --- NOT NEEDED IN THIS ITERATION---
        //private List<double> EvaluateUpdatedOrientation(OptimizedOrientation _OptimizationOrientation)
        //{
        //    Transformation TransformationMatrix = GenerateTransformationMatrix(_OptimizationOrientation.OptimizedEulerAngles, _OptimizationOrientation.OptimizedOrigin);

        //    UpdateOrientation(TransformationMatrix);

        //    //return EvaluateWishboneConstraints();

        //    return null;
        //}

        //private Transformation GenerateTransformationMatrix(MathNet.Spatial.Euclidean.EulerAngles _optimizedOrientation, Point3D _optimizedOrigin)
        //{

        //    var UprightCSTranslation = Matrix<double>.Build.DenseOfArray(new double[,]
        //        {
        //            { (1) , (0) , (0) , (tempUnsprungAssembly["WcStart"].NominalCoordinates.X)},
        //            { (0) , (1) , (0) , (tempUnsprungAssembly["WcStart"].NominalCoordinates.Y)},
        //            { (0) , (0) , (1) , (tempUnsprungAssembly["WcStart"].NominalCoordinates.Z)},
        //            { (0) , (0) , (0) , (1)},
        //        });


        //    Angle a = _optimizedOrientation.Alpha;

        //    Angle b = _optimizedOrientation.Beta;

        //    Angle g = _optimizedOrientation.Gamma;

        //    double S1 = System.Math.Sin(a.Radians);

        //    double S2 = System.Math.Sin(b.Radians);

        //    double S3 = System.Math.Sin(g.Radians);


        //    double C1 = System.Math.Cos(a.Radians);

        //    double C2 = System.Math.Cos(b.Radians);

        //    double C3 = System.Math.Cos(g.Radians);

        //    var TaitRotationMatrix_YZX = Matrix<double>.Build.DenseOfArray(new double[,]
        //    {
        //        { ((C1 * C2))               ,   ((S1 * S3) - (C1 * C3 * S2))   ,   ((C3 * S1) + (C1 * S2 * S3 ))  ,   0},
        //        { (S2)                      ,   ((C2 * C3))                    ,   (-(C2 * S3))                   ,   0},
        //        { (-C2 * S1)                ,   ((C1 * S3) + (C3 * S1 * S2) )  ,   ((C1 * C3) - (S1 * S2 * S3))   ,   0},
        //        { (0)                       ,   (0)                            ,   (0)                            ,   1}

        //    });


        //    double dx = -_optimizedOrigin.X;

        //    double dy = -_optimizedOrigin.Y;

        //    double dz = -_optimizedOrigin.Z;



        //    var OrientationTranslationMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
        //    {
        //        { (1) , (0) , (0) , (dx)},
        //        { (0) , (1) , (0) , (dy)},
        //        { (0) , (0) , (1) , (dz)},
        //        { (0) , (0) , (0) , (1)}
        //    });

        //    var UprightCSInverseTranslation = Matrix<double>.Build.DenseOfArray(new double[,]
        //        {
        //            { (1) , (0) , (0) , (-tempUnsprungAssembly["WcStart"].NominalCoordinates.X)},
        //            { (0) , (1) , (0) , (-tempUnsprungAssembly["WcStart"].NominalCoordinates.Y)},
        //            { (0) , (0) , (1) , (-tempUnsprungAssembly["WcStart"].NominalCoordinates.Z)},
        //            { (0) , (0) , (0) , (1)},
        //        });

        //    Matrix<double> Transformation_1 = UprightCSTranslation.Multiply(TaitRotationMatrix_YZX);

        //    Matrix<double> Transformation_2 = Transformation_1.Solve(OrientationTranslationMatrix);

        //    Matrix<double> Transformation_3 = Transformation_2.Solve(UprightCSInverseTranslation);

        //    Matrix<double> FinalTransformation = Transformation_3;


        //    Transformation TransformationMatrix = new Transformation(FinalTransformation.ToArray());

        //    return TransformationMatrix;

        //}

        //private void UpdateOrientation(Transformation _transformationMatrix)
        //{
        //    foreach (string point in tempUnsprungAssembly.Keys)
        //    {
        //        Point3D tempPoint = tempUnsprungAssembly[point].NominalCoordinates.Clone() as Point3D;
        //        tempPoint.TransformBy(_transformationMatrix);
        //        tempUnsprungAssembly[point].OptimizedCoordinates = tempPoint.Clone() as Point3D;
        //    }

        //    tempAxisLines["SteeringAxis"] = new Line(tempUnsprungAssembly["UBJ"].OptimizedCoordinates, tempUnsprungAssembly["LBJ"].OptimizedCoordinates);

        //}

        #endregion

        //Trial for Caster Change with Toe Constant Constraint
        private void EvaluateWishboneConstraints()
        {
            double linkLengthError = 0;

            double TopFrontLength = System.Math.Round(SCM.UpperFrontLength, 3);

            double TopFrontLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["UBJ"].OptimizedCoordinates.DistanceTo(TopFront), 3);

            double TopRearLength = System.Math.Round(SCM.UpperRearLength, 3);

            double TopRearLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["UBJ"].OptimizedCoordinates.DistanceTo(TopRear), 3);

            double BottomFrontLength = System.Math.Round(SCM.LowerFrontLength, 3);

            double BottomFrontLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["LBJ"].OptimizedCoordinates.DistanceTo(BottomFront), 3);

            double BottomRearLength = System.Math.Round(SCM.LowerRearLength, 3);

            double BottomRearLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["LBJ"].OptimizedCoordinates.DistanceTo(BottomRear), 3);

            double ToeLinkLength = System.Math.Round(SCM.ToeLinkLength, 3);

            double ToeLinkLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["ToeLinkOutboard"].OptimizedCoordinates.DistanceTo(/*tempInboardPoints["ToeLinkInboard"].OptimizedCoordinates*/ga_ToeLinkInboard), 3);

            double PushrodLength = System.Math.Round(SCM.PushRodLength);

            double PushrodLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["Pushrod"].OptimizedCoordinates.DistanceTo(PushrodShockMount), 3);


            linkLengthError += System.Math.Pow(CalculateLinkLengthError(TopFrontLength, TopFrontLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateLinkLengthError(TopRearLength + -5.5 /*WishboneLinkLength*/, TopRearLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateLinkLengthError(BottomFrontLength, BottomFrontLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateLinkLengthError(BottomRearLength, BottomRearLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateLinkLengthError(ToeLinkLength, ToeLinkLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateLinkLengthError(PushrodLength, PushrodLength_UpdatedOrientation), 2);

            linkLengthError = System.Math.Sqrt(linkLengthError);
        }

        private double CalculateLinkLengthError(double _original, double _calculated)
        {
            double difference = ((_calculated - _original));

            double error = ((difference / _original/*10*/));

            return error;
        }

        private void Update_SuspensionCoordinateData()
        {
            Dictionary<string, Point3D> SuspensionCoordintes = ConvertTo_PointDictionary();

            SCM.Clone_Outboard_FromDictionary(SuspensionCoordintes);

            tempAxisLines["SteeringAxis"] = new Line(UnsprungAssembly["UBJ"].OptimizedCoordinates, UnsprungAssembly["LBJ"].OptimizedCoordinates);

            tempAxisLines["WheelSpindle"] = new Line(UnsprungAssembly["WcStart"].OptimizedCoordinates, UnsprungAssembly["WcEnd"].OptimizedCoordinates);

        }

        private Dictionary<string, Point3D> ConvertTo_PointDictionary()
        {
            Dictionary<string, Point3D> suspCoordinates = new Dictionary<string, Point3D>();

            foreach (string point in UnsprungAssembly.Keys)
            {
                suspCoordinates.Add(point, UnsprungAssembly[point].OptimizedCoordinates.Clone() as Point3D);
            }

            return suspCoordinates;
        }

        private void SolveKinematics()
        {
            dwSolver.AssignLocalCoordinateVariables_FixesPoints(SCM_Clone);
            dwSolver.AssignLocalCoordinateVariables_MovingPoints(SCM_Clone);
            dwSolver.L1x = SCM_Clone.L1x; dwSolver.L1y = SCM_Clone.L1y; dwSolver.L1z = SCM_Clone.L1z;
            dwSolver.OptimizedSteeringPoint = ga_ToeLinkInboard;
            dwSolver.AssignOptimizedSteeringPoints();

            OutputClass tempOC = new OutputClass();

            dwSolver.Optimization_SteeringAxis(ga_TopFront, ga_TopRear, ga_BottomFront, ga_BottomRear, Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D F, out MathNet.Spatial.Euclidean.Point3D E);

            dwSolver.Optimization_Pushrod(Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D G);

            dwSolver.Optimization_ToeLink(ga_ToeLink, Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D M);

            dwSolver.Optimization_CamberMountTop(ga_TopCamberShims, Vehicle, tempOC, out MathNet.Spatial.Euclidean.Point3D TCM);

            dwSolver.Optimization_CamberMountBottom(ga_BottomCamberShims, Vehicle, tempOC, out MathNet.Spatial.Euclidean.Point3D BCM);

            dwSolver.Optimization_WheelSpindleStart(Vehicle, tempOC, AdjustmentTools.TopCamberMount, out MathNet.Spatial.Euclidean.Point3D K);

            dwSolver.Optimization_WheelSpindleEnd(Vehicle, tempOC, AdjustmentTools.TopCamberMount, out MathNet.Spatial.Euclidean.Point3D L);

            dwSolver.Optimization_ContatcPatch(Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D W);

            UnsprungAssembly["UBJ"].OptimizedCoordinates = new Point3D(F.X, F.Y, F.Z);

            UnsprungAssembly["LBJ"].OptimizedCoordinates = new Point3D(E.X, E.Y, E.Z);

            UnsprungAssembly["Pushrod"].OptimizedCoordinates = new Point3D(G.X, G.Y, G.Z);

            UnsprungAssembly["ToeLinkOutboard"].OptimizedCoordinates = new Point3D(M.X, M.Y, M.Z);

            UnsprungAssembly["TopCamberMount"].OptimizedCoordinates = new Point3D(TCM.X, TCM.Y, TCM.Z);

            UnsprungAssembly["BottomCamberMount"].OptimizedCoordinates = new Point3D(BCM.X, BCM.Y, BCM.Z);

            UnsprungAssembly["WcStart"].OptimizedCoordinates = new Point3D(K.X, K.Y, K.Z);

            UnsprungAssembly["WcEnd"].OptimizedCoordinates = new Point3D(L.X, L.Y, L.Z);

            UnsprungAssembly["ContactPatch"].OptimizedCoordinates = new Point3D(W.X, W.Y, W.Z);


        }




        DoubleWishboneKinematicsSolver dwSolver = new DoubleWishboneKinematicsSolver();
        Angle dCaster_New;
        private double ComputeCasterError()
        {
            dCaster_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(tempAxisLines["SteeringAxis"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(tempAxisLines["SteeringAxis_Ref"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(tempAxisLines["LateralAxis_WheelCenter"]));

            Angle staticCaster = new Angle(OC[0].Caster, AngleUnit.Radians);

            Calc_Caster = new Angle(dCaster_New.Degrees + staticCaster.Degrees, AngleUnit.Degrees);

            CasterError = ((dCaster_New.Degrees + staticCaster.Degrees) - (Req_Caster.Degrees)) / Req_Caster.Degrees;

            Setup_OP.Caster_Conv = new Convergence(1 - SetConvergenceError(CasterError));

            Setup_OP.Caster = -Calc_Caster;

            return (CasterError);
        }

        Angle dKPI_new;
        private double ComputeKPIError()
        {
            dKPI_new = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(tempAxisLines["SteeringAxis"]),
                                                               Custom3DGeometry.GetMathNetVector3D(tempAxisLines["SteeringAxis_Ref"]),
                                                               Custom3DGeometry.GetMathNetVector3D(tempAxisLines["LongitudinalAxis_WheelCenter"]));

            Angle staticKPI = new Angle(OC[0].KPI, AngleUnit.Radians);

            Calc_KPI = new Angle(dKPI_new.Degrees + staticKPI.Degrees, AngleUnit.Degrees);

            KpiError = (((dKPI_new.Degrees + staticKPI.Degrees) - (Req_KPI.Degrees)) / (Req_KPI.Degrees));

            Setup_OP.KPI_Conv = new Convergence(1 - SetConvergenceError(KpiError));

            double tmepKPI = Calc_KPI.Degrees;

            SolverMasterClass.AssignDirection_KPI((int)VCorner, ref tmepKPI);

            Setup_OP.KPI = new Angle(tmepKPI, AngleUnit.Degrees);

            return KpiError;
        }

        Angle dToe_New;
        private double ComputeToeError()
        {
            dToe_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(tempAxisLines["WheelSpindle"]),
                                                                     Custom3DGeometry.GetMathNetVector3D(tempAxisLines["WheelSpindle_Ref"]),
                                                                     Custom3DGeometry.GetMathNetVector3D(tempAxisLines["VerticalAxis_WheelCenter"]));

            Angle staticToe = new Angle(WA.StaticToe, AngleUnit.Radians);

            Calc_Toe = new Angle(dToe_New.Degrees + staticToe.Degrees, AngleUnit.Degrees);

            ///<remarks>
            /// NOT ANYMORE
            ///---IMPORTANT--- FOR NOW TOE ERROR IS CALCUALTED AS ABSOLUTE ERROR AND NOT RELATIVE ERROR LIKE CASTER ABOVE
            /// </remarks>
            ToeError = (((dToe_New.Degrees + staticToe.Degrees) - (Req_Toe.Degrees)) / (Req_Toe.Degrees));

            Setup_OP.Toe_Conv = new Convergence(1 - SetConvergenceError(ToeError));

            double tempToe = Calc_Toe.Degrees;
            double tempCamber = 0;

            SolverMasterClass.AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, (int)VCorner);

            Setup_OP.Toe = new Angle(tempToe, AngleUnit.Degrees);

            return ToeError;

        }

        Angle dCamber_New;
        private double ComputeCamberError()
        {
            dCamber_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(tempAxisLines["WheelSpindle"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(tempAxisLines["WheelSpindle_Ref"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(tempAxisLines["LongitudinalAxis_WheelCenter"]));

            Angle staticCamber = new Angle(WA.StaticCamber, AngleUnit.Radians);

            Calc_Camber = new Angle(dCamber_New.Degrees + staticCamber.Degrees, AngleUnit.Degrees);

            CamberError = (((dCamber_New.Degrees + staticCamber.Degrees) - (Req_Camber.Degrees)) / (Req_Camber.Degrees));

            Setup_OP.Camber_Conv = new Convergence(1 - SetConvergenceError(CamberError));

            double tempToe = 0;
            double tempCamber = Calc_Camber.Degrees;

            SolverMasterClass.AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, (int)VCorner);

            Setup_OP.Camber = new Angle(tempCamber, AngleUnit.Degrees);

            return CamberError;
        }

        #region --Computing the Bump Steer Error--
        /// <summary>
        /// Method to which calls the <see cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/> Solver
        /// to compute the Bump Steer Values for the Wheel Deflection Range set by the User
        /// </summary>
        /// <param name="_xCoord">X Coordinate of the Inboard Toe Link Pick-Up Point</param>
        /// <param name="_yCoord">Y Coordinate of the Inboard Toe Link Pick-Up Point</param>
        /// <param name="_zCoord">x Coordinate of the Inboard Toe Link Pick-Up Point</param>
        /// <returns>Returns Error of the computed Bump Steer Curve with the Curve that the user wants</returns>
        private double ComputeBumpSteerError()
        {
            

            

            Point3D InboardPickUpPoint = new Point3D(ga_ToeLinkInboard.X, ga_ToeLinkInboard.Y, ga_ToeLinkInboard.Z);

            ///<summary>Assigning the Simulation Type in the <see cref="DoubleWishboneKinematicsSolver"/></summary>
            dwSolver.SimulationType = SimulationType.Optimization;
            ///<summary>Assignin the Simulation type in the <see cref="SolverMasterClass"/></summary>
            SolverMasterClass.SimType = SimulationType.Optimization;

            ///<summary>Assigning the Optimized Inboard Pick Up Point in the <see cref="DoubleWishboneKinematicsSolver"/></summary>
            dwSolver.OptimizedSteeringPoint = InboardPickUpPoint;

            ///<summary>Assigning the Wheel Spindle End Coordinate of the <see cref="DoubleWishboneKinematicsSolver"/> Class</summary>
            dwSolver.L1x = SCM.L1x; dwSolver.L1y = SCM.L1y; dwSolver.L1z = SCM.L1z;

            ///<summary>
            ///Invoking the <see cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
            ///Class to compute the Kinematics and calculate the Bump Steer at each interval of Wheel Deflection
            /// </summary>
            dwSolver.Kinematics(Identifier, SCM, WA, Tire, ARB, ARBRate_Nmm, Spring, Damper, OC_BumpSteer, Vehicle, CalculateWheelDeflections(SuspensionEvalStepSize, dwSolver), true, false);

            ///<summary>Invoking the <see cref="GetResultValues(List{OutputClass})"/> to extract the Bump Steer Data</summary>
            BumpSteerError = GetResultValues(OC_BumpSteer);

            ///<summary>Reassigning the Simulation type to Dummy so to prevent confusion if any other simulation is run after this one</summary>
            dwSolver.SimulationType = SimulationType.Dummy;

            SolverMasterClass.SimType = SimulationType.Dummy;

            return BumpSteerError;

        }

        /// <summary>
        /// Method to generate the Wheel Deflection curve based on the <see cref="SuspensionEvalStepSize"/> passed as an argument and the <see cref="SuspensionEvalIterations"/>
        /// </summary>
        /// <param name="_StepSize">Step Size </param>
        /// <param name="_dwSolver">Object of the <see cref="DoubleWishboneKinematicsSolver"/></param>
        /// <returns><see cref="List{T}"/> of Wheel Deflections which define a Wheel Deflection Curve</returns>
        private List<double> CalculateWheelDeflections(int _StepSize, DoubleWishboneKinematicsSolver _dwSolver)
        {
            _dwSolver.NoOfIterationsOptimization = (SuspensionEvalIterations * 2) + 1;

            List<double> WheelDefelctions = new List<double>();

            WheelDefelctions.Add(0);


            List<double> wheelDeflections0ToUpper = new List<double>();

            wheelDeflections0ToUpper.Add(_StepSize);

            for (int i = 0; i < SuspensionEvalIterations_UpperLimit - 1; i++)
            {
                wheelDeflections0ToUpper.Add(wheelDeflections0ToUpper[i] + _StepSize);
            }
            for (int i = 0; i < SuspensionEvalIterations_UpperLimit - 1; i++)
            {
                wheelDeflections0ToUpper.Add(wheelDeflections0ToUpper[SuspensionEvalIterations_UpperLimit - 1 - i] - _StepSize);
            }

            WheelDefelctions.AddRange(wheelDeflections0ToUpper.ToArray());





            List<double> WheelDeflections0ToLower = new List<double>();

            WheelDeflections0ToLower.Add(-_StepSize);

            for (int i = 0; i < SuspensionEvalIterations_LowerLimit - 1; i++)
            {
                WheelDeflections0ToLower.Add(WheelDeflections0ToLower[i] - _StepSize);
            }
            for (int i = 0; i < SuspensionEvalIterations_LowerLimit - 1; i++)
            {
                WheelDeflections0ToLower.Add(WheelDeflections0ToLower[SuspensionEvalIterations_LowerLimit - 1 - i] + _StepSize);
            }

            WheelDefelctions.AddRange(WheelDeflections0ToLower.ToArray());

            WheelDefelctions.Insert(SuspensionEvalIterations_UpperLimit * 2, 0);
            WheelDefelctions.Insert(WheelDefelctions.Count, 0);

            if (WheelDefelctions.Count != _dwSolver.NoOfIterationsOptimization)
            {
                int diff = _dwSolver.NoOfIterationsOptimization - WheelDefelctions.Count - 1;

                for (int i = 0; i < diff; i++)
                {
                    WheelDefelctions.Insert(WheelDefelctions.Count - 1, 1);
                }
            }

            return WheelDefelctions;
        }

        /// <summary>
        /// <para>Method to extract the Results
        /// <para>In this case the Toe Angle Varation per Step Size of Wheel Deflectin</para>
        /// </summary>
        /// <param name="_oc"> <see cref="List{T}"/> of <see cref="OutputClass"/> objects</param>
        /// <returns></returns>
        private double GetResultValues(List<OutputClass> _oc)
        {
            double averageBS = 1;

            double maxBumpSteer = 0;

            List<Angle> ToeAngles = new List<Angle>();

            List<Angle> ToeAngles_Bump = new List<Angle>();

            List<Angle> ToeAngle_Rebound = new List<Angle>();

            for (int i = 0; i < _oc.Count; i++)
            {
                ToeAngles.Add(new Angle(_oc[i].waOP.StaticToe, AngleUnit.Radians));
            }

            ToeAngles_Bump.AddRange(ToeAngles.GetRange(0, SuspensionEvalIterations_UpperLimit + 1));

            ToeAngle_Rebound.AddRange(ToeAngles.GetRange((SuspensionEvalIterations_UpperLimit * 2) + 1, SuspensionEvalIterations_LowerLimit));

            Req_BumpSteerGraph = new List<Angle>();

            Req_BumpSteerGraph.AddRange(ToeAngle_Rebound);

            Req_BumpSteerGraph.AddRange(ToeAngles_Bump);

            Req_BumpSteerGraph.Sort();

            Angle StaticToe = new Angle(WA.StaticToe, AngleUnit.Radians);

            ///<summary>Invoking the <see cref="EvaluateDeviation(List{Angle}, Angle)"/> method to compute the Eucledian Ditance between the User's Bump Steer Curve and the computed Bump Steer Curve. The output is the Error which needs to be minimized</summary>
            double Error = EvaluateDeviation(Req_BumpSteerGraph, StaticToe);

            return Error;

        }

        /// <summary>
        /// Method to compute the Eucledian Distance betweent he Points on the User's Bump Steer Curve and the computed Bump Steer Curve AND to find the Error which is later conditioned as the Fitness
        /// </summary>
        /// <param name="_toeAngle"><see cref="List{T}"/> of Toe Angles</param>
        /// <param name="_staticToe">The Static Toe Angle</param>
        /// <returns>Returns the error based on the Eucledian Distance</returns>
        private double EvaluateDeviation(List<Angle> _toeAngle, Angle _staticToe)
        {
            List<Angle> UserBumpSteerCurve = new List<Angle>();

            ///<summary>Generating an arbitrary Bump Steer Curve. This will be later on obtained from the user using a Chart</summary>
            for (int i = 0; i < SuspensionEvalIterations + 1; i++)
            {

                UserBumpSteerCurve.Add(new Angle(_staticToe.Degrees, AngleUnit.Degrees));

            }

            Req_BumpSteerGraph = UserBumpSteerCurve;

            //double reverse = 2.5;
            //for (int i = 0; i < SuspensionEvalIterations / 2; i++)
            //{
            //    UserBumpSteerCurve.Add(new Angle(-reverse + _staticToe.Degrees, AngleUnit.Degrees));
            //    reverse -= 0.1;
            //}

            //for (int i = 0; i < SuspensionEvalIterations / 2; i++)
            //{
            //    UserBumpSteerCurve.Add(new Angle(_staticToe.Degrees + (i * 0.1), AngleUnit.Degrees));
            //}
            //UserBumpSteerCurve.Insert(UserBumpSteerCurve.Count, new Angle(2, AngleUnit.Degrees));


            List<Angle> ErrorCalc_Step1 = new List<Angle>();

            ///<summary>Finding the distance between each pair of Points</summary>
            for (int i = 0; i < SuspensionEvalIterations + 1; i++)
            {
                if (i != SuspensionEvalIterations - 1)
                {

                    //ErrorCalc_Step1.Add(new Angle((_toeAngle[i].Degrees - UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));

                    if (UserBumpSteerCurve[i].Degrees != 0)
                    {
                        ErrorCalc_Step1.Add(new Angle(((_toeAngle[i].Degrees - UserBumpSteerCurve[i].Degrees) / UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));
                    }
                    else
                    {
                        ErrorCalc_Step1.Add(new Angle((_toeAngle[i].Degrees - UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));
                    }
                }
                else
                {
                    ErrorCalc_Step1.Add(ErrorCalc_Step1[i - 1]);
                }
            }

            List<Angle> ErrorCalc_Step2 = new List<Angle>();

            ///<summary>Finiding the square of the Distance</summary>
            for (int i = 0; i < SuspensionEvalIterations + 1; i++)
            {
                ErrorCalc_Step2.Add(new Angle(ErrorCalc_Step1[i].Degrees * ErrorCalc_Step1[i].Degrees, AngleUnit.Degrees));
            }

            double ErrorCalc_Step3 = 0;

            ///<summary>Summing the squares of the distances</summary>
            for (int i = 0; i < SuspensionEvalIterations + 1; i++)
            {
                ErrorCalc_Step3 += ErrorCalc_Step2[i].Degrees;
            }

            ///<summary>Computing the Final Error by finding the Square Root of the Squares of the distances and dividing it by the No. of Iterations</summary>
            double FinalError = System.Math.Sqrt(ErrorCalc_Step3) / SuspensionEvalIterations;

            Setup_OP.BumpSteer_Conv = new Convergence(1 - SetConvergenceError(FinalError));

            Setup_OP.BumpSteerChart = _toeAngle;

            return FinalError;
        }
        #endregion

        private double SetConvergenceError(double error)
        {

            if (error < 0) 
            {
                error *= -1;
            }

            if (error > 1)
            {
                error = 1 - error;
            }

            return System.Math.Round(error, 2);
        }

        private void EvaluateParetoOptimial()
        {
            //double[,] Fitness_MultipleObjective = ConstructParetoArray();

            //FindDominatingSolutions(Fitness_MultipleObjective);

            //FindScaledRanks();
        }

        private double[,] ConstructParetoArray()
        {
            double[,] multipleObjectiveFitness = new double[Ga_Values.Rows.Count, 2];

            for (int i = 0; i < Ga_Values.Rows.Count - 1; i++) 
            {
                multipleObjectiveFitness[i, 0] = Ga_Values.Rows[i].Field<double>("Orientation Fitness");
                multipleObjectiveFitness[i, 1] = Ga_Values.Rows[i].Field<double>("Bump Steer Fitness");
            }
            return multipleObjectiveFitness;
        }

        private void FindDominatingSolutions(double[,] _moopFitnesses)
        {

            for (int i = 0; i < Ga_Values.Rows.Count - 1; i++) 
            {
                bool dominatingSolution = true;
                int dominatedSolIndex = 0;
                for (int j = 0; j < Ga_Values.Rows.Count - 1; j++) 
                {
                    if ((_moopFitnesses[i, 0] < _moopFitnesses[j, 0] && _moopFitnesses[i, 1] < _moopFitnesses[j, 1]) && (_moopFitnesses[j, 0] != 0 && _moopFitnesses[j, 1] != 0)) 
                    {
                        if (i != j)
                        {
                            Ga_Values.Rows[j].SetField<double>("Pareto Rank", Ga_Values.Rows[j].Field<double>("Pareto Rank") + 1);
                        }
                    }
                    else if ((_moopFitnesses[i, 0] > _moopFitnesses[j, 0] && _moopFitnesses[i, 1] > _moopFitnesses[j, 1]) && (_moopFitnesses[j, 0] != 0 && _moopFitnesses[j, 1] != 0))
                    {
                        dominatingSolution = false;

                        break;
                    }
                }

                if (dominatingSolution && (_moopFitnesses[i, 0] != 0 && _moopFitnesses[i, 1] != 0)) 
                {
                    Ga_Values.Rows[i].SetField<bool>("Pareto Optimal", true);
                }

            }
        }

        private void FindScaledRanks()
        {
            int mostDominated = 0;

            for (int i = 0; i < Ga_Values.Rows.Count - 1; i++) 
            {
                double tempRank = Ga_Values.Rows[i].Field<double>("Pareto Rank");

                if (Convert.ToInt32(tempRank) > mostDominated) 
                {
                    mostDominated = Convert.ToInt32(tempRank);
                }
            }

            for (int i = 0; i < Ga_Values.Rows.Count - 1; i++) 
            {
                Ga_Values.Rows[i].SetField<double>("Pareto Rank", Ga_Values.Rows[i].Field<double>("Pareto Rank") / mostDominated);
            }

        }

        #endregion

    }

    #region Auxillary Enums and Classes
    enum WheelDeflectionType
    {
        Bump,
        Rebound
    };

    public enum OptimizationParameter
    {
        CasterKPILinkLength,
        Toe,
        CasterKPILinkLengthToe
    }

    public class OptimizedCoordinate
    {
        public string PointName;

        public Point3D OptimizedCoordinates;

        public Point3D NominalCoordinates;

        public Point3D UpperCoordinateLimit;

        public Point3D LowerCoordinateLimit;

        public double UpperLinkLengthLimit_Front;

        public double LowerLinkengthLimit_Front;

        public double UpperLinkLengthLimit_Rear;

        public double LowerLinkLengthLimit_Rear;

        //public int BitSize;

        public OptimizedCoordinate(Point3D _nominalCoord, Point3D _upperLimit, Point3D _lowerLimit/*, int _bitSize*/)
        {
            NominalCoordinates = _nominalCoord;

            OptimizedCoordinates = NominalCoordinates.Clone() as Point3D;

            UpperCoordinateLimit = _upperLimit;

            LowerCoordinateLimit = _lowerLimit;

            //BitSize = _bitSize;
        }
    }

    public class Opt_AdjToolParams
    {
        public string ParamName { get; set; }

        public double Nominal { get; set; }

        public double Uppwer { get; set; }

        public double Lower { get; set; }

        public int BitSize { get; set; }

        public double OptimizedIteration { get; set; }

        public Opt_AdjToolParams() { }

        public Opt_AdjToolParams(string _paramName, double _nominal, double _upper, double _lower, int _bitSiz)
        {
            ParamName = _paramName;

            Nominal = _nominal;

            Uppwer = _upper;

            Lower = _lower;

            BitSize = _bitSiz;
        }
    }

    #endregion







}

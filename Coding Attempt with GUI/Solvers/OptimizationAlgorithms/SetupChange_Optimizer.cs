using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
    public class SetupChange_Optimizer
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
        /// Variable indicating the number of Generations before Termination
        /// </summary>
        public int No_Generations { get; set; }

        /// <summary>
        /// Maximium Error in a particular Generation
        /// </summary>
        double MaximumErrorOfGeneration { get; set; }

        ///// <summary>
        ///// Dictionary of <see cref="OptimizedOrientation"/> into the which the Genetic Algorithm passes it's iteration vales
        ///// </summary>
        //Dictionary<string, OptimizedOrientation> GAOrientation { get; set; }

        /// <summary>
        /// Top Front Link Length proposed by the Optimizer
        /// </summary>
        public double ga_TopFront;

        /// <summary>
        /// Top Rear Link Length proposed by the Optimizer
        /// </summary>
        public double ga_TopRear;

        /// <summary>
        /// Bottom Front Link Length proposed by the Optimizer
        /// </summary>
        public double ga_BottomFront;

        /// <summary>
        /// Bottom Rear Link Length proposed by the Optimizer
        /// </summary>
        public double ga_BottomRear;

        /// <summary>
        /// Toe Link Length proposed by the Optimizer
        /// </summary>
        public double ga_ToeLink;

        /// <summary>
        /// Top Camber Shims Length proposed by the Optimizer
        /// </summary>
        public double ga_TopCamberShims;

        /// <summary>
        /// Bottom Camber Shims Length proposed by the Optimizer
        /// </summary>
        public double ga_BottomCamberShims;

        /// <summary>
        /// Toe Link Inboard 
        /// </summary>
        public Point3D ga_ToeLinkInboard;

        ///// <summary>
        ///// <para>DataTable which will hold all the information regarding the Optimization</para>
        ///// <para> Link Lengths | Coordiantes | All Setup Params Convergence values | All Setup Params actual values  </para>
        ///// </summary>
        //DataTable Ga_Values { get; set; }

        List<SetupChange_Outputs> List_Setup_OP;

        public List<SetupChange_Outputs> OptimizationResults;

        /// <summary>
        /// Variable indicating number of columns in the <see cref="Ga_Values"/>
        /// </summary>
        int Columns_DataTable;

        ///---Delete if not Needed---
        //List<object> GA_Values_Params;

        ///---DELETE IF NOT NEEDED - Used for Simmulated Annealing---
        //Point3D BestFit_CurrGen_ToeLinkInboard;

        ///---DELETE IF NOT NEEDED - Used for Simmulated Annealing---
        //double BestFitness_CurrGen;

        /// <summary>
        /// ---Obsolete--- Was used for Pareto
        /// </summary>
        Dictionary<string, double[,]> Fitness_Individual_Objectives;

        /// <summary>
        /// <para>Variable to keep track of which solution is currently being evaluated</para>
        /// <para>---IMPORTANT--- This variable is crucial as it also aids in populating the <see cref="Ga_Values"/> <see cref="DataTable"/> </para>
        /// </summary>
        int SolutionCounter { get; set; }

        int No_GaOutputs { get; set; }
        #endregion

        #region --DELETE.MOSTLY NOT NEEDED -FITNESS FUNCTION EVALUATION PARAMETERS--

        //--FITNESS FUNCTION EVALUATION PARAMETERS--


        //---IMPORTANT--- These are not Parameters of the Genetic Algorithm. These are parameters which the FITNESS FUCNTION will use to determine the Fitness. 
        /// <summary>
        /// Step Size of the Wheel Defelction.
        /// </summary>
        double SuspensionEvalStepSize;
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
        /// Object of the <see cref="DoubleWishboneKinematicsSolver"/> Class used for all the Kinematics computations
        /// </summary>
        DoubleWishboneKinematicsSolver dwSolver = new DoubleWishboneKinematicsSolver();

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
        Dictionary<string, SetupChange_OptimizedCoordinate> UnsprungAssembly;
        /// <summary>
        /// 
        /// </summary>
        //Dictionary<string, OptimizedCoordinate> tempInboardPoints;
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, Line> AxisLines;
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

        Dictionary<string, Dictionary<string, SetupChange_AdjToolParams>> MasterDictionary;

        Dictionary<String, double> Opt_AdjToolValues;


        #endregion

        #region ---Setup Change Params---
        /// <summary>
        /// Object of the <see cref="SetupChange_CornerVariables"/> pertaining to the corner which is calling this methhhod 
        /// </summary>
        public SetupChange_CornerVariables Setup_CV { get; set; }

        /// <summary>
        /// Object of the <see cref="SetupChange_Outputs"/> class
        /// </summary>
        public SetupChange_Outputs Setup_OP { get; set; }

        ///// <summary>
        ///// Requested Camber
        ///// </summary>
        //public Angle Req_Camber { get; set; }
        /// <summary>
        /// Calculated Camber after optimization
        /// </summary>
        public Angle Calc_Camber;
        /// <summary>
        /// Convergence status or Error of Camber
        /// </summary>
        public double CamberError { get; set; }

        ///// <summary>
        ///// Requested Caster
        ///// </summary>
        //public Angle Req_Caster { get; set; }
        /// <summary>
        /// Calculated Caster after optimization
        /// </summary>
        public Angle Calc_Caster;
        /// <summary>
        /// Convergence status or Error of Caster
        /// </summary>
        public double CasterError { get; set; }

        ///// <summary>
        ///// Requested KPI
        ///// </summary>
        //public Angle Req_KPI { get; set; }
        /// <summary>
        /// Calculated KPI after optimization
        /// </summary>
        public Angle Calc_KPI;
        /// <summary>
        /// Convergence status or Error of KPI
        /// </summary>
        public double KpiError { get; set; }

        ///// <summary>
        ///// Requested Toe
        ///// </summary>
        //public Angle Req_Toe { get; set; }
        /// <summary>
        /// Calculated Toe after optimization
        /// </summary>
        public Angle Calc_Toe;
        /// <summary>
        /// Convergence status or Error of Toe
        /// </summary>
        public double ToeError { get; set; }

        ///// <summary>
        ///// Requested Bump Steer Graph
        ///// </summary>
        //public List<Angle> Req_BumpSteerGraph { get; set; }
        /// <summary>
        /// Calculated Toe after Bump Steer Graph
        /// </summary>
        public List<Angle> Calc_BumpSteerGraph;
        /// <summary>
        /// Convergence status or Error of Bump Steer
        /// </summary>
        public double BumpSteerError { get; set; }

        /// <summary>
        /// ID of the Setup Change which is running the show 
        /// </summary>
        public int SetupID;

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
        /// <para>The Constructor of the <see cref="SetupChange_Optimizer"/> Class</para>
        /// </summary>
        /// <param name="_crossover">Percentage of the Population which is going to be subject to Crossover</param>
        /// <param name="_mutation">Percentage of the Population which is going to be subject to Random Mutation</param>
        /// <param name="_elites">Percentage of the population which is going to be treated as Elite without an modifications to the Genes</param>
        /// <param name="_popSize">Size of the Population</param>
        /// <param name="_chromoseLength"> <para>Chromosome Length of the <see cref="Population"/></para>
        /// <para>Decided based on 2 things: The number of Genes and the bit size required for each Gene</para>
        /// <para>https://gaframework.org/wiki/index.php/How_to_Encode_Parameters for more information</para> </param>
        public SetupChange_Optimizer(double _crossover, double _mutation, int _elites, int _noOfGenerations)
        {
            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;

            No_Generations = _noOfGenerations;

            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Fitness_Individual_Objectives = new Dictionary<string, double[,]>();

            //No_GaOutputs = _chromoseLength / BitSize;

            Opt_AdjToolValues = new Dictionary<string, double>();

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
        public void InitializeSetupParams(SetupChange_CornerVariables _reqChanges, SetupChange_Outputs _setupOP, Dictionary<string, Dictionary<string, SetupChange_AdjToolParams>> _masterD, Angle _fCamber, Angle _fCaster, Angle _fToe, Angle _fKPI, int _setupGUIID)
        {
            ///<summary>Assigning the <see cref="SetupChange_CornerVariables"/> object</summary>
            Setup_CV = _reqChanges;

            ///<summary>Passing the Master Dictionary which contains the Dictionaries (with Adjustmer Options) of all the Setup Changes requested</summary>
            MasterDictionary = _masterD;

            List_Setup_OP = new List<SetupChange_Outputs>();

            //---NEEDED---
            //InitializeDataTable(/*MasterDictionary.Keys.ToList()*/);

            ///<summary>Passing the <see cref="SetupChange_Outputs"/> object</summary>
            Setup_OP = _setupOP;

            ///<summary>Passing all the Requested Values of the Setup Change. If there is not value requested then the Required and Initial value of the Param is the same</summary>
            Setup_OP.Req_Camber = _fCamber;

            Setup_OP.Req_Caster = _fCaster;

            Setup_OP.Req_Toe = _fToe;

            Setup_OP.Req_KPI = _fKPI;

            SetupID = _setupGUIID;
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
            SuspensionEvalIterations = Convert.ToInt32(Setup_CV.BS_Params.WheelDeflections.Count / Setup_CV.BS_Params.StepSize);

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

            OC_BumpSteer = VehicleParamsAssigner.AssignVehicleParams_Custom_OC_BumpSteer(SCM, _vCorner, _vehicle, /*(SuspensionEvalIterations * 2) + 1*/ Setup_CV.BS_Params.WheelDeflections.Count);

            Identifier = (int)tempVehicleParams["Identifier"];

            PopulateDictionary();

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
            ///<summary>Initializing all the delegates</summary>
            Del_KPI_Error = new ParamsToEvaluate(ComputeKPIError);

            Del_Caster_Error = new ParamsToEvaluate(ComputeCasterError);

            Del_Camber_Error = new ParamsToEvaluate(ComputeCamberError);

            Del_Toe_Error = new ParamsToEvaluate(ComputeToeError);

            Del_BumpSteer_Error = new ParamsToEvaluate(ComputeBumpSteerError);

            Del_RMS_Error = new ParamsToEvaluate(Dummy);

            ///<summary>
            ///Populating the Main RMS delegate with the child delegates
            ///---Important---
            ///Since I need to monitor the changes of the params even if they are not requested, I need to hence call the error function to monitor the change in each of the parameters. 
            ///If a particular Setup is not requested then it's error is simply not computed
            /// </summary>
            Del_RMS_Error += Del_KPI_Error;

            Del_RMS_Error += Del_Caster_Error;

            Del_RMS_Error += Del_Camber_Error;

            Del_RMS_Error += Del_Toe_Error;

            ///<summary>Since Bump Steer Computation takes a lot of time I have decided to call its error function only if the user wants to change or moinitor it </summary>
            if (Setup_CV.monitorBumpSteer || Setup_CV.BumpSteerChangeRequested)
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
                ///<summary>
                ///The IF statement is to ensure that the if a Bump Steer Change or Monitor Bump Steer is issued then the Wheel Deflection is not null and not 0
                ///So basically this is to ensure that the user ( after selecting Change/Monitor Bump Steer also either clicks the Min BumpSteer Option or creates a curve)
                /// </summary>
                /// 
                if ((Setup_CV.BumpSteerChangeRequested || Setup_CV.monitorBumpSteer) && ((Setup_CV.BS_Params.WheelDeflections == null) || (Setup_CV.BS_Params.WheelDeflections.Count == 0)))
                {
                    MessageBox.Show("Bump Steer Curve Not Initialized");
                }
                else
                {
                    GA.Run(Terminate);
                }
                //if ((Setup_CV.BumpSteerChangeRequested || Setup_CV.monitorBumpSteer) && (Setup_CV.BS_Params.WheelDeflections != null) && (Setup_CV.BS_Params.WheelDeflections.Count != 0))  
                //{
                //    GA.Run(Terminate); 
                //}
                //else
                //{
                //    MessageBox.Show("Bump Steer Curve Not Initialized");

                //}
                
            }
            else
            {
                ///<summary>In case Setup Change isn't requested for a corner then this method is called to update the progress bar</summary>
                SetupChange_GUI.List_SetupChangeGUI[SetupID].SetProgressValue((int)VCorner * No_Generations);

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
                ExtractOptimizedValues(chromosome);
                //AddRowToTable("Solution" + SolutionCounter);

                ///<summary>Invoking the <see cref="ComputeBumpSteerError(double, double, double)"/> method to check the error of the calcualted Bump Steer Curve with the Curve that the user wants</summary>
                double resultError = EvaluateRMSError();

                ///<summary>Calculating the Fitness based on the Error</summary>
                Fitness = 1 - (resultError);

                List_Setup_OP.Add(Setup_OP.Clone());

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
            ///<summary>If the terminat conditions in the <see cref="Terminate"/> method are NOT met then it means anoher generation will procceed so the <see cref="List_Setup_OP"/> is cleared to hold the fresh values</summary>
            if (!TerminateAlgorithm(e.Population, e.Generation, e.Evaluations))
            {
                List_Setup_OP.Clear();
            }
            else
            {
                ///<summary>If the terminate condition is met then it means the last generation is this one and hence the <see cref="List_Setup_OP"/> is final and only now needs to be sorted</summary>
                OptimizationResults = new List<SetupChange_Outputs>();
                OptimizationResults = List_Setup_OP.OrderBy(rms => Convert.ToDouble(rms.Total_Conv.ConvergenceStatus)).ToList();
            }

            ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
            var chromosome = e.Population.GetTop(1)[0];


            ///<summary>
            ///Invokinig the <see cref="ExtractOptimizedValues(Chromosome, out double, out double, out double)"/> which extracts the coordinates 
            ///of the BEST FIT of this Generation
            /// </summary>
            ExtractOptimizedValues(chromosome);

            ///<summary>Extracting the Max Fitness</summary>
            double Fitness = e.Population.MaximumFitness;

            ///<summary>Getting information regarding the Current Generation and the number of Evaluations perfomed</summary>
            int Generations = e.Generation;

            long Evaluations = e.Evaluations;

            #region Simmulated Annealing & Pareto Optimal Calls. Not used as of now 
            Anneal(Fitness);

            //EvaluateParetoOptimial(); 
            #endregion

            double resultError = EvaluateRMSError();

            ///<summary>Updating the Progress bar </summary>
            SetupChange_GUI.List_SetupChangeGUI[SetupID].UpdateProgressForm(Setup_OP.Caster_Conv, Setup_OP.KPI_Conv, Setup_OP.Camber_Conv, Setup_OP.Toe_Conv, Setup_OP.BumpSteer_Conv, Setup_OP.Total_Conv);

            ///--Pareto Soltuion Method Call. Not used now 
            //GetParetoSolutions();

            ///<summary>Setting the SolutionCounter to 0 so that in the next Generation it can be re-used in conjunction with the <see cref="Ga_Values"/></summary>
            SolutionCounter = 0;


        }

        /// <summary>
        /// Event triggered when the Optimization is complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GA_OnRunComplete(object sender, GaEventArgs e)
        {
            ///<summary>If the progress value is not the (Number of Iterations / 4 ) then this method pushes it to that value </summary>
            SetupChange_GUI.List_SetupChangeGUI[SetupID].SetProgressValue((int)VCorner * No_Generations);
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
            if (_currGeneration > No_Generations || _population.MaximumFitness > 0.999)
            {
                ///<summary>Extracting the BEST <see cref="Chromosome"/> from the <see cref="Population"/></summary>
                var chromosome = _population.GetTop(1)[0];

                ///<summary>
                ///Invokinig the <see cref="ExtractOptimizedValues(Chromosome, out double, out double, out double)"/> which extracts the coordinates 
                /// of the BEST FIT of this Generation
                /// </summary>
                ExtractOptimizedValues(chromosome);

                ///<summary>Extracting the Max Fitness</summary>
                double Fitness = _population.MaximumFitness;

                //double resultError = EvaluateRMSError();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ---METHOD TO POPULATE THE DICTIONARIES LOCALLY---  

        private void PopulateDictionary()
        {
            Point3D Upper = new Point3D(20, 20, 20);

            Point3D Lower = new Point3D(-20, -20, -20);


            UnsprungAssembly = new Dictionary<string, SetupChange_OptimizedCoordinate>();

            ToeLinkInboard = new Point3D(SCM.N1x, SCM.N1y, SCM.N1z);
            ga_ToeLinkInboard = ToeLinkInboard.Clone() as Point3D;

            TopFront = new Point3D(SCM.A1x, SCM.A1y, SCM.A1z);

            TopRear = new Point3D(SCM.B1x, SCM.B1y, SCM.B1z);

            BottomFront = new Point3D(SCM.D1x, SCM.D1y, SCM.D1z);

            BottomRear = new Point3D(SCM.C1x, SCM.C1y, SCM.C1z);


            UBJ = new Point3D(SCM.F1x, SCM.F1y, SCM.F1z);

            UnsprungAssembly.Add("UBJ", new SetupChange_OptimizedCoordinate(UBJ, Upper, Lower));

            UnsprungAssembly.Add("TopCamberMount", new SetupChange_OptimizedCoordinate(UBJ.Clone() as Point3D, Upper, Lower));

            Pushrod = new Point3D(SCM.G1x, SCM.G1y, SCM.G1z);

            UnsprungAssembly.Add("Pushrod", new SetupChange_OptimizedCoordinate(Pushrod, Upper, Lower));

            PushrodShockMount = new Point3D(SCM.H1x, SCM.H1y, SCM.H1z);

            LBJ = new Point3D(SCM.E1x, SCM.E1y, SCM.E1z);

            UnsprungAssembly.Add("BottomCamberMount", new SetupChange_OptimizedCoordinate(LBJ.Clone() as Point3D, Upper, Lower));

            UnsprungAssembly.Add("LBJ", new SetupChange_OptimizedCoordinate(LBJ, Upper, Lower));

            WcStart = new Point3D(SCM.K1x, SCM.K1y, SCM.K1z);

            UnsprungAssembly.Add("WcStart", new SetupChange_OptimizedCoordinate(WcStart, Upper, Lower));

            WcEnd = new Point3D(SCM.L1x, SCM.L1y, SCM.L1z);

            UnsprungAssembly.Add("WcEnd", new SetupChange_OptimizedCoordinate(WcEnd, Upper, Lower));

            ToeLinkOutboard = new Point3D(SCM.M1x, SCM.M1y, SCM.M1z);

            UnsprungAssembly.Add("ToeLinkOutboard", new SetupChange_OptimizedCoordinate(ToeLinkOutboard, Upper, Lower));

            ContactPatch = new Point3D(SCM.W1x, SCM.W1x, SCM.W1z);

            UnsprungAssembly.Add("ContactPatch", new SetupChange_OptimizedCoordinate(ContactPatch, Upper, Lower));


            AxisLines = new Dictionary<string, Line>();

            AxisLines.Add("SteeringAxis", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

            AxisLines.Add("SteeringAxis_Ref", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

            AxisLines.Add("LateralAxis_WheelCenter", new Line(WcStart.Clone() as Point3D, new Point3D(WcStart.X + 100, WcStart.Y, WcStart.Z)));

            AxisLines.Add("WheelSpindle", new Line(WcStart.Clone() as Point3D, WcEnd.Clone() as Point3D));

            AxisLines.Add("WheelSpindle_Ref", new Line(WcStart.Clone() as Point3D, WcEnd.Clone() as Point3D));

            AxisLines.Add("VerticalAxis_WheelCenter", new Line(WcStart.Clone() as Point3D, new Point3D(WcStart.X, WcStart.Y + 100, WcStart.Z)));

            AxisLines.Add("LongitudinalAxis_WheelCenter", new Line(WcStart.Clone() as Point3D, new Point3D(WcStart.X, WcStart.Y, WcStart.Z + 100)));

        }
        //---TEMP--
        private void PopulateDictionaryTrial_2()
        {




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

        /// <summary>
        /// Method to extract the Chromosome Length baed on the BIT SIZE of each adjuster in the <see cref="MasterDictionary"/>
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method to extract the index of the Row in the <see cref="Ga_Values"/> <see cref="DataTable"/> which has the most Fitness
        /// </summary>
        /// <returns></returns>
        private int GetMaxIndex()
        {
            int maxRowIndex = 0;

            double rmsFitness = 0;

            ///--Use List of <see cref="List{SetupChange_Outputs}"/> instead of the Data Table

            //for (int i = 0; i < Ga_Values.Rows.Count - 1; i++)
            //{
            //    if (Ga_Values.Rows[i].Field<double>("RMS Fitness") > rmsFitness)
            //    {
            //        rmsFitness = Ga_Values.Rows[i].Field<double>("RMS Fitness");
            //        maxRowIndex = i;
            //    }
            //}

            return maxRowIndex;
        }

        #region --Useful but not currenly employed -- Method to perform Simulated Annealing
        /// <summary>
        /// ---Useful but not currently employed---
        /// Method to perform Simulated Annealing by reducing the Range of the parameters being optimized depending upon their fitness
        /// </summary>
        /// <param name="Fitness"></param>
        private void Anneal(double Fitness)
        {
            if (Fitness > 0.994 && Fitness < 0.998 && Fitness < 0.999)
            {
                //if (Fitness > BestFitness_CurrGen)
                //{
                //    BestFitness_CurrGen = Fitness;

                //    ModfiyStepSize(0.5, 20, 8);
                //}

            }
            else if (Fitness > 0.998 && Fitness < 0.999)
            {

                //if (Fitness > BestFitness_CurrGen)
                //{
                //    BestFitness_CurrGen = Fitness;

                //    ModfiyStepSize(0.3, 10, 4);
                //}

            }
            else if (Fitness > 0.999 && Fitness < 0.9999)
            {

                //if (Fitness > BestFitness_CurrGen)
                //{
                //    BestFitness_CurrGen = Fitness;

                //    ModfiyStepSize(0.15, 5, 2);
                //}

            }
            else if (Fitness > 0.9999)
            {
                //if (Fitness > BestFitness_CurrGen)
                //{
                //    BestFitness_CurrGen = Fitness;

                //    ModfiyStepSize(0.05, 2, 1);
                //}
            }
        }

        /// <summary>
        /// Method to update the Lower and Upper Limit of the of Params by passing a reduced Range to close in on the best solution
        /// </summary>
        /// <param name="_range">New reduced range of the Parameter</param>
        /// <param name="_coordinateRange">New Recued range of the Coordinate in case Coordinate is being Optimized</param>
        /// <param name="_linkLengthRange">New Recued range of the Link Length in case Coordinate is being Optimized</param>
        private void ModfiyStepSize(double _range, double _coordinateRange, double _linkLengthRange)
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
        #endregion


        #region -Extraction Methods-
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

                    //Ga_Values.Rows[SolutionCounter].SetField<double>(adjTool, param);

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

        /// <summary>
        /// Method to extract the data from the <see cref="MasterDictionary"/>
        /// </summary>
        /// <param name="_extractedValues"></param>
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
                ga_ToeLink = Opt_AdjToolValues[AdjustmentTools.ToeLinkLength.ToString()];

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
        #endregion

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

        #region -Main RMS Error Function-
        /// <summary>
        /// Main Error Calculating fucntion. 
        /// Computes the error of all the Setup Params which have been requested (and hence have their delegates initialized)
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private double EvaluateRMSError()
        {
            ///<summary>
            ///--Cricual Step---
            ///First and foremost, solving for the Suspension Coordinates. 
            ///This step computes the Suspension Coorinates with the Link Length Changes accounted for. 
            ///If there ia no Link Length Change then the result will be the same coordinates
            /// </summary>
            SolveKinematics();

            ///<summary>Updating the Local <see cref="Dictionary{TKey, TValue}"/> of Suspension Coordinates called the <see cref="UnsprungAssembly"/></summary>
            Update_SuspensionCoordinateData();

            ///<summary>Initializing the errors to prevent residue error</summary>
            BumpSteerError = CasterError = ToeError = CamberError = KpiError = 0;
            double rmsError = 0;

            ///<summary>Invoking the delegate to call all the error functions in it's Invocation List</summary>
            Del_RMS_Error();

            ///<summary>Computing the RMS Error</summary>
            rmsError = ComputeRMSError();
            //EvaluateWishboneConstraints();

            Setup_OP.Total_Conv = new Convergence(1 - rmsError);

            if (rmsError > 1)
            {
                Setup_OP.Total_Conv = new Convergence(1 - rmsError);

                return 0.99;
            }
            else if (rmsError < 0)
            {
                Setup_OP.Total_Conv = new Convergence(1 - rmsError);

                return 0.99;
            }


            else return rmsError;
        }

        /// <summary>
        /// This method selectively adds up the errors (in RMS fashion) ONLY if they are requested. 
        /// </summary>
        private double ComputeRMSError()
        {
            double rms_1 = 0;

            double rmsFinal;

            ///<summary>Summing the RMS Erro with the child errors based on whether the Setup Change Exists</summary>
            if (Setup_CV.constKPI == true || Setup_CV.KPIChangeRequested)
            {
                rms_1 += System.Math.Pow(KpiError, 2);
            }
            if (Setup_CV.constCaster == true || Setup_CV.CasterChangeRequested)
            {
                rms_1 += System.Math.Pow(CasterError, 2);
            }
            if (Setup_CV.constCamber == true || Setup_CV.CamberChangeRequested)
            {
                rms_1 += System.Math.Pow(CamberError, 2);
            }
            if (Setup_CV.constToe == true || Setup_CV.ToeChangeRequested)
            {
                rms_1 += System.Math.Pow(ToeError, 2);
            }
            if (Setup_CV.monitorBumpSteer || Setup_CV.BumpSteerChangeRequested)
            {
                rms_1 += System.Math.Pow(BumpSteerError, 2);
            }

            rmsFinal = System.Math.Sqrt(rms_1);

            //System.Math.Sqrt((System.Math.Pow(BumpSteerError, 2) + System.Math.Pow(CasterError, 2) + System.Math.Pow(ToeError, 2) + System.Math.Pow(CamberError, 2) + System.Math.Pow(KpiError, 2)));

            return rmsFinal;
        } 
        #endregion

        #region Not Needed BUT will sever as good validation tools 
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
        #endregion

        #region -Kinematic Solver and Helper Methods-
        /// <summary>
        /// Method to Solve the Suspension Kienmatics after a Link Length change has been proposed by the Optimizer
        /// </summary>
        private void SolveKinematics()
        {
            ///<summary>Assigning the Suspension Coordinates to the local coordinate variables of the <see cref="DoubleWishboneKinematicsSolver"/> Class</summary>
            dwSolver.AssignLocalCoordinateVariables_FixesPoints(SCM_Clone);
            dwSolver.AssignLocalCoordinateVariables_MovingPoints(SCM_Clone);
            dwSolver.L1x = SCM_Clone.L1x; dwSolver.L1y = SCM_Clone.L1y; dwSolver.L1z = SCM_Clone.L1z;
            ///<summary>Assigning the Inboard Toe Link Point proposed by the Optimizer</summary>
            dwSolver.OptimizedSteeringPoint = ga_ToeLinkInboard;
            dwSolver.AssignOptimizedSteeringPoints();

            ///<summary>Using a temproary <see cref="OutputClass"/> object for the Solver</summary>
            OutputClass tempOC = new OutputClass();

            ///<summary>
            /// ---Points F & E (UBJ and LBJ) computed with the new Link Lengths which have been proposed by the Optimizer. 
            /// </summary>
            dwSolver.SC_Optimization_SteeringAxis(ga_TopFront, ga_TopRear, ga_BottomFront, ga_BottomRear, Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D F, out MathNet.Spatial.Euclidean.Point3D E);

            ///<summary>
            ///---Point G (Pushrod)
            /// </summary>
            dwSolver.SC_Optimization_Pushrod(Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D G);

            ///<summary>
            ///Point M (Toe Link) computed with the new Toe Link Inboard (N) point proposed by the Optimize
            ///</summary>
            dwSolver.SC_Optimization_ToeLink(ga_ToeLink, Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D M);

            ///<summary>
            ///Point TCM (Camber Mount Top) computed with the new Shim Vector Length proposd by the Optimizer
            /// </summary>
            dwSolver.SC_Optimization_CamberMountTop(ga_TopCamberShims, Vehicle, tempOC, out MathNet.Spatial.Euclidean.Point3D TCM);

            ///<summary>
            ///Point BCM (Camber Mount Bottom) computed with the new Shim Vector Length proposd by the Optimizer
            /// </summary>
            dwSolver.SC_Optimization_CamberMountBottom(ga_BottomCamberShims, Vehicle, tempOC, out MathNet.Spatial.Euclidean.Point3D BCM);

            ///<summary>
            ///Point K (Wheel Spindle Start)
            /// </summary>
            dwSolver.SC_Optimization_WheelSpindleStart(Vehicle, tempOC, AdjustmentTools.TopCamberMount, out MathNet.Spatial.Euclidean.Point3D K);

            ///<summary>
            ///Point L (Wheel Spindle End)
            /// </summary>
            dwSolver.SC_Optimization_WheelSpindleEnd(Vehicle, tempOC, AdjustmentTools.TopCamberMount, out MathNet.Spatial.Euclidean.Point3D L);

            ///<summary>
            ///Point W (Contact Patch)
            /// </summary>
            dwSolver.SC_Optimization_ContatcPatch(Vehicle, Identifier, tempOC, out MathNet.Spatial.Euclidean.Point3D W);

            ///<summary>Assigning all the computed points to a the <see cref="UnsprungAssembly"/> dictionary</summary>
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

        /// <summary>
        /// Method to update all the <see cref="SCM"/> Suspnsion coordinate Objects
        /// </summary>
        private void Update_SuspensionCoordinateData()
        {
            ///<summary>Generating a simple <see cref="Dictionary{String, Point3D}"/> to represent the Suspension Coordinates </summary>
            Dictionary<string, Point3D> SuspensionCoordintes = ConvertTo_PointDictionary();

            ///<summary>Cloning the coordinates using the dictionary</summary>
            SCM.Clone_Outboard_FromDictionary(SuspensionCoordintes);

            ///<summary>Updating the Axis lines</summary>
            AxisLines["SteeringAxis"] = new Line(UnsprungAssembly["UBJ"].OptimizedCoordinates, UnsprungAssembly["LBJ"].OptimizedCoordinates);

            AxisLines["WheelSpindle"] = new Line(UnsprungAssembly["WcStart"].OptimizedCoordinates, UnsprungAssembly["WcEnd"].OptimizedCoordinates);

        }

        /// <summary>
        /// Method to convert the <see cref="UnsprungAssembly"/> dictionary items (which is a <see cref="Dictionary{String, OptimizedCoordinate}"/> dictionary ) to a regular <see cref="Dictionary{String, Point3D}"/>
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Point3D> ConvertTo_PointDictionary()
        {
            Dictionary<string, Point3D> suspCoordinates = new Dictionary<string, Point3D>();

            foreach (string point in UnsprungAssembly.Keys)
            {
                suspCoordinates.Add(point, UnsprungAssembly[point].OptimizedCoordinates.Clone() as Point3D);
            }

            return suspCoordinates;
        }
        #endregion

        #region ---Computing the Caster Error---
        /// <summary>
        /// Newly computed DELTA Caster Angle after all the Kinematics Computations have been made
        /// </summary>
        Angle dCaster_New;

        /// <summary>
        /// Method to compute the Caster Error
        /// </summary>
        /// <returns>Caster Error</returns>
        private double ComputeCasterError()
        {
            ///<summary>Computing the DELTA or chage in caster with respect to the Initial Caster</summary>
            dCaster_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(AxisLines["SteeringAxis"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(AxisLines["SteeringAxis_Ref"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(AxisLines["LateralAxis_WheelCenter"]));

            ///<summary> Getting the Initial Caster </summary>
            Angle staticCaster = new Angle(OC[0].Caster, AngleUnit.Radians);

            ///<summary>Calcualating the new Castet</summary>
            Calc_Caster = new Angle(dCaster_New.Degrees + staticCaster.Degrees, AngleUnit.Degrees);

            ///<summary>Computing the Caster Error</summary>
            CasterError = ((dCaster_New.Degrees + staticCaster.Degrees) - (Setup_OP.Req_Caster.Degrees)) / Setup_OP.Req_Caster.Degrees;

            if (Setup_CV.constCaster || Setup_CV.CasterChangeRequested) 
            {
                ///<summary>Setting the Caster COnvergence</summary>
                Setup_OP.Caster_Conv = new Convergence(1 - SetConvergenceError(CasterError));
            }

            /////<summary>Populating the <see cref="Ga_Values"/> DataTable</summary>
            //Ga_Values.Rows[SolutionCounter].SetField<double>("Caster", 1 - SetConvergenceError(CasterError));

            ///<summary>
            /// Assigning Direction to the Caster Angle
            /// This needs to be done because for the user Positve Caster is CW rotation of Steering Axis
            /// AND for us CW rotation of steering axis means Negative 
            /// </summary>
            Setup_OP.Calc_Caster = -Calc_Caster;

            return (CasterError);
        }
        #endregion

        #region ---Computing the KPI Error---
        /// <summary>
        /// Newly computed DELTA KPI Angle after all the Kinematics Computations have been made
        /// </summary>
        Angle dKPI_new;

        /// <summary>
        /// Method to compute the KPI Error
        /// </summary>
        /// <returns>KPI Errpr</returns>
        private double ComputeKPIError()
        {
            ///<summary>Computing the DELTA or chage in KPI with respect to the Initial KPI</summary>
            dKPI_new = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(AxisLines["SteeringAxis"]),
                                                               Custom3DGeometry.GetMathNetVector3D(AxisLines["SteeringAxis_Ref"]),
                                                               Custom3DGeometry.GetMathNetVector3D(AxisLines["LongitudinalAxis_WheelCenter"]));

            ///<summary>Getting the Inital KPI</summary>
            Angle staticKPI = new Angle(OC[0].KPI, AngleUnit.Radians);

            ///<summary>Calculating the new KPI</summary>
            Calc_KPI = new Angle(dKPI_new.Degrees + staticKPI.Degrees, AngleUnit.Degrees);

            ///<summary>Calclating the KPI Error</summary>
            KpiError = (((dKPI_new.Degrees + staticKPI.Degrees) - (Setup_OP.Req_KPI.Degrees)) / (Setup_OP.Req_KPI.Degrees));

            if (Setup_CV.KPIChangeRequested || Setup_CV.constKPI) 
            {
                ///<summary>Setting the KPI Convergence</summary>
                Setup_OP.KPI_Conv = new Convergence(1 - SetConvergenceError(KpiError)); 
            }

            double tmepKPI = Calc_KPI.Degrees;

            ///<summary>Setting the direction of the Caster in the USER"S Perspective</summary>
            SolverMasterClass.AssignDirection_KPI((int)VCorner, ref tmepKPI);

            Setup_OP.Calc_KPI = new Angle(tmepKPI, AngleUnit.Degrees);

            //Ga_Values.Rows[SolutionCounter].SetField<double>("KPI", 1 - SetConvergenceError(KpiError));

            return KpiError;
        }
        #endregion

        #region ---Computing the Toe Error---

        /// <summary>
        /// Change in Toe Angle or DELTA Toe Angle
        /// </summary>
        Angle dToe_New;

        /// <summary>
        /// Method to compute the error in Toe
        /// </summary>
        /// <returns>Toe Error</returns>
        private double ComputeToeError()
        {
            ///<summary>Computing the change or Delta off Toe angle with respect to rhe initial position of the Wheel Spindle</summary>
            dToe_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(AxisLines["WheelSpindle"]),
                                                                     Custom3DGeometry.GetMathNetVector3D(AxisLines["WheelSpindle_Ref"]),
                                                                     Custom3DGeometry.GetMathNetVector3D(AxisLines["VerticalAxis_WheelCenter"]));

            ///<summary>Setting the Static Toe </summary>
            Angle staticToe = new Angle(WA.StaticToe, AngleUnit.Radians);

            ///<summary>Computing the new Toe Angle</summary>
            Calc_Toe = new Angle(dToe_New.Degrees + staticToe.Degrees, AngleUnit.Degrees);

            ///<remarks>
            /// NOT ANYMORE
            ///---IMPORTANT--- FOR NOW TOE ERROR IS CALCUALTED AS ABSOLUTE ERROR AND NOT RELATIVE ERROR LIKE CASTER ABOVE
            /// </remarks>
            ToeError = (((dToe_New.Degrees + staticToe.Degrees) - (Setup_OP.Req_Toe.Degrees)) / (Setup_OP.Req_Toe.Degrees));

            if (Setup_CV.constToe || Setup_CV.ToeChangeRequested) 
            {
                ///<summary>Setting the Toe Convergence</summary>
                Setup_OP.Toe_Conv = new Convergence(1 - SetConvergenceError(ToeError)); 
            }

            double tempToe = Calc_Toe.Degrees;
            double tempCamber = 0;

            ///<summary>Setting direction to the Toe Angle based on the USER"S PERSPECTIVE</summary>
            SolverMasterClass.AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, (int)VCorner);

            Setup_OP.Calc_Toe = new Angle(tempToe, AngleUnit.Degrees);

            return ToeError;

        }
        #endregion

        #region ---Computing the Camber Error
        /// <summary>
        /// Delta or Change in Camber
        /// </summary>
        Angle dCamber_New;

        /// <summary>
        /// Method to comute the Camber Error
        /// </summary>
        /// <returns>Camber Error</returns>
        private double ComputeCamberError()
        {
            ///<summary>Computing the change in Camber with respect to the inital Wheel Spindle</summary>
            dCamber_New = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(AxisLines["WheelSpindle"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(AxisLines["WheelSpindle_Ref"]),
                                                                  Custom3DGeometry.GetMathNetVector3D(AxisLines["LongitudinalAxis_WheelCenter"]));

            ///<summary>Getting the static value of Camber</summary>
            Angle staticCamber = new Angle(WA.StaticCamber, AngleUnit.Radians);

            ///<summary>Computing the new Camber</summary>
            Calc_Camber = new Angle(dCamber_New.Degrees + staticCamber.Degrees, AngleUnit.Degrees);

            ///<summary>Computing the Camber Error</summary>
            CamberError = (((dCamber_New.Degrees + staticCamber.Degrees) - (Setup_OP.Req_Camber.Degrees)) / (Setup_OP.Req_Camber.Degrees));

            if (Setup_CV.CamberChangeRequested || Setup_CV.constCamber) 
            {
                ///<summary>Setting the Camber Convergence </summary>
                Setup_OP.Camber_Conv = new Convergence(1 - SetConvergenceError(CamberError)); 
            }

            double tempToe = 0;
            double tempCamber = Calc_Camber.Degrees;

            ///<summary>Setting direction to the Camber in User's Perspective</summary>
            SolverMasterClass.AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, (int)VCorner);

            Setup_OP.Calc_Camber = new Angle(tempCamber, AngleUnit.Degrees);

            return CamberError;
        }
        #endregion

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

            ///<summary>Setting the number of iterations to be performed in the <see cref="DoubleWishboneKinematicsSolver"/></summary>
            dwSolver.NoOfIterationsOptimization = OC_BumpSteer.Count;

            ///<summary>
            ///---BUMP---
            ///Invoking the <see cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
            ///Class to compute the Kinematics and calculate the Bump Steer at each interval of Wheel Deflection 
            /// </summary>
            dwSolver.Kinematics(Identifier, SCM, WA, Tire, ARB, ARBRate_Nmm, Spring, Damper, OC_BumpSteer, Vehicle, Setup_CV.BS_Params.WheelDeflections, true, false);

            ///<summary>Extractingg the Toe Angles Computed in the above Solver Pass for Bump</summary>
            ExtractToeAngles(OC_BumpSteer);

            ///<summary>Extracting the Bump Steer Error based on the Euclidean Distance </summary>
            BumpSteerError = GetResultValues();

            ///<summary>Reassigning the Simulation type to Dummy so to prevent confusion if any other simulation is run after this one</summary>
            dwSolver.SimulationType = SimulationType.Dummy;

            SolverMasterClass.SimType = SimulationType.Dummy;

            return BumpSteerError;

        }

        /// <summary>
        /// Method to Extract the Toe Angles from the <see cref="OutputClass"/> and collate it into the <see cref="Calc_BumpSteerGraph"/> list to be compared 
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
                if (i >= Setup_CV.BS_Params.HighestBumpindex)
                {
                    Calc_BumpSteerGraph.Add(temp[i]);
                }
            }

            Setup_OP.Calc_BumpSteerChart = Calc_BumpSteerGraph.ToList();

        }

        /// <summary>
        /// <para>Method to extract the Results
        /// <para>In this case the Toe Angle Varation per Step Size of Wheel Deflectin</para>
        /// </summary>
        /// <paramref name="_wdtype"/>
        /// <returns></returns>
        private double GetResultValues()
        {
            double Error;

            ///<summary>Assigning the Static Toe</summary>
            Angle StaticToe = new Angle(WA.StaticToe, AngleUnit.Radians);

            ///<summary>Copying the User's Required Toe Angle Variation into the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/> so that it can be coupled with the Static Toe </summary>
            Setup_OP.Req_BumpSteerChart = new List<Angle>();

            ///<summary>Initializing the User's Required Toe Angle Variation by summing the <see cref="CustomBumpSteerParams.ToeAngles"/> with the Static Toe </summary>
            ///<remarks>
            ///---IMPORTANT---
            ///Notice that I have done Setup_CV.BS_Params.ToeAngles.Count - 1 instead of Setup_CV.BS_Params.ToeAngles.Count
            ///This is because while generting the Bump Steer Graph I added an extra element to the end of the List
            ///This is done so that the last deflection of the <see cref="CustomBumpSteerParams.WheelDeflections"/> also produces tangible results. If the extra element is not there then the Results at the 
            ///last wheel deflection would ALL be zero because of Lack of DELTA to compute from 
            /// </remarks>
            for (int i = 0; i < Setup_CV.BS_Params.ToeAngles.Count - 1; i++)
            {
                ///<summary>
                ///This IF Loop below is crucial
                ///When the <see cref="SetupChange_Optimizer"/> runs the <see cref="DoubleWishboneKinematicsSolver"/> to compute the Toe Angles for the Wheel Defelction Range, the 
                ///<see cref="SolverMasterClass.CalculatenewCamberAndToe_Rear(List{OutputClass}, int, Vehicle, int)"/> ALREADY HAS A CONDITIONING TOOL which makes the Left and Right Toe Angles according to the User's Convention 
                ///that is both wheels inwars for Toe IN which is negative and both wheels outwards which is positive 
                ///hence the Toe Angles come to me in the User convention system. 
                ///So the IF LOOP below is needed
                /// </summary>
                if (VCorner == VehicleCorner.FrontLeft || VCorner == VehicleCorner.RearLeft)
                {
                    Setup_OP.Req_BumpSteerChart.Add(new Angle(Setup_CV.BS_Params.ToeAngles[i].Degrees + StaticToe.Degrees, AngleUnit.Degrees));
                }
                else
                {
                    Setup_OP.Req_BumpSteerChart.Add(new Angle(Setup_CV.BS_Params.ToeAngles[i].Degrees - StaticToe.Degrees, AngleUnit.Degrees));

                }

            }

            ///<summary>Calling the method which performs the actual Euclidean Distance Error Computation</summary>
            Error = EvaluateDeviation(Calc_BumpSteerGraph, Setup_OP.Req_BumpSteerChart);

            return Error;

        }

        /// <summary>
        /// Method to compute the Eucledian Distance betweent he Points on the User's Bump Steer Curve and the computed Bump Steer Curve AND to find the Error which is later conditioned as the Fitness
        /// </summary>
        /// <param name="_toeAngle"><see cref="List{T}"/> of Toe Angles</param>
        /// <param name="_staticToe">The Static Toe Angle</param>
        /// <returns>Returns the error based on the Eucledian Distance</returns>
        private double EvaluateDeviation(List<Angle> _toeAngle, List<Angle> _userBumpSteerCurve)
        {
            List<Angle> UserBumpSteerCurve = _userBumpSteerCurve;

            List<Angle> ErrorCalc_Step1 = new List<Angle>();

            ///<summary>Finding the distance between each pair of Points</summary>
            for (int i = 0; i < _userBumpSteerCurve.Count; i++)
            {
                if (i != 0 && i != _userBumpSteerCurve.Count - 1)
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
                    ErrorCalc_Step1.Add(new Angle((_toeAngle[i - 1].Degrees - UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));
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

            ///<summary>
            ///You will notice that I have added only 1 condition for Bump Steer Convergence. This is because for the Special Case of BumpSteer there is no Const Bump Steer but only a "Monitor Bump Steer' 
            ///If the user chooses to monitor it then the convergence need to be computed. the Bmp Steer Graph only needs to be plotted 
            ///</summary>
            if (Setup_CV.BumpSteerChangeRequested) 
            {
                ///<summary>Setting the Bump Steer Convergence</summary>
                Setup_OP.BumpSteer_Conv = new Convergence(1 - SetConvergenceError(FinalError)); 
            }

            ///<summary>Passing the <see cref="Req_Camber"/> of the this class to the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/></summary>
            ///<remark>Don't need to do this here as the in THIS for the <see cref="SetupChange_Outputs.Req_BumpSteerChart"/> is what is being used right from the start</remark>

            Setup_OP.Calc_BumpSteerChart = _toeAngle;

            return FinalError;
        }
        #endregion

        /// <summary>
        /// Method to process the Convergence in special cases
        /// This method is necessary if the solution is not converging 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private double SetConvergenceError(double error)
        {
            ///<summary> If the error is less than 0 then I need to make it positive  to keep it within the (0 to 1) limit</summary>
            if (error < 0)
            {
                error *= -1;
            }
            ///<summary>If the error is more than one then sending 1 minus the error to keep it within the limits</summary>
            if (error > 1)
            {
                error = 1 - error;
            }

            return System.Math.Round(error, 2);
        }

        
        #endregion

    }







}

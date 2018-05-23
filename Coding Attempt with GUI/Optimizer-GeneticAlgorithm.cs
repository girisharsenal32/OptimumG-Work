using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        Dictionary<string, OptimizedOrientation> GAOrientation { get; set; }

        int BitSize = 10;

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

        Point3D TopFront;

        Point3D TopRear;

        Point3D BottomFront;

        Point3D BottomRear;


        Dictionary<string, OptimizedCoordinate> UnsprungAssembly;

        Dictionary<string, OptimizedCoordinate> InboardPoints;
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
        public OptimizerGeneticAlgorithm(double _crossover, double _mutation, int _elites, int _popSize, int _chromoseLength)
        {
            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;


            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Population = new Population(_popSize, _chromoseLength, false, false);


        }

        /// <summary>
        /// <para>---2nd--- This method is to be called 2nd in the sequence</para>
        /// <para>Method to Initialize the Vehicle of the class and initialize all the parameters of the Vehicle along with it  </para>
        /// </summary>
        /// <param name="_vCorner">Object of the <see cref="VehicleCorner"/> which decides the corner of the Vehicle calling this Class</param>
        /// <param name="_vehicle">The object of the Vehicle itself which is calling this class</param>
        public void InitializeVehicleParams(VehicleCorner _vCorner, Vehicle _vehicle, int _suspensionEvalStepSize, int _suspensionEvalUpperLimit, int _suspensionEvalLowerLimit)
        {
            SuspensionEvalStepSize = _suspensionEvalStepSize;

            SuspensionEvalLowerLimit = _suspensionEvalLowerLimit;

            SuspensionEvalUpperLimit = _suspensionEvalUpperLimit;

            ///<summary>Calculating the Number of iterations based on the <see cref="SuspensionEvalStepSize"/> the <see cref="SuspensionEvalUpperLimit"/> and the <see cref="SuspensionEvalLowerLimit"/></summary>
            SuspensionEvalIterations = GetNoOfIterations(SuspensionEvalUpperLimit, SuspensionEvalLowerLimit, SuspensionEvalStepSize);
            SuspensionEvalIterations_UpperLimit = GetNoOfIterations(SuspensionEvalUpperLimit, 0, SuspensionEvalStepSize);
            SuspensionEvalIterations_LowerLimit = GetNoOfIterations(0, SuspensionEvalLowerLimit, SuspensionEvalStepSize);


            Vehicle = _vehicle;

            VCorner = _vCorner;

            ///<summary>Invoking the <see cref="VehicleParamsAssigner.AssignVehicleParams(VehicleCorner, Vehicle, int)"/> method to assign the right Vehicle's Params (based on the Vehicle Corner) into a Dictionary</summary>
            Dictionary<string, object> tempVehicleParams = VehicleParamsAssigner.AssignVehicleParams(_vCorner, _vehicle, (SuspensionEvalIterations * 2) + 1);

            ///<summary>Passing the <see cref="Dictionary{TKey, TValue}"/> of Vehicle Params's objects into the right Parameter</summary>
            SCM = tempVehicleParams["SuspensionCoordinateMaster"] as SuspensionCoordinatesMaster;

            Tire = tempVehicleParams["Tire"] as Tire;

            Spring = tempVehicleParams["Spring"] as Spring;

            Damper = tempVehicleParams["Damper"] as Damper;

            ARB = tempVehicleParams["AntirollBar"] as AntiRollBar;
            ARBRate_Nmm = (double)tempVehicleParams["ARB_Rate_Nmm"];

            WA = tempVehicleParams["WheelAlignment"] as WheelAlignment;

            Chassis = tempVehicleParams["Chassis"] as Chassis;

            OC = tempVehicleParams["OutputClass"] as List<OutputClass>;

            Identifier = (int)tempVehicleParams["Identifier"];

            PopulateDictionaryTrial();


        }

        private void InitializeSuspensionAssemblies(Dictionary<string, OptimizedCoordinate> _assemblyPoints)
        {
            UnsprungAssembly = _assemblyPoints;
        }

        /// <summary>
        /// <para>---3rd--- This method is to be called 3rd in the sequence</para>
        /// <para>Method to construct and run the <see cref="GA"/> (<see cref="GeneticAlgorithm"/>)</para>
        /// </summary>
        public void ConstructGeneticAlgorithm()
        {
            ///<summary>Assigning the <see cref="Delegate"/> of the Ftiness Function</summary>
            EvaluateFitnessOfGeneticAlforithm = EvaluateFitnessCurve;
            ///<summary>Assigning the <see cref="Delegate"/> of the Terminate Function</summary>
            Terminate = TerminateAlgorithm;

            GA = new GeneticAlgorithm(Population, EvaluateFitnessOfGeneticAlforithm);

            ///<summary>Assigning an Event for the <see cref="GeneticAlgorithm"/></summary>
            GA.OnGenerationComplete += GA_OnGenerationComplete;

            ///<summary>Adding the <see cref="Elites"/> <see cref="Crossovers"/> and the <see cref="Mutations"/> operator to the <see cref="GA"/></summary>
            GA.Operators.Add(Elites);
            GA.Operators.Add(Crossovers);
            GA.Operators.Add(Mutations);

            ///<summary>Running the Algorithm</summary>
            GA.Run(Terminate);

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

                ///<summary>Invoking the <see cref="EvaluateBumpSteer(double, double, double)"/> method to check the error of the calcualted Bump Steer Curve with the Curve that the user wants</summary>
                double resultError = /*EvaluateBumpSteer(x, y, z);*/ EvaluateRMSError();

                ///<summary>Calculating the Fitness based on the Error</summary>
                Fitness = 1 - (resultError);
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
            /// of the BEST FIT of this Generation
            /// </summary>
            //ExtractOptimizedValues(chromosome, out double x, out double y, out double z);
            ExtractOptimizedValues(chromosome);

            ///<summary>Extracting the Max Fitness</summary>
            double Fitness = e.Population.MaximumFitness;

            double resultError = /*EvaluateBumpSteer(x, y, z);*/ EvaluateRMSError();


            int Generations = e.Generation;

            long Evaluations = e.Evaluations;
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
            if (_currGeneration > 200)
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

                double resultError = /*EvaluateBumpSteer(x, y, z);*/ EvaluateRMSError();

                return true;


            }
            else
            {
                return false;
            }
        }
        #endregion


        private void PopulateDictionaryTrial()
        {
            GAOrientation = new Dictionary<string, OptimizedOrientation>();

            Point3D upperLimit = new Point3D(50, 50, 50);

            Point3D LowerLimit = new Point3D(-50, -50, -50);

            MathNet.Spatial.Euclidean.EulerAngles upperEuler = new MathNet.Spatial.Euclidean.EulerAngles(new Angle(2, AngleUnit.Degrees), new Angle(4, AngleUnit.Degrees), new Angle(4, AngleUnit.Degrees));

            MathNet.Spatial.Euclidean.EulerAngles lowerEuler = new MathNet.Spatial.Euclidean.EulerAngles(new Angle(1, AngleUnit.Degrees), new Angle(-4, AngleUnit.Degrees), new Angle(-4, AngleUnit.Degrees));

            GAOrientation.Add("NewOrientation1", new OptimizedOrientation(upperLimit, LowerLimit, upperEuler, lowerEuler, BitSize));

            PopulateDictionaryTrial_2();
            
        }

        private void PopulateDictionaryTrial_2()
        {
            Point3D Upper = new Point3D(100, 100, 100);

            Point3D Lower = new Point3D(-100, -100, -100);


            UnsprungAssembly = new Dictionary<string, OptimizedCoordinate>();

            InboardPoints = new Dictionary<string, OptimizedCoordinate>();

            ToeLinkInboard = new Point3D(SCM.N1x, SCM.N1y, SCM.N1z);

            InboardPoints.Add("ToeLinkInboard",new OptimizedCoordinate(ToeLinkInboard, Upper, Lower, BitSize));

            TopFront = new Point3D(SCM.A1x, SCM.A1y, SCM.A1z);

            TopRear = new Point3D(SCM.B1x, SCM.B1y, SCM.B1z);

            BottomFront = new Point3D(SCM.D1x, SCM.D1y, SCM.D1z);

            BottomRear = new Point3D(SCM.C1x, SCM.C1y, SCM.C1z);


            UBJ = new Point3D(SCM.F1x, SCM.F1y, SCM.F1z);

            UnsprungAssembly.Add("UBJ", new OptimizedCoordinate(UBJ, Upper, Lower, BitSize));

            LBJ = new Point3D(SCM.E1x, SCM.E1y, SCM.E1z);

            UnsprungAssembly.Add("LBJ", new OptimizedCoordinate(LBJ, Upper, Lower, BitSize));

            WcStart = new Point3D(SCM.K1x, SCM.K1y, SCM.K1z);

            UnsprungAssembly.Add("WcStart", new OptimizedCoordinate(WcStart, Upper, Lower, BitSize));

            WcEnd = new Point3D(SCM.L1x, SCM.L1y, SCM.L1z);

            UnsprungAssembly.Add("WcEnd", new OptimizedCoordinate(WcEnd, Upper, Lower, BitSize));

            ToeLinkOutboard = new Point3D(SCM.M1x, SCM.M1y, SCM.M1z);

            UnsprungAssembly.Add("ToeLinkOutboard", new OptimizedCoordinate(ToeLinkOutboard, Upper, Lower, BitSize));

            ContactPatch = new Point3D(SCM.W1x, SCM.W1x, SCM.W1z);

            UnsprungAssembly.Add("ContactPatch", new OptimizedCoordinate(ContactPatch, Upper, Lower, BitSize));

        }

        #region --HELPER METHODS--
        //--HELPER METHODS--



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


        /// <summary>
        /// <para>Method to Extract the values out of the <see cref="Chromosome"/></para>
        /// <para>In this case the X,Y,Z coordinates of the Inoard Toe Link Point</para>
        /// </summary>
        /// <param name="chromosome"><see cref="Chromosome"/> of the <see cref="Population"/> which is to be extracted</param>
        /// <param name="_x">X Coordinate Output of the Extraction</param>
        /// <param name="_y">Y Coordinate Output of the Extraction</param>
        /// <param name="_z">Z Coordinate Output of the Extraction</param>
        private void ExtractOptimizedValues(Chromosome chromosome, out double _x, out double _y, out double _z)
        {
            

            ///<summary>
            ///Assigning the Nominal,Upper and Lower Values of the X/Y/Z Coordinates. This will be later on obtained from the User
            /// </summary>

            double nominalX = 232.12;
            double upperDeltaX = 100;
            double lowerDeltaX = -100;

            double nominalY = 64.4 + SCM.InputOriginY;
            double upperDeltaY = 100;
            double lowerDeltaY = -100;

            double nominalZ = 60.8;
            double upperDeltaZ = 20;
            double lowerDeltaZ = -2;

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
            _x = System.Math.Round((x1 * rcx) + (nominalX + lowerDeltaX) + SCM.InputOriginX, 3);
            _y = System.Math.Round((y1 * rcy) + (nominalY + lowerDeltaY) + SCM.InputOriginY, 3);
            _z = System.Math.Round((z1 * rcz) + (nominalZ + lowerDeltaZ) + SCM.InputOriginZ, 3);

        }

        /// <summary>
        /// Overloaded Method to 
        /// </summary>
        /// <param name="chromosome"></param>
        /// <param name="_OptimizedOrigin"></param>
        /// <param name="_OptimizerOrientation"></param>
        private void ExtractOptimizedValues(Chromosome chromosome/*,out Point3D _OptimizedOrigin, out MathNet.Spatial.Euclidean.EulerAngles _OptimizerOrientation, out Point3D _OptimizedInboardToeLink*/)
        {
            int geneNumber = 0;

            string coordinateName = GAOrientation.Keys.ElementAt(GAOrientation.Keys.Count - 1);






            var rOriginX = GAF.Math.GetRangeConstant((GAOrientation[coordinateName].Upper_Origin.X - GAOrientation[coordinateName].Lower_Origin.X), GAOrientation[coordinateName].BitSize);

            var rOriginY = GAF.Math.GetRangeConstant((GAOrientation[coordinateName].Upper_Origin.Y - GAOrientation[coordinateName].Lower_Origin.Y), GAOrientation[coordinateName].BitSize);

            var rOriginZ = GAF.Math.GetRangeConstant((GAOrientation[coordinateName].Upper_Origin.Z - GAOrientation[coordinateName].Lower_Origin.Z), GAOrientation[coordinateName].BitSize);

            var rEulerAlpha = GAF.Math.GetRangeConstant((GAOrientation[coordinateName].Upper_Orientation.Alpha.Degrees - GAOrientation[coordinateName].Lower_Orientation.Alpha.Degrees), GAOrientation[coordinateName].BitSize);

            var rEulerBeta = GAF.Math.GetRangeConstant((GAOrientation[coordinateName].Upper_Orientation.Beta.Degrees - GAOrientation[coordinateName].Lower_Orientation.Beta.Degrees), GAOrientation[coordinateName].BitSize);

            var rEulerGamma = GAF.Math.GetRangeConstant((GAOrientation[coordinateName].Upper_Orientation.Gamma.Degrees - GAOrientation[coordinateName].Lower_Orientation.Gamma.Degrees), GAOrientation[coordinateName].BitSize);

            var rcx = GAF.Math.GetRangeConstant(InboardPoints["ToeLinkInboard"].UpperCoordinateLimit.X - InboardPoints["ToeLinkInboard"].LowerCoordinateLimit.X, InboardPoints["ToeLinkInboard"].BitSize);

            var rcy = GAF.Math.GetRangeConstant(InboardPoints["ToeLinkInboard"].UpperCoordinateLimit.Y - InboardPoints["ToeLinkInboard"].LowerCoordinateLimit.Y, InboardPoints["ToeLinkInboard"].BitSize);

            var rcz = GAF.Math.GetRangeConstant(InboardPoints["ToeLinkInboard"].UpperCoordinateLimit.Z - InboardPoints["ToeLinkInboard"].LowerCoordinateLimit.Z, InboardPoints["ToeLinkInboard"].BitSize);






            var OriginX1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * GAOrientation[coordinateName].BitSize, GAOrientation[coordinateName].BitSize), 2);

            geneNumber++;

            var OriginY1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * GAOrientation[coordinateName].BitSize, GAOrientation[coordinateName].BitSize), 2);

            geneNumber++;

            var OriginZ1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * GAOrientation[coordinateName].BitSize, GAOrientation[coordinateName].BitSize), 2);

            geneNumber++;

            var EulerAlpha1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * GAOrientation[coordinateName].BitSize, GAOrientation[coordinateName].BitSize), 2);

            geneNumber++;

            var EulerBeta1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * GAOrientation[coordinateName].BitSize, GAOrientation[coordinateName].BitSize), 2);

            geneNumber++;

            var EulerGamma1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * GAOrientation[coordinateName].BitSize, GAOrientation[coordinateName].BitSize), 2);

            geneNumber++;

            var x1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber* InboardPoints["ToeLinkInboard"].BitSize, InboardPoints["ToeLinkInboard"].BitSize), 2);

            geneNumber++;

            var y1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * InboardPoints["ToeLinkInboard"].BitSize, InboardPoints["ToeLinkInboard"].BitSize), 2);

            geneNumber++;

            var z1 = Convert.ToInt32(chromosome.ToBinaryString(geneNumber * InboardPoints["ToeLinkInboard"].BitSize, InboardPoints["ToeLinkInboard"].BitSize), 2);






            GAOrientation["NewOrientation1"].OptimizedOrigin = new Point3D();

            GAOrientation["NewOrientation1"].OptimizedOrigin.X = System.Math.Round((OriginX1 * rOriginX) + (0 + GAOrientation[coordinateName].Lower_Origin.X) /*+ SCM.InputOriginX*/, 3);

            GAOrientation["NewOrientation1"].OptimizedOrigin.Y = System.Math.Round((OriginY1 * rOriginY) + (0 + GAOrientation[coordinateName].Lower_Origin.Y) /*+ SCM.InputOriginY*/, 3);

            GAOrientation["NewOrientation1"].OptimizedOrigin.Z = System.Math.Round((OriginZ1 * rOriginZ) + (0 + GAOrientation[coordinateName].Lower_Origin.Z) /*+ SCM.InputOriginZ*/, 3);


            Angle OptimizedAlpha = new Angle(System.Math.Round((EulerAlpha1 * rEulerAlpha) + (0 + GAOrientation[coordinateName].Lower_Orientation.Alpha.Degrees), 3), AngleUnit.Degrees);

            Angle OptimizedBeta = new Angle(System.Math.Round((EulerBeta1 * rEulerBeta) + (0 + GAOrientation[coordinateName].Lower_Orientation.Beta.Degrees), 3), AngleUnit.Degrees);

            Angle OptimizedGamma = new Angle(System.Math.Round((EulerGamma1 * rEulerGamma) + (0 + GAOrientation[coordinateName].Lower_Orientation.Gamma.Degrees), 3), AngleUnit.Degrees);

            GAOrientation["NewOrientation1"].OptimizedEulerAngles = new MathNet.Spatial.Euclidean.EulerAngles(OptimizedAlpha, OptimizedBeta, OptimizedGamma);


            // multiply by the appropriate range constant and adjust for any offset 
            // in the range to get the real values
            ///<remarks>
            ///https://gaframework.org/wiki/index.php/How_to_Encode_Parameters
            ///Visit link above for more information on the code below
            /// </remarks>
            InboardPoints["ToeLinkInboard"].OptimizedCoordinates = new Point3D();
            InboardPoints["ToeLinkInboard"].OptimizedCoordinates.X = System.Math.Round((x1 * rcx) + (InboardPoints["ToeLinkInboard"].NominalCoordinates.X + InboardPoints["ToeLinkInboard"].LowerCoordinateLimit.X) + SCM.InputOriginX, 3);
            InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Y = System.Math.Round((y1 * rcy) + (InboardPoints["ToeLinkInboard"].NominalCoordinates.Y + InboardPoints["ToeLinkInboard"].LowerCoordinateLimit.Y) + SCM.InputOriginY, 3);
            InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Z = System.Math.Round((z1 * rcz) + (InboardPoints["ToeLinkInboard"].NominalCoordinates.Z + InboardPoints["ToeLinkInboard"].LowerCoordinateLimit.Z) + SCM.InputOriginZ, 3);


        }

        private double EvaluateRMSError()
        {
            double BumpSteerError = EvaluateBumpSteer(InboardPoints["ToeLinkInboard"].OptimizedCoordinates.X, InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Y, InboardPoints["ToeLinkInboard"].OptimizedCoordinates.Z);

            double WishboneLengthError = EvaluateUpdatedOrientation(GAOrientation["NewOrientation1"]);

            if (BumpSteerError >= 1)
            {
                return 1;
            }
            if (WishboneLengthError >= 1)
            {
                return 1;
            }

            BumpSteerError = 0;

            double rmsError = System.Math.Sqrt((System.Math.Pow(BumpSteerError, 2) + System.Math.Pow(WishboneLengthError, 2)) / 1);

            return rmsError;
        }

        /// <summary>
        /// Method to which calls the <see cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/> Solver
        /// to compute the Bump Steer Values for the Wheel Deflection Range set by the User
        /// </summary>
        /// <param name="_xCoord">X Coordinate of the Inboard Toe Link Pick-Up Point</param>
        /// <param name="_yCoord">Y Coordinate of the Inboard Toe Link Pick-Up Point</param>
        /// <param name="_zCoord">x Coordinate of the Inboard Toe Link Pick-Up Point</param>
        /// <returns>Returns Error of the computed Bump Steer Curve with the Curve that the user wants</returns>
        private double EvaluateBumpSteer(double _xCoord, double _yCoord, double _zCoord)
        {
            double bumpSteer = 1;

            int StepSize = 1;

            Point3D InboardPickUpPoint = new Point3D(_xCoord, _yCoord, _zCoord);

            DoubleWishboneKinematicsSolver dwSolver = new DoubleWishboneKinematicsSolver();

            ///<summary>Assigning the Simulation Type in the <see cref="DoubleWishboneKinematicsSolver"/></summary>
            dwSolver.SimulationType = SimulationType.Optimization;
            ///<summary>Assignin the Simulation type in the <see cref="SolverMasterClass"/></summary>
            SolverMasterClass.SimType = SimulationType.Optimization;

            ///<summary>Assigning the Optimized Inboard Pick Up Point in the <see cref="DoubleWishboneKinematicsSolver"/></summary>
            dwSolver.OptimizedSteeringPoint = InboardPickUpPoint;

            ///<summary>
            ///Invoking the <see cref="DoubleWishboneKinematicsSolver.Kinematics(int, SuspensionCoordinatesMaster, WheelAlignment, Tire, AntiRollBar, double, Spring, Damper, List{OutputClass}, Vehicle, List{double}, bool, bool)"/>
            ///Class to compute the Kinematics and calculate the Bump Steer at each interval of Wheel Deflection
            /// </summary>
            dwSolver.Kinematics(Identifier, SCM, WA, Tire, ARB, ARBRate_Nmm, Spring, Damper, OC, Vehicle, CalculateWheelDeflections(StepSize, dwSolver), true, false);

            ///<summary>Invoking the <see cref="GetResultValues(List{OutputClass})"/> to extract the Bump Steer Data</summary>
            bumpSteer = GetResultValues(OC);

            ///<summary>Reassigning the Simulation type to Dummy so to prevent confusion if any other simulation is run after this one</summary>
            dwSolver.SimulationType = SimulationType.Dummy;

            SolverMasterClass.SimType = SimulationType.Dummy;

            return bumpSteer;

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

            List<Angle> ToeAngleRelevant = new List<Angle>();

            for (int i = 0; i < _oc.Count; i++)
            {
                ToeAngles.Add(new Angle(_oc[i].waOP.StaticToe, AngleUnit.Radians));
            }

            ToeAngles_Bump.AddRange(ToeAngles.GetRange(0, SuspensionEvalIterations_UpperLimit + 1));

            ToeAngle_Rebound.AddRange(ToeAngles.GetRange((SuspensionEvalIterations_UpperLimit * 2) + 1, SuspensionEvalIterations_LowerLimit));

            ToeAngleRelevant.AddRange(ToeAngle_Rebound);

            ToeAngleRelevant.AddRange(ToeAngles_Bump);

            ToeAngleRelevant.Sort();

            Angle StaticToe = new Angle(WA.StaticToe, AngleUnit.Degrees);

            ///<summary>Invoking the <see cref="EvaluateDeviation(List{Angle}, Angle)"/> method to compute the Eucledian Ditance between the User's Bump Steer Curve and the computed Bump Steer Curve. The output is the Error which needs to be minimized</summary>
            double Error = EvaluateDeviation(ToeAngleRelevant, StaticToe);

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
                UserBumpSteerCurve.Add(new Angle(/*_staticToe.Degrees + (i * 0.12)*/  _staticToe.Degrees, AngleUnit.Degrees));
            }

            List<Angle> ErrorCalc_Step1 = new List<Angle>();

            ///<summary>Finding the distance between each pair of Points</summary>
            for (int i = 0; i < SuspensionEvalIterations + 1; i++)
            {
                if (i != SuspensionEvalIterations - 1)
                {

                    ErrorCalc_Step1.Add(UserBumpSteerCurve[i] - _toeAngle[i]);

                    //ErrorCalc_Step1.Add(new Angle(((UserBumpSteerCurve[i].Degrees - _toeAngle[i].Degrees) / UserBumpSteerCurve[i].Degrees), AngleUnit.Degrees));

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

            if (FinalError > 1)
            {
                FinalError = 1;
            }

            return FinalError;
        }

        private double EvaluateUpdatedOrientation(OptimizedOrientation _OptimizationOrientation)
        {
            Transformation TransformationMatrix = GenerateTransformationMatrix(_OptimizationOrientation.OptimizedEulerAngles, _OptimizationOrientation.OptimizedOrigin);

            UpdateOrientation(TransformationMatrix);

            return EvaluateWishboneConstraints();
        }

        private Transformation GenerateTransformationMatrix(MathNet.Spatial.Euclidean.EulerAngles _optimizedOrientation, Point3D _optimizedOrigin)
        {

            var UprightCSTranslation = Matrix<double>.Build.DenseOfArray(new double[,]
                {
                    { (1) , (0) , (0) , (UnsprungAssembly["WcStart"].NominalCoordinates.X)},
                    { (0) , (1) , (0) , (UnsprungAssembly["WcStart"].NominalCoordinates.Y)},
                    { (0) , (0) , (1) , (UnsprungAssembly["WcStart"].NominalCoordinates.Z)},
                    { (0) , (0) , (0) , (1)},
                });


            Angle a = _optimizedOrientation.Alpha;

            Angle b = _optimizedOrientation.Beta;

            Angle g = _optimizedOrientation.Gamma;

            double S1 = System.Math.Sin(a.Radians);

            double S2 = System.Math.Sin(b.Radians);

            double S3 = System.Math.Sin(g.Radians);


            double C1 = System.Math.Cos(a.Radians);

            double C2 = System.Math.Cos(b.Radians);

            double C3 = System.Math.Cos(g.Radians);



            //var EulerRotationMatrix_ZXY = Matrix<double>.Build.DenseOfArray(new double[,]
            //{
            //    { ((C1 * C3) - (C2 * S1 * S3))   ,   (-(C1 * S3) - (C2 * C3 * S1))   ,   (S1 * S2)   ,   0},
            //    { ((C3 * S1) + (C1 * C2 * S3))   ,   (-(S1 * S3) + (C1 * C2 * C3))   ,   (-C1 * S2)  ,   0},
            //    { (S2 * S3)                      ,   (C3 * S2)                       ,   (C2)        ,   0},
            //    { (0)                            ,   (0)                             ,   (0)         ,   1}

            //});
            //var EulerRotationMatrix_YZX = Matrix<double>.Build.DenseOfArray(new double[,]
            //{
            //    { ((C1 * C3) - (C2 * S1 * S3))   ,   (-(C1 * S3) - (C2 * C3 * S1))   ,   (S1 * S2)   ,   0},
            //    { ((C3 * S1) + (C1 * C2 * S3))   ,   (-(S1 * S3) + (C1 * C2 * C3))   ,   (-C1 * S2)  ,   0},
            //    { (S2 * S3)                      ,   (C3 * S2)                       ,   (C2)        ,   0},
            //    { (0)                            ,   (0)                             ,   (0)         ,   1}

            //});


            //var TaitRotationMatrix_ZXY = Matrix<double>.Build.DenseOfArray(new double[,]
            //{
            //    { ((C1 * C3) - (S1 * S2 * S3))   ,   (-(C2 * S1))   ,   ((C1 * S3) + (C3 * S1 * S2 ))  ,   0},
            //    { ((C3 * S1) + (C1 * S2 * S3))   ,   ((C1 * C2))    ,   ((S1 * S3) - (C1 * C3 * S2))   ,   0},
            //    { (-C2 * S3)                     ,   (S2 )          ,   (C2 * S3)                      ,   0},
            //    { (0)                            ,   (0)            ,   (0)                            ,   1}

            //});
            var TaitRotationMatrix_YZX = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { ((C1 * C2))               ,   ((S1 * S3) - (C1 * C3 * S2))   ,   ((C3 * S1) + (C1 * S2 * S3 ))  ,   0},
                { (S2)                      ,   ((C2 * C3))                    ,   (-(C2 * S3))                   ,   0},
                { (-C2 * S1)                ,   ((C1 * S3) + (C3 * S1 * S2) )  ,   ((C1 * C3) - (S1 * S2 * S3))   ,   0},
                { (0)                       ,   (0)                            ,   (0)                            ,   1}

            });


            double dx = -_optimizedOrigin.X;

            double dy = -_optimizedOrigin.Y;

            double dz = -_optimizedOrigin.Z;



            var OrientationTranslationMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { (1) , (0) , (0) , (dx)},
                { (0) , (1) , (0) , (dy)},
                { (0) , (0) , (1) , (dz)},
                { (0) , (0) , (0) , (1)}
            });

            var UprightCSInverseTranslation = Matrix<double>.Build.DenseOfArray(new double[,]
                {
                    { (1) , (0) , (0) , (-UnsprungAssembly["WcStart"].NominalCoordinates.X)},
                    { (0) , (1) , (0) , (-UnsprungAssembly["WcStart"].NominalCoordinates.Y)},
                    { (0) , (0) , (1) , (-UnsprungAssembly["WcStart"].NominalCoordinates.Z)},
                    { (0) , (0) , (0) , (1)},
                });

            //Matrix<double> Transformation = EulerRotationMatrix.Solve(TranslationMatrix);


            Matrix<double> Transformation_1 = UprightCSTranslation.Multiply(TaitRotationMatrix_YZX);

            Matrix<double> Transformation_2 = Transformation_1.Solve(OrientationTranslationMatrix);

            Matrix<double> Transformation_3 = Transformation_2.Solve(UprightCSInverseTranslation);

            Matrix<double> FinalTransformation = Transformation_3;


            Transformation TransformationMatrix = new Transformation(FinalTransformation.ToArray());

            return TransformationMatrix;
            
        }



        private void UpdateOrientation(Transformation _transformationMatrix)
        {
            foreach (string point in UnsprungAssembly.Keys)
            {
                Point3D tempPoint = UnsprungAssembly[point].NominalCoordinates.Clone() as Point3D;
                tempPoint.TransformBy(_transformationMatrix);
                UnsprungAssembly[point].OptimizedCoordinates = tempPoint.Clone() as Point3D;
            }

        }

        //Trial for Caster Change with Toe Constant Constraint
        private double EvaluateWishboneConstraints()
        {
            double linkLengthError = 0;

            double TopFrontLength = System.Math.Round(SCM.UpperFrontLength, 3);

            double TopFrontLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["UBJ"].OptimizedCoordinates.DistanceTo(TopFront),3);

            double TopRearLength = System.Math.Round(SCM.UpperRearLength,3);

            double TopRearLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["UBJ"].OptimizedCoordinates.DistanceTo(TopRear),3);

            double BottomFrontLength = System.Math.Round(SCM.LowerFrontLength,3);

            double BottomFrontLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["LBJ"].OptimizedCoordinates.DistanceTo(BottomFront),3);

            double BottomRearLength = System.Math.Round(SCM.LowerRearLength,3);

            double BottomRearLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["LBJ"].OptimizedCoordinates.DistanceTo(BottomRear),3);

            double ToeLinkLength = System.Math.Round(SCM.ToeLinkLength, 3);

            double ToeLinkLength_UpdatedOrientation = System.Math.Round(UnsprungAssembly["ToeLinkOutboard"].OptimizedCoordinates.DistanceTo(ToeLinkInboard), 3);

            ///<remarks>TopFrontLength not checked because it is a degree of freedom required for Caster Chnge</remarks>
            //if (System.Math.Abs(TopFrontLength)-System.Math.Abs(TopFrontLength_UpdatedOrientation)>0.0009)
            //{
            //    linkLengthError = 1;
            //}

            linkLengthError += System.Math.Pow(CalculateScaledError(TopFrontLength, TopFrontLength_UpdatedOrientation), 2);

            //linkLengthError += System.Math.Pow(CalculateScaledError(TopRearLength, TopRearLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateScaledError(BottomFrontLength, BottomFrontLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateScaledError(BottomRearLength, BottomRearLength_UpdatedOrientation), 2);

            linkLengthError += System.Math.Pow(CalculateScaledError(ToeLinkLength, ToeLinkLength_UpdatedOrientation), 2);

            linkLengthError /= 4;

            linkLengthError = System.Math.Sqrt(linkLengthError);

            return linkLengthError;
        }

        private double CalculateScaledError(double _original, double _calculated)
        {
            double scalingFactor = 1;

            if (_original > _calculated) 
            {
                scalingFactor = _calculated / _original;
            }
            else
            {
                scalingFactor = _original / _calculated;
            }

            double difference = System.Math.Abs((_original - _calculated));

            double scaledError = (difference / _original) * scalingFactor;

            return scaledError;
        }


        #endregion

    }

    enum WheelDeflectionType
    {
        Bump,
        Rebound
    }

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

        public int BitSize;

        public OptimizedCoordinate(Point3D _nominalCoord, Point3D _upperLimit, Point3D _lowerLimit, int _bitSize)
        {
            NominalCoordinates = _nominalCoord;

            OptimizedCoordinates = NominalCoordinates.Clone() as Point3D;

            UpperCoordinateLimit = _upperLimit;

            LowerCoordinateLimit = _lowerLimit;

            BitSize = _bitSize;
        }
    }

    public class OptimizedOrientation
    {
        public Point3D Upper_Origin;

        public Point3D Lower_Origin;

        public MathNet.Spatial.Euclidean.EulerAngles Upper_Orientation;

        public MathNet.Spatial.Euclidean.EulerAngles Lower_Orientation;

        public Point3D OptimizedOrigin;

        public MathNet.Spatial.Euclidean.EulerAngles OptimizedEulerAngles;

        public int BitSize;

        public OptimizedOrientation(Point3D _upperOrigin, Point3D _lowerOrigin, MathNet.Spatial.Euclidean.EulerAngles _upperOrientation, MathNet.Spatial.Euclidean.EulerAngles _lowerOrientation, int _bitSize)
        {

            Upper_Origin = _upperOrigin;

            Lower_Origin = _lowerOrigin;

            OptimizedOrigin = new Point3D();

            Upper_Orientation = _upperOrientation;

            Lower_Orientation = _lowerOrientation;

            OptimizedEulerAngles = new MathNet.Spatial.Euclidean.EulerAngles();

            BitSize = _bitSize;

        }


    }





}

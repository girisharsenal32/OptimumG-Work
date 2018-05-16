using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public class OptimizerGeneticAlgorithm
    {
        double CrossOverProbability { get; set; }

        Crossover Crossovers { get; set; }

        double MutationProbability { get; set; }

        BinaryMutate Mutations { get; set; }

        int ElitePercentage { get; set; }

        Elite Elites { get; set; }

        List<double> CoordinateList { get; set; } = new List<double>();

        Population Population { get; set; }

        GeneticAlgorithm GA { get; set; }
        
        FitnessFunction EvaluateFitness;

        TerminateFunction Terminate;



        VehicleCorner VCorner;

        SuspensionCoordinatesMaster SCM;

        Spring Spring;

        Damper Damper;

        AntiRollBar ARB;
        double ARBRate_Nmm;

        Tire Tire;

        Chassis Chassis;

        WheelAlignment WA;

        List<OutputClass> OC;

        int Identifier;

        Vehicle Vehicle;

        /// <summary>
        /// <para>---1st---</para>
        /// </summary>
        /// <param name="_crossover"></param>
        /// <param name="_mutation"></param>
        /// <param name="_elites"></param>
        /// <param name="_popSize"></param>
        /// <param name="_chromoseLength"></param>
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
        /// <para>---2nd---</para>
        /// </summary>
        /// <param name="_vCorner"></param>
        /// <param name="_vehicle"></param>
        public void InitializeVehicleParams(VehicleCorner _vCorner, Vehicle _vehicle)
        {
            Vehicle = _vehicle;

            VCorner = _vCorner;

            Dictionary<string, object> tempVehicleParams = VehicleParamsAssigner.AssignVehicleParams(_vCorner, _vehicle);

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


        }

        /// <summary>
        /// <para>---3rd---</para>
        /// </summary>
        public void ConstructGeneticAlgorithm()
        {
            EvaluateFitness = EvaluateBumpSteerFitness;

            Terminate = TerminateAlgorithm;

            GA = new GeneticAlgorithm(Population, EvaluateFitness);

            GA.OnGenerationComplete += GA_OnGenerationComplete;

            GA.Operators.Add(Elites);
            GA.Operators.Add(Crossovers);
            GA.Operators.Add(Mutations);

            GA.Run(Terminate);

        }

        private void GA_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var chromosome = e.Population.GetTop(1)[0];
        }

        private double EvaluateBumpSteerFitness(Chromosome chromosome)
        {
            double bsFitness = -1;

            if (chromosome != null)
            {

                // range constant for x
                var rcx = GAF.Math.GetRangeConstant(40, 20);

                // range constant for y
                var rcy = GAF.Math.GetRangeConstant(10, 20);

                // range constant for z
                var rcz = GAF.Math.GetRangeConstant(20, 20);

                // when evaluating the fitness simply retrieve the 20 bit values as integers 
                // from the chromosome e.g.
                var x1 = Convert.ToInt32(chromosome.ToBinaryString(0, 20), 2);
                var y1 = Convert.ToInt32(chromosome.ToBinaryString(20, 20), 2);
                var z1 = Convert.ToInt32(chromosome.ToBinaryString(40, 20), 2);

                // multiply by the appropriate range constant and adjust for any offset 
                // in the range to get the real values
                double x = (x1 * rcx) + 212.12;
                double y = (y1 * rcy) + 59.4;
                double z = (y1 * rcz) + 70.8;


                //using binary F6 for fitness.
                var temp1 = System.Math.Sin(System.Math.Sqrt(x * x + y * y));
                var temp2 = 1 + 0.001 * (x * x + y * y);
                var result = 0.5 + (temp1 * temp1 - 0.5) / (temp2 * temp2);


                bsFitness = 1 - result;
            }

            return bsFitness;
        }

        private bool TerminateAlgorithm(Population _population, int _currGeneration, long currEvaluation)
        {
            if (_population.MinimumFitness < 0.01)  
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        private void EvaluateBumpSteer(double _xCoord, double _yCoord, double _zCoord)
        {
            Point3D InboardPickUpPoint = new Point3D(_xCoord, _yCoord, _zCoord);

            DoubleWishboneKinematicsSolver dwSolver = new DoubleWishboneKinematicsSolver();

            dwSolver.OptimizedSteeringPoint = InboardPickUpPoint;

            dwSolver.Kinematics(Identifier, SCM, WA, Tire, ARB, ARBRate_Nmm, Spring, Damper, OC, Vehicle, CalculateWheelDeflections(), true, false);

        }

        private List<double> CalculateWheelDeflections()
        {
            double UpperWheelDeflectionLimit = 25;
            double LowerWheelDeflectionLimit = -25;
            double Range = UpperWheelDeflectionLimit - LowerWheelDeflectionLimit;
            int NoOfIterations = 100;
            double StepSize = Range / NoOfIterations;

            List<double> wheelDeflections = new List<double>();

            for (int i = 0; i < NoOfIterations; i++)
            {
                wheelDeflections.Add(LowerWheelDeflectionLimit + StepSize);
            }

            return wheelDeflections;
        }

        private double CalculateAverageBumpSteer(List<OutputClass> _oc)
        {
            double averageBS = 1;

            double maxBumpSteer = 0;

            List<double> ToeAngles = new List<double>();

            List<double> ToeVariations = new List<double>();

            for (int i = 0; i < _oc.Count; i++)
            {
                ToeAngles.Add(_oc[i].waOP.StaticToe);
            }

            for (int i = 0; i < ToeAngles.Count; i++)
            {
                if (i==0)
                {
                    ToeVariations.Add(0);
                }
                else
                {
                    ToeVariations.Add(ToeAngles[i] - ToeAngles[i - 1]);
                }
            }

            averageBS = ToeVariations.Sum() / ToeVariations.Count;

            maxBumpSteer = ToeVariations.Max();

            return averageBS;

        }








        
    }
}

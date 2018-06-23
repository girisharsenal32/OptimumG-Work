using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class KO_SimulationParams
    {

        /// <summary>
        /// <para>---VERY IMPORTANT---</para>
        /// <para>This variable dictates the Number of Iterations that the user would like to run the Kinematic Solver for</para>
        /// <para>Based on this variable the Number of Wheel Deflections AND Wheel Steers will be determined</para>
        /// <para>The Step Size is also determined by this</para>
        /// </summary>
        public int NumberOfIterations_KinematicSolver { get; set; } 

        /// <summary>
        /// <para>---IMPORTANT---</para>
        /// <para>This variable determines the Upper Limit off ALL the Heave Charts</para>
        /// <para>It is crucial that ALL the Heave Charts have the same Upper Value, Lower Value and Range</para>
        /// </summary>
        public double Maximum_Heave_Deflection { get; set; }

        /// <summary>
        /// <para>---IMPORTANT---</para>
        /// <para>This variable determines the Lower Limit off ALL the Heave Charts</para>
        /// <para>It is crucial that ALL the Heave Charts have the same Upper Value, Lower Value and Range</para>
        /// </summary>
        public double Minimum_Heave_Deflection { get; set; }

        /// <summary>
        /// This variable holds the information regarding the index of the <see cref="Maximum_Heave_Deflection"/>
        /// </summary>
        public int Max_HeaveDeflection_Index { get; set; }


        /// <summary>
        /// <para>---IMPORTANT---</para>
        /// <para>This variable determines the Upper Limit off ALL the Steering Charts</para>
        /// <para>It is crucial that ALL the Steering Charts have the same Upper Value, Lower Value and Range</para>
        /// </summary>
        public double Maximum_Steering_Angle { get; set; }

        /// <summary>
        /// <para>---IMPORTANT---</para>
        /// <para>This variable determines the Upper Limit off ALL the Steering Charts</para>
        /// <para>It is crucial that ALL the Steering Charts have the same Upper Value, Lower Value and Range</para>
        /// </summary>
        public double Minimum_Steering_Angle { get; set; }

        /// <summary>
        /// This variable holds information regarding the index of the <see cref="Maximum_Steering_Angle"/>
        /// </summary>
        public int Max_SteeringAngle_Index { get; set; }

        /// <summary>
        /// Step Size calculated for the Heave based on the <see cref="Maximum_Heave_Deflection"/> & <see cref="Minimum_Heave_Deflection"/>
        /// </summary>
        public double StepSize_Heave { get; set; }

        /// <summary>
        /// Step Size calculated for the Steering based on the <see cref="Maximum_Steering_Angle"/> & <see cref="Minimum_Steering_Angle"/>
        /// </summary>
        public double StepSize_Steering { get; set; }


        public KO_SimulationParams()
        {
            NumberOfIterations_KinematicSolver = 75;


            Maximum_Heave_Deflection = 25;

            Minimum_Heave_Deflection = -25;


            Maximum_Steering_Angle = 120;

            Minimum_Steering_Angle = -120;

        }



        /// <summary>
        /// Method to compute the <see cref="StepSize_Heave"/>
        /// </summary>
        /// <param name="_maxHeave">Maximum Heave Deflection as input by the User</param>
        /// <param name="_minHeave">Minimum Heave Deflection as input by the User</param>
        /// <param name="_noOfIterations">Number of Iterations of the Kinematic Solver as input by the user</param>
        /// <returns>Returns the Heave Step Size</returns>
        public double Compute_StepSize_Heave(double _maxHeave, double _minHeave, int _noOfIterations)
        {
            ///<remarks>
            ///The <see cref="_maxHeave"/> is considered twice for a reason. 
            ///The <see cref="BumpSteerCurve"/> computes a Wheel Defelction Range starting from 0 to Max and then from Max to Min
            ///Hence, in order to account for the portion ranging from 0 to Max, the <see cref="_maxHeave"/> is accounted for twice
            ///<see cref="CustomBumpSteerParams.PopulateBumpSteerGraph(List{double}, List{double})"/> to understand how 0 to Max is accounted for 
            /// </remarks>
            double stepSizeHeave = (_maxHeave + _maxHeave - _minHeave) / _noOfIterations;

            return stepSizeHeave;
        }

        /// <summary>
        /// Method to compute the <see cref="StepSize_Steering"/> 
        /// </summary>
        /// <param name="_maxSteering">Maximum Steering Angle as input by the User</param>
        /// <param name="_minSteering">Minimum Steering Angle as input by the User</param>
        /// <param name="_noOfIterations">Number of Iterations of the Kinematic Solver as input by the user</param>
        /// <returns>Returns the Steering Step Size</returns>
        public double Compute_StepSize_Steering(double _maxSteering, double _minSteering, int _noOfIterations)
        {
            ///<remarks>
            ///The <see cref="_maxSteering"/> is considered twice for a reason. 
            ///The <see cref="BumpSteerCurve"/> computes a Wheel Defelction Range starting from 0 to Max and then from Max to Min
            ///Hence, in order to account for the portion ranging from 0 to Max, the <see cref="_maxSteering"/> is accounted for twice
            ///<see cref="CustomBumpSteerParams.PopulateBumpSteerGraph(List{double}, List{double})"/> to understand how 0 to Max is accounted for 
            /// </remarks>
            double stepSizeSteering = (_maxSteering + _maxSteering - _minSteering) / _noOfIterations;

            return stepSizeSteering;
        }

    }
}

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
        /// Step Size calculated for the Heave based on the <see cref="Maximum_Heave_Deflection"/> & <see cref="Minimum_Heave_Deflection"/>
        /// </summary>
        public int StepSize_Heave { get; set; }

        /// <summary>
        /// Step Size calculated for the Steering based on the <see cref="Maximum_Steering_Angle"/> & <see cref="Minimum_Steering_Angle"/>
        /// </summary>
        public int StepSize_Steering { get; set; }


    }
}

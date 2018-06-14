using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class KinematicsOptimization
    {
        /// <summary>
        /// Name of the Kinematic Optimization
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID of the Kinematic Optimization
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Static Object to keep track of all the Kinematic Optimizations created so far
        /// </summary>
        public static int Counter { get; set; }

        /// <summary>
        /// Static List which holds all the <see cref="KinematicsOptimization"/> class objects
        /// </summary>
        public static List<KinematicsOptimization> List_KO = new List<KinematicsOptimization>();

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Front Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FL;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Front Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FR;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Rear Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RL;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Rear Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RR;

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/>
        /// </summary>
        public KO_SimulationParams KO_SimParams;
    


        public KinematicsOptimization() { }


        public KinematicsOptimization(string _name, int _id)
        {
            Name = _name;

            ID = _id;
        }
        
        /// <summary>
        /// Method to Initialize OR Update the Simulation Parameters
        /// </summary>
        /// <param name="_koSimparams"></param>
        public void Init_Update_Simulationparameters(KO_SimulationParams _koSimparams)
        {
            KO_SimParams = _koSimparams;
        }

        /// <summary>
        /// Method to Updat the <see cref="KO_CornverVariables"/> using the <see cref="KinematicsOptimization_GUI"/> Class objects which have been updated by the user him/her self
        /// </summary>
        /// <param name="_koCV_FL_GUI">Front Left <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCV_FR_GUI">Front Right <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCV_RL_GUI">Rear Left <see cref="KO_CornverVariables"/></param>
        /// <param name="_koCV_RR_GUI">Rear Right <see cref="KO_CornverVariables"/></param>
        public void Init_Update_CornerVariables(KO_CornverVariables _koCV_FL_GUI, KO_CornverVariables _koCV_FR_GUI, KO_CornverVariables _koCV_RL_GUI, KO_CornverVariables _koCV_RR_GUI)
        {
            KO_CV_FL = _koCV_FL_GUI;

            KO_CV_FR = _koCV_FR_GUI;

            KO_CV_RL = _koCV_RL_GUI;

            KO_CV_RR = _koCV_RR_GUI;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class KinematicsOptimization_GUI
    {

        /// <summary>
        /// Name of the Kinematic Optimization
        /// </summary>
        public string Name_GUI { get; set; }

        /// <summary>
        /// ID of the Kinematic Optimization
        /// </summary>
        public int ID_GUI { get; set; }

        /// <summary>
        /// Static Object to keep track of all the Kinematic Optimizations created so far
        /// </summary>
        public static int Counter_GUI { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/> which stores the information regarding Number of Iterations etc.
        /// </summary>
        public KO_SimulationParams KO_SimParams { get; set; }

        /// <summary>
        /// <see cref="KO_CornverVariables"/>  GUI Object of the Front Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FL_GUI { get; set; }
        
        /// <summary>
        /// <see cref="KO_CornverVariables"/>  GUI Object of the Front Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FR_GUI { get; set; }
        
        /// <summary>
        /// <see cref="KO_CornverVariables"/> GUI  Object of the Rear Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RL_GUI { get; set; }
     
        /// <summary>
        /// <see cref="KO_CornverVariables"/> GUI Object of the Rear Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RR_GUI { get; set; }


        public XUC_KinematicOptimization xuc_KO;


        public KinematicsOptimization_GUI(){}


        public KinematicsOptimization_GUI(string _name, int _id)
        {
            Name_GUI = _name;

            ID_GUI = _id;

            KO_CV_FL_GUI = new KO_CornverVariables();

            KO_CV_FR_GUI = new KO_CornverVariables();

            KO_CV_RL_GUI = new KO_CornverVariables();

            KO_CV_RR_GUI = new KO_CornverVariables();

            KO_SimParams = new KO_SimulationParams();

            xuc_KO = new XUC_KinematicOptimization();

        }


    }
}

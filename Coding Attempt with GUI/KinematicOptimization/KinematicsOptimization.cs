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
        /// <see cref="KO_CornverVariables"/> Object of the Front Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FL { get; set; }

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Front Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FR { get; set; }
        
        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Rear Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RL { get; set; }
        
        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Rear Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RR { get; set; }


        public KinematicsOptimization() { }


        public KinematicsOptimization(string _name, int _id)
        {
            Name = _name;

            ID = _id;
        }



        
    }
}

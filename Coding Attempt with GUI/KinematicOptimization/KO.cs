using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class KO
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
        /// Static List which holds all the <see cref="KO"/> class objects
        /// </summary>
        public static List<KO> List_KO = new List<KO>();

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Front Left Corner
        /// </summary>
        KO_CornverVariables KO_CV_FL;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Front Right Corner
        /// </summary>
        KO_CornverVariables KO_CV_FR;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Rear Left Corner
        /// </summary>
        KO_CornverVariables KO_CV_RL;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> Object of the Rear Right Corner
        /// </summary>
        KO_CornverVariables KO_CV_RR;

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/>
        /// </summary>
        KO_SimulationParams KO_SimParams;


        private Vehicle KO_Vehicle;
        

        /// <summary>
        /// Object of the <see cref="KO_Master_Optimizer"/> Class which performs the Genetic Algorithm based optimization
        /// </summary>
        private KO_Master_Optimizer Optimizer;


        public KO() { }


        public KO(string _name, int _id)
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
        /// Method to Updat the <see cref="KO_CornverVariables"/> using the <see cref="KO_GUI"/> Class objects which have been updated by the user him/her self
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

        /// <summary>
        /// Method to Initialize the Vehicle item of the <see cref="KO"/> class
        /// </summary>
        /// <param name="_assembledVehicle"></param>
        public void Init_KOVehicle(Vehicle _assembledVehicle)
        {
            KO_Vehicle = _assembledVehicle;
        }


        ///// <summary>
        ///// Method to Initialize the <see cref="KO_Master_Optimizer"/> CLass object to proceed with the Genetic Algorithm based Optimization
        ///// </summary>
        //public void Init_GeneticAlgorithm()
        //{
        //    Optimizer = new KO_Master_Optimizer(0.85, 0.05, 5, 50);

        //    Optimizer.Initialize_CornverVariables(KO_Vehicle, KO_SimParams, KO_CV_FL, KO_CV_FR, KO_CV_RL, KO_CV_RR);

        //}



        
    }
}

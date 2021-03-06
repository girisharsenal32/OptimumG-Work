﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class KO_GUI
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
        /// Static List which holds all the <see cref="KO_GUI"/> class objects
        /// </summary>
        public static List<KO_GUI> List_KO_GUI = new List<KO_GUI>();

        /// <summary>
        /// Object of the <see cref="KO_SimulationParams"/> which stores the information regarding Number of Iterations etc.
        /// </summary>
        public KO_SimulationParams KO_SimParams;

        /// <summary>
        /// <see cref="KO_CornverVariables"/>  GUI Object of the Front Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FL_GUI;

        /// <summary>
        /// <see cref="KO_CornverVariables"/>  GUI Object of the Front Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_FR_GUI;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> GUI  Object of the Rear Left Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RL_GUI;

        /// <summary>
        /// <see cref="KO_CornverVariables"/> GUI Object of the Rear Right Corner
        /// </summary>
        public KO_CornverVariables KO_CV_RR_GUI;

        /// <summary>
        /// Object of the <see cref="KO_CentralVariables"/> class which holds important parameter info which is central to the Vehicle
        /// </summary>
        public KO_CentralVariables KO_Central;

        /// <summary>
        /// The <see cref="XUC_KinematicOptimization"/> UserControl 
        /// </summary>
        public XUC_KinematicOptimization xuc_KO;

        /// <summary>
        /// Object of the <see cref="ParametersAndImportance"/> Form where the user can select the Parameters and set their Importance
        /// </summary>
        public ParametersAndImportance Param_Imp_Form;

        /// <summary>
        /// Obbject of the <see cref="DesignForm"/> Form which will be used to create the Suspension from Scratch
        /// </summary>
        public DesignForm Design_Form;
        



        public KO_GUI(){}

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="_name">Name of <see cref="KO_GUI"/></param>
        /// <param name="_id">ID of the <see cref="KO_GUI"/></param>
        public KO_GUI(string _name, int _id)
        {
            Name_GUI = _name;

            ID_GUI = _id;

            KO_CV_FL_GUI = new KO_CornverVariables(VehicleCorner.FrontLeft);

            KO_CV_FR_GUI = new KO_CornverVariables(VehicleCorner.FrontRight);

            KO_CV_RL_GUI = new KO_CornverVariables(VehicleCorner.RearLeft);

            KO_CV_RR_GUI = new KO_CornverVariables(VehicleCorner.RearRight);

            KO_Central = new KO_CentralVariables();

            KO_SimParams = new KO_SimulationParams();

            //xuc_KO = new XUC_KinematicOptimization();

            //Param_Imp_Form = new ParametersAndImportance();

            Design_Form = new DesignForm();

            Design_Form.Set_KO_Variables(ref KO_Central, ref KO_CV_FL_GUI, ref KO_CV_FR_GUI, ref KO_CV_RL_GUI, ref KO_CV_RR_GUI);

            Design_Form.Set_KO_SimulationParams(ref KO_SimParams);

            /////<summary>Passing the <see cref="KO_CornverVariables"/> Objects of the 4 corners to the <see cref="Param_Imp_Form"/></summary>
            //Param_Imp_Form.SetCornerVariables(KO_CV_FL_GUI, KO_CV_FR_GUI, KO_CV_RL_GUI, KO_CV_RR_GUI);

        }


    }
}

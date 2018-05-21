using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class SetupChange
    {
        /// <summary>
        /// Setup Change Name
        /// </summary>
        public string SetupChangeName { get; set; }
        /// <summary>
        /// Setup Change ID
        /// </summary>
        public int SetupChangeID { get; set; }
        /// <summary>
        /// Static Setup Change Counter
        /// </summary>
        public static int SetupChangeCounter { get; set; }
        /// <summary>
        /// List of Setup Change Objects
        /// </summary>
        public static List<SetupChange> List_SetupChange = new List<SetupChange>();
        /// <summary>
        /// GUI Object of the Setup Change 
        /// </summary>
        public SetupChange_GUI setupChangeGUI;

        public SetupChange_CornerVariables setupChange_CV_Master = new SetupChange_CornerVariables();

        public SetupChange_CornerVariables setupChange_CV_FL = new SetupChange_CornerVariables();

        public SetupChange_CornerVariables setupChange_CV_FR = new SetupChange_CornerVariables();

        public SetupChange_CornerVariables setupChange_CV_RL = new SetupChange_CornerVariables();

        public SetupChange_CornerVariables setupChange_CV_RR = new SetupChange_CornerVariables();

        public SetupChange() { }

        public SetupChange(string _setupChangeName, int _setupChangeID)
        {
            SetupChangeName = _setupChangeName + _setupChangeID;
            SetupChangeID = _setupChangeID;
        }

        public void InitOrEditDeltas(SetupChange_GUI _setupChangeGUI, SetupChange_CornerVariables _setupChange_CV_Master_GUI, int identifier)
        {
            setupChangeGUI = _setupChangeGUI;

            setupChange_CV_Master = new SetupChange_CornerVariables();

            setupChange_CV_Master = _setupChange_CV_Master_GUI;

            if (identifier == 1)
            {
                setupChange_CV_FL = null;
                setupChange_CV_FL = setupChange_CV_Master;
            }
            else if (identifier == 2)
            {
                setupChange_CV_FR = null;
                setupChange_CV_FR = setupChange_CV_Master;
            }
            else if (identifier == 3)
            {
                setupChange_CV_RL = null;
                setupChange_CV_RL = setupChange_CV_Master;
            }
            else if (identifier ==4)
            {
                setupChange_CV_RR = null;
                setupChange_CV_RR = setupChange_CV_Master;
            }
        }

    }
}

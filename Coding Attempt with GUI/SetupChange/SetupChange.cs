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

        /// <summary>
        /// Master Object of the <see cref="SetupChange_CornerVariables"/> Class.
        /// Called Master because it us used generically for all the corners and then passed to the right corner at the end of everything
        /// </summary>
        public SetupChange_CornerVariables setupChange_CV_Master = new SetupChange_CornerVariables();
        /// <summary>
        /// Front Left Object of the <see cref="SetupChange_CornerVariables"/>
        /// </summary>
        public SetupChange_CornerVariables setupChange_CV_FL = new SetupChange_CornerVariables();
        /// <summary>
        /// Front Right Object of the <see cref="SetupChange_CornerVariables"/>
        /// </summary>
        public SetupChange_CornerVariables setupChange_CV_FR = new SetupChange_CornerVariables();
        /// <summary>
        /// Rear Left Object of the <see cref="SetupChange_CornerVariables"/>
        /// </summary>
        public SetupChange_CornerVariables setupChange_CV_RL = new SetupChange_CornerVariables();
        /// <summary>
        /// Rear Right Object of the <see cref="SetupChange_CornerVariables"/>
        /// </summary>
        public SetupChange_CornerVariables setupChange_CV_RR = new SetupChange_CornerVariables();

        public SetupChange() { }

        public SetupChange(string _setupChangeName, int _setupChangeID)
        {
            SetupChangeName = _setupChangeName + _setupChangeID;
            SetupChangeID = _setupChangeID;
        }

        /// <summary>
        /// <para>Method to Modify the <see cref="SetupChange_CornerVariables"/> objects of this</para>
        /// <para>This method is called immediately as a change is made in the <see cref="XUC_SetupChange_Children"/></para>
        /// </summary>
        /// <param name="_setupChangeGUI">GUI Object of <see cref="SetupChange_GUI"/></param>
        /// <param name="_setupChange_CV_Master_GUI"><see cref="SetupChange_CornerVariables"/> object of the <see cref="SetupChange_GUI"/> class which is going to be passed onto the <see cref="SetupChange_CornerVariables"/>
        /// object of this class</param>
        /// <param name="identifier"></param>
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
            else if (identifier == 4)
            {
                setupChange_CV_RR = null;
                setupChange_CV_RR = setupChange_CV_Master;
            }
        }

    }

    /// <summary>
    /// Enum which has all the Setup Possibilities.  
    /// </summary>
    public enum AvailableSetups
    {
        Caster_KPI,
        Camber,
        Toe,
        BumpSteer
    };

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// Class which holds the delta (or change) of each parameter which the user has requested along with the booleans which dictate which parameters are to be maintained constant and which parameters the user will allow to be changed 
    /// when making the Setup Change 
    /// </summary>
    public class SetupChange_CornerVariables
    {

        /// <summary>
        /// Change in KPI
        /// </summary>
        public double deltaKPI;
        /// <summary>
        /// Boolean to Determine if the user has requested a KPI Change
        /// </summary>
        public bool KPIChangeRequested;
        /// <summary>
        /// 
        /// </summary>
        public double deltaTopFrontArm;
        /// <summary>
        /// 
        /// </summary>
        public double deltaTopRearArm;
 

        /// <summary>
        /// Change in Camber
        /// </summary>
        public double deltaCamber;
        /// <summary>
        /// Boolean to Determine if the user has requested a Camber Change
        /// </summary>
        public bool CamberChangeRequested;
        /// <summary>
        /// Change in number of Shims 
        /// </summary>
        public int deltaCamberShims;
        /// <summary>
        /// Thickness of each Shim
        /// </summary>
        public double camberShimThickness;
        /// <summary>
        /// Change in Length of the Vector Representing the Shims
        /// </summary>
        public double deltaCamberShimVectorLength;


        /// <summary>
        /// Change in Caster
        /// </summary>
        public double deltaCaster;
        /// <summary>
        /// Boolean to Determine if the user has requested a Caster Change
        /// </summary>
        public bool CasterChangeRequested;
        /// <summary>
        /// 
        /// </summary>
        public double deltaBottmFrontArm;
        /// <summary>
        /// 
        /// </summary>
        public double deltaBottomRearArm;


        /// <summary>
        /// Change in Toe
        /// </summary>
        public double deltaToe;
        /// <summary>
        /// Boolean to Determine if the user has requested a Toe Change
        /// </summary>
        public bool ToeChangeRequested;
        /// <summary>
        /// Change in the Toe Link Length
        /// </summary>
        public double deltaToeLinkLength;
        /// <summary>
        /// 
        /// </summary>
        public double deltaToeShims;
        /// <summary>
        /// 
        /// </summary>
        public double ToeShimThickness;


        /// <summary>
        /// Change in Ride Height
        /// </summary>
        public double deltaRideHeight;
        /// <summary>
        /// Boolean to Determine if the user has requested a Ride Height Change Change
        /// </summary>
        public bool RHIChangeRequested;
        /// <summary>
        /// 
        /// </summary>
        public double deltaPushrod;

        public bool RideHeightChanged { get; set; } = false;

        public bool BumpSteerChangeRequested;


        /// <summary>
        /// Boolean to determine if KPI is to be constant during the iterations
        /// </summary>
        public bool constKPI;
        /// <summary>
        /// Boolean to determine if Camber is to be constant during the iterations
        /// </summary>
        public bool constCamber;
        /// <summary>
        /// Boolean to determine if Caster is to be constant during the iterations
        /// </summary>
        public bool constCaster;
        /// <summary>
        /// Boolean to determine if Toe is to be constant during the iterations
        /// </summary>
        public bool constToe;
        /// <summary>
        /// Boolean to determine if Ride Height is to be constant during the iterations
        /// </summary>
        public bool constRideHeight;

        public bool constBumpSteer;



        /// <summary>
        /// Integer to determine the Number of Iterations which the user wants to perform for Camber. 
        /// <para> Not yet taken from the user though</para>
        /// </summary>
        public int iterationsCamber = 50;
        /// <summary>
        /// Integer to determine the Number of Iterations which the user wants to perform for Camber. 
        /// <para> Not yet taken from the user though</para>
        /// </summary>
        public int iterationsCaster= 50;
        /// <summary>
        /// Integer to determine the Number of Iterations which the user wants to perform for Camber. 
        /// <para> Not yet taken from the user though</para>
        /// </summary>
        public int iterationsKPI = 50;
        /// <summary>
        /// Integer to determine the Number of Iterations which the user wants to perform for Camber. 
        /// <para> Not yet taken from the user though</para>
        /// </summary>
        public int iterationsToe = 50;
        /// <summary>
        /// Integer to determine the Number of Iterations which the user wants to perform for Camber. 
        /// <para> Not yet taken from the user though</para>
        /// </summary>
        public int iterationsLinkLength = 50;

        public bool LinkLengthChanged { get; set; } = false;

        /// <summary>
        /// <para>1-> TopFrontArm</para> 
        /// <para>2-> TopRearArm</para> 
        /// <para>3-> BottomFrontArm</para> 
        /// <para>4-> BottomRearArm</para> 
        /// </summary>
        public List<int> LinkLengthsWhichHaveNotChanged { get; set; }/* = new List<int>();*/

        /// <summary>
        /// String representing the name of the variable
        /// </summary>
        public string cornerName;


        /// <summary>
        /// Enum to decide whether the Camber adjustment will be direct or indirect.
        /// </summary>
        public AdjustmentType camberAdjustmentType = new AdjustmentType();
        /// <summary>
        /// Enum to decide which of the 4 available <see cref="AdjustmentTools"/> will be used to adjust the Camber
        /// </summary>
        public AdjustmentTools camberAdjustmentTool = AdjustmentTools.DirectValue;

        public bool OverrideRandomSelectorForKPI { get; set; }

        /// <summary>
        /// Enum to decide whether the Toe adjustment will be direct or indirect.
        /// </summary>
        public AdjustmentType toeAdjustmentType = new AdjustmentType();
        /// <summary>
        /// Enum to decide which of the 4 available <see cref="AdjustmentTools"/> will be used to adjust the Toe
        /// </summary>
        public AdjustmentTools toeAdjustmentTool = AdjustmentTools.DirectValue;
        /// <summary>
        /// Enum to decide whether the Caster adjustment will be direct or indirect.
        /// </summary>
        public AdjustmentType casterAdjustmentType = new AdjustmentType();

        public bool OverrideRandomSelectorForCaster { get; set; }

        /// <summary>
        /// Enum to decide which of the 4 available <see cref="AdjustmentTools"/> will be used to adjust the Caster
        /// </summary>
        public AdjustmentTools casterAdjustmentTool = AdjustmentTools.DirectValue;
        /// <summary>
        /// Enum to decide whether the KPI adjustment will be direct or indirect.
        /// </summary>
        public AdjustmentType kpiAdjustmentType = new AdjustmentType();
        /// <summary>
        /// Enum to decide which of the 4 available <see cref="AdjustmentTools"/> will be used to adjust the KPI
        /// </summary>
        public AdjustmentTools kpiAdjustmentTool = AdjustmentTools.DirectValue;
        /// <summary>
        /// Enum to decide whether the Ride Height adjustment will be direct or indirect.
        /// </summary>
        public AdjustmentType rideheightAdjustmentType = new AdjustmentType();
        /// <summary>
        /// Enum to decide which of the 4 available <see cref="AdjustmentTools"/> will be used to adjust the RideHeight
        /// </summary>
        public AdjustmentTools rideheightAdjustmentTool = AdjustmentTools.PushrodLength;
        /// <summary>
        /// This Dictionary will contain the <see cref="AdjustmentTools"/> option which the user has selected FOR EACH Setup Change.
        /// </summary>
        public Dictionary<string, AdjustmentTools> AdjToolsDictionary = new Dictionary<string, AdjustmentTools>();

        public Dictionary<string, Opt_AdjToolParams> Caster_KPI_Adj;

        public Dictionary<string, Opt_AdjToolParams> Camber_Adj;

        public Dictionary<string, Opt_AdjToolParams> Toe_Adj;

        public Dictionary<string, Opt_AdjToolParams> BumpSteer_Adj;

        public Dictionary<string, Dictionary<string, Opt_AdjToolParams>> Master_Adj;

        public SetupChange_CornerVariables()
        {
            Caster_KPI_Adj = new Dictionary<string, Opt_AdjToolParams>();

            Camber_Adj = new Dictionary<string, Opt_AdjToolParams>();

            Toe_Adj = new Dictionary<string, Opt_AdjToolParams>();

            BumpSteer_Adj = new Dictionary<string, Opt_AdjToolParams>();

            Master_Adj = new Dictionary<string, Dictionary<string, Opt_AdjToolParams>>();
        }

        /// <summary>
        /// Method to add the user selected <see cref="AdjustmentTools"/>
        /// </summary>
        /// <param name="camberTool"></param>
        /// <param name="toeTool"></param>
        /// <param name="casterTool"></param>
        /// <param name="kpiTool"></param>
        /// <param name="rideHeightTool"></param>
        public void InitAdjustmentToolsDictionary(AdjustmentTools camberTool, AdjustmentTools toeTool, AdjustmentTools casterTool, AdjustmentTools kpiTool, AdjustmentTools rideHeightTool)
        {
            AdjToolsDictionary.Add("CamberChange", camberTool);
            AdjToolsDictionary.Add("ToeChange", toeTool);
            AdjToolsDictionary.Add("CasterChange", casterTool);
            AdjToolsDictionary.Add("KPIChange", kpiTool);
            AdjToolsDictionary.Add("RideHeightChange", rideHeightTool);
            //LinkLengthsWhichHaveNotChanged = new List<int>();
            //LinkLengthsWhichHaveNotChanged.AddRange(new int[] { 1, 2, 3, 4 });

        }



    }

    

    /// <summary>
    /// Enum to decide whether the adjustment will be direct or indirect. 
    /// Example - Toe change given in term of Angle is DIRECT whereas Toe change given in terms of Toe Link length or Toe Shim increase/decrease is indrect
    /// </summary>
    public enum AdjustmentType
    {
        Direct,
        Indirect
    };
}

﻿using System;
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
        public double camberShimThickness = 1;
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

        public bool monitorBumpSteer;



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
        





        /// <summary>
        /// Dictionary which holds the Adjustment Tools availble to the user to adjust Caster and/or KPI
        /// </summary>
        public Dictionary<string, SetupChange_AdjToolParams> Caster_KPI_Adj;
        /// <summary>
        /// Dictionary which holds the Adjustment Tools availble to the user to adjust Camber
        /// </summary>
        public Dictionary<string, SetupChange_AdjToolParams> Camber_Adj;
        /// <summary>
        /// Dictionary which holds the Adjustment Tools availble to the user to adjust Toe
        /// </summary>
        public Dictionary<string, SetupChange_AdjToolParams> Toe_Adj;
        /// <summary>
        /// Dictionary which holds the Adjustment Tools availble to the user to adjust Bump Steer
        /// </summary>
        public Dictionary<string, SetupChange_AdjToolParams> BumpSteer_Adj;
        /// <summary>
        /// Object of the <see cref="CustomBumpSteerParams"/> Class
        /// </summary>
        public CustomBumpSteerParams BS_Params;


        /// <summary>
        /// Master Dictionary which holds all the above Dictionary depending upon whether they are to be SETUP or not 
        /// </summary>
        public Dictionary<string, Dictionary<string, SetupChange_AdjToolParams>> Master_Adj;





        public SetupChange_CornerVariables()
        {
            Caster_KPI_Adj = new Dictionary<string, SetupChange_AdjToolParams>();

            Camber_Adj = new Dictionary<string, SetupChange_AdjToolParams>();

            Toe_Adj = new Dictionary<string, SetupChange_AdjToolParams>();

            BumpSteer_Adj = new Dictionary<string, SetupChange_AdjToolParams>();

            Master_Adj = new Dictionary<string, Dictionary<string, SetupChange_AdjToolParams>>();

            BS_Params = new CustomBumpSteerParams();
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


        public SetupChange_CornerVariables Clone()
        {
            SetupChange_CornerVariables tempCV = new SetupChange_CornerVariables();

            tempCV.deltaKPI = this.deltaKPI;
            tempCV.KPIChangeRequested = this.KPIChangeRequested;
            tempCV.deltaTopFrontArm = this.deltaTopFrontArm;
            tempCV.deltaTopRearArm = this.deltaTopRearArm;


            tempCV.deltaCamber = this.deltaCamber;
            tempCV.CamberChangeRequested = this.CamberChangeRequested;
            tempCV.deltaCamberShims = this.deltaCamberShims;
            tempCV.camberShimThickness = this.camberShimThickness;
            tempCV.deltaCamberShimVectorLength = this.deltaCamberShimVectorLength;

            tempCV.deltaCaster = this.deltaCaster;
            tempCV.CasterChangeRequested = this.CasterChangeRequested;
            tempCV.deltaBottmFrontArm = this.deltaBottmFrontArm;
            tempCV.deltaBottomRearArm = this.deltaBottomRearArm;


            tempCV.deltaToe = this.deltaToe;
            tempCV.ToeChangeRequested = this.ToeChangeRequested;
            tempCV.deltaToeLinkLength = this.deltaToeLinkLength;
            tempCV.deltaToeShims = this.deltaToeShims;
            tempCV.ToeShimThickness = this.ToeShimThickness;


            tempCV.deltaRideHeight = this.deltaRideHeight;
            tempCV.RHIChangeRequested = this.RHIChangeRequested;
            tempCV.deltaPushrod = this.deltaPushrod;
            tempCV.RideHeightChanged = this.RideHeightChanged;

            tempCV.BumpSteerChangeRequested = this.BumpSteerChangeRequested;


            tempCV.constKPI = this.constKPI;
            tempCV.constCamber = this.constCamber;
            tempCV.constCaster = this.constCaster;
            tempCV.constToe = this.constToe;
            tempCV.constRideHeight = this.constRideHeight;

            tempCV.monitorBumpSteer = this.monitorBumpSteer;



            tempCV.iterationsCamber = this.iterationsCamber;
            tempCV.iterationsCaster = this.iterationsCaster;
            tempCV.iterationsKPI = this.iterationsKPI;
            tempCV.iterationsToe = this.iterationsToe;
            tempCV.iterationsLinkLength = this.iterationsLinkLength;

            tempCV.LinkLengthChanged = tempCV.LinkLengthChanged;

            tempCV.LinkLengthsWhichHaveNotChanged = tempCV.LinkLengthsWhichHaveNotChanged;

            tempCV.cornerName = this.cornerName;


            tempCV.camberAdjustmentType = this.camberAdjustmentType;
            tempCV.camberAdjustmentTool = this.camberAdjustmentTool;

            tempCV.OverrideRandomSelectorForKPI = this.OverrideRandomSelectorForKPI;

            tempCV.toeAdjustmentType = this.toeAdjustmentType;
            tempCV.toeAdjustmentTool = this.toeAdjustmentTool;
            tempCV.casterAdjustmentType = this.casterAdjustmentType;

            tempCV.OverrideRandomSelectorForCaster = this.OverrideRandomSelectorForCaster;

            tempCV.casterAdjustmentTool = this.casterAdjustmentTool;
            tempCV.kpiAdjustmentType = this.kpiAdjustmentType;
            tempCV.kpiAdjustmentTool = this.kpiAdjustmentTool;
            tempCV.rideheightAdjustmentType = this.rideheightAdjustmentType;
            tempCV.rideheightAdjustmentTool = this.rideheightAdjustmentTool;
            tempCV.AdjToolsDictionary = this.AdjToolsDictionary;
            

            tempCV.Caster_KPI_Adj = this.Caster_KPI_Adj;
            tempCV.Camber_Adj = this.Camber_Adj;
            tempCV.Toe_Adj = this.Toe_Adj;
            tempCV.BumpSteer_Adj = this.BumpSteer_Adj;
            tempCV.BS_Params = this.BS_Params;


            tempCV.Master_Adj = this.Master_Adj;

            return tempCV;
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

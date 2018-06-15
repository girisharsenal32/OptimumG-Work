using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// <para>This Class Houses all the Items pertaining to a Corner of the Vehicle</para>
    /// <para>Items Include</para>
    /// <para>-> INPUTS :- All User Requested Suspension Parameters, their Static Values/Curves etc. and their Importance</para>
    /// <para>-> Adjusters :- All User Specified Adjusters</para>
    /// <para>-> Vehicle Corner Components</para>
    /// </summary>
    public class KO_CornverVariables
    {
        #region ---Declarations---

        public VehicleCorner VCorner { get; set; }

        public int Identifier { get; set; }

        #region --Suspension Parameters - Parameters Requested by the User--
        /// <summary>
        /// <para>Master <see cref="Dictionary{String, KO_AdjToolParams}"/> which holds ALL the coordinate information of the Adjustable coordinates</para> 
        /// <para>This dictionary is crucial and will be used in the Main Optimizer Class</para>
        /// </summary>
        public Dictionary<string, KO_AdjToolParams> KO_MasterAdjs;

        /// <summary>
        /// <para><see cref="List{SuspensionParameters}"/> of ALL the parameters requested by the User</para>
        /// <para>This List is crucial and will be used in the Main Optimization Class</para>
        /// <para>---IMPORTANT--- This list will also house the <see cref="SuspensionParameters"/> in the RIGHT ORDER OF IMPORTANCE</para>
        /// </summary>
        public List<SuspensionParameters> KO_ReqParams;

        /// <summary>
        /// <para><see cref="Dictionary{SuspensionParameters, Double}"/> which holds the IMportance of each of the <see cref="SuspensionParameters"/></para>
        /// </summary>
        public Dictionary<SuspensionParameters, double> KO_ReqParams_Importance;
        #endregion

        #region Suspension Parameters - Parameter's Input Values provided by User

        //--Parameter Variation Curves--

        /// <summary>
        /// Object of the <see cref="CustomBumpSteerParams"/> which contains information regarding the Custom Curve of BUmp Steer which the user has generated
        /// </summary>
        public BumpSteerCurve BumpSteerCurve { get; set; }

        /// <summary>
        /// Object of the <see cref="CustomCamberCurve"/> which contains information regarding the Custom Curve of Camber which the user has generated
        /// </summary>
        public CustomCamberCurve CamberCurve { get; set; }

        #endregion

        #region --Suspension Inputs provided by the User---

        /// <summary>
        /// Caster Angle as input by the user
        /// </summary>
        public Angle Caster { get; set; }

        /// <summary>
        /// KPI Angle as input by the user
        /// </summary>
        public Angle KPI { get; set; }

        /// <summary>
        /// Scrub Radius as input by the user
        /// </summary>
        public double ScrubRadius { get; set; }

        /// <summary>
        /// Caster or Mechanical Trail as input by the user
        /// </summary>
        public double CasterTrail { get; set; }

        /// <summary>
        /// IC Heignt in the FRONT VIEW as input by the user 
        /// </summary>
        public double ICHeight_FV { get; set; }

        /// <summary>
        /// Virtual Swing Arm Lenght in the FRONT VIEW input by the user
        /// </summary>
        public double VSAL_FV { get; set; }

        /// <summary>
        /// IC Height in the SIDE VIEW as input by the user
        /// </summary>
        public double ICHeight_SV { get; set; }

        /// <summary>
        /// Virtual Swing Arm Length in the SIDE VIEW as input by the user
        /// </summary>
        public double VSAL_SV { get; set; }


        #endregion

        #region --Vehicle Corner Components--
        /// <summary>
        /// Object of the <see cref="VehicleCornerParams"/> Class containing ALL the Front Left Corner Compontnents
        /// </summary>
        public VehicleCornerParams VCornerParams { get; set; }
        #endregion

        

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public KO_CornverVariables()
        {
            KO_MasterAdjs = new Dictionary<string, KO_AdjToolParams>();

            KO_ReqParams = new List<SuspensionParameters>();

            KO_ReqParams_Importance = new Dictionary<SuspensionParameters, double>();
        }

        /// <summary>
        /// Method to Intialze the Corner Components of a given corner of a Vehicle
        /// </summary>
        /// <param name="_vehicle">The <see cref="Vehicle"/> class' object</param>
        /// <param name="_vCorner">Corner of the Vhicle</param>
        /// <param name="_numberOfIterations">Number of iterations that the Kinematic Solver (<see cref="DoubleWishboneKinematicsSolver"/></param> or <see cref="McPhersonKinematicsSolver"/> is going to 
        /// run for 
        /// <returns>Returns an object of the <see cref="VehicleCornerParams"/> Class</returns>
        public VehicleCornerParams Initialize_VehicleCornerParams(Vehicle _vehicle, VehicleCorner _vCorner, int _numberOfIterations)
        {
            Dictionary<string, object> tempVehicleParams = VehicleParamsAssigner.AssignVehicleParams_PreKinematicsSolver(_vCorner, _vehicle, 0);

            VehicleCornerParams vCornerParams = new VehicleCornerParams();

            ///<summary>Passing the <see cref="Dictionary{TKey, TValue}"/> of Vehicle Params's objects into the right Parameter</summary>
            vCornerParams.SCM = tempVehicleParams["SuspensionCoordinateMaster"] as SuspensionCoordinatesMaster;

            vCornerParams.SCM_Clone = new SuspensionCoordinatesMaster();
            vCornerParams.SCM_Clone.Clone(vCornerParams.SCM);

            vCornerParams.Tire = tempVehicleParams["Tire"] as Tire;

            vCornerParams.Spring = tempVehicleParams["Spring"] as Spring;

            vCornerParams.Damper = tempVehicleParams["Damper"] as Damper;

            vCornerParams.ARB = tempVehicleParams["AntirollBar"] as AntiRollBar;
            vCornerParams.ARBRate_Nmm = (double)tempVehicleParams["ARB_Rate_Nmm"];

            vCornerParams.WA = tempVehicleParams["WheelAlignment"] as WheelAlignment;

            ///<remarks>Chassis is not a part of the <see cref="VehicleCornerParams"/> and hence it is taken care of outside of this method just like the <see cref="Vehicle"/></remarks>

            vCornerParams.OC = tempVehicleParams["OutputClass"] as List<OutputClass>;

            vCornerParams.OC_BumpSteer = VehicleParamsAssigner.AssignVehicleParams_Custom_OC_BumpSteer(vCornerParams.SCM, _vCorner, _vehicle, /*Setup_CV.BS_Params.WheelDeflections.Count*/_numberOfIterations);

            vCornerParams.Identifier = (int)tempVehicleParams["Identifier"];

            vCornerParams.InitializePointsAndDictionary();

            return vCornerParams;
        }

        /// <summary>
        /// Method to Initialize the <see cref="KO_AdjToolParams.NominalCoordinates"/> of the <see cref="KO_MasterAdjs"/> Dictionary using the <see cref="VehicleCornerParams.InboardAssembly"/> and the <see cref="VehicleCornerParams.OutboardAssembly"/>
        /// </summary>
        /// <param name="_vCornerParams">Object of the <see cref="VehicleCornerParams"/> Class used to initalize the <see cref="KO_AdjToolParams.NominalCoordinates"/></param>
        public void Initialize_AdjusterCoordinates(VehicleCornerParams _vCornerParams)
        {
            foreach (string coordinate in KO_MasterAdjs.Keys)
            {
                foreach (string outboardCoord in _vCornerParams.OutboardAssembly.Keys)
                {
                    if (outboardCoord == coordinate)
                    {
                        KO_MasterAdjs[coordinate].NominalCoordinates = _vCornerParams.OutboardAssembly[outboardCoord];
                        KO_MasterAdjs[coordinate].OptimizedCoordinates= _vCornerParams.OutboardAssembly[outboardCoord];
                    }
                }

                foreach (string coordName in KO_MasterAdjs.Keys)
                {
                    foreach (string inboardCoord in _vCornerParams.InboardAssembly.Keys)
                    {
                        if (inboardCoord == coordName)
                        {
                            KO_MasterAdjs[coordName].NominalCoordinates = _vCornerParams.InboardAssembly[inboardCoord];
                            KO_MasterAdjs[coordName].OptimizedCoordinates= _vCornerParams.InboardAssembly[inboardCoord];
                        }
                    }
                }
                

            }
        }



    }
}

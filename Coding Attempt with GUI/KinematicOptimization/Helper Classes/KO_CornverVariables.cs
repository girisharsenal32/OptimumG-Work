using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public class KO_CornverVariables
    {
        /// <summary>
        /// <para>Master <see cref="Dictionary{String, KO_AdjToolParams}"/> which holds ALL the coordinate information of the Adjustable coordinates</para> 
        /// <para>This dictionary is crucial and will be used in the Main Optimizer Class</para>
        /// </summary>
        public Dictionary<string,KO_AdjToolParams> KO_MasterAdjs { get; set; }

        /// <summary>
        /// <para><see cref="List{SuspensionParameters}"/> of ALL the parameters requested by the User</para>
        /// <para>This List is crucial and will be used in the Main Optimization Class</para>
        /// <para>---IMPORTANT--- This list will also house the <see cref="SuspensionParameters"/> in the RIGHT ORDER OF IMPORTANCE</para>
        /// </summary>
        public List<SuspensionParameters> KO_ReqParams { get; set; }

        /// <summary>
        /// <para><see cref="Dictionary{SuspensionParameters, Double}"/> which holds the IMportance of each of the <see cref="SuspensionParameters"/></para>
        /// </summary>
        public Dictionary<SuspensionParameters, double> KO_ReqParams_Importance { get; set; }

        /// <summary>
        /// Object of the <see cref="VehicleCornerParams"/> Class containing ALL the Front Left Corner Compontnents
        /// </summary>
        public VehicleCornerParams VeicleParams_FL { get; set; }

        /// <summary>
        /// Object of the <see cref="VehicleCornerParams"/> Class containing ALL the Front Right Corner Compontnents
        /// </summary>
        public VehicleCornerParams VeicleParams_FR { get; set; }

        /// <summary>
        /// Object of the <see cref="VehicleCornerParams"/> Class containing ALL the Rear Left Corner Compontnents
        /// </summary>
        public VehicleCornerParams VeicleParams_RL { get; set; }

        /// <summary>
        /// Object of the <see cref="VehicleCornerParams"/> Class containing ALL the Rear Right Corner Compontnents
        /// </summary>
        public VehicleCornerParams VeicleParams_RR { get; set; }


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
        public VehicleCornerParams InitializeVehicleCornerParams(Vehicle _vehicle, VehicleCorner _vCorner, int _numberOfIterations)
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

            return vCornerParams;
        }
        



    }
}

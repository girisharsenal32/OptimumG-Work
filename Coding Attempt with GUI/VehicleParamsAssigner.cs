using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// <para>This class serves only 1 purpose:-</para>
    /// <para>To assign all the Params inside a particular Vehicle</para>
    /// </summary>
    public class VehicleParamsAssigner
    {
        static Dictionary<string, object> VehicleParams = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_vCorner"></param>
        /// <param name="_vehicle"></param>
        /// <param name="noOfOCSteps">Number of Steps that the <see cref="OutputClass"/> List is going have </param>
        /// <returns></returns>
        public static Dictionary<string, object> AssignVehicleParams_PreKinematicsSolver(VehicleCorner _vCorner, Vehicle _vehicle, int noOfOCSteps)
        {
            VehicleParams.Clear();

            _vehicle.InitializeOutputClass(noOfOCSteps);

            if (_vCorner == VehicleCorner.FrontLeft)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.sc_FL);
                VehicleParams.Add("Tire", _vehicle.tire_FL);
                VehicleParams.Add("Spring", _vehicle.spring_FL);
                VehicleParams.Add("Damper", _vehicle.damper_FL);
                VehicleParams.Add("AntirollBar", _vehicle.arb_FL);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_FL);
                VehicleParams.Add("OutputClass", _vehicle.oc_FL);
                VehicleParams.Add("WheelAlignment", _vehicle.wa_FL);
                VehicleParams.Add("Identifier", 1);
            }
            else if (_vCorner == VehicleCorner.FrontRight)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.sc_FR);
                VehicleParams.Add("Tire", _vehicle.tire_FR);
                VehicleParams.Add("Spring", _vehicle.spring_FR);
                VehicleParams.Add("Damper", _vehicle.damper_FR);
                VehicleParams.Add("AntirollBar", _vehicle.arb_FR);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_FR);
                VehicleParams.Add("OutputClass", _vehicle.oc_FR);
                VehicleParams.Add("WheelAlignment", _vehicle.wa_FR);
                VehicleParams.Add("Identifier", 2);
            }
            else if (_vCorner == VehicleCorner.RearLeft)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.sc_RL);
                VehicleParams.Add("Tire", _vehicle.tire_RL);
                VehicleParams.Add("Spring", _vehicle.spring_RL);
                VehicleParams.Add("Damper", _vehicle.damper_RL);
                VehicleParams.Add("AntirollBar", _vehicle.arb_RL);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_RL);
                VehicleParams.Add("OutputClass", _vehicle.oc_RL);
                VehicleParams.Add("WheelAlignment", _vehicle.wa_RL);
                VehicleParams.Add("Identifier", 3);
            }
            else if (_vCorner == VehicleCorner.RearRight)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.sc_RR);
                VehicleParams.Add("Tire", _vehicle.tire_RR);
                VehicleParams.Add("Spring", _vehicle.spring_RR);
                VehicleParams.Add("Damper", _vehicle.damper_RR);
                VehicleParams.Add("AntirollBar", _vehicle.arb_RR);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_RR);
                VehicleParams.Add("OutputClass", _vehicle.oc_RR);
                VehicleParams.Add("WheelAlignment", _vehicle.wa_RR);
                VehicleParams.Add("Identifier", 4);
            }

            VehicleParams.Add("Chassis", _vehicle.chassis_vehicle);

            return VehicleParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_vCorner"></param>
        /// <param name="_vehicle"></param>
        /// <param name="_opIndex"></param>
        /// <returns></returns>
        public static Dictionary<string, object> AssignVehicleParams_PostKinematicsSolver(VehicleCorner _vCorner, Vehicle _vehicle, int _opIndex)
        {
            VehicleParams.Clear();


            if (_vCorner == VehicleCorner.FrontLeft)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.oc_FL[_opIndex].scmOP);
                VehicleParams.Add("Tire", _vehicle.tire_FL);
                VehicleParams.Add("Spring", _vehicle.spring_FL);
                VehicleParams.Add("Damper", _vehicle.damper_FL);
                VehicleParams.Add("AntirollBar", _vehicle.arb_FL);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_FL);
                VehicleParams.Add("OutputClass", _vehicle.oc_FL);
                VehicleParams.Add("WheelAlignment", _vehicle.oc_FL[_opIndex].waOP);
                VehicleParams.Add("Identifier", 1);
            }
            else if (_vCorner == VehicleCorner.FrontRight)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.oc_FR[_opIndex].scmOP);
                VehicleParams.Add("Tire", _vehicle.tire_FR);
                VehicleParams.Add("Spring", _vehicle.spring_FR);
                VehicleParams.Add("Damper", _vehicle.damper_FR);
                VehicleParams.Add("AntirollBar", _vehicle.arb_FR);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_FR);
                VehicleParams.Add("OutputClass", _vehicle.oc_FR);
                VehicleParams.Add("WheelAlignment", _vehicle.oc_FR[_opIndex].waOP);
                VehicleParams.Add("Identifier", 2);
            }
            else if (_vCorner == VehicleCorner.RearLeft)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.oc_RL[_opIndex].scmOP);
                VehicleParams.Add("Tire", _vehicle.tire_RL);
                VehicleParams.Add("Spring", _vehicle.spring_RL);
                VehicleParams.Add("Damper", _vehicle.damper_RL);
                VehicleParams.Add("AntirollBar", _vehicle.arb_RL);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_RL);
                VehicleParams.Add("OutputClass", _vehicle.oc_RL);
                VehicleParams.Add("WheelAlignment", _vehicle.oc_RL[_opIndex].waOP);
                VehicleParams.Add("Identifier", 3);
            }
            else if (_vCorner == VehicleCorner.RearRight)
            {
                VehicleParams.Add("SuspensionCoordinateMaster", _vehicle.oc_RR[_opIndex].scmOP);
                VehicleParams.Add("Tire", _vehicle.tire_RR);
                VehicleParams.Add("Spring", _vehicle.spring_RR);
                VehicleParams.Add("Damper", _vehicle.damper_RR);
                VehicleParams.Add("AntirollBar", _vehicle.arb_RR);
                VehicleParams.Add("ARB_Rate_Nmm", _vehicle.ARB_Rate_Nmm_RR);
                VehicleParams.Add("OutputClass", _vehicle.oc_RR);
                VehicleParams.Add("WheelAlignment", _vehicle.oc_RR[_opIndex].waOP);
                VehicleParams.Add("Identifier", 4);
            }

            VehicleParams.Add("Chassis", _vehicle.chassis_vehicle);

            return VehicleParams;
        }

        public static List<OutputClass> AssignVehicleParams_Custom_OC_BumpSteer(VehicleCorner _vCorner, Vehicle _vehicle, int noOfOCSteps)
        {
            List<OutputClass> oc = new List<OutputClass>();

            _vehicle.InitializeOutputClass(noOfOCSteps);

            if (_vCorner == VehicleCorner.FrontLeft)
            {
                oc = _vehicle.oc_FL;
            }
            else if (_vCorner == VehicleCorner.FrontRight)
            {
                oc = _vehicle.oc_FR;
            }
            else if (_vCorner == VehicleCorner.RearLeft)
            {
                oc = _vehicle.oc_RL;
            }
            else if (_vCorner == VehicleCorner.RearRight)
            {
                oc = _vehicle.oc_RR;
            }

            return oc;

        }

    }
}

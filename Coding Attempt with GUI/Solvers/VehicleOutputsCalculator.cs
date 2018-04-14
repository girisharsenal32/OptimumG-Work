using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This static class performs calculations for the Vehicle Level Outputs such as New Wheelbase, trackwidth, roll angle etc.
    /// </summary>

    public class VehicleOutputsCalculator
    {

        #region Vehicle Outputs
        public void Solver_VehicleOutputs(Vehicle _vO, List<OutputClass> _ocFL, List<OutputClass> _ocFR, List<OutputClass> _ocRL, List<OutputClass> _ocRR, int NoOfSteps)
        {
            for (int i_VO = 0; i_VO < NoOfSteps; i_VO++)
            {
                #region Initial Vehicle CG Height
                _vO.Vehicle_CG_y = ((_vO.chassis_vehicle.SuspendedMassCoGy * _vO.SM_Vehicle * 9.81) + 
                    ((_vO.NSM_FL * 9.81 * _vO.chassis_vehicle.NonSuspendedMassFLCoGy) + (_vO.NSM_FR * 9.81 * _vO.chassis_vehicle.NonSuspendedMassFRCoGy) + 
                    (_vO.NSM_RL * 9.81 * _vO.chassis_vehicle.NonSuspendedMassRLCoGy) + (_vO.NSM_RR * 9.81 * _vO.chassis_vehicle.NonSuspendedMassRRCoGy))) / ((_vO.CW[0] + _vO.CW[1] + _vO.CW[2] + _vO.CW[3]));
                #endregion

                _vO.New_SMCoGy[i_VO] = (((_ocFL[i_VO].CW + _ocFR[i_VO].CW + _ocRL[i_VO].CW + _ocRR[i_VO].CW) * _vO.Vehicle_CG_y) - (_vO.NSM_FL * _ocFL[i_VO].New_NonSuspendedMassCoGy * 9.81) -
                      (_vO.NSM_FR * _ocFR[i_VO].New_NonSuspendedMassCoGy * 9.81) - (_vO.NSM_RL * _ocRL[i_VO].New_NonSuspendedMassCoGy * 9.81) - (_vO.NSM_RR * _ocRR[i_VO].New_NonSuspendedMassCoGy * 9.81)) / (_vO.SM_Vehicle * 9.81);
                
                #region This code is exclusively to added to handle the Anti-Roll Bar Chassis Point in the case of T type Anti-Roll Bar
                if (_vO.TARBIdentifierFront == 1)
                {
                    _ocFL[i_VO].scmOP.Q1x = _ocFR[i_VO].scmOP.Q1x = (_ocFL[i_VO].scmOP.P1x + _ocFR[i_VO].scmOP.P1x) / 2;
                    _ocFL[i_VO].scmOP.Q1y = _ocFR[i_VO].scmOP.Q1y = (_ocFL[i_VO].scmOP.P1y + _ocFR[i_VO].scmOP.P1y) / 2;
                    _ocFL[i_VO].scmOP.Q1z = _ocFR[i_VO].scmOP.Q1z = (_ocFL[i_VO].scmOP.P1z + _ocFR[i_VO].scmOP.P1z) / 2;

                }

                if (_vO.TARBIdentifierRear == 1)
                {

                    _ocRL[i_VO].scmOP.Q1x = _ocRR[i_VO].scmOP.Q1x = (_ocRL[i_VO].scmOP.P1x + _ocRR[i_VO].scmOP.P1x) / 2;
                    _ocRL[i_VO].scmOP.Q1y = _ocRR[i_VO].scmOP.Q1y = (_ocRL[i_VO].scmOP.P1y + _ocRR[i_VO].scmOP.P1y) / 2;
                    _ocRL[i_VO].scmOP.Q1z = _ocRR[i_VO].scmOP.Q1z = (_ocRL[i_VO].scmOP.P1z + _ocRR[i_VO].scmOP.P1z) / 2;
                }
                #endregion

                #region Front and Rear ARB Motion Ratio
                if (_vO.TARBIdentifierFront == 1) //  This if statement ensures that the code selects the top view for T ARB and Right Side View for U ARB
                {
                    _vO.P_FLFR1 = Math.Sqrt(Math.Pow((_vO.P1x_FL - _vO.P1x_FR), 2) + Math.Pow((_vO.P1z_FL - _vO.P1z_FR), 2));
                    _vO.P_FLFR2 = Math.Sqrt(Math.Pow((_ocFL[i_VO].scmOP.P1x - _ocFR[i_VO].scmOP.P1x), 2) + Math.Pow((_ocFL[i_VO].scmOP.P1z - _ocFR[i_VO].scmOP.P1z), 2));
                    _vO.ARB_Twist_Front = (Math.Acos(((_vO.P1x_FL - _vO.P1x_FR) * (_ocFL[i_VO].scmOP.P1x - _ocFR[i_VO].scmOP.P1x) + (_vO.P1z_FL - _vO.P1z_FR) * (_ocFL[i_VO].scmOP.P1z - _ocFR[i_VO].scmOP.P1z)) / (_vO.P_FLFR1 * _vO.P_FLFR2)));
                }
                else if (_vO.TARBIdentifierFront == 0)
                {
                    _vO.QP_FL = Math.Sqrt(Math.Pow((_ocFL[i_VO].scmOP.Q1y - _ocFL[i_VO].scmOP.P1y), 2) + Math.Pow((_ocFL[i_VO].scmOP.Q1z - _ocFL[i_VO].scmOP.P1z), 2));
                    _vO.QP_FR = Math.Sqrt(Math.Pow((_ocFR[i_VO].scmOP.Q1y - _ocFR[i_VO].scmOP.P1y), 2) + Math.Pow((_ocFR[i_VO].scmOP.Q1z - _ocFR[i_VO].scmOP.P1z), 2));
                    _vO.ARB_Twist_Front = (Math.Acos(((_ocFL[i_VO].scmOP.Q1y - _ocFL[i_VO].scmOP.P1y) * (_ocFR[i_VO].scmOP.Q1y - _ocFR[i_VO].scmOP.P1y) + (_ocFL[i_VO].scmOP.Q1z - _ocFL[i_VO].scmOP.P1z) * (_ocFR[i_VO].scmOP.Q1z - _ocFR[i_VO].scmOP.P1z)) / (_vO.QP_FL * _vO.QP_FR)));
                }

                _vO.ARB_MR_Front[i_VO] = _vO.RollAngle[i_VO] / (_vO.ARB_Twist_Front*(180/Math.PI));

                if (_vO.TARBIdentifierRear == 1) //  This if statement ensures that the code selects the top view for T ARB and Right Side View for U ARB
                {
                    _vO.P_RLRR1 = Math.Sqrt(Math.Pow((_vO.P1x_RL - _vO.P1x_RR), 2) + Math.Pow((_vO.P1z_RL - _vO.P1z_RR), 2));
                    _vO.P_RLRR2 = Math.Sqrt(Math.Pow((_ocRL[i_VO].scmOP.P1x - _ocRR[i_VO].scmOP.P1x), 2) + Math.Pow((_ocRL[i_VO].scmOP.P1z - _ocRR[i_VO].scmOP.P1z), 2));
                    _vO.ARB_Twist_Rear = (Math.Acos(((_vO.P1x_RL - _vO.P1x_RR) * (_ocRL[i_VO].scmOP.P1x - _ocRR[i_VO].scmOP.P1x) + (_vO.P1z_RL - _vO.P1z_RR) * (_ocRL[i_VO].scmOP.P1z - _ocRR[i_VO].scmOP.P1z)) / (_vO.P_RLRR1 * _vO.P_RLRR2)));
                }
                else if (_vO.TARBIdentifierRear == 0)
                {
                    _vO.QP_RL = Math.Sqrt(Math.Pow((_ocRL[i_VO].scmOP.Q1y - _ocRL[i_VO].scmOP.P1y), 2) + Math.Pow((_ocRL[i_VO].scmOP.Q1z - _ocRL[i_VO].scmOP.P1z), 2));
                    _vO.QP_RR = Math.Sqrt(Math.Pow((_ocRR[i_VO].scmOP.Q1y - _ocRR[i_VO].scmOP.P1y), 2) + Math.Pow((_ocRR[i_VO].scmOP.Q1z - _ocRR[i_VO].scmOP.P1z), 2));
                    _vO.ARB_Twist_Rear = (Math.Acos(((_ocRL[i_VO].scmOP.Q1y - _ocRL[i_VO].scmOP.P1y) * (_ocRR[i_VO].scmOP.Q1y - _ocRR[i_VO].scmOP.P1y) + (_ocRL[i_VO].scmOP.Q1z - _ocRL[i_VO].scmOP.P1z) * (_ocRR[i_VO].scmOP.Q1z - _ocRR[i_VO].scmOP.P1z)) / (_vO.QP_RL * _vO.QP_RR)));
                }

                _vO.ARB_MR_Rear[i_VO] = _vO.RollAngle[i_VO] / (_vO.ARB_Twist_Rear * (180 / Math.PI));
                #endregion

                #region Calculating the Front ARB Droop Link Forces. Droop Link force considered equal to the Blade Force
                ////
                ////Front Left Droop Link Force
                //_ocFL[i_VO].ARBDroopLink = Math.Sin(_vO.ARB_Twist_Front) * _vO.arb_FL.AntiRollBarRate_Nmm;

                //double UV_O2P2xFL, UV_O2P2yFL, UV_O2P2zFL;
                //UV_O2P2xFL = (_ocFL[i_VO].scmOP.O1x - _ocFL[i_VO].scmOP.P1x) / _vO.sc_FL.ARBDroopLinkLength;
                //UV_O2P2yFL = (_ocFL[i_VO].scmOP.O1y - _ocFL[i_VO].scmOP.P1y) / _vO.sc_FL.ARBDroopLinkLength;
                //UV_O2P2zFL = (_ocFL[i_VO].scmOP.O1z - _ocFL[i_VO].scmOP.P1z) / _vO.sc_FL.ARBDroopLinkLength;

                //_ocFL[i_VO].ARBDroopLink_x = _ocFL[i_VO].ARBDroopLink * UV_O2P2xFL;
                //_ocFL[i_VO].ARBDroopLink_y = _ocFL[i_VO].ARBDroopLink * UV_O2P2yFL;
                //_ocFL[i_VO].ARBDroopLink_z = _ocFL[i_VO].ARBDroopLink * UV_O2P2zFL;

                ////
                //// Front Right Droop Link Force
                //_ocFR[i_VO].ARBDroopLink = Math.Sin(_vO.ARB_Twist_Front) * _vO.arb_FR.AntiRollBarRate_Nmm;

                //double UV_O2P2xFR, UV_O2P2yFR, UV_O2P2zFR;
                //UV_O2P2xFR = (_ocFR[i_VO].scmOP.O1x - _ocFR[i_VO].scmOP.P1x) / _vO.sc_FR.ARBDroopLinkLength;
                //UV_O2P2yFR = (_ocFR[i_VO].scmOP.O1y - _ocFR[i_VO].scmOP.P1y) / _vO.sc_FR.ARBDroopLinkLength;
                //UV_O2P2zFR = (_ocFR[i_VO].scmOP.O1z - _ocFR[i_VO].scmOP.P1z) / _vO.sc_FR.ARBDroopLinkLength;

                //_ocFR[i_VO].ARBDroopLink_x = _ocFR[i_VO].ARBDroopLink * UV_O2P2xFR;
                //_ocFR[i_VO].ARBDroopLink_y = _ocFR[i_VO].ARBDroopLink * UV_O2P2yFR;
                //_ocFR[i_VO].ARBDroopLink_z = _ocFR[i_VO].ARBDroopLink * UV_O2P2zFR;
                #endregion

                #region Calculating the Rear ARB Droop Link Forces. Droop Link force considered equal to the Blade Force
                ////
                //// Rear Left Droop Link Force
                //_ocRL[i_VO].ARBDroopLink = Math.Sin(_vO.ARB_Twist_Rear) * _vO.arb_RL.AntiRollBarRate_Nmm;

                //double UV_O2P2xRL, UV_O2P2yRL, UV_O2P2zRL;
                //UV_O2P2xRL = (_ocRL[i_VO].scmOP.O1x - _ocRL[i_VO].scmOP.P1x) / _vO.sc_RL.ARBDroopLinkLength;
                //UV_O2P2yRL = (_ocRL[i_VO].scmOP.O1y - _ocRL[i_VO].scmOP.P1y) / _vO.sc_RL.ARBDroopLinkLength;
                //UV_O2P2zRL = (_ocRL[i_VO].scmOP.O1z - _ocRL[i_VO].scmOP.P1z) / _vO.sc_RL.ARBDroopLinkLength;

                //_ocRL[i_VO].ARBDroopLink_x = _ocRL[i_VO].ARBDroopLink * UV_O2P2xRL;
                //_ocRL[i_VO].ARBDroopLink_y = _ocRL[i_VO].ARBDroopLink * UV_O2P2yRL;
                //_ocRL[i_VO].ARBDroopLink_z = _ocRL[i_VO].ARBDroopLink * UV_O2P2zRL;

                ////
                //// Rear Right Droo Link Force
                //_ocRR[i_VO].ARBDroopLink = Math.Sin(_vO.ARB_Twist_Rear) * _vO.arb_RR.AntiRollBarRate_Nmm;

                //double UV_O2P2xRR, UV_O2P2yRR, UV_O2P2zRR;
                //UV_O2P2xRR = (_ocRR[i_VO].scmOP.O1x - _ocRR[i_VO].scmOP.P1x) / _vO.sc_RR.ARBDroopLinkLength;
                //UV_O2P2yRR = (_ocRR[i_VO].scmOP.O1y - _ocRR[i_VO].scmOP.P1y) / _vO.sc_RR.ARBDroopLinkLength;
                //UV_O2P2zRR = (_ocRR[i_VO].scmOP.O1z - _ocRR[i_VO].scmOP.P1z) / _vO.sc_RR.ARBDroopLinkLength;

                //_ocRR[i_VO].ARBDroopLink_x = _ocRR[i_VO].ARBDroopLink * UV_O2P2xRR;
                //_ocRR[i_VO].ARBDroopLink_y = _ocRR[i_VO].ARBDroopLink * UV_O2P2yRR;
                //_ocRR[i_VO].ARBDroopLink_z = _ocRR[i_VO].ARBDroopLink * UV_O2P2zRR;

                #endregion 
            }
        } 
        #endregion
    }
}

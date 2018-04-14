using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Coding_Attempt_with_GUI
{
    public static class Default_Values
    {

        #region Default values of Tire Item
        public static void TireDefaultValues2(TireGUI _tGUI)
        {
            _tGUI.TireDataTableGUI.Rows.Add("Tire Stiffness (N/mm)", 117.43);
            _tGUI.TireDataTableGUI.Rows.Add("Tire Width (mm)", 157.48);
        }
        #endregion

        #region Default values of Spring Item
        public static void SpringDefaultValues2(SpringGUI _sGUI)
        {
            _sGUI.SpringDataTableGUI.Rows.Add("Spring Rate (N/mm) ", 43.75);
            _sGUI.SpringDataTableGUI.Rows.Add("Spring Preload (mm) ", 3.5);
            _sGUI.SpringDataTableGUI.Rows.Add("Spring Freelength (mm) ", 122);
        }

        #endregion

        #region Default values of Damper Item
        public static void DamperDefaultValues2(DamperGUI _dGUI)
        {
            _dGUI.DamperDataTableGUI.Rows.Add("Damper Gas Pressure (MPa) ", 0.5);
            _dGUI.DamperDataTableGUI.Rows.Add("Damper Shaft Diameter (mm) ", 8);
            _dGUI.DamperDataTableGUI.Rows.Add("Damper Free Length (mm)", 260);
            _dGUI.DamperDataTableGUI.Rows.Add("Damper Stroke Length (mm)", 200);
        }

        #endregion

        #region Default values of Anti-Roll Bar Item
        public static void ARBDefaultValues2(AntiRollBarGUI _arbGUI)
        {
            _arbGUI.ARBDataTableGUI.Rows.Add("Anti-Roll Bar Stiffness (N-m/deg) ", 7);
        }
        #endregion

        #region Default values of Chassis Item
        public static class ChassisDefaultValues
        {

            public static void MassAndSMCoGDefaultValues(ChassisGUI _cGUI, Kinematics_Software_New r1)
            {
                _cGUI.ChassisDataTableGUI.Rows.Add("Suspended Mass Properties", 216, -770, 0, 298 + Convert.ToDouble(r1.InputOriginY.Text));
            }

            public static void FRONTLEFTNonSuspendedMassCoGValues(ChassisGUI _cGUI, Kinematics_Software_New r1)
            {

                if (r1.DoubleWishboneFront_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add(" Front Left Non Suspended Mass", 10, 0.71, 547.99, 149.5 + Convert.ToDouble(r1.InputOriginY.Text));
                }

                else if (r1.McPhersonFront_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add(" Front Left Non Suspended Mass", 10, 0.47, 363.89, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                }

            }

            public static void FRONTRIGHTNonSuspendedMassCoGValues(ChassisGUI _cGUI, Kinematics_Software_New r1)
            {
                if (r1.DoubleWishboneFront_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add("Front Right Non Suspended Mass", 10, 0.71, -547.99, 149.5 + Convert.ToDouble(r1.InputOriginY.Text));
                }

                else if (r1.McPhersonFront_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add("Front Right Non Suspended Mass", 10, 0.47, -363.89, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                }

            }


            public static void REARLEFTNonSuspendedMassCoGValues(ChassisGUI _cGUI, Kinematics_Software_New r1)
            {
                if (r1.DoubleWishboneRear_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add("Rear Left Non Suspended Mass", 12, -1551, 498.75, 147.5 + Convert.ToDouble(r1.InputOriginY.Text));
                }


                else if (r1.McPhersonRear_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add("Rear Left Non Suspended Mass", 12, -1029.89, 331.18, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                }
            }


            public static void REARRIGHTNonSuspendedMassCoGValues(ChassisGUI _cGUI, Kinematics_Software_New r1)
            {

                if (r1.DoubleWishboneRear_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add("Rear Right Non Suspended Mass", 12, -1551, -498.75, 147.5 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                else if (r1.McPhersonRear_VehicleGUI == 1)
                {
                    _cGUI.ChassisDataTableGUI.Rows.Add("Rear Right Non Suspended Mass", 12, -1029.89, -331.18, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                }

            }
        } 
        #endregion

        #region Default values of Wheel Alignment
        public static void WheelAlignmentDefaultValues2(WheelAlignmentGUI _wGUI)
        {
            _wGUI.WADataTableGUI.Rows.Add("Static Camber (deg) ", -0.8);
            _wGUI.WADataTableGUI.Rows.Add("Static Toe (deg) ", -0.5);
            _wGUI.WADataTableGUI.Rows.Add("Steering Ratio (mm/revolution)", 90);
        }
        #endregion

        #region Default values of Front Left Suspension Coordinate Item
        public static class FRONTLEFTSuspensionDefaultValues
        {
            #region Populating the SCFL Data Table for Double Wishbone
            public static void DoubleWishBone(Kinematics_Software_New r1, SuspensionCoordinatesFrontGUI _scflGUI)
            {
                 #region Lower Front Chassis - Point D
                _scflGUI.SCFLDataTableGUI.Rows.Add("Lower Front Chassis", 105, 221.21, 32.17 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion
                
                #region Lower Rear Chassis - Poing C
                _scflGUI.SCFLDataTableGUI.Rows.Add("Lower Rear Chassis", -220.68, 239.68, 35.38 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Front Chassis - Point A
                _scflGUI.SCFLDataTableGUI.Rows.Add("Upper Front Chassis", 105, 235.73, 154.32 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Rear Chassis - Point B
                _scflGUI.SCFLDataTableGUI.Rows.Add("Upper Rear Chassis", -223.02, 237.07, 134.39 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Bell Crank Pivot - Point I
                if (r1.PushrodFront_VehicleGUI == 1) { _scflGUI.SCFLDataTableGUI.Rows.Add("Bell Crank Pivot", -6.73, 240.91, 497 + Convert.ToDouble(r1.InputOriginY.Text)); }
                else if (r1.PullrodFront_VehicleGUI == 1) { _scflGUI.SCFLDataTableGUI.Rows.Add("Bell Crank Pivot", -6.73, 240.91, 30 + Convert.ToDouble(r1.InputOriginY.Text)); }
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 60.8, 284, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 60.8, 284, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 98.061, 0, 459.022 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 98.061, 0, 459.022 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Steering Link Chassis - Point N
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Link Chassis", 60.8, 232.12, 64.4 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Pinion - Point Pin1
                _scflGUI.SCFLDataTableGUI.Rows.Add("Pinion Centre", 60.8, 0, 64.4 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Universal Joint 2 - UV2
                if (r1.NoOfCouplings_VehicleGUI == 2)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Pinion Universal Joint", -20, 0, 90.774 + Convert.ToDouble(r1.InputOriginY.Text)); 
                }
                #endregion

                #region Universal Joint 1 - UV1 
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Shaft Universal Joint", -212.6, 0, 458 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Mount Chassis - STC1
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Shaft Support Chassis", -280, 0, 480 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Shock Mount - Point JO
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Shock Mount", -6.73, 36, 539 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Shock Mount", -6.73, 36, 72 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Shock Mount", 96.676, 63.115, 533.974 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Shock Mount", 96.676, 63.115, 66.74 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Ride Height Reference
                _scflGUI.SCFLDataTableGUI.Rows.Add("Ride Height Reference", 0, 100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Bell-Crank - Point J
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Bell-Crank", -6.73, 235.10, 559.74 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Bell-Crank", -6.73, 235.10, 92.4 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Bell-Crank", -29.548, 217.557, 550.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Bell-Crank", -29.548, 217.557, 83.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Pushrod Bell-Crank - Point H
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Pushrod Bell-Crank", -6.73, 298.55, 524.22 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Pullrod Bell-Crank", -6.73, 298.55, 40 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Pushrod Bell-Crank", -47.502, 289.433, 503.817 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Pullrod Bell-Crank", -47.502, 289.433, 36.817 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Anti-Roll Bar Bell-Crank - Point O
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -6.73, 277.03, 471.96 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -6.73, 277.03, 4.96 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -22.828, 224.435, 535.017 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -22.828, 224.435, 68.017 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Pushrod Upright - Point G
                if (r1.PushrodFront_VehicleGUI == 1)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Pushrod Upright", -5.58, 519.99, 232.28 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                else if (r1.PullrodFront_VehicleGUI == 1)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Pullrod Upright", -5.58, 519.99, 200 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

                #region Upper Ball Joint - Point F
                _scflGUI.SCFLDataTableGUI.Rows.Add("Upper Ball Joint", -2.2, 546.73, 218.41 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scflGUI.SCFLDataTableGUI.Rows.Add("Lower Ball Joint", 3.94, 566.57, 43.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Link", -5.57, 278.97, -11.42 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Link", 98.061, 136.833, 459.022 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                #endregion

                #region Wheel Centre - Point K
                _scflGUI.SCFLDataTableGUI.Rows.Add("Wheel Centre", 0.71, 547.99, 149.5 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Link Upright", 71.31, 586.92, 93.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scflGUI.SCFLDataTableGUI.Rows.Add("Contact Patch", -1.13, 621.35, -74.15 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Torion Bar Bottom - Point R
                if (r1.TARBFront_VehicleGUI == 1)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Torsion Bar Bottom Pivot", 0.478, 0, 429.565 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

            }
            #endregion

            #region Populating the SCFL Data table for McPherson
            public static void McPherson(SuspensionCoordinatesFrontGUI _scflGUI, Kinematics_Software_New r1)
            {
                #region Lower Front Chassis - Point D
                _scflGUI.SCFLDataTableGUI.Rows.Add("Lower Front Chassis", 69.72, 146.87, 21.3 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Point C
                _scflGUI.SCFLDataTableGUI.Rows.Add("Lower Rear Chassis", -146.53, 159.15, 23.5 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 40.37, 350, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Chassis - Point N
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Link Chassis", 40.37, 154.13, 42.7 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Pinion - Point Pin1
                _scflGUI.SCFLDataTableGUI.Rows.Add("Pinion Centre", 60.8, 0, 64.4 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Universal Joint 2 - UV2
                if (r1.NoOfCouplings_VehicleGUI == 2)
                {
                    _scflGUI.SCFLDataTableGUI.Rows.Add("Pinion Universal Joint", -20, 0, 90.774 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

                #region Universal Joint 1 - UV1 
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Shaft Universal Joint", -212.6, 0, 458 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Mount Chassis - STC1
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Shaft Support Chassis", -280, 0, 480 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Shock Mount - Point JO
                _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Shock Mount", -1.46, 312.06, 384.92 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Ride Height Reference
                _scflGUI.SCFLDataTableGUI.Rows.Add("Ride Height Reference", 0, 100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Upright - Point J
                _scflGUI.SCFLDataTableGUI.Rows.Add("Damper Upright", -1.46, 363.04, 145.035 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scflGUI.SCFLDataTableGUI.Rows.Add("Lower Ball Joint", 2.61, 376.21, 28.57 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                _scflGUI.SCFLDataTableGUI.Rows.Add("Anti-Roll Bar Link", -3.69, 380.76, -7.58 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Wheel Centre - Point K
                _scflGUI.SCFLDataTableGUI.Rows.Add("Wheel Centre", 0.47, 363.89, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scflGUI.SCFLDataTableGUI.Rows.Add("Steering Link Upright", 47.35, 389.73, 61.77 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scflGUI.SCFLDataTableGUI.Rows.Add("Contact Patch", -0.75, 412.6, -75 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

            }
            #endregion

            #region Populating the DataTable for the creation of a Mapped Suspension created using Imported SM and NSM
            public static void CreateMappedSuspension(CoordinateDatabase _coordinateFL,SuspensionCoordinatesFrontGUI _scFLGUI)
            {
                for (int i = 0; i < _scFLGUI.SCFLDataTableGUI.Rows.Count; i++)
                {
                    foreach (string key in _coordinateFL.InboardPickUp.Keys)
                    {
                        if (_scFLGUI.SCFLDataTableGUI.Rows[i].Field<string>(0) == key) 
                        {
                            _scFLGUI.SCFLDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateFL.InboardPickUp[key].Position.Z);
                            _scFLGUI.SCFLDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateFL.InboardPickUp[key].Position.X);
                            _scFLGUI.SCFLDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateFL.InboardPickUp[key].Position.Y);
                            break;
                        }
                    }
                    foreach (string key in _coordinateFL.OutboardPickUp.Keys)
                    {
                        if (_scFLGUI.SCFLDataTableGUI.Rows[i].Field<string>(0) == key) 
                        {
                            _scFLGUI.SCFLDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateFL.OutboardPickUp[key].Position.Z);
                            _scFLGUI.SCFLDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateFL.OutboardPickUp[key].Position.X);
                            _scFLGUI.SCFLDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateFL.OutboardPickUp[key].Position.Y);
                            break; 
                        }
                    } 
                }
            }
            #endregion

            #region Editing the DataTable which had been created using the Map Method above
            public static void EditMappedSuspension() { } /// <remarks>Mostly won't be needing this. Aldready have a method to modify the created Suspension</remarks>
            #endregion

        } 
        #endregion

        #region Default values of Front Right Suspension Coordinate Item
        public static class FRONTRIGHTSuspensionDefaultValues
        {
            #region Populating the SCFR Data Table for Double Wishbone
            public static void DoubleWishBone(Kinematics_Software_New r1, SuspensionCoordinatesFrontRightGUI _scfrGUI)
            {
                #region Lower Front Chassis - Point D
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Lower Front Chassis", 105, -221.21, 32.17 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Poing C
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Lower Rear Chassis", -220.68, -239.68, 35.38 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Front Chassis - Point A
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Upper Front Chassis", 105, -235.73, 154.32 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Rear Chassis - Point B
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Upper Rear Chassis", -223.02, -237.07, 134.39 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Bell Crank Pivot - Point I
                if (r1.PushrodFront_VehicleGUI == 1) { _scfrGUI.SCFRDataTableGUI.Rows.Add("Bell Crank Pivot", -6.73, -240.91, 497 + Convert.ToDouble(r1.InputOriginY.Text)); }
                else if (r1.PullrodFront_VehicleGUI == 1) { _scfrGUI.SCFRDataTableGUI.Rows.Add("Bell Crank Pivot", -6.73, -240.91, 30 + Convert.ToDouble(r1.InputOriginY.Text)); }
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 60.8, -284, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 60.8, -284, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 98.061, 0, 459.022 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 98.061, 0, 459.022 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Steering Link Chassis - Point N
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Steering Link Chassis", 60.8, -232.12, 64.4 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Shock Mount - Point JO
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Shock Mount", -6.73, -36, 539 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Shock Mount", -6.73, -36, 72 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Shock Mount", 96.676, -63.115, 533.974 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Shock Mount", 96.676, -63.115, 66.74 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Ride Height Reference
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Ride Height Reference", 0, -100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Bell-Crank - Point J
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Bell-Crank", -6.73, -235.10, 559.74 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Bell-Crank", -6.73, -235.10, 92.4 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Bell-Crank", -29.548, -217.557, 550.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Bell-Crank", -29.548, -217.557, 83.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Pushrod Bell-Crank - Point H
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Pushrod Bell-Crank", -6.73, -298.55, 524.22 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Pullrod Bell-Crank", -6.73, -298.55, 40 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Pushrod Bell-Crank", -47.502, -289.433, 503.817 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Pullrod Bell-Crank", -47.502, -289.433, 36.817 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Anti-Roll Bar Bell-Crank - Point O
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -6.73, -277.03, 471.96 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -6.73, -277.03, 4.96 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    if (r1.PullrodFront_VehicleGUI == 0 && r1.PushrodFront_VehicleGUI == 1)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -22.828, -224.435, 535.017 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodFront_VehicleGUI == 1 && r1.PushrodFront_VehicleGUI == 0)
                    {
                        _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -22.828, -224.435, 68.017 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Pushrod Upright - Point G
                if (r1.PushrodFront_VehicleGUI == 1)
                {
                    _scfrGUI.SCFRDataTableGUI.Rows.Add("Pushrod Upright", -5.58, -519.99, 232.28 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                else if (r1.PullrodFront_VehicleGUI == 1)
                {
                    _scfrGUI.SCFRDataTableGUI.Rows.Add("Pullrod Upright", -5.58, -519.99, 200 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

                #region Upper Ball Joint - Point F
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Upper Ball Joint", -2.2, -546.73, 218.41 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Lower Ball Joint", 3.94, -566.57, 43.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                if (r1.UARBFront_VehicleGUI == 1)
                {
                    _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Link", -5.57, -278.97, -11.42 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                else if (r1.TARBFront_VehicleGUI == 1)
                {
                    _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Link", 98.061, -136.833, 459.022 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                #endregion

                #region Wheel Centre - Point K
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Wheel Centre", 0.71, -547.99, 149.5 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Steering Link Upright", 71.31, -586.92, 93.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Contact Patch", -1.13, -621.35, -74.15 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Torion Bar Bottom - Point R
                if (r1.TARBFront_VehicleGUI == 1)
                {
                    _scfrGUI.SCFRDataTableGUI.Rows.Add("Torsion Bar Bottom Pivot", 0.478, 0, 429.565 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

            }
            #endregion

            #region Populating the SCFR Data table for McPherson 
            public static void McPherson(SuspensionCoordinatesFrontRightGUI _scfrGUI, Kinematics_Software_New r1)
            {
                #region Lower Front Chassis - Point D
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Lower Front Chassis", 69.72, -146.87, 21.3 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Point C
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Lower Rear Chassis", -146.53, -159.15, 23.5 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", 40.37, -350, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Chassis - Point N
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Steering Link Chassis", 40.37, -154.13, 42.7 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Chassis Mount - Point JO
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Shock Mount", -1.46, -312.06, 384.92 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Ride Height Reference
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Ride Height Reference", 0, -100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Upright - Point J
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Damper Upright", -1.46, -363.04, 145.035 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Lower Ball Joint", 2.61, -376.21, 28.57 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Anti-Roll Bar Link", -3.69, -380.76, -7.58 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Wheel Centre - Point K
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Wheel Centre", 0.47, -363.89, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Steering Link Upright", 47.35, -389.73, 61.77 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scfrGUI.SCFRDataTableGUI.Rows.Add("Contact Patch", -0.75, -412.6, -75 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

            }
            #endregion

            #region Populating the DataTable for the creation of a Mapped Suspension created using Imported SM and NSM
            public static void CreateMappedSuspension(CoordinateDatabase _coordinateFR, SuspensionCoordinatesFrontRightGUI _scFRGUI)
            {
                for (int i = 0; i < _scFRGUI.SCFRDataTableGUI.Rows.Count; i++)
                {
                    foreach (string key in _coordinateFR.InboardPickUp.Keys)
                    {
                        if (_scFRGUI.SCFRDataTableGUI.Rows[i].Field<string>(0) == key)
                        {
                            _scFRGUI.SCFRDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateFR.InboardPickUp[key].Position.Z);
                            _scFRGUI.SCFRDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateFR.InboardPickUp[key].Position.X);
                            _scFRGUI.SCFRDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateFR.InboardPickUp[key].Position.Y);
                            break;
                        }
                    }
                    foreach (string key in _coordinateFR.OutboardPickUp.Keys)
                    {
                        if (_scFRGUI.SCFRDataTableGUI.Rows[i].Field<string>(0) == key)
                        {
                            _scFRGUI.SCFRDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateFR.OutboardPickUp[key].Position.Z);
                            _scFRGUI.SCFRDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateFR.OutboardPickUp[key].Position.X);
                            _scFRGUI.SCFRDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateFR.OutboardPickUp[key].Position.Y);
                            break; 
                        }
                    }
                }

            }
            #endregion

            #region Editing the DataTable which had been created using the Map Method above
            public static void EditMappedSuspension() { } /// <remarks>Mostly won't be needing this. Aldready have a method to modify the created Suspension</remarks>
            #endregion

        }
        #endregion

        #region Default values of Rear Left Suspension Coordinate Item
        public static class REARLEFTSuspensionDefaultValues
        {

            #region Populating the SCRL Data Table for Double Wishbone
            public static void DoubleWishBone(Kinematics_Software_New r1, SuspensionCoordinatesRearGUI _scrlGUI)
            {
                #region Lower Front Chassis - Point D
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Lower Front Chassis", -1050, 225.38, 54.2 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Poing C
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Lower Rear Chassis", -1460, 190.8, 56.65 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Front Chassis - Point A
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Upper Front Chassis", -1065, 247.87, 149.57 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Rear Chassis - Point B
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Upper Rear Chassis", -1520, 216.86, 183.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Bell Crank Pivot - Point I
                if (r1.PushrodRear_VehicleGUI == 1) { _scrlGUI.SCRLDataTableGUI.Rows.Add("Bell Crank Pivot", -1505, 220.98, 295.14 + Convert.ToDouble(r1.InputOriginY.Text)); }
                else if (r1.PullrodRear_VehicleGUI == 1) { _scrlGUI.SCRLDataTableGUI.Rows.Add("Bell Crank Pivot", -1505, 220.98, 30 + Convert.ToDouble(r1.InputOriginY.Text)); }
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1548.13, 265, 35 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1548.13, 265, 35 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1440.473, 0, 261.733 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1440.473, 0, 261.733 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Steering Link Chassis - Point N
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Steering Link Chassis", -1500, 185.8, 48.65 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Shock Mount - Point JO
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Shock Mount", -1505, 18, 334 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Shock Mount", -1505, 18, 72 + Convert.ToDouble(r1.InputOriginY.Text));

                    }
                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Shock Mount", -1449.481, 24.093, 322.491 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Shock Mount", -1449.481, 24.093, 66.74 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Ride Height Reference
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Ride Height Reference", -1550, 100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Bell-Crank - Point J
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Bell-Crank", -1505, 216.65, 356.98 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Bell-Crank", -1505, 216.65, 92.4 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Bell-Crank", -1539.509, 201.496, 342.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Bell-Crank", -1539.509, 201.496, 83.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Pushrod Bell-Crank - Point H
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Pushrod Bell-Crank", -1505, 267.84, 316.01 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Pullrod Bell-Crank", -1505, 267.84, 40 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Pushrod Bell-Crank", -1524.188, 259.957, 298.938 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Pullrod Bell-Crank", -1524.188, 259.957, 36.817 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Anti-Roll Bar Bell-Crank - Point O
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1505, 255.99, 269.17 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1505, 255.99, 4.96 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1528.209, 207.876, 327.199 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1528.209, 207.876, 68.017 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Pushrod Upright - Point G
                if (r1.PushrodRear_VehicleGUI == 1)
                {
                    _scrlGUI.SCRLDataTableGUI.Rows.Add("Pushrod Upright", -1504.89, 441.71, 61.39 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                else if (r1.PullrodRear_VehicleGUI == 1)
                {
                    _scrlGUI.SCRLDataTableGUI.Rows.Add("Pullrod Upright", -1504.89, 441.71, 200 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

                #region Upper Ball Joint - Point F
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Upper Ball Joint", -1559.77, 462.69, 218.19 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Lower Ball Joint", -1526.35, 474.28, 43.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Link", -1507.11, 260.2, 22.57 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Link", -1440.473, 135.166, 261.73 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                #endregion

                #region Wheel Centre - Point K
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Wheel Centre", -1551, 498.75, 147.5 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Steering Link Upright", -1603.84, 487.55, 42.97 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Contact Patch", -1548.21, 573.14, -83.802 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Torion Bar Bottom - Point R
                if (r1.TARBRear_VehicleGUI == 1)
                {
                    _scrlGUI.SCRLDataTableGUI.Rows.Add("Torsion Bar Bottom Pivot", -1515.183, 0, 195.262 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

            }
            #endregion

            #region Populating the SCRL Data table for McPherson
            public static void McPherson(SuspensionCoordinatesRearGUI _scrlGUI, Kinematics_Software_New r1)
            {
                #region Lower Front Chassis - Point D
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Lower Front Chassis", -697.2, 149.65, 35.9 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Point C
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Lower Rear Chassis", -969.47, 126.7, 32.3 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1027.99, 315.06, 23.2 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Chassis - Point N
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Steering Link Chassis", -996.03, 123.37, 32.3 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Chassis Mount - Point JO
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Shock Mount", -1035.72, 246.58, 356.37 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Ride Height Reference
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Ride Height Reference", -1550, 100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Upright - Point J
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Damper Upright", -1035.72, 307.22, 144.89 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Lower Ball Joint", -1035.72, 314.95, 29.17 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Anti-Roll Bar Link", -1000.75, 337, 14.98 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Wheel Centre - Point K
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Wheel Centre", -1029.89, 331.18, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Steering Link Upright", -1064.98, 324.74, 28.53 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scrlGUI.SCRLDataTableGUI.Rows.Add("Contact Patch", -1028.59, 573.14, -83 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

            }
            #endregion

            #region Populating the DataTable for the creation of a Mapped Suspension created using Imported SM and NSM
            public static void CreateMappedSuspension(CoordinateDatabase _coordinateRL, SuspensionCoordinatesRearGUI _scRLGUI)
            {
                for (int i = 0; i < _scRLGUI.SCRLDataTableGUI.Rows.Count; i++)
                {
                    foreach (string key in _coordinateRL.InboardPickUp.Keys)
                    {
                        if (_scRLGUI.SCRLDataTableGUI.Rows[i].Field<string>(0) == key)
                        {
                            _scRLGUI.SCRLDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateRL.InboardPickUp[key].Position.Z);
                            _scRLGUI.SCRLDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateRL.InboardPickUp[key].Position.X);
                            _scRLGUI.SCRLDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateRL.InboardPickUp[key].Position.Y);
                            break;
                        }
                    }
                    foreach (string key in _coordinateRL.OutboardPickUp.Keys)
                    {
                        if (_scRLGUI.SCRLDataTableGUI.Rows[i].Field<string>(0) == key)
                        {
                            _scRLGUI.SCRLDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateRL.OutboardPickUp[key].Position.Z);
                            _scRLGUI.SCRLDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateRL.OutboardPickUp[key].Position.X);
                            _scRLGUI.SCRLDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateRL.OutboardPickUp[key].Position.Y);
                            break; 
                        }
                    }
                }
            }
            #endregion

            #region Editing the DataTable which had been created using the Map Method above
            public static void EditMappedSuspension() { } /// <remarks>Mostly won't be needing this. Aldready have a method to modify the created Suspension</remarks>
            #endregion


        }
        #endregion

        #region Default values of Rear Right Suspension Coordinate Item
        public static class REARRIGHTSuspensionDefaultValues
        {
            #region Populating the SCRL Data Table for Double Wishbone
            public static void DoubleWishBone(Kinematics_Software_New r1, SuspensionCoordinatesRearRightGUI _scrrGUI)
            {
                #region Lower Front Chassis - Point D
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Lower Front Chassis", -1050, -225.38, 54.2 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Poing C
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Lower Rear Chassis", -1460, -190.8, 56.65 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Front Chassis - Point A
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Upper Front Chassis", -1065, -247.87, 149.57 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Upper Rear Chassis - Point B
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Upper Rear Chassis", -1520, -216.86, 183.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Bell Crank Pivot - Point I
                if (r1.PushrodRear_VehicleGUI == 1) { _scrrGUI.SCRRDataTableGUI.Rows.Add("Bell Crank Pivot", -1505, -220.98, 295.14 + Convert.ToDouble(r1.InputOriginY.Text)); }
                else if (r1.PullrodRear_VehicleGUI == 1) { _scrrGUI.SCRRDataTableGUI.Rows.Add("Bell Crank Pivot", -1505, -220.98, 30 + Convert.ToDouble(r1.InputOriginY.Text)); }
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1548.13, -265, 35 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1548.13, -265, 35 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1440.473, 0, 261.733 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1440.473, 0, 261.733 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Steering Link Chassis - Point N
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Steering Link Chassis", -1500, -185.8, 48.65 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Shock Mount - Point JO
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Shock Mount", -1505, -18, 334 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Shock Mount", -1505, -18, 72 + Convert.ToDouble(r1.InputOriginY.Text));

                    }
                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Shock Mount", -1449.481, -24.093, 322.491 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Shock Mount", -1449.481, -24.093, 66.74 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Ride Height Reference
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Ride Height Reference", -1550, -100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Bell-Crank - Point J
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Bell-Crank", -1505, -216.65, 356.98 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Bell-Crank", -1505, -216.65, 92.4 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Bell-Crank", -1539.509, -201.496, 342.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Bell-Crank", -1539.509, -201.496, 83.889 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                #endregion

                #region Pushrod Bell-Crank - Point H
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Pushrod Bell-Crank", -1505, -267.84, 316.01 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Pullrod Bell-Crank", -1505, -267.84, 40 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Pushrod Bell-Crank", -1524.188, -259.957, 298.938 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Pullrod Bell-Crank", -1524.188, -259.957, 36.817 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Anti-Roll Bar Bell-Crank - Point O
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1505, -255.99, 269.17 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1505, -255.99, 4.96 + Convert.ToDouble(r1.InputOriginY.Text));
                    }


                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    if (r1.PullrodRear_VehicleGUI == 0 && r1.PushrodRear_VehicleGUI == 1)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1528.209, -207.876, 327.199 + Convert.ToDouble(r1.InputOriginY.Text));
                    }

                    else if (r1.PullrodRear_VehicleGUI == 1 && r1.PushrodRear_VehicleGUI == 0)
                    {
                        _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Bell-Crank", -1528.209, -207.876, 68.017 + Convert.ToDouble(r1.InputOriginY.Text));
                    }
                }
                #endregion

                #region Pushrod Upright - Point G
                if (r1.PushrodRear_VehicleGUI == 1)
                {
                    _scrrGUI.SCRRDataTableGUI.Rows.Add("Pushrod Upright", -1504.89, -441.71, 61.39 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                else if (r1.PullrodRear_VehicleGUI == 1)
                {
                    _scrrGUI.SCRRDataTableGUI.Rows.Add("Pullrod Upright", -1504.89, -441.71, 200 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

                #region Upper Ball Joint - Point F
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Upper Ball Joint", -1559.77, -462.69, 218.19 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Lower Ball Joint", -1526.35, -474.28, 43.03 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                if (r1.UARBRear_VehicleGUI == 1)
                {
                    _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Link", -1507.11, -260.2, 22.57 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                else if (r1.TARBRear_VehicleGUI == 1)
                {
                    _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Link", -1440.473, -135.166, 261.73 + Convert.ToDouble(r1.InputOriginY.Text));

                }
                #endregion

                #region Wheel Centre - Point K
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Wheel Centre", -1551, -498.75, 147.5 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Steering Link Upright", -1603.84, -487.55, 42.97 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Contact Patch", -1548.21, -573.14, -83.802 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Torion Bar Bottom - Point R
                if (r1.TARBRear_VehicleGUI == 1)
                {
                    _scrrGUI.SCRRDataTableGUI.Rows.Add("Torsion Bar Bottom Pivot", -1515.183, 0, 195.262 + Convert.ToDouble(r1.InputOriginY.Text));
                }
                #endregion

            }
            #endregion

            #region Populating the SCRL Data table for McPherson
            public static void McPherson(SuspensionCoordinatesRearRightGUI _scrrGUI, Kinematics_Software_New r1)
            {
                #region Lower Front Chassis - Point D
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Lower Front Chassis", -697.2, -149.65, 35.9 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Rear Chassis - Point C
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Lower Rear Chassis", -969.47, -126.7, 32.3 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Chassis - Point Q
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Chassis", -1027.99, -315.06, 23.2 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Chassis - Point N
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Steering Link Chassis", -996.03, -123.37, 32.3 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Chassis Mount - Point JO
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Shock Mount", -1035.72, -246.58, 356.37 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Ride Height Reference
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Ride Height Reference", -1550, -100, 0 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Damper Upright - Point J
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Damper Upright", -1035.72, -307.22, 144.89 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Lower Ball Joint - Point E
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Lower Ball Joint", -1035.72, -314.95, 29.17 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Anti-Roll Bar Link - Point P
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Anti-Roll Bar Link", -1000.75, -337, 14.98 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Wheel Centre - Point K
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Wheel Centre", -1029.89, -331.18, 147.26 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Steering Link Upright - Point M
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Steering Link Upright", -1064.98, -324.74, 28.53 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

                #region Contact Patch - Point W
                _scrrGUI.SCRRDataTableGUI.Rows.Add("Contact Patch", -1028.59, -573.14, -83 + Convert.ToDouble(r1.InputOriginY.Text));
                #endregion

            }
            #endregion

            #region Populating the DataTable for the creation of a Mapped Suspension created using Imported SM and NSM
            public static void CreateMappedSuspension(CoordinateDatabase _coordinateRR, SuspensionCoordinatesRearRightGUI _scRRGUI)
            {
                for (int i = 0; i < _scRRGUI.SCRRDataTableGUI.Rows.Count; i++)
                {
                    foreach (string key in _coordinateRR.InboardPickUp.Keys)
                    {
                        if (_scRRGUI.SCRRDataTableGUI.Rows[i].Field<string>(0) == key)
                        {
                            _scRRGUI.SCRRDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateRR.InboardPickUp[key].Position.Z);
                            _scRRGUI.SCRRDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateRR.InboardPickUp[key].Position.X);
                            _scRRGUI.SCRRDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateRR.InboardPickUp[key].Position.Y);
                            break;
                        }
                    }
                    foreach (string key in _coordinateRR.OutboardPickUp.Keys)
                    {
                        if (_scRRGUI.SCRRDataTableGUI.Rows[i].Field<string>(0) == key)
                        {
                            _scRRGUI.SCRRDataTableGUI.Rows[i].SetField<double>("X (mm)", _coordinateRR.OutboardPickUp[key].Position.Z);
                            _scRRGUI.SCRRDataTableGUI.Rows[i].SetField<double>("Y (mm)", _coordinateRR.OutboardPickUp[key].Position.X);
                            _scRRGUI.SCRRDataTableGUI.Rows[i].SetField<double>("Z (mm)", _coordinateRR.OutboardPickUp[key].Position.Y);
                            break; 
                        }
                    }
                }
            }
            #endregion

            #region Editing the DataTable which had been created using the Map Method above
            public static void EditMappedSuspension() { } /// <remarks>Mostly won't be needing this. Aldready have a method to modify the created Suspension</remarks>
            #endregion

        }
        #endregion

        #region Default Values of the Load Cases
        public class LoadCaseDefaultValues
        {
            /// <summary>
            /// Represents Accelerations at the Suspended Mass CoG. Will be translated to each tire
            /// </summary>
            public static double SM_Ay_DV = 0, SM_Ax_DV = 0, SM_Az_DV = 0;

            /// <summary>
            /// Represents the Lateral Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Lateral Acceleration at the CoG
            /// </summary>
            public static double NSM_FL_Ay_DV = 0, NSM_FR_Ay_DV = 0, NSM_RL_Ay_DV = 0, NSM_RR_Ay_DV = 0;

            /// <summary>
            /// Represents the Longitudinal Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Longitudinal Acceleration at the CoG
            /// </summary>
            public static double NSM_FL_Ax_DV = 0, NSM_FR_Ax_DV = 0, NSM_RL_Ax_DV = 0, NSM_RR_Ax_DV = 0;

            /// <summary>
            /// Represents the Vertical Accelerations at each of the Non Suspended Masses. The force due to these accelerations will be added to the Force that is caused by the Vertical Acceleration at the CoG
            /// </summary>
            public static double NSM_FL_Az_DV = 0, NSM_FR_Az_DV = 0, NSM_RL_Az_DV = 0, NSM_RR_Az_DV = 0;

            /// <summary>
            /// Represents the Lateral Grip Distribution. This parameter will be used to determine how much of the Lateral Force at the CG is reacted at the tire. 
            /// </summary>
            public static double NSM_FL_LatGripDistribution_DV = 0, NSM_FR_LatGripDistribution_DV = 0, NSM_RL_LatGripDistribution_DV = 0, NSM_RR_LatGripDistribution_DV = 0;

            /// <summary>
            /// Represents the Longitudinal Grip Distribution. This parameter will be used to determine how much of the Lateral Force at the CG is reacted at the tire
            /// </summary>
            public static double NSM_FL_LongGripDistribution_DV = 0, NSM_FR_LongGripDistribution_DV = 0, NSM_RL_LongGripDistribution_DV = 0, NSM_RR_LongGripDistribution_DV = 0;

            /// <summary>
            /// Represents the Overturning Moment on the Tire on each corner
            /// </summary>
            public static double NSM_FL_Mx_DV = 0, NSM_FR_Mx_DV = 0, NSM_RL_Mx_DV = 0, NSM_RR_Mx_DV = 0;

            /// <summary>
            /// Represents the Self Aligning Torque on the Tire on each corner
            /// </summary>
            public static double NSM_FL_Mz_DV = 0, NSM_FR_Mz_DV = 0, NSM_RL_Mz_DV = 0, NSM_RR_Mz_DV = 0;

            #region Assigning values to the Variabls based on the type of load case the user wants to create
            /// <summary>
            /// This method assigns the Load Case variables with the appropriate values based on the type of Load Case which the user wants to create. 
            /// </summary>
            /// <param name="_loadCaseName">Name of the Load Case that the user wants to create. Determines the values of the variables</param>
            public static void AssignTypeOfLoadCase(string _loadCaseName)
            {
                SM_Ay_DV = SM_Ax_DV = SM_Az_DV = NSM_FL_Ay_DV = NSM_FR_Ay_DV = NSM_RL_Ay_DV = NSM_RR_Ay_DV = NSM_FL_Ax_DV = NSM_FR_Ax_DV = NSM_RL_Ax_DV = NSM_RR_Ax_DV = NSM_FL_Az_DV = NSM_FR_Az_DV = NSM_RL_Az_DV = NSM_RR_Az_DV =
                NSM_FL_LatGripDistribution_DV = NSM_FR_LatGripDistribution_DV = NSM_RL_LatGripDistribution_DV = NSM_RR_LatGripDistribution_DV =
                NSM_FL_LongGripDistribution_DV = NSM_FR_LongGripDistribution_DV = NSM_RL_LongGripDistribution_DV = NSM_RR_LongGripDistribution_DV =
                NSM_FL_Mx_DV = NSM_FR_Mx_DV = NSM_RL_Mx_DV = NSM_RR_Mx_DV = NSM_FL_Mz_DV = NSM_FR_Mz_DV = NSM_RL_Mz_DV = NSM_RR_Mz_DV = 0;

                if (_loadCaseName == "Pure Ay(+)")
                {
                    SM_Ay_DV = 3;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_Mz_DV = 3;
                    NSM_FR_Mz_DV = 7;
                    NSM_RL_Mz_DV = 3.5;
                    NSM_RR_Mz_DV = 9;
                }
                else if (_loadCaseName == "Pure Ay(-)")
                {
                    SM_Ay_DV = -3;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_Mz_DV = 3;
                    NSM_FR_Mz_DV = 7;
                    NSM_RL_Mz_DV = 3.5;
                    NSM_RR_Mz_DV = 9;
                }

                else if (_loadCaseName == "Pure Ax(+)")
                {
                    SM_Ax_DV = 0.8;
                    NSM_FL_LongGripDistribution_DV = 0;
                    NSM_FR_LongGripDistribution_DV = 0;
                    NSM_RL_LongGripDistribution_DV = 50;
                    NSM_RR_LongGripDistribution_DV = 50;
                    NSM_FL_Mz_DV = 0;
                    NSM_FR_Mz_DV = 0;
                    NSM_RL_Mz_DV = 2;
                    NSM_RR_Mz_DV = 2;
                }
                else if (_loadCaseName == "Pure Ax(-)")
                {
                    SM_Ax_DV = -4.5;
                    NSM_FL_LongGripDistribution_DV = 30;
                    NSM_FR_LongGripDistribution_DV = 30;
                    NSM_RL_LongGripDistribution_DV = 20;
                    NSM_RR_LongGripDistribution_DV = 20;
                    NSM_FL_Mz_DV = 2.5;
                    NSM_FR_Mz_DV = 2.5;
                    NSM_RL_Mz_DV = 0.5;
                    NSM_RR_Mz_DV = 0.5;
                }
                else if (_loadCaseName == "Cornering with Steering")
                {
                    SM_Ay_DV = 1.5;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_Mz_DV = 10;
                    NSM_FR_Mz_DV = 13;
                    NSM_RL_Mz_DV = 10;
                    NSM_RR_Mz_DV = 16;
                }
                else if (_loadCaseName == "NSM Ay(+)")
                {
                    NSM_FL_Az_DV = 15;
                    NSM_FR_Az_DV = 15;
                    NSM_RL_Az_DV = 15;
                    NSM_RR_Az_DV = 15;
                }
                else if (_loadCaseName == "NSM Ay(-)")
                {
                    NSM_FL_Az_DV = -15;
                    NSM_FR_Az_DV = -15;
                    NSM_RL_Az_DV = -15;
                    NSM_RR_Az_DV = -15;
                }
                else if (_loadCaseName == "Ay(+) & Ax(+)")
                {
                    SM_Ay_DV = 1.5;
                    SM_Ax_DV = 0.8;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_LongGripDistribution_DV = 30;
                    NSM_FR_LongGripDistribution_DV = 30;
                    NSM_RL_LongGripDistribution_DV = 20;
                    NSM_RR_LongGripDistribution_DV = 20;
                    NSM_FL_Mz_DV = 1.2;
                    NSM_FR_Mz_DV = 5.5;
                    NSM_RL_Mz_DV = 5;
                    NSM_RR_Mz_DV = 11;
                }
                else if (_loadCaseName == "Ay(+) & Ax(-)")
                {
                    SM_Ay_DV = 1.5;
                    SM_Ax_DV = -1.8;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_LongGripDistribution_DV = 30;
                    NSM_FR_LongGripDistribution_DV = 30;
                    NSM_RL_LongGripDistribution_DV = 20;
                    NSM_RR_LongGripDistribution_DV = 20;
                    NSM_FL_Mz_DV = 1.2;
                    NSM_FR_Mz_DV = 5.5;
                    NSM_RL_Mz_DV = 5;
                    NSM_RR_Mz_DV = 11;
                }
                else if (_loadCaseName == "Ay(-) & Ax(+)")
                {
                    SM_Ay_DV = -1.5;
                    SM_Ax_DV = 0.8;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_LongGripDistribution_DV = 30;
                    NSM_FR_LongGripDistribution_DV = 30;
                    NSM_RL_LongGripDistribution_DV = 20;
                    NSM_RR_LongGripDistribution_DV = 20;
                    NSM_FL_Mz_DV = 1.2;
                    NSM_FR_Mz_DV = 5.5;
                    NSM_RL_Mz_DV = 5;
                    NSM_RR_Mz_DV = 11;
                }
                else if (_loadCaseName == "Ay(-) & Ax(-)")
                {
                    SM_Ay_DV = -1.5;
                    SM_Ax_DV = -1.8;
                    NSM_FL_LatGripDistribution_DV = 12;
                    NSM_FR_LatGripDistribution_DV = 36;
                    NSM_RL_LatGripDistribution_DV = 13;
                    NSM_RR_LatGripDistribution_DV = 39;
                    NSM_FL_LongGripDistribution_DV = 30;
                    NSM_FR_LongGripDistribution_DV = 30;
                    NSM_RL_LongGripDistribution_DV = 20;
                    NSM_RR_LongGripDistribution_DV = 20;
                    NSM_FL_Mz_DV = 1.2;
                    NSM_FR_Mz_DV = 5.5;
                    NSM_RL_Mz_DV = 5;
                    NSM_RR_Mz_DV = 11;
                }

            }
            #endregion

            #region Populating the Data Tables of the Load Cases with the Default values which were assigned
            /// <summary>
            /// This method populates the Data Table of the Load cases with the Default values which were assigned
            /// </summary>
            /// <param name="_loadCaseGUI"></param>
            public static void InitializeLoadCase(LoadCaseGUI _loadCaseGUI)
            {
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Lateral Acceleration: Ay (g)", NSM_FL_Ay_DV);
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Lateral Grip Distribution (%)", NSM_FL_LatGripDistribution_DV);
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Longitudinal Acceleration: Ax (g)", NSM_FL_Ax_DV);
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Longitudinal Grip Distribution (%)", NSM_FL_LongGripDistribution_DV);
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Vertical Acceleration: Az (g)", NSM_FL_Az_DV);
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Tire OVerturning Moment: Mx (Nm)", NSM_FL_Mx_DV);
                _loadCaseGUI.NSM_FL_DataTableGUI.Rows.Add("Self-Aligning Torque: Mz (Nm)", NSM_FL_Mz_DV);

                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Lateral Acceleration: Ay (g)", NSM_FR_Ay_DV);
                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Lateral Grip Distribution (%)", NSM_FR_LatGripDistribution_DV);
                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Longitudinal Acceleration: Ax (g)", NSM_FR_Ax_DV);
                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Longitudinal Grip Distribution (%)", NSM_FR_LongGripDistribution_DV);
                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Vertical Acceleration: Az (g)", NSM_FR_Az_DV);
                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Tire OVerturning Moment: Mx (Nm)", NSM_FR_Mx_DV);
                _loadCaseGUI.NSM_FR_DataTableGUI.Rows.Add("Self-Aligning Torque: Mz (Nm)", NSM_FR_Mz_DV);

                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Lateral Acceleration: Ay (g)", NSM_RL_Ay_DV);
                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Lateral Grip Distribution (%)", NSM_RL_LatGripDistribution_DV);
                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Longitudinal Acceleration: Ax (g)", NSM_RL_Ax_DV);
                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Longitudinal Grip Distribution (%)", NSM_RL_LongGripDistribution_DV);
                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Vertical Acceleration: Az (g)", NSM_RL_Az_DV);
                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Tire OVerturning Moment: Mx (Nm)", NSM_RL_Mx_DV);
                _loadCaseGUI.NSM_RL_DataTableGUI.Rows.Add("Self-Aligning Torque: Mz (Nm)", NSM_RL_Mz_DV);

                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Lateral Acceleration: Ay (g)", NSM_RR_Ay_DV);
                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Lateral Grip Distribution (%)", NSM_RR_LatGripDistribution_DV);
                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Longitudinal Acceleration: Ax (g)", NSM_RR_Ax_DV);
                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Longitudinal Grip Distribution (%)", NSM_RR_LongGripDistribution_DV);
                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Vertical Acceleration: Az (g)", NSM_RR_Az_DV);
                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Tire OVerturning Moment: Mx (Nm)", NSM_RR_Mx_DV);
                _loadCaseGUI.NSM_RR_DataTableGUI.Rows.Add("Self-Aligning Torque: Mz (Nm)", NSM_RR_Mz_DV);

                _loadCaseGUI.SuspendedMass_DataTableGUI.Rows.Add("Lateral Acceleration: Ay (g)", SM_Ay_DV);
                _loadCaseGUI.SuspendedMass_DataTableGUI.Rows.Add("Longitudinal Acceleration: Ax (g)", SM_Ax_DV);
                _loadCaseGUI.SuspendedMass_DataTableGUI.Rows.Add("Vertical Acceleration: Az (g)", SM_Az_DV);

                _loadCaseGUI.FL_Bearing_DataTable_GUI.Rows.Add("Steering Rack Bearing Attachment Point 1", 40, 202, 125);
                _loadCaseGUI.FL_Bearing_DataTable_GUI.Rows.Add("Steering Rack Bearing Attachment Point 2", 80, 202, 125);
                _loadCaseGUI.FL_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 1", 50, 254, 65);
                _loadCaseGUI.FL_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 2", 70, 254, 65);

                _loadCaseGUI.FR_Bearing_DataTable_GUI.Rows.Add("Steering Rack Bearing Attachment Point 1", 40, -202, 125);
                _loadCaseGUI.FR_Bearing_DataTable_GUI.Rows.Add("Steering Rack Bearing Attachment Point 2", 80, -202, 125);
                _loadCaseGUI.FR_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 1", 50, -254, 65);
                _loadCaseGUI.FR_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 2", 70, -254, 65);

                _loadCaseGUI.RL_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 1", -1540, 265, 65);
                _loadCaseGUI.RL_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 2", -1560, 265, 65);

                _loadCaseGUI.RR_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 1", -1540, -265, 65);
                _loadCaseGUI.RR_Bearing_DataTable_GUI.Rows.Add("ARB Bearing Attachment Point 2", -1560, -265, 65);

                _loadCaseGUI.SteeringColumnBearing_DataTable_GUI.Rows.Add("Steering Columnn Bearing Attachment Point 1", -280, 10, 540);
                _loadCaseGUI.SteeringColumnBearing_DataTable_GUI.Rows.Add("Steering Columnn Bearing Attachment Point 2", -280,-10, 540);
            } 
            #endregion
        }
        #endregion

    }
}

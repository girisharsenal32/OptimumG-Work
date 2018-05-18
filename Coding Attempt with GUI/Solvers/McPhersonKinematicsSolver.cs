using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Spatial.Euclidean;
using MathNet.Numerics.LinearAlgebra;

namespace Coding_Attempt_with_GUI
{
    public class McPhersonKinematicsSolver : SolverMasterClass
    {
        /// <summary
        /// This method is used to select the correct set of coordinates of the Kinematic Points of interest (using the Solver Method) as the car is dropped from the stands to the ground when the user has 
        /// selected a McPherson Geometry
        /// The wheel and spring deflections are calculated and these are used to calculate all the remaining points using the Solver Method
        /// The actual calculation of the points is carried out in the Solver Method 
        /// </summary>

        VehicleModel _modelMcP = new VehicleModel();
        /// <summary>
        /// Tire Forces from the Load Case
        /// </summary>
        private double LatForce, LongForce, VerticalForce;
        /// <summary>
        /// Tire Moments from the Load Case
        /// </summary>
        private double Mx, Mz;

        #region Methods

        #region Final Motion Ratio
        private void CalculateFinaMotionRatio(OutputClass _ocMR, SuspensionCoordinatesMaster _scmMR)
        {
            //Final Motion Ratio for McPherson Strut
            double J2JO_Perp, J2JO, alpha;
            J2JO_Perp = Math.Abs(_ocMR.scmOP.J1x - l_JO1x);
            J2JO = Math.Sqrt(Math.Pow((_ocMR.scmOP.J1x - l_JO1x), 2) + Math.Pow((_ocMR.scmOP.J1y - l_JO1y), 2));
            alpha = Math.Asin(J2JO_Perp / J2JO);
            _ocMR.FinalMR = Math.Acos(alpha);
            _ocMR.Final_ARB_MR = (_scmMR.O1I / _scmMR.P1Q) * (Math.Cos(alpha));
        } 
        #endregion

        #region Outboard Points
        private void CalculateOutboardPoints(OutputClass _ocOut, SuspensionCoordinatesMaster _scmOut)
        {
            // TO CALCULATE THE NEW POSITION OF J i.e., TO CALCULATE J'
            // Vectors used -> J'Jo, J'D & J'C
            double XJ1 = 0, YJ1 = 0, ZJ1 = 0;
            QuadraticEquationSolver.Solver(l_J1x, l_J1y, l_J1z, l_JO1x, l_JO1y, l_JO1z, _ocOut.DamperLength, l_D1x, l_D1y, l_D1z, l_C1x, l_C1y, l_C1z, l_JO1x, l_JO1y, l_JO1z, l_D1x, l_D1y, l_D1z, l_C1x, l_C1y, l_C1z, l_JO1y, true, out XJ1, out YJ1, out ZJ1);

            _ocOut.scmOP.J1x = XJ1;
            _ocOut.scmOP.J1y = YJ1;
            _ocOut.scmOP.J1z = ZJ1;

            // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' 
            // Vectors used -> E'J', E'C & E'D   
            double XE1 = 0, YE1 = 0, ZE1 = 0;
            QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_J1x, l_J1y, l_J1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.J1x, _ocOut.scmOP.J1y, _ocOut.scmOP.J1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.J1y, true, out XE1, out YE1, out ZE1);

            _ocOut.scmOP.E1x = XE1;
            _ocOut.scmOP.E1y = YE1;
            _ocOut.scmOP.E1z = ZE1;

            CalculateFinaMotionRatio(_ocOut, _scmOut);

            // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
            // Vectors used -> M'E', M'J' & M'N  
            double XM1 = 0, YM1 = 0, ZM1 = 0;
            QuadraticEquationSolver.Solver(l_M1x, l_M1y, l_M1z, l_E1x, l_E1y, l_E1z, 0, l_J1x, l_J1y, l_J1z, l_N1x, l_N1y, l_N1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, _ocOut.scmOP.J1x, _ocOut.scmOP.J1y, _ocOut.scmOP.J1z, l_N1x, l_N1y, l_N1z, l_JO1y, true, out XM1, out YM1, out ZM1);

            _ocOut.scmOP.M1x = XM1;
            _ocOut.scmOP.M1y = YM1;
            _ocOut.scmOP.M1z = ZM1;

            _ocOut.scmOP.N1x = l_N1x;
            _ocOut.scmOP.N1y = l_N1y;
            _ocOut.scmOP.N1z = l_N1z;

            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            // Vectors used -> K'M', K'J' & K'E'   
            double XK1 = 0, YK1 = 0, ZK1 = 0;
            QuadraticEquationSolver.Solver(l_K1x, l_K1y, l_K1z, l_M1x, l_M1y, l_M1z, 0, l_J1x, l_J1y, l_J1z, l_E1x, l_E1y, l_E1z, _ocOut.scmOP.M1x, _ocOut.scmOP.M1y, _ocOut.scmOP.M1z, _ocOut.scmOP.J1x, _ocOut.scmOP.J1y, _ocOut.scmOP.J1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, _ocOut.scmOP.J1y, true, out XK1, out YK1, out ZK1);

            _ocOut.scmOP.K1x = XK1;
            _ocOut.scmOP.K1y = YK1;
            _ocOut.scmOP.K1z = ZK1;

            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            // Vectors used -> P'O', P'Q & P'D   
            double XP1 = 0, YP1 = 0, ZP1 = 0;
            QuadraticEquationSolver.Solver(l_P1x, l_P1y, l_P1z, l_E1x, l_E1y, l_E1z, 0, l_Q1x, l_Q1y, l_Q1z, l_J1x, l_J1y, l_J1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, l_Q1x, l_Q1y, l_Q1z, _ocOut.scmOP.J1x, _ocOut.scmOP.J1y, _ocOut.scmOP.J1z, _ocOut.scmOP.J1y, true, out XP1, out YP1, out ZP1);

            _ocOut.scmOP.P1x = XP1;
            _ocOut.scmOP.P1y = YP1;
            _ocOut.scmOP.P1z = ZP1;
        }
        #endregion

        #region Method to Assign the Lateral, Longitudinal and Vertical Load fro the Load Case
        /// <summary>
        /// This method assigns the Forces and Moments that the user has defined (or predefined) during the Load Case creation
        /// </summary>
        /// <param name="_latForce">Total Tire Lateral Force as calculated from the Load Case</param>
        /// <param name="_longForce">Total Tire Longitudinal Force as calculated from the Load Case</param>
        /// <param name="_vertForce">Total Tire Vertical Force as calculated from the Load Case</param>
        /// <param name="_mx">Tire Overturning Moment as defined in the Load Case</param>
        /// <param name="_mz">Tire Self-Aligning Torque as defined in the Load Case</param>
        public void AssignLoadCaseMcPherson(double _latForce, double _longForce, double _vertForce, double _mx, double _mz)
        {
            LatForce = _latForce;
            LongForce = _longForce;
            VerticalForce = _vertForce;
            Mx = _mx;
            Mz = _mz;
        }
        #endregion

        #region Kinematics - McPherson
        //Kinematics Solver for McPherson Suspension
        public void KinematicsMcPherson(int Identifier, SuspensionCoordinatesMaster scm, WheelAlignment wa, Tire tire, AntiRollBar arb, double ARB_Rate_Nmm, Spring spring, Damper damper, List<OutputClass> oc, Vehicle _vehicleForMcPherson, List<double> WheelOrSpringDeflections, bool MotionExists, bool RecalculateSteering/*, bool FirstIteration*/)
        {
            dummy1 = dummy2 = 0;

            #region Initialization methods

            ///<remarks>
            ///The IF loop is employed so that if the < c > Kinematics </ c > method is being run to calculate the Points for Steering then local coordinate variables should be initialized with the Output Class coordinates rather than the Input Suspension Coordinates as
            ///the method has already been executed and is been run for the second time for the purpose of calcualting steering 
            /// </remarks>

            // Initialization of the Local Coordinate variables
            if (!RecalculateSteering)
            {
                AssignLocalCoordinateVariables_FixesPoints(scm);
                AssignLocalCoordinateVariables_MovingPoints(scm);
            }
            else if (RecalculateSteering)
            {
                AssignLocalCoordinateVariables_FixesPoints(oc[dummy1].scmOP);
                AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;
            }


            if (MotionExists)
            {
                N = _vehicleForMcPherson.vehicle_Motion.Final_WheelDeflectionsY.Count;

                // Initializating the Local Wheel Alignment variables
                InitializeWheelAlignmentVariables(wa, oc[dummy1], Identifier, _vehicleForMcPherson.vehicle_Motion.SteeringExists);

                // Initialize array which holds the previous value of spring deflection
                SpringDeflection_Prev = new double[_vehicleForMcPherson.vehicle_Motion.Final_WheelDeflectionsX.Count];
            }
            else if (!MotionExists)
            {
                N = 200;

                // Initializating the Local Wheel Alignment variables
                InitializeWheelAlignmentVariables(wa, oc[dummy1], Identifier, false);
            }


            //  The value of Ride will be calculated at the end as the relative value of Ride Height. This will be used to translate all the coordinates to reflect the car falling on the ground.



            if (!RecalculateSteering)
            {
                ///<remarks> Resetting the wheel defelction due to steering to prevent residue error</remarks>
                for (int i = 0; i < 100; i++)
                {
                    scm.WheelDeflection_Steering[i] = 0;
                }

                // Converting Coordinates from World Coordinate System to Car Coordinate System
                TranslateToLocalCS(_vehicleForMcPherson.sc_FL);


                // Initializing the Spindle End
                InitializeSpindleEndCoordinates(tire);


                // To Calculate position of far end of the spindle vector using Static Camber and Toe
                RotateSpindleVector(oc[dummy1].waOP.StaticCamber, oc[dummy1].waOP.StaticToe, false, 0, 0, 0, 0, 0, 0, Lx, Ly, Lz, out L1x, out L1y, out L1z);


                // Initial Motion Ratio for McPherson Strut
                scm.MotionRatio(_vehicleForMcPherson.McPhersonFront, _vehicleForMcPherson.McPhersonRear, _vehicleForMcPherson.PullRodIdentifierFront, _vehicleForMcPherson.PullRodIdentifierRear, Identifier);
                // Passing the Motion Ratio to the Output Class
                oc[dummy1].InitialMR = scm.InitialMR;
                oc[dummy1].Initial_ARB_MR = scm.Initial_ARB_MR;

                // Preliminary Calculations to find the New Camber and Toe
                CalculateInitialSpindleVector();
            } 
            #endregion


            #region Wheel Deflection Calculations for Stand to Ground Calculations
            if (!MotionExists)
            {
                //
                //Only for Stand to Ground
                //

                CalculateWheelAndSpringDeflection(scm, spring, damper, _vehicleForMcPherson, null, oc[0], tire, Identifier, ARB_Rate_Nmm, MotionExists,RecalculateSteering, 0);

            }
            #endregion


            #region For Loop to Calculate the SUSPENSION POINTS

            for (a1 = 1; a1 < N; a1++)
            {
                oc[dummy1].scmOP.NoOfCouplings = _vehicleForMcPherson.sc_FL.NoOfCouplings;

                #region Wheel Deflection Calculations for Motion Calculation
                if (MotionExists && ((!RecalculateSteering) /*|| ((RecalculateRearSteering) && (Identifier == 3 || Identifier == 4) && dummy1 != 0)*/))
                {
                    //
                    // For Motion Simulation   
                    //
                    CalculateWheelAndSpringDeflection(scm, spring, damper, _vehicleForMcPherson, WheelOrSpringDeflections, oc[dummy1], tire, Identifier, ARB_Rate_Nmm, MotionExists, RecalculateSteering, dummy1);

                }
                else if (MotionExists && (RecalculateSteering /*&& (Identifier == 1 || Identifier == 2)*/))
                {
                    CalculateSpringDeflection_AfterVehicleModel(oc, Identifier, WheelOrSpringDeflections[dummy1], oc[dummy1].FinalMR, dummy1);
                }
                #endregion

                #region Confirm if needed and delete if not
                ///////<remarks>
                ///////The IF loop is employed so that if the < c > Kinematics </ c > method is being run to calculate the Points for Steering then local coordinate variables should be initialized with the Output Class coordinates rather than the Input Suspension Coordinates as
                ///////the method has already been executed and is been run for the second time for the purpose of calcualting steering 
                ///////This loop is executed only once as the 
                /////// </remarks>
                //if (RecalculateSteering && dummy1 != 0)
                //{
                //    AssignLocalCoordinateVariables_FixesPoints(oc[dummy1].scmOP);
                //    AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                //    L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;
                //} 
                #endregion

                #region Progress Bar Operations
                _vehicleForMcPherson.vehicleGUI.ProgressBarVehicleGUI.PerformStep();
                _vehicleForMcPherson.vehicleGUI.ProgressBarVehicleGUI.Update();
                #endregion

                #region Calculating the deflection of the spring in Steps
                CalculateAngleOfRotationOrDamperLength(oc[dummy1], MotionExists, dummy1, damper,RecalculateSteering);
                #endregion

                #region Calculating the Caster and KPI
                CalculateKPIandCaster(oc[dummy1], false, Identifier);
                #endregion

                if (!RecalculateSteering || ((RecalculateSteering) && (Identifier == 3 || Identifier == 4)))
                {
                    #region Calculate the remaining Outboard Points
                    CalculateOutboardPoints(oc[dummy1], scm); 
                    #endregion

                    #region Point L - Wheel Spindle End
                    // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                    // Vectors used -> L'M', L'J' & L'E'   // THE INITIAL COORDINATES OF L SHOULD NOT BE TAKEN FROM USER. THEY SHOULD BE CALCULATED USING 'K' , THE INPUT CAMBER AND TOE
                    double XL1 = 0, YL1 = 0, ZL1 = 0;
                    QuadraticEquationSolver.Solver(L1x, L1y, L1z, l_M1x, l_M1y, l_M1z, 0, l_J1x, l_J1y, l_J1z, l_E1x, l_E1y, l_E1z, oc[dummy1].scmOP.M1x, oc[dummy1].scmOP.M1y, oc[dummy1].scmOP.M1z, oc[dummy1].scmOP.J1x, oc[dummy1].scmOP.J1y, oc[dummy1].scmOP.J1z, oc[dummy1].scmOP.E1x, oc[dummy1].scmOP.E1y, oc[dummy1].scmOP.E1z, oc[dummy1].scmOP.J1y * 2, true, out XL1, out YL1, out ZL1);

                    oc[dummy1].scmOP.L1x = XL1;
                    oc[dummy1].scmOP.L1y = YL1;
                    oc[dummy1].scmOP.L1z = ZL1;
                    #endregion

                    #region Point W - Contact Patch
                    // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                    // Vectors used -> W'L', W'J' & W'E'
                    double XW1 = 0, YW1 = 0, ZW1 = 0;
                    QuadraticEquationSolver.Solver(l_W1x, l_W1y, l_W1z, L1x, L1y, L1z, 0, l_J1x, l_J1y, l_J1z, l_E1x, l_E1y, l_E1z, oc[dummy1].scmOP.L1x, oc[dummy1].scmOP.L1y, oc[dummy1].scmOP.L1z, oc[dummy1].scmOP.J1x, oc[dummy1].scmOP.J1y, oc[dummy1].scmOP.J1z, oc[dummy1].scmOP.E1x, oc[dummy1].scmOP.E1y, oc[dummy1].scmOP.E1z, oc[dummy1].scmOP.E1y, true, out XW1, out YW1, out ZW1);

                    oc[dummy1].scmOP.W1x = XW1;
                    oc[dummy1].scmOP.W1y = YW1;
                    oc[dummy1].scmOP.W1z = ZW1;
                    #endregion  
                }

                BreakPointA:
                //Calculating the Tire Loaded Radius
                oc[dummy1].TireLoadedRadius = (l_K1y - l_W1y) + (_vehicleForMcPherson.CW[Identifier - 1] / tire.TireRate);

                //Calculating The Final Ride Height 
                oc[dummy1].FinalRideHeight = Math.Abs((oc[dummy1].scmOP.W1y) - l_RideHeightRefy + (_vehicleForMcPherson.CW[Identifier - 1] / tire.TireRate));

                if (_vehicleForMcPherson.Vehicle_Results_Tracker == 0)
                {
                    oc[dummy1].FinalRideHeight_1 = oc[dummy1].FinalRideHeight;
                }

                oc[dummy1].FinalRideHeight_ForTrans = (oc[dummy1].scmOP.W1y) - l_RideHeightRefy + (_vehicleForMcPherson.CW[Identifier - 1] / tire.TireRate); // This value of Ride will represent represent the relative and not absolute value of Ride Height. 

                // This is necessary to translate all the coordinate to the ground by an amount equal to the negative of Ride Height

                //if (!RecalculateSteering)
                //{
                    //Re-assigning the the new coordinates to initial coordinates so that the loop carries on
                    AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                    L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z; 
                //}

                if ((MotionExists) && dummy1 != 0 && (Identifier != 3 && Identifier != 4) && (_vehicleForMcPherson.vehicle_Motion.SteeringExists))  
                {
                    ///<summary>
                    ///This IF loop takes care of 2 things and is used ONLY for the Front in the event of SteeringExists is true
                    ///Calculating the new Spindel End, Steering Upright and Contact Patch Coordinates which is done inside the IF part of the loop
                    ///Calculating the Outboard and Inboard Points Seperately for the Front.
                    /// </summary>

                    if (!RecalculateSteering)
                    {
                        //
                        //Calculating the new coordinates of the Toe Rod Inboard and Outboard End
                        //
                        ToeRod_Steering(oc[dummy1], dummy1, _vehicleForMcPherson, wa);

                        //
                        // Calculating the Delta Camber due to steering 
                        //
                        CalculateDCamberDToe_Steering(oc[dummy1], _vehicleForMcPherson, dummy1);

                        //
                        // Calculating the New Camber due to Steering
                        //
                        CalculateNewCamberAndToe_Steering(oc, dummy1, _vehicleForMcPherson);

                        //
                        // Calculating the new Wheel Spindle End due to Steering
                        //
                        RotateSpindleVector(dCamber_Steering, dToe_Steering, true, l_J1x, l_J1y, l_J1z, l_E1x, l_E1y, l_E1z, L1x, L1y, L1z, out oc[dummy1].scmOP.L1x, out oc[dummy1].scmOP.L1y, out oc[dummy1].scmOP.L1z);

                        //
                        //Recalculting the Contact Patch due to steering
                        //
                        ContactPatch_Steering(oc[dummy1]);
                        double DeltaWheelDef_Steering;
                        DeltaWheelDef_Steering = oc[dummy1].scmOP.W1y - l_W1y;
                        scm.WheelDeflection_Steering[dummy1] = -DeltaWheelDef_Steering; 
                    }
                    else
                    {
                        /////<remarks>
                        /////Call for the finding the new coordinates of the Outboard points will be made here
                        ///// </remarks>
                        //WheelCentre_Steering(oc, _vehicleKinematicsDWSolver, dummy1);

                        //UBJandLBJ_Steering(oc, dummy1);

                        //Pushrod_Steering(oc, dummy1);
                    }

                    //CalculateSpringDeflection_ForSteering_Front(oc,Identifier, DeltaWheelDef_Steering, oc[dummy1].FinalMR, dummy1);
                    //CalculateAngleOfRotationOrDamperLength(oc[dummy1], MotionExists, dummy1, damper);

                    //InitializeRotationMatrices(Identifier, oc[dummy1]);
                    //BellCrankPoints(oc[dummy1]);

                    l_M1x = oc[dummy1].scmOP.M1x; l_M1y = oc[dummy1].scmOP.M1y; l_M1z = oc[dummy1].scmOP.M1z;

                    AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                    L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;

                }

                _vehicleForMcPherson.SuspensionIsSolved = true;

                if (MotionExists)
                {
                    dummy1++;
                }
            }

            #endregion

            #region Initializing loop variables for other Outputs
            if (MotionExists)
            {
                N = _vehicleForMcPherson.vehicle_Motion.Final_WheelDeflectionsY.Count;
                dummy2 = 0;
            }
            else if (!MotionExists)
            {
                N = 200;
                dummy2 = 0;
            }
            #endregion

            #region For Loop for other Outputs

            for (a1 = 1; a1 < N; a1++)
            {
                // This variable keeps track of the total value or magnitude of the Spring Deflection. 
                oc[dummy2].Corrected_SpringDeflection_1 += oc[dummy2].Corrected_SpringDeflection; // This 
                                                                                                  // This variable keeps track of the total value or magnitude of the Spring Deflection. 
                oc[dummy2].Corrected_WheelDeflection_1 = oc[dummy2].Corrected_WheelDeflection - oc[dummy2].Corrected_WheelDeflection_1;


                if (dummy2 != 0 && ((MotionExists) && ((Identifier == 3 || Identifier == 4) || ((Identifier == 1 || Identifier == 2) && (!_vehicleForMcPherson.vehicle_Motion.SteeringExists)))))
                {
                    CalculatenewCamberAndToe_Rear(oc, dummy2, _vehicleForMcPherson, Identifier);

                }
                else if (!MotionExists)
                {
                    CalculatenewCamberAndToe_Rear(oc, dummy2, _vehicleForMcPherson, Identifier);

                }




                #region Coordinate Translations
                ///<remarks>
                ///Need this if loop here because otherwise, this will Vehicle will be translated by the InputOrigin as many times as there are for loops 
                /// </remarks>
                if (MotionExists)
                {
                    TranslateToRequiredCS(scm, oc[dummy2], _vehicleForMcPherson, MotionExists, 0, 0, 0, 0, 0, 0);
                }
                else if (!MotionExists)
                {
                    TranslateToRequiredCS(scm, oc[dummy2], _vehicleForMcPherson, MotionExists, _vehicleForMcPherson.sc_FL.InputOriginX, _vehicleForMcPherson.sc_FL.InputOriginY, _vehicleForMcPherson.sc_FL.InputOriginZ,
                        _vehicleForMcPherson.OutputOrigin_x, _vehicleForMcPherson.OutputOrigin_y, _vehicleForMcPherson.OutputOrigin_z);
                }
                #endregion


                //New Non Suspended Mass CG Calculation
                oc[dummy2].New_NonSuspendedMassCoGx = (oc[dummy2].scmOP.K1x + oc[dummy2].scmOP.L1x) / 2;
                oc[dummy2].New_NonSuspendedMassCoGy = (oc[dummy2].scmOP.K1y + oc[dummy2].scmOP.L1y) / 2;
                oc[dummy2].New_NonSuspendedMassCoGz = (oc[dummy2].scmOP.K1z + oc[dummy2].scmOP.L1z) / 2;

                //// Passing the Corner Weight to the Outputclass
                //oc[dummy2].CW = _vehicleForMcPherson.CW[Identifier - 1];
                //oc[dummy2].CW_1 = _vehicleForMcPherson.CW[Identifier - 1];

                #region Passing the Motion Ratio to the Output Class
                oc[dummy2].InitialMR = scm.InitialMR;
                oc[dummy2].Initial_ARB_MR = scm.Initial_ARB_MR;
                #endregion

                //Calculating the Suspension Wishbone Forces
                //Unit Vectors
                double UV_D2E2x, UV_D2E2y, UV_D2E2z, UV_C2E2x, UV_C2E2y, UV_C2E2z, UV_M2N2x, UV_M2N2y, UV_M2N2z, UV_J2Jox, UV_J2Joy, UV_J2Joz;

                //Calculating the Unit vectors
                UV_D2E2x = (oc[dummy2].scmOP.D1x - oc[dummy2].scmOP.E1x) / scm.LowerFrontLength;
                UV_D2E2y = (oc[dummy2].scmOP.D1y - oc[dummy2].scmOP.E1y) / scm.LowerFrontLength;
                UV_D2E2z = (oc[dummy2].scmOP.D1z - oc[dummy2].scmOP.E1z) / scm.LowerFrontLength;
                UV_C2E2x = (oc[dummy2].scmOP.C1x - oc[dummy2].scmOP.E1x) / scm.LowerRearLength;
                UV_C2E2y = (oc[dummy2].scmOP.C1y - oc[dummy2].scmOP.E1y) / scm.LowerRearLength;
                UV_C2E2z = (oc[dummy2].scmOP.C1z - oc[dummy2].scmOP.E1z) / scm.LowerRearLength;
                UV_M2N2x = (oc[dummy2].scmOP.N1x - oc[dummy2].scmOP.M1x) / scm.ToeLinkLength;
                UV_M2N2y = (oc[dummy2].scmOP.N1y - oc[dummy2].scmOP.M1y) / scm.ToeLinkLength;
                UV_M2N2z = (oc[dummy2].scmOP.N1z - oc[dummy2].scmOP.M1z) / scm.ToeLinkLength;
                UV_J2Jox = (oc[dummy2].scmOP.JO1x - oc[dummy2].scmOP.J1x) / scm.PushRodLength;
                UV_J2Joy = (oc[dummy2].scmOP.JO1y - oc[dummy2].scmOP.J1y) / scm.PushRodLength;
                UV_J2Joz = (oc[dummy2].scmOP.JO1z - oc[dummy2].scmOP.J1z) / scm.PushRodLength;

                // Matrix A - Unit Vectors and Cross Products 

                double[,] matrixa;
                matrixa = new double[3, 3];
                matrixa[0, 0] = UV_D2E2x;
                matrixa[1, 0] = UV_D2E2y;
                matrixa[2, 0] = UV_D2E2z;
                matrixa[0, 1] = UV_C2E2x;
                matrixa[1, 1] = UV_C2E2y;
                matrixa[2, 1] = UV_C2E2z;
                matrixa[0, 2] = UV_M2N2x;
                matrixa[1, 2] = UV_M2N2y;
                matrixa[2, 2] = UV_M2N2z;

                //Unit Matrix 
                double[,] unitmatrixa;
                unitmatrixa = new double[3, 3];
                for (i = 0; i < 3; ++i)
                {
                    for (j = 0; j < 6; ++j)
                    {
                        if (j == i)
                        {
                            unitmatrixa[i, j] = 1;
                        }
                    }
                }

                //Pseudo Matrix
                double psuedo, psuedo2;

                //Inverse of Matrix A
                for (j = 0; j < 3; j++)
                {
                    psuedo = matrixa[j, j];

                    for (i = 0; i < 3; i++)
                    {
                        matrixa[j, i] = matrixa[j, i] / psuedo;
                        unitmatrixa[j, i] = unitmatrixa[j, i] / psuedo;

                    }

                    for (k = 0; k < 3; k++)
                    {
                        if (k == j)
                        {

                        }
                        else
                        {
                            psuedo2 = matrixa[k, j];
                            for (i = 0; i < 3; i++)
                            {
                                matrixa[k, i] = matrixa[k, i] - (matrixa[j, i] * psuedo2);
                                unitmatrixa[k, i] = unitmatrixa[k, i] - (unitmatrixa[j, i] * psuedo2);

                            }
                        }
                    }
                }


                //Forces and Moments generated by the Front Left Tire (Considering Braking + Left Turn)
                double LatF, LongF, MOMx, MOMy, MOMz;
                LatF = LatForce;
                LongF = LongForce;
                MOMx = ((LongF * oc[dummy2].scmOP.W1y / 1000) - (oc[dummy2].scmOP.W1z * (oc[dummy2].CW + VerticalForce) / 1000));
                ///<remarks>
                /// Y axis for my software is the vertixal Axis so I have added Mz to the Moment about the Y Axis as Mz is the Moment about the vertical axis as defined by the User
                /// </remarks>
                MOMy = ((LatF * oc[dummy2].scmOP.W1z / 1000) - (LongF * oc[dummy2].scmOP.W1x / 1000)) + Mz;
                ///<remarks>
                ///Z axis for my software is the Longitudinal Axis so I have added Mx to the Moment about the Z Axis as Mx is the Moment about the Longitudinal axis as defined by the User
                ///</remarks>
                MOMz = (((oc[dummy2].CW + VerticalForce) * oc[dummy2].scmOP.W1x / 1000) - (LatF * oc[dummy2].scmOP.W1y / 1000)) + Mx;

                //Matrx B
                double[,] matrixb;
                matrixb = new double[3, 1];
                matrixb[0, 0] = LatF;
                ///<remarks>The <c>oc[dummy2].deltaNet_CornerWeight</c> is already included in the <c>oc[dummy2].CW</c> <seealso cref="VehicleModel.ComputeVehicleModel_SummationOfResults(Vehicle)"/> </remarks>
                matrixb[1, 0] = oc[dummy2].CW + (VerticalForce /*- oc[dummy2].deltaNet_CornerWeight*/);
                matrixb[2, 0] = LongF;


                //Matrix X
                double X;
                double[,] matrixX;
                matrixX = new double[3, 1];

                for (j = 0; j <= 2; j++)
                {
                    X = 0;
                    for (i = 0; i <= 2; i++)
                    {
                        X += (unitmatrixa[j, i] * matrixb[i, 0]);
                    }
                    matrixX[j, 0] = X;
                }


                oc[dummy2].LowerFront = (matrixX[0, 0]); // Lower Front
                oc[dummy2].LowerRear = (matrixX[1, 0]); // Lower Rear
                oc[dummy2].ToeLink = (matrixX[2, 0]); // Toe Link
                oc[dummy2].PushRod = (oc[dummy2].Corrected_SpringDeflection * spring.SpringRate);

                //Forces in Each Pick Up Points
                oc[dummy2].LowerFront_x = oc[dummy2].LowerFront * UV_D2E2x;
                oc[dummy2].LowerFront_y = oc[dummy2].LowerFront * UV_D2E2y;
                oc[dummy2].LowerFront_z = oc[dummy2].LowerFront * UV_D2E2z;
                oc[dummy2].LowerRear_x = oc[dummy2].LowerRear * UV_C2E2x;
                oc[dummy2].LowerRear_y = oc[dummy2].LowerRear * UV_C2E2y;
                oc[dummy2].LowerRear_z = oc[dummy2].LowerRear * UV_C2E2z;
                oc[dummy2].ToeLink_x = oc[dummy2].ToeLink * UV_M2N2x;
                oc[dummy2].ToeLink_y = oc[dummy2].ToeLink * UV_M2N2y;
                oc[dummy2].ToeLink_z = oc[dummy2].ToeLink * UV_M2N2z;
                oc[dummy2].PushRod_x = oc[dummy2].PushRod * UV_J2Jox;
                oc[dummy2].PushRod_y = oc[dummy2].PushRod * UV_J2Joy;
                oc[dummy2].PushRod_z = oc[dummy2].PushRod * UV_J2Joz;

                if (MotionExists)
                {
                    dummy2++;
                }

            }

            #endregion


        }

        #endregion 
        #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Spatial.Euclidean;


namespace Coding_Attempt_with_GUI
{
    public class VehicleModel
    {
        /// <summary>
        /// 
        /// </summary>
        double a1, a2, b1, b2;
        /// <summary>
        /// <value> Spring Rate at the Wheel of each corner</value>
        /// </summary>
        double Kflw, Kfrw, Krlw, Krrw;
        /// <summary>
        /// Temporary variable to hold the Corner Weights
        /// </summary>
        double[] temp_CW = new double[4];
        /// <summary>
        /// Lateral and Longitudinal distances of the corners from the CoG
        /// </summary>
        double X_fl, Y_fl, d_fl, X_fr, Y_fr, d_fr, X_rl, Y_rl, d_rl, X_rr, Y_rr, d_rr;
        /// <summary>
        /// 
        /// </summary>
        double VehicleWeight;
        /// <summary>
        /// 
        /// </summary>
        double New_Wheelbase_Left, New_Wheelbase_Right, New_AvgWheelBase, New_Trackwidth_Front, New_Trackwidth_Rear;
        /// <summary>
        /// 
        /// </summary>
        double Previous_TorquePinion;
        /// <summary>
        /// <value> X represents the 7x1 Matrix which contains the solution of the 7 Degrees of Freedom of the Vehicle: Roll Pitch Heave and 4 tire Deflections </value>
        /// </summary>
        Vector<double> X;
        /// <summary>
        /// <value> Beta represents the Spring Deflections of each corner </value>
        /// </summary>
        Vector<double> Beta;
        /// <summary>
        /// Temporary variables to store the Motion Ratio of each corner
        /// </summary>
        double MR_FL, MR_FR, MR_RL, MR_RR;
        /// <summary>
        /// Rear Left Ride Height used only and only for the Ride Height Changes induced in the Rear due to Setup Change
        /// </summary>
        public double DZ3 { get; set; } = 0;
        /// <summary>
        /// Rear Right Ride Height used only and only for the Ride Height Changes induced in the Rear due to Setup Change
        /// </summary>
        public double DZ4 { get; set; } = 0;


        #region Method to calcualte the Vertical and Horizontal distances of the corners from the Vehicel CoG
        /// <summary>
        /// Calculates the distances of the Corners from the CoG. 
        /// </summary>
        /// <param name="_scmFL">Front Left Suspension Coordinates</param>
        /// <param name="_scmFR">Front Right Suspension Coordinates</param>
        /// <param name="_scmRL">Rear Left Suspension Coordinates</param>
        /// <param name="_scmRR">Rear Right Suspension Coordinates</param>
        /// <param name="_vehicleLever">Object of the Vehicle Class</param>
        /// <param name="Index"></param>
        public void CalculateLevers(SuspensionCoordinatesMaster _scmFL, SuspensionCoordinatesMaster _scmFR, SuspensionCoordinatesMaster _scmRL, SuspensionCoordinatesMaster _scmRR, Vehicle _vehicleLever, int Index)
        {
            ///<remarks>
            ///
            /// This function calculates the distances of the four corners of the vehicle to the CoG. These distances act as the levers through which the momentd act
            /// Only the TOp View is considered and hence the term "Y_" is used to represent the longitudinal distances of the corners from the CoG. 
            /// 
            /// </remarks>

            #region This set of remarks is irrelevant now as I have rectified the flaw which it pointed out. 
            /////<remarks>
            /////
            ///// I have used L1z to calculate the vertical distance (in TopView) of the 4 corners of the vehicle. I should actually be using the contact patch coordinate. But there is some unexplainable error in the calculation
            ///// of the contact patch. Consider the Left corner of the vehicle: When the Wheel is steered completely to the right, then for a wheel spindle end Z coordinate of about 40mm I get about 18mm as the Z coordinate of the C
            ///// contact Patch. But when the wheel is steered completely to the left, then for a wheel spindle Z coordinate of about -40mm I get only -5.6mm of wheel spindle coordinate. This maybe right, I don't know, but this 
            ///// is not the result I get in OptimumDynamics. So I have used L1z instead of W1z. This is a stopgap method but this is getting me better results than with W1z. Need to see the actual corner weight variation that 
            ///// we get with Orion's car.
            ///// 
            ///// </remarks> 
            #endregion

            X_fl = Math.Abs(_scmFL.W1x - _vehicleLever.chassis_vehicle.SuspendedMassCoGx);
            Y_fl = Math.Abs(_scmFL.W1z - _vehicleLever.chassis_vehicle.SuspendedMassCoGz);

            X_fr = Math.Abs(_scmFR.W1x - _vehicleLever.chassis_vehicle.SuspendedMassCoGx);
            Y_fr = Math.Abs(_scmFR.W1z - _vehicleLever.chassis_vehicle.SuspendedMassCoGz);

            X_rl = Math.Abs(_scmRL.W1x - _vehicleLever.chassis_vehicle.SuspendedMassCoGx);
            Y_rl = Math.Abs(_scmRL.W1z - _vehicleLever.chassis_vehicle.SuspendedMassCoGz);

            X_rr = Math.Abs(_scmRR.W1x - _vehicleLever.chassis_vehicle.SuspendedMassCoGx);
            Y_rr = Math.Abs(_scmRR.W1z - _vehicleLever.chassis_vehicle.SuspendedMassCoGz);

            d_fl = Math.Sqrt(Math.Pow(X_fl, 2) + Math.Pow(Y_fl, 2));

            d_fr = Math.Sqrt(Math.Pow(X_fr, 2) + Math.Pow(Y_fr, 2));

            d_rl = Math.Sqrt(Math.Pow(X_rl, 2) + Math.Pow(Y_rl, 2));

            d_rr = Math.Sqrt(Math.Pow(X_rr, 2) + Math.Pow(Y_rr, 2));

            New_Trackwidth_Front = X_fl + X_fr;
            New_Trackwidth_Rear = X_rl + X_rr;
            _vehicleLever.New_TrackF[Index] = New_Trackwidth_Front;
            _vehicleLever.New_TrackR[Index] = New_Trackwidth_Rear;


            New_Wheelbase_Left = ((Y_fl + Y_rl));
            New_Wheelbase_Right = (Y_fr + Y_rr);
            New_AvgWheelBase = (New_Wheelbase_Left + New_Wheelbase_Right) / 2;

            _vehicleLever.New_WheelBase[Index] = New_AvgWheelBase;
        }
        #endregion

        #region Method to calculate the Corner Weights 
        public void ComputeVehicleModel_Fz(double _vehicleWeight, double _rollAngle, out double _f1, out double _f2, out double _f3, out double _f4)
        {
            ///<summary>
            ///This method employs the conditions of Static Equilibrium to obtain 3 equation of motion. 
            ///It then imposes a final condition: Front Roll angle = Rear Roll angle = Average Roll angle. using this final condition it calculates the Corner Weights. 
            ///</summary>

            ///<remarks>
            ///This is the coefficient matrix or the LHS
            ///</remarks

            var LHS = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {  (1),     (1),     (1),    (1) },
                { (X_fl), -(X_fr),  (X_rl),-(X_rr) },
                {-(Y_fl), -(Y_fr),  (Y_rl), (Y_rr) },
                {  (-1),    (1),     (0),    (0) }
            });

            ///<remarks>
            ///This is the Force and Moment Equilibrium Matrix or the RHS
            /// </remarks>
            var RHS = Vector<double>.Build.Dense(new double[] { _vehicleWeight * 9.81, 0, 0, (X_fl + X_fr) * _rollAngle * Kflw });

            var CornerWeights = LHS.Solve(RHS);

            _f1 = CornerWeights[0];
            _f2 = CornerWeights[1];
            _f3 = CornerWeights[2];
            _f4 = CornerWeights[3];
        }
        #endregion

        private double CalculateWheelDeflections(double _cornerWeight, double _rideRate)
        {
            double _wheelDeflection = 0;

            _wheelDeflection = _cornerWeight / _rideRate;

            if (Double.IsInfinity(_wheelDeflection))
            {
                return 0;
            }
            else return _wheelDeflection;
        }

        private void GetMotionRatio(Vehicle _vMR, int _i)
        {
            if (!_vMR.SuspensionIsSolved)
            {
                MR_FL = _vMR.sc_FL.MotionRatio(_vMR.McPhersonFront, _vMR.McPhersonRear, _vMR.PullRodIdentifierFront, _vMR.PullRodIdentifierRear, 1);
                MR_FR = _vMR.sc_FR.MotionRatio(_vMR.McPhersonFront, _vMR.McPhersonRear, _vMR.PullRodIdentifierFront, _vMR.PullRodIdentifierRear, 2);
                MR_RL = _vMR.sc_RL.MotionRatio(_vMR.McPhersonFront, _vMR.McPhersonRear, _vMR.PullRodIdentifierFront, _vMR.PullRodIdentifierRear, 3);
                MR_RR = _vMR.sc_RR.MotionRatio(_vMR.McPhersonFront, _vMR.McPhersonRear, _vMR.PullRodIdentifierFront, _vMR.PullRodIdentifierRear, 4);
            }
            else
            {
                MR_FL = _vMR.oc_FL[_i].FinalMR;
                MR_FR = _vMR.oc_FR[_i].FinalMR;
                MR_RL = _vMR.oc_RL[_i].FinalMR;
                MR_RR = _vMR.oc_RR[_i].FinalMR;
            }

        }

        #region LHS or Stiffness Matrix for the 7DOF Model
        private Matrix<double> InitializeStiffnessMatrix(Vehicle _vSM, bool _sTEERINGPASS, int i)
        {
            Matrix<double> _k;

            ///<summary>
            ///This method is used to initialize the stiffness matrix everytime the Model is Computed. This is because, the Spring Rate at the wheel keeps changing because the Motion Ratio keeps changing with the Kinematics. 
            ///This matrix is basically the complete coefficient matrix of the 7-DOF Vehicle Model 
            /// </summary>

            ///<param name="_vSM">
            ///This is the object of the Vehicle Class. SM signifies that the vehicle object is used for generating the Stiffness mtrix
            ///</param>

            /////<param name="scm">
            /////The parameter scm is a LIST of SuepsnsionCoordinateMaster objects. 
            /////It is initialized as LIST only so that the arguement list of this method is not overcrowded with 4 different SuspensionCoordinateMasterItems each represented one corner of the Vehicle 
            ///// </param>

            /////<param name="spring">
            /////The parameter spring is a LIST of Spring Objects. 
            /////Again, the only reason it is initilized as a LIST is because, I need the spring objects of all the 4 courners of the Vehicle and so it doesn't make sense to add all of them as arguements to the methods as this will overcrowd it. 
            ///// </param>

            ///<remarks>
            ///Calculating the Motion Ratio. 
            ///This method is necessary ONLY during the calculation of the static values, that is, when this model is executed for the first time 
            /// </remarks>
            GetMotionRatio(_vSM, i);


            ///<param name="Kw">
            ///This is the Spring Rate at the Wheel. It is calculated using the Motion Ratio and the Spring Rate for each corner of the Vehicle. 
            /// </param>
            Kflw = _vSM.spring_FL.SpringRate * (Math.Pow(MR_FL, 2));
            Kfrw = _vSM.spring_FR.SpringRate * (Math.Pow(MR_FR, 2));
            Krlw = _vSM.spring_RL.SpringRate * (Math.Pow(MR_RL, 2));
            Krrw = _vSM.spring_RR.SpringRate * (Math.Pow(MR_RR, 2));

            if (_sTEERINGPASS)
            {
                _k = Matrix<double>.Build.DenseOfArray(new double[,]
        {
{ -(Kflw+Kfrw+Krlw+Krrw),                              ((-X_fl*Kflw)+(X_fr*Kfrw)+(-X_rl*Krlw)+(X_rr*Krrw)),                     ((Y_fl*Kflw)+(Y_fr*Kfrw)+(-Y_rl*Krlw)+(-Y_rr*Krrw)),                      (-Kflw),               (-Kfrw),       (-Krlw),                 (-Krrw)},
{  ((-X_fl*Kflw)+(X_fr*Kfrw)+(-X_rl*Krlw)+(X_rr*Krrw)),-((Kflw*X_fl*X_fl)+(Kfrw*X_fr*X_fr)+(Krlw*X_rl*X_rl)+(Krrw*X_rr*X_rr)),  ((X_fl*Y_fl*Kflw)-(X_fr*Y_fr*Kfrw)-(X_rl*Y_rl*Krlw)+(X_rr*Y_rr*Krrw)),   (-X_fl*Kflw),         (X_fr*Kfrw),   (-X_rl*Krlw),           (X_rr*Krrw)},
{  ((Y_fl*Kflw)+(Y_fr*Kfrw)+(-Y_rl*Krlw)+(-Y_rr*Krrw)),(((X_fl*Y_fl*Kflw)-(X_fr*Y_fr*Kfrw)-(X_rl*Y_rl*Krlw)+(X_rr*Y_rr*Krrw))),-(((Kflw*Y_fl*Y_fl)+(Kfrw*Y_fr*Y_fr)+(Krlw*Y_rl*Y_rl)+(Krrw*Y_rr*Y_rr))), (Y_fl*Kflw),          (Y_fr*Kfrw),   (-Y_rl*Krlw),           (-Y_rr*Krrw)},
{  (-Kflw),                                           -(X_fl*Kflw),                                                             (Y_fl*Kflw),                                                       (-Kflw-_vSM.tire_FL.TireRate),  (0),            (0),                    (0)},
{  (-Kfrw),                                            (X_fr*Kfrw),                                                             (Y_fr*Kfrw),                                                                (0),    (-Kfrw-_vSM.tire_FL.TireRate), (0),                    (0)},
{  (-Krlw),                                           -(X_rl*Krlw),                                                            -(Y_rl*Krlw),                                                                (0),                   (0),  (-Krlw-_vSM.tire_FL.TireRate),    (0)},
{  (-Krrw),                                            (X_rr*Krrw),                                                            -(Y_rr*Krrw),                                                                (0),                   (0),            (0),     (-Krrw-_vSM.tire_FL.TireRate)},
        }
        );
            }
            else
            {
                ///<remarks>
                ///Using a completely different concept here for th RHS (and also Stiffness Matrix) Out of the 7DOFs, I am considering the Input Defelction to be the average Heave which I will pass to this new 6DOF and hence 6x1 Matrix
                ///</remarks>
                _k = Matrix<double>.Build.DenseOfArray(new double[,]
{{  -((Kflw*X_fl*X_fl)+(Kfrw*X_fr*X_fr)+(Krlw*X_rl*X_rl)+(Krrw*X_rr*X_rr)),  ((X_fl*Y_fl*Kflw)-(X_fr*Y_fr*Kfrw)-(X_rl*Y_rl*Krlw)+(X_rr*Y_rr*Krrw)),                                      (X_fl*Kflw),         (-X_fr*Kfrw),   (X_rl*Krlw),           (-X_rr*Krrw)},
{  (((X_fl*Y_fl*Kflw)-(X_fr*Y_fr*Kfrw)-(X_rl*Y_rl*Krlw)+(X_rr*Y_rr*Krrw))),-(((Kflw*Y_fl*Y_fl)+(Kfrw*Y_fr*Y_fr)+(Krlw*Y_rl*Y_rl)+(Krrw*Y_rr*Y_rr))),                                    (-Y_fl*Kflw),        (-Y_fr*Kfrw),   (Y_rl*Krlw),            (Y_rr*Krrw)},
{                                       -(X_fl*Kflw),                                                             (Y_fl*Kflw),                                                       (Kflw+_vSM.tire_FL.TireRate),  (0),            (0),                    (0)},
{                                        (X_fr*Kfrw),                                                             (Y_fr*Kfrw),                                                                (0),    (Kfrw+_vSM.tire_FL.TireRate), (0),                    (0)},
{                                       -(X_rl*Krlw),                                                            -(Y_rl*Krlw),                                                                (0),                   (0),  (Krlw+_vSM.tire_FL.TireRate),    (0)},
{                                        (X_rr*Krrw),                                                            -(Y_rr*Krrw),                                                                (0),                   (0),            (0),     (Krrw+_vSM.tire_FL.TireRate)},
}
);

            }

            return _k;

        }
        #endregion

        #region RHS or Results matrix for the 7DOF Model

        /// <summary>
        /// Method to Assign the Ride Height Change induced in the Rear due to the Setup Changes 
        /// </summary>
        /// <param name="_DZ3">Rear Left Ride Height Change</param>
        /// <param name="_DZ4">Rear Right Ride Height Change</param>
        public void AssignRearRideHeightChanges(double _DZ3, double _DZ4)
        {
            DZ3 = _DZ3;

            DZ4 = _DZ4;
        }

        /// <summary>
        ///This method initializes the RHS Column Matrix. 
        ///It also decides whether or not to inlucde the conditions required to calcualte parameters for Static Conditions. That is, if the index is 0, then it implies that the values are being calculated for the first time and hence, the 
        ///conditions to evaluate the static values are used. These conditions are written below. 
        /// </summary>
        /// <param name="_vRHS"></param>
        /// <param name="_dZ1">This represents the change in wheel Centre Height of the Front Left Corner It is 0 for static conditions</param>
        /// <param name="_dZ2">This represents the change in wheel Centre Height of the Front Right Corner It is 0 for static conditions<</param>
        /// <param name="_dZ3">This represents the change in the Ride Height or WHeel Centre Height of the Rear Left Corner. Will be 0 for Actual Steering Conditions and some value when <see cref="SetupChange"/> Calculations are being done</param>
        /// <param name="_dZ4">This represents the change in the Ride Height or WHeel Centre Height of the Rear Left Corner. Will be 0 for Actual Steering Conditions and some value when <see cref="SetupChange"/> Calculations are being done</param>
        /// <param name="_dZW">This represents the change in Average Heave of the Vehicle. This value should be set to ZERO dueing the Steering Pass<</param>
        /// <param name="i">Index or Percentage of Motion at which calculation is done</param>
        /// <returns>Returns a 7x7 Vector containing the RHS of the Equation</returns>
        private Vector<double> InitializeResultMatrix(Vehicle _vRHS, double _dZ1, double _dZ2, double _dZ3, double _dZ4, double _dZW, int i, bool _steeringPass)
        {
            Vector<double> _rhs;
            ///<summary>
            ///Variable to represent the Vehicle Weight
            ///If the index "i" == 0, then it implies thatr the calculations are done to initlaize the Vehicle, that is, to calcualte the static values. In this case, the Vehicle Weight is required and hence it is initialized as shown inside the if loop
            ///If index  i!=0, then it implies that results are beign calculated for a change in equilibrium condition. Hence at this point, the total Vehicle Weight is not required because we are only dealing with Deltas and the summation of change 
            ///of forces is '0' for equilibrium to be maintained.The moments of the changes of force about the axes is also 0. The Vehicle Weight is therefore initialized to 0
            ///By including the Vehicle Weight and making it 0 when reqauired, I can use the same RHS matrix for both static value calculation and also delta calculation
            ///The reason this works is that in static conditions the values of dZ1 and dZ2 are 0. 
            /// </summary>
            double P;
            if (i == 0)
            {
                P = (_vRHS.chassis_vehicle.SuspendedMass + _vRHS.NSM_FL + _vRHS.NSM_FR + _vRHS.NSM_RL + _vRHS.NSM_RR) * 9.81;
            }
            else P = 0;

            ///<summary>
            ///Variables to represnent the Unsprung Masses of each of the corners
            ///Same theory as total Vehicel Weight and same reason for making it 0 as total Vehicle Weight 
            ///</summary>
            double Fu1, Fu2, Fu3, Fu4;
            if (i == 0)
            {
                Fu1 = _vRHS.NSM_FL * 9.81; Fu2 = _vRHS.NSM_FR * 9.81; Fu3 = _vRHS.NSM_RL * 9.81; Fu4 = _vRHS.NSM_RR * 9.81;
            }
            else
                Fu1 = Fu2 = Fu3 = Fu4 = 0;

            ///<summary>
            ///Variables to represent the eccentricity of the point of application of Vehicle Weight 
            ///<remarks>
            ///According to me, eccenticity is required only if the Moment Equiliobrium is calculated about the Centre of the Vehicle. 
            ///But we are calculating the moments about the CoG. Hence, the moment due to the Weight of the Vehicle is 0. Hence, no need of eccentricity
            ///NOT DELETING these variables for now. 
            ///</remarks>
            ///</summary>
            double ex, ey;

            if (_steeringPass)
            {
                _rhs = Vector<double>.Build.Dense(new double[]
            {
                ( (P-Fu1-Fu2-Fu3-Fu4)                                -  ((_dZ1     *    Kflw) + (_dZ2    *     Kfrw) + (_dZ3    *     Krlw) + (_dZ4     *    Krrw))),
                (-((X_fl*Fu1)-(X_fr*Fu2)  +  (X_rl*Fu3)-(X_rr*Fu4))  -  ((Kflw * X_fl * _dZ1) - (Kfrw * X_fr * _dZ2) + (Krlw * X_rl * _dZ3) - (Krrw * X_rr * _dZ4))),
                (-(-(Y_fl*Fu1)-(Y_fr*Fu2) +  (Y_rl*Fu3)+(Y_rr*Fu4))  +  ((Kflw * Y_fl * _dZ1) + (Kfrw * Y_fr * _dZ2) - (Krlw * Y_rl * _dZ3) - (Krrw * Y_rr * _dZ4))),
                (-(Fu1)                                              -  (Kflw     *    _dZ1)),
                (-(Fu2)                                              -  (Kfrw     *    _dZ2)),
                (-(Fu3)                                              -  (Krlw     *    _dZ3)),
                (-(Fu4)                                              -  (Krrw     *    _dZ4))
            });
            }
            else
            {
                ///<remarks>
                ///Using a completely different concept here for th RHS (and also Stiffness Matrix) Out of the 7DOFs, I am considering the Input Defelction to be the average Heave which I will pass to this new 6DOF and hence 6x1 Matrix
                ///</remarks>
                _rhs = Vector<double>.Build.Dense(new double[]
            {
                (-((X_fl*Fu1)-(X_fr*Fu2)+(X_rl*Fu3)-(X_rr*Fu4)) - (((-X_fl*Kflw)+(X_fr*Kfrw)+(-X_rl*Krlw)+(X_rr*Krrw))*_dZW) ),
                (-(-(Y_fl*Fu1)-(Y_fr*Fu2)+(Y_rl*Fu3)+(Y_rr*Fu4)) -(((Y_fl*Kflw)+(Y_fr*Kfrw)+(-Y_rl*Krlw)+(-Y_rr*Krrw))*_dZW) ),
                (-(Fu1) + (Kflw *_dZW)),
                (-(Fu2) + (Kfrw *_dZW)),
                (-(Fu3) + (Krlw *_dZW)),
                (-(Fu4) + (Krrw *_dZW))
            });
            }

            return _rhs;
        }
        #endregion

        #region Method to calculate the Spring Displacements or Change in Spring Displacements AND the Tire displacementsor the Change in Tire displacements
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_x">This the 7x1 Matrix containing the 7DOFs. It is important to note that the last 4 DOFs are not the tire deflections but the downward motion of the wheel centre which is -ve for downward unlike the tire deflection which is +ve for downward</param>
        /// <param name="_dZ1">Downward motion of the Front Left Wheel Centre</param>
        /// <param name="_dZ2">Downward motion of the Front Right Wheel Centre</param>
        /// <param name="_dZ3">Downward motion of the Rear Left Wheel Centre</param>
        /// <param name="_dZ4">Downward motion of the Rear Right Wheel Centre</param>
        /// <returns></returns>
        private Vector<double> ComputeVehicleModel_TireDefllection(Vector<double> _x, double _dZ1, double _dZ2, double _dZ3, double _dZ4)
        {
            Vector<double> _betaTire;

            _betaTire = Vector<double>.Build.Dense(new double[]
            {
                    (_dZ1 - _x[3]),
                    (_dZ2 - _x[4]),
                    (_dZ3 - _x[5]),
                    (_dZ4 - _x[6])
            });

            return _betaTire;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_x">This is the 7x1 Matrix containing results of the computed 7DOF Vehicle Mode. The 7 rows represent Heave, Pitch, Roll and the 4 tire defelctions OR the 4 wheel centre downward movement (for Heave).</param>
        /// <param name="_dZ1">This is the change in wheel deflection due to steering OR due to Heave on the FRONT LEFT Corner</param>
        /// <param name="_dZ2">This is the change in wheel deflection due to steering OR due to Heave on the FRONT RIGHT Corner</param>
        /// <param name="_dZW">This is the change in wheel deflection due to steering OR due to Heave on the Rear LEFT Corner</param>
        /// <param name="_dZ4">This is the change in wheel deflection due to steering OR due to Heave on the Rear RIGHT Corner</param>
        /// <param name="_steeringPass">Variable to identify whether it is the Steering Pass or the Heave pass</param>
        /// <returns>Returns the Spring Deflections or the Delta of Spring Deflections</returns>
        private Vector<double> ComputeVehicleModel_SpringDeflections(Vector<double> _x, double _dZ1, double _dZ2, double _dZW/*, double _dZ4*/, bool _steeringPass)
        {
            ///<value>
            ///_beta is the 4x1 Matrix containing the spring displacements of each corner. The spring displacements are calculated based on the Heave, Roll, Pitch, 4 Tire Deflections and the Change in Wheel deflections of Front Left and Front Right due to Steering. 
            ///Since, by using the 7DOFs and the wheel centre height change we actually get the displacement of the spring at the wheel, we need to multiply with the Motion Ratio to calculate the actual spring displacements
            /// </value>
            Vector<double> _beta;

            if (_steeringPass)
            {
                _beta = Vector<double>.Build.Dense(new double[]
            {
                    (_dZ1 - _x[3] - _x[0] - (X_fl*_x[1]) + (Y_fl*_x[2])) * MR_FL,
                    (_dZ2 - _x[4] - _x[0] + (X_fr*_x[1]) + (Y_fr*_x[2])) * MR_FR,
                    (     - _x[5] - _x[0] - (X_rl*_x[1]) - (Y_rl*_x[2])) * MR_RL,
                    (     - _x[6] - _x[0] + (X_rr*_x[1]) - (Y_rr*_x[2])) * MR_RR
            });

            }
            else
            {
                _beta = Vector<double>.Build.Dense(new double[]
            {
                    (_x[2] - _dZW - (X_fl*_x[0]) + (Y_fl*_x[1])) * MR_FL,
                    (_x[3] - _dZW + (X_fr*_x[0]) + (Y_fr*_x[1])) * MR_FR,
                    (_x[4] - _dZW - (X_rl*_x[0]) - (Y_rl*_x[1])) * MR_RL,
                    (_x[5] - _dZW + (X_rr*_x[0]) - (Y_rr*_x[1])) * MR_RR
            });

                //Vector<double> temp_X;
                //temp_X = ComputeVehicleModel_TireDefllection(_x, _dZ1, _dZ2, _dZW, _dZ4);
                _x[2] = -_x[2];
                _x[3] = -_x[3];
                _x[4] = -_x[4];
                _x[5] = -_x[5];
            }

            return _beta;

        }

        #endregion

        #region This method solves a 7 DOF model based on its equilibrium conditions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_vModel">Object of the Vehicle Class</param>
        /// <param name="dZ1">This is the change in wheel deflection due to steering on the FRONT LEFT Corner</param>
        /// <param name="dZ2">This is the change in wheel deflection due to steering on the FRONT RIGHT Corner</param>
        /// <param name="_index"> Represents the Percentage of Motion or the interval which is being calculated for</param>
        /// <returns></returns>
        private Vector<double> ComputeVehicleModel_7DOF(Vehicle _vModel, double dZ1, double dZ2, double dZ3, double dZ4, double dZW, int _index, bool _steeringPass)
        {
            ///<summary>
            ///This method computes 7 degrees of freedom of a Vehicle. 
            ///These are the Heave, Roll, Pitch and the 4 tire deflections. 
            ///The model is computed using a Stiffness Matrix and an RHS Matrix. 
            /// </summary>

            ///<summary>
            ///Using the InitializeStiffnessMatrix to calculate the Stiffness Matrix of the Model. 
            ///Basically, this is the coefficient matrix of the 7DOF model 
            /// </summary>
            var K = InitializeStiffnessMatrix(_vModel, _steeringPass, _index);

            ///<summary>
            ///Using the InitialzeResultMatrix method to calculate the RHS Column Matrix of the Model 
            /// </summary>
            var RHS = InitializeResultMatrix(_vModel, dZ1, dZ2, dZ3, dZ4, dZW, _index, _steeringPass);

            ///<summary>
            ///Calculating the 7 Degrees of Freedom using inverse matrix multiplcation
            /// </summary>
            var _x = K.Solve(RHS);

            return _x;

        }
        #endregion

        #region Confirm if needed and Delete if not 
        private double ComputeVehicleModel_Attitude(double _x1, double _x2, double _base)
        {
            double _attitudeParam = 0;

            #region Will probably not need this now that I am using a 7DOF model 
            //#region Unused Vehicle Model from Jazar'z VD book Maybe will be useful. If not delete
            /////<remarks>
            /////This is the stiffness matrix [k]
            ///// </remarks>
            ///// 

            //var K = Matrix<double>.Build.DenseOfArray(new double[,] {
            //{-((b1*Krlw)+(Krlw/(b1+b2))), (a1*Kflw),  (0),  (0) },
            //{((b2*Kflw)+(Krlw/(b1+b2))),  (a1*Kflw),  (0),  (0) },
            //{(b1*Krlw),                 (-a2*Krlw), (Krlw), (0) },
            //{(-b2*Krlw),                (-a2*Krlw), (0),  (Krlw) } });

            /////<remarks>
            /////This is the Force Matrix [F]
            ///// </remarks>
            //double _roll, _pitch;
            //var F = Vector<double>.Build.Dense(new double[] {(-((Kflw*_x1) + (Krlw*(_x1-_x2)/(b1+b2)))),
            //                                                 (-((Kflw*_x2) - (Krlw*(_x1-_x2)/(b1+b2)))),
            //                                                 (0),
            //                                                 (0)});
            //var x = K.Solve(F);

            //_roll = x[0] * (180 / Math.PI);
            //_pitch = x[1] * (180 / Math.PI);
            //#endregion

            //_attitudeParam = Math.Tan((_x1 - _x2) / (_base));
            #endregion

            return _attitudeParam;
        }
        #endregion

        #region Method to Calculate the location of the CG in Top View
        private void ComputeVehicleModel_CoGTopView(double F1, double F2, double F3, double F4, double Track_avg, double Wheelbase_avg, out double _x, out double _z)
        {

            ///<remarks>
            ///
            /// Force amd Moment coefficient matrix 
            /// 
            /// </remarks>

            var LHS = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {-(F1 + F2), (F3 + F4),   (0),       (0) },
                {    (0),       (0),  -(F1 + F3), (F2 + F4) },
                {    (1),       (1),      (0),       (0) },
                {    (0),       (0),      (1),       (1) }
            });

            var RHS = Vector<double>.Build.Dense(new double[]
                {
                          (0),
                          (0),
                    (Wheelbase_avg),
                       (Track_avg)
                });

            var Result = LHS.Solve(RHS);

            _z = Result[0];
            _x = Track_avg / 2 - Result[2];


        }
        #endregion

        #region Simple Method to Calculate the Angle using ArcTan
        private double AngleOfRotation_Tan(double Perpendicular, double Base)
        {
            ///<summary>
            ///This is a simple method used to calculate the angle of rotation using the arcTangent
            /// </summary>

            double _theta;

            _theta = Math.Atan(Perpendicular / Base);

            return _theta;
        }
        #endregion

        #region Computing the Steering Effort 
        public void ComputeVehicleModel_SteeringEffort(List<OutputClass> _ocEffortFL, List<OutputClass> _ocEffortFR, Vehicle _vehicleSteering)
        {
            double DeltaRideHeightFL, DeltaRideHeightFR;
            double thetaFL, thetaFR;
            double EnergyFL, EnergyFR;
            double TorqueFL, TorqueFR, CurrentTotalTorque_Pinion;

            for (int Index = 0; Index < _ocEffortFL.Count; Index++)
            {
                if (Index == 0)
                {
                    ///<remarks>
                    /// Because no Steering effort or torque for straight ahead condition
                    /// </remarks>
                    _ocEffortFL[Index].SteeringTorque = 0;
                    _ocEffortFL[Index].SteeringEffort = 0;
                }
                else
                {
                    ///<remarks>
                    ///Calcualting the height by which the corners are lifted or raised
                    /// </remarks>
                    DeltaRideHeightFL = _ocEffortFL[Index].scmOP.K1y - _ocEffortFL[/*Index - 1*/ 0].scmOP.K1y;
                    DeltaRideHeightFR = _ocEffortFR[Index].scmOP.K1y - _ocEffortFR[/*Index - 1*/ 0].scmOP.K1y;

                    ///<remarks>
                    ///Calculating the angle of rotation of the left and right corners
                    /// </remarks>
                    thetaFL = AngleOfRotation_Tan(DeltaRideHeightFL, _vehicleSteering.New_TrackF[Index] / 2);
                    thetaFR = AngleOfRotation_Tan(DeltaRideHeightFR, _vehicleSteering.New_TrackF[Index] / 2);

                    ///<remarks>
                    ///Calculating the Energy required to raise or lower the left or right corner of the Vehicle
                    /// </remarks>
                    EnergyFL = _ocEffortFL[Index - 1].CW * DeltaRideHeightFL / 1000;
                    EnergyFR = _ocEffortFR[Index - 1].CW * DeltaRideHeightFR / 1000;


                    ///<remarks>
                    ///Calculating the Torque caused by the change in the heights of the left and right corrners
                    ///The Torque on the left side of the Vehicle is always NEGATIVE as the force is upwards always and hence the rotation is clockwise. Similarly, the Torque on the right side of the Vehicle is always Positive as the force is upwards always and hence the 
                    ///rotation is counter clockwise
                    /// </remarks>
                    TorqueFL = -EnergyFL / thetaFL;
                    TorqueFR = +EnergyFR / thetaFR;
                    CurrentTotalTorque_Pinion = TorqueFL + TorqueFR;


                    double Current_IntermediateTorque, Previous_IntermediateTorque;
                    double Current_SteeringTorque, Previous_SteeringTorque;
                    double delta_SteeringTorque;
                    if (_vehicleSteering.sc_FL.NoOfCouplings == 2)
                    {

                        ///<remarks>
                        ///The last term of the equation below has a 1.571 added to it. This is based on the assumption that the the 2 UV joints in the system are out of of phase by 90 degree or 1.571 radian. 
                        ///For 2 universal joints it is a necessary condition that the 2 Universal Joints have a phase difference of 90 degree fore constant chanfe of angle, velocity and torque between the Input and Output shaft. 
                        /// </remarks>
                        Current_IntermediateTorque = CurrentTotalTorque_Pinion * ((Math.Cos(_ocEffortFL[Index].Angle_IntertermediateOutputShaft)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index].Angle_IntertermediateOutputShaft), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index].Angle_Intermediate + 1.571), 2)))));
                        Current_SteeringTorque = Current_IntermediateTorque * ((Math.Cos(_ocEffortFL[Index].Angle_InputIntermediateShaft)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index].Angle_InputIntermediateShaft), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index].Angle_Steering), 2)))));

                        Previous_IntermediateTorque = Previous_TorquePinion * ((Math.Cos(_ocEffortFL[Index - 1].Angle_IntertermediateOutputShaft)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index - 1].Angle_IntertermediateOutputShaft), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index - 1].Angle_Intermediate + 1.57), 2)))));
                        Previous_SteeringTorque = Previous_IntermediateTorque * ((Math.Cos(_ocEffortFL[Index - 1].Angle_InputIntermediateShaft)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index - 1].Angle_InputIntermediateShaft), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index - 1].Angle_Steering), 2)))));

                        delta_SteeringTorque = Current_SteeringTorque - Previous_SteeringTorque;

                        _ocEffortFL[Index].SteeringTorque = _ocEffortFL[Index - 1].SteeringTorque + delta_SteeringTorque;
                    }
                    else
                    {
                        ///<remarks>
                        ///Calculating the Steering Torque 
                        /// </remarks>
                        //Current_SteeringTorque = CurrentTotalTorque_Pinion * ((Math.Cos(_ocEffortFL[Index].Angle_InputOutputShafts)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index].Angle_InputOutputShafts), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index].Angle_Steering), 2)))));

                        //Previous_SteeringTorque = Previous_TorquePinion * ((Math.Cos(_ocEffortFL[Index - 1].Angle_InputOutputShafts)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index - 1].Angle_InputOutputShafts), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index - 1].Angle_Steering), 2)))));

                        //delta_SteeringTorque = Current_SteeringTorque - Previous_SteeringTorque;

                        delta_SteeringTorque = (CurrentTotalTorque_Pinion - Previous_TorquePinion) * ((Math.Cos(_ocEffortFL[Index].Angle_InputOutputShafts)) / (1 - ((Math.Pow(Math.Sin(_ocEffortFL[Index].Angle_InputOutputShafts), 2)) * (Math.Pow(Math.Cos(_ocEffortFL[Index].Angle_Steering) - Math.Cos(_ocEffortFL[Index - 1].Angle_Steering), 2)))));

                        _ocEffortFL[Index].SteeringTorque = _ocEffortFL[Index - 1].SteeringTorque + delta_SteeringTorque;
                    }
                    Previous_TorquePinion = CurrentTotalTorque_Pinion;

                }
            }

        }
        #endregion

        #region Confirm if needed and Delete if not
        private void InitializeParameters(double SMCoGx, double SMCoGz, double Wheelbase, double TrackAvg, Spring _springFront, Spring _springRear, Chassis _chassis)
        {
            a1 = Math.Abs(SMCoGz);
            a2 = Wheelbase - Math.Abs(a1);
            b1 = (TrackAvg / 2) - SMCoGx;
            ///<remarks>
            /// Technically, the sign should be + instead of - but since, we are using Left Hand coordinate system, -ve COG coordinate implies that COG is offset towards the right. Since, 
            ///offset towards right impplies 
            /// b1 should be more, we are adding -ve sign so that the net value increases
            ///</remarks> 
            b2 = TrackAvg - b1;


            //Kflw = _springFront.SpringRate;
            //Krlw = _springRear.SpringRate;

            VehicleWeight = Math.Abs(_chassis.SuspendedMass + _chassis.NonSuspendedMassFL + _chassis.NonSuspendedMassFR + _chassis.NonSuspendedMassRL + _chassis.NonSuspendedMassRR);
        }
        #endregion

        private void Reset_Deflections(Vehicle _vReset)
        {
            ///<remarks>
            ///Need to add .Insert(i,j) command here to the <value>SpringDeflection_DeltaSteering</value> because the Spring Deflection Deltas are added from 1st position and if I don't add this insert code here then the code will fail 
            ///because of Index Out of Range Exception <seealso cref="ComputeVehicleModel_SummationOfResults"/>
            ///</remarks>

            _vReset.sc_RL.SpringDeflection_DeltSteering.Clear();
            _vReset.sc_RL.SpringDeflection_DeltSteering.Insert(0, 0);
            _vReset.sc_RR.SpringDeflection_DeltSteering.Clear();
            _vReset.sc_RR.SpringDeflection_DeltSteering.Insert(0, 0);

            _vReset.sc_FL.SpringDeflection_DeltSteering.Clear();
            _vReset.sc_FL.SpringDeflection_DeltSteering.Insert(0, 0);
            _vReset.sc_FR.SpringDeflection_DeltSteering.Clear();
            _vReset.sc_FR.SpringDeflection_DeltSteering.Insert(0, 0);

        }

        /// <summary>
        /// The PRIMARY Public Method of the <see cref="VehicleModel"/> class which can be accessed by other classes's objects
        /// </summary>
        /// <param name="_vehicleOP">Object of the Vehicle Class</param>
        /// <param name="_SteeringPass"> Identifier to determine if the Model is being solved for Steering</param>
        /// <param name="_HeaveMotionPass"> Identifier to determine if the Model if being solved for Motion. An explicit variable for Motion is needed because otherwise, the Model will be solved for complete Heave when the auxillary function <c>ChassisCornerMassCalculator</c>
        /// calls it. <seealso cref="ChassisCornerMassCalculator"/> </param>
        /// <param name="i">Represents the Percentage of Motion or the interval which is being calculated for</param>
        public void InitializeVehicleOutputModel(Vehicle _vehicleOP, bool _SteeringPass, bool _HeaveMotionPass, SimulationType _simType)
        {
            Reset_Deflections(_vehicleOP);

            for (int i = 0; i < _vehicleOP.oc_FL.Count; i++)
            {
                
                InitializeParameters(_vehicleOP.chassis_vehicle.SuspendedMassCoGx, _vehicleOP.chassis_vehicle.SuspendedMassCoGz, _vehicleOP.WheelBase, _vehicleOP.TrackAvg, _vehicleOP.spring_FL, _vehicleOP.spring_RL, _vehicleOP.chassis_vehicle);

                ///<remarks> 
                ///The IF loop below is employed because, there is a method called the <c>ChassisCornerMassCalculator</c> in the <c>Vehicle</c> class which uses the <c>InitializeVehicleOutputModel</c> of this class. When the <c>ChassisCornerMassCalculator</c> is called no Suspension
                ///calculations are done and theregore if the Suspension Coordinates within the <c>OutputClass</c> is used, then it will lead all the levers being initialized to 0. 
                /// </remarks>
                if (_vehicleOP.SuspensionIsSolved)
                {
                    CalculateLevers(_vehicleOP.oc_FL[i].scmOP, _vehicleOP.oc_FR[i].scmOP, _vehicleOP.oc_RL[i].scmOP, _vehicleOP.oc_RR[i].scmOP, _vehicleOP, i);
                }
                else CalculateLevers(_vehicleOP.sc_FL, _vehicleOP.sc_FR, _vehicleOP.sc_RL, _vehicleOP.sc_RR, _vehicleOP, i);

                ///<remarks>
                ///Solving the 7DOF model whcih results in Heave, Roll, Pitch and 4 Tire Deflections
                ///The IF loop determines if the Model is being called to evaluate the Heave Phase or the Steering Phase of the Calculations
                ///</remarks>
                ///<remarks>
                ///Solving for the Spring Deflections
                ///</remarks>
                if (_simType == SimulationType.SetupChange)
                {
                    ///<summary>Initialization Pass or First Pass</summary>
                    X = ComputeVehicleModel_7DOF(_vehicleOP, 0, 0, 0, 0, 0, i, _SteeringPass);
                    Beta = ComputeVehicleModel_SpringDeflections(X, 0, 0, 0, _SteeringPass);
                    ComputeVehicleModel_Fz(VehicleWeight, -X[1], out temp_CW[0], out temp_CW[1], out temp_CW[2], out temp_CW[3]);
                    Assign_StaticResults(_vehicleOP, X, Beta, temp_CW);

                    ///<summary>2nd Pass to find the Deltas</summary>
                    X = ComputeVehicleModel_7DOF(_vehicleOP, _vehicleOP.sc_FL.WheelDeflection_Steering[i], _vehicleOP.sc_FR.WheelDeflection_Steering[i], DZ3, DZ4, 0, i, _SteeringPass);
                    Beta = ComputeVehicleModel_SpringDeflections(X, _vehicleOP.sc_FL.WheelDeflection_Steering[i], _vehicleOP.sc_FR.WheelDeflection_Steering[i], 0, _SteeringPass);
                    ComputeVehicleModel_Fz(VehicleWeight, -X[1], out temp_CW[0], out temp_CW[1], out temp_CW[2], out temp_CW[3]);
                    ComputeVehicleModel_NetDelta(_vehicleOP, X, Beta, temp_CW, _SteeringPass, i);

                    break;
                }
                else if (_SteeringPass)
                {
                    DZ3 = DZ4 = 0;
                    X = ComputeVehicleModel_7DOF(_vehicleOP, _vehicleOP.sc_FL.WheelDeflection_Steering[i], _vehicleOP.sc_FR.WheelDeflection_Steering[i], DZ3, DZ4, 0, i, _SteeringPass);
                    Beta = ComputeVehicleModel_SpringDeflections(X, _vehicleOP.sc_FL.WheelDeflection_Steering[i], _vehicleOP.sc_FR.WheelDeflection_Steering[i], 0, _SteeringPass);
                }
                else
                {
                    double deltaHeave = 0;
                    if (i != 0 && _HeaveMotionPass)
                    {
                        ///<remarks>
                        ///Calculating the Deltas of the 6DOFs. During the Heave Pass, the Model assumes that it has one of the 7DOFs, that is, the heave is known from the User Input. Hence taking the Heave as Input, the remaining 6DOFs are calculated. 
                        ///Next, the delta of Heave is added to the Static Chassis Heave. 
                        /// </remarks>
                        deltaHeave = (_vehicleOP.vehicle_Motion.Final_WheelDeflectionsY[i] - _vehicleOP.vehicle_Motion.Final_WheelDeflectionsY[i - 1]);
                        X = ComputeVehicleModel_7DOF(_vehicleOP, 0, 0,0,0, deltaHeave, i, _SteeringPass);
                        Beta = ComputeVehicleModel_SpringDeflections(X, 0, 0, deltaHeave, _SteeringPass);
                        ///<remarks>
                        ///Contrary to the other outputs, the delta of Heave during the Heave Pass is calculated inside this function itself rather that inside the <c>ComputeVehicleModel_NetDelta</c> method. The only reason for this is to reduce the number of parameters passed to 
                        ///that function <seealso cref="ComputeVehicleModel_NetDelta(Vehicle, Vector{double}, Vector{double}, double[], bool, int)"/>
                        /// </remarks>
                        _vehicleOP.oc_FL[i].deltaNet_ChassisHeave += deltaHeave;
                    }
                    else
                    {
                        ///<remarks>
                        ///This it he 1st Motion Percentage
                        ///Although this is the Motion Pass, to initialize all the 7DOFs for static conditions, I'm making use of the Steering Pass' Stiffness and RHS Matrix. 
                        /// </remarks>
                        X = ComputeVehicleModel_7DOF(_vehicleOP, 0, 0, 0, 0, 0, i, true);
                        Beta = ComputeVehicleModel_SpringDeflections(X, 0, 0, 0, true);
                    }

                }



                ///<remarks>
                ///Solving for the Vehicle Weights
                ///The IF loop is employed because during the initial calculation stage (i==0), the absolute values of Forces are calculated and for all the other percentages of motion ,the delta is calculated and added to the previous Motion's values
                ///
                ///</remarks>
                int _temp; if (_SteeringPass || i == 0) { _temp = 1; } else _temp = 0;
                if (i == 0)
                {
                    ComputeVehicleModel_Fz(VehicleWeight, -X[_temp], out temp_CW[0], out temp_CW[1], out temp_CW[2], out temp_CW[3]);
                }
                else
                {
                    ///<remarks>
                    ///Need this is IF loop here so that if it is steering pass I can assign a negative sign to the ROll Angle. Still not sure why I need to do this 
                    /// </remarks>
                    if (_SteeringPass)
                    {
                        ComputeVehicleModel_Fz(0, -X[_temp], out temp_CW[0], out temp_CW[1], out temp_CW[2], out temp_CW[3]);
                    }
                    else ComputeVehicleModel_Fz(0, 0, out temp_CW[0], out temp_CW[1], out temp_CW[2], out temp_CW[3]);
                }

                ///<remarks>
                ///Solving for the Vehicle CoG's Lateral and Longitudinal Coordinates
                ///</remarks>
                ComputeVehicleModel_CoGTopView(_vehicleOP.oc_FL[i].CW, _vehicleOP.oc_FR[i].CW, _vehicleOP.oc_RL[i].CW, _vehicleOP.oc_RR[i].CW, (New_Trackwidth_Front + New_Trackwidth_Rear) / 2, New_AvgWheelBase, out _vehicleOP.New_SMCoGx[i], out _vehicleOP.New_SMCoGz[i]);

                ///<remarks>
                ///Calculating thw New Values of the parameters 
                /// </remarks>
                if (i != 0)
                {
                    ComputeVehicleModel_NetDelta(_vehicleOP, X, Beta, temp_CW, _SteeringPass, i);
                }
                else Assign_StaticResults(_vehicleOP, X, Beta, temp_CW);

                //if (_simType == SimulationType.SetupChange)
                //{
                //    ComputeVehicleModel_NetDelta(_vehicleOP, X, Beta, temp_CW, _SteeringPass, i);
                //}

            }
        }

        #region Method to Assign the Static Results. This method is called only once in the begining to intialize the first percentage of Motion Values with the Static Results
        /// <summary>
        /// This method assigns the static or initial results to the corresponding result channels
        /// </summary>
        /// <param name="_vStatic"></param>
        /// <param name="_xStatic"></param>
        /// <param name="_betaStatic"></param>
        /// <param name="_cwStatic"></param>
        private void Assign_StaticResults(Vehicle _vStatic, Vector<double> _xStatic, Vector<double> _betaStatic, double[] _cwStatic)
        {
            ///<remarks>
            ///Static results being assigned to the Vehicle's OutptuClass variables
            ///</remarks>
            _vStatic.oc_FL[0].ChassisHeave = _xStatic[0];
            _vStatic.RollAngle[0] = _xStatic[1] * (180 / Math.PI);
            _vStatic.PitchAngle[0] = _xStatic[2] * (180 / Math.PI);
            _vStatic.oc_FL[0].TireDeflection = _xStatic[3];
            _vStatic.oc_FR[0].TireDeflection = _xStatic[4];
            _vStatic.oc_RL[0].TireDeflection = _xStatic[5];
            _vStatic.oc_RR[0].TireDeflection = _xStatic[6];

            _vStatic.oc_FL[0].Corrected_SpringDeflection = _betaStatic[0];
            _vStatic.oc_FR[0].Corrected_SpringDeflection = _betaStatic[1];
            _vStatic.oc_RL[0].Corrected_SpringDeflection = _betaStatic[2];
            _vStatic.oc_RR[0].Corrected_SpringDeflection = _betaStatic[3];

            _vStatic.oc_FL[0].Corrected_WheelDeflection = _betaStatic[0] / MR_FL;
            _vStatic.oc_FR[0].Corrected_WheelDeflection = _betaStatic[1] / MR_FR;
            _vStatic.oc_RL[0].Corrected_WheelDeflection = _betaStatic[2] / MR_RL;
            _vStatic.oc_RR[0].Corrected_WheelDeflection = _betaStatic[3] / MR_RR;

            _vStatic.oc_FL[0].CW = _cwStatic[0];
            _vStatic.oc_FR[0].CW = _cwStatic[1];
            _vStatic.oc_RL[0].CW = _cwStatic[2];
            _vStatic.oc_RR[0].CW = _cwStatic[3];
        } 
        #endregion

        #region Method to compute the 2 DELTAS or the Summation of DELTAS
        /// <summary>
        /// This method computes the Net or Total Delta of a particular result channel. This is needed because there are 2 passes (heave and steering pass) both of which generate a delta value each. Using a summation of both deltas we can calculate the overall or net change in the 
        /// result channel
        /// </summary>
        /// <param name="_vNetD">Object of the Vehicle Class</param>
        /// <param name="_xNetD">Vector which holds the summation of changes of the 7 or 6DOF model </param>
        /// <param name="_betaNetD">Vector which holds the summation of changes of the Spring Deflections</param>
        /// <param name="_cwNetD">Array which holds the summation of the changes of the Corner Weight of each coner. Pass 4x1 Array</param>
        /// <param name="_steeringPass">Variable to determine if the Model is being solved for the Steering Pass</param>
        /// <param name="_i">Percentage of Motion or Index</param>
        private void ComputeVehicleModel_NetDelta(Vehicle _vNetD, Vector<double> _xNetD, Vector<double> _betaNetD, double[] _cwNetD, bool _steeringPass, int _i)
        {
            int index;
            ///<remarks> This IF loop is necessary because the vector X is a 7x1 matrix for the Steering Pass and a 6x1 matrix for the Heave Pass</remarks>
            if (_steeringPass)
            {
                ///<remarks>
                ///For a Heave Pass, the Chassis Heave is calculated 
                /// </remarks>
                _vNetD.oc_FL[_i].deltaNet_ChassisHeave += _xNetD[0];
                index = 1;
            }

            else
            {
                index = 0;
            }

            ///<remarks> Summation of Changes of Roll and Pitch Angle</remarks>
            _vNetD.deltaNet_RollAngle[_i] += _xNetD[index]; index++;
            _vNetD.deltaNet_Pitch[_i] += _xNetD[index]; index++;

            ///<remarks>Summation of changes of tire deflections</remarks>
            _vNetD.oc_FL[_i].deltaNet_TireDeflection += _xNetD[index]; index++;
            _vNetD.oc_FR[_i].deltaNet_TireDeflection += _xNetD[index]; index++;
            _vNetD.oc_RL[_i].deltaNet_TireDeflection += _xNetD[index]; index++;
            _vNetD.oc_RR[_i].deltaNet_TireDeflection += _xNetD[index]; index++;

            ///<remarks>Summation of changes of spring deflections</remarks>
            _vNetD.oc_FL[_i].deltaNet_SpringDeflection += _betaNetD[0];
            _vNetD.oc_FR[_i].deltaNet_SpringDeflection += _betaNetD[1];
            _vNetD.oc_RL[_i].deltaNet_SpringDeflection += _betaNetD[2];
            _vNetD.oc_RR[_i].deltaNet_SpringDeflection += _betaNetD[3];

            if (_steeringPass)
            {
                _vNetD.oc_FL[_i].DeltaSpringDef_Steering = _betaNetD[0];
                _vNetD.oc_FR[_i].DeltaSpringDef_Steering = _betaNetD[1];
                _vNetD.oc_RL[_i].DeltaSpringDef_Steering = _betaNetD[2];
                _vNetD.oc_RR[_i].DeltaSpringDef_Steering = _betaNetD[3];
            }

            ///<remarks>Summation of changes of the wheel deflections</remarks>
            _vNetD.oc_FL[_i].deltaNet_WheelDeflection += _betaNetD[0] / MR_FL;
            _vNetD.oc_FR[_i].deltaNet_WheelDeflection += _betaNetD[1] / MR_FR;
            _vNetD.oc_RL[_i].deltaNet_WheelDeflection += _betaNetD[2] / MR_RL;
            _vNetD.oc_RR[_i].deltaNet_WheelDeflection += _betaNetD[3] / MR_RR;

            ///<remarks> Summation of changes of corner weights</remarks>
            _vNetD.oc_FL[_i].deltaNet_CornerWeight += _cwNetD[0];
            _vNetD.oc_FR[_i].deltaNet_CornerWeight += _cwNetD[1];
            _vNetD.oc_RL[_i].deltaNet_CornerWeight += _cwNetD[2];
            _vNetD.oc_RR[_i].deltaNet_CornerWeight += _cwNetD[3];
        }
        #endregion

        #region Method to compute the Summation of Results. This method is called only the last
        /// <summary>
        /// This method computes the Vehicle Results at each interval of the Motion, that is, at each percentage of the motion by adding the delta of the results to the previous results. This method shall be accessed from within the <c> Vehicle </c> Class after the summation of the 
        /// deltas are calculated. 
        /// </summary>
        /// <param name="_vehicleSum"> Object of Vehicle Class </param>
        /// <param name="_xSum"> Represents the 7x1 Matrix which contains the solution of the 7 Degrees of Freedom of the Vehicle: Roll Pitch Heave and 4 tire Deflections </param>
        /// <param name="_betaSum"> Represents the Spring Deflections of each corner </param>
        /// <param name="_cwSUM"> Represents the delta of the Corner Weight of each corner </param>
        /// <param name="_i"> Represents the Index at which the operations are to be performed </param>
        public void ComputeVehicleModel_SummationOfResults(Vehicle _vehicleSum/*Vector<double> _xSum, Vector<double> _betaSum, double[] _cwSUM,*/ /*int _i*/)
        {
            ///<remarks>
            ///The first IF Loop is employed becsuse if the model is being solved for Heave Pass, then only 6DOFs are considered and hence even in the 
            ///The IF loop is employed to detect if the <c>ComputeVehicleModel_SummationOfResults</c> is being called for the first time. If it is then it implies that the static values are being calculated and not the delta values. 
            /// </remarks>



            ///<remarks>
            ///An integer variable called <c>pseudo</c> is used. This is because, if it is the Motion Pass that is being solved then we simply need to calculate the new values by adding the previous values to it. 
            ///But if it is the Steering Pass being evaluated then we must remember that there is already a value stored in the current index position. 
            /// </remarks>

            ///<remarks>New Results being calculated by adding the delta of the values to the previous values</remarks>
            for (int _i = 1; _i < _vehicleSum.oc_FL.Count; _i++)
            {

                ///<remarks>Calculating the new Roll and Pitch Angles</remarks>
                _vehicleSum.oc_FL[_i].ChassisHeave = _vehicleSum.oc_FL[_i - 1].ChassisHeave + _vehicleSum.oc_FL[_i].deltaNet_ChassisHeave;
                _vehicleSum.RollAngle[_i] = _vehicleSum.RollAngle[_i - 1] + _vehicleSum.deltaNet_RollAngle[_i] * (180 / Math.PI);
                _vehicleSum.PitchAngle[_i] = _vehicleSum.PitchAngle[_i - 1] + _vehicleSum.deltaNet_Pitch[_i] * (180 / Math.PI);
                ///<remarks>Calculating the New Tire Deflections</remarks>
                _vehicleSum.oc_FL[_i].TireDeflection = _vehicleSum.oc_FL[_i - 1].TireDeflection + _vehicleSum.oc_FL[_i].deltaNet_TireDeflection;
                _vehicleSum.oc_FR[_i].TireDeflection = _vehicleSum.oc_FR[_i - 1].TireDeflection + _vehicleSum.oc_FR[_i].deltaNet_TireDeflection;
                _vehicleSum.oc_RL[_i].TireDeflection = _vehicleSum.oc_RL[_i - 1].TireDeflection + _vehicleSum.oc_RL[_i].deltaNet_TireDeflection;
                _vehicleSum.oc_RR[_i].TireDeflection = _vehicleSum.oc_RR[_i - 1].TireDeflection + _vehicleSum.oc_RR[_i].deltaNet_TireDeflection;
                ///<remarks>Calculating the new Spring Deflections</remarks>
                _vehicleSum.oc_FL[_i].Corrected_SpringDeflection = _vehicleSum.oc_FL[_i - 1].Corrected_SpringDeflection + _vehicleSum.oc_FL[_i].deltaNet_SpringDeflection;
                _vehicleSum.oc_FR[_i].Corrected_SpringDeflection = _vehicleSum.oc_FR[_i - 1].Corrected_SpringDeflection + _vehicleSum.oc_FR[_i].deltaNet_SpringDeflection;
                _vehicleSum.oc_RL[_i].Corrected_SpringDeflection = _vehicleSum.oc_RL[_i - 1].Corrected_SpringDeflection + _vehicleSum.oc_RL[_i].deltaNet_SpringDeflection;
                _vehicleSum.oc_RR[_i].Corrected_SpringDeflection = _vehicleSum.oc_RR[_i - 1].Corrected_SpringDeflection + _vehicleSum.oc_RR[_i].deltaNet_SpringDeflection;
                ///<remarks>Calculating the new Wheel Deflections</remarks>
                _vehicleSum.oc_FL[_i].Corrected_WheelDeflection = _vehicleSum.oc_FL[_i - 1].Corrected_WheelDeflection + _vehicleSum.oc_FL[_i].deltaNet_WheelDeflection;
                _vehicleSum.oc_FR[_i].Corrected_WheelDeflection = _vehicleSum.oc_FR[_i - 1].Corrected_WheelDeflection + _vehicleSum.oc_FR[_i].deltaNet_WheelDeflection;
                _vehicleSum.oc_RL[_i].Corrected_WheelDeflection = _vehicleSum.oc_RL[_i - 1].Corrected_WheelDeflection + _vehicleSum.oc_RL[_i].deltaNet_WheelDeflection;
                _vehicleSum.oc_RR[_i].Corrected_WheelDeflection = _vehicleSum.oc_RR[_i - 1].Corrected_WheelDeflection + _vehicleSum.oc_RR[_i].deltaNet_WheelDeflection;

                if (_vehicleSum.SuspensionIsSolved && (_vehicleSum.vehicle_Motion.SteeringExists))
                {
                    ///<remarks>
                    ///Calculating the Spring Defelctions on the Front and the Rear  
                    ///The Spring deflection on the rear is a consequence of Diagonal Weight Transfer and hence an indrect consequence of the steering. Unlike the Front Spring Deflections which are a direct consequence of Steering (because of which they are calculated while solving the 
                    ///Kinematics itself by computing the delta of the Wheel Deflection)
                    /// </remarks>
                    _vehicleSum.sc_RL.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_RL[_i].DeltaSpringDef_Steering);
                    _vehicleSum.sc_RR.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_RR[_i].DeltaSpringDef_Steering);

                    _vehicleSum.sc_FL.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_FL[_i].DeltaSpringDef_Steering);
                    _vehicleSum.sc_FR.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_FR[_i].DeltaSpringDef_Steering);
                }
                ///<remarks>Calculating the new Corner Weights</remarks>
                _vehicleSum.oc_FL[_i].CW = _vehicleSum.oc_FL[_i - 1].CW + _vehicleSum.oc_FL[_i].deltaNet_CornerWeight;
                _vehicleSum.oc_FR[_i].CW = _vehicleSum.oc_FR[_i - 1].CW + _vehicleSum.oc_FR[_i].deltaNet_CornerWeight;
                _vehicleSum.oc_RL[_i].CW = _vehicleSum.oc_RL[_i - 1].CW + _vehicleSum.oc_RL[_i].deltaNet_CornerWeight;
                _vehicleSum.oc_RR[_i].CW = _vehicleSum.oc_RR[_i - 1].CW + _vehicleSum.oc_RR[_i].deltaNet_CornerWeight;
            }


        }

        /// <summary>
        /// <para>Method to assign the Newly Calculated Variables. This is not similar to <see cref="ComputeVehicleModel_SummationOfResults(Vehicle)"/> method above.</para> 
        /// <para>In the above method you add the Summation of Deltas (because of Steering and Heave Pass) and you add it to the previous value of the OutputClass to get the new value</para>
        /// <para>In this method you Simply assign the newly calculate variable to the current one inside the <see cref="OutputClass"/></para>
        /// <para>Ths newly Calculated vaiables are NOT DELTAS</para>
        /// </summary>
        /// <param name="_vehicleSum"></param>
        /// <param name="_simType"></param>
        public void ComputeVehicleModel_SummationOfResults_For_SetupChange(Vehicle _vehicleSum, SimulationType _simType)
        {
            ///<remarks>New Results being calculated by adding the delta of the values to the previous values</remarks>
            for (int _i = 0; _i < _vehicleSum.oc_FL.Count; _i++)
            {

                ///<remarks>Calculating the new Roll and Pitch Angles</remarks>
                _vehicleSum.oc_FL[_i].ChassisHeave = _vehicleSum.oc_FL[_i].deltaNet_ChassisHeave/*- _vehicleSum.oc_FL[_i].ChassisHeave*/;
                _vehicleSum.RollAngle[_i] = _vehicleSum.deltaNet_RollAngle[_i] * (180 / Math.PI)/* - _vehicleSum.RollAngle[_i]*/;
                _vehicleSum.PitchAngle[_i] = _vehicleSum.deltaNet_Pitch[_i] * (180 / Math.PI)/* - _vehicleSum.PitchAngle[_i]*/;
                ///<remarks>Calculating the New Tire Deflections</remarks>
                _vehicleSum.oc_FL[_i].TireDeflection = _vehicleSum.oc_FL[_i].deltaNet_TireDeflection /*- _vehicleSum.oc_FL[_i].TireDeflection*/;
                _vehicleSum.oc_FR[_i].TireDeflection = _vehicleSum.oc_FR[_i].deltaNet_TireDeflection /*- _vehicleSum.oc_FR[_i].TireDeflection*/;
                _vehicleSum.oc_RL[_i].TireDeflection = _vehicleSum.oc_RL[_i].deltaNet_TireDeflection/*- _vehicleSum.oc_RL[_i].TireDeflection*/;
                _vehicleSum.oc_RR[_i].TireDeflection = +_vehicleSum.oc_RR[_i].deltaNet_TireDeflection /*- _vehicleSum.oc_RR[_i].TireDeflection*/;

                ///<summary>
                ///Before I assign the Newly calculate Spring Deflection to the <see cref="OutputClass.Corrected_SpringDeflection"/> I need to calculate the delta between the 2 to pass to the <see cref="SuspensionCoordinatesMaster.SpringDeflection_DeltSteering"/>
                ///This is because only then can the Kinematic Solver find the Change in Suspension Coordinates. If this is not then the Kinematics Solver will calculate the Suspension Coordinates for another 16 or 15 mm of 
                /// </summary>
                //if (_vehicleSum.SuspensionIsSolved && (_vehicleSum.vehicle_Motion.SteeringExists || _simType == SimulationType.SetupChange))
                //{
                ///<remarks>
                ///Calculating the Spring Defelctions on the Front and the Rear  
                ///The Spring deflection on the rear is a consequence of Diagonal Weight Transfer and hence an indrect consequence of the steering. Unlike the Front Spring Deflections which are a direct consequence of Steering (because of which they are calculated while solving the 
                ///Kinematics itself by computing the delta of the Wheel Deflection)
                /// </remarks>
                _vehicleSum.sc_RL.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_RL[_i].deltaNet_SpringDeflection - _vehicleSum.oc_RL[_i].Corrected_SpringDeflection);
                _vehicleSum.sc_RR.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_RR[_i].deltaNet_SpringDeflection - _vehicleSum.oc_RR[_i].Corrected_SpringDeflection);

                _vehicleSum.sc_FL.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_FL[_i].deltaNet_SpringDeflection - _vehicleSum.oc_FL[_i].Corrected_SpringDeflection);
                _vehicleSum.sc_FR.SpringDeflection_DeltSteering.Insert(_i, _vehicleSum.oc_FR[_i].deltaNet_SpringDeflection - _vehicleSum.oc_FR[_i].Corrected_SpringDeflection);
                //}

                ///<remarks>Calculating the new Spring Deflections</remarks>
                _vehicleSum.oc_FL[_i].Corrected_SpringDeflection = +_vehicleSum.oc_FL[_i].deltaNet_SpringDeflection/*- _vehicleSum.oc_FL[_i].Corrected_SpringDeflection*/;
                _vehicleSum.oc_FR[_i].Corrected_SpringDeflection = +_vehicleSum.oc_FR[_i].deltaNet_SpringDeflection/*- _vehicleSum.oc_FR[_i].Corrected_SpringDeflection*/;
                _vehicleSum.oc_RL[_i].Corrected_SpringDeflection = +_vehicleSum.oc_RL[_i].deltaNet_SpringDeflection/*- _vehicleSum.oc_RL[_i].Corrected_SpringDeflection*/;
                _vehicleSum.oc_RR[_i].Corrected_SpringDeflection = +_vehicleSum.oc_RR[_i].deltaNet_SpringDeflection/*- _vehicleSum.oc_RR[_i].Corrected_SpringDeflection*/;
                ///<remarks>Calculating the new Wheel Deflections</remarks>
                _vehicleSum.oc_FL[_i].Corrected_WheelDeflection = +_vehicleSum.oc_FL[_i].deltaNet_WheelDeflection/*- _vehicleSum.oc_FL[_i].Corrected_WheelDeflection*/;
                _vehicleSum.oc_FR[_i].Corrected_WheelDeflection = +_vehicleSum.oc_FR[_i].deltaNet_WheelDeflection/*- _vehicleSum.oc_FR[_i].Corrected_WheelDeflection*/;
                _vehicleSum.oc_RL[_i].Corrected_WheelDeflection = +_vehicleSum.oc_RL[_i].deltaNet_WheelDeflection/*- _vehicleSum.oc_RL[_i].Corrected_WheelDeflection*/;
                _vehicleSum.oc_RR[_i].Corrected_WheelDeflection = +_vehicleSum.oc_RR[_i].deltaNet_WheelDeflection/*- _vehicleSum.oc_RR[_i].Corrected_WheelDeflection*/;

                ///<remarks>Calculating the new Corner Weights</remarks>
                _vehicleSum.oc_FL[_i].CW = /*_vehicleSum.oc_FL[_i].CW +*/ _vehicleSum.oc_FL[_i].deltaNet_CornerWeight;
                _vehicleSum.oc_FR[_i].CW = /*_vehicleSum.oc_FR[_i].CW +*/ _vehicleSum.oc_FR[_i].deltaNet_CornerWeight;
                _vehicleSum.oc_RL[_i].CW = /*_vehicleSum.oc_RL[_i].CW +*/ _vehicleSum.oc_RL[_i].deltaNet_CornerWeight;
                _vehicleSum.oc_RR[_i].CW = /*_vehicleSum.oc_RR[_i].CW +*/ _vehicleSum.oc_RR[_i].deltaNet_CornerWeight;
            }
        }

        #endregion

        #region Bolted System Calculations for the Steering Rack and ARB Bearing Attachment Points
        ///<summary>
        ///Represents Left Hand Coordinates of the Bolts.
        ///P is the bolt on the upper corner 
        ///Q is the bolt in the Lower Corner
        ///N is the point of the application of force
        /// </summary>
        Point3D P_Left, Q_Left, N_Left;

        ///<summary>
        ///Represents Right Hand Coordinates of the Bolts.
        ///P is the bolt on the upper corner 
        ///Q is the bolt in the Lower Corner
        ///N is the point of the application of force
        /// </summary>
        Point3D P_Right, Q_Right, N_Right;


        ///<summary>
        ///Represents the CoG of the Bolted System
        /// </summary>
        Point3D CoG;

        ///<summary>
        ///Represents the Radial Distance between the CoG and each of the Bolts in the  TOP VIEW
        /// </summary>
        Vector2D radial_TOP_P_Left, radial_TOP_Q_Left, radial_TOP_P_Right, radial_TOP_Q_Right;

        ///<summary>
        ///Represents the Radial Distance between CoG and each of the bolts in the FRONT VIEW
        /// </summary>
        Vector2D radial_FRONT_PQ_Left, radial_FRONT_PQ_Right;

        /// <summary>
        /// Eccentricity of the Applied External Load from the Steering Column Attachment Bolts
        /// </summary>
        Vector2D eccentricity_SIDE_SteeringColumn;

        ///<summary>
        ///Angle that the radial vectors make with the COG in the TOP View
        /// </summary>
        double theta_TOP_P_Left, theta_TOP_P_Right, theta_TOP_Q_Left, theta_TOP_Q_Right;
        ///<summary>
        ///Angle that the radial vectors make with the COG in the FRONT View
        ///Usually this is zero as in the FRONT view all the bolts lie in the same plane
        /// </summary>
        double theta_FRONT_P_Left, theta_FRONT_P_Right;

        ///<summary>
        ///Net Force on the Left hand side bolts of the Steering Rack and the ARB Bearing Attachments
        /// </summary>
        Vector3D Force_P_Left, Force_Q_Left;

        ///<summary>
        ///Net Force on the Right hand side bolts of the Steering Rack and the ARB Bearing Attachments
        /// </summary>
        Vector3D Force_P_Right, Force_Q_Right;

        /// <summary>
        /// Net Force on the Left and Right Steering Column Attachments
        /// </summary>
        Vector3D Force_P_SteeringColumn, Force_Q_SteeringColumn;

        ///<summary>
        ///Represents the Shear Force components of the Forces caused when the Bolt System is analysed in the TOP VIEW for eccentric loading
        /// </summary>
        double Dir_Shear_P_Left, Dir_Shear_Q_Left, Dir_Shear_P_Right, Dir_Shear_Q_Right;
        double Sec_Shear_P_Left, Sec_Shear_Q_Left, Sec_Shear_P_Right, Sec_Shear_Q_Right;
        /// <summary>
        /// Represents the Tensile or Pull Force components of the forces caused when the Bolt System is analysed in Front View for eccentric loading
        /// </summary>
        double Dir_Tensile_P_Left, Dir_Tensile_P_Right;
        double Sec_Tensile_P_Left, Sec_Tensile_P_Right;

        /// <summary>
        /// Represents the Tensile Force Components of the Steering Column Attachments
        /// </summary>
        double Dir_Tensile_P_SteeringColumn;
        double Sec_Tensile_P_SteeringColumn;

        /// <summary>
        /// Represents the Shear Force Components of the Steering Column Attachments 
        /// </summary>
        double Dir_Shear_P_SteeringColumn;


        /// <summary>
        /// Initializes all the Coordinates, Applied External Forces, Vectors which are needed
        /// </summary>
        /// <param name="_leftAttachments">Left bolt attachment points; TOp and Bottom</param>
        /// <param name="_rightAttachments">Right bolt attachment points; Top and Bottom</param>
        /// <param name="_ocInitLeft">Object of the outptu class of the Left</param>
        /// <param name="_ocInitRight">Object of the outptu class of the Right</param>
        /// <param name="_steering">Variable to indicate wheather the STEERING Bolt Coordinates are to be used or if the ARB Bolt coordinates are used</param>
        private void InitializeBoltedJointVariables(double[,] _leftAttachments, double[,] _rightAttachments, OutputClass _ocInitLeft, OutputClass _ocInitRight, bool _steering)
        {
            ///<remarks>
            ///This is an indexer variable to obtain the coordinates of the Left and Right Attachment Points from the 3x4 array that is used in the Load Case. 
            /// </remarks>
            int position = 0;
            if (_steering)
            {
                position = 0;
            }
            else
            {
                position = 2;
            }

            ///<remarks>
            ///Initializing the Bolt Coordinates
            /// </remarks>

            Q_Left = new Point3D(_leftAttachments[0, position], _leftAttachments[1, position], _leftAttachments[2, position]);
            P_Left = new Point3D(_leftAttachments[0, position + 1], _leftAttachments[1, position + 1], _leftAttachments[2, position + 1]);
            N_Left = new Point3D(_ocInitLeft.scmOP.N1x, _ocInitLeft.scmOP.N1y, _ocInitLeft.scmOP.N1z);

            Q_Right = new Point3D(_rightAttachments[0, position], _rightAttachments[1, position], _rightAttachments[2, position]);
            P_Right = new Point3D(_rightAttachments[0, position + 1], _rightAttachments[1, position + 1], _rightAttachments[2, position + 1]);
            N_Right = new Point3D(_ocInitRight.scmOP.N1x, _ocInitRight.scmOP.N1y, _ocInitRight.scmOP.N1z);


            ///<remarks>
            ///Calculating the CoG of the Bolted System
            /// </remarks>
            double xCord = (P_Left.X + Q_Left.X + P_Right.X + Q_Right.X) / (4);
            double ycord = (P_Left.Y + Q_Left.Y + P_Right.Y + P_Right.Y) / (4);
            double zCord = (P_Left.Z + Q_Left.Z + P_Right.Z + Q_Right.Z) / (4);
            CoG = new Point3D(xCord, ycord, zCord);

            ///<remarks>
            ///Calculating the Radial Distances in Front and Top View
            /// </remarks>

            radial_TOP_P_Left = new Vector2D(P_Left.X - CoG.X, P_Left.Z - CoG.Z);
            radial_TOP_Q_Left = new Vector2D(Q_Left.X - CoG.X, Q_Left.Z - CoG.Z);
            radial_TOP_P_Right = new Vector2D(P_Right.X - CoG.X, P_Right.Z - CoG.Z);
            radial_TOP_Q_Right = new Vector2D(Q_Right.X - CoG.X, Q_Right.Z - CoG.Z);

            radial_FRONT_PQ_Left = new Vector2D(P_Left.X - CoG.X, P_Left.Y - CoG.Y);
            radial_FRONT_PQ_Right = new Vector2D(P_Right.X - CoG.X, P_Right.Y - CoG.Y);

            ///<remarks>
            ///Calclating the angles that the radial distances make with the CoG in the TOP VIEW
            ///Finding the absolute values so they are less confusing to deal with when Calculating the Components of the Shear Forces
            /// </remarks>
            theta_TOP_P_Left = Math.Abs(Math.Atan((P_Left.Z - CoG.Z) / (P_Left.X - CoG.X)));
            theta_TOP_P_Right = Math.Abs(Math.Atan((P_Right.Z - CoG.Z) / (P_Right.X - CoG.X)));
            theta_TOP_Q_Left = Math.Abs(Math.Atan((Q_Left.Z - CoG.Z) / (Q_Left.X - CoG.X)));
            theta_TOP_Q_Right = Math.Abs(Math.Atan((Q_Right.Z - CoG.Z) / (Q_Right.X - CoG.X)));

            ///<remarks>
            ///Calclating the angles that the radial distances make with the CoG in the FRONT VIEW
            /// </remarks>
            theta_FRONT_P_Left = Math.Atan((P_Left.Y - CoG.Y) / (P_Left.X - CoG.X));
            theta_FRONT_P_Right = Math.Atan((P_Right.Y - CoG.Y) / (P_Right.X - CoG.X));
        }

        /// <summary>
        /// Computes the Net Force Components on the bolt and creates a Vector out of it
        /// </summary>
        /// <param name="_moment">Moment acting abou the CoG</param>
        private void Compute_TotalForce_ARBandSteeringRack(double _moment)
        {
            if (_moment > 0)
            /// <remarks> Implies that ClockWise moment </remarks>
            ///<remarks>
            ///IF LOOP is needed to determine whether the <c>Force_P_Left</c> which represents the Force on the First Bolt has a positive or negative value of the Vertical and Horizontal Values.  
            /// </remarks>
            {
                ///<remarks> CORNER 1 </remarks>
                Force_P_Left = new Vector3D(Sec_Shear_P_Left * Math.Sin(theta_TOP_P_Left), (Dir_Tensile_P_Left - Sec_Tensile_P_Left) / 2, Dir_Shear_P_Left - Sec_Shear_P_Left * Math.Cos(theta_TOP_P_Left));
                ///<remarks>
                ///CORNER 2
                ///<c>theta_TOP_P_Right</c> is POSITIVE
                ///Hence POSITIVE Sign is needed to make Horizontal Component orient in the SAME direction as <c>Force_P_Left</c> Horizontal Component
                ///Hence POSITIVE sign is needed to make Vertical Component orient in the OPPOSITVE direction as the <c>Force_P_Left</c> Vertical Component
                /// </remarks>
                Force_P_Right = new Vector3D(Sec_Shear_P_Right * Math.Sin(theta_TOP_P_Right), (Dir_Tensile_P_Right + Sec_Tensile_P_Right) / 2, Dir_Shear_P_Right + Sec_Shear_P_Right * Math.Cos(theta_TOP_P_Right));
                ///<remarks>
                ///CORNER 3
                ///<c>theta_TOP_Q_Right</c> is POSITIVE 
                ///Hence NEGATIVE Sign is needed to make the Horizontal component orient in the OPPOSITE direction as <c>Force_P_Right</c> Horizontal Component
                ///Hence PPOSITIVE Sign is needed to make the Vertical component orient in the SAME Directioni as the <c>Force_P_Right</c> Vertical Component
                /// </remarks>
                Force_Q_Right = new Vector3D(-Sec_Shear_Q_Right * Math.Sin(theta_TOP_Q_Right), (Dir_Tensile_P_Right + Sec_Tensile_P_Right) / 2, Dir_Shear_Q_Right + Sec_Shear_Q_Right * Math.Cos(theta_TOP_Q_Right));
                ///<remarks>
                ///CORNER 4
                ///<c>theta_TOP_Q_LEFT</c> is POSITIVE
                /// Hence NEGATIVE Sign is needed to make the Horizontal component orient in the SAME direction as <c>Force_Q_Right</c> Horizontal Component
                /// Hence NEGATIVE Sign is needed to make the Vertical component orinet in the OPPOSITE Direction as the <c>Force_Q_RIght</c>  Vertical Component
                /// </remarks>
                Force_Q_Left = new Vector3D(-Sec_Shear_Q_Right * Math.Sin(theta_TOP_Q_Left), (Dir_Tensile_P_Left - Sec_Tensile_P_Left) / 2, Dir_Shear_Q_Left - Sec_Shear_Q_Left * Math.Cos(theta_TOP_Q_Left));
            }
            else
            {
                ///<remarks> CORNER 1 </remarks>
                Force_P_Left = new Vector3D(-Sec_Shear_P_Left * Math.Sin(theta_TOP_P_Left), (Dir_Tensile_P_Left + Sec_Tensile_P_Left) / 2, Dir_Shear_P_Left + Sec_Shear_P_Left * Math.Cos(theta_TOP_P_Left));
                ///<remarks>
                ///CORNER 2
                ///<c>theta_TOP_P_Right</c> is POSITIVE
                ///Hence NEGATIVE Sign is needed to make Horizontal Component orient in the SAME direction as <c>Force_P_Left</c> Horizontal Component
                ///Hence NEGATIVE sign is needed to make Vertical Component orient in the OPPOSITVE direction as the <c>Force_P_Left</c> Vertical Component
                /// </remarks>
                Force_P_Right = new Vector3D(-Sec_Shear_P_Right * Math.Sin(theta_TOP_P_Right), (Dir_Tensile_P_Right - Sec_Tensile_P_Right) / 2, Dir_Shear_P_Right - Sec_Shear_P_Right * Math.Cos(theta_TOP_P_Right));
                ///<remarks>
                ///CORNER 3
                ///<c>theta_TOP_Q_Right</c> is POSITIVE 
                ///Hence POSITIVE Sign is needed to make the Horizontal component orient in the OPPOSITE direction as <c>Force_P_Right</c> Horizontal Component
                ///Hence NEGATIVE Sign is needed to make the Vertical component orient in the SAME Directioni as the <c>Force_P_Right</c> Vertical Component
                /// </remarks>
                Force_Q_Right = new Vector3D(Sec_Shear_Q_Right * Math.Sin(theta_TOP_Q_Right), (Dir_Tensile_P_Right - Sec_Tensile_P_Right) / 2, Dir_Shear_Q_Right - Sec_Shear_Q_Right * Math.Cos(theta_TOP_Q_Right));
                ///<remarks>
                ///CORNER 4
                ///<c>theta_TOP_Q_LEFT</c> is POSITIVE
                /// Hence POSITIVE Sign is needed to make the Horizontal component orient in the SAME direction as <c>Force_Q_Right</c> Horizontal Component
                /// Hence POSITIVE Sign is needed to make the Vertical component orinet in the OPPOSITE Direction as the <c>Force_Q_RIght</c>  Vertical Component
                /// </remarks>
                Force_Q_Left = new Vector3D(Sec_Shear_Q_Right * Math.Sin(theta_TOP_Q_Left), (Dir_Tensile_P_Left + Sec_Tensile_P_Left) / 2, Dir_Shear_Q_Left + Sec_Shear_Q_Left * Math.Cos(theta_TOP_Q_Left));

            }
        }

        /// <summary>
        /// Calculates the Primary and Secondary Shear Forces acting on the Bolts
        /// </summary>
        /// <param name="_moment">Moment about the CoG</param>
        /// <param name="_forceZLeft">Left Applied External Force</param>
        /// <param name="_forceZRight">RIght Applied External Force</param>
        private void Compute_TOP_ShearForces(double _moment, double _forceZLeft, double _forceZRight)
        {
            ///<remarks>
            ///Calculating the Primary Shear Forces at the Bolts
            /// </remarks>
            Dir_Shear_P_Left = Dir_Shear_P_Right = Dir_Shear_Q_Left = Dir_Shear_Q_Right = (_forceZLeft + _forceZRight) / 4;

            ///<remarks>
            ///Calculating the Shear Force Component which must be must be resolved based on the angle it makes with the vertical
            ///Absolute value is taken so that the Final Force value can be decided based purely on its orientation as decided during my theoretical calculations. 
            ///NOTE - Absolute Value is not taken for the Direct component of as there is no orientation concept to it like the shear forces which must be resolve. The Direct Forces at the COG of the System are always along the Origin Axes
            /// </remarks>
            Sec_Shear_P_Left  = Math.Abs((_moment * radial_TOP_P_Left.Length)  / (Math.Pow(radial_TOP_P_Left.Length, 2) + Math.Pow(radial_TOP_Q_Left.Length, 2) + Math.Pow(radial_TOP_P_Right.Length, 2) + Math.Pow(radial_TOP_Q_Right.Length, 2)));
            Sec_Shear_Q_Left  = Math.Abs((_moment * radial_TOP_Q_Left.Length)  / (Math.Pow(radial_TOP_P_Left.Length, 2) + Math.Pow(radial_TOP_Q_Left.Length, 2) + Math.Pow(radial_TOP_P_Right.Length, 2) + Math.Pow(radial_TOP_Q_Right.Length, 2)));
            Sec_Shear_P_Right = Math.Abs((_moment * radial_TOP_P_Right.Length) / (Math.Pow(radial_TOP_P_Left.Length, 2) + Math.Pow(radial_TOP_Q_Left.Length, 2) + Math.Pow(radial_TOP_P_Right.Length, 2) + Math.Pow(radial_TOP_Q_Right.Length, 2)));
            Sec_Shear_Q_Right = Math.Abs((_moment * radial_TOP_Q_Right.Length) / (Math.Pow(radial_TOP_P_Left.Length, 2) + Math.Pow(radial_TOP_Q_Left.Length, 2) + Math.Pow(radial_TOP_P_Right.Length, 2) + Math.Pow(radial_TOP_Q_Right.Length, 2)));
        }

        /// <summary>
        /// Computes the Direct andw Secondary Tensile Forces in the FRONT VIEW
        /// </summary>
        /// <param name="_moment"></param>
        /// <param name="_forceLeft"></param>
        /// <param name="_forceRight"></param>
        private void Compute_FRONT_TensileForces(double _moment, double _forceLeft, double _forceRight)
        {
            ///<remarks>
            ///Computing the Primary Tensile Forces acting on the Bolt
            ///</remarks>
            Dir_Tensile_P_Left = Dir_Tensile_P_Right = (_forceLeft + _forceRight) / 2;

            ///<remarks>
            ///Computing the Tensile Forces acting on the Bolt
            ///Absolute value is taken so that the Final Force value can be decided based purely on its orientation as decided during my theoretical calculations.
            ///NOTE - Absolute Value is not taken for the Direct component of as there is no orientation concept to it like the shear forces which must be resolve. The Direct Forces at the COG of the System are always along the Origin Axes
            /// </remarks>
            Sec_Tensile_P_Left  = Math.Abs((_moment * radial_FRONT_PQ_Left.Length)  / (Math.Pow(radial_FRONT_PQ_Left.Length, 2) + Math.Pow(radial_FRONT_PQ_Right.Length, 2)));
            Sec_Tensile_P_Right = Math.Abs((_moment * radial_FRONT_PQ_Right.Length) / (Math.Pow(radial_FRONT_PQ_Left.Length, 2) + Math.Pow(radial_FRONT_PQ_Right.Length, 2)));

        }

        /// <summary>
        /// Computes the X,Y,Z Forces in the 4 bolt system which is used to clamp the ARB bearings and the Steering Rack Bearings
        /// </summary>
        /// <param name="_ocLeft">OutputClass Object of the left</param>
        /// <param name="_ocRight">OutputClass Object of the right</param>
        /// <param name="i"></param>
        /// <param name="_LeftAttachments">Coordinates of the Left Attachment Points of the ARB abd Steering Bolts</param>
        /// <param name="_RightAttachments">Coordinates of the Right Attachment Points of the ARB abd Steering Bolts</param>
        /// <param name="Steering">Boolean parameter used to determine if the Steering Attachment or the ARB Attachment Points are to be extracted from the 3x4 arrays passed </param>
        /// <param name="_LeftForce_FRONT">The Applied Force on the left hand side in the FFRONT VIEW</param>
        /// <param name="_LeftForce_TOP">The Applied Force on the left hand side in the TOP VIEW</param>
        /// <param name="_RightForce_FRONT">The Applied Force on the right hand side in the FRONT VIEW</param>
        /// <param name="_RightForce_TOP"> The Applied Force in the right hand side in the TOP VIEW</param>
        public void BoltedJoint_ARB_And_Rack(List<OutputClass> _ocLeft, List<OutputClass> _ocRight, int i, double[,] _LeftAttachments, double[,] _RightAttachments, bool Steering, double _LeftForce_TOP, double _RightForce_TOP, double _LeftForce_FRONT, double _RightForce_FRONT)
        {
            InitializeBoltedJointVariables(_LeftAttachments, _RightAttachments, _ocLeft[i], _ocRight[i], Steering);

            ///<summary>
            ///Top View Calculations for the Shear orces
            /// </summary>
            /// 
            ///<remarks>
            ///Calculating the Moment about the CoG
            /// </remarks>
            double Moment_TOP_COG = (_RightForce_TOP * N_Right.X) + (_LeftForce_TOP * N_Left.X);

            ///<summary>
            ///Computing the Total Force in the Bolted System Calculted from the Top View and Front View
            /// </summary>
            Compute_TOP_ShearForces(Moment_TOP_COG, _LeftForce_TOP, _RightForce_TOP);

            ///<summary>
            ///>Front View Calculations for the Tensile/Pull Forces
            /// </summary
            /// 
            ///<remarks>
            ///Calculating the Moments about the CoG in the FRONT View
            /// </remarks>
            double Moment_FRONT_CoG = (_RightForce_FRONT * N_Right.X) + (_LeftForce_FRONT * N_Left.X);

            Compute_FRONT_TensileForces(Moment_FRONT_CoG, _LeftForce_FRONT, _RightForce_FRONT);

            ///<remarks>
            ///Computing the Total Forces due to Tensile in the Front View and Shear in the Top View
            /// </remarks>
            Compute_TotalForce_ARBandSteeringRack(Moment_TOP_COG);

        }

        /// <summary>
        /// Assigns the calculated forces of the Steering Rack Bearing Attachment Points to the corresponding OutputClass Variables
        /// </summary>
        /// <param name="_ocL">Output Class Object of the Left</param>
        /// <param name="_ocR">Output Class Object of the Right</param>
        /// <param name="_i"></param>
        public void AssignRackForces(List<OutputClass> _ocL, List<OutputClass> _ocR, int _i)
        {
            _ocL[_i].RackInboard1_x = Force_P_Left.X;
            _ocL[_i].RackInboard1_y = Force_P_Left.Y;
            _ocL[_i].RackInboard1_z = Force_P_Left.Z;
            _ocL[_i].RackInboard2_x = Force_Q_Left.X;
            _ocL[_i].RackInboard2_y = Force_Q_Left.Y;
            _ocL[_i].RackInboard2_z = Force_Q_Left.Z;

            _ocR[_i].RackInboard1_x = Force_P_Right.X;
            _ocR[_i].RackInboard1_y = Force_P_Right.Y;
            _ocR[_i].RackInboard1_z = Force_P_Right.Z;
            _ocR[_i].RackInboard2_x = Force_Q_Right.X;
            _ocR[_i].RackInboard2_y = Force_Q_Right.Y;
            _ocR[_i].RackInboard2_z = Force_Q_Right.Z;
        }

        /// <summary>
        /// Assigns the calculated forces of the ARB Bearing Attachment Points to the corresponding OutputClass variables 
        /// </summary>
        /// <param name="_ocL"></param>
        /// <param name="_ocR"></param>
        /// <param name="_i"></param>
        public void AssignARBForces(List<OutputClass> _ocL, List<OutputClass> _ocR, int _i)
        {
            _ocL[_i].ARBInboard1_x = Force_P_Left.X;
            _ocL[_i].ARBInboard1_y = Force_P_Left.Y;
            _ocL[_i].ARBInboard1_z = Force_P_Left.Z;
            _ocL[_i].ARBInboard2_x = Force_Q_Left.X;
            _ocL[_i].ARBInboard2_y = Force_Q_Left.Y;
            _ocL[_i].ARBInboard2_z = Force_Q_Left.Z;

            _ocR[_i].ARBInboard1_x = Force_P_Right.X;
            _ocR[_i].ARBInboard1_y = Force_P_Right.Y;
            _ocR[_i].ARBInboard1_z = Force_P_Right.Z;
            _ocR[_i].ARBInboard2_x = Force_Q_Right.X;
            _ocR[_i].ARBInboard2_y = Force_Q_Right.Y;
            _ocR[_i].ARBInboard2_z = Force_Q_Right.Z;
        }

        #endregion
        
        #region Bolted System Calculations for the Steering Column Attachment Points
        /// <summary>
        /// Initializes the Variables 
        /// </summary>
        /// <param name="_SteeringColumnAttachment"></param>
        /// <param name="_ocLeft"></param>
        /// <param name="_ocRight"></param>
        private void InitializeBoltedJointVariables(double[,] _SteeringColumnAttachment, OutputClass _ocLeft, OutputClass _ocRight)
        {
            P_Left  = new Point3D(_SteeringColumnAttachment[0, 0], _SteeringColumnAttachment[1, 0], _SteeringColumnAttachment[2, 0]);
            P_Right = new Point3D(_SteeringColumnAttachment[0, 1], _SteeringColumnAttachment[1, 1], _SteeringColumnAttachment[2, 1]);

            double xCord = (P_Left.X + P_Right.X) / (2);
            double yCord = (P_Left.Y + P_Right.Y) / (2);
            double zCord = (P_Left.Z + P_Right.Z) / (2);
            ///<remarks>
            ///This CoG will be needed only for the Front View Calculations and not for the Side View Calculations
            /// </remarks>
            CoG = new Point3D(xCord, yCord, zCord);

            ///<remarks>
            ///There is an Arbitrary reduction of 15 mm which is made in the Y Coordinatte. This 15 mm is assumed to the distance between the y coordinate of the Attachment Point and the Lower Portion of the Attachment itself. 
            ///It is about the Lower Portion of the Attachment that the Moments are calculated in teh Side View
            /// </remarks>
            eccentricity_SIDE_SteeringColumn = new Vector2D(P_Left.Z - ((_ocLeft.scmOP.N1z - _ocRight.scmOP.N1z) / 2), P_Left.Y - 15 - ((_ocLeft.scmOP.N1y - _ocRight.scmOP.N1y) / 2));
        }

        /// <summary>
        /// Computes Tension and Shear Forces calulted during a SIDE VIEW Bolted Joint analsis of the Steering Column Attachment
        /// </summary>
        /// <param name="_ocLeft"></param>
        /// <param name="_ocRight"></param>
        /// <param name="_moment"></param>
        private void Compute_SIDE_TensileAndShear(OutputClass _ocLeft, OutputClass _ocRight, double _moment)
        {
            Dir_Tensile_P_SteeringColumn = _ocLeft.ToeLink_z + _ocRight.ToeLink_z;
            Dir_Shear_P_SteeringColumn = _ocLeft.ToeLink_y + _ocRight.ToeLink_y;

            Sec_Tensile_P_SteeringColumn = (_moment * 15) / ((15 * 15) + (15 * 15));
        }

        /// <summary>
        /// Computes the Total Force on the Steering Column Bearing Attachment by summing the Direct and Shear Loads obtained using Bolted joint Analysis
        /// </summary>
        /// <param name="_moment"></param>
        private void Compute_TotalForces_SteeringColumn(double _moment)
        {
            if (_moment > 0)
            {
                Force_P_SteeringColumn = new Vector3D(0, Dir_Shear_P_SteeringColumn / 2, (Dir_Shear_P_SteeringColumn + Sec_Tensile_P_SteeringColumn) / 2);
                Force_Q_SteeringColumn = new Vector3D(0, Dir_Shear_P_SteeringColumn / 2, (Dir_Shear_P_SteeringColumn + Sec_Tensile_P_SteeringColumn) / 2);
            }
            else
            {
                Force_P_SteeringColumn = new Vector3D(0, Dir_Shear_P_SteeringColumn / 2, (Dir_Shear_P_SteeringColumn - Sec_Tensile_P_SteeringColumn) / 2);
                Force_Q_SteeringColumn = new Vector3D(0, Dir_Shear_P_SteeringColumn / 2, (Dir_Shear_P_SteeringColumn - Sec_Tensile_P_SteeringColumn) / 2);
            }
        }


        /// <summary>
        /// Computes the X,Y,Z Forces in the 2 bolt system which is used to clamp the Steering Column Bearings
        /// </summary>
        /// <param name="_ocLeft">OutputClass Object of the Left</param>
        /// <param name="_ocRight">OutputClass Object of the Right</param>
        /// <param name="i"></param>
        /// <param name="_leftAttachments">Coordinatwes of the Left Attachment point of the Steering Column Bolt</param>
        public void BoltedJoint_SteeringColumn(List<OutputClass> _ocLeft, List<OutputClass> _ocRight, int i, double[,] _SteeringColumnBearing)
        {
            InitializeBoltedJointVariables(_SteeringColumnBearing, _ocLeft[i], _ocRight[i]);

            double Moment_SteeringColumn;
            ///<remarks>
            ///THe moment is computed using : Horizontal Force Comp x Vertical Height Comp  +  Vertical Force Comp x Horizontal Height Comp
            ///Since I am seeing considering a Side View, for me the Lateral component is Z and not X. 
            /// </remarks>
            Moment_SteeringColumn = ((_ocLeft[i].ToeLink_z + _ocRight[i].ToeLink_z) * eccentricity_SIDE_SteeringColumn.Y) + ((_ocLeft[i].ToeLink_y + _ocLeft[i].ToeLink_y) * eccentricity_SIDE_SteeringColumn.X);

            Compute_SIDE_TensileAndShear(_ocLeft[i], _ocRight[i], Moment_SteeringColumn);

            Compute_TotalForces_SteeringColumn(Moment_SteeringColumn); 

        }

        /// <summary>
        /// Assigns the Steering Column Forces to the corresponding OutputClass variables
        /// </summary>
        /// <param name="_ocL">OutputClass Object of the Left</param>
        /// <param name="_i"></param>
        public void AssignSteeringColumnForces(List<OutputClass> _ocL, int _i)
        {
            _ocL[_i].SColumnInboard1_x = Force_P_SteeringColumn.X;
            _ocL[_i].SColumnInboard1_y = Force_P_SteeringColumn.Y;
            _ocL[_i].SColumnInboard1_z = Force_P_SteeringColumn.Z;

            _ocL[_i].SColumnInboard2_x = Force_Q_SteeringColumn.X;
            _ocL[_i].SColumnInboard2_y = Force_Q_SteeringColumn.Y;
            _ocL[_i].SColumnInboard2_z = Force_Q_SteeringColumn.Z;
        }

        #endregion

        #region Tried out a Sparse Matrix
        public void Trial()
        {
            var LHS = Matrix<double>.Build.SparseOfArray(new double[,]
                {
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,27,-344,0,24,-326,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,-27,0,-101,-24,0,224,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,344,101,0,326,-224,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{ 1,    0,   0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
{ 0,    1,   0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,225,0,0},
{ 0,    0,   1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-290,0,0},
{ 0,   80, -307,0,100,-305,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-5130,0,0},
{-80,   0, -108,-100,0,220,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1148,0,0},
{307, 108,   0,305,-220,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-873,0,0},
{ 0,    0,   0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,12,0},
{ 0,    0,   0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,353,0},
{ 0,    0,   0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,46,0},
{ 0,    0,   0,0,0,0,0,0,0,0,-175,-23,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-17083,0},
{ 0,    0,   0,0,0,0,0,0,0,175,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-2516,0},
{ 0,    0,   0,0,0,0,0,0,0,23,-7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,23764,0},
{ 0,    0,   0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,-12,0},
{ 0,    0,   0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,-353,0},
{ 0,    0,   0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,-46,0},
{ 0,    0,   0,0,0,0,0,-5,-30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-5,-30,0,0,0},
{ 0,    0,   0,0,0,0,5,0,-10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,10,0,0,0},
{ 0,    0,   0,0,0,0,30,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,30,-10,0,0,0,0},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2.25,0,1},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,506,0,-9},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-652,0,482},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-15510,0,28236},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,270,0,-558},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,156,0,-69},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,-1},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,9},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,-482},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-5,-30,0,-5,-30,0,0,0,0,0,-2874},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,10,5,0,-10,0,0,0,0,0,-32292},
{ 0,    0,   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,30,-10,0,30,10,0,0,0,0,0,0,-597},
                });

            Vector<double> RHS = Vector<double>.Build.SparseOfArray(new double[]
                {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,10000,570000,40000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                });


            var RRR = LHS.Solve(RHS);

            var inn = LHS.PseudoInverse();

            var sol = inn.Multiply(RHS);




            var TrialLHS = Matrix<double>.Build.SparseOfArray(new double[,]
                {
                    { 0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0},
                    { 0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0},
                    { 0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
                    { 0,0,0,0,0,0,0,0,0,0,-9,-275,-9,-275,0,0,0,0,0},
                    { 0,0,0,0,0,0,0,0,0,-9,0,-115,0,115,0,0,0,0,0},
                    { 0,0,0,0,0,0,0,0,0,275,115,0,-115,0,0,0,0,0,0},
                    { 1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,3},
                    { 0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,-9},
                    { 0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,-436},
                    { 0,-15,-329,0,-1,-239,0,0,0,0,0,0,0,0,0,0,0,0,42521},
                    { 15,0,-115,1,0,115,0,0,0,0,0,0,0,0,0,0,0,0,-69 },
                    { 239,115,0,239,-115,0,0,0,0,0,0,0,0,0,0,0,0,0,294},
                    { 0,0,0,0,0,0,-1,0,0,0,0,0,0,0,-1,0,0,0,0},
                    { 0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,-1,0,228,0},
                    { 0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,-1,-8,0},
                    { 0,0,0,0,0,0,0,216,31,0,0,0,0,0,0,0,0,-60276,0},
                    { 0,0,0,0,0,0,-216,0,-19,0,0,0,0,0,0,0,0,-1304,0},
                    { 0,0,0,0,0,0,-31,19,0,0,0,0,0,0,0,0,0,-37164,0},
                    { 0,0,0,230,0,14,0,0,0,0,0,0,0,0,0,0,0,0,0},
                });

            var TrialResult = Vector<double>.Build.SparseOfArray(new double[]
                {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,-10000,-580000,-70000,0,0
                });


            var x = TrialLHS.Solve(TrialResult);

            var HartyIn = TrialLHS.PseudoInverse();
            var Y = HartyIn.Multiply(TrialResult);

            var TrialBCEQLHS = Matrix<double>.Build.DenseOfArray(new double[,]
                {
                    {  1,    0,   0,   1,   0,    0},
                    {  0,    1,   0,   0,   1,    0},
                    {  0,    0,   1,   0,   0,    1},
                    {  0,   -5, -30,   0,   5,   30},
                    {  5,    0, -10,  -5,   0,  -10},
                    { 30,   10,   0, -30,  10,    0}
                });

            var TrialBCERHS = Vector<double>.Build.Dense(new double[]
                {
                    0,0,-523,3138,0,0
                });

            var INNverse = TrialBCEQLHS.PseudoInverse();

            var TriaLBCEWQResult = INNverse.Multiply(TrialBCERHS);

            var T = TrialBCEQLHS.Solve(TrialBCERHS);






        }
        #endregion


    }
}

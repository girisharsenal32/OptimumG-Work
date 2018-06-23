﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;
using MathNet.Spatial.Units;
using devDept.Eyeshot.Entities;

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

        /// <summary>
        /// Object of the <see cref="KO_BumpSteer_Solver"/> to call the methods to solve for Inboard Toe Link Point for a given Bump Steer Curve
        /// </summary>
        public KO_BumpSteer_Solver KO_BS_SOlver;

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

        #region --Suspension Parameters - Parameter's Input Values provided by User--

        //--Parameter Variation Curves--

        /// <summary>
        /// Object of the <see cref="CustomBumpSteerParams"/> which contains information regarding the Custom Curve of BUmp Steer which the user has generated
        /// </summary>
        public CustomBumpSteerParams BumpSteerCurve { get; set; }

        /// <summary>
        /// Convergence of the Bump Steer
        /// </summary>
        public Convergence BumpSteerConvergence { get; set; }

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
        public double MechTrail { get; set; }

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

        /// <summary>
        /// Pitman Trail to compute the Outboard Toe Link Point
        /// </summary>
        public double PitmanTrail { get; set; }

        /// <summary>
        /// Length of the Toe Link. This is used along with the <see cref="VehicleCornerParams.ToeLinkOutboard"/> to generate an initial guess for the <see cref="VehicleCornerParams.ToeLinkInboard"/>
        /// so that the Optimizer can then optimize it for Bump Steer
        /// </summary>
        public double ToeLinkLength { get; set; }

        /// <summary>
        /// <see cref="Point3D"/> representing the Contact Patch Initialized using the Track and Wheelbase
        /// </summary>
        public Point3D ContactPatch { get; set; }

        #endregion

        #region --Vehicle's Corner Components (Includes Tire, Spring, Damper, ARB, Wheel Alignment, Suspension Coordinates)--
        /// <summary>
        /// Object of the <see cref="VehicleCornerParams"/> Class containing ALL the Front Left Corner Compontnents
        /// </summary>
        public VehicleCornerParams VCornerParams { get; set; }
        #endregion


        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public KO_CornverVariables(VehicleCorner _vCorner)
        {
            VCorner = _vCorner;

            KO_MasterAdjs = new Dictionary<string, KO_AdjToolParams>();

            KO_ReqParams = new List<SuspensionParameters>();

            KO_ReqParams_Importance = new Dictionary<SuspensionParameters, double>();

            BumpSteerCurve = new CustomBumpSteerParams();

            CamberCurve = new CustomCamberCurve();

            Caster = new Angle();

            KPI = new Angle();

            VCornerParams = new VehicleCornerParams();

            ContactPatch = new Point3D();

        }


        /// <summary>
        /// Method to Intialze the Corner Components of a given corner of a Vehicle
        /// </summary>
        /// <param name="_vehicle">The <see cref="Vehicle"/> class' object</param>
        /// <param name="_vCorner">Corner of the Vhicle</param>
        /// <param name="_numberOfIterations">Number of iterations that the Kinematic Solver (<see cref="DoubleWishboneKinematicsSolver"/></param> or <see cref="McPhersonKinematicsSolver"/> is going to 
        /// run for 
        /// <returns>Returns an object of the <see cref="VehicleCornerParams"/> Class</returns>
        public void Initialize_VehicleCornerParams(ref KO_CornverVariables _koCV, Vehicle _vehicle, VehicleCorner _vCorner, int _numberOfIterations)
        {
            Dictionary<string, object> tempVehicleParams = VehicleParamsAssigner.AssignVehicleParams_PreKinematicsSolver(_vCorner, _vehicle, _numberOfIterations);



            ///<summary>Passing the <see cref="Dictionary{TKey, TValue}"/> of Vehicle Params's objects into the right Parameter</summary>
            _koCV.VCornerParams.SCM = tempVehicleParams["SuspensionCoordinateMaster"] as SuspensionCoordinatesMaster;

            _koCV.VCornerParams.SCM_Clone = new SuspensionCoordinatesMaster();
            _koCV.VCornerParams.SCM_Clone.Clone(VCornerParams.SCM);

            _koCV.VCornerParams.Tire = tempVehicleParams["Tire"] as Tire;

            _koCV.VCornerParams.Spring = tempVehicleParams["Spring"] as Spring;

            _koCV.VCornerParams.Damper = tempVehicleParams["Damper"] as Damper;

            _koCV.VCornerParams.ARB = tempVehicleParams["AntirollBar"] as AntiRollBar;
            _koCV.VCornerParams.ARBRate_Nmm = (double)tempVehicleParams["ARB_Rate_Nmm"];

            _koCV.VCornerParams.WA = tempVehicleParams["WheelAlignment"] as WheelAlignment;

            ///<remarks>Chassis is not a part of the <see cref="VehicleCornerParams"/> and hence it is taken care of outside of this method just like the <see cref="Vehicle"/></remarks>

            _koCV.VCornerParams.OC = tempVehicleParams["OutputClass"] as List<OutputClass>;

            _koCV.VCornerParams.OC_BumpSteer = VehicleParamsAssigner.AssignVehicleParams_Custom_OC_BumpSteer(VCornerParams.SCM, _vCorner, _vehicle, /*Setup_CV.BS_Params.WheelDeflections.Count*/_numberOfIterations);

            _koCV.VCornerParams.Identifier = (int)tempVehicleParams["Identifier"];

            //VCornerParams.Initialize_Points();

            //return VCornerParams;
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


        /// <summary>
        /// Method to compute a Point on a line using the Parametric Equation of the Line
        /// </summary>
        /// <param name="_scalar">The Scalae which represents the parameter "t" in the parametric equation</param>
        /// <param name="_startPoint">The Start Point of the Line</param>
        /// <param name="_lineToCompute">The Line being considered</param>
        /// <returns></returns>
        public Point3D Compute_PointOnLine_FromScalarParametric(double _scalar, Point3D _startPoint, Line _lineToCompute)
        {
            Point3D tempPoint = new Point3D();

            Vector3D _vectorToCompute = new Vector3D(_lineToCompute.StartPoint, _lineToCompute.EndPoint);

            _vectorToCompute.Normalize();

            tempPoint = _startPoint+(_scalar*_vectorToCompute);

            tempPoint = Round_Point(tempPoint);

            return tempPoint;
        }


        /// <summary>
        /// Method to complete a Point by using 2 Coordinates (passed as Input) and computing the 3rd Coordinate using the Equation of the Plane it lies in
        /// </summary>
        /// <param name="_wishbonePlane">Plane on which the Point lies in</param>
        /// <param name="_inputFormat">Format the user wishes to use for computing the Point</param>
        /// <param name="_pointToBeComputed">Point to be computed (with 2 coordinates input by the user)</param>
        /// <returns>Returns fully computed point</returns>
        public Point3D Compute_PointOnPlane(Plane _wishbonePlane, InboardInputFormat _inputFormat, Point3D _pointToBeComputed)
        {
            PlaneEquation _wishPlaneEq = _wishbonePlane.Equation;

            double[] _wishPlaneEqArray = _wishPlaneEq.ToArray();

            if (_inputFormat == InboardInputFormat.IIO)
            {
                _pointToBeComputed.Z = (-((_wishPlaneEqArray[0] * +_pointToBeComputed.X) + (_wishPlaneEqArray[1] * +_pointToBeComputed.Y) + (_wishPlaneEqArray[3])) / (_wishPlaneEqArray[2]));
            }
            else if (_inputFormat == InboardInputFormat.IOI)
            {
                _pointToBeComputed.Y = (-((_wishPlaneEqArray[0] * +_pointToBeComputed.X) + (_wishPlaneEqArray[2] * +_pointToBeComputed.Z) + (_wishPlaneEqArray[3])) / (_wishPlaneEqArray[1]));
            }
            else
            {
                _pointToBeComputed.X = (-((_wishPlaneEqArray[1] * +_pointToBeComputed.Y) + (_wishPlaneEqArray[2] * +_pointToBeComputed.Z) + (_wishPlaneEqArray[3])) / (_wishPlaneEqArray[0]));
            }

            _pointToBeComputed = Round_Point(_pointToBeComputed);

            return _pointToBeComputed;

        }


        /// <summary>
        /// Method to Compute the Inboard Toe Link Point using the Outboard Toe Link Point and the Toe Link Length
        /// </summary>
        /// <param name="_inboardToeLink">Inboard Toe Link Point which is going to be initialized</param>
        /// <param name="_outboardToeLink">Outboard Toe Link Point</param>
        /// <param name="_toeLinkLength">Toe Link Length</param>
        public void Compute_InboardToeLink(out Point3D _inboardToeLink, Point3D _outboardToeLink, double _toeLinkLength)
        {
            _inboardToeLink = new Point3D(_outboardToeLink.X - _toeLinkLength, _outboardToeLink.Y, _outboardToeLink.Z);

            _inboardToeLink = Round_Point(_inboardToeLink);
        }


        public void Optimize_InboardToeLink(ref KO_CornverVariables _koCV, ref KO_CentralVariables _koCentral, ref KO_SimulationParams _koSimParams, VehicleCorner _vCorner, DesignForm _designForm)
        {
            KO_BS_SOlver = new KO_BumpSteer_Solver(ref _koCV, ref _koCentral, ref _koSimParams, _vCorner, ref _designForm);

            _koCV.VCornerParams.ToeLinkInboard = Round_Point(_koCV.VCornerParams.ToeLinkInboard);
        }

        /// <summary>
        /// Method to return a Point with EACH coordinated rounded to the 3rd decimal
        /// </summary>
        /// <param name="_point"></param>
        /// <returns></returns>
        public Point3D Round_Point(Point3D _point)
        {
            _point.X = Math.Round(_point.X, 3);
            _point.Y = Math.Round(_point.Y, 3);
            _point.Z = Math.Round(_point.Z, 3);

            return _point;
        }



    }


    public enum InboardInputFormat
    {
        IIO,
        IOI,
        OII,
    }



}
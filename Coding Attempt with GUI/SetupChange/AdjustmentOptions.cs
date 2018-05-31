using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    public class AdjustmentOptions
    {
        /// <summary>
        /// Plane to represent the Top Wishbone's Plane
        /// </summary>
        public Plane TopWishbonePlane { get; set; }
        /// <summary>
        /// Line to represent the Top Front Wishbone Arm
        /// </summary>
        public List<Line> TopFrontArm { get; set; } = new List<Line>();
        /// <summary>
        /// Vector to work in unison with the <see cref="TopFrontArm"/>. This vector will be used to simulate the increase/decrease of the length of the Arm
        /// </summary>
        public List<Vector3D> TopFrontVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Line to represent the Top Rear Wishbone Arm
        /// </summary>
        public List<Line> TopRearArm { get; set; } = new List<Line>();
        /// <summary>
        /// Vector to work in unison with the <see cref="TopRearArm"/>. This vector will be used to simulate the increase/decrease of the length of the Arm
        /// </summary>
        public List<Vector3D> TopRearVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Plane to represent the Botom Wishbone's Plane
        /// </summary>
        public Plane BottomWishbonePlane { get; set; }
        /// <summary>
        /// Line to represent the Bottom Front Wishbone Arm
        /// </summary>
        public List<Line> BottomFrontArm { get; set; } = new List<Line>();
        /// <summary>
        /// Vector to work in unison with the <see cref="BottomFrontArm"/>. This vector will be used to simulate the increase/decrease of the length of the Arm
        /// </summary>
        public List<Vector3D> BottomFrontArmVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Line to represent the Bottom Rear Wishbone Arm
        /// </summary>
        public List<Line> BottomRearArm { get; set; } = new List<Line>();
        /// <summary>
        /// Vector to work in unison with the <see cref="BottomRearArm"/>. This vector will be used to simulate the increase/decrease of the length of the Arm
        /// </summary>
        public List<Vector3D> BottomRearArmVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Line which represents the length formed by the Camber Shims used for camber adjustment at the <see cref="SetupChangeDatabase.UBJ"/>
        /// </summary>
        List<Line> TopCamberShimsLine { get; set; } = new List<Line>();
        /// <summary>
        /// Line which represents the length formed by the Camber Shims used for camber adjustment at the <see cref="SetupChangeDatabase.LBJ"/>
        /// </summary>
        List<Line> BottomCamberShimsLine { get; set; } = new List<Line>();
        /// <summary>
        /// Vector which wil work in unison with the <see cref="TopCamberShimsLine"/> to allow for the increase the length of Line to simulate an increase/decrease in the number of shims on the UBJ
        /// </summary>
        List<Vector3D> TopCamberShimsVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Vector which wil work in unison with the <see cref="BottomCamberShimsLine"/> to allow for the increase the length of Line to simulate an increase/decrease in the number of shims on the LBJ
        /// </summary>
        List<Vector3D> BottomCamberShimsVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Line which represents the length formed by the pushrod used for the Ride Height Adjustment
        /// </summary>
        public List<Line> PushrodLine { get; set; } = new List<Line>();
        /// <summary>
        /// Vector which represents the length formed by the pushrod used for the RIde Height Adjustment. 
        /// </summary>
        public List<Vector3D> PushrodVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Line which represents the length formed by the Damper Eyeto the Spring Preload Adjuster Platform used for the Ride Height Adjustment
        /// </summary>
        List<Line> EyetoPerchLine { get; set; } = new List<Line>();
        /// <summary>
        /// Vector which represents the length formed by the Damper Eyeto the Spring Preload Adjuster Platform used for the Ride Height Adjustment
        /// </summary>
        List<Vector3D> EyetoPerchVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Line which represents the Spiring Preload in "mmm" used for for the Ride Height Adjustment
        /// </summary>
        List<Line> SpringPreloadLine { get; set; } = new List<Line>();
        /// <summary>
        /// Vector which represents the Spiring Preload in "mmm" used for for the Ride Height Adjustment
        /// </summary>
        List<Vector3D> SpringPreloadVector { get; set; } = new List<Vector3D>();

        /// <summary>
        /// This is the public Camber Adjuster Line which will be assigned based on User Input of <see cref="AdjustmentTools"/>
        /// </summary>
        public List<Line> MCamberAdjusterLine { get; set; }
        /// <summary>
        /// This is the public Camber Adjuster Vector which will be assigned based on User Input of <see cref="AdjustmentTools"/> and will work in Unison with <see cref="MCamberAdjusterLine"/>
        /// </summary>
        public List<Vector3D> MCamberAdjusterVector { get; set; }
        /// <summary>
        /// Axis of Rotation for Camber Rotation
        /// </summary>
        public List<Line> AxisRotation_Camber { get; set; }
        /// <summary>
        /// This is the public Caster Adjuster Line which will be assigned based on User Input of <see cref="AdjustmentTools"/>
        /// </summary>
        public List<Line> MCasterAdjustmenterLine { get; set; }
        /// <summary>
        /// This is the public Caster Adjuster Vector which will be assigned based on User Input of <see cref="AdjustmentTools"/> and will work in Unison with <see cref="MCasterAdjustmenterLine"/>
        /// </summary>
        public List<Vector3D> MCasterAdjusterVector { get; set; }
        /// <summary>
        /// Axis of Rotation for Caster Rotation
        /// </summary>
        public List<Line> AxisRotation_Caster { get; set; }
        /// <summary>
        /// This is the public KPI Adjuster Line which will be assigned based on User Input of <see cref="AdjustmentTools"/>. 
        /// </summary>
        public List<Line> MKPIAdjusterLine { get; set; }
        /// <summary>
        /// This is the public KPI Adjuster Vector which will be assigned based on User Input of <see cref="AdjustmentTools"/> and will work in Unison with <see cref="MKPIAdjusterLine"/>
        /// </summary>
        public List<Vector3D> MKPIAdjusterVector { get; set; }
        /// <summary>
        /// Axis of Rotation for the KPI Rotation
        /// </summary>
        public List<Line> AxisRotation_KPI { get; set; }
        /// <summary>
        /// This is the public Toe Adjuster Line which will be assigned based on User Input of <see cref="AdjustmentTools"/> 
        /// </summary>
        public List<Line> MToeAdjusterLine { get; set; } = new List<Line>();
        /// <summary>
        /// This is the public Camber Adjuster Vector which will be assigned based on User Input of <see cref="AdjustmentTools"/> and will work in Unison with <see cref="MToeAdjusterLine"/>
        /// </summary>
        public List<Vector3D> MToeAdjusterVector { get; set; } = new List<Vector3D>();
        /// <summary>
        /// Axis of Rotation for Toe Rotation
        /// </summary>
        public List<Line> AxisRotation_Toe { get; set; }
        /// <summary>
        /// This is the public Ride Height Adjuster Line which will be assigned based on User Input of <see cref="AdjustmentTools"/>
        /// </summary>
        public List<Line> MRideHeightAdjusterLine { get; set; }
        /// <summary>
        /// This is the public Ride Height Adjuster Vector which will be assigned based on User Input of <see cref="AdjustmentTools"/> and will work in Unison with <see cref="MRideHeightAdjusterLine"/>
        /// </summary>
        public List<Vector3D> MRideHeightAdjusterVector { get; set; }

        public int UprightIndexForCamber { get; set; } = 0;

        public int UprightIndexForCaster { get; set; } = 0;

        public int UprightIndexForKPI { get; set; } = 0;

        public int UprightIndexForToe { get; set; } = 2;

        /// <summary>
        /// Method to Initialize all the Adjustment tools and decide which the Master Adjusters are going to be 
        /// </summary>
        /// <param name="scm"></param>
        /// <param name="scd"></param>
        /// <param name="adjToolDictionary">Dictionary which is initialized with all the <see cref="AdjustmentTools"/> the user has selected for each Setup Change</param>
        public AdjustmentOptions(SuspensionCoordinatesMaster scm, SetupChangeDatabase scd, Dictionary<string, AdjustmentTools> adjToolDictionary, SetupChange_CornerVariables sccv)
        {
            TopWishbonePlane = new Plane(new Point3D(scm.F1x, scm.F1y, scm.F1z), new Point3D(scm.A1x, scm.A1y, scm.A1z), new Point3D(scm.B1x, scm.B1y, scm.B1z));

            TopFrontArm.Add(new Line(new Point3D(scm.A1x, scm.A1y, scm.A1z), new Point3D(scm.F1x, scm.F1y, scm.F1z) /*scd.UBJ*/));

            TopFrontVector.Add(new Vector3D(TopFrontArm[TopFrontArm.Count - 1].StartPoint, TopFrontArm[TopFrontArm.Count - 1].EndPoint));

            TopRearArm.Add(new Line(new Point3D(scm.B1x, scm.B1y, scm.B1z), new Point3D(scm.F1x, scm.F1y, scm.F1z) /*scd.UBJ*/));

            TopRearVector.Add(new Vector3D(TopRearArm[TopRearArm.Count - 1].StartPoint, TopRearArm[TopRearArm.Count - 1].EndPoint));

            BottomWishbonePlane = new Plane(new Point3D(scm.E1x, scm.E1y, scm.E1z), new Point3D(scm.D1x, scm.D1y, scm.D1z), new Point3D(scm.C1x, scm.C1y, scm.C1z));

            BottomFrontArm.Add(new Line(new Point3D(scm.D1x, scm.D1y, scm.D1z), new Point3D(scm.E1x, scm.E1y, scm.E1z) /*scd.LBJ*/));

            BottomFrontArmVector.Add(new Vector3D(BottomFrontArm[BottomFrontArm.Count - 1].StartPoint, BottomFrontArm[BottomFrontArm.Count - 1].EndPoint));

            BottomRearArm.Add(new Line(new Point3D(scm.C1x, scm.C1y, scm.C1z), new Point3D(scm.E1x, scm.E1y, scm.E1z) /*scd.LBJ*/));

            BottomRearArmVector.Add(new Vector3D(BottomRearArm[BottomRearArm.Count - 1].StartPoint, BottomRearArm[BottomRearArm.Count - 1].EndPoint));

            MToeAdjusterLine.Add(new Line(new Point3D(scm.N1x, scm.N1y, scm.N1z), new Point3D(scm.M1x, scm.M1y, scm.M1z) /*scd.ToeLinkUpright*/));

            MToeAdjusterVector.Add(new Vector3D(MToeAdjusterLine[MToeAdjusterLine.Count - 1].StartPoint, MToeAdjusterLine[MToeAdjusterLine.Count - 1].EndPoint));

            PushrodLine.Add(new Line(new Point3D(scm.J1x, scm.J1y, scm.J1z), new Point3D(scm.G1x, scm.G1y, scm.G1z)));

            PushrodVector.Add(new Vector3D(PushrodLine[PushrodLine.Count - 1].StartPoint, PushrodLine[PushrodLine.Count - 1].EndPoint));

            InitializeShimsGeometry(scm, scd, TopCamberShimsLine, TopCamberShimsVector, TopWishbonePlane, scd.UBJ);

            InitializeShimsGeometry(scm, scd, BottomCamberShimsLine, BottomCamberShimsVector, BottomWishbonePlane, scd.LBJ);

            //InitUprightBasedOnAdjustmentTool(adjToolDictionary, scd);

            AssignMasterAdjusters(sccv, adjToolDictionary, scd);

        }

        /// <summary>
        /// Method to Assign the Master Adjusters based on the <see cref="AdjustmentTools"/> which the user has selected for each SetupChange 
        /// </summary>
        /// <param name="adjToolDictionary">tionary which is initialized with all the <see cref="AdjustmentTools"/> the user has selected for each Setup Change</param>
        internal void AssignMasterAdjusters(SetupChange_CornerVariables sccv, Dictionary<string, AdjustmentTools> adjToolDictionary, SetupChangeDatabase scd)
        {
            AssignCamberAdjust(sccv, adjToolDictionary, scd);

            AssignToeAdjuster(scd);

            AssignCasterAdjuster(sccv, adjToolDictionary, scd);

            AssignKPIAdjuster(sccv, adjToolDictionary, scd);

            AssignRideHeightAdjuster(adjToolDictionary);
        }

        /// <summary>
        /// Method to assing the Camber Master Adjuster
        /// </summary>
        /// <param name="_adjToolDictionary">tionary which is initialized with all the <see cref="AdjustmentTools"/> the user has selected for each Setup Change</param>
        private void AssignCamberAdjust(SetupChange_CornerVariables _sccv, Dictionary<string, AdjustmentTools> _adjToolDictionary, SetupChangeDatabase _scd)
        {
            if (_sccv.camberAdjustmentTool == AdjustmentTools.TopCamberMount || _sccv.camberAdjustmentTool == AdjustmentTools.DirectValue)
            {
                MCamberAdjusterLine = TopCamberShimsLine;
                MCamberAdjusterVector = TopCamberShimsVector;
                AxisRotation_Camber = _scd.LBJToToeLink.Line.DeltaLine;
                _sccv.AdjToolsDictionary["CamberChange"] = _sccv.camberAdjustmentTool;
                UprightIndexForCamber = 0;
            }
            else if (_sccv.camberAdjustmentTool == AdjustmentTools.BottomCamberMount)
            {
                MCamberAdjusterLine = BottomCamberShimsLine;
                MCamberAdjusterVector = BottomCamberShimsVector;
                AxisRotation_Camber = _scd.UBJToToeLink.Line.DeltaLine;
                _sccv.AdjToolsDictionary["CamberChange"] = _sccv.camberAdjustmentTool;
                UprightIndexForCamber = 1;
            }
        }

        private void AssignToeAdjuster(SetupChangeDatabase _scd)
        {
            AxisRotation_Toe = _scd.SteeringAxis.Line.DeltaLine;
        }

        /// <summary>
        /// Method to assign the Caster Master Adjuster
        /// </summary>
        /// <param name="_adjToolDictionary">tionary which is initialized with all the <see cref="AdjustmentTools"/> the user has selected for each Setup Change</param>
        private void AssignCasterAdjuster(SetupChange_CornerVariables _sccv, Dictionary<string, AdjustmentTools> _adjToolDictionary, SetupChangeDatabase _scd)
        {
            if (_sccv.casterAdjustmentTool == AdjustmentTools.TopFrontArm || _sccv.casterAdjustmentTool == AdjustmentTools.DirectValue)
            {
                _adjToolDictionary["CasterChange"] = _sccv.casterAdjustmentTool;
                MCasterAdjustmenterLine = TopFrontArm;
                MCasterAdjusterVector = TopFrontVector;
                AxisRotation_Caster = _scd.LBJToToeLink.Line.DeltaLine;
                UprightIndexForCaster = 0;
            }
            else if (_sccv.casterAdjustmentTool == AdjustmentTools.TopRearArm)
            {
                _adjToolDictionary["CasterChange"] = _sccv.casterAdjustmentTool;
                MCasterAdjustmenterLine = TopRearArm;
                MCasterAdjusterVector = TopRearVector;
                AxisRotation_Caster = _scd.LBJToToeLink.Line.DeltaLine;
                UprightIndexForCaster = 0;
            }
            else if (_sccv.casterAdjustmentTool == AdjustmentTools.BottomFrontArm)
            {
                _adjToolDictionary["CasterChange"] = _sccv.casterAdjustmentTool;
                MCasterAdjustmenterLine = BottomFrontArm;
                MCasterAdjusterVector = BottomFrontArmVector;
                AxisRotation_Caster = _scd.UBJToToeLink.Line.DeltaLine;
                UprightIndexForCaster = 1;
            }
            else if (_sccv.casterAdjustmentTool == AdjustmentTools.BottomRearArm)
            {
                _adjToolDictionary["CasterChange"] = _sccv.casterAdjustmentTool;
                MCasterAdjustmenterLine = BottomRearArm;
                MCasterAdjusterVector = BottomRearArmVector;
                AxisRotation_Caster = _scd.UBJToToeLink.Line.DeltaLine;
                UprightIndexForCaster = 1;
            }
        }

        /// <summary>
        /// Method to assign the KPI Master Adjuster
        /// </summary>
        /// <param name="_adjToolDictionary">tionary which is initialized with all the <see cref="AdjustmentTools"/> the user has selected for each Setup Change</param>
        private void AssignKPIAdjuster(SetupChange_CornerVariables _sccv, Dictionary<string, AdjustmentTools> _adjToolDictionary, SetupChangeDatabase _scd)
        {
            if (_sccv.kpiAdjustmentTool == AdjustmentTools.TopFrontArm || _sccv.kpiAdjustmentTool == AdjustmentTools.DirectValue)
            {
                _adjToolDictionary["KPIChange"] = _sccv.kpiAdjustmentTool;
                MKPIAdjusterLine = TopFrontArm;
                MKPIAdjusterVector = TopFrontVector;
                AxisRotation_KPI = _scd.LBJToToeLink.Line.DeltaLine;
                UprightIndexForKPI = 0;

            }
            else if (_sccv.kpiAdjustmentTool == AdjustmentTools.TopRearArm)
            {
                _adjToolDictionary["KPIChange"] = _sccv.kpiAdjustmentTool;
                MKPIAdjusterLine = TopRearArm;
                MKPIAdjusterVector = TopRearVector;
                AxisRotation_KPI = _scd.LBJToToeLink.Line.DeltaLine;
                UprightIndexForKPI = 0;
            }
            else if (_sccv.kpiAdjustmentTool == AdjustmentTools.BottomFrontArm)
            {
                _adjToolDictionary["KPIChange"] = _sccv.kpiAdjustmentTool;
                MKPIAdjusterLine = BottomFrontArm;
                MKPIAdjusterVector = BottomFrontArmVector;
                AxisRotation_KPI = _scd.UBJToToeLink.Line.DeltaLine;
                UprightIndexForKPI = 1;
            }
            else if (_sccv.kpiAdjustmentTool == AdjustmentTools.BottomRearArm)
            {
                _adjToolDictionary["KPIChange"] = _sccv.kpiAdjustmentTool;
                MKPIAdjusterLine = BottomRearArm;
                MKPIAdjusterVector = BottomRearArmVector;
                AxisRotation_KPI = _scd.UBJToToeLink.Line.DeltaLine;
                UprightIndexForKPI = 1;
            }
        }
        
        /// <summary>
        /// Method to assign the Ride Height Master Adjuster
        /// </summary>
        /// <param name="_adjToolDictionary">tionary which is initialized with all the <see cref="AdjustmentTools"/> the user has selected for each Setup Change</param>
        private void AssignRideHeightAdjuster(Dictionary<string, AdjustmentTools> _adjToolDictionary)
        {
            if (_adjToolDictionary["RideHeightChange"] == AdjustmentTools.PushrodLength )
            {
                MRideHeightAdjusterLine = PushrodLine;
                MRideHeightAdjusterVector = PushrodVector;
            }
            else if (_adjToolDictionary["RideHeightChange"] == AdjustmentTools.SpringPreloadValue)
            {
                MRideHeightAdjusterLine = SpringPreloadLine;
                MRideHeightAdjusterVector = SpringPreloadVector;
            }
            else if (_adjToolDictionary["RideHeightChange"] == AdjustmentTools.DamperEyetoSpringPerch)
            {
                MRideHeightAdjusterLine = EyetoPerchLine;
                MRideHeightAdjusterVector = EyetoPerchVector;
            }
        }

        //private void InitUprightBasedOnAdjustmentTool(Dictionary<string, AdjustmentTools> _adjToolDictionary, SetupChangeDatabase _scd)
        //{
        //    if (_adjToolDictionary["CamberChange"] == AdjustmentTools.TopCamberMount)
        //    {
        //        GetNewUprightTrianglePosition(ref _scd.UprightTriangle, TopCamberShimsLine[TopCamberShimsLine.Count - 1].EndPoint, _scd.LBJ, _scd.ToeLinkUpright, _scd);
        //    }
        //    else if (_adjToolDictionary["CamberChange"] == AdjustmentTools. BottomCamberMount)
        //    {

        //    }


        //}

        /// <summary>
        /// Method to get a Vector and Line which represent the group of Shims.These maybe camber shims at the Camber Mount or Link Shims at the Wishbone Arms (or Toe Link) Obtained by drawing a line at the UBJ or LBJ on the Wishbone Plane and then making it parallel in the 
        /// Top View with the Wheel Spindle
        /// </summary>
        /// <param name="_scmPlane"></param>
        /// <param name="_scd"></param>
        /// <param name="_camberShimsLine"></param>
        /// <param name="_camberShimsVector"></param>
        /// <param name="_wishbonePlane"></param>
        /// <param name="_UBJorLBJ"></param>
        private void InitializeShimsGeometry(SuspensionCoordinatesMaster _scmPlane, SetupChangeDatabase _scd, List<Line> _camberShimsLine, List<Vector3D> _camberShimsVector, Plane _wishbonePlane, Point3D _UBJorLBJ)
        {

            ///<summary>Creating a temp line to represent the Camber Shims Line and will added to the <see cref="BottomCamberShimsLine"/> OR <see cref="TopCamberShimsLine"/></summary>
            ///<remarks>
            ///COordinate are passed as (0,0) and (5,0). This is becase the <see cref="TopWishbonePlane"/> and <see cref="BottomWishbonePlane"/> have the UBJ and LBJ as their origins. 
            ///Hence, you need to pass 0,0 and 5,0 so that the Camber Line starts at UBJ and extends by 5 in X direction
            ///</remarks>
            Line _tempCamberShimsLine = new Line(_wishbonePlane, /*_UBJorLBJ.X*/0, /*_UBJorLBJ.Z*/0, /*_UBJorLBJ.X + */5, /*_UBJorLBJ.Z*/0);
            

            Angle angle = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(new Line(_tempCamberShimsLine.StartPoint.X, 0, _tempCamberShimsLine.StartPoint.Z, _tempCamberShimsLine.EndPoint.X, 0, _tempCamberShimsLine.EndPoint.Z)),
                                                                  Custom3DGeometry.GetMathNetVector3D(new Line(_scd.WheelSpindle.Line.DeltaLine[0].StartPoint.X, 0, _scd.WheelSpindle.Line.DeltaLine[0].StartPoint.Z,
                                                                                                                  _scd.WheelSpindle.Line.DeltaLine[0].EndPoint.X, 0, _scd.WheelSpindle.Line.DeltaLine[0].EndPoint.Z)),
                                                                  Custom3DGeometry.GetMathNetVector3D(_scd.WheelCentreAxis.Vertical));

            _tempCamberShimsLine.Rotate(-angle.Radians, _tempCamberShimsLine.StartPoint, new Point3D(_tempCamberShimsLine.StartPoint.X, _tempCamberShimsLine.StartPoint.Y + 100, _tempCamberShimsLine.StartPoint.Z));

            _camberShimsLine.Add(_tempCamberShimsLine);

            _camberShimsVector.Add(new Vector3D(new Point3D(_tempCamberShimsLine.StartPoint.X, _tempCamberShimsLine.StartPoint.Y, _tempCamberShimsLine.StartPoint.Z), new Point3D(_tempCamberShimsLine.EndPoint.X, _tempCamberShimsLine.EndPoint.Y, _tempCamberShimsLine.EndPoint.Z)));

        }

        public void AddAdjusterToMasterAdjusterList(List<Line> _adjusterLine, List<Vector3D> _adjusterVector)
        {
            _adjusterVector.Add(new Vector3D(_adjusterVector[_adjusterVector.Count - 1].X, _adjusterVector[_adjusterVector.Count - 1].Y, _adjusterVector[_adjusterVector.Count - 1].Z));

            _adjusterLine.Add(new Line(_adjusterLine[_adjusterLine.Count - 1].StartPoint.X, _adjusterLine[_adjusterLine.Count - 1].StartPoint.Y, _adjusterLine[_adjusterLine.Count - 1].StartPoint.Z,
                                         _adjusterLine[_adjusterLine.Count - 1].EndPoint.X, _adjusterLine[_adjusterLine.Count - 1].EndPoint.Y, _adjusterLine[_adjusterLine.Count - 1].EndPoint.Z));
        }

        /// <summary>
        /// Method to calculate the increase of the Shims Vector. Can be used for both <see cref="TopCamberShimsLine"/> and eventually when I add Shims for Toe, Caster, KPI etc
        /// </summary>
        /// <param name="_shimsLine"></param>
        /// <param name="_shimsVector"></param>
        /// <param name="_noOfShims"></param>
        /// <param name="_thickness"></param>
        public void NoOfShims_OR_ShimsVectorLengthChanged(List<Line> _shimsLine, List<Vector3D> _shimsVector, double _deltaLength)
        {
            AddAdjusterToMasterAdjusterList(_shimsLine, _shimsVector);

            double changeInShimLength = _deltaLength;

            _shimsVector[_shimsVector.Count - 1].Length += changeInShimLength;

            _shimsLine[_shimsLine.Count - 1].EndPoint.X = +_shimsVector[_shimsVector.Count - 1].X + _shimsLine[_shimsLine.Count - 1].StartPoint.X;
            _shimsLine[_shimsLine.Count - 1].EndPoint.Y = +_shimsVector[_shimsVector.Count - 1].Y + _shimsLine[_shimsLine.Count - 1].StartPoint.Y;
            _shimsLine[_shimsLine.Count - 1].EndPoint.Z = +_shimsVector[_shimsVector.Count - 1].Z + _shimsLine[_shimsLine.Count - 1].StartPoint.Z;

        }

        public void LinkLengthChanged(double _dLinkLength, Plane _linksPlane, List<Line> _link, List<Vector3D> _linnkVector, List<Line> _counterLink, SetupChangeDatabase _scd, SolverMasterClass _smc)
        {
            AddAdjusterToMasterAdjusterList(_link, _linnkVector);

            Custom3DGeometry _counter = new Custom3DGeometry(new Point3D(_counterLink[_counterLink.Count - 1].StartPoint.X, _counterLink[_counterLink.Count - 1].StartPoint.Y, _counterLink[_counterLink.Count - 1].StartPoint.Z), 
                                                             new Point3D(_counterLink[_counterLink.Count - 1].EndPoint.X, _counterLink[_counterLink.Count - 1].EndPoint.Y, _counterLink[_counterLink.Count - 1].EndPoint.Z));

            Angle FV = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(_counter.ViewLines.FrontView.DeltaLine[_counter.ViewLines.FrontView.DeltaLine.Count-1]),
                                                               Custom3DGeometry.GetMathNetVector3D(_scd.WheelCentreAxis.Vertical),
                                                               Custom3DGeometry.GetMathNetVector3D(_scd.WheelCentreAxis.Longitudinal));
            FV = new Angle(90 + FV.Degrees, AngleUnit.Degrees);

            _counter.PerpAlongY.Aft.Translate(_counterLink[_counterLink.Count - 1].MidPoint.X - _counterLink[_counterLink.Count - 1].StartPoint.X, 
                                              _counterLink[_counterLink.Count - 1].MidPoint.Y - _counterLink[_counterLink.Count - 1].StartPoint.Y, 
                                              _counterLink[_counterLink.Count - 1].MidPoint.Z - _counterLink[_counterLink.Count - 1].StartPoint.Z);


            Triangle SV = new Triangle(_link[_link.Count - 1].StartPoint, _link[_link.Count - 1].EndPoint, _counterLink[_counterLink.Count - 1].StartPoint);

            SV.Regen(0);

            Angle test = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(_counter.PerpAlongY.Aft),
                                                                 new MathNet.Spatial.Euclidean.Vector3D(SV.Normal.X, SV.Normal.Y, SV.Normal.Z),
                                                                 Custom3DGeometry.GetMathNetVector3D(_scd.WheelCentreAxis.Lateral));

            _counter.PerpAlongY.Aft.Rotate(FV.Radians,Custom3DGeometry.GetdevDeptVector3D(_scd.WheelCentreAxis.Longitudinal));
            _counter.PerpAlongY.Aft.Rotate(test.Radians, Custom3DGeometry.GetdevDeptVector3D(_scd.WheelCentreAxis.Lateral));

            Angle testAngleRotatate = new Angle(_dLinkLength / _counterLink[_counterLink.Count - 1].Length(), AngleUnit.Radians);

            Start:
            _counterLink[_counterLink.Count - 1].Rotate(testAngleRotatate.Radians, Custom3DGeometry.GetdevDeptVector3D(_counter.PerpAlongY.Aft));
            _link[_link.Count - 1].EndPoint.X = _counterLink[_counterLink.Count - 1].EndPoint.X;
            _link[_link.Count - 1].EndPoint.Y = _counterLink[_counterLink.Count - 1].EndPoint.Y;
            _link[_link.Count - 1].EndPoint.Z = _counterLink[_counterLink.Count - 1].EndPoint.Z;

            double staticLength = _link[0].Length();
            double newLenggth = _link[_link.Count - 1].Length();

            double checker =_dLinkLength - (_link[_link.Count - 1].Length()- _link[0].Length());

            if (Math.Abs(checker) > 0.5) 
            {
                AddAdjusterToMasterAdjusterList(_link, _linnkVector);
                double testCounter = _counterLink[_counterLink.Count - 1].Length();
                testAngleRotatate = new Angle((checker / _counterLink[_counterLink.Count - 1].Length()),AngleUnit.Radians);
                goto Start;
            }
            

        }


        /// <summary>
        /// <para>Method to get the Angle between the Last and 2nd Last position of the Upright</para>
        /// <para>At the end of the this method, the Upright is returned to its 2nd last position. This is because, in this method I cam literally stretching the Upright to a position defined by the <paramref name="_shimsOrLinkLine"/>'s End Point. This is not True. In actuality the Upright is 
        /// rotated when the <paramref name="_shimsOrLinkLine"/> is increase/decreased. So the Upright needs to be Rotated by the angle calculated and then the <paramref name="_shimsOrLinkLine"/>'s End Point shoud be set using 
        /// <see cref="GetLinkLengthChangeFromUprightAngle(List{Line}, SetupChangeDatabase, CurrentChange, AdjustmentTools)"/> and an iterative loop should be formed until the <paramref name="_shimsOrLinkLine"/> is of length which the user wants</para>
        /// </summary>
        /// <param name="_upright">List of Upright Triangles belonging to <see cref="SetupChangeDatabase"/> with UBJ[0] LBJ[1] and ToeLink[2] </param>
        /// <param name="_vertex1">This could be any of the other 3 points. Remeber UBJ[0] LBJ[1] and ToeLink[2] while passing c</param>
        /// <param name="_vertex2">This could be any of the other 3 points. Remeber UBJ[0] LBJ[1] and ToeLink[2] while passing  </param>
        /// <param name="_vertex3">This could be any of the other 3 points. Remeber UBJ[0] LBJ[1] and ToeLink[2] while passing</param>
        /// <param name="_scd"></param>
        /// <returns></returns>
        public Angle GetNewUprightTrianglePosition(ref List<Triangle> _upright, Point3D _vertex1, Point3D _vertex2, Point3D _vertex3, SetupChangeDatabase _scd, Line _axisOfRotation)
        {
            Angle rotatedAngle = new Angle(0, AngleUnit.Radians);


            _upright.Add(new Triangle(new Point3D(_vertex1.X, _vertex1.Y, _vertex1.Z),
                                      new Point3D(_vertex2.X, _vertex2.Y, _vertex2.Z),
                                      new Point3D(_vertex3.X, _vertex3.Y, _vertex3.Z)));


            for (int i = 0; i < _upright.Count; i++)
            {
                _upright[i].Regen(0);
            }

            rotatedAngle = SetupChangeDatabase.AngleInRequiredView(new MathNet.Spatial.Euclidean.Vector3D(_upright[_upright.Count - 1].Normal.X, _upright[_upright.Count - 1].Normal.Y, _upright[_upright.Count - 1].Normal.Z),
                                                                   new MathNet.Spatial.Euclidean.Vector3D(_upright[_upright.Count - 2].Normal.X, _upright[_upright.Count - 2].Normal.Y, _upright[_upright.Count - 2].Normal.Z),
                                                                   Custom3DGeometry.GetMathNetVector3D(_scd.WheelCentreAxis.Longitudinal));

            //rotatedAngle = SetupChangeDatabase.AngleInRequiredView(new MathNet.Spatial.Euclidean.Vector3D(_upright[_upright.Count - 1].Normal.X, _upright[_upright.Count - 1].Normal.Y, _upright[_upright.Count - 1].Normal.Z),
            //                                           new MathNet.Spatial.Euclidean.Vector3D(_upright[_upright.Count - 2].Normal.X, _upright[_upright.Count - 2].Normal.Y, _upright[_upright.Count - 2].Normal.Z),
            //                                           Custom3DGeometry.GetMathNetVector3D(_scd.WheelCentreAxis.Vertical));

            rotatedAngle = SetupChangeDatabase.AngleInRequiredView(new MathNet.Spatial.Euclidean.Vector3D(_upright[_upright.Count - 1].Normal.X, _upright[_upright.Count - 1].Normal.Y, _upright[_upright.Count - 1].Normal.Z),
                                                                   new MathNet.Spatial.Euclidean.Vector3D(_upright[_upright.Count - 2].Normal.X, _upright[_upright.Count - 2].Normal.Y, _upright[_upright.Count - 2].Normal.Z),
                                                                   Custom3DGeometry.GetMathNetVector3D(/*_scd.LBJToToeLink.Line.DeltaLine[_scd.LBJToToeLink.Line.DeltaLine.Count - 1]*/ _axisOfRotation));

            ///<summary>Reverting the Upright to its original state so it can rotated and brought to the right state</summary>
            _upright[_upright.Count - 1] = (new Triangle(new Point3D(_upright[_upright.Count - 2].Vertices[0].X, _upright[_upright.Count - 2].Vertices[0].Y, _upright[_upright.Count - 2].Vertices[0].Z),
                                                         new Point3D(_upright[_upright.Count - 2].Vertices[1].X, _upright[_upright.Count - 2].Vertices[1].Y, _upright[_upright.Count - 2].Vertices[1].Z),
                                                         new Point3D(_upright[_upright.Count - 2].Vertices[2].X, _upright[_upright.Count - 2].Vertices[2].Y, _upright[_upright.Count - 2].Vertices[2].Z)));

            return rotatedAngle;
        }

        /// <summary>
        /// <para>Method to the get the change in link length (or Shimslength) using the latest Position of the Upright Triangle</para>
        /// <para>This method will be used for both Gettting the Link Length because of making an angle change with direct user input in the form of Angle OROROR
        /// will be used for an iterative loop approach when the user has requested a length change. Since I don't have a method like Solidworks to constrain the sides of the upright triangle, I will increase the link length long itself thereyby stretching the upright and finding the angle</para> 
        /// <para>Then I will return thr upright to its original postion and rotate it about that angle and use this method to get the new length and end point of the LinkLength Line. This will continue iteratively until the i get the length required by the user</para>
        /// </summary>
        /// <param name="_linkOrShimLine">List of the ShimsLine or LinkLine </param>
        /// <param name="_scd"></param>
        /// <param name="_currentChange"></param>
        /// <param name="_adjTool"></param>
        /// <param name="_vIndexUpright">This is the Index of the Vertices of the Upright which the method should Consider. Remeber UBJ[0] LBJ[1] and ToeLink[2] while passing </param>
        /// <returns></returns>
        public double GetLinkLengthChangeFromUprightAngle(List<Line> _linkOrShimLine, List<Vector3D> _linkOrShimVector, SetupChangeDatabase _scd, int _vIndexUpright)
        {
            double latestLinkLengthChange = 0;
            _linkOrShimLine[_linkOrShimLine.Count - 1].EndPoint = new Point3D(_scd.UprightTriangle[_scd.UprightTriangle.Count - 1].Vertices[_vIndexUpright].X,
                                                                              _scd.UprightTriangle[_scd.UprightTriangle.Count - 1].Vertices[_vIndexUpright].Y,
                                                                              _scd.UprightTriangle[_scd.UprightTriangle.Count - 1].Vertices[_vIndexUpright].Z);

            latestLinkLengthChange = _linkOrShimLine[_linkOrShimLine.Count - 1].Length() - _linkOrShimLine[0].Length();
            
            _linkOrShimVector[_linkOrShimVector.Count - 1] = new Vector3D(new Point3D(_linkOrShimLine[_linkOrShimLine.Count - 1].StartPoint.X, _linkOrShimLine[_linkOrShimLine.Count - 1].StartPoint.Y, _linkOrShimLine[_linkOrShimLine.Count - 1].StartPoint.Z),
                                                                          new Point3D(_linkOrShimLine[_linkOrShimLine.Count - 1].EndPoint.X, _linkOrShimLine[_linkOrShimLine.Count - 1].EndPoint.Y, _linkOrShimLine[_linkOrShimLine.Count - 1].EndPoint.Z));
            return latestLinkLengthChange;

        }

        /// <summary>
        /// Method to decide whether the Top or Bottom Wishbone/Camber Mount will be used to make the adjustments
        /// </summary>
        /// <param name="adjustmentTools"></param>
        public void DecideAdjustmentTools(AdjustmentTools adjustmentTools)
        {

        }

    }

    /// <summary>
    /// Enumeration to represent the options which the user has to make the Setup Change 
    /// </summary>
    public enum AdjustmentTools
    {
        TopFrontArm = 1,
        TopRearArm = 2,
        BottomFrontArm = 3,
        BottomRearArm = 4,
        TopCamberMount = 5,
        BottomCamberMount = 6,
        ToeLinkLength = 7,
        ToeLinkInboardPoint = 8,
        ToeShims = 9,
        SpringPreloadValue = 10,
        PushrodLength = 11,
        DamperEyetoSpringPerch = 12,
        DirectValue = 13,
        Dummy = 14
    };

}

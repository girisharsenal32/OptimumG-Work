using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using devDept.Geometry;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// Class which holds all Vector, Point and Line components of the Wheel Assembly. This class is used to access the wheel assembly components and manipulate them using transformations to make the setup changes requested by the user
    /// </summary>
    public class SetupChangeDatabase
    {
        ///<summary>Contact Patch point</summary>
        public ChildPoint_VariablePoint ContactPatch;

        ///<summary>Vector of the Spindle End Coordinates so that I can use the Mathnet.Euclidean functions to calcualate its angles with respect to space</summary>
        public Custom3DGeometry WheelSpindle;

        /// <summary>
        /// Upright Ball Joint
        /// </summary>
        public Point3D LBJ, UBJ, ToeLinkUpright;

        ///<summary>
        ///Steering axis vector for all the above points to be rotated about
        ///</summary>
        public Custom3DGeometry SteeringAxis;

        /// <summary>
        /// Vector Joining the Toe Link Pickup Point on the Upright to the Lower Ball Joint. 
        /// It is about this vector that the Wheel Assembly rotates when changing the Camber or Caster or KPI (When Setup is done using the bottom part of the Wheel Assembly such as Lower Wishbone or LBJ Camber adjustmnent shims)
        /// </summary>
        public Custom3DGeometry LBJToToeLink;

        /// <summary>
        /// Vector Joining the Toe Link Pickup Point on the Upright to the Upper Ball Joint. 
        /// It is about this vector that the Wheel Assembly rotates when changing the Camber or Caster or KPI (When Setup is done using the top part of the Wheel Assembly such as upper Wishbone or UBJ Camber adjustmnent shims)
        /// </summary>
        public Custom3DGeometry UBJToToeLink;

        /// <summary>
        /// Axis along the Z axis and passing through the wheel centre
        /// </summary>
        public StandardAxesLines WheelCentreAxis;

        /// <summary>
        /// Triangle joining the <see cref="UBJ"/> [0] <see cref="LBJ"/> [1] and <see cref="ToeLinkUpright"/> [2] and which represents the Upright
        /// 
        /// </summary>
        public List<Triangle> UprightTriangle = new List<Triangle>();

        /// <summary>
        /// Object of the <see cref="AdjustmentOptions"/> Class which has many options which the user can choose from to make the adjustments to camber,toe,caster,kpi etc
        /// </summary>
        public AdjustmentOptions AdjOptions;

        /// <summary>
        /// Dictionary of the Setup Change Ouptuts
        /// </summary>
        public Dictionary<string, double> SetupChangeOPDictionary = new Dictionary<string, double>();

        /// <summary>
        /// Constructor. Must be called only once because everytime constructor is called, the Dictionary which holds the outputs is reset
        /// </summary>
        public SetupChangeDatabase()
        {
            InitializeSetupChangeDictionary();
        }

        /// <summary>
        /// Method to initialize the Dictionary of Setup Change variables
        /// </summary>
        private void InitializeSetupChangeDictionary()
        {
            ///<summary>Initializing the Setup Change variables to 0</summary>
            SetupChangeOPDictionary.Add("KPI", 0);
            SetupChangeOPDictionary.Add("Camber", 0);
            SetupChangeOPDictionary.Add("Caster", 0);
            SetupChangeOPDictionary.Add("Toe", 0);
            SetupChangeOPDictionary.Add("RideHeight", 0); 
        }

        /// <summary>
        /// Method to initialize all the Axes involved in the wheel assembly.Called only once during 1st time initialization which happenes using <see cref="SetupChangeDatabase()"/> construction
        /// </summary>
        /// <param name="scm">Object of the SuspensionCoordinateMaster Class</param>
        public void InitializePointsAndVectors(SetupChange_CornerVariables sccv, SuspensionCoordinatesMaster scm, Dictionary<string, AdjustmentTools> adjToolDictionary)
        {
            ///<summary>Creating a vector out the Spindle End Coordinates so that I can use the Mathnet.Euclidean functions to calcualate its angles with respect to space</summary>
            WheelSpindle = new Custom3DGeometry(new Point3D(scm.K1x, scm.K1y, scm.K1z), new Point3D(scm.L1x, scm.L1y, scm.L1z));

            ///<summary>Creating the contact patch point</summary>
            ContactPatch = new ChildPoint_VariablePoint(new Point3D(scm.W1x, scm.W1y, scm.W1z));

            ///<summary>Creatng the Ball Joints</summary>
            LBJ = new Point3D(scm.E1x, scm.E1y, scm.E1z);
            UBJ = new Point3D(scm.F1x, scm.F1y, scm.F1z);
            ToeLinkUpright = new Point3D(scm.M1x, scm.M1y, scm.M1z);

            ///<summary>Initializing axes passing through the wheel centre. These will be used to initialize other axes</summary>
            WheelCentreAxis = new StandardAxesLines(new Point3D(scm.K1x, scm.K1y, scm.K1z), new Point3D(scm.K1x, scm.K1y, scm.K1z + 100),
                                                    new Point3D(scm.K1x, scm.K1y, scm.K1z), new Point3D(scm.K1x + 100, scm.K1y, scm.K1z),
                                                    new Point3D(scm.K1x, scm.K1y, scm.K1z), new Point3D(scm.K1x, scm.K1y + 100, scm.K1z));

            ///<summary>
            ///Initializing the SteeringAxis Vector
            ///</summary>
            ///<remarks>Initialization of the Steering Axis</remarks>
            SteeringAxis = new Custom3DGeometry(LBJ, UBJ);
            InitializeAuxillaries(0);

            UprightTriangle.Add(new Triangle(/*new Point3D(UBJ.X, UBJ.Y, UBJ.Z),*/UBJ, /*new Point3D(LBJ.X, LBJ.Y, LBJ.Z)*/ LBJ,/* new Point3D(ToeLinkUpright.X, ToeLinkUpright.Y, ToeLinkUpright.Z)*/ ToeLinkUpright));

            ///<summary>Initializing the <see cref="LBJToToeLink"/> Vector</summary>
            LBJToToeLink = new Custom3DGeometry(LBJ, ToeLinkUpright);
            ///<summary>Initializing the <see cref="UBJToToeLink"/> Vector</summary>
            UBJToToeLink = new Custom3DGeometry(UBJ, ToeLinkUpright);

            ///<summary></summary>
            AdjOptions = new AdjustmentOptions(scm, this, adjToolDictionary, sccv);

        }


        #region ---DELETE---
        public void AddLineAndPoint(Line prevWheelSpindle, Point3D preContactPatch, Line prevSteeringAxis)
        {

            WheelSpindle.Line.DeltaLine.Add(prevWheelSpindle);


            ContactPatch.DeltaPoint.Add(preContactPatch);


            SteeringAxis.Line.DeltaLine.Add(prevSteeringAxis);

        }

        #endregion

        Block WheelAssembly = new Block("Wheel Assembly");
        private void AddToBlock()
        {
            WheelAssembly.Entities.Add(WheelSpindle.Line.DeltaLine[WheelSpindle.Line.DeltaLine.Count - 1]);
            WheelAssembly.Entities.Add(SteeringAxis.Line.DeltaLine[SteeringAxis.Line.DeltaLine.Count - 1]);
            WheelAssembly.Entities.Add(UprightTriangle[UprightTriangle.Count - 1]);

        }

        /// <summary>
        /// Method to initialize the steering axis and ALL of its components
        /// </summary>
        /// <param name="i">Index of the <see cref="ChildLine_VariableLine.DeltaLine"/></param>
        public void InitializeAuxillaries(int i)
        {
            ///<remarks>Calculating the Angles that the Steering Axis Makes with the UNROTATED AXIS constructed above. The angles in Front and Side view are calculated</remarks>
            Angle SteeringAxisFV = AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SteeringAxis.ViewLines.FrontView.DeltaLine[i]), Custom3DGeometry.GetMathNetVector3D(WheelCentreAxis.Vertical), Custom3DGeometry.GetMathNetVector3D(WheelCentreAxis.Longitudinal));
            Angle SteeringAxisSV = AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SteeringAxis.ViewLines.SideView.DeltaLine[i]), Custom3DGeometry.GetMathNetVector3D(WheelCentreAxis.Vertical),Custom3DGeometry.GetMathNetVector3D(WheelCentreAxis.Lateral));
            Angle SteeringAxisTV = AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(SteeringAxis.ViewLines.TopView.DeltaLine[i]), Custom3DGeometry.GetMathNetVector3D(WheelCentreAxis.Lateral), Custom3DGeometry.GetMathNetVector3D(WheelCentreAxis.Vertical));

            ///<remarks>Rotating the AXIS which is along Z to make it perpendicular to the Steering Axis</remarks>
            SteeringAxisPerpendicularOperations(SteeringAxisFV, SteeringAxisSV, SteeringAxisTV, SteeringAxis.PerpAlongX.Mid, SteeringAxis.PerpAlongZ.Mid);
            SteeringAxisPerpendicularOperations(SteeringAxisFV, SteeringAxisSV, SteeringAxisTV, SteeringAxis.PerpAlongX.Fore, SteeringAxis.PerpAlongZ.Fore);
            SteeringAxisPerpendicularOperations(SteeringAxisFV, SteeringAxisSV, SteeringAxisTV, SteeringAxis.PerpAlongX.Aft, SteeringAxis.PerpAlongZ.Aft);

            ///<summary>DeltaLine[i]ialize the Fore and Aft components of the Axis which is along Z and perp to the Steering axis</summary>
            SteeringAxis.PerpAlongZ.Fore.Translate(new Vector3D(SteeringAxis.Line.DeltaLine[i].MidPoint, SteeringAxis.Line.DeltaLine[i].EndPoint));
            SteeringAxis.PerpAlongZ.Aft.Translate(new Vector3D(SteeringAxis.Line.DeltaLine[i].MidPoint,SteeringAxis.Line.DeltaLine[i].StartPoint));
            
            ///<summary>DeltaLine ialize the Fore and Aft components of the Axis which is along X  and perp to the Steering axis</summary>
            SteeringAxis.PerpAlongX.Fore.Translate(new Vector3D(SteeringAxis.Line.DeltaLine[i].MidPoint, SteeringAxis.Line.DeltaLine[i].EndPoint));
            SteeringAxis.PerpAlongX.Aft.Translate(new Vector3D(SteeringAxis.Line.DeltaLine[i].MidPoint, SteeringAxis.Line.DeltaLine[i].StartPoint));
        }

        /// <summary>
        /// Method to make the Steering Axis Perpendiculars along X and Z ACTUALLY Perpendicular to the Steering Axis by rotating them by the right angles. This method is needed because there exist 3 components for each Perpendicular <see cref="ChildLine_LinePair"/>
        /// </summary>
        /// <param name="SteeringAxisFV"></param>
        /// <param name="SteeringAxisSV"></param>
        /// <param name="SteeringAxisTV"></param>
        /// <param name="_perpAlongX"></param>
        /// <param name="_perpAlongZ"></param>
        private void SteeringAxisPerpendicularOperations(Angle SteeringAxisFV, Angle SteeringAxisSV, Angle SteeringAxisTV, Line _perpAlongX, Line _perpAlongZ)
        {
            ///<remarks>Rotating the AXIS which is along Z to make it perpendicular to the Steering Axis</remarks>
            _perpAlongZ.Rotate(-SteeringAxisSV.Radians, new Vector3D(WheelCentreAxis.Lateral.StartPoint, WheelCentreAxis.Lateral.EndPoint), SteeringAxis.PerpAlongZ.Mid.StartPoint);
            _perpAlongZ.Rotate(-SteeringAxisFV.Radians, new Vector3D(WheelCentreAxis.Longitudinal.StartPoint, WheelCentreAxis.Longitudinal.EndPoint), SteeringAxis.PerpAlongZ.Mid.StartPoint);

            ///<remarks>Rotating the AXIS which is along X to make it perpendicular to the Steering Axis</remarks>
            _perpAlongX.Rotate(-SteeringAxisFV.Radians, new Vector3D(WheelCentreAxis.Longitudinal.StartPoint, WheelCentreAxis.Longitudinal.EndPoint), SteeringAxis.PerpAlongX.Mid.StartPoint);
            _perpAlongX.Rotate(-SteeringAxisTV.Radians, new Vector3D(WheelCentreAxis.Vertical.StartPoint, WheelCentreAxis.Vertical.EndPoint), SteeringAxis.PerpAlongX.Mid.StartPoint);
        }


        /// <summary>
        /// Method to find the angle between 2 vectors in any required View or in 3D. 
        /// Make the Vector a 2D vector by setting any of the axis to 0 to get the FV, TV or SV 
        /// </summary>
        /// <example>For top view the 2D vector will consist of only Z and Y coordinates</example>
        /// <param name="_vAngleOfThis">The 3D vector converted to 2D whose angle is to be found with respect to a reference vector</param>
        /// <param name="_vAngleWithThis">The 3D vector converted to 2D which is the reference vector</param>
        /// <param name="_vNormalToViewPlane">The 3D vector about which the <paramref name="_vAngleOfThis"/> is rotated to get the angle about <paramref name="_vAngleWithThis"/> </param>
        /// <returns>The <see cref="Angle"/> between the 1st 2 vectors passed</returns>
        public static Angle AngleInRequiredView(MathNet.Spatial.Euclidean.Vector3D _vAngleOfThis, MathNet.Spatial.Euclidean.Vector3D _vAngleWithThis, MathNet.Spatial.Euclidean.Vector3D _vNormalToViewPlane)
        {
            ///<summary>Temp Angle declared</summary>
            Angle angle;

            ///<summary>Using the <see cref="MathNet.Spatial.Euclidean.Vector3D.SignedAngleTo(MathNet.Spatial.Euclidean.Vector3D, MathNet.Spatial.Euclidean.UnitVector3D)"/> method to calculate the angle between the 2 vectors</summary>
            //angle = _vAngleOfThis.SignedAngleTo(_vAngleWithThis, _vNormalToViewPlane.Normalize());
            angle = _vAngleWithThis.SignedAngleTo(_vAngleOfThis, _vNormalToViewPlane.Normalize());
            
            return angle;
        }

        /// <summary>
        /// EXCEPT the contact patch, all the Points and Lines of the Wheel Assembly are translated using this method.
        /// This method is called when the <see cref="ContactPatch"/>'s Y Coordinate changes. 
        /// When the Y Coordinate changes it means the Wheel Assemly also changes and hence this method must be called
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        public void TranslateWheelAssembly(double _x, double _y, double _z, int _i)
        {
            WheelSpindle.Line.DeltaLine[_i].Translate(_x, _y, _z);

            SteeringAxis.Line.DeltaLine[SteeringAxis.Line.DeltaLine.Count - 1].Translate(_x, _y, _z);

            UprightTriangle[UprightTriangle.Count - 1].Translate(_x, _y, _z); 

        }



        public void GetUprightTriangleRotation()
        {

        }


    }

}

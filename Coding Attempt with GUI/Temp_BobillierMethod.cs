using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using MathNet.Spatial.Units;


namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This Class creates the Bobillier Line for Minimum Bump Ster using pure Geometry. 
    /// The Class uses <see cref="devDept.Eyeshot"/> for the operations. However, <see cref="MathNet.Spatial"/> can be used instead.
    /// </summary>
    public partial class Temp_BobillierMethod : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Temp_BobillierMethod()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <see cref="SuspensionCoordinatesMaster"/> object used to perform the Bobillier operations
        /// </summary>
        public SuspensionCoordinatesMaster SCM { get; set; }
        /// <summary>
        /// Object of the <see cref="VehicleCorner"/> to determine the corner that is calling this method
        /// </summary>
        public VehicleCorner Corner { get; set; }
        /// <summary>
        /// Object of the <see cref="SuspensionConfiguration"/> enum to  determine the Configuration of the Suspension calling this method. 
        /// </summary>
        public SuspensionConfiguration Config { get; set; }

        public Vehicle Vehicle { get; set; }


        /// <summary>
        /// <para>This variable is used to determine the King Pin Axis in case of a 5 link Suspension</para> 
        /// <para>Used to translate the Point <see cref="SuspensionCoordinatesMaster.M1x"/> (Steering Link Upright)
        /// to simulate the a Steering Input needed to generate the King Pin Axis of a 5Link Suspension</para>
        /// </summary>
        double RackDisplacement = 0.5;

        /// <summary>
        /// Method to obtain the Suspension Coordinates of the corner for which wre want to plot the Bobillier Line
        /// </summary>
        /// <param name="_scm"></param>
        public void AssignLocalSuspensionObject(Vehicle _vehicle, SuspensionCoordinatesMaster _scm, int _identifier)
        {
            if (_scm != null)
            {
                SCM = _scm;
            }
            if (_vehicle != null)
            {
                Vehicle = _vehicle;
            }

            Corner = (VehicleCorner)_identifier;
        }

        /// <summary>
        /// Method to execute all the geometry operations required to construct the Bobillier Line in a sequential manner
        /// Calling this method creates the Bobillier's Line for the corner which calls it. 
        /// </summary>
        public void ConstructBobilierLine()
        {
            AssignBasePoints();
            ConstructWishbonePlanes();
            ConstructInstantCentreLine();
            ConstructBisector1();
            ConstructSteeringAxisINFLine();
            ConstructPlaneRedPlane1AndPointA();
            ConstructPlaneREDs();
            ConstructPlaneSTEERING();
            ConstructBisector2();
            ConstructPlaneBAndPointB();
            ConstructBobillierLine();
            CheckForBumpSteer();
            OptimizePoints();

            for (int i = 0; i < cad1.viewportLayout1.Entities.Count; i++)
            {
                cad1.viewportLayout1.Entities[i].ColorMethod = colorMethodType.byEntity;
            }

        }

        

        /// <summary>
        /// Method to assign the Configuration of the Suspension
        /// </summary>
        /// <param name="_config"></param>
        private void AssignSuspensionConfiguration(SuspensionConfiguration _config)
        {
            Config = _config;
        }

        /// <summary>
        /// <para>Rear Upper Ball Joint</para>
        /// <para>Front and Rear are same for Double Wishbone</para>
        /// </summary>
        Joint UBJ_Rear;
        /// <summary>
        /// <para>Front Upper Ball Joint</para>
        /// <para>Front and Rear are same  for Double Wishbone</para>
        /// </summary>
        Joint UBJ_Front;
        /// <summary>
        /// <para>Rear Lower Ball Joint</para>
        /// <para>Front and Rear are same for Double Wishbone</para>
        /// </summary>
        Joint LBJ_Rear;
        /// <summary>
        /// <para>Rear Lower Ball Joint</para>
        /// <para>Front and Rear are same for Double Wishbone</para>
        /// </summary>
        Joint LBJ_Front;
        /// <summary>
        /// Top Front Inboard Pick-Up Point
        /// </summary>
        Joint TopFrontInboard;
        /// <summary>
        /// Toe Rear Inboard Pick-Up Point 
        /// </summary>
        Joint TopRearInboard;
        /// <summary>
        /// Bottom Front Inboard Pick-Up Point
        /// </summary>
        Joint BottomFrontInboard;
        /// <summary>
        /// Bottom Rear Inboard Pick-Up Point
        /// </summary>
        Joint BottomRearInboard;
        /// <summary>
        /// Toe Link Upright Attachment Point
        /// </summary>
        Joint ToeLinkupright;
        /// <summary>
        /// Toe Link Inboard Pick-Up Point
        /// </summary>
        Joint ToeLinkInboard;

        /// <summary>
        /// Method to Assign all the Joints required to constrcut the Bobillier's Line
        /// </summary>
        private void AssignBasePoints()
        {
            UBJ_Rear = new Joint(new Point3D(SCM.F1x, SCM.F1y, SCM.F1z), 5, 2);

            ///<remarks>Arbitrarily Assigning this point for now. Depending on whether a solver is created for the 5 Links this code could be changed. </remarks>
            UBJ_Front = new Joint(new Point3D(SCM.F1x, SCM.F1y, SCM.F1z + 25), 5, 2);

            LBJ_Rear = new Joint(new Point3D(SCM.E1x, SCM.E1y, SCM.E1z), 5, 2);

            ///<remarks>Arbitrarily Assigning this point for now. Depending on whether a solver is created for the 5 Links this code could be changed. </remarks>
            LBJ_Front = new Joint(new Point3D(SCM.E1x, SCM.E1y, SCM.E1z + 25), 5, 2);

            ToeLinkupright = new Joint(new Point3D(SCM.M1x, SCM.M1y, SCM.M1z), 5, 2);

            TopFrontInboard = new Joint(new Point3D(SCM.A1x, SCM.A1y, SCM.A1z), 5, 2);

            TopRearInboard = new Joint(new Point3D(SCM.B1x, SCM.B1y, SCM.B1z), 5, 2);

            BottomFrontInboard = new Joint(new Point3D(SCM.D1x, SCM.D1y, SCM.D1z), 5, 2);

            BottomRearInboard = new Joint(new Point3D(SCM.C1x, SCM.C1y, SCM.C1z), 5, 2);

            ToeLinkInboard = new Joint(new Point3D(SCM.N1x, SCM.N1y, SCM.N1z), 5, 2);

            cad1.viewportLayout1.Entities.AddRange(new Entity[] { UBJ_Rear, LBJ_Rear, ToeLinkupright, TopFrontInboard, TopRearInboard, BottomFrontInboard, BottomRearInboard });
        }

        /// <summary>
        /// <see cref="Plane"/> containing the 3 points of the Top Wishbone
        /// </summary>
        Plane TopWishbonePlane;
        /// <summary>
        /// <see cref="Plane"/> containing the 3 points of the Bottom Wishbone
        /// </summary>
        Plane BottomWishbonePlane;

        /// <summary>
        /// Method to construct the Wishbone Planes
        /// </summary>
        private void ConstructWishbonePlanes()
        {
            TopWishbonePlane = new Plane(UBJ_Rear.Position, TopFrontInboard.Position, TopRearInboard.Position);

            BottomWishbonePlane = new Plane(LBJ_Rear.Position, BottomFrontInboard.Position, BottomRearInboard.Position);
        }

        /// <summary>
        /// <see cref="Line"/> representing the Instant Axis
        /// </summary>
        Line InstantAxis;
        /// <summary>
        /// Object of the <see cref="InstntCentrePosition"/> to determine whether the instant center is inboard or outboad
        /// </summary>
        InstntCentrePosition ICPosition;

        /// <summary>
        /// Method to Construct the Instant Axis
        /// </summary>
        private void ConstructInstantCentreLine()
        {
            ///<summary>Calculating the Intersection of the Top and Bottom Wishbone Planes. The result of this calculation is the Instant Axis Line</summary>
            Plane.Intersection(TopWishbonePlane, BottomWishbonePlane, out Segment3D ICSegment);
            
            ///<summary>Creating the Instant Axis Line using the result of the Intersection calculation above</summary>
            InstantAxis = new Line(ICSegment);
            ///<summary>Extending the Line along both ends by an amount of 500 each</summary>
            CustomInfiniteLine.DrawInfiniteLine(InstantAxis, 500);
            Plane f = new Plane();
            cad1.viewportLayout1.Entities.Add(InstantAxis);
            
            ///<summary>Determining the Instant Axis is Inoard or Outboard using the Contact Patch Coordinate and the Instant Axis Mid Point</summary>
            double ICDistance = ICSegment.MidPoint.DistanceTo(new Point3D());
            if (Corner == VehicleCorner.FrontLeft || Corner == VehicleCorner.FrontRight) 
            {
                if ((SCM.W1x) - (InstantAxis.MidPoint.X) > 0)
                {
                    ICPosition = InstntCentrePosition.Inboard;
                }
                else
                {
                    ICPosition = InstntCentrePosition.Outboard;
                } 
            }
            else
            {
                if ((SCM.W1x - InstantAxis.MidPoint.X) < 0)
                {
                    ICPosition = InstntCentrePosition.Inboard;
                }
                else ICPosition = InstntCentrePosition.Outboard;
            }
        }

        /// <summary>
        /// <see cref="Line"/> representing the Bisector 1. This is the bisector of the TOP and Bottom Wishbone Planes
        /// </summary>
        Line Bisector1;

        /// <summary>
        /// Method to construct the <see cref="Bisector1"/>
        /// </summary>
        private void ConstructBisector1()
        {
            ///<summary>Creating 2 temporay Lines to represent the Top and Bottom Wishbones. Need this to find the angle between the Top and Bottom Wishbone Planes</summary>
            Line LineForAngle_TopWishbone = new Line(InstantAxis.MidPoint.Clone() as Point3D, UBJ_Rear.Position.Clone() as Point3D);
            Line LineForAngle_BottomWishbone = new Line(InstantAxis.MidPoint.Clone() as Point3D, LBJ_Rear.Position.Clone() as Point3D);

            ///<summary>Calculating the Line between the Top and Bottom Wishbone Planes by calculating the angle between the Lines created above</summary>
            Angle angle = (SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.MidPoint, UBJ_Rear.Position)), Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.MidPoint, LBJ_Rear.Position)),
                                                                   Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.StartPoint, InstantAxis.EndPoint))));

            ///<summary>Drawing a line alonge the LBJ Line </summary>
            Bisector1 = new Line(InstantAxis.MidPoint.Clone() as Point3D, LBJ_Rear.Position.Clone() as Point3D);
            ///<summary>Rotating the Bisector Line by twice the amount of half the angle of the angle between the Top and Bottom Wishbone Planes</summary>
            Bisector1.Rotate(angle.Radians / 2, new Vector3D(InstantAxis.StartPoint.Clone() as Point3D, InstantAxis.EndPoint.Clone() as Point3D), InstantAxis.MidPoint);
            cad1.viewportLayout1.Entities.Add(Bisector1);
        }

        #region Delete IF not needed
        //private Angle AssignAngleSignBasedOnVectorDirection(Angle _angleRotation, MathNet.Spatial.Euclidean.Vector3D _rotationAxisVector)
        //{
        //    if (_rotationAxisVector.Z < 0)
        //    {
        //        return _angleRotation;
        //    }
        //    else
        //    {
        //        return -_angleRotation;
        //    }
        //} 
        #endregion

        /// <summary>
        /// <see cref="Line"/> representing the Steering Axis
        /// </summary>
        Line SteeringAxis;

        /// <summary>
        /// Method to construct the <see cref="SteeringAxis"/>
        /// </summary>
        private void ConstructSteeringAxisINFLine()
        {
            SteeringAxis = new Line(UBJ_Rear.Position.Clone() as Point3D, LBJ_Rear.Position.Clone() as Point3D);
            cad1.viewportLayout1.Entities.Add(SteeringAxis);
        }

        /// <summary>
        /// <see cref="Plane"/> representing the Plane 1
        /// </summary>
        Plane Plane1;
        /// <summary>
        /// Point A
        /// </summary>
        Point3D PointA;
        /// <summary>
        /// Object of the <see cref="PositionOfPointA"/> Enum which determines whether the PointA is above or below the Wishbones
        /// </summary>
        PositionOfPointA PointAPos;
        
        /// <summary>
        /// Method to constrcut the Plane 1 and Point A
        /// </summary>
        private void ConstructPlaneRedPlane1AndPointA()
        {
            ///Constructing Plane1 from Bottom Wishbone Points and the Top Front Pick-Up Point
            ///<remarks>Right now selection of these 3 points is random. Need to figure out how to select them properly</remarks>
            Plane1 = new Plane(BottomFrontInboard.Position.Clone() as Point3D, BottomRearInboard.Position.Clone() as Point3D, TopFrontInboard.Position.Clone() as Point3D);
            ///<summary>Creating a <see cref="Segment3D"/> using the <see cref="SteeringAxis"/> so that I can use the <see cref="Segment3D.IntersectWith(Plane, bool, out Point3D)"/> to compute the intersection of the Plane1 and Steering Axis</summary>
            Segment3D SteeringAxisSegment = new Segment3D(SteeringAxis.StartPoint.Clone() as Point3D, SteeringAxis.EndPoint.Clone() as Point3D);
            ///<summary>Computing the Intersection of the <see cref="SteeringAxis"/> with the <see cref="Plane1"/> which results in <see cref="PointA"/></summary>
            SteeringAxisSegment.IntersectWith(Plane1, true, out PointA);

            ///<summary>Determining the whether the <see cref="PointA"/> is above or below the Wishbones</summary>
            if (PointA.Y > UBJ_Rear.Position.Y && PointA.Y > TopFrontInboard.Position.Y && PointA.Y > TopRearInboard.Position.Y) 
            {
                PointAPos = PositionOfPointA.AboveWishbones;
            }
            else if (PointA.Y < LBJ_Rear.Position.Y && PointA.Y < BottomFrontInboard.Position.Y && PointA.Y < BottomRearInboard.Position.Y)
            {
                PointAPos = PositionOfPointA.BelowWishbones;
            }

            ///<summary>Adding them to the VIewport as a Planar Entity</summary>
            PlanarEntity Plane1_Ent = new PlanarEntity(Plane1);
            Joint PointA_Ent = new Joint(PointA, 10, 2);
            PointA_Ent.Color = Color.AntiqueWhite;
            cad1.viewportLayout1.Entities.AddRange(new Entity[] { Plane1_Ent, PointA_Ent });
        }

        /// <summary>
        /// The Plane formed using the <see cref="InstantAxis"/> and the <see cref="PointA"/>
        /// </summary>
        Plane PlaneRED;
        /// <summary>
        /// The equiangled counterpart of <see cref="PlaneRED"/>
        /// </summary>
        Plane PlaneRED2;
        /// <summary>
        /// Angle which the <see cref="PlaneRED"/> makes with the <see cref="Bisector1"/>
        /// </summary>
        Angle Angle_Bis1_PlaneRED;
        /// <summary>
        /// The Lines representing the <see cref="PlaneRED"/> and the <see cref="PlaneRED2"/>. Needed to compute the angles.
        /// </summary>
        Line LineForAngle_PlaneRED, LineForAngle_PlaneRed2;

        /// <summary>
        /// Method to construct the <see cref="PlaneRED"/> and the <see cref="PlaneRED2"/>
        /// </summary>
        private void ConstructPlaneREDs()
        {
            ///<summary>Constructing the <see cref="PlaneRED"/> using the <see cref="InstantAxis"/> and the <see cref="PointA"/></summary>
            PlaneRED = new Plane(InstantAxis.StartPoint.Clone() as Point3D, InstantAxis.EndPoint.Clone() as Point3D, PointA.Clone() as Point3D);
            ///<summary>Constructing the Line of the <see cref=PlaneRED"/> needed for the angle</summary>
            LineForAngle_PlaneRED = new Line(InstantAxis.MidPoint.Clone() as Point3D, PointA.Clone() as Point3D);

            ///<summary>Computing the <see cref="Angle"/> between the <see cref="PlaneRED"/> and the <see cref="Bisector1"/></summary>
            Angle_Bis1_PlaneRED = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneRED), Custom3DGeometry.GetMathNetVector3D(Bisector1),
                                                                           Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.StartPoint, InstantAxis.EndPoint)));

            ///<summary>Cloning <see cref="PlaneRED"/> onto <see cref="PlaneRED2"/> and creating its line</summary>
            PlaneRED2 = PlaneRED.Clone() as Plane;
            LineForAngle_PlaneRed2 = LineForAngle_PlaneRED.Clone() as Line;

            ///<summary>Rotating the <see cref="PlaneRED2"/> and its <see cref="LineForAngle_PlaneRed2"/> by an amount equal to twice the Angle b/w <see cref="Bisector1"/> and <see cref="PlaneRED"/></summary>
            PlaneRED2.Rotate(-Angle_Bis1_PlaneRED.Radians * 2, new Vector3D(InstantAxis.StartPoint.Clone() as Point3D, InstantAxis.EndPoint.Clone() as Point3D),InstantAxis.MidPoint);
            LineForAngle_PlaneRed2.Rotate(-Angle_Bis1_PlaneRED.Radians * 2, new Vector3D(InstantAxis.StartPoint.Clone() as Point3D, InstantAxis.EndPoint.Clone() as Point3D),InstantAxis.StartPoint);

            PlanarEntity PlaneRED_Ent = new PlanarEntity(PlaneRED);
            PlanarEntity PlaneRED2_Ent = new PlanarEntity(PlaneRED2);
            cad1.viewportLayout1.Entities.AddRange(new Entity[] { PlaneRED_Ent, PlaneRED2_Ent });
        }

        /// <summary>
        /// <see cref="Plane"/> representing the Steering
        /// </summary>
        Plane PlaneSTEERING;
        /// <summary>
        /// M
        /// </summary>
        ToeLinkUprightPosition ToeLinkUprightPos;
        /// <summary>
        /// Object of the <see cref="BobillierPair"/> to decide which Wishbone Plane will be coupled with the <see cref="PlaneSTEERING"/>
        /// </summary>
        BobillierPair LinkagePair;

        /// <summary>
        /// Mthod to construct the <see cref="PlaneSTEERING"/>
        /// </summary>
        private void ConstructPlaneSTEERING()
        {
            PlaneSTEERING = new Plane(InstantAxis.StartPoint.Clone() as Point3D, InstantAxis.EndPoint.Clone() as Point3D, ToeLinkupright.Position.Clone() as Point3D);

            if (ToeLinkupright.Position.Y > Bisector1.MidPoint.Y && ToeLinkupright.Position.Y > Bisector1.EndPoint.Y) 
            {
                ToeLinkUprightPos = ToeLinkUprightPosition.AboveBisector1;
                LinkagePair = BobillierPair.BottomWishboneAndSteering;                
            }
            else if (ToeLinkupright.Position.Y < Bisector1.MidPoint.Y && ToeLinkupright.Position.Y < Bisector1.EndPoint.Y)
            {
                ToeLinkUprightPos = ToeLinkUprightPosition.BelowBisector1;
                LinkagePair = BobillierPair.TopWishboneAndSteering;
            }

            PlanarEntity PlaneSTEERING_Ent = new PlanarEntity(PlaneSTEERING);
            cad1.viewportLayout1.Entities.Add(PlaneSTEERING_Ent);

        }

        /// <summary>
        /// <see cref="Line"/ representing the Bisector2
        /// </summary>
        Line Bisector2;

        /// <summary>
        /// Method to construct the Bisector 2
        /// </summary>
        private void ConstructBisector2()
        {
            ///<summary>Drawing the Line of the <see cref="PlaneSTEERING"/></summary>
            Line LineForAngle_PlaneSTEERING = new Line(InstantAxis.MidPoint.Clone() as Point3D, ToeLinkupright.Position.Clone() as Point3D);

            ///<summary>Drawing the Line of the Wishbone Plane depending upon the <see cref="LinkagePair"/></summary>
            Line LineForAngle_PlaneWISHBONE = new Line(0, 0, 0, 1, 1, 1);
            if (LinkagePair == BobillierPair.BottomWishboneAndSteering)
            {
                LineForAngle_PlaneWISHBONE = new Line(InstantAxis.MidPoint.Clone() as Point3D, LBJ_Rear.Position.Clone() as Point3D);
            }
            else if (LinkagePair == BobillierPair.TopWishboneAndSteering)
            {
                LineForAngle_PlaneWISHBONE = new Line(InstantAxis.MidPoint.Clone() as Point3D, UBJ_Rear.Position.Clone() as Point3D);
            }

            ///<summary>Finding the Angle of the Steering Plane with the selected Wishbone Plane </summary>
            Angle Angle_PlaneSTEERING_PlaneWISHBONE = SetupChangeDatabase.AngleInRequiredView(_vAngleOfThis: Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneSTEERING),
                                                                                              _vAngleWithThis: Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneWISHBONE),
                                                                                              _vNormalToViewPlane: Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.StartPoint, InstantAxis.EndPoint)));

            //Angle_PlaneSTEERING_PlaneWISHBONE = AssignAngleSignBasedOnVectorDirection(Angle_PlaneSTEERING_PlaneWISHBONE, Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));
            ///<summary>Creating a Bisector Line</summary>
            Bisector2 = LineForAngle_PlaneWISHBONE.Clone() as Line;
            Bisector2.Rotate(angleInRadians: Angle_PlaneSTEERING_PlaneWISHBONE.Radians / 2, axis: new Vector3D(InstantAxis.StartPoint.Clone() as Point3D, InstantAxis.EndPoint.Clone() as Point3D),center: InstantAxis.MidPoint);
            cad1.viewportLayout1.Entities.Add(Bisector2);
        }

        /// <summary>
        /// PointB
        /// </summary>
        Point3D PointB = new Point3D();
        /// <summary>
        /// <see cref="Angle"/> between the Bisector 2 and wither of the PlaneREDs. Which ever angle is smaller
        /// </summary>
        Angle Angle_Bis2_Final_PlaneREDs;
        /// <summary>
        /// <see cref="Plane"/> required for the construction of <see cref="PlaneB"/>
        /// </summary>
        Plane PlaneToConstructPlaneB;
        /// <summary>
        /// <see cref="Plane"/>B
        /// </summary>
        Plane PlaneB;
        /// <summary>
        /// <see cref="Segment3D"/> representing the PointB. Used to compute the Intersection of the PointB
        /// </summary>
        Segment3D SegmentForPointB;

        /// <summary>
        /// Method to Construct the <see cref="PlaneB"/> and <see cref="PointB"/>
        /// </summary>
        private void ConstructPlaneBAndPointB()
        {
            ///<summary>Calculating the angle between the <see cref="PlaneRED"/> and the <see cref="Bisector2"/></summary>
            Angle Angle_Bis2_PlaneRED = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneRED),
                                                                                Custom3DGeometry.GetMathNetVector3D(Bisector2),
                                                                                Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.StartPoint, InstantAxis.EndPoint)));

            ///<summary>Calculating the angle between the <see cref="PlaneRED2"/> and the <see cref="Bisector2"/></summary>
            Angle Angle_Bis2_PlaneRED2 = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneRed2),
                                                                                 Custom3DGeometry.GetMathNetVector3D(Bisector2),
                                                                                 Custom3DGeometry.GetMathNetVector3D(new Line(InstantAxis.StartPoint, InstantAxis.EndPoint)));

            ///<summary>Depending on which of the above 2 angles is smaller the final angle called <see cref="Angle_Bis2_Final_PlaneREDs"/> is assigned</summary>
            if (Math.Abs(Angle_Bis2_PlaneRED.Degrees) < Math.Abs(Angle_Bis1_PlaneRED.Degrees)) 
            {
                Angle_Bis2_Final_PlaneREDs = Angle_Bis2_PlaneRED;
                PlaneToConstructPlaneB = PlaneRED.Clone() as Plane; 
            }
            else
            {
                Angle_Bis2_Final_PlaneREDs = Angle_Bis2_PlaneRED2;
                PlaneToConstructPlaneB = PlaneRED2.Clone() as Plane;
            }

            ///<summary>Rotating the <see cref="PlaneToConstructPlaneB"/> by an amount equal to twice the final angle calculated above</summary>
            PlaneToConstructPlaneB.Rotate(-Angle_Bis2_Final_PlaneREDs.Radians * 2, new Vector3D(InstantAxis.StartPoint, InstantAxis.EndPoint), InstantAxis.MidPoint);

            ///<summary>Depending upon the Position of <see cref="PointA"/> the <see cref="SegmentForPointB"/> is decided</summary>
            if (PointAPos == PositionOfPointA.AboveWishbones)
            {
                SegmentForPointB = new Segment3D(LBJ_Rear.Position, ToeLinkupright.Position);

            }
            else if (PointAPos == PositionOfPointA.BelowWishbones)
            {
                SegmentForPointB = new Segment3D(UBJ_Rear.Position, ToeLinkupright.Position);

            }

            ///<summary>Computing the Intersection between the <see cref="SegmentForPointB"/> and the <see cref="PlaneToConstructPlaneB"/> which results in the <see cref="PointB"/></summary>
            SegmentForPointB.IntersectWith(PlaneToConstructPlaneB, true, out PointB);

            ///<summary>Depending upon whether the <<see cref="PointA"/> is above or below the Wishbones, the Points to draw <see cref="PlaneB"/> (apart from <see cref="PointB"/>!!!) are chosen</summary>
            if (PointAPos == PositionOfPointA.AboveWishbones)
            {
                PlaneB = new Plane(BottomRearInboard.Position, BottomFrontInboard.Position, PointB);
            }
            else if (PointAPos == PositionOfPointA.BelowWishbones)
            {
                PlaneB = new Plane(TopRearInboard.Position, TopFrontInboard.Position, PointB);
            }

            PlanarEntity PlaneB_Ent = new PlanarEntity(PlaneB);
            Joint PointB_Ent = new Joint(PointB, 5, 2);
            PointB_Ent.Color = Color.AntiqueWhite;
            cad1.viewportLayout1.Entities.AddRange(new Entity[] { PlaneB_Ent, PointB_Ent });
        }

        /// <summary>
        /// The FINAL <see cref="Line"/> representing the Bobilier's Line for Minimum Bump Steer
        /// </summary>
        Line BobillierLine;

        /// <summary>
        /// Method to construct the Bobillier Line
        /// </summary>
        private void ConstructBobillierLine()
        {
            ///<summary>Computing the intersection of the <see cref="PlaneSTEERING"/> and the <see cref="PlaneB"/> which results in the <see cref="BobillierLine"/></summary>
            Plane.Intersection(PlaneB, PlaneSTEERING, out Segment3D BobillierSegment);
            BobillierLine = new Line(BobillierSegment);
            cad1.viewportLayout1.SetView(viewType.Isometric);
            ///<summary>Extending the <see cref="BobillierLine"/> by 500mm on each side</summary>
            CustomInfiniteLine.DrawInfiniteLine(BobillierLine, 500);

            Bar BobillierLine_Ent = new Bar(BobillierLine.StartPoint, BobillierLine.EndPoint, 4.5, 8);
            BobillierLine_Ent.Color = Color.AliceBlue;
            cad1.viewportLayout1.Entities.Add(BobillierLine_Ent);

            BobillierLine.GetPointsByLength(1);

        }

        /// <summary>
        /// Checking the <see cref="ToeLinkInboard"/> is close to the Bump Steer Line. This would be an indication of how much Bump Steer is there
        /// </summary>
        private void CheckForBumpSteer()
        {
            Segment3D tempBobillierSegment = new Segment3D(BobillierLine.StartPoint, BobillierLine.EndPoint);
            ToeLinkInboard.Position.DistanceTo(tempBobillierSegment);
            double bumpSteerError = ToeLinkInboard.Position.DistanceTo(tempBobillierSegment);

        }



        private void OptimizePoints()
        {
            if (Vehicle != null)
            {
                OptimizerGeneticAlgorithm Optimizer = new OptimizerGeneticAlgorithm(0.85, 0.05, 5, 200, 90);

                Optimizer.InitializeVehicleParams(Corner, Vehicle, 1, 25, -25);

                Optimizer.ConstructGeneticAlgorithm(); 
            }
            else
            {
                MessageBox.Show("HRHRRMRMHRHRHKHKKKHH");
            }
           
        }


    }

    /// <summary>
    /// Enum to determine whether the Instant Axis is Inboard or Outboard
    /// </summary>
    enum InstntCentrePosition
    {
        Inboard,
        Outboard
    }

    /// <summary>
    /// Enum to determine whether the PointA is above or below the Wishbones
    /// </summary>
    enum PositionOfPointA
    {
        AboveWishbones, 
        BelowWishbones
    }

    enum ToeLinkUprightPosition
    {
        AboveBisector1,
        BelowBisector1
    }

    /// <summary>
    /// Enum to determine which Wishbone (Top or Bottom) is going to be coupled with the Steeering Plane
    /// </summary>
    enum BobillierPair
    {
        TopWishboneAndSteering,
        BottomWishboneAndSteering
    }


}

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
    public partial class Temp_BobillierMethod : Form
    {
        public Temp_BobillierMethod()
        {
            InitializeComponent();
            
        }

        public void AssignLocalSuspensionObject(SuspensionCoordinatesMaster _scm)
        {
            if (_scm != null)
            {
                SCM = _scm;
            }
        }

        public void Execute()
        {
            AssignBasePoints();
            AssignBasePlanes();
            ConstructInstantCentreLine();
            ConstructBisector1();
            ConstructSteeringAxisINFLine();
            ConstructPlaneRedPlane1AndPointA();
            ConstructPlaneRED();
            ConstructPlaneSTEERING();
            ConstructBisector2();
            ConstructPlaneBAndPointB();
            ConstructBobillierLine();
            CheckForBumpSteer();

            for (int i = 0; i < cad1.viewportLayout1.Entities.Count; i++)
            {
                cad1.viewportLayout1.Entities[i].ColorMethod = colorMethodType.byEntity;
            }

        }

        SuspensionCoordinatesMaster SCM;

        Joint UBJ;
        Joint LBJ;
        Joint TopFrontInboard;
        Joint TopRearInboard;
        Joint BottomFrontInboard;
        Joint BottomRearInboard;
        Joint ToeLinkupright;
        Joint ToeLinkInboard;
        private void AssignBasePoints()
        {
            UBJ = new Joint(new Point3D(SCM.F1x, SCM.F1y, SCM.F1z), 5, 2);

            LBJ = new Joint(new Point3D(SCM.E1x, SCM.E1y, SCM.E1z), 5, 2);

            ToeLinkupright = new Joint(new Point3D(SCM.M1x, SCM.M1y, SCM.M1z), 5, 2);

            TopFrontInboard = new Joint(new Point3D(SCM.A1x, SCM.A1y, SCM.A1z), 5, 2);

            TopRearInboard = new Joint(new Point3D(SCM.B1x, SCM.B1y, SCM.B1z), 5, 2);

            BottomFrontInboard = new Joint(new Point3D(SCM.D1x, SCM.D1y, SCM.D1z), 5, 2);

            BottomRearInboard = new Joint(new Point3D(SCM.C1x, SCM.C1y, SCM.C1z), 5, 2);

            ToeLinkInboard = new Joint(new Point3D(SCM.N1x, SCM.N1y, SCM.N1z), 5, 2);

            cad1.viewportLayout1.Entities.AddRange(new Entity[] { UBJ, LBJ, ToeLinkupright, TopFrontInboard, TopRearInboard, BottomFrontInboard, BottomRearInboard });
        }

        Plane TopWishbonePlane;
        Plane BottomWishbonePlane;
        Transformation scalePlane = new Transformation(20);

        private void AssignBasePlanes()
        {
            TopWishbonePlane = new Plane(UBJ.Position, TopFrontInboard.Position, TopRearInboard.Position);
            BottomWishbonePlane = new Plane(LBJ.Position, BottomFrontInboard.Position, BottomRearInboard.Position);
        }

        Line ICLine;
        InstntCentrePosition ICPosition;
        private void ConstructInstantCentreLine()
        {
            Plane.Intersection(TopWishbonePlane, BottomWishbonePlane, out Segment3D ICSegment);
            Plane.Intersection(TopWishbonePlane, BottomWishbonePlane, 0, out Segment3D ICSegment2);
            
            ICLine = new Line(ICSegment);
            cad1.viewportLayout1.Entities.Add(ICLine);
            
            double halfTrack = SCM.W1x;

            double ICDistance = ICSegment.MidPoint.DistanceTo(new Point3D());

            if (halfTrack > ICDistance)
            {
                ICPosition = InstntCentrePosition.Inboard;
            }
            else
            {
                ICPosition = InstntCentrePosition.Outboard;
            }
        }

        Line Bisector1;
        private void ConstructBisector1()
        {
            Line tempLineForAngle = new Line(ICLine.MidPoint.Clone() as Point3D, UBJ.Position.Clone() as Point3D);
            Line tempBisecVector = new Line(ICLine.MidPoint.Clone() as Point3D, LBJ.Position.Clone() as Point3D);

            Angle angle = (SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.MidPoint, UBJ.Position)), Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.MidPoint, LBJ.Position)),
                                                                   Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint))));

            angle = AssignAngleSignBasedOnVectorDirection(angle, Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));

            Bisector1 = new Line(ICLine.MidPoint.Clone() as Point3D, LBJ.Position.Clone() as Point3D);
            Bisector1.Rotate(angle.Radians / 2, new Vector3D(ICLine.StartPoint.Clone() as Point3D, ICLine.EndPoint.Clone() as Point3D), ICLine.MidPoint);
            cad1.viewportLayout1.Entities.Add(Bisector1);
        }

        private Angle AssignAngleSignBasedOnVectorDirection(Angle _angleRotation, MathNet.Spatial.Euclidean.Vector3D _rotationAxisVector)
        {
            if (_rotationAxisVector.Z < 0)
            {
                return _angleRotation;
            }
            else
            {
                return -_angleRotation;
            }
        }

        Line SteeringAxis;
        private void ConstructSteeringAxisINFLine()
        {
            SteeringAxis = new Line(UBJ.Position.Clone() as Point3D, LBJ.Position.Clone() as Point3D);
            //InfiniteLine.DrawInfiniteLine(SteeringAxis.StartPoint, SteeringAxis.EndPoint, cad1.viewportLayout1.Viewports[0]);
            cad1.viewportLayout1.Entities.Add(SteeringAxis);
        }

        Plane Plane1;
        Point3D PointA;
        PositionOfPointA PointAPos;
        private void ConstructPlaneRedPlane1AndPointA()
        {
            Plane1 = new Plane(BottomFrontInboard.Position.Clone() as Point3D, BottomRearInboard.Position.Clone() as Point3D, TopFrontInboard.Position.Clone() as Point3D);
            Segment3D SteeringAxisSegment = new Segment3D(SteeringAxis.StartPoint.Clone() as Point3D, SteeringAxis.EndPoint.Clone() as Point3D);
            SteeringAxisSegment.IntersectWith(Plane1, true, out PointA);

            if (PointA.Y > UBJ.Position.Y && PointA.Y > TopFrontInboard.Position.Y && PointA.Y > TopRearInboard.Position.Y) 
            {
                PointAPos = PositionOfPointA.AboveWishbones;
            }
            else if (PointA.Y < LBJ.Position.Y && PointA.Y < BottomFrontInboard.Position.Y && PointA.Y < BottomRearInboard.Position.Y)
            {
                PointAPos = PositionOfPointA.BelowWishbones;
            }

            PlanarEntity Plane1_Ent = new PlanarEntity(Plane1);
            Joint PointA_Ent = new Joint(PointA, 10, 2);
            PointA_Ent.Color = Color.AntiqueWhite;
            cad1.viewportLayout1.Entities.AddRange(new Entity[] { Plane1_Ent, PointA_Ent });
        }

        Plane PlaneRED, PlaneRED2;
        Angle Angle_Bis1_PlaneRED;
        Line LineForAngle_PlaneRED, LineForAngle_PlaneRed2;
        private void ConstructPlaneRED()
        {
            PlaneRED = new Plane(ICLine.StartPoint.Clone() as Point3D, ICLine.EndPoint.Clone() as Point3D, PointA.Clone() as Point3D);
            LineForAngle_PlaneRED = new Line(ICLine.MidPoint.Clone() as Point3D, PointA.Clone() as Point3D);

            Angle_Bis1_PlaneRED = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneRED), Custom3DGeometry.GetMathNetVector3D(Bisector1),
                                                                           Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));


            PlaneRED2 = PlaneRED.Clone() as Plane;
            LineForAngle_PlaneRed2 = LineForAngle_PlaneRED.Clone() as Line;

            PlaneRED2.Rotate(-Angle_Bis1_PlaneRED.Radians * 2, new Vector3D(ICLine.StartPoint.Clone() as Point3D, ICLine.EndPoint.Clone() as Point3D),ICLine.MidPoint);
            LineForAngle_PlaneRed2.Rotate(-Angle_Bis1_PlaneRED.Radians * 2, new Vector3D(ICLine.StartPoint.Clone() as Point3D, ICLine.EndPoint.Clone() as Point3D),ICLine.StartPoint);

            PlanarEntity PlaneRED_Ent = new PlanarEntity(PlaneRED);
            PlanarEntity PlaneRED2_Ent = new PlanarEntity(PlaneRED2);
            cad1.viewportLayout1.Entities.AddRange(new Entity[] { PlaneRED_Ent, PlaneRED2_Ent });
        }


        Plane PlaneSTEERING;
        ToeLinkUprightPosition ToeLinkUprightPos;
        BobillierPair LinkagePair;
        private void ConstructPlaneSTEERING()
        {
            PlaneSTEERING = new Plane(ICLine.StartPoint.Clone() as Point3D, ICLine.EndPoint.Clone() as Point3D, ToeLinkupright.Position.Clone() as Point3D);

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

        Line Bisector2;
        private void ConstructBisector2()
        {
            Line tempLineForAngle_PlaneSTEERING = new Line(ICLine.MidPoint.Clone() as Point3D, ToeLinkupright.Position.Clone() as Point3D);
            Line tempLineForAngle_PlaneWISHBONE = new Line(0, 0, 0, 1, 1, 1);
            if (LinkagePair == BobillierPair.BottomWishboneAndSteering)
            {
                tempLineForAngle_PlaneWISHBONE = new Line(ICLine.MidPoint.Clone() as Point3D, LBJ.Position.Clone() as Point3D);
            }
            else if (LinkagePair == BobillierPair.TopWishboneAndSteering)
            {
                tempLineForAngle_PlaneWISHBONE = new Line(ICLine.MidPoint.Clone() as Point3D, UBJ.Position.Clone() as Point3D);
            }

            ///<summary>Finding the Angle of the Steering Plane with the selected Wishbone Plane </summary>
            Angle Angle_PlaneSTEERING_PlaneWISHBONE = SetupChangeDatabase.AngleInRequiredView(_vAngleOfThis: Custom3DGeometry.GetMathNetVector3D(tempLineForAngle_PlaneSTEERING),
                                                                                              _vAngleWithThis: Custom3DGeometry.GetMathNetVector3D(tempLineForAngle_PlaneWISHBONE),
                                                                                              _vNormalToViewPlane: Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));

            //Angle_PlaneSTEERING_PlaneWISHBONE = AssignAngleSignBasedOnVectorDirection(Angle_PlaneSTEERING_PlaneWISHBONE, Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));
            ///<summary>Creating a Bisector Line</summary>
            Bisector2 = tempLineForAngle_PlaneWISHBONE.Clone() as Line;
            Bisector2.Rotate(angleInRadians: Angle_PlaneSTEERING_PlaneWISHBONE.Radians / 2, axis: new Vector3D(ICLine.StartPoint.Clone() as Point3D, ICLine.EndPoint.Clone() as Point3D),center: ICLine.MidPoint);
            cad1.viewportLayout1.Entities.Add(Bisector2);
        }

        Point3D PointB = new Point3D();
        /// <summary>
        /// <see cref="Angle"/> between the Bisector 2 and wither of the PlaneREDs. Which ever angle is smaller
        /// </summary>
        Angle Angle_Bis2_Final_PlaneREDs;
        Plane PlaneToConstructPlaneB, PlaneB;
        Segment3D SegmentForPointB;
        private void ConstructPlaneBAndPointB()
        {
            Angle Angle_Bis2_PlaneRED = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneRED),
                                                                                Custom3DGeometry.GetMathNetVector3D(Bisector2),
                                                                                Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));

            Angle Angle_Bis2_PlaneRED2 = SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(LineForAngle_PlaneRed2),
                                                                                 Custom3DGeometry.GetMathNetVector3D(Bisector2),
                                                                                 Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.StartPoint, ICLine.EndPoint)));

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

            PlaneToConstructPlaneB.Rotate(-Angle_Bis2_Final_PlaneREDs.Radians * 2, new Vector3D(ICLine.StartPoint, ICLine.EndPoint), ICLine.MidPoint);

            if (PointAPos == PositionOfPointA.AboveWishbones)
            {
                SegmentForPointB = new Segment3D(LBJ.Position, ToeLinkupright.Position);

            }
            else if (PointAPos == PositionOfPointA.BelowWishbones)
            {
                SegmentForPointB = new Segment3D(UBJ.Position, ToeLinkupright.Position);

            }

            SegmentForPointB.IntersectWith(PlaneToConstructPlaneB, true, out PointB);

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

        Line BobillierLine;
        private void ConstructBobillierLine()
        {
            Plane.Intersection(PlaneB, PlaneSTEERING, out Segment3D BobillierSegment);
            BobillierLine = new Line(BobillierSegment);

            Bar BobillierLine_Ent = new Bar(BobillierLine.StartPoint, BobillierLine.EndPoint, 100, 8);
            BobillierLine_Ent.Color = Color.AliceBlue;
            cad1.viewportLayout1.Entities.Add(BobillierLine_Ent);
        }

        private void CheckForBumpSteer()
        {
            Segment3D tempBobillierSegment = new Segment3D(BobillierLine.StartPoint, BobillierLine.EndPoint);
            ToeLinkInboard.Position.DistanceTo(tempBobillierSegment);
            double bumpSteerError = ToeLinkInboard.Position.DistanceTo(tempBobillierSegment);

        }
    }

    enum VehicleCorner
    {
        FrontLeft,
        FrontRight,
        RearLeft,
        RearRight
    }

    enum InstntCentrePosition
    {
        Inboard,
        Outboard
    }

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

    enum BobillierPair
    {
        TopWishboneAndSteering,
        BottomWishboneAndSteering
    }


}

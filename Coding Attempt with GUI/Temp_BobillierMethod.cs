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
            AssignBasePointstPoints();
            AssignBasePlanes();
            GetInstantCentreLine();
            GetBisector1();
            DrawSteeringAxisINFLine();
            DrawPlane1AndPointA();
        }

        SuspensionCoordinatesMaster SCM;

        Joint UBJ;
        Joint LBJ;
        Joint TopFrontInboard;
        Joint TopRearInboard;
        Joint BottomFrontInboard;
        Joint BottomRearInboard;
        Joint ToeLinkupright;
        private void AssignBasePointstPoints()
        {
            UBJ = new Joint(new Point3D(SCM.F1x, SCM.F1y, SCM.F1z), 5, 2);

            LBJ = new Joint(new Point3D(SCM.E1x, SCM.E1y, SCM.E1z), 5, 2);

            ToeLinkupright = new Joint(new Point3D(SCM.M1x, SCM.M1y, SCM.M1z), 5, 2);

            TopFrontInboard = new Joint(new Point3D(SCM.A1x, SCM.A1y, SCM.A1z), 5, 2);

            TopRearInboard = new Joint(new Point3D(SCM.B1x, SCM.B1y, SCM.B1z), 5, 2);

            BottomFrontInboard = new Joint(new Point3D(SCM.D1x, SCM.D1y, SCM.D1z), 5, 2);

            BottomRearInboard = new Joint(new Point3D(SCM.C1x, SCM.C1y, SCM.C1z), 5, 2);

            cad1.viewportLayout1.Entities.AddRange(new Entity[] { UBJ, LBJ, ToeLinkupright, TopFrontInboard, TopRearInboard, BottomFrontInboard, BottomRearInboard });
        }

        Plane TopWishbonePlane;
        Plane BottomWishbonePlane;
        Transformation scalePlane = new Transformation(20);

        private void AssignBasePlanes()
        {
            TopWishbonePlane = new Plane(UBJ.Position, TopFrontInboard.Position, TopRearInboard.Position);
            BottomWishbonePlane = new Plane(LBJ.Position, BottomFrontInboard.Position, BottomRearInboard.Position);

            //TopWishbonePlane.TransformBy(scalePlane);
            //BottomWishbonePlane.TransformBy(scalePlane);
        }

        Line ICLine;
        InstntCentrePosition ICPosition;
        private void GetInstantCentreLine()
        {
            Plane.Intersection(TopWishbonePlane, BottomWishbonePlane, out Segment3D ICSegment);
            
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
        private void GetBisector1()
        {
            Vector3D tempLineForAngle = new Vector3D(ICLine.MidPoint, UBJ.Position);
            Vector3D tempBisecVector = new Vector3D(ICLine.MidPoint, LBJ.Position);
            //MathNet.Spatial.Units.Angle angle = new MathNet.Spatial.Units.Angle(Vector3D.AngleBetween(tempBisecVector, tempLineForAngle), MathNet.Spatial.Units.AngleUnit.Radians);

            MathNet.Spatial.Units.Angle angle = (SetupChangeDatabase.AngleInRequiredView(Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.MidPoint, UBJ.Position)), 
                                                                                                                        Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.MidPoint, LBJ.Position)),
                                                                                                                        Custom3DGeometry.GetMathNetVector3D(new Line(ICLine.MidPoint,
                                                                                                                        new Point3D(ICLine.MidPoint.X,ICLine.MidPoint.Y,ICLine.MidPoint.Z+100)))));
            Bisector1 = new Line(ICLine.MidPoint, LBJ.Position);
            Bisector1.Rotate(angle.Radians / 2, new Vector3D(ICLine.StartPoint, ICLine.EndPoint));
            cad1.viewportLayout1.Entities.Add(Bisector1);
        }

        Line SteeringAxis;
        private void DrawSteeringAxisINFLine()
        {
            SteeringAxis = new Line(UBJ.Position, LBJ.Position);
            //InfiniteLine.DrawInfiniteLine(SteeringAxis.StartPoint, SteeringAxis.EndPoint, cad1.viewportLayout1.Viewports[0]);
            cad1.viewportLayout1.Entities.Add(SteeringAxis);
        }

        Plane Plane1;
        Point3D PointA;
        private void DrawPlane1AndPointA()
        {
            Plane1 = new Plane(BottomFrontInboard.Position, BottomRearInboard.Position, TopFrontInboard.Position);
            Plane1.TransformBy(scalePlane);
            Segment3D tempSeg = new Segment3D(SteeringAxis.StartPoint, SteeringAxis.EndPoint);
            tempSeg.IntersectWith(Plane1, out PointA);
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

}

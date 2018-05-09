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

        private void AssignLocalSuspensionObject(SuspensionCoordinatesMaster _scm)
        {
            if (_scm != null)
            {
                SCM = _scm;
            }
        }

        private void Execute()
        {
            AssignBasePointstPoints();
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
            UBJ = new Joint(new Point3D(SCM.F1x, SCM.F1y, SCM.F1z), 5, 8);
            
            LBJ = new Joint(new Point3D(SCM.E1x, SCM.E1y, SCM.E1z), 5, 8);

            ToeLinkupright = new Joint(new Point3D(SCM.M1x, SCM.M1y, SCM.M1z), 5, 8);

            TopFrontInboard = new Joint(new Point3D(SCM.A1x, SCM.A1y, SCM.A1z), 5, 8);

            TopRearInboard = new Joint(new Point3D(SCM.B1x, SCM.B1y, SCM.B1z), 5, 8);

            BottomFrontInboard = new Joint(new Point3D(SCM.D1x, SCM.D1y, SCM.D1z), 5, 8);

            BottomRearInboard = new Joint(new Point3D(SCM.C1x, SCM.C1y, SCM.C1z), 5, 8);

            cad1.viewportLayout1.Entities.AddRange(new Entity[] { UBJ, LBJ, ToeLinkupright, TopFrontInboard, TopRearInboard, BottomFrontInboard, BottomRearInboard });
        }

        Plane TopWishbonePlane;
        Plane BottomWishbonePlane;
        private void AssignBasePlanes()
        {
            TopWishbonePlane = new Plane(UBJ.Position, TopFrontInboard.Position, TopRearInboard.Position);
            BottomWishbonePlane = new Plane(LBJ.Position, BottomFrontInboard.Position, BottomRearInboard.Position);

            
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
        private void GetBisector1(VehicleCorner _vCorner)
        {
            if (_vCorner == VehicleCorner.FrontLeft || _vCorner == VehicleCorner.RearLeft)
            {
                if (ICPosition == InstntCentrePosition.Inboard)
                {
                    Bisector1 = new Line(ICLine.MidPoint, LBJ.Position);
                }
            }
            else
            {
                
            }
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

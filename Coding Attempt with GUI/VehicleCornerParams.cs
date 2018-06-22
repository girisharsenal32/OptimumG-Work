using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Units;
using devDept.Geometry;
using devDept.Eyeshot.Entities;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// <para>This Class contains the Informtation of ALL the Components assembled onto a given corner of the Vehicle</para> 
    /// <para>Components include Suspension Coordinates, Springs, Dampers, ARB, Tire etc</para>
    /// </summary>
    public class VehicleCornerParams
    {

        //--VEHICLE Corner PARAMETERS--

        /// <summary>
        /// Object of the <see cref="VehicleCorner"/> which decides the corner of the Vehicle calling this Class
        /// </summary>
        public VehicleCorner VCorner;
        /// <summary>
        /// Suspension Coordiantes of the corner of the Vehicle calling this class
        /// </summary>
        public SuspensionCoordinatesMaster SCM;
        /// <summary>
        /// Cloned Object of the <see cref="SCM"/>. This is used to prevent residue error as in each iteraiton a fresh simulation is run
        /// </summary>
        public SuspensionCoordinatesMaster SCM_Clone;
        /// <summary>
        /// Spring of the corner of the Vehicle calling this class
        /// </summary>
        public Spring Spring;
        /// <summary>
        /// Damper of the corner of the Vehicle calling this class
        /// </summary>
        public Damper Damper;
        /// <summary>
        /// Anti-Roll Bar of the corner of the Vehicle calling this class
        /// </summary>
        public AntiRollBar ARB;
        /// <summary>
        /// Anti-Roll Bar Rate in N/mm of the corner of the Vehicle calling this class
        /// </summary>
        public double ARBRate_Nmm;
        /// <summary>
        /// Tire of the corner of the Vehicle calling this class
        /// </summary>
        public Tire Tire;
        /// <summary>
        /// Wheel Alignmetn of the corner of the Vehicle calling this class
        /// </summary>
        public WheelAlignment WA;
        /// <summary>
        /// <see cref="List{T}"/> of <see cref="OutputClass"/> objects of the corner of the Vehicle calling this class
        /// </summary>
        public List<OutputClass> OC;
        /// <summary>
        /// This <see cref="OutputClass"/> List is exclusively used for the Bump Steer method as it needs to run a Motion Analysis from -25 to +25 (or whatever the user decides)
        /// </summary>
        public List<OutputClass> OC_BumpSteer;
        /// <summary>
        /// Identifier Number of the corner of the Vehicle calling this class
        /// </summary>
        public int Identifier;


        //-RELEVANT COORDINATES-

        /// <summary>
        /// Upper Ball Joint
        /// </summary>
        public Point3D UBJ;
        /// <summary>
        /// 2nd Upper Ball Joint incase 5 Link Suspension
        /// </summary>
        public Point3D UBJ_2;
        /// <summary>
        /// Top Camber Mount 
        /// </summary>
        public Point3D TopCamberMount;
        /// <summary>
        /// Pushrod
        /// </summary>
        public Point3D PushrodOutboard;
        /// <summary>
        /// Lower Ball Joint
        /// </summary>
        public Point3D LBJ;
        /// <summary>
        /// 2nd Lower Ball Joint incase 5 Link Suspension
        /// </summary>
        public Point3D LBJ_2;
        /// <summary>
        /// Bottom Camber Mount 
        /// </summary>
        public Point3D BottomCamberMount;
        /// <summary>
        /// Outboard Toe Link Joint
        /// </summary>
        public Point3D ToeLinkOutboard;
        /// <summary>
        /// Wheel Center Start
        /// </summary>
        public Point3D WheelCenter;
        /// <summary>
        /// Wheel Center End
        /// </summary>
        public Point3D WcEnd;
        /// <summary>
        /// Contact Patch 
        /// </summary>
        public Point3D ContactPatch;

        /// <summary>
        /// Inboard Toe Link Joint
        /// </summary>
        public Point3D ToeLinkInboard;
        /// <summary>
        /// Top Front Inboard Pick-Up Point
        /// </summary>
        public Point3D UpperFront;
        /// <summary>
        /// Top Rear Inboard Pick-Up Point
        /// </summary>
        public Point3D UpperRear;
        /// <summary>
        /// Bottom Front Inboard Pick-Up Point
        /// </summary>
        public Point3D LowerFront;
        /// <summary>
        /// Bottom Rear Inboard Pick-Up Point
        /// </summary>
        public Point3D LowerRear;
        /// <summary>
        /// Pushrod Bell-Crank Inboard Pick-Up Point
        /// </summary>
        public Point3D PushrodInboard;
        /// <summary>
        /// Damper Shock Mount Pick-Up Point
        /// </summary>
        public Point3D DamperShockMount;
        /// <summary>
        /// Damper Bell-Crank Pick-Up Point
        /// </summary>
        public Point3D DamperBellCrank;
        /// <summary>
        /// Only for MCPHERSON
        /// </summary>
        public Point3D DamperOutboard;
        /// <summary>
        /// <see cref="Dictionary{String, Point3D}"/> of all the Outboard Points
        /// </summary>
        public Dictionary<string, Point3D> OutboardAssembly;
        /// <summary>
        /// <see cref="Dictionary{String, Point3D}"/> of the All the Inboard Pick-Up Points
        /// </summary>
        public Dictionary<string, Point3D> InboardAssembly;
        
        
        
        /// <summary>
        /// <see cref="Dictionary{String, Line}"/> of Lines reprsenting the Axis Lines originating the Wheel Center
        /// </summary>
        public Dictionary<string, Line> AxisLines_WheelCenter;

        /// <summary>
        /// <see cref="Line"/> representing the Steering Axis
        /// </summary>
        public Line SteeringAxis;

        /// <summary>
        /// Line representing the Front View VSAL
        /// </summary>
        public Line FV_IC_Line;

        /// <summary>
        /// Line representing the Side View VSAL
        /// </summary>
        public Line SV_IC_Line;

        /// <summary>
        /// Plane containing the Top Wishbones  
        /// </summary>
        public Plane TopWishbonePlane;

        /// <summary>
        /// Plne containing the Bottom Wishbones
        /// </summary>
        public Plane BottomWishbonePlane;


        public InboardInputFormat InputFormat = InboardInputFormat.IIO;





        public VehicleCornerParams() {}

        /// <summary>
        /// Method to Initialize ALL the <see cref="Point3D"/>s using a <see cref="SuspensionCoordinatesMaster"/> object
        /// </summary>
        public void Initialize_Points(SuspensionCoordinatesMaster _scm)
        {
            OutboardAssembly = new Dictionary<string, Point3D>();

            InboardAssembly = new Dictionary<string, Point3D>();

            ///---INBOARD POINTS

            ///Upper Front
            UpperFront = new Point3D(_scm.A1x, _scm.A1y, _scm.A1z);
            InboardAssembly.Add(CoordinateOptions.UpperFront.ToString(), UpperFront);

            ///Upper Rear
            UpperRear = new Point3D(_scm.B1x, _scm.B1y, _scm.B1z);
            InboardAssembly.Add(CoordinateOptions.UpperRear.ToString(), UpperRear);

            ///Lower Front
            LowerFront = new Point3D(_scm.D1x, _scm.D1y, _scm.D1z);
            InboardAssembly.Add(CoordinateOptions.LowerFront.ToString(), LowerFront);

            ///Lower Rear
            LowerRear = new Point3D(_scm.C1x, _scm.C1y, _scm.C1z);
            InboardAssembly.Add(CoordinateOptions.LowerRear.ToString(), LowerRear);

            ///Toe Link Inboard
            ToeLinkInboard = new Point3D(_scm.N1x, _scm.N1y, _scm.N1z);
            InboardAssembly.Add(CoordinateOptions.ToeLinkInboard.ToString(), ToeLinkInboard);

            ///Pushrod Inboard
            PushrodInboard = new Point3D(_scm.H1x, _scm.H1y, _scm.H1z);
            InboardAssembly.Add(CoordinateOptions.PushrodInboard.ToString(), PushrodInboard);

            ///Damper BellCrank
            DamperBellCrank = new Point3D(_scm.J1x, _scm.J1y, _scm.J1z);
            InboardAssembly.Add(CoordinateOptions.DamperBellCrank.ToString(), DamperBellCrank);

            ///Damper Chassis Shock Mount
            DamperShockMount = new Point3D(_scm.JO1x, _scm.JO1y, _scm.JO1z);
            InboardAssembly.Add(CoordinateOptions.DamperShockMount.ToString(), DamperShockMount);


            ///---OUTBOARD POINTS
            

            ///UBJ
            UBJ = new Point3D(_scm.F1x, _scm.F1y, _scm.F1z);
            OutboardAssembly.Add(CoordinateOptions.UBJ.ToString(), UBJ);

            ///Top Camber Mount
            TopCamberMount = new Point3D(_scm.TCM1x, _scm.TCM1y, _scm.TCM1z);
            OutboardAssembly.Add(CoordinateOptions.TopCamberMount.ToString(), /*UBJ.Clone() as Point3D*/ TopCamberMount);

            ///Pushrod Outboard
            PushrodOutboard = new Point3D(_scm.G1x, _scm.G1y, _scm.G1z);
            OutboardAssembly.Add(CoordinateOptions.PushrodOutboard.ToString(), PushrodOutboard);

            ///LBJ
            LBJ = new Point3D(_scm.E1x, _scm.E1y, _scm.E1z);
            OutboardAssembly.Add(CoordinateOptions.LBJ.ToString(), LBJ);

            ///Bottom Camber Mount
            BottomCamberMount = new Point3D(_scm.BCM1x, _scm.BCM1y, _scm.BCM1z);
            OutboardAssembly.Add(CoordinateOptions.BottomCamberMount.ToString(), BottomCamberMount);

            ///Wheel Center
            WheelCenter = new Point3D(_scm.K1x, _scm.K1y, _scm.K1z);
            OutboardAssembly.Add(CoordinateOptions.WheelCenter.ToString(), WheelCenter);

            ///Wheel Spindle End
            WcEnd = new Point3D(_scm.L1x, _scm.L1y, _scm.L1z);
            OutboardAssembly.Add("WcEnd", WcEnd);

            ///Toe Link Outboard
            ToeLinkOutboard = new Point3D(_scm.M1x, _scm.M1y, _scm.M1z);
            OutboardAssembly.Add(CoordinateOptions.ToeLinkOutboard.ToString(), ToeLinkOutboard);

            ///Contact Patch
            ContactPatch = new Point3D(_scm.W1x, _scm.W1x, _scm.W1z);
            OutboardAssembly.Add("ContactPatch", ContactPatch);


            AxisLines_WheelCenter = new Dictionary<string, Line>();

            AxisLines_WheelCenter.Add("SteeringAxis", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

            AxisLines_WheelCenter.Add("SteeringAxis_Ref", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

            AxisLines_WheelCenter.Add("LateralAxis_WheelCenter", new Line(WheelCenter.Clone() as Point3D, new Point3D(WheelCenter.X + 100, WheelCenter.Y, WheelCenter.Z)));

            AxisLines_WheelCenter.Add("WheelSpindle", new Line(WheelCenter.Clone() as Point3D, WcEnd.Clone() as Point3D));

            AxisLines_WheelCenter.Add("WheelSpindle_Ref", new Line(WheelCenter.Clone() as Point3D, WcEnd.Clone() as Point3D));

            AxisLines_WheelCenter.Add("VerticalAxis_WheelCenter", new Line(WheelCenter.Clone() as Point3D, new Point3D(WheelCenter.X, WheelCenter.Y + 100, WheelCenter.Z)));

            AxisLines_WheelCenter.Add("LongitudinalAxis_WheelCenter", new Line(WheelCenter.Clone() as Point3D, new Point3D(WheelCenter.X, WheelCenter.Y, WheelCenter.Z + 100)));

        }

        /// <summary>
        /// Method to Initialize the Points as Empty 
        /// </summary>
        public void Initialize_Points()
        {
            UpperFront = new Point3D();

            UpperRear = new Point3D();


            LowerFront = new Point3D();

            LowerRear = new Point3D();

            ToeLinkInboard = new Point3D();

            PushrodInboard = new Point3D();

            DamperBellCrank = new Point3D();

            DamperShockMount = new Point3D();

            UBJ = new Point3D();

            TopCamberMount = new Point3D();

            PushrodOutboard = new Point3D();

            LBJ = new Point3D();

            BottomCamberMount = new Point3D();

            WheelCenter = new Point3D();

            WcEnd = new Point3D();

            ToeLinkOutboard = new Point3D();

            ContactPatch = new Point3D();

        }

        /// <summary>
        /// Method to Initialize the <see cref="InboardAssembly"/> and <see cref="OutboardAssembly"/> <see cref="Dictionary{String, Point3D}"/>s
        /// </summary>
        public void Initialize_Dictionary()
        {
            OutboardAssembly = new Dictionary<string, Point3D>();

            InboardAssembly = new Dictionary<string, Point3D>();



            ///---INBOARD POINTS

            ///Upper Front
            InboardAssembly.Add(CoordinateOptions.UpperFront.ToString(), UpperFront);

            ///Upper Rear
            InboardAssembly.Add(CoordinateOptions.UpperRear.ToString(), UpperRear);

            ///Lower Front
            InboardAssembly.Add(CoordinateOptions.LowerFront.ToString(), LowerFront);

            ///Lower Rear
            InboardAssembly.Add(CoordinateOptions.LowerRear.ToString(), LowerRear);

            ///Toe Link Inboard
            InboardAssembly.Add(CoordinateOptions.ToeLinkInboard.ToString(), ToeLinkInboard);

            ///Pushrod Inboard
            InboardAssembly.Add(CoordinateOptions.PushrodInboard.ToString(), PushrodInboard);

            ///Damper BellCrank
            InboardAssembly.Add(CoordinateOptions.DamperBellCrank.ToString(), DamperBellCrank);

            ///Damper Chassis Shock Mount
            InboardAssembly.Add(CoordinateOptions.DamperShockMount.ToString(), DamperShockMount);


            ///---OUTBOARD POINTS


            ///UBJ
            OutboardAssembly.Add(CoordinateOptions.UBJ.ToString(), UBJ);

            ///Top Camber Mount
            OutboardAssembly.Add(CoordinateOptions.TopCamberMount.ToString(), /*UBJ.Clone() as Point3D*/ TopCamberMount);

            ///Pushrod Outboard
            OutboardAssembly.Add(CoordinateOptions.PushrodOutboard.ToString(), PushrodOutboard);

            ///LBJ
            OutboardAssembly.Add(CoordinateOptions.LBJ.ToString(), LBJ);

            ///Bottom Camber Mount
            OutboardAssembly.Add(CoordinateOptions.BottomCamberMount.ToString(), BottomCamberMount);

            ///Wheel Center
            OutboardAssembly.Add(CoordinateOptions.WheelCenter.ToString(), WheelCenter);

            ///Wheel Spindle End
            OutboardAssembly.Add(CoordinateOptions.WheelSpindleEnd.ToString(), WcEnd);

            ///Toe Link Outboard
            OutboardAssembly.Add(CoordinateOptions.ToeLinkOutboard.ToString(), ToeLinkOutboard);

            ///Contact Patch
            OutboardAssembly.Add("ContactPatch", ContactPatch);


            AxisLines_WheelCenter = new Dictionary<string, Line>();

            if (UBJ != null && LBJ != null && WheelCenter != null && WcEnd != null) 
            {
                AxisLines_WheelCenter.Add("SteeringAxis", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

                AxisLines_WheelCenter.Add("SteeringAxis_Ref", new Line(UBJ.Clone() as Point3D, LBJ.Clone() as Point3D));

                AxisLines_WheelCenter.Add("LateralAxis_WheelCenter", new Line(WheelCenter.Clone() as Point3D, new Point3D(WheelCenter.X + 100, WheelCenter.Y, WheelCenter.Z)));

                AxisLines_WheelCenter.Add("WheelSpindle", new Line(WheelCenter.Clone() as Point3D, WcEnd.Clone() as Point3D));

                AxisLines_WheelCenter.Add("WheelSpindle_Ref", new Line(WheelCenter.Clone() as Point3D, WcEnd.Clone() as Point3D));

                AxisLines_WheelCenter.Add("VerticalAxis_WheelCenter", new Line(WheelCenter.Clone() as Point3D, new Point3D(WheelCenter.X, WheelCenter.Y + 100, WheelCenter.Z)));

                AxisLines_WheelCenter.Add("LongitudinalAxis_WheelCenter", new Line(WheelCenter.Clone() as Point3D, new Point3D(WheelCenter.X, WheelCenter.Y, WheelCenter.Z + 100)));  
            }



        }




    }
}

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





        public VehicleCornerParams() {}

        /// <summary>
        /// Method to Initialize ALL the <see cref="Point3D"/>s and correspondong <see cref="Dictionary{String, Point3D}"/>s of the <see cref="VehicleCornerParams"/> Class
        /// </summary>
        public void InitializePointsAndDictionary()
        {
            OutboardAssembly = new Dictionary<string, Point3D>();

            InboardAssembly = new Dictionary<string, Point3D>();

            ///---INBOARD POINTS

            ///Upper Front
            UpperFront = new Point3D(SCM.A1x, SCM.A1y, SCM.A1z);
            InboardAssembly.Add(CoordinateOptions.UpperFront.ToString(), UpperFront);

            ///Upper Rear
            UpperRear = new Point3D(SCM.B1x, SCM.B1y, SCM.B1z);
            InboardAssembly.Add(CoordinateOptions.UpperRear.ToString(), UpperRear);

            ///Lower Front
            LowerFront = new Point3D(SCM.D1x, SCM.D1y, SCM.D1z);
            InboardAssembly.Add(CoordinateOptions.LowerFront.ToString(), LowerFront);

            ///Lower Rear
            LowerRear = new Point3D(SCM.C1x, SCM.C1y, SCM.C1z);
            InboardAssembly.Add(CoordinateOptions.LowerRear.ToString(), LowerRear);

            ///Toe Link Inboard
            ToeLinkInboard = new Point3D(SCM.N1x, SCM.N1y, SCM.N1z);
            InboardAssembly.Add(CoordinateOptions.ToeLinkInboard.ToString(), ToeLinkInboard);

            ///Pushrod Inboard
            PushrodInboard = new Point3D(SCM.H1x, SCM.H1y, SCM.H1z);
            InboardAssembly.Add(CoordinateOptions.PushrodInboard.ToString(), PushrodInboard);

            ///Damper BellCrank
            DamperBellCrank = new Point3D(SCM.J1x, SCM.J1y, SCM.J1z);
            InboardAssembly.Add(CoordinateOptions.DamperBellCrank.ToString(), DamperBellCrank);

            ///Damper Chassis Shock Mount
            DamperShockMount = new Point3D(SCM.JO1x, SCM.JO1y, SCM.JO1z);
            InboardAssembly.Add(CoordinateOptions.DamperShockMount.ToString(), DamperShockMount);


            ///---OUTBOARD POINTS
            

            ///UBJ
            UBJ = new Point3D(SCM.F1x, SCM.F1y, SCM.F1z);
            OutboardAssembly.Add(CoordinateOptions.UBJ.ToString(), UBJ);

            ///Top Camber Mount
            TopCamberMount = new Point3D(SCM.TCM1x, SCM.TCM1y, SCM.TCM1z);
            OutboardAssembly.Add(CoordinateOptions.TopCamberMount.ToString(), /*UBJ.Clone() as Point3D*/ TopCamberMount);

            ///Pushrod Outboard
            PushrodOutboard = new Point3D(SCM.G1x, SCM.G1y, SCM.G1z);
            OutboardAssembly.Add(CoordinateOptions.PushrodOutboard.ToString(), PushrodOutboard);

            ///LBJ
            LBJ = new Point3D(SCM.E1x, SCM.E1y, SCM.E1z);
            OutboardAssembly.Add(CoordinateOptions.LBJ.ToString(), LBJ);

            ///Bottom Camber Mount
            BottomCamberMount = new Point3D(SCM.BCM1x, SCM.BCM1y, SCM.BCM1z);
            OutboardAssembly.Add(CoordinateOptions.BottomCamberMount.ToString(), BottomCamberMount);

            ///Wheel Center
            WheelCenter = new Point3D(SCM.K1x, SCM.K1y, SCM.K1z);
            OutboardAssembly.Add(CoordinateOptions.WheelCenter.ToString(), WheelCenter);

            ///Wheel Spindle End
            WcEnd = new Point3D(SCM.L1x, SCM.L1y, SCM.L1z);
            OutboardAssembly.Add("WcEnd", WcEnd);

            ///Toe Link Outboard
            ToeLinkOutboard = new Point3D(SCM.M1x, SCM.M1y, SCM.M1z);
            OutboardAssembly.Add(CoordinateOptions.ToeLinkOutboard.ToString(), ToeLinkOutboard);

            ///Contact Patch
            ContactPatch = new Point3D(SCM.W1x, SCM.W1x, SCM.W1z);
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





    }
}

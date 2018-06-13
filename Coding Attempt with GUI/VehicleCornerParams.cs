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
        public Point3D Pushrod;
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
        public Point3D WcStart;
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
        public Point3D TopFront;
        /// <summary>
        /// Top Rear Inboard Pick-Up Point
        /// </summary>
        public Point3D TopRear;
        /// <summary>
        /// Bottom Front Inboard Pick-Up Point
        /// </summary>
        public Point3D BottomFront;
        /// <summary>
        /// Bottom Rear Inboard Pick-Up Point
        /// </summary>
        public Point3D BottomRear;
        /// <summary>
        /// Pushrod Bell-Crank Inboard Pick-Up Point
        /// </summary>
        public Point3D PushrodShockMount;
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

    }
}

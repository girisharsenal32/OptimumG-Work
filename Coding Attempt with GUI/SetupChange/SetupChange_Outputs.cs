using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This Class consists of all the Outputs pertaining to the SetupChange problem. 
    /// These includes Angles, Wishbone Link Lengths, Camber Shims and Pick-Up Point Coordinates
    /// </summary>
    public class SetupChange_Outputs
    {
        #region ---Declarations---
        #region --Properties--
        /// <summary>
        /// Determines which corner this OutputClass belongs to 
        /// </summary>
        public VehicleCorner Corner { get; set; }

        /// <summary>
        /// Identifier Number to decide the Vehicle Corner incase <see cref="VehicleCorner"/> can be used directly
        /// </summary>
        public int Identifier { get; set; }
        #endregion

        #region --Setup Change Ouptuts--

        #region -Link Lengths-
        /// <summary>
        /// Top Front Arm Length
        /// </summary>
        public double TopFrontLength;

        /// <summary>
        /// Top Rear Arm Length
        /// </summary>
        public double TopRearLength;

        /// <summary>
        /// Bottom Front Arm Length
        /// </summary>
        public double BottomFrontLength;

        /// <summary>
        /// Bottom Rear Arm Length
        /// </summary>
        public double BottomRearLength;

        /// <summary>
        /// Pushrod Length
        /// </summary>
        public double PushrodLength;

        /// <summary>
        /// Toe Link Length
        /// </summary>
        public double ToeLinklength;
        #endregion

        #region -Pick-Up Points-
        /// <summary>
        /// Toe Link Inboard Pick-Up Point
        /// </summary>
        public Point3D ToeLinkInboard;
        #endregion

        #region -Shims-
        /// <summary>
        /// Vector Magnitude of the Top Camber Shims
        /// </summary>
        public double TopCamberShimsLength;

        /// <summary>
        /// Number of Top Camber Shims
        /// </summary>
        public double TopCamberShimsNo;

        /// <summary>
        /// Vector Magnitude of the Bottom Camber Shims
        /// </summary>
        public double BottomCamberShimsLength;

        /// <summary>
        /// Number of Bottom Camber Shims 
        /// </summary>
        public double BottomCamberShimsNo;

        #endregion

        #region -Angles-
        /// <summary>
        /// Final Camber Angle 
        /// </summary>
        public Angle Calc_Camber;
        /// <summary>
        /// Requested Camber Angle. 
        /// </summary>
        public Angle Req_Camber;

        /// <summary>
        /// Fnal Toe Angle
        /// </summary>
        public Angle Calc_Toe;
        /// <summary>
        /// Requested Toe Angle
        /// </summary>
        public Angle Req_Toe;

        /// <summary>
        /// Final Caster Angle
        /// </summary>
        public Angle Calc_Caster;
        /// <summary>
        /// Requested Caster Angle
        /// </summary>
        public Angle Req_Caster;

        /// <summary>
        /// Final KPI Angle
        /// </summary>
        public Angle Calc_KPI;
        /// <summary>
        /// Requested KPI Angle
        /// </summary>
        public Angle Req_KPI;
        #endregion

        #region -Direct Values-
        public double Calc_RideHeight;

        public double Req_RideHeight;
        #endregion

        #region -Charts-
        /// <summary>
        /// Final Bump Steer Chart
        /// </summary>
        public List<Angle> Calc_BumpSteerChart;
        /// <summary>
        /// Bump Steer Chsrt requested by the User
        /// </summary>
        public List<Angle> Req_BumpSteerChart;
        #endregion

        #endregion

        #region --Convergence Criteria--
        /// <summary>
        /// Convergance value of Caster
        /// </summary>
        public Convergence Caster_Conv;

        /// <summary>
        /// Convergance value of KPI
        /// </summary>
        public Convergence KPI_Conv;

        /// <summary>
        /// Convergance value of Camber
        /// </summary>
        public Convergence Camber_Conv;

        /// <summary>
        /// Convergance value of Toe
        /// </summary>
        public Convergence Toe_Conv;

        /// <summary>
        /// Convergance value of Bump Steer
        /// </summary>
        public Convergence BumpSteer_Conv;

        /// <summary>
        /// Convergence value of the Ride Height
        /// </summary>
        public Convergence RideHeight_Conv;

        /// <summary>
        /// Total or RMS Convergence Value
        /// </summary>
        public Convergence Total_Conv;

        #endregion

        #endregion


        #region ---Methods---

        #region --Constructors--
        /// <summary>
        /// Base Constructor
        /// </summary>
        public SetupChange_Outputs()
        {
            SetBaseConvergence();
        }

        /// <summary>
        /// First OVerloaded Consturctor accepting <see cref="VehicleCorner"/>
        /// </summary>
        /// <param name="_vCorner"></param>
        public SetupChange_Outputs(VehicleCorner _vCorner)
        {
            Corner = _vCorner;

            Identifier = (int)Corner;

            ToeLinkInboard = new Point3D();

            SetBaseConvergence();
        }

        /// <summary>
        /// 2nd overloaded constructor accpting identifier integer
        /// </summary>
        /// <param name="_identifier"></param>
        public SetupChange_Outputs(int _identifier)
        {
            Identifier = _identifier;

            Corner = (VehicleCorner)Identifier;

            ToeLinkInboard = new Point3D();

            SetBaseConvergence();
        }
        #endregion


        /// <summary>
        /// Method to initialize the <see cref="Convergence"/> of each of the Setup Params.
        /// This is crucial because if a Setup Param is not requested then the method below would have alreaddy set its <see cref="Convergence.ConvergenceStatus"/> to "Not Requested"
        /// </summary>
        private void SetBaseConvergence()
        {
            Caster_Conv = new Convergence("NotRequested");

            KPI_Conv = new Convergence("NotRequested");

            Camber_Conv = new Convergence("NotRequested");

            Toe_Conv = new Convergence("NotRequested");

            BumpSteer_Conv = new Convergence("NotRequested");

            RideHeight_Conv = new Convergence("NotRequested");

            Total_Conv = new Convergence("0");
        }

        /// <summary>
        /// Method to Assign the link Lengths
        /// </summary>
        /// <param name="_topFront"></param>
        /// <param name="_topRear"></param>
        /// <param name="_bottomFront"></param>
        /// <param name="_bottomRear"></param>
        /// <param name="_pushRod"></param>
        /// <param name="_toeLink"></param>
        public void SetLinkLengths(double _topFront, double _topRear, double _bottomFront, double _bottomRear, double _pushRod, double _toeLink)
        {
            TopFrontLength = _topFront;

            TopRearLength = _topRear;

            BottomFrontLength = _bottomFront;

            BottomRearLength = _bottomRear;

            PushrodLength = _pushRod;

            ToeLinklength = _toeLink;
        }

        /// <summary>
        /// Method to assign ORIENTATION to the Angles according to the User's Convention and then initialize the angles 
        /// The goal is that, if a Setup Angle is requested then these angles below will be overridden by the <see cref="SetupChange_Optimizer"/>'s results and then later will be conditioned with the right orientation
        /// BUT, if a Setup ANgle is not requested, then initalizing it and conditioning it with right direction here itself will be useful as then it will simply have to be displayed in the Final Output Stage
        /// </summary>
        /// <param name="_fCamber"></param>
        /// <param name="_fToe"></param>
        /// <param name="_fCaster"></param>
        /// <param name="_fKPI"></param>
        public void InitializeAngles(Angle _fCamber, Angle _fToe, Angle _fCaster, Angle _fKPI)
        {
            double tempToe = _fToe.Degrees;

            double tempCamber = _fCamber.Degrees;

            double tempKPI = _fKPI.Degrees;

            //SolverMasterClass.AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, Identifier);

            Calc_Camber = new Angle(tempCamber, AngleUnit.Degrees);

            Calc_Toe = new Angle(tempToe, AngleUnit.Degrees);

            Calc_Caster = -_fCaster;

            SolverMasterClass.AssignDirection_KPI(Identifier, ref tempKPI);

            Calc_KPI = new Angle(tempKPI, AngleUnit.Degrees);
        }

        /// <summary>
        /// Method to assign the Charts
        /// </summary>
        /// <param name="_bumpSteerChart"></param>
        public void SetCharts(List<Angle> _bumpSteerChart)
        {
            Calc_BumpSteerChart = _bumpSteerChart;
        }

        /// <summary>
        /// Method to assign the Pick-Up Points
        /// </summary>
        /// <param name="_toeLinkInboard"></param>
        public void SetPickUpPonts(Point3D _toeLinkInboard)
        {
            ToeLinkInboard = _toeLinkInboard;
        }

        /// <summary>
        /// Method to Clone all the properpties and variables of the <see cref="SetupChange_Outputs"/> Class from one Object to another
        /// </summary>
        /// <returns></returns>
        public SetupChange_Outputs Clone()
        {
            SetupChange_Outputs tempOP = new SetupChange_Outputs();

            tempOP.Corner = this.Corner;

            tempOP.Identifier = this.Identifier;

            tempOP.TopFrontLength = this.TopFrontLength;

            tempOP.TopRearLength = this.TopRearLength;

            tempOP.BottomFrontLength = this.BottomFrontLength;

            tempOP.BottomRearLength = this.BottomRearLength;

            tempOP.PushrodLength = this.PushrodLength;

            tempOP.ToeLinklength = this.ToeLinklength;

            tempOP.ToeLinkInboard = this.ToeLinkInboard;

            tempOP.TopCamberShimsLength = this.TopCamberShimsLength;

            tempOP.TopCamberShimsNo = this.TopCamberShimsNo;

            tempOP.BottomCamberShimsLength = this.BottomCamberShimsLength;

            tempOP.BottomCamberShimsNo = this.BottomCamberShimsNo;

            tempOP.Calc_Camber = this.Calc_Camber;

            tempOP.Calc_Toe = this.Calc_Toe;

            tempOP.Calc_Caster = this.Calc_Caster;

            tempOP.Calc_KPI = this.Calc_KPI;

            tempOP.Req_Camber = this.Req_Camber;

            tempOP.Req_Toe = this.Req_Toe;

            tempOP.Req_Caster = this.Req_Caster;

            tempOP.Req_KPI = this.Req_KPI;

            tempOP.Calc_RideHeight = this.Calc_RideHeight;

            tempOP.Req_RideHeight = this.Req_RideHeight;

            tempOP.Calc_BumpSteerChart = this.Calc_BumpSteerChart;

            tempOP.Req_BumpSteerChart = this.Req_BumpSteerChart;

            tempOP.Caster_Conv = this.Caster_Conv;

            tempOP.KPI_Conv = this.KPI_Conv;

            tempOP.Camber_Conv = this.Camber_Conv;

            tempOP.Toe_Conv = this.Toe_Conv;

            tempOP.BumpSteer_Conv = this.BumpSteer_Conv;

            tempOP.RideHeight_Conv = this.RideHeight_Conv;

            tempOP.Total_Conv = this.Total_Conv;


            return tempOP;

        }

        #endregion

    }

    #region ---Convergence Struct---
    /// <summary>
    /// Class to hold information regarding the Convergance of a Setup Paras
    /// </summary>
    public struct Convergence
    {
        /// <summary>
        /// Percentage of Convergence. Between 0 and 100
        /// </summary>
        public double Percentage { get; set; }

        /// <summary>
        /// Ratio of Convergence. Between 0 and 1
        /// </summary>
        public double Ratio { get; set; }

        /// <summary>
        /// Status of the convergence. Used only when a Particular Param is not requested
        /// </summary>
        public string ConvergenceStatus { get; set; }

        /// <summary>
        /// Constructor for Params which have been requested. The Ratio of Convergence is to be passed as argument
        /// </summary>
        /// <param name="_ratio"></param>
        public Convergence(double _ratio)
        {
            Ratio = _ratio;

            Percentage = Math.Round(Ratio * 100, 3);

            ConvergenceStatus = Percentage.ToString();
        }

        /// <summary>
        /// <para>Method to construct a Null convergence object. Calling this will set the Convergence status to "Not Requested"</para>
        /// <para>This constructor should be used ALWAYS when initializing a convergence object for the first time. 
        /// This is so that if the convergence object is not required then it can display "Not Requested"</para>
        /// </summary>
        /// <param name="_notRequested"></param>
        public Convergence(string _notRequested)
        {
            Ratio = 0;

            Percentage = Ratio * 100;

            ConvergenceStatus = _notRequested;
        }



    }
    #endregion

}

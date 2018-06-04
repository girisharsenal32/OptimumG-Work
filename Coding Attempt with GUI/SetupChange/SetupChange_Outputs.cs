﻿using System;
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
        public Angle Camber;

        /// <summary>
        /// Fnal Toe Angle
        /// </summary>
        public Angle Toe;

        /// <summary>
        /// Final Caster Angle
        /// </summary>
        public Angle Caster;

        /// <summary>
        /// Final KPI Angle
        /// </summary>
        public Angle KPI;
        #endregion

        #region -Direct Values-
        public double RideHeight;
        #endregion

        #region -Charts-
        /// <summary>
        /// Final Bump Steer Chart
        /// </summary>
        public List<Angle> BumpSteerChart;
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

        #endregion

        #endregion


        #region ---Methods---

        #region --Constructors--
        public SetupChange_Outputs()
        {
            SetBaseConvergence();
        }

        public SetupChange_Outputs(VehicleCorner _vCorner)
        {
            Corner = _vCorner;

            Identifier = (int)Corner;

            ToeLinkInboard = new Point3D();

            SetBaseConvergence();
        }

        public SetupChange_Outputs(int _identifier)
        {
            Identifier = _identifier;

            Corner = (VehicleCorner)Identifier;

            ToeLinkInboard = new Point3D();

            SetBaseConvergence();
        }
        #endregion

        private void SetBaseConvergence()
        {
            Caster_Conv = new Convergence("NotRequested");

            KPI_Conv = new Convergence("NotRequested");

            Camber_Conv= new Convergence("NotRequested");

            Toe_Conv = new Convergence("NotRequested");

            BumpSteer_Conv= new Convergence("NotRequested");
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
        /// The goal is that, if a Setup Angle is requested then these angles below will be overridden by the <see cref="OptimizerGeneticAlgorithm"/>'s results and then later will be conditioned with the right orientation
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

            SolverMasterClass.AssignOrientation_CamberToe(ref tempCamber, ref tempToe, tempCamber, tempToe, Identifier);

            Camber = new Angle(tempCamber, AngleUnit.Degrees);

            Toe = new Angle(tempToe, AngleUnit.Degrees);

            Caster = -_fCaster;

            SolverMasterClass.AssignDirection_KPI(Identifier, ref tempKPI);

            KPI = new Angle(tempKPI, AngleUnit.Degrees);
        }

        /// <summary>
        /// Method to assign the Charts
        /// </summary>
        /// <param name="_bumpSteerChart"></param>
        public void SetCharts(List<Angle> _bumpSteerChart)
        {
            BumpSteerChart = _bumpSteerChart;
        }

        /// <summary>
        /// Method to assign the Pick-Up Points
        /// </summary>
        /// <param name="_toeLinkInboard"></param>
        public void SetPickUpPonts(Point3D _toeLinkInboard)
        {
            ToeLinkInboard = _toeLinkInboard;
        } 
        #endregion

    }

    /// <summary>
    /// Class to hold information regarding the Convergance of a Setup Paras
    /// </summary>
    public struct Convergence
    {
        /// <summary>
        /// Percentage of Convergence. Between 0 and 100
        /// </summary>
        public double Percentage{ get; set; }

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

            Percentage = Ratio * 100;

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

}

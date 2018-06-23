using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;
using devDept.Eyeshot.Entities;



namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This Class houses the Components assembled onto the Vehicle which connect 2 corners\
    /// This would include the Anti-Roll Bar
    /// </summary>
    public class KO_CentralVariables
    {
        /// <summary>
        /// Vehicle item onto which the Suspension Coordinates will be assembled
        /// </summary>
        public Vehicle Vehicle;

        /// <summary>
        /// Wheelbase of the Vehicle as input by the User
        /// </summary>
        public double WheelBase { get; set; }

        /// <summary>
        /// Front Track of the Vehicle as input by the user
        /// </summary>
        public double Track_Front { get; set; }

        /// <summary>
        /// Rear Track of the Vehicle as input by the user
        /// </summary>
        public double Track_Rear { get; set; }

        /// <summary>
        /// <para><see cref="Point3D"/> representing the Front Roll Center Position</para>
        /// <para>Usually this would represent only a RC Height as for the Front, the lateral and longitudinal coordinates are usually 0</para>
        /// </summary>
        public Point3D RC_Front { get; set; }

        /// <summary>
        /// <para><see cref="Point3D"/> representing the Rear Roll Center Position</para>
        /// <para>Usually this would represent a Longitudinal and Vertical coordinates as the RC is usually symmetric </para>
        /// </summary>
        public Point3D RC_Rear { get; set; }

        /// <summary>
        /// <see cref="Point3D"/> representing the Left Pitch Center
        /// </summary>
        public Point3D PC_Left { get; set; }

        /// <summary>
        /// <see cref="Point3D"/> representing the Right Pitch Center
        /// </summary>
        public Point3D PC_Right { get; set; } 

        /// <summary>
        /// Ackermann given as a percentage of the Wheelbase
        /// </summary>
        public double Ackermann { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public KO_CentralVariables()
        {
            Vehicle = new Vehicle();

            RC_Front = new Point3D();

            RC_Rear = new Point3D();

            PC_Left = new Point3D();

            PC_Right = new Point3D();
        }


        /// <summary>
        /// Method to Initialize the Front Roll Center in 3D
        /// </summary>
        /// <param name="_front_RC_Height">Front Roll Center Height as input by the User</param>
        /// <param name="_front_RC_LatOffset">Front Roll Center Lateral Offset as input by the User for Assymmetric Suspension</param>
        public void Initialize_RC_Front(double _front_RC_Height, double _front_RC_LatOffset)
        {
            RC_Front.X = _front_RC_LatOffset;

            RC_Front.Y = _front_RC_Height;

            RC_Front.Z = 0;
        }


        /// <summary>
        /// Method to Initialize the Rear Roll Center in 3D
        /// </summary>
        /// <param name="_rear_RC_Height">Rear Roll Center Height as input by the User</param>
        /// <param name="_rear_RC_LatOffset">Rear Roll Center Lateral Offset as input by the User for Assymmetric Suspension</param>
        /// <param name="_wheelBase"></param>
        public void Initialize_RC_Rear(double _rear_RC_Height, double _rear_RC_LatOffset, double _wheelBase)
        {
            RC_Rear.X = _rear_RC_LatOffset;

            RC_Rear.Y = _rear_RC_Height;

            RC_Rear.Z = _wheelBase;
        }


        /// <summary>
        /// Method to Initialize the LEFT AND RIGHT Pitch Centers assuming a Symmetric Suspension
        /// </summary>
        /// <param name="_left_PC_Height">Pitch Center Height</param>
        /// <param name="_left_PC_LongOffset">Pitch Center Longitudinal Offset</param>
        /// <param name="_frontTrack">Front Track Width</param>
        /// <param name="_rearTrack">Rear Track Width</param>
        public void Initialize_PC_Symmetric(double _left_PC_Height, double _left_PC_LongOffset, double _frontTrack, double _rearTrack)
        {
            PC_Left.X = (_frontTrack /*+ _rearTrack*/) / 2;

            PC_Left.Y = _left_PC_Height;

            PC_Left.Z = _left_PC_LongOffset;


            PC_Right.X = -(_frontTrack /*+ _rearTrack*/) / 2;

            PC_Right.Y = _left_PC_Height;

            PC_Right.Z = _left_PC_LongOffset;
        }


        /// <summary>
        /// Method to Initialize the LEFT Pitch Center for Assymmetric Suspension
        /// </summary>
        /// <param name="_left_PC_Height">Left Pitch Center Height</param>
        /// <param name="_left_PC_LongOffset">Left Pitch Center Longitudinal Offset</param>
        /// <param name="_frontTrack">Front Track Width</param>
        /// <param name="_rearTrack">Rear Track Width</param>
        public void Initialize_PC_Left(double _left_PC_Height, double _left_PC_LongOffset, double _frontTrack, double _rearTrack)
        {
            PC_Left.X = (_frontTrack + _rearTrack) / 2;

            PC_Left.Y = _left_PC_Height;

            PC_Left.Z = _left_PC_LongOffset;
        }


        /// <summary>
        /// Method to Initialize the RIGHT Pitch Center for Assymmetric Suspension
        /// </summary>
        /// <param name="_right_PC_Height">Right Pitch Center Height</param>
        /// <param name="_right_PC_LongOffset">Right Pitch Center Longitudinal Offset</param>
        /// <param name="_frontTrack">Front Track Width</param>
        /// <param name="_rearTrack">Rear Track Width</param>
        public void Initialize_PC_Right(double _right_PC_Height, double _right_PC_LongOffset, double _frontTrack, double _rearTrack)
        {
            PC_Right.X = -(_frontTrack + _rearTrack) / 2;

            PC_Right.Y = _right_PC_Height;

            PC_Right.Z = _right_PC_LongOffset;

        }


        /// <summary>
        /// <para>Placed inside <see cref="KO_CentralVariables"/> because we need to use literall all the 4 <see cref="KO_CornverVariables"/> to compute the Outboard Toe Link Points</para>
        /// <para>Method to Compute the Outboard Toe Link Points using the Ackermann and Pitman Arm Length</para>
        /// </summary>
        /// <param name="_kocvFL"><see cref="KO_CornverVariables"/> object of the Front Left</param>
        /// <param name="_kocvFR"><see cref="KO_CornverVariables"/> object of the Front Right</param>
        /// <param name="_kocvRL"><see cref="KO_CornverVariables"/> object of the Rear Left</param>
        /// <param name="_kocvRR"><see cref="KO_CornverVariables"/> object of the Rear Right</param>
        public void Compute_OutboardToeLink(ref KO_CornverVariables _kocvFL, ref KO_CornverVariables _kocvFR, ref KO_CornverVariables _kocvRL, ref KO_CornverVariables _kocvRR)
        {
            ///<summary>Translating the Ackermann Percentage to a Line poostion based on the Wheelbase</summary>
            double ackermannLinePos = (Ackermann / 100) * WheelBase;


            ///<summary>Computing the Rear Axle Line (approximating that the Rear Axle and Wheelbase is at the same position) </summary>
            Line rearAxleLine = new Line(_kocvRL.VCornerParams.WheelCenter, _kocvRR.VCornerParams.WheelCenter);


            ///<summary>Computing the Ackermann Line</summary>
            Line ackermannLine = new Line(_kocvRL.VCornerParams.WheelCenter.Clone() as Point3D, _kocvRR.VCornerParams.WheelCenter.Clone() as Point3D);
            ackermannLine.Translate(0, 0, -(WheelBase - ackermannLinePos));


            ///Creating the Pitman Lines of the Left and Right
            Line pitmanLine_Left = new Line(ackermannLine.MidPoint, _kocvFL.VCornerParams.LBJ);
            Line pitmanLine_Right = new Line(ackermannLine.MidPoint, _kocvFR.VCornerParams.LBJ);


            ///<summary>Computing the Pitman Vector and its corresponding Unit Vector</summary>
            Vector3D pitmanLineVector_Left = new Vector3D(pitmanLine_Left.StartPoint, pitmanLine_Left.EndPoint);
            pitmanLineVector_Left.Normalize();
            Vector3D pitmanLineVector_Right = new Vector3D(pitmanLine_Right.StartPoint, pitmanLine_Right.EndPoint);
            pitmanLineVector_Right.Normalize();


            ///<summary>Computing the Outboard Toe Link Point based on the Pitman Arm Lenght of the Left and Right</summary
            _kocvFL.VCornerParams.ToeLinkOutboard = _kocvFL.VCornerParams.LBJ + (_kocvFL.PitmanTrail * pitmanLineVector_Left);
            _kocvFR.VCornerParams.ToeLinkOutboard = _kocvFR.VCornerParams.LBJ + (_kocvFR.PitmanTrail * pitmanLineVector_Right);


        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding_Attempt_with_GUI
{

    ///<summary
    ///
    /// This is the solver method which is used purely calculate the Kinematic Points of Interest as they move
    ///
    /// The Code below makes use of the coeffecients of the three variables x,y and z. The final quadratic equation is developed by manipulating the coefficients of the variables. The manipulation follows the course
    /// of the Vector Geometry Analysis which is explained in the Documentation
    ///
    /// S is the vector which is moving and hence is represented as S'. T, U and V may be moveable (and hence KNOWN) or non moveable points.
    /// They are not represented with a dash.
    /// Strut Deflection has been passed ONLY to handle the calculation of the lower end of the damper in case of McPherson Strut
    /// Magnitude and Coordinates of Vector S'T = ST 
    ///
    ///</summary>
    public static class QuadraticEquationSolver
    {
        #region Solver

        //Solver 

        public static SimulationType simType;

        public static double WishboneLengthChange = 0;

        public static double ToeLinkLengthChange = 0;

        public static void Solver(double Sx, double Sy, double Sz, double T1x, double T1y, double T1z, double StrutDeflection, double U1x, double U1y, double U1z, double V1x, double V1y, double V1z, double T2x, double T2y, double T2z, double U2x, double U2y, double U2z, 
                                  double V2x, double V2y, double V2z, double check1Y, bool GreaterThan, out double X, out double Y, out double Z )
        {
            double X1, Y1, Z1, X2, Y2, Z2;

            X = 0; Y = 0; Z = 0;
            double ST; // '2' indicates that the coordinates are new or changed and hence only can be applied to the movable points
            if (StrutDeflection == 0)
            { ST = Math.Sqrt(Math.Pow((Sx - T1x), 2) + (Math.Pow((Sy - T1y), 2)) + (Math.Pow((Sz - T1z), 2))); }
            else
            { ST = StrutDeflection; }
            //Equation A --> ALHS = (Sx^2+ (ACSx)Sx) + (Sy^2 + (ACSy)Sy) + (Sz^2 + (ACSz)Sz)
            double ASLHS, ACSx, ACSy, ACSz; // Named like this because S is the movable point we are calculating - Refere Nomenclature
            ASLHS = ((Math.Pow(ST, 2)) - ((Math.Pow(T2x, 2)) + (Math.Pow(T2y, 2)) + (Math.Pow(T2z, 2)))); // Eq A's LHS
            ACSx = 2 * (-T2x); //Coefficient of Sx in Eq A
            ACSy = 2 * (-T2y); //Coefficient of Sy in Eq A
            ACSz = 2 * (-T2z); //Coefficient of Sz in Eq A


            //Magnitudeand and Coordinates of Vector S'U = SU
            double SU;
            SU = Math.Sqrt(Math.Pow((Sx - U1x), 2) + (Math.Pow((Sy - U1y), 2)) + (Math.Pow((Sz - U1z), 2)));
            SU = SU + WishboneLengthChange;
            //Equation B --> BLHS = (Sx^2+ (BCSx)Sx) + (Sy^2 + (BCSy)Sy) + (Sz^2 + (BCSz)Sz)
            double BSLHS, BCSx, BCSy, BCSz;
            BSLHS = ((Math.Pow(SU, 2)) - ((Math.Pow(U2x, 2)) + (Math.Pow(U2y, 2)) + (Math.Pow(U2z, 2)))); //Eq B's LHS
            BCSx = 2 * (-U2x); // Coeffecient of Sx in Eq B
            BCSy = 2 * (-U2y); // Coeffecient of Sy in Eq B
            BCSz = 2 * (-U2z); // Coeffecient of Sz in Eq B

            //Magnitude and Cordinates of Vector S'V = SV
            double SV;
            SV = Math.Sqrt(Math.Pow((Sx - V1x), 2) + (Math.Pow((Sy - V1y), 2)) + (Math.Pow((Sz - V1z), 2)));
            SV = SV + ToeLinkLengthChange;
            //Equation C --> CLHS = (Sx^2+ (CCSx)Sx) + (Sy^2 + (CCSy)Sy) + (Sz^2 + (CCSz)Sz)
            double CSLHS, CCSx, CCSy, CCSz;
            CSLHS = ((Math.Pow(SV, 2)) - ((Math.Pow(V2x, 2)) + (Math.Pow(V2y, 2)) + (Math.Pow(V2z, 2)))); // Eq C's LHS
            CCSx = 2 * (-V2x); //Coefficient of Sx in Eq C
            CCSy = 2 * (-V2y); //Coefficient of Sy in Eq C
            CCSz = 2 * (-V2z); //Coefficient of Sz in Eq C

            //Below are 5 set of equations which wen executed will give the value of Y and Z purely in terms of X
            //Equation A-B --> AMBLHS = (AMBCSx)Sx + (AMBCSy)Sy + (AMBCSz)Sz
            double AMBSLHS, AMBCSx, AMBCSy, AMBCSz;
            AMBSLHS = ASLHS - BSLHS; // Equation A-B's LHS
            AMBCSx = ACSx - BCSx; // Coefficient of Sx in Eq A-B
            AMBCSy = ACSy - BCSy; // Coefficient of Sy in Eq A-B
            AMBCSz = ACSz - BCSz; // Coefficient of Sz in Eq A-B


            //Equation B-C --> BMCLHS = (BMCSx)Sx + (BMCSy)Sy + (BMCSz)Sz
            double BMCSLHS, BMCSx, BMCSy, BMCSz;
            BMCSLHS = BSLHS - CSLHS; // Equation B-C's LHS
            BMCSx = BCSx - CCSx; // Coefficient of Sx in Eq B-C 
            BMCSy = BCSy - CCSy; // Coefficient of Sy in Eq B-C
            BMCSz = BCSz - CCSz; // Coefficient of Sz in Eq B-C


            //Equation A-C --> AMCLHS = (AMCSx)Sx + (AMCSy)Sy + (AMCSz)Sz
            double AMCSLHS, AMCSx, AMCSy, AMCSz;
            AMCSLHS = ASLHS - CSLHS; // Equation A-C's LHS
            AMCSx = ACSx - CCSx; // Coefficient of Sx in Eq A-C 
            AMCSy = ACSy - CCSy; // Coefficient of Sy in Eq A-C 
            AMCSz = ACSz - CCSz; // Coefficient of sz in Eq A-C 


            //Equation III (Obtained from Equation A-C) // Obtaining everything in terms of Ey and Ex
            double IIISLHS, IIISx, IIISy, IIISz;
            IIISLHS = AMCSLHS / AMCSz;
            IIISx = AMCSx / (-AMCSz); // Minus sign to reflect the action of sending the coefficient to the LHS 
            IIISy = AMCSy / (-AMCSz); // Minus sign to reflect the action of sending the coefficient to the LHS
            IIISz = AMCSz / AMCSz;


            //Equation II (Obtained from Equation B-C) // Obtaining everything in terms of Ez and Ex 
            double IISLHS, IISx, IISy, IISz;
            IISLHS = BMCSLHS / (BMCSy);
            IISx = BMCSx / (-BMCSy); // Minus sign to reflect the action of sending the coefficient to the LHS 
            IISz = BMCSz / (-BMCSy); // Minus sign to reflect the action of sending the coefficient to the LHS
            IISy = BMCSy / BMCSy;


            //Equation IV (Substituting Ez in Equation II) // Obtaining everything in terms of only Ex
            double IVSLHS, IVSx, IVSy;
            IVSLHS = IIISLHS * IISz;
            IVSx = IIISx * IISz;
            IVSy = IIISy * IISz;
            IISy = IISy - IVSy;
            IISx = (IISx + IVSx) / IISy;
            double YIISx = IISx; // To find the final Y coordinate at the end 
            IISLHS = (IISLHS + IVSLHS) / IISy;
            double YIISLHS = IISLHS; // To find the final Y coordinate at the end 
            IISy = IISy / IISy; // Ey is now purely in terms of Ex


            //Equation V (Substituting Ey back into Ez's equation to get Ez in terms of purely Ex)
            double VSLHS, VSx;
            VSLHS = IIISy * IISLHS;
            VSx = IIISy * IISx;
            IIISLHS = IIISLHS + VSLHS;
            double ZIIISLHS = IIISLHS; // To find the final Z coordinate at the end 
            IIISx = IIISx + VSx; // Ez is now purely in terms of Ex
            double ZIIISx = IIISx; // To find the final Z coordinate at the end 


            //The Quadratic Equation
            double IISLHS_2, TwoIISLHSIISx, IISx_2, IIISLHS_2, TwoIIISLHSIIISx, IIISx_2;
            double SLHS, S_x_2, S_x; // Coefficients of the Quadratic Equation
            IISLHS_2 = Math.Pow(IISLHS, 2); // To reflect the formula (a+b)^2 = a^2 + 2ab + b^2
            TwoIISLHSIISx = 2 * IISLHS * IISx;
            IISx_2 = Math.Pow(IISx, 2);

            IIISLHS_2 = Math.Pow(IIISLHS, 2); // To reflect the formula (a+b)^2 = a^2 + 2ab + b^2 
            TwoIIISLHSIIISx = 2 * IIISLHS * IIISx;
            IIISx_2 = Math.Pow(IIISx, 2);

            IISLHS = IISLHS * ACSy;
            IISx = IISx * ACSy;

            IIISLHS = IIISLHS * ACSz;
            IIISx = IIISx * ACSz;

            SLHS = -((ASLHS) - (IISLHS_2) - (IIISLHS_2) - (IISLHS) - (IIISLHS));
            S_x_2 = 1 + (IISx_2) + (IIISx_2);
            S_x = (ACSx) + (TwoIISLHSIISx) + (TwoIIISLHSIIISx) + (IISx) + (IIISx);


            //The 2 roots of the Equation
            X1 = (((-S_x) + Math.Sqrt((Math.Pow(S_x, 2)) - (4 * S_x_2 * SLHS))) / (2 * S_x_2));
            X2 = (((-S_x) - Math.Sqrt((Math.Pow(S_x, 2)) - (4 * S_x_2 * SLHS))) / (2 * S_x_2));
            // Y and Z Coordinates
            Y1 = Y2 = Z2 = Z1 = 0; // If I don't initialize it here to 0 then "Unassigned variable error" 

            if (Sy <= 0) // This logic is to deal with the tires since the tires have a negative coordinate for y 
            {
                Y1 = YIISLHS + (YIISx * X1);
                Z1 = ZIIISLHS + (ZIIISx * X1);
                Y2 = YIISLHS + (YIISx * X2);
                Z2 = ZIIISLHS + (ZIIISx * X2);
            }

            // Basic Sanity Check for the coordinates - Valid for Left OR Right Corners and ONLY for BUMP
            else if (Sx < 0) //  Implies that it is one of the right corners of the vehicle 
            {
                if (X1 < 0)
                {
                    Y1 = YIISLHS + (YIISx * X1);
                    //if (((Y1 > 0) && (Y1 > Sy)) || ((Y1 > 0) && (Y1 < Sy))) // Implies 2 conditions necessary for Bump OR the 2 conditions necessary for heave
                    //{
                        Z1 = ZIIISLHS + (ZIIISx * X1);
                    //}
                    //else
                    //{
                    //    X1 = 0;
                    //    Y1 = 0;
                    //}
                }
                else
                    X1 = 0;

                if (X2 < 0)
                {
                    Y2 = YIISLHS + (YIISx * X2);
                    //if (((Y2 > 0) && (Y2 > Sy)) || ((Y2 > 0) && (Y2 < Sy))) // Implies 2 conditions necessary for Bump OR the 2 conditions necessary for heave
                    //{
                        Z2 = ZIIISLHS + (ZIIISx * X2);
                    //}
                    //else
                    //{
                    //    X2 = 0;
                    //    Y2 = 0;
                    //}
                }
                else
                    X2 = 0;
            }

            else if (Sx > 0) //  Implies that it is one of the left corners of the vehicle 
            {
                if (X1 > 0)
                {
                    Y1 = YIISLHS + (YIISx * X1);

                    //if (((Y1 > 0) && (Y1 > Sy)) || ((Y1 > 0) && (Y1 < Sy))) // Implies 2 conditions necessary for Bump OR the 2 conditions necessary for heave
                    //{
                        Z1 = ZIIISLHS + (ZIIISx * X1);
                    //}
                    //else
                    //{
                    //    X1 = 0;
                    //    Y1 = 0;
                    //}
                }
                else
                    X1 = 0;

                if (X2 > 0)
                {
                    Y2 = YIISLHS + (YIISx * X2);
                    //if (((Y2 > 0) && (Y2 > Sy)) || ((Y2 > 0) && (Y2 < Sy))) // Implies 2 conditions necessary for Bump OR the 2 conditions necessary for heave
                    //{
                         Z2 = ZIIISLHS + (ZIIISx * X2);
                    //}
                    //else
                    //{
                    //    X2 = 0;
                    //    Y2 = 0;
                    //}
                }
                else
                    X2 = 0;
            }

            SanityCheck(Sx, Sy, Sz, GreaterThan, check1Y, X1, Y1, Z1, X2, Y2, Z2, out X, out Y, out Z);

            if ((Double.IsNaN(X1) && Double.IsNaN(X2)) || (X1 == 0 && X2 == 0)) 
            {
                X = Sx;
            }
            if ((Double.IsNaN(Y1) && Double.IsNaN(Y2)) || (Y1 == 0 && Y2 == 0))
            {
                Y = Sy;
            }
            if ((Double.IsNaN(Z1) && Double.IsNaN(Z2)) || (Z1 == 0 && Z2 == 0))
            {
                Z = Sz;
            }
           


        }

        static void SanityCheck(double _sx, double _sy, double _sz, bool _greaterThan, double _check1Y, double _x1,double _y1, double _z1, double _x2, double _y2, double _z2, out double _x, out double _y, out double _z)
        {
            if (_sy >= 0)
            {
                if (_greaterThan)
                {
                    if (_y1 > _check1Y) { _x1 = 0; _y1 = 0; _z1 = 0; }
                    else if (_y2 > _check1Y) { _x2 = 0; _y2 = 0; _z2 = 0; }
                }
                else if (!_greaterThan)
                {
                    if (_y1 < _check1Y) { _x1 = 0; _y1 = 0; _z1 = 0; }
                    else if (_y2 < _check1Y) { _x2 = 0; _y2 = 0; _z2 = 0; }

                }
            }

            double Mag_sx1 = (Math.Sqrt(Math.Pow(_sx - _x1, 2) + Math.Pow(_sy - _y1, 2) + Math.Pow(_sz - _z1, 2)));
            double Mag_sx2 = (Math.Sqrt(Math.Pow(_sx - _x2, 2) + Math.Pow(_sy - _y2, 2) + Math.Pow(_sz - _z2, 2)));

            if (Mag_sx1 > Mag_sx2)
            {
                _x = _x2;
                _y = _y2;
                _z = _z2;
            }
            else
            {
                _x = _x1;
                _y = _y1;
                _z = _z1;
            }

            //if ((((Math.Abs(_x1 - _sx) > Math.Abs(_x2 - _sx)) && (Math.Abs(_y1 - _sy) > Math.Abs(_y2 - _sy))) || ((Math.Abs(_y1 - _sy) > Math.Abs(_y2 - _sy)) && ((Math.Abs(_x1 - _sx) > Math.Abs(_x2 - _sx)) && (Math.Abs(_y1 - _sy) > Math.Abs(_y2 - _sy))))) || ((Math.Abs(_x1 - _sx) > Math.Abs(_x2 - _sx)) && (Math.Abs(_z1 - _sz) > Math.Abs(_z2 - _sz))))
            //{
            //    _x = _x2;
            //    _y = _y2;
            //    _z = _z2;
            //}
            //else
            //{
            //    _x = _x1;
            //    _y = _y1;
            //    _z = _z1;
            //}
        }
        #endregion


    }
}

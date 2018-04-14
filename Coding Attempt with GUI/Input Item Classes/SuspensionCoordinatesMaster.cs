using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class SuspensionCoordinatesMaster
    {

        
        public string _SCName { get; set; }

        #region Auto implemented propeties for Suspension Type
        public int DoubleWishboneIdentifierFront { get; set; }
        public int McPhersonIdentifierFront { get; set; }
        public int DoubleWishboneIdentifierRear { get; set; }
        public int McPhersonIdentifierRear { get; set; }
        public int PushrodIdentifierFront { get; set; }
        public int PullrodIdentifierFront { get; set; }
        public int PushrodIdentifierRear { get; set; }
        public int PullrodIdentifierRear { get; set; }
        public int UARBIdentifierFront { get; set; }
        public int TARBIdentifierFront { get; set; }
        public int UARBIdentifierRear { get; set; }
        public int TARBIdentifierRear { get; set; }
        public int NoOfCouplings { get; set; }
        #endregion

        #region Auto implemented properties for Suspension Symmetry
        public bool FrontSymmetry { get; set; }
        public bool RearSymmetry { get; set; }
        #endregion

        #region Auto implemented property of Suspension Coordinate MotionExists
        public bool SuspensionMotionExists { get; set; }
        #endregion

        #region Auto implemented proerties for the Suspensions Input and OutputOrigins
        public double InputOriginX { get; set; }
        public double InputOriginY { get; set; }
        public double InputOriginZ { get; set; }
        #endregion

        #region Fixed Points Front Declaration
        public double A1x, A1y, A1z, B1x, B1y, B1z, C1x, C1y, C1z, D1x, D1y, D1z, JO1x, JO1y, JO1z, I1x, I1y, I1z, Q1x, Q1y, Q1z, N1x, N1y, N1z,
                      Pin1x, Pin1y, Pin1z, UV1x, UV1y, UV1z, UV2x, UV2y, UV2z, STC1x, STC1y, STC1z, R1x, R1y, R1z, RideHeightRefx, RideHeightRefy, RideHeightRefz;
        #endregion

        #region Moving Points Front Declaration
        public double J1x, J1y, J1z, H1x, H1y, H1z, O1x, O1y, O1z, G1x, G1y, G1z, F1x, F1y, F1z, E1x, E1y, E1z, M1x, M1y, M1z,L1x,L1y,L1z, K1x, K1y, K1z, P1x, P1y, P1z, W1x, W1y, W1z;
        #endregion

        #region Variables for InitialMotion Ratio Calculations
        //For Initial Motion Ratio Calculation
        public double J1I, H1I, G1AB, G1DC, F1AB, E1DC, alpha, ABx, ABy, DCx, DCy, G1H1_Perp, G1H1, P1Q, O1I, J1JO, J1JO_Perp;
        public double InitialMR, Initial_ARB_MR;
        #endregion

        #region Link Lengths Declaration
        public double LowerFrontLength, LowerRearLength, UpperFrontLength, UpperRearLength, PushRodLength, PushRodLength_1, ToeLinkLength, DamperLength, ARBDroopLinkLength, ARBBladeLength;
        #endregion

        #region List to store the Deflections due to Steering
        /// <summary>
        /// <Value>
        /// WheelDeflection_Steering represents the change in vertical postion of the Wheel Centre of the FRONT due to steering. This List is used only for the front and not for the rear. This is because, sense of wheel deflection due to steering is not as expected. Thas is 
        /// downward deflection results in increase in weight of the front
        /// </Value>
        /// </summary>
        public double[] WheelDeflection_Steering = new double[100];

        #region Confirm if this variable is necessary and delete if not
        ///// <summary>
        ///// <value>
        ///// WheelDeflection_DiagonalWT_Steering represents the change in vertical postion of the wheel centre of REAR due to steering. A seperate list is employed for this because, the wheel deflection in the rear is due to diagonal weight transfer and the sense is as expected
        ///// </value>
        ///// </summary>
        //public List<double> WheelDeflection_DiagonalWT_Steering = new List<double>(); 
        #endregion

        /// <summary>
        /// <value>
        /// Represents the Spring Deflection in the Front that is caused due to the static Steering
        /// </value>
        /// <remarks>
        /// A seperate List is necessary to store this because of the unusal relationship between the Spring and Wheel Deflection due to Steering. That is,as the wheel deflecs downwards with 
        /// respect to the chassis (because it lifts the chassis), the spring compresses too!.
        /// </remarks>
        /// </summary>
        public List<double> SpringDeflection_DeltSteering = new List<double>();
        #endregion

        /*This Master Class for the Suspension Coordinates is created so that 4 derived classes (1 each) for the Suspension Coordinates of each corner can be created. 
         These derived classes are then used to create the corresponding instances for the Front and Rear. The objects of these instances are then passed on to the Kinematics and KinematicMcPherson Functions*/

        #region Method to calculate the Intial Motion Ratio
        /// <summary>
        /// Method to calculate the Motion Ratio 
        /// </summary>
        /// <param name="McPhersonIdentifierFront"></param>
        /// <param name="McPhersonIdentifierRear"></param>
        /// <param name="PullRodIdentifierFront"></param>
        /// <param name="PullRodIdentifierRear"></param>
        /// <param name="Identifier"></param>
        /// <returns></returns>
        public double MotionRatio(int McPhersonIdentifierFront, int McPhersonIdentifierRear, int PullRodIdentifierFront, int PullRodIdentifierRear, int Identifier)
        {
            P1Q = Math.Sqrt(Math.Pow((P1y - Q1y), 2) + Math.Pow((P1z - Q1z), 2) + Math.Pow((P1x - Q1x), 2));
            O1I = Math.Sqrt(Math.Pow((O1x - I1x), 2) + Math.Pow((O1y - I1y), 2) + Math.Pow((O1z - I1z), 2));
            J1I = Math.Sqrt(Math.Pow((J1x - I1x), 2) + Math.Pow((J1y - I1y), 2)); // dC
            H1I = Math.Sqrt(Math.Pow((H1x - I1x), 2) + Math.Pow((H1y - I1y), 2)); // dD
            J1JO_Perp = Math.Abs(J1x - JO1x);
            J1JO = Math.Sqrt(Math.Pow((J1x - JO1x), 2) + Math.Pow((J1y - JO1y), 2));

            if (McPhersonIdentifierFront == 1 && ((Identifier == 1) || (Identifier == 2)))
            {
                alpha = Math.Asin(J1JO_Perp / J1JO);
                InitialMR = Math.Acos(alpha);
                Initial_ARB_MR = (O1I / P1Q) * (Math.Cos(alpha));
            }

            else if (McPhersonIdentifierRear == 1 && ((Identifier == 3) || (Identifier == 4)))
            {
                alpha = Math.Asin(J1JO_Perp / J1JO);
                InitialMR = Math.Acos(alpha);
                Initial_ARB_MR = (O1I / P1Q) * (Math.Cos(alpha));
            }

            else if ((PullRodIdentifierFront == 1) && ((Identifier == 1) || (Identifier == 2))) // Implies Pull Rod on Front
            {
                ABx = (A1x + B1x) / 2;
                ABy = (A1y + B1y) / 2;
                G1AB = Math.Sqrt(Math.Pow((G1x - ABx), 2) + Math.Pow((G1y - ABy), 2)); // dB
                F1AB = Math.Sqrt(Math.Pow((F1x - ABx), 2) + Math.Pow((F1y - ABy), 2)); // dA
                G1H1_Perp = Math.Abs(G1x - H1x);
                G1H1 = Math.Sqrt(Math.Pow((G1x - H1x), 2) + Math.Pow((G1y - H1y), 2));
                alpha = Math.Asin(G1H1_Perp / G1H1);
                InitialMR = ((J1I / H1I) * (G1AB / F1AB)) * (Math.Cos(alpha));
                Initial_ARB_MR = (O1I / P1Q) * (((G1AB / F1AB)) * (Math.Cos(alpha)));
            }

            else if ((PullRodIdentifierRear == 1) && ((Identifier == 3) || (Identifier == 4))) // Implies Pull Rod on Rear
            {
                ABx = (A1x + B1x) / 2;
                ABy = (A1y + B1y) / 2;
                G1AB = Math.Sqrt(Math.Pow((G1x - ABx), 2) + Math.Pow((G1y - ABy), 2)); // dB
                F1AB = Math.Sqrt(Math.Pow((F1x - ABx), 2) + Math.Pow((F1y - ABy), 2)); // dA
                G1H1_Perp = Math.Abs(G1x - H1x);
                G1H1 = Math.Sqrt(Math.Pow((G1x - H1x), 2) + Math.Pow((G1y - H1y), 2));
                alpha = Math.Asin(G1H1_Perp / G1H1);
                InitialMR = ((J1I / H1I) * (G1AB / F1AB)) * (Math.Cos(alpha));
                Initial_ARB_MR = (O1I / P1Q) * (((G1AB / F1AB)) * (Math.Cos(alpha)));
            }

            else if (G1y >= F1y) // For Pushrod on Upper Wishbone
            {
                ABx = (A1x + B1x) / 2;
                ABy = (A1y + B1y) / 2;
                G1AB = Math.Sqrt(Math.Pow((G1x - ABx), 2) + Math.Pow((G1y - ABy), 2)); // dB
                F1AB = Math.Sqrt(Math.Pow((F1x - ABx), 2) + Math.Pow((F1y - ABy), 2)); // dA
                G1H1_Perp = Math.Abs(G1x - H1x);
                G1H1 = Math.Sqrt(Math.Pow((G1x - H1x), 2) + Math.Pow((G1y - H1y), 2));
                alpha = Math.Asin(G1H1_Perp / G1H1);
                InitialMR = (Math.Pow((J1I/H1I),1) /** (Math.Pow((G1AB / F1AB),1))*/) / (Math.Cos(alpha));
                InitialMR = 1 / InitialMR;
                Initial_ARB_MR = (O1I / P1Q) * (((G1AB / F1AB)) * (Math.Cos(alpha)));
            }

            else if ((G1y >= E1y) && (G1y < F1y)) // For Pushrod on Lower Wishbone
            {
                DCx = (D1x + C1x) / 2;
                DCy = (D1y + C1y) / 2;
                G1DC = Math.Sqrt(Math.Pow((G1x - DCx), 2) + Math.Pow((G1y - DCy), 2)); // dB
                E1DC = Math.Sqrt(Math.Pow((E1x - DCx), 2) + Math.Pow((E1y - DCy), 2)); // dA
                G1H1_Perp = Math.Abs(G1x - H1x);
                G1H1 = Math.Sqrt(Math.Pow((G1x - H1x), 2) + Math.Pow((G1y - H1y), 2));
                alpha = Math.Asin(G1H1_Perp / G1H1);
                InitialMR = ((J1I / H1I) * (Math.Pow((G1DC / E1DC),1))) / (Math.Cos(alpha));
                InitialMR = 1 / InitialMR;
                Initial_ARB_MR = (O1I / P1Q) * (((G1DC / E1DC)) * (Math.Cos(alpha)));
            }

            return InitialMR;
        }
        #endregion


    }
}

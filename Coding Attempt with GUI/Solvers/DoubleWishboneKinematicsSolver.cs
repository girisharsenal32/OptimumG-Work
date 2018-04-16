using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Spatial.Euclidean;


namespace Coding_Attempt_with_GUI
{
    public class DoubleWishboneKinematicsSolver : SolverMasterClass
    {
        /// <summary
        /// This method is used to select the corect set of coordinates of the Kinematic Points of interest (using the Solver Method) as the car is dropped from the stands to the ground.
        /// The wheel and spring deflections are calculated and these are used to calculate all rotation of the bell-crank. Using the vectors of the bell-crank all the remaining points are calculated by using 
        /// the Solver Method
        /// The actual calculation of the points is carried out in the Solver Method 
        /// </summary>

        VehicleModel _modelDW = new VehicleModel();
        /// <summary>
        /// Tire Forces from the Load Case
        /// </summary>
        private double LatForce, LongForce, VerticalForce;
        /// <summary>
        /// Tire Moments from the Load Case
        /// </summary>
        private double Mx, Mz;

        ///// <summary>
        ///// Temp <see cref="BatchRunResults"/> object. This is to be used ONLY during a regular Simulation Run to enable coloring of Forces arrows in <see cref="devDept.Eyeshot"/>
        ///// </summary>
        //BatchRunResults singleLoadCaseResults = new BatchRunResults();

        #region Methods

        #region Final Motion Ratio
        private void CalculateFinalMotionRatio(OutputClass _ocMR, SuspensionCoordinatesMaster _scmMR, bool UseUBJ)
        {
            //
            // To calculate the final motion ratio
            //
            double ABx, ABy, ABz, DCx, DCy, DCz, O2I, P2Q, J2I, H2I, G2AB, G2DC, F2AB, E2DC, alpha2, G2H2_Perp, G2H2;
            P2Q = Math.Sqrt(Math.Pow((_scmMR.P1y - _scmMR.Q1y), 2) + Math.Pow((_scmMR.P1z - _scmMR.Q1z), 2) + Math.Pow((_scmMR.P1x - _scmMR.Q1x), 2));
            O2I = Math.Sqrt(Math.Pow((_scmMR.O1x - _scmMR.I1x), 2) + Math.Pow((_scmMR.O1y - _scmMR.I1y), 2) + Math.Pow((_scmMR.O1z - _scmMR.I1z), 2));

            // To Calculate the Final Motion Ratio if it is a Pull Rod System
            J2I = Math.Sqrt(Math.Pow((_ocMR.scmOP.J1x - l_I1x), 2) + Math.Pow((_ocMR.scmOP.J1y - l_I1y), 2) + Math.Pow((_ocMR.scmOP.J1z - l_I1z), 2)); // dC
            H2I = Math.Sqrt(Math.Pow((_ocMR.scmOP.H1x - l_I1x), 2) + Math.Pow((_ocMR.scmOP.H1y - l_I1y), 2) + Math.Pow((_ocMR.scmOP.H1z - l_I1z), 2)); // dD

            ABx = (l_A1x + l_B1x) / 2;
            ABy = (l_A1y + l_B1y) / 2;
            ABz = (l_A1z + l_B1z) / 2;
            DCx = (l_D1x + l_C1x) / 2;
            DCy = (l_D1y + l_C1y) / 2;
            DCz = (l_D1z + l_C1z) / 2;

            G2AB = Math.Sqrt(Math.Pow((_ocMR.scmOP.G1x - ABx), 2) + Math.Pow((_ocMR.scmOP.G1y - ABy), 2) /*+ Math.Pow((_ocMR.scmOP.G1z - ABz), 2)*/); // dB
            F2AB = Math.Sqrt(Math.Pow((_ocMR.scmOP.F1x - ABx), 2) + Math.Pow((_ocMR.scmOP.F1y - ABy), 2) /*+ Math.Pow((_ocMR.scmOP.F1z - ABz), 2)*/); // dA
            G2DC = Math.Sqrt(Math.Pow((_ocMR.scmOP.G1x - DCx), 2) + Math.Pow((_ocMR.scmOP.G1y - DCy), 2) /*+ Math.Pow((_ocMR.scmOP.G1z - DCz), 2)*/); // dB
            E2DC = Math.Sqrt(Math.Pow((_ocMR.scmOP.E1x - DCx), 2) + Math.Pow((_ocMR.scmOP.E1y - DCy), 2) /*+ Math.Pow((_ocMR.scmOP.E1z - DCz), 2)*/); // dA

            G2H2_Perp = Math.Abs(_ocMR.scmOP.G1x - _ocMR.scmOP.H1x);
            G2H2 = Math.Sqrt(Math.Pow((_ocMR.scmOP.G1x - _ocMR.scmOP.H1x), 2) + Math.Pow((_ocMR.scmOP.G1y - _ocMR.scmOP.H1y), 2));

            alpha2 = Math.Asin(G2H2_Perp / G2H2);

            if (UseUBJ)
            {
                _ocMR.FinalMR = ((J2I / H2I) /** (G2AB / F2AB)*/) / (Math.Cos(alpha2));
                _ocMR.Final_ARB_MR = (O2I / P2Q) * (((G2AB / F2AB)) * (Math.Cos(alpha2)));
                _ocMR.FinalMR = 1 / _ocMR.FinalMR;
            }

            else if (!UseUBJ)
            {
                _ocMR.FinalMR = ((J2I / H2I) * (G2DC / E2DC)) / (Math.Cos(alpha2));
                _ocMR.Final_ARB_MR = (O2I / P2Q) * (((G2DC / E2DC)) * (Math.Cos(alpha2)));
                _ocMR.FinalMR = 1 / _ocMR.FinalMR;
            }

        }
        #endregion

        #region Bell Crank Points

        #region Method to Initialize the Transformation Matrix
        private void InitializeRotationMatrices(int _identifier, OutputClass _ocInitRotationMatrix)
        {
            //if (J2Jo < J1Jo) // Implies that the new shock length is lower than the old length i.e., BUMP condition
            //{
            if (_identifier == 1 || _identifier == 3) // Implies that we are dealing with left corner of the vehicle 
            {
                _ocInitRotationMatrix.AngleOfRotation *= 1;
            }
            else if (_identifier == 2 || _identifier == 4) // Implies that we are dealing with right corner of the vehicle 
            {
                _ocInitRotationMatrix.AngleOfRotation *= -1;
            }
            //}
            //else if (J2Jo > J1Jo) // Implies that the new shock length is more than the old length i.e., DROOP condition
            //{
            //if (_identifier == 1 || _identifier == 3) // Implies that we are dealing with left corner of the vehicle 
            //{
            //    _ocInitRotationMatrix.AngleOfRotation *= -1;
            //}
            //else if (_identifier == 2 || _identifier == 4)
            //{
            //    _ocInitRotationMatrix.AngleOfRotation *= 1;
            //}
            //}

            //To Calculate the Rotation of the Vector IJ i.e., to calculate the coordinates of J', H' and O'
            // Currently the Z coordinates of the BellCrank are considered to be on a plane which is parallel to the Front Wheel Axle Plane 
            // A seperate rotation matrix to rotate the Bell Crank Vectors when they are not in the same plane will be implemented when the T-ARB code is implemented.
            //To calculate the direction cosines and hence the angles that the bell crank vectors make with the z axis. This is done so that the vectors can be first rotated about the Y axis and made perp to Z axis before rotation with Z axis





            Matrix_TranslationBC = new double[4, 4];
            Matrix_RotationXBC = new double[4, 4];
            Matrix_RotationYBC = new double[4, 4];
            Matrix_RotationZBC = new double[4, 4];
            Matrix_InverseRotYBC = new double[4, 4];
            Matrix_InverseRotXBC = new double[4, 4];
            Matrix_InverseTBC = new double[4, 4];
            Matrix_J = new double[4, 7];
            Matrix_H = new double[4, 7];
            Matrix_O = new double[4, 7];
            Matrix_AxisBC = new double[4, 1];
            Matrix_pseudo_AxisBC1 = new double[4, 1];
            Matrix_pseudo_AxisBC2 = new double[4, 1];
            Matrix_pseudo_AxisBC3 = new double[4, 1];


            for (i = 0; i < 4; i++) // To initialize all diagonal elements to 1 and remaining to 0
            {
                for (j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        Matrix_TranslationBC[i, j] = 1;
                        Matrix_RotationXBC[i, j] = 1;
                        Matrix_RotationYBC[i, j] = 1;
                        Matrix_RotationZBC[i, j] = 1;
                        Matrix_InverseRotYBC[i, j] = 1;
                        Matrix_InverseRotXBC[i, j] = 1;
                        Matrix_InverseTBC[i, j] = 1;
                    }
                    else
                    {
                        Matrix_TranslationBC[i, j] = 0;
                        Matrix_RotationXBC[i, j] = 0;
                        Matrix_RotationYBC[i, j] = 0;
                        Matrix_RotationZBC[i, j] = 0;
                        Matrix_InverseRotYBC[i, j] = 0;
                        Matrix_InverseRotXBC[i, j] = 0;
                        Matrix_InverseTBC[i, j] = 0;
                    }

                }
            }




            double H1I_x, H1I_y, H1I_z, O1I_x, O1I_y, O1I_z, J1I_x, J1I_y, J1I_z, theta_AxisBC_X, theta_AxisBC_Y;
            H1I_x = l_H1x - l_I1x; H1I_y = l_H1y - l_I1y; H1I_z = l_H1z - l_I1z; // Position vector of Pushrod point on Bell Crank
            J1I_x = l_J1x - l_I1x; J1I_y = l_J1y - l_I1y; J1I_z = l_J1z - l_I1z; // Position vector of Damper point on Bell Crank
            O1I_x = l_O1x - l_I1x; O1I_y = l_O1y - l_I1y; O1I_z = l_O1z - l_I1z; // Position vector of ARB point on Bell Crank 

            if ((_identifier == 1) || (_identifier == 3))
            {
                Matrix_AxisBC[0, 0] = ((J1I_y * H1I_z) - (J1I_z * H1I_y)) + l_I1x;
                Matrix_AxisBC[1, 0] = -((J1I_x * H1I_z) - (J1I_z * H1I_x)) + l_I1y;
                Matrix_AxisBC[2, 0] = ((J1I_x * H1I_y) - (J1I_y * H1I_x)) + l_I1z;
                Matrix_AxisBC[3, 0] = 1;
            }

            else if ((_identifier == 2) || (_identifier == 4))
            {
                Matrix_AxisBC[0, 0] = -((J1I_y * H1I_z) - (J1I_z * H1I_y)) + l_I1x;
                Matrix_AxisBC[1, 0] = ((J1I_x * H1I_z) - (J1I_z * H1I_x)) + l_I1y;
                Matrix_AxisBC[2, 0] = -((J1I_x * H1I_y) - (J1I_y * H1I_x)) + l_I1z;
                Matrix_AxisBC[3, 0] = 1;
            }


            Matrix_TranslationBC[0, 3] = -l_I1x; // To assign the required values to the required cells
            Matrix_TranslationBC[1, 3] = -l_I1y;
            Matrix_TranslationBC[2, 3] = -l_I1z;


            for (i = 0; i < 4; i++) //Translation Matrix 
            {
                for (k = 0; k < 4; k++)
                {
                    Matrix_pseudo_AxisBC1[i, 0] = Matrix_pseudo_AxisBC1[i, 0] + (Matrix_TranslationBC[i, k] * Matrix_AxisBC[k, 0]);

                }
            }

            theta_AxisBC_X = Math.Abs(Math.Atan(Matrix_pseudo_AxisBC1[1, 0] / Matrix_pseudo_AxisBC1[2, 0]));
            if (Matrix_AxisBC[1, 0] > l_I1y)
            {
                theta_AxisBC_X *= 1;
            }
            else if (Matrix_AxisBC[1, 0] < l_I1y) { theta_AxisBC_X *= -1; }
            Matrix_RotationXBC[1, 1] = Math.Cos(theta_AxisBC_X);
            Matrix_RotationXBC[1, 2] = -Math.Sin(theta_AxisBC_X);
            Matrix_RotationXBC[2, 1] = Math.Sin(theta_AxisBC_X);
            Matrix_RotationXBC[2, 2] = Math.Cos(theta_AxisBC_X);

            for (i = 0; i < 4; i++) // Translation x Rotation about X
            {
                for (k = 0; k < 4; k++)
                {
                    Matrix_pseudo_AxisBC2[i, 0] = Matrix_pseudo_AxisBC2[i, 0] + (Matrix_RotationXBC[i, k] * Matrix_pseudo_AxisBC1[k, 0]);
                }

            }

            theta_AxisBC_Y = Math.Abs(Math.Atan(Matrix_pseudo_AxisBC2[0, 0] / Matrix_pseudo_AxisBC2[2, 0]));
            if (Matrix_AxisBC[0, 0] > l_I1x) { theta_AxisBC_Y *= 1; }
            else if (Matrix_AxisBC[0, 0] < l_I1x) { theta_AxisBC_Y *= -1; }
            Matrix_RotationYBC[0, 0] = Math.Cos(theta_AxisBC_Y);
            Matrix_RotationYBC[0, 2] = Math.Sin(theta_AxisBC_Y);
            Matrix_RotationYBC[2, 0] = -Math.Sin(theta_AxisBC_Y);
            Matrix_RotationYBC[2, 2] = Math.Cos(theta_AxisBC_Y);

            for (i = 0; i < 4; i++) // Translation x Rotation about X x Rotation about Y
            {
                for (k = 0; k < 4; k++)
                {
                    Matrix_pseudo_AxisBC3[i, 0] = Matrix_pseudo_AxisBC3[i, 0] + (Matrix_RotationYBC[i, k] * Matrix_pseudo_AxisBC2[k, 0]);
                }
            }

            //At this point we have successfully used the Bell Crank Axis to determine the Angles by which the Bell Crank Vectors need to be roated about the X and Y Axis to make them fall on a plane perpendicular to the Z axis. 

            Matrix_RotationZBC[0, 0] = Math.Cos(_ocInitRotationMatrix.AngleOfRotation);
            Matrix_RotationZBC[0, 1] = -Math.Sin(_ocInitRotationMatrix.AngleOfRotation);
            Matrix_RotationZBC[1, 0] = Math.Sin(_ocInitRotationMatrix.AngleOfRotation);
            Matrix_RotationZBC[1, 1] = Math.Cos(_ocInitRotationMatrix.AngleOfRotation);

            Matrix_InverseRotYBC[0, 0] = Math.Cos(-theta_AxisBC_Y);
            Matrix_InverseRotYBC[0, 2] = Math.Sin(-theta_AxisBC_Y);
            Matrix_InverseRotYBC[2, 0] = -Math.Sin(-theta_AxisBC_Y);
            Matrix_InverseRotYBC[2, 2] = Math.Cos(-theta_AxisBC_Y);

            Matrix_InverseRotXBC[1, 1] = Math.Cos(-theta_AxisBC_X);
            Matrix_InverseRotXBC[1, 2] = -Math.Sin(-theta_AxisBC_X);
            Matrix_InverseRotXBC[2, 1] = Math.Sin(-theta_AxisBC_X);
            Matrix_InverseRotXBC[2, 2] = Math.Cos(-theta_AxisBC_X);

            Matrix_InverseTBC[0, 3] = l_I1x;
            Matrix_InverseTBC[1, 3] = l_I1y;
            Matrix_InverseTBC[2, 3] = l_I1z;
        }
        #endregion

        #region Calculating Bell Crank Points
        private void BellCrankPoints(OutputClass _ocMatrix, Point3D J1, Point3D H1, Point3D O1)
        {

            Matrix_J[0, 0] = ((Matrix_TranslationBC[0, 0] * J1.X) + (Matrix_TranslationBC[0, 1] * J1.Y) + (Matrix_TranslationBC[0, 2] * J1.Z) + (Matrix_TranslationBC[0, 3] * 1));
            Matrix_J[1, 0] = ((Matrix_TranslationBC[1, 0] * J1.X) + (Matrix_TranslationBC[1, 1] * J1.Y) + (Matrix_TranslationBC[1, 2] * J1.Z) + (Matrix_TranslationBC[1, 3] * 1));
            Matrix_J[2, 0] = ((Matrix_TranslationBC[2, 0] * J1.X) + (Matrix_TranslationBC[2, 1] * J1.Y) + (Matrix_TranslationBC[2, 2] * J1.Z) + (Matrix_TranslationBC[2, 3] * 1));
            Matrix_H[0, 0] = ((Matrix_TranslationBC[0, 0] * H1.X) + (Matrix_TranslationBC[0, 1] * H1.Y) + (Matrix_TranslationBC[0, 2] * H1.Z) + (Matrix_TranslationBC[0, 3] * 1));
            Matrix_H[1, 0] = ((Matrix_TranslationBC[1, 0] * H1.X) + (Matrix_TranslationBC[1, 1] * H1.Y) + (Matrix_TranslationBC[1, 2] * H1.Z) + (Matrix_TranslationBC[1, 3] * 1));
            Matrix_H[2, 0] = ((Matrix_TranslationBC[2, 0] * H1.X) + (Matrix_TranslationBC[2, 1] * H1.Y) + (Matrix_TranslationBC[2, 2] * H1.Z) + (Matrix_TranslationBC[2, 3] * 1));
            Matrix_O[0, 0] = ((Matrix_TranslationBC[0, 0] * O1.X) + (Matrix_TranslationBC[0, 1] * O1.Y) + (Matrix_TranslationBC[0, 2] * O1.Z) + (Matrix_TranslationBC[0, 3] * 1));
            Matrix_O[1, 0] = ((Matrix_TranslationBC[1, 0] * O1.X) + (Matrix_TranslationBC[1, 1] * O1.Y) + (Matrix_TranslationBC[1, 2] * O1.Z) + (Matrix_TranslationBC[1, 3] * 1));
            Matrix_O[2, 0] = ((Matrix_TranslationBC[2, 0] * O1.X) + (Matrix_TranslationBC[2, 1] * O1.Y) + (Matrix_TranslationBC[2, 2] * O1.Z) + (Matrix_TranslationBC[2, 3] * 1));

            Matrix_J[0, 1] = ((Matrix_RotationXBC[0, 0] * Matrix_J[0, 0]) + (Matrix_RotationXBC[0, 1] * Matrix_J[1, 0]) + (Matrix_RotationXBC[0, 2] * Matrix_J[2, 0]) + (Matrix_RotationXBC[0, 3] * 1));
            Matrix_J[1, 1] = ((Matrix_RotationXBC[1, 0] * Matrix_J[0, 0]) + (Matrix_RotationXBC[1, 1] * Matrix_J[1, 0]) + (Matrix_RotationXBC[1, 2] * Matrix_J[2, 0]) + (Matrix_RotationXBC[1, 3] * 1));
            Matrix_J[2, 1] = ((Matrix_RotationXBC[2, 0] * Matrix_J[0, 0]) + (Matrix_RotationXBC[2, 1] * Matrix_J[1, 0]) + (Matrix_RotationXBC[2, 2] * Matrix_J[2, 0]) + (Matrix_RotationXBC[2, 3] * 1));
            Matrix_H[0, 1] = ((Matrix_RotationXBC[0, 0] * Matrix_H[0, 0]) + (Matrix_RotationXBC[0, 1] * Matrix_H[1, 0]) + (Matrix_RotationXBC[0, 2] * Matrix_H[2, 0]) + (Matrix_RotationXBC[0, 3] * 1));
            Matrix_H[1, 1] = ((Matrix_RotationXBC[1, 0] * Matrix_H[0, 0]) + (Matrix_RotationXBC[1, 1] * Matrix_H[1, 0]) + (Matrix_RotationXBC[1, 2] * Matrix_H[2, 0]) + (Matrix_RotationXBC[1, 3] * 1));
            Matrix_H[2, 1] = ((Matrix_RotationXBC[2, 0] * Matrix_H[0, 0]) + (Matrix_RotationXBC[2, 1] * Matrix_H[1, 0]) + (Matrix_RotationXBC[2, 2] * Matrix_H[2, 0]) + (Matrix_RotationXBC[2, 3] * 1));
            Matrix_O[0, 1] = ((Matrix_RotationXBC[0, 0] * Matrix_O[0, 0]) + (Matrix_RotationXBC[0, 1] * Matrix_O[1, 0]) + (Matrix_RotationXBC[0, 2] * Matrix_O[2, 0]) + (Matrix_RotationXBC[0, 3] * 1));
            Matrix_O[1, 1] = ((Matrix_RotationXBC[1, 0] * Matrix_O[0, 0]) + (Matrix_RotationXBC[1, 1] * Matrix_O[1, 0]) + (Matrix_RotationXBC[1, 2] * Matrix_O[2, 0]) + (Matrix_RotationXBC[1, 3] * 1));
            Matrix_O[2, 1] = ((Matrix_RotationXBC[2, 0] * Matrix_O[0, 0]) + (Matrix_RotationXBC[2, 1] * Matrix_O[1, 0]) + (Matrix_RotationXBC[2, 2] * Matrix_O[2, 0]) + (Matrix_RotationXBC[2, 3] * 1));

            Matrix_J[0, 2] = ((Matrix_RotationYBC[0, 0] * Matrix_J[0, 1]) + (Matrix_RotationYBC[0, 1] * Matrix_J[1, 1]) + (Matrix_RotationYBC[0, 2] * Matrix_J[2, 1]) + (Matrix_RotationYBC[0, 3] * 1));
            Matrix_J[1, 2] = ((Matrix_RotationYBC[1, 0] * Matrix_J[0, 1]) + (Matrix_RotationYBC[1, 1] * Matrix_J[1, 1]) + (Matrix_RotationYBC[1, 2] * Matrix_J[2, 1]) + (Matrix_RotationYBC[1, 3] * 1));
            Matrix_J[2, 2] = ((Matrix_RotationYBC[2, 0] * Matrix_J[0, 1]) + (Matrix_RotationYBC[2, 1] * Matrix_J[1, 1]) + (Matrix_RotationYBC[2, 2] * Matrix_J[2, 1]) + (Matrix_RotationYBC[2, 3] * 1));
            Matrix_H[0, 2] = ((Matrix_RotationYBC[0, 0] * Matrix_H[0, 1]) + (Matrix_RotationYBC[0, 1] * Matrix_H[1, 1]) + (Matrix_RotationYBC[0, 2] * Matrix_H[2, 1]) + (Matrix_RotationYBC[0, 3] * 1));
            Matrix_H[1, 2] = ((Matrix_RotationYBC[1, 0] * Matrix_H[0, 1]) + (Matrix_RotationYBC[1, 1] * Matrix_H[1, 1]) + (Matrix_RotationYBC[1, 2] * Matrix_H[2, 1]) + (Matrix_RotationYBC[1, 3] * 1));
            Matrix_H[2, 2] = ((Matrix_RotationYBC[2, 0] * Matrix_H[0, 1]) + (Matrix_RotationYBC[2, 1] * Matrix_H[1, 1]) + (Matrix_RotationYBC[2, 2] * Matrix_H[2, 1]) + (Matrix_RotationYBC[2, 3] * 1));
            Matrix_O[0, 2] = ((Matrix_RotationYBC[0, 0] * Matrix_O[0, 1]) + (Matrix_RotationYBC[0, 1] * Matrix_O[1, 1]) + (Matrix_RotationYBC[0, 2] * Matrix_O[2, 1]) + (Matrix_RotationYBC[0, 3] * 1));
            Matrix_O[1, 2] = ((Matrix_RotationYBC[1, 0] * Matrix_O[0, 1]) + (Matrix_RotationYBC[1, 1] * Matrix_O[1, 1]) + (Matrix_RotationYBC[1, 2] * Matrix_O[2, 1]) + (Matrix_RotationYBC[1, 3] * 1));
            Matrix_O[2, 2] = ((Matrix_RotationYBC[2, 0] * Matrix_O[0, 1]) + (Matrix_RotationYBC[2, 1] * Matrix_O[1, 1]) + (Matrix_RotationYBC[2, 2] * Matrix_O[2, 1]) + (Matrix_RotationYBC[2, 3] * 1));

            Matrix_J[0, 3] = ((Matrix_RotationZBC[0, 0] * Matrix_J[0, 2]) + (Matrix_RotationZBC[0, 1] * Matrix_J[1, 2]) + (Matrix_RotationZBC[0, 2] * Matrix_J[2, 2]) + (Matrix_RotationZBC[0, 3] * 1));
            Matrix_J[1, 3] = ((Matrix_RotationZBC[1, 0] * Matrix_J[0, 2]) + (Matrix_RotationZBC[1, 1] * Matrix_J[1, 2]) + (Matrix_RotationZBC[1, 2] * Matrix_J[2, 2]) + (Matrix_RotationZBC[1, 3] * 1));
            Matrix_J[2, 3] = ((Matrix_RotationZBC[2, 0] * Matrix_J[0, 2]) + (Matrix_RotationZBC[2, 1] * Matrix_J[1, 2]) + (Matrix_RotationZBC[2, 2] * Matrix_J[2, 2]) + (Matrix_RotationZBC[2, 3] * 1));
            Matrix_H[0, 3] = ((Matrix_RotationZBC[0, 0] * Matrix_H[0, 2]) + (Matrix_RotationZBC[0, 1] * Matrix_H[1, 2]) + (Matrix_RotationZBC[0, 2] * Matrix_H[2, 2]) + (Matrix_RotationZBC[0, 3] * 1));
            Matrix_H[1, 3] = ((Matrix_RotationZBC[1, 0] * Matrix_H[0, 2]) + (Matrix_RotationZBC[1, 1] * Matrix_H[1, 2]) + (Matrix_RotationZBC[1, 2] * Matrix_H[2, 2]) + (Matrix_RotationZBC[1, 3] * 1));
            Matrix_H[2, 3] = ((Matrix_RotationZBC[2, 0] * Matrix_H[0, 2]) + (Matrix_RotationZBC[2, 1] * Matrix_H[1, 2]) + (Matrix_RotationZBC[2, 2] * Matrix_H[2, 2]) + (Matrix_RotationZBC[2, 3] * 1));
            Matrix_O[0, 3] = ((Matrix_RotationZBC[0, 0] * Matrix_O[0, 2]) + (Matrix_RotationZBC[0, 1] * Matrix_O[1, 2]) + (Matrix_RotationZBC[0, 2] * Matrix_O[2, 2]) + (Matrix_RotationZBC[0, 3] * 1));
            Matrix_O[1, 3] = ((Matrix_RotationZBC[1, 0] * Matrix_O[0, 2]) + (Matrix_RotationZBC[1, 1] * Matrix_O[1, 2]) + (Matrix_RotationZBC[1, 2] * Matrix_O[2, 2]) + (Matrix_RotationZBC[1, 3] * 1));
            Matrix_O[2, 3] = ((Matrix_RotationZBC[2, 0] * Matrix_O[0, 2]) + (Matrix_RotationZBC[2, 1] * Matrix_O[1, 2]) + (Matrix_RotationZBC[2, 2] * Matrix_O[2, 2]) + (Matrix_RotationZBC[2, 3] * 1));

            Matrix_J[0, 4] = ((Matrix_InverseRotYBC[0, 0] * Matrix_J[0, 3]) + (Matrix_InverseRotYBC[0, 1] * Matrix_J[1, 3]) + (Matrix_InverseRotYBC[0, 2] * Matrix_J[2, 3]) + (Matrix_InverseRotYBC[0, 3] * 1));
            Matrix_J[1, 4] = ((Matrix_InverseRotYBC[1, 0] * Matrix_J[0, 3]) + (Matrix_InverseRotYBC[1, 1] * Matrix_J[1, 3]) + (Matrix_InverseRotYBC[1, 2] * Matrix_J[2, 3]) + (Matrix_InverseRotYBC[1, 3] * 1));
            Matrix_J[2, 4] = ((Matrix_InverseRotYBC[2, 0] * Matrix_J[0, 3]) + (Matrix_InverseRotYBC[2, 1] * Matrix_J[1, 3]) + (Matrix_InverseRotYBC[2, 2] * Matrix_J[2, 3]) + (Matrix_InverseRotYBC[2, 3] * 1));
            Matrix_H[0, 4] = ((Matrix_InverseRotYBC[0, 0] * Matrix_H[0, 3]) + (Matrix_InverseRotYBC[0, 1] * Matrix_H[1, 3]) + (Matrix_InverseRotYBC[0, 2] * Matrix_H[2, 3]) + (Matrix_InverseRotYBC[0, 3] * 1));
            Matrix_H[1, 4] = ((Matrix_InverseRotYBC[1, 0] * Matrix_H[0, 3]) + (Matrix_InverseRotYBC[1, 1] * Matrix_H[1, 3]) + (Matrix_InverseRotYBC[1, 2] * Matrix_H[2, 3]) + (Matrix_InverseRotYBC[1, 3] * 1));
            Matrix_H[2, 4] = ((Matrix_InverseRotYBC[2, 0] * Matrix_H[0, 3]) + (Matrix_InverseRotYBC[2, 1] * Matrix_H[1, 3]) + (Matrix_InverseRotYBC[2, 2] * Matrix_H[2, 3]) + (Matrix_InverseRotYBC[2, 3] * 1));
            Matrix_O[0, 4] = ((Matrix_InverseRotYBC[0, 0] * Matrix_O[0, 3]) + (Matrix_InverseRotYBC[0, 1] * Matrix_O[1, 3]) + (Matrix_InverseRotYBC[0, 2] * Matrix_O[2, 3]) + (Matrix_InverseRotYBC[0, 3] * 1));
            Matrix_O[1, 4] = ((Matrix_InverseRotYBC[1, 0] * Matrix_O[0, 3]) + (Matrix_InverseRotYBC[1, 1] * Matrix_O[1, 3]) + (Matrix_InverseRotYBC[1, 2] * Matrix_O[2, 3]) + (Matrix_InverseRotYBC[1, 3] * 1));
            Matrix_O[2, 4] = ((Matrix_InverseRotYBC[2, 0] * Matrix_O[0, 3]) + (Matrix_InverseRotYBC[2, 1] * Matrix_O[1, 3]) + (Matrix_InverseRotYBC[2, 2] * Matrix_O[2, 3]) + (Matrix_InverseRotYBC[2, 3] * 1));

            Matrix_J[0, 5] = ((Matrix_InverseRotXBC[0, 0] * Matrix_J[0, 4]) + (Matrix_InverseRotXBC[0, 1] * Matrix_J[1, 4]) + (Matrix_InverseRotXBC[0, 2] * Matrix_J[2, 4]) + (Matrix_InverseRotXBC[0, 3] * 1));
            Matrix_J[1, 5] = ((Matrix_InverseRotXBC[1, 0] * Matrix_J[0, 4]) + (Matrix_InverseRotXBC[1, 1] * Matrix_J[1, 4]) + (Matrix_InverseRotXBC[1, 2] * Matrix_J[2, 4]) + (Matrix_InverseRotXBC[1, 3] * 1));
            Matrix_J[2, 5] = ((Matrix_InverseRotXBC[2, 0] * Matrix_J[0, 4]) + (Matrix_InverseRotXBC[2, 1] * Matrix_J[1, 4]) + (Matrix_InverseRotXBC[2, 2] * Matrix_J[2, 4]) + (Matrix_InverseRotXBC[2, 3] * 1));
            Matrix_H[0, 5] = ((Matrix_InverseRotXBC[0, 0] * Matrix_H[0, 4]) + (Matrix_InverseRotXBC[0, 1] * Matrix_H[1, 4]) + (Matrix_InverseRotXBC[0, 2] * Matrix_H[2, 4]) + (Matrix_InverseRotXBC[0, 3] * 1));
            Matrix_H[1, 5] = ((Matrix_InverseRotXBC[1, 0] * Matrix_H[0, 4]) + (Matrix_InverseRotXBC[1, 1] * Matrix_H[1, 4]) + (Matrix_InverseRotXBC[1, 2] * Matrix_H[2, 4]) + (Matrix_InverseRotXBC[1, 3] * 1));
            Matrix_H[2, 5] = ((Matrix_InverseRotXBC[2, 0] * Matrix_H[0, 4]) + (Matrix_InverseRotXBC[2, 1] * Matrix_H[1, 4]) + (Matrix_InverseRotXBC[2, 2] * Matrix_H[2, 4]) + (Matrix_InverseRotXBC[2, 3] * 1));
            Matrix_O[0, 5] = ((Matrix_InverseRotXBC[0, 0] * Matrix_O[0, 4]) + (Matrix_InverseRotXBC[0, 1] * Matrix_O[1, 4]) + (Matrix_InverseRotXBC[0, 2] * Matrix_O[2, 4]) + (Matrix_InverseRotXBC[0, 3] * 1));
            Matrix_O[1, 5] = ((Matrix_InverseRotXBC[1, 0] * Matrix_O[0, 4]) + (Matrix_InverseRotXBC[1, 1] * Matrix_O[1, 4]) + (Matrix_InverseRotXBC[1, 2] * Matrix_O[2, 4]) + (Matrix_InverseRotXBC[1, 3] * 1));
            Matrix_O[2, 5] = ((Matrix_InverseRotXBC[2, 0] * Matrix_O[0, 4]) + (Matrix_InverseRotXBC[2, 1] * Matrix_O[1, 4]) + (Matrix_InverseRotXBC[2, 2] * Matrix_O[2, 4]) + (Matrix_InverseRotXBC[2, 3] * 1));

            Matrix_J[0, 6] = ((Matrix_InverseTBC[0, 0] * Matrix_J[0, 5]) + (Matrix_InverseTBC[0, 1] * Matrix_J[1, 5]) + (Matrix_InverseTBC[0, 2] * Matrix_J[2, 5]) + (Matrix_InverseTBC[0, 3] * 1));
            Matrix_J[1, 6] = ((Matrix_InverseTBC[1, 0] * Matrix_J[0, 5]) + (Matrix_InverseTBC[1, 1] * Matrix_J[1, 5]) + (Matrix_InverseTBC[1, 2] * Matrix_J[2, 5]) + (Matrix_InverseTBC[1, 3] * 1));
            Matrix_J[2, 6] = ((Matrix_InverseTBC[2, 0] * Matrix_J[0, 5]) + (Matrix_InverseTBC[2, 1] * Matrix_J[1, 5]) + (Matrix_InverseTBC[2, 2] * Matrix_J[2, 5]) + (Matrix_InverseTBC[2, 3] * 1));
            Matrix_H[0, 6] = ((Matrix_InverseTBC[0, 0] * Matrix_H[0, 5]) + (Matrix_InverseTBC[0, 1] * Matrix_H[1, 5]) + (Matrix_InverseTBC[0, 2] * Matrix_H[2, 5]) + (Matrix_InverseTBC[0, 3] * 1));
            Matrix_H[1, 6] = ((Matrix_InverseTBC[1, 0] * Matrix_H[0, 5]) + (Matrix_InverseTBC[1, 1] * Matrix_H[1, 5]) + (Matrix_InverseTBC[1, 2] * Matrix_H[2, 5]) + (Matrix_InverseTBC[1, 3] * 1));
            Matrix_H[2, 6] = ((Matrix_InverseTBC[2, 0] * Matrix_H[0, 5]) + (Matrix_InverseTBC[2, 1] * Matrix_H[1, 5]) + (Matrix_InverseTBC[2, 2] * Matrix_H[2, 5]) + (Matrix_InverseTBC[2, 3] * 1));
            Matrix_O[0, 6] = ((Matrix_InverseTBC[0, 0] * Matrix_O[0, 5]) + (Matrix_InverseTBC[0, 1] * Matrix_O[1, 5]) + (Matrix_InverseTBC[0, 2] * Matrix_O[2, 5]) + (Matrix_InverseTBC[0, 3] * 1));
            Matrix_O[1, 6] = ((Matrix_InverseTBC[1, 0] * Matrix_O[0, 5]) + (Matrix_InverseTBC[1, 1] * Matrix_O[1, 5]) + (Matrix_InverseTBC[1, 2] * Matrix_O[2, 5]) + (Matrix_InverseTBC[1, 3] * 1));
            Matrix_O[2, 6] = ((Matrix_InverseTBC[2, 0] * Matrix_O[0, 5]) + (Matrix_InverseTBC[2, 1] * Matrix_O[1, 5]) + (Matrix_InverseTBC[2, 2] * Matrix_O[2, 5]) + (Matrix_InverseTBC[2, 3] * 1));

            // New coordinates of Damper (On Bell Crank Side)
            _ocMatrix.scmOP.J1x = Matrix_J[0, 6];
            _ocMatrix.scmOP.J1y = Matrix_J[1, 6];
            _ocMatrix.scmOP.J1z = Matrix_J[2, 6];

            // FINAL COORDINATES OF H - Pushrod on Bell Crank Side
            _ocMatrix.scmOP.H1x = Matrix_H[0, 6];
            _ocMatrix.scmOP.H1y = Matrix_H[1, 6];
            _ocMatrix.scmOP.H1z = Matrix_H[2, 6];

            // FINAL COORDINATES OF O - ARB on Bell Crank Side
            _ocMatrix.scmOP.O1x = Matrix_O[0, 6];
            _ocMatrix.scmOP.O1y = Matrix_O[1, 6];
            _ocMatrix.scmOP.O1z = Matrix_O[2, 6];
        }
        #endregion

        #endregion

        #region Remaining Outboard Points

        #region ---OBSOLETE--- : Failed attempt. DONT USE THIS METHOD BECAUSE IT DESTROYS THE ORDER OF CALLING THE POINTS WHICH IS CRUCIAL to try to acces this method from SolverMaster Class for the SetupChange Operations but realised it wasn't necessary.
        ///// <summary>
        ///// Method to calculate the Steering Axis Start and End points. I.E., the LBJ and UBJ points
        ///// </summary>
        ///// <param name="_identifierOut"></param>
        ///// <param name="_vehicleOut"></param>
        ///// <param name="_ocOut"></param>
        ///// <param name="_scmOut"></param>
        ///// <param name="CalculateSteering"></param>
        //public void CalcualteSteeringAxisPoints(int _identifierOut, Vehicle _vehicleOut, OutputClass _ocOut, SuspensionCoordinatesMaster _scmOut, bool CalculateSteering)
        //{
        //    #region Points F and E - Upper and Lower Ball Joints
        //    // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' or E' depending upon whether it is pullrod or  pushrod and if pushrod is housed by Lpper or Lower Wishbone 
        //    double XF1 = 0, YF1 = 0, ZF1 = 0, XE1 = 0, YE1 = 0, ZE1 = 0;

        //    if ((_vehicleOut.PullRodIdentifierFront == 1) && ((_identifierOut == 1) || (_identifierOut == 2))) // Imples that Front is a Pull Rod System
        //    {
        //        // To Calculate New Position of F i.e, to Calculate F' for Front when it is a Pull Rod System
        //        // Vectors used -> F'G', F'A & F'B 
        //        QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_G1x, l_G1y, l_G1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);

        //        //CalculateFinalMotionRatio(_ocOut, _scmOut, true);

        //        // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When it is a PullRod System
        //        // Vectors used -> E'F', E'C & E'D   
        //        //QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);
        //        QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, XF1, YF1, ZF1, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

        //    }
        //    else if ((_vehicleOut.PullRodIdentifierRear == 1) && ((_identifierOut == 3) || (_identifierOut == 4))) // Imples that Rear is a Pull Rod System
        //    {
        //        // To Calculate New Position of F for Rear i.e, to Calculate F' when it is a Pull Rod System
        //        // Vectors used -> F'G', F'A & F'B 
        //        QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_G1x, l_G1y, l_G1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);

        //        //CalculateFinalMotionRatio(_ocOut, _scmOut, true);

        //        // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When it is a PullRod System
        //        // Vectors used -> E'F', E'C & E'D   
        //        //QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);
        //        QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, XF1, YF1, ZF1, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

        //    }
        //    else if (l_G1y >= l_F1y) // Implies that it is a PushRod System and the the Upper Wishbone houses the Pushrod
        //    {

        //        // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' When Pushrod is housed by Upper Wishbone
        //        // Vectors used -> F'G', F'A & F'B 
        //        QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_G1x, l_G1y, l_G1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, true, out XF1, out YF1, out ZF1);

        //        //CalculateFinalMotionRatio(_ocOut, _scmOut, true);

        //        // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When Pushrod is housed by Upper Wishbone
        //        // Vectors used -> E'F', E'C & E'D   
        //        //QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1y, true, out XE1, out YE1, out ZE1);
        //        QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, XF1, YF1, ZF1, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

        //    }
        //    else if ((l_G1y >= l_E1y) && (l_G1y < l_F1y)) // Implies that the Pushrod is housed by the Lower Wishbone
        //    {
        //        // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When Pushrod is housed by Lower Wishbone
        //        QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_G1x, l_G1y, l_G1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

        //        //CalculateFinalMotionRatio(_ocOut, _scmOut, false);

        //        // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' When Pushrod is housed by Lower Wishbone
        //        //QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);
        //        QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, XE1, YE1, ZE1, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);

        //    }

        //    _ocOut.scmOP.F1x = XF1;
        //    _ocOut.scmOP.F1y = YF1;
        //    _ocOut.scmOP.F1z = ZF1;

        //    _ocOut.scmOP.E1x = XE1;
        //    _ocOut.scmOP.E1y = YE1;
        //    _ocOut.scmOP.E1z = ZE1;

        //    if ((l_G1y >= l_E1y) && (l_G1y < l_F1y))
        //    {
        //        CalculateFinalMotionRatio(_ocOut, _scmOut, false);
        //    }
        //    else CalculateFinalMotionRatio(_ocOut, _scmOut, true);
        //    #endregion

        //    #region Point K - Wheel Centre (Wheel Spindle Start)
        //    //if (CalculateSteering)
        //    //{
        //    // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
        //    // Vectors used -> K'M', K'F' & K'E'   
        //    double XK1 = 0, YK1 = 0, ZK1 = 0/*, XK2 = 0, YK2 = 0, ZK2 = 0*/;
        //    QuadraticEquationSolver.Solver(l_K1x, l_K1y, l_K1z, l_M1x, l_M1y, l_M1z, 0, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, _ocOut.scmOP.M1x, _ocOut.scmOP.M1y, _ocOut.scmOP.M1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, _ocOut.scmOP.E1y, false, out XK1, out YK1, out ZK1);

        //    _ocOut.scmOP.K1x = XK1;
        //    _ocOut.scmOP.K1y = YK1;
        //    _ocOut.scmOP.K1z = ZK1;
        //    //}

        //    #endregion

        //} 
        #endregion


        /// <summary>
        /// Method to calculalte all the remaining Outboard Points. Only exception is the Point which is not really an Outboard Point
        /// </summary>
        /// <param name="_identifierOut"></param>
        /// <param name="_vehicleOut"></param>
        /// <param name="_ocOut"></param>
        /// <param name="_scmOut"></param>
        /// <param name="CalculateSteering"></param>
        private void CalculateOutboardPoints(int _identifierOut,Vehicle _vehicleOut, OutputClass _ocOut, SuspensionCoordinatesMaster _scmOut,bool CalculateSteering)
        {
            #region Point G - Pushrod Upright
            // TO CALCULATE THE NEW POSITION OF G i.e., TO CALCULATE G'
            // Vectors used -> G'H', G'B' & G'A'   

            double XG1 = 0, YG1 = 0, ZG1 = 0/*, XG2 = 0, YG2 = 0, ZG2 = 0*/;
            if ((_vehicleOut.PullRodIdentifierFront == 1) && ((_identifierOut == 1) || (_identifierOut == 2))) // To calculate the points for a Pull Rod System in the Front
            { QuadraticEquationSolver.Solver(l_G1x, l_G1y, l_G1z, l_H1x, l_H1y, l_H1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.H1x, _ocOut.scmOP.H1y, _ocOut.scmOP.H1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.H1y, false, out XG1, out YG1, out ZG1); }

            else if ((_vehicleOut.PullRodIdentifierRear == 1) && ((_identifierOut == 3) || (_identifierOut == 4))) // To calculate the points for a Pull Rod System in the Rear
            { QuadraticEquationSolver.Solver(l_G1x, l_G1y, l_G1z, l_H1x, l_H1y, l_H1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.H1x, _ocOut.scmOP.H1y, _ocOut.scmOP.H1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.H1y, false, out XG1, out YG1, out ZG1); }

            else if ((l_G1y >= l_F1y)) // Implies that the the Upper Wishbone houses the Pushrod
            { QuadraticEquationSolver.Solver(l_G1x, l_G1y, l_G1z, l_H1x, l_H1y, l_H1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.H1x, _ocOut.scmOP.H1y, _ocOut.scmOP.H1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.H1y, true, out XG1, out YG1, out ZG1); }

            else if ((l_G1y >= l_E1y) && (l_G1y < l_F1y)) // Implies that the Lower Wishbone houses the Pushrod
            { QuadraticEquationSolver.Solver(l_G1x, l_G1y, l_G1z, l_H1x, l_H1y, l_H1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.H1x, _ocOut.scmOP.H1y, _ocOut.scmOP.H1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.H1y, true, out XG1, out YG1, out ZG1); }


            _ocOut.scmOP.G1x = XG1;
            _ocOut.scmOP.G1y = YG1;
            _ocOut.scmOP.G1z = ZG1;
            #endregion

            #region Points F and E - Upper and Lower Ball Joints
            // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' or E' depending upon whether it is pullrod or  pushrod and if pushrod is housed by Lpper or Lower Wishbone 
            double XF1 = 0, YF1 = 0, ZF1 = 0, XE1 = 0, YE1 = 0, ZE1 = 0;

            if ((_vehicleOut.PullRodIdentifierFront == 1) && ((_identifierOut == 1) || (_identifierOut == 2))) // Imples that Front is a Pull Rod System
            {
                // To Calculate New Position of F i.e, to Calculate F' for Front when it is a Pull Rod System
                // Vectors used -> F'G', F'A & F'B 
                QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_G1x, l_G1y, l_G1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);

                //CalculateFinalMotionRatio(_ocOut, _scmOut, true);

                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When it is a PullRod System
                // Vectors used -> E'F', E'C & E'D   
                //QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);
                QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, XF1, YF1, ZF1, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

            }
            else if ((_vehicleOut.PullRodIdentifierRear == 1) && ((_identifierOut == 3) || (_identifierOut == 4))) // Imples that Rear is a Pull Rod System
            {
                // To Calculate New Position of F for Rear i.e, to Calculate F' when it is a Pull Rod System
                // Vectors used -> F'G', F'A & F'B 
                QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_G1x, l_G1y, l_G1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);

                //CalculateFinalMotionRatio(_ocOut, _scmOut, true);

                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When it is a PullRod System
                // Vectors used -> E'F', E'C & E'D   
                //QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);
                QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, XF1, YF1, ZF1, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

            }
            else if (l_G1y >= l_F1y) // Implies that it is a PushRod System and the the Upper Wishbone houses the Pushrod
            {

                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' When Pushrod is housed by Upper Wishbone
                // Vectors used -> F'G', F'A & F'B 
                QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_G1x, l_G1y, l_G1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, true, out XF1, out YF1, out ZF1);

                //CalculateFinalMotionRatio(_ocOut, _scmOut, true);

                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When Pushrod is housed by Upper Wishbone
                // Vectors used -> E'F', E'C & E'D   
                //QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.F1y, true, out XE1, out YE1, out ZE1);
                QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_F1x, l_F1y, l_F1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, XF1, YF1, ZF1, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

            }
            else if ((l_G1y >= l_E1y) && (l_G1y < l_F1y)) // Implies that the Pushrod is housed by the Lower Wishbone
            {
                // TO CALCULATE THE NEW POSITION OF E i.e., TO CALCULATE E' When Pushrod is housed by Lower Wishbone
                QuadraticEquationSolver.Solver(l_E1x, l_E1y, l_E1z, l_G1x, l_G1y, l_G1z, 0, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1x, _ocOut.scmOP.G1y, _ocOut.scmOP.G1z, l_C1x, l_C1y, l_C1z, l_D1x, l_D1y, l_D1z, _ocOut.scmOP.G1y, true, out XE1, out YE1, out ZE1);

                //CalculateFinalMotionRatio(_ocOut, _scmOut, false);

                // TO CALCULATE THE NEW POSITION OF F i.e., TO CALCULATE F' When Pushrod is housed by Lower Wishbone
                //QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);
                QuadraticEquationSolver.Solver(l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, 0, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, XE1, YE1, ZE1, l_B1x, l_B1y, l_B1z, l_A1x, l_A1y, l_A1z, _ocOut.scmOP.G1y, false, out XF1, out YF1, out ZF1);

            }

            _ocOut.scmOP.F1x = XF1;
            _ocOut.scmOP.F1y = YF1;
            _ocOut.scmOP.F1z = ZF1;

            _ocOut.scmOP.E1x = XE1;
            _ocOut.scmOP.E1y = YE1;
            _ocOut.scmOP.E1z = ZE1;

            if ((l_G1y >= l_E1y) && (l_G1y < l_F1y))
            {
                CalculateFinalMotionRatio(_ocOut, _scmOut, false);
            }
            else CalculateFinalMotionRatio(_ocOut, _scmOut, true);
            #endregion

            #region Point M - Steering Link Chassis
            if (CalculateSteering)
            {
                // TO CALCULATE THE NEW POSITION OF M i.e., TO CALCULATE M'
                // Vectors used -> M'E', M'F' & M'N  
                double XM1 = 0, YM1 = 0, ZM1 = 0/*, XM2 = 0, YM2 = 0, ZM2 = 0*/;
                QuadraticEquationSolver.Solver(l_M1x, l_M1y, l_M1z, l_E1x, l_E1y, l_E1z, 0, l_F1x, l_F1y, l_F1z, l_N1x, l_N1y, l_N1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, l_N1x, l_N1y, l_N1z, l_K1y + l_K1y + Math.Abs(l_W1y), true, out XM1, out YM1, out ZM1);

                _ocOut.scmOP.M1x = XM1;
                _ocOut.scmOP.M1y = YM1;
                _ocOut.scmOP.M1z = ZM1;

                _ocOut.scmOP.N1x = l_N1x;
                _ocOut.scmOP.N1y = l_N1y;
                _ocOut.scmOP.N1z = l_N1z; 
            }
            #endregion

            #region Point K - Wheel Centre (Wheel Spindle Start)
            //if (CalculateSteering)
            //{
            // TO CALCULATE THE NEW POSITION OF K i.e., TO CALCULATE K'
            // Vectors used -> K'M', K'F' & K'E'   
            double XK1 = 0, YK1 = 0, ZK1 = 0/*, XK2 = 0, YK2 = 0, ZK2 = 0*/;
            QuadraticEquationSolver.Solver(l_K1x, l_K1y, l_K1z, l_M1x, l_M1y, l_M1z, 0, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, _ocOut.scmOP.M1x, _ocOut.scmOP.M1y, _ocOut.scmOP.M1z, _ocOut.scmOP.F1x, _ocOut.scmOP.F1y, _ocOut.scmOP.F1z, _ocOut.scmOP.E1x, _ocOut.scmOP.E1y, _ocOut.scmOP.E1z, _ocOut.scmOP.E1y, false, out XK1, out YK1, out ZK1);

            _ocOut.scmOP.K1x = XK1;
            _ocOut.scmOP.K1y = YK1;
            _ocOut.scmOP.K1z = ZK1;
            //}

            #endregion

            #region Point P - Anti-roll Bar Outboard
            // TO CALCULATE THE NEW POSITION OF P i.e., TO CALCULATE P'
            // Vectors used -> P'O', P'Q & P'D   
            double XP1 = 0, YP1 = 0, ZP1 = 0/*, XP2 = 0, YP2 = 0, ZP2 = 0*/;
            if (((_identifierOut == 1) || (_identifierOut == 2)) && (_vehicleOut.TARBIdentifierFront == 1))
            {
                QuadraticEquationSolver.Solver(l_P1x, l_P1y, l_P1z, l_O1x, l_O1y, l_O1z, 0, l_Q1x, l_Q1y, l_Q1z, l_R1x, l_R1y, l_R1z, _ocOut.scmOP.O1x, _ocOut.scmOP.O1y, _ocOut.scmOP.O1z, l_Q1x, l_Q1y, l_Q1z, l_R1x, l_R1y, l_R1z, 0, false, out XP1, out YP1, out ZP1/*, out XP2, out YP2, out ZP2*/);
            }
            else if (((_identifierOut == 1) || (_identifierOut == 2)) && (_vehicleOut.TARBIdentifierFront == 0))
            {
                QuadraticEquationSolver.Solver(l_P1x, l_P1y, l_P1z, l_O1x, l_O1y, l_O1z, 0, l_Q1x, l_Q1y, l_Q1z, l_C1x, l_C1y, l_C1z, _ocOut.scmOP.O1x, _ocOut.scmOP.O1y, _ocOut.scmOP.O1z, l_Q1x, l_Q1y, l_Q1z, l_C1x, l_C1y, l_C1z, _ocOut.scmOP.O1y, true, out XP1, out YP1, out ZP1/*, out XP2, out YP2, out ZP2*/);
            }
            else if (((_identifierOut == 3) || (_identifierOut == 4)) && (_vehicleOut.TARBIdentifierRear == 1))
            {
                QuadraticEquationSolver.Solver(l_P1x, l_P1y, l_P1z, l_O1x, l_O1y, l_O1z, 0, l_Q1x, l_Q1y, l_Q1z, l_R1x, l_R1y, l_R1z, _ocOut.scmOP.O1x, _ocOut.scmOP.O1y, _ocOut.scmOP.O1z, l_Q1x, l_Q1y, l_Q1z, l_R1x, l_R1y, l_R1z, 0, false, out XP1, out YP1, out ZP1/*, out XP2, out YP2, out ZP2*/);
            }
            else if (((_identifierOut == 3) || (_identifierOut == 4)) && (_vehicleOut.TARBIdentifierRear == 0))
            {
                QuadraticEquationSolver.Solver(l_P1x, l_P1y, l_P1z, l_O1x, l_O1y, l_O1z, 0, l_Q1x, l_Q1y, l_Q1z, l_C1x, l_C1y, l_C1z, _ocOut.scmOP.O1x, _ocOut.scmOP.O1y, _ocOut.scmOP.O1z, l_Q1x, l_Q1y, l_Q1z, l_C1x, l_C1y, l_C1z, _ocOut.scmOP.O1y, true, out XP1, out YP1, out ZP1/*, out XP2, out YP2, out ZP2*/);
            }

            _ocOut.scmOP.P1x = XP1;
            _ocOut.scmOP.P1y = YP1;
            _ocOut.scmOP.P1z = ZP1;
            #endregion

        }
        #endregion

        #region Method to Calculate the Antiroll Bar Droop Link Forces
        private void ArbDroopLinkForce( OutputClass _ocARB)
        {
            ///<remarks>
            ///Declaring the Reaction Froces of the Bell Crank about the Pivot. Since it is a bearing which is present along the Z Axis, Mz is 0
            ///Rx is also zero because it is a Hinge Support and hence there is no reaction force along the Lateral Axis
            /// </remarks>
            double Ry, Rz, Mx, My;

            #region Initializng the Position Vectors
            ///<remarks>
            ///Represent the Position Vectors of the Damper, ARB and Pushrod Points on the bellcrank. They are the position vectors with respect to the Pivot Point, I
            /// </remarks>
            Vector3D Position_JI =  new Vector3D(_ocARB.scmOP.J1x - _ocARB.scmOP.I1x,  _ocARB.scmOP.J1y - _ocARB.scmOP.I1y,  _ocARB.scmOP.J1z - _ocARB.scmOP.I1z);
            Vector3D Position_OI =  new Vector3D(_ocARB.scmOP.O1x - _ocARB.scmOP.I1x,  _ocARB.scmOP.O1y - _ocARB.scmOP.I1y,  _ocARB.scmOP.O1z - _ocARB.scmOP.I1z);
            Vector3D Position_HI =  new Vector3D(_ocARB.scmOP.H1x - _ocARB.scmOP.I1x,  _ocARB.scmOP.H1y - _ocARB.scmOP.I1y,  _ocARB.scmOP.H1z - _ocARB.scmOP.I1z);

            ///<remarks>
            ///Represent the Position Vectos of the Arb link and of the Damper Link . Basically, this is the position vector represent the entire link from start point to end point
            /// </remarks>
            Vector3D Position_OP =  new Vector3D(_ocARB.scmOP.O1x - _ocARB.scmOP.P1x,  _ocARB.scmOP.O1y - _ocARB.scmOP.P1y,  _ocARB.scmOP.O1z - _ocARB.scmOP.P1z);
            Vector3D Position_JJo = new Vector3D(_ocARB.scmOP.J1x - _ocARB.scmOP.JO1x, _ocARB.scmOP.J1y - _ocARB.scmOP.JO1y, _ocARB.scmOP.J1z - _ocARB.scmOP.JO1z);

            ///<remarks>
            ///Represents the Vector configuration of the Pushrod Forcd
            /// </remarks>
            Vector3D PushrodForce = new Vector3D(_ocARB.PushRod_x, _ocARB.PushRod_y, _ocARB.PushRod_z);
            ///<remarks> If the Damper Force assignment below doesn't work then just multiply Pushrod with the Motion Ratio</remarks>
            Vector3D DamperForce = new Vector3D(_ocARB.DamperForce_x, _ocARB.DamperForce_y, _ocARB.DamperForce_z);
            #endregion

            ///<remarks>
            ///Calculating the cross products
            /// </remarks>
            Vector3D Cross_JIxDamperForce = Position_JI.CrossProduct(DamperForce);
            Vector3D Cross_OIxOP = Position_OI.CrossProduct(Position_OP);
            Vector3D Cross_HIxPushrod = Position_HI.CrossProduct(PushrodForce);


            Matrix<double> LHS = Matrix<double>.Build.DenseOfArray(new double[,]
                {
                    { Position_OP.X, 1, 0, 0, 0, 0},
                    { Position_OP.Y, 0, 1, 0, 0, 0},
                    { Position_OP.Z, 0, 0, 1, 0, 0},
                    { Cross_OIxOP.X, 0, 0, 0, 1, 0},
                    { Cross_OIxOP.Y, 0, 0, 0, 0, 1},
                    { Cross_OIxOP.Z, 0, 0, 0, 0, 0}
                });

            ///<remarks>
            ///Initializing the RHS Matrix. Negative sign is added to show translation from LHS to RHS
            /// </remarks>
            Vector<double> RHS = Vector<double>.Build.Dense(new double[]
                {
                    -(DamperForce.X          +   PushrodForce.X),
                    -(DamperForce.Y          +   PushrodForce.Y),
                    -(DamperForce.Z          +   PushrodForce.Z),
                    -(Cross_JIxDamperForce.X +   Cross_HIxPushrod.X),
                    -(Cross_JIxDamperForce.Y +   Cross_HIxPushrod.Y),
                    -(Cross_JIxDamperForce.Z +   Cross_HIxPushrod.Z)
                });

            Vector<double> Result = LHS.Solve(RHS);

            _ocARB.ARBDroopLink = Result[0] * Position_OP.Length;
            _ocARB.ARBDroopLink_x = _ocARB.ARBDroopLink * (Position_OP.X / Position_OP.Length);
            _ocARB.ARBDroopLink_y = _ocARB.ARBDroopLink * (Position_OP.Y / Position_OP.Length);
            _ocARB.ARBDroopLink_z = _ocARB.ARBDroopLink * (Position_OP.Z / Position_OP.Length);


        }
        #endregion

        #region Method to Assign the Lateral, Longitudinal and Vertical Load fro the Load Case
        /// <summary>
        /// This method assigns the Forces and Moments that the user has defined (or predefined) during the Load Case creation
        /// </summary>
        /// <param name="_latForce">Total Tire Lateral Force as calculated from the Load Case</param>
        /// <param name="_longForce">Total Tire Longitudinal Force as calculated from the Load Case</param>
        /// <param name="_vertForce">Total Tire Vertical Force as calculated from the Load Case</param>
        /// <param name="_mx">Tire Overturning Moment as defined in the Load Case</param>
        /// <param name="_mz">Tire Self-Aligning Torque as defined in the Load Case</param>
        public void AssignLoadCaseDW(double _latForce, double _longForce, double _vertForce, double _mx, double _mz)
        {
            LatForce = _latForce;
            LongForce = _longForce;
            VerticalForce = _vertForce;
            Mx = _mx;
            Mz = _mz;
        }
        #endregion

        #region Kinematics - Double Wishbone
        //Kinmetics Solver for Double Wishbone Suspenion
        public void Kinematics(int Identifier, SuspensionCoordinatesMaster scm, WheelAlignment wa, Tire tire, AntiRollBar arb, double ARB_Rate_Nmm, Spring spring, Damper damper, List<OutputClass> oc, Vehicle _vehicleKinematicsDWSolver, 
                               List<double> WheelOrSpringDeflections, bool MotionExists, bool RecalculateSteering/*, bool FirstIteration*/)
        {
            
            dummy1 = 0; dummy2 = 0;

            #region Initialization methods

            ///<remarks>
            ///The IF loop is employed so that if the < c > Kinematics </ c > method is being run to calculate the Points for Steering then local coordinate variables should be initialized with the Output Class coordinates rather than the Input Suspension Coordinates as
            ///the method has already been executed and is been run for the second time for the purpose of calcualting steering 
            /// </remarks>
            if (!RecalculateSteering)
            {
                AssignLocalCoordinateVariables_FixesPoints(scm);
                AssignLocalCoordinateVariables_MovingPoints(scm); 
            }
            else if (RecalculateSteering)
            {
                AssignLocalCoordinateVariables_FixesPoints(oc[dummy1].scmOP);
                AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;
            }

            if (MotionExists)
            {
                N = _vehicleKinematicsDWSolver.vehicle_Motion.Final_WheelDeflectionsX.Count;

                /// Initializating the Local Wheel Alignment variables
                InitializeWheelAlignmentVariables(wa, oc[dummy1], Identifier, _vehicleKinematicsDWSolver.vehicle_Motion.SteeringExists);

                /// Initialize array which holds the previous value of spring deflection
                SpringDeflection_Prev = new double[_vehicleKinematicsDWSolver.vehicle_Motion.Final_WheelDeflectionsX.Count];
            }
            else if (SolverMasterClass.SimType == SimulationType.SetupChange)
            {
                N = 200;

            }
            else if (!MotionExists)
            {
                N = 200;

                /// Initializating the Local Wheel Alignment variables
                InitializeWheelAlignmentVariables(wa, oc[dummy1], Identifier, false);

            }


            //  The value of Ride will be calculated at the end as the relative value of Ride Height. This will be used to translate all the coordinates to reflect the car falling on the ground.


            if (!RecalculateSteering)
            {
                ///<remarks> Resetting the wheel defelction due to steering to prevent residue error</remarks>
                for (int i = 0; i < 100; i++)
                {
                    scm.WheelDeflection_Steering[i] = 0; 
                }

                // Converting Coordinates from World Coordinate System to Car Coordinate System
                TranslateToLocalCS(_vehicleKinematicsDWSolver.sc_FL);


                // Initializing the Wheel Spindle end
                InitializeSpindleEndCoordinates(tire);


                // Initializing Wheel Spindle far end vector using Static Camber and Toe
                RotateSpindleVector(oc[dummy1].waOP.StaticCamber, oc[dummy1].waOP.StaticToe, false, 0, 0, 0, 0, 0, 0, Lx, Ly, Lz, out L1x, out L1y, out L1z);

                // Initial Motion Ratio Calculations
                scm.MotionRatio(_vehicleKinematicsDWSolver.McPhersonFront, _vehicleKinematicsDWSolver.McPhersonRear, _vehicleKinematicsDWSolver.PullRodIdentifierFront, _vehicleKinematicsDWSolver.PullRodIdentifierRear, Identifier);
                // Passing the Motion Ratio to the Output Class
                oc[dummy1].InitialMR = scm.InitialMR;
                oc[dummy1].Initial_ARB_MR = scm.Initial_ARB_MR;

                // Preliminary Calculations to find the New Camber and Toe
                CalculateInitialSpindleVector();
                
            }


            if (SolverMasterClass.SimType == SimulationType.SetupChange)
            {
                // Preliminary Calculations to find the New Camber and Toe
                CalculateInitialSpindleVector();

            }
            #endregion 

            #region Wheel Deflection Calculations for Stand to Ground Calculations
            if (!MotionExists)
            {
                //
                // Only for Stand to Ground
                //
                CalculateWheelAndSpringDeflection(scm, spring, damper, _vehicleKinematicsDWSolver, null, oc[0], tire, Identifier, ARB_Rate_Nmm, MotionExists,RecalculateSteering, 0);
            }
            #endregion

            #region For Loop to Calculate SUSPENSION POINTS

            for (a1 = 1; a1 < N; a1++)
            {
                oc[dummy1].scmOP.NoOfCouplings = _vehicleKinematicsDWSolver.sc_FL.NoOfCouplings;

                #region Wheel Deflection Calculations for Motion Calculation
                if (MotionExists && ((!RecalculateSteering)))
                {
                    //
                    // For Motion Simulation   
                    //
                    CalculateWheelAndSpringDeflection(scm, spring, damper, _vehicleKinematicsDWSolver, WheelOrSpringDeflections, oc[dummy1], tire, Identifier, ARB_Rate_Nmm, MotionExists, RecalculateSteering, dummy1);

                }
                else if (MotionExists && (RecalculateSteering ))
                {
                    CalculateSpringDeflection_AfterVehicleModel(oc, Identifier, WheelOrSpringDeflections[dummy1], oc[dummy1].FinalMR, dummy1);
                }

                #endregion

                #region Confirm if needed and delete if not
                /////<remarks>
                /////The IF loop is employed so that if the < c > Kinematics </ c > method is being run to calculate the Points for Steering then local coordinate variables should be initialized with the Output Class coordinates rather than the Input Suspension Coordinates as
                /////the method has already been executed and is been run for the second time for the purpose of calcualting steering 
                /////This loop is executed only once as the 
                ///// </remarks>
                //if (RecalculateRearSteering/* && dummy1 != 01*/)
                //{                    
                //    AssignLocalCoordinateVariables_FixesPoints(oc[dummy1].scmOP);
                //    AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                //    L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;
                //} 
                #endregion

                #region Progress Bar Operations
                _vehicleKinematicsDWSolver.vehicleGUI.ProgressBarVehicleGUI.PerformStep();
                _vehicleKinematicsDWSolver.vehicleGUI.ProgressBarVehicleGUI.Update();
                #endregion

                //To calculate the Angle of Rotation of the Bell-crank by calculating the spring deflection. These are done in small steps

                #region Calculating the deflection of the spring in Steps 
                CalculateAngleOfRotationOrDamperLength(oc[dummy1], MotionExists, dummy1, damper,RecalculateSteering);
                #endregion

                #region Calculating the Caster and KPI
                CalculateKPIandCaster(oc[dummy1], true, Identifier);
                #endregion

                #region Calculating Bell Crank Points
                InitializeRotationMatrices(Identifier, oc[dummy1]);

                if (RecalculateSteering)
                {
                    Point3D J1 = new Point3D(oc[dummy1].scmOP.J1x, oc[dummy1].scmOP.J1y, oc[dummy1].scmOP.J1z);
                    Point3D H1 = new Point3D(oc[dummy1].scmOP.H1x, oc[dummy1].scmOP.H1y, oc[dummy1].scmOP.H1z);
                    Point3D O1 = new Point3D(oc[dummy1].scmOP.O1x, oc[dummy1].scmOP.O1y, oc[dummy1].scmOP.O1z);

                    BellCrankPoints(oc[dummy1], J1, H1, O1);
                }
                else
                {
                    Point3D J1 = new Point3D(l_J1x, l_J1y, l_J1z);
                    Point3D H1 = new Point3D(l_H1x, l_H1y, l_H1z);
                    Point3D O1 = new Point3D(l_O1x, l_O1y, l_O1z);

                    BellCrankPoints(oc[dummy1], J1, H1, O1);
                }
                #endregion

                if (!RecalculateSteering || ((RecalculateSteering) && (Identifier == 3 || Identifier == 4)) || (RecalculateSteering && !MotionExists)) 
                {
                    #region Calculating the remaining Outboard Points - G, then M, then, P
                    CalculateOutboardPoints(Identifier, _vehicleKinematicsDWSolver, oc[dummy1], scm, true);
                    #endregion

                    #region Calculating the Steering Axis Points and the Wheel Centre point --- F, then E, then K
                    ///<summary>
                    ///---OBSOLETE---DELETE EVENTUALL. DONT USE. I DESTROYS THE ORDER OF SOLVING THE POINTS WHICH IS CRUCIAL 
                    /// Seperate method which can be acccessed by the <see cref="SolverMasterClass"/> Class for the purpose of <see cref="SetupChange"/
                    /// ></summary>
                    //CalcualteSteeringAxisPoints(Identifier, _vehicleKinematicsDWSolver, oc[dummy1], scm, true); 
                    #endregion

                    #region Point L - Wheel Spindle End
                    // TO CALCULATE THE NEW POSITION OF L i.e., TO CALCULATE L'
                    // Vectors used -> L'M', L'F' & L'E'   // THE INITIAL COORDINATES OF L SHOULD NOT BE TAKEN FROM USER. THEY SHOULD BE CALCULATED USING 'K' , THE INPUT CAMBER AND TOE
                    double XL1 = 0, YL1 = 0, ZL1 = 0/*, XL2 = 0, YL2 = 0, ZL2 = 0*/;
                    QuadraticEquationSolver.Solver(L1x, L1y, L1z, l_M1x, l_M1y, l_M1z, 0, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, oc[dummy1].scmOP.M1x, oc[dummy1].scmOP.M1y, oc[dummy1].scmOP.M1z, oc[dummy1].scmOP.F1x, oc[dummy1].scmOP.F1y, oc[dummy1].scmOP.F1z, oc[dummy1].scmOP.E1x, oc[dummy1].scmOP.E1y, oc[dummy1].scmOP.E1z, oc[dummy1].scmOP.E1y, false, out XL1, out YL1, out ZL1);

                    oc[dummy1].scmOP.L1x = XL1;
                    oc[dummy1].scmOP.L1y = YL1;
                    oc[dummy1].scmOP.L1z = ZL1;
                    #endregion

                    #region Point W - Contact Patch 

                    //Contact Patch is seperately calculated because after an initial sweep of calculation of all points, the contact patch alone is recalculated to account for steering 
                    // TO CALCULATE THE NEW POSITION OF W i.e., TO CALCULATE W'
                    // Vectors used -> W'M', W'F' & W'E'
                    double XW1 = 0, YW1 = 0, ZW1 = 0/*, XW2 = 0, YW2 = 0, ZW2 = 0*/;
                    QuadraticEquationSolver.Solver(l_W1x, l_W1y, l_W1z, L1x, L1y, L1z, 0, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, oc[dummy1].scmOP.L1x, oc[dummy1].scmOP.L1y, oc[dummy1].scmOP.L1z, oc[dummy1].scmOP.F1x, oc[dummy1].scmOP.F1y, oc[dummy1].scmOP.F1z,
                                                                                                                                                oc[dummy1].scmOP.E1x, oc[dummy1].scmOP.E1y, oc[dummy1].scmOP.E1z, oc[dummy1].scmOP.E1y, true, out XW1, out YW1, out ZW1);
                    oc[dummy1].scmOP.W1x = XW1;
                    oc[dummy1].scmOP.W1y = YW1;
                    oc[dummy1].scmOP.W1z = ZW1;
                    #endregion 
                }

                BreakPointA:
                //Calculating the Tire Loaded Radius
                oc[dummy1].TireLoadedRadius = (l_K1y - l_W1y) + (_vehicleKinematicsDWSolver.CW[Identifier - 1] / tire.TireRate);

                //Calculating The Final Ride Height 
                oc[dummy1].FinalRideHeight = Math.Abs((oc[dummy1].scmOP.W1y) - l_RideHeightRefy + (_vehicleKinematicsDWSolver.CW[Identifier - 1] / tire.TireRate));

                if (_vehicleKinematicsDWSolver.Vehicle_Results_Tracker == 0)
                {
                    oc[dummy1].FinalRideHeight_1 = oc[dummy1].FinalRideHeight;
                }

                oc[dummy1].FinalRideHeight_ForTrans = (oc[dummy1].scmOP.W1y) - l_RideHeightRefy + (_vehicleKinematicsDWSolver.CW[Identifier - 1] / tire.TireRate); // This value of Ride will represent represent the relative and not absolute value of Ride Height. This is necessary to translate all the coordinate
                                                                                                                                                                   // to the ground by an amount equal to the negative of Ride Height


                //if (!RecalculateSteering)
                //{
                    AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                    L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;
                //}


            if ((MotionExists) && dummy1 != 0 && (Identifier != 3 && Identifier != 4) && (_vehicleKinematicsDWSolver.vehicle_Motion.SteeringExists)) 
                {
                    ///<summary>
                    ///This IF loop takes care of 2 things and is used ONLY for the Front in the event of SteeringExists is true
                    ///Calculating the new Spindel End, Steering Upright and Contact Patch Coordinates which is done inside the IF part of the loop
                    ///Calculating the Outboard and Inboard Points Seperately for the Front.
                    /// </summary>

                    if (!RecalculateSteering )
                    {
                        //
                        //Calculating the new coordinates of the Toe Rod Inboard and Outboard End
                        //
                        ToeRod_Steering(oc[dummy1], dummy1, _vehicleKinematicsDWSolver, wa);

                        //
                        // Calculating the Delta Camber due to steering 
                        //
                        CalculateDCamberDToe_Steering(oc[dummy1], _vehicleKinematicsDWSolver, dummy1);

                        //
                        //Calculating the New Camber due to steering
                        //
                        CalculateNewCamberAndToe_Steering(oc, dummy1, _vehicleKinematicsDWSolver);

                        //
                        // Calculating the new Wheel Spindle End due to Steering
                        //
                        RotateSpindleVector(0, dToe_Steering, true, l_F1x, l_F1y, l_F1z, l_E1x, l_E1y, l_E1z, L1x, L1y, L1z, out oc[dummy1].scmOP.L1x, out oc[dummy1].scmOP.L1y, out oc[dummy1].scmOP.L1z);

                        //
                        //Recalculting the Contact Patch due to steering
                        //
                        ContactPatch_Steering(oc[dummy1]);
                        double DeltaWheelDef_Steering;
                        DeltaWheelDef_Steering = oc[dummy1].scmOP.W1y - l_W1y;
                        scm.WheelDeflection_Steering[dummy1] = -DeltaWheelDef_Steering;  
                    }

                    else
                    {
                        /////<remarks>
                        /////Call for the finding the new coordinates of the Outboard points will be made here
                        ///// </remarks>
                        //WheelCentre_Steering(oc, _vehicleKinematicsDWSolver, dummy1);

                        //UBJandLBJ_Steering(oc, dummy1);

                        //Pushrod_Steering(oc, dummy1);

                    }

                    //CalculateSpringDeflection_ForSteering_Front(oc,Identifier, DeltaWheelDef_Steering, oc[dummy1].FinalMR, dummy1);
                    //CalculateAngleOfRotationOrDamperLength(oc[dummy1], MotionExists, dummy1, damper);

                    //InitializeRotationMatrices(Identifier, oc[dummy1]);
                    //BellCrankPoints(oc[dummy1]);

                    l_M1x = oc[dummy1].scmOP.M1x; l_M1y = oc[dummy1].scmOP.M1y; l_M1z = oc[dummy1].scmOP.M1z;

                    //CalculateOutboardPoints(Identifier, _vehicleKinematicsDWSolver, oc[dummy1], scm, false);

                    AssignLocalCoordinateVariables_MovingPoints(oc[dummy1].scmOP);
                    L1x = oc[dummy1].scmOP.L1x; L1y = oc[dummy1].scmOP.L1y; L1z = oc[dummy1].scmOP.L1z;

                }

                _vehicleKinematicsDWSolver.SuspensionIsSolved = true;

                if (MotionExists)
                {
                    dummy1++;
                }

            }

            #endregion

            #region Initializing loop variables for other Outputs
            if (MotionExists)
            {
                N = _vehicleKinematicsDWSolver.vehicle_Motion.Final_WheelDeflectionsX.Count;
                dummy2 = 0;
            }
            else if (!MotionExists)
            {
                N = 2;
                dummy2 = 0;
            }
            #endregion

            #region For loop to calculate other Outputs

            for (a1 = 1; a1 < N; a1++)
            {

                //
                // This variable keeps track of the total value or magnitude of the Spring Deflection. 
                if (_vehicleKinematicsDWSolver.Vehicle_Results_Tracker == 0)
                {
                    oc[dummy2].Corrected_SpringDeflection_1 = oc[dummy2].Corrected_SpringDeflection;
                    oc[dummy2].RideRate_1 = oc[dummy2].RideRate;
                    oc[dummy2].Corrected_WheelDeflection_1 = oc[dummy2].Corrected_WheelDeflection;
                }

                if (dummy2 != 0 && ((MotionExists) && ((Identifier == 3 || Identifier == 4) || ((Identifier == 1 || Identifier == 2) && (!_vehicleKinematicsDWSolver.vehicle_Motion.SteeringExists))))) 
                {
                    CalculatenewCamberAndToe_Rear(oc, dummy2, _vehicleKinematicsDWSolver, Identifier);
                    
                }
                else if (!MotionExists)
                {
                    CalculatenewCamberAndToe_Rear(oc, dummy2, _vehicleKinematicsDWSolver, Identifier);
                    
                }
                #region Coordinate Translations
                ///<remarks>
                ///Need this if loop here because otherwise, this will Vehicle will be translated by the InputOrigin as many times as there are for loops and hence the final vertical coordinate will be very very high
                /// </remarks>
                if (MotionExists)
                {
                    TranslateToRequiredCS(scm, oc[dummy2], _vehicleKinematicsDWSolver, MotionExists, 0, 0, 0, 0, 0, 0);
                }
                else if (!MotionExists)
                {
                    TranslateToRequiredCS(scm, oc[dummy2], _vehicleKinematicsDWSolver, MotionExists, _vehicleKinematicsDWSolver.sc_FL.InputOriginX, _vehicleKinematicsDWSolver.sc_FL.InputOriginY, _vehicleKinematicsDWSolver.sc_FL.InputOriginZ,
                        _vehicleKinematicsDWSolver.OutputOrigin_x, _vehicleKinematicsDWSolver.OutputOrigin_y, _vehicleKinematicsDWSolver.OutputOrigin_z);
                }
                #endregion


                #region Initializing a local variable which is to be specifically used for T-AntiRollBar
                if (Identifier == 1)
                {
                    _vehicleKinematicsDWSolver.P1x_FL = scm.P1x + _vehicleKinematicsDWSolver.sc_FL.InputOriginX + _vehicleKinematicsDWSolver.OutputOrigin_x;
                    _vehicleKinematicsDWSolver.P1z_FL = scm.P1z + _vehicleKinematicsDWSolver.sc_FL.InputOriginZ + _vehicleKinematicsDWSolver.OutputOrigin_z;

                }
                else if (Identifier == 2)
                {
                    _vehicleKinematicsDWSolver.P1x_FR = scm.P1x + _vehicleKinematicsDWSolver.sc_FL.InputOriginX + _vehicleKinematicsDWSolver.OutputOrigin_x;
                    _vehicleKinematicsDWSolver.P1z_FR = scm.P1z + _vehicleKinematicsDWSolver.sc_FL.InputOriginZ + _vehicleKinematicsDWSolver.OutputOrigin_z;

                }
                else if (Identifier == 3)
                {
                    _vehicleKinematicsDWSolver.P1x_RL = scm.P1x + _vehicleKinematicsDWSolver.sc_FL.InputOriginX + _vehicleKinematicsDWSolver.OutputOrigin_x;
                    _vehicleKinematicsDWSolver.P1z_RL = scm.P1z + _vehicleKinematicsDWSolver.sc_FL.InputOriginZ + _vehicleKinematicsDWSolver.OutputOrigin_z;

                }
                else if (Identifier == 4)
                {
                    _vehicleKinematicsDWSolver.P1x_RR = scm.P1x + _vehicleKinematicsDWSolver.sc_FL.InputOriginX + _vehicleKinematicsDWSolver.OutputOrigin_x;
                    _vehicleKinematicsDWSolver.P1z_RR = scm.P1z + _vehicleKinematicsDWSolver.sc_FL.InputOriginZ + _vehicleKinematicsDWSolver.OutputOrigin_z;
                    
                }

                #endregion

                //New Non Suspended Mass CG Calculation
                oc[dummy2].New_NonSuspendedMassCoGx = (oc[dummy2].scmOP.K1x + oc[dummy2].scmOP.L1x) / 2;
                oc[dummy2].New_NonSuspendedMassCoGy = (oc[dummy2].scmOP.K1y + oc[dummy2].scmOP.L1y) / 2;
                oc[dummy2].New_NonSuspendedMassCoGz = (oc[dummy2].scmOP.K1z + oc[dummy2].scmOP.L1z) / 2;

                CalculateSuspensionForces(oc, scm, spring, dummy2);

                //singleLoadCaseResults = AssignSingleBatchRunResultsObject(Identifier, _vehicleKinematicsDWSolver);

                //singleLoadCaseResults.AssignSuspensionForces(oc[dummy2]);

                BreakPointA2:
                if (MotionExists)
                {
                    dummy2++;
                }
            }

            //singleLoadCaseResults.SortSuspensionForces(N);

            #endregion
        }

        //private BatchRunResults AssignSingleBatchRunResultsObject(int _identifier,Vehicle _vLoadCase)
        //{
        //    if (_identifier == 1)
        //    {
        //        return _vLoadCase.vehicleLoadCase.runResults_FL;
        //    }
        //    else if (_identifier == 2)
        //    {
        //        return _vLoadCase.vehicleLoadCase.runResults_FR;
        //    }
        //    else if (_identifier == 3)
        //    {
        //        return _vLoadCase.vehicleLoadCase.runResults_RL;
        //    }
        //    else
        //    {
        //        return _vLoadCase.vehicleLoadCase.runResults_RR;
        //    }


        //}


        public void CalculateSuspensionForces(List<OutputClass> _oc, SuspensionCoordinatesMaster _scm, Spring spring, int _dummy2)
        {
            //Calculating the Suspension Wishbone Forces
            //Unit Vectors
            double UV_D2E2x, UV_D2E2y, UV_D2E2z, UV_C2E2x, UV_C2E2y, UV_C2E2z, UV_F2A2x, UV_F2A2y, UV_F2A2z, UV_F2B2x,
                   UV_F2B2y, UV_F2B2z, UV_G2H2x, UV_G2H2y, UV_G2H2z, UV_J2JO2x, UV_J2JO2y, UV_J2JO2z, UV_M2N2x, UV_M2N2y, UV_M2N2z;

            //Calculating the Unit vectors
            UV_D2E2x = (_oc[_dummy2].scmOP.D1x - _oc[_dummy2].scmOP.E1x) / _scm.LowerFrontLength;
            UV_D2E2y = (_oc[_dummy2].scmOP.D1y - _oc[_dummy2].scmOP.E1y) / _scm.LowerFrontLength;
            UV_D2E2z = (_oc[_dummy2].scmOP.D1z - _oc[_dummy2].scmOP.E1z) / _scm.LowerFrontLength;
            UV_C2E2x = (_oc[_dummy2].scmOP.C1x - _oc[_dummy2].scmOP.E1x) / _scm.LowerRearLength;
            UV_C2E2y = (_oc[_dummy2].scmOP.C1y - _oc[_dummy2].scmOP.E1y) / _scm.LowerRearLength;
            UV_C2E2z = (_oc[_dummy2].scmOP.C1z - _oc[_dummy2].scmOP.E1z) / _scm.LowerRearLength;
            UV_F2A2x = (_oc[_dummy2].scmOP.A1x - _oc[_dummy2].scmOP.F1x) / _scm.UpperFrontLength;
            UV_F2A2y = (_oc[_dummy2].scmOP.A1y - _oc[_dummy2].scmOP.F1y) / _scm.UpperFrontLength;
            UV_F2A2z = (_oc[_dummy2].scmOP.A1z - _oc[_dummy2].scmOP.F1z) / _scm.UpperFrontLength;
            UV_F2B2x = (_oc[_dummy2].scmOP.B1x - _oc[_dummy2].scmOP.F1x) / _scm.UpperRearLength;
            UV_F2B2y = (_oc[_dummy2].scmOP.B1y - _oc[_dummy2].scmOP.F1y) / _scm.UpperRearLength;
            UV_F2B2z = (_oc[_dummy2].scmOP.B1z - _oc[_dummy2].scmOP.F1z) / _scm.UpperRearLength;
            UV_G2H2x = (_oc[_dummy2].scmOP.H1x - _oc[_dummy2].scmOP.G1x) / _scm.PushRodLength;
            UV_G2H2y = (_oc[_dummy2].scmOP.H1y - _oc[_dummy2].scmOP.G1y) / _scm.PushRodLength;
            UV_G2H2z = (_oc[_dummy2].scmOP.H1z - _oc[_dummy2].scmOP.G1z) / _scm.PushRodLength;
            UV_M2N2x = (_oc[_dummy2].scmOP.N1x - _oc[_dummy2].scmOP.M1x) / _scm.ToeLinkLength;
            UV_M2N2y = (_oc[_dummy2].scmOP.N1y - _oc[_dummy2].scmOP.M1y) / _scm.ToeLinkLength;
            UV_M2N2z = (_oc[_dummy2].scmOP.N1z - _oc[_dummy2].scmOP.M1z) / _scm.ToeLinkLength;
            UV_J2JO2x = (_oc[_dummy2].scmOP.J1x - _oc[_dummy2].scmOP.JO1x) / _scm.DamperLength;
            UV_J2JO2y = (_oc[_dummy2].scmOP.J1y - _oc[_dummy2].scmOP.JO1y) / _scm.DamperLength;
            UV_J2JO2z = (_oc[_dummy2].scmOP.J1z - _oc[_dummy2].scmOP.JO1z) / _scm.DamperLength;



            // Matrix A - Unit Vectors and Cross Products 

            double[,] matrixa;
            matrixa = new double[6, 6];
            matrixa[0, 0] = UV_D2E2x;
            matrixa[1, 0] = UV_D2E2y;
            matrixa[2, 0] = UV_D2E2z;
            matrixa[0, 1] = UV_C2E2x;
            matrixa[1, 1] = UV_C2E2y;
            matrixa[2, 1] = UV_C2E2z;
            matrixa[0, 2] = UV_F2A2x;
            matrixa[1, 2] = UV_F2A2y;
            matrixa[2, 2] = UV_F2A2z;
            matrixa[0, 3] = UV_F2B2x;
            matrixa[1, 3] = UV_F2B2y;
            matrixa[2, 3] = UV_F2B2z;
            matrixa[0, 4] = UV_G2H2x;
            matrixa[1, 4] = UV_G2H2y;
            matrixa[2, 4] = UV_G2H2z;
            matrixa[0, 5] = UV_M2N2x;
            matrixa[1, 5] = UV_M2N2y;
            matrixa[2, 5] = UV_M2N2z;

            matrixa[3, 0] = (UV_D2E2z * _oc[_dummy2].scmOP.E1y / 1000) - (UV_D2E2y * _oc[_dummy2].scmOP.E1z / 1000);
            matrixa[4, 0] = -((UV_D2E2z * _oc[_dummy2].scmOP.E1x / 1000) - (UV_D2E2x * _oc[_dummy2].scmOP.E1z / 1000));
            matrixa[5, 0] = (UV_D2E2y * _oc[_dummy2].scmOP.E1x / 1000) - (UV_D2E2x * _oc[_dummy2].scmOP.E1y / 1000);
            matrixa[3, 1] = (UV_C2E2z * _oc[_dummy2].scmOP.E1y / 1000) - (UV_C2E2y * _oc[_dummy2].scmOP.E1z / 1000);
            matrixa[4, 1] = -((UV_C2E2z * _oc[_dummy2].scmOP.E1x / 1000) - (UV_C2E2x * _oc[_dummy2].scmOP.E1z / 1000));
            matrixa[5, 1] = (UV_C2E2y * _oc[_dummy2].scmOP.E1x / 1000) - (UV_C2E2x * _oc[_dummy2].scmOP.E1y / 1000);
            matrixa[3, 2] = (UV_F2A2z * _oc[_dummy2].scmOP.F1y / 1000) - (UV_F2A2y * _oc[_dummy2].scmOP.F1z / 1000);
            matrixa[4, 2] = -((UV_F2A2z * _oc[_dummy2].scmOP.F1x / 1000) - (UV_F2A2x * _oc[_dummy2].scmOP.F1z / 1000));
            matrixa[5, 2] = (UV_F2A2y * _oc[_dummy2].scmOP.F1x / 1000) - (UV_F2A2x * _oc[_dummy2].scmOP.F1y / 1000);
            matrixa[3, 3] = (UV_F2B2z * _oc[_dummy2].scmOP.F1y / 1000) - (UV_F2B2y * _oc[_dummy2].scmOP.F1z / 1000);
            matrixa[4, 3] = -((UV_F2B2z * _oc[_dummy2].scmOP.F1x / 1000) - (UV_F2B2x * _oc[_dummy2].scmOP.F1z / 1000));
            matrixa[5, 3] = (UV_F2B2y * _oc[_dummy2].scmOP.F1x / 1000) - (UV_F2B2x * _oc[_dummy2].scmOP.F1y / 1000);
            matrixa[3, 4] = (UV_G2H2z * _oc[_dummy2].scmOP.G1y / 1000) - (UV_G2H2y * _oc[_dummy2].scmOP.G1z / 1000);
            matrixa[4, 4] = -((UV_G2H2z * _oc[_dummy2].scmOP.G1x / 1000) - (UV_G2H2x * _oc[_dummy2].scmOP.G1z / 1000));
            matrixa[5, 4] = (UV_G2H2y * _oc[_dummy2].scmOP.G1x / 1000) - (UV_G2H2x * _oc[_dummy2].scmOP.G1y / 1000);
            matrixa[3, 5] = (UV_M2N2z * _oc[_dummy2].scmOP.M1y / 1000) - (UV_M2N2y * _oc[_dummy2].scmOP.M1z / 1000);
            matrixa[4, 5] = -((UV_M2N2z * _oc[_dummy2].scmOP.M1x / 1000) - (UV_M2N2x * _oc[_dummy2].scmOP.M1z / 1000));
            matrixa[5, 5] = (UV_M2N2y * _oc[_dummy2].scmOP.M1x / 1000) - (UV_M2N2x * _oc[_dummy2].scmOP.M1y / 1000);

            Matrix<double> MatrixA = Matrix<double>.Build.DenseOfArray(new double[,]
                {
                        { UV_D2E2x,         UV_C2E2x,        UV_F2A2x,      UV_F2B2x,       UV_G2H2x,         UV_M2N2x },
                        { UV_D2E2y,         UV_C2E2y,        UV_F2A2y,      UV_F2B2y,       UV_G2H2y,         UV_M2N2y },
                        { UV_D2E2z,         UV_C2E2z,        UV_F2A2z,      UV_F2B2z,       UV_G2H2z,         UV_M2N2z },
                        { matrixa[3, 0],  matrixa[3, 1],  matrixa[3, 2],  matrixa[3, 3],  matrixa[3, 4],  matrixa[3, 5]},
                        { matrixa[4, 0],  matrixa[4, 1],  matrixa[4, 2],  matrixa[4, 3],  matrixa[4, 4],  matrixa[4, 5]},
                        { matrixa[5, 0],  matrixa[5, 1],  matrixa[5, 2],  matrixa[5, 3],  matrixa[5, 4],  matrixa[5, 5]}
                });



            //Unit Matrix 
            double[,] unitmatrixa;
            unitmatrixa = new double[6, 6];
            for (i = 0; i < 6; ++i)
            {
                for (j = 0; j < 6; ++j)
                {
                    if (j == i)
                    {
                        unitmatrixa[i, j] = 1;
                    }
                }
            }

            //Pseudo Matrix
            double psuedo, psuedo2;

            //Inverse of Matrix A
            for (j = 0; j < 6; j++)
            {
                psuedo = matrixa[j, j];

                for (i = 0; i < 6; i++)
                {
                    matrixa[j, i] = matrixa[j, i] / psuedo;
                    unitmatrixa[j, i] = unitmatrixa[j, i] / psuedo;

                }

                for (k = 0; k < 6; k++)
                {
                    if (k == j)
                    {

                    }
                    else
                    {
                        psuedo2 = matrixa[k, j];
                        for (i = 0; i < 6; i++)
                        {
                            matrixa[k, i] = matrixa[k, i] - (matrixa[j, i] * psuedo2);
                            unitmatrixa[k, i] = unitmatrixa[k, i] - (unitmatrixa[j, i] * psuedo2);

                        }
                    }
                }
            }


            //Forces and Moments generated by the Front Left Tire (Considering Braking + Left Turn)
            double LatF, LongF, MOMx, MOMy, MOMz, Ax = 0, Ay = 0;
            LatF = LatForce;
            LongF = LongForce;
            MOMx = ((LongF * _oc[_dummy2].scmOP.W1y / 1000) - (_oc[_dummy2].scmOP.W1z * (_oc[_dummy2].CW + VerticalForce) / 1000));
            ///<remarks>
            /// Y axis for my software is the vertixal Axis so I have added Mz to the Moment about the Y Axis as Mz is the Moment about the vertical axis as defined by the User
            /// </remarks>
            MOMy = ((LatF * _oc[_dummy2].scmOP.W1z / 1000) - (LongF * _oc[_dummy2].scmOP.W1x / 1000)) + Mz;
            ///<remarks>
            ///Z axis for my software is the Longitudinal Axis so I have added Mx to the Moment about the Z Axis as Mx is the Moment about the Longitudinal axis as defined by the User
            ///</remarks>
            MOMz = (((_oc[_dummy2].CW + VerticalForce) * _oc[_dummy2].scmOP.W1x / 1000) - (LatF * _oc[_dummy2].scmOP.W1y / 1000)) + Mx;

            //Matrx B
            double[,] matrixb;
            matrixb = new double[6, 1];
            matrixb[0, 0] = LatF;
            ///<remarks>The <c>oc[dummy2].deltaNet_CornerWeight</c> is already included in the <c>oc[dummy2].CW</c> <seealso cref="VehicleModel.ComputeVehicleModel_SummationOfResults(Vehicle)"/> </remarks>
            matrixb[1, 0] = _oc[_dummy2].CW + (VerticalForce /*- oc[dummy2].deltaNet_CornerWeight*/);
            matrixb[2, 0] = LongF;
            matrixb[3, 0] = MOMx;
            matrixb[4, 0] = MOMy;
            matrixb[5, 0] = MOMz;

            Vector<double> MatrixB = Vector<double>.Build.Dense(new double[]
                {
                        matrixb[0,0],
                        matrixb[1,0],
                        matrixb[2,0],
                        matrixb[3,0],
                        matrixb[4,0],
                        matrixb[5,0],
                });


            //Matrix X
            double X;
            double[,] matrixX;
            matrixX = new double[6, 1];

            for (j = 0; j <= 5; j++)
            {
                X = 0;
                for (i = 0; i <= 5; i++)
                {
                    X += (unitmatrixa[j, i] * matrixb[i, 0]);
                }
                matrixX[j, 0] = X;
            }

            var Inverse = MatrixA.Inverse();

            Vector<double> WishboneForces = MatrixA.Solve(MatrixB);

            _oc[_dummy2].LowerFront = (matrixX[0, 0]); // Lower Front
            _oc[_dummy2].LowerRear = (matrixX[1, 0]); // Lower Rear
            _oc[_dummy2].UpperFront = (matrixX[2, 0]); // Upper Front
            _oc[_dummy2].UpperRear = (matrixX[3, 0]); // Upper Rear
            _oc[_dummy2].PushRod = (matrixX[4, 0]); // Pushrod
            _oc[_dummy2].ToeLink = (matrixX[5, 0]); // Toe Link
            _oc[_dummy2].DamperForce = ((spring.SpringRate * _oc[_dummy2].Corrected_SpringDeflection) + (spring.SpringPreload * spring.SpringRate)) + Damper_Preload + (VerticalForce * _oc[dummy2].FinalMR);

            //Forces in Each Pick Up Points
            _oc[_dummy2].LowerFront_x = _oc[_dummy2].LowerFront * UV_D2E2x;
            _oc[_dummy2].LowerFront_y = _oc[_dummy2].LowerFront * UV_D2E2y;
            _oc[_dummy2].LowerFront_z = _oc[_dummy2].LowerFront * UV_D2E2z;
            _oc[_dummy2].LowerRear_x = _oc[_dummy2].LowerRear * UV_C2E2x;
            _oc[_dummy2].LowerRear_y = _oc[_dummy2].LowerRear * UV_C2E2y;
            _oc[_dummy2].LowerRear_z = _oc[_dummy2].LowerRear * UV_C2E2z;
            _oc[_dummy2].UpperFront_x = _oc[_dummy2].UpperFront * UV_F2A2x;
            _oc[_dummy2].UpperFront_y = _oc[_dummy2].UpperFront * UV_F2A2y;
            _oc[_dummy2].UpperFront_z = _oc[_dummy2].UpperFront * UV_F2A2z;
            _oc[_dummy2].UpperRear_x = _oc[_dummy2].UpperRear * UV_F2B2x;
            _oc[_dummy2].UpperRear_y = _oc[_dummy2].UpperRear * UV_F2B2y;
            _oc[_dummy2].UpperRear_z = _oc[_dummy2].UpperRear * UV_F2B2z;
            _oc[_dummy2].PushRod_x = _oc[_dummy2].PushRod * UV_G2H2x;
            _oc[_dummy2].PushRod_y = _oc[_dummy2].PushRod * UV_G2H2y;
            _oc[_dummy2].PushRod_z = _oc[_dummy2].PushRod * UV_G2H2z;
            _oc[_dummy2].DamperForce_x = _oc[_dummy2].DamperForce * UV_J2JO2x;
            _oc[_dummy2].DamperForce_y = _oc[_dummy2].DamperForce * UV_J2JO2y;
            _oc[_dummy2].DamperForce_z = _oc[_dummy2].DamperForce * UV_J2JO2z;
            _oc[_dummy2].ToeLink_x = _oc[_dummy2].ToeLink * UV_M2N2x;
            _oc[_dummy2].ToeLink_y = _oc[_dummy2].ToeLink * UV_M2N2y;
            _oc[_dummy2].ToeLink_z = _oc[_dummy2].ToeLink * UV_M2N2z;
            _oc[_dummy2].UBJ_x = (_oc[_dummy2].UpperFront_x + _oc[_dummy2].UpperRear_x);
            _oc[_dummy2].UBJ_y = (_oc[_dummy2].UpperFront_y + _oc[_dummy2].UpperRear_y);
            _oc[_dummy2].UBJ_z = (_oc[_dummy2].UpperFront_z + _oc[_dummy2].UpperRear_z);
            _oc[_dummy2].LBJ_x = (_oc[_dummy2].LowerFront_x + _oc[_dummy2].LowerRear_x);
            _oc[_dummy2].LBJ_y = (_oc[_dummy2].LowerFront_y + _oc[_dummy2].LowerRear_y);
            _oc[_dummy2].LBJ_z = (_oc[_dummy2].LowerFront_z + _oc[_dummy2].LowerRear_z);

            ArbDroopLinkForce(_oc[_dummy2]);
        }


        #endregion 
        #endregion





    }
}

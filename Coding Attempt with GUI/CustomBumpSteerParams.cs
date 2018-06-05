using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This class will store all the Params of a Custom Bump Steer characteristic that a user may want in his/her SetupChange
    /// </summary>
    public class CustomBumpSteerParams
    {
        /// <summary>
        /// List of Wheel Defelctions over which the Toe Angle Variation is going to be plotted
        /// </summary>
        public List<double> WheelDeflections;

        /// <summary>
        /// <para>The Toe Angles at each Wheel Deflection</para>
        /// <para> ---IMPORTANT--- This is the DELTA or VARIATION of Toe Angle. Hence, the Static Toe Angle must be added to this before using this </para>
        /// </summary>
        public List<Angle> ToeAngles;

        public List<Angle> ToeAnglesBump;

        public List<Angle> ToeAnglesRebound;


        /// <summary>
        /// Variable holding the Highest Bump Value
        /// </summary>
        double HighestBump;

        /// <summary>
        /// Variable holding the index of the Highest Bump
        /// </summary>
        public int HighestBumpindex;

        /// <summary>
        /// Step Size of the Motion Analysis
        /// </summary>
        public int StepSize = 1;

        double slope, intercept, VerifyEQ;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomBumpSteerParams()
        {
            WheelDeflections = new List<double>();

            ToeAngles = new List<Angle>();

            ToeAnglesBump = new List<Angle>();

            ToeAnglesRebound = new List<Angle>();
        }

        #region Calculating the equation of the line of motion
        /// <summary>
        /// Method to compute the Equation of the Line
        /// </summary>
        /// <param name="_Y"></param>
        /// <param name="_X"></param>
        /// <param name="Index"></param>
        private void EquationOfLine(double[] _Y, double[] _X, int Index)
        {
            slope = (_Y[Index + 1] - _Y[Index]) / (_X[Index + 1] - _X[Index]);
            intercept = (_Y[Index] - (slope * _X[Index]));
            VerifyEQ = (slope * _X[Index]) + intercept; // This line exists only to verify if the slope is calculated properly. This line will be used only during debugging
        }
        #endregion

        /// <summary>
        /// Method to populate the <see cref="ToeAngles"/> list with the plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// Method to populate the <see cref="WheelDeflections"/> list with the Plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// </summary>
        /// <param name="_toeAngleFromChart"></param>
        /// <param name="_wdFromChart"></param>
        public void PopulateBumpSteerGraph(List<double> wdFromChart, List<double> toeAngleFromChart)
        {



            int NoOfDeflections = wdFromChart.Count;

            int NoOfSteps;

            wdFromChart.Sort();

            toeAngleFromChart.Sort();

            

            List<double> tempWdFromChart = new List<double>(/*new double[] { 0 }*/);

            tempWdFromChart.AddRange(wdFromChart.ToArray());
            

            List<double> tempToeAngleFromChart = new List<double>(/*new double[] { 0 }*/);

            tempToeAngleFromChart.AddRange(toeAngleFromChart.ToArray());








            List<double> deflections = new List<double>();

            List<double> toeVariations = new List<double>();


            double NextX;

            for (int i = 0; i < NoOfDeflections - 1; i++) 
            {
                ///<summary>Computing the Equation of the Line betweent he current and next plotted point</summary>
                EquationOfLine(tempToeAngleFromChart.ToArray(), tempWdFromChart.ToArray(), i);
                ///<summary>Calculating the number of steps required to get from the current point to the next for a step size of Delta = 1 </summary>
                NoOfSteps = Math.Abs(Convert.ToInt32((tempWdFromChart[i + 1] - tempWdFromChart[i]) / StepSize));
                ///<summary>Assigning the Current chart point to a temporary variable</summary>
                NextX = tempWdFromChart[i];

                ///<summary>Incrementing the Current variable based on the Slope, Intercept and delta</summary>
                for (int j = 0; j < NoOfSteps; j++)
                {

                    int PositionOfInsert = toeVariations.Count;

                    toeVariations.Insert(PositionOfInsert, new Double());
                    toeVariations[PositionOfInsert] = (slope * NextX) + intercept;

                    deflections.Insert(PositionOfInsert, new Double());
                    deflections[PositionOfInsert] = NextX;

                    NextX += StepSize;
                }
            }

            ///<summary>Populating the Toe Angles List</summary>
            ToeAngles.Clear();

            for (int i = 0; i < toeVariations.Count; i++)
            {
                ToeAngles.Add(new Angle(toeVariations[i], AngleUnit.Degrees));
            }


            ///<summary> Populating the Wheel Deflections List</summary>
            WheelDeflections.Clear();

            for (int i = 0; i < deflections.Count; i++)
            {
                WheelDeflections.Add(deflections[i]);
            }

            //WheelDeflections.Add(0);

            //ToeAngles.Add(new Angle());

            ///<summary>Sorting the Toe Angles based on the sorting of Wheel Deflections</summary>
            SortToeList();

            //ToeAngles.Insert(0, ToeAngles[0]);

            //ToeAngles.Insert(ToeAngles.Count, ToeAngles[ToeAngles.Count - 1]);

            //WheelDeflections.Insert(0, WheelDeflections[0]);

            //WheelDeflections.Insert(WheelDeflections.Count, WheelDeflections[WheelDeflections.Count - 1]);

            //SplitToeAngles();

            ExtendWheelDeflection(StepSize);

        }

        /// <summary>
        /// Method to Sort the Toe Angles according to the Wheel Deflections
        /// </summary>
        private void SortToeList()
        {
            ///<summary>Assigning the Toe Angles to a temporary <see cref="Double[]"/> <see cref="Array"/></summary>
            double[] toeAngle = new double[ToeAngles.Count];

            for (int i = 0; i < ToeAngles.Count; i++)
            {
                toeAngle[i] = ToeAngles[i].Degrees;
            }

            ///<summary>Assigning the Wheel Deflections to a temporary <see cref="Double[]"/> <see cref="Array"/></summary>
            double[] wheelDeflections = WheelDeflections.ToArray();

            ///<summary>Sorting the Toe Angles based on the sorting of the Wheel Deflections</summary>
            Array.Sort(wheelDeflections, toeAngle);

            ///<summary>Converting the temporary <see cref="Array"/>s back to <see cref="List{T}"/>s</summary>
            WheelDeflections = wheelDeflections.ToList();

            WheelDeflections.Reverse();

            ToeAngles.Clear();

            for (int i = 0; i < toeAngle.Length; i++)
            {
                ToeAngles.Add(new Angle(toeAngle[i], AngleUnit.Degrees));
            }

            ToeAngles.Reverse();

        }

        /// <summary>
        /// Method to split the <see cref="ToeAngles"/> into 2 seperate lists of Bump and Rebound
        /// </summary>
        private void SplitToeAngles()
        {
            ToeAnglesBump.Clear();

            ToeAnglesRebound.Clear();

            ///<summary>Splitting the Toe Angles into Bump</summary>
            for (int i = 0; i < WheelDeflections.Count; i++)
            {

                if (WheelDeflections[i] >= 0) 
                {
                    ToeAnglesBump.Add(ToeAngles[i]); 
                }
            }

            ///<remarks>Doing them in 2 different loops so that the value 0 will be added to both the lists</remarks>

            ///<summary>Splitting the Toe Angles into Rebound</summary>
            for (int i = 0; i < WheelDeflections.Count; i++)
            {
                if (WheelDeflections[i] <= 0)
                {
                    ToeAnglesRebound.Add(ToeAngles[i]);
                }
            }

        }


        private void ExtendWheelDeflection(int _stepSize)
        {
            if (WheelDeflections.Count != 0)
            {
                HighestBump = WheelDeflections[0];

                double iterations = HighestBump / _stepSize;

                for (int i = 0; i < iterations; i++)
                {
                    WheelDeflections.Insert(i, i * _stepSize);
                }

                HighestBumpindex = WheelDeflections.IndexOf(WheelDeflections.Max());  
            }

        }


    }
}

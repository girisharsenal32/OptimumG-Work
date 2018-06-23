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
        public double StepSize = 1;

        double slope, intercept, VerifyEQ;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomBumpSteerParams()
        {
            WheelDeflections = new List<double>();

            ToeAngles = new List<Angle>();
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
            ///<summary>Basically this is a count of a the number of Wheel Deflections which the user has created</summary>
            int NoOfDeflections = wdFromChart.Count;

            ///<summary>This is the number of Steps that WILL exist between 2 points. This is calculated using the <see cref="StepSize"/> which the user selects</summary>
            int NoOfSteps;

            ///<summary>Sorting the Wheel Deflections and the Toe Angles</summary>
            wdFromChart.Sort();

            toeAngleFromChart.Sort();
            
            ///<summary>Temporary Wheel Deflection List which will be used to cmpute to all the intermediary Wheel Defelctions between 2 Wheel Deflection Points (using the equation of the line jjoining the 2 wheel def points</summary>
            List<double> tempWdFromChart = new List<double>();
            tempWdFromChart.AddRange(wdFromChart.ToArray());

            ///<summary>Temporary Toe Angle List which will be used to cmpute to all the intermediary Toe Angles between 2 Toe Angle Points (using the equation of the line jjoining the 2 wheel def points</summary>
            List<double> tempToeAngleFromChart = new List<double>(/*new double[] { 0 }*/);
            tempToeAngleFromChart.AddRange(toeAngleFromChart.ToArray());

            ///<summary>The List which will hold the final wheel deflections</summary>
            List<double> deflections = new List<double>();

            ///<summary>The List which will hold the final toe angles</summary>
            List<double> toeVariations = new List<double>();


            double NextX;

            ///<summary>Computing the entire Wheel Deflection curve using all the points the user has created and using his desired step size</summary>
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
            
            ///<summary>Sorting the Toe Angles based on the sorting of Wheel Deflections</summary>
            SortToeList();

            ///<summary>Extending the Wheel Deflection from 0 to Max Positive value so I have a continuous profile to work with </summary>
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

            ///<summary>>Reversing the order of the Wheel Defelctions so that the sequence is (0 -> 25 | 25-> 0 | 0 -> -25) </summary>
            WheelDeflections.Reverse();

            if (WheelDeflections.Count != 0)
            {
                ///<summary>Inserting an additional element at the end of the wheel deflection so that the solver runs through till the last wheel deflection and produces meaninful results</summary>
                WheelDeflections.Insert(WheelDeflections.Count - 1, WheelDeflections[WheelDeflections.Count - 1]); 
            }

            ToeAngles.Clear();

            for (int i = 0; i < toeAngle.Length; i++)
            {
                ToeAngles.Add(new Angle(toeAngle[i], AngleUnit.Degrees));
            }

            ///<summary>Reversing the order of the Toe Angles so that they are along the same liines of the Wheel Deflection</summary>
            ToeAngles.Reverse();

            if (ToeAngles.Count != 0)
            {
                ///<summary>Adding an additional element to the ToeANgles list jsut the I did to the Wheel Deflection just so both the lists are in the same wavelength</summary>
                ToeAngles.Insert(ToeAngles.Count - 1, ToeAngles[ToeAngles.Count - 1]); 
            }


        }


        private void ExtendWheelDeflection(double _stepSize)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    public class CustomCamberCurve
    {

        /// <summary>
        /// List of Wheel Defelctions over which the Camber Angle Variation is going to be plotted
        /// </summary>
        public List<double> WheelDeflections;


        /// <summary>
        /// <para>The Camber Angles at each Wheel Deflection</para>
        /// <para> ---IMPORTANT--- This is the DELTA or VARIATION of Camber Angle. Hence, the Static Camber Angle must be added to this before using this </para>
        /// </summary>
        public List<Angle> CamberAnglesHeave;

        /// <summary>
        /// List of Steering Wheel Angles over which the Camber Angle Variation is going to be plotted
        /// </summary>
        public List<double> SteeringWheelAngles;

        /// <summary>
        /// <para>The Camber Angles at each Steering Wheel Angle</para>
        /// <para> ---IMPORTANT--- This is the DELTA or VARIATION of Camber Angle. Hence, the Static Camber Angle must be added to this before using this </para>
        /// </summary>
        public List<Angle> CamberAnglesSteering;

        /// <summary>
        /// Variable holding the Highest Bump Value
        /// </summary>
        double HighestBump;

        /// <summary>
        /// Variable holding the index of the Highest Bump
        /// </summary>
        public int HighestBumpindex;

        /// <summary>
        /// Variable holding the Highest Stering Wheel Angle Value
        /// </summary>
        double HighestSteer;

        /// <summary>
        /// Variable holding the index of the Highest Steering Wheel Angle
        /// </summary>
        public int HighestSteerIndex;


        /// <summary>
        /// Step Size of the Heave Heave Analysis
        /// </summary>
        public int StepSizeHeave = 1;

        /// <summary>
        /// Step Size of the Steering Steering Analysis
        /// </summary>
        public int StepSizeSteer = 1;

        double slope, intercept, VerifyEQ;





        public CustomCamberCurve()
        {
            WheelDeflections = new List<double>();

            SteeringWheelAngles = new List<double>();

            CamberAnglesHeave = new List<Angle>();

            CamberAnglesSteering = new List<Angle>();
            
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
        /// Method to populate the <see cref="camberAngleFromChart"/> list with the plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// Method to populate the <see cref="WheelDeflections"/> list with the Plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// </summary>
        /// <param name="camberAngleFromChart">Camber Angles from the Heave Chart</param>
        /// <param name="_wdFromChart">Wheel Deflections</param>
        public void Populate_CamerVariation_Heave(List<double> wdFromChart, List<double> camberAngleFromChart)
        {
            ///<summary>Basically this is a count of a the number of Wheel Deflections which the user has created</summary>
            int NoOfDeflections = wdFromChart.Count;

            ///<summary>This is the number of Steps that WILL exist between 2 points. This is calculated using the <see cref="StepSize"/> which the user selects</summary>
            int NoOfSteps;

            ///<summary>Sorting the Wheel Deflections and the Toe Angles</summary>
            wdFromChart.Sort();

            camberAngleFromChart.Sort();

            ///<summary>Temporary Wheel Deflection List which will be used to cmpute to all the intermediary Wheel Defelctions between 2 Wheel Deflection Points (using the equation of the line jjoining the 2 wheel def points</summary>
            List<double> tempWdFromChart = new List<double>();
            tempWdFromChart.AddRange(wdFromChart.ToArray());

            ///<summary>Temporary Toe Angle List which will be used to cmpute to all the intermediary Toe Angles between 2 Toe Angle Points (using the equation of the line jjoining the 2 wheel def points</summary>
            List<double> tempCamberAngleFromChart = new List<double>(/*new double[] { 0 }*/);
            tempCamberAngleFromChart.AddRange(camberAngleFromChart.ToArray());

            ///<summary>The List which will hold the final wheel deflections</summary>
            List<double> deflections = new List<double>();

            ///<summary>The List which will hold the final toe angles</summary>
            List<double> camberVariations = new List<double>();


            double NextX;

            ///<summary>Computing the entire Wheel Deflection curve using all the points the user has created and using his desired step size</summary>
            for (int i = 0; i < NoOfDeflections - 1; i++)
            {
                ///<summary>Computing the Equation of the Line betweent he current and next plotted point</summary>
                EquationOfLine(tempCamberAngleFromChart.ToArray(), tempWdFromChart.ToArray(), i);
                ///<summary>Calculating the number of steps required to get from the current point to the next for a step size of Delta = 1 </summary>
                NoOfSteps = Math.Abs(Convert.ToInt32((tempWdFromChart[i + 1] - tempWdFromChart[i]) / StepSizeHeave));
                ///<summary>Assigning the Current chart point to a temporary variable</summary>
                NextX = tempWdFromChart[i];

                ///<summary>Incrementing the Current variable based on the Slope, Intercept and delta</summary>
                for (int j = 0; j < NoOfSteps; j++)
                {

                    int PositionOfInsert = camberVariations.Count;

                    camberVariations.Insert(PositionOfInsert, new Double());
                    camberVariations[PositionOfInsert] = (slope * NextX) + intercept;

                    deflections.Insert(PositionOfInsert, new Double());
                    deflections[PositionOfInsert] = NextX;

                    NextX += StepSizeHeave;
                }
            }

            ///<summary>Populating the Toe Angles List</summary>
            CamberAnglesHeave.Clear();

            for (int i = 0; i < camberVariations.Count; i++)
            {
                CamberAnglesHeave.Add(new Angle(camberVariations[i], AngleUnit.Degrees));
            }

            ///<summary> Populating the Wheel Deflections List</summary>
            WheelDeflections.Clear();

            for (int i = 0; i < deflections.Count; i++)
            {
                WheelDeflections.Add(deflections[i]);
            }

            ///<summary>Sorting the Toe Angles based on the sorting of Wheel Deflections</summary>
            SortToeList(CamberAnglesHeave, WheelDeflections);

            ///<summary>Extending the Wheel Deflection from 0 to Max Positive value so I have a continuous profile to work with </summary>
            ExtendWheelDeflection(StepSizeHeave, HighestBump, HighestBumpindex);

        }

        /// <summary>
        /// Method to populate the <see cref="camberAngleFromChart"/> list with the plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// Method to populate the <see cref="saFromCHart"/> list with the Plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// </summary>
        /// <param name="camberAngleFromChart">Camber Angles from the Steering Chart</param>
        /// <param name="saFromCHart">Steering Wheel Angles From the Chart</param>
        public void Populate_CamerVariation_Steering(List<double> saFromCHart, List<double> camberAngleFromChart)
        {
            ///<summary>Basically this is a count of a the number of Wheel Deflections which the user has created</summary>
            int NoOfDeflections = saFromCHart.Count;

            ///<summary>This is the number of Steps that WILL exist between 2 points. This is calculated using the <see cref="StepSize"/> which the user selects</summary>
            int NoOfSteps;

            ///<summary>Sorting the Wheel Deflections and the Toe Angles</summary>
            saFromCHart.Sort();

            camberAngleFromChart.Sort();

            ///<summary>Temporary Wheel Deflection List which will be used to cmpute to all the intermediary Wheel Defelctions between 2 Wheel Deflection Points (using the equation of the line jjoining the 2 wheel def points</summary>
            List<double> tempSAFromChart = new List<double>();
            tempSAFromChart.AddRange(saFromCHart.ToArray());

            ///<summary>Temporary Toe Angle List which will be used to cmpute to all the intermediary Toe Angles between 2 Toe Angle Points (using the equation of the line jjoining the 2 wheel def points</summary>
            List<double> tempCamberAngleFromChart = new List<double>(/*new double[] { 0 }*/);
            tempCamberAngleFromChart.AddRange(camberAngleFromChart.ToArray());

            ///<summary>The List which will hold the final wheel deflections</summary>
            List<double> steers = new List<double>();

            ///<summary>The List which will hold the final toe angles</summary>
            List<double> camberVariations = new List<double>();


            double NextX;

            ///<summary>Computing the entire Wheel Deflection curve using all the points the user has created and using his desired step size</summary>
            for (int i = 0; i < NoOfDeflections - 1; i++)
            {
                ///<summary>Computing the Equation of the Line betweent he current and next plotted point</summary>
                EquationOfLine(tempCamberAngleFromChart.ToArray(), tempSAFromChart.ToArray(), i);
                ///<summary>Calculating the number of steps required to get from the current point to the next for a step size of Delta = 1 </summary>
                NoOfSteps = Math.Abs(Convert.ToInt32((tempSAFromChart[i + 1] - tempSAFromChart[i]) / StepSizeHeave));
                ///<summary>Assigning the Current chart point to a temporary variable</summary>
                NextX = tempSAFromChart[i];

                ///<summary>Incrementing the Current variable based on the Slope, Intercept and delta</summary>
                for (int j = 0; j < NoOfSteps; j++)
                {

                    int PositionOfInsert = camberVariations.Count;

                    camberVariations.Insert(PositionOfInsert, new Double());
                    camberVariations[PositionOfInsert] = (slope * NextX) + intercept;

                    steers.Insert(PositionOfInsert, new Double());
                    steers[PositionOfInsert] = NextX;

                    NextX += StepSizeHeave;
                }
            }

            ///<summary>Populating the Toe Angles List</summary>
            CamberAnglesSteering.Clear();

            for (int i = 0; i < camberVariations.Count; i++)
            {
                CamberAnglesSteering.Add(new Angle(camberVariations[i], AngleUnit.Degrees));
            }

            ///<summary> Populating the Wheel Deflections List</summary>
            SteeringWheelAngles.Clear();

            for (int i = 0; i < steers.Count; i++)
            {
                SteeringWheelAngles.Add(steers[i]);
            }

            ///<summary>Sorting the Toe Angles based on the sorting of Wheel Deflections</summary>
            SortToeList(CamberAnglesSteering,SteeringWheelAngles);

            ///<summary>Extending the Wheel Deflection from 0 to Max Positive value so I have a continuous profile to work with </summary>
            ExtendWheelDeflection(StepSizeSteer, HighestSteer, HighestSteerIndex);

        }
        
        /// <summary>
        /// Method to Sort the Toe Angles according to the Wheel Deflections
        /// </summary>
        private void SortToeList(List<Angle> _camberAngles, List<double> _wheelORSteeringDeflections)
        {
            ///<summary>Assigning the Toe Angles to a temporary <see cref="Double[]"/> <see cref="Array"/></summary>
            double[] toeAngle = new double[_camberAngles.Count];

            for (int i = 0; i < _camberAngles.Count; i++)
            {
                toeAngle[i] = _camberAngles[i].Degrees;
            }

            ///<summary>Assigning the Wheel Deflections to a temporary <see cref="Double[]"/> <see cref="Array"/></summary>
            double[] wheelDeflections = _wheelORSteeringDeflections.ToArray();

            ///<summary>Sorting the Toe Angles based on the sorting of the Wheel Deflections</summary>
            Array.Sort(wheelDeflections, toeAngle);

            ///<summary>Converting the temporary <see cref="Array"/>s back to <see cref="List{T}"/>s</summary>
            _wheelORSteeringDeflections = wheelDeflections.ToList();

            ///<summary>>Reversing the order of the Wheel Defelctions so that the sequence is (0 -> 25 | 25-> 0 | 0 -> -25) </summary>
            _wheelORSteeringDeflections.Reverse();

            if (_wheelORSteeringDeflections.Count != 0)
            {
                ///<summary>Inserting an additional element at the end of the wheel deflection so that the solver runs through till the last wheel deflection and produces meaninful results</summary>
                _wheelORSteeringDeflections.Insert(_wheelORSteeringDeflections.Count - 1, _wheelORSteeringDeflections[_wheelORSteeringDeflections.Count - 1]);
            }

            _camberAngles.Clear();

            for (int i = 0; i < toeAngle.Length; i++)
            {
                _camberAngles.Add(new Angle(toeAngle[i], AngleUnit.Degrees));
            }

            ///<summary>Reversing the order of the Toe Angles so that they are along the same liines of the Wheel Deflection</summary>
            _camberAngles.Reverse();

            if (_camberAngles.Count != 0)
            {
                ///<summary>Adding an additional element to the ToeANgles list jsut the I did to the Wheel Deflection just so both the lists are in the same wavelength</summary>
                _camberAngles.Insert(_camberAngles.Count - 1, _camberAngles[_camberAngles.Count - 1]);
            }


        }

        /// <summary>
        /// Method to Extend the Wheel Deflections or Steering Angles so as to get a uniform chart to simulation a Heave or Steering Profile
        /// </summary>
        /// <param name="_stepSize"></param>
        /// <param name="_highestBumpOrSteer"></param>
        /// <param name="_highestBumpOrSteerIndex"></param>
        private void ExtendWheelDeflection(int _stepSize, double _highestBumpOrSteer, int _highestBumpOrSteerIndex)
        {
            if (WheelDeflections.Count != 0)
            {
                _highestBumpOrSteer = WheelDeflections[0];

                double iterations = _highestBumpOrSteer / _stepSize;

                for (int i = 0; i < iterations; i++)
                {
                    WheelDeflections.Insert(i, i * _stepSize);
                }

                _highestBumpOrSteerIndex = WheelDeflections.IndexOf(WheelDeflections.Max());
            }

        }
























    }
}

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
        /// Step Size of the Motion Analysis
        /// </summary>
        public int StepSize;

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

        /// <summary>
        /// Method to populate the <see cref="ToeAngles"/> list with the plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// Method to populate the <see cref="WheelDeflections"/> list with the Plotted Chart Points in the <see cref="BumpSteerCurve"/> Chart
        /// </summary>
        /// <param name="_toeAngleFromChart"></param>
        /// <param name="_wdFromChart"></param>
        public void PopulateBumpSteerGraph(List<double> _wdFromChart, List<double> _toeAngleFromChart)
        {

            ///<summary>Populating the Toe Angles List</summary>
            ToeAngles.Clear();

            for (int i = 0; i < _toeAngleFromChart.Count; i++)
            {
                ToeAngles.Add(new Angle(_toeAngleFromChart[i], AngleUnit.Degrees));
            }


            ///<summary> Populating the Wheel Deflections List</summary>
            WheelDeflections.Clear();

            for (int i = 0; i < _wdFromChart.Count; i++)
            {
                WheelDeflections.Add(_wdFromChart[i]);
            }

            WheelDeflections.Add(0);

            ToeAngles.Add(new Angle());

            ///<summary>Sorting the Toe Angles based on the sorting of Wheel Deflections</summary>
            SortToeList();

            ToeAngles.Insert(0, ToeAngles[0]);

            ToeAngles.Insert(ToeAngles.Count, ToeAngles[ToeAngles.Count - 1]);

            WheelDeflections.Insert(0, WheelDeflections[0]);

            WheelDeflections.Insert(WheelDeflections.Count, WheelDeflections[WheelDeflections.Count - 1]);

            SplitToeAngles();

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

            //wheelDeflections.Reverse();

            //toeAngle.Reverse();

            ///<summary>Converting the temporary <see cref="Array"/>s back to <see cref="List{T}"/>s</summary>
            WheelDeflections = wheelDeflections.ToList();

            ToeAngles.Clear();

            for (int i = 0; i < toeAngle.Length; i++)
            {
                ToeAngles.Add(new Angle(toeAngle[i], AngleUnit.Degrees));
            }

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




    }
}

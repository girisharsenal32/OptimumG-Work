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
        }



        public void PopulateToeAngleList(List<double> _toeAngleFromChart)
        {
            for (int i = 0; i < _toeAngleFromChart.Count; i++)
            {
                ToeAngles.Add(new Angle(_toeAngleFromChart[i], AngleUnit.Degrees));
            }
        }

    }
}

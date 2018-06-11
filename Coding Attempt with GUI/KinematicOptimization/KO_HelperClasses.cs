using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class KO_HelperClasses
    {

    }

    /// <summary>
    /// Enum of <see cref="SuspensionParameters"/> for which the Suspension Coordinates would be Optimized for
    /// </summary>
    public enum SuspensionParameters
    {
        FrontRollCenter,
        RearRollCenter,
        LeftPitchCenter,
        RightPitchCenter,
        CamberVariation,
        BumpSteer,
        Ackermann,
        SpringDeflection
    }

    public enum SuspensionParameterMode
    {
        Static,
        Variation
    }



}


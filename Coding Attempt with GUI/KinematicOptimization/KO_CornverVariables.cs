using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public class KO_CornverVariables
    {
        /// <summary>
        /// <para>Master <see cref="Dictionary{String, KO_AdjToolParams}"/> which holds ALL the coordinate information of the Adjustable coordinates</para> 
        /// <para>This dictionary is crucial and will be used in the Main Optimizer Class</para>
        /// </summary>
        public Dictionary<string,KO_AdjToolParams> KO_MasterAdjs { get; set; }

        /// <summary>
        /// <para><see cref="List{SuspensionParameters}"/> of ALL the parameters requested by the User</para>
        /// <para>This List is crucial and will be used in the Main Optimization Class</para>
        /// <para>---IMPORTANT--- This list will also house the <see cref="SuspensionParameters"/> in the RIGHT ORDER OF IMPORTANCE</para>
        /// </summary>
        public List<SuspensionParameters> KO_ReqParams { get; set; }

        /// <summary>
        /// <para><see cref="Dictionary{SuspensionParameters, Double}"/> which holds the IMportance of each of the <see cref="SuspensionParameters"/></para>
        /// </summary>
        public Dictionary<SuspensionParameters, double> KO_ReqParams_Importance { get; set; }




        public KO_CornverVariables()
        {
            KO_MasterAdjs = new Dictionary<string, KO_AdjToolParams>();

            KO_ReqParams = new List<SuspensionParameters>();

            KO_ReqParams_Importance = new Dictionary<SuspensionParameters, double>();
        }
    }
}

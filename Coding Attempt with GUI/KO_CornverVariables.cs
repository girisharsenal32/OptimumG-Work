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

        


        public KO_CornverVariables()
        {
            KO_MasterAdjs = new Dictionary<string, KO_AdjToolParams>();
        }
    }
}

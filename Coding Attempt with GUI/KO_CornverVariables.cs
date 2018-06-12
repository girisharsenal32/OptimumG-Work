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
        public Dictionary<string,KO_AdjToolParams> KO_MasterAdjs { get; set; }


        public KO_CornverVariables()
        {
            KO_MasterAdjs = new Dictionary<string, KO_AdjToolParams>();
        }
    }
}

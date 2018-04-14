using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Data;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class OutputClassGUI : ISerializable
    {
        #region Output Class Data Table
        public DataTable OC_SC_DataTableGUI = new DataTable("Output Coordinates");
        #endregion

        #region Output Class Banded Grid View 
        public CustomBandedGridView bandedGridView_Outputs = new CustomBandedGridView();
        #endregion

        public OutputClassGUI() { }

        public static void Rounder_Outputs(OutputClass _ocRounder)
        {
            #region Rounding off to 3 decimals
            _ocRounder.scmOP.A1x = Math.Round(_ocRounder.scmOP.A1x, 3);
            _ocRounder.scmOP.A1y = Math.Round(_ocRounder.scmOP.A1y, 3);
            _ocRounder.scmOP.A1z = Math.Round(_ocRounder.scmOP.A1z, 3);

            _ocRounder.scmOP.B1x = Math.Round(_ocRounder.scmOP.B1x, 3);
            _ocRounder.scmOP.B1y = Math.Round(_ocRounder.scmOP.B1y, 3);
            _ocRounder.scmOP.B1z = Math.Round(_ocRounder.scmOP.B1z, 3);

            _ocRounder.scmOP.C1x = Math.Round(_ocRounder.scmOP.C1x, 3);
            _ocRounder.scmOP.C1y = Math.Round(_ocRounder.scmOP.C1y, 3);
            _ocRounder.scmOP.C1z = Math.Round(_ocRounder.scmOP.C1z, 3);

            _ocRounder.scmOP.D1x = Math.Round(_ocRounder.scmOP.D1x, 3);
            _ocRounder.scmOP.D1y = Math.Round(_ocRounder.scmOP.D1y, 3);
            _ocRounder.scmOP.D1z = Math.Round(_ocRounder.scmOP.D1z, 3);

            _ocRounder.scmOP.E1x = Math.Round(_ocRounder.scmOP.E1x, 3);
            _ocRounder.scmOP.E1y = Math.Round(_ocRounder.scmOP.E1y, 3);
            _ocRounder.scmOP.E1z = Math.Round(_ocRounder.scmOP.E1z, 3);

            _ocRounder.scmOP.F1x = Math.Round(_ocRounder.scmOP.F1x, 3);
            _ocRounder.scmOP.F1y = Math.Round(_ocRounder.scmOP.F1y, 3);
            _ocRounder.scmOP.F1z = Math.Round(_ocRounder.scmOP.F1z, 3);

            _ocRounder.scmOP.G1x = Math.Round(_ocRounder.scmOP.G1x, 3);
            _ocRounder.scmOP.G1y = Math.Round(_ocRounder.scmOP.G1y, 3);
            _ocRounder.scmOP.G1z = Math.Round(_ocRounder.scmOP.G1z, 3);

            _ocRounder.scmOP.H1x = Math.Round(_ocRounder.scmOP.H1x, 3);
            _ocRounder.scmOP.H1y = Math.Round(_ocRounder.scmOP.H1y, 3);
            _ocRounder.scmOP.H1z = Math.Round(_ocRounder.scmOP.H1z, 3);

            _ocRounder.scmOP.I1x = Math.Round(_ocRounder.scmOP.I1x, 3);
            _ocRounder.scmOP.I1y = Math.Round(_ocRounder.scmOP.I1y, 3);
            _ocRounder.scmOP.I1z = Math.Round(_ocRounder.scmOP.I1z, 3);

            _ocRounder.scmOP.JO1x = Math.Round(_ocRounder.scmOP.JO1x, 3);
            _ocRounder.scmOP.JO1y = Math.Round(_ocRounder.scmOP.JO1y, 3);
            _ocRounder.scmOP.JO1z = Math.Round(_ocRounder.scmOP.JO1z, 3);

            _ocRounder.scmOP.J1x = Math.Round(_ocRounder.scmOP.J1x, 3);
            _ocRounder.scmOP.J1y = Math.Round(_ocRounder.scmOP.J1y, 3);
            _ocRounder.scmOP.J1z = Math.Round(_ocRounder.scmOP.J1z, 3);

            _ocRounder.scmOP.K1x = Math.Round(_ocRounder.scmOP.K1x, 3);
            _ocRounder.scmOP.K1y = Math.Round(_ocRounder.scmOP.K1y, 3);
            _ocRounder.scmOP.K1z = Math.Round(_ocRounder.scmOP.K1z, 3);

            _ocRounder.scmOP.L1x = Math.Round(_ocRounder.scmOP.L1x, 3);
            _ocRounder.scmOP.L1y = Math.Round(_ocRounder.scmOP.L1y, 3);
            _ocRounder.scmOP.L1z = Math.Round(_ocRounder.scmOP.L1z, 3);

            _ocRounder.scmOP.M1x = Math.Round(_ocRounder.scmOP.M1x, 3);
            _ocRounder.scmOP.M1y = Math.Round(_ocRounder.scmOP.M1y, 3);
            _ocRounder.scmOP.M1z = Math.Round(_ocRounder.scmOP.M1z, 3);

            _ocRounder.scmOP.N1x = Math.Round(_ocRounder.scmOP.N1x, 3);
            _ocRounder.scmOP.N1y = Math.Round(_ocRounder.scmOP.N1y, 3);
            _ocRounder.scmOP.N1z = Math.Round(_ocRounder.scmOP.N1z, 3);

            _ocRounder.scmOP.O1x = Math.Round(_ocRounder.scmOP.O1x, 3);
            _ocRounder.scmOP.O1y = Math.Round(_ocRounder.scmOP.O1y, 3);
            _ocRounder.scmOP.O1z = Math.Round(_ocRounder.scmOP.O1z, 3);

            _ocRounder.scmOP.P1x = Math.Round(_ocRounder.scmOP.P1x, 3);
            _ocRounder.scmOP.P1y = Math.Round(_ocRounder.scmOP.P1y, 3);
            _ocRounder.scmOP.P1z = Math.Round(_ocRounder.scmOP.P1z, 3);

            _ocRounder.scmOP.Q1x = Math.Round(_ocRounder.scmOP.Q1x, 3);
            _ocRounder.scmOP.Q1y = Math.Round(_ocRounder.scmOP.Q1y, 3);
            _ocRounder.scmOP.Q1z = Math.Round(_ocRounder.scmOP.Q1z, 3);

            _ocRounder.scmOP.R1x = Math.Round(_ocRounder.scmOP.R1x, 3);
            _ocRounder.scmOP.R1y = Math.Round(_ocRounder.scmOP.R1y, 3);
            _ocRounder.scmOP.R1z = Math.Round(_ocRounder.scmOP.R1z, 3);

            _ocRounder.scmOP.W1x = Math.Round(_ocRounder.scmOP.W1x, 3);
            _ocRounder.scmOP.W1y = Math.Round(_ocRounder.scmOP.W1y, 3);
            _ocRounder.scmOP.W1z = Math.Round(_ocRounder.scmOP.W1z, 3);
            #endregion
        }

        public OutputClassGUI(SerializationInfo info, StreamingContext context)
        {
            OC_SC_DataTableGUI = (DataTable)info.GetValue("OC_SC_DataTableGUI", typeof(DataTable));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("OC_SC_DataTableGUI", OC_SC_DataTableGUI);

        }
    }
}

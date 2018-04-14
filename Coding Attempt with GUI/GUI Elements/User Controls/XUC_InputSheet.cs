using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    public partial class XtraUserControl_InputSheet : DevExpress.XtraEditors.XtraUserControl
    {
        public static Kinematics_Software_New r1;

        public XtraUserControl_InputSheet(Kinematics_Software_New _r1)
        {
            r1 = _r1;
            InitializeComponent();
        }

        public void Kinematics_Software_New_ObjectInitializer(Kinematics_Software_New _r1)
        {
            r1 = _r1;
        }

        private void RecalculateCornerWeightForPushRodLength_Click(object sender, EventArgs e)
        {
            r1.ReCalculate_Click();
        }

        private void RecalculatePushrodLengthForDesiredCornerWeight_Click(object sender, EventArgs e)
        {
            r1.ReCalculateForDesiredCornerWeight_Click();
        }



        public void XtraUserControl_InputSheet_Load(object sender, EventArgs e)
        {
            if (Vehicle.List_Vehicle[r1.navBarControlResults.Groups.IndexOf(r1.navBarControlResults.ActiveGroup)].Vehicle_Results_Tracker == 1)
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[r1.navBarControlResults.Groups.IndexOf(r1.navBarControlResults.ActiveGroup)].IS.RecalculateCornerWeightForPushRodLength.Enabled = true;
                Kinematics_Software_New.M1_Global.vehicleGUI[r1.navBarControlResults.Groups.IndexOf(r1.navBarControlResults.ActiveGroup)].IS.RecalculatePushrodLengthForDesiredCornerWeight.Enabled = true;
            }
            else
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[r1.navBarControlResults.Groups.IndexOf(r1.navBarControlResults.ActiveGroup)].IS.RecalculateCornerWeightForPushRodLength.Enabled = false;
                Kinematics_Software_New.M1_Global.vehicleGUI[r1.navBarControlResults.Groups.IndexOf(r1.navBarControlResults.ActiveGroup)].IS.RecalculatePushrodLengthForDesiredCornerWeight.Enabled = false;
            }

            
        }
    }
}

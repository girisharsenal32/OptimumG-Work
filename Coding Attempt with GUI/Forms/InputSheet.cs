using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public partial class InputSheet : DevExpress.XtraEditors.XtraForm,ISerializable
    {
        public static int InputSheetCounter = 0;

        public Kinematics_Software_New R1_InputSheet;
        public InputSheet(Kinematics_Software_New r1_InputSheet)
        {
            
            InitializeComponent();
            R1_InputSheet = r1_InputSheet;

        }

        private void RecalculateCornerWeightForPushRodLength_Click(object sender, EventArgs e)
        {
            this.Hide();
            R1_InputSheet.ReCalculate_Click();
            
        }

        private void RecalculatePushrodLengthForDesiredCornerWeight_Click(object sender, EventArgs e)
        {
            this.Hide();
            R1_InputSheet.ReCalculateForDesiredCornerWeight_Click();


        }

        private void Hide_ItemClicked(object sender, EventArgs e)
        {
            this.Hide();
        }


        public InputSheet(SerializationInfo info, StreamingContext context)
        {
            InputSheetCounter = (int)info.GetValue("InputSheet_Counter", typeof(int));
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InputSheet_Counter", InputSheetCounter);
        }

        private void label205_Click(object sender, EventArgs e)
        {

        }

        private void xtraScrollableControl2_Click(object sender, EventArgs e)
        {

        }
    }
}
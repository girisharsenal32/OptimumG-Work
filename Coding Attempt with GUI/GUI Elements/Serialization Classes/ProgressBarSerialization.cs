using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class ProgressBarSerialization : ProgressBarControl
    {

        public ProgressBarSerialization() : base() { InitializeDefault(); InitializeDefaultProperties(); }

        public static ProgressBarSerialization CreateProgressBar(ProgressBarSerialization _progressBar, int _maxValue, int _step)
        {
            
            _progressBar = new ProgressBarSerialization();
            _progressBar.EditValue = 0;
            _progressBar.BringToFront();
            _progressBar.Name = "Progress Bar";
            _progressBar.Properties.Maximum = _maxValue;
            _progressBar.Properties.Step = _step;
            _progressBar.Hide();
            _progressBar.Dock = DockStyle.Right;


            return _progressBar;
        }

        public void AddProgressBarToRibbonStatusBar(Kinematics_Software_New r1, ProgressBarSerialization _progressBar)
        {
            r1.ribbonStatusBar.Controls.Clear();
            r1.ribbonStatusBar.Controls.Add(_progressBar);
        }

        public static Form AddProgressBarToForm(Form _form, ProgressBarSerialization _progressBar,string _formText)
        {
            //_form = new Form();
            
            _form.Text = _formText;
            _form.Controls.Add(_progressBar);
            _form.Show();
            _progressBar.Anchor = AnchorStyles.None;
            _progressBar.Show();
            return _form;
        }

        //public ProgressBarSerialization(SerializationInfo info, StreamingContext context)
        //{
        //    Name = (string)info.GetValue("ProgressBar", typeof(string));
        //    Properties.Maximum = (int)info.GetValue("MaximumValue", typeof(int));
        //    Properties.Step = (int)info.GetValue("Step", typeof(int));
        //}


        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("ProgressBar", Name);
        //    info.AddValue("MaximumValue",Properties.Maximum);
        //    info.AddValue("Step", Properties.Step);
        //}
    }
}

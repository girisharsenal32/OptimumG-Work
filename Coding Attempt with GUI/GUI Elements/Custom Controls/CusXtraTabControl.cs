using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab.Buttons;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class CustomXtraTabControl: XtraTabControl,ISerializable
    {
        public CustomXtraTabControl() : base() { }

        public static void AddNewPage_For_TabControl_Outputs(CustomXtraTabControl _xtraTabControl)
        {
 
        }

        public CustomXtraTabControl(SerializationInfo info, StreamingContext context)
        {
            Visible = (bool)info.GetValue("Visible", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Visible", Visible);
        }
    }
}

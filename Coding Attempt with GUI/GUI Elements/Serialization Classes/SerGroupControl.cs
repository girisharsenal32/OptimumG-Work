using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DevExpress.XtraEditors;





namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class SerGroupControl : GroupControl, ISerializable
    {
        public SerGroupControl() : base() { }


        public SerGroupControl(SerializationInfo info, StreamingContext context)
        {
            //Name = (string)info.GetValue("Name", typeof(string));
            //Text = (string)info.GetValue("Text",, typeof(string));
            Visible = (bool)info.GetValue("Visibility", typeof(bool));
 
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("Name", Name);
            //info.AddValue("Text", Text);
            info.AddValue("Visibility", Visible);
        }
    }
}

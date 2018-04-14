using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;


namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    class SerTabPane : TabPane, ISerializable
    {
        public SerTabPane() : base() { }

        public SerTabPane(SerializationInfo info, StreamingContext context)
        {
            Visible = (bool)info.GetValue("Visibility", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Visibility", Visible);          
        }
    }
}

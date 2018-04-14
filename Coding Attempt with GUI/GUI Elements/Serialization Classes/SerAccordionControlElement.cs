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
    public class SerAccordionControlElement : AccordionControlElement, ISerializable
    {
        public SerAccordionControlElement() : base() { }

        public SerAccordionControlElement(SerializationInfo info, StreamingContext context)
        {
            Expanded = (bool)info.GetValue("Expanded_State", typeof(bool));
            Visible = (bool)info.GetValue("Visibility", typeof(bool));
            Text = (string)info.GetValue("Text", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Expanded_State", Expanded);
            info.AddValue("Visibility", Visible);
            info.AddValue("Text", Text);
        }
    }
}

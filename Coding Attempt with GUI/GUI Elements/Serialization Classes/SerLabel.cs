﻿using System;
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

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    class SerLabel : Label, ISerializable
    {
        public SerLabel() : base() { }

        public SerLabel(SerializationInfo info, StreamingContext context)
        {
            Text = (string)info.GetValue("Text", typeof(string));
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Text", Text);
        }
    }
}

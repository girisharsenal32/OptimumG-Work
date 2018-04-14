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
using DevExpress.XtraBars.Navigation;



namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class SerNavigationPage : NavigationPage, ISerializable
    {
        public SerNavigationPage() : base() { }

        public SerNavigationPage(SerializationInfo info, StreamingContext context)
        {
            Caption = (string)info.GetValue("Caption", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Caption", Caption);
        }
    }
}

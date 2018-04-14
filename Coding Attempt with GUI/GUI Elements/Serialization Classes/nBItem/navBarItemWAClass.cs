using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DevExpress.XtraNavBar;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class navBarItemWAClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {

        #region Creating a navBarItemWAClass list of objects
        public static List<navBarItemWAClass> navBarItemWA = new List<navBarItemWAClass>(); 
        #endregion

        public navBarItemWAClass() : base() { }

        public void CreateNewWAItem(int i_wa, navBarItemWAClass _navBarItemWA, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupWheelAlignment)
        {
            #region Creating a new navBarItem and adding it the Wheel Alignment Group
            navBarItemWAClass temp_navBarItemWA = _navBarItemWA;
            navBarItemWA.Insert(i_wa, temp_navBarItemWA);
            _navBarControl2.Items.Add(navBarItemWA[i_wa]);
            navBarItemWA[i_wa].Name = "WA " + Convert.ToString(WheelAlignment.WheelAlignmentCounter + 1);
            navBarItemWA[i_wa].Caption = "WA " + Convert.ToString(WheelAlignment.WheelAlignmentCounter + 1);
            _navBarGroupWheelAlignment.ItemLinks.Add(navBarItemWA[i_wa]);
            #endregion
        }

        #region De-serialization of the navBarItemTireClass Object's Dataion
        public navBarItemWAClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        } 
        #endregion

        #region Serialization of the navBarItemTireClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        } 
        #endregion
    }
}

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
    public class navBarItemSCFLClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        #region Creating a new navBarItemSCFLClass list of objects
        public static List<navBarItemSCFLClass> navBarItemSCFL = new List<navBarItemSCFLClass>(); 
        #endregion

        public navBarItemSCFLClass() : base() { }

        public void CreateNewNavbarItem(int i_scfl, navBarItemSCFLClass _navBarItemSCFL, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupSuspensionFL)
        {
            #region Creating a new NavBarItem and adding it to the Suspension FL group
            navBarItemSCFLClass temp_navBarItemSCFL = _navBarItemSCFL;
            navBarItemSCFL.Insert(i_scfl, temp_navBarItemSCFL);
            _navBarControl2.Items.Add(navBarItemSCFL[i_scfl]);
            navBarItemSCFL[i_scfl].Name = "Front Left Coordinates " + Convert.ToString(SuspensionCoordinatesFront.SCFLCounter + 1);
            navBarItemSCFL[i_scfl].Caption = "Front Left Coordinates " + Convert.ToString(SuspensionCoordinatesFront.SCFLCounter + 1);
            _navBarGroupSuspensionFL.ItemLinks.Add(navBarItemSCFL[i_scfl]); 
            #endregion
        }

        #region De-serialization of the navBarItemSCFLClass Object's Data
        public navBarItemSCFLClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        }
        #endregion

        #region Serialization of the navBarItemSCFLClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        }
        #endregion
    }
}

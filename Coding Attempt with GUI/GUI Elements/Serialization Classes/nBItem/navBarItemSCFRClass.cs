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
    public class navBarItemSCFRClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        #region Creating a new navBarItemSCFRClass list of objects
        public static List<navBarItemSCFRClass> navBarItemSCFR = new List<navBarItemSCFRClass>(); 
        #endregion

        public navBarItemSCFRClass() : base() { }

        public void CreateNewNarBarItem(int i_scfr, navBarItemSCFRClass _navBarItemSCFR, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupSuspensionFR)
        {
            #region Creating a new NavBarItem and adding it to the Suspension FR group
            navBarItemSCFRClass temp_navBarItemSCFR = _navBarItemSCFR;
            navBarItemSCFR.Insert(i_scfr, temp_navBarItemSCFR);
            _navBarControl2.Items.Add(navBarItemSCFR[i_scfr]);
            navBarItemSCFR[i_scfr].Name = "Front Right Coordinates " + Convert.ToString(SuspensionCoordinatesFrontRight.SCFRCounter + 1);
            navBarItemSCFR[i_scfr].Caption = "Front Right Coordinates " + Convert.ToString(SuspensionCoordinatesFrontRight.SCFRCounter + 1);
            _navBarGroupSuspensionFR.ItemLinks.Add(navBarItemSCFR[i_scfr]);
            #endregion
        }

        #region De-serialization of the navBarItemSCFRClass Object's Data
        public navBarItemSCFRClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        } 
        #endregion

        #region Serialization of the navBarItemSCFRClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        } 
        #endregion
    }
}

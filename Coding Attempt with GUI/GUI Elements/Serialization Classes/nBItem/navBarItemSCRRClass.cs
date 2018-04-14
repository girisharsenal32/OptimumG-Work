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
    public class navBarItemSCRRClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {

        #region Creating an new navBarItemSCRRClass list of objects
        public static List<navBarItemSCRRClass> navBarItemSCRR = new List<navBarItemSCRRClass>();
        #endregion


        public navBarItemSCRRClass() : base() { }

        public void CreateNewNarBarItem(int i_scrr, navBarItemSCRRClass _navBarItemSCRR, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupSuspensionRR)
        {
            #region Creating a new NavBarItem and adding it to the Suspension RR group
            navBarItemSCRRClass temp_navBarItemSCRR = _navBarItemSCRR;
            navBarItemSCRR.Insert(i_scrr, temp_navBarItemSCRR);
            _navBarControl2.Items.Add(navBarItemSCRR[i_scrr]);
            navBarItemSCRR[i_scrr].Name = "Rear Right Coordinates " + Convert.ToString(SuspensionCoordinatesRearRight.SCRRCounter + 1);
            navBarItemSCRR[i_scrr].Caption = "Rear Right Coordinates " + Convert.ToString(SuspensionCoordinatesRearRight.SCRRCounter + 1);
            _navBarGroupSuspensionRR.ItemLinks.Add(navBarItemSCRR[i_scrr]); 
            #endregion
 
        }

        #region De-serialization of the navBarItemSCRRClass Object's Data
        public navBarItemSCRRClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        }
        #endregion

        #region Serialization of the navBarItemSCRRClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        }
        #endregion

    }
}

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
    public class navBarItemSCRLClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        #region Creating a new navBarItemSCRLClass list of objects
        public static List<navBarItemSCRLClass> navBarItemSCRL = new List<navBarItemSCRLClass>(); 
        #endregion

        public navBarItemSCRLClass() : base() { }

        public void CreateNewNavBarItem(int i_scrl, navBarItemSCRLClass _navBarItemSCRL, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupSuspensionRL)
        {
            #region Creating a new NavBarItem and adding it to the Suspension RL group
            navBarItemSCRLClass temp_navBarItemSCRL = _navBarItemSCRL;
            navBarItemSCRL.Insert(i_scrl, temp_navBarItemSCRL);
            _navBarControl2.Items.Add(navBarItemSCRL[i_scrl]);
            navBarItemSCRL[i_scrl].Name = "Rear Left Coordinates " + Convert.ToString(SuspensionCoordinatesRear.SCRLCounter + 1);
            navBarItemSCRL[i_scrl].Caption = "Rear Left Coordinates " + Convert.ToString(SuspensionCoordinatesRear.SCRLCounter + 1);
            _navBarGroupSuspensionRL.ItemLinks.Add(navBarItemSCRL[i_scrl]); 
            #endregion
        }


        #region De-serialization of the navBarItemSCRLClass Object's Data
        public navBarItemSCRLClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        } 
        #endregion

        #region Serialization of the navBarItemSCRLClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        } 
        #endregion
    }
}

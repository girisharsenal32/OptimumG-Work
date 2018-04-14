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
    public class navBarItemARBClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {

        #region Creating a navbarItemARBClass List of Objects
        public static List<navBarItemARBClass> navBarItemARB = new List<navBarItemARBClass>(); 
        #endregion

        public navBarItemARBClass() : base() { }

        public void CreateNewNavBarItem(int i_arb, navBarItemARBClass _navBarItemARB, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupAntiRollBar)
        {
            #region Creating a new NavBarItem and adding it the ARB Group
            navBarItemARBClass temp_navBarItemARB = _navBarItemARB;
            navBarItemARB.Insert(i_arb, temp_navBarItemARB);
            _navBarControl2.Items.Add(navBarItemARB[i_arb]);
            navBarItemARB[i_arb].Name = "Anti-Roll Bar " + Convert.ToString(AntiRollBar.AntiRollBarCounter + 1);
            navBarItemARB[i_arb].Caption = "Anti-Roll Bar " + Convert.ToString(AntiRollBar.AntiRollBarCounter + 1);
            _navBarGroupAntiRollBar.ItemLinks.Add(navBarItemARB[i_arb]);
            #endregion
        }

        #region De-serialization of the navBarItemTireClass Object's Data
        public navBarItemARBClass(SerializationInfo info, StreamingContext context)
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

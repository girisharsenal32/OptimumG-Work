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
    public class navbarItemDamperClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        #region Creating a navBarItemDamperClass List of Objects
        public static List<navbarItemDamperClass> navBarItemDamper = new List<navbarItemDamperClass>();
        #endregion

        public navbarItemDamperClass() : base() { }

        public void CreateNewNavBarItem(int i_damper, navbarItemDamperClass _navBarItemDamper, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupDamper)
        {
            #region Creating a new navBarItem and adding to the Damper Group
            navbarItemDamperClass temp_navBarItemDamper = _navBarItemDamper;
            navBarItemDamper.Insert(i_damper, temp_navBarItemDamper);
            _navBarControl2.Items.Add(navBarItemDamper[i_damper]);
            navBarItemDamper[i_damper].Name = "Damper " + Convert.ToString(Damper.DamperCounter + 1);
            navBarItemDamper[i_damper].Caption = "Damper " + Convert.ToString(Damper.DamperCounter + 1);
            _navBarGroupDamper.ItemLinks.Add(navBarItemDamper[i_damper]);
            #endregion
        }



        #region De-serialization of the navBarItemDamper Class's Objects
        public navbarItemDamperClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));

        } 
        #endregion


        #region Serialization of the navBarItemDamper Class's Objects
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);

        } 
        #endregion
    }
}

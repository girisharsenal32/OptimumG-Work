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
    public class navBarItemChassisClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        #region Creating a list of navBarItemChassis Class
        public static List<navBarItemChassisClass> navBarItemChassis = new List<navBarItemChassisClass>(); 
        #endregion


        public navBarItemChassisClass() : base() { }

        public void CreateNewNavBarItem(int i_chassis, navBarItemChassisClass _navBarItemChassis, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupChassis)
        {
            #region Creating a new navBarItem and adding it the Chassis Group
            navBarItemChassisClass temp_navBarItemChassis = _navBarItemChassis;
            navBarItemChassis.Insert(i_chassis, temp_navBarItemChassis);
            _navBarControl2.Items.Add(navBarItemChassis[i_chassis]);
            navBarItemChassis[i_chassis].Name = "Chassis " + Convert.ToString(Chassis.ChassisCounter + 1);
            navBarItemChassis[i_chassis].Caption = "Chassis " + Convert.ToString(Chassis.ChassisCounter + 1);
            _navBarGroupChassis.ItemLinks.Add(navBarItemChassis[i_chassis]); 
            #endregion
        }


        #region De-serialization of the navBarItemChassisClass Object's Data
        public navBarItemChassisClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        } 
        #endregion


        #region Serialization of the navBarItemChassisClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        } 
        #endregion
    }
}

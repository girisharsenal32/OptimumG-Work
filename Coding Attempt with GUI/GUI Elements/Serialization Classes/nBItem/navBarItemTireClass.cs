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
    public class navBarItemTireClass : DevExpress.XtraNavBar.NavBarItem, ISerializable 
    {
        #region Creating a navbarItemTireClass List of Objects
        public static List<navBarItemTireClass> navBarItemTire = new List<navBarItemTireClass>(); 
        #endregion

        public navBarItemTireClass() : base() { }

        public void CreateNewNavBarItem(int i__tire, navBarItemTireClass _navBarItemTire, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupTireStiffness)
        {
            #region Creating a new NavBarItem and adding it the Tire Group
            navBarItemTireClass temp_navBarItemTire = _navBarItemTire;
            navBarItemTire.Insert(i__tire, temp_navBarItemTire);
            _navBarControl2.Items.Add(navBarItemTire[i__tire]);
            navBarItemTire[i__tire].Name = "Tire " + Convert.ToString(Tire.TireCounter + 1);
            navBarItemTire[i__tire].Caption = "Tire " + Convert.ToString(Tire.TireCounter + 1);
            _navBarGroupTireStiffness.ItemLinks.Add(navBarItemTire[i__tire]);
            #endregion

        }
          
        #region De-serialization of the navBarItemTireClass Object's Data
        public navBarItemTireClass(SerializationInfo info, StreamingContext context)
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

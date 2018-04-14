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
    public class navBarItemSpringClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        public navBarItemSpringClass() : base() { }

        #region Creating a navbarItemTireClass List of Objects
        public static List<navBarItemSpringClass> navBarItemSpring = new List<navBarItemSpringClass>(); 
        #endregion

        public void CreateNewNavBarItem(int i_spring, navBarItemSpringClass _navBarItemSpring, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupSprings)
        {
            #region Creating a new NavBarItem and adding it the Spring Group
            navBarItemSpringClass temp_navBarItemSpring = _navBarItemSpring;
            navBarItemSpring.Insert(i_spring, temp_navBarItemSpring);
            _navBarControl2.Items.Add(navBarItemSpring[i_spring]);
            navBarItemSpring[i_spring].Name = "Spring " + Convert.ToString(Spring.SpringCounter + 1);
            navBarItemSpring[i_spring].Caption = "Spring " + Convert.ToString(Spring.SpringCounter + 1);
            _navBarGroupSprings.ItemLinks.Add(navBarItemSpring[i_spring]);
            #endregion
        }
        
        #region De-serialization of the navBarItemSpringClass Object's Data
        public navBarItemSpringClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string)); 
        }
        #endregion

        #region Serialization of the navBarItemSpringClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {   
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        }
        #endregion
    }
}

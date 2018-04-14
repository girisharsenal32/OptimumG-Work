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
    public class navBarItemVehicleClass : DevExpress.XtraNavBar.NavBarItem, ISerializable
    {
        #region Creating a list of navBarItemVehicleClass objects
        public static List<navBarItemVehicleClass> navBarItemVehicle = new List<navBarItemVehicleClass>(); 
        #endregion

        public navBarItemVehicleClass() : base() { }

        public void CreateNewNavBarItem(int i_vehicle, navBarItemVehicleClass _navBarItemVehicle, NavBarControl _navBarControl2, DevExpress.XtraNavBar.NavBarGroup _navBarGroupVehicle)
        {
            #region Creating a new NavBarItem and adding it to the Tire Group
            navBarItemVehicleClass temp_navBarItemVehicle = _navBarItemVehicle;
            navBarItemVehicle.Insert(i_vehicle, temp_navBarItemVehicle);
            _navBarControl2.Items.Add(navBarItemVehicle[i_vehicle]);
            navBarItemVehicle[i_vehicle].Name = "Vehicle " + Convert.ToString(Vehicle.VehicleCounter + 1);
            navBarItemVehicle[i_vehicle].Caption = "Vehicle " + Convert.ToString(Vehicle.VehicleCounter + 1);
            _navBarGroupVehicle.ItemLinks.Add(navBarItemVehicle[i_vehicle]); 
            #endregion
        }

        #region De-serialization of the navBarItemVehicleClass Object's Data
        public navBarItemVehicleClass(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        }
        #endregion

        #region Serialization of the navBarItemVehicleClass Object's Data
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
        }
        #endregion



    }
}

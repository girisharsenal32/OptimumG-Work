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
    public class CusNavBarGroup : NavBarGroup, ISerializable
    {
        public int navBarID = 0;

        public CusNavBarGroup() : base() { }

        public CusNavBarGroup CreateNewNavBarGroup_For_VehicleResults(CusNavBarGroup _navBarGroup, NavBarControl _navBarControl, int _vehicleID, string _navBarGroupName)
        {
            _navBarGroup = new CusNavBarGroup();

            _navBarGroup.Name = _navBarGroupName + " - Results";
            _navBarGroup.Caption = _navBarGroupName + " - Results";
            _navBarGroup.navBarID = _vehicleID;

            return _navBarGroup;
        }

        #region Re adding the navBarGroupResults ItemLinks for Open
        public CusNavBarGroup ItemLinks_ReAdd(NavBarControl _navBarControl, CusNavBarGroup _navBarGroupReAdd, List<CusNavBarItem> _navBarItemReAdd,Kinematics_Software_New _r1)
        {
            for (int i_ItemLinksReAdd = 0; i_ItemLinksReAdd < _navBarItemReAdd.Count; i_ItemLinksReAdd++)
            {
                _navBarItemReAdd[i_ItemLinksReAdd] = _navBarItemReAdd[0].LinkClickedEventCreator(_navBarItemReAdd[i_ItemLinksReAdd],_r1);
                _navBarControl.Groups[_navBarGroupReAdd.Name].ItemLinks.Add(_navBarItemReAdd[i_ItemLinksReAdd]);
            }
            return _navBarGroupReAdd;

        }
        #endregion

        public NavBarControl Items_ReAdd(NavBarControl _navBarControl, List<CusNavBarItem> _navBarItemReAdd)
        {
            for (int i_ReAdd = 0; i_ReAdd < _navBarItemReAdd.Count; i_ReAdd++)
            {
                _navBarControl.Items.Add(_navBarItemReAdd[i_ReAdd]);
            }
            return _navBarControl;
        }


        public CusNavBarGroup(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
            navBarID = (int)info.GetValue("GroupID", typeof(int));

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);
            info.AddValue("GroupID", navBarID);
        }
    }
}

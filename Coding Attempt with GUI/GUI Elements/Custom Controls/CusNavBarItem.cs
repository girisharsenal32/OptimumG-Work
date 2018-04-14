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
    public class CusNavBarItem : NavBarItem, ISerializable
    {
        public CusNavBarItem() : base() { }

        public static Kinematics_Software_New r1;

        public CusNavBarItem(string _Name, int _ID, object _InputItem)
        {
            Name = _Name;

            if (_InputItem as Vehicle != null)
            {
                Caption = _Name + Vehicle.List_Vehicle[_ID - 1]._VehicleName;
                LinkClicked += new NavBarLinkEventHandler(navBarItemResults_LinkClicked);
            }
            else if (_InputItem as MotionGUI != null)
            {
                Caption = _Name + _ID;
            }
            else if (_InputItem as Simulation != null)
            {
                Caption = _Name + _ID;
            }
            else if (_InputItem as LoadCaseGUI != null)
            {
                Caption = _Name + " - #" + _ID;
            }
            else if (_InputItem as LoadCase != null)
            {
                Caption = _Name;
            }
            else if (_InputItem as SetupChange_GUI != null)
            {
                Caption = _Name + _ID;
            }
            else if (_InputItem as BatchRunGUI != null)
            {
                Caption = _Name/* + " " + _ID*/;
            }
            else if (_InputItem as HeatMapWorksheet != null)
            {
                Caption = _Name;
            }

        }

        public CusNavBarItem LinkClickedEventCreator(CusNavBarItem _navBarItemResults,Kinematics_Software_New _r1)
        {
            _navBarItemResults.LinkClicked += new NavBarLinkEventHandler(navBarItemResults_LinkClicked);

            r1 = _r1;

            return _navBarItemResults;
        }

         #region NavBarItem Link Clicked event for Vehicle 
        public void navBarItemResults_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string Name = e.Link.Group.Name;
            int SelectedPage = 0;
            int index = 0;

            for (int i = 0; i < Kinematics_Software_New.M1_Global.vehicleGUI.Count; i++)
            {
                if (Name == Kinematics_Software_New.M1_Global.vehicleGUI[i].navBarGroup_Vehicle_Result.Name)
                {
                    index = Kinematics_Software_New.M1_Global.vehicleGUI[i].navBarGroup_Vehicle_Result.navBarID;
                    index = index - 1;
                    break;
                }
            }

            //r1.gridControl2.DataSource = Vehicle.List_Vehicle[index].vehicle_Motion.Motion_DataTable;
            //r1.gridControl2.MainView = MotionGUI.List_MotionGUI[Vehicle.List_Vehicle[index].vehicle_Motion.MotionID - 1].bandedGridView_Motion;

            #region Events
            if (e.Link.ItemName == "Input Sheet- ")
            {
                //Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[0].PageVisible = true;
                //SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[0]);
                //Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                //Kinematics_Software_New.M1_Global.vehicleGUI[index].IS.Visible = true;
            }
            if (e.Link.ItemName == "Suspension Coordinates- ")
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[1].PageVisible = true;
                SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[1]);
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                Kinematics_Software_New.M1_Global.vehicleGUI[index].xtraScrollableControl_OutputCoordinates.Visible = true;
            }
            else if (e.Link.ItemName == "Wishbone Forces- ")
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[2].PageVisible = true;
                SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[2]);
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                Kinematics_Software_New.M1_Global.vehicleGUI[index].WF.Visible = true;
            }
            else if (e.Link.ItemName == "Corner Weights, Deflections and Wheel Alignment- ")
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[3].PageVisible = true;
                SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[3]);
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                Kinematics_Software_New.M1_Global.vehicleGUI[index].CW_Def_WA.Visible = true;
            }
            else if (e.Link.ItemName == "Vehicle Outputs- ")
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[4].PageVisible = true;
                SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[4]);
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                Kinematics_Software_New.M1_Global.vehicleGUI[index].VO.Visible = true;
            }
            else if (e.Link.ItemName == "Link Lengths- ")
            {
                Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[5].PageVisible = true;
                SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[5]);
                Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                Kinematics_Software_New.M1_Global.vehicleGUI[index].LL.Visible = true;
            }
            else if (e.Link.ItemName == "CAD- ")
            {
                try
                {
                    Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[6].PageVisible = true;
                    SelectedPage = Kinematics_Software_New.TabControl_Outputs.TabPages.IndexOf(Kinematics_Software_New.M1_Global.vehicleGUI[index].TabPages_Vehicle[6]);
                    Kinematics_Software_New.TabControl_Outputs.SelectedTabPageIndex = SelectedPage;
                    Kinematics_Software_New.M1_Global.vehicleGUI[index].CADVehicleOutputs.Visible = true;
                }
                catch (Exception)
                {

                    //To prevent exceptions thrown by devDeptv10 Graphics Viewport
                }
            }
        }
        #endregion

        #endregion

        #region Create New NavBarItem for Vehicle Results
        /// <summary>
        /// Method to create <see cref="NavBarItem"/>s specifically for <see cref="Vehicle"/> Result
        /// </summary>
        /// <param name="_navBarItemResuls_List"></param>
        /// <param name="_navBarGroupResults"></param>
        /// <param name="_navBarControl"></param>
        /// <param name="_vehicleID"></param>
        /// <param name="_r1"></param>
        public void CreateNavBarItem(List<CusNavBarItem> _navBarItemResuls_List, CusNavBarGroup _navBarGroupResults, NavBarControl _navBarControl, int _vehicleID, Kinematics_Software_New _r1)
        {
            r1 = _r1;

            _navBarItemResuls_List.Insert(0, new CusNavBarItem("Input Sheet- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));
            _navBarItemResuls_List.Insert(1, new CusNavBarItem("Suspension Coordinates- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));
            _navBarItemResuls_List.Insert(2, new CusNavBarItem("Wishbone Forces- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));
            _navBarItemResuls_List.Insert(3, new CusNavBarItem("Corner Weights, Deflections and Wheel Alignment- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));
            _navBarItemResuls_List.Insert(4, new CusNavBarItem("Vehicle Outputs- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));
            _navBarItemResuls_List.Insert(5, new CusNavBarItem("Link Lengths- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));
            _navBarItemResuls_List.Insert(6, new CusNavBarItem("CAD- ", _vehicleID, Vehicle.List_Vehicle[_vehicleID - 1]));

            _navBarControl.Items.Add(_navBarItemResuls_List[0]);
            _navBarControl.Items[0].Visible = true;
            _navBarControl.Items.Add(_navBarItemResuls_List[1]);
            _navBarControl.Items[1].Visible = true;
            _navBarControl.Items.Add(_navBarItemResuls_List[2]);
            _navBarControl.Items[2].Visible = true;
            _navBarControl.Items.Add(_navBarItemResuls_List[3]);
            _navBarControl.Items[3].Visible = true;
            _navBarControl.Items.Add(_navBarItemResuls_List[4]);
            _navBarControl.Items[4].Visible = true;
            _navBarControl.Items.Add(_navBarItemResuls_List[5]);
            _navBarControl.Items[5].Visible = true;
            _navBarControl.Items.Add(_navBarItemResuls_List[6]);
            _navBarControl.Items[6].Visible = true;


            _navBarGroupResults.GroupStyle = NavBarGroupStyle.Default;

            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[0]);
            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[1]);
            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[2]);
            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[3]);
            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[4]);
            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[5]);
            _navBarGroupResults.ItemLinks.Add(_navBarItemResuls_List[6]);


        }




        public void CreateNavBarItem(CusNavBarItem _navBarItem, NavBarGroup _navBarGroup, NavBarControl _navBarControl)
        {
            _navBarControl.Items.Add(_navBarItem);

            _navBarGroup.ItemLinks.Add(_navBarItem);
        }
        #endregion

        public CusNavBarItem(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Caption = (string)info.GetValue("Caption", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Caption);

        }
    }
}

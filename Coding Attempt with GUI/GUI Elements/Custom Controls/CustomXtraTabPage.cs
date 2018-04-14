using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab.Buttons;


namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class CustomXtraTabPage : XtraTabPage, ISerializable
    {
        public CustomXtraTabPage() : base() { }

        public static CustomXtraTabPage CreateNewTabPage_ForVehicleOutputs(string _TabPageName, int _VehicleID)
        {
            CustomXtraTabPage xtraTabPage = new CustomXtraTabPage();
            xtraTabPage.Name = _TabPageName;
            xtraTabPage.Text = _TabPageName + Vehicle.List_Vehicle[_VehicleID - 1]._VehicleName;

            return xtraTabPage;
        }
        public static CustomXtraTabPage CreateNewTabPage_ForInputs(string _TabPageName, int _ID)
        {
            CustomXtraTabPage xtraTabPage = new CustomXtraTabPage();
            xtraTabPage.Name = _TabPageName;
            xtraTabPage.Text = _TabPageName + " " + _ID;
            return xtraTabPage;
        }
        public static CustomXtraTabPage CreateNewTabPage_General(string _TabPageName)
        {
            CustomXtraTabPage xtraTabPage = new CustomXtraTabPage();

            xtraTabPage.Name = _TabPageName;
            xtraTabPage.Text = _TabPageName;

            return xtraTabPage;
        }


        #region Method to Add new TabPages to the TabControl

        /// <summary>
        /// Method to add <see cref="TabPage"/>s to the <see cref="TabControl"/> using a single <see cref="TabPage"/>
        /// </summary>
        /// <param name="_xtraTabControl"></param>
        /// <param name="_xtraTabPage"></param>
        /// <returns></returns>
        public static CustomXtraTabControl AddTabPages(CustomXtraTabControl _xtraTabControl, CustomXtraTabPage _xtraTabPage)
        {
            _xtraTabControl.Dock = DockStyle.Fill;
            _xtraTabControl.TabPages.Add(_xtraTabPage);

            return _xtraTabControl;

        }
        /// <summary>
        /// Method to add <see cref="TabPage"/>s to the <see cref="TabControl"/> using a <see cref="List{T}"/> of <see cref="TabPage"/>s
        /// </summary>
        /// <param name="_xtraTabContorl"></param>
        /// <param name="_xtraTabPages_List"></param>
        /// <returns></returns>
        public static CustomXtraTabControl AddTabPages(CustomXtraTabControl _xtraTabContorl, List<CustomXtraTabPage> _xtraTabPages_List)
        {
            _xtraTabContorl.Dock = DockStyle.Fill;

            for (int i = 0; i < _xtraTabPages_List.Count; i++)
            {
                _xtraTabContorl.TabPages.Add(_xtraTabPages_List[i]);
            }

            return _xtraTabContorl;
        }
        /// <summary>
        /// Method to add <see cref="TabPage"/>s to the <see cref="TabControl"/> using a <see cref="Dictionary{TKey, TValue}"/> of <see cref="TabPage"/>s
        /// </summary>
        /// <param name="_xtraTabContorl"></param>
        /// <param name="_xtraTabPages_Dictionary"></param>
        /// <returns></returns>
        public static CustomXtraTabControl AddTabPages(CustomXtraTabControl _xtraTabContorl, Dictionary<string, CustomXtraTabPage> _xtraTabPages_Dictionary)
        {
            _xtraTabContorl.Dock = DockStyle.Fill;

            foreach (string tabPageName in _xtraTabPages_Dictionary.Keys)
            {
                if (!_xtraTabContorl.TabPages.Contains(_xtraTabPages_Dictionary[tabPageName]))
                {
                    _xtraTabContorl.TabPages.Add(_xtraTabPages_Dictionary[tabPageName]); 
                }
            }

            return _xtraTabContorl;
        }
        #endregion

        #region Method to clear Specific Tab Pages
        public static CustomXtraTabControl ClearTabPages(CustomXtraTabControl _xtraTabControl, List<CustomXtraTabPage> _xtraTabPages_List)
        {
            try
            {
                for (int i_TabControlClear = 0; i_TabControlClear < _xtraTabPages_List.Count; i_TabControlClear++)
                {
                    int index = _xtraTabControl.TabPages.IndexOf(_xtraTabPages_List[i_TabControlClear]);
                    _xtraTabControl.TabPages.RemoveAt(index);
                }
            }
            catch (Exception E)
            {
                string message = E.Message;
                string source = E.Source;
                // Will prevent exception incase the tabpages list is empty, i.e, the calculations are being done for the first time OR if an edited vehicle is being recalculated
            }

            return _xtraTabControl;
        }
        #endregion

        public static CustomXtraTabPage AddUserControlToTabPage(XtraUserControl xtraUserControl, CustomXtraTabPage customXtraTabPage, DockStyle dockStyle)
        {
            customXtraTabPage.Controls.Add(xtraUserControl);
            xtraUserControl.Dock = dockStyle;

            return customXtraTabPage;

        }

        public CustomXtraTabPage(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Text = (string)info.GetValue("Caption", typeof(string));
            PageVisible = (bool)info.GetValue("Visible", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Caption", Text);
            info.AddValue("Visible", PageVisible);
        }
    }
}

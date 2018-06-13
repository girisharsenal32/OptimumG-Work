using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    public partial class AdjustableListBox : XtraUserControl
    {
        //public List<SuspensionParameters> Req_Params_Importance { get; set; }

        //public VehicleCorner VCorner { get; set; }

        public KO_CornverVariables KO_CV { get; set; }

        //public AdjustableListBox CounterList_FrontRear { get; set; }

        //public AdjustableListBox CounterList_LeftRight { get; set; }

        public AdjustableListBox()
        {
            InitializeComponent();

            //Req_Params_Importance = new List<SuspensionParameters>();
        }

        /// <summary>
        /// Method to obtain <see cref="KO_CornverVariables"/> object corresponding to correct Vehicle Corner of which this class is a part of 
        /// </summary>
        /// <param name="_koCV"></param>
        public void GetCornerVariableObject(KO_CornverVariables _koCV/*, VehicleCorner _vCorner*/)
        {
            KO_CV = _koCV;

            //VCorner = _vCorner;
        }

        /// <summary>
        /// Event fired when the <see cref="simpleButtonUP"/> is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUP_Click(object sender, EventArgs e)
        {
            int index = -1;

            if (listBoxControl1.Items.Count != 0 && listBoxControl1.Items.Count != 1)
            {
                index = listBoxControl1.SelectedIndex;
                MoveSelectedItem_UP(index);

                UpdateCornverVariablesObject();
            }
        }

        /// <summary>
        /// Method to move the Selected Item upwards in the ListBox
        /// </summary>
        /// <param name="_index"></param>
        private void MoveSelectedItem_UP(int _index)
        {
            object selectedItem = listBoxControl1.Items[_index];

            if (_index != 0)
            {

                if (_index != 1) 
                {
                    listBoxControl1.Items.Remove(selectedItem);
                    listBoxControl1.Items.Insert(_index - 1, selectedItem); 
                }
                else
                {
                    listBoxControl1.Items.Remove(selectedItem);
                    listBoxControl1.Items.Insert(0, selectedItem);
                }

            }
        }

        /// <summary>
        /// Event fired when the <see cref="simpleButtonDown"/> is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDown_Click(object sender, EventArgs e)
        {
            int index = -1;

            if (listBoxControl1.Items.Count != 0 && listBoxControl1.Items.Count != 1) 
            {
                index = listBoxControl1.SelectedIndex;
                MoveSelectedItem_Down(index);

                UpdateCornverVariablesObject();
            }
        }

        /// <summary>
        /// Method to move the selected item Down
        /// </summary>
        /// <param name="_index"></param>
        public void MoveSelectedItem_Down(int _index)
        {
            object selectedItem = listBoxControl1.Items[_index];

            if (_index != listBoxControl1.Items.Count - 1) 
            {
                if (_index != listBoxControl1.Items.Count - 2) 
                {
                    listBoxControl1.Items.Remove(selectedItem);
                    listBoxControl1.Items.Insert(_index + 1, selectedItem); 
                }
                else
                {
                    listBoxControl1.Items.Remove(selectedItem);
                    listBoxControl1.Items.Add(selectedItem);
                }
            }
        }

        public void listBoxControl1_Validated(object sender, EventArgs e)
        {
            UpdateCornverVariablesObject();
        }

        private void UpdateCornverVariablesObject()
        {
            KO_CV.KO_ReqParams.Clear();

            for (int i = 0; i < listBoxControl1.Items.Count; i++)
            {
                KO_CV.KO_ReqParams.Add((SuspensionParameters)listBoxControl1.Items[i]);
            }

        }

    }
}

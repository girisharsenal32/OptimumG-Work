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
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public partial class XUC_TranslateObject : XtraUserControl
    {
        CAD CAD;

        public XUC_TranslateObject() => InitializeComponent();

        public void GetCADObject(CAD _cad)
        {
            CAD = _cad;
        }

        private void simpleButtonTranslateObject_Click(object sender, EventArgs e)
        {
            double TranslationX = 0, TranslationY = 0, TranslationZ = 0; 

            if ((Double.TryParse(textBoxTranslateX.Text, out TranslationZ)) && (Double.TryParse(textBoxTranslateY.Text, out TranslationX)) && (Double.TryParse(textBoxTranslateY.Text, out TranslationZ)))
            {
                CAD.GetTranslationXYZ(TranslationX, TranslationY, TranslationZ, (string)listBoxItemsWhichCanBeTranslated.SelectedItem);
            }
            else { MessageBox.Show("Please enter numeric values"); return; }

            if (listBoxItemsWhichCanBeTranslated.SelectedIndex == -1)
            {
                if (listBoxItemsWhichCanBeTranslated.Items.Count != 0)
                {
                    listBoxItemsWhichCanBeTranslated.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Please Map Components First");
                    return;
                }
            }
        }

        private void simpleButtonCloseControl_Click(object sender, EventArgs e)
        {

        }
    }
}

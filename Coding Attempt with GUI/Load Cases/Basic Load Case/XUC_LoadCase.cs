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
    public partial class XUC_LoadCase : DevExpress.XtraEditors.XtraUserControl
    {
        public XUC_LoadCase()
        {
            InitializeComponent();
        }

        public bool InocrrectInput;

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TextValidation(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class XUC_RotateObject : DevExpress.XtraEditors.XtraUserControl
    {
        CAD CAD;
        public XUC_RotateObject()
        {
            InitializeComponent();
            comboBoxAxisOfRotation.SelectedIndex = 0;
        }

        public void GetCADObject(CAD _cad)
        {
            CAD = _cad;
        }

        private void simpleButtonRotateObject_Click(object sender, EventArgs e)
        {
            double RotationAngle;
            Vector3D AxisOfRotation;

            if (Double.TryParse(textBoxPointOfRotationX.Text, out double X) && Double.TryParse(textBoxPointOfRotationY.Text, out double Y) && Double.TryParse(textBoxPointOfRotationZ.Text, out double Z) && Double.TryParse(textBoxRotateObject.Text, out double A))
            {
                RotationAngle = Convert.ToDouble(textBoxRotateObject.Text);

                if ((string)comboBoxAxisOfRotation.SelectedItem == "X Axis")
                {
                    AxisOfRotation = Vector3D.AxisZ;
                }
                else if ((string)comboBoxAxisOfRotation.SelectedItem == "Y Axis")
                {
                    AxisOfRotation = Vector3D.AxisX;
                }
                else
                {
                    AxisOfRotation = Vector3D.AxisY;
                }
                Point3D PointOfRotation = new Point3D();
                PointOfRotation = new Point3D(X, Y, Z);

                if (listBoxItemsWhichCanBeRotated.SelectedIndex == -1)
                {
                    if (listBoxItemsWhichCanBeRotated.Items.Count != 0)
                    {
                        listBoxItemsWhichCanBeRotated.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Please Map Components First");
                        return;
                    }
                }

                CAD.GetRotationAngleAndAxisOfRotation(RotationAngle, AxisOfRotation, PointOfRotation, (string)listBoxItemsWhichCanBeRotated.SelectedItem);

            }
            else return;
            
        }

        private void simpleButtonCloseControl_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void listBoxItemsWhichCanBeRotated_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)listBoxItemsWhichCanBeRotated.SelectedItem == "Suspended Mass")
            {
                textBoxPointOfRotationX.Text = Convert.ToString(0);
                textBoxPointOfRotationY.Text = Convert.ToString(293);
                textBoxPointOfRotationZ.Text = Convert.ToString(-770);
            }
            else if ((string)listBoxItemsWhichCanBeRotated.SelectedItem == "Front Left Non Suspended Mass")
            {
                textBoxPointOfRotationX.Text = Convert.ToString(623);
                textBoxPointOfRotationY.Text = Convert.ToString(-50);
                textBoxPointOfRotationZ.Text = Convert.ToString(0);
            }
            else if ((string)listBoxItemsWhichCanBeRotated.SelectedItem == "Front Right Non Suspended Mass")
            {
                textBoxPointOfRotationX.Text = Convert.ToString(-623);
                textBoxPointOfRotationY.Text = Convert.ToString(-50);
                textBoxPointOfRotationZ.Text = Convert.ToString(0);

            }
            else if ((string)listBoxItemsWhichCanBeRotated.SelectedItem == "Rear Left Non Suspended Mass")
            {
                textBoxPointOfRotationX.Text = Convert.ToString(575);
                textBoxPointOfRotationY.Text = Convert.ToString(-50);
                textBoxPointOfRotationZ.Text = Convert.ToString(-1550);
            }
            else if ((string)listBoxItemsWhichCanBeRotated.SelectedItem == "Rear Right Non Suspended Mass")
            {
                textBoxPointOfRotationX.Text = Convert.ToString(-575);
                textBoxPointOfRotationY.Text = Convert.ToString(-50);
                textBoxPointOfRotationZ.Text = Convert.ToString(-1550);
            }
        }
    }
}

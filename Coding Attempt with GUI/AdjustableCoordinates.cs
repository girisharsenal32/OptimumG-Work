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
using DevExpress.XtraVerticalGrid.Rows;

namespace Coding_Attempt_with_GUI
{
    public partial class AdjustableCoordinates : XtraUserControl
    {
        /// <summary>
        /// MultiEditorRow - Lower Front - Upper Limit
        /// </summary>
        MultiEditorRow mr_LowerFront_UpperLimits;
        /// <summary>
        /// MultiEditorRow - Lower Front - Lower Limit
        /// </summary>
        MultiEditorRow mr_LowerFront_LowerLimits;
        /// <summary>
        /// Editor Row -Lower Front - Upper 
        /// </summary>
        EditorRow er_LowerFront_Upper;
        /// <summary>
        /// Editor Row - Lower Front - Lower
        /// </summary>
        EditorRow er_LowerFront_Lower;
        /// <summary>
        /// Category Row - Lower Front 
        /// </summary>
        CategoryRow c_LowerFront;


        MultiEditorRow mr_LowerRear_UpperLimits;
        MultiEditorRow mr_LowerRear_LowerLimits;
        EditorRow er_LowerRear_Upper;
        EditorRow er_LowerRear_Lower;
        CategoryRow c_LowerRear;


        MultiEditorRow mr_UpperFront_UpperLimits;
        MultiEditorRow mr_UpperFront_LowerLimits;
        EditorRow er_UpperFront_Upper;
        EditorRow er_UpperFront_Lower;
        CategoryRow c_UpperFront;


        MultiEditorRow mr_UpperRear_UpperLimits;
        MultiEditorRow mr_UpperRear_LowerLimits;
        EditorRow er_UpperRear_Upper;
        EditorRow er_UpperRear_Lower;
        CategoryRow c_UpperRear;


        MultiEditorRow mr_PushrodInboard_UpperLimits;
        MultiEditorRow mr_PushrodInboard_LowerLimits;
        EditorRow er_PushrodInboard_Upper;
        EditorRow er_PushrodInboard_Lower;
        CategoryRow c_PushrodInboard;



        MultiEditorRow mr_ToeLinkInboard_UpperLimits;
        MultiEditorRow mr_ToeLinkInboard_LowerLimits;
        EditorRow er_ToeLinkInboard_Upper;
        EditorRow er_ToeLinkInboard_Lower;
        CategoryRow c_ToeLinkInboard;


        MultiEditorRow mr_UBJ_UpperLimits;
        MultiEditorRow mr_UBJ_LowerLimits;
        EditorRow er_UBJ_Upper;
        EditorRow er_UBJ_Lower;
        CategoryRow c_UBJ;


        MultiEditorRow mr_LBJ_UpperLimits;
        MultiEditorRow mr_LBJ_LowerLimits;
        EditorRow er_LBJ_Upper;
        EditorRow er_LBJ_Lower;
        CategoryRow c_LBJ;


        MultiEditorRow mr_PushrodOutboard_UpperLimits;
        MultiEditorRow mr_PushrodOutboard_LowerLimits;
        EditorRow er_PushrodOutboard_Upper;
        EditorRow er_PushrodOutboard_Lower;
        CategoryRow c_PushrodOutboard;


        MultiEditorRow mr_ToeLinkOoutboard_UpperLimits;
        MultiEditorRow mr_ToeLinkOoutboard_LowerLimits;
        EditorRow er_ToeLinkOoutboard_Upper;
        EditorRow er_ToeLinkOoutboard_Lower;
        CategoryRow c_ToeLinkOutboard;


        MultiEditorRow mr_WheelCenter_UpperLimits;
        MultiEditorRow mr_WheelCenter_LowerLimits;
        EditorRow er_WheelCenter_Upper;
        EditorRow er_WheelCenter_Lower;
        CategoryRow c_WheelCenter;

        List<CategoryRow> Inboard_CategoryRows;

        List<CategoryRow> Outboard_CategoryRows;


        public AdjustableCoordinates()
        {
            InitializeComponent();

            InitializePropertyGrid();

            category.Visible = false;
        }

        /// <summary>
        /// Method to Initialize and Populate the <see cref="vGridControl1"/> (which displays to the user the range of all SELECTED Adjustable Coordinates) at Run Timi
        /// Done this way because creating the <see cref="vGridControl1"/> at Design is too complicated as there any many bugs
        /// </summary>
        private void InitializePropertyGrid()
        {
            Inboard_CategoryRows = new List<CategoryRow>();

            ///<summary>Inboard Point GUI Row Creation</summary>
            c_LowerFront = CreateRows("Lower Front", out mr_LowerFront_UpperLimits, out mr_LowerFront_LowerLimits, out er_LowerFront_Upper, out er_LowerFront_Lower);
            vGridControl1.Rows.Add(c_LowerFront);
            
            c_LowerRear = CreateRows("Lower Rear", out mr_LowerRear_UpperLimits, out mr_LowerRear_LowerLimits, out er_LowerRear_Upper, out er_LowerRear_Lower);
            vGridControl1.Rows.Add(c_LowerRear);

            c_UpperFront = CreateRows("Upper Front", out mr_UpperFront_UpperLimits, out mr_UpperFront_LowerLimits, out er_UpperFront_Upper, out er_UpperFront_Lower);
            vGridControl1.Rows.Add(c_UpperFront);

            c_UpperRear = CreateRows("Upper Rear", out mr_UpperRear_UpperLimits, out mr_UpperRear_LowerLimits, out er_UpperRear_Upper, out er_UpperRear_Lower);
            vGridControl1.Rows.Add(c_UpperRear);

            c_PushrodInboard = CreateRows("Pushrod Inboard", out mr_PushrodInboard_UpperLimits, out mr_PushrodInboard_LowerLimits, out er_PushrodInboard_Upper, out er_PushrodInboard_Lower);
            vGridControl1.Rows.Add(c_PushrodInboard);

            c_ToeLinkInboard = CreateRows("Toe Link Inboard", out mr_ToeLinkInboard_UpperLimits, out mr_ToeLinkInboard_LowerLimits, out er_ToeLinkInboard_Upper, out er_ToeLinkInboard_Lower);
            vGridControl1.Rows.Add(c_ToeLinkInboard);


            ///<summary>Outboard Point GUI Row Creation</summary>
            c_UBJ = CreateRows("UBJ", out mr_UBJ_UpperLimits, out mr_UBJ_LowerLimits, out er_UBJ_Upper, out er_UBJ_Lower);
            vGridControl1.Rows.Add(c_UBJ);

            c_LBJ = CreateRows("LBJ", out mr_LBJ_UpperLimits, out mr_LBJ_LowerLimits, out er_LBJ_Upper, out er_LBJ_Lower);
            vGridControl1.Rows.Add(c_LBJ);

            c_PushrodOutboard = CreateRows("Pushrod Upright", out mr_PushrodOutboard_UpperLimits, out mr_PushrodOutboard_LowerLimits, out er_PushrodOutboard_Upper, out er_PushrodOutboard_Lower);
            vGridControl1.Rows.Add(c_PushrodOutboard);

            c_ToeLinkOutboard = CreateRows("ToeLinkOoutboard", out mr_ToeLinkOoutboard_UpperLimits, out mr_ToeLinkOoutboard_LowerLimits, out er_ToeLinkOoutboard_Upper, out er_ToeLinkOoutboard_Lower);
            vGridControl1.Rows.Add(c_ToeLinkOutboard);

            c_WheelCenter = CreateRows("Wheel Center", out mr_WheelCenter_UpperLimits, out mr_WheelCenter_LowerLimits, out er_WheelCenter_Upper, out er_WheelCenter_Lower);
            vGridControl1.Rows.Add(c_WheelCenter);
            


            vGridControl1.Refresh();

            Inboard_CategoryRows.AddRange(new CategoryRow[] { c_LowerFront, c_LowerRear, c_UpperFront, c_UpperRear, c_PushrodInboard, c_ToeLinkInboard });

            Outboard_CategoryRows.AddRange(new CategoryRow[] { c_UBJ, c_LBJ, c_PushrodOutboard, c_ToeLinkOutboard, c_WheelCenter });

        }

        /// <summary>
        /// Method to an Adjustbile Coordinate Row SET. That is each call of this method creates 2 MultiEditorRows with 3 Rows (for X/Y/Z), 2 Editor Rows which represenet Upper and Lower Range of the Adjustible
        /// Coordinate and 1 category row which houses all the above mentioned rows
        /// </summary>
        /// <param name="_name">Caption of the Category</param>
        /// <param name="mr_Upper">Multi-EditorRow which will house the Upper Range of the Adjustible Coordinate</param>
        /// <param name="mr_Lower">Multi-EditorRow which will house the Lower Range of the Adjustible Coordinate</param>
        /// <param name="er_Upper">Editor Row which will house the Upper <paramref name="mr_Upper"/></param>
        /// <param name="er_Lower">Editor Row which will house the Upper <paramref name="mr_Lower"/></param>
        /// <returns><see cref="CategoryRow"/> containing all the Rows passed as parameters</returns>
        private CategoryRow CreateRows(string _name, out MultiEditorRow mr_Upper, out MultiEditorRow mr_Lower, out EditorRow er_Upper, out EditorRow er_Lower)
        {
            ///<summary>Creating the Upper Limit's Multi-Editor Row</summary>
            mr_Upper = new MultiEditorRow();

            MultiEditorRowProperties m1 = new MultiEditorRowProperties();
            m1.Caption = "X";
            MultiEditorRowProperties m2 = new MultiEditorRowProperties();
            m2.Caption = "Y";
            MultiEditorRowProperties m3 = new MultiEditorRowProperties();
            m3.Caption = "Z";

            mr_Upper.PropertiesCollection.AddRange(new MultiEditorRowProperties[] { m1, m2, m3 });
            mr_Upper.Enabled = false;



            ///<summary>Creating the Lower Limit's Multi-Editor Row</summary>
            mr_Lower = new MultiEditorRow();

            MultiEditorRowProperties m4 = new MultiEditorRowProperties();
            m4.Caption = "X";
            MultiEditorRowProperties m5 = new MultiEditorRowProperties();
            m5.Caption = "Y";
            MultiEditorRowProperties m6 = new MultiEditorRowProperties();
            m6.Caption = "Z";

            mr_Lower.PropertiesCollection.AddRange(new MultiEditorRowProperties[] { m4, m5, m6 });
            mr_Lower.Enabled = false;


            ///<summary>Creating the Upper Limit's Editor Row which will house the Upper Multi-Editor Row</summary>
            er_Upper = new EditorRow();
            er_Upper.Properties.Caption = "Upper";
            er_Upper.Properties.AllowEdit = false;
            er_Upper.ChildRows.Add(mr_Upper);
            er_Upper.Enabled = false;


            ///<summary>Creating the Lower Limit's Editor Row which will house the Lower Multi-Editor Row</summary>
            er_Lower = new EditorRow();
            er_Lower.Properties.Caption = "Lower";
            er_Lower.Properties.AllowEdit = false;
            er_Lower.ChildRows.Add(mr_Lower);
            er_Lower.Enabled = false;
            

            ///<summary>The Category which will house all the above rowss</summary>
            CategoryRow category = new CategoryRow(_name);
            
            category.ChildRows.AddRange(new BaseRow[] { er_Upper, er_Lower });

            return category;
        }

        private void clb_Inboard_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if ((string)clb_InboardAdjPoints.Items[e.Index].Value == "Lower Front") 
            {

            }
        }
    }
}

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
using DevExpress.XtraEditors.Controls;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public partial class KO_AdjustableCoordinates : XtraUserControl
    {
        #region ---Declarations---

        #region --Grid Control Requirements (Rows)---
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
        #endregion

        public KO_CornverVariables KO_CV = new KO_CornverVariables();

        #endregion


        public KO_AdjustableCoordinates()
        {
            InitializeComponent();

            InitializePropertyGrid();

            InitializeListBoxes();

            category.Visible = false;

        }

        public void GetParentObjectData(KO_CornverVariables _koCV)
        {
            KO_CV = _koCV;
        }

        #region ---GridControl Initializer Methods---
        /// <summary>
        /// Method to Initialize and Populate the <see cref="vGridControl1"/> (which displays to the user the range of all SELECTED Adjustable Coordinates) at Run Timi
        /// Done this way because creating the <see cref="vGridControl1"/> at Design is too complicated as there any many bugs
        /// </summary>
        private void InitializePropertyGrid()
        {
            Inboard_CategoryRows = new List<CategoryRow>();

            Outboard_CategoryRows = new List<CategoryRow>();

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

            vGridControl1.CellValueChanged += VGridControl1_CellValueChanged;

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
            mr_Upper.Name = "UpperLimit";

            MultiEditorRowProperties m1 = new MultiEditorRowProperties();
            m1.Caption = "X";
            m1.Value = (double)0;
            MultiEditorRowProperties m2 = new MultiEditorRowProperties();
            m2.Caption = "Y";
            m2.Value = (double)0;
            MultiEditorRowProperties m3 = new MultiEditorRowProperties();
            m3.Caption = "Z";
            m3.Value = (double)0;

            mr_Upper.PropertiesCollection.AddRange(new MultiEditorRowProperties[] { m1, m2, m3 });
            mr_Upper.Enabled = false;

            

            ///<summary>Creating the Lower Limit's Multi-Editor Row</summary>
            mr_Lower = new MultiEditorRow();
            mr_Lower.Name = "LowerLimit";

            MultiEditorRowProperties m4 = new MultiEditorRowProperties();
            m4.Caption = "X";
            m4.Value = (double)0;
            MultiEditorRowProperties m5 = new MultiEditorRowProperties();
            m5.Caption = "Y";
            m5.Value = (double)0;
            MultiEditorRowProperties m6 = new MultiEditorRowProperties();
            m6.Caption = "Z";
            m6.Value = (double)0;

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
            category.Name = _name;
            category.ChildRows.AddRange(new BaseRow[] { er_Upper, er_Lower });


            return category;
        }
        #endregion

        #region ---CheckedListBoxControl Initialization Methods---

        private void InitializeListBoxes()
        {
            CheckedListBoxItem LowerFront = new CheckedListBoxItem(CoordinateOptions.LowerFront.ToString());
            CheckedListBoxItem LowerRear = new CheckedListBoxItem(CoordinateOptions.LowerRear.ToString());
            CheckedListBoxItem UpperFront = new CheckedListBoxItem(CoordinateOptions.UpperFront.ToString());
            CheckedListBoxItem UpperRear = new CheckedListBoxItem(CoordinateOptions.UpperRear.ToString());
            CheckedListBoxItem PushrodInboard = new CheckedListBoxItem(CoordinateOptions.PushrodInboard.ToString());
            CheckedListBoxItem ToeLinkInboard= new CheckedListBoxItem(CoordinateOptions.ToeLinkInboard.ToString());
            CheckedListBoxItem DamperShockMount = new CheckedListBoxItem(CoordinateOptions.DamperShockMount.ToString());
            CheckedListBoxItem DamperBellCrank = new CheckedListBoxItem(CoordinateOptions.DamperBellCrank.ToString());


            CheckedListBoxItem UBJ = new CheckedListBoxItem(CoordinateOptions.UBJ.ToString());
            CheckedListBoxItem LBJ = new CheckedListBoxItem(CoordinateOptions.LBJ.ToString());
            CheckedListBoxItem PushrodOutboard = new CheckedListBoxItem(CoordinateOptions.PushrodOutboard.ToString());
            CheckedListBoxItem ToeLinkOutBoard = new CheckedListBoxItem(CoordinateOptions.ToeLinkOutboard.ToString());
            CheckedListBoxItem WheelCenter = new CheckedListBoxItem(CoordinateOptions.WheelCenter.ToString());

            clb_InboardAdjPoints.Items.AddRange(new CheckedListBoxItem[] { LowerFront, LowerRear, UpperFront, UpperRear, PushrodInboard, ToeLinkInboard, DamperShockMount,DamperBellCrank });

            clb_OutboardAdjPoints.Items.AddRange(new CheckedListBoxItem[] { UBJ, LBJ, PushrodOutboard, ToeLinkOutBoard, WheelCenter });
        }

        #endregion

        #region ---Inboard Pick-Up Points GUI--- (Checked List Box Events and GridControl operations)---

        /// <summary>
        /// Event fired when the <see cref="clb_InboardAdjPoints"/>'s Item's Checkstate Changeds
        /// Contains calls to activate or de-activate the Adjusters present in the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_Inboard_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (clb_InboardAdjPoints.Items[e.Index].CheckState == CheckState.Checked)
            {
                Activate_InboardPointAdjusters(index);
                Add_InboardPointAdj_ToDictionary(index);
            }
            else
            {
                Deactivate_InboardAdjusters(index);
                Remove_InboardPointAdj_ToDictionary(index);
            }
        }

        /// <summary>
        /// Method to Activate the Adjusters present in the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="_index">Index of the <see cref="clb_InboardAdjPoints"/> item whose checkstate was set to Checked</param>
        private void Activate_InboardPointAdjusters(int _index)
        {
            EditorRow upper = Inboard_CategoryRows[_index].ChildRows[0] as EditorRow;
            EditorRow lower = Inboard_CategoryRows[_index].ChildRows[1] as EditorRow;

            upper.Enabled = true;

            lower.Enabled = true;

            MultiEditorRow upperXYZ = Inboard_CategoryRows[_index].ChildRows[0].ChildRows[0] as MultiEditorRow;
            MultiEditorRow lowerXYZ = Inboard_CategoryRows[_index].ChildRows[1].ChildRows[0] as MultiEditorRow;

            upperXYZ.Enabled = true;
            lowerXYZ.Enabled = true;
        }

        /// <summary>
        /// Method to add Adjuster to the Dictionary
        /// </summary>
        /// <param name="_index"></param>
        private void Add_InboardPointAdj_ToDictionary(int _index)
        {
            string coordname = clb_InboardAdjPoints.Items[_index].Value as string;

            if (!KO_CV.KO_MasterAdjs.ContainsKey(coordname))
            {
                KO_CV.KO_MasterAdjs.Add(coordname, new KO_AdjToolParams());
            }

        }

        /// <summary>
        /// Method to De-activate the Adjusters present in the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="_index">Index of the <see cref="clb_InboardAdjPoints"/> item whose checkstate was set to Unchecked</param>
        private void Deactivate_InboardAdjusters(int _index)
        {
            EditorRow upper = Inboard_CategoryRows[_index].ChildRows[0] as EditorRow;
            EditorRow lower = Inboard_CategoryRows[_index].ChildRows[1] as EditorRow;

            upper.Enabled = false;

            lower.Enabled = false;

            MultiEditorRow upperXYZ = Inboard_CategoryRows[_index].ChildRows[0].ChildRows[0] as MultiEditorRow;
            MultiEditorRow lowerXYZ = Inboard_CategoryRows[_index].ChildRows[1].ChildRows[0] as MultiEditorRow;

            upperXYZ.Enabled = false;
            lowerXYZ.Enabled = false;
        }

        /// <summary>
        /// Method to Remove the Adjuster from the Dictionary
        /// </summary>
        /// <param name="_index"></param>
        private void Remove_InboardPointAdj_ToDictionary(int _index)
        {
            string coordName = clb_InboardAdjPoints.Items[_index].Value as string;

            if (KO_CV.KO_MasterAdjs.ContainsKey(coordName))
            {
                KO_CV.KO_MasterAdjs.Remove(coordName);
            }
        }

        #endregion

        #region ---Outboard Pick-Up Points GUI--- (Checked List Box Events and GridControl operations)---

        /// <summary>
        /// Event fired when the <see cref="clb_OutboardAdjPoints"/>'s Item's Checkstate Changeds
        /// Contains calls to activate or de-activate the Adjusters present in the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_OutboardAdjPoints_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (clb_OutboardAdjPoints.Items[index].CheckState == CheckState.Checked)
            {
                Activate_Outboarddjusters(index);
                Add_OutboardAdj_ToDictionary(index);
            }
            else
            {
                Deactivate_OutboardAdjusters(index);
                Remove_OutboardAdj_ToDictionary(index);
            }
        }

        /// <summary>
        /// Method to Activate the Adjusters present in the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="_index">Index of the <see cref="clb_OutboardAdjPoints"/> item whose checkstate was set to Checked</param>
        private void Activate_Outboarddjusters(int _index)
        {
            EditorRow upper = Outboard_CategoryRows[_index].ChildRows[0] as EditorRow;
            EditorRow lower = Inboard_CategoryRows[_index].ChildRows[1] as EditorRow;

            upper.Enabled = true;

            lower.Enabled = true;

            MultiEditorRow upperXYZ = Outboard_CategoryRows[_index].ChildRows[0].ChildRows[0] as MultiEditorRow;
            MultiEditorRow lowerXYZ = Outboard_CategoryRows[_index].ChildRows[1].ChildRows[0] as MultiEditorRow;

            upperXYZ.Enabled = true;
            lowerXYZ.Enabled = true;
        }

        /// <summary>
        /// Method to add the Adjuster to the Dictionary
        /// </summary>
        /// <param name="_index"></param>
        private void Add_OutboardAdj_ToDictionary(int _index)
        {
            string coordName = clb_OutboardAdjPoints.Items[_index].Value as string;

            if (!KO_CV.KO_MasterAdjs.ContainsKey(coordName))
            {
                KO_CV.KO_MasterAdjs.Add(coordName, new KO_AdjToolParams());
            }
        }

        /// <summary>
        /// Method to De-activate the Adjusters present in the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="_index">Index of the <see cref="clb_OutboardAdjPoints"/> item whose checkstate was set to Unchecked</param>
        private void Deactivate_OutboardAdjusters(int _index)
        {
            EditorRow upper = Outboard_CategoryRows[_index].ChildRows[0] as EditorRow;
            EditorRow lower = Outboard_CategoryRows[_index].ChildRows[1] as EditorRow;

            upper.Enabled = false;

            lower.Enabled = false;

            MultiEditorRow upperXYZ = Outboard_CategoryRows[_index].ChildRows[0].ChildRows[0] as MultiEditorRow;
            MultiEditorRow lowerXYZ = Outboard_CategoryRows[_index].ChildRows[1].ChildRows[0] as MultiEditorRow;

            upperXYZ.Enabled = false;
            lowerXYZ.Enabled = false;
        }

        /// <summary>
        /// Method to Remove the Adjuster from the Dictionary
        /// </summary>
        /// <param name="_index"></param>
        private void Remove_OutboardAdj_ToDictionary(int _index)
        {
            string coordName = clb_OutboardAdjPoints.Items[_index].Value as string;

            if (KO_CV.KO_MasterAdjs.ContainsKey(coordName))
            {
                KO_CV.KO_MasterAdjs.Remove(coordName);
            }
        }



        #endregion

        private void VGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            int mainIndex = 0;

            if (vGridControl1.FocusedRow is MultiEditorRow)
            {
                MultiEditorRow row = vGridControl1.FocusedRow as MultiEditorRow;

                EditorRow eRow = row.ParentRow as EditorRow;

                CategoryRow cRow = eRow.ParentRow as CategoryRow;

                string coordName = "";

                if (Inboard_CategoryRows.Contains(cRow))
                {
                    mainIndex = Inboard_CategoryRows.IndexOf(cRow);

                    coordName = clb_InboardAdjPoints.Items[mainIndex].Value as string;

                }
                else if (Outboard_CategoryRows.Contains(cRow))
                {
                    mainIndex = Outboard_CategoryRows.IndexOf(cRow);

                    coordName = clb_OutboardAdjPoints.Items[mainIndex].Value as string;
                }

                if (KO_CV.KO_MasterAdjs.ContainsKey(coordName))
                {
                    if (row.Name == ("UpperLimit" + mainIndex)) 
                    {
                        KO_CV.KO_MasterAdjs[coordName].UpperCoordinateLimit = new Point3D((double)row.PropertiesCollection[0].Value,
                                                                                          (double)row.PropertiesCollection[1].Value,
                                                                                          (double)row.PropertiesCollection[2].Value);
                    }
                    else if (row.Name == ("LowerLimit" + mainIndex))
                    {
                        KO_CV.KO_MasterAdjs[coordName].LowerCoordinateLimit = new Point3D((double)row.PropertiesCollection[0].Value,
                                                                                          (double)row.PropertiesCollection[1].Value,
                                                                                          (double)row.PropertiesCollection[2].Value);
                    }
                }


            }
        }

    }
}

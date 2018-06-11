namespace Coding_Attempt_with_GUI
{
    partial class AdjustableCoordinates
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clb_InboardAdjPoints = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cl_OutboardAdjPoints = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControlInboardPoints = new DevExpress.XtraEditors.GroupControl();
            this.groupControlOutboardPoints = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.vGridControl1 = new DevExpress.XtraVerticalGrid.VGridControl();
            this.category = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.row1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.row3 = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            this.row = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.row2 = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.clb_InboardAdjPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cl_OutboardAdjPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlInboardPoints)).BeginInit();
            this.groupControlInboardPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlOutboardPoints)).BeginInit();
            this.groupControlOutboardPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // clb_InboardAdjPoints
            // 
            this.clb_InboardAdjPoints.Cursor = System.Windows.Forms.Cursors.Default;
            this.clb_InboardAdjPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_InboardAdjPoints.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Lower Front"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Lower Rear"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Upper Front"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Upper Rear"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Pushrod"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Toe Link")});
            this.clb_InboardAdjPoints.Location = new System.Drawing.Point(2, 15);
            this.clb_InboardAdjPoints.Name = "clb_InboardAdjPoints";
            this.clb_InboardAdjPoints.Size = new System.Drawing.Size(140, 177);
            this.clb_InboardAdjPoints.TabIndex = 0;
            this.clb_InboardAdjPoints.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.clb_Inboard_ItemCheck);
            // 
            // cl_OutboardAdjPoints
            // 
            this.cl_OutboardAdjPoints.Cursor = System.Windows.Forms.Cursors.Default;
            this.cl_OutboardAdjPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cl_OutboardAdjPoints.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Upper Ball Joint"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Lower Ball Joint"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Pushrod"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Toe Link"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Wheel Center")});
            this.cl_OutboardAdjPoints.Location = new System.Drawing.Point(2, 15);
            this.cl_OutboardAdjPoints.Name = "cl_OutboardAdjPoints";
            this.cl_OutboardAdjPoints.Size = new System.Drawing.Size(140, 139);
            this.cl_OutboardAdjPoints.TabIndex = 1;
            // 
            // groupControlInboardPoints
            // 
            this.groupControlInboardPoints.Controls.Add(this.clb_InboardAdjPoints);
            this.groupControlInboardPoints.Location = new System.Drawing.Point(12, 30);
            this.groupControlInboardPoints.Name = "groupControlInboardPoints";
            this.groupControlInboardPoints.Size = new System.Drawing.Size(144, 194);
            this.groupControlInboardPoints.TabIndex = 2;
            this.groupControlInboardPoints.Text = "Inboard Adj Points";
            // 
            // groupControlOutboardPoints
            // 
            this.groupControlOutboardPoints.Controls.Add(this.cl_OutboardAdjPoints);
            this.groupControlOutboardPoints.Location = new System.Drawing.Point(12, 261);
            this.groupControlOutboardPoints.Name = "groupControlOutboardPoints";
            this.groupControlOutboardPoints.Size = new System.Drawing.Size(144, 156);
            this.groupControlOutboardPoints.TabIndex = 3;
            this.groupControlOutboardPoints.Text = "Outboard Adj Points";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.Control.BorderColor = System.Drawing.Color.Red;
            this.layoutControl1.Appearance.Control.Options.UseBorderColor = true;
            this.layoutControl1.Controls.Add(this.vGridControl1);
            this.layoutControl1.Controls.Add(this.groupControlOutboardPoints);
            this.layoutControl1.Controls.Add(this.groupControlInboardPoints);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(491, 446);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // vGridControl1
            // 
            this.vGridControl1.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.vGridControl1.Location = new System.Drawing.Point(160, 30);
            this.vGridControl1.Name = "vGridControl1";
            this.vGridControl1.RecordWidth = 131;
            this.vGridControl1.RowHeaderWidth = 151;
            this.vGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.category});
            this.vGridControl1.Size = new System.Drawing.Size(319, 404);
            this.vGridControl1.TabIndex = 4;
            // 
            // category
            // 
            this.category.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.row1,
            this.row});
            this.category.Name = "category";
            // 
            // row1
            // 
            this.row1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.row3});
            this.row1.Name = "row1";
            // 
            // row3
            // 
            this.row3.Name = "row3";
            // 
            // row
            // 
            this.row.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.row2});
            this.row.Name = "row";
            // 
            // row2
            // 
            this.row2.Name = "row2";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.layoutControlItem3,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(491, 446);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControlInboardPoints;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(148, 198);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControlOutboardPoints;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 249);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(148, 160);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 216);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(148, 33);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 409);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(148, 17);
            this.emptySpaceItem3.Text = "emptySpaceItem1";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.vGridControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(148, 18);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(323, 408);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(471, 18);
            this.simpleLabelItem1.Text = "Adjustable Coordinates";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(144, 14);
            // 
            // AdjustableCoordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "AdjustableCoordinates";
            this.Size = new System.Drawing.Size(491, 446);
            ((System.ComponentModel.ISupportInitialize)(this.clb_InboardAdjPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cl_OutboardAdjPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlInboardPoints)).EndInit();
            this.groupControlInboardPoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlOutboardPoints)).EndInit();
            this.groupControlOutboardPoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl clb_InboardAdjPoints;
        private DevExpress.XtraEditors.CheckedListBoxControl cl_OutboardAdjPoints;
        private DevExpress.XtraEditors.GroupControl groupControlInboardPoints;
        private DevExpress.XtraEditors.GroupControl groupControlOutboardPoints;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow category;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow row1;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRow row3;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow row;
        private DevExpress.XtraVerticalGrid.Rows.MultiEditorRow row2;
    }
}

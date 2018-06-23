namespace Coding_Attempt_with_GUI
{
    partial class KO_WishboneInboardPoints
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.listBoxControlSuspensionCoordinate = new DevExpress.XtraEditors.ListBoxControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.tbZ = new System.Windows.Forms.TextBox();
            this.tbY = new System.Windows.Forms.TextBox();
            this.tbX = new System.Windows.Forms.TextBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlSuspensionCoordinate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.listBoxControlSuspensionCoordinate);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.tbZ);
            this.layoutControl1.Controls.Add(this.tbY);
            this.layoutControl1.Controls.Add(this.tbX);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(276, 245);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // listBoxControlSuspensionCoordinate
            // 
            this.listBoxControlSuspensionCoordinate.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxControlSuspensionCoordinate.Location = new System.Drawing.Point(12, 30);
            this.listBoxControlSuspensionCoordinate.Name = "listBoxControlSuspensionCoordinate";
            this.listBoxControlSuspensionCoordinate.Size = new System.Drawing.Size(108, 170);
            this.listBoxControlSuspensionCoordinate.StyleController = this.layoutControl1;
            this.listBoxControlSuspensionCoordinate.TabIndex = 9;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(124, 30);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "X In : Y In : Z Out"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "X In : Y Out : Z In"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "X Out : Y In : Z In")});
            this.radioGroup1.Size = new System.Drawing.Size(140, 113);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 8;
            // 
            // tbZ
            // 
            this.tbZ.Location = new System.Drawing.Point(215, 213);
            this.tbZ.Name = "tbZ";
            this.tbZ.Size = new System.Drawing.Size(49, 20);
            this.tbZ.TabIndex = 6;
            this.tbZ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbZ_KeyDown);
            this.tbZ.Leave += new System.EventHandler(this.tbZ_Leave);
            // 
            // tbY
            // 
            this.tbY.Location = new System.Drawing.Point(215, 189);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(49, 20);
            this.tbY.TabIndex = 5;
            this.tbY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbY_KeyDown);
            this.tbY.Leave += new System.EventHandler(this.tbY_Leave);
            // 
            // tbX
            // 
            this.tbX.Location = new System.Drawing.Point(215, 165);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(49, 20);
            this.tbX.TabIndex = 4;
            this.tbX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbX_KeyDown);
            this.tbX.Leave += new System.EventHandler(this.tbX_Leave);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.simpleLabelItem3,
            this.simpleLabelItem2,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(276, 245);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.Location = new System.Drawing.Point(112, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(144, 18);
            this.simpleLabelItem1.Text = "Input Format";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(88, 14);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(112, 18);
            this.simpleLabelItem3.Text = "Pick-Up Points";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(88, 14);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem2.Location = new System.Drawing.Point(112, 135);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(144, 18);
            this.simpleLabelItem2.Text = "Coordinates";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(88, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tbX;
            this.layoutControlItem1.Location = new System.Drawing.Point(112, 153);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(144, 24);
            this.layoutControlItem1.Text = "     X";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(88, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tbY;
            this.layoutControlItem2.Location = new System.Drawing.Point(112, 177);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(144, 24);
            this.layoutControlItem2.Text = "     Y";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(88, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tbZ;
            this.layoutControlItem3.Location = new System.Drawing.Point(112, 201);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(144, 24);
            this.layoutControlItem3.Text = "     Z";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(88, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.radioGroup1;
            this.layoutControlItem5.Location = new System.Drawing.Point(112, 18);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(144, 117);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.listBoxControlSuspensionCoordinate;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(112, 174);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 192);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(112, 33);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // KO_WishboneInboardPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "KO_WishboneInboardPoints";
            this.Size = new System.Drawing.Size(276, 245);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlSuspensionCoordinate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.TextBox tbZ;
        private System.Windows.Forms.TextBox tbY;
        private System.Windows.Forms.TextBox tbX;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlSuspensionCoordinate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}

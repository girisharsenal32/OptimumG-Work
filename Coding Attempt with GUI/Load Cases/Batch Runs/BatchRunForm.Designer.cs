namespace Coding_Attempt_with_GUI
{
    partial class BatchRunForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchRunForm));
            this.checkedListBoxLoadCases = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.simpleButtonRun = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlLoadCases = new DevExpress.XtraEditors.GroupControl();
            this.simpleButtonSelectAllLoadCases = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlVehicle = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxVehicleBatchRun = new System.Windows.Forms.ComboBox();
            this.groupControlMotion = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxMotionBatchRun = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelOPChanel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxLoadCases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlLoadCases)).BeginInit();
            this.groupControlLoadCases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlVehicle)).BeginInit();
            this.groupControlVehicle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlMotion)).BeginInit();
            this.groupControlMotion.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBoxLoadCases
            // 
            this.checkedListBoxLoadCases.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.checkedListBoxLoadCases.Appearance.Options.UseFont = true;
            this.checkedListBoxLoadCases.CheckOnClick = true;
            this.checkedListBoxLoadCases.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxLoadCases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxLoadCases.Location = new System.Drawing.Point(2, 20);
            this.checkedListBoxLoadCases.Name = "checkedListBoxLoadCases";
            this.checkedListBoxLoadCases.Size = new System.Drawing.Size(356, 209);
            this.checkedListBoxLoadCases.TabIndex = 0;
            this.checkedListBoxLoadCases.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxLoadCases_ItemCheck);
            // 
            // simpleButtonRun
            // 
            this.simpleButtonRun.Location = new System.Drawing.Point(52, 346);
            this.simpleButtonRun.Name = "simpleButtonRun";
            this.simpleButtonRun.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonRun.TabIndex = 1;
            this.simpleButtonRun.Text = "Run";
            this.simpleButtonRun.Click += new System.EventHandler(this.simpleButtonRun_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(133, 346);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 1;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // groupControlLoadCases
            // 
            this.groupControlLoadCases.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlLoadCases.Controls.Add(this.simpleButtonSelectAllLoadCases);
            this.groupControlLoadCases.Controls.Add(this.checkedListBoxLoadCases);
            this.groupControlLoadCases.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlLoadCases.Location = new System.Drawing.Point(0, 0);
            this.groupControlLoadCases.Name = "groupControlLoadCases";
            this.groupControlLoadCases.Size = new System.Drawing.Size(360, 231);
            this.groupControlLoadCases.TabIndex = 2;
            this.groupControlLoadCases.Text = "Load Cases Created";
            // 
            // simpleButtonSelectAllLoadCases
            // 
            this.simpleButtonSelectAllLoadCases.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButtonSelectAllLoadCases.Location = new System.Drawing.Point(2, 206);
            this.simpleButtonSelectAllLoadCases.Name = "simpleButtonSelectAllLoadCases";
            this.simpleButtonSelectAllLoadCases.Size = new System.Drawing.Size(356, 23);
            this.simpleButtonSelectAllLoadCases.TabIndex = 2;
            this.simpleButtonSelectAllLoadCases.Text = "Select All Load Cases";
            this.simpleButtonSelectAllLoadCases.Click += new System.EventHandler(this.simpleButtonSelectAllLoadCases_Click);
            // 
            // groupControlVehicle
            // 
            this.groupControlVehicle.Controls.Add(this.comboBoxVehicleBatchRun);
            this.groupControlVehicle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlVehicle.Location = new System.Drawing.Point(0, 231);
            this.groupControlVehicle.Name = "groupControlVehicle";
            this.groupControlVehicle.Size = new System.Drawing.Size(360, 53);
            this.groupControlVehicle.TabIndex = 3;
            this.groupControlVehicle.Text = "Vehicle ";
            // 
            // comboBoxVehicleBatchRun
            // 
            this.comboBoxVehicleBatchRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVehicleBatchRun.FormattingEnabled = true;
            this.comboBoxVehicleBatchRun.Location = new System.Drawing.Point(2, 20);
            this.comboBoxVehicleBatchRun.Name = "comboBoxVehicleBatchRun";
            this.comboBoxVehicleBatchRun.Size = new System.Drawing.Size(356, 21);
            this.comboBoxVehicleBatchRun.TabIndex = 0;
            this.comboBoxVehicleBatchRun.SelectedIndexChanged += new System.EventHandler(this.comboBoxVehicleBatchRun_SelectedIndexChanged);
            // 
            // groupControlMotion
            // 
            this.groupControlMotion.Controls.Add(this.comboBoxMotionBatchRun);
            this.groupControlMotion.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlMotion.Location = new System.Drawing.Point(0, 284);
            this.groupControlMotion.Name = "groupControlMotion";
            this.groupControlMotion.Size = new System.Drawing.Size(360, 48);
            this.groupControlMotion.TabIndex = 4;
            this.groupControlMotion.Text = "Motion";
            // 
            // comboBoxMotionBatchRun
            // 
            this.comboBoxMotionBatchRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMotionBatchRun.FormattingEnabled = true;
            this.comboBoxMotionBatchRun.Location = new System.Drawing.Point(2, 20);
            this.comboBoxMotionBatchRun.Name = "comboBoxMotionBatchRun";
            this.comboBoxMotionBatchRun.Size = new System.Drawing.Size(356, 21);
            this.comboBoxMotionBatchRun.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelOPChanel,
            this.toolStripLabel2,
            this.toolStripProgressBar1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 378);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(360, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelOPChanel
            // 
            this.toolStripLabelOPChanel.Name = "toolStripLabelOPChanel";
            this.toolStripLabelOPChanel.Size = new System.Drawing.Size(108, 22);
            this.toolStripLabelOPChanel.Text = "Output Channels :-";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(108, 22);
            this.toolStripLabel2.Text = "Multiple Channels";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            // 
            // BatchRunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(360, 403);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupControlMotion);
            this.Controls.Add(this.groupControlVehicle);
            this.Controls.Add(this.groupControlLoadCases);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonRun);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BatchRunForm";
            this.Text = "BatchRun";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BatchRun_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxLoadCases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlLoadCases)).EndInit();
            this.groupControlLoadCases.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlVehicle)).EndInit();
            this.groupControlVehicle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlMotion)).EndInit();
            this.groupControlMotion.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButtonRun;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.GroupControl groupControlLoadCases;
        private DevExpress.XtraEditors.GroupControl groupControlVehicle;
        public System.Windows.Forms.ComboBox comboBoxVehicleBatchRun;
        public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxLoadCases;
        private DevExpress.XtraEditors.GroupControl groupControlMotion;
        public System.Windows.Forms.ComboBox comboBoxMotionBatchRun;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelOPChanel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSelectAllLoadCases;
    }
}
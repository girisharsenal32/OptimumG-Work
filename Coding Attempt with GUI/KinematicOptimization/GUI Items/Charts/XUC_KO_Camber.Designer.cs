namespace Coding_Attempt_with_GUI
{
    partial class XUC_KO_Camber
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
            this.xuC_KO_CamberSteering = new Coding_Attempt_with_GUI.XUC_KO_GenericChart();
            this.xuC_KO_CamberHeave = new Coding_Attempt_with_GUI.XUC_KO_GenericChart();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xuC_KO_CamberSteering);
            this.layoutControl1.Controls.Add(this.xuC_KO_CamberHeave);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(745, 503);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xuC_KO_CamberSteering
            // 
            this.xuC_KO_CamberSteering.IsOutputChart = false;
            this.xuC_KO_CamberSteering.Location = new System.Drawing.Point(12, 255);
            this.xuC_KO_CamberSteering.Name = "xuC_KO_CamberSteering";
            this.xuC_KO_CamberSteering.seriesPointsInChart = null;
            this.xuC_KO_CamberSteering.Size = new System.Drawing.Size(721, 236);
            this.xuC_KO_CamberSteering.StepSize = 0;
            this.xuC_KO_CamberSteering.TabIndex = 5;
            this.xuC_KO_CamberSteering.X_Lower = 0D;
            this.xuC_KO_CamberSteering.X_Upper = 0D;
            this.xuC_KO_CamberSteering.Y_Lower = 0D;
            this.xuC_KO_CamberSteering.Y_Upper = 0D;
            // 
            // xuC_KO_CamberHeave
            // 
            this.xuC_KO_CamberHeave.IsOutputChart = false;
            this.xuC_KO_CamberHeave.Location = new System.Drawing.Point(12, 12);
            this.xuC_KO_CamberHeave.Name = "xuC_KO_CamberHeave";
            this.xuC_KO_CamberHeave.seriesPointsInChart = null;
            this.xuC_KO_CamberHeave.Size = new System.Drawing.Size(721, 234);
            this.xuC_KO_CamberHeave.StepSize = 0;
            this.xuC_KO_CamberHeave.TabIndex = 4;
            this.xuC_KO_CamberHeave.X_Lower = 0D;
            this.xuC_KO_CamberHeave.X_Upper = 0D;
            this.xuC_KO_CamberHeave.Y_Lower = 0D;
            this.xuC_KO_CamberHeave.Y_Upper = 0D;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.splitterItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(745, 503);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.xuC_KO_CamberSteering;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 243);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(725, 240);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xuC_KO_CamberHeave;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(725, 238);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 238);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(725, 5);
            // 
            // XUC_KO_Camber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XUC_KO_Camber";
            this.Size = new System.Drawing.Size(745, 503);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private XUC_KO_GenericChart xuC_KO_CamberSteering;
        private XUC_KO_GenericChart xuC_KO_CamberHeave;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}

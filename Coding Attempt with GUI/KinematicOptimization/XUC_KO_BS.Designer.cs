namespace Coding_Attempt_with_GUI
{
    partial class XUC_KO_BS
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
            this.bumpSteerCurve1 = new Coding_Attempt_with_GUI.BumpSteerCurve();
            this.SuspendLayout();
            // 
            // bumpSteerCurve1
            // 
            this.bumpSteerCurve1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bumpSteerCurve1.CustomBumpSteerCurve = false;
            this.bumpSteerCurve1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bumpSteerCurve1.IsOutputChart = false;
            this.bumpSteerCurve1.Location = new System.Drawing.Point(0, 0);
            this.bumpSteerCurve1.Name = "bumpSteerCurve1";
            this.bumpSteerCurve1.seriesPointsInChart = null;
            this.bumpSteerCurve1.Size = new System.Drawing.Size(768, 448);
            this.bumpSteerCurve1.StepSize = 0;
            this.bumpSteerCurve1.TabIndex = 0;
            this.bumpSteerCurve1.X_Lower = 0D;
            this.bumpSteerCurve1.X_Upper = 0D;
            this.bumpSteerCurve1.Y_Lower = 0D;
            this.bumpSteerCurve1.Y_Upper = 0D;
            // 
            // XUC_KO_BS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bumpSteerCurve1);
            this.Name = "XUC_KO_BS";
            this.Size = new System.Drawing.Size(768, 448);
            this.ResumeLayout(false);

        }

        #endregion

        private BumpSteerCurve bumpSteerCurve1;
    }
}

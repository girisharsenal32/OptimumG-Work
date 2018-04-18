namespace Coding_Attempt_with_GUI
{
    partial class LegendEditor
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
            this.groupControlStandardFem = new DevExpress.XtraEditors.GroupControl();
            this.groupControlCustomColours = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlStandardFem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCustomColours)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlStandardFem
            // 
            this.groupControlStandardFem.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlStandardFem.Location = new System.Drawing.Point(0, 0);
            this.groupControlStandardFem.Name = "groupControlStandardFem";
            this.groupControlStandardFem.Size = new System.Drawing.Size(284, 100);
            this.groupControlStandardFem.TabIndex = 0;
            this.groupControlStandardFem.Text = "Standard FEM";
            // 
            // groupControlCustomColours
            // 
            this.groupControlCustomColours.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlCustomColours.Location = new System.Drawing.Point(0, 100);
            this.groupControlCustomColours.Name = "groupControlCustomColours";
            this.groupControlCustomColours.Size = new System.Drawing.Size(284, 100);
            this.groupControlCustomColours.TabIndex = 1;
            this.groupControlCustomColours.Text = "Two Colour Gradient";
            // 
            // LegendEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 330);
            this.Controls.Add(this.groupControlCustomColours);
            this.Controls.Add(this.groupControlStandardFem);
            this.Name = "LegendEditor";
            this.Text = "LegendEditor";
            ((System.ComponentModel.ISupportInitialize)(this.groupControlStandardFem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCustomColours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlStandardFem;
        private DevExpress.XtraEditors.GroupControl groupControlCustomColours;
    }
}
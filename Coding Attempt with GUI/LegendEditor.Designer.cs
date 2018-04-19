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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlCustomColours = new DevExpress.XtraEditors.GroupControl();
            this.groupControlStandardFem = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCustomColours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlStandardFem)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(41, 165);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(41, 248);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 4;
            this.simpleButton2.Text = "simpleButton1";
            // 
            // groupControlCustomColours
            // 
            this.groupControlCustomColours.Location = new System.Drawing.Point(165, 118);
            this.groupControlCustomColours.Name = "groupControlCustomColours";
            this.groupControlCustomColours.Size = new System.Drawing.Size(284, 100);
            this.groupControlCustomColours.TabIndex = 1;
            this.groupControlCustomColours.Text = "Two Colour Gradient";
            // 
            // groupControlStandardFem
            // 
            this.groupControlStandardFem.Location = new System.Drawing.Point(165, 12);
            this.groupControlStandardFem.Name = "groupControlStandardFem";
            this.groupControlStandardFem.Size = new System.Drawing.Size(284, 100);
            this.groupControlStandardFem.TabIndex = 0;
            this.groupControlStandardFem.Text = "Standard FEM";
            // 
            // LegendEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 336);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControlCustomColours);
            this.Controls.Add(this.groupControlStandardFem);
            this.Name = "LegendEditor";
            this.Text = "LegendEditor";
            ((System.ComponentModel.ISupportInitialize)(this.groupControlCustomColours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlStandardFem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.GroupControl groupControlCustomColours;
        private DevExpress.XtraEditors.GroupControl groupControlStandardFem;
    }
}
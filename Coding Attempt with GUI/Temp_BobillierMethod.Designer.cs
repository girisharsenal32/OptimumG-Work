namespace Coding_Attempt_with_GUI
{
    partial class Temp_BobillierMethod
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
            this.cad1 = new Coding_Attempt_with_GUI.CAD();
            this.SuspendLayout();
            // 
            // cad1
            // 
            this.cad1.AutoScroll = true;
            this.cad1.AutoSize = true;
            this.cad1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cad1.GradientColor1 = System.Drawing.Color.DarkViolet;
            this.cad1.GradientColor2 = System.Drawing.Color.Azure;
            this.cad1.Location = new System.Drawing.Point(0, 0);
            this.cad1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cad1.Name = "cad1";
            this.cad1.PrimaryBlockExploded = false;
            this.cad1.Size = new System.Drawing.Size(1194, 788);
            this.cad1.TabIndex = 0;
            // 
            // Temp_BobillierMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 788);
            this.Controls.Add(this.cad1);
            this.Name = "Temp_BobillierMethod";
            this.Text = "Temp_BobillierMethod";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CAD cad1;
    }
}
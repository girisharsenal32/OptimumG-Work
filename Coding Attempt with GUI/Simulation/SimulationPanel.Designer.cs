namespace Coding_Attempt_with_GUI
{
    partial class SimulationPanel
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
            this.components = new System.ComponentModel.Container();
            this.groupControlSelectMotion = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxMotion = new System.Windows.Forms.ComboBox();
            this.groupControlSelectVehicle = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxVehicle = new System.Windows.Forms.ComboBox();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlSelectLoadCase = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxLoadCase = new System.Windows.Forms.ComboBox();
            this.groupControlSetupChange = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxSetupChange = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelectMotion)).BeginInit();
            this.groupControlSelectMotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelectVehicle)).BeginInit();
            this.groupControlSelectVehicle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelectLoadCase)).BeginInit();
            this.groupControlSelectLoadCase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSetupChange)).BeginInit();
            this.groupControlSetupChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControlSelectMotion
            // 
            this.groupControlSelectMotion.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupControlSelectMotion.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelectMotion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlSelectMotion.Controls.Add(this.comboBoxMotion);
            this.groupControlSelectMotion.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlSelectMotion.Location = new System.Drawing.Point(0, 45);
            this.groupControlSelectMotion.Margin = new System.Windows.Forms.Padding(2);
            this.groupControlSelectMotion.Name = "groupControlSelectMotion";
            this.groupControlSelectMotion.Size = new System.Drawing.Size(366, 50);
            this.groupControlSelectMotion.TabIndex = 6;
            this.groupControlSelectMotion.Text = "Select Motion";
            // 
            // comboBoxMotion
            // 
            this.comboBoxMotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMotion.FormattingEnabled = true;
            this.comboBoxMotion.Location = new System.Drawing.Point(2, 15);
            this.comboBoxMotion.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBoxMotion.Name = "comboBoxMotion";
            this.comboBoxMotion.Size = new System.Drawing.Size(362, 21);
            this.comboBoxMotion.TabIndex = 1;
            this.comboBoxMotion.SelectedIndexChanged += new System.EventHandler(this.comboBoxMotion_SelectedIndexChanged);
            // 
            // groupControlSelectVehicle
            // 
            this.groupControlSelectVehicle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.groupControlSelectVehicle.Appearance.Options.UseBackColor = true;
            this.groupControlSelectVehicle.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupControlSelectVehicle.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelectVehicle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlSelectVehicle.Controls.Add(this.comboBoxVehicle);
            this.groupControlSelectVehicle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlSelectVehicle.Location = new System.Drawing.Point(0, 0);
            this.groupControlSelectVehicle.Margin = new System.Windows.Forms.Padding(2);
            this.groupControlSelectVehicle.Name = "groupControlSelectVehicle";
            this.groupControlSelectVehicle.Size = new System.Drawing.Size(366, 45);
            this.groupControlSelectVehicle.TabIndex = 5;
            this.groupControlSelectVehicle.Text = "Select Vehicle";
            // 
            // comboBoxVehicle
            // 
            this.comboBoxVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVehicle.FormattingEnabled = true;
            this.comboBoxVehicle.Location = new System.Drawing.Point(2, 15);
            this.comboBoxVehicle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBoxVehicle.Name = "comboBoxVehicle";
            this.comboBoxVehicle.Size = new System.Drawing.Size(362, 21);
            this.comboBoxVehicle.TabIndex = 2;
            this.comboBoxVehicle.SelectedIndexChanged += new System.EventHandler(this.comboBoxVehicle_SelectedIndexChanged);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "VS2010";
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(124, 212);
            this.simpleButtonOK.Margin = new System.Windows.Forms.Padding(2);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(88, 29);
            this.simpleButtonOK.TabIndex = 2;
            this.simpleButtonOK.Text = "OK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(220, 212);
            this.simpleButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(88, 29);
            this.simpleButtonCancel.TabIndex = 2;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // groupControlSelectLoadCase
            // 
            this.groupControlSelectLoadCase.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.groupControlSelectLoadCase.Appearance.Options.UseBackColor = true;
            this.groupControlSelectLoadCase.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupControlSelectLoadCase.AppearanceCaption.Options.UseFont = true;
            this.groupControlSelectLoadCase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlSelectLoadCase.Controls.Add(this.comboBoxLoadCase);
            this.groupControlSelectLoadCase.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlSelectLoadCase.Location = new System.Drawing.Point(0, 95);
            this.groupControlSelectLoadCase.Margin = new System.Windows.Forms.Padding(2);
            this.groupControlSelectLoadCase.Name = "groupControlSelectLoadCase";
            this.groupControlSelectLoadCase.Size = new System.Drawing.Size(366, 45);
            this.groupControlSelectLoadCase.TabIndex = 5;
            this.groupControlSelectLoadCase.Text = "Select Load Case";
            // 
            // comboBoxLoadCase
            // 
            this.comboBoxLoadCase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxLoadCase.FormattingEnabled = true;
            this.comboBoxLoadCase.Location = new System.Drawing.Point(2, 15);
            this.comboBoxLoadCase.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBoxLoadCase.Name = "comboBoxLoadCase";
            this.comboBoxLoadCase.Size = new System.Drawing.Size(362, 21);
            this.comboBoxLoadCase.TabIndex = 2;
            // 
            // groupControlSetupChange
            // 
            this.groupControlSetupChange.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.groupControlSetupChange.Appearance.Options.UseBackColor = true;
            this.groupControlSetupChange.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupControlSetupChange.AppearanceCaption.Options.UseFont = true;
            this.groupControlSetupChange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlSetupChange.Controls.Add(this.comboBoxSetupChange);
            this.groupControlSetupChange.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlSetupChange.Location = new System.Drawing.Point(0, 140);
            this.groupControlSetupChange.Margin = new System.Windows.Forms.Padding(2);
            this.groupControlSetupChange.Name = "groupControlSetupChange";
            this.groupControlSetupChange.Size = new System.Drawing.Size(366, 45);
            this.groupControlSetupChange.TabIndex = 7;
            this.groupControlSetupChange.Text = "Select Setup Change";
            // 
            // comboBoxSetupChange
            // 
            this.comboBoxSetupChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSetupChange.FormattingEnabled = true;
            this.comboBoxSetupChange.Location = new System.Drawing.Point(2, 15);
            this.comboBoxSetupChange.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBoxSetupChange.Name = "comboBoxSetupChange";
            this.comboBoxSetupChange.Size = new System.Drawing.Size(362, 21);
            this.comboBoxSetupChange.TabIndex = 2;
            this.comboBoxSetupChange.SelectedIndexChanged += new System.EventHandler(this.comboBoxSetupChange_SelectedIndexChanged);
            // 
            // SimulationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 254);
            this.ControlBox = false;
            this.Controls.Add(this.groupControlSetupChange);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.groupControlSelectLoadCase);
            this.Controls.Add(this.groupControlSelectMotion);
            this.Controls.Add(this.groupControlSelectVehicle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SimulationPanel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimulationPanel";
            this.Load += new System.EventHandler(this.SimulationPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelectMotion)).EndInit();
            this.groupControlSelectMotion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelectVehicle)).EndInit();
            this.groupControlSelectVehicle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSelectLoadCase)).EndInit();
            this.groupControlSelectLoadCase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSetupChange)).EndInit();
            this.groupControlSetupChange.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox comboBoxMotion;
        private DevExpress.XtraEditors.GroupControl groupControlSelectVehicle;
        public System.Windows.Forms.ComboBox comboBoxVehicle;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        public DevExpress.XtraEditors.GroupControl groupControlSelectMotion;
        private DevExpress.XtraEditors.GroupControl groupControlSelectLoadCase;
        public System.Windows.Forms.ComboBox comboBoxLoadCase;
        private DevExpress.XtraEditors.GroupControl groupControlSetupChange;
        public System.Windows.Forms.ComboBox comboBoxSetupChange;
        public DevExpress.XtraEditors.SimpleButton simpleButtonOK;
    }
}
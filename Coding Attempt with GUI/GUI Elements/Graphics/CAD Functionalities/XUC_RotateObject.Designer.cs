namespace Coding_Attempt_with_GUI
{
    partial class XUC_RotateObject
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
            this.groupControlRotateObject = new DevExpress.XtraEditors.GroupControl();
            this.groupControlEntityName = new DevExpress.XtraEditors.GroupControl();
            this.listBoxItemsWhichCanBeRotated = new DevExpress.XtraEditors.ListBoxControl();
            this.simpleButtonRotateObject = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxAxisOfRotation = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPointOfRotationZ = new System.Windows.Forms.TextBox();
            this.textBoxPointOfRotationY = new System.Windows.Forms.TextBox();
            this.textBoxPointOfRotationX = new System.Windows.Forms.TextBox();
            this.textBoxRotateObject = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlRotateObject)).BeginInit();
            this.groupControlRotateObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlEntityName)).BeginInit();
            this.groupControlEntityName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxItemsWhichCanBeRotated)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlRotateObject
            // 
            this.groupControlRotateObject.Controls.Add(this.groupControlEntityName);
            this.groupControlRotateObject.Controls.Add(this.simpleButtonRotateObject);
            this.groupControlRotateObject.Controls.Add(this.comboBoxAxisOfRotation);
            this.groupControlRotateObject.Controls.Add(this.label8);
            this.groupControlRotateObject.Controls.Add(this.label7);
            this.groupControlRotateObject.Controls.Add(this.label6);
            this.groupControlRotateObject.Controls.Add(this.label2);
            this.groupControlRotateObject.Controls.Add(this.label4);
            this.groupControlRotateObject.Controls.Add(this.label5);
            this.groupControlRotateObject.Controls.Add(this.label1);
            this.groupControlRotateObject.Controls.Add(this.textBoxPointOfRotationZ);
            this.groupControlRotateObject.Controls.Add(this.textBoxPointOfRotationY);
            this.groupControlRotateObject.Controls.Add(this.textBoxPointOfRotationX);
            this.groupControlRotateObject.Controls.Add(this.textBoxRotateObject);
            this.groupControlRotateObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlRotateObject.Location = new System.Drawing.Point(0, 0);
            this.groupControlRotateObject.Name = "groupControlRotateObject";
            this.groupControlRotateObject.Size = new System.Drawing.Size(256, 427);
            this.groupControlRotateObject.TabIndex = 0;
            this.groupControlRotateObject.Text = "Rotate";
            // 
            // groupControlEntityName
            // 
            this.groupControlEntityName.Controls.Add(this.listBoxItemsWhichCanBeRotated);
            this.groupControlEntityName.Location = new System.Drawing.Point(12, 23);
            this.groupControlEntityName.Name = "groupControlEntityName";
            this.groupControlEntityName.Size = new System.Drawing.Size(239, 224);
            this.groupControlEntityName.TabIndex = 31;
            this.groupControlEntityName.Text = "Entity Name";
            // 
            // listBoxItemsWhichCanBeRotated
            // 
            this.listBoxItemsWhichCanBeRotated.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxItemsWhichCanBeRotated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxItemsWhichCanBeRotated.Location = new System.Drawing.Point(2, 20);
            this.listBoxItemsWhichCanBeRotated.Name = "listBoxItemsWhichCanBeRotated";
            this.listBoxItemsWhichCanBeRotated.Size = new System.Drawing.Size(235, 202);
            this.listBoxItemsWhichCanBeRotated.TabIndex = 0;
            this.listBoxItemsWhichCanBeRotated.SelectedIndexChanged += new System.EventHandler(this.listBoxItemsWhichCanBeRotated_SelectedIndexChanged);
            // 
            // simpleButtonRotateObject
            // 
            this.simpleButtonRotateObject.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.simpleButtonRotateObject.Location = new System.Drawing.Point(95, 387);
            this.simpleButtonRotateObject.Name = "simpleButtonRotateObject";
            this.simpleButtonRotateObject.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonRotateObject.TabIndex = 4;
            this.simpleButtonRotateObject.Text = "Rotate Object";
            this.simpleButtonRotateObject.Click += new System.EventHandler(this.simpleButtonRotateObject_Click);
            // 
            // comboBoxAxisOfRotation
            // 
            this.comboBoxAxisOfRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAxisOfRotation.FormattingEnabled = true;
            this.comboBoxAxisOfRotation.Items.AddRange(new object[] {
            "X Axis",
            "Y Axis",
            "Z Axis"});
            this.comboBoxAxisOfRotation.Location = new System.Drawing.Point(99, 279);
            this.comboBoxAxisOfRotation.Name = "comboBoxAxisOfRotation";
            this.comboBoxAxisOfRotation.Size = new System.Drawing.Size(150, 21);
            this.comboBoxAxisOfRotation.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(151, 364);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "z";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(151, 337);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "deg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Axis of Rotation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Point of Rotation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Rotation Angle";
            // 
            // textBoxPointOfRotationZ
            // 
            this.textBoxPointOfRotationZ.Location = new System.Drawing.Point(99, 306);
            this.textBoxPointOfRotationZ.Name = "textBoxPointOfRotationZ";
            this.textBoxPointOfRotationZ.Size = new System.Drawing.Size(51, 21);
            this.textBoxPointOfRotationZ.TabIndex = 3;
            this.textBoxPointOfRotationZ.Text = "-770";
            // 
            // textBoxPointOfRotationY
            // 
            this.textBoxPointOfRotationY.Location = new System.Drawing.Point(99, 360);
            this.textBoxPointOfRotationY.Name = "textBoxPointOfRotationY";
            this.textBoxPointOfRotationY.Size = new System.Drawing.Size(51, 21);
            this.textBoxPointOfRotationY.TabIndex = 2;
            this.textBoxPointOfRotationY.Text = "293";
            // 
            // textBoxPointOfRotationX
            // 
            this.textBoxPointOfRotationX.Location = new System.Drawing.Point(99, 333);
            this.textBoxPointOfRotationX.Name = "textBoxPointOfRotationX";
            this.textBoxPointOfRotationX.Size = new System.Drawing.Size(51, 21);
            this.textBoxPointOfRotationX.TabIndex = 1;
            this.textBoxPointOfRotationX.Text = "0";
            // 
            // textBoxRotateObject
            // 
            this.textBoxRotateObject.Location = new System.Drawing.Point(99, 252);
            this.textBoxRotateObject.Name = "textBoxRotateObject";
            this.textBoxRotateObject.Size = new System.Drawing.Size(51, 21);
            this.textBoxRotateObject.TabIndex = 0;
            this.textBoxRotateObject.Text = "0";
            // 
            // XUC_RotateObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControlRotateObject);
            this.Name = "XUC_RotateObject";
            this.Size = new System.Drawing.Size(256, 427);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlRotateObject)).EndInit();
            this.groupControlRotateObject.ResumeLayout(false);
            this.groupControlRotateObject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlEntityName)).EndInit();
            this.groupControlEntityName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxItemsWhichCanBeRotated)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlRotateObject;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRotateObject;
        public System.Windows.Forms.ComboBox comboBoxAxisOfRotation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxPointOfRotationZ;
        public System.Windows.Forms.TextBox textBoxPointOfRotationY;
        public System.Windows.Forms.TextBox textBoxPointOfRotationX;
        public System.Windows.Forms.TextBox textBoxRotateObject;
        private DevExpress.XtraEditors.GroupControl groupControlEntityName;
        public DevExpress.XtraEditors.ListBoxControl listBoxItemsWhichCanBeRotated;
    }
}

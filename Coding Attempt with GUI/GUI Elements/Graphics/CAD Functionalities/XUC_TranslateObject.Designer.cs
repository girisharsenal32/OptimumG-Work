namespace Coding_Attempt_with_GUI
{
    partial class XUC_TranslateObject
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
            this.simpleButtonTranslateObject = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTranslateZ = new System.Windows.Forms.TextBox();
            this.textBoxTranslateY = new System.Windows.Forms.TextBox();
            this.textBoxTranslateX = new System.Windows.Forms.TextBox();
            this.groupControlTranslateObject = new DevExpress.XtraEditors.GroupControl();
            this.groupControlEntityName = new DevExpress.XtraEditors.GroupControl();
            this.listBoxItemsWhichCanBeTranslated = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTranslateObject)).BeginInit();
            this.groupControlTranslateObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlEntityName)).BeginInit();
            this.groupControlEntityName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxItemsWhichCanBeTranslated)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonTranslateObject
            // 
            this.simpleButtonTranslateObject.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.simpleButtonTranslateObject.Location = new System.Drawing.Point(47, 295);
            this.simpleButtonTranslateObject.Name = "simpleButtonTranslateObject";
            this.simpleButtonTranslateObject.Size = new System.Drawing.Size(88, 23);
            this.simpleButtonTranslateObject.TabIndex = 34;
            this.simpleButtonTranslateObject.Text = "Translate Object";
            this.simpleButtonTranslateObject.Click += new System.EventHandler(this.simpleButtonTranslateObject_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "mm";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "mm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Translation Z";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Translation Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Translation X";
            // 
            // textBoxTranslateZ
            // 
            this.textBoxTranslateZ.Location = new System.Drawing.Point(86, 268);
            this.textBoxTranslateZ.Name = "textBoxTranslateZ";
            this.textBoxTranslateZ.Size = new System.Drawing.Size(104, 21);
            this.textBoxTranslateZ.TabIndex = 24;
            this.textBoxTranslateZ.Text = "0";
            // 
            // textBoxTranslateY
            // 
            this.textBoxTranslateY.Location = new System.Drawing.Point(86, 241);
            this.textBoxTranslateY.Name = "textBoxTranslateY";
            this.textBoxTranslateY.Size = new System.Drawing.Size(104, 21);
            this.textBoxTranslateY.TabIndex = 25;
            this.textBoxTranslateY.Text = "0";
            // 
            // textBoxTranslateX
            // 
            this.textBoxTranslateX.Location = new System.Drawing.Point(86, 214);
            this.textBoxTranslateX.Name = "textBoxTranslateX";
            this.textBoxTranslateX.Size = new System.Drawing.Size(104, 21);
            this.textBoxTranslateX.TabIndex = 26;
            this.textBoxTranslateX.Text = "0";
            // 
            // groupControlTranslateObject
            // 
            this.groupControlTranslateObject.Controls.Add(this.groupControlEntityName);
            this.groupControlTranslateObject.Controls.Add(this.textBoxTranslateX);
            this.groupControlTranslateObject.Controls.Add(this.simpleButtonTranslateObject);
            this.groupControlTranslateObject.Controls.Add(this.textBoxTranslateY);
            this.groupControlTranslateObject.Controls.Add(this.label8);
            this.groupControlTranslateObject.Controls.Add(this.textBoxTranslateZ);
            this.groupControlTranslateObject.Controls.Add(this.label6);
            this.groupControlTranslateObject.Controls.Add(this.label1);
            this.groupControlTranslateObject.Controls.Add(this.label2);
            this.groupControlTranslateObject.Controls.Add(this.label5);
            this.groupControlTranslateObject.Controls.Add(this.label7);
            this.groupControlTranslateObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlTranslateObject.Location = new System.Drawing.Point(0, 0);
            this.groupControlTranslateObject.Name = "groupControlTranslateObject";
            this.groupControlTranslateObject.Size = new System.Drawing.Size(243, 332);
            this.groupControlTranslateObject.TabIndex = 38;
            this.groupControlTranslateObject.Text = "Translate";
            // 
            // groupControlEntityName
            // 
            this.groupControlEntityName.Controls.Add(this.listBoxItemsWhichCanBeTranslated);
            this.groupControlEntityName.Location = new System.Drawing.Point(5, 23);
            this.groupControlEntityName.Name = "groupControlEntityName";
            this.groupControlEntityName.Size = new System.Drawing.Size(217, 185);
            this.groupControlEntityName.TabIndex = 37;
            this.groupControlEntityName.Text = "Entity Name";
            // 
            // listBoxItemsWhichCanBeTranslated
            // 
            this.listBoxItemsWhichCanBeTranslated.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxItemsWhichCanBeTranslated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxItemsWhichCanBeTranslated.Location = new System.Drawing.Point(2, 20);
            this.listBoxItemsWhichCanBeTranslated.Name = "listBoxItemsWhichCanBeTranslated";
            this.listBoxItemsWhichCanBeTranslated.Size = new System.Drawing.Size(213, 163);
            this.listBoxItemsWhichCanBeTranslated.TabIndex = 0;
            // 
            // XUC_TranslateObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControlTranslateObject);
            this.Name = "XUC_TranslateObject";
            this.Size = new System.Drawing.Size(243, 332);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlTranslateObject)).EndInit();
            this.groupControlTranslateObject.ResumeLayout(false);
            this.groupControlTranslateObject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlEntityName)).EndInit();
            this.groupControlEntityName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxItemsWhichCanBeTranslated)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButtonTranslateObject;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxTranslateZ;
        public System.Windows.Forms.TextBox textBoxTranslateY;
        public System.Windows.Forms.TextBox textBoxTranslateX;
        private DevExpress.XtraEditors.GroupControl groupControlTranslateObject;
        private DevExpress.XtraEditors.GroupControl groupControlEntityName;
        public DevExpress.XtraEditors.ListBoxControl listBoxItemsWhichCanBeTranslated;
    }
}

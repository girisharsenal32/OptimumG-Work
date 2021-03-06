﻿namespace Coding_Attempt_with_GUI
{
    partial class XUC_SetupChangeResults
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
            this.vGridControl1 = new DevExpress.XtraVerticalGrid.VGridControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.bumpSteerCurve1 = new Coding_Attempt_with_GUI.BumpSteerCurve();
            this.categoryKPICaster = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowKPIAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLinkKPIName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLinkKPIDelta = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowKPIConvergance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCasterAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLinkCasterName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLinkCasterDelta = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCasterConvergance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTopFrontAdj = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTopRearAdj = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBottomFrontAdj = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBottomRearAdj = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryToe = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowToeAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowToeConvergance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryCamber = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowCamberAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowShimThickness = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTopCamberMount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowShimsTopCamberMount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBottomCamberMount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowShimsBottomCamberMount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCamberConvergance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRideHeight = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowRideHeight = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLinkRHName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLinkRHDelta = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRHConvergance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryBumpSteer = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowToeLinkInboard_x = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowToeLinkInboard_y = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowToeLinkInboard_z = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBSConvergence = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowToeLink = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vGridControl1
            // 
            this.vGridControl1.Appearance.DisabledRecordValue.Font = new System.Drawing.Font("Tahoma", 9F);
            this.vGridControl1.Appearance.DisabledRecordValue.ForeColor = System.Drawing.Color.Black;
            this.vGridControl1.Appearance.DisabledRecordValue.Options.UseFont = true;
            this.vGridControl1.Appearance.DisabledRecordValue.Options.UseForeColor = true;
            this.vGridControl1.Appearance.DisabledRow.Font = new System.Drawing.Font("Tahoma", 9F);
            this.vGridControl1.Appearance.DisabledRow.ForeColor = System.Drawing.Color.Black;
            this.vGridControl1.Appearance.DisabledRow.Options.UseFont = true;
            this.vGridControl1.Appearance.DisabledRow.Options.UseForeColor = true;
            this.vGridControl1.Appearance.RowHeaderPanel.BackColor2 = System.Drawing.Color.White;
            this.vGridControl1.Appearance.RowHeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vGridControl1.Appearance.RowHeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.vGridControl1.Appearance.RowHeaderPanel.Options.UseFont = true;
            this.vGridControl1.Appearance.RowHeaderPanel.Options.UseForeColor = true;
            this.vGridControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.vGridControl1.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.vGridControl1.CustomizationFormBounds = new System.Drawing.Rectangle(1420, 550, 210, 254);
            this.vGridControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.vGridControl1.Location = new System.Drawing.Point(2, 20);
            this.vGridControl1.Name = "vGridControl1";
            this.vGridControl1.RecordWidth = 115;
            this.vGridControl1.RowHeaderWidth = 173;
            this.vGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.categoryKPICaster,
            this.categoryToe,
            this.categoryCamber,
            this.categoryRideHeight,
            this.categoryBumpSteer});
            this.vGridControl1.ScrollVisibility = DevExpress.XtraVerticalGrid.ScrollVisibility.Vertical;
            this.vGridControl1.Size = new System.Drawing.Size(296, 485);
            this.vGridControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.bumpSteerCurve1);
            this.groupControl1.Controls.Add(this.vGridControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1107, 507);
            this.groupControl1.TabIndex = 1;
            // 
            // bumpSteerCurve1
            // 
            this.bumpSteerCurve1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.bumpSteerCurve1.CustomBumpSteerCurve = false;
            this.bumpSteerCurve1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bumpSteerCurve1.Enabled = false;
            this.bumpSteerCurve1.IsOutputChart = false;
            this.bumpSteerCurve1.Location = new System.Drawing.Point(298, 20);
            this.bumpSteerCurve1.Name = "bumpSteerCurve1";
            this.bumpSteerCurve1.seriesPointsInChart = null;
            this.bumpSteerCurve1.Size = new System.Drawing.Size(807, 485);
            this.bumpSteerCurve1.StepSize = 0;
            this.bumpSteerCurve1.TabIndex = 1;
            this.bumpSteerCurve1.X_Lower = 0D;
            this.bumpSteerCurve1.X_Upper = 0D;
            this.bumpSteerCurve1.Y_Lower = 0D;
            this.bumpSteerCurve1.Y_Upper = 0D;
            // 
            // categoryKPICaster
            // 
            this.categoryKPICaster.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.categoryKPICaster.Appearance.Options.UseFont = true;
            this.categoryKPICaster.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowKPIAngle,
            this.rowCasterAngle,
            this.rowTopFrontAdj,
            this.rowTopRearAdj,
            this.rowBottomFrontAdj,
            this.rowBottomRearAdj});
            this.categoryKPICaster.Name = "categoryKPICaster";
            this.categoryKPICaster.Properties.Caption = "KPI & Caster";
            // 
            // rowKPIAngle
            // 
            this.rowKPIAngle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowKPIAngle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowKPIAngle.Appearance.Options.UseFont = true;
            this.rowKPIAngle.Appearance.Options.UseForeColor = true;
            this.rowKPIAngle.Appearance.Options.UseTextOptions = true;
            this.rowKPIAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowKPIAngle.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowLinkKPIName,
            this.rowLinkKPIDelta,
            this.rowKPIConvergance});
            this.rowKPIAngle.Enabled = false;
            this.rowKPIAngle.Name = "rowKPIAngle";
            this.rowKPIAngle.OptionsRow.AllowFocus = false;
            this.rowKPIAngle.Properties.AllowEdit = false;
            this.rowKPIAngle.Properties.Caption = "KPI Angle (deg)";
            this.rowKPIAngle.Properties.Format.FormatString = "n3";
            this.rowKPIAngle.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // rowLinkKPIName
            // 
            this.rowLinkKPIName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowLinkKPIName.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowLinkKPIName.Appearance.Options.UseFont = true;
            this.rowLinkKPIName.Appearance.Options.UseForeColor = true;
            this.rowLinkKPIName.Appearance.Options.UseTextOptions = true;
            this.rowLinkKPIName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowLinkKPIName.Enabled = false;
            this.rowLinkKPIName.Name = "rowLinkKPIName";
            this.rowLinkKPIName.Properties.Caption = "Adjuster Link KPI";
            this.rowLinkKPIName.Visible = false;
            // 
            // rowLinkKPIDelta
            // 
            this.rowLinkKPIDelta.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowLinkKPIDelta.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowLinkKPIDelta.Appearance.Options.UseFont = true;
            this.rowLinkKPIDelta.Appearance.Options.UseForeColor = true;
            this.rowLinkKPIDelta.Appearance.Options.UseTextOptions = true;
            this.rowLinkKPIDelta.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowLinkKPIDelta.Enabled = false;
            this.rowLinkKPIDelta.Name = "rowLinkKPIDelta";
            this.rowLinkKPIDelta.Properties.Caption = "Link Length for KPI (mm)";
            this.rowLinkKPIDelta.Properties.Format.FormatString = "n3";
            this.rowLinkKPIDelta.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rowLinkKPIDelta.Visible = false;
            // 
            // rowKPIConvergance
            // 
            this.rowKPIConvergance.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowKPIConvergance.Appearance.Options.UseFont = true;
            this.rowKPIConvergance.Enabled = false;
            this.rowKPIConvergance.Name = "rowKPIConvergance";
            this.rowKPIConvergance.Properties.Caption = "Convergance (%)";
            // 
            // rowCasterAngle
            // 
            this.rowCasterAngle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowCasterAngle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowCasterAngle.Appearance.Options.UseFont = true;
            this.rowCasterAngle.Appearance.Options.UseForeColor = true;
            this.rowCasterAngle.Appearance.Options.UseTextOptions = true;
            this.rowCasterAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowCasterAngle.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowLinkCasterName,
            this.rowLinkCasterDelta,
            this.rowCasterConvergance});
            this.rowCasterAngle.Enabled = false;
            this.rowCasterAngle.Name = "rowCasterAngle";
            this.rowCasterAngle.Properties.AllowEdit = false;
            this.rowCasterAngle.Properties.Caption = "Caster Angle (deg)";
            this.rowCasterAngle.Properties.Format.FormatString = "n3";
            this.rowCasterAngle.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // rowLinkCasterName
            // 
            this.rowLinkCasterName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowLinkCasterName.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowLinkCasterName.Appearance.Options.UseFont = true;
            this.rowLinkCasterName.Appearance.Options.UseForeColor = true;
            this.rowLinkCasterName.Appearance.Options.UseTextOptions = true;
            this.rowLinkCasterName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowLinkCasterName.Enabled = false;
            this.rowLinkCasterName.Name = "rowLinkCasterName";
            this.rowLinkCasterName.Properties.Caption = "Adjuster Link Caster";
            this.rowLinkCasterName.Visible = false;
            // 
            // rowLinkCasterDelta
            // 
            this.rowLinkCasterDelta.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowLinkCasterDelta.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowLinkCasterDelta.Appearance.Options.UseFont = true;
            this.rowLinkCasterDelta.Appearance.Options.UseForeColor = true;
            this.rowLinkCasterDelta.Appearance.Options.UseTextOptions = true;
            this.rowLinkCasterDelta.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowLinkCasterDelta.Enabled = false;
            this.rowLinkCasterDelta.Name = "rowLinkCasterDelta";
            this.rowLinkCasterDelta.Properties.Caption = "Link Length for Caster (mm)";
            this.rowLinkCasterDelta.Properties.Format.FormatString = "n3";
            this.rowLinkCasterDelta.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rowLinkCasterDelta.Visible = false;
            // 
            // rowCasterConvergance
            // 
            this.rowCasterConvergance.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowCasterConvergance.Appearance.Options.UseFont = true;
            this.rowCasterConvergance.Enabled = false;
            this.rowCasterConvergance.Name = "rowCasterConvergance";
            this.rowCasterConvergance.Properties.Caption = "Convergance (%)";
            // 
            // rowTopFrontAdj
            // 
            this.rowTopFrontAdj.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowTopFrontAdj.Appearance.Options.UseFont = true;
            this.rowTopFrontAdj.Enabled = false;
            this.rowTopFrontAdj.Name = "rowTopFrontAdj";
            this.rowTopFrontAdj.Properties.Caption = "Top Front(mm)";
            this.rowTopFrontAdj.Visible = false;
            // 
            // rowTopRearAdj
            // 
            this.rowTopRearAdj.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowTopRearAdj.Appearance.Options.UseFont = true;
            this.rowTopRearAdj.Enabled = false;
            this.rowTopRearAdj.Name = "rowTopRearAdj";
            this.rowTopRearAdj.Properties.Caption = "Top Rear (mm)";
            this.rowTopRearAdj.Visible = false;
            // 
            // rowBottomFrontAdj
            // 
            this.rowBottomFrontAdj.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowBottomFrontAdj.Appearance.Options.UseFont = true;
            this.rowBottomFrontAdj.Enabled = false;
            this.rowBottomFrontAdj.Name = "rowBottomFrontAdj";
            this.rowBottomFrontAdj.Properties.Caption = "Bottom Front (mm)";
            this.rowBottomFrontAdj.Visible = false;
            // 
            // rowBottomRearAdj
            // 
            this.rowBottomRearAdj.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowBottomRearAdj.Appearance.Options.UseFont = true;
            this.rowBottomRearAdj.Enabled = false;
            this.rowBottomRearAdj.Name = "rowBottomRearAdj";
            this.rowBottomRearAdj.Properties.Caption = "Bottom Rear (mm)";
            this.rowBottomRearAdj.Visible = false;
            // 
            // categoryToe
            // 
            this.categoryToe.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.categoryToe.Appearance.Options.UseFont = true;
            this.categoryToe.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowToeAngle,
            this.rowToeLink,
            this.rowToeConvergance});
            this.categoryToe.Name = "categoryToe";
            this.categoryToe.Properties.Caption = "Toe";
            // 
            // rowToeAngle
            // 
            this.rowToeAngle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowToeAngle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowToeAngle.Appearance.Options.UseFont = true;
            this.rowToeAngle.Appearance.Options.UseForeColor = true;
            this.rowToeAngle.Appearance.Options.UseTextOptions = true;
            this.rowToeAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowToeAngle.Enabled = false;
            this.rowToeAngle.Name = "rowToeAngle";
            this.rowToeAngle.Properties.AllowEdit = false;
            this.rowToeAngle.Properties.Caption = "Toe Angle (deg)";
            this.rowToeAngle.Properties.Format.FormatString = "n3";
            this.rowToeAngle.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // rowToeConvergance
            // 
            this.rowToeConvergance.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowToeConvergance.Appearance.Options.UseFont = true;
            this.rowToeConvergance.Enabled = false;
            this.rowToeConvergance.Name = "rowToeConvergance";
            this.rowToeConvergance.Properties.Caption = "Convergance (%)";
            // 
            // categoryCamber
            // 
            this.categoryCamber.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.categoryCamber.Appearance.Options.UseFont = true;
            this.categoryCamber.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowCamberAngle,
            this.rowShimThickness,
            this.rowTopCamberMount,
            this.rowBottomCamberMount,
            this.rowCamberConvergance});
            this.categoryCamber.Name = "categoryCamber";
            this.categoryCamber.Properties.Caption = "Camber";
            // 
            // rowCamberAngle
            // 
            this.rowCamberAngle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowCamberAngle.Appearance.Options.UseFont = true;
            this.rowCamberAngle.Enabled = false;
            this.rowCamberAngle.Name = "rowCamberAngle";
            this.rowCamberAngle.Properties.Caption = "Camber Angle (deg)";
            // 
            // rowShimThickness
            // 
            this.rowShimThickness.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowShimThickness.Appearance.Options.UseFont = true;
            this.rowShimThickness.Enabled = false;
            this.rowShimThickness.Name = "rowShimThickness";
            this.rowShimThickness.Properties.Caption = "Shim Thickness (mm)";
            // 
            // rowTopCamberMount
            // 
            this.rowTopCamberMount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowTopCamberMount.Appearance.Options.UseFont = true;
            this.rowTopCamberMount.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowShimsTopCamberMount});
            this.rowTopCamberMount.Enabled = false;
            this.rowTopCamberMount.Name = "rowTopCamberMount";
            this.rowTopCamberMount.Properties.Caption = "Top Mount (mm)";
            this.rowTopCamberMount.Visible = false;
            // 
            // rowShimsTopCamberMount
            // 
            this.rowShimsTopCamberMount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowShimsTopCamberMount.Appearance.Options.UseFont = true;
            this.rowShimsTopCamberMount.Enabled = false;
            this.rowShimsTopCamberMount.Name = "rowShimsTopCamberMount";
            this.rowShimsTopCamberMount.Properties.Caption = "Shims (#)";
            // 
            // rowBottomCamberMount
            // 
            this.rowBottomCamberMount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowBottomCamberMount.Appearance.Options.UseFont = true;
            this.rowBottomCamberMount.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowShimsBottomCamberMount});
            this.rowBottomCamberMount.Enabled = false;
            this.rowBottomCamberMount.Name = "rowBottomCamberMount";
            this.rowBottomCamberMount.Properties.Caption = "Bottom Mount (mm)";
            this.rowBottomCamberMount.Visible = false;
            // 
            // rowShimsBottomCamberMount
            // 
            this.rowShimsBottomCamberMount.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowShimsBottomCamberMount.Appearance.Options.UseFont = true;
            this.rowShimsBottomCamberMount.Enabled = false;
            this.rowShimsBottomCamberMount.Name = "rowShimsBottomCamberMount";
            this.rowShimsBottomCamberMount.Properties.Caption = "Shims (#)";
            // 
            // rowCamberConvergance
            // 
            this.rowCamberConvergance.Enabled = false;
            this.rowCamberConvergance.Name = "rowCamberConvergance";
            this.rowCamberConvergance.Properties.Caption = "Convergence (%)";
            // 
            // categoryRideHeight
            // 
            this.categoryRideHeight.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.categoryRideHeight.Appearance.Options.UseFont = true;
            this.categoryRideHeight.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowRideHeight,
            this.rowLinkRHName,
            this.rowLinkRHDelta,
            this.rowRHConvergance});
            this.categoryRideHeight.Name = "categoryRideHeight";
            this.categoryRideHeight.Properties.Caption = "Ride height";
            // 
            // rowRideHeight
            // 
            this.rowRideHeight.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowRideHeight.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowRideHeight.Appearance.Options.UseFont = true;
            this.rowRideHeight.Appearance.Options.UseForeColor = true;
            this.rowRideHeight.Appearance.Options.UseTextOptions = true;
            this.rowRideHeight.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowRideHeight.Enabled = false;
            this.rowRideHeight.Name = "rowRideHeight";
            this.rowRideHeight.Properties.AllowEdit = false;
            this.rowRideHeight.Properties.Caption = "Ride Height (mm)";
            this.rowRideHeight.Properties.Format.FormatString = "n3";
            this.rowRideHeight.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // rowLinkRHName
            // 
            this.rowLinkRHName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowLinkRHName.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowLinkRHName.Appearance.Options.UseFont = true;
            this.rowLinkRHName.Appearance.Options.UseForeColor = true;
            this.rowLinkRHName.Appearance.Options.UseTextOptions = true;
            this.rowLinkRHName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowLinkRHName.Enabled = false;
            this.rowLinkRHName.Name = "rowLinkRHName";
            this.rowLinkRHName.Properties.Caption = "Adjust Link RH";
            // 
            // rowLinkRHDelta
            // 
            this.rowLinkRHDelta.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowLinkRHDelta.Appearance.ForeColor = System.Drawing.Color.Black;
            this.rowLinkRHDelta.Appearance.Options.UseFont = true;
            this.rowLinkRHDelta.Appearance.Options.UseForeColor = true;
            this.rowLinkRHDelta.Appearance.Options.UseTextOptions = true;
            this.rowLinkRHDelta.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowLinkRHDelta.Enabled = false;
            this.rowLinkRHDelta.Name = "rowLinkRHDelta";
            this.rowLinkRHDelta.Properties.Caption = "Link Length(mm)";
            this.rowLinkRHDelta.Properties.Format.FormatString = "n3";
            this.rowLinkRHDelta.Properties.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // rowRHConvergance
            // 
            this.rowRHConvergance.Enabled = false;
            this.rowRHConvergance.Name = "rowRHConvergance";
            this.rowRHConvergance.Properties.Caption = "Convergance (%)";
            // 
            // categoryBumpSteer
            // 
            this.categoryBumpSteer.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.categoryBumpSteer.Appearance.Options.UseFont = true;
            this.categoryBumpSteer.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowToeLinkInboard_x,
            this.rowToeLinkInboard_y,
            this.rowToeLinkInboard_z,
            this.rowBSConvergence});
            this.categoryBumpSteer.Name = "categoryBumpSteer";
            this.categoryBumpSteer.Properties.Caption = "Bump Steer";
            // 
            // rowToeLinkInboard_x
            // 
            this.rowToeLinkInboard_x.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowToeLinkInboard_x.Appearance.Options.UseFont = true;
            this.rowToeLinkInboard_x.Enabled = false;
            this.rowToeLinkInboard_x.Name = "rowToeLinkInboard_x";
            this.rowToeLinkInboard_x.Properties.Caption = "Toe Inbrd X";
            this.rowToeLinkInboard_x.Visible = false;
            // 
            // rowToeLinkInboard_y
            // 
            this.rowToeLinkInboard_y.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowToeLinkInboard_y.Appearance.Options.UseFont = true;
            this.rowToeLinkInboard_y.Enabled = false;
            this.rowToeLinkInboard_y.Name = "rowToeLinkInboard_y";
            this.rowToeLinkInboard_y.Properties.Caption = "Toe Inbrd Y";
            this.rowToeLinkInboard_y.Visible = false;
            // 
            // rowToeLinkInboard_z
            // 
            this.rowToeLinkInboard_z.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowToeLinkInboard_z.Appearance.Options.UseFont = true;
            this.rowToeLinkInboard_z.Enabled = false;
            this.rowToeLinkInboard_z.Name = "rowToeLinkInboard_z";
            this.rowToeLinkInboard_z.Properties.Caption = "Toe Inbrd Z";
            this.rowToeLinkInboard_z.Visible = false;
            // 
            // rowBSConvergence
            // 
            this.rowBSConvergence.Enabled = false;
            this.rowBSConvergence.Name = "rowBSConvergence";
            this.rowBSConvergence.Properties.Caption = "Convergence (%)";
            // 
            // rowToeLink
            // 
            this.rowToeLink.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rowToeLink.Appearance.Options.UseFont = true;
            this.rowToeLink.Enabled = false;
            this.rowToeLink.Name = "rowToeLink";
            this.rowToeLink.Properties.Caption = "Toe Link (mm)";
            this.rowToeLink.Visible = false;
            // 
            // XUC_SetupChangeResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "XUC_SetupChangeResults";
            this.Size = new System.Drawing.Size(1107, 507);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraVerticalGrid.VGridControl vGridControl1;
        public DevExpress.XtraEditors.GroupControl groupControl1;
        public BumpSteerCurve bumpSteerCurve1;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryKPICaster;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowKPIAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLinkKPIName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLinkKPIDelta;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowKPIConvergance;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCasterAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLinkCasterName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLinkCasterDelta;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCasterConvergance;
        
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryToe;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowToeAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowToeConvergance;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRideHeight;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRideHeight;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLinkRHName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLinkRHDelta;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRHConvergance;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTopFrontAdj;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTopRearAdj;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBottomFrontAdj;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBottomRearAdj;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryCamber;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCamberAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowShimThickness;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTopCamberMount;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBottomCamberMount;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowShimsBottomCamberMount;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowShimsTopCamberMount;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryBumpSteer;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowToeLinkInboard_x;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowToeLinkInboard_y;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowToeLinkInboard_z;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBSConvergence;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCamberConvergance;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowToeLink;
    }
}

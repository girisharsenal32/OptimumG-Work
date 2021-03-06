﻿namespace Coding_Attempt_with_GUI
{
    partial class BumpSteerCurve
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.groupControlBSChart = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonClearCharrt = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonPlotMinBS = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxStepSize = new System.Windows.Forms.TextBox();
            this.textBoxYLowerLimit = new System.Windows.Forms.TextBox();
            this.textBoxYUpperLimit = new System.Windows.Forms.TextBox();
            this.textBoxXLowerLimit = new System.Windows.Forms.TextBox();
            this.textBoxXUpperLimit = new System.Windows.Forms.TextBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBSChart)).BeginInit();
            this.groupControlBSChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.BorderOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(73)))), ((int)(((byte)(122)))));
            this.chartControl1.BorderOptions.Thickness = 3;
            this.chartControl1.CrosshairOptions.SnapMode = DevExpress.XtraCharts.CrosshairSnapMode.NearestValue;
            this.chartControl1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chartControl1.DataBindings = null;
            constantLine1.AxisValueSerializable = "0";
            constantLine1.Name = "Constant Line 1";
            constantLine1.ShowInLegend = false;
            constantLine1.Title.Visible = false;
            xyDiagram1.AxisX.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1});
            xyDiagram1.AxisX.GridLines.MinorVisible = true;
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.Interlaced = true;
            xyDiagram1.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.NumericScaleOptions.GridSpacing = 5D;
            xyDiagram1.AxisX.Tickmarks.CrossAxis = true;
            xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9F);
            xyDiagram1.AxisX.Title.MaxLineCount = 1;
            xyDiagram1.AxisX.Title.Text = "Wheel Deflection (mm)";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "25";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "-25";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "25";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "-25";
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 0D;
            constantLine2.AxisValueSerializable = "0";
            constantLine2.Name = "Constant Line 1";
            constantLine2.ShowInLegend = false;
            constantLine2.Title.Visible = false;
            xyDiagram1.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine2});
            xyDiagram1.AxisY.GridLines.MinorVisible = true;
            xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 0.5D;
            xyDiagram1.AxisY.Tickmarks.CrossAxis = true;
            xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Tahoma", 9F);
            xyDiagram1.AxisY.Title.Text = "Toe Variation (deg)";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisualRange.Auto = false;
            xyDiagram1.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "5";
            xyDiagram1.AxisY.VisualRange.MinValueSerializable = "-5";
            xyDiagram1.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.WholeRange.Auto = false;
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "5";
            xyDiagram1.AxisY.WholeRange.MinValueSerializable = "-5";
            xyDiagram1.AxisY.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.chartControl1.Legend.MarkerSize = new System.Drawing.Size(8, 14);
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.Location = new System.Drawing.Point(2, 20);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Aspect";
            series1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            series1.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.True;
            series1.CrosshairLabelPattern = "{A}{V:#,#}";
            series1.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Name = "Custom BS Curve";
            lineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(146)))), ((int)(((byte)(60)))));
            lineSeriesView1.LineMarkerOptions.Size = 5;
            lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.View = lineSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.SeriesTemplate.LegendName = "Default Legend";
            this.chartControl1.Size = new System.Drawing.Size(550, 412);
            this.chartControl1.TabIndex = 0;
            this.chartControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseClick);
            // 
            // groupControlBSChart
            // 
            this.groupControlBSChart.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlBSChart.AppearanceCaption.Options.UseFont = true;
            this.groupControlBSChart.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControlBSChart.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControlBSChart.Controls.Add(this.chartControl1);
            this.groupControlBSChart.Location = new System.Drawing.Point(12, 22);
            this.groupControlBSChart.Name = "groupControlBSChart";
            this.groupControlBSChart.Size = new System.Drawing.Size(554, 434);
            this.groupControlBSChart.TabIndex = 1;
            this.groupControlBSChart.Text = "Bump Steer Chart";
            this.groupControlBSChart.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControlBSChart_Paint);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButtonClearCharrt);
            this.layoutControl1.Controls.Add(this.simpleButtonPlotMinBS);
            this.layoutControl1.Controls.Add(this.textBoxStepSize);
            this.layoutControl1.Controls.Add(this.textBoxYLowerLimit);
            this.layoutControl1.Controls.Add(this.textBoxYUpperLimit);
            this.layoutControl1.Controls.Add(this.textBoxXLowerLimit);
            this.layoutControl1.Controls.Add(this.textBoxXUpperLimit);
            this.layoutControl1.Controls.Add(this.groupControlBSChart);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(591, 317, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(668, 468);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButtonClearCharrt
            // 
            this.simpleButtonClearCharrt.Location = new System.Drawing.Point(570, 283);
            this.simpleButtonClearCharrt.Name = "simpleButtonClearCharrt";
            this.simpleButtonClearCharrt.Size = new System.Drawing.Size(86, 22);
            this.simpleButtonClearCharrt.StyleController = this.layoutControl1;
            this.simpleButtonClearCharrt.TabIndex = 6;
            this.simpleButtonClearCharrt.Text = "Clear Chart";
            this.simpleButtonClearCharrt.Click += new System.EventHandler(this.simpleButtonClearCharrt_Click);
            // 
            // simpleButtonPlotMinBS
            // 
            this.simpleButtonPlotMinBS.Location = new System.Drawing.Point(570, 247);
            this.simpleButtonPlotMinBS.Name = "simpleButtonPlotMinBS";
            this.simpleButtonPlotMinBS.Size = new System.Drawing.Size(86, 22);
            this.simpleButtonPlotMinBS.StyleController = this.layoutControl1;
            this.simpleButtonPlotMinBS.TabIndex = 5;
            this.simpleButtonPlotMinBS.Text = "Min. BumpSteer";
            this.simpleButtonPlotMinBS.Click += new System.EventHandler(this.simpleButtonMinBumpSteerChart_Click);
            // 
            // textBoxStepSize
            // 
            this.textBoxStepSize.Location = new System.Drawing.Point(609, 50);
            this.textBoxStepSize.Name = "textBoxStepSize";
            this.textBoxStepSize.Size = new System.Drawing.Size(36, 20);
            this.textBoxStepSize.TabIndex = 0;
            this.textBoxStepSize.Text = "5";
            this.textBoxStepSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxStepSize_KeyDown);
            this.textBoxStepSize.Leave += new System.EventHandler(this.textBoxStepSize_Leave);
            // 
            // textBoxYLowerLimit
            // 
            this.textBoxYLowerLimit.Location = new System.Drawing.Point(618, 197);
            this.textBoxYLowerLimit.Name = "textBoxYLowerLimit";
            this.textBoxYLowerLimit.Size = new System.Drawing.Size(38, 20);
            this.textBoxYLowerLimit.TabIndex = 4;
            this.textBoxYLowerLimit.Text = "-6";
            this.textBoxYLowerLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxYLowerLimit_KeyDown);
            this.textBoxYLowerLimit.Leave += new System.EventHandler(this.textBoxYLowerLimit_Leave);
            // 
            // textBoxYUpperLimit
            // 
            this.textBoxYUpperLimit.Location = new System.Drawing.Point(618, 173);
            this.textBoxYUpperLimit.Name = "textBoxYUpperLimit";
            this.textBoxYUpperLimit.Size = new System.Drawing.Size(38, 20);
            this.textBoxYUpperLimit.TabIndex = 3;
            this.textBoxYUpperLimit.Text = "6";
            this.textBoxYUpperLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxYUpperLimit_KeyDown);
            this.textBoxYUpperLimit.Leave += new System.EventHandler(this.textBoxYUpperLimit_Leave);
            // 
            // textBoxXLowerLimit
            // 
            this.textBoxXLowerLimit.Location = new System.Drawing.Point(618, 124);
            this.textBoxXLowerLimit.Name = "textBoxXLowerLimit";
            this.textBoxXLowerLimit.Size = new System.Drawing.Size(38, 20);
            this.textBoxXLowerLimit.TabIndex = 2;
            this.textBoxXLowerLimit.Text = "-25";
            this.textBoxXLowerLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxXLowerLimit_KeyDown);
            this.textBoxXLowerLimit.Leave += new System.EventHandler(this.textBoxXLowerLimit_Leave);
            // 
            // textBoxXUpperLimit
            // 
            this.textBoxXUpperLimit.Location = new System.Drawing.Point(618, 100);
            this.textBoxXUpperLimit.Name = "textBoxXUpperLimit";
            this.textBoxXUpperLimit.Size = new System.Drawing.Size(38, 20);
            this.textBoxXUpperLimit.TabIndex = 1;
            this.textBoxXUpperLimit.Text = "25";
            this.textBoxXUpperLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxXUpperLimit_KeyDown);
            this.textBoxXUpperLimit.Leave += new System.EventHandler(this.textBoxXUpperLimit_Leave);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem5,
            this.simpleLabelItem1,
            this.emptySpaceItem6,
            this.emptySpaceItem4,
            this.layoutControlItem7,
            this.emptySpaceItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(668, 468);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControlBSChart;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(558, 438);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(558, 17);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(90, 21);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(558, 136);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(90, 25);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textBoxXUpperLimit;
            this.layoutControlItem2.Location = new System.Drawing.Point(558, 88);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(90, 24);
            this.layoutControlItem2.Text = "X : Upper";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textBoxXLowerLimit;
            this.layoutControlItem3.Location = new System.Drawing.Point(558, 112);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(90, 24);
            this.layoutControlItem3.Text = "X : Lower";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(558, 209);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(90, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textBoxYUpperLimit;
            this.layoutControlItem4.Location = new System.Drawing.Point(558, 161);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(90, 24);
            this.layoutControlItem4.Text = "Y : Upper";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textBoxYLowerLimit;
            this.layoutControlItem5.Location = new System.Drawing.Point(558, 185);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(90, 24);
            this.layoutControlItem5.Text = "Y : Lower";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(558, 38);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(90, 50);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textBoxStepSize;
            this.layoutControlItem6.Location = new System.Drawing.Point(549, 38);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(88, 24);
            this.layoutControlItem6.Text = "Step Size";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(45, 13);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.simpleLabelItem1.Location = new System.Drawing.Point(558, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(90, 17);
            this.simpleLabelItem1.Text = "Chart";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(558, 10);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(558, 261);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(90, 10);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.simpleButtonPlotMinBS;
            this.layoutControlItem7.Location = new System.Drawing.Point(558, 235);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(90, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(558, 297);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(90, 151);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.simpleButtonClearCharrt;
            this.layoutControlItem8.Location = new System.Drawing.Point(558, 271);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(90, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // BumpSteerCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "BumpSteerCurve";
            this.Size = new System.Drawing.Size(668, 468);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBSChart)).EndInit();
            this.groupControlBSChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControlBSChart;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.TextBox textBoxXUpperLimit;
        private System.Windows.Forms.TextBox textBoxYLowerLimit;
        private System.Windows.Forms.TextBox textBoxYUpperLimit;
        private System.Windows.Forms.TextBox textBoxXLowerLimit;
        private System.Windows.Forms.TextBox textBoxStepSize;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        public DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        public DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        public DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        public DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        public DevExpress.XtraEditors.SimpleButton simpleButtonPlotMinBS;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        public DevExpress.XtraCharts.ChartControl chartControl1;
        public DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClearCharrt;
        public DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}

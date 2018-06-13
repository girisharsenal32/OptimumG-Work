namespace Coding_Attempt_with_GUI
{
    partial class XUC_KO_GenericChart
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
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine3 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine4 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            this.groupControlChart = new DevExpress.XtraEditors.GroupControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonClear = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonNoVariationChart = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxYLower = new System.Windows.Forms.TextBox();
            this.textBoxXLower = new System.Windows.Forms.TextBox();
            this.textBoxXUpper = new System.Windows.Forms.TextBox();
            this.textBoxYUpper = new System.Windows.Forms.TextBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlChart)).BeginInit();
            this.groupControlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlChart
            // 
            this.groupControlChart.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControlChart.AppearanceCaption.Options.UseFont = true;
            this.groupControlChart.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControlChart.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControlChart.Controls.Add(this.chartControl1);
            this.groupControlChart.Location = new System.Drawing.Point(12, 12);
            this.groupControlChart.Name = "groupControlChart";
            this.groupControlChart.Size = new System.Drawing.Size(547, 395);
            this.groupControlChart.TabIndex = 2;
            this.groupControlChart.Text = "Variation Chart";
            // 
            // chartControl1
            // 
            this.chartControl1.BorderOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(73)))), ((int)(((byte)(122)))));
            this.chartControl1.BorderOptions.Thickness = 3;
            this.chartControl1.CrosshairOptions.SnapMode = DevExpress.XtraCharts.CrosshairSnapMode.NearestValue;
            this.chartControl1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chartControl1.DataBindings = null;
            constantLine3.AxisValueSerializable = "0";
            constantLine3.Name = "Constant Line 1";
            constantLine3.ShowInLegend = false;
            constantLine3.Title.Visible = false;
            xyDiagram2.AxisX.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine3});
            xyDiagram2.AxisX.GridLines.MinorVisible = true;
            xyDiagram2.AxisX.GridLines.Visible = true;
            xyDiagram2.AxisX.Interlaced = true;
            xyDiagram2.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram2.AxisX.NumericScaleOptions.GridSpacing = 5D;
            xyDiagram2.AxisX.Tickmarks.CrossAxis = true;
            xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9F);
            xyDiagram2.AxisX.Title.MaxLineCount = 1;
            xyDiagram2.AxisX.Title.Text = "X Axis";
            xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisX.VisualRange.Auto = false;
            xyDiagram2.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram2.AxisX.VisualRange.MaxValueSerializable = "25";
            xyDiagram2.AxisX.VisualRange.MinValueSerializable = "-25";
            xyDiagram2.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram2.AxisX.WholeRange.Auto = false;
            xyDiagram2.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisX.WholeRange.MaxValueSerializable = "25";
            xyDiagram2.AxisX.WholeRange.MinValueSerializable = "-25";
            xyDiagram2.AxisX.WholeRange.SideMarginsValue = 0D;
            constantLine4.AxisValueSerializable = "0";
            constantLine4.Name = "Constant Line 1";
            constantLine4.ShowInLegend = false;
            constantLine4.Title.Visible = false;
            xyDiagram2.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine4});
            xyDiagram2.AxisY.GridLines.MinorVisible = true;
            xyDiagram2.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram2.AxisY.NumericScaleOptions.GridSpacing = 0.5D;
            xyDiagram2.AxisY.Tickmarks.CrossAxis = true;
            xyDiagram2.AxisY.Title.Font = new System.Drawing.Font("Tahoma", 9F);
            xyDiagram2.AxisY.Title.Text = "Y Axis";
            xyDiagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisualRange.Auto = false;
            xyDiagram2.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram2.AxisY.VisualRange.MaxValueSerializable = "5";
            xyDiagram2.AxisY.VisualRange.MinValueSerializable = "-5";
            xyDiagram2.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram2.AxisY.WholeRange.Auto = false;
            xyDiagram2.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisY.WholeRange.MaxValueSerializable = "5";
            xyDiagram2.AxisY.WholeRange.MinValueSerializable = "-5";
            xyDiagram2.AxisY.WholeRange.SideMarginsValue = 0D;
            xyDiagram2.EnableAxisXScrolling = true;
            xyDiagram2.EnableAxisXZooming = true;
            xyDiagram2.EnableAxisYScrolling = true;
            xyDiagram2.EnableAxisYZooming = true;
            this.chartControl1.Diagram = xyDiagram2;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.chartControl1.Legend.MarkerSize = new System.Drawing.Size(8, 14);
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.Location = new System.Drawing.Point(2, 20);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Aspect";
            series2.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            series2.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.True;
            series2.CrosshairLabelPattern = "{A}{V:#,#}";
            series2.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Custom Curve";
            lineSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(146)))), ((int)(((byte)(60)))));
            lineSeriesView2.LineMarkerOptions.Size = 5;
            lineSeriesView2.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.View = lineSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl1.SeriesTemplate.LegendName = "Default Legend";
            this.chartControl1.Size = new System.Drawing.Size(543, 373);
            this.chartControl1.TabIndex = 0;
            this.chartControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseClick);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButtonClear);
            this.layoutControl1.Controls.Add(this.simpleButtonNoVariationChart);
            this.layoutControl1.Controls.Add(this.textBoxYLower);
            this.layoutControl1.Controls.Add(this.textBoxXLower);
            this.layoutControl1.Controls.Add(this.textBoxXUpper);
            this.layoutControl1.Controls.Add(this.textBoxYUpper);
            this.layoutControl1.Controls.Add(this.groupControlChart);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(650, 419);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButtonClear
            // 
            this.simpleButtonClear.Location = new System.Drawing.Point(563, 234);
            this.simpleButtonClear.Name = "simpleButtonClear";
            this.simpleButtonClear.Size = new System.Drawing.Size(75, 22);
            this.simpleButtonClear.StyleController = this.layoutControl1;
            this.simpleButtonClear.TabIndex = 9;
            this.simpleButtonClear.Text = "Clear";
            this.simpleButtonClear.Click += new System.EventHandler(this.simpleButtonClearCharrt_Click);
            // 
            // simpleButtonNoVariationChart
            // 
            this.simpleButtonNoVariationChart.Location = new System.Drawing.Point(563, 208);
            this.simpleButtonNoVariationChart.Name = "simpleButtonNoVariationChart";
            this.simpleButtonNoVariationChart.Size = new System.Drawing.Size(75, 22);
            this.simpleButtonNoVariationChart.StyleController = this.layoutControl1;
            this.simpleButtonNoVariationChart.TabIndex = 8;
            this.simpleButtonNoVariationChart.Text = "No Variation";
            this.simpleButtonNoVariationChart.Click += new System.EventHandler(this.simpleButtonNOVariationChart_Click);
            // 
            // textBoxYLower
            // 
            this.textBoxYLower.Location = new System.Drawing.Point(611, 133);
            this.textBoxYLower.Name = "textBoxYLower";
            this.textBoxYLower.Size = new System.Drawing.Size(27, 20);
            this.textBoxYLower.TabIndex = 7;
            this.textBoxYLower.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxYLower_KeyDown);
            this.textBoxYLower.Leave += new System.EventHandler(this.textBoxYLower_Leave);
            // 
            // textBoxXLower
            // 
            this.textBoxXLower.Location = new System.Drawing.Point(611, 53);
            this.textBoxXLower.Name = "textBoxXLower";
            this.textBoxXLower.Size = new System.Drawing.Size(27, 20);
            this.textBoxXLower.TabIndex = 6;
            this.textBoxXLower.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxXLower_KeyDown);
            this.textBoxXLower.Leave += new System.EventHandler(this.textBoxXLower_Leave);
            // 
            // textBoxXUpper
            // 
            this.textBoxXUpper.Location = new System.Drawing.Point(611, 29);
            this.textBoxXUpper.Name = "textBoxXUpper";
            this.textBoxXUpper.Size = new System.Drawing.Size(27, 20);
            this.textBoxXUpper.TabIndex = 5;
            this.textBoxXUpper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxXUpper_KeyDown);
            this.textBoxXUpper.Leave += new System.EventHandler(this.textBoxXUpper_Leave);
            // 
            // textBoxYUpper
            // 
            this.textBoxYUpper.Location = new System.Drawing.Point(611, 109);
            this.textBoxYUpper.Name = "textBoxYUpper";
            this.textBoxYUpper.Size = new System.Drawing.Size(27, 20);
            this.textBoxYUpper.TabIndex = 4;
            this.textBoxYUpper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxYUpper_KeyDown);
            this.textBoxYUpper.Leave += new System.EventHandler(this.textBoxYUpper_Leave);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleLabelItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem3,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(650, 419);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControlChart;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(551, 399);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.Location = new System.Drawing.Point(551, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(79, 17);
            this.simpleLabelItem1.Text = "Chart";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textBoxYUpper;
            this.layoutControlItem2.Location = new System.Drawing.Point(551, 97);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem2.Text = "Y : Upper";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textBoxXUpper;
            this.layoutControlItem3.Location = new System.Drawing.Point(551, 17);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.Text = "X : Upper";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textBoxXLower;
            this.layoutControlItem4.Location = new System.Drawing.Point(551, 41);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem4.Text = "X : Lower";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(551, 65);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(79, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textBoxYLower;
            this.layoutControlItem5.Location = new System.Drawing.Point(551, 121);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem5.Text = "Y : Lower";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonNoVariationChart;
            this.layoutControlItem6.Location = new System.Drawing.Point(551, 196);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.simpleButtonClear;
            this.layoutControlItem7.Location = new System.Drawing.Point(551, 222);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(551, 248);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(79, 151);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(551, 145);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(79, 51);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XUC_KO_GenericChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XUC_KO_GenericChart";
            this.Size = new System.Drawing.Size(650, 419);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlChart)).EndInit();
            this.groupControlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlChart;
        public DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.TextBox textBoxXLower;
        private System.Windows.Forms.TextBox textBoxXUpper;
        private System.Windows.Forms.TextBox textBoxYUpper;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClear;
        private DevExpress.XtraEditors.SimpleButton simpleButtonNoVariationChart;
        private System.Windows.Forms.TextBox textBoxYLower;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}

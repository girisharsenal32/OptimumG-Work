namespace Coding_Attempt_with_GUI
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
            DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint46 = new DevExpress.XtraCharts.SeriesPoint(0D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint47 = new DevExpress.XtraCharts.SeriesPoint(5D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint48 = new DevExpress.XtraCharts.SeriesPoint(10D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint49 = new DevExpress.XtraCharts.SeriesPoint(15D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint50 = new DevExpress.XtraCharts.SeriesPoint(20D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint51 = new DevExpress.XtraCharts.SeriesPoint(-5D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint52 = new DevExpress.XtraCharts.SeriesPoint(-10D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint53 = new DevExpress.XtraCharts.SeriesPoint(-15D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint54 = new DevExpress.XtraCharts.SeriesPoint(-20D, new object[] {
            ((object)(0D))});
            DevExpress.XtraCharts.LineSeriesView lineSeriesView6 = new DevExpress.XtraCharts.LineSeriesView();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.groupControlBSChart = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
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
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView6)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.BorderOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(73)))), ((int)(((byte)(122)))));
            this.chartControl1.BorderOptions.Thickness = 2;
            this.chartControl1.DataBindings = null;
            xyDiagram6.AxisX.GridLines.Visible = true;
            xyDiagram6.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram6.AxisX.NumericScaleOptions.GridSpacing = 5D;
            xyDiagram6.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9F);
            xyDiagram6.AxisX.Title.Text = "Wheel Deflection";
            xyDiagram6.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram6.AxisX.VisualRange.Auto = false;
            xyDiagram6.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram6.AxisX.VisualRange.MaxValueSerializable = "25";
            xyDiagram6.AxisX.VisualRange.MinValueSerializable = "-25";
            xyDiagram6.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram6.AxisX.WholeRange.Auto = false;
            xyDiagram6.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram6.AxisX.WholeRange.MaxValueSerializable = "25";
            xyDiagram6.AxisX.WholeRange.MinValueSerializable = "-25";
            xyDiagram6.AxisX.WholeRange.SideMarginsValue = 0D;
            xyDiagram6.AxisY.Title.Font = new System.Drawing.Font("Tahoma", 9F);
            xyDiagram6.AxisY.Title.Text = "Toe Angle";
            xyDiagram6.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram6.AxisY.WholeRange.Auto = false;
            xyDiagram6.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram6.AxisY.WholeRange.MaxValueSerializable = "5";
            xyDiagram6.AxisY.WholeRange.MinValueSerializable = "-5";
            xyDiagram6.AxisY.WholeRange.SideMarginsValue = 1D;
            this.chartControl1.Diagram = xyDiagram6;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.MarkerSize = new System.Drawing.Size(8, 14);
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.TextVisible = false;
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(2, 20);
            this.chartControl1.Name = "chartControl1";
            series6.Name = "Series 1";
            series6.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint46,
            seriesPoint47,
            seriesPoint48,
            seriesPoint49,
            seriesPoint50,
            seriesPoint51,
            seriesPoint52,
            seriesPoint53,
            seriesPoint54});
            series6.View = lineSeriesView6;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series6};
            this.chartControl1.Size = new System.Drawing.Size(617, 439);
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
            this.groupControlBSChart.Size = new System.Drawing.Size(621, 461);
            this.groupControlBSChart.TabIndex = 1;
            this.groupControlBSChart.Text = "Bump Steer Chart";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textBoxStepSize);
            this.layoutControl1.Controls.Add(this.textBoxYLowerLimit);
            this.layoutControl1.Controls.Add(this.textBoxYUpperLimit);
            this.layoutControl1.Controls.Add(this.textBoxXLowerLimit);
            this.layoutControl1.Controls.Add(this.textBoxXUpperLimit);
            this.layoutControl1.Controls.Add(this.groupControlBSChart);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(591, 316, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(718, 495);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textBoxStepSize
            // 
            this.textBoxStepSize.Location = new System.Drawing.Point(685, 68);
            this.textBoxStepSize.Name = "textBoxStepSize";
            this.textBoxStepSize.Size = new System.Drawing.Size(21, 20);
            this.textBoxStepSize.TabIndex = 0;
            this.textBoxStepSize.Text = "5";
            this.textBoxStepSize.TextChanged += new System.EventHandler(this.textBoxStepSize_TextChanged);
            // 
            // textBoxYLowerLimit
            // 
            this.textBoxYLowerLimit.Location = new System.Drawing.Point(685, 222);
            this.textBoxYLowerLimit.Name = "textBoxYLowerLimit";
            this.textBoxYLowerLimit.Size = new System.Drawing.Size(21, 20);
            this.textBoxYLowerLimit.TabIndex = 4;
            this.textBoxYLowerLimit.Text = "-6";
            this.textBoxYLowerLimit.TextChanged += new System.EventHandler(this.textBoxYLowerLimit_TextChanged);
            // 
            // textBoxYUpperLimit
            // 
            this.textBoxYUpperLimit.Location = new System.Drawing.Point(685, 198);
            this.textBoxYUpperLimit.Name = "textBoxYUpperLimit";
            this.textBoxYUpperLimit.Size = new System.Drawing.Size(21, 20);
            this.textBoxYUpperLimit.TabIndex = 3;
            this.textBoxYUpperLimit.Text = "6";
            this.textBoxYUpperLimit.TextChanged += new System.EventHandler(this.textBoxYUpperLimit_TextChanged);
            // 
            // textBoxXLowerLimit
            // 
            this.textBoxXLowerLimit.Location = new System.Drawing.Point(685, 145);
            this.textBoxXLowerLimit.Name = "textBoxXLowerLimit";
            this.textBoxXLowerLimit.Size = new System.Drawing.Size(21, 20);
            this.textBoxXLowerLimit.TabIndex = 2;
            this.textBoxXLowerLimit.Text = "-25";
            this.textBoxXLowerLimit.TextChanged += new System.EventHandler(this.textBoxXLowerLimit_TextChanged);
            // 
            // textBoxXUpperLimit
            // 
            this.textBoxXUpperLimit.Location = new System.Drawing.Point(685, 121);
            this.textBoxXUpperLimit.Name = "textBoxXUpperLimit";
            this.textBoxXUpperLimit.Size = new System.Drawing.Size(21, 20);
            this.textBoxXUpperLimit.TabIndex = 1;
            this.textBoxXUpperLimit.Text = "25";
            this.textBoxXUpperLimit.TextChanged += new System.EventHandler(this.textBoxXUpperLimit_TextChanged);
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
            this.layoutControlItem6,
            this.simpleLabelItem1,
            this.emptySpaceItem4,
            this.emptySpaceItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(718, 495);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControlBSChart;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(625, 465);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(625, 32);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(73, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(625, 157);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(73, 29);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textBoxXUpperLimit;
            this.layoutControlItem2.Location = new System.Drawing.Point(625, 109);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(73, 24);
            this.layoutControlItem2.Text = "X : Upper";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textBoxXLowerLimit;
            this.layoutControlItem3.Location = new System.Drawing.Point(625, 133);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(73, 24);
            this.layoutControlItem3.Text = "X : Lower";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(625, 234);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(73, 241);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textBoxYUpperLimit;
            this.layoutControlItem4.Location = new System.Drawing.Point(625, 186);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(73, 24);
            this.layoutControlItem4.Text = "Y : Upper";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textBoxYLowerLimit;
            this.layoutControlItem5.Location = new System.Drawing.Point(625, 210);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(73, 24);
            this.layoutControlItem5.Text = "Y : Lower";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(625, 80);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(73, 29);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textBoxStepSize;
            this.layoutControlItem6.Location = new System.Drawing.Point(625, 56);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(73, 24);
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
            this.simpleLabelItem1.Location = new System.Drawing.Point(625, 15);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(73, 17);
            this.simpleLabelItem1.Text = "Chart";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(45, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(625, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(73, 15);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(625, 10);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // BumpSteerCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "BumpSteerCurve";
            this.Size = new System.Drawing.Size(718, 495);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.GroupControl groupControlBSChart;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private System.Windows.Forms.TextBox textBoxXUpperLimit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.TextBox textBoxYLowerLimit;
        private System.Windows.Forms.TextBox textBoxYUpperLimit;
        private System.Windows.Forms.TextBox textBoxXLowerLimit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.TextBox textBoxStepSize;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
    }
}

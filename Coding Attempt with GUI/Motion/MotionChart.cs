using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;


namespace Coding_Attempt_with_GUI
{
    public partial class MotionChart : XtraUserControl
    {

        MotionGUI motionChart_MotionGUI;
        SeriesPointCollection seriesPointsInChart, seriesPointsInChart2;
        bool DeflectionExists, SteeringExist;

        public MotionChart()
        {
            InitializeComponent();

        }

        public MotionChart(MotionGUI _motionGUI)
        {
            InitializeComponent();
            motionChart_MotionGUI = _motionGUI;

            /////<summary>Creating 2 points in the chart right at the start. This is done so that if user creates a motion item but doesn't create any points on the chart, then the software won't fail</summary>
            //AddPointToChart(chartControl1, 0, 0, 0);
            //AddPointToChart(chartControl1, 100, 0, 0);
            //motionChart_MotionGUI.MotionCreateOrEdit(false, DeflectionExists, SteeringExist);

        }

        private void MotionChart_Load(object sender, EventArgs e)
        {
            ///<summary>Creating 2 points in the chart right at the start. This is done so that if user creates a motion item but doesn't create any points on the chart, then the software won't fail</summary>
            //AddPointToChart(chartControl1, 0, 0, 0);
            //AddPointToChart(chartControl1, 100, 0, 0);
            //motionChart_MotionGUI.MotionCreateOrEdit(false, DeflectionExists, SteeringExist);
        }

        public void AddPointToChart(ChartControl _chart, double _x, double _y, int _seriesNo)
        {
            _chart.Series[_seriesNo].Points.AddPoint(_x, _y);
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                XYDiagram xYDiagram = (XYDiagram)chartControl1.Diagram;

                double LastMouseCentreX = xYDiagram.PointToDiagram(e.Location).NumericalArgument;
                double LastMouseCentreY = xYDiagram.PointToDiagram(e.Location).NumericalValue;

                AddPointToChart(chartControl1, LastMouseCentreX, LastMouseCentreY, 0);
                LineSeriesView line = new LineSeriesView();

                seriesPointsInChart = chartControl1.Series[0].Points;
                DeflectionExists = true;
            }

        }

        private void chartControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                XYDiagram xYDiagram = (XYDiagram)chartControl2.Diagram;

                double LastMouseCentreX = xYDiagram.PointToDiagram(e.Location).NumericalArgument;
                double LastMouseCentreY = xYDiagram.PointToDiagram(e.Location).NumericalValue;

                AddPointToChart(chartControl2, LastMouseCentreX, LastMouseCentreY, 0);
                LineSeriesView line = new LineSeriesView();

                seriesPointsInChart2 = chartControl2.Series[0].Points;
                SteeringExist = true;
            }
        }

        static int lastY = -1;
        static bool isPressed = false;
        static SeriesPoint selectedPoint = null;

        private void chartControl1_ObjectHotTracked(object sender, HotTrackEventArgs e)
        {
            #region Add tooltip code here 
            //if (e.HitInfo.SeriesPoint != null)
            //    selectedPoint = e.HitInfo.SeriesPoint;

            //if (selectedPoint != null && isPressed)
            //{

            //    DiagramCoordinates point =
            //        ((XYDiagram)(sender as ChartControl).Diagram).PointToDiagram(e.HitInfo.HitPoint);

            //    if (lastY != -1)
            //    {
            //        VisualRange rangeY = ((XYDiagram)(sender as ChartControl).Diagram).AxisY.VisualRange;
            //        double delta = ((double)rangeY.MaxValue - (double)rangeY.MinValue) / 8;

            //        if (selectedPoint.Values[0] >= (double)rangeY.MaxValue - delta)
            //            rangeY.MaxValue = selectedPoint.Values[0] + delta;

            //        selectedPoint.Values[0] = point.NumericalValue;

            //    }

            //    ((ChartControl)sender).RefreshData();
            //    lastY = e.HitInfo.HitPoint.Y;
            //    return;
            //}

            //lastY = -1; 
            #endregion
        }

        private void chartControl1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
        }

        private void saveAsTemplateChartItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void loadTemplateChartItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }



        private void chartControl1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            motionChart_MotionGUI.MotionCreateOrEdit(false, DeflectionExists, SteeringExist);
        }
        private void chartControl2_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            motionChart_MotionGUI.MotionCreateOrEdit(false, DeflectionExists, SteeringExist);
        }
    }
}


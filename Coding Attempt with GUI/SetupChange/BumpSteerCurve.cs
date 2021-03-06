﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    public partial class BumpSteerCurve : DevExpress.XtraEditors.XtraUserControl
    {

        #region --Chart Params--
        /// <summary>
        /// X Axis Upper Limit of the Chart
        /// </summary>
        public double X_Upper { get; set; }
        /// <summary>
        /// X Axis Lower Limit of the Chart
        /// </summary>
        public double X_Lower { get; set; }
        /// <summary>
        /// Y Axis Upper Limit of the Chart
        /// </summary>
        public double Y_Upper { get; set; }
        /// <summary>
        /// Y Axis Lower Limit of the Chart
        /// </summary>
        public double Y_Lower { get; set; }
        /// <summary>
        /// Step Size of the Wheel Deflection
        /// </summary>
        public double StepSize { get; set; }
        /// <summary>
        /// Array containing the X Coordinates of the Chart
        /// </summary>
        List<double> ChartPoints_X { get; set; }
        /// <summary>
        /// Array containing the Y Coordinates of the Chart
        /// </summary>
        List<double> ChartPoints_Y { get; set; }
        /// <summary>
        /// Boolean to determine if the Chart is being plotted by the USER during input or by solver during Output Display
        /// </summary>
        public bool IsOutputChart { get; set; }

        #endregion

        public CustomBumpSteerParams BumpSteerParms;

        SetupChange_CornerVariables Setup_CV;

        KO_CornverVariables KO_CV;

        /// <summary>
        /// Object which would contain the Series Points of the Chart
        /// </summary>
        public SeriesPointCollection seriesPointsInChart { get; set; }

        public bool CustomBumpSteerCurve { get; set; }

        public BumpSteerCurve()
        {
            InitializeComponent();

            ChartPoints_X = new List<double>();

            ChartPoints_Y = new List<double>();

            BumpSteerParms = new CustomBumpSteerParams();

            //AddPointToChart(chartControl1, 0, 0, 0, false);
        }

        /// <summary>
        /// Method to initialize the Parent Data of the Bump Steer Chart
        /// ---IMP---This is used only for the Input section of the Setup Change and not the Output Section
        /// </summary>
        /// <param name="_setupCV"></param>
        public void GetParentObjectData(SetupChange_CornerVariables _setupCV)
        {
            Setup_CV = _setupCV;

            Setup_CV.BS_Params = BumpSteerParms;

            AddPointToChart(chartControl1, 0, 0, 0, false);
        }

        public void GetParentObjectData(KO_CornverVariables _koCV)
        {
            KO_CV = _koCV;

            KO_CV.BumpSteerCurve = BumpSteerParms;

            AddPointToChart(chartControl1, 0, 0, 0, false);
        }


        /// <summary>
        /// Method to add points to the Chart
        /// </summary>
        /// <param name="_chart"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_seriesNo"></param>
        public void AddPointToChart(ChartControl _chart, double _x, double _y, int _seriesNo, bool _isOutputChart)
        {
            _chart.Series[_seriesNo].Points.AddPoint(_x, _y);

            if (!_isOutputChart)
            {
                ChartPoints_X.Add(_x);

                ChartPoints_Y.Add(_y);

                BumpSteerParms.PopulateBumpSteerGraph(ChartPoints_X, ChartPoints_Y); 
            }

        }
        /// <summary>
        /// Method to add a new Series to the Chart
        /// </summary>
        /// <param name="_chart"></param>
        public void AddSeriesToChart(ChartControl _chart)
        {
            if (!_chart.Series.Contains(_chart.Series["Computed BS Curve"]))
            {
                _chart.Series.Add("Computed BS Curve", ViewType.Line); 
            }
            else
            {
                _chart.Series["Computed BS Curve"].Points.Clear();
            }
        }

        /// <summary>
        /// Event raised during the MouseClick event inside the chart
        /// ----Chart is disabled during Output Plotting and hence this won't fired---
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsOutputChart) 
            {
                if (e.Button == MouseButtons.Right)
                {
                    XYDiagram xYDiagram = (XYDiagram)chartControl1.Diagram;

                    double LastMouseCentreX = xYDiagram.PointToDiagram(e.Location).NumericalArgument;
                    double LastMouseCentreY = xYDiagram.PointToDiagram(e.Location).NumericalValue;

                    AddPointToChart(chartControl1, LastMouseCentreX, LastMouseCentreY, 0, false);
                    LineSeriesView line = new LineSeriesView();

                    seriesPointsInChart = chartControl1.Series[0].Points;
                    CustomBumpSteerCurve = true;
                } 
            }
        }

        /// <summary>
        /// Event raised when the <see cref="simpleButtonPlotMinBS"/> is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMinBumpSteerChart_Click(object sender, EventArgs e)
        {
            PlotMinBumpSteerChart();
        }

        /// <summary>
        /// Method to Plot a Minimum Bump Steer Chart. 
        /// That is a chart with 0 Toe Angle VARIATION
        /// </summary>
        private void PlotMinBumpSteerChart()
        {
            AddPointToChart(chartControl1, 25, 0, 0, false);

            AddPointToChart(chartControl1, -25, 0, 0, false);
        }


        /// <summary>
        /// Event fired when the <see cref="simpleButtonClearCharrt"/> button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClearCharrt_Click(object sender, EventArgs e)
        {
            ClearChart();
        }

        /// <summary>
        /// Method to clear the chart
        /// </summary>
        private void ClearChart()
        {
            ///<summary>Clearing the chart</summary>
            chartControl1.Series[0].Points.Clear();

            ChartPoints_X.Clear();

            ChartPoints_Y.Clear();

            ///<summary>Re-plotting the point 0,0</summary>
            AddPointToChart(chartControl1, 0, 0, 0, false);


        }


        #region ---Chart Params Text Changed Events---
        private void textBoxStepSize_Leave(object sender, EventArgs e)
        {
            SetStepSize();
        }
        private void textBoxStepSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetStepSize();
            }
        }

        private void SetStepSize()
        {
            if (Double.TryParse(textBoxStepSize.Text, out double result))
            {
                if (result < 0)
                {
                    MessageBox.Show("Step Size can't be negative");
                }
                else
                {
                    StepSize = result;

                    Setup_CV.BS_Params.StepSize = StepSize;

                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }





        private void textBoxXUpperLimit_Leave(object sender, EventArgs e)
        {
            SetXUpperLimit();
        }

        private void textBoxXUpperLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetXUpperLimit();
            }
        }

        private void SetXUpperLimit()
        {
            if (Double.TryParse(textBoxXUpperLimit.Text, out double result))
            {
                if (result < 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    X_Upper = result;

                    XYDiagram xyDiagram = (XYDiagram)chartControl1.Diagram;

                    xyDiagram.AxisX.WholeRange.MaxValue = X_Upper;
                    xyDiagram.AxisX.VisualRange.MaxValue = X_Upper;

                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }







        private void textBoxXLowerLimit_Leave(object sender, EventArgs e)
        {
            SetXLowerLimit();
        }

        private void textBoxXLowerLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetXLowerLimit();
            }
        }

        private void SetXLowerLimit()
        {
            if (Double.TryParse(textBoxXLowerLimit.Text, out double result))
            {
                if (result > 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    X_Lower = result;

                    XYDiagram xyDiagram = (XYDiagram)chartControl1.Diagram;

                    xyDiagram.AxisX.WholeRange.MinValue = X_Lower;
                    xyDiagram.AxisX.VisualRange.MinValue = X_Lower;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }









        private void textBoxYUpperLimit_Leave(object sender, EventArgs e)
        {
            SetYUpperLimit();
        }

        private void textBoxYUpperLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetYUpperLimit();
            }
        }
        private void SetYUpperLimit()
        {
            if (Double.TryParse(textBoxYUpperLimit.Text, out double result))
            {
                if (result < 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    Y_Upper = result;

                    XYDiagram xyDiagram = (XYDiagram)chartControl1.Diagram;

                    xyDiagram.AxisY.WholeRange.MaxValue = Y_Upper;
                    xyDiagram.AxisY.VisualRange.MaxValue = Y_Upper;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }






        private void textBoxYLowerLimit_Leave(object sender, EventArgs e)
        {
            SetYLowerLimit();
        }

        private void textBoxYLowerLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetYLowerLimit();
            }
        }

        private void SetYLowerLimit()
        {
            if (Double.TryParse(textBoxYLowerLimit.Text, out double result))
            {
                if (result > 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    Y_Lower = result;

                    XYDiagram xyDiagram = (XYDiagram)chartControl1.Diagram;

                    xyDiagram.AxisY.WholeRange.MinValue = Y_Lower;
                    xyDiagram.AxisY.VisualRange.MinValue = Y_Lower;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }

        private void groupControlBSChart_Paint(object sender, PaintEventArgs e)
        {

        } 
        #endregion


    }
}

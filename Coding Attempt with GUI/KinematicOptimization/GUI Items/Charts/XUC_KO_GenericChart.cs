using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;


namespace Coding_Attempt_with_GUI
{
    public partial class XUC_KO_GenericChart : XtraUserControl
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
        public int StepSize { get; set; }
        /// <summary>
        /// Array containing the X Coordinates of the Chart
        /// </summary>
        public List<double> ChartPoints_X { get; set; }
        /// <summary>
        /// Array containing the Y Coordinates of the Chart
        /// </summary>
        public List<double> ChartPoints_Y { get; set; }
        /// <summary>
        /// Boolean to determine if the Chart is being plotted by the USER during input or by solver during Output Display
        /// </summary>
        public bool IsOutputChart { get; set; }

        /// <summary>
        /// Object of the <see cref="MotionProfiles"/> which determines which type of Motion profile is going to be depicted by this CHart
        /// </summary>
        public MotionProfiles Profile { get; set; }


        #endregion

        /// <summary>
        /// Object which would contain the Series Points of the Chart
        /// </summary>
        public SeriesPointCollection seriesPointsInChart { get; set; }

        public XUC_KO_GenericChart()
        {
            InitializeComponent();
        }


        #region ---Add Points to Chart Methods---
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

                //BumpSteerParms.PopulateBumpSteerGraph(ChartPoints_X, ChartPoints_Y);
            }

        }
        #endregion

        #region ---Chart Click Events---
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

                }
            }
        } 
        #endregion

        #region ---Plot No Variation Chart Events---
        /// <summary>
        /// Event raised when the <see cref="simpleButtonPlotMinBS"/> is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNOVariationChart_Click(object sender, EventArgs e)
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
        #endregion

        #region ---Clear Chart Events---
        /// <summary>
        /// Event fired when the <see cref="simpleButtonClearCharrt"/> button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClearCharrt_Click(object sender, EventArgs e)
        {
            ClearChart();

            ChartPoints_X = new List<double>();

            ChartPoints_Y = new List<double>();

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
        #endregion

        #region ---Chart Parameters Text Changed Events---
        /// <summary>
        /// Upper Limit X Textbox Change Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxXUpper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetXUpperLimit();
            }
        }

        private void textBoxXUpper_Leave(object sender, EventArgs e)
        {
            SetXUpperLimit();
        }

        private void SetXUpperLimit()
        {
            if (Double.TryParse(textBoxXUpper.Text, out double result))
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



        /// <summary>
        /// Lower Limit X Textbox Changed Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxXLower_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetXLowerLimit();
            }
        }

        private void textBoxXLower_Leave(object sender, EventArgs e)
        {
            SetXLowerLimit();
        }

        private void SetXLowerLimit()
        {
            if (Double.TryParse(textBoxXLower.Text, out double result))
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





        /// <summary>
        /// Upper Limit Y Textbox Changed Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxYUpper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetYUpperLimit();
            }
        }

        private void textBoxYUpper_Leave(object sender, EventArgs e)
        {
            SetYUpperLimit();
        }

        private void SetYUpperLimit()
        {
            if (Double.TryParse(textBoxYUpper.Text, out double result))
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






        /// <summary>
        /// Lower Limit Y Textbox Changed Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxYLower_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetYLowerLimit();
            }
        }

        private void textBoxYLower_Leave(object sender, EventArgs e)
        {
            SetYLowerLimit();
        }

        private void SetYLowerLimit()
        {
            if (Double.TryParse(textBoxYLower.Text, out double result))
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
        #endregion









    }
}

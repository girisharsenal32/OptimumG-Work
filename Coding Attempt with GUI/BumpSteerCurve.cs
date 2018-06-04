using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;

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
        public int StepSize { get; set; }
        /// <summary>
        /// Array containing the X Coordinates of the Chart
        /// </summary>
        List<double> ChartPoints_X { get; set; }
        /// <summary>
        /// Array containing the Y Coordinates of the Chart
        /// </summary>
        List<double> ChartPoints_Y { get; set; }

        #endregion

        List<double> WheelDeflections;

        List<double> ToeAngles;

        SetupChange_CornerVariables Setup_CV;


        /// <summary>
        /// Object which would contain the Series Points of the Chart
        /// </summary>
        public SeriesPointCollection seriesPointsInChart { get; set; }

        public bool CustomBumpSteerCurve { get; set; }

        public BumpSteerCurve()
        {
            InitializeComponent();
        }

        public void GetParentObjectData(SetupChange_CornerVariables _setupCV)
        {
            Setup_CV = _setupCV;
        }


        /// <summary>
        /// Method to add points to the Chart
        /// </summary>
        /// <param name="_chart"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_seriesNo"></param>
        public void AddPointToChart(ChartControl _chart, double _x, double _y, int _seriesNo)
        {
            _chart.Series[_seriesNo].Points.AddPoint(_x, _y);

            ChartPoints_X.Add(_x);

            ChartPoints_Y.Add(_y);

            WheelDeflections = ChartPoints_X;

            ToeAngles = ChartPoints_Y;

            WheelDeflections.Sort();

            ToeAngles.Sort();

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
                CustomBumpSteerCurve = true;
            }
        }

        private void textBoxStepSize_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(e.ToString(),out int result))
            {
                if (result < 0)
                {
                    MessageBox.Show("Step Size can't be negative");
                }
                else
                {
                    StepSize = result;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }

        private void textBoxXUpperLimit_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(e.ToString(), out double result))
            {
                if (result < 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    X_Upper = result;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }

        private void textBoxXLowerLimit_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(e.ToString(), out double result))
            {
                if (result > 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    X_Lower = result;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }

        private void textBoxYUpperLimit_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(e.ToString(), out double result))
            {
                if (result < 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    Y_Upper = result;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }

        private void textBoxYLowerLimit_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(e.ToString(), out double result))
            {
                if (result > 0)
                {
                    MessageBox.Show("Upper Limit can't be negative");
                }
                else
                {
                    Y_Lower = result;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Numeric Values");
            }
        }
    }
}

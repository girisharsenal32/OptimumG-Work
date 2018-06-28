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
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    public partial class XUC_KO_Camber : XtraUserControl
    {
        /// <summary>
        /// Object of the <see cref="CustomCamberCurve"/> Class which contains information regarding the Heave/Steering Profile and corresponding Camber Curves
        /// </summary>
        public CustomCamberCurve CamberCurve { get; set; }

        public XUC_KO_Camber()
        {
            InitializeComponent();

            ///<summary>Customizing the Chart to Accept Heave Camber Variation</summary>
            Initialize_HeaveChartParams();

            ///<summary>Customizing the Chart to Accept Steering Camber Variation</summary>
            Initialize_SteeringChartParams();

            CamberCurve = new CustomCamberCurve();

            xuC_KO_CamberHeave.Profile = MotionProfiles.Heave;

            xuC_KO_CamberSteering.Profile = MotionProfiles.Steering;
        }

        /// <summary>
        /// Method to Customize the Chart for Heave
        /// </summary>
        private void Initialize_HeaveChartParams()
        {
            ///<summary>Assigning Name to Chart's Series</summary>
            xuC_KO_CamberHeave.chartControl1.Series[0].Name = "Custom Camber Curve";

            ///<summary>Obtaining the Diagram of the Chart</summary>
            XYDiagram steeringDiag = (XYDiagram)xuC_KO_CamberHeave.chartControl1.Diagram;

            ///<summary>Customizing the X Axis of the CHart</summary>
            steeringDiag.AxisX.Title.Text = "Wheel Deflection (mm)";

            steeringDiag.AxisX.WholeRange.MaxValue = 25;

            steeringDiag.AxisX.WholeRange.MinValue = -25;

            ///<summary>Customizing the Y Axis of the Chart</summary>
            steeringDiag.AxisY.Title.Text = "Camber Angle (deg)";


        }

        /// <summary>
        /// Method to Customize the Chart for Steering
        /// </summary>
        private void Initialize_SteeringChartParams()
        {
            ///<summary>Assigning Name to Chart's Series</summary
            xuC_KO_CamberSteering.chartControl1.Series[0].Name = "Custom Camber Curve";

            ///<summary>Obtaining the Diagram of the Chart</summary>
            XYDiagram steeringDiag = (XYDiagram)xuC_KO_CamberSteering.chartControl1.Diagram;

            ///<summary>Customizing the X Axis of the CHart</summary>
            steeringDiag.AxisX.Title.Text = "Steering Wheel Angle (deg)";

            steeringDiag.AxisX.WholeRange.MaxValue = 120;

            steeringDiag.AxisX.WholeRange.MinValue = -120;

            ///<summary>Customizing the Y Axis of the Chart</summary>
            steeringDiag.AxisY.Title.Text = "Camber Angle (deg)";

        }

        /// <summary>
        /// Internam method to create the Heave Profile and Corresponding Camber Angles using <see cref="CustomCamberCurve.WheelDeflections"/> and <see cref="CustomCamberCurve.CamberAnglesHeave"/>
        /// </summary>
        private void UpdateHeaveChart()
        {
            CamberCurve.Populate_CamerVariation_Heave(xuC_KO_CamberHeave.ChartPoints_X, xuC_KO_CamberHeave.ChartPoints_Y);
        }

        /// <summary>
        /// Internam method to create the Steering Profile and Corresponding Camber Angles using <see cref="CustomCamberCurve.SteeringWheelAngles"/> and <see cref="CustomCamberCurve.CamberAnglesSteering"/>
        /// </summary>

        private void UpdateSteeringChart()
        {
            CamberCurve.Populate_CamerVariation_Steering(xuC_KO_CamberSteering.ChartPoints_X, xuC_KO_CamberSteering.ChartPoints_Y);
        }




    }
}

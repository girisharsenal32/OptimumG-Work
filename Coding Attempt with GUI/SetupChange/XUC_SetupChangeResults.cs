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
using DevExpress.XtraVerticalGrid;
using MathNet.Spatial.Units;
using DevExpress.XtraCharts;

namespace Coding_Attempt_with_GUI
{
    public partial class XUC_SetupChangeResults : DevExpress.XtraEditors.XtraUserControl
    {
        public XUC_SetupChangeResults()
        {
            InitializeComponent();

            ShowRelevant();
        }

        /// <summary>
        /// Method to hide the unneccesary layout items of the <see cref="BumpSteerCurve"/> user control 
        /// </summary>
        private void ShowRelevant()
        {
            ///<summary>Hiding the empyt space items in the right</summary>
            bumpSteerCurve1.emptySpaceItem1.HideToCustomization();
            bumpSteerCurve1.emptySpaceItem2.HideToCustomization();
            bumpSteerCurve1.emptySpaceItem3.HideToCustomization();
            bumpSteerCurve1.emptySpaceItem4.HideToCustomization();
            bumpSteerCurve1.emptySpaceItem5.HideToCustomization();
            bumpSteerCurve1.emptySpaceItem7.HideToCustomization();


            ///<summary>Hiding the Layout items which contain the Textboxes</summary>
            bumpSteerCurve1.layoutControlItem2.HideToCustomization();
            bumpSteerCurve1.layoutControlItem3.HideToCustomization();
            bumpSteerCurve1.layoutControlItem4.HideToCustomization();
            bumpSteerCurve1.layoutControlItem5.HideToCustomization();
            bumpSteerCurve1.layoutControlItem6.HideToCustomization();

            ///<summary>Hiding the Title on the Right Side</summary>
            bumpSteerCurve1.simpleLabelItem1.HideToCustomization();

            ///<summary>Hiding the Layout item containing the Buttons</summary>
            bumpSteerCurve1.layoutControlItem7.HideToCustomization();
            bumpSteerCurve1.layoutControlItem8.HideToCustomization();
            
        }



        /// <summary>
        /// Method to display the Outputs of Each Setup Change. If a particular param is not requested the initial value is shown
        /// </summary>
        /// <param name="_oc"></param>
        /// <param name="_setupOP"></param>
        /// <param name="_cv"></param>
        /// <param name="_resultsGUI"></param>
        /// <param name="_resultsGrid"></param>
        /// <param name="_converged"></param>
        public void DisplayIndividualOutputs(OutputClass _oc, SetupChange_Outputs _setupOP, SetupChange_CornerVariables _cv, XUC_SetupChangeResults _resultsGUI, VGridControl _resultsGrid, ref bool _converged)
        {
            string test = _cv.kpiAdjustmentTool.ToString();

            ///<summary>Assining the KPI Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowKPIAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.KPI).Degrees, 2)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_KPI.Degrees, 2)));
            if (_cv.KPIChangeRequested || _cv.constKPI || _cv.CasterChangeRequested || _cv.constCaster)
            {
                if (_cv.Master_Adj["Caster/KPI"].ContainsKey(AdjustmentTools.TopFrontArm.ToString()))
                {
                    _resultsGrid.SetCellValue(_resultsGUI.rowTopFrontAdj, 1, Convert.ToString(Math.Round(_setupOP.TopFrontLength, 3)));
                    _resultsGUI.rowTopFrontAdj.Visible = true;
                }
                if (_cv.Master_Adj["Caster/KPI"].ContainsKey(AdjustmentTools.TopRearArm.ToString()))
                {
                    _resultsGrid.SetCellValue(_resultsGUI.rowTopRearAdj, 1, Convert.ToString(Math.Round(_setupOP.TopRearLength, 3)));
                    _resultsGUI.rowTopRearAdj.Visible = true;
                }
                if (_cv.Master_Adj["Caster/KPI"].ContainsKey(AdjustmentTools.BottomFrontArm.ToString()))
                {
                    _resultsGrid.SetCellValue(_resultsGUI.rowBottomFrontAdj, 1, Convert.ToString(Math.Round(_setupOP.BottomFrontLength, 3)));
                    _resultsGUI.rowBottomFrontAdj.Visible = true;
                }
                if (_cv.Master_Adj["Caster/KPI"].ContainsKey(AdjustmentTools.BottomRearArm.ToString()))
                {
                    _resultsGrid.SetCellValue(_resultsGUI.rowBottomRearAdj, 1, Convert.ToString(Math.Round(_setupOP.BottomRearLength, 3)));
                    _resultsGUI.rowBottomRearAdj.Visible = true;
                }
            }
            _resultsGrid.SetCellValue(_resultsGUI.rowKPIConvergance, 1, _setupOP.KPI_Conv.ConvergenceStatus);


            ///<summary>Assingint the Caster Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowCasterAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.Caster).Degrees, 2)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_Caster.Degrees, 2)));
            _resultsGrid.SetCellValue(_resultsGUI.rowCasterConvergance, 1, _setupOP.Caster_Conv.ConvergenceStatus);


            ///<summary>Assining the Camber Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowCamberAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.waOP.StaticCamber).Degrees, 3)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_Camber.Degrees, 3)));
            if (_cv.CamberChangeRequested || _cv.constCamber)
            {
                if (_cv.Master_Adj["Camber"].ContainsKey(AdjustmentTools.TopCamberMount.ToString()))
                {
                    _resultsGrid.SetCellValue(rowTopCamberMount, 1, Convert.ToString(Math.Round(_setupOP.TopCamberShimsLength,3))); 
                    _resultsGrid.SetCellValue(rowShimsTopCamberMount, 1, Convert.ToString(Math.Round(_setupOP.TopCamberShimsNo, 3)));
                    rowTopCamberMount.Visible = true;
                }
                if (_cv.Master_Adj["Camber"].ContainsKey(AdjustmentTools.BottomCamberMount.ToString()))
                {
                    _resultsGrid.SetCellValue(rowBottomCamberMount, 1, Convert.ToString(Math.Round(_setupOP.BottomCamberShimsLength, 3)));
                    _resultsGrid.SetCellValue(rowShimsBottomCamberMount, 1, Convert.ToString(Math.Round(_setupOP.BottomCamberShimsNo, 3)));
                    rowBottomCamberMount.Visible = true;
                }
            }
            _resultsGrid.SetCellValue(_resultsGUI.rowCamberConvergance, 1, _setupOP.Camber_Conv.ConvergenceStatus);
            

            ///<summary>Assigning the Toe Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowToeAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.waOP.StaticToe).Degrees, 3)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_Toe.Degrees, 3)));
            if (_cv.ToeChangeRequested || _cv.constToe)
            {
                if (_cv.Master_Adj["Toe"].ContainsKey(AdjustmentTools.ToeLinkLength.ToString()))
                {
                    _resultsGrid.SetCellValue(rowToeLink, 1, Convert.ToString(Math.Round(_setupOP.ToeLinklength, 3)));
                    rowToeLink.Visible = true;
                }
            }
            _resultsGrid.SetCellValue(_resultsGUI.rowToeConvergance, 1, _setupOP.Toe_Conv.ConvergenceStatus);
            

            ///<summary>Assigning the Ride Height</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowRideHeight, 1, _setupOP.Calc_RideHeight);
            _resultsGrid.SetCellValue(_resultsGUI.rowLinkRHName, 1, _cv.rideheightAdjustmentTool.ToString());
            _resultsGrid.SetCellValue(rowLinkRHDelta, 1, Convert.ToString(Math.Round(_setupOP.PushrodLength, 3)));
            _resultsGrid.SetCellValue(_resultsGUI.rowRHConvergance, 1, _setupOP.RideHeight_Conv.ConvergenceStatus);


            ///<summary>Setting the results of the Bump Steer</summary>
            if (_cv.constBumpSteer || _cv.BumpSteerChangeRequested)
            {
                _resultsGrid.SetCellValue(rowToeLinkInboard_x, 1, Convert.ToString(Math.Round(_setupOP.ToeLinkInboard.X, 3)));
                rowToeLinkInboard_x.Visible = true;
                _resultsGrid.SetCellValue(rowToeLinkInboard_y, 1, Convert.ToString(Math.Round(_setupOP.ToeLinkInboard.Y, 3)));
                rowToeLinkInboard_y.Visible = true;
                _resultsGrid.SetCellValue(rowToeLinkInboard_z, 1, Convert.ToString(Math.Round(_setupOP.ToeLinkInboard.Z, 3)));
                rowToeLinkInboard_z.Visible = true;
            }

            _resultsGrid.SetCellValue(rowBSConvergence, 1, _setupOP.BumpSteer_Conv.ConvergenceStatus);
            

        }


        public void PlotBumpSteerGraph(SetupChange_Outputs _setupOP, SetupChange_CornerVariables _cv, XUC_SetupChangeResults _resultsGUI)
        {
            if (_cv.BumpSteerChangeRequested)
            {
                ///<summary>If the Bump Steer Change is requested then setting the Enaled status to true so that the user can scroll and zoom the Bump Steer Contro </summary>
                _resultsGUI.bumpSteerCurve1.Enabled = true;

                ///<summary>Setting the <see cref="BumpSteerCurve.IsOutputChart"/> value to true to teach the CHart that the Output is calling it. 
                ///---IMPORTANT--- This is an important step so that the accidentally clicking the control doesn;t create a series point
                /// </summary>
                _resultsGUI.bumpSteerCurve1.IsOutputChart = true;

                ///<summary>Plotting the Computed Bump Steer Chart</summary>
                for (int i = 0; i < _setupOP.Calc_BumpSteerChart.Count; i++)
                {
                    _resultsGUI.bumpSteerCurve1.AddPointToChart(_resultsGUI.bumpSteerCurve1.chartControl1, _cv.BS_Params.WheelDeflections[_cv.BS_Params.HighestBumpindex + i], _setupOP.Calc_BumpSteerChart[i].Degrees, 0, true);
                }

                _resultsGUI.bumpSteerCurve1.AddSeriesToChart(_resultsGUI.bumpSteerCurve1.chartControl1);

                for (int i = 0; i < _setupOP.Req_BumpSteerChart.Count; i++)
                {
                    _resultsGUI.bumpSteerCurve1.AddPointToChart(_resultsGUI.bumpSteerCurve1.chartControl1, _cv.BS_Params.WheelDeflections[_cv.BS_Params.HighestBumpindex + i], _setupOP.Req_BumpSteerChart[i].Degrees, 1, true);
                }

                _resultsGUI.bumpSteerCurve1.Enabled = true;
            }

        }











    }
}

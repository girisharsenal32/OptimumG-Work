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

            ///<summary>Hiding the Layout items which contain the Textboxes</summary>
            bumpSteerCurve1.layoutControlItem2.HideToCustomization();
            bumpSteerCurve1.layoutControlItem3.HideToCustomization();
            bumpSteerCurve1.layoutControlItem4.HideToCustomization();
            bumpSteerCurve1.layoutControlItem5.HideToCustomization();
            bumpSteerCurve1.layoutControlItem6.HideToCustomization();

            ///<summary>Hiding the Title on the Right Side</summary>
            bumpSteerCurve1.simpleLabelItem1.HideToCustomization();

            ///<summary>Hiding the Layout item containing the Button</summary>
            bumpSteerCurve1.layoutControlItem7.HideToCustomization();

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


            //if (_cls.CamberConvergence == Convergence.UnSuccessful || _cls.ToeConvergence == Convergence.UnSuccessful || _cls.CasterConvergence == Convergence.UnSuccessful || _cls.KPIConvergence == Convergence.UnSuccessful || _cls.LinkLengthConvergence == Convergence.UnSuccessful)
            //{
            //    _converged = false;
            //}




            ///<summary>Assining the KPI Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowKPIAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.KPI).Degrees, 2)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_KPI.Degrees, 2)));
            //if (_cv.kpiAdjustmentTool == AdjustmentTools.DirectValue)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkKPIName, 1, AdjustmentTools.TopFrontArm.ToString());
            //}
            //else
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkKPIName, 1, _cv.kpiAdjustmentTool.ToString());

            //}
            //if (_cls.Final_KPIAdjusterLength.Count != 0)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkKPIDelta, 1, Convert.ToString(Math.Round(_cls.Final_KPIAdjusterLength[_cls.Final_KPIAdjusterLength.Count - 1], 2)));
            //}
            //else
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkKPIDelta, 1, null);

            //}
            _resultsGrid.SetCellValue(_resultsGUI.rowKPIConvergance, 1, _setupOP.KPI_Conv.ConvergenceStatus);



            ///<summary>Assingint the Caster Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowCasterAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.Caster).Degrees, 2)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_Caster.Degrees, 2)));
            //if (_cv.casterAdjustmentTool == AdjustmentTools.DirectValue)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkCasterName, 1, AdjustmentTools.BottomFrontArm.ToString());
            //}
            //else
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkCasterName, 1, _cv.casterAdjustmentTool.ToString());
            //}
            //if (_cls.Final_CasterAdjusterLength.Count != 0)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkCasterDelta, 1, Convert.ToString(Math.Round(_cls.Final_CasterAdjusterLength[_cls.Final_CasterAdjusterLength.Count - 1], 2)));
            //}
            //else
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkCasterDelta, 1, null);
            //}
            _resultsGrid.SetCellValue(_resultsGUI.rowCasterConvergance, 1, _setupOP.Caster_Conv.ConvergenceStatus);



            ///<summary>Assining the Camber Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowCamberAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.waOP.StaticCamber).Degrees, 2)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_Camber.Degrees, 2))
                /*_cls.Final_Camber[_cls.Final_Camber.Count - 1].Degrees*/);
            //if (_cv.camberAdjustmentTool == AdjustmentTools.DirectValue)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowMountName, 1, AdjustmentTools.TopCamberMount.ToString());
            //}
            //else
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowMountName, 1, _cv.camberAdjustmentTool);
            //}
            //if (_cv.camberShimThickness > 0)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowShimsCamber, 1, Convert.ToString(Math.Round(_cls.Final_CamberAdjusterLength[_cls.Final_CamberAdjusterLength.Count - 1] / _cv.camberShimThickness, 2))/* + " | " + Convert.ToString(0)*/
            //                                                                                                                                                                                                  /*_cls.Final_CamberAdjusterLength[_cls.Final_CamberAdjusterLength.Count - 1] / _cv.camberShimThickness*/);
            //    _resultsGrid.SetCellValue(_resultsGUI.rowShimThickness, 1, _cv.camberShimThickness);
            //}
            //else
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowShimsCamber, 1, _cls.Final_CamberAdjusterLength[_cls.Final_CamberAdjusterLength.Count - 1]);
            //    _resultsGrid.SetCellValue(_resultsGUI.rowShimThickness, 1, 1);
            //}
            //_resultsGrid.SetCellValue(_resultsGUI.rowShimThickness, 1, _cv.camberShimThickness);
            _resultsGrid.SetCellValue(_resultsGUI.rowCamberConvergance, 1, _setupOP.Camber_Conv.ConvergenceStatus);



            ///<summary>Assigning the Toe Outputs</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowToeAngle, 1, Convert.ToString(Math.Round(Angle.FromRadians(_oc.waOP.StaticToe).Degrees, 2)) + " | " + Convert.ToString(Math.Round(_setupOP.Calc_Toe.Degrees, 2))
                /*_cls.Final_Toe[_cls.Final_Toe.Count - 1].Degrees*/);
            //if (_cls.Final_ToeAdjusterLength.Count != 1)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkToeDelta, 1, _cls.Final_ToeAdjusterLength[_cls.Final_ToeAdjusterLength.Count - 1] /*- _cls.Final_ToeAdjusterLength[0]*/);
            //}
            //else if (_cls.Final_ToeAdjusterLength.Count == 1)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkToeDelta, 1, _cls.Final_ToeAdjusterLength[_cls.Final_ToeAdjusterLength.Count - 1] - _cls.Final_ToeAdjusterLength[0]);
            //}
            _resultsGrid.SetCellValue(_resultsGUI.rowToeConvergance, 1, _setupOP.Toe_Conv.ConvergenceStatus);



            ///<summary>Assigning the Ride Height</summary>
            _resultsGrid.SetCellValue(_resultsGUI.rowRideHeight, 1, _setupOP.Calc_RideHeight);
            _resultsGrid.SetCellValue(_resultsGUI.rowLinkRHName, 1, _cv.rideheightAdjustmentTool.ToString());
            //if (_cls.Final_RideHeight.Count > 1)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkRHDelta, 1, _cls.Final_Pushrod[_cls.Final_Pushrod.Count - 1]/* - _cls.Final_Pushrod[0]*/);
            //}
            //else if (_cls.Final_RideHeight.Count == 1)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowLinkRHDelta, 1, _cls.Final_Pushrod[_cls.Final_Pushrod.Count - 1] - _cls.Final_Pushrod[0]);

            //}
            _resultsGrid.SetCellValue(_resultsGUI.rowRHConvergance, 1, _setupOP.RideHeight_Conv.ConvergenceStatus);



            #region ---NOT NEEDED---LINK LENGTHS
            /////<summary>Assigning the Link Lengths </summary>
            //if (/*_cls.Final_TopFrontArm.Count > 1*/ _cv.LinkLengthChanged)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowTopFront, 1, Convert.ToString(Math.Round(_cls.Final_TopFrontArm[0], 2)) + " | " + Convert.ToString(Math.Round(_cls.Final_TopFrontArm[_cls.Final_TopFrontArm.Count - 1] + _cls.Final_TopFrontArm[0], 2))
            //        /*_cls.Final_TopFrontArm[_cls.Final_TopFrontArm.Count - 1] + _cls.Final_TopFrontArm[0]*/);
            //}
            //else if (/*_cls.Final_TopFrontArm.Count == 1*/ !_cv.LinkLengthChanged)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowTopFront, 1, _cls.Final_TopFrontArm[_cls.Final_TopFrontArm.Count - 1]);
            //}


            //if (/*_cls.Final_TopRearArm.Count > 1*/_cv.LinkLengthChanged)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowTopRear, 1, Convert.ToString(Math.Round(_cls.Final_TopRearArm[0], 2)) + " | " + Convert.ToString(Math.Round(_cls.Final_TopRearArm[_cls.Final_TopRearArm.Count - 1] + _cls.Final_TopRearArm[0], 2))
            //        /*_cls.Final_TopRearArm[_cls.Final_TopRearArm.Count - 1] + _cls.Final_TopRearArm[0]*/);
            //}
            //else /*if (_cls.Final_TopRearArm.Count == 1)*/
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowTopRear, 1, _cls.Final_TopRearArm[_cls.Final_TopRearArm.Count - 1]);

            //}


            //if (/*_cls.Final_BottomFrontArm.Count > 1*/ _cv.LinkLengthChanged)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowBottomFront, 1, Convert.ToString(Math.Round(_cls.Final_BottomFrontArm[0], 2)) + " | " + Convert.ToString(Math.Round(_cls.Final_BottomFrontArm[_cls.Final_BottomFrontArm.Count - 1] + _cls.Final_BottomFrontArm[0], 2))
            //        /*_cls.Final_BottomFrontArm[_cls.Final_BottomFrontArm.Count - 1] + _cls.Final_BottomFrontArm[0]*/);
            //}
            //else /*if (_cls.Final_BottomFrontArm.Count == 1)*/
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowBottomFront, 1, _cls.Final_BottomFrontArm[_cls.Final_BottomFrontArm.Count - 1]);
            //}


            //if (/*_cls.Final_BottomRearArm.Count > 1*/ _cv.LinkLengthChanged)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowBottomRear, 1, Convert.ToString(Math.Round(_cls.Final_BottomRearArm[0], 2)) + " | " + Convert.ToString(Math.Round(_cls.Final_BottomRearArm[_cls.Final_BottomRearArm.Count - 1] + _cls.Final_BottomRearArm[0], 2))
            //        /*_cls.Final_BottomRearArm[_cls.Final_BottomRearArm.Count - 1] + _cls.Final_BottomRearArm[0]*/);
            //}
            //else /*if (_cls.Final_BottomRearArm.Count == 1)*/
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowBottomRear, 1, _cls.Final_BottomRearArm[_cls.Final_BottomRearArm.Count - 1]);
            //}


            //if (/*_cls.Final_Pushrod.Count > 1*/ _cv.RideHeightChanged)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowPushrod, 1, Convert.ToString(Math.Round(_cls.Final_Pushrod[0], 2)) + " | " + Convert.ToString(Math.Round(_cls.Final_Pushrod[_cls.Final_Pushrod.Count - 1] + _cls.Final_Pushrod[0], 2))
            //        /*_cls.Final_Pushrod[_cls.Final_Pushrod.Count - 1] + _cls.Final_Pushrod[0]*/);
            //}
            //else /*if (_cls.Final_Pushrod.Count == 1)*/
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowPushrod, 1, _cls.Final_Pushrod[_cls.Final_Pushrod.Count - 1]);
            //}


            //if (_cls.Final_ToeAdjusterLength.Count > 1)
            //{
            //    ///<remarks><see cref="SetupChange_ClosedLoopSolver.InitializeLists"/> for Explanataion</remarks>
            //    _resultsGrid.SetCellValue(_resultsGUI.rowToeLink, 1, Convert.ToString(Math.Round(_cls.Final_ToeAdjusterLength[0], 2)) + " | " + Convert.ToString(Math.Round(_cls.Final_ToeAdjusterLength[_cls.Final_ToeAdjusterLength.Count - 1] + _cls.Final_ToeAdjusterLength[0], 2))
            //        /*_cls.Final_ToeAdjusterLength[_cls.Final_ToeAdjusterLength.Count - 1] + _cls.Final_ToeAdjusterLength[0]*/);
            //}
            //else if (_cls.Final_ToeAdjusterLength.Count == 1)
            //{
            //    _resultsGrid.SetCellValue(_resultsGUI.rowToeLink, 1, _cls.Final_ToeAdjusterLength[_cls.Final_ToeAdjusterLength.Count - 1]);
            //}
            //_resultsGrid.SetCellValue(_resultsGUI.rowLinkConvergance, 1, _cls.LinkLengthConvergence.ToString()); 
            #endregion



        }


        public void PlotBumpSteerGraph(SetupChange_Outputs _setupOP, SetupChange_CornerVariables _cv, XUC_SetupChangeResults _resultsGUI)
        {
            if (/*_cv.constBumpSteer || */_cv.BumpSteerChangeRequested)
            {
                ///<summary>If the Bump Steer Change is requested then setting the Enaled status to true so that the user can scroll and zoom the Bump Steer Contro </summary>
                _resultsGUI.bumpSteerCurve1.Enabled = true;

                ///<summary>Setting the <see cref="BumpSteerCurve.IsOutputChart"/> value to true to teach the CHart that the Output is calling it. 
                ///---IMPORTANT--- This is an important step so that the accidentally clicking the control doesn;t create a series point
                /// </summary>
                _resultsGUI.bumpSteerCurve1.IsOutputChart = true;

                for (int i = 0; i < _setupOP.Calc_BumpSteerChart.Count; i++)
                {
                    _resultsGUI.bumpSteerCurve1.AddPointToChart(_resultsGUI.bumpSteerCurve1.chartControl1, _cv.BS_Params.WheelDeflections[_cv.BS_Params.HighestBumpindex + i], _setupOP.Calc_BumpSteerChart[i].Degrees, 0, true);
                }

                if (_cv.BumpSteerChangeRequested || _cv.constBumpSteer)
                {
                    _resultsGUI.bumpSteerCurve1.AddSeriesToChart(_resultsGUI.bumpSteerCurve1.chartControl1);
                }
                for (int i = 0; i < _setupOP.Req_BumpSteerChart.Count; i++)
                {
                    _resultsGUI.bumpSteerCurve1.AddPointToChart(_resultsGUI.bumpSteerCurve1.chartControl1, _cv.BS_Params.WheelDeflections[_cv.BS_Params.HighestBumpindex + i], _setupOP.Req_BumpSteerChart[i].Degrees, 1, true);
                }

                _resultsGUI.bumpSteerCurve1.Enabled = true;
            }

        }











    }
}

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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
namespace Coding_Attempt_with_GUI
{
    public partial class XUC_SetupChange_Children : XtraUserControl
    {

        SetupChange_GUI setupChangeGUI;

        SetupChange_CornerVariables setupChangeCornerVariables;

        int Identifier;

        Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();

        XUC_SetupChange parentForm;

        public XUC_SetupChange_Children()
        {
            InitializeComponent();
            vGridControl1.ValidatingEditor += VGridControl1_ValidatingEditor;
            vGridControl1.BindingContext = new BindingContext();
            vGridControl1.DataSource = null;
        }

        #region Cell Validator Events
        /// <summary>
        /// Method to check if the focused row of the Vertical Grid is the row of number of Shims 
        /// </summary>
        /// <returns></returns>
        private bool CheckIfShimsRow()
        {
            if (vGridControl1.FocusedRow == rowNoOfShims)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Method to check if the focused row of the Vertical Grid is the row of Shim Thickness
        /// </summary>
        /// <returns></returns>
        private bool CheckIfShimThicknessRow()
        {
            if (vGridControl1.FocusedRow == rowShimThickness)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Method to check if the Row to select the CamberMount to be used is focused.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfCamberMountSelecter()
        {
            if (vGridControl1.FocusedRow == rowCamberMount)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckIfKPIAdjusterSelecter()
        {
            if (vGridControl1.FocusedRow == rowKPIAdjusterSelect)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckIfCasterAdjusterSelecter()
        {
            if (vGridControl1.FocusedRow == rowCasterAdjusterSelect)
            {
                return true;
            }
            else return false;
        }

        private bool CheckIfCamberChangeMethod()
        {
            if (vGridControl1.FocusedRow == rowCamberChangeMethod)
            {
                return true;
            }
            else return false;
        }

        private bool CheckIfRideHeightChangeMethod()
        {
            if (vGridControl1.FocusedRow == rowRideHeightChangeMethod)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Cell Value Validator of the <see cref="vGridControl1"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VGridControl1_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (!Double.TryParse(e.Value as string, out double checker))
            {
                if (!CheckIfCamberMountSelecter() && !CheckIfCasterAdjusterSelecter() && !CheckIfKPIAdjusterSelecter() && !CheckIfCamberChangeMethod() && !CheckIfRideHeightChangeMethod())
                {
                    e.Valid = false;
                    e.ErrorText = "Please enter numeric values";
                }
            }
            else if (CheckIfShimsRow())
            {
                if (!Int32.TryParse(e.Value as string, out int check))
                {
                    e.Valid = false;
                    e.ErrorText = "Please enter number of shims as a whole number ";
                }
            }

            else if (CheckIfShimThicknessRow())
            {
                if (Convert.ToDouble(e.Value) < 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Please enter thickness value greater than 0";
                }
            }
        }
        #endregion

        /// <summary>
        /// Method to Get the Object Data of this child class's <see cref="Parent"/>
        /// </summary>
        /// <param name="_setupChangeGUI">Object of the <see cref="SetupChange_GUI"/> Class</param>
        /// <param name="_setupChangeCornerVariables">Object of the <see cref="SetupChange_CornerVariables"/> Class</param>
        /// <param name="_identifier">Identifier</param>
        public void GetGrandParentObjectData(SetupChange_GUI _setupChangeGUI, SetupChange_CornerVariables _setupChangeCornerVariables, int _identifier, string _cornerName, int indexSetupChangeGUI)
        {
            setupChangeGUI = _setupChangeGUI;
            

            this.setupChangeCornerVariables = _setupChangeCornerVariables;
            this.setupChangeCornerVariables.cornerName = _cornerName;

            setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged = new List<int>();

            setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.AddRange(new int[] { 1, 2, 3, 4 });

            this.setupChangeCornerVariables.InitAdjustmentToolsDictionary(AdjustmentTools.TopCamberMount, AdjustmentTools.ToeLinkLength, AdjustmentTools.TopFrontArm, AdjustmentTools.TopFrontArm, AdjustmentTools.PushrodLength);
            Identifier = _identifier;

            parentForm = XUC_SetupChange.AssignFormVariable();

        }

        bool casterDisabledDuetoThreeLL = false;
        bool kpiDisabledDueToThreeLL = false;

        #region Checkbox Changes Events
        /// <summary>
        /// Checked Listbox Event which is fired when the item is checked or unchecked in the Listbox of SetupChanges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            ///<summary>
            ///KPI Checkbox Constraint Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Checked)
            {
                //vGridControl1.Rows["rowKPI"].Visible = true;
                rowKPIAngle.Enabled = true;
                rowKPIAdjusterSelect.Enabled = true;
                checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["KPI constant"].Enabled = false;

                if (setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Count == 1)
                {
                    checkedListBoxControlChanges.Items["Caster Change"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlChanges.Items["Caster Change"].Enabled = false;
                    checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlConstraints.Items["Caster constant"].Enabled = false;
                    casterDisabledDuetoThreeLL = true;
                    return;
                }

            }
            else if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Unchecked)
            {
                //rowKPIAngle.Visible = false;
                rowKPIAngle.Enabled = false;
                rowKPIAdjusterSelect.Enabled = false;
                vGridControl1.SetCellValue(rowKPIAngle, 1, null);
                rowKPIAngle.Properties.Value = null;
                vGridControl1.SetCellValue(rowKPIAdjusterSelect, 1, null);
                rowKPIAdjusterSelect.Properties.Value = null;
                checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["KPI constant"].Enabled = true;


                if (casterDisabledDuetoThreeLL == true)
                {
                    checkedListBoxControlChanges.Items["Caster Change"].Enabled = true;
                    checkedListBoxControlConstraints.Items["Caster constant"].Enabled = true;
                    casterDisabledDuetoThreeLL = false;
                }

            }



            ///<summary>
            ///Camber Checkbox Constraint Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Checked)
            {
                rowCamberChangeMethod.Enabled = true;
                rowShimThickness.Enabled = true;
                checkedListBoxControlConstraints.Items["Camber constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Camber constant"].Enabled = false;

            }
            else if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Unchecked)
            {
                rowCamberChangeMethod.Enabled = false;
                vGridControl1.SetCellValue(rowCamberChangeMethod, 1, null);
                rowCamberChangeMethod.Properties.Value = null;
                rowCamberAngle.Enabled = false;
                vGridControl1.SetCellValue(rowCamberAngle, 1, null);
                rowCamberAngle.Properties.Value = null;
                rowCamberMount.Enabled = false;
                vGridControl1.SetCellValue(rowCamberMount, 1, null);
                rowCamberMount.Properties.Value = null;
                rowNoOfShims.Enabled = false;
                vGridControl1.SetCellValue(rowNoOfShims, 1, null);
                rowNoOfShims.Properties.Value = null;
                if (checkedListBoxControlConstraints.Items["Camber constant"].CheckState == CheckState.Unchecked)
                {
                    rowShimThickness.Enabled = false;
                    vGridControl1.SetCellValue(rowShimThickness, 1, null);
                    rowShimThickness.Properties.Value = null;
                }

                checkedListBoxControlConstraints.Items["Camber constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Camber constant"].Enabled = true;
            }

            

            ///<summary>
            ///Caster Checkbox Constraint Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Checked)
            {
                //vGridControl1.Rows["rowCaster"].Visible = true;
                rowCasterAngle.Enabled = true;
                rowCasterAdjusterSelect.Enabled = true;
                checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Caster constant"].Enabled = false;

                if (setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Count == 1)
                {
                    checkedListBoxControlChanges.Items["KPI Change"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlChanges.Items["KPI Change"].Enabled = false;
                    checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlConstraints.Items["KPI constant"].Enabled = false;
                    kpiDisabledDueToThreeLL = true;
                    return;
                }
            }
            else if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Unchecked)
            {
                //vGridControl1.Rows["rowCaster"].Visible = false;
                rowCasterAngle.Enabled = false;
                vGridControl1.SetCellValue(rowCasterAngle, 1, null);
                rowCasterAngle.Properties.Value = null;
                rowCasterAdjusterSelect.Enabled = false;
                vGridControl1.SetCellValue(rowCasterAdjusterSelect, 1, null);
                rowCasterAdjusterSelect.Properties.Value = null;
                checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Caster constant"].Enabled = true;

                if (kpiDisabledDueToThreeLL == true)
                {
                    checkedListBoxControlChanges.Items["KPI Change"].Enabled = true;
                    checkedListBoxControlConstraints.Items["KPI constant"].Enabled = true;
                    kpiDisabledDueToThreeLL = false;
                    return;
                }
            }

            if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Checked)
            {
                //vGridControl1.Rows["rowToe"].Visible = true;
                rowToeAngle.Enabled = true;
                checkedListBoxControlConstraints.Items["Toe constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Toe constant"].Enabled = false;
            }
            else if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Unchecked)
            {
                //vGridControl1.Rows["rowToe"].Visible = false;
                rowToeAngle.Enabled = false;
                vGridControl1.SetCellValue(rowToeAngle, 1, null);
                rowToeAngle.Properties.Value = null;
                checkedListBoxControlConstraints.Items["Toe constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Toe constant"].Enabled = true;
            }

            if (checkedListBoxControlChanges.Items["Ride Height Change"].CheckState == CheckState.Checked)
            {
                rowRideHeightChangeMethod.Enabled = true;
                //rowRideHeight.Enabled = true;
                //rowDamperEyeToPerch.Enabled = false;
                checkedListBoxControlConstraints.Items["Ride Height constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Ride Height constant"].Enabled = false;
            }
            else if (checkedListBoxControlChanges.Items["Ride Height Change"].CheckState == CheckState.Unchecked)
            {
                rowRideHeightChangeMethod.Enabled = false;
                vGridControl1.SetCellValue(rowRideHeightChangeMethod, 1, null);
                rowRideHeightChangeMethod.Properties.Value = null;
                rowRideHeight.Enabled = false;
                vGridControl1.SetCellValue(rowRideHeight, 1, null);
                rowRideHeight.Properties.Value = null;
                rowDamperEyeToPerch.Enabled = false;
                vGridControl1.SetCellValue(rowDamperEyeToPerch, 1, null);
                rowDamperEyeToPerch.Properties.Value = null;
                checkedListBoxControlConstraints.Items["Ride Height constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Ride Height constant"].Enabled = true;
            }

            if (checkedListBoxControlChanges.Items["Link Length Change"].CheckState == CheckState.Checked)
            {
                rowTopFront.Enabled = true;
                rowTopRear.Enabled = true;
                rowBottomFron.Enabled = true;
                rowBottomRear.Enabled = true;
                rowPushrod.Enabled = true;
                rowToeLinkLength.Enabled = true;
            }
            else if (checkedListBoxControlChanges.Items["Link Length Change"].CheckState == CheckState.Unchecked)
            {
                rowTopFront.Enabled = false;
                vGridControl1.SetCellValue(rowTopFront, 1, null);
                rowTopFront.Properties.Value = null;
                rowTopRear.Enabled = false;
                vGridControl1.SetCellValue(rowTopRear, 1, null);
                rowTopRear.Properties.Value = null;
                rowBottomFron.Enabled = false;
                vGridControl1.SetCellValue(rowBottomFron, 1, null);
                rowBottomFron.Properties.Value = null;
                rowBottomRear.Enabled = false;
                vGridControl1.SetCellValue(rowBottomRear, 1, null);
                rowBottomRear.Properties.Value = null;
                rowPushrod.Enabled = false;
                vGridControl1.SetCellValue(rowPushrod, 1, null);
                rowPushrod.Properties.Value = null;
                rowToeLinkLength.Enabled = false;
                vGridControl1.SetCellValue(rowToeLinkLength, 1, null);
                rowToeLinkLength.Properties.Value = null;

            }

            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        #endregion

        #region Checkbox Constant Events
        /// <summary>
        /// Checked Listbox Event which is fired when the item is checked or unchecked in the Listbox of Constraints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxControlConstraints_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (checkedListBoxControlConstraints.Items["KPI constant"].CheckState == CheckState.Checked)
            {
                setupChangeCornerVariables.constKPI = true;

                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }
            else
            {
                setupChangeCornerVariables.constKPI = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }

            if (checkedListBoxControlConstraints.Items["Camber constant"].CheckState == CheckState.Checked)
            {
                setupChangeCornerVariables.constCamber = true;
                rowShimThickness.Enabled = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }
            else
            {
                if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Unchecked)
                {
                    rowShimThickness.Enabled = false;
                }
                setupChangeCornerVariables.constCamber = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }


            if (checkedListBoxControlConstraints.Items["Caster constant"].CheckState == CheckState.Checked)
            {
                setupChangeCornerVariables.constCaster = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }
            else
            {
                setupChangeCornerVariables.constCaster = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }


            if (checkedListBoxControlConstraints.Items["Toe constant"].CheckState == CheckState.Checked)
            {
                setupChangeCornerVariables.constToe = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }
            else
            {
                setupChangeCornerVariables.constToe = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }


            if (checkedListBoxControlConstraints.Items["Ride Height constant"].CheckState == CheckState.Checked)
            {
                setupChangeCornerVariables.constRideHeight = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }
            else
            {
                setupChangeCornerVariables.constRideHeight = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            }

            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();

        } 
        #endregion


        #region Text Changed events of all the RepositoryItem Textboxes
        private void r1TextboxKPI_Leave(object sender, EventArgs e)
        {
            //vGridControl1.UpdateFocusedRecord();
            //setupChangeCornerVariables.deltaKPI = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow,vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.kpiAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.kpiAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["KPIChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();

        }

        /// <summary>
        /// Method to assign the change in KPI requested by the User and assign the <see cref="AdjustmentTools"/> and the <see cref="AdjustmentType"/>
        /// </summary>
        /// <param name="_kpiValue">change in KPI value requested. </param>
        /// <param name="_adjType">Adjustment Type</param>
        /// <param name="_adjTool">Adjust Tool which the user has selected. That is, Direct Angle </param>
        private void KPIChangeRequested(double _kpiValue, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChangeCornerVariables.deltaKPI = _kpiValue;
            setupChangeCornerVariables.kpiAdjustmentType = _adjType;
            setupChangeCornerVariables.kpiAdjustmentTool = _adjTool;
            setupChangeCornerVariables.AdjToolsDictionary["KPIChange"] = _adjTool;

            if (setupChangeCornerVariables.deltaKPI != 0)
            {
                setupChangeCornerVariables.KPIChangeRequested = true;
            }
            else
            {
                setupChangeCornerVariables.KPIChangeRequested = false;
            }
        }
        
        private void r1TextboxCamber_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaCamber = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.camberAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.camberAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["CamberChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void CamberChangeRequested(double _camberValue, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChangeCornerVariables.deltaCamber = _camberValue;
            setupChangeCornerVariables.camberAdjustmentType = _adjType;
            setupChangeCornerVariables.camberAdjustmentTool = _adjTool;
            setupChangeCornerVariables.AdjToolsDictionary["CamberChange"] = _adjTool;

            if (setupChangeCornerVariables.deltaCamber != 0)
            {
                setupChangeCornerVariables.CamberChangeRequested = true;
            }
            else
            {
                setupChangeCornerVariables.CamberChangeRequested = false;
            }
        }
        private void CamberShimCountChanged(int _noOfShims, double _shimThickness, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChangeCornerVariables.deltaCamberShims = _noOfShims;
            setupChangeCornerVariables.camberShimThickness = _shimThickness;
            setupChangeCornerVariables.camberAdjustmentType = _adjType;
            setupChangeCornerVariables.camberAdjustmentTool = _adjTool;
            setupChangeCornerVariables.AdjToolsDictionary["CamberChange"] = _adjTool;

            if (setupChangeCornerVariables.deltaCamberShims != 0)
            {
                setupChangeCornerVariables.CamberChangeRequested = true;
            }
            else
            {
                setupChangeCornerVariables.CamberChangeRequested = false;
            }

        }





        private void r1TextboxCaster_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaCaster = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.casterAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.casterAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["CasterChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        private void CasterChangeRequested(double _casterChange, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChangeCornerVariables.deltaCaster = _casterChange;
            setupChangeCornerVariables.casterAdjustmentType = _adjType;
            setupChangeCornerVariables.casterAdjustmentTool = _adjTool;
            setupChangeCornerVariables.AdjToolsDictionary["CasterChange"] = _adjTool;

            if (setupChangeCornerVariables.deltaCaster != 0)
            {
                setupChangeCornerVariables.CasterChangeRequested = true;
            }
            else
            {
                setupChangeCornerVariables.CasterChangeRequested = false;
            }

        }





        private void r1TextboxToe_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaToe = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.toeAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.toeAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["ToeChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        private void ToeChangeRequested(double _toeChange, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChangeCornerVariables.deltaToe = _toeChange;
            setupChangeCornerVariables.toeAdjustmentType = _adjType;
            setupChangeCornerVariables.toeAdjustmentTool = _adjTool;
            setupChangeCornerVariables.AdjToolsDictionary["ToeChange"] = _adjTool;


            if (setupChangeCornerVariables.deltaToe != 0)
            {
                setupChangeCornerVariables.ToeChangeRequested = true;
            }
            else
            {
                setupChangeCornerVariables.ToeChangeRequested = false;
            }

        }






        private void r1TextboxRideHeight_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaRideHeight = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.rideheightAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.rideheightAdjustmentTool = AdjustmentTools.PushrodLength;
            //setupChangeCornerVariables.AdjToolsDictionary["RideHeightChange"] = AdjustmentTools.PushrodLength;
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        private void RideHeightChangeRequested(double _rideHeightChange, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChangeCornerVariables.deltaRideHeight = _rideHeightChange;
            setupChangeCornerVariables.rideheightAdjustmentType = _adjType;
            setupChangeCornerVariables.rideheightAdjustmentTool = _adjTool;
            setupChangeCornerVariables.AdjToolsDictionary["RideHeightChange"] = _adjTool;
            setupChangeCornerVariables.RideHeightChanged = true;

            if (setupChangeCornerVariables.deltaRideHeight != 0)
            {
                setupChangeCornerVariables.RHIChangeRequested = true;
            }
            else
            {
                setupChangeCornerVariables.RHIChangeRequested = false;
            }


        }




        private void r1TextboxShims_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaCamberShims = Convert.ToInt32(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.camberAdjustmentType = AdjustmentType.Indirect;
            //setupChangeCornerVariables.camberAdjustmentTool = AdjustmentTools.TopCamberMount;
            //setupChangeCornerVariables.AdjToolsDictionary["CamberChange"] = AdjustmentTools.TopCamberMount;
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void r1TextboxShimThickness_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.camberShimThickness = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void LinkLengthChanged()
        {
            double ToeLinkLength = Convert.ToDouble(vGridControl1.GetCellValue(rowToeLinkLength, 1));
            double PushrodLength = Convert.ToDouble(vGridControl1.GetCellValue(rowPushrod, 1));
            double TopFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopFront, 1));
            double TopRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopRear, 1));
            double BottomFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomFron, 1));
            double BottomRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomRear, 1));

            //WillCauseEndlessLoop = true;
            setupChangeCornerVariables.LinkLengthChanged = true;
            if (TopFrontArm != 0)
            {
                setupChangeCornerVariables.deltaTopFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopFront, 1));
                setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Remove(1);
                RemoveFromRICombobox("Top Front Arm", rIComboBoxWishboneSelecter);
                //WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowKPIAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowCasterAdjusterSelect, 1, null);
                rowKPIAdjusterSelect.Properties.Value = null;
                rowCasterAdjusterSelect.Properties.Value = null;
                
            }
            else
            {
                setupChangeCornerVariables.deltaTopFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopFront, 1));
                if (!setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Contains(1))
                {
                    setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Add(1); 
                }
                AddToRIComboBox("Top Front Arm", rIComboBoxWishboneSelecter);
            }


            if (TopRearArm != 0)
            {
                setupChangeCornerVariables.deltaTopRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopRear, 1));
                setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Remove(2);
                RemoveFromRICombobox("Top Rear Arm", rIComboBoxWishboneSelecter); /*WillCauseEndlessLoop = true;*/
                //vGridControl1.SetCellValue(rowKPIAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowCasterAdjusterSelect, 1, null); WillCauseEndlessLoop = true;

            }
            else
            {
                setupChangeCornerVariables.deltaTopRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopRear, 1));
                if (!setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Contains(2))
                {
                    setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Add(2);
                }
                AddToRIComboBox("Top Rear Arm", rIComboBoxWishboneSelecter);
            }


            if (BottomFrontArm != 0)
            {
                setupChangeCornerVariables.deltaBottmFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomFron, 1));
                setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Remove(3);
                RemoveFromRICombobox("Bottom Front Arm", rIComboBoxWishboneSelecter); /*WillCauseEndlessLoop = true*/;
                //vGridControl1.SetCellValue(rowKPIAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowCasterAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                rowKPIAdjusterSelect.Properties.Value = null;
                rowCasterAdjusterSelect.Properties.Value = null;
            }
            else
            {
                setupChangeCornerVariables.deltaBottmFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomFron, 1));
                if (!setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Contains(3))
                {
                    setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Add(3);
                }
                AddToRIComboBox("Bottom Front Arm", rIComboBoxWishboneSelecter);
            }


            if (BottomRearArm != 0)
            {
                setupChangeCornerVariables.deltaBottomRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomRear, 1));
                setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Remove(4);
                RemoveFromRICombobox("Bottom Rear Arm", rIComboBoxWishboneSelecter); /*WillCauseEndlessLoop = true;*/
                //vGridControl1.SetCellValue(rowKPIAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowCasterAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                rowKPIAdjusterSelect.Properties.Value = null;
                rowCasterAdjusterSelect.Properties.Value = null;
            }
            else
            {
                setupChangeCornerVariables.deltaBottomRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomRear, 1));
                if (!setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Contains(4))
                {
                    setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Add(4);
                }
                AddToRIComboBox("Bottom Rear Arm", rIComboBoxWishboneSelecter);
            }


            if (PushrodLength != 0)
            {
                setupChangeCornerVariables.deltaPushrod = Convert.ToDouble(vGridControl1.GetCellValue(rowPushrod, 1));
                //vGridControl1.SetCellValue(rowRideHeight, 1, null);
                rowRideHeight.Properties.Value = null;
                rowRideHeight.Enabled = false;
                //vGridControl1.SetCellValue(rowDamperEyeToPerch, 1, null);
                rowDamperEyeToPerch.Properties.Value = null;
                rowDamperEyeToPerch.Enabled = false;
                //vGridControl1.SetCellValue(rowRideHeightChangeMethod, 1, null);
                rowRideHeightChangeMethod.Properties.Value = null;
                rowRideHeightChangeMethod.Enabled = false;
                setupChangeCornerVariables.RideHeightChanged = true;
                setupChangeCornerVariables.rideheightAdjustmentType = AdjustmentType.Indirect;
                setupChangeCornerVariables.rideheightAdjustmentTool = AdjustmentTools.PushrodLength;
            }
            else
            {
                setupChangeCornerVariables.deltaPushrod = Convert.ToDouble(vGridControl1.GetCellValue(rowPushrod, 1));
                if (checkedListBoxControlChanges.Items["Ride Height Change"].CheckState == CheckState.Checked) 
                {
                    setupChangeCornerVariables.RideHeightChanged = false;
                    //rowRideHeight.Enabled = true;
                    rowRideHeightChangeMethod.Enabled = true;
                    //rowDamperEyeToPerch.Enabled = true; 

                }
            }


            if (ToeLinkLength != 0)
            {
                setupChangeCornerVariables.deltaToeLinkLength = Convert.ToDouble(vGridControl1.GetCellValue(rowToeLinkLength, 1));
                setupChangeCornerVariables.toeAdjustmentType = AdjustmentType.Indirect;
                setupChangeCornerVariables.toeAdjustmentTool = AdjustmentTools.ToeLinkLength;
                //vGridControl1.SetCellValue(rowToeAngle, 1, null);
                rowToeAngle.Properties.Value = null;
                setupChangeCornerVariables.ToeChangeRequested = true;
                rowToeAngle.Enabled = false;
                checkedListBoxControlChanges.Items["Toe Change"].CheckState = CheckState.Unchecked;
                checkedListBoxControlChanges.Items["Toe Change"].Enabled = false;
                checkedListBoxControlConstraints.Items["Toe constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Toe constant"].Enabled = false;
            }
            else
            {
                setupChangeCornerVariables.deltaToeLinkLength = Convert.ToDouble(vGridControl1.GetCellValue(rowToeLinkLength, 1));
                checkedListBoxControlChanges.Items["Toe Change"].Enabled = true;
                checkedListBoxControlConstraints.Items["Toe constant"].Enabled = true;
                setupChangeCornerVariables.ToeChangeRequested = false;
                if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Checked)
                {
                    rowToeAngle.Enabled = true; 
                }
            }

            if (setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Count == 0)
            {
                //WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowKPIAngle, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowKPIAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowCasterAngle, 1, null); WillCauseEndlessLoop = true;
                //vGridControl1.SetCellValue(rowCasterAdjusterSelect, 1, null); WillCauseEndlessLoop = true;
                rowKPIAngle.Properties.Value = null;
                rowKPIAdjusterSelect.Properties.Value = null;
                rowCasterAngle.Properties.Value = null;
                rowCasterAdjusterSelect.Properties.Value = null;

                checkedListBoxControlChanges.Items["KPI Change"].CheckState = CheckState.Unchecked; /*WillCauseEndlessLoop = true;*/
                checkedListBoxControlChanges.Items["Caster Change"].CheckState = CheckState.Unchecked; /*WillCauseEndlessLoop = true;*/
                checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked; /*WillCauseEndlessLoop = true;*/
                checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked; /*WillCauseEndlessLoop = true;*/

                checkedListBoxControlChanges.Items["KPI Change"].Enabled = false;
                checkedListBoxControlChanges.Items["Caster Change"].Enabled = false;
                checkedListBoxControlConstraints.Items["KPI constant"].Enabled = false;
                checkedListBoxControlConstraints.Items["Caster constant"].Enabled = false;


            }
            else
            {
                checkedListBoxControlChanges.Items["KPI Change"].Enabled = true;
                checkedListBoxControlChanges.Items["Caster Change"].Enabled = true;
                checkedListBoxControlConstraints.Items["KPI constant"].Enabled = true;
                checkedListBoxControlConstraints.Items["Caster constant"].Enabled = true;
            }

            if (setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Count == 3)
            {
                
            }

            setupChangeCornerVariables.LinkLengthChanged = true;




            if (TopFrontArm == 0 && TopRearArm == 0 && BottomFrontArm == 0 && BottomRearArm == 0 && (ToeLinkLength == 0) && (PushrodLength == 0)) 
            {
                setupChangeCornerVariables.LinkLengthChanged = false;
            }

            if (setupChangeCornerVariables.LinkLengthsWhichHaveNotChanged.Count == 1)
            {
                checkedListBoxControlChanges.Items["KPI Change"].CheckState = CheckState.Unchecked;
                checkedListBoxControlChanges.Items["Caster Change"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
            }


        }


        //bool WillCauseEndlessLoop = false;
        
        private void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            //if (WillCauseEndlessLoop == true)
            //{
            //    WillCauseEndlessLoop = false;
            //    return;
            //}

            if (vGridControl1.FocusedRow == rowKPIAngle)
            {
                KPIChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }

            
            else if (vGridControl1.FocusedRow == rowKPIAdjusterSelect)
            {
                if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Front Arm")
                {
                    setupChangeCornerVariables.kpiAdjustmentTool = AdjustmentTools.TopFrontArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForKPI = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Rear Arm")
                {
                    setupChangeCornerVariables.kpiAdjustmentTool = AdjustmentTools.TopRearArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForKPI = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Front Arm")
                {
                    setupChangeCornerVariables.kpiAdjustmentTool = AdjustmentTools.BottomFrontArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForKPI = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Rear Arm")
                {
                    setupChangeCornerVariables.kpiAdjustmentTool = AdjustmentTools.BottomRearArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForKPI = true;
                }
                else
                {
                    setupChangeCornerVariables.OverrideRandomSelectorForKPI = false;
                }
            }


            else if (vGridControl1.FocusedRow == rowCamberAngle)
            {
                CamberChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }


            else if (vGridControl1.FocusedRow == rowCamberMount)
            {
                if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow,vGridControl1.FocusedRecord)) == "Top Camber Mount")
                {
                    setupChangeCornerVariables.camberAdjustmentTool = AdjustmentTools.TopCamberMount;
                    
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Camber Mount")
                {
                    setupChangeCornerVariables.camberAdjustmentTool = AdjustmentTools.BottomCamberMount;
                }
            }


            else if (vGridControl1.FocusedRow == rowNoOfShims)
            {

                CamberShimCountChanged(Convert.ToInt32(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), Convert.ToDouble(vGridControl1.GetCellValue(rowShimThickness, vGridControl1.FocusedRecord)),
                                        AdjustmentType.Indirect, setupChangeCornerVariables.camberAdjustmentTool);
            }
            else if (vGridControl1.FocusedRow == rowShimThickness)
            {
                CamberShimCountChanged(Convert.ToInt32(vGridControl1.GetCellValue(rowNoOfShims, vGridControl1.FocusedRecord)), Convert.ToDouble(vGridControl1.GetCellValue(rowShimThickness, vGridControl1.FocusedRecord)),
                        AdjustmentType.Indirect, setupChangeCornerVariables.camberAdjustmentTool);
            }

            else if (vGridControl1.FocusedRow == rowToeAngle)
            {
                ToeChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }


            else if (vGridControl1.FocusedRow == rowCasterAngle)
            {
                CasterChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }


            else if (vGridControl1.FocusedRow == rowCasterAdjusterSelect)
            {
                if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Front Arm")
                {
                    setupChangeCornerVariables.casterAdjustmentTool = AdjustmentTools.TopFrontArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForCaster = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Rear Arm")
                {
                    setupChangeCornerVariables.casterAdjustmentTool = AdjustmentTools.TopRearArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForCaster = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Front Arm")
                {
                    setupChangeCornerVariables.casterAdjustmentTool = AdjustmentTools.BottomFrontArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForCaster = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Rear Arm")
                {
                    setupChangeCornerVariables.casterAdjustmentTool = AdjustmentTools.BottomRearArm;
                    setupChangeCornerVariables.OverrideRandomSelectorForCaster = true;
                }
                else
                {
                    setupChangeCornerVariables.OverrideRandomSelectorForCaster = false;
                }
            }


            else if (vGridControl1.FocusedRow == rowRideHeight)
            {
                RideHeightChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }


            else if (vGridControl1.FocusedRow == rowTopFront || vGridControl1.FocusedRow == rowTopRear || vGridControl1.FocusedRow == rowBottomFron || vGridControl1.FocusedRow == rowBottomRear || vGridControl1.FocusedRow == rowPushrod || vGridControl1.FocusedRow == rowToeLinkLength)
            {
                
                LinkLengthChanged();
            }
            

            setupChangeGUI.EditSetupChangeDeltas(setupChangeCornerVariables, Identifier);


            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();

        }
        #endregion

        #region Repository Wishbone Selecter Combobox Add/Remove Operations
        private void RemoveFromRICombobox(string _itemToBeRemoved, RepositoryItemComboBox _riCB)
        {
            if (_riCB.Items.Contains(_itemToBeRemoved))
            {
                _riCB.Items.Remove(_itemToBeRemoved);
            }
        }

        private void AddToRIComboBox(string _itemToBeAdded, RepositoryItemComboBox _riCB)
        {
            if (!_riCB.Items.Contains(_itemToBeAdded))
            {
                _riCB.Items.Add(_itemToBeAdded);
            }
        } 
        #endregion

        private void vGridControl1_Click(object sender, EventArgs e)
        {
        }

        private void vGridControl1_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
        }

        private void vGridControl1_CustomDrawRowHeaderCell(object sender, DevExpress.XtraVerticalGrid.Events.CustomDrawRowHeaderCellEventArgs e)
        {
        }

        private void rIComboBoxAdjustmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void rIComboBoxAdjustmentTypeCamber_Leave(object sender, EventArgs e)
        {
            string adjType = Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            if (vGridControl1.FocusedRow == rowCamberChangeMethod)
            {
                if (adjType == "Direct")
                {
                    rowCamberAngle.Enabled = true;
                    rowShims.Enabled = false;
                    rowCamberMount.Enabled = false;
                    rowNoOfShims.Enabled = false;
                    rowShimThickness.Enabled = false;
                    vGridControl1.SetCellValue(rowCamberMount, 1, null);
                    vGridControl1.SetCellValue(rowNoOfShims, 1, null);
                    vGridControl1.SetCellValue(rowShimThickness, 1, null);
                    rowCamberMount.Properties.Value = null;
                    rowNoOfShims.Properties.Value = null;
                    rowShimThickness.Properties.Value = null;
                }
                else if (adjType == "Indirect")
                {
                    rowShims.Enabled = true;
                    rowNoOfShims.Enabled = true;
                    rowShimThickness.Enabled = true;
                    rowCamberMount.Enabled = true;
                    rowCamberAngle.Enabled = false;
                    vGridControl1.SetCellValue(rowCamberAngle, 1, null);
                    rowCamberAngle.Properties.Value = null;
                }
            }


        }

        private void rIComboBoxAdjustmentTypeRideHeight_Leave(object sender, EventArgs e)
        {
            string adjType = Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));

            if (vGridControl1.FocusedRow == rowRideHeightChangeMethod)
            {
                if (adjType == "Direct")
                {
                    rowRideHeight.Enabled = true;
                    rowDamperEyeToPerch.Enabled = false;
                    vGridControl1.SetCellValue(rowDamperEyeToPerch, 1, null);
                    rowDamperEyeToPerch.Properties.Value = null;

                }
                else if (adjType == "Indirect")
                {
                    rowRideHeight.Enabled = false;
                    rowDamperEyeToPerch.Enabled = true;
                    vGridControl1.SetCellValue(rowRideHeight, 1, null);
                    rowRideHeight.Properties.Value = null;
                }
            }
        }






    }
}

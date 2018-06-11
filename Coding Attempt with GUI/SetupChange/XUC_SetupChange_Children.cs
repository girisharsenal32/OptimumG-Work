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
        /// <summary>
        /// Object of the <see cref="SetupChange_GUI"/> Class which is also the Parent Class of this cClass
        /// </summary>
        public SetupChange_GUI setupChangeGUI;

        /// <summary>
        /// Object of the <see cref="SetupChange_CornerVariables"/> (corresponding to the Corner to which object and THIS class belongs to)
        /// </summary>
        public SetupChange_CornerVariables setupChange_CV;

        public int Identifier;

        Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();

        /// <summary>
        /// Object of the <see cref="XUC_SetupChange"/> which contains 4 instance of THIS Class and is the Parent Form which displays everything to the user
        /// </summary>
        XUC_SetupChange parentForm;

        /// <summary>
        /// Represents the Bit Size of the genes of the Chromosome.
        /// Not ideal to put it here but seems like the only option as this is not something I can ask the user 
        /// </summary>
        int BitSize;

        #region ---Initializer Methods---
        public XUC_SetupChange_Children()
        {
            InitializeComponent();
            vGridControl1.ValidatingEditor += VGridControl1_ValidatingEditor;
            vGridControl1.BindingContext = new BindingContext();
            vGridControl1.DataSource = null;

            

            BitSize = 25;
            
        }

        private void InitializeAdjustmentTools()
        {
            rICheckedCB_Adj_CasterKPI.Items.AddRange(new object[] { AdjustmentTools.TopFrontArm, AdjustmentTools.TopRearArm, AdjustmentTools.BottomFrontArm, AdjustmentTools.BottomRearArm });

            rICheckedCB_Adj_Camber.Items.AddRange(new object[] { AdjustmentTools.TopCamberMount, AdjustmentTools.BottomCamberMount });

            rICheckedCB_Adj_BumpSteer.Items.AddRange(new object[] { AdjustmentTools.ToeLinkInboardPoint });
        }

        private void InitializeDictionaries()
        {
            setupChange_CV.Caster_KPI_Adj = new Dictionary<string, SetupChange_AdjToolParams>();
            setupChange_CV.Caster_KPI_Adj.Add(AdjustmentTools.TopFrontArm.ToString(), new SetupChange_AdjToolParams(AdjustmentTools.TopFrontArm.ToString(), 0, 10, -10, BitSize));

            setupChange_CV.Camber_Adj = new Dictionary<string, SetupChange_AdjToolParams>();
            setupChange_CV.Camber_Adj.Add(AdjustmentTools.TopCamberMount.ToString(), new SetupChange_AdjToolParams(AdjustmentTools.TopCamberMount.ToString(), 0, 5, -5, BitSize));

            setupChange_CV.Toe_Adj = new Dictionary<string, SetupChange_AdjToolParams>();
            setupChange_CV.Toe_Adj.Add(AdjustmentTools.ToeLinkLength.ToString(), new SetupChange_AdjToolParams(AdjustmentTools.ToeLinkLength.ToString(), 0, 10, -10, BitSize));

            setupChange_CV.BumpSteer_Adj = new Dictionary<string, SetupChange_AdjToolParams>();
            setupChange_CV.BumpSteer_Adj.Add(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x", new SetupChange_AdjToolParams(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x", 232.12, 5, -5, BitSize));
            setupChange_CV.BumpSteer_Adj.Add(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y", new SetupChange_AdjToolParams(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y", 124.4, 5, -5, BitSize));
            setupChange_CV.BumpSteer_Adj.Add(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z", new SetupChange_AdjToolParams(AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z", 60.8, 5, -5, BitSize));
        }

        /// <summary>
        /// Method to Get the Object Data of this child class's <see cref="Parent"/>
        /// </summary>
        /// <param name="_setupChangeGUI">Object of the <see cref="SetupChange_GUI"/> Class</param>
        /// <param name="_setupChangeCornerVariables">Object of the <see cref="SetupChange_CornerVariables"/> Class</param>
        /// <param name="_identifier">Identifier</param>
        public void GetGrandParentObjectData(SetupChange_GUI _setupChangeGUI, SetupChange_CornerVariables _setupChangeCornerVariables, int _identifier, string _cornerName, int indexSetupChangeGUI)
        {
            setupChangeGUI = _setupChangeGUI;


            this.setupChange_CV = _setupChangeCornerVariables;
            this.setupChange_CV.cornerName = _cornerName;

            setupChange_CV.LinkLengthsWhichHaveNotChanged = new List<int>();

            setupChange_CV.LinkLengthsWhichHaveNotChanged.AddRange(new int[] { 1, 2, 3, 4 });

            this.setupChange_CV.InitAdjustmentToolsDictionary(AdjustmentTools.TopCamberMount, AdjustmentTools.ToeLinkLength, AdjustmentTools.TopFrontArm, AdjustmentTools.TopFrontArm, AdjustmentTools.PushrodLength);
            Identifier = _identifier;

            parentForm = XUC_SetupChange.AssignFormVariable();

            InitializeDictionaries();

            InitializeAdjustmentTools();

        }

        #endregion

        #region ---Cell Validator Events---
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
            if (vGridControl1.FocusedRow == rowKPICasterAdjusterSelect)
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
            if (vGridControl1.FocusedRow == rowCamberMount)
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

        private bool CheckIfBumpSteerAduster()
        {
            if (vGridControl1.FocusedRow == rowBumpSteerAdjuster)
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
                if (!CheckIfCamberMountSelecter() && !CheckIfCasterAdjusterSelecter() && !CheckIfKPIAdjusterSelecter() && !CheckIfCamberChangeMethod() && !CheckIfRideHeightChangeMethod() && !CheckIfBumpSteerAduster())
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
        
        bool casterDisabledDuetoThreeLL = false;
        bool kpiDisabledDueToThreeLL = false;
        
        #region ---Checkbox Changes Events---
        /// <summary>
        /// Checked Listbox Event which is fired when the item is checked or unchecked in the Listbox of SetupChanges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

            #region ---KPI---
            ///<summary>
            ///---KPI--- Checkbox Constraint Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Checked)
            {
                setupChange_CV.KPIChangeRequested = true;
                rowKPIAngle.Enabled = true;
                rowKPICasterAdjusterSelect.Enabled = true;
                checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["KPI constant"].Enabled = false;

                if (setupChange_CV.LinkLengthsWhichHaveNotChanged.Count == 1)
                {
                    checkedListBoxControlChanges.Items["Caster Change"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlChanges.Items["Caster Change"].Enabled = false;
                    checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlConstraints.Items["Caster constant"].Enabled = false;
                    casterDisabledDuetoThreeLL = true;
                    return;
                }

                if (!setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                {
                    setupChange_CV.Master_Adj.Add("Caster/KPI", setupChange_CV.Caster_KPI_Adj);
                }

            }
            else if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Unchecked)
            {
                setupChange_CV.KPIChangeRequested = false;
                rowKPIAngle.Enabled = false;
                vGridControl1.SetCellValue(rowKPIAngle, 1, null);
                rowKPIAngle.Properties.Value = null;

                setupChange_CV.KPIChangeRequested = false;

                if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Unchecked && checkedListBoxControlConstraints.Items["Caster constant"].CheckState == CheckState.Unchecked)
                {
                    if (checkedListBoxControlConstraints.Items["KPI constant"].CheckState == CheckState.Unchecked)
                    {
                        Deactivate_KPICaster_Adjusters();

                        if (setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                        {
                            setupChange_CV.Master_Adj.Remove("Caster/KPI");
                        }
                    }
                }

                //checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["KPI constant"].Enabled = true;

                if (casterDisabledDuetoThreeLL == true)
                {
                    checkedListBoxControlChanges.Items["Caster Change"].Enabled = true;
                    checkedListBoxControlConstraints.Items["Caster constant"].Enabled = true;
                    casterDisabledDuetoThreeLL = false;
                }

            }
            #endregion

            #region ---Camber---

            ///<summary>
            ///---Camber--- Checkbox Constraint Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Checked)
            {
                setupChange_CV.CamberChangeRequested = true;
                //rowCamberChangeMethod.Enabled = true;
                rowCamberAngle.Enabled = true;
                rowCamberMount.Enabled = true;
                rowShimThickness.Enabled = true;

                checkedListBoxControlConstraints.Items["Camber constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Camber constant"].Enabled = false;

                if (!setupChange_CV.Master_Adj.ContainsKey("Camber"))
                {
                    setupChange_CV.Master_Adj.Add("Camber", setupChange_CV.Camber_Adj);
                }

            }
            else if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Unchecked)
            {

                setupChange_CV.CamberChangeRequested = false;
                rowCamberAngle.Enabled = false;
                vGridControl1.SetCellValue(rowCamberAngle, 1, null);
                rowCamberAngle.Properties.Value = null;

                setupChange_CV.CamberChangeRequested = false;

                //rowNoOfShims.Enabled = false;
                //vGridControl1.SetCellValue(rowNoOfShims, 1, null);
                //rowNoOfShims.Properties.Value = null;

                if (checkedListBoxControlConstraints.Items["Camber constant"].CheckState == CheckState.Unchecked)
                {
                    //rowCamberMount.Enabled = false;
                    //vGridControl1.SetCellValue(rowCamberMount, 1, null);
                    //rowCamberMount.Properties.Value = null;

                    Deactivate_Camber_Adjusters();

                    if (setupChange_CV.Master_Adj.ContainsKey("Camber"))
                    {
                        setupChange_CV.Master_Adj.Remove("Camber");
                    }
                }

                //if (checkedListBoxControlConstraints.Items["Camber constant"].CheckState == CheckState.Unchecked)
                //{
                //    rowShimThickness.Enabled = false;
                //    vGridControl1.SetCellValue(rowShimThickness, 1, null);
                //    rowShimThickness.Properties.Value = null;
                //}

                //checkedListBoxControlConstraints.Items["Camber constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Camber constant"].Enabled = true;


            }
            #endregion

            #region ---Caster---
            ///<summary>
            ///---Caster--- Checkbox Constraint Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Checked)
            {
                setupChange_CV.CasterChangeRequested = true;

                rowCasterAngle.Enabled = true;

                rowKPICasterAdjusterSelect.Enabled = true;

                checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Caster constant"].Enabled = false;

                if (setupChange_CV.LinkLengthsWhichHaveNotChanged.Count == 1)
                {
                    checkedListBoxControlChanges.Items["KPI Change"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlChanges.Items["KPI Change"].Enabled = false;
                    checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
                    checkedListBoxControlConstraints.Items["KPI constant"].Enabled = false;
                    kpiDisabledDueToThreeLL = true;
                    return;
                }

                if (!setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                {
                    setupChange_CV.Master_Adj.Add("Caster/KPI", setupChange_CV.Caster_KPI_Adj);
                }
            }
            else if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Unchecked)
            {

                setupChange_CV.CasterChangeRequested = false;
                rowCasterAngle.Enabled = false;
                vGridControl1.SetCellValue(rowCasterAngle, 1, null);
                rowCasterAngle.Properties.Value = null;

                setupChange_CV.CasterChangeRequested = false;

                if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Unchecked && checkedListBoxControlConstraints.Items["KPI constant"].CheckState == CheckState.Unchecked)
                {
                    if (checkedListBoxControlConstraints.Items["Caster constant"].CheckState == CheckState.Unchecked)
                    {
                        Deactivate_KPICaster_Adjusters();

                        if (setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                        {
                            setupChange_CV.Master_Adj.Remove("Caster/KPI");
                        }

                    }
                }

                //checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Caster constant"].Enabled = true;

                if (kpiDisabledDueToThreeLL == true)
                {
                    checkedListBoxControlChanges.Items["KPI Change"].Enabled = true;
                    checkedListBoxControlConstraints.Items["KPI constant"].Enabled = true;
                    kpiDisabledDueToThreeLL = false;
                    return;
                }
            }
            #endregion

            #region ---TOE---
            ///<summary>
            ///---TOE--- Check Box Operations
            /// </summary>
            if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Checked)
            {
                setupChange_CV.ToeChangeRequested = true;
                //vGridControl1.Rows["rowToe"].Visible = true;
                rowToeAngle.Enabled = true;
                rowToeLink.Visible = true;
                checkedListBoxControlConstraints.Items["Toe constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Toe constant"].Enabled = false;

                if (!setupChange_CV.Master_Adj.ContainsKey("Toe"))
                {
                    setupChange_CV.Master_Adj.Add("Toe", setupChange_CV.Toe_Adj);
                }

            }
            else if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Unchecked)
            {
                //vGridControl1.Rows["rowToe"].Visible = false;
                rowToeAngle.Enabled = false;
                vGridControl1.SetCellValue(rowToeAngle, 1, null);
                rowToeAngle.Properties.Value = null;

                setupChange_CV.ToeChangeRequested = false;

                if (checkedListBoxControlConstraints.Items["Toe constant"].CheckState == CheckState.Unchecked)
                {
                    Deactivate_Toe_Adjusters();

                    if (setupChange_CV.Master_Adj.ContainsKey("Toe"))
                    {
                        setupChange_CV.Master_Adj.Remove("Toe");
                    }
                }

                //checkedListBoxControlConstraints.Items["Toe constant"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Toe constant"].Enabled = true;


            }
            #endregion

            #region --_Ride Height---
            ///<summary>
            ///---Ride Height --- Check Box Operations
            ///</summary>
            if (checkedListBoxControlChanges.Items["Ride Height Change"].CheckState == CheckState.Checked)
            {
                
                //rowRideHeightChangeMethod.Enabled = true;
                //rowRideHeight.Enabled = true;
                //rowDamperEyeToPerch.Enabled = false;
                rowRideHeight.Enabled = true;
                //checkedListBoxControlConstraints.Items["Ride Height constant"].CheckState = CheckState.Unchecked;
                //checkedListBoxControlConstraints.Items["Ride Height constant"].Enabled = false;
            }
            else if (checkedListBoxControlChanges.Items["Ride Height Change"].CheckState == CheckState.Unchecked)
            {
                //rowRideHeightChangeMethod.Enabled = false;
                //vGridControl1.SetCellValue(rowRideHeightChangeMethod, 1, null);
                //rowRideHeightChangeMethod.Properties.Value = null;
                rowRideHeight.Enabled = false;
                vGridControl1.SetCellValue(rowRideHeight, 1, null);
                rowRideHeight.Properties.Value = null;
                rowDamperEyeToPerch.Enabled = false;
                vGridControl1.SetCellValue(rowDamperEyeToPerch, 1, null);
                rowDamperEyeToPerch.Properties.Value = null;
                ////checkedListBoxControlConstraints.Items["Ride Height constant"].CheckState = CheckState.Unchecked;
                //checkedListBoxControlConstraints.Items["Ride Height constant"].Enabled = true;


            }

            #endregion

            #region ---Bump Steer---

            ///<summary>
            ///
            ///---Bump Steer--- Check Box Operations
            ///</summary>
            if (checkedListBoxControlChanges.Items["Bump Steer Change"].CheckState == CheckState.Checked)
            {
                setupChange_CV.BumpSteerChangeRequested = true;

                bsCurve.Enabled = true;
                bsCurve.GetParentObjectData(setupChange_CV);

                rowBumpSteerAdjuster.Enabled = true;
                checkedListBoxControlConstraints.Items["Monitor Bump Steer"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Monitor Bump Steer"].Enabled = false;

                if (!setupChange_CV.Master_Adj.ContainsKey("Bump Steer"))
                {
                    setupChange_CV.Master_Adj.Add("Bump Steer", setupChange_CV.BumpSteer_Adj);
                }
            }
            else
            {
                setupChange_CV.BumpSteerChangeRequested = false;

                bsCurve.Enabled = false;
                //rowBumpSteerAdjuster.Enabled = false;
                //vGridControl1.SetCellValue(rowBumpSteerAdjuster, 1, null);

                //if (checkedListBoxControlConstraints.Items["Monitor Bump Steer"].CheckState == CheckState.Unchecked)
                //{
                    Deactivate_BumpSteer_Adjusters();

                    if (setupChange_CV.Master_Adj.ContainsKey("Bump Steer"))
                    {
                        setupChange_CV.Master_Adj.Remove("Bump Steer");
                    }
                //}

                //checkedListBoxControlConstraints.Items["Monitor Bump Steer"].CheckState = CheckState.Unchecked;
                checkedListBoxControlConstraints.Items["Monitor Bump Steer"].Enabled = true;



            } 
            #endregion


            #region NOT NEEDED-LinkLength
            //if (checkedListBoxControlChanges.Items["Link Length Change"].CheckState == CheckState.Checked)
            //{
            //    rowTopFront.Enabled = true;
            //    rowTopRear.Enabled = true;
            //    rowBottomFron.Enabled = true;
            //    rowBottomRear.Enabled = true;
            //    rowPushrod.Enabled = true;
            //    rowToeLinkLength.Enabled = true;
            //}
            //else if (checkedListBoxControlChanges.Items["Link Length Change"].CheckState == CheckState.Unchecked)
            //{
            //    rowTopFront.Enabled = false;
            //    vGridControl1.SetCellValue(rowTopFront, 1, null);
            //    rowTopFront.Properties.Value = null;
            //    rowTopRear.Enabled = false;
            //    vGridControl1.SetCellValue(rowTopRear, 1, null);
            //    rowTopRear.Properties.Value = null;
            //    rowBottomFron.Enabled = false;
            //    vGridControl1.SetCellValue(rowBottomFron, 1, null);
            //    rowBottomFron.Properties.Value = null;
            //    rowBottomRear.Enabled = false;
            //    vGridControl1.SetCellValue(rowBottomRear, 1, null);
            //    rowBottomRear.Properties.Value = null;
            //    rowPushrod.Enabled = false;
            //    vGridControl1.SetCellValue(rowPushrod, 1, null);
            //    rowPushrod.Properties.Value = null;
            //    rowToeLinkLength.Enabled = false;
            //    vGridControl1.SetCellValue(rowToeLinkLength, 1, null);
            //    rowToeLinkLength.Properties.Value = null;

            //} 
            #endregion

            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void Deactivate_KPICaster_Adjusters()
        {
            rowKPICasterAdjusterSelect.Enabled = false;
            rowKPICasterAdjusterSelect.Properties.Value = null;
            vGridControl1.SetCellValue(rowKPICasterAdjusterSelect, 1, null);

            rowTopFrontArm.Visible = false;
            rowTopRearArm.Visible = false;
            rowBottomFrontArm.Visible = false;
            rowBottomRearArm.Visible = false;

            for (int i = 0; i < rICheckedCB_Adj_CasterKPI.Items.Count; i++)
            {
                rICheckedCB_Adj_CasterKPI.Items[i].CheckState = CheckState.Unchecked;
            }


            //setupChange_CV.Caster_KPI_Adj.Clear(); 
        }
        private void Activate_KPICaster_Adjusters()
        {
            rowKPICasterAdjusterSelect.Enabled = true;

        }


        private void Deactivate_Camber_Adjusters()
        {
            rowCamberMount.Enabled = false;
            vGridControl1.SetCellValue(rowCamberMount, 1, null);
            rowCamberMount.Properties.Value = null;

            rowTopCamberMount.Visible = false;
            rowBottomCamberMount.Visible = false;

            for (int i = 0; i < rICheckedCB_Adj_Camber.Items.Count; i++)
            {
                rICheckedCB_Adj_Camber.Items[i].CheckState = CheckState.Unchecked;
            }

            //setupChange_CV.Camber_Adj.Clear();
        }
        private void Activate_Camber_Adjusters()
        {
            rowCamberMount.Enabled = true;
        }

        private void Deactivate_BumpSteer_Adjusters()
        {
            rowBumpSteerAdjuster.Enabled = false;
            vGridControl1.SetCellValue(rowBumpSteerAdjuster, 1, null);

            rowToeLinkInboard_x.Visible = false;
            rowToeLinkInboard_y.Visible = false;
            rowToeLinkInboard_z.Visible = false;

            for (int i = 0; i < rICheckedCB_Adj_Camber.Items.Count; i++)
            {
                rICheckedCB_Adj_Camber.Items[0].CheckState = CheckState.Unchecked;
            }

            //setupChange_CV.BumpSteer_Adj.Clear();
        }
        private void Activate_BumpSteer_Adjusters()
        {
            rowBumpSteerAdjuster.Enabled = true;
        }

        private void Deactivate_Toe_Adjusters()
        {
            rowToeLink.Enabled = false;
            rowToeLink.Visible = false;
        }
        private void Activate_Toe_Adjusters()
        {
            rowToeLink.Enabled = true;
            rowToeLink.Visible = true;
        }

        #endregion

        #region ---Checkbox Constant Events---
        /// <summary>
        /// Checked Listbox Event which is fired when the item is checked or unchecked in the Listbox of Constraints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxControlConstraints_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            #region ---KPI---
            ///<summary>
            ///---KPI--- Check Box Operations
            /// </summary>
            if (checkedListBoxControlConstraints.Items["KPI constant"].CheckState == CheckState.Checked)
            {
                setupChange_CV.constKPI = true;

                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);

                Activate_KPICaster_Adjusters();

                if (!setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                {
                    setupChange_CV.Master_Adj.Add("Caster/KPI", setupChange_CV.Caster_KPI_Adj);
                }
            }
            else
            {
                setupChange_CV.constKPI = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);

                if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Unchecked && checkedListBoxControlConstraints.Items["Caster constant"].CheckState == CheckState.Unchecked)
                {
                    if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Unchecked)
                    {
                        Deactivate_KPICaster_Adjusters();

                        if (setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                        {
                            setupChange_CV.Master_Adj.Remove("Caster/KPI");
                        }
                    }

                }

            }
            #endregion

            #region ---Caster---
            ///<summary>
            ///---Caster--- Check Box Operations
            /// </summary>
            if (checkedListBoxControlConstraints.Items["Caster constant"].CheckState == CheckState.Checked)
            {
                setupChange_CV.constCaster = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
                Activate_KPICaster_Adjusters();

                if (!setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                {
                    setupChange_CV.Master_Adj.Add("Caster/KPI", setupChange_CV.Caster_KPI_Adj);
                }

            }
            else
            {
                setupChange_CV.constCaster = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);

                if (checkedListBoxControlChanges.Items["KPI Change"].CheckState == CheckState.Unchecked && checkedListBoxControlConstraints.Items["KPI constant"].CheckState == CheckState.Unchecked)
                {
                    if (checkedListBoxControlChanges.Items["Caster Change"].CheckState == CheckState.Unchecked)
                    {
                        Deactivate_KPICaster_Adjusters();

                        if (setupChange_CV.Master_Adj.ContainsKey("Caster/KPI"))
                        {
                            setupChange_CV.Master_Adj.Remove("Caster/KPI");
                        }
                    }
                }
            }
            #endregion

            #region ---Camber---
            ///<summary>
            ///---Camber--- Check Box Operations
            /// </summary>
            if (checkedListBoxControlConstraints.Items["Camber constant"].CheckState == CheckState.Checked)
            {
                setupChange_CV.constCamber = true;
                rowShimThickness.Enabled = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
                Activate_Camber_Adjusters();

                if (!setupChange_CV.Master_Adj.ContainsKey("Camber"))
                {
                    setupChange_CV.Master_Adj.Add("Camber", setupChange_CV.Camber_Adj);
                }
            }
            else
            {
                if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Unchecked)
                {
                    rowShimThickness.Enabled = false;
                }
                setupChange_CV.constCamber = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);

                if (checkedListBoxControlChanges.Items["Camber Change"].CheckState == CheckState.Unchecked)
                {
                    Deactivate_Camber_Adjusters();

                    if (setupChange_CV.Master_Adj.ContainsKey("Camber"))
                    {
                        setupChange_CV.Master_Adj.Remove("Camber");
                    }

                }



            }
            #endregion

            #region ---Toe---
            ///<summary>
            ///---Toe--- Check Box Operations
            /// </summary>
            if (checkedListBoxControlConstraints.Items["Toe constant"].CheckState == CheckState.Checked)
            {
                setupChange_CV.constToe = true;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
                Activate_Toe_Adjusters();

                if (!setupChange_CV.Master_Adj.ContainsKey("Toe"))
                {
                    setupChange_CV.Master_Adj.Add("Toe", setupChange_CV.Toe_Adj);
                }
            }
            else
            {
                setupChange_CV.constToe = false;
                setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);

                if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Unchecked) 
                {
                    Deactivate_Toe_Adjusters();

                    if (setupChange_CV.Master_Adj.ContainsKey("Toe"))
                    {
                        setupChange_CV.Master_Adj.Remove("Toe");
                    }
                }


            }
            #endregion

            #region ---Ride Height----
            /////<summary>
            /////---Ride Height--- Check Box Operations
            ///// </summary>
            //if (checkedListBoxControlConstraints.Items["Ride Height constant"].CheckState == CheckState.Checked)
            //{
            //    setupChange_CV.constRideHeight = true;
            //    setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            //}
            //else
            //{
            //    setupChange_CV.constRideHeight = false;
            //    setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            //}
            #endregion

            #region ---Bump Steer---
            ///<summary>
            ///---Bump Steer--- Check Box Operationa
            /// </summary>
            if (checkedListBoxControlConstraints.Items["Monitor Bump Steer"].CheckState == CheckState.Checked)
            {
                setupChange_CV.monitorBumpSteer = true;

                setupChange_CV.BS_Params.PopulateBumpSteerGraph(new List<double>(new double[] { 25, 0, -25 }), new List<double>(new double[] { 0, 0, 0 }));

                //Activate_BumpSteer_Adjusters();

                //if (!setupChange_CV.Master_Adj.ContainsKey("Bump Steer"))
                //{
                //    setupChange_CV.Master_Adj.Add("Bump Steer", setupChange_CV.BumpSteer_Adj);
                //}
            }
            else
            {
                setupChange_CV.monitorBumpSteer = false;

                setupChange_CV.BS_Params.WheelDeflections.Clear();

                setupChange_CV.BS_Params.ToeAngles.Clear();

                //if (checkedListBoxControlChanges.Items["Bump Steer Change"].CheckState == CheckState.Unchecked) 
                //{
                //    Deactivate_BumpSteer_Adjusters();

                //    if (setupChange_CV.Master_Adj.ContainsKey("Bump Steer"))
                //    {
                //        setupChange_CV.Master_Adj.Remove("Bump Steer");
                //    }
                //}


            } 
            #endregion


            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();

        } 
        #endregion

        #region ---Text Changed Events--- All the RepositoryItem Textboxes
        private void r1TextboxKPI_Leave(object sender, EventArgs e)
        {
            //vGridControl1.UpdateFocusedRecord();
            //setupChangeCornerVariables.deltaKPI = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow,vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.kpiAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.kpiAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["KPIChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
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
            setupChange_CV.deltaKPI = _kpiValue;
            setupChange_CV.kpiAdjustmentType = _adjType;
            setupChange_CV.kpiAdjustmentTool = _adjTool;
            setupChange_CV.AdjToolsDictionary["KPIChange"] = _adjTool;

            if (setupChange_CV.deltaKPI != 0)
            {
                setupChange_CV.KPIChangeRequested = true;
            }
            else
            {
                setupChange_CV.KPIChangeRequested = false;
            }
        }
        
        private void r1TextboxCamber_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaCamber = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.camberAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.camberAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["CamberChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void CamberChangeRequested(double _camberValue, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChange_CV.deltaCamber = _camberValue;
            setupChange_CV.camberAdjustmentType = _adjType;
            setupChange_CV.camberAdjustmentTool = _adjTool;
            setupChange_CV.AdjToolsDictionary["CamberChange"] = _adjTool;

            if (setupChange_CV.deltaCamber != 0)
            {
                setupChange_CV.CamberChangeRequested = true;
            }
            else
            {
                setupChange_CV.CamberChangeRequested = false;
            }
        }
        private void CamberShimCountChanged(int _noOfShims, double _shimThickness, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChange_CV.deltaCamberShims = _noOfShims;
            setupChange_CV.camberShimThickness = _shimThickness;
            setupChange_CV.camberAdjustmentType = _adjType;
            setupChange_CV.camberAdjustmentTool = _adjTool;
            setupChange_CV.AdjToolsDictionary["CamberChange"] = _adjTool;

            if (setupChange_CV.deltaCamberShims != 0)
            {
                setupChange_CV.CamberChangeRequested = true;
            }
            else
            {
                setupChange_CV.CamberChangeRequested = false;
            }

        }





        private void r1TextboxCaster_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaCaster = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.casterAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.casterAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["CasterChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        private void CasterChangeRequested(double _casterChange, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChange_CV.deltaCaster = _casterChange;
            setupChange_CV.casterAdjustmentType = _adjType;
            setupChange_CV.casterAdjustmentTool = _adjTool;
            setupChange_CV.AdjToolsDictionary["CasterChange"] = _adjTool;

            if (setupChange_CV.deltaCaster != 0)
            {
                setupChange_CV.CasterChangeRequested = true;
            }
            else
            {
                setupChange_CV.CasterChangeRequested = false;
            }

        }





        private void r1TextboxToe_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaToe = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.toeAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.toeAdjustmentTool = AdjustmentTools.DirectAngle;
            //setupChangeCornerVariables.AdjToolsDictionary["ToeChange"] = AdjustmentTools.DirectAngle;
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        private void ToeChangeRequested(double _toeChange, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChange_CV.deltaToe = _toeChange;
            setupChange_CV.toeAdjustmentType = _adjType;
            setupChange_CV.toeAdjustmentTool = _adjTool;
            setupChange_CV.AdjToolsDictionary["ToeChange"] = _adjTool;


            if (setupChange_CV.deltaToe != 0)
            {
                setupChange_CV.ToeChangeRequested = true;
            }
            else
            {
                setupChange_CV.ToeChangeRequested = false;
            }

        }






        private void r1TextboxRideHeight_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaRideHeight = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.rideheightAdjustmentType = AdjustmentType.Direct;
            //setupChangeCornerVariables.rideheightAdjustmentTool = AdjustmentTools.PushrodLength;
            //setupChangeCornerVariables.AdjToolsDictionary["RideHeightChange"] = AdjustmentTools.PushrodLength;
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }
        private void RideHeightChangeRequested(double _rideHeightChange, AdjustmentType _adjType, AdjustmentTools _adjTool)
        {
            setupChange_CV.deltaRideHeight = _rideHeightChange;
            setupChange_CV.rideheightAdjustmentType = _adjType;
            setupChange_CV.rideheightAdjustmentTool = _adjTool;
            setupChange_CV.AdjToolsDictionary["RideHeightChange"] = _adjTool;
            setupChange_CV.RideHeightChanged = true;

            if (setupChange_CV.deltaRideHeight != 0)
            {
                setupChange_CV.RHIChangeRequested = true;
            }
            else
            {
                setupChange_CV.RHIChangeRequested = false;
            }


        }




        private void r1TextboxShims_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.deltaCamberShims = Convert.ToInt32(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //setupChangeCornerVariables.camberAdjustmentType = AdjustmentType.Indirect;
            //setupChangeCornerVariables.camberAdjustmentTool = AdjustmentTools.TopCamberMount;
            //setupChangeCornerVariables.AdjToolsDictionary["CamberChange"] = AdjustmentTools.TopCamberMount;
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void r1TextboxShimThickness_Leave(object sender, EventArgs e)
        {
            //setupChangeCornerVariables.camberShimThickness = Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();
        }

        private void Set_TopFrontArmParams()
        {
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.TopFrontArm.ToString()].Uppwer = (double)rowTopFrontArm.PropertiesCollection[0].Value;
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.TopFrontArm.ToString()].Lower = (double)rowTopFrontArm.PropertiesCollection[1].Value;
        }

        private void Set_TopRearArmParams()
        {
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.TopRearArm.ToString()].Uppwer = (double)rowTopRearArm.PropertiesCollection[0].Value;
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.TopRearArm.ToString()].Lower = (double)rowTopRearArm.PropertiesCollection[1].Value;
        }

        private void Set_BottomFrontArmParams()
        {
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.BottomFrontArm.ToString()].Uppwer = (double)rowBottomFrontArm.PropertiesCollection[0].Value;
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.BottomFrontArm.ToString()].Lower = (double)rowBottomFrontArm.PropertiesCollection[1].Value;
        }

        private void Set_BottomRearArmParams()
        {
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.BottomRearArm.ToString()].Uppwer = (double)rowBottomRearArm.PropertiesCollection[0].Value;
            setupChange_CV.Caster_KPI_Adj[AdjustmentTools.BottomRearArm.ToString()].Lower = (double)rowBottomRearArm.PropertiesCollection[1].Value;
        }

        private void Set_TopCamberMountParams()
        {
            setupChange_CV.Camber_Adj[AdjustmentTools.TopCamberMount.ToString()].Uppwer = (double)rowTopCamberMount.PropertiesCollection[0].Value;
            setupChange_CV.Camber_Adj[AdjustmentTools.TopCamberMount.ToString()].Lower = (double)rowTopCamberMount.PropertiesCollection[1].Value;
        }

        private void Set_BottomCamberMountParams()
        {
            setupChange_CV.Camber_Adj[AdjustmentTools.BottomCamberMount.ToString()].Uppwer = (double)rowBottomCamberMount.PropertiesCollection[0].Value;
            setupChange_CV.Camber_Adj[AdjustmentTools.BottomCamberMount.ToString()].Lower = (double)rowBottomCamberMount.PropertiesCollection[1].Value;
        }

        private void Set_ToeLinkParams()
        {
            setupChange_CV.Toe_Adj[AdjustmentTools.ToeLinkLength.ToString()].Uppwer = (double)rowToeLink.PropertiesCollection[0].Value;
            setupChange_CV.Toe_Adj[AdjustmentTools.ToeLinkLength.ToString()].Lower = (double)rowToeLink.PropertiesCollection[1].Value;
        }

        private void Set_ToeLinkInboardParams()
        {
            setupChange_CV.BumpSteer_Adj[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x"].Uppwer = (double)rowToeLinkInboard_x.PropertiesCollection[0].Value;
            setupChange_CV.BumpSteer_Adj[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_x"].Lower = (double)rowToeLinkInboard_x.PropertiesCollection[1].Value;

            setupChange_CV.BumpSteer_Adj[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y"].Uppwer = (double)rowToeLinkInboard_y.PropertiesCollection[0].Value;
            setupChange_CV.BumpSteer_Adj[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_y"].Lower = (double)rowToeLinkInboard_y.PropertiesCollection[1].Value;

            setupChange_CV.BumpSteer_Adj[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z"].Uppwer = (double)rowToeLinkInboard_z.PropertiesCollection[0].Value;
            setupChange_CV.BumpSteer_Adj[AdjustmentTools.ToeLinkInboardPoint.ToString() + "_z"].Lower = (double)rowToeLinkInboard_z.PropertiesCollection[1].Value;
        }


          
        #region Link Length Changed methods. NOT NEEDED
        //private void LinkLengthChanged()
        //{
        //    double ToeLinkLength = Convert.ToDouble(vGridControl1.GetCellValue(rowToeLinkLength, 1));
        //    double PushrodLength = Convert.ToDouble(vGridControl1.GetCellValue(rowPushrod, 1));
        //    double TopFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopFront, 1));
        //    double TopRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopRear, 1));
        //    double BottomFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomFron, 1));
        //    double BottomRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomRear, 1));

        //    //WillCauseEndlessLoop = true;
        //    setupChange_CV.LinkLengthChanged = true;
        //    if (TopFrontArm != 0)
        //    {
        //        setupChange_CV.deltaTopFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopFront, 1));
        //        setupChange_CV.LinkLengthsWhichHaveNotChanged.Remove(1);
        //        RemoveFromRICombobox("Top Front Arm", rIComboBoxWishboneSelecter);
        //        rowKPICasterAdjusterSelect.Properties.Value = null;
        //        rowCasterAdjusterSelect.Properties.Value = null;

        //    }
        //    else
        //    {
        //        setupChange_CV.deltaTopFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopFront, 1));
        //        if (!setupChange_CV.LinkLengthsWhichHaveNotChanged.Contains(1))
        //        {
        //            setupChange_CV.LinkLengthsWhichHaveNotChanged.Add(1);
        //        }
        //        AddToRIComboBox("Top Front Arm", rIComboBoxWishboneSelecter);
        //    }


        //    if (TopRearArm != 0)
        //    {
        //        setupChange_CV.deltaTopRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopRear, 1));
        //        setupChange_CV.LinkLengthsWhichHaveNotChanged.Remove(2);
        //        RemoveFromRICombobox("Top Rear Arm", rIComboBoxWishboneSelecter); /*WillCauseEndlessLoop = true;*/

        //    }
        //    else
        //    {
        //        setupChange_CV.deltaTopRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowTopRear, 1));
        //        if (!setupChange_CV.LinkLengthsWhichHaveNotChanged.Contains(2))
        //        {
        //            setupChange_CV.LinkLengthsWhichHaveNotChanged.Add(2);
        //        }
        //        AddToRIComboBox("Top Rear Arm", rIComboBoxWishboneSelecter);
        //    }


        //    if (BottomFrontArm != 0)
        //    {
        //        setupChange_CV.deltaBottmFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomFron, 1));
        //        setupChange_CV.LinkLengthsWhichHaveNotChanged.Remove(3);
        //        RemoveFromRICombobox("Bottom Front Arm", rIComboBoxWishboneSelecter); 
        //        rowKPICasterAdjusterSelect.Properties.Value = null;
        //        rowCasterAdjusterSelect.Properties.Value = null;
        //    }
        //    else
        //    {
        //        setupChange_CV.deltaBottmFrontArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomFron, 1));
        //        if (!setupChange_CV.LinkLengthsWhichHaveNotChanged.Contains(3))
        //        {
        //            setupChange_CV.LinkLengthsWhichHaveNotChanged.Add(3);
        //        }
        //        AddToRIComboBox("Bottom Front Arm", rIComboBoxWishboneSelecter);
        //    }


        //    if (BottomRearArm != 0)
        //    {
        //        setupChange_CV.deltaBottomRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomRear, 1));
        //        setupChange_CV.LinkLengthsWhichHaveNotChanged.Remove(4);
        //        RemoveFromRICombobox("Bottom Rear Arm", rIComboBoxWishboneSelecter); 
        //        rowKPICasterAdjusterSelect.Properties.Value = null;
        //        rowCasterAdjusterSelect.Properties.Value = null;
        //    }
        //    else
        //    {
        //        setupChange_CV.deltaBottomRearArm = Convert.ToDouble(vGridControl1.GetCellValue(rowBottomRear, 1));
        //        if (!setupChange_CV.LinkLengthsWhichHaveNotChanged.Contains(4))
        //        {
        //            setupChange_CV.LinkLengthsWhichHaveNotChanged.Add(4);
        //        }
        //        AddToRIComboBox("Bottom Rear Arm", rIComboBoxWishboneSelecter);
        //    }


        //    if (PushrodLength != 0)
        //    {
        //        setupChange_CV.deltaPushrod = Convert.ToDouble(vGridControl1.GetCellValue(rowPushrod, 1));
        //        rowRideHeight.Properties.Value = null;
        //        rowRideHeight.Enabled = false;
        //        rowDamperEyeToPerch.Properties.Value = null;
        //        rowDamperEyeToPerch.Enabled = false;
        //        rowRideHeightChangeMethod.Properties.Value = null;
        //        rowRideHeightChangeMethod.Enabled = false;
        //        setupChange_CV.RideHeightChanged = true;
        //        setupChange_CV.rideheightAdjustmentType = AdjustmentType.Indirect;
        //        setupChange_CV.rideheightAdjustmentTool = AdjustmentTools.PushrodLength;
        //    }
        //    else
        //    {
        //        setupChange_CV.deltaPushrod = Convert.ToDouble(vGridControl1.GetCellValue(rowPushrod, 1));
        //        if (checkedListBoxControlChanges.Items["Ride Height Change"].CheckState == CheckState.Checked)
        //        {
        //            setupChange_CV.RideHeightChanged = false;
        //            rowRideHeightChangeMethod.Enabled = true;

        //        }
        //    }


        //    if (ToeLinkLength != 0)
        //    {
        //        setupChange_CV.deltaToeLinkLength = Convert.ToDouble(vGridControl1.GetCellValue(rowToeLinkLength, 1));
        //        setupChange_CV.toeAdjustmentType = AdjustmentType.Indirect;
        //        setupChange_CV.toeAdjustmentTool = AdjustmentTools.ToeLinkLength;
        //        rowToeAngle.Properties.Value = null;
        //        setupChange_CV.ToeChangeRequested = true;
        //        rowToeAngle.Enabled = false;
        //        checkedListBoxControlChanges.Items["Toe Change"].CheckState = CheckState.Unchecked;
        //        checkedListBoxControlChanges.Items["Toe Change"].Enabled = false;
        //        checkedListBoxControlConstraints.Items["Toe constant"].CheckState = CheckState.Unchecked;
        //        checkedListBoxControlConstraints.Items["Toe constant"].Enabled = false;
        //    }
        //    else
        //    {
        //        setupChange_CV.deltaToeLinkLength = Convert.ToDouble(vGridControl1.GetCellValue(rowToeLinkLength, 1));
        //        checkedListBoxControlChanges.Items["Toe Change"].Enabled = true;
        //        checkedListBoxControlConstraints.Items["Toe constant"].Enabled = true;
        //        setupChange_CV.ToeChangeRequested = false;
        //        if (checkedListBoxControlChanges.Items["Toe Change"].CheckState == CheckState.Checked)
        //        {
        //            rowToeAngle.Enabled = true;
        //        }
        //    }

        //    if (setupChange_CV.LinkLengthsWhichHaveNotChanged.Count == 0)
        //    {
 
        //        rowKPIAngle.Properties.Value = null;
        //        rowKPICasterAdjusterSelect.Properties.Value = null;
        //        rowCasterAngle.Properties.Value = null;
        //        rowCasterAdjusterSelect.Properties.Value = null;

        //        checkedListBoxControlChanges.Items["KPI Change"].CheckState = CheckState.Unchecked; 
        //        checkedListBoxControlChanges.Items["Caster Change"].CheckState = CheckState.Unchecked; 
        //        checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked; 
        //        checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked; 

        //        checkedListBoxControlChanges.Items["KPI Change"].Enabled = false;
        //        checkedListBoxControlChanges.Items["Caster Change"].Enabled = false;
        //        checkedListBoxControlConstraints.Items["KPI constant"].Enabled = false;
        //        checkedListBoxControlConstraints.Items["Caster constant"].Enabled = false;


        //    }
        //    else
        //    {
        //        checkedListBoxControlChanges.Items["KPI Change"].Enabled = true;
        //        checkedListBoxControlChanges.Items["Caster Change"].Enabled = true;
        //        checkedListBoxControlConstraints.Items["KPI constant"].Enabled = true;
        //        checkedListBoxControlConstraints.Items["Caster constant"].Enabled = true;
        //    }

        //    if (setupChange_CV.LinkLengthsWhichHaveNotChanged.Count == 3)
        //    {

        //    }

        //    setupChange_CV.LinkLengthChanged = true;




        //    if (TopFrontArm == 0 && TopRearArm == 0 && BottomFrontArm == 0 && BottomRearArm == 0 && (ToeLinkLength == 0) && (PushrodLength == 0))
        //    {
        //        setupChange_CV.LinkLengthChanged = false;
        //    }

        //    if (setupChange_CV.LinkLengthsWhichHaveNotChanged.Count == 1)
        //    {
        //        checkedListBoxControlChanges.Items["KPI Change"].CheckState = CheckState.Unchecked;
        //        checkedListBoxControlChanges.Items["Caster Change"].CheckState = CheckState.Unchecked;
        //        checkedListBoxControlConstraints.Items["KPI constant"].CheckState = CheckState.Unchecked;
        //        checkedListBoxControlConstraints.Items["Caster constant"].CheckState = CheckState.Unchecked;
        //    }


        //}

        #endregion


        private void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
          

            if (vGridControl1.FocusedRow == rowKPIAngle)
            {
                KPIChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }

            
            else if (vGridControl1.FocusedRow == rowKPICasterAdjusterSelect)
            {
                if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Front Arm")
                {
                    setupChange_CV.kpiAdjustmentTool = AdjustmentTools.TopFrontArm;
                    setupChange_CV.OverrideRandomSelectorForKPI = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Rear Arm")
                {
                    setupChange_CV.kpiAdjustmentTool = AdjustmentTools.TopRearArm;
                    setupChange_CV.OverrideRandomSelectorForKPI = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Front Arm")
                {
                    setupChange_CV.kpiAdjustmentTool = AdjustmentTools.BottomFrontArm;
                    setupChange_CV.OverrideRandomSelectorForKPI = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Rear Arm")
                {
                    setupChange_CV.kpiAdjustmentTool = AdjustmentTools.BottomRearArm;
                    setupChange_CV.OverrideRandomSelectorForKPI = true;
                }
                else
                {
                    setupChange_CV.OverrideRandomSelectorForKPI = false;
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
                    setupChange_CV.camberAdjustmentTool = AdjustmentTools.TopCamberMount;
                    
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Camber Mount")
                {
                    setupChange_CV.camberAdjustmentTool = AdjustmentTools.BottomCamberMount;
                }
            }


            else if (vGridControl1.FocusedRow == rowNoOfShims)
            {

                CamberShimCountChanged(Convert.ToInt32(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), Convert.ToDouble(vGridControl1.GetCellValue(rowShimThickness, vGridControl1.FocusedRecord)),
                                        AdjustmentType.Indirect, setupChange_CV.camberAdjustmentTool);
            }

            else if (vGridControl1.FocusedRow == rowShimThickness)
            {
                CamberShimCountChanged(Convert.ToInt32(vGridControl1.GetCellValue(rowNoOfShims, vGridControl1.FocusedRecord)), Convert.ToDouble(vGridControl1.GetCellValue(rowShimThickness, vGridControl1.FocusedRecord)),
                        AdjustmentType.Indirect, setupChange_CV.camberAdjustmentTool);
            }

            else if (vGridControl1.FocusedRow == rowToeAngle)
            {
                ToeChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }

            else if (vGridControl1.FocusedRow == rowCasterAngle)
            {
                ///<summary>For reasons I cannot understand I need to pass a negative sign below to make the code work in the right sign convention</summary>
                double caster = -Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
                CasterChangeRequested(caster, AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }

            else if (vGridControl1.FocusedRow == rowCasterAdjusterSelect)
            {
                if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Front Arm")
                {
                    setupChange_CV.casterAdjustmentTool = AdjustmentTools.TopFrontArm;
                    setupChange_CV.OverrideRandomSelectorForCaster = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Top Rear Arm")
                {
                    setupChange_CV.casterAdjustmentTool = AdjustmentTools.TopRearArm;
                    setupChange_CV.OverrideRandomSelectorForCaster = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Front Arm")
                {
                    setupChange_CV.casterAdjustmentTool = AdjustmentTools.BottomFrontArm;
                    setupChange_CV.OverrideRandomSelectorForCaster = true;
                }
                else if (Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)) == "Bottom Rear Arm")
                {
                    setupChange_CV.casterAdjustmentTool = AdjustmentTools.BottomRearArm;
                    setupChange_CV.OverrideRandomSelectorForCaster = true;
                }
                else
                {
                    setupChange_CV.OverrideRandomSelectorForCaster = false;
                }
            }

            else if (vGridControl1.FocusedRow == rowRideHeight)
            {
                RideHeightChangeRequested(Convert.ToDouble(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord)), AdjustmentType.Direct, AdjustmentTools.DirectValue);
            }

            //else if (vGridControl1.FocusedRow == rowTopFront || vGridControl1.FocusedRow == rowTopRear || vGridControl1.FocusedRow == rowBottomFron || vGridControl1.FocusedRow == rowBottomRear || vGridControl1.FocusedRow == rowPushrod || vGridControl1.FocusedRow == rowToeLinkLength)
            //{
                
            //    LinkLengthChanged();
            //}

            else if (vGridControl1.FocusedRow == rowTopFrontArm)
            {
                Set_TopFrontArmParams();
            }

            else if (vGridControl1.FocusedRow == rowTopRearArm)
            {
                Set_TopRearArmParams();
            }

            else if (vGridControl1.FocusedRow == rowBottomFrontArm)
            {
                Set_BottomFrontArmParams();
            }

            else if (vGridControl1.FocusedRow == rowBottomRearArm)
            {
                Set_BottomRearArmParams();
            }

            else if (vGridControl1.FocusedRow == rowTopCamberMount)
            {
                Set_TopCamberMountParams();
            }

            else if (vGridControl1.FocusedRow == rowBottomCamberMount)
            {
                Set_BottomCamberMountParams();
            }

            else if (vGridControl1.FocusedRow == rowToeLinkLength)
            {
                Set_ToeLinkParams();
            }

            else if (vGridControl1.FocusedRow == rowToeLinkInboard_x || vGridControl1.FocusedRow == rowToeLinkInboard_y || vGridControl1.FocusedRow == rowToeLinkInboard_z)
            {
                Set_ToeLinkInboardParams();
            }

            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);


            Kinematics_Software_New.ComboboxSetupChangeOperations_Invoker();

        }
        #endregion


        public void UpateSetupChangeClass()
        {
            setupChangeGUI.EditSetupChangeDeltas(setupChange_CV, Identifier);
        }


        #region ---Adjuster Selection Events---

        #region Caster/KPI Adjuster Selection Event
        private void rICheckedCB_Adj_CasterKPI_EditValueChanged(object sender, EventArgs e)
        {
            List<object> checkedItems = rICheckedCB_Adj_CasterKPI.Items.GetCheckedValues();
            

            setupChange_CV.Caster_KPI_Adj.Clear();

            for (int i = 0; i < checkedItems.Count; i++)
            {
                if (!setupChange_CV.Caster_KPI_Adj.ContainsKey(checkedItems[i].ToString()))
                {
                    setupChange_CV.Caster_KPI_Adj.Add(checkedItems[i].ToString(), new SetupChange_AdjToolParams(checkedItems[i].ToString(), 0, 10, -10, BitSize));

                }
            }


            if (checkedItems.Contains(AdjustmentTools.TopFrontArm))
            {
                rowTopFrontArm.Visible = true;
            }
            else
            {
                rowTopFrontArm.Visible = false;
            }
            if (checkedItems.Contains(AdjustmentTools.TopRearArm))
            {
                rowTopRearArm.Visible = true;
            }
            else
            {
                rowTopRearArm.Visible = false;
            }

            if (checkedItems.Contains(AdjustmentTools.BottomFrontArm))
            {
                rowBottomFrontArm.Visible = true;
            }
            else
            {
                rowBottomFrontArm.Visible = false;
            }

            if (checkedItems.Contains(AdjustmentTools.BottomRearArm))
            {
                rowBottomRearArm.Visible = true;
            }
            else
            {
                rowBottomRearArm.Visible = false;
            }

        }
        #endregion

        #region Camber Adjuster Selection Event
        private void rICheckedCB_Adj_Camber_EditValueChanged(object sender, EventArgs e)
        {
            List<object> checkeditems = rICheckedCB_Adj_Camber.Items.GetCheckedValues();

            setupChange_CV.Camber_Adj.Clear();

            for (int i = 0; i < checkeditems.Count; i++)
            {
                if (!setupChange_CV.Camber_Adj.ContainsKey(checkeditems[i].ToString()))
                {
                    setupChange_CV.Camber_Adj.Add(checkeditems[i].ToString(), new SetupChange_AdjToolParams(checkeditems[i].ToString(), 0, 5, -5, BitSize));
                }
            }

            if (checkeditems.Contains(AdjustmentTools.TopCamberMount))
            {
                rowTopCamberMount.Visible = true;
            }
            else
            {
                rowTopCamberMount.Visible = false;
            }

            if (checkeditems.Contains(AdjustmentTools.BottomCamberMount))
            {
                rowBottomCamberMount.Visible = true;
            }
            else
            {
                rowBottomCamberMount.Visible = false;
            }
        }
        #endregion

        #region Bump Steer Adjuster Selection Event
        private void rICheckedCB_Adj_BumpSteer_EditValueChanged(object sender, EventArgs e)
        {
            List<object> checkeditems = rICheckedCB_Adj_BumpSteer.Items.GetCheckedValues();

            setupChange_CV.BumpSteer_Adj.Clear();

            for (int i = 0; i < checkeditems.Count; i++)
            {
                if (!setupChange_CV.BumpSteer_Adj.ContainsKey(checkeditems[i].ToString()))
                {
                    setupChange_CV.BumpSteer_Adj.Add(checkeditems[i].ToString() + "_x", new SetupChange_AdjToolParams(checkeditems[i].ToString() + "_x", 232.12, 5, -5, BitSize));
                    setupChange_CV.BumpSteer_Adj.Add(checkeditems[i].ToString() + "_y", new SetupChange_AdjToolParams(checkeditems[i].ToString() + "_y", 124.4, 5, -5, BitSize));
                    setupChange_CV.BumpSteer_Adj.Add(checkeditems[i].ToString() + "_z", new SetupChange_AdjToolParams(checkeditems[i].ToString() + "_z", 60.8, 5, -5, BitSize));
                }
            }

            if (checkeditems.Contains(AdjustmentTools.ToeLinkInboardPoint))
            {
                rowToeLinkInboard_x.Visible = true;
                rowToeLinkInboard_y.Visible = true;
                rowToeLinkInboard_z.Visible = true;
            }
            else
            {
                rowToeLinkInboard_x.Visible = false;
                rowToeLinkInboard_y.Visible = false;
                rowToeLinkInboard_z.Visible = false;
                
            }
        }
        #endregion
        #endregion















        #region Not Needed Now
        private void rIComboBoxAdjustmentTypeCamber_Leave(object sender, EventArgs e)
        {
            string adjType = Convert.ToString(vGridControl1.GetCellValue(vGridControl1.FocusedRow, vGridControl1.FocusedRecord));
            //if (vGridControl1.FocusedRow == rowCamberChangeMethod)
            //{
            //    if (adjType == "Direct")
            //    {
            //        rowCamberAngle.Enabled = true;
            //        rowShims.Enabled = false;
            //        rowCamberMount.Enabled = false;
            //        rowNoOfShims.Enabled = false;
            //        rowShimThickness.Enabled = false;
            //        vGridControl1.SetCellValue(rowCamberMount, 1, null);
            //        vGridControl1.SetCellValue(rowNoOfShims, 1, null);
            //        vGridControl1.SetCellValue(rowShimThickness, 1, null);
            //        rowCamberMount.Properties.Value = null;
            //        rowNoOfShims.Properties.Value = null;
            //        rowShimThickness.Properties.Value = null;
            //    }
            //    else if (adjType == "Indirect")
            //    {
            //        rowShims.Enabled = true;
            //        rowNoOfShims.Enabled = true;
            //        rowShimThickness.Enabled = true;
            //        rowCamberMount.Enabled = true;
            //        rowCamberAngle.Enabled = false;
            //        vGridControl1.SetCellValue(rowCamberAngle, 1, null);
            //        rowCamberAngle.Properties.Value = null;
            //    }
            //}


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
                    rowPushrod.Visible = false;
                }
                else if (adjType == "Indirect")
                {
                    rowRideHeight.Enabled = false;
                    //rowDamperEyeToPerch.Enabled = true;
                    vGridControl1.SetCellValue(rowRideHeight, 1, null);
                    rowRideHeight.Properties.Value = null;
                    rowPushrod.Visible = true;
                    rowPushrod.Enabled = true;

                }
            }
        }
        #endregion

        #region NOT NEEDED NOW .Repository Wishbone Selecter Combobox Add/Remove Operations
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

    }
}

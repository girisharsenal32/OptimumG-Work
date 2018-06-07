using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Coding_Attempt_with_GUI
{
    public partial class BatchRunForm : XtraForm
    {
        Kinematics_Software_New R1;

        public List<LoadCase> BatchRunloadCases = new List<LoadCase>();



        /// <summary>
        /// Constructor
        /// </summary>
        public BatchRunForm()
        {
            InitializeComponent();

            R1 = Kinematics_Software_New.AssignFormVariable();
        }

        /// <summary>
        /// This method populates the <see cref="checkedListBoxLoadCases"/> with the default <see cref="LoadCase"/> and the ones created by the user S
        /// </summary>
        /// <param name="_loadCases"></param>
        public void PopulateBatchRunLoadCases(List<LoadCase> _loadCases)
        {
            checkedListBoxLoadCases.Items.Clear();
            checkedListBoxLoadCases.DataSource = null;
            checkedListBoxLoadCases.DataSource = _loadCases;
            checkedListBoxLoadCases.DisplayMember = "LoadCaseName";
        }

        private void BatchRun_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// <see cref="checkedListBoxLoadCases"/> Item check event which assigns or removes the checked or unchecked item from the <see cref="LoadCase.BatchRunloadCases"/> class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxLoadCases_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            LoadCase tempLoadCase = (LoadCase)checkedListBoxLoadCases.GetItem(e.Index);

            if (e.State == CheckState.Checked)
            {
                if (!BatchRunloadCases.Contains(tempLoadCase))
                {
                    BatchRunloadCases.Add(tempLoadCase);
                }
            }
            else
            {
                if (BatchRunloadCases.Contains(tempLoadCase))
                {
                    BatchRunloadCases.Remove(tempLoadCase);
                }

            }
        }

        /// <summary>
        /// Method to restore the checked state of all the items in the <see cref="checkedListBoxLoadCases"/>
        /// </summary>
        public void RestoreCheckState()
        {
            for (int i = 0; i < checkedListBoxLoadCases.ItemCount; i++)
            {
                LoadCase tempLoadCase = (LoadCase)checkedListBoxLoadCases.GetItem(i);
                if (BatchRunloadCases.Contains(tempLoadCase))
                {
                    checkedListBoxLoadCases.SetItemCheckState(i, CheckState.Checked);
                }
            }
        }


        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }



        /// <summary>
        /// Event which fires when the selected Index of the <see cref="comboBoxVehicleBatchRun"/> is changed. This event decides whether or not the <see cref="groupControlMotion"/> is to be hidden or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxVehicleBatchRun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!BatchRunGUI.batchRunBeingCreated)
            {
                GetVehicleItem();
            }



        }

        /// <summary>
        /// Method to get the Vehicle item from the <see cref="comboBoxVehicleBatchRun"/>. Called by the <see cref="comboBoxVehicleBatchRun_SelectedIndexChanged(object, EventArgs)"/> and the <see cref="SolveBatchRun_PrelimCheck(int)"/>
        /// </summary>
        public void GetVehicleItem()
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            int BatchRun_GUI_Index = R1.navBarGroupLoadCaseBatchRun.SelectedLinkIndex;

            if (comboBoxVehicleBatchRun.SelectedItem != null)
            {
                BatchRunGUI.batchRuns_GUI[BatchRun_GUI_Index].batchRunVehicle = (Vehicle)comboBoxVehicleBatchRun.SelectedItem;

                if (BatchRunGUI.batchRuns_GUI[BatchRun_GUI_Index].batchRunVehicle.sc_FL != null)
                {
                    if (!BatchRunGUI.batchRuns_GUI[BatchRun_GUI_Index].batchRunVehicle.sc_FL.SuspensionMotionExists)
                    {
                        groupControlMotion.Hide();
                    }
                    else
                    {
                        groupControlMotion.Show();
                    }
                }

            }


        }

        /// <summary>
        /// Event fired when the <see cref="simpleButtonRun"/> is clicked. This method runs the software for all the Load Cases selected in the Batch Run 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRun_Click(object sender, EventArgs e)
        {

            if (BatchRunloadCases.Count != 0)
            {
                R1 = Kinematics_Software_New.AssignFormVariable();


                int BatchRun_GUI_Index = R1.navBarGroupLoadCaseBatchRun.SelectedLinkIndex;

                ///<summary>Getting the vehicle item selected by the user in the combobox</summary>
                GetVehicleItem();

                if (SolveBatchRun_PrelimCheck(BatchRun_GUI_Index))
                {
                    ///<summary>Getting the Motion selected by the user</summary>
                    AssignMotion(BatchRun_GUI_Index);
                    toolStripProgressBar1.ProgressBar.Increment(25);
                    toolStripProgressBar1.ProgressBar.Update();

                    ///<summary>Solving for all the Load Cases and Assigning them into private and Public Dictionaries</summary>
                    BatchRun_Solver(BatchRun_GUI_Index);
                    toolStripProgressBar1.ProgressBar.Increment(25);
                    toolStripProgressBar1.ProgressBar.Update();

                    ///<summary>Output GUI Operations</summary>
                    BatchRun_OutputGUI(BatchRun_GUI_Index);

                    toolStripProgressBar1.ProgressBar.Increment(25);
                    toolStripProgressBar1.ProgressBar.Update();

                    this.Hide();

                }
                else
                {
                    return;
                } 
            }

            else
            {
                MessageBox.Show("Please Select Load Cases");
            }

        }

        /// <summary>
        /// This methid is the primary gate which decides that the selected item from the <see cref="BatchRunGUI.batchRuns_GUI"/> List is fully equipped to perform a Simulation
        /// </summary>
        /// <param name="_brIndex">Index of the <see cref="BatchRunGUI.batchRuns_GUI"/> obtained from the <see cref="Kinematics_Software_New.navBarGroupLoadCaseBatchRun"/></param>
        /// <returns></returns>
        private bool SolveBatchRun_PrelimCheck(int _brIndex)
        {
            String ErrorMessage;

            if (BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle != null)
            {
                if (BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.ValidateAssembly(out ErrorMessage))
                {
                    if (BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.sc_FL.SuspensionMotionExists && (Motion)comboBoxMotionBatchRun.SelectedItem == null)
                    {
                        MessageBox.Show("Please Create Motion Item ");
                        return false;
                    }
                    else
                    {
                        toolStripProgressBar1.ProgressBar.Increment(25);
                        toolStripProgressBar1.ProgressBar.Update();
                        return true;

                    }
                }
                else
                {
                    MessageBox.Show(/*"Please Create or Assemble Suspension Item Into Vehicle"*/ ErrorMessage);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Please Create Vehicle Item");
                return false;
            }
        }

        /// <summary>
        /// Based on the succes of the <see cref="SolveBatchRun_PrelimCheck(int)"/> method, this  method Assigns a <see cref="Motion"/> item from the <see cref="comboBoxMotionBatchRun"/> or null item 
        /// </summary>
        /// <param name="_brIndex">Index of the <see cref="BatchRunGUI.batchRuns_GUI"/> obtained from the <see cref="Kinematics_Software_New.navBarGroupLoadCaseBatchRun"/></param>
        private void AssignMotion(int _brIndex)
        {
            if (BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.sc_FL.SuspensionMotionExists)
            {
                BatchRunGUI.batchRuns_GUI[_brIndex].batchRunMotion = (Motion)comboBoxMotionBatchRun.SelectedItem;
                BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.vehicle_Motion = (Motion)comboBoxMotionBatchRun.SelectedItem;
                NoOfSteps = BatchRunGUI.batchRuns_GUI[_brIndex].batchRunMotion.Final_WheelDeflectionsX.Count;
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();
            }
            else if (!BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.sc_FL.SuspensionMotionExists)
            {
                BatchRunGUI.batchRuns_GUI[_brIndex].batchRunMotion = null;
                NoOfSteps = 1;
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();
            }
        }

        /// <summary>
        /// Method which performs the Batch Run Simulations. This method can be accessed only if <see cref="SolveBatchRun_PrelimCheck(int)"/> returns <see cref="true"/>
        /// </summary>
        /// <param name="brIndex">Index of the <see cref="BatchRunGUI.batchRuns_GUI"/> obtained from the <see cref="Kinematics_Software_New.navBarGroupLoadCaseBatchRun"/></param>
        private void BatchRun_Solver(int brIndex)
        {

            //ClearResidues(BatchRunloadCases[i_LoadCase].runResults_FL)

            ///<summary>Performing preliminary Operations similar to <see cref="Kinematics_Software_New.PreliminaryVehicleChecks"/></summary>
            BatchRun_PrelimOperations(brIndex);
            toolStripProgressBar1.ProgressBar.Increment(5);
            toolStripProgressBar1.ProgressBar.Update();

            ///<summary>Solving the 4 corners of the Suspension. In this solution the LoadCases is non existant and hence the component forces will only be due to Static Corner Weight</summary>
            BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.KinematicsInvoker(BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.sc_FL.SuspensionMotionExists, SimulationType.BatchRun);
            toolStripProgressBar1.ProgressBar.Increment(5);
            toolStripProgressBar1.ProgressBar.Update();

            ///<summary>Solving the Kinematics with a new Load Case every time </summary>
            for (int i_LoadCase = 0; i_LoadCase < BatchRunloadCases.Count; i_LoadCase++)
            {

                ClearResidues(BatchRunloadCases[i_LoadCase].runResults_FL, BatchRunloadCases[i_LoadCase].runResults_FR, BatchRunloadCases[i_LoadCase].runResults_RL, BatchRunloadCases[i_LoadCase].runResults_RR);

                ///<summary>Assigining the new LoadCase inside the <see cref="BatchRunloadCases"/> List</summary>
                BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase = null;
                BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase = BatchRunloadCases[i_LoadCase];
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                ///<summary>Computing the Forces due to this new Load Case </summary>
                BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.ComputeWheelLoads(BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                ///<summary>Solving for the Suspension Forces due to this Load Case. If the Suspension of the Vehicle has a motion then this Load Case would be solved for each percentage of Motion </summary>
                CalculateSuspensionForces(brIndex, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_FL_Fx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_FL_Fz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_FL_Fy, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_FL_Mx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_FL_Mz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_FL, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.sc_FL, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.spring_FL, BatchRunloadCases[i_LoadCase].runResults_FL);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                CalculateSuspensionForces(brIndex, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_FR_Fx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_FR_Fz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_FR_Fy, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_FR_Mx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_FR_Mz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_FR, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.sc_FR, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.spring_FR, BatchRunloadCases[i_LoadCase].runResults_FR);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                CalculateSuspensionForces(brIndex, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_RL_Fx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_RL_Fz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_RL_Fy, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_RL_Mx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_RL_Mz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_RL, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.sc_RL, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.spring_RL, BatchRunloadCases[i_LoadCase].runResults_RL);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                CalculateSuspensionForces(brIndex, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_RR_Fx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_RR_Fz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.TotalLoad_RR_Fy, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_RR_Mx, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.NSM_RR_Mz,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_RR, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.sc_RR, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.spring_RR, BatchRunloadCases[i_LoadCase].runResults_RR);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                ///<summary>Calculating the Bearing Cap Attachment forces for the ARB and Steering (steering forces calculated only for Front)</summary>
                CalculateBearingCapForces(BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleModel, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_FL, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_FR,
                                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.FL_BearingCoordinates, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.FR_BearingCoordinates, false,
                                          BatchRunloadCases[i_LoadCase].runResults_FL, BatchRunloadCases[i_LoadCase].runResults_FR);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                CalculateBearingCapForces(BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleModel, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_RL, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_RR,
                          BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.RL_BearingCoordinates, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.vehicleLoadCase.RR_BearingCoordinates, true,
                          BatchRunloadCases[i_LoadCase].runResults_RL, BatchRunloadCases[i_LoadCase].runResults_RR);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

                ///<summary>Calculating the Steering Colummn Bearing Forces</summary>
                CalculateSteeringColumnForces(brIndex, BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.oc_FL, BatchRunloadCases[i_LoadCase].runResults_FL);
                toolStripProgressBar1.ProgressBar.Increment(5);
                toolStripProgressBar1.ProgressBar.Update();

            }

            Kinematics_Software_New.M1_Global.vehicleGUI[BatchRunGUI.batchRuns_GUI[brIndex].batchRunVehicle.VehicleID - 1].ProgressBarVehicleGUI.Hide();
            toolStripProgressBar1.ProgressBar.Increment(5);
            toolStripProgressBar1.ProgressBar.Update();
        }

        int NoOfSteps = 1;
        /// <summary>
        /// Method to perform the Preliminary Operations required to Solve the Suspension similar to <see cref="Kinematics_Software_New.PreliminaryVehicleChecks"/>
        /// </summary>
        /// <param name="_brIndex"></param>
        private void BatchRun_PrelimOperations(int _brIndex)
        {
            int VIndex = BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.VehicleID - 1;

            R1 = Kinematics_Software_New.AssignFormVariable();

            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.vehicleLoadCase = new LoadCase();

            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.Vehicle_Results_Tracker = 0;

            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.SuspensionIsSolved = false;

            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.InitializeOutputClass(NoOfSteps);

            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.ChassisCornerMassCalculator();

            Kinematics_Software_New.M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI = ProgressBarSerialization.CreateProgressBar(Kinematics_Software_New.M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI, 800, 1);

            Kinematics_Software_New.M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI.AddProgressBarToRibbonStatusBar(R1, Kinematics_Software_New.M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI);

            Kinematics_Software_New.M1_Global.vehicleGUI[VIndex].ProgressBarVehicleGUI.Show();

            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.vehicleGUI = Kinematics_Software_New.M1_Global.vehicleGUI[VIndex];
            toolStripProgressBar1.ProgressBar.Increment(5);
            toolStripProgressBar1.ProgressBar.Update();
        }

        private void ClearResidues(BatchRunResults _resultsFL, BatchRunResults _resultsFR, BatchRunResults _resultsRL, BatchRunResults _resultsRR)
        {
            _resultsFL.ClearResidue();

            _resultsFR.ClearResidue();

            _resultsRL.ClearResidue();

            _resultsRR.ClearResidue();
        }
        /// <summary>
        /// Method to Calculate the Suspension Forces for each <see cref="OutputClass"/> object inside the  <see cref="List{T}"/> of <see cref="OutputClass"/> inside each of the corners of the <see cref="BatchRunGUI.batchRunVehicle"/>
        /// </summary>
        /// <param name="_brIndex"></param>
        /// <param name="latForce"></param>
        /// <param name="longorce"></param>
        /// <param name="vertForce"></param>
        /// <param name="mx"></param>
        /// <param name="mz"></param>
        /// <param name="oc"></param>
        /// <param name="scm"></param>
        /// <param name="spring"></param>
        private void CalculateSuspensionForces(int _brIndex, double latForce, double longorce, double vertForce, double mx, double mz, List<OutputClass> oc, SuspensionCoordinatesMaster scm, Spring spring, BatchRunResults runResults)
        {
            BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.DWSolver.AssignLoadCaseDW(latForce, longorce, vertForce, mx, mz);

            for (int i_V = 0; i_V < NoOfSteps; i_V++)
            {
                BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.DWSolver.CalculateSuspensionForces(oc, scm, spring, i_V);
                runResults.AssignSuspensionForces(oc[i_V]);
            }


            runResults.SortSuspensionForces(NoOfSteps);

        }

        /// <summary>
        /// Method to Calculate the Bearing Cap Attachment Forces for each <see cref="OutputClass"/> object inside the  <see cref="List{T}"/> of <see cref="OutputClass"/> inside each of the corners of the <see cref="BatchRunGUI.batchRunVehicle"/>
        /// </summary>
        /// <param name="_vmodel"></param>
        /// <param name="ocLeft"></param>
        /// <param name="ocRight"></param>
        /// <param name="coorLeft"></param>
        /// <param name="coorRight"></param>
        /// <param name="rear"></param>
        private void CalculateBearingCapForces(VehicleModel _vmodel, List<OutputClass> ocLeft, List<OutputClass> ocRight, double[,] coorLeft, double[,] coorRight, bool rear, BatchRunResults runResultsLeft, BatchRunResults runResultsRight)
        {
            for (int i_BC = 0; i_BC < NoOfSteps; i_BC++)
            {
                if (!rear)
                {
                    _vmodel.BoltedJoint_ARB_And_Rack(ocLeft, ocRight, i_BC, coorLeft, coorRight, true, ocLeft[i_BC].ToeLink_z, ocRight[i_BC].ToeLink_z, ocLeft[i_BC].ToeLink_y, ocRight[i_BC].ToeLink_y);
                    _vmodel.AssignRackForces(ocLeft, ocRight, i_BC);
                    runResultsLeft.AssignBearingCapForces_Rack(ocLeft[i_BC]);runResultsRight.AssignBearingCapForces_Rack(ocRight[i_BC]);


                    _vmodel.BoltedJoint_ARB_And_Rack(ocLeft, ocRight, i_BC, coorLeft, coorRight, false, ocLeft[i_BC].ARBDroopLink_z, ocRight[i_BC].ARBDroopLink_z, ocLeft[i_BC].ARBDroopLink_y, ocRight[i_BC].ARBDroopLink_y);
                    _vmodel.AssignARBForces(ocLeft, ocRight, i_BC);
                    runResultsLeft.AssignBearingCapForces_ARB(ocLeft[i_BC]); runResultsRight.AssignBearingCapForces_ARB(ocRight[i_BC]);

                }
                ///<summary>For Rear the boolean needs be <see cref="true"/> even though there is NO steering because the boolean is needed to acces the array of coordinates.
                ///<see cref="VehicleModel.BoltedJoint_ARB_And_Rack(List{OutputClass}, List{OutputClass}, int, double[,], double[,], bool, double, double, double, double)"/> to understand how the Boolean is used
                ///</summary>
                else if (rear)
                {
                    _vmodel.BoltedJoint_ARB_And_Rack(ocLeft, ocRight, i_BC, coorLeft, coorRight, true, ocLeft[i_BC].ARBDroopLink_z, ocRight[i_BC].ARBDroopLink_z, ocLeft[i_BC].ARBDroopLink_y, ocRight[i_BC].ARBDroopLink_y);
                    _vmodel.AssignARBForces(ocLeft, ocRight, i_BC);
                    runResultsLeft.AssignBearingCapForces_ARB(ocLeft[i_BC]); runResultsRight.AssignBearingCapForces_ARB(ocRight[i_BC]);
                }
            }

            if (!rear)
            {
                runResultsLeft.SortBearingCapForces_ARB(NoOfSteps);
                runResultsRight.SortBearingCapForces_ARB(NoOfSteps);

                runResultsLeft.SortBeatingCapForces_Rack(NoOfSteps);
                runResultsRight.SortBeatingCapForces_Rack(NoOfSteps);
            }
            else if (rear)
            {
                runResultsLeft.SortBearingCapForces_ARB(NoOfSteps);
                runResultsRight.SortBearingCapForces_ARB(NoOfSteps);
            }

        }

        /// <summary>
        /// Method to Calculate the Steering Column Forces for each <see cref="OutputClass"/> object inside the  <see cref="List{T}"/> of <see cref="OutputClass"/> inside each of the corners of the <see cref="BatchRunGUI.batchRunVehicle"/>
        /// </summary>
        /// <param name="_brIndex"></param>
        private void CalculateSteeringColumnForces(int _brIndex, List<OutputClass> ocSteering, BatchRunResults runResultsSteering)
        {
            for (int i_SC = 0; i_SC < NoOfSteps; i_SC++)
            {
                BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.vehicleModel.BoltedJoint_SteeringColumn(BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.oc_FL, BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.oc_FR, i_SC,
                                                                                               BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.vehicleLoadCase.SteeringColumnBearing);
                BatchRunGUI.batchRuns_GUI[_brIndex].batchRunVehicle.vehicleModel.AssignSteeringColumnForces(ocSteering, i_SC);
                runResultsSteering.AssignBearingCapForces_SteeringColumn(ocSteering[i_SC]);
            }

            runResultsSteering.SortBearingCapForces_SteeringColumn(NoOfSteps);

        }


        private void BatchRun_OutputGUI(int brIndex)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            BatchRunGUI.batchRuns_GUI[brIndex].CreateTabPages(BatchRunGUI.batchRuns_GUI[brIndex].TabPages_BatchRUn, BatchRunloadCases);

            Kinematics_Software_New.TabControl_Outputs = CustomXtraTabPage.AddTabPages(Kinematics_Software_New.TabControl_Outputs, BatchRunGUI.batchRuns_GUI[brIndex].TabPages_BatchRUn);

            DisplayBasicOutputs();

            BatchRunGUI.batchRuns_GUI[brIndex].BatchRun_NavBarGroupOperations(BatchRunGUI.batchRuns_GUI[brIndex], BatchRunloadCases);

            R1.ribbonPageGroupHeatMap.Enabled = true;

            R1.ribbon.SelectedPage = R1.ribbonPageResults;

            R1.navBarControl1.ActiveGroup = R1.navBarGroupResults;


        }

        /// <summary>
        /// Method to Display the HEAVIEST LOAD Experienced by each Wishbone or Bearing Cap and display it on the <see cref="XtraUserControl_WishboneForces"/> Usercontrol
        /// </summary>
        /// <param name="_resultsFL"></param>
        private void DisplayBasicOutputs()
        {
            BatchRunResults _resultsFL, _resultsFR, _resultsRL, _resultsRR;

            for (int i = 0; i < BatchRunloadCases.Count; i++)
            {
                int lcGUI = BatchRunloadCases[i].LoadCaseID - 1;

                _resultsFL = BatchRunloadCases[i].runResults_FL;
                _resultsFR = BatchRunloadCases[i].runResults_FR;
                _resultsRL = BatchRunloadCases[i].runResults_RL;
                _resultsRR = BatchRunloadCases[i].runResults_RR;
                
                #region Displaying the Wishbone Forces of the Front Left Corner
                //Calculating the Wishbone Forces
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Pushrod"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link"));

                //Chassic Pick Up Points in XYZ direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Pushrod Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Pushrod Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Pushrod X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.SpringPreloadOutputFL.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Damper Force X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkForceFL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Droop Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Toe Link X"));

                //Upper and Lower Ball Joint Forces in XYZ Direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightFLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightFLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightFLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Upper Ball Joint X"));

                ///<remarks>
                ///Steering Rack and ARB Attachment point forces in XYZ direction
                /// </remarks>
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard1x_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Steering Rack Cap Left Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard1y_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Steering Rack Cap Left Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard1z_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Steering Rack Cap Left X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard2x_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Steering Rack Cap Right Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard2y_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Steering Rack Cap Right Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard2z_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "Steering Rack Cap Right X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1x_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Bearing Cap Left Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1y_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Bearing Cap Left Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1z_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Bearing Cap Left X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2x_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Bearing Cap Right Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2y_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Bearing Cap Right Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2z_FL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFL, "ARB Bearing Cap Right X"));
                #endregion

                #region Displaying the Wishbone Forces of the Front Right Corner
                //Calculating the Wishbone Forces
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Pushrod"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link"));

                //Chassic Pick Up Points in XYZ direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Pushrod Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Pushrod Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Pushrod X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.SpringPreloadOutputFR.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Damper Force X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkForceFR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Droop Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Toe Link X"));

                //Upper and Lower Ball Joint Forces in XYZ Direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightFRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightFRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightFRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Upper Ball Joint X"));

                ///<remarks>
                ///Steering Rack and ARB Attachment point forces in XYZ direction
                /// </remarks>
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard1x_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Steering Rack Cap Left Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard1y_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Steering Rack Cap Left Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard1z_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Steering Rack Cap Left X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard2x_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Steering Rack Cap Right Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard2y_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Steering Rack Cap Right Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.RackInboard2z_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "Steering Rack Cap Right X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1x_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Bearing Cap Left Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1y_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Bearing Cap Left Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1z_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Bearing Cap Left X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2x_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Bearing Cap Right Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2y_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Bearing Cap Right Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2z_FR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsFR, "ARB Bearing Cap Right X"));
                #endregion

                #region Displaying the Wishbone Forces of the Rear Left Corner
                //Calculating the Wishbone Forces
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Pushrod"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link"));

                //Chassic Pick Up Points in XYZ direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Pushrod Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Pushrod Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Pushrod X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.SpringPreloadOutputRL.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Damper Force X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkForceRL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Droop Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Toe Link X"));

                //Upper and Lower Ball Joint Forces in XYZ Direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightRLx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightRLy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightRLz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "Upper Ball Joint X"));

                ///<remarks>
                ///ARB Attachment point forces in XYZ direction
                /// </remarks>
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1x_RL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Bearing Cap Left Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1y_RL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Bearing Cap Left Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1z_RL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Bearing Cap Left X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2x_RL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Bearing Cap Right Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2y_RL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Bearing Cap Right Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2z_RL.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRL, "ARB Bearing Cap Right X"));
                #endregion

                #region Displaying the Wishbone Forces of the Rear Right Corner
                //Calculating the Wishbone Forces
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Front Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Rear Wishbone"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Pushrod"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link"));

                //Chassic Pick Up Points in XYZ direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Front Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Front Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Front Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Rear Wishbone Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Rear Wishbone Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Rear Wishbone X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Pushrod Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Pushrod Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Pushrod X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.PushRodUprightRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.SpringPreloadOutputRR.Text = String.Format("{0:0.000}", Spring.Assy_Spring[0].SpringPreload * Spring.Assy_Spring[0].PreloadForce);
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DamperForceBellCrankRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Damper Force X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkForceRR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkBellCrankRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.DroopLinkLeverRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Droop Link X"));

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkChassisRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ToeLinkUprightRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Toe Link X"));

                //Upper and Lower Ball Joint Forces in XYZ Direction
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerFrontUprightRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.LowerRearUprightRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Lower Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperFrontUprightRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Ball Joint X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightRRx.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Ball Joint Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightRRy.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Ball Joint Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.UpperRearUprightRRz.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "Upper Ball Joint X"));

                ///<remarks>
                ///ARB Attachment point forces in XYZ direction
                /// </remarks>

                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1x_RR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Bearing Cap Left Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1y_RR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Bearing Cap Left Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard1z_RR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Bearing Cap Left X"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2x_RR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Bearing Cap Right Y"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2y_RR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Bearing Cap Right Z"));
                LoadCaseGUI.List_LoadCaseGUI[lcGUI].batchRun_WF.ARBInboard2z_RR.Text = String.Format("{0:0.000}", FindMaxDisplayOutput(_resultsRR, "ARB Bearing Cap Right X"));
                #endregion

            }
        }

        private double FindMaxDisplayOutput(BatchRunResults _results, string _opChannel)
        {
            double opChannelValue = 0;

            List<double> tempListOfOPChannels = new List<double>();


            foreach (string item in _results.OutputChannels[_opChannel].Keys)
            {
                tempListOfOPChannels.Add(_results.OutputChannels[_opChannel][item]);

            }

            tempListOfOPChannels.Sort();

            if (tempListOfOPChannels.Count != 0)
            {
                if (Math.Abs(tempListOfOPChannels[tempListOfOPChannels.Count - 1]) > Math.Abs(tempListOfOPChannels[0])) 
                {
                    opChannelValue = tempListOfOPChannels[tempListOfOPChannels.Count - 1];
                }
                else
                {
                    opChannelValue = tempListOfOPChannels[0];
                }
            }

            return opChannelValue;

        }

        private void simpleButtonSelectAllLoadCases_Click(object sender, EventArgs e)
        {
            if (checkedListBoxLoadCases.ItemCount != 0)
            {
                checkedListBoxLoadCases.CheckAll();
            }
        }
    }
}
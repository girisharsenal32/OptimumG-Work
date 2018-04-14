using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using devDept.Eyeshot.Translators;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Eyeshot;
using devDept.Graphics;

namespace Coding_Attempt_with_GUI
{
    public partial class XUC_ImportCAD : DevExpress.XtraEditors.XtraUserControl
    {
        public XUC_ImportCAD()
        {
            InitializeComponent();
            ///<remarks>Viewport Control initalized here in this way so that the viewport User control has an instance of this form. The instance of this form is needed to pass information of items which are selected in the viewport</remarks>
            importCADViewport = new CAD(this);
            importCADViewport.viewportLayout1.WorkCompleted += ViewportLayout1_WorkCompleted;
            xuC_CoordinateMap1.GetParentObject(this);
            /////<summary>
            /////Plotting the Vehicle's Suspension. This will be the first thing to happen 
            /////This will happen ONLY if a Suspension is created
            /////</summary>
            //PlotPreviewCAD();
            dockPanelPreviewCAD.ActiveChildChanged += DockPanelPreviewCAD_ActiveChildChanged;
            ///<summary>Don't need this now because if the user wants a different choice of Suspension then he will do it from the Vehicle Comboboxes</summary>
            groupControlVehicleCorners.Hide();
            //simpleButtonPlotSuspension.Hide();
        }



        public void AssignVehicleGUIObject(VehicleGUI _vGUI)
        {
            ///<summary>Assigning the VehicleGUI object</summary>
            vGUI = _vGUI;
        }



        #region Declarations

        /// <summary>
        /// Index which will identify which Object from the List of Front Left Suspension Coordinate should be used
        /// </summary>
        public int indexSusFL_Form = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Front Right Suspension Coordinate should be used
        /// </summary>
        public int indexSusFR_Form = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Rear Left Suspension Coordinate should be used
        /// </summary>
        public int indexSusRL_Form = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Rear Right Suspension Coordinate should be used
        /// </summary>
        public int indexSusRR_Form = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Front Left <see cref="WheelAlignment"/> should be used
        /// </summary>
        public int indexWAFL_Form = 0;
        /// <summary>
        /// Temp Front Left Wheel Alignmentt Object to handle WA Rotations. This will account for the fact that the <see cref="WheelAlignment"/> may or may not be created
        /// </summary>
        public WheelAlignment tempWAFL;
        /// <summary>
        /// Index which will identify which Object from the List of Front Right <see cref="WheelAlignment"/> should be used
        /// </summary>
        public int indexWAFR_Form = 0;
        /// <summary>
        /// Temp Rear Front Right Alignmentt Object to handle WA Rotations. This will account for the fact that the <see cref="WheelAlignment"/> may or may not be created
        /// </summary>
        public WheelAlignment tempWAFR;
        /// <summary>
        /// Index which will identify which Object from the List of Rear Left <see cref="WheelAlignment"/> should be used
        /// </summary>
        public int indexWARL_Form = 0;
        /// <summary>
        /// Temp Rear Left Wheel  Alignmentt Object to handle WA Rotations. This will account for the fact that the <see cref="WheelAlignment"/> may or may not be created
        /// </summary>
        public WheelAlignment tempWARL;
        /// <summary>
        /// Index which will identify which Object from the List of Rear Right <see cref="WheelAlignment"/> should be used
        /// </summary>
        public int indexWARR_Form = 0;
        /// <summary>
        /// Temp Rear Right Wheel  Alignmentt Object to handle WA Rotations. This will account for the fact that the <see cref="WheelAlignment"/> may or may not be created
        /// </summary>
        public WheelAlignment tempWARR;


        /// <summary>
        /// Boolean to determine if a Suspension Object is created
        /// </summary>
        public bool SuspensionIsCreated_Form;
        /// <summary>
        /// Boolean to determine if a Wheel Alignment Object is created
        /// </summary>
        public bool WAIsCreated_Form;
        /// <summary>
        /// Boolean to detmine if the user wants to use the created Suspension
        /// </summary>
        public bool UseCreatedSuspnsion_Form;
        /// <summary>
        /// Boolean to determine if the user wants to use the created Wheel Alignment
        /// </summary>
        public bool UseCreatedWA_Form;
        /// <summary>
        /// Boolean to determine if the user has imported the File or not
        /// </summary>
        public bool FileHasBeenImported_Form;
        /// <summary>
        /// Boolean to determine if the Wheel is to be Plotted. This value is determined based on whether the user is importing the N.S.M
        /// </summary>
        public bool PlotWheel_Form = true;
        /// <summary>
        /// Object of the ReadIGES Class
        /// </summary>
        public ReadFileAsync importedFile_Form;
        /// <summary>
        /// Object of the Main Form
        /// </summary>
        Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();
        /// <summary>
        /// Index of the VehicleGUI item of which this is a member
        /// </summary>
        public int index_VehicleGUI_Form = 0;
        /// <summary>
        /// Set to false once an Import has been made. Boolean to determine if the Form is being initilized. That is, this variable helps determine if Form has just been imported. Set to false once an Import has been made.
        /// </summary>
        public bool IsInitializing = true;
        /// <summary>
        /// Object of the CAD user control
        /// </summary>
        public CAD importCADViewport;
        /// <summary>
        /// Object of the VehicleGUI Class
        /// </summary>
        VehicleGUI vGUI; 
        


        #endregion

        #region Form LoadEvent
        private void XUC_ImportCAD_Load(object sender, EventArgs e)
        {
            dockPanelPreviewCAD.Controls.Add(importCADViewport);
            importCADViewport.Dock = DockStyle.Fill;
            importCADViewport.Show();
            importCADViewport.BringToFront();


        }
        #endregion

        #region Method to Initialize Variables
        /// <summary>
        /// Method to initialize the ImportCAD Form variables
        /// </summary>
        /// <param name="_suspensionIsCreated">Boolean to determine if a Suspension Object is created</param>
        /// <param name="_wAIsCreated">Boolean to determine if a Wheel Alignment Object is created</param>
        /// <param name="_susFL">List of Front Left Suspension Coordinate Objects</param>
        /// <param name="_susFR">List of Front Right Suspension Coordinate Objects</param>
        /// <param name="_susRL">List of Rear Left Suspension Coordinate Objects</param>
        /// <param name="_susRR">List of Rear Right Suspension Coordinate Objects</param>
        /// <param name="_wa">List of Wheel Alignment Objects</param>
        public void AssignFormVariables(bool _suspensionIsCreated, bool _wAIsCreated, int _index_vehicleGUI)
        {
            SuspensionIsCreated_Form = _suspensionIsCreated;
            WAIsCreated_Form = _wAIsCreated;
            index_VehicleGUI_Form = _index_vehicleGUI;
        }
        #endregion

        #region Suspension Combobox Operations 
        /// <summary>
        /// Based on whether SuspensionCoordinate Objects are created, this method adds strings to the comboBoxSuspension
        /// </summary>
        private void SuspensionComboboxOperations_Parent()
        {
            int index = comboBoxSuspension.SelectedIndex;
            comboBoxSuspension.Items.Clear();

            if (SuspensionIsCreated_Form)
            {
                UseCreatedSuspnsion_Form = true;
                comboBoxSuspension.Items.AddRange(new string[] { "Use Created Suspension", "Create New Suspension Using Mapping" });
                comboBoxSuspension.SelectedIndex = 0;
            }
            else
            {
                UseCreatedSuspnsion_Form = false;
                //comboBoxSuspension.Items.Add("Suspension not created");
                comboBoxSuspension.Items.AddRange(new string[] { "Use Created Suspension", "Create New Suspension Using Mapping" });
                comboBoxSuspension.SelectedIndex = 0;
                dockPanelMapSuspensionCoordinates.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }

            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    comboBoxSuspension.SelectedIndex = index;
                }
                else comboBoxSuspension.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
                try
                {
                    comboBoxSuspension.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    // This additional block is to deal with the scenarion that the Suspension has been imported with lesser number of items than what existed and hence, to resotre the combobox to 0 index value
                }
            }
            #endregion


        }

        /// <summary>
        /// Adds the Suspension Coordinate items to the corresponding Comboboxes of the ImportCAD Form
        /// </summary>
        private void SuspensionComboboxOperations_Children()
        {
            if ((string)comboBoxSuspension.SelectedItem == "Suspension not created" || (string)comboBoxSuspension.SelectedItem == "Create New Suspension Using Mapping")
            {
                UseCreatedSuspnsion_Form = false;
                groupControlVehicleCorners.Hide();
            }
            else if ((string)comboBoxSuspension.SelectedItem == "Use Created Suspension")
            {
                UseCreatedSuspnsion_Form = true;
                ///<summary>Don't need this now because if the user wants a different choice of Suspension then he will do it from the Vehicle Comboboxes</summary>
                //groupControlVehicleCorners.Show();
                simpleButtonPlotSuspension.Show();

                SuspensionComboboxOperations_Children_Helper(ref comboBoxSuspensionFL, 1);
                SuspensionComboboxOperations_Children_Helper(ref comboBoxSuspensionFR, 2);
                SuspensionComboboxOperations_Children_Helper(ref comboBoxSuspensionRL, 3);
                SuspensionComboboxOperations_Children_Helper(ref comboBoxSuspensionRR, 4);

            }
        }

        /// <summary>
        /// Helper class to populate and initalize the Suspension Comboboxes
        /// </summary>
        /// <param name="temp_Combobox">Combobox object passed by reference</param>
        /// <param name="_scmCB">Object of the Suspension Master Class</param>
        private void SuspensionComboboxOperations_Children_Helper(ref ComboBox temp_Combobox, int identifier)
        {
            #region Creating a Master List of Suspension Coordinate Items. Need to do this becauseI cannot pass a List of SuspensionCoordinateFront to a parameter which is a List of SUspensionCoordinateMaster
            List<SuspensionCoordinatesMaster> _scmCB = new List<SuspensionCoordinatesMaster>();

            if (identifier == 1)
            {
                for (int i = 0; i < SuspensionCoordinatesFront.Assy_List_SCFL.Count; i++)
                {
                    _scmCB.Add(SuspensionCoordinatesFront.Assy_List_SCFL[i]);
                }
            }
            else if (identifier == 2)
            {
                for (int i = 0; i < SuspensionCoordinatesFrontRight.Assy_List_SCFR.Count; i++)
                {
                    _scmCB.Add(SuspensionCoordinatesFrontRight.Assy_List_SCFR[i]);
                }
            }
            else if (identifier == 3)
            {
                for (int i = 0; i < SuspensionCoordinatesRear.Assy_List_SCRL.Count; i++)
                {
                    _scmCB.Add(SuspensionCoordinatesRear.Assy_List_SCRL[i]);
                }
            }
            else if (identifier == 4)
            {
                for (int i = 0; i < SuspensionCoordinatesRearRight.Assy_List_SCRR.Count; i++)
                {
                    _scmCB.Add(SuspensionCoordinatesRearRight.Assy_List_SCRR[i]);
                }
            } 
            #endregion

            int index = temp_Combobox.SelectedIndex;
            temp_Combobox.Items.Clear();

            for (int i = 0; i < _scmCB.Count; i++)
            {
                temp_Combobox.Items.Add(_scmCB[i]);
                temp_Combobox.DisplayMember = "_SCName";
            }

            #region Re-assigning the combobox selected item index
            try
            {
                if (index != -1)
                {
                    temp_Combobox.SelectedIndex = index;
                }
                else temp_Combobox.SelectedIndex = 0;
            }
            catch (Exception)
            {// To safeguard against Open command if there is no item in combobox
                try
                {
                    temp_Combobox.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    // This additional block is to deal with the scenarion that the Suspension has been imported with lesser number of items than what existed and hence, to resotre the combobox to 0 index value
                }
            }
            #endregion

        }
        #endregion

        #region Suspension Combobox Events
        /// <summary>
        /// ComboboxSuspension Index changed Operations. Fired when the index of the comboBoxSuspension is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSuspension_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox temp_CB = (ComboBox)sender;
            int index = temp_CB.SelectedIndex;

            if ((string)comboBoxSuspension.SelectedItem == "Create New Suspension Using Mapping")
            {
                UseCreatedSuspnsion_Form = false;
                groupControlVehicleCorners.Hide();
                simpleButtonPlotSuspension.Hide();
                dockPanelMapSuspensionCoordinates.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                DialogResult dialogResult =  MessageBox.Show("Clear Viewport?", "Clear Viewpot Command", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    importCADViewport.viewportLayout1.Clear();
                    importCADViewport.viewportLayout1.Refresh();
                }
                else { }
            }
            else if ((string)comboBoxSuspension.SelectedItem == "Use Created Suspension")
            {
                UseCreatedSuspnsion_Form = true;
                ///<summary>Don't need this now because if the user wants a different choice of Suspension then he will do it from the Vehicle Comboboxes</summary>
                //groupControlVehicleCorners.Show();
                simpleButtonPlotSuspension.Show();
                //dockPanelMapSuspensionCoordinates.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

        bool FLIsChanged = false, FRIsChanged = false, RLIsChanged = false, RRIsChanged = false;
        /// <summary>
        /// Fired when the index any of the 4 comboBoxes of the Suspension Corners is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleCornerSuspension_Left_Changed(object sender, EventArgs e)
        {

            if (!IsInitializing)
            {

                ComboBox temp_CB = (ComboBox)sender;
                int index = temp_CB.SelectedIndex;

                if (temp_CB.SelectedItem as SuspensionCoordinatesFront != null)
                {
                    indexSusFL_Form = temp_CB.SelectedIndex;
                    comboBoxSuspensionFR.SelectedIndex = indexSusFL_Form;
                    comboBoxSuspensionFR.Refresh();
                    indexSusFR_Form = indexSusFL_Form;
                    FLIsChanged = true;
                    FRIsChanged = false;

                }

                if (temp_CB.SelectedItem as SuspensionCoordinatesRear != null)
                {
                    indexSusRL_Form = temp_CB.SelectedIndex;
                }
                PlotPreviewCAD();
            }

        }

        private void VehicleCornerSuspension_Right_Changed(object sender, EventArgs e)
        {

            if (!IsInitializing)
            {
                ComboBox temp_CB = (ComboBox)sender;
                int index = temp_CB.SelectedIndex;

                if ((temp_CB.SelectedItem as SuspensionCoordinatesFrontRight != null /*&& !FLIsChanged*/))
                {
                    indexSusFR_Form = temp_CB.SelectedIndex;
                    comboBoxSuspensionFL.SelectedIndex = indexSusFR_Form;
                    comboBoxSuspensionFL.Refresh();
                    indexSusFL_Form = indexSusFR_Form;
                    FRIsChanged = true;
                    FLIsChanged = false;
                }

                if (temp_CB.SelectedItem as SuspensionCoordinatesRearRight != null)
                {
                    indexSusRR_Form = temp_CB.SelectedIndex;
                }
            }
        }
        #endregion

        #region Wheel Alignment Combobox Operations
        /// <summary>
        /// Based on whether Wheel Alignment Objects are created, this method adds strings to the comboBoxWA
        /// </summary>
        private void WAComboboxOperations_Parent()
        {
            if (WAIsCreated_Form)
            {
                UseCreatedWA_Form = true;
                comboBoxWA.Items.AddRange(new string[] { "Use Created Alignment", "Do Not use created Alignment" });
                comboBoxWA.SelectedIndex = 0;
            }
            else
            {
                UseCreatedWA_Form = false;
                comboBoxWA.Items.Add("Alignment not created");
                comboBoxWA.SelectedIndex = 0;
            }

        }



        /// <summary>
        /// Adds the Wheel Alignment items to the corresponding Comboboxes of the ImportCAD Form
        /// </summary>
        private void WAComboboxOperations_Children()
        {
            if ((string)comboBoxWA.SelectedItem == "Alignment not created" || (string)comboBoxWA.SelectedItem == "Do Not use created Alignment")
            {
                UseCreatedWA_Form = false;
                groupControlVehicleEnds.Hide();
            }
            else if ((string)comboBoxWA.SelectedItem == "Use Created Alignment")
            {
                UseCreatedWA_Form = false;
                groupControlVehicleEnds.Show();
                for (int i = 0; i < WheelAlignment.Assy_List_WA.Count; i++)
                {
                    WAComboboxOperations_Children_Helper(ref comboBoxWAFront, WheelAlignment.Assy_List_WA[i]);
                    WAComboboxOperations_Children_Helper(ref comboBoxWARear, WheelAlignment.Assy_List_WA[i]);
                }
            }
        }

        /// <summary>
        /// Helper Class to initialize the Wheel Alignment Comboboxes
        /// </summary>
        /// <param name="temp_Combobox">Combobox Object passed by reference</param>
        /// <param name="_waCB">Object of the Wheel Alignment Class</param>
        private void WAComboboxOperations_Children_Helper(ref ComboBox temp_Combobox, WheelAlignment _waCB)
        {
            temp_Combobox.Items.Add(_waCB);
            temp_Combobox.DisplayMember = "_WAName";

            temp_Combobox.SelectedIndex = 0;
        }
        #endregion

        #region Wheel Alignment Combobox Events
        /// <summary>
        /// Fired when the index of any of the 2 comboboxes of the Wheel Alignment Ends is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox temp_CB = (ComboBox)sender;
            int index = temp_CB.SelectedIndex;

            if ((string)temp_CB.SelectedItem == "Do Not use created Alignment")
            {
                UseCreatedWA_Form = false;
                groupControlVehicleEnds.Hide();
            }
            else if ((string)temp_CB.SelectedItem == "Use Created Alignment")
            {
                UseCreatedWA_Form = true;
                groupControlVehicleEnds.Show();
            }
        }

        private void VehicleEndWA_Changed(object sender, EventArgs e)
        {
            ComboBox temp_CB = (ComboBox)sender;
            int index = temp_CB.SelectedIndex;
        }

        #endregion

        #region ImportCAD Combobox Operations
        /// <summary>
        /// Adds the necessary text to teh ImprotCad Comboboces
        /// </summary>
        private void ImportCADComboboxOperations()
        {
            if (UseCreatedSuspnsion_Form)
            {
                comboBoxImportCAD.Items.AddRange(new string[] { "Import Suspended Mass", "Import Non-Suspended Mass", "Import Suspended and Non-Suspended Mass"/*, "Do not import"*/ });
                comboBoxImportCAD.SelectedIndex = 0;
            }
            else
            {
                comboBoxImportCAD.Items.AddRange(new string[] { "Import Suspended Mass", "Import Non-Suspended Mass", "Import Suspended and Non-Suspended Mass"/*, "Do not import"*/ });
                comboBoxImportCAD.SelectedIndex = 0;
            }
            ///<remarks>
            ///Both the IF loops have the exact same condition. This is because I want to remind myself that initially if the User didn't want to use the created suspension or if a Suspension was not creaated at all then he should import a CAD file 
            ///which has both the SM and NSM. This is ofcourse stupid. He can Import the SM and NSM seperately. 
            /// </remarks>

        }
        #endregion

        #region ImportCAD Combobox Events
        /// <summary>
        /// Fired when the index of the ImportCAD Combobox is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxImportCAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBoxImportCAD.SelectedItem == "Import Suspended Mass" || (string)comboBoxImportCAD.SelectedItem == "Do not import")
            {
                PlotWheel_Form = true;
            }
            else
            {
                PlotWheel_Form = false;
                //groupControlMap.Show();
            }
            MapComboboxOperations();
            Kinematics_Software_New.M1_Global.vehicleGUI[index_VehicleGUI_Form].PlotWheel = PlotWheel_Form;
        }
        #endregion

        #region Map Combobox Operations
        /// <summary>
        ///Adds the necessary text to the Map Comboboxes
        /// </summary>
        private void MapComboboxOperations()
        {
            if ((string)comboBoxImportCAD.SelectedItem == "Import Suspended Mass")
            {
                /////<remarks>
                /////If only Suspended Mass is imported, then the NSM is drawn and hence doesn't need to be mapped
                /////</remarks>
                //groupControlMap.Hide();
                groupControlMap.Show();
                comboBoxMap.Items.Clear();
                comboBoxMap.Items.Add("Suspended Mass");
                comboBoxMap.SelectedIndex = 0;
            }
            else if ((string)comboBoxImportCAD.SelectedItem == "Import Non-Suspended Mass")
            {

                groupControlMap.Show();
                comboBoxMap.Items.Clear();
                comboBoxMap.Items.AddRange(new string[] { "Front Left Non Suspended Mass", "Front Right Non Suspended Mass", "Rear Left Non Suspended Mass", "Rear Right Non Suspended Mass" });
                comboBoxMap.SelectedIndex = 0;
            }
            else if ((string)comboBoxImportCAD.SelectedItem == "Import Suspended and Non-Suspended Mass")
            {
                groupControlMap.Show();
                comboBoxMap.Items.Clear();
                comboBoxMap.Items.AddRange(new string[] { "Suspended Mass", "Front Left Non Suspended Mass", "Front Right Non Suspended Mass", "Rear Left Non Suspended Mass", "Rear Right Non Suspended Mass" });
                comboBoxMap.SelectedIndex = 0;
            }
        }
        #endregion

        /// <summary>
        /// Initializes the ImportCAD Form; all the comboboxes are initialized based using the Suspension and Wheel Alignment items (if they are created)
        /// </summary>
        /// <param name="_SuspensionIsCreated"></param>
        /// <param name="_SusFL">List of Front Left Suspension Coordinate Objects</param>
        /// <param name="_SusFR">List of Front Right Suspension Coordinate Objects</param>
        /// <param name="_SusRL">List of Rear Left Suspension Coordinate Objects</param>
        /// <param name="_SusRR">List of Rear Right Suspension Coordinate Objects</param>
        public void InitializeForm()
        {
            SuspensionComboboxOperations_Parent();
            SuspensionComboboxOperations_Children();
            WAComboboxOperations_Parent();
            WAComboboxOperations_Children();
            ImportCADComboboxOperations();
            MapComboboxOperations();
            IsInitializing = false;
        }

        #region UndoButton Click Event
        private void simpleButtonUndoImport_Click(object sender, EventArgs e)
        {
            if (importedFile_Form != null)
            {
                if (importCADViewport.viewportLayout1.Entities.Count != 0)
                {
                    for (int i = 0; i < importedFile_Form.Entities.Count(); i++)
                    {
                        if (importCADViewport.viewportLayout1.Entities.Count != 0 && importCADViewport.viewportLayout1.Entities.Contains(importedFile_Form.Entities[i]))
                        {
                            importCADViewport.viewportLayout1.Entities.Remove(importedFile_Form.Entities[i]);
                        }
                    } 
                }

                importCADViewport.viewportLayout1.Refresh();
                importedFile_Form = null;
            }
        }
        #endregion

        #region Import File Operations
        /// <summary>
        /// Method to initialize the object of the OpenFileDialog Class
        /// </summary>
        /// <param name="_openCAD">Object ofthe OpenFileDialog Class</param>
        /// <returns>Initalized object of the OpenFileDialogClass</returns>
        private OpenFileDialog InitializeOpenFileFialog(OpenFileDialog _openCAD)
        {
            _openCAD = new OpenFileDialog();
            _openCAD.Filter = "IGES Files (*.igs)|*.igs";
            _openCAD.FilterIndex = 2;
            _openCAD.RestoreDirectory = true;

            return _openCAD;
        }

        /// <summary>
        /// Method to the Import the File using the Dialog Box
        /// </summary>
        private void ImportFile()
        {
            OpenFileDialog OpenCAD = new OpenFileDialog();
            OpenCAD = InitializeOpenFileFialog(OpenCAD);
            importCADViewport.openFileDialog1 = InitializeOpenFileFialog(importCADViewport.openFileDialog1);

            if (OpenCAD.ShowDialog() == DialogResult.OK)
            {
                importedFile_Form = new ReadIGES(OpenCAD.FileName);
                ///<summary> <c>importCADViewport.openFileDialog1.FileName</c> Assigned here so that during the Import Operation it is not empty. <seealso cref="VehicleGUI.InputDrawer(CAD, int, bool, bool, bool)"/> </summary>
                importCADViewport.openFileDialog1.FileName = OpenCAD.FileName;
                importCADViewport.viewportLayout1.StartWork(importedFile_Form);
                importCADViewport.viewportLayout1.SetView(viewType.Trimetric, true, importCADViewport.viewportLayout1.AnimateCamera);



            }
        }

        /// <summary>
        /// Brows button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImport_Click(object sender, EventArgs e)
        {
            FileHasBeenImported_Form = false;

            ///<remarks> Decided that when the file is imported into the CAD. the existing imported file Should NOT be removed. This is because the user can import both SM and NSM together or imported them seperately </remarks>
            /////<summary>Clearing the viewport of all entities including any entities which may be imported</summary>
            //importCADViewport.ClearViewPort(false, false);

            ///<summary>Imported a File based if the user wants to </summary>
            if ((string)comboBoxImportCAD.SelectedItem != "Do not import")
            {
                ///<summary>Initializing the Layers of the Eyeshot ViewportLayout</summary>
                //importCADViewport.InitializeLayers();

                ImportFile();



            }
            else
            {
            }
            ///<summary>Form is not initialzing anymore</summary>
            IsInitializing = false;
            /////<summary>Plotting the Vehicle's Suspension. This will happen irrespective of whether the user Imports a CAD model</summary>
            //PlotPreviewCAD();

        }

        /// <summary>
        /// devDept Library method to add the imported entites to the viewPortLayout. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewportLayout1_WorkCompleted(object sender, WorkCompletedEventArgs e)
        {

            ///<summary>Reading the IGES File Asynchronously</summary>
            importedFile_Form = (ReadFileAsync)e.WorkUnit;

            ///<summary>Adding the entities to the Viewport</summary>
            //importedFile_Form.AddToScene(importCADViewport.viewportLayout1, null,false);

            importCADViewport.viewportLayout1.Layers.Add(new Layer("ImportedFile"));

            importedFile_Form.AddToSceneAsSingleObject(importCADViewport.viewportLayout1, "VehicleCAD", "ImportedFile");

            ///<summary>Passing the igesEntity variable information to an igesEntities variable to facilitate biased clearing of items <seealso cref="CAD.ClearViewPort(bool, bool)"/></summary>
            importCADViewport.igesEntities = importedFile_Form.Entities;

            /////<summary>Setting Zoom</summary>
            importCADViewport.viewportLayout1.ZoomFit();

            ///<summary>Updating the ViewPort</summary>
            importCADViewport.viewportLayout1.UpdateViewportLayout();

            ///<summary>Assigning the Boolean variable</summary>
            FileHasBeenImported_Form = true;vGUI.FileHasBeenImported = true;

            ///<summary>Reversing the Entities to make the imported files in the begining of the Entity List</summary>
            importCADViewport.viewportLayout1.Entities.Reverse();
            /////<summary>Clearing all the Blocks which are generated during the importing. This is done so that the SM and each of the NSM can be added as an Individual Block</summary>
            //importCADViewport.viewportLayout1.Blocks.Clear();
            //AssignImportedFileBlocks();

        }
        #endregion 

        /// <summary>
        /// Method to obtain the Indices of the Suspension Items in the <see cref="List{T}"/> of Suspension Items by gettin the ID of the corresponding Vehicle's Suspension Items 
        /// </summary>
        private void GetSuspensionItemIndex()
        {
            R1 = Kinematics_Software_New.AssignFormVariable();
            int Vindex = R1.navBarGroupVehicle.SelectedLinkIndex;
            indexSusFL_Form = Vehicle.List_Vehicle[Vindex].sc_FL.SCFL_ID - 1;
            indexSusFR_Form = Vehicle.List_Vehicle[Vindex].sc_FR.SCFR_ID - 1;
            indexSusRL_Form = Vehicle.List_Vehicle[Vindex].sc_RL.SCRL_ID - 1;
            indexSusRR_Form = Vehicle.List_Vehicle[Vindex].sc_RR.SCRR_ID - 1;
        }

        /// <summary>
        /// Method to get the Wheel Alignment of the Vehicle's Corners
        /// </summary>
        private void GetWheelAlignment()
        {
            R1 = Kinematics_Software_New.AssignFormVariable();
            int Vindex = R1.navBarGroupVehicle.SelectedLinkIndex;
            if (Vehicle.List_Vehicle[Vindex].wa_FL != null)
            {
                indexWAFL_Form = Vehicle.List_Vehicle[Vindex].wa_FL.WheelAlignmentID - 1;
                if (indexWAFL_Form != -1)
                {
                    tempWAFL = Vehicle.List_Vehicle[Vindex].wa_FL;
                }
            }
            if (Vehicle.List_Vehicle[Vindex].wa_FR != null)
            {
                indexWAFR_Form = Vehicle.List_Vehicle[Vindex].wa_FR.WheelAlignmentID - 1;
                if (indexWAFR_Form != -1)
                {
                    tempWAFR = Vehicle.List_Vehicle[Vindex].wa_FR;
                }
            }
            if (Vehicle.List_Vehicle[Vindex].wa_RL != null)
            {
                indexWARL_Form = Vehicle.List_Vehicle[Vindex].wa_RL.WheelAlignmentID - 1;
                if (indexWARL_Form != -1)
                {
                    tempWARL = Vehicle.List_Vehicle[Vindex].wa_RL;
                }
            }
            if (Vehicle.List_Vehicle[Vindex].wa_RR != null)
            {
                indexWARR_Form = Vehicle.List_Vehicle[Vindex].wa_RR.WheelAlignmentID - 1;
                if (indexWARR_Form != -1)
                {
                    tempWARR = Vehicle.List_Vehicle[Vindex].wa_RR;
                }
            }
        }

        #region Plot Preview CAD
        /// <summary>
        /// Method to Plot the CAD on the Preview Pane
        /// </summary>
        private void PlotPreviewCAD()
        {
            R1 = Kinematics_Software_New.AssignFormVariable();
            ///<summary>
            ///Clearing the Viewport selectively <seealso cref="CAD.ClearViewPort(bool, bool)"/>
            /// </summary>
            importCADViewport.ClearViewPort(true, FileHasBeenImported_Form, Kinematics_Software_New.M1_Global.vehicleGUI[R1.navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.igesEntities);
            ///<summary>Initializing the Layers</summary>
            importCADViewport.InitializeLayers();

            ///<summary>Getting the Suspension Item IDs of the Vehicle's Suspension Items</summary>
            GetSuspensionItemIndex();

            GetWheelAlignment();


            if (SuspensionCoordinatesFront.Assy_List_SCFL.Count != 0)
            {
                ///<summary>Plotting Front Left Points</summary>
                importCADViewport.SuspensionPlotterInvoker(SuspensionCoordinatesFront.Assy_List_SCFL[indexSusFL_Form], 1, /*null*/tempWAFL, true, PlotWheel_Form, null, 0, 0, 0);
                ///<summary>Plotting Front Right Points</summary>
                importCADViewport.SuspensionPlotterInvoker(SuspensionCoordinatesFrontRight.Assy_List_SCFR[indexSusFR_Form], 2, /*null*/tempWAFR, true, PlotWheel_Form, null, 0, 0, 0);
                ///<summary>Plotting Rear Left Points</summary>
                importCADViewport.SuspensionPlotterInvoker(SuspensionCoordinatesRear.Assy_List_SCRL[indexSusRL_Form], 3, /*null*/ tempWARL, true, PlotWheel_Form, null, 0, 0, 0);
                ///<summary>Plotting Rear Right Points</summary>
                importCADViewport.SuspensionPlotterInvoker(SuspensionCoordinatesRearRight.Assy_List_SCRR[indexSusRR_Form], 4, /*null*/ tempWARR, true, PlotWheel_Form, null, 0, 0, 0);
                ///<summary>Plotting Front ARB</summary>
                importCADViewport.ARBConnector(importCADViewport.CoordinatesFL.InboardPickUp, importCADViewport.CoordinatesFR.InboardPickUp);
                ///<summary>Plotting Rear ARB</summary>
                importCADViewport.ARBConnector(importCADViewport.CoordinatesRL.InboardPickUp, importCADViewport.CoordinatesRR.InboardPickUp);
                ///<summary>Plotting Steering Column</summary>
                importCADViewport.SteeringCSystemPlotter(SuspensionCoordinatesFront.Assy_List_SCFL[indexSusFL_Form], SuspensionCoordinatesFrontRight.Assy_List_SCFR[indexSusFR_Form],
                                                                                  importCADViewport.CoordinatesFL.InboardPickUp, importCADViewport.CoordinatesFR.InboardPickUp);
            }
            ///<summary>Refreshing the ViewPort</summary>
            importCADViewport.RefreshViewPort();

        }
        #endregion

        #region Export Button Click Event and Export Operations
        /// <summary>
        /// This method plots the Previewed Vehicle and Suspension Items into the actual CAD Usercontrol of the VehicleGUI class
        /// It also creates a new Vehicle Item and a corresponding VehicleGUI item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            ///<summary>Finding the Index of the Vehicle which called the ImportCAD Form</summary>
            int indexVehicle = R1.navBarGroupVehicle.SelectedLinkIndex;

            ///<summary>Ensuring that the Suspension is created before proceeding with any Suspension Modification</summary>
            if (SuspensionIsCreated_Form)
            {
                ///<summary>Obtaining the indices of each of Suspension Items selected by the Vehicle</summary>
                R1 = Kinematics_Software_New.AssignFormVariable();
                indexSusFL_Form = R1.comboBoxSCFL.SelectedIndex;
                indexSusFR_Form = R1.comboBoxSCFR.SelectedIndex;
                indexSusRL_Form = R1.comboBoxSCRL.SelectedIndex;
                indexSusRR_Form = R1.comboBoxSCRR.SelectedIndex;

                ///<summary>
                ///Chanding the values in the Suspension's Grid View Tabl
                /// </summary>
                importCADViewport.ModifySuspensionItem(indexSusFL_Form, indexSusFR_Form, indexSusRL_Form, indexSusRR_Form);

                ///<summary>Checking if the Left and Right Coordinates are still the same. If they are symmetry is not changed. Otherwise symmetry is set to false</summary>
                R1.scflGUI[indexSusFL_Form].FrontSymmetryGUI = importCADViewport.CheckSymmetry(importCADViewport.CoordinatesFL, importCADViewport.CoordinatesFR, R1.scflGUI[indexSusFL_Form].FrontSymmetryGUI);
                R1.scfrGUI[indexSusFR_Form].FrontSymmetryGUI = importCADViewport.CheckSymmetry(importCADViewport.CoordinatesFL, importCADViewport.CoordinatesFR, R1.scflGUI[indexSusFL_Form].FrontSymmetryGUI);
                R1.scrlGUI[indexSusRL_Form].RearSymmetryGUI = importCADViewport.CheckSymmetry(importCADViewport.CoordinatesRL, importCADViewport.CoordinatesRR, R1.scrlGUI[indexSusRL_Form].RearSymmetryGUI);
                R1.scrrGUI[indexSusRR_Form].RearSymmetryGUI = importCADViewport.CheckSymmetry(importCADViewport.CoordinatesRL, importCADViewport.CoordinatesRR, R1.scrlGUI[indexSusRL_Form].RearSymmetryGUI);

                ///<summary>Editing the all the Suspension Items </summary>
                R1.ModifyFrontLeftSuspension(false, indexSusFL_Form);
                R1.ModifyFrontRightSuspension(false, indexSusFR_Form);
                R1.ModifyRearLeftSuspension(false, indexSusRL_Form);
                R1.ModifyRearRightSuspension(false, indexSusRR_Form);
            }

            string vehicleName = Vehicle.List_Vehicle[indexVehicle]._VehicleName;
            ///<summary>Saving Imported File so it can be exported into the VehicleGUI CAD's Viewport</summary>
            importCADViewport.viewportLayout1.SaveScene(vehicleName);

            ///<summary>Passing the ImportedCAD information to the VehicleGUI Object</summary>
            ///<remarks> NOT REALLY USEFUL. NEED TO EVALUATE </remarks>
            R1.AssignIgesEntities(this, indexVehicle);
            //this.Dispose();
            ////Kinematics_Software_New.M1_Global.vehicleGUI[indexVehicle].CADVehicleInputs.viewportLayout1.Clear();
            //Kinematics_Software_New.EditVehicleCAD(Kinematics_Software_New.M1_Global.vehicleGUI[indexVehicle].CADVehicleInputs, indexVehicle, true, Kinematics_Software_New.M1_Global.vehicleGUI[indexVehicle].CadIsTobeImported, PlotWheel_Form);
            //Kinematics_Software_New.M1_Global.vehicleGUI[indexVehicle].CADVehicleInputs.viewportLayout1.LoadScene(vehicleName);

            //R1.CreateNewVehicleItem();

        }
        #endregion

        #region Mouse Click Event for listBoxSelectedParts
        int SelectedPartIndex;
        /// <summary>
        /// Fires when the Mouse is clicked inside the the Selected Part Listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxSelectedParts_MouseClick(object sender, MouseEventArgs e)
        {
            ///<summary>
            ///De-Selecting all the selected items in the viewport
            ///Doing it through the for loop as shown below doesn't trigger the <see cref="CAD.ViewportLayout1_SelectionChanged(object, ViewportLayout.SelectionChangedEventArgs)\"/> event!!
            /// </summary>
            for (int i = 0; i < importCADViewport.viewportLayout1.Entities.Count; i++)
            {
                importCADViewport.viewportLayout1.Entities[i].Selected = false;
            }
            ///<summary>
            ///Checking if the preious index is same as the new list box selected index. If this is true then either the same item is clicked again or the white area of the list box is selected. In both cases I want 
            ///the selection to be wiped out. 
            /// </summary>
            if (SelectedPartIndex == listBoxSelectedParts.SelectedIndex)
            {
                listBoxSelectedParts.SelectedIndex = -1;
                SelectedPartIndex = -1;
            }
            else
            {
                SelectedPartIndex = listBoxSelectedParts.SelectedIndex;
            }

            ///<summary>Individually selecting the item which the user has selected from the list box</summary>
            if (SelectedPartIndex != -1)
            {
                Entity tempEntity = (Entity)importCADViewport.SelectedEntityList[SelectedPartIndex].Item;

                importCADViewport.viewportLayout1.Entities[importCADViewport.viewportLayout1.Entities.IndexOf(tempEntity)].Selected = true;
            }
            else
            {

            }

            importCADViewport.viewportLayout1.Refresh();

        }
        #endregion

        #region Mouse Click Event fo listBoxMappedParts
        int MappedPartIndex;
        /// <summary>
        /// Select all the entites inside a Mapped Block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxControlMappedParts_MouseClick(object sender, MouseEventArgs e)
        {
            ///<summary>
            ///De-Selecting all the selected items in the viewport
            ///Doing it through the for loop as shown below doesn't trigger the <see cref="CAD.ViewportLayout1_SelectionChanged(object, ViewportLayout.SelectionChangedEventArgs)\"/> event!!
            /// </summary>
            for (int i = 0; i < importCADViewport.viewportLayout1.Entities.Count; i++)
            {
                importCADViewport.viewportLayout1.Entities[i].Selected = false;
            }

            ///<summary>
            ///Checking if the preious index is same as the new list box selected index. If this is true then either the same item is clicked again or the white area of the list box is selected. In both cases I want 
            ///the selection to be wiped out. 
            /// </summary>
            if (MappedPartIndex == listBoxControlMappedParts.SelectedIndex)
            {
                listBoxControlMappedParts.SelectedIndex = -1;
                MappedPartIndex = -1;
            }
            else
            {
                MappedPartIndex = listBoxControlMappedParts.SelectedIndex;
            }

            ///<summary>Individually selecting the BLOCK which the user has selected from the list box</summary>
            if (MappedPartIndex != -1)
            {
                if ((string)listBoxControlMappedParts.SelectedItem == "Suspended Mass")
                {
                    if (importCADViewport.viewportLayout1.Blocks.Contains("SuspendedMass"))
                    {
                        for (int i = 0; i < importCADViewport.viewportLayout1.Blocks["SuspendedMass"].Entities.Count; i++)
                        {
                            importCADViewport.viewportLayout1.Blocks["SuspendedMass"].Entities[i].Selected = true;
                        }
                    }
                }
                else if ((string)listBoxControlMappedParts.SelectedItem == "Front Left Non Suspended Mass")
                {
                    if (importCADViewport.viewportLayout1.Blocks.Contains("Front Left Non Suspended Mass"))
                    {
                        for (int i = 0; i < importCADViewport.viewportLayout1.Blocks["Front Left Non Suspended Mass"].Entities.Count; i++)
                        {
                            importCADViewport.viewportLayout1.Blocks["Front Left Non Suspended Mass"].Entities[i].Selected = true;
                        }
                    }
                }
                else if ((string)listBoxControlMappedParts.SelectedItem == "Front Right Non Suspended Mass")
                {
                    if (importCADViewport.viewportLayout1.Blocks.Contains("Front Right Non Suspended Mass"))
                    {
                        for (int i = 0; i < importCADViewport.viewportLayout1.Blocks["Front Right Non Suspended Mass"].Entities.Count; i++)
                        {
                            importCADViewport.viewportLayout1.Blocks["Front Right Non Suspended Mass"].Entities[i].Selected = true;
                        }
                    }
                }
                else if ((string)listBoxControlMappedParts.SelectedItem == "Rear Left Non Suspended Mass")
                {

                }
                else if ((string)listBoxControlMappedParts.SelectedItem == "Rear Right Non Suspended Mass")
                {

                }
            }

            importCADViewport.viewportLayout1.Refresh();

        }
        #endregion

        private void simpleButtonSelectItems_Click(object sender, EventArgs e)
        {
            importCADViewport.viewportLayout1.ActionMode = actionType.SelectVisibleByPick;
            importCADViewport.viewportLayout1.MultipleSelection = true;

        }

        /// <summary>
        /// Method to enable Individual Selection of the Entities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSetCurrent_Click(object sender, EventArgs e)
        {
            ///<summary>Setting the Imported Files to Current so that the user can select them individually</summary>
            importCADViewport.SetCurrent();

        }

        private void simpleButtonClearCurrent_Click(object sender, EventArgs e)
        {
            importCADViewport.ClearCurrent();
        }


        #region Mapping Operations
        /// <summary>
        /// Method which returns a List of Entities which are selected in the Viewport
        /// </summary>
        /// <param name="selectedEntities"> List of Selected Entities</param>
        /// <returns>List of Selected Entites</returns>
        private List<Entity> GetSelectedEntites(List<Entity> selectedEntities)
        {
            for (int i = 0; i < importCADViewport.SelectedEntityList.Count; i++)
            {
                selectedEntities.Add((Entity)importCADViewport.SelectedEntityList[i].Item);
            }

            return selectedEntities;

        }

        /// <summary>
        /// Method to Map the Components
        /// </summary>
        /// <param name="_mapToThisBlock">Block into which the selected items are to be Mapped</param>
        /// <param name="_mapToThisBlockReference">Reference of the Block into which the selected items are to be mapped</param>
        /// <param name="_nameOfBlock">Name of the Block</param>
        List<Entity> selectedEntities;
        private void MapComponents(Block _mapToThisBlock, BlockReference _mapToThisBlockReference, string _nameOfBlock)
        {
            ///<summary>Getting the List of selected entities</summary>
            selectedEntities = new List<Entity>();
            selectedEntities = GetSelectedEntites(selectedEntities);

            if (selectedEntities != null && selectedEntities.Count != 0)
            {
                ///<summary>Adding the selected items into the Block</summary>
                for (int i = 0; i < importCADViewport.SelectedEntityList.Count; i++)
                {
                    ///<remarks>
                    ///The IF Loop is employed to check if the Suspended Mass Block already contains the selected Entities
                    ///This is necessary to ensure if the MAP button is clicked more than once with the same entities selected a "Key already exists" error doesn't come 
                    ///</remarks>
                    if (!_mapToThisBlock.Entities.Contains(importCADViewport.viewportLayout1.Entities[importCADViewport.viewportLayout1.Entities.IndexOf(selectedEntities[i])]))
                    {
                        _mapToThisBlock.Entities.Add(importCADViewport.viewportLayout1.Entities[importCADViewport.viewportLayout1.Entities.IndexOf(selectedEntities[i])]);
                    }
                    else
                    {
                        _mapToThisBlock.Entities = selectedEntities;
                        break;
                    }
                }

                ///<summary>Adding attributes to the Block</summary>
                devDept.Eyeshot.Entities.Attribute at = new devDept.Eyeshot.Entities.Attribute(new Point3D(0, 0, 0), _nameOfBlock, _nameOfBlock, 5);

                ///<summary>Adding the Blocck and the Block Reference into the Viewport</summary>
                if (!importCADViewport.viewportLayout1.Blocks.Contains(_nameOfBlock))
                {
                    importCADViewport.viewportLayout1.Blocks.Add(_nameOfBlock, _mapToThisBlock);
                    _mapToThisBlockReference = new BlockReference(_nameOfBlock);
                    _mapToThisBlockReference.Attributes.Add(_nameOfBlock, _nameOfBlock);
                    importCADViewport.viewportLayout1.Blocks.Reverse();
                    //importCADViewport.viewportLayout1.Entities.Add(_mapToThisBlockReference);
                }
                else
                {
                    //importCADViewport.viewportLayout1.Blocks[_nameOfBlock] = _mapToThisBlock;
                    importCADViewport.viewportLayout1.Blocks.ReplaceItem(_mapToThisBlock);
                }


                ///<summary>Viewport Operations</summary>
                importCADViewport.viewportLayout1.Entities.Regen();
                importCADViewport.viewportLayout1.Refresh();
                importCADViewport.viewportLayout1.ActionMode = actionType.None;
            }
            else
            {
                MessageBox.Show("Please select entites to Map");
            }

        }

        /// <summary>
        /// Method to add Invoked the Mapper method and populate the corresponding List Boxes. 
        /// </summary>
        /// <param name="_blockName">Name of the Block</param>
        private void MapComponents_Invoker(string _blockName)
        {
            if (_blockName == "Suspended Mass")
            {
                MapComponents(importCADViewport.SuspendedMass, importCADViewport.SuspendedMass_BR, "Suspended Mass");

                if (selectedEntities != null && selectedEntities.Count != 0)
                {
                    if (!listBoxControlMappedParts.Items.Contains("Suspended Mass"))
                    {
                        listBoxControlMappedParts.Items.Add("Suspended Mass");
                    }
                    if (!rotateObject.listBoxItemsWhichCanBeRotated.Items.Contains("Suspended Mass"))
                    {
                        rotateObject.listBoxItemsWhichCanBeRotated.Items.Add("Suspended Mass");
                    }
                    if (!translateObject.listBoxItemsWhichCanBeTranslated.Items.Contains("Suspended Mass"))
                    {
                        translateObject.listBoxItemsWhichCanBeTranslated.Items.Add("Suspended Mass");
                    }
                    barStaticItemMapInfo.Caption = _blockName + "Mapped";
                }
            }

            else if (_blockName == "Front Left Non Suspended Mass")
            {
                MapComponents(importCADViewport.NSM_FL, importCADViewport.NSM_FL_BR, "Front Left Non Suspended Mass");
                if (selectedEntities != null && selectedEntities.Count != 0)
                {
                    if (!listBoxControlMappedParts.Items.Contains("Front Left Non Suspended Mass"))
                    {
                        listBoxControlMappedParts.Items.Add("Front Left Non Suspended Mass");
                    }
                    if (!rotateObject.listBoxItemsWhichCanBeRotated.Items.Contains("Front Left Non Suspended Mass"))
                    {
                        rotateObject.listBoxItemsWhichCanBeRotated.Items.Add("Front Left Non Suspended Mass");
                    }
                    if (!translateObject.listBoxItemsWhichCanBeTranslated.Items.Contains("Front Left Non Suspended Mass"))
                    {
                        translateObject.listBoxItemsWhichCanBeTranslated.Items.Add("Front Left Non Suspended Mass");
                    }
                    barStaticItemMapInfo.Caption = _blockName + "Mapped";
                }
            }

            else if (_blockName == "Front Right Non Suspended Mass")
            {
                MapComponents(importCADViewport.NSM_FR, importCADViewport.NSM_FR_BR, "Front Right Non Suspended Mass");
                if (selectedEntities != null && selectedEntities.Count != 0)
                {
                    if (!listBoxControlMappedParts.Items.Contains("Front Right Non Suspended Mass"))
                    {
                        listBoxControlMappedParts.Items.Add("Front Right Non Suspended Mass");
                    }
                    if (!rotateObject.listBoxItemsWhichCanBeRotated.Items.Contains("Front Right Non Suspended Mass"))
                    {
                        rotateObject.listBoxItemsWhichCanBeRotated.Items.Add("Front Right Non Suspended Mass");
                    }
                    if (!translateObject.listBoxItemsWhichCanBeTranslated.Items.Contains("Front Right Non Suspended Mass"))
                    {
                        translateObject.listBoxItemsWhichCanBeTranslated.Items.Add("Front Right Non Suspended Mass");
                    }
                    barStaticItemMapInfo.Caption = _blockName + "Mapped";
                }
            }

            else if (_blockName == "Rear Left Non Suspended Mass")
            {
                MapComponents(importCADViewport.NSM_RL, importCADViewport.NSM_RL_BR, "Rear Left Non Suspended Mass");
                if (selectedEntities != null && selectedEntities.Count != 0)
                {
                    if (!listBoxControlMappedParts.Items.Contains("Rear Left Non Suspended Mass"))
                    {
                        listBoxControlMappedParts.Items.Add("Rear Left Non Suspended Mass");
                    }
                    if (!rotateObject.listBoxItemsWhichCanBeRotated.Items.Contains("Rear Left Non Suspended Mass"))
                    {
                        rotateObject.listBoxItemsWhichCanBeRotated.Items.Add("Rear Left Non Suspended Mass");
                    }
                    if (!translateObject.listBoxItemsWhichCanBeTranslated.Items.Contains("Rear Left Non Suspended Mass"))
                    {
                        translateObject.listBoxItemsWhichCanBeTranslated.Items.Add("Rear Left Non Suspended Mass");
                    }
                    barStaticItemMapInfo.Caption = _blockName + "Mapped";
                }
            }

            else if (_blockName == "Rear Right Non Suspended Mass")
            {
                MapComponents(importCADViewport.NSM_RR, importCADViewport.NSM_RR_BR, "Rear Right Non Suspended Mass");
                if (selectedEntities != null && selectedEntities.Count != 0)
                {
                    if (!listBoxControlMappedParts.Items.Contains("Rear Right Non Suspended Mass"))
                    {
                        listBoxControlMappedParts.Items.Add("Rear Right Non Suspended Mass");
                    }
                    if (!rotateObject.listBoxItemsWhichCanBeRotated.Items.Contains("Rear Right Non Suspended Mass"))
                    {
                        rotateObject.listBoxItemsWhichCanBeRotated.Items.Add("Rear Right Non Suspended Mass");
                    }
                    if (!translateObject.listBoxItemsWhichCanBeTranslated.Items.Contains("Rear Right Non Suspended Mass"))
                    {
                        translateObject.listBoxItemsWhichCanBeTranslated.Items.Add("Rear Right Non Suspended Mass");
                    }
                    barStaticItemMapInfo.Caption = _blockName + "Mapped";
                }
            }

        }

        /// <summary>
        /// Fires when the Map Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMap_Click(object sender, EventArgs e)
        {
            MapComponents_Invoker((string)comboBoxMap.SelectedItem);

            ///<remarks>
            ///Shifting this to the Constructor of the CoordinatesMap UserControl because it doesn't make much sense to put this here. The user will be able to create the Suspension Points regardless of whether the Parts are mapped or not
            ///The only thing is that, if the parts are not mapped, the Software will not show the coordinates in the textboxes.
            /// </remarks>
            //xuC_CoordinateMap1.VehicleCornerComboboxes_Parent();

            //xuC_CoordinateMap1.SuspensionCoordinatesCombobox_Parent();

        }
        #endregion

        #region Re-map and Unmap Operations
        /// <summary>
        /// Method to remove a block from the viewport
        /// </summary>
        /// <param name="_blockName"></param>
        /// <returns></returns>
        private bool RemoveBlockFromViewport(string _blockName)
        {
            if (_blockName != null)
            {
                if (importCADViewport.viewportLayout1.Blocks.Contains(_blockName))
                {
                    importCADViewport.viewportLayout1.Blocks.Remove(_blockName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return false;
        }

        /// <summary>
        /// Fires when the Remap button is clicked
        /// This can be used by the user to Remap anything which has already been mapped. Useful when the user has made a mistake while mapping. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonReMap_Click(object sender, EventArgs e)
        {
            ///<summary>Finding the Name of the Block using the Mapped Parts Listbox</summary>
            string blockName = (string)listBoxControlMappedParts.SelectedItem;
            ///<summary>Removing that block from the Viewport</summary>
            if (RemoveBlockFromViewport(blockName))
            {
                ///<summary>Since this is a re-map operation, adding the edited block back into the viewport</summary>
                MapComponents_Invoker(blockName);
            }
        }

        /// <summary>
        /// Fires when the Unmap Button is Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUnMap_Click(object sender, EventArgs e)
        {
            ///<summary>Finding the Name of the Block using the Mapped Parts Listbox</summary>
            string blockName = (string)listBoxControlMappedParts.SelectedItem;
            ///<summary>Removing that block from the Viewport</summary>
            if (RemoveBlockFromViewport(blockName))
            {
                ///<summary>Removing the string from the List Box</summary>
                listBoxControlMappedParts.Items.Remove(blockName);
                rotateObject.listBoxItemsWhichCanBeRotated.Items.Remove(blockName);
                translateObject.listBoxItemsWhichCanBeTranslated.Items.Remove(blockName);
            }

        }
        #endregion

        private void simpleButtonClearSelection_Click(object sender, EventArgs e)
        {
            importCADViewport.ClearSelection();
        }
        
        private void barButtonImportSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelImportSettings.Show();
            


        }

        private void barButtonPreviewPane_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelPreviewCAD.Show();
            dockPanelPreviewCAD.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

            //dockPanelPreviewCAD.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
        }

        private void barButtonView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonRotateObject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelRotateObject.ShowSliding();

        }

        private void barButtonTranslateObject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelTranslateObject.ShowSliding();
        }

        private void rotateObject_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void vGridControl1_Click(object sender, EventArgs e)
        {

        }

        private void xuC_CoordinateMap1_Load(object sender, EventArgs e)
        {

        }

        private bool refreshVP = false;

        private void dockPanelPreviewCAD_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DockPanelPreviewCAD_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            refreshVP = true;
        }



        private void tabbedView1_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            refreshVP = true;
        }

        private void tabbedView1_Paint(object sender, PaintEventArgs e)
        {
            if (refreshVP)
            {
                importCADViewport.ResizeVP();
            }
            refreshVP = false;
        }



        private void dockPanelImportSettings_Click(object sender, EventArgs e)
        {

        }

        #region Method to De-select all the Suspension Entiies so mapping is easier
        /// <summary>
        /// Fires when teh Deselect Suspension Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDeselectSuspension_Click(object sender, EventArgs e)
        {

            if (importCADViewport.viewportLayout1.Entities.Count != 0)
            {
                int[] addedItems;
                List<int> addedItemList = new List<int>();
                int[] remItems;
                List<int> remItemList = new List<int>();

                for (int i = 0; i < importCADViewport.viewportLayout1.Entities.Count; i++)
                {
                    if (importCADViewport.viewportLayout1.Entities[i].Selected == true && (importCADViewport.viewportLayout1.Entities[i] as Joint != null || importCADViewport.viewportLayout1.Entities[i] as Bar != null || importCADViewport.viewportLayout1.Entities[i] as Triangle != null || 
                                                                                           importCADViewport.viewportLayout1.Entities[i] as Solid3D != null || importCADViewport.viewportLayout1.Entities[i] as devDept.Eyeshot.Entities.Region != null)) 
                    {
                        remItemList.Add(i);
                    }
                    else if (importCADViewport.viewportLayout1.Entities[i].Selected == true)
                    {
                        addedItemList.Add(i);
                    }
                }
                addedItems = new int[addedItemList.Count];
                remItems = new int[remItemList.Count];

                for (int i = 0; i < addedItemList.Count; i++)
                {
                    addedItems[i] = addedItemList[i];
                }
                for (int i = 0; i < remItemList.Count; i++)
                {
                    remItems[i] = remItemList[i];
                }

                importCADViewport.ClearSuspensionSelection();
                ViewportLayout.SelectionChangedEventArgs eventArgs = new ViewportLayout.SelectionChangedEventArgs(addedItems, remItems, importCADViewport.viewportLayout1);
                importCADViewport.TriggerSelectionChangedEvent(eventArgs);
            }
        } 
        #endregion


        private void simpleButtonPlotSuspension_Click(object sender, EventArgs e)
        {
            ///<summary>
            ///Plotting the Vehicle's Suspension. This will be the first thing to happen 
            ///This will happen ONLY if a Suspension is created
            ///</summary>
            if (SuspensionCoordinatesFront.Assy_List_SCFL.Count != 0 )
            {
                R1 = Kinematics_Software_New.AssignFormVariable();
                int VIndex = R1.navBarGroupVehicle.SelectedLinkIndex;

                if (Vehicle.List_Vehicle[VIndex].sc_FL != null)
                {
                    PlotPreviewCAD(); 
                }
                else
                {
                    MessageBox.Show("Vehicle's Suspension Not Validated");
                }
            }
            else
            {
                MessageBox.Show("Suspension Not Created");
            }
        }



















    }
}

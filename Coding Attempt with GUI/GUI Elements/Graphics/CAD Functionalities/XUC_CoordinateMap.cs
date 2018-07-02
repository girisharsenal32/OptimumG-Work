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
using devDept.Eyeshot.Translators;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Eyeshot;
using devDept.Graphics;

namespace Coding_Attempt_with_GUI
{
    public partial class XUC_CoordinateMap : DevExpress.XtraEditors.XtraUserControl
    {
        #region Declarations
        /// <summary>
        /// Object of the ImportCAD UserControl
        /// </summary>
        XUC_ImportCAD importCAD;
        /// <summary>
        /// Object of the Suspension Type Class
        /// </summary>
        SuspensionType susTemplate;
        /// <summary>
        /// Double Wishbone Identifier
        /// </summary>
        int DoubleWishboneFront, DoubleWishboneRear;
        /// <summary>
        /// McPherson Identifier
        /// </summary>
        int McPhersonFront, McPhersonRear;
        /// <summary>
        /// Pushrod Identifier
        /// </summary>
        int PushrodFront, PushrodRear;
        /// <summary>
        /// Pullrod Identifier
        /// </summary>
        int PullrodFront, PullrodRear;
        /// <summary>
        /// UARB Identifier
        /// </summary>
        int UARBFront, UARBRear;
        /// <summary>
        /// TARB Identifier
        /// </summary>
        int TARBFront, TARBRear;
        /// <summary>
        /// Boolean to determine symmetry
        /// </summary>
        bool FrontSymmetry, RearSymmetry;
        /// <summary>
        /// Couplings count 
        /// </summary>
        int NoOfCouplings = 2;
        /// <summary>
        /// List of Steering System Coordinates
        /// </summary>
        List<string> SteeringCoordinates = new List<string>();

        /// <summary>
        /// List of Front Left Inboard Coordinates
        /// </summary>
        List<string> Inboard_FL = new List<string>();
        /// <summary>
        /// List of Front Left Outboard Coordinates
        /// </summary>
        List<string> Outboard_FL = new List<string>();
        /// <summary>
        /// List of Front Left coordinates which have been successfully Mapped
        /// </summary>
        List<string> MappedCoordinates_FL = new List<string>();

        /// <summary>
        /// List of Front Right Inboard Coordinates
        /// </summary>
        List<string> Inboard_FR = new List<string>();
        /// <summary>
        /// List of Front Right Outboard Coordinates
        /// </summary>
        List<string> Outboard_FR = new List<string>();
        /// <summary>
        /// List of Front Right coordinates which have been successfully Mapped
        /// </summary>
        List<string> MappedCoordinates_FR = new List<string>();


        /// <summary>
        /// List of Rear Left Inboard Coordinates
        /// </summary>
        List<string> Inboard_RL = new List<string>();
        /// <summary>
        /// List of Rear Left Outtboard Coordinates
        /// </summary>
        List<string> Outboard_RL = new List<string>();
        /// <summary>
        /// List of Rear Left coordinates which have been successfully Mapped
        /// </summary>
        List<string> MappedCoordinates_RL = new List<string>();

        /// <summary>
        /// List of Rear Right Inboard Coordinates
        /// </summary>
        List<string> Inboard_RR = new List<string>();
        /// <summary>
        /// List of Rear Right Inboard Coordinates
        /// </summary>
        List<string> Outboard_RR = new List<string>();
        /// <summary>
        /// List of Rear Right coordinates which have been successfully Mapped
        /// </summary>
        List<string> MappedCoordinates_RR = new List<string>();

        /// <summary>
        /// Boolean variable to determine if the mapping operations were sucessfull
        /// </summary>
        bool MappingSuccessful = false;
        /// <summary>
        /// Boolean to determine if the unmapping operations were successfull
        /// </summary>
        bool UnMappingSuccessfull = false;
        #endregion

        public XUC_CoordinateMap()
        {
            InitializeComponent();


        }

        #region Initializing a Suspension Template for the Import
        /// <summary>
        /// Method to create a Suspension TEMPLATE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSuspensionTemplate_Click(object sender, EventArgs e)
        {
            CreateSuspensionTemplate();
            simpleButtonSuspensionTemplate.Text = "Modify Suspension Template";
        }
        /// <summary>
        /// Creates a TEMPLATE of the Suspension
        /// </summary>
        private void CreateSuspensionTemplate()
        {
            ///<summary>Invoking the Suspension Template Form</summary>
            susTemplate = new SuspensionType(true, this);
            susTemplate.Reset();
            susTemplate.Show();

        }
        /// <summary>
        /// Assigns the Geometry Variables
        /// </summary>
        /// <param name="_dwFront">Double WIshbone Front Identifier</param>
        /// <param name="_dwRear">Double WIshbone Rear Identifier</param>
        /// <param name="_mcpFront">McPherson Front Identifier</param>
        /// <param name="_mcpRear">McPherson Rear Identifier</param>
        public void GeometryType(int _dwFront, int _dwRear, int _mcpFront, int _mcpRear)
        {
            DoubleWishboneFront = _dwFront;
            DoubleWishboneRear = _dwRear;
            McPhersonFront = _mcpFront;
            McPhersonRear = _mcpRear;
        }
        /// <summary>
        /// Assigns the Actuation Variables
        /// </summary>
        /// <param name="_pushrodFront">Pushrod Front Identifier</param>
        /// <param name="_pushrodRear">Pushrod Rear Identifier</param>
        /// <param name="_pullrodFront">Pullrod Front Identifier</param>
        /// <param name="_pullrodRear">Pullrod Rear Identifier</param>
        public void ActuationType(int _pushrodFront, int _pullrodFront, int _pushrodRear, int _pullrodRear)
        {
            PushrodFront = _pushrodFront;
            PushrodRear = _pushrodRear;
            PullrodFront = _pullrodFront;
            PullrodRear = _pullrodRear;
        }
        /// <summary>
        /// Assigns the ARB Variables
        /// </summary>
        /// <param name="_uarbFront">UARB Front Identifier</param>
        /// <param name="_uarbRear">UARB Rear Identifier</param>
        /// <param name="_tarbFront">TARB Front Identifier</param>
        /// <param name="_tarbRear">TARB Rear Identifier</param>
        public void AntiRollBarType(int _uarbFront, int _tarbFront, int _uarbRear, int _tarbRear)
        {
            UARBFront = _uarbFront;
            UARBRear = _uarbRear;
            TARBFront = _tarbFront;
            TARBRear = _tarbRear;
        }
        /// <summary>
        /// Method to determine whether the Suspension is of Front and Rear is symmetric
        /// </summary>
        /// <param name="_frontSymmetry">Boolean which identifies Front Symmetry</param>
        /// <param name="_rearSymmetry">Boolean which identifies Rear Symmetry</param>
        public void Symmetry(bool _frontSymmetry, bool _rearSymmetry)
        {
            FrontSymmetry = _frontSymmetry;
            RearSymmetry = _rearSymmetry;
        }
        /// <summary>
        /// Assigns the number of Couplings
        /// </summary>
        /// <param name="_noOfCouplings"></param>
        public void NoOfCouplingsCount(int _noOfCouplings)
        {
            NoOfCouplings = _noOfCouplings;
        }
        /// <summary>
        /// Initialzes the Listboxes based on the Identifiers obtained aboce
        /// </summary>
        public void InitializeListBoxes()
        {
            ///<summary>Populating the Vehicle Corner Listbox</summary>
            VehicleCornerComboboxes_Parent();

            ///<summary>Populating the Inbaord and Outboard Coordinate LISTS of each of the corners of the Vehicle</summary>
            PopulateListOfCoordinateNames_AllCorners();

            ///<summary>Populating the ListBoxes using the DataSource property</summary>
            SuspensionCoordinatesCombobox_Parent();

            ///<summary>Populating the Dictionary of Joints each of the corners </summary>
            importCAD.importCADViewport.DictionaryOfSusCoordinates_InitializeDictionary(Inboard_FL, Outboard_FL, importCAD.importCADViewport.CoordinatesFL);
            ///<remarks>The below code adds the steering system coordinates to the Front Left CoordinateDatabase Dictionary</remarks>
            importCAD.importCADViewport.DictionaryOfSuspensionCoordinate_InitializeDictionary_OnlySteering(SteeringCoordinates, importCAD.importCADViewport.CoordinatesFL);
            importCAD.importCADViewport.DictionaryOfSusCoordinates_InitializeDictionary(Inboard_FR, Outboard_FR, importCAD.importCADViewport.CoordinatesFR);
            importCAD.importCADViewport.DictionaryOfSusCoordinates_InitializeDictionary(Inboard_RL, Outboard_RL, importCAD.importCADViewport.CoordinatesRL);
            importCAD.importCADViewport.DictionaryOfSusCoordinates_InitializeDictionary(Inboard_RR, Outboard_RR, importCAD.importCADViewport.CoordinatesRR);
        }

        #endregion

        /// <summary>
        /// Method to get the Object of the Data Which owns 
        /// </summary>
        public void GetParentObject(object _parentObject)
        {
            importCAD = _parentObject as XUC_ImportCAD;
            //importCAD.importCADViewport.DictionaryOfSusCoordinates_InitializeDictionary_Invoker(listBoxControlSusCoordinatesInboard, listBoxControlSusCoordinatesOutboard);
        }

        public void VehicleCornerComboboxes_Parent()
        {
            listBoxControlVehicleCorners.Items.Clear();
            listBoxControlVehicleCorners.Items.AddRange(new string[] { "Front Left", "Front Right", "Rear Left", "Rear Right", "Steering System" });
            listBoxControlVehicleCorners.SelectedIndex = 0;
        }


        /// <summary>
        /// Method to populate the List of Suspension Coordinates. This is necessary because the Front and Rear can have different types of Suspension.
        /// By using a list of Coordinate Names for each coordinate, I can simply change the datasource of the listBox 
        /// </summary>
        private void PopulateListOfCoordinateNames_AllCorners()
        {
            string damperName = null, pushPullName = null;
            ///<summary>Changing the name of the damper and pushrod based on the suspension template</summary>
            if (DoubleWishboneFront == 1) { damperName = "Damper Bell-Crank"; }
            else { damperName = "Damper Upright"; }
            if (PushrodFront == 1) { pushPullName = "Pushrod"; }
            else { pushPullName = "Pullrod"; }

            ///<summary>Poulating the Lists of Front left and right Coordinate Names</summary>
            PoplateMasterList(Inboard_FL, Outboard_FL, damperName, pushPullName, DoubleWishboneFront, TARBFront);
            PoplateMasterList(Inboard_FR, Outboard_FR, damperName, pushPullName, DoubleWishboneFront, TARBFront);


            ///<summary>Changing the name of the damper and pushrod based on the suspension template</summary>
            if (DoubleWishboneRear == 1) { damperName = "Damper Bell-Crank"; }
            else { damperName = "Damper Upright"; }
            if (PushrodRear == 1) { pushPullName = "Pushrod"; }
            else { pushPullName = "Pullrod"; }
            ///<summary>Poulating the Lists of Rear left and right Coordinate Names</summary>
            PoplateMasterList(Inboard_RL, Outboard_RL, damperName, pushPullName, DoubleWishboneRear, TARBRear);
            PoplateMasterList(Inboard_RR, Outboard_RR, damperName, pushPullName, DoubleWishboneRear, TARBRear);

            ///<summary>Populating the List of Steering System Coordinates</summary
            SteeringCoordinates.Clear();
            if (NoOfCouplings == 2)
            {
                SteeringCoordinates.AddRange(new string[] { "Pinion Centre", "Pinion Universal Joint", "Steering Shaft Universal Joint", "Steering Shaft Support Chassis" });
            }
            else if (NoOfCouplings == 1)
            {
                SteeringCoordinates.AddRange(new string[] { "Pinion Centre", "Steering Shaft Universal Joint", "Steering Shaft Support Chassis" });
            }

        }

        /// <summary>
        /// Method to populate the List of Suspension Coordinate Names 
        /// </summary>
        /// <param name="_masterListInboard"></param>
        /// <param name="_masterListOutboard"></param>
        /// <param name="_damperName"></param>
        /// <param name="_pushPullName"></param>
        private void PoplateMasterList(List<string> _masterListInboard, List<string> _masterListOutboard, string _damperName, string _pushPullName, int _doubleWishboneMaster, int _tARBMaster)
        {
            ///<summary>Clearing the Listboxes so that no residue is left if user wants to change the Suspension Type</summary>
            _masterListInboard.Clear();
            _masterListOutboard.Clear();

            ///<summary>Adding the Coordinate Names for Common Suspension Joints</summary>
            _masterListInboard.AddRange(new string[] { "Lower Front Chassis", "Lower Rear Chassis", "Anti-Roll Bar Chassis", "Steering Link Chassis", "Damper Shock Mount", _damperName, "Ride Height Reference", "Anti-Roll Bar Link", });
            _masterListOutboard.AddRange(new string[] { "Lower Ball Joint", "Wheel Centre", "Steering Link Upright", "Contact Patch" });

            ///<summary>Adding the coordinate names for the DW Suspension Type</summary>
            if (_doubleWishboneMaster == 1)
            {
                _masterListInboard.AddRange(new string[] { "Upper Front Chassis", "Upper Rear Chassis", _pushPullName + " Bell-Crank", "Bell Crank Pivot", "Anti-Roll Bar Bell-Crank" });
                _masterListOutboard.AddRange(new string[] { "Upper Ball Joint", _pushPullName + " Upright" });
            }

            if (_tARBMaster == 1)
            {
                _masterListInboard.Add("Torsion Bar Bottom Pivot");
            }

        }

        public void SuspensionCoordinatesCombobox_Parent()
        {
            listBoxControlSusCoordinatesInboard.DataSource = Inboard_FL;
            listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
            listBoxControlSusCoordinatesOutboard.DataSource = Outboard_FL;
            listBoxControlSusCoordinatesOutboard.SelectedIndex = -1;
        }

        /// <summary>
        /// Fires when the Map Coordinate Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMapCoordinate_Click(object sender, EventArgs e)
        {
            MappingSuccessful = false;

            ///<summary>Getting the Coorner of the Coordinate which is being mapped</summary>
            string VehicleCorner = (string)listBoxControlVehicleCorners.SelectedItem;

            ///<summary>Getting the Coordinate which is selected</summary>
            string SelectedCoordinate = null;
            if (listBoxControlSusCoordinatesInboard.SelectedIndex == -1 && listBoxControlSusCoordinatesOutboard.SelectedIndex != -1)
            {
                SelectedCoordinate = (string)listBoxControlSusCoordinatesOutboard.SelectedItem;
            }
            else if (listBoxControlSusCoordinatesInboard.SelectedIndex != -1 && listBoxControlSusCoordinatesOutboard.SelectedIndex == -1)
            {
                SelectedCoordinate = (string)listBoxControlSusCoordinatesInboard.SelectedItem;
            }

            MapCoordinate(VehicleCorner, SelectedCoordinate);
        }

        private void MapCoordinate(string vehicleCorner, string selectedCoordinate)
        {
            if (vehicleCorner != null && selectedCoordinate != null)
            {
                MappingSuccessful = importCAD.importCADViewport.MapSuspensionCoordinate();

                if (MappingSuccessful)
                {
                    ///<summary>Getting the blockName of the Block to which the Coordinate belongs. That is, SM or any of the NSMs</summary>
                    ///<remarks>Null if the user hasn't done any mapping</remarks>
                    string BlockName = FindBlockOfSelectedItem();

                    ///<summary>At this point, the Dictionary has been initalized and all joints have a position of (0,0,0). The code below changes the position of the selected Coordinate to what the user has mapped</summary>
                    ///<remarks>Selected Cooordinate and Corner of that Coordinate both must exist for the point to be created</remarks>
                    //if (selectedCoordinate != null && vehicleCorner != null)
                    //{
                    importCAD.importCADViewport.DictionaryOfSusCoordinates_UpdateDictionary_Invoker(vehicleCorner, selectedCoordinate);
                    ///<summary>Adding the SelectedCoordinate along with its corner to the listBoxControlMappedCoordinates </summary>

                    if (!listBoxControlMappedCoordinate.Items.Contains(vehicleCorner + ' ' + selectedCoordinate))
                    {
                        listBoxControlMappedCoordinate.Items.Add(vehicleCorner + ' ' + selectedCoordinate);
                    }

                    AddToMappedCoordinateList(vehicleCorner, selectedCoordinate);

                    ///<summary>Setting the MappingSuccessfull Boolean to true because if this section of the code is reachhed then mapping would have been successfull</summary>
                    MappingSuccessful = true;

                    ///<summary>Clearing the Selection of the viewport using a custom method. <see cref="CAD.ClearSelection"/></summary>
                    importCAD.importCADViewport.ClearSelection();

                    ///<summary>Causing the Listbox to redraw itself with colored item names </summary>
                    listBoxControlSusCoordinatesInboard.Invalidate();
                    listBoxControlSusCoordinatesOutboard.Invalidate();
                    //}
                    //else
                    //{
                    //    MappingSuccessful = false;
                    //} 
                }

                else
                {
                    MessageBox.Show("Please Select The Coordinates You Wish To Map");
                }

            }
        }

        private void AddToMappedCoordinateList(string _vehicleCorner, string _selectedCoordinate)
        {
            if (_vehicleCorner == "Front Left")
            {
                if (!MappedCoordinates_FL.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_FL.Add(_selectedCoordinate); 
                }
            }
            else if (_vehicleCorner == "Front Right")
            {
                if (!MappedCoordinates_FR.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_FR.Add(_selectedCoordinate);
                }
            }
            else if (_vehicleCorner == "Rear Left")
            {
                if (!MappedCoordinates_RL.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_RL.Add(_selectedCoordinate);
                }
            }
            else if (_vehicleCorner == "Rear Right")
            {
                if (!MappedCoordinates_RR.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_RR.Add(_selectedCoordinate);
                }
            }
        }

        private void simpleButtonUnMapSuspensionCoordinate_Click(object sender, EventArgs e)
        {
            UnMapCoordinates(false);
        }

        private void UnMapCoordinates(bool _calledByReMap)
        {
            ///<summary>Declaring variable which will extract the strings of Vehicle Corner and Selected Coordinate from the listbox's selected item</summary>
            FindVehicleCornerAndCoordinate(out string VehicleCorner, out string SelectedCoordinate);

            if (VehicleCorner != null && SelectedCoordinate != null)
            {
                ///<summary>Finding the CoordinateDatabase Object that is needed based on the Vehicle Corner</summary>
                importCAD.importCADViewport.UnMapSuspensionCoordinate(VehicleCorner, SelectedCoordinate);

                if (!_calledByReMap)
                {
                    if (listBoxControlMappedCoordinate.Items.Contains(VehicleCorner + ' ' + SelectedCoordinate))
                    {
                        int index = listBoxControlMappedCoordinate.Items.IndexOf(VehicleCorner + ' ' + SelectedCoordinate);

                        listBoxControlMappedCoordinate.Items.BeginUpdate();

                        listBoxControlMappedCoordinate.Items.RemoveAt(index);

                        listBoxControlMappedCoordinate.Items.EndUpdate();
                    }

                    RemoveFromMappedCoordinateList(VehicleCorner, SelectedCoordinate); 

                    importCAD.importCADViewport.ClearSelection(); 
                }

            }
        }

        /// <summary>
        /// Method to find the Vehicle Corner and the Selected Coordinate from the Selected Item in the ListBox of Mapped Coordinate Items
        /// </summary>
        /// <param name="_vehicleCorner">Out Variable which returns the Vehicle Corner name</param>
        /// <param name="_selectedCoordinate">Out Variable which returns the Selected Coordinate name</param>
        private void FindVehicleCornerAndCoordinate(out string _vehicleCorner, out string _selectedCoordinate)
        {
            _vehicleCorner = null;
            _selectedCoordinate = null;
            ///<summary>Splitting the Coordinate's entire name as received from the ListBox of Mapped Coordinates and then seperating it into a Vehicle Corner and Coordinate Name</summary>
            if (listBoxControlMappedCoordinate.SelectedItem != null)
            {
                ///<summary>Getting the Full Name of the coordinate</summary>
                string FullNameOfCoordinate = (string)listBoxControlMappedCoordinate.SelectedItem;
                ///<summary>Splitting the Coordinate Name into an array of words</summary>
                string[] b = FullNameOfCoordinate.Split(' ');
                ///<summary>Using the First 2 words to form a string of Vehicle Corner</summary>
                _vehicleCorner = string.Concat(b[0], ' ', b[1]);
                ///<summary>Finding the actual coordinate name3</summary>
                _selectedCoordinate = b[2];
                for (int i = 3; i < b.Length; i++)
                {
                    _selectedCoordinate = string.Concat(_selectedCoordinate, ' ', b[i]);
                }
            }
        }

        private void RemoveFromMappedCoordinateList(string _vehicleCorner,string _selectedCoordinate)
        {
            if (_vehicleCorner == "Front Left")
            {
                if (MappedCoordinates_FL.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_FL.Remove(_selectedCoordinate);
                }
            }
            else if (_vehicleCorner == "Front Right")
            {
                if (MappedCoordinates_FR.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_FR.Remove(_selectedCoordinate);
                }
            }
            else if (_vehicleCorner == "Rear Left")
            {
                if (MappedCoordinates_RL.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_RL.Remove(_selectedCoordinate);
                }
            }
            else if (_vehicleCorner == "Rear Right")
            {
                if (MappedCoordinates_RR.Contains(_selectedCoordinate))
                {
                    MappedCoordinates_RR.Remove(_selectedCoordinate);
                }
            }

        }


        private void simpleButtonReMapSuspensionCoordinate_Click(object sender, EventArgs e)
        {
            ReMapCoordinate();
        }

        private void ReMapCoordinate()
        {
            ///<summary>Obtaining the Vehicle Corner and SelectedCoordinate which the user wants to remap using the ListBox of Mapped Coordinates</summary>
            FindVehicleCornerAndCoordinate(out string VehicleCorner, out string SelectedCoordinate);
            ///<summary>Unmapping the coordinate</summary>
            UnMapCoordinates(true);
            ///<summary>Re-mapping the coordinate using the Map Coordinate Method</summary>
            MapCoordinate(VehicleCorner, SelectedCoordinate);
        }

        private void listBoxControlMappedComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindVehicleCornerAndCoordinate(out string VehicleCorner, out string SelectedCoordinate);
            importCAD.importCADViewport.SelectMappedCoordinate(VehicleCorner, SelectedCoordinate);
        }

        /// <summary>
        /// Fires when the Create Suspension Button of the CoordinateMap UserCOntrol is clicked. This method creates a new Suspension item for each corner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCreateSuspension_Click(object sender, EventArgs e)
        {
            ///<summary>Getting the Object of the Main Form </summary>
            Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();
            R1.GeometryType(DoubleWishboneFront, DoubleWishboneRear, McPhersonFront, McPhersonRear);
            R1.ActuationType(PushrodFront, PullrodFront, PushrodRear, PullrodRear);
            R1.AntiRollBarType(UARBFront, TARBFront, UARBRear, TARBRear);
            R1.NoOfCouplings(NoOfCouplings);
            R1.FrontSymmetry = FrontSymmetry;
            R1.RearSymmetry = RearSymmetry;
            R1.CurrentSuspensionIsMapped = SuspensionCreationMode.Mapping;

            #region Creating new Suspension Coordinate items for each corner
            R1.barButtonSCFLItem.PerformClick();
            R1.barButtonItemSCFRItem.PerformClick();
            R1.barButtonItemSCRLItem.PerformClick();
            R1.barButtonItemSCRRItem.PerformClick();
            #endregion

            #region Invoking the CAD Creator
            R1.CreateFrontInputCAD(SuspensionCoordinatesFront.SCFLCounter - 1, false);
            R1.CreateRearInputCAD(SuspensionCoordinatesRear.SCRLCounter - 1, false);
            #endregion
        }



        private string FindBlockOfSelectedItem()
        {
            ///<summary>Finding the Block which the selected entity belongs to</summary>
            importCAD.importCADViewport.FindBlockOfSelectedItem(out string blockName);

            return blockName;

        }


        /// <summary>
        /// Event which is triggered when the Mouse is used to click an item of the <see cref="listBoxControlVehicleCorners"/>. 
        /// This event changes the Data Source of the <see cref="listBoxControlSusCoordinatesInboard"/> and <see cref="listBoxControlSusCoordinatesOutboard"/> with the corresponding <see cref="List{T}"/> of Suspension Pick Up <see cref="String"/>
        /// such as <see cref="Inboard_FL"/> <see cref="Outboard_FR"/> etc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxControlVehicleCorners_MouseClick(object sender, MouseEventArgs e)
        {
            if ((string)listBoxControlVehicleCorners.SelectedItem == "Front Left")
            {
                listBoxControlSusCoordinatesInboard.DataSource = Inboard_FL;
                listBoxControlSusCoordinatesOutboard.DataSource = Outboard_FL;
                listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
                listBoxControlSusCoordinatesOutboard.SelectedIndex = -1;
            }
            else if ((string)listBoxControlVehicleCorners.SelectedItem == "Front Right")
            {
                listBoxControlSusCoordinatesInboard.DataSource = Inboard_FR;
                listBoxControlSusCoordinatesOutboard.DataSource = Outboard_FR;
                listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
                listBoxControlSusCoordinatesOutboard.SelectedIndex = -1;
            }
            else if ((string)listBoxControlVehicleCorners.SelectedItem == "Rear Left")
            {
                listBoxControlSusCoordinatesInboard.DataSource = Inboard_RL;
                listBoxControlSusCoordinatesOutboard.DataSource = Outboard_RL;
                listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
                listBoxControlSusCoordinatesOutboard.SelectedIndex = -1;
            }
            else if ((string)listBoxControlVehicleCorners.SelectedItem == "Reaar Left")
            {
                listBoxControlSusCoordinatesInboard.DataSource = Inboard_RR;
                listBoxControlSusCoordinatesOutboard.DataSource = Outboard_RR;
                listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
                listBoxControlSusCoordinatesOutboard.SelectedIndex = -1;
            }
            else if ((string)listBoxControlVehicleCorners.SelectedItem == "Steering System")
            {
                listBoxControlSusCoordinatesInboard.DataSource = SteeringCoordinates;
                listBoxControlSusCoordinatesOutboard.DataSource = null;
                listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Event which allows the user to use the <see cref="EntityList.SetCurrent(BlockReference, bool)"/> method to allow access to the Faces of the SELECTED (<see cref="ViewportLayout.SelectedItem"/>) <see cref="BlockReference"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSetCurrent_Click(object sender, EventArgs e)
        {
            ///<summary>Setting Current the Selected <see cref="BlockReference"/>s </summary>
            importCAD.importCADViewport.SetCurrent();
        }

        /// <summary>
        /// Event which sets the Current <see cref="BlockReference"/> to <see cref="null"/> thereby regrouping all the <see cref="BlockReference"/>s to their original state as when they were first imported. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClearCurrent_Click(object sender, EventArgs e)
        {
            ///<summary>Clearing the <see cref="BlockReference"/>s set as current </summary>
            importCAD.importCADViewport.ClearCurrent();
        }

        /// <summary>
        /// Event of the <see cref="selectCheckButton"/> to allow the user to select the Faces or Entire Entities using the <see cref="selectionComboBox"/>'s items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkButtonSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (selectCheckButton.Checked)

                Selection();

            else

                importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.None;
        }

        /// <summary>
        /// Event to Clear the Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            importCAD.importCADViewport.ClearSelection();
        }

        /// <summary>
        /// Event to handle the Index change of the <see cref="selectionComboBox"/>. Based on whether the <see cref="selectCheckButton"/> is selected or not, the <see cref="ViewportLayout"/>'s Select Cursor is activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectCheckButton.Checked)

                Selection();
        }

        /// <summary> 
        /// Method to set the <see cref="actionType"/> for selection in the <see cref="ViewportLayout"/> based on the <see cref="selectionComboBox"/>'s selected item 
        /// </summary>
        private void Selection()
        {
            switch (selectionComboBox.SelectedIndex)
            {
                case 0: // by pick
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectByPick;
                    break;

                case 1: // by box
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectByBox;
                    break;

                case 2: // by poly
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectByPolygon;
                    break;

                case 3: // by box enclosed
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectByBoxEnclosed;
                    break;

                case 4: // by poly enclosed
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectByPolygonEnclosed;
                    break;

                case 5: // visible by pick
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectVisibleByPick;
                    break;

                case 6: // visible by box
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectVisibleByBox;
                    break;

                case 7: // visible by poly
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectVisibleByPolygon;
                    break;

                case 8: // visible by pick dynamic
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectVisibleByPickDynamic;
                    break;

                case 9: // visible by pick label
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.SelectVisibleByPickLabel;
                    
                    break;

                default:
                    importCAD.importCADViewport.viewportLayout1.ActionMode = actionType.None;
                    break;
            }
        }

        /// <summary>
        /// Event to handle in the Index change of the <see cref="selectionFilterComboBox"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionFilter();
        }

        /// <summary>
        /// Method to handle the <see cref="selectionFilterType"/> of the <see cref="ViewportLayout"/> based on the <see cref="selectionFilterComboBox"/>'s selected item 
        /// </summary>
        private void SelectionFilter()
        {
            switch (selectionFilterComboBox.SelectedIndex)
            {
                case 0: // by Vertex
                    importCAD.importCADViewport.viewportLayout1.SelectionFilterMode = selectionFilterType.Vertex;
                    break;

                case 1: // by Edge
                    importCAD.importCADViewport.viewportLayout1.SelectionFilterMode = selectionFilterType.Edge;
                    break;

                case 2: // by Face
                    importCAD.importCADViewport.viewportLayout1.SelectionFilterMode = selectionFilterType.Face;
                    break;

                case 3: // by Entity
                    importCAD.importCADViewport.viewportLayout1.SelectionFilterMode = selectionFilterType.Entity;
                    break;
            }
        }



        private void listBoxControlSusCoordinatesOutboard_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void groupControlFLCoordinate_Paint(object sender, PaintEventArgs e)
        {

        }



        private void listBoxControlSusCoordinatesInboard_MouseClick(object sender, MouseEventArgs e)
        {
            listBoxControlSusCoordinatesOutboard.SelectedIndex = -1;
        }

        private void listBoxControlSusCoordinatesOutboard_MouseClick(object sender, MouseEventArgs e)
        {
            listBoxControlSusCoordinatesInboard.SelectedIndex = -1;
        }

        private List<string> GetVehicleCornerASDataSource()
        {
            if (listBoxControlSusCoordinatesInboard.DataSource == Inboard_FL)
            {
                return MappedCoordinates_FL;
            }
            else if (listBoxControlSusCoordinatesInboard.DataSource == Inboard_FR)
            {
                return MappedCoordinates_FR;
            }
            else if (listBoxControlSusCoordinatesInboard.DataSource == Inboard_RL)
            {
                return MappedCoordinates_RL;
            }
            else if (listBoxControlSusCoordinatesInboard.DataSource == Inboard_RR)
            {
                return MappedCoordinates_RR;
            }
            else
            {
                return null;
            }
        }

        private void listBoxControlSusCoordinatesInboard_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            List<string> _masterString = GetVehicleCornerASDataSource();

            if (_masterString != null)
            {
                for (int i = 0; i < _masterString.Count; i++)
                {
                    if ((string)e.Item == _masterString[i])
                    {
                        e.Appearance.BackColor = Color.Green;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.White;
                    }
                } 
            }
        }

        private void listBoxControlSusCoordinatesOutboard_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            List<string> _masterString = GetVehicleCornerASDataSource();

            if (_masterString != null)
            {
                for (int i = 0; i < _masterString.Count; i++)
                {
                    if ((string)e.Item == _masterString[i])
                    {
                        e.Appearance.BackColor = Color.Green;
                    }
                    //else
                    //{
                    //    e.Appearance.BackColor = Color.White;
                    //}
                }
            }
        }

    }
}

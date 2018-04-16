using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using devDept.Eyeshot;
using devDept.Graphics;
using devDept.Eyeshot.Translators;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Eyeshot.Labels;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MathNet.Spatial;
using DevExpress.XtraBars.Docking;
using System.Data;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public partial class CAD : XtraUserControl, ISerializable/*,IComparer<Entity>*/
    {


        #region Declarations
        Point3D tV1, tV2, tV3;
        Vector3D Xaxis, YAxis, Zaxis; // World Axes
        Solid wheel;
        /// <summary>
        /// Temporary entity used for mouse click events
        /// </summary>
        Entity temp_Entity;
        /// <summary>
        /// Temporary joint used for mouse click events
        /// </summary>
        Joint temp_Joint;
        /// <summary>
        /// Temporary bar used for mouse click events
        /// </summary>
        Bar temp_Bar;
        /// <summary>
        /// Temporary points used for mouse click events
        /// </summary>
        Point3D temp_Point, temp_Bar_Start, temp_Bar_End;
        /// <summary>
        /// Temporary vector used for mouse click events
        /// </summary>
        Vector3D temp_Bar_Length;
        /// <summary>
        /// Temporary Arrow used for mouse click events
        /// </summary>
        Mesh temp_arrow;
        /// <summary>
        /// Ends of the Stand
        /// </summary>
        Point3D StandFL, StandFR, StandRL, StandRR;
        /// <summary>
        /// Quad to represent the stand
        /// </summary>
        Quad Stand;
        /// <summary>
        /// Bar to represent the central support of the Stand
        /// </summary>
        Bar SupportBar;
        /// <summary>
        /// Support Bar's Centre Point
        /// </summary>
        Point3D SupportBarStart, SupportBarEnd;
        /// <summary>
        /// List of 3D Points which represents the start of the Side Panel. Declared as Global List so that this can be used to later plot the Nose as well
        /// </summary>
        List<Point3D> sidePanelStartCurvePoints;
        /// <summary>
        /// List of 3D Points which represents the end of the Side Panel. Declared as Global List so that this can be used to later plot the Roll Hoop as well
        /// </summary>
        List<Point3D> sidePanelEndCurvePoints;
        /// <summary>
        /// Contains the file which is imported
        /// </summary>
        public ReadFileAsync importedFile;
        ///// <summary>
        ///// Entities of the Imported IGES File
        ///// </summary>
        public Entity[] igesEntities;
        ///// <summary>
        ///// Boolean variable to determine if an IGS file of a Vehicle Object has been imported
        ///// </summary>
        //public bool FileHasBeenImported = false;
        ///// <summary>
        ///// Boolean varaible to determine if the user wishes the CAD to be imported
        ///// </summary>
        //public bool CADToBeImported = false;
        /// <summary>
        /// OpenFileDialog object. 
        /// </summary>
        public OpenFileDialog openFileDialog1;
        /// <summary>
        /// Stack to hold the Index of the Entites which are hidden so they can unhidden
        /// </summary>
        Stack<int> hiddenEntities = new Stack<int>();
        ///// <summary>
        ///// Object  of the Rotate Object Form
        ///// </summary>
        //XUC_RotateObject rotateObject;
        ///// <summary>
        ///// Object of the Translate Object Form
        ///// </summary>
        //XUC_TranslateObject translateObject;
        /// <summary>
        /// Angle of Rotation
        /// </summary>
        double RotationAngle;
        /// <summary>
        /// Axis of Rotation
        /// </summary>
        Vector3D AxisOfRotation;
        /// <summary>
        /// Translation Amount
        /// </summary>
        double TranslationX, TranslationY, TranslationZ;

        /// <summary>
        /// List which holds all the selected entites. 
        /// IMPORTANT - This list can be cast as an Entity
        /// </summary>
        public List<ViewportLayout.SelectedItem> SelectedEntityList = new List<ViewportLayout.SelectedItem>();
        /// <summary>
        /// Master Object of the <see cref="CoordinateDatabase"/> Class
        /// </summary>
        public CoordinateDatabase CoordinatesTemp = new CoordinateDatabase();
        /// <summary>
        /// Object of the <see cref="CoordinateDatabase"/> Class to hold the Front Left Coordinates
        /// </summary>
        public CoordinateDatabase CoordinatesFL = new CoordinateDatabase();
        /// <summary>
        /// Object of the <see cref="CoordinateDatabase"/> Class to hold the Front Right Coordinates
        /// </summary>
        public CoordinateDatabase CoordinatesFR = new CoordinateDatabase();
        /// <summary>
        /// Object of the <see cref="CoordinateDatabase"/> Class to hold the Rear Left Coordinates
        /// </summary>
        public CoordinateDatabase CoordinatesRL = new CoordinateDatabase();
        /// <summary>
        /// Object of the <see cref="CoordinateDatabase"/> Class to hold the Rear Right Coordinates
        /// </summary>
        public CoordinateDatabase CoordinatesRR = new CoordinateDatabase();
        /// <summary>
        /// CoG of Vehicle
        /// </summary>
        Point3D PointOfRotation = new Point3D();
        /// <summary>
        /// Block of the Suspended Mass
        /// </summary>
        public Block SuspendedMass = new Block();
        public BlockReference SuspendedMass_BR;
        /// <summary>
        /// Block of the Front Left Non Suspended Mass 
        /// </summary>
        public Block NSM_FL = new Block();
        public BlockReference NSM_FL_BR = new BlockReference("Non Suspended Mass Front Left");
        /// <summary>
        /// Block of the Front Right Non Suspended Mass 
        /// </summary>
        public Block NSM_FR = new Block();
        public BlockReference NSM_FR_BR = new BlockReference("Non Suspended Mass Front Right");
        /// <summary>
        /// Block of the Rear Left Non Suspended Mass 
        /// </summary>
        public Block NSM_RL = new Block();
        public BlockReference NSM_RL_BR = new BlockReference("Non Suspended Mass Rear Left");
        /// <summary>
        /// Block of the Rear Right Non Suspended Mass 
        /// </summary>
        public Block NSM_RR = new Block();
        public BlockReference NSM_RR_BR = new BlockReference("Non Suspended Mass Rear Right");
        /// <summary>
        /// Object of the ImportCAD Form.
        /// </summary>
        /*ImportCADForm*/
        XUC_ImportCAD importCADForm;
        /// <summary>
        /// 
        /// </summary>
        Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();
        #endregion

        #region Constructor
        public CAD()
        {
            InitializeComponent();
            viewportLayout1.Unlock("US1-126DS-NQPVC-7XRN-S062");
            //viewportLayout1.CreateControl();
            viewportLayout1.MultipleSelection = true;
            viewportLayout1.ShowLoad = true;
            viewportLayout1.AskForHardwareAcceleration = false;
            string Version = viewportLayout1.ProductVersion;
        }
        /// <summary>
        /// This overloaded constructor accpets an object of the ImportCADForm. This is so that a connection can be established between this user control and the ImportCADForm when the user wishes to Import a Vehicle
        /// </summary>
        /// <param name="_importCADForm"></param>
        public CAD(XUC_ImportCAD _importCADForm)
        {
            InitializeComponent();
            viewportLayout1.Unlock("US1-126DS-NQPVC-7XRN-S062");
            //viewportLayout1.CreateControl();
            viewportLayout1.Refresh();
            viewportLayout1.Update();

            viewportLayout1.WorkCompleted += ViewportLayout1_WorkCompleted;
            viewportLayout1.ToolBars[0].Buttons[7].Click += RotateObject_FormInvoker;
            viewportLayout1.ToolBars[0].Buttons[8].Click += TranslateObject_FormInvoker;
            viewportLayout1.SelectionChanged += ViewportLayout1_SelectionChanged;
            viewportLayout1.MultipleSelection = true;
            viewportLayout1.ShowLoad = true;
            importCADForm = _importCADForm;
            importCADForm.rotateObject.GetCADObject(this);
            importCADForm.translateObject.GetCADObject(this);
            viewportLayout1.AskForHardwareAcceleration = false;
            string Version = viewportLayout1.ProductVersion;
        }


        #endregion

        #region Usercontrol Load Event

        private void CAD_Load(object sender, EventArgs e)
        {

        }
        private void OpenTKInput_Load(object sender, EventArgs e)
        {


        }
        #endregion

        #region Selection Changed Event
        /// <summary>
        /// Method to trigger the Selection Changed event. Used when using the boolean "Selected" property is used to clear or add selection
        /// </summary>
        /// <param name="eventArgs"></param>
        public void TriggerSelectionChangedEvent(ViewportLayout.SelectionChangedEventArgs eventArgs)
        {
            ViewportLayout1_SelectionChanged(viewportLayout1, eventArgs);
        }

        private void AddedItems(List<ViewportLayout.SelectedItem> _addedItems)
        {
            if (_addedItems.Count != 0)
            {
                if (viewportLayout1.ActionMode == actionType.SelectVisibleByBox || viewportLayout1.ActionMode == actionType.SelectByBox)
                {
                    SelectedEntityList = new List<ViewportLayout.SelectedItem>();
                    SelectedEntityList = _addedItems;

                    for (int i = 0; i < SelectedEntityList.Count; i++)
                    {
                        importCADForm.listBoxSelectedParts.Items.Add(SelectedEntityList[i]);
                    }

                }
                else
                {
                    ///<remarks>
                    ///Need to first perform an Add operation and then a delete operation because only then will the Selected Entity contain some elements. Otherwise it will never enter the Loop because the count will always be zero
                    /// </remarks>
                    if (SelectedEntityList.Count == 0)
                    {
                        SelectedEntityList.Add(_addedItems[0]);
                        importCADForm.listBoxSelectedParts.Items.Add(_addedItems[0]);
                    }
                    else
                    {
                        if (!SelectedEntityList.Contains(_addedItems[0]))
                        {
                            SelectedEntityList.Add(_addedItems[0]);
                            importCADForm.listBoxSelectedParts.Items.Add(_addedItems[0]);
                        }

                    }
                }
            }




        }
        private void RemovedItems(List<ViewportLayout.SelectedItem> _removedItems)
        {
            if (_removedItems.Count != 0)
            {
                if (viewportLayout1.ActionMode != actionType.SelectVisibleByBox)
                {
                    if (SelectedEntityList.Count != 0)
                    {
                        if (SelectedEntityList.Contains(_removedItems[0]))
                        {
                            SelectedEntityList.Remove(_removedItems[0]);
                            importCADForm.listBoxSelectedParts.Items.Remove(_removedItems[0]);
                        }
                    }
                }
            }




        }
        private void ViewportLayout1_SelectionChanged(object sender, ViewportLayout.SelectionChangedEventArgs e)
        {
            AddedItems(e.AddedItems);

            RemovedItems(e.RemovedItems);
        }
        #endregion

        #region Method to clear all the selected items of the viewport quickly OR Selectively
        public void ClearSelection()
        {
            viewportLayout1.Entities.ClearSelection();
            importCADForm.listBoxSelectedParts.Items.Clear();
            SelectedEntityList.Clear();
            viewportLayout1.Refresh();
        }

        public void ClearSuspensionSelection()
        {
            for (int i = 0; i < viewportLayout1.Entities.Count; i++)
            {

                if (viewportLayout1.Entities[i] as Joint != null || viewportLayout1.Entities[i] as Triangle != null || viewportLayout1.Entities[i] as Bar != null || viewportLayout1.Entities[i] as Solid3D != null || viewportLayout1.Entities[i] as devDept.Eyeshot.Entities.Region != null)
                {
                    viewportLayout1.Entities[i].Selected = false;
                }

            }
            viewportLayout1.Refresh();
        }
        #endregion

        public void GetCoG(Chassis _chassisCoG)
        {
            PointOfRotation.X = _chassisCoG.SuspendedMassCoGx;
            PointOfRotation.Y = _chassisCoG.SuspendedMassCoGy;
            PointOfRotation.Z = _chassisCoG.SuspendedMassCoGz;


        }

        #region Translate Operations. Contains the method to rotate the selected Block and the Inboard or Outboard Points with it
        /// <summary>
        /// Method to Invoked the Translate Object UserControl and pass the list of Mapped Items to its ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslateObject_FormInvoker(object sender, EventArgs e)
        {
            ///<summary>Adding the List of Mapped Parts into Translate Object List</summary>
            for (int i = 0; i < importCADForm.listBoxControlMappedParts.Items.Count; i++)
            {
                importCADForm.translateObject.listBoxItemsWhichCanBeTranslated.Items.Add(importCADForm.listBoxControlMappedParts.Items[i]);
            }
        }
        /// <summary>
        /// Method to Translate the Particular Block of which is selected by the user 
        /// </summary>
        /// <param name="_blockName"></param>
        private void TranslateBlock(string _blockName)
        {
            if (viewportLayout1.Blocks.Contains(_blockName))
            {
                for (int i = 0; i < viewportLayout1.Blocks[_blockName].Entities.Count; i++)
                {
                    viewportLayout1.Blocks[_blockName].Entities[i].Translate(TranslationX, TranslationY, TranslationZ);
                }
            }
        }

        /// <summary>
        /// Method to Invoke the Translator Functions based on whether the user wants to translate the Inboard or Outboard Points
        /// </summary>
        /// <param name="_blockName"></param>
        private void TranslatePickUpPoint_Invoker(string _blockName)
        {
            if (_blockName == "Suspended Mass")
            {
                TranslatePickUpPoint_Inboard();
            }
            else if (_blockName == "Front Left Non Suspended Mass")
            {
                TranslatePickUpPoint_Outboard(CoordinatesFL.OutboardPickUp);
            }
            else if (_blockName == "Front Right Non Suspended Mass")
            {
                TranslatePickUpPoint_Outboard(CoordinatesFR.OutboardPickUp);
            }
            else if (_blockName == "Rear Left Non Suspended Mass")
            {
                TranslatePickUpPoint_Outboard(CoordinatesRL.OutboardPickUp);
            }
            else if (_blockName == "Rear Right Non Suspended Mass")
            {
                TranslatePickUpPoint_Outboard(CoordinatesRR.OutboardPickUp);
            }
        }

        /// <summary>
        /// Method to Translate all the Inboard Points of all the corners. Called when the Suspended Mass is to be translated
        /// </summary>
        private void TranslatePickUpPoint_Inboard()
        {
            foreach (string key in CoordinatesFL.InboardPickUp.Keys)
            {
                viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesFL.InboardPickUp[key])].Translate(TranslationX, TranslationY, TranslationZ);
            }
            foreach (string key in CoordinatesFR.InboardPickUp.Keys)
            {
                viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesFR.InboardPickUp[key])].Translate(TranslationX, TranslationY, TranslationZ);
            }
            foreach (string key in CoordinatesRL.InboardPickUp.Keys)
            {
                viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesRL.InboardPickUp[key])].Translate(TranslationX, TranslationY, TranslationZ);
            }
            foreach (string key in CoordinatesRR.InboardPickUp.Keys)
            {
                viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesRR.InboardPickUp[key])].Translate(TranslationX, TranslationY, TranslationZ);
            }
        }

        /// <summary>
        /// Method to Translate the Specific set of Outboard points of interest. User will select which NSM to translate
        /// </summary>
        /// <param name="_outboardPoints">Dictionary of Outboard Pointd which are to be rotateds</param>
        private void TranslatePickUpPoint_Outboard(Dictionary<string, Joint> _outboardPoints)
        {
            foreach (string key in _outboardPoints.Keys)
            {
                viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(_outboardPoints[key])].Translate(TranslationX, TranslationY, TranslationZ);
            }
        }

        public void GetTranslationXYZ(double _translationX, double _translationY, double _translationZ, string _blockToBeTranslated)
        {
            /////<summary>List of Integers to store the indices of the Selected Entities in the Vieewport</summary>
            //List<int> SelectedEntityIndex = new List<int>();

            ///<summary>Getting the Translation in XYZ axes</summary>
            TranslationX = _translationX;
            TranslationY = _translationY;
            TranslationZ = _translationZ;

            ///<summary>Translating the Block</summary>
            TranslateBlock(_blockToBeTranslated);

            ///<summary>Rotating the Suspension Inboard or Outboard Pick Up Points. That is the Joints are rotated</summary>
            TranslatePickUpPoint_Invoker(_blockToBeTranslated);

            ///<summary>Forcing the entities of the viewport to Regenerate </summary>
            for (int i = 0; i < viewportLayout1.Entities.Count; i++)
            {
                viewportLayout1.Entities[i].RegenMode = regenType.RegenAndCompile;
            }

            viewportLayout1.Entities.Regen();
            viewportLayout1.Invalidate();
            viewportLayout1.UpdateViewportLayout();
            viewportLayout1.Refresh();


        }
        #endregion


        #region Rotate Operations. Containts method which rotate the entire SM or NSMs
        private void RotateObject_FormInvoker(object sender, EventArgs e)
        {
            ///<summary>Adding the List of Mapped Parts into Rotate Object List</summary>
            for (int i = 0; i < importCADForm.listBoxControlMappedParts.Items.Count; i++)
            {
                importCADForm.rotateObject.listBoxItemsWhichCanBeRotated.Items.Add(importCADForm.listBoxControlMappedParts.Items[i]);
            }

            importCADForm.rotateObject.Show();
            importCADForm.rotateObject.BringToFront();
        }

        /// <summary>
        /// Rotates all the Entities within the Block which is passed as a string parameter
        /// </summary>
        /// <param name="_blockName"></param>
        private void RotateBlock(string _blockName)
        {
            if (viewportLayout1.Blocks.Contains(_blockName))
            {
                for (int i = 0; i < viewportLayout1.Blocks[_blockName].Entities.Count; i++)
                {
                    viewportLayout1.Blocks[_blockName].Entities[i].Rotate(RotationAngle, AxisOfRotation, PointOfRotation);
                }
            }
        }

        /// <summary>
        /// Method to Rotate the Inboard Pick Up Point
        /// </summary>
        private void RotatePickUpPoint_Inboard()
        {
            int index;
            foreach (string key in CoordinatesFL.InboardPickUp.Keys)
            {
                index = viewportLayout1.Entities.IndexOf(CoordinatesFL.InboardPickUp[key]);

                if (index != -1)
                {
                    viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesFL.InboardPickUp[key])].Rotate(RotationAngle, AxisOfRotation, PointOfRotation); 
                }
            }
            foreach (string key in CoordinatesFR.InboardPickUp.Keys)
            {
                index = viewportLayout1.Entities.IndexOf(CoordinatesFR.InboardPickUp[key]);

                if (index != -1)
                {
                    viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesFR.InboardPickUp[key])].Rotate(RotationAngle, AxisOfRotation, PointOfRotation); 
                }
            }
            foreach (string key in CoordinatesRL.InboardPickUp.Keys)
            {
                index = viewportLayout1.Entities.IndexOf(CoordinatesRL.InboardPickUp[key]);

                if (index != -1)
                {
                    viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesRL.InboardPickUp[key])].Rotate(RotationAngle, AxisOfRotation, PointOfRotation); 
                }
            }
            foreach (string key in CoordinatesRR.InboardPickUp.Keys)
            {
                index = viewportLayout1.Entities.IndexOf(CoordinatesRR.InboardPickUp[key]);

                if (index != -1)
                {
                    viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(CoordinatesRR.InboardPickUp[key])].Rotate(RotationAngle, AxisOfRotation, PointOfRotation); 
                }
            }
        }

        /// <summary>
        /// Method to Rotate the Outboard Point which is passed as parameter
        /// </summary>
        /// <param name="_outboardPoints">Dictionary of Outboard Points</param>
        private void RotatePickUpPoint_Outboard(Dictionary<string, Joint> _outboardPoints)
        {
            foreach (string key in _outboardPoints.Keys)
            {
                int index = viewportLayout1.Entities.IndexOf(_outboardPoints[key]);

                if (index != -1)
                {
                    viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(_outboardPoints[key])].Rotate(RotationAngle, AxisOfRotation, PointOfRotation); 
                }
            }
        }

        /// <summary>
        /// Method to inboke the Pick Up Point Invoker Methods
        /// </summary>
        /// <param name="_blockName"></param>
        private void RotatePickUpPoint_Invoker(string _blockName)
        {
            if (_blockName == "Suspended Mass")
            {
                RotatePickUpPoint_Inboard();
            }
            else if (_blockName == "Front Left Non Suspended Mass")
            {
                RotatePickUpPoint_Outboard(CoordinatesFL.OutboardPickUp);
            }
            else if (_blockName == "Front Right Non Suspended Mass")
            {
                RotatePickUpPoint_Outboard(CoordinatesFR.OutboardPickUp);
            }
            else if (_blockName == "Rear Left Non Suspended Mass")
            {
                RotatePickUpPoint_Outboard(CoordinatesRL.OutboardPickUp);
            }
            else if (_blockName == "Rear Right Non Suspended Mass")
            {
                RotatePickUpPoint_Outboard(CoordinatesRR.OutboardPickUp);

            }

        }

        public void GetRotationAngleAndAxisOfRotation(double _rotationAngle, Vector3D _axisOfRotation, Point3D _pointOfRotation, string _blockToBeRotated)
        {
            /////<summary>List of Integers to store the indices of the Selected Entities in the Vieewport</summary>
            //List<int> SelectedEntityIndex = new List<int>();
            ///<summary>Assiging the Rotation Angle and the Axis of Rotation</summary>
            RotationAngle = _rotationAngle * ((Math.PI) / 180);
            AxisOfRotation = _axisOfRotation;
            ///<summary>Assining the point through which the Axis of Rotation passes</summary>
            PointOfRotation = _pointOfRotation;
            ///<summary>
            ///Rotating the Block
            /// </summary>
            RotateBlock(_blockToBeRotated);

            ///<summary>Rotating the Suspension Inboard or Outboard Pick Up Points. That is the Joints are rotated</summary>
            RotatePickUpPoint_Invoker(_blockToBeRotated);

            ///<summary>Forcing the entities of the viewport to Regenerate </summary>
            for (int i = 0; i < viewportLayout1.Entities.Count; i++)
            {
                viewportLayout1.Entities[i].RegenMode = regenType.RegenAndCompile;
            }

            viewportLayout1.Entities.Regen();
            viewportLayout1.Invalidate();
            viewportLayout1.UpdateViewportLayout();
            viewportLayout1.Refresh();

        }
        #endregion

        #region Pop-up Menu Button Click events
        private void barButtonBoxSelection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.ActionMode = actionType.SelectVisibleByBox;
            //SelectedEntityList = new List<ViewportLayout.SelectedItem>();
            //viewportLayout1.Entities.ClearSelection();
            //importCADForm.listBoxSelectedParts.Items.Clear();
        }
        private void barButtonBoxSelectionAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.ActionMode = actionType.SelectByBox;

        }
        private void barButtonMultipleSelection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.ActionMode = actionType.SelectVisibleByPick;
            //viewportLayout1.Entities.ClearSelection();
            viewportLayout1.MultipleSelection = true;
            //SelectedEntityList = new List<ViewportLayout.SelectedItem>();
            //importCADForm.listBoxSelectedParts.Items.Clear();
        }

        private void barButtonSingleSelection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.ActionMode = actionType.SelectVisibleByPick;
            //viewportLayout1.Entities.ClearSelection();
            viewportLayout1.MultipleSelection = false;
            //SelectedEntityList = new List<ViewportLayout.SelectedItem>();
            //importCADForm.listBoxSelectedParts.Items.Clear();
        }
        private void barButtonRestoreOrientation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonHideObject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.ActionMode = actionType.None;
        }

        private void barButtonClearSelection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearSelection();
        }

        #endregion

        #region Initializing the Layers of the Entities which will be drawn onto the viewport
        public void InitializeLayers()
        {
            try
            {
                Layer Joints = new Layer("Joints", Color.White);
                Layer Bars = new Layer("Bars", Color.Orange);
                Layer Triangles = new Layer("Triangles");
                Layer Quads = new Layer("Quads");
                Layer Surfaces = new Layer("Surfaces", Color.Magenta);
                Layer IGES = new Layer("IGES");
                Layer Solid3D = new Layer("Solid3D");

                if (!viewportLayout1.Layers.Contains(Joints))
                {
                    viewportLayout1.Layers.Add(Joints);
                }
                if (!viewportLayout1.Layers.Contains(Bars))
                {
                    viewportLayout1.Layers.Add(Bars);
                }
                if (!viewportLayout1.Layers.Contains(Triangles))
                {
                    viewportLayout1.Layers.Add(Triangles);
                }
                if (!viewportLayout1.Layers.Contains(Quads))
                {
                    viewportLayout1.Layers.Add(Quads);
                }
                if (!viewportLayout1.Layers.Contains(Surfaces))
                {
                    viewportLayout1.Layers.Add(Surfaces);
                }
                if (!viewportLayout1.Layers.Contains(IGES))
                {
                    viewportLayout1.Layers.Add(IGES);
                }
                if (!viewportLayout1.Layers.Contains(Solid3D))
                {
                    viewportLayout1.Layers.Add(Solid3D);
                }
            }
            catch (Exception)
            {

                ///<remarks>
                /// In case layer is being readded
                /// </remarks>
            }

        }
        #endregion

        public void CloneOutputViewPort(ViewportLayout _outputViewPort, ViewportLayout _inputViewport)
        {
            for (int i = 0; i < _inputViewport.Layers.Count; i++)
            {
                if (i != 0)
                {
                    _outputViewPort.Layers.Add((Layer)_inputViewport.Layers[i].Clone());
                }
            }

            //foreach (string blockName in _inputViewport.Blocks.Keys)
            //{
            for (int i = 0; i < _inputViewport.Blocks.Count; i++)
            {
                //_outputViewPort.Blocks.Add(_inputViewport.Blocks[i].Name, (Block)_inputViewport.Blocks[i].Clone());
                _outputViewPort.Blocks.Add((Block)_inputViewport.Blocks[i].Clone());
            }
            //}

        }

        #region Plotter or Drawer methods

        #region Method to Plot the Load Case Force component arrows
        /// <summary>
        /// Plots the Force Components of the Bolts of the ARB Attachments, Steering Rack Attachments and Steering Column Attachments
        /// </summary>
        /// <param name="_modelLC">Object of the Vehicle Model</param>
        /// <param name="_isInitializing">Boolean variable to determine if the inputs are being drawing or the Outputs</param>
        /// <param name="_force_P_Left">Force Vector on the Top Left Attachment Bolt</param>
        /// <param name="_force_Q_Left">Force Vector on the Bottom Left Attachment Bolt</param>
        /// <param name="_force_P_Right">Force Vector on the Top Right Attachment Bolt</param>
        /// <param name="_force_Q_Right">Force Vector on the Bottom Right Attachment Bolt</param>
        public void PlotLoadCase(double[,] _leftAttach, double[,] _rightAttach, bool _isInitializing, bool _sRack, bool _sColumn, MathNet.Spatial.Euclidean.Vector3D _force_P_Left, MathNet.Spatial.Euclidean.Vector3D _force_Q_Left, MathNet.Spatial.Euclidean.Vector3D _force_P_Right, MathNet.Spatial.Euclidean.Vector3D _force_Q_Right)
        {
            int pos = 0;
            if (_sRack)
            {
                pos = 0;
            }
            else pos = 2;

            ///<summary>
            ///Plotting arrows for the Front Left and Front Right
            ///The notation "P" stands for front in the TOP VIEW of the Bolted system <seealso cref="VehicleModel.InitializeBoltedJointVariables(double[,], double[,], OutputClass, OutputClass, bool)"/>
            /// </summary>
            if (!_isInitializing) { PlotArrows(_leftAttach[0, pos], _leftAttach[1, pos], _leftAttach[2, pos], _force_P_Left.X, _force_P_Left.Y, _force_P_Left.Z, false); }
            viewportLayout1.Entities.Add(new Joint(_leftAttach[0, pos], _leftAttach[1, pos], _leftAttach[2, pos], 2, 2), 1);

            if (_sColumn)
            {
                ///<summary>
                ///If Steering Column is passed then it means that <c>_leftAttach</c> and <c>_rightAttach</c> mean the same thing <seealso cref="VehicleGUI.OutputDrawer(CAD, int, int, bool)"/> 
                ///So I need to increment pos by 1 so that the position 1 of the 3x2 array which is passed is accessed. 
                /// </summary>
                pos += 1;
                if (!_isInitializing) { PlotArrows(_rightAttach[0, pos], _rightAttach[1, pos], _rightAttach[2, pos], _force_P_Right.X, _force_P_Right.Y, _force_P_Right.Z, false); }
                viewportLayout1.Entities.Add(new Joint(_rightAttach[0, pos], _rightAttach[1, pos], _rightAttach[2, pos], 2, 2), 1);
            }
            else
            {
                if (!_isInitializing) { PlotArrows(_rightAttach[0, pos], _rightAttach[1, pos], _rightAttach[2, pos], _force_P_Right.X, _force_P_Right.Y, _force_P_Right.Z, false); }
                viewportLayout1.Entities.Add(new Joint(_rightAttach[0, pos], _rightAttach[1, pos], _rightAttach[2, pos], 2, 2), 1);
            }


            if (!_sColumn)
            {
                ///<summary>
                ///Plotting arrows for the REAR Left and Front Right
                ///The notation "Q" stands for rear in the TOP VIEW of the Bolted system <seealso cref="VehicleModel.InitializeBoltedJointVariables(double[,], double[,], OutputClass, OutputClass, bool)"/>
                /// </summary>
                if (!_isInitializing) { PlotArrows(_leftAttach[0, pos + 1], _leftAttach[1, pos + 1], _leftAttach[2, pos + 1], _force_Q_Left.X, _force_Q_Left.Y, _force_Q_Left.Z, false); }
                viewportLayout1.Entities.Add(new Joint(_leftAttach[0, pos + 1], _leftAttach[1, pos + 1], _leftAttach[2, pos + 1], 2, 2), 1);


                if (!_isInitializing) { PlotArrows(_rightAttach[0, pos + 1], _rightAttach[1, pos + 1], _rightAttach[2, pos + 1], _force_Q_Right.X, _force_Q_Right.Y, _force_Q_Right.Z, false); }
                viewportLayout1.Entities.Add(new Joint(_rightAttach[0, pos + 1], _rightAttach[1, pos + 1], _rightAttach[2, pos + 1], 2, 2), 1);
            }
        }
        #endregion

        #region Moment Arrow Plotter to represent Moments - NON FUNCTIONAL
        public void PlotMomentArrows(double _startX, double _startY, double _startZ, double _mx, double _my, double _mz)
        {
            ///<summary>
            ///Wheel CP Coordinates of the Wheel being considered
            /// </summary>
            Point3D momentArrowStart = new Point3D(_startX, _startY, _startZ);

            ///<summary>
            ///Vectors to allign the arrows in along the Axes
            /// </summary>
            Vector3D directionX = Vector3D.AxisX;
            Vector3D directionY = Vector3D.AxisY;
            Vector3D directionZ = Vector3D.AxisZ;

            ///<summary> Rolling Resistance Moment </summary>
            if (_mx < 0) { directionX.X *= -1; }
            ///<summary> Self Aligning Moment </summary>
            if (_my < 0) { directionY.Y *= -1; }
            ///<summary> Over turning Moment </summary>
            if (_mz < 0) { directionZ.Z *= -1; }

            double coneRadius = 10, coneLength = 18, cylinderRadius = 4;

            if (_mx != 0)
            {
                ///<summary>
                ///Try arrows and Labels. Both not working. 
                ///Putting same type arrows for Moment is not right and I'm not able to change the Arrow Head TYpe. 
                ///Labels are not workig because it says that the LABEL Class is abstract
                /// </summary>
            }

        }


        #endregion

        #region Arrow Plotter to represent Forces
        /// <summary>
        /// Decides which color 'value' represents. Scales the colour based ont he Ratio of Cell Value to (Max-Min) Value
        /// </summary>
        /// <param name="_cellValue">Current Value of the Force</param>
        /// <param name="_minValue">Max Force Value</param>
        /// <param name="_maxValue">Min Force Value</param>
        /// <returns></returns>
        public Color PaintArrow(double _cellValue, double _minValue, double _maxValue,Color _firstColour,Color _secondColour)
        {
            double val;

            int deltaR = _firstColour.R - _secondColour.R;
            int deltaG = _firstColour.G - _secondColour.G;
            int deltaB = _firstColour.B - _secondColour.B;

            if (_maxValue == _minValue)  //It avoids NotANumber results if max = min
            {
                val = 1;
            }
            else
            {
                val = ((_cellValue) - _minValue) / (_maxValue - _minValue);
            }
            int r = _secondColour.R + Convert.ToInt32(Math.Round((deltaR * (val))));
            int g = _secondColour.G + Convert.ToInt32(Math.Round((deltaG * (val))));
            int b = _secondColour.B + Convert.ToInt32(Math.Round((deltaB * (val))));

            return Color.FromArgb(255, r, g, b);
        }
        /// <summary>
        /// Method to Plot Arrows which will represent the XYZ forces acting at a particular Joint of interest. 
        /// </summary>
        /// <param name="_startX">X Coordinate of the Joint being considered</param>
        /// <param name="_startY">Y Coordinate of the Joint being considered</param>
        /// <param name="_startZ">Z Coordinate of the Joint being considered</param>
        /// <param name="_forceX">X Component of the Force on Joint being considered</param>
        /// <param name="_forceY">Y Component of the Force on Joint being considered</param>
        /// <param name="_forceZ">Z Component of the Force on Joint being considered</param>
        private void PlotArrows(double _startX, double _startY, double _startZ, double _forceX, double _forceY, double _forceZ, bool CPPassed)
        {
            Point3D arrowStart = new Point3D();
            if (!CPPassed)
            {
                arrowStart = new Point3D(_startX, _startY, _startZ);
            }
            else
            {
                arrowStart = new Point3D(_startX, _startY - (_forceY * 0.1) - 30, _startZ);
            }

            Color scaledLimeGreen = Color.FromArgb(50, 205, 50);
            Color scaledTurquoise = Color.FromArgb(64, 224, 208);
            Color scaledOrange = Color.FromArgb(255, 165, 0);

            ///<summary>
            ///Vectors to allign the arrows in along the Axes
            ////summary>
            Vector3D directionX = Vector3D.AxisX;
            Vector3D directionY = Vector3D.AxisY;
            Vector3D directionZ = Vector3D.AxisZ;

            if (_forceX < 0) { directionX.X *= -1; }
            if (_forceY < 0) { directionY.Y *= -1; }
            if (_forceZ < 0) { directionZ.Z *= -1; }

            ///<summary>
            ///Variables to hold the Arrrow Dimensions
            /// </summary>
            double coneRadius = 1.5, coneLength = 12, cylinderRadius = 1;
            if (CPPassed)
            {
                coneRadius = 10; coneLength = 30; cylinderRadius = 8;

            }

            ///<summary>
            ///Plotting the arrows along the directions of the Force Vectors
            /// </summary>
            /// <remarks>
            /// There is a 0.1 added to the Length of the arrow. This is so that in case, the force along a particular axis is zero, the vector will not fail because of incorrect length
            /// There is a 0.2 multiplied to the Length of the arrow. This is to scale the length as the length of the arrow is equal to the force along that axis
            /// </remarks>
            if (_forceX != 0)
            {
                Mesh arrowX = Mesh.CreateArrow(arrowStart, directionX, cylinderRadius, Math.Abs(_forceX + 0.01) * 0.1, coneRadius, coneLength, 10, Mesh.natureType.Smooth, Mesh.edgeStyleType.Sharp);
                viewportLayout1.Entities.Add(arrowX, 1, scaledLimeGreen);
                arrowX.MaterialName = Convert.ToString(_forceX);

            }

            if (_forceY != 0)
            {
                Mesh arrowY = Mesh.CreateArrow(arrowStart, directionY, cylinderRadius, Math.Abs(_forceY + 0.01) * 0.1, coneRadius, coneLength, 10, Mesh.natureType.Smooth, Mesh.edgeStyleType.Sharp);
                viewportLayout1.Entities.Add(arrowY, 1, scaledTurquoise);
                arrowY.MaterialName = Convert.ToString(_forceY);

            }

            if (_forceZ != 0)
            {
                Mesh arrowZ = Mesh.CreateArrow(arrowStart, directionZ, cylinderRadius, Math.Abs(_forceZ + 0.01) * 0.1, coneRadius, coneLength, 10, Mesh.natureType.Smooth, Mesh.edgeStyleType.Sharp);
                viewportLayout1.Entities.Add(arrowZ, 1, scaledOrange);
                arrowZ.MaterialName = Convert.ToString(_forceZ);

            }


        }
        #endregion

        #region Common Suspension Plotter
        /// <summary>
        /// Plots the Suspension Joints and Linkages which are common to both McPherson and Double Wishbone
        /// </summary>
        /// <param name="_scmPlotCommon">Object of the SuspensionCoordinateMaster Class</param>
        /// <param name="_isInitializing">Boolean variable to determine if the input items are being plotted or the Output </param>
        /// <param name="_ocColor">Object of the Output Class. Here is it used to determine the Color of the Wishbone</param>
        string damperName;
        private void PlotCommonSuspension(SuspensionCoordinatesMaster _scmPlotCommon, bool _isInitializing, OutputClass _ocColor, double _cPForcex, double _cPForcey, double _cPForcez, int _mcPhersonIdentifier)
        {
            Color color = Color.Orange;

            #region Common Joints

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.C1x, _scmPlotCommon.C1y, _scmPlotCommon.C1z, _ocColor.LowerRear_x, _ocColor.LowerRear_y, _ocColor.LowerRear_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Lower Rear Chassis", (new Joint(_scmPlotCommon.C1x, _scmPlotCommon.C1y, _scmPlotCommon.C1z, 5, 2)));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Lower Rear Chassis"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.D1x, _scmPlotCommon.D1y, _scmPlotCommon.D1z, _ocColor.LowerFront_x, _ocColor.LowerFront_y, _ocColor.LowerFront_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Lower Front Chassis", (new Joint(_scmPlotCommon.D1x, _scmPlotCommon.D1y, _scmPlotCommon.D1z, 5, 2)));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Lower Front Chassis"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.E1x, _scmPlotCommon.E1y, _scmPlotCommon.E1z, _ocColor.LBJ_x, _ocColor.LBJ_y, _ocColor.LBJ_z, false); }
            CoordinatesTemp.OutboardPickUp.Add("Lower Ball Joint", (new Joint(_scmPlotCommon.E1x, _scmPlotCommon.E1y, _scmPlotCommon.E1z, 5, 2)));
            viewportLayout1.Entities.Add(CoordinatesTemp.OutboardPickUp["Lower Ball Joint"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.J1x, _scmPlotCommon.J1y, _scmPlotCommon.J1z, _ocColor.DamperForce_x, _ocColor.DamperForce_y, _ocColor.DamperForce_z, false); }
            if (_mcPhersonIdentifier == 1)
            {
                damperName = "Damper Upright";
            }
            else
            {
                damperName = "Damper Bell-Crank";
            }

            CoordinatesTemp.InboardPickUp.Add(damperName, new Joint(_scmPlotCommon.J1x, _scmPlotCommon.J1y, _scmPlotCommon.J1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp[damperName], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.JO1x, _scmPlotCommon.JO1y, _scmPlotCommon.JO1z, _ocColor.DamperForce_x, _ocColor.DamperForce_y, _ocColor.DamperForce_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Damper Shock Mount", new Joint(_scmPlotCommon.JO1x, _scmPlotCommon.JO1y, _scmPlotCommon.JO1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Damper Shock Mount"], 1);

            CoordinatesTemp.OutboardPickUp.Add("Wheel Centre", new Joint(_scmPlotCommon.K1x, _scmPlotCommon.K1y, _scmPlotCommon.K1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.OutboardPickUp["Wheel Centre"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.M1x, _scmPlotCommon.M1y, _scmPlotCommon.M1z, _ocColor.ToeLink_x, _ocColor.ToeLink_y, _ocColor.ToeLink_z, false); }
            CoordinatesTemp.OutboardPickUp.Add("Steering Link Upright", new Joint(_scmPlotCommon.M1x, _scmPlotCommon.M1y, _scmPlotCommon.M1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.OutboardPickUp["Steering Link Upright"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.N1x, _scmPlotCommon.N1y, _scmPlotCommon.N1z, _ocColor.ToeLink_x, _ocColor.ToeLink_y, _ocColor.ToeLink_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Steering Link Chassis", new Joint(_scmPlotCommon.N1x, _scmPlotCommon.N1y, _scmPlotCommon.N1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Steering Link Chassis"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.P1x, _scmPlotCommon.P1y, _scmPlotCommon.P1z, _ocColor.ARBDroopLink_x, _ocColor.ARBDroopLink_y, _ocColor.ARBDroopLink_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Anti-Roll Bar Link", new Joint(_scmPlotCommon.P1x, _scmPlotCommon.P1y, _scmPlotCommon.P1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Anti-Roll Bar Link"], 1);

            CoordinatesTemp.InboardPickUp.Add("Anti-Roll Bar Chassis", new Joint(_scmPlotCommon.Q1x, _scmPlotCommon.Q1y, _scmPlotCommon.Q1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Anti-Roll Bar Chassis"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlotCommon.W1x, _scmPlotCommon.W1y, _scmPlotCommon.W1z, _cPForcex, _cPForcey, _cPForcez, true); }
            CoordinatesTemp.OutboardPickUp.Add("Contact Patch", new Joint(_scmPlotCommon.W1x, _scmPlotCommon.W1y, _scmPlotCommon.W1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.OutboardPickUp["Contact Patch"], 1);
            #endregion

            ///<remarks>
            ///The order of plotting is as follows ->
            ///While moving from the Centre of the Chassis outwards, the point which comes first is the start point of the Bar and hence is written first in the code to generate a bar
            /// </remarks>

            #region Common Bars
            ///<remarks>
            ///Determining the color of the Wishbone based on wheter the force in it is compressive or tensile. Compression is Positive and RED.Tension is NEGATIVE and BLUE
            /// </remarks>

            ///<summary>
            ///Lower Rear Wishbone
            /// </summary>
            if (!_isInitializing && _ocColor.LowerRear > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.LowerRear < 0) { color = Color.Blue; }
            Bar LowerRearArm = new Bar(CoordinatesTemp.InboardPickUp["Lower Rear Chassis"].Position, CoordinatesTemp.OutboardPickUp["Lower Ball Joint"].Position, 4.5, 8); ///<remarks>Need to use the dictionaries for all the plot commands shown below </remarks>
            CoordinatesTemp.SuspensionLinks.Add("LowerRearArm", LowerRearArm);
            viewportLayout1.Entities.Add(LowerRearArm, 2, color);
            if (_ocColor != null)
            {
                LowerRearArm.MaterialName = Convert.ToString(_ocColor.LowerRear);
            }


            ///<summary>
            ///Lower Front Wishbone
            /// </summary>
            if (!_isInitializing && _ocColor.LowerFront > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.LowerFront < 0) { color = Color.Blue; }
            Bar LowerFrontArm = new Bar(CoordinatesTemp.InboardPickUp["Lower Front Chassis"].Position, CoordinatesTemp.OutboardPickUp["Lower Ball Joint"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("LowerFrontArm", LowerFrontArm);
            viewportLayout1.Entities.Add(LowerFrontArm, 2, color);
            if (_ocColor != null)
            {
                LowerFrontArm.MaterialName = Convert.ToString(_ocColor.LowerFront);
            }

            ///<summary>
            ///Toe Link
            /// </summary>
            if (!_isInitializing && _ocColor.ToeLink > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.ToeLink < 0) { color = Color.Blue; }
            Bar ToeLink = new Bar(CoordinatesTemp.InboardPickUp["Steering Link Chassis"].Position, CoordinatesTemp.OutboardPickUp["Steering Link Upright"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("ToeLink", ToeLink);
            viewportLayout1.Entities.Add(ToeLink, 2, color);
            if (_ocColor != null)
            {
                ToeLink.MaterialName = Convert.ToString(_ocColor.ToeLink);
            }

            ///<summary>
            ///Damper 
            /// </summary>
            if (!_isInitializing && _ocColor.DamperForce > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.DamperForce < 0) { color = Color.Blue; }
            Bar Damper = new Bar(CoordinatesTemp.InboardPickUp["Damper Shock Mount"].Position, CoordinatesTemp.InboardPickUp[damperName].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("Damper", Damper);
            viewportLayout1.Entities.Add(Damper, 2, color);
            if (_ocColor != null)
            {
                Damper.MaterialName = Convert.ToString(_ocColor.DamperForce);
            }

            ///<summary>
            ///Anti Roll Bar Droop Link
            /// </summary>
            if (!_isInitializing && _ocColor.ARBDroopLink > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.ARBDroopLink < 0) { color = Color.Blue; }
            Bar ARBLever = new Bar(CoordinatesTemp.InboardPickUp["Anti-Roll Bar Chassis"].Position, CoordinatesTemp.InboardPickUp["Anti-Roll Bar Link"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("ARBLever", ARBLever);
            viewportLayout1.Entities.Add(ARBLever, 2, color);
            if (_ocColor != null)
            {
                ARBLever.MaterialName = Convert.ToString(_ocColor.ARBDroopLink);
            }

            //tV1 = new Point3D(CoordinatesMaster.OutboardPickUp["LowerBallJoint"].Position);
            //tV2 = new Point3D(CoordinatesMaster.InboardPickUp["DamperShockMount"].Position);
            //tV3 = new Point3D(CoordinatesMaster.OutboardPickUp["SteeringLinkUpright"].Position);
            #endregion
        }

        private void PlotCommonSuspension_ForceArrows(SuspensionCoordinatesMaster _scmPlotCommon, OutputClass _ocColor, double _cPForcex, double _cPForcey, double _cPForcez, int _mcPhersonIdentifier)
        {
            PlotArrows(_scmPlotCommon.C1x, _scmPlotCommon.C1y, _scmPlotCommon.C1z, _ocColor.LowerRear_x, _ocColor.LowerRear_y, _ocColor.LowerRear_z, false);
        }

        #region Maybe this a better method to add bars to the viewport. Scrapping this for now because no time to debug and test
        //private void CreateCommonBars(ref Dictionary<string, Joint> inBoardPoints, ref Dictionary<string, Joint> outBoardJoints, Dictionary<string, Bar> suspensionLinks)
        //{
        //    suspensionLinks = new Dictionary<string, Bar>();   
        //    Bar LowerRearArm = new Bar(outBoardJoints["LowerBallJoint"].Position, inBoardPoints["LowerRearArm"].Position, 4.5, 8);
        //    suspensionLinks.Add("LowerRearArm", LowerRearArm);

        //}

        //private void PlotCommonSuspension_Bars(CoordinateDatabase _coordinateMaster, bool _isInitializing, OutputClass _ocColor)
        //{
        //    Color color = Color.Orange;

        //    CreateCommonBars(ref _coordinateMaster.InboardPickUp, ref _coordinateMaster.OutboardPickUp, _coordinateMaster.SuspensionLinks);

        //    #region Common Bars
        //    ///<summary>
        //    ///Determining the color of the Wishbone based on wheter the force in it is compressive or tensile. Compression is Positive and RED.Tension is NEGATIVE and BLUE
        //    /// </remarks>

        //    ///<summary>
        //    ///Lower Rear Wishbone
        //    /// </summary>
        //    if (!_isInitializing && _ocColor.LowerRear > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.LowerRear < 0) { color = Color.Blue; }
        //    //Bar LowerRearArm = new Bar(_scmPlotCommon.E1x, _scmPlotCommon.E1y, _scmPlotCommon.E1z, _scmPlotCommon.C1x, _scmPlotCommon.C1y, _scmPlotCommon.C1z, 4.5, 8);
        //    //CoordinatesMaster.SuspensionLinks.Add("LowerRearArm", LowerRearArm);
        //    viewportLayout1.Entities.Add(_coordinateMaster.SuspensionLinks["LowerRearArm"], 2, color);
        //    if (_ocColor != null)
        //    {
        //        ///<remarks> Must be assigned only after addding the Entity to the Viewport. Otherwise you get error saying that the Material doesn't exist.  </remarks>
        //        _coordinateMaster.SuspensionLinks["LowerRearArm"].MaterialName = Convert.ToString(_ocColor.LowerRear);
        //    }


        //    /////<summary>
        //    /////Lower Front Wishbone
        //    ///// </summary>
        //    //if (!_isInitializing && _ocColor.LowerFront > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.LowerFront < 0) { color = Color.Blue; }
        //    //Bar LowerFrontArm = new Bar(_scmPlotCommon.E1x, _scmPlotCommon.E1y, _scmPlotCommon.E1z, _scmPlotCommon.D1x, _scmPlotCommon.D1y, _scmPlotCommon.D1z, 4.5, 8);
        //    //CoordinatesMaster.SuspensionLinks.Add("LowerFrontArm", LowerFrontArm);
        //    //viewportLayout1.Entities.Add(LowerFrontArm, 2, color);
        //    //if (_ocColor != null)
        //    //{
        //    //    LowerFrontArm.MaterialName = Convert.ToString(_ocColor.LowerFront);
        //    //}

        //    /////<summary>
        //    /////Toe Link
        //    ///// </summary>
        //    //if (!_isInitializing && _ocColor.ToeLink > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.ToeLink < 0) { color = Color.Blue; }
        //    //Bar ToeLink = new Bar(_scmPlotCommon.M1x, _scmPlotCommon.M1y, _scmPlotCommon.M1z, _scmPlotCommon.N1x, _scmPlotCommon.N1y, _scmPlotCommon.N1z, 4.5, 8);
        //    //CoordinatesMaster.SuspensionLinks.Add("ToeLink", ToeLink);
        //    //viewportLayout1.Entities.Add(ToeLink, 2, color);
        //    //if (_ocColor != null)
        //    //{
        //    //    ToeLink.MaterialName = Convert.ToString(_ocColor.ToeLink);
        //    //}

        //    /////<summary>
        //    /////Damper 
        //    ///// </summary>
        //    //if (!_isInitializing && _ocColor.DamperForce > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.DamperForce < 0) { color = Color.Blue; }
        //    //Bar Damper = new Bar(_scmPlotCommon.J1x, _scmPlotCommon.J1y, _scmPlotCommon.J1z, _scmPlotCommon.JO1x, _scmPlotCommon.JO1y, _scmPlotCommon.JO1z, 4.5, 8);
        //    //CoordinatesMaster.SuspensionLinks.Add("Damper", Damper);
        //    //viewportLayout1.Entities.Add(Damper, 2, color);
        //    //if (_ocColor != null)
        //    //{
        //    //    Damper.MaterialName = Convert.ToString(_ocColor.DamperForce);
        //    //}

        //    /////<summary>
        //    /////Anti Roll Bar Droop Link
        //    ///// </summary>
        //    //if (!_isInitializing && _ocColor.ARBDroopLink > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.ARBDroopLink < 0) { color = Color.Blue; }
        //    //Bar ARBDroopLink = new Bar(_scmPlotCommon.P1x, _scmPlotCommon.P1y, _scmPlotCommon.P1z, _scmPlotCommon.Q1x, _scmPlotCommon.Q1y, _scmPlotCommon.Q1z, 4.5, 8);
        //    //CoordinatesMaster.SuspensionLinks.Add("ARBDroopLink", ARBDroopLink);
        //    //viewportLayout1.Entities.Add(ARBDroopLink, 2, color);
        //    //if (_ocColor != null)
        //    //{
        //    //    ARBDroopLink.MaterialName = Convert.ToString(_ocColor.ARBDroopLink);
        //    //}

        //    //tV1 = new Point3D(_scmPlotCommon.E1x, _scmPlotCommon.E1y, _scmPlotCommon.E1z);
        //    //tV2 = new Point3D(_scmPlotCommon.J1x, _scmPlotCommon.J1y, _scmPlotCommon.J1z);
        //    //tV3 = new Point3D(_scmPlotCommon.M1x, _scmPlotCommon.M1y, _scmPlotCommon.M1z);
        //    #endregion
        //} 
        #endregion

        #endregion

        #region Double Wishbone Suspension Plotter
        string pushPullName;
        private void PlotDoubleWishboneSuspension(SuspensionCoordinatesMaster _scmPlot, bool _isInitializing, OutputClass _ocColor, int pushrodIdentifier)
        {

            Color color = Color.Orange;

            #region Double Wishbone Joints

            if (!_isInitializing) { PlotArrows(_scmPlot.A1x, _scmPlot.A1y, _scmPlot.A1z, _ocColor.UpperFront_x, _ocColor.UpperFront_y, _ocColor.UpperFront_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Upper Front Chassis", new Joint(_scmPlot.A1x, _scmPlot.A1y, _scmPlot.A1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Upper Front Chassis"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlot.B1x, _scmPlot.B1y, _scmPlot.B1z, _ocColor.UpperRear_x, _ocColor.UpperRear_y, _ocColor.UpperRear_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Upper Rear Chassis", new Joint(_scmPlot.B1x, _scmPlot.B1y, _scmPlot.B1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Upper Rear Chassis"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlot.F1x, _scmPlot.F1y, _scmPlot.F1z, _ocColor.UBJ_x, _ocColor.UBJ_y, _ocColor.UBJ_z, false); }
            CoordinatesTemp.OutboardPickUp.Add("Upper Ball Joint", new Joint(_scmPlot.F1x, _scmPlot.F1y, _scmPlot.F1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.OutboardPickUp["Upper Ball Joint"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlot.G1x, _scmPlot.G1y, _scmPlot.G1z, _ocColor.PushRod_x, _ocColor.PushRod_y, _ocColor.PushRod_z, false); }
            if (pushrodIdentifier == 1)
            {
                pushPullName = "Pushrod";
            }
            else
            {
                pushPullName = "Pullrod";
            }
            CoordinatesTemp.OutboardPickUp.Add(pushPullName + " Upright", new Joint(_scmPlot.G1x, _scmPlot.G1y, _scmPlot.G1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.OutboardPickUp[pushPullName + " Upright"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlot.H1x, _scmPlot.H1y, _scmPlot.H1z, _ocColor.PushRod_x, _ocColor.PushRod_y, _ocColor.PushRod_z, false); }
            CoordinatesTemp.InboardPickUp.Add(pushPullName + " Bell-Crank", new Joint(_scmPlot.H1x, _scmPlot.H1y, _scmPlot.H1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp[pushPullName + " Bell-Crank"], 1);

            CoordinatesTemp.InboardPickUp.Add("Bell Crank Pivot", new Joint(_scmPlot.I1x, _scmPlot.I1y, _scmPlot.I1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Bell Crank Pivot"], 1);

            if (!_isInitializing) { PlotArrows(_scmPlot.O1x, _scmPlot.O1y, _scmPlot.O1z, _ocColor.ARBDroopLink_x, _ocColor.ARBDroopLink_y, _ocColor.ARBDroopLink_z, false); }
            CoordinatesTemp.InboardPickUp.Add("Anti-Roll Bar Bell-Crank", new Joint(_scmPlot.O1x, _scmPlot.O1y, _scmPlot.O1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Anti-Roll Bar Bell-Crank"], 1);


            #endregion

            ///<remarks>
            ///The order of plotting is as follows ->
            ///While moving from the Centre of the Chassis outwards, the point which comes first is the start point of the Bar and hence is written first in the code to generate a bar
            /// </remarks>


            #region Bars and Trianlges

            ///<summary>
            ///Upper Front Wishbone
            /// </summary>
            if (!_isInitializing && _ocColor.UpperFront > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.UpperFront < 0) { color = Color.Blue; }
            Bar UpperFrontArm = new Bar(CoordinatesTemp.InboardPickUp["Upper Front Chassis"].Position, CoordinatesTemp.OutboardPickUp["Upper Ball Joint"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("UpperFrontArm", UpperFrontArm);
            viewportLayout1.Entities.Add(UpperFrontArm, 2, color);
            if (_ocColor != null)
            {
                UpperFrontArm.MaterialName = Convert.ToString(_ocColor.UpperFront);
            }

            ///<summary>
            ///Upper Rear Wishbone
            /// </summary>
            if (!_isInitializing && _ocColor.UpperRear > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.UpperRear < 0) { color = Color.Blue; }
            Bar UpperRearArm = new Bar(CoordinatesTemp.InboardPickUp["Upper Rear Chassis"].Position, CoordinatesTemp.OutboardPickUp["Upper Ball Joint"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("UpperRearArm", UpperRearArm);
            viewportLayout1.Entities.Add(UpperRearArm, 2, color);
            if (_ocColor != null)
            {
                UpperRearArm.MaterialName = Convert.ToString(_ocColor.UpperRear);
            }

            ///<summary>
            ///Pushrod 
            /// </summary>
            if (!_isInitializing && _ocColor.PushRod > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.PushRod < 0) { color = Color.Blue; }
            Bar Pushrod = new Bar(CoordinatesTemp.InboardPickUp[pushPullName + " Bell-Crank"].Position, CoordinatesTemp.OutboardPickUp[pushPullName + " Upright"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("Pushrod", Pushrod);
            viewportLayout1.Entities.Add(Pushrod, 2, color);
            if (_ocColor != null)
            {
                Pushrod.MaterialName = Convert.ToString(_ocColor.PushRod);
            }

            ///<summary>
            ///Bell Crank Arms
            /// </summary>
            Bar DamperBellcrankToPivot = new Bar(CoordinatesTemp.InboardPickUp["Damper Bell-Crank"].Position, CoordinatesTemp.InboardPickUp["Bell Crank Pivot"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("DamperBellcrankToPivot", DamperBellcrankToPivot);
            viewportLayout1.Entities.Add(DamperBellcrankToPivot, 2);
            ///<remarks>Point I comes before H while going from centre of the Vehicle towards thr wheel </remarks>
            Bar PushrodBellcrankToPivot = new Bar(CoordinatesTemp.InboardPickUp["Bell Crank Pivot"].Position, CoordinatesTemp.InboardPickUp[pushPullName + " Bell-Crank"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("PushrodBellcrankToPivot", PushrodBellcrankToPivot);
            viewportLayout1.Entities.Add(PushrodBellcrankToPivot, 2);
            ///<remarks>Point I comes before O while going from the Centre of the Vehicle towards the Wheel </remarks>
            Bar ARBDroopLinkBellcrankToPivot = new Bar(CoordinatesTemp.InboardPickUp["Bell Crank Pivot"].Position, CoordinatesTemp.InboardPickUp["Anti-Roll Bar Bell-Crank"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("ARBDroopLinkBellcrankToPivot", ARBDroopLinkBellcrankToPivot);
            viewportLayout1.Entities.Add(ARBDroopLinkBellcrankToPivot, 2);

            ///<summary>
            ///Points to form the triangles joining the Bell-Crank vectors
            /// </summary>
            Point3D bellCrankTriangle_I = CoordinatesTemp.InboardPickUp["Bell Crank Pivot"].Position;
            Point3D bellCrankTriangle_J = CoordinatesTemp.InboardPickUp["Damper Bell-Crank"].Position;
            Point3D bellCrankTriangle_H = CoordinatesTemp.InboardPickUp[pushPullName + " Bell-Crank"].Position;
            Point3D bellCrankTriangle_O = CoordinatesTemp.InboardPickUp["Anti-Roll Bar Bell-Crank"].Position;

            ///<summary> 
            ///Triangles joining the Bell-Crank Vectors
            /// </summary>
            viewportLayout1.Entities.Add(new Triangle(bellCrankTriangle_I, bellCrankTriangle_J, bellCrankTriangle_H), 4, Color.Orange);
            viewportLayout1.Entities.Add(new Triangle(bellCrankTriangle_I, bellCrankTriangle_H, bellCrankTriangle_O), 4, Color.Orange);

            ///<summary>
            ///ARB Droop Link 
            /// </summary>
            if (!_isInitializing && _ocColor.ARBDroopLink > 0) { color = Color.Red; } else if (!_isInitializing && _ocColor.ARBDroopLink < 0) { color = Color.Blue; }
            Bar ARBDroopLink = new Bar(CoordinatesTemp.InboardPickUp["Anti-Roll Bar Bell-Crank"].Position, CoordinatesTemp.InboardPickUp["Anti-Roll Bar Link"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("ARBDroopLink", ARBDroopLink);
            viewportLayout1.Entities.Add(ARBDroopLink, 2, color);
            if (_ocColor != null)
            {
                ARBDroopLink.MaterialName = Convert.ToString(_ocColor.ARBDroopLink);
            }
            #endregion
        }
        #endregion

        #region TARB Suspension Plotter
        private void TARBPlotter(SuspensionCoordinatesMaster _scmPlotTARB)
        {
            #region TARB Joint
            CoordinatesTemp.InboardPickUp.Add("Torsion Bar Bottom Pivot", new Joint(_scmPlotTARB.R1x, _scmPlotTARB.R1y, _scmPlotTARB.R1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Torsion Bar Bottom Pivot"], 1);
            #endregion

            #region TARB Bar
            Bar TARB = new Bar(CoordinatesTemp.InboardPickUp["Anti-Roll Bar Chassis"].Position, CoordinatesTemp.InboardPickUp["Torsion Bar Bottom Pivot"].Position, 4.5, 8);
            CoordinatesTemp.SuspensionLinks.Add("TARB", TARB);
            viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["TARB"], 2);
            #endregion
        }
        #endregion

        #region Anti Roll Bar Connector
        public void ARBConnector(Dictionary<string, Joint> _inboardLeft, Dictionary<string, Joint> _inboardRight)
        {
            viewportLayout1.Entities.Add(new Bar(_inboardLeft["Anti-Roll Bar Chassis"].Position, _inboardRight["Anti-Roll Bar Chassis"].Position, 3, 8));
        }
        #endregion

        #region Steerng System Plotter
        public void SteeringCSystemPlotter(SuspensionCoordinatesMaster _scmSteeringLeft, SuspensionCoordinatesMaster _scmSteeringRight, Dictionary<string, Joint> _inboardLeft, Dictionary<string, Joint> _inboardRight)
        {
            // Pinion Point
            CoordinatesTemp.InboardPickUp.Add("Pinion Centre", new Joint(_scmSteeringLeft.Pin1x, _scmSteeringLeft.Pin1y, _scmSteeringLeft.Pin1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Pinion Centre"], 1);

            // UV2 Point (if applicable)
            if (_scmSteeringLeft.NoOfCouplings == 2)
            {
                CoordinatesTemp.InboardPickUp.Add("Pinion Universal Joint", new Joint(_scmSteeringLeft.UV2x, _scmSteeringLeft.UV2y, _scmSteeringLeft.UV2z, 5, 2));
                viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Pinion Universal Joint"], 1);
            }

            // UV1 Point
            CoordinatesTemp.InboardPickUp.Add("Steering Shaft Universal Joint", new Joint(_scmSteeringLeft.UV1x, _scmSteeringLeft.UV1y, _scmSteeringLeft.UV1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Steering Shaft Universal Joint"], 1);

            // Steering Shaft Chassis Mount
            CoordinatesTemp.InboardPickUp.Add("Steering Shaft Support Chassis", new Joint(_scmSteeringLeft.STC1x, _scmSteeringLeft.STC1y, _scmSteeringLeft.STC1z, 5, 2));
            viewportLayout1.Entities.Add(CoordinatesTemp.InboardPickUp["Steering Shaft Support Chassis"], 1);

            //Steering Column
            Bar RackHousing = new Bar(_inboardLeft["Steering Link Chassis"].Position, _inboardRight["Steering Link Chassis"].Position, 12, 8);
            CoordinatesTemp.SuspensionLinks.Add("RackHousing", RackHousing);
            viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["RackHousing"], 2);

            // Pinion to UV2 or UV1 (whichever is applicable
            if (_scmSteeringLeft.NoOfCouplings == 2)
            {
                Bar Pinon1ToUV2 = new Bar(CoordinatesTemp.InboardPickUp["Pinion Centre"].Position, CoordinatesTemp.InboardPickUp["Pinion Universal Joint"].Position, 4.5, 8);
                CoordinatesTemp.SuspensionLinks.Add("Pinon1ToUV2", Pinon1ToUV2);
                viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["Pinon1ToUV2"], 2);

                Bar UV2ToUV1 = new Bar(CoordinatesTemp.InboardPickUp["Pinion Universal Joint"].Position, CoordinatesTemp.InboardPickUp["Steering Shaft Universal Joint"].Position, 4.5, 8);
                CoordinatesTemp.SuspensionLinks.Add("UV2ToUV1", UV2ToUV1);
                viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["UV2ToUV1"], 2);

                Bar UV1TOSTC = new Bar(CoordinatesTemp.InboardPickUp["Steering Shaft Universal Joint"].Position, CoordinatesTemp.InboardPickUp["Steering Shaft Support Chassis"].Position, 4.5, 8);
                CoordinatesTemp.SuspensionLinks.Add("UV1TOSTC", UV1TOSTC);
                viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["UV1TOSTC"], 2);
            }
            else
            {
                Bar Pinon1ToUV1 = new Bar(CoordinatesTemp.InboardPickUp["Pinion Centre"].Position, CoordinatesTemp.InboardPickUp["Steering Shaft Universal Joint"].Position, 4.5, 8);
                CoordinatesTemp.SuspensionLinks.Add("Pinon1ToUV1", Pinon1ToUV1);
                viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["Pinon1ToUV1"], 2);

                Bar UV1TOSTC = new Bar(CoordinatesTemp.InboardPickUp["Steering Shaft Universal Joint"].Position, CoordinatesTemp.InboardPickUp["Steering Shaft Support Chassis"].Position, 4.5, 8);
                CoordinatesTemp.SuspensionLinks.Add("UV1TOSTC", UV1TOSTC);
                viewportLayout1.Entities.Add(CoordinatesTemp.SuspensionLinks["UV1TOSTC"], 2);
            }
        }
        #endregion

        #region Caster Triangle Plotter
        private void PlotCasterTriangle(SuspensionCoordinatesMaster _scmCT, int Identifier)
        {
            #region Plotting the Caster Triangle

            #region Front Caster Triangle
            if (_scmCT.DoubleWishboneIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
            {
                Triangle CasterTriangle = new Triangle(CoordinatesTemp.OutboardPickUp["Lower Ball Joint"].Position, CoordinatesTemp.OutboardPickUp["Steering Link Upright"].Position, CoordinatesTemp.OutboardPickUp["Upper Ball Joint"].Position);
                viewportLayout1.Entities.Add(CasterTriangle, 3, Color.Orange);

                Vector3D vector3D = CasterTriangle.Normal;

            }
            else if (_scmCT.McPhersonIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
            {
                Triangle CasterTriangle = new Triangle(CoordinatesTemp.OutboardPickUp["Lower Ball Joint"].Position, CoordinatesTemp.OutboardPickUp["Steering Link Upright"].Position, CoordinatesTemp.OutboardPickUp["Damper Bell-Crank"].Position);
                viewportLayout1.Entities.Add(CasterTriangle, 3, Color.Orange);
            }
            #endregion



            #region Rear Caster Triangle
            else if (_scmCT.DoubleWishboneIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
            {
                Triangle CasterTriangle = new Triangle(CoordinatesTemp.OutboardPickUp["Lower Ball Joint"].Position, CoordinatesTemp.OutboardPickUp["Steering Link Upright"].Position, CoordinatesTemp.OutboardPickUp["Upper Ball Joint"].Position);
                viewportLayout1.Entities.Add(CasterTriangle, 3, Color.Orange);
            }
            else if (_scmCT.McPhersonIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
            {
                Triangle CasterTriangle = new Triangle(CoordinatesTemp.OutboardPickUp["Lower Ball Joint"].Position, CoordinatesTemp.OutboardPickUp["Steering Link Upright"].Position, CoordinatesTemp.OutboardPickUp["Damper Bell-Crank"].Position);
                viewportLayout1.Entities.Add(CasterTriangle, 3, Color.Orange);
            }

            #endregion

            #endregion
        }
        #endregion

        #region Wheel Plotter
        Vector3D wTV;

        #region Rotating for Camber and Toe
        public Solid3D RotateWheel(Solid3D _tire, WheelAlignment _waRotate, Vector3D _wheelCentre, int _identifierRotate, bool _isInitialzing)
        {

            _tire.Translate(-_wheelCentre.X, -_wheelCentre.Y, -_wheelCentre.Z);
            if (_isInitialzing)
            {

                if ((_identifierRotate == 1 || _identifierRotate == 3))
                {

                    _tire.Rotate(-(_waRotate.StaticCamber * (Math.PI / 180)), Vector3D.AxisZ);
                    _tire.Rotate(/*-*/(_waRotate.StaticToe * (Math.PI / 180)), Vector3D.AxisY);
                }
                else if (_identifierRotate == 2 || _identifierRotate == 4)
                {
                    _tire.Rotate((_waRotate.StaticCamber * (Math.PI / 180)), Vector3D.AxisZ);
                    _tire.Rotate(-(_waRotate.StaticToe * (Math.PI / 180)), Vector3D.AxisY);
                }
            }

            else if (!_isInitialzing)
            {

                ///<remarks>
                ///This portion is really confusing. According to me, the sign convention followed by C# and Eyeshot10 is CW is positive and CCW is negative. But when the wheel is steered, there is a nagtive oe value
                ///but the whee is steered to the right that is CW
                /// </remarks>
                _tire.Rotate((_waRotate.StaticCamber), Vector3D.AxisZ);
                _tire.Rotate((_waRotate.StaticToe), Vector3D.AxisY);
            }

            _tire.Translate(_wheelCentre.X, _wheelCentre.Y, _wheelCentre.Z);

            return _tire;
        }
        #endregion

        Line wheelAxis; Circle wheelCircleOuter, wheelCircleInner;

        #region Drawing the Wheel
        private void PlotWheel(SuspensionCoordinatesMaster _scmPlotWheel, WheelAlignment _waPlotWheel, int _identifierPlotWheel, bool _isInitializing)
        {
            int sign = 1;
            if (_identifierPlotWheel == 1 || _identifierPlotWheel == 3)
            { sign = -1; }
            else sign = 1;

            #region Drawing the Wheel
            Point3D wheelCentrePoint = new Point3D(_scmPlotWheel.K1x - (-sign * 78.784), _scmPlotWheel.K1y, _scmPlotWheel.K1z);
            Plane wheelPlane = new Plane(wheelCentrePoint, Vector3D.AxisZ, Vector3D.AxisY);
            wheelCircleOuter = new Circle(wheelPlane, wheelCentrePoint, 223.8);
            wheelCircleInner = new Circle(wheelPlane, wheelCentrePoint, 125);

            //wheel = Solid.CreateTorus(223.8, 75, 100, 100);

            wTV = new Vector3D(_scmPlotWheel.K1x, _scmPlotWheel.K1y, _scmPlotWheel.K1z);

            //wheel.Rotate(Math.PI / 2, YAxis); // Making it parallel to the vehicle

            //wheel.Translate(wTV);

            devDept.Eyeshot.Entities.Region tireRegion = new devDept.Eyeshot.Entities.Region(wheelCircleOuter, wheelCircleInner);
            Solid3D Tire = tireRegion.ExtrudeAsSolid3D((sign * 157.48), 0);

            if (_waPlotWheel != null)
            {
                Tire = RotateWheel(Tire, _waPlotWheel, wTV, _identifierPlotWheel, _isInitializing);
            }

            viewportLayout1.Entities.Add(Tire, 6, Color.DarkGray);
            #endregion
        }
        #endregion

        #endregion

        #region Stand Plotter
        public void DrawStands(SuspensionCoordinatesMaster _scmStandFL, SuspensionCoordinatesMaster _scmStandFR, SuspensionCoordinatesMaster _scmStandRL, SuspensionCoordinatesMaster _scmStandRR)
        {
            StandFL = new Point3D(_scmStandFL.D1x, _scmStandFL.D1y, _scmStandFL.D1z);
            StandFR = new Point3D(_scmStandFR.D1x, _scmStandFR.D1y, _scmStandFR.D1z);
            StandRL = new Point3D(_scmStandRL.C1x, _scmStandRL.C1y, _scmStandRL.C1z);
            StandRR = new Point3D(_scmStandRR.C1x, _scmStandRR.C1y, _scmStandRR.C1z);

            Stand = new Quad(StandFL, StandFR, StandRR, StandRL);
            viewportLayout1.Entities.Add(Stand, 4, Color.White);

            SupportBarStart = new Point3D((_scmStandFL.D1x + _scmStandFR.D1x) / 2, 0, (_scmStandFL.D1z + _scmStandRL.D1z) / 2);
            SupportBarEnd = new Point3D((_scmStandFL.D1x + _scmStandFR.D1x) / 2, _scmStandFL.D1y, (_scmStandFL.D1z + _scmStandRL.D1z) / 2);

            SupportBar = new Bar(SupportBarStart, SupportBarEnd, 130, 8);


            viewportLayout1.Entities.Add(SupportBar, 2, Color.White);
            viewportLayout1.Entities.Add(new Joint(SupportBarStart, 130, 2), 1, Color.White);

        }
        #endregion

        #region Plotting the SUrfaces

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="_deltaX"></param>
        /// <param name="_deltaY"></param>
        /// <param name="_deltaZ"></param>
        private void RearFrame(int identifier, double _deltaX, double _deltaY, double _deltaZ)
        {

            int sign = 1;
            if (identifier == 1 || identifier == 3) sign = -1;
            else if (identifier == 2 || identifier == 4) sign = 1;

            ///<remarks>
            ///Plotting the first member of the Rear Frame
            ///Connectd to <see cref="sidePanelEndCurvePoints[2]"/>
            ///</remarks>
            barStart_1stHalf = RollHoopPointCalculater(barStart_1stHalf, sidePanelEndCurvePoints[2], 0, 0, 0, 0, sign);
            barEnd_1stHalf = RollHoopPointCalculater(barEnd_1stHalf, new Point3D(), 0, _deltaX, _deltaY, _deltaZ, sign);
            PlotRollHoop(barStart_1stHalf, barEnd_1stHalf);

            ///<remarks>
            ///Plotting the 2nd member
            ///Connected to the 1st member and vertical 
            /// </remarks>
            barStart_2ndHalf = RollHoopPointCalculater(barStart_2ndHalf, barEnd_1stHalf, 0, 0, 0, 0, sign);
            barEnd_2ndHalf = RollHoopPointCalculater(barEnd_2ndHalf, barStart_2ndHalf, 0, 14, -87, 45, sign);
            PlotRollHoop(barStart_2ndHalf, barEnd_2ndHalf);

            ///<remarks>
            ///Plotting the 3rd member
            ///Connrect to the 2nd member and joining <see cref="sidePanelEndCurvePoints[3]"/>
            /// </remarks>
            barStart_2ndHalf = RollHoopPointCalculater(barStart_2ndHalf, barEnd_2ndHalf, 0, 0, 0, 0, sign);
            barEnd_2ndHalf = RollHoopPointCalculater(barEnd_2ndHalf, sidePanelEndCurvePoints[3], 0, 0, 0, 0, sign);
            PlotRollHoop(barStart_2ndHalf, barEnd_2ndHalf);

        }

        /// <summary>
        /// Calculates the Point of interest of the Roll Hoop based on a passed refernce Point. The reference for the bottom portion is the Side Panel. Reference for Top Portion is the LAST POINT of the bottom portion and hence delta with respect to that point needs to be passed
        /// </summary>
        /// <param name="_rollHoopPoint">Roll Hoop Point of Interest</param>
        /// <param name="_referencePoint">Reference Point</param>
        /// <param name="_scaleFactor">Factor to Scale the Reference Point by. Used only for Bottom Portion</param>
        /// <param name="_deltaX">Delta of X coordinate between the Roll Hoop point of interest and the Reference Point. Used only for the Top Portion</param>
        /// <param name="_deltaY">Delta of Y coordinate between the Roll Hoop point of interest and the Reference Point. Used only for the Top Portion</param>
        /// <param name="_deltaZ">Delta of Z coordinate between the Roll Hoop point of interest and the Reference Point. Used only for the Top Portion</param>
        /// <param name="_sign"></param>
        /// <returns></returns>
        private Point3D RollHoopPointCalculater(Point3D _rollHoopPoint, Point3D _referencePoint, double _scaleFactor, double _deltaX, double _deltaY, double _deltaZ, int _sign)
        {
            //_rollHoopPoint = new Point3D();

            _rollHoopPoint.X = _referencePoint.X + (_sign * (_scaleFactor * _referencePoint.X)) + (_sign * _deltaX);
            _rollHoopPoint.Y = _referencePoint.Y + _deltaY;
            _rollHoopPoint.Z = _referencePoint.Z + ((_scaleFactor * _referencePoint.Z)) + _deltaZ;

            return _rollHoopPoint;
        }

        /// <summary>
        /// Plots the Roll Hoop onto the View Port 
        /// </summary>
        /// <param name="_barStart">Start Point of the Roll Hoop Bar</param>
        /// <param name="_barEnd">End Point of the Roll Hoop Bar</param>
        private void PlotRollHoop(Point3D _barStart, Point3D _barEnd)
        {
            Bar rollHoopBar = new Bar(_barStart, _barEnd, 10, 8);

            Joint rollHoopBar_Joint = new Joint(barEnd_1stHalf, 11, 2);

            viewportLayout1.Entities.Add(rollHoopBar_Joint, 1, Color.DarkGray);
            viewportLayout1.Entities.Add(rollHoopBar, 2, Color.DarkGray);
        }

        /// <summary>
        /// Represents the START or END point of the Bars in the First Half. This maybe any section of the group of Bars which take their reference from an external structure. For example, the Bottom Portion of the Roll Hoop takes reference from the Side Panel and hence is the 1st 
        /// half
        /// </summary>
        Point3D barStart_1stHalf = new Point3D();
        Point3D barEnd_1stHalf = new Point3D();
        /// <summary>
        /// Represents the START or END point of the Bars in the SECOND Half. This maybe any section of the group of Bars which take their reference from the First Half of the Bar group.For example, the Top Portion of the Roll Hoop takes reference from the First Half of the Bar Group
        /// </summary>
        Point3D barStart_2ndHalf = new Point3D();
        Point3D barEnd_2ndHalf = new Point3D();
        /// <summary>
        /// Invokes the method which plots the Roll Hop onto the View Port
        /// </summary>
        /// <param name="identifier">Corner Identifier</param>
        private void InvokeRollHoopPlotter(int identifier)
        {
            int sign = 1;
            if (identifier == 1 || identifier == 3) sign = -1;
            else if (identifier == 2 || identifier == 4) sign = 1;

            ///<remarks>
            ///Plotting the first portion of the MRH (Botom Portion)
            ///The Side Panel End Curve Points are taken as the reference
            /// </remarks>
            barStart_1stHalf = RollHoopPointCalculater(barStart_1stHalf, sidePanelEndCurvePoints[3], 0.01, 0, 0, 0, sign); /*sidePanelEndCurvePoints[3]*/;
            barEnd_1stHalf = RollHoopPointCalculater(barEnd_1stHalf, sidePanelEndCurvePoints[2], 0.01, 0, 0, 0, sign);
            ///<remarks>Approximating the MRH Bar points using the end point of the side pannel<seealso cref="sidePanelEndCurvePoints"/></remarks>
            PlotRollHoop(barStart_1stHalf, barEnd_1stHalf);

            ///<remarks>
            ///Plotting the second portion of the MRH
            /// </remarks>
            barStart_1stHalf = RollHoopPointCalculater(barStart_1stHalf, sidePanelEndCurvePoints[2], 0.01, 0, 0, 0, sign);
            barEnd_1stHalf = RollHoopPointCalculater(barEnd_1stHalf, sidePanelEndCurvePoints[1], 0.01, 0, 0, 0, sign);
            ///<remarks>Approximating the MRH Bar points using the end point of the side pannel<seealso cref="sidePanelEndCurvePoints"/></remarks>
            PlotRollHoop(barStart_1stHalf, barEnd_1stHalf);

            ///<remarks>
            ///Plotting the 3rd Portion of the MRH
            /// </remarks>
            barStart_1stHalf = RollHoopPointCalculater(barStart_1stHalf, sidePanelEndCurvePoints[1], 0.01, 0, 0, 0, sign);
            barEnd_1stHalf = RollHoopPointCalculater(barEnd_1stHalf, sidePanelEndCurvePoints[0], 0.01, 0, 0, 0, sign);
            ///<remarks>Approximating the MRH Bar points using the end point of the side pannel<seealso cref="sidePanelEndCurvePoints"/></remarks>
            PlotRollHoop(barStart_1stHalf, barEnd_1stHalf);

            ///Plotting the the 4th Portion of Roll Hoop 
            ///<remarks>
            ///This Portion is Plotted by taking the previous point as Reference instead of taking the Side Panel end as the Reference
            ///Delta Z needs to be taken as negative. This is because uptil now, Z coordinate was calculate as a percentage of the existing coordinate. So percentage of a negative number, is a negative number so no need of additional negative sign. But now since, it just pure 
            ///subtraction which is used to calculate delta, minus is needed. 
            /// </remarks>
            barStart_2ndHalf = RollHoopPointCalculater(barStart_2ndHalf, barEnd_1stHalf, 0, 0, 0, 0, sign);
            barEnd_2ndHalf = RollHoopPointCalculater(barEnd_2ndHalf, barEnd_1stHalf, 0, 150, 498, -19.19, sign);
            ///<remarks>Approximating the MRH Bar points using the end point of the previous Roll Hoop Point<seealso cref="barStart_1stHalf"/> <seealso cref="barEnd_1stHalf"/> </remarks>
            PlotRollHoop(barStart_2ndHalf, barEnd_2ndHalf);

            ///Plotting the the 5th Portion of Roll Hoop 
            ///<remarks>
            ///This Portion is Plotted by taking the previous point as Reference instead of taking the Side Panel end as the Reference
            ///Delta Z needs to be taken as negative. This is because uptil now, Z coordinate was calculate as a percentage of the existing coordinate. So percentage of a negative number, is a negative number so no need of additional negative sign. But now since, it just pure 
            ///subtraction which is used to calculate delta, minus is needed. 
            /// </remarks>
            barStart_2ndHalf = RollHoopPointCalculater(barStart_2ndHalf, barEnd_2ndHalf, 0, 0, 0, 0, sign);
            barEnd_2ndHalf = RollHoopPointCalculater(barEnd_2ndHalf, barEnd_2ndHalf, 0, 76, 56, -2, sign);
            ///<remarks>Approximating the MRH Bar points using the end point of the previous Roll Hoop Point<seealso cref="barStart_1stHalf"/> <seealso cref="barEnd_1stHalf"/> </remarks>
            PlotRollHoop(barStart_2ndHalf, barEnd_2ndHalf);

            ///Plotting the the 6th Portion of Roll Hoop 
            ///<remarks>
            ///This Portion is Plotted by taking the previous point as Reference instead of taking the Side Panel end as the Reference
            ///Delta Z needs to be taken as negative. This is because uptil now, Z coordinate was calculate as a percentage of the existing coordinate. So percentage of a negative number, is a negative number so no need of additional negative sign. But now since, it just pure 
            ///subtraction which is used to calculate delta, minus is needed. 
            /// </remarks>
            barStart_2ndHalf = RollHoopPointCalculater(barStart_2ndHalf, barEnd_2ndHalf, 0, 0, 0, 0, sign);
            barEnd_2ndHalf = RollHoopPointCalculater(barEnd_2ndHalf, barEnd_2ndHalf, 0, 75, 0, 0, sign);
            ///<remarks>Approximating the MRH Bar points using the end point of the previous Roll Hoop Point<seealso cref="barStart_1stHalf"/> <seealso cref="barEnd_1stHalf"/> </remarks>
            PlotRollHoop(barStart_2ndHalf, barEnd_2ndHalf);


        }

        /// <summary>
        /// Plots the Nose of th FS Vehicle 
        /// </summary>
        /// <param name="identifier">Corner beingn considered</param>
        private void PlotNose(int identifier)
        {
            int sign = 1;
            if (identifier == 1 || identifier == 3) sign = -1;
            else if (identifier == 2 || identifier == 4) sign = 1;

            ///<remarks>
            ///The Nose is a looped part, that is, there is no left or hand side to it like the side panel. Despite that fact, it is split into left and right hand side for ease of drawing.
            ///The List of NoseCurvePoints are initialized using the Side Panel start curve points and then 3 additional points are added to the top and bottm and hence are inserted in that same chronological order into the List. 
            /// </remarks>
            List<Point3D> halfNoseCurvePoints_Start = new List<Point3D>
            {
                new Point3D(0, sidePanelStartCurvePoints[0].Y + 56, sidePanelStartCurvePoints[0].Z),
                new Point3D(sidePanelStartCurvePoints[0].X + (sign * 58), sidePanelStartCurvePoints[0].Y + 33, sidePanelStartCurvePoints[0].Z),
                new Point3D(sidePanelStartCurvePoints[0].X, sidePanelStartCurvePoints[0].Y, sidePanelStartCurvePoints[0].Z),
                new Point3D(sidePanelStartCurvePoints[1].X, sidePanelStartCurvePoints[1].Y, sidePanelStartCurvePoints[1].Z),
                new Point3D(sidePanelStartCurvePoints[2].X, sidePanelStartCurvePoints[2].Y, sidePanelStartCurvePoints[2].Z),
                new Point3D(sidePanelStartCurvePoints[3].X, sidePanelStartCurvePoints[3].Y, sidePanelStartCurvePoints[3].Z),
                new Point3D(0, sidePanelStartCurvePoints[3].Y, sidePanelStartCurvePoints[3].Z)
            };
            ///<remarks> Calculating the Curve of the start of the Nose </remarks>
            Curve halfNoseCurveStart = Curve.GlobalInterpolation(halfNoseCurvePoints_Start, 2);

            ///<remarks>
            ///The middle off the nose Cone is assumed to be exactly similar to the Start of the Nose cone but scaled 
            ///The scaling factors are abritrary and approximate
            /// </remarks>
            List<Point3D> halfNoseCurvePoints_Middle = new List<Point3D>
            {
                new Point3D(halfNoseCurvePoints_Start[0].X - (0.2 * halfNoseCurvePoints_Start[0].X), halfNoseCurvePoints_Start[0].Y - (0.1 * halfNoseCurvePoints_Start[0].Y), sidePanelStartCurvePoints[3].Z + 480),
                new Point3D(halfNoseCurvePoints_Start[1].X - (0.2 * halfNoseCurvePoints_Start[1].X), halfNoseCurvePoints_Start[1].Y - (0.1 * halfNoseCurvePoints_Start[1].Y), sidePanelStartCurvePoints[3].Z + 480),
                new Point3D(halfNoseCurvePoints_Start[2].X - (0.2 * halfNoseCurvePoints_Start[2].X), halfNoseCurvePoints_Start[2].Y - (0.1 * halfNoseCurvePoints_Start[2].Y), sidePanelStartCurvePoints[3].Z + 480),
                new Point3D(halfNoseCurvePoints_Start[3].X - (0.2 * halfNoseCurvePoints_Start[3].X), halfNoseCurvePoints_Start[3].Y - (0.1 * halfNoseCurvePoints_Start[3].Y), sidePanelStartCurvePoints[3].Z + 480),
                new Point3D(halfNoseCurvePoints_Start[4].X - (0.19 * halfNoseCurvePoints_Start[4].X), halfNoseCurvePoints_Start[4].Y                                          , sidePanelStartCurvePoints[3].Z + 480),
                new Point3D(halfNoseCurvePoints_Start[5].X - (0.3 * halfNoseCurvePoints_Start[5].X), halfNoseCurvePoints_Start[5].Y + (0.9 * halfNoseCurvePoints_Start[5].Y),  sidePanelStartCurvePoints[3].Z + 480),
                new Point3D(halfNoseCurvePoints_Start[6].X - (0.2 * halfNoseCurvePoints_Start[6].X), halfNoseCurvePoints_Start[6].Y + (0.9 * halfNoseCurvePoints_Start[6].Y),  sidePanelStartCurvePoints[3].Z + 480)
            };

            Curve halfNoseCurveMiddle = Curve.GlobalInterpolation(halfNoseCurvePoints_Middle, 2);

            ///<remarks>
            ///The Far end of the Nose Cone is assumed to be exactly similar to the Start of the Nose cone but scaled 
            ///The scaling factors are abritrary and approximate
            /// </remarks>
            List<Point3D> halfNoseCurvePoints_End = new List<Point3D>
            {
                new Point3D(halfNoseCurvePoints_Start[0].X - (0.4 * halfNoseCurvePoints_Start[0].X), halfNoseCurvePoints_Start[0].Y - (0.4 * halfNoseCurvePoints_Start[0].Y), sidePanelStartCurvePoints[3].Z + 960),
                new Point3D(halfNoseCurvePoints_Start[1].X - (0.4 * halfNoseCurvePoints_Start[1].X), halfNoseCurvePoints_Start[1].Y - (0.4 * halfNoseCurvePoints_Start[1].Y), sidePanelStartCurvePoints[3].Z + 960),
                new Point3D(halfNoseCurvePoints_Start[2].X - (0.4 * halfNoseCurvePoints_Start[2].X), halfNoseCurvePoints_Start[2].Y - (0.4 * halfNoseCurvePoints_Start[2].Y), sidePanelStartCurvePoints[3].Z + 960),
                new Point3D(halfNoseCurvePoints_Start[3].X - (0.4 * halfNoseCurvePoints_Start[3].X), halfNoseCurvePoints_Start[3].Y - (0.4 * halfNoseCurvePoints_Start[3].Y), sidePanelStartCurvePoints[3].Z + 960),
                new Point3D(halfNoseCurvePoints_Start[4].X - (0.38 * halfNoseCurvePoints_Start[4].X), halfNoseCurvePoints_Start[4].Y                                         , sidePanelStartCurvePoints[3].Z + 960),
                new Point3D(halfNoseCurvePoints_Start[5].X - (0.6 * halfNoseCurvePoints_Start[5].X), halfNoseCurvePoints_Start[5].Y + (1.5 * halfNoseCurvePoints_Start[5].Y), sidePanelStartCurvePoints[3].Z + 960),
                new Point3D(halfNoseCurvePoints_Start[6].X - (0.4 * halfNoseCurvePoints_Start[6].X), halfNoseCurvePoints_Start[6].Y + (1.5 * halfNoseCurvePoints_Start[6].Y), sidePanelStartCurvePoints[3].Z + 960)
            };
            ///<remarks> Calculating the Curve of the Far end of the Nose </remarks>
            Curve halffNoseCurveEnd = Curve.GlobalInterpolation(halfNoseCurvePoints_End, 2);


            Surface halfNose = Surface.Loft(new ICurve[] { halfNoseCurveStart, halfNoseCurveMiddle, halffNoseCurveEnd }, 1)[0];

            //viewportLayout1.Entities.Add(halfNose_StartToMiddle, 4, Color.DarkGray);
            viewportLayout1.Entities.Add(halfNose, 4, Color.DarkGray);

        }

        /// <summary>
        /// This method uses the 3D Points to obtain a Curve. The 3D points are obtained by totalling a base point with the delta of the curve point from that point
        /// </summary>
        /// <param name="_tempCurvePoint">List of Points which will be added to the Curve</param>
        /// <param name="_deltaFrom_ReferencePickUp">List of deltas of the Curve Points from the Refereence Wishbone's Chassis Pick Up Point. Passed in the form of a List ONLY to reduce the number of parameters passed to this function </param>
        /// <param name="_scmCurveP">Object of the SuspensionCoordinateMaster Class which contains the coordinates of the LOWER REAR  </param>
        /// <param name="_chassisPU_x"> The coordinate X of the Chassis Pick Up Point which is taken as the referece to calculate the delta of the Curve Point from </param>
        /// <param name="_chassisPU_y"> The coordinate Y of the Chassis Pick Up Point which is taken as the referece to calculate the delta of the Curve Point from </param>
        /// <param name="_chassisPU_z"> The coordinate Z of the Chassis Pick Up Point which is taken as the referece to calculate the delta of the Curve Point from </param>
        /// <returns>Creates and RETURNS a curve point after creating it through Global Interpolation</returns>
        private Curve AddCurvePoints(List<double> _deltaFrom_ReferencePickUp, double _chassisPU_x, double _chassisPU_y, double _chassisPU_z, out List<Point3D> CalculateCurvePoints)
        {
            Curve returnCurve;

            List<Point3D> _tempCurvePoint = new List<Point3D>();

            _tempCurvePoint.Add(new Point3D(_chassisPU_x + _deltaFrom_ReferencePickUp[0], _chassisPU_y + _deltaFrom_ReferencePickUp[4], _chassisPU_z + _deltaFrom_ReferencePickUp[8]));
            _tempCurvePoint.Add(new Point3D(_chassisPU_x + _deltaFrom_ReferencePickUp[1], _chassisPU_y + _deltaFrom_ReferencePickUp[5], _chassisPU_z + _deltaFrom_ReferencePickUp[9]));
            _tempCurvePoint.Add(new Point3D(_chassisPU_x + _deltaFrom_ReferencePickUp[2], _chassisPU_y + _deltaFrom_ReferencePickUp[6], _chassisPU_z + _deltaFrom_ReferencePickUp[10]));
            _tempCurvePoint.Add(new Point3D(_chassisPU_x + _deltaFrom_ReferencePickUp[3], _chassisPU_y + _deltaFrom_ReferencePickUp[7], _chassisPU_z + _deltaFrom_ReferencePickUp[11]));

            returnCurve = Curve.GlobalInterpolation(_tempCurvePoint, 1);

            CalculateCurvePoints = _tempCurvePoint;

            return returnCurve;
        }

        /// <summary>
        /// Plots the Side Panel
        /// </summary>
        /// <param name="_chassisPU_Front_x">X Coordinate of the Reference Point of the Front Side. LOWER REAR Chassis Pick up is used for Front</param>
        /// <param name="_chassisPU_Front_y">Y Coordinate of the Reference Point of the Front Side. LOWER REAR Chassis Pick up is used for Front</param>
        /// <param name="_chassisPU_Front_z">Z Coordinate of the Reference Point of the Front Side. LOWER REAR Chassis Pick up is used for Front</param>
        /// <param name="_chassisPU_Rear_x"> X Coordinate of the Reference Point of the Rear Side. LOWER FRONT Chassis Pick up is used for Front</param>
        /// <param name="_chassisPU_Rear_y"> Y Coordinate of the Reference Point of the Rear Side. LOWER FRONT Chassis Pick up is used for Front</param>
        /// <param name="_chassisPU_Rear_z"> Z Coordinate of the Reference Point of the Rear Side. LOWER FRONT Chassis Pick up is used for Front</param>
        /// <param name="_identifier"></param>
        private void PlotSidePanel(double _chassisPU_Front_x, double _chassisPU_Front_y, double _chassisPU_Front_z, double _chassisPU_Rear_x, double _chassisPU_Rear_y, double _chassisPU_Rear_z, int _identifier)
        {
            int sign = 1;

            if (_identifier == 1 || _identifier == 3) sign = 1;
            else if (_identifier == 2 || _identifier == 4) sign = -1;

            ///<remarks>Side Panel Start Curve Points</remarks>
            sidePanelStartCurvePoints = new List<Point3D>();
            ///<remarks>
            ///FRONT
            ///From data obtained through Solidworks, the distances off the Curve Points from the Lower Rear Chassis Pick up are observed and then totalled from the Chassis Pick up Points
            ///<c> deltaFrom_FLRPickup </c> stands for the deltas of the Curve Points from the FRONT LOWER REAR Wishbone's Chassis Pick Up Point
            ///These deltas are added to a List ONLY so that they can be passed to the <seealso cref="AddCurvePoints(List{Point3D}, Vehicle)"/> method without the need of too many parameters
            /// </remarks>
            List<double> deltaFrom_FLRPickup = new List<double>
            {
                sign * -27.5,sign * -3.5,sign * -9,sign * -33, 505, 442, 240, -20, -31, -31, -30, -31
            };
            Curve startCurve = AddCurvePoints(deltaFrom_FLRPickup, _chassisPU_Front_x, _chassisPU_Front_y, _chassisPU_Front_z, out sidePanelStartCurvePoints);

            ///<remarks>Side Pane END Curve Points</remarks>
            sidePanelEndCurvePoints = new List<Point3D>();
            ///<remarks>
            ///REAR
            ///From data obtained through Solidworks, the distances off the Curve Points from the LOWER FRONT Chassis Pick up are observed and then totalled from the Chassis Pick up Points
            ///<c> deltaFrom_LRPickup </c> stands for the deltas of the Curve Points from the REAR LOWER FRONT Wishbone's Chassis Pick Up Point
            ///These deltas are added to a List ONLY so that they can be passed to the <seealso cref="AddCurvePoints(List{Point3D}, Vehicle)"/> method without the need of too many parameters
            /// </remarks>
            List<double> deltaFrom_RLFPickup = new List<double>
            {
                sign * 64.5,sign * 68.5,sign * 73,sign * -13.5, 486, 464, 189, -14, 25, 24, 35, 43
            };
            Curve sidePanelendCurve = AddCurvePoints(deltaFrom_RLFPickup, _chassisPU_Rear_x, _chassisPU_Rear_y, _chassisPU_Rear_z, out sidePanelEndCurvePoints);

            ///<remarks>
            ///This list contains the Points of the MIDDLE Curve. 
            /// </remarks>
            List<Point3D> sidePanelMiddleCurveP = new List<Point3D>
            {
                new Point3D((sidePanelStartCurvePoints[0].X + sidePanelEndCurvePoints[0].X)/2 + sign*100, (sidePanelStartCurvePoints[0].Y + sidePanelEndCurvePoints[0].Y) / 2, (sidePanelStartCurvePoints[0].Z + sidePanelEndCurvePoints[0].Z) / 2),
                new Point3D((sidePanelStartCurvePoints[1].X + sidePanelEndCurvePoints[1].X)/2 + sign*100, (sidePanelStartCurvePoints[1].Y + sidePanelEndCurvePoints[1].Y) / 2, (sidePanelStartCurvePoints[1].Z + sidePanelEndCurvePoints[1].Z) / 2),
                new Point3D((sidePanelStartCurvePoints[2].X + sidePanelEndCurvePoints[2].X)/2 + sign*100, (sidePanelStartCurvePoints[2].Y + sidePanelEndCurvePoints[2].Y) / 2, (sidePanelStartCurvePoints[2].Z + sidePanelEndCurvePoints[2].Z) / 2),
                new Point3D((sidePanelStartCurvePoints[3].X + sidePanelEndCurvePoints[3].X)/2 + sign*100, (sidePanelStartCurvePoints[3].Y + sidePanelEndCurvePoints[3].Y) / 2, (sidePanelStartCurvePoints[3].Z + sidePanelEndCurvePoints[3].Z) / 2),
            };
            Curve sidePanelMiddleCurve = Curve.GlobalInterpolation(sidePanelMiddleCurveP, 1);

            Surface sidePanel = Surface.Loft(new ICurve[] { startCurve, sidePanelMiddleCurve, sidePanelendCurve }, 1)[0];

            viewportLayout1.Entities.Add(sidePanel, 4, Color.DarkGray);
        }

        /// <summary>
        /// Draws the Vehicle Body: Nose, Side Panels and Roll Hoop is drawn
        /// </summary>
        /// <param name="chassisPU_Front_x"> X Coordinate of the Reference Point of the Front Side. LOWER REAR Chassis Pick up is used for Front</param>
        /// <param name="chassisPU_Front_y"> Y Coordinate of the Reference Point of the Front Side. LOWER REAR Chassis Pick up is used for Front</param>
        /// <param name="chassisPU_Front_z"> Z Coordinate of the Reference Point of the Front Side. LOWER REAR Chassis Pick up is used for Front</param>
        /// <param name="chassisPU_Rear_x">  X Coordinate of the Reference Point of the Rear Side. LOWER FRONT Chassis Pick up is used for Front</param>
        /// <param name="chassisPU_Rear_y">  Y Coordinate of the Reference Point of the Rear Side. LOWER FRONT Chassis Pick up is used for Front</param>
        /// <param name="chassisPU_Rear_z">  Z Coordinate of the Reference Point of the Rear Side. LOWER FRONT Chassis Pick up is used for Front</param>
        /// <param name="identifier">Corner Identifier</param>
        public void DrawVehicleBody_TillRollHoop(double chassisPU_Front_x, double chassisPU_Front_y, double chassisPU_Front_z, double chassisPU_Rear_x, double chassisPU_Rear_y, double chassisPU_Rear_z, double chassisPU_RearFrame_x, double chassisPU_RearFrame_y, double chassisPU_RearFrame_z, int identifier)
        {
            ///<summary>
            ///Plotting the Side Panel
            /// </summary>
            PlotSidePanel(chassisPU_Front_x, chassisPU_Front_y, chassisPU_Front_z, chassisPU_Rear_x, chassisPU_Rear_y, chassisPU_Rear_z, identifier);

            ///<summary>
            ///Plotting Half the Nose
            /// </summary>
            PlotNose(identifier);

            ///<summary>
            ///Plotting Half the Roll Hoop
            /// </summary>
            InvokeRollHoopPlotter(identifier);

            ///<summary>
            ///Plotting Half the Rear Frame
            /// </summary>
            RearFrame(identifier, chassisPU_RearFrame_x + 5, chassisPU_RearFrame_y + 5, chassisPU_RearFrame_z + 5);
        }



        #endregion

        #region Methods to edit the Suspension Items. Used when any changes are brought about to the Suspension Items using the CAD 
        /// <summary>
        /// Method to Invoke the Modify Suspension Methods
        /// </summary>
        /// <param name="indexSus_FL">Index of the Front Left selected Suspension</param>
        /// <param name="indexSus_FR">Index of the Front Right selected Suspension</param>
        /// <param name="indexSus_RL">Index of the Rear Left selected Suspension</param>
        /// <param name="indexSus_RR">Index of the Rear RIght selected Suspension</param>
        internal void ModifySuspensionItem(int indexSus_FL, int indexSus_FR, int indexSus_RL, int indexSus_RR)
        {
            ModifySuspensionItem(CoordinatesFL, indexSus_FL, 1);
            ModifySuspensionItem(CoordinatesFR, indexSus_FR, 2);
            ModifySuspensionItem(CoordinatesRL, indexSus_RL, 3);
            ModifySuspensionItem(CoordinatesRR, indexSus_RR, 4);
        }

        /// <summary>
        /// Method to obtain the DataTable of the Suspension which is being considered for modification
        /// </summary>
        /// <param name="_indexSus">Indec of the Suspension Item</param>
        /// <param name="_identifer">Identifier variable to detemine the corner being considered</param>
        /// <returns>Data Table of the Suspension Corner being considered</returns>
        private DataTable GetSuspensionDataTable(int _indexSus, int _identifer)
        {
            R1 = Kinematics_Software_New.AssignFormVariable();

            if (_identifer == 1 && R1.scflGUI[_indexSus].SCFLDataTableGUI != null)
            {
                return R1.scflGUI[_indexSus].SCFLDataTableGUI;
            }
            else if (_identifer == 2 && R1.scfrGUI[_indexSus].SCFRDataTableGUI != null)
            {
                return R1.scfrGUI[_indexSus].SCFRDataTableGUI;
            }
            else if (_identifer == 3 && R1.scrlGUI[_indexSus].SCRLDataTableGUI != null)
            {
                return R1.scrlGUI[_indexSus].SCRLDataTableGUI;
            }
            else if (_identifer == 4 && R1.scrrGUI[_indexSus].SCRRDataTableGUI != null)
            {
                return R1.scrrGUI[_indexSus].SCRRDataTableGUI;
            }
            else return null;
        }

        /// <summary>
        /// Method to modifiy the Suspension using its DataTable. 
        /// </summary>
        /// <param name="_coordinateMaster">CoordinateDatabase Object of the Corner being considered</param>
        /// <param name="indexSus">Index of the Suspension being considered</param>
        /// <param name="identifier">Identifier variable to detemine the corner being considered</param>
        private void ModifySuspensionItem(CoordinateDatabase _coordinateMaster, int indexSus, int identifier)
        {
            ///<summary>Updating the Main Form's Object</summary>
            R1 = Kinematics_Software_New.AssignFormVariable();
            ///<summary>Getting the DataTable</summary>
            DataTable masterTable = GetSuspensionDataTable(indexSus, identifier);
            ///<summary>Ensuring that the DataTable is not null to prevent exception</summary>
            if (masterTable != null)
            {
                ///<summary>Checking each row of the Data Table and matching it with the CoordinateDatabase object and then editing the columns of the DataTable</summary>

                for (int i = 0; i < masterTable.Rows.Count; i++)
                {
                    foreach (string key in _coordinateMaster.InboardPickUp.Keys)
                    {
                        if (masterTable.Rows[i].Field<string>(0) == key)
                        {
                            masterTable.Rows[i].SetField<double>("X (mm)", _coordinateMaster.InboardPickUp[key].Position.Z);
                            masterTable.Rows[i].SetField<double>("Y (mm)", _coordinateMaster.InboardPickUp[key].Position.X);
                            masterTable.Rows[i].SetField<double>("Z (mm)", _coordinateMaster.InboardPickUp[key].Position.Y);
                            break;
                        }
                    }

                    foreach (string key in _coordinateMaster.OutboardPickUp.Keys)
                    {
                        if (masterTable.Rows[i].Field<string>(0) == key)
                        {
                            masterTable.Rows[i].SetField<double>("X (mm)", _coordinateMaster.OutboardPickUp[key].Position.Z);
                            masterTable.Rows[i].SetField<double>("Y (mm)", _coordinateMaster.OutboardPickUp[key].Position.X);
                            masterTable.Rows[i].SetField<double>("Z (mm)", _coordinateMaster.OutboardPickUp[key].Position.Y);
                            break; ;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to check if the left and right suspensions are STILL symmatric. 
        /// </summary>
        /// <param name="_leftSuspension">Data table of the Left Suspension</param>
        /// <param name="_rightSuspension">Data Table of the Right Suspension</param>
        /// <returns>Boolean value indicating symmetry</returns>
        public bool CheckSymmetry(CoordinateDatabase _leftSuspension, CoordinateDatabase _rightSuspension, bool _wasSymmetric)
        {
            bool symmetricityBroken = false;
            ///<summary>Using a foreach loop to determine if every single point in the Inboard point of the left and right are equal</summary>
            foreach (string key in _leftSuspension.InboardPickUp.Keys)
            {
                if (_leftSuspension.InboardPickUp[key].Position != _rightSuspension.InboardPickUp[key].Position)
                {
                    symmetricityBroken = true;
                }
            }
            ///<summary>Using a foreach loop to determine if every single point in the Outboard point of the left and right are equal </summary>
            foreach (string key in _leftSuspension.OutboardPickUp.Keys)
            {
                if (_leftSuspension.OutboardPickUp[key].Position != _rightSuspension.OutboardPickUp[key].Position)
                {
                    symmetricityBroken = true;
                }
            }
            ///<summary>Returning boolean </summary>
            if ((symmetricityBroken && _wasSymmetric) || (!symmetricityBroken && !_wasSymmetric) || (symmetricityBroken && !_wasSymmetric))
            {
                return false;
            }

            else
            {
                return true;
            }
        }
        #endregion

        #endregion

        #region Plotter Invoker
        /// <summary>
        /// Plots the entire Suspension of the Vehicle. Also imports CAD of the chassis if it has not yet been imported
        /// </summary>
        /// <param name="_scm">Object of the SuspensionCoordinateMaster Class</param>
        /// <param name="Identifier">Corner Identifier</param>
        /// <param name="_wa">Object of the WheelAlignment Class</param>
        /// <param name="_IsInitializing">Boolean variable to determine if the Input items are being plotted or the Output Items</param>
        /// <param name="_ocPlotter"> Object of the OutputClass. Here it is needed to color the Wishbones during Output display. NULL for Input calls</param>
        public void SuspensionPlotterInvoker(SuspensionCoordinatesMaster _scm, int Identifier, WheelAlignment _wa, bool _IsInitializing, bool _PlotWheel, OutputClass _ocPlotter, double _CPForceX, double _CPForceY, double _CPForceZ)
        {
            CoordinatesTemp = new CoordinateDatabase();

            string d = viewportLayout1.SerialNumber;
            #region Invoking the Plotter for all the four corners of the Suspension

            #region Invoking the Common Plotter (Commnon to both McPherson and Double Wishbone
            if (Identifier == 1 || Identifier == 2)
            {
                PlotCommonSuspension(_scm, _IsInitializing, _ocPlotter, _CPForceX, _CPForceY, _CPForceZ, _scm.McPhersonIdentifierFront);
            }
            else if (Identifier == 3 || Identifier == 4)
            {
                PlotCommonSuspension(_scm, _IsInitializing, _ocPlotter, _CPForceX, _CPForceY, _CPForceZ, _scm.McPhersonIdentifierRear);

            }
            #endregion

            #region Invoking the Double Wishbone Plotter
            if (_scm.DoubleWishboneIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
            {
                PlotDoubleWishboneSuspension(_scm, _IsInitializing, _ocPlotter, _scm.PushrodIdentifierFront);

                if (_scm.TARBIdentifierFront == 1 && (Identifier == 1 || Identifier == 2))
                {
                    TARBPlotter(_scm);
                }

            }
            if (_scm.DoubleWishboneIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
            {

                PlotDoubleWishboneSuspension(_scm, _IsInitializing, _ocPlotter, _scm.PushrodIdentifierRear);

                if (_scm.TARBIdentifierRear == 1 && (Identifier == 3 || Identifier == 4))
                {
                    TARBPlotter(_scm);
                }
            }
            #endregion

            #region Invoking the Caster Triangle Plotter
            PlotCasterTriangle(_scm, Identifier);
            #endregion

            #region Invoking the Spring and Wheel Plotter
            if (_PlotWheel)
            {
                PlotWheel(_scm, _wa, Identifier, _IsInitializing);

            }

            //PlotSpring(_scm);
            #endregion

            #endregion

            #region Assigning the Dictionary of the Suspension Links and Coordinates based on which Corner of the Vehicle is calling this method
            if (Identifier == 1)
            {
                CoordinatesFL = CoordinatesTemp;
            }
            else if (Identifier == 2)
            {
                CoordinatesFR = CoordinatesTemp;
            }
            else if (Identifier == 3)
            {
                CoordinatesRL = CoordinatesTemp;
            }
            else if (Identifier == 4)
            {
                CoordinatesRR = CoordinatesTemp;
            }
            #endregion



        }

        public void PlotForceArrows(OutputClass _ocForce)
        {
            _ocForce.PopulateForceLists(_ocForce);
        }

        #endregion

        #region Suspension Coordinate Mapping Functions
        Joint coordinatePoint;
        public bool MapSuspensionCoordinate()
        {
            bool MappingSuccessful = false;

            ///<summary>Creating List of Cylindrical and Planar Surfaces</summary>
            List<CylindricalSurface> _selectedCYLS = new List<CylindricalSurface>();
            List<PlanarSurface> _selectedPLN = new List<PlanarSurface>();

            bool planeSelected = false;
            ///<summary>Populating the lists with the selected entities</summary>
            for (int i = 0; i < SelectedEntityList.Count; i++)
            {
                ///<summary>Passing the Selected Cylindrical surfaces to the List of Cylindrical Surfaces</summary>
                if (SelectedEntityList[i].Item as CylindricalSurface != null)
                {
                    _selectedCYLS.Add((CylindricalSurface)SelectedEntityList[i].Item);
                }
                ///<summary>Passing the Selected Planar surfaces to the List of Planar Surfaces</summary>
                else if (SelectedEntityList[i].Item as PlanarSurface != null)
                {
                    _selectedPLN.Add((PlanarSurface)SelectedEntityList[i].Item);
                    planeSelected = true;
                }

            }
            ///<summary>If user wants to create a point with only 2 cylindrical sufraces and their centre points and an axis passing through them. On the Mid Point of the Axis</summary>
            if (!planeSelected && SelectedEntityList.Count >= 2)
            {

                for (int i = 0; i < _selectedCYLS.Count; i++)
                {
                    if ((i + 1 != _selectedCYLS.Count))
                    {
                        if (_selectedCYLS[i].Center != _selectedCYLS[i + 1].Center)
                        {
                            ///<summary>Drawing an axis between the 2 selected cylindrical surfacees</summary>
                            Line axisLine = new Line(_selectedCYLS[i].Center, _selectedCYLS[i + 1].Center);
                            ///<summary>Creating the Joint which represents the Coordinate Point</summary>
                            coordinatePoint = new Joint(axisLine.MidPoint, 5, 2);

                            if (!viewportLayout1.Entities.Contains(coordinatePoint))
                            {
                                ///<summary>Adding the joint to the viewport ONLY if it doesn't already exist. This IF loop will not allow more than 1 joint for a Suspension Coordinate</summary>
                                //viewportLayout1.Entities.Add(coordinatePoint, 1, Color.Cyan);
                                MappingSuccessful = true;
                            }
                        }
                    }
                }
            }
            ///<summary>If the user wants to create a point using the 2 planes, an axis through 2 concentric cylindrical surface</summary>
            else if (planeSelected && SelectedEntityList.Count >= 4)
            {
                ///<summary>Generating a new Line in the MathNet.Spatial domain</summary>
                MathNet.Spatial.Euclidean.Point3D startP = new MathNet.Spatial.Euclidean.Point3D(_selectedCYLS[0].Center.X, _selectedCYLS[0].Center.Y, _selectedCYLS[0].Center.Z);
                MathNet.Spatial.Euclidean.Point3D endP = new MathNet.Spatial.Euclidean.Point3D(_selectedCYLS[1].Center.X, _selectedCYLS[1].Center.Y, _selectedCYLS[1].Center.Z);
                if (startP.Y > endP.Y)
                {
                    MathNet.Spatial.Euclidean.Point3D tempPoint = new MathNet.Spatial.Euclidean.Point3D(startP.X, startP.Y, startP.Z);
                    startP = new MathNet.Spatial.Euclidean.Point3D();
                    startP = endP;
                    endP = new MathNet.Spatial.Euclidean.Point3D();
                    endP = tempPoint;
                }

                MathNet.Spatial.Euclidean.Line3D axisLin = new MathNet.Spatial.Euclidean.Line3D(startP, endP);
                //double slope = Math.Atan((axisLin.EndPoint.Y - axisLin.StartPoint.Y) / (axisLin.EndPoint.Z - axisLin.StartPoint.Z));
                //double intercept = (axisLin.EndPoint.Y - (slope * axisLin.EndPoint.Z));

                //startP = new MathNet.Spatial.Euclidean.Point3D(startP.X, startP.Y / 2, (((startP.Y / 2) - intercept) / slope));
                //endP = new MathNet.Spatial.Euclidean.Point3D(endP.X, endP.Y * 2, (((endP.Y * 2) - intercept) / slope));
                //axisLin = new MathNet.Spatial.Euclidean.Line3D(startP, endP);

                ///<remarks>Just changing the Y Coordinatate of the Start and End Point to make sure the axis passes through the plan on both the ends. Sometimes it happened that w/o this code the axis didn't intersect</remarks>
                startP = new MathNet.Spatial.Euclidean.Point3D(startP.X, startP.Y - 5, (startP.Z));
                endP = new MathNet.Spatial.Euclidean.Point3D(endP.X, endP.Y + 5, endP.Z);
                axisLin = new MathNet.Spatial.Euclidean.Line3D(startP, endP);

                MathNet.Spatial.Euclidean.Plane plane1 = new MathNet.Spatial.Euclidean.Plane(_selectedPLN[0].Plane.Equation.X, _selectedPLN[0].Plane.Equation.Y, _selectedPLN[0].Plane.Equation.Z, _selectedPLN[0].Plane.Equation.D);
                MathNet.Spatial.Euclidean.Plane plane2 = new MathNet.Spatial.Euclidean.Plane(_selectedPLN[1].Plane.Equation.X, _selectedPLN[1].Plane.Equation.Y, _selectedPLN[1].Plane.Equation.Z, _selectedPLN[1].Plane.Equation.D);
                //MathNet.Spatial.Euclidean.Vector3D vector3D = new MathNet.Spatial.Euclidean.Vector3D();

                ///<summary>Calculating the intersection points of the Planes with the Axis</summary>
                MathNet.Spatial.Euclidean.Point3D? intersection1 = plane1.IntersectionWith(axisLin);
                MathNet.Spatial.Euclidean.Point3D? intersection2 = plane2.IntersectionWith(axisLin);

                if (intersection1 != null && intersection2 != null)
                {
                    Line axisLineMain = new Line(intersection1.Value.X, intersection1.Value.Y, intersection1.Value.Z, intersection2.Value.X, intersection2.Value.Y, intersection2.Value.Z);
                    coordinatePoint = new Joint(axisLineMain.MidPoint, 5, 2);
                    if (!viewportLayout1.Entities.Contains(coordinatePoint))
                    {
                        ///<remarks>
                        ///Removing this code here and adding it in the <see cref="DictionaryOfSusCoordinates_UpdateDictionary(CoordinateDatabase, string)" method/>
                        ///This is because if I addd this here then the viewport doesn't have a CoordinateDatabase object and HENCE the UNMAP Function will not work then 
                        /// </remarks>
                        //viewportLayout1.Entities.Add(coordinatePoint, 1, Color.Cyan);
                        MappingSuccessful = true;
                    }

                }
            }
            else
            {
                MessageBox.Show("Appropriate Reference Surfaces not selected");
                MappingSuccessful =  false;
            }



            viewportLayout1.UpdateViewportLayout();
            viewportLayout1.Refresh();
            return MappingSuccessful;

        }

        /// <summary>
        /// Method to fnd Block to which the selected item belongs
        /// </summary>
        public void FindBlockOfSelectedItem(out string _blockName)
        {
            _blockName = null;
            if (SelectedEntityList.Count != 0)
            {
                //foreach (string key in viewportLayout1.Blocks.Keys)
                for (int jBlock = 0; jBlock < viewportLayout1.Blocks.Count; jBlock++)
                {
                    for (int iSelEnt = 0; iSelEnt < SelectedEntityList.Count; iSelEnt++)
                    {
                        if (viewportLayout1.Blocks[/*key*/jBlock].Entities.Contains((Entity)SelectedEntityList[iSelEnt].Item))
                        {
                            _blockName = /*key*/ viewportLayout1.Blocks[jBlock].Name;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Exclusive Method to add the Steering System joints to the Front Left CoordinateDatabase object
        /// </summary>
        /// <param name="_steeringCoordinates"></param>
        /// <param name="_coordinateDatabase"></param>
        public void DictionaryOfSuspensionCoordinate_InitializeDictionary_OnlySteering(List<string> _steeringCoordinates, CoordinateDatabase _coordinateDatabase)
        {
            foreach (string item in _steeringCoordinates)
            {
                _coordinateDatabase.InboardPickUp.Add(item, new Joint(0, 0, 0, 3, 2));
            }
        }


        /// <summary>
        /// Method to Initialize the Suspension Dictionaries if the user is going for the option of creating a Suspension using Mapping 
        /// </summary>
        /// <param name="_listBoxSusCoordinatesInboard">ListBoxControl containing the Inboard Coordinates</param>
        /// <param name="_listBoxSusCoordinatesOutboard">ListBoxControl containing the Outboard Coordinates</param>
        /// <param name="_coordinateMaster"></param>
        public void DictionaryOfSusCoordinates_InitializeDictionary(/*ListBoxControl _listBoxSusCoordinatesInboard, ListBoxControl _listBoxSusCoordinatesOutboard,*/List<string> _listBoxSusCoordinatesInboard, List<string> _listBoxSusCoordinatesOutboard, CoordinateDatabase _coordinateMaster)
        {
            _coordinateMaster.InboardPickUp = new Dictionary<string, Joint>();
            _coordinateMaster.OutboardPickUp = new Dictionary<string, Joint>();

            ///<summary>Populating the Dictionary of Inboard Coordinates </summary>
            foreach (string item in _listBoxSusCoordinatesInboard)
            {
                _coordinateMaster.InboardPickUp.Add(item, new Joint(0, 0, 0, 3, 2));
            }
            ///<summary>Populating the Dictionary of Outboard Coordinates</summary>
            foreach (string item in _listBoxSusCoordinatesOutboard /*.Items*/)
            {
                _coordinateMaster.OutboardPickUp.Add(item, new Joint(0, 0, 0, 3, 2));
            }
        }

        /// <summary>
        /// Method to Invoke the Update Dictionary method
        /// </summary>
        /// <param name="vehicleCorner"></param>
        /// <param name="selectedCoordinate"></param>
        public void DictionaryOfSusCoordinates_UpdateDictionary_Invoker(string vehicleCorner, string selectedCoordinate)
        {
            if (vehicleCorner == "Steering System")
            {
                DictionaryOfSusCoordinates_UpdateDictionary(CoordinatesFL, selectedCoordinate);
            }
            else if (vehicleCorner == "Front Left")
            {
                DictionaryOfSusCoordinates_UpdateDictionary(CoordinatesFL, selectedCoordinate);
            }
            else if (vehicleCorner == "Front Right")
            {
                DictionaryOfSusCoordinates_UpdateDictionary(CoordinatesFR, selectedCoordinate);
            }
            else if (vehicleCorner == "Rear Left")
            {
                DictionaryOfSusCoordinates_UpdateDictionary(CoordinatesRL, selectedCoordinate);
            }
            else if (vehicleCorner == "Rear Right")
            {
                DictionaryOfSusCoordinates_UpdateDictionary(CoordinatesRR, selectedCoordinate);
            }
        }


        /// <summary>
        /// Method to update or edit the Suspension Coordinate based on the selected coordinate from the ListBoxControl
        /// </summary>
        /// <param name="_coordinateMaster"></param>
        /// <param name="_selectededCoordinate"></param>
        private void DictionaryOfSusCoordinates_UpdateDictionary(CoordinateDatabase _coordinateMaster, string _selectededCoordinate)
        {
            if (coordinatePoint != null)
            {
                foreach (string key in _coordinateMaster.InboardPickUp.Keys)
                {
                    if (_selectededCoordinate == key)
                    {
                        _coordinateMaster.InboardPickUp[key].Position = coordinatePoint.Position;
                        viewportLayout1.Entities.Add(_coordinateMaster.InboardPickUp[key], 0, Color.White);
                        break;
                    }
                }
                foreach (string key in _coordinateMaster.OutboardPickUp.Keys)
                {
                    if (_selectededCoordinate == key)
                    {
                        _coordinateMaster.OutboardPickUp[key].Position = coordinatePoint.Position;
                        viewportLayout1.Entities.Add(_coordinateMaster.OutboardPickUp[key], 0, Color.White);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Suspension Coordinate Un-Mapping function
        /// <summary>
        /// Method to Un-map a suspension coordinate and remove it from the viewport 
        /// </summary>
        /// <param name="vehicleCorner">String representing the Vehicle Corner</param>
        /// <param name="selectedCoordinate">String representing the Selected Coordinate</param>
        public void UnMapSuspensionCoordinate(string vehicleCorner, string selectedCoordinate)
        {
            CoordinateDatabase coordinateMaster = FindCoordinateDataBaseObject(vehicleCorner);
            if (coordinateMaster != null)
            {
                foreach (string key in coordinateMaster.InboardPickUp.Keys)
                {
                    if (key == selectedCoordinate)
                    {
                        viewportLayout1.Entities.Remove(viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(coordinateMaster.InboardPickUp[key])]);
                        coordinateMaster.InboardPickUp[key] = new Joint(0, 0, 0, 3, 2);
                        break;
                    }
                }
                foreach (string key in coordinateMaster.OutboardPickUp.Keys)
                {
                    if (key == selectedCoordinate)
                    {
                        viewportLayout1.Entities.Remove(viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(coordinateMaster.OutboardPickUp[key])]);
                        coordinateMaster.OutboardPickUp[key] = new Joint(0, 0, 0, 3, 2);
                        break;
                    }
                }
            }


        }
        /// <summary>
        /// Method to find which Corner the a Coordinate (which is selected using a ListBox of Mapped Coordinates) belongs to. 
        /// </summary>
        /// <param name="_vehicleCorner">String representing the Vehicle Corner </param>
        /// <returns></returns>
        private CoordinateDatabase FindCoordinateDataBaseObject(string _vehicleCorner)
        {
            if (_vehicleCorner == "Steering System")
            {
                return CoordinatesFL;
            }
            else if (_vehicleCorner == "Front Left")
            {
                return CoordinatesFL;
            }
            else if (_vehicleCorner == "Front Right")
            {
                return CoordinatesFR;
            }
            else if (_vehicleCorner == "Rear Left")
            {
                return CoordinatesRL;
            }
            else if (_vehicleCorner == "Rear Right")
            {
                return CoordinatesRR;
            }
            else
            {
                return null;
            }
        }
        #endregion

        public void SelectMappedCoordinate(string _vehicleCorner, string _selectedCoordinate)
        {
            ClearSelection();

            CoordinateDatabase coordinateMaster = FindCoordinateDataBaseObject(_vehicleCorner);
            if (coordinateMaster != null)
            {
                foreach (string key in coordinateMaster.InboardPickUp.Keys)
                {
                    if (key == _selectedCoordinate)
                    {
                        viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(coordinateMaster.InboardPickUp[key])].SetSelection(true);
                        break;
                    }
                }
                foreach (string key in coordinateMaster.OutboardPickUp.Keys)
                {
                    if (key == _selectedCoordinate)
                    {
                        viewportLayout1.Entities[viewportLayout1.Entities.IndexOf(coordinateMaster.OutboardPickUp[key])].Selected = true;
                        break;
                    }
                }
                viewportLayout1.Refresh(); 
            }

        }

        #region Output Imported CAD Clone operations 
        public void CloneImportedCAD(ref bool _FileHasBeenImported, ref bool _CADIsToBeImported, bool OutputIsCalling,/*, string _FileName*/Entity[] _igesEntitiesToClone)
        {
            if (_FileHasBeenImported || (!_FileHasBeenImported && OutputIsCalling))
            {
                Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();

                for (int i = 0; i < Kinematics_Software_New.M1_Global.vehicleGUI[R1.navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.igesEntities.Length; i++)
                {
                    //viewportLayout1.Entities.Add((Entity)Kinematics_Software_New.M1_Global.vehicleGUI[R1.navBarGroupVehicle.SelectedLinkIndex].importCADForm.importCADViewport.igesEntities[i].Clone());
                    viewportLayout1.Entities.Add((Entity)_igesEntitiesToClone[i].Clone());
                }


                viewportLayout1.Entities.Reverse();


            }
            else if (!_FileHasBeenImported)
            {
                ImportCAD(ref _FileHasBeenImported, ref _CADIsToBeImported/*, _ImportedFile*/);
            }

        }

        #region Very useful selection filters but not working for ImprotFiles
        private void barButtonSelectByEdge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.SelectionFilterMode = selectionFilterType.Edge;
        }

        private void barButtonSelectByVertex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.SelectionFilterMode = selectionFilterType.Vertex;
        }

        private void barButtonSelectByFace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewportLayout1.SelectionFilterMode = selectionFilterType.Face;
            viewportLayout1.SelectionFilterMode = selectionFilterType.Entity;
        }
        #endregion

        private void viewportLayout1_Click(object sender, EventArgs e)
        {

        }



        private void ViewportLayout1_WorkCompleted(object sender, WorkCompletedEventArgs e)
        {
            if (importedFile != null || igesEntities != null)
            {
                importedFile = (ReadIGES)e.WorkUnit;
                importedFile.AddToScene(viewportLayout1, 6);
                igesEntities = importedFile.Entities;
                viewportLayout1.UpdateViewportLayout();
                viewportLayout1.ZoomFit();
                viewportLayout1.Entities.Reverse();
            }
        }

        private void ImportCAD(ref bool _fileHasBeenImported, ref bool _cadIsToBeImported)
        {
            importedFile = new ReadIGES(openFileDialog1.FileName, false); /*_importedFile*/;
            viewportLayout1.StartWork(importedFile);
            _fileHasBeenImported = true;
        }
        #endregion

        #region Method to setup the viewport
        public void SetupViewPort()
        {
            #region Setting up the view of the viewport
            viewportLayout1.SetView(viewType.Isometric);

            //viewportLayout1.ZoomFit();

            viewportLayout1.Refresh();
            #endregion
        }
        #endregion

        #region Method to Clear the Viewport
        public void ClearViewPort(bool _cADToBeImported, bool _fileHasBeenImported, Entity[] _importedEntities)

        {
            //List<int> igesEntityIndex = new List<int>();

            if (_cADToBeImported && _fileHasBeenImported)
            {

                if (viewportLayout1.Entities.Count != 0)
                {
                    viewportLayout1.Entities.SetCurrent(null, false);

                    importedBlocks.Clear();

                    for (int i = 0; i < viewportLayout1.Entities.Count; i++)
                    {
                        if (viewportLayout1.Entities[i] as BlockReference != null)
                        {
                            importedBlocks.Add(viewportLayout1.Entities[i] as BlockReference);
                        }

                    }

                    viewportLayout1.Entities.Clear();

                    for (int i = 0; i < importedBlocks.Count; i++)
                    {
                        viewportLayout1.Entities.Add(importedBlocks[i]);
                    }

                    viewportLayout1.Update();
                    viewportLayout1.Refresh();
                }
            }

            else
            {
                viewportLayout1.Entities.Clear();
            }

        }
        #endregion

        #region Method to Set a Block as Current Block to allow for individual component Selection
        /// <summary>
        /// List of <see cref="BlockReference"/> which stores ALL the Blocks inside the Viewport. These maybe Blocks created during the operation <see cref="ReadFileAsync.AddToSceneAsSingleObject(ViewportLayout, string, string)"/> or during the Mapping Operations
        /// </summary>
        List<BlockReference> importedBlocks = new List<BlockReference>();

        /// <summary>
        /// Boolean to determine if the Primary or First block added to the viewport during the operation <see cref="ReadFileAsync.AddToSceneAsSingleObject(ViewportLayout, string, string)"/> is exploded by <see cref="SetCurrent"/> method
        /// </summary>
        public bool PrimaryBlockExploded { get; set; } = false;

        public void SetCurrent()
        {
            

            if (!PrimaryBlockExploded)
            {
                SetCurrent_Primary();
                importCADForm.barStaticItemMapInfo.Caption = "Primary Block Expanded";
            }
            else
            {
                SetCurrent_Secondary();
                importCADForm.barStaticItemMapInfo.Caption = "Secondary Block Expanded";
            }

            ClearSelection();

        }

        /// <summary>
        /// Method to set the Primary <see cref="BlockReference"/> as the Current. Primary is the <see cref="BlockReference"/> created during the operation <see cref="ReadFileAsync.AddToSceneAsSingleObject(ViewportLayout, string, string)"/>
        /// </summary>
        private void SetCurrent_Primary()
        {
            importedBlocks.Clear();
            for (int i = 0; i < viewportLayout1.Entities.Count; i++)
            {
                if (viewportLayout1.Entities[i] as BlockReference != null)
                {
                    importedBlocks.Add(viewportLayout1.Entities[i] as BlockReference);
                    PrimaryBlockExploded = true;
                }

            }

            for (int i = 0; i < importedBlocks.Count; i++)
            {
                if (viewportLayout1.Entities.Contains(importedBlocks[i]))
                {
                    viewportLayout1.Entities.SetCurrent(importedBlocks[i], true);
                    viewportLayout1.Invalidate();

                    viewportLayout1.Update();
                    viewportLayout1.Refresh();
                }
            }
        }

        private void SetCurrent_Secondary()
        {
            for (int i = 0; i < SelectedEntityList.Count; i++)
            {
                if (SelectedEntityList[i].Item.Selected)
                {
                    if (SelectedEntityList[i].Item as BlockReference != null)
                    {
                        BlockReference tempBlock = SelectedEntityList[i].Item as BlockReference;
                        viewportLayout1.Entities.SetCurrent(tempBlock);
                        viewportLayout1.Invalidate();

                        viewportLayout1.Update();
                        viewportLayout1.Refresh();
                    } 
                }
            }
        }
        

        #endregion

        /// <summary>
        /// Method to clear the <see cref="BlockReference"/> which is set as Current
        /// </summary>
        public void ClearCurrent()
        {
            viewportLayout1.Entities.SetCurrent(null, false);
            PrimaryBlockExploded = false;

            viewportLayout1.Update();
            viewportLayout1.Refresh();
        }

        public void RefreshViewPort() => viewportLayout1.Refresh();

        #region Spring Plotter - Still Building
        public void PlotSpring(SuspensionCoordinatesMaster _scmPlotSpring)
        {
            //#region Drawing the Spring

            //springJ1JO = new Vector3D(new Point3D(_scmPlotSpring.J1x, _scmPlotSpring.J1y, _scmPlotSpring.J1z), new Point3D(_scmPlotSpring.JO1x, _scmPlotSpring.JO1y, _scmPlotSpring.JO1z));
            //thetaX = springJ1JO.AngleInXY;
            //thetaY = springJ1JO.AngleFromXY;


            //Xaxis = new Vector3D(new Point3D(0, 0, 0), new Point3D(100, 0, 0));
            //YAxis = new Vector3D(new Point3D(0, 0, 0), new Point3D(0, 100, 0));
            //Zaxis = new Vector3D(new Point3D(0, 0, 0), new Point3D(0, 0, 100));

            ////thetaX = Math.Acos(springJ1JO.X / springJ1JO.Length);
            ////thetaY = Math.Acos(springJ1JO.Y / springJ1JO.Length);
            ////thetaZ = Math.Acos(springJ1JO.Z / springJ1JO.Length);

            //Solid spring = new Solid();
            //spring = Solid.CreateSpring(30, 5, 100, 10, 10, 10, false);
            

            ////Point3D sP1 = new Point3D(_scmPlotSW.J1x, _scmPlotSW.J1y, _scmPlotSW.J1z);
            //Vector3D sTV = new Vector3D(_scmPlotSpring.JO1x, _scmPlotSpring.JO1y, _scmPlotSpring.JO1z);

            //spring.Rotate(thetaY, YAxis);
            //spring.Rotate(thetaX, Xaxis);
            
            ////spring.Rotate(thetaZ, sTV);
            ////spring.Translate(sTV);

            ////Point3D[] sP = new Point3D[2];
            //////sP[0] = sP1;
            //////sP[1] = sP2;
            ////sP = spring.Vertices;


            //viewportLayout1.Entities.Add(spring);
            //#endregion
        }
        #endregion

        #region Click events (To show the coordinates of the selected points) Still building
        private void viewportLayout1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ///<summary>
            ///Getting the Entity under the Mouse Cursor when the double click action was performed
            /// </summary>
            int entityIndex = viewportLayout1.GetEntityUnderMouseCursor(e.Location);
            ///<summary>
            ///Proceeding only if there was an entity under the mouse cursor
            /// </summary>
            if (entityIndex != -1)
            {
                Entity temp_Entity = viewportLayout1.Entities[entityIndex];
            }
        }


        private void viewportLayout1_MouseLeave(object sender, EventArgs e)
        {
            //Kinematics_Software_New.GraphicsCoordinatesHide();
        }

        #endregion

        public void ResizeVP()
        {
            viewportLayout1.Visible = false;
            Size mySize = viewportLayout1.Size;
            viewportLayout1.Size = new Size(mySize.Width - 1, mySize.Height - 1);
            Application.DoEvents();
            viewportLayout1.Size = mySize;
            viewportLayout1.Visible = true;

        }




        public CAD(SerializationInfo info, StreamingContext context)
        {
            //FileHasBeenImported = (bool)info.GetValue("FileHasBeenImported", typeof(bool));
            //CADToBeImported = (bool)info.GetValue("CADToBeImported", typeof(bool));
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("FileHasBeenImported", FileHasBeenImported);
            //info.AddValue("CADToBeImported", CADToBeImported);
            

        }
    }

    enum ForceRepresentationType
    {
        AxialForce,
        DecompositionForce,
        ContactPatchForce
    };

}

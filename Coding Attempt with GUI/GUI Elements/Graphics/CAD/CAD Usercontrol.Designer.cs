namespace Coding_Attempt_with_GUI
{
    partial class CAD
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            devDept.Eyeshot.ButtonSettings buttonSettings1 = new devDept.Eyeshot.ButtonSettings(32, 0, 4, System.Drawing.Color.DeepPink);
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton1 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar1 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.GhostWhite, System.Drawing.Color.Gray, System.Drawing.Color.DimGray, 1D, true, cancelToolBarButton1, true, 0.1D, true);
            devDept.Eyeshot.DisplayModeSettingsRendered displayModeSettingsRendered1 = new devDept.Eyeshot.DisplayModeSettingsRendered(true, devDept.Eyeshot.edgeColorMethodType.EntityColor, System.Drawing.Color.Black, 1F, 2F, devDept.Eyeshot.silhouettesDrawingType.LastFrame, false, devDept.Graphics.shadowType.Realistic, null, false, true, 0.3F, devDept.Graphics.realisticShadowQualityType.High);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(71)))), ((int)(((byte)(82))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(41))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.3D);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 50D), 600D, new devDept.Geometry.Quaternion(0.086824088833465166D, 0.15038373318043533D, 0.492403876506104D, 0.85286853195244339D), devDept.Graphics.projectionType.Perspective, 50D, 2.0000000646266471D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton1 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton1 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton1 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton1 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton1 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton1 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CAD));
            devDept.Eyeshot.ToolBarButton toolBarButton1 = new devDept.Eyeshot.ToolBarButton(((System.Drawing.Image)(resources.GetObject("viewportLayout1.Viewports"))), "LegendEditor", "Edit Legend Style", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, false, ((System.Drawing.Image)(resources.GetObject("viewportLayout1.Viewports1"))), null);
            devDept.Eyeshot.ToolBarButton toolBarButton2 = new devDept.Eyeshot.ToolBarButton(((System.Drawing.Image)(resources.GetObject("viewportLayout1.Viewports2"))), "MakeTransparent", "Make Transparent", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true, ((System.Drawing.Image)(resources.GetObject("viewportLayout1.Viewports3"))), null);
            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton1)),
            toolBarButton1,
            toolBarButton2});
            devDept.Eyeshot.RotateSettings rotateSettings1 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 10D, true, 1D, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings1 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, true, 10, true);
            devDept.Eyeshot.PanSettings panSettings1 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings1 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager1 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(988, 712), backgroundSettings1, camera1, new devDept.Eyeshot.ToolBar[] {
            toolBar1}, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[0], false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, savedViewsManager1, devDept.Eyeshot.viewType.Trimetric);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.White, System.Drawing.Color.LimeGreen, System.Drawing.Color.Turquoise, System.Drawing.Color.Orange, "Origin", "Y", "Z", "X", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, true);
            devDept.Eyeshot.Legend legend1 = new devDept.Eyeshot.Legend(-100D, 100D, "Force Distribution (N)", "Positive - Compression ; Negative - Tension", new System.Drawing.Point(24, 24), new System.Drawing.Size(10, 30), false, false, false, "{0:+0;-0;0}", System.Drawing.Color.Transparent, System.Drawing.Color.White, System.Drawing.Color.GhostWhite, new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(63)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(191))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(127))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(63))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(63)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))))}, true, false);
            devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
            this.viewportLayout1 = new devDept.Eyeshot.ViewportLayout();
            this.popupMenuCADRightClick = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonSelectByEdge = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSelectByFace = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSelectByVertex = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonBoxSelectionVisible = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonBoxSelectionAll = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSingleSelection = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonMultipleSelection = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonHideObject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRestoreOrientation = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClearSelection = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLegendEditor = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.barToggleSwitchItem1 = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barEditItem4 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barEditItem5 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItemEnableIndividualSelection = new DevExpress.XtraBars.BarButtonItem();
            this.barToggleSwitchItem2 = new DevExpress.XtraBars.BarToggleSwitchItem();
            ((System.ComponentModel.ISupportInitialize)(this.viewportLayout1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuCADRightClick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // viewportLayout1
            // 
            this.viewportLayout1.ButtonStyle = buttonSettings1;
            this.viewportLayout1.Cursor = System.Windows.Forms.Cursors.Default;
            this.viewportLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewportLayout1.Font = new System.Drawing.Font("Tahoma", 7.875F);
            this.viewportLayout1.Location = new System.Drawing.Point(0, 0);
            this.viewportLayout1.Margin = new System.Windows.Forms.Padding(2);
            this.viewportLayout1.Name = "viewportLayout1";
            this.viewportLayout1.OrientationMode = devDept.Graphics.orientationType.UpAxisY;
            this.barManager1.SetPopupContextMenu(this.viewportLayout1, this.popupMenuCADRightClick);
            this.viewportLayout1.ProgressBar = progressBar1;
            this.viewportLayout1.Rendered = displayModeSettingsRendered1;
            this.viewportLayout1.SelectionColor = System.Drawing.Color.Aqua;
            this.viewportLayout1.Size = new System.Drawing.Size(988, 712);
            this.viewportLayout1.TabIndex = 0;
            this.viewportLayout1.Text = "viewportLayout1";
            this.viewportLayout1.Units = devDept.Geometry.linearUnitsType.Millimeters;
            coordinateSystemIcon1.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            viewport1.CoordinateSystemIcon = coordinateSystemIcon1;
            legend1.TextFont = null;
            legend1.TitleFont = new System.Drawing.Font("Tahoma", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            viewport1.Legends = new devDept.Eyeshot.Legend[] {
        legend1};
            originSymbol1.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            viewport1.OriginSymbol = originSymbol1;
            viewCubeIcon1.Font = null;
            viewCubeIcon1.InitialRotation = new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D);
            viewport1.ViewCubeIcon = viewCubeIcon1;
            this.viewportLayout1.Viewports.Add(viewport1);
            this.viewportLayout1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.viewportLayout1_MouseDoubleClick);
            this.viewportLayout1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.viewportLayout1_MouseDoubleClick);
            this.viewportLayout1.MouseLeave += new System.EventHandler(this.viewportLayout1_MouseLeave);
            // 
            // popupMenuCADRightClick
            // 
            this.popupMenuCADRightClick.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSelectByEdge),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSelectByFace),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSelectByVertex),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonBoxSelectionVisible),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonBoxSelectionAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSingleSelection),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonMultipleSelection),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonHideObject, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRestoreOrientation),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClearSelection),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLegendEditor, true)});
            this.popupMenuCADRightClick.Manager = this.barManager1;
            this.popupMenuCADRightClick.Name = "popupMenuCADRightClick";
            // 
            // barButtonSelectByEdge
            // 
            this.barButtonSelectByEdge.Caption = "Select By Edge";
            this.barButtonSelectByEdge.Id = 8;
            this.barButtonSelectByEdge.Name = "barButtonSelectByEdge";
            this.barButtonSelectByEdge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonSelectByEdge.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSelectByEdge_ItemClick);
            // 
            // barButtonSelectByFace
            // 
            this.barButtonSelectByFace.Caption = "Select By Face";
            this.barButtonSelectByFace.Id = 9;
            this.barButtonSelectByFace.Name = "barButtonSelectByFace";
            this.barButtonSelectByFace.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonSelectByFace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSelectByFace_ItemClick);
            // 
            // barButtonSelectByVertex
            // 
            this.barButtonSelectByVertex.Caption = "Select By Vertex";
            this.barButtonSelectByVertex.Id = 10;
            this.barButtonSelectByVertex.Name = "barButtonSelectByVertex";
            this.barButtonSelectByVertex.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonSelectByVertex.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSelectByVertex_ItemClick);
            // 
            // barButtonBoxSelectionVisible
            // 
            this.barButtonBoxSelectionVisible.Caption = "Box Selection (Only Visible Items)";
            this.barButtonBoxSelectionVisible.Id = 0;
            this.barButtonBoxSelectionVisible.Name = "barButtonBoxSelectionVisible";
            this.barButtonBoxSelectionVisible.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonBoxSelection_ItemClick);
            // 
            // barButtonBoxSelectionAll
            // 
            this.barButtonBoxSelectionAll.Caption = "Box Selection (All Items)";
            this.barButtonBoxSelectionAll.Id = 7;
            this.barButtonBoxSelectionAll.Name = "barButtonBoxSelectionAll";
            this.barButtonBoxSelectionAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonBoxSelectionAll_ItemClick);
            // 
            // barButtonSingleSelection
            // 
            this.barButtonSingleSelection.Caption = "Single Selection";
            this.barButtonSingleSelection.Id = 3;
            this.barButtonSingleSelection.Name = "barButtonSingleSelection";
            this.barButtonSingleSelection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSingleSelection_ItemClick);
            // 
            // barButtonMultipleSelection
            // 
            this.barButtonMultipleSelection.Caption = "Multiple Selection";
            this.barButtonMultipleSelection.Id = 2;
            this.barButtonMultipleSelection.Name = "barButtonMultipleSelection";
            this.barButtonMultipleSelection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonMultipleSelection_ItemClick);
            // 
            // barButtonHideObject
            // 
            this.barButtonHideObject.Caption = "Hide Object(s)";
            this.barButtonHideObject.Id = 5;
            this.barButtonHideObject.Name = "barButtonHideObject";
            this.barButtonHideObject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonHideObject_ItemClick);
            // 
            // barButtonRestoreOrientation
            // 
            this.barButtonRestoreOrientation.Caption = "Restore Previous Orientation";
            this.barButtonRestoreOrientation.Id = 4;
            this.barButtonRestoreOrientation.Name = "barButtonRestoreOrientation";
            this.barButtonRestoreOrientation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonRestoreOrientation_ItemClick);
            // 
            // barButtonClearSelection
            // 
            this.barButtonClearSelection.Caption = "Clear Selection";
            this.barButtonClearSelection.Id = 6;
            this.barButtonClearSelection.Name = "barButtonClearSelection";
            this.barButtonClearSelection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClearSelection_ItemClick);
            // 
            // barButtonItemLegendEditor
            // 
            this.barButtonItemLegendEditor.Caption = "Legend Editor";
            this.barButtonItemLegendEditor.Enabled = false;
            this.barButtonItemLegendEditor.Id = 26;
            this.barButtonItemLegendEditor.Name = "barButtonItemLegendEditor";
            this.barButtonItemLegendEditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLegendEditor_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonBoxSelectionVisible,
            this.barButtonItem1,
            this.barButtonMultipleSelection,
            this.barButtonSingleSelection,
            this.barButtonRestoreOrientation,
            this.barButtonHideObject,
            this.barButtonClearSelection,
            this.barButtonBoxSelectionAll,
            this.barButtonSelectByEdge,
            this.barButtonSelectByFace,
            this.barButtonSelectByVertex,
            this.barSubItem1,
            this.barToolbarsListItem1,
            this.barToggleSwitchItem1,
            this.barEditItem1,
            this.barButtonItem2,
            this.barSubItem2,
            this.barEditItem2,
            this.barCheckItem1,
            this.barEditItem3,
            this.barEditItem4,
            this.barEditItem5,
            this.barCheckItem2,
            this.barButtonItemEnableIndividualSelection,
            this.barToggleSwitchItem2,
            this.barButtonItemLegendEditor});
            this.barManager1.MaxItemId = 27;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this.repositoryItemPopupContainerEdit1});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(988, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 712);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(988, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 712);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(988, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 712);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "ddd";
            this.barSubItem1.Id = 11;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Caption = "barToolbarsListItem1";
            this.barToolbarsListItem1.Id = 12;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // barToggleSwitchItem1
            // 
            this.barToggleSwitchItem1.Caption = "barToggleSwitchItem1";
            this.barToggleSwitchItem1.Id = 13;
            this.barToggleSwitchItem1.Name = "barToggleSwitchItem1";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 14;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 15;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Enable Individual Selection";
            this.barSubItem2.Id = 16;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem1)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 18;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "barEditItem2";
            this.barEditItem2.Edit = this.repositoryItemCheckedComboBoxEdit1;
            this.barEditItem2.Id = 17;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "barEditItem3";
            this.barEditItem3.Edit = this.repositoryItemTextEdit2;
            this.barEditItem3.Id = 19;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // barEditItem4
            // 
            this.barEditItem4.Caption = "barEditItem4";
            this.barEditItem4.Edit = this.repositoryItemTextEdit3;
            this.barEditItem4.Id = 20;
            this.barEditItem4.Name = "barEditItem4";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // barEditItem5
            // 
            this.barEditItem5.Caption = "barEditItem5";
            this.barEditItem5.Edit = this.repositoryItemPopupContainerEdit1;
            this.barEditItem5.Id = 21;
            this.barEditItem5.Name = "barEditItem5";
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Id = 23;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // barButtonItemEnableIndividualSelection
            // 
            this.barButtonItemEnableIndividualSelection.Caption = "Enable Individual Selection";
            this.barButtonItemEnableIndividualSelection.Id = 24;
            this.barButtonItemEnableIndividualSelection.Name = "barButtonItemEnableIndividualSelection";
            // 
            // barToggleSwitchItem2
            // 
            this.barToggleSwitchItem2.Caption = "barToggleSwitchItem2";
            this.barToggleSwitchItem2.Id = 25;
            this.barToggleSwitchItem2.Name = "barToggleSwitchItem2";
            // 
            // CAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.viewportLayout1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "CAD";
            this.Size = new System.Drawing.Size(988, 712);
            this.Load += new System.EventHandler(this.CAD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.viewportLayout1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuCADRightClick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.PopupMenu popupMenuCADRightClick;
        public devDept.Eyeshot.ViewportLayout viewportLayout1;
        private DevExpress.XtraBars.BarButtonItem barButtonBoxSelectionVisible;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonMultipleSelection;
        private DevExpress.XtraBars.BarButtonItem barButtonSingleSelection;
        private DevExpress.XtraBars.BarButtonItem barButtonRestoreOrientation;
        private DevExpress.XtraBars.BarButtonItem barButtonHideObject;
        private DevExpress.XtraBars.BarButtonItem barButtonClearSelection;
        private DevExpress.XtraBars.BarButtonItem barButtonBoxSelectionAll;
        private DevExpress.XtraBars.BarButtonItem barButtonSelectByEdge;
        private DevExpress.XtraBars.BarButtonItem barButtonSelectByFace;
        private DevExpress.XtraBars.BarButtonItem barButtonSelectByVertex;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
        private DevExpress.XtraBars.BarToggleSwitchItem barToggleSwitchItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarEditItem barEditItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraBars.BarEditItem barEditItem5;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEnableIndividualSelection;
        private DevExpress.XtraBars.BarToggleSwitchItem barToggleSwitchItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLegendEditor;
    }
}

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using devDept.Eyeshot;
using devDept.Graphics;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Eyeshot.Labels;
using System.Drawing;

namespace Coding_Attempt_with_GUI
{
    public partial class OpenTKInput : XtraUserControl
    {


        //#region Fixed Points Front Left Declaration
        //private double A1xFL, A1yFL, A1zFL, B1xFL, B1yFL, B1zFL, C1xFL, C1yFL, C1zFL, D1xFL, D1yFL, D1zFL, JO1xFL, JO1yFL, JO1zFL, I1xFL, I1yFL, I1zFL, Q1xFL, Q1yFL, Q1zFL, N1xFL, N1yFL, N1zFL,
        //              R1xFL, R1yFL, R1zFL; 
        //#endregion

        //#region Moving Points Front Left Declaration
        //private double J1xFL, J1yFL, J1zFL, H1xFL, H1yFL, H1zFL, O1xFL, O1yFL, O1zFL, G1xFL, G1yFL, G1zFL, F1xFL, F1yFL, F1zFL, E1xFL, E1yFL, E1zFL, M1xFL, M1yFL, M1zFL, 
        //    K1xFL, K1yFL, K1zFL, P1xFL, P1yFL, P1zFL, W1xFL, W1yFL, W1zFL;
        //#endregion 

        //#region Fixed Points Front Right Declaration
        //private double A1xFR, A1yFR, A1zFR, B1xFR, B1yFR, B1zFR, C1xFR, C1yFR, C1zFR, D1xFR, D1yFR, D1zFR, JO1xFR, JO1yFR, JO1zFR, I1xFR, I1yFR, I1zFR, Q1xFR, Q1yFR, Q1zFR, N1xFR, N1yFR, N1zFR,
        //              R1xFR, R1yFR, R1zFR /*RideHeightRefxFR, RideHeightRefyFR, RideHeightRefzFR*/;
        //#endregion

        //#region Moving Points Front Right Declaration
        //private double J1xFR, J1yFR, J1zFR, H1xFR, H1yFR, H1zFR, O1xFR, O1yFR, O1zFR, G1xFR, G1yFR, G1zFR, F1xFR, F1yFR, F1zFR, E1xFR, E1yFR, E1zFR, M1xFR, M1yFR, M1zFR, K1xFR, K1yFR, K1zFR, P1xFR, P1yFR, P1zFR, W1xFR, W1yFR, W1zFR;
        //#endregion 

        public OpenTKInput()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void OpenTKInput_Load(object sender, EventArgs e)
        {
            this.Hide(); // We pop the form over everything else.
            this.Focus();

            viewportLayout1.Layers.Add("Joints", Color.White);
            viewportLayout1.Layers.Add("Bars", Color.Orange);
            viewportLayout1.Layers.Add("Triangles");
            viewportLayout1.Layers.Add("Quads");
        }

        private void PlotSuspension(SuspensionCoordinatesMaster _scmPlot)
        {
            #region Joints
            viewportLayout1.Entities.Add(new Joint(_scmPlot.A1x, _scmPlot.A1y, _scmPlot.A1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.B1x, _scmPlot.B1y, _scmPlot.B1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.C1x, _scmPlot.C1y, _scmPlot.C1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.D1x, _scmPlot.D1y, _scmPlot.D1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.E1x, _scmPlot.E1y, _scmPlot.E1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.F1x, _scmPlot.F1y, _scmPlot.F1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.G1x, _scmPlot.G1y, _scmPlot.G1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.H1x, _scmPlot.H1y, _scmPlot.H1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.I1x, _scmPlot.I1y, _scmPlot.I1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.J1x, _scmPlot.J1y, _scmPlot.J1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.JO1x, _scmPlot.JO1y, _scmPlot.JO1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.K1x, _scmPlot.K1y, _scmPlot.K1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.M1x, _scmPlot.M1y, _scmPlot.M1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.N1x, _scmPlot.N1y, _scmPlot.N1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.O1x, _scmPlot.O1y, _scmPlot.O1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.P1x, _scmPlot.P1y, _scmPlot.P1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.Q1x, _scmPlot.Q1y, _scmPlot.Q1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.R1x, _scmPlot.R1y, _scmPlot.R1z, 2.5, 2), 1);
            viewportLayout1.Entities.Add(new Joint(_scmPlot.W1x, _scmPlot.W1y, _scmPlot.W1z, 2.5, 2), 1);
            #endregion

            #region Bars and Trianlges
            viewportLayout1.Entities.Add(new Bar(_scmPlot.F1x, _scmPlot.F1y, _scmPlot.F1z, _scmPlot.A1x, _scmPlot.A1y, _scmPlot.A1z, 1, 8), 2);
            viewportLayout1.Entities.Add(new Bar(_scmPlot.F1x, _scmPlot.F1y, _scmPlot.F1z, _scmPlot.B1x, _scmPlot.B1y, _scmPlot.B1z, 1, 8), 2);

            viewportLayout1.Entities.Add(new Bar(_scmPlot.E1x, _scmPlot.E1y, _scmPlot.E1z, _scmPlot.C1x, _scmPlot.C1y, _scmPlot.C1z, 1, 8), 2);
            viewportLayout1.Entities.Add(new Bar(_scmPlot.E1x, _scmPlot.E1y, _scmPlot.E1z, _scmPlot.D1x, _scmPlot.D1y, _scmPlot.D1z, 1, 8), 2);

            viewportLayout1.Entities.Add(new Bar(_scmPlot.G1x, _scmPlot.G1y, _scmPlot.G1z, _scmPlot.H1x, _scmPlot.H1y, _scmPlot.H1z, 1, 8), 2);

            viewportLayout1.Entities.Add(new Bar(_scmPlot.M1x, _scmPlot.M1y, _scmPlot.M1z, _scmPlot.N1x, _scmPlot.N1y, _scmPlot.N1z, 1, 8), 2);

            Vector3D bellCrankTriangle_I = new Vector3D(_scmPlot.I1x, _scmPlot.I1y, _scmPlot.I1z);
            Vector3D bellCrankTriangle_J = new Vector3D(_scmPlot.J1x, _scmPlot.J1y, _scmPlot.J1z);
            Vector3D bellCrankTriangle_H = new Vector3D(_scmPlot.H1x, _scmPlot.H1y, _scmPlot.H1z);
            Vector3D bellCrankTriangle_O = new Vector3D(_scmPlot.O1x, _scmPlot.O1y, _scmPlot.O1z);
            viewportLayout1.Entities.Add(new Quad(bellCrankTriangle_I, bellCrankTriangle_J, bellCrankTriangle_H, bellCrankTriangle_O), 4, Color.Orange);

            viewportLayout1.Entities.Add(new Bar(_scmPlot.I1x, _scmPlot.I1y, _scmPlot.I1z, _scmPlot.J1x, _scmPlot.J1y, _scmPlot.J1z, 1, 8), 2);
            viewportLayout1.Entities.Add(new Bar(_scmPlot.I1x, _scmPlot.I1y, _scmPlot.I1z, _scmPlot.H1x, _scmPlot.H1y, _scmPlot.H1z, 1, 8), 2);
            viewportLayout1.Entities.Add(new Bar(_scmPlot.I1x, _scmPlot.I1y, _scmPlot.I1z, _scmPlot.O1x, _scmPlot.O1y, _scmPlot.O1z, 1, 8), 2);

            viewportLayout1.Entities.Add(new Bar(_scmPlot.J1x, _scmPlot.J1y, _scmPlot.J1z, _scmPlot.JO1x, _scmPlot.JO1y, _scmPlot.JO1z, 1, 8), 2);

            viewportLayout1.Entities.Add(new Bar(_scmPlot.P1x, _scmPlot.P1y, _scmPlot.P1z, _scmPlot.O1x, _scmPlot.O1y, _scmPlot.O1z, 1, 8), 2);
            viewportLayout1.Entities.Add(new Bar(_scmPlot.P1x, _scmPlot.P1y, _scmPlot.P1z, _scmPlot.Q1x, _scmPlot.Q1y, _scmPlot.Q1z, 1, 8), 2);

            Vector3D tV1 = new Vector3D(_scmPlot.E1x, _scmPlot.E1y, _scmPlot.E1z);
            Vector3D tV2 = new Vector3D(_scmPlot.F1x, _scmPlot.F1y, _scmPlot.F1z);
            Vector3D tV3 = new Vector3D(_scmPlot.M1x, _scmPlot.M1y, _scmPlot.M1z);

            viewportLayout1.Entities.Add(new Triangle(tV1, tV2, tV3), 3, Color.Orange);
            #endregion

            #region Drawing the Spring
            Solid spring = new Solid();
            spring = Solid.CreateSpring(1, 0.5, 5, 10, 1, 5, false);
            //Point3D sP1 = new Point3D(_scmPlot.J1x, _scmPlot.J1y, _scmPlot.J1z);
            Vector3D sTV = new Vector3D(_scmPlot.JO1x, _scmPlot.JO1y, _scmPlot.JO1z);
            spring.Translate(sTV);
            Point3D[] sP = new Point3D[2];
            //sP[0] = sP1;
            //sP[1] = sP2;
            sP = spring.Vertices;


            viewportLayout1.Entities.Add(spring); 
            #endregion

            #region Drawing the Wheel
            Solid wheel = new Solid();
            wheel = Solid.CreateTorus(457.2 / 2, 85, 100, 100);
            Vector3D wTV = new Vector3D(_scmPlot.K1x, _scmPlot.K1y, _scmPlot.K1z);
            Point3D wP1 = new Point3D(_scmPlot.W1x, _scmPlot.W1y, _scmPlot.W1z);
            Point3D wP2 = new Point3D(_scmPlot.W1x, _scmPlot.W1y + 100, _scmPlot.W1z);
            Vector3D wRV = new Vector3D(wP1, wP2);
            wheel.Rotate(Math.PI / 2, wRV);
            wheel.Translate(wTV);

            viewportLayout1.Entities.Add(wheel); 
            #endregion
        }

        public void SuspensionPlotterInvoker(SuspensionCoordinatesFront _scfl, SuspensionCoordinatesFrontRight _scfr,SuspensionCoordinatesRear _scrl,SuspensionCoordinatesRearRight _scrr)
        {
            #region Invoking the Plotter for all the four corners of the Suspension

            PlotSuspension(_scfl);

            PlotSuspension(_scfr);

            PlotSuspension(_scrl);

            PlotSuspension(_scrr);

            viewportLayout1.SetView(viewType.Isometric);

            viewportLayout1.ZoomFit();
            #endregion

        }

        //public void AssignCoordinates()
        //{
            
        //    #region Assigning Front Left Coordinates
        //    A1xFL = _scfl.A1x;
        //    A1yFL = _scfl.A1y;
        //    A1zFL = _scfl.A1z;

        //    B1xFL = _scfl.B1x;
        //    B1yFL = _scfl.B1y;
        //    B1zFL = _scfl.B1z;

        //    C1xFL = _scfl.C1x;
        //    C1yFL = _scfl.C1y;
        //    C1zFL = _scfl.C1z;

        //    D1xFL = _scfl.D1x;
        //    D1yFL = _scfl.D1y;
        //    D1zFL = _scfl.D1z;

        //    E1xFL = _scfl.E1x;
        //    E1yFL = _scfl.E1y;
        //    E1zFL = _scfl.E1z;

        //    F1xFL = _scfl.F1x;
        //    F1yFL = _scfl.F1y;
        //    F1zFL = _scfl.F1z;

        //    G1xFL = _scfl.G1x;
        //    G1yFL = _scfl.G1y;
        //    G1zFL = _scfl.G1z;

        //    H1xFL = _scfl.H1x;
        //    H1yFL = _scfl.H1y;
        //    H1zFL = _scfl.H1z;

        //    I1xFL = _scfl.I1x;
        //    I1yFL = _scfl.I1y;
        //    I1zFL = _scfl.I1z;

        //    J1xFL = _scfl.J1x;
        //    J1yFL = _scfl.J1y;
        //    J1zFL = _scfl.J1z;

        //    JO1xFL = _scfl.JO1x;
        //    JO1yFL = _scfl.JO1y;
        //    JO1zFL = _scfl.JO1z;

        //    K1xFL = _scfl.K1x;
        //    K1yFL = _scfl.K1y;
        //    K1zFL = _scfl.K1z;

        //    M1xFL = _scfl.M1x;
        //    M1yFL = _scfl.M1y;
        //    M1zFL = _scfl.M1z;

        //    N1xFL = _scfl.N1x;
        //    N1yFL = _scfl.N1y;
        //    N1zFL = _scfl.N1z;

        //    O1xFL = _scfl.O1x;
        //    O1yFL = _scfl.O1y;
        //    O1zFL = _scfl.O1z;

        //    P1xFL = _scfl.P1x;
        //    P1yFL = _scfl.P1y;
        //    P1zFL = _scfl.P1z;

        //    Q1xFL = _scfl.Q1x;
        //    Q1yFL = _scfl.Q1y;
        //    Q1zFL = _scfl.Q1z;

        //    R1xFL = _scfl.R1x;
        //    R1yFL = _scfl.R1y;
        //    R1zFL = _scfl.R1z;

        //    W1xFL = _scfl.W1x;
        //    W1yFL = _scfl.W1y;
        //    W1zFL = _scfl.W1z;

        //    #endregion

        //    #region Assigning Front Right Coordinates
        //    A1xFR = _scfr.A1x;
        //    A1yFR = _scfr.A1y;
        //    A1zFR = _scfr.A1z;

        //    B1xFR = _scfr.B1x;
        //    B1yFR = _scfr.B1y;
        //    B1zFR = _scfr.B1z;

        //    C1xFR = _scfr.C1x;
        //    C1yFR = _scfr.C1y;
        //    C1zFR = _scfr.C1z;

        //    D1xFR = _scfr.D1x;
        //    D1yFR = _scfr.D1y;
        //    D1zFR = _scfr.D1z;

        //    E1xFR = _scfr.E1x;
        //    E1yFR = _scfr.E1y;
        //    E1zFR = _scfr.E1z;

        //    F1xFR = _scfr.F1x;
        //    F1yFR = _scfr.F1y;
        //    F1zFR = _scfr.F1z;

        //    G1xFR = _scfr.G1x;
        //    G1yFR = _scfr.G1y;
        //    G1zFR = _scfr.G1z;

        //    H1xFR = _scfr.H1x;
        //    H1yFR = _scfr.H1y;
        //    H1zFR = _scfr.H1z;

        //    I1xFR = _scfr.I1x;
        //    I1yFR = _scfr.I1y;
        //    I1zFR = _scfr.I1z;

        //    J1xFR = _scfr.J1x;
        //    J1yFR = _scfr.J1y;
        //    J1zFR = _scfr.J1z;

        //    JO1xFR = _scfr.JO1x;
        //    JO1yFR = _scfr.JO1y;
        //    JO1zFR = _scfr.JO1z;

        //    K1xFR = _scfr.K1x;
        //    K1yFR = _scfr.K1y;
        //    K1zFR = _scfr.K1z;

        //    M1xFR = _scfr.M1x;
        //    M1yFR = _scfr.M1y;
        //    M1zFR = _scfr.M1z;

        //    N1xFR = _scfr.N1x;
        //    N1yFR = _scfr.N1y;
        //    N1zFR = _scfr.N1z;

        //    O1xFR = _scfr.O1x;
        //    O1yFR = _scfr.O1y;
        //    O1zFR = _scfr.O1z;

        //    P1xFR = _scfr.P1x;
        //    P1yFR = _scfr.P1y;
        //    P1zFR = _scfr.P1z;

        //    Q1xFR = _scfr.Q1x;
        //    Q1yFR = _scfr.Q1y;
        //    Q1zFR = _scfr.Q1z;

        //    R1xFR = _scfr.R1x;
        //    R1yFR = _scfr.R1y;
        //    R1zFR = _scfr.R1z;

        //    W1xFR = _scfr.W1x;
        //    W1yFR = _scfr.W1y;
        //    W1zFR = _scfr.W1z;
        //    #endregion

        //    SuspensionPlotterInvoker();
        //}


    }
}

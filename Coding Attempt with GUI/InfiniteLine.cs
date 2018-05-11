using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Drawing;

namespace Coding_Attempt_with_GUI
{
    class InfiniteLine
    {
        public static void DrawInfiniteLine(Point3D pt1, Point3D pt2, Viewport viewport)
        {

            Segment2D screenLineX = new Segment2D(pt1, pt2);


            int[] viewFrame = viewport.GetViewFrame();

            int left = viewFrame[0];
            int right = viewFrame[0] + viewFrame[2];
            int bottom = viewFrame[1];
            int top = viewFrame[1] + viewFrame[3] - 1;
            Point2D lowerLeft = new Point2D(left, bottom);
            Point2D lowerRight = new Point2D(right, bottom);
            Point2D upperLeft = new Point2D(left, top);
            Point2D upperRight = new Point2D(right, top);


            Segment2D[] screenLines = new Segment2D[]
                                          {
                                              new Segment2D(lowerLeft, lowerRight),
                                              new Segment2D(upperLeft, upperRight),
                                              new Segment2D(lowerLeft, upperLeft),
                                              new Segment2D(lowerRight, upperRight)
                                          };

            Point2D ptAxis1 = null, ptAxis2 = null;

            // Compute the intersection of the infinite screen line against the lower and upper border of the viewport 
            Segment2D.IntersectionLine(screenLineX, screenLines[0], out ptAxis1);
            Segment2D.IntersectionLine(screenLineX, screenLines[1], out ptAxis2);

            bool clipAgainstVertical = true;

            if (ptAxis1 == null || ptAxis2 == null)
            {
                // Compute the intersection of the infinite screen line against the left and right border of the viewport 
                clipAgainstVertical = false;
                Segment2D.IntersectionLine(screenLineX, screenLines[2], out ptAxis1);
                Segment2D.IntersectionLine(screenLineX, screenLines[3], out ptAxis2);
            }

            if (ptAxis1 != null && ptAxis2 != null)
            {
                Segment2D myLine = new Segment2D(ptAxis1, ptAxis2);

                Point2D clippedPt1 = null, clippedPt2 = null;

                if (ptAxis1.X < ptAxis2.X)
                {
                    clippedPt1 = ptAxis1;
                    clippedPt2 = ptAxis2;
                }
                else
                {
                    clippedPt2 = ptAxis1;
                    clippedPt1 = ptAxis2;
                }

                // Compute the intersection of the screen line against the other 2 borders of the viewport 
                Point2D clipped;
                if (Segment2D.Intersection(myLine, screenLines[(clipAgainstVertical) ? 2 : 0], out clipped))
                    clippedPt1 = clipped;

                if (Segment2D.Intersection(myLine, screenLines[(clipAgainstVertical) ? 3 : 1], out clipped))
                    clippedPt2 = clipped;

                //renderContext.SetLineSize(1);
                //renderContext.EnableThickLines();
                //renderContext.SetColorWireframe(color);
                //renderContext.DrawLines(new float[]
                //{
                //    (float) clippedPt1.X, (float) clippedPt1.Y, 0,
                //    (float) clippedPt2.X, (float) clippedPt2.Y, 0
                //});
            }
        }

    }

    internal class MyViewportLayout : ViewportLayout
    {
        protected override void DrawViewportBackground(DrawSceneParams data)
        {
            base.DrawViewportBackground(data);

            ComputeNearFarIntersections(data.Viewport);

            var rc = data.RenderContext;
            rc.PushDepthStencilState();

            //rc.SetState(depthStencilStateType.DepthTestOff);
            //rc.SetShader(shaderType.NoLights);

            // Draw in 2D the parts of the lines beyond the far camera plane

            // X axis 
            DrawLinesBeyondFar(data.Viewport, ptXNear, ptXFar, Color.Red);

            // Y axis 
            DrawLinesBeyondFar(data.Viewport, ptYNear, ptYFar, Color.Green);

            // Z axis 
            DrawLinesBeyondFar(data.Viewport, ptZNear, ptZFar, Color.Blue);

            rc.PopDepthStencilState();
        }

        private void ComputeNearFarIntersections(Viewport viewport)
        {
            Plane farPlane = viewport.Camera.FarPlane;
            Plane nearPlane = viewport.Camera.NearPlane;

            Segment3D sX = new Segment3D(0, 0, 0, 1, 0, 0);
            Segment3D sY = new Segment3D(0, 0, 0, 0, 1, 0);
            Segment3D sZ = new Segment3D(0, 0, 0, 0, 0, 1);

            // Compute the intersections with the camera planes
            sX.IntersectWith(nearPlane, true, out ptXNear);
            sX.IntersectWith(farPlane, true, out ptXFar);

            sY.IntersectWith(nearPlane, true, out ptYNear);
            sY.IntersectWith(farPlane, true, out ptYFar);

            sZ.IntersectWith(nearPlane, true, out ptZNear);
            sZ.IntersectWith(farPlane, true, out ptZFar);
        }

        // Intersections with the camera planes
        Point3D ptXNear, ptXFar, ptYNear, ptYFar, ptZNear, ptZFar;

        protected override void DrawViewport(DrawSceneParams myParams)
        {
            base.DrawViewport(myParams);

            var rc = myParams.RenderContext;

            //rc.SetShader(shaderType.NoLights);

            // Draw the 3D lines between the camera planes

            if (ptXNear != null && ptXFar != null)
            {
                rc.SetColorWireframe(Color.Red);
                rc.DrawLines(new Point3D[] { ptXNear, ptXFar });
            }

            if (ptYNear != null && ptYFar != null)
            {
                rc.SetColorWireframe(Color.Green);
                rc.DrawLines(new Point3D[] { ptYNear, ptYFar });
            }

            if (ptZNear != null && ptZFar != null)
            {
                rc.SetColorWireframe(Color.Blue);
                rc.DrawLines(new Point3D[] { ptZNear, ptZFar });
            }

        }

        protected override void DrawOverlay(DrawSceneParams data)
        {
            // Draw in 2D the parts of the lines before the near camera plane
            //renderContext.SetShader(shaderType.NoLights);

            for (int i = 0; i < Viewports.Count; i++)
            {
                ComputeNearFarIntersections(Viewports[i]);

                // X axis 
                DrawLinesBeforeNear(Viewports[i], ptXNear, ptXFar, Color.Red);

                // Y axis 
                DrawLinesBeforeNear(Viewports[i], ptYNear, ptYFar, Color.Green);

                // Z axis 
                DrawLinesBeforeNear(Viewports[i], ptZNear, ptZFar, Color.Blue);
            }
        }

        private void DrawLinesBeyondFar(Viewport viewport, Point3D nearPt, Point3D farPt, Color color)
        {
            if (farPt == null || nearPt == null)
                return;

            Vector3D dir = Vector3D.Subtract(farPt, nearPt);
            dir.Normalize();

            if (viewport == null)
                viewport = Viewports[ActiveViewport];

            Point3D pt1 = viewport.WorldToScreen(farPt);
            Point3D pt2 = viewport.WorldToScreen(farPt + dir);
            DrawLine(viewport, pt1, pt2, color, true);
        }

        private void DrawLinesBeforeNear(Viewport viewport, Point3D nearPt, Point3D farPt, Color color)
        {
            if (farPt == null || nearPt == null)
                return;

            Vector3D dir = Vector3D.Subtract(farPt, nearPt);
            dir.Normalize();

            if (viewport == null)
                viewport = Viewports[ActiveViewport];

            var pt1 = viewport.WorldToScreen(nearPt);
            var pt2 = viewport.WorldToScreen(nearPt - dir);
            DrawLine(viewport, pt1, pt2, color, false);
        }

        private void DrawLine(Viewport viewport, Point3D pt1, Point3D pt2, Color color, bool convertToViewport)
        {
            if (pt1 == null || pt2 == null)
                return;

            Segment2D screenLine = new Segment2D(pt1, pt2);

            int[] viewFrame = viewport.GetViewFrame();

            double left = viewFrame[0];
            double right = viewFrame[0] + viewFrame[2];
            double bottom = viewFrame[1];
            double top = viewFrame[1] + viewFrame[3] - 1;
            Point2D lowerLeft = new Point2D(left, bottom);
            Point2D lowerRight = new Point2D(right, bottom);
            Point2D upperLeft = new Point2D(left, top);
            Point2D upperRight = new Point2D(right, top);


            Segment2D[] screenLines = new Segment2D[]
            {
            new Segment2D(lowerLeft, lowerRight),
            new Segment2D(upperLeft, upperRight),
            new Segment2D(lowerLeft, upperLeft),
            new Segment2D(lowerRight, upperRight)
            };

            Point2D ptAxis1 = null, ptAxis2 = null;

            Vector2D dir = Vector2D.Subtract(pt2, pt1);
            dir.Normalize();

            // extend the segment outside the window limits
            screenLine.P1 = screenLine.P0 + dir * (viewport.Size.Width + viewport.Size.Height);

            // Compute the intersection of the screen line against the lower and upper border of the viewport 
            Segment2D.Intersection(screenLine, screenLines[0], out ptAxis1);
            Segment2D.Intersection(screenLine, screenLines[1], out ptAxis2);

            if (ptAxis1 != null)

                screenLine.P1 = ptAxis1;

            if (ptAxis2 != null)

                screenLine.P1 = ptAxis2;

            bool clipAgainstVertical = true;

            // Compute the intersection of the infinite screen line against the left and right border of the viewport 
            Segment2D.Intersection(screenLine, screenLines[2], out ptAxis1);
            Segment2D.Intersection(screenLine, screenLines[3], out ptAxis2);

            if (ptAxis1 != null)

                screenLine.P1 = ptAxis1;

            if (ptAxis2 != null)

                screenLine.P1 = ptAxis2;

            renderContext.SetLineSize(1);
            renderContext.EnableThickLines();
            renderContext.SetColorWireframe(color);

            var tol = 1e-6;

            // Consider some tolerance
            if (screenLine.P0.X >= left - tol && screenLine.P0.X <= right + tol &&
                screenLine.P0.Y >= bottom - tol && screenLine.P0.Y <= top + tol &&
                screenLine.P1.X >= left - tol && screenLine.P1.X <= right + tol &&
                screenLine.P1.Y >= bottom - tol && screenLine.P1.Y <= top + tol)
            {
                if (convertToViewport)
                {
                    // When drawing the lines beyond far inside the DrawViewportBackground, the camera is set to just the Viewport, not to the whole ViewportLayout,
                    // so if we have multiple viewports we must adjust the screen coordinates
                    screenLine.P0 = new Point2D(screenLine.P0.X - left, screenLine.P0.Y - bottom);
                    screenLine.P1 = new Point2D(screenLine.P1.X - left, screenLine.P1.Y - bottom);
                }

                renderContext.DrawLines(new float[]
                {
                (float) screenLine.P0.X, (float) screenLine.P0.Y, 0,
                (float) screenLine.P1.X, (float) screenLine.P1.Y, 0
                });
            }
        }
    }

}

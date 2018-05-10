using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

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
}

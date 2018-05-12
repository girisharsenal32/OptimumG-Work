using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Drawing;
using devDept.Graphics;

namespace Coding_Attempt_with_GUI
{
    class CustomInfiniteLine : ViewportLayout
    {

        public static void DrawInfiniteLine(/*Viewport Viewport, Point3D NearPt, Point3D FarPt, Color Color,*/ Line LineToBeInfinite, Double Extension)
        {
            LineExtender(LineToBeInfinite, Extension);
        }

        private static void LineExtender(Line _lineToBeExtended, double _extension)
        {
            Segment3D segmentToBeInfinite = new Segment3D(_lineToBeExtended.StartPoint, _lineToBeExtended.EndPoint);

            Point3D extendedPointStart = new Point3D();
            Point3D extendedPointEnd = new Point3D();
            
            extendedPointEnd.X = segmentToBeInfinite.P1.X - (((segmentToBeInfinite.P1.X - segmentToBeInfinite.P0.X) / segmentToBeInfinite.Length) * _extension);
            extendedPointEnd.Y = segmentToBeInfinite.P1.Y - (((segmentToBeInfinite.P1.Y - segmentToBeInfinite.P0.Y) / segmentToBeInfinite.Length) * _extension);
            extendedPointEnd.Z = segmentToBeInfinite.P1.Z - (((segmentToBeInfinite.P1.Z - segmentToBeInfinite.P0.Z) / segmentToBeInfinite.Length) * _extension);

            extendedPointStart.X = segmentToBeInfinite.P0.X + (((segmentToBeInfinite.P1.X - segmentToBeInfinite.P0.X) / segmentToBeInfinite.Length) * _extension);
            extendedPointStart.Y = segmentToBeInfinite.P0.Y + (((segmentToBeInfinite.P1.Y - segmentToBeInfinite.P0.Y) / segmentToBeInfinite.Length) * _extension);
            extendedPointStart.Z = segmentToBeInfinite.P0.Z + (((segmentToBeInfinite.P1.Z - segmentToBeInfinite.P0.Z) / segmentToBeInfinite.Length) * _extension);


            _lineToBeExtended.EndPoint = extendedPointEnd;
            _lineToBeExtended.StartPoint = extendedPointStart;
        }
        
    }
}

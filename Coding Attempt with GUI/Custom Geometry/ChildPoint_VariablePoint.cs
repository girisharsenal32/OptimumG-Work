using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// Class of Points which have a initial and final state. The Final state represents the Point after a set of Transformations have been applied to it 
    /// </summary>
    public class ChildPoint_VariablePoint
    {
        /// <summary>
        /// List of Points. The <see cref="List{T}[0]"/> represent the Initial State of the Point with no transformations or changes. All the remaining indices correspond to a changed state of the Point as the tranformations are applied
        /// </summary>
        public List<Point3D> DeltaPoint = new List<Point3D>();

        /// <summary>
        /// Overloaded constructor to initialize the Start Point
        /// </summary>
        /// <param name="_start">Start Point</param>
        public ChildPoint_VariablePoint(Point3D _start)
        {
            DeltaPoint.Add(new Point3D(_start.X, _start.Y, _start.Z));
            //DeltaPoint.Add(new Point3D(_start.X, _start.Y, _start.Z));
        }

        /// <summary>
        /// Method to Rotate a 3D Point of type <see cref="devDept.Geometry.Point3D"/>. This is needed because the <see cref="devDept"/> namespace doesn't have a method to Rotate Point
        /// This method draws a line between the Point to be rotated and Point on the Axis of Rotataion about which the point is to be rotated and then rotates that line
        /// </summary>
        /// <param name="_pointToBeRotated">Point to be rotated</param>
        /// <param name="_pointToRotateAbout">Point on the Axis Line about which the concerned Point is to be rotated</param>
        /// <param name="_rotationLine">Axis of Rotation</param>
        /// <param name="_angleOfRotation">Angle of Rotation</param>
        /// <returns></returns>
        public Point3D RotatePoint(Point3D _pointToBeRotated, Point3D _pointToRotateAbout, Line _rotationLine, MathNet.Spatial.Units.Angle _angleOfRotation)
        {
            Line tempLine = new Line(_pointToRotateAbout.X, _pointToRotateAbout.Y, _pointToRotateAbout.Z, _pointToBeRotated.X, _pointToBeRotated.Y, _pointToBeRotated.Z);

            ///<summary>Rotating the Line should also rotate the Point of Interst</summary>
            tempLine.Rotate(_angleOfRotation.Radians, _rotationLine.StartPoint, _rotationLine.EndPoint);

            _pointToBeRotated = new Point3D(tempLine.EndPoint.X, tempLine.EndPoint.Y, tempLine.EndPoint.Z);

            return _pointToBeRotated;
        }
    }
}

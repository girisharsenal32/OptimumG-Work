using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MathNet.Spatial.Euclidean;
//using MathNet.Spatial.Units;
using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace Coding_Attempt_with_GUI
{
    public class Custom3DGeometry
    {
        /// <summary>
        /// The actual Line of the object of the <see cref="Custom3DGeometry"/> class with an Initial and Final Condition
        /// </summary>
        public ChildLine_VariableLine Line { get; set; } = new ChildLine_VariableLine();

        /// <summary>
        /// The Perpendicular <see cref="ChildLine_LinePair"/> to the Actual Line along the X direction.
        /// </summary>
        public ChildLine_LinePair PerpAlongX { get; set; }
       
        /// <summary>
        /// The Perpendicular <see cref="ChildLine_LinePair"/> to the Actual Line along the Y direction.
        /// </summary>
        public ChildLine_LinePair PerpAlongY { get; set; }
        
        /// <summary>
        /// The Perpendicular <see cref="ChildLine_LinePair"/> to the Actual Line along the Z direction.
        /// </summary>
        public ChildLine_LinePair PerpAlongZ { get; set; }
        
        /// <summary>
        /// Represent the <see cref="Custom3DGeometry.Line"/> projection on the Front, Side and Top View Planes
        /// </summary>
        public ChildLine_ViewLines ViewLines { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startPoint">Start and End Point of the Actual Line</param>
        /// <param name="endPoint"></param>
        public Custom3DGeometry(Point3D startPoint, Point3D endPoint)
        {
            InitializeComponent(startPoint, endPoint, 0);
        }

        /// <summary>
        /// Method to initialize the components of a <see cref="Custom3DGeometry"/> by adding a Line to List <see cref="ChildLine_VariableLine.DeltaLine"/> of the <see cref="Line"/> and the <see cref="ViewLines"/> and the 3 prependicular Lines
        /// </summary>
        /// <param name="_startPoint"></param>
        /// <param name="_endPoint"></param>
        public void InitializeComponent(Point3D _startPoint, Point3D _endPoint, int _i)
        {
            Line = new ChildLine_VariableLine(_startPoint, _endPoint);

            PerpAlongX = new ChildLine_LinePair(Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint, new Point3D(Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.X + 100, Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.Y, Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.Z));

            PerpAlongY = new ChildLine_LinePair(Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint, new Point3D(Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.X, Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.Y + 100, Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.Z));

            PerpAlongZ = new ChildLine_LinePair(Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint, new Point3D(Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.X, Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.Y, Line.DeltaLine[Line.DeltaLine.Count-1].MidPoint.Z + 100));

            ViewLines = new ChildLine_ViewLines(new Point2D(_startPoint.X, _startPoint.Y), new Point2D(_endPoint.X, _endPoint.Y),
                                                new Point2D(_startPoint.Z, _startPoint.Y), new Point2D(_endPoint.Z, _endPoint.Y),
                                                new Point2D(_startPoint.X, _startPoint.Z), new Point2D(_endPoint.X, _endPoint.Z));
        }

        public void AddLineAndPointToDeltaLineAndDeltaPoint(Custom3DGeometry _finalGeometry, int i)
        {
            _finalGeometry.Line.DeltaLine.Add(new Line(_finalGeometry.Line.DeltaLine[i].StartPoint.X, _finalGeometry.Line.DeltaLine[i].StartPoint.Y, _finalGeometry.Line.DeltaLine[i].StartPoint.Z,
                                                       _finalGeometry.Line.DeltaLine[i].EndPoint.X, _finalGeometry.Line.DeltaLine[i].EndPoint.Y, _finalGeometry.Line.DeltaLine[i].EndPoint.Z));

            _finalGeometry.PerpAlongX = new ChildLine_LinePair(_finalGeometry.Line.DeltaLine[i + 1].MidPoint, new Point3D(_finalGeometry.Line.DeltaLine[i + 1].MidPoint.X + 100, _finalGeometry.Line.DeltaLine[i + 1].MidPoint.Y, _finalGeometry.Line.DeltaLine[i + 1].MidPoint.Z));

            _finalGeometry.PerpAlongY = new ChildLine_LinePair(_finalGeometry.Line.DeltaLine[i + 1].MidPoint, new Point3D(_finalGeometry.Line.DeltaLine[i + 1].MidPoint.X, _finalGeometry.Line.DeltaLine[i + 1].MidPoint.Y + 100, _finalGeometry.Line.DeltaLine[i + 1].MidPoint.Z));

            _finalGeometry.PerpAlongZ = new ChildLine_LinePair(_finalGeometry.Line.DeltaLine[i + 1].MidPoint, new Point3D(_finalGeometry.Line.DeltaLine[i + 1].MidPoint.X, _finalGeometry.Line.DeltaLine[i + 1].MidPoint.Y, _finalGeometry.Line.DeltaLine[i + 1].MidPoint.Z + 100));

            _finalGeometry.ViewLines.AddLineToDeltaLine(new Point2D(_finalGeometry.Line.DeltaLine[i + 1].StartPoint.X, _finalGeometry.Line.DeltaLine[i + 1].StartPoint.Y), new Point2D(_finalGeometry.Line.DeltaLine[i + 1].EndPoint.X, _finalGeometry.Line.DeltaLine[i + 1].EndPoint.Y),
                                                        new Point2D(_finalGeometry.Line.DeltaLine[i + 1].StartPoint.Z, _finalGeometry.Line.DeltaLine[i + 1].StartPoint.Y), new Point2D(_finalGeometry.Line.DeltaLine[i + 1].EndPoint.Z, _finalGeometry.Line.DeltaLine[i + 1].EndPoint.Y),
                                                        new Point2D(_finalGeometry.Line.DeltaLine[i + 1].StartPoint.X, _finalGeometry.Line.DeltaLine[i + 1].StartPoint.Z), new Point2D(_finalGeometry.Line.DeltaLine[i + 1].EndPoint.X, _finalGeometry.Line.DeltaLine[i + 1].EndPoint.Z));
        }

        public void UpdateComponent(Custom3DGeometry _finalGeometry, int i)
        {
            _finalGeometry.Line.AssignFinalState_3DLine(_finalGeometry.Line.DeltaLine[i].StartPoint, _finalGeometry.Line.DeltaLine[i].EndPoint, i);

            _finalGeometry.PerpAlongX = new ChildLine_LinePair(_finalGeometry.Line.DeltaLine[i].MidPoint, new Point3D(_finalGeometry.Line.DeltaLine[i].MidPoint.X + 100, _finalGeometry.Line.DeltaLine[i].MidPoint.Y, _finalGeometry.Line.DeltaLine[i].MidPoint.Z));

            _finalGeometry.PerpAlongY = new ChildLine_LinePair(_finalGeometry.Line.DeltaLine[i].MidPoint, new Point3D(_finalGeometry.Line.DeltaLine[i].MidPoint.X, _finalGeometry.Line.DeltaLine[i].MidPoint.Y + 100, _finalGeometry.Line.DeltaLine[i].MidPoint.Z));

            _finalGeometry.PerpAlongZ = new ChildLine_LinePair(_finalGeometry.Line.DeltaLine[i].MidPoint, new Point3D(_finalGeometry.Line.DeltaLine[i].MidPoint.X, _finalGeometry.Line.DeltaLine[i].MidPoint.Y, _finalGeometry.Line.DeltaLine[i].MidPoint.Z + 100));

            _finalGeometry.ViewLines.AssignFinalState(new Point2D(_finalGeometry.Line.DeltaLine[i].StartPoint.X, _finalGeometry.Line.DeltaLine[i].StartPoint.Y), new Point2D(_finalGeometry.Line.DeltaLine[i].EndPoint.X, _finalGeometry.Line.DeltaLine[i].EndPoint.Y),
                                                      new Point2D(_finalGeometry.Line.DeltaLine[i].StartPoint.Z, _finalGeometry.Line.DeltaLine[i].StartPoint.Y), new Point2D(_finalGeometry.Line.DeltaLine[i].EndPoint.Z, _finalGeometry.Line.DeltaLine[i].EndPoint.Y),
                                                      new Point2D(_finalGeometry.Line.DeltaLine[i].StartPoint.X, _finalGeometry.Line.DeltaLine[i].StartPoint.Z), new Point2D(_finalGeometry.Line.DeltaLine[i].EndPoint.X, _finalGeometry.Line.DeltaLine[i].EndPoint.Z), i);

        }

        /// <summary>
        /// Method to Convert a 2D Line to a <see cref="MathNet.Spatial.Euclidean.Vector2D"/>
        /// </summary>
        /// <param name="_2DLinetoBeVectored">2D Line which is to be converted to 2D Vector of <see cref="MathNet.Spatial.Euclidean.Vector2D"/></param>
        /// <returns></returns>
        public static MathNet.Spatial.Euclidean.Vector2D GetMathNetVector2D(Line _2DLinetoBeVectored)
        {
            MathNet.Spatial.Euclidean.Vector2D vector2D = new MathNet.Spatial.Euclidean.Vector2D(_2DLinetoBeVectored.StartPoint.X - _2DLinetoBeVectored.EndPoint.X, _2DLinetoBeVectored.StartPoint.Y - _2DLinetoBeVectored.EndPoint.Y);
            
            return vector2D;
        }

        /// <summary>
        /// Method to convert a 3D Line to 3D <see cref="MathNet.Spatial.Euclidean.Vector3D"/>
        /// </summary>
        /// <param name="_3DLinetobeVectored"></param>
        /// <returns></returns>
        public static MathNet.Spatial.Euclidean.Vector3D GetMathNetVector3D(Line _3DLinetobeVectored)
        {
            MathNet.Spatial.Euclidean.Vector3D vector3D = new MathNet.Spatial.Euclidean.Vector3D(_3DLinetobeVectored.EndPoint.X - _3DLinetobeVectored.StartPoint.X, 
                                                                                                 _3DLinetobeVectored.EndPoint.Y - _3DLinetobeVectored.StartPoint.Y, 
                                                                                                 _3DLinetobeVectored.EndPoint.Z - _3DLinetobeVectored.StartPoint.Z);

            return vector3D;
        }
        /// <summary>
        /// Method to Convert a 3D Line to <see cref="Vector3D"/>
        /// </summary>
        /// <param name="_3DLineToVector"></param>
        /// <returns></returns>
        public static Vector3D GetdevDeptVector3D(Line _3DLineToVector)
        {
            Vector3D vector3D = new Vector3D(_3DLineToVector.EndPoint.X - _3DLineToVector.StartPoint.X, _3DLineToVector.EndPoint.Y - _3DLineToVector.StartPoint.Y, _3DLineToVector.EndPoint.Z - _3DLineToVector.StartPoint.Z);

            return vector3D;
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class Motion : ISerializable
    {
        #region Declarations

        public double[] ChartPoints_WheelDef_Y, ChartPoints_Steering_Y;
        public double[] ChartPoints_WheelDef_X, ChartPoints_Steering_X;

        /// <remarks>
        /// Initialization of the Lists below seems unneccessary but this is done to prevent an exception which occurred in some case when steering points 
        /// are added but wheel deflection points are not added 
        /// </summary>
        public List<double> Final_WheelDeflectionsY = new List<double>(), Final_WheelSteerY = new List<double>();
        public List<double> Final_WheelDeflectionsX = new List<double>(), Final_WheelSteerX = new List<double>();

        double slope, intercept, VerifyEQ, Delta = 1, NoOfSteps, NextX;

        int NoOfDeflections, NoOfSteers;

        public string MotionName { get; set; }
        public int MotionID = 0;
        public static int MotionCounter = 0;
        public static List<Motion> List_Motion = new List<Motion>();
        public MotionGUI Motion_MotionGUI;
        public DataTable Motion_DataTable;

        public bool WheelDeflectionExists { get; set; }
        public bool SteeringExists { get; set; }
        #endregion

        #region Constructors
        public Motion()
        {
            // This constructor exists so that in a dummly Motion Object can be created in case a Simulation from Stands to Ground is being done
        }

        public Motion(string _motionName, int _motionID)
        {
            MotionName = _motionName + _motionID;
            MotionID = _motionID;
        }
        #endregion

        #region Initializing the Motion Data Table
        private void InitializeDataTable()
        {
            
            Motion_DataTable = new DataTable();

            Motion_DataTable.TableName = "Motion View";

            Motion_DataTable.Columns.Add("Column 1", typeof(int));
            Motion_DataTable.Columns[0].ReadOnly = true;

            Motion_DataTable.Columns.Add("Column 2", typeof(double));
            Motion_DataTable.Columns[1].ReadOnly = true;

            Motion_DataTable.Columns.Add("Column 3", typeof(double));
            Motion_DataTable.Columns[2].ReadOnly = true;
        }
        #endregion

        #region Method to fix the length of the array to 100
        private List<double> FixListLength(List<double> _list, int _reqCount, bool XAxis)
        {
            if (_list.Count < _reqCount)
            {
                int delta = _reqCount - _list.Count;
                for (int i_insert = 0; i_insert < delta; i_insert++)
                {
                    if (XAxis)
                    {
                        _list.Insert(_list.Count, Convert.ToDouble(_list.Count));
                    }
                    else _list.Insert(_list.Count, new double());
                }
            }

            else if (_list.Count > _reqCount)
            {
                int delta = _list.Count - _reqCount;
                for (int i_remove = delta; i_remove > 0; i_remove--)
                {
                    _list.RemoveAt(_list.Count - 1);
                }
            }

            return _list;
        } 
        #endregion

        #region Calculating the equation of the line of motion
        private void EquationOfLine(double[] _wheelMotionY, double[] _wheelMotionX, int Index)
        {
            slope = (_wheelMotionY[Index + 1] - _wheelMotionY[Index]) / (_wheelMotionX[Index + 1] - _wheelMotionX[Index]);
            intercept = (_wheelMotionY[Index] - (slope * _wheelMotionX[Index]));
            VerifyEQ = (slope * _wheelMotionX[Index]) + intercept; // This line exists only to verify if the slope is calculated properly. This line will be used only during debugging
        }
        #endregion

        #region Calculating the entire motion of the Vehicle
        private void GetMotion()
        {
            Final_WheelDeflectionsY = new List<double>();
            Final_WheelDeflectionsX = new List<double>();
            Final_WheelSteerY = new List<double>();
            Final_WheelSteerX = new List<double>();

            InitializeDataTable();

            for (int i_slope_Heave = 0; i_slope_Heave < NoOfDeflections - 1; i_slope_Heave++) 
            {
                EquationOfLine(ChartPoints_WheelDef_Y, ChartPoints_WheelDef_X, i_slope_Heave);
                NoOfSteps = Math.Abs(Convert.ToInt32((ChartPoints_WheelDef_X[i_slope_Heave + 1] - ChartPoints_WheelDef_X[i_slope_Heave]) / Delta));
                NextX = ChartPoints_WheelDef_X[i_slope_Heave];

                for (int i = 0; i < NoOfSteps; i++)
                {
                    
                    int PositionOfInsert = Final_WheelDeflectionsY.Count;

                    Final_WheelDeflectionsY.Insert(PositionOfInsert, new Double());
                    Final_WheelDeflectionsY[PositionOfInsert] = (slope * NextX) + intercept;

                    Final_WheelDeflectionsX.Insert(PositionOfInsert, new Double());
                    Final_WheelDeflectionsX[PositionOfInsert] = NextX;

                    NextX += Delta;
                }
            }

            Final_WheelDeflectionsX = FixListLength(Final_WheelDeflectionsX, 100, true);
            Final_WheelDeflectionsY = FixListLength(Final_WheelDeflectionsY, 100, false);

            for (int i_slope_Steer = 0; i_slope_Steer < NoOfSteers - 1; i_slope_Steer++) 
            {
                EquationOfLine(ChartPoints_Steering_Y, ChartPoints_Steering_X, i_slope_Steer);
                NoOfSteps = Math.Abs(Convert.ToInt32((ChartPoints_Steering_X[i_slope_Steer + 1] - ChartPoints_Steering_X[i_slope_Steer]) / Delta));
                NextX = ChartPoints_Steering_X[i_slope_Steer];

                for (int i = 0; i < NoOfSteps; i++)
                {
                    int PositionOfInsert = Final_WheelSteerY.Count;

                    Final_WheelSteerY.Insert(PositionOfInsert, new Double());
                    Final_WheelSteerY[PositionOfInsert] = (slope * NextX) + intercept;

                    Final_WheelSteerX.Insert(PositionOfInsert, new Double());
                    Final_WheelSteerX[PositionOfInsert] = NextX;

                    NextX += Delta;
                }

            }

            Final_WheelSteerX = FixListLength(Final_WheelSteerX, 100, true);
            Final_WheelSteerY = FixListLength(Final_WheelSteerY, 100, false);

            PopulateDataTable();
        }
        #endregion

        #region Method to Populate the MotionView Data Table
        public void PopulateDataTable()
        {
            for (int i_Table = 0; i_Table < Final_WheelDeflectionsX.Count; i_Table++)
            {
                Motion_DataTable.Rows.Add(Convert.ToInt32(Final_WheelDeflectionsX[i_Table]), Final_WheelDeflectionsY[i_Table], Final_WheelSteerY[i_Table]);
                
            }
        } 
        #endregion

        #region Parent function to get the Wheel Deflections & Steering Angles of the Vehicle 
        public void GetWheelDeflectionAndSteer(MotionGUI motion_MotionGUI, bool IsControlInitialized, bool _deflectionExists, bool _steeringExists)
        {
            Motion_MotionGUI = motion_MotionGUI;
            NoOfDeflections = Motion_MotionGUI.motionGUI_MotionChart.chartControl1.Series[0].Points.Count;
            ChartPoints_WheelDef_Y = new double[NoOfDeflections];
            ChartPoints_WheelDef_X = new double[NoOfDeflections];

            for (int i_Motion_Heave = 0; i_Motion_Heave < NoOfDeflections; i_Motion_Heave++)
            {

                ChartPoints_WheelDef_Y[i_Motion_Heave] = Convert.ToDouble(Motion_MotionGUI.motionGUI_MotionChart.chartControl1.Series[0].Points[i_Motion_Heave].GetNumericValue(DevExpress.XtraCharts.ValueLevel.Value));
                ChartPoints_WheelDef_X[i_Motion_Heave] = Convert.ToDouble(Motion_MotionGUI.motionGUI_MotionChart.chartControl1.Series[0].Points[i_Motion_Heave].Argument);
            }

            Array.Sort(ChartPoints_WheelDef_X, ChartPoints_WheelDef_Y);

            NoOfSteers = Motion_MotionGUI.motionGUI_MotionChart.chartControl2.Series[0].Points.Count;
            ChartPoints_Steering_Y = new double[NoOfSteers];
            ChartPoints_Steering_X = new double[NoOfSteers];

            for (int i_Motion_Steer = 0; i_Motion_Steer < NoOfSteers; i_Motion_Steer++)
            {
                ChartPoints_Steering_Y[i_Motion_Steer] = Convert.ToDouble(Motion_MotionGUI.motionGUI_MotionChart.chartControl2.Series[0].Points[i_Motion_Steer].GetNumericValue(DevExpress.XtraCharts.ValueLevel.Value));
                ChartPoints_Steering_X[i_Motion_Steer] = Convert.ToDouble(Motion_MotionGUI.motionGUI_MotionChart.chartControl2.Series[0].Points[i_Motion_Steer].Argument);
            }

            Array.Sort(ChartPoints_Steering_X, ChartPoints_Steering_Y);

            if (_deflectionExists)
            {
                WheelDeflectionExists = _deflectionExists;
            }
            if (_steeringExists)
            {
                SteeringExists = _steeringExists;
            }

            if (!IsControlInitialized)
            {
                GetMotion();
            }

        }
        #endregion

        #region De-Serialization of the Motion Class
        public Motion(SerializationInfo info, StreamingContext context)
        {
            ChartPoints_WheelDef_X = (double[])info.GetValue("ChartPoints_WheelDef_X", typeof(double[]));
            ChartPoints_WheelDef_Y = (double[])info.GetValue("ChartPoints_WheelDef_Y", typeof(double[]));
            ChartPoints_Steering_X = (double[])info.GetValue("ChartPoints_Steering_X", typeof(double[]));
            ChartPoints_Steering_Y = (double[])info.GetValue("ChartPoints_Steering_Y", typeof(double[]));

            Final_WheelDeflectionsX = (List<double>)info.GetValue("Final_WheelDeflectionsX", typeof(List<double>));
            Final_WheelDeflectionsY = (List<double>)info.GetValue("Final_WheelDeflectionsY", typeof(List<double>));
            Final_WheelSteerX = (List<double>)info.GetValue("Final_WheelSteerX", typeof(List<double>));
            Final_WheelSteerY = (List<double>)info.GetValue("Final_WheelSteerY", typeof(List<double>));

            NoOfDeflections = (int)info.GetValue("NoOfDeflections", typeof(int));
            NoOfSteers = (int)info.GetValue("NoOfSteers", typeof(int));

            MotionName = (string)info.GetValue("MotionName", typeof(string));
            MotionID = (int)info.GetValue("MotionID", typeof(int));
            MotionCounter = (int)info.GetValue("MotionCounter", typeof(int));
            List_Motion = (List<Motion>)info.GetValue("List_Motion", typeof(List<Motion>));
            Motion_MotionGUI = (MotionGUI)info.GetValue("Motion_MotionGUI", typeof(MotionGUI));
            Motion_DataTable = (DataTable)info.GetValue("Motion_DataTable", typeof(DataTable));

            WheelDeflectionExists = (bool)info.GetValue("WheelDeflectionExists", typeof(bool));
            SteeringExists = (bool)info.GetValue("SteeringExists", typeof(bool));

        }
        #endregion

        #region Serialization of the Motion Class
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ChartPoints_WheelDef_X", ChartPoints_WheelDef_X);
            info.AddValue("ChartPoints_WheelDef_Y", ChartPoints_WheelDef_Y);
            info.AddValue("ChartPoints_Steering_X", ChartPoints_Steering_X);
            info.AddValue("ChartPoints_Steering_Y", ChartPoints_Steering_Y);

            info.AddValue("Final_WheelDeflectionsX", Final_WheelDeflectionsX);
            info.AddValue("Final_WheelDeflectionsY", Final_WheelDeflectionsY);
            info.AddValue("Final_WheelSteerX", Final_WheelSteerX);
            info.AddValue("Final_WheelSteerY", Final_WheelSteerY);

            info.AddValue("NoOfDeflections", NoOfDeflections);
            info.AddValue("NoOfSteers", NoOfSteers);

            info.AddValue("MotionName", MotionName);
            info.AddValue("MotionID", MotionID);
            info.AddValue("MotionCounter", MotionCounter);
            info.AddValue("List_Motion", List_Motion);
            info.AddValue("Motion_MotionGUI", Motion_MotionGUI);
            info.AddValue("Motion_DataTable", Motion_DataTable);

            info.AddValue("WheelDeflectionExists", WheelDeflectionExists);
            info.AddValue("SteeringExists", SteeringExists);

        } 
        #endregion
    }
}

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

namespace Coding_Attempt_with_GUI
{
    public partial class HeatMapControl : XtraUserControl
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public static int Counter { get; set; }

        public BatchRunOutputMode OutputMode { get; set; }

        public HeatMapMode HeatMapType { get; set; }

        public SpecialCaseOption SpecialCase { get; set; }

        public int CornerIdentifier { get; set; }

        public string CornerName { get; set; }

        public double MaxForce { get; set; } = 0;

        public double MinForce { get; set; } = 0;

        public DataTable HeatMapDataSource { get; set; } = new DataTable();

        ///<summary>Creating a List of Link Length Names with the exact same keys as in <see cref="BatchRunResults.OutputChannels"/></summary>
        List<string> LinkNames = new List<string>(new string[] { "Lower Front Wishbone", "Lower Rear Wishbone", "Upper Front Wishbone", "Upper Rear Wishbone", "Pushrod", "Toe Link", "Damper Force" });  

        public static List<HeatMapControl> HeatMaps = new List<HeatMapControl>();

        public BatchRunGUI BatchRunGUI;

        public List<LoadCase> LoadCases;





        public HeatMapControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overloaded Constructor Initialize a Heat Map, create its name, ID and also a Data Table using the <see cref="HeatMapDataSource"/>
        /// </summary>
        /// <param name="_heatMapName">Name of the HeatMap. Pass the proper Name using an extrnal form which accepts the Name</param>
        /// <param name="_heatMapID">If using <see cref="Counter"/> add 1 before passing as inside this constructo, <see cref="ID"/> is not added with 1</param>
        public void ConstructHeatMapControl(string _heatMapName, int _heatMapID, BatchRunOutputMode _outPutMode, HeatMapMode _heatMapMode, SpecialCaseOption _specialCase, int _identifier, string _cornerName)
        {
            ID = _heatMapID;

            Name = _heatMapName + " " + _heatMapID;

            OutputMode = _outPutMode;

            HeatMapType = _heatMapMode;

            SpecialCase = _specialCase;

            CornerIdentifier = _identifier;

            CornerName = _cornerName;

            HeatMapDataSource.TableName = Name;

            HeatMapDataSource = InitializeDataTable(HeatMapDataSource);
        }

        /// <summary>
        /// Method to initialize the Data Datable with the columns based on  the <see cref="OutputMode"/>
        /// </summary>
        /// <param name="_heatMapeSource"></param>
        /// <returns></returns>
        private DataTable InitializeDataTable(DataTable _heatMapeSource)
        {
            int NoOfColumns = 1;

            _heatMapeSource.Columns.Add("Load Case Name", typeof(string));
            _heatMapeSource.Columns[0].ReadOnly = true;

            if (SpecialCase == SpecialCaseOption.None) 
            {

                if (OutputMode == BatchRunOutputMode.MultipleOutputChannel)
                {
                    NoOfColumns = 11;

                    for (int i = 1; i < NoOfColumns; i++)
                    {
                        _heatMapeSource.Columns.Add(Convert.ToString(i * 10) + " %", typeof(double));
                        _heatMapeSource.Columns[i].ReadOnly = true;
                    }

                }
                else if (OutputMode == BatchRunOutputMode.SingleOutputChannel)
                {
                    _heatMapeSource.Columns.Add("100 %", typeof(double));
                    _heatMapeSource.Columns[1].ReadOnly = true;
                } 
            }
            else
            {
                NoOfColumns = 8;

                for (int i = 0; i < LinkNames.Count; i++)
                {
                    _heatMapeSource.Columns.Add(LinkNames[i], typeof(double));
                    _heatMapeSource.Columns[i].ReadOnly = true;
                }


            }


            return _heatMapeSource;
        }

        public void DrawHeatMapSource(string opChannel, BatchRunGUI _batchRunGUI, List<LoadCase> _loadCases)
        {
            BatchRunGUI = _batchRunGUI;

            LoadCases = _loadCases;

            if (HeatMapType == HeatMapMode.RegularHeatMap) 
            {
                if (OutputMode == BatchRunOutputMode.MultipleOutputChannel)
                {
                    DrawHeatMapSource_MultipleOutputChannels(opChannel);
                    DrawHeatMap(11, opChannel);
                }
                else if (OutputMode == BatchRunOutputMode.SingleOutputChannel)
                {
                    DrawHeatMapSource_SingleOutputChannel(opChannel);
                    DrawHeatMap(2, opChannel);
                } 
            }
            else if (HeatMapType == HeatMapMode.SpecialCase)
            {
                DrawHeatMapSource_SpecialCase(LinkNames);
                DrawHeatMap(8, "Highest Load");
            }



        }
        /// <summary>
        /// Method to plot the source <see cref="DataTable"/> of the HeatMap <see cref="DataGrid"/> for <see cref="BatchRunOutputMode.MultipleOutputChannel"/>
        /// </summary>
        /// <param name="_opChannel"></param>
        private void DrawHeatMapSource_MultipleOutputChannels(string _opChannel)
        {
            BatchRunResults results;

            for (int i = 0; i < LoadCases.Count; i++)
            {
                results = AssignBatchRunResults(LoadCases[i]);

                if (results != null)
                {
                    HeatMapDataSource.Rows.Add(LoadCases[i].LoadCaseName, results.ReturnMaxValue(10, _opChannel, results), results.ReturnMaxValue(20, _opChannel, results), results.ReturnMaxValue(30, _opChannel, results),
                                                                          results.ReturnMaxValue(40, _opChannel, results), results.ReturnMaxValue(50, _opChannel, results), results.ReturnMaxValue(60, _opChannel, results),
                                                                          results.ReturnMaxValue(70, _opChannel, results), results.ReturnMaxValue(80, _opChannel, results), results.ReturnMaxValue(90, _opChannel, results), results.ReturnMaxValue(98, _opChannel, results));
                }
            }
        }

        

        /// <summary>
        /// Method to plot the source <see cref="DataTable"/> of the HeatMap <see cref="DataGrid"/> for <see cref="BatchRunOutputMode.SingleOutputChannel"/>
        /// </summary>
        /// <param name="_opChannel"></param>
        private void DrawHeatMapSource_SingleOutputChannel(string _opChannel)
        {
            BatchRunResults results;

            for (int i = 0; i < LoadCases.Count; i++)
            {
                results = AssignBatchRunResults(LoadCases[i]);

                if (results != null)
                {
                    HeatMapDataSource.Rows.Add(LoadCases[i].LoadCaseName, /*results.OutputChannels[_opChannel]["MaxPos"]*/ results.ReturnMaxValue(_opChannel, results));
                }
            }
        }

        /// <summary>
        /// Method to draw the HeatMap for the <see cref="HeatMapMode.SpecialCase"/> where the user will want to see only the Highest Loads
        /// </summary>
        /// <param name="_linkNames"></param>
        private void DrawHeatMapSource_SpecialCase(List<string> _linkNames)
        {
            BatchRunResults results;

            for (int i = 0; i < LoadCases.Count; i++)
            {
                results = AssignBatchRunResults(LoadCases[i]);

                if (results != null)
                {
                    HeatMapDataSource.Rows.Add(LoadCases[i].LoadCaseName, results.ReturnMaxValue(_linkNames[0], results), results.ReturnMaxValue(_linkNames[1], results), results.ReturnMaxValue(_linkNames[2], results), results.ReturnMaxValue(_linkNames[3], results),
                                                                          results.ReturnMaxValue(_linkNames[4], results), results.ReturnMaxValue(_linkNames[5], results), results.ReturnMaxValue(_linkNames[6], results));
                }
            }
        }

        /// <summary>
        /// Method to determine which <see cref="BatchRunResults"/> object is to be used by THIS <see cref="HeatMapControl"/>
        /// </summary>
        /// <param name="_loadCase"></param>
        /// <returns></returns>
        private BatchRunResults AssignBatchRunResults(LoadCase _loadCase)
        {
            if (CornerIdentifier == 1)
            {
                return _loadCase.runResults_FL;
            }
            else if (CornerIdentifier == 2)
            {
                return _loadCase.runResults_FR;
            }
            else if (CornerIdentifier == 3)
            {
                return _loadCase.runResults_RL;
            }
            else if (CornerIdentifier == 4)
            {
                return _loadCase.runResults_RR;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to assign the <see cref="gridControlHeatMap"/> Data Source, Main View and Paint the Heat Map
        /// </summary>
        /// <param name="noOfColumns"></param>
        /// <param name="outputChannel"></param>
        private void DrawHeatMap(int noOfColumns, string outputChannel)
        {
            BatchRunGUI.bandedGridViewBatchRun = CustomBandedGridView.CreateNewBandedGridView(0, noOfColumns, outputChannel + " Heat Map");

            gridControlHeatMap.DataSource = HeatMapDataSource;

            gridControlHeatMap.MainView = BatchRunGUI.bandedGridViewBatchRun;

            BatchRunGUI.bandedGridViewBatchRun = CustomBandedGridColumn.ColumnEditor_ForHeatMapControl(BatchRunGUI.bandedGridViewBatchRun);

            ConditionHeatMap(OutputMode);

            BatchRunGUI.bandedGridViewBatchRun.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

        }

        /// <summary>
        /// Method to condition the Heat Map. Method to find the Maximim and Minimum values in the Solution sort them into variables so that they can be used to assign the Heat Map colors
        /// </summary>
        /// <param name="opMode"></param>
        private void ConditionHeatMap(BatchRunOutputMode opMode)
        {
            int NoofColumns = 0;
            ///<summary>Heat Map Force Values List which will help to determine the Max and Min Force</summary>
            List<double> HeatMapForceValues_List = new List<double>();
            
            ///<summary>Assigning <see cref="NoOfColumns"/> based on the type of <see cref="BatchRunOutputMode"/> and <see cref="HeatMapMode"/></summary>
            if (HeatMapType == HeatMapMode.RegularHeatMap)
            {
                if (opMode == BatchRunOutputMode.MultipleOutputChannel)
                {
                    NoofColumns = 11;
                }
                else if (opMode == BatchRunOutputMode.SingleOutputChannel)
                {
                    NoofColumns = 2;
                } 
            }
            else if (HeatMapType == HeatMapMode.SpecialCase)
            {
                NoofColumns = 8;
            }
            

            ///<summary>Adding the the <see cref="HeatMapDataSource"/> table values to the List. If Loop to avoid adding the Column and Row Titles into the LIst</summary>
            for (int i = 0; i < LoadCases.Count; i++)
            {
                int k = 1;

                for (int j = 0; j < NoofColumns; j++)
                {
                    if (k != NoofColumns)
                    {
                        HeatMapForceValues_List.Add(HeatMapDataSource.Rows[i].Field<double>(k));
                        k++;
                    }

                }
            }

            ///<summary>Sorting and assiging the Max and Min Values of Force</summary>
            HeatMapForceValues_List.Sort();

            if (HeatMapForceValues_List.Count != 0)
            {
                MaxForce = HeatMapForceValues_List[HeatMapForceValues_List.Count - 1];
                MinForce = HeatMapForceValues_List[0];
            }

            ///<summary>Paint event which will paint the heatmap according to the force value in the cell </summary>
            BatchRunGUI.bandedGridViewBatchRun.RowCellStyle += BandedGridViewBatchRun_RowCellStyle;
            

        }

        /// <summary>
        /// Event which will paint the <see cref="gridControlHeatMap"/>'s cells in the Heat Map format according to the color of the corner and also provide special conditioning to the cells with the max and min values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BandedGridViewBatchRun_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            double cellValue = 0;

            FontFamily MaxForceFont = new FontFamily("Tahoma");
            float NewFontSize = 10;


            if (e.Column.AbsoluteIndex != 0)
            {
                cellValue = (double)e.CellValue;
                e.Appearance.BackColor = PaintHeatMap(cellValue, MaxForce, MinForce);
            }

            if (HeatMapType == HeatMapMode.RegularHeatMap)
            {
                if (cellValue == MaxForce)
                {
                    e.Appearance.Font = new Font(MaxForceFont, NewFontSize, FontStyle.Bold);
                    e.Appearance.FontStyleDelta = FontStyle.Underline;
                }
                else if (cellValue == MinForce)
                {
                    e.Appearance.Font = new Font(MaxForceFont, NewFontSize, FontStyle.Bold);
                    e.Appearance.FontStyleDelta = FontStyle.Underline;
                }
            }

            else if (HeatMapType == HeatMapMode.SpecialCase)
            {
                bool FoundHighestLoad = false;

                //if (HeatMapDataSource.Rows.Count > 2)
                //{
                if (e.Column.AbsoluteIndex != 0)
                {

                    for (int i = 0; i < HeatMapDataSource.Rows.Count; i++)
                    {
                        if (Math.Abs((double)e.CellValue) >= Math.Abs(HeatMapDataSource.Rows[i].Field<double>(e.Column.AbsoluteIndex)))
                        {
                            FoundHighestLoad = true;
                        }
                        else
                        {
                            FoundHighestLoad = false;
                            break;
                        }
                    }

                    if (FoundHighestLoad)
                    {
                        e.Appearance.Font = new Font(MaxForceFont, NewFontSize, FontStyle.Bold);
                        e.Appearance.FontStyleDelta = FontStyle.Underline;
                        e.Appearance.FontStyleDelta = FontStyle.Italic;
                    }

                }


                //}


            }


        }


        /// <summary>
        /// Decides which color 'value' represents. Scales the colour based ont he Ratio of Cell Value to (Max-Min) Value
        /// </summary>
        /// <param name="_cellValue"></param>
        /// <param name="_minValue"></param>
        /// <param name="_maxValue"></param>
        /// <returns></returns>
        private Color PaintHeatMap(double _cellValue, double _minValue, double _maxValue)
        {
            double val;
            Color firstColour = Color.DarkGreen;
            Color secondColour = Color.Yellow;

            if (CornerIdentifier == 1)
            {
                firstColour = Color.OrangeRed;
                if (HeatMapType == HeatMapMode.SpecialCase)
                {
                    secondColour = Color.AntiqueWhite; 
                }
                else
                {
                    secondColour = Color.Orange;
                }
            }
            else if (CornerIdentifier == 2)
            {
                firstColour = Color.LimeGreen;
                secondColour = Color.Honeydew;
            }
            else if (CornerIdentifier == 3)
            {
                firstColour = Color.RoyalBlue;
                secondColour = Color.Azure;
            }
            else if (CornerIdentifier == 4)
            {
                firstColour = Color.DarkOrange;
                secondColour = Color.LightYellow;
            }
            

            int deltaR = firstColour.R - secondColour.R;
            int deltaG = firstColour.G - secondColour.G;
            int deltaB = firstColour.B - secondColour.B;

            if (_maxValue == _minValue)  //It avoids NotANumber results if max = min
            {
                val = 1;
            }
            else
            {
                val = ((_cellValue) - _minValue) / (_maxValue - _minValue);
            }
            int r = secondColour.R + Convert.ToInt32(Math.Round((deltaR * (val))));
            int g = secondColour.G + Convert.ToInt32(Math.Round((deltaG * (val))));
            int b = secondColour.B + Convert.ToInt32(Math.Round((deltaB * (val))));

            return Color.FromArgb(255, r, g, b);
        }


    }

    public enum HeatMapMode
    {
        RegularHeatMap,
        SpecialCase

            
    }

    public enum SpecialCaseOption
    {
        HighestLoad,
        //HeaviestCompressive,
        None
    };

}

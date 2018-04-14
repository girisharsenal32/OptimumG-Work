using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Attempt_with_GUI
{
    class Bruno_Heat_Map_Code
    {

        ///Control used: DataGridView from Visual Studio Toolbox

        ///Method that goes through all the cells in the table setting their color.It consults another method(that is below) to define what is the necessary color

        /// <summary>
        /// Changes the color of the cell, showing to the user which values have a bigger or a smaller value. 
        /// Is uses the method HeatMapColor to define which is the right color to the given value of the cell, knowing the maximum and minimum value.
        /// With the argument 'type', the method can Activate (fill with color) and Deactivate (return to white) the Heat Map.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataGridViewCells"></param>
        /// <param name="MatrixOfValues"></param>
        /// <param name="parameter2Index"></param>
        /// <param name="roundValue"></param>
        private void HeatMap(string type, DataGridView dataGridViewCells, double[][,] MatrixOfValues, int parameter2Index, int roundValue)
        {
            //if (type == "Activate")
            //{
            //    var rowCount = Size2[parameter2Index];
            //    var columnCount = Size1[parameter2Index];

            //    //Sweeps through
            //    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            //    {
            //        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            //        {
            //            dataGridViewCells.Rows[rowIndex].Cells[columnIndex].Style.BackColor = HeatMapColor(cellValue, MinValueInTable, MaxValueInTable);
            //        }
            //    }
            //}

            //else if (type == "Deactivate")
            //{
            //    var rowCount = Size2[parameter2Index];
            //    var columnCount = Size1[parameter2Index];

            //    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            //    {
            //        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            //        {
            //            dataGridViewCells.Rows[rowIndex].Cells[columnIndex].Style.BackColor = System.Drawing.Color.White;
            //        }
            //    }
            //}
        }


        ///Method that calculates and returns the color for a specific cell value given the minimum and maximum values possible in that table.It analyzes what is the R, the G, and the B for the limit colors (dark green to yellow) and interpolates in between 
        ///the minimum and maximum for R, G, and B for the given value of the cell.

        /// <summary>
        /// Decides which color 'value' represents. The darkgreen color represents the maximum value and the yellow represents the minimum value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private Color HeatMapColor(double value, double min, double max)
        {
            double val;
            Color firstColour = Color.DarkGreen;
            Color secondColour = Color.Yellow;

            int deltaR = firstColour.R - secondColour.R;
            int deltaG = firstColour.G - secondColour.G;
            int deltaB = firstColour.B - secondColour.B;

            if (max == min)  //It avoids NotANumber results if max = min
            {
                val = 1;
            }
            else
            {
                val = (value - min) / (max - min);
            }
            int r = secondColour.R + Convert.ToInt32(Math.Round((deltaR * (val))));
            int g = secondColour.G + Convert.ToInt32(Math.Round((deltaG * (val))));
            int b = secondColour.B + Convert.ToInt32(Math.Round((deltaB * (val))));

            return Color.FromArgb(255, r, g, b);
        }













    }
}

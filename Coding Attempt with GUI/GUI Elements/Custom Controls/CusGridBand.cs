using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;

namespace Coding_Attempt_with_GUI
{
    
    public class CustomGridBand:GridBand
    {
        public CustomGridBand() : base() { }


        #region Declarations and Initializations
        public static CustomGridBand _band = new CustomGridBand();
        public static CustomGridBand _band_CopyCoordinates = new CustomGridBand();

        #endregion

        #region Creating a new band and its columns 
        public static CustomGridBand CreateNewGridBand(int _NoOfColumns, string _BandName)
        {
            _band = new CustomGridBand();
            _band.Name = _BandName;
            _band.Caption = _BandName;
            _band.AutoFillDown = true;

            _band.AppearanceHeader.Options.UseFont = true;
            _band.AppearanceHeader.FontStyleDelta = FontStyle.Bold;

            _band.AppearanceHeader.Options.UseTextOptions = true;
            _band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            _band.AppearanceHeader.FontSizeDelta = 1;
            
            _band.OptionsBand.AllowSize = true;

            for (int i_NoOfColumns = 0; i_NoOfColumns < _NoOfColumns; i_NoOfColumns++)
            {
                _band.Columns.Add(CustomBandedGridColumn.CreateNewColumn("Column " + (i_NoOfColumns + 1)));
                
            }
            


            return _band;

        }
        public static CustomGridBand CreateNewGridBand_ForSuspension(string _BandName)
        {
            _band_CopyCoordinates = new CustomGridBand();
            _band_CopyCoordinates.Name = _BandName;
            _band_CopyCoordinates.Caption = "Copy Front Left Coordinates to Front Right";

            return _band_CopyCoordinates;
        }


        #endregion


    }
}

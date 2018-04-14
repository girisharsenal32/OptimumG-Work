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
    public class CustomBandedGridView : BandedGridView
    {
        #region Declarations
        public static CustomGridBand band = new CustomGridBand();
        //public static CustomGridBand tire_band_copyCoordinates = new CustomGridBand();
        public static CustomBandedGridView bandedGridView = new CustomBandedGridView();
        #endregion

        public CustomBandedGridView() : base() { }

        #region Creating a new banded Grid View and adding it to the list of objects
        public static CustomBandedGridView CreateNewBandedGridView(int l_InputItem, int NoOfColumns,string _BandName)
        {
            bandedGridView = new CustomBandedGridView();
            bandedGridView.Name = "Banded Grid View";

            band = CustomGridBand.CreateNewGridBand(NoOfColumns, _BandName);

            bandedGridView.Bands.Add(band);
            bandedGridView.OptionsView.ShowColumnHeaders = false;
            bandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
            bandedGridView.OptionsView.ShowGroupPanel = false;

            
            return bandedGridView;

        }

        #endregion


    }
}

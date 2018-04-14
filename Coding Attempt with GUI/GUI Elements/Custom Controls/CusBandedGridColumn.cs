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
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;

namespace Coding_Attempt_with_GUI
{
    
    public class CustomBandedGridColumn:BandedGridColumn
    {
        public static Kinematics_Software_New r1;

        #region Declaration and Initialization
        public static CustomBandedGridColumn tire_band_column = new CustomBandedGridColumn();

        #endregion

        public CustomBandedGridColumn() : base() { }

        public static CustomBandedGridColumn CreateNewColumn(string colName)
        {

            tire_band_column = new CustomBandedGridColumn();
            tire_band_column.Name = colName;
            tire_band_column.BestFit();


            return tire_band_column;
        }

        #region Column Override method to edit the column properties for the Chassis
        public static CustomBandedGridView ColumnEditor_ForChassis(CustomBandedGridView bandedGridView_ForChassis)
        {
            bandedGridView_ForChassis.OptionsView.ShowColumnHeaders = true;

            bandedGridView_ForChassis.Columns[0].Caption = "Component";
            bandedGridView_ForChassis.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForChassis.Columns[0].BestFit();
            bandedGridView_ForChassis.Columns[0].Group();
            bandedGridView_ForChassis.Columns[0].Visible = false;

            bandedGridView_ForChassis.Columns[1].Caption = "Mass (Kg)";
            bandedGridView_ForChassis.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForChassis.Columns[1].BestFit();


            bandedGridView_ForChassis.Columns[2].Caption = "X (mm)";
            bandedGridView_ForChassis.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForChassis.Columns[2].BestFit();


            bandedGridView_ForChassis.Columns[3].Caption = "Y (mm)";
            bandedGridView_ForChassis.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForChassis.Columns[3].BestFit();
   

            bandedGridView_ForChassis.Columns[4].Caption = "Z (mm)";
            bandedGridView_ForChassis.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForChassis.Columns[4].BestFit();



            bandedGridView_ForChassis.ExpandAllGroups();

            return bandedGridView_ForChassis;

        } 
        #endregion

        #region Column Override method to edit the column properties for the Suspension Coordinates
        public static CustomBandedGridView ColumnEditor_ForSuspension(CustomBandedGridView bandedGridView_ForSuspension,Kinematics_Software_New _r1)
        {
            bandedGridView_ForSuspension.OptionsView.ShowColumnHeaders = true;

            bandedGridView_ForSuspension.Columns[0].Caption = "Pick Up Point";
            bandedGridView_ForSuspension.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForSuspension.Columns[0].BestFit();
            bandedGridView_ForSuspension.Columns[0].Group();
            bandedGridView_ForSuspension.Columns[0].Visible = false;

            bandedGridView_ForSuspension.Columns[1].Caption = "X (mm)";
            bandedGridView_ForSuspension.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForSuspension.Columns[1].BestFit();
            bandedGridView_ForSuspension.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridView_ForSuspension.Columns[1].DisplayFormat.FormatString = "n3";

            bandedGridView_ForSuspension.Columns[2].Caption = "Y (mm)";
            bandedGridView_ForSuspension.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForSuspension.Columns[2].BestFit();
            bandedGridView_ForSuspension.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridView_ForSuspension.Columns[2].DisplayFormat.FormatString = "n3";

            bandedGridView_ForSuspension.Columns[3].Caption = "Z (mm)";
            bandedGridView_ForSuspension.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_ForSuspension.Columns[3].BestFit();
            bandedGridView_ForSuspension.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridView_ForSuspension.Columns[3].DisplayFormat.FormatString = "n3";

            bandedGridView_ForSuspension.ExpandAllGroups();

            return bandedGridView_ForSuspension;

        }



        //static void button_Click(object sender, EventArgs e)
        //{
        //    r1.CopyFrontLeftTORight_Click(sender, e);
        //}
        #endregion

        #region Column Override method to edit the column properties for the Motion View
        public static CustomBandedGridView ColumnEditor_ForMotion(CustomBandedGridView bandedGridView_Motion, Kinematics_Software_New _r1)
        {
            bandedGridView_Motion.OptionsView.ShowColumnHeaders = true;

            bandedGridView_Motion.Columns[0].Caption = "Percentage Motion";
            bandedGridView_Motion.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_Motion.Columns[0].BestFit();
            //bandedGridView_Motion.Columns[0].Group();
            bandedGridView_Motion.Columns[0].Visible = true;

            bandedGridView_Motion.Columns[1].Caption = "Wheel Deflection";
            bandedGridView_Motion.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_Motion.Columns[1].BestFit();
            bandedGridView_Motion.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridView_Motion.Columns[1].DisplayFormat.FormatString = "n3";

            bandedGridView_Motion.Columns[2].Caption = "Steering Angle";
            bandedGridView_Motion.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView_Motion.Columns[2].BestFit();
            bandedGridView_Motion.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bandedGridView_Motion.Columns[2].DisplayFormat.FormatString = "n3";

            bandedGridView_Motion.ExpandAllGroups();

            return bandedGridView_Motion;
        }
        #endregion

        #region Column Override method to edit the column properties for the Load Cases
        public static CustomBandedGridView ColumnEditor_ForLoadCases(CustomBandedGridView _bandedGridView, string _caption1, string _caption2)
        {
            //float fontSize = 11;
            //FontFamily LoadCaseFont = new FontFamily("Tahoma");
            //_bandedGridView.Appearance.Row.Font = new Font(LoadCaseFont, fontSize);

            _bandedGridView.OptionsView.ShowColumnHeaders = true;

            _bandedGridView.Columns[0].Caption = _caption1;
            _bandedGridView.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            _bandedGridView.Columns[0].BestFit();

            _bandedGridView.Columns[1].Caption = _caption2;
            _bandedGridView.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            _bandedGridView.Columns[1].BestFit();

            _bandedGridView.ExpandAllGroups();

            return _bandedGridView;
        }
        #endregion

        public static CustomBandedGridView ColumnEditor_ForHeatMapControl(CustomBandedGridView _bandedGridView)
        {
            FontFamily font = new FontFamily("Tahoma");
            float fontSize = 10;

            //_bandedGridView.Columns[0].BestFit();


            for (int i = 0; i < _bandedGridView.Columns.Count; i++)
            {
                _bandedGridView.Columns[i].AppearanceCell.Font = new Font(font, fontSize);
                _bandedGridView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                _bandedGridView.Columns[i].DisplayFormat.FormatString = "n3";
                
            }

            _bandedGridView.Columns[0].AppearanceCell.Font = new Font(font, fontSize, FontStyle.Bold);
            _bandedGridView.Columns[0].AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            _bandedGridView.Columns[0].AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            if (_bandedGridView.Columns.Count > 2)
            {
                if (_bandedGridView.Columns.Count > 8) 
                {
                    //_bandedGridView.Columns[10].Visible = false; 
                }
            }

            _bandedGridView.OptionsView.ShowColumnHeaders = true;

            return _bandedGridView;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Coding_Attempt_with_GUI
{
    public partial class ParametersAndImportance : XtraForm
    {
        #region -- CheckedLIstBoxControl Items---
        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.FrontRollCenter"/>
        /// </summary>
        CheckedListBoxItem FrontRollCenter;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.RearRollCenter"/>
        /// </summary>
        CheckedListBoxItem RearRollCenter;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.LeftPitchCenter"/>
        /// </summary>
        CheckedListBoxItem LeftPitchCenter;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.RightPitchCenter"/>
        /// </summary>
        CheckedListBoxItem RightPitchCenter;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.CamberVariation"/>
        /// </summary>
        CheckedListBoxItem Camber;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.Ackermann"/>
        /// </summary>
        CheckedListBoxItem Ackermann;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.BumpSteer"/>
        /// </summary>
        CheckedListBoxItem BumpSteer;

        /// <summary>
        /// <see cref="CheckedListBoxItem"/> for the <see cref="SuspensionParameters.SpringDeflection"/>
        /// </summary>
        CheckedListBoxItem SpringDeflection;
        #endregion

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> representing the Front Left COrner
        /// </summary>
        public KO_CornverVariables KO_CV_FL { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> representing the Front Right COrner
        /// </summary>
        public KO_CornverVariables KO_CV_FR { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> representing the Rear Left COrner
        /// </summary>
        public KO_CornverVariables KO_CV_RL { get; set; }

        /// <summary>
        /// Object of the <see cref="KO_CornverVariables"/> representing the Rear Right COrner
        /// </summary>
        public KO_CornverVariables KO_CV_RR { get; set; }


        #region ---Initialization Methods---
        public ParametersAndImportance()
        {
            InitializeComponent();

            ///<summary>Populating the <see cref="clb_FL"/></summary>
            Initialize_CLB_FL();

            ///<summary>Populating the <see cref="clb_FR"/></summary>
            Initialize_CLB_FR();

            ///<summary>Populating the <see cref="clb_RL"/></summary>
            Initialize_CLB_RL();

            ///<summary>Populating the <see cref="clb_RR"/></summary>
            Initialize_CLB_RR();

        }

        /// <summary>
        /// Method to extract the <see cref="KO_CornverVariables"/> objects for each of the corners
        /// </summary>
        /// <param name="_flCV"></param>
        /// <param name="_frCV"></param>
        /// <param name="_rlCV"></param>
        /// <param name="_rrCV"></param>
        public void GetParentObjectData(KO_CornverVariables _flCV, KO_CornverVariables _frCV, KO_CornverVariables _rlCV, KO_CornverVariables _rrCV)
        {
            ///<summary>Initializing the <see cref="KO_CornverVariables"/> object of each corner</summary>
            KO_CV_FL = _flCV;

            KO_CV_FR = _frCV;

            KO_CV_RL = _rlCV;

            KO_CV_RR = _rrCV;

            ///<summary>Passing the Corner Variables Object to the <see cref="CustomListBox"/> User Control</summary>
            adjustableListBoxFL.GetCornerVariableObject(KO_CV_FL/*, VehicleCorner.FrontLeft*/);

            adjustableListBoxFR.GetCornerVariableObject(KO_CV_FR/*, VehicleCorner.FrontRight*/);

            adjustableListBoxRL.GetCornerVariableObject(KO_CV_RL/*, VehicleCorner.RearLeft*/);

            adjustableListBoxRR.GetCornerVariableObject(KO_CV_RR/*, VehicleCorner.RearRight*/);

        }

        #region -Initialization - Checked List Box Contorl-
        /// <summary>
        /// Method to initialise all the required <see cref="CheckedListBoxItem"/> for each of the 4 <see cref="CheckedListBoxControl"/>s
        /// </summary>
        private void Initialize_CLB_Items()
        {
            FrontRollCenter = new CheckedListBoxItem(SuspensionParameters.FrontRollCenter.ToString());

            RearRollCenter = new CheckedListBoxItem(SuspensionParameters.RearRollCenter.ToString());

            LeftPitchCenter = new CheckedListBoxItem(SuspensionParameters.LeftPitchCenter.ToString());

            RightPitchCenter = new CheckedListBoxItem(SuspensionParameters.RightPitchCenter.ToString());

            Camber = new CheckedListBoxItem(SuspensionParameters.CamberVariation.ToString());

            Ackermann = new CheckedListBoxItem(SuspensionParameters.Ackermann.ToString());

            BumpSteer = new CheckedListBoxItem(SuspensionParameters.BumpSteer.ToString());

            SpringDeflection = new CheckedListBoxItem(SuspensionParameters.SpringDeflection.ToString());
        }

        /// <summary>
        /// Method to populate the <see cref="clb_FL"/>
        /// </summary>
        private void Initialize_CLB_FL()
        {
            ///<summary>Initializing the <see cref="CheckedListBoxItem"/>s required to populate the 4 <see cref="CheckedListBoxControl"/>s</summary>
            Initialize_CLB_Items();

            clb_FL.Items.AddRange(new CheckedListBoxItem[] { FrontRollCenter, LeftPitchCenter, Camber, BumpSteer, Ackermann, SpringDeflection });
        }

        /// <summary>
        /// Method to populate the <see cref="clb_FR"/>
        /// </summary>
        private void Initialize_CLB_FR()
        {
            ///<summary>Initializing the <see cref="CheckedListBoxItem"/>s required to populate the 4 <see cref="CheckedListBoxControl"/>s</summary>
            Initialize_CLB_Items();

            clb_FR.Items.AddRange(new CheckedListBoxItem[] { /*FrontRollCenter, */RightPitchCenter, Camber, BumpSteer/*, Ackermann*/, SpringDeflection });
        }


        /// <summary>
        /// Method to populate the <see cref="clb_RL"/>
        /// </summary>
        private void Initialize_CLB_RL()
        {
            ///<summary>Initializing the <see cref="CheckedListBoxItem"/>s required to populate the 4 <see cref="CheckedListBoxControl"/>s</summary>
            Initialize_CLB_Items();

            clb_RL.Items.AddRange(new CheckedListBoxItem[] { RearRollCenter, /*LeftPitchCenter,*/ Camber, BumpSteer, SpringDeflection });
        }

        /// <summary>
        /// Method to populate the <see cref="clb_RR"/>
        /// </summary>
        private void Initialize_CLB_RR()
        {
            ///<summary>Initializing the <see cref="CheckedListBoxItem"/>s required to populate the 4 <see cref="CheckedListBoxControl"/>s</summary>
            Initialize_CLB_Items();

            clb_RR.Items.AddRange(new CheckedListBoxItem[] { /*RearRollCenter, */ /*RightPitchCenter,*/ Camber, BumpSteer, SpringDeflection });
        }
        #endregion 

        #endregion



        #region ---Checked List Box Control Item Check Events---
        /// <summary>
        /// Event fired when an item inside the <see cref="clb_FL"/> is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_FL_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (clb_FL.Items[index].CheckState == CheckState.Checked)
            {
                if (!adjustableListBoxFL.listBoxControl1.Items.Contains(clb_FL.Items[index]))
                {
                    adjustableListBoxFL.listBoxControl1.Items.Add(clb_FL.Items[index]);
                }
            }
            else
            {
                if (adjustableListBoxFL.listBoxControl1.Items.Contains(clb_FL.Items[index]))
                {
                    adjustableListBoxFL.listBoxControl1.Items.Remove(clb_FL.Items[index]);
                }
            }

            //KO_CV_FL.KO_ReqParams = adjustableListBoxFL.Req_Params_Importance;

        }

        /// <summary>
        /// Event fired when an item inside the <see cref="clb_FR"/> is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_FR_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (clb_FR.Items[index].CheckState == CheckState.Checked)
            {
                if (!adjustableListBoxFR.listBoxControl1.Items.Contains(clb_FR.Items[index]))
                {
                    adjustableListBoxFR.listBoxControl1.Items.Add(clb_FR.Items[index]);
                }
            }
            else
            {
                if (adjustableListBoxFR.listBoxControl1.Items.Contains(clb_FR.Items[index]))
                {
                    adjustableListBoxFR.listBoxControl1.Items.Remove(clb_FR.Items[index]);
                }
            }

            //KO_CV_FR.KO_ReqParams = adjustableListBoxFR.Req_Params_Importance;


        }

        /// <summary>
        /// Event fired when an item inside the <see cref="clb_RL"/> is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_RL_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (clb_RL.Items[index].CheckState == CheckState.Checked)
            {
                if (!adjustableListBoxRL.listBoxControl1.Items.Contains(clb_RL.Items[index]))
                {
                    adjustableListBoxRL.listBoxControl1.Items.Add(clb_RL.Items[index]);
                }
            }
            else
            {
                if (adjustableListBoxRL.listBoxControl1.Items.Contains(clb_RL.Items[index]))
                {
                    adjustableListBoxRL.listBoxControl1.Items.Remove(clb_RL.Items[index]);
                }
            }

            //KO_CV_RL.KO_ReqParams = adjustableListBoxRL.Req_Params_Importance;

        }

        /// <summary>
        /// Event fired when an item inside the <see cref="clb_RR"/> is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clb_RR_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int index = e.Index;

            if (clb_RR.Items[index].CheckState == CheckState.Checked)
            {
                if (!adjustableListBoxRR.listBoxControl1.Items.Contains(clb_RR.Items[index]))
                {
                    adjustableListBoxRR.listBoxControl1.Items.Add(clb_RR.Items[index]);
                }
            }
            else
            {
                if (adjustableListBoxRR.listBoxControl1.Items.Contains(clb_RR.Items[index]))
                {
                    adjustableListBoxRR.listBoxControl1.Items.Remove(clb_RR.Items[index]);
                }
            }

            //KO_CV_RR.KO_ReqParams = adjustableListBoxRR.Req_Params_Importance;


        }
        #endregion


        

    }
}
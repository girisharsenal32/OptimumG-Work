using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Attempt_with_GUI
{
    public class BatchRunResults
    {


        List<double> LowerFront = new List<double>(), LowerRear = new List<double>(), UpperFront = new List<double>(), UpperRear = new List<double>(), PushRod = new List<double>(), ToeLink = new List<double>(), DamperForce = new List<double>(), ARBDroopLink = new List<double>();

        List<double> LowerFront_x = new List<double>(), LowerFront_y = new List<double>(), LowerFront_z = new List<double>();

        List<double> LowerRear_x = new List<double>(), LowerRear_y = new List<double>(), LowerRear_z = new List<double>();

        List<double> UpperFront_x = new List<double>(), UpperFront_y = new List<double>(), UpperFront_z = new List<double>();

        List<double> UpperRear_x = new List<double>(), UpperRear_y = new List<double>(), UpperRear_z = new List<double>();

        List<double> PushRod_x = new List<double>(), PushRod_y = new List<double>(), PushRod_z = new List<double>();

        List<double> DamperForce_x = new List<double>(), DamperForce_y = new List<double>(), DamperForce_z = new List<double>();

        List<double> ARBDroopLink_x = new List<double>(), ARBDroopLink_y = new List<double>(), ARBDroopLink_z = new List<double>();

        List<double> ToeLink_x = new List<double>(), ToeLink_y = new List<double>(), ToeLink_z = new List<double>();

        List<double> UBJ_x = new List<double>(), UBJ_y = new List<double>(), UBJ_z = new List<double>();

        List<double> LBJ_x = new List<double>(), LBJ_y = new List<double>(), LBJ_z = new List<double>();

        List<double> RackInboard1_x = new List<double>(), RackInboard1_y = new List<double>(), RackInboard1_z = new List<double>();

        List<double> RackInboard2_x = new List<double>(), RackInboard2_y = new List<double>(), RackInboard2_z = new List<double>();

        List<double> ARBInboard1_x = new List<double>(), ARBInboard1_y = new List<double>(), ARBInboard1_z = new List<double>();

        List<double> ARBInboard2_x = new List<double>(), ARBInboard2_y = new List<double>(), ARBInboard2_z = new List<double>();

        List<double> SColumnInboard1_x = new List<double>(), SColumnInboard1_y = new List<double>(), SColumnInboard1_z = new List<double>();

        List<double> SColumnInboard2_x = new List<double>(), SColumnInboard2_y = new List<double>(), SColumnInboard2_z = new List<double>();



        Dictionary<string, double> LowerFront_Dic = new Dictionary<string, double>(), LowerRear_Dic = new Dictionary<string, double>(), UpperFront_Dic = new Dictionary<string, double>(), UpperRear_Dic = new Dictionary<string, double>(),
                                   PushRod_Dic = new Dictionary<string, double>(), ToeLink_Dic = new Dictionary<string, double>(), DamperForce_Dic = new Dictionary<string, double>(), ARBDroopLink_Dic = new Dictionary<string, double>();

        Dictionary<string, double> LowerFront_x_Dic = new Dictionary<string, double>(), LowerFront_y_Dic = new Dictionary<string, double>(), LowerFront_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> LowerRear_x_Dic = new Dictionary<string, double>(), LowerRear_y_Dic = new Dictionary<string, double>(), LowerRear_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> UpperFront_x_Dic = new Dictionary<string, double>(), UpperFront_y_Dic = new Dictionary<string, double>(), UpperFront_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> UpperRear_x_Dic = new Dictionary<string, double>(), UpperRear_y_Dic = new Dictionary<string, double>(), UpperRear_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> PushRod_x_Dic = new Dictionary<string, double>(), PushRod_y_Dic = new Dictionary<string, double>(), PushRod_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> DamperForce_x_Dic = new Dictionary<string, double>(), DamperForce_y_Dic = new Dictionary<string, double>(), DamperForce_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> ARBDroopLink_x_Dic = new Dictionary<string, double>(), ARBDroopLink_y_Dic = new Dictionary<string, double>(), ARBDroopLink_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> ToeLink_x_Dic = new Dictionary<string, double>(), ToeLink_y_Dic = new Dictionary<string, double>(), ToeLink_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> UBJ_x_Dic = new Dictionary<string, double>(), UBJ_y_Dic = new Dictionary<string, double>(), UBJ_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> LBJ_x_Dic = new Dictionary<string, double>(), LBJ_y_Dic = new Dictionary<string, double>(), LBJ_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> RackInboard1_x_Dic = new Dictionary<string, double>(), RackInboard1_y_Dic = new Dictionary<string, double>(), RackInboard1_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> RackInboard2_x_Dic = new Dictionary<string, double>(), RackInboard2_y_Dic = new Dictionary<string, double>(), RackInboard2_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> ARBInboard1_x_Dic = new Dictionary<string, double>(), ARBInboard1_y_Dic = new Dictionary<string, double>(), ARBInboard1_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> ARBInboard2_x_Dic = new Dictionary<string, double>(), ARBInboard2_y_Dic = new Dictionary<string, double>(), ARBInboard2_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> SColumnInboard1_x_Dic = new Dictionary<string, double>(), SColumnInboard1_y_Dic = new Dictionary<string, double>(), SColumnInboard1_z_Dic = new Dictionary<string, double>();

        Dictionary<string, double> SColumnInboard2_x_Dic = new Dictionary<string, double>(), SColumnInboard2_y_Dic = new Dictionary<string, double>(), SColumnInboard2_z_Dic = new Dictionary<string, double>();


        public Dictionary<string, Dictionary<string, double>> OutputChannels = new Dictionary<string, Dictionary<string, double>>();


        /// <summary>
        /// Method to assign the Suspension Forces to the Batch Run Results
        /// </summary>
        /// <param name="_ocResults"></param>
        public void AssignSuspensionForces(OutputClass _ocResults)
        {
            LowerFront.Add(_ocResults.LowerFront);
            LowerFront_x.Add(_ocResults.LowerFront_x);
            LowerFront_y.Add(_ocResults.LowerFront_y);
            LowerFront_z.Add(_ocResults.LowerFront_z);


            LowerRear.Add(_ocResults.LowerRear);
            LowerRear_x.Add(_ocResults.LowerRear_x);
            LowerRear_y.Add(_ocResults.LowerRear_y);
            LowerRear_z.Add(_ocResults.LowerRear_z);


            UpperFront.Add(_ocResults.UpperFront);
            UpperFront_x.Add(_ocResults.UpperFront_x);
            UpperFront_y.Add(_ocResults.UpperFront_y);
            UpperFront_z.Add(_ocResults.UpperFront_z);


            UpperRear.Add(_ocResults.UpperRear);
            UpperRear_x.Add(_ocResults.UpperRear_x);
            UpperRear_y.Add(_ocResults.UpperRear_y);
            UpperRear_z.Add(_ocResults.UpperRear_z);


            PushRod.Add(_ocResults.PushRod);
            PushRod_x.Add(_ocResults.PushRod_x);
            PushRod_y.Add(_ocResults.PushRod_y);
            PushRod_z.Add(_ocResults.PushRod_z);


            ToeLink.Add(_ocResults.ToeLink);
            ToeLink_x.Add(_ocResults.ToeLink_x);
            ToeLink_y.Add(_ocResults.ToeLink_y);
            ToeLink_z.Add(_ocResults.ToeLink_z);


            DamperForce.Add(_ocResults.DamperForce);
            DamperForce_x.Add(_ocResults.DamperForce_x);
            DamperForce_y.Add(_ocResults.DamperForce_y);
            DamperForce_z.Add(_ocResults.DamperForce_z);


            ARBDroopLink.Add(_ocResults.ARBDroopLink);
            ARBDroopLink_x.Add(_ocResults.ARBDroopLink_x);
            ARBDroopLink_y.Add(_ocResults.ARBDroopLink_y);
            ARBDroopLink_z.Add(_ocResults.ARBDroopLink_z);


            UBJ_x.Add(_ocResults.UBJ_x);
            UBJ_y.Add(_ocResults.UBJ_y);
            UBJ_z.Add(_ocResults.UBJ_z);

            LBJ_x.Add(_ocResults.LBJ_x);
            LBJ_y.Add(_ocResults.LBJ_y);
            LBJ_z.Add(_ocResults.LBJ_z);
        }

        /// <summary>
        /// Method to assign the ARB Bearing CapForces to the Batch Run Results
        /// </summary>
        /// <param name="_ocResults"></param>
        public void AssignBearingCapForces_ARB(OutputClass _ocResults)
        {
            ARBInboard1_x.Add(_ocResults.ARBInboard1_x);
            ARBInboard1_y.Add(_ocResults.ARBInboard1_y);
            ARBInboard1_z.Add(_ocResults.ARBInboard1_z);

            ARBInboard2_x.Add(_ocResults.ARBInboard2_x);
            ARBInboard2_y.Add(_ocResults.ARBInboard2_y);
            ARBInboard2_z.Add(_ocResults.ARBInboard2_z);
        }

        /// <summary>
        /// Method to assign the Rack Bearing Cap Forces Forces to the Batch Run Results
        /// </summary>
        /// <param name="_ocResultsLeft"></param>
        public void AssignBearingCapForces_Rack(OutputClass _ocResultsLeft)
        {
            RackInboard1_x.Add(_ocResultsLeft.RackInboard1_x);
            RackInboard1_y.Add(_ocResultsLeft.RackInboard1_y);
            RackInboard1_z.Add(_ocResultsLeft.RackInboard1_z);

            RackInboard2_x.Add(_ocResultsLeft.RackInboard2_x);
            RackInboard2_y.Add(_ocResultsLeft.RackInboard2_y);
            RackInboard2_z.Add(_ocResultsLeft.RackInboard2_z);
        }

        /// <summary>
        /// Method to assign the Steering Column Forces to the Batch Run Results
        /// </summary>
        /// <param name="_ocResults"></param>
        public void AssignBearingCapForces_SteeringColumn(OutputClass _ocResults)
        {
            SColumnInboard1_x.Add(_ocResults.SColumnInboard1_x);
            SColumnInboard1_y.Add(_ocResults.SColumnInboard1_y);
            SColumnInboard1_z.Add(_ocResults.SColumnInboard1_z);

            SColumnInboard2_x.Add(_ocResults.SColumnInboard2_x);
            SColumnInboard2_y.Add(_ocResults.SColumnInboard2_y);
            SColumnInboard2_z.Add(_ocResults.SColumnInboard2_z);
        }

        /// <summary>
        /// Method to Sort the Suspension Forces into the Maximum Positive and Maximum Negative and enter the corresponding values into a <see cref="Dictionary{TKey, TValue}"/> Max Positive and Max Negative values
        /// </summary>
        /// <param name="_noOfSteps"></param>
        public void SortSuspensionForces(int _noOfSteps)
        {
            LowerFront_Dic = GeneralSorter(_noOfSteps, LowerFront, LowerFront_Dic);
            LowerFront_x_Dic = GeneralSorter(_noOfSteps, LowerFront_x, LowerFront_x_Dic);
            LowerFront_y_Dic = GeneralSorter(_noOfSteps, LowerFront_y, LowerFront_y_Dic);
            LowerFront_z_Dic = GeneralSorter(_noOfSteps, LowerFront_z, LowerFront_z_Dic);


            LowerRear_Dic = GeneralSorter(_noOfSteps, LowerRear, LowerRear_Dic);
            LowerRear_x_Dic = GeneralSorter(_noOfSteps, LowerRear_x, LowerRear_x_Dic);
            LowerRear_y_Dic = GeneralSorter(_noOfSteps, LowerRear_y, LowerRear_y_Dic);
            LowerRear_z_Dic = GeneralSorter(_noOfSteps, LowerRear_z, LowerRear_z_Dic);


            UpperFront_Dic = GeneralSorter(_noOfSteps, UpperFront, UpperFront_Dic);
            UpperFront_x_Dic = GeneralSorter(_noOfSteps, UpperFront_x, UpperFront_x_Dic);
            UpperFront_y_Dic = GeneralSorter(_noOfSteps, UpperFront_y, UpperFront_y_Dic);
            UpperFront_z_Dic = GeneralSorter(_noOfSteps, UpperFront_z, UpperFront_z_Dic);


            UpperRear_Dic = GeneralSorter(_noOfSteps, UpperRear, UpperRear_Dic);
            UpperRear_x_Dic = GeneralSorter(_noOfSteps, UpperRear_x, UpperRear_x_Dic);
            UpperRear_y_Dic = GeneralSorter(_noOfSteps, UpperRear_y, UpperRear_y_Dic);
            UpperRear_z_Dic = GeneralSorter(_noOfSteps, UpperRear_z, UpperRear_z_Dic);


            PushRod_Dic = GeneralSorter(_noOfSteps, PushRod, PushRod_Dic);
            PushRod_x_Dic = GeneralSorter(_noOfSteps, PushRod_x, PushRod_x_Dic);
            PushRod_y_Dic = GeneralSorter(_noOfSteps, PushRod_y, PushRod_y_Dic);
            PushRod_z_Dic = GeneralSorter(_noOfSteps, PushRod_z, PushRod_z_Dic);


            ToeLink_Dic = GeneralSorter(_noOfSteps, ToeLink, ToeLink_Dic);
            ToeLink_x_Dic = GeneralSorter(_noOfSteps, ToeLink_x, ToeLink_x_Dic);
            ToeLink_y_Dic = GeneralSorter(_noOfSteps, ToeLink_y, ToeLink_y_Dic);
            ToeLink_z_Dic = GeneralSorter(_noOfSteps, ToeLink_z, ToeLink_z_Dic);


            DamperForce_Dic = GeneralSorter(_noOfSteps, DamperForce, DamperForce_Dic);
            DamperForce_x_Dic = GeneralSorter(_noOfSteps, DamperForce_x, DamperForce_x_Dic);
            DamperForce_y_Dic = GeneralSorter(_noOfSteps, DamperForce_y, DamperForce_y_Dic);
            DamperForce_z_Dic = GeneralSorter(_noOfSteps, DamperForce_z, DamperForce_z_Dic);


            ARBDroopLink_Dic = GeneralSorter(_noOfSteps, ARBDroopLink, ARBDroopLink_Dic);
            ARBDroopLink_x_Dic = GeneralSorter(_noOfSteps, ARBDroopLink_x, ARBDroopLink_x_Dic);
            ARBDroopLink_y_Dic = GeneralSorter(_noOfSteps, ARBDroopLink_y, ARBDroopLink_y_Dic);
            ARBDroopLink_z_Dic = GeneralSorter(_noOfSteps, ARBDroopLink_z, ARBDroopLink_z_Dic);


            UBJ_x_Dic = GeneralSorter(_noOfSteps, UBJ_x, UBJ_x_Dic);
            UBJ_y_Dic = GeneralSorter(_noOfSteps, UBJ_y, UBJ_y_Dic);
            UBJ_z_Dic = GeneralSorter(_noOfSteps, UBJ_z, UBJ_z_Dic);


            LBJ_x_Dic = GeneralSorter(_noOfSteps, LBJ_x, LBJ_x_Dic);
            LBJ_y_Dic = GeneralSorter(_noOfSteps, LBJ_y, LBJ_y_Dic);
            LBJ_z_Dic = GeneralSorter(_noOfSteps, LBJ_z, LBJ_z_Dic);

            OutputChannelSuspensionForces();
        }

        /// <summary>
        /// Method to Sort the ARB Bearing Cap Forces into the Maximum Positive and Maximum Negative and enter the corresponding values into a <see cref="Dictionary{TKey, TValue}"/> Max Positive and Max Negative values
        /// </summary>
        /// <param name="_noOfSteps"></param>
        public void SortBearingCapForces_ARB(int _noOfSteps)
        {

            ARBInboard1_x_Dic = GeneralSorter(_noOfSteps, ARBInboard1_x, ARBInboard1_x_Dic);
            ARBInboard1_y_Dic = GeneralSorter(_noOfSteps, ARBInboard1_y, ARBInboard1_y_Dic);
            ARBInboard1_z_Dic = GeneralSorter(_noOfSteps, ARBInboard1_z, ARBInboard1_z_Dic);


            ARBInboard2_x_Dic = GeneralSorter(_noOfSteps, ARBInboard2_x, ARBInboard2_x_Dic);
            ARBInboard2_y_Dic = GeneralSorter(_noOfSteps, ARBInboard2_y, ARBInboard2_y_Dic);
            ARBInboard2_z_Dic = GeneralSorter(_noOfSteps, ARBInboard2_z, ARBInboard2_z_Dic);

            OutputChannelBearingCapForces_ARB();

        }

        /// <summary>
        /// Method to Sort the Rack Bearing Cap Forces into the Maximum Positive and Maximum Negative and enter the corresponding values into a <see cref="Dictionary{TKey, TValue}"/> Max Positive and Max Negative values
        /// </summary>
        /// <param name="_noOfSteps"></param>
        public void SortBeatingCapForces_Rack(int _noOfSteps)
        {

            RackInboard1_x_Dic = GeneralSorter(_noOfSteps, RackInboard1_x, RackInboard1_x_Dic);
            RackInboard1_y_Dic = GeneralSorter(_noOfSteps, RackInboard1_y, RackInboard1_y_Dic);
            RackInboard1_z_Dic = GeneralSorter(_noOfSteps, RackInboard1_z, RackInboard1_z_Dic);


            RackInboard2_x_Dic = GeneralSorter(_noOfSteps, RackInboard2_x, RackInboard2_x_Dic);
            RackInboard2_y_Dic = GeneralSorter(_noOfSteps, RackInboard2_y, RackInboard2_y_Dic);
            RackInboard2_z_Dic = GeneralSorter(_noOfSteps, RackInboard2_z, RackInboard2_z_Dic);

            OutputChannelBearingCapForces_Rack();
        }

        /// <summary>
        /// Method to Sort the Steering Column Bearing Cap Forces into the Maximum Positive and Maximum Negative and enter the corresponding values into a <see cref="Dictionary{TKey, TValue}"/> Max Positive and Max Negative values
        /// </summary>
        /// <param name="_noOfSteps"></param>
        public void SortBearingCapForces_SteeringColumn(int _noOfSteps)
        {
            SColumnInboard1_x_Dic = GeneralSorter(_noOfSteps, SColumnInboard1_x, SColumnInboard1_x_Dic);
            SColumnInboard1_y_Dic = GeneralSorter(_noOfSteps, SColumnInboard1_y, SColumnInboard1_y_Dic);
            SColumnInboard1_z_Dic = GeneralSorter(_noOfSteps, SColumnInboard1_z, SColumnInboard1_z_Dic);


            SColumnInboard2_x_Dic = GeneralSorter(_noOfSteps, SColumnInboard2_x, SColumnInboard2_x_Dic);
            SColumnInboard2_y_Dic = GeneralSorter(_noOfSteps, SColumnInboard2_y, SColumnInboard2_y_Dic);
            SColumnInboard2_z_Dic = GeneralSorter(_noOfSteps, SColumnInboard2_z, SColumnInboard2_z_Dic);

            OutputChannelBearingCapForces_SteeringColumn();

        }

        /// <summary>
        /// General Method to Sort the Any Force List into the Maximum Positive and Maximum Negative and enter the corresponding values into a <see cref="Dictionary{TKey, TValue}"/> Max Positive and Max Negative values
        /// </summary>
        /// <param name="_NoOfSteps"></param>
        /// <param name="_ChannelToBeSorted"></param>
        /// <param name="_DictionaryToAssign"></param>
        /// <returns></returns>
        private Dictionary<string, double> GeneralSorter(int _NoOfSteps, List<double> _ChannelToBeSorted, Dictionary<string, double> _DictionaryToAssign)
        {
            List<double> tempPost = new List<double>();
            List<double> tempNeg = new List<double>();

            int j;

            #region Initializing the Dictionary
            j = 1;
            if (!_DictionaryToAssign.ContainsKey("MaxPos"))
            {
                _DictionaryToAssign.Add("MaxPos", 0);
            }
            for (int i = 0; i < _NoOfSteps; i++)
            {

                if (i == j * 10 || i == _NoOfSteps - 2) 
                {
                    if (!_DictionaryToAssign.ContainsKey("MaxPos" + i))
                    {
                        _DictionaryToAssign.Add("MaxPos" + i, 0);
                    }
                    j++;
                }
            }

            if (!_DictionaryToAssign.ContainsKey("MaxNeg"))
            {
                _DictionaryToAssign.Add("MaxNeg", 0);
            }

            j = 1;
            for (int i = 0; i < _NoOfSteps; i++)
            {
                if (i == j * 10 || i == _NoOfSteps - 2) 
                {
                    if (!_DictionaryToAssign.ContainsKey("MaxNeg" + i))
                    {
                        _DictionaryToAssign.Add("MaxNeg" + i, 0);
                    }
                    j++;
                }
            }
            #endregion

            j = 1;
            for (int i = 0; i < _NoOfSteps; i++)
            {
                if (_ChannelToBeSorted[i] > 0)
                {
                    tempPost.Add(_ChannelToBeSorted[i]);

                    if (i == j * 10 || i == _NoOfSteps - 2)
                    {
                        GeneralSorter_Helper(_DictionaryToAssign, tempPost, i, false);
                        j++;
                        tempPost.Clear();
                    }

                }
            }
            j = 1;
            for (int i = 0; i < _NoOfSteps; i++)
            {
                if (_ChannelToBeSorted[i] < 0)
                {
                    tempNeg.Add(_ChannelToBeSorted[i]);

                    if (i == j * 10 || i == _NoOfSteps - 2)
                    {
                        GeneralSorter_Helper(_DictionaryToAssign, tempNeg, i, true);
                        j++;
                        tempNeg.Clear();
                    }
                }
            }


            if (_NoOfSteps == 1)
            {

                tempPost.Sort();
                if (tempPost.Count != 0)
                {
                    _DictionaryToAssign["MaxPos"] = tempPost[tempPost.Count - 1];
                }


                tempNeg.Sort();
                if (tempNeg.Count != 0)
                {
                    _DictionaryToAssign["MaxNeg"] = tempNeg[0];
                }
            }


            return _DictionaryToAssign;

        }

        /// <summary>
        /// Method which splits the Motion Percentage into groups of 10 and assigns the Highest positive and highest negative values within that group of 10 to the MaxPos and MaxNeg keys of the dictionary
        /// </summary>
        /// <param name="_dictionaryToAssign"></param>
        /// <param name="_tempOutputChannel"></param>
        /// <param name="_motionPercentage"></param>
        /// <param name="_negativeValues"></param>
        private void GeneralSorter_Helper(Dictionary<string, double> _dictionaryToAssign, List<double> _tempOutputChannel, int _motionPercentage, bool _negativeValues)
        {
            ///<summary>Sorting the array in the ASCENDING ORDER</summary>
            _tempOutputChannel.Sort();

            if (_tempOutputChannel.Count != 0)
            {
                if (!_negativeValues)
                {
                    _dictionaryToAssign["MaxPos" + _motionPercentage] = _tempOutputChannel[_tempOutputChannel.Count - 1];

                    ///<summary>The key called MasPos inside the dictionary will contain the MOST HEAVIEST TENSILE LOAD experienced by a particula component for a particulal load </summary>
                    if (_dictionaryToAssign["MaxPos"] < _dictionaryToAssign["MaxPos" + _motionPercentage])
                    {
                        _dictionaryToAssign["MaxPos"] = _tempOutputChannel[_tempOutputChannel.Count - 1];
                    }
                }
                else if (_negativeValues)
                {
                    _dictionaryToAssign["MaxNeg" + _motionPercentage] = _tempOutputChannel[0];

                    ///<summary>The key called MaxNeg inside the dictionary will contain the MOST HEAVIEST COMPRESSIVE LOAD experienced by a particula component for a particulal load </summary>
                    if (_dictionaryToAssign["MaxNeg"] > _dictionaryToAssign["MaxNeg" + _motionPercentage])
                    {
                        _dictionaryToAssign["MaxNeg"] = _tempOutputChannel[0];
                    }
                }
                
            }

        }

        /// <summary>
        /// Method to Assign the Suspension Component Force Dictionaries which have been sorted to a Master Public Dictionary. <see cref="OutputChannels"/>
        /// </summary>
        private void OutputChannelSuspensionForces()
        {
            OutputChannels.Clear();

            OutputChannels.Add("Lower Front Wishbone", LowerFront_Dic);
            OutputChannels.Add("Lower Front Wishbone X", LowerFront_z_Dic);
            OutputChannels.Add("Lower Front Wishbone Y", LowerFront_x_Dic);
            OutputChannels.Add("Lower Front Wishbone Z", LowerFront_y_Dic);



            OutputChannels.Add("Lower Rear Wishbone", LowerRear_Dic);
            OutputChannels.Add("Lower Rear Wishbone X", LowerRear_z_Dic);
            OutputChannels.Add("Lower Rear Wishbone Y", LowerRear_x_Dic);
            OutputChannels.Add("Lower Rear Wishbone Z", LowerRear_y_Dic);



            OutputChannels.Add("Upper Front Wishbone", UpperFront_Dic);
            OutputChannels.Add("Upper Front Wishbone X", UpperFront_z_Dic);
            OutputChannels.Add("Upper Front Wishbone Y", UpperFront_x_Dic);
            OutputChannels.Add("Upper Front Wishbone Z", UpperFront_y_Dic);



            OutputChannels.Add("Upper Rear Wishbone", UpperRear_Dic);
            OutputChannels.Add("Upper Rear Wishbone X", UpperRear_z_Dic);
            OutputChannels.Add("Upper Rear Wishbone Y", UpperRear_x_Dic);
            OutputChannels.Add("Upper Rear Wishbone Z", UpperRear_y_Dic);



            OutputChannels.Add("Pushrod", PushRod_Dic);
            OutputChannels.Add("Pushrod X", PushRod_z_Dic);
            OutputChannels.Add("Pushrod Y", PushRod_x_Dic);
            OutputChannels.Add("Pushrod Z", PushRod_y_Dic);



            OutputChannels.Add("Toe Link", ToeLink_Dic);
            OutputChannels.Add("Toe Link X", ToeLink_z_Dic);
            OutputChannels.Add("Toe Link Y", ToeLink_x_Dic);
            OutputChannels.Add("Toe Link Z", ToeLink_y_Dic);



            OutputChannels.Add("Damper Force", DamperForce_Dic);
            OutputChannels.Add("Damper Force X", DamperForce_z_Dic);
            OutputChannels.Add("Damper Force Y", DamperForce_x_Dic);
            OutputChannels.Add("Damper Force Z", DamperForce_y_Dic);



            OutputChannels.Add("ARB Droop Link", ARBDroopLink_Dic);
            OutputChannels.Add("ARB Droop Link X", ARBDroopLink_z_Dic);
            OutputChannels.Add("ARB Droop Link Y", ARBDroopLink_x_Dic);
            OutputChannels.Add("ARB Droop Link Z", ARBDroopLink_y_Dic);


            OutputChannels.Add("Upper Ball Joint X", UBJ_z_Dic);
            OutputChannels.Add("Upper Ball Joint Y", UBJ_x_Dic);
            OutputChannels.Add("Upper Ball Joint Z", UBJ_y_Dic);


            OutputChannels.Add("Lower Ball Joint X", LBJ_z_Dic);
            OutputChannels.Add("Lower Ball Joint Y", LBJ_x_Dic);
            OutputChannels.Add("Lower Ball Joint Z", LBJ_y_Dic);


        }

        /// <summary>
        /// Method to Assign the ARB Bearing Cap Force Dictionaries which have been sorted to a Master Public Dictionary. <see cref="OutputChannels"/>
        /// </summary>
        private void OutputChannelBearingCapForces_ARB()
        {
            OutputChannels.Add("ARB Bearing Cap Left X", ARBInboard1_z_Dic);
            OutputChannels.Add("ARB Bearing Cap Left Y", ARBInboard1_x_Dic);
            OutputChannels.Add("ARB Bearing Cap Left Z", ARBInboard1_y_Dic);

            OutputChannels.Add("ARB Bearing Cap Right X", ARBInboard2_z_Dic);
            OutputChannels.Add("ARB Bearing Cap Right Y", ARBInboard2_x_Dic);
            OutputChannels.Add("ARB Bearing Cap Right Z", ARBInboard2_y_Dic);

        }

        /// <summary>
        /// Method to Assign the Rack Bearing Cap Component Force Dictionaries which have been sorted to a Master Public Dictionary. <see cref="OutputChannels"/>
        /// </summary>
        private void OutputChannelBearingCapForces_Rack()
        {
            OutputChannels.Add("Steering Rack Cap Left X", RackInboard1_z_Dic);
            OutputChannels.Add("Steering Rack Cap Left Y", RackInboard1_x_Dic);
            OutputChannels.Add("Steering Rack Cap Left Z", RackInboard1_y_Dic);

            OutputChannels.Add("Steering Rack Cap Right X", RackInboard2_z_Dic);
            OutputChannels.Add("Steering Rack Cap Right Y", RackInboard2_x_Dic);
            OutputChannels.Add("Steering Rack Cap Right Z", RackInboard2_y_Dic);


        }

        /// <summary>
        /// Method to Assign the Steering Column Bearing Cap Component Force Dictionaries which have been sorted to a Master Public Dictionary. <see cref="OutputChannels"/>
        /// </summary>
        private void OutputChannelBearingCapForces_SteeringColumn()
        {
            OutputChannels.Add("Steering Column Cap Left X", SColumnInboard1_z_Dic);
            OutputChannels.Add("Steering Column Cap Left Y", SColumnInboard1_x_Dic);
            OutputChannels.Add("Steering Column Cap Left Z", SColumnInboard1_y_Dic);


            OutputChannels.Add("Steering Column Cap Right X", SColumnInboard2_z_Dic);
            OutputChannels.Add("Steering Column Cap Right Y", SColumnInboard2_x_Dic);
            OutputChannels.Add("Steering Column Cap Right Z", SColumnInboard2_y_Dic);

        }

        /// <summary>
        /// Method to compare the Most Positive and Most Negative value of Force that an Output Channel experiences when subject to Motion and return the Most Negative or Most Positive whichever is greater when the motion is divided into intervals of 10. 
        /// </summary>
        /// <param name="_motionPercentage"></param>
        /// <param name="_opChannel"></param>
        /// <param name="_resultToEvaluate"></param>
        /// <returns></returns>
        public double ReturnMaxValue(int _motionPercentage, string _opChannel, BatchRunResults _resultToEvaluate)
        {
            if (_resultToEvaluate.OutputChannels[_opChannel]["MaxPos" + _motionPercentage] > Math.Abs(_resultToEvaluate.OutputChannels[_opChannel]["MaxNeg" + _motionPercentage]))
            {
                return _resultToEvaluate.OutputChannels[_opChannel]["MaxPos" + _motionPercentage];
            }
            else if (_resultToEvaluate.OutputChannels[_opChannel]["MaxPos" + _motionPercentage] < Math.Abs(_resultToEvaluate.OutputChannels[_opChannel]["MaxNeg" + _motionPercentage]))
            {
                return _resultToEvaluate.OutputChannels[_opChannel]["MaxNeg" + _motionPercentage];
            }

            return 0;
        }

        /// <summary>
        /// Method to return the Maximum value out of the MaxPos and MaxNeg keys. Used for <see cref="HeatMapMode.SpecialCase"/>
        /// </summary>
        /// <param name="_opChannel"></param>
        /// <param name="_resultsToEvaluate"></param>
        /// <returns></returns>
        public double ReturnMaxValue(string _opChannel, BatchRunResults _resultToEvaluate)
        {
            if (_resultToEvaluate.OutputChannels[_opChannel]["MaxPos"] > Math.Abs(_resultToEvaluate.OutputChannels[_opChannel]["MaxNeg"]))
            {
                return _resultToEvaluate.OutputChannels[_opChannel]["MaxPos"];
            }
            else if (_resultToEvaluate.OutputChannels[_opChannel]["MaxPos"] < Math.Abs(_resultToEvaluate.OutputChannels[_opChannel]["MaxNeg"]))
            {
                return _resultToEvaluate.OutputChannels[_opChannel]["MaxNeg"];
            }

            return 0;
        }
























    }
}

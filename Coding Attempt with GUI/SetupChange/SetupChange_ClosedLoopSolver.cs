using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using MathNet.Spatial.Units;

namespace Coding_Attempt_with_GUI
{
    public class SetupChange_ClosedLoopSolver
    {
        /// <summary>
        /// List which holds the HISTORY of DELTAS of Camber. This delta  is about the Static Wheel Spindle Position whicih (in most cases) has a Static Camber
        /// </summary>
        public List<Angle> Summ_dCamber { get; set; } = new List<Angle>();
        /// <summary>
        /// List which holds the HISTORY of ABSOLUTE Camber. This absolute value is about the Horizontal
        /// </summary>
        public List<Angle> Final_Camber { get; set; } = new List<Angle>();
        /// <summary>
        /// Angle which is to be rotated to get the Camber which the user wants. 
        /// If camber is the first change requested then the first value of the list will be the requested value. Then as the iterative loop runs this list will keep getting updated until the Requested Camber is ACTUALLY reached
        /// It can also contain the angle to be UNROTATED to get the requred camber (In case changing something else inadvertantly changed the camber)
        /// </summary>
        public List<Angle> AngleToBeRotatedForCamber { get; set; } = new List<Angle>();
        /// <summary>
        /// This is the List which stores the History of the Camber Adjuster Length deltas.This would contain the ACTUAL Adjuster(shim Link Length) Change. This List is as of now useless because the <see cref="Final_CamberAdjusterLength"/> List is going to hold the same data. 
        /// I am maintaining it nonetheless to maintain consistency in the coding structure of this class
        /// </summary>
        public List<double> Summ_dCamberAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// This is the List which stores the History of the ShimLinkLength DELTAS (Since there is no Static Number of Shims that I have to assign FOR NOW).
        /// This would contain the ACTUAL Shim Link Length Change. 
        /// </summary>
        public List<double> Final_CamberAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// Not used 
        /// </summary>
        public List<double> ShimLengthNeededForCamber { get; set; } = new List<double>();

        /// <summary>
        /// List which holds the HISTORY of DELTAS of Toe. This delta  is about the Static Wheel Spindle Position whicih (in most cases) has a Static Toe
        /// </summary>
        public List<Angle> Summ_dToe { get; set; } = new List<Angle>();
        /// <summary>
        /// List which holds the HISTORY of ABSOLUTE Toe. This absolute value is about the Horizontal
        /// </summary>
        public List<Angle> Final_Toe { get; set; } = new List<Angle>();
        /// <summary>
        /// Angle which is to be rotated to get the Toe which the user wants. 
        /// If Toe is the first change requested then the first value of the list will be the requested value. Then as the iterative loop runs this list will keep getting updated until the Requested Toe is ACTUALLY reached
        /// It can also contain the angle to be UNROTATED to get the requred Toe (In case changing something else inadvertantly changed the Toe)
        /// </summary>
        public List<Angle> AngleToBeRotatedForToe { get; set; } = new List<Angle>();
        /// <summary>
        /// List which will hold the history of delta of the Toe Link Length Change
        /// </summary>
        public List<double> Summ_dToeAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// This is the List which stores the History of the Toe Link DELTAS (Although there is static Toe Length, I am using delta in this list to maintain consistency with the <see cref="Final_CamberAdjusterLength"/> List).
        /// This would contain the ACTUAL Shim Link Length Change. 
        /// </summary>
        public List<double> Final_ToeAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// Not Used as of now 
        /// </summary>
        List<double> LinkLengthNeededForToe { get; set; } = new List<double>();

        /// <summary>
        /// List which holds the HISTORY of DELTAS of Caster. This delta  is about the Static Wheel Spindle Position whicih (in most cases) has a Static Caster
        /// </summary>
        public List<Angle> Summ_dCaster { get; set; } = new List<Angle>();
        /// <summary>
        /// List which holds the HISTORY of ABSOLUTE Caster. This absolute value is about the Horizontal
        /// </summary>
        public List<Angle> Final_Caster { get; set; } = new List<Angle>();
        /// <summary>
        /// Angle which is to be rotated to get the Caster which the user wants. 
        /// If Caster is the first change requested then the first value of the list will be the requested value. Then as the iterative loop runs this list will keep getting updated until the Requested Caster is ACTUALLY reached
        /// It can also contain the angle to be UNROTATED to get the requred Caster (In case changing something else inadvertantly changed the Caster)
        /// </summary>
        public List<Angle> AngleToBeRotatedForCaster { get; set; } = new List<Angle>();
        /// <summary>
        /// This is the List which stores the History of the Caster Adjuster deltas.This would contain the ACTUAL Adjuster Change. This List is as of now useless because the <see cref="Final_CasterAdjusterLength"/> List is going to hold the same data. I am maintaining it nonetheless
        /// to maintain consistency in the coding structure of this class
        /// </summary>
        public List<double> Summ_dCasterAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// This is the List which stores the History of the Adjuster DELTAS (Although there is static Wishbone Length, I am using delta in this list to maintain consistency with the <see cref="Final_CamberAdjusterLength"/> List).
        /// This would contain the ACTUAL Shim Link Length Change. 
        /// </summary>
        public List<double> Final_CasterAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// Not used 
        /// </summary>
        List<double> ShimLengthNeededForCaster { get; set; } = new List<double>();


        /// <summary>
        /// List which holds the HISTORY of DELTAS of KPI. This delta  is about the Static Wheel Spindle Position whicih (in most cases) has a Static KPI
        /// </summary>
        public List<Angle> Summ_dKPI { get; set; } = new List<Angle>();
        /// <summary>
        /// List which holds the HISTORY of ABSOLUTE KPI. This absolute value is about the Horizontal
        /// </summary>
        public List<Angle> Final_KPI { get; set; } = new List<Angle>();
        /// <summary>
        /// Angle which is to be rotated to get the KPI which the user wants. 
        /// If KPI is the first change requested then the first value of the list will be the requested value. Then as the iterative loop runs this list will keep getting updated until the Requested KPI is ACTUALLY reached
        /// It can also contain the angle to be UNROTATED to get the requred KPI (In case changing something else inadvertantly changed the KPI)
        /// </summary>
        public List<Angle> AngleToBeRotatedForKPI { get; set; } = new List<Angle>();
        /// <summary>
        /// This is the List which stores the History of the KPI Adjuster deltas.This would contain the ACTUAL Adjuster Change. This List is as of now useless because the <see cref="Final_KPIAdjusterLength"/> List is going to hold the same data. I am maintaining it nonetheless
        /// to maintain consistency in the coding structure of this class
        /// </summary>
        public List<double> Summ_dKPIAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// This is the List which stores the History of the Adjuster DELTAS (Although there is static Wishbone Length, I am using delta in this list to maintain consistency with the <see cref="Final_CamberAdjusterLength"/> List).
        /// This would contain the ACTUAL Shim Link Length Change. 
        /// </summary>
        public List<double> Final_KPIAdjusterLength { get; set; } = new List<double>();
        /// <summary>
        /// Not used 
        /// </summary>
        List<double> ShimLengthNeededForKPI { get; set; } = new List<double>();

        /// <summary>
        /// List which holds the HISTORY of DELTAS of Ride Height. This delta  is about the Static Wheel Spindle Position whicih (in most cases) has a Static Ride Height
        /// </summary>
        public List<double> Summ_RideHeight { get; set; } = new List<double>();
        /// <summary>
        /// List which holds the HISTORY of ABSOLUTE Ride Height. This absolute value is about the Horizontal
        /// </summary>
        public List<double> Final_RideHeight { get; set; } = new List<double>();

        public List<double> Final_TopFrontArm { get; set; } = new List<double>();

        public List<double> Final_TopRearArm { get; set; } = new List<double>();

        public List<double> Final_BottomFrontArm { get; set; } = new List<double>();

        public List<double> Final_BottomRearArm { get; set; } = new List<double>();

        public List<double> Final_Pushrod { get; set; } = new List<double>();









        /// <summary>
        /// Angle which is to be rotated to get the Ride Height which the user wants. 
        /// If Ride Height is the first change requested then the first value of the list will be the requested value. Then as the iterative loop runs this list will keep getting updated until the Requested Ride Height is ACTUALLY reached
        /// It can also contain the angle to be UNROTATED to get the requred Ride Height (In case changing something else inadvertantly changed the Ride Height)
        /// </summary>
        public List<double> HeightToBeChangedForRideHeight { get; set; } = new List<double>();
        /// <summary>
        /// List of all Rotations performed about the <see cref="SetupChangeDatabase.LBJToToeLink"/> axis
        /// </summary>
        public List<Angle> RotationsAbout_LBJToToeLinkAxis { get; set; } = new List<Angle>();
        /// <summary>
        /// List of all Rotations performed about the <see cref="SetupChangeDatabase.UBJToToeLink"/> axis
        /// </summary>
        public List<Angle> RotationsAbout_UBJToToeLinkAxis { get; set; } = new List<Angle>();
        /// <summary>
        /// List of all Rotations performed about the <see cref="SetupChangeDatabase.SteeringAxis"/> axis
        /// </summary>
        public List<Angle> RotationsAbout_SteeringAxis { get; set; } = new List<Angle>();
        /// <summary>
        /// Object of the <see cref="SolverMasterClass"/>
        /// </summary>
        public SolverMasterClass SMC { get; set; }
        /// <summary>
        /// List of the <see cref="OutputClass"/>
        /// </summary>
        public List<OutputClass> OC { get; set; }
        /// <summary>
        /// Object of the <see cref="SetupChange_CornerVariables"/> Class
        /// </summary>
        public SetupChange_CornerVariables SetupChange_CV_Base { get; set; } = new SetupChange_CornerVariables();
        /// <summary>
        /// Object of the <see cref="SetupChangeDatabase"/> CLass
        /// </summary>
        public SetupChangeDatabase SetupChange_DB { get; set; }
        /// <summary>
        /// ---Obselete---DIctionary which stores all current changes in Camber etxc
        /// </summary>
        public Dictionary<string, double> SetupChange_DB_Dictionary { get; set; } = new Dictionary<string, double>();

        public Angle FinalCamber { get; set; }

        public Angle FinalToe { get; set; }

        public Angle FinalCaster { get; set; }

        public Angle FinalKPI { get; set; }

        /// <summary>
        /// <see cref="Convergence"/> Enum's variable which contains information regarding whether the Camber has Converged or not
        /// </summary>
        public Convergence CamberConvergence = Convergence.NotRequested;
        /// <summary>
        /// <see cref="Convergence"/> Enum's variable which contains information regarding whether the Caster has Converged or not
        /// </summary>
        public Convergence CasterConvergence = Convergence.NotRequested;
        /// <summary>
        /// <see cref="Convergence"/> Enum's variable which contains information regarding whether the KPI has Converged or not
        /// </summary>
        public Convergence KPIConvergence = Convergence.NotRequested;
        /// <summary>
        /// <see cref="Convergence"/> Enum's variable which contains information regarding whether the Toe has Converged or not
        /// </summary>
        public Convergence ToeConvergence = Convergence.NotRequested;
        /// <summary>
        /// <see cref="Convergence"/> Enum's variable which contains information regarding whether the RH has Converged or not
        /// </summary>
        public Convergence RHConvergence = Convergence.NotRequested;
        /// <summary>
        /// <see cref="Convergence"/> Enum's variable which contains information regarding whether the Link Lengths has Converged or not
        /// </summary>
        public Convergence LinkLengthConvergence = Convergence.NotRequested;

        /// <summary>
        /// Multicast Delegate which will be used to decide the Methods to be invoked and their order
        /// </summary>
        private delegate void MethodInvocationOrderDecider();
        /// <summary>
        /// Delegate which will hold the pointer which points to the function of the Parent Change
        /// Example - If Camber is the starting point of the closed loop then the object of the <see cref="PrimaryClosedLoopMethod"/> delegate will point to <see cref="ClosedLoop_ChangeCamber(double)"/>
        /// </summary>
        private delegate void PrimaryClosedLoopMethod();

        /// <summary>
        /// Object of the <see cref="MethodInvocationOrderDecider"/> multicast delagte which will contain the list of pointers pointing to the methods which are to be invoked in the right order
        /// The is the object which will be used for the Closed Loop Operations inside the <see cref="SolveSetupChange"/>
        /// </summary>
        MethodInvocationOrderDecider DecideInvocationOrder;
        /// <summary>
        /// Object of the <see cref="MethodInvocationOrderDecider"/> which points to <see cref="ClosedLoop_ChangeCamber_Invoker"/> 
        /// </summary>
        MethodInvocationOrderDecider invokeCamberClosedLoop;
        /// <summary>
        /// Object of the <see cref="MethodInvocationOrderDecider"/> which points to <see cref="ClosedLoop_ChangeToe_Invoker"/>
        /// </summary>
        MethodInvocationOrderDecider invokeToeClosedLoop;
        /// <summary>
        /// Object of the <see cref="MethodInvocationOrderDecider"/> which points to <see cref="ClosedLoop_ChangeCaster_Invoker"/>
        /// </summary>
        MethodInvocationOrderDecider invokeCasterClosedLoop;
        /// <summary>
        /// Object of the <see cref="MethodInvocationOrderDecider"/> which points to the <see cref="ClosedLoop_ChangeKPI_Invoker"/>
        /// </summary>
        MethodInvocationOrderDecider invokeKPIClosedLoop;

        

        /// <summary>
        /// Object of the <see cref="MethodInvocationOrderDecider"/> which points to the <see cref="ClosedLoop_ChangeLinkLengths_Invoker"/>
        /// </summary>
        MethodInvocationOrderDecider invokeLinkLengthClosedLoop;
        /// <summary>
        /// Object of the <see cref="PrimaryClosedLoopMethod"/> which will point to the function of the Parent Change 
        /// Example - If Camber is the starting point of the closed loop then the object of the <see cref="PrimaryClosedLoopMethod"/> delegate will point to <see cref="ClosedLoop_ChangeCamber(double)"/>
        /// </summary>
        PrimaryClosedLoopMethod primaryClosedLoopMethod;

        /// <summary>
        /// Enumeration to decide which the Parent Change is. That is, the Entry point of the Setup Change Closed Loop Solver
        /// </summary>



        /// <summary>
        /// Overloaded constructor to <see cref="SolverMasterClass"/> and <see cref="OutputClass"/> and <see cref="SetupChange_CornerVariables"/> along with lists 
        /// </summary>
        /// <param name="_sMC"></param>
        /// <param name="_oC"></param>
        /// <param name="_setupChange_DB_Dictionary"></param>
        public SetupChange_ClosedLoopSolver(SolverMasterClass _sMC, List<OutputClass> _oC, ref Dictionary<string, double> _setupChange_DB_Dictionary, Angle _finalCamber, Angle _finalToe, Angle _finalCaster, Angle _finalKPI)
        {
            ///<summary>Assining the object of the <see cref="SolverMasterClass"/></summary>
            SMC = _sMC;
            
            ///<summary>Assining the object of the <see cref="OutputClass"/></summary>
            OC = _oC;
            
            ///<summary>Assining the object of the <see cref="SetupChange_CornerVariables"/></summary>
            SetupChange_CV_Base = new SetupChange_CornerVariables();
            SetupChange_CV_Base = OC[0].sccvOP;

            ///<summary>Assining the object of the <see cref="SetupChangeDatabase"/></summary>
            SetupChange_DB = SMC.SetupChange_DB_Master;
            ///<remarks>---OBSELETE---</remarks>
            SetupChange_DB_Dictionary = _setupChange_DB_Dictionary;

            ///<summary>Initializing all the Lists of this class which will be used for the Setup Change Verificating and History maintainance</summary>
            InitializeLists();

            ///<summary>Creating delegates which will be stored in a master Multicast Delegate. This master delegate will the Invocation list and will call the methods according to that lsit </summary>
            invokeCamberClosedLoop = new MethodInvocationOrderDecider(ClosedLoop_ChangeCamber_Invoker);
            invokeToeClosedLoop = new MethodInvocationOrderDecider(ClosedLoop_ChangeToe_Invoker);
            invokeCasterClosedLoop = new MethodInvocationOrderDecider(ClosedLoop_ChangeCaster_Invoker);
            invokeKPIClosedLoop = new MethodInvocationOrderDecider(ClosedLoop_ChangeKPI_Invoker);
            invokeLinkLengthClosedLoop = new MethodInvocationOrderDecider(ClosedLoop_ChangeLinkLengths_Invoker);
            ///<summary>Dumping all the above delegates into the Multicast delegate. This will be sorted eventually based on the entry point to the closed loop </summary>
            AssignMulticastDelegate();

            FinalCamber = _finalCamber;

            FinalToe = _finalToe;

            FinalCaster = _finalCaster;

            FinalKPI = _finalKPI;

        }

        /// <summary>
        /// Method to assign the starting value to all the Final Parameter Lists and the starting Angle of Rotations
        /// For Exampe, starting value assinged to <see cref="Final_Camber"/> and <see cref="AngleToBeRotatedForCamber"/>
        /// </summary>
        private void InitializeLists()
        {
            Final_Camber.Insert(0, new Angle(OC[0].waOP.StaticCamber, AngleUnit.Radians));
            AngleToBeRotatedForCamber.Add(new Angle(OC[0].sccvOP.deltaCamber, AngleUnit.Degrees));

            ///<summary>Assiningn the <see cref="Final_CamberAdjusterLength"/> List with 0 as the first value because unlike <see cref="Final_Camber"/> there is not static value of ShimLength to add to this </summary>
            Final_CamberAdjusterLength.Add(/*OC[0].sccvOP.deltaCamberShims * OC[0].sccvOP.camberShimThickness*/0);
            

            Final_Toe.Insert(0, new Angle(OC[0].waOP.StaticToe, AngleUnit.Radians));
            AngleToBeRotatedForToe.Add(new Angle(OC[0].sccvOP.deltaToe, AngleUnit.Degrees));

            ///< summary > Assiningn the < see cref = "Final_ToeAdjusterLength" /> List with 0 as the first value because unlike<see cref= "Final_Toe" /> there is not static value of ToeLinkLength to add to this </ summary >
            //Final_ToeAdjusterLength.Add(/*OC[0].sccvOP.deltaToeLinkLength*/0);


            Final_Caster.Insert(0, new Angle(OC[0].Caster, AngleUnit.Radians));
            AngleToBeRotatedForCaster.Add(new Angle(OC[0].sccvOP.deltaCaster, AngleUnit.Degrees));

            Final_KPI.Insert(0, new Angle(OC[0].KPI, AngleUnit.Radians));
            AngleToBeRotatedForKPI.Add(new Angle(OC[0].sccvOP.deltaKPI, AngleUnit.Degrees));

            Final_RideHeight.Insert(0, OC[0].sccvOP.deltaRideHeight);
            HeightToBeChangedForRideHeight.Add(OC[0].sccvOP.deltaRideHeight);

            Final_TopFrontArm.Add(SetupChange_DB.AdjOptions.TopFrontArm[SetupChange_DB.AdjOptions.TopFrontArm.Count-1].Length());

            Final_TopRearArm.Add(SetupChange_DB.AdjOptions.TopRearArm[SetupChange_DB.AdjOptions.TopRearArm.Count - 1].Length());

            Final_BottomFrontArm.Add(SetupChange_DB.AdjOptions.BottomFrontArm[SetupChange_DB.AdjOptions.BottomFrontArm.Count - 1].Length());

            Final_BottomRearArm.Add(SetupChange_DB.AdjOptions.BottomRearArm[SetupChange_DB.AdjOptions.BottomRearArm.Count - 1].Length());

            ///<remarks>Unline other Wishbone Length Lists, the Toe is used for both Direct Toe Angle Setting and also Toe Link length Setting. So I am initilaizing its first valur with the actual toe Link Length and when I add this first value with the last I will get the final Toe Link lenght</remarks>
            Final_ToeAdjusterLength.Add(SetupChange_DB.AdjOptions.MToeAdjusterLine[SetupChange_DB.AdjOptions.MToeAdjusterLine.Count - 1].Length());

            Final_Pushrod.Add(SetupChange_DB.AdjOptions.PushrodLine[SetupChange_DB.AdjOptions.PushrodLine.Count - 1].Length());

        }

        public void InitializeSpecificLists()
        {

        }

        public void DummyMethod() { }

        private void AssignMulticastDelegate()
        {
            DecideInvocationOrder = new MethodInvocationOrderDecider(DummyMethod);

            if (SetupChange_CV_Base.constKPI == true || SetupChange_CV_Base.KPIChangeRequested)
            {
                DecideInvocationOrder += invokeKPIClosedLoop;
            }
            if (SetupChange_CV_Base.constCaster == true || SetupChange_CV_Base.CasterChangeRequested)
            {
                DecideInvocationOrder += invokeCasterClosedLoop;
            }
            if (SetupChange_CV_Base.constCamber == true || SetupChange_CV_Base.CamberChangeRequested)
            {
                DecideInvocationOrder += invokeCamberClosedLoop;
            }
            if (SetupChange_CV_Base.constToe == true || SetupChange_CV_Base.ToeChangeRequested)
            {
                DecideInvocationOrder += invokeToeClosedLoop;
            }
            //if (SetupChange_CV_Base.constCaster == true || SetupChange_CV_Base.deltaCaster != 0 )
            //{
            //    DecideInvocationOrder += invokeCasterClosedLoop;
            //}
            //if (SetupChange_CV_Base.constKPI == true || SetupChange_CV_Base.deltaKPI != 0)
            //{
            //    DecideInvocationOrder += invokeKPIClosedLoop;
            //}
            if (SetupChange_CV_Base.deltaToeLinkLength != 0|| SetupChange_CV_Base.deltaTopFrontArm != 0 || SetupChange_CV_Base.deltaTopRearArm != 0 || SetupChange_CV_Base.deltaBottmFrontArm != 0 || SetupChange_CV_Base.deltaBottomRearArm != 0 || SetupChange_CV_Base.deltaPushrod!= 0 )
            {
                DecideInvocationOrder += invokeLinkLengthClosedLoop;
            }

        }

        /// <summary>
        /// Called ONLY once before the start of a closed Loop. Sort of an initializer method to teach this class which Requested Change is the Parent Change
        /// </summary>
        /// <param name="_currentChange">Enum which teaches this class which the parent change is </param>
        public void ClosedLoop_Solver(CurrentChange _currentChange)
        {
            //CurrentChange currentChange = _currentChange;

            if (_currentChange == CurrentChange.Camber)
            {
                primaryClosedLoopMethod = new PrimaryClosedLoopMethod(ClosedLoop_ChangeCamber_Invoker);
                if (DecideInvocationOrder.GetInvocationList().Contains(invokeCamberClosedLoop))
                {
                    DecideInvocationOrder -= invokeCamberClosedLoop;
                }
            }

            else if (_currentChange== CurrentChange.Toe)
            {
                primaryClosedLoopMethod = new PrimaryClosedLoopMethod(ClosedLoop_ChangeToe_Invoker);
                if (DecideInvocationOrder.GetInvocationList().Contains(invokeToeClosedLoop))
                {
                    DecideInvocationOrder -= invokeToeClosedLoop;
                }
            }
            else if (_currentChange == CurrentChange.KPI)
            {
                primaryClosedLoopMethod = new PrimaryClosedLoopMethod(ClosedLoop_ChangeKPI_Invoker);
                if (DecideInvocationOrder.GetInvocationList().Contains(invokeKPIClosedLoop))
                {
                    DecideInvocationOrder -= invokeKPIClosedLoop;
                }
            }
            else if (_currentChange == CurrentChange.Caster)
            {
                primaryClosedLoopMethod = new PrimaryClosedLoopMethod(ClosedLoop_ChangeCaster_Invoker);
                if (DecideInvocationOrder.GetInvocationList().Contains(invokeCasterClosedLoop))
                {
                    DecideInvocationOrder -= invokeCasterClosedLoop;
                }
            }
            //else if (_currentChange == CurrentChange.RideHeight)
            //{

            //}
            else if (_currentChange == CurrentChange.LinkLength || _currentChange == CurrentChange.RideHeight)
            {
                
                primaryClosedLoopMethod = new PrimaryClosedLoopMethod(ClosedLoop_ChangeLinkLengths_Invoker);
                if (DecideInvocationOrder.GetInvocationList().Contains(invokeLinkLengthClosedLoop))
                {
                    DecideInvocationOrder -= invokeLinkLengthClosedLoop;
                }
            }
            
            if (DecideInvocationOrder != null)
            {
                SolveSetupChange();
            }
        }

        public int NoOfClosedLoopiterations { get; set; } = 100;

        /// <summary>
        /// 
        /// </summary>
        private void SolveSetupChange()
        {
            for (int i = 0; i <= NoOfClosedLoopiterations; i++)  
            {
                DecideInvocationOrder();

                if (ClosedLoop_AllConditionsCheck(0.0009, 0.0009, 0.0009, 0.0009, 1, i))
                {
                    break;
                }
                else
                {
                    if (i != NoOfClosedLoopiterations) 
                    {
                        primaryClosedLoopMethod(); 
                    }
                }
            }

        }

        private void ClosedLoop_ChangeCamber_Invoker()
        {
            //Start:
            //if (ClosedLoop_CamberCheck(0.0009))
            //{
            //    return;
            //}
            //else
            //{
                ///<summary>Invoking the <see cref="ClosedLoop_ChangeCamber(double)"/> method </summary>
                if (SetupChange_CV_Base.camberAdjustmentType == AdjustmentType.Direct)
                {
                    ClosedLoop_ChangeCamber(AngleToBeRotatedForCamber[AngleToBeRotatedForCamber.Count - 1].Degrees, SetupChange_DB.AdjOptions.MCamberAdjusterLine, SetupChange_DB.AdjOptions.MCamberAdjusterVector, 0); 
                }
                else
                {
                    //SetupChange_DB.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(SetupChange_DB.AdjOptions.MCamberAdjusterLine, SetupChange_DB.AdjOptions.MCamberAdjusterVector, ((OC[0].sccvOP.deltaCamberShims * OC[0].sccvOP.camberShimThickness) - Final_CamberAdjusterLength[Final_CamberAdjusterLength.Count - 1]));
                    Angle angleTobeRotated = SMC.SetupChange_CamberShims_OR_ShimsVectorLengthChanged((OC[0].sccvOP.deltaCamberShims * OC[0].sccvOP.camberShimThickness) - Final_CamberAdjusterLength[Final_CamberAdjusterLength.Count - 1], SetupChange_DB.UBJ, SetupChange_DB.LBJ,
                                                                                                      SetupChange_CV_Base.camberAdjustmentTool);
                    ClosedLoop_ChangeCamber(angleTobeRotated.Degrees, SetupChange_DB.AdjOptions.MCamberAdjusterLine, SetupChange_DB.AdjOptions.MCamberAdjusterVector, SetupChange_DB.AdjOptions.UprightIndexForCamber);
                }
            //    goto Start;
            //}
        }

        private void ClosedLoop_ChangeCamber(double _dCamber_New, List<Line> shimsLine, List<Vector3D> shimsVector, int _uprightVertexIndex)
        {

            SMC.SetupChange_CamberChange(_dCamber_New, OC[0], true, SetupChange_CV_Base, shimsLine, shimsVector, SetupChange_DB.AdjOptions.AxisRotation_Camber[SetupChange_DB.AdjOptions.AxisRotation_Camber.Count - 1], _uprightVertexIndex);
        }

        private void ClosedLoop_ChangeToe_Invoker()
        {
            //Start:
            //if (ClosedLoop_ToeCheck(0.0009)) 
            //{
            //    return;
            //}
            //else
            //{
                ///<remarks>Invoking the <see cref="ClosedLoop_ChangeToe(double)"/> method </remarks>
                if (SetupChange_CV_Base.toeAdjustmentType == AdjustmentType.Direct) 
                {
                    ClosedLoop_ChangeToe(/*-*/AngleToBeRotatedForToe[AngleToBeRotatedForToe.Count - 1].Degrees); 
                }
                else
                {
                    //SetupChange_DB.AdjOptions.NoOfShims_OR_ShimsVectorLengthChanged(SetupChange_DB.AdjOptions.MToeAdjusterLine, SetupChange_DB.AdjOptions.MToeAdjusterVector, OC[0].sccvOP.deltaToeLinkLength - Final_ToeAdjusterLength[Final_ToeAdjusterLength.Count - 1]);
                    //Angle angleToBeRotated = SetupChange_DB.AdjOptions.GetNewUprightTrianglePosition(ref SetupChange_DB.UprightTriangle, SetupChange_DB.UBJ, SetupChange_DB.LBJ, SetupChange_DB.AdjOptions.MToeAdjusterLine[SetupChange_DB.AdjOptions.MToeAdjusterLine.Count - 1].EndPoint, SetupChange_DB);
                    Angle angleToBeRotated = SMC.SetupChange_ToeLinkLengthChanged(OC[0].sccvOP.deltaToeLinkLength - Final_ToeAdjusterLength[Final_ToeAdjusterLength.Count - 1]);
                    ClosedLoop_ChangeToe(angleToBeRotated.Degrees);

                }
            //    goto Start;
            //}
        }

        private void ClosedLoop_ChangeToe(double _dToe_New)
        {
            //SetupChange_DB.AdjOptions.AxisRotation_Toe = SetupChange_DB.SteeringAxis.Line.DeltaLine;

            SMC.SetupChange_ToeChange(_dToe_New, OC[0], true, SetupChange_CV_Base, SetupChange_DB.AdjOptions.MToeAdjusterLine, SetupChange_DB.AdjOptions.MToeAdjusterVector, SetupChange_DB.AdjOptions.AxisRotation_Toe[SetupChange_DB.AdjOptions.AxisRotation_Toe.Count - 1], 2);
        }


        private void ClosedLoop_ChangeCaster_Invoker()
        {
            //Start:
            //if (ClosedLoop_CasterCheck(0.0009))
            //{
            //    return;
            //}
            //else
            //{
                ///<remarks>Invoking the <see cref="ClosedLoop_ChangeCaster(double)"/> method</remarks>
                ClosedLoop_ChangeCaster(/*-*/AngleToBeRotatedForCaster[AngleToBeRotatedForCaster.Count - 1].Degrees);
            //    goto Start;
            //}

        }

        private void ClosedLoop_ChangeCaster(double _dCaster_New)
        {
            SMC.SetupChange_CasterChange(_dCaster_New, OC[0], true, SetupChange_CV_Base, SetupChange_DB.AdjOptions.MCasterAdjustmenterLine, SetupChange_DB.AdjOptions.MCasterAdjusterVector, SetupChange_DB.AdjOptions.AxisRotation_Caster[SetupChange_DB.AdjOptions.AxisRotation_Caster.Count - 1], 
                                         SetupChange_DB.AdjOptions.UprightIndexForCaster);
        }


        private void ClosedLoop_ChangeKPI_Invoker()
        {
            //Start:
            //if (ClosedLoop_KPICheck(0.0009))
            //{
            //    return;
            //}
            //else
            //{
                ///<summary>Invoking the <see cref="ClosedLoop_ChangeKPI(double)"/> method</summary>
                ClosedLoop_ChangeKPI(AngleToBeRotatedForKPI[AngleToBeRotatedForKPI.Count - 1].Degrees);
            //    goto Start;
            //}

        }

        private void ClosedLoop_ChangeKPI(double _dKPI_New)
        {
            SMC.SetupChange_KPIChange(_dKPI_New, OC[0], true, SetupChange_CV_Base, SetupChange_DB.AdjOptions.MKPIAdjusterLine, SetupChange_DB.AdjOptions.MKPIAdjusterVector, SetupChange_DB.AdjOptions.AxisRotation_KPI[SetupChange_DB.AdjOptions.AxisRotation_KPI.Count - 1] ,
                                      SetupChange_DB.AdjOptions.UprightIndexForKPI);
        }


        /// <summary>
        /// Method to Call all the <see cref="ClosedLoop_LinkLengthCheck(double, double, double)"/> method for each of the 6 Link Lengths. If the Requested Change is 0 or if the change is achieved it will be caught by the <see cref="ClosedLoop_LinkLengthCheck(double, double, double)"/> and terminate
        /// </summary>
        private void ClosedLoop_ChangeLinkLengths_Invoker()
        {
            ///<summary>TopFrontArm </summary>
            ClosedLoop_ChangeLinkLengths(OC[0].sccvOP.deltaTopFrontArm, SetupChange_DB.AdjOptions.TopFrontArm, SetupChange_DB.AdjOptions.TopFrontVector, AdjustmentTools.TopFrontArm, Final_TopFrontArm);

            ///<summary>TopRearArm </summary>
            ClosedLoop_ChangeLinkLengths(OC[0].sccvOP.deltaTopRearArm, SetupChange_DB.AdjOptions.TopRearArm, SetupChange_DB.AdjOptions.TopRearVector, AdjustmentTools.TopRearArm, Final_TopRearArm);

            ///<summary>BottomFrontArm </summary>
            ClosedLoop_ChangeLinkLengths(OC[0].sccvOP.deltaBottmFrontArm, SetupChange_DB.AdjOptions.BottomFrontArm, SetupChange_DB.AdjOptions.BottomFrontArmVector, AdjustmentTools.BottomFrontArm, Final_BottomFrontArm);

            ///<summary>BottomRearArm </summary>
            ClosedLoop_ChangeLinkLengths(OC[0].sccvOP.deltaBottomRearArm, SetupChange_DB.AdjOptions.BottomRearArm, SetupChange_DB.AdjOptions.BottomRearArmVector, AdjustmentTools.BottomRearArm, Final_BottomRearArm);

            ///<summary>Pushrod</summary>
            ClosedLoop_ChangeLinkLengths(OC[0].sccvOP.deltaPushrod, SetupChange_DB.AdjOptions.PushrodLine, SetupChange_DB.AdjOptions.PushrodVector, AdjustmentTools.PushrodLength, Final_Pushrod);

            ///<summary>ToeLink</summary>
            ClosedLoop_ChangeLinkLengths(OC[0].sccvOP.deltaToeLinkLength, SetupChange_DB.AdjOptions.MToeAdjusterLine, SetupChange_DB.AdjOptions.MToeAdjusterVector, AdjustmentTools.ToeLinkLength, Final_ToeAdjusterLength);
        }

        /// <summary>
        /// Method to perform the Link Length Change Operations of Link Length change and Wheel Assembly Rotate which are methods in the <see cref="SolverMasterClass"/>
        /// </summary>
        /// <param name="_deltaLinkLength">Change in Link Length of the Concerned Link requested by the user</param>
        /// <param name="_linkLine">List of the Link Line in the <see cref="AdjustmentOptions"/> Class</param>
        /// <param name="_linkVector">List of the Link Vectors in the <see cref="AdjustmentOptions"/> Class</param>
        /// <param name="_adjTool">Object of the <see cref="AdjustmentTools"/> Enum</param>
        /// <param name="_finalLinkLengthList"> List which holds the history of the deltas of the Concerned Link Length. Ex - <see cref="Final_TopFrontArm"/></param>
        private void ClosedLoop_ChangeLinkLengths(double _deltaLinkLength, List<Line> _linkLine, List<Vector3D> _linkVector, AdjustmentTools _adjTool,List<double> _finalLinkLengthList)
        {
            Start:

            if (_finalLinkLengthList.Count == 0)
            {
                return;
            }

            if (ClosedLoop_LinkLengthCheck(_deltaLinkLength, _finalLinkLengthList[_finalLinkLengthList.Count - 1], 1))
            {
                return;
            }
            else
            {
                /////<summary>
                /////Finding the angle to be rotated by the Upright because of the Wishbone length Change. The 3 points of the Triangle are passed as it is. 
                /////The will worked upon inside the <see cref="SolverMasterClass.SetupChange_WishboneLengthChanged(double, List{Line}, List{Vector3D}, Point3D, Point3D, Point3D, AdjustmentTools)"/> method
                /////</summary>
                //Angle AngleToBeRotated = SMC.SetupChange_WishboneLengthChanged(_deltaLinkLength, _linkLine, _linkVector,
                //                                                           SetupChange_DB.UBJ, SetupChange_DB.LBJ, SetupChange_DB.ToeLinkUpright, _adjTool);
                /////<summary>Link Length Changed Operations</summary>
                //SMC.SetupChange_LinkLengthChange(AngleToBeRotated.Degrees, _deltaLinkLength, OC[0], SetupChange_DB.LBJToToeLink.Line.DeltaLine[SetupChange_DB.LBJToToeLink.Line.DeltaLine.Count - 1],
                //                                                  _linkLine, _linkVector, _finalLinkLengthList, 0);
                //goto Start;
            }
        }






        private bool ClosedLoop_AllConditionsCheck(double camberTol, double toeTol, double casterTol, double kpiTol, double linkLengthtol, int iterationsDone)
        {

            //if (ClosedLoop_CamberCheck(camberTol) && ClosedLoop_ToeCheck(toeTol) && ClosedLoop_CasterCheck(casterTol) && ClosedLoop_KPICheck(kpiTol) && ClosedLoop_LinkLengthCheck_Invoker(linkLengthtol)) 
            //{
            //    return true;
            //}
            bool OverallSuccessfull = true;

            if ((SetupChange_CV_Base.constCamber || SetupChange_CV_Base.CamberChangeRequested)&& !ClosedLoop_CamberCheck(camberTol, iterationsDone)) 
            {
                OverallSuccessfull = false;
            }
            if ((SetupChange_CV_Base.constToe || SetupChange_CV_Base.ToeChangeRequested)&& !ClosedLoop_ToeCheck(toeTol, iterationsDone))
            {
                //return false;
                OverallSuccessfull = false;
            }
            if ((SetupChange_CV_Base.constCaster || SetupChange_CV_Base.CasterChangeRequested) && !ClosedLoop_CasterCheck(casterTol, iterationsDone))
            {
                //return false;
                OverallSuccessfull = false;
            }
            if ((SetupChange_CV_Base.constKPI || SetupChange_CV_Base.KPIChangeRequested)&& !ClosedLoop_KPICheck(kpiTol, iterationsDone))
            {
                //return false;
                OverallSuccessfull = false;
            }
            if (!ClosedLoop_LinkLengthCheck_Invoker(linkLengthtol, iterationsDone))
            {
                //return false;
                OverallSuccessfull = false;
            }

            if (OverallSuccessfull == true)
            {
                return true;
            }
            else return false;

        }

        private bool ClosedLoop_CamberCheck(double _camberTol, int _iterationsDone)
        {
            if (OC[0].sccvOP.camberAdjustmentType == AdjustmentType.Direct)
            {
                if (Math.Abs(/*OC[0].waOP.StaticCamber*/FinalCamber.Radians - Final_Camber[Final_Camber.Count - 1].Radians) < _camberTol)
                {
                    CamberConvergence = Convergence.Successful;
                    return true;
                }
                else
                {
                    //CamberConvergence = Convergence.InProgress;

                    if (_iterationsDone == NoOfClosedLoopiterations)
                    {
                        CamberConvergence = Convergence.UnSuccessful;
                    }

                    return false;
                }
            }
            else
            {
                if ((Math.Abs(OC[0].sccvOP.deltaCamberShims * OC[0].sccvOP.camberShimThickness) - Final_CamberAdjusterLength[Final_CamberAdjusterLength.Count - 1]) < _camberTol) 
                {
                    CamberConvergence = Convergence.Successful;
                    return true;
                }
                else
                {
                    //CamberConvergence = Convergence.InProgress;

                    if (_iterationsDone == NoOfClosedLoopiterations)
                    {
                        CamberConvergence = Convergence.UnSuccessful;
                    }

                    return false;
                }
            }
        }
        private bool ClosedLoop_ToeCheck(double _toeTol, int _iterationsDone)
        {
            if (OC[0].sccvOP.toeAdjustmentType == AdjustmentType.Direct) 
            {
                if (Math.Abs(/*OC[0].waOP.StaticToe*/FinalToe.Radians - Final_Toe[Final_Toe.Count - 1].Radians) < _toeTol)
                {
                    ToeConvergence = Convergence.Successful;
                    return true;
                }
                else
                {
                    //ToeConvergence = Convergence.InProgress;

                    if (_iterationsDone == NoOfClosedLoopiterations)
                    {
                        ToeConvergence = Convergence.UnSuccessful;
                    }

                    return false;
                }
            }
            else
            {
                if (Math.Abs(OC[0].sccvOP.deltaToeLinkLength - Final_ToeAdjusterLength[Final_ToeAdjusterLength.Count - 1]) < 0.05)
                {
                    ToeConvergence = Convergence.Successful;
                    return true;
                }
                else
                {
                    //ToeConvergence = Convergence.InProgress;

                    if (_iterationsDone == NoOfClosedLoopiterations)
                    {
                        ToeConvergence = Convergence.UnSuccessful;
                    }

                    return false;
                }
            }
            
        }
        private bool ClosedLoop_CasterCheck(double _casterTol, int _iterationsDone)
        {
            if (Math.Abs(/*OC[0].Caster*/FinalCaster.Radians - Final_Caster[Final_Caster.Count - 1].Radians) < _casterTol)
            {
                CasterConvergence = Convergence.Successful;
                return true;
            }
            else
            {
                //CasterConvergence = Convergence.InProgress;

                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    CasterConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
        }
        private bool ClosedLoop_KPICheck(double _kpiTol, int _iterationsDone)
        {
            if (Math.Abs(/*OC[0].KPI*/FinalKPI.Radians - Final_KPI[Final_KPI.Count - 1].Radians) < _kpiTol)
            {
                KPIConvergence = Convergence.Successful;
                return true;
            }
            else
            {
                //KPIConvergence = Convergence.InProgress;

                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    KPIConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
        }

        private bool ClosedLoop_RideHeightCheck()
        {
            return true;
        }

        private bool ClosedLoop_LinkLengthCheck_Invoker(double _linkLengthTol, int _iterationsDone)
        {
            if (OC[0].sccvOP.deltaTopFrontArm != 0 && !ClosedLoop_LinkLengthCheck(Final_TopFrontArm[Final_TopFrontArm.Count - 1], OC[0].sccvOP.deltaTopFrontArm, _linkLengthTol)) 
            {
                //LinkLengthConvergence = Convergence.InProgress;
                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    LinkLengthConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
            else if (OC[0].sccvOP.deltaTopRearArm != 0 && !ClosedLoop_LinkLengthCheck(Final_TopRearArm[Final_TopRearArm.Count - 1], OC[0].sccvOP.deltaTopRearArm, _linkLengthTol)) 
            {
                //LinkLengthConvergence = Convergence.InProgress;
                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    LinkLengthConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
            else if (OC[0].sccvOP.deltaBottmFrontArm != 0 && !ClosedLoop_LinkLengthCheck(Final_BottomFrontArm[Final_BottomFrontArm.Count - 1], OC[0].sccvOP.deltaBottmFrontArm, _linkLengthTol)) 
            {
                //LinkLengthConvergence = Convergence.InProgress;
                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    LinkLengthConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
            else if (OC[0].sccvOP.deltaBottomRearArm != 0 && !ClosedLoop_LinkLengthCheck(Final_BottomRearArm[Final_BottomRearArm.Count - 1], OC[0].sccvOP.deltaBottomRearArm, _linkLengthTol)) 
            {
                //LinkLengthConvergence = Convergence.InProgress;
                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    LinkLengthConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
            else if (OC[0].sccvOP.deltaPushrod != 0 && !ClosedLoop_LinkLengthCheck(Final_Pushrod[Final_Pushrod.Count - 1], OC[0].sccvOP.deltaPushrod, _linkLengthTol)) 
            {
                //LinkLengthConvergence = Convergence.InProgress;
                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    LinkLengthConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
            else if (OC[0].sccvOP.deltaToeLinkLength != 0 && !ClosedLoop_LinkLengthCheck(Final_ToeAdjusterLength[Final_ToeAdjusterLength.Count - 1], OC[0].sccvOP.deltaToeLinkLength, _linkLengthTol)) 
            {
                //LinkLengthConvergence = Convergence.InProgress;
                if (_iterationsDone == NoOfClosedLoopiterations)
                {
                    LinkLengthConvergence = Convergence.UnSuccessful;
                }
                return false;
            }
            else
            {
                LinkLengthConvergence = Convergence.Successful;
                return true;
            }
        }
        
        private bool ClosedLoop_LinkLengthCheck(double acutalLength, double requiredLength, double tol)
        {
            if (Math.Abs(requiredLength - acutalLength) < tol)
            {
                return true;
            }
            else return false;
        }





    }

    /// <summary>
    /// Enumeration to determine which the Current Change is. This will be used by methods to determine which SetupChange method 
    /// (such as <see cref="SolverMasterClass.SetupChange_CamberChange(double, OutputClass, int, int, bool, AdjustmentType)"/> OR <see cref="SetupChange_ClosedLoopSolver.ClosedLoop_ChangeCamber_Invoker"/>) is calling it. 
    /// </summary>
    public enum CurrentChange
    {
        Camber,
        Toe,
        Caster,
        KPI,
        RideHeight,
        LinkLength
    };

    public enum Convergence
    {
        NotRequested,
        Successful,
        InProgress,
        UnSuccessful
    };

}

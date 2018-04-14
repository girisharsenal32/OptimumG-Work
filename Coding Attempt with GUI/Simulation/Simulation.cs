using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraNavBar;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    public class Simulation : ISerializable
    {
        #region Declrarations
        public string SimulationName;
        public static int SimulationCounter = 0;
        //public static int CurrentID = 0;
        //public int SimulationID = 0;
        //public int NoOfSteps = 200;
        public static List<Simulation> List_Simulation = new List<Simulation>();
        public SimulationPanel simulationPanel = new SimulationPanel();
        public Vehicle Simulation_Vehicle = new Vehicle();
        public Motion Simulation_Motion = new Motion();
        public LoadCase Simulation_LoadCase = new LoadCase();
        public SetupChange Simulation_SetupChange = new SetupChange();
        public bool Simulation_MotionExists;
        public CusNavBarItem navBarItemSimulation;
        public Kinematics_Software_New r1; 
        #endregion

        #region Constructor
        public Simulation(string _simulationName, int _simulationID, Kinematics_Software_New _r1)
        {
            r1 = _r1;

            SimulationName = _simulationName;
            navBarItemSimulation = new CusNavBarItem(_simulationName, _simulationID, this);
        }
        #endregion

        public void HandleGUI(NavBarGroup _navBarGroup, NavBarControl _navBarControl, Kinematics_Software_New _r1, int Index)
        {
            Simulation.List_Simulation[Index].simulationPanel.FindSimulationID(Index);

            Simulation.List_Simulation[Index].navBarItemSimulation.CreateNavBarItem(Simulation.List_Simulation[Index].navBarItemSimulation, _navBarGroup, _navBarControl/*, Index, "Simulation", Simulation.List_Simulation[Index]*/);
            Simulation.List_Simulation[Index].navBarItemSimulation = Simulation.List_Simulation[Index].LinkClickedEventCreater(Simulation.List_Simulation[Index].navBarItemSimulation, _r1);

            Simulation.List_Simulation[Index].simulationPanel.Show();
        }

        #region NavBarItem Event Operations

        #region NavBarItem LinkClicked Event Creater
        public CusNavBarItem LinkClickedEventCreater(CusNavBarItem _navBarItem, Kinematics_Software_New _r1)
        {
            r1 = _r1;
            _navBarItem.LinkClicked += _navBarItem_LinkClicked;
            return _navBarItem;
        }
        #endregion

        #region NvaBarItem Link Clicked Event
        private void _navBarItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            simulationPanel.FindSimulationID(r1.navBarGroupSimulation.SelectedLinkIndex);

            for (int i_HideAllPanels = 0; i_HideAllPanels < Simulation.List_Simulation.Count; i_HideAllPanels++)
            {
                List_Simulation[i_HideAllPanels].simulationPanel.Hide();
            }

            ///<remarks>
            ///The below IF loop is necessary to safeguard the software if a simulation is created before the Vehicle is created. In this situation, there will be a Vehicle Object within the simulation (because <c>public Vehicle Simulation_Vehicle = new Vehicle();</c> is the 
            ///way vehicle object is declared. But the <c>sc_FL</c> object of the Vehicle wil be null. 
            /// </remarks>
            if (List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].Simulation_Vehicle.sc_FL != null)
            {
                if (!List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].Simulation_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].simulationPanel.groupControlSelectMotion.Hide();
                    List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].simulationPanel.comboBoxMotion.Hide();
                }
                else if (List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].Simulation_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].simulationPanel.groupControlSelectMotion.Show();
                    List_Simulation[r1.navBarGroupSimulation.SelectedLinkIndex].simulationPanel.comboBoxMotion.Show();
                } 
            }

            simulationPanel.Show();
            simulationPanel.BringToFront();
        }
        #endregion

        #endregion

        #region De-serialization of the Simulation Class
        public Simulation(SerializationInfo info, StreamingContext context)
        {
            SimulationName = (string)info.GetValue("SimulationName", typeof(string));
            SimulationCounter = (int)info.GetValue("SimulationCounter", typeof(int));

            List_Simulation = (List<Simulation>)info.GetValue("List_Simulation", typeof(List<Simulation>));
            Simulation_Vehicle = null;
            Simulation_Vehicle = (Vehicle)info.GetValue("Simulation_Vehicle", typeof(Vehicle));
            Simulation_Motion = (Motion)info.GetValue("Simulation_Motion", typeof(Motion));
            Simulation_LoadCase = (LoadCase)info.GetValue("Simulation_LoadCase", typeof(LoadCase));

            Simulation_MotionExists = (bool)info.GetValue("Simulation_MotionExists", typeof(bool));

            navBarItemSimulation = (CusNavBarItem)info.GetValue("navBarItemSimulation", typeof(CusNavBarItem));

        }
        #endregion

        #region Serialization of the Simulation Class
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SimulationName", SimulationName);
            info.AddValue("SimulationCounter", SimulationCounter);

            info.AddValue("List_Simulation", List_Simulation);
            info.AddValue("Simulation_Vehicle", Simulation_Vehicle);
            info.AddValue("Simulation_Motion", Simulation_Motion);
            info.AddValue("Simulation_LoadCase", Simulation_LoadCase);

            info.AddValue("Simulation_MotionExists", Simulation_MotionExists);

            info.AddValue("navBarItemSimulation", navBarItemSimulation);

        } 
        #endregion
    }
}

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

namespace Coding_Attempt_with_GUI
{
    public partial class SimulationPanel : XtraForm
    {
        int SimulationID;
        public SimulationPanel()
        {
            InitializeComponent();
        }

        public void FindSimulationID(int _simulationID)
        {
            SimulationID = _simulationID;
            this.Text = "Simulation Panel - " + (_simulationID + 1) ;
        }

        public void AssignSimulationObjects()
        {
            Simulation.List_Simulation[SimulationID].Simulation_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;

            if (Simulation.List_Simulation[SimulationID].Simulation_Vehicle.sc_FL != null)
            {
                if (Simulation.List_Simulation[SimulationID].Simulation_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    if (comboBoxMotion.SelectedItem != null)
                    {
                        Simulation.List_Simulation[SimulationID].Simulation_Motion = (Motion)comboBoxMotion.SelectedItem;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Motion Item");
                    }
                }
                else if (!Simulation.List_Simulation[SimulationID].Simulation_Vehicle.sc_FL.SuspensionMotionExists)
                {
                    if (comboBoxSetupChange.SelectedItem != null)
                    {
                        Simulation.List_Simulation[SimulationID].Simulation_SetupChange = (SetupChange)comboBoxSetupChange.SelectedItem;
                        this.Hide();
                    }
                } 
            }

            if (comboBoxLoadCase.SelectedItem != null)
            {
                Simulation.List_Simulation[SimulationID].Simulation_LoadCase = (LoadCase)comboBoxLoadCase.SelectedItem;
                this.Hide();
            }
            else
            {
                this.Hide();
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxVehicle.SelectedItem != null)
            {
                AssignSimulationObjects();
            }

            else
            {
                MessageBox.Show("Please Select a Vehicle Item");
            }


        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBoxVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxVehicle.SelectedItem != null)
            {
                Simulation.List_Simulation[SimulationID].Simulation_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;

                if (Simulation.List_Simulation[SimulationID].Simulation_Vehicle.sc_FL != null)
                {
                    if (!Simulation.List_Simulation[SimulationID].Simulation_Vehicle.sc_FL.SuspensionMotionExists)
                    {

                        groupControlSelectMotion.Hide();
                        groupControlSetupChange.Show();
                        comboBoxMotion.Hide();
                    }
                    else
                    {
                        Simulation.List_Simulation[SimulationID].Simulation_Motion = (Motion)comboBoxMotion.SelectedItem;
                        groupControlSelectMotion.Show();
                        comboBoxMotion.Show();
                        groupControlSetupChange.Hide();
                    } 
                } 
            }

        }

        private void comboBoxMotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Simulation.List_Simulation[SimulationID].Simulation_Vehicle = (Vehicle)comboBoxVehicle.SelectedItem;
            //Simulation.List_Simulation[SimulationID].Simulation_Motion = (Motion)comboBoxMotion.SelectedItem;
        }

        private void SimulationPanel_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxSetupChange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
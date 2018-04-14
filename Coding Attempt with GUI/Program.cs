using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Coding_Attempt_with_GUI
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Kinematics_Software_New R1 = new Kinematics_Software_New();
            //OpenTKInputForm OTKi = new OpenTKInputForm();
            Application.Run(R1);
 

        }

    }
}
 
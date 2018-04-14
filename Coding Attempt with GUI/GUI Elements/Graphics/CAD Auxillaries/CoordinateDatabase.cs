using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;


namespace Coding_Attempt_with_GUI
{
    /// <summary>
    /// This class houses Dictionaries for the Inboard Pick Up Points, Outboard Pick Up Points and Suspension Links
    /// </summary>
    public class CoordinateDatabase
    {

        /// <summary>
        /// Dictionary of Inboard Pick Up Points
        /// </summary>
        public Dictionary<string, Joint> InboardPickUp = new Dictionary<string, Joint>();
        /// <summary>
        /// Dictionary of Outboard Pick up Points
        /// </summary>
        public Dictionary<string, Joint> OutboardPickUp = new Dictionary<string, Joint>();
        /// <summary>
        /// Dictionary of Suspension Links 
        /// </summary>
        public Dictionary<string, Bar> SuspensionLinks = new Dictionary<string, Bar>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using devDept.Eyeshot.Translators;

/// <summary>
/// This class will be used for 2 purposes 
/// -> Any computations that need to be done as per the user's requirement.
/// -> To store the settings/variables that the user sets using the ImportCADForm.
/// </summary>
namespace Coding_Attempt_with_GUI
{
    [Serializable()]
    
    public class ImportCAD : ISerializable
    {

        /// <summary>
        /// Index which will identify which Object from the List of Front Left Suspension Coordinate should be used
        /// </summary>
        int indexFL = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Front Right Suspension Coordinate should be used
        /// </summary>
        int indexFR = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Rear Left Suspension Coordinate should be used
        /// </summary>
        int indexRL = 0;
        /// <summary>
        /// Index which will identify which Object from the List of Rear Right Suspension Coordinate should be used
        /// </summary>
        int indexRR = 0;
        /// <summary>
        /// Boolean to determine if a Suspension Object is created
        /// </summary>
        public bool SuspensionIsCreated;
        /// <summary>
        /// Boolean to determine if a Wheel Alignment Object is created
        /// </summary>
        public bool WAIsCreated;
        /// <summary>
        /// Boolean to detmine if the user wants to use the created Suspension
        /// </summary>
        public bool UseCreatedSuspnsion;
        /// <summary>
        /// Boolean to determine if the user wants to use the created Wheel Alignment
        /// </summary>
        public bool UseCreatedWA;
        /// <summary>
        /// Boolean to determine if the user has imported the File or not
        /// </summary>
        public bool FileHasBeenImported;
        /// <summary>
        /// Boolean to determine if the Wheel is to be Plotted. This value is determined based on whether the user is importing the N.S.M
        /// </summary>
        public bool PlotWheel = true;
        /// <summary>
        /// Object of the ReadIGES Class
        /// </summary>
        public ReadFileAsync importedFile;
        /// <summary>
        /// Object of the Main Form
        /// </summary>
        Kinematics_Software_New R1 = Kinematics_Software_New.AssignFormVariable();
        /// <summary>
        /// Index of the VehicleGUI item of which this is a member
        /// </summary>
        public int index_VehicleGUI = 0;



        /// <summary>
        /// Method to obtain all the settings and initialize all the variables using the ImportCADForm
        /// </summary>
        /// <param name="_importCADForm">Object of the ImportCADForm</param>
        public void GetImportSettings(ImportCADForm _importCADForm)
        {
            ///<summary>Initialzing the index of List of the Front Left SuspensionCoordinate Objects</summary>
            indexFL = _importCADForm.indexSusFL_Form;
            ///<summary>Initialzing the index of List of the Front Right SuspensionCoordinate Objects</summary>
            indexFR = _importCADForm.indexSusFR_Form;
            ///<summary>Initialzing the index of List of the Rear Left SuspensionCoordinate Objects</summary>
            indexRL = _importCADForm.indexSusRL_Form;
            ///<summary>Initialzing the index of List of the Rear Right SuspensionCoordinate Objects</summary>
            indexRR = _importCADForm.indexSusRR_Form;
            ///<summary>Initialzing the index of List of VehicleGUI Objects</summary>
            index_VehicleGUI = _importCADForm.index_VehicleGUI_Form;

            ///<summary>Initializing the Boolean Variables</summary>
            SuspensionIsCreated = _importCADForm.SuspensionIsCreated_Form;
            WAIsCreated = _importCADForm.WAIsCreated_Form;
            UseCreatedSuspnsion = _importCADForm.UseCreatedSuspnsion_Form;
            UseCreatedWA = _importCADForm.UseCreatedWA_Form;
            PlotWheel = _importCADForm.PlotWheel_Form;
            FileHasBeenImported = _importCADForm.FileHasBeenImported_Form;

            ///<summary>Assigning the Imported IGES File</summary>
            importedFile = _importCADForm.importedFile_Form;


        }
        


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}

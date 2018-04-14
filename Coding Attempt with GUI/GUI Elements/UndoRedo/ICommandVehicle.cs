using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding_Attempt_with_GUI
{
    public interface ICommandVehicle
    {
        void ModifyVehicle(int l_modify,SuspensionCoordinatesMaster[] Assy_SCM,Tire[] Assy_Tire,Spring[] Assy_Spring,Damper[] Assy_Damper,AntiRollBar[] Assy_ARB,Chassis Assy_Chassis,WheelAlignment[] Assy_WA,OutputClass[] Assy_OC);
        void Undo_ModifyVehicle(int l_modify, ICommandVehicle command);
    }
}

                            
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding_Attempt_with_GUI
{
    public interface ICommand
    {
        void ModifyObjectData(int l_modify, Object InputObject,bool RedoIdentifier);
        void Undo_ModifyObjectData(int l_unexcute,ICommand command);
    }
}

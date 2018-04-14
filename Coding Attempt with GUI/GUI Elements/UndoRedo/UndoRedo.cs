using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Coding_Attempt_with_GUI
{
    public class UndoRedo
    {
        public int IndexIdentifier { get; set; }

        private static bool _modifiedIdentifier;

        public static bool ModifiedIdentifier
        {
            get { return _modifiedIdentifier; }
            set { _modifiedIdentifier = value; }
        }

        #region UndoRedo Class's Undo/Redo Stacks
        private Stack<ICommand> Undocommands = new Stack<ICommand>();
        private Stack<ICommand> Redocommands = new Stack<ICommand>(); 
        #endregion

        public ICommand command;

        public static event EventHandler EnableDisableUndoRedoFeature;

        #region This method identifies the index (input item number) of the Input Item and whether is is modified (and hence undoable) or not
        public void Identifier(Stack<ICommand> _UndoInputItemCommands, Stack<ICommand> _RedoInputItemCommands, int _IndexIdentifier, bool _ModifiedIdentifier)
        {
            IndexIdentifier = _IndexIdentifier;
            ModifiedIdentifier = _ModifiedIdentifier;

            Undocommands = _UndoInputItemCommands;
            Redocommands = _RedoInputItemCommands;

            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        } 
        #endregion

        #region This method assigns the UndoRedo Class's Undo/Stacks with the Input Item Stack which is currently in focus and hence could be Undone or Redone
        private void UndoRedoStackSelector(Stack<ICommand> _UndoCommands, Stack<ICommand> _RedoCommands)
        {
            Undocommands = _UndoCommands;
            Redocommands = _RedoCommands;

            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }

        } 
        #endregion

        #region Redo Method
        public void Redo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (Redocommands.Count != 0)
                {
                    command = Redocommands.Pop();
                    command.ModifyObjectData(IndexIdentifier - 1, command, true);
                    ModifiedIdentifier = true;
                }
            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        } 
        #endregion

        #region Undo Method
        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (Undocommands.Count != 0)
                {
                    command = Undocommands.Pop();
                    command.Undo_ModifyObjectData(IndexIdentifier - 1, command);
                }
            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        } 
        #endregion

        #region Resetting the Undo/Redo Stacks
        public void ResetUndoRedo()
        {
            Undocommands.Clear();
            Redocommands.Clear();
        }
        #endregion

        public static void EnableDisableEventRaiser()
        {
            EnableDisableUndoRedoFeature?.Invoke(null, null);

        }

        #region Checking if Undo/Redo is possible
        public bool IsUndoPossible()
        {
            if (Undocommands.Count != 0 && ModifiedIdentifier == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRedoPossible()
        {
            if (Redocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion











    }

}

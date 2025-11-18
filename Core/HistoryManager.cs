using System.Collections.Generic;
using BoardGames.Interfaces;

namespace BoardGames.Core
{
    public class HistoryManager // LIFO stacks for undo and redo history
    {
        private Stack<IGameCommand> UndoStack = new Stack<IGameCommand>();
        private Stack<IGameCommand> RedoStack = new Stack<IGameCommand>();

        public void ExecuteCommand(IGameCommand command) // runs a command and records it
        {
            command.Execute();
            UndoStack.Push(command);
            RedoStack.Clear();
        }

        public bool Undo() // Reverts the last executed command and moves it to the redo stack
        {
            if (UndoStack.Count == 0) return false;
            IGameCommand command = UndoStack.Pop();
            command.Undo();
            RedoStack.Push(command);
            return true;
        }

        public bool Redo() // Re-apply the last undone command and move it back to the undo stack
        {
            if (RedoStack.Count == 0) return false;
            IGameCommand command = RedoStack.Pop();
            command.Execute();
            UndoStack.Push(command);
            return true;
        }

        public void Clear() // clears all history
        {
            UndoStack.Clear();
            RedoStack.Clear();
        }
    }
}
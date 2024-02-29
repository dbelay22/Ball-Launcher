using System;
using System.Collections;
using System.Collections.Generic;
using Yxp.Command;
using UnityEngine;
using Yxp.Debug;

namespace Yxp.Unity.Command
{
    public class CommandInvoker : MonoBehaviour
    {
        private static CommandInvoker _instance;
        public static CommandInvoker Instance { get { return _instance; } }

        private Stack<ICommand> _undoStack;

        private Stack<ICommand> _callStack;

        private CommandInvoker() { }

        void Awake()
        {
            _instance = this;
            
            _undoStack = new Stack<ICommand>();
            
            _callStack = new Stack<ICommand>();
        }

        public void ExecuteCommand(ICommand command)
        {
            _callStack.Push(command);

            StartCoroutine(ExecuteNextCommand());
        } 

        IEnumerator ExecuteNextCommand()
        {
            ICommand nextCommand = _callStack.Pop();

            if (nextCommand == null)
            {
                YLogger.Error("CommandInvoker] ExecuteNextCommand) There's no command to execute");
            }

            nextCommand.Execute();

            _undoStack.Push(nextCommand);

            yield return null;
        }

        public void UndoLastCommand()
        {
            if (_undoStack.Count > 0)
            {
                ICommand lastCommand = _undoStack.Pop();

                lastCommand.Undo();
            }
        }
    }
}

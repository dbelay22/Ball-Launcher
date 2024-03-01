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
        public static CommandInvoker Instance { get; private set; }

        private Stack<ICommand> _undoStack;

        private Queue<ICommand> _executionQueue;

        private CommandInvoker() { }

        private void Awake()
        {
            Instance = this;

            _executionQueue = new Queue<ICommand>();

            _undoStack = new Stack<ICommand>();            
        }

        public void ExecuteCommand(ICommand command)
        {
            // enque command and return control
            _executionQueue.Enqueue(command);
            
            YLogger.Verbose($"CommandInvoker] ExecuteCommand) just enqueded: {command}");
        }

        private void Update()
        {
            if (_executionQueue.Count > 0)
            {
                YLogger.Verbose($"CommandInvoker] Update) callstack count: {_executionQueue.Count} - Executing next command");
                
                StartCoroutine(ExecuteNextCommand());
            }            
        }        

        private IEnumerator ExecuteNextCommand()
        {
            ICommand nextCommand = _executionQueue.Dequeue();

            YLogger.Verbose($"CommandInvoker] ExecuteNextCommand) nextCommand: {nextCommand} - executing...");

            if (nextCommand == null)
            {
                YLogger.Error("CommandInvoker] ExecuteNextCommand) There's no command to execute");
            }

            nextCommand.Execute();

            YLogger.Verbose($"CommandInvoker] ExecuteNextCommand) put command on Undo stack");            
            _undoStack.Push(nextCommand);

            yield return null;
        }

        public bool UndoLastCommand()
        {
            if (_undoStack.Count < 1)
            {
                // nothing to undo
                return false;
            }

            ICommand lastCommand = _undoStack.Pop();

            if (lastCommand == null)            
            {
                YLogger.Error($"CommandInvoker] UndoLastCommand) lastCommand is null");
                return false;
            }

            YLogger.Verbose($"CommandInvoker] UndoLastCommand) Undoing now...{lastCommand}");
            lastCommand.Undo();

            return true;
        }
    }
}

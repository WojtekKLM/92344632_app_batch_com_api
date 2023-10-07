/*This sample code is made available at no cost.
 * Any liability for defects as to quality or title of the information,
 * software and documentation especially in relation to the correctness
 * or absence of defects or the absence of claims or third party rights 
 * or in relation to completeness and/or fitness for purpose are excluded except
 * for cases involving willful misconduct or fraud.
 * Any further liability of Siemens is excluded unless required by law,
 * e.g. under the Act on Product Liability or in cases of willful misconduct,
 * gross negligence, personal injury or death, failure to meet guaranteed characteristics,
 * fraudulent concealment of a defect or in case of breach of fundamental contractual obligations.
 * The damages in case of breach of fundamental contractual obligations is limited to the contract-typical,
 * foreseeable damage if there is no willful misconduct or gross negligence.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input; 
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows; 
using System.ComponentModel; 

namespace  Siemens.Automation.bfapicmx_csharpsamplex
{
    
    /// <summary>
    /// Routing of events from MainWindow to functions in the class ApiInvokes
    /// </summary>
    public class ViewModelBase
    {
        MainWindow m_mainwindow;
        public ViewModelBase(MainWindow _mainwindow)
        {
            m_mainwindow = _mainwindow; 
        }

        private bfapicmx_CApiInvocation m_apiInvokes;
        public bfapicmx_CApiInvocation APIInvokes
        {
            get
            {
                if (m_apiInvokes == null)
                {
                    m_apiInvokes = new bfapicmx_CApiInvocation(m_mainwindow); 
                }
                return m_apiInvokes; 
            }
        }

        private CBFAPICMXCommand<string> m_GetCommand;
        private CBFAPICMXCommand<string> m_SetCommand;
        private CBFAPICMXCommand<string> m_CreateCommand;
        private CBFAPICMXCommand<string> m_CopyCommand;
        private CBFAPICMXCommand<string> m_DeleteCommand;
        private CBFAPICMXCommand<string> m_ExecuteCommand;

        /// <summary>
        /// Changes the cursor to Wait until the command is finished and calls APIInvokes.InvokeCommand.
        /// </summary>
        /// <param name="command">The command to invoke</param>
        /// <param name="type">The type of command to execute (e.g. Get, Set)</param>
        void Invoke(string command, bfapicmx_CApiInvocation.CommandType type)
        {
            m_mainwindow.Cursor = Cursors.Wait;
            Dispatcher.CurrentDispatcher.Invoke(new Action(() => { }), DispatcherPriority.Background, null);
            try
            {
                APIInvokes.InvokeCommand(command, false, type);
            }
            catch ( ArgumentException e)
            {
                if (command == "CopyParameter" 
                    || command == "DelParameter" 
                    || command == "AddOps" 
                    || command == "ClearOps" 
                    || command == "AddObj" 
                    || command == "ClearObj")
                {
                    ;//MessageBox.Show( command  + " " + " succeded "  );
                }
                else
                MessageBox.Show("APIInvokes.InvokeCommand() failed: " + e.Message);
            }
            m_mainwindow.Cursor = Cursors.Arrow;        
        }

        // The properties return the command that is called when clicking a button.
        // The type of the command (e.g. Get, Set) is set in the xaml in the attribute Command.
        // The attribute CommandParameter defines the name of the command, which is passed in the key parameter.
                
        public ICommand GetCommand
        {
            get
            {
                if (m_GetCommand == null)
                {
                    m_GetCommand = new CBFAPICMXCommand<string>(delegate(string key)
                        {
                            Invoke(key, bfapicmx_CApiInvocation.CommandType.Get);
                        }
                        );
                }
                return m_GetCommand; 
            }
        }

        public ICommand SetCommand
        {
            get
            {
                if (m_SetCommand == null)
                {
                    m_SetCommand = new CBFAPICMXCommand<string>(delegate(string key)
                    {
                        Invoke(key, bfapicmx_CApiInvocation.CommandType.Set);
                    });
                }
                return m_SetCommand;
            }
        }

        public ICommand CreateCommand
        {
            get
            {
                if (m_CreateCommand == null)
                {
                    m_CreateCommand = new CBFAPICMXCommand<string>(delegate(string key)
                    {
                        Invoke(key, bfapicmx_CApiInvocation.CommandType.Create);
                    });
                }
                return m_CreateCommand;
            }
        }

        public ICommand CopyCommand
        {
            get
            {
                if (m_CopyCommand == null)
                {
                    m_CopyCommand = new CBFAPICMXCommand<string>(delegate(string key)
                    {
                        Invoke(key, bfapicmx_CApiInvocation.CommandType.Copy);
                    });
                }
                return m_CopyCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (m_DeleteCommand == null)
                {
                    m_DeleteCommand = new CBFAPICMXCommand<string>(delegate(string key)
                    {
                        Invoke(key, bfapicmx_CApiInvocation.CommandType.Delete);
                    });
                }
                return m_DeleteCommand;
            }
        }
        
        public ICommand ExecuteCommand
        {
            get
            {
                if (m_ExecuteCommand == null)
                {
                    m_ExecuteCommand = new CBFAPICMXCommand<string>(delegate(string key)
                        {
                            Invoke(key, bfapicmx_CApiInvocation.CommandType.Execute);
                        });
                }
                return m_ExecuteCommand; 
            }
        }
    }

    /// <summary>
    /// Implementation of ICommand. 
    /// Require an action to be passed which is execute when a button is clicked on the GUI .
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CBFAPICMXCommand<T> : ICommand
    {
        public CBFAPICMXCommand(Action<T> action) :
            this(action, null, false)
        {
        }

        public CBFAPICMXCommand(Action<T> action, Func<T,bool> canExecute) :
            this(action, canExecute, false)
        {
        }

        public CBFAPICMXCommand(Action<T> action, Func<T,bool> canExecute, bool isAutomaticRequeryDisabled)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            _action = action;
            _canExecute = canExecute;
            _isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        private readonly Action<T> _action;
        private readonly Func<T,bool> _canExecute;
        private bool _isAutomaticRequeryDisabled;
        private List<WeakReference> _canExecuteChangedHandlers;

        public bool CanExecute(T parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }

        public void Execute(T parameter)
        {
            if (_action != null)
            {
                _action(parameter);
            }
        }
        
        public bool IsAutomaticRequeryDisabled
        {
            get
            {
                return _isAutomaticRequeryDisabled;
            }
            set
            {
                if (_isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersFromRequerySuggested(_canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangedHandlers);
                    }
                    _isAutomaticRequeryDisabled = value;
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangedHandlers);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }
                CommandManagerHelper.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2);
            }
            remove
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }
                CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null && typeof(T).IsValueType)
            {
                return CanExecute((T)parameter);
            }
            return CanExecute((T)parameter); 
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }
    
    internal class CommandManagerHelper
    {
        internal static void CallWeakReferenceHandlers(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                EventHandler[] calles = new EventHandler[handlers.Count];
                int count = 0;

                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler handler = reference.Target as EventHandler;
                    if (handler == null)
                    {
                        handlers.RemoveAt(i);
                    }
                    else
                    {
                        calles[count] = handler;
                        count++;
                    }
                }
                for (int i = 0; i < count; i++)
                {
                    EventHandler handler = calles[i];
                    handler(null, EventArgs.Empty);
                }
            }
        }

        internal static void AddHandlersToRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                foreach (WeakReference handlerRef in handlers)
                {
                    EventHandler handler = handlerRef.Target as EventHandler;
                    if (handler != null)
                    {
                        CommandManager.RequerySuggested += handler;
                    }
                }
            }
        }

        internal static void RemoveHandlersFromRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                foreach (WeakReference handlerRef in handlers)
                {
                    EventHandler handler = handlerRef.Target as EventHandler;
                    if (handler != null)
                    {
                        CommandManager.RequerySuggested -= handler;
                    }
                }
            }
        }

        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        {
            AddWeakReferenceHandler(ref handlers, handler, -1);
        }

        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultListSize)
        {
            if (handlers == null)
            {
                handlers = (defaultListSize > 0 ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
            }

            handlers.Add(new WeakReference(handler));
        }

        internal static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers != null)
            {
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler existingHandler = reference.Target as EventHandler;
                    if ((existingHandler == null) || (existingHandler == handler))
                    {
                        // Clean up old handlers that have been collected
                        // in addition to the handler that is to be removed.
                        handlers.RemoveAt(i);
                    }
                }
            }
        }

    }
}


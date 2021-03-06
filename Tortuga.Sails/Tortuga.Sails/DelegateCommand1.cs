﻿using System;
using System.Windows.Input;

namespace Tortuga.Sails
{
    /// <summary>
    /// Creates a delegate-based ICommand for which a parameter of type T is needed.
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool>? m_CanExecute;

        private readonly Action<T> m_Command;

        /// <summary>
        /// Creates a Delegate command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        public DelegateCommand(Action<T> command, Func<T, bool>? canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command), $"{nameof(command)} is null.");

            m_Command = command;
            if (canExecute != null)
                m_CanExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        /// <remarks> This event only exists if a CanExecute delegate is provided. Otherwise add/remove are no-ops.</remarks>
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                if (m_CanExecute != null)
                    m_CanExecuteChanged += value;
            }
            remove
            {
                if (m_CanExecute != null)
                    m_CanExecuteChanged -= value;
            }
        }

        private event EventHandler? m_CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">
        /// Data used by the command.  If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public bool CanExecute(T parameter)
        {
            if (m_CanExecute == null)
                return true;
            else
                return m_CanExecute(parameter);
        }

#nullable disable
        bool ICommand.CanExecute(object parameter)
        {
            if (parameter != null && !(parameter is T))
                throw new ArgumentException($"{nameof(parameter)} was of type {parameter.GetType().Name}, but type {typeof(T).Name} or null was expected.", nameof(parameter));

            T typedParameter;
            try
            {
                typedParameter = (T)parameter;
            }
            catch (NullReferenceException) { return true; } //work-around when binding a non-nullable value type

            return CanExecute(typedParameter);
        }
#nullable restore

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.  If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(T parameter)
        {
            m_Command(parameter);
        }

#nullable disable
        void ICommand.Execute(object parameter)
        {
            if (parameter != null && !(parameter is T))
                throw new ArgumentException($"{nameof(parameter)} was of type {parameter.GetType().Name}, but type {typeof(T).Name} or null was expected.", nameof(parameter));

            Execute((T)parameter);
        }
#nullable restore

        /// <summary>
        /// Raises a CanExecuteChanged event.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            m_CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

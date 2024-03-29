﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Picosa.App.Infrastructure.Dialogs
{
    public abstract class DialogButtonViewModelBase : ViewModelBase, ICommand
    {
        private string _buttonText;
        private DialogButtonOptions _buttonOptions;
        private readonly Func<bool> _canExecute;

        protected DialogButtonViewModelBase(string buttonText, Func<bool> canExecute, DialogButtonOptions buttonOptions)
        {
            _buttonText = buttonText;
            _canExecute = canExecute;
            _buttonOptions = buttonOptions;
        }

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText == value) return;
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public bool IsDefault
        {
            get => _buttonOptions.HasFlag(DialogButtonOptions.IsDefault);
            set
            {
                if (IsDefault == value) return;

                if (value)
                    _buttonOptions |= DialogButtonOptions.IsDefault;
                else
                    _buttonOptions &= ~DialogButtonOptions.IsDefault;

                OnPropertyChanged();
            }
        }

        public bool IsCancel
        {
            get => _buttonOptions.HasFlag(DialogButtonOptions.IsCancel);
            set
            {
                if (IsCancel == value) return;

                if (value)
                    _buttonOptions |= DialogButtonOptions.IsCancel;
                else
                    _buttonOptions &= ~DialogButtonOptions.IsCancel;

                OnPropertyChanged();
            }
        }

        public bool IsDangerous
        {
            get => _buttonOptions.HasFlag(DialogButtonOptions.IsDangerous);
            set
            {
                if (IsDangerous == value) return;

                if (value)
                    _buttonOptions |= DialogButtonOptions.IsDangerous;
                else
                    _buttonOptions &= ~DialogButtonOptions.IsDangerous;

                OnPropertyChanged();
            }
        }

        [DebuggerStepThrough]
        public virtual bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public abstract void Execute(object parameter);

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
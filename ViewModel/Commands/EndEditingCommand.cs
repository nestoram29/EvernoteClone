﻿using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands {
    public class EndEditingCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        public NotesVM ViewModel { get; set; }

        public EndEditingCommand(NotesVM vm) {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if (parameter is Notebook notebook) {
                ViewModel.StopEditing(notebook);
            }
        }
    }
}

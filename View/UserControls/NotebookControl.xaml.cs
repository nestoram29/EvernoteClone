﻿using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvernoteClone.View.UserControls {
    /// <summary>
    /// Interaction logic for NotebookControl.xaml
    /// </summary>
    public partial class NotebookControl : UserControl {
        public Notebook Notebook {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is NotebookControl notebookControl) {
                notebookControl.DataContext = notebookControl.Notebook;
            }
        }

        public NotebookControl() {
            InitializeComponent();
        }
    }
}

using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View {
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window {
        NotesVM viewModel;
        public NotesWindow() {
            InitializeComponent();
            viewModel = Resources["vm"] as NotesVM;
            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;
            fontFamilyCB.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);

            fontSizeCB.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 28, 48, 72 };
        }

        private void ViewModel_SelectedNoteChanged(object sender, EventArgs e) {
            contentRichTB.Document.Blocks.Clear();
            if(viewModel.SelectedNote != null) {
                if (!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation)) {
                    using FileStream fStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open);
                    TextRange range = new TextRange(contentRichTB.Document.ContentStart, contentRichTB.Document.ContentEnd);
                    range.Load(fStream, DataFormats.Rtf);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void ContentRichTB_TextChanged(object sender, TextChangedEventArgs e) {
            int characterCount = (new TextRange(contentRichTB.Document.ContentStart, contentRichTB.Document.ContentEnd)).Text.Length;
            statusTB.Text = $"Document length: {characterCount} characters";
        }

        private void BoldBtn_Click(object sender, RoutedEventArgs e) {
            bool isChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isChecked)
                contentRichTB.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTB.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);

        }

        private void ItalicBtn_Click(object sender, RoutedEventArgs e) {
            bool isChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isChecked)
                contentRichTB.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTB.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);

        }

        private void UnderlineBtn_Click(object sender, RoutedEventArgs e) {
            bool isChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isChecked)
                contentRichTB.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else {
                (contentRichTB.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out TextDecorationCollection textDecorations);
                contentRichTB.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }

        }

        private void fontFamilyCB_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(fontFamilyCB.SelectedItem != null) {
                contentRichTB.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyCB.SelectedValue);
            }
        }

        private void fontSizeCB_TextChanged(object sender, TextChangedEventArgs e) {
            contentRichTB.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeCB.Text);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e) {
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{viewModel.SelectedNote.Id}.rtf");
            viewModel.SelectedNote.FileLocation = rtfFile;
            DatabaseHelper.Update(viewModel.SelectedNote);

            FileStream fStream = new FileStream(rtfFile, FileMode.Create);
            var contents = new TextRange(contentRichTB.Document.ContentStart, contentRichTB.Document.ContentEnd);
            contents.Save(fStream, DataFormats.Rtf);
        }
    }
}

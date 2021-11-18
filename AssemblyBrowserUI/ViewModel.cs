using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AssemblyBrowserCore;
using AssemblyBrowserCore.Model;
using Microsoft.Win32;

namespace AssemblyBrowserUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        public string FilePath { get; private set; }

        private AssemblyBrowser _browser = new AssemblyBrowser();

        public List<NamespaceMetadata> Namespaces { get; set; }

        private BrowserCommand _openFileCommand;
        public BrowserCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ??
                       (_openFileCommand = new BrowserCommand(obj =>
                       {
                           try
                           {
                               var openFileDialog = new OpenFileDialog();
                               if (openFileDialog.ShowDialog() == true)
                               {
                                   FilePath = openFileDialog.FileName;
                                   Namespaces = _browser.GetAssemblyData(FilePath);
                                   OnPropertyChanged(nameof(Namespaces));
                                   OnPropertyChanged(nameof(FilePath));
                               }
                           }
                           catch (Exception e)
                           {
                               MessageBox.Show($"Failed to load assembly. Error: {e}");
                           }
                       }));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
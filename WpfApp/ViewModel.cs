using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.Annotations;
using Microsoft.Win32;
using Browser;

namespace WpfApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Commander openSelectAssemblyDialog;
        public List<Node> Data { get; private set; }

        public Commander OpenSelectAssemblyDialog
        {
            get
            {
                return openSelectAssemblyDialog ??= new Commander(obj =>
                {
                    var openFileDialog = new OpenFileDialog();

                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        Data = new List<Node>();
                        Data.Add(AssemblyBrowser.Browse(openFileDialog.FileName));
                        OnPropertyChanged(nameof(Data));
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            Data = new();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

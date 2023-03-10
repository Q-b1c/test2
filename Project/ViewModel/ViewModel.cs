using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project
{
    class ViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModel()
        {
            openFileDialogCommand = new Command(ExecuteOpenFileDialog);
            openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Image files (*.TIF)|*.tif"
            };
        }

        readonly OpenFileDialog openFileDialog;

        public ImageSource Image { get; private set; }

        readonly ICommand openFileDialogCommand;
        public ICommand OpenFileDialogCommand { get { return openFileDialogCommand; } }

        void ExecuteOpenFileDialog()
        {
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    Image = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    OnPropertyChanged("Image");
                }
            }
        }
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
    }
}

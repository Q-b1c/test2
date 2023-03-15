using Microsoft.Win32;
using OSGeo.GDAL;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using ViewModelBase.Commands.QuickCommands;
using Color = System.Drawing.Color;

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
               
                    Gdal.AllRegister();
                    Ogr.RegisterAll();
                    Dataset dataset = Gdal.Open(openFileDialog.FileName, Access.GA_ReadOnly);
                    Band band = dataset.GetRasterBand(1);
                    int width = band.XSize;
                    int height = band.YSize;
                    int size = width * height;
                    byte[] data = new byte[width * height];
                    var arrData = Band.ReadRaster(0, 0, width, height, data, width, height, 0, 0);
                    int i, j;
                    for (i = 0; i < width; i++)
                    {
                        for (j = 0; j < height; j++)
                        {
                            (Convert.ToInt32(data[i + j * width]), Convert.ToInt32(data[i + j * width]), Convert.ToInt32(data[i + j * width]));
                        }
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

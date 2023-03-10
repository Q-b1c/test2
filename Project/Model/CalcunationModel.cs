using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project
{
    class CalculationModel
    {
        /// <summary>
        /// Функция для расчета NDSI
        /// </summary>
        /// <param name="raster3"></param>
        /// <param name="raster6"></param>
        /// <returns></returns>
        public int GettingNDSI(int[,] raster3, int[,] raster6,int width,int height) 
        {
            int NDSI=0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    NDSI = (raster3[i, j] - raster6[i, j]) / (raster3[i, j] + raster6[i, j]);   
                }
            }
            return NDSI;
        }

    }
}

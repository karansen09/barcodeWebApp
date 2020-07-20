using BarcodeWebApp.Interface;
using BarcodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarcodeWebApp.BAL
{
    public class BusinessScan
    {
        private static IAddNewScanning iaddnewscan = new DAL.AddNewScan();

        public static int AddScanning(BarcodeScan barcodeScan)
        {
            return iaddnewscan.AddNewScanning(barcodeScan);
        }
    }
}

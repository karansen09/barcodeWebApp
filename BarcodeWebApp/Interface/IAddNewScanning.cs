using BarcodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarcodeWebApp.Interface
{
    interface IAddNewScanning
    {
        int AddNewScanning(BarcodeScan scan);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarcodeWebApp.Models
{
    public class BarcodeScan
    {
        public string Code { get; set; }
        public string image { get; set; }
        public string imageurl { get; set; }
        public int State { get; set; }
        public int UserId { get; set; }
    }
}

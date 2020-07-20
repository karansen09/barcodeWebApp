using BarcodeWebApp.Helpers;
using BarcodeWebApp.Interface;
using BarcodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BarcodeWebApp.DAL
{
    public class AddNewScan : IAddNewScanning
    {
        public int AddNewScanning(BarcodeScan scan)
        {
            int add = 0;
            try
            {
                SqlCommand cmd = DataAccess.GetCommand();
                cmd.CommandText = "dbo.AddScanning";
                cmd.Parameters.AddWithValue("@Command", "NewScanning");
                cmd.Parameters.AddWithValue("@Scanresult", scan.Code);
                cmd.Parameters.AddWithValue("@ScanImageUrl", scan.imageurl);
                cmd.Parameters.AddWithValue("@StateName", scan.State);
                cmd.Parameters.AddWithValue("@UserId", scan.UserId);
                add = Convert.ToInt32(DataAccess.ExecureScaler(cmd));
            }
            catch(Exception ex)
            {

            }
            return add;
        }
    }
}

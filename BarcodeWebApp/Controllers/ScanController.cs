using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BarcodeWebApp.BAL;
using BarcodeWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarcodeWebApp.Controllers
{
    public class ScanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult MyImageResult(BarcodeScan barcode)
        {
            string img = Regex.Match(barcode.image, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

            string storePath = "wwwroot/UploadsImg/";
            string filepath= SaveImage(img, storePath);

            barcode.imageurl = filepath;
            if(string.IsNullOrWhiteSpace(Convert.ToString(barcode.State)))
            {
                barcode.State = 0;
            }
            if(string.IsNullOrWhiteSpace(Convert.ToString(barcode.UserId)))
            {
                barcode.UserId = 0;
            }
            int result = BusinessScan.AddScanning(barcode);
            return Json(result);
        }

        public string SaveImage(string base64,string storePath)
        {
            string strm = base64;
            strm = strm.Replace("data:image/png;base64,", "");
            //this is a simple white background image
            var myfilename = string.Format(@"{0}", Guid.NewGuid());

            //Generate unique filename
            string filepath = storePath + myfilename+ ".png";
            var bytess = Convert.FromBase64String(strm);
            using (var imageFile = new FileStream(filepath, FileMode.Create))
            {
                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }
            return filepath;
        }
    }
}
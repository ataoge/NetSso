using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ataoge.SsoServer.Web.Models;
using QRCoder;
using System.Drawing;
using System.IO;

namespace Ataoge.SsoServer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SendVerifyCode()
        {
            var aa = new {Code = 0};
            return Json(aa);
        }

        
        public IActionResult AuthenicatorQrCoder(string id)
        {
            
            var authenicatorUri = GenerateQrCodeUri(id);
            return QrCoder(authenicatorUri);
        }      
        
        private const string AuthenicatorUriFormat = "{0}://{1}{2}/Identity/Account/ConfirmDeviceLogin?secret={3}";

        private string GenerateQrCodeUri(string unformattedKey)
        {

            return string.Format(
                AuthenicatorUriFormat,
                Request.Scheme,
                Request.Host, Request.PathBase,
                unformattedKey);
        }

        public IActionResult QrCoder(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            if (string.IsNullOrEmpty(text))
            {
                text ="The text which should be encoded.";
            }
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            rootPath = AppContext.BaseDirectory;
            rootPath = Directory.GetCurrentDirectory();
            var path = Path.Combine(rootPath, "wwwroot");
            var icoPath = Path.Combine(path,"favicon.ico");
            var logoIcon = (Bitmap)Bitmap.FromFile(icoPath);
            Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, logoIcon);
            var ms = new MemoryStream();
            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return new FileContentResult(ms.ToArray(), "image/png");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

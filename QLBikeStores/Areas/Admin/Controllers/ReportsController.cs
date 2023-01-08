using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public ReportsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        //tra ve duong dan vat ly (contentrootpath (c:\baitap\reports\bc1.frx))
        //path.combine noi cac chuoi voi nhau = dau \
        public IActionResult ListOfProducts() 
        {
            var duongdan = Path.Combine(_env.ContentRootPath, "Reports", "Listofproducts.frx");
            var webReport = new WebReport();
            webReport.Report.Load(duongdan);
            //webReport.Report.Dictionary.Connections[0].ConnectionString = "Server=PM307-01\\MSSQLSERVER2019;Database=demo;User Id=sa; Password=123;"; pull
            return View(webReport);
        }

        public IActionResult ListOfProductsPDF()
        {
            var duongdan = Path.Combine(_env.ContentRootPath, "Reports", "Listofproducts.frx");
            var webReport = new WebReport();
            webReport.Report.Load(duongdan);

            webReport.Report.Prepare(); //tien hanh ket noi db nap du lieu len bao cao

            //ghi du lieu xuong tap tin pdf
            using(MemoryStream ms=new MemoryStream())
            {
                PDFSimpleExport pdfExport=new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                //webReport.Report.Dictionary.Connections[0].ConnectionString = "Server=PM307-01\\MSSQLSERVER2019;Database=demo;User Id=sa; Password=123;"; pull
                ms.Flush();//do du lieu tu bo nho tra ve respone client
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension(duongdan) + ".pdf");
            }

        }

        public IActionResult ListOfProductsPush()
        {
            var duongdan = Path.Combine(_env.ContentRootPath, "Reports", "Listofproducts.frx");
            var webReport = new WebReport();
            webReport.Report.Load(duongdan);
            //webReport.Report.Dictionary.Connections[0].ConnectionString = "Server=PM307-01\\MSSQLSERVER2019;Database=demo;User Id=sa; Password=123;"; pull
            return View(webReport);
        }


    }
}

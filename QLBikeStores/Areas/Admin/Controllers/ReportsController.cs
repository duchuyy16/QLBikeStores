using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
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
            webReport.Report.Dictionary.Connections[0].ConnectionString = "Server=MSI\\HUYSQLSERVER;Database=demo;User Id=sa; Password=123;"; //pull
            return View(webReport);
        }

        public IActionResult ListOfProductsPDF()
        {
            var duongdan = Path.Combine(_env.ContentRootPath, "Reports", "Listofproducts.frx");
            var webReport = new WebReport();
            webReport.Report.Load(duongdan);
            webReport.Report.Dictionary.Connections[0].ConnectionString = "Server=MSI\\HUYSQLSERVER;Database=demo;User Id=sa; Password=123;";
            webReport.Report.Prepare(); //tien hanh ket noi db nap du lieu len bao cao

            //ghi du lieu xuong tap tin pdf
            using(MemoryStream ms=new MemoryStream())
            {
                PDFSimpleExport pdfExport=new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms); 
                ms.Flush();//do du lieu tu bo nho tra ve respone client
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension(duongdan) + ".pdf");
            }

        }

        public IActionResult ListOfProductsPush()
        {
            try
            {
                var duongdanreport = Path.Combine(_env.ContentRootPath, "Reports", "Listofproducts.frx");
                var webReport = new WebReport();
                webReport.Report.Load(duongdanreport);

                var dsSanPham = Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham);

                var bangSanPham = Utilities.GetTable<Product>(dsSanPham, "Product");
                webReport.Report.RegisterData(bangSanPham, "Product");
                return View(webReport);
            }
            catch (Exception)
            {

                throw;
            }

            //dsXepHangPhim = dsXepHangPhim.Where(x => x.KyHieu.Equals("C13")).ToList();
            //var xephangid = dsXepHangPhim.FirstOrDefault(x => x.KyHieu.Equals("P")).Id;
            //dsPhim = dsPhim.Where(x => x.XepHangPhimId.Equals(xephangid)).ToList();
            //webReport.Report.SetParameterValue("pXepHangPhimId", xephangid);

            
        }

        public IActionResult ListOfProductsPushPDF()
        {
            var tentaptinbaocao = "Listofproducts.frx";
            var duongdanreport = Path.Combine(_env.ContentRootPath, "Reports", tentaptinbaocao);
            var webReport = new WebReport();
            webReport.Report.Load(duongdanreport);

            var dsSanPham = Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham);

            //var xephangid = dsXepHangPhim.FirstOrDefault(x => x.KyHieu.Equals("P")).Id;

            var bangSanPham = Utilities.GetTable<Product>(dsSanPham, "Product");
            webReport.Report.RegisterData(bangSanPham, "Product");
            webReport.Report.Prepare();

            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                //var tenbaocao = tentaptinbaocao.ToLower().Replace(".frx", "");
                var tenbaocao = Path.GetFileNameWithoutExtension(tentaptinbaocao);
                return File(ms.ToArray(), "application/pdf", tenbaocao + ".pdf");
            }
        }


    }
}

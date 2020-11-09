using System.IO;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;


namespace RISTExamOnlineProject.Controllers
{
    public class FastReportController : Controller
    {
        public IActionResult Index()
        {
            var webReport = GetReport();
            ViewBag.WebReport = webReport;
            return View();
        }

       
        public WebReport GetReport()
        {
           
            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            var webReport = new WebReport();

            var sqlConnection = new MsSqlDataConnection
            {
                ConnectionString = "Data Source=10.29.1.116;Initial Catalog=sptosystem;Persist Security Info=True;User ID=sa;Password=pwpolicy;Application Name=SPTO_SYSTEM"
            };
            //sqlConnection.ConnectionString = sqlConnection;
            sqlConnection.CreateAllTables();
            webReport.Report.Dictionary.Connections.Add(sqlConnection);
            webReport.Report.Load($@"Reports/Untitled.frx");
            const string staffop = "007895";
            const string planid = "T2220-0001";
            const string license = "CO1001";
            webReport.Report.SetParameterValue("PAM_OP", staffop);
            webReport.Report.SetParameterValue("PAM_PLANID", planid);
            webReport.Report.SetParameterValue("PAM_License", license);

            ViewBag.WebReport = webReport;
            return webReport;
        }
          

        public IActionResult Pdf()
        {
            var webReport = GetReport();
            webReport.Report.Prepare();

            using (var ms = new MemoryStream())
            {
                var pdfExport = new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Master-Detail") + ".pdf");
            }
        }
    }
}
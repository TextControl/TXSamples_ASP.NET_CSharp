using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tx_aspnet_core_mailmerge.Models;

namespace tx_aspnet_core_mailmerge.Controllers
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

            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
            {
                tx.Create();

                // adding static text
                TXTextControl.Selection sel = new TXTextControl.Selection();
                sel.Text = "Welcome to Text Control\r\n";
                sel.Bold = true;

                tx.Selection = sel;

                // adding merge fields
                TXTextControl.DocumentServer.Fields.MergeField mergeField =
                  new TXTextControl.DocumentServer.Fields.MergeField()
                  {
                      Text = "{{company}}",
                      Name = "company",
                      TextBefore = "Company name: "
                  };

                tx.ApplicationFields.Add(mergeField.ApplicationField);

                // alternatively load a template
                //TXTextControl.LoadSettings ls = new TXTextControl.LoadSettings() {
                //	ApplicationFieldFormat = TXTextControl.ApplicationFieldFormat.MSWord
                //};

                //tx.Load("template.docx", TXTextControl.StreamType.WordprocessingML, ls);

                // merge fields with MailMerge engine
                using (TXTextControl.DocumentServer.MailMerge mailMerge =
                  new TXTextControl.DocumentServer.MailMerge())
                {
                    mailMerge.TextComponent = tx;
                    mailMerge.MergeJsonData("[{\"company\": \"Text Control, LLC\" }]");
                }

                // return result as HTML
                string result = "";
                tx.Save(out result, TXTextControl.StringStreamType.HTMLFormat);

                // alternatively save as PDF
                //byte[] baPdf;
                //tx.Save(out baPdf, TXTextControl.BinaryStreamType.AdobePDF);

                ViewBag.Document = result;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
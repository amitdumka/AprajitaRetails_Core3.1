using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AprajitaRetails.Areas.Uploader.Controllers
{
    [Area("Uploader")]
    [Authorize]
    public class BankStatementUploadController : Controller
    {

        private readonly AprajitaRetailsContext db;
        private readonly ILogger<BankStatementUploadController> _logger;
        public BankStatementUploadController(AprajitaRetailsContext context, ILogger<BankStatementUploadController> logs)
        {
            db = context;
            _logger = logs;
        }

        // GET: BankStatementUploadController
        public ActionResult Index()
        {

            ViewData["AccountNumber"] = new SelectList(db.AccountNumbers, "AccountNumberId", "Account");
            return View();
        }

        // GET: BankStatementUploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public JsonResult Uploader(IFormFile file)
        {
            // IFormFile file = Request.Form.Files[0];

            string data = Request.Form["item"];

            InputData input = new InputData();
            InputData.CopyObjects(data, out input);

            ReturnInfo Info = new ReturnInfo
            {
                IsSuccess = true,
                LinkAdress = "",
                SuccessMessage = "Successfuly Uploaded!",
                ErrorMessage = data

            };
            if (file.FileName.Length > 0) Info.LinkAdress = "FileName:" + file.FileName;
            else
            {
                Info.ErrorMessage = "File not found"; Info.IsSuccess = false;
            }
            return new JsonResult(Info);

        }

    }
}

using AprajitaRetails.Data;
using AprajitaRetails.Models.JsonData;
using AprajitaRetails.Ops.Uploader;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoyBillController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public VoyBillController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: api/VoyBill
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Get", "Get function not implemented. Use Post to Upload Bill" };
        }

        // POST api/VoyBill
        [HttpPost]
        public async Task<ServerReturn> PostVoyBillAsync([FromBody] Bill invoice)
        {
            return await VoyBillProcessor.ProcessVoyInvoiceXML(invoice, _context);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Uploader;
using AprajitaRetailsWatcher.Model.XMLData;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route ("api/[controller]")]
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
            return new string [] { "value1", "value2" };
        }

        // GET api/VoyBill/5
        [HttpGet ("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/VoyBill
        [HttpPost]
        public async Task<ActionResult<ServerReturn>> PostVoyBill([FromBody] root invoice)
        {

            return await VoyBillProcessor.ProcessVoyInvoiceXML (invoice, _context);
        }


        // PUT api/VoyBill/5
        [HttpPut ("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/VoyBill/5
        [HttpDelete ("{id}")]
        public void Delete(int id)
        {
        }
    }
}

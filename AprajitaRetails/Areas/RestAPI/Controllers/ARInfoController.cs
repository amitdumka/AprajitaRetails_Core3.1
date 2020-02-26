using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ARInfoController : ControllerBase
    {
        private readonly AprajitaRetailsContext db;
        public ARInfoController(AprajitaRetailsContext con)
        {
            db = con;
        }


        // GET: api/ARInfo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string [] { "value1", "value2" };
        }

        // GET: api/ARInfo/5
        [HttpGet ("{id:int}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        // GET: api/ARInfo/5
        [HttpGet ("{myInfo:long}", Name = "Get")]
        public string GetMyInfo(long myInfo)
        {   
            //TODO: add Total Sale, total Attendance, leaves, incentive, Lp  etc. Dues, Adances
            var emp = db.TelegramAuthUsers.Where (c => c.TelegramChatId == myInfo).FirstOrDefault ();
            if ( emp != null )
            {
                string msg = "Name:\t" + emp.TelegramUserName + "\nChatId:\t" + emp.TelegramChatId;
                return msg;
            }
            else
                return "Error";
        }
        // GET: api/ARInfo/5
        [HttpGet ("{myInfo}/{staffInfo}", Name = "Get")]
        public string GetStaffInfo(string staffInfo)
        {
            return "value";
        }

        // POST: api/ARInfo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ARInfo/5
        [HttpPut ("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete ("{id}")]
        public void Delete(int id)
        {
        }
    }
}

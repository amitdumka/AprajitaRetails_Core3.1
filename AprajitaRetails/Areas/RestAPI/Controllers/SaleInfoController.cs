using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Voyager.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models.ViewModels;
using AprajitaRetails.Ops.WidgetModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AprajitaRetails.Areas.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleInfoController : ControllerBase
    {

        private readonly AprajitaRetailsContext _context;

        public SaleInfoController(AprajitaRetailsContext con)
        {
            _context = con;
        }
        // GET: api/SaleInfo
        /// <summary>
        /// Get All Store Sale Info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DailySaleReport Get()
        {
            DailySaleReport saleReport = SaleWidgetModel.GetSaleRecord(_context);
            return saleReport;
            //return new string[] { "value1", "value2" };
        }

        // GET api/SaleInfo/1
        /// <summary>
        /// Get Sale Info for a Store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public DailySaleReport Get(int id)
        {
            DailySaleReport saleReport = SaleWidgetModel.GetSaleRecord(_context);
            return saleReport;
        }

        //// POST api/<SaleInfoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<SaleInfoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SaleInfoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }

    [Route("api/[controller]")]
    [ApiController]
    public class StoreInfoController : ControllerBase
    {
        private readonly AprajitaRetailsContext _context;

        public StoreInfoController(AprajitaRetailsContext con)
        {
            _context = con;
        }
        [HttpGet]
        public async Task<IEnumerable<Store>> GetAsync()
        {

            return await _context.Stores.ToListAsync();
            //return new string[] { "value1", "value2" };
        }

        // GET api/SaleInfo/1
        /// <summary>
        /// Get Sale Info for a Store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Store> GetAsync(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseInfoController : ControllerBase
    {
        private readonly AprajitaRetailsContext db;
        public ExpenseInfoController(AprajitaRetailsContext con)
        {
            db = con;
        }
        [HttpGet]
        public AccountsInfo Get()
        {
            AccountsInfo info = HomeWidgetModel.GetAccoutingRecord(db);
            return info;
        }

        [HttpGet("{id}")]
        public AccountsInfo Get(int id)
        {
            AccountsInfo info = HomeWidgetModel.GetAccoutingRecord(db,id);
            return info;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmpInfoController : ControllerBase
    {
        private readonly AprajitaRetailsContext db;
        public EmpInfoController(AprajitaRetailsContext con)
        {
            db = con;
        }
        [HttpGet]
        public List<EmpBasicInfo> Get()
        {
            List<EmpBasicInfo> info = HomeWidgetModel.GetEmpBasicInfo(db);
            return info;
        }

        [HttpGet("{id}")]
        public List<EmpBasicInfo> Get(int id)
        {
            List<EmpBasicInfo> info = HomeWidgetModel.GetEmpBasicInfo(db, id);
            return info;
        }
    }



}

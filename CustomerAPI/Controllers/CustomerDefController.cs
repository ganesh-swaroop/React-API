using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Models;
using CustomerAPI.DAL;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic;


namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerDefController : ControllerBase
    {
        private CustomerDefDal dal;
        private CustomerDef _dtoPrev;

        private readonly CustomerDbContext _context= new CustomerDbContext();

        public CustomerDefController(CustomerDbContext context){
            _context = context;
        }

        [HttpPost]
        [Route("SearchRecord")]
        public async Task<List<CustomerDef>> SearchRecord(CustomerDefSO defSO)
        {
            dal = new CustomerDefDal(_context);
            var dbDto =  dal.SearchRecord(defSO);
            return  await dbDto.ToListAsync();
        }

        [HttpPost]
        [Route("SearchPagedRecord")]
        public async Task<List<CustomerDef>> SearchPagedRecords(CustomerDefSO dtoSO)
        {
            dal = new CustomerDefDal(_context);
            var dbDto = dal.SearchPagedRecords(dtoSO);
            return await dbDto.ToListAsync();
        }

        [HttpPost]
        [Route("RecordCount")]
        public async Task<int> GetRecordCount(CustomerDefSO dtoSO)
        {
            dal = new CustomerDefDal(_context);
            var count=  await dal.GetTotalCount(dtoSO);
            return count;
        }

        [HttpPost]
        [Route("Select")]
        public async Task<CustomerDef> GetRecord(string ClientID)
        {
            dal = new CustomerDefDal(_context);
            var clientDef = await dal.GetRecord(ClientID);

            return clientDef;
        }

        [HttpPost]
        [Route("SaveRecord")]
        public async Task<CustomerDef> SaveRecord(CustomerDef customerDef,bool IsNewRecord)
        {
            dal = new CustomerDefDal(_context);
            if (IsNewRecord)
            {
                customerDef.CreTime = System.DateTime.Now;
                customerDef.ModTime = System.DateTime.Now;
                customerDef.IsActive = "Y";
                await dal.Insert(customerDef);
            }
            else
            {
                customerDef.ModTime = System.DateTime.Now;
                await dal.Update(customerDef);
            }

            return customerDef;            
        }
        
        [HttpPost]
        [Route("Delete")]
        public async Task RemoveRecord(string CustomerID)
        {
            dal = new CustomerDefDal(_context);
            _dtoPrev = dal.Select(CustomerID);
            if (_dtoPrev != null)
            {
                await dal.Delete(CustomerID);
            }
            else
            {
                throw new Exception("Can not be removed ");
            }

        }
        
        [HttpPost]
        [Route("Getvalues")]
        public Dictionary<string, string> GetKeyValues(string keyCol, string valueCol, CustomerDefSO dtoSO)
        {
            dal = new CustomerDefDal(_context);
            var dctdata = dal.GetKeyValues(keyCol, valueCol, dtoSO);
            return dctdata;
        }
    }

}

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
        private readonly CustomerDefDal _dal;
        private readonly CustomerDbContext _context;

        public CustomerDefController(CustomerDbContext context){
            _dal = new CustomerDefDal(_context);
            _context = context;
        }

        [HttpPost(nameof(SearchRecord))]
        [Route("SearchRecord")]
        public async Task<List<CustomerDef>> SearchRecord(CustomerDefSO defSO)
        {
            var dbDto =  _dal.SearchRecord(defSO);
            return  await dbDto.ToListAsync();
        }

        [HttpPost(nameof(SearchPagedRecords))]
        [Route("SearchPagedRecords")]
        public async Task<List<CustomerDef>> SearchPagedRecords(CustomerDefSO dtoSO)
        {
            var dbDto = _dal.SearchPagedRecords(dtoSO);
            return await dbDto.ToListAsync();
        }

        [HttpPost(nameof(RecordCount))]
        [Route("RecordCount")]
        public async Task<int> RecordCount(CustomerDefSO dtoSO)
        {
            var count=  await _dal.GetTotalCount(dtoSO);
            return count;
        }

        [HttpPost(nameof(GetRecord))]
        [Route("GetRecord")]
        public async Task<CustomerDef> GetRecord(string customerID)
        {
            var customerDef = await _dal.GetRecord(customerID);

            return customerDef;
        }

        [HttpPost(nameof(SaveRecord))]
        [Route("SaveRecord")]
        public async Task<CustomerDef> SaveRecord(CustomerDef customerDef,bool isNewRecord)
        {
            if (isNewRecord)
            {
                customerDef.CreTime = System.DateTime.Now;
                customerDef.ModTime = System.DateTime.Now;
                customerDef.IsActive = "Y";
                await _dal.Insert(customerDef);
            }
            else
            {
                customerDef.ModTime = System.DateTime.Now;
                await _dal.Update(customerDef);
            }

            return customerDef;            
        }
        
        [HttpPost(nameof(RemoveRecord))]
        [Route("RemoveRecord")]
        public async Task RemoveRecord(string customerID)
        {
            CustomerDef _dtoPrev;

            _dtoPrev = await _dal.GetRecord(customerID);
            if (_dtoPrev != null)
            {
                await _dal.Delete(customerID);
            }
            else
            {
                throw new Exception("Can not be removed ");
            }

        }
        
        [HttpPost(nameof(GetKeyValues))]
        [Route("GetKeyValues")]
        public Dictionary<string, string> GetKeyValues(string keyCol, string valueCol, CustomerDefSO dtoSO)
        {
            var dctdata = _dal.GetKeyValues(keyCol, valueCol, dtoSO);
            return dctdata;
        }
    }

}

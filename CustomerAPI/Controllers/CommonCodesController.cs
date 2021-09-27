using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Models;
using CustomerAPI.DAL;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace CustomerAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CommonCodesController
    {
        private readonly CustomerDbContext _context;
        private readonly CommonCodesDal _dal;

        public CommonCodesController(CustomerDbContext context)
        {
            _context = context;
            _dal = new CommonCodesDal(_context);
        }

        [HttpPost(nameof(SearchRecord))]
        public async Task<List<CommonCodes>> SearchRecord(CommonCodesSO defSO)
        {
            var dbDto = _dal.SearchRecord(defSO);
            return await dbDto.ToListAsync();
        }

        [HttpPost(nameof(SearchPagedRecords))]
        public async Task<List<CommonCodes>> SearchPagedRecords(CommonCodesSO dtoSO)
        {
            var dbDto = _dal.SearchPagedRecords(dtoSO);
            return await dbDto.ToListAsync();
        }

        [HttpPost(nameof(GetRecordCount))]
        public async Task<int> GetRecordCount(CommonCodesSO dtoSO)
        {
            var count = await _dal.GetTotalCount(dtoSO);
            return count;
        }

        [HttpGet(nameof(GetRecord))]
        public async Task<CommonCodes> GetRecord(string CodeType, string CMCode)
        {
            var clientDef = await _dal.GetRecord(CodeType, CMCode);
            return clientDef;
        }

        [HttpPost(nameof(SaveRecord))]
        public async Task<CommonCodes> SaveRecord(CommonCodes commonCodes, bool isNewRecord)
        {

            if (isNewRecord)
            {
                commonCodes.CreTime = System.DateTime.Now;
                commonCodes.ModTime = System.DateTime.Now;
                commonCodes.IsActive = "Y";
                await _dal.Insert(commonCodes);
            }
            else
            {
                commonCodes.ModTime = System.DateTime.Now;
                await _dal.Update(commonCodes);
            }

            return commonCodes;
        }

        [HttpDelete(nameof(RemoveRecord))]
        public async Task RemoveRecord(string codeType, string cmCode)
        {
            CommonCodes _dtoPrev;
            _dtoPrev = await _dal.GetRecord(codeType, cmCode);
            if (_dtoPrev != null)
            {
                await _dal.Delete(codeType, cmCode);
            }
            else
            {
                throw new Exception("Can not be removed ");
            }

        }

        [HttpPost(nameof(GetKeyValues))]
        public Dictionary<string, string> GetKeyValues(string keyCol, string valueCol, CommonCodesSO dtoSO)
        {
            var dctdata = _dal.GetKeyValues(keyCol, valueCol, dtoSO);
            return dctdata;
        }


    }
}
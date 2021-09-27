using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CustomerAPI.DAL
{

    public partial class CustomerDbContext : DbContext
    {
        public DbSet<CommonCodes> commonCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonCodes>().HasKey(k => new { k.CodeType, k.CMCode });
        }
    }

    public class CommonCodesDal
    {
        private readonly CustomerDbContext _db;
        public CommonCodesDal(CustomerDbContext db)
        {
            _db = db;
        }

        public async Task Insert(CommonCodes commonCode)
        {
            if (commonCode == null)
            {
                throw new InvalidOperationException("CommonCode parameter cannot be null");
            }
            if (commonCode.CodeType == null)
            {
                throw new Exception("CodeType or CMCode can not be null");
            }
            await _db.commonCodes.AddAsync(commonCode);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommonCodesExists(commonCode.CodeType, commonCode.CMCode) != null)
                {
                    throw new Exception("CodeType already exists");
                }
                else
                {
                    throw new Exception("CodeType was not added");
                }
            }
        }

        public async Task Update(CommonCodes commonCode)
        {
            if (commonCode == null)
            {
                throw new InvalidOperationException("CommonCodes parameter cannot be null");
            }
            _db.commonCodes.Update(commonCode);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommonCodesExists(commonCode.CodeType, commonCode.CMCode) == null)
                {
                    throw new Exception("CodeType does not exists");
                }
                else
                {
                    throw new Exception("CodeType was not updated");
                }
            }
        }

        public IQueryable<CommonCodes> SearchRecord(CommonCodesSO dtoSO)
        {
            if (dtoSO == null)
            {
                throw new Exception("CommonCodesSO can not be null");
            }
            var dbDto = from c in _db.commonCodes select c;
            dbDto = AddFilter(dbDto, dtoSO);
            return dbDto;
        }

        public IQueryable<CommonCodes> SearchPagedRecords(CommonCodesSO dtoSO)
        {
            if (dtoSO == null)
            {
                throw new Exception("CommonCodesSO can not be null");
            }
            if (dtoSO.StartIndex < 0)
            {
                throw new Exception("StartIndex can not be negative");
            }
            if (dtoSO.RecordsPerPage <= 0)
            {
                throw new Exception("Records to be fetched should be greater than 0");
            }
            List<CommonCodes> listDto = new List<CommonCodes>();
            var dbDto = from c in _db.commonCodes select c;
            dbDto = AddFilter(dbDto, dtoSO);
            dbDto = AddOrderBy(dbDto, dtoSO);
            dbDto = dbDto.Skip(dtoSO.StartIndex).Take(dtoSO.RecordsPerPage);
            return dbDto;
        }

        public async Task<int> GetTotalCount(CommonCodesSO dtoSO)
        {
            if (dtoSO == null)
            {
                throw new Exception("CommonCodesSO can not be null");
            }
            var dbDto = from c in _db.commonCodes select c;
            dbDto = AddFilter(dbDto, dtoSO);
            return await dbDto.CountAsync();
        }

        public async Task<CommonCodes> GetRecord(string codeType, string cmCode)
        {
            if (string.IsNullOrEmpty(codeType) || string.IsNullOrEmpty(cmCode))
            {
                throw new Exception("CodeType or CMCode can not be null or empty");
            }
            var commonCodes = await _db.commonCodes.FindAsync(codeType, cmCode);

            if (commonCodes == null)
            {
                throw new Exception("CodeType is not valid");
            }
            return commonCodes;
        }

        private CommonCodes CommonCodesExists(string codeType, string cmCode)
        {
            if (codeType == null || cmCode == null)
            {
                throw new Exception("CodeType and CMCode can not be null");
            }
            return _db.commonCodes.Find(codeType, cmCode);
        }

        public async Task Delete(string codeType, string cmCode)
        {
            if (codeType == null || cmCode == null)
            {
                throw new Exception("CodeType and CMCode can not be null");
            }
            var commonCodes = await _db.commonCodes.FindAsync(codeType, cmCode);
            if (commonCodes == null)
            {
                throw new Exception("CodeType is not valid");
            }

            _db.commonCodes.Remove(commonCodes);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("CodeType was not Deleted");

            }
        }

        public Dictionary<string, string> GetKeyValues(string keyCol, string valueCol, CommonCodesSO dtoSO)
        {
            if (keyCol == null)
            {
                throw new InvalidOperationException("keyCol parameter cannot be null");
            }
            if (valueCol == null)
            {
                throw new InvalidOperationException("valueCol parameter cannot be null");
            }
            if (dtoSO.Orderby == null) dtoSO.Orderby = "";
            Dictionary<string, string> keyValPair = new Dictionary<string, string>();
            var dbDto = from c in _db.commonCodes select c;
            dbDto = AddFilter(dbDto, dtoSO);
            dbDto = AddOrderBy(dbDto, dtoSO, valueCol);
            IQueryable result;

            if (keyCol == valueCol)
            {
                result = dbDto.Select("new (" + keyCol + ")");
            }
            else
            {
                result = dbDto.Select("new (" + keyCol + "," + valueCol + ")");
            }
            string a = "";
            foreach (dynamic d in result)
            {
                if (!keyValPair.TryGetValue(d.GetType().GetProperty(keyCol).GetValue(d).ToString(), out a))
                    keyValPair.Add(d.GetType().GetProperty(keyCol).GetValue(d).ToString(),
                        d.GetType().GetProperty(valueCol).GetValue(d).ToString());
            }
            return keyValPair;
        }

        public async Task InsertRecords(List<CommonCodes> record)
        {
            if (record == null)
            {
                throw new InvalidOperationException("List<CommonCodes> parameter cannot be null");
            }
            await _db.commonCodes.AddRangeAsync(record);
            // foreach (CommonCodes commonCodes in record)
            // {
            //     if (commonCodes.CodeType == null || commonCodes.CMCode == null)
            //     {
            //         throw new Exception("CodeType or CMCode can not be null");
            //     }
            //     if (CommonCodesExists(commonCodes.CodeType, commonCodes.CMCode) != null)
            //     {
            //         throw new Exception("CodeType already exists");
            //     }
            //     await _db.commonCodes.AddRangeAsync(commonCodes);
            // }
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("CodeType was not Added");

            }
        }

        public async Task DeleteRecords(List<CommonCodes> record)
        {
            if (record == null)
            {
                throw new InvalidOperationException("List<CommonCodes> parameter cannot be null");
            }
            _db.commonCodes.RemoveRange(record);
            // foreach (CommonCodes commonCodes in record)
            // {
            //     if (CommonCodesExists(commonCodes.CodeType, commonCodes.CMCode) == null)
            //     {
            //         throw new Exception("CodeType does not exists");
            //     }
            //     _db.commonCodes.RemoveRange(commonCodes);
            // }
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("CodeType was not Deleted");

            }
        }

        private IQueryable<CommonCodes> AddOrderBy(IQueryable<CommonCodes> dbDto, CommonCodesSO dtoSO)
        {
            if (dtoSO.Orderby == null)
            {
                dtoSO.Orderby = "CodeType";
            }
            string sort = Convert.ToString(dtoSO.Orderby);
            dbDto = dbDto.OrderBy(sort);
            return dbDto;
        }

        private IQueryable<CommonCodes> AddOrderBy(IQueryable<CommonCodes> dbDto, CommonCodesSO dtoSO, string defaultOrderByColumn)
        {
            string sort = Convert.ToString(dtoSO.Orderby);
            dbDto = !string.IsNullOrEmpty(sort) ? dbDto.OrderBy(x => dtoSO.Orderby) : dbDto.OrderBy(x => defaultOrderByColumn);

            return dbDto;
        }
        private IQueryable<CommonCodes> AddFilter(IQueryable<CommonCodes> dbDto, CommonCodesSO dtoSO)
        {
            if (dtoSO != null)
            {
                if (dtoSO.CodeTypeArr != null && dtoSO.CodeTypeArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CodeTypeArr.Contains(x.CodeType));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CodeType))
                {
                    dbDto = dbDto.Where(x => x.CodeType.ToUpper().StartsWith(dtoSO.CodeType.ToUpper()));
                }
                if (dtoSO.CMCodeArr != null && dtoSO.CMCodeArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CMCodeArr.Contains(x.CMCode));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CMCode))
                {
                    dbDto = dbDto.Where(x => x.CMCode.ToUpper().StartsWith(dtoSO.CMCode.ToUpper()));
                }
                if (dtoSO.CDDescArr != null && dtoSO.CDDescArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CDDescArr.Contains(x.CDDesc));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CDDesc))
                {
                    dbDto = dbDto.Where(x => x.CDDesc.ToUpper().StartsWith(dtoSO.CDDesc.ToUpper()));
                }
                if (dtoSO.IsSysParamArr != null && dtoSO.IsSysParamArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.IsSysParamArr.Contains(x.IsSysParam));
                }
                else if (!string.IsNullOrEmpty(dtoSO.IsSysParam))
                {
                    dbDto = dbDto.Where(x => x.IsSysParam.ToUpper().StartsWith(dtoSO.IsSysParam.ToUpper()));
                }
                if (dtoSO.IsActiveArr != null && dtoSO.IsActiveArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.IsActiveArr.Contains(x.IsActive));
                }
                else if (!string.IsNullOrEmpty(dtoSO.IsActive))
                {
                    dbDto = dbDto.Where(x => x.IsActive.ToUpper().StartsWith(dtoSO.IsActive.ToUpper()));
                }
                if (dtoSO.CreIDArr != null && dtoSO.CreIDArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CreIDArr.Contains(x.CreID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CreID))
                {
                    dbDto = dbDto.Where(x => x.CreID.ToUpper().StartsWith(dtoSO.CreID.ToUpper()));
                }
                if (dtoSO.ModIDArr != null && dtoSO.ModIDArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.ModIDArr.Contains(x.ModID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.ModID))
                {
                    dbDto = dbDto.Where(x => x.ModID.ToUpper().StartsWith(dtoSO.ModID.ToUpper()));
                }

            }
            return dbDto;
        }
    }
}

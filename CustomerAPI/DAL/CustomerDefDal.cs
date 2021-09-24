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
    // public partial class CustomerDbContext : DbContext
    // {
    //     public virtual DbSet<CustomerDef> CustomerDef {get;set;}

    // }
    public class CustomerDefDal 
    {
        private readonly CustomerDbContext _db;
        public CustomerDefDal(){
            _db = new CustomerDbContext();
        }
        public CustomerDefDal(CustomerDbContext db){
            _db = db;
        }
        public async Task Insert(CustomerDef customerDef){
            if (customerDef == null)
            {
                throw new InvalidOperationException("CustomerDef parameter cannot be null");
            }
            if (customerDef.CustomerID == null)
            {
                throw new Exception("CustomerID cannot be null");
            }
            await _db.CustomerDef.AddAsync(customerDef);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerDefExists(customerDef.CustomerID))
                {
                    throw new Exception("CustomerID already exists");
                }
                else
                {
                    throw new Exception("Customer was not added");
                }
            }
        }

        public async Task Update(CustomerDef customerDef)
        {
            if (customerDef == null) {
                throw new InvalidOperationException("CustomerDef parameter cannot be null");
            }
            _db.CustomerDef.Update(customerDef);
            try
            {
               await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CustomerDefExists(customerDef.CustomerID))
                {
                    throw new Exception("CustomerID does not exists");
                }
                else
                {
                    throw new Exception("Customer was not updated");
                }
            }
        }


        public async Task Delete(string id)
        {
            if (id == null)
            {
                throw new Exception("CustomerID can not be null");
            }
            var customerDef =  await _db.CustomerDef.FindAsync(id);
            Console.WriteLine(customerDef);
            if (customerDef == null)
            {
                throw new Exception("CustomerID is not valid");
            }

            _db.CustomerDef.Remove(customerDef);
            await _db.SaveChangesAsync();
            try
            {
                
            }
            catch (DbUpdateException)
            {
                throw new Exception("Customer was not Deleted");
            }
        }

        public IQueryable<CustomerDef> SearchRecord(CustomerDefSO dtoSO)
        {
            if (dtoSO == null)
            {
                throw new Exception("CustomerDefSO can not be null");
            }
            var dbDto = from c in _db.CustomerDef select c;
            dbDto = AddFilter(dbDto, dtoSO);

            return  dbDto;
        }

        public IQueryable<CustomerDef> SearchPagedRecords(CustomerDefSO dtoSO)
        {
             if (dtoSO == null)
            {
                throw new Exception("CustomerDefSO can not be null");
            }
            if (dtoSO.StartIndex < 0)
            {
                throw new Exception("StartIndex can not be negative");
            }
            if (dtoSO.RecordsPerPage <= 0)
            {
                throw new Exception("Records to be fetched should be greater than 0");
            }
            List<CustomerDef> listDto = new List<CustomerDef>();
            var dbDto = from c in _db.CustomerDef select c;
            dbDto = AddFilter(dbDto, dtoSO);
            dbDto = AddOrderBy(dbDto, dtoSO);
            dbDto = dbDto.Skip(dtoSO.StartIndex).Take(dtoSO.RecordsPerPage);
            return dbDto;
        }

        public async Task<int> GetTotalCount(CustomerDefSO dtoSO)
        {
            if (dtoSO == null)
            {
                throw new Exception("CustomerDefSO can not be null");
            }
            var dbDto = from c in _db.CustomerDef select c;
            dbDto = AddFilter(dbDto, dtoSO);
            return  await dbDto.CountAsync();
        }

        private bool CustomerDefExists(string id)
        {
            if (id == null)
            {
                throw new Exception("ID can not be null");
            }
            return _db.CustomerDef.Any(e => e.CustomerID == id);
        }

        private IQueryable<CustomerDef> AddFilter(IQueryable<CustomerDef> dbDto, CustomerDefSO dtoSO)
        {
            if (dtoSO != null)
            {
                if (dtoSO.CustomerIdArr != null && dtoSO.CustomerIdArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CustomerIdArr.Contains(x.CustomerID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CustomerID))
                {
                    dbDto = dbDto.Where(x => x.CustomerID.ToUpper().StartsWith(dtoSO.CustomerID.ToUpper()));
                }
                if (dtoSO.FirstNameArr != null && dtoSO.FirstNameArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.FirstNameArr.Contains(x.FirstName));
                }
                else if (!string.IsNullOrEmpty(dtoSO.FirstName))
                {
                    dbDto = dbDto.Where(x => x.FirstName.ToUpper().StartsWith(dtoSO.FirstName.ToUpper()));
                }
                if (dtoSO.LastNameArr != null && dtoSO.LastNameArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.LastNameArr.Contains(x.LastName));
                }
                else if (!string.IsNullOrEmpty(dtoSO.LastName))
                {
                    dbDto = dbDto.Where(x => x.LastName.ToUpper().StartsWith(dtoSO.LastName.ToUpper()));
                }
                if (dtoSO.CustomerCDArr != null && dtoSO.CustomerCDArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CustomerCDArr.Contains(x.CustomerCD));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CustomerCD))
                {
                    dbDto = dbDto.Where(x => x.CustomerCD.ToUpper().StartsWith(dtoSO.CustomerCD.ToUpper()));
                }
                if (dtoSO.CountryIdArr != null && dtoSO.CountryIdArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CountryIdArr.Contains(x.CountryID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CountryID))
                {
                    dbDto = dbDto.Where(x => x.CountryID.ToUpper().StartsWith(dtoSO.CountryID.ToUpper()));
                }
                if (dtoSO.SalutationArr != null && dtoSO.SalutationArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.SalutationArr.Contains(x.Salutation));
                }
                else if (!string.IsNullOrEmpty(dtoSO.Salutation))
                {
                    dbDto = dbDto.Where(x => x.Salutation.ToUpper().StartsWith(dtoSO.Salutation.ToUpper()));
                }
                if (dtoSO.CityIdArr != null && dtoSO.CityIdArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CityIdArr.Contains(x.CityID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CityID))
                {
                    dbDto = dbDto.Where(x => x.CityID.ToUpper().StartsWith(dtoSO.CityID.ToUpper()));
                }
                if (dtoSO.CityIdArr != null && dtoSO.CityIdArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CityIdArr.Contains(x.CityID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CityID))
                {
                    dbDto = dbDto.Where(x => x.CityID.ToUpper().StartsWith(dtoSO.CityID.ToUpper()));
                }
                if (dtoSO.ModIdArr != null && dtoSO.ModIdArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.ModIdArr.Contains(x.ModID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.ModID))
                {
                    dbDto = dbDto.Where(x => x.ModID.ToUpper().StartsWith(dtoSO.ModID.ToUpper()));
                }
                if (dtoSO.CreIdArr != null && dtoSO.CreIdArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.CreIdArr.Contains(x.CreID));
                }
                else if (!string.IsNullOrEmpty(dtoSO.CreID))
                {
                    dbDto = dbDto.Where(x => x.CreID.ToUpper().StartsWith(dtoSO.CreID.ToUpper()));
                }
                if (dtoSO.IsActiveArr != null && dtoSO.IsActiveArr.Length > 0)
                {
                    dbDto = dbDto.Where(x => dtoSO.IsActiveArr.Contains(x.IsActive));
                }
                else if (!string.IsNullOrEmpty(dtoSO.IsActive))
                {
                    dbDto = dbDto.Where(x => x.IsActive.ToUpper().StartsWith(dtoSO.IsActive.ToUpper()));
                }

            }
            return dbDto;
        }

        private IQueryable<CustomerDef> AddOrderBy(IQueryable<CustomerDef> dbDto, CustomerDefSO dtoSO)
        {
            if (dtoSO.Orderby == null)
            {
                dtoSO.Orderby = "";
            }
            string sort=Convert.ToString(dtoSO.Orderby);
            dbDto = dbDto.OrderBy(sort);
            return dbDto;
        }

        private IQueryable<CustomerDef> AddOrderBy(IQueryable<CustomerDef> dbDto, CustomerDefSO dtoSO, string defaultOrderByColumn)
        {
            string sort = Convert.ToString(dtoSO.Orderby);
            dbDto = !string.IsNullOrEmpty(sort) ? dbDto.OrderBy(x => dtoSO.Orderby) : dbDto.OrderBy(x => defaultOrderByColumn);

            return dbDto;
        }

        public async Task<CustomerDef> GetRecord(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("CustomerID can not be null or empty");
            }
            var CustomerDef = await _db.CustomerDef.FindAsync(id);

            if (CustomerDef == null)
            {
                throw new Exception("CustomerID is not valid");
            }
            return CustomerDef;
        }
    
        public Dictionary<string, string> GetKeyValues(string keyCol, string valueCol, CustomerDefSO dtoSO)
        {
            if (keyCol == null) {
                throw new InvalidOperationException("keyCol parameter cannot be null");
            }
            if (valueCol == null) {
                throw new InvalidOperationException("valueCol parameter cannot be null");
            }
            if (dtoSO.Orderby == null) dtoSO.Orderby = "";
            Dictionary<string, string> keyValPair = new Dictionary<string, string>();
            var dbDto = from c in _db.CustomerDef select c;
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
    }

}

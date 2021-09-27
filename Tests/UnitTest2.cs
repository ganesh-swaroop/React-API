using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CustomerAPI.Models;
using CustomerAPI.DAL;
using CustomerAPI.Controllers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;



namespace CustomerAPItest
{
    [TestClass]
    public class CommonCodeTest
    {
        private readonly CustomerDbContext _context;
        public CommonCodeTest()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>();
            options.UseSqlServer("Data Source=PRATIPA;Initial Catalog=CustomerDef;Integrated Security=True");
            _context = new CustomerDbContext(options.Options);
        }
        public List<string> SalutelstObj()
        {
            var salute = new List<string>{
                "Mr","Mrs","Miss"
            };
            return salute;
        }
        // public CommonCodes CreateObj()
        // {
        //     var salute = new List<string>{
        //         "Mr","Mrs","Miss"
        //     };
        //     CommonCodes cdo = new CommonCodes();

        //     foreach (var str in salute)
        //     {
        //         cdo.CodeType = "Salutation";
        //         cdo.CMCode = str;
        //         cdo.IsActive = "Y";
        //         cdo.CreID = "HYS87";
        //         cdo.CreTime = DateTime.Now;
        //         cdo.ModTime = DateTime.Now;
        //         cdo.ModID = "GYGS97769";
        //         cdo.IsSysParam = "N";
        //         cdo.CDDesc = "Salutations for person";

        //     }
        // }

        [TestMethod]
        public async Task Insert()
        {
            var salute = new List<string>{
                "Mr","Mrs","Miss"
            };
            CommonCodesController cdc = new CommonCodesController(_context);

            CommonCodes cdo = new CommonCodes();
            foreach (var str in salute)
            {
                cdo.CodeType = "Salutation";
                cdo.CMCode = str;
                cdo.IsActive = "Y";
                cdo.CreID = "HYS87";
                cdo.CreTime = DateTime.Now;
                cdo.ModTime = DateTime.Now;
                cdo.ModID = "GYGS97769";
                cdo.IsSysParam = "N";
                cdo.CDDesc = "Salutations for person";
                var result = await cdc.SaveRecord(cdo,true);

            }
        }

    }
}
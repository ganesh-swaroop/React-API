using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Diagnostics;
using CustomerAPI.Models;
using CustomerAPI.DAL;
using CustomerAPI.Controllers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace CustomerAPItest
{

    [TestClass]
    public class UnitTest1
    {
        private readonly CustomerDbContext _context;
        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>();
            options.UseSqlServer("Data Source=PRATIPA;Initial Catalog=CustomerDef;Integrated Security=True,MinPoolSize = 50;MinPoolSize = 100;");
            _context = new CustomerDbContext(options.Options);
        }

        public CustomerDbContext Context()
        {
            CustomerDbContext _context;
            var options = new DbContextOptionsBuilder<CustomerDbContext>();
            options.UseSqlServer("Data Source=PRATIPA;Initial Catalog=CustomerDef;Integrated Security=True");
            _context = new CustomerDbContext(options.Options);
            return _context;

        }
        //private CustomerDbContext _db;


        // [ClassInitialize]
        // public void setUp(){
        //     _db = new CustomerDbContext();
        // }

        // public UnitTest1(CustomerDbContext db){
        //     _db = db;
        // }

        public string FirstName()
        {
            var random = new Random();
            var f_names = new List<string> {"Maia",
        "Asher",
        "Olivia",
        "Atticus",
        "Amelia",
        "Jack",
        "Charlotte",
        "Theodore",
        "Isla",
        "Oliver",
        "Isabella",
        "Jasper",
        "Cora",
        "Levi",
        "Violet",
        "Arthur",
        "Mia",
        "Thomas",
        "Elizabeth"};

            int index = random.Next(0, f_names.Count);
            return f_names[index];
        }

        public string CountryId()
        {
            var random = new Random();
            var country_id = new List<string> {"IND",
        "AUS",
        "PAK",
        "USA",
        "UK"};

            int index = random.Next(0, country_id.Count);
            return country_id[index];
        }

        public string Address()
        {
            var random = new Random();
            var address = new List<string> {"India",
        "Australia",
        "Pakistan",
        "USA",
        "UK"};

            int index = random.Next(0, address.Count);
            return address[index];
        }

        public string LastName()
        {
            var random = new Random();
            var l_name = new List<string> {"Finch",
        "Smith",
        "Mathew",
        "Peterson",
        "Solomon"};

            int index = random.Next(0, l_name.Count);
            return l_name[index];
        }

        public string MiddleName()
        {
            var random = new Random();
            var m_name = new List<string> {"Richard",
        "Birch",
        "Kevin"};

            int index = random.Next(0, m_name.Count);
            return m_name[index];
        }

        public string Salutation()
        {
            var random = new Random();
            var salute = new List<string> { "Mr", "Ms", "Mrs" };
            int index = random.Next(0, salute.Count);
            return salute[index];
        }
        public List<CustomerDef> CreateRecords(int n)
        {
            DateTime time = DateTime.Now;
            List<CustomerDef> customerList = new List<CustomerDef>();
            for (int i = 1; i <= n; i++)
            {
                var dto = new CustomerDef() { CustomerID = i.ToString(), CustomerCD = "abc", Salutation = Salutation(), FirstName = FirstName(), MiddleName = MiddleName(), LastName = LastName(), ShortName = "john", DateOfBirth = "05/08/1979", AddrLine1 = Address(), AddrLine2 = Address(), AddrLine3 = Address(), CountryID = CountryId(), CityID = "MGL877", StateID = "GYF7678", PostalCode = "653422", MobileNumber = "8762534162", PhoneNumber = "7625362534", FaxNumber = "GHST7t566", Email = "johnpaul@mail.com", GooglePlus = "Jhn_plu", Facebook = "paul_jh", Twitter = "twt_jhn", SPID = "76", AlertChannel = "uhash", IsActive = "Yes", IsSysCust = "No", CegidDataPost = "YGHBJKBKBB", CreID = "786YU", CreTime = time, ModID = "765", ModTime = time };
                customerList.Add(dto);
            }

            return customerList;

        }
        // [TestMethod]
        // public async Task InsertBulk(){
        //     for(int i=0 ; i < 100 ; i++)
        //     var customerList
        // }

        [TestMethod]
        public async Task Insert()
        {
            Stopwatch sw = new Stopwatch();
            var customerList = CreateRecords(1000);
            CustomerDefController custobj;
            var custdal = new CustomerDefDal(Context());
            sw.Start();
            //await custdal.InsertRecords(customerList);            

            foreach (var customer in customerList)
            {
                custobj = new CustomerDefController(Context());
                sw.Start();
                await custobj.SaveRecord(customer, true);
            }
            sw.Stop();
            using StreamWriter stream = new("D:/WEB/React-API/Tests/Insert_Performance.txt", append: true);
            await stream.WriteLineAsync("Time elapsed for inserting 1000 records is , " + sw.Elapsed);


        }


        [TestMethod]
        public async Task Update()
        {
            Stopwatch sw = new Stopwatch();
            var customerList = CreateRecords(100000);

            foreach (var customer in customerList)
            {
                var custobj = new CustomerDefController(Context());
                sw.Start();
                await custobj.SaveRecord(customer, true);
            }
            sw.Stop();
            using StreamWriter stream = new("D:/WEB/React-API/Tests/Update_Performance.txt", append: false);
            await stream.WriteLineAsync("Time elapsed for Updating 1LK records is (Test1) , " + sw.Elapsed);

        }
        [TestMethod]
        public void CreateCommonCodeSO(){
            CommonCodesSO dtoSO = new CommonCodesSO{};
            Console.WriteLine(JsonConvert.SerializeObject(dtoSO));

        }
        [TestMethod]
        public void CreateCustomerDefSO(){
            CustomerDefSO dtoSO = new CustomerDefSO { CustomerID = "1"};
            Console.WriteLine(JsonConvert.SerializeObject(dtoSO));
        }

        [TestMethod]
        public async Task Select()
        {
            var custobj = new CustomerDefController(_context);
            var result = await custobj.GetRecord("2");
        } 
        [TestMethod]
        public async Task Delete()
        {
            Stopwatch sw = new Stopwatch();

            CustomerDefController custobj;
            for (int i = 1; i <= 100000; i++)
            {
                // if (i % 100 == 0)
                // {
                //     custobj = new CustomerDefController(Context());
                // }
                custobj = new CustomerDefController(Context());
                sw.Start();
                await custobj.RemoveRecord(i.ToString());
            }
            sw.Stop();
            using StreamWriter stream = new("D:/WEB/React-API/Tests/Delete_Performance.txt", append: true);
            await stream.WriteLineAsync("Time elapsed for Deleting 1LK records is (Test2) , " + sw.Elapsed);

        }

        [TestMethod]
        public async Task InsertCommon()
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
                var result = await cdc.SaveRecord(cdo, true);

            }


        }
        // [TestMethod]
        // public async Task SelectCommon()
        // {


        // }


    }
}


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
    public class UnitTest1
    {
        //private CustomerDbContext _db;


        // [ClassInitialize]
        // public void setUp(){
        //     _db = new CustomerDbContext();
        // }

        // public UnitTest1(CustomerDbContext db){
        //     _db = db;
        // }

        public string FirstName(){
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

        int index = random.Next(0,f_names.Count);
        return f_names[index];
        }

        public string CountryId(){
            var random = new Random();
            var country_id = new List<string> {"IND",
        "AUS",
        "PAK",
        "USA",
        "UK"};

        int index = random.Next(0,country_id.Count);
        return country_id[index];
        }

        public string Address(){
            var random = new Random();
            var address = new List<string> {"India",
        "Australia",
        "Pakistan",
        "USA",
        "UK"};

        int index = random.Next(0,address.Count);
        return address[index];
        }

        public string LastName(){
            var random = new Random();
            var l_name = new List<string> {"Finch",
        "Smith",
        "Mathew",
        "Peterson",
        "Solomon"};

        int index = random.Next(0,l_name.Count);
        return l_name[index];
        }        

        public string MiddleName(){
            var random = new Random();
            var m_name = new List<string> {"Richard",
        "Birch",
        "Kevin"};

        int index = random.Next(0,m_name.Count);
        return m_name[index];
        } 

        public string Salutation(){
            var random = new Random();
            var salute = new List<string> {"Mr", "Ms", "Mrs"};
        int index = random.Next(0,salute.Count);
        return salute[index];
        }  
        public CustomerDef CreateRecord(int i)
        {

                DateTime time = DateTime.Now;
                var dto = new CustomerDef() {CustomerID = i.ToString(),CustomerCD = "abc",Salutation=Salutation(),FirstName=FirstName(),MiddleName=MiddleName(),LastName=LastName(),ShortName="john",DateOfBirth="05/08/1979",AddrLine1=Address(),AddrLine2=Address(),AddrLine3=Address(),CountryID=CountryId(),CityID="MGL877",StateID="GYF7678",PostalCode="653422",MobileNumber="8762534162",PhoneNumber="7625362534",FaxNumber="GHST7t566",Email="johnpaul@mail.com",GooglePlus="Jhn_plu",Facebook="paul_jh",Twitter="twt_jhn",SPID="76",AlertChannel="uhash",IsActive="Yes",IsSysCust="No",CegidDataPost="YGHBJKBKBB",CreID="786YU",CreTime=time,ModID="765",ModTime=time};
            

            return dto;

        }

        [TestMethod]
        public async Task Insert()
        {
            
                for(int i=1;i<=100000;i++){
                var custobj = new CustomerDefController();
                    var dto = CreateRecord(i);
                    await custobj.SaveRecord(dto,true);
                }
            
        }

        [TestMethod]
        public async Task  Update()
        {
            DateTime time = DateTime.Now;

             var i = 100;
                var custobj = new CustomerDefController();
                var dto = new CustomerDef() {CustomerID = i.ToString(),CustomerCD = "efj",Salutation="Mr",FirstName="Johny",MiddleName="pauly",LastName="rose",ShortName="john_rs",DateOfBirth="05/08/1979",AddrLine1="#208 87th street",AddrLine2="5th block",AddrLine3="Mangalore",CountryID="AUS923",CityID="MGL877",PostalCode="653422",MobileNumber="8762534162",PhoneNumber="7625362534",FaxNumber="GHST7t566",Email="johnpaul@mail.com",GooglePlus="Jhn_plu",Facebook="paul_jh",Twitter="twt_jhn",SPID="76",AlertChannel="uhash",IsActive="Yes",IsSysCust="No",CegidDataPost="YGHBJKBKBB",CreID="786YU",CreTime=time,ModID="765",ModTime=time};
                await custobj.SaveRecord(dto,false);
            
        }

        [TestMethod]
        public async Task Select()
        {
            var custobj = new CustomerDefDal();
            var result = await custobj.GetRecord("2");
        }
        [TestMethod]
        public async Task Delete()
        {
            var custobj = new CustomerDefController();
            await custobj.RemoveRecord("1");
        }
    }
}


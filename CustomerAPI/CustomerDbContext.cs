using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CustomerAPI.DAL
{
    public partial class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        {

        }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if (!optionsBuilder.IsConfigured)
            // {
            //     optionsBuilder.UseSqlServer("Data Source=PRATIPA;Initial Catalog=CustomerDef;Integrated Security=True");
            // }
        }
        
        


    }
}

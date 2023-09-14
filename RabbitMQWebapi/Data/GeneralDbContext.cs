using Microsoft.EntityFrameworkCore;
using RabbitMQWebapi.Models.Configurations;
using RabbitMQWebapi.Models.ControllerModels;
using System;

namespace RabbitMQDemo.Data
{
    public class GeneralDbContext : DbContext
    {
        public GeneralDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbContextOptions<DbContext> GetAllOptions()
        {
            DbContextOptionsBuilder<DbContext> optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer("Data Source=ENES;Initial Catalog=RabbitMQ;User Id=sa;Password=sas");

            return optionsBuilder.Options;
        }
    }
}

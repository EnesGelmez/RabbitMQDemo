using Microsoft.EntityFrameworkCore;
using RabbitMQDemo.Data;
using RabbitMQWebapi.Models.Configurations;
using System;

namespace RabbitMQWebapi.Background
{
    public class DbHelper
    {
        private readonly IGeneralConfig _generalConfig;
        public DbHelper(IGeneralConfig generalConfig)
        {
            _generalConfig = generalConfig;

        }
        private GeneralDbContext _dbContext;

        public DbContextOptions<GeneralDbContext> GetAllOptions()
        {
            DbContextOptionsBuilder<GeneralDbContext> optionsBuilder = new DbContextOptionsBuilder<GeneralDbContext>();

            optionsBuilder.UseSqlServer(_generalConfig.SqlConnectionString);

            return optionsBuilder.Options;
        }


    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkExercice.Models;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkExercice
{
    public class AppDbContext : DbContext
    {
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;userid=root;password=1234567;database=entityFramewordTest");

            // Criar log no console das consultas...
            optionsBuilder.EnableSensitiveDataLogging(true).
            UseLoggerFactory(new LoggerFactory().AddConsole((category, level) =>
                level == LogLevel.Information &&
                category == DbLoggerCategory.Database.Command.Name, true));
        }

    }
}

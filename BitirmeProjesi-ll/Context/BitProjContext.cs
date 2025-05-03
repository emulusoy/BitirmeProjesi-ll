using BitirmeProjesi_ll.Entities;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi_ll.Context
{
    public class BitProjContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MULUSOY\\SQLEXPRESS01;Initial Catalog=BitirmeProjeDB;integrated Security=true; trust server certificate=true");

        }
        public DbSet<Harcamalar> Harcamalars { get; set; }
    }
}

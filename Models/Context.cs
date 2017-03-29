using Ex1.Models;
using Ex1.Models.AccountViewModels;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class GjesterDbContext : DbContext
    {
        //Konstruktøren
        public GjesterDbContext(DbContextOptions<GjesterDbContext> options) 
        : base(options)
        {}
        public DbSet<Gjest> Gjester { get; set;}    
    }
}


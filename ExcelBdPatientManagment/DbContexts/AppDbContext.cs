using Common.Models;
using Common.Models.DbSet;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// The dataset for the tables.
        /// </summary>

        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
        public DbSet<Allergies> Allergies => Set<Allergies>();
        public DbSet<AllergiesDetails> AllergiesDetails => Set<AllergiesDetails>();
        public DbSet<DiseaseInformation> DiseaseInformation => Set<DiseaseInformation>();
        public DbSet<NCD> NCD => Set<NCD>();
        public DbSet<NCDDetails> NCDDetails => Set<NCDDetails>();
        public DbSet<Patients> Patients => Set<Patients>();


        /// <summary>
        /// Do any database initialization required.
        /// </summary>
        /// <returns>A task that completes when the database is initialized</returns>
        public async Task InitializeDatabaseAsync()
        {
            await Database.EnsureCreatedAsync();
        }
    }

}

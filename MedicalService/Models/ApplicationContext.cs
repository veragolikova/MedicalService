using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MedicalService.Models.SubModels;

namespace MedicalService.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Event> Events { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }

        public ApplicationContext()
        {}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MedicalService;Trusted_Connection=True;");

    }
}

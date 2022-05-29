using System; 
using System.Linq;
using System.Threading.Tasks;

using Linkedin.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Student.Data.Context
{

    public class MyDataBase : DbContext
    {
      
        public MyDataBase(DbContextOptions<MyDataBase> options, bool testMode) : base(options)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Visit> Visits { get; set; }
        //

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
            modelBuilder.Entity<Request>().ToTable("Request");
            modelBuilder.Entity<Visit>().ToTable("Visit");

            //modelBuilder.Seed();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
                //mkh
                // optionsBuilder.UseSqlServer("Data Source=185.97.118.69,1433;Initial Catalog=HAFTAD_CF7;User Id=sa;Password=1qazZAQ!@WSX;MultipleActiveResultSets=True");
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; UID=sa; Password=ABCabc123456;Database=Linkdin;");
           
            //optionsBuilder  .UseSqlServer(_AppSetting.Value.ConnectionString, providerOptions => providerOptions.CommandTimeout(60));
        } 

    }

}
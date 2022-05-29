using System;
using System.Data.Entity;
using System.Linq;
using Student.Models;

namespace Student.Data.Context
{
    
    public class MyDataBase : DbContext
    {
        // Your context has been configured to use a 'MyDataBase' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Student.Models.MyDataBase' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyDataBase' 
        // connection string in the application configuration file.
        public MyDataBase()

        : base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Students;Integrated Security=True")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<STStudent> STStudents { get; set; }
        public DbSet<STStudentCourse> STStudentCourses { get; set; }
        //

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
           modelBuilder.Entity<STStudent>().ToTable("STStudent");
            modelBuilder.Entity<STStudentCourse>().ToTable("STStudentCourse");

            base.OnModelCreating(modelBuilder);
        }

        
    }

    
}
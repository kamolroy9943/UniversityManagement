using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProgrammer.UniversityManagement.Core.Entities;
using TeamProgrammer.UniversityManagement.Core.Entities.ViewModels;

namespace TeamProgrammer.UniversityManagement.DataRepository.Context
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(): base("UniversityDbConnection") {}

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Designation> Designations { get; set; }

        public DbSet<StudentDeptSemester> StudentDeptSemesters { get; set; }
        public DbSet<CourseDeptSemester> CourseDeptSemesters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentDeptSemester());
            modelBuilder.Configurations.Add(new CourseDeptSemester());
        }
    }
}
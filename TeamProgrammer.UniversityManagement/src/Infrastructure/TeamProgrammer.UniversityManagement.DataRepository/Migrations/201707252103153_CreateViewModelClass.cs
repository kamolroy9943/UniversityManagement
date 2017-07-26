namespace TeamProgrammer.UniversityManagement.DataRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateViewModelClass : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Course_Dept_Semester",
            //    c => new
            //        {
            //            CourseId = c.Int(nullable: false, identity: true),
            //            CourseName = c.String(),
            //            DeptName = c.String(),
            //            SemesterName = c.String(),
            //        })
            //    .PrimaryKey(t => t.CourseId);
            
            //CreateTable(
            //    "dbo.Student_Dept_Semester",
            //    c => new
            //        {
            //            StudentId = c.Int(nullable: false, identity: true),
            //            StudentName = c.String(),
            //            Registration = c.String(),
            //            DeptName = c.String(),
            //            SemesterName = c.String(),
            //        })
            //    .PrimaryKey(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Student_Dept_Semester");
            DropTable("dbo.Course_Dept_Semester");
        }
    }
}

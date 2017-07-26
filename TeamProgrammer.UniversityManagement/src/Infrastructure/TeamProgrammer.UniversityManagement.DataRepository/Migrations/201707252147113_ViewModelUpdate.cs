namespace TeamProgrammer.UniversityManagement.DataRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViewModelUpdate : DbMigration
    {
        public override void Up()
        {
        //    AddColumn("dbo.Course_Dept_Semester", "CourseCode", c => c.String());
        //    AddColumn("dbo.Course_Dept_Semester", "Credit", c => c.Double(nullable: false));
        //    AddColumn("dbo.Course_Dept_Semester", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Course_Dept_Semester", "Description");
            DropColumn("dbo.Course_Dept_Semester", "Credit");
            DropColumn("dbo.Course_Dept_Semester", "CourseCode");
        }
    }
}

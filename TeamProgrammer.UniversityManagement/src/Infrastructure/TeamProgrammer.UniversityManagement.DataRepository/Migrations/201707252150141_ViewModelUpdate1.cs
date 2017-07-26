namespace TeamProgrammer.UniversityManagement.DataRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViewModelUpdate1 : DbMigration
    {
        public override void Up()
        {
        //    AddColumn("dbo.Course_Dept_Semester", "Discription", c => c.String());
        //    DropColumn("dbo.Course_Dept_Semester", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Course_Dept_Semester", "Description", c => c.String());
            DropColumn("dbo.Course_Dept_Semester", "Discription");
        }
    }
}

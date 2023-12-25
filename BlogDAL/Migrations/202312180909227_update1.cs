namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminInfoes",
                c => new
                    {
                        AdminInfoId = c.Int(nullable: false, identity: true),
                        EmailId = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AdminInfoId);
            
            CreateTable(
                "dbo.BlogInfoes",
                c => new
                    {
                        BlogInfoId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Subject = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        BlogUrl = c.String(),
                        Employee_EmpInfoId = c.Int(),
                    })
                .PrimaryKey(t => t.BlogInfoId)
                .ForeignKey("dbo.EmpInfoes", t => t.Employee_EmpInfoId)
                .Index(t => t.Employee_EmpInfoId);
            
            CreateTable(
                "dbo.EmpInfoes",
                c => new
                    {
                        EmpInfoId = c.Int(nullable: false, identity: true),
                        EmailId = c.String(maxLength: 255),
                        Name = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        PassCode = c.String(),
                    })
                .PrimaryKey(t => t.EmpInfoId)
                .Index(t => t.EmailId, unique: true, name: "UQ_EmailId");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogInfoes", "Employee_EmpInfoId", "dbo.EmpInfoes");
            DropIndex("dbo.EmpInfoes", "UQ_EmailId");
            DropIndex("dbo.BlogInfoes", new[] { "Employee_EmpInfoId" });
            DropTable("dbo.EmpInfoes");
            DropTable("dbo.BlogInfoes");
            DropTable("dbo.AdminInfoes");
        }
    }
}

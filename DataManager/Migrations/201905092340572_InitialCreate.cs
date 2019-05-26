namespace DataManagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.QueueOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QueueInfoId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.QueueInfoes", t => t.QueueInfoId, cascadeDelete: true)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.QueueInfoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "public.QueueInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Link = c.String(nullable: false),
                        Timer = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.UserAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QueueInfoId = c.Int(nullable: false),
                        Nickname = c.String(nullable: false, maxLength: 32),
                        AccessTypeName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.QueueInfoes", t => t.QueueInfoId, cascadeDelete: true)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.QueueInfoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "public.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 32),
                        PasswordHash = c.String(),
                        LastActivity = c.DateTime(nullable: false),
                        IsTemporary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.UserTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.UserAccesses", "UserId", "public.Users");
            DropForeignKey("public.UserTokens", "UserId", "public.Users");
            DropForeignKey("public.QueueOrders", "UserId", "public.Users");
            DropForeignKey("public.UserAccesses", "QueueInfoId", "public.QueueInfoes");
            DropForeignKey("public.QueueOrders", "QueueInfoId", "public.QueueInfoes");
            DropIndex("public.UserAccesses", new[] { "UserId" });
            DropIndex("public.UserTokens", new[] { "UserId" });
            DropIndex("public.QueueOrders", new[] { "UserId" });
            DropIndex("public.UserAccesses", new[] { "QueueInfoId" });
            DropIndex("public.QueueOrders", new[] { "QueueInfoId" });
            DropTable("public.UserTokens");
            DropTable("public.Users");
            DropTable("public.UserAccesses");
            DropTable("public.QueueInfoes");
            DropTable("public.QueueOrders");
        }
    }
}

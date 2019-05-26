namespace DataManagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableTimer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.QueueInfoes", "Timer", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("public.QueueInfoes", "Timer", c => c.DateTime(nullable: false));
        }
    }
}

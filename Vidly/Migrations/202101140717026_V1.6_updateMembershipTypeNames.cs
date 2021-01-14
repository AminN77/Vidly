namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V16_updateMembershipTypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Type1'  WHERE Id=1 ");
            Sql("UPDATE MembershipTypes SET Name = 'Type2'  WHERE Id=2 ");
            Sql("UPDATE MembershipTypes SET Name = 'Type3'  WHERE Id=3 ");
            Sql("UPDATE MembershipTypes SET Name = 'Type4'  WHERE Id=4 ");
        }
        
        public override void Down()
        {
        }
    }
}

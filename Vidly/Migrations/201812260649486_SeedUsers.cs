namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'51b87628-8fd8-4cb3-a234-184a2aa5c1e5', N'admin', N'AHWCZ/T6NjRhGBygFhp+Rp46TL1yNIblhO7g1QlHAE9kNa28oUdEGanJb6i12q7SXA==', N'1ab0f5fb-5f4a-4f0c-be7a-06e7b584f1e3', N'ApplicationUser')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'acb175ee-a53c-4564-aef6-a3836dbce0f2', N'guest', N'AHiVCiBYixO6KRWT0XAujsmMSr67rmkC4B4xcMzmvgopFl/F3XerLQ2R5svvRLgHqA==', N'03c2cde6-b9cb-49e3-b3fd-bfaa2e9e038c', N'ApplicationUser')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8365ea35-1e2a-4a87-a3bd-6dbf7ce23df6', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'51b87628-8fd8-4cb3-a234-184a2aa5c1e5', N'8365ea35-1e2a-4a87-a3bd-6dbf7ce23df6')             
            ");
        }
        
        public override void Down()
        {
        }
    }
}

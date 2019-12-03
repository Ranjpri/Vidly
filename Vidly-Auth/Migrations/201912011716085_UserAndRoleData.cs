namespace Vidly_Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAndRoleData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2e53d892-90fa-4b2a-a101-b38bb6578f5e', N'admin@vidly.com', 0, N'ACbHfLkxBh/sjVNW+pwIk+7lYTMVPCcKqiw8g8GHzmzpWlDDqCR/ISHBMI/yCnyLlg==', N'33909e67-b5a5-4aef-9b4e-7bf51f5803dc', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9f913f9b-dd86-4e66-816c-453a3959ad89', N'guest@vidly.com', 0, N'AElwbpF0KwSt6QD4PIxBPVJFoWMlm3jwHo7NiUBH1FuLuwbT+/KmastAzddx62Ol6w==', N'1ba9577e-5049-4b5b-a031-f46c2b0c3a2f', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')


INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f847f1d3-fa5c-43e6-84b2-758ce651c8ce', N'CanManageMovies')


INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2e53d892-90fa-4b2a-a101-b38bb6578f5e', N'f847f1d3-fa5c-43e6-84b2-758ce651c8ce')

");
        }
        
        public override void Down()
        {
        }
    }
}

namespace ProjectQT.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectQT.DataModel.Models.QTShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ProjectQT.DataModel.Models.QTShopDbContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProjectQT.DataModel.Models.QTShopDbContext context)
        {
            base.Seed(context);

            context.Roles.AddOrUpdate(x => x.RoleId,
            new Models.Role() { RoleId = "Detail", RoleName = "Detail", CreateAt = DateTime.Now, CreateBy = 1, Status = true },
            new Models.Role() { RoleId = "Add New", RoleName = "Add New", CreateAt = DateTime.Now, CreateBy = 1, Status = true },
            new Models.Role() { RoleId = "Update", RoleName = "Update", CreateAt = DateTime.Now, CreateBy = 1, Status = true },
            new Models.Role() { RoleId = "Delete", RoleName = "Delete", CreateAt = DateTime.Now, CreateBy = 1, Status = true }

            );

            context.Businesses.AddOrUpdate(x => x.BusinessId,
            new Models.Business() { BusinessId = "Attributes", BusinessName = "Attributes", Status = true },
            new Models.Business() { BusinessId = "Business", BusinessName = "Business", Status = true },
            new Models.Business() { BusinessId = "Categories", BusinessName = "Categories", Status = true },
            new Models.Business() { BusinessId = "GroupRoles", BusinessName = "GroupRoles", Status = true },
            new Models.Business() { BusinessId = "Groups", BusinessName = "Groups", Status = true },
            new Models.Business() { BusinessId = "Home", BusinessName = "Home", Status = true },
            new Models.Business() { BusinessId = "Products", BusinessName = "Products", Status = true },
            new Models.Business() { BusinessId = "TypeAttribute", BusinessName = "TypeAttribute", Status = true },
            new Models.Business() { BusinessId = "User", BusinessName = "User", Status = true }

            );

            //context.GroupRoles.AddOrUpdate(x => x.GroupId,
            //new Models.GroupRole() { GroupId = 1, RoleId = "Detail", BusinessId = "Business" },
            //new Models.GroupRole() { GroupId = 1, RoleId = "Add New", BusinessId = "Business" },
            //new Models.GroupRole() { GroupId = 1, RoleId = "Update", BusinessId = "Business" },
            //new Models.GroupRole() { GroupId = 1, RoleId = "Delete", BusinessId = "Business" }

            //);

            //context.Groups.AddOrUpdate(x => x.GroupId,
            //new Models.Group() { GroupId = 1, GroupName = "Admin", CreateAt = DateTime.Now, CreateBy = 1, Status = true },
            //new Models.Group() { GroupId = 2, GroupName = "User", CreateAt = DateTime.Now, CreateBy = 1, Status = true },
            //new Models.Group() { GroupId = 3, GroupName = "Customer", CreateAt = DateTime.Now, CreateBy = 1, Status = true }
            //);

            //context.Users.AddOrUpdate(x => x.Id,
            //new Models.User() { Id = 1, Email = "adminQTShop@gmail.com", Password = "11", FullName = "adminQTShop",
            //    BirthDay = DateTime.Parse("1999-1-1"), PhoneNumber = "0365989676", Address = "Vinh phuc",
            //    UpdateAt = DateTime.Now, CreateAt = DateTime.Now, Status = true, GroupId = 1 }
            //);
             context.SaveChanges();
        }
    }
}

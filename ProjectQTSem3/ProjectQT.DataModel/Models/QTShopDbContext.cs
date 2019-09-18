namespace ProjectQT.DataModel.Models
{
    using System.Data.Entity;

    public class QTShopDbContext : DbContext
    {
        public QTShopDbContext()
            : base("name=QTShopDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QTShopDbContext, Migrations.Configuration>("QTShopDbContext"));
        }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<GroupRole> GroupRoles { get; set; }
        public virtual DbSet<TypeAttribute> TypeAttributes { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }


    }
}
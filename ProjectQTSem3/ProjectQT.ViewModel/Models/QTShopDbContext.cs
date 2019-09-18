namespace ProjectQT.DataModel.Models
{
    using System.Data.Entity;

    public class QTShopDbContext : DbContext
    {
        public QTShopDbContext()
            : base("name=QTShopDbContext")
        {}
        public DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
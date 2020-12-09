namespace SKAutoNew.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;

    public class SKAutoDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public SKAutoDbContext(DbContextOptions<SKAutoDbContext> options) : base(options){}

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<ModelCategories> ModelsCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Manufactory> Manufactories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<UseFullCategory> UseFullCategories { get; set; }

        public DbSet<TownUseFullCategory> TownUseFullCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ModelCategories>()
                .HasKey(mc => new { mc.CategoryId, mc.ModelId });

            builder.Entity<ModelCategories>()
                .HasOne(c => c.Category)
                .WithMany(mc => mc.ModelCategories)
                .HasForeignKey(c => c.CategoryId);

            builder.Entity<ModelCategories>()
                .HasOne(m => m.Model)
                .WithMany(mc => mc.ModelCategories)
                .HasForeignKey(m => m.ModelId);

            builder.Entity<TownUseFullCategory>()
                .HasKey(tu => new { tu.TownId, tu.UseFullCategoryId });

            builder.Entity<TownUseFullCategory>()
                .HasOne(t => t.Town)
                .WithMany(uc => uc.UseFullCategories)
                .HasForeignKey(t => t.TownId);

            builder.Entity<TownUseFullCategory>()
                .HasOne(uc => uc.UseFullCategory)
                .WithMany(t => t.Towns)
                .HasForeignKey(uc => uc.UseFullCategoryId);

            //builder.Entity<Part>()
            //    .HasOne(b => b.Brand)
            //    .WithMany(p => p.Parts)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Part>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Parts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Part>()
                .HasOne(m => m.Model)
                .WithMany(p => p.Parts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Part>()
                .HasOne(m => m.Manufactory)
                .WithMany(p => p.Parts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Part>()
                .Property(p => p.InComePrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(builder);
        }
    }
}

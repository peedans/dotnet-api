using app.Models.Master;
using dotnetapi.Models.User;
using Microsoft.EntityFrameworkCore;
// using dotnetapi.Models.Context;
using NTR.Common.DataAccess.Context;
using NTR.Common.DataAccess.Entities;
using ActionModel = dotnetapi.Models.User.Action;
using UserModel = dotnetapi.Models.User.User;

namespace dotnetapi.Models.Master
{
    public partial class DBMasterContext : EntityContextBase<DBMasterContext>
    {
        private readonly IConfiguration configuration;

        public DbSet<BlogPost>? BlogPost { get; set; }

        public DbSet<BlogPostTag>? BlogPostTag { get; set; }

        public DbSet<Tag>? Tag { get; set; }

        public DbSet<Author>? Author { get; set; }

        public DbSet<ActionModel>? Action { get; set; }

        public DbSet<Company>? Company { get; set; }

        public DbSet<Department>? Department { get; set; }

        public DbSet<Role>? Role { get; set; }

        public DbSet<RoleAction>? RoleAction { get; set; }

        public DbSet<Salary>? Salary { get; set; }

        public DbSet<SalaryHistory>? SalaryHistory { get; set; }

        public DbSet<UserModel>? User { get; set; }

        public DbSet<UserCompany>? UserCompany { get; set; }

        public DbSet<Category>? Category { get; set; }

        public DbSet<BlogPostCategory>? BlogPostCategory { get; set; }

        public DBMasterContext(DbContextOptions<DBMasterContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPostTag>()
               .HasKey(x => new { x.BlogPostId, x.TagId });
            modelBuilder.Entity<RoleAction>()
                .HasKey(x => new { x.RoleId, x.ActionId });
            modelBuilder.Entity<UserCompany>()
                .HasKey(uc => new { uc.UserId, uc.CompanyId });
            modelBuilder.Entity<BlogPostCategory>()
                .HasKey(uc => new { uc.BlogPostId, uc.CategoryId });

            // modelBuilder.Entity<BlogPost>()
            //     .HasMany(x => x.Tags)
            //     .WithMany(y => y.BlogPosts)
            //     .UsingEntity(j => { j.ToTable("BlogPostTags");
            //         j.Property<long>("BlogPostId").HasColumnName("blog_post_id");
            //         j.Property<long>("TagsId").HasColumnName("tag_id");
            //         j.Property<DateTime>("created_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            //         j.Property<DateTime>("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            //         j.Property<bool?>("is_delete").HasDefaultValue(false);
            //         });

        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).updated_date = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).created_date = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                var auditableEntity = insertedEntry as EntityBase;
                if (auditableEntity != null)
                {
                    auditableEntity.created_date = DateTime.Now;
                }
            }

            var modifiedEntries = this.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                var auditableEntity = modifiedEntry as EntityBase;
                if (auditableEntity != null)
                {
                    auditableEntity.updated_date = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}


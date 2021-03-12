using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookExchanger.Application
{
    public partial class BookExchangerContext : DbContext
    {
        public BookExchangerContext()
        {
        }

        public BookExchangerContext(DbContextOptions<BookExchangerContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<Book> Books { get; set; }
        //public virtual DbSet<Category> Categories { get; set; }
        //public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=47.100.3.215;Database=BookExchanger;Username=postgres;Password=CLANNADlilium123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("book_name");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Creator)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("creator");

                entity.Property(e => e.Level)
                    .HasMaxLength(30)
                    .HasColumnName("level");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("date")
                    .HasColumnName("modified_at");

                entity.Property(e => e.PublishedAt)
                    .HasColumnType("date")
                    .HasColumnName("published_at");

                entity.Property(e => e.Tags)
                    .HasMaxLength(255)
                    .HasColumnName("tags");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_category_id");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_id");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("category_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasMaxLength(64)
                    .HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .HasColumnName("country");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Degree)
                    .HasMaxLength(255)
                    .HasColumnName("degree");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.District)
                    .HasMaxLength(30)
                    .HasColumnName("district");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.LastSignAt)
                    .HasColumnType("date")
                    .HasColumnName("last_sign_at");

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .HasColumnName("nick_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Province)
                    .HasMaxLength(30)
                    .HasColumnName("province");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("salt");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("user_pass");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
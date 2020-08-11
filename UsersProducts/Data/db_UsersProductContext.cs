using Microsoft.EntityFrameworkCore;
using UsersProducts.Models;


namespace UsersProducts.Models
{
    public partial class db_UsersProductContext : DbContext
    {
        public db_UsersProductContext()
        {
        }

        public db_UsersProductContext(DbContextOptions<db_UsersProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUserRoles> Tbl_UserRoles { get; set; }
        public virtual DbSet<TblUsers> Tbl_Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer("Server=DESKTOP-NDN7SS5\\SQLEXPRESS; Database=db_UsersProduct;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            modelBuilder.Entity<TblUserRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("tbl_USER_ROLES");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.ToTable("tbl_USERS");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserRoleId).HasColumnName("User_Role_Id");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_USERS_tbl_USER_ROLES");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}

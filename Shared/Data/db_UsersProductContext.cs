﻿using Microsoft.EntityFrameworkCore;
using Shared.Models;


namespace Shared.Models
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

        public virtual DbSet<TblProductCategory> Tbl_ProductCategory { get; set; }
        public virtual DbSet<TblProducts> Tbl_Products { get; set; } 

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer("Server=DESKTOP-NDN7SS5\\SQLEXPRESS; Database=db_UsersProduct;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("tbl_PRODUCT_CATEGORY");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblProducts>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tbl_PRODUCTS");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProCatId).HasColumnName("pro_cat_id");

                entity.Property(e => e.ProductImage)
                    .IsRequired()
                    .HasColumnName("product_image")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.ProCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_PRODUCTS_tbl_PRODUCT_CATEGORY");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}

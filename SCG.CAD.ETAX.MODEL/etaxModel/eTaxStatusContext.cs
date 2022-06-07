using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class eTaxStatusContext : DbContext
    {
        public eTaxStatusContext()
        {
        }

        public eTaxStatusContext(DbContextOptions<eTaxStatusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProfileUserManagement> ProfileUserManagements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SCCBSTAXQDB1;Database=eTaxStatus;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileUserManagement>(entity =>
            {
                entity.HasKey(e => e.UserNo)
                    .HasName("PK_authenUserProfile");

                entity.ToTable("profileUserManagement");

                entity.Property(e => e.UserNo).HasColumnName("userNo");

                entity.Property(e => e.AccountStatus)
                    .HasMaxLength(10)
                    .HasColumnName("accountStatus");

                entity.Property(e => e.AttempLast)
                    .HasColumnType("datetime")
                    .HasColumnName("attempLast");

                entity.Property(e => e.AttempLogin)
                    .HasMaxLength(150)
                    .HasColumnName("attempLogin");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.DomainName)
                    .HasMaxLength(150)
                    .HasColumnName("domainName");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .HasColumnName("firstName");

                entity.Property(e => e.GroupId).HasMaxLength(150);

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .HasColumnName("lastName");

                entity.Property(e => e.PasswordExpire)
                    .HasColumnType("datetime")
                    .HasColumnName("passwordExpire");

                entity.Property(e => e.PasswordRegister)
                    .HasColumnType("datetime")
                    .HasColumnName("passwordRegister");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(150)
                    .HasColumnName("userEmail");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(150)
                    .HasColumnName("userPassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

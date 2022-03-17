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

        public virtual DbSet<ProfileBranch> ProfileBranches { get; set; } = null!;

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
            modelBuilder.Entity<ProfileBranch>(entity =>
            {
                entity.HasKey(e => e.ProfileBranchNo);

                entity.ToTable("profileBranch");

                entity.Property(e => e.ProfileBranchNo).HasColumnName("profileBranchNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.ProfileBranchCode)
                    .HasMaxLength(50)
                    .HasColumnName("profileBranchCode");

                entity.Property(e => e.ProfileBranchDescrition)
                    .HasMaxLength(500)
                    .HasColumnName("profileBranchDescrition");

                entity.Property(e => e.ProfileBranchNameEn)
                    .HasMaxLength(500)
                    .HasColumnName("profileBranchNameEn");

                entity.Property(e => e.ProfileBranchNameTh)
                    .HasMaxLength(500)
                    .HasColumnName("profileBranchNameTh");

                entity.Property(e => e.ProfileCompanyCode)
                    .HasMaxLength(10)
                    .HasColumnName("profileCompanyCode")
                    .IsFixedLength();

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

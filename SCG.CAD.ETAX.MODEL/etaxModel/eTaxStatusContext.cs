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

        public virtual DbSet<ConfigMftsCompressPrintSetting> ConfigMftsCompressPrintSettings { get; set; } = null!;

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
            modelBuilder.Entity<ConfigMftsCompressPrintSetting>(entity =>
            {
                entity.HasKey(e => e.ConfigMftsCompressPrintSettingNo);

                entity.ToTable("configMftsCompressPrintSetting");

                entity.Property(e => e.ConfigMftsCompressPrintSettingNo).HasColumnName("configMftsCompressPrintSettingNo");

                entity.Property(e => e.ConfigMftsCompressPrintSettingAnyTime)
                    .HasMaxLength(1000)
                    .HasColumnName("configMftsCompressPrintSettingAnyTime");

                entity.Property(e => e.ConfigMftsCompressPrintSettingCompanyCode)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressPrintSettingCompanyCode");

                entity.Property(e => e.ConfigMftsCompressPrintSettingDataSource)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressPrintSettingDataSource");

                entity.Property(e => e.ConfigMftsCompressPrintSettingInputPdf)
                    .HasMaxLength(1000)
                    .HasColumnName("configMftsCompressPrintSettingInputPdf");

                entity.Property(e => e.ConfigMftsCompressPrintSettingOneTime)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressPrintSettingOneTime");

                entity.Property(e => e.ConfigMftsCompressPrintSettingOutputPdf)
                    .HasMaxLength(1000)
                    .HasColumnName("configMftsCompressPrintSettingOutputPdf");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("((1))");

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

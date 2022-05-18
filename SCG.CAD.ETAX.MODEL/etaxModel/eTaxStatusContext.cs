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

        public virtual DbSet<ConfigMftsCompressXmlSetting> ConfigMftsCompressXmlSettings { get; set; } = null!;

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
            modelBuilder.Entity<ConfigMftsCompressXmlSetting>(entity =>
            {
                entity.HasKey(e => e.ConfigMftsCompressXmlSettingNo);

                entity.ToTable("configMftsCompressXmlSetting");

                entity.Property(e => e.ConfigMftsCompressXmlSettingNo).HasColumnName("configMftsCompressXmlSettingNo");

                entity.Property(e => e.ConfigMftsCompressXmlSettingCompanyCode)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingCompanyCode");

                entity.Property(e => e.ConfigMftsCompressXmlSettingCompressType)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingCompressType");

                entity.Property(e => e.ConfigMftsCompressXmlSettingHost)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingHost");

                entity.Property(e => e.ConfigMftsCompressXmlSettingInputFolder)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsCompressXmlSettingInputFolder");

                entity.Property(e => e.ConfigMftsCompressXmlSettingOutputFolder)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsCompressXmlSettingOutputFolder");

                entity.Property(e => e.ConfigMftsCompressXmlSettingPassword)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingPassword");

                entity.Property(e => e.ConfigMftsCompressXmlSettingPort)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingPort");

                entity.Property(e => e.ConfigMftsCompressXmlSettingSourceName)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingSourceName");

                entity.Property(e => e.ConfigMftsCompressXmlSettingUsername)
                    .HasMaxLength(100)
                    .HasColumnName("configMftsCompressXmlSettingUsername");

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

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

        public virtual DbSet<ConfigMftsEmailSetting> ConfigMftsEmailSettings { get; set; } = null!;

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
            modelBuilder.Entity<ConfigMftsEmailSetting>(entity =>
            {
                entity.HasKey(e => e.ConfigMftsEmailSettingNo);

                entity.ToTable("configMftsEmailSetting");

                entity.Property(e => e.ConfigMftsEmailSettingNo).HasColumnName("configMftsEmailSettingNo");

                entity.Property(e => e.ConfigMftsEmailSettingAnyTime)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingAnyTime");

                entity.Property(e => e.ConfigMftsEmailSettingApiKey)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingApiKey");

                entity.Property(e => e.ConfigMftsEmailSettingCompanyCode)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingCompanyCode");

                entity.Property(e => e.ConfigMftsEmailSettingEmail)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingEmail");

                entity.Property(e => e.ConfigMftsEmailSettingEmailTemplate)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingEmailTemplate");

                entity.Property(e => e.ConfigMftsEmailSettingHost)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingHost");

                entity.Property(e => e.ConfigMftsEmailSettingOneTime)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingOneTime");

                entity.Property(e => e.ConfigMftsEmailSettingOperation)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingOperation");

                entity.Property(e => e.ConfigMftsEmailSettingPassword)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingPassword");

                entity.Property(e => e.ConfigMftsEmailSettingPort)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingPort");

                entity.Property(e => e.ConfigMftsEmailSettingProtocol)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingProtocol");

                entity.Property(e => e.ConfigMftsEmailSettingUsername)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsEmailSettingUsername");

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

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

        public virtual DbSet<ConfigMftsIndexGenerationSettingOutput> ConfigMftsIndexGenerationSettingOutputs { get; set; } = null!;

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
            modelBuilder.Entity<ConfigMftsIndexGenerationSettingOutput>(entity =>
            {
                entity.HasKey(e => e.ConfigMftsIndexGenerationSettingOutputNo);

                entity.ToTable("configMftsIndexGenerationSettingOutput");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputNo).HasColumnName("configMftsIndexGenerationSettingOutputNo");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputAnyTime)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputAnyTime");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputCompanyCode)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputCompanyCode");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputFolder)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputFolder");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputHost)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputHost");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputLogReceiveFolder");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputLogReceiveType)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputLogReceiveType");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputNextTime)
                    .HasColumnType("datetime")
                    .HasColumnName("configMftsIndexGenerationSettingOutputNextTime");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputOneTime)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputOneTime");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputPassword)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputPassword");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputPort)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputPort");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputSourceName)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputSourceName");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputType)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputType");

                entity.Property(e => e.ConfigMftsIndexGenerationSettingOutputUsername)
                    .HasMaxLength(300)
                    .HasColumnName("configMftsIndexGenerationSettingOutputUsername");

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

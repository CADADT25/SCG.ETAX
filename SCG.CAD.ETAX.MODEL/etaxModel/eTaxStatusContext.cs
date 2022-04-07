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

        public virtual DbSet<ConfigXmlSign> ConfigXmlSigns { get; set; } = null!;

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
            modelBuilder.Entity<ConfigXmlSign>(entity =>
            {
                entity.HasKey(e => e.ConfigXmlsignNo)
                    .HasName("PK_configXmlign");

                entity.ToTable("configXmlSign");

                entity.Property(e => e.ConfigXmlsignNo).HasColumnName("configXmlsignNo");

                entity.Property(e => e.ConfigXmlsignCertificateSerial)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignCertificateSerial");

                entity.Property(e => e.ConfigXmlsignCompanyTax)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignCompanyTax");

                entity.Property(e => e.ConfigXmlsignCompanycode)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignCompanycode");

                entity.Property(e => e.ConfigXmlsignHsmSerial)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignHsmSerial");

                entity.Property(e => e.ConfigXmlsignInputPath)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignInputPath");

                entity.Property(e => e.ConfigXmlsignInputSource)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignInputSource");

                entity.Property(e => e.ConfigXmlsignOnlineRecordNumber)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignOnlineRecordNumber");

                entity.Property(e => e.ConfigXmlsignOutputConvertPath)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignOutputConvertPath");

                entity.Property(e => e.ConfigXmlsignOutputPath)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignOutputPath");

                entity.Property(e => e.ConfigXmlsignOutputSource)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlsignOutputSource");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

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

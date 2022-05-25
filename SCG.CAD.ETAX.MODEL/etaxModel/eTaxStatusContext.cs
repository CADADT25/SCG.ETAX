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

        public virtual DbSet<ConfigPdfSign> ConfigPdfSigns { get; set; } = null!;

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
            modelBuilder.Entity<ConfigPdfSign>(entity =>
            {
                entity.HasKey(e => e.ConfigPdfsignNo);

                entity.ToTable("configPdfSign");

                entity.Property(e => e.ConfigPdfsignNo).HasColumnName("configPdfsignNo");

                entity.Property(e => e.ConfigPdfsignAnyTime)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignAnyTime");

                entity.Property(e => e.ConfigPdfsignCompanyCode)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignCompanyCode");

                entity.Property(e => e.ConfigPdfsignCompanyTax)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignCompanyTax");

                entity.Property(e => e.ConfigPdfsignDsLocation)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignDsLocation");

                entity.Property(e => e.ConfigPdfsignDsReason)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignDsReason");

                entity.Property(e => e.ConfigPdfsignFontName)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignFontName");

                entity.Property(e => e.ConfigPdfsignFontSize)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignFontSize");

                entity.Property(e => e.ConfigPdfsignFtpHost)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignFtpHost");

                entity.Property(e => e.ConfigPdfsignFtpPassword)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignFtpPassword");

                entity.Property(e => e.ConfigPdfsignFtpPort)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignFtpPort");

                entity.Property(e => e.ConfigPdfsignFtpUserName)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignFtpUserName");

                entity.Property(e => e.ConfigPdfsignHsmModule)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignHsmModule");

                entity.Property(e => e.ConfigPdfsignHsmPassword)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignHsmPassword");

                entity.Property(e => e.ConfigPdfsignHsmSerial)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignHsmSerial");

                entity.Property(e => e.ConfigPdfsignHsmSlot)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignHsmSlot");

                entity.Property(e => e.ConfigPdfsignImage).HasColumnName("configPdfsignImage");

                entity.Property(e => e.ConfigPdfsignInputPath)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignInputPath");

                entity.Property(e => e.ConfigPdfsignInputSource)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignInputSource");

                entity.Property(e => e.ConfigPdfsignInputType)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignInputType");

                entity.Property(e => e.ConfigPdfsignKeyAlias)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignKeyAlias");

                entity.Property(e => e.ConfigPdfsignOneTime)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignOneTime");

                entity.Property(e => e.ConfigPdfsignOnlineRecordNumber)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignOnlineRecordNumber");

                entity.Property(e => e.ConfigPdfsignOperationMode)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignOperationMode");

                entity.Property(e => e.ConfigPdfsignOutputPath)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignOutputPath");

                entity.Property(e => e.ConfigPdfsignOutputSource)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignOutputSource");

                entity.Property(e => e.ConfigPdfsignOutputType)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignOutputType");

                entity.Property(e => e.ConfigPdfsignPage)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignPage");

                entity.Property(e => e.ConfigPdfsignTimestampPassword)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignTimestampPassword");

                entity.Property(e => e.ConfigPdfsignTimestampUrl)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignTimestampUrl");

                entity.Property(e => e.ConfigPdfsignTimestampUserName)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignTimestampUserName");

                entity.Property(e => e.ConfigPdfsignUlx)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignUlx");

                entity.Property(e => e.ConfigPdfsignUly)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignUly");

                entity.Property(e => e.ConfigPdfsignVisibleDs)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignVisibleDs");

                entity.Property(e => e.ConfigPdfsignWithTimeStamp)
                    .HasMaxLength(255)
                    .HasColumnName("configPdfsignWithTimeStamp");

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

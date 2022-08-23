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

        public virtual DbSet<ProfileCertificate> ProfileCertificates { get; set; } = null!;

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
            modelBuilder.Entity<ProfileCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateNo);

                entity.ToTable("profileCertificate");

                entity.Property(e => e.CertificateNo).HasColumnName("certificateNo");

                entity.Property(e => e.CertificateCertSerial).HasColumnName("certificateCertSerial");

                entity.Property(e => e.CertificateCompanyCode).HasColumnName("certificateCompanyCode");

                entity.Property(e => e.CertificateEndDate).HasColumnName("certificateEndDate");

                entity.Property(e => e.CertificateHsmname).HasColumnName("certificateHSMName");

                entity.Property(e => e.CertificateHsmserial).HasColumnName("certificateHSMSerial");

                entity.Property(e => e.CertificateKeyAlias).HasColumnName("certificateKeyAlias");

                entity.Property(e => e.CertificateName).HasColumnName("certificateName");

                entity.Property(e => e.CertificateSlotPassword).HasColumnName("certificateSlotPassword");

                entity.Property(e => e.CertificateStartDate).HasColumnName("certificateStartDate");

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

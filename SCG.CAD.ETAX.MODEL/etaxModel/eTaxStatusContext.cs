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

        public virtual DbSet<ProfileCustomer> ProfileCustomers { get; set; } = null!;

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
            modelBuilder.Entity<ProfileCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerProfileNo)
                    .HasName("PK_customerProfile");

                entity.ToTable("profileCustomer");

                entity.Property(e => e.CustomerProfileNo).HasColumnName("customerProfileNo");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(255)
                    .HasColumnName("companyCode");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(255)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerCcemail)
                    .HasMaxLength(255)
                    .HasColumnName("customerCCEmail");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(255)
                    .HasColumnName("customerEmail");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(255)
                    .HasColumnName("customerId");

                entity.Property(e => e.EmailTemplateNo)
                    .HasMaxLength(255)
                    .HasColumnName("emailTemplateNo");

                entity.Property(e => e.EmailType).HasColumnName("emailType");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.NumberOfCopies).HasColumnName("numberOfCopies");

                entity.Property(e => e.OutputType).HasColumnName("outputType");

                entity.Property(e => e.StatusEmail).HasColumnName("statusEmail");

                entity.Property(e => e.StatusPrint).HasColumnName("statusPrint");

                entity.Property(e => e.StatusSignPdf).HasColumnName("statusSignPdf");

                entity.Property(e => e.StatusSignXml).HasColumnName("statusSignXml");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(255)
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

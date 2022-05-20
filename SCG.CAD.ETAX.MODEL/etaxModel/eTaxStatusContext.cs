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

        public virtual DbSet<TransactionDescription> TransactionDescriptions { get; set; } = null!;

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
            modelBuilder.Entity<TransactionDescription>(entity =>
            {
                entity.HasKey(e => e.TransactionNo);

                entity.ToTable("transactionDescription");

                entity.Property(e => e.TransactionNo).HasColumnName("transactionNo");

                entity.Property(e => e.BillTo).HasColumnName("billTo");

                entity.Property(e => e.BillingDate)
                    .HasColumnType("date")
                    .HasColumnName("billingDate");

                entity.Property(e => e.BillingNumber)
                    .HasMaxLength(255)
                    .HasColumnName("billingNumber");

                entity.Property(e => e.BillingYear).HasColumnName("billingYear");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(255)
                    .HasColumnName("companyCode");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(500)
                    .HasColumnName("companyName");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(255)
                    .HasColumnName("customerId");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(255)
                    .HasColumnName("customerName");

                entity.Property(e => e.DmsAttachmentFileName)
                    .HasMaxLength(255)
                    .HasColumnName("dmsAttachmentFileName");

                entity.Property(e => e.DmsAttachmentFilePath)
                    .HasMaxLength(255)
                    .HasColumnName("dmsAttachmentFilePath");

                entity.Property(e => e.DocType)
                    .HasMaxLength(255)
                    .HasColumnName("docType");

                entity.Property(e => e.EmailSendDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("emailSendDateTime");

                entity.Property(e => e.EmailSendDetail)
                    .HasMaxLength(255)
                    .HasColumnName("emailSendDetail");

                entity.Property(e => e.EmailSendStatus)
                    .HasMaxLength(255)
                    .HasColumnName("emailSendStatus");

                entity.Property(e => e.FiDoc).HasColumnName("fiDoc");

                entity.Property(e => e.Foc).HasColumnName("foc");

                entity.Property(e => e.GenerateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("generateDateTime");

                entity.Property(e => e.GenerateDetail)
                    .HasMaxLength(255)
                    .HasColumnName("generateDetail");

                entity.Property(e => e.GenerateStatus)
                    .HasMaxLength(255)
                    .HasColumnName("generateStatus");

                entity.Property(e => e.Ic).HasColumnName("ic");

                entity.Property(e => e.ImageDocType)
                    .HasMaxLength(255)
                    .HasColumnName("imageDocType");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.OneTimeEmail)
                    .HasMaxLength(255)
                    .HasColumnName("oneTimeEmail");

                entity.Property(e => e.OutputMailTransactionNo)
                    .HasMaxLength(255)
                    .HasColumnName("outputMailTransactionNo");

                entity.Property(e => e.OutputPdfTransactionNo)
                    .HasMaxLength(255)
                    .HasColumnName("outputPdfTransactionNo");

                entity.Property(e => e.OutputXmlTransactionNo)
                    .HasMaxLength(255)
                    .HasColumnName("outputXmlTransactionNo");

                entity.Property(e => e.Payer).HasColumnName("payer");

                entity.Property(e => e.PdfIndexingDateTime)
                    .HasMaxLength(255)
                    .HasColumnName("pdfIndexingDateTime");

                entity.Property(e => e.PdfIndexingDetail)
                    .HasMaxLength(255)
                    .HasColumnName("pdfIndexingDetail");

                entity.Property(e => e.PdfIndexingStatus)
                    .HasMaxLength(255)
                    .HasColumnName("pdfIndexingStatus");

                entity.Property(e => e.PdfSignDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pdfSignDateTime");

                entity.Property(e => e.PdfSignDetail)
                    .HasMaxLength(255)
                    .HasColumnName("pdfSignDetail");

                entity.Property(e => e.PdfSignLocation)
                    .HasMaxLength(255)
                    .HasColumnName("pdfSignLocation");

                entity.Property(e => e.PdfSignStatus)
                    .HasMaxLength(255)
                    .HasColumnName("pdfSignStatus");

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(255)
                    .HasColumnName("poNumber");

                entity.Property(e => e.PostingYear)
                    .HasMaxLength(255)
                    .HasColumnName("postingYear");

                entity.Property(e => e.PrintDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("printDateTime");

                entity.Property(e => e.PrintDetail)
                    .HasMaxLength(255)
                    .HasColumnName("printDetail");

                entity.Property(e => e.PrintStatus)
                    .HasMaxLength(255)
                    .HasColumnName("printStatus");

                entity.Property(e => e.ProcessingDate)
                    .HasColumnType("date")
                    .HasColumnName("processingDate");

                entity.Property(e => e.SellOrg)
                    .HasMaxLength(255)
                    .HasColumnName("sellOrg");

                entity.Property(e => e.ShipTo).HasColumnName("shipTo");

                entity.Property(e => e.SoldTo).HasColumnName("soldTo");

                entity.Property(e => e.SourceName)
                    .HasMaxLength(255)
                    .HasColumnName("sourceName");

                entity.Property(e => e.TypeInput)
                    .HasMaxLength(255)
                    .HasColumnName("typeInput");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.XmlCompressDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("xmlCompressDateTime");

                entity.Property(e => e.XmlCompressDetail)
                    .HasMaxLength(255)
                    .HasColumnName("xmlCompressDetail");

                entity.Property(e => e.XmlCompressStatus)
                    .HasMaxLength(255)
                    .HasColumnName("xmlCompressStatus");

                entity.Property(e => e.XmlSignDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("xmlSignDateTime");

                entity.Property(e => e.XmlSignDetail)
                    .HasMaxLength(255)
                    .HasColumnName("xmlSignDetail");

                entity.Property(e => e.XmlSignLocation)
                    .HasMaxLength(255)
                    .HasColumnName("xmlSignLocation");

                entity.Property(e => e.XmlSignStatus)
                    .HasMaxLength(255)
                    .HasColumnName("xmlSignStatus");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

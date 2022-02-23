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

        public virtual DbSet<CancelZipHeader> CancelZipHeaders { get; set; } = null!;
        public virtual DbSet<CancelZipLine> CancelZipLines { get; set; } = null!;
        public virtual DbSet<DocumentCode> DocumentCodes { get; set; } = null!;
        public virtual DbSet<ErpDocument> ErpDocuments { get; set; } = null!;
        public virtual DbSet<NewsBoard> NewsBoards { get; set; } = null!;
        public virtual DbSet<ProductUnit> ProductUnits { get; set; } = null!;
        public virtual DbSet<ProfileCertificate> ProfileCertificates { get; set; } = null!;
        public virtual DbSet<ProfileCompany> ProfileCompanies { get; set; } = null!;
        public virtual DbSet<ProfileCustomer> ProfileCustomers { get; set; } = null!;
        public virtual DbSet<ProfileDataSource> ProfileDataSources { get; set; } = null!;
        public virtual DbSet<ProfileEmailTemplate> ProfileEmailTemplates { get; set; } = null!;
        public virtual DbSet<ProfileEmailType> ProfileEmailTypes { get; set; } = null!;
        public virtual DbSet<ProfileIsActive> ProfileIsActives { get; set; } = null!;
        public virtual DbSet<ProfilePartner> ProfilePartners { get; set; } = null!;
        public virtual DbSet<ProfileSellOrg> ProfileSellOrgs { get; set; } = null!;
        public virtual DbSet<ProfileSeller> ProfileSellers { get; set; } = null!;
        public virtual DbSet<ProfileStatus> ProfileStatuses { get; set; } = null!;
        public virtual DbSet<RdDocument> RdDocuments { get; set; } = null!;
        public virtual DbSet<TaxCode> TaxCodes { get; set; } = null!;
        public virtual DbSet<TransactionDescription> TransactionDescriptions { get; set; } = null!;
        public virtual DbSet<ZipFileConfig> ZipFileConfigs { get; set; } = null!;
        public virtual DbSet<ZipFilePost> ZipFilePosts { get; set; } = null!;
        public virtual DbSet<ZipFileTransaction> ZipFileTransactions { get; set; } = null!;
        public virtual DbSet<ZipFileType> ZipFileTypes { get; set; } = null!;

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
            modelBuilder.Entity<CancelZipHeader>(entity =>
            {
                entity.HasKey(e => e.CancelZipHeaderNo);

                entity.ToTable("cancelZipHeader");

                entity.Property(e => e.CancelZipHeaderNo).HasColumnName("cancelZipHeaderNo");

                entity.Property(e => e.CancelReason)
                    .HasMaxLength(500)
                    .HasColumnName("cancelReason");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.StatusCancel)
                    .HasMaxLength(50)
                    .HasColumnName("statusCancel");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.WorkFlowCode)
                    .HasMaxLength(50)
                    .HasColumnName("workFlowCode");
            });

            modelBuilder.Entity<CancelZipLine>(entity =>
            {
                entity.HasKey(e => e.CancelZipLineNo);

                entity.ToTable("cancelZipLine");

                entity.Property(e => e.CancelZipLineNo).HasColumnName("cancelZipLineNo");

                entity.Property(e => e.BillingNo)
                    .HasMaxLength(50)
                    .HasColumnName("billingNo");

                entity.Property(e => e.CancelZipHeaderNo).HasColumnName("cancelZipHeaderNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.TransactionNo)
                    .HasMaxLength(50)
                    .HasColumnName("transactionNo");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<DocumentCode>(entity =>
            {
                entity.HasKey(e => e.DocumentCodeNo);

                entity.ToTable("documentCode");

                entity.Property(e => e.DocumentCodeNo).HasColumnName("documentCodeNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.DocumentCodeErp)
                    .HasMaxLength(30)
                    .HasColumnName("documentCodeErp");

                entity.Property(e => e.DocumentCodeRd)
                    .HasMaxLength(30)
                    .HasColumnName("documentCodeRd");

                entity.Property(e => e.DocumentDescription)
                    .HasMaxLength(100)
                    .HasColumnName("documentDescription");

                entity.Property(e => e.ErpSource)
                    .HasMaxLength(30)
                    .HasColumnName("erpSource");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ErpDocument>(entity =>
            {
                entity.HasKey(e => e.ErpDocumentNo);

                entity.ToTable("erpDocument");

                entity.Property(e => e.ErpDocumentNo).HasColumnName("erpDocumentNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.ErpDocumentCode)
                    .HasMaxLength(50)
                    .HasColumnName("erpDocumentCode");

                entity.Property(e => e.ErpDocumentNameEn)
                    .HasMaxLength(500)
                    .HasColumnName("erpDocumentNameEN");

                entity.Property(e => e.ErpDocumentNameTh)
                    .HasMaxLength(500)
                    .HasColumnName("erpDocumentNameTH");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<NewsBoard>(entity =>
            {
                entity.HasKey(e => e.NewsBoardNo);

                entity.ToTable("newsBoard");

                entity.Property(e => e.NewsBoardNo).HasColumnName("newsBoardNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NewsBoardBody).HasColumnName("newsBoardBody");

                entity.Property(e => e.NewsBoardEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("newsBoardEnd");

                entity.Property(e => e.NewsBoardFooter)
                    .HasMaxLength(100)
                    .HasColumnName("newsBoardFooter");

                entity.Property(e => e.NewsBoardHeader)
                    .HasMaxLength(100)
                    .HasColumnName("newsBoardHeader");

                entity.Property(e => e.NewsBoardSeq).HasColumnName("newsBoardSeq");

                entity.Property(e => e.NewsBoardStart)
                    .HasColumnType("datetime")
                    .HasColumnName("newsBoardStart");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProductUnit>(entity =>
            {
                entity.HasKey(e => e.ProductUnitNo);

                entity.ToTable("productUnit");

                entity.Property(e => e.ProductUnitNo).HasColumnName("productUnitNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.ProductUnitDescription)
                    .HasMaxLength(500)
                    .HasColumnName("productUnitDescription");

                entity.Property(e => e.ProductUnitErp)
                    .HasMaxLength(250)
                    .HasColumnName("productUnitErp");

                entity.Property(e => e.ProductUnitRd)
                    .HasMaxLength(250)
                    .HasColumnName("productUnitRd");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateNo);

                entity.ToTable("profileCertificate");

                entity.Property(e => e.CertificateNo).HasColumnName("certificateNo");

                entity.Property(e => e.CompanyCertificateEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("companyCertificateEndDate");

                entity.Property(e => e.CompanyCertificateKeyAlias).HasColumnName("companyCertificateKeyAlias");

                entity.Property(e => e.CompanyCertificateSerial).HasColumnName("companyCertificateSerial");

                entity.Property(e => e.CompanyCertificateStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("companyCertificateStartDate");

                entity.Property(e => e.CompanyCode).HasColumnName("companyCode");

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

            modelBuilder.Entity<ProfileCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyNo)
                    .HasName("PK_compnanyProfile");

                entity.ToTable("profileCompany");

                entity.Property(e => e.CompanyNo).HasColumnName("companyNo");

                entity.Property(e => e.CertificateProfileNo).HasColumnName("certificateProfileNo");

                entity.Property(e => e.CompanyCode).HasColumnName("companyCode");

                entity.Property(e => e.CompanyNameEn)
                    .HasMaxLength(250)
                    .HasColumnName("companyNameEN");

                entity.Property(e => e.CompanyNameTh)
                    .HasMaxLength(250)
                    .HasColumnName("companyNameTH");

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

            modelBuilder.Entity<ProfileCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerProfileNo);

                entity.ToTable("profileCustomer");

                entity.Property(e => e.CustomerProfileNo).HasColumnName("customerProfileNo");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("companyCode");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerCcemail)
                    .HasMaxLength(50)
                    .HasColumnName("customerCCEmail");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(50)
                    .HasColumnName("customerEmail");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.EmailType)
                    .HasMaxLength(50)
                    .HasColumnName("emailType");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.NumberOfCopies)
                    .HasMaxLength(50)
                    .HasColumnName("numberOfCopies");

                entity.Property(e => e.OutputType)
                    .HasMaxLength(50)
                    .HasColumnName("outputType");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileDataSource>(entity =>
            {
                entity.HasKey(e => e.DataSourceNo);

                entity.ToTable("profileDataSource");

                entity.Property(e => e.DataSourceNo).HasColumnName("dataSourceNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.DataSourceDescription)
                    .HasMaxLength(500)
                    .HasColumnName("dataSourceDescription");

                entity.Property(e => e.DataSourceName)
                    .HasMaxLength(250)
                    .HasColumnName("dataSourceName");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileEmailTemplate>(entity =>
            {
                entity.HasKey(e => e.EmailTemplateNo);

                entity.ToTable("profileEmailTemplate");

                entity.Property(e => e.EmailTemplateNo).HasColumnName("emailTemplateNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.EmailBody).HasColumnName("emailBody");

                entity.Property(e => e.EmailTypeNo).HasColumnName("emailTypeNo");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileEmailType>(entity =>
            {
                entity.HasKey(e => e.EmailTypeNo);

                entity.ToTable("profileEmailType");

                entity.Property(e => e.EmailTypeNo).HasColumnName("emailTypeNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.EmailTypeName)
                    .HasMaxLength(500)
                    .HasColumnName("emailTypeName");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileIsActive>(entity =>
            {
                entity.HasKey(e => e.IsActiveNo);

                entity.ToTable("profileIsActive");

                entity.Property(e => e.IsActiveNo).HasColumnName("isActiveNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActiveCode).HasColumnName("isActiveCode");

                entity.Property(e => e.IsActiveNameEn)
                    .HasMaxLength(100)
                    .HasColumnName("isActiveNameEN");

                entity.Property(e => e.IsActiveNameTh)
                    .HasMaxLength(100)
                    .HasColumnName("isActiveNameTH");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProfilePartner>(entity =>
            {
                entity.HasKey(e => e.PartnerProfileNo);

                entity.ToTable("profilePartner");

                entity.Property(e => e.PartnerProfileNo).HasColumnName("partnerProfileNo");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("companyCode");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("customerId");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.NumberOfCopies)
                    .HasMaxLength(50)
                    .HasColumnName("numberOfCopies");

                entity.Property(e => e.PartnerEmailType)
                    .HasMaxLength(50)
                    .HasColumnName("partnerEmailType");

                entity.Property(e => e.PartnerOutputType)
                    .HasMaxLength(50)
                    .HasColumnName("partnerOutputType");

                entity.Property(e => e.SellOrg)
                    .HasMaxLength(50)
                    .HasColumnName("sellOrg");

                entity.Property(e => e.ShipToCcemail)
                    .HasMaxLength(50)
                    .HasColumnName("shipToCCEmail");

                entity.Property(e => e.ShipToCode)
                    .HasMaxLength(50)
                    .HasColumnName("shipToCode");

                entity.Property(e => e.ShipToEmail)
                    .HasMaxLength(50)
                    .HasColumnName("shipToEmail");

                entity.Property(e => e.SoldToCcemail)
                    .HasMaxLength(50)
                    .HasColumnName("soldToCCEmail");

                entity.Property(e => e.SoldToCode)
                    .HasMaxLength(50)
                    .HasColumnName("soldToCode");

                entity.Property(e => e.SoldToEmail)
                    .HasMaxLength(50)
                    .HasColumnName("soldToEmail");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileSellOrg>(entity =>
            {
                entity.HasKey(e => e.SellOrgNo);

                entity.ToTable("profileSellOrg");

                entity.Property(e => e.SellOrgNo).HasColumnName("sellOrgNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.SellOrgDescripttion)
                    .HasMaxLength(500)
                    .HasColumnName("sellOrgDescripttion");

                entity.Property(e => e.SellOrgName)
                    .HasMaxLength(250)
                    .HasColumnName("sellOrgName");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileSeller>(entity =>
            {
                entity.HasKey(e => e.SellerNo);

                entity.ToTable("profileSeller");

                entity.Property(e => e.SellerNo).HasColumnName("sellerNo");

                entity.Property(e => e.AddressNumber)
                    .HasMaxLength(20)
                    .HasColumnName("addressNumber");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("branchCode")
                    .IsFixedLength();

                entity.Property(e => e.Building)
                    .HasMaxLength(100)
                    .HasColumnName("building");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("companyCode")
                    .IsFixedLength();

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.District)
                    .HasMaxLength(100)
                    .HasColumnName("district");

                entity.Property(e => e.EmailTemplateNo).HasColumnName("emailTemplateNo");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Province)
                    .HasMaxLength(100)
                    .HasColumnName("province");

                entity.Property(e => e.Road)
                    .HasMaxLength(100)
                    .HasColumnName("road");

                entity.Property(e => e.SellerEmail)
                    .HasMaxLength(100)
                    .HasColumnName("sellerEmail");

                entity.Property(e => e.SubDistrict)
                    .HasMaxLength(100)
                    .HasColumnName("subDistrict");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<ProfileStatus>(entity =>
            {
                entity.HasKey(e => e.StatusNo);

                entity.ToTable("profileStatus");

                entity.Property(e => e.StatusNo).HasColumnName("statusNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.StatusNameEn)
                    .HasMaxLength(100)
                    .HasColumnName("statusNameEN");

                entity.Property(e => e.StatusNameTh)
                    .HasMaxLength(100)
                    .HasColumnName("statusNameTH");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<RdDocument>(entity =>
            {
                entity.HasKey(e => e.RdDocumentNo);

                entity.ToTable("rdDocument");

                entity.Property(e => e.RdDocumentNo).HasColumnName("rdDocumentNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.RdDocumentCode)
                    .HasMaxLength(20)
                    .HasColumnName("rdDocumentCode");

                entity.Property(e => e.RdDocumentNameEn)
                    .HasMaxLength(250)
                    .HasColumnName("rdDocumentNameEN");

                entity.Property(e => e.RdDocumentNameTh)
                    .HasMaxLength(250)
                    .HasColumnName("rdDocumentNameTH");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<TaxCode>(entity =>
            {
                entity.HasKey(e => e.TaxCodeNo);

                entity.ToTable("taxCode");

                entity.Property(e => e.TaxCodeNo).HasColumnName("taxCodeNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.TaxCodeDescription).HasMaxLength(500);

                entity.Property(e => e.TaxCodeErp).HasMaxLength(20);

                entity.Property(e => e.TaxCodeRd).HasMaxLength(20);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<TransactionDescription>(entity =>
            {
                entity.HasKey(e => e.TransactionNo);

                entity.ToTable("transactionDescription");

                entity.Property(e => e.TransactionNo).HasColumnName("transactionNo");

                entity.Property(e => e.BillTo)
                    .HasMaxLength(300)
                    .HasColumnName("billTo");

                entity.Property(e => e.BillingDate)
                    .HasColumnType("date")
                    .HasColumnName("billingDate");

                entity.Property(e => e.BillingNumber)
                    .HasMaxLength(50)
                    .HasColumnName("billingNumber");

                entity.Property(e => e.BillingYear)
                    .HasMaxLength(50)
                    .HasColumnName("billingYear");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("companyCode");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(300)
                    .HasColumnName("companyName");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(300)
                    .HasColumnName("customerName");

                entity.Property(e => e.DmsAttachmentFileName)
                    .HasMaxLength(300)
                    .HasColumnName("dmsAttachmentFileName");

                entity.Property(e => e.DmsAttachmentFilePath)
                    .HasMaxLength(300)
                    .HasColumnName("dmsAttachmentFilePath");

                entity.Property(e => e.DocType)
                    .HasMaxLength(20)
                    .HasColumnName("docType");

                entity.Property(e => e.EmailSendDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("emailSendDateTime");

                entity.Property(e => e.EmailSendDetail)
                    .HasMaxLength(300)
                    .HasColumnName("emailSendDetail");

                entity.Property(e => e.EmailSendStatus).HasColumnName("emailSendStatus");

                entity.Property(e => e.FiDoc)
                    .HasMaxLength(50)
                    .HasColumnName("fiDoc");

                entity.Property(e => e.Foc).HasColumnName("foc");

                entity.Property(e => e.GenerateDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("generateDateTime");

                entity.Property(e => e.GenerateDetail)
                    .HasMaxLength(300)
                    .HasColumnName("generateDetail");

                entity.Property(e => e.GenerateStatus).HasColumnName("generateStatus");

                entity.Property(e => e.Ic).HasColumnName("ic");

                entity.Property(e => e.ImageDocType)
                    .HasMaxLength(20)
                    .HasColumnName("imageDocType");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Payer)
                    .HasMaxLength(300)
                    .HasColumnName("payer");

                entity.Property(e => e.PdfIndexingDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pdfIndexingDateTime");

                entity.Property(e => e.PdfIndexingDetail)
                    .HasMaxLength(300)
                    .HasColumnName("pdfIndexingDetail");

                entity.Property(e => e.PdfIndexingStatus).HasColumnName("pdfIndexingStatus");

                entity.Property(e => e.PdfSignDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pdfSignDateTime");

                entity.Property(e => e.PdfSignDetail)
                    .HasMaxLength(300)
                    .HasColumnName("pdfSignDetail");

                entity.Property(e => e.PdfSignLocation)
                    .HasMaxLength(300)
                    .HasColumnName("pdfSignLocation");

                entity.Property(e => e.PdfSignStatus).HasColumnName("pdfSignStatus");

                entity.Property(e => e.PoNumber).HasColumnName("poNumber");

                entity.Property(e => e.PostingYear)
                    .HasMaxLength(4)
                    .HasColumnName("postingYear");

                entity.Property(e => e.PrintDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("printDateTime");

                entity.Property(e => e.PrintDetail)
                    .HasMaxLength(300)
                    .HasColumnName("printDetail");

                entity.Property(e => e.PrintStatus).HasColumnName("printStatus");

                entity.Property(e => e.ProcessingDate)
                    .HasColumnType("date")
                    .HasColumnName("processingDate");

                entity.Property(e => e.SellOrg)
                    .HasMaxLength(50)
                    .HasColumnName("sellOrg");

                entity.Property(e => e.ShipTo)
                    .HasMaxLength(300)
                    .HasColumnName("shipTo");

                entity.Property(e => e.SoldTo)
                    .HasMaxLength(300)
                    .HasColumnName("soldTo");

                entity.Property(e => e.SourceName)
                    .HasMaxLength(10)
                    .HasColumnName("sourceName");

                entity.Property(e => e.TypeInput)
                    .HasMaxLength(20)
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
                    .HasMaxLength(300)
                    .HasColumnName("xmlCompressDetail");

                entity.Property(e => e.XmlCompressStatus).HasColumnName("xmlCompressStatus");

                entity.Property(e => e.XmlSignDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("xmlSignDateTime");

                entity.Property(e => e.XmlSignDetail)
                    .HasMaxLength(300)
                    .HasColumnName("xmlSignDetail");

                entity.Property(e => e.XmlSignLocation)
                    .HasMaxLength(300)
                    .HasColumnName("xmlSignLocation");

                entity.Property(e => e.XmlSignStatus).HasColumnName("xmlSignStatus");

                entity.Property(e => e.ZipTransactionNo).HasColumnName("zipTransactionNo");
            });

            modelBuilder.Entity<ZipFileConfig>(entity =>
            {
                entity.HasKey(e => e.ZipFileConfigNo);

                entity.ToTable("zipFileConfig");

                entity.Property(e => e.ZipFileConfigNo).HasColumnName("zipFileConfigNo");

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

                entity.Property(e => e.ZipFileConfigName1)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileConfigName1");

                entity.Property(e => e.ZipFileConfigName2)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileConfigName2");

                entity.Property(e => e.ZipFileConfigName3)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileConfigName3");

                entity.Property(e => e.ZipFileConfigName4)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileConfigName4");

                entity.Property(e => e.ZipFileConfigName5)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileConfigName5");

                entity.Property(e => e.ZipFileConfigValue1)
                    .HasMaxLength(50)
                    .HasColumnName("zipFileConfigValue1");

                entity.Property(e => e.ZipFileConfigValue2)
                    .HasMaxLength(50)
                    .HasColumnName("zipFileConfigValue2");

                entity.Property(e => e.ZipFileConfigValue3)
                    .HasMaxLength(50)
                    .HasColumnName("zipFileConfigValue3");

                entity.Property(e => e.ZipFileConfigValue4)
                    .HasMaxLength(50)
                    .HasColumnName("zipFileConfigValue4");

                entity.Property(e => e.ZipFileConfigValue5)
                    .HasMaxLength(50)
                    .HasColumnName("zipFileConfigValue5");
            });

            modelBuilder.Entity<ZipFilePost>(entity =>
            {
                entity.HasKey(e => e.ZipFilePostNo);

                entity.ToTable("zipFilePost");

                entity.Property(e => e.ZipFilePostNo).HasColumnName("zipFilePostNo");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(100)
                    .HasColumnName("createBy");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(100)
                    .HasColumnName("updateBy");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.ZipFileStatus).HasColumnName("zipFileStatus");

                entity.Property(e => e.ZipTransactionNo).HasColumnName("zipTransactionNo");
            });

            modelBuilder.Entity<ZipFileTransaction>(entity =>
            {
                entity.HasKey(e => e.ZipTransactionNo);

                entity.ToTable("zipFileTransaction");

                entity.Property(e => e.ZipTransactionNo).HasColumnName("zipTransactionNo");

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

                entity.Property(e => e.ZipFileName)
                    .HasMaxLength(10)
                    .HasColumnName("zipFileName")
                    .IsFixedLength();

                entity.Property(e => e.ZipFilePath)
                    .HasMaxLength(10)
                    .HasColumnName("zipFilePath")
                    .IsFixedLength();

                entity.Property(e => e.ZipFilePostStatus)
                    .HasMaxLength(10)
                    .HasColumnName("zipFilePostStatus")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ZipFileType>(entity =>
            {
                entity.HasKey(e => e.ZipFileTypeNo);

                entity.ToTable("zipFileType");

                entity.Property(e => e.ZipFileTypeNo).HasColumnName("zipFileTypeNo");

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

                entity.Property(e => e.ZipFileTypeCode)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileTypeCode");

                entity.Property(e => e.ZipFileTypeName)
                    .HasMaxLength(100)
                    .HasColumnName("zipFileTypeName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

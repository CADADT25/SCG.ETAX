using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<CancelZipHeader> cancelZipHeader { get; set; }
        public DbSet<CancelZipLine> cancelZipLine { get; set; }
        public DbSet<DocumentCode> documentCode { get; set; }
        public DbSet<NewsBoard> newsBoard { get; set; }
        public DbSet<ProductUnit> productUnit { get; set; }
        public DbSet<ProfileCertificate> profileCertificate { get; set; }
        public DbSet<ProfileCompany> profileCompany { get; set; }
        public DbSet<ProfileCustomer> profileCustomer { get; set; }
        public DbSet<ProfileEmailTemplate> profileEmailTemplate { get; set; }
        public DbSet<ProfileEmailType> profileEmailType { get; set; }
        public DbSet<ProfileIsActive> profileIsActive { get; set; }
        public DbSet<ProfilePartner> profilePartner { get; set; }
        public DbSet<ProfileSeller> profileSeller { get; set; }
        public DbSet<ProfileStatus> profileStatuse { get; set; }
        public DbSet<RdDocument> rdDocument { get; set; }
        public DbSet<TaxCode> taxCode { get; set; }
        public DbSet<TransactionDescription> transactionDescription { get; set; }
        public DbSet<ZipFileConfig> zipFileConfig { get; set; }
        public DbSet<ZipFilePost> zipFilePost { get; set; }
        public DbSet<ZipFileTransaction> zipFileTransaction { get; set; }
        public DbSet<ZipFileType> zipFileType { get; set; }
        public DbSet<ProfileSellOrg> profileSellOrg { get; set; }
        public DbSet<ProfileDataSource> profileDataSource { get; set; }
        public DbSet<ErpDocument> erpDocument { get; set; }
        public DbSet<ProfileCompanyCode> profileCompanyCode { get; set; }
        public DbSet<ProfileReasonIssue> profileReasonIssue { get; set; }
        public DbSet<ProfileBranch> profileBranch { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["ConnectionStr"];

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

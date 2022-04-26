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

        public virtual DbSet<ConfigXmlGenerator> ConfigXmlGenerators { get; set; } = null!;

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
            modelBuilder.Entity<ConfigXmlGenerator>(entity =>
            {
                entity.HasKey(e => e.ConfigXmlGeneratorNo);

                entity.ToTable("configXmlGenerator");

                entity.Property(e => e.ConfigXmlGeneratorNo).HasColumnName("configXmlGeneratorNo");

                entity.Property(e => e.ConfigXmlGeneratorCompanyCode)
                    .HasMaxLength(10)
                    .HasColumnName("configXmlGeneratorCompanyCode");

                entity.Property(e => e.ConfigXmlGeneratorInputPath)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlGeneratorInputPath");

                entity.Property(e => e.ConfigXmlGeneratorInputSource)
                    .HasMaxLength(50)
                    .HasColumnName("configXmlGeneratorInputSource");

                entity.Property(e => e.ConfigXmlGeneratorInputType)
                    .HasMaxLength(50)
                    .HasColumnName("configXmlGeneratorInputType");

                entity.Property(e => e.ConfigXmlGeneratorOnlineRecordNumber)
                    .HasMaxLength(50)
                    .HasColumnName("configXmlGeneratorOnlineRecordNumber");

                entity.Property(e => e.ConfigXmlGeneratorOutputPath)
                    .HasMaxLength(255)
                    .HasColumnName("configXmlGeneratorOutputPath");

                entity.Property(e => e.ConfigXmlGeneratorOutputSource)
                    .HasMaxLength(50)
                    .HasColumnName("configXmlGeneratorOutputSource");

                entity.Property(e => e.ConfigXmlGeneratorOutputType)
                    .HasMaxLength(50)
                    .HasColumnName("configXmlGeneratorOutputType");

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

using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RISTExamOnlineProject.Models.db
{
    public class SPTODbContext : DbContext
    {
        public SPTODbContext(DbContextOptions<SPTODbContext> options) : base(options)
        {

        }

        public virtual DbSet<vewOperatorAlls> vewOperatorAll { get; set; }
        //public virtual DbSet<UserLoginModel> UserLoginModel { get; set; }
        public virtual DbSet<vewOperatorLicense> vewOperatorLicense { get; set; }

        public virtual DbSet<vewOperatorAdditionalDep> vewOperatorAdditionalDep { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vewOperatorAdditionalDep>()
                .HasKey(k => new {k.OperatorID, k.SectionCode});

            modelBuilder.Query<sprOperatorReqChange>();

            modelBuilder.Entity<InputItemList>()
                .HasKey(k => new {k.ItemCateg, k.ItemCode});

            modelBuilder.Entity<Exam_QuestionDetail>()
              .HasKey(k => new { k.ItemCode,k.ValueCodeQuestion,k.ValueCodeAnswer });

            /////////////////////////////////////////////////////
            //modelBuilder.Entity<ItemCategoryModel>(entity =>
            //{
            //    entity.Property(e => e.ItemCateg)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ItemCategName)
            //        .IsRequired()
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    //modelBuilder.Entity<Passage>()
            //    //    .Property(b => b.CreatedDate)
            //    //    .HasDefaultValueSql("getdate()");
            //    //entity.Property(e => e.AddDate)
            //    //    .ValueGeneratedOnAddOrUpdate();

            //    //entity.Property(e => e.UpdDate)
            //    //    .ValueGeneratedOnAddOrUpdate();

            //    //entity.Property(e => e.AddDate)
            //    //    .HasDefaultValueSql("getdate()");
            //    entity.Property(e => e.AddDate)
            //        .IsRequired()
            //        .HasColumnType("DateTime");
            //    entity.Property(e => e.UpdDate)
            //        .IsRequired()
            //        .HasColumnType("DateTime");
            //    //    .HasDefaultValueSql("GetDate()");
            //    //    //entity.Property(e => e.UpdDate)
            //    //    //    .HasColumnType("timestamp without time zone")


            //});
            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        //public class OrderConfiguration : IEntityTypeConfiguration<ItemCategory>
        //{
        //    public void Configure(EntityTypeBuilder<ItemCategory> builder)
        //    {
        //        builder.HasKey(o => o.ItemCateg);
        //        builder.Property(t => t.AddDate)
        //            .IsRequired()
        //            .HasColumnType("Date")
        //            .HasDefaultValueSql("GetDate()");
        //        builder.Property(x => x.UpdDate)
        //            .IsRequired()
        //            .HasColumnType("Date")
        //            .HasDefaultValueSql("GetDate()");
        //    }
        //}

        public virtual DbSet<sprOperatorShowListInCharge> sprOperatorShowListInChang { get; set; }
        public virtual DbSet<sprRunningNo> sprRunningNo { get; set; }
        //public virtual DbSet<OperatorReqChange> OperatorReqChange { get; set; }


        public virtual DbSet<vewT_Section_Master> vewT_Section_Master { get; set; }
        public DbSet<vewDivisionMaster> vewDivisionMaster { get; set; }
        public virtual DbSet<vewDepartmentMaster> vewDepartmentMaster { get; set; }
        public DbSet<vewSectionMaster> vewSectionMaster { get; set; }
        public DbSet<vewOperatorReqChange> vewOperatorReqChange { get; set; }
        public DbSet<vewOperatorReqChange_Groupby> vewOperatorReqChange_Groupby { get; set; }              

        public virtual DbSet<vewOperatorGroupMaster> vewOperatorGroupMaster { get; set; }
        public virtual DbSet<vewLicenseMaster> vewLicenseMaster { get; set; }

        public virtual DbSet<TempReqChange> TempReqChange { get; set; }
        public virtual DbSet<TempListAddition> TempListAddition { get; set; }
        public virtual DbSet<vewOperatorReqChangeCompare> vewOperatorReqChangeCompare { get; set; }
        public virtual DbSet<ReqChangeCompareData> ReqChangeCompareData { get; set; }
        public virtual DbSet<Exam_QuestionDetail> Exam_QuestionDetail { get; set; }

        public DbSet<ItemCategoryModel> ItemCategory { get; set; }
        public DbSet<ItemCode_Detail> ItemCode_Detail { get; set; }
        public DbSet<ExamApproved_Detail> ExamApproved_Detail { get; set; }
      

    }
}
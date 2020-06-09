﻿using Microsoft.EntityFrameworkCore;

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
        }
        public virtual DbSet<sprOperatorShowListInCharge> sprOperatorShowListInChang { get; set; }

        public virtual DbSet<OperatorReqChange> OperatorReqChange { get; set; }
        public virtual DbSet<vewT_Section_Master> vewT_Section_Master { get; set; }
        public DbSet<vewDivisionMaster> vewDivisionMaster { get; set; }
        public DbSet<vewDepartmentMaster> vewDepartmentMaster { get; set; }
        public DbSet<vewSectionMaster> vewSectionMaster { get; set; }

        public virtual DbSet<vewOperatorGroupMaster> vewOperatorGroupMaster { get; set; }
        public virtual DbSet<vewLicenseMaster> vewLicenseMaster { get; set; }

    }
}
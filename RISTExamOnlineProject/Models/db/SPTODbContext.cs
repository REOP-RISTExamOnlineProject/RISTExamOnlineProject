using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RISTExamOnlineProject.Models.db
{
    public class SPTODbContext : DbContext
    {
        public SPTODbContext(DbContextOptions<SPTODbContext> options) : base(options)
        {

        }
        public virtual DbSet<vewOperatorAll> vewOperatorAll { get; set; }
        public virtual DbSet<vewOperatorLicense> vewOperatorLicense { get; set; }
    }
}

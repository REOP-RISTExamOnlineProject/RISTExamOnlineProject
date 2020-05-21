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

        public virtual DbSet<TbOperator> Operator { get; set; }
        public virtual DbSet<vewT_Training_Record> Training_Record { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RISTExamOnlineProject.Models.db
{
    public class STPODbContext : DbContext
    {
        public STPODbContext(DbContextOptions<STPODbContext> options) : base(options)
        {

        }
        public virtual DbSet<TbOperator> Operator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewDivisionMaster
    {
        
        public string DivisionID { get; set; }
        public string DivisionName { get; set; }
        [Key]
        public long row_num { get; set; }

        [NotMapped]
        public string DepartmentID { get; set; }
       // [NotMapped]
       // public int SubCategoryID { get; set; }
    }
}

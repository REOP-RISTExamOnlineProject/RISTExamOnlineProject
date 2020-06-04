using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewDepartmentMaster
    {
        
        public string DivisionID { get; set; }
        [Key]
        public string DepartmentID { get; set; }
        public string Department { get; set; }
        public long row_num { get; set; }

    }
}

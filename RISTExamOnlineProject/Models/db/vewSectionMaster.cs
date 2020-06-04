using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewSectionMaster
    {
        public string SectionCodeID { get; set; }

        public string Section { get; set; }
       
        public long row_dept_id { get; set; }
        [Key]
        public long row_sect_id { get; set; }

    }
}

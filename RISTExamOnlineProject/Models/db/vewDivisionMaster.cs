using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewDivisionMaster
    {
        [Key]
        public string SectionCodeID { get; set; }
        public string Division { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class sprOperatorShowListInCharge
    {
        [Key]
        public string OperatorID { get; set; }
        [Display(Name = "Name Eng")] public string NameEng { get; set; }

        [Display(Name = "Name Th")] public string NameThai { get; set; }
        public string SectionCode { get; set; }
        public string Position { get; set; }
        public string JobTitle { get; set; }
       
        
    }
}

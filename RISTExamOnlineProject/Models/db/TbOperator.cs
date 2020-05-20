using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class TbOperator
    {
        [Key]
        [Display(Name = "OPNO.")]
        public string OperatorID { get; set; }

        
        public string Password { get; set; }

        [Display(Name = "Name Eng")]
        public string NameEng { get; set; }

        [Display(Name = "Name Th")]
        public string NameThai { get; set; }

    }
}

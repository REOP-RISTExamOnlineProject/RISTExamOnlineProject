using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorAlls
    {
        [Key]
        [Display(Name = "OPNO.")]
        public string OperatorID { get; set; }
        public string Password { get; set; }
        [Display(Name = "Name Eng")]
        public string NameEng { get; set; }
        [Display(Name = "Name Th")]
        public string NameThai { get; set; }
        public string JobTitle { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string GroupName { get; set; }
        public string Email1 { get; set; }
        public string RFID { get; set; }

      
    }
   

}

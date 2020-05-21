using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorAll
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

        //public IEnumerable<SelectListItem> PositionSelectListItems { get; set; }


        //[NotMapped]
        ////SelectListeItem type list creating Dropdown 
        //public IEnumerable<SelectListItem> PositionListItems { get; set; }

        //for first dropdown selected value


        //for second dropdown selected value

    }

    //public class DropdownViewModel
    //{
    //    //SelectListeItem type list creating Dropdown 
    //    public IEnumerable<SelectListItem> EmpList { get; set; }

    //    //for first dropdown selected value
    //    public string SelectedEmp { get; set; }

    //    //for second dropdown selected value
    //    public string SelectedEmp2 { get; set; }
    //}
}

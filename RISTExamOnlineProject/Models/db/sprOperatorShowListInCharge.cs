using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string SectionName { get; set; }
        public string Position { get; set; }
       
        public string JobTitle { get; set; }

       

    }

    //public class TempReqChange
    //{

    //    public string opno { get; set; }
    //    public string OperatorID { get; set; }
    //    public string SectionCode { get; set; }
    //    public string SectionAttribute { get; set; }
    //    public string Shift { get; set; }
    //    public string License { get; set; }
    //    public string active { get; set; }
    //    public string ReqOperatorID { get; set; }
    //    public string ChangeOperatorID { get; set; }
    //}
    //public class TempReqChange
    //{
    //    //@OperatorID char (10),
    //    //@SectionCode nvarchar(255),
    //    //@SectionAttribute nvarchar(255),
    //    //@OperatorGroup char (2) ,
    //    //@License nvarchar(255),
    //    //@Active nvarchar(15),
    //    //@ReqOperatorID char (10),
    //    //@ChangeOperatorID char (10)	
    //    [NotMapped]
    //    public string OperatorID { get; set; }
    //    [NotMapped]
    //    public string SectionCode { get; set; }
    //    [NotMapped]
    //    public string SectionAttribute { get; set; }
    //    [NotMapped]
    //    public string Shift { get; set; }
    //    [NotMapped]
    //    public string License { get; set; }
    //    [NotMapped]
    //    public string active { get; set; }
    //    [NotMapped]
    //    public string ReqOperatorID { get; set; }
    //    [NotMapped]
    //    public string ChangeOperatorID { get; set; }
    //}
}

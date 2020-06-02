using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorLicense
    {
        [Key] [Display(Name = "OPNO.")] public string OperatorID { get; set; }

        public string License { get; set; }
    }

    public class ListOperatorLicense
    {
        List<vewOperatorLicense> ListOPLicense { get; set; }
    }
}
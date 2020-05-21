using System.ComponentModel.DataAnnotations;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorLicense
    {
        [Key]
        [Display(Name = "OPNO.")]
        public string OperatorID { get; set; }
        public string License { get; set; }
    }
}

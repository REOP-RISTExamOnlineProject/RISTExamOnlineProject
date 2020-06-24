using System.ComponentModel.DataAnnotations;

namespace RISTExamOnlineProject.Models.db
{
    public class TempListAddition
    {
        [Key]
        public int ID { get; set; }
        public string OPNO { get; set; }
        public string SectionSelect { get; set; }
        public bool FlagSend { get; set; }
    }
}

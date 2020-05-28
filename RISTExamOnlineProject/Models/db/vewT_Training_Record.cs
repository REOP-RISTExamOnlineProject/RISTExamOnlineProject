using System.ComponentModel.DataAnnotations;

namespace RISTExamOnlineProject.Models.db
{
    public class vewT_Training_Record
    {
        [Key] public int TRec_ID { get; set; }

        public string StaffCode { get; set; }
        public string Name_of_training { get; set; }
        public string Lecturer { get; set; }
        public string Training_completion_day { get; set; }
        public string Training_summary { get; set; }
        public string Training_material { get; set; }
        public string Rev { get; set; }
        public string Cost { get; set; }
        public string Evaluation_of_results { get; set; }
        public string Remarks { get; set; }
        public string Site { get; set; }
        public string Training_period { get; set; }
        public string CreationDate { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorAdditionalDep
    {
        [Key]
        public string OperatorID { get; set; }
        public string SectionCode { get; set; }
        public string SectionCode2 { get; set; }
        public string Section { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string S5 { get; set; }
        public string S6 { get; set; }
        public int StatusC { get; set; }
    }
}
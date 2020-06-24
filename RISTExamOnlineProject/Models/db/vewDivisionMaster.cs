using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RISTExamOnlineProject.Models.db
{
    public class vewDivisionMaster
    {
        [Key]
        public long row_num { get; set; }

        public string DivisionName { get; set; }
        

        [NotMapped]
        public string DepartmentID { get; set; }
        [NotMapped]
        public string row_dept_id { get; set; }
        [NotMapped]
        public string SectionCodeID { get; set; }


        
    }

    //public class categorydivision
    //{

    //}

    public class additionalist
    {
        [NotMapped]
        public string SectionCode { get; set; }
        [NotMapped]
        public string Section { get; set; }
        [NotMapped]
        public string Department { get; set; }
        [NotMapped]
        public string Division { get; set; }
    }
    
}

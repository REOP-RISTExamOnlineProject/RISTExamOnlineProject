using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RISTExamOnlineProject.Models.db
{
    public class vewDepartmentMaster
    {

        // public string DivisionID { get; set; }
        
        //public string DepartmentID { get; set; }
      
        [Key]
        //[Display(Name = "DepartmentID")]
        public long row_dept_id { get; set; }
        public string Department { get; set; }
       
        public long row_num { get; set; }
        public long DepartmentID { get; internal set; }
        public string Departmentname { get; internal set; }
    }
}

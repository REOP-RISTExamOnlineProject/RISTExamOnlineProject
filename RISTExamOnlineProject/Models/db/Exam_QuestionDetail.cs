using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class Exam_QuestionDetail
    {

        public string ItemCode { get; set; }
        public string temCategName { get; set; }
        [Key]
        public string ValueCodeQuestion { get; set; }
       // [Key]
        public string ValueCodeAnswer { get; set; }
       // [Key]
        public int Seq { get; set; }
        public string Question { get; set; }
        public string Ans_Count { get; set; }
        public string Max_Seq { get; set; }

    }
}

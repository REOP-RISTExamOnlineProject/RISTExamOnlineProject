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
        public string ItemCategName { get; set; } 
        public string ValueCodeQuestion { get; set; }
        public string ValueCodeAnswer { get; set; }
        public int Seq { get; set; }
        public string Question { get; set; }
        public string Ans_Count { get; set; }
        public string Max_Seq { get; set; }

    }
}

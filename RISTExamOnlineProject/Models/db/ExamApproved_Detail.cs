using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class ExamApproved_Detail
    {
        [Key]
        public int Seq { get; set; }
        public string Question { get; set; }
        public int Total_ANS { get; set; }
        public string ValueStatus { get; set; }
        public int Rewrite_Master { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class _ExamQuestionAnswer
    {
        public string strQuestion { get; set; }
        public string strAnswer { get; set; }
    }
    public class _ExamCommitResult
    {
        public Boolean BoolResult { get; set; }
        public string strResult { get; set; }
        public string strMgs { get; set; }

    }
}

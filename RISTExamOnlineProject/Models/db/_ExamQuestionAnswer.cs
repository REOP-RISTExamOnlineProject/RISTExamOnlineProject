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


    public class _excamResultCtrl
    {
        public string PlanRefID { get; set; }
        public string ItemCateg { get; set; }
        public string ItemCode { get; set; }
        public string OperatorID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }



    public class _ExamResultDetail
    {
        public string PlanRefID { get; set; }
        public string ItemCateg { get; set; }
        public string ItemCategName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }
        public string Minutes { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Level { get; set; }
        public string Correct { get; set; }
        public string Wrong { get; set; }
        public string Total { get; set; }
        public string Results { get; set; }
        public string AddDate { get; set; }
    }


    public class _ExamResultList
    {
        public List<_ExamResultDetail> DataExamReultList { get; set; }
        public string strResult { get; set; }
    }



}

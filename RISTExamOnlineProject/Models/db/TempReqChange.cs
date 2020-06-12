using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class TempReqChange
    {
        //@OperatorID char (10),
        //@SectionCode nvarchar(255),
        //@SectionAttribute nvarchar(255),
        //@OperatorGroup char (2) ,
        //@License nvarchar(255),
        //@Active nvarchar(15),
        //@ReqOperatorID char (10),
        //@ChangeOperatorID char (10)	
        public string OperatorID { get; set; }
        public string SectionCode { get; set; }
        public string SectionAttribute { get; set; }
        public string Shift { get; set; }
        public string License { get; set; }
        public string active { get; set; }
        public string ReqOperatorID { get; set; }
        public string ChangeOperatorID { get; set; }
    }
}

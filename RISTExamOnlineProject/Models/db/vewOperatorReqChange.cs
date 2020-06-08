using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorReqChange
    {
       

        [Key]
        public int Nbr { get; set; }
        public string DocNo { get; set; }
        public int Seq { get; set; }
        public string OperatorID { get; set; }
        public string SectionCode { get; set; }
        public string SectionAttribute { get; set; }
        public string OperatorGroup { get; set; }
        public string License { get; set; }
        public string Active { get; set; }
        public string ReqOperatorID { get; set; }
        public DateTime ReqDate { get; set; }
        public string ChangeOperatorID { get; set; }
        public DateTime ChangeDate { get; set; }








    }
}

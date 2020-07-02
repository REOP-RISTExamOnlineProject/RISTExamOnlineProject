using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorReqChangeCompare
    {

        [Key]
        public int Nbr { get; set; }
        public string DocNo { get; set; }
        public string Seq { get; set; }
        public string OperatorID { get; set; }
        public string New_SectionCode { get; set; }
        public string New_Section { get; set; }
        public string New_SectionAttribute { get; set; }
        public string New_OperatorGroup { get; set; }
        public string New_GroupName { get; set; }
        public string New_License { get; set; }
        public string New_Active { get; set; }
        public string ReqOperatorID { get; set; }
        public string GroupName { get; set; }
        public string SectionCode { get; set; }
        public string Section { get; set; }
        public string SectionAttribute { get; set; }
        public string License { get; set; }
        public string Active { get; set; }
        public string ReqDate { get; set; }
        
        public string ChangeOperatorID { get; set; }
    }
}

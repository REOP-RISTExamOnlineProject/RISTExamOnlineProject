using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    
    public class sprOperatorReqChange
    {
        //([DocNo]
        //,[Seq]
        //,[OperatorID]
        //,[SectionCode]
        //,[SectionAttribute]
        //,[OperatorGroup]
        //,[License]
        //,[Active]
        //,[ReqOperatorID]
        //,[ReqDate]
        [NotMapped]
        public string DocNo { get; set; }
        [NotMapped]
        public string Seq { get; set; }
        [NotMapped]
        public string OperatorID { get; set; }
        [NotMapped]
        public string SectionCode { get; set; }
        [NotMapped]
        public string SectionAttribute { get; set; }
        [NotMapped]
        public string OperatorGroup { get; set; }
        [NotMapped]
        public string License { get; set; }
        [NotMapped]
        public string Active { get; set; }
        [NotMapped]
        public string ReqDate { get; set; }
        
    }
}

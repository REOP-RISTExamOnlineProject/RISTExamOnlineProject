using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorReqChange_Groupby
    {

   [Key]
        public string DocNo { get; set; }    
        public string ReqOperatorID { get; set; }
        public string ReqDate { get; set; }
        public int TotalJob { get; set; }


    }
}

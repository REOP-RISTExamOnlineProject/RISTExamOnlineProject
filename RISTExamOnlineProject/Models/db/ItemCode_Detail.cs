using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class ItemCode_Detail
    {
        [Key]
        public int Nbr { get; set; }
        public string ItemCateg { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int DisplayOrder { get; set; }
        public int TimeLimit { get; set; }
           
    }
}

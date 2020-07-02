using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class ReqChangeCompareData
    {
        [Key]
        public string Catagory    { get; set; }
        public string New { get; set; }
        public string Present { get; set; }
    }
}

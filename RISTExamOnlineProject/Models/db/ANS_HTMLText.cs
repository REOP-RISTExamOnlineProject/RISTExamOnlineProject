using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class ANS_HTMLText
    { 
        [Key]
        public int row { get; set; }
        public int AnsCount { get; set; }
        public string CodeBase64 { get; set; }
    }
  

}

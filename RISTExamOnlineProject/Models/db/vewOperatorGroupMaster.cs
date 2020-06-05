using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewOperatorGroupMaster
    {
        [Key]
        public string OperatorGroup { get; set; }
        public string GroupName { get; set; }
        public string Shift { get; set; }
    }
}

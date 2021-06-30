using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class ItemCategoryTypeModel
    {
        [Key]
        public int Group_No { get; set; }

        public string License_Grp_Nm { get; set; }
    }
}

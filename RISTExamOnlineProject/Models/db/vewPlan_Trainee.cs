using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class vewPlan_Trainee
    {
        [Key]
        public string Staffcode { get; set; }
        [Key]
        public string Plan_ID { get; set; }

        public string Training_Section { get; set; }
        public string License_Name { get; set; }
        public string Trianer { get; set; }
        public string ControlBy { get; set; }
        public string Start_Training { get; set; }
        public string End_Training { get; set; }
        public string License_Type { get; set; }
        public string Plan_Type_Name { get; set; }
        public string Test_Pass { get; set; }

    }
}

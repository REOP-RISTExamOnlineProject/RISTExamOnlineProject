using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.db
{
    public class InputItemListModel
    {

        [Key]
        public int Nbr { get; set; }

        [DisplayName("ItemCategID")]
        [Required(ErrorMessage = "This Field is required.")]
        public string ItemCateg { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string ItemCode { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string ItemName { get; set; }
        public int DisplayOrder { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? AddDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? UpdDate { get; set; }

        public string UserName { get; set; }

        public string ComputerName { get; set; }
    }
}

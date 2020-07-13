using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RISTExamOnlineProject.Models.db
{
    public  class ItemCategory
    {
        [Key]
        [DisplayName("ItemCategID")]
        public string ItemCateg { get; set; }
        [Required]
        public string ItemCategName { get; set; }
        
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AddDate { get; set; }
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime UpdDate { get; set; }/* = DateTime.Now;*/
        //[Required]
        public string UserName { get; set; }
        //[Required]

        public string ComputerName { get; set; }
       
       
    }

    public class InputItemList
    {
        public string ItemCateg { get; set; }
        public string ItemCode{ get; set; }
        public string ItemName { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }

}

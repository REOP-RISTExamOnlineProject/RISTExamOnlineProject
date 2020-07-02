using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RISTExamOnlineProject.Models.db
{
    public class ItemCategory
    {
        [Key]
        [Required]
        [DisplayName("ItemCategKey")]
        public string ItemCateg { get; set; }
        [Required]
        public string ItemCategName { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[DatabaseGenerated(DatabaseGeneratedOption.)]
        public DateTime AddDate { get; set; }
        // [Required]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdDate { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RISTExamOnlineProject.Models.db
{
    public class _OperatorItemCateg
    { 
        [Key]
        public string ItemCateg { get; set; }
        public string ItemCategName { get; set; }
        public string cntItemCateg { get; set; }
    }
    public class ResultItemCateg
    {
         public List<_OperatorItemCateg> _listOpCateg { get; set; }
        public string strResult { get; set; } 
    }



    public class ListSelectList_
    {
        public List<SelectListItem> _ListSelectList { get; set; }
        public string strResult { get; set; }
    }
}
 
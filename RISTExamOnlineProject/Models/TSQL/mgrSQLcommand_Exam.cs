using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand_Exam
    {
        private readonly IConfiguration _configuration;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private string strSQL = "";
        public mgrSQLcommand_Exam(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetExamHTML(string ItemCateg, string ItemCode)
        {

            string HTMLTEXT = "";

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " [dbo].[srpMakeHTMLQuestion]	'"+ ItemCateg + "', '"+ ItemCode + " '" ;

            dt = ObjRun.GetDatatables(strSQL);

            if (dt.Rows.Count > 0) 
            {
                HTMLTEXT = dt.Rows[0][0].ToString(); 

            }         


            return HTMLTEXT;
        }


        public List<SelectListItem> GetCategory() {

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "  SELECT[ItemCateg],[ItemCategName]  FROM[SPTOSystem].[dbo].[vewQuestionNull] group by[ItemCateg],[ItemCategName]";

            dt = ObjRun.GetDatatables(strSQL);
            List<SelectListItem> listItems = new List<SelectListItem>();

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "--- Choose Category ---",
                    Value = ""
                });
                foreach (DataRow row in dt.Rows)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = row["ItemCateg"].ToString().Trim() + "-" +row["ItemCategName"].ToString().Trim(),
                        Value = row["ItemCateg"].ToString().Trim(),

                    });
                }
            }

            return listItems;

        }

        public List<SelectListItem> GetExamName(string ItemCateg)
        {

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT    [ItemCode],[ItemName]   FROM [SPTOSystem].[dbo].[vewQuestionNull] where[ItemCateg] = '"+ ItemCateg + "' group by[ItemName],[ItemCode]";

            dt = ObjRun.GetDatatables(strSQL);
            List<SelectListItem> listItems = new List<SelectListItem>();

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "--- Choose Exam name ---",
                    Value = ""
                });
                foreach (DataRow row in dt.Rows)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = row["ItemCode"].ToString().Trim() +"-"+row["ItemName"].ToString().Trim(),
                        Value = row["ItemCode"].ToString().Trim(),

                    });
                }
            }

            return listItems;

        }


    }
}

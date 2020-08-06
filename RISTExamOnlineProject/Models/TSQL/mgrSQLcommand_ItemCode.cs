using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand_ItemCode
    {
        private readonly IConfiguration _configuration;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private string strSQL = "";
        public mgrSQLcommand_ItemCode(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<SelectListItem> GetCategory() {

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "    SELECT[ItemCateg],[ItemCateg]+' - '+[ItemCategName]  as [ItemCategName] FROM[SPTOSystem].[dbo].[vewQuestionCateg] group by[ItemCateg],[ItemCategName] order by ItemCateg asc";

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
                        Text =  row["ItemCategName"].ToString().Trim(),
                        Value = row["ItemCateg"].ToString().Trim(),

                    });
                }
            }

            return listItems;
        }



        public string Itemcode_Management(string Job, string ItemCateg, string ItemCode, string ItemName, int time, int Nbr,string IP,string OPID) {
            string MS;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "EXEC [dbo].[srpItemCode_Management] '"+ Job + "', '" + ItemCateg + "','" + ItemCode + "','" + ItemName + "','" + Nbr + "','" + time + "','" + OPID + "','" + IP + "'";
      
            dt = ObjRun.GetDatatables(strSQL);
        
             MS = dt.Rows[0][0].ToString();


            return MS;
        }

        public List<ItemCode_Detail> Get_ItemCode_TableDetail(string ItemCateg) {
            var ObjRun = new mgrSQLConnect(_configuration);
            List<ItemCode_Detail> Detail = new List<ItemCode_Detail>();

            strSQL = " select * FROM [SPTOSystem].[dbo].[InputItemList] where ItemCateg = '"+ ItemCateg .Trim()+ "' order by ItemCode asc ";
            dt = ObjRun.GetDatatables(strSQL);

            if (dt.Rows.Count != 0) {

                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new ItemCode_Detail()
                    {
                        Nbr = Convert.ToInt32(row["Nbr"].ToString()),
                        ItemCateg = row["ItemCateg"].ToString(),
                        ItemCode = row["ItemCode"].ToString(),
                        ItemName = row["ItemName"].ToString(),
                        DisplayOrder = Convert.ToInt32( row["DisplayOrder"].ToString()),
                        TimeLimit = Convert.ToInt32(row["TimeLimit"].ToString()),
                    });


                }

            } 

            return Detail;
        }
    }
}

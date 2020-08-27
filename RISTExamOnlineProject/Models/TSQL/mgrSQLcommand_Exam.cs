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
            strSQL = " [dbo].[srpMakeHTMLQuestion]	'" + ItemCateg + "', '" + ItemCode + " '";

            dt = ObjRun.GetDatatables(strSQL);

            if (dt.Rows.Count > 0)
            {
                HTMLTEXT = dt.Rows[0][0].ToString();

            }


            return HTMLTEXT;
        }






        public DataTable Get_ValueCount(string ValueCodeQuestion)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT [ValueCodeQuestion] ,ISNULL([Seq],0)  as [Seq],ItemName FROM[SPTOSystem].[dbo].[vewQuestionAll] where[ValueCodeQuestion] ='" + ValueCodeQuestion + "' group by[ValueCodeQuestion], [Seq],ItemName order by[Seq]  desc";
            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }

        public DataTable Get_ValueCode(string Itemcode)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT [ValueCodeQuestion] ,[ValueCodeAnswer] FROM[SPTOSystem].[dbo].[InputItem] where ItemCode  = '" + Itemcode.Trim() + "'";
            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }




        public DataTable Get_ExamDetail(string Itemcode)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " select [ItemCode],ItemCategName,[ValueCodeQuestion],[ValueCodeAnswer],ISNULL(Seq,0) as Seq  ,ISNULL([Question],'') as [Question]  ,[InputItemName] ,count(*) as Ans_Count  ";
            strSQL += ",ISNULL((select max(Seq)    FROM   [SPTOSystem].[dbo].[vewQuestionAll]   where[ItemCode] = '" + Itemcode.Trim() + "'),0)  As Max_Seq ,[ValueStatus] FROM[SPTOSystem].[dbo].[vewQuestionAll] where[ItemCode] = '" + Itemcode.Trim() + "' ";
            strSQL += "   group by[ItemCode], ItemCategName,[ValueCodeQuestion],[ValueCodeAnswer],[Question],[Seq],[InputItemName] ,[ValueStatus] order by[ValueCodeQuestion], Seq";

            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }



        public List<SelectListItem> GetCategory()
        {

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "  SELECT[ItemCateg],[ItemCategName]  FROM[SPTOSystem].[dbo].[vewQuestionCateg] group by[ItemCateg],[ItemCategName] order by ItemCateg asc";

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
                        Text = row["ItemCateg"].ToString().Trim() + "-" + row["ItemCategName"].ToString().Trim(),
                        Value = row["ItemCateg"].ToString().Trim(),

                    });
                }
            }

            return listItems;

        }

        public List<SelectListItem> GetExamName(string ItemCateg)
        {

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT    [ItemCode],[ItemName]   FROM [SPTOSystem].[dbo].[vewQuestionCateg] where[ItemCateg] = '" + ItemCateg + "' group by[ItemName],[ItemCode]";

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
                        Text = row["ItemCode"].ToString().Trim() + "-" + row["ItemName"].ToString().Trim(),
                        Value = row["ItemCode"].ToString().Trim(),

                    });
                }
            }

            return listItems;

        }
 

         



        public string HTML_Question_Detail(string ValueQuestion, string ValueAnswer, int Seq,string Job) {

            string HTML_Test;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "[dbo].[srpEditQuestion_SelectHTML] '" + ValueQuestion.Trim() + "','" + ValueAnswer.Trim() + "','" + Seq.ToString() + "','0','"+ Job + "' ";          
            dt = ObjRun.GetDatatables(strSQL);
            HTML_Test = dt.Rows[0][0].ToString();
            return HTML_Test;

        }



        public string Valueslist_Management(string Job,string ValueCode,int Seq,string Value_HTML, string Value_TEXT, string Answer, string Need, string ComputerName, string OPID, string ValueQuestion, string ValueAnswer) {
            string MS;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " [dbo].[sprValueList_Management] '"+ Job + "', '"+ ValueCode + "', '"+ Seq + "', N'"+ Value_HTML + "', N'"+ Value_TEXT + "', " +
                "'"+ Answer + "', '"+ Need + "', '"+ ComputerName + "', '"+ OPID + "', '"+ ValueQuestion + "', '" + ValueAnswer + "'  ";        
            
            dt = ObjRun.GetDatatables(strSQL);
            MS = dt.Rows[0][1].ToString();

            return MS;

        }


    }
}

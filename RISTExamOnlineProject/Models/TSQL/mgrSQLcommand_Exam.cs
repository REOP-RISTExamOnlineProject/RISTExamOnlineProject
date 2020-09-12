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
            strSQL = " [dbo].[srpMakeHTMLExam]	'" + ItemCateg + "', '" + ItemCode + " '";

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




        public     DataTable Get_ExamDetail(string Itemcode)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " select [ItemCode],ItemCategName,[ValueCodeQuestion],[ValueCodeAnswer],ISNULL(Seq,0) as Seq  ,ISNULL([Question],'') as [Question]  ,[InputItemName] ,count(*) as Ans_Count  ";
            strSQL += ",ISNULL((select max(Seq)    FROM   [SPTOSystem].[dbo].[vewQuestionAll]   where[ItemCode] = '" + Itemcode.Trim() + "' and (Rewrite_ValueList = Rewrite_Master or Rewrite_ValueList = 0)),0)  As Max_Seq ,[ValueStatus],Rewrite_Master,[Rewrite_ValueList]  FROM[SPTOSystem].[dbo].[vewQuestionAll] where[ItemCode] = '" + Itemcode.Trim() + "' ";
            strSQL += "  and (Rewrite_ValueList = Rewrite_Master or Rewrite_ValueList = 0)   group by[ItemCode], ItemCategName,[ValueCodeQuestion],[ValueCodeAnswer],[Question],[Seq],[InputItemName] ,[ValueStatus],Rewrite_Master,[Rewrite_ValueList] order by[ValueStatus],[ValueCodeQuestion], Seq";

            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }



        public List<ExamApproved_Detail> Get_ExamDetail_Approved(string ValueCodeQuestion) {
            var ObjRun = new mgrSQLConnect(_configuration);
            List<ExamApproved_Detail> Detail = new List<ExamApproved_Detail>();
            strSQL = "SELECT  [Seq],[Question],count(*) as  Total_ANS ,[ValueStatus] FROM [SPTOSystem].[dbo].[vewQuestionAll]"+
            " where [ValueCodeQuestion] ='"+ ValueCodeQuestion + "' and[ValueStatus] != 'RUN'   group by[Seq] ,[Question] ,[ValueStatus]";
            dt = ObjRun.GetDatatables(strSQL);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new ExamApproved_Detail()
                    {

                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                    
                        Question = row["Question"].ToString(),
                        Total_ANS = Convert.ToInt32(row["Total_ANS"].ToString()),
                        ValueStatus = row["ValueStatus"].ToString(),




                    }); 

                }
            
            }
                return Detail;
        }




        public List<SelectListItem> GetItemDropDownList(string StrSQL,string TextDisplay)
        {
           
            var ObjRun = new mgrSQLConnect(_configuration);      
            dt = ObjRun.GetDatatables(StrSQL);
            List<SelectListItem> listItems = new List<SelectListItem>();
            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "-- Choose "+ TextDisplay + " --",
                    Value = "0"
                });
                foreach (DataRow row in dt.Rows)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = row[0].ToString().Trim() ,
                        Value = row[1].ToString().Trim(),

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



        public string Valueslist_Management(string Job,string ValueCode,int Seq,string Value_HTML, string Value_TEXT, string Answer, string Need, string ComputerName, string OPID, string ValueQuestion, string ValueAnswer,int Rewrite) {
            string MS;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " [dbo].[sprValueList_Management] '"+ Job + "', '"+ ValueCode + "', '"+ Seq + "', N'"+ Value_HTML + "', N'"+ Value_TEXT + "', " +
                "'"+ Answer + "', '"+ Need + "', '"+ ComputerName + "', '"+ OPID + "', '"+ ValueQuestion + "', '" + ValueAnswer + "','"+Rewrite.ToString()+"'  ";        
            
            dt = ObjRun.GetDatatables(strSQL);
            MS = dt.Rows[0][1].ToString();

            return MS;

        }

        public string View_Question(int seq, string ValueCodeQuestion, string ValueCodeAnswer ,string ValueStatus) {
            string MS = "";
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "[dbo].[srpMakeHTMLQuestion] '" + seq.ToString() + "','"+ ValueCodeQuestion + "','"+ ValueCodeAnswer + "','"+ ValueStatus + "'";

            dt = ObjRun.GetDatatables(strSQL);
            MS =  dt.Rows[0][0].ToString();


            return MS;

        
        
        }


        public string Get_ValueCodeAnswer(string valueCodeQuestion) {

            string valueCodeAnswer;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "  select top 1  [ValueCodeAnswer]  FROM [SPTOSystem].[dbo].[vewQuestionAll] where  [ValueCodeQuestion] = '"+ valueCodeQuestion.Trim() + "'";
            dt = ObjRun.GetDatatables(strSQL);
            valueCodeAnswer = dt.Rows[0][0].ToString();
            return valueCodeAnswer;
        }



        public string Approved_Reject_Question(string Job,int seq, string ValueCodeQuestion, string ValueCodeAnswer, string ValueStatus)
        {
            string MS = "";
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "[dbo].[srpApproved_Reject_Question] '"+ Job + "','"+ ValueStatus + "','"+ seq.ToString() + "','"+ ValueCodeQuestion + "','"+ ValueCodeAnswer + "'";

            dt = ObjRun.GetDatatables(strSQL);
            MS = dt.Rows[0][1].ToString();  
            return MS;

        }


    }
}

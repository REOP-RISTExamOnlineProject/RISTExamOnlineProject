using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            strSQL = " [dbo].[sprMakeHTMLExam]	'" + ItemCateg + "', '" + ItemCode + " '";

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


        public DataTable InputItem_Detail(string Itemcode)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "  select  top 1 [ItemCode],[ValueCodeQuestion], [ValueCodeAnswer],[Rewrite]  ,Convert(nvarchar(16),[UpdDate],120) as [UpdDate]   FROM [SPTOSystem].[dbo].[InputItem] where [ItemCode] = '" + Itemcode.Trim() + "' order by [UpdDate] desc";         
            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }

        public DataTable Get_ExamDetail (string Itemcode)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " select [ItemCode],ItemCategName,[ValueCodeQuestion],[ValueCodeAnswer],ISNULL(Seq,0) as Seq  ,ISNULL([Question],'') as [Question]  ,[InputItemName] ,count(*) as Ans_Count  ";
            strSQL += ",ISNULL((select max(Seq)    FROM   [SPTOSystem].[dbo].[vewQuestionAll]   where[ItemCode] = '" + Itemcode.Trim() + "' and (Rewrite_ValueList = Rewrite_Master or Rewrite_ValueList = 0)),0)  As Max_Seq ,[ValueStatus],Rewrite_Master,[Rewrite_ValueList],Convert(nvarchar(16),[UpdDate],120) as [UpdDate]  FROM[SPTOSystem].[dbo].[vewQuestionAll] where[ItemCode] = '" + Itemcode.Trim() + "' ";
            strSQL += "  and (Rewrite_ValueList = Rewrite_Master or Rewrite_ValueList = 0)   group by[ItemCode], ItemCategName,[ValueCodeQuestion],[ValueCodeAnswer],[Question],[Seq],[InputItemName] ,[ValueStatus],Rewrite_Master,[Rewrite_ValueList],Convert(nvarchar(16),[UpdDate],120)  order by[ValueStatus],[ValueCodeQuestion], Seq";

            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }






        public List<ExamApproved_Detail> Get_ExamDetail_Approved(string ValueCodeQuestion)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            List<ExamApproved_Detail> Detail = new List<ExamApproved_Detail>();
            strSQL = "SELECT  [Seq],[Question],count(*) as  Total_ANS ,[ValueStatus],Rewrite_Master FROM [SPTOSystem].[dbo].[vewQuestionAll]" +
            " where [ValueCodeQuestion] ='" + ValueCodeQuestion + "' and[ValueStatus] != 'RUN'   group by[Seq] ,[Question] ,[ValueStatus],[Rewrite_Master]";
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
                        Rewrite_Master = Convert.ToInt32(row["Rewrite_Master"].ToString()),



                    });

                }

            }
            return Detail;
        }




        public List<SelectListItem> GetItemDropDownList(string StrSQL, string TextDisplay)
        {

            var ObjRun = new mgrSQLConnect(_configuration);
            dt = ObjRun.GetDatatables(StrSQL);
            List<SelectListItem> listItems = new List<SelectListItem>();
            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "-- Choose " + TextDisplay + " --",
                    Value = "0"
                });
                foreach (DataRow row in dt.Rows)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = row[0].ToString().Trim(),
                        Value = row[1].ToString().Trim(),

                    });
                }
            }

            return listItems;

        }




        public string HTML_Question_Detail(string ValueQuestion, string ValueAnswer, int Seq, string Job)
        {

            string HTML_Test;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "[dbo].[srpEditQuestion_SelectHTML] '" + ValueQuestion.Trim() + "','" + ValueAnswer.Trim() + "','" + Seq.ToString() + "','0','" + Job + "' ";
            dt = ObjRun.GetDatatables(strSQL);
            HTML_Test = dt.Rows[0][0].ToString();
            return HTML_Test;

        }



        public string Valueslist_Management(string Job, string ValueCode, int Seq, string Value_HTML, string Value_TEXT, string Answer, string Need, string ComputerName, string OPID, string ValueQuestion, string ValueAnswer, int Rewrite)
        {
            string MS;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = " [dbo].[sprValueList_Management] '" + Job + "', '" + ValueCode + "', '" + Seq + "', N'" + Value_HTML + "', N'" + Value_TEXT + "', " +
                "'" + Answer + "', '" + Need + "', '" + ComputerName + "', '" + OPID + "', '" + ValueQuestion + "', '" + ValueAnswer + "','" + Rewrite.ToString() + "'  ";

            dt = ObjRun.GetDatatables(strSQL);
            MS = dt.Rows[0][1].ToString();

            return MS;

        }

        public string View_Question(int seq, string ValueCodeQuestion, string ValueCodeAnswer, string ValueStatus)
        {
            string MS = "";
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "[dbo].[sprMakeHTMLQuestion] '" + seq.ToString() + "','" + ValueCodeQuestion + "','" + ValueCodeAnswer + "','" + ValueStatus + "'";

            dt = ObjRun.GetDatatables(strSQL);
            MS = dt.Rows[0][0].ToString();


            return MS;



        }


        public string Get_ValueCodeAnswer(string valueCodeQuestion)
        {

            string valueCodeAnswer;
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "  select top 1  [ValueCodeAnswer]  FROM [SPTOSystem].[dbo].[vewQuestionAll] where  [ValueCodeQuestion] = '" + valueCodeQuestion.Trim() + "'";
            dt = ObjRun.GetDatatables(strSQL);
            valueCodeAnswer = dt.Rows[0][0].ToString();
            return valueCodeAnswer;
        }



        //public string Approved_Reject_Question(string Job,int seq, string ValueCodeQuestion, string ValueCodeAnswer, string ValueStatus,int Rewrite_Master)
        //{




        //    string MS = "";
        //    var ObjRun = new mgrSQLConnect(_configuration);      





        //    strSQL = "[dbo].[srpApproved_Reject_Question] '"+ Job + "','"+ ValueStatus + "','"+ seq.ToString() + "','"+ ValueCodeQuestion + "','"+ ValueCodeAnswer + "','"+ Rewrite_Master.ToString() + "'";

        //    dt = ObjRun.GetDatatables(strSQL);
        //    MS = dt.Rows[0][1].ToString();  
        //    return MS;


        //}
        public string Approved_Reject_Question_(string Job, string valueStatus_Array, string seq_Array, string ValueCodeQuestion, int Rewrite_Master)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            int Intpro = 0;
            string StrSql = "";
            string MS = "";
            string ValueCodeAnswer = Get_ValueCodeAnswer(ValueCodeQuestion);



            if (Job == "APP")
            {


                if (Rewrite_Master > 0)
                {
                    //------------------ Back up The last Exam -------
                    StrSql = @"insert into  [SPTOSystem].[dbo].[ValueList]  
	                            ([ValueCode],[DisplayOrder],[Value_HTML],[Value_TEXT],[Answer],[Need]
                                 ,[ValueStatus],[Rewrite],[AddDate],[UpdDate],[UserName],[ComputerName])
                                select [ValueCode],[DisplayOrder],[Value_HTML],[Value_TEXT],[Answer],[Need]
                                ,'OLD',[Rewrite],[AddDate],[UpdDate],[UserName],[ComputerName] from [SPTOSystem].[dbo].[ValueList]
                                 where(ValueCode = '" + ValueCodeQuestion + "' or ValueCode = '" + ValueCodeAnswer + "') and  [ValueStatus] != 'NEW'    and Rewrite = '" + Rewrite_Master.ToString() + "' ";
                    Intpro = ObjRun.ExecProc(StrSql);
                    if (Intpro < 0)
                    {
                        return "False";
                    }

                }


                //--------------- Update -----------------
                StrSql = strSQL = "[dbo].[sprApproved_Reject_Question] '" + Job + "','" + valueStatus_Array + "','" + seq_Array + "','" + ValueCodeQuestion + "','" + ValueCodeAnswer + "','" + Rewrite_Master.ToString() + "'";
                dt = ObjRun.GetDatatables(strSQL);
                MS = dt.Rows[0][1].ToString();

                if (MS != "OK")
                {
                    StrSql = @"delete [SPTOSystem].[dbo].[ValueList]  where
                         (ValueCode = '" + ValueCodeQuestion + "' or ValueCode = '" + ValueCodeAnswer + "') and ValueStatus ='OLD' and Rewrite = '" + Rewrite_Master.ToString() + "'  ";
                    Intpro = ObjRun.ExecProc(StrSql);

                    return "False";
                }
                else
                {
                    Rewrite_Master = Rewrite_Master + 1;
                    StrSql = @" update [SPTOSystem].[dbo].[InputItem] set [Rewrite] = '" + Rewrite_Master.ToString() + "' ,  [UpdDate] = getdate() where [ValueCodeQuestion]='" + ValueCodeQuestion + "' and [ValueCodeAnswer] = '" + ValueCodeAnswer + "' ";
                    Intpro = ObjRun.ExecProc(StrSql);
                }


            }
            else {


                //--------------- Update -----------------
                  strSQL = "[dbo].[sprApproved_Reject_Question] '" + Job + "','" + valueStatus_Array + "','" + seq_Array + "','" + ValueCodeQuestion + "','" + ValueCodeAnswer + "','" + Rewrite_Master.ToString() + "'";
                dt = ObjRun.GetDatatables(strSQL);
                MS = dt.Rows[0][1].ToString();



            }
                  

            return MS;

        }

        public string Approved_Reject_Question(string Job, string[] valueStatus_Array, int[] seq_Array, string ValueCodeQuestion, int Rewrite_Master)
        {

            var ObjRun = new mgrSQLConnect(_configuration);

            var constr = _configuration.GetConnectionString("CONSPTO");
            var dt = new DataTable();
            int seq;
            int Count = 0;
            string ValueCodeAnswer = Get_ValueCodeAnswer(ValueCodeQuestion);


            var ds = new DataSet();

            using (var connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlCommand objSqlCmd = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();

                foreach (string Status in valueStatus_Array)
                {
                    seq = seq_Array[Count];
                    try
                    {
                        objSqlCmd.Connection = connection;
                        objSqlCmd.Transaction = transaction;

                        objSqlCmd.CommandType = CommandType.StoredProcedure;
                        objSqlCmd.CommandText = "sprApproved_Reject_Question";
                        objSqlCmd.Connection = connection;
                        objSqlCmd.Parameters.Clear();
                        objSqlCmd.Parameters.Add("@Job", SqlDbType.VarChar).Value = Job;
                        objSqlCmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;
                        objSqlCmd.Parameters.Add("@Seq", SqlDbType.Int).Value = seq;
                        objSqlCmd.Parameters.Add("@valueCodeQuestion", SqlDbType.VarChar).Value = ValueCodeQuestion;
                        objSqlCmd.Parameters.Add("@valueCodeAnswer", SqlDbType.VarChar).Value = ValueCodeAnswer;
                        objSqlCmd.Transaction = transaction;

                        dt = ObjRun.GetDataTable(objSqlCmd);
                    }
                    catch (Exception e)
                    {
                        var dsa = e;
                        transaction.Rollback();
                    }
                    Count = Count + 1;
                }

                transaction.Commit();
            }
            return "555";
        }



    }
}

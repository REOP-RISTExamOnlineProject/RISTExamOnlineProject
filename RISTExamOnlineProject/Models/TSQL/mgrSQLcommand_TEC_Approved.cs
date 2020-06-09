using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand_TEC_Approved
    {
        private readonly IConfiguration _configuration;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private string strSQL = "";


        public mgrSQLcommand_TEC_Approved(IConfiguration configuration)
        {
            _configuration = configuration;
        }





        public List<vewOperatorReqChange_Groupby> Get_ApprovedDetailGroup()
        {

            List<vewOperatorReqChange_Groupby> Detail = new List<vewOperatorReqChange_Groupby>();


            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT       [DocNo]      ,[ReqOperatorID]     ,CONVERT(varchar, [ReqDate],120) as [ReqDate],count(*) as TotalJob FROM[SPTOSystem].[dbo].[vewOperatorReqChange]";
            strSQL += " where [ChangeOperatorID] is null   or [ChangeOperatorID] = ''    group by[DocNo],[ReqOperatorID], CONVERT(varchar, [ReqDate], 120) order by CONVERT(varchar, [ReqDate], 120) desc ";
            dt = ObjRun.GetDatatables(strSQL);


            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new vewOperatorReqChange_Groupby()
                    {
                        DocNo = row["DocNo"].ToString(),
                        ReqOperatorID = row["ReqOperatorID"].ToString(),
                        ReqDate = row["ReqDate"].ToString(),
                        TotalJob = Convert.ToInt32(row["TotalJob"].ToString()),

                    });

                }



            }
            return Detail;

        }



        public string OperatorReqChange(string Flag, string DocNo,string OperatorID, string SectionCode, string SectionAttribute, string OperatorGroup, string License, string Active, string ReqOperatorID, string ChangeOperatorID)
        {


            var ObjRun = new mgrSQLConnect(_configuration);

            strSQL = "EXEC [dbo].[sprOperatorReqChange] ";
            strSQL += " '" + Flag + "' ,'" + DocNo + "' ,'"+ OperatorID + "','" + SectionCode + "' ,'" + SectionAttribute + "' ,'" + OperatorGroup + "'    ,";
            strSQL += "'" + License + "','" + Active + "'    ,'" + ReqOperatorID + "'    ,'" + ChangeOperatorID + "'     ";

            DataTable dt = ObjRun.GetDatatables(strSQL);
            string check = dt.Rows[0][1].ToString();

            return check;


        }



        public List<vewOperatorReqChange> Get_ApprovedDetail(string DocNO)
        {

            List<vewOperatorReqChange> Detail = new List<vewOperatorReqChange>();


            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT  [Nbr],[DocNo],[Seq]      ,[OperatorID]      ,[SectionCode]      ,[SectionAttribute]      ,[OperatorGroup]      ,[License]      ,[Active]      ,[ReqOperatorID]            ";
            strSQL += " ,CONVERT(varchar, [ReqDate],120) as [ReqDate]      ,IsNULL(ChangeOperatorID, '') as [ChangeOperatorID]      ,IsNULL(CONVERT(varchar, ChangeOperatorID,120) , '') as [ChangeDate]    ";
            strSQL += "  FROM[SPTOSystem].[dbo].[vewOperatorReqChange]  where [DocNo] = '" + DocNO + "' ";
            dt = ObjRun.GetDatatables(strSQL);


            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new vewOperatorReqChange()
                    {

                        Nbr = Convert.ToInt32(row["Nbr"].ToString()),
                        DocNo = row["DocNo"].ToString(),
                        Seq = Convert.ToInt32(row["Seq"].ToString()),
                        OperatorID = row["OperatorID"].ToString(),
                        SectionCode = row["SectionCode"].ToString(),
                        SectionAttribute = row["SectionAttribute"].ToString(),
                        OperatorGroup = row["OperatorGroup"].ToString(),
                        License = row["License"].ToString(),
                        Active = row["Active"].ToString(),
                        ReqOperatorID = row["ReqOperatorID"].ToString(),
                        ReqDate = row["ReqDate"].ToString(),
                        ChangeOperatorID = row["ChangeOperatorID"].ToString(),
                        ChangeDate = row["ChangeDate"].ToString(),



                    });

                }



            }
            return Detail;

        }

    }
}

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
            strSQL = "SELECT       [DocNo]      ,[ReqOperatorID]     ,CONVERT(varchar, [ReqDate],120) as [ReqDate],count(*) as TotalJob FROM [SPTOSystem].[dbo].[vewOperatorReqChange]";
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



        public string OperatorReqChange(string Flag, string DocNo, string OperatorID, string SectionCode, string SectionAttribute, string OperatorGroup, string License, string Active, string ReqOperatorID, string ChangeOperatorID)
        {


            var ObjRun = new mgrSQLConnect(_configuration);

            strSQL = "EXEC [dbo].[sprOperatorReqChange] ";
            strSQL += " '" + Flag + "' ,'" + DocNo + "' ,'" + OperatorID + "','" + SectionCode + "' ,'" + SectionAttribute + "' ,'" + OperatorGroup + "'    ,";
            strSQL += "'" + License + "','" + Active + "'    ,'" + ReqOperatorID + "'    ,'" + ChangeOperatorID + "'     ";

            DataTable dt = ObjRun.GetDatatables(strSQL);
            string check = dt.Rows[0][1].ToString();

            return check;


        }

        public List<ReqChangeCompareData> GetCompareData(int Nbr)
        {

            List<ReqChangeCompareData> Detail = new List<ReqChangeCompareData>();

            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT  [New_Section],REPLACE([New_SectionAttribute], ';', ',') as  New_SectionAttribute,[New_GroupName],REPLACE([New_License], ';', ',') as [New_License]";
            strSQL += " ,[New_Active],[GroupName],[Section],REPLACE([SectionAttribute], ';', ',') as  SectionAttribute      ,REPLACE([License], ';', ',') as [License]      ,[Active]   ,CONVERT(varchar, [ReqDate],120) as [ReqDate]     ";
            strSQL += "  FROM[SPTOSystem].[dbo].[vewOperatorReqChangeCompare]  where [Nbr] = '" + Nbr + "' ";
            dt = ObjRun.GetDatatables(strSQL);

            DataTable tempData = new DataTable();
            tempData.Clear();
            tempData.Columns.Add("Catagory");
            tempData.Columns.Add("New");
            tempData.Columns.Add("present");

            DataRow _ravi = tempData.NewRow();
            _ravi["Catagory"] = "Section";
            _ravi["New"] = dt.Rows[0]["New_Section"].ToString();
            _ravi["present"] = dt.Rows[0]["Section"].ToString();
            tempData.Rows.Add(_ravi);

            _ravi = tempData.NewRow();
            _ravi["Catagory"] = "SectionAttribute";
            _ravi["New"] = dt.Rows[0]["New_SectionAttribute"].ToString();
            _ravi["present"] = dt.Rows[0]["SectionAttribute"].ToString();
            tempData.Rows.Add(_ravi);

            _ravi = tempData.NewRow();
            _ravi["Catagory"] = "GroupName";
            _ravi["New"] = dt.Rows[0]["New_GroupName"].ToString();
            _ravi["present"] = dt.Rows[0]["GroupName"].ToString();
            tempData.Rows.Add(_ravi);

            _ravi = tempData.NewRow();
            _ravi["Catagory"] = "License";
            _ravi["New"] = dt.Rows[0]["New_License"].ToString();
            _ravi["present"] = dt.Rows[0]["License"].ToString();
            tempData.Rows.Add(_ravi);

            _ravi = tempData.NewRow();
            _ravi["Catagory"] = "Active";
            _ravi["New"] = dt.Rows[0]["New_Active"].ToString();
            _ravi["present"] = dt.Rows[0]["Active"].ToString();
            tempData.Rows.Add(_ravi);


            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in tempData.Rows)
                {
                    Detail.Add(new ReqChangeCompareData()
                    {
                        Catagory = row["Catagory"].ToString(),
                        New = row["New"].ToString(),
                        Present = row["Present"].ToString(),


                    });

                }
            }
              






            return Detail;
        }

        public List<vewOperatorReqChangeCompare> Get_ApprovedDetail(string DocNO)
        {


            List<vewOperatorReqChangeCompare> Detail = new List<vewOperatorReqChangeCompare>();


            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT  [DocNo],[Nbr],[Seq],[OperatorID],[New_SectionCode],[New_Section],[New_SectionAttribute],[New_OperatorGroup],[New_GroupName],REPLACE([New_License], ';', ',') as [New_License]";
            strSQL += " ,[New_Active],[ReqOperatorID],[GroupName],[SectionCode],[Section],[SectionAttribute]      ,[License]      ,[Active]   ,CONVERT(varchar, [ReqDate],120) as [ReqDate]  ,ChangeOperatorID   ";
            strSQL += "  FROM[SPTOSystem].[dbo].[vewOperatorReqChangeCompare]  where [DocNo] = '" + DocNO + "'  and ([ChangeOperatorID] is null   or [ChangeOperatorID] = '' ) ";
            dt = ObjRun.GetDatatables(strSQL);


            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new vewOperatorReqChangeCompare()
                    {


                        DocNo = row["DocNo"].ToString(),
                        Nbr = Convert.ToInt32(row["Nbr"].ToString()),
                        Seq = row["Seq"].ToString(),
                        OperatorID = row["OperatorID"].ToString(),
                        New_SectionCode = row["New_SectionCode"].ToString(),
                        New_Section = row["New_Section"].ToString(),
                        New_SectionAttribute = row["New_SectionAttribute"].ToString(),
                        New_OperatorGroup = row["New_OperatorGroup"].ToString(),
                        New_GroupName = row["New_GroupName"].ToString(),
                        New_License = row["New_License"].ToString(),
                        New_Active = row["New_Active"].ToString(),
                        ReqOperatorID = row["ReqOperatorID"].ToString(),
                        GroupName = row["GroupName"].ToString(),
                        SectionCode = row["SectionCode"].ToString(),
                        Section = row["Section"].ToString(),
                        SectionAttribute = row["SectionAttribute"].ToString(),
                        License = row["License"].ToString(),
                        Active = row["Active"].ToString(),
                        ReqDate = row["ReqDate"].ToString(),
                        ChangeOperatorID = row["ChangeOperatorID"].ToString(),


                        //Nbr = Convert.ToInt32(row["Nbr"].ToString()),
                        //DocNo = row["DocNo"].ToString(),
                        //Seq = Convert.ToInt32(row["Seq"].ToString()),
                        //OperatorID = row["OperatorID"].ToString(),
                        //SectionCode = row["SectionCode"].ToString(),
                        //SectionAttribute = row["SectionAttribute"].ToString(),
                        //OperatorGroup = row["OperatorGroup"].ToString(),
                        //License = row["License"].ToString(),
                        //Active = row["Active"].ToString(),
                        //ReqOperatorID = row["ReqOperatorID"].ToString(),
                        //ReqDate = row["ReqDate"].ToString(),
                        //ChangeOperatorID = row["ChangeOperatorID"].ToString(),
                        //ChangeDate = row["ChangeDate"].ToString(),



                    });

                }



            }
            return Detail;

        }



        public List<reqeustInquiry> GetRequestList()
        {

            List<reqeustInquiry> Detail = new List<reqeustInquiry>();


            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT    * FROM[SPTOSystem].[dbo].[vewOperatorReqChange]";
            strSQL += "  ";
            dt = ObjRun.GetDatatables(strSQL);


            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new reqeustInquiry()
                    {
                        DocNo = row["DocNo"].ToString(),
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

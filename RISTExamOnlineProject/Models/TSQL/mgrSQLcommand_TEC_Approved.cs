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





        public List<vewOperatorReqChange_Groupby> Get_ApprovedDetailGroup() {

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

                    }) ;

                }



            }
            return Detail;

        }

    }
}

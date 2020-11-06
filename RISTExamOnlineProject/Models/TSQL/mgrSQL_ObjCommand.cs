using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQL_ObjCommand
    {

        private readonly IConfiguration _configuration;




        public mgrSQL_ObjCommand(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable(); 

   


        public int ExecProc(SqlCommand objCmd)
        {
            var constr = _configuration.GetConnectionString("CONSPTO");
            int iniRet = 0;
            using (var Con = new SqlConnection(constr))
            {
                using (objCmd)
                {
                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }
                    objCmd.Connection = Con;
                    iniRet = objCmd.ExecuteNonQuery();
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
            return (iniRet);
        }


        public DataTable GetDataTable(SqlCommand objCmd)
        {

            var constr = _configuration.GetConnectionString("CONSPTO");

            SqlDataAdapter objDataAdp;
            using (var Con = new SqlConnection(constr))
            {
                using (objCmd)
                {
                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }

                    dt = new DataTable();
                    objCmd.Connection = Con;
                    objDataAdp = new SqlDataAdapter(objCmd);
                    objDataAdp.SelectCommand = objCmd;
                    objDataAdp.Fill(dt);

                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }


            return dt;
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

        
    }
}

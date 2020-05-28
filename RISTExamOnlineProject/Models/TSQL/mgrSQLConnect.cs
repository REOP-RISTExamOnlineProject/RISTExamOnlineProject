using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLConnect
    {
        private readonly IConfiguration configuration;
        private DataSet ds = new DataSet();


        private DataTable dt = new DataTable();
        private string strSQL = "";

        public mgrSQLConnect(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DataTable GetPosition_()
        {
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT  [Position] FROM [SPTOSystem].[dbo].[Operator]  group by [Position]  order by [Position]";

            dt = GetDatatables(strSQL);

            return dt;
        }

        public DataTable GetDatatables(string Sql)
        {
            var constr = configuration.GetConnectionString("CONSPTO");
            var dt = new DataTable();
            try
            {
                var query = Sql;

                var ds = new DataSet();
                using (var con = new SqlConnection(constr))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        var adpterdata = new SqlDataAdapter();
                        adpterdata.SelectCommand = new SqlCommand(query, con);
                        adpterdata.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e;
                return dt;
            }
        }

        public DataSet GetDataSet(string Sql)
        {
            var ds = new DataSet();
            try
            {
                var constr = configuration.GetConnectionString("CONSPTO");
                var query = Sql;
                //string constr = ConfigurationManager.ConnectionStrings["BOIDbContext1"].ConnectionString; 
                using (var con = new SqlConnection(constr))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        var adpterdata = new SqlDataAdapter();
                        adpterdata.SelectCommand = new SqlCommand(query, con);
                        adpterdata.Fill(ds);
                        con.Close();
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e;
                return ds;
            }
        }

        public string[] GetDataExcute(string Sql)
        {
            string[] DataReturn;
            string DataMgs;
            var results = true;
            var dt = new DataTable();
            var constr = configuration.GetConnectionString("CONSPTO");
            try
            {
                var query = Sql;
                var rowsAffected = 0;
                var ds = new DataSet();
                using (var con = new SqlConnection(constr))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        var adpterdata = new SqlDataAdapter();
                        adpterdata.InsertCommand = new SqlCommand(query, con);
                        rowsAffected = adpterdata.InsertCommand.ExecuteNonQuery();
                        con.Close();


                        if (rowsAffected > 0)
                        {
                            results = true;
                            DataMgs = "OK";
                        }
                        else
                        {
                            results = false;
                            DataMgs = "Please Contect to IS.";
                        }
                    }


                    // return results;
                }
            }
            catch (Exception e)
            {
                DataMgs = e.Message + ":" + Sql;
                results = false;
            }

            DataReturn = new[] {results.ToString(), DataMgs};

            return DataReturn;
        }
    }
}
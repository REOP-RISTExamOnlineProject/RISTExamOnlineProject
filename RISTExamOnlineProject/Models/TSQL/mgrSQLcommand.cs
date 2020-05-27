using System.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand
    {
        private IConfiguration configuration;

        public mgrSQLcommand(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public DataTable GetDatatables(string Sql )
        {
            string constr = configuration.GetConnectionString("CONSPTO");
            DataTable dt = new DataTable();
            try
            {
                string query = Sql;

                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter();
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
        public DataSet GetDataSet(string Sql )
        {
            DataSet ds = new DataSet();
            try
            {
                string constr = configuration.GetConnectionString("CONSPTO");
                string query = Sql;
                //string constr = ConfigurationManager.ConnectionStrings["BOIDbContext1"].ConnectionString; 
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter();
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
        public string[] GetDataExcute(string Sql )
        {
            string[] DataReturn;
            string DataMgs;
            Boolean results = true;
            DataTable dt = new DataTable();
            string constr = configuration.GetConnectionString("CONSPTO");
            try
            {

                string query = Sql;
                int rowsAffected = 0;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter();
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

            DataReturn = new string[] { results.ToString(), DataMgs };

            return DataReturn;
        }




    }
} 
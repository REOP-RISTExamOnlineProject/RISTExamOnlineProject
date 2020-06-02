using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand
    {
        private readonly IConfiguration _configuration;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private string strSQL = "";

        public mgrSQLcommand(IConfiguration configuration)
        {
            _configuration = configuration;
        } 
        public DataTable GetSectionCode(string strDivision, string strDepartment)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";
            int Chk = 0;
            strSQL += "SELECT [SectionCode],[Section],[Department],[Division]";
            strSQL += "FROM [SPTOSystem].[dbo].[vewT_Section_Master] ";
            if (strDivision != "" && strDivision != null)
            {
                strSQL += Chk == 0 ? " Where  " : " and  ";

                strSQL += "Substring(sectionCode,1,1) = '" + strDivision + "'";
                Chk++;
            }
            if (strDepartment != "" && strDepartment != null)
            {
                strSQL += Chk == 0 ? " Where  " : " and  ";

                strSQL += "Substring(sectionCode,1,2) = '" + strDepartment + "'";
                Chk++;
            } 
            strSQL += "group by  [SectionCode],[Section],[Department],[Division]  "; 
            dt = ObjRun.GetDatatables(strSQL); 
            return dt;
        }

        public DataTable GetDepartment(string strDivision)
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";
            int Chk = 0;
            strSQL += "SELECT Substring(sectionCode,1,2) as sectionCode , [Department]" +
                "FROM [SPTOSystem].[dbo].[vewT_Section_Master] ";

            if (strDivision != "" && strDivision != null)
            {
                strSQL += Chk == 0 ? " Where  " : " and  "; 
                strSQL += "Substring(sectionCode,1,1) = '" + strDivision + "'"; 
                Chk++;
            }
            strSQL += "group by Substring(sectionCode,1,2),[Department] ";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }
        public DataTable GetDivision()
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT Substring(sectionCode,1,1) as sectionCode  ,[Division] " +
                "FROM [SPTOSystem].[dbo].[vewT_Section_Master] ";
            strSQL += " group by Substring(sectionCode,1,1) ,[Division]  ";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }

        public DataTable GetGroupName()
        {
         
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT [OperatorGroup] ,[GroupName] FROM [SPTOSystem].[dbo].[OperatorGroup]";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }


        public List<vewOperatorLicense> GetUserLicense(string Opid)
        {
            
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            List<vewOperatorLicense> dataList = new List<vewOperatorLicense>();
            dt = new DataTable();
            strSQL = "";
            strSQL += "SELECT * FROM [SPTOSystem].[dbo].vewOperatorLicense  where OperatorID ='"+ Opid + "'";
            dt = ObjRun.GetDatatables(strSQL); 
            if (dt.Rows.Count != 0)
            { 
                foreach (DataRow row in dt.Rows)
                {
                    dataList.Add(new vewOperatorLicense()
                    {
                        OperatorID = row["OperatorID"].ToString().Trim(),
                        License = row["License"].ToString().Trim(), 
                    });
                }
            }
            return dataList;

        }

        public string[] GetUpdUserdetail(vewOperatorAlls _Data, List<vewOperatorLicense> _DataLicense,string OpNo,string strIpAddress)
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            string DataMgs,strFlag;
            var results = true;
            strSQL = "";
            string[] Result;
            strFlag = "UPD";
            try
            {
                string DataLicense = "";
                foreach(vewOperatorLicense i in _DataLicense)
                {
                    DataLicense += ";" + i.License;
                   
                }


                strSQL += "Exec [sprOperator]";
                strSQL += "'" + strFlag + "',";                                              //flag
                strSQL += "'" + _Data.OperatorID + "',";
                strSQL += "'" + _Data.Password + "',";
                strSQL += "'" + _Data.NameEng + "',";
                strSQL += "N'" + _Data.NameThai + "',";
                strSQL += "'" + _Data.SectionCode + "',";
                strSQL += "'" + _Data.OperatorGroup + "',";
                strSQL += "'" + _Data.Position + "',";
                strSQL += "'" + _Data.JobTitle + "',";
                strSQL += "'" + _Data.Email1 + "',";
                strSQL += "'" + _Data.Email2 + "',";
                strSQL += "'" + _Data.RFID + "',";
                strSQL += "'" + _Data.Authority + "',";
                strSQL += "'" + _Data.Active + "',";
                strSQL += "'" + OpNo + "',";
                strSQL += "'" + strIpAddress + "';";

                
               


                strSQL += "   Exec [sprOperatorLicense]";
                strSQL += "'"+_Data.OperatorID+"'";
                strSQL += ",'" + DataLicense.Substring(1) + "'";
                strSQL += ",'" + OpNo + "'";
                strSQL += ",'" + strIpAddress + "';";



                dt = ObjRun.GetDatatables(strSQL);
                if(dt.Rows.Count > 0)
                {
                    Result = new[] { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString() };
                }
                else
                {
                    results = false;
                    Result = new[] { results.ToString(), "Error " };
                } 
            } 
            catch (Exception e)
            {
                DataMgs = e.Message + ":" + strSQL;
                results = false;
                Result = new[] { results.ToString(), DataMgs };
            } 
            return Result;
        }
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUBRID.Data.CUBRIDClient;
using System.IO;
using System.Drawing;
using System.Data.Common;
using System.Windows.Forms;
using System.ComponentModel;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev.CubridUtils
{
    public class RULE_GETADDRESSBOOKPOPUP
    {
        //CUBRID DB 연결 정보
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");
  
        public void ConnectionToDB()
        {
            dbConnection.Open(); //CUBRID DB Open
        }

        public void DisconnectionToDB()
        {
            if (dbConnection != null)
            {
                dbConnection.Close(); //CUBRID DB Close
            }
        }

        //* MessageSet *//
        string SearchType = string.Empty;
        string Staff_name = string.Empty;
        string Customer_name = string.Empty;
        string EmailAddrs = string.Empty;

        public RULE_GETADDRESSBOOKPOPUP()
        {
            //* MessageSet *//
             SearchType = string.Empty;
             Staff_name = string.Empty;
             Customer_name = string.Empty;
             EmailAddrs = string.Empty;
        }

        //* 조회 *//
        public DataTable Search(Dictionary<string,string> MessageSet)
        {
            ConnectionToDB();

            DataTable ds = new DataTable();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("SearchType"))
                SearchType = MessageSet["SearchType"].ToString();
            if (MessageSet.ContainsKey("Staff_name"))
                Staff_name = MessageSet["Staff_name"].ToString();
            if (MessageSet.ContainsKey("Customer_name"))
                Customer_name = MessageSet["Customer_name"].ToString();
            if (MessageSet.ContainsKey("EmailAddrs"))
                EmailAddrs = MessageSet["EmailAddrs"].ToString();

            if (SearchType == "SearchStaffAddr")
            {
                sql = " SELECT " +
                   "           a.staff_id , " +
                   "           a.staff_name , " +
                   "           a.staff_email , " +
                   "           b.depart_name   " +
                   " FROM " +
                   "       mng_staff a INNER JOIN mng_depart b ON a.depart_id = b.depart_id  " +
                   " WHERE 1 = 1 ";

                //if (EmailAddrs != string.Empty)
                //{
                //    string[] arrayEmailAddrs = EmailAddrs.Split(';');
                //    string strEmailAddrs = string.Empty;

                //    foreach (string strAddr in arrayEmailAddrs)
                //    {
                //        if (strAddr != string.Empty)
                //            strEmailAddrs += "'" + strAddr + "',";
                //    }
                //    strEmailAddrs = strEmailAddrs.Substring(0, strEmailAddrs.Length - 1);
                //    sql += "    AND a.staff_email NOT IN (" + strEmailAddrs + ") ";
                //}

                sql += " ORDER BY a.staff_id  ";

            } else if (SearchType == "SearchCustomAddr")
            {
                sql = " SELECT " +
                   "           customer_id , " +
                   "           customer_name , " +
                   "           customer_staff_name , " +
                   "           customer_staff_email  " +
                   " FROM " +
                   "      mng_customer " +
                   " WHERE 1 = 1 ";

                //if (EmailAddrs != string.Empty)
                //{
                //    string[] arrayEmailAddrs = EmailAddrs.Split(';');
                //    string strEmailAddrs = string.Empty;

                //    foreach (string strAddr in arrayEmailAddrs)
                //    {
                //        strEmailAddrs += "'" + strAddr + "',";
                //    }
                //    strEmailAddrs = strEmailAddrs.Substring(0, strEmailAddrs.Length - 1);
                //    sql += "    AND customer_staff_email NOT IN (" + strEmailAddrs + ") ";
                //}

                sql += " ORDER BY customer_id  ";
            }

            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
                {

                    da.Fill(ds);

                    DisconnectionToDB();

                    return ds;
                }            
        }
    }
}

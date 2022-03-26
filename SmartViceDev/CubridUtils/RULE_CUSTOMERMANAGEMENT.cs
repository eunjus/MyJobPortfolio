using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUBRID.Data.CUBRIDClient;
using System.Drawing;
using System.Data.Common;
using System.IO;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev.CubridUtils
{
    public class RULE_CUSTOMERMANAGEMENT
    {
        //CUBRID DB 연결 정보
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");

        //* MessageSet *//
        string QueryType = string.Empty;
        string SearchType = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string customer_id = string.Empty;
        string customer_name = string.Empty;
        string customer_tel = string.Empty;
        string customer_fax = string.Empty;
        string business_number = string.Empty;
        string customer_address = string.Empty;
        string customer_staff_name = string.Empty;
        string customer_staff_tel = string.Empty;
        string customer_staff_email = string.Empty;
        string customer_homepage = string.Empty;
        string COMMENT = string.Empty;

        public RULE_CUSTOMERMANAGEMENT()
        {
            //* MessageSet *//
            QueryType = string.Empty;
            SearchType = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            customer_id = string.Empty;
            customer_name = string.Empty;
            customer_tel = string.Empty;
            customer_fax = string.Empty;
            business_number = string.Empty;
            customer_address = string.Empty;
            customer_staff_name = string.Empty;
            customer_staff_tel = string.Empty;
            customer_staff_email = string.Empty;
            customer_homepage = string.Empty;
            COMMENT = string.Empty;
        }

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
        
        //공통 데이터 가져오는 쿼리 - 공통데이터 가져오는 cs 파일을 따로 하나 만드는게 나을듯.
        public DataTable SearchCommon(string searchtype, Dictionary<string, string> MessageSet)
         {
            ConnectionToDB();

            DataTable ds = new DataTable();

            //if (MessageSet.ContainsKey("Staff_Nm"))
            //    Staff_Nm = MessageSet["Staff_Nm"].ToString();
            //if (MessageSet.ContainsKey("Staff_Id"))
            //    Staff_Id = MessageSet["Staff_Id"].ToString();

            if (searchtype == "seachStaffInfo")
            {
                string sql = " SELECT" +
                    "                 staff_id , " +
                    "                 staff_name  " +       
                    " 	        FROM 	mng_staff" +                    
                    "           WHERE 1 = 1";

                //if (Staff_Id != string.Empty)
                //    sql += "    AND staff_id = '" + Staff_Id + "' ";

                //if (Staff_Nm != string.Empty)
                //    sql += "    AND staff_name = '" + Staff_Nm + "' ";

                using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
                {

                    da.Fill(ds);

                    DisconnectionToDB();

                    return ds;
                }
            }
            return ds;
        }

        //* 조회 *//
        public DataSet Search(Dictionary<string,string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("SearchType"))
                SearchType = MessageSet["SearchType"].ToString();
            if (MessageSet.ContainsKey("customer_id"))
                customer_id = MessageSet["customer_id"].ToString();
            else
                customer_id = string.Empty;
            if (MessageSet.ContainsKey("customer_name"))
                customer_name = MessageSet["customer_name"].ToString();
            else
                customer_name = string.Empty;
            if (MessageSet.ContainsKey("customer_staff_name"))
                customer_staff_name = MessageSet["customer_staff_name"].ToString();
            else
                customer_staff_name = string.Empty;
            if (MessageSet.ContainsKey("StartDate"))
                StartDate = MessageSet["StartDate"].ToString();
            if (MessageSet.ContainsKey("EndDate"))
                EndDate = MessageSet["EndDate"].ToString();

            if (SearchType == "MainSearch")
            {
                sql = " SELECT" +
                        "        a.customer_id , " +
                        "        a.customer_name ," +
                        "        a.customer_tel , " +
                        "        a.customer_fax , " +
                        "        a.business_number , " +
                        "        a.customer_address , " +
                        "        a.customer_staff_name , " +
                        "        a.customer_staff_tel , " +
                        "        a.customer_staff_email , " +
                        "        a.customer_homepage , " +
                        "        b.bank_name , " +
                        "    	 b.account_number, " +
                        "        a.COMMENT , " +
                        "        TO_CHAR(a.reg_dt, 'YYYY-MM-DD') reg_dt , " +
                        "        TO_CHAR(a.update_dt, 'YYYY-MM-DD') update_dt  " +
                        "   FROM " +
                        "               mng_customer a 	LEFT JOIN mng_bank b ON a.customer_id = b.id  " +
                        "   WHERE 1 = 1  ";

                //if (StartDate != string.Empty && EndDate != string.Empty)
                //    sql += "  AND a.pay_month BETWEEN TO_DATETIME('" + StartDate + "', 'YYYY-MM-DD') AND " + " TO_DATETIME('" + EndDate + "', 'YYYY-MM-DD') ";
                if (customer_id != string.Empty)
                    sql += "    AND a.customer_id = '" + customer_id + "' ";
                if (customer_name != string.Empty)
                    sql += "    AND a.customer_name LIKE '" + customer_name + "%' ";
                if (customer_staff_name != string.Empty)
                    sql += "    AND a.customer_staff_name LIKE '" + customer_staff_name + "%' ";
                //sql += " ORDER BY work_year ";       
            }
            else if (SearchType == "GetCustomerId")
            {
                sql = " SELECT" +
                        "        TO_NUMBER(MAX(SUBSTR(customer_id,7,6))) + 1 new_id  " +
                        "   FROM " +
                        "       mng_customer " +
                        "   WHERE 1 = 1  ";                
            }
            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {
                
                da.Fill(ds);

                DisconnectionToDB();

                return ds;
            }                
        }       

        //* 저장 - 추가,삭제,갱신 기능
        public bool Save(Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (MessageSet.ContainsKey("QueryType"))
                QueryType = MessageSet["QueryType"].ToString();
            else
                QueryType = string.Empty;
            if (MessageSet.ContainsKey("customer_id"))
                customer_id = MessageSet["customer_id"].ToString();
            else
                customer_id = string.Empty;
            if (MessageSet.ContainsKey("customer_name"))
                customer_name = MessageSet["customer_name"].ToString();
            else
                customer_name = string.Empty;
            if (MessageSet.ContainsKey("customer_tel"))
                customer_tel = MessageSet["customer_tel"].ToString();
            else
                customer_tel = string.Empty;
            if (MessageSet.ContainsKey("customer_fax"))
                customer_fax = MessageSet["customer_fax"].ToString();
            else
                customer_fax = string.Empty;
            if (MessageSet.ContainsKey("business_number"))
                business_number = MessageSet["business_number"].ToString();
            else
                business_number = string.Empty;
            if (MessageSet.ContainsKey("customer_address"))
                customer_address = MessageSet["customer_address"].ToString();
            else
                customer_address = string.Empty;
            if (MessageSet.ContainsKey("customer_staff_name"))
                customer_staff_name = MessageSet["customer_staff_name"].ToString();
            else
                customer_staff_name = string.Empty;
            if (MessageSet.ContainsKey("customer_staff_tel"))
                customer_staff_tel = MessageSet["customer_staff_tel"].ToString();
            else
                customer_staff_tel = string.Empty;
            if (MessageSet.ContainsKey("customer_staff_email"))
                customer_staff_email = MessageSet["customer_staff_email"].ToString();
            else
                customer_staff_email = string.Empty;
            if (MessageSet.ContainsKey("customer_homepage"))
                customer_homepage = MessageSet["customer_homepage"].ToString();
            else
                customer_homepage = string.Empty;
            if (MessageSet.ContainsKey("COMMENT"))
                COMMENT = MessageSet["COMMENT"].ToString();
            else
                COMMENT = string.Empty;


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_customer " +
                    "                    ( " +                    
                    "	           customer_id,     " +
                    "	           customer_name,     " +
                    "	           customer_tel,     " +
                    "	           customer_fax,     " +
                    "	           business_number,     " +
                    "	           customer_address,     " +
                    "	           customer_staff_name,     " +
                    "	           customer_staff_tel,     " +
                    "	           customer_staff_email,     " +
                    "	           customer_homepage,     " +                    
                    "	           COMMENT,     " +
                    "	           reg_dt,     " +
                    "	           update_dt " +
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +                                    
                    "          '" + customer_id + "',  " +
                    "          '" + customer_name + "',  ";
                if (customer_tel != string.Empty)
                    sql += "          " + customer_tel + ",  ";
                else
                    sql += "          null,  ";

                if (customer_fax != string.Empty)
                    sql += "          " + customer_fax + ",  ";
                else
                    sql += "          null,  ";

                if (business_number != string.Empty)
                    sql += "          " + business_number + ",  ";
                else
                    sql += "          null,  ";

                if (customer_address != string.Empty)
                    sql += "          '" + customer_address + "',  ";
                else
                    sql += "          null,  ";

                if (customer_staff_name != string.Empty)
                    sql += "          '" + customer_staff_name + "',  ";
                else
                    sql += "          null,  ";

                if (customer_staff_tel != string.Empty)
                    sql += "          " + customer_staff_tel + ",  ";
                else
                    sql += "          null,  ";

                if (customer_staff_email != string.Empty)
                    sql += "          '" + customer_staff_email + "',  ";
                else
                    sql += "          null,  ";

                if (customer_homepage != string.Empty)
                    sql += "          '" + customer_homepage + "',  ";
                else
                    sql += "          null,  ";                

                if (COMMENT != string.Empty)
                    sql += "          '" + COMMENT + "',  ";
                else
                    sql += "          null,  ";

                sql += "          CURRENT_DATE,  " +
                       "          CURRENT_DATE  " +
                    "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                    "            mng_customer " +
                    " 	SET ";                                             
                if (customer_name != string.Empty)
                    sql += "    customer_name = '" + customer_name + "',  ";
                else
                    sql += "    customer_name =  null,  ";

                if (customer_tel != string.Empty)
                    sql += "    customer_tel = " + customer_tel + ",  ";
                else
                    sql += "    customer_tel = null,  ";

                if (customer_fax != string.Empty)
                    sql += "    customer_fax = " + customer_fax + ",  ";
                else
                    sql += "    customer_fax =  null,  ";

                if (business_number != string.Empty)
                    sql += "    business_number = " + business_number + ",  ";
                else
                    sql += "    business_number = null,  ";

                if (customer_address != string.Empty)
                    sql += "    customer_address = '" + customer_address + "',  ";
                else
                    sql += "    customer_address = null,  ";

                if (customer_staff_name != string.Empty)
                    sql += "     customer_staff_name = '" + customer_staff_name + "',  ";
                else
                    sql += "     customer_staff_name = null,  ";

                if (customer_staff_tel != string.Empty)
                    sql += "    customer_staff_tel = " + customer_staff_tel + ",  ";
                else
                    sql += "    customer_staff_tel = null,  ";

                if (customer_staff_email != string.Empty)
                    sql += "    customer_staff_email = '" + customer_staff_email + "',  ";
                else
                    sql += "    customer_staff_email = null,  ";

                if (customer_homepage != string.Empty)
                    sql += "    customer_homepage = '" + customer_homepage + "',  ";
                else
                    sql += "    customer_homepage = null,  ";                

                if (COMMENT != string.Empty)
                    sql += "   COMMENT = '" + COMMENT + "',  ";
                else
                    sql += "  COMMENT = null,  ";

                sql += " 	update_dt = CURRENT_DATE " +
                        " 	WHERE  1 = 1" +
                        "           AND     customer_id = '" + customer_id + "'  ";
            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_customer " +
                " 	WHERE " +
                "        customer_id = '" + customer_id + "' ";
            }
            using (CUBRIDCommand com = new CUBRIDCommand(sql, dbConnection))
            {
                try
                {                  
                    com.ExecuteNonQuery();

                    DisconnectionToDB();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}

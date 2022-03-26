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
    public class RULE_CHARGEFOODMANAGEMENT
    {
        //CUBRID DB 연결 정보
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");

        //* MessageSet *//
        string QueryType = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string Valid_id = string.Empty;
        string Monthly_date = string.Empty;
        string Staff_Id = string.Empty;
        string Staff_Nm = string.Empty;
        string Food_place = string.Empty;
        string Use_times = string.Empty;
        string Food_cost = string.Empty;

        public RULE_CHARGEFOODMANAGEMENT()
        {
             QueryType = string.Empty;
             StartDate = string.Empty;
             EndDate = string.Empty;
             Valid_id = string.Empty;
             Monthly_date = string.Empty;
             Staff_Id = string.Empty;
             Staff_Nm = string.Empty;
             Food_place = string.Empty;
             Use_times = string.Empty;
             Food_cost = string.Empty;
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

            if (MessageSet.ContainsKey("Staff_Nm"))
                Staff_Nm = MessageSet["Staff_Nm"].ToString();
            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();

            if (searchtype == "seachStaffInfo")
            {
                string sql = " SELECT" +
                    "                 staff_id , " +
                    "                 staff_name  " +       
                    " 	        FROM 	mng_staff" +                    
                    "           WHERE 1 = 1";

                if (Staff_Id != string.Empty)
                    sql += "    AND staff_id = '" + Staff_Id + "' ";

                if (Staff_Nm != string.Empty)
                    sql += "    AND staff_name = '" + Staff_Nm + "' ";

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

            if (MessageSet.ContainsKey("QueryType"))
                QueryType = MessageSet["QueryType"].ToString();
            if (MessageSet.ContainsKey("StartDate"))
                StartDate = MessageSet["StartDate"].ToString();
            if (MessageSet.ContainsKey("EndDate"))
                EndDate = MessageSet["EndDate"].ToString();

            if (QueryType == "GetDataByDay")
            {
                sql = " SELECT" +
                        "        a.valid_id , " +
                        "        TO_CHAR(a.monthly_date, 'YYYY-MM-DD') monthly_date, " +
                        "        a.staff_id ," +
                        "        b.staff_name , " +
                        "        a.food_place, " +
                        "        a.use_times, " +
                        "        a.food_cost " +
                        "   FROM " +
                        "               mng_charge_food a INNER JOIN mng_staff b ON a.staff_id = b.staff_id  " +
                        "   WHERE 1 = 1  ";
                if (StartDate != string.Empty && EndDate != string.Empty)
                    sql += "  AND a.monthly_date BETWEEN TO_DATETIME('" + StartDate + "', 'YYYY-MM-DD') AND " + " TO_DATETIME('" + EndDate + "', 'YYYY-MM-DD') ";

                sql += " ORDER BY " +
                    " a.monthly_date DESC, " +
                    " a.staff_id ";
            }
            else if (QueryType == "GetDataByMonth")
            {
                sql = " SELECT" +
                       "        a.valid_id , " +
                       "        TO_CHAR(a.monthly_date, 'YYYY-MM') monthly_date, " +
                       "        a.staff_id ," +
                       "        b.staff_name , " +
                       "        a.food_place, " +
                       "        SUM(a.use_times) use_times, " +
                       "        TO_NUMBER(AVG (a.food_cost)) food_cost " +
                       "   FROM " +
                       "               mng_charge_food a INNER JOIN mng_staff b ON a.staff_id = b.staff_id  " +
                       "   WHERE 1 = 1  ";
                if (StartDate != string.Empty && EndDate != string.Empty)
                    sql += "  AND a.monthly_date BETWEEN TO_DATETIME('" + StartDate + "', 'YYYY-MM-DD') AND " + " TO_DATETIME('" + EndDate + "', 'YYYY-MM-DD') ";

                sql += " GROUP BY " +
                    "           TO_CHAR(a.monthly_date, 'YYYY-MM'), " +
                    "           a.staff_id, " +
                    "           b.staff_name, " +
                    "           a.food_place " +
                    "   ORDER BY " +
                    "           a.monthly_date DESC, " +
                    "           a.staff_id ";

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
            if (MessageSet.ContainsKey("valid_id"))
                Valid_id = MessageSet["valid_id"].ToString();
            if (MessageSet.ContainsKey("monthly_date"))
                Monthly_date = MessageSet["monthly_date"].ToString();
            if (MessageSet.ContainsKey("staff_id"))
                Staff_Id = MessageSet["staff_id"].ToString();
            if (MessageSet.ContainsKey("food_place"))
                Food_place = MessageSet["food_place"].ToString();
            if (MessageSet.ContainsKey("use_times"))
                Use_times = MessageSet["use_times"].ToString();    
            if (MessageSet.ContainsKey("food_cost"))
                Food_cost = MessageSet["food_cost"].ToString();


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_charge_food " +
                    "                    ( " +
                    "           valid_id, " +
                    "           monthly_date, " +
                    "           staff_id, " +
                    "           food_place, " +
                    "           use_times," +
                    "           food_cost " +   
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "        '" + Valid_id + "', " +
                    "       TO_DATETIME('" + Monthly_date + "', 'YYYY-MM-DD'), " +
                    "        '" + Staff_Id + "', " +
                    "        '" + Food_place + "', " +
                    "        " + Use_times + ", " +    
                    "        " + Food_cost + "   " +                                         
                    "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                    "            mng_charge_food " +
                    " 	SET " +
                    "           monthly_date = TO_DATETIME('" + Monthly_date + "', 'YYYY-MM-DD')," +
                    "           staff_id = '" + Staff_Id + "' , " +
                    "           food_place = '" + Food_place + "' , " +
                    "           use_times = " + Use_times + " , " +
                    "           food_cost = " + Food_cost + " " +
                    " 	WHERE " +
                    "        valid_id = '" + Valid_id + "' ";
            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_charge_food " +
                       " 	WHERE " +
                       "        valid_id = '" + Valid_id + "' ";
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

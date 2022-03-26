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
    public class RULE_STAFFSALARYMANAGEMENT
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

            if (MessageSet.ContainsKey("StartDate"))
                StartDate = MessageSet["StartDate"].ToString();
            if (MessageSet.ContainsKey("EndDate"))
                EndDate = MessageSet["EndDate"].ToString();

            sql = "	WITH " +
                    "	get_foodInfo AS ( " +
                    "		SELECT " +
                    "			IFNULL(TO_CHAR(a.monthly_date, 'yyyy-mm'), '"+ StartDate.Substring(0,7) + "') use_month, " +
                    "			a.staff_id, " +
                    "			SUM(a.use_times) cnt_usefimes, " +
                    "			SUM(a.food_cost * a.use_times) sum_foodcosts " +
                    "		FROM " +
                    "			mng_charge_food a " +
                    "		WHERE " +
                    "			1 = 1 " +
                    "			AND IFNULL(TO_CHAR(a.monthly_date, 'yyyy-mm'), '" + StartDate.Substring(0, 7) + "') = '" + StartDate.Substring(0, 7) + "' " +
                    "		GROUP BY " +
                    "			TO_CHAR(a.monthly_date, 'yyyy-mm'), " +
                    "			a.staff_id " +
                    "	), " +
                    "	get_staff_food AS ( " +
                    "		SELECT " +
                    "			'" + StartDate.Substring(0, 7) + "' use_month, " +
                    "			a.staff_id, " +
                    "			d.cnt_usefimes cnt_usefimes, " +
                    "			d.sum_foodcosts sum_foodcosts " +
                    "		FROM " +
                    "			mng_staff a " +
                    "			LEFT JOIN get_foodInfo d ON a.staff_id = d.staff_id " +
                    "	) " +
                    " SELECT " +
                    "	TO_CHAR(a.pay_month, 'yyyy-mm') pay_mont, " +
                    "	a.staff_id, " +
                    "	c.staff_name, " +
                    "	IFNULL(a.basic_pay, 0) basic_pay, " +
                    "	IFNULL(a.bonus_cost, 0) bonus_cost, " +
                    "	IFNULL(a.position_cost, 0) position_cost, " +
                    "	IFNULL(a.month_leave_cost, 0) month_leave_cost, " +
                    "	IFNULL(f.sum_foodcosts, 0) food_cost, " +
                    "	IFNULL(a.self_driving_subsidy, 0) self_driving_subsidy, " +
                    "	IFNULL(a.overtime_cost, 0) overtime_cost, " +
                    "	IFNULL(a.nps_cost, 0) nps_cost, " +
                    "	IFNULL(a.nhis_cost, 0) nhis_cost, " +
                    "	IFNULL(a.care_cost, 0) care_cost, " +
                    "	IFNULL(a.income_tax, 0) income_tax, " +
                    "	IFNULL(a.local_income_tax, 0) local_income_tax, " +
                    "	IFNULL(a.empoly_cost, 0) empoly_cost, " +
                    "	IFNULL(a.year_end_income_tax, 0) year_end_income_tax, " +
                    "	IFNULL(a.year_end_local_income_tax, 0) year_end_local_income_tax, " +
                    "	IFNULL(a.special_tax, 0) special_tax, " + 
                    "	IFNULL(f.cnt_usefimes, 0) cnt_usefimes, " +
                    "	d.bank_name, " +
                    "	d.account_number, " +
                    "	a.COMMENT " +
                    " FROM	 " +
                    "	mng_pay_stub a LEFT JOIN get_staff_food f ON a.staff_id = f.staff_id, " +
                    "	mng_pay_stub b INNER JOIN mng_staff c ON b.staff_id = c.staff_id, " +
                    "	mng_pay_stub e INNER JOIN mng_bank d ON e.staff_id = d.id " +
                    " WHERE 1=1 " +
                    " AND TO_CHAR(a.pay_month, 'YYYY-MM') = f.use_month " +
                    " AND a.staff_id = c.staff_id " +
                    " AND a.staff_id = d.id " +
                    " GROUP BY a.staff_id " +
                    " ORDER BY a.staff_id ";

            //if (StartDate != string.Empty && EndDate != string.Empty)
            //    sql += "  AND a.monthly_date BETWEEN TO_DATETIME('" + StartDate + "', 'YYYY-MM-DD') AND " + " TO_DATETIME('" + EndDate + "', 'YYYY-MM-DD') ";

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
                    "       TO_DATETIME('" + Monthly_date + "', 'YYYY-MM'), " +
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
                    "           monthly_date = TO_DATETIME('" + Monthly_date + "', 'YYYY-MM')," +
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

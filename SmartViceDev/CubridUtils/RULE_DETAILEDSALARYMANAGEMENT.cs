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
    public class RULE_DETAILEDSALARYMANAGEMENT
    {
        //CUBRID DB 연결 정보
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");

        //* MessageSet *//
        string QueryType = string.Empty;
        string SearchType = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string pay_month = string.Empty;
        string Staff_Id = string.Empty;
        string basic_pay = string.Empty;
        string bonus_cost = string.Empty;
        string position_cost = string.Empty;
        string month_leave_cost = string.Empty;
        string food_cost = string.Empty;
        string self_driving_subsidy = string.Empty;
        string overtime_cost = string.Empty;
        string nps_cost = string.Empty;
        string nhis_cost = string.Empty;
        string care_cost = string.Empty;
        string empoly_cost = string.Empty;
        string income_tax = string.Empty;
        string local_income_tax = string.Empty;
        string year_end_income_tax = string.Empty;
        string year_end_local_tax = string.Empty;
        string special_tax = string.Empty;
        string COMMENT = string.Empty;

        public RULE_DETAILEDSALARYMANAGEMENT()
        {
            //* MessageSet *//
             QueryType = string.Empty;
             SearchType = string.Empty;
             StartDate = string.Empty;
             EndDate = string.Empty;
             pay_month = string.Empty;
             Staff_Id = string.Empty;
             basic_pay = string.Empty;
             bonus_cost = string.Empty;
             position_cost = string.Empty;
             month_leave_cost = string.Empty;
             food_cost = string.Empty;
             self_driving_subsidy = string.Empty;
             overtime_cost = string.Empty;
             nps_cost = string.Empty;
             nhis_cost = string.Empty;
             care_cost = string.Empty;
             empoly_cost = string.Empty;
             income_tax = string.Empty;
             local_income_tax = string.Empty;
             year_end_income_tax = string.Empty;
             year_end_local_tax = string.Empty;
             special_tax = string.Empty;
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
        public DataTable SearchCommon(Dictionary<string, string> MessageSet)
         {
            ConnectionToDB();

            DataTable ds = new DataTable();

            if (MessageSet.ContainsKey("SearchType"))
                SearchType = MessageSet["SearchType"].ToString();
            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();

            if (SearchType == "seachStaffInfo")
            {
                string sql = " SELECT" +
                    "                 a.staff_id , " +
                    "                 a.staff_name,  " +
                    "                 d.depart_name , " +
                    "                 b.position_name , " +
                    "               TO_CHAR(a.join_dt, 'YYYY-MM-DD') join_dt , " +
                    "               TO_CHAR(a.resign_dt, 'YYYY-MM-DD') resign_dt  " +
                    " 	        FROM 	" +
                    "               mng_staff a INNER JOIN mng_position b ON a.position_id = b.position_id ,  " +
                    "               mng_staff c INNER JOIN mng_depart d ON c.depart_id = d.depart_id  " +
                    "           WHERE 1 = 1 " +
                    "                AND a.staff_id = c.staff_id ";
                if (Staff_Id != string.Empty)
                    sql += "    AND a.staff_id = '" + Staff_Id + "' ";

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
            if (MessageSet.ContainsKey("staff_id"))
                Staff_Id = MessageSet["staff_id"].ToString();
            if (MessageSet.ContainsKey("StartDate"))
                StartDate = MessageSet["StartDate"].ToString();
            if (MessageSet.ContainsKey("EndDate"))
                EndDate = MessageSet["EndDate"].ToString();

            if (SearchType == "MainSearch")
            {
                sql = "	WITH " +
                    "	get_foodInfo AS ( " +
                    "		SELECT " +
                    "			IFNULL(TO_CHAR(a.monthly_date, 'yyyy-mm'), '" + StartDate.Substring(0, 7) + "') use_month, " +
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
                    " SELECT" +
                        "        c.staff_id , " +
                        "        c.staff_name , " +
                        "        d.depart_name , " +
                        "        f.position_name , " +
                        "        TO_CHAR(e.join_dt, 'YYYY-MM-DD') join_dt , " +
                        "        TO_CHAR(e.resign_dt, 'YYYY-MM-DD') resign_dt , " +
                        "        a.pay_month , " +
                        "	IFNULL(a.basic_pay, 0) basic_pay, " +
                        "	IFNULL(a.bonus_cost, 0) bonus_cost, " +
                        "	IFNULL(a.position_cost, 0) position_cost, " +
                        "	IFNULL(a.month_leave_cost, 0) month_leave_cost, " +
                        "	IFNULL(b.sum_foodcosts, 0) food_cost, " +
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
                        "        a.COMMENT  " +
                        "   FROM " +
                        "	            mng_pay_stub a LEFT JOIN get_staff_food b ON a.staff_id = b.staff_id, " +
                        "               mng_staff e INNER JOIN mng_position f ON e.position_id = f.position_id ,  " +
                        "               mng_staff c INNER JOIN mng_depart d ON c.depart_id = d.depart_id  " +
                        "   WHERE 1 = 1  " +
                        "           AND a.staff_id = c.staff_id  " +
                        "           AND a.staff_id = e.staff_id  " +
                        "           AND TO_CHAR(a.pay_month, 'YYYY-MM') = b.use_month "+
                        "   GROUP BY a.staff_id " +
                        "   ORDER BY a.staff_id ";

                //sql += " ORDER BY work_year ";       
            }
            else if (SearchType == "chkNewMonth")
            {
                sql = " SELECT" +
                        "        a.staff_id  " +
                        "   FROM " +
                        "       mng_pay_stub a" +
                        "   WHERE 1 = 1  ";
                if (Staff_Id != string.Empty)
                    sql += "  AND a.staff_id = '" + Staff_Id + "' ";
                if (StartDate != string.Empty && EndDate != string.Empty)
                    sql += "  AND a.pay_month BETWEEN TO_DATETIME('" + StartDate + "', 'YYYY-MM-DD') AND " + " TO_DATETIME('" + EndDate + "', 'YYYY-MM-DD') ";
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
            if (MessageSet.ContainsKey("staff_id"))
                Staff_Id = MessageSet["staff_id"].ToString();
            if (MessageSet.ContainsKey("pay_month"))
                pay_month = MessageSet["pay_month"].ToString();
            if (MessageSet.ContainsKey("basic_pay"))
                basic_pay = MessageSet["basic_pay"].ToString();
            if (MessageSet.ContainsKey("bonus_cost"))
                bonus_cost = MessageSet["bonus_cost"].ToString();
            if (MessageSet.ContainsKey("position_cost"))
                position_cost = MessageSet["position_cost"].ToString();
            if (MessageSet.ContainsKey("month_leave_cost"))
                month_leave_cost = MessageSet["month_leave_cost"].ToString();    
            if (MessageSet.ContainsKey("food_cost"))
                food_cost = MessageSet["food_cost"].ToString();
            if (MessageSet.ContainsKey("self_driving_subsidy"))
                self_driving_subsidy = MessageSet["self_driving_subsidy"].ToString();
            if (MessageSet.ContainsKey("overtime_cost"))
                overtime_cost = MessageSet["overtime_cost"].ToString();
            if (MessageSet.ContainsKey("nps_cost"))
                nps_cost = MessageSet["nps_cost"].ToString();
            if (MessageSet.ContainsKey("nhis_cost"))
                nhis_cost = MessageSet["nhis_cost"].ToString();
            if (MessageSet.ContainsKey("care_cost"))
                care_cost = MessageSet["care_cost"].ToString();
            if (MessageSet.ContainsKey("empoly_cost"))
                empoly_cost = MessageSet["empoly_cost"].ToString();
            if (MessageSet.ContainsKey("income_tax"))
                income_tax = MessageSet["income_tax"].ToString();
            if (MessageSet.ContainsKey("local_income_tax"))
                local_income_tax = MessageSet["local_income_tax"].ToString();
            if (MessageSet.ContainsKey("year_end_income_tax"))
                year_end_income_tax = MessageSet["year_end_income_tax"].ToString();
            if (MessageSet.ContainsKey("year_end_local_tax"))
                year_end_local_tax = MessageSet["year_end_local_tax"].ToString();
            if (MessageSet.ContainsKey("special_tax"))
                special_tax = MessageSet["special_tax"].ToString();
            if (MessageSet.ContainsKey("COMMENT"))
                COMMENT = MessageSet["COMMENT"].ToString();


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_pay_stub " +
                    "                    ( " +
                    "	           pay_month,     " +
                    "	           staff_id,     " +
                    "	           basic_pay,     " +
                    "	           bonus_cost,     " +
                    "	           position_cost,     " +
                    "	           month_leave_cost,     " +
                    "	           food_cost,     " +
                    "	           self_driving_subsidy,     " +
                    "	           overtime_cost,     " +
                    "	           nps_cost,     " +
                    "	           nhis_cost,     " +
                    "	           care_cost,     " +
                    "	           income_tax,     " +
                    "	           local_income_tax,     " +
                    "	           empoly_cost,     " +
                    "	           year_end_income_tax,     " +
                    "	           year_end_local_income_tax,     " +
                    "	           special_tax,     " +
                    "	           COMMENT,     " +
                    "	           reg_dt,     " +
                    "	           update_dt " +
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "       TO_DATETIME('" + pay_month + "', 'YYYY-MM-DD'), " +                
                   "          '" + Staff_Id + "',  ";
                if (basic_pay != string.Empty)
                    sql += "          '" + basic_pay + "',  ";
                else
                    sql += "          null,  ";

                if (bonus_cost != string.Empty)
                    sql += "          " + bonus_cost + ",  ";
                else
                    sql += "          null,  ";

                if (position_cost != string.Empty)
                    sql += "          " + position_cost + ",  ";
                else
                    sql += "          null,  ";

                if (month_leave_cost != string.Empty)
                    sql += "          " + month_leave_cost + ",  ";
                else
                    sql += "          null,  ";

                if (food_cost != string.Empty)
                    sql += "          " + food_cost + ",  ";
                else
                    sql += "          null,  ";

                if (self_driving_subsidy != string.Empty)
                    sql += "          " + self_driving_subsidy + ",  ";
                else
                    sql += "          null,  ";

                if (overtime_cost != string.Empty)
                    sql += "          " + overtime_cost + ",  ";
                else
                    sql += "          null,  ";

                if (nps_cost != string.Empty)
                    sql += "          " + nps_cost + ",  ";
                else
                    sql += "          null,  ";

                if (nhis_cost != string.Empty)
                    sql += "          " + nhis_cost + ",  ";
                else
                    sql += "          null,  ";

                if (care_cost != string.Empty)
                    sql += "          " + care_cost + ",  ";
                else
                    sql += "          null,  ";

                if (income_tax != string.Empty)
                    sql += "          " + income_tax + ",  ";
                else
                    sql += "          null,  ";

                if (local_income_tax != string.Empty)
                    sql += "          " + local_income_tax + ",  ";
                else
                    sql += "          null,  ";

                if (empoly_cost != string.Empty)
                    sql += "          " + empoly_cost + ",  ";
                else
                    sql += "          null,  ";

                if (year_end_income_tax != string.Empty)
                    sql += "          " + year_end_income_tax + ",  ";
                else
                    sql += "          null,  ";

                if (year_end_local_tax != string.Empty)
                    sql += "          " + year_end_local_tax + ",  ";
                else
                    sql += "          null,  ";

                if (special_tax != string.Empty)
                    sql += "          " + special_tax + ",  ";
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
                    "            mng_pay_stub " +
                    " 	SET ";                           
                    if (basic_pay != string.Empty)
                    sql += "   basic_pay = '" + basic_pay + "',  ";
                else
                    sql += "   basic_pay = null,  ";

                if (bonus_cost != string.Empty)
                    sql += "    bonus_cost = " + bonus_cost + ",  ";
                else
                    sql += "    bonus_cost =  null,  ";

                if (position_cost != string.Empty)
                    sql += "    position_cost = " + position_cost + ",  ";
                else
                    sql += "    position_cost = null,  ";

                if (month_leave_cost != string.Empty)
                    sql += "    month_leave_cost = " + month_leave_cost + ",  ";
                else
                    sql += "    month_leave_cost =  null,  ";

                if (food_cost != string.Empty)
                    sql += "    food_cost = " + food_cost + ",  ";
                else
                    sql += "    food_cost = null,  ";

                if (self_driving_subsidy != string.Empty)
                    sql += "    self_driving_subsidy = " + self_driving_subsidy + ",  ";
                else
                    sql += "    self_driving_subsidy = null,  ";

                if (overtime_cost != string.Empty)
                    sql += "     overtime_cost = " + overtime_cost + ",  ";
                else
                    sql += "     overtime_cost = null,  ";

                if (nps_cost != string.Empty)
                    sql += "    nps_cost = " + nps_cost + ",  ";
                else
                    sql += "    nps_cost = null,  ";

                if (nhis_cost != string.Empty)
                    sql += "    nhis_cost = " + nhis_cost + ",  ";
                else
                    sql += "    nhis_cost = null,  ";

                if (care_cost != string.Empty)
                    sql += "    care_cost = " + care_cost + ",  ";
                else
                    sql += "    care_cost = null,  ";

                if (income_tax != string.Empty)
                    sql += "    income_tax = " + income_tax + ",  ";
                else
                    sql += "    income_tax = null,  ";

                if (local_income_tax != string.Empty)
                    sql += "    local_income_tax = " + local_income_tax + ",  ";
                else
                    sql += "    local_income_tax = null,  ";

                if (empoly_cost != string.Empty)
                    sql += "    empoly_cost = " + empoly_cost + ",  ";
                else
                    sql += "    empoly_cost = null,  ";

                if (year_end_income_tax != string.Empty)
                    sql += "    year_end_income_tax = " + year_end_income_tax + ",  ";
                else
                    sql += "    year_end_income_tax =  null,  ";

                if (year_end_local_tax != string.Empty)
                    sql += "    year_end_local_income_tax = " + year_end_local_tax + ",  ";
                else
                    sql += "    year_end_local_income_tax = null,  ";

                if (special_tax != string.Empty)
                    sql += "    special_tax = " + special_tax + ",  ";
                else
                    sql += "    special_tax = null,  ";

                if (COMMENT != string.Empty)
                    sql += "   COMMENT = '" + COMMENT + "',  ";
                else
                    sql += "  COMMENT = null,  ";

                sql += " 	update_dt = CURRENT_DATE " +
                        " 	WHERE  1 = 1" +
                        "           AND     pay_month = TO_DATETIME('" + pay_month.Substring(0,7) + "', 'YYYY-MM') " +
                        "           AND     staff_id = '" + Staff_Id + "'  ";
            }
            else if (QueryType == "Delete" )
            {
                //sql = " DELETE FROM " +
                //       "        mng_pay_stub " +
                //" 	WHERE " +
                //"        valid_id = '" + Valid_id + "' ";
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

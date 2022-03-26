using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUBRID.Data.CUBRIDClient;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev.CubridUtils
{
    public class RULE_VACATIONMANAGEMENT
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
        string EmpId = string.Empty;
        string EmpName = string.Empty;
        string PositionName = string.Empty;
        string JoinDt = string.Empty;
        string UsedLeaveDate = string.Empty;

        public RULE_VACATIONMANAGEMENT()
        {
            //* MessageSet *//
             EmpId = string.Empty;
             EmpName = string.Empty;
             PositionName = string.Empty;
             JoinDt = string.Empty;
             UsedLeaveDate = string.Empty;
        }

        //공통 데이터 가져오는 쿼리
        public DataSet SearchCommon(string searchtype)
        {
            ConnectionToDB();

            string sql = string.Empty;
            DataSet ds = new DataSet();

            if (searchtype == "seachPosition")
            {
                 sql = " SELECT position_id, " +
                    "                 position_name, " +
                    "                 position_sort_order " +
                    " 	        FROM 	mng_position" +
                    " 	        ORDER BY position_sort_order ";
            
            }
            else if (searchtype == "seachEmpNm")
            {
                 sql = " SELECT staff_id, " +
                    "                 staff_name " +                    
                    " 	        FROM 	mng_staff";

            }
            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {

                da.Fill(ds);

                DisconnectionToDB();

                return ds;
            }
        }

        //* 조회 *//
        public DataSet Search(string searchtype,Dictionary<string,string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("EmpId"))
                EmpId = MessageSet["EmpId"].ToString();
            if (MessageSet.ContainsKey("EmpName"))
                EmpName = MessageSet["EmpName"].ToString();
            if (MessageSet.ContainsKey("PositionName"))
                PositionName = MessageSet["PositionName"].ToString();
            if (MessageSet.ContainsKey("JoinDt"))
                JoinDt = MessageSet["JoinDt"].ToString();

            if (searchtype == "DetailSearch")
            {
                sql = " WITH " +
                    " work_yeardt AS ( SELECT " +
                    "                       CAST(TRUNCATE(MONTHS_BETWEEN(SYS_DATE, TO_DATE(TO_CHAR(a.join_dt, 'YYYY-MM-DD'), 'YYYY-MM-DD')) / 1.2, -1) AS BIGINT) / 10 work_year, " +
                    "                       a.join_dt, " +
                    "                       a.staff_id, " +
                    "                       a.staff_name, " +
                    "                       b.position_name " +
                    "                   FROM mng_staff a " +
                    "                       INNER JOIN mng_position b ON a.position_id = b.position_id " +
                    " 	            ), " +
                    " cnt_leave_by_year AS ( SELECT " +
                    " 	                            b.staff_id, " +
                    " 	                            b.staff_name, " +
                    " 	                            COUNT(A.use_leave_dt) cntleavebyyear " +
                    "                       FROM " +
                    "                               ( SELECT a.* FROM mng_use_leave a WHERE TO_CHAR(a.use_leave_dt, 'YYYY') = '" + DateTime.Today.Year.ToString() + "' ) a " +
                    "                                RIGHT OUTER JOIN mng_staff b ON a.staff_id = b.staff_id  " +
                    "                      WHERE 1 = 1 " +
                    "                      GROUP BY  b.staff_id " +
                    " 	             ), " +
                    " cnt_leave_by_month AS ( SELECT " +
                    " 	                            b.staff_id, " +
                    " 	                            b.staff_name, " +
                    "                               IFNULL(a.leaveyyyymm, '9999-99') leaveyyyymm, " +
                    " 	                            IFNULL(COUNT(a.use_leave_dt), 0) cntleave " +
                    "                       FROM " +
                    "                               ( SELECT TO_CHAR(a.use_leave_dt, 'YYYY-MM') leaveyyyymm, a.* FROM mng_use_leave a WHERE TO_CHAR(a.use_leave_dt, 'YYYY') = '" + DateTime.Today.Year.ToString() + "' ) a " +
                    "                                RIGHT OUTER JOIN mng_staff b ON a.staff_id = b.staff_id  " +
                    "                       WHERE 1 = 1 " +
                    "                       GROUP BY  b.staff_id, " +
                    "                                 a.leaveyyyymm " +
                    " 	             ), " +
                   " detail_leave_date AS ( SELECT " +
                   " 	                        a.staff_id, " +
                   " 	                        COALESCE(TO_CHAR(b.use_leave_dt, 'YYYY-MM-DD'),'9999-99-99') use_leave_dt, " +
                   "                            COALESCE(TO_CHAR(b.use_leave_dt, 'YYYY-MM'),'9999-99') month_leave_dt " +
                   "                        FROM " +
                   "                               mng_staff a " +
                   "                               LEFT OUTER JOIN mng_use_leave b ON a.staff_id = b.staff_id  " +
                   " 	             ) " +
                   " SELECT " +
                   "           e.staff_id   EmpId, " +
                   "           e.staff_name  EmpNm, " +
                   "           e.position_name  EmpPosition, " +
                   "           e.join_dt  EmpJoinDate, " +
                   "           f.leave_totoal_day  UpdatedDayOff, " +
                   "           f.leave_create_day  DayOff, " +
                   "           f.leave_add_day  AddedDays, " +
                   "           h.cntleavebyyear  TotalUsedDays, " +
                   "           f.leave_totoal_day - h.cntleavebyyear  RestDays,	 " +
                   "           d.leaveyyyymm, " +
                   "           n.use_leave_dt detailLeaveDate, " +
                   "           d.cntleave   UsedDays" +
                   " FROM " +                   
                   "       work_yeardt e   INNER JOIN mng_leave f ON e.work_year = f.work_year,  " +
                   "       mng_staff g     INNER JOIN cnt_leave_by_year h ON g.staff_id = h.staff_id, " +
                   "       mng_staff c     INNER JOIN cnt_leave_by_month d ON c.staff_id = d.staff_id, " +
                   "       mng_staff m     INNER JOIN detail_leave_date n ON m.staff_id = n.staff_id " +
                   " WHERE 1 = 1 " +                  
                   "        AND e.staff_id = h.staff_id " +
                   "        AND e.staff_id = n.staff_id " +
                   "        AND h.staff_id = d.staff_id " +
                   "        AND d.leaveyyyymm = n.month_leave_dt " ;
                    

                if (EmpName != string.Empty)
                    sql += " AND e.staff_name = '" + EmpName + "'" ;

                if (PositionName != string.Empty)
                    sql += "AND e.position_name = '" + PositionName + "'";

                sql +=
                   " GROUP BY e.staff_id, " +
                   "           e.staff_name, " +
                   "           e.position_name, " +
                   "           d.leaveyyyymm, " +
                   "           n.use_leave_dt " +
                   " ORDER BY EmpId ";
                ;
            }
            else if (searchtype == "MainSearch")
            {
                sql = " WITH " +
                    " work_yeardt AS ( SELECT " +
                    "                       CAST(TRUNCATE(MONTHS_BETWEEN(SYS_DATE, TO_DATE(TO_CHAR(a.join_dt, 'YYYY-MM-DD'), 'YYYY-MM-DD')) / 1.2, -1) AS BIGINT) / 10 work_year, " +
                    "                       a.join_dt, " +
                    "                       a.staff_id, " +
                    "                       a.staff_name, " +
                    "                       b.position_name " +
                    "                   FROM mng_staff a " +
                    "                       INNER JOIN mng_position b ON a.position_id = b.position_id " +
                    " 	            ), " +
                    " cnt_leave_by_year AS ( SELECT " +
                    " 	                            b.staff_id, " +
                    " 	                            b.staff_name, " +
                    " 	                            COUNT(A.use_leave_dt) cntleavebyyear " +
                    "                       FROM " +
                    "                               ( SELECT a.* FROM mng_use_leave a WHERE TO_CHAR(a.use_leave_dt, 'YYYY') = '" + DateTime.Today.Year.ToString() + "' ) a " +
                    "                                RIGHT OUTER JOIN mng_staff b ON a.staff_id = b.staff_id  " +
                    "                      WHERE 1 = 1 " +
                    "                      GROUP BY  b.staff_id " +
                    " 	             ), " +
                    " cnt_leave_by_month AS ( SELECT " +
                    " 	                            b.staff_id, " +
                    " 	                            b.staff_name, " +
                    "                               IFNULL(a.leaveyyyymm, '9999-99') leaveyyyymm, " +
                    " 	                            IFNULL(COUNT(a.use_leave_dt), 0) cntleave " +
                    "                       FROM " +
                    "                               ( SELECT TO_CHAR(a.use_leave_dt, 'YYYY-MM') leaveyyyymm, a.* FROM mng_use_leave a WHERE TO_CHAR(a.use_leave_dt, 'YYYY') = '" + DateTime.Today.Year.ToString() + "' ) a " +
                    "                                RIGHT OUTER JOIN mng_staff b ON a.staff_id = b.staff_id  " +
                    "                       WHERE 1 = 1 " +
                    "                       GROUP BY  b.staff_id, " +
                    "                                 a.leaveyyyymm " +
                    " 	             ) " +
                    " SELECT " +
                    "           e.staff_id   EmpId, " +
                    "           e.staff_name  EmpNm, " +
                    "           e.position_name  EmpPosition, " +
                    "           e.join_dt  EmpJoinDate, " +
                    "           f.leave_totoal_day  UpdatedDayOff, " +
                    "           f.leave_create_day  DayOff, " +
                    "           f.leave_add_day  AddedDays, " +
                    "           h.cntleavebyyear  TotalUsedDays, " +
                    "           f.leave_totoal_day - h.cntleavebyyear  RestDays ,	 " +
                    "           d.leaveyyyymm,  " +
                    "           d.cntleave UsedDaysByMonth" +
                    " FROM " +
                    "       mng_staff c INNER JOIN cnt_leave_by_month d ON c.staff_id = d.staff_id,  " +
                    "       work_yeardt e INNER JOIN mng_leave f ON e.work_year = f.work_year,  " +
                    "       mng_staff g INNER JOIN cnt_leave_by_year h ON g.staff_id = h.staff_id  " +
                    " WHERE 1 = 1 " +
                    "        AND e.staff_id = h.staff_id " +
                    "        AND e.staff_id = d.staff_id ";

                if (EmpName != string.Empty)
                    sql += "AND e.staff_name = '" + EmpName + "'";

                if (PositionName != string.Empty)
                    sql += "AND e.position_name = '" + PositionName + "'";

                sql +=  " GROUP BY e.staff_id, " +
                    "        e.staff_name, " +                    
                    "        e.position_name, " +
                    "        d.leaveyyyymm " +
                    "ORDER BY EmpId ";
            }
            else if (searchtype == "GetLeaveData")
            {
                sql = " SELECT " +
                    "           work_year , " +
                    "           leave_create_day  DayOff," +
                    "           leave_add_day  AddedDays, " +
                    "           leave_totoal_day UpdatedDayOff" +
                    "   FROM mng_leave " +
                    "   WHERE " +
                    "   CAST(TRUNCATE(MONTHS_BETWEEN(SYS_DATE, TO_DATE(TO_CHAR('" + JoinDt + "', 'YYYY-MM-DD'), 'YYYY-MM-DD')) / 1.2, -1) / 10 AS BIGINT) = work_year ";
            }

            //sql = "SELECT * from mng_leave ";
            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {
                
                da.Fill(ds, searchtype);

                DisconnectionToDB();

                return ds;
            }                
        }

        //* 저장 - 추가,삭제,갱신 기능
        public bool Save(string Savetype, Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (MessageSet.ContainsKey("Emp_Id"))
                EmpId = MessageSet["Emp_Id"].ToString();
            if (MessageSet.ContainsKey("Use_Leave_Dt"))
                UsedLeaveDate = MessageSet["Use_Leave_Dt"].ToString();
            if (MessageSet.ContainsKey("JoinDt"))
                JoinDt = MessageSet["JoinDt"].ToString();


            if ( Savetype == "Insert" )
            {
                sql = " INSERT INTO " +
                   "        mng_use_leave " +
                   "                    ( " +
                   "                       staff_id, " +
                   "                       use_leave_dt, " +
                   "                       COMMENT " +
                   "                    ) " +
                   " 	VALUES "    +
                   "       ( "      +
                   "        '"      + EmpId + "', " +
                   "        TO_DATETIME(" + UsedLeaveDate + ", 'YYYY-MM-DD'), " +
                   "        NULL " +
                   "        ) ";
            }
            else if ( Savetype == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_use_leave " +
                       " 	WHERE " +
                       "        staff_id = '" + EmpId + "' " +
                       "    AND use_leave_dt = TO_DATETIME(" + UsedLeaveDate + ", 'YYYY-MM-DD') ";
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
        

        public void UpdateDataUoTable()
        {
            ConnectionToDB();

            try
            {
                string sql = "UPDATE member SET name = 'yooosumin' ";

                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

                CUBRIDCommand com = new CUBRIDCommand(sql, dbConnection);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DisconnectionToDB();
        }
    }
}

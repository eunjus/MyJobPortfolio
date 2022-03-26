﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUBRID.Data.CUBRIDClient;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev.CubridUtils
{
    public class RULE_VACATIONSTANDARDPOPUP
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
        string Work_year = string.Empty;
        string Leave_create_day = string.Empty;
        string Leave_add_day = string.Empty;
        string Leave_total_day = string.Empty;
        string COMMENT = string.Empty;
        string Reg_dt = string.Empty;
        string Update_dt = string.Empty;

        public RULE_VACATIONSTANDARDPOPUP()
        {
            //* MessageSet *//
             QueryType = string.Empty;
             Work_year = string.Empty;
             Leave_create_day = string.Empty;
             Leave_add_day = string.Empty;
             Leave_total_day = string.Empty;
             COMMENT = string.Empty;
             Reg_dt = string.Empty;
             Update_dt = string.Empty;
        }

        //공통 데이터 가져오는 쿼리
        public DataSet SearchCommon(string searchtype)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();

            if (searchtype == "seachPosition")
            {
                string sql = " SELECT position_id, " +
                    "                 position_name, " +
                    "                 position_sort_order " +
                    " 	        FROM 	mng_position";

                using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
                {

                    da.Fill(ds, "search01");

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

            if (MessageSet.ContainsKey("Work_year"))
                Work_year = MessageSet["Work_year"].ToString();            



                sql = " SELECT " +
                       "           work_year, " +
                       "           leave_create_day, " +
                       "           leave_add_day, " +
                       "           leave_totoal_day, " +
                       "           COALESCE(COMMENT,'') COMMENT, " +
                       "           TO_CHAR(reg_dt, 'YYYY-MM-DD') reg_dt ," +
                       "           TO_CHAR(update_dt,'YYYY-MM-DD') update_dt " +
                       " FROM " +
                       "       mng_leave  "+
                       " WHERE 1 = 1 " ;
                
                if (Work_year != string.Empty)
                    sql += "AND work_year = " + Work_year;

                sql += " ORDER BY work_year ";       
            
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
            if (MessageSet.ContainsKey("Work_year"))
                Work_year = MessageSet["Work_year"].ToString();
            if (MessageSet.ContainsKey("Leave_create_day"))
                Leave_create_day = MessageSet["Leave_create_day"].ToString();
            if (MessageSet.ContainsKey("Leave_add_day"))
                Leave_add_day = MessageSet["Leave_add_day"].ToString();
            if (MessageSet.ContainsKey("Leave_total_day"))
                Leave_total_day = MessageSet["Leave_total_day"].ToString();
            if (MessageSet.ContainsKey("COMMENT"))
                COMMENT = MessageSet["COMMENT"].ToString();
            if (MessageSet.ContainsKey("Reg_dt"))
                Reg_dt = MessageSet["Reg_dt"].ToString();
            if (MessageSet.ContainsKey("Update_dt"))
                Update_dt = MessageSet["Update_dt"].ToString();


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_leave " +
                    "                    ( " +
                    "           work_year, " +
                    "           leave_create_day, " +
                    "           leave_add_day, " +
                    "           leave_totoal_day, " +
                    "           comment, " +
                    "           reg_dt," +
                    "           update_dt " +
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "        " + Work_year + ", " +
                    "        " + Leave_create_day + ", " +
                    "        " + Leave_add_day + ", " +
                    "        " + Leave_total_day + ", ";

                    if (COMMENT.Trim() != string.Empty)
                        sql += "    '" + COMMENT + "', ";
                    else
                        sql += "    NULL,  ";
                
                
                sql += "        CURRENT_DATE, " +
                        "       CURRENT_DATE " +                        
                        //"        TO_DATETIME(" + UsedLeaveDate + ", 'YYYY-MM-DD'), " +
                        "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                        "            mng_leave " +
                        " 	SET " +
                        "           work_year = " + Work_year + ",  " +
                        "           leave_create_day = " + Leave_create_day + ",  " +
                        "           leave_add_day = " + Leave_add_day + ",  " +
                        "           leave_totoal_day = " + Leave_total_day + ",  ";
                       if (COMMENT != string.Empty)
                            sql += "    comment ='" + COMMENT + "', ";
                        else
                            sql += "   comment = NULL,  ";

                sql +=  "           reg_dt = '" + Reg_dt + "',  " +
                        "           update_dt = '" + Update_dt + "'  " +
                        "   WHERE " +
                        "      work_year = " + Work_year ;

            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_leave " +
                       " 	WHERE " +
                       "        work_year = '" + Work_year + "' ";
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

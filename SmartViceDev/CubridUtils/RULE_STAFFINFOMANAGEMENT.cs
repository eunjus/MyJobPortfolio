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
    public class RULE_STAFFINFOMANAGEMENT
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
        string Staff_id = string.Empty;        
        string Depart_name = string.Empty;
        string Position_name = string.Empty;

        public RULE_STAFFINFOMANAGEMENT()
        {
            //* MessageSet *//
             QueryType = string.Empty;
             Staff_id = string.Empty;
             Depart_name = string.Empty;
             Position_name = string.Empty;
        }

        //공통 데이터 가져오는 쿼리 - 공통데이터 가져오는 cs 파일을 따로 하나 만드는게 나을듯.
        public DataSet SearchCommon(string searchtype)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;

            if (searchtype == "seachPosition")
            {
                 sql = " SELECT position_id, " +
                    "                 position_name, " +
                    "                 position_sort_order " +
                    " 	        FROM 	mng_position";

            }
            else if (searchtype == "seachDepart")
            {
                 sql = " SELECT depart_id, " +
                    "                 depart_name , " +
                    "                 depart_sort_order " +
                    " 	        FROM 	mng_depart";

            }
            else if (searchtype == "seachEmpNm")
            {
                sql = " SELECT staff_id, " +
                   "                 staff_name " +
                   " 	        FROM 	mng_staff";

            }
            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
                {

                    da.Fill(ds, "search01");

                    DisconnectionToDB();

                    return ds;
                }
            
            return ds;
        }

        //* 조회 *//
        public DataSet Search(Dictionary<string,string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("Depart_name"))
                Depart_name = MessageSet["Depart_name"].ToString();
            if (MessageSet.ContainsKey("Position_name"))
                Position_name = MessageSet["Position_name"].ToString();

            sql = " SELECT " +
                   "           a.staff_id , " +
                   "           a.staff_name , " +
                   "           a.staff_email , " +
                   "           b.position_name , " +
                   "           d.depart_name ," +
                   "           TO_CHAR(a.reg_dt, 'YYYY-MM-DD') reg_dt ," +
                   "           TO_CHAR(a.update_dt, 'YYYY-MM-DD') update_dt " +
                   " FROM " +
                   "       mng_staff a INNER JOIN mng_position b ON a.position_id = b.position_id ,  " +
                   "       mng_staff c INNER JOIN mng_depart d ON c.depart_id = d.depart_id  " +
                   " WHERE " +
                   "        1 = 1 " +
                   "            AND a.staff_id = c.staff_id ";

            if (Depart_name.Trim() != string.Empty)
                sql += "    AND a.depart_id = (SELECT depart_id FROM mng_depart WHERE depart_name = '" + Depart_name + "' ) ";

            if (Position_name.Trim() != string.Empty)
                sql += "    AND a.position_id = (SELECT position_id FROM mng_position WHERE position_name = '" + Position_name + "' ) " ;

            sql += " GROUP BY a.staff_id  ";
                
                //if (Work_year != string.Empty)
                //    sql += "AND work_year = " + Work_year;

                //sql += " ORDER BY work_year ";       
            
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
            if (MessageSet.ContainsKey("staff_id"))
                Staff_id = MessageSet["staff_id"].ToString();           


            if (QueryType == "Insert" )
            {
                //sql = " INSERT INTO " +
                //    "        mng_depart " +
                //    "                    ( " +
                //    "           depart_id, " +
                //    "           depart_name, " +
                //    "           depart_sort_order, " +
                //    "           comment, " +
                //    "           reg_dt," +
                //    "           update_dt " +
                //    "                    ) " +
                //    " 	VALUES " +
                //    "       ( " +
                //    "        '" + Depart_id + "', " +
                //    "        '" + Depart_name + "', " +
                //    "        '" + Depart_sort_order + "'" +
                //    ", ";

                //    if (COMMENT.Trim() != string.Empty)
                //        sql += "    '" + COMMENT + "', ";
                //    else
                //        sql += "    NULL,  ";
                                
                //sql += "        CURRENT_DATE, " +
                //        "       CURRENT_DATE " +                                                
                //        "        ) ";
            }
            else if (QueryType == "Update")
            {
                //sql = "     UPDATE  " +
                //        "            mng_depart " +
                //        " 	SET " +
                //        "           depart_id = '" + Depart_id + "',  " +
                //        "           depart_name = '" + Depart_name + "',  " +
                //        "           depart_sort_order = '" + Depart_sort_order + "',  ";                        
                //       if (COMMENT != string.Empty)
                //            sql += "    comment = NULL,  ";
                //        else
                //            sql += "    comment = '" + COMMENT + "', ";

                //sql +=  "           reg_dt = '" + Reg_dt + "',  " +
                //        "           update_dt = '" + Update_dt + "'  " +
                //        "   WHERE " +
                //        "      depart_id = '" + Depart_id + "'";

            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_staff " +
                       " 	WHERE " +
                       "        staff_id = '" + Staff_id + "' ";
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

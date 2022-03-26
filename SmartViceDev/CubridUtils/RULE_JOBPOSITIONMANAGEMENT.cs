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
    public class RULE_JOBPOSITIONMANAGEMENT
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
        string Position_id = string.Empty;
        string Position_name = string.Empty;
        string Position_sort_order = string.Empty;
        string COMMENT = string.Empty;
        string Reg_dt = string.Empty;
        string Update_dt = string.Empty;

        public RULE_JOBPOSITIONMANAGEMENT()
        {
            //* MessageSet *//
             QueryType = string.Empty;
             Position_id = string.Empty;
             Position_name = string.Empty;
             Position_sort_order = string.Empty;
             COMMENT = string.Empty;
             Reg_dt = string.Empty;
             Update_dt = string.Empty;
        }
        //공통 데이터 가져오는 쿼리 - 공통데이터 가져오는 cs 파일을 따로 하나 만드는게 나을듯.
        public DataSet SearchCommon(string searchtype)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();

            if (searchtype == "seachPosition")
            {
                string sql = " SELECT" +
                    "                 position_id, " +
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

            //if (MessageSet.ContainsKey("Work_year"))
            //    Work_year = MessageSet["Work_year"].ToString();            


                sql =   " SELECT " +
                        "         position_id, " +
                        "         position_name, " +
                        "         position_sort_order, " +
                        "         COALESCE(COMMENT,'') COMMENT, " +
                        "         TO_CHAR(reg_dt, 'YYYY-MM-DD') reg_dt ," +
                        "         TO_CHAR(update_dt, 'YYYY-MM-DD') update_dt " +
                        " FROM " +
                        "       mng_position  " +
                        " WHERE 1 = 1 " ;
                
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
            if (MessageSet.ContainsKey("Position_id"))
                Position_id = MessageSet["Position_id"].ToString();
            if (MessageSet.ContainsKey("Position_name"))
                Position_name = MessageSet["Position_name"].ToString();
            if (MessageSet.ContainsKey("Position_sort_order"))
                Position_sort_order = MessageSet["Position_sort_order"].ToString();
            if (MessageSet.ContainsKey("COMMENT"))
                COMMENT = MessageSet["COMMENT"].ToString();
            if (MessageSet.ContainsKey("Reg_dt"))
                Reg_dt = MessageSet["Reg_dt"].ToString();
            if (MessageSet.ContainsKey("Update_dt"))
                Update_dt = MessageSet["Update_dt"].ToString();


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_position " +
                    "                    ( " +
                    "           position_id, " +
                    "           position_name, " +
                    "           position_sort_order, " +
                    "           comment, " +
                    "           reg_dt," +
                    "           update_dt " +
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "        '" + Position_id + "', " +
                    "        '" + Position_name + "', " +
                    "        '" + Position_sort_order + "'" +
                    ", ";

                    if (COMMENT.Trim() != string.Empty)
                        sql += "    '" + COMMENT + "', ";
                    else
                        sql += "    NULL,  ";
                                
                sql += "        CURRENT_DATE, " +
                        "       CURRENT_DATE " +                                                
                        "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                        "            mng_position " +
                        " 	SET " +
                        "           position_id = '" + Position_id + "',  " +
                        "           position_name = '" + Position_name + "',  " +
                        "           position_sort_order = '" + Position_sort_order + "',  ";                        
                       if (COMMENT != string.Empty)
                            sql += "    comment = '" + COMMENT + "', ";                
                        else
                            sql += "    comment = NULL,  ";

                sql +=  "           reg_dt = '" + Reg_dt + "',  " +
                        "           update_dt = '" + Update_dt + "'  " +
                        "   WHERE " +
                        "      position_id = '" + Position_id + "'";

            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_position " +
                       " 	WHERE " +
                       "        position_id = '" + Position_id + "' ";
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

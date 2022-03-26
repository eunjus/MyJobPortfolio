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
    public class RULE_BOOKMANAGEMENT
    {
        //CUBRID DB 연결 정보  
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");

        //* MessageSet *//
        string QueryType = string.Empty;
        string Valid_id = string.Empty;
        string Staff_Nm = string.Empty;
        string Staff_Id = string.Empty;
        string Book_title = string.Empty;
        string Publisher = string.Empty;
        string Purchase_dt = string.Empty;
        string Borrow_situation = string.Empty;
        string Borrower_id = string.Empty;
        string Return_schedule_dt = string.Empty;
        string COMMENT = string.Empty;
        string Reg_dt = string.Empty;
        string Update_dt = string.Empty;

        public RULE_BOOKMANAGEMENT()
        {
            //* MessageSet *//
            QueryType = string.Empty;
            Valid_id = string.Empty;
            Book_title = string.Empty;
            Publisher = string.Empty;
            Purchase_dt = string.Empty;
            Borrow_situation = string.Empty;
            Borrower_id = string.Empty;
            Return_schedule_dt = string.Empty;
            COMMENT = string.Empty;
            Reg_dt = string.Empty;
            Update_dt = string.Empty;
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
            
            string sql = string.Empty;

            if (MessageSet.ContainsKey("Staff_Nm"))
                Staff_Nm = MessageSet["Staff_Nm"].ToString();
            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();
            //if (MessageSet.ContainsKey("Code_Type"))
            //    Code_Type = MessageSet["Code_Type"].ToString();
            //if (MessageSet.ContainsKey("Spend_type_cd"))
            //    Spend_type_cd = MessageSet["Spend_type_cd"].ToString();
            //if (MessageSet.ContainsKey("Spend_type_nm"))
            //    Spend_type_nm = MessageSet["Spend_type_nm"].ToString();

            if (searchtype == "seachPublisher")
            {
                 sql = " SELECT publisher " +
                            " 	FROM 	mng_book" +
                            "   GROUP BY publisher ";                
            }
            else if (searchtype == "searchStaffInfo")
            {
                sql = " SELECT" +
                   "                 staff_id , " +
                   "                 staff_name  " +
                   " 	        FROM 	mng_staff" +
                   "           WHERE 1 = 1";

                if (Staff_Id != string.Empty)
                    sql += "    AND staff_id = '" + Staff_Id + "' ";

                if (Staff_Nm != string.Empty)
                    sql += "    AND staff_name = '" + Staff_Nm + "' ";

            }
            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {

                da.Fill(ds);

                DisconnectionToDB();

                return ds;
            }

        }

        //* 조회 *//
        public DataSet Search(Dictionary<string,string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("book_title"))
                Book_title = MessageSet["book_title"].ToString();
            if (MessageSet.ContainsKey("publisher"))
                Publisher = MessageSet["publisher"].ToString();

            sql = " SELECT " +
                       "           a.valid_id, " +
                       "           a.book_title, " +
                       "           a.publisher, " +
                       "           TO_CHAR(a.purchase_dt, 'YYYY-MM-DD') purchase_dt, " +
                       "           a.borrow_situation, " +
                       "           a.borrower_id, " +
                       "           b.staff_name, " +
                       "           TO_CHAR(a.return_schedule_dt, 'YYYY-MM-DD') return_schedule_dt, " +
                       "           COALESCE(a.COMMENT,'') COMMENT, " +
                       "           TO_CHAR(a.reg_dt, 'YYYY-MM-DD') reg_dt ," +
                       "           TO_CHAR(a.update_dt, 'YYYY-MM-DD') update_dt " +
                       " FROM " +
                       "       mng_book a LEFT JOIN mng_staff b ON a.borrower_id = b.staff_id   " +
                       " WHERE 1 = 1 " ;

            if (Book_title != string.Empty)
                sql += "AND UPPER(a.book_title) LIKE '%" + Book_title.ToUpper() + "%'";
            if (Publisher != string.Empty)
                sql += "AND UPPER(a.publisher) LIKE '%" + Publisher.ToUpper() + "%'";

            sql += " ORDER BY a.book_title ";       

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
            if (MessageSet.ContainsKey("book_title"))
                Book_title = MessageSet["book_title"].ToString();
            if (MessageSet.ContainsKey("publisher"))
                Publisher = MessageSet["publisher"].ToString();
            if (MessageSet.ContainsKey("purchase_dt"))
                Purchase_dt = MessageSet["purchase_dt"].ToString();
            if (MessageSet.ContainsKey("borrow_situation"))
                Borrow_situation = MessageSet["borrow_situation"].ToString();
            if (MessageSet.ContainsKey("borrower_id"))
                Borrower_id = MessageSet["borrower_id"].ToString();
            if (MessageSet.ContainsKey("return_schedule_dt"))
                Return_schedule_dt = MessageSet["return_schedule_dt"].ToString();
            if (MessageSet.ContainsKey("COMMENT"))
                COMMENT = MessageSet["COMMENT"].ToString();
            //if (MessageSet.ContainsKey("Reg_dt"))
            //    Reg_dt = MessageSet["Reg_dt"].ToString();
            //if (MessageSet.ContainsKey("Update_dt"))
            //    Update_dt = MessageSet["Update_dt"].ToString();


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_book " +
                    "                    ( " +
                    "           valid_id, " +
                    "           book_title, " +
                    "           publisher, " +
                    "           purchase_dt, " +
                    "           borrow_situation, " +
                    "           borrower_id, " +
                    "           return_schedule_dt, " +
                    "           comment, " +
                    "           reg_dt," +
                    "           update_dt " +
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "        '" + Valid_id + "', " +
                    "        '" + Book_title + "', " +
                    "        '" + Publisher + "', ";
                if (Purchase_dt != string.Empty)
                    sql += "       TO_DATETIME('" + Purchase_dt + "', 'YYYY-MM-DD'), ";
                else
                    sql += "          null,  ";

                sql += "        '" + Borrow_situation + "', ";
                    if (Borrower_id.Trim() != string.Empty)
                        sql += "    '" + Borrower_id + "', ";
                    else
                        sql += "    null,  ";

                    if (Return_schedule_dt.Trim() != string.Empty)
                        sql += "       TO_DATETIME('" + Return_schedule_dt + "', 'YYYY-MM-DD'), ";
                    else
                        sql += "    null,  ";

                    if (COMMENT.Trim() != string.Empty)
                            sql += "    '" + COMMENT + "', ";
                        else
                            sql += "    null,  ";
                                
                sql += "        CURRENT_DATE, " +
                        "       CURRENT_DATE " +                                                
                        "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                        "            mng_book " +
                        " 	SET " +
                        "           valid_id = '" + Valid_id + "',  " +
                        "           book_title = '" + Book_title + "',  " +
                        "           publisher = '" + Publisher + "',  ";
                    if (Purchase_dt != string.Empty)
                        sql += "    purchase_dt = TO_DATETIME('" + Purchase_dt + "', 'YYYY-MM-DD'), ";
                    else
                        sql += "    purchase_dt = null,  ";
               
                sql += "           borrow_situation = '" + Borrow_situation + "',  ";
                
                if (Borrower_id != string.Empty)
                    sql += "    borrower_id = '" + Borrower_id + "', ";
                else
                    sql += "    borrower_id = null,  ";

                if (Return_schedule_dt != string.Empty)
                    sql += "    return_schedule_dt = TO_DATETIME('" + Return_schedule_dt + "', 'YYYY-MM-DD'), ";
                else
                    sql += "    return_schedule_dt = null,  ";

                if (COMMENT != string.Empty)
                                sql += "    comment = '" + COMMENT + "', ";
                    else
                                sql += "    comment = null, ";

                sql += "           update_dt = CURRENT_DATE  " +
                        "   WHERE " +
                        "      Valid_id = '" + Valid_id + "'";

            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_book " +
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

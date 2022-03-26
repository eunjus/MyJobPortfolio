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
    public class RULE_SPENDINGMANAGEMENT
    {
        //CUBRID DB 연결 정보
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");
  
        public void ConnectionToDB()
        {
            if( dbConnection.State != ConnectionState.Open)
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
        string Spend_date = string.Empty;
        string Spend_content = string.Empty;
        string Spend_type_cd = string.Empty;
        string Spend_type_nm = string.Empty;
        string Staff_Id = string.Empty;
        string Staff_Nm = string.Empty;
        string Spend_cost = string.Empty;
        string Expense_yn = string.Empty;
        string Deposit_date = string.Empty;
        string Receipt_photo = string.Empty;
        string Give_yn = string.Empty;
        string Code_Type = string.Empty;

        byte[] receiptData = null;        
        uint uFileSize;

        public RULE_SPENDINGMANAGEMENT()
        {
            //* MessageSet *//
             QueryType = string.Empty;
             StartDate = string.Empty;
             EndDate = string.Empty;
             Valid_id = string.Empty;
             Spend_date = string.Empty;
             Spend_content = string.Empty;
             Spend_type_cd = string.Empty;
             Spend_type_nm = string.Empty;
             Staff_Id = string.Empty;
             Staff_Nm = string.Empty;
             Spend_cost = string.Empty;
             Expense_yn = string.Empty;
             Deposit_date = string.Empty;
             Receipt_photo = string.Empty;
             Give_yn = string.Empty;
             Code_Type = string.Empty;


             receiptData = null;
             uFileSize = 0;
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
            if (MessageSet.ContainsKey("Code_Type"))
                Code_Type = MessageSet["Code_Type"].ToString();
            if (MessageSet.ContainsKey("Spend_type_cd"))
                Spend_type_cd = MessageSet["Spend_type_cd"].ToString();
            if (MessageSet.ContainsKey("Spend_type_nm"))
                Spend_type_nm = MessageSet["Spend_type_nm"].ToString();


            if (searchtype == "searchStaffInfo")
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
            else if (searchtype == "searchSpendType")
            {
                 sql = " SELECT" +
                    "                 type_code, " +
                    "                 code_name  " +
                    " 	        FROM 	mng_gubun_code" +
                    "           WHERE 1 = 1";                
                if (Spend_type_cd != string.Empty)
                    sql += "    AND type_code = '" + Spend_type_cd + "' ";

                if (Spend_type_nm != string.Empty)
                    sql += "    AND code_name = '" + Spend_type_nm + "' ";

            }

            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {

                da.Fill(ds);

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

            if (MessageSet.ContainsKey("StartDate"))
                StartDate = MessageSet["StartDate"].ToString();
            if (MessageSet.ContainsKey("EndDate"))
                EndDate = MessageSet["EndDate"].ToString();



            sql = " SELECT" +
                    "        a.valid_id , " +
                    "        d.type_code spend_type_code, " +
                    "        d.code_name spend_type_name, " +
                    "        a.spend_content ," +
                    "        TO_CHAR(a.spend_date, 'YYYY-MM-DD') spend_date, " +
                    "        b.staff_name, " +
                    "        a.staff_id, " +
                    "        a.spend_cost, " +
                    "        d.code_name cost_handle_type, " +
                    "        a.give_yn, " +
                    "        TO_CHAR(a.deposit_dt, 'YYYY-MM-DD') deposit_dt, " +
                    "        a.expense_yn, " +
                    "        a.receipt_photo" +
                    " " +
                    "   FROM " +
                    "               mng_spending a INNER JOIN mng_staff b ON a.staff_id = b.staff_id,  " +
                    "               mng_spending c INNER JOIN mng_gubun_code d ON c.spend_type_code = d.type_code  " +
                    "   WHERE 1 = 1";
            if (StartDate != string.Empty && EndDate != string.Empty)
                sql += "  AND a.spend_date BETWEEN TO_DATETIME('" + StartDate + "', 'YYYY-MM-DD') AND "+ " TO_DATETIME('" + EndDate + "', 'YYYY-MM-DD') ";

            sql += " GROUP BY a.valid_id " +
                    " ORDER BY a.spend_date ";       

            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {
                
                da.Fill(ds);

                DisconnectionToDB();

                return ds;
            }                
        }
        public Image GetReceiptPhoto(Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataTable ds = new DataTable();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("valid_id"))
                Valid_id = MessageSet["valid_id"].ToString();

            sql = " SELECT " +
                 "           a.receipt_photo  " +
                 " FROM " +
                 "       mng_spending a " +
                 " WHERE 1 = 1 ";
            if (Valid_id != string.Empty)
                sql += "    AND a.valid_id = '" + Valid_id + "' ";

            sql += " GROUP BY a.valid_id  ";

            using (CUBRIDCommand cmd = new CUBRIDCommand(sql, dbConnection))
            {
                DbDataReader reader = cmd.ExecuteReader();

                ds.Columns.Add("receipt_photo");

                while (reader.Read())
                {

                    CUBRIDBlob bImage = (CUBRIDBlob)reader["receipt_photo"];
                    if (bImage.BlobLength > 0)
                    {
                        byte[] bytes = new byte[(int)bImage.BlobLength];
                        bytes = bImage.GetBytes(1, (int)bImage.BlobLength);

                        DisconnectionToDB();

                        return BlobToImg(bytes);
                    }
                }
            }
            DisconnectionToDB();

            return null;
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
            if (MessageSet.ContainsKey("spend_date"))
                Spend_date = MessageSet["spend_date"].ToString();
            if (MessageSet.ContainsKey("spend_content"))
                Spend_content = MessageSet["spend_content"].ToString();
            if (MessageSet.ContainsKey("spend_type_code"))
                Spend_type_cd = MessageSet["spend_type_code"].ToString();
            if (MessageSet.ContainsKey("spend_type_name"))
                Spend_type_nm = MessageSet["spend_type_name"].ToString();
            if (MessageSet.ContainsKey("staff_id"))
                Staff_Id = MessageSet["staff_id"].ToString();
            if (MessageSet.ContainsKey("spend_cost"))
                Spend_cost = MessageSet["spend_cost"].ToString();
            if (MessageSet.ContainsKey("expense_yn"))
                Expense_yn = MessageSet["expense_yn"].ToString();
            if (MessageSet.ContainsKey("deposit_date"))
                Deposit_date = MessageSet["deposit_date"].ToString();
            if (MessageSet.ContainsKey("receipt_photo"))
            {
                Receipt_photo = MessageSet["receipt_photo"].ToString();
                if(Receipt_photo != "null")
                    ImgToBlob(Receipt_photo);
            }
            if (MessageSet.ContainsKey("give_yn"))
                Give_yn = MessageSet["give_yn"].ToString();


            if (QueryType == "Insert" )
            {
                sql = " INSERT INTO " +
                    "        mng_spending " +
                    "                    ( " +
                    "           valid_id, " +
                    "           spend_date, " +
                    "           staff_id, " +
                    "           spend_cost, " +
                    "           spend_content," +
                    "           receipt_photo, " +
                    "           give_yn, " +
                    "           deposit_dt, " +
                    "           spend_type_code, " +
                    "           expense_yn," +
                    "           reg_dt, " +
                    "           update_dt " +

                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "        '" + Valid_id + "', " +
                    "       TO_DATETIME('" + Spend_date + "', 'YYYY-MM-DD'), " +
                    "        '" + Staff_Id + "', " +
                    "        " + Spend_cost + ", " +
                    "        '" + Spend_content + "', ";
                    //직원사진
                if (Receipt_photo != string.Empty)
                    sql += "        ?, ";
                else
                    sql += " null, ";

                sql += "        '" + Give_yn + "',";

                if (Deposit_date != string.Empty)
                    sql += "  TO_DATETIME('" + Deposit_date + "', 'YYYY-MM-DD'), ";
                else
                    sql += "  null, ";

            sql +=  "        '" + Spend_type_cd + "'," +
                    "        '" + Expense_yn + "'," +
                    "        CURRENT_DATE," +
                    "        CURRENT_DATE" +
                    "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                    "            mng_spending " +
                    " 	SET " +                  
                    "           spend_date = TO_DATETIME('" + Spend_date + "', 'YYYY-MM-DD'), " +
                    "           staff_id = '" + Staff_Id + "' , " +
                    "           spend_cost = " + Spend_cost + " , " +
                    "           spend_content = '" + Spend_content + "' , ";
                //직원사진
                if (Receipt_photo != string.Empty && Receipt_photo != "null")
                    sql += "  receipt_photo = ?, ";
                else if (Receipt_photo != string.Empty && Receipt_photo == "null")
                    sql += "  receipt_photo = null, ";

                sql += "        give_yn = '" + Give_yn + "', ";

                //지급날짜
                if (Deposit_date != string.Empty)
                    sql += "  deposit_dt = TO_DATETIME('" + Deposit_date + "', 'YYYY-MM-DD'), ";
                else
                    sql += "  deposit_dt = null, ";


                sql += "           spend_type_code = '" + Spend_type_cd + "' , " +
                    "           expense_yn = '" + Expense_yn + "' , " +
                    "           update_dt = CURRENT_DATE " +
                    " 	WHERE " +
                    "        valid_id = '" + Valid_id + "' ";
            }
            else if (QueryType == "Delete" )
            {
                sql = " DELETE FROM " +
                       "        mng_spending " +
                       " 	WHERE " +
                       "        valid_id = '" + Valid_id + "' ";
            }
            using (CUBRIDCommand com = new CUBRIDCommand(sql, dbConnection))
            {
                try
                {
                    if (receiptData != null)
                    {
                        CUBRIDBlob blob = new CUBRIDBlob(dbConnection);
                        blob.SetBytes(1, receiptData);

                        CUBRIDParameter param = new CUBRIDParameter();
                        //Image file 
                        param.ParameterName = "?";
                        param.CUBRIDDataType = CUBRIDDataType.CCI_U_TYPE_BLOB;
                        param.Value = blob;
                        com.Parameters.Add(param);
                    }

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

        private void ImgToBlob(string fileFullpath)
        {
            FileStream fs = new FileStream(fileFullpath, FileMode.Open, FileAccess.Read);
            uFileSize = (uint)fs.Length;

            receiptData = new byte[uFileSize];
            fs.Read(receiptData, 0, (int)uFileSize);
            fs.Close();

            return;

        }

        private Image BlobToImg(byte[] imgblob)
        {
            byte[] image = imgblob;
            MemoryStream ms = new MemoryStream(image);
            Image staff_img = Image.FromStream(ms);

            return staff_img;

        }

    }
}

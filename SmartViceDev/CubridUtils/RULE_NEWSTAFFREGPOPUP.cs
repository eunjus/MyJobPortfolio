using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUBRID.Data.CUBRIDClient;
using System.IO;
using System.Drawing;
using System.Data.Common;
using System.Windows.Forms;
using System.ComponentModel;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev.CubridUtils
{
    public class RULE_NEWSTAFFREGPOPUP
    {
        //CUBRID DB 연결 정보
        //private CUBRIDConnection dbConnection = new CUBRIDConnection("server=172.30.1.18;database=smartvicedb;port=30000;user=jetspurt;password=jetspurt0718;charset=utf-8");
        private CUBRIDConnection dbConnection = new CUBRIDConnection("server=127.0.0.1;database=smartvicedb;port=30000;user=dba;password=1214;charset=utf-8");
  
        public void ConnectionToDB()
        {
            if(dbConnection.State != ConnectionState.Open)
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
        string Staff_Id = string.Empty;
        string Staff_name = string.Empty;
        string Staff_gender = string.Empty;
        string Staff_email = string.Empty;
        string Staff_Dep = string.Empty;
        string Staff_Pos = string.Empty;
        string Staff_TelNum = string.Empty;
        string Staff_CelNum = string.Empty;
        string Bank_Name = string.Empty;
        string Account_number = string.Empty;
        string Account_name = string.Empty;
        string Staff_YNmarriage = string.Empty;
        string Staff_Dtmarriage = string.Empty;
        string Staff_DtJoin = string.Empty;
        string Staff_DtRetire = string.Empty;
        string Staff_Birth = string.Empty;
        string Staff_Type = string.Empty;
        string Staff_IMGPath = string.Empty;
        string staff_IMGSize = string.Empty;
        string Staff_JobFile = string.Empty;
        string Staff_Addr = string.Empty;        
        string chk_IMG = string.Empty;        

        byte[] ImgrawData = null;
        byte[] filerawData = null;
        //string fileFullPath = string.Empty;
        uint uFileSize = 0;
        //string FileSize = string.Empty;

        public RULE_NEWSTAFFREGPOPUP()
        {
            //* MessageSet *//
             QueryType = string.Empty;
             Staff_Id = string.Empty;
             Staff_name = string.Empty;
             Staff_gender = string.Empty;
             Staff_email = string.Empty;
             Staff_Dep = string.Empty;
             Staff_Pos = string.Empty;
             Staff_TelNum = string.Empty;
             Staff_CelNum = string.Empty;
             Bank_Name = string.Empty;
             Account_number = string.Empty;
             Account_name = string.Empty;
             Staff_YNmarriage = string.Empty;
             Staff_Dtmarriage = string.Empty;
             Staff_DtJoin = string.Empty;
             Staff_DtRetire = string.Empty;
             Staff_Birth = string.Empty;
             Staff_Type = string.Empty;
             Staff_IMGPath = string.Empty;
             staff_IMGSize = string.Empty;
             Staff_JobFile = string.Empty;
             Staff_Addr = string.Empty;
             chk_IMG = string.Empty;

             ImgrawData = null;
             filerawData = null;
            //string fileFullPath = string.Empty;
             uFileSize = 0;
        }

        //공통 데이터 가져오는 쿼리 - 공통데이터 가져오는 cs 파일을 따로 하나 만드는게 나을듯.
        public DataTable SearchCommon(string searchtype , Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataTable dt = new DataTable();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();

            if (searchtype == "GetStaffId")
            {
                sql = "    SELECT    " +
                       //"       SUBSTR(staff_id,0,10) JetId, " + 
                       "       MAX(SUBSTR(staff_id,11)) max_staff_Id" +
                       " 	 FROM 	mng_staff";
                        //"    GROUP BY SUBSTR(staff_id,0,10)" ;

            }
            else if (searchtype == "searchStaffId")
            {
                sql = "   SELECT    " +
                        "       staff_id " +
                        " 	FROM 	mng_staff " +
                        "   WHERE 1 = 1 ";
                if (Staff_Id != string.Empty)
                    sql += "    AND staff_id = '" + Staff_Id + "' ";
            }
            else if (searchtype == "GetDepart")
            {
                 sql = " SELECT depart_id, " +
                    "                 depart_name , " +
                    "                 depart_sort_order " +
                    " 	        FROM 	mng_depart";

            }
            else if (searchtype == "GetPosition")
            {
                sql = "         SELECT      position_id, " +
                   "                        position_name " +
                   " 	        FROM 	mng_position";

            }
            else if (searchtype == "GetStaffType")
            {
                sql = "         SELECT staff_type_id, " +
                   "                 staff_type_name " +
                   " 	        FROM 	mng_staff_type";

            }
            using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
            {

                da.Fill(dt);

                DisconnectionToDB();

                return dt;
            }            
        }

        //* 조회 *//
        public DataTable Search(Dictionary<string,string> MessageSet)
        {
            ConnectionToDB();

            DataTable ds = new DataTable();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();

                sql = " SELECT " +
                   "           a.staff_id , " +
                   "           a.staff_name , " +
                   "           a.staff_email , " +
                   "           b.position_name , " +
                   "           d.depart_name ," +
                   "           f.staff_type_name , " +
                   "           TO_CHAR(a.tel_number) tel_number, " +
                   "           a.cel_number , " +
                   "           a.gender , " +
                   "           h.account_name , " +
                   "           h.account_number , " +
                   "           h.bank_name , " +
                   "           a.marriage_yn , " +
                   "           COALESCE( a.marriage_dt,CURRENT_DATE ) marriage_dt , " +
                   "           COALESCE( a.join_dt,CURRENT_DATE ) join_dt , " +
                   "           COALESCE( a.resign_dt,'' ) resign_dt , " +
                   "           a.staff_photo , " +
                   "           COALESCE( a.birth_dt,CURRENT_DATE ) birth_dt , " +                   
                   "           a.address , " +
                   "           TO_CHAR(a.reg_dt, 'YYYY-MM-DD') reg_dt ," +
                   "           TO_CHAR(a.update_dt, 'YYYY-MM-DD') update_dt " +
                   " FROM " +
                   "       mng_staff a INNER JOIN mng_position b ON a.position_id = b.position_id ,  " +
                   "       mng_staff c INNER JOIN mng_depart d ON c.depart_id = d.depart_id,  " +
                   "       mng_staff e INNER JOIN mng_staff_type f ON e.staff_type_id = f.staff_type_id,  " +
                   "       mng_staff g LEFT JOIN mng_bank h ON g.staff_id = h.id  " +
                   " WHERE 1 = 1 ";
            if (Staff_Id != string.Empty)
            {
                sql += "    AND a.staff_id = '" + Staff_Id + "' "  +
                       "    AND a.staff_id = g.staff_id  " ;
            }

            sql += " GROUP BY a.staff_id  ";
     
                using (CUBRIDDataAdapter da = new CUBRIDDataAdapter(sql, dbConnection))
                {

                    da.Fill(ds);

                    DisconnectionToDB();

                    return ds;
                }            
        }

        public byte[] GetStaffPhoto(Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataTable ds = new DataTable();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();

            sql = " SELECT " +
                 "           a.staff_photo  " +
                 " FROM " +
                 "       mng_staff a " +
                 " WHERE 1 = 1 ";
            if (Staff_Id != string.Empty)
                sql += "    AND a.staff_id = '" + Staff_Id + "' ";

            sql += " GROUP BY a.staff_id  ";

            using (CUBRIDCommand cmd = new CUBRIDCommand(sql, dbConnection))
            {
                DbDataReader reader = cmd.ExecuteReader();

                ds.Columns.Add("staff_photo");

                while (reader.Read())
                {

                    CUBRIDBlob bImage = (CUBRIDBlob)reader["staff_photo"];
                    if (bImage.BlobLength > 0)
                    {
                        byte[] bytes = new byte[(int)bImage.BlobLength];
                        bytes = bImage.GetBytes(1, (int)bImage.BlobLength);

                        DisconnectionToDB();

                        return bytes;
                    }
                }
            }
            DisconnectionToDB();

            return null;
        }

        public byte[] GetJobFile(Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataTable ds = new DataTable();
            string sql = string.Empty;

            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();

            sql = " SELECT " +
                 "           a.career_file  " +
                 " FROM " +
                 "       mng_staff a " +
                 " WHERE 1 = 1 ";
            if (Staff_Id != string.Empty)
                sql += "    AND a.staff_id = '" + Staff_Id + "' ";

            sql += " GROUP BY a.staff_id  ";

            using (CUBRIDCommand cmd = new CUBRIDCommand(sql, dbConnection))
            {
                DbDataReader reader = cmd.ExecuteReader();

                ds.Columns.Add("career_file");

                while (reader.Read())
                {
                    CUBRIDBlob bJobfile = (CUBRIDBlob)reader["career_file"];
                    if (bJobfile.BlobLength > 0)
                    {
                        byte[] bytes = new byte[(int)bJobfile.BlobLength];
                        bytes = bJobfile.GetBytes(1, (int)bJobfile.BlobLength);

                        DisconnectionToDB();

                        return bytes;
                    }
                }
            }
            DisconnectionToDB();

            return null;
        }
        private byte[] newbyte(int v)
        {
            throw new NotImplementedException();
        }

        //* 저장 - 추가,삭제,갱신 기능
        public bool Save(Dictionary<string, string> MessageSet)
        {
            ConnectionToDB();

            DataSet ds = new DataSet();
            string sql = string.Empty;
            if (MessageSet.ContainsKey("QueryType"))
                QueryType = MessageSet["QueryType"].ToString();
            if (MessageSet.ContainsKey("Staff_Id"))
                Staff_Id = MessageSet["Staff_Id"].ToString();
            if (MessageSet.ContainsKey("staff_name"))
                Staff_name = MessageSet["staff_name"].ToString();
            if (MessageSet.ContainsKey("staff_gender"))
                Staff_gender = MessageSet["staff_gender"].ToString();
            if (MessageSet.ContainsKey("staff_email"))
                Staff_email = MessageSet["staff_email"].ToString();
            if (MessageSet.ContainsKey("staff_pos"))
                Staff_Pos = MessageSet["staff_pos"].ToString();
            if (MessageSet.ContainsKey("staff_dep"))
                Staff_Dep = MessageSet["staff_dep"].ToString();
            if (MessageSet.ContainsKey("staff_telnum"))
                Staff_TelNum = MessageSet["staff_telnum"].ToString();
            if (MessageSet.ContainsKey("staff_celnum"))
                Staff_CelNum = MessageSet["staff_celnum"].ToString();
            if (MessageSet.ContainsKey("account_name"))
                Account_name = MessageSet["account_name"].ToString();
            if (MessageSet.ContainsKey("account_number"))
                Account_number = MessageSet["account_number"].ToString();
            if (MessageSet.ContainsKey("bank_name"))
                Bank_Name = MessageSet["bank_name"].ToString();
            if (MessageSet.ContainsKey("staff_YNmarriage"))
                Staff_YNmarriage = MessageSet["staff_YNmarriage"].ToString();
            if (MessageSet.ContainsKey("staff_Dtmarriage"))
                Staff_Dtmarriage = MessageSet["staff_Dtmarriage"].ToString();
            if (MessageSet.ContainsKey("staff_DtJoin"))
                Staff_DtJoin = MessageSet["staff_DtJoin"].ToString();
            if (MessageSet.ContainsKey("staff_DtRetire"))
                Staff_DtRetire = MessageSet["staff_DtRetire"].ToString();
            if (MessageSet.ContainsKey("staff_birth"))
                Staff_Birth = MessageSet["staff_birth"].ToString();
            if (MessageSet.ContainsKey("staff_Type"))
                Staff_Type = MessageSet["staff_Type"].ToString();
            if (MessageSet.ContainsKey("staff_IMGPath"))
            {
                Staff_IMGPath = MessageSet["staff_IMGPath"].ToString();
                staff_IMGSize = MessageSet["staff_IMGSize"].ToString();

                ImgToBlob(Staff_IMGPath);
            }
            if (MessageSet.ContainsKey("chk_IMG"))
                chk_IMG = MessageSet["chk_IMG"].ToString();

            if (MessageSet.ContainsKey("staff_JobFile"))
            {
                Staff_JobFile = MessageSet["staff_JobFile"].ToString();
                filerawData = StreamFile(Staff_JobFile);

            }
            if (MessageSet.ContainsKey("staff_addr"))
                Staff_Addr = MessageSet["staff_addr"].ToString();           


            if (QueryType == "Insert")
            {
                sql = " INSERT INTO " +
                    "        mng_staff " +
                    "                    ( " +
                    "           staff_id, " +
                    "           staff_type_id, " +
                    "           staff_name, " +
                    "           staff_email, " +
                    "           tel_number," +
                    "           cel_number, " +
                    "           position_id, " +
                    "           depart_id, " +
                    "           gender, " +
                    "           marriage_yn," +
                    "           marriage_dt, " +
                    "           birth_dt, " +
                    "           address, " +
                    "           join_dt, " +
                    "           resign_dt," +
                    "           career_file, " +
                    "           staff_photo," +
                    "           COMMENT, " +
                    "           reg_dt," +
                    "           update_dt " +
                    "                    ) " +
                    " 	VALUES " +
                    "       ( " +
                    "        '" + Staff_Id + "', " +
                    "       (SELECT staff_type_id FROM mng_staff_type WHERE staff_type_name = '" + Staff_Type + "' ), " +
                    "        '" + Staff_name + "', " +
                    "        '" + Staff_email + "', ";
                //결혼여부
                if (Staff_TelNum != string.Empty)
                    sql += " " + Staff_TelNum + ", ";
                else
                    sql += " null, ";

                sql += "        " + Staff_CelNum + ", " +
                     "       (SELECT position_id FROM mng_position WHERE position_name = '" + Staff_Pos + "' ), " +
                     "       (SELECT depart_id FROM mng_depart WHERE depart_name = '" + Staff_Dep + "' ), " +
                     "        '" + Staff_gender + "', ";
                //결혼여부
                if (Staff_YNmarriage != string.Empty)
                    sql += " '" + Staff_YNmarriage + "', ";
                else
                    sql += " null, ";
                //결혼날짜
                if (Staff_Dtmarriage != string.Empty)
                    sql += "        TO_DATETIME('" + Staff_Dtmarriage + "', 'YYYY-MM-DD'), ";
                else
                    sql += " null, ";
                //생일
                if (Staff_Birth != string.Empty)
                    sql += "        TO_DATETIME('" + Staff_Birth + "', 'YYYY-MM-DD'), ";
                else
                    sql += " null, ";
                //주소
                if (Staff_Addr != string.Empty)
                    sql += " '" + Staff_Addr + "', ";
                else
                    sql += " null, ";

                sql += "        TO_DATETIME('" + Staff_DtJoin + "', 'YYYY-MM-DD'), ";

                //퇴사날짜
                if (Staff_DtRetire != string.Empty)
                    sql += "        TO_DATETIME('" + Staff_DtRetire + "', 'YYYY-MM-DD'), ";
                else
                    sql += " null, ";

                //이력서
                if (Staff_JobFile != string.Empty)
                    sql += "        ?, ";
                else
                    sql += " null, ";

                //직원사진
                if (Staff_IMGPath != string.Empty)
                    sql += "        ?, ";
                else
                    sql += " null, ";

                sql += "        null, " +
                        "        CURRENT_DATE, " +
                        "        CURRENT_DATE " +
                        "        ) ";
            }
            else if (QueryType == "Insert_bank_Acct")
            {
                sql = " INSERT INTO " +
                    "        mng_bank " +
                    "                    ( " +
                    "           staff_type_id, " +
                    "           id, " +
                    "           bank_name, " +
                    "           account_number," +
                    "           account_name, " +
                    "           COMMENT, " +
                    "           reg_dt, " +
                    "           update_dt " +
                    "        ) " +
                    " 	VALUES " +
                    "       ( " +
                    "       (SELECT staff_type_id FROM mng_staff_type WHERE staff_type_name = '" + Staff_Type + "' ), " +
                    "        '" + Staff_Id + "', " +
                    "        '" + Bank_Name + "', " +
                    "        '" + Account_number + "', " +
                    "        '" + Account_name + "', " +
                    "        null, " +
                    "        CURRENT_DATE, " +
                    "        CURRENT_DATE" +
                    "        ) ";
            }
            else if (QueryType == "Update")
            {
                sql = "     UPDATE  " +
                        "            mng_staff " +
                        " 	SET " +
                        "           staff_id = '" + Staff_Id + "' , " +
                        "           staff_type_id = (SELECT staff_type_id FROM mng_staff_type WHERE staff_type_name = '" + Staff_Type + "')," +
                        "           staff_name = '" + Staff_name + "' , " +
                        "           staff_email = '" + Staff_email + "' , ";
                if (Staff_TelNum != string.Empty)
                    sql += "  tel_number = '" + Staff_TelNum + "' , ";
                else
                    sql += "  tel_number = null, ";

                sql += "    cel_number = '" + Staff_CelNum + "' , " +
                        "   position_id = (SELECT position_id FROM mng_position WHERE position_name = '" + Staff_Pos + "' ), " +
                        "   depart_id = (SELECT depart_id FROM mng_depart WHERE depart_name = '" + Staff_Dep + "' ), " +
                        "   gender = '" + Staff_gender + "' , ";

                if (Staff_YNmarriage != string.Empty)
                    sql += "  marriage_yn = '" + Staff_YNmarriage + "' , ";
                else
                    sql += "  marriage_yn = null, ";

                if (Staff_Dtmarriage != string.Empty)
                    sql += "  marriage_dt = TO_DATETIME('" + Staff_Dtmarriage + "', 'YYYY-MM-DD'), ";
                else
                    sql += "  marriage_dt = null, ";

                if (Staff_Birth != string.Empty)
                    sql += "  birth_dt = '" + Staff_Birth + "' , ";
                else
                    sql += "  birth_dt = null, ";

                if (Staff_Addr != string.Empty)
                    sql += "  address = '" + Staff_Addr + "' , ";
                else
                    sql += "  address = null, ";

                sql += " join_dt = TO_DATETIME('" + Staff_DtJoin + "', 'YYYY-MM-DD'), ";

                //퇴사날짜
                if (Staff_DtRetire != string.Empty)
                    sql += " resign_dt = TO_DATETIME('" + Staff_DtRetire + "', 'YYYY-MM-DD'), ";
                else
                    sql += " resign_dt = null, ";

                //이력서
                if (Staff_JobFile != string.Empty)
                    sql += "  career_file =  ?p2, ";
                else
                    sql += " career_file = null, ";

                //이미지 변경 사항 있을 때만 해당 쿼리 적용
                if (chk_IMG == string.Empty)
                {
                    if (Staff_IMGPath != string.Empty && Staff_IMGPath != "unedited")
                        sql += "  staff_photo = ?p1, ";
                    else if (Staff_IMGPath == "unedited")
                        sql += " staff_photo = null, ";
                }
                
                sql += "           comment = NULL,  " +
                        "           update_dt = CURRENT_DATE  " +
                        "   WHERE " +
                        "      staff_id = '" + Staff_Id + "'";

            }
            else if (QueryType == "Update_bank_Acct")
            {
                sql = "     UPDATE  " +
                    "            mng_bank " +
                    " 	SET " +              
                    "           bank_name = '" + Bank_Name + "', " +
                    "           account_number = '" + Account_number + "', " +
                    "           account_name = '" + Account_name + "', " +
                    "           update_dt = CURRENT_DATE " +
                    "   WHERE " +
                    "           staff_type_id = (SELECT staff_type_id FROM mng_staff_type WHERE staff_type_name = '" + Staff_Type + "' ) " +
                    "     AND  id = '" + Staff_Id + "'";

            }
                    
            //else if (QueryType == "Delete")
            //{
            //    sql = " DELETE FROM " +
            //           "        mng_depart " +
            //           " 	WHERE " +
            //           "        depart_id = '" + Depart_id + "' ";
            //}
            using (CUBRIDCommand com = new CUBRIDCommand(sql, dbConnection))
            {
                try
                {
                    if (ImgrawData != null && filerawData != null)
                    {
                        CUBRIDBlob Imgblob = new CUBRIDBlob(dbConnection);
                        Imgblob.SetBytes(1, ImgrawData);

                        CUBRIDParameter p1 = new CUBRIDParameter();
                        //Image file 
                        p1.ParameterName = "?p1";
                        p1.CUBRIDDataType = CUBRIDDataType.CCI_U_TYPE_BLOB;
                        p1.Value = Imgblob;
                        com.Parameters.Add(p1);

                        //job file 
                        CUBRIDBlob Jobfileblob = new CUBRIDBlob(dbConnection);
                        Jobfileblob.SetBytes(1, filerawData);
                        CUBRIDParameter p2 = new CUBRIDParameter();
                        p2.ParameterName = "?p2";
                        p2.CUBRIDDataType = CUBRIDDataType.CCI_U_TYPE_BLOB;
                        p2.Value = Jobfileblob;
                        com.Parameters.Add(p2);
                    }
                    else if (ImgrawData == null && filerawData != null)
                    {
                        CUBRIDBlob blob = new CUBRIDBlob(dbConnection);
                        blob.SetBytes(1, filerawData);

                        CUBRIDParameter param = new CUBRIDParameter();
                        //Image file 
                        param.ParameterName = "?p2";
                        param.CUBRIDDataType = CUBRIDDataType.CCI_U_TYPE_BLOB;
                        param.Value = blob;
                        com.Parameters.Add(param);
                    }
                    else if (ImgrawData != null && filerawData == null)
                    {
                        CUBRIDBlob blob = new CUBRIDBlob(dbConnection);
                        blob.SetBytes(1, ImgrawData);

                        CUBRIDParameter param = new CUBRIDParameter();
                        //Image file 
                        param.ParameterName = "?p1";
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

            ImgrawData = new byte[uFileSize];
            fs.Read(ImgrawData, 0, (int)uFileSize);
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

        private byte[] StreamFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            byte[] FileData = new byte[fs.Length];

            fs.Read(FileData, 0, System.Convert.ToInt32(fs.Length));

            fs.Close();

            return FileData;
        }
    }
}

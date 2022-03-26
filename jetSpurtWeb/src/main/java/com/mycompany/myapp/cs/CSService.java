package com.mycompany.myapp.cs;

import java.util.List;
import java.util.Map;

public interface CSService {

	List<Map<String, Object>> selectBoardList(Map<String, Object> map) throws Exception;
	List<Map<String, Object>> indexBoardList () throws Exception;
	List<Map<String, Object>> selectReply(Map<String, Object> map)throws Exception;
	List<Map<String, Object>> selectPostFile(Map<String, Object> map)throws Exception;
	
	void insertBoard(Map<String, Object> map) throws Exception;
	void insertReply(Map<String, Object> map) throws Exception;
	void insertFile(Map<String, Object> map) throws Exception;
	
	void updateNotice(Map<String, Object> map) throws Exception;
	void updateQna(Map<String, Object> map) throws Exception;
	void updateRepliedQna(Map<String, Object> map) throws Exception;
	void updateReply(Map<String, Object> map) throws Exception;
	
	void deleteBoard(Map<String, Object> map) throws Exception;
	void deleteReply(Map<String, Object> map) throws Exception;	
	void deleteFile(Map<String, Object> map) throws Exception;
}

package com.mycompany.myapp.dao;

import java.util.List;
import java.util.Map;

import org.springframework.stereotype.Repository;

import com.mycompany.myapp.common.CommandMap;
import com.mycompany.myapp.dao.AbstractDAO;

@ Repository("NoticeDAO")
public class NoticeDAO extends AbstractDAO {

	 @ SuppressWarnings("unchecked")
	 public List<Map<String, Object >> selectBoardList(Map < String, Object > map)throws Exception {
	    return (List<Map<String, Object >> )selectList("cs.selectBoardList", map);
	 }
	 
	 @ SuppressWarnings("unchecked")
	 public List<Map<String, Object >> selectPostFile(Map < String, Object > map)throws Exception {
	    return (List<Map<String, Object >> )selectList("cs.selectPostFile", map);
	 }	 	 
	 
	 @SuppressWarnings("unchecked")
	public List<Map<String, Object >> indexBoardList() throws Exception {
	    return (List<Map<String, Object >> )selectList("cs.indexBoardList");
	 }

	public void insertBoard(Map < String, Object > map) throws Exception{
		insert("cs.insertBoard", map);
	}
	
	public void insertFile(Map < String, Object > map) throws Exception{
		insert("cs.insertFile", map);
	}

	public void updateNotice(Map<String, Object> map) throws Exception{
		// TODO Auto-generated method stub
		update("cs.updateNotice", map);
	}

	public void deleteBoard(Map<String, Object> map) throws Exception{
		// TODO Auto-generated method stub
		update("cs.deleteBoard", map);
	}

	public void updateQna(Map<String, Object> map) throws Exception{
		// TODO Auto-generated method stub
		update("cs.updateQna", map);
	}

	public void insertReply(Map<String, Object> map) throws Exception{
		// TODO Auto-generated method stub
		insert("cs.insertReply", map);
	}

	public void updateRepliedQna(Map<String, Object> map) throws Exception{
		// TODO Auto-generated method stub
		update("cs.updateRepliedQna", map);
	}

	@SuppressWarnings("unchecked")
	public List<Map<String, Object>> selectReply(Map<String, Object> map) throws Exception{
		// TODO Auto-generated method stub
		return (List<Map<String, Object >> )selectList("cs.selectReply", map);
	}

	public void deleteReply(Map<String, Object> map) {
		// TODO Auto-generated method stub
		update("cs.deleteReply", map);
	}

	public void updateReply(Map<String, Object> map) {
		// TODO Auto-generated method stub
		update("cs.updateReply", map);
	}
	
	public void deleteFile(Map<String, Object> map) {
		// TODO Auto-generated method stub
		update("cs.deleteFile", map);
	}
	
}

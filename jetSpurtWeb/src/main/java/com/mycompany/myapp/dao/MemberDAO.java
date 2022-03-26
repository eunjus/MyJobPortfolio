package com.mycompany.myapp.dao;

import java.util.List;
import java.util.Map;

import org.springframework.stereotype.Repository;

import com.mycompany.myapp.dao.AbstractDAO;

@ Repository("MemberDAO")
public class MemberDAO extends AbstractDAO {

	 @ SuppressWarnings("unchecked")
	 public List<Map<String, Object>> selectUserId(Map < String, Object > map)throws Exception {
	    return (List<Map<String, Object >> )selectList("member.selectUserId", map);
	}

	public void insertNewUser(Map < String, Object > map) throws Exception{
		insert("member.insertNewUser", map);
	}
	
	public void updateUser(Map < String, Object > map) throws Exception{
		update("member.updateUser", map);
	}
	
	public void updateUserPW(Map < String, Object > map) throws Exception{
		update("member.updateUserPW", map);
	}

	public void withdrawalUser(Map<String, Object> map) {
		// TODO Auto-generated method stub
		update("member.withdrawalUser", map);
	}
}

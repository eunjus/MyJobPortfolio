package com.mycompany.myapp.member;

import java.util.List;
import java.util.Map;

public interface MemberService {

	void insertNewUser(Map<String, Object> map) throws Exception;

	void updateUser(Map<String, Object> map) throws Exception;
	
	List<Map<String, Object>> selectUserId(Map<String, Object> commandMap) throws Exception;

	void updateUserPW(Map<String, Object> map) throws Exception;

	void withdrawalUser(Map<String, Object> map) throws Exception;

}

package com.mycompany.myapp.member;

import java.util.List;
import java.util.Map;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import org.springframework.stereotype.Service;

import com.mycompany.myapp.dao.MemberDAO;

@Service("MemberService")
public class MemberServiceImpl implements MemberService {

	private static final Logger logger = LoggerFactory.getLogger(MemberServiceImpl.class);
	
	@Resource(name="MemberDAO")
	private MemberDAO memberDAO;
	

	@Override
	public void insertNewUser(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		memberDAO.insertNewUser(map);
	}

	@Override
	public List<Map<String, Object>> selectUserId(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		return memberDAO.selectUserId(map);
	}

	@Override
	public void updateUser(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		memberDAO.updateUser(map);
	}

	@Override
	public void updateUserPW(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		memberDAO.updateUserPW(map);
	}

	@Override
	public void withdrawalUser(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		memberDAO.withdrawalUser(map);
	}

}

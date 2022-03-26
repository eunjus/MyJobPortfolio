package com.mycompany.myapp.cs;

import java.util.List;
import java.util.Map;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import org.springframework.stereotype.Service;

import com.mycompany.myapp.common.CommandMap;
import com.mycompany.myapp.dao.NoticeDAO;

@Service("CSService")
public class CSServiceImpl implements CSService {

	private static final Logger logger = LoggerFactory.getLogger(CSServiceImpl.class);
	
	@Resource(name="NoticeDAO")
	private NoticeDAO noticeDAO;
	
	@Override
	public List<Map<String, Object>> selectBoardList(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		return noticeDAO.selectBoardList(map);
	}
	
	@Override
	public List<Map<String, Object>> selectPostFile(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		return noticeDAO.selectPostFile(map);
	}
	
	@Override
	public void insertBoard(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.insertBoard(map);
	}

	@Override
	public void insertFile(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.insertFile(map);
	}
	
	@Override
	public List<Map<String, Object>> indexBoardList() throws Exception {
		// TODO Auto-generated method stub
		return noticeDAO.indexBoardList();
	}

	@Override
	public void updateNotice(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.updateNotice(map);
	}

	@Override
	public void deleteBoard(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.deleteBoard(map);
	}

	@Override
	public void updateQna(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.updateQna(map);
	}

	@Override
	public void insertReply(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.insertReply(map);
	}

	@Override
	public void updateRepliedQna(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.updateRepliedQna(map);
	}

	@Override
	public List<Map<String, Object>> selectReply(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		return noticeDAO.selectReply(map);
	}

	@Override
	public void deleteReply(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.deleteReply(map);
	}
	
	@Override
	public void deleteFile(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.deleteFile(map);
	}

	@Override
	public void updateReply(Map<String, Object> map) throws Exception {
		// TODO Auto-generated method stub
		noticeDAO.updateReply(map);
	}

}

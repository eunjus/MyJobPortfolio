package com.mycompany.myapp.cs;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import javax.annotation.Resource;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletRequestWrapper;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.commons.io.FileUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.servlet.ModelAndView;

import com.mycompany.myapp.common.CommandMap;
import com.mycompany.myapp.common.CommonFunc;

/**
 * Handles requests for the application home page.
 */
@Controller
public class CSController {
	
	private static final Logger logger = LoggerFactory.getLogger(CSController.class);
		
	
	@Resource(name="CSService") 
	private CSService csService;
	
	@ RequestMapping(value = "/openSampleBoardList.do")
	 public ModelAndView openSampleBoardList(String code)throws Exception {		
			
		Map<String, Object > commandMap = new HashMap<String, Object>();
		commandMap.put("code", code);	
		
	    ModelAndView mv = new ModelAndView("/boardList");
	    
	    List < Map < String,Object >> list = csService.selectBoardList(commandMap);
	    mv.addObject("list", list);
	    return mv;
	 }
	
	
	 @RequestMapping(value = "/community.do")
	 public ModelAndView openBoardList(HttpSession session, String code)throws Exception {							
		 
		Map<String, Object > commandMap = new HashMap<String, Object>();
		commandMap.put("code", code);	
		
	    ModelAndView mv = new ModelAndView("/community/" + code );
	    
	    if(code.contains("qna_write") || code.contains("notice_write"))
	    	return mv;
	    
	    List < Map < String,Object >> list = csService.selectBoardList(commandMap);
	    mv.addObject("list", list);	    
		
	    Enumeration<String> enum_session = session.getAttributeNames();
		while(enum_session.hasMoreElements()) {
			String key = enum_session.nextElement();
			String value=(String) session.getAttribute(key);
			
			System.out.println("<p>"+key+":"+value+"</p>");
		}
	    
	    //�α��� ���� ������
	    if(session.getAttribute("userid") != null)
	    {
	    	mv.addObject("login", true);
	    	
	    	 //���� ������
		    if(session.getAttribute("authority").equals("ROLE_USER"))
		    	mv.addObject("authority", false);
		    else
				mv.addObject("authority", true);
			 		   
	    }
	    else
	    	mv.addObject("login", null);
	    
	   
	    return mv;
	 }
	 
	 @ RequestMapping(value = "/boardSearch.do")
	 public ModelAndView boardSearch(CommandMap commandMap)throws Exception {	    			
		
		 Map<String, Object > Map = new HashMap<String, Object>();
		 
		 if(commandMap.get("code").equals("notice"))			 		 			 	
		 	Map.put("code", "notice");
		 else if (commandMap.get("code").equals("qna"))
			 Map.put("code", "qna");
		 
		 if(commandMap.get("search_type").equals("title"))
			 Map.put("post_title", commandMap.get("search"));
		 else if (commandMap.get("search_type").equals("content"))
			 Map.put("post", commandMap.get("search"));
		 else if (commandMap.get("search_type").equals("name"))
			 Map.put("post_writer", commandMap.get("search"));

		 List < Map < String,Object >> list = csService.selectBoardList(Map);
			
		ModelAndView mv = new ModelAndView("/community/" + commandMap.get("code"));
	    mv.addObject("list", list);
	    return mv;
	 } 
	 
	 @ RequestMapping(value = "/boardView.do", method = {RequestMethod.GET,RequestMethod.POST})
	 @ResponseBody
	 public ModelAndView openBoardView(HttpServletResponse response,String code, String no, String password, String sub) throws Exception {
	    		 
		 Map<String, Object > commandMap = new HashMap<String, Object>();
		 
		 ModelAndView mv;
		 	
		 //case1. ��б� Ŭ�� ���� �� -> ��� Ȯ�� ���������� �Ѿ���� ���� �����ֱ�
		 //case2. �Խñ� �ȿ� ������ư�� ������ �� -> �Խñ� ���� �������� 
		 if(code.contains("qna_secret"))
		 {
			 mv = new ModelAndView("/community/" + code);
			 mv.addObject("no",no);
			 mv.addObject("sub", sub);
			 return mv;
		 }
		 
		 
		 commandMap.put("no", no);
		 commandMap.put("code", code);
		 commandMap.put("password", password);
		 
		 List < Map < String,Object >> list = csService.selectBoardList(commandMap); //�Խñ� ���� ��ȸ
		 List < Map < String,Object >> fileList = csService.selectPostFile(commandMap); //÷������ ��ȸ
		 
		 //��й�ȣ�� ��ġ���� �ʴ� ���
		 if(list.size() == 0 )	
		 {
			 response.setContentType("text/html; charset=UTF-8");
			 PrintWriter out = response.getWriter();
			 
			 //case2. qna ���� ������
			 if(sub != null && sub.contains("qna_write"))
			 {
				 out.println("<script>alert('��й�ȣ�� ��ġ���� �ʽ��ϴ�.'); location.href='/myapp/boardView.do?code=qna_secret&sub=qna_write&no=" + no + "';</script>");
			 }
			 else
			 {
				 out.println("<script>alert('��й�ȣ�� ��ġ���� �ʽ��ϴ�.'); location.href='/myapp/boardView.do?code=qna_secret&no=" + no + "';</script>");
			 }
			 out.flush();
			

			 mv = new ModelAndView("/community/" + code + "_view");
			 
		 }
		 else
		 {
			 //case2. qna ���� ������
			 if(sub != null && sub.contains("qna_write"))
			 {
				 mv = new ModelAndView("/community/" + sub);
				 mv.addObject("qna", list.get(0));		
				 mv.addObject("fileList", convertByteToSize(fileList));
				 mv.addObject("code", code);
				 mv.addObject("no", no);
			
				 return mv;
			 }
		     
			 mv = new ModelAndView("/community/" + code + "_view");
			 mv.addObject("list", list.get(0));
			 mv.addObject("fileList", convertByteToSize(fileList));
			 
			 commandMap.put("post_category", "qna_reply");
			 List<Map<String,Object>> reply = csService.selectReply(commandMap);
			 
			 if(reply.size() != 0)
				 mv.addObject("reply", reply.get(0));
			 else
				 mv.addObject("reply", null);
			 
		 }					 			 	
		 
	    return mv;
	}
	
	 public List<Map< String,Object >> convertByteToSize(List < Map < String,Object >> fileList) {
		 
		 for(Map < String,Object > file : fileList) {
			 String temp = bytesToSize((int)file.get("file_size"));
			 file.put("file_size",temp);
		 }
		 
		 return fileList;
	 }
	 
	 public String bytesToSize(int bytes) {
		    
		 	String[] sizes = {"Bytes", "KB", "MB", "GB", "TB"};		    
		 	
		    if (bytes == 0) return "";
		    int i =  (int)Math.floor(Math.log(bytes) / Math.log(1024));
		    if (i == 0) return bytes + " " + sizes[i];
		    return String.format("%.1f",(bytes / Math.pow(1024, i))) + " " + sizes[i];
		};
	 
	 @ RequestMapping(value = "/insertBoard2.do")
	 public ModelAndView insertBoard2(CommandMap commandMap,String code, HttpServletRequest request, @RequestParam("mailfile") MultipartFile[] multipartFile
			 , @RequestParam("arrAddedFiles") MultipartFile[] multipartFiles)throws Exception {	    			
		 
		 //���� ���� DB �ݿ�
		 String deleteFiles = request.getParameter("arrDeleteFiles");
		 String[] arrDeleteFiles = deleteFiles.split(",");		 		
		 
		 ModelAndView mv = new ModelAndView("redirect://community.do?code="+code);		 	 
		 
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            
	            if(key.contains("mailfile"))
	            	continue;
	            
	            String value = (String)commandMap.get(key);
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
	     
	     String post_key = UUID.randomUUID() + "";
	     
	     commandMap.put("post_number", post_key);
		 csService.insertBoard(commandMap.getMap());
	    
		 
		 if(multipartFile != null && post_key != null && multipartFiles != null)
		 {
			 
			 //1. �� commandMap ���� �����ؼ� file table �� ���� ������ �� �����
			 //1-1. ��� insert �� �Խñ� ��ȣ ��������
			 //2. �ӽ� ���� ���� ������ ����
			 //3. �ӽ� ���� ���� blob�� ��ȯ �� commandMap�� �ٽ� ����
			 //4. insert			 
			 		
			 fileUpload(multipartFile, post_key, arrDeleteFiles);
		 }
	
		 
	    return mv;
	 }

	 @RequestMapping(value = "/insertBoard.do" , method = RequestMethod.POST)
	 @ResponseBody
	 public String insertBoard(CommandMap commandMap,HttpServletRequest request, @RequestParam("article_file") MultipartFile[] multipartFile)throws Exception {	    			
		 
		 String strResult = "{ \"result\":\"FAIL\" }";
		 
		 try {
			 //���� ���� DB �ݿ�
			 String deleteFiles = request.getParameter("arrDeleteFiles");
			 String[] arrDeleteFiles = deleteFiles.split(",");		 		
			 
			 String code = "";
			 ModelAndView mv = new ModelAndView("redirect://community.do?code="+code);		 	 
			 
			 Iterator<String> keys = commandMap.keySet().iterator();
		        while( keys.hasNext() ){
		            String key = keys.next();
		            
		            if(key.contains("mailfile"))
		            	continue;
		            
		            String value = (String)commandMap.get(key);
		            System.out.println("Ű : "+key+", �� : "+value);
		        }
		     
		     String post_key = UUID.randomUUID() + "";
		     
		     commandMap.put("post_number", post_key);
			 csService.insertBoard(commandMap.getMap());
		    
			 
			 if(multipartFile != null && post_key != null )
			 {
				 
				 //1. �� commandMap ���� �����ؼ� file table �� ���� ������ �� �����
				 //1-1. ��� insert �� �Խñ� ��ȣ ��������
				 //2. �ӽ� ���� ���� ������ ����
				 //3. �ӽ� ���� ���� blob�� ��ȯ �� commandMap�� �ٽ� ����
				 //4. insert			 
				 		
				 strResult = fileUpload(multipartFile, post_key, arrDeleteFiles);
			 }
			 			
		 }catch(Exception e){
				e.printStackTrace();
			}
		 
	    return strResult;
	 }	
	 
	public String fileUpload(MultipartFile[] multipartFile, String file_post_no, String[] arrDeleteFiles ) {

		String strResult = "{ \"result\":\"FAIL\" }";
		// String contextRoot = new HttpServletRequestWrapper(request).getRealPath("/");
		String fileRoot;
		Map<String, Object > fileMap = new HashMap<String, Object>();
		
		boolean chkfile = false;
		
		try {
			// ������ ������ ź��.
			if (multipartFile.length > 0 && !multipartFile[0].getOriginalFilename().equals("")) {

				for (MultipartFile file : multipartFile) {
					
					if(arrDeleteFiles != null)
					{
						chkfile = false;
						//�Խñ� insert �������� �߰��ߴ� ���� ���� üũ
						for(String delFileName : arrDeleteFiles)
						{
							if(delFileName.equals(file.getOriginalFilename()))
							{
								chkfile = true;
								break;
							}
						}
						
						if(chkfile)
							continue;
					}
					
					// fileRoot = contextRoot +  "resources/upload/";
					fileRoot = "C:/Temp/JetspurtWeb/mp/";
					System.out.println(fileRoot);

					String originalFileName = file.getOriginalFilename(); // �������� ���ϸ�
					String extension = originalFileName.substring(originalFileName.lastIndexOf(".")); // ���� Ȯ����
					String savedFileName = UUID.randomUUID() + extension; // ����� ���� ��
					
					fileMap.put("org_file_name",originalFileName);
					
					File targetFile = new File(fileRoot + savedFileName);
					try {					
						
						Map<String, Object > commandMap = new HashMap<String, Object>(); 
						
						InputStream fileStream = file.getInputStream();
						FileUtils.copyInputStreamToFile(fileStream, targetFile); // ���� ����
						
						byte[] file_content = convertFileContentToBlob(fileRoot + savedFileName);//���� BLOB Ÿ������ ��ȯ
						String file_key = UUID.randomUUID() + "";
					     
						commandMap.put("file_number", file_key);
						commandMap.put("post_number", file_post_no);
						commandMap.put("org_file_name", originalFileName);
						commandMap.put("file_content", file_content);
						commandMap.put("file_extension", extension.substring(0,extension.length()));
						commandMap.put("file_size", file.getSize());
						
						csService.insertFile(commandMap);
						
						//DB�� ���� ���� �� �ӽ����� ����
						if(targetFile.exists()) {
							if(targetFile.delete()){ 
								System.out.println("���ϻ��� ����"); 
							}else{ 
								System.out.println("���ϻ��� ����"); 
							}
						}
						
					} catch (Exception e) {
						// ���ϻ���
						FileUtils.deleteQuietly(targetFile); // ����� ���� ���� ����
						e.printStackTrace();
						break;
					}
				}
				strResult = "{ \"result\":\"OK\" }";
			}
			// ���� �ƹ��͵� ÷�� �������� ź��.(�Խ����϶�, ���ε� ���� ���� ����ϴ°��)
			else
				strResult = "{ \"result\":\"OK\" }";
		} catch (Exception e) {
			e.printStackTrace();
		}
		return strResult;
	}

	public static byte[] convertFileContentToBlob(String filePath) throws IOException {
		   byte[] fileContent = null;
		   try {
			fileContent = FileUtils.readFileToByteArray(new File(filePath));
		   } catch (IOException e) {
			throw new IOException("Unable to convert file to byte array. " +
		              e.getMessage());
		   }
		   return fileContent;
	}
	
	@RequestMapping(value="/fileDown")
	public void fileDown(@RequestParam("no") String no, HttpServletResponse response) throws Exception{
		
		Map<String, Object > commandMap = new HashMap<String, Object>(); 
		commandMap.put("file_number",no);
		
		List<Map<String,Object>> fileInfo = csService.selectPostFile(commandMap);
		
		String originalFileName = (String) fileInfo.get(0).get("org_file_name");
		
		// ������ �����ߴ� ��ġ���� ÷�������� �о� byte[]�������� ��ȯ�Ѵ�.
		byte fileByte[] = (byte[]) fileInfo.get(0).get("file_content");
		
		response.setContentType("application/octet-stream");
		response.setContentLength(fileByte.length);
		response.setHeader("Content-Disposition",  "attachment; fileName=\""+URLEncoder.encode(originalFileName, "UTF-8")+"\";");
		response.getOutputStream().write(fileByte);
		response.getOutputStream().flush();
		response.getOutputStream().close();		
	}
		
	 @ RequestMapping(value = "/board_comment_write.do")
	 public ModelAndView insertReply(CommandMap commandMap, String code, String no,String mode)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("redirect://boardView.do?code="+code);		 	 
		 
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            String value = (String)commandMap.get(key);
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
		 
	     if(mode.contains("write"))
	     {	    	 
	    	 csService.insertReply(commandMap.getMap());
	    	 
	    	 commandMap.put("reply_yn", "y");
	    	 csService.updateRepliedQna(commandMap.getMap());
	     }
	     else if (mode.contains("modify"))
	    	 csService.updateReply(commandMap.getMap());
	    	 
//		 List<Map<String,Object>> list = csService.selectBoardList(commandMap.getMap());
//		 
//		 commandMap.put("post_category", "qna_reply");
//		 List<Map<String,Object>> reply = csService.selectReply(commandMap.getMap());
//		 
//		 mv.addObject("list", list.get(0));
//		 mv.addObject("reply", reply.get(0));
//		 
	    return mv;
	 }
	 
	 @ RequestMapping(value = "/updateQna2.do")
	 public ModelAndView updateQna2(CommandMap commandMap,String no, HttpServletRequest request, @RequestParam("mailfile") MultipartFile[] multipartFile)throws Exception {	    			
		 
		 //���� ���� DB �ݿ�
		 String deleteFiles = request.getParameter("arrDeleteFiles");
		 String[] arrDeleteFiles = deleteFiles.split(",");		 		
		 
		 for(String fileNo : arrDeleteFiles)
		 {
			 Map<String, Object > fileMap = new HashMap<String, Object>();
			 fileMap.put("no", fileNo);
			 
			 csService.deleteFile(fileMap);			 
		 }
		 
		 //�߰� ���� DB �ݿ�
		 if(multipartFile != null && no != null)
		 {						 	
			 fileUpload(multipartFile, no, null);
		 }
			 
		 ModelAndView mv = new ModelAndView("redirect://boardView.do?code=" + commandMap.get("code") + "&no=" + no);

        String title = ((String)commandMap.get("title")).replace("[�亯�Ϸ�]", "");
        commandMap.put("title",title);

        Iterator<String> keys = commandMap.keySet().iterator();
        while( keys.hasNext() ){
            String key = keys.next();
            
            if(key.contains("mailfile"))
            	continue;
            
            String value = String.valueOf(commandMap.get(key));
            System.out.println("Ű : "+key+", �� : "+value);
        }
		 csService.updateQna(commandMap.getMap());
		 
	    return mv;
	 }
	 
	 @ RequestMapping(value = "/updateQna.do")
	 @ResponseBody
	 public String updateQna(CommandMap commandMap, HttpServletRequest request, @RequestParam("article_file") MultipartFile[] multipartFile)throws Exception {	    			
		 
		 String strResult = "{ \"result\":\"FAIL\" }";
		 
		 try {
			 //���� ���� DB �ݿ�
			 String deleteFiles = request.getParameter("arrDeleteFiles");
			 String[] arrDeleteFiles = deleteFiles.split(",");		 		
			 
			 for(String fileNo : arrDeleteFiles)
			 {
				 Map<String, Object > fileMap = new HashMap<String, Object>();
				 fileMap.put("no", fileNo);
				 
				 csService.deleteFile(fileMap);			 
			 }
			 
			 String QnaNo = (String)commandMap.get("no");
			 
			 //�߰� ���� DB �ݿ�
			 if(multipartFile != null && QnaNo != null)
			 {						 	
				 strResult = fileUpload(multipartFile, QnaNo, null);
			 }
				 
			 ModelAndView mv = new ModelAndView("redirect://boardView.do?code=" + commandMap.get("code") + "&no=" + QnaNo);
	
	        String title = ((String)commandMap.get("title")).replace("[�亯�Ϸ�]", "");
	        commandMap.put("title",title);
	
	        Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            
	            if(key.contains("mailfile"))
	            	continue;
	            
	            String value = String.valueOf(commandMap.get(key));
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
			 csService.updateQna(commandMap.getMap());
			 
		 }catch(Exception e){
				e.printStackTrace();
			}
		 
	    return strResult;
	 }
	 
	 @RequestMapping(value = "/updateNotice2.do")
	 public ModelAndView updateNotice2(CommandMap commandMap,String no, HttpServletRequest request, @RequestParam("mailfile") MultipartFile[] multipartFile, @RequestParam("arrAddedFiles") MultipartFile[] multipartFiles)throws Exception {	    			
		 
		//���� ���� DB �ݿ�
		 String deleteFiles = request.getParameter("arrDeleteFiles");
		 String[] arrDeleteFiles = deleteFiles.split(",");		 		
		 
		 for(String fileNo : arrDeleteFiles)
		 {
			 Map<String, Object > fileMap = new HashMap<String, Object>();
			 fileMap.put("no", fileNo);
			 
			 csService.deleteFile(fileMap);			 
		 }
		 
		 //�߰� ���� DB �ݿ�
		 if(multipartFile != null && no != null && multipartFiles != null)
		 {						 	
			 fileUpload(multipartFile, no, null);
		 }
			 
		 
		 ModelAndView mv = new ModelAndView("/community/notice_view");
		 //CommandMap commandMap = new CommandMap();		
		 
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            
	            if(key.contains("mailfile"))
	            	continue;
	            
	            String value = String.valueOf(commandMap.get(key));
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
        
	     //commandMap.put("no", no);
		 csService.updateNotice(commandMap.getMap());
		 
		 Map<String, Object > Map = new HashMap<String, Object >();
			
		 Map.put("no", no);
		 Map.put("code", commandMap.get("code"));		 
		 List < Map < String,Object >> list = csService.selectBoardList(Map);	
		 List < Map < String,Object >> fileList = csService.selectPostFile(Map); //÷������ ��ȸ
		 mv.addObject("list", list.get(0));
		 mv.addObject("fileList", convertByteToSize(fileList));
	    return mv;
	 }
	 
	 @RequestMapping(value = "/updateNotice.do")
	 @ResponseBody
	 public String updateNotice(CommandMap commandMap, HttpServletRequest request, @RequestParam("article_file") MultipartFile[] multipartFile)throws Exception {	    			
		 
	 	String strResult = "{ \"result\":\"FAIL\" }";
		 
		 try {
		//���� ���� DB �ݿ�
		 String deleteFiles = request.getParameter("arrDeleteFiles");
		 String[] arrDeleteFiles = deleteFiles.split(",");		 		
		 
		 for(String fileNo : arrDeleteFiles)
		 {
			 Map<String, Object > fileMap = new HashMap<String, Object>();
			 fileMap.put("no", fileNo);
			 
			 csService.deleteFile(fileMap);			 
		 }
		 
		 String noticeNo = (String)commandMap.get("no");
		 
		 //�߰� ���� DB �ݿ�
		 if(multipartFile != null && noticeNo != null )
		 {						 	
			 strResult = fileUpload(multipartFile, noticeNo, null);
		 }			 
		 
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            
	            if(key.contains("mailfile"))
	            	continue;
	            
	            String value = String.valueOf(commandMap.get(key));
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
        
	     //commandMap.put("no", no);
		 csService.updateNotice(commandMap.getMap());		 
		 
		 }catch(Exception e){
				e.printStackTrace();
			}
		 
	    return strResult;
	 }
	 
	//ȸ������ ���� ������ 
	@RequestMapping(value = "/detailBoard", method = {RequestMethod.GET,RequestMethod.POST})
	public ModelAndView detailBoard(String code, String no, String sub)throws Exception {	    			
		 
		ModelAndView mv;
				
		if(sub != null && sub.contains("update"))
		{
			mv = new ModelAndView("/community/qna_secret");
			mv.addObject("no",no);
			mv.addObject("sub","qna_write");
			return mv;
		}
		
		 mv = new ModelAndView("/community/notice_write");
		 Map<String, Object > commandMap = new HashMap<String, Object >();
				
		 commandMap.put("no", no);
		 commandMap.put("code", code);		 
		 
		 List < Map < String,Object >> list = csService.selectBoardList(commandMap);	
		 List < Map < String,Object >> fileList = csService.selectPostFile(commandMap); //÷������ ��ȸ
		 
		 mv.addObject("fileList", convertByteToSize(fileList));
		 mv.addObject("notice", list.get(0));
		 mv.addObject("no", no);
		 
		 return mv;
	}
		
	 @ RequestMapping(value = "/deleteBoard.do")
	 public ModelAndView deleteBoard(CommandMap commandMap,String code)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("redirect://community.do?code="+code);		 	 
		 
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            String value = (String)commandMap.get(key);
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
		 
	      //��۰� �Բ� �Խñ� ����
		 csService.deleteBoard(commandMap.getMap());		 
		 commandMap.put("post_category", "qna_reply");
		 csService.deleteReply(commandMap.getMap());
		 commandMap.put("reply_yn", "n");
		 csService.updateRepliedQna(commandMap.getMap());
		 //�Խù��� �޸� ÷�����ϵ� �Բ� ����
		 Map<String, Object > fileMap = new HashMap<String, Object>();
		 fileMap.put("post_number", commandMap.getMap().get("no"));
		 
		 csService.deleteFile(fileMap);	
	    return mv;
	 }
	 
	 @ RequestMapping(value = "/deleteReply.do")
	 public ModelAndView deleteReply(CommandMap commandMap,String code)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("redirect://community.do?code="+code);		 	 
		 
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            String value = (String)commandMap.get(key);
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
	        
         commandMap.put("post_category", "qna_reply");
		 csService.deleteReply(commandMap.getMap());
		 
		 commandMap.put("reply_yn", "n");
		 csService.updateRepliedQna(commandMap.getMap());
		 
	    return mv;
	 }
	 	 
	 @ RequestMapping(value = "/insertRecruitBoard.do")
	 public ModelAndView insertRecruitBoard(CommandMap commandMap)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("redirect://recruit.do?sub=human");
		 commandMap.put("code", "recruit");		 
		 csService.insertBoard(commandMap.getMap());
		 
		 String content = (String)commandMap.get("content");
		 
		 CommonFunc commonfunc = new CommonFunc();
		 commonfunc.sendMailTest((String)commandMap.get("title"),content,"eunju.lee@jetspurt.com");
	    
	    return mv;
	 }
	 
	 @RequestMapping(value="/singleImageUploader.do")
		public String simpleImageUploader(
			HttpServletRequest req, CommandMap commandMap) 
	        	throws UnsupportedEncodingException{
		
		 Iterator<String> keys = commandMap.keySet().iterator();
	        while( keys.hasNext() ){
	            String key = keys.next();
	            String value = String.valueOf(commandMap.get(key));
	            System.out.println("Ű : "+key+", �� : "+value);
	        }
	        
//		String callback = smarteditorVO.getCallback();
//		String callback_func = smarteditorVO.getCallback_func();
//		String file_result = "";
//		String result = "";
//		MultipartFile multiFile = smarteditorVO.getFiledata();
//		try{
//			if(multiFile != null && multiFile.getSize() > 0 && 
//	        		StringUtils.isNotBlank(multiFile.getName())){
//				if(multiFile.getContentType().toLowerCase().startsWith("image/")){
//	            	String oriName = multiFile.getName();
//	                String uploadPath = req.getServletContext().getRealPath("/img");
//	                String path = uploadPath + "/smarteditor/";
//	                File file = new File(path);
//	                if(!file.exists()){
//	                file.mkdirs();
//	                }
//	                String fileName = UUID.randomUUID().toString();
//	                smarteditorVO.getFiledata().transferTo(new File(path + fileName));
//	                file_result += "&bNewLine=true&sFileName=" + oriName + 
//	                			   "&sFileURL=/img/smarteditor/" + fileName;
//				}else{
//					file_result += "&errstr=error";
//				}
//			}else{
//				file_result += "&errstr=error";
//			}
//		} catch (Exception e){
//			e.printStackTrace();
//		}
//		result = "redirect:" + callback + 
//				 "?callback_func=" + URLEncoder.encode(callback_func,"UTF-8") + file_result;
//		return result;
	        return "";
	}
	 	 
}

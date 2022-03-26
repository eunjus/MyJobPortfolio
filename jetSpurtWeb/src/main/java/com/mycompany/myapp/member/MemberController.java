package com.mycompany.myapp.member;

import java.io.PrintWriter;
import java.util.Collection;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;

import javax.annotation.Resource;
import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.mindrot.jbcrypt.BCrypt;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.mail.MailSender;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSenderImpl;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.mvc.support.RedirectAttributes;

import com.mycompany.myapp.common.CommandMap;
import com.mycompany.myapp.common.CommonFunc;


/**
 * Handles requests for the application home page.
 */
@Controller
public class MemberController {
	
	private static final Logger logger = LoggerFactory.getLogger(MemberController.class);
		
	
	@Resource(name="MemberService") 
	private MemberService memberService;
	
	@Autowired
	MailSender sender; //타입으로 받을 수 있음
	
	@RequestMapping(value = "/member.do", method = {RequestMethod.GET,RequestMethod.POST})
	public ModelAndView member(String code) {
		logger.info(code); 
		
		ModelAndView mv;
		
		try {
			System.out.println("----------TEST");
			
			String pageTitle ="index";
				
			if(code.contains("login"))
				pageTitle = "member/login";
			else if(code.contains("find_id"))	
				pageTitle = "member/find_id";
			else if(code.contains("find_pw"))	
				pageTitle = "member/find_pw";
			else if(code.contains("find_ok"))	
				pageTitle = "member/find_ok";
			else if(code.contains("join_agreement"))	
				pageTitle = "member/join_agreement";
			else if(code.contains("join_ok"))	
				pageTitle = "member/join_ok";
			else if(code.contains("join_reg"))	
				pageTitle = "member/join";
			else if(code.contains("logout"))	
				pageTitle = "member/logout";
			else if(code.contains("mypage"))	
				pageTitle = "member/mypage";
			else if(code.contains("change_pw"))	
				pageTitle = "member/change_pw";
			else if(code.contains("change_ok"))	
				pageTitle = "member/change_ok";
			else if(code.contains("withdrawal"))	
				pageTitle = "member/withdrawal";
			else if(code.contains("agreement"))	
				pageTitle = "member/agreement";
			else if(code.contains("usepolicy"))	
				pageTitle = "member/usepolicy";
			
			mv = new ModelAndView("/"+pageTitle);
			return mv;
		
		}
		catch(Exception e)
		{
			e.printStackTrace();
			mv = new ModelAndView("/index");
			return mv;
		}
	}
		
	//회원정보 수정 페이지 진입
	@RequestMapping(value = "/mypage", method = {RequestMethod.GET,RequestMethod.POST})
	public ModelAndView showMypage(HttpServletRequest request)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("/member/mypage");
		 CommandMap commandMap = new CommandMap();
				
		 //로그인 세션 정보 가져오기
		 HttpSession session = request.getSession();
		 commandMap.put("userid", session.getAttribute("userid"));
		 //commandMap.put("userid", "wewe");

		 List < Map < String,Object >> list = memberService.selectUserId(commandMap.getMap());
		 
		 list.get(0).put("birth_dt", list.get(0).get("birth_dt").toString().replace("-", ""));
		 list.get(0).put("cel_number", "010-" + list.get(0).get("cel_number").toString().substring(2,6) + "-" + list.get(0).get("cel_number").toString().substring(6));
		 list.get(0).put("email_id", list.get(0).get("user_email").toString().split("@")[0]);
		 list.get(0).put("email_domain", list.get(0).get("user_email").toString().split("@")[1]);
		 mv.addObject("userInfo", list.get(0));
		 
	   return mv;
	}
	
	@RequestMapping(value = "/updateUserInfo.do", method = {RequestMethod.GET,RequestMethod.POST})
	public ModelAndView updateUserInfo(HttpServletRequest request, CommandMap commandMap)throws Exception {	    			
		 
		 
		 //로그인 세션 정보 가져오기
		 HttpSession session = request.getSession();
		 commandMap.put("userid", session.getAttribute("userid"));
		 		 
		 //commandMap.put("userid", "wewe");			 

		 memberService.updateUser(commandMap.getMap());
		 
		 ModelAndView mv = new ModelAndView("/member/mypage");
	 	 List < Map < String,Object >> list = memberService.selectUserId(commandMap.getMap());
		 
		 list.get(0).put("birth_dt", list.get(0).get("birth_dt").toString().replace("-", ""));
		 list.get(0).put("cel_number", "010-" + list.get(0).get("cel_number").toString().substring(2,6) + "-" + list.get(0).get("cel_number").toString().substring(6));
		 list.get(0).put("email_id", list.get(0).get("user_email").toString().split("@")[0]);
		 list.get(0).put("email_domain", list.get(0).get("user_email").toString().split("@")[1]);
		 mv.addObject("userInfo", list.get(0));
		 
		 return mv;
	}
	
	@RequestMapping(value = "/changeUserPW.do", method = {RequestMethod.GET,RequestMethod.POST})
	public ModelAndView changeUserPW(HttpServletRequest request,HttpServletResponse response, CommandMap commandMap)throws Exception {	    					 
		
		ModelAndView mv;
		
		CommandMap Map = new CommandMap();	 
		//Map.put("userid", "eunjulee");

		//로그인 세션 정보 가져오기
		HttpSession session = request.getSession();			
		Map.put("userid", session.getAttribute("userid"));
					
		List < Map < String,Object >> userlist = memberService.selectUserId(Map.getMap());
		
		if(!BCrypt.checkpw((String)commandMap.get("old_password"), (String)userlist.get(0).get("passwd")))
		 {		
			 mv = new ModelAndView("/member/change_pw");
			
	 		 response.setContentType("text/html; charset=UTF-8");
			 PrintWriter out = response.getWriter();
			 out.println("<script>alert('비밀번호가 일치하지 않습니다.'); location.href='/myapp/member.do?code=change_pw';</script>");
			 out.flush();
			 			 		 		
		}	
		else //비번 변경
		{			
			mv = new ModelAndView("/member/change_ok");
			
			Map.put("password", BCrypt.hashpw((String)commandMap.get("password"), BCrypt.gensalt()));
			memberService.updateUserPW(Map.getMap());
			
		}	
		
		return mv;
	}
	
	@RequestMapping(value = "/loginfdsdfsdf.do", method = {RequestMethod.GET,RequestMethod.POST})
	 public ModelAndView login(HttpServletResponse response,HttpSession session,RedirectAttributes redirectAttr, CommandMap commandMap)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("/index");
		 
		 CommandMap userMap = new CommandMap();
		 userMap.put("userid", commandMap.get("userid"));
		 		 
		 List < Map < String,Object >> userlist = memberService.selectUserId(userMap.getMap());
		 String temp = (String)userlist.get(0).get("passwd");
		 
		 try {
			 if(userlist.size() > 0)
			 {
				 if(!BCrypt.checkpw((String)commandMap.get("password"), (String)userlist.get(0).get("passwd")))
				 {
				 	response.setContentType("text/html; charset=UTF-8");
					 PrintWriter out = response.getWriter();
					 out.println("<script>alert('비밀번호가 일치하지 않습니다.'); location.href='/myapp/member.do?code=login';</script>");
					 out.flush();
				 }
			 }
			  else if(userlist.size() == 0)
			    {
				  	
				  	 response.setContentType("text/html; charset=UTF-8");
					 PrintWriter out = response.getWriter();
					 out.println("<script>alert('존재하지 않는 아이디 입니다.'); location.href='/myapp/member.do?code=login';</script>");
					 out.flush();
					 
					 /*
					 redirectAttr.addFlashAttribute("errorMessage","암호가 틀렸습니다.");
					 return "redirect:/member.do?code=login";
					 */
			    }
			  else
			  {
				  	session.setAttribute("isAdmin", "true");		  
			  }
		 }
		 catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		 return mv;
	 }
	
	
	@ResponseBody
	@RequestMapping(value = "member/userid_duplicate_check.do")
	 public Map<String, Object> userid_duplicate_check(@RequestParam (value="userid") String param) throws Exception {		
					
		Map<String, Object > commandMap = new HashMap<String, Object>();
		commandMap.put("userid", param);	
		
		Map<String,Object> map = new HashMap<String, Object>();	
	    
	    List < Map < String,Object >> list = memberService.selectUserId(commandMap);
	    
	    if(list.size() > 0)
	    {
	    	map.put("code", true);	
	    	map.put("use", false);
	    	map.put("msg", "이미 등록된 아이디입니다.");
	    }
	    else
	    {
	    	map.put("code", true);	    	
	    	map.put("use", true);
	    	map.put("msg", "사용가능한 아이디입니다.");
	    }
	    
	    return map;
	 }
	
	@ResponseBody
	@RequestMapping(value = "member/email_duplicate_check.do")
	 public Map<String, Object> email_duplicate_check(@RequestParam (value="userid") String Userid,
			 										@RequestParam (value="email") String Email)throws Exception {		
					
		Map<String, Object > commandMap = new HashMap<String, Object>();	
		commandMap.put("email", Email);
		
		Map<String,Object> map = new HashMap<String, Object>();	
	    
	    List < Map < String,Object >> list = memberService.selectUserId(commandMap);
	    
	    if(list.size() > 0)
	    {
	    	map.put("code", true);
	    	map.put("use", false);
	    	map.put("msg", "이미 등록된 이메일입니다.");
	    }
	    else
	    {
	    	map.put("code", true);
	    	map.put("use", true);
	    	map.put("msg", "사용가능한 이메일입니다.");
	    }
	    
	    return map;
	 }

	
	 @RequestMapping(value = "/insertNewUser.do", method = {RequestMethod.GET,RequestMethod.POST})
	 public ModelAndView insertBoard(CommandMap commandMap)throws Exception {	    			
		 		 
		 ModelAndView mv = new ModelAndView("redirect://member.do?code=join_ok");
		 //비밀번호 암호화	 
		 String hashedPw = BCrypt.hashpw((String)commandMap.get("password"), BCrypt.gensalt());
		 commandMap.put("hashedpw", hashedPw);
		 memberService.insertNewUser(commandMap.getMap());

	    return mv;
	 }

	 @RequestMapping(value = "/find_id.do", method = {RequestMethod.GET,RequestMethod.POST})
	 public ModelAndView funcfind_id(HttpServletResponse response, CommandMap commandMap)throws Exception {	    			
		 
		 ModelAndView mv = new ModelAndView("/member/find_ok");
		 Map<String,Object> map = new HashMap<String, Object>();	
		 
		 try {
			 List < Map < String,Object >> list = memberService.selectUserId(commandMap.getMap());
			 
			 if(list.size() > 0)
			    {
				 	map.put("code", true);	
				 	map.put("title", "아이디·비밀번호 찾기 완료");
				 	map.put("msg", "회원님의 아이디 찾기가 완료되었습니다.");
				 	map.put("list", "회원님의 아이디는 '"+ list.get(0).get("user_id") + "' 입니다.");		    			    			    	
			    }
			    else
			    {
			    	response.setContentType("text/html; charset=UTF-8");
					 PrintWriter out = response.getWriter();
					 out.println("<script>alert('회원정보를 찾을 수 없습니다.'); location.href='/myapp/member.do?code=find_id';</script>");
					 out.flush();
					 
//			    	map.put("code", false);		
//			    	map.put("title", "아이디·비밀번호 찾기 실패");		    	
//			    	map.put("msg", "회원정보를 찾을 수 없습니다.");
//			    	map.put("list", "회원정보를 찾을 수 없습니다.");
			    }
			 }
		 catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} 	
		    mv.addObject("list",map);
		    //mv.setViewName("redirect://member.do?code=find_ok");		    		  
			 
	    return mv;
	 }

	 @RequestMapping(value = "/find_pw.do", method = {RequestMethod.GET,RequestMethod.POST})
	 public ModelAndView funcfind_pw(CommandMap commandMap)throws Exception {	    			
		 		 
		 ModelAndView mv = new ModelAndView("/member/find_ok");
		 //commandMap.put("code", "qna");		 
		 List < Map < String,Object >> list = memberService.selectUserId(commandMap.getMap());
		 
		 Map<String,Object> map = new HashMap<String, Object>();	
		 
		 if(list.size() > 0)
		    {
			 	//임시 비밀번호 발급
			 	String tempPW = randomPassword(10);
			 	
			 	String msg = "";
				   	msg += "<div align='center' style='border:1px solid black; font-family:verdana'>";
					msg += "<h3 style='color: blue;'>";
					msg += list.get(0).get("user_id") + "님의 임시 비밀번호 입니다. 비밀번호를 변경하여 사용하세요.</h3>";
					msg += "<p>임시 비밀번호 : ";
					msg += tempPW + "</p></div>";
					
			 	//임시 비밀번호 저장 	 
				 String hashedPw = BCrypt.hashpw(tempPW, BCrypt.gensalt());
				 commandMap.put("password", hashedPw);				 
				 memberService.updateUserPW(commandMap.getMap());
			
				 CommonFunc commonfunc = new CommonFunc();
				 commonfunc.sendMailTest("비밀번호 찾기 메일 발송",msg,(String)commandMap.get("email"));
			 	
				 //메일 보내기
			 	//sendMailTest();
			 	
			 	map.put("code", true);	
			 	map.put("title", "아이디·비밀번호 찾기 완료");
			 	map.put("msg", "회원님의 비밀번호 찾기가 완료되었습니다.");
			 	map.put("list", "가입 시 기입하신 이메일 " + list.get(0).get("user_email") + "'로 회원님의 비밀번호를 발송하였습니다.");		    			    			    	
		    }
		    else
		    {
		    	map.put("code", false);		
		    	map.put("title", "아이디·비밀번호 찾기 실패");		    	
		    	map.put("msg", "회원정보를 찾을 수 없습니다.");
		    	map.put("list", "회원정보를 찾을 수 없습니다.");
		    }
		 
		    mv.addObject("list",map);
		    
	    return mv;
	 }
	 
	 @RequestMapping(value = "/withdrawalUser.do", method = {RequestMethod.GET,RequestMethod.POST})
		public ModelAndView withdrawalUser(HttpServletRequest request,HttpServletResponse response, CommandMap commandMap)throws Exception {	    					 
			
			ModelAndView mv;
			
			CommandMap Map = new CommandMap();	 
			//Map.put("userid", "eunjulee");

			//로그인 세션 정보 가져오기
			HttpSession httpSession = request.getSession();
			String userID = (String)httpSession.getAttribute("userid");
			
			Map.put("userid", userID);
						
			List < Map < String,Object >> userlist = memberService.selectUserId(Map.getMap());
			
			if(!BCrypt.checkpw((String)commandMap.get("password"), (String)userlist.get(0).get("passwd")))
			 {		
				 mv = new ModelAndView("/member/withdrawal");
				
		 		 response.setContentType("text/html; charset=UTF-8");
				 PrintWriter out = response.getWriter();
				 out.println("<script>alert('비밀번호가 일치하지 않습니다.'); location.href='/myapp/member.do?code=withdrawal';</script>");
				 out.flush();
				 			 		 		
			}	
			else //회원 탈퇴
			{			
		
				memberService.withdrawalUser(Map.getMap());
				
				if (userID != null) {
			        
			        httpSession.removeAttribute("userid");	        
					httpSession.removeAttribute("password");
					httpSession.removeAttribute("authority");
			        httpSession.invalidate();
			        	       
			    }
				
				mv = new ModelAndView("/index");
				
				response.setContentType("text/html; charset=UTF-8");
				 PrintWriter out = response.getWriter();
				 out.println("<script>alert('회원탈퇴가 처리되었습니다.'); location.href='/myapp/';</script>");
				 out.flush();
			}	
			
			return mv;
		}
	 
	 
	public static String randomPassword(int length){

		int index = 0;
		char[] charset = new char[] {
		'0','1','2','3','4','5','6','7','8','9'
		,'A','B','C','D','E','F','G','H','I','J','K','L','M'
		,'N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
		,'a','b','c','d','e','f','g','h','i','j','k','l','m'
		,'n','o','p','q','r','s','t','u','v','w','x','y','z'
		};

		StringBuffer sb = new StringBuffer();

		for(int i = 0 ; i<length ; i++){
			index = (int) (charset.length * Math.random());
			sb.append(charset[index]);
		}

		return sb.toString();
		}
			
	 	
}

package com.mycompany.myapp.user;

import java.io.PrintWriter;
import java.util.List;
import java.util.Map;

import javax.inject.Inject;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.mindrot.jbcrypt.BCrypt;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.util.WebUtils;

import com.mycompany.myapp.common.CommandMap;
import com.mycompany.myapp.member.MemberService;

@Controller
public class UserLoginController{
	
	private final MemberService memberService;
	
	@Inject
	public UserLoginController(MemberService memberService) {
		this.memberService = memberService;
	}
	
	//로그인 페이지
	@RequestMapping(value="/login", method = RequestMethod.GET)
	public String loginGet(@ModelAttribute("commandMap") CommandMap commandMap) {
		return "/user/login";		
	}
	
	//로그인 처리
	@RequestMapping(value="/loginPost.do", method=RequestMethod.POST)	
	public ModelAndView loginPOST(HttpServletRequest request, HttpServletResponse response,CommandMap commandMap, Model model) throws Exception{
		
		HttpSession session = request.getSession();
				
		CommandMap userMap = new CommandMap();
		userMap.put("userid", commandMap.get("userid"));		 		 	
		List < Map < String,Object >> userlist = memberService.selectUserId(userMap.getMap());
		
		ModelAndView mv = new ModelAndView("/index");
		 	
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
				 else
				 {
					 /* session.setAttribute("login", userlist.get(0));
					 session.setAttribute("userid",userlist.get(0).get("user_id"));
					 session.setAttribute("password",userlist.get(0).get("passwd"));
					 session.setAttribute("authority",userlist.get(0).get("user_authority"));
					 */
					 
					 //model.addAttribute("user",userlist.get(0));		  
					 model.addAttribute("userid",userlist.get(0).get("user_id"));
					 model.addAttribute("password",userlist.get(0).get("passwd"));
					 model.addAttribute("authority",userlist.get(0).get("user_authority"));
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
		 }
		 catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		 return mv;
	}
	//회원정보 수정 페이지 진입
	@RequestMapping(value = "/logout", method = {RequestMethod.GET,RequestMethod.POST})
	public ModelAndView logout(HttpServletRequest request,HttpServletResponse response, HttpSession httpSession) throws Exception {
					 
		 ModelAndView mv = new ModelAndView("/index");

		 String userID = (String)httpSession.getAttribute("userid");
		 
	    if (userID != null) {
	        
	        httpSession.removeAttribute("userid");	        
			httpSession.removeAttribute("password");
			httpSession.removeAttribute("authority");
	        httpSession.invalidate();
	        	       
	    }
	    
	    return mv;
	    		
	}
}

package com.mycompany.myapp.interceptor;

import java.util.Enumeration;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ui.ModelMap;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;

public class LoginInterceptor extends HandlerInterceptorAdapter{
	
	private static final String LOGIN = "userid";
	private static final Logger logger = LoggerFactory.getLogger(LoginInterceptor.class);
	
	@Autowired
	private HttpSession httpSession;
	
	@Override
	public void postHandle(HttpServletRequest request, HttpServletResponse response, Object handler, ModelAndView modelAndView) throws Exception{
		
		httpSession = request.getSession();
		ModelMap modelMap = modelAndView.getModelMap();
		//Object userVO = modelMap.get("user");
		String userID = (String)modelMap.get("userid");
		String userPW = (String)modelMap.get("password");
		String userAuth = (String)modelMap.get("authority");
		
		if(userID != null ) {
			logger.info("new login success");
			
			//httpSession.setAttribute(LOGIN, userVO);
			httpSession.setAttribute(LOGIN, userID);
			httpSession.setAttribute("password", userPW);
			httpSession.setAttribute("authority", userAuth);
			
			response.sendRedirect("member.do?code=index");
									
		}		
	}
	 
	@Override
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception{
		
		HttpSession httpSession = request.getSession();
		//기존의 로그인 정보 제거
		if(httpSession.getAttribute(LOGIN) != null) {
			logger.info("clear login data before");			
			httpSession.removeAttribute(LOGIN);
			httpSession.removeAttribute("password");
			httpSession.removeAttribute("authority");
		}
		
		return true;
	}
	
}

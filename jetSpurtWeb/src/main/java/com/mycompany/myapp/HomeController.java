package com.mycompany.myapp;

import java.text.DateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import com.mycompany.myapp.common.CommandMap;
import com.mycompany.myapp.cs.CSService;

/**
 * Handles requests for the application home page.
 */
@Controller
public class HomeController {
	
	private static final Logger logger = LoggerFactory.getLogger(HomeController.class);
		
	
	@Resource(name="CSService") 
	private CSService csService;
	/**
	 * Simply selects the home view to render by returning its name.
	 */
	@RequestMapping(value = "/", method = RequestMethod.GET)
	public ModelAndView home(Locale locale, Model model) throws Exception{
		
		logger.info("Welcome home! The client locale is {}.", locale);

		//공지사항 조회
		Map<String, Object > commandMap = new HashMap<String, Object>();
		commandMap.put("code", "qna");	
		
	    ModelAndView mv = new ModelAndView("/index" );
	    
	    List < Map < String,Object >> list = csService.indexBoardList();
	    mv.addObject("BoardList", list);
	    return mv;

	}
	
	@RequestMapping(value = "/about.do", method = RequestMethod.GET)
	public String about(String sub) {
		logger.info(sub);
		
		try {
			System.out.println("----------TEST");
			
			String pageTitle ="index";
				
			if(sub.contains("about"))
				pageTitle = "introduction/about";
			else if(sub.contains("history"))	
				pageTitle = "introduction/history";
			else if(sub.contains("chart"))	
				pageTitle = "introduction/chart";
			else if(sub.contains("introduce"))	
				pageTitle = "introduction/introduce";
			else if(sub.contains("location"))	
				pageTitle = "introduction/location";
			
			return pageTitle;
		
		}
		catch(Exception e)
		{
			e.printStackTrace();
			return "index";
		}
	}
	
	@RequestMapping(value = "/recruit.do", method = RequestMethod.GET)
	public String recruit(String sub) {
		logger.info(sub);
		
		try {
			System.out.println("----------TEST");
			
			String pageTitle ="index";
				
			if(sub.contains("human"))
				pageTitle = "recruit/human";
			else if(sub.contains("benefit"))	
				pageTitle = "recruit/benefit";
			else if(sub.contains("process"))	
				pageTitle = "recruit/process";
			else if(sub.contains("recruit_write"))	
				pageTitle = "recruit/recruit_write";
			
			return pageTitle;
		
		}
		catch(Exception e)
		{
			e.printStackTrace();
			return "index";
		}
	}
	
	
	 @ RequestMapping(value = "/sample/testMapArgumentResolver.do")
	 public ModelAndView testMapArgumentResolver(CommandMap commandMap)throws Exception {
	    ModelAndView mv = new ModelAndView("");
	    
		if (commandMap.isEmpty() == false) {
	        
			Iterator < Entry < String,Object >> iterator = commandMap.getMap().entrySet().iterator();
	        Entry < String,Object > entry = null;
	        while (iterator.hasNext()) {
	            entry = iterator.next();
	            logger.debug("key : " + entry.getKey() + ", value : " + entry.getValue());
	        }
	    }
	    return mv;
	}
	 
	 @RequestMapping(value="/sample/openBoardWrite.do") 
	 public ModelAndView openBoardWrite(CommandMap commandMap) throws Exception{ 
		 
		 ModelAndView mv = new ModelAndView("boardWrite"); 
		 
		 return mv; 
	 }

	 

}

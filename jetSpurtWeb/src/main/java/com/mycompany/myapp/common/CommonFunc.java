package com.mycompany.myapp.common;

import java.util.Properties;

import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;

public class CommonFunc {
    
	
	
	public void sendMailTest(String title, String content, String ToEmail)
	{	
		String toEmail = ToEmail; // �޸�(,)�� ������ ����
		
		//���� jetspurt ���Ϸ� ����
		final String username = "help@jetspurt.com";         
		final String password = "jetspurt0712!";
				
		// ���� �ɼ� ����
		Properties prop = new Properties(); 
		prop.put("mail.smtp.host", "mail.jetspurt.com"); 
		//prop.put("mail.smtp.host", "smtp.naver.com"); 
		//prop.put("mail.smtp.port", 587); //TSL
		prop.put("mail.smtp.port", 465);  //SSL
		prop.put("mail.smtp.auth", "true"); 
		//prop.put("mail.smtp.ssl.enable", "true"); 
		//prop.put("mail.smtp.starttls.enable", "true");
		//prop.put("mail.smtp.ssl.trust", "smtp.naver.com");
		//prop.put("mail.smtp.ssl.protocols", "TLSv1.2");
		
		
		
		try {
			// ���� ����  ���� ���� ����
			Session session = Session.getDefaultInstance(prop, new javax.mail.Authenticator() {
	            protected PasswordAuthentication getPasswordAuthentication() {
	                return new PasswordAuthentication(username, password);
	            }
	        });
		
	        MimeMessage message = new MimeMessage(session);
	        message.setFrom(new InternetAddress(username));
	
	        //�����ڸ����ּ�
	        message.addRecipient(Message.RecipientType.TO, new InternetAddress(toEmail)); 
	
	        message.setSubject(title, "UTF-8");
	        message.setText(content, "UTF-8");
	        message.setHeader("content-Type", "text/html");
	
	        // send the message
	        Transport.send(message); ////����
	        System.out.println("message sent successfully...");
	    } catch (AddressException e) {
	        // TODO Auto-generated catch block
	        e.printStackTrace();
	    } catch (MessagingException e) {
	        // TODO Auto-generated catch block
	        e.printStackTrace();
		} catch ( Exception e ) {
		  e.printStackTrace();
		}
	
	
	}
}
	

<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %>
<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html lang="ko">
<head>
<title>젯스퍼트</title>
<link rel="shortcut icon" href="/upload/conf/favicon1.ico" type="image/x-icon" />
<link rel="icon" href="/upload/conf/favicon1.ico" type="image/x-icon" />

<!--jQuery UI CSS파일-->
<link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" />

<meta charset="UTF-8">
<meta name="title" content="젯스퍼트" />
<meta name="keywords" content="젯스퍼트">
<meta name="description" content="젯스퍼트">
<meta name="author" content="젯스퍼트">
<meta name="viewport" content="width=device-width, user-scalable=no">
<meta name="format-detection" content="telephone=no" /><!--애플전화번호링크자동설정헤제-->

<link rel="stylesheet" type="text/css" href="resources/lib/css/common.css" /><!-- 서브 공통 css ｜ custom 금지 css -->
<link rel="stylesheet" type="text/css" href="resources/css/reset.css" />
<link rel="stylesheet" type="text/css" href="resources/css/jquery.fullPage.css" />
<link rel="stylesheet" type="text/css" href="resources/css/common.css" /><!-- 서브 공통 css ｜ custom 금지 css -->
<link rel="stylesheet" type="text/css" href="resources/css/sub.css" /><!-- 서브 공통 custom 요소 css ｜ 개별페이지 css -->
<link rel="stylesheet" type="text/css" href="resources/css/skin.css" /><!-- 스킨 css ｜ 상하단·메인 -->
<!--<link rel="stylesheet" type="text/css" href="resources/css/slick.css" /> 슬라이드 기능 -->
<link rel="stylesheet" type="text/css" href="//kenwheeler.github.io/slick/slick/slick.css">

<!--<script src="//ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>-->
<!-- <script src="resources/js/jquery.min.js"></script>-->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<!-- <script src="/lib/js/jquery-2.2.4.min.js"></script> -->
<script src="resources/js/jquery.validate.min.js"></script>
<script src="resources/js/common.js?ver=1"></script>
<script src="resources/js/jquery-ui.min.js"></script>
<script src="resources/js/moment-with-locales.min.js"></script>
<script src="//dmaps.daum.net/map_js_init/postcode.v2.js"></script>
<script src="resources/js/Html5FileUpload.js"></script>
<!-- <script type="text/javascript" src="/data/skin/default/js/jquery.bxslider.min.js"></script> -->

<!--#script src="//cdn.jsdelivr.net/bxslider/4.2.12/jquery.bxslider.min.js"></script-->
<script src="//kenwheeler.github.io/slick/slick/slick.js"></script>
<!--#script src="/data/skin/default/js/ui_common.js"></script-->
<script type="text/javascript" src="resources/js/common_board.js"></script>
<script type="text/javascript" src="resources/js/common_form.js"></script>
<script src="resources/js/jquery.mCustomScrollbar.concat.min.js"></script>
<script type="text/javascript">
	var CURRENT_DATE = "2021-06-10 10:00:43";
	
	$(function() {
		$.validator.setDefaults({
			onkeyup: false,
			onclick: false,
			onfocusout: false,
			ignore : '.ignore',
			showErrors: function(errorMap, errorList) {
				if(errorList.length < 1) {
					return;
				}
				alert(errorList[0].message);
				errorList[0].element.focus();
			}
		});
		
			
		
	});
</script>

<style>	
	.btn_right {position:absolute; bottom:25px; right:0;}
</style>

</head>
<body>
<div class="skip_nav">
	<a href="#header" class="skip_nav_link">상단메뉴 바로가기</a>
	<a href="#content" class="skip_nav_link">본문 바로가기</a>
	<a href="#sub_nav" class="skip_nav_link">본문 하위메뉴 바로가기</a>
	<a href="#footer" class="skip_nav_link">하단 바로가기</a>
</div><!-- 웹접근성용 바로가기 링크 모음 -->
<article id="wrap" class="clear">
<div class="header">
	<div class="hd_content clear">
		<h1 class="hd_logo"><a href="${pageContext.request.contextPath}/"><img src="resources/images/s-images/젯스퍼트_로고.png" alt="㈜젯스퍼트"></a></h1>
		<ul class="hd_lnb">
			<li class="lnb_1 has_child">
				<a class="dep1_a" href="${pageContext.request.contextPath}/about.do?sub=about">회사소개</a>
				<ul class="dep2 dn">
						<li><a href="${pageContext.request.contextPath}/about.do?sub=about" class="dep2_a">인사말</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=history" class="dep2_a">연혁</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=chart" class="dep2_a">조직도</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=introduce" class="dep2_a">사업소개</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=location" class="dep2_a">오시는 길</a></li>
	
				</ul>
			</li>
			<li class="lnb_2 has_child">
				<a class="dep1_a" href="${pageContext.request.contextPath}/community.do?code=notice">커뮤니티</a>
				<ul class="dep2 dn">
						<li><a href="${pageContext.request.contextPath}/community.do?code=notice" class="dep2_a">공지사항</a></li>
						<li><a href="${pageContext.request.contextPath}/community.do?code=qna" class="dep2_a">Q&A</a></li>
	
				</ul>
			</li>
			<li class="lnb_3 ">
				<a class="dep1_a" href="${pageContext.request.contextPath}/recruit.do?sub=human">인재채용</a>
				<ul class="dep2 dn">	
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=human" class="dep2_a">인재상/인재육성</a></li>
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=benefit" class="dep2_a">복지</a></li>
						
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=process" class="dep2_a">채용절차</a></li>
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=recruit_write" class="dep2_a">채용문의</a></li>
				</ul>
			</li>
		</ul><!-- hd_lnb 상단 메뉴 -->
		<div class="side_btm">
				<div class="side_sns">					
				</div>
			</div>
		<div class="btn_mMenu"><a class="menu-trigger" href="#"><span></span><span></span><span></span></a></div>
		<div class="aside_bg dn"></div>
	</div>
</div>

<aside id="aside">
	<div class="aside_box clear">
		<div class="aside_lnb">
			<div class="mo_menuTitle clear">
				<div class="btn_mMenuClose"></div>
			</div>
			<ul class="slidemenu">
				<li><a href="${pageContext.request.contextPath}/about.do?sub=about"><span>인사말</span><div class="menu_arr">&nbsp;</div></a>
					<ul>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=history">연혁</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=chart">조직도</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=introduce">사업소개</a></li>
						<li><a href="${pageContext.request.contextPath}/about.do?sub=location">오시는 길</a></li>
					</ul>
				</li>
				<li><a href="${pageContext.request.contextPath}/community.do?code=notice"><span>게시판</span><div class="menu_arr">&nbsp;</div></a>
					<ul>
						<li><a href="${pageContext.request.contextPath}/community.do?code=notice">공지사항</a></li>
						<li><a href="${pageContext.request.contextPath}/community.do?code=qna">Q&A</a></li>
					</ul>
				</li>
				<li><a href="${pageContext.request.contextPath}/recruit.do?sub=human"><span>채용</span><div class="menu_arr">&nbsp;</div></a>
					<ul>
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=human">인재상/인재육성</a></li>
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=benefit">복지</a></li>
						
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=process">채용절차</a></li>
						<li><a href="${pageContext.request.contextPath}/recruit.do?sub=recruit_write">채용문의</a></li>						
					</ul>
				</li>			
				<c:if test="${sessionScope.userid eq null}">	
					<li><a href="${pageContext.request.contextPath}/member.do?code=login"><span>회원관리</span><div class="menu_arr">&nbsp;</div></a>
						<ul>
							<li><a href="${pageContext.request.contextPath}/member.do?code=login">로그인</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=find_id">아이디 찾기</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=find_pw">비밀번호 찾기</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=join_agreement">회원가입</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=agreement">이용약관</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=usepolicy">개인정보처리방침</a></li>
						</ul>
					</li>
				 </c:if>
				 <c:if test="${sessionScope.userid ne null}">
				 	<li><a href="${pageContext.request.contextPath}/member.do?code=login"><span>회원관리</span><div class="menu_arr">&nbsp;</div></a>
						<ul>
							<li><a href="${pageContext.request.contextPath}/logout">로그아웃</a></li>
							<li><a href="${pageContext.request.contextPath}/mypage">회원정보수정</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=change_pw">비밀번호 변경</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=withdrawal">회원탈퇴</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=agreement">이용약관</a></li>
							<li><a href="${pageContext.request.contextPath}/member.do?code=usepolicy">개인정보처리방침</a></li>
						</ul>
					</li>
				 
			  	</c:if>
			</ul><!--slidemenu-->
			<div class="side_btm">
				<div class="side_sns">
				</div>
			</div>
		</div>
	</div>
</aside><!-- #aside 상단영역 -->


<script>
$(document).ready(function(){
	jQuery("#lnb_nav").on("mouseover",function(){
		jQuery(this).parents(".hd_btm").find(".lnb_dep2Wrap").addClass("open")
	});
	jQuery(".hd_btm").on("mouseleave",function(){
		jQuery(this).find(".lnb_dep2Wrap").removeClass("open") 
	});
	jQuery("#lnb_nav li").on("mouseenter",function(){
		jQuery(this).find("a").addClass("hov")
	});
	jQuery("#lnb_nav li").on("mouseleave",function(){
		jQuery(this).find("a").removeClass("hov")
	});
	jQuery(".lnb_dep2Wrap > ul > li").on("mouseenter",function(){
		var idx = jQuery(this).index();
		jQuery("#lnb_nav li a").removeClass("hov")
		jQuery("#lnb_nav li").eq(idx).find("a").addClass("hov")
	});
	jQuery(".lnb_dep2Wrap > ul > li").on("mouseleave",function(){
		jQuery("#lnb_nav li a").removeClass("hov")
	});


	var box = $('#aside'),
		  bg = $('.aside_bg'),
		  itval;
	//카테고리 여닫기
	var burger = $('.menu-trigger');
	var burger2 = $('.menu-trigger2');
	$('.menu-trigger, .menu-trigger2').each(function(index){
		var $this = $(this);		
		$this.on('click', function(e){
			e.preventDefault();
			$('.menu-trigger').toggleClass('active-1');
			if (box.hasClass('on')) {
				box.removeClass('on');
				bg.removeClass('on');
				$('body').css({'overflow':'inherit','height':'auto'});
				clearTimeout(itval);
				itval = setTimeout(function () {
					bg.addClass('dn');
				}, 800);
			} else {
				box.addClass('on');
				bg.removeClass('dn');
				$('body').css({'overflow':'hidden','height':'100%'});
				clearTimeout(itval);
				itval = setTimeout(function () {
					bg.addClass('on');
				}, 20);
			}
		})
		$('.btn_mMenuClose').click(function(){
			$('.menu-trigger').removeClass('active-1');
			box.removeClass('on');
			bg.removeClass('on');
			$('body').css({'overflow':'inherit','height':'auto'});
			clearTimeout(itval);
			itval = setTimeout(function () {
				bg.addClass('dn');
			}, 800);
		});
		$('.aside_bg').click(function(){
			$('.menu-trigger').removeClass('active-1');
			box.removeClass('on');
			bg.removeClass('on');
			$('body').css({'overflow':'inherit','height':'auto'});
			clearTimeout(itval);
			itval = setTimeout(function () {
				bg.addClass('dn');
			}, 800);
		});
	});
	
	//카테고리 메뉴
	$('.slidemenu>li>a span').click(function() {
		var thisa = $(this).parent();
		var checkElement = thisa.next();
		
		if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
			thisa.removeClass("now");
			checkElement.slideUp('normal');
		}
		if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
			$(".slidemenu>li>a").removeClass("now");
			thisa.addClass("now");
			$('.slidemenu>li>ul:visible').slideUp('normal');
			checkElement.slideDown('normal');
		}
		if(thisa.closest('li').find('ul').children().length == 0) {
			return true;	
		} else {
			return false;	
		}	
	});
	//

	/*
	jQuery(".btn_mMenu").on("click",function(){
		jQuery(".mo_hd_btm").addClass("on")
		jQuery(".mo_hd_btm").prepend("<div class='dim'></div>")
		jQuery(".mo_menu").addClass("on")
	})
	jQuery(".btn_mMenuClose").on("click",function(){
		jQuery(".mo_menu").removeClass("on")
		jQuery(".mo_hd_btm").removeClass("on")
		jQuery(".dim").remove()
	})
	jQuery(document.body).on("click",".dim",function(){
		jQuery(".mo_menu").removeClass("on")
		jQuery(".mo_hd_btm").removeClass("on")
		jQuery(".dim").remove()
	})
	*/
	

	//즐겨찾기 
	$('#favorite').on('click', function(e) {
		var bookmarkURL = window.location.href;
		var bookmarkTitle = document.title;
		var triggerDefault = false;
		if (window.sidebar && window.sidebar.addPanel) {
			// Firefox version &lt; 23
			window.sidebar.addPanel(bookmarkTitle, bookmarkURL, '');
		} else if ((window.sidebar && (navigator.userAgent.toLowerCase().indexOf('firefox') < -1)) || (window.opera && window.print)) {
			// Firefox version &gt;= 23 and Opera Hotlist
			var $this = $(this);
			$this.attr('href', bookmarkURL);
			$this.attr('title', bookmarkTitle);
			$this.attr('rel', 'sidebar');
			$this.off(e);
			triggerDefault = true;
		} else if (window.external && ('AddFavorite' in window.external)) {
			// IE Favorite
			window.external.AddFavorite(bookmarkURL, bookmarkTitle);
		} else {
			// WebKit - Safari/Chrome
			alert((navigator.userAgent.toLowerCase().indexOf('mac') != -1 ? 'Cmd' : 'Ctrl') + '+D 를 이용해 이 페이지를 즐겨찾기에 추가할 수 있습니다.');
		}
		return triggerDefault;
	});
	//즐겨찾기
});
</script>
		
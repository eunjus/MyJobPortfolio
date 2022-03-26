<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>
		
<link rel="stylesheet" type="text/css" href="resources/css/hexagon/reset.css" />
<link rel="stylesheet" type="text/css" href="resources/css/hexagon/skin.css" />
<!-- 반응형 css -->
<link rel="stylesheet" type="text/css" href="resources/css/hexagon/pc.css" />
<link rel="stylesheet" type="text/css" href="resources/css/hexagon/mobile.css" />
<link rel="stylesheet" type="text/css" href="resources/css/hexagon/tablet.css" />

<style>
.bbs_view div.view_files {
	padding: 0.5% 20px;
	border-bottom: 1px solid #bbbbbb;
	background: #fafafa;
	font-size: 0;
}

.bbs_view div.view_files > p{
	font-size: 15px;
	width: 20%;
	line-height: inherit;	
}

table.fileTable td.file_name a {
}

table.fileTable td.file_name a:hover {
	text-decoration: underline;
}
</style>
		
	<!-- ** layout정리190513  ** -->
	<article id="container" class="clear">
		<div id="contents_wrap" class="clear">
			<!-- 측면 시작 #aside -->
				<div id="side_box">
<div class="sub_menu">
	<dl>
		<dt class="">community</dt>
			<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
			<dd class="menu01 "><a href="${pageContext.request.contextPath}/community.do?code=notice">공지사항</a></dd>
			<dd class="menu02 on"><a href="${pageContext.request.contextPath}/community.do?code=qna">Q&A</a></dd>
	</dl>
</div>
				</div><!-- #aside -->
			<!-- 측면 끝 #aside -->

			<div id="contents_box">

				<!-- #네비게이션 시작 .nav_wrap -->
					<!-- outline/header -->
					<div class="nav_wrap">
						<div class="nav_box">
							<h2>
									Q&A<!-- 게시판명 노출 -->
							</h2>
							<p>
									Q&A 게시판 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/community.do?code=notice">community</a> ></li>
								<li>
									<strong>

												Q&A<!-- 게시판명 노출 -->

									</strong>
								</li>
							</ul>
						</div>
					</div>
				<!-- #네비게이션 끝 .nav_wrap -->

				<!-- 컨텐츠 시작 #contents -->
				<div id="content" class="clear ">
					<!-- #공통 상단요소 시작 -->
<ul id="sub_nav" class="submenu dn">
	<li><a href="/board/board_list?code=history" >연혁</a></li>
	<li><a href="/board/board_list?code=recruit" >recruit</a></li>
	<li><a href="${pageContext.request.contextPath}/recruit.do?sub=human" >gallery</a></li>
	<li><a href="${pageContext.request.contextPath}/community.do?code=notice" class="on">notice</a></li>
	<li><a href="/board/board_list?code=inquiry" >Q&A</a></li>
	<li><a href="/board/board_list?code=review" >리뷰</a></li>
</ul><!--submenu-->

					<!-- #공통 상단요소 끝 -->
					

					<!-- #서브 컨텐츠 시작 -->

<!-- ** layout정리190513  ** -->

<%@ include file="/WEB-INF/include/include-body.jsp" %>
<script type="text/javascript" >
var post_no = '${list.post_number}';

var Common_Board = new common_board({
	code : "qna",
	no : post_no,
	is_login : "",
	nonMember_title : "(비회원) 개인정보 수집항목 동의",
});

	$(function() {	

		//기존 댓글이 있을 경우에만 수정부분 숨김처리
		if('${list.reply_yn}'==='y' && '${reply}'!== null)
			document.getElementById("modify_comment_content_1").style.display = "none";
			
		
			$("form[name='comment_frm']").validate({
				rules : {
					name : {required : true},
					password : {required : true, rangelength : [4, 20]},
					//content : {required : {depends : function(){return !getSmartEditor("comment");}}},
					content : {required : true},
					file : {}
				}, messages : {
					name : {required : "작성자를 입력해주세요."},
					password : {required : "비밀번호를 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다.")},
					content : {required : "내용을 입력해주세요."},
					file : {}
				}
			});
		});

	
	function fn_deleteQna()
	{ 
		if(!confirm("삭제하시겠습니까?")) {
			return false;
		}
		
		var comSubmit = new ComSubmit(); 
		comSubmit.setUrl('<c:url value="deleteBoard.do?code=qna&no=${list.post_number}" />'); 
		comSubmit.submit(); 
	}
	
	function comment_modify_check(idx, is_secret) { 		
		$("#modify_comment_content_"+idx).show();			
	}

	function comment_delete_check(idx, is_secret) { 			

		Common_Board.board_comment_delete(idx); 		
	}
		
</script>
<div class="sub_content">
	<div class="sub_board">
		<div class="bbs_view">			
			<div class="view_tit">				
				<h3>${list.post_title}</h3>
				<p>
					${list.post_writer}&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;${list.reg_dt}
				</p>
			</div>
			<div class="view_cont">
				<p>${list.post } </p>
			</div>
			<div class="view_files"><p>파일 목록</p></div>	
				<table class="fileTable">
					<caption>게시글 내용</caption>
					<colgroup>						
						<col width="71%">
						<col width="29%">		
					</colgroup>
				<tbody>
				<c:choose>
					<c:when test="${fn:length(fileList) > 0}">
						<c:forEach items="${fileList}" var="row" varStatus="status">														
							<tr>								
								<td style="display:none">${row.file_number}</td>					
								<td class="file_name" style="font-size: 14px; padding: 0.5% 20px;">
									<a href="${pageContext.request.contextPath}/fileDown?no=${row.file_number}">${row.org_file_name}(${row.file_size})</a><br>
								</td>
							</tr>							
						</c:forEach>
					</c:when>
					<c:otherwise>
					</c:otherwise>
				</c:choose>
				</tbody>
				</table><!--board_list-->
			<div style="border-bottom: 1px solid #bbbbbb;"></div>
		</div><!--bbs_view-->
	<c:choose>	
	<c:when test="${sessionScope.userid ne null}">	
		<c:if test="${sessionScope.authority eq 'ROLE_ADMIN' && list.reply_yn eq 'n'}">
		<form action="boardView.do" name=comment_frm method="post" accept-charset="utf-8">		
				<fieldset>
					<legend>게시글에대한 코멘트 작성</legend>
					<div class="board_comment">
						
						<textarea name="content" allowBlank="false" label="댓글" title="댓글 내용을 작성헤주세요."></textarea>
						<input type="hidden" name="file_fname" />
						<input type="hidden" name="file_oname" />
						<input type="hidden" name="reply_yn" value ='y' />
						<input type="hidden" name="no" id="no" value="${list.post_number}" />
						<a href="javascript://" onclick="Common_Board.board_comment_write(document.comment_frm);" class="comment_btn">댓글달기</a>
				
					</div><!-- .board_comment -->
				</fieldset>
			</form>
			</c:if>
	</c:when>
	</c:choose>
	
	<c:if test="${list.reply_yn eq 'y' && not empty reply}">
			<div class="board_comment_list">
				<div class="comment_title">댓글 <span>1</span>개</div>
				<div class="comment" id="display_comment_1">
					<input type="hidden" name="idx_1" value="1" /> 
					<input type="hidden" name="mode_1" value="" /> 
					<div class="comment_writer">${reply.post_writer}<span class="board_line"></span>${reply.reg_dt}</div>
				<c:choose>
				<c:when test="${sessionScope.userid ne null}">	
					<c:if test="${sessionScope.authority eq 'ROLE_ADMIN' && list.reply_yn eq 'y'}">
						<div class="com_btn">
							<div id="view_comment_btn_1">
								<a class="btn_more" href="javascript://" onclick="comment_modify_check('1', 's');">수정</a>							
								<span class="board_line"></span>
								<a class="btn_more" href="javascript://" onclick="comment_delete_check('1', 's')">삭제</a>																			
							</div>
						</div>
					</c:if>
				</c:when>
				</c:choose>
	
					<div id="view_comment_content_1"class="com_txt">${reply.post}</div>
					<div id="modify_comment_content_1" class="com_modify">
						<textarea name="content_1" title="댓글을 수정해주세요">${reply.post}</textarea>
						<input type="hidden" name="name_1" value="${reply.post_writer}" />
						<a href="javascript://" onclick="Common_Board.board_comment_modify('1')" class="btn_modify">수정</a>
					</div>
					
				</div>
			</div>
		</c:if>
			
		<div class="view_btn">
			<div class="btn_wrap ta_left"><a href="${pageContext.request.contextPath}/community.do?code=qna" class="btn btn_point">목록</a></div>
			<div class="btn_wrap ta_right">
				
					<a href="${pageContext.request.contextPath}/detailBoard?code=qna_secret&no=${list.post_number}&sub=update" class="btn_basic btn">수정</a>
					<a href="javascript://" onclick="fn_deleteQna(); return false;" class="btn_basic btn">삭제</a>
							
			</div>
		</div><!--view_btn-->
	</div>
</div>
<!-- ** layout정리190513  ** -->
					<!-- #서브 컨텐츠 끝 -->

				</div><!-- #contents -->
				<!-- 컨텐츠 끝 #contents -->

			</div><!-- #contents_box -->

		</div><!-- #contents_wrap -->
	</div><!-- #container -->
<!-- ** layout정리190513  ** -->

	<!-- include footer -->
<%@ include file="/WEB-INF/include/footer.jsp" %>

</article><!-- #wrap 전체영역 -->
<iframe name="ifr_processor" id="ifr_processor" title="&nbsp;" width="0" height="0" frameborder="0" style="display:none;"></iframe>
</body>
</html>
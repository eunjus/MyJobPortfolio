<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>

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

<script src="resources/js/jquery.min.js"></script>
<script src="resources/js/jquery.validate.min.js"></script>
	<!-- ** layout정리190513  ** -->
	<article id="container" class="clear">
		<div id="contents_wrap" class="clear">
			<!-- 측면 시작 #aside -->
				<div id="side_box">
<div class="sub_menu">
	<dl>
		<dt class="">community</dt>
			<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
			<dd class="menu01 on"><a href="${pageContext.request.contextPath}/community.do?code=notice">공지사항</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/community.do?code=qna">Q&A</a></dd>
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
									notice<!-- 게시판명 노출 -->
							</h2>
							<p>
									notice 게시판 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/community.do?code=notice">community</a> ></li>
								<li>
									<strong>

												공지사항<!-- 게시판명 노출 -->

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
	
	function fn_deleteQna()
	{ 
		if(!confirm("삭제하시겠습니까?")) {
			return false;
		}
		
		var comSubmit = new ComSubmit(); 
		comSubmit.setUrl('<c:url value="deleteBoard.do?code=notice&no=${list.post_number}" />'); 
		comSubmit.submit(); 
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



		<div class="view_btn">
			<div class="btn_wrap ta_left"><a href="${pageContext.request.contextPath}/community.do?code=notice" class="btn btn_point">목록</a></div>
			<div class="btn_wrap ta_right">
			
		<c:if test="${sessionScope.userid ne null && !authority}">
				<a href="${pageContext.request.contextPath}/detailBoard?code=notice&no=${list.post_number}" class="btn_basic btn">수정</a>
				<a href="javascript://" onclick="fn_deleteQna(); return false;" class="btn_basic btn">삭제</a>			
		</c:if>
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

<script>

</script>

</article><!-- #wrap 전체영역 -->
<iframe name="ifr_processor" id="ifr_processor" title="&nbsp;" width="0" height="0" frameborder="0" style="display:none;"></iframe>
</body>
</html>
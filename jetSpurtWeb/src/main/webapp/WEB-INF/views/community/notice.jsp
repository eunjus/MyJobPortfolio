<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>

<style>	
	.btn_right {position:absolute; bottom:25px; right:0;}
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
<script src="resources/js/jquery.min.js"></script>
<script src="resources/js/jquery.validate.min.js"></script>
<script type="text/javascript">
	$(function() {
		$("form[name='frm']").validate({
			rules : {
				search : {required : true}
			}, messages : {
				search : {required : "검색어를 입력해주세요."}
			}
		});
	});
</script>
<div class="sub_cont">
	<div class="sub_board">
		<div class="clear">
			<form action="boardSearch.do" name="frm" method="get" accept-charset="utf-8">

			<div class="board_search">
				<fieldset>
				<legend>게시글 검색</legend>
					<input type="hidden" name="code" value="notice" />
					<select name="search_type" id="search_type" title="검색할 항목을 선택하세요." class="select">
						<option value="title" selected="selected">제목</option>
						<option value="content">내용</option>
						<option value="name">작성자</option>						
					</select>
					<label for="search" class="dn">검색어를 입력하세요.</label>
					<input type="text" name="search" id="search" value="" class="input_text" />
					<input type="submit" class="btn_md btn_default" value="검색" />
				</fieldset>
			</div><!--board_search-->
			</form>
		</div>
<div class="bbs_num">총 게시물 <strong>${fn:length(list)}</strong>개</div>
<table class="bbs_list" summary="게시글 제목, 작성자, 작성일, 조회수, 게시글 내용 등..">
	<caption>게시글 내용</caption>
	<colgroup>
		<col width="7%">
		<col >
		<col width="10%">
		<col width="12%">		
	</colgroup>
	<thead>
		<tr>
			<th scope="col">번호</th>
			<th scope="col">제목</th>
			<th scope="col" class="m_non">작성자</th>
			<th scope="col" class="m_non">작성일</th>			
		</tr>
	</thead>
	<tbody>
		<c:choose>
			<c:when test="${fn:length(list) > 0}">
				<c:forEach items="${list}" var="row" varStatus="status">
					<tr>
						<td>${status.count}</td>
						<td class="left">
						<a href="${pageContext.request.contextPath}/boardView.do?code=notice&no=${row.post_number}"> ${row.post_title}</a> </td>
						<td>${row.post_writer }</td>
						<td>${row.reg_dt }</td>
					</tr>
				</c:forEach>
			</c:when>
			<c:otherwise>
				<tr>
					<td colspan="4">조회된 결과가 없습니다.</td>
				</tr>
			</c:otherwise>
		</c:choose>
		
	</tbody>
</table><!--board_list-->
		<div class="view_btn clear">		
		</div>	
			
	<c:if test="${not empty login && authority}">
		<div class="btn_right"><a href="${pageContext.request.contextPath}/community.do?code=notice_write" class="btn orange">글쓰기</a></div>
	</c:if>
	
	</div><!-- .sub_board -->
</div><!-- .sub_content -->
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
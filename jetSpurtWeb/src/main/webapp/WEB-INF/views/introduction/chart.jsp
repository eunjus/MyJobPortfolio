<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>
		
		
	<!-- ** layout정리190513  ** -->
	<article id="container" class="clear">
		<div id="contents_wrap" class="clear">
			<!-- 측면 시작 #aside -->
				<div id="side_box">
<div class="sub_menu">
	<dl>
		<dt class="">인사말</dt>
			<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
			<dd class="menu01 "><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/about.do?sub=history">연혁</a></dd>
			<dd class="menu03 on"><a href="${pageContext.request.contextPath}/about.do?sub=chart">조직도</a></dd>
			<dd class="menu04 "><a href="${pageContext.request.contextPath}/about.do?sub=introduce">사업소개</a></dd>
			<dd class="menu05 "><a href="${pageContext.request.contextPath}/about.do?sub=location">오시는 길</a></dd>
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
									chart<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							<p>
									chart 페이지 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a> ></li>
								<li>
									<strong>
										chart<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
									</strong>
								</li>
							</ul>
						</div>
					</div>
				<!-- #네비게이션 끝 .nav_wrap -->

				<!-- 컨텐츠 시작 #contents -->
				<div id="content" class="clear ">
					<!-- #공통 상단요소 시작 -->
					<!-- #공통 상단요소 끝 -->

					

					<!-- #서브 컨텐츠 시작 -->

<!-- ** layout정리190513  ** -->
<div class="sub_content">
	<div class="sub_chart chart1">
		<div class="chart">
			<div class="chart_box">
				<h3>감사</h3>
				<h3>대표이사</h3>
				<div class="chart_step">
					<h4>총괄이사</h4>
					<ul>
						<li>
							<h5>경영본부</h5>
							<p>회사경영관리</p>
						</li>
						<li>
							<h5>관리부</h5>
							<p>세무/회계 관리<br>인사관리</p>
						</li>
						<li>
							<h5>공무본부</h5>
							<p>입찰/수주<br>계약관리</p>
						</li>
						<li>
							<h5>건설본부</h5>
							<p>건축사업<br>주택사업<br/>안전관리</p>
						</li>
						<li>
							<h5>품질본부</h5>
							<p>품질관리<br/>AS서비스</p>
						</li>
					</ul>
				</div>
			</div>
		</div>
	</div><!--sub_chart-->
</div><!--sub_content-->
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
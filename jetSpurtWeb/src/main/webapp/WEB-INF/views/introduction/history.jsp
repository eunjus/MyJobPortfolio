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
			<dd class="menu02 on"><a href="${pageContext.request.contextPath}/about.do?sub=history">연혁</a></dd>
			<dd class="menu03 "><a href="${pageContext.request.contextPath}/about.do?sub=chart">조직도</a></dd>
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
									history<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							<p>
									history 페이지 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a> ></li>
								<li>
									<strong>
										history<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
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

<div class="mask"></div>

	<article id="content">
		<div id="content_sub">	
			<div class="sub_history history1">
				<div class="history_intro"></div>
				<div class="history_wrap">
					<div class="history_box">
						<div class="his_year">
							<p class="year">
								<strong>2017</strong>
							</p>
						</div>
						<div class="his_detail">
							<dl class="detailYear">
								<dt>03</dt>
								<dd>
									<ul>
										<li>㈜헥사곤 창립 2주년</li>
										<li>전기 공사업 면허 등록</li>
									</ul>
								</dd>
							</dl>
							<dl class="detailYear">
								<dt>08</dt>
								<dd>
									<ul>
										<li>사우디아라비아 해외 빌딩 건설 진출</li>
										<li>㈜헥사곤 우수시공업체 선정</li>
									</ul>
								</dd>
							</dl>
						</div>
					</div>
					<div class="history_box">
						<div class="his_year">
							<p class="year">
								<strong>2016</strong>
							</p>
						</div>
						<div class="his_detail">
							<dl class="detailYear">
								<dt>05</dt>
								<dd>
									<ul>
										<li>부산 도로공사 수주</li>
										<li>말레이시아 우준타워 복합개발 공사 수주</li>
										<li>한경 주거문화대상 ㈜헥사곤 아파트대상 수상</li>
									</ul>
								</dd>
							</dl>
							<dl class="detailYear">
								<dt>07</dt>
								<dd>
									<ul>
										<li>한국토지주택공사 품질우수업체 선정</li>
									</ul>
								</dd>
							</dl>
							<dl class="detailYear">
								<dt>11</dt>
								<dd>
									<ul>
										<li>부산 ㈜헥사곤 호텔 착공</li>
										<li>서울 대치동 ㈜헥사곤 아파트 준공</li>
									</ul>
								</dd>
							</dl>
						</div>
					</div>
					<div class="history_box">
						<div class="his_year">
							<p class="year">
								<strong>2015</strong>
							</p>
						</div>
						<div class="his_detail">
							<dl class="detailYear">
								<dt>08</dt>
								<dd>
									<ul>
										<li>서울 대치동 ㈜헥사곤 빌딩 착공</li>
										<li>주식회사 ㈜헥사곤 출범(건설/무역 부문)</li>
									</ul>
								</dd>
							</dl>
							<dl class="detailYear">
								<dt>09</dt>
								<dd>
									<ul>
										<li>㈜헥사곤 대표 이사 김홍국  취임</li>
									</ul>
								</dd>
							</dl>
							<dl class="detailYear">
								<dt>10</dt>
								<dd>
									<ul>
										<li>㈜헥사곤 주식회사 창립</li>
										<li>건축 공사업 면허 취득</li>
									</ul>
								</dd>
							</dl>
						</div>
					</div>
				</div><!-- .history -->
			</div><!-- sub_history -->
		</div><!--content_sub-->
	</article><!-- #content 본문 감싸는 영역 -->

	<!-- include footer -->
<%@ include file="/WEB-INF/include/footer.jsp" %>

</article><!-- #wrap 전체영역 -->
<iframe name="ifr_processor" id="ifr_processor" title="&nbsp;" width="0" height="0" frameborder="0" style="display:none;"></iframe>
</body>
</html>
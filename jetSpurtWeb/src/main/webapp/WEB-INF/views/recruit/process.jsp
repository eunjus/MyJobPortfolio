<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>

<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>

<link rel="stylesheet" type="text/css" href="resources/css/daou/sub.css" /><!-- 서브 공통 custom 요소 css ｜ 개별페이지 css -->

	<!-- ** layout정리190513  ** -->
	<article id="container" class="clear">
		<div id="contents_wrap" class="clear">
			<!-- 측면 시작 #aside -->
				<div id="side_box">
<div class="sub_menu">
	<dl>
		<dt class="">채용</dt>
			<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
			<dd class="menu01 "><a href="${pageContext.request.contextPath}/recruit.do?sub=human">인재상/교육</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/recruit.do?sub=benefit">복지</a></dd>
			<dd class="menu03 on"><a href="${pageContext.request.contextPath}/about.do?sub=chart">채용절차</a></dd>
			<dd class="menu04 "><a href="${pageContext.request.contextPath}/recruit.do?sub=recruit_write">채용문의</a></dd>			
	</dl>
</div>
				</div><!-- #aside -->
			<!-- 측면 끝 #aside -->

		<div id="contents_box">
		
		<!-- #네비게이션 시작 .nav_wrap -->
					<!-- outline/header -->
					<div class="nav_wrap">
						<div class="nav_box">							
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">채용</a> ></li>
								<li>
									<strong>
										채용절차<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
									</strong>
								</li>
							</ul>
						</div>
					</div>
				<!-- #네비게이션 끝 .nav_wrap -->
				
			<!-- 컨텐츠 Start -->
			<div class="SubContents">
				<div class="ContBox">
					<h1 class="Tit">신입 공채 전형</h1>
                    <img src="resources/images/p-images/recruit_img23.jpg" class="FullImg1" alt="Step01. 서류전형/Step02. 인적성검사(기술 직군에 한하여 기술력 진단이 진행됩니다.)/Step03. 면접전형/Step04. 입사확정">
                    <h1 class="Tit">신입 채용 전제 인턴 전형</h1>
                    <img src="resources/images/p-images/recruit_img24.jpg" class="FullImg1" alt="Step01. 서류전형/Step02. 면접전형(기술 직군에 한하여 기술력 진단이 진행됩니다.)/Step03. 인턴근무/Step04. 입사확정">
                    <h1 class="Tit">경력 채용 전형</h1>
                    <img src="resources/images/p-images/recruit_img25.jpg" class="FullImg2" alt="Step01. 서류전형/Step02.면접전형/Step03. 온라인 인적성 검사/Step04. 입사 확정">
				</div>
				<div class="ContBox">
					<h1 class="Tit">자격 요건</h1>
					<table class="Type_01 mtnone">
						<caption>경력, 신입/채용 전제형 인턴 자격 요건 관련 표</caption>
						<colgroup>
							<col style="width:20%;">
							<col style="width:80%;">
						</colgroup>
						<tr>
							<th scope="row">경력</th>
							<td>동종 업계 만 2년 이상 경력자 / 전문대졸 이상</td>
						</tr>
						<tr>
							<th scope="row">신입 / 채용 전제형 인턴</th>
							<td>대졸 이상</td>
						</tr>
					</table>
				</div>
				<div class="ContBox">
					<h1 class="Tit">지원 방법</h1>
					<table class="Type_01 mtnone">
						<caption>온라인 입사지원 방법 관련 표</caption>
						<colgroup>
							<col style="width:20%;">
							<col style="width:80%;">
						</colgroup>
						<tr>
							<th scope="row">온라인 입사지원</th>
							<td><a href="https://recruit.daou.co.kr/" target="_blank">다우기술 채용홈페이지 (recruit.daou.co.kr)</a></td>
						</tr>
					</table>
				</div>
                <div class="ContBox mb70">
					<h1 class="Tit">채용 일정</h1>
					<table class="Type_01 mtnone">
						<caption>정기/수시 채용 일정 관련 표</caption>
						<colgroup>
							<col style="width:20%;">
							<col style="width:80%;">
						</colgroup>
						<tr>
							<th scope="row">정기채용</th>
							<td>신입사원 공채를 중심으로 매년 상/하반기 2회 진행</td>
						</tr>
                        <tr>
							<th scope="row">수시채용</th>
							<td>채용 전제형 인턴, 경력사원을 중심으로 연중 수시 진행</td>
						</tr>
					</table>
				</div>
			</div>
			<!-- 컨텐츠 End -->

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
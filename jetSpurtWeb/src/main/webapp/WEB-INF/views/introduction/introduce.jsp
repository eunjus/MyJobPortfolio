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
			<dd class="menu03 "><a href="${pageContext.request.contextPath}/about.do?sub=chart">조직도</a></dd>
			<dd class="menu04 on"><a href="${pageContext.request.contextPath}/about.do?sub=introduce">사업소개</a></dd>
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
									introduce<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							<p>
									introduce 페이지 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a> ></li>
								<li>
									<strong>
										introduce<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
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
<div class="sub_introduce introduce2">
			<div class="box1 introduce_box">
				<ul class="duce_list">
					<li>
						<div class="thumb"><img src="resources/images/p-images/introduce2_list_thumb01.jpg"></div>
						<div class="txt">
							<h4><em>공공사업</em></h4>
							<p><strong>선진화된 건설사업관리 능력</strong>으로 성공적인 건설을 약속</p>
						</div>
					</li>
					<li>
						<div class="thumb"><img src="resources/images/p-images/introduce2_list_thumb02.jpg"></div>
						<div class="txt">
							<h4><em>솔루션사업 </em></h4>
							<p>사업성검토, 설계, 관리 등 <strong>전문적인 서비스</strong>로 개발이익의 극대화</p>
						</div>
					</li>
					<li>
						<div class="thumb"><img src="resources/images/p-images/introduce2_list_thumb03.jpg"></div>
						<div class="txt">
							<h4><em>SI/SM 사업 </em></h4>
							<p>세계시장에서 위상을 높이며 <strong>글로벌 최고의 기업</strong>으로 도약</p>
						</div>
					</li>
				</ul>
			</div><!-- .introduce_box -->
			<div class="box2 introduce_box">
				<div class="duce_message">
					<p>㈜젯스퍼트(는) <strong>미래가치형 글로벌기업</strong>입니다.</p>
					<span>- 임직원 일동 -</span>
				</div>
			</div><!-- .introduce_box -->
		</div><!--sub_introduce-->
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
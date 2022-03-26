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
			<dd class="menu01 on"><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/about.do?sub=history">연혁</a></dd>
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
									about<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							<p>
									about 페이지 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a> ></li>
								<li>
									<strong>
										about<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
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
	<div class="sub_about">
		<div class="sub_about_img"></div>

		<div class="sub_about_txt">
			<p class="txt01">Beyond Technology</p>
			<p class="txt02">저희 기업은 차별화된 가치 창출을 위해 끊임없이 노력하고 다양한 서비스 사업과 제품 생산에 힘쓰고 있습니다.</p>
			<p class="txt03">트루 인더스트리은(는) 오랜 시간 쌓은 노하우와 경험을 바탕으로
			합리적인 시스템을 구축하고 있으며 효율적인 사업대안을 제시하고 차별화된 경쟁력을 통해 성공적인 사업을 추진합니다.
					계획과 개발의 균형을 최우선으로 생각하고 장기적이고 체계적인 사업계획으로 변화에 유연하게 대처하는 최상의 서비스를 제공합니다.</p>
		</div><!-- .sub_about_txt -->

		<div class="sub_about_btn">
			<ul>
				<li class="gray"><a href="/goods/goods_list?cate=001">제품소개</a></li>
				<li class="orange"><a href="${pageContext.request.contextPath}/recruit.do?sub=human">시공사례</a></li>
			</ul>
		</div><!-- .sub_about_btn -->

		<div class="sub_about_list">
			<ul class="clear">
				<li class="li01">
					<p>
						<strong>기술선도</strong>
						<span>고객의 Needs 그 이상의 가치와<br> 창조 개발에 공헌합니다.</span>
						<span class="for_m">고객의 Needs 그 이상의 가치,<br>창조 개발 공헌</span>
					</p>
				</li>
				<li class="li02">
					<p>
						<strong>글로벌네트워크</strong>
						<span>전 세계로 수출하는 신뢰할 수 있는<br> 트루 인더스트리입니다.</span>
						<span class="for_m">전 세계 수출,<br>신뢰할 수 있는 기업</span>
					</p>
				</li>
				<li class="li03">
					<p>
						<strong>도전적 실행</strong>
						<span>미래를 향한 열정으로 늘 노력하며<br> 새로운 가능성에 도전합니다.</span>
						<span class="for_m">미래를 향한 열정<br>새로운 가능성에 도전</span>
					</p>
				</li>
				<li class="li04">
					<p>
						<strong>윤리경영</strong>
						<span>투명하고 공정한 기업경영으로<br> 미래를 이끌겠습니다.</span>
						<span class="for_m">투명하고 공정한 기업경영<br>미래를 이끌겠습니다.</span>
					</p>
				</li>
			</ul>
		</div><!-- .sub_about_list -->
	</div><!-- .sub_about -->
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
<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>
		
	<!-- ** layout정리190513  ** -->
	<article id="container" class="clear">
		<div id="contents_wrap" class="clear">
			<!-- 측면 시작 #aside -->
				<div id="side_box">
				
<div class="sub_menu">
    <!--leftmenu영역시작-->
    <dl>
		<dt class="">회원관련</dt>
		<dd class="menu01 "><a href="${pageContext.request.contextPath}/member.do?code=login">로그인</a></dd>
		<dd class="menu02 "><a href="${pageContext.request.contextPath}/member.do?code=find_id">아이디 찾기</a></dd>
		<dd class="menu03 "><a href="${pageContext.request.contextPath}/member.do?code=find_pw">비밀번호 찾기</a></dd>
		<dd class="menu04 "><a href="${pageContext.request.contextPath}/member.do?code=join_agreement" class="">회원가입</a></dd>
		<dd class="menu05 "><a href="${pageContext.request.contextPath}/member.do?code=agreement" class="">이용약관</a></dd>
		<dd class="last menu06 on"><a href="${pageContext.request.contextPath}/member.do?code=usepolicy" class="">개인정보처리방침</a></dd>
	</dl>
    <!--leftmenu영역끝-->
</div><!-- .sub_menu -->
				</div><!-- #aside -->
			<!-- 측면 끝 #aside -->

			<div id="contents_box">

				<!-- #네비게이션 시작 .nav_wrap -->
					<!-- outline/header -->
					<div class="nav_wrap">
						<div class="nav_box">
							<h2>
									개인정보처리방침
							</h2>
							<p>
									개인정보처리방침 페이지입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
								<li>
									<strong>
										개인정보처리방침
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
<div class="sub_cont">	
	<div class="sub_content">
		<div class="sub_agree">
			<div class="agree_box member_agree">
				<div class="agree_box_con">㈜젯스퍼트(이하 ‘회사'라 함)는 회원의 개인정보 보호를 매우 중요시하며, 『정보통신망 이용촉진 및 정보보호 등에 관한 법률』 및 전기통신사업법, 통신비밀보호법을 준수하고 있습니다. <br />
<br />
회사는 개인정보취급방침을 수립하여 회원의 개인정보를 보호하고 있으며, 이를 홈페이지에 명시하여 회원이 온라인상에서 회사에 제공한 개인정보가 어떠한 용도와 방식으로 이용되고 있으며 개인정보보호를 위해 어떠한 조치를 취하는지 알려드립니다. <br />
<br />
회사의 개인정보취급방침은 다음과 같은 내용을 담고 있습니다. <br />
1. 수집하는 개인정보 항목 및 수집방법 <br />
2. 개인정보의 수집 및 이용목적 <br />
3. 수집한 개인정보의 보유 및 이용기간 <br />
4. 수집한 개인정보의 공유 및 제공 <br />
5. 수집한 개인정보 취급위탁<br />
6. 개인정보의 파기 절차 및 방법 <br />
7. 이용자 및 법정대리인의 권리와 그 행사방법 <br />
8. 개인정보 자동수집장치의 설치, 운영 및 그 거부에 관한 사항 <br />
9. 개인정보의 기술적, 관리적 보호 대책 <br />
10 개인정보 관리 책임자 및 담당자의소속-성명 및 연락처 <br />
11. 개인정보관련 신고 및 분쟁조정 <br />
12. 고지의 의무</div>
			</div>
		 </div>
	</div><!-- .sub_content -->
</div><!--sub_cont-->
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
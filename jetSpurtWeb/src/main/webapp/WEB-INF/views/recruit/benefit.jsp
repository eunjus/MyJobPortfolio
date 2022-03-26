<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<link rel="stylesheet" type="text/css" href="resources/css/bing/common.css" />
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>
	
		
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
			<dd class="menu02 on"><a href="${pageContext.request.contextPath}/recruit.do?sub=benefit">복지</a></dd>
			<dd class="menu03 "><a href="${pageContext.request.contextPath}/recruit.do?sub=process">채용절차</a></dd>
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
							<h2>
									복리후생<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							<p>
									일하는 즐거움과 보람이 가득한 복리후생 프로그램을 시행하고 있습니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">채용</a> ></li>
								<li>
									<strong>
										복지<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
									</strong>
								</li>
							</ul>
						</div>
					</div>
				<!-- #네비게이션 끝 .nav_wrap -->

				<!-- 컨텐츠 시작 #contents -->
	<div id="main_con">
		<div class="ct">                			
                <table class="benefittb">
                    <tr>
                        <td class="txtc"><img src="resources/images/p-images/ct_benefit_01.png" /></td>
                        <td>성과 인센티브<br />
                            Profit Sharing 제도<br />
			                            중식대 지원<br />
			                            유류비 전액 지원 (영업)<br />
			                            휴대폰 사용료 지원 (영업)
	                 	</td>
                        <td>
			                            연차수당<br />
			                            자격 / 면허 및 직책 수당<br />
			                            명절조 지급 자녀 학자금 지원<br />
			                            각종 경조금 지원
                        </td>
                    </tr>
                    <tr>
                     	<td class="txtc"><img src="resources/images/p-images/ct_benefit_02.png" /></td>
                        <td>
			                            전국 주요콘도 이용<br />
			                            단체 상해보험<br />
			                            사내 동우회<br />
			                            장기근속자 포상
                        </td>
                        <td>
			                            정기 건강검진<br />
			                            배우자 종합검진<br />
			                            우수사원 표창 / 보상<br />
                           	4대 보험
                        </td>
                    </tr>
                    <tr>
                        <td class="txtc"><img src="resources/images/p-images/ct_benefit_03.png" /></td>
                        <td>
			                            주 5일 근무<br />
	                        4주 단위 자율근로제 (Flexible time)<br />
	                        Mobile Office 지원<br />
			                            법적 연차 外 추가 휴가 8일<br />
			                            연차/반차/반반차 휴가
                        </td>
                        <td>
                           	경조휴가<br />
                         	休 Nine 제도 (9일 연속휴가)<br />
			                            사원아파트 운영 (공장)<br />
			                            통근버스 운행 (공장)<br />
			                            각종 기념일 선물 지급
                        </td>
                    </tr>
                    <tr>
                        <td class="bb1 txtc"><img src="resources/images/p-images/ct_benefit_04.png" /></td>
                        <td class="bb1">
			                            신입사원 입문과정<br />
			                            마케팅대학<br />
			                            해외 테마 연수<br />
			                            해외 세미나<br />
			                            외국어 강좌<br />
                            MBA 학위과정
                        </td>
                        <td class="bb1">
			                            직무 전문가 과정<br />
			                            국제 심포지엄 리더쉽 교육<br />
			                            스마트러닝<br />
			                            학습조직 (Study Group)<br />
			                            팀빌딩 프로그램
                        </td>
                    </tr>
                </table>
            </div>
           </div>
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
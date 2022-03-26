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
			<dd class="menu01 on"><a href="${pageContext.request.contextPath}/recruit.do?sub=human">인재상/교육</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/recruit.do?sub=benefit">복지</a></dd>
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
									<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							
							<p>									
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">채용</a> ></li>
								<li>
									<strong>
										인재상/인재육성<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
									</strong>
								</li>
							</ul>
						</div>
					</div>
				<!-- #네비게이션 끝 .nav_wrap -->

				<!-- 컨텐츠 시작 #contents -->
	<div id="main_con">
		<div class="ct"> 
			<h3>인재상<span>젯스퍼트는 최초를 생각하는 창조인, 최선을 다하는 열정인, 최고를 지향하는 도전인을 인재상으로 하여,<br/>체계적이고 다양한 인재육성 프로그램으로 전문가를 육성하고 글로벌 리더를 양성합니다.</span></h3>
<br/><br/><br/><br/><h3>인재육성</h3>
                <table class="humantb">
                    <tr>
                        <td class="txtc"><img src="resources/images/p-images/ct_human_01.png" /> 
                        <h4>신입사원 양성과정</h4>
                        <p>사내·외 우수 강사진의 전문 지식 교육과<br/>
                            실무 현장 경험, 팀워크 활동을 통해<br />
                            기본 소양과 업무지식, 실무 감각을 갖춘<br />
                            경쟁력 있는 젯스퍼트인을 육성합니다.<br />&nbsp;
                        </p>
                        </td>
                        <td class="txtc">
                            <img src="resources/images/p-images/ct_human_02.png" />
                            <h4>직무 전문가 과정</h4>
                            <p>
                                체계적인 직무교육과 사내강사 제도,<br />
                                스마트러닝, 도서학습, 학습조직, 세미나,<br />
                                학술지, 케이스 스터디 등 다양한 콘텐츠와<br />
                                학습방법을 제공하여 각 분야의 전문가로<br />
                                성장할 수 있도록 지원합니다.
                            </p>
                        </td>
                        <td class="txtc">
                            <img src="resources/images/p-images/ct_human_03.png" />
                            <h4>MBA 학위과정</h4>
                            <p>
                                젯스퍼트의 미래 변화를 선도할<br />
                                전문 경영인으로 성장할 수 있도록<br />
                                최신 트렌드와 경영이론을 배우고 다양한 분야의<br />
                                경영인들과 네트워크를 구축할 수 있는<br />
                                국내 및 해외 대학원 위탁과정을 시행합니다.
                            </p>
                        </td>
                    </tr>
                </table>
                <table class="humantb">
                    <tr>
                        <td class="txtc bb1" style="padding-left:90px;">
                            <img src="resources/images/p-images/ct_human_04.png" />
                            <h4>Global Business 교육 및 해외연수</h4>
                            <p>
                                해외 시장의 확대와 더불어 국제적 시각과<br />
                                마인드를 갖춘 글로벌 인재 양성을 목표로<br />
                                어학교육은 물론 해외 Biz전문 교육,<br />
                                각 국가별 특화교육, 해외연수 및 세미나를<br />
                                실시하고 있습니다.
                            </p>
                        </td>
                        <td class="txtc bb1" style="padding-right:140px;">
                            <img src="resources/images/p-images/ct_human_05.png" />
                            <h4>Team Building Program</h4>
                            <p>
                                Fun . Dynamic한 프로그램으로<br />
                                젯스퍼트인의 열정과 자신감을 표출하고<br />
                                도전과 혁신, 신뢰와 협력의 조직문화를<br />
                                만들어 갑니다.<br />&nbsp;
                            </p>
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
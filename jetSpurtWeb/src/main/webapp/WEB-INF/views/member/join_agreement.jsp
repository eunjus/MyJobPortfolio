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
		<dd class="menu04 on"><a href="${pageContext.request.contextPath}/member.do?code=join_agreement" class="">회원가입</a></dd>
		<dd class="menu05 "><a href="${pageContext.request.contextPath}/member.do?code=agreement" class="">이용약관</a></dd>
		<dd class="last menu06 "><a href="${pageContext.request.contextPath}/member.do?code=usepolicy" class="">개인정보처리방침</a></dd>
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
									회원가입
							</h2>
							<p>
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
								<li>
									<strong>
										회원가입
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

<script type="text/javascript">
	$(function() {
		$("form[name='frm']").validate({
			rules : {
				agree : {required : true},
				agree2 : {required : true}
			}, messages : {
				agree : {required : "이용약관에 동의해주세요."},
				agree2 : {required : "개인정보 수집에 동의해주세요."}
			}
		});
	});
</script>

<div class="sub_content">
	<form action="member.do" name=frm method="post" accept-charset="utf-8">

	<fieldset>
	<legend>회원가입 약관 동의</legend>
	<div class="sub_agree">
		<div class="agree_box line">
			<h3 class="bbs_write_title">기업 이용약관 동의</h3>
			<textarea title="기업 이용약관 동의">제1조 (목적)
본 약관(이하 &quot;약관&quot;)은 ㈜젯스퍼트(이하 '회사')와 회원에 관한 제반사항을 규정함을 목적으로 합니다.

제2조 (약관의 효력 등) 
① 약관은 공시하고 상대방이 동의함으로써 효력을 발생합니다. 본 약관의 공시는 회사 홈페이지 ({홈페이지ㅣURL코드})에 게시하는 방법으로 합니다.
② 회사는 약관의 규제에 관한 법률 등 관련법을 위배하지 않는 범위내에서 약관을 개정할 수 있습니다.
③ 회사가 약관을 개정할 경우에는 시행일 및 개정사유를 명시하여 회사 홈페이지에 시행일 7일전까지 공지합니다.
④ 제3항의 방법으로 변경 고지된 약관은 기존의 회원에게도 유효하게 적용됩니다.

제3조 (약관의 해석 및 관할법원)
① 약관에 정하지 아니한 사항과 이 약관의 해석에 관하여는 관계법령 및 상관례에 따릅니다.
② 회원과 회사 사이에 분쟁이 발생할 경우에 관할 법원은 서울중앙지방법원으로 합니다.

제4조 (용어의 정의)
'회원'은 회사에 개인정보를 제공하여 회원등록을 한 자로서, 회사가 제공하는 서비스를 계속적으로 이용할 수 있는자를 말합니다.

제5조 (회원 가입 및 자격)
① 회사가 정한 양식에 따라 회원정보를 기입한 후 회원가입을 신청함으로써 회원으로 등록됩니다.
② 다음 각 호에 해당하는 경우에 회사는 회원 가입을 인정하지 않거나 회원 자격을 박탈할 수 있습니다.
1. 다른 사람의 명의를 사용하여 가입 신청한 경우
2. 신청 시 필수 작성 사항을 허위로 기재한 경우
3. 관계법령의 위반을 목적으로 신청하거나 그러한 행위를 하는 경우
4. 사회의 안녕질서 또는 미풍양속을 저해할 목적으로 신청하거나 그러한 행위를 하는 경우
5. 다른 사람의 회사의 이용을 방해하거나 그 정보를 도용하는 등 전자거래질서를 위협하는 경우
③ 회사가 회원 자격을 박탈하는 경우에는 회원등록을 말소합니다. 이 경우 회원에게 사전 통지하여 소명할 기회를 부여합니다.

제6조 (개인정보의 취득 및 이용)
① 회사는 개인정보 보호정책을 제정하여 시행하고, 개인 정보의 취득과 이용, 보호 등에 관한 법률을 준수합니다. 개인정보보호정책은 홈페이지 하단에 상시적으로 게시합니다.
② 회사는 고객이 제공하는 개인정보를 본 서비스 이외의 목적을 위하여 사용할 수 없습니다.
③ 회사는 고객이 제공한 개인정보를 고객의 사전 동의 없이 제 3자에게 제공할 수 없습니다. 단, 다음 각 호에 해당하는 경우에는 예외로 합니다.
1. 도메인이름 검색서비스를 제공하는 경우
2. 전기통신기본법 등 관계법령에 의하여 국가기관의 요청에 의한 경우
3. 범죄에 대한 수사상의 목적이 있거나 정보통신윤리위원회의 요청이 있는 경우
4. 업무상 연락을 위하여 회원의 정보(성명, 주소, 전화번호)를 사용하는 경우
5. 은행업무상 관련사항에 한하여 일부 정보를 공유하는 경우
6. 통계작성, 홍보자료, 학술연구 또는 시장조사를 위하여 필요한 경우로서 특정 고객임을 식별할 수 없는 형태로 
제공되는 경우

제7조 (회원 탈퇴)
① 회원은 회사에 언제든지 탈퇴를 요청할 수 있으며 회사는 즉시 회원탈퇴를 처리합니다.
② 회원이 회사에서 이용중인 서비스의 만기일이 지나지 않은 경우 회사는 탈퇴를 처리하지 않습니다.

제8조 (회원에 대한 통지)
① 회사가 회원에 대한 통지를 하는 경우, 회원이 회사에 제출한 전자우편 주소로 할 수 있습니다.
② 회사는 불특정다수 회원에 대한 통지의 경우 1주일 이상 회사 게시판에 게시함으로서 개별 통지에 갈음할 수 있습니다.

제9조 (쿠폰의 발행 및 사용)
① 쿠폰이란 일정 금액 또는 비율을 회사 서비스 비용 결제시 할인 받을 수 있는 온라인 또는 오프라인 쿠폰을 말하며, 회원은 해당 쿠폰을 사용할 수 있습니다.
② 쿠폰은 회사가 발행할 수 있으며, 쿠폰의 사용 범위 및 할인한도, 유효기간, 제한 사항 등은 쿠폰 또는 서비스 화면에 표시합니다.
③ 쿠폰은 해당 유효기간내 사용을 원칙으로 하며 재발행, 양도, 매매, 환불이 불가능합니다.

제10조 (회사의 의무)
① 회사는 본 약관이 정하는 바에 따라 지속적이고 안정적인 서비스를 제공하는데 최선을 다합니다.
② 회사는 항상 등록자의 정보를 포함한 개인신상정보에 대하여 관리적, 기술적 안전조치를 강구하여 정보보안에 최선을 다합니다.
③ 회사는 공정하고 건전한 운영을 통하여 전자상거래 질서유지에 최선을 다하고 지속적인 연구개발을 통하여 양질의 서비스를 제공함으로써 고객만족을 극대화하여 인터넷 사업 발전에 기여합니다.
④ 회사는 고객으로부터 제기되는 불편사항 및 문제에 대해 정당하다고 판단될 경우 우선적으로 그 문제를 즉시 처리합니다. 단, 신속한 처리가 곤란할 경우, 고객에게 그 사유와 처리일정을 즉시 통보합니다.
⑤ 회사는 소비자 보호단체 및 공공기관의 소비자 보호업무의 추진에 필요한 자료 등의 요구에 적극 협력합니다.

제11조 (회원의 의무)
① ID와 비밀번호에 관한 모든 관리의 책임은 회원에게 있습니다.
② 회원은 ID와 비밀번호를 제 3 자가 알 수 있도록 해서는 안 됩니다.
③ 회원은 본 약관 및 관계법령에서 규정한 사항을 준수합니다. 

부칙</textarea>
			<p><input type="checkbox" name="agree" id="agree" value="y" /> <label for="agree">이용약관에 동의합니다.</label></p>
		</div><!--agree_box-->
		<div class="agree_box">
			<h3 class="bbs_write_title">개인정보 수집 및 이용에 대한 안내 </h3>
			<textarea title="개인정보 수집 및 이용에 대한 안내">㈜젯스퍼트(이하 ‘회사'라 함)는 회원의 개인정보 보호를 매우 중요시하며, 『정보통신망 이용촉진 및 정보보호 등에 관한 법률』 및 전기통신사업법, 통신비밀보호법을 준수하고 있습니다. 

회사는 개인정보취급방침을 수립하여 회원의 개인정보를 보호하고 있으며, 이를 홈페이지에 명시하여 회원이 온라인상에서 회사에 제공한 개인정보가 어떠한 용도와 방식으로 이용되고 있으며 개인정보보호를 위해 어떠한 조치를 취하는지 알려드립니다. 

회사의 개인정보취급방침은 다음과 같은 내용을 담고 있습니다. 
1. 수집하는 개인정보 항목 및 수집방법 
2. 개인정보의 수집 및 이용목적 
3. 수집한 개인정보의 보유 및 이용기간 
4. 수집한 개인정보의 공유 및 제공 
5. 수집한 개인정보 취급위탁
6. 개인정보의 파기 절차 및 방법 
7. 이용자 및 법정대리인의 권리와 그 행사방법 
8. 개인정보 자동수집장치의 설치, 운영 및 그 거부에 관한 사항 
9. 개인정보의 기술적, 관리적 보호 대책 
10 개인정보 관리 책임자 및 담당자의소속-성명 및 연락처 
11. 개인정보관련 신고 및 분쟁조정 
12. 고지의 의무</textarea>
			<p><input type="checkbox" name="agree2" id="agree2" value="y" /> <label for="agree2">개인정보 수집 및 이용에 대한 정책에 동의 합니다.</label></p>
		</div><!--agree_box-->
		<input type="hidden" name="code" id="code" value="join_reg" />
		<div class="btn_wrap ta_center">
			<button type="submit"><a class="btn btn_point">확인</a></button>
			<button onclick="history.back();"><a href="javascript://" class="btn btn_basic">취소</a></button>
		</div><!--btn_center-->
	</div><!--sub_agree-->
	</fieldset>
	</form>

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
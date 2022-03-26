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
		<dt class="">마이페이지</dt>
		<dd class="menu01 "><a href="${pageContext.request.contextPath}/logout">로그아웃</a></dd>
		<dd class="menu02 "><a href="${pageContext.request.contextPath}/mypage">회원정보수정</a></dd>
		<dd class="menu03 "><a href="${pageContext.request.contextPath}/member.do?code=change_pw">비밀번호 변경</a></dd>
		<dd class="menu04 on"><a href="${pageContext.request.contextPath}/member.do?code=withdrawal">회원탈퇴</a></dd>
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
									회원탈퇴
							</h2>
							<p>
							</p>
							<ul>
								<li><a href="/" class="home">Home</a> ></li>
								<li>
									<strong>
										회원탈퇴
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

					

<!-- ** layout정리190513  ** -->
<script type="text/javascript">
	$(function() {
		$("form[name='frm']").validate({
			rules : {
				password : {required : true},
				withdrawal_reason : {required : true, maxlength : 100},
			}, messages : {
				password : {required : "비밀번호를 입력해주세요."},
				withdrawal_reason : {required : "회원탈퇴사유를 입력해주세요.", maxlength : $.validator.format("회원탈퇴사유를 {0}자 이내로 입력해주세요.")}
			}
		});
	});

	function withdrawal_chk() {
		var frm = $("form[name='frm']");
		if(!frm.valid()) {
			return false;
		}

		if(!confirm("회원탈퇴하시겠습니까?")) {
			return false;
		}
		frm.submit();
	}
</script>

<div class="sub_content">
	<div class="sub_join">
		<form action="withdrawalUser.do" name=frm method="post" accept-charset="utf-8">

		<fieldset>
			<legend>회원 탈퇴</legend>
			<table class="bbs_write" summary="회원 아이디, 비밀번호, 탈퇴사유 입력">
				<caption>회원탈퇴시 회원정보 재확인</caption>
				<colgroup>
					<col width="15%">
					<col >
				</colgroup>
				<tbody>
					<tr>
						<th scope="row">아이디</th>
						<td>${sessionScope.userid}</td>
					</tr>
					<tr>
						<th scope="row">비밀번호</th>
						<td><input type="password" name="password" id="password" /><label for="password" class="dn">비밀번호</label></td>
					</tr>				
				</tbody>
			</table><!-- .bbs_write -->
			
			<p class="bbs_write_info">* 회원탈퇴 후에도 재가입은 가능하나, 동일한 아이디로 재가입은 절대 불가능합니다. 한번 가입했던 아이디는 영구적으로 사용이 중지됩니다.</p>

			<div class="btn_wrap ta_center">
				<button onclick="withdrawal_chk(); return false;"><a class="btn btn_point" href="javascript://">회원탈퇴</a></button>
				<button onclick="history.back(); return false;"><a class="btn btn_basic" href="javascript://">취소</a></button>
			</div><!-- .btn_center -->
		</fieldset>
		</form>
	</div><!--sub_join-->

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
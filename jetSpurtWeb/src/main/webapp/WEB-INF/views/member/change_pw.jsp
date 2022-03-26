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
		<dd class="menu03 on"><a href="${pageContext.request.contextPath}/member.do?code=change_pw">비밀번호 변경</a></dd>
		<dd class="menu04 "><a href="${pageContext.request.contextPath}/member.do?code=withdrawal">회원탈퇴</a></dd>
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
									비밀번호변경
							</h2>
							<p>
							</p>
							<ul>
								<li><a href="/" class="home">Home</a> ></li>
								<li>
									<strong>
										비밀번호변경
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
<script src="resources/js/jquery.min.js"></script>
<script src="resources/js/jquery.validate.min.js"></script>
<script type="text/javascript">

	$(function() {
		$("form[name='frm']").validate({
			submitHandler: function(form) {
				form.submit();
			}, rules : {
				old_password : {required : true},
				password : {required : true, rangelength : [10, 16], equalTo : "#password2", passwordValid : true},
				password2 : {required : true, rangelength : [10, 16], passwordValid : true}
			}, messages : {
				old_password : {required : "비밀번호를 입력해주세요."},
				password : {required : "새 비밀번호를 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다."), equalTo : "새 비밀번호가 일치하지 않습니다.", passwordValid : "비밀번호는 영어/숫자/특수문자를 2종류 이상 혼용하여 사용해야 합니다."},
				password2 : {required : "새 비밀번호 확인을 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다."), passwordValid : "비밀번호는 영어/숫자/특수문자를 2종류 이상 혼용하여 사용해야 합니다."}
			}
		});
	})
	
		function change_chk(form) {
		if(!$(form).valid()){
			return false;
		}
		form.submit();
	}
</script>
<div class="sub_content">
	<h2 class="bbs_write_title">비밀번호 변경</h2>
	<div class="sub_agree change_pw">
		<form action="changeUserPW.do" name=frm method="post" accept-charset="utf-8">

		<table class="bbs_write" summary="비밀번호 변경" style="">
			<caption>회원 비밀번호 변경</caption>
			<colgroup>
				<col width="15%">
				<col >
			</colgroup>
			<tbody>
				<tr>
					<th scope="row">비밀번호</th>
					<td><input type="password" id="changepw1" name="old_password" value=""><label for="changepw1" class="dn">비밀번호</label></td>
				</tr>
				<tr>
					<th scope="row">새 비밀번호</th>
					<td><input type="password" id="changepw2" name="password" value=""><label for="changepw2" class="dn">새 비밀번호</label></td>
				</tr>
				<tr>
					<th scope="row">새 비밀번호 확인</th>
					<td><input type="password" id="password2" name="password2" value=""><label for="changepw3" class="dn">새 비밀번호 확인</label></td>
				</tr>
			</tbody>
		</table><!-- .bbs_write -->
		<p class="bbs_write_info">안전한 개인정보보호를 위해 비밀번호를 변경해주세요.</p>
		<div class="btn_wrap ta_center">
			<button onclick="change_chk(this.form); return false;"><a href="javascript://" class="btn btn_point">변경</a></button>
			<button onclick="history.back(); return false;"><a href="javascript://" class="btn btn_basic">취소</a></button>
		</div><!--btn_center-->
		</form>
	 </div>
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
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
		<dd class="menu02 on"><a href="${pageContext.request.contextPath}/member.do?code=find_id">아이디 찾기</a></dd>
		<dd class="menu03 "><a href="${pageContext.request.contextPath}/member.do?code=find_pw">비밀번호 찾기</a></dd>
		<dd class="menu04 "><a href="${pageContext.request.contextPath}/member.do?code=join_agreement" class="">회원가입</a></dd>
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
									아이디찾기
							</h2>
							<p>
									아이디를 잊으셨나요? 회원가입 시 입력한 정보를 입력해주세요.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
								<li>
									<strong>
										아이디찾기
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
				name : {required : true},
				email : {required : true, email : true}
			}, messages : {
				name : {required : "이름을 입력해주세요."},
				email : {required : "이메일을 입력해주세요.", email : "올바른 이메일을 입력해주세요."}
			}
		});
	});
	
	function find_id(form) {
		if(!$(form).valid()){
			return false;
		}
		//form.target = "ifr_processor";
		form.submit();
	}
</script>

<div class="sub_content">
	<div class="sub_login">
		<div class="login_box">
			<form action="find_id.do" name=frm method="post" accept-charset="utf-8">

			<fieldset>
			<legend>회원 이름, 이메일 정보로 분실한 아이디 찾기</legend>
			<input type="hidden" name="mode" value="find_id" />
			<ul>
				<li><input type="text" name="username" id="username" value="" placeholder="이름"><label for="username" class="dn">회원이름</label></li>
				<li><input type="text" name="email" id="email" value="" placeholder="이메일"><label for="email" class="dn">회원이메일</label></li>
				<li><a href="javascript://" ><button onclick="secret_chk(document.frm);" class="btn_wd btn_point">SEARCH</button></a></li>
				
			</ul>
			</fieldset>
			</form>
			<ul class="login_link">
				<li><a href="${pageContext.request.contextPath}/member.do?code=login">로그인</a></li>
				<li><a href="${pageContext.request.contextPath}/member.do?code=find_pw">비밀번호찾기</a></li>
				<li><a href="${pageContext.request.contextPath}/member.do?code=join_agreement">회원가입</a></li>
			</ul>
		</div><!--login_box-->
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
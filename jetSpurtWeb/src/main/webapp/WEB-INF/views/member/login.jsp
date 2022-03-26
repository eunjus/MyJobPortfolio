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
		<dd class="menu01 on"><a href="${pageContext.request.contextPath}/member.do?code=login" class="">로그인</a></dd>
		<dd class="menu02 "><a href="${pageContext.request.contextPath}/member.do?code=find_id" class="">아이디 찾기</a></dd>
		<dd class="menu03 "><a href="${pageContext.request.contextPath}/member.do?code=find_pw" class="">비밀번호 찾기</a></dd>
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
									로그인
							</h2>
							<p>
									회원이 아니신 경우에는 회원가입 후 이용해 주시기 바랍니다.
							</p>
							<ul>
								<li><a href="/myapp" class="home">Home</a> ></li>
								<li>
									<strong>
										로그인
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
<!-- <script type="text/javascript" src="/lib/js/common_member.js"></script> -->
<script type="text/javascript" src="resources/js/common_member.js"></script>
<script type="text/javascript">
	var Common_Member = new common_member({
		is_login : ""
	});

	$(function() {
		$("form[name='frm']").validate({
			rules : {
				userid : {required : true},
				password : {required : true}
			}, messages : {
				userid : {required : "아이디를 입력해주세요."},
				password : {required : "비밀번호를 입력해주세요."}
			}
		});
	});

</script>

<div class="sub_content">
	<div class="sub_login">
		<div class="login_box">
			<form action="loginPost.do" name=frm method="post" accept-charset="utf-8">

			<fieldset>
			<legend>회원 로그인 아이디/비밀번호 입력</legend>
			<input type="hidden" name="encrypt" value="p2" />
			<input type="hidden" name="return_url" value="" />
			<ul>
				<li><input type="text" name="userid" id="userid" placeholder="아이디"><label for="userid" class="dn">회원아이디</label></li>
				<li><input type="password" name="password" id="password" placeholder="비밀번호"><label for="password" class="dn">회원비밀번호</label></li>
				<li><button type="submit" class="btn_wd btn_point">LOGIN</button></li>
			</ul>
			</fieldset>
			</form>
			<ul class="login_link">
				<li class="first"><a href="${pageContext.request.contextPath}/member.do?code=find_id">아이디찾기</a></li>
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
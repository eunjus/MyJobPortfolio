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
					<dt class="">community</dt>
						<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
						<dd class="menu01 "><a href="${pageContext.request.contextPath}/community.do?code=notice">공지사항</a></dd>
						<dd class="menu02 on"><a href="${pageContext.request.contextPath}/community.do?code=qna">Q&A</a></dd>
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
									Q&A<!-- 게시판명 노출 -->
							</h2>
							<p>
									Q&A 게시판 입니다.
							</p>
							<ul>
								<li><a href="/" class="home">Home</a> ></li>
									<li><a href="/board/board_write?code=inquiry">cs </a> ></li>
								<li>
									<strong>
									<!-- 목록 사용안함일때 -->
											Q&A<!-- 게시판명 노출 -->

									</strong>
								</li>
							</ul>
						</div>
					</div>
				<!-- #네비게이션 끝 .nav_wrap -->

				<!-- 컨텐츠 시작 #contents -->
				<div id="content" class="clear ">
					<!-- #공통 상단요소 시작 -->
<ul id="sub_nav" class="submenu dn">
	<li><a href="/board/board_list?code=history" >연혁</a></li>
	<li><a href="/board/board_list?code=recruit" >recruit</a></li>
	<li><a href="${pageContext.request.contextPath}/recruit.do?sub=human" >gallery</a></li>
	<li><a href="${pageContext.request.contextPath}/community.do?code=notice" class="on">notice</a></li>
	<li><a href="/board/board_list?code=inquiry" >Q&A</a></li>
	<li><a href="/board/board_list?code=review" >리뷰</a></li>
</ul><!--submenu-->

					<!-- #공통 상단요소 끝 -->

					<!-- #서브 컨텐츠 시작 -->

<!-- ** layout정리190513  ** -->

	<script type="text/javascript">
		$(function() {
			$("form[name='frm']").validate({
				rules : {
					password : {required : true, rangelength : [4, 20]}
				}, messages : {
					password : {required : "비밀번호를 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다.")}
				}
			});
		});

		function secret_chk(form) {
			if(!$(form).valid()){
				return false;
			}
			form.submit();
		}
	</script>
	<div class="sub_content" >
		<div class="sub_board">
		<div class="sub_pw_input">
			<form action="boardView.do" method="POST" name="frm" accept-charset="utf-8">
			<fieldset>
				<legend>게시글 비밀번호 입력</legend>
				<div class="ok_box">
					<h2>작성시 입력하셨던 비밀번호를 입력해주세요.</h2>
					<div class="input_box">
					<input type="password" name="password" />
					<input type="hidden" name="sub" value="${sub}" />
					<input type="hidden" name="code" value="qna" />
					<input type="hidden" name="no" value="${no}"/>
					</div>
					<a href="javascript://" onclick="secret_chk(document.frm);" class="ok_bt">확인</a>
				</div><!--login_box-->
			</fieldset>
			</form>
		</div><!--sub_ok-->
	</div><!-- .sub_cont -->
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
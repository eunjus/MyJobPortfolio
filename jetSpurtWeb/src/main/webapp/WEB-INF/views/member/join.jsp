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
<script type="text/javascript" src="resources/js/common_member.js"></script>
<script type="text/javascript">
	var Common_Member = new common_member({});

	$(function() {
		$("form[name='frm']").validate({
			rules : {
				userid : {required : true, rangelength: [4, 14], onlyNumEngValid : true},
				userid_duplicate : {required : function(ele){return !(ele.value == "y");}},
				name  : {required : true, maxlength : 10},
				password : {required : true, rangelength : [10, 16], equalTo : "#password2", passwordValid : true},
				sex : {required : true},
				birth : {required : true, rangelength : [8, 8], number : true, man14Valid : true},
				email_duplicate : {required : function(ele){return $("[name='email_id']").val() && $("[name='email_domain']").val() && !(ele.value == "y");}},
				email : {required : true, email : true},
				zip : {required : true},
				address : {required :  true },
				address2 : {required :  true },
				mobile : {required : true, phoneValid : true},
				fax : {required : false, phoneValid : false},
				yn_mailling : {required : true},
				yn_sms : {required : true},
				ex1 : {required : false},
				ex2 : {required : false},
				ex3 : {required : false},
				ex4 : {required : false},
				ex5 : {required : false},
				ex6 : {required : false},
				ex7 : {required : false},
				ex8 : {required : false},
				ex9 : {required : false},
				ex10 : {required : false},
				ex11 : {required : false},
				ex12 : {required : false},
				ex13 : {required : false},
				ex14 : {required : false},
				ex15 : {required : false},
				ex16 : {required : false},
				ex17 : {required : false},
				ex18 : {required : false},
				ex19 : {required : false},
				ex20 : {required : false}
			}, messages : {
				userid : {required : "아이디를 입력해주세요.", rangelength: $.validator.format("아이디는 {0}~{1}자입니다."), onlyNumEngValid : "아이디는 영어, 숫자만 사용 가능합니다."},
				userid_duplicate : {required : "아이디 중복확인을 해주세요."},
				name : {required : "이름를 입력해주세요.", maxlength: $.validator.format("이름은 {0}자 이하입니다.")},
				password : {required : "비밀번호를 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다."), equalTo : "비밀번호가 일치하지 않습니다.", passwordValid : "비밀번호는 영어/숫자/특수문자를 2종류 이상 혼용하여 사용해야 합니다."},
				sex : {required : "성별를 선택해주세요."},
				birth : {required : "생년월일를 입력해주세요.", rangelength: $.validator.format("생년월일을 입력해주세요. ex)19900101"), number : "숫자만 입력가능합니다.", man14Valid : "14세 미만 회원가입 시 법정대리인의 동의가 필요합니다. 고객센터로 문의해주세요."},
				email_duplicate : {required : "이메일 중복확인을 해주세요."},
				email : {required : "이메일를 입력해주세요.", email : "올바른 이메일을 입력해주세요."},
				zip : {required : "주소를 입력해주세요."},
				address : {required : "주소를 입력해주세요."},
				address2 : {required : "상세주소를 입력해주세요."},
				mobile : {required : "연락처를 입력해주세요.", phoneValid : "올바른 연락처를 입력해주세요. ex)000-0000-0000)"},
				fax : {required : "팩스를 입력해주세요.", phoneValid : "올바른 팩스를 입력해주세요. ex)000-0000-0000)"},
				yn_mailling : {required : "메일수신를 선택해주세요."},
				yn_sms : {required : "SMS수신를 선택해주세요."},
				ex1 : {required : "예비1를 입력해주세요."},
				ex2 : {required : "예비2를 입력해주세요."},
				ex3 : {required : "예비3를 입력해주세요."},
				ex4 : {required : "예비4를 입력해주세요."},
				ex5 : {required : "예비5를 입력해주세요."},
				ex6 : {required : "예비6를 입력해주세요."},
				ex7 : {required : "예비7를 입력해주세요."},
				ex8 : {required : "예비8를 입력해주세요."},
				ex9 : {required : "예비9를 입력해주세요."},
				ex10 : {required : "예비10를 입력해주세요."},
				ex11 : {required : "예비11를 입력해주세요."},
				ex12 : {required : "예비12를 입력해주세요."},
				ex13 : {required : "예비13를 입력해주세요."},
				ex14 : {required : "예비14를 입력해주세요."},
				ex15 : {required : "예비15를 입력해주세요."},
				ex16 : {required : "예비16를 입력해주세요."},
				ex17 : {required : "예비17를 입력해주세요."},
				ex18 : {required : "예비18를 입력해주세요."},
				ex19 : {required : "예비19를 입력해주세요."},
				ex20 : {required : "예비20를 입력해주세요."}
			}
		});
	});

	function join_chk(form) {
		if(!$(form).valid()){
			return false;
		}
		//form.target = "ifr_processor";
		form.submit();
	}
</script>

<div class="sub_content">
	<div class="sub_agree">
		<div class="sub_join">

			<form action="insertNewUser.do" name=frm method="post" accept-charset="utf-8">

			<fieldset>
			<legend>회원가입 정보 기입</legend>
			<h2 class="bbs_write_title">기본정보</h2>
			<table class="bbs_write" summary="회원가입 정보 입력,아이디,이름,비밀번호,비밀번호확인,이메일 등등..">
				<caption>회원가입 정보 입력</caption>
				<colgroup>
					<col width="15%">
					<col >
				</colgroup>
				<tbody>
					<tr>
						<th class="join_tit" scope="row">이름<span class="require_dot"></span></th>
						<td><input type="text" name="name" id="name" /><label for="name" class="dn">이름</label></td>
					</tr>
					<tr>
						<th class="join_tit" scope="row">아이디<span class="require_dot"></span></th>
						<td>
							<input type="text" name="userid" id="userid" onkeyup="Common_Member.duplicate_init(this.form.userid_duplicate);" />
							<label for="userid" class="dn">아이디</label>
							<input type="hidden" name="userid_duplicate" />
							<a href="javascript://" onclick="Common_Member.userid_duplicate_check(document.frm.userid);" class="btn_sm">아이디 중복확인</a>
						</td>
					</tr>
					<tr>
						<th class="join_tit" scope="row">비밀번호<span class="require_dot"></span></th>
						<td><input type="password" name="password" id="password" /><label for="password" class="dn">비밀번호</label><p>영문 대·소문자/숫자/특수문자 중 2가지 이상을 혼용하여 10~16자 이내로 작성해주세요.</p></td>
					</tr>
					<tr>
						<th class="join_tit" scope="row">비밀번호 확인<span class="require_dot"></span></th>
						<td><input type="password" name="password2" id="password2" /><label for="password2" class="dn">비밀번호 확인</label></td>
					</tr>

					<tr>
						<th class="join_tit" scope="row">생년월일 <span class="require_dot"></span></th>
						<td>
							<input type="text" name="birth" id="birth" placeholder="예) 19950301" /><label for="birth" class="dn">생년월일</label>
<p>14세 미만 회원가입 시 법정대리인의 동의가 필요합니다. 고객센터로 문의해주세요.</p>
						</td>
					</tr>


					<tr>
						<th class="join_tit" scope="row">이메일 <span class="require_dot"></span></th>
						<td>
							<input type="hidden" name="email_duplicate" />
							<input type="hidden" name="email" />
							<input type="text" name="email_id" onkeyup="Common_Member.duplicate_init(this.form.email_duplicate, this.form.email);" style=";" /> @&nbsp;&nbsp;
							<input type="text" name="email_domain" onchange="Common_Member.duplicate_init(this.form.email_duplicate, this.form.email);" style=";" />
							<select name="email_domain_select" title="선택해주세요" class="mail_select" onchange="domain_select_change('email');Common_Member.duplicate_init(this.form.email_duplicate, this.form.email);">
								<option value="직접입력">직접입력</option>
								<option value="daum.net">daum.net</option>
								<option value="naver.com">naver.com</option>
								<option value="yahoo.com">yahoo.com</option>
								<option value="gmail.com">gmail.com</option>
							</select>
							<a href="javascript://" onclick="Common_Member.email_duplicate_check(document.frm.email);" class="btn_sm">이메일 중복확인</a>
						</td>
					</tr>


					<tr>
						<th class="join_tit" scope="row">우편번호 <span class="require_dot"></span></th>
						<td>
							<input type="text" name="zip" id="zip" readonly /><label for="zip" class="dn">우편번호</label>
							<a href="javascript://" onclick="searchAddress(document.frm.zip, document.frm.address, document.frm.address2)"  class="btn_sm">주소검색</a>
						</td>
					</tr>


					<tr>
						<th class="join_tit" scope="row">주소 <span class="require_dot"></span></th>
						<td>
							<input type="text" name="address" id="address" readonly /><label for="address" class="dn">주소</label>
						</td>
					</tr>


					<tr>
						<th class="join_tit" scope="row">상세주소 <span class="require_dot"></span></th>
						<td>
							<input type="text" name="address2" id="address2"/><label for="address2" class="dn">상세주소</label>
						</td>
					</tr>


					<tr>
						<th class="join_tit" scope="row">연락처 <span class="require_dot"></span></th>
						<td>
							<input type="text" id="mobile" name="mobile" value="" placeholder="예) 010-1234-1234"/><label for="mobile" class="dn">연락처</label>
						</td>
					</tr>
				</tbody>
			</table><!--bbs_write-->
			<div class="btn_wrap ta_center">
				<button onclick="join_chk(this.form); return false;"><a href="javascript://" class="btn btn_point">회원가입</a></button>
				<button onclick="history.back(); return false;"><a href="javascript://" class="btn btn_basic">취소</a></button>
			</div><!--btn_center-->
			</fieldset>
			</form>

		</div>
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
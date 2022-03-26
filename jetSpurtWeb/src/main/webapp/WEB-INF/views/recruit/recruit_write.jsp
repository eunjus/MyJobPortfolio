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
		<dt class="">채용</dt>
			<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
			<dd class="menu01 "><a href="${pageContext.request.contextPath}/recruit.do?sub=human">인재상/교육</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/recruit.do?sub=benefit">복지</a></dd>
			<dd class="menu03 "><a href="${pageContext.request.contextPath}/recruit.do?sub=process">채용절차</a></dd>
			<dd class="menu04 on"><a href="${pageContext.request.contextPath}/recruit.do?sub=recruit_write">채용문의</a></dd>	
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
									채용 문의<!-- 게시판명 노출 -->
							</h2>
							<p>
									젯스퍼트 채용과 관련하여 궁금한 사항을 질문해주세요.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="/board/board_list?code=notice">인재채용</a> ></li>
								<li>
									<strong>

												<a href="/board/board_list?code=recruit">채용 문의<!-- 게시판명 노출 --></a>

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
	<li><a href="/board/board_list?code=recruit" class="on">recruit</a></li>
	<li><a href="${pageContext.request.contextPath}/recruit.do?sub=human" >gallery</a></li>
	<li><a href="/board/board_list?code=notice" >notice</a></li>
	<li><a href="/board/board_list?code=inquiry" >Q&A</a></li>
	<li><a href="/board/board_list?code=review" >리뷰</a></li>
</ul><!--submenu-->

					<!-- #공통 상단요소 끝 -->
				
					<!-- #서브 컨텐츠 시작 -->

<!-- ** layout정리190513  ** -->
<div class="sub_content">
	<div class="sub_board">
				
<script type="text/javascript" src="resources/js/common_board.js"></script>
<!-- <script src="//cdnjs.cloudflare.com/ajax/libs/validate.js/0.12.0/validate.min.js"></script>-->
<script type="text/javascript" src="resources/lib/se2.8.2/js/HuskyEZCreator.js" charset="utf-8"></script>
<script>
	var oEditors = [];
	
	var Common_Board = new common_board({
		code : "qna",
		no : "",
		is_login : ""
	});

	 $(function() {
		 
	       nhn.husky.EZCreator.createInIFrame({
	           oAppRef: oEditors,
	           elPlaceHolder: "contents", //textarea에서 지정한 id와 일치해야 합니다. 
	           //SmartEditor2Skin.html 파일이 존재하는 경로
	           sSkinURI: "resources/lib/se2.8.2/SmartEditor2Skin.php",  
	           htParams : {
	               // 툴바 사용 여부 (true:사용/ false:사용하지 않음)
	               bUseToolbar : true,             
	               // 입력창 크기 조절바 사용 여부 (true:사용/ false:사용하지 않음)
	               bUseVerticalResizer : true,     
	               // 모드 탭(Editor | HTML | TEXT) 사용 여부 (true:사용/ false:사용하지 않음)
	               bUseModeChanger : true,         
	               fOnBeforeUnload : function(){
	                    
	               }
	           }, 
	           fOnAppLoad : function(){
	               //기존 저장된 내용의 text 내용을 에디터상에 뿌려주고자 할때 사용
	               oEditors.getById["contents"].exec("PASTE_HTML", [""]);
	           },
	           fCreator: "createSEditor2"
	       });
       
       //저장버튼 클릭시 form 전송
       $("#save").click(function(){
           oEditors.getById["contents"].exec("UPDATE_CONTENTS_FIELD", []);
           $("#frm").submit();
       });  
       
		$("form[name='frm']").validate({
			rules : {
				title : {required : true},
				mobile : {required : true, phoneValid : true},
				email : {required : true, email : true},
				name : {required : true},
				password : {required : true, rangelength : [4, 20]},
			//메인에서 에디터 적용금지
				//content : {required : true},
				file : {},
				nonMember : {required : true},
			}, messages : {
				title : {required : "제목을 입력해주세요."},
				mobile : {required : "휴대폰을 입력해주세요.", phoneValid : "올바른 휴대폰을 입력해주세요. ex)000-0000-0000)"},
				email : {required : "이메일을 입력해주세요.", email : "올바른 이메일을 입력해주세요."},
				name : {required : "작성자를 입력해주세요."},
				password : {required : "비밀번호를 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다.")},
			//메인에서 에디터 적용금지
				//content : {editorRequired : "내용을 입력해주세요."},
				file : {},
			//메인에서 태그 가져오지 못하는 오류 수정
				nonMember : {required : "(비회원) 개인정보 수집항목 동의를 체크해주세요."},
			}
		});
		
		//메인에서 에디터 적용금지
		//attachSmartEditor("contents", "board");
		//uploadForm.init(document.frm);
	});

    function thumbnail_image_choice(value) {
        var file_fname = $('[name="'+value+'_fname"]').val();

        if ($('[name="'+value+'_image"]').is(":checked") === true) {
            if (file_fname == "" || typeof file_fname === "undefined")
            {
                $('[name="'+value+'_image"]').prop("checked", false);
                alert("선택된 파일이 없습니다.");
                return false;
            } else {
                if ($(".thumbnail_image:checked").length > 1) {
                    $('[name="'+value+'_image"]').prop("checked", false);
                }else {
                    $('[name="'+value+'_image"]').prop("checked", true);
                    $('[name="'+value+'_image"]').val(file_fname);
                }
            }
        }
    }
</script>
	<form name="frm" id="frm">
		<fieldset>
			<legend>게시글 작성</legend>
			<input type="hidden" name="write_userid" value="" />
			<input type="hidden" name="code" value="recruit" />
			<input type="hidden" name="mode" value="write" />
			<input type="hidden" name="no" value="" />
			<input type="hidden" name="cref" value="" />
			<input type="hidden" name="upload_path" value="" />
			<!-- 메인에서 게시글 작성시 사용하는 폼 -->
			<!-- 게시글 작성 페이지에서 게시글 작성시 사용하는 폼 -->

			<table class="bbs_write"  summary="게시글 작성, 제목, 작성자, 내용, 파일첨부 등등..">
				<caption>게시글 작성</caption>
				<colgroup>
					<col width="15%">
					<col width="85%">
				</colgroup>
				<tbody>
					<tr>
						<th scope="row">제목</th>
						<td><input type="text" name="title" id="title" value="" /><label for="title" class="dn">제목</label></td>
					</tr>
					<tr>
						<th scope="row">이름</th>
						<td><input type="text" name="name" id="name" value="" /><label for="name" class="dn">이름</label></td>
					</tr>										
					<tr>
						<th scope="row">이메일</th>
						<td>
							<input type="text" name="writer_email" id="writer_email" /><label for="writer_email" class="dn">이메일</label>
						</td>
					</tr>

					<tr>
						<th scope="row">내용</th>
						<td>							
							<div class="edit-box" style="width:100%;"><textarea name="content" id="contents" style="height:320px" title="내용을 입력하세요."></textarea></div>
						</td>
					</tr>
					<tr>
						<td colspan="4">
							<!-- 개인정보 수집항목 동의 -->
							<div class="policy_cont">
								<div>
									<input type="checkbox" name="nonMember" id="checkbox-nonMember" />
									<label for="checkbox-nonMember">(비회원) 개인정보 수집항목 동의</label>
									<a href="${pageContext.request.contextPath}/member.do?code=agreement" target="_blank" class="btn_sm btn_info">전체보기 ></a>
								</div>
								<textarea cols="30" rows="5" align="left" class="" title="개인정보 수집항목 동의">'㈜젯스퍼트'는 (이하 '회사'는) 고객님의 개인정보를 중요시하며, &quot;정보통신망 이용촉진 및 정보보호&quot;에 관한 법률을 준수하고 있습니다.

회사는 개인정보취급방침을 통하여 고객님께서 제공하시는 개인정보가 어떠한 용도와 방식으로 이용되고 있으며, 개인정보보호를 위해 어떠한 조치가 취해지고 있는지 알려드립니다.

회사는 개인정보취급방침을 개정하는 경우 웹사이트 공지사항(또는 개별공지)을 통하여 공지할 것입니다.

■ 수집하는 개인정보 항목
 회사는 회원가입, 상담, 서비스 신청 등등을 위해 아래와 같은 개인정보를 수집하고 있습니다.

ο 수집항목 : 이름 , 로그인ID , 비밀번호 , 휴대전화번호 , 이메일 , 주민등록번호 , 행사일
ο 개인정보 수집방법 : 홈페이지(회원가입) 

■ 개인정보의 수집 및 이용목적
 회사는 수집한 개인정보를 다음의 목적을 위해 활용합니다..

ο 서비스 제공에 관한 계약 이행 및 서비스 제공에 따른 요금정산 콘텐츠 제공
ο 회원 관리
 회원제 서비스 이용에 따른 본인확인 , 개인 식별 , 불량회원의 부정 이용 방지와 비인가 사용 방지 , 가입 의사 확인 , 연령확인 , 만14세 미만 아동 개인정보 수집 시 법정 대리인 동의여부 확인 , 불만처리 등 민원처리 , 고지사항 전달
ο 마케팅 및 광고에 활용
 신규 서비스(제품) 개발 및 특화 , 이벤트 등 광고성 정보 전달 , 인구통계학적 특성에 따른 서비스 제공 및 광고 게재

■ 개인정보의 보유 및 이용기간
 회사는 개인정보 수집 및 이용목적이 달성된 후에는 예외 없이 해당 정보를 지체 없이 파기합니다.

귀하께서는 회사의 서비스를 이용하시며 발생하는 모든 개인정보보호 관련 민원을 개인정보관리책임자 혹은 담당부서로 신고하실 수 있습니다.
회사는 이용자들의 신고사항에 대해 신속하게 충분한 답변을 드릴 것입니다.
기타 개인정보침해에 대한 신고나 상담이 필요하신 경우에는 아래 기관에 문의하시기 바랍니다.

1.개인분쟁조정위원회 (www.1336.or.kr / 1336)
 2.정보보호마크인증위원회 (http://www.eprivacy.or.kr / 02-580-0533~4)
 3.대검찰청 인터넷범죄수사센터 (http://http://icic.sppo.go.kr / 02-3480-3600)
 4.경찰청 사이버테러대응센터 (http://www.ctrc.go.kr / 02-392-0330)</textarea>								
							</div><!-- .policy_cont -->
						</td>
					</tr>
				</tbody>
			</table><!--board_write-->
			<div class="btn_wrap ta_center">
				<button onclick="fn_insertRecruitBoard(); return false;"><a href="javascript://" class="btn btn_point">문의</a></button>				
			</div><!--btn_center-->

		</fieldset>
	</form>
	</div><!-- .sub_cont -->
</div><!--content_sub-->
<!-- ** layout정리190513  ** -->
					<!-- #서브 컨텐츠 끝 -->

				</div><!-- #contents -->
				<!-- 컨텐츠 끝 #contents -->

			</div><!-- #contents_box -->

		</div><!-- #contents_wrap -->
	</div><!-- #container -->
<!-- ** layout정리190513  ** -->

<%@ include file="/WEB-INF/include/include-body.jsp" %>
			<script src="resources/js/common_form.js" charset="utf-8"/></script>
			<script type="text/javascript"> 
				
			$(document).ready(function(){ 							
				
				$("#write").on("click", function(e){ //목록으로 버튼
					e.preventDefault();
					fn_insertBoard();
				});
											
			}); 
						
			function fn_insertRecruitBoard(){
				var comSubmit = new ComSubmit("frm");
				comSubmit.setUrl("<c:url value='/insertRecruitBoard.do?code=recruit' />");			
				oEditors.getById["contents"].exec("UPDATE_CONTENTS_FIELD", []);
				
				var ir1 = $("[name='content']", form).val();

		        if( ir1 == ""  || ir1 == null || ir1 == '&nbsp;' || ir1 == '<p>&nbsp;</p>')  {
		             alert("내용을 입력하세요.");
		             oEditors.getById["contents"].exec("FOCUS"); //포커싱
		             return;
		        }
		        
				comSubmit.submit();
			}
				
			</script>
			
	<!-- include footer -->
<%@ include file="/WEB-INF/include/footer.jsp" %>

</article><!-- #wrap 전체영역 -->
<iframe name="ifr_processor" id="ifr_processor" title="&nbsp;" width="0" height="0" frameborder="0" style="display:none;"></iframe>
</body>
</html>
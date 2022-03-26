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
									Q&A 작성 페이지 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="/board/board_list?code=notice">community</a> ></li>
								<li>
									<strong>

												<a href="/board/board_list?code=recruit">recruit<!-- 게시판명 노출 --></a>

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


<script type="text/javascript" src="resources/js/common_board.js"></script>
<script type="text/javascript" src="resources/lib/se2.8.2/js/HuskyEZCreator.js" charset="utf-8"></script>
<script>

	// 파일 현재 필드 숫자 totalCount랑 비교값
	var fileCount = 0;
	// 해당 숫자를 수정하여 전체 업로드 갯수를 정한다.
	var totalCount = 3;
	// 파일 고유넘버
	var fileNum = 0;
	// 첨부파일 배열
	var content_files = new Array();
	// 첨부파일 배열
	var delete_files = new Array();
	var allFileSize = 0;

	$(document).ready(function(){ 
			
		$("#mailfile").on("change", fileCheck);
		
	}); 
	
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
	           sSkinURI: "resources/lib/se2.8.2/SmartEditor2Skin.html",  
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
				name : {required : true},
				password : {required : true, rangelength : [4, 20]},
//메인에서 에디터 적용금지
				//content : {required : true},
				file : {},
				nonMember : {required : {depends : function(){return true}}},
			}, messages : {
				title : {required : "제목을 입력해주세요."},
				name : {required : "작성자를 입력해주세요."},
				password : {required : "비밀번호를 입력해주세요.", rangelength: $.validator.format("비밀번호는 {0}~{1}자입니다.")},
//메인에서 에디터 적용금지
				//content : {required : "내용을 입력해주세요."},
				file : {},
//메인에서 태그 가져오지 못하는 오류 수정
				nonMember : {required : "(비회원) 개인정보 수집항목 동의를 체크해주세요."},
			}
		});

//메인에서 에디터 적용금지
		//attachSmartEditor("contents", "board");
		//uploadForm.init(document.frm);
		
	 });
	 
	 function fn_insertBoard(form)
	 {
		 if(!$(form).valid()){
				return false;
			}
		 
		 if(allFileSize > 10000000 ){
			 alert("파일 크기는 최대 10MB를 넘을 수 없습니다." + allFileSize);
			 return false;
			 }
		 
		 	 form.arrDeleteFiles.value = delete_files;
			 //form.action = "<c:url value='/insertBoard.do?' />";	
			 //form.method = "post";
			 oEditors.getById["contents"].exec("UPDATE_CONTENTS_FIELD", []);			 		
		        
			 var ir1 = $("[name='content']", form).val();

		        if( ir1 == ""  || ir1 == null || ir1 == '&nbsp;' || ir1 == '<p>&nbsp;</p>')  {
		             alert("내용을 입력하세요.");
		             oEditors.getById["contents"].exec("FOCUS"); //포커싱
		             return;
		        }
		                		
			 //form.submit();
	        ajax_insertBoard(form);
		}
		
	 /*
	  * 폼 submit 로직
	  */
	 	function ajax_insertBoard(form){
	 		
	 	//var form = $("frm")[0];        
	  	var formData = new FormData(form);
	 		for (var x = 0; x < content_files.length; x++) {
	 			// 삭제 안한것만 담아 준다. 
				if(!content_files[x].is_delete){
 				 formData.append("article_file", content_files[x]);	 
				}
	 		}
	    /*
	    * 파일업로드 multiple ajax처리
	    */    
	 	$.ajax({
	    	      type: "POST",
	    	   	  enctype: "multipart/form-data",
	    	      url: "insertBoard.do",
	        	  data : formData,
	        	  processData: false,
	    	      contentType: false,
	    	      success: function (data) {
	    	    	if(JSON.parse(data)['result'] == "OK"){
	    	    		alert("파일업로드 성공");
	    	    		location.href = "${pageContext.request.contextPath}/community.do?code=qna";
	 				} else
	 				alert("서버내 오류로 처리가 지연되고있습니다. 잠시 후 다시 시도해주세요");
	    	    	
	    	      },
	    	      error: function (xhr, status, error) {
	    	    	alert("서버오류로 지연되고있습니다. 잠시 후 다시 시도해주시기 바랍니다.");
	    	     return false;
	    	      }
	    	    });
	    	    return false;
	 	}
	 
	function fn_updateNotice(form)
	{
		if(!$(form).valid()){
			return false;
		}
		var oldfilesize = 0;
		
		var tableData = document.getElementById('selectedFileTable');
		
		for(var i=1; i<tableData.rows.length;i++){
			//alert(tabledata.rows[i].cells[4].innerHTML);
			oldfilesize += parseInt(tableData.rows[i].cells[4].innerText);
		}
		
		if(allFileSize + oldfilesize > 10000000 ){
			 alert("파일 크기는 최대 10MB를 넘을 수 없습니다." + allFileSize);
			 return false;
			 }
		
		//registerAction();
		
		form.arrDeleteFiles.value = delete_files;
		//form.action = "<c:url value='/updateQna.do?' />";			
		//form.method = "post";
	 	oEditors.getById["contents"].exec("UPDATE_CONTENTS_FIELD", []);
	 	
 	 	var ir1 = $("[name='content']", form).val();

        if( ir1 == ""  || ir1 == null || ir1 == '&nbsp;' || ir1 == '<p>&nbsp;</p>')  {
             alert("내용을 입력하세요.");
             oEditors.getById["contents"].exec("FOCUS"); //포커싱
             return;
        }
		//form.submit();
        ajax_updateBoard(form);
	}		

	/*
	  * 폼 submit 로직
	  */
	 	function ajax_updateBoard(form){
	 		
	 	//var form = $("frm")[0];        
	  	var formData = new FormData(form);
	 		for (var x = 0; x < content_files.length; x++) {
	 		// 삭제 안한것만 담아 준다. 
				if(!content_files[x].is_delete){ 
	 				formData.append("article_file", content_files[x]);
				}
	 		}
	    /*
	    * 파일업로드 multiple ajax처리
	    */    
	 	$.ajax({
	    	      type: "POST",
	    	   	  enctype: "multipart/form-data",
	    	      url: "updateQna.do",
	        	  data : formData,
	        	  processData: false,
	    	      contentType: false,
	    	      success: function (data) {
	    	    	if(JSON.parse(data)['result'] == "OK"){
	    	    		alert("파일업로드 성공");
	    	    		location.href = "${pageContext.request.contextPath}/boardView.do?code=qna&no=${qna.post_number}";
	 				} else
	 				alert("서버내 오류로 처리가 지연되고있습니다. 잠시 후 다시 시도해주세요");
	    	    	
	    	      },
	    	      error: function (xhr, status, error) {
	    	    	alert("서버오류로 지연되고있습니다. 잠시 후 다시 시도해주시기 바랍니다.");
	    	     return false;
	    	      }
	    	    });
	    	    return false;
	}
	
	/**
	 * 첨부파일로직
	 */
	$(function () {
	    $('#btnSelectFile').click(function (e) {
	        e.preventDefault();
	        $('#mailfile').click();
	    });
	});
	
	function fn_openBoardList(){
		var comSubmit = new ComSubmit();
		comSubmit.setUrl("<c:url value='/community.do?code=${code}' />");					
		comSubmit.submit();
	}
	
	//오브젝트 가져오기
	function Elem(id) {
		return document.getElementById(id);
	}
	
	function fileCheck(e) {
		
		var mFiles;
	    
	    if(e != null && e.target == undefined) {
		    mFiles = e;
	        }
		else if(e.target.files != null) {
	            mFiles = e.target.files;
	        }
		else if(e.originalEvent != null) {
		    mFiles = e.originalEvent.dataTransfer.files;
		}
		
	    if(mFiles == null) {
			return;
		}
	    
	    var Html = "";	
		var totalsize = 0;
		
	    if(mFiles.length > 0 && content_files.length == 0){
	    	
	    	Html = "<table id='uploadFileTable' width='100%' class='FileTable'>";
	    	Html += '<tbody>';	  
	    		    	
	    	
	    	for (var i=0; i < mFiles.length; i++){
	    		if(mFiles[i].size <= 10000000 && totalsize <= 10000000  && (mFiles[i].size + totalsize) <= 10000000) {
	    			totalsize = totalsize + mFiles[i].size;
	    			content_files.push(mFiles[i]);
	    			
	    			Html += MakeTableRowHtml(mFiles[i].name, mFiles[i].size, i);
	    		}
	    		else{
	    			 alert("파일 크기는 최대 10MB를 넘을 수 없습니다." + allFileSize);
	    			 continue;
	    		}
	    	}
	    	
	    	if(totalsize > 0) {
	    		Elem("divFileTable").innerHTML = Html + "</table>";
	    		Elem("divFileArea").style.display = "";
                Elem("divDropArea").style.display = "none";
	    	}
	    }
	    else {
	    	
	    	for (var i=0; i < content_files.length; i++) {
	    		totalsize = totalsize + content_files[i].size;
	    	    }
	    	
	    	for (var i=0; i < mFiles.length; i++){

	    		var overlap = false;

                for(var j=0; j < content_files.length; j++) {

                    if(mFiles[i].name == content_files[j].name && mFiles[i].type == content_files[j].type && mFiles[i].size == content_files[j].size ) {
                        overlap = true;
                        break;
                    }
                }
                
                if(mFiles[i].size <= 10000000 && totalsize <= 10000000 && mFiles[i].size + totalsize <= 10000000 && overlap == false) {

                	content_files.push(mFiles[i]);
        		    totalsize = totalsize + mFiles[i].size;

        		    var uft = Elem("uploadFileTable");
        		    var row_index = content_files.length + i;
        		    
                    var row = uft.insertRow(uft.rows.length);
                    	row.id = "file" + row_index;
                		row.className = "file" + row_index;
                    var cell1 = row.insertCell(0);
                    	cell1.className = "delete";
                    var cell2 = row.insertCell(1);
                    	cell2.className = "name string";
                        
                   	cell1.innerHTML = '<input type="button" name="chkDelete" value="X" style="background-color: rgba(0,0,0,0);border: 1px solid white;" onclick="delNewRow('+ row_index + ",'" + mFiles[i].name + "'" + ')">';
                   	cell2.innerHTML = mFiles[i].name + " ("+bytesToSize(mFiles[i].size)+")" ;
                    
                    var _lastDot = mFiles[i].name.lastIndexOf('.');
                    var _filelen = mFiles[i].name.length;
                    
                    //cell3.innerHTML = mFiles[i].name.substring(_lastDot,_filelen).toLowerCase();
                    //cell4.innerHTML = "<input type='button' name='chkDelete' value=\"" + i + "\"> </td>";			
                    
                }
                else{
	    			 alert("파일 크기는 최대 10MB를 넘을 수 없습니다.");
	    			 continue;
	    		}
    		}	   
	  	}
	    allFileSize = totalsize;
		$("#mailfile").val("");
		//registerAction();
	}
	
	//파일리스트 td 생성
	function MakeTableRowHtml(fileName, fileSize, i){

		var Html = "";
		
		var _lastDot = fileName.lastIndexOf('.');
		var _filelen = fileName.length;
		
		Html = "<tr id=file" + i + " class=file" + i + ">";		
		Html += '<td class="delete" style="width: 2%"> <input type="button" name="chkDelete" value="X" style="background-color: rgba(0,0,0,0);border: 1px solid white;" onclick="delNewRow('+ i + ",'" + fileName + "'" + ')"></td>';
		Html += "<td class='name string' style='width: 98%'>" + fileName + " ("+bytesToSize(fileSize)+")" +"</td>";		
		Html += "</tr>";
		return Html;
	}
				
	function bytesToSize(bytes) {
	    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
	    if (bytes == 0) return '';
	    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
	    if (i == 0) return bytes + ' ' + sizes[i];
	    return (bytes / Math.pow(1024, i)).toFixed(1) + ' ' + sizes[i];
	};


	function delRow(no,fileNo){			
		delete_files.push(fileNo);			
		$('#sfile' + no).remove();
	}
	
	function delNewRow(no,fileName){
		content_files[no].is_delete = true;
		//delete_files.push(fileName);			
		$('#file' + no).remove();
		console.log(content_files);
	}
	
</script>


	<form name="frm" id="frm" encType="multipart/form-data">
		<fieldset>
			<legend>게시글 작성</legend>
			<input type="hidden" name="write_userid" value="" />
			<input type="hidden" name="code" value="qna" />
			<input type="hidden" name="mode" value="write" />
			<input type="hidden" name="no" value="${qna.post_number}" />
			<input type="hidden" name="cref" value="" />
			<input type="hidden" name="arrDeleteFiles" value="" />
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
						<td><input type="text" name="title" id="title" value="${qna.post_title}" /><label for="title" class="dn">제목</label></td>
					</tr>
					<tr>
						<th scope="row">이름</th>
						<td><input type="text" name="name" id="name" value="${qna.post_writer}" /><label for="name" class="dn">이름</label></td>
					</tr>
					<!-- 글작성, 글답변작성 시 비회원유저 -->
					<tr>
						<th scope="row">비밀번호</th>
						<td>
							<input type="password" name="password" id="password" value="${qna.post_passwd}"/><label for="password" class="dn">게시글 비밀번호</label>
						</td>
					</tr>

					<tr>
						<th scope="row">내용</th>
						<td>
							<input type="checkbox" name="is_secret" id="is_secret-y" value="y" /><label for="is_secret-y">비밀글로 작성</label>
							<div class="edit-box" style="width:100%;"><textarea name="content" id="contents" style="height:320px; display: none;" title="내용을 입력하세요.">${qna.post}</textarea>							
							</div>
						</td>
					</tr>
					<tr>
						<th scope="row">첨부 파일</th>
						<td >								
							<div class="file_select">																			
								<input type="file" class="input" id="mailfile" name="mailfile" multiple="multiple" style="display:none">	
								<input type="button" value="내 PC" id="btnSelectFile" class="btnSelectFile" style="background-color:white;border:1px solid lightgray;padding-top: 4px;padding-bottom: 4px;padding-left: 10px;padding-right: 10px;font-size:12px">										
							</div>							
							<!-- S://드래그 파일존 -->
							<div class="file_drag" id="divMain">
								<table id="selectedFileTable" class="FileTable">
									<c:choose>
										<c:when test="${fn:length(fileList) > 0}">
											<c:forEach items="${fileList}" var="row" varStatus="status">
												<tr id="sfile${status.count}" class="file">								
													<td class="fileNo" style="display:none">${row.file_number}</td>
													<td style="display:none">${status.count}</td>													
													<td class="delete" style='width: 2%;'> <input type="button" name="chkDelete" value="X" style="background-color: rgba(0,0,0,0);border: 1px solid white;" onclick="delRow(${status.count},'${row.file_number}')"></td>
													<td class='name string' style='width: 98%; '>${row.org_file_name} (${row.file_size})</td>
																																			
												</tr>						
											</c:forEach>
										</c:when>
										<c:otherwise>
										</c:otherwise>
									</c:choose>							
								</table>

								<!-- 첨부파일 없을때 -->
								<div class="dropbox" id="divDropArea">
									<div class="dropbox_message"></div>
								</div>
								<!-- 첨부파일 있을 때 -->
								<div class="file_list" id="divFileArea" style="display: none">
									<div id="divFileTable"></div>
								</div>
								<!-- E://첨부파일 있을 때 -->
							</div>
							<!-- E://file_drag -->
						
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
				<c:choose>
					<c:when test="${qna.post_title eq null}">						
						<button onclick="fn_insertBoard(this.form); return false;"><a href="" class="btn btn_point">확인</a></button>
					</c:when>
					<c:otherwise>
						<button onclick="fn_updateNotice(this.form); return false;"><a href="javascript://" class="btn btn_point">확인</a></button>
					</c:otherwise>
				</c:choose>				
				<button ><a href="${pageContext.request.contextPath}/community.do?code=qna" class="btn btn_basic">취소</a></button>
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
		
		$("#list").on("click", function(e){ //목록으로 버튼
			e.preventDefault();
			fn_openBoardList();
		});
		
		$("#write").on("click", function(e){ //목록으로 버튼
			e.preventDefault();
			fn_insertBoard();
		});
		
	
	}); 
	
	</script>			

	<!-- include footer -->
<%@ include file="/WEB-INF/include/footer.jsp" %>

</article><!-- #wrap 전체영역 -->
<iframe name="ifr_processor" id="ifr_processor" title="&nbsp;" width="0" height="0" frameborder="0" style="display:none;"></iframe>
</body>
</html>
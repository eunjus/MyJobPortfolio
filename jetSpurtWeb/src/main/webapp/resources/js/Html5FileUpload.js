var ouploadFiles;
var normalSize = 0;
var normailCount = 0;
var hugSize = 0;
var hugSize = 0;
var success_cnt = 0;
var failed_cnt = 0;
var fileUploadComplete = false;
var fileUploadSuccess;
var fileUpCheck;
var allFileSize = 0;
var allSendFileSize = 0;
var allSendProgress =  Elem("all_upload_progressbar");
var allProgressPercent = Elem("all_upload_progress_percent");


//페이지 로드시 초기화 
function Html5FileUploadInit() {
	/*var br = navigator.userAgent;
	var trident = br.match(/Trident\/(\d.\d)/i);

	if(trident != null) {

	    if(trident[1] == "7.0" || trident[1] == "6.0") {

		ieVersion1011 = true;
	    }
	}*/

	var form_obj = Elem("mail_compose");

	fileUpCheck = setInterval(function () {

  	    if(ouploadFiles != null) {
                if(fileUploadComplete) {
                    if(fileUploadSuccess && failed_cnt ==0) {

			jQuery("#close_file_upload_btn").click();
						jQuery("#send_modal").click();
		        allSendFileSize = 0;
		        allSendProgress.style.width = 0 + "%";
		        allProgressPercent.innerHTML = "";
			clearFileObject();
   	                form_obj.submit();
                        clearInterval(fileUpCheck);

                    }
                    else {
                        //clearInterval(fileUpCheck);
						//clearInterval(1);
                        //alert("파일 업로드에 실패하였습니다.");

						jQuery(".progress-bar").width("");
						jQuery("#all_upload_progress_percent").text('');
						allSendFileSize = 0;
						allSendProgress.style.width = 0 + "%";
						allProgressPercent.innerHTML = "";
						jQuery("#close_file_upload_btn").click();

			//jQuery("#divFileTableModal").dialog("close");
			//jQuery("#send_modal").dialog("close");
			//jQuery("#div_mail_header").show();
		    //jQuery("#div_mail_compose").show();
			//jQuery("#mailwrite_notice").show();
			fileUploadComplete = false;
						failed_cnt=0;

						clearInterval(1);
						alert("파일 업로드에 실패하였습니다.");


						return false;

                    }
                }
	    }

        }, 1);

	var fileupload = jQuery("#mailfile");
	fileupload.on('change', FileSelectHandler);

	var filedragMain = jQuery("#divMain");

	filedragMain.on('dragenter', FileDragHover);
	filedragMain.on('dragover', FileDragHover);
	filedragMain.on('drop', FileDrop);

	jQuery(document).on('dragenter', function (e)
	{
	    e.stopPropagation();
	    e.preventDefault();
	});

	jQuery(document).on('dragover', function (e)
	{
	    e.stopPropagation();
	    e.preventDefault();
	});

	jQuery(document).on('drop', function (e)
	{
	    e.stopPropagation();
	    e.preventDefault();
	});

}

//파일 선택 버튼 클릭 시 이벤트
function selectFile() {

	Elem("mailfile").click();
}

//파일 드레그
function FileDragHover(e) {
	e.stopPropagation();
	e.preventDefault();
	//e.target.className = (e.type == "dragover") ? "hover" : "";
}

//파일 드롭
function FileDrop(e) {

    FileDragHover(e);
    FileSelectHandler(e);
}


function MdrvieFileSelect(filelist) {

    var url = '';

    //url 정보를 가져온다.
    url = get_webfolder_url();

     //wedbdav에서 파일을 가져온다.
    var filecnt = filelist.length;

    for(i=0; i < filecnt; i++) {

	GetWebFolderFile(url, filelist[i]);
    }
}

function GetWebFolderFile(url, f) {
    var xhr = new XMLHttpRequest();
    var file = [];

    var filename = f.replace(/.*\/(.*)/, '$1');
    xhr.open('GET', url + f, true);
    xhr.responseType = 'blob';

    // 파일 전송 성공/실패
    xhr.onreadystatechange = function(e) {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                    var blobObject = xhr.response;
                        if(is_msie) {

                            blobObject.lastModifiedDate = new Date();
                            blobObject.name = filename;

                            file.push(blobObject);

                        }
                        else {

                            //file = new File([blobObject], 'jdk-8u92-windows-x64.exe');
                            file.push(new File([blobObject], filename));
                        }

                        FileSelectHandler(file);
            }
            else {

                alert('웹폴더에서 파일을 가져올 수 없습니다.');
            }

        }
    };

    xhr.send();

}


//파일 선택 및 선택한 파일 테이블 생성
function FileSelectHandler(e) {
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
	var PHtml = "";
	var totalsize = 0;
	var attmode = "";

	if(ouploadFiles == null) {

	    normalSize = 0;
	    normalCount = 0;
	    hugSize = 0;
	    hugCount = 0;

	    Html = "<table id='uploadFileTable' width='100%' class='no-margin-b'>";
	    PHtml = "<table id='uploadFileTable_P' width='100%' class='no-margin-b'>";

	    ouploadFiles = [];

	    for (var i=0; i < mFiles.length; i++){


		if(i < 50 && mFiles[i].size <= 2147483648 && totalsize <= 5368709120 && (mFiles[i].size + totalsize) <= 5368709120) {

		    totalsize = totalsize + mFiles[i].size;
		    ouploadFiles.push(mFiles[i]);

		    if(mFiles[i].size > 10485760 || (normalSize + mFiles[i].size) > 52428800) {
		        hugSize = hugSize +  mFiles[i].size;
			hugCount = hugCount + 1;
			attmode = "L";

		    } else {
			attmode = "N";
	    		normalSize = normalSize + mFiles[i].size;
			normalCount = normalCount + 1;
		    }

                    Html += MakeTableRowHtml(mFiles[i].name, mFiles[i].size, i, attmode, "N");
		    PHtml += MakeTableRowHtml(mFiles[i].name, mFiles[i].size, i, attmode,"P");
		}
            }

	    if(totalsize > 0) {

                Elem("divFileTable").innerHTML = Html + "</table>";
                //Elem("divFileTableModal").innerHTML = PHtml + "</table>";
                Elem("divFileArea").style.display = "";
                Elem("divDropArea").style.display = "none";
                Elem("btnDeleteFileAll").style.display = "";
                Elem("btnDeleteFile").style.display = "";
                Elem("btnUploadFile").style.display = "";

		allFileSize = totalsize;
	    }
	    else {
		ouploadFiles = null;
	    }

	}
	else {

	    for (var i=0; i < ouploadFiles.length; i++) {
		totalsize = totalsize + ouploadFiles[i].size;
	    }

	    for (var i=0; i < mFiles.length; i++){

		var overlap = false;

                for(var j=0; j < ouploadFiles.length; j++) {

                    if(mFiles[i].name == ouploadFiles[j].name && mFiles[i].type == ouploadFiles[j].type && mFiles[i].size == ouploadFiles[j].size) {
                        overlap = true;
                        break;
                    }
                }

		if(i < 50 && mFiles[i].size <= 2147483648 && totalsize <= 5368709120 && mFiles[i].size + totalsize <= 5368709120 && overlap == false) {

		    totalsize = totalsize + mFiles[i].size;

		    var ck = document.getElementsByName("chkDelete");
                    var cv = Number(ck[ouploadFiles.length - 1].value) + 1;

                    var uft = Elem("uploadFileTable");
                    var row = uft.insertRow(uft.rows.length);
                    var cell1 = row.insertCell(0);
                        cell1.className = "check";
                    var cell2 = row.insertCell(1);
                        cell2.className = "name string";
                    var cell3 = row.insertCell(2);
                        cell3.className = "capacity";
                    var cell4 = row.insertCell(3);
                        cell4.className = "attach_type";
                    var cell5 = row.insertCell(4);
                        cell5.id = "progressBar" + cv;
                        cell5.className = "status";


                    cell1.innerHTML = "<input type=checkbox name=chkDelete value=\"" + cv  + "\" class='no-margin-r'>";
		    var icon = iconCheck(mFiles[i].name);
                    cell2.innerHTML =  "<span class='icon " + icon +"'>파일아이콘</span>" + mFiles[i].name;
                    cell3.innerHTML = "<span id='attsize" + cv +"' value='" + mFiles[i].size + "'>" + sizeToString(mFiles[i].size) + "</span>";

                    if(mFiles[i].size >  10485760 || (normalSize + mFiles[i].size) > 52428800) {
                        hugSize = hugSize +  mFiles[i].size;
			hugCount = hugCount + 1;

			attmode = "L";

			if(normalSize + mFiles[i].size > 52428800) {

			    cell4.innerHTML = "<span id='attmode" + cv + "' value='L'>" + file_upload_msg3 + "(" + file_upload_msg4 + ")</span>";
			}
		 	else {

			    cell4.innerHTML = "<span id='attmode" + cv + "' value='L'>" + file_upload_msg3 + "</span><button type='button' class='btn btn-outline btn-xs' onclick=changeUploadMode('" + cv + "')>" + file_upload_msg5 + "</button>";
			}
                    }
                    else {
                        normalSize = normalSize + mFiles[i].size;
			normalCount = normalCount + 1;
			attmode = "N";
                        cell4.innerHTML = "<span id='attmode" + cv + "' value='N'>" + file_upload_msg2 + "</span><button type='button' class='btn btn-outline btn-xs' onclick=changeUploadMode('"+ cv +"')>" + file_upload_msg5 + "</button>";
		    }

                    cell5.innerHTML = file_upload_msg6;


		    uft = Elem("uploadFileTable_P");
                    row = uft.insertRow(uft.rows.length);
                    cell1 = row.insertCell(0);
                    cell1.className = "name string";
                    cell2 = row.insertCell(1);
                    cell2.className = "up_progress";
                    cell3 = row.insertCell(2);
		    cell3.id = "progress_status" + cv;
                    cell3.className = "status";

                    cell1.innerHTML =  "<span class='icon "+ icon + "'>파일아이콘</span>" + mFiles[i].name;
                    cell2.innerHTML = "<div class='progress no-margin-b'><div class='progress-bar' id='progressBar_P"+cv+"'></div></div><span class='number'><span id='progress_percent"+cv+"'></span> / "+ sizeToString(mFiles[i].size) +"</span>";
                    cell3.innerHTML = file_upload_msg6;
		    ouploadFiles.splice(ouploadFiles.length,1,mFiles[i]);

		}
            }
	}

	allFileSize = totalsize;

	Elem("all_upload_filesize").innerHTML = sizeToString(allFileSize);
	Elem("all_upload_filecnt").innerHTML = ouploadFiles.length + "개";
	Elem("html5normalSize").innerHTML = sizeToString(normalSize);
        Elem("html5hugSize").innerHTML = sizeToString(hugSize);
        Elem("html5normalCount").innerHTML = normalCount + " EA";
        Elem("html5hugCount").innerHTML = hugCount + " EA";

}

//파일리스트 td 생성
function MakeTableRowHtml(fileName, fileSize, i, attmode, Htmltype){

	var Html = "";
	var icon = iconCheck(fileName);

	if(attmode == "L") {

	    if(Htmltype == "N") {

		Html = "<tr>";
	        Html += "<td class='check'><input type='checkbox' name='chkDelete' value=\"" + i + "\" class='no-margin-r'> </td>";
		Html += "<td class='name string'><span class='icon " + icon +"'>파일아이콘</span>" + fileName + "</td>";
		Html += "<td class='capacity'><span id='attsize" + i +"' value='" + fileSize + "'>" + sizeToString(fileSize) + "</span></td>";

		if(fileSize + normalSize > 52428800) {
		    Html += "<td class='attach_type'><span id='attmode" + i + "' value='L'>" + file_upload_msg3 + "(" + file_upload_msg4 + ")</span></td>";
		}
		else {
		    Html += "<td class='attach_type'><span id='attmode" + i +"' value='L'>" + file_upload_msg3 + "</span><button type='button' class='btn btn-outline btn-xs' onclick=changeUploadMode('"+ i +"')>" + file_upload_msg5 + "</button></td>";

		}

		Html += "<td id=progressBar"+i+" class='status'>" + file_upload_msg6 + "</td>";
	    }
	    else {

	        Html = "<tr>";
	        Html += "<td class='name string'><span class='icon "+icon+"'>파일아이콘</span>" + fileName + "</td>";
	        Html += "<td class='up_progress'><div class='progress no-margin-b'><div class='progress-bar' id='progressBar_P"+i+"'></div></div><span class='number'><span id='progress_percent"+i+"'></span> / "+ sizeToString(fileSize) +"</span></td>";
        	Html += "<td class='status' id='progress_status"+i+"'>" + file_upload_msg6 + "</td>";

	    }
	}
	else {

	    if(Htmltype == "N") {

		Html = "<tr>";
                Html += "<td class='check'><input type='checkbox' name='chkDelete' value=\"" + i + "\" class='no-margin-r'> </td>";
                Html += "<td class='name string'><span class='icon " + icon +"'>파일아이콘</span>" + fileName + "</td>";
                Html += "<td class='capacity'><span id='attsize" + i +"' value='" + fileSize + "'>" + sizeToString(fileSize) + "</span></td>";

                Html += "<td class='attach_type'><span id='attmode" + i +"' value='N'>" + file_upload_msg2 + "</span><button type='button' class='btn btn-outline btn-xs' onclick=changeUploadMode('"+ i +"')>" + file_upload_msg5 + "</button></td>";
                Html += "<td id=progressBar"+i+" class='status'>" + file_upload_msg6 + "</td>";

	    }
	    else {

		Html = "<tr>";
                Html += "<td class='name string'><span class='icon "+icon+"'>파일아이콘</span>" + fileName + "</td>";
                Html += "<td class='up_progress'><div class='progress no-margin-b'><div class='progress-bar' id='progressBar_P"+i+"'></div></div><span class='number'><span id='progress_percent"+i+"'></span> / "+ sizeToString(fileSize) +"</span></td>";
                Html += "<td class='status' id='progress_status"+i+"'>" + file_upload_msg6 + "</td>";

	    }
	}

	Html += "</tr>";
	return Html;
}

//첨부 전환 
function changeUploadMode(obj) {
	var elem_mode = Elem("attmode" + obj);
	var elem_size = Elem("attsize" + obj);
	//var elem_mode_p = Elem("attmode_P" + obj);
        var mode_val = elem_mode.getAttribute("value");
	var size_val = Number(elem_size.getAttribute("value"));

	if(mode_val == "N") {
	    elem_mode.setAttribute("value", "L");
	    elem_mode.innerHTML = file_upload_msg3;
	    //elem_mode_p.setAttribute("value", "L");
            //elem_mode_p.innerHTML = "대용량첨부";

	    normalSize = normalSize - size_val;
	    normalCount = normalCount - 1;
	    hugSize = hugSize + size_val;
	    hugCount = hugCount + 1;
	}
	else {
	    if(size_val + normalSize > 52428800) {

		alert(file_upload_msg7);
		return false;
	    }
	    elem_mode.setAttribute("value", "N");
	    elem_mode.innerHTML = file_upload_msg2;
	    //elem_mode_p.setAttribute("value", "N");
            //elem_mode_p.innerHTML = "일반첨부";
	    normalSize = normalSize + size_val;
	    normalCount = normalCount + 1;
            hugSize = hugSize - size_val;
	    hugCount = hugCount - 1;
	}

	Elem("html5normalSize").innerHTML = sizeToString(normalSize);
        Elem("html5hugSize").innerHTML = sizeToString(hugSize);

	Elem("html5normalCount").innerHTML = normalCount + " EA";
        Elem("html5hugCount").innerHTML = hugCount + " EA";
}

//확장자 체크
function iconCheck(fileName) {

    var extIndex = fileName.lastIndexOf(".");
    var extName = fileName.substring(extIndex + 1, fileName.length);
    extName = extName.toLowerCase();

    switch (extName) {
        case "hwp" :
            break;
        case "txt" :
            break;
        case "pdf" :
            break;
        case "xlsx" :
            break;
	case "xls" :
	    extName = "xlsx";
	    break;
        case "pptx" :
            break;
        case "jpg" :
            break;
        case "doc" :
            break;
	case "docx" :
	    extName = "doc";
	    break;
        case "zip" :
            break;
        default :
            extName = "etc";
            break;
    }
    return extName;
}

//byte를 변환
function sizeToString(size) {

	var length;

	if(size == 0) {
            return size + " KB";
	}

	if (size < 12)
            length = size + " b";
        else if (size < 1024 * 1024)
            length = (size/1024).formatNumber(2,',','.') + " KB";
        else if (size < 1024 * 1024 * 1024)
            length = (size/(1024 * 1024)).formatNumber(2,',','.') + " MB";
        else
            length = (size / (1024 * 1024 * 1024)).formatNumber(2,',','.') + " GB";

        return length;
}

//전체 선택
function DeleteAllChk(chk){
        var chkBoxes = document.getElementsByName("chkDelete");

        for (var i=0; i < chkBoxes.length; i++){

                chkBoxes[i].checked = chk.checked;
        }
}

//전체 파일 리스트 삭제
function DeleteFileAll() {

	Elem("divFileTable").innerHTML = "";
	//Elem("divFileTableModal").innerHTML = "";
    Elem("divFileArea").style.display = "none";
    Elem("divDropArea").style.display = "";
	Elem("btnDeleteFileAll").style.display = "none";
    Elem("btnDeleteFile").style.display = "none";
    Elem("btnUploadFile").style.display = "none";

	ouploadFiles.splice(0, ouploadFiles.length);
	normalSize = 0;
	normalCount = 0;
	hugSize = 0;
	hugCount = 0;
	allFileSize = 0;
	ouploadFiles = null;

	clearFileObject();

	Elem("all_upload_filesize").innerHTML = sizeToString(allFileSize);
        Elem("all_upload_filecnt").innerHTML = "0개";
        Elem("html5normalSize").innerHTML = sizeToString(normalSize);
        Elem("html5hugSize").innerHTML = sizeToString(hugSize);
	Elem("html5normalCount").innerHTML = normalCount + " EA";
        Elem("html5hugCount").innerHTML = hugCount + " EA";
}

//선택한 파일 삭제
function DeleteFile() {
	var dchkbox = document.getElementsByName("chkDelete");

	if(dchkbox.length == 0) {

            Elem("divFileArea").style.display = "none";
            Elem("divDropArea").style.display = "";
	    Elem("btnDeleteFileAll").style.display = "none";
            Elem("btnDeleteFile").style.display = "none";
            Elem("btnUploadFile").style.display = "none";

	    normalSize = 0;
	    normalCount = 0;
            hugSize = 0;
	    hugCount = 0;
	    allFileSize = 0;
	    ouploadFiles = null;

	    Elem("all_upload_filesize").innerHTML = sizeToString(allFileSize);
            Elem("all_upload_filecnt").innerHTML = "0개";
            Elem("html5normalSize").innerHTML = sizeToString(normalSize);
            Elem("html5hugSize").innerHTML = sizeToString(hugSize);
	    Elem("html5normalCount").innerHTML = normalCount + " EA";
            Elem("html5hugCount").innerHTML = hugCount + " EA";

	    return false;
	}

	for(var i=0; i < dchkbox.length; i++) {

		if(dchkbox[i].checked) {

		       if(Elem("attmode" + dchkbox[i].value).getAttribute("value") == "N") {
                            normalSize = normalSize - (ouploadFiles[i].size);
			    normalCount = normalCount - 1;
                        }
                        else {
                            hugSize = hugSize - (ouploadFiles[i].size);
			    hugCount = hugCount - 1;
                        }

			allFileSize = allFileSize - (ouploadFiles[i].size);

			Elem("uploadFileTable").deleteRow(i);
			Elem("uploadFileTable_P").deleteRow(i);
			ouploadFiles.splice(i,1);
			DeleteFile();
		}
	}

	clearFileObject();

	Elem("all_upload_filesize").innerHTML = sizeToString(allFileSize);
        Elem("all_upload_filecnt").innerHTML = ouploadFiles.length + "개";
	Elem("html5normalSize").innerHTML = sizeToString(normalSize);
	Elem("html5hugSize").innerHTML = sizeToString(hugSize);
	Elem("html5normalCount").innerHTML = normalCount + " EA";
        Elem("html5hugCount").innerHTML = hugCount + " EA";
}

function clearFileObject() {

    if(is_msie){
        jQuery("#mailfile").replaceWith( jQuery("#mailfile").clone(true) );
    }
    else {
        jQuery("#mailfile").val("");
    }
}


//파일 업로드 실행
function UploadAction(i) {
	if(i < ouploadFiles.length) {
	    var dchkbox = document.getElementsByName("chkDelete");
	    //AjaxFileUpload(ouploadFiles[i],dchkbox[i].value);

	    if(i == 0) {
  		UploadFile(ouploadFiles[i],dchkbox[i].value, i);
	    }
	    else {
		setTimeout(UploadFile, 1000, ouploadFiles[i], dchkbox[i].value, dchkbox[i].value);
	    }

        } else {

	    //jQuery("#close_file_upload_btn").click();
	    //jQuery("#send_modal").click();
	    fileUploadComplete = "true";
	    return fileUploadSuccess;
	}
}


//업로드 실행 - jquery 테스트
/*function AjaxFileUpload(file, i) {

    var attmode = Elem("attmode_P" + i).getAttribute("value");
    var fd = new FormData();
    fd.append("file", file);
    if(attmode == "N") {
        fd.append("sid", sid);
    }
    else {
        fd.append("sid", ftp_uid + i);
    }

    fd.append("attmode", attmode);
    var obj = Elem("charset");
    fd.append("charset", obj[obj.selectedIndex].value);

    var uploadURL ="/new_mua/index.php/mailfile/file_upload_html5";
    var filexhr = jQuery.ajax({
            xhr: function() {
            var xhrobj = jQuery.ajaxSettings.xhr();
            if (xhrobj.upload) {
		Elem("progressBar_P"+i).innerHTML = "";
                var progress = Elem("progressBar_P" + i).appendChild(document.createElement("div"));

                // 프로그레스 바
                xhrobj.upload.addEventListener('progress', function(e) {
                    var pc = parseInt((e.loaded / e.total) * 100);
                    progress.style.width = pc + "%";
                    progress.innerHTML = parseInt((e.loaded / e.total) * 100) + "%";
                }, false);

		xhrobj.addEventListener('load', function(e) {
		    progress.innerHTML = "완료";
                    fileUploadSuccess = true;
                }, false);

		xhrobj.addEventListener('error', function(e) {
                    progress.innerHTML = "전송실패";
                    fileUploadSuccess = false;
                }, false);
            }
            return xhrobj;
        },
    url: uploadURL,
    type: "POST",
    contentType:false,
    processData: false,
        cache: false,
        data: fd,
        success: function(data){
	    uploadComplete(Number(i)+Number(1));
        }
    });
}*/

//서버로 파일 업로드
function UploadFile(file,elemvalue,i) {
	var xhr = new XMLHttpRequest();
	if (xhr.upload) {

		var old_loaded = 0;
		var attmode = Elem("attmode" + elemvalue).getAttribute("value");
		var progress = Elem("progressBar_P" + elemvalue);
		progress.innerHTML = "";
		var progress_percent = Elem("progress_percent" + elemvalue);
		var progress_status = Elem("progress_status" + elemvalue);


		// 프로그레스 바  
		xhr.upload.addEventListener("progress", function(e) {
			var pc = parseInt((e.loaded / e.total) * 100);
			progress.style.width = pc + "%";
		        progress_percent.innerHTML = parseInt((e.loaded / e.total) * 100) + "%";
		        progress_status.innerHTML = file_upload_msg8;

			allSendFileSize = allSendFileSize + (e.loaded - old_loaded);
		        old_loaded = e.loaded;
		        var allpc = parseInt((allSendFileSize / allFileSize) * 100);
		        allSendProgress.style.width = allpc + "%";
		        allProgressPercent.innerHTML =  allpc + "%";

		}, false);

		// 파일 전송 성공/실패
		xhr.onreadystatechange = function(e) {
			if (xhr.readyState == 4) {


				if(xhr.status == 200 && xhr.responseText =="ok") {
			        //progress.className = "progressBar progressSuccess";
			        progress_status.innerHTML = file_upload_msg9;
					fileUploadSuccess = true;


			    }
			    else {
				//progress.className = "progressBar progressFailed";
                                progress_status.innerHTML = file_upload_msg10;
				fileUploadSuccess = false;
					failed_cnt+=1;
			    }

			    uploadComplete(Number(i)+Number(1));
			}
		};

		//form mode
		var fd = new FormData();
		fd.append("file", file);
	        if(attmode == "N") {
		    fd.append("sid", sid);
		}
		else {
		    fd.append("sid", ftp_uid + i);
		}
		fd.append("attmode", attmode);
		var obj = Elem("charset");
		fd.append("charset", obj[obj.selectedIndex].value);
		fd.append("filename", file.name);

		if(mailnara_server_type == "1") {
		    //메일 서비스
		    if(attmode == "N") { //일반첨부
			xhr.open("POST", "/new_mailnara_web/index.php/mailfile/file_upload_html5", true);
		    }
		    else { //대용량파일첨부
			fd.append("auth_key", auth_key);
			fd.append("user", hug_user);
			fd.append("ksid", sid); //인증키용 sid
			xhr.open("POST", "https://file.mailnara.co.kr/file/file_upload.php", true);
		    }
		}
		else {
		    //독립메일
		    xhr.open("POST", "/new_mailnara_web/index.php/mailfile/file_upload_html5", true);
		}
		xhr.send(fd);
	}

}

function uploadComplete(i) {

    UploadAction(i);
}

function uploadFailed(e) {

    fileUploadSuccess = false;
    failed_cnt = failed_cnt + 1;
    if((success_cnt + failed_cnt) == ouploadFiles.length) {

        fileUploadComplete = true;
    }

    //alert("파일 업로드에 실패하였습니다.\n관리자에게 문의하여 주시기 바랍니다.");
}




//오브젝트 가져오기
function Elem(id) {
	return document.getElementById(id);
}

//숫자 단위 변환
Number.prototype.formatNumber = function(decPlaces, thouSeparator, decSeparator) {
    var n = this,
        decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSeparator = decSeparator == undefined ? "." : decSeparator,
        thouSeparator = thouSeparator == undefined ? "," : thouSeparator,
        sign = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
};

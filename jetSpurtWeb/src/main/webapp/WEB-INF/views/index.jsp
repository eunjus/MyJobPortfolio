<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!-- include header -->
<%@ include file="/WEB-INF/include/include-header.jsp" %>

	<!-- ** layout정리190513  ** -->
	<article id="container" class="clear">
		<div id="contents_wrap" class="clear">
			<!-- 측면 시작 #aside -->
			<!-- 측면 끝 #aside -->

			<div id="contents_box">

				<!-- #네비게이션 시작 .nav_wrap -->
				<!-- #네비게이션 끝 .nav_wrap -->

				<!-- 컨텐츠 시작 #contents -->
				<div id="content" class="clear ">
					<!-- #공통 상단요소 시작 -->
					<!-- #공통 상단요소 끝 -->

					

					<!-- #서브 컨텐츠 시작 -->

<!-- ** layout정리190513  ** -->
<script>
var noShow = function(checkbox){
	var no = checkbox.value;
	if(checkbox.checked){
		Cookie.setCookie("popup_"+no, 'y', 1);
	}else{
		Cookie.setCookie("popup_"+no, 'y', -1);
	}
};
</script>

<style>
#contents_wrap {width:100%;padding-left:0 !important;padding-right:0 !important;}
#contents_wrap #contents_box {padding:0;}

body {padding-top:0px;}
.header {position:absolute;background:none;border:0 none;opacity:1;transition:0.3s;}
.header.del {opacity:0;}
.header.del2 {opacity:0;display:none;}
#wrap {overflow:hidden;}
</style>

<!-- 메인 이미지 슬라이드 -->
<div class="main_visual">
	<ul class="visual_ul">
	</ul>
	<button class="visual_go_next">go next</button>
	<div class="sld_count">
		<div class="txt">
			<span class="now">1</span>&nbsp;/&nbsp;<span class="all"></span>
		</div>
		<div class="line"></div>
	</div>
	<!--div class="auto_control">
		<a href="javascript://" onClick="$('.visual_ul').slick('slickPause');" class="auto_stop">stop</a>
		<a href="javascript://" onClick="$('.visual_ul').slick('slickPlay');" class="auto_start">start</a>
	</div-->
</div>

<script type="text/javascript">
	
	var confData = JSON.parse('{"form":"responsive","fixed":{"time":"2","speed":"800","mode":"fade"},"responsive":{"pc":{"width":"1218","time":"5","speed":"800","mode":"fade"},"tablet":{"width":"641","time":"5","speed":"800","mode":"horizontal"},"mobile":{"time":"5","speed":"800","mode":"horizontal"}},"files":{"fixed":[{"oname":"main_visual1.jpg","fname":"3.jpg","link":"\/company\/history"},{"oname":"main_visual2.jpg","fname":"2.jpg","link":"\/company\/history"},{"oname":"main_visual3.jpg","fname":"1.jpg","link":"\/company\/history"}],"responsive":{"pc":[{"oname":"main_visual01.jpg","fname":"3.jpg","link":"\/company\/index"},{"oname":"main_visual02.jpg","fname":"2.jpg","link":"\/company\/index"},{"oname":"main_visual03.jpg","fname":"1.jpg","link":"\/company\/index"}],"tablet":[{"oname":"t_main_visual01.jpg","fname":"3.jpg","link":"\/company\/index"},{"oname":"t_main_visual02.jpg","fname":"2.jpg","link":"\/company\/index"},{"oname":"t_main_visual03.jpg","fname":"1.jpg","link":"\/company\/index"}],"mobile":[{"oname":"m_main_visual01.jpg","fname":"3.jpg","link":"\/company\/index"},{"oname":"m_main_visual02.jpg","fname":"2.jpg","link":"\/company\/index"},{"oname":"m_main_visual03.jpg","fname":"1.jpg","link":"\/company\/index"}]},"responsive_length":["","",""]}}');
	confData.files = confData.files || {};

	/* 반응형 js */
	// confData.responsive 가 없을경우 초기화
	confData.responsive = confData.responsive || {};
	confData.responsive.pc = confData.responsive.pc || {};
	confData.responsive.tablet = confData.responsive.tablet || {};
	confData.responsive.mobile = confData.responsive.mobile || {};
	confData.files.responsive = confData.files.responsive || {};
	
	var currMod = '';	
	$(window).resize(function() {
		var width = $(window).width();

		var mode = 'mobile';
		if(width >= Number(confData.responsive.pc.width)){
			mode = 'pc';
		} else if(width >= Number(confData.responsive.tablet.width)){
			mode = 'tablet';
		}

		if(currMod !== mode){
			if(currMod !== ''){
				$('.visual_ul').slick('unslick');
			}
			currMod = mode;
			$('.visual_ul').html(makeLiTag(mode, (confData.files.responsive[mode] || [])));
			$('.visual_ul').slick({					

				vertical: (confData.responsive[mode].mode === 'vertical'),
				fade: (confData.responsive[mode].mode === 'fade'),
				speed: Number(confData.responsive[mode].speed || 1000),
				autoplaySpeed: (Number(confData.responsive[mode].time || 3)) * 1000,
				
				autoplay: true,
				dots: true,
				arrows: true
			});

		}
	});
	$(window).trigger('resize');

	function makeLiTag(type, file){
		var path = "resources/images/main/imageSlide/kor/" + (["pc", "tablet", "mobile"].indexOf(type) > -1? "responsive/" + type + "/" : "fixed/");
		var li = '';
		for(var i=0; i<file.length; i++){
			li += '<li style="width:100%;background-image:url(' + path + file[i].fname + ');">';
			li += '<a href="">';
			li += '<div class="line before">';
			li += '</div>';
			li += '<div class="w_custom set_wdh">';
			li += '<div class="txt">';
			li += '<div class="big">With JetSpurt';
			li += '<div class="line after in">';
			li += '</div>';
			li += '</div>';
			li += '<div class="sub">언제나 고객을 최우선으로 생각합니다.<br>우리는 젯 스퍼트입니다.';
			li += '</div>';
			li += '</div>';
			li += '</div>';
			li += '<div class="line after out">';
			li += '</div>';
			li += '	</a>';
			li += '</li>';
		}
		return li;
	}
			
	$('.visual_ul').on('beforeChange', function(event, slick, currentSlide, nextSlide){		
		$('.main_visual .sld_count .txt .now').html(nextSlide);		
	});
	$('.visual_ul').on('init reInit afterChange', function(event, slick, currentSlide, nextSlide){
	    //currentSlide is undefined on init -- set it to 0 in this case (currentSlide is 0 based)
	    var i = (currentSlide ? currentSlide : 0) + 1;
	    $('.main_visual .sld_count .txt .all').html(slick.slideCount);
	});
</script>
<!-- //메인 이미지 슬라이드 -->
<div class="main">
	<div class="main_cont main_cont01 section">
		<div class="w_custom">
			<div class="title">
				<p class="big">Beyond Technology</p>
				<p class="sub">저희 기업은 차별화된 가치 창출을 위해 끊임없이 노력하고<br>다양한 서비스 사업과 제품 생산에 힘쓰고 있습니다.</p>
				<div class="btns">
					<div class="prev">prev</div>
					<div class="next">next</div>
				</div>
			</div>
			<div class="cont clear">
				<div class="list list01">
					<div class="position">
						<div class="pic"></div>
						<p class="title">기술선도</p>
						<p class="sub">고객의 Needs 그 이상의<br>가치와 창조 개발에 <br class="for_m">공헌합니다.</p>
					</div>
				</div>
				<div class="list list02">
					<div class="position">
						<div class="pic"></div>
						<p class="title">글로벌네트워크</p>
						<p class="sub">전 세계로 수출하는 <br class="for_m">신뢰할 수 있는<br>젯스퍼트입니다.</p>
					</div>
				</div>
				<div class="list list03">
					<div class="position">
						<div class="pic"></div>
						<p class="title">도전적 실행</p>
						<p class="sub">미래를 향한 열정으로 <br class="for_m">늘 노력하며<br class="for_pc">새로운 <br class="for_m">가능성에 도전합니다.</p>
					</div>
				</div>
				<div class="list list04">
					<div class="position">
						<div class="pic"></div>
						<p class="title">윤리경영</p>
						<p class="sub">투명하고 공정한 기업<br class="for_m">경영으로<br class="for_pc">젯스퍼트 <br class="for_m">미래를 이끌겠습니다.</p>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="main_cont main_cont02 section">
		<div class="table">
			<div class="w_custom set_wdh clear">
				<div class="title">
					<p class="big">연혁</p>
					<!--div class="btns">
						<div class="prev">prev</div>
						<div class="next">next</div>
					</div-->
					<p class="small">고객과 함께 달려온 젯스퍼트의<br>지나온 발자취입니다.</p>
					<div class="link"><a href="${pageContext.request.contextPath}/about.do?sub=history">자세히보기</a></div>
				</div>
				<div class="bbs">
				<div class="position">
				<div class="li">
					<a href="#" alt="2017.01">
						<div class="pic"><img src="resources/images/p-images/20191126174908_7900.jpg" onerror="this.src='resources/images/common/noimg.gif'" alt="2017.01"></div>
						<div class="txt">
							<p class="date">2017.01</p>
							<p class="cont">중소기업 전요 무료 인트라넷 서비스 출시메티오피아노테크와 서비스 체결  </p>
						</div>
					</a>
				</div>
				<div class="li">
					<a href="#" alt="2017.02">
						<div class="pic"><img src="resources/images/p-images/20191126174800_1774.jpg" onerror="this.src='resources/images/common/noimg.gif'" alt="2017.02"></div>
						<div class="txt">
							<p class="date">2017.02</p>
							<p class="cont">중소기업 전요 무료 인트라넷 서비스 출시메티오피아노테크와 서비스 체결</p>
						</div>
					</a>
				</div>
					</div>
				<div class="position">
				<div class="li">
					<a href="#" alt="2017.12">
						<div class="pic"><img src="resources/images/p-images/20191126174837_5036.jpg" onerror="this.src='resources/images/common/noimg.gif'" alt="2017.12"></div>
						<div class="txt">
							<p class="date">2017.12</p>
							<p class="cont">중소기업 전요 무료 인트라넷 서비스 출시메티오피아노테크와 서비스 체결</p>
						</div>
					</a>
				</div>
				<div class="li">
					<a href="#" alt="2018.08">
						<div class="pic"><img src="resources/images/p-images/20191126174633_2609.jpg" onerror="this.src='resources/images/common/noimg.gif'" alt="2018.08"></div>
						<div class="txt">
							<p class="date">2018.08</p>
							<p class="cont">중소기업 전요 무료 인트라넷 서비스 출시메티오피아노테크와 서비스 체결 </p>
						</div>
					</a>
				</div>
				</div>

				</div>
			</div>
		</div>
	</div>
	<div class="main_cont main_cont03 clear section">
		<a href="${pageContext.request.contextPath}/recruit.do?sub=recruit_write?code=recruit" class="left main_bnr01">
			<div class="table">
				<div class="table-cell">
					<div class="txtbox">
						<p class="big">CHALLENGE<br><strong>YOURSELF<br>NOW.</strong></p>
						<p class="small">함께 할 인재를 기다립니다.</p>
						<p class="go"><span>GO</span></p>
						<p class="plus"><span>채용 신청하기</span></p>
					</div>
				</div>
			</div>
		</a>
		<div class="right">
			<div href="" class="main_bnr02">
				<div class="position">
					<p class="big">Brochure</p>
					<p class="small">젯스퍼트의 회사소개<br class="for_m">브로셔 다운 받아보세요</p>
					<a class="btn" href="#">다운받기</a>
				</div>
			</div>
			<div class="main_news">
				<div class="position">
					<div class="relative clear">
						<div class="relative2 clear">
							<div class="title">
								<p class="big">NEWS</p>
								<p class="sml">젯스퍼트의 소식입니다.</p>
								<div class="btns">
									<div class="prev">prev</div>
									<div class="next">next</div>
								</div> 
							</div>
							<div class="bbs">
								<div class="sld">
									<c:choose>
										<c:when test="${fn:length(BoardList) > 0}">
											<c:forEach items="${BoardList}" var="row">
												<li>
													<a href="${pageContext.request.contextPath}/boardView.do?code=notice&no=${row.post_number}">
														<p class="cate">공지사항</p>
														<p class="title truncate-ellipsis2">${row.post_title}</p>
														<p class="cont truncate-text2">${row.post_title}</p>
														<p class="date">${row.reg_dt }</p>												
													</a>
												</li>											
											</c:forEach>
										</c:when>
										<c:otherwise>
										<li>
										<a href="${pageContext.request.contextPath}/">
											<p class="cate">공지사항</p>
											<p class="title truncate-ellipsis2">조회된 결과가 없습니다.</p>
											<p class="cont truncate-text2">조회된 결과가 없습니다.</p>
											<p class="date"></p>												
										</li>	
									</c:otherwise>
									</c:choose>									
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<script src="https://code.jquery.com/jquery-latest.js"></script>
<script src="resources/js/slick.js"></script>
<script type="text/javascript">
	$(document).ready(function(){
		// news 슬라이드
		$('.main_cont03 .sld').slick({
			dots:false,
			slidesToShow: 2,
			responsive: [{
				breakpoint: 1300,
				settings: {
					slidesToShow: 1
				}
			}]
		});
		$('.main_cont03 .title .btns > div.prev').click(function(){$(".main_cont03 .sld").slick('slickPrev');});
		$('.main_cont03 .title .btns > div.next').click(function(){$(".main_cont03 .sld").slick('slickNext');});
	});
</script>
<!-- ** layout정리190513  ** -->
					<!-- #서브 컨텐츠 끝 -->

				</div><!-- #contents -->
				<!-- 컨텐츠 끝 #contents -->

			</div><!-- #contents_box -->

		</div><!-- #contents_wrap -->
<!-- ** layout정리190513  ** -->

<!-- include footer -->
<%@ include file="/WEB-INF/include/footer.jsp" %>
	
</article><!-- #wrap 전체영역 -->
<iframe name="ifr_processor" id="ifr_processor" title="&nbsp;" width="0" height="0" frameborder="0" style="display:none;"></iframe>
</body>
</html>
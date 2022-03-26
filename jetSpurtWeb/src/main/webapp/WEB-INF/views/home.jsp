<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ page session="true" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html lang="ko">
<head>
<title>젯스퍼트</title>
<link rel="shortcut icon" href="/upload/conf/favicon1.ico" type="image/x-icon" />
<link rel="icon" href="/upload/conf/favicon1.ico" type="image/x-icon" />

<meta charset="UTF-8">
<meta name="title" content="젯스퍼트" />
<meta name="keywords" content="젯스퍼트">
<meta name="description" content="젯스퍼트">
<meta name="author" content="젯스퍼트">
<meta name="viewport" content="width=device-width, user-scalable=no">
<meta name="format-detection" content="telephone=no" /><!--애플전화번호링크자동설정헤제-->


<link rel="stylesheet" type="text/css" href="resources/css/reset.css" />
<link rel="stylesheet" type="text/css" href="resources/css/jquery.fullPage.css" />
<link rel="stylesheet" type="text/css" href="resources/css/common.css" /><!-- 서브 공통 css ｜ custom 금지 css -->
<link rel="stylesheet" type="text/css" href="resources/css/sub.css" /><!-- 서브 공통 custom 요소 css ｜ 개별페이지 css -->
<link rel="stylesheet" type="text/css" href="resources/css/skin.css" /><!-- 스킨 css ｜ 상하단·메인 -->
<link rel="stylesheet" type="text/css" href="resources/css/slick.css" /><!-- 슬라이드 기능 -->

<script src="resources/js/jquery.min.js"></script>
<!-- <script src="resources/lib/js/jquery-2.2.4.min.js"></script> -->
<script src="resources/lib/js/jquery.validate.min.js"></script>
<script src="resources/lib/js/common.js"></script>
<script src="resources/lib/js/jquery-ui.min.js"></script>
<script src="resources/lib/js/moment-with-locales.min.js"></script>
<script src="//dmaps.daum.net/map_js_init/postcode.v2.js"></script>
<!-- <script type="text/javascript" src="resources/js/jquery.bxslider.min.js"></script> -->
<!-- <link rel="stylesheet" type="text/css" href="//kenwheeler.github.io/slick/slick/slick.css"> -->
<!--#script src="//cdn.jsdelivr.net/bxslider/4.2.12/jquery.bxslider.min.js"></script-->
<!-- <script src="//kenwheeler.github.io/slick/slick/slick.js"></script> -->
<!--#script src="resources/js/ui_common.js"></script-->

<script src="resources/js/jquery.mCustomScrollbar.concat.min.js"></script>
<script type="text/javascript">
	var CURRENT_DATE = "2021-05-06 11:49:27";
</script>

</head>
<body>

<!-- 메인 이미지 슬라이드 -->
<div class="main_visual">
	<ul class="visual_ul">
		<li style="width:100%;background-image:url('resources/images/main/imageSlide/kor/responsive/pc/1.jpg');">
			<a href="about.jsp">
			<div class="line before"></div>
			<div class="w_custom set_wdh">
			<div class="txt">
			<div class="big">Safety Always
			<div class="line after in">
			</div>
			</div>
			<div class="sub">언제나 안전을 최우선으로 생각합니다.<br>우리는 트루 인더스트리입니다.</div>
			</div>
			</div>
			<div class="line after out">
			</div>
			</a>
		</li>
		<li style="width:100%;background-image:url('resources/images/main/imageSlide/kor/responsive/pc/2.jpg');">
			<a href="about.jsp">
			<div class="line before"></div>
			<div class="w_custom set_wdh">
			<div class="txt">
			<div class="big">Safety Always
			<div class="line after in">
			</div>
			</div>
			<div class="sub">언제나 안전을 최우선으로 생각합니다.<br>우리는 트루 인더스트리입니다.</div>
			</div>
			</div>
			<div class="line after out">
			</div>
			</a>
		</li>
		<li style="width:100%;background-image:url('resources/images/main/imageSlide/kor/responsive/pc/3.jpg');">
			<a href="about.jsp">
			<div class="line before"></div>
			<div class="w_custom set_wdh">
			<div class="txt">
			<div class="big">Safety Always
			<div class="line after in">
			</div>
			</div>
			<div class="sub">언제나 안전을 최우선으로 생각합니다.<br>우리는 트루 인더스트리입니다.</div>
			</div>
			</div>
			<div class="line after out">
			</div>
			</a>
		</li>
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
<script src="https://code.jquery.com/jquery-latest.js"></script>
<script src="resources/js/slick.js"></script>
<script src="resources/js/slick.min.js"></script>
<script type="text/javascript">
$(document).ready(function () {

$('.visual_ul').slick({					
	autoplay : true,
	dots: true,
	speed : 800 /* 이미지가 슬라이딩시 걸리는 시간 */,
	infinite: true,
	autoplaySpeed: 5000 /* 이미지가 다른 이미지로 넘어 갈때의 텀 */,
	arrows: true,
	slidesToShow: 1,
	slidesToScroll: 1,
	fade: true
	
});
$('.main_visual .sld_count .txt .all').html($('.visual_ul .slick-slide').length);
$('.visual_ul').on('beforeChange', function(event, slick, currentSlide, nextSlide){
	$('.main_visual .sld_count .txt .now').html(nextSlide);
});
});
</script>
<!-- //메인 이미지 슬라이드 -->

</body>
</html>
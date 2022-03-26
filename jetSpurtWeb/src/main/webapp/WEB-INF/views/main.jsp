<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<!DOCTYPE HTML>
<html lang="ko">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
<title>FLAT DESIGN</title>
<link rel="stylesheet" type="text/css" href="resources/css/reset.css">
<link rel="stylesheet" type="text/css" href="resources/css/default.css">
<link rel="shortcut icon" href="resources/images/favicon/favicon.ico">
<link rel="apple-touch-icon-precomposed" href="resources/images/icon/flat-design-touch.png">

<script src="resources/js/flat.min.js"></script>
</head>
<body>
	<div id="wrap">
		<section class="info_section">
			<ul class="info_list">
				<li><a href="main.jsp"><img src="resources/images/s_images/info_icon_01.png" alt=""></a></li>
				<li><a href=""><img src="resources/images/s_images/info_icon_02.png" alt=""></a></li>
				<li><a href=""><img src="resources/images/s_images/info_icon_03.png" alt=""></a></li>
				<li><a href=""><img src="resources/images/s_images/info_icon_04.png" alt=""></a></li>
			</ul>
		</section>
		<header class="header">
			<h1 class="logo">
				<a href="main.jsp">flat<br>design</a>
			</h1>
			<nav class="nav">
				<ul class="gnb">
					<li><a href="main.jsp">홈</a><span class="sub_menu_toggle_btn">하위 메뉴 토글 버튼</span></li>
					<li><a href="introudce.jsp">플랫 디자인이란?</a><span class="sub_menu_toggle_btn">하위 메뉴 토글 버튼</span></li>
					<li><a href="gallery.jsp">갤러리</a><span class="sub_menu_toggle_btn">하위 메뉴 토글 버튼</span></li>
					<li><a href="board.jsp">문의사항</a><span class="sub_menu_toggle_btn">하위 메뉴 토글 버튼</span></li>
				</ul>
			</nav>
			<span class="menu_toggle_btn">전체 메뉴 토글 버튼</span>
		</header>
		<section class="slider_section">
			<span class="prev_btn">이전 버튼</span><span class="next_btn">다음 버튼</span>
		</section>
		<section class="latest_post_section">
			<h2 class="title">최근 글</h2>
			<ul class="latest_post_list">
				<li><a href="">안녕하세요 홈페이지가 오픈...</a></li>
				<li><a href="">홈페이지 리뉴얼...</a></li>
				<li><a href="">flat design은...</a></li>
				<li><a href="">blog에서 다양한 정보를...</a></li>
				<li><a href="">저는 누굴까요?...</a></li>
			</ul>
		</section>
		<section class="popular_post_section">
			<h2 class="title">인기 글</h2>
			<ul class="popular_post_list">
				<li><a href="">안녕하세요 홈페이지가 오픈...</a></li>
				<li><a href="">홈페이지 리뉴얼...</a></li>
				<li><a href="">flat design은...</a></li>
				<li><a href="">blog에서 다양한 정보를...</a></li>
				<li><a href="">저는 누굴까요?...</a></li>
			</ul>
		</section>
		<section class="gallery_section">
			<ul class="gallery_list">
				<li>
					<a href="#">
						<figure>
							<img src="resources/images/p_images/gallery_01.jpg" alt="">
							<figcaption>디자인 트렌트 플랫</figcaption>
						</figure>
					</a>
				</li>
				<li>
					<a href="#">
						<figure>
						<img src="resources/images/p_images/gallery_02.jpg" alt="">
						<figcaption>원색이 포인트 플랫</figcaption>
						</figure>
					</a>
				</li>
			</ul>				
		</section>
		<section class="rankup_section">
			<h2 class="title">인기 검색어</h2>
			<ul class="rankup_list">
				<li><a href="">반응형 웹</a></li>
				<li><a href="">미디어 쿼리</a></li>
				<li><a href="">뷰포트</a></li>
				<li><a href="">CSS 트릭스</a></li>
				<li><a href="">W3C</a></li>
				<li><a href="">루크 W</a></li>
				<li><a href="">CSS 젠 가든</a></li>
				<li><a href="">클리어 보스</a></li>
				<li><a href="">XE</a></li>
				<li><a href="">워드프레스</a></li>
			</ul>
		</section>
		<section class="banner_section">
			<div class="banner_box_01">
				<a href=""><img src="resources/images/s_images/w3c_logo.png" alt=""></a>
			</div>
			<div class="banner_box_02">
				<ul class="banner_list">
					<li><a href=""><img src="resources/images/s_images/js_logo.png" alt=""></a></li>
					<li><a href=""><img src="resources/images/s_images/html_logo.png" alt=""></a></li>
					<li><a href=""><img src="resources/images/s_images/css_logo.png" alt=""></a></li>
				</ul>
			</div>
		</section>
		<section class="social_section">
			<ul class="social_list">
				<li><a href=""><img src="resources/images/s_images/social_icon_01.png" alt=""></a></li>
				<li><a href=""><img src="resources/images/s_images/social_icon_02.png" alt=""></a></li>
				<li><a href=""><img src="resources/images/s_images/social_icon_03.png" alt=""></a></li>
			</ul>
		</section>
		<footer class="footer">
			<p>copyright&copy; 2014.flat design blog all rights reserved.</p>
		</footer>
	</div>
</body>
</html>
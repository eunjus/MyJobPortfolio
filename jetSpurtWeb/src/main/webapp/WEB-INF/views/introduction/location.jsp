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
		<dt class="">인사말</dt>
			<!-- 메뉴관리 1차 하위 2차메뉴 있을 때 -->
			<dd class="menu01 "><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a></dd>
			<dd class="menu02 "><a href="${pageContext.request.contextPath}/about.do?sub=history">연혁</a></dd>
			<dd class="menu03 "><a href="${pageContext.request.contextPath}/about.do?sub=chart">조직도</a></dd>
			<dd class="menu04 "><a href="${pageContext.request.contextPath}/about.do?sub=introduce">사업소개</a></dd>
			<dd class="menu05 on"><a href="${pageContext.request.contextPath}/about.do?sub=location">오시는 길</a></dd>
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
									location<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
							</h2>
							<p>
									location 페이지 입니다.
							</p>
							<ul>
								<li><a href="${pageContext.request.contextPath}/" class="home">Home</a> ></li>
									<li><a href="${pageContext.request.contextPath}/about.do?sub=about">인사말</a> ></li>
								<li>
									<strong>
										location<!-- 메뉴관리에 등록된 개별페이지만 페이지명 노출 -->
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
<script type="text/javascript" src="http://openapi.map.naver.com/openapi/v3/maps.js?clientId=IMIEQwAacmOyrkEUBm6E&callback=initMap"></script>
<script type="text/javascript">
	var map = null;
	var marker = null;
	var position = new naver.maps.LatLng(37.401216, 127.107415);

	var mapOptions = {
		center: position,
		zoom: 12
	};
	
	function initMap() {
		map = new naver.maps.Map('map', mapOptions);
		marker = new naver.maps.Marker({
			position: position,
			map: map
			//,icon: '/data/skin/default/company/img/pin_default.png'
		});
	}
</script>
<div class="sub_content">
	<div class="sub_location">
		<div class="sub_location_map clear">
			<div class="map_box">				
				<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3175.8305455000004!2d127.07494541558822!3d37.2517277798581!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x357b44af88ccaac5%3A0x348dd24202a101!2z7KCv7Iqk7Y287Yq4KOyjvCk!5e0!3m2!1sko!2skr!4v1620724435541!5m2!1sko!2skr" width="100%" height="100%" frameborder="0" style="border:0;" allowfullscreen=""></iframe>
			</div>
		</div><!-- .sub_location_map -->

		<div class="sub_location_info">
			<dl>
				<dt>(주)젯스퍼트</dt>
				<dd>
					<ul>
						<li class="map1"><span>주소 : </span><p>(000000) 경기도 수원시 영통구 반달로 35번길 30 영통역아이파크 348호</p></li>
						<li class="map2"><span>대표번호 : </span><p>031-211-9030</p></li>
						<li class="map3"><span>이메일 : </span><p>jetspurt@mail.jetspurt.com</p></li>
					</ul>
				</dd>
			</dl>
			<dl>
				<dt>교통정보</dt>
				<dd>
					<ul>
						<li class="map4"><span>지하철 : </span><p>영통역 2번 출구로 나오신 후 도보로 약 8분 소요</p></li>
						<li class="map5"><span>자가용 : </span><p>경부고속도로 판교 IC에서 분당.판교 방면으로 나온 후 통게이트를 지나 고가도로 옆 길, 서현로를 따라 이동 후 대왕판교로로 이동</p></li>
					</ul>
				</dd>
			</dl>
		</div><!-- .sub_location_info -->
	</div><!-- .sub_location -->
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
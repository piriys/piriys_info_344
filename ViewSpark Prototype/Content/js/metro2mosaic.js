function changeThemeColor_1(color) {
	var colorClass = "color-" + color;
	$('.metro-color-1').removeClass(
		function (index, css) {
			return (css.match(/\bcolor-\S+/g) || []).join(' ');
		}
	);
				
	$('.metro-color-1').addClass(colorClass);
}

function changeThemeColor_2(color) {
	var colorClass = "color-" + color;
	$('.metro-color-2').removeClass(
		function (index, css) {
			return (css.match(/\bcolor-\S+/g) || []).join(' ');
		}
	);
				
	$('.metro-color-2').addClass(colorClass);
}

function toggleToInactive(className) {
	$(className).removeClass('state-active');
	$(className).addClass('state-inactive');
}

function toggleToActive(className) {
	$(className).removeClass('state-inactive');
	$(className).addClass('state-active');
}
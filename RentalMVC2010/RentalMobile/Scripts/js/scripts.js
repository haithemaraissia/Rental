$(document).ready(function () {

	//scroll to top link
	$('.scroll-top').click(function(){
    	$("html, body").animate({ scrollTop: 0 }, 600);
            return false;
    });
	
	
	
	//Lightbox Fancybox
	
	$("#fancybox-manual-c").click(function() {
				$.fancybox.open([
					{
						href : 'images/single-property/big-800x500-1.jpg',
						title : 'Property 1 Example'
					}, {
						href : 'images/single-property/big-800x500-2.jpg',
						title : 'Property 2 Example'
					}, {
						href : 'images/single-property/big-800x500-3.jpg',
						title : 'Property 3 Example'
					},{
						href : 'images/single-property/big-800x500-4.jpg',
						title : 'Property 4 Example'
					},{
						href : 'images/single-property/big-800x500-5.jpg',
						title : 'Property 5 Example'
					},{
						href : 'images/single-property/big-800x500-6.jpg',
						title : 'Property 6 Example'
					},{
						href : 'images/single-property/big-800x500-7.jpg',
						title : 'Property 7 Example'
					}, {
						href : 'images/single-property/big-800x500-4.jpg',
						title : 'Property 8 Example'
					}, {
						href : 'images/single-property/big-800x500-1.jpg',
						title : 'Property 9 Example'
					},{
						href : 'images/single-property/big-800x500-2.jpg',
						title : 'Property 10 Example'
					},{
						href : 'images/single-property/big-800x500-3.jpg',
						title : 'Property 11 Example'
					},{
						href : 'images/single-property/big-800x500-5.jpg',
						title : 'Property 12 Example'
					}
					
					
				], {
					helpers : {
						thumbs : {
							width: 75,
							height: 50
						}
					}
				});
			});
	
    
    //responsive menu
    $('<select />').appendTo('header nav');

	$('<option />', {
	    'selected': 'selected',
	    'value'   : '',
	    'text'    : 'Select a Page...'
	}).appendTo('header nav select');
	
	$('.menu-bar .menu a').each(function() {
	    var el = $(this);
	    if(el.parents('.menu-bar .menu ul').length) {
	        $('<option />', {
	            'value': el.attr('href'),
	            'text':  '... ' + el.text()
	        }).appendTo('nav select');
	    } else {
	        $('<option />', {
	            'value': el.attr('href'),
	            'text': el.text()
	        }).appendTo('nav select');
	    }
	});
	
	$('header nav select').change(function() {
	    window.location = $(this).find('option:selected').val();
	});

	//tipsy
	$('ul.social a, .home-btn').tipsy({
		gravity: 's',
		fade: true
	});

	//filters on home and rent
	var newSelection = "";
	
	$(".filter-list a").click(function(){
	
	    $("#property-list").fadeTo(200, 0.10);
	
		$(".filter-list a").removeClass("current");
		$(this).addClass("current");
		
		newSelection = $(this).attr("data-rel");
		
		$(".prop-type").not("."+newSelection).slideUp();
		$("."+newSelection).slideDown();
		
	    $("#property-list").fadeTo(600, 1);
		
	});

});
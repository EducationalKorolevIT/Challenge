let isHideTopics = false;

/* MENU */



/* END MENU */





/* CONTENT */

/* Change icon music */
function music_off(image) {
    if (document.getElementById("image").src.indexOf("css/audio_off.png")>0)
            {document.getElementById("image").src="css/audio.png"}
        else
            {document.getElementById("image").src="css/audio_off.png"}
}

/* Bxslider */
jQuery(document).ready(function () {
    jQuery('.bxslider').bxSlider({
        /* General setting */
        mode: 'vertical',
        speed: 500,
        slideMargin: 0,
        easing: 'jswing',
        captions: true,
        video: true,
        useCSS: false,
        adaptiveHeight: true,
        adaptiveHeight: 500,
        preloadImages: 'all',

        /* Slider */
        pager: false,
        pagerType: 'full',

        /* For touch screen */
        touchEnabled: true,
        oneToOneTouch: true,
        preventDefaultSwipeY: false,
        swipeThreshold: 50,

        /* Control element */
        controls: true,
        nextText: '',
        nextSelector: null,
        prevText: '',
        prevSelector: null,
        autoControls: false,

        /* Automatic slide show yeeeep */
        auto: true,
        pause: 10000,
        autoStart: true,
        autoDirection: 'next',
        autoHover: false,
        autoDelay: 200,

        /* Carousel, hmmm */
        minSlides: 1,
        maxSlides: 1,
        moveSlides: 0,
        slideWidth: 0,


    });
});

/* Open/Close relevant menu */
function closeOpenMenu() {
    let top = document.querySelector('.arrow_menu');

    if(isHideTopics == false){
        isHideTopics = true;
        $('.menu').animate({
            left: '-350px'
        }, 200);

        $('body').animate({
            right: '0px'
        }, 400);

        top.innerHTML = "<";
    } else {
        isHideTopics = false;
        $('.menu').animate({
            left: '0px'
        }, 200);
        $('body').animate({
            right: '0px'
        }, 400);

        top.innerHTML = ">";
    }
};



/* Open/Close relevant topics */
function closeOpenTopics() {
    let top = document.querySelector('.arrow_topics');

    if(isHideTopics == false){
        isHideTopics = true;
        $('.topics').animate({
            right: '-350px'
        }, 200);

        $('body').animate({
            left: '0px'
        }, 400);

        top.innerHTML = "<";
    } else {
        isHideTopics = false;
        $('.topics').animate({
            right: '0px'
        }, 200);
        $('body').animate({
            left: '0px'
        }, 400);

        top.innerHTML = ">";
    }
};



/* END CONTENT */





/* TOPICS */



/* END TOPICS */
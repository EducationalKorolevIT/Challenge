let isHideTopics = false;

/* MENU */



/* END MENU */



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
        controls: false,
        nextText: '',
        nextSelector: null,
        prevText: '',
        prevSelector: null,
        autoControls: false,

        /* Automatic slide show yeeeep */
        auto: true,
        pause: 2000,
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



/* CONTENT */

/* Change icon music */
function sound_off(sound) {
    if (document.getElementById("sound").src.indexOf("/Files/GetJpgFile?fileString=Content/css/icon_content/sound_off.png") > 0) { document.getElementById("sound").src = "/Files/GetJpgFile?fileString=Content/css/icon_content/sound.png" }
    else { document.getElementById("sound").src = "/Files/GetJpgFile?fileString=Content/css/icon_content/sound_off.png" }
}

/* Open/Close relevant menu */
function closeOpenMenu() {
    let top = document.querySelector('.arrow_menu');

    if (isHideTopics == false) {
        isHideTopics = true;
        $('.menu').animate({
            left: '-300px'
        }, 200);

        $('body').animate({
            right: '0px'
        }, 400);

        top.innerHTML = ">";
    } else {
        isHideTopics = false;
        $('.menu').animate({
            left: '0px'
        }, 200);
        $('body').animate({
            right: '0px'
        }, 400);

        top.innerHTML = "<";
    }
};



/* Open/Close relevant topics */
function closeOpenTopics() {
    let top = document.querySelector('.arrow_topics');

    if (isHideTopics == false) {
        isHideTopics = true;
        $('.topics').animate({
            right: '-300px'
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
/*function hidemenu(){
    let elements_menu = document.querySelectorAll('.nav_link');
    let menu = document.querySelector('.menu');
    let content = document.querySelector('.content');

    if(menu.main.width == '30%'){
        menu.main.width = '5%';
        content.main.margin = '0 0 0 200px';
    } else {
        menu.main.width = '30%';
        content.main.margin = '0 0 0 50px';
    }

    for (let elem of elements_menu) {
        if(elem.main.display == 'none')
            elem.main.display = 'flex';
        else
            elem.main.display = 'none';
    }

    $('#menu-button').click(function() {
        $('#nav').toggleClass('visible');
    });
}*/


function music_off(image) {
    if (document.getElementById("image").src.indexOf("/Files/GetJpgFile?fileString=Content/css/audio_off.png") > 0) { document.getElementById("image").src = "/Files/GetJpgFile?fileString=Content/css/audio.png" }
    else { document.getElementById("image").src = "/Files/GetJpgFile?fileString=Content/css/audio_off.png" }
}


/* Открытие меню */
function openMenu() {
    $('.menu').animate({
        left: '0px'
    }, 200);
    $('body').animate({
        left: '285px'
    }, 400);
}

/* Закрытие меню */
function closeMenu() {
    $('.menu').animate({
        left: '-285px'
    }, 200);

    $('body').animate({
        left: '0px'
    }, 400);
};
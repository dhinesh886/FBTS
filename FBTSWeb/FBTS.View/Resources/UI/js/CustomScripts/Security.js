// JScript File
// window.history.go(-1);
window.history.go(1);
/*------F5 Disble-----------*/
document.attachEvent("onkeydown", my_onkeydown_handler);
function my_onkeydown_handler() {
    switch (event.keyCode) {
        case 116: // 'F5'
            event.returnValue = false;
            event.keyCode = 0;
            window.status = "Sorry, We have disabled F5";
            break;
    }
}

/*------Right Click Disble-----------*/
document.onmousedown = right;
if (document.layers) window.captureEvents(Event.MOUSEDOWN); window.onmousedown = right;
var debug = true;
function right(e) {
    if (navigator.appName == 'Netscape' && (e.which == 3 || e.which == 2))
        return false;
    else if (navigator.appName == 'Microsoft Internet Explorer' && (event.button == 2 || event.button == 3)) {
        alert('Sorry, Right click is disabled because of Security Reasons!');
        return false;
    }
    return true;
}
document.attachEvent("onkeydown", noCTRL);
function noCTRL(e) {
    var code = (document.all) ? event.keyCode : e.which;
    var msg = "Sorry, this functionality is disabled because of Security Reasons!";
    if (parseInt(code) == 17123333) // This is the Key code for CTRL key
    {
        alert(msg);
        window.event.returnValue = false;
    }
}
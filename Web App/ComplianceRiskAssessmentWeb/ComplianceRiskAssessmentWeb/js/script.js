function alerting(RiskID,BizID,CRID,IRID,RiskLabel,BizLabel,UserID,VersionID){
    document.getElementById("commentsID").value = "";
    document.querySelector(".popup").style.display = "flex";

    document.getElementById("RiskID").value = RiskID;
    document.getElementById("BizID").value = BizID;

    document.getElementById("RiskIDHistory").value = RiskID;
    document.getElementById("BizIDHistory").value = BizID;

    document.getElementById("UserID").value = UserID;
    document.getElementById("VersionID").value = VersionID;

    document.getElementById("popup-contentheaderspan").textContent = BizLabel + ", " + RiskLabel;
    document.getElementById("popup-contentheaderspan").title = BizLabel + ", " + RiskLabel;

    document.getElementById("ControlRisk").selectedIndex = CRID - 1;
    document.getElementById("InherentRisk").selectedIndex = IRID - 1;

    var el = document.getElementById("popup-content").clientHeight;
    if(el > 650){
        closeHistory();
    }
}

function editHeaders(){
    document.querySelector(".popupHeaders").style.display = "flex";
}

function openHistory(){
    var el = document.getElementById("popup-content")
    var height = el.clientHeight;
    var newHeight = height + 180;
    el.style.height = newHeight + 'px';

    document.getElementById("divHistory").style.display = "none";
    document.getElementById("divCloseHistory").style.display = "block";
}

function closeHistory(){
    var el = document.getElementById("popup-content")
    var height = el.clientHeight;
    var newHeight = height - 220;
    el.style.height = newHeight + 'px';

    document.getElementById("divCloseHistory").style.display = "none";
    document.getElementById("divHistory").style.display = "block";
}

function closePopUp(){
    document.querySelector(".popup").style.display = "none";
}

function closePopUpHeaders(){
    document.querySelector(".popupHeaders").style.display = "none";
    window.location.reload();
}

function dragElement(elmnt) {
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    if (document.getElementById(elmnt.id + "header")) {
            // if present, the header is where you move the DIV from:
        document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
    } else {
        // otherwise, move the DIV from anywhere inside the DIV:
        elmnt.onmousedown = dragMouseDown;
    }

    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }

    function closeDragElement() {
        // stop moving when mouse button is released:
        document.onmouseup = null;
        document.onmousemove = null;
    }
}




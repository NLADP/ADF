<script type="text/javascript">

function ShowInfoButton(e) {
    var evt = e || window.event;
    var element = evt.target || evt.srcElement;
    var InfoButton = document.getElementById("InfoButton");

    if (InfoButton != null)
    {
        InfoButton.innerHTML = element.InfoText;

        var left = (document.body.clientWidth - InfoButton.clientWidth) / 2;
        InfoButton.style.left = left + "px";
        
        var top = (document.body.clientHeight - InfoButton.clientHeight) / 2;
        InfoButton.style.top = top + "px";
        
        InfoButton.show();
    }
}

function HideInfoButton(e) {
    document.getElementById("InfoButton").hide();
}

function getElementLeft(element) {
    if (element == null) return 0;
    if (element.id == "con") return 0;
    return element.offsetLeft + getElementLeft(element.offsetParent);
}

function getElementTop(element) {
    if (element == null) return 0;
    if (element.id == "con") return 0;
    return element.offsetTop + getElementTop(element.offsetParent);
}
        
function InitInfoButton() 
{
    // var InfoButton = document.createElement("div");
    // document.body.appendChild(InfoButton);
    var InfoButton = document.getElementById("InfoButton");
    
    if (InfoButton != null) 
    {
        InfoButton.id = "InfoButton";
        InfoButton.className = "InfoButton";
        InfoButton.show = function() { this.style.visibility = "visible"; }
        InfoButton.hide = function() { this.style.visibility = "hidden"; }
    }
}
</script>
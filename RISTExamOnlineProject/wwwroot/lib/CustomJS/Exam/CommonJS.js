
function InputHTML(elementID, Code) {
    document.getElementById(elementID).innerHTML += Code;
}

function DeleteHTML(elementID) {
    document.getElementById(elementID).innerHTML = "";
}


function CheckRole() {

    debugger

    if ('@User.IsInRole("Admin")' != "True") {

        window.location.href = "@Url.Action("index", "home")";
    }


}

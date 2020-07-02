
function Add_Detail_Display(DisplayID, SummernoteID) {

    $('#BTN_Edit').attr('disabled', false);
    $('#BTN_Save').attr('disabled', false);

    var markup = $('#' + SummernoteID).summernote('code');
    InputHTML(DisplayID, markup)

    Reset_Summernote(SummernoteID)

};
function Show_Summernote(DisplayID) {
   
    TempDisplayID = DisplayID
    $('#Summernote_modal').summernote({ height: 150 });
    Reset_Summernote('Summernote_modal')
    DeleteHTML('Display_Modal')
    var HTMLText = document.getElementById(DisplayID).innerHTML
    InputHTML('Display_Modal', HTMLText)
    $('#BTN_Edit').attr('disabled', false);
    $('#BTN_Save').attr('disabled', false);
    $('#Modal_Summernote').modal('show')
}

function Edit_Detail_Display(DisplayID, SummernoteID) {

    $('#BTN_Edit').attr('disabled', true);
    $('#BTN_Save').attr('disabled', true);

    Reset_Summernote(SummernoteID)
    var HTMLText = document.getElementById(DisplayID).innerHTML
    Summernote_PasteHTML(SummernoteID, HTMLText)
    DeleteHTML(DisplayID)
};


function Add_Ans() {

    debugger
    AnsrowCount++
    var newid_Dp = "Display_Answer_" + AnsrowCount
    var newid_Rd =  "RD_Ans_"+ AnsrowCount
    var newel = $('.ANS:last').clone();

    var replaseID_RD = newel[0].getElementsByClassName('RD_Display');

    replaseID_RD = replaseID_RD[0].childNodes[1].id

    var replaseID_Display = newel[0].getElementsByClassName('Display_Answer');
    replaseID_Display = replaseID_Display[0].id

    replaseID_RD = new RegExp(replaseID_RD, 'g');
    replaseID_Display = new RegExp(replaseID_Display, 'g');



    var HTMLText = $('.ANS:last').clone().html();
    HTMLText = HTMLText.replace(replaseID_Display, newid_Dp).replace(replaseID_RD, newid_Rd)

    newel[0].id = 'Ans' + AnsrowCount
    newel[0].innerHTML = HTMLText

    $(newel).insertAfter(".ANS:last")
    $('#' + newid_Dp + '').empty();
    CountAns()
}
function Summernote_PasteHTML(SummernoteID, HTMLText) {
    $('#' + SummernoteID).summernote('pasteHTML', HTMLText);
}


function Reset_Summernote(SummernoteID) {
    $('#' + SummernoteID).summernote('reset');
}

function Clear_Display(DisplayID) {
    DeleteHTML(DisplayID)
}


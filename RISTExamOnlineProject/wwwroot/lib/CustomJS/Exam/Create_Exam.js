
function GetExamDetail(Itemcode) {
 
    $.ajax({
        type: 'POST',
        url: '../Exam/GetExamDetail',
        dataType: 'json',
        data: { Itemcode: Itemcode },
        success: function (response) {
            if (response.success == true) {
                LastSeq = response.lastSeq
                QuestionCount = response.questionCount
                ValueCodeQuestion = response.valueCodeQuestion
                ValueCodeAnswer = response.valueCodeAnswer
                ItemName = response.itemName
          
                $('#LB_Exam_Count').text(QuestionCount);
                $('#LB_Exam_Name').text(ItemName)
            } else {

            }

        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });




}




var $widget = $("[data-ks-widget]");
var toggleCls = "open";

$widget.on("click", "[data-widget-toggle]", function (e) {
    e.preventDefault();
    $widget.toggleClass(toggleCls);
});






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


function CountAns() {

    var parent = document.getElementById("FormDisplay_Answer");
    var eee = 0
    eee = parent.getElementsByClassName("ANS");
    var count = eee.length

    $('#LB_Ans_Count').text(count);

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





$(document).on('click', 'button.remove', function (e) {
  
    var parent = document.getElementById("FormDisplay_Answer");
    var nodesSameClass = 0
    nodesSameClass = parent.getElementsByClassName("ANS");

    if (nodesSameClass.length > 1) {

        e.preventDefault();

        $(this).closest('div.ANS').remove();
    }


    CountAns()

});



function Save_Exam() {
    Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: "Are you sure you want to Save Question ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Do it!'

    }).then(function (result) {
        if (result.value) {
            Insert_Exam()         
        }
    })


}



function Insert_Exam() {
    //------------------ Save Ans ----------------------
    var Ans_TextDisplay = [];
    var Ans_Text_HTML_Display = [];
    var Ans_Value = [];
    var Need_value = [];
    var Need = document.getElementById("CB_Need");
    Need = Need.checked;

    var parent = document.getElementById("FormDisplay_Answer");
    var nodesSameClass = 0
    nodesSameClass = parent.getElementsByClassName("ANS");
    var AnsCount = nodesSameClass.length
    var Display = document.getElementsByClassName("Display_Answer");
    var RD = document.getElementsByClassName("RD_Display");

    for (var i = 0; i <= AnsCount - 1; i++) {
        Ans_Text_HTML_Display.push(Display[i].innerHTML);  // HTML TEXT
        Ans_TextDisplay.push(Display[i].innerText);   //  TEXT
        Ans_Value.push(Number(RD[i].childNodes[1].checked)); // RadioValue
        if (Need == true && Number(RD[i].childNodes[1].checked) == 1) {
            Need_value.push("1")
        } else {
            Need_value.push("0")
        }
    }


    //----------------------------- Save Question ------------

    var Text_Question;
    var TextHTML_Question;

    var Question_FormDetail = document.getElementById("Display_Question");
    Text_Question = Question_FormDetail.innerText // TEXT
    TextHTML_Question = Question_FormDetail.innerHTML // HTML TEXT

    //---------- Check ข้อมูลต้องถูก Input ให้ครบ ----
    var TextAleart = "";
    var Check = true
    var Check_Ans_Text = Ans_TextDisplay.indexOf(""); // -1 คือ ครบ
    var Check_Ans = Ans_Value.indexOf(1) // ต้องไม่เท่ากับ -1             


    if (Check_Ans_Text != -1) {
        TextAleart = TextAleart + " <p style='color: red;'>- กรุณาใส่คำตอบให้ครับ </p>"
        Check = false
    }

    if (Check_Ans == -1) {
        TextAleart = TextAleart + " <p style='color: red;'>- กรุณากำหนดคำตอบที่ถูกต้อง</p>"
        Check = false
    }

    if (Text_Question == "") {
        TextAleart = TextAleart + " <p style='color: red;'>- กรุณาใส่คำถาม</p>"
        Check = false
    }



    if (Check == true) {

        $.ajax({
            type: 'POST',
            url: '../Exam/InseartExam',
            dataType: 'json',
            data: {
                LastSeq: LastSeq, QuestionCount: QuestionCount, ValueCodeQuestion: ValueCodeQuestion, ValueCodeAnswer: ValueCodeAnswer,
                Ans_TextDisplay: Ans_TextDisplay, Ans_Text_HTML_Display: Ans_Text_HTML_Display, Ans_Value: Ans_Value, Need_value: Need_value,
                Text_Question: Text_Question, TextHTML_Question: TextHTML_Question
            },
            success: function (response) {
                if (response.success == true) {

                    Swal.fire({
                        position: 'top-mid',
                        icon: 'success',
                        title: ("Save exam success "),
                        showConfirmButton: true,
                        //   timer: 1700
                    }).then(function (result) {

                        location.reload();

                    });

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: (response.responseText)
                    })
                }

            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });



    } else {

        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            html: TextAleart,
        })
        return false;

    }




}

function Add_Ans() {

    var count_row = $('#LB_Ans_Count').text();
    if (count_row < 5) {

        AnsrowCount++
        var newid_Dp = "Display_Answer_" + AnsrowCount
        var newid_Rd = "RD_Ans_" + AnsrowCount
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
    } else {

        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: ("เพิ่มได้ สูงสุด 5 ข้อ")
        })

    }


}



function Save() {

    var HTMLText = document.getElementById('Display_Modal').innerHTML
    DeleteHTML(TempDisplayID)
    InputHTML(TempDisplayID, HTMLText)
    $('#Modal_Summernote').modal('hide')
}







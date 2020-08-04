
function GetExamDetail(Itemcode) {
 
    $.ajax({
        type: 'POST',
        url: '../Exam/GetExamDetail',
        dataType: 'json',
        data: { Itemcode: Itemcode },
        success: function (response) {
    
            if (response.success == true) {
            
                Max_Seq = response.max_Seq
                QuestionCount = response.questionCount
                ValueCodeQuestion = response.valueCodeQuestion
                ValueCodeAnswer = response.valueCodeAnswer
                ItemName = response.itemName
           var     Detail = response.detail
                $('#LB_Exam_Count').text(QuestionCount);
                $('#LB_Exam_Name').text(ItemName);
              
                MakeTable(Detail);

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


function CountAns(type) {
    if (type == 'new') {
        var parent = document.getElementById("FormDisplay_Answer_New");
        var eee = 0
        eee = parent.getElementsByClassName("ANS_New");
        var count = eee.length
        $('#LB_Ans_Count_New').text(count);
    } else {
        var parent = document.getElementById("FormDisplay_Answer_Edit");
        var eee = 0
        eee = parent.getElementsByClassName("ANS_Edit");
        var count = eee.length

        $('#LB_Ans_Count_Edit').text(count);

    }
   


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





$(document).on('click', 'button.remove-new', function (e) {
  
    var parent = document.getElementById("FormDisplay_Answer_New");
    var nodesSameClass = 0
    nodesSameClass = parent.getElementsByClassName("ANS");

    if (nodesSameClass.length > 1) {

        e.preventDefault();

        $(this).closest('div.ANS_New').remove();
    }


    CountAns('new')

});

$(document).on('click', 'button.remove-edit', function (e) {

    var parent = document.getElementById("FormDisplay_Answer_Edit");
    var nodesSameClass = 0
    nodesSameClass = parent.getElementsByClassName("ANS_Edit");

    if (nodesSameClass.length > 1) {

        e.preventDefault();

        $(this).closest('div.ANS_Edit').remove();
    }


    CountAns('Edit')

});



function Save_Exam(job) {



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

            Insert_Exam(job)
            
        }
    })


}



function Insert_Exam(job) {
    //------------------ Save Ans ----------------------
  

    var CB_Need_class
    var FormDisplay_class
    var ANS_class
    var Display_Answer_class
    var RD_Display_class
    var Display_Question
    if (job == 'new') {
        CB_Need_class = "CB_Need_New";
        FormDisplay_class = "FormDisplay_Answer_New";
        ANS_class = "ANS_New"
        Display_Answer_class = "Display_Answer_New"
        RD_Display_class = "RD_Display_New"
        Display_Question ="Display_Question_New"   
    } else {
        CB_Need_class = "CB_Need_Edit";
        FormDisplay_class = "FormDisplay_Answer_Edit";
        ANS_class = "ANS_Edit"
        Display_Answer_class = "Display_Answer_Edit"
        RD_Display_class = "RD_Display_Edit"
        Display_Question = "Display_Question_Edit"  
    }




    var Ans_TextDisplay = [];
    var Ans_Text_HTML_Display = [];
    var Ans_Value = [];
  
    var Need_value = document.getElementById(CB_Need_class);
    Need_value = Need_value.checked;

    var parent = document.getElementById(FormDisplay_class);
    var nodesSameClass = 0
    nodesSameClass = parent.getElementsByClassName(ANS_class);
    var AnsCount = nodesSameClass.length
    var Display = document.getElementsByClassName(Display_Answer_class);
    var RD = document.getElementsByClassName(RD_Display_class);



    for (var i = 0; i <= AnsCount - 1; i++) {
        Ans_Text_HTML_Display.push(Display[i].innerHTML);  // HTML TEXT
        Ans_TextDisplay.push(Display[i].innerText);   //  TEXT
        Ans_Value.push(Number(RD[i].childNodes[0].checked)); // RadioValue
    
    }


    //----------------------------- Save Question ------------

    var Text_Question;
    var TextHTML_Question;

    var Question_FormDetail = document.getElementById(Display_Question);
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
                Max_Seq: Max_Seq, QuestionCount: QuestionCount, ValueCodeQuestion: ValueCodeQuestion, ValueCodeAnswer: ValueCodeAnswer,
                Ans_TextDisplay: Ans_TextDisplay, Ans_Text_HTML_Display: Ans_Text_HTML_Display, Ans_Value: Ans_Value, Need_value: Need_value,
                Text_Question: Text_Question, TextHTML_Question: TextHTML_Question, job: job, OP_UPD: OP_UPD, DisplayOrder: DisplayOrder
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

function Add_Ans(type) {
    if (type == "new") {


        var count_row = $('#LB_Ans_Count_New').text();
        if (count_row < 5) {

            AnsrowCount++
            var newid_Dp = "Display_Answer_New_" + AnsrowCount
            var newid_Rd = "RD_Ans_New_" + AnsrowCount
            var newel = $('.ANS_New:last').clone();
            var replaseID_RD = newel[0].getElementsByClassName('RD_Display_New');
            replaseID_RD = replaseID_RD[0].childNodes[0].id
            var replaseID_Display = newel[0].getElementsByClassName('Display_Answer_New');
            replaseID_Display = replaseID_Display[0].id
            replaseID_RD = new RegExp(replaseID_RD, 'g');
            replaseID_Display = new RegExp(replaseID_Display, 'g');
            var HTMLText = $('.ANS_New:last').clone().html();
            HTMLText = HTMLText.replace(replaseID_Display, newid_Dp).replace(replaseID_RD, newid_Rd)
            newel[0].id = 'ANS_New' + AnsrowCount
            newel[0].innerHTML = HTMLText
            $(newel).insertAfter(".ANS_New:last")
            $('#' + newid_Dp + '').empty();
            CountAns('new')
        } else {

            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: ("เพิ่มได้ สูงสุด 5 ข้อ")
            })

        }
    } else {
       
        var count_row = $('#LB_Ans_Count_Edit').text();
        if (count_row < 5) {

            if (count_row > AnsrowCount_Edit) {
                AnsrowCount_Edit = count_row 
                AnsrowCount_Edit++
            }
            else {
                AnsrowCount_Edit ++
            }


            var newid_Dp = "Display_Answer_Edit_" + AnsrowCount_Edit
            var newid_Rd = "RD_ANS_Edit_" + AnsrowCount_Edit
            var newel = $('.ANS_Edit:last').clone();
            var replaseID_RD = newel[0].getElementsByClassName('RD_Display_Edit');
            replaseID_RD = replaseID_RD[0].childNodes[0].id
            var replaseID_Display = newel[0].getElementsByClassName('Display_Answer_Edit');
            replaseID_Display = replaseID_Display[0].id
            replaseID_RD = new RegExp(replaseID_RD, 'g');
            replaseID_Display = new RegExp(replaseID_Display, 'g');
            var HTMLText = $('.ANS_Edit:last').clone().html();
            HTMLText = HTMLText.replace(replaseID_Display, newid_Dp).replace(replaseID_RD, newid_Rd)
            newel[0].id = 'ANS_Edit_' + AnsrowCount_Edit
            newel[0].innerHTML = HTMLText
            $(newel).insertAfter(".ANS_Edit:last")
            $('#' + newid_Dp + '').empty();
            CountAns('Edit')

        } else {

            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: ("เพิ่มได้ สูงสุด 5 ข้อ")
            })

        }

    }

}



function Save() {
    $('#Modal_Summernote').modal('hide')
    var HTMLText = document.getElementById('Display_Modal').innerHTML
    DeleteHTML(TempDisplayID)
    InputHTML(TempDisplayID, HTMLText)

   
   
}







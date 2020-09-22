
function GetExamCategoryType_Approved() {
   
    $.ajax({
        type: 'POST',
        url: '../Exam/GetCategoryType_Approved',
        dataType: 'json',      
        success: function (response) {
            if (response.length != 0) {
                $.each(response, function (i, div) {
                    $("#DDL_ExamCategoryType_Approved").append('<option value="' + div.value + '">' + div.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
};


function GetExamCategory_Approved(ItemCategType) {
   
    $.ajax({
        type: 'POST',
        url: '../Exam/GetCategory_Approved',
        dataType: 'json',  
        data: { ItemCategType: ItemCategType},
        success: function (response) {
            if (response.length != 0) {
                $.each(response, function (i, div) {
                    $("#DDL_ExamCategory_Approved").append('<option value="' + div.value + '">' + div.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
};

function GetExamname_Approved(Category) {

    $.ajax({
        type: 'POST',
        url: '../Exam/GetExamname_Approved',
        dataType: 'json',
        data: { Category: Category },
        success: function (response) {
            if (response.length != 0) {
                $.each(response, function (i, div) {
                    $("#DDL_ExamName_Approved").append('<option value="' + div.value + '">' + div.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });

};



function GetTableDetail(ValueCodeQuestion, ValueCodeAnswer) {

    if (TableTarget != null) {
        TableTarget.destroy();
    }

    try {
        TableTarget = $("#MyTable").DataTable({

            searching: false,
            ordering: false,
            serverSide: true,
            processing: true,
            cache: true,
            ajax: ({
                type: "post",
                url: "../Exam/Approved_Detail",
                dataSrc: "data",
                data: { ValueCodeQuestion: ValueCodeQuestion },
                dataType: "json",
                //success: function (response) {
                //    if (response.length != 0) {
                        
                //        Rewrite_Master = response.rewrite_Master
                //    }
                //},
            }),

            dom: '<"top">rtl<"bottom">ip<"clear">',
            
        
            columns: [
                {
                    //data: "Delete",
                    //render: function (data, type, row)
                    data: null,
                    className: "center",
                    "fnCreatedCell": function (nTd, sData, oData, iRow, iCol) {

                        var Target = oData.seq + "," + oData.valueStatus + "," + oData.rewrite_Master;
            
                        $(nTd).html('<input type="checkbox"   class="editor-active" id="CB_Delete"  name="CB_Delete" value="' + Target +'"  />');

                    }, className: "text-center"
                },
                { data: "seq", name: "seq", class: "text-wrap text-center" },
                { data: "question", name: "question", class: "text-wrap text-center" },
                { data: "total_ANS", name: "total_ANS", class: "text-wrap text-center" },
                {                 
                    data: null,
                    render: function (data, type, row) {
                        var valueStatus = row.valueStatus.trim();
                         
                        if (valueStatus == 'NEW')
                        {
                            return " <label  class='text-success font-weight-bold'>" + valueStatus + "</label> ";
                        }
                        else if (valueStatus == 'UPD')
                        {
                            return "<label  class='text-primary font-weight-bold'>" + valueStatus + "</label>";
                        }
                        else if (valueStatus == 'DEL')
                        {
                            return "<label  class='text-danger font-weight-bold'>" + valueStatus + "</label>";
                        }
                        else
                        {
                            return "";
                        }
                   

                    }, class: "text-wrap text-center"

                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var Seq_ = row.seq;
                        var valueStatus = row.valueStatus;

                        return "<a href='#' class='btn_response btn btn-info w-auto text-white 'title='View Detail' onclick=ViewDetail('" + encodeURIComponent(Seq_) + "','" + encodeURIComponent(ValueCodeQuestion) + "','" + encodeURIComponent(ValueCodeAnswer) + "','" + encodeURIComponent(valueStatus)+"'); > " +
                            "<i class='fas fa-book'></i>" +
                            " Detail </a>";                     

                    }, class: "text-wrap text-center"
                },
            ],

            order: [0, "asc"], 

        });

    } catch (e) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: ("table:" + e)
        })


    }
    finally {

        $('#Display').show();
        $("#example-select-all").prop("checked", false);
    }
};



function ViewDetail(seq, ValueCodeQuestion, ValueCodeAnswer, ValueStatus) {

    seq = decodeURIComponent(seq);
    ValueCodeQuestion = decodeURIComponent(ValueCodeQuestion);
    ValueCodeAnswer = decodeURIComponent(ValueCodeAnswer);
    ValueStatus = decodeURIComponent(ValueStatus);

    $.ajax({
        type: 'POST',
        url: '../Exam/View_QuestionDetail',
        dataType: 'json',
        data: { seq: seq, ValueCodeQuestion: ValueCodeQuestion, ValueCodeAnswer: ValueCodeAnswer, ValueStatus: ValueStatus},
        success: function (response) {
            if (response.success == true) {
      


                //if (ValueStatus == "DEL") {
                //    $('#Edit-tab').addClass("disabled");



                //} else {

                //    Edit_Question_Approved(ValueCodeAnswer, ValueCodeQuestion, seq);


                //    $('#Edit-tab').removeClass("disabled");
                //}

                var text = response.responseText;
                DeleteHTML('Modal_body_ShowDetail');
                InputHTML('Modal_body_ShowDetail', text)

                //$('#Edit').removeClass("active");
                //$('#Edit').removeClass("show");
                //$('#Edit-tab').removeClass("active");
                //$('#Edit-tab').attr("aria-expanded", "false");

                //$('#Detail-tab').addClass("active");             
                //$('#Detail-tab').attr("aria-expanded", "true");
                //$('#Detail').addClass("active show");


                $('#Modal_ShowDetail').modal('show');

           

   
              
            }
        },

        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
};











function Approved_And_Reject(Job, valueStatus_Array, seq_Array, ValueCodeQuestion, Rewrite_Master) {
 
    Rewrite_Master
    $.ajax({
        type: 'POST',
        url: '../Exam/Job_Reject_And_Approved',
        dataType: 'json',
        data: { Job: Job, valueStatus_Array: valueStatus_Array, seq_Array: seq_Array, valueCodeQuestion: ValueCodeQuestion, Rewrite_Master: Rewrite_Master },
        success: function (response) {
            if (response.success == true) {
              
                Swal.fire({
                    icon: 'success',
                    title: response.textresponse,
                 
                }).then(function (result) {
                    GetTableDetail(ValueCodeQuestion, ValueCodeAnswer);
                });

            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: response.textresponse,
                })
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });



};




function SubmitData(Job) {
    
    var text_class;
    var text_Job;
    if (Job == 'Reject') {
        text_class = 'text-danger';
        text_Job = 'REJ'
    } else {
        text_class = 'text-success';
        text_Job = 'APP'
    }


    var arrdata = TableTarget.$('input, select').serializeArray();
    var seq_Array = new Array();
    var valueStatus_Array = new Array();


    if (arrdata.length != 0) {

        Swal.fire({
            title: "Are you sure you want to reject?",
            html: "The total number of questions to be " + Job + " is <b class='text-danger'> " + arrdata.length + " </b> . Are you sure you want to <b class='" + text_class+"'>" + Job + " ?<b/>",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Do it!'

        }).then(function (result) {
            if (result.value) {
           
                for (i = 0; i < arrdata.length; i++) {
                    arrtemp = arrdata[i].value.split(',');
                 
                    seq_Array.push(arrtemp[0]);
                    valueStatus_Array.push(arrtemp[1]);
                    Rewrite_Master = arrtemp[2].toString();
                    arrtemp = [];
                }

                var Status = valueStatus_Array.toString();
                var seq_String = seq_Array.toString();
            
                Approved_And_Reject(text_Job, Status, seq_String, ValueCodeQuestion, Rewrite_Master);

            }
        });


    } else {

        Swal.fire({
            title: "Opss..!!",
            text: "Plase Select Question",
            icon: 'error',
        });
    }




};



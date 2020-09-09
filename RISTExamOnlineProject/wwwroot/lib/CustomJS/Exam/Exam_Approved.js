


function GetExamCategory_Approved() {
   
    $.ajax({
        type: 'POST',
        url: '../Exam/GetCategory_Approved',
        dataType: 'json',       
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

            }),

            dom: '<"top">rtl<"bottom">ip<"clear">',

        
            columns: [
                {
                    //data: "Delete",
                    //render: function (data, type, row)
                    data: null,
                    className: "center",
                    "fnCreatedCell": function (nTd, sData, oData, iRow, iCol) {

                        var Target =  oData.seq + "," + oData.valueStatus
                
                        $(nTd).html('<input type="checkbox"   class="editor-active" id="CB_Delete"  name="CB_Delete" value="' + Target +'"  />');

                    }, className: "text-center"
                },
                { data: "seq", name: "seq", class: "text-wrap text-center" },
                { data: "question", name: "question", class: "text-wrap text-center" },
                { data: "total_ANS", name: "total_ANS", class: "text-wrap text-center" },
                {
                    //data: "valueStatus", name: "valueStatus", class: "text-wrap text-center"

                    data: null,
                    render: function (data, type, row) {
                        var valueStatus = row.valueStatus.trim();                   
                        if (valueStatus == 'NEW') {
                            return " <label  class='text-success font-weight-bold'>" + valueStatus +"</label> ";                              
                        } else if (valueStatus == 'UPD') {
                            return "<label  class='text-primary font-weight-bold'>" + valueStatus + "</label>";
                        } else if (valueStatus == 'DEL') {
                            return "<label  class='text-danger font-weight-bold'>" + valueStatus + "</label>";
                        } else {
                           return "";
                        }                  


                    }, class: "text-wrap text-center"

                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var Seq_ = row.seq;
                        var valueStatus = row.valueStatus;

                        return "<a href='#' class='btn_response btn btn-info w-auto text-white ' onclick=ViewDetail('" + encodeURIComponent(Seq_) + "','" + encodeURIComponent(ValueCodeQuestion) + "','" + encodeURIComponent(ValueCodeAnswer) + "','" + encodeURIComponent(valueStatus)+"'); > " +
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
      


                if (ValueStatus == "DEL") {
                    $('#Edit-tab').addClass("disabled");



                } else {
                    $('#Edit-tab').removeClass("disabled");
                }



                var text = response.responseText;
                DeleteHTML('Detail');
                InputHTML('Detail', text)

                $('#Edit').removeClass("active");
                $('#Edit').removeClass("show");
                $('#Edit-tab').removeClass("active");
                $('#Edit-tab').attr("aria-expanded", "false");

                $('#Detail-tab').addClass("active");             
                $('#Detail-tab').attr("aria-expanded", "true");
                $('#Detail').addClass("active show");


                $('#Modal_ShowDetail').modal('show');

           

   
              
            }
        },

        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
};


function DataApproved() {

    var arrdata = TableTarget.$('input, select').serializeArray();
    var seq_Array = new Array();
    var valueStatus_Array = new Array();

    if (arrdata.length != 0) {

        Swal.fire({
            title: "Are you sure you want to approve?",
            html: "The total number of questions to be approved is <b class='text-danger'> " + arrdata.length + " </b> . Are you sure you want to <b class='text-success'>Approve ?<b/>",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Approve it!'

        }).then(function (result) {
            if (result.value) {

                for (i = 0; i < arrdata.length; i++) {         
                    arrtemp = arrdata[i].value.split(','); 
                    seq_Array.push(arrtemp[0]);
                    valueStatus_Array.push(arrtemp[1]);
                    arrtemp = [];
                }
                Approved_And_Reject('APP', valueStatus_Array, seq_Array, ValueCodeQuestion);     
            }
        });


    } else    {

        Swal.fire({
            title: "Opss..!!",
            text: "Plase Select Question",
            icon: 'error',
        });
    }


  

}





function DataReject() {

    var arrdata = TableTarget.$('input, select').serializeArray();
    var seq_Array = new Array();
    var valueStatus_Array = new Array();

    if (arrdata.length != 0) {

        Swal.fire({
            title: "Are you sure you want to reject?",
            html: "The total number of questions to be approved is <b class='text-danger'> " + arrdata.length + " </b> . Are you sure you want to <b class='text-danger'>Reject ?<b/>",
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
                    arrtemp = [];
                }
                Approved_And_Reject('REJ', valueStatus_Array, seq_Array, ValueCodeQuestion);
            }
        });


    } else {

        Swal.fire({
            title: "Opss..!!",
            text: "Plase Select Question",
            icon: 'error',
        });
    }




}





function Approved_And_Reject(Job, valueStatus_Array, seq_Array, ValueCodeQuestion) {

    $.ajax({
        type: 'POST',
        url: '../Exam/Job_Reject_And_Approved',
        dataType: 'json',
        data: { Job: Job, valueStatus_Array: valueStatus_Array, seq_Array: seq_Array, valueCodeQuestion: ValueCodeQuestion },
        success: function (response) {
            if (response.success == true) {


                GetTableDetail(ValueCodeQuestion, ValueCodeAnswer);


            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });



}
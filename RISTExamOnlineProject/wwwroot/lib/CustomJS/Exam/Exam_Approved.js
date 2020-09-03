



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

            //dom: '<"top"B>rt<"bottom">ip<"clear">',


            dom: '<"top"B>rt<"bottom"lip><"clear">',
      
            buttons: [
                {
                    text: "My button",
               className:"btn btn-dark m-2",
                    action: function (e, dt, node, config) {
                        alert('Button activated');
                    }
                }
            ],
        
            columns: [
                {
                    //data: "Delete",
                    //render: function (data, type, row)
                    data: null,
                    className: "center",
                    "fnCreatedCell": function (nTd, sData, oData, iRow, iCol) {
                        //var Target = oData.WFLotNo + "," + oData.LotsCount

                        $(nTd).html('<input type="checkbox"   class="editor-active" id="CB_Delete"  name="CB_Delete" value=""  />');

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
                debugger
                var text = response.responseText;
                DeleteHTML('Modal_body_ShowDetail');
                InputHTML('Modal_body_ShowDetail', text)

                $('#Modal_ShowDetail').modal('show');

              
            }
        },

        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
};

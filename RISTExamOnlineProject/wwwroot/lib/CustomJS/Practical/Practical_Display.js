var getParams = function (url) {
    debugger
    var params = {};
    var parser = document.createElement('a');
    parser.href = url;
    var query = parser.search.substring(1);
    var vars = query.split('&');
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        params[pair[0]] = decodeURIComponent(pair[1]);
    }

    return params;
};


function SaveDetail(QuestionNo_) {
    
    QuestionNo = QuestionNo_    
 
    var HearingJudge;
    var PracticalJudge;
    var ActualTimeJudge;
    var Judge;
    var CodeHTML;

    Swal.fire({
        icon: 'warning',
        title: 'คุณแน่ใจไหม?',
        text: "คุณแน่ใจไหม ที่จะบันทึกผลการทอดสอบ ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'ใช่, บันทึกผล!',
        cancelButtonText : 'ไม่ใช่',
    }).then(function (result) {
        if (result.value) {




            

            HearingJudge = $('input[name=Hearing_' + QuestionNo_ + ']:checked').val();
            PracticalJudge = $('input[name=Practical_' + QuestionNo_ + ']:checked').val();

            var Minutes = $('#Minutes_' + QuestionNo_ + '').text();
            var Seconds = $('#Seconds_' + QuestionNo_ + '').text();


            var Time_Practical = '00:' + Minutes + ':' + Seconds;
            var a = Time_Practical.split(':');
            var ActualTime_Seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);



            var Min_time = $('#Min_Time_' + QuestionNo_ + '').text();
            Min_time = '00:' + Min_time
            var b = Min_time.split(':');
            var Min_time_Seconds = (+b[0]) * 60 * 60 + (+b[1]) * 60 + (+b[2]);

            var Max_time = $('#Max_Time_' + QuestionNo_ + '').text();
            Max_time = '00:' + Max_time
            var c = Max_time.split(':');
            var Max_time_Seconds = (+c[0]) * 60 * 60 + (+c[1]) * 60 + (+c[2]);


            if ((ActualTime_Seconds < Max_time_Seconds) && (ActualTime_Seconds > Min_time_Seconds)) {
                ActualTimeJudge = 1;

            } else {
                ActualTimeJudge = 0;
            }

            if (HearingJudge != undefined && PracticalJudge != undefined) {
             //   pause(QuestionNo_);
                stop(QuestionNo_);

                $.ajax({
                    type: 'POST',
                    url: '../PracticalExam/Savepractical',
                    dataType: 'json',
                    data: {
                        Flag: "UPD", Staffcode: Staffcode, PlanID: PlanID, LicenseName: LicenseName, ItemID: ItemID, QuestionNo: QuestionNo,
                        HearingJudge: HearingJudge, ActualTime_Seconds: ActualTime_Seconds, PracticalJudge: PracticalJudge, ActualTimeJudge: ActualTimeJudge, OPID: OPID
                    },
                    success: function (response) {
                        if (response.success == true) {

                            Judge = response.judge
                            debugger

                            if (Judge == 1) {
                                CodeHTML = "<h2 class='font-weight-bold text-success'> ผ่าน </h2>";
                                $('#LINK_' + QuestionNo_).addClass("bg-success text-white badge-success");


                            } else {
                                CodeHTML = "<h2 class='font-weight-bold text-danger'> ไม่ผ่าน </h2>";
                                $('#LINK_' + QuestionNo_).addClass("bg-danger text-white badge-danger");
                            }

                            $("#BTN_Backward_" + QuestionNo_ + ", #BTN_Forward_" + QuestionNo_).hide();

                            //$("#DIV_Forward_and_Back_" + QuestionNo_).hide();

                            Swal.fire({
                                position: 'top-mid',
                                icon: 'success',
                                title: response.responetext,
                                html: "<h2 class='font-weight-bold'>  ผลการทดสอบ </h2>  " + CodeHTML,
                                showConfirmButton: true,
                          
                            }).then(function (result) {

                                debugger

                              

                                var clickfun = $("#LINK_" + QuestionNo_).attr("onClick");
                                 var funname = clickfun.substring(0, clickfun.indexOf("("));     
                                $("#LINK_" + QuestionNo_).attr("onclick", funname + "('" + QuestionNo_ + "'," + "'" + Judge + "')");

                                DeleteHTML('button_wrap_' + QuestionNo_);                            
                
                                InputHTML('button_wrap_' + QuestionNo_, CodeHTML);      
                                $('input[name=Hearing_' + QuestionNo_ + ']').prop('disabled', true);
                                $('input[name=Practical_' + QuestionNo_ + ']').prop('disabled', true);                                               
                       
                                                                     
                                document.getElementById('BTN_Save_' + QuestionNo_).remove();
                                document.getElementById('BTN_Backward_' + QuestionNo_).remove();
                                document.getElementById('BTN_Forward_' + QuestionNo_).remove();


                               // Disabled(QuestionNo_);
                              //   window.location = window.location;
                              //  location.reload();                              
                              //  MakeDisplayPractical();
                              

                            });


                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: response.responetext,
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
                    title: 'อุ๊ป!!...',
                    text: ("กรุณเลือกคำตอบให้ครบทุกข้อ")
                })

                return false;


            }





        } else {
            return false;

        }

    })


 





    //@Flag			char(3), --ADD, UPD
    //@Staffcode		nvarchar(50),
    //@PlanID			nvarchar(50),
    // @LicenseName	nvarchar(50),
    //@ItemID			int,
    //@DetailNo		int,
    //@HearingJudge	bit,
    //@ActualTime		time(7),
    //@PracticalJudge bit,
    //@Judge			bit,
    //@UserName		nvarchar(50)




}

function Disabled(QuestionNo_) {
    

   

    var RD_Practical = document.getElementsByName("Practical_" + QuestionNo_ + "");
    for (var i = 0; i < RD_Practical.length; i++) {
        RD_Practical[i].disabled = true;
    }

    var RD_Hearing = document.getElementsByName("Hearing_" + QuestionNo_ + "");
    for (var i = 0; i < RD_Hearing.length; i++) {
        RD_Hearing[i].disabled = true;
    }

    var btn = document.getElementsByName("QuestionNo_" + QuestionNo_ + "");
    for (var i = 0; i < btn.length; i++) {
        btn[i].disabled = true;
    }

    //var btn = document.getElementById("button_wrap_" + QuestionNo_+""); 
    //for (var i = 0; i < btn.children.length; i++) {
    //    btn.children[i].disabled = true;       
    //}



};

function GetParameter(QueryString, Type) {

    Type = Type.toUpperCase();  


    const UrlParams = new URLSearchParams(QueryString);
    var Value = UrlParams.get(Type);



    if (Value != undefined) {
        return Value;
    }
    else {

        return "";
    }

}



function MakeDisplayPractical() {
    
    //var Parameter = {};

    //Parameter = getParams(document.URL)
    //Staffcode = Parameter.Staffcode;
    //PlanID = Parameter.PlanID;
    //ItemID = Parameter.ItemID;
    //LicenseName = Parameter.LicenseName;
    debugger


    try {
        const QueryString = window.location.search.toUpperCase();
        ItemID = GetParameter(QueryString, 'ItemID');
        Staffcode = GetParameter(QueryString, 'Staffcode');
        PlanID = GetParameter(QueryString, 'PlanID');
        LicenseName = GetParameter(QueryString, 'LicenseName');

        ItemID = decodeURIComponent(ItemID);
        Staffcode = decodeURIComponent(Staffcode);
        PlanID = decodeURIComponent(PlanID);
        LicenseName = decodeURIComponent(LicenseName);


        $.ajax({
            type: 'POST',
            url: '../PracticalExam/MakeDisplayPractical',
            dataType: 'json',
            data: { OPID: OPID, Staffcode: Staffcode, PlanID: PlanID, ItemID: ItemID, LicenseName: LicenseName },

            success: function (response) {
                if (response.success == true) {

                    var HTML_Text = response.responetext

                    DeleteHTML('Main_Display');
                    InputHTML('Main_Display', HTML_Text);


                    //$("#pause_1, #resume_1").hide();
                    //$("#start_1").show();
                    //$("#reset_1, #stop_1, #DIV_Forward_and_Back_1").hide();



                } else {
                    DeleteHTML('Main_Display');

                }
            },
            error: function (ex) {
           

                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: ex,

                })
            }

        });
    } catch (e) {

        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: e,

        })

    }
    




}





function SwithNO(Index_,Judge_) {
  debugger 
    if (Judge_ == '') {
        Index = Index_
        counter = 0;
        pauseClock();
        $('#ActualTime_' + Index).removeClass("bg-success bg-danger  text-white");
        $('#Minutes_' + Index + '').removeClass("text-white");
        $('#Seconds_' + Index + '').removeClass("text-white");
        $('#LB_ActualTime_' + Index + '').removeClass("text-white");
        $("#pause_" + Index + ", #resume_" + Index + ",#BTN_Save_" + Index+"").hide();
        $("#start_" + Index).show();
        $("#reset_" + Index + ", #stop_" + Index + "").hide();
        $("#BTN_Backward_" + Index + ", #BTN_Forward_" + Index).hide();
        resetClock();
    }
   
}


//$('#Carousel_Question').bind('slide.bs.carousel', function (e) {
//    debugger
//    console.log('slide event!');
//});


//$('#Carousel_Question').bind('slid', function (e) {
//    debugger
//    console.log("slid event!");
//});
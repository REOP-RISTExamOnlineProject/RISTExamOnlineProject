
function GetPlanID(Staffcode_, planID, licenseName) {   
    

    

    $.ajax({
        type: 'POST',
        url: '../PracticalExam/GetPlanID',
        dataType: 'json',
        data: { OPID: OPID, Staffcode: Staffcode_, planID: planID, licenseName: licenseName},
        success: function (response) {
            if (response.success == true)
            {
                
                PlanID_List = response.planID;
                LicenseName_List = response.licenseName;


            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });



}

var getParams = function (url) {

    var params = {};
    var parser = document.createElement('a');
    parser.href = url;
    var query = parser.search.substring(1);
    var vars = query.split('&');
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        params[pair[0]] = decodeURIComponent(pair[1]);
    }
    Staffcode = params.Staffcode;
    return Staffcode;
};

function GetParameter(QueryString, Type) {
    
    Type = Type.toUpperCase()


    // const QueryString = window.location.search.toUpperCase();
    const UrlParams = new URLSearchParams(QueryString);
    var Value = UrlParams.get(Type);
    //   Level_App_Code = UrlParams.get('LEVEL_APP_CODE');

    //var regex = RegExp(/^\(?([0-9]{8})\)?[-. ]?([0-9]{2})$/);

    //   var regexInt = RegExp(/^[0-9]+$/);

    if (Value != undefined ) {
        return Value;
    }
    else {

        return "";
    }

}



function GetValue(QueryString) {


    Staffcode = GetParameter(QueryString, 'Staffcode');
    PlanID_List = GetParameter(QueryString, 'PlanID');
    LicenseName_List = GetParameter(QueryString, 'LicenseName');
    
    document.getElementById('LB_Staff').innerHTML = Staffcode;
    document.getElementById('LB_PlanID').innerHTML = PlanID_List;
    document.getElementById('LB_LicenseName').innerHTML = LicenseName_List;


}
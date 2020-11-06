
function GetPlanID(Staffcode_) {
    




    $.ajax({
        type: 'POST',
        url: '../PracticalExam/GetPlanID',
        dataType: 'json',
        data: { OPID: OPID, Staffcode: Staffcode_ },
        success: function (response) {
            if (response.length != 0) {
                $.each(response, function (i, div) {
                    $("#DDL_PlanID").append('<option value="' + div.value + '">' + div.text + '</option>');
                });
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


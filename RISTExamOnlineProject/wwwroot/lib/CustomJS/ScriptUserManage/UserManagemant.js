  



function LoadData(OPID) {

    $.ajax({
        type: "post",
        url: '@Url.Action("Load_Training_Record", "Home")',
        dataSrc: "data",
        data: { OPID: OPID },
        dataType: "json",

        success: function (response) {
            if (response.success == true) {
                debugger
                var Datadetail = response.data

                return Datadetail

            }
        }
    });

}

function showdata(OPID) {


    debugger
    if (TableTarget != null) {
        TableTarget.destroy();
    }

    TableTarget = $("#MyTable").DataTable({
        //  data: Datatable_Data,
        ordering: true,
        serverSide: true,
        paging: true,
        processing: true,
        cache: true,
        ajax: ({
            type: "post",
            url: '@Url.Action("Load_OperatorAdditional_Detail", "Management")',
            dataSrc: "data",
            data: { OPID: OPID },
            dataType: "json",
        }),

        dom: '<"top">rt<"bottom"ip>l<"clear">',
        columns: [
            { data: "OperatorID", name: "OperatorID", class: "text-wrap text-center" },
            { data: "SectionCode", name: "SectionCode", class: "text-wrap text-center" },
            { data: "SectionCode2", name: "SectionCode2", class: "text-wrap text-center" },
            { data: "Division", name: "Division", class: "text-wrap text-center" },
            { data: "Department", name: "Department", class: "text-wrap text-center" },
            { data: "Section", name: "Section", class: "text-wrap text-center" },
            { data: "StatusC", name: "StatusC", class: "text-wrap text-center" },
        ],
        order: [1, "asc"],

    });




}

function GetSectionCode(strDiv,strDepart,strsection) {
     
    var teste11s = $("#ddlDivision").val();
    var testes = $("#ddlDepartment").val();
    $.ajax({        
        type: 'POST',
        url: '../Management/GetSectionCode',
        dataType: 'json',
        data: { strDivision: strDiv, strDepartment: strDepart },
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) { 
                    $("#ddlSection").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
                $("#ddlSection").val((strsection != null ? strsection:""));
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
      
}


function GetDepartment(strDiv, strDepart) { 
    var testes = $("#ddlDivision").val();
    $.ajax({
        type: 'POST',
        url: '../Management/GetDepartment',
        data: { strDivision: strDiv },
        dataType: 'json',
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) {
                    $("#ddlDepartment").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
                $("#ddlDepartment").val((strDepart != null ? strDepart : ""));
               
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });

    
}

function GetDivision() {
    $.ajax({
        type: 'POST',
        url: '../Management/GetDivision', 
        dataType: 'json',
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) {
                    $("#ddlDivision").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
     
}
function GetAuthority() {
    $.ajax({
        type: 'POST',
        url: '../Management/GetAuthority',
        dataType: 'json',
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) {
                    $("#ddlAuthority").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });

}
function GetActive() {
    $.ajax({
        type: 'POST',
        url: '../Management/GetActive',
        dataType: 'json',
        success: function (citys) {
           
            if (citys.length != 0) {
                $.each(citys, function (i, city) { 
                    $("#ddlActive").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    }); 
}

function GetGroupName() {
    $.ajax({
        type: 'POST',
        url: '../Management/GetGroupName',
        dataType: 'json',
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) {
                    $("#ddlShift").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });

}



function GetChart() { 
    var radarChartCanvas = $("#radarChart_G").get(0).getContext("2d");
    var radarChart = new Chart(radarChartCanvas, {
        type: 'radar',
        data: radarChartData,
        options: radarChartOptions
    });

}

var radarChartData = {
    labels: ["Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running"],
    datasets: [
        {
            "label": "My First Dataset",
            "data": [65, 59, 90, 81, 56, 55, 40],
            "fill": true,
            "backgroundColor": "rgba(255, 99, 132, 0.2)", "borderColor": "rgb(239, 83, 80)", "pointBackgroundColor": "rgb(239, 83, 80)", "pointBorderColor": "#fff", "pointHoverBackgroundColor": "#fff", "pointHoverBorderColor": "rgb(239, 83, 80)"
        }, {
            "label": "My Second Dataset",
            "data": [28, 48, 40, 19, 96, 27, 100],
            "fill": true,
            "backgroundColor": "rgba(54, 162, 235, 0.2)", "borderColor": "rgb(57, 139, 247)", "pointBackgroundColor": "rgb(57, 139, 247)", "pointBorderColor": "#fff", "pointHoverBackgroundColor": "#fff", "pointHoverBorderColor": "rgb(57, 139, 247)"
        }
    ]
}

var radarChartOptions = {
    elements: {
        line: {
            tension: 0,
            borderWidth: 3
        }
    }
}
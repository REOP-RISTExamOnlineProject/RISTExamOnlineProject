  



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

function GetSectionCode() {
     
    $.ajax({        
        type: 'POST',
        url: '../Management/GetSectionCode',
        dataType: 'json',
       data: { strDivision : $("#ddlDivision").val(), strDepartment : $("#ddlDepartment").val() },
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) { 
                    $("#ddlSection").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
      
}


function GetDepartment() { 
    $.ajax({
        type: 'POST',
        url: '../Management/GetDepartment',
        data: { strDivision : $("#ddlDivision").val() },
        dataType: 'json',
        success: function (citys) {
            if (citys.length != 0) {
                $.each(citys, function (i, city) {
                    $("#ddlDepartment").append('<option value="' + city.value + '">' + city.text + '</option>');
                });
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


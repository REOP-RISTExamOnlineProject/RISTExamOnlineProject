  

function GetuserDetail() { 
    $.ajax({
        type: 'POST',
        url: "../Management/GetDataUserdetail",
        data: { opno: $("#strOPNo").val() },
        dataType: 'json',
        success: function (Data) { 
            if (Data.strboolbel == true && Data.strResult == "OK") {
                var _Data = Data.data; 
               
                $("#NameEn").val(_Data.nameEng);
                $("#NameTh").val(_Data.nameThai);
                $("#txtPosition").val(_Data.position);  
                $("#ddlDivision").val(_Data.division);
                $("#ddlDepartment").val(_Data.department);
                $("#ddlSection").val(_Data.sectionCode);
                $("#ddlShift").val(_Data.operatorGroup);
                $("#txtEmail").val(_Data.email1);
                $("#password").val(_Data.rfid); 
                $("#txtUpdDate").val(_Data.updDate); 
                $("#txtAddDate").val(_Data.addDate); 
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: Data.dataLabel,
                    type: 'error',
                    timer: 1700,
                }).then(function () {
                    return false;
                });

            };
        },
        error: function (ex) {
            debugger
            alert('Failed to retrieve states.' + ex.statusText);
        },
    });


}


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


$("#ddlDepartment").on("change", function () {

    $("#ddlSection option").remove();
    GetSectionCode();
});
$("#ddlDivision").on("change", function () {
    $("#ddlDepartment option").remove();
    GetDepartment();
    $("#ddlSection option").remove();
    GetSectionCode();


});
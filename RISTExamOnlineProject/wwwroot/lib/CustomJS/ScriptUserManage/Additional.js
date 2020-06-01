
function Getdata() {
    debugger

    var OPID = $("#strOPNo").val();

   
          


    debugger

            TableTarget = $("#MyTable").DataTable({               
                ordering: true,
                serverSide: true,
                paging: true,
                processing: true,
                cache: true,
                 ajax: ({
                type: "post",              
                url: "/Management/Load_OperatorAdditional_Detail",
                dataSrc: "data",
                     data: { OPID: OPID },
                dataType: "json",
            }) ,
      
                dom: '<"top"l>rt<"bottom">ip<"clear">',
                    columns: [
                        { data: "operatorID", name: "operatorID", class: "text-wrap text-center" },
                        { data: "sectionCode", name: "sectionCode", class: "text-wrap text-center" },
                        { data: "sectionCode2", name: "sectionCode2", class: "text-wrap text-center" },
                        { data: "division", name: "division", class: "text-wrap text-center" },
                        { data: "department", name: "department", class: "text-wrap text-center" },
                        { data: "section", name: "section", class: "text-wrap text-center" },
                        { data: "statusC", name: "statusC", class: "text-wrap text-center" },
                        {
                            "render": function (data, type, row) {
                                return "<a href='#' class='btn btn-danger text-white' onclick=Delete_Data('" + row.operatorID + "','" + row.sectionCode + "'); >Delete</a>";

                            }
                        },



                    ],
                        order: [1, "asc"],

                });


    
}

function Delete_Data(operatorID, sectionCode) {



}

function GetDepartment_Addition(DIV) {
    $.ajax({
        type: 'POST',
        url: '../Management/GetDepartment_Addition',
        dataType: 'json',
        data: {DIV:DIV},
        success: function (Departments) {
            if (Departments.length != 0) {
                $.each(Departments, function (i, Department) {
                    $("#DDL_Department").append('<option value="' + Department.value + '">' + Department.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });



}



function GetDivision_Addition() {
    $.ajax({
        type: 'POST',
        url: '../Management/GetDivision_Addition',
        dataType: 'json',
        success: function (Divisions) {
            if (Divisions.length != 0) {
                $.each(Divisions, function (i, div) {
                    $("#DDL_Division").append('<option value="' + div.value + '">' + div.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });

}






function GetSection_Addition(DIV,DEP) {
    $.ajax({
        type: 'POST',
        url: '../Management/GetSection_Addition',
        dataType: 'json',
        data: { DIV: DIV ,DEP: DEP},
        success: function (Sections) {
            if (Sections.length != 0) {
                $.each(Sections, function (i, Section) {
                    $("#DDL_Section").append('<option value="' + Section.value + '">' + Section.text + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });



}





$("#DDL_Department").on("change", function () {

    var DIV = $("#DDL_Division").val() 
    var DEP = $("#DDL_Department").val() 
    debugger
    if ($("#DDL_Department").val != 0) {
     
        $("#DDL_Section option").remove();     

        GetSection_Addition(DIV, DEP)
    } else {             
        $("#DDL_Section option").remove();

    }


});


$("#DDL_Division").on("change", function () {


    var DIV = $("#DDL_Division").val() 
    debugger
    if (DIV != 0) {
        debugger
        $("#DDL_Department option").remove();
        $("#DDL_Section option").remove();
        GetDepartment_Addition(DIV)

    } else {

        $("#DDL_Department option").remove();
        $("#DDL_Section option").remove();

    }   



});
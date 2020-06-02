
var TableTarget;

function Getdata(OPID) {
    debugger

   // var OPID = $("#strOPNo").val();   

    debugger
    if (TableTarget != null) {
        TableTarget.destroy();
    }

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
                        //{
                        //    "render": function (data, type, row) {
                        //        return "<a href='#' class='btn btn-danger text-white' onclick=Delete_Data('" + row.operatorID + "','" + row.sectionCode + "'); >Delete</a>";

                        //    }
                        //},
                        {
                            //data: "Delete",
                            //render: function (data, type, row)
                            data:null,
                            className: "center",
                            "fnCreatedCell": function (nTd, sData, oData, iRow, iCol) {
                                var Target = oData.sectionCode

                                $(nTd).html('<input type="checkbox"   class="editor-active" id="CB_Delete"  name="CB_Delete" value="' + Target + '"  />');

                            }, className: "dt-body-center"
                        },

                    ],
                        order: [1, "asc"],

                });

    debugger
    var x = document.getElementById("display_grid");
    x.style.display = "block";
    var a = document.getElementById("Form_Add");
    a.style.display = "block";
    var t = document.getElementById("Display_tableAdd");
    t.style.display = "none";

   // CheckData()
    
}




function CheckData() {

debugger
   // var arrdata = TableTarget.$('input, select').serializeArray();

    var TableTarget = $('#MyTable').DataTable();

    var rowCount = TableTarget.data().count() 

    if (rowCount != 0) {
        var x = document.getElementById("display_grid");
        x.style.display = "block";
        var a = document.getElementById("Form_Add");
        a.style.display = "block";
        var t = document.getElementById("Display_tableAdd");
        t.style.display = "none";
    }
    else {
        var x = document.getElementById("display_grid");
        x.style.display = "none";
        var a = document.getElementById("Form_Add");
        a.style.display = "none";
        var t = document.getElementById("Display_tableAdd");
        t.style.display = "none";
    }







}












function Delete_Data() {

    debugger

 


    var arrdata = TableTarget.$('input:checkbox false').serializeArray();

    

    var Lotcount = 0;
    var sectionCode = new Array();
    var WFLotCount = arrdata.length


    if (arrdata.length != 0) {



        for (i = 0; i < arrdata.length; i++) {
            debugger

            arrtemp = arrdata[i].value.split(',');
            Lotcount = parseInt(arrtemp[1]) + parseInt(Lotcount);
            sectionCode.push(arrtemp[1]);
            arrtemp = [];
        }
    }
    else {
        Swal.fire({
          
            title: 'Please select checkbox',
            text: '',
            type: 'error',
            timer: 1700,
        }).then(function () {
            return false;
        });


    }



    var i;

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


//$('#BTN_Delete').on('click', function () {


//    Delete_Data();


//});

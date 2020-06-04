
var TableTarget;



function Getdata(OPID) {
    debugger

   // var OPID = $("#strOPNo").val();   
  
    debugger
    if (TableTarget != null) {
        TableTarget.destroy();
    }
    try {

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
                data: { OPID: OPID, MakerID: MakerID },
                dataType: "json",
            }),

            dom: '<"top"l>rt<"bottom">ip<"clear">',
            columns: [
                { data: "operatorID", name: "operatorID", class: "text-wrap text-center" },
                { data: "sectionCode", name: "sectionCode", class: "text-wrap text-center" },

                { data: "division", name: "division", class: "text-wrap text-center" },
                { data: "department", name: "department", class: "text-wrap text-center" },
                { data: "section", name: "section", class: "text-wrap text-center" },

                //{
                //    "render": function (data, type, row) {
                //        return "<a href='#' class='btn btn-danger text-white' onclick=Delete_Data('" + row.operatorID + "','" + row.sectionCode + "'); >Delete</a>";

                //    }
                //},
                {
                    //data: "Delete",
                    //render: function (data, type, row)
                    data: null,
                    className: "center",
                    "fnCreatedCell": function (nTd, sData, oData, iRow, iCol) {
                        var Target = oData.sectionCode

                        $(nTd).html('<input type="checkbox"   class="editor-active" id="CB_Delete"  name="CB_Delete" value="' + Target + '"  />');

                    }, className: "dt-body-center"
                },

            ],
            order: [1, "asc"],

        });

    } catch (e) {
        Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: (e)
        })


    }
          
     
    var x = document.getElementById("display_grid");
    x.style.display = "block";
    var a = document.getElementById("Form_Add");
    a.style.display = "block";
    var t = document.getElementById("Display_tableAdd");
    t.style.display = "none";

   // CheckData()
    
}





function MakeDataTemp(OPID, MakerID) {

    debugger

   
    $.ajax({
        type: 'post',
        url: "/Management/GetMakeTemp_Additional",
        dataSrc: "data",
        data: { OPID: OPID, MakerID: MakerID },
        dataType: 'json',
        success: function (response) {
            if (response.success == true) {
                r
            }

        },
        error: function (ex) {
            Swal.fire({
                text: ('Failed to retrieve states.' + ex),
                type: 'error',
                timer: 1700,
            }).then(function () {
                return false;
            });
        },


    });


}

function SaveData(OPID, MakerID) {

    $.ajax({
        type: 'POST',
        url: "/Management/Save_Additional",
        dataSrc: "data",
        data: { OPID: OPID, MakerID: MakerID },
        dataType: 'json',
        success: function (response) {
            if (response.success == true) {

                Swal.fire({
                    position: 'top-mid',
                    type: 'success',
                    title: (response.responseText),
                    showConfirmButton: true,
                    timer: 1700
                }).then(function (result) {
                    // if (result.value) {
                    debugger
                 //   Getdata(OPID)
                    // }
                    $("#strOPNo").val('');
                    var x = document.getElementById("display_grid");
                    x.style.display = "none";

                    var a = document.getElementById("Form_Add");
                    a.style.display = "none";

                });


            } else {

                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: (response.responseText)
                }).then(function (result) {
                    //   if (result.value) {
                    Getdata(OPID)
                    // }
                });


            }

        }
    });


}




function CheckData() {
     
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


function AddData_Data(OPID, MakerID, SectionCode) {


    

    $.ajax({
        type: 'POST',
        url: "/Management/AddNewSectionCode_Additional",
        dataSrc: "data",
        data: { OPID: OPID, MakerID: MakerID, SectionCode: SectionCode},
        dataType: 'json',
        success: function (response) {
            if (response.success == true) {

                Swal.fire({
                    position: 'top-mid',
                    type: 'success',
                    title: (response.responseText),
                    showConfirmButton: true,
                    timer: 1700
                }).then(function (result) {
                   // if (result.value) {
                    debugger
                        Getdata(OPID)
                   // }
        

                });


            } else {

                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: (response.responseText)
                }).then(function (result) {
                 //   if (result.value) {
                        Getdata(OPID)
                   // }
                });


            }

        }
    });



}



function Delete_Data(OPID,MakerID) {
     
 


    var arrdata = TableTarget.$('input,deselect').serializeArray();

    
    var SectionCode = new Array();

    debugger

    if (arrdata.length != 0) {



        for (i = 0; i < arrdata.length; i++) {
            debugger

            arrtemp = arrdata[i].value.split(',');            
            SectionCode.push(arrtemp[0])+';';
            arrtemp = [];
        }


        debugger
        $.ajax({
            type: 'POST',
            url: "/Management/DeleteSectionCode_Additional",
            dataSrc: "data",
            data: { OPID: OPID, MakerID: MakerID, SectionCode: SectionCode},
            dataType: 'json',
            success: function (response) {
                if (response.success == true) {                                                        
                    Swal.fire({
                        position: 'top-mid',
                        type: 'success',
                        title: (response.responseText),
                        showConfirmButton: true,
                        timer: 1700
                    }).then(function (result) {
                      //  if (result.value) {
                            Getdata(OPID)
                        //}
                    });

                } else {

                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: (response.responseText)
                    }).then(function (result) {
                       // if (result.value) {
                            Getdata(OPID)
                        //}
                    });

                  


                }

            },
            error: function (ex) {              

                Swal.fire({                  
                    text: ('Failed to retrieve states.' + ex) ,
                    type: 'error',
                    timer: 1700,
                }).then(function () {
                    return false;
                });
            },


        });


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


function Getdata_(OPID) {

    debugger

    $.ajax({
        type: 'POST',
        url: "/Management/Load_OperatorAdditional_Detail",
        dataSrc: "data",
        data: { OPID: OPID },
        dataType: 'json',       
        success: function (response) {
            if (response.success == true) {

                var Dataarray = response.data
                debugger
            }
                 
                           

            //$.each(data, function (index, value) {
            //    /*console.log(value);*/
            //    event_data += '<tr>';
            //    event_data += '<td>' + value.name + '</td>';
            //    event_data += '<td>' + value.id + '</td>';
            //    event_data += '<tr>';
            //});
            //$("#list_table_json").append(event_data);
     
           
            //var x = document.getElementById("display_grid");
            //x.style.display = "block";
            //var a = document.getElementById("Form_Add");
            //a.style.display = "block";
            //var t = document.getElementById("Display_tableAdd");
            //t.style.display = "none";

        },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        },
     

    });



}



function GetDepartment_Addition(DIV) {

    debugger

    $.ajax({
        type: 'POST',
        url: '/Management/GetDepartment_Addition',
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
    debugger
    $.ajax({
        type: 'POST',
        url: '/Management/GetDivision_Addition',
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



function GetSection_Addition(DIV, DEP) {

    $.ajax({
        type: 'POST',
        url: '/Management/GetSection_Addition',
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




//$('#BTN_Delete').on('click', function () {


//    Delete_Data();


//});

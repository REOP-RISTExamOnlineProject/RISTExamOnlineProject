




//function Getdata(OPID) {
//    debugger
//    if (TableTarget != null) {
//        TableTarget.destroy();
//        TableTarget.clear();
//    }
//    debugger
//    try {

    

//        TableTarget = $("#MyTable").DataTable({      
            
//            searching: false,
           
//            ordering: true,
//            serverSide: true,
          
//            processing: true,
//            cache: true,
//            ajax: ({
//                type: "post",
//                url: "../Management/Load_OperatorAdditional_Detail",
//                dataSrc: "data",
//                data: { OPID: OPID},
//                dataType: "json",

//            }),

//            dom: '<"top"l>rt<"bottom">ip<"clear">',
//            columns: [
//                { data: "operatorID", name: "operatorID", class: "text-wrap text-center" },
//                { data: "sectionCode", name: "sectionCode", class: "text-wrap text-center" },
//                { data: "division", name: "division", class: "text-wrap text-center" },
//                { data: "department", name: "department", class: "text-wrap text-center" },
//                { data: "section", name: "section", class: "text-wrap text-center" },
//                {
//                    //   data: "Delete",
//                    //  render: function (data, type, row),
//                    data: null,
//                    name: null,
//                    className: "center",
//                    "fnCreatedCell": function (nTd, oData, ) {
//                        var Target = oData.sectionCode

//                        $(nTd).html('<input type="checkbox"   class="editor-active" id="CB_Delete"  name="CB_Delete" value="' + Target + '"  />');

//                    }, className: "dt-body-center"
//                },

//            ],
//            order: [1, "asc"],

//        });

//    } catch (e) {
//        Swal.fire({
//            icon: 'error',           
//            title: 'Oops...',
//            text: ("table:"+e)
//        })


//    }
//    finally {

//        GetDivision_Addition()

//        var x = document.getElementById("display_grid");
//        x.style.display = "block";
//        var a = document.getElementById("Form_Add");
//        a.style.display = "block";
//        var t = document.getElementById("Display_tableAdd");
//        t.style.display = "none";

//    }
          
    
//}






function MakeDataTemp(OPID, MakerID) {

    debugger   
    $.ajax({
        type: 'post',
        url: "../Management/GetMakeTemp_Additional",
        dataSrc: "data",
        data: { OPID: OPID, MakerID: MakerID },
        dataType: 'json',
        success: function (response) {
            if (response.success == true) {
                debugger
          
                Getdata(OPID)
            } else {
                Swal.fire({
                    text: ('Make Temp Data Error'),
                    icon: 'error',
                    title: 'Oops...',
                    //     timer: 1700,
                }).then(function () {
                    return false;
                });
            }

        },
        error: function (ex) {
            Swal.fire({
                text: ('MakeDataTemp :' + ex.statusText),
                type: 'error',
           //     timer: 1700,
            }).then(function () {
                return false;
            });
        },


    });


}





function SaveData(OPID, MakerID) {

    $.ajax({
        type: 'POST',
        url: "../Management/Save_Additional",
        dataSrc: "data",
        data: { OPID: OPID, MakerID: MakerID },
        dataType: 'json',
        success: function (response) {
            if (response.success == true) {

                Swal.fire({
                    position: 'top-mid',
                    icon: 'success',
                    title: (response.responseText),
                    showConfirmButton: true,
                 //   timer: 1700
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





function AddData_Data(OPID, MakerID, SectionCode) {   

    $.ajax({
        type: 'POST',
        url: "../Management/AddNewSectionCode_Additional",
        dataSrc: "data",
        data: { OPID: OPID, MakerID: MakerID, SectionCode: SectionCode},
        dataType: 'json',
        success: function (response) {
            if (response.success == true) {

                Swal.fire({
                    position: 'top-mid',
                    icon: 'success',
                    title: (response.responseText),
                    showConfirmButton: true,
                   // timer: 1700
                }).then(function (result) {
                   // if (result.value) {
                    debugger

               //     var table = $('#myTable').DataTable();
                   

                        Getdata(OPID)
                   // }
        

                });


            } else {

                Swal.fire({
                    icon: 'error',
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
            url: "../Management/DeleteSectionCode_Additional",
            dataSrc: "data",
            data: { OPID: OPID, MakerID: MakerID, SectionCode: SectionCode},
            dataType: 'json',
            success: function (response) {
                if (response.success == true) {                                                        
                    Swal.fire({
                        position: 'top-mid',
                        icon: 'success',
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
                    icon: 'error',
                    //timer: 1700,
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




function GetDepartment_Addition(DIV) {

    debugger

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
    debugger
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



function GetSection_Addition(DIV, DEP) {

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




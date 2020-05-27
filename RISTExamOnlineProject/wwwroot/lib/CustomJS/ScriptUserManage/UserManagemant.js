  

function GetuserDetail() { 
    $.ajax({
        type: 'POST',
        url: "/Management/GetDataUserdetail",
        data: { opno: $("#strOPNo").val() },
        dataType: 'json',
        success: function (Data) { 
            if (Data.strboolbel == true && Data.strResult == "OK") {
                debugger



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
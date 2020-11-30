﻿var dataTable;
var id = getUrlParameter('id');
$(document).ready(function () {

    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": (id == null) ? "/api/lotfile" : "/api/lotfile?id=" + id,
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                {
                    data: "title", width: "20%"
                },
                {
                    data: "description", width: "60%"
                },
                {
                    data: "file", width: "5%",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="${data}" download
                                class="btn btn-primary text-white"
                                style="cursor:pointer; width=100px;">
                                <i class="fas fa-file-download"></i>
                            </a>
                        </div>
                    `
                    }
                },
                {
                    data: "id",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="/Admin/Lot/Files/Upsert?id=${data}"
                               class="btn btn-warning text-white" style="cursor: pointer; width: 100px;">
                                <i class="far fa-edit"></i>
                                Edit 
                            </a>
                            <a onClick=Delete('/api/lotfile/'+${data})
                               class="btn btn-danger text-white" style="cursor: pointer"; width: 100px;">
                                <i class="far fa-trash-alt"></i>
                                Delete
                            </a>
                        </div>
`}, width: "15%"

                }
            ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}
function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the date!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
// GET PARAMETER
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;
    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};
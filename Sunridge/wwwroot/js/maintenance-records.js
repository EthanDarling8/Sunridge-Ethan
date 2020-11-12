var id = getUrlParameter('id');

var dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/maintenanceRecord/" + id,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "dateCompleted", width: "25%", render: function (data) { return moment(data).format('MMMM Do YYYY'); } },
            { data: "description", width: "25%" },
            { data: "cost", width: "25%" },
            {
                data: "id", width: "25%",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/CommonAreaAssets/MaintenanceRecords/Upsert?id=${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a onClick=Delete('/api/maintenanceRecord/'+${data}) class="btn btn-danger text-white" style="cursor: pointer; width: 100px">
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                            </div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%",
        "bAutoWidth": false,
        responsive: true
    });
});

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
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

// Function courtesy of StackOverflow: https://stackoverflow.com/questions/19491336/how-to-get-url-parameter-using-jquery-or-plain-javascript
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
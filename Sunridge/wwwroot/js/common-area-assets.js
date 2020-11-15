var dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/asset",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "name", width: "15%" },
            { data: "price", width: "15%" },
            { data: "description", width: "15%" },
            { data: "status", width: "15%" },
            { data: "date", width: "15%", render: function (data) { return moment(data).format('MMMM Do YYYY'); } },
            {
                data: "id", width: "25%",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/CommonAreaAssets/Upsert?id=${data}" class="btn btn-success text-white" style="cursor: pointer">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a onClick=Delete('/api/asset/'+${data}) class="btn btn-danger text-white" style="cursor: pointer">
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                                <a href="/Admin/CommonAreaAssets/MaintenanceRecords/Index?id=${data}" class="btn btn-dark text-white" style="cursor: pointer">
                                    <i class="far fa-clipboard"></i> Maintenance Records
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
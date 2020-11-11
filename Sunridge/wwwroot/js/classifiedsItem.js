var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/ClassifiedsItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "categoryid", width: "9%" },
            { data: "title", width: "9%" },
            { data: "desription", width: "9%" },
            { data: "price", render: $.fn.dataTable.render.number(',', '.', 2, '$'), width: "7%" },
            { data: "name", width: "9%" },
            { data: "phone", width: "9%" },
            { data: "email", width: "9%" },
            { data: "website", width: "9%" },
            { data: "timeadded", width: "9%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Classifieds/Item/Upsert?id=${data}"
                            class="btn btn-success text-white style="cursor:pointer; width:100px;">
                            <i class="far fa-edit"></i>
                            Edit
                            </a>
                            <a onClick=Delete('/api/ClassifiedsItem/'+${data})
                            class="btn btn-danger text-white style="cursor:pointer; width:100px;">
                            <i class="far fa-trash-alt"></i>
                            Delete
                            </a>
                        </div>`;
                }, width: "19%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%",
        "order": [[2, "asc"]]
    });
}

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
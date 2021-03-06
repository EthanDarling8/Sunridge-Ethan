var dataTable;



$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/classifiedsItem",
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            { data: "id", width: "5%" },
            { data: "title", width: "5%" },
            { data: "description", width: "35%" },
            { data: "price", render: $.fn.dataTable.render.number(',', '.', 2, '$'), width: "5%" },
            { data: "name", width: "5%" },
            { data: "phone", width: "5%" },
            { data: "email", width: "5%" },
            { data: "website", width: "5%" },
            //{ data: "images", width: "5%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Owner/Classifieds/ClassifiedsUpsert?id=${data}"
                            class="btn btn-warning text-black style="cursor:pointer; width:100px;">
                            <i class="far fa-edit"></i>
                            Edit
                            </a>
                            <a onClick=Delete('/api/classifiedsItem/'+${data})
                            class="btn btn-danger text-white style="cursor:pointer; width:100px;">
                            <i class="far fa-trash-alt"></i>
                            Delete
                            </a>
                        </div>`;
                }, width: "30%"
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


var dataTable;

$(document).ready(function () {
    loadList();
});
// LOSTITEM TABLE
function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": "/api/lostitem",
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                {
                    data: "image", width: "20%"
                },
                {
                    data: "date", width: "20%"
                },
                {
                    data: "name", width: "20%"
                },
                {
                    data: "description", width: "20%"
                },
                {
                    data: "id",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="/Customer/LostItem/Details?id=${data}"
                               class="btn btn-info text-white" style="cursor: pointer; width: 100px;">
                                <i class="fas fa-book-open"></i>
                                Read 
                            </a>
                            <a href="/Customer/LostItem/Upsert?id=${data}"
                               class="btn btn-success text-white" style="cursor: pointer; width: 100px;">
                                <i class="far fa-edit"></i>
                                Edit 
                            </a>
                            <a onClick=Delete('/api/LostItem/'+${data})
                               class="btn btn-danger text-white" style="cursor: pointer"; width: 100px;">
                                <i class="far fa-trash-alt"></i>
                                Delete
                            </a>
                        </div>
                        `}, width: "20%"

                }
            ],
        "lengthMenu": [[5, 10, 25, -1], [5, 10, 25, "All"]],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}
//DELETE FUNCTION
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
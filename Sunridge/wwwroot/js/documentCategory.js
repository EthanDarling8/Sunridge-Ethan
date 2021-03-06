var dataTable;

//$ means jquery. Don't run the script until the html is all loaded.
$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/documentCategory/",
            "type": "GET",
            "datatype": "json"
        },
        //The column names use camel case from when they were converted to Json
        //the ` ` are necessary in the return to return html
        "columns": [
            { data: "name", width: "70%" },
            {
                data: "id", width: "30%",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Document/Category/Upsert?documentCategoryId=${data}"
                            class="btn btn-warning text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit
                        </a>
                        <a onClick=Delete('/api/documentCategory/'+${data})
                            class="btn btn-danger text-white style="cursor:pointer; width:100px;" )>
                                <i class="far fa-trash-alt"></i> Delete
                        </a>
                    </div>`;
                }
            }
        ],
        //Display this instead if the table doesn't render
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}


function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "This will also delete all associated files, sections, and text! You will not be able to restore the data!",
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
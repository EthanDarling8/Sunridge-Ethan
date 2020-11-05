
var dataTable;

$(document).ready(function () {
    loadList();
});


function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/fireInfo",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "title", width: "20%" },
            {
                data: "file", width: "5%" ,
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/fireinfo/${data}" download
                                class="btn btn-primary text-white"
                                style="cursor:pointer; width=100px;">
                                <i class="fas fa-file-download"></i>
                            </a>
                        </div>
                    `
                }
            },
            {
                data: "link1", width: "5%",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="${data}" target="_blank"
                                class="btn btn-primary text-white"
                                style="cursor:pointer; width=100px;">
                                <i class="fas fa-external-link-alt"></i>
                            </a>
                        </div>
                    `
                }
            },
            {
                data: "link2", width: "5%",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="${data}" target="_blank"
                                class="btn btn-primary text-white"
                                style="cursor:pointer; width=100px;">
                                <i class="fas fa-external-link-alt"></i>
                            </a>
                        </div>
                    `
                }
            },
            { data: "phone", width: "10%" },
            { data: "email", width: "25%" },
            { data: "postDate", width: "10%"},
            {
                data: "id", width: "15%",
                "render": function (data) {
                    return `
                        <div class"text-center">
                            <a href="/Home/FireInfo/Details?id=${data}"
                                class="btn btn-primary text-white" 
                                style="cursor:pointer; width=100px;">
                                <i class="fas fa-info-circle"></i>
                            </a>

                            <a href="/Home/FireInfo/Upsert?id=${data}"
                                class="btn btn-secondary text-white" 
                                style="cursor:pointer; width=100px;">
                                <i class="far fa-edit"></i>
                            </a>

                            <a onClick="Delete('/api/fireInfo/'+${data})"
                                class="btn btn-warning text-white" 
                                style="cursor:pointer; width=100px;">
                                <i class="far fa-trash-alt"></i>
                            </a>
                        </div>
                    `
                }
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data.",
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
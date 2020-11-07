var dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/lot",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "lotNumber", width: "50%" },
            {
                data: "id", width: "50%",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/KeyHistory/Details?id=${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px">
                                    <i class="fa fa-eye"></i> View
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
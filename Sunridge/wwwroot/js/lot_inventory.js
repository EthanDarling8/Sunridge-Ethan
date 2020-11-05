var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": "/api/lotfile",
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                {
                    data: "id", width: "16%"
                },
                {
                    data: "lot.id", width: "16%"
                },
                {
                    data: "inventory.id", width: "16%"
                }
            ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}
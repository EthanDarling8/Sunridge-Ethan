var dataTable;

$(document).ready(function () {
    loadList();
});
// LOSTITEM TABLE
function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": "/api/lot",
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                {
                    data: "lotNumber", width: "16%"
                },
                {
                    data: "address", width: "16%"
                },
                {
                    data: "lot_Owner", "aTargets": [0], "render": function (data) { return formatList(data); }
                    , width: "16%"
                },
                {
                    data: "taxId", "aTargets": [0], "render": function (data) { return <a href="http://www3.co.weber.ut.us/psearch/tax_summary.php?id=' + data + '" class="statusd-img-top p-2 rounded mx-auto w-100" id="taxLink" >Test</a>'; }
                    , width: "16%"
                },
                {
                    data: "lot_Inventory", "aTargets": [0], "render": function (data) { return formatList(data); }
                    , width: "16%"
                },
                {
                    data: "id",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="/Admin/lot/Files/Index?lotid=${data}"
                               class="btn btn-secondary text-white" style="cursor: pointer; width: 100px;">
                                <i class="fas fa-folder"></i>
                                Files 
                            </a>
                            <a href="/Admin/lot/Upsert?id=${data}"
                               class="btn btn-warning text-white" style="cursor: pointer; width: 100px;">
                                <i class="far fa-edit"></i>
                                Edit 
                            </a>
                        </div>
                        `}, width: "16%"

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
// FORMAT LIST FUNCTION
function formatList(myList) {
    if (myList != null) {
        var formattedList = myList.replace(/ *, */g, '<br>');
        return formattedList;
    }
}
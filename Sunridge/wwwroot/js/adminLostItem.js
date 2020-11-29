var dataTable;

$(document).ready(function () {
    loadList();
});
// LOSTITEM TABLE
function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": "/api/adminlostitem",
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                {
                    data: "image", "aTargets": [0], "render": function (data) { return '<img src="\\images\\lostItems\\' + data + '" class="statusd-img-top p-2 rounded mx-auto w-100" id="collectionImg" />'; }
                    , width: "30%"
                },
                {
                    data: "status", width: "20%"
                },
                {
                    data: "name", width: "20%"
                },
                {
                    data: "date", "aTargets": [0], "render": function (data) { return formatDate(data); }
                    , width: "30%"
                },
                {
                    data: "active", width: "20%"
                },
                {
                    data: "id",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="/Admin/lostandfound/Details?id=${data}"
                               class="btn btn-info text-white" style="cursor: pointer; width: 100px;">
                                <i class="fas fa-book-open"></i>
                                Read 
                            </a>
                            <a onClick=Resolve('/api/adminLostItem/'+${data})
                               class="btn btn-success text-white" style="cursor: pointer"; width: 100px;">
                                <i class="far fa-check-square"></i>
                                Resolve
                            </a>
                            <a href="/Admin/lostandfound/Upsert?id=${data}"
                               class="btn btn-warning text-white" style="cursor: pointer; width: 100px;">
                                <i class="far fa-edit"></i>
                                Edit 
                            </a>
                            <a onClick=Delete('/api/adminLostItem/'+${data})
                               class="btn btn-danger text-white" style="cursor: pointer"; width: 100px;">
                                <i class="far fa-trash-alt"></i>
                                Delete
                            </a>
                        </div>
                        `}, width: "20%"

                }
            ],
        "sDom": 'lrtip',
        "fnInitComplete": function (oSettings, json) {
            addSearchControl(json);
        },
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
//RESOLVE FUNCTION
function Resolve(url) {
    swal({
        title: "Are you sure you want to resolve this listing?",
        text: "This will remove the listing!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'POST',
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
// FILTERING GENERAL LOST AND FOUND FUNCTION
function addSearchControl(json) {
    $("#DT_load thead").append($("#DT_load thead tr:first").clone());                           // Clone the First row
    $("#DT_load thead tr:eq(1) th").each(function (index) {                                     // Look at the header of each column for the cloned row
        if (index == 0) { $(this).replaceWith('<th class="w-100 mx-auto"></th >'); }                                              // Replace button column header with an empty header (no search bar)
        if (index == 1) {
            var statusDropDown = $('<select class="w-auto mx-auto"/>');
            statusDropDown.append($('<option class="w-auto mx-auto"/>').attr('value', '').text('Select'));
            statusDropDown.append($('<option/>').attr('value', 'Lost').text('Lost'));
            statusDropDown.append($('<option/>').attr('value', 'Found').text('Found'));
            $(this).replaceWith('<th class="w-auto mx-auto">' + $(statusDropDown).prop('outerHTML') + '</th>');
            var searchControl = $("#DT_load thead tr:eq(1) th:eq(" + index + ") select");
            searchControl.on('change', function () {
                dataTable.column(index).search(searchControl.val() == "" ? "" : '^' + searchControl.val() + '$', true, false).draw();
            });
        }
        if (index == 2) {
            $(this).replaceWith('<th class="w-auto mx-auto"><input class="w-100 mx-auto" type="text" placeholder="Search"></input ></th >');
            var searchControl = $("#DT_load thead tr:eq(1) th:eq(" + index + ") input");
            searchControl.on('keyup', function () {
                dataTable.column(index).search(searchControl.val()).draw();
            });
        }
        if (index == 3) {                                                                        // Replace all searchable headers with search bars (skip image and button columns)
            $(this).replaceWith('<th class="w-auto mx-auto"><input class="w-100 mx-auto" type="text" placeholder="Search"></input ></th >');
            var searchControl = $("#DT_load thead tr:eq(1) th:eq(" + index + ") input");
            searchControl.on('keyup', function () {
                dataTable.column(index).search(searchControl.val()).draw();
            });
        }
        if (index == 4) {
            var statusDropDown = $('<select class="w-auto mx-auto"/>');
            statusDropDown.append($('<option class="w-auto mx-auto"/>').attr('value', '').text('Select'));
            statusDropDown.append($('<option/>').attr('value', 'true').text('true'));
            statusDropDown.append($('<option/>').attr('value', 'false').text('false'));
            $(this).replaceWith('<th class="w-auto mx-auto">' + $(statusDropDown).prop('outerHTML') + '</th>');
            var searchControl = $("#DT_load thead tr:eq(1) th:eq(" + index + ") select");
            searchControl.on('change', function () {
                dataTable.column(index).search(searchControl.val() == "" ? "" : '^' + searchControl.val() + '$', true, false).draw();
            });
        }
        if (index == 5) { $(this).replaceWith('<th class="w-100 mx-auto"></th >'); }

    });
}
// Date Formatting
function formatDate(myDate) {
    var date = new Date(myDate);
    return '' + (date.getMonth()+1) + '/' + date.getDate() + '/' + date.getFullYear() + '';

}
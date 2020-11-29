var dataTable;

$(document).ready(function () {
    loadList();
});
// LOSTITEM TABLE
function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": "/api/homelostitem",
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
                    data: "id",
                    "render": function (data) {
                        return `
                        <div class="text-center w-100 mx-auto">
                            <a href="/Home/lostandfound/Details?id=${data}"
                               class="btn btn-info text-white w-100 mx-auto" style="cursor: pointer;">
                                <i class="fas fa-book-open w-100 mx-auto"></i>
                                Read
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
// FILTERING GENERAL LOST AND FOUND FUNCTION
function addSearchControl(json) {
    $("#DT_load thead").append($("#DT_load thead tr:first").clone());                           // Clone the First row
    $("#DT_load thead tr:eq(1) th").each(function (index) {                                     // Look at the header of each column for the cloned row
        if (index == 0) { $(this).replaceWith('<th class="w-100 mx-auto"></th >'); }                                              // Replace button column header with an empty header (no search bar)
        if (index == 1) {
            var statusDropDown = $('<select class="w-auto mx-auto"/>');
            statusDropDown.append($('<option class="w-auto mx-auto"/>').attr('value', '').text('Select'));
            var statusarr = [];
            $(json.data).each(function (index, element) {
                if ($.inArray(element.status, statusarr) == -1) {
                    statusarr.push(element.status);
                    statusDropDown.append($('<option/>').attr('value', element.status).text(element.status));
                }
            });
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
        if (index == 4) { $(this).replaceWith('<th class="w-100 mx-auto"></th >'); }

    });
}
// Date Formatting
function formatDate(myDate) {
    var date = new Date(myDate);
    return '' + (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() + '';
}
var dataTable;
var lotid = getUrlParameter('lotid');
var downloadName;
$(document).ready(function () {

    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax":
        {
            "url": (lotid == null) ? "/api/lotfile" : "/api/lotfile?id=" + lotid,
            "type": "GET",
            "datatype": "json"
        },
        "columns":
            [
                {
                    data: "title", width: "20%"
                },
                {
                    data: "description", width: "40%"
                },
                {
                    data: "date", "aTargets": [0], "render": function (data) { return formatDate(data); }
                    , width: "20%"
                },
                {
                    data: "file", width: "5%",
                    "render": function (data) {
                        downloadName = data;
                        return `
                        <div class="text-center">
                            <a href="/files/lot/${data}" download
                                class="btn btn-primary text-white"
                                style="cursor:pointer; width=100px;">
                                <i class="fas fa-file-download"></i>
                                Download
                            </a>
                        </div>
                    `
                    }
                },
                {
                    data: "id",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="/Admin/Lot/Files/Upsert?lotid=`+ lotid + `&id=${data}"
                               class="btn btn-warning text-white" style="cursor: pointer; width: 100px;">
                                <i class="far fa-edit"></i>
                                Edit 
                            </a>
                            <a onClick=Delete('/api/lotfile/'+${data})
                               class="btn btn-danger text-white" style="cursor: pointer"; width: 100px;">
                                <i class="far fa-trash-alt"></i>
                                Delete
                            </a>
                        </div>
`}, width: "15%"

                }
            ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}
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
// GET PARAMETER
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;
    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};
// Date Formatting
function formatDate(myDate) {
    var date = new Date(myDate);
    return '' + (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() + '';

}
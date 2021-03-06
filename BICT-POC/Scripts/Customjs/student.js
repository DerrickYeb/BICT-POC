﻿
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

const loadDataTable = () => {
    dataTable = $('#tbData').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Student/GetAll",
            "datatype":"json"
        },
        "columns": [
            { "data": "firstname", "width": "15%" },
            { "data": "lastname", "width": "15%" },
            { "data": "gender", "width": "15%" },
            { "data": "class", "width": "15%" },
            { "data": "course", "width": "15%" },
            { "data": "academicyear", "width": "15%" },
            { "data": "guidian", "width": "15%" },
            { "data": "guideancontact", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                <div class="text-center">
                 <a href="/Student/Assign/${data}" class="btn btn-success text-white" style="cursor:pointer">
                <i class="fas fa-edit"></i>
                  </a>
            </div>
                    `;
                }, "width": "40%"
            }
        ]
    })
}



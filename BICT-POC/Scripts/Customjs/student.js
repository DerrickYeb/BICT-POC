var dataTable;

$(document).ready(function () {
    loadDataTable();
});

const loadDataTable = () => {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "type":"GET",
            "url": "/Student/GetStudents",
            "datatype":"json"
        },
        "columns": [
            { "data": "firstname", "width": "15%" },
            { "data": "lastname", "width": "15%" },
            { "data": "class", "width": "15%" },
            { "data": "guidian", "width": "15%" },
            { "data": "guideancontact", "width": "15%" },
            { "data": "academicyear", "width": "15%" },
            { "data": "course.title", "width": "15%" },
            
        ]
    })
}

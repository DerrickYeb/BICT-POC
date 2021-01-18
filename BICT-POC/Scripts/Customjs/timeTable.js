$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/TimeTable/GetCourses",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Please select a course </option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Title + '</option';

            }
            $("#courseDropdown").html(s);
        }
    })
})

$('#idButton').on('click', function () {
    var print = $(this).closest('tr').html();
    newWin = window.open("");
    newWin.document.write(print);
    newWin.print();
    newWin.close();
})

$(function () {
    $('#searchForm').submit(function (e) {
        e.preventDefault();
        var searchTerm = $('#searchInput').val();
        $.ajax({
            url: searchUrl,
            type: 'GET',
            data: { searchTerm: searchTerm },
            success: function (result) {
                console.log(result);
                $('#companiList').html(result);
            }
        });
    });
});


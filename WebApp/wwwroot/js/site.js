$(document).ready(function () {
    $('#navbar-reddit').click(function () {
        document.getElementById('navbar-reddit').disabled = true;
        document.getElementById('navbar-twitter').disabled = true;
        document.getElementById('detailsButton').disabled = true;
    });

    $('#navbar-twitter').click(function () {
        document.getElementById('navbar-reddit').disabled = true;
        document.getElementById('navbar-twitter').disabled = true;
        document.getElementById('detailsButton').disabled = true;
    });

    $('#detailsButton').click(function () {
        document.getElementById('navbar-reddit').disabled = true;
        document.getElementById('navbar-twitter').disabled = true;
        document.getElementById('detailsButton').disabled = true;
    });

    $('#myTable').DataTable({
        searching: true,
        ordering: false,
        info: true,
        lengthChange: false,
        pageLength: 10,
        stripeClasses: [],
        dom: '<"mySearchForm"f>rt<"bottom"ip>',
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Search..."
        }
    });

    $('#twitterTable').DataTable({
        searching: true,
        ordering: false,
        info: true,
        lengthChange: false,
        pageLength: 10,
        stripeClasses: [],
        dom: '<"mySearchForm"f>rt<"bottom"ip>',
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Search..."
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

});
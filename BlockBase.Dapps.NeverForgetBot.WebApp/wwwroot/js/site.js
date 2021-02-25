$(document).ready(function () {
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
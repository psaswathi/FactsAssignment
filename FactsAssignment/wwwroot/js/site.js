// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
   var t= $('#productsTable').DataTable({
        processing: true,
       ordering: true,
       info: false,
       paging: true,
       searching: true,
       "pagingType": "simple_numbers",
       "language": {
           "paginate": {
               "next": "<button type='button'>Next</button>",
               "previous": "<button type='button'>Previous</button>"
           }
       },
        initComplete: function () {
            this.api().columns([0]).every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value="">-- Select --</option></select>')
                    .appendTo($('#searchName').empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
            this.api().columns([5]).every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value="">-- Select --</option></select>')
                    .appendTo($('#searchColor').empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
   });

    //t.on('order.dt search.dt', function () {
    //    t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();
    t.on('draw', function () {
        var select = $("#searchName select");
        if (select.val() === '') {
            select.empty().append('<option value="">-- Select --</option>');

            t.column(0, { search: 'applied' }).data().unique().sort().each(function (d, j) {
                select.append('<option value="' + d + '">' + d + '</option>');
            });
        }
        var select = $("#searchColor select");
        if (select.val() === '') {
            select.empty().append('<option value="">-- Select --</option>');

            t.column(5, { search: 'applied' }).data().unique().sort().each(function (d, j) {
                select.append('<option value="' + d + '">' + d + '</option>');
            });
        }        
    });
});

$('#searchReset').click(function () {
    $('#addSearch select').val('').trigger("change");
});
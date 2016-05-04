﻿define(['viewModels/title', 'moment', 'daterange'], function (title, moment) {

    function Controller() {

        var $containerElement = $('#payrollSection');

        function cb(start, end) {
            $('#reportrange span').html(start.format('MMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }

        var options = {
            "showDropdowns": true,
            "ranges": {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            "alwaysShowCalendars": true,
            "startDate": "04/27/2016",
            "endDate": "05/03/2016"
        };

        this.init = function () {

            $('#reportrange').daterangepicker(options, cb);

            $containerElement.parent().fadeIn();
            title.partialViewTitle('Planillas');
        }
    }

    return new Controller();
});

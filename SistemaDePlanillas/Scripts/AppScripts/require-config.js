var require = {
    baseUrl: '/Scripts/lib',
    paths: {
        'app': '/Scripts/AppScripts',
        'viewModels': '/Scripts/AppScripts/ViewModels',
        'controllers': '/Scripts/AppScripts/Controllers',
        'jquery': 'jquery-2.2.1',
        'knockout': 'knockout-3.4.0.debug',
        'sammy': 'sammy-0.7.5',
        'bootstrap': 'bootstrap',
        'respond': 'respond.min',
        'jasny': 'jasny-bootstrap',
        'switch': 'bootstrap-switch',
        'toggle': 'bootstrap-toggle',
        'dataGrid': 'knockout-DataGrid',
        'simpleGrid': 'knockout.simpleGrid.3.0'
    },
    shim: {
        'bootstrap': ['jquery'],
        'jasny': ['bootstrap'],
        'switch': ['jquery', 'bootstrap'],
        'toggle': ['jquery', 'bootstrap'],
        'daterangepicker': ['moment', 'jquery', 'bootstrap'],
        'bootsrap-select': ['jquery', 'bootstrap'],
        'jquery.validate': ['jquery'],
        'jquery.validate.unobtrusive': ['jquery.validate'],
        'jquery.unobtrusive-ajax': ['jquery'],
        'dataGrid': ['knockout'],
        'knockout': {
            deps: ['jquery'],
            exports: 'ko'
        },
        'sammy': {
            deps: ['jquery'],
            exports: 'Sammy'
        },
    }
};

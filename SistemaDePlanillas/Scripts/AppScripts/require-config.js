var require = {
    baseUrl: '/Scripts/lib',
    paths: {
        'app': '/Scripts/AppScripts',
        'viewModels': '/Scripts/AppScripts/ViewModels',
        'controllers': '/Scripts/AppScripts/Controllers',
        'jquery': 'jquery-2.2.1.min',
        'knockout': 'knockout-3.4.0',
        'sammy': 'sammy-0.7.5',
        'bootstrap': 'bootstrap.min',
        'respond': 'respond.min',
        'jasny': 'jasny-bootstrap.min',
    },
    shim: {
        'bootstrap': ['jquery'],
        'jasny': ['bootstrap'],
        'bootstrap-switch': ['bootstrap'],
        'jquery.validate': ['jquery'],
        'jquery.validate.unobtrusive': ['jquery.validate'],
        'jquery.unobtrusive-ajax': ['jquery'],
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

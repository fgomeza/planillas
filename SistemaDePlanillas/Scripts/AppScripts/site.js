// Write your Javascript code.

function action(group, operation, args, callback) {
    $.ajax({
        url: "/api/action/"+group+"/"+operation,
        type: "POST",
        data: JSON.stringify(args),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(data);},
        error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' });}
    });
}

function actionMethod(group, operation, method, args, callback) {
    $.ajax({
        url: "/api/action/" + group + "/" + operation+"/"+method,
        type: "POST",
        data: JSON.stringify(args),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(JSON.parse(data)); },
        error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }); }
    });
}

function actionString(group, operation, args, callback) {
    args = "[" + args + "]";
    var x = JSON.parse(args);
    $.ajax({
        url: "/api/action/" + group + "/" + operation,
        type: "POST",
        data: args,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(data); },
        error: function () { callback(JSON.stringify({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }, null, 2)); }
    });
}

var displayAlerts = function () {
    var alert = $('.field-validation-error').closest('.alert');
    alert.show();
}

var fadeOutAlerts = function () {
    $(".alert").addClass("in").fadeOut(4500);
}

var swapOpenCloseSideMenuIcons = function () {
    // swap open/close side menu icons 
    $('[data-toggle=collapse]').click(function () {
        // toggle icon
        $(this).find("i").toggleClass("glyphicon-chevron-right glyphicon-chevron-down");
    });
}

/* Bootstrap feedback for client-side unobtrusive validation */
var setupUnobtrusiveValidationForBootstrap = function () {
    var defaultOptions = {
        validClass: 'has-success',
        errorClass: 'has-error',
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .removeClass(validClass)
                .addClass(errorClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .removeClass(errorClass);
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass,
    };
}

var addAnimationForDropdowns = function () {
    $('nav .dropdown').on('show.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
    })
    $('nav .dropdown').on('hide.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
    })
}

var highlightCurrentLink = function () {
    console.log("window.location=" + window.location);

    $('#navmenu-links a').filter(function () {
        //console.log("link=" + this.innerHTML + " | this.href=" + this.href);
        return this.href == window.location;
    }).closest('li').addClass('active').parent().closest('li').addClass('active');
}

var setupSPANavigation = function() {
    var contentShell = $('#page');
    var History = window.History, State = History.getState();       
    
    $(".ajax-navigation").on('click', function (e) {        
        e.preventDefault();  
        var url = $(this).attr('href');
        //var title = $(this).data('title');
        History.pushState(null, url);
    });

    History.Adapter.bind(window, 'statechange', function () {
        State = History.getState();
        if (State.url !== '') {
            navigateToURL(State.url);
        }
    });

    function navigateToURL(url) {        
        contentShell.showLoading();
        $.ajax({
            type: "GET",
            url: url,
            dataType: "html",
            success: function (data, status, xhr) {
                contentShell.hideLoading();
                contentShell.hide();
                contentShell.html(data);                
                contentShell.fadeIn(1000);                
            },
            error: function (xhr, status, error) {
                contentShell.hideLoading();
                alert("Error loading Page.");
            }
        });
    }
}

var ajaxNavigationBegin = function (aaa) {
    //$('#page').hide();
    $('#page').css('top', topOffset).css('opacity', '0');
}

var ajaxNavigationComplete = function (result) {
    console.log(result);
}

var ajaxNavigationSuccess = function (html, status, xhr) {
    updateUrl(html, this.href);
    animatePage();
}

var ajaxNavigationFailure = function (result) {
    if (result.status === 401) {
        window.location.assign(this.href);
    }
}

var allowBackNavigationSPA = function () {
    window.onpopstate = function(event) {
        event.preventDefault();
        window.location.assign(document.location.pathname);
    };
}

var updateUrl = function (html, requestedUrl) {
    if (window.location.href == requestedUrl)
    {
        history.replaceState({html:html}, document.title, requestedUrl);
    }
    else
    {
        history.pushState({html:html}, document.title, requestedUrl);
    }}

var animatePage = function () {
    $('#page').animate(
        { top: "-="+topOffset, opacity: "+=1" }, 500, function () { }
    );
}

var initPage = function () {
    //fadeOutAlerts();
    //swapOpenCloseSideMenuIcons();
    displayAlerts();
    addAnimationForDropdowns();
    //highlightCurrentLink(); // Not suitable for ajax navigation
    //setupSPANavigation();
    animatePage();
    allowBackNavigationSPA();
}

$(document).ready(initPage);
$(setupUnobtrusiveValidationForBootstrap(jQuery));

topOffset = 50;

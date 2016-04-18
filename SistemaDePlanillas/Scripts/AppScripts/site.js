define(['jquery', 'jquery.unobtrusive-ajax', 'jquery.validate.unobtrusive'], function ($) {
    var displayAlerts = function () {
        var alert = $('.field-validation-error').closest('.alert');
        alert.slideDown();
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

    var animatePage = function () {
        $('#page').animate(
                { top: "-=" + topOffset, opacity: "+=1" }, 500, function () { }
        );
    }

    var initPage = function () {
        //fadeOutAlerts();
        //swapOpenCloseSideMenuIcons();
        displayAlerts();
        addAnimationForDropdowns();
        //highlightCurrentLink(); // Not suitable for ajax navigation
        //animatePage();
    }

    $(document).ready(initPage);
    $(setupUnobtrusiveValidationForBootstrap());

    var topOffset = 50;
});

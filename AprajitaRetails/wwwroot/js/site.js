// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $.ajaxSetup({ cache: false });
    $(document).on('click', 'a[data_modal]',/* $("a[data_modal]").on("click",*/ function (e) {
        if (this.id == "edit") {
            $("#myModalLabel").html("Edit Items");
            $("#modalHead").addClass("modal-warning");
        }
        else if (this.id == "detail") {
            $("#myModalLabel").html("Detail Items");
            $("#modalHead").addClass("modal-success");
        }
        else if (this.id == "create") {
            $("#myModalLabel").html("Create Items");
            $("#modalHead").addClass("modal-primary");
        }
        else if (this.id == "delete") {
            $("#myModalLabel").html("Delete Items");
            $("#modalHead").addClass("modal-info");
        }
        $('#myModalContent').load(this.href, function () {


            $('#myModal').modal({
                keyboard: true
            }, 'show');
        });
        return false;
    });
});


// TODO Function
$(document).ready(function () {

    $('.done-checkbox').on('click', function (e) {
        markCompleted(e.target);
    });
   // LoadThoughts();
});

function markCompleted(checkbox) {
    checkbox.disabled = true;

    var row = checkbox.closest('tr');
    $(row).addClass('done');

    var form = checkbox.closest('form');
    form.submit();
}

(function () {
    var reformatTimeStamps = function () {
        var timeStamps = document.getElementsByClassName("timeStampValue");
        for (var ts of timeStamps) {
            var thisTimeStamp = ts.getAttribute("data-value");
            var date = new Date(thisTimeStamp);
            ts.textContent = moment(date).format('LLL');
        }
    }
    reformatTimeStamps();
})();

(function () {
    var modernizeTimeStamps = function () {
        var timeStamps = document.getElementsByClassName("timeStampValueModernized");
        for (var ts of timeStamps) {
            var thisTimeStamp = ts.getAttribute("data-value");
            var date = new Date(thisTimeStamp);
            ts.textContent = moment(date).fromNow();
        }
    }
    modernizeTimeStamps();
})();

var settings = {
    "async": true,
    "crossDomain": true,
    "url": "https://andruxnet-random-famous-quotes.p.rapidapi.com/?count=10&cat=famous",
    "method": "POST",
    "headers": {
        "x-rapidapi-host": "andruxnet-random-famous-quotes.p.rapidapi.com",
        "x-rapidapi-key": "2794ef64d6msha40bb0fc5b20442p13b8adjsndc64838e1f99",
        "content-type": "application/x-www-form-urlencoded"
    },
    "data": {}
}

function LoadThoughts() {
    $.ajax(settings).done(function (response) {
        console.log(response);
        RenderThoughs(response);
    });
}


function RenderThoughs(data) {
    //TODO:
}

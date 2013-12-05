$(document).ready(
    bookChange()
);

function bookChange() {
    $("#BookId").change(function () {
        var selectedOption = $("#BookId option:selected").text();
        var chapters = $("select#ChapterId");
        chapters[0].options.length = 0;
        var httpRequest = $.ajax({
            type: "POST",
            url: "/Bible/Chapters?book=" + selectedOption,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (json) {
                json.forEach(function (object) {
                    chapters.append("<option value='" + object.Value + "'>" + object.Text + "</option>");
                });
                chapters[0].selected = true;
            }
        });
        httpRequest.fail(function (jqXhr, textStatus) {
            alert("Request failed: " + textStatus);
        });
    });
};

function selectChapter() {
    var t = $("#TranslationId option:selected").text();
    var b = $("#BookId option:selected").text();
    var c = $("#ChapterId option:selected").text();
    var querystring = "translation=" + t + "&book=" + b + "&chapter=" + c;
    var content = document.getElementById('Content');
    content.innerHTML = "";
    
    $.ajax({
        type: 'GET',
        url: '/Bible/_Text?' + querystring,
        dataType: 'html', 
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#Content').html(data);
        }
    });
};

function submitChapter() {
    var t = $("#TranslationId option:selected").text();
    var b = $("#BookId option:selected").text();
    var c = $("#ChapterId option:selected").text();
    var url = "/Bible/_Text?translation=" + t + "&book=" + b + "&chapter=" + c;

    window.location.href = url;
};

function insertChapterScript() {
    var t = $("#TranslationId option:selected").text();
    var b = $("#BookId option:selected").text();
    var c = $("#ChapterId option:selected").text();
    var url = "/Bible/_TextGen?translation=" + t + "&book=" + b + "&chapter=" + c;

    window.location.href = url;
};
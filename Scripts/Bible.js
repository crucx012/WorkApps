$(document).ready(translationChange());
$(document).ready(bookChange());
$(document).ready(chapterChange());

function translationChange() {
    $("#TranslationId").change(function () {
        if ($('input#Search')[0].value == "") {
            selectChapter();
        } else {
            setPage(1);
        };
    });
};

function bookChange() {
    $("#BookId").change(function () {
        var chapters = $("select#ChapterId");
        chapters[0].options.length = 0;
        $.ajax({
            type: "POST",
            url: "/Bible/Chapters?book=" + $("#BookId option:selected").text(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (json) {
                json.forEach(function (object) {
                    chapters.append("<option value='" + object.Value + "'>" + object.Text + "</option>");
                });
                chapters[0].selected = true;
                selectChapter();
            }
        });
    });
};

function chapterChange() {
    $("#ChapterId").change(function () {
        selectChapter();
    });
};

function selectChapter() {
    $('#Content')[0].innerHTML = "";
    clearSearching();

    $.ajax({
        type: 'GET',
        url: "/Bible/_Text?translationID=" + $("#TranslationId option:selected")[0].value
            + "&book=" + $("#BookId option:selected").text()
            + "&chapter=" + $("#ChapterId option:selected").text(),
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#Content').html(data);
        }
    });
};

function searchBible() {
    if ($('input#Search')[0].value != "") {
        $.ajax({
            type: 'GET',
            url: "/Bible/_Search?searchtext=" + $('input#Search')[0].value
                + "&translationID=" + $("#TranslationId option:selected")[0].value
                + "&page=" + $('input#hiddenPage')[0].value
                + "&perPage=" + $('input#nudPerPage')[0].value,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#Content').html(data);
                wrapText();
                translations();
                paging();
            }
        });
    } else {
        $('#Content').html("");
        clearSearching();
    };
}

function wrapText() {
    var matches = $('#Content')[0].children;
    for (var i = 1; i < matches.length; i++) {
        var ttlLength = 0;
        var list = matches[i].innerHTML.split(getRegEx());
        for (var j = 0; j < list.length; j++) {
            var len = matches[i].innerHTML.length;
            var start = list[j].length + ttlLength;
            var end = start + $('input#Search')[0].value.length;
            var selectedText = matches[i].innerHTML.substring(start, end);
            var replacement = '<b>' + selectedText + '</b>';
            matches[i].innerHTML = matches[i].innerHTML.substring(0, start) + replacement + matches[i].innerHTML.substring(end, len);
            ttlLength += list[j].length + replacement.length;
        };
    };
};

function translations() {
    $.ajax({
        type: 'GET',
        url: "/Bible/_Translations?searchtext=" + $('input#Search')[0].value,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#Translations').html(data);
        }
    });
};

function getRegEx() {
    var preString = "";
    var postString = "";
    var flags = "";
    if ($('input#cbBegining')[0].checked == true)
        preString += '\\b';
    if ($('input#cbEnding')[0].checked == true)
        postString += '\\b';
    if ($('input#cbCaseSensitive')[0].checked == false)
        flags += 'i';
    return new RegExp(preString + $('input#Search')[0].value + postString, flags);
};

function paging() {
    $.ajax({
        type: 'GET',
        url: "/Bible/_Paging?searchtext=" + $('input#Search')[0].value
            + "&translationID=" + $("#TranslationId option:selected")[0].value
            + "&page=" + $('input#hiddenPage')[0].value
            + "&perPage=" + $('input#nudPerPage')[0].value,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#TopPaging').html(data);
            $('#BottomPaging').html(data);
        }
    });
};

function clearSearching() {
    $('input#Search')[0].value = "";
    $('#TopPaging').html("");
    $('#BottomPaging').html("");
    $('#Translations').html("");
}

function setTranslation(translationIndex) {
    $('select#TranslationId')[0].selectedIndex = translationIndex;
    setPage(1);
}

function setPage(page) {
    $('input#hiddenPage')[0].value = page;
    searchBible();
}
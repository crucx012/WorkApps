$(document).ready(translationChange());
$(document).ready(bookChange());
$(document).ready(chapterChange());

function translationChange() {
    $("#TranslationId").change(function () {
        if (getText() == "") {
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
            url: "/Bible/Chapters?book=" + getBook(),
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
    getContent().innerHTML = "";
    $('input#Search')[0].value = "";

    $.ajax({
        type: 'GET',
        url: "/Bible/_Text?translationID=" + getTranslation() + "&book=" + getBook() + "&chapter=" + getChapter(),
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            getContent().html(data);
            clearPaging();
        }
    });
};

function searchBible() {
    if (getText() != "") {
        $.ajax({
            type: 'GET',
            url: "/Bible/_Search?searchtext=" + getText() + "&translationID=" + getTranslation() + "&page=" + getPage() + "&perPage=" + getPerPage(),
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                getContent().html(data);
                wrapText();
                paging();
            }
        });
    } else {
        getContent().html("");
        clearPaging();
    };
}

function wrapText() {
    var matches = getMatches();
    for (var i = 1; i < matches.length; i++) {
        var ttlLength = 0;
        var list = matches[i].innerHTML.split(getRegEx());
        for (var j = 0; j < list.length; j++) {
            var len = matches[i].innerHTML.length;
            var start = list[j].length + ttlLength;
            var end = start + getText().length;
            var selectedText = matches[i].innerHTML.substring(start, end);
            var replacement = '<b>' + selectedText + '</b>';
            matches[i].innerHTML = matches[i].innerHTML.substring(0, start) + replacement + matches[i].innerHTML.substring(end, len);
            ttlLength += list[j].length + replacement.length;
        };
    };
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
    return new RegExp(preString + getText() + postString, flags);
};

function paging() {
    $.ajax({
        type: 'GET',
        url: "/Bible/_Paging?searchtext=" + getText() + "&translationID=" + getTranslation() + "&page=" + getPage() + "&perPage=" + getPerPage(),
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            getTopPaging().html(data);
            getBottomPaging().html(data);
        }
    });
};

function clearPaging() {
    getTopPaging().html("");
    getBottomPaging().html("");
}

function getTranslation() {
    return $("#TranslationId option:selected")[0].value;
}

function getBook() {
    return $("#BookId option:selected").text();
}

function getChapter() {
    return $("#ChapterId option:selected").text();
}

function getContent() {
    return $('#Content');
}

function getMatches() {
    return getContent()[0].children;
}

function getText() {
    return $('input#Search')[0].value;
}

function getPage() {
    return $('input#hiddenPage')[0].value;
}

function setPage(page) {
    $('input#hiddenPage')[0].value = page;
    searchBible();
}

function getPerPage() {
    return $('input#nudPerPage')[0].value;
}

function getTopPaging() {
    return $('#TopPaging');
}

function getBottomPaging() {
    return $('#BottomPaging');
}


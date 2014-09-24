$(document).ready(resetPage());

// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
// left: 37, up: 38, right: 39, down: 40
scrollEventKeys = [32, 33, 34, 35, 36, 37, 38, 39, 40];
$window = $(window);
$document = $(document);
var processing = false;

$(window).scroll(function () {
    if (!processing && nearBottomOfPage() && !isNotPosted()) {
        loadTransactions();
    }
});

function nearBottomOfPage() {
    return ($(document).height() - $(window).height()) - $(window).scrollTop() <= 100;
}

$("#nonPostedTable").on("click", "td", function () {
    disable_scrolling();
    displayPostTransForm(parseInt($(this).parents('tr').children('td')[0].textContent));
});

function displayPostTransForm(intID) {
    $.ajax({
        type: 'GET',
        url: "/Wblack/_PostTransaction",
        data: { transID: intID },
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != null) {
                $("article").append(data);
            }
        }
    });
}

function postTransaction() {
    var postData = getPostTransaction();
    $.ajax({
        type: 'GET',
        url: "/Wblack/PostTransaction",
        data: postData,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function () {
            removePostedData(parseInt($('#postTransID')[0].innerText));
        },
        error: function () {
            alert("Couldn't find a BANK STATEMENT for that ACCOUNT on that DATE.");
        },
        complete: function () {
            removePostForm();
            enable_scrolling();
        }
    });
}

function getPostTransaction() {
    return {
        TransactionID: parseInt($('#postTransID')[0].innerText),
        BankRecordedDate: $('#Date')[0].value,
        BankRecordedAmt: parseFloat($('#Amount')[0].value)
    };
}

function removePostForm() {
    $('div#postTrans').remove();
}

function removePostedData(transID) {
    $('notPostedTable,td').not(function() {
        return $(this).text() != transID.toString();
    }).parent().remove();
}

function displayNewTransForm() {
    disable_scrolling();
    $.ajax({
        type: 'GET',
        url: "/Wblack/_NewTransaction",
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != null) {
                $("article").append(data);
            }
        }
    });
}

function insertTransaction() {
    if (!processing) {
        processing = true;
        createTransaction();
    }
}

function createTransaction() {
    var postData = getNewTransaction();
    $.ajax({
        type: 'GET',
        url: "/Wblack/InsertTransaction",
        data: postData,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        complete: function () {
            var acc = $('#NewAccountId option:selected')[0].value;
            removeInsertForm();
            displayData(acc);
            enable_scrolling();
        }
    });
}

function getNewTransaction() {
    return {
        AccountID: $('#NewAccountId option:selected')[0].value,
        EntityID: $('#NewEntityId option:selected')[0].value,
        CategoryID: $('#NewCategoryId option:selected')[0].value,
        TenderID: $('#NewTenderId option:selected')[0].value,
        TransactionDate: $('#Date')[0].value,
        TransactionAmt: $('#Amount')[0].value,
        CashBack: $('#CashBack')[0].value,
        Description: $('#Desc')[0].value,
        TransferAccount: $('#NewTransAcc option:selected')[0].value
    };
}

function removeInsertForm() {
    $('div#newTrans').remove();
}

function displayData(acc) {
    if (getAccount() == acc && !isAscending() && isAll()) {
        loadNewTransaction();
    } else {
        setFilters(acc);
        resetPage();
    }
}

function loadNewTransaction() {
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions_Descending?accountID=" + getAccount()
        + "&skip=" + 0
        + "&take=" + 1,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayNewTransaction(data);
        },
        complete: function () {
            processing = false;
        }
    });
}

function displayNewTransaction(data) {
    $('#transTable tbody tr:first').after(data);
}

function setFilters(acc) {
    $("#AccountId").val(acc);
    $('input:radio[name=Transactions][value=2]').prop('checked', true);
    $('input:radio[name=Sort][value=1]').prop('checked', true);
}

function resetPage() {
    removeNonHeaderRows();
    showProperHeaders();
    loadTransactions();
}

function removeNonHeaderRows() {
    $('tr').not(function () {
        return $(this).has('th').length;
    }).remove();
}

function showProperHeaders() {
    if (isAll()) {
        showAllTableHeaders();
    } else if (isPosted()) {
        showPostedHeaders();
    } else if (isNotPosted()) {
        showNonPostedHeaders();
    }
}

function showAllTableHeaders() {
    $('#postedTable').hide();
    $('#nonPostedTable').hide();
    $('#allTransTable').show();
}

function showPostedHeaders() {
    $('#nonPostedTable').hide();
    $('#allTransTable').hide();
    $('#postedTable').show();
}

function showNonPostedHeaders() {
    $('#allTransTable').hide();
    $('#postedTable').hide();
    $('#nonPostedTable').show();
}

function loadTransactions() {
    if ($('input:radio[name=Sort]:checked')[0].value == 0) {
        loadAscending();
    } else {
        loadDescending();
    }
}

function loadAscending() {
    if (isPosted()) {
        getPostedTransactions(getSkip());
    } else if (isNotPosted()) {
        getNotPostedTransactions();
    } else if (isAll()) {
        getTransactions(getSkip());
    }
}

function getPostedTransactions() {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions_Posted?accountID=" + getAccount()
        + "&skip=" + getSkip()
        + "&take=" + 30,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayPosted(data);
        },
        complete: function () {
            processing = false;
        }
    });
};

function getNotPostedTransactions() {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions_NotPosted",
        data: { accountID: parseInt(getAccount()) },
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayNonPosted(data);
        },
        complete: function () {
            processing = false;
        }
    });
}

function getTransactions() {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions?accountID=" + getAccount()
        + "&skip=" + getSkip()
        + "&take=" + 30,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayAll(data);
        },
        complete: function () {
            processing = false;
        }
    });
};

function loadDescending() {
    if (isPosted()) {
        getPostedTransactionsDesc();
    } else if (isNotPosted()) {
        getNotPostedTransactionsDescending();
    } else if (isAll()) {
        getTransactionsDesc();
    }
}

function getPostedTransactionsDesc() {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions_PostedDescending?accountID=" + getAccount()
        + "&skip=" + getSkip()
        + "&take=" + 30,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayPosted(data);
        },
        complete: function () {
            processing = false;
        }
    });
};

function getNotPostedTransactionsDescending() {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions_NotPostedDescending",
        data: { accountID: parseInt(getAccount()) },
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayNonPosted(data);
        },
        complete: function () {
            processing = false;
        }
    });
}

function getTransactionsDesc() {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions_Descending?accountID=" + getAccount()
        + "&skip=" + getSkip()
        + "&take=" + 30,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayAll(data);
        },
        complete: function () {
            processing = false;
        }
    });
};

function getAccount() {
    return $("#AccountId option:selected")[0].value;
}

function getSkip() {
    if (isAll()) {
        return $('#allTransTable tbody')[0].children.length - 1;
    } else if (isPosted()) {
        return $('#postedTable tbody')[0].children.length - 1;
    } else if (isNotPosted()) {
        return $('#nonPostedTable tbody')[0].children.length - 1;
    }
}

function isAscending() {
    return $('input:radio[name=Sort]:checked')[0].value != 0;
}

function isPosted() {
    return $('input:radio[name=Transactions]:checked')[0].value == 0;
}

function isNotPosted() {
    return $('input:radio[name=Transactions]:checked')[0].value == 1;
}

function isAll() {
    return $('input:radio[name=Transactions]:checked')[0].value == 2;
}

function displayAll(data) {
    if (data != null && data != "") {
        $('#allTransTable tbody').append(data);
    }
}

function displayPosted(data) {
    if (data != null && data != "") {
        $('#postedTable tbody').append(data);
    }
}

function displayNonPosted(data) {
    if (data != null && data != "") {
        $('#nonPostedTable tbody').append(data);
    }
}

function disable_scrolling() {
    var t = this;
    t.$window.on("mousewheel.UserScrollDisabler DOMMouseScroll.UserScrollDisabler", this._handleWheel);
    t.$document.on("mousewheel.UserScrollDisabler touchmove.UserScrollDisabler", this._handleWheel);
    t.$document.on("keydown.UserScrollDisabler", function (event) {
        t._handleKeydown.call(t, event);
    });
}

function enable_scrolling() {
    var t = this;
    t.$window.off(".UserScrollDisabler");
    t.$document.off(".UserScrollDisabler");
}

function _handleKeydown(event) {
    for (var i = 0; i < this.scrollEventKeys.length; i++) {
        if (event.keyCode === this.scrollEventKeys[i]) {
            event.preventDefault();
            return;
        }
    }
}

function _handleWheel(event) {
    event.preventDefault();
}
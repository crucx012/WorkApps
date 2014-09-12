$(document).ready(resetPage());

var finished = 0;
var processing = false;

function resetPage() {
    $('tr').not(function () {
        if ($(this).has('th').length) {
            return true;
        }
    }).remove();
    finished = 0;
    loadTransactions();
}

$(window).scroll(function () {
    if (!processing && loadingComplete() && nearBottomOfPage()) {
        loadTransactions();
    }
});

function loadTransactions() {
    if ($("input[type='radio']:checked").val() == "0") {
        getTransactions($('tbody')[0].children.length - 1);
    } else {
        getTransactionsDesc($('tbody')[0].children.length - 1);
    }
}

function loadingComplete() {
    return finished === $('tbody')[0].children.length - 1;
}

function nearBottomOfPage() {
    return ($(document).height() - $(window).height()) - $(window).scrollTop() <= 100;
}

function getTransactions(skip) {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_Transactions?accountID=" + $("#AccountId option:selected")[0].value
        + "&skip=" + skip
        + "&take=" + 30,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function(data) {
            append(data);
        },
        complete: function () {
            processing = false;
        }
    });
};

function getTransactionsDesc(skip) {
    processing = true;
    $.ajax({
        type: 'GET',
        url: "/Wblack/_TransactionsDescending?accountID=" + $("#AccountId option:selected")[0].value
        + "&skip=" + skip
        + "&take=" + 30,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function(data) {
            append(data);
        },
        complete: function () {
            processing = false;
        }
    });
};

function append(data) {
    if (data != null && data != "") {
        $('tbody').append(data);
        finished += data.match(/<tr>/g).length;
    }
}

function displayNewTransForm() {
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
            loadNewTransaction(acc);
        }
    });
}

function loadNewTransaction(acc) {
    $.ajax({
        type: 'GET',
        url: "/Wblack/_TransactionsDescending?accountID=" + $("#AccountId option:selected")[0].value
        + "&skip=" + 0
        + "&take=" + 1,
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            displayData(data, acc);
        },
        complete: function() {
            processing = false;
        }
    });
}

function displayData(data, acc) {
    if ($("#AccountId option:selected")[0].value == acc && $("input[type='radio']:checked").val() != "0") {
        displayNewTransaction(data);
    } else {
        $("#AccountId").val(acc);
        resetPage();
    }
}

function displayNewTransaction(data) {
    $('tbody tr:first').after(data);
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
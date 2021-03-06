﻿var busy = false;

function startGame() {
    var playerMarker = getPlayerMarker();
    setMarkers(playerMarker);
    $.ajax({
        type: "POST",
        url: "/Games/ChooseStartingPlayer",
        data: JSON.stringify({playerPiece:playerMarker}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (json) {
            begin(json);
        }
    });
};

function begin(json) {
    $("span#players")[0].hidden = true;
    $("label#secondPlayerStart")[0].innerText = json;
    if (json) {
        busy = true;
        $("div#Message")[0].innerText = "Second players turn";
        AI();
    } else {
        $("div#Message")[0].innerText = "First players turn";
    }
}

function takeTurn(cell) {
    if (isReady(cell)) {
        busy = true;
        populateCell(cell, getPlayerMarker());
        populateBoard(cell);
        AI();
    }
};

function isReady(cell) {
    return (playerPieceChosen() && !busy && cellIsUnclaimed(cell));
}

function playerPieceChosen() {
    return $('span#players')[0].hidden == true;
}

function cellIsUnclaimed(cell) {
    return $('img#square_' + cell)[0].src.indexOf('E.jpg') > -1;
}

function AI() {
    loadingBar();
    $.ajax({
        type: "POST",
        url: "/Games/ClaimCell",
        data: JSON.stringify({
            playerPiece: getPlayerMarker(),
            isSecondPlayerFirst: secondGoesFirst(),
            board: getBoard(),
            claimedSquare: 1
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (json) {
            var cell = json.pop();
            populateCell(cell, getPCMarker());
            populateBoard(cell);
        },
        complete: function() {
            closeLoadingBar();
            busy = false;
        }
    });
}

function secondGoesFirst() {
    return $("#secondPlayerStart")[0].innerText;
}

function getPlayerMarker() {
    if ($('input#Marker')[0].checked == true) {
        return 2;
    } else {
        return 1;
    };
}

function getPCMarker() {
    if (getPlayerMarker() == 1) {
        return 2;
    } else {
        return 1;
    };
}

function populateBoard(cell) {
    for (var i = 1; i < 10; i++) {
        if ($("span#data")[0].getElementsByTagName("Label")[i].innerText == "") {
            $("span#data")[0].getElementsByTagName("Label")[i].innerText = cell;
            break;
        }
    };
}

function getBoard() {
    var board = [];
    for (var i = 1; i < 10; i++) {
        if ($("span#data")[0].getElementsByTagName("Label")[i].innerText != "") {
            board.push(parseInt($("span#data")[0].getElementsByTagName("Label")[i].innerText));
        } 
    };
    return board;
}

function setMarkers(playerMarker) {
    if (playerMarker == 2) {
        $('img#player1')[0].src = "/Images/TicTacToe/X.jpg";
        $('img#player2')[0].src = "/Images/TicTacToe/O.jpg";
    } else if (playerMarker == 1) {
        $('img#player1')[0].src = "/Images/TicTacToe/O.jpg";
        $('img#player2')[0].src = "/Images/TicTacToe/X.jpg";
    };
};

function populateCell(index, marker) {
    if (marker == 2) {
        $('img#square_' + index)[0].src = "/Images/TicTacToe/X.jpg";
    } else if (marker == 1) {
        $('img#square_' + index)[0].src = "/Images/TicTacToe/O.jpg";
    } else {
        $('img#square_' + index)[0].src = "/Images/TicTacToe/E.jpg";
    };
}

function loadingBar() {
    $("div#loading").dialog({ modal: true, id: 'loading_dialog' });
};

function closeLoadingBar() {
    $('[aria-describedby=loading]').remove();
}

function reset() {
    for (var i = 1; i < 10; i++) {
        if ($("span#data")[0].getElementsByTagName("Label")[i].innerText != "") {
            $("span#data")[0].getElementsByTagName("Label")[i].innerText = "";
        }
        populateCell(i, 0);
    };
    startGame();
}
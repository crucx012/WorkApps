var previous = $("div.rolls").children();
var scores = $("div.scores").children();
var pins = $("input#nudPins")[0];
var rolls = [];

function hitenter(e) {
    if (e.keyCode == 13) {
        getFrame();
    }
}

function getFrame() {
    if (!arePinsValid()) {
        return;
    }

    storedata();
    displayrolls();
    getscores(previous, scores);
};

function arePinsValid() {
    if (pins.valueAsNumber > 10 || pins.valueAsNumber < 0) {
        $('label#lblMessage')[0].innerHTML = "Number of pins knocked down is not valid.";
        return false;
    } else {
        $('label#lblMessage')[0].innerHTML = "";
        return true;
    }
}

function storedata() {
    if (previous.length > rolls.length) {
        if (previous[19].textContent == ""
            || previous[18].textContent == "X"
            || previous[19].textContent == "/") {
            rolls.push(parseInt(pins.value));
        };
    };
};

function displayrolls() {
    var s = 0;
    for (var i = 0; i < Object.keys(rolls).length; i++) {
        if (previous[i] != null) {
            if (rolls[i] == 10) {
                previous[i + s].textContent = "X";
                setmax(10);
                if ((i + s) < 18) {
                    previous[i + s + 1].textContent = "-";
                    s++;
                }
            } else if ((parseInt(rolls[i]) + parseInt(rolls[i + 1])) == 10) {
                previous[i + s].textContent = rolls[i];
                previous[i + s + 1].textContent = "/";
                setmax(10);
                i++;
            } else {
                previous[i + s].textContent = rolls[i];
                setmax(10 - parseInt(rolls[i]));
                if (rolls[i + 1] != null) {
                    previous[i + s + 1].textContent = rolls[i + 1];
                    setmax(10);
                    i++;
                };
            };
        };
    };
};

function setmax(val) {
    pins.max = val;
    pins.value = pins.max;
};

function getscores() {
    $.ajax({
        url: "/Games/Roll",
        data: JSON.stringify({ rolls: rolls }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (json) {
            displayScores(json);
        }
    });
}

function displayScores(json) {
    for (var i = 0; i < Object.keys(json).length; i++) {
        if (scores[i] != null) {
            if (json[i] == 0) {
                if (previous[i * 2].textContent == "0"
                    && previous[i * 2 + 1].textContent == ""
                    || previous[i * 2 + 1].textContent == "0") {
                    scores[i].textContent = json[i];
                } else {
                    scores[i].textContent = "";
                }
            } else {
                scores[i].textContent = json[i];
            };
        }
    };
}
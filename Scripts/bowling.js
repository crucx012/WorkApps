function hitenter(e) {
    if (e.keyCode == 13) {
        getFrame();
    }
}

function getFrame() {
    var previous = document.getElementById("rolls").children;
    var scores = document.getElementById("score").children;
    var pins = document.getElementById("nudPins");
    var rolls = [];

    rolls = storedata(previous, pins, rolls);
    displayrolls(rolls, previous, pins);
    getscores(rolls, previous, scores);
};

function storedata(previous, current, array) {
    if (previous.length > array.length) {
        array = buildarray(previous, array);
        if (previous[19].innerHTML == ""
            || previous[18].innerHTML == "X"
            || previous[19].innerHTML == "/") {
            array.push(current.value);
        };
    };
    return array;
};

function buildarray(previous, array) {
    for (var i = 0; previous[i].innerHTML != ""; i++) {
        if (previous[i].innerHTML == "X") {
            array.push("10");
        } else if (previous[i].innerHTML == "/") {
            array.push((10 - parseInt(previous[i - 1].innerHTML)) + "");
        } else if (previous[i].innerHTML != "-") {
            array.push(previous[i].innerHTML);
        };
    };
    return array;
};

function displayrolls(array, previous, current) {
    var s = 0;
    for (var i = 0; i < Object.keys(array).length; i++) {
        if (previous[i] != null) {
            if (array[i] == 10) {
                previous[i + s].innerHTML = "X";
                setmax(current, 10);
                if ((i + s) < 18) {
                    previous[i + s + 1].innerHTML = "-";
                    s++;
                }
            } else if ((parseInt(array[i]) + parseInt(array[i + 1])) == 10) {
                previous[i + s].innerHTML = array[i];
                previous[i + s + 1].innerHTML = "/";
                setmax(current, 10);
                i++;
            } else {
                previous[i + s].innerHTML = array[i];
                setmax(current, 10 - parseInt(array[i]));
                if (array[i + 1] != null) {
                    previous[i + s + 1].innerHTML = array[i + 1];
                    setmax(current, 10);
                    i++;
                };
            };
        };
    };
};

function setmax(current, max) {
    current.max = max;
    current.value = current.max;
};

function getscores(array, previous, scores) {
    var s = 0;
    var httpRequest = $.ajax({
        url: "/Games/Roll?rolls=" + array,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (json) {
            for (var i = 0; i < Object.keys(json).length; i++) {
                if (scores[i] != null) {
                    if (json[i] == 0) {
                        if (previous[i * 2].innerHTML == "0"
                            && previous[i * 2 + 1].innerHTML == ""
                            || previous[i * 2 + 1].innerHTML == "0") {
                            scores[i].innerHTML = json[i];
                        } else {
                            scores[i].innerHTML = "";
                        }
                    } else {
                        scores[i].innerHTML = json[i];
                    };
                }
            };
            httpRequest.fail(function (jqXhr, textStatus) {
                document.getElementById("lblMessage").innerHTML = "Request failed: " + textStatus;
            });
        }
    });
}
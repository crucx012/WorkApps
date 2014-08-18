function hitenter(e) {
    if (e.keyCode == 13) {
        getFrame();
    }
}

function getFrame() {
    var previous = $("div.rolls").children();
    var scores = $("div.scores").children();
    var pins = $("input#nudPins")[0];
    var rolls = [];

    rolls = storedata(previous, pins, rolls);
    displayrolls(rolls, previous, pins);
    getscores(rolls, previous, scores);
};

function storedata(previous, current, array) {
    if (previous.length > array.length) {
        array = buildarray(previous, array);
        if (previous[19].textContent == ""
            || previous[18].textContent == "X"
            || previous[19].textContent == "/") {
            array.push(current.value);
        };
    };
    return array;
};

function buildarray(previous, array) {
    for (var i = 0; previous[i].textContent != ""; i++) {
        if (previous[i].textContent == "X") {
            array.push("10");
        } else if (previous[i].textContent == "/") {
            array.push((10 - parseInt(previous[i - 1].textContent)) + "");
        } else if (previous[i].textContent != "-") {
            array.push(previous[i].textContent);
        };
    };
    return array;
};

function displayrolls(array, previous, current) {
    var s = 0;
    for (var i = 0; i < Object.keys(array).length; i++) {
        if (previous[i] != null) {
            if (array[i] == 10) {
                previous[i + s].textContent = "X";
                setmax(current, 10);
                if ((i + s) < 18) {
                    previous[i + s + 1].textContent = "-";
                    s++;
                }
            } else if ((parseInt(array[i]) + parseInt(array[i + 1])) == 10) {
                previous[i + s].textContent = array[i];
                previous[i + s + 1].textContent = "/";
                setmax(current, 10);
                i++;
            } else {
                previous[i + s].textContent = array[i];
                setmax(current, 10 - parseInt(array[i]));
                if (array[i + 1] != null) {
                    previous[i + s + 1].textContent = array[i + 1];
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
    var httpRequest = $.ajax({
        url: "/Games/Roll?rolls=" + array,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (json) {
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
            httpRequest.fail(function (jqXhr, textStatus) {
                document.getElementById("lblMessage").textContent = "Request failed: " + textStatus;
            });
        }
    });
}
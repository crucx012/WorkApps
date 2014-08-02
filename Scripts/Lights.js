$(document).ready(function () {
    var mdflag;

    document.addEventListener("mousedown", mdown, false);
    document.addEventListener("mouseup", mup, false);
    $(document).disableSelection();

    for (var num = 0; num < 4; num++) {
        $("#Color_" + num)[0].value = getRandomColor();
    };

    function blinking() {
        var blinks = getBlinkCount();
        for (var i = 0; i < blinks; i++) {
            blink();
        };
    };

    function blink() {
        var colors = getColorList();
        var light = getRandomLight();
        blinkNow(light, colors);
    };

    function blinkNow(light, colors) {
        for (var i = 0; i < colors.length; i++) {
            if (light.hasClass(colors[i])) {
                light.stop().animate({ boxShadow: "0 0 0px 0px" }, 500).animate({ boxShadow: "0 0 10px 3px" }, 500);
            };
        };
    };

    setInterval(blinking, 250);

    $("#nudColors").change(function () {
        for (var i = 0; i < 4; i++) {
            setColorChoice(i);
        };
    });

    function setColorChoice(index) {
        if ((parseInt($("#nudColors")[0].value) - 1) > index) {
            $("#Color_" + index)[0].disabled = false;
        } else if ((parseInt($("#nudColors")[0].value) - 1) == index) {
            $("#Color_" + index)[0].disabled = false;
            $("#Color_" + index)[0].value = getRandomColor();
        } else {
            $("#Color_" + index)[0].setAttribute('disabled', 'disabled');
        };
    };

    $(".light").mousedown(function () {
        change($(this));
    });

    $(".light").mouseenter(function () {
        if (mdflag) {
            change($(this));
        };
    });

    function mdown() {
        mdflag = true;
    };

    function mup() {
        mdflag = false;
    };

    function change(light) {
        var lightChanged = turnOff(light);
        if (!lightChanged) {
            renderOn(light);
        };
    };

    function turnOff(light) {
        var lightChanged;
        getColorList().forEach(function(color) {
            if (light.hasClass(color)) {
                renderOff(light);
                lightChanged = true;
            }
        });
        return lightChanged;
    }

    function renderOff(light) {
        light.animate({ boxShadow: "0 0 0px 0px" }, 500);
        getColorList().forEach(function (color) {
            light.removeClass(color);
        });
    };

    function renderOn(light) {
        var color;
        if ((parseInt($("#nudColors")[0].value) - 1) > 0) {
            color = getRandomChosenColor();
        } else {
            color = getColor(0);
        };
        light.addClass(color).stop().animate({ boxShadow: "0 0 10px 3px" }, 500);
    };

    function getBlinkCount() {
        return $("#blink")[0].value;
    };

    function getColorList() {
        var colors = [];
        for (var i = 0; i < $("#Color_0")[0].children.length; i++) {
            colors.push($("#Color_0")[0].children[i].innerText);
        };
        return colors;
    };

    function getRandomLight() {
        var number = Math.floor(Math.random() * 100);
        if (number < 10) {
            number = "0" + number;
        };
        return $("#light_" + number);
    };

    function getRandomColor() {
        var colorList = getColorList();
        colorList = removeChosenColors(colorList);
        var index = Math.floor(Math.random() * colorList.length);
        return colorList[index];
    };

    function removeChosenColors(colorList) {
        for (var i = 0; i < 4; i++) {
            var index = colorList.indexOf(getColor(i));
            if (index > -1) {
                colorList.splice(index, 1);
            };
        };
        return colorList;
    };

    function getRandomChosenColor() {
        var colorInt = Math.floor(Math.random() * parseInt($("#nudColors")[0].value));
        return getColor(colorInt);
    };

    function getColor(colorInt) {
        return $("#Color_" + colorInt + " option:selected").val();
    };
})
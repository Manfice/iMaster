(function () {
    if (!navigator.cookieEnabled) {
        alert("Coockie files switch off!");
    }
    var persVisible = false;
    var geo;
    $("#ddMenu").on("click", function (event) {
        var el = $(".dropDownPersonal");
        if (!persVisible) {
            $({ y: -156 }).animate({ y: 201 }, {
                duration: 50,
                step: function(now) {
                    el.css("transform", "translateY(" + now + "px)");
                }
            }, {
                easing: "easeOutElastic"
            });
            //el.show("fast");
            persVisible = true;
        } else {
            $({ y: 201 }).animate({ y: -201 }, {
                duration: 50,
                step: function (now) {
                    el.css("transform", "translateY(" + now + "px)");
                }
            }, {
                 easing: "easeOutElastic"
            });
            //el.hide("fast");
            persVisible = false;
        }
    });

    //var canvas = document.getElementById("chart"),
    //    c = canvas.getContext("2d");
    //c.lineWidth = 0.5;
    //for (var i = 0; i <= 50; i++) {
    //    c.lineTo(0, i*10);
    //    c.lineTo(1500, i * 10);
    //    c.moveTo(0, i * 10);
    //    setInterval(50);
    //};
    //for (var y = 0; y <= 150; y++) {
    //    c.lineTo(y*10, 0);
    //    c.lineTo(y*10, 500);
    //    c.moveTo(y*10, 0);
    //};
    //c.moveTo(0, 250);
    //for (var j = 0; j <= 1500; j++) {
    //    c.lineTo(j,j*0.75);
    //}
    //c.stroke();
    //c.strokeStyle = "#ff0000";

     ymaps.ready(function () {
        ymaps.geolocation.get({
            // Выставляем опцию для определения положения по ip
            provider: 'yandex',
            // Автоматически геокодируем полученный результат.
            autoReverseGeocode: true
        }).then(function (result) {
            // Выведем в консоль данные, полученные в результате геокодирования объекта.
            geo = result.geoObjects.get(0).properties.get('metaDataProperty');
            if (geo) {
                $("#myLoc").text(geo.GeocoderMetaData.text);
            }
        });
     });
})();
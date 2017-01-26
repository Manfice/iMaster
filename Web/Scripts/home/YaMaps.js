(function () {
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
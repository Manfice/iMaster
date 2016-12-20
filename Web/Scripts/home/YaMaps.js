(function () {
    var persVisible = false;
    $("#personLabel").on("click", function(event) {
        var el = $(".dropDownPersonal");
        if (!persVisible) {
            $({ y: -176 }).animate({ y: 201 }, {
                duration: 1000,
                step: function(now) {
                    console.log(now);
                    el.css("transform", "translateY(" + now + "px)");
                }
            }, {
                easing: "easeOutElastic"
            });
            el.show("fast");
            persVisible = true;
        } else {
            $({ y: 201 }).animate({ y: -176 }, {
                duration: 1000,
                step: function (now) {
                    console.log(now);
                    el.css("transform", "translateY(" + now + "px)");
                }
            }, {
                 easing: "easeOutElastic"
            });
            el.hide("fast");
            persVisible = false;
        }
    });
})();
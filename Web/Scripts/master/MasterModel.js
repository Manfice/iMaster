var MasterModel = function() {
    /*Model*/
    var model = {
        menu: ko.observable("wow"),
        settings: {
            tab: ko.observable("personal"),
            master: {
                fio:ko.observable()
            }
        },
        orders: ko.observableArray([]),
        goods: ko.observableArray([])
    };
    /*Methods*/
    var setView = function (v) {
        model.menu(v);
    }
    var isActiveView = function(view) {
        return model.menu() === view;
    }
    /*Init*/
    var init = function () {
        ko.applyBindings(model, document.getElementById("masterPage"));
    }

    $(init);
    /*Return*/
    return {
        setView: setView, isActiveView: isActiveView
    }
}()
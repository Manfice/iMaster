var MasterModel = function () {
    var client = MasterClient();
    var fadeUpDownStatus = function() {
        var block = $(".updateStatus");
        block.fadeIn(100);
        setTimeout(function() {
            block.fadeOut();
        }, 2000);
    }
    /*Model*/
    var model = {
        menu: ko.observable("wow"),
        settings: {
            tab: ko.observable("personal"),
            publicInfo: {
                Id: ko.observable(),
                Nikname: ko.observable("Vasia"),
                Country: ko.observable("Россия"),
                City: ko.observable("Ставрополь"),
                AboutMe: ko.observable("Что-то о моем бизнесе"),
                Facebook: ko.observable("Фейсбук"),
                Vkontakte: ko.observable("ВК"),
                Instagram: ko.observable("300 грамм")
            },
            contacts:ko.observableArray()
        },
        orders: ko.observableArray([]),
        goods: ko.observableArray([])
    };

    /*Methods*/
    var setView = function(v) {
        model.menu(v);
    }
    var isActiveView = function(view) {
        return model.menu() === view;
    }
    var setTab = function(v) {
        model.settings.tab(v);
    }
    var isActiveTab = function(view) {
        return model.settings.tab() === view;
    }
    var getPublicInfoCallback = function(data) {
        var pi = model.settings.publicInfo;
        pi.Id(data.Id);
        pi.Nikname(data.Nikname);
        pi.Country(data.Country);
        pi.City(data.City);
        pi.AboutMe(data.AboutMe);
        pi.Facebook(data.Facebook);
        pi.Vkontakte(data.Vkontakte);
        pi.Instagram(data.Instagram);
    }
    var retrievePublicInfo = function() {
        client.getMasterPersonalInfo(getPublicInfoCallback);
    }
    var updPublicInfo = function() {
        client.updatePublicMasterInfo(model.settings.publicInfo, getPublicInfoCallback);
        fadeUpDownStatus();
    }
    var submitPublic = function () {
        updPublicInfo();
    }

    var addContact = function(t) {
        model.settings.contacts.push({ Id: null, Title: ko.observable(t), Value: null, display: "NEW" });

    }
    /*Init*/
    var init = function () {
        retrievePublicInfo();
        ko.applyBindings(model, document.getElementById("masterPage"));
    }

    $(init);
    /*Return*/
    return {
        setView: setView,
        isActiveView: isActiveView,
        setTab: setTab,
        isActiveTab: isActiveTab,
        submitPublic: submitPublic,
        fadeUpDownStatus: fadeUpDownStatus,
        addContact: addContact
    }
}();



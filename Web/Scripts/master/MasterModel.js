var MasterModel = function () {
    var client = MasterClient();
    var fadeUpDownStatus = function() {
        var block = $(".updateStatus");
        block.fadeIn(100);
        setTimeout(function() {
            block.fadeOut();
        }, 2000);
    };
    var $cropImage;

    function readFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function(e) {
                $(".uploadImage").addClass("ready");
                $cropImage.croppie("bind", {
                    url: e.target.result
                });
            };
            reader.readAsDataURL(input.files[0]);
        } else {
            alert("Browser error 400");
        }
    }
    /*Model*/
    var model = {
        menu: ko.observable("settings"),
        settings: {
            tab: ko.observable("avas"),
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
            contacts: ko.observableArray(),
            avatar:ko.observable(null)
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
    /*Croppie image block*/
    var croppieInit = function() {
        $cropImage = $("#uploadBlock").croppie({
            viewport: {
                width: 200,
                height: 200,
                type: "circle"
            },
            enableExif: true,
            boundary: {
                width: 300,
                height:300
                }
        });
    }
    $("#upload").on("change", function() {
        readFile(this);
    });
    $("#uploadResult").on("click", function(ev) {
        $cropImage.croppie("result", {
            type: "canvas",
            size:"viewport"
        }).then(function (dt) {
            model.settings.avatar(dt);
        });
    });
    /*Init*/
    var init = function () {
        retrievePublicInfo();
        croppieInit();
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
        addContact: addContact,
    }
}();



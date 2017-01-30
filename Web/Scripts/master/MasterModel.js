var MasterModel = function () {
    var client = MasterClient();
    var fadeUpDownStatus = function() {
        var block = $(".updateStatus");
        block.fadeIn(100);
        setTimeout(function() {
            block.fadeOut();
        }, 2000);
    };

    /*Model*/
    var model = {
        menu: ko.observable("settings"),
        settings: {
            tab: ko.observable("contacts"),
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
    var getPublicInfoCallback = function (data) {
        
        var pi = model.settings.publicInfo;
        pi.Id(data.PublicMasterInfo.Id);
        pi.Nikname(data.PublicMasterInfo.Nikname);
        pi.Country(data.PublicMasterInfo.Country);
        pi.City(data.PublicMasterInfo.City);
        pi.AboutMe(data.PublicMasterInfo.AboutMe);
        pi.Facebook(data.PublicMasterInfo.Facebook);
        pi.Vkontakte(data.PublicMasterInfo.Vkontakte);
        pi.Instagram(data.PublicMasterInfo.Instagram);
        if (data.Avatar.Path) {
            model.settings.avatar(data.Avatar.Path);
        }
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
        model.settings.contacts.push({ Id: ko.observable(null), Title: ko.observable(t), Value: ko.observable(null), display: "NEW" });

    }
    var updateContacts = function () {
        var c = model.settings.contacts();
        var contacts = [];
        var contact = function(data) {
            this.Id = data.Id;
            this.Title = data.Title;
            this.Value = data.Value;
        }
        c.forEach(function (ct) {

            if (ct.Value()) {
                contacts.push(new contact(ct));
            } else {
                model.settings.contacts.remove(ct);
            }
        });
        client.updateContacts(model.settings.contacts);

    };
    var removeContact = function (c) {
        if (!c.Id()) {
            model.settings.contacts.remove(c);
        }
    }
    /*Croppie image block*/
    var $cropImage;

    function readFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $(".uploadImage").addClass("ready");
                $cropImage.croppie("bind", {
                    url: e.target.result,
                    orientation: 1
                });
            };
            reader.readAsDataURL(input.files[0]);
        } else {
            alert("Browser error 400");
        }
    }
    var croppieInit = function() {
        $cropImage = $("#uploadBlock").croppie({
            viewport: {
                width: 200,
                height: 200,
                type: "circle"
            },
            enableExif: false,
            boundary: {
                width: 300,
                height:300
            },
            enableOrientation: true

        });
    }

    $("#upload").on("change", function() {
        readFile(this);
    });
    var uploadAvatarCallback = function (data) {
        model.settings.avatar(data.Path);
        fadeUpDownStatus();
    }

    var uplAvatar = function(data) {
        var fd = new FormData();
        fd.append("avatar", data, "avatar.png");
        client.uploadAvatar(fd, uploadAvatarCallback);
    }
    $("#uploadResult").on("click", function(ev) {
        $cropImage.croppie("result", {
            type: "blob",
            size:"viewport"
        }).then(function (dt) {
            model.settings.avatar(null);
            uplAvatar(dt);
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
        addContact: addContact, $cropImage: $cropImage,
        updateContacts: updateContacts,
        removeContact: removeContact
    }
}();



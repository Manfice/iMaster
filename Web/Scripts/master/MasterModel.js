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
    function Contact (data,view) {
        this.data = {};
        this.data.Id = ko.observable(data.Id);
        this.data.Title = ko.observable(data.Title);
        this.data.Value = ko.observable(data.Value);
        this.display = ko.observable(view);
    }
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
        if (data.MemberContacts) {
            model.settings.contacts.removeAll();
            var c = data.MemberContacts;
            c.forEach(function(cont) {
                //model.settings.contacts.push({ Id: ko.observable(cont.Id), Title: ko.observable(cont.Title), Value: ko.observable(cont.Value), display: ko.observable("OLD") });
                model.settings.contacts.push(new Contact(cont, "OLD"));
            });
            console.log(ko.toJSON(model.settings.contacts()));
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

    var addContact = function (t) {
        var itm = { Id: null, Title: t, Value: null };
        model.settings.contacts.push(new Contact(itm, "NEW"));

    }
    var updateContactsCallback = function (c) {
        model.settings.contacts.removeAll();
        if (!c) return;
        c.forEach(function(cont) {
            model.settings.contacts.push(new Contact(cont, "OLD"));
        });
        fadeUpDownStatus();
    }
    var updateContacts = function () {
        var c = model.settings.contacts();
        var contacts = [];
        c.forEach(function (ct) {
            if (!ct.data.Value()) {
                model.settings.contacts.remove(ct);
            } else {
                contacts.push(ct.data);
            }
        });
        client.updateContacts(contacts, updateContactsCallback);

    };
    var removeContact = function (c) {
        if (!c.data.Id()) {
            model.settings.contacts.remove(c);
        } else {
            client.removeContact(c.data.Id(), updateContactsCallback);
        }
    }

    var editContact = function (c) {
        c.display("NEW");
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
        removeContact: removeContact, editContact: editContact
    }
}();



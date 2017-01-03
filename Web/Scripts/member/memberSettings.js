var SettingsModel = function () {

    var memberClient = function () {

        var rt = "/api/apimember/";

        var updMember = function (model, callback) {
            var member = {
                PersonName: model.personInfo.PersonName,
                Birthday: model.personInfo.Birthday,
                AboutMe: model.personInfo.AboutMe,
                Country: model.personContacts.Country,
                City: model.personContacts.City,
                Phone: model.personContacts.Phone,
                Email: model.personContacts.Email
            };
            console.log(ko.toJSON(member));

            $.ajax({
                type: "POST",
                url: rt + "UpadteMemberInfo",
                data: member,
                success: function (result) {
                    alert(result);
                }
            });
        }

        return {
            updMember: updMember
        };
    }

    var client = new memberClient();

    var person = {
        personInfo: {
            PersonName: ko.observable("s"),
            AboutMe: ko.observable(""),
            Birthday: ko.observable("29.08.1984")
        },
        personContacts: {
            Email: ko.observable("c592@yandex.ru"),
            Phone: ko.observable("9034441974"),
            Country: ko.observable("Россия"),
            City: ko.observable("Ставрополь")
        }

    }

    var submit = function () {
        client.updMember(person);
    }

    function init() {
        if ($("#mSettings")) {
            ko.applyBindings(SettingsModel, document.getElementById("mSettings"));
        }
    };

    $(init);

    return {
        submit: submit, person: person
    };

}();

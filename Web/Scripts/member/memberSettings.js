var SettingsModel = function () {

    var MemberClient = function () {

        var rt = "/api/apimember/";
        var getMember = function(callback) {
            $.ajax({
                url: rt + "GetMember",
                type: "GET",
                success: function(r) {
                    callback(r);
                }
            });
        };
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
                    callback(result);
                }
            });
        }

        return {
            updMember: updMember, getMember: getMember
        };
    }

    var client = new MemberClient();

    var person = {
        personInfo: {
            PersonName: ko.observable(),
            AboutMe: ko.observable(),
            Birthday: ko.observable()
        },
        personContacts: {
            Email: ko.observable(),
            Phone: ko.observable(),
            Country: ko.observable(),
            City: ko.observable()
        }

    }

    var updatePersonData = function(p) {
        person.personInfo.PersonName(p.PersonName);
        if (moment(p.Birthday).isValid()) {
            person.personInfo.Birthday(moment(p.Birthday).format("DD.MM.YYYY"));
        } else {
            alert("wew");
        }
        person.personInfo.AboutMe(p.AboutMe);
        person.personContacts.City(p.City);
        person.personContacts.Country(p.Country);
        person.personContacts.Email(p.Email);
        person.personContacts.Phone(p.Phone);
    };
    var submit = function () {
        client.updMember(person, updatePersonData);
    }
    var getPersonCallback = function (d) {
        console.log(ko.toJSON(d));
        updatePersonData(d);
    }
    function init() {
        $("#dtpc").datetimepicker({
            locale: "ru",
            viewMode: "years",
            format:"DD.MM.YYYY"

        });
        if ($("#mSettings")) {
            client.getMember(getPersonCallback);
            ko.applyBindings(SettingsModel, document.getElementById("mSettings"));
        }
    };

    $(init);

    return {
        submit: submit, person: person
    };

}();

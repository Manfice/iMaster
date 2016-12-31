(function settingsModel() {

    var person = {
        personInfo: {
            nik: ko.observable("Valera"),
            fio: ko.observable("Petr Povazhniy"),
            birthday: ko.observable("29.08.1984")
        },
        personContacts: {
            mail: ko.observable("c592@yandex.ru"),
            phone: ko.observable("9034441974"),
            country: ko.observable("Россия"),
            city: ko.observable("Ставрополь")
        }

    }

    var submit = function() {
    }

    function init() {
        ko.applyBindings(new settingsModel());
    };

    $(init);

    return {
        submit: submit, person: person
    };

})();
var masterModel = function() {
    /*Model*/
    var model = {
        menu: ko.observable("")
    };
    /*Methods*/
    /*Init*/
    function init() {
        ko.applyBindings(model, document.getElementById("masterPage"));
    }

    $(init());
    /*Return*/

}()
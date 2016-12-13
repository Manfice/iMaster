var Register = function (steps) {
    var totalSteps;
    var model = {
        total:ko.observable(),
        currentStep:ko.observable(1),
        back: function() {
            if (model.currentStep() > 1) {
                model.currentStep(model.currentStep() - 1);
            }
        },
        next: function() {
            if (model.currentStep() < totalSteps) {
                model.currentStep(model.currentStep() + 1);
            }
        },
        done: function() {
            console.log("User press Done");
            model.currentStep(1);
        },
        isLastStep: ko.pureComputed(function () {
            return model.currentStep() === totalSteps;
        },this),
        isFirstStep:ko.pureComputed(function() {
            return model.currentStep() === 1;
        }, this),
        user: {
            userName: ko.observable(),
            address: {
                country: ko.observable(),
                city: ko.observable()
            },
            contacts:ko.observableArray([])
        }
    };

    var init = function () {
        totalSteps = steps;
        model.total(steps);
        ko.applyBindings(model, document.getElementById("wizard"));
    };

    $(init);

    return {
        wizard: wizard
    };
}(3);

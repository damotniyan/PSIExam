(function () {
    "use strict";
    angular
        .module("claimManagement")
        .controller("claimFormCtrl",
                     ["petResource", "Upload", "$timeout",
                     claimFormCtrl]);

    function claimFormCtrl(petResource, Upload, $timeout) {
        var vm = this;
        vm.pickedPet = '';
        vm.files = '';

        petResource.query(function (data) {
            vm.pickedPet = data[0].petName;
            vm.pets = data;            
        });        

        vm.submit = function () {
            var fileNames = '';
            for (var i = 0; i < vm.files.length; i++) {
                fileNames += '\n' + vm.files[i].name; 
            }
            alert('Thank you for submitting\n' + fileNames + '\n\nfor ' + vm.pickedPet);
            vm.files = '';
        };

    }
}());

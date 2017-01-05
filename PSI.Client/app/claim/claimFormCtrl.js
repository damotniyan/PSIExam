(function () {
    "use strict";
    angular
        .module("claimManagement")
        .controller("claimFormCtrl",
                     ["petResource", "Upload", "$timeout", "appSettings", "$q",
                     claimFormCtrl]);

    function claimFormCtrl(petResource, Upload, $timeout, appSettings, $q) {
        var vm = this;
        vm.pickedPet = '';
        vm.files = '';

        petResource.query(function (data) {
            vm.pickedPet = data[0].petName;
            vm.pets = data;            
        });
        vm.submit = function () {
            var deferred = $q.defer();
            var fileNames = '';
            for (var i = 0; i < vm.files.length; i++) {
                fileNames += '\n' + vm.files[i].name;
                var file = vm.files[i];
                if (!file.$error) {
                    Upload.upload({                        
                        url: appSettings.serverPath + '/api/files/upload',
                        method: 'POST',
                        data: {
                            petName: vm.pickedPet,
                            file: file
                        }
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        deferred.resolve(data);
                    }).error(function (data, status, headers, config) {
                        // file failed to upload
                        deferred.reject();
                    });
                }
            }
            alert('Thank you for submitting\n' + fileNames + '\n\nfor ' + vm.pickedPet);
            vm.files = '';
        };

    }
}());

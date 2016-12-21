(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("petResource",
                ["$resource",
                 "appSettings",
                    petResource])

    function petResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/pets/:id");
    }
}());

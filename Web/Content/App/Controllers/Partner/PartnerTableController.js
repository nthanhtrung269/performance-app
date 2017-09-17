﻿var partnerTableController = function (service) {

    var initializeDatatable = function (result) {
        $("#partnerTable").DataTable({
            data: result,
            columns: [
                {
                    data: "Name"
                },
                {
                    data: "Number"
                }
            ],
            language: {
                emptyTable: "No records at present."
            }
        });
    }

    var init = function () {
        service.getPartners(initializeDatatable, initializeDatatable);
    }

    return {
        init: init
    };

}(partnerService)
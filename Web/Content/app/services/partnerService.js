﻿var partnerService = function (routing) {

    var getPartners = function (done, fail) {
        $.ajax({
                url: routing.getApiUri("Partner"),
                type: "GET",
                dataSrc: "",
                dataType: "json"
            }).done(done)
              .fail(fail);
    };

    var getPartnersByAccounts = function (accountId, done, fail) {
        $.ajax({
                url: routing.getApiUri("Partner") + "accounts/" + accountId,
                type: "GET",
                dataSrc: "",
                dataType: "json"
            }).done(done)
            .fail(fail);
    };

    var deletePartner = function (partnerId, done, fail) {
        this.partnerId = partnerId;

        $.ajax({
                url: routing.getApiUri("Partner") + partnerId + "/delete",
                type: "POST",
                method: "DELETE",
                contentType: "text/plain"
            }).done(done)
            .fail(fail);
    }

    return {
        getPartners: getPartners,
        deletePartner: deletePartner,
        getPartnersByAccounts: getPartnersByAccounts
    }
}(routing);
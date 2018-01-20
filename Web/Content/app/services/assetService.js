﻿var AssetService = function (routing) {

    var getAssets = function (done, fail) {
        $.ajax({
                url: routing.getApiUri("Asset"),
                type: "GET",
                dataSrc: "",
                dataType: "json"
            }).done(done)
            .fail(fail);
    };

    var getAssetsByPortfolios = function (portfolioId, done, fail) {
        $.ajax({
            url: routing.getApiUri("Asset") + "portfolios/" + portfolioId,
                type: "GET",
                dataSrc: "",
                dataType: "json"
            }).done(done)
            .fail(fail);
    };

    var getAssetPrices = function (done, fail) {
        $.ajax({
                url: routing.getApiUri("Asset") + "prices",
                type: "GET",
                dataSrc: "",
                dataType: "json"
            }).done(done)
            .fail(fail);
    };

    return {
        getAssets: getAssets,
        getAssetPrices: getAssetPrices,
        getAssetsByPortfolios: getAssetsByPortfolios
    }
}(Routing);
var MasterClient = function() {
    /**/
    var baseUrl = "/api/apimember";
    var getMasterPersonalInfo = function(callback) {
        $.ajax({
            type: "GET",
            url: baseUrl + "/GetMasterPublicInfo",
            success: function (data) {
                callback(data);
            }
        });
    };

    var updatePublicMasterInfo = function(data, callback) {
        $.ajax({
            type: "POST",
            url: baseUrl + "/UpdatePublicInfo",
            data: data,
            success: function(result) {
                callback(result);
            }
        });
    }

    var uploadAvatar = function(data) {
        $.ajax({
            type: "POST",
            url: baseUrl + "/UploadAvatar",
            data: data,
            success: function(result) {
                alert("Send");
            }
        });
    }

    return {
        getMasterPersonalInfo: getMasterPersonalInfo,
        updatePublicMasterInfo: updatePublicMasterInfo,
        uploadAvatar: uploadAvatar
    }
};
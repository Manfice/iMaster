﻿var MasterClient = function() {
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

    var uploadAvatar = function(data,callback) {
        $.ajax({
            type: "POST",
            url: baseUrl + "/UploadAvatar",
            data: data,
            processData: false,
            contentType: false,
            success: function(result) {
                callback(result);
            }
        });
    }
    var updateContacts = function (data) {
        $.ajax({
            url: baseUrl + "/UpdateContacts",
            type: "POST",
            contentType: 'application/json',
            data: ko.toJSON(data),
            success: function(result) {
                alert(ko.toJSON(result));
            }
        });
    }
    return {
        getMasterPersonalInfo: getMasterPersonalInfo,
        updatePublicMasterInfo: updatePublicMasterInfo,
        uploadAvatar: uploadAvatar, updateContacts: updateContacts
    }
};
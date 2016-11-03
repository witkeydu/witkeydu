$(function () {
    $("#imgCheckCode").bind("click", function () {
        this.src = "GetValidateCode?time=" + (new Date()).getTime();
    });
    $("#ThirdPartyQQLogin").bind("click", function () {
        $.get("ThirdPartyQQLogin", null, function (data) {
            alert(data);
        });
    });
    $("#ThirdPartyTBLogin").bind("click", function () {
        $.get("ThirdPartyTBLogin", null, function (data) {
            alert(data);
        });
     });
    $("#ThirdPartyWBLogin").bind("click", function () {
        $.get("ThirdPartyWBLogin", null, function (data) {
            alert(data);
        });
     });
});

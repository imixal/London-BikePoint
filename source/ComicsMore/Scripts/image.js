$("#file-upload").change(function () {
    var data = new FormData();
    var files = $("#file-upload").get(0).files;
    if (files.length > 0) {
        data.append("HelpSectionImages", files[0]);
    }
    $.ajax({
        url: '/Comics/UploadImage/',
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            alert(data);
        },
        error: function () {
            alert("error");
        }

    });
});
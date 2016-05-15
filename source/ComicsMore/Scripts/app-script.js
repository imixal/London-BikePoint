//$(".comment-delete").click(function () {
//    var commentId = $(this).attr("comment-id");
//    jQuery.ajax({
//        type: "POST",
//        url: '@Url.Action("DeleteComment", "Profile")',
//        data: { commentId: commentId },
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",

//        success: function () {
//            alert("success");
//        },
//        error: function () {
//            alert("error");
//        }
//    });
//});
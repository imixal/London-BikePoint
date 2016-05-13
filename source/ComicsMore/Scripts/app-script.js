$(".comment-delete").click(function () {
    commentId = $(this).attr("comment-id");
    jQuery.ajax({
        type: "POST",
        url: 'DeleteComment',
        data: JSON.stringify({ commentId: commentId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function () {
            alert("success");
        },
        error: function () {
            alert("error");
        }
    });
    alert(commentId);
});
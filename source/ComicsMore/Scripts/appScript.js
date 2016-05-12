$(".comment-delete").click(function () {
    commentId = $(this).attr("comment-id");
    jQuery.ajax({
        type: "POST",
        url: 'DeleteComment',
        data: { commentId: commentId },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            alert("success");
        }
    });
    alert(commentId);
});
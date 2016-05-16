$(function () {
    var object_active;
    var gen_text = 1;
    var active_obj = null;
    var number_of_page = 1;
    var current_page_number = 1;
    var comics = {};
    var page = [];
    var tempp;
    var text_id;
    $(document).ready(function () {
        drags();
        
    });
    function drags() {
        $('.draggable')
            .draggable({
                containment: "#page-" + current_page_number,
                stop: function () {
                    page[current_page_number].text[$(this).text()].left = $(this).css("left");
                    page[current_page_number].text[$(this).text()].top = $(this).css("top");
                },
            })
    };
    function active(object_active) {
        if (!$(object_active).hasClass("active")) {
            $(".active").removeClass("active")
            $(object_active).addClass("active");
        }
        else { $(object_active).removeClass("active") }
        $("#change").hide();
    }
    function close() {
        $(".modal-window").hide("fast");
    }
    function cell(celling) {
        switch (celling.style) {
            case "cell-1": {
                $("#page-inner-" + current_page_number).remove();
                $("#page-" + current_page_number).append('<div id="page-inner-' + current_page_number + '"><div class="template-tab"></div></div>');
                $(".template-tab").css({ "width": $("#page-" + current_page_number).css("width"), "height": $("#page-" + current_page_number).css("height"), "border": "solid " + celling.color+ " " +celling.weight + "px" });
                page[current_page_number].template.style = "cell-1";
                $("#cell-1").prop("checked", true);
                break
            }
            case "cell-2": { $("#page-inner-" + current_page_number).remove();
                $("#page-" + current_page_number).append('<div id="page-inner-' + current_page_number + '"><div class="template-tab"></div><div class="template-tab"></div></div>');
                $(".template-tab").css({ "width": $("#page-" + current_page_number).css("width"), "height": Number($("#page-" + current_page_number).css("height").substring(0, 3)) / 2 + "px", "border": "solid " + celling.color + " " + celling.weight + "px" });
                page[current_page_number].template.style = "cell-2";
                $("#cell-2").prop("checked", true);
                break
            }
            case "cell-4": { $("#page-inner-" + current_page_number).remove();
                $("#page-" + current_page_number).append('<div id="page-inner-' + current_page_number + '"><div class="template-tab"></div><div class="template-tab"></div><div class="template-tab"></div><div class="template-tab"></div></div>');
                $(".template-tab").css({ "width": Number($("#page-" + current_page_number).css("width").substring(0, 3)) / 2 + "px", "height": Number($("#page-" + current_page_number).css("height").substring(0, 3)) / 2 + "px", "border": "solid "  +celling.color + " " + celling.weight + "px" });
                page[current_page_number].template.style = "cell-4";
                $("#cell-4").prop("checked", true);
                break
            }
            case "cell-incline": {
                $("#page-inner-" + current_page_number).remove();
                $("#page-" + current_page_number).append('<div id="page-inner-' + current_page_number + '"><div class="template-tab-incline"></div><div class="template-tab-incline">  </div></div>');
                if ($("#A4a").is(":checked")) {
                    $(".template-tab-incline").css({ "width": Number($("#page-" + current_page_number).css("width").substring(0, 3)) * 3 + "px", "height": Number($("#page-" + current_page_number).css("height").substring(0, 3)) / 3 * 2 + "px", "border-bottom": "solid " + celling.color + " " + celling.weight + "px" });
                }
                if ($("#A4b").is(":checked")) {
                    $(".template-tab-incline").css({ "width": Number($("#page-" + current_page_number).css("width").substring(0, 3)) * 3 + "px", "height": Number($("#page-" + current_page_number).css("height").substring(0, 3)) / 4 * 3 + "px", "border-bottom": "solid " + celling.color + " " + celling.weight + "px" });

                }
                $("#incline-cell").prop("checked", true);
                page[current_page_number].template.style = "cell-incline";
                break
            }
        }


    }
    function template() {
        if ($("#cell-1").is(":checked")) {
            page[current_page_number].template.style = "cell-1";
            cell(page[current_page_number].template);
        }
        if ($("#cell-2").is(":checked")) {
            page[current_page_number].template.style = "cell-2";
            cell(page[current_page_number].template);
        }
        if ($("#cell-4").is(":checked")) {
            page[current_page_number].template.style = "cell-4";
            cell(page[current_page_number].template);
        }
        if ($("#incline-cell").is(":checked")) {
            page[current_page_number].template.style = "cell-incline";
            cell(page[current_page_number].template);
        }
        addtexts();
    };
    function page_format(format) {
        $("#page-" + current_page_number).css({ "overflow": "hidden", "position": "absolute", "top": "50px", "left": "310px", "background-color": "#fff" })
        if (format) {
            $("#page-" + current_page_number).css({ "width": "500px", "height": "700px" })
            page[current_page_number].style_page = 1;
            $("#A4a").prop("checked", true);
            cell(page[current_page_number].template);
        }
        else {
            $("#page-" + current_page_number).css({ "width": "700px", "height": "500px" })
            page[current_page_number].style_page = 0;
            $("#A4b").prop("checked", true);
            cell(page[current_page_number].template);
        }
    };
    $("input[name=format]").change(function () {
        
        if ($("#A4a").is(":checked")) {
            page_format(1);
        }
        if ($("#A4b").is(":checked")) {
            page_format(0);
        }
        addtexts();
    });
    $(function () { $("input[name=template]").change(function () { template(); }) })
    $(function () {
        $("input[name=weight]").change(function () {
            page[current_page_number].template.weight = $("input[name=weight]").val();
            template();
        })
    });
    $(function () {
        $("input[name=color]").change(function () {
            page[current_page_number].template.color = $("input[name=color]").val();
            template();
        })
    });

    $("#tool-page").click(function () {
        close();
        $("#modal-window-page").stop().toggle("fast");
        active($(this));
    });
    $("#tool-template").click(function () {
        close();
        $("#modal-window-template").stop().toggle("fast");
        active($(this));
    });
    $("#tool-picture").click(function () {
        close();
        $("#modal-window-picture").stop().toggle("fast");
        active($(this));
    });
    $("#tool-video").click(function () {
        close();
        $("#modal-window-video").stop().toggle("fast");
        active($(this));
    });
    $("#tool-font").click(function () {
        close();
        $("#modal-window-font").stop().toggle("fast");
        active($(this));
    });
    $("#tool-property").click(function () {
        close();
        $("#modal-window-property").stop().toggle("fast");
        active($(this));
    });
    function addtext(text) {
        tempp = gen_text;
        $("#page-inner-" + current_page_number).children().first().append('<div id="t' + tempp + '" class="draggable generate-text speech-bubble speech-bubble-' + text.side_cloud + '" >' + text.value + '</div>');
        $("#t" + tempp).css({"width":text.width+"px","height":text.height+"px","padding":"auto", "font-size": text.size + "px", "color": text.color + "px", "font-weight": text.weight,"left" :text.left,"top": text.top,"background-color":text.color_cloud })
        drags();
        $("#t" + tempp).click(function () {
            text_id = $(this).text();
            change_text($(this).text());
        });
        gen_text++;
    };
    function addtexts() {
        if (Object.keys(page[current_page_number].text).length != 0) {
            for (var item in page[current_page_number].text) {
                addtext(page[current_page_number].text[item]);
            }
        }
    }
    function initilization_page() {
        $("#menu-page").append('<input id="menu-page-' + number_of_page + '" type="radio" name="pages"/><label class="pages" for="menu-page-' + number_of_page + '">' + number_of_page + '</label>');
        $("#edit-page").append('<section id="page-' + number_of_page + '"><div id="page-inner-' + number_of_page + '" >   </div></section>');
        page[number_of_page] = {
            style_page: 1,
            template: {
                style: "cell-1",
                weight: 5,
                color: "",
            },
            picture: {},
            video: {},
            text: [],
        };
        $("#menu-page-" + number_of_page).click(function () {
            $(" section").hide();
            current_page_number = Number($(this).attr("id").replace("menu-page-", ""));
            page_format(page[current_page_number].style_page);
            stop();
            cell(page[current_page_number].template);
            addtexts();
            $("#page-" + current_page_number).show();
            $("#change").hide();

        });
        number_of_page++;

    };
    function text_add(number) {
            page[number].text[$("#textholder-generate").val()] = {
            value: $("#textholder-generate").val(),
            left: 0,
            right: 0,
            size: $("#size-generate").val(),
            color: $("#color-generate").val(),
            weight: $("#weight-generate").val(),
            height: $("#height-generate").val(),
            width: $("#width-generate").val(),
            color_cloud: $("#cloud-generate").val(),
            side_cloud: $("#side-cloud-generate").val(),
        };
    };
    function change_text(text) {
        $("#textholder-change").attr({ "value": page[current_page_number].text[text].value });
        $("#size-change").attr({ "value": page[current_page_number].text[text].size });
        $("#color-change").attr({ "value": page[current_page_number].text[text].color });
        $("#cloud-change").attr({ "value": page[current_page_number].text[text].color_cloud });
        $("#side-cloud-change").attr({ "value": page[current_page_number].text[text].side_cloud });
        $("#change").show();

    };
    $("#textholder-change").change(function () {
        page[current_page_number].text[text_id].value = $("#textholder-change").val();
    });
    /* картинка и облако*/
    $("#generate-btn").click(function () {
            text_add(current_page_number);
            addtext(page[current_page_number].text[$("#textholder-generate").val()]);
    });
    $("#btn-add-page").click(function () { initilization_page(); });

    $("#create").click(function () { 
        comics = {
            name: $("#comics-name").val(),
            pages: page,
        }
        //alert($.param(comics));
        $.ajax({
            traditional: true,
            type: "POST",
           // contentType: "application/json; charset=utf-8",
            url: "CreateComics/",
            data: { json: JSON.stringify(comics)},
            success: function (data) {
                alert(JSON.parse(data).pages[1].style_page);
            },
            error: function(){
                alert("c");
            },
            dataType: "json"
        });
    });
    
});


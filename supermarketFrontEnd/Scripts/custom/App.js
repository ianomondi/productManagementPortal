﻿

const MYLOADER = $(".loading_content");
const ToastSeparator = ';';
const DefaultErrorMessage = 'Oops an error has occurred';

const ApiStatusCodes = {
    SUCCESS: 1,
    ACTIVATE_USER: 5,
    SESSION_OUT: 6,
    STATUS_ERROR: 2,
    STATUS_FAIL: 0
}

function showLoader() {
    MYLOADER.fadeIn("slow");
}

function hideLoader() {
    MYLOADER.fadeOut("slow");
}

function LoadHtmlToPage(element, url) {
    $.ajax({
        url,
        method: 'POST',
        contentType: 'application/html; charset=utf-8',
        //type: 'GET',
        dataType: 'html',
        success: function (result) {
            element.html(result);
        },
        error: function (xhr, status) {
            element.html(`<div class='alert alert-danger'>${xhr.statusText}</div>`)
        }
    })
}


function cleanToastMessage(input) {
    if (input == null || input.length == null) return input;
    return input.replace(ToastSeparator, " ");
}

function GenerateToastError(message = null, title = "Error") {
    if (message == null || message.length == 0) message = DefaultErrorMessage;

    const finalMessage = `error${ToastSeparator}${cleanToastMessage(message)}${ToastSeparator}${title}`;

    return finalMessage;
}

function GenerateToastSuccess(message = null, title = "Success") {

    const finalMessage = `success${ToastSeparator}${cleanToastMessage(message)}${ToastSeparator}${title}`;

    return finalMessage;
}

function GenerateToastInfo(message = null, title = "Message") {

    const finalMessage = `info${ToastSeparator}${cleanToastMessage(message)}${ToastSeparator}${title}`;

    return finalMessage;
}

function addScript(url) {
    var script = document.createElement('script');
    script.type = 'application/javascript';
    script.src = url;

    $("#appendableScripts").append(script);

    //document.head.appendChild(script);
}

/*function submitRegForm(e) {


    let id = $(e).attr('id');

    alert(id);

    debugger;

    $(`#${id}`).validate({
        submitHandler: function (form) {

            let url = $(form).attr('action');

            let fd = new FormData();

            $(form).find('.frm_input').each(function (i, e) {
                fd.append($(e).attr('name'), $(e).val())
            });

            debugger;

            alert(id);

            $.ajax({
                url,
                method: 'POST',
                data: fd,
                cache: false,
                processData: false,
                contentType: false,
                success: function (res) {
                    $('.reg_form_container').html(res);
                },
                error: function (err) {
                    let error = GenerateToastError(err.statusText);

                    toastrAlert(error);
                }
            })
        }
    });

    alert('submitted')

    return false;
}*/


$(window).on('load', function () {
    hideLoader();
});
/*window.addEventListener('load', (event) => {
    console.log('page is fully loaded');
});*/



$(document).ready(function () {

    addScript('https://raw.githack.com/eKoopmans/html2pdf/master/dist/html2pdf.bundle.js');

    var table = $(".data-table").DataTable({
        lengthChange: false,
        stateSave: true,
        select: true,
        dom: 'Bfrtip',
        buttons: [

            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"></i> Copy',
                titleAttr: 'Copy'
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i> Excel',
                titleAttr: 'Excel'
            },
            {
                extend: 'pdfHtml5',
                download: 'open',
                text: '<i class="fa fa-file-pdf-o"></i> PDF',
                titleAttr: 'PDF',
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> Print',
                titleAttr: 'Print',
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10pt')
                        .prepend(
                            '<img src="http://datatables.net/media/images/logo-fade.png" style="position:absolute; top:0; left:0;" />'
                        );

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                },
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'colvis',
                text: '<i class="fa fa-file-pdf-o"></i> Visible Columns',
                titleAttr: 'Colvis',
            }
        ],

        

    });

    table.buttons().container().appendTo('#example_wrapper .col-md-6:eq(0)');

    $('.form-control input[type="file"]').addClass('fill');
    $('.form-control select').addClass('fill');


    $('.form-control input[type="file"]').css('background-color', 'red')

    $('.form-control').on('blur', function () {

        if ($(this).is("input:file") || $(this).is("select")){
            if (!$(this).hasClass('fill')) $(this).addClass('fill');
        }
        
    });



    //add option

    let varintOptionForm = $('#variantOptionModal');

    $(".varOptionTrigger").on("click", function () {
        let url = $(this).data("url");
        let title = $(this).data("title");

        let value = $(this).data('value');

        if (value !== undefined && value != '') {
            $('#variant_option_val').val(value);
        } else {
            $('#variant_option_val').val('');
        }



        varintOptionForm.attr('action', url);
        varintOptionForm.data('title', title);
    })

    varintOptionForm.on("submit", function (e) {
        e.preventDefault();

        let title = $(this).data('title');

        let url = $(this).attr('action');

        let data = {
            value: $('#variant_option_val').val()
        };


        $.ajax({
            method: "POST",
            url,
            processData: false,
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            cache: false,
            data: JSON.stringify(data),
            beforeSend: function () {
                showLoader();
            },
            complete: function () {
                hideLoader();
            },
            success: function (data) {
                /*hideLoader();
                $(".loading_content").css("display", "none");*/

                console.log(data);


                switch (data.status) {
                    case ApiStatusCodes.SUCCESS:

                        toastr.success(data.message, title);

                        setTimeout(function () {
                            window.location.reload();
                        }, 2000);

                        break;
                    default:
                        toastr.error(data.message, title);
                        break;

                }
            },
            error: function () {
                //hideLoader();

                toastr.error('Oops! An error has occurred', 'Delete Failed');

            }
        });
    });


    

    $(".lazy_loaded").html(`<div class="load_img"><img src="/Content/imgs/Ripple-1s-200px.gif" alt="" /></div>`);

    if ($(".lazy_loaded").length > 0) {
        $(".lazy_loaded").each(function (i, e) {
            const url = $(this).data('url');

            const elem = $(e);


            $.ajax({
                url,
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    elem.html(result);
                },
                error: function (xhr, status) {
                    //alert(status);
                }
            })


        });
    }

    $(".delete_ajax").click(function (e) {
        e.preventDefault();

        hideLoader();

        let confirm = window.confirm("Are you sure you want to remove this?");

        //alert(confirm === false)


        let parent = $(this).closest(".delete_ancestor");

        if (confirm === true) {
            $(".loading_content").css("display", "flex");
            $.ajax({
                method: "POST",
                url: $(this).attr("href"),
                processData: false,
                cache: false,
                data: {},
                beforeSend: function () {
                    showLoader();
                },
                complete: function () {
                    hideLoader();
                },
                success: function (data) {
                    /*hideLoader();
                    $(".loading_content").css("display", "none");*/

                    console.log(data);

                    debugger;

                    switch (data.status) {
                        case ApiStatusCodes.SUCCESS:

                            parent.remove();
                            toastr.success(data.message, 'Delete');

                            break;
                        default:
                            toastr.error(data.message, 'Delete');
                            break;

                    }
                },
                error: function () {
                    //hideLoader();

                    toastr.error('Oops! An error has occurred', 'Delete Failed');
                    
                }
            });
        } else {
            $(".loading_content").css("display", "none");
            hideLoader();
        }
        $(".loading_content").css("display", "none");
        hideLoader();
    });
});

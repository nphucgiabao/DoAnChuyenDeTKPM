﻿let dataTable;
let tableId = '#dataTable';
var options = {
    'backdrop': 'static',
    'keyboard': true,
    'show': true,
    'focus': false
};
$(document).ready(function () {

    $.extend(true, $.fn.dataTable.defaults, {
        dom:
            "<'row'<'col-12 col-sm-6'l><'col-12 col-sm-6 text-right table-tools-col'f>>" +
            "<'row'<'col-12 scroll-x'tr>>" +
            "<'row'<'col-12 col-md-5'i><'col-12 col-md-7'p>>",
        renderer: 'bootstrap'
    });
    $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
    dataTable = $(tableId).DataTable({
        "bInfo": false,
        "responsive": false,
        "autoWidth": false,
        "ajax": {
            "url": '/Manage/Driver/GetAll',
            "type": "GET",
            "contentType": "application/json",
            "datatype": "json",
            "dataSrc": function (json) {
                if (json.data)
                    return json.data;
                return [];
            },
            "error": function (jqXHR, exception, error) {
                console.log(error + ': ' + jqXHR.responseText);
            }
        },
        "columnDefs": [
            { "data": "name", "orderable": false, "className": " ", "targets": 0 },
            { "data": "bienSoXe", "className": "text-center ", "orderable": false, "targets": 1 },
            { "data": "phone", "className": "text-center ", "orderable": false, "targets": 2 },
            {
                "data": "id", "className": "text-center ", "orderable": false, "targets": 3,
                "render": function (data, type, row, meta) {
                    return `<a class='btn btn-info btn-sm text-white' onclick=showPopup('/Manage/Driver/AddEdit?id=${data}')>Chỉnh sửa</a><a class='btn btn-primary btn-sm text-white' style='margin-left:5px' onclick=showPopup('/Manage/Driver/CreateAccount?id=${data}')>Tạo tài khoản</a>`;
                }
            }
            
        ],
        "createdRow": function (row, data, dataIndex) {
            $(row).addClass('d-style bgc-h-default-l4');
        },
        colReorder: {
            //disable column reordering for first and last columns
            fixedColumnsLeft: 1,
            fixedColumnsRight: 1
        },
        classes: {
            sLength: "dataTables_length text-left w-auto"
        },
        buttons: {
            dom: {
                button: {
                    className: 'btn'//remove the default 'btn-secondary'
                },
                container: {
                    className: 'dt-buttons btn-group bgc-white-tp2 text-right w-auto'
                }
            },
            buttons: [
                //{
                //    "extend": "colvis",
                //    "text": "<i class='far fa-eye text-125 text-dark-m2'></i> <span class='d-none'>Ẩn/hiện cột</span>",
                //    "className": "btn-sm btn-outline-info btn-h-outline-primary btn-a-outline-primary",
                //    columns: ':not(:first)'
                //},
                //{
                //    "extend": "copy",
                //    "text": "<i class='far fa-copy text-125 text-purple'></i> <span class='d-none'>Sao chép bảng</span>",
                //    "className": "btn-sm btn-outline-info btn-h-outline-primary btn-a-outline-primary"
                //},
                //{
                //    "extend": "csv",
                //    "text": "<i class='fa fa-database text-125 text-success-m2'></i> <span class='d-none'>Xuất sang CSV</span>",
                //    "className": "btn-sm btn-outline-info btn-h-outline-primary btn-a-outline-primary"
                //},
                //{
                //    "extend": "print",
                //    "text": "<i class='fa fa-print text-125 text-orange-d1'></i> <span class='d-none'>In</span>",
                //    "className": "btn-sm btn-outline-info btn-h-outline-primary",
                //    autoPrint: false,
                //    message: 'This print was produced using the Print button for DataTables'
                //}
            ]
        },
        //multiple row selection
        select: {
            style: 'multis'
        },
        order: [],//no specific initial ordering,
        initComplete: function (settings, json) {
            dataTable.buttons().container().appendTo($('.table-tools-col:eq(0)', dataTable.table().container()));
        },
        language: {
            search: '<i class="fa fa-search pos-abs mt-2 ml-3 text-blue-m2"></i>',
            searchPlaceholder: " Tra cứu ...",
            emptyTable: "Dữ liệu hiện tại chưa có...",
            zeroRecords: "Không tìm thấy dữ liệu...",
            //sLengthMenu: "Hiển thị _MENU_ mẫu tin",
            loadingRecords: "<img src='asset/image/loadingdata.gif' style='width: 40px; height: 40px;'/>",
            paginate: {
                "first": "First",
                "last": "Last",
                "next": "Tiếp theo",
                "previous": "Quay lại"
            }
        },
        "bLengthChange": false
    });
    //specify position of table buttons
    $('.table-tools-col')//specified above in $.fn.dataTable.defaults
        .append(dataTable.buttons().container())
        //move searchbox into table header
        .find('.dataTables_filter').find('input').addClass('pl-4 radius-round').prop('name', 'search')
        //and add a "plus" button
        .end().append('<button data-rel="tooltip" type="button" data-toggle="ajax-modal" class="btn btn-sm btn-primary" title="Thêm mới" data-url="/Manage/Driver/AddEdit">Thêm</button>');

    //dataTable.on('draw', function () {
    //    $('.js-switch').each(function () {
    //        new Switchery($(this)[0], $(this).data());
    //    });
    //});

    placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        showPopup(url);
    });

});

function addEdit(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        let headers = createHeader(form);
        let model = $(form).serializeJSON();
        model['TypeCar'] = 1;
        postData('/Manage/Driver/AddEdit', JSON.stringify(model), headers, 'application/json; charset=utf-8')
            .then((response) => {
                console.log(response);
                if (response.success) {
                    placeholderElement.find('.modal').modal('hide');
                    alert(response.message);
                    dataTable.ajax.reload();
                }
                else {
                    console.log(response.message);
                }
            }).catch(err => console.log(err));
    }
    return false;
}

function createAccount(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        let headers = createHeader(form);
        let model = $(form).serializeJSON();        
        postData('/Manage/Driver/CreateAccount', JSON.stringify(model), headers, 'application/json; charset=utf-8')
            .then((response) => {
                console.log(response);
                if (response.success) {
                    placeholderElement.find('.modal').modal('hide');
                    alert(response.message);
                    //dataTable.ajax.reload();
                }
                else {
                    console.log(response.message);
                }
            }).catch(err => console.log(err));
    }
    return false;
}
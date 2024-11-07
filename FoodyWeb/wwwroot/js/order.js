var dataTable;


$(document).ready(function () {
    var url = window.location.search;
    var status = ["allStatus", "inprocess", "completed", "pending", "approved"];
    var paymentType = ["allPayment", "Card", "Cash"];


    var selectedPaymentType = "allPayment";
    var selectedStatus = "allStatus";

    for (var i = 0; i < status.length; i++) {
        if (url.includes(status[i])) {
            selectedStatus = status[i];
            break;  
        }
    }

    for (var j = 0; j < paymentType.length; j++) {
        if (url.includes(paymentType[j])) {
            selectedPaymentType = paymentType[j];
            break;  
        }
    }

    loadDataTable(selectedStatus, selectedPaymentType);

});


function loadDataTable(status, payMentType) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status + '&payMentType=' + payMentType },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "20%" },
            { data: 'applicationUser.email', "width": "25%" },
            { data: 'paymentMethod', "width": "10%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'paymentStatus',"width":"10%"},
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>               
                    </div>`
                },
                "width": "10%"
            }
        ]
    });

}

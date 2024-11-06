var dataTable;


$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("allStatus");
                }
            }
        }
    }

});
//alert("Enter select status");
//var status = ["allStatus","inprocess", "completed", "pending","approved"]
//var paymentType = ["allPayment","Card","Cash"];

//var selectedPaymentType = "allPayment";
//var selectedStatus = "allStatus";

//status.forEach(element => {
//    if (url.includes(element)) {
//        selectedStatus = element;
//        break;
//    }
//})
//paymentType.forEach(element => {
//    if (url.includes(element)) {
//        selectedPaymentType = element;
//        break;
//    }
//})
//loadDataTable(selectedStatus, selectedPaymentType);

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'applicationUser.phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "25%" },
            { data: 'paymentMethod', "width": "10%" },
            { data: 'orderStatus', "width": "10%" },
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

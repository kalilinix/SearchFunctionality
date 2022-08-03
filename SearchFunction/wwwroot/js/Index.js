//$("#btnsearch").click(function () {
//    var txtSearch = $.trim($("#txtSearch").val());
//    if (txtSearch == "" || txtSearch == null) {
//        window.alert("Please enter the details");
//    }
//    else {

//        window.location.href = "/home/search?Search=" + txtSearch;

//    }

//});


//$("#btnclear").click(function () {
//    $("#txtSearch").val("");
//    $("#txtSearch").trigger("reset");
//})

$(function () {
    //debugger;
    var $grid = $("#gdCustomerMaster");
    var SearchEnt = {
        Id: null, Name: null, Gender: null,
        Email: null
    };
    BindGrid2(SearchEnt);


    $("#btnSearch").click(function () {
        var Id = $.trim($("#txtid").val());
        var Name = $.trim($("#txtCustomerName").val());
        var Gender = $.trim($("#txtGender").val());
        var Email = $.trim($("#txtEmail").val());


        SearchEnt = {
            Id: Id, Name: Name, Gender: Gender,
            Email: Email

        };
        //$("#gdCustomerMaster").jqGrid('setGridParam', { url: "/Home/GetAllCustomers?SearchEnt=" + JSON.stringify(SearchEnt) });
        BindGrid2(SearchEnt);
       // $("#gdCustomerMaster").trigger("reloadGrid");
    });

    $("#btnClear").click(function () {
        $("#txtid").val("");
        $("#txtCustomerName").val("");
        $("#txtGender").val("");
        $("#txtEmail").val("");


        SearchEnt = {
            Id: null, Name: null, Gender: null,
            Email: null

        };
        $("#gdCustomerMaster").jqGrid('setGridParam', { url: "/Home/GetAllCustomers?SearchEnt=" + JSON.stringify(SearchEnt) });
        $("#gdCustomerMaster").trigger("reloadGrid");
    });

});

function LinkCustomerNumber(id) {
    var row = id.split("=");
    var row_ID = row[1];
    //console.log(row[1]);
    var sCustomerNo = $("#gdCustomerMaster").getCell(row_ID, 'Id');
    var url = "/CustomerMaster/CustomerDetails?CustomerNo=" + sCustomerNo;
    window.open(url);
}




function BindGrid2(SearchEnt) {

    $("#gdCustomerMaster").jqGrid
        ({
            url: "/Home/GetAllCustomers?SearchEnt=" + JSON.stringify(SearchEnt),
            datatype: 'json',
            mtype: 'Get',
            colNames: ['Id', 'Name', 'Gender', 'Email'],
            //colModel takes the data from controller and binds to grid
            colModel: [
                {
                    name: 'Id', width: 80, align: 'center', display: 'inline-block', formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkCustomerNumber('", addParam: "');" }
                },
                { name: 'Name', autowidth: true },
                { name: 'Gender', width: 80, align: 'center' },
                { name: 'Email', width: 80, align: 'center' },

            ],
            height: '100%',
            rowNum: 20,
            rowList: [20, 40, 60, 80, 100, 120],
            pager: '#pager2',
            viewrecords: true,
            caption: '',
            emptyrecords: 'No records',
            sortname: 'Id',
            sortorder: "Id",
            jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            //  gridview: true,
            autowidth: true,
        });
    jQuery("#gdCustomerMaster").jqGrid('navGrid', '#pager2', { edit: false, add: false, del: false, search: false });
    //jQuery("#gdCustomerMaster").jqGrid('setLabel', 'CUSTOMER_NUMBER_CSTM', '', { 'text-align': 'right' });
}


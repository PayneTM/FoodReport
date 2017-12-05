var isFormAllowed = true;
function Save(itemid) {
    var inputs = document.getElementsByTagName("input");

    var result =
        {
            list: [],
            id: itemid
        };
    for (var i = 1; i < inputs.length; i = i + 3) {
        var field =
            {
                product: '',
                count: 0,
                price: 0
            };
        field.product = inputs.item(i).value;
        field.count = inputs.item(i + 1).value;
        field.price = inputs.item(i + 2).value;
        result.list.push(field);
    };
    var leng = result.toString().length;
    var datar = JSON.stringify(result);
    $.ajax(
        {
            type: "POST",
            url: "/api/report/edit/" + itemid,
            data: datar,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
}
function Approve(userid, admin) {
    var result =
        {
            id: userid,
            status: "Approved",
            reason: null,
            adminName: admin
        };
    var datar = JSON.stringify(result);
    $.ajax(
        {
            type: "POST",
            url: "/api/report/approve/" + userid,
            data: datar,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: window.location.href = '/api/report/all/'
        });
}
function RejectForm(itemid, admin) {
    if (isFormAllowed == true) {

        var global = document.getElementById("rej");
        var divv = document.createElement("div");

        var textarea = document.createElement("textarea");
        textarea.id = "reason";
        var labeel = document.createElement("label");
        labeel.htmlFor = "reason";
        labeel.innerHTML = "Reason"

        var send = document.createElement("a");
        send.innerHTML = "Send";
        send.onclick = function () {
            Reject(itemid, admin);
        };

        divv.appendChild(labeel);
        divv.appendChild(document.createElement("br"));
        divv.appendChild(textarea);
        divv.appendChild(document.createElement("br"));
        divv.appendChild(send);
        divv.appendChild(document.createElement("br"));
        global.appendChild(divv);
        isFormAllowed = false;
    }
}
function Reject(itemid, admin) {
    var result =
        {
            id: itemid,
            status: "Rejected",
            reason: document.getElementById("reason").value,
            adminName: admin
        };
    var datar = JSON.stringify(result);
    $.ajax(
        {
            type: "POST",
            url: "/api/report/approve/" + itemid,
            data: datar,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: window.location.href = '/api/report/all/'
        });
}
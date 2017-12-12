var isFormAllowed = true;
function Save(itemid) {
    var inputs = document.getElementsByTagName("input");
    var opt = document.getElementsByTagName("select");

    var result =
        {
            list: [],
            id: itemid
        };
    for (var i = 1, j = 0; i < inputs.length; i = i + 2, j++) {
        var field =
            {
                product: '',
                count: 0,
                price: 0
            };
        if (j != opt.length) {
            field.product = opt[j].value;
        }
        field.count = inputs.item(i).value;
        field.price = inputs.item(i + 1).value;
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
            dataType: "json",
            success: window.location.href = "/api/report/details/" + itemid
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
            success: window.location.href = '/api/report/refresh/'
        });
}
function RejectForm(itemid, admin) {
    if (isFormAllowed == true) {

        var global = document.getElementById("rej");
        var divv = document.createElement("div");

        var textarea = document.createElement("textarea");
        textarea.id = "reason";
        textarea.style.width = "500px";
        textarea.style.height = "100px";
        var labeel = document.createElement("label");
        labeel.htmlFor = "reason";
        labeel.innerHTML = "Reason:"

        var send = document.createElement("a");
        send.innerHTML = "Send";
        send.className = "btn btn-default";
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
            success: window.location.href = '/api/report/refresh/'
        });
}
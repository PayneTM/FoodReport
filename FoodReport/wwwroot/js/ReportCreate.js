var counter = 2;
var isNewRowAllowed = true;
var list = [];
function Add() {
    if (NotEmptyInputs()) {

        //make inputs
        ///////////////////////////////////////
        //Product input
        //<input class="form-control" type="text" id = product# placeholder="Product">
        //////////////////////////////////////
        //var input1 = document.createElement("input");
        var input1 = document.createElement("select");
        input1.className = "form-control";
        input1.type = "text";
        input1.id = "prod" + counter;
        for (var i = 0; i != list[0].length; i++) {
            var rest = list[0][i].name;
            var opt = document.createElement("option");
            opt.innerHTML = rest;
            input1.options.add(opt);
        }

        ///////////////////////////////////////
        //Count input
        //<input class="form-control" type="text" id = count# placeholder="Count">
        //////////////////////////////////////
        var input2 = document.createElement("input");
        input2.className = "form-control";
        input2.type = "number";
        input2.id = "count" + counter;
        input2.placeholder = "Count";

        ///////////////////////////////////////
        //Price input
        //<input class="form-control" type="text" id = price# placeholder="Price">
        //////////////////////////////////////
        var input3 = document.createElement("input");
        input3.className = "form-control";
        input3.type = "number";
        input3.id = "price" + counter;
        input3.placeholder = "Price";


        //make divs for inputs
        var divinput1 = document.createElement("div");
        divinput1.className = "col-sm-4";
        divinput1.style.paddingTop = "5px";
        divinput1.appendChild(input1);

        var divinput2 = document.createElement("div");
        divinput2.className = "col-sm-4";
        divinput2.style.paddingTop = "5px";

        divinput2.appendChild(input2);

        var divinput3 = document.createElement("div");
        divinput3.className = "col-sm-4";
        divinput3.style.paddingTop = "5px";

        divinput3.appendChild(input3);


        //get global div
        var cont = document.getElementById("container");

        var block = document.createElement("div");
        block.id = "blockInput" + counter;
        block.appendChild(document.createElement("br"));
        block.appendChild(divinput1);
        block.appendChild(divinput2);
        block.appendChild(divinput3);
        cont.appendChild(block);
        counter++;
    }
}

function NotEmptyInputs() {
    var OK = true;
    var inputs = document.getElementsByTagName("input");
    for (var i = 1; i != inputs.length; i++) {
        if (inputs.item(i).value.length == 0) {
            inputs.item(i).style.borderColor = "red";
            OK = false;
        }
        else {
            inputs.item(i).style.borderColor = "silver";
        }
    };
    return OK;
}

function Send() {
    var inputs = document.getElementsByTagName("input");
    var opt = document.getElementsByTagName("select");

    var result = [];
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
        result.push(field);
    };
    var leng = result.toString().length;
    var datar = JSON.stringify(result);
    $.ajax(
        {
            type: "POST",
            url: "create",
            data: datar,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: window.location.href = '/api/report/all'
        });
}
function GetProds() {
    var product =
        {
            id: "",
            available: 0,
            name: "",
            price: 0,
            provider: "",
            unit: ""
        };
    $.getJSON("/api/product/prodsjson", product, function (product) {
        list.push(product);
    });
}
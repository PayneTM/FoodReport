var counter = 2;
function Add() {
    //make inputs
    ///////////////////////////////////////
    //Product input
    //<input class="form-control" type="text" id = product# placeholder="Product">
    //////////////////////////////////////
    var input1 = document.createElement("input");
    input1.className = "form-control";
    input1.type = "text";
    input1.id = "prod" + counter;
    input1.placeholder = "Product";

    ///////////////////////////////////////
    //Count input
    //<input class="form-control" type="text" id = count# placeholder="Count">
    //////////////////////////////////////
    var input2 = document.createElement("input");
    input2.className = "form-control";
    input2.type = "text";
    input2.id = "count" + counter;
    input2.placeholder = "Count";

    ///////////////////////////////////////
    //Price input
    //<input class="form-control" type="text" id = price# placeholder="Price">
    //////////////////////////////////////
    var input3 = document.createElement("input");
    input3.className = "form-control";
    input3.type = "text";
    input3.id = "price" + counter;
    input3.placeholder = "Price";


    //make divs for inputs
    var divinput1 = document.createElement("div");
    divinput1.className = "col-sm-4";
    divinput1.appendChild(input1);
    divinput1.appendChild

    var divinput2 = document.createElement("div");
    divinput2.className = "col-sm-4";
    divinput2.appendChild(input2);

    var divinput3 = document.createElement("div");
    divinput3.className = "col-sm-4";
    divinput3.appendChild(input3);


    //get global div
    var cont = document.getElementById("container");

    var block = document.createElement("div");
    block.id = "blockInput" + counter;


    block.appendChild(divinput1);
    block.appendChild(divinput2);
    block.appendChild(divinput3);
    cont.appendChild(block);
    counter++;
}

function Send() {
    var inputs = document.getElementsByTagName("input");

    var result = [];
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
            success: //function () {
            //    $.ajax({
            //        type: "GET",
            //        url: "all"
            //    });
            //}
            window.location.href = 'all'
        });
}
var colorArray = {
    green: "black",
    black: "white",
    blue: "white",
    white: "black"
};

var classColor = "green";
var ingredients = [];
var instructions = [];
var textBoxId;

async function formVals() {
    var ingredientList = "";
    var instructionList = "";

    await retainValues();

    ingredients.forEach(function (value, index) {
        ingredientList += value + '|';
    });
    instructions.forEach(function (value, index) {
        instructionList += value + '|';
    });

    $("#ing").val(ingredientList);
    $("#ins").val(instructionList);
    return true;
}
function do_resize(textbox) {
    var maxrows = 8;
    var txt = textbox.value;
    var cols = textbox.cols;

    var arraytxt = txt.split('\n');
    var rows = 1;

    for (i = 0; i < arraytxt.length; i++)
        rows += parseInt((arraytxt[i].length) / cols);

    if (rows > maxrows) {
        textbox.rows = maxrows;
    }
    else {
        textbox.rows = rows;
    }
}
function resetCursor(txtElement) {
    if (txtElement.setSelectionRange) {
        txtElement.focus();
        txtElement.setSelectionRange(0, 0);
    } else if (txtElement.createTextRange) {
        var range = txtElement.createTextRange();
        range.moveStart('character', 0);
        range.select();
    }
}
async function handleEnter(e, textBox, prevent) {
    await retainValues();

    var keycode = (e.keyCode ? e.keyCode : e.which);
   
    var val = document.getElementById(textBox.id).value;
    if (keycode == '13' && textBox.id === 'Name') {
        e.preventDefault();
    } else if (keycode == '13' && val != '') {
        addItem(textBox.id);
        resetCursor(textBox);
        e.preventDefault();
    } else if (keycode == '13') {
        resetCursor(textBox);
        e.preventDefault();
    }
}
async function addItem(BoxId) {
    await retainValues();

    textBoxId = BoxId;

    $("#dynamic-list-" + textBoxId).empty();
    var candidate = document.getElementById(textBoxId);
    if (!candidate.value == "" && !candidate.value.includes("\n")) {
        if (textBoxId == "ing")
            ingredients.push(candidate.value);
        else
            instructions.push(candidate.value);
        printList(textBoxId);
        candidate.value = candidate.defaultValue;
    }
    else
        printList(textBoxId);

    candidate.value = candidate.defaultValue;
}
async function removeItem(item, index, list) {
    await retainValues();

    textBoxId = list.id;

    var array = [];
    if (list.id == "ing")
        array = ingredients;
    else
        array = instructions

    array.splice(index, 1);

    if (list.id == "ing")
        $("#dynamic-list-ing").empty();
    else
        $("#dynamic-list-ins").empty();

    printList(list.id);
}
function printList(textBoxId) {
    $("#dynamic-list-" + textBoxId).empty();

    var array = [];
    if (textBoxId == "ing") {
        array = ingredients;
    }
    else {
        array = instructions;
    }

    for (var i = 0; i < array.length; i++) {
        $("#dynamic-list-" + textBoxId)
            .append('<li><a onclick="removeItem(this, ' + `${i}, ${textBoxId}` + ')" class="glyphicon glyphicon-remove btn btn-xs btn-danger"> Remove Item</a> ' +
            `<textarea rows="1" cols="60" wrap="hard" id="` + (textBoxId + array[i]) + `" class="form-control ` + classColor + `" onkeyup="new do_resize(this);" onkeypress="handleEnter(event, this, true)">${array[i]}</textarea>` + '</li>');
    }
} 
async function color(shade) {
    await retainValues();

    var elements = $("." + classColor);
    elements.each(function (index, value) {
        $(this).html($(this).attr('class', 'form-control ' + shade.id));
    });

    classColor = shade.id;

    printList("ing");
    printList("ins");
}
async function retainValues() {
    instructions = [];
    ingredients = [];

    await $("ol[name=Ingredients]").children().children("textarea").each(function (index, value) {
        ingredients.push(this.value);
    });
    await $("ol[name=Instructions]").children().children("textarea").each(function (index, value) {
        instructions.push(this.value);
    });
}
// Write your Javascript code.
jQuery.fn.multiselect = function () {
    $(this).each(function () {
        var checkboxes = $(this).find("input:checkbox");
        checkboxes.each(function () {
            var checkbox = $(this);
            // Highlight pre-selected checkboxes
            if (checkbox.prop("checked"))
                checkbox.parent().addClass("multiselect-on");

            // Highlight checkboxes that the user selects
            checkbox.click(function () {
                if (checkbox.prop("checked"))
                    checkbox.parent().addClass("multiselect-on");
                else
                    checkbox.parent().removeClass("multiselect-on");
            });
        });
    });
};


var idButton = 1
//Add imput for Attribut produit
function addInputAttribut()
{
    var div = document.getElementById("attributs");
    var select = div.getElementsByTagName('select')[0];
    var input = div.getElementsByTagName('input')[0];
    var test = select.cloneNode(true);
    var test2 = input.cloneNode(true);
    var span = document.createElement("p");
    var ButtonDeleteSpan = document.createElement("button");
    ButtonDeleteSpan.type = "button";
    ButtonDeleteSpan.setAttribute("onclick", "DeleteSpan(" + idButton + ")");
    ButtonDeleteSpan.setAttribute("class", "close");
    ButtonDeleteSpan.setAttribute("aria-label", "Close;");
    ButtonDeleteSpan.className = "btn btn-secondary";
    var Bspan = document.createElement("span");
    Bspan.setAttribute("aria-hidden", "true");
    Bspan.setAttribute("value", "&times;");
    Bspan.textContent = "X";
    ButtonDeleteSpan.appendChild(Bspan);
    span.id = "span" + idButton;
    span.appendChild(test);
    span.appendChild(test2);
    span.appendChild(ButtonDeleteSpan);
    idButton += 1;

    //////////<button type="button" onclick="addInputAttribut()">Ajouter Attributs</button>


    //div.appendChild(select);
    //div.appendChild(input);
    //newAttributsValue = Element.getElementById("attributs").cloneNode(true);
    //document.getElementById('attributs').appendChild(test);
    //document.getElementById('attributs').appendChild(test2);
    document.getElementById('attributs').appendChild(span);
    
    //var newAttributsName = document.createElement('input');
    //newAttributsName.type = 'Text';
    //newAttributsName.name = 'nameAttribut';
    //newAttributsName.class = "form-control";
    //document.getElementById('attributs').appendChild(newAttributsName);
}

function DeleteSpan(idSpan)
{
    var x = document.getElementById('span' + idSpan);
    x.remove();
}

var doc = new jsPDF();
var specialElementHandlers = {
    '#editor': function (element, renderer) {
        return true;
    }
};

$('#cmd').click(function () {
    doc.fromHTML($('#content').html(), 15, 15, {
        'width': 170,
        'elementHandlers': specialElementHandlers
    });
    doc.save('sample-file.pdf');
});
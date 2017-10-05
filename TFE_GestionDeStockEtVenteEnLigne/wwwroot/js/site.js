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



//Add imput for Attribut produit
function addInputAttribut()
{
    var div = document.getElementById("attributs");
    var select = div.getElementsByTagName('select')[0];
    var input = div.getElementsByTagName('input')[0];
    var test = select.cloneNode(true);
    var test2 = input.cloneNode(true);
    
    //div.appendChild(select);
    //div.appendChild(input);
    //newAttributsValue = Element.getElementById("attributs").cloneNode(true);
    document.getElementById('attributs').appendChild(test);
    document.getElementById('attributs').appendChild(test2);
    
    //var newAttributsName = document.createElement('input');
    //newAttributsName.type = 'Text';
    //newAttributsName.name = 'nameAttribut';
    //newAttributsName.class = "form-control";
    //document.getElementById('attributs').appendChild(newAttributsName);
}

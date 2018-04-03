
$(document).ready(function () {    
    $('#MainContent_ddlCategoria').on('change', function () {
    	//$('#MainContent_ddlCategoria option').removeAttr("selected")
    	$(this).find('option:selected').attr("selected", "selected").siblings().removeAttr("selected");
    });
});
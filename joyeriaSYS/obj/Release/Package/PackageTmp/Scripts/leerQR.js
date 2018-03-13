Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoad)
function pageLoad() {
    $('#reader').html5_qrcode(function (data) {
        $('#ContentPlaceHolder1_txtCodigo').val(data);
        document.getElementById("ContentPlaceHolder1_btnValidar").click();
    },
        function (error) {
        }, function (videoError) {
            alert("No hay cámara");
        }
    );
}
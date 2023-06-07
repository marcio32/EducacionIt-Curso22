
$(document).ready(function () {
    debugger
    var conexion = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
    conexion.start().then(() => { conexion.invoke("AgregarAlGrupo", "0") });

    $("#btnEnviar").click(function (e) {
        debugger
        var room = 0;
        var usuario = $("#usuario").val();
        var mensaje = $("#mensaje").val();
        conexion.invoke("EnviarMensaje", room, usuario, mensaje);
        $("#mensaje").val("");
        $("#mensaje").focus();
        e.preventDefault();
    });

    conexion.on("RecibirMensaje", (usuario, mensaje) => {
        debugger
        var li = $("<li>", { "class": "list-group-item" });
        var small = $("<small>", { "text": usuario + "  -  " });
        li.append(small);
        li.append(mensaje);
        $("#mensajes").append(li);
    });
});

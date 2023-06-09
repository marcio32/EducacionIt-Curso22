var tablaUsuarios;

$(document).ready(function () {
    var token = getCookie("Token");
    var ajaxUrl = getCookie("AjaxUrl");
    tablaUsuarios = $('#usuarios').DataTable(
        {
            ajax: {
                url: `${ajaxUrl}Usuarios/BuscarUsuarios`,
                dataSrc: "",
                headers: {"Authorization": "Bearer " + token}
            },
            columns: [
                { data: 'id', title: 'Id' },
                { data: 'nombre', title: 'Nombre' },
                { data: 'apellido', title: 'Apellido' },
                {
                    data: function (data) {
                        return moment(data.fecha_Nacimiento).format('DD/MM/YYYY');
                    }, title: 'Fecha de Nacimiento'
                },
                { data: 'mail', title: 'Mail' },
                { data: 'roles.nombre', title: 'Rol' },
                {
                    data: function (data) {
                        console.log(data);
                        return data.activo == true ? "Si" : "No";
                    }, title: 'Activo'
                },
                {
                    data: function (data) {
                        var botones =
                            `<td><a href='javascript:EditarUsuario(${JSON.stringify(data)})'/><i class="fa-solid fa-pen-to-square editar-usuario"></i></td>` +
                            `<td><a href='javascript:EliminarUsuario(${JSON.stringify(data)})'/><i class="fa-solid fa-trash eliminar-usuario"></i></td>`;
                        return botones;
                    }
                }

            ],
            language: {
                url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json',
            },
        }
    );
});


function GuardarUsuario() {
    $("#usuariosAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Usuarios/UsuariosAddPartial",
        data: "",
        contentType:"application/json",
        dataType: "html",
        success: function (data) {
            $("#usuariosAddPartial").html(data);
            $("#usuarioModal").modal('show');
        }
       
    });
}

function EditarUsuario(data) {
    $("#usuariosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Usuarios/UsuariosAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#usuariosAddPartial").html(data);
            $("#usuarioModal").modal('show');
        }
    });
}

function EliminarUsuario(data) {
    Swal.fire({
        title: 'Estas por eliminar un usuario',
        text: "Quieres eliminar el usuario?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: "/Usuarios/EliminarUsuario",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (data) {
                    Swal.fire(
                        'Eliminado!',
                        'El usuario se elimino correctamente.',
                        'success'
                    )
                    tablaUsuarios.ajax.reload();
                }
            });
            
        }
    })
}
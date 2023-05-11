var tablaRoles;

$(document).ready(function () {
    tablaRoles = $('#roles').DataTable(
        {
            ajax: {
                url: 'https://localhost:7175/api/Roles/BuscarRoles',
                dataSrc: ""
            },
            columns: [
                { data: 'id', title: 'Id' },
                { data: 'nombre', title: 'Nombre' },             
                {
                    data: function (data) {
                        console.log(data);
                        return data.activo == true ? "Si" : "No";
                    }, title: 'Activo'
                },
                {
                    data: function (data) {
                        var botones =
                            `<td><a href='javascript:EditarRol(${JSON.stringify(data)})'/><i class="fa-solid fa-pen-to-square editar-rol"></i></td>` +
                            `<td><a href='javascript:EliminarRol(${JSON.stringify(data)})'/><i class="fa-solid fa-trash eliminar-rol"></i></td>`;
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


function GuardarRol() {
    $("#rolesAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Roles/RolesAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#rolesAddPartial").html(data);
            $("#rolModal").modal('show');
        }

    });
}

function EditarRol(data) {
    $("#rolesAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Roles/RolesAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#rolesAddPartial").html(data);
            $("#rolModal").modal('show');
        }
    });
}

function EliminarRol(data) {
    Swal.fire({
        title: 'Estas por eliminar un rol',
        text: "Quieres eliminar el rol?",
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
                url: "/Roles/EliminarRol",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (data) {
                    Swal.fire(
                        'Eliminado!',
                        'El rol se elimino correctamente.',
                        'success'
                    )
                    tablaRoles.ajax.reload();
                }
            });

        }
    })
}
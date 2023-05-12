var tablaServicios;

$(document).ready(function () {
    tablaServicios = $('#servicios').DataTable(
        {
            ajax: {
                url: 'https://localhost:7175/api/Servicios/BuscarServicios',
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
                            `<td><a href='javascript:EditarServicio(${JSON.stringify(data)})'/><i class="fa-solid fa-pen-to-square editar-servicio"></i></td>` +
                            `<td><a href='javascript:EliminarServicio(${JSON.stringify(data)})'/><i class="fa-solid fa-trash eliminar-servicio"></i></td>`;
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


function GuardarServicio() {
    $("#serviciosAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Servicios/ServiciosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#serviciosAddPartial").html(data);
            $("#servicioModal").modal('show');
        }

    });
}

function EditarServicio(data) {
    $("#serviciosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#serviciosAddPartial").html(data);
            $("#servicioModal").modal('show');
        }
    });
}

function EliminarServicio(data) {
    Swal.fire({
        title: 'Estas por eliminar un servicio',
        text: "Quieres eliminar el servicio?",
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
                url: "/Servicios/EliminarServicio",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (data) {
                    Swal.fire(
                        'Eliminado!',
                        'El servicio se elimino correctamente.',
                        'success'
                    )
                    tablaServicios.ajax.reload();
                }
            });

        }
    })
}
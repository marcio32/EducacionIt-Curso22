var tablaProductos;

$(document).ready(function () {
    tablaProductos = $('#productos').DataTable(
        {
            ajax: {
                url: 'https://localhost:7175/api/Productos/BuscarProductos',
                dataSrc: ""
            },
            columns: [
                { data: 'id', title: 'Id' },
                { data: 'descripcion', title: 'Descripcion' },
                { data: 'precio', title: 'Precio' },
                { data: 'stock', title: 'Stock' },
                {
                    data: 'imagen', render: function (data) {
                        if (data != null) {
                            return '<img src="data:image/jpeg;base64,' + data + '"width="100x" height="100px">';
                        } else {
                            return '<img src="/images/noexiste.jpg" width="100x" height="100px">';
                        }

                    } ,title: 'Imagen' },
                {
                    data: function (data) {
                        console.log(data);
                        return data.activo == true ? "Si" : "No";
                    }, title: 'Activo'
                },
                {
                    data: function (data) {
                        var botones =
                            `<td><a href='javascript:EditarProducto(${JSON.stringify(data)})'/><i class="fa-solid fa-pen-to-square editar-producto"></i></td>` +
                            `<td><a href='javascript:EliminarProducto(${JSON.stringify(data)})'/><i class="fa-solid fa-trash eliminar-producto"></i></td>`;
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


function GuardarProducto() {
    $("#productosAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Productos/ProductosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#productosAddPartial").html(data);
            $("#productoModal").modal('show');
        }

    });
}

function EditarProducto(data) {
    $("#productosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $("#productosAddPartial").html(data);
            $("#productoModal").modal('show');
        }
    });
}

function EliminarProducto(data) {
    Swal.fire({
        title: 'Estas por eliminar un producto',
        text: "Quieres eliminar el producto?",
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
                url: "/Productos/EliminarProducto",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (data) {
                    Swal.fire(
                        'Eliminado!',
                        'El producto se elimino correctamente.',
                        'success'
                    )
                    tablaProductos.ajax.reload();
                }
            });

        }
    })
}
function ActualizarCredito() {
    $.ajax({
        type: "GET",
        url: '/ConsultaAsync/ConsultarCreditoLabel',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (creditos) {
            // Actualizar el elemento HTML con los créditos actuales
            document.getElementById('ActualizarCreditos').innerHTML = creditos['CreditosActuales'];
        },
        error: function (xhr, status, error) {
            // Manejo de errores
            console.error("Error al intentar actualizar los créditos:", status, error);
            // Puedes mostrar un mensaje de error al usuario o realizar alguna otra acción aquí
        }
    });
}


}
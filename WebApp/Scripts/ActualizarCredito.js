function ActualizarCredito() {

    try {
        $.ajax({
            type: "GET",
            url: '/ConsultaAsync/ConsultarCreditoLabel',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: procesarCreditos
        });

    } catch (e) {
       
    }

    function procesarCreditos(creditos) {
        document.getElementById('ActualizarCreditos').innerHTML = creditos['CreditosActuales'];

    }

}
function Asignar_Ambiente_A_Propiedad( cantAmbientes) {
    
    var TipoProp = document.getElementsByName('TipoPropiedad')[0].value;
    var TipoAmb = new Array();

    for (var cont = 0; cont < cantAmbientes; cont++) {
        if (document.getElementsByName('TipoAmbiente')[cont].checked) {
            TipoAmb.push(document.getElementsByName('TipoAmbiente')[cont].value);
        }
    }
    var parametros = { "TipoPropiedad": TipoProp, "TipoAmbiente": TipoAmb };
    URL = "/ABMTipoPropiedadTipoAmbiente/AsignarTipoPropiedadTipoAmbiente";

    AjaxAsyn(URL, parametros);
    
}
function Asignar_Caracteristica_A_Propiedad(cantCaracteristica) {
    var TipoProp = document.getElementsByName('TipoPropiedad')[0].value;
    var Caract = new Array();

    for (var cont = 0; cont < cantAmbientes; cont++) {
        if (document.getElementsByName('Caracteristica')[cont].checked) {
            Caract.push(document.getElementsByName('Caracteristica')[cont].value);
        }
    }
    var parametros = { "TipoPropiedad": TipoProp, "Caracteristica": Caract };
    URL = "/ABMTipoPropiedadCarecteristia/AsignarTipoPropiedadTipoCaracteristica";

    AjaxAsyn(URL, parametros);
}

function CrearPlan() {
    var NombrePlan = document.getElementsByName('NombrePlan')[0].value;
    var Precio = document.getElementsByName('Precio')[0].value;
    var TipoMoneda = document.getElementsByName('TipoMoneda')[0].value;
    var CantCreditos = document.getElementsByName('CantCreditos')[0].value;
    var CantImage = document.getElementsByName('CantImage')[0].value;
    if (document.getElementsByName('PermitirVideo')[0].checked) {
        var PermitirVideo = true;
    } else {
        var PermitirVideo = false;
    }
    if (document.getElementsByName('PermitirEstadisticas')[0].checked) {
        var PermitirEstadisticas = true;
    } else {
        var PermitirEstadisticas = false;
    }

    var parametros = {
        "NombrePlan": NombrePlan, "TipoMoneda": TipoMoneda, "PermitirVideo": PermitirVideo,
        "PermitirEstadisticas": PermitirEstadisticas, "Precio": Precio,
        "CantCreditos": CantCreditos, "CantImage": CantImage
    };

    var URL = "/ABMPlan/CrearPlan";

    AjaxAsyn(URL, parametros);

}

function AjaxAsyn(URL, parametros) {
    try {
        $.ajax({
            type: "POST",
            url: URL,
            data: JSON.stringify(parametros),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: OperationResult
        });
    } catch (e) {
        throw e;
        alert("Uuups algo pasó con la página, disculpe.");
    }
}

function OperationResult(Result) {
    if (Result == "OK") {
        document.getElementById('resultadoOperacion').innerText = "Operación exitosa.";
    } else {
        document.getElementById('resultadoOperacion').innerText = "Operación fallida.";
    }
}
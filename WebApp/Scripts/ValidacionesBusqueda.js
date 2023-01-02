function verificarInputs() {
    var preciomin = document.getElementsByName('precioDesde')[0].value.replace(/[.]/g, "");
    var preciomax = document.getElementsByName('precioHasta')[0].value.replace(/[.]/g, "");
    var precioD = parseInt(preciomin);
    var precioH = parseInt(preciomax);
    var precio = -1;//document.getElementsByName('precio')[0].value;
    
    var x = true;

    if ((precioD > precioH) && (preciomax.length != 0) && (precio == -1)) {
        alert("Usted ha ingresado un precio mínimo, mayor al precio máximo");
        x = false;
    } else {
        if ((precioD == precioH) && (preciomin.length != 0) && (preciomax.length != 0) && (precio == -1)) {
            alert("Usted ha ingresado precios iguales")
            x = false;
        } else {
            if (((precio != -1) && (preciomin.length != 0) && (preciomax.length != 0)) || ((precio != -1) && (preciomin.length != 0)) || ((precio != -1) && (preciomax.length != 0))) {
                document.getElementsByName('precioDesde')[0].value = "";
                document.getElementsByName('precioHasta')[0].value = "";
            } else {
                if ((preciomin.length == 0) && (preciomax.length == 0)) {
                    document.getElementsByName('precioDesde')[0].value = "";
                    document.getElementsByName('precioHasta')[0].value = "";
                } else {
                    try {
                        if ((preciomin.length != 0) && (preciomax.length == 0)) {
                            throw "Ingrese el precio máximo";
                        } else {
                            if ((preciomax.length != 0) && (preciomin.length == 0)) {
                                throw "Ingrese el precio mínimo";
                            }
                        }
                    } catch (error) {
                        alert("Por favor, " + error);
                        x = false;
                    }
                }
            }
        }
    }

    var SupTMax = document.getElementsByName('SupTMax')[0].value.replace(/[.]/g, "");
    var SupTMin = document.getElementsByName('SupTMin')[0].value.replace(/[.]/g, "");
    var SuperficieTMax = parseInt(SupTMax);
    var SuperficieTMin = parseInt(SupTMin);

    if (SuperficieTMax < SuperficieTMin) {
        alert("Usted ha ingresado una superficie mínima, mayor a la superficie máxima");
        x = false;
    } else {
        if (SuperficieTMax == SuperficieTMin) {
            alert("Usted ha ingresado superficies iguales");
            x = false;
        } else {
            try {
                if ((SupTMin.length != 0) && (SupTMax.length == 0)) {
                    throw "Ingrese la superficie máxima";
                } else {
                    if ((SupTMax.length != 0) && (SupTMin.length == 0)) {
                        throw "Ingrese la superficie mínima";
                    }
                }
            } catch (error) {
                alert("Por favor, " + error);
                x = false;
            }
        }
    }


    return x;
}

var tipomoneda;

function desactivarprecio2(seleccionado) {
    
         tipomoneda = seleccionado.value;
    

    if (tipomoneda != -1) {
        document.getElementsByName('precioDesde')[0].disabled = false;
        document.getElementsByName('precioHasta')[0].disabled = false;
        document.getElementsByName('precioDesde')[0].value = "";
        document.getElementsByName('precioHasta')[0].value = "";
    } else {
        document.getElementsByName('precioDesde')[0].disabled = true;
        document.getElementsByName('precioHasta')[0].disabled = true;
        document.getElementsByName('precioDesde')[0].value = "";
        document.getElementsByName('precioHasta')[0].value = "";
    }
}

function desactivarprecio1() {
    var tipomoneda = document.getElementsByName('tipomoneda')[0].value;
    var precio = document.getElementsByName('precio')[0].value;

    if ((precio != -1) && (tipomoneda != -1)) {
        document.getElementById('precioDprecioH').style.display = "none";
    } else {
        if (tipomoneda == -1) {
            document.getElementById('preciorapido').style.display = "none";
            document.getElementsByName('precio')[0].value = -1;
            document.getElementById('precioDprecioH').style.display = "none";
        } else {
            document.getElementById('preciorapido').style.display = "block";
            document.getElementById('precioDprecioH').style.display = "block";
        }
    }
}

function format(input) {

    if (tipomoneda == 'ARS') {
        var num = input.value.replace(/\./g, '');
        if (!isNaN(num)) {
            num = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1.');
            num = num.split('').reverse().join('').replace(/^[\.]/, '');
            input.value = num;
        }
        else {
            alert('Solo se permiten numeros');
            input.value = input.value.replace(/[^\d\.]*/g, '');
        }
    } else {
        if (tipomoneda == 'USD') {
            var num = input.value.replace(/\,/g, '');
            if (!isNaN(num)) {
                num = num.toString().split('').reverse().join('').replace(/(?=\d*\,?)(\d{3})/g, '$1,');
                num = num.split('').reverse().join('').replace(/^[\,]/, '');
                input.value = num;
            }
            else {
                alert('Solo se permiten numeros');
                input.value = input.value.replace(/[^\d\,]*/g, '');
            }
        }
    }
}

function habilitarListboxs() {
    var x = document.getElementsByName('tipoPropiedad')[0].value;

    switch (x) {
        case 'Baulera':
            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = true;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Campo':

            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = true;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = false;
            document.getElementById('SupTMax').disabled = false;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Casa':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = false;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Chacra':

            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = true;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = false;
            document.getElementById('SupTMax').disabled = false;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Cochera':

            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Consultorio':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Departamento':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = false;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Depósito':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Edificio':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Edificio Industrial':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Finca':

            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = true;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = true;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = false;
            document.getElementById('SupTMax').disabled = false;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Galpón':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Hotel':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = false;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Local Comercial':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Lote':

            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = true;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = true;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = false;
            document.getElementById('SupTMax').disabled = false;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        case 'Oficina':

            document.getElementById('cantidadBaños').disabled = false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = false;
            break;
        case 'Quinta':

            document.getElementById('cantidadBaños').disabled= false;
            document.getElementById('cantidadCocheras').disabled = false;
            document.getElementById('cantidadDormitorios').disabled = false;
            document.getElementById('añosAntiguedad').disabled = false;
            document.getElementById('cantidadPlantas').disabled = false;
            document.getElementById('SupTMin').disabled = false;
            document.getElementById('SupTMax').disabled = false;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
        default:
            document.getElementById('cantidadBaños').disabled = true;
            document.getElementById('cantidadCocheras').disabled = true;
            document.getElementById('cantidadDormitorios').disabled = true;
            document.getElementById('añosAntiguedad').disabled = true;
            document.getElementById('cantidadPlantas').disabled = true;
            document.getElementById('SupTMin').disabled = true;
            document.getElementById('SupTMax').disabled = true;
            document.getElementById('cantidadAmbientes').disabled = true;
            break;
    }
}

function resetform() {
    document.getElementById('form').reset();
}

/*function habilitarProvincias() { No utilizado
    var paisSelected = document.getElementById('pais').value;

    if (paisSelected == -1) {
        document.getElementById('blockProvincia').style.display = "none";
        document.getElementById('provincia').options[0].value = -1;
    } else {
        document.getElementById('blockProvincia').style.display = "block";
        try {
            $.ajax({
                type: "GET",
                url: 'Consulta/ConsultarProvincias',
                data: { pais: paisSelected },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: procesarprovincia
            });
        } catch (e) {
            throw e;
            alert("Uuups algo pasó con la página, disculpe.");
        }
    }
}

function procesarprovincia(resultado) {

    document.getElementById('provincia').length = resultado.length + 1;

    document.getElementById('provincia').options[0].value = -1;
    document.getElementById('provincia').options[0].text = "Seleccione una opción";

    for (var x = 0; x < resultado.length; x++) {
        document.getElementById('provincia').options[x + 1].value = resultado[x]['nombreProvincia'];
        document.getElementById('provincia').options[x + 1].text = resultado[x]['nombreProvincia'];
    }
}


function habilitarCiudades() {
    var provinciaSelected = document.getElementById('provincia').value;

    if (provinciaSelected == -1) {
        document.getElementById('blockCiudad').style.display = "none";
        document.getElementById('ciudad').options[0].value = -1;
    } else {
        document.getElementById('blockCiudad').style.display = "block";
        try {
            $.ajax({
                type: "GET",
                url: 'Consulta/ConsultarCiudades',
                data: { provincia: provinciaSelected },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: procesarciudades
            });
        } catch (e) {
            throw e;
            alert("Uuups algo pasó con la página, disculpe.");
        }
    }
}

function procesarciudades(resultado) {
    document.getElementById('ciudad').length = resultado.length + 1;

    document.getElementById('ciudad').options[0].value = -1;
    document.getElementById('ciudad').options[0].text = "Seleccione una opción";

    for (var x = 0; x < resultado.length; x++) {
        document.getElementById('ciudad').options[x + 1].value = resultado[x]['nombreCiudad'];
        document.getElementById('ciudad').options[x + 1].text = resultado[x]['nombreCiudad'];
    }
}

*/
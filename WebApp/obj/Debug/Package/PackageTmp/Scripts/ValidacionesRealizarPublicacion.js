
var tipomoneda

function initialvalor() {
    select = document.getElementsByName('tipoMoneda')[0].value;
    tipomoneda = document.getElementById(select).innerText;
}

function valor(Moneda) {
    select = Moneda.value;
    tipomoneda = document.getElementById(select).innerText;

    if (tipomoneda == null) {
        document.getElementById('precioPropiedad').disabled = true;
    } else {
        if (tipomoneda == 'ARS'){
            document.getElementById('precioPropiedad').value = "";
            document.getElementById('precioPropiedad').disabled = false;
        } else {
            document.getElementById('precioPropiedad').value = "";
            document.getElementById('precioPropiedad').disabled = false;
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

function formatsurfaceyear(input) {

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
}

function habilitarLabelTable() {
    
    var select = document.getElementById('tipoPropiedad');
    var option = select.options[select.selectedIndex].text;

    switch (option) {
        case 'Baulera': //Baulera
            document.getElementById('table').style.display = "none";
            document.getElementById('etiqueta1').style.display = "none";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "none";
            break;
        case 'Campo': //Campo
            document.getElementById('table').style.display = "none";
            document.getElementById('etiqueta1').style.display = "none";
            document.getElementById('etiqueta2').style.display = "block";
            document.getElementById('superficie').style.display = "block";
            break;
        case 'Casa': //Casa
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                document.getElementById('table').rows[x].style.display = "block";
            }
            break;
        case 'Chacra'://Chacra
            document.getElementById('table').style.display = "none";
            document.getElementById('etiqueta1').style.display = "none";
            document.getElementById('etiqueta2').style.display = "block";
            document.getElementById('superficie').style.display = "block";
            break;
        case 'Cochera'://Cochera
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 1) || (x == 4)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Consultorio'://Consultorio
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Departamento'://Departamento
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                document.getElementById('table').rows[x].style.display = "block";
            }
            break;
        case 'Depósito'://Depósito
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 1) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Edificio'://Edificio
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 1) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Edificio Industrial'://Edificio Industrial
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 1) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Finca'://Finca
            document.getElementById('table').style.display = "none";
            document.getElementById('etiqueta1').style.display = "none";
            document.getElementById('etiqueta2').style.display = "block";
            document.getElementById('superficie').style.display = "block";
            break;
        case 'Galpón'://Galpón
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Hotel'://Hotel
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                document.getElementById('table').rows[x].style.display = "block";
            }
            break;
        case 'Local Comercial'://Local Comercial
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 1) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Lote'://Lote
            document.getElementById('table').style.display = "none";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            break;
        case 'Oficina'://Oficina
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "block";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                if ((x == 0) || (x == 4) || (x == 3)) {
                    document.getElementById('table').rows[x].style.display = "block";
                } else {
                    document.getElementById('table').rows[x].style.display = "none";
                }
            }
            break;
        case 'Quinta'://Quinta
            document.getElementById('table').style.display = "block";
            document.getElementById('etiqueta1').style.display = "none";
            document.getElementById('etiqueta2').style.display = "block";
            document.getElementById('superficie').style.display = "block";
            for (var x = 0; x < filas; x++) {
                document.getElementById('table').rows[x].style.display = "block";
            }
            break;
        default:
            document.getElementById('table').style.display = "none";
            document.getElementById('etiqueta1').style.display = "none";
            document.getElementById('etiqueta2').style.display = "none";
            document.getElementById('superficie').style.display = "none";
            break;
    }
}